using System.Drawing.Text;

namespace WinPaletter.UI.Style
{
    public partial class Config
    {
        /// <summary>
        /// Used to make custom controls follow Manager's font smoothing
        /// </summary>
        public static TextRenderingHint RenderingHint = TextRenderingHint.SystemDefault;

        public Colors_Structure Colors;

        public Colors_Structure Disabled_Colors = new();

        public bool DarkMode = true;
    }
}