namespace WinPaletter
{
    public partial class Localizer
    {
        public partial class Strings_Cls
        {
            public partial class Aspects_Cls
            {
                public partial class Consoles_Cls
                {
                    public string Foregrounds { get; set; } = "Enter foreground color (hex 0-F or decimal 0-15)";
                    public string Backgrounds { get; set; } = "Enter background color (hex 0-F or decimal 0-15)";
                    public string CurrentForeground { get; set; } = "Current foreground";
                    public string CurrentBackground { get; set; } = "Current background";
                    public string ColorName { get; set; } = "Color name";
                    public string Foregrounds_PS { get; set; } = "Enter new foreground color (or Enter to exit)";
                    public string Backgrounds_PS { get; set; } = "Enter new background color (or Enter to exit)";
                    public string InvalidColors { get; set; } = "Invalid color(s). Please try again.";
                    public string OptTitleRefresh { get; set; } = "Optional: refreshes window title";
                    public string Popup_Note { get; set; } = "NOTE: Popup colors can't be shown live here. You must apply the colors first by WinPaletter before testing them. Press F2 to test them.";
                    public string CMDSimulator_Alert0 { get; set; } = "This is just a preview of your custom terminal.";
                    public string CMDSimulator_Alert1 { get; set; } = "*Note: Raster Font will look different from the preview.";
                    public string CMDSimulator_ThisIsAPopUp { get; set; } = "This is a pop-up.";
                    public string CMD_Enable { get; set; } = "You should enable terminal editing from the toggle above.";
                    public string CMD_NotAllWeights { get; set; } = "Not all weights are available according to your OS and the font itself. Normal and Bold ones are the basic ones.";
                    public string PowerShellx86_notFound { get; set; } = "Microsoft PowerShell x86 is not installed.";
                    public string PowerShellx64_notFound { get; set; } = "Microsoft PowerShell x64 is not installed.";
                    public string ExtTer_NotFound { get; set; } = "Terminal is not found. Select a valid one from the list.";
                    public string ExtTer_Set { get; set; } = "Terminal preferences are set successfully!";
                    public string ExtTer_NewSuccess { get; set; } = "This key has been entered into the registry successfully.";
                    public string ExtTer_NewError { get; set; } = "Couldn't write this entry to the registry. Please check if this key already exists or check permissions.";
                }
            }
        }
    }
}