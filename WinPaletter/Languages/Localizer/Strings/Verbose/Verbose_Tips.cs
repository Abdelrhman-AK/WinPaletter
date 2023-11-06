namespace WinPaletter
{
    public partial class Localizer
    {
        public string TM_MetricsHighDPIAlert { get; set; } = "Please Logoff and Logon after setting Metrics and Fonts with a high DPI";
        public string TM_Restricted_Cursors { get; set; } = "Modifying Windows Cursors is restricted from settings";
        public string TM_RestoreCursorsTip { get; set; } = "If you want to restore default cursors, go to Control Panel > Mouse > Pointers";
        public string TM_UpdateDLL_AsAdmin_Error0 { get; set; } = "You must be running WinPaletter as Administrator to update resources of '{0}'";
        public string TM_UpdateDLL_AsAdmin_Error1 { get; set; } = "This process is required for changing Windows startup sound";
        public string TM_Wallpaper_NonBMP0 { get; set; } = "Due to odd reason, Windows XP, Vista & 7 can't set an image that is not a bitmap format directly as a wallpaper.";
        public string TM_Wallpaper_NonBMP1 { get; set; } = "Do you want to convert the current image to have internal bitmap format? (It will still have the same file extension)";
        public string NoDefResExplorer { get; set; } = "Restarting Explorer is disabled. If theme is not applied correctly, restart it";
    }
}
