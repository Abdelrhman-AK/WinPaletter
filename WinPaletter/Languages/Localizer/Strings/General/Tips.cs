namespace WinPaletter
{
    public partial class Localizer
    {
        public partial class Strings_Cls
        {
            public partial class AppTips_Cls
            {
                public string OS_PreviewingAs { get; set; } = "Review the theme as if you are using {0}";
                public string TitlebarColorNotice { get; set; } = "Windows volume slider, UAC dialogs and LogonUI background follow active titlebar color";
                public string PaletteExtraction { get; set; } = "Extracting the palette from the image depends on your device's performance, maximum palette colors number, image quality, and its resolution...";
                public string WallpaperTone_Notice { get; set; } = "You are currently editing preferences for {0}. To change preferences for another OS, switch the OS in the main form.";
                public string VisualStyles_WrongPlatform { get; set; } = "This Visual Styles file is for {0} and doesn't work with the selected {1} in the main form. Applying it could cause a system crash, so WinPaletter won't apply it to prevent this.";
                public string ClickToEdit { get; set; } = "Click to edit current value";
                public string ClickToReset { get; set; } = "Reset to default Windows value";
                public string PressEnterToUseValue { get; set; } = "Press 'Enter' to use this value.";
                public string PressEscToDismissEditing { get; set; } = "Press 'Esc' to dismiss editing value.";
                public string PaletteSourceGeneration { get; set; } = "Couldn't find a palette.\r\nPlease select a source above to generate a palette from it";
            }
        }
    }
}