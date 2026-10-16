using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;

namespace WinPaletter.GlobalVariables
{
    /// <summary>
    /// System paths
    /// <br></br><b>Class contains strings have paths to system and application-related directories and files</b>
    /// <br>- Windows 9x (<c>OS.W9x</c>): there is no System32/SysWOW64 (the system directory is <c>%windir%\SYSTEM</c>), no <c>AppData\Local</c> and no <c>CSIDL_PROFILE</c>. Paths are resolved with fallbacks so they are never empty.</br>
    /// <br>- IMPORTANT: the static initializers of this class must never throw and must never touch other WinPaletter classes (<c>OS</c>, <c>Program</c>, ...).
    /// Anything that depends on them is a computed property (<c>=&gt;</c>), evaluated when it is accessed. A throwing initializer makes EVERY member of the class fail with a <see cref="TypeInitializationException"/>.</br>
    /// <br>- Static initializers run in textual order. Keep each initialized property below the ones it uses.</br>
    /// </summary>
    public static class SysPaths
    {
        #region Helpers (private)

        /// <summary>
        /// Gets a special folder path. If Windows can't provide it (returns empty, throws, or the CSIDL is not supported, as on Windows 9x), the fallback is used.
        /// <br>- Never throws and never returns null</br>
        /// </summary>
        private static string GetFolder(Environment.SpecialFolder folder, Func<string> fallback)
        {
            string path = string.Empty;

            try { path = Environment.GetFolderPath(folder); } catch { }

            if (string.IsNullOrWhiteSpace(path))
            {
                try { path = fallback?.Invoke(); } catch { }
            }

            return path ?? string.Empty;
        }

        /// <summary>
        /// Windows directory from the environment (defined on both Windows 9x and NT)
        /// </summary>
        private static string WindowsFallback() => Environment.GetEnvironmentVariable("windir") ?? @"C:\WINDOWS";

        /// <summary>
        /// Windows 9x flag that never throws (if the <c>OS</c> class fails to initialize, it is treated as not Windows 9x)
        /// </summary>
        private static bool IsW9x
        {
            get
            {
                try { return OS.W9x; }
                catch { return false; }
            }
        }

        /// <summary>
        /// Environment variable used to represent the Windows directory in registry values.
        /// <br>- <c>%SystemRoot%</c> doesn't exist on Windows 9x, but <c>%windir%</c> does</br>
        /// </summary>
        private static string SystemRootVar => IsW9x ? "%windir%" : "%SystemRoot%";

        private static string _productName;

        /// <summary>
        /// <see cref="Application.ProductName"/> that never throws.
        /// <br>- It reads the version resource of the executable (or the entry assembly), which can fail when the app is hosted in an unusual way, as on Windows 9x</br>
        /// </summary>
        private static string ProductName
        {
            get
            {
                if (string.IsNullOrEmpty(_productName))
                {
                    try { _productName = Application.ProductName; } catch { }
                    if (string.IsNullOrEmpty(_productName)) _productName = "WinPaletter";
                }

                return _productName;
            }
        }

        private static string _companyName;

        /// <summary>
        /// <see cref="Application.CompanyName"/> that never throws. Falls back to the product name
        /// </summary>
        private static string CompanyName
        {
            get
            {
                if (string.IsNullOrEmpty(_companyName))
                {
                    try { _companyName = Application.CompanyName; } catch { }
                    if (string.IsNullOrEmpty(_companyName)) _companyName = ProductName;
                }

                return _companyName;
            }
        }

        #endregion

        #region System directories
        /// <summary>
        /// Windows directory
        /// </summary>
        public static string Windows { get; } = GetFolder(Environment.SpecialFolder.Windows, WindowsFallback).Replace("WINDOWS", "Windows");

        /// <summary>
        /// System32 directory
        /// <br>- On Windows 9x this is the <c>SYSTEM</c> directory, as there is no System32</br>
        /// </summary>
        public static string System32 => IsW9x ? $"{Windows}\\System" : $"{Windows}\\System32";

        /// <summary>
        /// SysWOW64 directory
        /// <br>- On Windows 9x (32-bit only) this is the same as <see cref="System32"/></br>
        /// </summary>
        public static string SysWOW64 => IsW9x ? System32 : $"{Windows}\\SysWOW64";

        /// <summary>
        /// System resources directory for storing assets should be in *.dll in Windows 10\11 in System32 and SysWOW64, but in lower versions of Windows, it's in System32 directory.
        /// <br>- Doesn't exist on Windows 9x</br>
        /// </summary>
        public static string SystemResources { get; } = $"{Windows}\\SystemResources";

        /// <summary>
        /// Fonts directory
        /// </summary>
        public static string Fonts { get; } = GetFolder(Environment.SpecialFolder.Fonts, () => $"{Windows}\\Fonts");

        /// <summary>
        /// User profile directory
        /// <br>- <c>CSIDL_PROFILE</c> is not supported on Windows 9x, so the Windows directory is used until <c>User.UpdatePathsFromSID</c> sets the real profile</br>
        /// </summary>
        public static string UserProfile { get; set; } = GetFolder(Environment.SpecialFolder.UserProfile, () => Windows);

        /// <summary>
        /// Local app data directory
        /// <br>- Windows 9x has no <c>AppData\Local</c>. <c>Application Data</c> is used instead</br>
        /// </summary>
        public static string LocalAppData { get; set; } = GetFolder(Environment.SpecialFolder.LocalApplicationData, () => GetFolder(Environment.SpecialFolder.ApplicationData, () => $"{UserProfile}\\Application Data"));

        /// <summary>
        /// Program files directory
        /// </summary>
        public static string ProgramFiles { get; } = GetFolder(Environment.SpecialFolder.ProgramFiles, () =>
        {
            // Windows 9x: the location is stored in the registry
            string dir = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion", "ProgramFilesDir", null) as string;

            return !string.IsNullOrWhiteSpace(dir) ? dir : Path.Combine(Path.GetPathRoot(Windows), "Program Files");
        });

        /// <summary>
        /// Program files (x86) directory
        /// <br>- On 32-bit systems (including Windows 9x) this is the same as <see cref="ProgramFiles"/></br>
        /// </summary>
        public static string ProgramFilesX86 { get; } = GetFolder(Environment.SpecialFolder.ProgramFilesX86, () => ProgramFiles);
        #endregion

        #region WinPaletter
        /// <summary>
        /// WinPaletter application data folder
        /// <br>- Built with string concatenation, not <see cref="Path.Combine(string[])"/>, which throws on invalid characters</br>
        /// </summary>
        public static string appData { get; set; } = $"{LocalAppData.TrimEnd('\\')}\\{CompanyName}\\{ProductName}";

        /// <summary>
        /// WinPaletter program files data folder
        /// </summary>
        public static string ProgramFilesData => $"{ProgramFiles}\\{ProductName}\\Data";

        /// <summary>
        /// Directory for storing exceptions logs
        /// </summary>
        public static string Reports => $"{appData}\\Reports";

        /// <summary>
        /// Directory for storing exceptions logs
        /// </summary>
        public static string LogsDir => $"{appData}\\Logs";

        /// <summary>
        /// WinPaletter services directory
        /// </summary>
        public static string Services => $"{ProgramFilesData}\\Services";

        /// <summary>
        /// WinPaletter system events sounds services
        /// </summary>
        public static string SysEventsSoundsDir => $"{Services}\\SysEventsSounds";

        /// <summary>
        /// WinPaletter system events sounds services
        /// </summary>
        public static string SysEventsSounds => $"{SysEventsSoundsDir}\\WinPaletter.SysEventsSounds.exe";

        /// <summary>
        /// WinPaletter system events sounds INI File
        /// </summary>
        public static string SysEventsSounds_Local_INI => $"{appData}\\sounds.ini";

        /// <summary>
        /// WinPaletter system events sounds INI File
        /// </summary>
        public static string SysEventsSounds_Global_INI => $"{SysEventsSoundsDir}\\sounds.ini";

        /// <summary>
        /// WinPaletter Store cache directory
        /// </summary>
        public static string StoreCache => $"{ProgramFilesData}\\Store";

        /// <summary>
        /// WinPaletter themes resources pack extraction directory
        /// </summary>
        public static string ThemeResPackCache => $"{appData}\\ThemeResPack_Cache";

        /// <summary>
        /// WinPaletter cursors directory (that cursors are rendered into)
        /// </summary>
        public static string CursorsWP => $"{appData}\\Cursors";

        public static string ThemesBackup_OnThemeApply => $"{Program.Settings.BackupTheme.BackupPath}\\OnThemeApply";

        public static string ThemesBackup_OnAspectApply => $"{Program.Settings.BackupTheme.BackupPath}\\OnAspectApply";

        public static string ThemesBackup_OnThemeOpen => $"{Program.Settings.BackupTheme.BackupPath}\\OnThemeOpen";

        public static string ThemesBackup_OnAppOpen => $"{Program.Settings.BackupTheme.BackupPath}\\OnAppOpen";

        public static string ThemesBackup_OnExceptionError => $"{Program.Settings.BackupTheme.BackupPath}\\OnExceptionError";

        #endregion

        #region System processes
        /// <summary>
        /// Explorer process File path
        /// </summary>
        public static string Explorer { get; } = $"{Windows}\\explorer.exe";

        /// <summary>
        /// Task Scheduler command process File path
        /// <br>- Doesn't exist on Windows 9x</br>
        /// </summary>
        public static string SchTasks => $"{System32}\\schtasks.exe";

        /// <summary>
        /// Take ownership command process File path
        /// <br>- Doesn't exist on Windows 9x</br>
        /// </summary>
        public static string TakeOwn => $"{System32}\\takeown.exe";
        #endregion

        #region System PE files

        /// <summary>
        /// Imageres.dll PE File
        /// <br>- Doesn't exist on Windows 9x</br>
        /// </summary>
        public static string imageres => $"{System32}\\imageres.dll";

        /// <summary>
        /// Imageres.dll.mun PE File located in SystemResources directory (for Windows 10/11)
        /// </summary>
        public static string imageres_mun { get; } = $"{SystemResources}\\imageres.dll.mun";

        #endregion

        #region Windows themes
        /// <summary>
        /// Temporary theme directory (for preview)
        /// </summary>
        public static string Theme_Temp => $"{appData}\\VisualStyles\\temp.theme";

        /// <summary>
        /// Temporary theme directory (for preview)
        /// </summary>
        public static string Theme_Luna_WP => $"{appData}\\VisualStyles\\Luna\\Luna.theme";

        /// <summary>
        /// Temporary theme msstyles (for preview)
        /// </summary>
        public static string MSSTYLES_Luna_WP => $"{appData}\\VisualStyles\\Luna\\Luna.msstyles";

        /// <summary>
        /// Temporary theme directory (for preview)
        /// </summary>
        public static string Theme_Dir_WP => $"{appData}\\VisualStyles\\Luna";

        /// <summary>
        /// Extracted Luna.zip
        /// </summary>
        public static string Theme_ZIP_WP => $"{appData}\\VisualStyles\\Luna\\Luna.zip";

        /// <summary>
        /// Represents the file path to the Aero theme's .msstyles file.
        /// </summary>
        public static string MSSTYLES_Aero_Win { get; } = $"{Windows}\\Resources\\Themes\\aero\\aero.msstyles";

        /// <summary>
        /// Gets the file path to the Aero Lite theme's .msstyles file.
        /// </summary>
        public static string MSSTYLES_AeroLite_Win { get; } = $"{Windows}\\Resources\\Themes\\aero\\aerolite.msstyles";

        /// <summary>
        /// Represents the file path to the Luna theme's .msstyles file.
        /// </summary>
        public static string MSSTYLES_Luna_Win { get; } = $"{Windows}\\Resources\\Themes\\Luna\\Luna.msstyles";

        #endregion

        #region Consoles\Terminals
        /// <summary>
        /// Command Prompt process File path
        /// <br>- On Windows 9x the command interpreter is <c>command.com</c> (in the Windows directory), as there is no cmd.exe</br>
        /// </summary>
        public static string CMD => IsW9x ? $"{Windows}\\command.com" : $"{System32}\\cmd.exe";

        /// <summary>
        /// PowerShell x86 process File directory
        /// </summary>
        public static string PS86_dir => $"{System32}\\WindowsPowerShell\\v1.0";

        /// <summary>
        /// PowerShell x86 process File path
        /// </summary>
        public static string PS86_app => $"{PS86_dir}\\powershell.exe";

        /// <summary>
        /// PowerShell x64 process File directory
        /// </summary>
        public static string PS64_dir => $"{SysWOW64}\\WindowsPowerShell\\v1.0";

        /// <summary>
        /// PowerShell x64 process File path
        /// </summary>
        public static string PS64_app => $"{PS64_dir}\\powershell.exe";

        /// <summary>
        /// PowerShell x86 registry key path
        /// <br>- Console registry key names use underscores in place of backslashes (e.g. <c>%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe</c>)</br>
        /// </summary>
        public static string PS86_reg => $"{PS86_dir.Replace(Windows, SystemRootVar).Replace("\\", "_")}_powershell.exe";

        /// <summary>
        /// PowerShell x64 registry key path
        /// </summary>
        public static string PS64_reg => $"{PS64_dir.Replace(Windows, SystemRootVar).Replace("\\", "_")}_powershell.exe";

        /// <summary>
        /// Microsoft Terminal JSON settings File
        /// </summary>
        public static string TerminalJSON => $"{LocalAppData}\\Packages\\Microsoft.WindowsTerminal_8wekyb3d8bbwe\\LocalState\\settings.json";

        /// <summary>
        /// Microsoft Terminal Preview JSON settings File
        /// </summary>
        public static string TerminalPreviewJSON => $"{LocalAppData}\\Packages\\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\\LocalState\\settings.json";
        #endregion

        #region Helpers

        /// <summary>
        /// Replaces the beginning of a path that is inside the Windows directory with the system root variable
        /// (<c>%SystemRoot%</c>, or <c>%windir%</c> on Windows 9x)
        /// </summary>
        public static string Normalize(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) return path;

            string systemRoot = GetFolder(Environment.SpecialFolder.Windows, WindowsFallback);

            // An empty root would match every path (StartsWith("") is always true)
            if (string.IsNullOrEmpty(systemRoot)) return path;

            if (path.StartsWith(systemRoot, StringComparison.OrdinalIgnoreCase))
                return SystemRootVar + path.Substring(systemRoot.Length);

            return path;
        }

        #endregion
    }
}