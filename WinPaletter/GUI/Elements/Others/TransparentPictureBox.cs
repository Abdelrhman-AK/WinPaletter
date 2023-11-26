using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{

    [Description("PictureBox that supports transparent background")]
    public class TransparentPictureBox : PictureBox
    {
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            DoubleBuffered = true;

            if (Parent is not null)
            {
                int index = Parent.Controls.GetChildIndex(this);

                for (int i = Parent.Controls.Count - 1; i >= index + 1; i -= 1)
                {
                    var c = Parent.Controls[i];
                    if (c.Bounds.IntersectsWith(Bounds) && c.Visible)
                    {
                        var bmp = new Bitmap(c.Width, c.Height, e.Graphics);
                        c.DrawToBitmap(bmp, c.ClientRectangle);
                        e.Graphics.TranslateTransform(c.Left - Left, c.Top - Top);
                        e.Graphics.DrawImageUnscaled(bmp, Point.Empty);
                        e.Graphics.TranslateTransform(Left - c.Left, Top - c.Top);
                        bmp.Dispose();
                    }
                }
            }
        }
    }

}