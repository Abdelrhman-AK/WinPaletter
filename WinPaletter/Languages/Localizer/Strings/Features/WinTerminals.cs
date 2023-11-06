namespace WinPaletter
{
    public partial class Localizer
    {
        public string Terminal_alreadyset { get; set; } = "You can't set this name as it is already set to another profile.";
        public string TerminalStable_notFound { get; set; } = "Windows Terminal Stable isn't installed or 'settings.json' isn't accessible.";
        public string TerminalPreview_notFound { get; set; } = "Windows Terminal Preview isn't installed or 'settings.json' isn't accessible.";
        public string Terminal_supposed { get; set; } = "It is supposed to be located in: ";
        public string Terminal_Bypass { get; set; } = "You can bypass this restriction in Settings > Terminals (In case you want to design a theme for all versions of Windows and save it as a file for sharing, not applying it).";
        public string Terminal_CantRun { get; set; } = "You can't run Windows Terminal in current OS. It is available only in Windows 10 and 11.";
        public string Terminal_ErrorFile { get; set; } = "Error occurred while reading settings file: ";
        public string Terminal_ProfileNotCloneable { get; set; } = "Default Profile isn't cloneable, select a different profile.";
        public string Terminal_ThemeNotCloneable { get; set; } = @"Default themes (Dark\Light\System) are not cloneable, select a different theme or create a new theme if you want to clone.";
        public string Terminal_Clone { get; set; } = "Clone";
        public string Terminal_NewProfile { get; set; } = "New profile";
        public string Terminal_NewScheme { get; set; } = "New scheme";
        public string Terminal_NewTheme { get; set; } = "New theme";
        public string Terminal_TypeSchemeName { get; set; } = "Type theme name here:";
        public string Terminal_External_Empty { get; set; } = "Terminal can't be empty. Enter a valid one.";
        public string Terminal_External_NotExist { get; set; } = "Terminal doesn't exist. Enter a valid one.";
        public string Terminal_External_Reversed { get; set; } = "This terminal is reserved for system. Try again with another one.";
        public string Terminal_External_Exists { get; set; } = "This terminal already exists. Try again with another one.";
        public string Terminal_ConsoleSample { get; set; } = "Console sample";
        public string Terminal_ThisIsASelection { get; set; } = "This is a selection";
        public string Terminal_Another { get; set; } = "Another Terminal";
    }
}
