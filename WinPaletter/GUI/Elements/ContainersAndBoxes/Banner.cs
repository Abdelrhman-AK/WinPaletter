using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

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

        int parentLevel = 0;
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            parentLevel = this.Level();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Noise?.Dispose();
        }

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
            Rectangle Rect_Fix = new(-2, -2, Width + 4, Height + 4);

            Rectangle ImageRect = Rect;

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;
            Color _color = Enabled && ImageColor != Color.Empty ? ImageColor : scheme.Colors.Back_Checked;
            Color _color_line;

            if (_color != Color.Empty)
            {
                if (Program.Style.DarkMode) _color = _color.Dark(0.2f); else _color = _color.CB(0.5f);
            }

            using (Style.Config.Colors_Collection colors = new(_color, _color, Program.Style.DarkMode))
            {
                _color_line = colors.Line_Hover(parentLevel);
            }

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            using (LinearGradientBrush lgb0 = new(Rect_Fix, _color, scheme.Colors.Back(parentLevel), LinearGradientMode.Horizontal))
            using (LinearGradientBrush lgb1 = new(Rect_Fix, _color_line, scheme.Colors.Line_Hover(parentLevel), LinearGradientMode.Horizontal))
            using (Pen P = new(lgb1))
            {
                G.FillRoundedRect(lgb0, Rect);
                G.DrawRoundedRect(P, Rect);
            }

            //Draw noise
            G.FillRoundedRect(Noise, Rect);

            using (SolidBrush B = new(ForeColor))
            using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat())
            using (Font f = new(Fonts.Title, Font.Size, Font.Style))
            {
                //Draw image
                if (Image != null)
                {
                    ImageRect = new(Rect.X + 10, Rect.Y + (Rect.Height - _image.Height) / 2, _image.Width, _image.Height);
                    G.DrawImage(Image, ImageRect);
                    G.DrawString(Text, f, B, new Rectangle(ImageRect.Right + 10, 0, Width, Height), sf);
                }
                else
                {
                    G.DrawString(Text, f, B, new Rectangle(Rect.X + 10, Rect.Y, Rect.Width - 20, Rect.Height), sf);
                }
            }
        }
    }
}
