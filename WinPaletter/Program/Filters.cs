namespace WinPaletter
{
    internal partial class Program
    {
        /// <summary>
        /// Contains the filters for the file dialog.
        /// </summary>
        public static class Filters
        {
            /// <summary>
            /// Gets the filter for all files.
            /// </summary>
            public static string All => $"{Localization.Strings.Extensions.AllFiles} (*.*)|*.*";

            /// <summary>
            /// Gets the filter for WinPaletter theme files.
            /// </summary>
            public static string WinPaletterTheme => $"{Localization.Strings.Extensions.WinPaletterThemeFiles} (*.wpth)|*.wpth";

            /// <summary>
            /// Gets the filter for WinPaletter settings files.
            /// </summary>
            public static string WinPaletterSettings => $"{Localization.Strings.Extensions.WinPaletterSettings} (*.wpsf)|*.wpsf";

            /// <summary>
            /// Gets the filter for JSON files.
            /// </summary>
            public static string JSON => $"{Localization.Strings.Extensions.JSON} (*.json)|*.json";

            /// <summary>
            /// Gets the filter for images.
            /// </summary>
            public static string Images => $"{Localization.Strings.Extensions.Images} (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";

            /// <summary>
            /// Gets the filter for PNG images.
            /// </summary>
            public static string PNG => $"{Localization.Strings.Extensions.PNG} (*.png)|*.png";

            /// <summary>
            /// Gets the filter for Microsoft Windows theme files.
            /// </summary>
            public static string Themes => $"{Localization.Strings.Extensions.Themes} (*.theme)|*.theme";

            /// <summary>
            /// Gets the filter for Microsoft Windows visual styles.
            /// </summary>
            public static string VisualStyles => $"{Localization.Strings.Extensions.VisualStyles} (*.msstyles)|*.msstyles";

            /// <summary>
            /// Gets the filter for Microsoft Windows visual styles and themes.
            /// </summary>
            public static string VisualStyles_And_Themes => $"{Localization.Strings.Extensions.SupportedThemes} (*.msstyles, *.theme)|*.msstyles;*.theme|{VisualStyles}|{Themes}";

            /// <summary>
            /// Gets the filter for text files.
            /// </summary>
            public static string Text => $"{Localization.Strings.Extensions.Text} (*.txt)|*.txt";

            /// <summary>
            /// Gets the filter for cursor files (including animated cursors).
            /// </summary>
            public static string Cursors => $"{Localization.Strings.Extensions.Cursors} (*.cur, *.ani)|*.cur;*.ani|{Localization.Strings.Extensions.Cur} (*.cur)|*.cur|{Localization.Strings.Extensions.Ani} (*.ani)|*.ani";

            /// <summary>
            /// Gets the filter for Screen Savers.
            /// </summary>
            public static string ScreenSavers => $"{Localization.Strings.Extensions.ScreenSavers} (*.scr)|*.scr";

            /// <summary>
            /// Gets the filter for WAV files.
            /// </summary>
            public static string WAV => $"{Localization.Strings.Extensions.WAV} (*.wav)|*.wav";

            /// <summary>
            /// Gets the filter for executable files.
            /// </summary>
            public static string EXE => $"{Localization.Strings.Extensions.EXE} (*.exe)|*.exe";

            /// <summary>
            /// Gets the filter for DLL files.
            /// </summary>
            public static string DLL => $"{Localization.Strings.Extensions.DLL} (*.dll)|*.dll";

            /// <summary>
            /// Gets the filter for Windows Imaging Format (WIM) and Electronic Software Delivery (ESD) files.
            /// </summary>
            public static string WinImages => $"install.wim|install.wim|install.esd|install.esd|*.wim|*.wim|*.esd|*.esd";

            /// <summary>
            /// Gets the filter for palettes.
            /// </summary>
            public static string Palettes =>
                $"{Localization.Strings.Extensions.Palettes} (*.pal, *.act, *.aco, *.txt, *.gpl, *.bbm, *.lbm, *.pal)|*.pal;*.act;*.aco;*.txt;*.gpl;*.bbm;*.lbm;*.pal|{Localization.Strings.Extensions.Palette_Adobe} (*.act)|*.act|{Localization.Strings.Extensions.Palette_PhotoShop} (*.aco)|*.aco|{Localization.Strings.Extensions.Palette_PaintNet} (*.txt)|*.txt|{Localization.Strings.Extensions.Palette_GIMP} (*.gpl)|*.gpl|{Localization.Strings.Extensions.Palette_DeluxePaint} (*.bbm) [{Localization.Strings.General.ReadOnly.ToLower()}]|*.bbm|{Localization.Strings.Extensions.Palette_DeluxePaint} (*.lbm) [{Localization.Strings.General.ReadOnly.ToLower()}]|*.lbm|{Localization.Strings.Extensions.Palette_JASC}(*.pal)|*.pal|{Localization.Strings.Extensions.Palette_RawRGB} (*.pal)|*.pal|{All}";
        }
    }
}