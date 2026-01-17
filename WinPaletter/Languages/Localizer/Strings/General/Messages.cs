namespace WinPaletter
{
    public partial class Localizer
    {
        public partial class Strings_Cls
        {
            public partial class Messages_Cls
            {
                public string SaveMsg { get; set; } = "Do you want to save settings?";
                public string SettingsSaved { get; set; } = "Settings are saved";
                public string Bug_NoReport { get; set; } = "There are no saved reports located in '{0}'";
                public string InvalidTheme { get; set; } = "Error: Invalid theme file.";
                public string ThemeFileNotExist { get; set; } = "Theme file does not exist.";
                public string ThemesDirectoryNotExist { get; set; } = "Directory containing themes files does not exist.";
                public string NoValidThemesFound { get; set; } = "No valid themes were found in the specified directory.";
                public string ThemeResPackNotExist { get; set; } = "Theme resources pack file does not exist.";
                public string SettingsFileNotExist { get; set; } = "Settings file does not exist.";
                public string SettingsFileNotJSON { get; set; } = "This settings file is not a valid JSON.";
                public string AltTab_Unsupported { get; set; } = "Windows Switcher isn't supported in {0} as it is a classic switcher that can't be changed by registry. Change the preview to another OS and try again.";
                public string VistaLogonNotSupported { get; set; } = "Editing Windows Vista LogonUI with the registry is not supported. Change the preview to another OS and try again.";
                public string MonitorIssue { get; set; } = "Error occurred during loading registry monitor (Used in real-time-detection of wallpaper\\dark mode change from registry). Resetting your wallpaper may fix the issue.";
                public string MonitorIssue2 { get; set; } = "Anyway, loading will continue without it.";
                public string LogoffQuestion { get; set; } = "Are you sure you want to log off?";
                public string SignOutQuestion { get; set; } = "Are you sure you want to sign out?";
                public string RestartQuestion { get; set; } = "Are you sure you want to restart your Windows?";
                public string RestartRecoveryQuestion { get; set; } = "Are you sure you want to restart your Windows into recovery mode?";
                public string RestartSafeModeQuestion { get; set; } = "Are you sure you want to restart your Windows into safe mode?";
                public string ShutdownQuestion { get; set; } = "Are you sure you want to shut down your Windows?";
                public string LogoffAlert1 { get; set; } = "This will close all open files and applications.";
                public string LogoffNotFound { get; set; } = "Couldn't find the logoff.exe in '{0}' directory. You should log off your Windows profile manually.";
                public string ShutdownNotFound { get; set; } = "Couldn't find shutdown.exe in '{0}' directory. You should restart or shut down your Windows manually.";
                public string BcdeditNotFound { get; set; } = "Couldn't find bcdedit.exe in '{0}' directory. You should restart your Windows into recovery environment manually.";
                public string RecoveryNotAvailable { get; set; } = "Recovery mode is not available for {0}. You should manually restart Windows into safe mode instead.";
                public string RemoveExtMsg { get; set; } = "Are you sure you want to remove file associations (*.wpth, *.wptp, *.wpsf) from the registry?";
                public string RemoveExtMsgNote { get; set; } = "Note: You can reassociate them by activating the checkbox and restarting WinPaletter.";
                public string EmptyName { get; set; } = "You can't leave the theme name empty. Please type a name for it.";
                public string EmptyAuthorName { get; set; } = "You can't leave the author's name empty. Please type the author's name or your name.";
                public string EmptyVer { get; set; } = "You can't leave the theme version empty. Please type a version for it in this style (x.x.x.x), replacing (x) with numbers.";
                public string WrongVerFormat { get; set; } = "Wrong version format. Please type the version for it in this style (x.x.x.x), replacing (x) with numbers.";
                public string RescueTools_UseWinUpdate { get; set; } = "Do you want to use the online source for repair?";
                public string RescueTools_UseInstallWIM { get; set; } = "If you pressed no, an open file dialog will appear to select the install.wim/install.esd file that will be the repair source.";
                public string RestartExplorerIssue0 { get; set; } = "Restarting Explorer using an elevated WinPaletter on another user can cause issues with the desktop and taskbar. As a result, WinPaletter won't be able to restart Explorer.";
                public string RestartExplorerIssue1 { get; set; } = "Try restarting Explorer yourself, or log off and log back on to your Windows profile to ensure the theme is fully applied.";
                public string ReadjustColor { get; set; } = "If you notice any accessibility issues, please readjust the colors accordingly.";
                public string SaveDialog_Question { get; set; } = "Do you want to save the current WinPaletter theme as a file?";
                public string ApplyingMode_ErrorDialog { get; set; } = "You can continue; the theme has been applied without these elements";
                public string LoadingMode_ErrorDialog { get; set; } = "You can continue; the theme has been loaded without these elements";
                public string InputThemeRepos { get; set; } = "Type a URL to a WinPaletter themes database";
                public string InputThemeRepos_Notice { get; set; } = "This database must follow the guidelines demonstrated in WinPaletter Store documentation";
                public string IconsImport_Shell32_1 { get; set; } = "Practice this feature with caution. Another shell32.dll may have different icons and indexes (IDs), which could lead to incorrect results.";
                public string IconsImport_Shell32_2 { get; set; } = "The best practice is to change every icon one by one manually. Would you like to proceed?";
                public string SecureUxTheme_HookExplorerWarning { get; set; } = "Hooking explorer on Windows 10 is rather pointless, and can cause instability, high memory usage and bad performance in explorer. Do you want to continue?";
                public string SecureUxTheme_Installed { get; set; } = "SecureUxTheme has been installed successfully.";
                public string SecureUxTheme_Uninstalled { get; set; } = "SecureUxTheme has been uninstalled successfully.";
                public string SecureUxTheme_Restart { get; set; } = "Please restart your Windows to apply the changes.";
                public string SecureUxThemeNotInstalled0 { get; set; } = "SecureUxTheme is not installed. Please use the official tool from its GitHub repository instead.";
                public string SecureUxThemeNotInstalled1 { get; set; } = "If it still does not work, please visit SecureUxTheme's GitHub repository and follow the instructions provided there. Alternatively, you can report any issues directly in the SecureUxTheme repository, rather than the WinPaletter repository.";
                public string Win12_Preview_Msg0 { get; set; } = "Windows 12 is not fully supported yet. WinPaletter will treat it as Windows 11 until Windows 12 is officially released.";
                public string Win12_Preview_Msg1 { get; set; } = "The main reason is that WinPaletter may not be maintained when Windows 12 is released.";
                public string OpenTabs_Close { get; set; } = "There are open forms in tabs. Do you want to exit WinPaletter anyway?";
                public string SysRestore_Msg0 { get; set; } = "A system restore point creation query is required to help you revert to a previous Windows theme if you encounter issues.";
                public string SysRestore_Msg1 { get; set; } = "Do you want to enable system restore for system partition `{0}:\\`?";
                public string SysRestore_Msg2 { get; set; } = "System restore point is created successfully.";
                public string RerunSetup_Msg0 { get; set; } = "WinPaletter will launch setup in the next application session.";
                public string ExitWinPaletter { get; set; } = "Do you want to exit WinPaletter now?";
                public string LogToClipboard { get; set; } = "Log content has been copied to the clipboard. You can paste it into a text editor or report it to WinPaletter' GitHub issues.";
                public string LogSaved { get; set; } = "Log file has been saved as '{0}'";
                public string LogCopied { get; set; } = "Log file has been copied to '{0}'";
                public string TerminalDeleteScheme { get; set; } = "Are you sure you want to delete scheme `{0}`?";
                public string TerminalDeleteTheme { get; set; } = "Are you sure you want to delete theme `{0}`?";
                public string CloseWizard { get; set; } = "Are you sure you want to close the wizard? All unsaved changes will be lost.";
            }
        }
    }
}