using System.Runtime.InteropServices;

namespace WinPaletter
{

    public class Metrics
    {
        public struct NONCLIENTMETRICS           // NEVER CHANGE VARIABLES ORDERS
        {
            public int cbSize;
            public int iBorderWidth;
            public int iScrollWidth;
            public int iScrollHeight;
            public int iCaptionWidth;
            public int iCaptionHeight;
            public LogFont lfCaptionFont;
            public int iSMCaptionWidth;
            public int iSMCaptionHeight;
            public LogFont lfSMCaptionFont;
            public int iMenuWidth;
            public int iMenuHeight;
            public LogFont lfMenuFont;
            public LogFont lfStatusFont;
            public LogFont lfMessageFont;
            public int iPaddedBorderWidth;
        }

        public struct ICONMETRICS
        {
            public uint cbSize;
            public int iHorzSpacing;
            public int iVertSpacing;
            public int iTitleWrap;
            public LogFont lfFont;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ANIMATIONINFO
        {
            public uint cbSize;
            public int IMinAnimate;
        }
    }
}