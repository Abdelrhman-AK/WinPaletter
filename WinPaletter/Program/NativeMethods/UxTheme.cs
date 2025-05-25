using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace WinPaletter.NativeMethods
{
    /// <summary>
    /// Provides partial class implementation for interacting with the User Experience (UX) WinTheme APIs.
    /// This partial class may contain additional members related to UxTheme functionality.
    /// </summary>
    public partial class UxTheme
    {
        /// <summary>
        /// Sets the theme for a specified window.
        /// </summary>
        /// <param name="hwnd">A handle to the window for which to set the theme.</param>
        /// <param name="pszSubAppName">A pointer to a string that contains the name of the application.</param>
        /// <param name="pszSubIdList">A pointer to a string that contains a semicolon-separated list of CLSID names for classes.</param>
        /// <returns>Returns zero if successful; otherwise, returns a non-zero error code.</returns>
        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

        /// <summary>
        /// Set The Window's WinTheme Attributes
        /// </summary>
        /// <param name="hWnd">The Handle to the Window</param>
        /// <param name="wtype">What ButtonOverlay of Attributes</param>
        /// <param name="attributes">The Attributes to Add/Remove</param>
        /// <param name="size">The Size of the Attributes Struct</param>
        /// <returns>If The Call Was Successful or Not</returns>
        [DllImport("UxTheme.dll")]
        public static extern int SetWindowThemeAttribute(IntPtr hWnd, WindowThemeAttributeType wtype, ref WTA_OPTIONS attributes, uint size);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="preferredAppMode"></param>
        /// <returns></returns>
        [DllImport("uxtheme.dll", EntryPoint = "#135", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int SetPreferredAppMode(PreferredAppMode preferredAppMode);

        /// <summary>
        /// The Preferred Application Mode (Dark, Light, System Default)
        /// </summary>
        public enum PreferredAppMode
        {
            /// <summary>
            /// The default mode
            /// </summary>
            Default = 0,

            /// <summary>
            /// The dark mode
            /// </summary>
            Dark = 1,

            /// <summary>
            /// The light mode
            /// </summary>
            Light = 2,

            /// <summary>
            /// The system default mode
            /// </summary>
            SystemDefault = 3
        }

        /// <summary>
        /// Do Not Draw The Caption (Text)
        /// </summary>
        public static uint WTNCA_NODRAWCAPTION = 0x1U;
        /// <summary>
        /// Do Not Draw the Icon
        /// </summary>
        public static uint WTNCA_NODRAWICON = 0x2U;
        /// <summary>
        /// Do Not Hide the System contextMenu
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
            /// <summary>
            /// Flags specifying which attributes to add or remove.
            /// </summary>
            public uint Flags;

            /// <summary>
            /// Mask specifying which attributes to modify.
            /// </summary>
            public uint Mask;
        }

        /// <summary>
        /// Options for drawing themed text.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct DttOpts
        {
            /// <summary>
            /// The size of the structure.
            /// </summary>
            public int dwSize;

            /// <summary>
            /// Flags specifying which options to apply.
            /// </summary>
            public DttOptsFlags dwFlags;

            /// <summary>
            /// The color of the text.
            /// </summary>
            public int crText;

            /// <summary>
            /// The color of the border.
            /// </summary>
            public int crBorder;

            /// <summary>
            /// The color of the shadow.
            /// </summary>
            public int crShadow;

            /// <summary>
            /// The type of text shadow.
            /// </summary>
            public int iTextShadowType;

            /// <summary>
            /// The offset of the shadow.
            /// </summary>
            public Point ptShadowOffset;

            /// <summary>
            /// The size of the border.
            /// </summary>
            public int iBorderSize;

            /// <summary>
            /// The font property ID.
            /// </summary>
            public int iFontPropId;

            /// <summary>
            /// The color property ID.
            /// </summary>
            public int iColorPropId;

            /// <summary>
            /// The state ID.
            /// </summary>
            public int iStateId;

            /// <summary>
            /// Indicates whether to apply overlay.
            /// </summary>
            public bool fApplyOverlay;

            /// <summary>
            /// The size of the glow.
            /// </summary>
            public int iGlowSize;

            /// <summary>
            /// Callback function for drawing text.
            /// </summary>
            public int pfnDrawTextCallback;

            /// <summary>
            /// User-defined parameter for the callback function.
            /// </summary>
            public IntPtr lParam;
        }

        /// <summary>
        /// Flags for specifying drawing options when using themed text.
        /// </summary>
        [Flags]
        public enum DttOptsFlags : int
        {
            /// <summary>
            /// Use the text color specified in the crText member.
            /// </summary>
            DTT_TEXTCOLOR = 1,

            /// <summary>
            /// Use the border color specified in the crBorder member.
            /// </summary>
            DTT_BORDERCOLOR = 2,

            /// <summary>
            /// Use the shadow color specified in the crShadow member.
            /// </summary>
            DTT_SHADOWCOLOR = 4,

            /// <summary>
            /// Use the shadow type specified in the iTextShadowType member.
            /// </summary>
            DTT_SHADOWTYPE = 8,

            /// <summary>
            /// Use the shadow offset specified in the ptShadowOffset member.
            /// </summary>
            DTT_SHADOWOFFSET = 16,

            /// <summary>
            /// Use the border size specified in the iBorderSize member.
            /// </summary>
            DTT_BORDERSIZE = 32,

            /// <summary>
            /// GetTextAndImageRectangles the rectangle size without writing the text.
            /// </summary>
            DTT_CALCRECT = 512,

            /// <summary>
            /// Apply an overlay to the text.
            /// </summary>
            DTT_APPLYOVERLAY = 1024,

            /// <summary>
            /// Use the glow size specified in the iGlowSize member.
            /// </summary>
            DTT_GLOWSIZE = 2048,

            /// <summary>
            /// Draw the text as a composited operation.
            /// </summary>
            DTT_COMPOSITED = 8192
        }

        /// <summary>
        /// Rectangle structure for specifying coordinates and dimensions.
        /// </summary>
        /// <remarks>
        /// Initializes a new instance of the <see cref="RECT"/> structure.
        /// </remarks>
        /// <param name="rect">A <see cref="Rectangle"/> specifying coordinates and dimensions.</param>
        [StructLayout(LayoutKind.Explicit)]
        public struct RECT(Rectangle rect)
        {
            /// <summary>
            /// Left coordinate of the rectangle.
            /// </summary>
            [FieldOffset(0)]
            public int left = rect.Left;

            /// <summary>
            /// Top coordinate of the rectangle.
            /// </summary>
            [FieldOffset(4)]
            public int top = rect.Top;

            /// <summary>
            /// Right coordinate of the rectangle.
            /// </summary>
            [FieldOffset(8)]
            public int right = rect.Right;

            /// <summary>
            /// Bottom coordinate of the rectangle.
            /// </summary>
            [FieldOffset(12)]
            public int bottom = rect.Bottom;

            /// <summary>
            /// Converts the RECT structure to a <see cref="Rectangle"/>.
            /// </summary>
            /// <returns>A <see cref="Rectangle"/> representing the coordinates and dimensions of the RECT structure.</returns>
            public readonly Rectangle ToRectangle()
            {
                return new Rectangle(left, top, right - left, bottom - top);
            }
        }

        /// <summary>
        /// Specifies the type of window theme attribute.
        /// </summary>
        public enum WindowThemeAttributeType
        {
            /// <summary>
            /// Non-client area attributes.
            /// </summary>
            WTA_NONCLIENT = 1
        }
    }
}
