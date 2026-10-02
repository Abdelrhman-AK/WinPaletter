using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
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
        // Font dialog control IDs (comdlg32.dll / ChooseFont)
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
        internal const int SUBCLASS_ID_SCRIPTCOMBO = 15;
        internal const int SUBCLASS_ID_SCRIPTLIST = 16;

        // Messages / control messages issued directly.
        private const uint LB_GETTEXT = 0x0189;
        private const uint LB_SETBKCOLOR = 0x0199;
        private const uint CB_GETCURSEL = 0x0147;
        private const uint CB_GETLBTEXT = 0x0148;
        private const uint LB_GETCOUNT = 0x018B;
        private const uint LB_GETTOPINDEX = 0x018E;
        private const uint LB_GETITEMRECT = 0x0198;

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
        private const int CAPTION_PAD_LEFT = 9;
        private const int CAPTION_PAD_RIGHT = 0;

        private const uint LB_GETCURSEL = 0x0188;
        private const uint LB_SETCURSEL = 0x0186;
        private const uint LB_SETTOPINDEX = 0x0197;
        private const uint LB_SETCARETINDEX = 0x019E;

        private readonly DarkWin32 _owner;

        private readonly Comctl32.SUBCLASSPROC _dialogProcDelegate;
        private readonly Comctl32.SUBCLASSPROC _listProcDelegate;
        private readonly Comctl32.SUBCLASSPROC _sampleProcDelegate;
        private readonly Comctl32.SUBCLASSPROC _comboProcDelegate;
        private readonly Comctl32.SUBCLASSPROC _groupBoxProcDelegate;
        private readonly User32.EnumWindowsProc _enumChildDelegate;
        private readonly User32.EnumWindowsProc _findSampleDelegate;
        private readonly User32.EnumWindowsProc _enumCombosDelegate;
        private readonly Comctl32.SUBCLASSPROC _scriptComboProcDelegate;
        private readonly Comctl32.SUBCLASSPROC _scriptListProcDelegate;
        internal Comctl32.SUBCLASSPROC ScriptComboProcDelegate => _scriptComboProcDelegate;
        internal Comctl32.SUBCLASSPROC DialogProcDelegate => _dialogProcDelegate;
        internal Comctl32.SUBCLASSPROC ListProcDelegate => _listProcDelegate;
        internal Comctl32.SUBCLASSPROC SampleProcDelegate => _sampleProcDelegate;
        internal Comctl32.SUBCLASSPROC ComboProcDelegate => _comboProcDelegate;
        internal Comctl32.SUBCLASSPROC GroupBoxProcDelegate => _groupBoxProcDelegate;
        internal Comctl32.SUBCLASSPROC ScriptListProcDelegate => _scriptListProcDelegate;

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
        private string _sampleText = Application.ProductName;

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
            _scriptComboProcDelegate = ScriptComboSubclassProc;
            _scriptListProcDelegate = ScriptListSubclassProc;

            if (_debug) Program.Log?.Debug("[FontDialogDarkHandler] Constructed.");
        }

        #region Detection

        private bool IsScriptDropdownWindow(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero || _scriptComboHwnd == IntPtr.Zero) return false;

            IntPtr parent = User32.GetParent(hWnd);

            // Normal child relationship.
            if (parent == _scriptComboHwnd) return true;

            // Windows 11 popup relationship.
            if (GetClassName(parent) != "Message") return false;

            if (!User32.GetWindowRect(hWnd, out var popupRc)) return false;

            if (!User32.GetWindowRect(_scriptComboHwnd, out var comboRc)) return false;

            int popupWidth = popupRc.right - popupRc.left;
            int comboWidth = comboRc.right - comboRc.left;

            bool horizontalOverlap = popupRc.left < comboRc.right && popupRc.right > comboRc.left;

            bool belowCombo = popupRc.top >= comboRc.bottom - 8 && popupRc.top <= comboRc.bottom + 32;

            bool widthMatches = popupWidth >= comboWidth - 16 && popupWidth <= comboWidth + 64;

            return horizontalOverlap && belowCombo && widthMatches;
        }

        internal bool IsFontDialog(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero) return false;

            string cls = GetClassName(hWnd);

            if (_debug)
            {
                IntPtr p = User32.GetParent(hWnd);
                IntPtr gp = p != IntPtr.Zero ? User32.GetParent(p) : IntPtr.Zero;
                Program.Log?.Debug($"[FontDialogDarkHandler] IsFontDialog ENTRY hWnd=0x{hWnd:X} class='{cls}' parent=0x{p:X} parentClass='{GetClassName(p)}' grandparent=0x{gp:X} _dialogHwnd=0x{_dialogHwnd:X} _scriptComboHwnd=0x{_scriptComboHwnd:X}");
            }

            if ((cls == "ComboLBox" || cls == "ListBox") && IsDropdownListOfOurCombo(hWnd))
            {
                IntPtr comboParent = User32.GetParent(hWnd);
                bool isScriptDropdown = IsScriptDropdownWindow(hWnd);

                if (_debug)
                {
                    Program.Log?.Debug(
                        $"[FontDialogDarkHandler] ComboLBox created " +
                        $"hWnd=0x{hWnd:X} parent=0x{comboParent:X} " +
                        $"parentClass='{GetClassName(comboParent)}' " +
                        $"_scriptComboHwnd=0x{_scriptComboHwnd:X} " +
                        $"isScriptDropdown={isScriptDropdown}");
                }

                NativeMethods.Helpers.SetHWNDDarkMode(hWnd, true);

                UxTheme.SetWindowTheme(hWnd, "", "");
                UxTheme.SetWindowTheme(hWnd, "DarkMode_Explorer", null);

                if (isScriptDropdown)
                {
                    _scriptListHwnd = hWnd;

                    _owner.SubclassWindow(hWnd, _scriptListProcDelegate, (UIntPtr)SUBCLASS_ID_SCRIPTLIST);

                    if (_debug)
                    {
                        Program.Log?.Debug(
                            $"[FontDialogDarkHandler] SCRIPT ComboLBox themed + subclassed " +
                            $"hWnd=0x{hWnd:X}");
                    }
                }
                else
                {
                    _owner.SubclassWindow(hWnd, _listProcDelegate, (UIntPtr)SUBCLASS_ID_FONTLIST);

                    if (_debug) Program.Log?.Debug($"[FontDialogDarkHandler] normal ComboLBox themed + subclassed " + $"hWnd=0x{hWnd:X}");
                }

                User32.RedrawWindow(hWnd, IntPtr.Zero, IntPtr.Zero, 0x0001 | 0x0004 | 0x0100);

                return true;
            }

            if (cls != "#32770") return false;

            IntPtr list = User32.GetDlgItem(hWnd, FONTDLG_ID_LIST_FONT);
            bool isFont = list != IntPtr.Zero;

            if (_debug) Program.Log?.Debug($"[FontDialogDarkHandler] IsFontDialog hWnd=0x{hWnd:X} class='{cls}' list=0x{list:X} -> {isFont}");
            return isFont;
        }

        private const int CBN_DROPDOWN = 7;

        private IntPtr FindScriptComboDropdown() => GetComboListHwnd(_scriptComboHwnd);

        private IntPtr _scriptListThemedHwnd = IntPtr.Zero;

        private void ThemeScriptDropdown(IntPtr listHwnd)
        {
            if (listHwnd == IntPtr.Zero || listHwnd == _scriptListThemedHwnd) return;

            _scriptListHwnd = listHwnd;
            _scriptListThemedHwnd = listHwnd;

            NativeMethods.Helpers.SetHWNDDarkMode(listHwnd, true);
            UxTheme.SetWindowTheme(listHwnd, "", "");
            UxTheme.SetWindowTheme(listHwnd, "DarkMode_Explorer", null);

            _owner.SubclassWindow(listHwnd, _scriptListProcDelegate, (UIntPtr)SUBCLASS_ID_SCRIPTLIST);

            User32.RedrawWindow(listHwnd, IntPtr.Zero, IntPtr.Zero, User32.RedrawWindowFlags.Invalidate | User32.RedrawWindowFlags.Erase | User32.RedrawWindowFlags.Frame | User32.RedrawWindowFlags.UpdateNow);

            if (_debug) Program.Log?.Debug($"[FontDialogDarkHandler] Script dropdown themed hWnd=0x{listHwnd:X}");
        }

        private bool IsDropdownListOfOurCombo(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero || _dialogHwnd == IntPtr.Zero) return false;

            string cls = GetClassName(hWnd);

            if (cls != "ComboLBox" && cls != "ListBox") return false;

            IntPtr parent = User32.GetParent(hWnd);
            string parentClass = GetClassName(parent);

            // Normal case: ComboLBox is actually parented by the ComboBox.
            if (parent == _scriptComboHwnd) return true;

            if (parent != IntPtr.Zero && GetClassName(parent) == "ComboBox" && User32.GetParent(parent) == _dialogHwnd)
            {
                return true;
            }

            // Windows 11 can create the Font dialog's ComboLBox as a popup whose parent is the message window rather than the ComboBox.
            // In that case identify it by its position relative to one of the Font dialog's ComboBoxes.
            if (parentClass != "Message") return false;

            if (!User32.GetWindowRect(hWnd, out var popupRc)) return false;

            if (!User32.GetWindowRect(_scriptComboHwnd, out var scriptRc)) return false;

            int popupWidth = popupRc.right - popupRc.left;
            int comboWidth = scriptRc.right - scriptRc.left;

            bool horizontalOverlap = popupRc.left < scriptRc.right && popupRc.right > scriptRc.left;

            // The dropdown normally starts at or very close to the bottom of the ComboBox. Allow a few pixels for the popup border/shadow.
            bool belowCombo = popupRc.top >= scriptRc.bottom - 8 && popupRc.top <= scriptRc.bottom + 32;

            // Don't accidentally identify an unrelated popup that merely happens to overlap the combo. ComboLBox width is normally close to combo width.
            bool widthMatches = popupWidth >= comboWidth - 16 && popupWidth <= comboWidth + 64;

            bool result = horizontalOverlap && belowCombo && widthMatches;

            if (_debug)
            {
                Program.Log?.Debug(
                    $"[FontDialogDarkHandler] IsDropdownListOfOurCombo " +
                    $"hWnd=0x{hWnd:X} parent=0x{parent:X} parentClass='{parentClass}' " +
                    $"popup=({popupRc.left},{popupRc.top},{popupRc.right},{popupRc.bottom}) " +
                    $"script=({scriptRc.left},{scriptRc.top},{scriptRc.right},{scriptRc.bottom}) " +
                    $"horizontal={horizontalOverlap} below={belowCombo} width={widthMatches} " +
                    $"-> {result}");
            }

            return result;
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

        private static void LogAllChildren(IntPtr dialogHwnd)
        {
            Program.Log?.Debug($"[FontDialogDarkHandler] --- full child dump for dialog hWnd=0x{dialogHwnd:X} ---");

            User32.EnumChildWindows(dialogHwnd, (childHwnd, _) =>
            {
                if (childHwnd == IntPtr.Zero) return true;

                string cls = GetClassName(childHwnd);
                int ctrlId = User32.GetDlgCtrlID(childHwnd);
                int style = (int)User32.GetWindowLong(childHwnd, GWL_STYLE);
                IntPtr parent = User32.GetParent(childHwnd);

                Program.Log?.Debug($"[FontDialogDarkHandler]   child hWnd=0x{childHwnd:X} class='{cls}' id={ctrlId} parent=0x{parent:X} style=0x{style:X}");

                return true;
            }, IntPtr.Zero);

            Program.Log?.Debug("[FontDialogDarkHandler] --- end child dump ---");
        }

        internal void ApplyDarkMode(IntPtr hWnd)
        {
            if (!Program.Style.DarkMode) { if (_debug) Program.Log?.Debug("[FontDialogDarkHandler] ApplyDarkMode skipped: DarkMode disabled."); return; }
            if (OS.WXP || OS.WVista || OS.W7 || OS.W8x) { if (_debug) Program.Log?.Debug("[FontDialogDarkHandler] ApplyDarkMode skipped: unsupported OS."); return; }
            if (hWnd == IntPtr.Zero) { if (_debug) Program.Log?.Debug("[FontDialogDarkHandler] ApplyDarkMode skipped: null hwnd."); return; }

            _dialogHwnd = hWnd;
            if (_debug) Program.Log?.Debug($"[FontDialogDarkHandler] ApplyDarkMode start hWnd=0x{hWnd:X}");

            NativeMethods.Helpers.SetHWNDDarkMode(hWnd, true);
            _owner.SubclassWindow(hWnd, _dialogProcDelegate, (UIntPtr)SUBCLASS_ID_FONTDLG);
            if (_debug) LogAllChildren(hWnd);

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
            if (childHwnd == _fontComboHwnd || childHwnd == _styleComboHwnd || childHwnd == _sizeComboHwnd) return true;

            _comboCount++;

            int ctrlId = User32.GetDlgCtrlID(childHwnd);
            bool isScript = ctrlId != FONTDLG_ID_COMBO_COLOR; // 1139 is the only stable, non-script ID left

            if (isScript)
            {
                // childHwnd IS the script ComboBox.
                // Do not call GetDlgItem(childHwnd, 1140): that searches
                // inside the ComboBox instead of the dialog.
                _scriptComboHwnd = childHwnd;

                NativeMethods.Helpers.SetHWNDDarkMode(_scriptComboHwnd, true);

                UxTheme.SetWindowTheme(_scriptComboHwnd, "", "");

                UxTheme.SetWindowTheme(_scriptComboHwnd, "DarkMode_CFD", null);

                _owner.SubclassWindow(_scriptComboHwnd, _scriptComboProcDelegate, (UIntPtr)SUBCLASS_ID_FONTCOMBO);

                ThemeScriptDropdown(GetComboListHwnd(_scriptComboHwnd));

                if (_debug) Program.Log?.Debug($"[FontDialogDarkHandler] script combo " + $"ID={ctrlId} hWnd=0x{_scriptComboHwnd:X} explicitly subclassed.");
            }
            else
            {
                _colorComboHwnd = childHwnd;
                NativeMethods.Helpers.SetHWNDDarkMode(childHwnd, true);
                UxTheme.SetWindowTheme(childHwnd, "", "");
                UxTheme.SetWindowTheme(childHwnd, "DarkMode_CFD", null);
            }

            NativeMethods.Helpers.SetHWNDDarkMode(childHwnd, true);
            UxTheme.SetWindowTheme(childHwnd, "", "");
            UxTheme.SetWindowTheme(childHwnd, "DarkMode_CFD", null);

            // Only the color combo still needs the manual closed-state redraw / owner-draw path;
            // DarkMode_CFD theming alone renders the script combo natively.
            if (!isScript) _owner.SubclassWindow(childHwnd, _comboProcDelegate, (UIntPtr)SUBCLASS_ID_FONTCOMBO);

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
                        if (isScript)
                        {
                            _scriptListHwnd = child;
                            if (_debug) Program.Log?.Debug($"[FontDialogDarkHandler]   script ComboLBox found via EnumCombosCallback hWnd=0x{child:X}, subclassing.");
                            NativeMethods.Helpers.SetHWNDDarkMode(child, true);
                            UxTheme.SetWindowTheme(child, "", "");
                            UxTheme.SetWindowTheme(child, "DarkMode_Explorer", null);
                            _owner.SubclassWindow(child, _scriptListProcDelegate, (UIntPtr)SUBCLASS_ID_SCRIPTLIST);
                        }
                        else
                        {
                            UxTheme.SetWindowTheme(child, "", "");
                            UxTheme.SetWindowTheme(child, "DarkMode_Explorer", null);
                            _owner.SubclassWindow(child, _listProcDelegate, (UIntPtr)SUBCLASS_ID_FONTLIST);
                        }
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
        /// Maps (family, style) to the GDI face name + lfWeight that actually renders it.
        /// Light/Semilight/Semibold/Black... are separate GDI families ("Segoe UI Light"),
        /// each of which is "Regular" (400) inside itself.
        /// </summary>
        private readonly Dictionary<string, (string Face, int Weight, bool Italic)> _faceCache = [with(StringComparer.OrdinalIgnoreCase)];

        /// <summary>
        /// Resolves the style into either:
        ///
        ///   1. The base family + GDI weight for common styles:
        ///        Segoe UI + Regular       -> Segoe UI, 400
        ///        Segoe UI + Italic        -> Segoe UI, 400, italic
        ///        Segoe UI + Bold          -> Segoe UI, 700
        ///        Segoe UI + Bold Italic   -> Segoe UI, 700, italic
        ///
        ///   2. A full font face for non-basic named variants:
        ///        Segoe UI + Light         -> Segoe UI Light, 400
        ///        Segoe UI + Light Italic  -> Segoe UI Light Italic, 400
        ///        Segoe UI + Semilight     -> Segoe UI Semilight, 400
        ///        etc.
        ///
        /// The important distinction is that Light/Semilight/etc. are treated
        /// as separate face names rather than asking GDI to synthesize them by
        /// changing lfWeight on the base family.
        /// </summary>
        private (string Face, int Weight, bool Italic) ResolveGdiFace(string family, string style, int weight)
        {
            if (string.IsNullOrWhiteSpace(family)) return (family, weight, false);

            style = string.IsNullOrWhiteSpace(style) ? "Regular" : style.Trim();

            string key = family + "|" + style;

            if (_faceCache.TryGetValue(key, out var cached)) return cached;

            string normalized = Regex.Replace( style, @"\s+", " ").Trim();

            string lower = normalized.ToLowerInvariant();

            bool italic = lower.Contains("italic") || lower.Contains("oblique");

            // Remove italic/oblique from the style to obtain the weight/style name.
            string variant = Regex.Replace( normalized, @"\b(italic|oblique)\b", "", RegexOptions.IgnoreCase).Trim();

            variant = Regex.Replace(variant, @"\s+", " ").Trim();

            // Common/basic styles stay in the base family.
            if (variant.Length == 0 ||  variant.Equals("regular", StringComparison.OrdinalIgnoreCase))
            {
                var result = (family, 400, italic);

                _faceCache[key] = result;
                return result;
            }

            if (variant.Equals("bold", StringComparison.OrdinalIgnoreCase))
            {
                var result = (family, 700, italic);

                _faceCache[key] = result;
                return result;
            }

            // Named non-basic variants become their own GDI face.
            //
            // Examples:
            //   Segoe UI + Light
            //       -> Segoe UI Light
            //
            //   Segoe UI + Semilight
            //       -> Segoe UI Semilight
            //
            //   Segoe UI + Light Italic
            //       -> Segoe UI Light Italic

            string fullFace = family + " " + variant;

            if (italic) fullFace += " Italic";

            var namedResult = (Face: fullFace, Weight: 400, Italic: false);

            _faceCache[key] = namedResult;

            return namedResult;
        }

        /// <summary>
        /// Parses a style item string ("Regular", "Italic", "Bold", "SemiBold",
        /// "Bold Italic", "Light Italic", …) into weight + italic flags.
        /// Order matters: "semibold" and "extrabold" must be checked before "bold".
        /// </summary>
        private static void ParseStyleString(string style, out int weight, out bool italic)
        {
            weight = 400;
            italic = false;

            if (string.IsNullOrEmpty(style)) return;

            string s = style.ToLowerInvariant();

            if (s.Contains("italic") || s.Contains("oblique")) italic = true;

            // Order matters: longest/most-specific first.
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
        private IntPtr CreateFontForItem(IntPtr listbox, string faceName, int weight, bool italic)
        {
            int lfHeight = -16;
            int lfWidth = 0;

            IntPtr currentFont = User32.SendMessage( listbox, User32.WindowsMessage.GetFont, IntPtr.Zero, IntPtr.Zero);

            if (currentFont != IntPtr.Zero)
            {
                GDI32.LOGFONT existing = new();

                int result = GDI32.GetObjectFont(currentFont, Marshal.SizeOf<GDI32.LOGFONT>(), existing);

                if (result != 0)
                {
                    if (existing.lfHeight != 0) lfHeight = existing.lfHeight - 2;

                    lfWidth = existing.lfWidth;
                }
            }

            if (faceName.Length > MAX_FACE_NAME) faceName = faceName.Substring(0, MAX_FACE_NAME);

            GDI32.LOGFONT lf = new()
            {
                lfHeight = lfHeight,
                lfWidth = lfWidth,
                lfWeight = weight,
                lfItalic = (byte)(italic ? 1 : 0),
                lfCharSet = 1,
                lfOutPrecision = 7,
                lfQuality = 5,
                lfFaceName = faceName,
            };

            return GDI32.CreateFontIndirect(lf);
        }

        /// <summary>
        /// Script combo: native comctl32 painting throughout (no WM_DRAWITEM, no manual
        /// FillRect/DrawText of the closed state). We only answer the standard
        /// WM_CTLCOLOR* messages the control already sends for its display text and
        /// popup list, exactly like FontListSubclassProc does for the other combos'
        /// ComboLBox children. DarkMode_CFD/DarkMode_Explorer theming covers the frame
        /// and popup chrome; this covers the text/background brush that theming doesn't.
        /// </summary>
        private IntPtr ScriptComboSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData)
        {
            if (!Program.Style.DarkMode) return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);

            if (uMsg == (uint)User32.WindowsMessage.CtlColorStatic || uMsg == (uint)User32.WindowsMessage.CtlColorEdit || uMsg == (uint)User32.WindowsMessage.CtlColorListBox)
            {
                GDI32.SetTextColor(wParam, 0x00FFFFFF);
                GDI32.SetBkColor(wParam, (int)DarkColors.kPrimary.Value);

                return _owner.DarkBrush;
            }

            if (_owner.TryHandleColorMessage(uMsg, wParam, out IntPtr brush))
            {
                return brush;
            }

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

                // The ComboLBox may be created/recreated every time the dropdown opens.
                IntPtr list = FindScriptComboDropdown();

                if (list != IntPtr.Zero) ThemeScriptDropdown(list);

                return result;
            }

            if (uMsg == (uint)User32.WindowsMessage.ShowWindow || uMsg == (uint)User32.WindowsMessage.WindowPosChanged || uMsg == (uint)User32.WindowsMessage.WindowPosChanging)
            {
                IntPtr list = FindScriptComboDropdown();

                if (list != IntPtr.Zero) ThemeScriptDropdown(list);
            }

            return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
        }

        #endregion

        #region Subclass procs

        private IntPtr FontDialogSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData)
        {
            if (Program.Style.DarkMode)
            {
                if (uMsg == (uint)User32.WindowsMessage.Command && ((int)(long)wParam >> 16 & 0xFFFF) == CBN_DROPDOWN && lParam == _scriptComboHwnd)
                {
                    ThemeScriptDropdown(GetComboListHwnd(_scriptComboHwnd));
                }

                if (uMsg == (uint)User32.WindowsMessage.DrawItem && lParam != IntPtr.Zero)
                {
                    var dis = Marshal.PtrToStructure<User32.DRAWITEMSTRUCT>(lParam);

                    bool isSample = dis.CtlID == FONTDLG_ID_SAMPLE || (_sampleHwnd != IntPtr.Zero && dis.hwndItem == _sampleHwnd);

                    if (isSample)
                    {
                        User32.FillRect(dis.hDC, ref dis.rcItem, _owner.DarkBrush);
                        GDI32.SetTextColor(dis.hDC, 0x00FFFFFF);
                        GDI32.SetBkMode(dis.hDC, 1);
                        return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
                    }

                    bool isFontCombo = dis.hwndItem == _fontComboHwnd || User32.GetParent(dis.hwndItem) == _fontComboHwnd;

                    bool isStyleCombo = dis.hwndItem == _styleComboHwnd || User32.GetParent(dis.hwndItem) == _styleComboHwnd;

                    bool isSizeCombo = dis.hwndItem == _sizeComboHwnd || User32.GetParent(dis.hwndItem) == _sizeComboHwnd;

                    if (isFontCombo || isStyleCombo || isSizeCombo) return DrawFontListItem(ref dis);
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

                if (uMsg == (uint)User32.WindowsMessage.CtlColorEdit || uMsg == (uint)User32.WindowsMessage.CtlColorStatic)
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
            if (!Program.Style.DarkMode) return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);

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
            if (!Program.Style.DarkMode) return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);

            if (uMsg == (uint)User32.WindowsMessage.CtlColorStatic || uMsg == (uint)User32.WindowsMessage.CtlColorEdit || uMsg == (uint)User32.WindowsMessage.CtlColorListBox)
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

            if (uMsg == (uint)User32.WindowsMessage.ShowWindow || uMsg == (uint)User32.WindowsMessage.WindowPosChanged)
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
        /// Script combo's dropdown ComboLBox: fully self-painted rather than relying on
        /// WM_DRAWITEM. LBS_OWNERDRAWFIXED can't be safely toggled on after creation here:
        /// the listbox's parent is a CBS_DROPDOWNLIST ComboBox, which never forwards
        /// WM_DRAWITEM to the dialog for a control it doesn't own as owner-draw.
        ///
        /// The native listbox paints highlight changes (hover, arrow keys, LB_SETCURSEL)
        /// directly via GetDC in light colors, bypassing WM_PAINT. So after the native
        /// control handles any highlight-changing message, we repaint the whole list dark.
        /// </summary>
        private IntPtr ScriptListSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData)
        {
            if (!Program.Style.DarkMode) return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);

            switch (uMsg)
            {
                case (uint)User32.WindowsMessage.EraseBkgnd: return (IntPtr)1; // everything is painted in WM_PAINT; erasing here only adds flicker

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
                        User32.RedrawWindow(hWnd, IntPtr.Zero, IntPtr.Zero, 0x0001 | 0x0100);
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

                        // Hover over the same item: nothing to repaint.
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

        private void DrawScriptListItems(IntPtr listHwnd, IntPtr hdc)
        {
            User32.GetClientRect(listHwnd, out var clientRc);
            User32.FillRect(hdc, ref clientRc, _owner.DarkBrush);

            int count = (int)User32.SendMessage(listHwnd, LB_GETCOUNT, IntPtr.Zero, IntPtr.Zero);
            if (count <= 0) return;

            int topIndex = (int)User32.SendMessage(listHwnd, LB_GETTOPINDEX, IntPtr.Zero, IntPtr.Zero);
            if (topIndex < 0) topIndex = 0;

            IntPtr hFont = User32.SendMessage(listHwnd, (uint)User32.WindowsMessage.GetFont, IntPtr.Zero, IntPtr.Zero);
            IntPtr hOldFont = hFont != IntPtr.Zero ? GDI32.SelectObject(hdc, hFont) : IntPtr.Zero;

            IntPtr buf = Marshal.AllocHGlobal(MAX_ITEM_TEXT * 2);
            try
            {
                int curSel = (int)User32.SendMessage(listHwnd, LB_GETCURSEL, IntPtr.Zero, IntPtr.Zero);

                for (int i = topIndex; i < count; i++)
                {
                    if (!TryGetListItemRect(listHwnd, i, out RECT itemRc)) break;   // scrolled out
                    if (itemRc.top >= clientRc.bottom) break;                       // below visible area

                    bool selected = (i == curSel);

                    User32.FillRect(hdc, ref itemRc, selected ? _owner.SelectionBrush : _owner.DarkBrush);

                    for (int b = 0; b < MAX_ITEM_TEXT * 2; b++) Marshal.WriteByte(buf, b, 0);
                    int len = (int)User32.SendMessage(listHwnd, LB_GETTEXT, (IntPtr)i, buf);
                    if (len > 0)
                    {
                        string text = Marshal.PtrToStringUni(buf);

                        GDI32.SetTextColor(hdc, 0x00FFFFFF);
                        GDI32.SetBkMode(hdc, GDI32.TRANSPARENT);

                        var textRc = itemRc;
                        textRc.left += 1;
                        textRc.right -= 4;

                        User32.DrawText(hdc, text, -1, ref textRc, GDI32.DT_LEFT | GDI32.DT_VCENTER | GDI32.DT_SINGLELINE | GDI32.DT_NOPREFIX);
                    }
                }
            }
            finally
            {
                Marshal.FreeHGlobal(buf);
                if (hOldFont != IntPtr.Zero) GDI32.SelectObject(hdc, hOldFont);
            }
        }

        /// <summary>LB_GETITEMRECT needs a pointer to a RECT as lParam, not a marshaled out-param.</summary>
        private static bool TryGetListItemRect(IntPtr listHwnd, int index, out RECT rect)
        {
            rect = default;
            IntPtr buf = Marshal.AllocHGlobal(Marshal.SizeOf<RECT>());
            try
            {
                IntPtr result = User32.SendMessage(listHwnd, LB_GETITEMRECT, (IntPtr)index, buf);
                if ((int)(long)result < 0) return false; // LB_ERR
                rect = Marshal.PtrToStructure<RECT>(buf);
                return true;
            }
            finally
            {
                Marshal.FreeHGlobal(buf);
            }
        }

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

                                IntPtr hFont = User32.SendMessage(hWnd, (uint)User32.WindowsMessage.GetFont, IntPtr.Zero, IntPtr.Zero);
                                IntPtr hOld = IntPtr.Zero;
                                if (hFont != IntPtr.Zero) hOld = GDI32.SelectObject(hdc, hFont);

                                GDI32.SetTextColor(hdc, 0x00FFFFFF);
                                GDI32.SetBkMode(hdc, 1);

                                var textRc = fill;
                                textRc.left += 4;

                                User32.DrawText(hdc, text, -1, ref textRc, GDI32.DT_LEFT | GDI32.DT_VCENTER | GDI32.DT_SINGLELINE | GDI32.DT_NOPREFIX);

                                if (hOld != IntPtr.Zero) GDI32.SelectObject(hdc, hOld);
                            }
                        }
                        finally
                        {
                            Marshal.FreeHGlobal(buf);
                        }
                    }
                }

                // Arrow button area: same dark background, plus a small manually drawn
                // chevron. Independent of whether there's a selection — always drawn.
                RECT arrowRc = new()
                {
                    left = rc.right - 1 - COMBO_ARROW_RESERVE,
                    top = rc.top + 1,
                    right = rc.right - 1,
                    bottom = rc.bottom - 1
                };

                if (arrowRc.right > arrowRc.left)
                {
                    User32.FillRect(hdc, ref arrowRc, _owner.DarkBrush);
                    DrawArrowGlyph(hdc, arrowRc);
                }
            }
            finally
            {
                User32.ReleaseDC(hWnd, hdc);
            }
        }

        /// <summary>
        /// Draws a small down-chevron centered in <paramref name="rc"/>, in white,
        /// via a filled GDI polygon. No DrawFrameControl (renders light-theme glyphs
        /// only) and no icon resource — three points is enough for a combo arrow.
        /// </summary>
        private void DrawArrowGlyph(IntPtr hdc, RECT rc)
        {
            int cx = (rc.left + rc.right) / 2;
            int cy = (rc.top + rc.bottom) / 2;
            const int halfWidth = 3;
            const int height = 3;

            var pts = new[]
            {
                new User32.POINT { X = cx - halfWidth, Y = cy - height / 2 },
                new User32.POINT { X = cx + halfWidth, Y = cy - height / 2 },
                new User32.POINT { X = cx,             Y = cy + height / 2 + 1 },
            };

            IntPtr brush = GDI32.CreateSolidBrush(0x00FFFFFF);
            IntPtr oldBrush = GDI32.SelectObject(hdc, brush);
            IntPtr pen = GDI32.CreatePen(0 /*PS_SOLID*/, 1, 0x00FFFFFF);
            IntPtr oldPen = GDI32.SelectObject(hdc, pen);

            GDI32.Polygon(hdc, pts, pts.Length);

            GDI32.SelectObject(hdc, oldBrush);
            GDI32.SelectObject(hdc, oldPen);
            GDI32.DeleteObject(brush);
            GDI32.DeleteObject(pen);
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
            if (!Program.Style.DarkMode) return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);

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

                        IntPtr hFont = User32.SendMessage(hWnd, (uint)User32.WindowsMessage.GetFont, IntPtr.Zero, IntPtr.Zero);
                        IntPtr hOld = IntPtr.Zero;
                        if (hFont != IntPtr.Zero) hOld = GDI32.SelectObject(hdc, hFont);

                        RECT captionRect;

                        if (isGroupBox)
                        {
                            // Measure the caption text so we fill only as wide as the text actually needs. The group box's top border line remains visible on both sides.
                            UxTheme.SIZE sz;
                            if (!GDI32.GetTextExtentPoint32(hdc, caption, caption.Length, out sz))
                                sz.cx = caption.Length * 7;     // conservative fallback

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
                            // Combo Static child: fill the whole caption area.
                            captionRect = rc;
                        }

                        User32.FillRect(hdc, ref captionRect, _owner.DarkBrush);

                        GDI32.SetTextColor(hdc, 0x00FFFFFF);
                        GDI32.SetBkMode(hdc, 1);

                        User32.DrawText(hdc, caption, -1, ref captionRect, GDI32.DT_LEFT | GDI32.DT_VCENTER | GDI32.DT_SINGLELINE | GDI32.DT_NOPREFIX);

                        if (hOld != IntPtr.Zero) GDI32.SelectObject(hdc, hOld);
                    }

                    // Repaint the sample preview if this group box contains it.
                    if (_sampleHwnd != IntPtr.Zero && isGroupBox && SampleIntersects(hWnd))
                    {
                        if (User32.GetWindowRect(_sampleHwnd, out var smScreen))
                        {
                            User32.POINT pt1 = new() { X = smScreen.left, Y = smScreen.top };
                            User32.POINT pt2 = new() { X = smScreen.right, Y = smScreen.bottom };
                            User32.ScreenToClient(hWnd, ref pt1);
                            User32.ScreenToClient(hWnd, ref pt2);

                            RECT sampleRc = new()
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

            return sm.left < gb.right && sm.right > gb.left && sm.top < gb.bottom && sm.bottom > gb.top;
        }

        #endregion

        #region Drawing

        /// <summary>
        /// Draws one item of the Font / Style / Size list.
        /// <br></br>
        ///  <br></br>- Font combo  (1136): each item is drawn in its own font face.
        ///  <br></br>- Style combo (1137): each item is drawn in its own weight / italic.
        ///  <br></br>- Size combo  (1138): plain text.
        /// </summary>
        private IntPtr DrawFontListItem(ref User32.DRAWITEMSTRUCT dis)
        {
            if (dis.hDC == IntPtr.Zero || dis.hwndItem == IntPtr.Zero) return (IntPtr)1;

            if (dis.itemID == unchecked((uint)-1)) return (IntPtr)1;

            bool selected = (dis.itemState & 0x0001) != 0;
            bool disabled = (dis.itemState & 0x0004) != 0;

            User32.FillRect(dis.hDC, ref dis.rcItem, selected ? _owner.SelectionBrush : _owner.DarkBrush);

            IntPtr buf = Marshal.AllocHGlobal(MAX_ITEM_TEXT * 2);

            try
            {
                for (int i = 0; i < MAX_ITEM_TEXT * 2; i++) Marshal.WriteByte(buf, i, 0);

                int len = (int)User32.SendMessage(dis.hwndItem, LB_GETTEXT, (IntPtr)dis.itemID, buf);

                if (len <= 0) return (IntPtr)1;

                string text = Marshal.PtrToStringUni(buf);

                // Determine Font / Style / Size from the actual combo owning this ComboLBox.
                IntPtr itemFont = BuildItemFont(text, dis.hwndItem);

                IntPtr hOld = IntPtr.Zero;

                if (itemFont != IntPtr.Zero)
                {
                    hOld = GDI32.SelectObject(dis.hDC, itemFont);
                }
                else
                {
                    IntPtr hFont = User32.SendMessage(dis.hwndItem, (uint)User32.WindowsMessage.GetFont, IntPtr.Zero, IntPtr.Zero);

                    if (hFont != IntPtr.Zero)
                    {
                        hOld = GDI32.SelectObject(dis.hDC, hFont);
                    }
                }

                GDI32.SetTextColor(dis.hDC, disabled ? 0x00808080 : 0x00FFFFFF);

                GDI32.SetBkMode(dis.hDC, GDI32.TRANSPARENT);

                RECT rc = dis.rcItem;
                rc.left += 4;
                rc.right -= 4;

                User32.DrawText(dis.hDC, text, -1, ref rc, GDI32.DT_LEFT | GDI32.DT_VCENTER | GDI32.DT_SINGLELINE | GDI32.DT_NOPREFIX);

                if (hOld != IntPtr.Zero) GDI32.SelectObject(dis.hDC, hOld);

                if (itemFont != IntPtr.Zero) GDI32.DeleteObject(itemFont);

                if (selected && (dis.itemState & 0x0010) != 0)
                {
                    User32.DrawFocusRect(dis.hDC, ref dis.rcItem);
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
            if (listbox == IntPtr.Zero) return FontListKind.Unknown;

            IntPtr combo = User32.GetParent(listbox);

            if (combo == _fontComboHwnd) return FontListKind.Font;

            if (combo == _styleComboHwnd) return FontListKind.Style;

            if (combo == _sizeComboHwnd) return FontListKind.Size;

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
            if (string.IsNullOrEmpty(text)) return IntPtr.Zero;

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
                    // Font list: face name IS the full name already (EnumFontFamilies gives full names).
                    return CreateFontForItem(listbox, text, 400, false);

                case FontListKind.Style:
                    {
                        string family = GetComboSelectionText(_fontComboHwnd);

                        ParseStyleString(text, out int weight, out bool italic);

                        var resolved = ResolveGdiFace(family, text, weight);

                        if (_debug)
                        {
                            Program.Log?.Debug(
                                $"[FontDialogDarkHandler] STYLE FONT " +
                                $"family='{family}', " +
                                $"style='{text}', " +
                                $"face='{resolved.Face}', " +
                                $"weight={resolved.Weight}, " +
                                $"italic={resolved.Italic}");
                        }

                        return CreateFontForItem(
                            listbox,
                            resolved.Face,
                            resolved.Weight,
                            resolved.Italic);
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
            if (previewFont != IntPtr.Zero) hOld = GDI32.SelectObject(hdc, previewFont);
            else if (_sampleHwnd != IntPtr.Zero)
            {
                IntPtr hFont = User32.SendMessage(_sampleHwnd, (uint)User32.WindowsMessage.GetFont, IntPtr.Zero, IntPtr.Zero);
                if (hFont != IntPtr.Zero) hOld = GDI32.SelectObject(hdc, hFont);
            }

            GDI32.SetTextColor(hdc, 0x00FFFFFF);
            GDI32.SetBkMode(hdc, 1);

            User32.DrawText(hdc, text, -1, ref rc, GDI32.DT_CENTER | GDI32.DT_VCENTER | GDI32.DT_SINGLELINE | GDI32.DT_NOPREFIX);

            if (hOld != IntPtr.Zero) GDI32.SelectObject(hdc, hOld);
            if (previewFont != IntPtr.Zero) GDI32.DeleteObject(previewFont);

            if (_debug) Program.Log?.Debug($"[FontDialogDarkHandler] sample preview drawn text='{text}'");
        }

        private const uint CB_GETCOMBOBOXINFO = 0x0164;

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

        /// <summary>Gets the dropdown list HWND of any combo, including popup (non-child) ComboLBox.</summary>
        private static IntPtr GetComboListHwnd(IntPtr combo)
        {
            if (combo == IntPtr.Zero) return IntPtr.Zero;

            var info = new COMBOBOXINFO_LOCAL { cbSize = Marshal.SizeOf<COMBOBOXINFO_LOCAL>() };
            IntPtr buf = Marshal.AllocHGlobal(info.cbSize);
            try
            {
                Marshal.StructureToPtr(info, buf, false);
                IntPtr ok = User32.SendMessage(combo, CB_GETCOMBOBOXINFO, IntPtr.Zero, buf);
                if (ok == IntPtr.Zero) return IntPtr.Zero;

                info = Marshal.PtrToStructure<COMBOBOXINFO_LOCAL>(buf);
                return info.hwndList;
            }
            finally
            {
                Marshal.FreeHGlobal(buf);
            }
        }

        /// <summary>Paints the whole script list into a memory DC and blits it once (no visible intermediate state).</summary>
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
                GDI32.BitBlt(hdc, 0, 0, w, h, mem, 0, 0, 0x00CC0020 /*SRCCOPY*/);
            }
            finally
            {
                GDI32.SelectObject(mem, oldBmp);
                GDI32.DeleteObject(bmp);
                GDI32.DeleteDC(mem);
            }
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
            {
                return IntPtr.Zero;
            }

            ParseStyleString(style, out int weight, out bool italic);

            var resolved = ResolveGdiFace(face, style, weight);

            int pointSize = DEFAULT_PREVIEW_POINTS;

            if (!string.IsNullOrEmpty(sizeText) && int.TryParse(sizeText.Trim(), out int parsedSize) && parsedSize > 0)
            {
                pointSize = parsedSize;
            }

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

            GDI32.LOGFONT lf = new()
            {
                lfHeight = lfHeight,
                lfWidth = 0,
                lfWeight = resolved.Weight,
                lfItalic = (byte)(resolved.Italic ? 1 : 0),
                lfCharSet = 1,
                lfOutPrecision = 7,
                lfQuality = 5,
                lfFaceName = resolved.Face
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
            _scriptListThemedHwnd = IntPtr.Zero;

            _sampleText = string.Empty;

            _disposed = true;
        }
    }
}