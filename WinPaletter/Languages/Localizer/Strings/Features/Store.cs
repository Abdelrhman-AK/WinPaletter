namespace WinPaletter
{
    public partial class Localizer
    {
        public string Store_RemoveTip { get; set; } = "You can't remove an essential themes database repository. Try again with another custom repository.";
        public string Store_NoNetwork { get; set; } = "No internet connection";
        public string Store_TryOffline { get; set; } = "Press 'Yes' if you want to continue in offline Store mode. You can select its folders from Settings > Store.";
        public string Store_Ping { get; set; } = "Testing access to '{0}'";
        public string Store_PingFailed { get; set; } = "Couldn't get response from '{0}'. Skipping this themes database";
        public string Store_Accessing { get; set; } = "Accessing themes database from '{0}'";
        public string Store_UpdateTheme { get; set; } = "Updating theme '{0}' from '{1}'";
        public string Store_DownloadTheme { get; set; } = "Downloading theme '{0}' from '{1}'";
        public string Store_DownloadingPackForTheme { get; set; } = "Downloading resources pack for theme '{0}' version {1}";
        public string Store_LoadingTheme { get; set; } = "Loading theme '{0}'";
        public string Store_LowAppVersionAlert { get; set; } = "This theme is designed by WinPaletter {0} which is higher than {1}. please update WinPaletter to enjoy all new themes aspects and avoid errors.";
        public string Store_CleaningFromMemory { get; set; } = "Cleaning store items from memory";
        public string Store_NoIncludedData { get; set; } = "There is no included data";
        public string Store_WontWork_Protocol { get; set; } = "Unfortunately, WinPaletter Store won't work as TLS 1.2 protocol isn't enabled in {0}. Do you want to continue?";
        public string Store_ThemeDesignedFor0 { get; set; } = "This theme can be applied to all supported versions of Windows, but it was designed for:";
        public string Store_ThemeDesignedFor1 { get; set; } = "This theme can be applied to all supported versions of Windows:";
        public string Store_LogoffRecommended { get; set; } = "It is recommended to logoff your Windows and logon to apply all effects of the theme";
        public string Store_Calculating { get; set; } = "Calculating ...";
        public string Store_AuthorURLRedirect { get; set; } = "This will redirect you to author's social media URL. Do you want to continue?";
    }
}
