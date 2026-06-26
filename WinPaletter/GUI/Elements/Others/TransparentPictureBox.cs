using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    [Description("PictureBox that supports transparent background")]
    public class TransparentPictureBox : PictureBox
    {
        public TransparentPictureBox()
        {
            DoubleBuffered = true;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            Graphics G = e.Graphics;

            int index = Parent.Controls.GetChildIndex(this);

            for (int i = Parent.Controls.Count - 1; i >= index + 1; i -= 1)
            {
                Control c = Parent.Controls[i];
                if (c.Bounds.IntersectsWith(Bounds) && c.Visible)
                {
                    Bitmap bmp = new(c.Width, c.Height, G);
                    c.DrawToBitmap(bmp, c.ClientRectangle);
                    G.TranslateTransform(c.Left - Left, c.Top - Top);
                    G.DrawImageUnscaled(bmp, Point.Empty);
                    G.TranslateTransform(Left - c.Left, Top - c.Top);
                    bmp.Dispose();
                }
            }
        }
    }
}