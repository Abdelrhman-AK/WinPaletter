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
        public bool Flat { get; set; } = false;
        public Color ButtonHilight { get; set; } = Color.White;
        public Color ButtonShadow { get; set; } = Color.FromArgb(128, 128, 128);
        public Color ButtonDkShadow { get; set; } = Color.FromArgb(105, 105, 105);
        public Color ButtonLight { get; set; } = Color.FromArgb(227, 227, 227);
        public bool Style2 { get; set; } = false;
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighSpeed;
            G.TextRenderingHint = Program.Style.RenderingHint;
            DoubleBuffered = true;
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