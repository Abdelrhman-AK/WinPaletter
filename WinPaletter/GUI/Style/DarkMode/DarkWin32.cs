using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.NativeMethods.UxTheme;

namespace WinPaletter.UI.Dark
{
    /// <summary>
    /// Applies dark mode to native common dialogs of the current thread:
    /// <list type="bullet">
    ///   <item>File / icon-picker dialogs (path edit, owner-drawn icon list view, auto-suggest dropdown).</item>
    ///   <item>The Windows Font dialog (comdlg32!ChooseFont / CHOOSEFONT), verified against Win11 22H2/23H2.</item>
    /// </list>
    ///
    /// <br></br>Font dialog notes:
    ///  <br></br>- Dialog is class #32770; detection anchors on control ID 1136.
    ///  <br></br>- Font / Style / Size are ComboBoxes; their dropdown list is a 'ComboLBox' child.
    ///    WM_DRAWITEM for the items is delivered to the dialog, with dis.hwndItem = the ComboLBox.
    ///  <br></br>- Color combo is at ID 1139. Script combo has an unstable ID (1140 on Win11) and is identified
    ///    by exclusion during combo enumeration. It gets DarkMode_CFD/DarkMode_Explorer theming only,
    ///    so the closed combo renders natively; its popup list is self-painted (see ScriptListSubclassProc).
    ///  <br></br>- The Sample/preview is a plain Static painted by the "Sample" group box's paint pass,
    ///    so it is redrawn from the group-box proc.
    ///  <br></br>- CBS_DROPDOWNLIST combos ignore DarkMode_CFD for their closed state; we redraw it with a 1px inset
    ///    so the border survives, and we leave the rightmost <see cref="COMBO_ARROW_RESERVE"/> px for the arrow.
    ///  <br></br>- Font list items are drawn in their own font, Style items in their own weight/italic,
    ///    and the preview in the currently selected combination.
    ///  <br></br>- The group-box caption background is filled only as wide as the caption text so the
    ///    group box's top border line stays visible on both sides.
    /// <br></br>
    /// <br></br>IMPORTANT: all User32/GDI32 P/Invokes used here MUST be CharSet.Unicode.
    /// </summary>
    public class DarkWin32 : IDisposable
    {
        #region Constants

        // Hooks
        private const int WH_CALLWNDPROCRET = 12;
        private const int WH_CBT = 5;
        private const int HCBT_CREATEWND = 3;
        private const int HCBT_DESTROYWND = 4;
        private const int HCBT_ACTIVATE = 5;

        // CWPRETSTRUCT { LRESULT lResult; LPARAM lParam; WPARAM wParam; UINT message; HWND hwnd; }
        // Reading the two fields we need directly avoids marshaling a struct for EVERY message on the thread.
        private static readonly int CwpRetMessageOffset = IntPtr.Size * 3;
        private static readonly int CwpRetHwndOffset = IntPtr.Size * 4;

        // Subclass IDs
        private const int SUBCLASS_ID_DIALOG = 1;
        private const int SUBCLASS_ID_LISTVIEW = 2;
        private const int SUBCLASS_ID_DROPDOWN = 3;
        private const int SUBCLASS_ID_FONTDLG = 10;
        private const int SUBCLASS_ID_FONTLIST = 11;
        private const int SUBCLASS_ID_FONTSAMPLE = 12;
        private const int SUBCLASS_ID_FONTCOMBO = 13;
        private const int SUBCLASS_ID_FONTGROUPBOX = 14;
        private const int SUBCLASS_ID_SCRIPTCOMBO = 15;
        private const int SUBCLASS_ID_SCRIPTLIST = 16;

        // Common file dialog
        private const int CTRL_ID_PATH_EDIT = 12290;
        private const int CTRL_ID_OK_BUTTON = 1;
        private const int EN_CHANGE = 0x0300;
        private const int CBN_DROPDOWN = 7;

        // List view
        private const uint LVM_FIRST = 0x1000;
        private const uint LVM_SETBKCOLOR = LVM_FIRST + 1;
        private const uint LVM_SETTEXTCOLOR = LVM_FIRST + 3;
        private const uint LVM_SETTEXTBKCOLOR = LVM_FIRST + 38;
        private const uint LVM_GETITEMTEXTW = LVM_FIRST + 115;

        // Owner-draw state flags
        private const uint ODS_SELECTED = 0x0001;
        private const uint ODS_DISABLED = 0x0004;
        private const uint ODS_FOCUS = 0x0010;

        // ListBox / ComboBox messages
        private const uint LB_SETCURSEL = 0x0186;
        private const uint LB_GETCURSEL = 0x0188;
        private const uint LB_GETTEXT = 0x0189;
        private const uint LB_GETCOUNT = 0x018B;
        private const uint LB_GETTOPINDEX = 0x018E;
        private const uint LB_SETTOPINDEX = 0x0197;
        private const uint LB_GETITEMRECT = 0x0198;
        private const uint LB_SETBKCOLOR = 0x0199;
        private const uint LB_SETCARETINDEX = 0x019E;
        private const uint CB_GETCURSEL = 0x0147;
        private const uint CB_GETLBTEXT = 0x0148;
        private const uint CB_GETCOMBOBOXINFO = 0x0164;

        // Misc Win32
        private const int GWL_STYLE = -16;
        private const int BS_GROUPBOX = 0x00000007;
        private const int SS_TYPEMASK = 0x0000001F;
        private const int SS_BLACKFRAME = 0x00000007;
        private const int SS_WHITEFRAME = 0x00000006;
        private const int SS_OWNERDRAW = 0x0000000B;
        private const int DI_NORMAL = 0x0003;
        private const int LOGPIXELSY = 90;
        private const int SRCCOPY = 0x00CC0020;
        private const int COLOR_WHITE = 0x00FFFFFF;
        private const int COLOR_DISABLED_TEXT = 0x00808080;
        private const int RDW_REFRESH = (int)(User32.RedrawWindowFlags.Invalidate | User32.RedrawWindowFlags.Erase | User32.RedrawWindowFlags.UpdateNow);

        // Font dialog control IDs (comdlg32.dll / ChooseFont)
        private const int FONTDLG_ID_LIST_FONT = 0x0470;   // 1136 - font ComboBox
        private const int FONTDLG_ID_LIST_STYLE = 0x0471;  // 1137 - style ComboBox
        private const int FONTDLG_ID_LIST_SIZE = 0x0472;   // 1138 - size ComboBox
        private const int FONTDLG_ID_COMBO_COLOR = 0x0473; // 1139 - color ComboBox
        private const int FONTDLG_ID_SAMPLE = 0x0476;      // 1142 - sample Static
        private const int FONTDLG_ID_CHECK_STRIKE = 0x0410;
        private const int FONTDLG_ID_CHECK_UNDER = 0x0411;

        private static readonly int[] FontListComboIds = [FONTDLG_ID_LIST_FONT, FONTDLG_ID_LIST_STYLE, FONTDLG_ID_LIST_SIZE];
        private static readonly int[] FontCheckBoxIds = [FONTDLG_ID_CHECK_STRIKE, FONTDLG_ID_CHECK_UNDER];

        private const int MAX_ITEM_TEXT = 512;          // chars, shared text buffer capacity
        private const int MAX_FACE_NAME = 31;           // lfFaceName is 32 chars incl. null
        private const int MAX_CACHED_FONTS = 512;       // safety valve for the HFONT cache
        private const int COMBO_ARROW_RESERVE = 20;     // px kept free on the right of a combo's closed state
        private const int DEFAULT_PREVIEW_POINTS = 12;  // what ChooseFont seeds itself with
        private const int CAPTION_PAD_LEFT = 9;
        private const int CAPTION_PAD_RIGHT = 0;
        private const int SAMPLE_MIN_WIDTH = 100;
        private const int SAMPLE_MIN_HEIGHT = 30;

        private static readonly string FallbackPreviewText = Application.ProductName;

        private static readonly Regex WhitespaceRegex = new(@"\s+");
        private static readonly Regex ItalicWordRegex = new(@"\b(italic|oblique)\b", RegexOptions.IgnoreCase);

        #endregion

        #region Fields

        private readonly int DARK_COLOR_INT = (int)DarkColors.kPrimary.Value;
        private readonly int DARK_COLOR_SELECTION_INT = (int)DarkColors.kSeparator.Value;

        private bool _disposed;
        private IntPtr _hookId = IntPtr.Zero;
        private IntPtr _cbtHookId = IntPtr.Zero;

        // Delegates are fields so the GC can't collect them while native code holds the pointer.
        private readonly User32.HookProc _hookDelegate;
        private readonly User32.HookProc _cbtDelegate;
        private readonly Comctl32.SUBCLASSPROC _dialogProc;
        private readonly Comctl32.SUBCLASSPROC _listViewProc;
        private readonly Comctl32.SUBCLASSPROC _dropdownProc;
        private readonly Comctl32.SUBCLASSPROC _fontDialogProc;
        private readonly Comctl32.SUBCLASSPROC _fontListProc;
        private readonly Comctl32.SUBCLASSPROC _fontSampleProc;
        private readonly Comctl32.SUBCLASSPROC _fontComboProc;
        private readonly Comctl32.SUBCLASSPROC _fontGroupBoxProc;
        private readonly Comctl32.SUBCLASSPROC _scriptComboProc;
        private readonly Comctl32.SUBCLASSPROC _scriptListProc;

        // Every subclassed window -> (proc, id). Storing the proc makes Dispose exact for every subclass.
        private readonly Dictionary<IntPtr, (Comctl32.SUBCLASSPROC Proc, UIntPtr Id)> _activeSubclasses = [];

        // Controls that already received their one-time theming / colors.
        private readonly HashSet<IntPtr> _themedControls = [];
        private readonly HashSet<IntPtr> _colorizedLists = [];

        // GDI objects, created lazily and deleted in Dispose.
        private IntPtr _darkBrush = IntPtr.Zero;
        private IntPtr _selectionBrush = IntPtr.Zero;
        private IntPtr _whiteBrush = IntPtr.Zero;
        private IntPtr _whitePen = IntPtr.Zero;
        private IntPtr _borderPen = IntPtr.Zero;
        private readonly User32.POINT[] _arrowPoints = new User32.POINT[3];

        // Reusable unmanaged scratch buffers (UI thread only), freed in Dispose.
        private IntPtr _textBuf = IntPtr.Zero;
        private IntPtr _rectBuf = IntPtr.Zero;
        private IntPtr _comboInfoBuf = IntPtr.Zero;
        private IntPtr _lvItemBuf = IntPtr.Zero;

        // File dialog state
        private string _acceptedPath = string.Empty;
        private IntPtr _targetDialogHwnd = IntPtr.Zero;
        private readonly Dictionary<int, IntPtr> _iconCache = [];
        private string _iconCachePath = string.Empty;
        private readonly Dictionary<uint, bool> _isListViewCtl = [];

        // Font dialog state (reset when the dialog is destroyed)
        private IntPtr _fontDialogHwnd = IntPtr.Zero;
        private IntPtr _sampleHwnd = IntPtr.Zero;
        private IntPtr _fontComboHwnd = IntPtr.Zero;
        private IntPtr _styleComboHwnd = IntPtr.Zero;
        private IntPtr _sizeComboHwnd = IntPtr.Zero;
        private IntPtr _scriptComboHwnd = IntPtr.Zero;
        private IntPtr _scriptListHwnd = IntPtr.Zero;   // the script popup list we already themed
        private string _sampleText = FallbackPreviewText;
        private readonly HashSet<IntPtr> _themedFontChildren = [];
        private readonly Dictionary<IntPtr, string> _captionCache = [];
        private readonly Dictionary<(string Face, int Weight, bool Italic, int Height, int Width), IntPtr> _fontCache = [];
        private readonly Dictionary<(string Family, string Style), (string Face, int Weight, bool Italic)> _faceCache = [];
        private int _itemFontHeight = -16;
        private int _itemFontWidth;
        private bool _itemFontMetricsRead;
        private int _dpiY;

        [ThreadStatic] private static StringBuilder _classNameBuffer;

        private enum FontListKind { Unknown, Font, Style, Size }

        private enum ComboPopupKind { None, Combo, ScriptCombo }

        [StructLayout(LayoutKind.Sequential)]
        private struct COMBOBOXINFO_LOCAL
        {
            public int cbSize;
            public RECT rcItem;
            public RECT rcButton;
            public int stateButton;
            public IntPtr hwndCombo;
            public IntPtr hwndItem;
            public IntPtr hwndList;
        }

        private static readonly int ComboInfoSize = Marshal.SizeOf<COMBOBOXINFO_LOCAL>();

        #endregion

        #region Lazy resources

        private IntPtr DarkBrush => _darkBrush != IntPtr.Zero ? _darkBrush : (_darkBrush = GDI32.CreateSolidBrush(DARK_COLOR_INT));
        private IntPtr SelectionBrush => _selectionBrush != IntPtr.Zero ? _selectionBrush : (_selectionBrush = GDI32.CreateSolidBrush(DARK_COLOR_SELECTION_INT));
        private IntPtr WhiteBrush => _whiteBrush != IntPtr.Zero ? _whiteBrush : (_whiteBrush = GDI32.CreateSolidBrush(COLOR_WHITE));
        private IntPtr WhitePen => _whitePen != IntPtr.Zero ? _whitePen : (_whitePen = GDI32.CreatePen(0 /*PS_SOLID*/, 1, COLOR_WHITE));
        private IntPtr BorderPen => _borderPen != IntPtr.Zero ? _borderPen : (_borderPen = GDI32.CreatePen(0, 1, DarkColors.kTextInstruct));

        private IntPtr TextBuffer => _textBuf != IntPtr.Zero ? _textBuf : (_textBuf = Marshal.AllocHGlobal((MAX_ITEM_TEXT + 1) * sizeof(char)));
        private IntPtr RectBuffer => _rectBuf != IntPtr.Zero ? _rectBuf : (_rectBuf = Marshal.AllocHGlobal(Marshal.SizeOf<RECT>()));
        private IntPtr ComboInfoBuffer => _comboInfoBuf != IntPtr.Zero ? _comboInfoBuf : (_comboInfoBuf = Marshal.AllocHGlobal(ComboInfoSize));
        private IntPtr LvItemBuffer => _lvItemBuf != IntPtr.Zero ? _lvItemBuf : (_lvItemBuf = Marshal.AllocHGlobal(Marshal.SizeOf<User32.LVITEM>()));

        private static bool IsLegacyOS => OS.WXP || OS.WVista || OS.W7 || OS.W8x;
        private static bool _debug = false;

        #endregion

        #region Construction

        public DarkWin32()
        {
            if (!Program.Style.DarkMode) return;
            if (IsLegacyOS) return;

            _dialogProc = DialogSubclassProc;
            _listViewProc = ListViewSubclassProc;
            _dropdownProc = DropdownSubclassProc;
            _fontDialogProc = FontDialogSubclassProc;
            _fontListProc = FontListSubclassProc;
            _fontSampleProc = FontSampleSubclassProc;
            _fontComboProc = FontComboSubclassProc;
            _fontGroupBoxProc = FontGroupBoxSubclassProc;
            _scriptComboProc = ScriptComboSubclassProc;
            _scriptListProc = ScriptListSubclassProc;
            _hookDelegate = HookProc;
            _cbtDelegate = CbtProc;

            uint threadId = Kernel32.GetCurrentThreadId();
            _hookId = User32.SetWindowsHookEx(WH_CALLWNDPROCRET, _hookDelegate, IntPtr.Zero, threadId);
            _cbtHookId = User32.SetWindowsHookEx(WH_CBT, _cbtDelegate, IntPtr.Zero, threadId);

            Log("[DarkWin32] Constructed.");
        }

        #endregion

        #region Small helpers

        /// <summary>Compiled out of Release builds, arguments included (no string interpolation cost).</summary>
        [Conditional("DEBUG")]
        private static void Log(string message)
        {
            if (_debug) Program.Log?.Debug(message);
        }

        private static string GetClassName(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) return string.Empty;

            StringBuilder sb = _classNameBuffer ??= new StringBuilder(256);
            sb.Clear();
            int len = User32.GetClassName(hWnd, sb, sb.Capacity);

            return len > 0 ? sb.ToString() : string.Empty;
        }

        private static string GetWindowTextSafe(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) return string.Empty;

            int len = User32.GetWindowTextLength(hWnd);
            if (len <= 0) return string.Empty;

            StringBuilder sb = new(len + 1);
            User32.GetWindowText(hWnd, sb, sb.Capacity);
            return sb.ToString();
        }

        private static int LoWord(IntPtr value) => (int)((long)value & 0xFFFF);
        private static int HiWord(IntPtr value) => (int)(((long)value >> 16) & 0xFFFF);

        private static void Redraw(IntPtr hWnd) => User32.RedrawWindow(hWnd, IntPtr.Zero, IntPtr.Zero, RDW_REFRESH);

        private static void RedrawFull(IntPtr hWnd) => User32.RedrawWindow(hWnd, IntPtr.Zero, IntPtr.Zero, User32.RedrawWindowFlags.Invalidate | User32.RedrawWindowFlags.Erase | User32.RedrawWindowFlags.Frame | User32.RedrawWindowFlags.UpdateNow);

        private static void DrawLabel(IntPtr hdc, string text, ref RECT rc) => User32.DrawText(hdc, text, -1, ref rc, GDI32.DT_LEFT | GDI32.DT_VCENTER | GDI32.DT_SINGLELINE | GDI32.DT_NOPREFIX);

        private static void DrawCentered(IntPtr hdc, string text, ref RECT rc) => User32.DrawText(hdc, text, -1, ref rc, GDI32.DT_CENTER | GDI32.DT_VCENTER | GDI32.DT_SINGLELINE | GDI32.DT_NOPREFIX);

        /// <summary>
        /// Selects the control's own font into the DC (if it has one) and returns the previous font.
        /// </summary>
        private static IntPtr SelectControlFont(IntPtr hdc, IntPtr control)
        {
            IntPtr hFont = User32.SendMessage(control, (uint)User32.WindowsMessage.GetFont, IntPtr.Zero, IntPtr.Zero);
            return hFont != IntPtr.Zero ? GDI32.SelectObject(hdc, hFont) : IntPtr.Zero;
        }

        private static void RestoreFont(IntPtr hdc, IntPtr oldFont)
        {
            if (oldFont != IntPtr.Zero) GDI32.SelectObject(hdc, oldFont);
        }

        /// <summary>
        /// Reads a ListBox/ComboBox item into the shared buffer. Returns null when empty/invalid.
        /// </summary>
        private string GetItemText(IntPtr hWnd, uint message, int index)
        {
            IntPtr buf = TextBuffer;
            int len = (int)User32.SendMessage(hWnd, message, (IntPtr)index, buf);
            if (len <= 0) return null;

            return Marshal.PtrToStringUni(buf, Math.Min(len, MAX_ITEM_TEXT));
        }

        private string GetComboSelectionText(IntPtr combo)
        {
            if (combo == IntPtr.Zero) return null;

            int sel = (int)User32.SendMessage(combo, CB_GETCURSEL, IntPtr.Zero, IntPtr.Zero);
            return sel < 0 ? null : GetItemText(combo, CB_GETLBTEXT, sel);
        }

        /// <summary>
        /// Handles WM_ERASEBKGND by filling with the dark brush. Caller returns 1 when this returns true.
        /// </summary>
        private bool TryEraseBackground(IntPtr hWnd, uint uMsg, IntPtr hdc)
        {
            if (uMsg != (uint)User32.WindowsMessage.EraseBkgnd) return false;

            User32.GetClientRect(hWnd, out var rect);
            User32.FillRect(hdc, ref rect, DarkBrush);
            return true;
        }

        internal bool TryHandleColorMessage(uint uMsg, IntPtr wParam, out IntPtr brush)
        {
            if (uMsg >= (int)User32.WindowsMessage.CtlColorMsgBox && uMsg <= (int)User32.WindowsMessage.CtlColorStatic)
            {
                GDI32.SetTextColor(wParam, COLOR_WHITE);
                GDI32.SetBkColor(wParam, DARK_COLOR_INT);
                brush = DarkBrush;
                return true;
            }

            brush = IntPtr.Zero;
            return false;
        }

        private static void SetDarkTheme(IntPtr hWnd, string theme, bool resetFirst)
        {
            NativeMethods.Helpers.SetHWNDDarkMode(hWnd, true);

            if (resetFirst) UxTheme.SetWindowTheme(hWnd, string.Empty, string.Empty);

            UxTheme.SetWindowTheme(hWnd, theme, null);
        }

        #endregion

        #region Subclass registry

        /// <summary>
        /// Applies a subclass only once per hwnd and remembers (proc, id) so Dispose can remove it exactly.
        /// </summary>
        internal void SubclassWindow(IntPtr hWnd, Comctl32.SUBCLASSPROC proc, int id)
        {
            if (_activeSubclasses.ContainsKey(hWnd)) return;

            UIntPtr uid = (UIntPtr)id;
            Comctl32.SetWindowSubclass(hWnd, proc, uid, IntPtr.Zero);
            _activeSubclasses[hWnd] = (proc, uid);
        }

        internal void UnsubclassWindow(IntPtr hWnd)
        {
            if (!_activeSubclasses.TryGetValue(hWnd, out var entry)) return;

            _activeSubclasses.Remove(hWnd);
            Comctl32.RemoveWindowSubclass(hWnd, entry.Proc, entry.Id);
        }

        internal bool IsSubclassed(IntPtr hWnd) => _activeSubclasses.ContainsKey(hWnd);

        #endregion

        #region Hooks

        private IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // Fires for every message on the thread: peek at the message id only, no struct marshaling.
            if (nCode >= 0 && lParam != IntPtr.Zero && Marshal.ReadInt32(lParam, CwpRetMessageOffset) == (int)User32.WindowsMessage.InitDialog)
            {
                OnInitDialog(Marshal.ReadIntPtr(lParam, CwpRetHwndOffset));
            }

            return User32.CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        private void OnInitDialog(IntPtr dialogHwnd)
        {
            if (_targetDialogHwnd == IntPtr.Zero) _targetDialogHwnd = dialogHwnd;
            if (dialogHwnd != _targetDialogHwnd) return;

            CaptureAcceptedPath(dialogHwnd);

            NativeMethods.Helpers.SetHWNDDarkMode(dialogHwnd, Program.Style.DarkMode);
            SubclassWindow(dialogHwnd, _dialogProc, SUBCLASS_ID_DIALOG);

            foreach (IntPtr child in User32.GetChildWindowHandles(dialogHwnd)) ApplyDarkModeToControl(new Win32Control(child));
        }

        private IntPtr CbtProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            switch (nCode)
            {
                case HCBT_CREATEWND:
                case HCBT_ACTIVATE:
                    OnWindowCreatedOrActivated(wParam);
                    break;

                case HCBT_DESTROYWND:
                    OnWindowDestroyed(wParam);
                    break;
            }

            return User32.CallNextHookEx(_cbtHookId, nCode, wParam, lParam);
        }

        private void OnWindowCreatedOrActivated(IntPtr hWnd)
        {
            Win32Control ctrl = new(hWnd);

            if (ctrl.Type == Win32Control.ControlType.AutoSuggestDropdown) ApplyDarkModeToAutoSuggestDropdown(hWnd);
            else HandleFontDialogWindow(hWnd);
        }

        private void OnWindowDestroyed(IntPtr hWnd)
        {
            _activeSubclasses.Remove(hWnd);
            _themedControls.Remove(hWnd);
            _colorizedLists.Remove(hWnd);
            _themedFontChildren.Remove(hWnd);
            _captionCache.Remove(hWnd);

            if (hWnd == _scriptListHwnd) _scriptListHwnd = IntPtr.Zero;

            if (hWnd == _fontDialogHwnd) ResetFontDialogState();
        }

        #endregion

        #region File dialog / generic controls

        private void CaptureAcceptedPath(IntPtr dialogHwnd)
        {
            string text = GetWindowTextSafe(User32.GetDlgItem(dialogHwnd, CTRL_ID_PATH_EDIT));
            if (text.Length > 0) _acceptedPath = Environment.ExpandEnvironmentVariables(text);
        }

        private bool IsListViewControl(IntPtr dialogHwnd, uint ctlId)
        {
            if (_isListViewCtl.TryGetValue(ctlId, out bool cached)) return cached;

            bool isListView = Win32Control.FromParent(dialogHwnd, (int)ctlId)?.Type == Win32Control.ControlType.ListView;
            _isListViewCtl[ctlId] = isListView;
            return isListView;
        }

        private IntPtr DialogSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData)
        {
            if (Program.Style.DarkMode)
            {
                if (uMsg == (uint)User32.WindowsMessage.Command)
                {
                    int id = LoWord(wParam);
                    int code = HiWord(wParam);

                    bool capture = id == CTRL_ID_OK_BUTTON || (code == EN_CHANGE && Win32Control.FromParent(hWnd, id)?.Type == Win32Control.ControlType.Edit);

                    if (capture) CaptureAcceptedPath(hWnd);
                }
                else if (uMsg == (uint)User32.WindowsMessage.DrawItem && lParam != IntPtr.Zero)
                {
                    var dis = Marshal.PtrToStructure<User32.DRAWITEMSTRUCT>(lParam);

                    if (IsListViewControl(hWnd, dis.CtlID))
                    {
                        DrawIconListItem(ref dis);
                        return (IntPtr)1;
                    }
                }

                if (TryHandleColorMessage(uMsg, wParam, out IntPtr colorBrush))
                {
                    GDI32.SetBkMode(wParam, GDI32.TRANSPARENT);
                    return colorBrush;
                }
            }

            return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
        }

        private void DrawIconListItem(ref User32.DRAWITEMSTRUCT dis)
        {
            if ((int)dis.itemID < 0) return;

            bool selected = (dis.itemState & ODS_SELECTED) != 0;

            User32.FillRect(dis.hDC, ref dis.rcItem, selected ? SelectionBrush : DarkBrush);

            IntPtr hIcon = GetCachedIcon((int)dis.itemID);
            if (hIcon != IntPtr.Zero)
            {
                const int iconSize = 32;
                int x = dis.rcItem.left + ((dis.rcItem.right - dis.rcItem.left) - iconSize) / 2;
                int y = dis.rcItem.top + ((dis.rcItem.bottom - dis.rcItem.top) - iconSize) / 2;
                User32.DrawIconEx(dis.hDC, x, y, hIcon, iconSize, iconSize, 0, IntPtr.Zero, DI_NORMAL);
            }

            if (!selected) return;

            IntPtr oldPen = GDI32.SelectObject(dis.hDC, BorderPen);
            IntPtr oldBrush = GDI32.SelectObject(dis.hDC, GDI32.GetStockObject(GDI32.StockObjects.NULL_BRUSH));

            try
            {
                GDI32.Rectangle(dis.hDC, dis.rcItem.left + 1, dis.rcItem.top + 1, dis.rcItem.right - 1, dis.rcItem.bottom - 1);
            }
            finally
            {
                GDI32.SelectObject(dis.hDC, oldPen);
                GDI32.SelectObject(dis.hDC, oldBrush);
            }

            User32.DrawFocusRect(dis.hDC, ref dis.rcItem);
        }

        /// <summary>
        /// Icons are extracted once per (path, index) instead of on every paint.
        /// </summary>
        private IntPtr GetCachedIcon(int index)
        {
            if (!string.Equals(_iconCachePath, _acceptedPath, StringComparison.OrdinalIgnoreCase))
            {
                ClearIconCache();
                _iconCachePath = _acceptedPath;
            }

            if (_iconCache.TryGetValue(index, out IntPtr cached)) return cached;

            IntPtr hIcon = Shell32.ExtractIcon(IntPtr.Zero, _acceptedPath, index);
            if (hIcon == (IntPtr)1) hIcon = IntPtr.Zero; // 1 = "file is not an executable"

            _iconCache[index] = hIcon;
            return hIcon;
        }

        private void ClearIconCache()
        {
            foreach (IntPtr icon in _iconCache.Values)
            {
                if (icon != IntPtr.Zero) User32.DestroyIcon(icon);
            }

            _iconCache.Clear();
        }

        private IntPtr ListViewSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData)
        {
            if (Program.Style.DarkMode)
            {
                if (TryHandleColorMessage(uMsg, wParam, out IntPtr brush)) return brush;

                if (TryEraseBackground(hWnd, uMsg, wParam)) return (IntPtr)1;

                if (uMsg == (uint)User32.WindowsMessage.Paint)
                {
                    // Colors persist on the control: set them once, and again only after a theme change.
                    if (_colorizedLists.Add(hWnd))
                    {
                        User32.SendMessage(hWnd, LVM_SETBKCOLOR, IntPtr.Zero, (IntPtr)DARK_COLOR_INT);
                        User32.SendMessage(hWnd, LVM_SETTEXTBKCOLOR, IntPtr.Zero, (IntPtr)DARK_COLOR_INT);
                        User32.SendMessage(hWnd, LVM_SETTEXTCOLOR, IntPtr.Zero, (IntPtr)COLOR_WHITE);
                    }
                }
                else if (uMsg == (uint)User32.WindowsMessage.ThemeChanged)
                {
                    _colorizedLists.Remove(hWnd);
                }
            }

            return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
        }

        private IntPtr DropdownSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData)
        {
            if (Program.Style.DarkMode)
            {
                if (uMsg == (uint)User32.WindowsMessage.DrawItem && lParam != IntPtr.Zero) return DrawDropdownItem(lParam);

                if (TryHandleColorMessage(uMsg, wParam, out IntPtr brush)) return brush;

                if (TryEraseBackground(hWnd, uMsg, wParam)) return (IntPtr)1;

                if (uMsg == (uint)User32.WindowsMessage.ShowWindow || uMsg == (uint)User32.WindowsMessage.WindowPosChanged)
                {
                    bool anyNew = false;

                    foreach (IntPtr child in User32.GetChildWindowHandles(hWnd))
                    {
                        if (_themedControls.Contains(child)) continue;

                        ApplyDarkModeToControl(new Win32Control(child));
                        anyNew = true;
                    }

                    if (anyNew) Redraw(hWnd);
                }
            }

            return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
        }

        private IntPtr DrawDropdownItem(IntPtr lParam)
        {
            var dis = Marshal.PtrToStructure<User32.DRAWITEMSTRUCT>(lParam);

            if (dis.hDC == IntPtr.Zero || dis.hwndItem == IntPtr.Zero || dis.itemID == uint.MaxValue) return (IntPtr)1;

            bool selected = (dis.itemState & ODS_SELECTED) != 0;

            var fillRect = dis.rcItem;
            fillRect.right -= 1;
            User32.FillRect(dis.hDC, ref fillRect, selected ? SelectionBrush : DarkBrush);

            if (new Win32Control(dis.hwndItem).Type == Win32Control.ControlType.ListView)
            {
                string itemText = GetListViewItemText(dis.hwndItem, (int)dis.itemID);

                if (!string.IsNullOrEmpty(itemText))
                {
                    GDI32.SetTextColor(dis.hDC, COLOR_WHITE);
                    GDI32.SetBkMode(dis.hDC, GDI32.TRANSPARENT);

                    IntPtr hOldFont = SelectControlFont(dis.hDC, dis.hwndItem);

                    var rect = dis.rcItem;
                    rect.left += 6;
                    rect.right -= 7;
                    DrawLabel(dis.hDC, itemText, ref rect);

                    RestoreFont(dis.hDC, hOldFont);
                }
            }

            if ((dis.itemState & ODS_FOCUS) != 0)
            {
                var focusRect = dis.rcItem;
                focusRect.right -= 1;
                User32.DrawFocusRect(dis.hDC, ref focusRect);
            }

            return (IntPtr)1;
        }

        private string GetListViewItemText(IntPtr listView, int index)
        {
            IntPtr textBuf = TextBuffer;
            Marshal.WriteInt16(textBuf, 0, 0);

            User32.LVITEM lvItem = new()
            {
                mask = 0x0001,
                iItem = index,
                iSubItem = 0,
                cchTextMax = MAX_ITEM_TEXT,
                pszText = textBuf
            };

            IntPtr ptrLvItem = LvItemBuffer;
            Marshal.StructureToPtr(lvItem, ptrLvItem, false);

            int len = (int)User32.SendMessage(listView, LVM_GETITEMTEXTW, (IntPtr)index, ptrLvItem);
            return len > 0 ? Marshal.PtrToStringUni(textBuf, Math.Min(len, MAX_ITEM_TEXT)) : null;
        }

        internal void ApplyDarkModeToControl(Win32Control ctrl)
        {
            if (!Program.Style.DarkMode) return;
            if (IsLegacyOS) return;
            if (ctrl is null || ctrl.Handle == IntPtr.Zero) return;

            // Theming is idempotent: once per window is enough.
            if (!_themedControls.Add(ctrl.Handle)) return;

            NativeMethods.Helpers.SetHWNDDarkMode(ctrl.Handle, Program.Style.DarkMode);

            switch (ctrl.Type)
            {
                case Win32Control.ControlType.ListView:
                case Win32Control.ControlType.ListBox:
                case Win32Control.ControlType.ComboBox:
                    SubclassWindow(ctrl.Handle, _listViewProc, SUBCLASS_ID_LISTVIEW);
                    UxTheme.SetWindowTheme(ctrl.Handle, "Explorer", null);
                    UxTheme.SetWindowTheme(ctrl.Handle, "DarkMode_Explorer", null);
                    ctrl.RemoveExtendedStyle(Win32Control.ControlExtendedStyles.ClientEdge);
                    break;

                default:
                    UxTheme.SetWindowTheme(ctrl.Handle, "DarkMode_Explorer", null);
                    break;
            }

            Redraw(ctrl.Handle);
        }

        private void ApplyDarkModeToAutoSuggestDropdown(IntPtr hWnd)
        {
            if (!Program.Style.DarkMode) return;
            if (IsLegacyOS) return;
            if (hWnd == IntPtr.Zero) return;

            NativeMethods.Helpers.SetHWNDDarkMode(hWnd, true);
            UxTheme.SetWindowTheme(hWnd, "DarkMode_Explorer", null);
            SubclassWindow(hWnd, _dropdownProc, SUBCLASS_ID_DROPDOWN);

            foreach (IntPtr child in User32.GetChildWindowHandles(hWnd)) ApplyDarkModeToControl(new Win32Control(child));

            Redraw(hWnd);
        }

        #endregion

        #region Font dialog: detection

        private void HandleFontDialogWindow(IntPtr hWnd)
        {
            if (!Program.Style.DarkMode) return;

            switch (GetClassName(hWnd))
            {
                case "ComboLBox":
                case "ListBox":
                    ThemeComboPopup(hWnd);
                    break;

                case "#32770":
                    // Only anchor on the dialog once; HCBT_ACTIVATE fires repeatedly while it lives.
                    if (hWnd != _fontDialogHwnd && User32.GetDlgItem(hWnd, FONTDLG_ID_LIST_FONT) != IntPtr.Zero)
                    {
                        ApplyFontDialogDarkMode(hWnd);
                    }
                    break;
            }
        }

        /// <summary>
        /// Is <paramref name="popup"/> a dropdown that opens right below <paramref name="combo"/>? (Win11 popup relationship)
        /// </summary>
        private static bool IsPopupBelowCombo(IntPtr popup, IntPtr combo)
        {
            if (popup == IntPtr.Zero || combo == IntPtr.Zero) return false;
            if (!User32.GetWindowRect(popup, out var p) || !User32.GetWindowRect(combo, out var c)) return false;

            int popupWidth = p.right - p.left;
            int comboWidth = c.right - c.left;

            bool horizontalOverlap = p.left < c.right && p.right > c.left;

            // The dropdown starts at (or very near) the bottom of the combo; allow a few px for border/shadow.
            bool belowCombo = p.top >= c.bottom - 8 && p.top <= c.bottom + 32;

            // Don't match unrelated popups that merely overlap: ComboLBox width is close to the combo's.
            bool widthMatches = popupWidth >= comboWidth - 16 && popupWidth <= comboWidth + 64;

            return horizontalOverlap && belowCombo && widthMatches;
        }

        private ComboPopupKind ClassifyComboPopup(IntPtr hWnd)
        {
            if (_fontDialogHwnd == IntPtr.Zero) return ComboPopupKind.None;

            IntPtr parent = User32.GetParent(hWnd);
            if (parent == IntPtr.Zero) return ComboPopupKind.None;

            // Normal case: the ComboLBox is a child of the script ComboBox.
            if (parent == _scriptComboHwnd) return ComboPopupKind.ScriptCombo;

            string parentClass = GetClassName(parent);

            if (parentClass == "ComboBox") return User32.GetParent(parent) == _fontDialogHwnd ? ComboPopupKind.Combo : ComboPopupKind.None;

            // Win11 can create the popup parented to a message window; identify it by geometry.
            if (parentClass == "Message" && IsPopupBelowCombo(hWnd, _scriptComboHwnd)) return ComboPopupKind.ScriptCombo;

            return ComboPopupKind.None;
        }

        private void ThemeComboPopup(IntPtr hWnd)
        {
            switch (ClassifyComboPopup(hWnd))
            {
                case ComboPopupKind.ScriptCombo:
                    ThemeScriptDropdown(hWnd);
                    break;

                case ComboPopupKind.Combo:
                    ThemeList(hWnd, _fontListProc, SUBCLASS_ID_FONTLIST);
                    Redraw(hWnd);
                    break;
            }
        }

        private void ThemeList(IntPtr listHwnd, Comctl32.SUBCLASSPROC proc, int id)
        {
            SetDarkTheme(listHwnd, "DarkMode_Explorer", resetFirst: true);
            SubclassWindow(listHwnd, proc, id);
        }

        private void ThemeScriptDropdown(IntPtr listHwnd)
        {
            if (listHwnd == IntPtr.Zero || listHwnd == _scriptListHwnd) return;

            _scriptListHwnd = listHwnd;

            ThemeList(listHwnd, _scriptListProc, SUBCLASS_ID_SCRIPTLIST);
            RedrawFull(listHwnd);

            Log($"[DarkWin32] Script dropdown themed hWnd=0x{listHwnd:X}");
        }

        /// <summary>
        /// Cheap check used from paint/show paths: only queries the combo when no list is themed yet.
        /// </summary>
        private void EnsureScriptDropdownThemed()
        {
            if (_scriptListHwnd == IntPtr.Zero) ThemeScriptDropdown(GetComboListHwnd(_scriptComboHwnd));
        }

        /// <summary>
        /// Gets the dropdown list HWND of any combo, including popup (non-child) ComboLBox.
        /// </summary>
        private IntPtr GetComboListHwnd(IntPtr combo)
        {
            if (combo == IntPtr.Zero) return IntPtr.Zero;

            IntPtr buf = ComboInfoBuffer;
            Marshal.WriteInt32(buf, 0, ComboInfoSize); // cbSize

            if (User32.SendMessage(combo, CB_GETCOMBOBOXINFO, IntPtr.Zero, buf) == IntPtr.Zero) return IntPtr.Zero;

            return Marshal.PtrToStructure<COMBOBOXINFO_LOCAL>(buf).hwndList;
        }

        #endregion

        #region Font dialog: entry point

        private void ApplyFontDialogDarkMode(IntPtr hWnd)
        {
            if (!Program.Style.DarkMode || IsLegacyOS || hWnd == IntPtr.Zero) return;

            ResetFontDialogState();
            _fontDialogHwnd = hWnd;

            Log($"[DarkWin32] Font dialog ApplyDarkMode start hWnd=0x{hWnd:X}");

            NativeMethods.Helpers.SetHWNDDarkMode(hWnd, true);
            SubclassWindow(hWnd, _fontDialogProc, SUBCLASS_ID_FONTDLG);

            // The three "list" combos (handled by ID).
            foreach (int id in FontListComboIds)
            {
                IntPtr combo = User32.GetDlgItem(hWnd, id);
                if (combo == IntPtr.Zero) continue;

                switch (id)
                {
                    case FONTDLG_ID_LIST_FONT: _fontComboHwnd = combo; break;
                    case FONTDLG_ID_LIST_STYLE: _styleComboHwnd = combo; break;
                    case FONTDLG_ID_LIST_SIZE: _sizeComboHwnd = combo; break;
                }

                SetDarkTheme(combo, "DarkMode_CFD", resetFirst: true);
                SubclassWindow(combo, _fontListProc, SUBCLASS_ID_FONTLIST);
            }

            // Sample preview.
            IntPtr sample = User32.GetDlgItem(hWnd, FONTDLG_ID_SAMPLE);
            if (sample == IntPtr.Zero)
            {
                sample = FindSampleStatic(hWnd);
                if (sample != IntPtr.Zero) CacheSampleText(sample);
            }
            else if (string.IsNullOrEmpty(_sampleText))
            {
                CacheSampleText(sample);
            }

            if (sample != IntPtr.Zero)
            {
                _sampleHwnd = sample;
                SubclassWindow(sample, _fontSampleProc, SUBCLASS_ID_FONTSAMPLE);
            }

            // Strikeout / underline checkboxes.
            foreach (int id in FontCheckBoxIds)
            {
                IntPtr chk = User32.GetDlgItem(hWnd, id);
                if (chk != IntPtr.Zero) SetDarkTheme(chk, "DarkMode_Explorer", resetFirst: false);
            }

            // Remaining combos (color, script) and group boxes: a single child enumeration.
            User32.EnumChildWindows(hWnd, EnumFontDialogChild, IntPtr.Zero);

            Redraw(hWnd);

            Log($"[DarkWin32] Font dialog ApplyDarkMode done hWnd=0x{hWnd:X}");
        }

        private void ResetFontDialogState()
        {
            _fontDialogHwnd = IntPtr.Zero;
            _sampleHwnd = IntPtr.Zero;
            _fontComboHwnd = IntPtr.Zero;
            _styleComboHwnd = IntPtr.Zero;
            _sizeComboHwnd = IntPtr.Zero;
            _scriptComboHwnd = IntPtr.Zero;
            _scriptListHwnd = IntPtr.Zero;
            _sampleText = FallbackPreviewText;
            _itemFontMetricsRead = false;
            _dpiY = 0;

            _themedFontChildren.Clear();
            _captionCache.Clear();
            ClearFontCache();
        }

        #endregion

        #region Font dialog: enumeration

        private bool EnumFontDialogChild(IntPtr child, IntPtr lParam)
        {
            if (child == IntPtr.Zero) return true;

            string cls = GetClassName(child);

            if (cls == "ComboBox") ThemeFontDialogCombo(child);
            else if (IsGroupBox(child, cls)) ThemeGroupBox(child);

            return true;
        }

        private void ThemeGroupBox(IntPtr groupBox)
        {
            SetDarkTheme(groupBox, "DarkMode_Explorer", resetFirst: false);
            SubclassWindow(groupBox, _fontGroupBoxProc, SUBCLASS_ID_FONTGROUPBOX);
        }

        /// <summary>
        /// Themes the combos not handled by ID: the color combo and the (unstable ID) script combo.
        /// </summary>
        private void ThemeFontDialogCombo(IntPtr combo)
        {
            // Font / Style / Size are already handled explicitly by ID.
            if (combo == _fontComboHwnd || combo == _styleComboHwnd || combo == _sizeComboHwnd) return;

            // 1139 (color) is the only stable ID left; anything else is the script combo.
            bool isScript = User32.GetDlgCtrlID(combo) != FONTDLG_ID_COMBO_COLOR;

            SetDarkTheme(combo, "DarkMode_CFD", resetFirst: true);

            if (isScript)
            {
                _scriptComboHwnd = combo;
                SubclassWindow(combo, _scriptComboProc, SUBCLASS_ID_SCRIPTCOMBO);
                ThemeScriptDropdown(GetComboListHwnd(combo));
            }
            else
            {
                // Only the color combo needs the manual closed-state redraw path.
                SubclassWindow(combo, _fontComboProc, SUBCLASS_ID_FONTCOMBO);
            }

            foreach (IntPtr child in User32.GetChildWindowHandles(combo))
            {
                if (child == IntPtr.Zero) continue;

                NativeMethods.Helpers.SetHWNDDarkMode(child, true);

                switch (GetClassName(child))
                {
                    case "Edit":
                        UxTheme.SetWindowTheme(child, "DarkMode_Explorer", null);
                        break;

                    case "Static":
                        UxTheme.SetWindowTheme(child, "DarkMode_Explorer", null);
                        if (!isScript) SubclassWindow(child, _fontGroupBoxProc, SUBCLASS_ID_FONTGROUPBOX);
                        break;

                    case "ComboLBox":
                    case "ListBox":
                        if (isScript) ThemeScriptDropdown(child);
                        else ThemeList(child, _fontListProc, SUBCLASS_ID_FONTLIST);
                        break;
                }
            }
        }

        private static bool IsGroupBox(IntPtr hWnd, string cls)
        {
            if (cls != "Button") return false;

            int style = (int)User32.GetWindowLong(hWnd, GWL_STYLE);
            return (style & 0x0000000F) == BS_GROUPBOX;
        }

        private IntPtr FindSampleStatic(IntPtr dialog)
        {
            IntPtr found = IntPtr.Zero;

            User32.EnumChildWindows(dialog, (child, _) =>
            {
                if (child == IntPtr.Zero || GetClassName(child) != "Static") return true;

                int type = (int)User32.GetWindowLong(child, GWL_STYLE) & SS_TYPEMASK;
                User32.GetClientRect(child, out var rc);

                bool legacySample = (type == SS_OWNERDRAW || type == SS_BLACKFRAME || type == SS_WHITEFRAME) && rc.right > 40 && rc.bottom > 20;
                bool modernSample = rc.right >= SAMPLE_MIN_WIDTH && rc.bottom >= SAMPLE_MIN_HEIGHT && User32.GetWindowTextLength(child) > 0;

                if (!legacySample && !modernSample) return true;

                found = child;
                return false;
            }, IntPtr.Zero);

            return found;
        }

        private void CacheSampleText(IntPtr sampleHwnd)
        {
            string text = GetWindowTextSafe(sampleHwnd);
            if (text.Length > 0) _sampleText = text;
        }

        #endregion

        #region Font dialog: subclass procs

        private IntPtr FontDialogSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData)
        {
            if (Program.Style.DarkMode)
            {
                if (uMsg == (uint)User32.WindowsMessage.Command)
                {
                    if (_scriptComboHwnd != IntPtr.Zero && lParam == _scriptComboHwnd && HiWord(wParam) == CBN_DROPDOWN)
                    {
                        ThemeScriptDropdown(GetComboListHwnd(_scriptComboHwnd));
                    }
                }
                else if (uMsg == (uint)User32.WindowsMessage.DrawItem && lParam != IntPtr.Zero)
                {
                    var dis = Marshal.PtrToStructure<User32.DRAWITEMSTRUCT>(lParam);

                    if (dis.CtlID == FONTDLG_ID_SAMPLE || (_sampleHwnd != IntPtr.Zero && dis.hwndItem == _sampleHwnd))
                    {
                        User32.FillRect(dis.hDC, ref dis.rcItem, DarkBrush);
                        GDI32.SetTextColor(dis.hDC, COLOR_WHITE);
                        GDI32.SetBkMode(dis.hDC, GDI32.TRANSPARENT);
                        return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
                    }

                    if (IsFontListControl(dis.hwndItem)) return DrawFontListItem(ref dis);
                }

                if (TryHandleColorMessage(uMsg, wParam, out IntPtr brush)) return brush;

                if (TryEraseBackground(hWnd, uMsg, wParam)) return (IntPtr)1;
            }

            return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
        }

        /// <summary>
        /// True for the Font/Style/Size combo or its ComboLBox.
        /// </summary>
        private bool IsFontListControl(IntPtr hwndItem)
        {
            if (hwndItem == IntPtr.Zero) return false;

            IntPtr parent = User32.GetParent(hwndItem);

            return hwndItem == _fontComboHwnd || parent == _fontComboHwnd ||
                   hwndItem == _styleComboHwnd || parent == _styleComboHwnd ||
                   hwndItem == _sizeComboHwnd || parent == _sizeComboHwnd;
        }

        private IntPtr FontListSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData)
        {
            if (Program.Style.DarkMode)
            {
                if (TryHandleColorMessage(uMsg, wParam, out IntPtr brush)) return brush;

                if (TryEraseBackground(hWnd, uMsg, wParam)) return (IntPtr)1;

                if (uMsg == (uint)User32.WindowsMessage.DrawItem && lParam != IntPtr.Zero)
                {
                    var dis = Marshal.PtrToStructure<User32.DRAWITEMSTRUCT>(lParam);
                    return DrawFontListItem(ref dis);
                }

                if (uMsg == (uint)User32.WindowsMessage.Paint)
                {
                    User32.SendMessage(hWnd, LB_SETBKCOLOR, IntPtr.Zero, (IntPtr)DARK_COLOR_INT);
                }
            }

            return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
        }

        private IntPtr FontSampleSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData)
        {
            if (!Program.Style.DarkMode) return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);

            if (TryEraseBackground(hWnd, uMsg, wParam)) return (IntPtr)1;

            if (uMsg == (uint)User32.WindowsMessage.Paint)
            {
                IntPtr result = Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);

                IntPtr hdc = User32.GetDC(hWnd);
                if (hdc == IntPtr.Zero) return result;

                try
                {
                    User32.GetClientRect(hWnd, out var rc);
                    DrawSamplePreview(hdc, ref rc);
                }
                finally
                {
                    User32.ReleaseDC(hWnd, hdc);
                }

                return result;
            }

            return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
        }

        /// <summary>
        /// Color combo: manual closed-state redraw + theming of the children it creates.
        /// </summary>
        private IntPtr FontComboSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData)
        {
            if (!Program.Style.DarkMode) return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);

            if (TryHandleColorMessage(uMsg, wParam, out IntPtr brush)) return brush;

            if (TryEraseBackground(hWnd, uMsg, wParam)) return (IntPtr)1;

            if (uMsg == (uint)User32.WindowsMessage.Paint)
            {
                IntPtr result = Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
                RedrawClosedComboState(hWnd);
                return result;
            }

            if (uMsg == (uint)User32.WindowsMessage.ShowWindow || uMsg == (uint)User32.WindowsMessage.WindowPosChanged)
            {
                bool anyNew = false;

                foreach (IntPtr child in User32.GetChildWindowHandles(hWnd))
                {
                    if (child == IntPtr.Zero || !_themedFontChildren.Add(child)) continue;

                    anyNew = true;

                    string cls = GetClassName(child);
                    NativeMethods.Helpers.SetHWNDDarkMode(child, true);

                    if (cls == "ComboLBox" || cls == "ListBox") ThemeList(child, _fontListProc, SUBCLASS_ID_FONTLIST);
                    else ApplyDarkModeToControl(new Win32Control(child));
                }

                if (anyNew) Redraw(hWnd);
            }

            return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
        }

        /// <summary>
        /// Script combo: native comctl32 painting for the closed state. We only answer the WM_CTLCOLOR*
        /// messages and re-theme the popup list, which may be recreated every time the dropdown opens.
        /// </summary>
        private IntPtr ScriptComboSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData)
        {
            if (!Program.Style.DarkMode) return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);

            if (TryHandleColorMessage(uMsg, wParam, out IntPtr brush)) return brush;

            if (TryEraseBackground(hWnd, uMsg, wParam)) return (IntPtr)1;

            if (uMsg == (uint)User32.WindowsMessage.Paint)
            {
                IntPtr result = Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);

                RedrawClosedComboState(hWnd);
                EnsureScriptDropdownThemed();

                return result;
            }

            if (uMsg == (uint)User32.WindowsMessage.ShowWindow ||
                uMsg == (uint)User32.WindowsMessage.WindowPosChanged ||
                uMsg == (uint)User32.WindowsMessage.WindowPosChanging)
            {
                EnsureScriptDropdownThemed();
            }

            return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
        }

        /// <summary>
        /// Script combo's popup ComboLBox: fully self-painted. LBS_OWNERDRAWFIXED can't be toggled after
        /// creation (the parent is a CBS_DROPDOWNLIST combo that never forwards WM_DRAWITEM to the dialog),
        /// and the native listbox paints highlight changes via GetDC in light colors, bypassing WM_PAINT.
        /// So after the native control handles any highlight-changing message, we repaint the list dark.
        /// </summary>
        private IntPtr ScriptListSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData)
        {
            if (!Program.Style.DarkMode) return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);

            switch (uMsg)
            {
                case (uint)User32.WindowsMessage.EraseBkgnd:
                    return (IntPtr)1; // everything is painted in WM_PAINT; erasing here only adds flicker

                case (uint)User32.WindowsMessage.Paint:
                    {
                        IntPtr hdc = User32.BeginPaint(hWnd, out User32.PAINTSTRUCT ps);
                        if (hdc == IntPtr.Zero) return IntPtr.Zero;

                        try { DrawScriptListBuffered(hWnd, hdc); }
                        finally { User32.EndPaint(hWnd, ref ps); }

                        return IntPtr.Zero;
                    }

                case (uint)User32.WindowsMessage.PrintClient:
                    if (wParam != IntPtr.Zero)
                    {
                        DrawScriptListBuffered(hWnd, wParam);
                        return IntPtr.Zero;
                    }
                    break;

                case (uint)User32.WindowsMessage.ThemeChanged:
                case (uint)User32.WindowsMessage.SetFont:
                    {
                        IntPtr r = Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
                        Redraw(hWnd);
                        return r;
                    }

                case (uint)User32.WindowsMessage.MouseMove:
                case (uint)User32.WindowsMessage.MouseLeave:
                case (uint)User32.WindowsMessage.LButtonDown:
                case (uint)User32.WindowsMessage.LButtonUp:
                case (uint)User32.WindowsMessage.KeyDown:
                case (uint)User32.WindowsMessage.MouseWheel:
                case (uint)User32.WindowsMessage.VScroll:
                case (uint)User32.WindowsMessage.Timer:
                case LB_SETCURSEL:
                case LB_SETCARETINDEX:
                case LB_SETTOPINDEX:
                    {
                        int selBefore = (int)User32.SendMessage(hWnd, LB_GETCURSEL, IntPtr.Zero, IntPtr.Zero);
                        int topBefore = (int)User32.SendMessage(hWnd, LB_GETTOPINDEX, IntPtr.Zero, IntPtr.Zero);

                        // Stop the native control from painting its light highlight while it handles the message.
                        User32.SendMessage(hWnd, (uint)User32.WindowsMessage.SetRedraw, IntPtr.Zero, IntPtr.Zero);

                        IntPtr res;
                        try { res = Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam); }
                        finally { User32.SendMessage(hWnd, (uint)User32.WindowsMessage.SetRedraw, (IntPtr)1, IntPtr.Zero); }

                        int selAfter = (int)User32.SendMessage(hWnd, LB_GETCURSEL, IntPtr.Zero, IntPtr.Zero);
                        int topAfter = (int)User32.SendMessage(hWnd, LB_GETTOPINDEX, IntPtr.Zero, IntPtr.Zero);

                        // Hovering over the same item changes nothing: skip the repaint.
                        bool changed = selAfter != selBefore || topAfter != topBefore;
                        bool alwaysRepaint = uMsg != (uint)User32.WindowsMessage.MouseMove && uMsg != (uint)User32.WindowsMessage.MouseLeave;

                        if (changed || alwaysRepaint)
                        {
                            IntPtr hdc = User32.GetDC(hWnd);
                            if (hdc != IntPtr.Zero)
                            {
                                try { DrawScriptListBuffered(hWnd, hdc); }
                                finally { User32.ReleaseDC(hWnd, hdc); }
                            }
                        }

                        return res;
                    }
            }

            return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
        }

        /// <summary>
        /// Group boxes ("Font:", "Style:", "Effects:", "Sample:" frames), also reused for the Static children of the color combo.
        /// <br></br>
        /// <br></br> Caption: we measure the text and fill only that rect (plus padding), so the group box's top border line stays visible on both sides of the caption.
        /// <br></br>
        /// <br></br> Sample preview: comdlg32 draws the preview from the "Sample" group box's paint pass, so we repaint the sample area here.
        /// </summary>
        private IntPtr FontGroupBoxSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData)
        {
            if (!Program.Style.DarkMode) return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);

            if (TryHandleColorMessage(uMsg, wParam, out IntPtr brush)) return brush;

            if (TryEraseBackground(hWnd, uMsg, wParam)) return (IntPtr)1;

            if (uMsg != (uint)User32.WindowsMessage.Paint) return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);

            IntPtr result = Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);

            IntPtr hdc = User32.GetDC(hWnd);
            if (hdc == IntPtr.Zero) return result;

            try
            {
                bool isGroupBox = IsGroupBox(hWnd, GetClassName(hWnd));

                PaintCaption(hdc, hWnd, isGroupBox);

                if (isGroupBox && _sampleHwnd != IntPtr.Zero) RepaintSamplePreviewIn(hdc, hWnd);
            }
            finally
            {
                User32.ReleaseDC(hWnd, hdc);
            }

            return result;
        }

        private void PaintCaption(IntPtr hdc, IntPtr hWnd, bool isGroupBox)
        {
            string caption;

            if (isGroupBox)
            {
                // Group box captions never change: read them once per window.
                if (!_captionCache.TryGetValue(hWnd, out caption)) _captionCache[hWnd] = caption = GetWindowTextSafe(hWnd);
            }
            else
            {
                // The combo's Static child shows the current selection and does change.
                caption = GetWindowTextSafe(hWnd);
            }

            if (caption.Length == 0) return;

            User32.GetClientRect(hWnd, out var rc);

            IntPtr hOld = SelectControlFont(hdc, hWnd);

            RECT captionRect;

            if (isGroupBox)
            {
                if (!GDI32.GetTextExtentPoint32(hdc, caption, caption.Length, out UxTheme.SIZE sz)) sz.cx = caption.Length * 7; // conservative fallback

                captionRect = new()
                {
                    left = rc.left + CAPTION_PAD_LEFT,
                    top = rc.top,
                    right = rc.left + CAPTION_PAD_LEFT + sz.cx + CAPTION_PAD_RIGHT,
                    bottom = rc.top + 16,
                };
            }
            else
            {
                captionRect = rc; // combo Static child: fill the whole caption area
            }

            User32.FillRect(hdc, ref captionRect, DarkBrush);

            GDI32.SetTextColor(hdc, COLOR_WHITE);
            GDI32.SetBkMode(hdc, GDI32.TRANSPARENT);

            DrawLabel(hdc, caption, ref captionRect);

            RestoreFont(hdc, hOld);
        }

        private void RepaintSamplePreviewIn(IntPtr hdc, IntPtr groupBox)
        {
            if (!User32.GetWindowRect(groupBox, out var gb) || !User32.GetWindowRect(_sampleHwnd, out var sm)) return;

            bool intersects = sm.left < gb.right && sm.right > gb.left && sm.top < gb.bottom && sm.bottom > gb.top;
            if (!intersects) return;

            User32.POINT pt1 = new() { X = sm.left, Y = sm.top };
            User32.POINT pt2 = new() { X = sm.right, Y = sm.bottom };
            User32.ScreenToClient(groupBox, ref pt1);
            User32.ScreenToClient(groupBox, ref pt2);

            RECT sampleRc = new() { left = pt1.X, top = pt1.Y, right = pt2.X, bottom = pt2.Y };

            DrawSamplePreview(hdc, ref sampleRc);
        }

        #endregion

        #region Font dialog: drawing

        /// <summary>
        /// Draws one item of the Font / Style / Size list.
        ///  <br></br>- Font  (1136): each item in its own font face.
        ///  <br></br>- Style (1137): each item in its own weight / italic.
        ///  <br></br>- Size  (1138): plain text.
        /// </summary>
        private IntPtr DrawFontListItem(ref User32.DRAWITEMSTRUCT dis)
        {
            if (dis.hDC == IntPtr.Zero || dis.hwndItem == IntPtr.Zero || dis.itemID == uint.MaxValue) return (IntPtr)1;

            bool selected = (dis.itemState & ODS_SELECTED) != 0;
            bool disabled = (dis.itemState & ODS_DISABLED) != 0;

            User32.FillRect(dis.hDC, ref dis.rcItem, selected ? SelectionBrush : DarkBrush);

            string text = GetItemText(dis.hwndItem, LB_GETTEXT, (int)dis.itemID);
            if (text is null) return (IntPtr)1;

            // Cached HFONT (owned by the cache; never deleted here).
            IntPtr itemFont = GetItemFont(text, dis.hwndItem);

            IntPtr hOld = itemFont != IntPtr.Zero ? GDI32.SelectObject(dis.hDC, itemFont) : SelectControlFont(dis.hDC, dis.hwndItem);

            GDI32.SetTextColor(dis.hDC, disabled ? COLOR_DISABLED_TEXT : COLOR_WHITE);
            GDI32.SetBkMode(dis.hDC, GDI32.TRANSPARENT);

            RECT rc = dis.rcItem;
            rc.left += 4;
            rc.right -= 4;
            DrawLabel(dis.hDC, text, ref rc);

            RestoreFont(dis.hDC, hOld);

            if (selected && (dis.itemState & ODS_FOCUS) != 0) User32.DrawFocusRect(dis.hDC, ref dis.rcItem);

            return (IntPtr)1;
        }

        private FontListKind GetFontListKind(IntPtr listbox)
        {
            if (listbox == IntPtr.Zero) return FontListKind.Unknown;

            IntPtr combo = User32.GetParent(listbox);
            if (combo == IntPtr.Zero) return FontListKind.Unknown;

            if (combo == _fontComboHwnd) return FontListKind.Font;
            if (combo == _styleComboHwnd) return FontListKind.Style;
            if (combo == _sizeComboHwnd) return FontListKind.Size;

            // Some builds hand us a different list window: fall back to the parent's control ID.
            return User32.GetDlgCtrlID(combo) switch
            {
                FONTDLG_ID_LIST_FONT => FontListKind.Font,
                FONTDLG_ID_LIST_STYLE => FontListKind.Style,
                FONTDLG_ID_LIST_SIZE => FontListKind.Size,
                _ => FontListKind.Unknown
            };
        }

        /// <summary>
        /// Returns the (cached) HFONT used to draw a given list item, or Zero to use the control's font.
        /// </summary>
        private IntPtr GetItemFont(string text, IntPtr listbox)
        {
            if (string.IsNullOrEmpty(text)) return IntPtr.Zero;

            switch (GetFontListKind(listbox))
            {
                case FontListKind.Font:
                    // EnumFontFamilies already gives the full face name.
                    return GetItemSizedFont(listbox, text, 400, false);

                case FontListKind.Style:
                    {
                        string family = GetComboSelectionText(_fontComboHwnd);
                        var (face, weight, italic) = ResolveGdiFace(family, text);
                        return GetItemSizedFont(listbox, face, weight, italic);
                    }

                default:
                    return IntPtr.Zero;
            }
        }

        /// <summary>
        /// Uses the listbox's own font height so item text fits the row. A real, non-zero lfHeight is what makes Windows honour lfFaceName (with lfHeight == 0 the mapper substitutes 
        /// a default face). The metrics are read once per dialog instead of once per item.
        /// </summary>
        private IntPtr GetItemSizedFont(IntPtr listbox, string face, int weight, bool italic)
        {
            if (!_itemFontMetricsRead)
            {
                IntPtr currentFont = User32.SendMessage(listbox, (uint)User32.WindowsMessage.GetFont, IntPtr.Zero, IntPtr.Zero);

                if (currentFont != IntPtr.Zero)
                {
                    GDI32.LOGFONT existing = new();

                    if (GDI32.GetObjectFont(currentFont, Marshal.SizeOf<GDI32.LOGFONT>(), existing) != 0)
                    {
                        if (existing.lfHeight != 0) _itemFontHeight = existing.lfHeight - 2;
                        _itemFontWidth = existing.lfWidth;
                    }

                    _itemFontMetricsRead = true;
                }
            }

            return GetCachedFont(face, weight, italic, _itemFontHeight, _itemFontWidth);
        }

        private IntPtr GetCachedFont(string face, int weight, bool italic, int height, int width)
        {
            face ??= string.Empty;
            if (face.Length > MAX_FACE_NAME) face = face.Substring(0, MAX_FACE_NAME);

            var key = (face, weight, italic, height, width);
            if (_fontCache.TryGetValue(key, out IntPtr cached)) return cached;

            // Safety valve: nothing is selected into a DC at lookup time, so clearing here is safe.
            if (_fontCache.Count >= MAX_CACHED_FONTS) ClearFontCache();

            GDI32.LOGFONT lf = new()
            {
                lfHeight = height,
                lfWidth = width,
                lfWeight = weight,
                lfItalic = (byte)(italic ? 1 : 0),
                lfCharSet = 1,
                lfOutPrecision = 7,
                lfQuality = 5,
                lfFaceName = face,
            };

            IntPtr hFont = GDI32.CreateFontIndirect(lf);
            if (hFont != IntPtr.Zero) _fontCache[key] = hFont;

            return hFont;
        }

        private void ClearFontCache()
        {
            foreach (IntPtr hFont in _fontCache.Values) GDI32.DeleteObject(hFont);

            _fontCache.Clear();
        }

        /// <summary>
        /// Maps (family, style) to the GDI face name + lfWeight that actually renders it.
        /// <br></br>Light/Semilight/Semibold/... are separate GDI families ("Segoe UI Light"), each "Regular" (400) inside itself, so they are NOT synthesized by changing lfWeight on the base family:
        /// <br></br>  Segoe UI + Regular / Italic / Bold / Bold Italic -> base family, weight 400/700, italic flag
        /// <br></br>  Segoe UI + Light / Semilight / Light Italic ...  -> "Segoe UI Light" / "Segoe UI Light Italic", 400
        /// </summary>
        private (string Face, int Weight, bool Italic) ResolveGdiFace(string family, string style)
        {
            if (string.IsNullOrWhiteSpace(family))
            {
                ParseStyleString(style, out int w, out bool it);
                return (family ?? string.Empty, w, it);
            }

            style = string.IsNullOrWhiteSpace(style) ? "Regular" : style.Trim();

            var key = (family, style);
            if (_faceCache.TryGetValue(key, out var cached)) return cached;

            string normalized = WhitespaceRegex.Replace(style, " ");

            bool italic = normalized.IndexOf("italic", StringComparison.OrdinalIgnoreCase) >= 0 || normalized.IndexOf("oblique", StringComparison.OrdinalIgnoreCase) >= 0;

            // Strip italic/oblique to get the weight/style name.
            string variant = WhitespaceRegex.Replace(ItalicWordRegex.Replace(normalized, string.Empty), " ").Trim();

            (string Face, int Weight, bool Italic) result;

            if (variant.Length == 0 || variant.Equals("regular", StringComparison.OrdinalIgnoreCase)) result = (family, 400, italic);
            else if (variant.Equals("bold", StringComparison.OrdinalIgnoreCase)) result = (family, 700, italic);
            else result = (family + " " + variant + (italic ? " Italic" : string.Empty), 400, false); // named variant = its own GDI face

            _faceCache[key] = result;
            return result;
        }

        /// <summary>
        /// Parses a style string ("Regular", "Bold Italic", "SemiBold", ...) into weight + italic.
        /// <br></br>Order matters: "semibold"/"extrabold" must be tested before "bold".
        /// </summary>
        private static void ParseStyleString(string style, out int weight, out bool italic)
        {
            weight = 400;
            italic = false;

            if (string.IsNullOrEmpty(style)) return;

            string s = style.ToLowerInvariant();

            italic = s.Contains("italic") || s.Contains("oblique");

            if (s.Contains("thin") || s.Contains("hairline")) weight = 100;
            else if (s.Contains("extralight") || s.Contains("ultralight") || s.Contains("extra light") || s.Contains("ultra light")) weight = 200;
            else if (s.Contains("semilight") || s.Contains("demilight") || s.Contains("semi light") || s.Contains("demi light")) weight = 350;
            else if (s.Contains("light")) weight = 300;
            else if (s.Contains("medium")) weight = 500;
            else if (s.Contains("semibold") || s.Contains("demibold") || s.Contains("semi bold") || s.Contains("demi bold")) weight = 600;
            else if (s.Contains("extrabold") || s.Contains("ultrabold") || s.Contains("extra bold") || s.Contains("ultra bold")) weight = 800;
            else if (s.Contains("bold")) weight = 700;
            else if (s.Contains("black") || s.Contains("heavy")) weight = 900;
        }

        /// <summary>
        /// Draws the sample preview using the current Font / Style / Size selections.
        /// </summary>
        private void DrawSamplePreview(IntPtr hdc, ref RECT rc)
        {
            User32.FillRect(hdc, ref rc, DarkBrush);

            string text = string.IsNullOrEmpty(_sampleText) ? FallbackPreviewText : _sampleText;

            IntPtr previewFont = BuildPreviewFont();

            IntPtr hOld = previewFont != IntPtr.Zero ? GDI32.SelectObject(hdc, previewFont) : (_sampleHwnd != IntPtr.Zero ? SelectControlFont(hdc, _sampleHwnd) : IntPtr.Zero);

            GDI32.SetTextColor(hdc, COLOR_WHITE);
            GDI32.SetBkMode(hdc, GDI32.TRANSPARENT);

            DrawCentered(hdc, text, ref rc);

            RestoreFont(hdc, hOld);
        }

        /// <summary>
        /// Returns the (cached) HFONT for the current combo selections, or Zero if no combo is available.
        /// </summary>
        private IntPtr BuildPreviewFont()
        {
            string face = GetComboSelectionText(_fontComboHwnd);
            string style = GetComboSelectionText(_styleComboHwnd);
            string sizeText = GetComboSelectionText(_sizeComboHwnd);

            if (string.IsNullOrEmpty(face) && string.IsNullOrEmpty(style) && string.IsNullOrEmpty(sizeText)) return IntPtr.Zero;

            var (resolvedFace, weight, italic) = ResolveGdiFace(face, style);

            int pointSize = DEFAULT_PREVIEW_POINTS;
            if (!string.IsNullOrEmpty(sizeText) && int.TryParse(sizeText.Trim(), out int parsed) && parsed > 0) pointSize = parsed;

            return GetCachedFont(resolvedFace, weight, italic, -((pointSize * GetScreenDpiY()) / 72), 0);
        }

        private int GetScreenDpiY()
        {
            if (_dpiY > 0) return _dpiY;

            int dpi = 96;
            IntPtr screenDc = User32.GetDC(IntPtr.Zero);

            if (screenDc != IntPtr.Zero)
            {
                int queried = GDI32.GetDeviceCaps(screenDc, LOGPIXELSY);
                if (queried > 0) dpi = queried;

                User32.ReleaseDC(IntPtr.Zero, screenDc);
            }

            return _dpiY = dpi;
        }

        /// <summary>
        /// Redraws a CBS_DROPDOWNLIST combo's closed state on top of the native paint, inset by 1px so the
        /// border survives and leaving the arrow area to <see cref="DrawArrowGlyph"/>.
        /// </summary>
        private void RedrawClosedComboState(IntPtr hWnd)
        {
            User32.GetClientRect(hWnd, out var rc);
            if (rc.right <= 0 || rc.bottom <= 0) return;

            IntPtr hdc = User32.GetDC(hWnd);
            if (hdc == IntPtr.Zero) return;

            try
            {
                var fill = rc;
                fill.left += 1;
                fill.top += 1;
                fill.right -= 1 + COMBO_ARROW_RESERVE;
                fill.bottom -= 1;

                if (fill.right > fill.left)
                {
                    User32.FillRect(hdc, ref fill, DarkBrush);

                    string text = GetComboSelectionText(hWnd);

                    if (text is not null)
                    {
                        IntPtr hOld = SelectControlFont(hdc, hWnd);

                        GDI32.SetTextColor(hdc, COLOR_WHITE);
                        GDI32.SetBkMode(hdc, GDI32.TRANSPARENT);

                        var textRc = fill;
                        textRc.left += 4;
                        DrawLabel(hdc, text, ref textRc);

                        RestoreFont(hdc, hOld);
                    }
                }

                // Arrow area: same dark background plus a small manual chevron, always drawn.
                RECT arrowRc = new()
                {
                    left = rc.right - 1 - COMBO_ARROW_RESERVE,
                    top = rc.top + 1,
                    right = rc.right - 1,
                    bottom = rc.bottom - 1
                };

                if (arrowRc.right > arrowRc.left)
                {
                    User32.FillRect(hdc, ref arrowRc, DarkBrush);
                    DrawArrowGlyph(hdc, arrowRc);
                }
            }
            finally
            {
                User32.ReleaseDC(hWnd, hdc);
            }
        }

        /// <summary>
        /// Small white down-chevron centered in <paramref name="rc"/> as a filled GDI polygon
        /// (DrawFrameControl only renders light-theme glyphs, and no icon resource is needed).
        /// </summary>
        private void DrawArrowGlyph(IntPtr hdc, RECT rc)
        {
            int cx = (rc.left + rc.right) / 2;
            int cy = (rc.top + rc.bottom) / 2;
            const int halfWidth = 3;
            const int height = 3;

            _arrowPoints[0] = new User32.POINT { X = cx - halfWidth, Y = cy - height / 2 };
            _arrowPoints[1] = new User32.POINT { X = cx + halfWidth, Y = cy - height / 2 };
            _arrowPoints[2] = new User32.POINT { X = cx, Y = cy + height / 2 + 1 };

            IntPtr oldBrush = GDI32.SelectObject(hdc, WhiteBrush);
            IntPtr oldPen = GDI32.SelectObject(hdc, WhitePen);

            GDI32.Polygon(hdc, _arrowPoints, _arrowPoints.Length);

            GDI32.SelectObject(hdc, oldBrush);
            GDI32.SelectObject(hdc, oldPen);
        }

        #endregion

        #region Font dialog: script list painting

        /// <summary>
        /// Paints the whole script list into a memory DC and blits it once (no visible intermediate state).
        /// </summary>
        private void DrawScriptListBuffered(IntPtr listHwnd, IntPtr hdc)
        {
            User32.GetClientRect(listHwnd, out var rc);
            int w = rc.right - rc.left, h = rc.bottom - rc.top;
            if (w <= 0 || h <= 0) return;

            IntPtr mem = GDI32.CreateCompatibleDC(hdc);
            if (mem == IntPtr.Zero) { DrawScriptListItems(listHwnd, hdc); return; }

            IntPtr bmp = GDI32.CreateCompatibleBitmap(hdc, w, h);
            IntPtr oldBmp = GDI32.SelectObject(mem, bmp);

            try
            {
                DrawScriptListItems(listHwnd, mem);
                GDI32.BitBlt(hdc, 0, 0, w, h, mem, 0, 0, SRCCOPY);
            }
            finally
            {
                GDI32.SelectObject(mem, oldBmp);
                GDI32.DeleteObject(bmp);
                GDI32.DeleteDC(mem);
            }
        }

        private void DrawScriptListItems(IntPtr listHwnd, IntPtr hdc)
        {
            User32.GetClientRect(listHwnd, out var clientRc);
            User32.FillRect(hdc, ref clientRc, DarkBrush);

            int count = (int)User32.SendMessage(listHwnd, LB_GETCOUNT, IntPtr.Zero, IntPtr.Zero);
            if (count <= 0) return;

            int topIndex = Math.Max(0, (int)User32.SendMessage(listHwnd, LB_GETTOPINDEX, IntPtr.Zero, IntPtr.Zero));
            int curSel = (int)User32.SendMessage(listHwnd, LB_GETCURSEL, IntPtr.Zero, IntPtr.Zero);

            IntPtr hOldFont = SelectControlFont(hdc, listHwnd);

            GDI32.SetTextColor(hdc, COLOR_WHITE);
            GDI32.SetBkMode(hdc, GDI32.TRANSPARENT);

            try
            {
                for (int i = topIndex; i < count; i++)
                {
                    if (!TryGetListItemRect(listHwnd, i, out RECT itemRc)) break; // scrolled out
                    if (itemRc.top >= clientRc.bottom) break;                     // below the visible area

                    User32.FillRect(hdc, ref itemRc, i == curSel ? SelectionBrush : DarkBrush);

                    string text = GetItemText(listHwnd, LB_GETTEXT, i);
                    if (text is null) continue;

                    var textRc = itemRc;
                    textRc.left += 1;
                    textRc.right -= 4;
                    DrawLabel(hdc, text, ref textRc);
                }
            }
            finally
            {
                RestoreFont(hdc, hOldFont);
            }
        }

        /// <summary>
        /// LB_GETITEMRECT needs a pointer to a RECT as lParam, not a marshaled out-param.
        /// </summary>
        private bool TryGetListItemRect(IntPtr listHwnd, int index, out RECT rect)
        {
            rect = default;

            IntPtr buf = RectBuffer;
            if ((int)(long)User32.SendMessage(listHwnd, LB_GETITEMRECT, (IntPtr)index, buf) < 0) return false; // LB_ERR

            rect = Marshal.PtrToStructure<RECT>(buf);
            return true;
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (_hookId != IntPtr.Zero)
            {
                User32.UnhookWindowsHookEx(_hookId);
                _hookId = IntPtr.Zero;
            }

            if (_cbtHookId != IntPtr.Zero)
            {
                User32.UnhookWindowsHookEx(_cbtHookId);
                _cbtHookId = IntPtr.Zero;
            }

            // Every subclass remembers its own proc, so removal is exact for all of them.
            foreach (var entry in _activeSubclasses)
            {
                Comctl32.RemoveWindowSubclass(entry.Key, entry.Value.Proc, entry.Value.Id);
            }

            _activeSubclasses.Clear();
            _themedControls.Clear();
            _colorizedLists.Clear();

            ResetFontDialogState();
            ClearIconCache();

            DeleteGdiObject(ref _darkBrush);
            DeleteGdiObject(ref _selectionBrush);
            DeleteGdiObject(ref _whiteBrush);
            DeleteGdiObject(ref _whitePen);
            DeleteGdiObject(ref _borderPen);

            FreeBuffer(ref _textBuf);
            FreeBuffer(ref _rectBuf);
            FreeBuffer(ref _comboInfoBuf);
            FreeBuffer(ref _lvItemBuf);

            _disposed = true;
        }

        private static void DeleteGdiObject(ref IntPtr handle)
        {
            if (handle == IntPtr.Zero) return;

            GDI32.DeleteObject(handle);
            handle = IntPtr.Zero;
        }

        private static void FreeBuffer(ref IntPtr buffer)
        {
            if (buffer == IntPtr.Zero) return;

            Marshal.FreeHGlobal(buffer);
            buffer = IntPtr.Zero;
        }

        #endregion
    }
}