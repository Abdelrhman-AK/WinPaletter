using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WinPaletter.UI.Retro
{
    /// <summary>
    /// A retro panel with Windows 9x style
    /// </summary>
    [Description("Retro panel with Windows 9x style")]
    public class PanelR : Panel
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PanelR"/> class.
        /// </summary>
        public PanelR()
        {
            DoubleBuffered = true;
            Font = new("Microsoft Sans Serif", 8f);
            BackColor = Color.FromArgb(192, 192, 192);
            ForeColor = Color.Black;
            BorderStyle = BorderStyle.None;
        }

        #region Properties

        private bool flat = false;
        private Color buttonHilight = SystemColors.ButtonHighlight;
        private Color buttonShadow = SystemColors.ButtonShadow;
        private Color buttonDkShadow = SystemColors.ControlDark;
        private Color buttonLight = SystemColors.ControlLight;
        private bool style2 = false;

        /// <summary>
        /// Gets or sets a value indicating whether the panel is flat or not.
        /// </summary>
        public bool Flat
        {
            get => flat;
            set
            {
                if (flat != value)
                {
                    flat = value;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the button hilight color.
        /// </summary>
        public Color ButtonHilight
        {
            get => buttonHilight;
            set
            {
                if (buttonHilight != value)
                {
                    buttonHilight = value;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the button shadow color.
        /// </summary>
        public Color ButtonShadow
        {
            get => buttonShadow;
            set
            {
                if (buttonShadow != value)
                {
                    buttonShadow = value;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the button dark shadow color.
        /// </summary>
        public Color ButtonDkShadow
        {
            get => buttonDkShadow;
            set
            {
                if (buttonDkShadow != value)
                {
                    buttonDkShadow = value;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the button light color.
        /// </summary>
        public Color ButtonLight
        {
            get => buttonLight;
            set
            {
                if (buttonLight != value)
                {
                    buttonLight = value;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the panel is styled as the second style.
        /// </summary>
        public bool Style2
        {
            get => style2;
            set
            {
                if (style2 != value)
                {
                    style2 = value;
                    Refresh();
                }
            }
        }

        #endregion

        /// <summary>
        /// Paints the panel.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighSpeed;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;
            Rectangle Rect = new(0, 0, Width - 1, Height - 1);

            // Draw background
            G.Clear(BackColor);

            // Draw border
            if (!Flat)
            {
                if (!Style2)
                {
                    // Draw 3D borders with style 1

                    using (Pen P0 = new(ButtonShadow))
                    using (Pen P1 = new(ButtonHilight))
                    {
                        G.DrawLine(P0, new Point(Rect.X, Rect.Y), new Point(Rect.Width - 1, Rect.Y));
                        G.DrawLine(P0, new Point(Rect.X, Rect.Y), new Point(Rect.X, Rect.Height - 1));
                        G.DrawLine(P1, new Point(Rect.Width, Rect.X), new Point(Rect.Width, Rect.Height));
                        G.DrawLine(P1, new Point(Rect.X, Rect.Height), new Point(Rect.Width, Rect.Height));
                    }
                }
                else
                {
                    // Draw 3D borders with style 2

                    using (Pen P0 = new(ButtonShadow))
                    using (Pen P1 = new(ButtonDkShadow))
                    using (Pen P2 = new(ButtonHilight))
                    using (Pen P3 = new(ButtonLight))
                    {
                        G.DrawLine(P0, new Point(Rect.X, Rect.Y), new Point(Rect.Width, Rect.Y));
                        G.DrawLine(P0, new Point(Rect.X, Rect.Y), new Point(Rect.X, Rect.Height));
                        G.DrawLine(P1, new Point(Rect.X + 1, Rect.Y + 1), new Point(Rect.Width - 1, Rect.Y + 1));
                        G.DrawLine(P1, new Point(Rect.X + 1, Rect.Y + 1), new Point(Rect.X + 1, Rect.Height - 1));
                        G.DrawLine(P2, new Point(Rect.Width, Rect.Y + 1), new Point(Rect.Width, Rect.Height));
                        G.DrawLine(P2, new Point(Rect.X + 1, Rect.Height), new Point(Rect.Width, Rect.Height));
                        G.DrawLine(P3, new Point(Rect.Width - 1, Rect.Y + 2), new Point(Rect.Width - 1, Rect.Height - 1));
                        G.DrawLine(P3, new Point(Rect.X + 2, Rect.Height - 1), new Point(Rect.Width - 1, Rect.Height - 1));
                    }
                }
            }
            else
            {
                // Draw a flat border

                using (Pen P = new(ButtonShadow)) G.DrawRectangle(P, Rect);
            }


        }
    }
}