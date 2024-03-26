namespace WinPaletter
{
    public partial class Localizer
    {
        public string Extracting { get; set; } = "Extracting the palette from the image depends on your device's performance, maximum palette colors number, image quality, and its resolution...";
        public string WallpaperTone_Notice { get; set; } = "You are currently editing preferences for {0}. To change preferences for another OS, switch the preview in the main form.";
        public string VisualStyles_WrongPlatform { get; set; } = "This Visual Styles file is for {0} and doesn't work with the selected {1} in the main form. Applying it could cause a system crash, so WinPaletter won't apply it to prevent this.";
    }
}
