using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Security.Principal;
using System.Windows.Forms;

namespace WinPaletter
{
    internal partial class Program
    {
        private static ManagementEventWatcher WallMon_Watcher1, WallMon_Watcher2, WallMon_Watcher3, WallMon_Watcher4;

        private static readonly MethodInvoker UpdateDarkModeInvoker = new(() =>
        {
            FetchDarkMode();
            if (Settings.Appearance.AutoDarkMode)
                ApplyStyle();
        });
        private static MethodInvoker UpdateWallpaperInvoker()
        {
            Bitmap wall = FetchSuitableWallpaper(TM, PreviewStyle);
            Forms.MainFrm.pnl_preview.BackgroundImage = wall;
            Forms.MainFrm.pnl_preview_classic.BackgroundImage = wall;
            Forms.Metrics_Fonts.pnl_preview1.BackgroundImage = wall;
            Forms.Metrics_Fonts.pnl_preview2.BackgroundImage = wall;
            Forms.Metrics_Fonts.pnl_preview3.BackgroundImage = wall;
            Forms.Metrics_Fonts.pnl_preview4.BackgroundImage = wall;
            Forms.Metrics_Fonts.Classic_Preview1.BackgroundImage = wall;
            Forms.Metrics_Fonts.Classic_Preview3.BackgroundImage = wall;
            Forms.Metrics_Fonts.Classic_Preview4.BackgroundImage = wall;
            Forms.AltTabEditor.pnl_preview1.BackgroundImage = wall;
            Forms.AltTabEditor.Classic_Preview1.BackgroundImage = wall;
            return null;
        }
        private static void FetchStockWallpaper()
        {
            using (var wall_New = new Bitmap((Bitmap)GetWallpaper().Clone()))
            {

                Wallpaper_Unscaled = (Bitmap)wall_New.Clone();
                Wallpaper = (Bitmap)wall_New.GetThumbnailImage(Computer.Screen.Bounds.Width, Computer.Screen.Bounds.Height, null, IntPtr.Zero);
            }
        }
        public static Bitmap FetchSuitableWallpaper(Theme.Manager TM, PreviewHelpers.WindowStyle PreviewConfig)
        {
            using (PictureBox picbox = new() { Size = Forms.MainFrm.pnl_preview.Size, BackColor = TM.Win32.Background })
            {
                Bitmap Wall;

                if (!TM.Wallpaper.Enabled)
                {
                    FetchStockWallpaper();
                    Wall = Wallpaper;
                }
                else
                {
                    bool condition0 = PreviewConfig == PreviewHelpers.WindowStyle.W11 & TM.WallpaperTone_W11.Enabled;
                    bool condition1 = PreviewConfig == PreviewHelpers.WindowStyle.W10 & TM.WallpaperTone_W10.Enabled;
                    bool condition2 = PreviewConfig == PreviewHelpers.WindowStyle.W81 & TM.WallpaperTone_W81.Enabled;
                    bool condition3 = PreviewConfig == PreviewHelpers.WindowStyle.W7 & TM.WallpaperTone_W7.Enabled;
                    bool condition4 = PreviewConfig == PreviewHelpers.WindowStyle.WVista & TM.WallpaperTone_WVista.Enabled;
                    bool condition5 = PreviewConfig == PreviewHelpers.WindowStyle.WXP & TM.WallpaperTone_WXP.Enabled;
                    bool condition = condition0 || condition1 || condition2 || condition3 || condition4 || condition5;

                    if (condition)
                    {
                        switch (PreviewConfig)
                        {
                            case PreviewHelpers.WindowStyle.W11:
                                {
                                    Wall = PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_W11);
                                    break;
                                }

                            case PreviewHelpers.WindowStyle.W10:
                                {
                                    Wall = PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_W10);
                                    break;
                                }

                            case PreviewHelpers.WindowStyle.W81:
                                {
                                    Wall = PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_W81);
                                    break;
                                }

                            case PreviewHelpers.WindowStyle.W7:
                                {
                                    Wall = PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_W7);
                                    break;
                                }

                            case PreviewHelpers.WindowStyle.WVista:
                                {
                                    Wall = PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_WVista);
                                    break;
                                }

                            case PreviewHelpers.WindowStyle.WXP:
                                {
                                    Wall = PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_WXP);
                                    break;
                                }

                            default:
                                {
                                    Wall = PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_W11);
                                    break;
                                }

                        }
                    }

                    else if (TM.Wallpaper.WallpaperType == Theme.Structures.Wallpaper.WallpaperTypes.Picture)
                    {
                        if (System.IO.File.Exists(TM.Wallpaper.ImageFile))
                        {
                            Wall = Bitmap_Mgr.Load(TM.Wallpaper.ImageFile);
                        }
                        else
                        {
                            FetchStockWallpaper();
                            Wall = Wallpaper;
                        }
                    }

                    else if (TM.Wallpaper.WallpaperType == Theme.Structures.Wallpaper.WallpaperTypes.SolidColor)
                    {
                        Wall = null;
                    }

                    else if (TM.Wallpaper.WallpaperType == Theme.Structures.Wallpaper.WallpaperTypes.SlideShow)
                    {

                        if (TM.Wallpaper.SlideShow_Folder_or_ImagesList)
                        {
                            string[] ls = System.IO.Directory.EnumerateFiles(TM.Wallpaper.Wallpaper_Slideshow_ImagesRootPath, "*.*", System.IO.SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".bmp") || s.EndsWith(".jpg") || s.EndsWith(".png") || s.EndsWith(".gif")).ToArray();


                            if (ls.Count() > 0 && System.IO.File.Exists(ls[0]))
                            {
                                Wall = Bitmap_Mgr.Load(ls[0]);
                            }

                            else
                            {
                                FetchStockWallpaper();
                                Wall = Wallpaper;
                            }
                        }

                        else if (TM.Wallpaper.Wallpaper_Slideshow_Images.Count() > 0 && System.IO.File.Exists(TM.Wallpaper.Wallpaper_Slideshow_Images[0]))
                        {
                            Wall = Bitmap_Mgr.Load(TM.Wallpaper.Wallpaper_Slideshow_Images[0]);
                        }
                        else
                        {
                            FetchStockWallpaper();
                            Wall = Wallpaper;
                        }
                    }
                    else
                    {
                        FetchStockWallpaper();
                        Wall = Wallpaper;
                    }
                }

                if (Wall is not null)
                {

                    double ScaleW = 1;
                    double ScaleH = 1;

                    if (Wall.Width > Screen.PrimaryScreen.Bounds.Size.Width | Wall.Height > Screen.PrimaryScreen.Bounds.Size.Height)
                    {
                        ScaleW = (1920 / (double)picbox.Size.Width);
                        ScaleH = (1080 / (double)picbox.Size.Height);
                    }

                    Wall = Wall.Resize((int)Math.Round((double)Wall.Width / ScaleW), (int)Math.Round((double)Wall.Height / ScaleH));

                    if (TM.Wallpaper.WallpaperStyle == Theme.Structures.Wallpaper.WallpaperStyles.Fill)
                    {
                        picbox.SizeMode = PictureBoxSizeMode.CenterImage;
                        Wall = (Bitmap)((Bitmap)Wall.Clone()).FillScale(picbox.Size);
                    }

                    else if (TM.Wallpaper.WallpaperStyle == Theme.Structures.Wallpaper.WallpaperStyles.Fit)
                    {
                        picbox.SizeMode = PictureBoxSizeMode.Zoom;
                    }

                    else if (TM.Wallpaper.WallpaperStyle == Theme.Structures.Wallpaper.WallpaperStyles.Stretched)
                    {
                        picbox.SizeMode = PictureBoxSizeMode.StretchImage;
                    }

                    else if (TM.Wallpaper.WallpaperStyle == Theme.Structures.Wallpaper.WallpaperStyles.Centered)
                    {
                        picbox.SizeMode = PictureBoxSizeMode.CenterImage;
                    }

                    else if (TM.Wallpaper.WallpaperStyle == Theme.Structures.Wallpaper.WallpaperStyles.Tile)
                    {
                        picbox.SizeMode = PictureBoxSizeMode.Normal;
                        Wall = ((Bitmap)Wall.Clone()).Tile(picbox.Size);

                    }

                }

                picbox.Image = Wall;

                return picbox.ToBitmap();
            }
        }
        public static Bitmap GetWallpaper()
        {
            string WallpaperPath = GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", "").ToString();
            int WallpaperType = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "BackgroundType", 0));

            if (System.IO.File.Exists(WallpaperPath) && WallpaperType != 1)
            {
                return new Bitmap(Bitmap_Mgr.Load(WallpaperPath).GetThumbnailImage(Computer.Screen.Bounds.Width, Computer.Screen.Bounds.Height, null, IntPtr.Zero));
            }
            else
            {
                return (Bitmap)(GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "Background", "0 0 0").ToString().FromWin32RegToColor().ToBitmap(Computer.Screen.Bounds.Size));
            }
        }
        public static void WallpaperType_Changed(object sender, EventArrivedEventArgs e)
        {
            int WallpaperType = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "BackgroundType", 0));
            var S = new Stopwatch();
            if (WallpaperType != 1)
            {
                S.Reset();
                S.Start();
                while (!System.IO.File.Exists(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", "").ToString()))
                {
                    if (S.ElapsedMilliseconds > 5000L)
                        break;
                }
                S.Stop();
                Wallpaper_Changed();
            }
        }
        public static void Monitor()
        {
            var currentUser = WindowsIdentity.GetCurrent();
            string KeyPath;
            string valueName;
            string Base;

            KeyPath = @"Control Panel\Desktop";
            valueName = "Wallpaper";
            Base = string.Format(@"SELECT * FROM RegistryValueChangeEvent WHERE Hive='HKEY_USERS' AND KeyPath='{0}\\{1}' AND ValueName='{2}'", currentUser.User.Value, KeyPath.Replace(@"\", @"\\"), valueName);
            var query1 = new WqlEventQuery(Base);
            WallMon_Watcher1 = new ManagementEventWatcher(query1);

            KeyPath = @"Control Panel\Colors";
            valueName = "Background";
            Base = string.Format(@"SELECT * FROM RegistryValueChangeEvent WHERE Hive='HKEY_USERS' AND KeyPath='{0}\\{1}' AND ValueName='{2}'", currentUser.User.Value, KeyPath.Replace(@"\", @"\\"), valueName);
            var query2 = new WqlEventQuery(Base);
            WallMon_Watcher2 = new ManagementEventWatcher(query2);

            WallMon_Watcher1.EventArrived += Wallpaper_Changed_EventHandler;
            WallMon_Watcher1.Start();

            WallMon_Watcher2.EventArrived += Wallpaper_Changed_EventHandler;
            WallMon_Watcher2.Start();

            if (OS.W10 || OS.W11)
            {
                KeyPath = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers";
                valueName = "BackgroundType";
                Base = string.Format(@"SELECT * FROM RegistryValueChangeEvent WHERE Hive='HKEY_USERS' AND KeyPath='{0}\\{1}' AND ValueName='{2}'", currentUser.User.Value, KeyPath.Replace(@"\", @"\\"), valueName);
                var query3 = new WqlEventQuery(Base);
                WallMon_Watcher3 = new ManagementEventWatcher(query3);

                KeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
                valueName = "AppsUseLightTheme";
                Base = string.Format(@"SELECT * FROM RegistryValueChangeEvent WHERE Hive='HKEY_USERS' AND KeyPath='{0}\\{1}' AND ValueName='{2}'", currentUser.User.Value, KeyPath.Replace(@"\", @"\\"), valueName);
                var query4 = new WqlEventQuery(Base);
                WallMon_Watcher4 = new ManagementEventWatcher(query4);

                WallMon_Watcher3.EventArrived += WallpaperType_Changed;
                WallMon_Watcher3.Start();

                WallMon_Watcher4.EventArrived += DarkMode_Changed_EventHandler;
                WallMon_Watcher4.Start();
            }

            else
            {
                SystemEvents.UserPreferenceChanged += OldWinPreferenceChanged;
            }

        }
        public static void OldWinPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            if (OS.WXP && e.Category == UserPreferenceCategory.General)
            {
                Wallpaper_Changed();
            }
            else if (e.Category == UserPreferenceCategory.Desktop | e.Category == UserPreferenceCategory.Color)
                Wallpaper_Changed();
        }
        public static void DarkMode_Changed_EventHandler(object sender, EventArgs e)
        {
            DarkMode_Changed();
        }
        public static void DarkMode_Changed()
        {
            Invoke(UpdateDarkModeInvoker);
        }
        public static void Wallpaper_Changed_EventHandler(object sender, EventArgs e)
        {
            Wallpaper_Changed();
        }
        public static void Wallpaper_Changed()
        {
            Invoke(UpdateWallpaperInvoker);

        }
    }
}