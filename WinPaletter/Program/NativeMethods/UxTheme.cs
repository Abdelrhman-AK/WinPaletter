using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace WinPaletter.NativeMethods
{
    public class UxTheme
    {
        [DllImport("UxTheme.DLL", BestFitMapping = false, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode, EntryPoint = "#65")]
        public static extern int SetSystemVisualStyle(string pszFilename, string pszColor, string pszSize, int dwReserved);

        [DllImport("uxtheme", ExactSpelling = true)]
        public static extern int EnableTheming(int fEnable);

        [DllImport("uxtheme", CharSet = CharSet.Unicode)]
        public static extern int GetCurrentThemeName(StringBuilder stringThemeName, int lengthThemeName, StringBuilder stringColorName, int lengthColorName, StringBuilder stringSizeName, int lengthSizeName);

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int DrawThemeTextEx(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, string text, int iCharCount, int dwFlags, ref RECT pRect, ref DttOpts pOptions);

        /// <summary>
        /// Set The Window's Theme Attributes
        /// </summary>
        /// <param name="hWnd">The Handle to the Window</param>
        /// <param name="wtype">What Type of Attributes</param>
        /// <param name="attributes">The Attributes to Add/Remove</param>
        /// <param name="size">The Size of the Attributes Struct</param>
        /// <returns>If The Call Was Successful or Not</returns>
        [DllImport("UxTheme.dll")]
        public static extern int SetWindowThemeAttribute(IntPtr hWnd, WindowThemeAttributeType wtype, ref WTA_OPTIONS attributes, uint size);

        /// <summary>
        /// Do Not Draw The Caption (Text)
        /// </summary>
        public static uint WTNCA_NODRAWCAPTION = 0x1U;
        /// <summary>
        /// Do Not Draw the Icon
        /// </summary>
        public static uint WTNCA_NODRAWICON = 0x2U;
        /// <summary>
        /// Do Not Show the System Menu
        /// </summary>
        public static uint WTNCA_NOSYSMENU = 0x4U;
        /// <summary>
        /// Do Not Mirror the Question mark Symbol
        /// </summary>
        public static uint WTNCA_NOMIRRORHELP = 0x8U;

        /// <summary>
        /// The Options of What Attributes to Add/Remove
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct WTA_OPTIONS
        {
            public uint Flags;
            public uint Mask;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DttOpts
        {
            public int dwSize;
            public DttOptsFlags dwFlags;
            public int crText;
            public int crBorder;
            public int crShadow;
            public int iTextShadowType;
            public Point ptShadowOffset;
            public int iBorderSize;
            public int iFontPropId;
            public int iColorPropId;
            public int iStateId;
            public bool fApplyOverlay;
            public int iGlowSize;
            public int pfnDrawTextCallback;
            public IntPtr lParam;
        }

        [Flags]
        public enum DttOptsFlags : int
        {
            DTT_TEXTCOLOR = 1,
            DTT_BORDERCOLOR = 2,
            DTT_SHADOWCOLOR = 4,
            DTT_SHADOWTYPE = 8,
            DTT_SHADOWOFFSET = 16,
            DTT_BORDERSIZE = 32,
            // DTT_FONTPROP = 64,		commented values are currently unused
            // DTT_COLORPROP = 128,
            // DTT_STATEID = 256,
            DTT_CALCRECT = 512,
            DTT_APPLYOVERLAY = 1024,
            DTT_GLOWSIZE = 2048,
            // DTT_CALLBACK = 4096,
            DTT_COMPOSITED = 8192
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct RECT
        {
            // Fields
            [FieldOffset(12)]
            public int bottom;
            [FieldOffset(0)]
            public int left;
            [FieldOffset(8)]
            public int right;
            [FieldOffset(4)]
            public int top;

            // Methods
            public RECT(Rectangle rect)
            {
                this.left = rect.Left;
                this.top = rect.Top;
                this.right = rect.Right;
                this.bottom = rect.Bottom;
            }

            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public void Set()
            {
                this.left = this.top = this.right = this.bottom = 0;
            }

            public void Set(Rectangle rect)
            {
                this.left = rect.Left;
                this.top = rect.Top;
                this.right = rect.Right;
                this.bottom = rect.Bottom;
            }

            public void Set(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public Rectangle ToRectangle()
            {
                return new Rectangle(this.left, this.top, this.right - this.left, this.bottom - this.top);
            }

            // Properties
            public int Height
            {
                get
                {
                    return (this.bottom - this.top);
                }
            }

            public Size Size
            {
                get
                {
                    return new Size(this.Width, this.Height);
                }
            }

            public int Width
            {
                get
                {
                    return (this.right - this.left);
                }
            }
        }

        /// <summary>
        /// What Type of Attributes? (Only One is Currently Defined)
        /// </summary>
        public enum WindowThemeAttributeType
        {
            WTA_NONCLIENT = 1
        }
    }
}
