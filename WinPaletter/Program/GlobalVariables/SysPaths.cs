using System;
using Application = System.Windows.Forms.Application;

namespace WinPaletter.GlobalVariables
{
    /// <summary>
    /// System paths
    /// <br></br><b>Class contains strings have paths to system and application-related directories and files</b>
    /// </summary>
    public static class SysPaths
    {
        #region System directories
        /// <summary>
        /// Windows directory
        /// </summary>
        public readonly static string Windows = Environment.GetFolderPath(Environment.SpecialFolder.Windows).Replace("WINDOWS", "Windows");

        /// <summary>
        /// System32 directory
        /// </summary>
        public readonly static string System32 = $"{Windows}\\System32";

        /// <summary>
        /// SysWOW64 directory
        /// </summary>
        public readonly static string SysWOW64 = $"{Windows}\\SysWOW64";

        /// <summary>
        /// User profile directory
        /// </summary>
        public static string UserProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        /// <summary>
        /// Local app data directory
        /// </summary>
        public static string LocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        /// <summary>
        /// Program files directory
        /// </summary>
        public readonly static string ProgramFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

        /// <summary>
        /// Program files directory
        /// </summary>
        public readonly static string ProgramFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        #endregion

        #region WinPaletter
        /// <summary>
        /// WinPaletter application data folder
        /// </summary>
        public static string appData = System.IO.Directory.GetParent(Application.LocalUserAppDataPath).FullName;

        /// <summary>
        /// WinPaletter application data folder
        /// </summary>
        public static string ProgramFilesData => $"{ProgramFiles}\\{Application.ProductName}\\Data";

        /// <summary>
        /// Directory for storing exceptions logs
        /// </summary>
        public static string Reports => $"{appData}\\Reports";

        /// <summary>
        /// Directory for storing exceptions logs
        /// </summary>
        public static string Logs => $"{appData}\\Logs";

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
        #endregion

        #region System processes
        /// <summary>
        /// Explorer process File path
        /// </summary>
        public readonly static string Explorer = $"{Windows}\\explorer.exe";

        /// <summary>
        /// Task Scheduler command process File path
        /// </summary>
        public readonly static string SchTasks = $"{System32}\\schtasks.exe";

        /// <summary>
        /// Take ownership command process File path
        /// </summary>
        public readonly static string TakeOwn = $"{System32}\\takeown.exe";
        #endregion

        #region System PE files
        /// <summary>
        /// Imageres.dll PE File
        /// </summary>
        public readonly static string imageres = $"{System32}\\imageres.dll";
        #endregion

        #region Windows themes
        /// <summary>
        /// Temporary theme directory (for preview)
        /// </summary>
        public static string MSTheme_Temp => $"{appData}\\VisualStyles\\temp.theme";

        /// <summary>
        /// Temporary theme directory (for preview)
        /// </summary>
        public static string MSTheme_Luna_theme => $"{appData}\\VisualStyles\\Luna\\Luna.theme";

        /// <summary>
        /// Temporary theme directory (for preview)
        /// </summary>
        public static string MSTheme_Dir => $"{appData}\\VisualStyles\\Luna";

        /// <summary>
        /// Extracted Luna.zip
        /// </summary>
        public static string MSTheme_ZIP => $"{appData}\\VisualStyles\\Luna\\Luna.zip";

        /// <summary>
        /// Represents the file path to the Aero theme's .msstyles file.
        /// </summary>
        public static string AeroMSSTYLES = $"{Windows}\\Resources\\Themes\\aero\\aero.msstyles";

        /// <summary>
        /// Gets the file path to the Aero Lite theme's .msstyles file.
        /// </summary>
        public static string AeroLiteMSSTYLES = $"{Windows}\\Resources\\Themes\\aero\\aerolite.msstyles";

        /// <summary>
        /// Represents the file path to the Luna theme's .msstyles file.
        /// </summary>
        public static string LunaMSSTYLES = $"{Windows}\\Resources\\Themes\\Luna\\Luna.msstyles";

        #endregion

        #region Consoles\Terminals
        /// <summary>
        /// Command Prompt process File path
        /// </summary>
        public readonly static string CMD = $"{System32}\\cmd.exe";

        /// <summary>
        /// PowerShell x86 process File directory
        /// </summary>
        public readonly static string PS86_dir = $"{System32}\\WindowsPowerShell\\v1.0";

        /// <summary>
        /// PowerShell x86 process File path
        /// </summary>
        public readonly static string PS86_app = $"{PS86_dir}\\powershell.exe";

        /// <summary>
        /// PowerShell x64 process File directory
        /// </summary>
        public readonly static string PS64_dir = $"{SysWOW64}\\WindowsPowerShell\\v1.0";

        /// <summary>
        /// PowerShell x64 process File path
        /// </summary>
        public readonly static string PS64_app = $"{PS64_dir}\\powershell.exe";

        /// <summary>
        /// PowerShell x86 registry key path
        /// </summary>
        public readonly static string PS86_reg = $"{PS86_dir.Replace(Windows, "%SystemRoot%").Replace("\"", "_")}_powershell.exe";

        /// <summary>
        /// PowerShell x64 registry key path
        /// </summary>
        public readonly static string PS64_reg = $"{PS64_dir.Replace(Windows, "%SystemRoot%").Replace("\"", "_")}_powershell.exe";

        /// <summary>
        /// Microsoft Terminal JSON settings File
        /// </summary>
        public static string TerminalJSON => $"{LocalAppData}\\Packages\\Microsoft.WindowsTerminal_8wekyb3d8bbwe\\LocalState\\settings.json";

        /// <summary>
        /// Microsoft Terminal Preview JSON settings File
        /// </summary>
        public static string TerminalPreviewJSON => $"{LocalAppData}\\Packages\\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\\LocalState\\settings.json";
        #endregion
    }
}
