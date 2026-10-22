using Microsoft.Win32;
using System;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
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

        private static readonly Regex _w95Regex = new(@"\bWindows\s+95\b", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

        private static readonly Regex _w98Regex = new( @"\bWindows\s+98\b", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

        private static readonly Regex _wMeRegex = new( @"\bWindows\s+(Me|Millennium)\b", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

        // Must be declared BEFORE the static readonly bool fields that use it.
        private static readonly string _description = GetOSDescription();

        private static string GetOSDescription()
        {
            // NT family (NT4/2000/XP/…/11)
            string name = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName", "")?.ToString() ?? "";
            if (!string.IsNullOrEmpty(name)) return name;

            // Win9x family (95/98/98 SE/ME)
            const string key9x = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion";

            name = Registry.GetValue(key9x, "ProductName", "")?.ToString() ?? "";  // e.g. "Microsoft Windows 98"
            if (string.IsNullOrEmpty(name))
                name = Registry.GetValue(key9x, "Version", "")?.ToString() ?? "";  // e.g. "Windows 98"

            if (!string.IsNullOrEmpty(name))
            {
                // Distinguish 98 SE: SubVersionNumber is " A" on SE, and build is 2222
                string sub = (Registry.GetValue(key9x, "SubVersionNumber", "")?.ToString() ?? "").Trim();
                if (name.EndsWith("98", StringComparison.OrdinalIgnoreCase) && (sub.Equals("A", StringComparison.OrdinalIgnoreCase) || Environment.OSVersion.Version.Build == 2222))
                {
                    name += " Second Edition";
                }
                return name;
            }

            // Last resort: derive from Environment.OSVersion
            return FromOSVersion();
        }

        private static string FromOSVersion()
        {
            OperatingSystem os = Environment.OSVersion;
            if (os.Platform == PlatformID.Win32Windows)
            {
                Version v = os.Version;
                if (v.Major == 4)
                {
                    switch (v.Minor)
                    {
                        case 0: return "Microsoft Windows 95";
                        case 10:
                            return v.Build == 2222
                                        ? "Microsoft Windows 98 Second Edition"
                                        : "Microsoft Windows 98";
                        case 90: return "Microsoft Windows Millennium Edition";
                    }
                }
            }
            return os.VersionString; // generic fallback
        }

        /// <summary>
        /// Gets a value indicating whether the current operating system is Windows 9x.
        /// </summary>
        public static bool W9x => W95 || W98 || WMe;

        /// <summary>
        /// Gets a value indicating whether the current operating system is Windows 95.
        /// </summary>
        public static bool W95 => IsRedirecting ? IsRedirected(WindowStyle.W9x) : _w95;
        private static readonly bool _w95 = _w95Regex.IsMatch(_description ?? "");

        /// <summary>
        /// Gets a value indicating whether the current operating system is Windows 98.
        /// </summary>
        public static bool W98 => IsRedirecting ? IsRedirected(WindowStyle.W9x) : _w98;
        private static readonly bool _w98 = _w98Regex.IsMatch(_description ?? "");

        /// <summary>
        /// Gets a value indicating whether the current operating system is Windows Me.
        /// </summary>
        public static bool WMe => IsRedirecting ? IsRedirected(WindowStyle.W9x) : _wme;
        private static readonly bool _wme = _wMeRegex.IsMatch(_description ?? "");

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
                else if (WMe) { return Program.Localization.Strings.Windows.WMe; }
                else if (W98) { return Program.Localization.Strings.Windows.W98; }
                else if (W95) { return Program.Localization.Strings.Windows.W95; }
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
                PreviewHelpers.WindowStyle.W9x => Program.Localization.Strings.Windows.W9x,
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
                else if (WMe) { return "Windows Millennium Edition"; }
                else if (W98) { return "Windows 98"; }
                else if (W95) { return "Windows 95"; }
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