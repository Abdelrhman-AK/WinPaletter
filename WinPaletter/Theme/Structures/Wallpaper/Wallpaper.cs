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
    /// <summary>
    /// Structure responsible for managing Windows wallpaper
    /// </summary>
    public struct Wallpaper : ICloneable
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled;

        /// <summary>
        /// - If true, slideshow source will be a folder
        /// <br>- If false, slideshow source will be a list of images</br>
        /// </summary>
        public bool SlideShow_Folder_or_ImagesList;

        /// <summary>Wallpaper file path</summary>
        public string ImageFile;

        /// <summary>
        /// It is how the image will be previewed in desktop
        /// <code>
        /// Centered
        /// Tile
        /// Stretched
        /// Fit
        /// Fill
        /// </code>
        /// </summary>
        public WallpaperStyles WallpaperStyle;

        /// <summary>
        /// Type of wallpaper
        /// <code>
        /// Picture
        /// SolidColor
        /// SlideShow
        /// </code>
        /// </summary>
        public WallpaperTypes WallpaperType;

        /// <summary>Folder that has images for wallpaper slide show, (if SlideShow_Folder_or_ImagesList = true;)</summary>
        public string Wallpaper_Slideshow_ImagesRootPath;

        /// <summary>
        /// String array that has pathes of images files, (if SlideShow_Folder_or_ImagesList = false;)
        /// <br><b><i>(!) Important note: array items (files) must be from the same folder, or slideshow won't load them.</i></b></br>
        /// </summary>
        public string[] Wallpaper_Slideshow_Images;

        /// <summary>Interval of wallpaper changing in slideshow</summary>
        public int Wallpaper_Slideshow_Interval;

        /// <summary>Shuffle slideshow images (don't preview them in their order)</summary>
        public bool Wallpaper_Slideshow_Shuffle;

        /// <summary>
        /// Enumeration for the ways the wallpaper can be rendered on desktop
        /// </summary>
        public enum WallpaperStyles : int
        {
            ///
            Centered = 0,
            ///
            Tile = 1,
            ///
            Stretched = 2,
            ///
            Fit = 6,
            ///
            Fill = 10
        }

        /// <summary>
        /// Enumeration for wallpaper types (or sources)
        /// </summary>
        public enum WallpaperTypes
        {
            ///
            Picture,
            ///
            SolidColor,
            ///
            SlideShow
        }

        /// <summary>
        /// Loads Wallpaper data from registry
        /// </summary>
        /// <param name="default">Default Wallpaper data structure</param>
        public void Load(Wallpaper @default)
        {
            Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "", @default.Enabled));
            SlideShow_Folder_or_ImagesList = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "SlideShow_Folder_or_ImagesList", @default.SlideShow_Folder_or_ImagesList));
            Wallpaper_Slideshow_ImagesRootPath = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "Wallpaper_Slideshow_ImagesRootPath", @default.Wallpaper_Slideshow_ImagesRootPath).ToString();
            Wallpaper_Slideshow_Images = (string[])GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "Wallpaper_Slideshow_Images", @default.Wallpaper_Slideshow_Images);

            ImageFile = GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", @default.ImageFile).ToString();

            string slideshow_img = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Themes\TranscodedWallpaper";
            string spotlight_img = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\LocalState\Assets";

            // Necessary to remember last wallpaper that is not from slideshow and not a spotlight image
            if (ImageFile.StartsWith(slideshow_img, StringComparison.OrdinalIgnoreCase) || ImageFile.StartsWith(spotlight_img, StringComparison.OrdinalIgnoreCase) || !File.Exists(ImageFile))
            {
                ImageFile = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "CurrentWallpaperPath", @default.ImageFile).ToString();
            }

            if (GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "TileWallpaper", "0").ToString() == "1")
            {
                WallpaperStyle = WallpaperStyles.Tile;
            }
            else
            {
                WallpaperStyle = (WallpaperStyles)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "WallpaperStyle", @default.WallpaperStyle));
            }

            WallpaperType = (WallpaperTypes)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "BackgroundType", @default.WallpaperType));

            Wallpaper_Slideshow_Interval = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Personalization\Desktop Slideshow", "Interval", @default.Wallpaper_Slideshow_Interval));
            Wallpaper_Slideshow_Shuffle = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Control Panel\Personalization\Desktop Slideshow", "Shuffle", @default.Wallpaper_Slideshow_Shuffle));

        }

        /// <summary>
        /// Saves Wallpaper data into registry
        /// </summary>
        /// <param name="SkipSettingWallpaper">
        /// This will write some registry items and won't apply the wallpaper
        /// <br><b>- Set it to 'true' if WallpaperTone is used</b></br>
        /// </param>
        /// <param name="TreeView">TreeView used as theme log</param>
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
                            Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Desktop.SETDESKWALLPAPER.ToString(), 0, "", SPIF.UpdateINIFile.ToString()), "dll");
                        User32.SystemParametersInfo((int)SPI.Desktop.SETDESKWALLPAPER, 0, "", (int)SPIF.UpdateINIFile);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", "", RegistryValueKind.String);
                    }

                    else if (WallpaperType == WallpaperTypes.Picture)
                    {
                        if (OS.WXP | OS.WVista | OS.W7 && File.Exists(ImageFile) && !new FileInfo(ImageFile).FullName.StartsWith(PathsExt.Windows + @"\Web", StringComparison.OrdinalIgnoreCase))
                        {
                            using (var bmp = new Bitmap(Bitmap_Mgr.Load(ImageFile)))
                            {
                                if (bmp.RawFormat != System.Drawing.Imaging.ImageFormat.Bmp)
                                {
                                    if (MsgBox(Program.Lang.TM_Wallpaper_NonBMP0, MessageBoxButtons.YesNo, MessageBoxIcon.Question, Program.Lang.TM_Wallpaper_NonBMP1) == DialogResult.Yes)
                                    {
                                        bmp.Save(ImageFile, System.Drawing.Imaging.ImageFormat.Bmp);
                                    }
                                }
                            }
                        }

                        if (TreeView is not null)
                            Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Desktop.SETDESKWALLPAPER.ToString(), 0, ImageFile, SPIF.UpdateINIFile.ToString()), "dll");
                        User32.SystemParametersInfo((int)SPI.Desktop.SETDESKWALLPAPER, 0, ImageFile, (int)SPIF.UpdateINIFile);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", ImageFile, RegistryValueKind.String);

                        // Necessary to make both WinPaletter and Windows remember last wallpaper that is not from slideshow and not a spotlight image
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "CurrentWallpaperPath", ImageFile, RegistryValueKind.String);
                    }

                    else if (WallpaperType == WallpaperTypes.SlideShow)
                    {
                        if (TreeView is not null)
                            Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Desktop.SETDESKWALLPAPER.ToString(), 0, slideshow_img, SPIF.UpdateINIFile.ToString()), "dll");
                        User32.SystemParametersInfo((int)SPI.Desktop.SETDESKWALLPAPER, 0, slideshow_img, (int)SPIF.UpdateINIFile);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", slideshow_img, RegistryValueKind.String);

                    }
                }

                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "BackgroundType", WallpaperType);

                if (!OS.WXP && !OS.WVista)
                {

                    if (!SkipSettingWallpaper)
                    {
                        using (var _ini = new INI(slideshow_ini))
                        {

                            if (TreeView is not null)
                                Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_SettingSlideshow, slideshow_ini), "dll");

                            if (WallpaperType == WallpaperTypes.SlideShow && SlideShow_Folder_or_ImagesList && Directory.Exists(Wallpaper_Slideshow_ImagesRootPath))
                            {
                                _ini.Write("Slideshow", "ImagesRootPath", Wallpaper_Slideshow_ImagesRootPath);
                            }

                            _ini.Write("Slideshow", "Interval", Wallpaper_Slideshow_Interval.ToString());
                            _ini.Write("Slideshow", "Shuffle", Wallpaper_Slideshow_Shuffle.ToString());

                            if (WallpaperType == WallpaperTypes.SlideShow && !SlideShow_Folder_or_ImagesList)
                            {
                                if (Directory.Exists(Wallpaper_Slideshow_Images[0]))
                                {
                                    _ini.Write("Slideshow", "ImagesRootPath", new FileInfo(Wallpaper_Slideshow_Images[0]).Directory.FullName);
                                }

                                for (int i = 0, loopTo = Wallpaper_Slideshow_Images.Count() - 1; i <= loopTo; i++)
                                    _ini.Write("Slideshow", "Item" + i + "Path", Wallpaper_Slideshow_Images[i]);
                            }

                        }
                    }

                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Personalization\Desktop Slideshow", "Interval", Wallpaper_Slideshow_Interval);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Personalization\Desktop Slideshow", "Shuffle", Wallpaper_Slideshow_Shuffle);
                }
            }
        }

        /// <summary>Operator to check if two Wallpaper structures are equal</summary>
        public static bool operator ==(Wallpaper First, Wallpaper Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two Wallpaper structures are not equal</summary>
        public static bool operator !=(Wallpaper First, Wallpaper Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones Wallpaper structure</summary>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Checks if two Wallpaper structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>Get hash code of Wallpaper structure</summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
