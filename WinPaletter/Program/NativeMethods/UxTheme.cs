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
        private const string _uxtheme = "uxtheme.dll";

        /// <summary>
        /// Sets the theme for a specified window.
        /// </summary>
        /// <param name="hwnd">A handle to the window for which to set the theme.</param>
        /// <param name="pszSubAppName">A pointer to a string that contains the name of the application.</param>
        /// <param name="pszSubIdList">A pointer to a string that contains a semicolon-separated list of CLSID names for classes.</param>
        /// <returns>Returns zero if successful; otherwise, returns a non-zero error code.</returns>
        [DllImport(_uxtheme, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

        /// <summary>
        /// Set The Window's WinTheme Attributes
        /// </summary>
        /// <param name="hWnd">The Handle to the Window</param>
        /// <param name="wtype">What ButtonOverlay of Attributes</param>
        /// <param name="attributes">The Attributes to Add/Remove</param>
        /// <param name="size">The Size of the Attributes Struct</param>
        /// <returns>If The Call Was Successful or Not</returns>
        [DllImport(_uxtheme)]
        public static extern int SetWindowThemeAttribute(IntPtr hWnd, WindowThemeAttributeType wtype, ref WTA_OPTIONS attributes, uint size);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="preferredAppMode"></param>
        /// <returns></returns>
        [DllImport(_uxtheme, EntryPoint = "#135", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int SetPreferredAppMode(int preferredAppMode);

        /// <summary>
        /// Allow dark mode for a window provided by its handle
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="allow"></param>
        /// <returns></returns>
        [DllImport(_uxtheme, EntryPoint = "#133", SetLastError = true)]
        public static extern bool AllowDarkModeForWindow(IntPtr hWnd, bool allow);

        /// <summary>
        /// Opens a theme data handle for a specified window and class list.
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="pszClassList"></param>
        /// <returns></returns>
        [DllImport(_uxtheme, CharSet = CharSet.Unicode)]
        public static extern IntPtr OpenThemeData(IntPtr hwnd, string pszClassList);

        /// <summary>
        /// Closes a theme data handle and releases associated resources.
        /// </summary>
        /// <param name="hTheme"></param>
        /// <returns></returns>
        [DllImport(_uxtheme)]
        public static extern int CloseThemeData(IntPtr hTheme);

        /// <summary>
        /// Gets the font associated with a specified theme part and state.
        /// </summary>
        /// <param name="hTheme"></param>
        /// <param name="hdc"></param>
        /// <param name="iPartId"></param>
        /// <param name="iStateId"></param>
        /// <param name="iPropId"></param>
        /// <param name="pFont"></param>
        /// <returns></returns>
        [DllImport(_uxtheme, CharSet = CharSet.Unicode)]
        public static extern int GetThemeFont(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, int iPropId, out GDI32.LOGFONT pFont);

        /// <summary>
        /// Gets the color associated with a specified theme part and property.
        /// </summary>
        /// <param name="hTheme"></param>
        /// <param name="iPartId"></param>
        /// <param name="iStateId"></param>
        /// <param name="iPropId"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        [DllImport(_uxtheme)]
        public static extern int GetThemeColor(IntPtr hTheme, int iPartId, int iStateId, int iPropId, out COLORREF color);

        /// <summary>
        /// Draws text using the visual style defined by the theme for a specified control part and state.
        /// </summary>
        /// <param name="hTheme"></param>
        /// <param name="hdc"></param>
        /// <param name="iPartId"></param>
        /// <param name="iStateId"></param>
        /// <param name="text"></param>
        /// <param name="iCharCount"></param>
        /// <param name="dwFlags"></param>
        /// <param name="pRect"></param>
        /// <param name="pOptions"></param>
        /// <returns></returns>
        [DllImport(_uxtheme, CharSet = CharSet.Unicode)]
        public static extern int DrawThemeTextEx(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, string text, int iCharCount, uint dwFlags, ref RECT pRect, ref DTTOPTS pOptions);

        /// <summary>
        /// Color reference structure used to represent colors in the UxTheme API.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct COLORREF(Color c)
        {
            public uint Value = (uint)(c.R | (c.G << 8) | (c.B << 16));

            public readonly Color Color => Color.FromArgb((int)(Value & 0xFF), (int)((Value >> 8) & 0xFF), (int)((Value >> 16) & 0xFF));
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct DTTOPTS
        {
            public uint dwSize;
            public uint dwFlags;
            public COLORREF crText;
            public uint iBorderSize;
            public uint iFontPropId;
            public uint iColorPropId;
            public uint iStateId;
            public bool fApplyOverlay;
            public int iGlowSize;
            public IntPtr pfnDrawTextCallback;
            public IntPtr lParam;
        }

        public const uint DT_TOP = 0x00000000;
        public const uint DT_LEFT = 0x00000000;
        public const uint DT_CENTER = 0x00000001;
        public const uint DT_RIGHT = 0x00000002;
        public const uint DT_VCENTER = 0x00000004;
        public const uint DT_BOTTOM = 0x00000008;
        public const uint DT_WORDBREAK = 0x00000010;
        public const uint DT_SINGLELINE = 0x00000020;
        public const uint DT_EXPANDTABS = 0x00000040;
        public const uint DT_TABSTOP = 0x00000080;
        public const uint DT_NOCLIP = 0x00000100;
        public const uint DT_EXTERNALLEADING = 0x00000200;
        public const uint DT_CALCRECT = 0x00000400;
        public const uint DT_NOPREFIX = 0x00000800;
        public const uint DT_INTERNAL = 0x00001000;
        public const uint DT_EDITCONTROL = 0x00002000;
        public const uint DT_PATH_ELLIPSIS = 0x00004000;
        public const uint DT_END_ELLIPSIS = 0x00008000;
        public const uint DT_MODIFYSTRING = 0x00010000;
        public const uint DT_RTLREADING = 0x00020000;
        public const uint DT_WORD_ELLIPSIS = 0x00040000;
        public const uint DT_NOFULLWIDTHCHARBREAK = 0x00080000;
        public const uint DT_HIDEPREFIX = 0x00100000;
        public const uint DT_PREFIXONLY = 0x00200000;
        public const uint DTT_GLOWSIZE = 2048;

        public const int TMT_TEXTCOLOR = 3803;
        public const int TMT_FONT = 210;

        public const int TDLG_MAININSTRUCTIONPANE = 1;
        public const int TDLG_CONTENTPANE = 2;

        public const uint DTT_TEXTCOLOR = 0x00000001;
        public const uint DTT_COMPOSITED = 0x00002000;

        /// <summary>
        /// Represents a method that opens a theme data handle for a specified window and class list.
        /// </summary>
        /// <remarks>The returned handle should be closed using the appropriate method when it is no
        /// longer needed to avoid resource leaks.</remarks>
        /// <param name="hwnd">A handle to the window for which theme data is to be opened.</param>
        /// <param name="pszClassList">The class name or a semicolon-separated list of class names that identify the parts and states to retrieve
        /// theme data for. Cannot be null.</param>
        /// <returns>An IntPtr that represents a handle to the theme data. Returns IntPtr.Zero if the operation fails.</returns>
        public delegate IntPtr FnOpenThemeData(IntPtr hwnd, [MarshalAs(UnmanagedType.LPWStr)] string pszClassList);

        /// <summary>
        /// Represents a method that closes a theme data handle and releases associated resources.
        /// </summary>
        /// <remarks>This delegate is commonly used when working with Windows visual styles to ensure that
        /// theme data handles are properly released. Failing to close theme data handles may result in resource
        /// leaks.</remarks>
        /// <param name="hTheme">A handle to the theme data to be closed. This handle must have been obtained from a previous theme-related
        /// operation and must not be zero.</param>
        /// <returns>An integer value indicating the result of the operation. Typically, zero indicates success; a nonzero value
        /// indicates failure.</returns>
        public delegate int FnCloseThemeData(IntPtr hTheme);

        /// <summary>
        /// Represents a method that draws the background image defined by the visual style for a specified control part
        /// and state.
        /// </summary>
        /// <remarks>This delegate is typically used to interoperate with native UxTheme APIs for custom
        /// drawing of themed controls. The caller is responsible for ensuring that the provided handles and structures
        /// are valid and remain valid for the duration of the call.</remarks>
        /// <param name="hTheme">A handle to the theme data for the control. This must be obtained from a call to the appropriate theme API.</param>
        /// <param name="hdc">A handle to the device context on which the background is drawn.</param>
        /// <param name="iPartId">The identifier of the part of the control to draw. The value is specific to the control's visual style.</param>
        /// <param name="iStateId">The identifier of the state of the part to draw. The value is specific to the control's visual style.</param>
        /// <param name="pRect">A reference to a RECT structure that specifies the bounds of the area to be drawn.</param>
        /// <param name="pClipRect">A pointer to a RECT structure that specifies the clipping rectangle, or IntPtr.Zero to indicate no clipping.</param>
        /// <returns>An integer value indicating success or failure. Returns zero if successful; otherwise, returns a nonzero
        /// error code.</returns>
        public delegate int FnDrawThemeBackground(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, ref UxTheme.RECT pRect, IntPtr pClipRect);

        /// <summary>
        /// Represents a method that determines whether a visual theme is currently active.
        /// </summary>
        /// <remarks>The exact meaning of the return value may depend on the implementation. This delegate
        /// is commonly used to abstract theme detection logic in UI frameworks.</remarks>
        /// <returns>An integer value indicating the theme status. Typically, a nonzero value indicates that a theme is active;
        /// zero indicates that no theme is active.</returns>
        public delegate int FnIsThemeActive();

        /// <summary>
        /// Represents a method that retrieves the size of a visual style part for a specified theme, state, and drawing
        /// context.
        /// </summary>
        /// <remarks>This delegate is typically used to interoperate with native Windows visual styles
        /// APIs. The caller is responsible for ensuring that all handles and pointers are valid and that the output
        /// parameter is properly initialized.</remarks>
        /// <param name="hTheme">A handle to the theme data for the current visual style.</param>
        /// <param name="hdc">A handle to the device context used for drawing. This can be IntPtr.Zero if not required.</param>
        /// <param name="iPartId">The identifier of the part within the theme whose size is to be retrieved.</param>
        /// <param name="iStateId">The identifier of the state of the part for which the size is requested.</param>
        /// <param name="pRect">A pointer to a RECT structure that defines the area to be used for drawing, or IntPtr.Zero to use the
        /// default size.</param>
        /// <param name="eSize">An integer specifying the type of size to retrieve. Typically corresponds to a THEMESIZE value such as
        /// TS_TRUE (2).</param>
        /// <param name="psz">When this method returns, contains the size of the specified theme part.</param>
        /// <returns>An integer value indicating the result of the operation. Returns zero if successful; otherwise, returns a
        /// nonzero error code.</returns>
        public delegate int FnGetThemePartSize(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, IntPtr pRect, int eSize, out SIZE psz);

        /// <summary>
        /// Retrieves the file name, color scheme, and size name of the current visual style theme.
        /// </summary>
        /// <remarks>This delegate is typically used to call native Windows APIs for theme information.
        /// All output buffers must be preallocated by the caller and sized according to the corresponding maximum
        /// character parameters. The method does not allocate memory for the output buffers.</remarks>
        /// <param name="pszThemeFileName">A StringBuilder that receives the file name of the current theme. The buffer must be large enough to contain
        /// the file name, including the terminating null character.</param>
        /// <param name="dwMaxNameChars">The maximum number of characters, including the terminating null character, that can be copied into
        /// pszThemeFileName.</param>
        /// <param name="pszColorBuff">A StringBuilder that receives the color scheme name of the current theme. The buffer must be large enough to
        /// contain the color name, including the terminating null character.</param>
        /// <param name="dwMaxColorChars">The maximum number of characters, including the terminating null character, that can be copied into
        /// pszColorBuff.</param>
        /// <param name="pszSizeBuff">A StringBuilder that receives the size name of the current theme. The buffer must be large enough to contain
        /// the size name, including the terminating null character.</param>
        /// <param name="cchMaxSizeChars">The maximum number of characters, including the terminating null character, that can be copied into
        /// pszSizeBuff.</param>
        /// <returns>An integer value indicating the result of the operation. Returns zero if successful; otherwise, returns a
        /// nonzero error code.</returns>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int FnGetCurrentThemeName(System.Text.StringBuilder pszThemeFileName, int dwMaxNameChars, System.Text.StringBuilder pszColorBuff, int dwMaxColorChars, System.Text.StringBuilder pszSizeBuff, int cchMaxSizeChars);

        /// <summary>
        /// Represents a callback method that retrieves a color value for a specified theme part and property.
        /// </summary>
        /// <remarks>This delegate is typically used to interoperate with native Windows theming APIs. The
        /// caller must ensure that the theme handle is valid and that the property identifiers correspond to supported
        /// theme parts and states.</remarks>
        /// <param name="hTheme">A handle to the theme data from which to retrieve the color.</param>
        /// <param name="iPartId">The identifier of the part within the theme to query.</param>
        /// <param name="iStateId">The identifier of the state of the part to query.</param>
        /// <param name="iPropId">The identifier of the color property to retrieve.</param>
        /// <param name="pColor">When this method returns, contains the ARGB color value associated with the specified theme part and
        /// property.</param>
        /// <returns>An integer value indicating the result of the operation. Returns 0 if successful; otherwise, returns a
        /// nonzero error code.</returns>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int FnGetThemeColor(IntPtr hTheme, int iPartId, int iStateId, int iPropId, out int pColor);

        /// <summary>
        /// Represents the width and height of a rectangle, typically in pixels.
        /// </summary>
        /// <remarks>The SIZE structure is commonly used in graphics programming to specify dimensions for
        /// drawing operations, window sizing, or layout calculations. The cx field specifies the width, and the cy
        /// field specifies the height.</remarks>
        [StructLayout(LayoutKind.Sequential)]
        public struct SIZE { public int cx, cy; }

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
            /// Process an overlay to the text.
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
