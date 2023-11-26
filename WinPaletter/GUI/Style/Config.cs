using System.Drawing;
using System.Drawing.Text;

namespace WinPaletter.UI.Style
{
    public partial class Config
    {
        public Config(Color Accent, Color Secondary, Color Tertiary, Color Disabled, Color BackColor, Color DisabledBackColor, bool Dark, bool Rounded)
        {
            DarkMode = Dark;
            RoundedCorners = Rounded;
            Radius = 5;

            Schemes.Main = new(Accent, BackColor, Dark);
            Schemes.Secondary = new(Secondary, BackColor, Dark);
            Schemes.Tertiary = new(Tertiary, BackColor, Dark);
            Schemes.Disabled = new(Disabled, DisabledBackColor, Dark);
        }

        /// <summary>
        /// Used to make custom controls follow Manager's font smoothing
        /// </summary>
        public TextRenderingHint RenderingHint { get; set; } = TextRenderingHint.SystemDefault;

        public bool DarkMode { get; set; } = true;

        public bool RoundedCorners { get; set; } = true;

        public readonly int Radius = 5;

        public Schemes_Collection Schemes = new();
    }
}