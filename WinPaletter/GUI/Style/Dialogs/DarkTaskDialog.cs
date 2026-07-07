using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.NativeMethods.Comctl32;
using static WinPaletter.NativeMethods.DWMAPI;
using static WinPaletter.NativeMethods.GDI32;
using static WinPaletter.NativeMethods.Shell32;
using static WinPaletter.NativeMethods.User32;
using static WinPaletter.NativeMethods.UxTheme;

namespace WinPaletter
{
    internal static class DarkColors
    {
        /// <summary>
        /// Primary panel background color.
        /// </summary>
        public static readonly COLORREF kPrimary = new(Color.FromArgb(32, 32, 32));

        /// <summary>
        /// Secondary panel background color.
        /// </summary>
        public static readonly COLORREF kSecondary = new(Color.FromArgb(44, 44, 44));

        /// <summary>
        /// Footnote area background color.
        /// </summary>
        public static readonly COLORREF kFootnote = new(Color.FromArgb(44, 44, 44));

        /// <summary>
        /// Separator line color.
        /// </summary>
        public static readonly COLORREF kSeparator = new(Color.FromArgb(77, 77, 77));

        /// <summary>
        /// Normal text color used for general content.
        /// </summary>
        public static readonly COLORREF kTextNormal = new(Color.FromArgb(255, 255, 255));

        /// <summary>
        /// Main instruction text color.
        /// </summary>
        public static readonly COLORREF kTextInstruct = new(Color.FromArgb(153, 235, 255));

        /// <summary>
        /// Content text color used for ContentText, ContentLink, ExpandedInformationText, and ExpandedInformationLink.
        /// </summary>
        public static readonly COLORREF kTextContent = new(Color.FromArgb(255, 255, 255));

        /// <summary>
        /// Expanded information text color.
        /// </summary>
        public static readonly COLORREF kTextExpInfo = new(Color.FromArgb(255, 255, 255));

        /// <summary>
        /// Expando button text color for both expanded and collapsed states.
        /// </summary>
        public static readonly COLORREF kTextExpando = new(Color.FromArgb(255, 255, 255));

        /// <summary>
        /// Verification checkbox text color.
        /// </summary>
        public static readonly COLORREF kTextVerify = new(Color.FromArgb(255, 255, 255));

        /// <summary>
        /// Footnote text color used for FootnoteText and FootnoteTextLink.
        /// </summary>
        public static readonly COLORREF kTextFootnote = new(Color.FromArgb(224, 224, 224));

        /// <summary>
        /// Expanded footer text color used for ExpandedFooterText and ExpandedFooterTextLink.
        /// </summary>
        public static readonly COLORREF kTextFtrExp = new(Color.FromArgb(224, 224, 224));

        /// <summary>
        /// Radio button text color. Note: Radio buttons use system themes by default.
        /// </summary>
        public static readonly COLORREF kTextRadio = new(Color.FromArgb(216, 216, 216));
    }

    internal sealed class UIAElementInfo
    {
        public RECT rect;
        public string automationId = string.Empty;
        public string name = string.Empty;
        public ComUIAutomation.ToggleState toggleState = ComUIAutomation.ToggleState.Indeterminate;
        public ComUIAutomation.ExpandCollapseState expandCollapseState = ComUIAutomation.ExpandCollapseState.LeafNode;

        public bool IsRectEmpty()
        {
            return rect.left >= rect.right || rect.top >= rect.bottom;
        }
    }

    /// <summary>
    /// Per-DirectUI-window state
    /// </summary>
    internal sealed class DirectUIState
    {
        // "TaskDialog"      -> panel bg, ExpandoButton glyph drawing
        // "TaskDialogStyle" -> text colour / font queries for all text parts
        public IntPtr hTD = IntPtr.Zero;
        public IntPtr hTDS = IntPtr.Zero;
        public IntPtr hButton = IntPtr.Zero;
        public bool isDarkTheme;
        public bool themesOk;

        public IntPtr brPrimary = IntPtr.Zero;
        public IntPtr brSecondary = IntPtr.Zero;
        public IntPtr brFootnote = IntPtr.Zero;

        public List<UIAElementInfo> elements = new List<UIAElementInfo>();
        public bool elemsOk;

        public bool tracking;
        public bool pressing;
        public int hotIdx = -1;

        public bool isExpanded;
        public bool isChecked;
        public bool defExpanded;
        public bool defChecked;

        public IntPtr pCfg = IntPtr.Zero;

        public void CloseThemes()
        {
            if (hTD != IntPtr.Zero) { CloseThemeData(hTD); hTD = IntPtr.Zero; }
            if (hTDS != IntPtr.Zero) { CloseThemeData(hTDS); hTDS = IntPtr.Zero; }
            if (hButton != IntPtr.Zero) { CloseThemeData(hButton); hButton = IntPtr.Zero; }
            themesOk = false;
        }

        public void DestroyBrushes()
        {
            if (brPrimary != IntPtr.Zero) { DeleteObject(brPrimary); brPrimary = IntPtr.Zero; }
            if (brSecondary != IntPtr.Zero) { DeleteObject(brSecondary); brSecondary = IntPtr.Zero; }
            if (brFootnote != IntPtr.Zero) { DeleteObject(brFootnote); brFootnote = IntPtr.Zero; }
        }

        public void Destroy()
        {
            CloseThemes();
            DestroyBrushes();
            elements.Clear();
            elemsOk = false;
        }
    }

    /// <summary>
    /// TaskDialog UIFILE part/state ids used by comctl32's built-in theme
    /// </summary>
    internal static class TaskDialogParts
    {
        /// <summary>
        /// Primary panel part ID
        /// </summary>
        public const int TDLG_PRIMARYPANEL = 1;

        /// <summary>
        /// Secondary panel part ID
        /// </summary>
        public const int TDLG_SECONDARYPANEL = 8;

        /// <summary>
        /// Main instruction pane part ID
        /// </summary>
        public const int TDLG_MAININSTRUCTIONPANE = 5;

        /// <summary>
        /// Content pane part ID
        /// </summary>
        public const int TDLG_CONTENTPANE = 7;

        /// <summary>
        /// Expand/Collapse text part ID
        /// </summary>
        public const int TDLG_EXPANDOTEXT = 12;

        /// <summary>
        /// Expand/Collapse button part ID
        /// </summary>
        public const int TDLG_EXPANDOBUTTON = 13;

        /// <summary>
        /// Verification checkbox text part ID
        /// </summary>
        public const int TDLG_VERIFICATIONTEXT = 14;

        /// <summary>
        /// Footnote pane part ID
        /// </summary>
        public const int TDLG_FOOTNOTEPANE = 15;

        /// <summary>
        /// Footnote separator part ID
        /// </summary>
        public const int TDLG_FOOTNOTESEPARATOR = 16;

        /// <summary>
        /// Expanded footer area part ID
        /// </summary>
        public const int TDLG_EXPANDEDFOOTERAREA = 18;

        /// <summary>
        /// Radio button pane part ID
        /// </summary>
        public const int TDLG_RADIOBUTTONPANE = 20;

        /// <summary>
        /// Expanded info pane part ID
        /// </summary>
        public const int TDLG_EXPINFOPANE = 9;

        /// <summary>
        /// Expand/Collapse button normal state
        /// </summary>
        public const int TDLGEBS_NORMAL = 1;

        /// <summary>
        /// Expand/Collapse button hover state
        /// </summary>
        public const int TDLGEBS_HOVER = 2;

        /// <summary>
        /// Expand/Collapse button pressed state
        /// </summary>
        public const int TDLGEBS_PRESSED = 3;

        /// <summary>
        /// Expand/Collapse button expanded normal state
        /// </summary>
        public const int TDLGEBS_EXPANDEDNORMAL = 4;

        /// <summary>
        /// Expand/Collapse button expanded hover state
        /// </summary>
        public const int TDLGEBS_EXPANDEDHOVER = 5;

        /// <summary>
        /// Expand/Collapse button expanded pressed state
        /// </summary>
        public const int TDLGEBS_EXPANDEDPRESSED = 6;

        /// <summary>
        /// Checkbox button part ID
        /// </summary>
        public const int BP_CHECKBOX = 3;

        /// <summary>
        /// Checkbox unchecked normal state
        /// </summary>
        public const int CBS_UNCHECKEDNORMAL = 1;

        /// <summary>
        /// Checkbox unchecked hot (hover) state
        /// </summary>
        public const int CBS_UNCHECKEDHOT = 2;

        /// <summary>
        /// Checkbox unchecked pressed state
        /// </summary>
        public const int CBS_UNCHECKEDPRESSED = 3;

        /// <summary>
        /// Checkbox checked normal state
        /// </summary>
        public const int CBS_CHECKEDNORMAL = 5;

        /// <summary>
        /// Checkbox checked hot (hover) state
        /// </summary>
        public const int CBS_CHECKEDHOT = 6;

        /// <summary>
        /// Checkbox checked pressed state
        /// </summary>
        public const int CBS_CHECKEDPRESSED = 7;

        /// <summary>
        /// Radio button part ID
        /// </summary>
        public const int BP_RADIOBUTTON = 2;

        /// <summary>
        /// Radio button unchecked normal state
        /// </summary>
        public const int RBS_UNCHECKEDNORMAL = 1;

        /// <summary>
        /// Theme property: Text color
        /// </summary>
        public const int TMT_TEXTCOLOR = 3803;

        /// <summary>
        /// Theme property: Fill color
        /// </summary>
        public const int TMT_FILLCOLOR = 3802;

        /// <summary>
        /// Theme property: Content margins
        /// </summary>
        public const int TMT_CONTENTMARGINS = 3602;

        /// <summary>
        /// Theme property: Font
        /// </summary>
        public const int TMT_FONT = 210;

        /// <summary>
        /// True value for theme properties
        /// </summary>
        public const int TS_TRUE = 1;

        /// <summary>
        /// Draw flag for theme properties
        /// </summary>
        public const int TS_DRAW = 1;
    }

    /// <summary>
    /// WinPaletter — Dark-mode support for Win32 TaskDialog on Windows 10 and 11.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This namespace provides a dark-theme extension for the native Win32 TaskDialog
    /// control, enabling dark backgrounds, inverted text colours, and themed child
    /// controls (buttons, radio buttons, checkboxes, progress bars, and hyperlinks)
    /// without modifying the calling application's visual style.
    /// </para>
    /// <para>
    /// The implementation supports two rendering paths:
    /// <list type="bullet">
    ///   <item><description><b>Windows 10 (no native dark theme):</b> Pixel-swap of panel backgrounds, overdraw of icons, glyphs, and text using GDI + UxTheme.</description></item>
    ///   <item><description><b>Windows 11 (native dark theme):</b> Leverages <c>DarkMode_Explorer</c> and <c>DarkMode_DarkTheme</c> theme classes, with fallback rendering for legacy controls.</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// The dark-mode state is attached via window subclassing (<c>SetWindowSubclass</c>)
    /// to the main TaskDialog window and its DirectUI child windows. Removing subclasses
    /// reverts the dialog to the system-light appearance.
    /// </para>
    /// <para>
    /// The <see cref="DarkTaskDialog.DarkenTaskDialog"/> method is the main entry point for
    /// applying dark mode to a TaskDialog window. It must be called after the dialog
    /// has been created and its window handle is known.
    /// </para>
    /// <para>
    /// <b>UI Automation:</b> Element discovery talks directly to uiautomationcore.dll
    /// through the raw COM interfaces defined in <see cref="ComUIAutomation"/>, matching
    /// the original C++ implementation's approach and content-view walking behaviour
    /// without going through the managed <c>System.Windows.Automation</c> wrapper.
    /// </para>
    /// <para>
    /// <b>Configuration:</b> This file depends on a <c>TASKDIALOGCONFIG</c> structure
    /// defined elsewhere in the project. If absent, a minimal definition can be enabled
    /// by defining the <c>DARKMODE_DEFINE_TASKDIALOGCONFIG</c> symbol.
    /// </para>
    /// </remarks>
    public static class DarkTaskDialog
    {
        private const int STATE_SYSTEM_CHECKED = 0x00000010;
        public const uint BS_SOLID = 0;

        private static bool s_hasNativeTheme;

        private static readonly UIntPtr kMainSubclassId = (UIntPtr)0xDEADBEEFUL;
        private static readonly UIntPtr kDirectUISubclassId = (UIntPtr)0xBADF00DUL;
        private static readonly UIntPtr kCtlColorId = (UIntPtr)0xC0FFEE01UL;

        private static readonly Dictionary<IntPtr, DirectUIState> s_states = [];

        // Keep delegate instances alive for the lifetime of the process --
        // SetWindowSubclass only stores a function pointer, so the CLR must
        // not garbage-collect these delegates while native code can still
        // call back into them.
        private static readonly SUBCLASSPROC s_directUiProc = DirectUISubclassProc;
        private static readonly SUBCLASSPROC s_ctColorProc = WmCtColorSubclassProc;
        private static readonly SUBCLASSPROC s_radioProc = RadioSubclassProc;
        private static readonly SUBCLASSPROC s_mainProc = TaskDialogMainSubclassProc;

        // delegates
        public delegate bool WNDENUMPROC(IntPtr hWnd, IntPtr lParam);
        private static readonly UIntPtr kProgressDuiSubclassId = (UIntPtr)0xDEADBEEF02UL;
        private static readonly SUBCLASSPROC s_progressDuiProc = ProgressDuiSubclassProc;
        private static User32.HookProc s_progressThreadHookDelegate;
        private static IntPtr s_hProgressThreadHook = IntPtr.Zero;
        private static IntPtr s_solidSecondaryBrush = IntPtr.Zero;

        private static bool IsDarkThemeActive(string dark, string baseClass)
        {
            IntPtr hD = OpenThemeData(IntPtr.Zero, dark);
            IntPtr hB = OpenThemeData(IntPtr.Zero, baseClass);
            bool active = hD != IntPtr.Zero && hD != hB;
            if (hD != IntPtr.Zero) CloseThemeData(hD);
            if (hB != IntPtr.Zero) CloseThemeData(hB);
            return active;
        }

        private static DirectUIState GetState(IntPtr h)
        {
            DirectUIState state;
            if (!s_states.TryGetValue(h, out state))
            {
                state = new DirectUIState();
                s_states[h] = state;
            }
            return state;
        }

        private static void DestroyState(IntPtr h)
        {
            DirectUIState state;
            if (s_states.TryGetValue(h, out state))
            {
                state.Destroy();
                s_states.Remove(h);
            }
        }

        /// <summary>
        /// Theme handles
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="s"></param>
        private static void RefreshThemes(IntPtr hwnd, DirectUIState s)
        {
            s.CloseThemes();
            uint dpi = GetDpiForWindow(hwnd);

            Func<string, IntPtr> open = delegate (string cls)
            {
                IntPtr h = OpenThemeDataForDpi(hwnd, cls, dpi);
                return h != IntPtr.Zero ? h : OpenThemeData(hwnd, cls);
            };

            bool hasDarkTheme = IsDarkThemeActive("DarkMode_DarkTheme::TaskDialog", "TaskDialog");

            s.isDarkTheme = hasDarkTheme || s_hasNativeTheme;

            if (hasDarkTheme) s.hTD = open("DarkMode_DarkTheme::TaskDialog");
            if (s.hTD == IntPtr.Zero && s_hasNativeTheme) s.hTD = open("DarkMode_Explorer::TaskDialog");
            if (s.hTD == IntPtr.Zero) s.hTD = open("TaskDialog");

            if (s_hasNativeTheme) s.hTDS = open("DarkMode_Explorer::TaskDialog");
            if (s.hTDS == IntPtr.Zero) s.hTDS = open("TaskDialog");

            s.hButton = open("Button");
            s.themesOk = true;
        }

        private static void EnsureBrushes(DirectUIState s)
        {
            if (s.brPrimary == IntPtr.Zero) s.brPrimary = CreateSolidBrush(DarkColors.kPrimary);
            if (s.brSecondary == IntPtr.Zero) s.brSecondary = CreateSolidBrush(DarkColors.kSecondary);
            if (s.brFootnote == IntPtr.Zero) s.brFootnote = CreateSolidBrush(DarkColors.kFootnote);
        }

        private static int GetTextColor(DirectUIState s, int uifilePart)
        {
            // comctl32's "TaskDialogStyle" class is what every TaskDialog text part
            // points to for TMT_TEXTCOLOR, but no dark-mode variant of it exists on
            // any shipping Windows version -- querying it (even DarkMode_-prefixed)
            // always resolves to the light-mode color. That was producing black
            // MainInstruction/ContentText text, so color is never sourced from a
            // theme here; it always comes from the hardcoded dark palette below.
            return uifilePart switch
            {
                TaskDialogParts.TDLG_MAININSTRUCTIONPANE => (int)DarkColors.kTextInstruct,
                TaskDialogParts.TDLG_CONTENTPANE => (int)DarkColors.kTextContent,
                TaskDialogParts.TDLG_EXPINFOPANE => (int)DarkColors.kTextExpInfo,
                TaskDialogParts.TDLG_EXPANDOTEXT => (int)DarkColors.kTextExpando,
                TaskDialogParts.TDLG_VERIFICATIONTEXT => (int)DarkColors.kTextVerify,
                TaskDialogParts.TDLG_FOOTNOTEPANE => (int)DarkColors.kTextFootnote,
                TaskDialogParts.TDLG_EXPANDEDFOOTERAREA => (int)DarkColors.kTextFtrExp,
                TaskDialogParts.TDLG_RADIOBUTTONPANE => (int)DarkColors.kTextRadio,
                _ => (int)DarkColors.kTextNormal,
            };
        }

        // Same TaskDialogStyle unreliability applies to automatic font resolution
        // once the wrong theme handle/class is involved -- an explicit Segoe UI
        // font is created and selected instead of trusting DrawThemeTextEx's
        // per-part theme-font lookup.
        private static LOGFONT GetThemedFont(int uifilePart)
        {
            bool isMainInstruction = uifilePart == TaskDialogParts.TDLG_MAININSTRUCTIONPANE;
            return new LOGFONT
            {
                lfFaceName = "Segoe UI",
                lfHeight = isMainInstruction ? -16 : -12,
                lfWeight = 400
            };
        }

        private static void RefreshElements(IntPtr hwnd, DirectUIState s)
        {
            ComUIAutomation.QueryElements(hwnd, s.elements);

            foreach (UIAElementInfo el in s.elements)
            {
                if (el.automationId == "VerificationCheckBox")
                {
                    s.isChecked = (el.toggleState + STATE_SYSTEM_CHECKED) != 0;
                }
                else if (el.automationId == "ExpandoButton")
                {
                    s.isExpanded = el.expandCollapseState is ComUIAutomation.ExpandCollapseState.Expanded or ComUIAutomation.ExpandCollapseState.PartiallyExpanded;
                }
            }
            if (s.defChecked) s.isChecked = true;

            s.elemsOk = true;
        }

        private static int HitTest(List<UIAElementInfo> els, POINT pt)
        {
            for (int i = 0; i < els.Count; i++)
            {
                if (PtInRect(ref els[i].rect, pt)) return i;
            }
            return -1;
        }

        /// <summary>
        /// Pixel-swap pass
        /// </summary>
        private struct SwapRule
        {
            public byte sR, sG, sB, dR, dG, dB;
        }

        private static unsafe void PixelSwap(IntPtr pxPtr, int rw, int w, int h, SwapRule[] rules)
        {
            byte* basePtr = (byte*)pxPtr.ToPointer();
            for (int y = 0; y < h; y++)
            {
                byte* row = basePtr + (y * rw * 4);
                for (int x = 0; x < w; x++)
                {
                    byte* p = row + (x * 4);
                    // RGBQUAD layout: rgbBlue, rgbGreen, rgbRed, rgbReserved
                    byte pb = p[0];
                    byte pg = p[1];
                    byte pr = p[2];

                    for (int r = 0; r < rules.Length; r++)
                    {
                        if (pr == rules[r].sR && pg == rules[r].sG && pb == rules[r].sB)
                        {
                            p[2] = rules[r].dR;
                            p[1] = rules[r].dG;
                            p[0] = rules[r].dB;
                            p[3] = 55;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Icon helper
        /// <br>NOTE: Adjust member access below to match the project's actual TASKDIALOGCONFIG layout/marshalling helper.</br>
        /// </summary>
        /// <param name="cfg"></param>
        /// <param name="isMain"></param>
        /// <returns></returns>
        private static IntPtr ResolveIcon(TaskDialogConfigView cfg, bool isMain)
        {
            if (isMain)
            {
                if ((cfg.dwFlags & TaskDialogConfigView.TDF_USE_HICON_MAIN) != 0) return cfg.hMainIcon;
            }
            else
            {
                if ((cfg.dwFlags & TaskDialogConfigView.TDF_USE_HICON_FOOTER) != 0) return cfg.hFooterIcon;
            }

            IntPtr res = isMain ? cfg.pszMainIcon : cfg.pszFooterIcon;
            if (res == IntPtr.Zero || (res.ToInt64() >> 16) != 0) return IntPtr.Zero;

            static IntPtr stock(SHSTOCKICONID siid)
            {
                SHSTOCKICONINFO sii = new()
                {
                    cbSize = (uint)Marshal.SizeOf(typeof(SHSTOCKICONINFO))
                };
                int hr = SHGetStockIconInfo(siid, SHGSI.ICON | SHGSI.LARGEICON, ref sii);
                return hr == 0 ? sii.hIcon : IntPtr.Zero;
            }

            long resValue = res.ToInt64();
            if (resValue == TaskDialogConfigView.TD_WARNING_ICON) return stock(SHSTOCKICONID.WARNING);
            if (resValue == TaskDialogConfigView.TD_ERROR_ICON) return stock(SHSTOCKICONID.Error);
            if (resValue == TaskDialogConfigView.TD_INFORMATION_ICON) return stock(SHSTOCKICONID.INFO);
            if (resValue == TaskDialogConfigView.TD_SHIELD_ICON) return stock(SHSTOCKICONID.SHIELD);
            return IntPtr.Zero;
        }

        /// <summary>
        /// PaintDirectUI
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="hdcWin"></param>
        /// <param name="s"></param>
        private static void PaintDirectUI(IntPtr hwnd, IntPtr hdcWin, DirectUIState s)
        {
            if (!s.themesOk) RefreshThemes(hwnd, s);
            EnsureBrushes(s);

            GetClientRect(hwnd, out RECT rc);
            RECT rcForBuffer = rc;
            IntPtr hbp = BeginBufferedPaint(hdcWin, ref rcForBuffer, BPBF_TOPDOWNDIB, IntPtr.Zero, out IntPtr hdcBuf);
            if (hbp == IntPtr.Zero)
            {
                DefSubclassProc(hwnd, WM_PRINTCLIENT, hdcWin, (IntPtr)PRF_CLIENT);
                return;
            }

            // Let comctl32 render the light-mode dialog into the offscreen buffer first.
            DefSubclassProc(hwnd, WM_PRINTCLIENT, hdcBuf, (IntPtr)PRF_CLIENT);

            // Pixel-swap the panel backgrounds to their dark equivalents.
            COLORREF bgPri = new(Color.FromArgb(255, 255, 255));
            COLORREF bgSec = new(Color.FromArgb(240, 240, 240));
            COLORREF bgFtn = new(Color.FromArgb(240, 240, 240));
            {
                IntPtr hL = OpenThemeData(IntPtr.Zero, "TaskDialog");
                if (hL != IntPtr.Zero)
                {
                    if (GetThemeColor(hL, TaskDialogParts.TDLG_PRIMARYPANEL, 0, TaskDialogParts.TMT_FILLCOLOR, out int c) == 0) bgPri = c;
                    if (GetThemeColor(hL, TaskDialogParts.TDLG_SECONDARYPANEL, 0, TaskDialogParts.TMT_FILLCOLOR, out c) == 0) bgSec = c;
                    if (GetThemeColor(hL, TaskDialogParts.TDLG_FOOTNOTEPANE, 0, TaskDialogParts.TMT_FILLCOLOR, out c) == 0) bgFtn = c;
                    CloseThemeData(hL);
                }
            }
            if (!s_hasNativeTheme)
            {
                if (GetBufferedPaintBits(hbp, out IntPtr pPx, out int rw) == 0)
                {
                    GetBufferedPaintTargetRect(hbp, out RECT rcBuf);
                    int w = rcBuf.right - rcBuf.left;
                    int h = rcBuf.bottom - rcBuf.top;

                    SwapRule[] rules =
                    [
                        new SwapRule { sR = bgPri.R, sG = bgPri.G, sB = bgPri.B, dR = DarkColors.kPrimary.R, dG = DarkColors.kPrimary.G, dB = DarkColors.kPrimary.B },
                        new SwapRule { sR = bgSec.R, sG = bgSec.G, sB = bgSec.B, dR = DarkColors.kSecondary.R, dG = DarkColors.kSecondary.G, dB = DarkColors.kSecondary.B },
                        new SwapRule { sR = 128, sG = 128, sB = 128, dR = DarkColors.kSeparator.R, dG = DarkColors.kSeparator.G, dB = DarkColors.kSeparator.B },
                        new SwapRule { sR = 223, sG = 223, sB = 223, dR = DarkColors.kSeparator.R, dG = DarkColors.kSeparator.G, dB = DarkColors.kSeparator.B },
                    ];
                    PixelSwap(pPx, rw, w, h, rules);
                }
            }

            // Icon overdraw applies only to the Windows 10 pixel-swap path; the native Windows 11 theme already paints icons correctly.
            if (s.pCfg != IntPtr.Zero && !s_hasNativeTheme)
            {
                TaskDialogConfigView cfgView = TaskDialogConfigView.FromPointer(s.pCfg);
                foreach (UIAElementInfo el in s.elements)
                {
                    if (el.IsRectEmpty()) continue;

                    IntPtr hIcon = IntPtr.Zero;
                    IntPtr brBg = IntPtr.Zero;

                    if (el.automationId == "MainIcon")
                    {
                        hIcon = ResolveIcon(cfgView, true);
                        brBg = s.brPrimary;
                    }
                    else if (el.automationId == "FootnoteIcon")
                    {
                        hIcon = ResolveIcon(cfgView, false);
                        brBg = s.brFootnote;
                    }

                    if (hIcon == IntPtr.Zero || brBg == IntPtr.Zero) continue;

                    FillRect(hdcBuf, ref el.rect, brBg);
                    DrawIconEx(hdcBuf, el.rect.left, el.rect.top, hIcon, el.rect.right - el.rect.left, el.rect.bottom - el.rect.top, 0, IntPtr.Zero, DI_NORMAL);
                }
            }

            // Glyph overdraw for the ExpandoButton arrow and the verification checkbox.
            if (s.hTD != IntPtr.Zero || s.hButton != IntPtr.Zero)
            {
                for (int i = 0; i < s.elements.Count; i++)
                {
                    UIAElementInfo el = s.elements[i];
                    if (el.IsRectEmpty()) continue;

                    bool hot = i == s.hotIdx;
                    bool press = hot && s.pressing;

                    if (el.automationId == "ExpandoButton" && s.hTD != IntPtr.Zero && !s_hasNativeTheme)
                    {
                        int st;
                        if (press && s.isExpanded) st = TaskDialogParts.TDLGEBS_EXPANDEDPRESSED;
                        else if (press) st = TaskDialogParts.TDLGEBS_PRESSED;
                        else if (hot && s.isExpanded) st = TaskDialogParts.TDLGEBS_EXPANDEDHOVER;
                        else if (hot) st = TaskDialogParts.TDLGEBS_HOVER;
                        else if (s.isExpanded) st = TaskDialogParts.TDLGEBS_EXPANDEDNORMAL;
                        else st = TaskDialogParts.TDLGEBS_NORMAL;

                        GetThemePartSize(s.hTD, hdcBuf, TaskDialogParts.TDLG_EXPANDOBUTTON, st, IntPtr.Zero, TaskDialogParts.TS_TRUE, out SIZE sz);

                        RECT rcGlyph = el.rect;
                        rcGlyph.right = el.rect.left + sz.cx + 1;

                        FillRect(hdcBuf, ref rcGlyph, s.brSecondary);
                        DrawThemeBackground(s.hTD, hdcBuf, TaskDialogParts.TDLG_EXPANDOBUTTON, st, ref rcGlyph, IntPtr.Zero);
                    }
                    else if (el.automationId == "VerificationCheckBox" && s.hButton != IntPtr.Zero && !s_hasNativeTheme)
                    {
                        GetThemePartSize(s.hButton, hdcBuf, TaskDialogParts.BP_CHECKBOX, TaskDialogParts.CBS_UNCHECKEDNORMAL, IntPtr.Zero, TaskDialogParts.TS_DRAW, out SIZE cs);
                        int mg = (el.rect.bottom - el.rect.top - cs.cy) / 3;
                        RECT rcGlyph = new()
                        {
                            left = el.rect.left + mg + 1,
                            top = el.rect.top + mg + 1,
                            right = el.rect.left + mg + 1 + cs.cx,
                            bottom = el.rect.bottom
                        };

                        int st;
                        if (press && s.isChecked) st = TaskDialogParts.CBS_CHECKEDPRESSED;
                        else if (press) st = TaskDialogParts.CBS_UNCHECKEDPRESSED;
                        else if (hot && s.isChecked) st = TaskDialogParts.CBS_CHECKEDHOT;
                        else if (hot) st = TaskDialogParts.CBS_UNCHECKEDHOT;
                        else if (s.isChecked) st = TaskDialogParts.CBS_CHECKEDNORMAL;
                        else st = TaskDialogParts.CBS_UNCHECKEDNORMAL;

                        FillRect(hdcBuf, ref rcGlyph, s.brSecondary);
                        DrawThemeBackground(s.hButton, hdcBuf, TaskDialogParts.BP_CHECKBOX, st, ref rcGlyph, IntPtr.Zero);
                    }
                }
            }

            // Text overdraw for every whitelisted TaskDialog text part.
            {
                IntPtr hThm = s.hTD; // valid dark-capable "TaskDialog" handle; only used for part plumbing, not color/font

                foreach (UIAElementInfo el in s.elements)
                {
                    if (el.IsRectEmpty()) continue;

                    RECT rcText = el.rect;
                    int uiPart = 0;
                    IntPtr brBg = s.brPrimary;
                    uint dtFlags = GDI32.DT_LEFT | GDI32.DT_VCENTER | GDI32.DT_WORDBREAK | GDI32.DT_NOPREFIX;

                    if (el.automationId == "MainInstruction")
                    {
                        uiPart = TaskDialogParts.TDLG_MAININSTRUCTIONPANE;
                        brBg = s.brPrimary;
                    }
                    else if (el.automationId == "ContentText")
                    {
                        uiPart = TaskDialogParts.TDLG_CONTENTPANE;
                        brBg = s.brPrimary;
                    }
                    else if (el.automationId == "ExpandedInformationText")
                    {
                        uiPart = TaskDialogParts.TDLG_EXPINFOPANE;
                        brBg = s.brPrimary;
                    }
                    else if (el.automationId == "ExpandedFooterText")
                    {
                        uiPart = TaskDialogParts.TDLG_EXPANDEDFOOTERAREA;
                        brBg = s.brFootnote;
                    }
                    else if (el.automationId == "FootnoteText")
                    {
                        uiPart = TaskDialogParts.TDLG_FOOTNOTEPANE;
                        brBg = s.brFootnote;
                    }
                    else if (el.automationId == "ExpandoButton" && s.hTD != IntPtr.Zero)
                    {
                        GetThemePartSize(s.hTD, hdcBuf, TaskDialogParts.TDLG_EXPANDOBUTTON, TaskDialogParts.TDLGEBS_NORMAL, IntPtr.Zero, TaskDialogParts.TS_TRUE, out SIZE sz);
                        RECT elRectForMargins = el.rect;
                        GetThemeMargins(s.hTD, hdcBuf, TaskDialogParts.TDLG_VERIFICATIONTEXT, 0, TaskDialogParts.TMT_CONTENTMARGINS, ref elRectForMargins, out MARGINS vtextMargins);
                        rcText.left += sz.cx + vtextMargins.leftWidth - 2;
                        rcText.top += 2;
                        uiPart = TaskDialogParts.TDLG_EXPANDOTEXT;
                        brBg = s.brSecondary;
                        dtFlags = GDI32.DT_LEFT | GDI32.DT_VCENTER | GDI32.DT_NOPREFIX;
                    }
                    else if (el.automationId == "VerificationCheckBox" && s.hButton != IntPtr.Zero && s.hTD != IntPtr.Zero)
                    {
                        GetThemePartSize(s.hButton, hdcBuf, TaskDialogParts.BP_CHECKBOX, TaskDialogParts.CBS_UNCHECKEDNORMAL, IntPtr.Zero, TaskDialogParts.TS_DRAW, out SIZE cs);
                        RECT elRectForMargins = el.rect;
                        GetThemeMargins(s.hTD, hdcBuf, TaskDialogParts.TDLG_VERIFICATIONTEXT, 0, TaskDialogParts.TMT_CONTENTMARGINS, ref elRectForMargins, out MARGINS textMargins);
                        rcText.left = el.rect.left + cs.cx + textMargins.leftWidth + 3;
                        rcText.top += 5;
                        uiPart = TaskDialogParts.TDLG_VERIFICATIONTEXT;
                        brBg = s.brSecondary;
                        dtFlags = GDI32.DT_LEFT | GDI32.DT_VCENTER | GDI32.DT_NOPREFIX;
                    }

                    if (uiPart == 0) continue;

                    DTTOPTS opts = new()
                    {
                        dwSize = (uint)Marshal.SizeOf(typeof(DTTOPTS)),
                        dwFlags = DTT_COMPOSITED | DTT_TEXTCOLOR,
                        crText = new(GetTextColor(s, uiPart))
                    };

                    LOGFONT lf = GetThemedFont(uiPart);
                    IntPtr hFont = CreateFontIndirect(ref lf);
                    IntPtr oldFont = hFont != IntPtr.Zero ? SelectObject(hdcBuf, hFont) : IntPtr.Zero;

                    if (!s_hasNativeTheme) FillRect(hdcBuf, ref rcText, brBg);

                    bool eligibleForPathCompaction =
                        uiPart == TaskDialogParts.TDLG_MAININSTRUCTIONPANE ||
                        uiPart == TaskDialogParts.TDLG_CONTENTPANE ||
                        uiPart == TaskDialogParts.TDLG_EXPINFOPANE ||
                        uiPart == TaskDialogParts.TDLG_EXPANDEDFOOTERAREA ||
                        uiPart == TaskDialogParts.TDLG_FOOTNOTEPANE;

                    // Cap an embedded path/URL at 3/4 of the available line width so it shrinks instead of dominating the wrapped line it sits on.
                    string drawText = eligibleForPathCompaction ? CompactEmbeddedPaths(hdcBuf, el.name, (rcText.right - rcText.left)) : el.name;

                    DrawThemeTextEx(hThm, hdcBuf, uiPart, 0, drawText, -1, dtFlags, ref rcText, ref opts);

                    if (hFont != IntPtr.Zero)
                    {
                        SelectObject(hdcBuf, oldFont);
                        DeleteObject(hFont);
                    }
                }
            }

            EndBufferedPaint(hbp, true);
        }
        
        private static bool LooksLikePath(string token)
        {
            if (string.IsNullOrEmpty(token)) return false;
            return token.Contains(":\\") || token.StartsWith(@"\\") || token.Contains("://") || token.Contains('/');
        }

        private static string CompactEmbeddedPaths(IntPtr hdc, string text, int maxWidthPx)
        {
            if (string.IsNullOrEmpty(text)) return text;

            string[] tokens = System.Text.RegularExpressions.Regex.Split(text, @"(\s+)");

            for (int i = 0; i < tokens.Length; i++)
            {
                if (LooksLikePath(tokens[i]))
                {
                    System.Text.StringBuilder sb = new(tokens[i], 260);
                    if (Shlwapi.PathCompactPath(hdc, sb, (uint)maxWidthPx)) tokens[i] = sb.ToString();
                }
            }

            return string.Concat(tokens);
        }

        /// <summary>
        /// Windows subclass procedure for DirectUI child windows. This is used to intercept paint and mouse messages for the DirectUI elements of a TaskDialog, allowing custom rendering and interaction handling for dark mode support.
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="uId"></param>
        /// <param name="refData"></param>
        /// <returns></returns>
        private static IntPtr DirectUISubclassProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam, UIntPtr uId, IntPtr refData)
        {
            switch (msg)
            {
                case WM_ERASEBKGND:
                    return (IntPtr)1;

                case WM_PAINT:
                    {
                        IntPtr hdc = BeginPaint(hwnd, out PAINTSTRUCT ps);
                        DirectUIState s = GetState(hwnd);
                        RefreshElements(hwnd, s);
                        PaintDirectUI(hwnd, hdc, s);
                        EndPaint(hwnd, ref ps);
                        return IntPtr.Zero;
                    }

                case WM_MOUSEMOVE:
                    {
                        DirectUIState s = GetState(hwnd);
                        if (!s.tracking)
                        {
                            TRACKMOUSEEVENT tme = new()
                            {
                                cbSize = Marshal.SizeOf(typeof(TRACKMOUSEEVENT)),
                                dwFlags = TME_LEAVE,
                                hwndTrack = hwnd
                            };
                            TrackMouseEvent(ref tme);
                            s.tracking = true;
                        }
                        int x = unchecked((short)((long)lParam & 0xFFFF));
                        int y = unchecked((short)(((long)lParam >> 16) & 0xFFFF));
                        POINT pt = new POINT { X = x, Y = y };
                        int newHot = HitTest(s.elements, pt);
                        if (newHot != s.hotIdx)
                        {
                            s.hotIdx = newHot;
                            InvalidateRect(hwnd, IntPtr.Zero, false);
                        }
                        break;
                    }
                case WM_MOUSELEAVE:
                    {
                        DirectUIState s = GetState(hwnd);
                        s.tracking = false;
                        s.pressing = false;
                        if (s.hotIdx != -1)
                        {
                            s.hotIdx = -1;
                            InvalidateRect(hwnd, IntPtr.Zero, false);
                        }
                        break;
                    }
                case WM_LBUTTONDOWN:
                    {
                        DirectUIState s = GetState(hwnd);
                        s.pressing = true;
                        RefreshElements(hwnd, s);
                        InvalidateRect(hwnd, IntPtr.Zero, false);
                        break;
                    }
                case WM_LBUTTONUP:
                    {
                        DirectUIState s = GetState(hwnd);
                        if (s.pressing) s.pressing = false;
                        RefreshElements(hwnd, s);
                        InvalidateRect(hwnd, IntPtr.Zero, false);
                        break;
                    }

                case WM_DESTROY:
                    DestroyState(hwnd);
                    RemoveWindowSubclass(hwnd, s_directUiProc, uId);
                    break;
            }
            return DefSubclassProc(hwnd, msg, wParam, lParam);
        }

        /// <summary>
        /// Windows subclass procedure for WM_CTLCOLOR* messages. This is used to intercept control color messages and apply custom background and text colors for child controls in dark mode, ensuring that they are drawn correctly according to the dark theme.
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="uId"></param>
        /// <param name="dwRef"></param>
        /// <returns></returns>
        private static IntPtr WmCtColorSubclassProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam, UIntPtr uId, IntPtr dwRef)
        {
            switch (msg)
            {
                case WM_ERASEBKGND:
                    {
                        if (dwRef == IntPtr.Zero) break;
                        IntPtr hdc = wParam;
                        RECT rc;
                        GetClientRect(hwnd, out rc);
                        FillRect(hdc, ref rc, dwRef);
                        SetTextColor(hdc, DarkColors.kTextNormal);
                        return (IntPtr)1;
                    }
                case WM_CTLCOLORMSGBOX:
                case WM_CTLCOLOREDIT:
                case WM_CTLCOLORLISTBOX:
                case WM_CTLCOLORBTN:
                case WM_CTLCOLORDLG:
                case WM_CTLCOLORSCROLLBAR:
                case WM_CTLCOLORSTATIC:
                    {
                        IntPtr hdc = wParam;
                        int bg = DarkColors.kSecondary;
                        if (dwRef != IntPtr.Zero)
                        {
                            LOGBRUSH lb = new LOGBRUSH();
                            GetObject(dwRef, Marshal.SizeOf(typeof(LOGBRUSH)), ref lb);
                            if (lb.lbStyle == BS_SOLID) bg = lb.lbColor;
                        }
                        GDI32.SetBkColor(hdc, bg);
                        SetTextColor(hdc, DarkColors.kTextNormal);
                        IntPtr br = dwRef != IntPtr.Zero ? dwRef : GetClassLongPtr(hwnd, GCLP_HBRBACKGROUND);
                        return br;
                    }
                case WM_DESTROY:
                    if (dwRef != IntPtr.Zero) DeleteObject(dwRef);
                    RemoveWindowSubclass(hwnd, s_ctColorProc, uId);
                    return DefSubclassProc(hwnd, msg, wParam, lParam);
            }
            return DefSubclassProc(hwnd, msg, wParam, lParam);
        }

        /// <summary>
        /// Windows subclass procedure for TaskDialog radio buttons. This is used to intercept paint messages and apply custom rendering for radio buttons in dark mode, ensuring that they are drawn correctly according to the dark theme.
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="uId"></param>
        /// <param name="dwRef"></param>
        /// <returns></returns>
        private static IntPtr RadioSubclassProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam, UIntPtr uId, IntPtr dwRef)
        {
            switch (msg)
            {
                case WM_PAINT:
                    {
                        IntPtr hTheme = OpenThemeData(IntPtr.Zero, "TaskDialogStyle");
                        uint dpi = GetDpiForWindow(hwnd);
                        IntPtr hThemeBtn = OpenThemeDataForDpi(hwnd, "BUTTON", dpi);
                        if (hThemeBtn == IntPtr.Zero)
                            hThemeBtn = OpenThemeData(IntPtr.Zero, "Button");

                        RECT rcClient;
                        GetClientRect(hwnd, out rcClient);

                        PAINTSTRUCT ps;
                        IntPtr hdc = BeginPaint(hwnd, out ps);
                        IntPtr hdcBuf = hdc;
                        RECT rcForBuffer = rcClient;
                        IntPtr hbp = BeginBufferedPaint(hdc, ref rcForBuffer, BPBF_TOPDOWNDIB, IntPtr.Zero, out hdcBuf);
                        if (hbp == IntPtr.Zero)
                        {
                            DefSubclassProc(hwnd, WM_PRINTCLIENT, hdcBuf, (IntPtr)PRF_CLIENT);
                        }

                        DefSubclassProc(hwnd, WM_PRINTCLIENT, hdcBuf, (IntPtr)PRF_CLIENT);

                        System.Text.StringBuilder sb = new System.Text.StringBuilder(256);
                        GetWindowTextW(hwnd, sb, sb.Capacity);
                        string text = sb.ToString();

                        SIZE gsize;
                        GetThemePartSize(hThemeBtn, hdcBuf, TaskDialogParts.BP_RADIOBUTTON, TaskDialogParts.RBS_UNCHECKEDNORMAL, IntPtr.Zero, TaskDialogParts.TS_TRUE, out gsize);

                        RECT rcText = new RECT
                        {
                            left = gsize.cx + 2,
                            top = 0,
                            right = rcClient.right,
                            bottom = rcClient.bottom
                        };

                        DTTOPTS dots = new DTTOPTS();
                        dots.dwSize = (uint)Marshal.SizeOf(typeof(DTTOPTS));
                        dots.dwFlags = DTT_COMPOSITED | DTT_TEXTCOLOR;
                        dots.crText = DarkColors.kTextNormal;

                        LOGFONT logFont;
                        GetThemeFont(hTheme, hdcBuf, TaskDialogParts.TDLG_RADIOBUTTONPANE, 0, TaskDialogParts.TMT_FONT, out logFont);
                        IntPtr font = CreateFontIndirect(ref logFont);
                        IntPtr oldFont = SelectObject(hdcBuf, font);

                        DrawThemeTextEx(hTheme, hdcBuf, TaskDialogParts.TDLG_RADIOBUTTONPANE, 0, text, -1, GDI32.DT_LEFT | GDI32.DT_VCENTER | 0x00008000 /* DT_END_ELLIPSIS */, ref rcText, ref dots);

                        CloseThemeData(hTheme);
                        CloseThemeData(hThemeBtn);
                        SelectObject(hdcBuf, oldFont);
                        DeleteObject(font);

                        if (hbp != IntPtr.Zero)
                            EndBufferedPaint(hbp, true);

                        EndPaint(hwnd, ref ps);
                        return IntPtr.Zero;
                    }

                case WM_DESTROY:
                    RemoveWindowSubclass(hwnd, s_radioProc, uId);
                    return DefSubclassProc(hwnd, msg, wParam, lParam);
            }
            return DefSubclassProc(hwnd, msg, wParam, lParam);
        }

        /// <summary>
        /// Windows subclass procedure for the main TaskDialog window. This is used to intercept messages related to theming, dark mode, and UI updates, allowing for custom rendering and behavior in dark mode scenarios.
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="uId"></param>
        /// <param name="dwRef"></param>
        /// <returns></returns>
        private static IntPtr TaskDialogMainSubclassProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam, UIntPtr uId, IntPtr dwRef)
        {
            switch (msg)
            {
                case WM_CTLCOLORDLG:
                    {
                        if (Program.Style.DarkMode)
                        {
                            IntPtr hdc = wParam;
                            GDI32.SetBkColor(hdc, DarkColors.kSecondary);
                            SetTextColor(hdc, DarkColors.kTextNormal);
                            if (s_solidSecondaryBrush == IntPtr.Zero)
                                s_solidSecondaryBrush = CreateSolidBrush(DarkColors.kSecondary);
                            return s_solidSecondaryBrush;
                        }
                        break;
                    }

                case WM_SETTINGCHANGE:
                    if (Program.Style.DarkMode)
                    {
                        DarkenTaskDialog(hwnd, dwRef);
                    }
                    break;

                case WM_DESTROY:
                    RemoveFromTaskDialog(hwnd);
                    RemoveWindowSubclass(hwnd, s_mainProc, uId);
                    break;

                case 0x1102 /*TDN_EXPANDO_BUTTON_CLICKED*/:
                    EnumChildWindows(hwnd, delegate (IntPtr hwndChild, IntPtr lp)
                    {
                        if (GetWindowSubclass(hwndChild, s_directUiProc, kDirectUISubclassId, out _))
                        {
                            DirectUIState s = GetState(hwndChild);
                            // Force-invert the state locally ahead of UIA synchronization
                            s.isExpanded = !s.isExpanded;
                        }
                        return true;
                    }, IntPtr.Zero);

                    PostMessage(hwnd, WM_REFRESH_DUI_STATE, IntPtr.Zero, IntPtr.Zero);
                    break;

                case WM_REFRESH_DUI_STATE:
                    EnumChildWindows(hwnd, delegate (IntPtr hwndChild, IntPtr lp)
                    {
                        if (GetWindowSubclass(hwndChild, s_directUiProc, kDirectUISubclassId, out _))
                        {
                            DirectUIState s = GetState(hwndChild);

                            // Retain the manual toggle state across the refresh safely
                            bool expectedState = s.isExpanded;
                            RefreshElements(hwndChild, s);
                            s.isExpanded = expectedState;

                            InvalidateRect(hwndChild, IntPtr.Zero, false);
                            UpdateWindow(hwndChild);
                        }
                        return true;
                    }, IntPtr.Zero);
                    break;
            }
            return DefSubclassProc(hwnd, msg, wParam, lParam);
        }

        /// <summary>
        /// Windows subclass procedure for the progress dialog's DirectUI window. This is used to intercept paint messages and apply custom theming for dark mode, specifically targeting text elements that are not natively themed by DirectUI.
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="uMsg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="uIdSubclass"></param>
        /// <param name="dwRefData"></param>
        /// <returns></returns>
        private static IntPtr ProgressDuiSubclassProc(IntPtr hwnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData)
        {
            const uint WM_DESTROY = 0x0002;
            const uint WM_PAINT = 0x000F;

            switch (uMsg)
            {
                case WM_PAINT:
                    if (Program.Style.DarkMode)
                    {
                        // 1. Let DirectUI render ALL its native components untouched (AVI, Main Title, colors stay perfectly clean)
                        IntPtr res = DefSubclassProc(hwnd, uMsg, wParam, lParam);

                        // 2. Acquire a true surface DC to draw over the specific text boundaries
                        IntPtr hdcScreen = User32.GetDC(hwnd);

                        if (hdcScreen != IntPtr.Zero)
                        {
                            try
                            {
                                UxTheme.RECT winRect;
                                User32.GetWindowRect(hwnd, out winRect);

                                NativeMethods.IUIAutomationElement baseEl = ComUIAutomation.FromHandle(hwnd);
                                if (baseEl != null)
                                {
                                    // 3. Scan for windowless text elements (UIA_TextControlTypeId = 50020)
                                    foreach (NativeMethods.IUIAutomationElement child in ComUIAutomation.GetContentChildren(baseEl))
                                    {
                                        int controlType = ComUIAutomation.GetPropertyValueInt(child, ComUIAutomation.UIA_ControlTypePropertyId);

                                        if (controlType == 50020)
                                        {
                                            // Get the Automation ID property to safely distinguish the elements
                                            string autoId = ComUIAutomation.GetPropertyValueString(child, ComUIAutomation.UIA_AutomationIdPropertyId);

                                            // LocLabel1 is description label, theme it only (other parts are themed correctly before)
                                            if (!string.IsNullOrEmpty(autoId) && autoId.Equals("LocLabel1", StringComparison.OrdinalIgnoreCase))
                                            {
                                                string textValue = ComUIAutomation.GetPropertyValueString(child, ComUIAutomation.UIA_NamePropertyId);
                                                if (string.IsNullOrWhiteSpace(textValue)) continue;

                                                child.GetCurrentPropertyValue(ComUIAutomation.UIA_BoundingRectanglePropertyId, out object propValue);
                                                double[] boundingBox = propValue as double[];

                                                if (boundingBox != null && boundingBox.Length == 4)
                                                {
                                                    int screenLeft = (int)boundingBox[0];
                                                    int screenTop = (int)boundingBox[1];
                                                    int screenWidth = (int)boundingBox[2];
                                                    int screenHeight = (int)boundingBox[3];

                                                    RECT clientTextRect = new()
                                                    {
                                                        left = screenLeft - winRect.left,
                                                        top = screenTop - winRect.top,
                                                        right = (screenLeft - winRect.left) + screenWidth,
                                                        bottom = (screenTop - winRect.top) + screenHeight
                                                    };

                                                    // 4. PINPOINT MASKING: Wipe ONLY the exact description string coordinate area.
                                                    IntPtr bgBrush = CreateSolidBrush(DarkColors.kSecondary);
                                                    User32.FillRect(hdcScreen, ref clientTextRect, bgBrush);
                                                    DeleteObject(bgBrush);

                                                    // 5. EXTRACT DESIGNATED SYSTEM LOGFONT & DRAW THE WHITE TEXT OVERLAY
                                                    LOGFONT lf = GetThemedFont(7); // Part 7 maps perfectly to TDLG_CONTENTPANE (Description typography metrics)
                                                    IntPtr hFont = GDI32.CreateFontIndirect(ref lf);
                                                    IntPtr oldFont = IntPtr.Zero;

                                                    if (hFont != IntPtr.Zero) oldFont = GDI32.SelectObject(hdcScreen, hFont);

                                                    GDI32.SetTextColor(hdcScreen, (uint)ColorTranslator.ToWin32(Color.White));
                                                    GDI32.SetBkMode(hdcScreen, 1); // TRANSPARENT

                                                    User32.DrawText(hdcScreen, textValue, textValue.Length, ref clientTextRect,
                                                        0x00000000 /* DT_LEFT */ | 0x00000010 /* DT_SINGLELINE */ | 0x00000004 /* DT_VCENTER */ | 0x00008000 /* DT_NOPREFIX */ | 0x00000020 /* DT_PATH_ELLIPSIS */);

                                                    if (oldFont != IntPtr.Zero) GDI32.SelectObject(hdcScreen, oldFont);
                                                    if (hFont != IntPtr.Zero) GDI32.DeleteObject(hFont);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Program.Log?.Write(Serilog.Events.LogEventLevel.Debug, $"[ProgressDialog Direct Overlay Error] {ex.Message}");
                            }

                            User32.ReleaseDC(hwnd, hdcScreen);
                        }

                        return res; // Return the exact result from the procedure chain
                    }
                    break;

                case WM_DESTROY:
                    RemoveWindowSubclass(hwnd, s_progressDuiProc, uIdSubclass);

                    if (s_hProgressThreadHook != IntPtr.Zero)
                    {
                        User32.UnhookWindowsHookEx(s_hProgressThreadHook);
                        s_hProgressThreadHook = IntPtr.Zero;
                        s_progressThreadHookDelegate = null;
                    }
                    break;
            }

            return DefSubclassProc(hwnd, uMsg, wParam, lParam);
        }

        private static void EnableForTLW(IntPtr hwnd, bool dark = true)
        {
            int use = dark ? 1 : 0;
            int hr = DwmSetWindowAttribute(hwnd, DWMWINDOWATTRIBUTE.USE_IMMERSIVE_DARK_MODE, ref use, sizeof(int));
            if (hr < 0) DwmSetWindowAttribute(hwnd, DWMWINDOWATTRIBUTE.USE_IMMERSIVE_DARK_MODE_BEFORE_20H1, ref use, sizeof(int));
        }

        private static void SetWindowTheme(IntPtr hwnd, string theme)
        {
            if (hwnd != IntPtr.Zero && !string.IsNullOrEmpty(theme)) UxTheme.SetWindowTheme(hwnd, theme, null);
        }

        /// <summary>
        /// Manipulates the visual style of a progress dialog window to apply dark mode theming. This method checks for the presence of native dark theme support and applies appropriate window themes and subclass procedures to ensure that the progress dialog and its child controls are rendered correctly in dark mode.
        /// </summary>
        /// <param name="hwndPD"></param>
        public static void DarkenProgressDialog(IntPtr hwndPD)
        {
            if (hwndPD == IntPtr.Zero || !Program.Style.DarkMode) return;

            bool hasNativeTheme = IsDarkThemeActive("DarkMode_Explorer::TaskDialog", "TaskDialog") || IsDarkThemeActive("DarkMode_DarkTheme::TaskDialog", "TaskDialog");
            bool hasCopyEngine = IsDarkThemeActive("DarkMode_CopyEngine::Progress", "Progress");

            // Frame wrapper framing dark
            SetWindowTheme(hwndPD, "DarkMode_Explorer");

            // GET THE BACKGROUND THREAD ID MANAGING THE PROGRESS DIALOG
            uint progressThreadId = User32.GetWindowThreadProcessId(hwndPD, out _);

            if (progressThreadId != 0 && s_hProgressThreadHook == IntPtr.Zero)
            {
                // Install a thread-context hook to intercept messages within the background thread
                s_progressThreadHookDelegate = (int nCode, IntPtr wParam, IntPtr lParam) =>
                {
                    if (nCode >= 0 && lParam != IntPtr.Zero)
                    {
                        // Structure mapping to capture window activation/creation events
                        var cwp = (User32.CWPSTRUCT)Marshal.PtrToStructure(lParam, typeof(User32.CWPSTRUCT));

                        // Check if message targets a valid window structure handle
                        if (cwp.hwnd != IntPtr.Zero)
                        {
                            System.Text.StringBuilder className = new(256);
                            User32.GetClassName(cwp.hwnd, className, className.Capacity);
                            string clsName = className.ToString();

                            if (clsName == "ProgressDialogUI" || clsName == "DirectUIHWND")
                            {
                                // We are now executing INSIDE the correct thread context! SetWindowSubclass will succeed.
                                if (!GetWindowSubclass(cwp.hwnd, s_progressDuiProc, kProgressDuiSubclassId, out _))
                                {
                                    bool success = SetWindowSubclass(cwp.hwnd, s_progressDuiProc, kProgressDuiSubclassId, IntPtr.Zero);
                                }
                            }
                        }
                    }
                    return User32.CallNextHookEx(s_hProgressThreadHook, nCode, wParam, lParam);
                };

                s_hProgressThreadHook = User32.SetWindowsHookEx(User32.WH_CALLWNDPROC, s_progressThreadHookDelegate, IntPtr.Zero, progressThreadId);
            }

            // Enumerate standard Win32 child controls that leak out to style their frames
            EnumChildWindows(hwndPD, delegate (IntPtr hwndChild, IntPtr lp)
            {
                NativeMethods.IUIAutomationElement el;
                try { el = ComUIAutomation.FromHandle(hwndChild); } catch { return true; }
                if (el == null) return true;

                string cls = ComUIAutomation.GetPropertyValueString(el, ComUIAutomation.UIA_ClassNamePropertyId);

                if (cls == "CCProgressBar")
                {
                    SetWindowTheme(hwndChild, hasCopyEngine ? "DarkMode_CopyEngine" : "DarkMode_Explorer");
                }
                else if (cls == "CCPushButton" || cls == "Button")
                {
                    SetWindowTheme(hwndChild, "DarkMode_Explorer");
                }

                return true;
            }, IntPtr.Zero);

            //// Force dark mode style for progressbar
            //SendMessage(hwndPD, WM_SYSCOLORCHANGE, IntPtr.Zero, IntPtr.Zero);
            //SendMessage(hwndPD, WM_THEMECHANGED, IntPtr.Zero, IntPtr.Zero);

            //EnumChildWindows(hwndPD, delegate (IntPtr hwndDuiChild, IntPtr lp)
            //{
            //    SendMessage(hwndDuiChild, WM_SYSCOLORCHANGE, IntPtr.Zero, IntPtr.Zero);
            //    SendMessage(hwndDuiChild, WM_THEMECHANGED, IntPtr.Zero, IntPtr.Zero);
            //    return true;
            //}, IntPtr.Zero);

            //RedrawWindow(hwndPD, IntPtr.Zero, IntPtr.Zero, RDW_INVALIDATE | RDW_UPDATENOW | RDW_ALLCHILDREN);
        }

        /// <summary>
        /// Manipulates the visual style of a TaskDialog window to apply dark mode theming. This method checks for the presence of native dark theme support and applies appropriate window themes and subclass procedures to ensure that the TaskDialog and its child controls are rendered correctly in dark mode.
        /// </summary>
        /// <param name="hwndTD"></param>
        /// <param name="pCfg"></param>
        public static void DarkenTaskDialog(IntPtr hwndTD, IntPtr pCfg)
        {
            s_hasNativeTheme = IsDarkThemeActive("DarkMode_Explorer::TaskDialog", "TaskDialog") || IsDarkThemeActive("DarkMode_DarkTheme::TaskDialog", "TaskDialog");

            bool dark = Program.Style.DarkMode;

            // If pCfg is zero, try to get it from the window
            if (pCfg == IntPtr.Zero)
            {
                // Try to get the stored config from the main window's subclass data
                if (GetWindowSubclass(hwndTD, s_mainProc, kMainSubclassId, out IntPtr existingConfig))
                {
                    pCfg = existingConfig;
                }
            }

            // Remove path
            if (!dark)
            {
                UxTheme.SetWindowTheme(hwndTD, null, null);

                EnumChildWindows(hwndTD, delegate (IntPtr hwndChild, IntPtr lp)
                {
                    if (GetWindowSubclass(hwndChild, s_directUiProc, kDirectUISubclassId, out IntPtr ex))
                    {
                        EnumChildWindows(hwndChild, delegate (IntPtr hwndDuiChild, IntPtr lp2)
                        {
                            UxTheme.SetWindowTheme(hwndDuiChild, null, null);
                            SendMessage(hwndDuiChild, WM_SYSCOLORCHANGE, IntPtr.Zero, IntPtr.Zero);
                            if (GetWindowSubclass(hwndDuiChild, s_ctColorProc, kCtlColorId, out IntPtr ex1))
                                RemoveWindowSubclass(hwndDuiChild, s_ctColorProc, kCtlColorId);
                            return true;
                        }, IntPtr.Zero);

                        UxTheme.SetWindowTheme(hwndChild, null, null);
                        RemoveWindowSubclass(hwndChild, s_directUiProc, kDirectUISubclassId);
                        DestroyState(hwndChild);
                    }
                    if (GetWindowSubclass(hwndChild, s_radioProc, kCtlColorId, out ex))
                        RemoveWindowSubclass(hwndChild, s_radioProc, kCtlColorId);
                    if (GetWindowSubclass(hwndChild, s_ctColorProc, kCtlColorId, out ex))
                        RemoveWindowSubclass(hwndChild, s_ctColorProc, kCtlColorId);

                    RedrawWindow(hwndChild, IntPtr.Zero, IntPtr.Zero, RDW_INVALIDATE | RDW_UPDATENOW | RDW_ALLCHILDREN);
                    return true;
                }, IntPtr.Zero);

                if (!GetWindowSubclass(hwndTD, s_mainProc, kMainSubclassId, out IntPtr existing))
                    SetWindowSubclass(hwndTD, s_mainProc, kMainSubclassId, pCfg);

                EnableForTLW(hwndTD, false);
                return;
            }

            // Attach path
            bool found = false;

            EnumChildWindows(hwndTD, delegate (IntPtr hwndChild, IntPtr lp)
            {
                NativeMethods.IUIAutomationElement el;
                try
                {
                    el = ComUIAutomation.FromHandle(hwndChild);
                }
                catch
                {
                    return true;
                }
                if (el == null) return true;
             
                string cls = ComUIAutomation.GetPropertyValueString(el, ComUIAutomation.UIA_ClassNamePropertyId);

                // CCSysLink — footnote / content hyperlinks
                if (cls == "CCSysLink")
                {
                    IntPtr hLink = ComUIAutomation.GetNativeWindowHandle(el);
                    if (hLink != IntPtr.Zero)
                    {
                        IntPtr hParent = GetParent(hLink);
                        if (hParent != IntPtr.Zero)
                        {
                            string id = ComUIAutomation.GetPropertyValueString(el, ComUIAutomation.UIA_AutomationIdPropertyId);

                            bool isFootnote = id == "FootnoteTextLink" || id == "ExpandedFooterTextLink" || id.Contains("Footnote") || id.Contains("ExpandedFooter");

                            int bg = isFootnote && !s_hasNativeTheme ? DarkColors.kFootnote : DarkColors.kPrimary;

                            if (id == "ContentLink")
                                bg = s_hasNativeTheme ? DarkColors.kFootnote : DarkColors.kPrimary;

                            if (!GetWindowSubclass(hParent, s_ctColorProc, kCtlColorId, out IntPtr ex))
                                SetWindowSubclass(hParent, s_ctColorProc, kCtlColorId, CreateSolidBrush(bg));
                        }
                    }
                    return true;
                }

                // "TaskDialog" — the DirectUI TaskPage window
                if (cls != "TaskDialog") return true;

                IntPtr hDUI = ComUIAutomation.GetNativeWindowHandle(el);
                if (hDUI == IntPtr.Zero) return true;

                // Class background brush
                {
                    IntPtr nb = CreateSolidBrush(DarkColors.kSecondary);
                    IntPtr ob = SetClassLongPtr(hDUI, GCLP_HBRBACKGROUND, nb);
                    if (ob != IntPtr.Zero && ob != GetSysColorBrush(COLOR_WINDOW) && ob != GetSysColorBrush(COLOR_BTNFACE)) DeleteObject(ob);
                }

                // Walk TaskPage UIA children using ComUIAutomation
                foreach (NativeMethods.IUIAutomationElement child in ComUIAutomation.GetContentChildren(el))
                {
                    int controlType = ComUIAutomation.GetPropertyValueInt(child, ComUIAutomation.UIA_ControlTypePropertyId);

                    if (controlType == ComUIAutomation.UIA_ButtonControlTypeId || controlType == ComUIAutomation.UIA_RadioButtonControlTypeId || controlType == ComUIAutomation.UIA_ProgressBarControlTypeId || controlType == ComUIAutomation.UIA_HyperlinkControlTypeId || controlType == ComUIAutomation.UIA_ScrollBarControlTypeId || controlType == ComUIAutomation.UIA_PaneControlTypeId)
                    {
                        IntPtr hBtn = ComUIAutomation.GetNativeWindowHandle(child);
                        if (hBtn != IntPtr.Zero)
                        {
                            string id = ComUIAutomation.GetPropertyValueString(child, ComUIAutomation.UIA_AutomationIdPropertyId);
                            IntPtr hP = GetParent(hBtn);

                            if (controlType == ComUIAutomation.UIA_ProgressBarControlTypeId)
                            {
                                bool hasCopyEngine = IsDarkThemeActive("DarkMode_CopyEngine::Progress", "Progress");
                                SetWindowTheme(hBtn, hasCopyEngine ? "DarkMode_CopyEngine" : "DarkMode_Explorer");
                            }
                            else if (controlType == ComUIAutomation.UIA_RadioButtonControlTypeId || id.StartsWith("RadioButton_") || controlType == ComUIAutomation.UIA_HyperlinkControlTypeId)
                            {
                                IntPtr ex;
                                bool hasDarkTheme = IsDarkThemeActive("DarkMode_DarkTheme::TaskDialog", "TaskDialog");
                                if (hasDarkTheme)
                                {
                                    SetWindowTheme(hBtn, "DarkMode_DarkTheme");
                                }
                                else
                                {
                                    if (!GetWindowSubclass(hBtn, s_radioProc, kCtlColorId, out ex))
                                        SetWindowSubclass(hBtn, s_radioProc, kCtlColorId, CreateSolidBrush(s_hasNativeTheme ? DarkColors.kSecondary : DarkColors.kPrimary));
                                }

                                if (hP != IntPtr.Zero && !GetWindowSubclass(hP, s_ctColorProc, kCtlColorId, out ex))
                                    SetWindowSubclass(hP, s_ctColorProc, kCtlColorId, CreateSolidBrush(s_hasNativeTheme ? DarkColors.kSecondary : DarkColors.kPrimary));
                            }
                            else if (id.StartsWith("CommandLink_"))
                            {
                                SetWindowTheme(hBtn, "DarkMode_Explorer");
                                if (hP != IntPtr.Zero && !GetWindowSubclass(hP, s_ctColorProc, kCtlColorId, out IntPtr ex))
                                    SetWindowSubclass(hP, s_ctColorProc, kCtlColorId, CreateSolidBrush(s_hasNativeTheme ? DarkColors.kSecondary : DarkColors.kPrimary));
                            }
                            else if (id.StartsWith("CommandButton_"))
                            {
                                SetWindowTheme(hBtn, "DarkMode_Explorer");
                                if (hP != IntPtr.Zero && !GetWindowSubclass(hP, s_ctColorProc, kCtlColorId, out IntPtr ex))
                                    SetWindowSubclass(hP, s_ctColorProc, kCtlColorId, CreateSolidBrush(DarkColors.kSecondary));
                            }
                            else
                            {
                                SetWindowTheme(hBtn, "DarkMode_Explorer");
                            }
                        }
                    }
                }

                // Window theme for TaskPage — must be set after children.
                SetWindowTheme(hDUI, "DarkMode_Explorer");

                // Store state and attach DirectUI subclass (idempotent)
                {
                    DirectUIState s = GetState(hDUI);
                    s.pCfg = pCfg;

                    TaskDialogConfigView cfgView = pCfg != IntPtr.Zero ? TaskDialogConfigView.FromPointer(pCfg) : null;
                    s.defExpanded = cfgView != null && (cfgView.dwFlags & TaskDialogConfigView.TDF_EXPANDED_BY_DEFAULT) != 0;
                    s.defChecked = cfgView != null && (cfgView.dwFlags & TaskDialogConfigView.TDF_VERIFICATION_FLAG_CHECKED) != 0;

                    s.isExpanded = s.defExpanded;
                    s.isChecked = s.defChecked;

                    s.elemsOk = false;
                    RefreshElements(hDUI, s);

                    if (!GetWindowSubclass(hDUI, s_directUiProc, kDirectUISubclassId, out IntPtr ex))
                        SetWindowSubclass(hDUI, s_directUiProc, kDirectUISubclassId, pCfg);
                }

                found = true;
                return true;
            }, IntPtr.Zero);

            if (found)
            {
                if (s_hasNativeTheme) SetWindowTheme(hwndTD, "DarkMode_Explorer");

                EnableForTLW(hwndTD);

                IntPtr existing;
                if (!GetWindowSubclass(hwndTD, s_mainProc, kMainSubclassId, out existing))
                    SetWindowSubclass(hwndTD, s_mainProc, kMainSubclassId, pCfg);

                EnumChildWindows(hwndTD, delegate (IntPtr hwndDuiChild, IntPtr lp)
                {
                    SendMessage(hwndDuiChild, WM_SYSCOLORCHANGE, IntPtr.Zero, IntPtr.Zero);
                    return true;
                }, IntPtr.Zero);

                SendMessage(hwndTD, WM_THEMECHANGED, IntPtr.Zero, IntPtr.Zero);
            }
        }

        /// <summary>
        /// RemoveFromTaskDialog
        /// </summary>
        /// <param name="hwndTD"></param>
        public static void RemoveFromTaskDialog(IntPtr hwndTD)
        {
            RemoveWindowSubclass(hwndTD, s_mainProc, kMainSubclassId);
            EnumChildWindows(hwndTD, delegate (IntPtr hwndChild, IntPtr lp)
            {
                if (GetWindowSubclass(hwndChild, s_directUiProc, kDirectUISubclassId, out IntPtr ex))
                {
                    RemoveWindowSubclass(hwndChild, s_directUiProc, kDirectUISubclassId);
                    DestroyState(hwndChild);
                }
                if (GetWindowSubclass(hwndChild, s_ctColorProc, kCtlColorId, out ex)) RemoveWindowSubclass(hwndChild, s_ctColorProc, kCtlColorId);
                UxTheme.SetWindowTheme(hwndChild, null, null);
                return true;
            }, IntPtr.Zero);
        }
    }
}