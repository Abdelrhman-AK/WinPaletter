using System.Drawing.Text;

namespace WinPaletter.UI.Style
{
    public partial class Config
    {
        /// <summary>
        /// Used to make custom controls follow Manager's font smoothing
        /// </summary>
        public static TextRenderingHint RenderingHint { get; set; } = TextRenderingHint.SystemDefault;

        public Colors_Structure Colors = new();

        public Colors_Structure Disabled_Colors = new();

        public bool DarkMode { get; set; } = true;

        public bool RoundedCorners { get; set; } = true;

        public readonly int Radius = 5;
    }
}