namespace WinPaletter
{
    public partial class Localizer
    {
        public string TM_CMD_Error { get; set; } = "Error occurred during applying Command Prompt";
        public string TM_PS32_Error { get; set; } = "Error occurred during applying PowerShell x86";
        public string TM_PS64_Error { get; set; } = "Error occurred during applying PowerShell x64";
        public string TM_WIN32UI_Error { get; set; } = "Error occurred during applying Classic Colors";
        public string TM_WinEffects_Error { get; set; } = "Error occurred during applying Windows Effects";
        public string TM_WallpaperTone_Error { get; set; } = "Error occurred during applying Wallpaper Tone";
        public string TM_LogonUI11_Error { get; set; } = "Error occurred during applying Windows 11 LogonUI";
        public string TM_LogonUI10_Error { get; set; } = "Error occurred during applying Windows 10 LogonUI";
        public string TM_LogonUI8_Error { get; set; } = "Error occurred during applying Windows 8.1 Lock Screen";
        public string TM_LogonUI7_Error { get; set; } = "Error occurred during applying Windows 7 LogonUI";
        public string TM_LogonUIXP_Error { get; set; } = "Error occurred during applying Windows XP LogonUI";
        public string TM_W10_Error { get; set; } = "Error occurred during applying Windows 10 scheme";
        public string TM_SavingInfo_Error { get; set; } = "Error occurred during saving theme info into registry";
        public string TM_ThemeReset_Error { get; set; } = "Error occurred during resetting theme to default Windows";
        public string TM_W11_Error { get; set; } = "Error occurred during applying Windows 11 scheme";
        public string TM_W7_Error { get; set; } = "Error occurred during applying Windows 7 theme";
        public string TM_WVista_Error { get; set; } = "Error occurred during applying Windows Vista theme";
        public string TM_W81_Error { get; set; } = "Error occurred during applying Windows 8.1 scheme";
        public string TM_WXP_Error { get; set; } = "Error occurred during applying Windows XP theme";
        public string TM_Error_Cursors { get; set; } = "Error occurred during applying Windows Cursors";
        public string TM_Error_SetDesktop { get; set; } = "Error occurred during applying Desktop for all users";
        public string TM_Error_Metrics { get; set; } = "Error occurred during applying Windows Metrics and Fonts";
        public string TM_Error_AltTab { get; set; } = "Error occurred during applying Windows Switcher (Alt+Tab) appearance";
        public string TM_Error_Wallpaper { get; set; } = "Error occurred during applying Wallpaper";
        public string TM_Error_AppTheme { get; set; } = "Error occurred during applying WinPaletter application theme";
        public string TM_Error_TerminalPreview { get; set; } = "Error occurred during applying Windows Terminal Preview";
        public string TM_Error_TerminalStable { get; set; } = "Error occurred during applying Windows Terminal Stable";
        public string TM_Error_ScreenSaver { get; set; } = "Error occurred during applying Screen Saver";
        public string TM_Error_Sounds { get; set; } = "Error occurred during applying Sounds";
        public string TM_RenderingCursors_Error { get; set; } = "Error occurred during rendering Windows Cursors";
        public string TM_CursorsApplying_Error { get; set; } = "Error occurred during applying Windows Cursors";
        public string TM_RestoreCursorsError { get; set; } = "Error occurred during resetting cursors to aero. Anyway, process will continue.";
        public string TM_RestoreCursorsErrorPressOK { get; set; } = "Pressing OK will show details of exception error.";
        public string TM_FatalErrorHappened { get; set; } = @"Fatal error happened and WinPaletter won't be able to continue theme applying. Press on 'Show Errors' for details.";
        public string ErrorExplorerRestart { get; set; } = "Error in restarting Explorer. Open rescue tools and open Explorer.";
        public string ErrorDetails { get; set; } = "Error details: ";
    }
}
