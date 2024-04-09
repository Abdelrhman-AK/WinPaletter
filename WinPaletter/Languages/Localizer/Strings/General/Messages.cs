namespace WinPaletter
{
    public partial class Localizer
    {
        public string Bug_NoReport { get; set; } = "There are no saved reports located in '{0}'";
        public string InvalidTheme { get; set; } = "Error: Invalid theme file.";
        public string ThemeNotExist { get; set; } = "Theme file doesn't exist.";
        public string SettingsFileNotExist { get; set; } = "Settings file doesn't exist.";
        public string SettingsFileNotJSON { get; set; } = "This settings file is not a valid JSON.";
        public string Win11ColorsDescTip { get; set; } = "These description labels are dependent on the latest stable Windows 11. If your Windows is outdated, these labels might not be the same as your current system.";
        public string Win11ColorsDescTip2 { get; set; } = "If you installed ExplorerPatcher and then uninstalled it, WinPaletter will detect that ExplorerPatcher is still installed (due to ExplorerPatcher registry remnants), and so the descriptions will be different. You can solve this by going to Settings > ExplorerPatcher and then disable preview synchronization.";
        public string AltTab_Unsupported { get; set; } = "Windows Switcher isn't supported in {0} as it is a classic switcher that can't be changed by registry. Change the preview to another OS and try again.";
        public string VistaLogonNotSupported { get; set; } = "Editing Windows Vista LogonUI with the registry is not supported. Change the preview to another OS and try again.";
        public string MonitorIssue { get; set; } = "Error occurred during loading registry monitor (Used in real-time-detection of wallpaper\\dark mode change from registry). Resetting your wallpaper may fix the issue.";
        public string MonitorIssue2 { get; set; } = "Anyway, loading will continue without it.";
        public string LogoffQuestion { get; set; } = "Are you sure you want to log off?";
        public string RestartQuestion { get; set; } = "Are you sure you want to restart your Windows?";
        public string ShutdownQuestion { get; set; } = "Are you sure you want to shut down your Windows?";
        public string LogoffAlert1 { get; set; } = "This will close all open files and applications.";
        public string LogoffAlert2 { get; set; } = "Logoff equals 'Sign out' in Windows 8.1, 10 & 11.";
        public string LogoffNotFound { get; set; } = "Couldn't find the logoff.exe in '{0}' directory. You should log off your Windows profile manually.";
        public string ShutdownNotFound { get; set; } = "Couldn't find shutdown.exe in '{0}' directory. You should restart or shut down your Windows manually.";
        public string RemoveExtMsg { get; set; } = "Are you sure you want to remove file associations (*.wpth, *.wptp, *.wpsf) from the registry?";
        public string RemoveExtMsgNote { get; set; } = "Note: You can reassociate them by activating the checkbox and restarting WinPaletter.";
        public string EmptyName { get; set; } = "You can't leave the theme name empty. Please type a name for it.";
        public string EmptyAuthorName { get; set; } = "You can't leave the author's name empty. Please type the author's name or your name.";
        public string EmptyVer { get; set; } = "You can't leave the theme version empty. Please type a version for it in this style (x.x.x.x), replacing (x) with numbers.";
        public string WrongVerFormat { get; set; } = "Wrong version format. Please type the version for it in this style (x.x.x.x), replacing (x) with numbers.";
        public string RT_UseWinUpdate { get; set; } = "Do you want to use the online source for repair?";
        public string RT_UseInstallWIM { get; set; } = "If you pressed no, an open file dialog will appear to select the install.wim/install.esd file that will be the repair source.";
        public string RestartExplorerIssue0 { get; set; } = "Restarting Explorer using an elevated WinPaletter on another user can cause issues with the desktop and taskbar. As a result, WinPaletter won't be able to restart Explorer.";
        public string RestartExplorerIssue1 { get; set; } = "Try restarting Explorer yourself, or log off and log back on to your Windows profile to ensure the theme is fully applied.";
        public string ReadjustColor { get; set; } = "If you notice any accessibility issues, please readjust the colors accordingly.";
        public string SaveDialog_Question { get; set; } = "Do you want to save the current WinPaletter theme as a file?";
        public string ApplyingMode_ErrorDialog { get; set; } = "You can continue; the theme has been applied without these elements";
        public string LoadingMode_ErrorDialog { get; set; } = "You can continue; the theme has been loaded without these elements";
        public string IconsImport_Shell32_1 { get; set; } = "Practice this feature with caution. Another shell32.dll may have different icons and indexes (IDs), which could lead to incorrect results.";
        public string IconsImport_Shell32_2 { get; set; } = "The best practice is to change every icon one by one manually. Would you like to proceed?";
        public string SecureUxTheme_HookExplorerWarning { get; set; } = "Hooking explorer on Windows 10 is rather pointless, and can cause instability, high memory usage and bad performance in explorer. Do you want to continue?";
        public string SecureUxTheme_Installed { get; set; } = "SecureUxTheme has been installed successfully.";
        public string SecureUxTheme_Uninstalled { get; set; } = "SecureUxTheme has been uninstalled successfully.";
        public string SecureUxTheme_Restart { get; set; } = "Please restart your Windows to apply the changes.";
        public string SecureUxThemeNotInstalled0 { get; set; } = "SecureUxTheme is not installed. Please use the official tool from its GitHub repository instead.";
        public string SecureUxThemeNotInstalled1 { get; set; } = "If it still doesn't work, please visit SecureUxTheme's GitHub repository and follow the instructions provided there. Alternatively, you can report any issues directly in the SecureUxTheme repository, rather than the WinPaletter repository.";
        public string Win12_Preview_Msg0 { get; set; } = "Windows 12 is not fully supported yet. WinPaletter will treat it as Windows 11 until Windows 12 is officially released.";
        public string Win12_Preview_Msg1 { get; set; } = "The main reason is that WinPaletter may not be maintained when Windows 12 is released.";

    }
}