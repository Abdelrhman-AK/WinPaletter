using System;
using System.Runtime.InteropServices;
using static WinPaletter.NativeMethods.UxTheme;

namespace WinPaletter.NativeMethods
{
    /// <summary>
    /// Provides P/Invoke declarations for the Desktop Window Manager (DWM) API.
    /// </summary>
    public class DWMAPI
    {
        private const string _dwmapi = "dwmapi.dll";

        #region Constants

        /// <summary>
        /// Represents a constant value for enabling blur in a region for DWM.
        /// </summary>
        public const int DWM_BB_BLURREGION = 2;

        /// <summary>
        /// Represents a constant value for enabling DWM composition.
        /// </summary>
        public const int DWM_BB_ENABLE = 1;

        /// <summary>
        /// Represents a constant value for transitioning on maximized state for DWM.
        /// </summary>
        public const int DWM_BB_TRANSITIONONMAXIMIZED = 4;

        /// <summary>
        /// Represents the base name for DWM composed events.
        /// </summary>
        public const string DWM_COMPOSED_EVENT_BASE_NAME = "DwmComposedEvent_";

        /// <summary>
        /// Represents the format for the name of DWM composed events.
        /// </summary>
        public const string DWM_COMPOSED_EVENT_NAME_FORMAT = "%s%d";

        /// <summary>
        /// Represents the maximum length of the name for DWM composed events.
        /// </summary>
        public const int DWM_COMPOSED_EVENT_NAME_MAX_LENGTH = 0x40;

        /// <summary>
        /// Represents the default duration for a DWM frame.
        /// </summary>
        public const int DWM_FRAME_DURATION_DEFAULT = -1;

        /// <summary>
        /// Represents a constant value for DWM transparency.
        /// </summary>
        public const int DWM_TNP_OPACITY = 4;

        /// <summary>
        /// Represents a constant value for DWM destination rectangle.
        /// </summary>
        public const int DWM_TNP_RECTDESTINATION = 1;

        /// <summary>
        /// Represents a constant value for DWM source rectangle.
        /// </summary>
        public const int DWM_TNP_RECTSOURCE = 2;

        /// <summary>
        /// Represents a constant value for limiting the source to the client area only in DWM.
        /// </summary>
        public const int DWM_TNP_SOURCECLIENTAREAONLY = 0x10;

        /// <summary>
        /// Represents a constant value for DWM visibility.
        /// </summary>
        public const int DWM_TNP_VISIBLE = 8;
   
        #endregion

        #region Methods
        /// <summary>
        /// Enables or disables the blur effect behind a window.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="pBlurBehind">A pointer to a DWM_BLURBEHIND structure that describes the blur-behind area.</param>
        [DllImport(_dwmapi, PreserveSig = false)]
        public static extern int DwmEnableBlurBehindWindow(IntPtr hWnd, DWM_BLURBEHIND pBlurBehind);

        /// <summary>
        /// Queries the composition state of Desktop Window Manager (DWM).
        /// </summary>
        /// <param name="isEnabled">Receives the current composition state. True if composition is enabled; otherwise, false.</param>
        /// <returns>Returns S_OK if successful; otherwise, an HRESULT error code.</returns>
        [DllImport(_dwmapi)]
        public static extern int DwmIsCompositionEnabled(out bool isEnabled);

        /// <summary>
        /// Retrieves a Desktop Window Manager (DWM) attribute for the specified window.
        /// </summary>
        /// <remarks>Requires DWM (dwmapi.dll) and is available on Windows Vista and later when Desktop
        /// Window Manager is present. Ensure cbAttribute matches the expected size for the requested attribute;
        /// otherwise the call may fail.</remarks>
        /// <param name="hwnd">Handle of the window to query.</param>
        /// <param name="dwAttribute">DWM attribute constant (DWMWA_*) that specifies which attribute to retrieve.</param>
        /// <param name="pvAttribute">Out parameter that receives the attribute value as a RECT; the actual data depends on dwAttribute.</param>
        /// <param name="cbAttribute">Size, in bytes, of the structure pointed to by pvAttribute; typically Marshal.SizeOf(typeof(RECT)).</param>
        /// <returns>HRESULT code: S_OK (0) on success; otherwise an error code indicating failure.</returns>
        [DllImport(_dwmapi)]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out RECT pvAttribute, int cbAttribute);

        /// <summary>
        /// Extends the window frame into the client area.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="pMarInset">A pointer to a MARGINS structure that describes the margins to use when extending the frame into the client area.</param>
        /// <returns>Returns S_OK if successful; otherwise, an HRESULT error code.</returns>
        [DllImport(_dwmapi)]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        /// <summary>
        /// Sets the value of Desktop Window Manager (DWM) non-client rendering attributes.
        /// </summary>
        /// <param name="hwnd">A handle to the window.</param>
        /// <param name="attr">The attribute to set.</param>
        /// <param name="attrValue">The value to set.</param>
        /// <param name="attrSize">The size of the value being set.</param>
        /// <returns>Returns S_OK if successful; otherwise, an HRESULT error code.</returns>
        [DllImport(_dwmapi)]
        internal static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        /// <summary>
        /// Sets the value of Desktop Window Manager (DWM) non-client rendering attributes.
        /// </summary>
        /// <param name="hwnd">A handle to the window.</param>
        /// <param name="dwAttribute">The attribute to set.</param>
        /// <param name="pvAttribute">The value to set.</param>
        /// <param name="cbAttribute">The size, in bytes, of the pvAttribute value.</param>
        /// <returns>Returns S_OK if successful; otherwise, an HRESULT error code.</returns>
        [DllImport(_dwmapi)]
        internal static extern int DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE dwAttribute, ref int pvAttribute, int cbAttribute);

        /// <summary>
        /// Sets the Desktop Window Manager (DWM) colorization parameters.
        /// </summary>
        /// <param name="parameters">A pointer to a DWM_COLORIZATION_PARAMS structure that specifies the colorization parameters.</param>
        /// <param name="unknown">Unknown parameter.</param>
        /// <returns>Returns S_OK if successful; otherwise, an HRESULT error code.</returns>
        [DllImport(_dwmapi, EntryPoint = "#131", PreserveSig = false)]
        public static extern void DwmSetColorizationParameters(ref DWM_COLORIZATION_PARAMS parameters, bool unknown);

        /// <summary>
        /// Retrieves the current colorization parameters used by the Desktop Window Manager (DWM).
        /// </summary>
        /// <remarks>This method is a P/Invoke wrapper for the DWM API function and requires the
        /// _dwmapi library. It does not set the last error, so error handling should be implemented based on the
        /// context of usage.</remarks>
        /// <param name="parameters">When this method returns, contains the <see cref="DWM_COLORIZATION_PARAMS"/> structure with the current
        /// colorization settings. This parameter is passed uninitialized.</param>
        [DllImport(_dwmapi, EntryPoint = "#127", SetLastError = false)]
        public static extern void DwmGetColorizationParameters(out DWM_COLORIZATION_PARAMS parameters);

        /// <summary>
        /// Checks if Desktop Window Manager (DWM) composition is enabled on the system.
        /// </summary>
        /// <returns>True if DWM composition is enabled; otherwise, false.</returns>
        public static bool IsCompositionEnabled()
        {
            try
            {
                DwmIsCompositionEnabled(out bool isEnabled);
                return Environment.OSVersion.Version.Major >= 6 && isEnabled;
            }
            catch { return false; }
        }

        #endregion

        #region Structures

        /// <summary>
        /// Represents a method that determines whether Desktop Window Manager (DWM) composition is currently enabled.
        /// </summary>
        /// <remarks>This delegate is typically used to call the native DwmIsCompositionEnabled function
        /// in Windows. The caller should check the return value to determine whether the operation succeeded before
        /// using the value in <paramref name="pfEnabled"/>.</remarks>
        /// <param name="pfEnabled">When this method returns, contains a value that is set to <see langword="true"/> if DWM composition is
        /// enabled; otherwise, <see langword="false"/>. This parameter is passed uninitialized.</param>
        /// <returns>An integer value indicating the result of the operation. A value of 0 typically indicates success;
        /// otherwise, an error code is returned.</returns>
        public delegate int FnDwmIsCompositionEnabled(out bool pfEnabled);

        /// <summary>
        /// Represents a callback that processes a window message using the Desktop Window Manager (DWM) default window
        /// procedure.
        /// </summary>
        /// <remarks>This delegate is typically used to forward window messages to the DWM for default
        /// processing. The caller should check the return value to determine whether the message was handled by
        /// DWM.</remarks>
        /// <param name="hwnd">A handle to the window receiving the message.</param>
        /// <param name="msg">The message identifier specifying which message is being processed.</param>
        /// <param name="wParam">Additional message-specific information. The exact meaning depends on the value of the msg parameter.</param>
        /// <param name="lParam">Additional message-specific information. The exact meaning depends on the value of the msg parameter.</param>
        /// <param name="plResult">When this method returns, contains the result of message processing if the message was handled. This
        /// parameter is passed uninitialized.</param>
        /// <returns>An integer value indicating whether the message was processed. Returns a nonzero value if the message was
        /// handled; otherwise, zero.</returns>
        public delegate int FnDwmDefWindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, out IntPtr plResult);

        /// <summary>
        /// Represents a method that retrieves the current Desktop Window Manager (DWM) colorization color and opacity setting.
        /// </summary>
        /// <remarks>This delegate is intended for use with native interop to the DwmGetColorizationColor
        /// function in the Windows API. The caller should ensure that the method is called on a supported version of
        /// Windows where DWM is available.</remarks>
        /// <param name="pcrColorization">When this method returns, contains the ARGB value of the current DWM colorization color.</param>
        /// <param name="pfOpaqueBlend">When this method returns, contains a value indicating whether the colorization is opaque. Contains <see
        /// langword="true"/> if the colorization is opaque; otherwise, <see langword="false"/>.</param>
        /// <returns>An integer value indicating the result of the operation. Typically, zero indicates success.</returns>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int FnDwmGetColorizationColor(out uint pcrColorization, out bool pfOpaqueBlend);

        /// <summary>
        /// Struct that specifies Desktop Window Manager (DWM) blur behind settings.
        /// </summary>
        /// <remarks>
        /// Initializes a new instance of the DWM_BLURBEHIND structure.
        /// </remarks>
        /// <param name="enable">True to enable the blur effect; false to disable.</param>
        [StructLayout(LayoutKind.Sequential)]
        public struct DWM_BLURBEHIND(bool enable)
        {
            /// <summary>
            /// Flags that indicate which members of this structure are valid.
            /// </summary>
            public uint dwFlags = 0x00000003;

            /// <summary>
            /// True to enable the blur effect; false to disable.
            /// </summary>
            public bool fEnable = enable;

            /// <summary>
            /// Handle to the region where the blur should be applied.
            /// </summary>
            public IntPtr hRgnBlur = IntPtr.Zero;

            /// <summary>
            /// True to transition to a blurred representation when maximized; false otherwise.
            /// </summary>
            public bool fTransitionOnMaximized = false;
        }

        /// <summary>
        /// Struct that specifies margins for extending the window frame into the client area.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            /// <summary>
            /// Width of the left border.
            /// </summary>
            public int leftWidth;

            /// <summary>
            /// Width of the right border.
            /// </summary>
            public int rightWidth;

            /// <summary>
            /// Height of the top border.
            /// </summary>
            public int topHeight;

            /// <summary>
            /// Height of the bottom border.
            /// </summary>
            public int bottomHeight;
        }

        /// <summary>
        /// Struct that specifies an unsigned ratio.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct UNSIGNED_RATIO
        {
            /// <summary>
            /// Numerator of the ratio.
            /// </summary>
            public int uiNumerator;

            /// <summary>
            /// Denominator of the ratio.
            /// </summary>
            public int uiDenominator;
        }

        /// <summary>
        /// Struct that specifies Desktop Window Manager (DWM) colorization parameters.
        /// </summary>
        public struct DWM_COLORIZATION_PARAMS
        {
            /// <summary>
            /// The color used for colorization.
            /// </summary>
            public uint clrColor;

            /// <summary>
            /// The color of the glow effect that follows colorization.
            /// </summary>
            public uint clrAfterGlow;

            /// <summary>
            /// The intensity of the colorization.
            /// </summary>
            public uint nIntensity;

            /// <summary>
            /// The balance of the colorization after the glow effect is applied.
            /// </summary>
            public uint clrAfterGlowBalance;

            /// <summary>
            /// The balance of the blur effect.
            /// </summary>
            public uint clrBlurBalance;

            /// <summary>
            /// The intensity of the glass reflection effect.
            /// </summary>
            public uint clrGlassReflectionIntensity;

            /// <summary>
            /// True if the colorization is opaque; false if it's not opaque.
            /// </summary>
            public bool fOpaque;
        }

        #endregion

        #region Enumerations

        public enum DWMWINDOWATTRIBUTE : uint
        {
            NCRENDERING_ENABLED = 1,
            NCRENDERING_POLICY = 2,
            TRANSITIONS_FORCEDISABLED = 3,
            ALLOW_NCPAINT = 4,
            CAPTION_BUTTON_BOUNDS = 5,
            NONCLIENT_RTL_LAYOUT = 6,
            FORCE_ICONIC_REPRESENTATION = 7,
            FLIP3D_POLICY = 8,
            EXTENDED_FRAME_BOUNDS = 9,
            HAS_ICONIC_BITMAP = 10,
            DISALLOW_PEEK = 11,
            EXCLUDED_FROM_PEEK = 12,
            CLOAK = 13,
            CLOAKED = 14,
            FREEZE_REPRESENTATION = 15,
            PASSIVE_UPDATE_MODE = 16,
            USE_HOSTBACKDROPBRUSH = 17,
            USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19,
            USE_IMMERSIVE_DARK_MODE = 20,
            WINDOW_CORNER_PREFERENCE = 33,
            DWMWA_SYSTEMBACKDROP_TYPE = 38,
            BORDER_COLOR = 34,
            CAPTION_COLOR = 35,
            TEXT_COLOR = 36,
            VISIBLE_FRAME_BORDER_THICKNESS = 37,
            MICA_EFFECT = 1029,
            LAST
        }

        /// <summary>
        /// Enumerates different types of form corners.
        /// </summary>
        public enum FormCornersType
        {
            /// <summary>
            /// Default form corners.
            /// </summary>
            Default,

            /// <summary>
            /// Rectangular form corners.
            /// </summary>
            Rectangular,

            /// <summary>
            /// Round form corners.
            /// </summary>
            Round,

            /// <summary>
            /// Small round form corners.
            /// </summary>
            SmallRound
        }

        public enum DWM_SYSTEMBACKDROP_TYPE
        {
            DWMSBT_AUTO = 0,
            DWMSBT_NONE = 1,
            DWMSBT_MAINWINDOW = 2,       // Mica
            DWMSBT_TRANSIENTWINDOW = 3,  // Acrylic
            DWMSBT_TABBEDWINDOW = 4
        }

        #endregion
    }
}
