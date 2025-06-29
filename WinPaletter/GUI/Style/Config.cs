using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WinPaletter.UI.Style
{
    /// <summary>
    /// Config class is used to store the current configuration of the application
    /// </summary>
    public partial class Config
    {
        /// <summary>
        /// Default constructor for WinPaletter theme configuration
        /// </summary>
        /// <param name="Accent"></param>
        /// <param name="Secondary"></param>
        /// <param name="Tertiary"></param>
        /// <param name="Disabled"></param>
        /// <param name="BackColor"></param>
        /// <param name="DisabledBackColor"></param>
        /// <param name="Dark"></param>
        /// <param name="Rounded"></param>
        /// <param name="EnableAnimations"></param>
        public Config(Color Accent, Color Secondary, Color Tertiary, Color Disabled, Color BackColor, Color DisabledBackColor, bool Dark, bool Rounded, bool EnableAnimations)
        {
            DarkMode = Dark;
            RoundedCorners = Rounded;
            Radius = 4;
            Animations = EnableAnimations;

            Schemes.Main = new(Accent, BackColor, Dark);
            Schemes.Secondary = new(Secondary, BackColor, Dark);
            Schemes.Tertiary = new(Tertiary, BackColor, Dark);
            Schemes.Disabled = new(Disabled, DisabledBackColor, Dark);
        }

        /// <summary>
        /// Used to make custom controls follow Theme manager's font smoothing
        /// </summary>
        public TextRenderingHint TextRenderingHint
        {
            get => _renderingHint;
            set
            {
                if (_renderingHint != value)
                {
                    _renderingHint = value;
                    foreach (Form form in Application.OpenForms)
                    {
                        // Update the font smoothing for all controls
                        form.Refresh();
                    }

                }
            }
        }
        private TextRenderingHint _renderingHint = TextRenderingHint.SystemDefault;

        /// <summary>
        /// Gets or sets the current theme of the application; true for dark mode, false for light mode
        /// </summary>
        public bool DarkMode { get; set; } = true;

        /// <summary>
        /// Gets or sets the current theme of the application; true for rounded corners, false for square corners
        /// </summary>

        public bool RoundedCorners { get; set; } = true;

        /// <summary>
        /// Gets or sets the radius of the rounded corners if <see cref="RoundedCorners"/> is set to true
        /// </summary>

        public readonly int Radius = 4;

        /// <summary>
        /// Gets or sets the current color schemes of the application
        /// </summary>
        public Schemes_Collection Schemes = new();

        /// <summary>
        /// Gets or sets the current animation state of the application; true for enabled, false for disabled
        /// </summary>
        public bool Animations = false;
    }
}