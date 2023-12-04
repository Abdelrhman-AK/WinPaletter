namespace WinPaletter
{
    public partial class Localizer
    {
        public string Bug_NoReport { get; set; } = "There is no previous saved report in '{0}'";
        public string InvalidTheme { get; set; } = "Error: Invalid theme file.";
        public string ThemeNotExist { get; set; } = "Theme file doesn't exist.";
        public string SettingsFileNotExist { get; set; } = "Settings file doesn't exist";
        public string SettingsFileNotJSON { get; set; } = "This settings file is not a valid JSON";
        public string Win11ColorsDescTip { get; set; } = "These description labels are dependent on the latest stable Windows 11. If your Windows is outdated, these labels might not be the same as your current system.";
        public string Win11ColorsDescTip2 { get; set; } = "And if you installed ExplorerPatcher and uninstalled it, WinPaletter will detect ExplorerPatcher is still installed (due do ExplorerPatcher registry remnants) and so the descriptions will be different. You can solve this by going to Settings > ExplorerPatcher and then disable preview synchronization.";
        public string AltTab_Unsupported { get; set; } = "Windows Switcher isn't supported in {0} as it is a classic switcher that can't be changed by registry. Change the preview to another OS and try again.";
        public string VistaLogonNotSupported { get; set; } = "Editing Windows Vista LogonUI with registry is not supported. Change the preview to another OS and try again.";
        public string MonitorIssue { get; set; } = @"Error occurred during loading registry monitor (Used in real-time-detection of wallpaper\dark mode change from registry). Resetting your wallpaper may fix the issue.";
        public string MonitorIssue2 { get; set; } = "Anyway, loading will continue without it.";
        public string LogoffQuestion { get; set; } = "Are you sure you want to log off?";
        public string RestartQuestion { get; set; } = "Are you sure you want to restart your Windows?";
        public string ShutdownQuestion { get; set; } = "Are you sure you want to shut down your Windows?";
        public string LogoffAlert1 { get; set; } = "This will close all open files and applications";
        public string LogoffAlert2 { get; set; } = "Logoff equals 'Sign out' in Windows 8.1, 10 & 11";
        public string LogoffNotFound { get; set; } = "Couldn't find logoff process in '{0}' directory. You should logoff your Windows profile manually.";
        public string ShutdownNotFound { get; set; } = "Couldn't find shutdown.exe in '{0}' directory. You should restart or shutdown your Windows manually.";
        public string ScalingTip { get; set; } = "Scaling option is only a preview, the cursor will be saved with different sizes and the suitable size will be loaded according to your DPI settings.";
        public string RemoveExtMsg { get; set; } = "Are you sure from removing files association (*.wpth, *.wptp, *.wpsf) from registry?";
        public string RemoveExtMsgNote { get; set; } = "Note: You can reassociate them by activating its checkbox and restarting WinPaletter";
        public string EmptyName { get; set; } = "You can't leave theme name empty. Please type a name to it";
        public string EmptyAuthorName { get; set; } = "You can't leave author's name empty. Please type author's name or your name.";
        public string EmptyVer { get; set; } = "You can't leave theme version empty. Please type a version to it in this style (x.x.x.x), replacing (x) by numbers";
        public string WrongVerFormat { get; set; } = "Wrong version format. Please type the version to it in this style (x.x.x.x), replacing (x) by numbers.";
        public string RT_UseWinUpdate { get; set; } = "Do you want to use the online source for repair?";
        public string RT_UseInstallWIM { get; set; } = "If you pressed no, an open file dialog will appear to select install.wim/install.esd file that will be repair source.";
        public string RestartExplorerIssue0 { get; set; } = "Restarting Explorer using an elevated WinPaletter on another user can cause issues with the desktop and taskbar. As a result, WinPaletter won't be able to restart Explorer";
        public string RestartExplorerIssue1 { get; set; } = "Try restarting Explorer yourself, or log off and log back on to your Windows profile to ensure the theme is fully applied.";
    }
}
