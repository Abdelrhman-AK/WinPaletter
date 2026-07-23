using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using WinPaletter.NativeMethods;
using static WinPaletter.NativeMethods.Comctl32;
using static WinPaletter.NativeMethods.DWMAPI;
using static WinPaletter.NativeMethods.GDI32;
using static WinPaletter.NativeMethods.Shell32;
using static WinPaletter.NativeMethods.User32;
using static WinPaletter.NativeMethods.UxTheme;

namespace WinPaletter.UI.Dark
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

    /// <summary>
    /// Per-DirectUI-window state
    /// </summary>
    internal sealed class DirectUIState
    {
        public IntPtr hTD = IntPtr.Zero;
        public IntPtr hTDS = IntPtr.Zero;
        public IntPtr hButton = IntPtr.Zero;
        public bool isDarkTheme;
        public bool themesOk;

        public IntPtr brPrimary = IntPtr.Zero;
        public IntPtr brSecondary = IntPtr.Zero;
        public IntPtr brFootnote = IntPtr.Zero;

        public List<ComUIAutomation.UIAElementInfo> elements = new();
        public bool elemsOk;

        public bool tracking;
        public bool pressing;
        public int hotIdx = -1;

        public bool isExpanded;
        public bool isChecked;
        public bool defExpanded;
        public bool defChecked;
        public bool _toggleHandled; // Flag to prevent double-toggling

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
        public const int TDLG_PRIMARYPANEL = 1;
        public const int TDLG_SECONDARYPANEL = 8;
        public const int TDLG_MAININSTRUCTIONPANE = 5;
        public const int TDLG_CONTENTPANE = 7;
        public const int TDLG_EXPANDOTEXT = 12;
        public const int TDLG_EXPANDOBUTTON = 13;
        public const int TDLG_VERIFICATIONTEXT = 14;
        public const int TDLG_FOOTNOTEPANE = 15;
        public const int TDLG_FOOTNOTESEPARATOR = 16;
        public const int TDLG_EXPANDEDFOOTERAREA = 18;
        public const int TDLG_RADIOBUTTONPANE = 20;
        public const int TDLG_EXPINFOPANE = 9;

        public const int TDLGEBS_NORMAL = 1;
        public const int TDLGEBS_HOVER = 2;
        public const int TDLGEBS_PRESSED = 3;
        public const int TDLGEBS_EXPANDEDNORMAL = 4;
        public const int TDLGEBS_EXPANDEDHOVER = 5;
        public const int TDLGEBS_EXPANDEDPRESSED = 6;

        public const int BP_CHECKBOX = 3;
        public const int CBS_UNCHECKEDNORMAL = 1;
        public const int CBS_UNCHECKEDHOT = 2;
        public const int CBS_UNCHECKEDPRESSED = 3;
        public const int CBS_CHECKEDNORMAL = 5;
        public const int CBS_CHECKEDHOT = 6;
        public const int CBS_CHECKEDPRESSED = 7;

        public const int BP_RADIOBUTTON = 2;
        public const int RBS_UNCHECKEDNORMAL = 1;

        public const int TMT_TEXTCOLOR = 3803;
        public const int TMT_FILLCOLOR = 3802;
        public const int TMT_CONTENTMARGINS = 3602;
        public const int TMT_FONT = 210;
        public const int TS_TRUE = 1;
        public const int TS_DRAW = 1;
    }

    public static class DarkDirectUI
    {
        private const int STATE_SYSTEM_CHECKED = 0x00000010;
        public const uint BS_SOLID = 0;

        private static bool s_hasNativeTheme;

        private static readonly UIntPtr kMainSubclassId = new(0xDEADBEEF);
        private static readonly UIntPtr kDirectUISubclassId = new(0xBADF00D);
        private static readonly UIntPtr kCtlColorId = new(0xC0FFEE01);
        private static readonly UIntPtr kProgressDuiSubclassId = new(0xDEADBEF2);

        private static readonly Dictionary<IntPtr, DirectUIState> s_states = [];

        private static readonly SUBCLASSPROC s_directUiProc = DirectUISubclassProc;
        private static readonly SUBCLASSPROC s_ctColorProc = WmCtColorSubclassProc;
        private static readonly SUBCLASSPROC s_radioProc = RadioSubclassProc;
        private static readonly SUBCLASSPROC s_mainProc = TaskDialogMainSubclassProc;

        private delegate bool WNDENUMPROC(IntPtr hWnd, IntPtr lParam);
        private static readonly SUBCLASSPROC s_progressDuiProc = ProgressDuiSubclassProc;
        private static readonly Dictionary<uint, IntPtr> s_progressThreadHooks = [];
        private static readonly Dictionary<uint, User32.HookProc> s_progressThreadHookDelegates = [];
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

        private static void RefreshElements(IntPtr hwnd, DirectUIState s, bool preserveManualState = true)
        {
            // Store manual state if we want to preserve it
            bool manualExpanded = s.isExpanded;
            bool manualChecked = s.isChecked;

            ComUIAutomation.QueryElements(hwnd, s.elements);

            bool foundCheckbox = false;
            bool foundExpando = false;

            foreach (ComUIAutomation.UIAElementInfo el in s.elements)
            {
                if (el.automationId == "VerificationCheckBox")
                {
                    foundCheckbox = true;
                    if (!preserveManualState) s.isChecked = (el.toggleState == ComUIAutomation.ToggleState.On);
                }
                else if (el.automationId == "ExpandoButton")
                {
                    foundExpando = true;
                    if (!preserveManualState) s.isExpanded = el.expandCollapseState is ComUIAutomation.ExpandCollapseState.Expanded or ComUIAutomation.ExpandCollapseState.PartiallyExpanded;
                }
            }

            // Only apply defaults if we're not preserving manual state
            if (!preserveManualState)
            {
                if (s.defChecked)
                {
                    s.isChecked = true;
                }
                if (s.defExpanded)
                {
                    s.isExpanded = true;
                }
            }
            else
            {
                // Restore manual state
                s.isExpanded = manualExpanded;
                s.isChecked = manualChecked;
            }

            s.elemsOk = true;
        }

        private static int HitTest(List<ComUIAutomation.UIAElementInfo> els, POINT pt)
        {
            for (int i = 0; i < els.Count; i++)
            {
                if (PtInRect(ref els[i].rect, pt)) return i;
            }
            return -1;
        }

        private struct SwapRule
        {
            public byte sR, sG, sB, dR, dG, dB;
        }

        private static unsafe void PixelSwap(IntPtr pxPtr, int rw, int w, int h, SwapRule[] rules, RECT? excludeRect = null)
        {
            byte* basePtr = (byte*)pxPtr.ToPointer();

            for (int y = 0; y < h; y++)
            {
                byte* row = basePtr + (y * rw * 4);
                for (int x = 0; x < w; x++)
                {
                    if (excludeRect.HasValue)
                    {
                        RECT ex = excludeRect.Value;
                        if (x >= ex.left && x < ex.right && y >= ex.top && y < ex.bottom) continue;
                    }

                    byte* p = row + (x * 4);
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

        private static void PaintDirectUI(IntPtr hwnd, IntPtr hdcWin, DirectUIState s)
        {
            if (!s.themesOk) RefreshThemes(hwnd, s);
            EnsureBrushes(s);

            GetClientRect(hwnd, out RECT rc);
            RECT rcForBuffer = rc;
            IntPtr hbp = BeginBufferedPaint(hdcWin, ref rcForBuffer, BPBF_TOPDOWNDIB, IntPtr.Zero, out IntPtr hdcBuf);
            if (hbp == IntPtr.Zero)
            {
                DefSubclassProc(hwnd, (int)User32.WindowsMessage.PrintClient, hdcWin, (IntPtr)PRF_CLIENT);
                return;
            }

            // Let comctl32 render the light-mode dialog into the offscreen buffer first.
            DefSubclassProc(hwnd, (int)User32.WindowsMessage.PrintClient, hdcBuf, (IntPtr)PRF_CLIENT);

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

            // Icon overdraw
            if (s.pCfg != IntPtr.Zero && !s_hasNativeTheme)
            {
                TaskDialogConfigView cfgView = TaskDialogConfigView.FromPointer(s.pCfg);
                foreach (ComUIAutomation.UIAElementInfo el in s.elements)
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

            // Glyph overdraw for ExpandoButton and VerificationCheckBox
            if (s.hTD != IntPtr.Zero || s.hButton != IntPtr.Zero)
            {
                for (int i = 0; i < s.elements.Count; i++)
                {
                    ComUIAutomation.UIAElementInfo el = s.elements[i];
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

            // Text overdraw
            {
                IntPtr hThm = s.hTD;

                foreach (ComUIAutomation.UIAElementInfo el in s.elements)
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
                    IntPtr hFont = CreateFontIndirect(lf);
                    IntPtr oldFont = hFont != IntPtr.Zero ? SelectObject(hdcBuf, hFont) : IntPtr.Zero;

                    if (!s_hasNativeTheme) FillRect(hdcBuf, ref rcText, brBg);

                    bool eligibleForPathCompaction =
                        uiPart == TaskDialogParts.TDLG_MAININSTRUCTIONPANE ||
                        uiPart == TaskDialogParts.TDLG_CONTENTPANE ||
                        uiPart == TaskDialogParts.TDLG_EXPINFOPANE ||
                        uiPart == TaskDialogParts.TDLG_EXPANDEDFOOTERAREA ||
                        uiPart == TaskDialogParts.TDLG_FOOTNOTEPANE;

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

        private static IntPtr DirectUISubclassProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam, UIntPtr uId, IntPtr refData)
        {
            switch (msg)
            {
                case (uint)WindowsMessage.EraseBkgnd:
                    return (IntPtr)1;

                case (uint)WindowsMessage.Paint:
                    {
                        IntPtr hdc = BeginPaint(hwnd, out PAINTSTRUCT ps);
                        DirectUIState s = GetState(hwnd);
                        // Preserve the manual toggle state during paint
                        RefreshElements(hwnd, s, preserveManualState: true);
                        PaintDirectUI(hwnd, hdc, s);
                        EndPaint(hwnd, ref ps);
                        return IntPtr.Zero;
                    }

                case (uint)WindowsMessage.MouseMove:
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
                            // Preserve state on mouse move (just invalidate, don't refresh)
                            InvalidateRect(hwnd, IntPtr.Zero, false);
                        }
                        break;
                    }
                case (uint)WindowsMessage.MouseLeave:
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
                case (uint)WindowsMessage.LButtonDown:
                    {
                        DirectUIState s = GetState(hwnd);
                        s.pressing = true;
                        // Preserve state on mouse down
                        RefreshElements(hwnd, s, preserveManualState: true);
                        InvalidateRect(hwnd, IntPtr.Zero, false);
                        break;
                    }
                case (uint)WindowsMessage.LButtonUp:
                    {
                        DirectUIState s = GetState(hwnd);

                        if (s.pressing)
                        {
                            s.pressing = false;
                            s._toggleHandled = false; // Reset flag

                            int x = unchecked((short)((long)lParam & 0xFFFF));
                            int y = unchecked((short)(((long)lParam >> 16) & 0xFFFF));
                            POINT pt = new POINT { X = x, Y = y };
                            int hitIdx = HitTest(s.elements, pt);

                            bool toggled = false;
                            if (hitIdx >= 0)
                            {
                                string automationId = s.elements[hitIdx].automationId;

                                if (automationId == "ExpandoButton")
                                {
                                    s.isExpanded = !s.isExpanded;
                                    s._toggleHandled = true; // Mark that toggle is handled
                                    toggled = true;
                                    PostMessage(GetParent(hwnd), (uint)WindowsMessage.ExpandoButtonClicked, IntPtr.Zero, IntPtr.Zero);
                                }
                                else if (automationId == "VerificationCheckBox")
                                {
                                    s.isChecked = !s.isChecked;
                                    s._toggleHandled = true;
                                    toggled = true;
                                }
                            }

                            RefreshElements(hwnd, s, preserveManualState: toggled);

                            InvalidateRect(hwnd, IntPtr.Zero, false);
                            UpdateWindow(hwnd);
                        }

                        break;
                    }

                case (uint)WindowsMessage.Destroy:
                    DestroyState(hwnd);
                    RemoveWindowSubclass(hwnd, s_directUiProc, uId);
                    break;
            }

            IntPtr result = DefSubclassProc(hwnd, msg, wParam, lParam);

            return result;
        }

        private static IntPtr WmCtColorSubclassProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam, UIntPtr uId, IntPtr dwRef)
        {
            switch (msg)
            {
                case (uint)WindowsMessage.EraseBkgnd:
                    {
                        if (dwRef == IntPtr.Zero) break;
                        IntPtr hdc = wParam;
                        RECT rc;
                        GetClientRect(hwnd, out rc);
                        FillRect(hdc, ref rc, dwRef);
                        SetTextColor(hdc, DarkColors.kTextNormal);
                        return (IntPtr)1;
                    }
                case (uint)WindowsMessage.CtlColorMsgBox:
                case (uint)WindowsMessage.CtlColorEdit:
                case (uint)WindowsMessage.CtlColorListBox:
                case (uint)WindowsMessage.CtlColorBtn:
                case (uint)WindowsMessage.CtlColorDlg:
                case (uint)WindowsMessage.CtlColorScrollBar:
                case (uint)WindowsMessage.CtlColorStatic:
                    {
                        IntPtr hdc = wParam;
                        int bg = DarkColors.kSecondary;
                        if (dwRef != IntPtr.Zero)
                        {
                            LOGBRUSH lb = new();
                            GetObject(dwRef, Marshal.SizeOf(typeof(LOGBRUSH)), ref lb);
                            if (lb.lbStyle == BS_SOLID) bg = lb.lbColor;
                        }
                        GDI32.SetBkColor(hdc, bg);
                        SetTextColor(hdc, DarkColors.kTextNormal);
                        IntPtr br = dwRef != IntPtr.Zero ? dwRef : GetClassLongPtr(hwnd, GCLP_HBRBACKGROUND);
                        return br;
                    }
                case (uint)WindowsMessage.Destroy:
                    if (dwRef != IntPtr.Zero) DeleteObject(dwRef);
                    RemoveWindowSubclass(hwnd, s_ctColorProc, uId);
                    return DefSubclassProc(hwnd, msg, wParam, lParam);
            }
            return DefSubclassProc(hwnd, msg, wParam, lParam);
        }

        private static IntPtr RadioSubclassProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam, UIntPtr uId, IntPtr dwRef)
        {
            switch (msg)
            {
                case (uint)WindowsMessage.Paint:
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
                            DefSubclassProc(hwnd, (int)User32.WindowsMessage.PrintClient, hdcBuf, (IntPtr)PRF_CLIENT);
                        }

                        DefSubclassProc(hwnd, (int)User32.WindowsMessage.PrintClient, hdcBuf, (IntPtr)PRF_CLIENT);

                        System.Text.StringBuilder sb = new(256);
                        GetWindowTextW(hwnd, sb, sb.Capacity);
                        string text = sb.ToString();

                        SIZE gsize;
                        GetThemePartSize(hThemeBtn, hdcBuf, TaskDialogParts.BP_RADIOBUTTON, TaskDialogParts.RBS_UNCHECKEDNORMAL, IntPtr.Zero, TaskDialogParts.TS_TRUE, out gsize);

                        RECT rcText = new()
                        {
                            left = gsize.cx + 2,
                            top = 0,
                            right = rcClient.right,
                            bottom = rcClient.bottom
                        };

                        DTTOPTS dots = new()
                        {
                            dwSize = (uint)Marshal.SizeOf(typeof(DTTOPTS)),
                            dwFlags = DTT_COMPOSITED | DTT_TEXTCOLOR,
                            crText = DarkColors.kTextNormal
                        };

                        GetThemeFont(hTheme, hdcBuf, TaskDialogParts.TDLG_RADIOBUTTONPANE, 0, TaskDialogParts.TMT_FONT, out LOGFONT logFont);
                        IntPtr font = CreateFontIndirect(logFont);
                        IntPtr oldFont = SelectObject(hdcBuf, font);

                        DrawThemeTextEx(hTheme, hdcBuf, TaskDialogParts.TDLG_RADIOBUTTONPANE, 0, text, -1, GDI32.DT_LEFT | GDI32.DT_VCENTER | 0x00008000, ref rcText, ref dots);

                        CloseThemeData(hTheme);
                        CloseThemeData(hThemeBtn);
                        SelectObject(hdcBuf, oldFont);
                        DeleteObject(font);

                        if (hbp != IntPtr.Zero) EndBufferedPaint(hbp, true);

                        EndPaint(hwnd, ref ps);
                        return IntPtr.Zero;
                    }

                case (uint)WindowsMessage.Destroy:
                    RemoveWindowSubclass(hwnd, s_radioProc, uId);
                    return DefSubclassProc(hwnd, msg, wParam, lParam);
            }
            return DefSubclassProc(hwnd, msg, wParam, lParam);
        }

        private static IntPtr TaskDialogMainSubclassProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam, UIntPtr uId, IntPtr dwRef)
        {
            switch (msg)
            {
                case (uint)WindowsMessage.CtlColorDlg:
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

                case (uint)WindowsMessage.SettingChange:
                    if (Program.Style.DarkMode)
                    {
                        DarkenTaskDialog(hwnd, dwRef);
                    }
                    break;

                case (uint)WindowsMessage.Destroy:
                    RemoveFromTaskDialog(hwnd);
                    RemoveWindowSubclass(hwnd, s_mainProc, uId);
                    break;

                case (uint)WindowsMessage.ExpandoButtonClicked:
                    EnumChildWindows(hwnd, delegate (IntPtr hwndChild, IntPtr lp)
                    {
                        if (GetWindowSubclass(hwndChild, s_directUiProc, kDirectUISubclassId, out _))
                        {
                            DirectUIState s = GetState(hwndChild);

                            // Only toggle if the DirectUI handler didn't already handle it
                            if (!s._toggleHandled)
                            {
                                s.isExpanded = !s.isExpanded;
                            }
                            else
                            {
                                s._toggleHandled = false; // Reset for next time
                            }
                        }
                        return true;
                    }, IntPtr.Zero);

                    PostMessage(hwnd, WindowsMessage.RefreshDuiState, IntPtr.Zero, IntPtr.Zero);
                    break;

                case (uint)WindowsMessage.RefreshDuiState:
                    EnumChildWindows(hwnd, delegate (IntPtr hwndChild, IntPtr lp)
                    {
                        if (GetWindowSubclass(hwndChild, s_directUiProc, kDirectUISubclassId, out _))
                        {
                            DirectUIState s = GetState(hwndChild);

                            // Preserve the manual state during refresh
                            bool expectedExpanded = s.isExpanded;
                            bool expectedChecked = s.isChecked;

                            RefreshElements(hwndChild, s, preserveManualState: true);

                            // Restore our manual state if RefreshElements tried to override it
                            s.isExpanded = expectedExpanded;
                            s.isChecked = expectedChecked;

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
        /// <summary>
        /// Windows subclass procedure for the progress dialog's DirectUI window. Dispatches to the native-theme overlay fix on Windows 11 or to full manual dark-forcing on Windows 10,
        /// depending on the hasNativeTheme flag captured in dwRefData when this subclass was installed.
        /// </summary>
        private static IntPtr ProgressDuiSubclassProc(IntPtr hwnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData)
        {
            switch (uMsg)
            {
                case (int)User32.WindowsMessage.Paint:
                    if (Program.Style.DarkMode)
                    {
                        bool hasNativeTheme = dwRefData != IntPtr.Zero;

                        if (hasNativeTheme)
                        {
                            IntPtr res = DefSubclassProc(hwnd, uMsg, wParam, lParam);
                            OverlayProgressDialogLabel(hwnd);
                            return res;
                        }
                        else
                        {
                            return PaintProgressDialogForced(hwnd);
                        }
                    }
                    break;

                case (int)User32.WindowsMessage.Destroy:
                    RemoveWindowSubclass(hwnd, s_progressDuiProc, uIdSubclass);

                    uint destroyedThreadId = User32.GetWindowThreadProcessId(hwnd, out _);
                    if (destroyedThreadId != 0 && s_progressThreadHooks.TryGetValue(destroyedThreadId, out IntPtr hHookToRemove))
                    {
                        User32.UnhookWindowsHookEx(hHookToRemove);
                        s_progressThreadHooks.Remove(destroyedThreadId);
                        s_progressThreadHookDelegates.Remove(destroyedThreadId);
                    }
                    break;
            }

            return DefSubclassProc(hwnd, uMsg, wParam, lParam);
        }

        private static void OverlayProgressDialogLabel(IntPtr hwnd)
        {
            IntPtr hdcScreen = User32.GetDC(hwnd);
            if (hdcScreen == IntPtr.Zero) return;

            try
            {
                User32.GetWindowRect(hwnd, out UxTheme.RECT winRect);

                NativeMethods.IUIAutomationElement baseEl = ComUIAutomation.FromHandle(hwnd);
                if (baseEl == null) return;

                foreach (NativeMethods.IUIAutomationElement child in ComUIAutomation.GetContentChildren(baseEl))
                {
                    int controlType = ComUIAutomation.GetPropertyValueInt(child, ComUIAutomation.UIA_ControlTypePropertyId);
                    if (controlType != 50020) continue; // UIA_TextControlTypeId

                    string autoId = ComUIAutomation.GetPropertyValueString(child, ComUIAutomation.UIA_AutomationIdPropertyId);
                    if (string.IsNullOrEmpty(autoId) || !autoId.Equals("LocLabel1", StringComparison.OrdinalIgnoreCase)) continue;

                    string textValue = ComUIAutomation.GetPropertyValueString(child, ComUIAutomation.UIA_NamePropertyId);

                    if (string.IsNullOrWhiteSpace(textValue)) continue;

                    child.GetCurrentPropertyValue(ComUIAutomation.UIA_BoundingRectanglePropertyId, out object propValue);
                    double[] boundingBox = propValue as double[];
                    if (boundingBox == null || boundingBox.Length != 4) continue;

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

                    IntPtr bgBrush = CreateSolidBrush(DarkColors.kSecondary);
                    FillRect(hdcScreen, ref clientTextRect, bgBrush);
                    DeleteObject(bgBrush);

                    LOGFONT lf = GetThemedFont(TaskDialogParts.TDLG_CONTENTPANE);
                    IntPtr hFont = CreateFontIndirect(lf);
                    IntPtr oldFont = hFont != IntPtr.Zero ? SelectObject(hdcScreen, hFont) : IntPtr.Zero;

                    SetTextColor(hdcScreen, (uint)ColorTranslator.ToWin32(Color.White));
                    GDI32.SetBkMode(hdcScreen, 1); // TRANSPARENT

                    User32.DrawText(hdcScreen, textValue, textValue.Length, ref clientTextRect,
                        0x00000000 /* DT_LEFT */ | 0x00000010 /* DT_SINGLELINE */ | 0x00000004 /* DT_VCENTER */ | 0x00008000 /* DT_NOPREFIX */ | 0x00000020 /* DT_PATH_ELLIPSIS */);

                    if (oldFont != IntPtr.Zero) SelectObject(hdcScreen, oldFont);
                    if (hFont != IntPtr.Zero) DeleteObject(hFont);

                    break;
                }
            }
            catch (Exception ex)
            {
                Program.Log?.Debug($"[ProgressDialog Native Overlay Error]", ex);
            }
            finally
            {
                User32.ReleaseDC(hwnd, hdcScreen);
            }
        }

        private static IntPtr PaintProgressDialogForced(IntPtr hwnd)
        {
            IntPtr hdc = BeginPaint(hwnd, out PAINTSTRUCT ps);

            GetClientRect(hwnd, out RECT rc);
            RECT rcForBuffer = rc;
            IntPtr hbp = BeginBufferedPaint(hdc, ref rcForBuffer, BPBF_TOPDOWNDIB, IntPtr.Zero, out IntPtr hdcBuf);
            if (hbp == IntPtr.Zero)
            {
                DefSubclassProc(hwnd, (int)User32.WindowsMessage.PrintClient, hdc, (IntPtr)PRF_CLIENT);
                EndPaint(hwnd, ref ps);
                return IntPtr.Zero;
            }

            DefSubclassProc(hwnd, (int)User32.WindowsMessage.PrintClient, hdcBuf, (IntPtr)PRF_CLIENT);

            User32.GetWindowRect(hwnd, out UxTheme.RECT winRect);
            NativeMethods.IUIAutomationElement baseEl = null;
            try { baseEl = ComUIAutomation.FromHandle(hwnd); } catch { }

            // Find AVI rect to exclude
            RECT? aviRect = null;
            if (baseEl != null)
            {
                foreach (NativeMethods.IUIAutomationElement child in ComUIAutomation.GetContentChildren(baseEl))
                {
                    string cls = ComUIAutomation.GetPropertyValueString(child, ComUIAutomation.UIA_ClassNamePropertyId);
                    string id = ComUIAutomation.GetPropertyValueString(child, ComUIAutomation.UIA_AutomationIdPropertyId);

                    bool looksLikeAvi = (!string.IsNullOrEmpty(cls) && cls.Equals("SysAnimate32", StringComparison.OrdinalIgnoreCase))
                                      || (!string.IsNullOrEmpty(id) && id.IndexOf("Avi", StringComparison.OrdinalIgnoreCase) >= 0);

                    if (looksLikeAvi)
                    {
                        child.GetCurrentPropertyValue(ComUIAutomation.UIA_BoundingRectanglePropertyId, out object bb);
                        if (bb is double[] box && box.Length == 4)
                        {
                            aviRect = new RECT
                            {
                                left = (int)box[0] - winRect.left,
                                top = (int)box[1] - winRect.top,
                                right = (int)(box[0] + box[2]) - winRect.left,
                                bottom = (int)(box[1] + box[3]) - winRect.top
                            };
                        }
                        break;
                    }
                }
            }

            // Pixel-swap
            if (GetBufferedPaintBits(hbp, out IntPtr pPx, out int rw) == 0)
            {
                GetBufferedPaintTargetRect(hbp, out RECT rcBuf);
                int w = rcBuf.right - rcBuf.left;
                int h = rcBuf.bottom - rcBuf.top;

                SwapRule[] rules =
                [
                    new SwapRule { sR = 255, sG = 255, sB = 255, dR = DarkColors.kSecondary.R, dG = DarkColors.kSecondary.G, dB = DarkColors.kSecondary.B },
                    new SwapRule { sR = 240, sG = 240, sB = 240, dR = DarkColors.kSecondary.R, dG = DarkColors.kSecondary.G, dB = DarkColors.kSecondary.B },
                    new SwapRule { sR = 128, sG = 128, sB = 128, dR = DarkColors.kSeparator.R, dG = DarkColors.kSeparator.G, dB = DarkColors.kSeparator.B },
                    new SwapRule { sR = 223, sG = 223, sB = 223, dR = DarkColors.kSeparator.R, dG = DarkColors.kSeparator.G, dB = DarkColors.kSeparator.B },
                ];
                PixelSwap(pPx, rw, w, h, rules, aviRect);
            }

            // Overdraw LocLabel1
            if (baseEl != null)
            {
                try
                {
                    foreach (NativeMethods.IUIAutomationElement child in ComUIAutomation.GetContentChildren(baseEl))
                    {
                        int controlType = ComUIAutomation.GetPropertyValueInt(child, ComUIAutomation.UIA_ControlTypePropertyId);
                        if (controlType != 50020) continue;

                        string autoId = ComUIAutomation.GetPropertyValueString(child, ComUIAutomation.UIA_AutomationIdPropertyId);
                        if (string.IsNullOrEmpty(autoId) || !autoId.Equals("LocLabel1", StringComparison.OrdinalIgnoreCase)) continue;

                        string textValue = ComUIAutomation.GetPropertyValueString(child, ComUIAutomation.UIA_NamePropertyId);

                        if (string.IsNullOrWhiteSpace(textValue)) continue;

                        child.GetCurrentPropertyValue(ComUIAutomation.UIA_BoundingRectanglePropertyId, out object propValue);
                        if (propValue is not double[] boundingBox || boundingBox.Length != 4) continue;

                        RECT clientTextRect = new()
                        {
                            left = (int)boundingBox[0] - winRect.left,
                            top = (int)boundingBox[1] - winRect.top,
                            right = (int)(boundingBox[0] + boundingBox[2]) - winRect.left,
                            bottom = (int)(boundingBox[1] + boundingBox[3]) - winRect.top
                        };

                        IntPtr bgBrush = CreateSolidBrush(DarkColors.kSecondary);
                        FillRect(hdcBuf, ref clientTextRect, bgBrush);
                        DeleteObject(bgBrush);

                        LOGFONT lf = GetThemedFont(TaskDialogParts.TDLG_CONTENTPANE);
                        IntPtr hFont = CreateFontIndirect(lf);
                        IntPtr oldFont = hFont != IntPtr.Zero ? SelectObject(hdcBuf, hFont) : IntPtr.Zero;

                        SetTextColor(hdcBuf, (uint)ColorTranslator.ToWin32(Color.White));
                        GDI32.SetBkMode(hdcBuf, 1); // TRANSPARENT

                        User32.DrawText(hdcBuf, textValue, textValue.Length, ref clientTextRect,
                            0x00000000 /* DT_LEFT */ | 0x00000004 /* DT_VCENTER */ | 0x00000010 /* DT_SINGLELINE */ | 0x00008000 /* DT_NOPREFIX */ | 0x00000020 /* DT_PATH_ELLIPSIS */);

                        if (oldFont != IntPtr.Zero) SelectObject(hdcBuf, oldFont);
                        if (hFont != IntPtr.Zero) DeleteObject(hFont);

                        break; // only one LocLabel1 exists
                    }
                }
                catch (Exception ex)
                {
                    Program.Log?.Debug($"[ProgressDialog Forced-Dark Overlay Error]", ex);
                }
            }

            EndBufferedPaint(hbp, true);
            EndPaint(hwnd, ref ps);
            return IntPtr.Zero;
        }

        private static void EnableForTLW(IntPtr hwnd, bool dark = true)
        {
            int use = dark ? 1 : 0;

            int hr = DwmSetWindowAttribute(hwnd, DWMWINDOWATTRIBUTE.USE_IMMERSIVE_DARK_MODE, ref use, sizeof(int));
            if (hr < 0)
            {
                hr = DwmSetWindowAttribute(hwnd, DWMWINDOWATTRIBUTE.USE_IMMERSIVE_DARK_MODE_BEFORE_20H1, ref use, sizeof(int));
            }
        }

        private static void SetWindowTheme(IntPtr hwnd, string theme)
        {
            if (hwnd != IntPtr.Zero && !string.IsNullOrEmpty(theme))
            {
                UxTheme.SetWindowTheme(hwnd, theme, null);
            }
        }

        /// <summary>
        /// Manipulates the visual style of a progress dialog window to apply dark mode theming.
        /// </summary>
        public static void DarkenProgressDialog(IntPtr hwndPD)
        {
            if (!Program.Style.DarkMode) return;
            if (OS.WXP || OS.WVista || OS.W7 || OS.W8x) return;
            if (hwndPD == IntPtr.Zero) return;

            // Clean up any existing state for this window first
            CleanupProgressDialog(hwndPD);

            bool hasNativeTheme = IsDarkThemeActive("DarkMode_Explorer::TaskDialog", "TaskDialog") || IsDarkThemeActive("DarkMode_DarkTheme::TaskDialog", "TaskDialog");
            bool hasCopyEngine = IsDarkThemeActive("DarkMode_CopyEngine::Progress", "Progress");

            // Frame wrapper framing dark
            SetWindowTheme(hwndPD, "DarkMode_Explorer");

            // GET THE BACKGROUND THREAD ID MANAGING THE PROGRESS DIALOG
            uint progressThreadId = User32.GetWindowThreadProcessId(hwndPD, out _);

            if (progressThreadId != 0 && !s_progressThreadHooks.ContainsKey(progressThreadId))
            {
                User32.HookProc hookDelegate = null;
                hookDelegate = (int nCode, IntPtr wParam, IntPtr lParam) =>
                {
                    if (nCode >= 0 && lParam != IntPtr.Zero)
                    {
                        var cwp = (User32.CWPSTRUCT)Marshal.PtrToStructure(lParam, typeof(User32.CWPSTRUCT));

                        if (cwp.hwnd != IntPtr.Zero && User32.IsChild(hwndPD, cwp.hwnd))
                        {
                            System.Text.StringBuilder className = new(256);
                            User32.GetClassName(cwp.hwnd, className, className.Capacity);
                            string clsName = className.ToString();

                            if (clsName == "ProgressDialogUI" || clsName == "DirectUIHWND")
                            {
                                if (!GetWindowSubclass(cwp.hwnd, s_progressDuiProc, kProgressDuiSubclassId, out _))
                                {
                                    SetWindowSubclass(cwp.hwnd, s_progressDuiProc, kProgressDuiSubclassId, hasNativeTheme ? (IntPtr)1 : IntPtr.Zero);
                                }
                            }
                        }
                    }
                    IntPtr selfHook = s_progressThreadHooks.TryGetValue(progressThreadId, out IntPtr h) ? h : IntPtr.Zero;
                    return User32.CallNextHookEx(selfHook, nCode, wParam, lParam);
                };

                IntPtr hHook = User32.SetWindowsHookEx(User32.WH_CALLWNDPROC, hookDelegate, IntPtr.Zero, progressThreadId);

                if (hHook != IntPtr.Zero)
                {
                    s_progressThreadHookDelegates[progressThreadId] = hookDelegate;
                    s_progressThreadHooks[progressThreadId] = hHook;
                }
            }

            // Fallback: subclass DirectUI child directly if it already exists
            EnumChildWindows(hwndPD, delegate (IntPtr hwndChild, IntPtr lp)
            {
                System.Text.StringBuilder cn = new(256);
                User32.GetClassName(hwndChild, cn, cn.Capacity);
                string cls = cn.ToString();

                if ((cls == "ProgressDialogUI" || cls == "DirectUIHWND") && !GetWindowSubclass(hwndChild, s_progressDuiProc, kProgressDuiSubclassId, out _))
                {
                    SetWindowSubclass(hwndChild, s_progressDuiProc, kProgressDuiSubclassId, hasNativeTheme ? (IntPtr)1 : IntPtr.Zero);
                }
                return true;
            }, IntPtr.Zero);

            // Fallback: subclass DirectUI child directly if it already exists
            EnumChildWindows(hwndPD, delegate (IntPtr hwndChild, IntPtr lp)
            {
                NativeMethods.IUIAutomationElement el;
                try { el = ComUIAutomation.FromHandle(hwndChild); } catch { return true; }
                if (el == null) return true;

                string cls = ComUIAutomation.GetPropertyValueString(el, ComUIAutomation.UIA_ClassNamePropertyId);

                if (cls == "CCProgressBar")
                {
                    string theme = hasCopyEngine ? "DarkMode_CopyEngine" : "DarkMode_Explorer";
                    SetWindowTheme(hwndChild, theme);
                }
                else if (cls == "CCPushButton" || cls == "Button")
                {
                    SetWindowTheme(hwndChild, "DarkMode_Explorer");
                }

                return true;
            }, IntPtr.Zero);

            // Force dark mode style
            SendMessage(hwndPD, (int)User32.WindowsMessage.SysColorChange, IntPtr.Zero, IntPtr.Zero);
            SendMessage(hwndPD, (int)User32.WindowsMessage.ThemeChanged, IntPtr.Zero, IntPtr.Zero);
        }

        /// <summary>
        /// Manipulates the visual style of a TaskDialog window to apply dark mode theming.
        /// </summary>
        public static void DarkenTaskDialog(IntPtr hwndTD, IntPtr pCfg)
        {
            if (!Program.Style.DarkMode) return;
            if (OS.WXP || OS.WVista || OS.W7 || OS.W8x) return;
            if (hwndTD == IntPtr.Zero) return;

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

            // Remove path first to ensure clean state
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
                            SendMessage(hwndDuiChild, WindowsMessage.SysColorChange, IntPtr.Zero, IntPtr.Zero);
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

                    RedrawWindow(hwndChild, IntPtr.Zero, IntPtr.Zero, RedrawWindowFlags.Invalidate | RedrawWindowFlags.UpdateNow | RedrawWindowFlags.AllChildren);
                    return true;
                }, IntPtr.Zero);

                if (!GetWindowSubclass(hwndTD, s_mainProc, kMainSubclassId, out IntPtr existing))
                    SetWindowSubclass(hwndTD, s_mainProc, kMainSubclassId, pCfg);

                EnableForTLW(hwndTD, false);
                return;
            }

            // Clear any existing state for this window before applying dark mode
            if (s_states.ContainsKey(hwndTD))
            {
                s_states.Remove(hwndTD);
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
                            if (id == "ContentLink") bg = s_hasNativeTheme ? DarkColors.kFootnote : DarkColors.kPrimary;

                            if (!GetWindowSubclass(hParent, s_ctColorProc, kCtlColorId, out IntPtr ex))
                            {
                                SetWindowSubclass(hParent, s_ctColorProc, kCtlColorId, CreateSolidBrush(bg));
                            }
                        }
                    }
                    return true;
                }

                // DirectUI
                if (cls != "TaskDialog" && cls != "DirectUIHWND") return true;

                IntPtr hDUI = ComUIAutomation.GetNativeWindowHandle(el);
                if (hDUI == IntPtr.Zero) return true;

                // Remove existing subclassing
                if (GetWindowSubclass(hDUI, s_directUiProc, kDirectUISubclassId, out _))
                {
                    RemoveWindowSubclass(hDUI, s_directUiProc, kDirectUISubclassId);
                }

                // Clear existing state for this DUI window
                if (s_states.ContainsKey(hDUI))
                {
                    s_states.Remove(hDUI);
                }

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
                    string id = ComUIAutomation.GetPropertyValueString(child, ComUIAutomation.UIA_AutomationIdPropertyId);

                    if (controlType == ComUIAutomation.UIA_ButtonControlTypeId ||
                        controlType == ComUIAutomation.UIA_RadioButtonControlTypeId ||
                        controlType == ComUIAutomation.UIA_ProgressBarControlTypeId ||
                        controlType == ComUIAutomation.UIA_HyperlinkControlTypeId ||
                        controlType == ComUIAutomation.UIA_ScrollBarControlTypeId ||
                        controlType == ComUIAutomation.UIA_PaneControlTypeId)
                    {
                        IntPtr hBtn = ComUIAutomation.GetNativeWindowHandle(child);
                        if (hBtn != IntPtr.Zero)
                        {
                            IntPtr hP = GetParent(hBtn);

                            if (controlType == ComUIAutomation.UIA_ProgressBarControlTypeId)
                            {
                                bool hasCopyEngine = IsDarkThemeActive("DarkMode_CopyEngine::Progress", "Progress");
                                string theme = hasCopyEngine ? "DarkMode_CopyEngine" : "DarkMode_Explorer";
                                SetWindowTheme(hBtn, theme);
                            }
                            else if (controlType == ComUIAutomation.UIA_RadioButtonControlTypeId || id.StartsWith("RadioButton_") || controlType == ComUIAutomation.UIA_HyperlinkControlTypeId)
                            {
                                bool hasDarkTheme = IsDarkThemeActive("DarkMode_DarkTheme::TaskDialog", "TaskDialog");
                                if (hasDarkTheme)
                                {
                                    SetWindowTheme(hBtn, "DarkMode_DarkTheme");
                                }
                                else
                                {
                                    if (!GetWindowSubclass(hBtn, s_radioProc, kCtlColorId, out IntPtr ex0))
                                    {
                                        SetWindowSubclass(hBtn, s_radioProc, kCtlColorId, CreateSolidBrush(s_hasNativeTheme ? DarkColors.kSecondary : DarkColors.kPrimary));
                                    }
                                }

                                if (hP != IntPtr.Zero && !GetWindowSubclass(hP, s_ctColorProc, kCtlColorId, out IntPtr ex1))
                                {
                                    SetWindowSubclass(hP, s_ctColorProc, kCtlColorId, CreateSolidBrush(s_hasNativeTheme ? DarkColors.kSecondary : DarkColors.kPrimary));
                                }
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

                // Store state and attach DirectUI subclass
                {
                    DirectUIState s = GetState(hDUI);
                    s.pCfg = pCfg;

                    TaskDialogConfigView cfgView = pCfg != IntPtr.Zero ? TaskDialogConfigView.FromPointer(pCfg) : null;
                    s.defExpanded = cfgView != null && (cfgView.dwFlags & TaskDialogConfigView.TDF_EXPANDED_BY_DEFAULT) != 0;
                    s.defChecked = cfgView != null && (cfgView.dwFlags & TaskDialogConfigView.TDF_VERIFICATION_FLAG_CHECKED) != 0;

                    s.isExpanded = s.defExpanded;
                    s.isChecked = s.defChecked;

                    s.elemsOk = false;
                    RefreshElements(hDUI, s, preserveManualState: false);

                    if (!GetWindowSubclass(hDUI, s_directUiProc, kDirectUISubclassId, out IntPtr ex))
                    {
                        SetWindowSubclass(hDUI, s_directUiProc, kDirectUISubclassId, pCfg);
                    }
                }

                found = true;
                return true;
            }, IntPtr.Zero);

            if (found)
            {
                if (s_hasNativeTheme) SetWindowTheme(hwndTD, "DarkMode_Explorer");

                EnableForTLW(hwndTD);

                if (!GetWindowSubclass(hwndTD, s_mainProc, kMainSubclassId, out IntPtr existing))
                {
                    SetWindowSubclass(hwndTD, s_mainProc, kMainSubclassId, pCfg);
                }

                EnumChildWindows(hwndTD, delegate (IntPtr hwndDuiChild, IntPtr lp)
                {
                    SendMessage(hwndDuiChild, WindowsMessage.SysColorChange, IntPtr.Zero, IntPtr.Zero);
                    return true;
                }, IntPtr.Zero);

                SendMessage(hwndTD, (int)User32.WindowsMessage.ThemeChanged, IntPtr.Zero, IntPtr.Zero);
            }
        }

        public static void CleanupProgressDialog(IntPtr hwndPD)
        {
            if (hwndPD != IntPtr.Zero)
            {
                RemoveWindowSubclass(hwndPD, s_progressDuiProc, kProgressDuiSubclassId);

                EnumChildWindows(hwndPD, delegate (IntPtr hwndChild, IntPtr lp)
                {
                    RemoveWindowSubclass(hwndChild, s_progressDuiProc, kProgressDuiSubclassId);
                    return true;
                }, IntPtr.Zero);

                uint threadId = User32.GetWindowThreadProcessId(hwndPD, out _);
                if (threadId != 0 && s_progressThreadHooks.TryGetValue(threadId, out IntPtr hHook))
                {
                    User32.UnhookWindowsHookEx(hHook);
                    s_progressThreadHooks.Remove(threadId);
                    if (s_progressThreadHookDelegates.ContainsKey(threadId))
                    {
                        s_progressThreadHookDelegates.Remove(threadId);
                    }
                }
            }
        }

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
                if (GetWindowSubclass(hwndChild, s_ctColorProc, kCtlColorId, out ex))
                    RemoveWindowSubclass(hwndChild, s_ctColorProc, kCtlColorId);
                UxTheme.SetWindowTheme(hwndChild, null, null);
                return true;
            }, IntPtr.Zero);

            if (s_states.ContainsKey(hwndTD))
            {
                s_states.Remove(hwndTD);
            }
        }
    }
}