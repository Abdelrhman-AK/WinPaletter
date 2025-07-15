namespace WinPaletter.Theme
{
    public partial class Manager
    {
        /// <summary>
        /// Checks if two WinPaletter themes are equal
        /// </summary>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            bool _Equals = true;

            Manager TMx = obj as Manager;

            if (Info != TMx.Info) _Equals = false;

            if (Windows12 != TMx.Windows12) _Equals = false;

            if (Windows11 != TMx.Windows11) _Equals = false;

            if (Windows10 != TMx.Windows10) _Equals = false;

            if (Windows81 != TMx.Windows81) _Equals = false;

            if (Windows7 != TMx.Windows7) _Equals = false;

            if (WindowsVista != TMx.WindowsVista) _Equals = false;

            if (WindowsXP != TMx.WindowsXP) _Equals = false;

            if (LogonUI12 != TMx.LogonUI12) _Equals = false;

            if (LogonUI11 != TMx.LogonUI11) _Equals = false;

            if (LogonUI10 != TMx.LogonUI10) _Equals = false;

            if (LogonUI81 != TMx.LogonUI81) _Equals = false;

            if (LogonUI7 != TMx.LogonUI7) _Equals = false;

            if (LogonUIXP != TMx.LogonUIXP) _Equals = false;

            if (Win32 != TMx.Win32) _Equals = false;

            if (Accessibility != TMx.Accessibility) _Equals = false;

            if (WindowsEffects != TMx.WindowsEffects) _Equals = false;

            if (MetricsFonts != TMx.MetricsFonts) _Equals = false;

            if (AltTab != TMx.AltTab) _Equals = false;

            if (WallpaperTone_W11 != TMx.WallpaperTone_W11) _Equals = false;

            if (WallpaperTone_W10 != TMx.WallpaperTone_W10) _Equals = false;

            if (WallpaperTone_W81 != TMx.WallpaperTone_W81) _Equals = false;

            if (WallpaperTone_W7 != TMx.WallpaperTone_W7) _Equals = false;

            if (WallpaperTone_WVista != TMx.WallpaperTone_WVista) _Equals = false;

            if (WallpaperTone_WXP != TMx.WallpaperTone_WXP) _Equals = false;

            if (ScreenSaver != TMx.ScreenSaver) _Equals = false;

            if (Sounds != TMx.Sounds) _Equals = false;

            if (Wallpaper != TMx.Wallpaper) _Equals = false;

            if (AppTheme != TMx.AppTheme) _Equals = false;

            if (Icons != TMx.Icons) _Equals = false;

            if (Cursors != TMx.Cursors) _Equals = false;

            if (CommandPrompt != TMx.CommandPrompt) _Equals = false;

            if (PowerShellx86 != TMx.PowerShellx86) _Equals = false;

            if (PowerShellx64 != TMx.PowerShellx64) _Equals = false;

            //if (Terminal != TMx.Terminal) _Equals = false;

            //if (TerminalPreview != TMx.TerminalPreview) _Equals = false;

            Program.Log?.Write(Serilog.Events.LogEventLevel.Debug, $"Comparing WinPaletter themes: {Info.ThemeName} == {TMx.Info.ThemeName} => {_Equals}");

            return _Equals;
        }

        /// <summary>
        /// Checks if two theme managers are equal
        /// </summary>
        /// <param name="TM1">WinTheme manager #1</param>
        /// <param name="TM2">WinTheme manager #2</param>
        /// <returns></returns>
        public static bool operator ==(Manager TM1, Manager TM2) => TM1.Equals(TM2);

        /// <summary>
        /// Checks if two theme managers are not equal
        /// </summary>
        /// <param name="TM1">WinTheme manager #1</param>
        /// <param name="TM2">WinTheme manager #2</param>
        /// <returns></returns>
        public static bool operator !=(Manager TM1, Manager TM2) => !(TM1 == TM2);

        /// <summary>
        /// WinPaletter theme hash code
        /// </summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}