using Microsoft.Win32;
using Microsoft.Xaml.Behaviors.Core;
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
        /// Monitor Windows preference changes (wallpaper, dark mode, and personalization).
        /// </summary>
        public static void Monitor()
        {
            // Stop monitoring if already active to prevent duplicates
            if (Watchers.Count > 0) StopWatchers();

            string sid = User.Identity.User.Value;

            //Predefine key paths
            const string Personalize = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
            const string DWM = @"SOFTWARE\Microsoft\Windows\DWM";
            const string Transparency = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize";

            WallpaperMonitor.Start();

            // Register modern Windows-specific events only if supported
            if (OS.W10 || OS.W11 || OS.W12)
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
                ManagementEventWatcher watcher;
                string escapedKey = $@"{sid}\{keyPath}".Replace(@"\", @"\\");
                string query = $"SELECT * FROM RegistryValueChangeEvent WHERE Hive='HKEY_USERS' AND KeyPath='{escapedKey}' AND ValueName='{valueName}'";
                watcher = new ManagementEventWatcher(new WqlEventQuery(query));
                watcher.EventArrived += handler;
                watcher.Start();

                Watchers.Add(new Tuple<ManagementEventWatcher, EventArrivedEventHandler>(watcher, handler));

                Program.Log?.Debug($"Registered watcher: {fullKey}\\{valueName}");
            }
            catch (ManagementException mex) when (mex.Message.Contains("Invalid class"))
            {
                Program.Log?.Debug($"Cannot register watcher: WMI class invalid on this system ({fullKey}\\{valueName})");
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