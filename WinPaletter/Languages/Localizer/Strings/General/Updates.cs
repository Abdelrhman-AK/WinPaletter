namespace WinPaletter
{
    public partial class Localizer
    {
        public string Checking { get; set; } = "Checking ...";
        public string DoAction_Update { get; set; } = "Do action";
        public string NoUpdateAvailable { get; set; } = "No update is available";
        public string CheckForUpdates { get; set; } = "Check for update";
        public string NetworkError { get; set; } = "Network error. Check you internet connection.";
        public string UpdatesOSNoTLS12 { get; set; } = "Updates won't work as TLS 1.2 protocol isn't enabled in {0}. Use GitHub instead";
        public string ServerError { get; set; } = "Error: Network issues or GitHub repository is private or deleted. Visit GitHub page for details.";
        public string Msgbox_Downloaded { get; set; } = "Downloaded successfully";
    }
}
