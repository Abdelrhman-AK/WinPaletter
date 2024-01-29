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
        /// A boolean that determines if OS is Windows XP
        /// </summary>
        public static bool WXP => _wxp;
        private readonly static bool _wxp = Environment.OSVersion.Version.Major == 5;

        /// <summary>
        /// A boolean that determines if OS is Windows Vista
        /// </summary>
        public static bool WVista => _wvista;
        private readonly static bool _wvista = Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 0;

        /// <summary>
        /// A boolean that determines if OS is Windows 7 or not
        /// </summary>
        public static bool W7 => _w7;
        private readonly static bool _w7 = Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 1;

        /// <summary>
        /// A boolean that determines if OS is Windows 8 or not
        /// </summary>
        public static bool W8 => _w8;
        private readonly static bool _w8 = Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 2;

        /// <summary>
        /// A boolean that determines if OS is Windows 8.1 or not
        /// </summary>
        public static bool W81 => _w81;
        private readonly static bool _w81 = Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 3;

        /// <summary>
        /// A boolean that determines if OS is Windows 8 or Windows 8.1 or not
        /// </summary>
        public static bool W8x => _w8 || _w81;

        /// <summary>
        /// A boolean that determines if OS is Windows 10 or not
        /// </summary>
        public static bool W10 => _w10;
        private readonly static bool _w10 = Environment.OSVersion.Version.Major == 10 && Environment.OSVersion.Version.Minor == 0 && Environment.OSVersion.Version.Build < 22000;

        /// <summary>
        /// A boolean that determines if OS is Windows 11 or not
        /// </summary>
        public static bool W11 => _w11;
        private readonly static bool _w11 = Environment.OSVersion.Version.Major == 10 && Environment.OSVersion.Version.Minor == 0 && Environment.OSVersion.Version.Build >= 22000;

        /// <summary>
        /// A boolean that determines if OS is Windows 12 or not (For near future! :))
        /// <br></br>
        /// <br>Value is got from OS name, or NT version. Nothing is known until Windows 12 releases are dropped</br>
        /// </summary>
        public static bool W12 => _w12;
        private readonly static bool _w12 = RuntimeInformation.OSDescription.Contains("12") || Environment.OSVersion.Version.Major > 10 || (Environment.OSVersion.Version.Major == 10 && Environment.OSVersion.Version.Minor > 0);

        /// <summary>
        /// A boolean that determines if OS is Windows 10 (19H2 = 1909) or higher or not
        /// </summary>
        public static bool W10_1909 => _w10_1909;
        private readonly static bool _w10_1909 = (!_wxp && !_wvista && !_w7 && !_w8 && !_w81 && !_w10) || _w10 && Convert.ToInt32(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", 0).ToString()) >= 1909;

        /// <summary>
        /// A boolean that determines if OS is Windows 10 (20H2 = 2004 = 19041) or higher or not
        /// </summary>
        public static bool W10_2004 => _w10_2004;
        private readonly static bool _w10_2004 = (!_wxp && !_wvista && !_w7 && !_w8 && !_w81 && !_w10) || _w10 && Environment.OSVersion.Version.Build >= 19041;

        /// <summary>
        /// A boolean that determines if OS is Windows 11 build 22523 or higher or not
        /// </summary>
        public static bool W11_22523 => _w11_22523;
        private readonly static bool _w11_22523 = (!_wxp && !_wvista && !_w7 && !_w8 && !_w81 && !_w10 && !_w11) || _w11 && Environment.OSVersion.Version.Build >= 22523;

        /// <summary>
        /// Get proper Windows name, returned as string differs according to current WinPaletter language.
        /// </summary>
        public static string Name
        {
            get
            {
                if (W12) { return Program.Lang.OS_Win12; }
                else if (W11) { return Program.Lang.OS_Win11; }
                else if (W10) { return Program.Lang.OS_Win10; }
                else if (W81) { return Program.Lang.OS_Win81; }
                else if (W8) { return Program.Lang.OS_Win8; }
                else if (W7) { return Program.Lang.OS_Win7; }
                else if (WVista) { return Program.Lang.OS_WinVista; }
                else if (WXP) { return Program.Lang.OS_WinXP; }
                else { return Program.Lang.OS_WinUndefined; }
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

                string X1 = $".{(GetReg("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "UBR", 0))}";
                if (X1 == ".0") { X1 = string.Empty; }

                return $"{X0}{X1}";
            }
        }

        /// <summary>
        /// Get if current Windows edition is 32-bit or 64-bit, returned as string differs according to current WinPaletter language.
        /// </summary>
        public static string Architecture => Environment.Is64BitOperatingSystem ? Program.Lang.OS_64Bit : Program.Lang.OS_32Bit;

        /// <summary>
        /// Get if current Windows edition is 32-bit or 64-bit, in English.
        /// </summary>
        public static string Architecture_English => Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit";

    }
}