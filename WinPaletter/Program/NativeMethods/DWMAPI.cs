using System;
using System.Runtime.InteropServices;

namespace WinPaletter.NativeMethods
{
    /// <summary>
    /// Provides P/Invoke declarations for the Desktop Window Manager (DWM) API.
    /// </summary>
    public class DWMAPI
    {
        #region Constants
        /// <summary>
        /// Represents a constant value for enabling drop shadow in a window.
        /// </summary>
        public const int CS_DROPSHADOW = 0x20000;

        /// <summary>
        /// Represents a constant value for the WM_NCPAINT message.
        /// </summary>
        public const int WM_NCPAINT = 0x85;

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

        /// <summary>
        /// Represents a constant value for the WM_DWMCOMPOSITIONCHANGED message.
        /// </summary>
        public const int WM_DWMCOMPOSITIONCHANGED = 0x31e;
        #endregion

        #region Methods
        /// <summary>
        /// Enables or disables the blur effect behind a window.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="pBlurBehind">A pointer to a DWM_BLURBEHIND structure that describes the blur-behind area.</param>
        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmEnableBlurBehindWindow(IntPtr hWnd, DWM_BLURBEHIND pBlurBehind);

        /// <summary>
        /// Queries the composition state of Desktop Window Manager (DWM).
        /// </summary>
        /// <param name="isEnabled">Receives the current composition state. True if composition is enabled; otherwise, false.</param>
        /// <returns>Returns S_OK if successful; otherwise, an HRESULT error code.</returns>
        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(out bool isEnabled);

        /// <summary>
        /// Extends the window frame into the client area.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="pMarInset">A pointer to a MARGINS structure that describes the margins to use when extending the frame into the client area.</param>
        /// <returns>Returns S_OK if successful; otherwise, an HRESULT error code.</returns>
        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        /// <summary>
        /// Sets the value of Desktop Window Manager (DWM) non-client rendering attributes.
        /// </summary>
        /// <param name="hwnd">A handle to the window.</param>
        /// <param name="attr">The attribute to set.</param>
        /// <param name="attrValue">The value to set.</param>
        /// <param name="attrSize">The size of the value being set.</param>
        /// <returns>Returns S_OK if successful; otherwise, an HRESULT error code.</returns>
        [DllImport("dwmapi.dll")]
        internal static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        /// <summary>
        /// Sets the value of Desktop Window Manager (DWM) non-client rendering attributes.
        /// </summary>
        /// <param name="hwnd">A handle to the window.</param>
        /// <param name="dwAttribute">The attribute to set.</param>
        /// <param name="pvAttribute">The value to set.</param>
        /// <param name="cbAttribute">The size, in bytes, of the pvAttribute value.</param>
        /// <returns>Returns S_OK if successful; otherwise, an HRESULT error code.</returns>
        [DllImport("dwmapi.dll")]
        internal static extern int DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE dwAttribute, ref int pvAttribute, int cbAttribute);

        /// <summary>
        /// Sets the Desktop Window Manager (DWM) colorization parameters.
        /// </summary>
        /// <param name="parameters">A pointer to a DWM_COLORIZATION_PARAMS structure that specifies the colorization parameters.</param>
        /// <param name="unknown">Unknown parameter.</param>
        /// <returns>Returns S_OK if successful; otherwise, an HRESULT error code.</returns>
        [DllImport("dwmapi.dll", EntryPoint = "#131", PreserveSig = false)]
        public static extern void DwmSetColorizationParameters(ref DWM_COLORIZATION_PARAMS parameters, bool unknown);

        /// <summary>
        /// Retrieves the current colorization parameters used by the Desktop Window Manager (DWM).
        /// </summary>
        /// <remarks>This method is a P/Invoke wrapper for the DWM API function and requires the
        /// "dwmapi.dll" library. It does not set the last error, so error handling should be implemented based on the
        /// context of usage.</remarks>
        /// <param name="parameters">When this method returns, contains the <see cref="DWM_COLORIZATION_PARAMS"/> structure with the current
        /// colorization settings. This parameter is passed uninitialized.</param>
        [DllImport("dwmapi.dll", EntryPoint = "#127", SetLastError = false)]
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

        /// <summary>
        /// Enumerates the DWM (Desktop Window Manager) window attributes.
        /// </summary>
        public enum DWMWINDOWATTRIBUTE : uint
        {
            /// <summary>
            /// Enables or disables non-client rendering in the window.
            /// </summary>
            NCRENDERING_ENABLED,

            /// <summary>
            /// Sets the non-client rendering policy.
            /// </summary>
            NCRENDERING_POLICY,

            /// <summary>
            /// Forces the window's transitions to be disabled.
            /// </summary>
            TRANSITIONS_FORCEDISABLED,

            /// <summary>
            /// Allows non-client area painting.
            /// </summary>
            ALLOW_NCPAINT,

            /// <summary>
            /// Retrieves the bounds of the caption button area in the window's title bar.
            /// </summary>
            CAPTION_BUTTON_BOUNDS,

            /// <summary>
            /// Sets the non-client area right-to-left (RTL) layout.
            /// </summary>
            NONCLIENT_RTL_LAYOUT,

            /// <summary>
            /// Forces the window to display an iconic representation in the taskbar.
            /// </summary>
            FORCE_ICONIC_REPRESENTATION,

            /// <summary>
            /// Sets the Flip3D policy for the window.
            /// </summary>
            FLIP3D_POLICY,

            /// <summary>
            /// Retrieves the extended frame bounds.
            /// </summary>
            EXTENDED_FRAME_BOUNDS,

            /// <summary>
            /// Determines if the window has an iconic bitmap.
            /// </summary>
            HAS_ICONIC_BITMAP,

            /// <summary>
            /// Disallows Peek functionality on the window.
            /// </summary>
            DISALLOW_PEEK,

            /// <summary>
            /// Specifies whether the window is excluded from Peek functionality.
            /// </summary>
            EXCLUDED_FROM_PEEK,

            /// <summary>
            /// Cloaks or uncloaks the window.
            /// </summary>
            CLOAK,

            /// <summary>
            /// Retrieves the cloaked state of the window.
            /// </summary>
            CLOAKED,

            /// <summary>
            /// Freezes or unfreezes the window's representation in the DWM thumbnail.
            /// </summary>
            FREEZE_REPRESENTATION,

            /// <summary>
            /// Sets the update mode for non-client rendering.
            /// </summary>
            PASSIVE_UPDATE_MODE,

            /// <summary>
            /// Uses the host's backdrop brush.
            /// </summary>
            USE_HOSTBACKDROPBRUSH,

            /// <summary>
            /// Uses immersive dark mode before Windows 10, version 2004.
            /// </summary>
            USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19,

            /// <summary>
            /// Uses immersive dark mode.
            /// </summary>
            USE_IMMERSIVE_DARK_MODE = 20,

            /// <summary>
            /// Specifies the preferred corner for the window's top-left corner when window resizing is allowed.
            /// </summary>
            WINDOW_CORNER_PREFERENCE = 33,

            /// <summary>
            /// Represents the system backdrop type attribute for a window, used to specify the visual style of the
            /// window's background.
            /// </summary>
            /// <remarks>This value corresponds to the DWM (Desktop Window Manager) attribute for
            /// setting the system backdrop type. It is typically used with the <see cref="DwmSetWindowAttribute"/>
            /// function to configure the appearance of a window's background.</remarks>
            DWMWA_SYSTEMBACKDROP_TYPE = 38,

            /// <summary>
            /// Sets the border color.
            /// </summary>
            BORDER_COLOR,

            /// <summary>
            /// Sets the caption color.
            /// </summary>
            CAPTION_COLOR,

            /// <summary>
            /// Sets the text color.
            /// </summary>
            TEXT_COLOR,

            /// <summary>
            /// Retrieves the visible frame border thickness.
            /// </summary>
            VISIBLE_FRAME_BORDER_THICKNESS,

            /// <summary>
            /// Specifies the system backdrop type.
            /// </summary>
            SYSTEMBACKDROP_TYPE,

            /// <summary>
            /// Specifies the Mica effect attribute.
            /// </summary>
            MICA_EFFECT = 1029,

            /// <summary>
            /// The last value in the enumeration.
            /// </summary>
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

        #endregion
    }
}
