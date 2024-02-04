namespace WinPaletter
{
    public partial class Localizer
    {
        public string TM_RestoreCursorsTip { get; set; } = "If you want to restore default cursors, go to Control Panel > Mouse > Pointers";
        public string TM_Wallpaper_NonBMP0 { get; set; } = "Due to odd reason, Windows XP, Vista & 7 can't set an image that is not a bitmap format directly as a wallpaper.";
        public string TM_Wallpaper_NonBMP1 { get; set; } = "Do you want to convert the current image to the internal bitmap format? (It will still have the same file extension.)";
        public string NoDefResExplorer { get; set; } = "Restarting Explorer is disabled. If the theme is not applied correctly, restart it";
    }
}
