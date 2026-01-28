using Newtonsoft.Json.Linq;
using Serilog.Sinks.File;
using System;
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

        public static event Action DarkModeChanged;
        /// <summary>
        /// Gets or sets the current theme of the application; true for dark mode, false for light mode
        /// </summary>
        public bool DarkMode
        {
            get => _darkMode;
            set
            {
                if (_darkMode != value)
                {
                    _darkMode = value;
                    DarkModeChanged?.Invoke();
                }
            }
        }
        private bool _darkMode;

        /// <summary>
        /// Gets or sets the current theme of the application; true for rounded corners, false for square corners
        /// </summary>

        public bool RoundedCorners { get; set; } = GetRoundedCorners();

        /// <summary>
        /// Gets or sets the radius of the rounded corners if <see cref="RoundedCorners"/> is set to true
        /// </summary>

        public int Radius => 4;

        /// <summary>
        /// Gets or sets the current color schemes of the application
        /// </summary>
        public Schemes_Collection Schemes { get; set; } = new();

        /// <summary>
        /// Gets or sets the current animation state of the application; true for enabled, false for disabled
        /// </summary>
        public bool Animations { get; set; } = false;

        private static Bitmap GetTexture(int val = 1, bool lightMode = false)
        {
            Bitmap bmp;

            switch (val)
            {
                case 0:
                    {
                        using (Bitmap x = new(1, 1)) { bmp = (Bitmap)x.Clone(); }
                        break;
                    }

                case 1:
                    {
                        bmp = Assets.Store.Pattern1;
                        break;
                    }
                case 2:
                    {
                        bmp = Assets.Store.Pattern2;
                        break;
                    }
                case 3:
                    {
                        bmp = Assets.Store.Pattern3;
                        break;
                    }
                case 4:
                    {
                        bmp = Assets.Store.Pattern4;
                        break;
                    }
                case 5:
                    {
                        bmp = Assets.Store.Pattern5;
                        break;
                    }
                case 6:
                    {
                        bmp = Assets.Store.Pattern6;
                        break;
                    }
                case 7:
                    {
                        bmp = Assets.Store.Pattern7;
                        break;
                    }
                case 8:
                    {
                        bmp = Assets.Store.Pattern8;
                        break;
                    }
                case 9:
                    {
                        bmp = Assets.Store.Pattern9;
                        break;
                    }
                case 10:
                    {
                        bmp = Assets.Store.Pattern10;
                        break;
                    }
                case 11:
                    {
                        bmp = Assets.Store.Pattern11;
                        break;
                    }
                case 12:
                    {
                        bmp = Assets.Store.Pattern12;
                        break;
                    }
                case 13:
                    {
                        bmp = Assets.Store.Pattern13;
                        break;
                    }
                case 14:
                    {
                        bmp = Assets.Store.Pattern14;
                        break;
                    }
                case 15:
                    {
                        bmp = Assets.Store.Pattern15;
                        break;
                    }
                case 16:
                    {
                        bmp = Assets.Store.Pattern16;
                        break;
                    }
                case 17:
                    {
                        bmp = Assets.Store.Pattern17;
                        break;
                    }
                default:
                    {
                        bmp = null;
                        break;
                    }

            }

            if (lightMode)
            {
                using (Bitmap bmpContrast = bmp?.Contrast(0.4f))
                using (Bitmap bmpInvert = bmpContrast?.Invert())
                {
                    bmp = bmpInvert.Fade(0.7f);
                }
            }

            return bmp;
        }

        /// <summary>
        /// Decorative texture bitmap
        /// </summary>
        public Bitmap Texture => texture;
        private static Bitmap texture;

        public static event Action PatternChanged;
        public int Pattern
        {
            get => pattern;
            set
            {
                if (pattern != value)
                {
                    pattern = value;
                    texture?.Dispose();
                    texture = GetTexture(value, !DarkMode);
                    PatternChanged?.Invoke();
                }
            }
        }
        private static int pattern = 1;
    }
}