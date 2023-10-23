using System;

namespace WinPaletter.GlobalVariables
{
    /// <summary>
    /// Extended paths
    /// <br></br><b>Class contains strings have paths to system and application important directories and files</b>
    /// </summary>
    public static class PathsExt
    {
        #region WinPaletter
        /// <summary>
        /// WinPaletter application data folder
        /// </summary>
        public readonly static string appData = System.IO.Directory.GetParent(System.Windows.Forms.Application.LocalUserAppDataPath).FullName;

        /// <summary>
        /// WinPaletter elevator process file path
        /// </summary>
        public readonly static string WPElevator = $"{appData}\\WinPaletter.Elevator.exe";

        /// <summary>
        /// WinPaletter system events sounds invoker
        /// </summary>
        public readonly static string WPSysEventsSounds = $"{appData}\\WinPaletter.SysEventsSounds.exe";

        /// <summary>
        /// WinPaletter Store cache directory
        /// </summary>
        public readonly static string StoreCache = $"{appData}\\Store";

        /// <summary>
        /// WinPaletter themes resources pack extraction directory
        /// </summary>
        public readonly static string ThemeResPackCache = $"{appData}\\ThemeResPack_Cache";

        /// <summary>
        /// WinPaletter cursors directory (that cursors are rendered into)
        /// </summary>
        public readonly static string CursorsWP = $"{appData}\\Cursors";
        #endregion

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
        public readonly static string UserProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        /// <summary>
        /// Local app data directory
        /// </summary>
        public readonly static string LocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        /// <summary>
        /// Program files directory
        /// </summary>
        public readonly static string ProgramFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        #endregion

        #region System processes
        /// <summary>
        /// Explorer process file path
        /// </summary>
        public readonly static string explorer = $"{Windows}\\explorer.exe";

        /// <summary>
        /// Task Scheduler command process file path
        /// </summary>
        public readonly static string SchTasks = $"{System32}\\schtasks.exe";

        /// <summary>
        /// Take ownership command process file path
        /// </summary>
        public readonly static string TakeOwn = $"{System32}\\takeown.exe";
        #endregion

        #region System PE files
        /// <summary>
        /// Imageres.dll PE file
        /// </summary>
        public readonly static string imageres = $"{System32}\\imageres.dll";
        #endregion

        #region Windows themes
        /// <summary>
        /// Temporary theme file (for preview)
        /// </summary>
        public static string MSTheme = $"{appData}\\VisualStyles\\Luna\\luna.theme";
        #endregion

        #region Consoles\Terminals
        /// <summary>
        /// Command Prompt process file path
        /// </summary>
        public readonly static string CMD = $"{System32}\\cmd.exe";

        /// <summary>
        /// PowerShell x86 process file path
        /// </summary>
        public readonly static string PS86_app = $"{System32}\\WindowsPowerShell\\v1.0";

        /// <summary>
        /// PowerShell x64 process file path
        /// </summary>
        public readonly static string PS64_app = $"{SysWOW64}\\WindowsPowerShell\\v1.0";

        /// <summary>
        /// PowerShell x86 registry key path
        /// </summary>
        public readonly static string PS86_reg = $"{PS86_app.Replace(Windows, "%SystemRoot%").Replace("\"", "_")}_powershell.exe";

        /// <summary>
        /// PowerShell x64 registry key path
        /// </summary>
        public readonly static string PS64_reg = $"{PS64_app.Replace(Windows, "%SystemRoot%").Replace("\"", "_")}_powershell.exe";

        /// <summary>
        /// Microsoft Terminal JSON settings file
        /// </summary>
        public readonly static string TerminalJSON = $"{LocalAppData}\\Packages\\Microsoft.WindowsTerminal_8wekyb3d8bbwe\\LocalState\\settings.json";

        /// <summary>
        /// Microsoft Terminal Preview JSON settings file
        /// </summary>
        public readonly static string TerminalPreviewJSON = $"{LocalAppData}\\Packages\\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\\LocalState\\settings.json";
        #endregion
    }
}
