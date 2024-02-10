using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.GlobalVariables
{
    public static class Fonts
    {
        /// <summary>
        /// Monospaced font for console-like text
        /// </summary>
        public static Font Console = new("Lucida Console", 7.5f);

        /// <summary>
        /// Monospaced font for console-like text (medium)
        /// </summary>
        public static Font ConsoleMedium = new("Lucida Console", 9f);

        /// <summary>
        /// Monospaced font for console-like text (large)
        /// </summary>
        public static Font ConsoleLarge = new("Lucida Console", 10f);

        /// <summary>
        /// Main font for application titles
        /// </summary>
        public static FontFamily Title = Exists("Segoe UI Variable Display") ? new ("Segoe UI Variable Display") : Exists("Segoe UI") ? new("Segoe UI") : new("Trebuchet MS");

        /// <summary>
        /// Checks if a font is installed or not (from its name)
        /// </summary>
        public static bool Exists(string fontName)
        {
            bool installed = Exists(fontName, FontStyle.Regular);

            if (!installed)
            {
                installed = Exists(fontName, FontStyle.Bold);
            }

            if (!installed)
            {
                installed = Exists(fontName, FontStyle.Italic);
            }

            return installed;
        }

        /// <summary>
        /// Checks if a font is installed or not (from its name and style)
        /// </summary>
        public static bool Exists(string fontName, FontStyle style)
        {
            bool installed = false;
            const float emSize = 8.0f;

            try
            {

                using (Font testFont = new(fontName, emSize, style))
                {
                    installed = 0 == string.Compare(fontName, testFont.Name, StringComparison.InvariantCultureIgnoreCase);
                }
            }
            catch
            {
            }

            return installed;
        }

    }
}