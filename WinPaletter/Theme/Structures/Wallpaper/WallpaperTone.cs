using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;
using static WinPaletter.NativeMethods.User32;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing Wallpaper Tone
    /// <br>Wallpaper Tone is a feature by WinPaletter. It modifies images HSL filter to alter wallpaper colors.</br>
    /// </summary>
    public struct WallpaperTone : ICloneable
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled;

        /// <summary>
        /// Image file used
        /// <br><b>- It is better to use a stock Windows wallpaper in '%windir%\Web\Wallpaper', but sure you can use other images.</b></br>
        /// </summary>
        public string Image;

        /// <summary>Hue</summary>
        public int H;

        /// <summary>Saturation</summary>
        public int S;

        /// <summary>Lightness</summary>
        public int L;

        /// <summary>
        /// Loads Wallpaper data from registry
        /// </summary>
        /// <param name="SubKey">
        /// Registry subkey in 'HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone' from which data will be got.
        /// <br><b>This is to load different data, as WinPaletter loads and saves data of multiple WallpaperTone object, each object is made for each version of Windows</b></br>
        /// </param>
        public void Load(string SubKey)
        {
            string wallpaper;

            if (SubKey.ToLower() == "winxp")
            {
                wallpaper = PathsExt.Windows + @"\Web\Wallpaper\Bliss.bmp";
            }
            else
            {
                wallpaper = PathsExt.Windows + @"\Web\Wallpaper\Windows\img0.jpg";
            }

            if (!File.Exists(wallpaper))
                wallpaper = GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", wallpaper).ToString();

            Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "Enabled", false));
            Image = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "Image", wallpaper).ToString();
            H = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "H", 0));
            S = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "S", 100));
            L = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "L", 100));
        }

        /// <summary>
        /// Save the data only to registry without processing and applying target image
        /// </summary>
        /// <param name="WT">WallpaperTone structure</param>
        /// <param name="SubKey">
        /// Registry subkey in 'HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone' into which data will be saved.
        /// <br><b>This is to save different data, as WinPaletter loads and saves data of multiple WallpaperTone object, each object is made for each version of Windows</b></br>
        /// </param>
        /// <param name="TreeView">TreeView used as theme log</param>
        public static void Save_To_Registry(WallpaperTone WT, string SubKey, TreeView TreeView = null)
        {
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "Enabled", WT.Enabled);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "Image", WT.Image, RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "H", WT.H);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "S", WT.S);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "L", WT.L);
        }

        /// <summary>
        /// Processes the image by modifying its HSL filter and applies it.
        /// </summary>
        /// <param name="TreeView">TreeView used as theme log</param>
        /// <exception cref="IOException"></exception>
        public void Apply(TreeView TreeView = null)
        {
            if (!File.Exists(Image))
                throw new IOException("Couldn't Find image");

            string path;
            if (!OS.WXP & !OS.WVista)
            {
                path = Path.Combine(PathsExt.appData, "TintedWallpaper.bmp");
            }
            else
            {
                path = Path.Combine(PathsExt.Windows, @"Web\Wallpaper\TintedWallpaper.bmp");
            }

            if (TreeView is not null)
                Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_SettingHSLImage, path), "pe_patch");

            using (var ImgF = new ImageProcessor.ImageFactory())
            {
                ImgF.Load(Image);
                ImgF.Hue(H, true);
                ImgF.Saturation(S - 100);
                ImgF.Brightness(L - 100);
                ImgF.Image.Save(path, System.Drawing.Imaging.ImageFormat.Bmp);
            }

            SystemParametersInfo(TreeView, (int)SPI.Desktop.SETDESKWALLPAPER, 0, path, SPIF.UpdateINIFile);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", path, RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "BackgroundType", (int)Wallpaper.WallpaperTypes.Picture);

            Forms.MainFrm.Update_Wallpaper_Preview();
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
