using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using WinPaletter.Properties;

namespace WinPaletter.UI.WP
{
    [Description("Banner for WinPaletter UI")]
    public class Banner : Control
    {
        private GraphicsExtensions.RoundedCorners corners = GraphicsExtensions.RoundedCorners.All;

        public Banner()
        {
            TabStop = false;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Size = new(200, 40);
            BackColor = Color.Transparent;
        }

        private static readonly TextureBrush Noise = new(Resources.Noise.Fade(0.6f));

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
                    Invalidate();
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
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //Leave it empty to make control background transparent
            base.OnPaintBackground(pevent);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateRegion();
        }

        protected override void OnDockChanged(EventArgs e)
        {
            base.OnDockChanged(e);
            UpdateRegion();
        }

        private void UpdateRegion()
        {
            if (IsDisposed) return;

            Rectangle rect = new(0, 0, Width, Height);

            Region?.Dispose();

            if (DesignMode)
            {
                Region = new Region(rect);
                return;
            }

            corners = this.UndockedCorners();

            using (GraphicsPath path = rect.Round(corners: corners)) Region = new(path);
        }

        public new string Text
        {
            get => base.Text;
            set
            {
                base.Text = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            RectangleF Rect = new(0, 0, Width - 1, Height - 1);
            RectangleF Rect_Fix = new(-2, -2, Width + 4, Height + 4);

            RectangleF ImageRect = Rect;

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;
            Color _color = Enabled && ImageColor != Color.Empty ? ImageColor : scheme.Colors.Back_Checked;
            Color _color_line;

            if (_color != Color.Empty)
            {
                if (Program.Style.DarkMode) _color = _color.Dark(0.2f); else _color = _color.CB(0.5f);
            }

            using (Config.Colors_Collection colors = new(_color, _color, Program.Style.DarkMode))
            {
                _color_line = colors.Line_Hover(parentLevel);
            }

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            using (LinearGradientBrush lgb0 = new(Rect_Fix, scheme.Colors.Back(parentLevel), _color, LinearGradientMode.Horizontal))
            using (LinearGradientBrush lgb1 = new(Rect_Fix, scheme.Colors.Line_Hover(parentLevel), _color_line, LinearGradientMode.Horizontal))
            using (Pen P = new(lgb1))
            {
                G.FillRoundedRect(lgb0, Rect, corners: corners);
                G.FillRoundedRect(Noise, Rect, corners: corners);
                G.DrawRoundedRect(P, Rect, corners: corners);
            }

            using (SolidBrush B = new(ForeColor))
            using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat())
            using (Font f = new(Fonts.Title, Font.Size, Font.Style))
            {
                const float padding = 10f;
                const float spacing = 10f;

                float textLeft = Rect.X + padding;
                float textWidth = Rect.Width - padding * 2f;

                if (Image != null)
                {
                    float imageY = Rect.Y + (Rect.Height - _image.Height) / 2f;

                    ImageRect = new RectangleF(Rect.X + padding, imageY, _image.Width, _image.Height);

                    G.DrawImage(Image, ImageRect);

                    textLeft = ImageRect.Right + spacing;
                    textWidth = Rect.Right - padding - textLeft;
                }

                RectangleF textRect = new(textLeft, Rect.Y, Math.Max(0, textWidth), Rect.Height);

                G.DrawString(Text, f, B, textRect, sf);
            }
        }
    }
}
