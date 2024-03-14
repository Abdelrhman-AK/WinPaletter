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

            if (Windows11 != TMx.Windows11) _Equals = false;

            if (LogonUI10x != TMx.LogonUI10x) _Equals = false;

            if (Windows81 != TMx.Windows81) _Equals = false;

            if (Windows7 != TMx.Windows7) _Equals = false;

            if (WindowsVista != TMx.WindowsVista) _Equals = false;

            if (WindowsXP != TMx.WindowsXP) _Equals = false;

            if (LogonUI7 != TMx.LogonUI7) _Equals = false;

            if (LogonUIXP != TMx.LogonUIXP) _Equals = false;

            if (Win32 != TMx.Win32) _Equals = false;

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


            if (Cursor_Enabled != TMx.Cursor_Enabled) _Equals = false;

            if (Cursor_Trails != TMx.Cursor_Trails) _Equals = false;

            if (Cursor_Shadow != TMx.Cursor_Shadow) _Equals = false;

            if (Cursor_Arrow != TMx.Cursor_Arrow) _Equals = false;

            if (Cursor_Help != TMx.Cursor_Help) _Equals = false;

            if (Cursor_AppLoading != TMx.Cursor_AppLoading) _Equals = false;

            if (Cursor_Busy != TMx.Cursor_Busy) _Equals = false;

            if (Cursor_Move != TMx.Cursor_Move) _Equals = false;

            if (Cursor_NS != TMx.Cursor_NS) _Equals = false;

            if (Cursor_EW != TMx.Cursor_EW) _Equals = false;

            if (Cursor_NESW != TMx.Cursor_NESW) _Equals = false;

            if (Cursor_NWSE != TMx.Cursor_NWSE) _Equals = false;

            if (Cursor_Up != TMx.Cursor_Up) _Equals = false;

            if (Cursor_Pen != TMx.Cursor_Pen) _Equals = false;

            if (Cursor_None != TMx.Cursor_None) _Equals = false;

            if (Cursor_Link != TMx.Cursor_Link) _Equals = false;

            if (Cursor_Pin != TMx.Cursor_Pin) _Equals = false;

            if (Cursor_Person != TMx.Cursor_Person) _Equals = false;

            if (Cursor_IBeam != TMx.Cursor_IBeam) _Equals = false;

            if (Cursor_Cross != TMx.Cursor_Cross) _Equals = false;


            if (CommandPrompt != TMx.CommandPrompt) _Equals = false;

            if (PowerShellx86 != TMx.PowerShellx86) _Equals = false;

            if (PowerShellx64 != TMx.PowerShellx64) _Equals = false;

            //if (Terminal != TMx.Terminal) _Equals = false;

            //if (TerminalPreview != TMx.TerminalPreview) _Equals = false;

            return _Equals;
        }

        /// <summary>
        /// Checks if two theme managers are equal
        /// </summary>
        /// <param name="TM1">Theme manager #1</param>
        /// <param name="TM2">Theme manager #2</param>
        /// <returns></returns>
        public static bool operator ==(Manager TM1, Manager TM2) => TM1.Equals(TM2);

        /// <summary>
        /// Checks if two theme managers are not equal
        /// </summary>
        /// <param name="TM1">Theme manager #1</param>
        /// <param name="TM2">Theme manager #2</param>
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