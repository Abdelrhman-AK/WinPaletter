using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Principal;
using System.Windows.Forms;
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
        /// Get the Windows desktop wallpaper
        /// </summary>
        /// <returns></returns>
        private static Bitmap GetWallpaperFromRegistry()
        {
            // If modern Windows, try TranscodedWallpaper first
            if (!OS.WXP && !OS.WVista)
            {
                string themesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Microsoft", "Windows", "Themes");
                string transcodedPath = Path.Combine(themesPath, "TranscodedWallpaper");

                if (File.Exists(transcodedPath))
                {
                    try
                    {
                        return BitmapMgr.Load(transcodedPath);
                    }
                    catch { }
                }
            }

            // Get wallpaper type and path from registry
            int wallpaperType = ReadReg("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Wallpapers", "BackgroundType", 0);
            string wallpaperPath = (wallpaperType != 1) ? (ReadReg("HKEY_CURRENT_USER\\Control Panel\\Desktop", "Wallpaper", string.Empty) ?? string.Empty) : null;

            // If the wallpaper type is a valid image and file exists, load it
            if (wallpaperType != 1 && !string.IsNullOrEmpty(wallpaperPath) && File.Exists(wallpaperPath))
            {
                return BitmapMgr.Load(wallpaperPath);
            }

            // Otherwise, fallback to solid color wallpaper
            string backgroundColor = ReadReg("HKEY_CURRENT_USER\\Control Panel\\Colors", "Background", Default.FromCurrentOS.Win32.Background.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)) ?? "255 255 255";
            return backgroundColor.ToColorFromWin32().ToBitmap(Screen.PrimaryScreen.Bounds.Size);
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
                    const string Personalize = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
                    const string DWM = @"SOFTWARE\Microsoft\Windows\DWM";
                    const string Transparency = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize";

                    WallpaperMonitor.Start();

                    // Register modern Windows-specific events only if supported
                    if (OS.W10 || OS.W11)
                    {
                        RegisterRegistryChangeEvent(sid, Personalize, "AppsUseLightTheme", DarkMode_Changed_EventHandler);
                        RegisterRegistryChangeEvent(sid, Transparency, "EnableTransparency", Transparency_Changed_EventHandler);
                    }

                    if (!OS.WXP)
                    {
                        // Register DWM/Transparency events (titlebar, effects, etc.)
                        RegisterRegistryChangeEvent(sid, Personalize, "EnableTransparency", PreferencesRelatedToTitlebarExtenderChanged);
                        RegisterRegistryChangeEvent(sid, DWM, "ColorPrevalence", PreferencesRelatedToTitlebarExtenderChanged);
                    }

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

            WallpaperMonitor.Stop();
        }
    }
}