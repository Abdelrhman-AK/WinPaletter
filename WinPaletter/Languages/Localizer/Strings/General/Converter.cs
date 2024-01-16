namespace WinPaletter
{
    public partial class Localizer
    {
        public string Convert_JSON_To_Old { get; set; } = "Theme file is JSON-internally-formatted. When you export this theme, it will be with old formatting system (valid for WinPaletter 1.0.7.6 and less).";
        public string Convert_Old_To_JSON { get; set; } = "Theme file is old-formatted. When you export this theme, it will be JSON-internally-formatted (valid for WinPaletter 1.0.7.7 and higher). It supports contents compression that is useful for uploading more amount of themes to WinPaletter Store with less downloading duration, and used resources pack export that is useful for downloading missing used resources (wallpapers, images and sounds) from WinPaletter Store (or by external sharing) and applying them automatically with the theme.";
        public string Convert_Error_Phrasing { get; set; } = "Error occurred while phrasing theme file";
        public string Convert_Done { get; set; } = "Theme file is converted and exported successfully";
        public string Convert_Detect_Old_OnLoading0 { get; set; } = "WinPaletter detected that you are using an old theme format";
        public string Convert_Detect_Old_OnLoadingTip { get; set; } = "Please use WinPaletter with earlier version to convert it then reuse the theme in current WinPaletter version";
    }
}
