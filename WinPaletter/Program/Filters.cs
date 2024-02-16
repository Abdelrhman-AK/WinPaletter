namespace WinPaletter
{
    internal partial class Program
    {
        public static class Filters
        {
            public static string All => $"{Lang.Filter_All} (*.*)|*.*";
            public static string WinPaletterTheme => $"{Lang.Filter_WinPaletterTheme} (*.wpth)|*.wpth";
            public static string WinPaletterSettings => $"{Lang.Filter_WinPaletterSettings} (*.wpsf)|*.wpsf";
            public static string JSON => $"{Lang.Filter_JSON} (*.json)|*.json";
            public static string Images => $"{Lang.Filter_Images} (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
            public static string PNG => $"{Lang.Filter_PNG} (*.png)|*.png";
            public static string Themes => $"{Lang.Filter_Themes} (*.theme)|*.theme";
            public static string VisualStyles => $"{Lang.Filter_VisualStyles} (*.msstyles)|*.msstyles";
            public static string VisualStyles_And_Themes => $"{Lang.Filter_SupportedThemes} (*.msstyles, *.theme)|*.msstyles;*.theme|{VisualStyles}|{Themes}";
            public static string Text => $"{Lang.Filter_Text} (*.txt)|*.txt";
            public static string Cursors => $"{Lang.Filter_Cursors} (*.cur, *.ani)|*.cur;*.ani|{Lang.Filter_Cur} (*.cur)|*.cur|{Lang.Filter_Ani} (*.ani)|*.ani";
            public static string Screensavers => $"{Lang.Filter_Screensavers} (*.scr)|*.scr";
            public static string WAV => $"{Lang.Filter_WAV} (*.wav)|*.wav";
            public static string EXE => $"{Lang.Filter_EXE} (*.exe)|*.exe";
            public static string WinImages => $"install.wim|install.wim|install.esd|install.esd|*.wim|*.wim|*.esd|*.esd";
            public static string Palettes =>
                $"{Lang.Filter_Palettes} (*.pal, *.act, *.aco, *.txt, *.gpl, *.bbm, *.lbm, *.pal)|*.pal;*.act;*.aco;*.txt;*.gpl;*.bbm;*.lbm;*.pal|{Lang.Filter_Adobe} (*.act)|*.act|{Lang.Filter_PhotoShop} (*.aco)|*.aco|{Lang.Filter_PaintNet} (*.txt)|*.txt|{Lang.Filter_GIMP} (*.gpl)|*.gpl|{Lang.Filter_DeluxePaint} (*.bbm) [{Lang.ReadOnly.ToLower()}]|*.bbm|{Lang.Filter_DeluxePaint} (*.lbm) [{Lang.ReadOnly.ToLower()}]|*.lbm|{Lang.Filter_JASC}(*.pal)|*.pal|{Lang.Filter_RawRGB} (*.pal)|*.pal|{All}";
        }
    }
}