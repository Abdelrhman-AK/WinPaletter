using System;
using System.Drawing;

namespace WinPaletter
{
    internal partial class Program
    {
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

        private static void Transparency_Changed_EventHandler(object sender, EventArgs e)
        {
            windowsTransparency = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", 1) == 1;
            WindowsTransparencyChanged?.Invoke(windowsTransparency);
        }

        /// <summary>
        /// Event handler for the wallpaper change event to update the wallpaper in all open forms previews
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void PreferencesRelatedToTitlebarExtenderChanged(object sender, EventArgs e)
        {
            Tabs.TitlebarExtender.AccentOnTitlebars = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", 1) == 1;
        }
    }
}