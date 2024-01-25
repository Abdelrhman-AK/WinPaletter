using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using WinPaletter.TypesExtensions;

namespace WinPaletter.UI.WP
{
    [Description("Banner for WinPaletter UI")]
    public class Banner : ContainerControl
    {
        public Banner()
        {
            TabStop = false;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Size = new(200, 40);
            BackColor = Color.Transparent;
        }

        private readonly TextureBrush Noise = new(Properties.Resources.Noise.Fade(0.6f));

        private Bitmap _image;
        [Description("Image to display on the banner")]
        public Bitmap Image
        {
            get => _image;
            set
            {
                if (value != _image)
                {
                    _image = value;
                    if (value != null) ImageColor = _image.AverageColor(); else ImageColor = Color.Empty;
                    Refresh();
                }
            }
        }
        private Color ImageColor;


        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //Leave it empty to make control background transparent
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = TextRenderingHint.SystemDefault;

            Rectangle Rect = new(0, 0, Width - 1, Height - 1);
            Rectangle ImageRect = Rect;

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;
            Color _color = Enabled && ImageColor != Color.Empty ? ImageColor : scheme.Colors.Back_Checked;
            Color _color_line;

            using (Style.Config.Colors_Collection colors = new(_color, _color, Program.Style.DarkMode))
            {
                _color_line = colors.Line_Hover;
            }

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            using (LinearGradientBrush lgb0 = new(Rect, _color, scheme.Colors.Back, LinearGradientMode.Horizontal))
            using (LinearGradientBrush lgb1 = new(Rect, _color_line, scheme.Colors.Line_Hover, LinearGradientMode.Horizontal))
            using (Pen P = new(lgb1))
            {
                G.FillRoundedRect(lgb0, Rect);
                G.DrawRoundedRect(P, Rect);
            }

            //Draw noise
            G.FillRoundedRect(Noise, Rect);

            //Draw image
            if (Image != null)
            {
                ImageRect = new(Rect.X + 10, Rect.Y + (Rect.Height - _image.Height) / 2, _image.Width, _image.Height);
                G.DrawImage(Image, ImageRect);
            }

            //Draw text
            using (SolidBrush B = new(ForeColor))
            using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat())
            {
                G.DrawString(Text, Font, B, new Rectangle(ImageRect.Right + 10, 0, Width, Height), sf);
            }
        }
    }
}
