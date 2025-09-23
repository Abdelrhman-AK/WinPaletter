using Microsoft.Win32;
using Serilog.Events;
using System;
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
    public class WallpaperTone : ICloneable
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled = false;

        /// <summary>
        /// Image File used
        /// <br><b>- It is better to use a stock Windows wallpaper in '%windir%\Web\Wallpaper', but sure you can use other images.</b></br>
        /// </summary>
        public string Image = $@"{SysPaths.Windows}\Web\Wallpaper\Windows\img0.jpg";

        /// <summary>Hue</summary>
        public int H = 0;

        /// <summary>Saturation</summary>
        public int S = 50;

        /// <summary>Lightness</summary>
        public int L = 50;

        /// <summary>
        /// Creates a new WallpaperTone structure with default values
        /// </summary>
        public WallpaperTone() { }

        /// <summary>
        /// Loads WallpaperTone data from registry
        /// </summary>
        /// <param name="SubKey">
        /// Registry subkey in 'HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone' from which data will be got.
        /// <br><b>This is to load different data, as WinPaletter loads and saves data of multiple WallpaperTone object, each object is made for each version of Windows</b></br>
        /// </param>
        public void Load(string SubKey)
        {
            if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, $"Loading WinPaletter Wallpaper Tone from registry, targeting {SubKey}");

            string wallpaper = SubKey.ToLower() != "winxp" ? $@"{SysPaths.Windows}\Web\Wallpaper\Windows\img0.jpg" : $@"{SysPaths.Windows}\Web\Wallpaper\Bliss.bmp";

            if (!File.Exists(wallpaper)) wallpaper = ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", wallpaper).ToString();

            Enabled = Convert.ToBoolean(ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\{SubKey}", "Enabled", false));
            Image = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\{SubKey}", "Image", wallpaper).ToString();
            H = Convert.ToInt32(ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\{SubKey}", "H", 0));
            S = Convert.ToInt32(ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\{SubKey}", "S", 50));
            L = Convert.ToInt32(ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\{SubKey}", "L", 50));
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
        public static void Save_To_Registry(WallpaperTone WT, string SubKey, TreeView treeView = null)
        {
            SaveToggleState(WT, SubKey, treeView);

            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\{SubKey}", "Image", WT.Image, RegistryValueKind.String);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\{SubKey}", "H", WT.H);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\{SubKey}", "S", WT.S);
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\{SubKey}", "L", WT.L);
        }

        /// <summary>
        /// Save WallpaperTone toggle state to registry
        /// </summary>
        /// <param name="WT"></param>
        /// <param name="SubKey"></param>
        /// <param name="treeView"></param>
        public static void SaveToggleState(WallpaperTone WT, string SubKey, TreeView treeView = null)
        {
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\{SubKey}", "Enabled", WT.Enabled);
        }

        /// <summary>
        /// Processes the image by modifying its HSL filter and applies it.
        /// </summary>
        /// <param name="treeView">treeView used as theme log</param>
        /// <exception cref="IOException"></exception>
        public void Apply(TreeView treeView = null)
        {
            if (!File.Exists(Image))
            {
                if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Error, $"Couldn't find base image file: `{Image}`.");
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

            if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, $"Saving WinPaletter Wallpaper Tone into registry and by rendering a custom image.");

            if (treeView is not null)
                ThemeLog.AddNode(treeView, string.Format(Program.Lang.Strings.ThemeManager.Advanced.SettingHSLImage, path), "pe_patch");

            if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, $"Rendering a custom image with HSL values: H={H}, S={S}, L={L}.");

            using (Bitmap wall_source = BitmapMgr.Load(Image))
            using (Bitmap wall = wall_source.AdjustHSL(H, S / 100f, L / 100f))
            {
                wall?.Save(path, ImageFormat.Bmp);
            }

            // Apply the processed image
            SystemParametersInfo(treeView, SPI.SPI_SETDESKWALLPAPER, 0, path, SPIF.SPIF_UPDATEINIFILE);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", path, RegistryValueKind.String);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "BackgroundType", (int)Wallpaper.WallpaperTypes.Picture);
        }

        /// <summary>Operator to check if two WallpaperTone structures are equal</summary>
        public static bool operator ==(WallpaperTone First, WallpaperTone Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two WallpaperTone structures are not equal</summary>
        public static bool operator !=(WallpaperTone First, WallpaperTone Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones WallpaperTone structure</summary>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Checks if two WallpaperTone structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>Get hash code of WallpaperTone structure</summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
