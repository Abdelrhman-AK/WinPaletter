using Microsoft.Win32;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using WinPaletter.Tabs;
using WinPaletter.Theme;
using WinPaletter.Theme.Structures;

namespace WinPaletter
{
    internal partial class Program
    {
        /// <summary>
        /// List of watchers and their event handlers
        /// </summary>
        static readonly List<Tuple<ManagementEventWatcher, EventArrivedEventHandler>> Watchers = [];

        /// <summary>
        /// Thread for updating the wallpaper
        /// </summary>
        static Thread thread;

        /// <summary>
        /// Invoker for updating the wallpaper
        /// </summary>
        /// <returns></returns>
        private static MethodInvoker UpdateWallpaperInvoker()
        {
            // If the thread is running, abort it
            if (thread != null && thread.IsAlive) thread.Abort();

            Program.Log?.Debug("Updating wallpaper preview in all open forms previews (as a signal of wallpaper change has been received).");
            thread = new(() =>
            {
                Bitmap wall = FetchSuitableWallpaper(TM, WindowStyle);
                Invoke(() =>
                {
                    // Update the wallpaper in all open forms if their previews should has Windows desktop wallpaper

                    if (Application.OpenForms.OfType<Win12Colors>().Count() > 0)
                    {
                        Forms.Win12Colors.windowsDesktop1.BackgroundImage = wall;
                    }

                    if (Application.OpenForms.OfType<Win11Colors>().Count() > 0)
                    {
                        Forms.Win11Colors.windowsDesktop1.BackgroundImage = wall;
                    }

                    if (Application.OpenForms.OfType<Win10Colors>().Count() > 0)
                    {
                        Forms.Win10Colors.windowsDesktop1.BackgroundImage = wall;
                    }

                    if (Application.OpenForms.OfType<Win81Colors>().Count() > 0)
                    {
                        Forms.Win81Colors.windowsDesktop1.BackgroundImage = wall;
                    }

                    if (Application.OpenForms.OfType<Win8Colors>().Count() > 0)
                    {
                        Forms.Win8Colors.windowsDesktop1.BackgroundImage = wall;
                    }

                    if (Application.OpenForms.OfType<Win7Colors>().Count() > 0)
                    {
                        Forms.Win7Colors.windowsDesktop1.BackgroundImage = wall;
                    }

                    if (Application.OpenForms.OfType<WinVistaColors>().Count() > 0)
                    {
                        Forms.WinVistaColors.windowsDesktop1.BackgroundImage = wall;
                    }

                    if (Application.OpenForms.OfType<WinXPColors>().Count() > 0)
                    {
                        Forms.WinXPColors.windowsDesktop1.BackgroundImage = wall;
                    }

                    if (Application.OpenForms.OfType<Metrics_Fonts>().Count() > 0)
                    {
                        Forms.Metrics_Fonts.windowMetrics1.BackgroundImage = wall;
                        Forms.Metrics_Fonts.Desktop_icons.BackgroundImage = wall;
                    }

                    if (Application.OpenForms.OfType<AltTabEditor>().Count() > 0)
                    {
                        Forms.AltTabEditor.pnl_preview1.BackgroundImage = wall;
                        Forms.AltTabEditor.Classic_Preview1.BackgroundImage = wall;
                    }

                    if (Application.OpenForms.OfType<IconsStudio>().Count() > 0)
                    {
                        Forms.IconsStudio.pnl_preview.BackgroundImage = wall;
                    }

                    if (Application.OpenForms.OfType<CMD>().Count() > 0)
                    {
                        Forms.CMD.CMD_Preview.BackgroundImage = wall;
                    }

                    if (Application.OpenForms.OfType<ExternalTerminal>().Count() > 0)
                    {
                        Forms.ExternalTerminal.CMD_Preview.BackgroundImage = wall;
                    }
                });
            });

            thread.Start();

            return null;
        }

        /// <summary>
        /// Get the Windows desktop wallpaper
        /// </summary>
        private static Bitmap ThumbnailWallpaper
        {
            get
            {
                using (Bitmap bmp = GetWallpaperFromRegistry())
                {
                    return bmp.Resize(PreviewSize.Width * 2, PreviewSize.Height * 2);
                }
            }
        }

        /// <summary>
        /// Get the suitable wallpaper according to the theme manager preferences and preview configuration
        /// </summary>
        /// <param name="TM"></param>
        /// <param name="previewConfig"></param>
        /// <returns></returns>
        public static Bitmap FetchSuitableWallpaper(Manager TM, PreviewHelpers.WindowStyle previewConfig)
        {
            Log?.Write(LogEventLevel.Information, $"Fetching suitable wallpaper for {previewConfig}");

            // Create a PictureBox to mimic the Windows desktop ratio and wallpaper style
            using (PictureBox picbox = new() { Size = Program.PreviewSize, BackColor = TM.Win32.Background })
            {
                Bitmap wallpaper;

                if (!TM.Wallpaper.Enabled)
                {
                    wallpaper = ThumbnailWallpaper;
                }
                else
                {
                    // If the theme has a tinted wallpaper, use it. Otherwise, use the theme's wallpaper. This function will return null if wallpaper tint is not enabled.
                    wallpaper = GetTintedWallpaper(TM, previewConfig);

                    // If the wallpaper is not tinted, get the wallpaper according to the theme manager settings.
                    wallpaper ??= TM.Wallpaper.WallpaperType switch
                    {
                        Theme.Structures.Wallpaper.WallpaperTypes.Picture when File.Exists(TM.Wallpaper.ImageFile) => BitmapMgr.Load(TM.Wallpaper.ImageFile),
                        Theme.Structures.Wallpaper.WallpaperTypes.SolidColor => null,
                        Theme.Structures.Wallpaper.WallpaperTypes.SlideShow => FetchSlideShowWallpaper(TM),
                        _ => ThumbnailWallpaper
                    };
                }

                // If the wallpaper is not null, apply the wallpaper style (fill/fit/stretch/tile/...).
                if (wallpaper != null)
                {
                    wallpaper = ApplyStyleToWallpaper(wallpaper, picbox.Size, TM.Wallpaper.WallpaperStyle);
                }

                picbox.Image = wallpaper;
                return picbox.ToBitmap();
            }
        }

        /// <summary>
        /// Get the tinted wallpaper according to the <see cref="Theme.Manager"/> preferences and preview configuration
        /// </summary>
        /// <param name="TM"></param>
        /// <param name="previewConfig"></param>
        /// <returns></returns>
        private static Bitmap GetTintedWallpaper(Manager TM, PreviewHelpers.WindowStyle previewConfig)
        {
            if (previewConfig == PreviewHelpers.WindowStyle.W12 && TM.WallpaperTone_W12.Enabled)
            {
                Log?.Write(LogEventLevel.Information, "Fetching a tinted wallpaper for preview as it is enabled in the theme manager of Windows 12's section.");
                return PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_W12);
            }
            else if (previewConfig == PreviewHelpers.WindowStyle.W11 && TM.WallpaperTone_W11.Enabled)
            {
                Log?.Write(LogEventLevel.Information, "Fetching a tinted wallpaper for preview as it is enabled in the theme manager of Windows 11's section.");
                return PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_W11);
            }
            else if (previewConfig == PreviewHelpers.WindowStyle.W10 && TM.WallpaperTone_W10.Enabled)
            {
                Log?.Write(LogEventLevel.Information, "Fetching a tinted wallpaper for preview as it is enabled in the theme manager of Windows 10's section.");
                return PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_W10);
            }
            else if (previewConfig == PreviewHelpers.WindowStyle.W81 && TM.WallpaperTone_W81.Enabled)
            {
                Log?.Write(LogEventLevel.Information, "Fetching a tinted wallpaper for preview as it is enabled in the theme manager of Windows 8.1's section.");
                return PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_W81);
            }
            else if (previewConfig == PreviewHelpers.WindowStyle.W7 && TM.WallpaperTone_W7.Enabled)
            {
                Log?.Write(LogEventLevel.Information, "Fetching a tinted wallpaper for preview as it is enabled in the theme manager of Windows 7's section.");
                return PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_W7);
            }
            else if (previewConfig == PreviewHelpers.WindowStyle.WVista && TM.WallpaperTone_WVista.Enabled)
            {
                Log?.Write(LogEventLevel.Information, "Fetching a tinted wallpaper for preview as it is enabled in the theme manager of Windows Vista's section.");
                return PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_WVista);
            }
            else if (previewConfig == PreviewHelpers.WindowStyle.WXP && TM.WallpaperTone_WXP.Enabled)
            {
                Log?.Write(LogEventLevel.Information, "Fetching a tinted wallpaper for preview as it is enabled in the theme manager of Windows XP's section.");
                return PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_WXP);
            }

            return null;
        }

        /// <summary>
        /// Get the slideshow wallpaper according to the <see cref="Theme.Manager"/> settings
        /// </summary>
        /// <param name="TM"></param>
        /// <returns></returns>
        private static Bitmap FetchSlideShowWallpaper(Manager TM)
        {
            string[] imageFiles;

            if (TM.Wallpaper.SlideShow_Folder_or_ImagesList)
            {
                string slideshowPath = TM.Wallpaper.Wallpaper_Slideshow_ImagesRootPath;

                if (Directory.Exists(slideshowPath))
                {
                    imageFiles = [.. Directory.EnumerateFiles(slideshowPath, "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".bmp") || s.EndsWith(".jpg") || s.EndsWith(".png") || s.EndsWith(".gif"))];
                }
                else
                {
                    imageFiles = [];
                }
            }
            else
            {
                // Use TM.Wallpaper.Wallpaper_Slideshow_Images directly if not using a folder for slideshow
                imageFiles = TM.Wallpaper.Wallpaper_Slideshow_Images;
            }

            if (imageFiles.Length > 0 && File.Exists(imageFiles[0]))
            {
                Program.Log?.Write(LogEventLevel.Information, $"Fetching the first image from the slideshow folder to be used as a preview: {imageFiles[0]}");

                return BitmapMgr.Load(imageFiles[0]);
            }

            // If the first image is not found, return the Windows desktop wallpaper
            return ThumbnailWallpaper;
        }

        /// <summary>
        /// Get the wallpaper with the specified style
        /// </summary>
        /// <param name="wallpaper"></param>
        /// <param name="targetSize"></param>
        /// <param name="wallpaperStyle"></param>
        /// <returns></returns>
        private static Bitmap ApplyStyleToWallpaper(Bitmap wallpaper, Size targetSize, Wallpaper.WallpaperStyles wallpaperStyle)
        {
            float scaleW = 1;
            float scaleH = 1;

            // Rescale the wallpaper according to the screen size
            if (wallpaper.Width > Screen.PrimaryScreen.Bounds.Size.Width || wallpaper.Height > Screen.PrimaryScreen.Bounds.Size.Height)
            {
                scaleW = Screen.PrimaryScreen.Bounds.Width / targetSize.Width;
                scaleH = Screen.PrimaryScreen.Bounds.Height / targetSize.Height;
            }

            // Resize the wallpaper according to the scale and preview area siz
            wallpaper = wallpaper.Resize((int)(wallpaper.Width / scaleW), (int)(wallpaper.Height / scaleH));

            Program.Log?.Write(LogEventLevel.Information, $"Rescaling wallpaper preview to {wallpaper.Width}x{wallpaper.Height} and adjusting its style");

            // Apply the wallpaper style
            return wallpaperStyle switch
            {
                Theme.Structures.Wallpaper.WallpaperStyles.Fill => wallpaper.FillInSize(targetSize),
                Theme.Structures.Wallpaper.WallpaperStyles.Fit => wallpaper,
                Theme.Structures.Wallpaper.WallpaperStyles.Stretched => wallpaper,
                Theme.Structures.Wallpaper.WallpaperStyles.Centered => wallpaper,
                Theme.Structures.Wallpaper.WallpaperStyles.Tile => wallpaper.Tile(targetSize),
                _ => wallpaper
            };
        }

        // Get the Windows desktop wallpaper
        public static Bitmap GetWallpaperFromRegistry()
        {
            // Get wallpaper type and path
            int wallpaperType = ReadReg("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Wallpapers", "BackgroundType", 0);
            string wallpaperPath = (wallpaperType != 1)
                ? (ReadReg("HKEY_CURRENT_USER\\Control Panel\\Desktop", "Wallpaper", string.Empty) ?? string.Empty)
                : null;

            // If the wallpaper type is valid (image), return the wallpaper from the registry
            if (wallpaperType != 1 && !string.IsNullOrEmpty(wallpaperPath) && File.Exists(wallpaperPath))
            {
                return BitmapMgr.Load(wallpaperPath);
            }

            // If the wallpaper type is solid color, return the solid color wallpaper
            string backgroundColor = (ReadReg("HKEY_CURRENT_USER\\Control Panel\\Colors", "Background", Default.Get().Win32.Background.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)) ?? "255 255 255");
            return backgroundColor.ToColorFromWin32().ToBitmap(Screen.PrimaryScreen.Bounds.Size);
        }

        /// <summary>
        /// Event handler for the wallpaper change event to update the wallpaper in all open forms previews
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void WallpaperType_Changed(object sender, EventArgs e)
        {
            int wallpaperType = ReadReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "BackgroundType", 0);
            Stopwatch stopwatch = new();

            // If the wallpaper type is not solid color, wait for the wallpaper to be available and update the wallpaper
            if (wallpaperType != 1)
            {
                stopwatch.Reset();
                stopwatch.Start();

                while (!File.Exists(ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", string.Empty)))
                {
                    // Wait for the wallpaper to be available as there is a delay between the registry change and the actual wallpaper change
                    if (stopwatch.ElapsedMilliseconds > 5000L) break;
                }

                stopwatch.Stop();
            }

            Program.Log?.Write(LogEventLevel.Information, $"Wallpaper type changed to {wallpaperType}");

            Wallpaper_Changed();
        }

        /// <summary>
        /// Monitor Windows preference changes (wallpaper, dark mode, and personalization).
        /// </summary>
        public static void Monitor()
        {
            // Skip initialization if already monitoring
            if (Watchers.Count > 0) return;

            string sid = User.Identity.User.Value;

            // Impersonate only once
            using (WindowsImpersonationContext wic = User.Identity.Impersonate())
            {
                try
                {
                    //Predefine key paths
                    const string Desktop = @"Control Panel\Desktop";
                    const string Colors = @"Control Panel\Colors";
                    const string Explorer = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers";
                    const string Personalize = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
                    const string DWM = @"SOFTWARE\Microsoft\Windows\DWM";

                    // Register essential events first
                    RegisterRegistryChangeEvent(sid, Desktop, "Wallpaper", Wallpaper_Changed_EventHandler);
                    RegisterRegistryChangeEvent(sid, Colors, "Background", Wallpaper_Changed_EventHandler);

                    // Register modern Windows-specific events only if supported
                    if (OS.W10 || OS.W11)
                    {
                        RegisterRegistryChangeEvent(sid, Explorer, "BackgroundType", WallpaperType_Changed);
                        RegisterRegistryChangeEvent(sid, Personalize, "AppsUseLightTheme", DarkMode_Changed_EventHandler);
                    }
                    else
                    {
                        // Use legacy fallback for old systems
                        SystemEvents.UserPreferenceChanged += OldWinPreferenceChanged;
                    }

                    // Register DWM/Transparency events (titlebar, effects, etc.)
                    RegisterRegistryChangeEvent(sid, Personalize, "EnableTransparency", PreferencesRelatedToTitlebarExtenderChanged);
                    RegisterRegistryChangeEvent(sid, DWM, "ColorPrevalence", PreferencesRelatedToTitlebarExtenderChanged);

                    // Automatically clean up on exit
                    Application.ApplicationExit += (_, _) => StopWatchers();
                }
                finally
                {
                    wic.Undo();
                }
            }
        }

        /// <summary>
        /// Register a registry watcher if the value exists.
        /// </summary>
        private static void RegisterRegistryChangeEvent(string sid, string keyPath, string valueName, EventArrivedEventHandler handler)
        {
            string fullKey = $@"HKEY_USERS\{sid}\{keyPath}";

            if (!ValueExists(fullKey, valueName))
            {
                Program.Log?.Debug($"Skipped watcher: {fullKey}\\{valueName} does not exist.");
                return;
            }

            try
            {
                // WMI expects double backslashes inside the query string only
                string escapedKey = $@"{sid}\{keyPath}".Replace(@"\", @"\\");
                string query = $"SELECT * FROM RegistryValueChangeEvent WHERE Hive='HKEY_USERS' AND KeyPath='{escapedKey}' AND ValueName='{valueName}'";

                ManagementEventWatcher watcher = new(new WqlEventQuery(query));
                watcher.EventArrived += handler;
                watcher.Start();

                Watchers.Add(new Tuple<ManagementEventWatcher, EventArrivedEventHandler>(watcher, handler));

                Program.Log?.Debug($"Registered watcher: {fullKey}\\{valueName}");
            }
            catch (Exception ex)
            {
                Program.Log?.Write(LogEventLevel.Error, $"Failed watcher registration for {fullKey}\\{valueName}", ex);
            }
        }


        /// <summary>
        /// Stop all the registered watchers
        /// </summary>
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

        /// <summary>
        /// Event handler for the dark mode change event to update the dark mode in all open forms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void DarkMode_Changed_EventHandler(object sender, EventArgs e)
        {
            Invoke(() =>
            {
                Program.Log?.Debug("Dark mode changed event received");
                GetDarkMode();
                if (Settings.Appearance.AutoDarkMode) ApplyStyle();
            });
        }

        /// <summary>
        /// Event handler for the wallpaper change event to update the wallpaper in all open forms previews
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Wallpaper_Changed_EventHandler(object sender, EventArgs e)
        {
            Wallpaper_Changed();
        }

        /// <summary>
        /// Event handler for the wallpaper change event to update the wallpaper in all open forms previews
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void PreferencesRelatedToTitlebarExtenderChanged(object sender, EventArgs e)
        {
            TitlebarExtender.Transparency = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", 1) == 1;
            TitlebarExtender.AccentOnTitlebars = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", 1) == 1;
        }

        /// <summary>
        /// Event handler for the old Windows (WXP) preferences change event to update the wallpaper in all open forms previews
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Update the wallpaper in all open forms previews by invoking the <see cref="UpdateWallpaperInvoker"/>
        /// </summary>
        public static void Wallpaper_Changed()
        {
            Invoke(UpdateWallpaperInvoker);
        }
    }
}