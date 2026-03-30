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
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
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
                    Invalidate();
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
                    InvalidateBorders();
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
                    InvalidateBorders();
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
                    InvalidateBorders();
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
                    InvalidateBorders();
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
                if (style2 == value) return;
                style2 = value;
                Invalidate();
            }
        }

        private void InvalidateBorders()
        {
            int w = Width;
            int h = Height;

            // Top
            Invalidate(new Rectangle(0, 0, w, 2));

            // Left
            Invalidate(new Rectangle(0, 0, 2, h));

            // Right
            Invalidate(new Rectangle(w - 2, 0, 2, h));

            // Bottom
            Invalidate(new Rectangle(0, h - 2, w, 2));
        }

        #endregion

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Prevent flicker
        }

        /// <summary>
        /// Paints the panel.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighSpeed;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            Rectangle rect = ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;

            // Background
            using (SolidBrush b = new(BackColor)) G.FillRectangle(b, ClientRectangle);

            if (Flat)
            {
                using Pen p = new(ButtonShadow);
                G.DrawRectangle(p, rect);
                return;
            }

            if (!Style2)
            {
                using Pen shadow = new(ButtonShadow);
                using Pen hilight = new(ButtonHilight);

                // Top + Left
                G.DrawLine(shadow, rect.Left, rect.Top, rect.Right - 1, rect.Top);
                G.DrawLine(shadow, rect.Left, rect.Top, rect.Left, rect.Bottom - 1);

                // Bottom + Right
                G.DrawLine(hilight, rect.Right, rect.Top, rect.Right, rect.Bottom);
                G.DrawLine(hilight, rect.Left, rect.Bottom, rect.Right, rect.Bottom);
            }
            else
            {
                using Pen shadow = new(ButtonShadow);
                using Pen dk = new(ButtonDkShadow);
                using Pen hi = new(ButtonHilight);
                using Pen light = new(ButtonLight);

                // Outer
                G.DrawLine(shadow, rect.Left, rect.Top, rect.Right, rect.Top);
                G.DrawLine(shadow, rect.Left, rect.Top, rect.Left, rect.Bottom);

                // Inner dark
                G.DrawLine(dk, rect.Left + 1, rect.Top + 1, rect.Right - 1, rect.Top + 1);
                G.DrawLine(dk, rect.Left + 1, rect.Top + 1, rect.Left + 1, rect.Bottom - 1);

                // Outer highlight
                G.DrawLine(hi, rect.Right, rect.Top + 1, rect.Right, rect.Bottom);
                G.DrawLine(hi, rect.Left + 1, rect.Bottom, rect.Right, rect.Bottom);

                // Inner light
                G.DrawLine(light, rect.Right - 1, rect.Top + 2, rect.Right - 1, rect.Bottom - 1);
                G.DrawLine(light, rect.Left + 2, rect.Bottom - 1, rect.Right - 1, rect.Bottom - 1);
            }
        }
    }
}