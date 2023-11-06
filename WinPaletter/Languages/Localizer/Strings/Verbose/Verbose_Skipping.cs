namespace WinPaletter
{
    public partial class Localizer
    {
        public string TM_Skip_TerminalPreview { get; set; } = "Skipping Windows Terminal Preview as it is disabled";
        public string TM_Skip_TerminalStable { get; set; } = "Skipping Windows Terminal Stable as it is disabled";
        public string TM_Skip_Terminals { get; set; } = "Skipping Windows Terminal Stable & Preview as they are disabled";
        public string TM_Skip_Terminals_NotSupported { get; set; } = "Skipping Windows Terminal Stable, Preview. Not supported in this OS";
        public string TM_Skip_TerminalPreview_NotInstalled { get; set; } = "Skipping Windows Terminal Preview as it isn't installed";
        public string TM_Skip_TerminalPreview_DeflectionNotFound { get; set; } = "Skipping Windows Terminal Preview as deflected JSON doesn't exist";
        public string TM_Skip_TerminalStable_NotInstalled { get; set; } = "Skipping Windows Terminal Stable as it isn't installed";
        public string TM_Skip_TerminalStable_DeflectionNotFound { get; set; } = "Skipping Windows Stable Preview as deflected JSON doesn't exist";
        public string TM_Skip_CMD { get; set; } = "Skipping Command Prompt as it is disabled";
        public string TM_Skip_PS64 { get; set; } = "Skipping PowerShell x64 as it is disabled";
        public string TM_Skip_PS32 { get; set; } = "Skipping PowerShell x86 as it is disabled";
        public string TM_Skip_Metrics { get; set; } = "Skipping Windows Metrics and Fonts as they are disabled";
        public string TM_Skip_AltTab { get; set; } = "Skipping Windows Switcher (Alt+Tab) Appearance as it is disabled";
        public string TM_Skip_Wallpaper { get; set; } = "Skipping Wallpaper as it is disabled";
        public string TM_Skip_AppTheme { get; set; } = "Skipping WinPaletter application theme as it is disabled";
        public string TM_Skip_Sounds { get; set; } = "Skipping Sounds as its toggle is disabled";
    }
}
