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
            bool _Equals = true;

            if (Info != ((Manager)obj).Info)
                _Equals = false;
            if (Windows11 != ((Manager)obj).Windows11)
                _Equals = false;
            if (LogonUI10x != ((Manager)obj).LogonUI10x)
                _Equals = false;
            if (Windows81 != ((Manager)obj).Windows81)
                _Equals = false;
            if (Windows7 != ((Manager)obj).Windows7)
                _Equals = false;
            if (WindowsVista != ((Manager)obj).WindowsVista)
                _Equals = false;
            if (WindowsXP != ((Manager)obj).WindowsXP)
                _Equals = false;
            if (LogonUI7 != ((Manager)obj).LogonUI7)
                _Equals = false;
            if (LogonUIXP != ((Manager)obj).LogonUIXP)
                _Equals = false;
            if (Win32 != ((Manager)obj).Win32)
                _Equals = false;
            if (WindowsEffects != ((Manager)obj).WindowsEffects)
                _Equals = false;
            if (MetricsFonts != ((Manager)obj).MetricsFonts)
                _Equals = false;
            if (AltTab != ((Manager)obj).AltTab)
                _Equals = false;
            if (WallpaperTone_W11 != ((Manager)obj).WallpaperTone_W11)
                _Equals = false;
            if (WallpaperTone_W10 != ((Manager)obj).WallpaperTone_W10)
                _Equals = false;
            if (WallpaperTone_W81 != ((Manager)obj).WallpaperTone_W81)
                _Equals = false;
            if (WallpaperTone_W7 != ((Manager)obj).WallpaperTone_W7)
                _Equals = false;
            if (WallpaperTone_WVista != ((Manager)obj).WallpaperTone_WVista)
                _Equals = false;
            if (WallpaperTone_WXP != ((Manager)obj).WallpaperTone_WXP)
                _Equals = false;
            if (ScreenSaver != ((Manager)obj).ScreenSaver)
                _Equals = false;
            if (Sounds != ((Manager)obj).Sounds)
                _Equals = false;
            if (Wallpaper != ((Manager)obj).Wallpaper)
                _Equals = false;
            if (AppTheme != ((Manager)obj).AppTheme)
                _Equals = false;

            if (Cursor_Enabled != ((Manager)obj).Cursor_Enabled)
                _Equals = false;
            if (Cursor_Arrow != ((Manager)obj).Cursor_Arrow)
                _Equals = false;
            if (Cursor_Help != ((Manager)obj).Cursor_Help)
                _Equals = false;
            if (Cursor_AppLoading != ((Manager)obj).Cursor_AppLoading)
                _Equals = false;
            if (Cursor_Busy != ((Manager)obj).Cursor_Busy)
                _Equals = false;
            if (Cursor_Move != ((Manager)obj).Cursor_Move)
                _Equals = false;
            if (Cursor_NS != ((Manager)obj).Cursor_NS)
                _Equals = false;
            if (Cursor_EW != ((Manager)obj).Cursor_EW)
                _Equals = false;
            if (Cursor_NESW != ((Manager)obj).Cursor_NESW)
                _Equals = false;
            if (Cursor_NWSE != ((Manager)obj).Cursor_NWSE)
                _Equals = false;
            if (Cursor_Up != ((Manager)obj).Cursor_Up)
                _Equals = false;
            if (Cursor_Pen != ((Manager)obj).Cursor_Pen)
                _Equals = false;
            if (Cursor_None != ((Manager)obj).Cursor_None)
                _Equals = false;
            if (Cursor_Link != ((Manager)obj).Cursor_Link)
                _Equals = false;
            if (Cursor_Pin != ((Manager)obj).Cursor_Pin)
                _Equals = false;
            if (Cursor_Person != ((Manager)obj).Cursor_Person)
                _Equals = false;
            if (Cursor_IBeam != ((Manager)obj).Cursor_IBeam)
                _Equals = false;
            if (Cursor_Cross != ((Manager)obj).Cursor_Cross)
                _Equals = false;

            if (CommandPrompt != ((Manager)obj).CommandPrompt)
                _Equals = false;
            if (PowerShellx86 != ((Manager)obj).PowerShellx86)
                _Equals = false;
            if (PowerShellx64 != ((Manager)obj).PowerShellx64)
                _Equals = false;
            // If Terminal <> DirectCast(obj, Manager).Terminal Then _Equals = False
            // If TerminalPreview <> DirectCast(obj, Manager).TerminalPreview Then _Equals = False

            return _Equals;
        }

        /// <summary>
        /// Checks if two theme managers are equal
        /// </summary>
        /// <param name="TM1">Theme manager #1</param>
        /// <param name="TM2">Theme manager #2</param>
        /// <returns></returns>
        public static bool operator ==(Manager TM1, Manager TM2) => (bool)TM1.Equals(TM2);

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