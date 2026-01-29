namespace WinPaletter
{
    public partial class Localizer
    {
        public partial class Strings_Cls
        {
            public partial class Updates_Cls
            {
                public string NewUpdate { get; set; } = "A new update is available";
                public string Update { get; set; } = "Update";
                public string CheckForUpdates { get; set; } = "Check for updates";
                public string NoTLS12 { get; set; } = "Updates won't work as TLS 1.2 protocol isn't enabled in {0}. Use GitHub instead.";
                public string Downloaded { get; set; } = "The update has been downloaded successfully. Do you want to restart WinPaletter right now?";
                public string CouldNotDownload { get; set; } = "Could not update WinPaletter. Please visit GitHub repository releases and download latest version.";
            }
        }
    }
}