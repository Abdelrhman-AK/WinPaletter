using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.NativeMethods.UxTheme;

namespace WinPaletter.UI.Dark
{
    /// <summary>
    /// Applies dark mode to the Windows Font Dialog (comdlg32!ChooseFont / CHOOSEFONT).
    ///
    /// Verified against Win11 22H2/23H2. Key facts:
    ///
    ///  - Dialog is class #32770; detection anchors on control ID 1136.
    ///  - Font / Style / Size are ComboBoxes; their dropdown list is a 'ComboLBox'
    ///    child. WM_DRAWITEM for the items is delivered to the ComboBox, with
    ///    dis.hwndItem = the ComboLBox.
    ///  - Color combo is at ID 1139. Script combo has an unstable ID (1140 on
    ///    Win11) and is identified by exclusion during combo enumeration — it gets only
    ///    DarkMode_CFD/DarkMode_Explorer theming, no owner-draw subclassing, so both the
    ///    closed combo and its dropdown ComboLBox render natively.
    ///  - Sample/preview is a plain Static painted by the "Sample" group box's paint
    ///    pass. We redraw it from the group-box proc.
    ///  - CBS_DROPDOWNLIST combos ignore DarkMode_CFD for their closed state; we
    ///    redraw it ourselves with a 1px inset so the border survives, and we do NOT
    ///    overwrite the rightmost 20px so the dropdown arrow survives.
    ///  - Font list items must be drawn in their own font, Style list items in their
    ///    own weight/italic, and the preview in the currently-selected combination.
    ///  - The group-box caption background is filled only as wide as the caption text
    ///    so the group box's top border line stays visible on both sides.
    ///
    /// IMPORTANT: all User32/GDI32 P/Invokes used here MUST be CharSet.Unicode.
    /// </summary>
    internal sealed class FontDialogDarkHandler : IDisposable
    {
        // --- Font dialog control IDs (comdlg32.dll / ChooseFont) ---
        internal const int FONTDLG_ID_LIST_FONT = 0x0470; // 1136 - font ComboBox
        internal const int FONTDLG_ID_LIST_STYLE = 0x0471; // 1137 - style ComboBox
        internal const int FONTDLG_ID_LIST_SIZE = 0x0472; // 1138 - size ComboBox
        internal const int FONTDLG_ID_COMBO_COLOR = 0x0473; // 1139 - color ComboBox
        internal const int FONTDLG_ID_EDIT_STYLE = 0x0474; // 1140 - Script combo on Win11
        internal const int FONTDLG_ID_EDIT_SIZE = 0x0475; // 1141 - unused on Win11
        internal const int FONTDLG_ID_SAMPLE = 0x0476; // 1142 - sample Static
        internal const int FONTDLG_ID_CHECK_STRIKE = 0x0410; // 1040 - strikeout
        internal const int FONTDLG_ID_CHECK_UNDER = 0x0411; // 1041 - underline

        // Subclass IDs reserved for the font dialog.
        internal const int SUBCLASS_ID_FONTDLG = 10;
        internal const int SUBCLASS_ID_FONTLIST = 11;
        internal const int SUBCLASS_ID_FONTSAMPLE = 12;
        internal const int SUBCLASS_ID_FONTCOMBO = 13;
        internal const int SUBCLASS_ID_FONTGROUPBOX = 14;

        // Messages / control messages issued directly.
        private const uint LB_GETTEXT = 0x0189;
        private const uint LB_SETBKCOLOR = 0x0199;
        private const uint CB_GETCURSEL = 0x0147;
        private const uint CB_GETLBTEXT = 0x0148;
        private const uint WM_GETFONT = 0x0031;

        private const int MAX_ITEM_TEXT = 512;
        private const int MAX_FACE_NAME = 31;   // lfFaceName is 32 chars incl. null
        private const int GWL_STYLE = -16;
        private const int BS_GROUPBOX = 0x00000007;

        // SS_* static styles (older-build sample detection).
        private const int SS_TYPEMASK = 0x0000001F;
        private const int SS_BLACKFRAME = 0x00000007;
        private const int SS_WHITEFRAME = 0x00000006;
        private const int SS_OWNERDRAW = 0x0000000B;

        // Sample static heuristics for modern builds.
        private const int SAMPLE_MIN_WIDTH = 100;
        private const int SAMPLE_MIN_HEIGHT = 30;

        // Fallback preview string.
        private string FALLBACK_PREVIEW_TEXT = Application.ProductName;

        // Reserved space on the right of a combo's closed state for the dropdown arrow.
        private const int COMBO_ARROW_RESERVE = 20;

        // Default size (in points) used for the sample preview when the Size combo is
        // not available. 12pt is what ChooseFont seeds itself with by default.
        private const int DEFAULT_PREVIEW_POINTS = 12;

        // Horizontal padding around the group-box caption background fill.
        private const int CAPTION_PAD_LEFT = 7;
        private const int CAPTION_PAD_RIGHT = 4;

        private readonly DarkWin32 _owner;

        private readonly Comctl32.SUBCLASSPROC _dialogProcDelegate;
        private readonly Comctl32.SUBCLASSPROC _listProcDelegate;
        private readonly Comctl32.SUBCLASSPROC _sampleProcDelegate;
        private readonly Comctl32.SUBCLASSPROC _comboProcDelegate;
        private readonly Comctl32.SUBCLASSPROC _groupBoxProcDelegate;
        private readonly User32.EnumWindowsProc _enumChildDelegate;
        private readonly User32.EnumWindowsProc _findSampleDelegate;
        private readonly User32.EnumWindowsProc _enumCombosDelegate;

        internal Comctl32.SUBCLASSPROC DialogProcDelegate => _dialogProcDelegate;
        internal Comctl32.SUBCLASSPROC ListProcDelegate => _listProcDelegate;
        internal Comctl32.SUBCLASSPROC SampleProcDelegate => _sampleProcDelegate;
        internal Comctl32.SUBCLASSPROC ComboProcDelegate => _comboProcDelegate;
        internal Comctl32.SUBCLASSPROC GroupBoxProcDelegate => _groupBoxProcDelegate;

        // Handles of interest.
        private IntPtr _dialogHwnd = IntPtr.Zero;
        private IntPtr _sampleHwnd = IntPtr.Zero;
        private IntPtr _fontComboHwnd = IntPtr.Zero;
        private IntPtr _styleComboHwnd = IntPtr.Zero;
        private IntPtr _sizeComboHwnd = IntPtr.Zero;
        private IntPtr _colorComboHwnd = IntPtr.Zero;
        private IntPtr _scriptComboHwnd = IntPtr.Zero;
        private IntPtr _scriptListHwnd = IntPtr.Zero;

        // Cached sample preview text.
        private string _sampleText = string.Empty;

        // Counters.
        private int _groupBoxCount;
        private int _comboCount;

        private IntPtr _foundSampleHwnd = IntPtr.Zero;

        private bool _disposed;
        private const bool _debug = true;

        internal FontDialogDarkHandler(DarkWin32 owner)
        {
            _owner = owner ?? throw new ArgumentNullException(nameof(owner));

            _dialogProcDelegate = FontDialogSubclassProc;
            _listProcDelegate = FontListSubclassProc;
            _sampleProcDelegate = FontSampleSubclassProc;
            _comboProcDelegate = FontComboSubclassProc;
            _groupBoxProcDelegate = FontGroupBoxSubclassProc;
            _enumChildDelegate = EnumChildCallback;
            _findSampleDelegate = FindSampleStaticCallback;
            _enumCombosDelegate = EnumCombosCallback;

            if (_debug) Program.Log?.Debug("[FontDialogDarkHandler] Constructed.");
        }

        #region Detection

        internal bool IsFontDialog(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) return false;

            string cls = GetClassName(hWnd);

            if ((cls == "ComboLBox" || cls == "ListBox") && IsDropdownListOfOurCombo(hWnd))
            {
                IntPtr comboParent = User32.GetParent(hWnd);
                bool isScriptDropdown = comboParent != IntPtr.Zero && comboParent == _scriptComboHwnd;

                if (_debug) Program.Log?.Debug($"[FontDialogDarkHandler] IsFontDialog hWnd=0x{hWnd:X} class='{cls}' recognised as combo dropdown (script={isScriptDropdown}), theming.");

                NativeMethods.Helpers.SetHWNDDarkMode(hWnd, true);
                UxTheme.SetWindowTheme(hWnd, "", "");
                UxTheme.SetWindowTheme(hWnd, "DarkMode_Explorer", null);

                if (isScriptDropdown)
                    _scriptListHwnd = hWnd;                 // native theme only, no subclass
                else
                    _owner.SubclassWindow(hWnd, _listProcDelegate, (UIntPtr)SUBCLASS_ID_FONTLIST);

                return true;
            }

            if (cls != "#32770") return false;

            IntPtr list = User32.GetDlgItem(hWnd, FONTDLG_ID_LIST_FONT);
            bool isFont = list != IntPtr.Zero;

            if (_debug) Program.Log?.Debug($"[FontDialogDarkHandler] IsFontDialog hWnd=0x{hWnd:X} class='{cls}' list=0x{list:X} -> {isFont}");
            return isFont;
        }

        private bool IsDropdownListOfOurCombo(IntPtr hWnd)
        {
            if (_dialogHwnd == IntPtr.Zero) return false;

            IntPtr parent = User32.GetParent(hWnd);
            if (parent == IntPtr.Zero) return false;
            if (GetClassName(parent) != "ComboBox") return false;

            IntPtr grandparent = User32.GetParent(parent);
            return grandparent == _dialogHwnd;
        }

        private static string GetClassName(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) return string.Empty;

            StringBuilder sb = new(256);
            int len = User32.GetClassName(hWnd, sb, sb.Capacity);

            return len > 0 ? sb.ToString() : string.Empty;
        }

        #endregion

        #region Entry point

        internal void ApplyDarkMode(IntPtr hWnd)
        {
            if (!Program.Style.DarkMode) { if (_debug) Program.Log?.Debug("[FontDialogDarkHandler] ApplyDarkMode skipped: DarkMode disabled."); return; }
            if (OS.WXP || OS.WVista || OS.W7 || OS.W8x) { if (_debug) Program.Log?.Debug("[FontDialogDarkHandler] ApplyDarkMode skipped: unsupported OS."); return; }
            if (hWnd == IntPtr.Zero) { if (_debug) Program.Log?.Debug("[FontDialogDarkHandler] ApplyDarkMode skipped: null hwnd."); return; }

            _dialogHwnd = hWnd;
            if (_debug) Program.Log?.Debug($"[FontDialogDarkHandler] ApplyDarkMode start hWnd=0x{hWnd:X}");

            NativeMethods.Helpers.SetHWNDDarkMode(hWnd, true);
            _owner.SubclassWindow(hWnd, _dialogProcDelegate, (UIntPtr)SUBCLASS_ID_FONTDLG);

            // The three "list" combos.
            foreach (int id in new[] { FONTDLG_ID_LIST_FONT, FONTDLG_ID_LIST_STYLE, FONTDLG_ID_LIST_SIZE })
            {
                IntPtr lb = User32.GetDlgItem(hWnd, id);
                if (lb == IntPtr.Zero) { if (_debug) Program.Log?.Debug($"[FontDialogDarkHandler]   list control id={id} not found."); continue; }

                string lbCls = GetClassName(lb);

                // Remember the three combos for the preview font lookup.
                if (id == FONTDLG_ID_LIST_FONT) _fontComboHwnd = lb;
                if (id == FONTDLG_ID_LIST_STYLE) _styleComboHwnd = lb;
                if (id == FONTDLG_ID_LIST_SIZE) _sizeComboHwnd = lb;

                NativeMethods.Helpers.SetHWNDDarkMode(lb, true);
                UxTheme.SetWindowTheme(lb, "", "");
                UxTheme.SetWindowTheme(lb, "DarkMode_CFD", null);

                _owner.SubclassWindow(lb, _listProcDelegate, (UIntPtr)SUBCLASS_ID_FONTLIST);
                if (_debug) Program.Log?.Debug($"[FontDialogDarkHandler]   list control id={id} hWnd=0x{lb:X} class='{lbCls}' subclassed.");
            }

            // Generic combo pass.
            _comboCount = 0;
            User32.EnumChildWindows(hWnd, _enumCombosDelegate, IntPtr.Zero);
            if (_debug) Program.Log?.Debug($"[FontDialogDarkHandler]   combobox enumeration complete, found={_comboCount}");

            // Sample preview.
            IntPtr sample = User32.GetDlgItem(hWnd, FONTDLG_ID_SAMPLE);
            if (sample == IntPtr.Zero)
            {
                sample = FindSampleStatic(hWnd);
                if (_debug) Program.Log?.Debug($"[FontDialogDarkHandler]   sample not found by ID 1142, enumerated -> 0x{sample:X}");
            }

            if (sample != IntPtr.Zero)
            {
                _sampleHwnd = sample;
                if (string.IsNullOrEmpty(_sampleText)) CacheSampleText(sample);
                _owner.SubclassWindow(sample, _sampleProcDelegate, (UIntPtr)SUBCLASS_ID_FONTSAMPLE);
                if (_debug) Program.Log?.Debug($"[FontDialogDarkHandler]   sample hWnd=0x{sample:X} subclassed (cachedText='{_sampleText}').");
            }

            // Strikeout / underline checkboxes.
            foreach (int id in new[] { FONTDLG_ID_CHECK_STRIKE, FONTDLG_ID_CHECK_UNDER })
            {
                IntPtr chk = User32.GetDlgItem(hWnd, id);
                if (chk == IntPtr.Zero) continue;
                NativeMethods.Helpers.SetHWNDDarkMode(chk, true);
                UxTheme.SetWindowTheme(chk, "DarkMode_Explorer", null);
            }

            // Group boxes.
            _groupBoxCount = 0;
            User32.EnumChildWindows(hWnd, _enumChildDelegate, IntPtr.Zero);
            if (_debug) Program.Log?.Debug($"[FontDialogDarkHandler]   group-box enumeration complete, found={_groupBoxCount}");

            User32.RedrawWindow(hWnd, IntPtr.Zero, IntPtr.Zero, 0x0001 | 0x0004 | 0x0100);
            if (_debug) Program.Log?.Debug($"[FontDialogDarkHandler] ApplyDarkMode done hWnd=0x{hWnd:X}");
        }

        #endregion

        #region Enumeration callbacks

        private bool EnumChildCallback(IntPtr childHwnd, IntPtr lParam)
        {
            if (childHwnd == IntPtr.Zero) return true;

            if (IsGroupBox(childHwnd))
            {
                _groupBoxCount++;
                NativeMethods.Helpers.SetHWNDDarkMode(childHwnd, true);
                UxTheme.SetWindowTheme(childHwnd, "DarkMode_Explorer", null);
                _owner.SubclassWindow(childHwnd, _groupBoxProcDelegate, (UIntPtr)SUBCLASS_ID_FONTGROUPBOX);
            }

            return true;
        }

        private bool EnumCombosCallback(IntPtr childHwnd, IntPtr lParam)
        {
            if (childHwnd == IntPtr.Zero) return true;
            if (GetClassName(childHwnd) != "ComboBox") return true;

            // Font / Style / Size are already handled explicitly by ID — don't re-touch them here.
            if (childHwnd == _fontComboHwnd || childHwnd == _styleComboHwnd || childHwnd == _sizeComboHwnd)
                return true;

            _comboCount++;

            int ctrlId = User32.GetDlgCtrlID(childHwnd);
            bool isScript = ctrlId != FONTDLG_ID_COMBO_COLOR; // 1139 is the only stable, non-script ID left

            if (isScript)
            {
                _scriptComboHwnd = childHwnd;
                if (_debug) Program.Log?.Debug($"[FontDialogDarkHandler]   script combo hWnd=0x{childHwnd:X} id={ctrlId} -> native theme only, no subclass.");
            }
            else
            {
                _colorComboHwnd = childHwnd;
            }

            NativeMethods.Helpers.SetHWNDDarkMode(childHwnd, true);
            UxTheme.SetWindowTheme(childHwnd, "", "");
            UxTheme.SetWindowTheme(childHwnd, "DarkMode_CFD", null);

            // Only the color combo still needs the manual closed-state redraw / owner-draw path;
            // DarkMode_CFD theming alone renders the script combo natively.
            if (!isScript)
                _owner.SubclassWindow(childHwnd, _comboProcDelegate, (UIntPtr)SUBCLASS_ID_FONTCOMBO);

            User32.GetChildWindowHandles(childHwnd).ForEach(child =>
            {
                if (child == IntPtr.Zero) return;

                string cls = GetClassName(child);
                NativeMethods.Helpers.SetHWNDDarkMode(child, true);

                switch (cls)
                {
                    case "Edit":
                        UxTheme.SetWindowTheme(child, "DarkMode_Explorer", null);
                        break;

                    case "Static":
                        UxTheme.SetWindowTheme(child, "DarkMode_Explorer", null);
                        if (!isScript)
                            _owner.SubclassWindow(child, _groupBoxProcDelegate, (UIntPtr)SUBCLASS_ID_FONTGROUPBOX);
                        break;

                    case "ComboLBox":
                    case "ListBox":
                        UxTheme.SetWindowTheme(child, "", "");
                        UxTheme.SetWindowTheme(child, "DarkMode_Explorer", null);

                        if (isScript)
                            _scriptListHwnd = child;   // native theme only
                        else
                            _owner.SubclassWindow(child, _listProcDelegate, (UIntPtr)SUBCLASS_ID_FONTLIST);
                        break;
                }
            });

            return true;
        }

        private bool FindSampleStaticCallback(IntPtr childHwnd, IntPtr lParam)
        {
            if (childHwnd == IntPtr.Zero) return true;
            if (GetClassName(childHwnd) != "Static") return true;

            int style = (int)User32.GetWindowLong(childHwnd, GWL_STYLE);
            int type = style & SS_TYPEMASK;

            User32.GetClientRect(childHwnd, out var rc);
            int textLen = User32.GetWindowTextLength(childHwnd);

            if (type == SS_OWNERDRAW || type == SS_BLACKFRAME || type == SS_WHITEFRAME)
            {
                if (rc.right > 40 && rc.bottom > 20)
                {
                    _foundSampleHwnd = childHwnd;
                    CacheSampleText(childHwnd);
                    return false;
                }
            }

            if (textLen > 0 && rc.right >= SAMPLE_MIN_WIDTH && rc.bottom >= SAMPLE_MIN_HEIGHT)
            {
                _foundSampleHwnd = childHwnd;
                CacheSampleText(childHwnd);
                return false;
            }

            return true;
        }

        private void CacheSampleText(IntPtr sampleHwnd)
        {
            if (sampleHwnd == IntPtr.Zero) return;
            int len = User32.GetWindowTextLength(sampleHwnd);
            if (len <= 0) return;
            StringBuilder sb = new(len + 1);
            User32.GetWindowText(sampleHwnd, sb, sb.Capacity);
            _sampleText = sb.ToString();
        }

        private IntPtr FindSampleStatic(IntPtr hWnd)
        {
            _foundSampleHwnd = IntPtr.Zero;
            User32.EnumChildWindows(hWnd, _findSampleDelegate, IntPtr.Zero);
            return _foundSampleHwnd;
        }

        private static bool IsGroupBox(IntPtr hWnd)
        {
            if (GetClassName(hWnd) != "Button") return false;
            int style = (int)User32.GetWindowLong(hWnd, GWL_STYLE);
            return (style & 0x0000000F) == BS_GROUPBOX;
        }

        #endregion

        #region Combo / listbox font construction

        /// <summary>
        /// Reads the current selection text of a ComboBox (CB_GETLBTEXT).
        /// Returns null if there is no selection.
        /// </summary>
        private static string GetComboSelectionText(IntPtr combo)
        {
            if (combo == IntPtr.Zero) return null;

            int sel = (int)User32.SendMessage(combo, CB_GETCURSEL, IntPtr.Zero, IntPtr.Zero);
            if (sel < 0) return null;

            IntPtr buf = Marshal.AllocHGlobal(MAX_ITEM_TEXT * 2);
            try
            {
                for (int i = 0; i < MAX_ITEM_TEXT * 2; i++) Marshal.WriteByte(buf, i, 0);

                int len = (int)User32.SendMessage(combo, CB_GETLBTEXT, (IntPtr)sel, buf);
                if (len <= 0) return null;

                return Marshal.PtrToStringUni(buf);
            }
            finally
            {
                Marshal.FreeHGlobal(buf);
            }
        }

        /// <summary>
        /// Parses a style item string ("Regular", "Italic", "Bold", "SemiBold",
        /// "Bold Italic", "Light Italic", …) into weight + italic flags.
        /// Order matters: "semibold" and "extrabold" must be checked before "bold".
        /// </summary>
        private static void ParseStyleString(string style, out int weight, out bool italic)
        {
            weight = 400;   // FW_NORMAL
            italic = false;

            if (string.IsNullOrEmpty(style)) return;

            string s = style.ToLowerInvariant();

            if (s.Contains("italic")) italic = true;

            if (s.Contains("thin")) weight = 100;
            else if (s.Contains("extralight") || s.Contains("ultralight")) weight = 200;
            else if (s.Contains("semilight") || s.Contains("demilight")) weight = 350;
            else if (s.Contains("light")) weight = 300;
            else if (s.Contains("medium")) weight = 500;
            else if (s.Contains("semibold") || s.Contains("demibold")) weight = 600;
            else if (s.Contains("extrabold") || s.Contains("ultrabold")) weight = 800;
            else if (s.Contains("bold")) weight = 700;
            else if (s.Contains("black") || s.Contains("heavy")) weight = 900;
        }

        /// <summary>
        /// Creates an HFONT for the given face name and style, at the listbox's own
        /// font height (so the item text fits the row without changing its size).
        ///
        /// The height is read via GetObjectW on the listbox's current font handle.
        /// Passing a real, non-zero lfHeight to CreateFontIndirect is what makes
        /// Windows actually honour lfFaceName — with lfHeight == 0, the font mapper
        /// substitutes a default face and every item renders identically.
        ///
        /// The caller is responsible for deleting the returned HFONT.
        /// </summary>
        private IntPtr CreateFontForItem(
            IntPtr listbox,
            string faceName,
            int weight,
            bool italic)
        {
            int lfHeight = -16;
            int lfWidth = 0;

            IntPtr currentFont = User32.SendMessage(
                listbox,
                WM_GETFONT,
                IntPtr.Zero,
                IntPtr.Zero);

            if (currentFont != IntPtr.Zero)
            {
                GDI32.LOGFONT existing = new();

                int result = GDI32.GetObjectFont(
                    currentFont,
                    Marshal.SizeOf<GDI32.LOGFONT>(),
                    existing);

                if (result != 0)
                {
                    // Preserve the exact row font height.
                    if (existing.lfHeight != 0)
                        lfHeight = existing.lfHeight;

                    lfWidth = existing.lfWidth;
                }
            }

            string face = faceName ?? string.Empty;

            if (face.Length > MAX_FACE_NAME)
                face = face.Substring(0, MAX_FACE_NAME);

            GDI32.LOGFONT lf = new()
            {
                lfHeight = lfHeight,
                lfWidth = lfWidth,

                lfEscapement = 0,
                lfOrientation = 0,

                lfWeight = weight,

                lfItalic = (byte)(italic ? 1 : 0),
                lfUnderline = 0,
                lfStrikeOut = 0,

                lfCharSet = 1, // DEFAULT_CHARSET

                lfOutPrecision = 0,
                lfClipPrecision = 0,

                // Let GDI choose the appropriate rendering.
                lfQuality = 0,

                lfPitchAndFamily = 0,

                lfFaceName = face
            };

            return GDI32.CreateFontIndirect(lf);
        }

        #endregion

        #region Subclass procs

        private IntPtr FontDialogSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData)
        {
            if (Program.Style.DarkMode)
            {
                if (uMsg == (uint)User32.WindowsMessage.DrawItem && lParam != IntPtr.Zero)
                {
                    var dis = Marshal.PtrToStructure<User32.DRAWITEMSTRUCT>(lParam);

                    bool isSample = dis.CtlID == FONTDLG_ID_SAMPLE
                                    || (_sampleHwnd != IntPtr.Zero && dis.hwndItem == _sampleHwnd);

                    if (isSample)
                    {
                        User32.FillRect(dis.hDC, ref dis.rcItem, _owner.DarkBrush);
                        GDI32.SetTextColor(dis.hDC, 0x00FFFFFF);
                        GDI32.SetBkMode(dis.hDC, 1);
                        return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
                    }

                    bool isFontCombo =
                        dis.hwndItem == _fontComboHwnd ||
                        User32.GetParent(dis.hwndItem) == _fontComboHwnd;

                    bool isStyleCombo =
                        dis.hwndItem == _styleComboHwnd ||
                        User32.GetParent(dis.hwndItem) == _styleComboHwnd;

                    bool isSizeCombo =
                        dis.hwndItem == _sizeComboHwnd ||
                        User32.GetParent(dis.hwndItem) == _sizeComboHwnd;

                    if (isFontCombo || isStyleCombo || isSizeCombo)
                        return DrawFontListItem(ref dis);
                }

                if (_owner.TryHandleColorMessage(uMsg, wParam, out IntPtr brush)) return brush;

                if (uMsg == (uint)User32.WindowsMessage.EraseBkgnd)
                {
                    User32.GetClientRect(hWnd, out var rect);
                    User32.FillRect(wParam, ref rect, _owner.DarkBrush);
                    return (IntPtr)1;
                }
            }
            return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
        }

        private IntPtr FontListSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData)
        {
            if (Program.Style.DarkMode)
            {
                if (uMsg == (uint)User32.WindowsMessage.CtlColorListBox)
                {
                    GDI32.SetTextColor(wParam, 0x00FFFFFF);
                    GDI32.SetBkColor(wParam, (int)DarkColors.kPrimary.Value);
                    return _owner.DarkBrush;
                }

                if (uMsg == (uint)User32.WindowsMessage.CtlColorEdit
                 || uMsg == (uint)User32.WindowsMessage.CtlColorStatic)
                {
                    GDI32.SetTextColor(wParam, 0x00FFFFFF);
                    GDI32.SetBkColor(wParam, (int)DarkColors.kPrimary.Value);
                    return _owner.DarkBrush;
                }

                if (_owner.TryHandleColorMessage(uMsg, wParam, out IntPtr brush)) return brush;

                if (uMsg == (uint)User32.WindowsMessage.EraseBkgnd)
                {
                    User32.GetClientRect(hWnd, out var rect);
                    User32.FillRect(wParam, ref rect, _owner.DarkBrush);
                    return (IntPtr)1;
                }

                if (uMsg == (uint)User32.WindowsMessage.DrawItem && lParam != IntPtr.Zero)
                {
                    var dis = Marshal.PtrToStructure<User32.DRAWITEMSTRUCT>(lParam);
                    return DrawFontListItem(ref dis);
                }

                if (uMsg == (uint)User32.WindowsMessage.Paint)
                {
                    User32.SendMessage(hWnd, LB_SETBKCOLOR, IntPtr.Zero, (IntPtr)DarkColors.kPrimary.Value);
                }
            }
            return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
        }

        private IntPtr FontSampleSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData)
        {
            if (!Program.Style.DarkMode)
                return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);

            if (uMsg == (uint)User32.WindowsMessage.EraseBkgnd)
            {
                User32.GetClientRect(hWnd, out var rect);
                User32.FillRect(wParam, ref rect, _owner.DarkBrush);
                return (IntPtr)1;
            }

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

        private IntPtr FontComboSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData)
        {
            if (!Program.Style.DarkMode)
                return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);

            if (uMsg == (uint)User32.WindowsMessage.CtlColorStatic
             || uMsg == (uint)User32.WindowsMessage.CtlColorEdit
             || uMsg == (uint)User32.WindowsMessage.CtlColorListBox)
            {
                GDI32.SetTextColor(wParam, 0x00FFFFFF);
                GDI32.SetBkColor(wParam, (int)DarkColors.kPrimary.Value);
                return _owner.DarkBrush;
            }

            if (_owner.TryHandleColorMessage(uMsg, wParam, out IntPtr brush)) return brush;

            if (uMsg == (uint)User32.WindowsMessage.EraseBkgnd)
            {
                User32.GetClientRect(hWnd, out var rect);
                User32.FillRect(wParam, ref rect, _owner.DarkBrush);
                return (IntPtr)1;
            }

            if (uMsg == (uint)User32.WindowsMessage.Paint)
            {
                IntPtr result = Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
                RedrawClosedComboState(hWnd);
                return result;
            }

            if (uMsg == (uint)User32.WindowsMessage.ShowWindow
                || uMsg == (uint)User32.WindowsMessage.WindowPosChanged)
            {
                User32.GetChildWindowHandles(hWnd).ForEach(child =>
                {
                    if (child == IntPtr.Zero) return;

                    string cls = GetClassName(child);
                    NativeMethods.Helpers.SetHWNDDarkMode(child, true);

                    if (cls == "ComboLBox" || cls == "ListBox")
                    {
                        UxTheme.SetWindowTheme(child, "", "");
                        UxTheme.SetWindowTheme(child, "DarkMode_Explorer", null);
                        _owner.SubclassWindow(child, _listProcDelegate, (UIntPtr)SUBCLASS_ID_FONTLIST);
                    }
                    else
                    {
                        _owner.ApplyDarkModeToControl(new Win32Control(child));
                    }
                });

                User32.RedrawWindow(hWnd, IntPtr.Zero, IntPtr.Zero, 0x0001 | 0x0004 | 0x0100);
            }

            return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
        }

        /// <summary>
        /// Redraws the closed state of a combo (background dark, selected text white).
        /// Insets the fill by 1px so the combo's border survives, and does not fill
        /// the rightmost COMBO_ARROW_RESERVE pixels so the dropdown arrow survives.
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

                if (fill.right <= fill.left) return;

                User32.FillRect(hdc, ref fill, _owner.DarkBrush);

                int sel = (int)User32.SendMessage(hWnd, CB_GETCURSEL, IntPtr.Zero, IntPtr.Zero);
                if (sel >= 0)
                {
                    IntPtr buf = Marshal.AllocHGlobal(MAX_ITEM_TEXT * 2);
                    try
                    {
                        for (int i = 0; i < MAX_ITEM_TEXT * 2; i++) Marshal.WriteByte(buf, i, 0);

                        int len = (int)User32.SendMessage(hWnd, CB_GETLBTEXT, (IntPtr)sel, buf);
                        if (len > 0)
                        {
                            string text = Marshal.PtrToStringUni(buf);

                            IntPtr hFont = User32.SendMessage(hWnd, WM_GETFONT, IntPtr.Zero, IntPtr.Zero);
                            IntPtr hOld = IntPtr.Zero;
                            if (hFont != IntPtr.Zero) hOld = GDI32.SelectObject(hdc, hFont);

                            GDI32.SetTextColor(hdc, 0x00FFFFFF);
                            GDI32.SetBkMode(hdc, 1);

                            var textRc = fill;
                            textRc.left += 4;

                            User32.DrawText(hdc, text, -1, ref textRc,
                                GDI32.DT_LEFT | GDI32.DT_VCENTER | GDI32.DT_SINGLELINE | GDI32.DT_NOPREFIX);

                            if (hOld != IntPtr.Zero) GDI32.SelectObject(hdc, hOld);
                        }
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(buf);
                    }
                }
            }
            finally
            {
                User32.ReleaseDC(hWnd, hdc);
            }
        }

        /// <summary>
        /// Group boxes ("Font:", "Style:", "Effects:", "Sample:" frames).
        ///
        /// Caption painting: we measure the caption text with GetTextExtentPoint32,
        /// then fill only the rect the text occupies (plus a small right padding).
        /// This preserves the group box's top border line on both sides of the
        /// caption.
        ///
        /// Sample preview: comdlg32 draws the preview from the "Sample" group box's
        /// paint pass, so we repaint the sample area here.
        ///
        /// This proc is also reused for the Static children of combos.
        /// </summary>
        private IntPtr FontGroupBoxSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData)
        {
            if (!Program.Style.DarkMode)
                return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);

            if (_owner.TryHandleColorMessage(uMsg, wParam, out IntPtr brush))
                return brush;

            if (uMsg == (uint)User32.WindowsMessage.EraseBkgnd)
            {
                User32.GetClientRect(hWnd, out var rect);
                User32.FillRect(wParam, ref rect, _owner.DarkBrush);
                return (IntPtr)1;
            }

            if (uMsg == (uint)User32.WindowsMessage.Paint)
            {
                IntPtr result = Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);

                IntPtr hdc = User32.GetDC(hWnd);
                if (hdc == IntPtr.Zero) return result;

                try
                {
                    bool isGroupBox = IsGroupBox(hWnd);

                    int len = User32.GetWindowTextLength(hWnd);
                    if (len > 0)
                    {
                        StringBuilder sb = new(len + 1);
                        User32.GetWindowText(hWnd, sb, sb.Capacity);
                        string caption = sb.ToString();

                        User32.GetClientRect(hWnd, out var rc);

                        IntPtr hFont = User32.SendMessage(hWnd, WM_GETFONT, IntPtr.Zero, IntPtr.Zero);
                        IntPtr hOld = IntPtr.Zero;
                        if (hFont != IntPtr.Zero) hOld = GDI32.SelectObject(hdc, hFont);

                        RECT captionRect;

                        if (isGroupBox)
                        {
                            // Measure the caption text so we fill only as wide as the
                            // text actually needs. The group box's top border line
                            // remains visible on both sides.
                            UxTheme.SIZE sz;
                            if (!GDI32.GetTextExtentPoint32(hdc, caption, caption.Length, out sz))
                                sz.cx = caption.Length * 7; // conservative fallback

                            captionRect = new RECT
                            {
                                left = rc.left + CAPTION_PAD_LEFT,
                                top = rc.top,
                                right = rc.left + CAPTION_PAD_LEFT + sz.cx + CAPTION_PAD_RIGHT,
                                bottom = rc.top + 16,
                            };
                        }
                        else
                        {
                            // Combo Static child: fill the whole caption area.
                            captionRect = rc;
                        }

                        User32.FillRect(hdc, ref captionRect, _owner.DarkBrush);

                        GDI32.SetTextColor(hdc, 0x00FFFFFF);
                        GDI32.SetBkMode(hdc, 1);

                        User32.DrawText(hdc, caption, -1, ref captionRect,
                            GDI32.DT_LEFT | GDI32.DT_VCENTER | GDI32.DT_SINGLELINE | GDI32.DT_NOPREFIX);

                        if (hOld != IntPtr.Zero) GDI32.SelectObject(hdc, hOld);
                    }

                    // Repaint the sample preview if this group box contains it.
                    if (_sampleHwnd != IntPtr.Zero && isGroupBox && SampleIntersects(hWnd))
                    {
                        if (User32.GetWindowRect(_sampleHwnd, out var smScreen))
                        {
                            var pt1 = new User32.POINT { X = smScreen.left, Y = smScreen.top };
                            var pt2 = new User32.POINT { X = smScreen.right, Y = smScreen.bottom };
                            User32.ScreenToClient(hWnd, ref pt1);
                            User32.ScreenToClient(hWnd, ref pt2);

                            var sampleRc = new RECT
                            {
                                left = pt1.X,
                                top = pt1.Y,
                                right = pt2.X,
                                bottom = pt2.Y,
                            };

                            DrawSamplePreview(hdc, ref sampleRc);
                        }
                    }
                }
                finally
                {
                    User32.ReleaseDC(hWnd, hdc);
                }

                return result;
            }

            return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
        }

        private bool SampleIntersects(IntPtr groupBoxHwnd)
        {
            if (_sampleHwnd == IntPtr.Zero) return false;
            if (!User32.GetWindowRect(groupBoxHwnd, out var gb)) return false;
            if (!User32.GetWindowRect(_sampleHwnd, out var sm)) return false;

            return sm.left < gb.right
                && sm.right > gb.left
                && sm.top < gb.bottom
                && sm.bottom > gb.top;
        }

        #endregion

        #region Drawing

        /// <summary>
        /// Draws one item of the Font / Style / Size list.
        ///
        ///  - Font combo  (1136): each item is drawn in its own font face.
        ///  - Style combo (1137): each item is drawn in its own weight / italic.
        ///  - Size combo  (1138): plain text.
        /// </summary>
        private IntPtr DrawFontListItem(ref User32.DRAWITEMSTRUCT dis)
        {
            if (dis.hDC == IntPtr.Zero || dis.hwndItem == IntPtr.Zero)
                return (IntPtr)1;

            if (dis.itemID == unchecked((uint)-1))
                return (IntPtr)1;

            bool selected = (dis.itemState & 0x0001) != 0;
            bool disabled = (dis.itemState & 0x0004) != 0;

            User32.FillRect(
                dis.hDC,
                ref dis.rcItem,
                selected
                    ? _owner.SelectionBrush
                    : _owner.DarkBrush);

            IntPtr buf = Marshal.AllocHGlobal(MAX_ITEM_TEXT * 2);

            try
            {
                for (int i = 0; i < MAX_ITEM_TEXT * 2; i++)
                    Marshal.WriteByte(buf, i, 0);

                int len = (int)User32.SendMessage(
                    dis.hwndItem,
                    LB_GETTEXT,
                    (IntPtr)dis.itemID,
                    buf);

                if (len <= 0)
                    return (IntPtr)1;

                string text = Marshal.PtrToStringUni(buf);

                // Determine Font / Style / Size from the actual combo
                // owning this ComboLBox.
                IntPtr itemFont = BuildItemFont(
                    text,
                    dis.hwndItem);

                IntPtr hOld = IntPtr.Zero;

                if (itemFont != IntPtr.Zero)
                {
                    hOld = GDI32.SelectObject(
                        dis.hDC,
                        itemFont);
                }
                else
                {
                    IntPtr hFont = User32.SendMessage(
                        dis.hwndItem,
                        WM_GETFONT,
                        IntPtr.Zero,
                        IntPtr.Zero);

                    if (hFont != IntPtr.Zero)
                    {
                        hOld = GDI32.SelectObject(
                            dis.hDC,
                            hFont);
                    }
                }

                GDI32.SetTextColor(
                    dis.hDC,
                    disabled
                        ? 0x00808080
                        : 0x00FFFFFF);

                GDI32.SetBkMode(
                    dis.hDC,
                    GDI32.TRANSPARENT);

                var rc = dis.rcItem;
                rc.left += 4;
                rc.right -= 4;

                User32.DrawText(
                    dis.hDC,
                    text,
                    -1,
                    ref rc,
                    GDI32.DT_LEFT |
                    GDI32.DT_VCENTER |
                    GDI32.DT_SINGLELINE |
                    GDI32.DT_NOPREFIX);

                if (hOld != IntPtr.Zero)
                    GDI32.SelectObject(dis.hDC, hOld);

                if (itemFont != IntPtr.Zero)
                    GDI32.DeleteObject(itemFont);

                if (selected &&
                    (dis.itemState & 0x0010) != 0)
                {
                    User32.DrawFocusRect(
                        dis.hDC,
                        ref dis.rcItem);
                }
            }
            finally
            {
                Marshal.FreeHGlobal(buf);
            }

            return (IntPtr)1;
        }

        private enum FontListKind
        {
            Unknown,
            Font,
            Style,
            Size
        }

        private FontListKind GetFontListKind(IntPtr listbox)
        {
            if (listbox == IntPtr.Zero)
                return FontListKind.Unknown;

            IntPtr combo = User32.GetParent(listbox);

            if (combo == _fontComboHwnd)
                return FontListKind.Font;

            if (combo == _styleComboHwnd)
                return FontListKind.Style;

            if (combo == _sizeComboHwnd)
                return FontListKind.Size;

            // Some builds can give us a different list window.
            // Fall back to the control ID of the parent ComboBox.
            if (combo != IntPtr.Zero)
            {
                int id = User32.GetDlgCtrlID(combo);

                return id switch
                {
                    FONTDLG_ID_LIST_FONT => FontListKind.Font,
                    FONTDLG_ID_LIST_STYLE => FontListKind.Style,
                    FONTDLG_ID_LIST_SIZE => FontListKind.Size,
                    _ => FontListKind.Unknown
                };
            }

            return FontListKind.Unknown;
        }

        /// <summary>
        /// Chooses (and creates) the HFONT to draw a given list item with.
        /// The caller must delete the returned HFONT.
        /// </summary>
        private IntPtr BuildItemFont(string text, IntPtr listbox)
        {
            if (string.IsNullOrEmpty(text))
                return IntPtr.Zero;

            FontListKind kind = GetFontListKind(listbox);

            if (_debug) Program.Log?.Debug(
                $"[FontDialogDarkHandler] BuildItemFont " +
                $"list=0x{listbox:X}, " +
                $"parent=0x{User32.GetParent(listbox):X}, " +
                $"kind={kind}, " +
                $"text='{text}'");

            switch (kind)
            {
                case FontListKind.Font:
                    return CreateFontForItem(
                        listbox,
                        text,
                        400,
                        false);

                case FontListKind.Style:
                    {
                        string face =
                            GetComboSelectionText(_fontComboHwnd);

                        ParseStyleString(
                            text,
                            out int weight,
                            out bool italic);

                        if (_debug) Program.Log?.Debug(
                            $"[FontDialogDarkHandler] Style item " +
                            $"face='{face}', " +
                            $"weight={weight}, " +
                            $"italic={italic}");

                        return CreateFontForItem(
                            listbox,
                            face,
                            weight,
                            italic);
                    }

                case FontListKind.Size:
                default:
                    return IntPtr.Zero;
            }
        }

        /// <summary>
        /// Draws the sample preview text using the current selections from the Font,
        /// Style and Size combos, so it reflects the user's choice.
        /// </summary>
        private void DrawSamplePreview(IntPtr hdc, ref RECT rc)
        {
            User32.FillRect(hdc, ref rc, _owner.DarkBrush);

            string text = _sampleText;
            if (string.IsNullOrEmpty(text)) text = FALLBACK_PREVIEW_TEXT;

            IntPtr previewFont = BuildPreviewFont();

            IntPtr hOld = IntPtr.Zero;
            if (previewFont != IntPtr.Zero)
                hOld = GDI32.SelectObject(hdc, previewFont);
            else if (_sampleHwnd != IntPtr.Zero)
            {
                IntPtr hFont = User32.SendMessage(_sampleHwnd, WM_GETFONT, IntPtr.Zero, IntPtr.Zero);
                if (hFont != IntPtr.Zero) hOld = GDI32.SelectObject(hdc, hFont);
            }

            GDI32.SetTextColor(hdc, 0x00FFFFFF);
            GDI32.SetBkMode(hdc, 1);

            User32.DrawText(hdc, text, -1, ref rc,
                GDI32.DT_CENTER | GDI32.DT_VCENTER | GDI32.DT_SINGLELINE | GDI32.DT_NOPREFIX);

            if (hOld != IntPtr.Zero) GDI32.SelectObject(hdc, hOld);
            if (previewFont != IntPtr.Zero) GDI32.DeleteObject(previewFont);

            if (_debug) Program.Log?.Debug($"[FontDialogDarkHandler] sample preview drawn text='{text}'");
        }

        /// <summary>
        /// Builds an HFONT from the current Font / Style / Size combo selections.
        /// Returns IntPtr.Zero if none of the combos are available.
        /// The caller must delete the returned HFONT.
        /// </summary>
        private IntPtr BuildPreviewFont()
        {
            string face = GetComboSelectionText(_fontComboHwnd);
            string style = GetComboSelectionText(_styleComboHwnd);
            string sizeText = GetComboSelectionText(_sizeComboHwnd);

            if (string.IsNullOrEmpty(face) && string.IsNullOrEmpty(style) && string.IsNullOrEmpty(sizeText))
                return IntPtr.Zero;

            ParseStyleString(style, out int weight, out bool italic);

            int pointSize = DEFAULT_PREVIEW_POINTS;
            if (!string.IsNullOrEmpty(sizeText) && int.TryParse(sizeText.Trim(), out int parsed) && parsed > 0)
                pointSize = parsed;

            int lfHeight;
            IntPtr screenDc = User32.GetDC(IntPtr.Zero);
            if (screenDc != IntPtr.Zero)
            {
                int dpi = GDI32.GetDeviceCaps(screenDc, 90); // LOGPIXELSY
                User32.ReleaseDC(IntPtr.Zero, screenDc);
                if (dpi <= 0) dpi = 96;
                lfHeight = -((pointSize * dpi) / 72);
            }
            else
            {
                lfHeight = -((pointSize * 96) / 72);
            }

            string faceName = face ?? string.Empty;
            if (faceName.Length > MAX_FACE_NAME) faceName = faceName.Substring(0, MAX_FACE_NAME);

            GDI32.LOGFONT lf = new()
            {
                lfHeight = lfHeight,
                lfWidth = 0,
                lfEscapement = 0,
                lfOrientation = 0,
                lfWeight = weight,
                lfItalic = (byte)(italic ? 1 : 0),
                lfUnderline = 0,
                lfStrikeOut = 0,
                lfCharSet = 1,       // DEFAULT_CHARSET
                lfOutPrecision = 0,
                lfClipPrecision = 0,
                lfQuality = 0,
                lfPitchAndFamily = 0,
                lfFaceName = faceName,
            };

            return GDI32.CreateFontIndirect(lf);
        }

        #endregion

        public void Dispose()
        {
            if (_disposed) return;

            if (_debug) Program.Log?.Debug($"[FontDialogDarkHandler] Dispose (dialog=0x{_dialogHwnd:X} sample=0x{_sampleHwnd:X}).");

            _dialogHwnd = IntPtr.Zero;
            _sampleHwnd = IntPtr.Zero;
            _fontComboHwnd = IntPtr.Zero;
            _styleComboHwnd = IntPtr.Zero;
            _sizeComboHwnd = IntPtr.Zero;
            _foundSampleHwnd = IntPtr.Zero;
            _colorComboHwnd = IntPtr.Zero;
            _scriptComboHwnd = IntPtr.Zero;
            _scriptListHwnd = IntPtr.Zero;
            _sampleText = string.Empty;

            _disposed = true;
        }
    }
}