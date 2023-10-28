using System;
using System.Runtime.InteropServices;

namespace WinPaletter.NativeMethods
{
    public class DWMAPI
    {
        [DllImport("DWMAPI.dll", EntryPoint = "#131", PreserveSig = false)]
        public static extern void DwmSetColorizationParameters(ref DWM_COLORIZATION_PARAMS parameters, bool unknown);

        [DllImport("DWMAPI.dll", PreserveSig = false)]
        private static extern bool DwmIsCompositionEnabled();

        public static bool IsCompositionEnabled()
        {
            try { return Environment.OSVersion.Version.Major >= 6 && DwmIsCompositionEnabled(); }
            catch { return false; }
        }

        [DllImport("DWMAPI")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("DWMAPI")]
        internal static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("DWMAPI.dll")]
        internal static extern int DwmSetWindowAttribute(IntPtr hwnd, DWMATTRIB dwAttribute, ref int pvAttribute, int cbAttribute);

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
        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
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

        public const int CS_DROPSHADOW = 0x20000;
        public const int WM_NCPAINT = 0x85;

    }
}
