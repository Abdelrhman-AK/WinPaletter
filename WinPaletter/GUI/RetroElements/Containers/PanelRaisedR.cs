using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WinPaletter.UI.Retro
{
    /// <summary>
    /// A retro panel with raised border and Windows 9x style
    /// </summary>
    [Description("Raised retro panel with Windows 9x style")]
    public class PanelRaisedR : Panel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PanelRaisedR"/> class.
        /// </summary>
        public PanelRaisedR()
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
        private bool useItAsWin7Taskbar = false;
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
        /// Gets or sets a value indicating whether to use the panel as a Windows 7 taskbar button (simulation).
        /// </summary>
        public bool UseItAsWin7Taskbar
        {
            get => useItAsWin7Taskbar;
            set
            {
                if (useItAsWin7Taskbar != value)
                {
                    useItAsWin7Taskbar = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use the second style.
        /// </summary>
        public bool Style2
        {
            get => style2;
            set
            {
                if (style2 != value)
                {
                    style2 = value;
                    Invalidate();
                }
            }
        }

        private void InvalidateBorders()
        {
            int w = Width;
            int h = Height;

            int thickness = (!Flat && Style2) ? 3 : 2;

            // Top
            Invalidate(new Rectangle(0, 0, w, thickness));

            // Left
            Invalidate(new Rectangle(0, 0, thickness, h));

            // Right
            Invalidate(new Rectangle(w - thickness, 0, thickness, h));

            // Bottom
            Invalidate(new Rectangle(0, h - thickness, w, thickness));
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

            Rectangle rect = ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;

            // Background
            using (SolidBrush b = new(BackColor)) G.FillRectangle(b, ClientRectangle);

            // Special mode: Win7 taskbar
            if (UseItAsWin7Taskbar)
            {
                using Pen p = new(ButtonHilight);
                G.DrawLine(p, rect.Left, rect.Top + 1, rect.Right, rect.Top + 1);
                return;
            }

            // Flat
            if (Flat)
            {
                using Pen p = new(ButtonShadow);
                G.DrawRectangle(p, rect);
                return;
            }

            // Style 1
            if (!Style2)
            {
                using Pen hi = new(ButtonHilight);
                using Pen sh = new(ButtonShadow);

                // Top + Left (highlight)
                G.DrawLine(hi, rect.Left, rect.Top, rect.Right - 1, rect.Top);
                G.DrawLine(hi, rect.Left, rect.Top, rect.Left, rect.Bottom - 1);

                // Bottom + Right (shadow)
                G.DrawLine(sh, rect.Right, rect.Top, rect.Right, rect.Bottom);
                G.DrawLine(sh, rect.Left, rect.Bottom, rect.Right, rect.Bottom);
            }
            else
            {
                // Style 2
                using Pen dk = new(ButtonDkShadow);
                using Pen sh = new(ButtonShadow);
                using Pen lt = new(ButtonLight);
                using Pen hi = new(ButtonHilight);

                // Outer shadow (bottom-right)
                G.DrawLine(dk, rect.Right, rect.Top, rect.Right, rect.Bottom);
                G.DrawLine(dk, rect.Left, rect.Bottom, rect.Right, rect.Bottom);

                // Inner shadow
                G.DrawLine(sh, rect.Right - 1, rect.Top, rect.Right - 1, rect.Bottom - 1);
                G.DrawLine(sh, rect.Left, rect.Bottom - 1, rect.Right - 1, rect.Bottom - 1);

                // Outer highlight (top-left)
                G.DrawLine(lt, rect.Left, rect.Top, rect.Right - 1, rect.Top);
                G.DrawLine(lt, rect.Left, rect.Top, rect.Left, rect.Bottom - 1);

                // Inner highlight
                G.DrawLine(hi, rect.Left + 1, rect.Top + 1, rect.Right - 2, rect.Top + 1);
                G.DrawLine(hi, rect.Left + 1, rect.Top + 1, rect.Left + 1, rect.Bottom - 2);
            }
        }
    }
}