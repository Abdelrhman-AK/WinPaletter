using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace WinPaletter
{
    internal partial class Program
    {
        static List<Tuple<ManagementEventWatcher, EventArrivedEventHandler>> Watchers = new();

        static Thread thread;

        private static MethodInvoker UpdateWallpaperInvoker()
        {

            if (thread != null && thread.IsAlive) thread.Abort();

            thread = new(() =>
            {
                Bitmap wall = FetchSuitableWallpaper(TM, WindowStyle);
                Invoke(() =>
                {
                    Forms.Metrics_Fonts.windowMetrics1.BackgroundImage = wall;
                    Forms.Metrics_Fonts.Desktop_icons.BackgroundImage = wall;
                    Forms.AltTabEditor.pnl_preview1.BackgroundImage = wall;
                    Forms.AltTabEditor.Classic_Preview1.BackgroundImage = wall;
                });
            });

            thread.Start();

            return null;
        }

        private static Bitmap ThumbnailWallpaper
        {
            get => GetWallpaperFromRegistry().GetThumbnailImage(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, null, IntPtr.Zero) as Bitmap;
        }

        public static Bitmap FetchSuitableWallpaper(Theme.Manager TM, PreviewHelpers.WindowStyle previewConfig)
        {
            using (PictureBox picbox = new() { Size = Forms.Win11Colors.windowsDesktop1.Size, BackColor = TM.Win32.Background })
            {
                Bitmap wallpaper;

                if (!TM.Wallpaper.Enabled)
                {
                    wallpaper = ThumbnailWallpaper;
                }
                else
                {
                    wallpaper = GetTintedWallpaper(TM, previewConfig);

                    if (wallpaper == null)
                    {
                        wallpaper = TM.Wallpaper.WallpaperType switch
                        {
                            Theme.Structures.Wallpaper.WallpaperTypes.Picture when System.IO.File.Exists(TM.Wallpaper.ImageFile) => Bitmap_Mgr.Load(TM.Wallpaper.ImageFile),
                            Theme.Structures.Wallpaper.WallpaperTypes.SolidColor => null,
                            Theme.Structures.Wallpaper.WallpaperTypes.SlideShow => FetchSlideShowWallpaper(TM),
                            _ => ThumbnailWallpaper
                        };
                    }
                }

                if (wallpaper != null)
                {
                    wallpaper = GetWallpaperWithStyle(wallpaper, picbox.Size, TM.Wallpaper.WallpaperStyle);
                }

                picbox.Image = wallpaper;
                return picbox.ToBitmap();
            }
        }

        private static Bitmap GetTintedWallpaper(Theme.Manager TM, PreviewHelpers.WindowStyle previewConfig)
        {
            if (previewConfig == PreviewHelpers.WindowStyle.W12 && TM.WallpaperTone_W12.Enabled)
            {
                return PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_W12);
            }
            else if (previewConfig == PreviewHelpers.WindowStyle.W11 && TM.WallpaperTone_W11.Enabled)
            {
                return PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_W11);
            }
            else if (previewConfig == PreviewHelpers.WindowStyle.W10 && TM.WallpaperTone_W10.Enabled)
            {
                return PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_W10);
            }
            else if (previewConfig == PreviewHelpers.WindowStyle.W81 && TM.WallpaperTone_W81.Enabled)
            {
                return PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_W81);
            }
            else if (previewConfig == PreviewHelpers.WindowStyle.W7 && TM.WallpaperTone_W7.Enabled)
            {
                return PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_W7);
            }
            else if (previewConfig == PreviewHelpers.WindowStyle.WVista && TM.WallpaperTone_WVista.Enabled)
            {
                return PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_WVista);
            }
            else if (previewConfig == PreviewHelpers.WindowStyle.WXP && TM.WallpaperTone_WXP.Enabled)
            {
                return PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_WXP);
            }

            return null;
        }

        private static Bitmap FetchSlideShowWallpaper(Theme.Manager TM)
        {
            string[] imageFiles = TM.Wallpaper.SlideShow_Folder_or_ImagesList
                ? System.IO.Directory.EnumerateFiles(TM.Wallpaper.Wallpaper_Slideshow_ImagesRootPath, "*.*", System.IO.SearchOption.TopDirectoryOnly)
                    .Where(s => s.EndsWith(".bmp") || s.EndsWith(".jpg") || s.EndsWith(".png") || s.EndsWith(".gif"))
                    .ToArray()
                : TM.Wallpaper.Wallpaper_Slideshow_Images;

            if (imageFiles.Length > 0 && System.IO.File.Exists(imageFiles[0]))
            {
                return Bitmap_Mgr.Load(imageFiles[0]);
            }

            return ThumbnailWallpaper;
        }

        private static Bitmap GetWallpaperWithStyle(Bitmap wallpaper, Size targetSize, Theme.Structures.Wallpaper.WallpaperStyles wallpaperStyle)
        {
            double scaleW = 1;
            double scaleH = 1;

            if (wallpaper.Width > Screen.PrimaryScreen.Bounds.Size.Width || wallpaper.Height > Screen.PrimaryScreen.Bounds.Size.Height)
            {
                scaleW = Screen.PrimaryScreen.Bounds.Width / targetSize.Width;
                scaleH = Screen.PrimaryScreen.Bounds.Height / targetSize.Height;
            }

            wallpaper = wallpaper.Resize((int)Math.Round(wallpaper.Width / scaleW), (int)Math.Round(wallpaper.Height / scaleH));

            return wallpaperStyle switch
            {
                Theme.Structures.Wallpaper.WallpaperStyles.Fill => wallpaper.FillScale(targetSize),
                Theme.Structures.Wallpaper.WallpaperStyles.Fit => wallpaper,
                Theme.Structures.Wallpaper.WallpaperStyles.Stretched => wallpaper,
                Theme.Structures.Wallpaper.WallpaperStyles.Centered => wallpaper,
                Theme.Structures.Wallpaper.WallpaperStyles.Tile => wallpaper.Tile(targetSize),
                _ => wallpaper
            };
        }

        public static Bitmap GetWallpaperFromRegistry()
        {
            string wallpaperPath = GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", string.Empty).ToString();
            int wallpaperType = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "BackgroundType", 0));

            if (System.IO.File.Exists(wallpaperPath) && wallpaperType != 1)
            {
                return Bitmap_Mgr.Load(wallpaperPath).GetThumbnailImage(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, null, IntPtr.Zero) as Bitmap;
            }
            else
            {
                string backgroundColor = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "Background", Theme.Default.Get().Win32.Background.ToWin32Reg()).ToString();
                return backgroundColor.FromWin32RegToColor().ToBitmap(Screen.PrimaryScreen.Bounds.Size);
            }
        }

        public static void WallpaperType_Changed(object sender, EventArrivedEventArgs e)
        {
            int wallpaperType = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "BackgroundType", 0));
            Stopwatch stopwatch = new();

            if (wallpaperType != 1)
            {
                stopwatch.Reset();
                stopwatch.Start();

                while (!System.IO.File.Exists(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", string.Empty).ToString()))
                {
                    if (stopwatch.ElapsedMilliseconds > 5000L)
                        break;
                }

                stopwatch.Stop();
                Wallpaper_Changed();
            }
        }

        public static void Monitor()
        {
            WindowsIdentity currentUser = WindowsIdentity.GetCurrent();

            RegisterRegistryChangeEvent(currentUser.User.Value, @"Control Panel\Desktop", "Wallpaper", Wallpaper_Changed_EventHandler);
            RegisterRegistryChangeEvent(currentUser.User.Value, @"Control Panel\Colors", "Background", Wallpaper_Changed_EventHandler);

            if (!OS.WXP && !OS.WVista && !OS.W7 && !OS.W8 && !OS.W81)
            {
                RegisterRegistryChangeEvent(currentUser.User.Value, @"Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "BackgroundType", WallpaperType_Changed);
                RegisterRegistryChangeEvent(currentUser.User.Value, @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", DarkMode_Changed_EventHandler);
            }
            else
            {
                SystemEvents.UserPreferenceChanged += OldWinPreferenceChanged;
            }

            Application.ApplicationExit += (sender, e) => StopWatchers();
        }

        private static void RegisterRegistryChangeEvent(string hive, string keyPath, string valueName, EventArrivedEventHandler eventHandler)
        {
            string query = $@"SELECT * FROM RegistryValueChangeEvent WHERE Hive='HKEY_USERS' AND KeyPath='{hive}\\{(keyPath.Replace(@"\", @"\\"))}' AND ValueName='{valueName}'";
            WqlEventQuery eventQuery = new(query);
            ManagementEventWatcher watcher = new(eventQuery);
            watcher.EventArrived += eventHandler;
            watcher.Start();

            // Store the watcher for later cleanup
            Watchers.Add(new Tuple<ManagementEventWatcher, EventArrivedEventHandler>(watcher, eventHandler));
        }

        private static void StopWatchers()
        {
            foreach (Tuple<ManagementEventWatcher, EventArrivedEventHandler> watcher in Watchers)
            {
                watcher.Item1.Stop();
                watcher.Item1.EventArrived -= watcher.Item2;
                watcher.Item1.Dispose();
            }

            Watchers.Clear();
        }

        private static void DarkMode_Changed_EventHandler(object sender, EventArgs e)
        {
            Invoke(() =>
            {
                GetDarkMode();
                if (Settings.Appearance.AutoDarkMode) ApplyStyle();
            });
        }

        private static void Wallpaper_Changed_EventHandler(object sender, EventArgs e)
        {
            Wallpaper_Changed();
        }

        private static void OldWinPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            if (OS.WXP && e.Category == UserPreferenceCategory.General)
            {
                Wallpaper_Changed();
            }
            else if (e.Category == UserPreferenceCategory.Desktop || e.Category == UserPreferenceCategory.Color)
            {
                Wallpaper_Changed();
            }
        }

        public static void Wallpaper_Changed()
        {
            Invoke(UpdateWallpaperInvoker);
        }
    }
}