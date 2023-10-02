using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.NativeMethods.User32;

namespace WinPaletter.Theme.Structures
{
    public struct WallpaperTone : ICloneable
    {
        public bool Enabled;
        public string Image;
        public int H, S, L;

        public void Load(string SubKey)
        {
            string wallpaper;

            if (SubKey.ToLower() == "winxp")
            {
                wallpaper = My.Env.PATH_Windows + @"\Web\Wallpaper\Bliss.bmp";
            }
            else
            {
                wallpaper = My.Env.PATH_Windows + @"\Web\Wallpaper\Windows\img0.jpg";
            }

            if (!File.Exists(wallpaper))
                wallpaper = GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", wallpaper).ToString();

            Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "Enabled", false));
            Image = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "Image", wallpaper).ToString();
            H = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "H", 0));
            S = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "S", 100));
            L = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "L", 100));
        }

        public static void Save_To_Registry(WallpaperTone WT, string SubKey, TreeView TreeView = null)
        {
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "Enabled", WT.Enabled);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "Image", WT.Image, RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "H", WT.H);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "S", WT.S);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "L", WT.L);
        }

        public void Apply(TreeView TreeView = null)
        {
            if (!File.Exists(Image))
                throw new IOException("Couldn't Find image");

            string path;
            if (!My.Env.WXP & !My.Env.WVista)
            {
                path = Path.Combine(My.Env.PATH_appData, "TintedWallpaper.bmp");
            }
            else
            {
                path = Path.Combine(My.Env.PATH_Windows, @"Web\Wallpaper\TintedWallpaper.bmp");
            }

            if (TreeView is not null)
                Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_SettingHSLImage, path), "pe_patch");

            using (var ImgF = new ImageProcessor.ImageFactory())
            {
                ImgF.Load(Image);
                ImgF.Hue(H, true);
                ImgF.Saturation(S - 100);
                ImgF.Brightness(L - 100);
                ImgF.Image.Save(path, System.Drawing.Imaging.ImageFormat.Bmp);
            }

            if (TreeView is not null)
                Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Desktop.SETDESKWALLPAPER.ToString(), 0, path, SPIF.UpdateINIFile.ToString()), "dll");
            User32.SystemParametersInfo((int)SPI.Desktop.SETDESKWALLPAPER, 0, path, (int)SPIF.UpdateINIFile);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", path, RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "BackgroundType", (int)Wallpaper.WallpaperTypes.Picture);

            My.MyProject.Forms.MainFrm.Update_Wallpaper_Preview();
        }

        public static bool operator ==(WallpaperTone First, WallpaperTone Second)
        {
            return First.Equals(Second);
        }

        public static bool operator !=(WallpaperTone First, WallpaperTone Second)
        {
            return !First.Equals(Second);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
