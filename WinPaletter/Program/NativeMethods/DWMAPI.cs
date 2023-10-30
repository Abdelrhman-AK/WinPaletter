using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace WinPaletter.NativeMethods
{
    public class DWMAPI
    {
        #region Constants
        public const int CS_DROPSHADOW = 0x20000;
        public const int WM_NCPAINT = 0x85;
        public const int DWM_BB_BLURREGION = 2;
        public const int DWM_BB_ENABLE = 1;
        public const int DWM_BB_TRANSITIONONMAXIMIZED = 4;
        public const string DWM_COMPOSED_EVENT_BASE_NAME = "DwmComposedEvent_";
        public const string DWM_COMPOSED_EVENT_NAME_FORMAT = "%s%d";
        public const int DWM_COMPOSED_EVENT_NAME_MAX_LENGTH = 0x40;
        public const int DWM_FRAME_DURATION_DEFAULT = -1;
        public const int DWM_TNP_OPACITY = 4;
        public const int DWM_TNP_RECTDESTINATION = 1;
        public const int DWM_TNP_RECTSOURCE = 2;
        public const int DWM_TNP_SOURCECLIENTAREAONLY = 0x10;
        public const int DWM_TNP_VISIBLE = 8;
        public const int WM_DWMCOMPOSITIONCHANGED = 0x31e;
        #endregion

        #region Methods
        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmEnableBlurBehindWindow(IntPtr hWnd, DWM_BLURBEHIND pBlurBehind);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, MARGINS pMargins);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmEnableComposition(bool bEnable);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmGetColorizationColor(out int pcrColorization, [MarshalAs(UnmanagedType.Bool)] out bool pfOpaqueBlend);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern IntPtr DwmRegisterThumbnail(IntPtr dest, IntPtr source);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmUnregisterThumbnail(IntPtr hThumbnail);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmUpdateThumbnailProperties(IntPtr hThumbnail, DWM_THUMBNAIL_PROPERTIES props);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmQueryThumbnailSourceSize(IntPtr hThumbnail, out Size size);

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        internal static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        internal static extern int DwmSetWindowAttribute(IntPtr hwnd, DWMATTRIB dwAttribute, ref int pvAttribute, int cbAttribute);

        [DllImport("dwmapi.dll", EntryPoint = "#131", PreserveSig = false)]
        public static extern void DwmSetColorizationParameters(ref DWM_COLORIZATION_PARAMS parameters, bool unknown);

        [DllImport("dwmapi.dll")]
        public static extern int DwmDefWindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, out IntPtr result);

        [DllImport("dwmapi.dll")]
        public static extern int DwmEnableComposition(int fEnable);

        [DllImport("dwmapi.dll")]
        public static extern int DwmEnableMMCSS(int fEnableMMCSS);

        [DllImport("dwmapi.dll")]
        public static extern int DwmGetColorizationColor(ref int pcrColorization, ref int pfOpaqueBlend);

        [DllImport("dwmapi.dll")]
        public static extern int DwmGetCompositionTimingInfo(IntPtr hwnd, ref DWM_TIMING_INFO pTimingInfo);

        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, IntPtr pvAttribute, int cbAttribute);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        [DllImport("dwmapi.dll")]
        public static extern int DwmModifyPreviousDxFrameDuration(IntPtr hwnd, int cRefreshes, int fRelative);

        [DllImport("dwmapi.dll")]
        public static extern int DwmQueryThumbnailSourceSize(IntPtr hThumbnail, ref SIZE pSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmRegisterThumbnail(IntPtr hwndDestination, IntPtr hwndSource, ref SIZE pMinimizedSize, ref IntPtr phThumbnailId);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetDxFrameDuration(IntPtr hwnd, int cRefreshes);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetPresentParameters(IntPtr hwnd, ref DWM_PRESENT_PARAMETERS pPresentParams);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int dwAttribute, IntPtr pvAttribute, int cbAttribute);

        [DllImport("dwmapi.dll")]
        public static extern int DwmUpdateThumbnailProperties(IntPtr hThumbnailId, ref DWM_THUMBNAIL_PROPERTIES ptnProperties);

        public static bool IsCompositionEnabled()
        {
            try { return Environment.OSVersion.Version.Major >= 6 && DwmIsCompositionEnabled(); }
            catch { return false; }
        }
        #endregion

        #region Classes
        [StructLayout(LayoutKind.Sequential)]
        public class DWM_THUMBNAIL_PROPERTIES
        {
            public uint dwFlags;
            public RECT rcDestination;
            public RECT rcSource;
            public byte opacity;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fVisible;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fSourceClientAreaOnly;
            public const uint DWM_TNP_RECTDESTINATION = 0x00000001;
            public const uint DWM_TNP_RECTSOURCE = 0x00000002;
            public const uint DWM_TNP_OPACITY = 0x00000004;
            public const uint DWM_TNP_VISIBLE = 0x00000008;
            public const uint DWM_TNP_SOURCECLIENTAREAONLY = 0x00000010;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class DWM_BLURBEHIND
        {
            public uint dwFlags;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fEnable;
            public IntPtr hRegionBlur;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fTransitionOnMaximized;

            public const uint DWM_BB_ENABLE = 0x00000001;
            public const uint DWM_BB_BLURREGION = 0x00000002;
            public const uint DWM_BB_TRANSITIONONMAXIMIZED = 0x00000004;
        }
        #endregion

        #region Structures
        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct UNSIGNED_RATIO
        {
            public int uiNumerator;
            public int uiDenominator;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left, top, right, bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left; this.top = top;
                this.right = right; this.bottom = bottom;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SIZE
        {
            public int cx;
            public int cy;
        }

        public struct DWM_COLORIZATION_PARAMS
        {
            public uint clrColor;
            public uint clrAfterGlow;
            public uint nIntensity;
            public uint clrAfterGlowBalance;
            public uint clrBlurBalance;
            public uint clrGlassReflectionIntensity;
            public bool fOpaque;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DWM_PRESENT_PARAMETERS
        {
            public int cbSize;
            public int fQueue;
            public long cRefreshStart;
            public int cBuffer;
            public int fUseSourceRate;
            public UNSIGNED_RATIO rateSource;
            public int cRefreshesPerFrame;
            public DWM_SOURCE_FRAME_SAMPLING eSampling;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DWM_TIMING_INFO
        {
            public int cbSize;
            public UNSIGNED_RATIO rateRefresh;
            public UNSIGNED_RATIO rateCompose;
            public long qpcVBlank;
            public long cRefresh;
            public long qpcCompose;
            public long cFrame;
            public long cRefreshFrame;
            public long cRefreshConfirmed;
            public int cFlipsOutstanding;
            public long cFrameCurrent;
            public long cFramesAvailable;
            public long cFrameCleared;
            public long cFramesReceived;
            public long cFramesDisplayed;
            public long cFramesDropped;
            public long cFramesMissed;
        }
        #endregion

        #region Enumerations
        public enum DWMATTRIB : int
        {
            SYSTEMBACKDROP_TYPE = 38,
            MICA_EFFECT = 1029,
            USE_IMMERSIVE_DARK_MODE = 20,
            WINDOW_CORNER_PREFERENCE = 33,
            TEXT_COLOR = 36,
            CAPTION_COLOR = 35,
            BORDER_COLOR = 34
        }

        public enum FormCornersType
        {
            Default,
            Rectangular,
            Round,
            SmallRound
        }

        public enum DWM_SOURCE_FRAME_SAMPLING
        {
            DWM_SOURCE_FRAME_SAMPLING_POINT,
            DWM_SOURCE_FRAME_SAMPLING_COVERAGE,
            DWM_SOURCE_FRAME_SAMPLING_LAST
        }

        public enum DWMNCRENDERINGPOLICY
        {
            DWMNCRP_USEWINDOWSTYLE,
            DWMNCRP_DISABLED,
            DWMNCRP_ENABLED
        }

        public enum DWMWINDOWATTRIBUTE
        {
            DWMWA_ALLOW_NCPAINT = 4,
            DWMWA_CAPTION_BUTTON_BOUNDS = 5,
            DWMWA_FLIP3D_POLICY = 8,
            DWMWA_FORCE_ICONIC_REPRESENTATION = 7,
            DWMWA_LAST = 9,
            DWMWA_NCRENDERING_ENABLED = 1,
            DWMWA_NCRENDERING_POLICY = 2,
            DWMWA_NONCLIENT_RTL_LAYOUT = 6,
            DWMWA_TRANSITIONS_FORCEDISABLED = 3
        }
        #endregion
    }
}
