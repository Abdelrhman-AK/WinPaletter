namespace WinPaletter
{
    public partial class Localizer
    {
        public partial class Strings_Cls
        {
            public partial class ThemeManager_Cls
            {
                /// <summary>
                /// A class that contains all the skip strings used in the ThemeManager.
                /// </summary>
                public partial class Skip_Cls
                {
                    public string Main { get; set; } = "Skipping the application of {0} as its toggle is disabled";
                    public string Terminals { get; set; } = "Skipping the application of Windows Terminal Stable and Preview as their toggles are disabled";
                    public string Terminals_NotSupported { get; set; } = "Skipping the application of both Windows Terminal Stable and Preview, as they are not supported on this OS";
                    public string Terminal_JSONNotFound { get; set; } = "Skipping the application of {0} as the JSON settings file is missing (not installed or redirected to a non-existent path)";
                }
            }
        }
    }
}