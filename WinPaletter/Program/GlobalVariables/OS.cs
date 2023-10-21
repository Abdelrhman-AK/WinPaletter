using System;

namespace WinPaletter.GlobalVariables
{
    /// <summary>
    /// Class contains booleans, represent if the current OS is the same as variable name or not.
    /// </summary>
    public static class OS
    {
        /// <summary>
        /// A boolean that represents if OS is Windows XP
        /// </summary>
        public readonly static bool WXP = Environment.OSVersion.Version.Major == 5;

        /// <summary>
        /// A boolean that represents if OS is Windows Vista
        /// </summary>
        public readonly static bool WVista = Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 0;

        /// <summary>
        /// A boolean that represents if OS is Windows 7 or not
        /// </summary>
        public readonly static bool W7 = Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 1;

        /// <summary>
        /// A boolean that represents if OS is Windows 8 or not
        /// </summary>
        public readonly static bool W8 = Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 2;

        /// <summary>
        /// A boolean that represents if OS is Windows 8.1 or not
        /// </summary>
        public readonly static bool W81 = Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 3;

        /// <summary>
        /// A boolean that represents if OS is Windows 10 or not
        /// </summary>
        public readonly static bool W10 = Environment.OSVersion.Version.Major == 10 && Environment.OSVersion.Version.Minor == 0 && Environment.OSVersion.Version.Build < 22000;

        /// <summary>
        /// A boolean that represents if OS is Windows 11 or not
        /// </summary>
        public readonly static bool W11 = Environment.OSVersion.Version.Major == 10 && Environment.OSVersion.Version.Minor == 0 && Environment.OSVersion.Version.Build >= 22000;

        /// <summary>
        /// A boolean that represents if OS is Windows 12 or not (For near future! :))
        /// </summary>
        public readonly static bool W12 = System.Runtime.InteropServices.RuntimeInformation.OSDescription.Contains("12") || Environment.OSVersion.Version.Major >= 10;

        /// <summary>
        /// A boolean that represents if OS is Windows 10 (19H2=1909) or higher or not at all
        /// </summary>
        public readonly static bool W10_1909 = W12 || W11 || W10 && Convert.ToInt32(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", 0).ToString()) >= 1909;

        /// <summary>
        /// A boolean that represents if OS is Windows 11 Build 22523 or higher or not at all
        /// </summary>
        public readonly static bool W11_22523 = W12 || W11 && Convert.ToInt32(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentBuild", 0).ToString()) >= 22523;
    }

}
