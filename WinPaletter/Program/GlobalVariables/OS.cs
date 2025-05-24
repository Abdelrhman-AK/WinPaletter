using System;
using System.Runtime.InteropServices;

namespace WinPaletter.GlobalVariables
{
    /// <summary>
    /// Class contains booleans representing if the current OS is the same as variable name or not.
    /// </summary>
    public static class OS
    {
        /// <summary>
        /// A boolean that determines if OS is Windows WXP
        /// </summary>
        public static bool WXP { get; } = Environment.OSVersion.Version.Major == 5;

        /// <summary>
        /// A boolean that determines if OS is Windows Vista
        /// </summary>
        public static bool WVista { get; } = Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 0;

        /// <summary>
        /// A boolean that determines if OS is Windows 7 or not
        /// </summary>
        public static bool W7 { get; } = Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 1;

        /// <summary>
        /// A boolean that determines if OS is Windows 8 or not
        /// </summary>
        public static bool W8 { get; } = Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 2;

        /// <summary>
        /// A boolean that determines if OS is Windows 8.1 or not
        /// </summary>
        public static bool W81 { get; } = Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 3;

        /// <summary>
        /// A boolean that determines if OS is Windows 8 or Windows 8.1 or not
        /// </summary>
        public static bool W8x => W8 || W81;

        /// <summary>
        /// A boolean that determines if OS is Windows 10 or not
        /// </summary>
        public static bool W10 { get; } = Environment.OSVersion.Version.Major == 10 && Environment.OSVersion.Version.Minor == 0 && Environment.OSVersion.Version.Build < 22000;

        /// <summary>
        /// A boolean that determines if OS is Windows 11 or not
        /// </summary>
        public static bool W11 { get; } = Environment.OSVersion.Version.Major == 10 && Environment.OSVersion.Version.Minor == 0 && Environment.OSVersion.Version.Build >= 22000;

        /// <summary>
        /// A boolean that determines if OS is Windows 12 or not (For near future! :))
        /// <br></br>
        /// <br>Value is got from OS name, or NT version. Nothing is known until Windows 12 releases are dropped</br>
        /// </summary>
        public static bool W12 { get; } = RuntimeInformation.OSDescription.Contains("12") || Environment.OSVersion.Version.Major > 10 || (Environment.OSVersion.Version.Major == 10 && Environment.OSVersion.Version.Minor > 0);

        /// <summary>
        /// A boolean that determines if OS is Windows 10 (19H2 = 1909) or higher or not
        /// </summary>
        public static bool W10_1909 { get; } = (!WXP && !WVista && !W7 && !W8x && !W10) || W11 && Convert.ToInt32(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", 0).ToString()) >= 1909;

        /// <summary>
        /// A boolean that determines if OS is Windows 10 (20H2 = 2004 = 19041) or higher or not
        /// </summary>
        public static bool W10_2004 { get; } = (!WXP && !WVista && !W7 && !W8x && !W10) || W11 && Environment.OSVersion.Version.Build >= 19041;

        /// <summary>
        /// A boolean that determines if OS is Windows 11 build 22523 or higher or not
        /// </summary>
        public static bool W11_22523 { get; } = (!WXP && !WVista && !W7 && !W8x && !W10 && !W11) || W11 && Environment.OSVersion.Version.Build >= 22523;

        /// <summary>
        /// Get proper Windows name, returned as string differs according to current WinPaletter language.
        /// </summary>
        public static string Name
        {
            get
            {
                if (W12) { return Program.Lang.Strings.Windows.W12; }
                else if (W11) { return Program.Lang.Strings.Windows.W11; }
                else if (W10) { return Program.Lang.Strings.Windows.W10; }
                else if (W81) { return Program.Lang.Strings.Windows.W81; }
                else if (W8) { return Program.Lang.Strings.Windows.W8; }
                else if (W7) { return Program.Lang.Strings.Windows.W7; }
                else if (WVista) { return Program.Lang.Strings.Windows.WVista; }
                else if (WXP) { return Program.Lang.Strings.Windows.WXP; }
                else { return Program.Lang.Strings.Windows.Undefined; }
            }
        }

        /// <summary>
        /// Get proper Windows name in English.
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
        /// Get current Windows build
        /// </summary>
        public static string Build
        {
            get
            {
                string X0 = RuntimeInformation.OSDescription.Replace("Microsoft Windows ", string.Empty);
                X0 = X0.Replace("S", string.Empty).Trim();

                string X1 = $".{GetReg("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "UBR", 0)}";
                if (X1 == ".0") { X1 = string.Empty; }

                return $"{X0}{X1}";
            }
        }

        /// <summary>
        /// Get if current Windows edition is 32-bit or 64-bit, returned as string differs according to current WinPaletter language.
        /// </summary>
        public static string Architecture => Environment.Is64BitOperatingSystem ? Program.Lang.Strings.Windows.Arc_64Bit : Program.Lang.Strings.Windows.Arc_32Bit;

        /// <summary>
        /// Get if current Windows edition is 32-bit or 64-bit, in English.
        /// </summary>
        public static string Architecture_English => Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit";

    }
}