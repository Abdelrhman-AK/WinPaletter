using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.Retro
{

    [Description("Retro panel with Windows 9x style")]
    public class PanelR : Panel
    {

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

        public bool Flat
        {
            get { return flat; }
            set
            {
                if (flat != value)
                {
                    flat = value;
                    Refresh();
                }
            }
        }

        public Color ButtonHilight
        {
            get { return buttonHilight; }
            set
            {
                if (buttonHilight != value)
                {
                    buttonHilight = value;
                    Refresh();
                }
            }
        }

        public Color ButtonShadow
        {
            get { return buttonShadow; }
            set
            {
                if (buttonShadow != value)
                {
                    buttonShadow = value;
                    Refresh();
                }
            }
        }

        public Color ButtonDkShadow
        {
            get { return buttonDkShadow; }
            set
            {
                if (buttonDkShadow != value)
                {
                    buttonDkShadow = value;
                    Refresh();
                }
            }
        }

        public Color ButtonLight
        {
            get { return buttonLight; }
            set
            {
                if (buttonLight != value)
                {
                    buttonLight = value;
                    Refresh();
                }
            }
        }

        public bool Style2
        {
            get { return style2; }
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

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighSpeed;
            G.TextRenderingHint = Program.Style.RenderingHint;
            Rectangle Rect = new(0, 0, Width - 1, Height - 1);

            G.Clear(BackColor);

            if (!Flat)
            {
                if (!Style2)
                {
                    G.DrawLine(new Pen(ButtonShadow), new Point(Rect.X, Rect.Y), new Point(Rect.Width - 1, Rect.Y));
                    G.DrawLine(new Pen(ButtonShadow), new Point(Rect.X, Rect.Y), new Point(Rect.X, Rect.Height - 1));
                    G.DrawLine(new Pen(ButtonHilight), new Point(Rect.Width, Rect.X), new Point(Rect.Width, Rect.Height));
                    G.DrawLine(new Pen(ButtonHilight), new Point(Rect.X, Rect.Height), new Point(Rect.Width, Rect.Height));
                }
                else
                {
                    G.DrawLine(new Pen(ButtonShadow), new Point(Rect.X, Rect.Y), new Point(Rect.Width, Rect.Y));
                    G.DrawLine(new Pen(ButtonShadow), new Point(Rect.X, Rect.Y), new Point(Rect.X, Rect.Height));
                    G.DrawLine(new Pen(ButtonDkShadow), new Point(Rect.X + 1, Rect.Y + 1), new Point(Rect.Width - 1, Rect.Y + 1));
                    G.DrawLine(new Pen(ButtonDkShadow), new Point(Rect.X + 1, Rect.Y + 1), new Point(Rect.X + 1, Rect.Height - 1));
                    G.DrawLine(new Pen(ButtonHilight), new Point(Rect.Width, Rect.Y + 1), new Point(Rect.Width, Rect.Height));
                    G.DrawLine(new Pen(ButtonHilight), new Point(Rect.X + 1, Rect.Height), new Point(Rect.Width, Rect.Height));
                    G.DrawLine(new Pen(ButtonLight), new Point(Rect.Width - 1, Rect.Y + 2), new Point(Rect.Width - 1, Rect.Height - 1));
                    G.DrawLine(new Pen(ButtonLight), new Point(Rect.X + 2, Rect.Height - 1), new Point(Rect.Width - 1, Rect.Height - 1));
                }
            }

            else
            {
                G.DrawRectangle(new Pen(ButtonShadow), Rect);
            }
        }

    }

}