using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    public class ToolTip : System.Windows.Forms.ToolTip
    {
        public ToolTip()
        {
            this.OwnerDraw = true;
            this.Draw += Tooltip_Draw;
            this.Popup += Tooltip_Popup;

            Font = !Fonts.Exists("Segoe UI") ? new Font("Tahoma", _font.Size == 9f ? 8.25f : _font.Size, _font.Style) : _font;
        }

        #region Variables

        int padding_inner = 2;
        Color imageColor;

        Rectangle rectImageSide = Rectangle.Empty;
        Rectangle rectTitle = Rectangle.Empty;
        Rectangle rectText = Rectangle.Empty;

        #endregion

        #region Properties

        public Size Size
        {
            get
            {
                Rectangle rectangle;

                SizeF sizeTitle = ToolTipTitle.Measure(_font_Title);
                rectTitle = new Rectangle(padding_inner, padding_inner, (int)sizeTitle.Width, (int)sizeTitle.Height);

                SizeF sizeText = ToolTipText.Measure(_font);
                rectText = new Rectangle(padding_inner, padding_inner, (int)sizeText.Width, (int)sizeText.Height);

                if (!string.IsNullOrWhiteSpace(ToolTipTitle) && !string.IsNullOrWhiteSpace(ToolTipText))
                {
                    rectText.Y = rectTitle.Bottom;
                }

                rectangle = Rectangle.Union(rectTitle, rectText);

                if (_image != null)
                {
                    rectImageSide = new Rectangle(padding_inner, padding_inner, _image.Width + padding_inner * 2, _image.Height + padding_inner * 2);
                    rectangle.X = rectImageSide.Right + padding_inner * 3;
                    rectTitle.X = rectangle.X;
                    rectText.X = rectangle.X;

                    rectangle.Height = Math.Max(rectangle.Height, rectImageSide.Height);
                    rectangle = Rectangle.Union(rectangle, rectImageSide);

                    rectangle.Width += padding_inner;   //Right padding
                }
                else if (_badgeColor != Color.Empty)
                {
                    rectImageSide = new Rectangle(padding_inner, padding_inner, 24 + padding_inner * 2, 24 + padding_inner * 2);
                    rectangle.X = rectImageSide.Right + padding_inner * 3;
                    rectTitle.X = rectangle.X;
                    rectText.X = rectangle.X;

                    rectangle.Height = Math.Max(rectangle.Height, rectImageSide.Height);
                    rectangle = Rectangle.Union(rectangle, rectImageSide);

                    rectangle.Width += padding_inner;   //Right padding
                }

                rectangle.Height += padding_inner * 2;  //Bottom padding

                if (!string.IsNullOrWhiteSpace(ToolTipTitle) && string.IsNullOrWhiteSpace(ToolTipText))
                {
                    rectTitle.Y = 0;
                    rectTitle.Height = rectangle.Height;
                }
                else if (!string.IsNullOrWhiteSpace(ToolTipText) && string.IsNullOrWhiteSpace(ToolTipTitle))
                {
                    rectText.Y = 0;
                    rectText.Height = rectangle.Height;
                }

                return rectangle.Size;
            }
        }

        private Image _image = null;
        public Image Image
        {
            get => _image;
            set
            {
                if (value != _image)
                {
                    _image = value;
                    if (value != null) imageColor = value.AverageColor();
                    this.ToolTipIcon = ToolTipIcon.Info;
                }
            }
        }


        private Color _badgeColor = Color.Empty;
        public Color BadgeColor
        {
            get => _badgeColor;
            set
            {
                if (value != _badgeColor)
                {
                    _badgeColor = value;
                }
            }
        }


        private Font _font = new("Segoe UI", 9f);
        public Font Font
        {
            get => _font;
            set
            {
                if (value != _font)
                {
                    _font = value;
                }
            }
        }


        private Font _font_Title = new("Segoe UI", 9f, FontStyle.Bold);
        public Font Font_Title
        {
            get => _font_Title;
            set
            {
                if (value != _font_Title)
                {
                    _font_Title = value;
                }
            }
        }


        private string _toolTipText = string.Empty;
        public string ToolTipText
        {
            get => _toolTipText;
            set
            {
                if (value != _toolTipText)
                {
                    _toolTipText = value;
                }
            }
        }

        #endregion

        #region Methods

        public new void Show(Control control, string title, string text, Image image, Point location_to_control, int duration = 5000)
        {
            ToolTipTitle = title;
            ToolTipText = text;
            Image = image;
            BadgeColor = Color.Empty;

            base.Show(".", control, location_to_control.X, location_to_control.Y, duration);
        }

        public new void Show(Control control, string title, string text, Color badgeColor, Point location_to_control, int duration = 5000)
        {
            ToolTipTitle = title;
            ToolTipText = text;
            Image = null;
            BadgeColor = badgeColor;

            base.Show(".", control, location_to_control.X, location_to_control.Y, duration);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            _font_Title?.Dispose();
            _image?.Dispose();
        }

        private void Tooltip_Popup(object sender, PopupEventArgs e)
        {
            e.ToolTipSize = Size;
        }

        private void Tooltip_Draw(object sender, DrawToolTipEventArgs e)
        {
            Graphics G = e.Graphics;
            G.TextRenderingHint = Program.Style.RenderingHint;

            Config.Scheme scheme;

            Color bordersPen;

            if (_image == null)
            {
                scheme = Program.Style.Schemes.Main;
                BackColor = scheme.Colors.Back_Hover();
                ForeColor = Program.Style.DarkMode ? Color.White : Color.Black;
                bordersPen = scheme.Colors.Line_Hover();
            }
            else
            {
                using (Config.Colors_Collection colors = new(imageColor, default, Program.Style.DarkMode))
                {
                    BackColor = colors.Back_Checked;
                    ForeColor = colors.ForeColor_Accent;
                    bordersPen = colors.Line_Checked;
                }
            }

            e.DrawBackground();
            e.DrawBorder();

            using (Pen penBorder = new(bordersPen))
            {
                Rectangle borders = new(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
                G.DrawRoundedRect_LikeW11(penBorder, borders, 1, true);
            }

            if (_image != null)
            {
                Rectangle rectImage = new(rectImageSide.X + (rectImageSide.Width - _image.Width) / 2 + 1, e.Bounds.Y + (e.Bounds.Height - _image.Height) / 2, _image.Width, _image.Height);
                G.DrawImage(_image, rectImage);

                using (Pen P = new(Color.FromArgb(50, ForeColor)))
                {
                    G.DrawLine(P, rectImageSide.Right + padding_inner, e.Bounds.Top + padding_inner * 2, rectImageSide.Right + padding_inner, e.Bounds.Bottom - padding_inner * 2);
                }
            }
            else if (_badgeColor != Color.Empty)
            {
                using (SolidBrush br = new(_badgeColor))
                using (Pen P = new(Color.FromArgb(50, ForeColor)))
                using (Pen P2 = new(_badgeColor))
                {
                    Rectangle rectImage = new(rectImageSide.X + (rectImageSide.Width - 24) / 2 + 1, e.Bounds.Y + (e.Bounds.Height - 24) / 2, 24, 24);
                    G.FillRoundedRect(br, rectImage, 1, true);
                    G.DrawRoundedRect_LikeW11(P2, rectImage, 1, true);
                    G.DrawLine(P, rectImageSide.Right + padding_inner + 2, e.Bounds.Top + padding_inner * 2, rectImageSide.Right + padding_inner + 2, e.Bounds.Bottom - padding_inner * 2);
                }
            }

            // Draw ToolTipTitle if not null or white spaces
            if (!string.IsNullOrWhiteSpace(ToolTipTitle) && !string.IsNullOrWhiteSpace(ToolTipText) && !rectTitle.IsEmpty && !rectText.IsEmpty)
            {
                TextRenderer.DrawText(G, ToolTipTitle, _font_Title, rectTitle, ForeColor, Color.Transparent, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
                TextRenderer.DrawText(G, ToolTipText, _font, rectText, ForeColor, Color.Transparent, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
            }
            else if (string.IsNullOrWhiteSpace(ToolTipText))
            {
                TextRenderer.DrawText(G, ToolTipTitle, _font_Title, rectTitle, ForeColor, Color.Transparent, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
            }
            else if (string.IsNullOrWhiteSpace(ToolTipTitle))
            {
                TextRenderer.DrawText(G, ToolTipText, _font, rectText, ForeColor, Color.Transparent, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
            }
        }
    }
}
