using Microsoft.Win32;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Management;
using System.Threading.Channels;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.UI.WP.AlertBox;

namespace WinPaletter
{
    internal partial class Program
    {
        static readonly List<Tuple<ManagementEventWatcher, EventArrivedEventHandler>> Watchers = [];

        public static void Monitor()
        {
            if (Watchers.Count > 0) StopWatchers();

            string sid = User.Identity.User.Value;

            const string Personalize = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
            const string DWM = @"SOFTWARE\Microsoft\Windows\DWM";
            const string Transparency = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize";

            WallpaperMonitor.Start();

            if (OS.W10 || OS.W11 || OS.W12)
            {
                RegisterRegistryChangeEvent(sid, Personalize, "AppsUseLightTheme", DarkMode_Changed_EventHandler);
                RegisterRegistryChangeEvent(sid, Transparency, "EnableTransparency", Transparency_Changed_EventHandler);
            }

            if (!OS.WXP)
            {
                RegisterRegistryChangeEvent(sid, Personalize, "EnableTransparency", PreferencesRelatedToTitlebarExtenderChanged);
                RegisterRegistryChangeEvent(sid, DWM, "ColorPrevalence", PreferencesRelatedToTitlebarExtenderChanged);
            }

            Application.ApplicationExit += (_, _) => StopWatchers();
        }

        /// <summary>
        /// Handles OS-level user preference change notifications, filtering for Visual Style changes only.
        /// </summary>
        private static void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            if (e.Category != UserPreferenceCategory.General && e.Category != UserPreferenceCategory.VisualStyle) return;

            string currentThemeFile = !ClassicThemeRunning ? UxTheme.GetCurrentVS().Item1 : string.Empty;
            if (string.Equals(currentThemeFile, s_lastThemeFile, StringComparison.OrdinalIgnoreCase)) return;

            s_lastThemeFile = currentThemeFile;

            VisualStyle_Changed_EventHandler(sender, e);
        }
        private static string s_lastThemeFile = !ClassicThemeRunning ? UxTheme.GetCurrentVS().Item1 : string.Empty;

        /// <summary>
        /// Raised when the active Visual Style (.msstyles theme) changes.
        /// </summary>
        private static void VisualStyle_Changed_EventHandler(object sender, UserPreferenceChangedEventArgs e)
        {
            Program.Style.RoundedCorners = GetRoundedCorners();
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