using Microsoft.Win32;
using Serilog.Events;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using static WinPaletter.NativeMethods.User32;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing Wallpaper Tone
    /// <br>Wallpaper Tone is a feature by WinPaletter (not a feature in Windows). It modifies images HSL filter to alter wallpaper colors.</br>
    /// </summary>
    public class WallpaperTone : ManagerBase<WallpaperTone>
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled { get; set; } = false;

        /// <summary>
        /// Image File used
        /// <br><b>- It is better to use a stock Windows wallpaper in '%windir%\Web\Wallpaper', but sure you can use other images.</b></br>
        /// </summary>
        public string Image { get; set; } = $@"{SysPaths.Windows}\Web\Wallpaper\Windows\img0.jpg";

        /// <summary>Hue</summary>
        public int H { get; set; } = 0;

        /// <summary>Saturation</summary>
        public int S { get; set; } = 50;

        /// <summary>Lightness</summary>
        public int L { get; set; } = 50;

        private string osSignature;

        /// <summary>
        /// Creates a new WallpaperTone structure with default values
        /// </summary>
        public WallpaperTone(string OSSignature) { osSignature = OSSignature; }

        /// <summary>
        /// Loads WallpaperTone data from registry
        /// </summary>
        /// <param name="SubKey">
        /// Registry subkey in 'HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone' from which data will be got.
        /// <br><b>This is to load different data, as WinPaletter loads and saves data of multiple WallpaperTone object, each object is made for each version of Windows</b></br>
        /// </param>
        public void Load()
        {
            Program.Log?.Write(LogEventLevel.Information, $"Loading WinPaletter Wallpaper Tone from registry, targeting {osSignature}");

            string wallpaper = osSignature.ToLower() != "winxp" ? $@"{SysPaths.Windows}\Web\Wallpaper\Windows\img0.jpg" : $@"{SysPaths.Windows}\Web\Wallpaper\Bliss.bmp";

            if (!File.Exists(wallpaper)) wallpaper = ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", wallpaper);

            Enabled = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\{osSignature}", "Enabled", false);
            Image = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\{osSignature}", "Image", wallpaper);
            H = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\{osSignature}", "H", 0);
            S = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\{osSignature}", "S", 50);
            L = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\{osSignature}", "L", 50);
        }

        /// <summary>
        /// Save data only to registry without processing and applying target image
        /// </summary>
        /// <param name="WT">WallpaperTone structure</param>
        /// <param name="SubKey">
        /// Registry subkey in 'HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone' into which data will be saved.
        /// <br><b>This is to save different data, as WinPaletter loads and saves data of multiple WallpaperTone object, each object is made for each version of Windows</b></br>
        /// </param>
        /// <param name="treeView">treeView used as theme log</param>
        public void Save(TreeView treeView = null)
        {
            SaveToggleState(treeView);

            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\{osSignature}", "Image", Image, RegistryValueKind.String);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\{osSignature}", "H", H);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\{osSignature}", "S", S);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\{osSignature}", "L", L);
        }

        /// <summary>
        /// Save WallpaperTone toggle state to registry
        /// </summary>
        /// <param name="WT"></param>
        /// <param name="SubKey"></param>
        /// <param name="treeView"></param>
        public void SaveToggleState(TreeView treeView = null)
        {
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\{osSignature}", "Enabled", Enabled);
        }

        /// <summary>
        /// Processes the image by modifying its HSL filter and applies it.
        /// </summary>
        /// <param name="treeView">treeView used as theme log</param>
        /// <exception cref="IOException"></exception>
        public void Process(TreeView treeView = null)
        {
            if (!File.Exists(Image))
            {
                Program.Log?.Write(LogEventLevel.Error, $"Couldn't find base image file: `{Image}`.");
                return;
            }

            string path;
            if (!OS.WXP & !OS.WVista)
            {
                path = Path.Combine(SysPaths.appData, "TintedWallpaper.bmp");
            }
            else
            {
                path = Path.Combine(SysPaths.Windows, @"Web\Wallpaper\TintedWallpaper.bmp");
            }

            Program.Log?.Write(LogEventLevel.Information, $"Saving WinPaletter Wallpaper Tone into registry and by rendering a custom image.");

            if (treeView is not null)
                ThemeLog.AddNode(treeView, string.Format(Program.Localization.Strings.ThemeManager.Advanced.SettingHSLImage, path), "pe_patch");

            Program.Log?.Write(LogEventLevel.Information, $"Rendering a custom image with HSL values: H={H}, S={S}, L={L}.");

            using (Bitmap wall_source = BitmapMgr.Load(Image))
            using (Bitmap wall = wall_source.AdjustHSL(H, S / 100f, L / 100f))
            {
                wall?.Save(path, ImageFormat.Bmp);
            }

            // Process the image
            SystemParametersInfo(treeView, SPI.SPI_SETDESKWALLPAPER, 0, path, SPIF.SPIF_UPDATEINIFILE);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", path, RegistryValueKind.String);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "BackgroundType", (int)Wallpaper.WallpaperTypes.Picture);
        }
    }
}
