using System;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter.GlobalVariables
{
    /// <summary>
    /// Provides utility methods and properties for identifying and retrieving information about the current operating
    /// system.
    /// </summary>
    /// <remarks>The <see cref="OS"/> class includes properties to determine the version, architecture, and
    /// specific characteristics of the operating system, as well as methods to retrieve the operating system name based
    /// on various criteria. It is particularly useful for applications that need to adapt their behavior based on the
    /// operating system version or architecture.</remarks>
    public static class OS
    {
        /// <summary>
        /// Gets the version of the operating system.
        /// </summary>
        private static Version _ver => NativeMethods.NTDLL.GetOSVersion();

        private static bool IsRedirecting => Program.Settings?.ThemeApplyingBehavior?.OS_Redirection_Enabled ?? false;

        private static bool IsRedirected(WindowStyle style) => IsRedirecting && Program.Settings.ThemeApplyingBehavior.OS_Redirection_Version == style;

        /// <summary>
        /// Gets a value indicating whether the current operating system is Windows XP.
        /// </summary>
        public static bool WXP => IsRedirecting ? IsRedirected(WindowStyle.WXP) : _wxp;
        private static readonly bool _wxp = _ver.Major == 5 && _ver.Minor == 1;

        /// <summary>
        /// Gets a value indicating whether the current operating system is Windows Vista.
        /// </summary>
        public static bool WVista => IsRedirecting ? IsRedirected(WindowStyle.WVista) : _wvista;
        private static readonly bool _wvista = _ver.Major == 6 && _ver.Minor == 0;

        /// <summary>
        /// Gets a value indicating whether the current operating system is Windows 7.
        /// </summary>
        public static bool W7 => IsRedirecting ? IsRedirected(WindowStyle.W7) : _w7;
        private static readonly bool _w7 = _ver.Major == 6 && _ver.Minor == 1;

        /// <summary>
        /// Gets a value indicating whether the current operating system is Windows 8.
        /// </summary>
        public static bool W8 => IsRedirecting ? IsRedirected(WindowStyle.W8) : _w8;
        private static readonly bool _w8 = _ver.Major == 6 && _ver.Minor == 2;

        /// <summary>
        /// Gets a value indicating whether the current operating system is Windows 8.1.
        /// </summary>
        public static bool W81 => IsRedirecting ? IsRedirected(WindowStyle.W81) : _w81;
        private static readonly bool _w81 = _ver.Major == 6 && _ver.Minor == 3;

        /// <summary>
        /// Gets a value indicating whether the current operating system is Windows 8 or 8.1.
        /// </summary>
        public static bool W8x => IsRedirecting ? IsRedirected(WindowStyle.W8) || IsRedirected(WindowStyle.W81) : _w8x;
        private static readonly bool _w8x = _ver.Major == 6 && _ver.Minor >= 2;

        /// <summary>
        /// Gets a value indicating whether the current operating system is Windows 10.
        /// </summary>
        public static bool W10 => IsRedirecting ? IsRedirected(WindowStyle.W10) : _w10;
        private static readonly bool _w10 = _ver.Major == 10 && _ver.Minor == 0 && _ver.Build < 22000;

        /// <summary>
        /// Gets a value indicating whether the current operating system is Windows 11.
        /// </summary>
        public static bool W11 => IsRedirecting ? IsRedirected(WindowStyle.W11) : _w11;
        private static readonly bool _w11 = _ver.Major == 10 && _ver.Minor == 0 && _ver.Build >= 22000;

        /// <summary>
        /// A boolean that determines if OS is Windows 12 or not (prediction only).
        /// </summary>
        public static bool W12 => IsRedirecting ? IsRedirected(WindowStyle.W12) : _w12;
        private static readonly bool _w12 = _ver.Major > 10 || RuntimeInformation.OSDescription.Contains("Windows 12");

        /// <summary>
        /// A flag that determines that Windows 12 is released or not (for UI elements)
        /// </summary>
        public static bool IsWin12Released => false;

        /// <summary>
        /// Gets a value indicating whether the current operating system is Windows 10, version 1909 (19H2 - build 18363) or later.
        /// </summary>
        public static bool W10_1909 => W10;

        /// <summary>
        /// Gets a value indicating whether the current operating system is Windows 10, version 1909 (19H2 - build 18363) or below.
        /// </summary>
        public static bool W10_1909_AndBelow => IsRedirecting ? false : _w10_1909_below;
        private static readonly bool _w10_1909_below = _ver.Major == 10 && _ver.Minor == 0 && _ver.Build <= 18363;

        /// <summary>
        /// Gets a value indicating whether the current operating system is Windows 10, version 2004 (20H1 - build 19041) or later.
        /// </summary>
        public static bool W10_2004 => IsRedirecting ? W10 : _w10_2004;
        private static readonly bool _w10_2004 = _ver.Major == 10 && _ver.Minor == 0 && _ver.Build >= 19041;

        /// <summary>
        /// Gets a value indicating whether the current operating system is Windows 11 (build 22523) or later.
        /// </summary>
        public static bool W11_22523 => IsRedirecting ? W11 : _w11_22523;
        private static readonly bool _w11_22523 = _ver.Major == 10 && _ver.Minor == 0 && _ver.Build >= 22523;

        /// <summary>
        /// Gets a value indicating whether the current operating system is Windows 11 24H2 (build 26100) or later.
        /// </summary>
        public static bool W11_24H2 => IsRedirecting ? W11 : _w11_24h2;
        private static readonly bool _w11_24h2 = _ver.Major == 10 && _ver.Minor == 0 && _ver.Build >= 26100;

        /// <summary>
        /// Gets a value indicating whether the current operating system is Windows 11 25H2 (build 26200) or later.
        /// </summary>
        public static bool W11_25H2 => IsRedirecting ? W11 : _w11_25h2;
        private static readonly bool _w11_25h2 = _ver.Major == 10 && _ver.Minor == 0 && _ver.Build >= 26200;

        /// <summary>
        /// Gets the name of the Windows operating system version based on the current environment.
        /// </summary>
        /// <remarks>The property evaluates the current environment to determine the Windows version and
        /// returns a corresponding name. This property is static and does not require an instance of the
        /// class.</remarks>
        public static string Name
        {
            get
            {
                if (W12) { return Program.Localization.Strings.Windows.W12; }
                else if (W11) { return Program.Localization.Strings.Windows.W11; }
                else if (W10) { return Program.Localization.Strings.Windows.W10; }
                else if (W81) { return Program.Localization.Strings.Windows.W81; }
                else if (W8) { return Program.Localization.Strings.Windows.W8; }
                else if (W7) { return Program.Localization.Strings.Windows.W7; }
                else if (WVista) { return Program.Localization.Strings.Windows.WVista; }
                else if (WXP) { return Program.Localization.Strings.Windows.WXP; }
                else { return Program.Localization.Strings.Windows.Undefined; }
            }
        }

        /// <summary>
        /// Retrieves the name of the operating system corresponding to the specified window style.
        /// </summary>
        /// <remarks>This method maps specific window styles to their corresponding operating system
        /// names. It is useful for  identifying the OS based on UI characteristics or predefined styles.</remarks>
        /// <param name="windowStyle">The <see cref="WindowStyle"/> value representing the style of the window.</param>
        /// <returns>A <see cref="string"/> containing the name of the operating system associated with the specified  <paramref
        /// name="windowStyle"/>. If the style is not recognized, returns a string indicating an undefined operating
        /// system.</returns>
        public static string GetName(WindowStyle windowStyle)
        {
            return windowStyle switch
            {
                PreviewHelpers.WindowStyle.W12 => Program.Localization.Strings.Windows.W12,
                PreviewHelpers.WindowStyle.W11 => Program.Localization.Strings.Windows.W11,
                PreviewHelpers.WindowStyle.W10 => Program.Localization.Strings.Windows.W10,
                PreviewHelpers.WindowStyle.W81 => Program.Localization.Strings.Windows.W81,
                PreviewHelpers.WindowStyle.W8 => Program.Localization.Strings.Windows.W8,
                PreviewHelpers.WindowStyle.W7 => Program.Localization.Strings.Windows.W7,
                PreviewHelpers.WindowStyle.WVista => Program.Localization.Strings.Windows.WVista,
                PreviewHelpers.WindowStyle.WXP => Program.Localization.Strings.Windows.WXP,
                _ => Program.Localization.Strings.Windows.Undefined,
            };
        }

        /// <summary>
        /// Gets the name of the Windows operating system in English based on the detected version.
        /// </summary>
        public static string Name_English
        {
            get
            {
                if (W12) { return "Windows 12"; }
                else if (W11) { return "Windows 11"; }
                else if (W10) { return "Windows 10"; }
                else if (W81) { return "Windows 8.1"; }
                else if (W8) { return "Windows 8"; }
                else if (W7) { return "Windows 7"; }
                else if (WVista) { return "Windows Vista"; }
                else if (WXP) { return "Windows XP"; }
                else { return "Windows 12 or higher"; }
            }
        }

        /// <summary>
        /// Gets the build version of the application as a formatted string.
        /// </summary>
        public static string Build => $"{_ver.Major}.{_ver.Minor}.{_ver.Build}.{_ver.Revision}";

        /// <summary>
        /// Gets the architecture of the operating system as a string.
        /// </summary>
        public static string Architecture => Environment.Is64BitOperatingSystem ? Program.Localization.Strings.Windows.Arc_64Bit : Program.Localization.Strings.Windows.Arc_32Bit;

        /// <summary>
        /// Gets a string representation of the system's architecture in English.
        /// </summary>
        public static string Architecture_English => Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit";
    }
}