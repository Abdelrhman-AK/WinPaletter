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
            public static string All => $"{Lang.Strings.Extensions.AllFiles} (*.*)|*.*";

            /// <summary>
            /// Gets the filter for WinPaletter theme files.
            /// </summary>
            public static string WinPaletterTheme => $"{Lang.Strings.Extensions.WinPaletterThemeFiles} (*.wpth)|*.wpth";

            /// <summary>
            /// Gets the filter for WinPaletter settings files.
            /// </summary>
            public static string WinPaletterSettings => $"{Lang.Strings.Extensions.WinPaletterSettings} (*.wpsf)|*.wpsf";

            /// <summary>
            /// Gets the filter for JSON files.
            /// </summary>
            public static string JSON => $"{Lang.Strings.Extensions.JSON} (*.json)|*.json";

            /// <summary>
            /// Gets the filter for images.
            /// </summary>
            public static string Images => $"{Lang.Strings.Extensions.Images} (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";

            /// <summary>
            /// Gets the filter for PNG images.
            /// </summary>
            public static string PNG => $"{Lang.Strings.Extensions.PNG} (*.png)|*.png";

            /// <summary>
            /// Gets the filter for Microsoft Windows theme files.
            /// </summary>
            public static string Themes => $"{Lang.Strings.Extensions.Themes} (*.theme)|*.theme";

            /// <summary>
            /// Gets the filter for Microsoft Windows visual styles.
            /// </summary>
            public static string VisualStyles => $"{Lang.Strings.Extensions.VisualStyles} (*.msstyles)|*.msstyles";

            /// <summary>
            /// Gets the filter for Microsoft Windows visual styles and themes.
            /// </summary>
            public static string VisualStyles_And_Themes => $"{Lang.Strings.Extensions.SupportedThemes} (*.msstyles, *.theme)|*.msstyles;*.theme|{VisualStyles}|{Themes}";

            /// <summary>
            /// Gets the filter for text files.
            /// </summary>
            public static string Text => $"{Lang.Strings.Extensions.Text} (*.txt)|*.txt";

            /// <summary>
            /// Gets the filter for cursor files (including animated cursors).
            /// </summary>
            public static string Cursors => $"{Lang.Strings.Extensions.Cursors} (*.cur, *.ani)|*.cur;*.ani|{Lang.Strings.Extensions.Cur} (*.cur)|*.cur|{Lang.Strings.Extensions.Ani} (*.ani)|*.ani";

            /// <summary>
            /// Gets the filter for Screen Savers.
            /// </summary>
            public static string ScreenSavers => $"{Lang.Strings.Extensions.ScreenSavers} (*.scr)|*.scr";

            /// <summary>
            /// Gets the filter for WAV files.
            /// </summary>
            public static string WAV => $"{Lang.Strings.Extensions.WAV} (*.wav)|*.wav";

            /// <summary>
            /// Gets the filter for executable files.
            /// </summary>
            public static string EXE => $"{Lang.Strings.Extensions.EXE} (*.exe)|*.exe";

            /// <summary>
            /// Gets the filter for DLL files.
            /// </summary>
            public static string DLL => $"{Lang.Strings.Extensions.DLL} (*.dll)|*.dll";

            /// <summary>
            /// Gets the filter for Windows Imaging Format (WIM) and Electronic Software Delivery (ESD) files.
            /// </summary>
            public static string WinImages => $"install.wim|install.wim|install.esd|install.esd|*.wim|*.wim|*.esd|*.esd";

            /// <summary>
            /// Gets the filter for palettes.
            /// </summary>
            public static string Palettes =>
                $"{Lang.Strings.Extensions.Palettes} (*.pal, *.act, *.aco, *.txt, *.gpl, *.bbm, *.lbm, *.pal)|*.pal;*.act;*.aco;*.txt;*.gpl;*.bbm;*.lbm;*.pal|{Lang.Strings.Extensions.Palette_Adobe} (*.act)|*.act|{Lang.Strings.Extensions.Palette_PhotoShop} (*.aco)|*.aco|{Lang.Strings.Extensions.Palette_PaintNet} (*.txt)|*.txt|{Lang.Strings.Extensions.Palette_GIMP} (*.gpl)|*.gpl|{Lang.Strings.Extensions.Palette_DeluxePaint} (*.bbm) [{Lang.Strings.General.ReadOnly.ToLower()}]|*.bbm|{Lang.Strings.Extensions.Palette_DeluxePaint} (*.lbm) [{Lang.Strings.General.ReadOnly.ToLower()}]|*.lbm|{Lang.Strings.Extensions.Palette_JASC}(*.pal)|*.pal|{Lang.Strings.Extensions.Palette_RawRGB} (*.pal)|*.pal|{All}";
        }
    }
}