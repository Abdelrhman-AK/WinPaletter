using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.NativeMethods.User32;

namespace WinPaletter.Theme.Structures
{
    public struct Wallpaper : ICloneable
    {
        public bool Enabled;
        public bool SlideShow_Folder_or_ImagesList;

        public string ImageFile;
        public WallpaperStyles WallpaperStyle;
        public WallpaperTypes WallpaperType;

        public string Wallpaper_Slideshow_ImagesRootPath;
        public string[] Wallpaper_Slideshow_Images;
        public int Wallpaper_Slideshow_Interval;
        public bool Wallpaper_Slideshow_Shuffle;

        public enum WallpaperStyles : int
        {
            Centered = 0,
            Tile = 1,
            Stretched = 2,
            Fit = 6,
            Fill = 10
        }
        public enum WallpaperTypes
        {
            Picture,
            SolidColor,
            SlideShow
        }

        public void Load(Wallpaper _DefWallpaper)
        {
            Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "", _DefWallpaper.Enabled));
            SlideShow_Folder_or_ImagesList = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "SlideShow_Folder_or_ImagesList", _DefWallpaper.SlideShow_Folder_or_ImagesList));
            Wallpaper_Slideshow_ImagesRootPath = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "Wallpaper_Slideshow_ImagesRootPath", _DefWallpaper.Wallpaper_Slideshow_ImagesRootPath).ToString();
            Wallpaper_Slideshow_Images = (string[])GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "Wallpaper_Slideshow_Images", _DefWallpaper.Wallpaper_Slideshow_Images);

            ImageFile = GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", _DefWallpaper.ImageFile).ToString();

            string slideshow_img = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Themes\TranscodedWallpaper";
            string spotlight_img = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\LocalState\Assets";

            // Necessary to remember last wallpaper that is not from slideshow and not a spotlight image
            if (ImageFile.StartsWith(slideshow_img, My.Env._ignore) || ImageFile.StartsWith(spotlight_img, My.Env._ignore) || !File.Exists(ImageFile))
            {
                ImageFile = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "CurrentWallpaperPath", _DefWallpaper.ImageFile).ToString();
            }

            if (GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "TileWallpaper", "0").ToString() == "1")
            {
                WallpaperStyle = WallpaperStyles.Tile;
            }
            else
            {
                WallpaperStyle = (WallpaperStyles)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "WallpaperStyle", _DefWallpaper.WallpaperStyle));
            }

            WallpaperType = (WallpaperTypes)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "BackgroundType", _DefWallpaper.WallpaperType));

            Wallpaper_Slideshow_Interval = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Personalization\Desktop Slideshow", "Interval", _DefWallpaper.Wallpaper_Slideshow_Interval));
            Wallpaper_Slideshow_Shuffle = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Control Panel\Personalization\Desktop Slideshow", "Shuffle", _DefWallpaper.Wallpaper_Slideshow_Shuffle));

        }

        public void Apply(bool SkipSettingWallpaper = false, TreeView TreeView = null)
        {
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "", Enabled);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "SlideShow_Folder_or_ImagesList", SlideShow_Folder_or_ImagesList);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "Wallpaper_Slideshow_ImagesRootPath", Wallpaper_Slideshow_ImagesRootPath, RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "Wallpaper_Slideshow_Images", Wallpaper_Slideshow_Images, RegistryValueKind.MultiString);

            if (Enabled)
            {
                string slideshow_ini = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Themes\slideshow.ini";
                string slideshow_img = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Themes\TranscodedWallpaper";

                if (File.Exists(slideshow_ini))
                {
                    File.SetAttributes(slideshow_ini, FileAttributes.Normal);
                    File.WriteAllText(slideshow_ini, "");
                    File.SetAttributes(slideshow_ini, FileAttributes.Hidden);
                }

                // Setting WallpaperStyle must be before setting wallpaper itself
                if (WallpaperStyle == WallpaperStyles.Tile)
                {
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "TileWallpaper", "1", RegistryValueKind.String);
                }
                else
                {
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "TileWallpaper", "0", RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "WallpaperStyle", (int)WallpaperStyle, RegistryValueKind.String);
                }

                if (!SkipSettingWallpaper)
                {

                    if (WallpaperType == WallpaperTypes.SolidColor)
                    {
                        if (TreeView is not null)
                            Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Desktop.SETDESKWALLPAPER.ToString(), 0, "", SPIF.UpdateINIFile.ToString()), "dll");
                        User32.SystemParametersInfo((int)SPI.Desktop.SETDESKWALLPAPER, 0, "", (int)SPIF.UpdateINIFile);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", "", RegistryValueKind.String);
                    }

                    else if (WallpaperType == WallpaperTypes.Picture)
                    {
                        if (My.Env.WXP | My.Env.WVista | My.Env.W7 && File.Exists(ImageFile) && !new FileInfo(ImageFile).FullName.StartsWith(My.Env.PATH_Windows + @"\Web", My.Env._ignore))
                        {
                            using (var bmp = new Bitmap(Bitmap_Mgr.Load(ImageFile)))
                            {
                                if (bmp.RawFormat != System.Drawing.Imaging.ImageFormat.Bmp)
                                {
                                    if (MsgBox(My.Env.Lang.TM_Wallpaper_NonBMP0, MessageBoxButtons.YesNo, MessageBoxIcon.Question, My.Env.Lang.TM_Wallpaper_NonBMP1) == DialogResult.Yes)
                                    {
                                        bmp.Save(ImageFile, System.Drawing.Imaging.ImageFormat.Bmp);
                                    }
                                }
                            }
                        }

                        if (TreeView is not null)
                            Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Desktop.SETDESKWALLPAPER.ToString(), 0, ImageFile, SPIF.UpdateINIFile.ToString()), "dll");
                        User32.SystemParametersInfo((int)SPI.Desktop.SETDESKWALLPAPER, 0, ImageFile, (int)SPIF.UpdateINIFile);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", ImageFile, RegistryValueKind.String);

                        // Necessary to make both WinPaletter and Windows remember last wallpaper that is not from slideshow and not a spotlight image
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "CurrentWallpaperPath", ImageFile, RegistryValueKind.String);
                    }

                    else if (WallpaperType == WallpaperTypes.SlideShow)
                    {
                        if (TreeView is not null)
                            Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Desktop.SETDESKWALLPAPER.ToString(), 0, slideshow_img, SPIF.UpdateINIFile.ToString()), "dll");
                        User32.SystemParametersInfo((int)SPI.Desktop.SETDESKWALLPAPER, 0, slideshow_img, (int)SPIF.UpdateINIFile);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", slideshow_img, RegistryValueKind.String);

                    }
                }

                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "BackgroundType", WallpaperType);

                if (!My.Env.WXP && !My.Env.WVista)
                {

                    if (!SkipSettingWallpaper)
                    {
                        using (var _ini = new INI(slideshow_ini))
                        {

                            if (TreeView is not null)
                                Manager.AddNode(TreeView, string.Format(My.Env.Lang.Verbose_SettingSlideshow, slideshow_ini), "dll");

                            if (WallpaperType == WallpaperTypes.SlideShow && SlideShow_Folder_or_ImagesList && Directory.Exists(Wallpaper_Slideshow_ImagesRootPath))
                            {
                                _ini.IniWriteValue("Slideshow", "ImagesRootPath", Wallpaper_Slideshow_ImagesRootPath);
                            }

                            _ini.IniWriteValue("Slideshow", "Interval", Wallpaper_Slideshow_Interval.ToString());
                            _ini.IniWriteValue("Slideshow", "Shuffle", Wallpaper_Slideshow_Shuffle.ToString());

                            if (WallpaperType == WallpaperTypes.SlideShow && !SlideShow_Folder_or_ImagesList)
                            {
                                if (Directory.Exists(Wallpaper_Slideshow_Images[0]))
                                {
                                    _ini.IniWriteValue("Slideshow", "ImagesRootPath", new FileInfo(Wallpaper_Slideshow_Images[0]).Directory.FullName);
                                }

                                for (int i = 0, loopTo = Wallpaper_Slideshow_Images.Count() - 1; i <= loopTo; i++)
                                    _ini.IniWriteValue("Slideshow", "Item" + i + "Path", Wallpaper_Slideshow_Images[i]);
                            }

                        }
                    }

                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Personalization\Desktop Slideshow", "Interval", Wallpaper_Slideshow_Interval);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Personalization\Desktop Slideshow", "Shuffle", Wallpaper_Slideshow_Shuffle);
                }
            }
        }

        public static bool operator ==(Wallpaper First, Wallpaper Second)
        {
            return First.Equals(Second);
        }


        public static bool operator !=(Wallpaper First, Wallpaper Second)
        {
            return !First.Equals(Second);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
