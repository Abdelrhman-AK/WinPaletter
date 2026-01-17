namespace WinPaletter
{
    public partial class Localizer
    {
        public partial class Strings_Cls
        {
            public partial class Aspects_Cls
            {
                public partial class Terminals_Cls
                {
                    public string Name_AlreadySet { get; set; } = "You can't set this name as it is already assigned to another profile.";
                    public string TerminalStable_notFound { get; set; } = "Windows Terminal Stable isn't installed, or 'settings.json' isn't accessible.";
                    public string TerminalPreview_notFound { get; set; } = "Windows Terminal Preview isn't installed, or 'settings.json' isn't accessible.";
                    public string PathSupposed { get; set; } = "It is supposed to be located in: ";
                    public string Bypass { get; set; } = "You can bypass this restriction in Settings > Terminals (In case you want to design a theme for all versions of Windows and save it as a file for sharing, not applying it).";
                    public string CantRun { get; set; } = "You can't run Windows Terminal on the current OS. It is available only on Windows 10 and 11.";
                    public string ProfileNotCloneable { get; set; } = "The default profile isn't cloneable. Please select a different profile.";
                    public string ThemeNotCloneable { get; set; } = @"Default themes (Dark\Light\System) are not cloneable. Please select a different theme or create a new theme if you want to clone.";
                    public string TypeSchemeName { get; set; } = "Type the theme name here:";
                    public string EmptyError { get; set; } = "The terminal name can't be empty. Please enter a valid one.";
                    public string NotExist { get; set; } = "The terminal does not exist. Please enter a valid one.";
                    public string Reserved { get; set; } = "This terminal is reserved for the system. Please try again with another one.";
                    public string Exists { get; set; } = "This terminal already exists. Please try again with another one.";
                    public string ConsoleSample { get; set; } = "Console sample";
                    public string ThisIsASelection { get; set; } = "This is a selection";
                    public string Another { get; set; } = "Another Terminal";
                }
            }
        }
    }
}