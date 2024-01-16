using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.Controllers
{
    [ToolboxItem(false)]
    public class TextTranslationItem : ContainerControl
    {
        public TextTranslationItem()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }

        #region Properties

        private Image _image;
        public new Image Image
        {
            get => _image;
            set
            {
                if (_image != value)
                {
                    _image = value;
                    Refresh();
                }
            }
        }


        private ContentAlignment _TextAlign = ContentAlignment.MiddleCenter;
        public ContentAlignment TextAlign
        {
            get => _TextAlign;
            set
            {
                if (value != _TextAlign)
                {
                    _TextAlign = value;
                    Invalidate();
                }
            }
        }


        private ContentAlignment _ImageAlign = ContentAlignment.MiddleCenter;
        public ContentAlignment ImageAlign
        {
            get => _ImageAlign;
            set
            {
                if (value != _ImageAlign)
                {
                    _ImageAlign = value;
                    Invalidate();
                }
            }
        }


        private TextImageRelation _TextImageRelation = TextImageRelation.ImageBeforeText;
        public TextImageRelation TextImageRelation
        {
            get => _TextImageRelation;
            set
            {
                if (value != _TextImageRelation)
                {
                    _TextImageRelation = value;
                    Invalidate();
                }
            }
        }


        private string _Text;
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text
        {
            get => _Text;
            set
            {
                if (value != _Text)
                {
                    _Text = value;
                    Invalidate();
                }
            }
        }

        public bool Pressed;
        public string Text_English { get; set; }
        public string Tag_English { get; set; }

        private string _SearchHighlight;
        public string SearchHighlight
        {
            get => _SearchHighlight;
            set
            {
                if (value != _SearchHighlight)
                {
                    _SearchHighlight = value;
                    Refresh();
                }
            }
        }

        #endregion

        #region Events/Overrides

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Pressed = true;
            BringToFront();
            Focus();
            Invalidate();

            base.OnMouseDown(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            Pressed = false;
            Invalidate();

            base.OnLostFocus(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            Pressed = true;
            BringToFront();
            Focus();
            Invalidate();

            base.OnGotFocus(e);
        }

        #endregion

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //Leave it empty to make control background transparent
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this == null) return;

            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = Program.Style.RenderingHint;

            Rectangle rect = new(0, 0, Width - 1, Height - 1);
            Rectangle PaddingRect = Rectangle.FromLTRB(Padding.Left, Padding.Top, Padding.Right, Padding.Bottom);
            Rectangle TextAndImageRect = new(rect.X + PaddingRect.X, rect.Y + PaddingRect.Y, Width - 1 - PaddingRect.Width * 2 - rect.X * 2, Height - 1 - PaddingRect.Height * 2 - rect.Y * 2);

            if (_SearchHighlight is not null && !string.IsNullOrWhiteSpace(_SearchHighlight) && Text.ToLower().Trim().Contains(_SearchHighlight.ToLower().Trim()))
            {
                G.FillRectangle(Program.Style.Schemes.Tertiary.Brushes.Back_Checked_Hover, rect);
            }

            else if (!string.IsNullOrWhiteSpace(Text) && (Text.Trim() ?? string.Empty) == (Text_English.Trim() ?? string.Empty))
            {
                G.FillRectangle(Program.Style.Schemes.Secondary.Brushes.Back_Checked, rect);
            }

            else if (Tag is not null && !string.IsNullOrWhiteSpace(Tag.ToString()) && (Tag.ToString().Trim() ?? string.Empty) == (Tag_English.Trim() ?? string.Empty))
            {
                G.FillRectangle(Program.Style.Schemes.Secondary.Brushes.Back_Checked, rect);
            }

            if (Pressed)
            {
                G.FillRectangle(Program.Style.Schemes.Main.Brushes.Back_Checked, rect);
                using (Pen P = new(Program.Style.DarkMode ? Color.White : Color.Black, 2f) { DashStyle = DashStyle.Dot }) { G.DrawRectangle(P, rect); }
            }
            else
            {
                using (Pen P = new(Color.FromArgb(100, Program.Style.DarkMode ? Color.White : Color.Black), 1f)) { G.DrawRectangle(P, rect); }
            }

            #region Text and Image Render
            using (StringFormat sf = TextAlign.ToStringFormat((int)base.RightToLeft == 1))
            {
                using (SolidBrush fc = new(ForeColor))
                {
                    if (Image == null)
                    {
                        //Fix label maladjustment in center position
                        if (TextAlign == ContentAlignment.MiddleCenter)
                        {
                            if (Width % 2 != 0) TextAndImageRect.Offset(1, 0);
                            if (Height % 2 != 0) TextAndImageRect.Offset(0, 1);
                        }

                        G.DrawString(Text, Font, fc, TextAndImageRect, sf);
                    }

                    else if (string.IsNullOrWhiteSpace(Text) && Image != null)
                    {
                        Rectangle imageRect = GetImageRectangle(TextAndImageRect, Image.Size, ImageAlign);

                        //Fix image maladjustment in center position
                        if (ImageAlign == ContentAlignment.MiddleCenter)
                        {
                            if (Width % Image.Width != 0) imageRect.Offset(1, 0);
                            if (Height % Image.Height != 0) imageRect.Offset(0, 1);
                        }

                        G.DrawImage(Enabled ? _image : _image.Grayscale(), imageRect);
                    }

                    else
                    {
                        if (TextImageRelation == TextImageRelation.Overlay)
                        {
                            Rectangle imageRect = GetImageRectangle(TextAndImageRect, Image.Size, ImageAlign);

                            //Fix image maladjustment in center position
                            if (ImageAlign == ContentAlignment.MiddleCenter)
                            {
                                if (Width % Image.Width != 0) imageRect.Offset(1, 0);
                                if (Height % Image.Height != 0) imageRect.Offset(0, 1);
                            }

                            //Fix label maladjustment in center position
                            if (TextAlign == ContentAlignment.MiddleCenter)
                            {
                                if (Width % 2 != 0) TextAndImageRect.Offset(1, 0);
                                if (Height % 2 != 0) TextAndImageRect.Offset(0, 1);
                            }

                            G.DrawImage(Enabled ? _image : _image.Grayscale(), imageRect);
                            G.DrawString(Text, Font, fc, TextAndImageRect, sf);
                        }
                        else
                        {
                            int innerSpacing = 1;
                            Size ImageSize = Image.Size;
                            SizeF TextSizeF = G.MeasureString(Text, Font, TextAndImageRect.Size, sf);
                            Size TextSize = new((int)TextSizeF.Width + innerSpacing * 2, (int)TextSizeF.Height);

                            Rectangle space = new();
                            Rectangle textRect = new();
                            Rectangle imageRect = new();

                            if (TextImageRelation == TextImageRelation.ImageAboveText)
                            {
                                space.Width = Math.Max(ImageSize.Width, TextSize.Width);
                                space.Height = ImageSize.Height + TextSize.Height + innerSpacing;

                                space = GetImageRectangle(TextAndImageRect, space.Size, ImageAlign);

                                imageRect = GetImageRectangle(space, Image.Size, ImageAlign);
                                imageRect.Y = space.Y;

                                textRect.Width = space.Width;
                                textRect.Height = TextSize.Height;
                                textRect.X = space.X;
                                textRect.Y = imageRect.Bottom + innerSpacing;
                            }

                            else if (TextImageRelation == TextImageRelation.TextAboveImage)
                            {
                                space.Width = Math.Max(ImageSize.Width, TextSize.Width);
                                space.Height = ImageSize.Height + TextSize.Height + innerSpacing;

                                space = GetImageRectangle(TextAndImageRect, space.Size, ImageAlign);

                                textRect.Width = space.Width;
                                textRect.Height = TextSize.Height;
                                textRect.X = space.X;
                                textRect.Y = space.Y;

                                imageRect = GetImageRectangle(space, Image.Size, ImageAlign);
                                imageRect.Y = textRect.Bottom + innerSpacing;
                            }

                            else if (TextImageRelation == TextImageRelation.ImageBeforeText)
                            {
                                space.Width = ImageSize.Width + TextSize.Width + innerSpacing;
                                space.Height = Math.Max(ImageSize.Height, TextSize.Height);

                                space = GetImageRectangle(TextAndImageRect, space.Size, ImageAlign);

                                imageRect = GetImageRectangle(space, ImageSize, ContentAlignment.MiddleLeft);

                                textRect.X = imageRect.Right + innerSpacing;
                                textRect.Y = space.Y + 1;
                                textRect.Width = TextSize.Width;
                                textRect.Height = space.Height;
                            }

                            else if (TextImageRelation == TextImageRelation.TextBeforeImage)
                            {
                                space.Width = ImageSize.Width + TextSize.Width + innerSpacing;
                                space.Height = Math.Max(ImageSize.Height, TextSize.Height);

                                space = GetImageRectangle(TextAndImageRect, space.Size, ImageAlign);

                                textRect.X = space.X;
                                textRect.Y = space.Y;
                                textRect.Width = TextSize.Width;
                                textRect.Height = space.Height;

                                imageRect.X = textRect.Right + innerSpacing;
                                imageRect.Y = space.Y;
                                imageRect.Width = ImageSize.Width;
                                imageRect.Height = ImageSize.Height;
                            }

                            G.DrawImage(Enabled ? _image : _image.Grayscale(), imageRect);
                            G.DrawString(Text, Font, fc, textRect, sf);
                        }
                    }
                }
            }
            #endregion

            base.OnPaint(e);
        }

        private Rectangle GetImageRectangle(Rectangle rect, Size size, ContentAlignment contentAlignment)
        {
            float CenterWidthD = rect.X + (float)(rect.Width - size.Width) / 2;
            float CenterHeightD = rect.Y + (float)(rect.Height - size.Height) / 2;

            int CenterWidth = (int)Math.Round(CenterWidthD, 2);
            int CenterHeight = (int)Math.Round(CenterHeightD, 2);

            switch (contentAlignment)
            {
                case ContentAlignment.TopLeft:
                    return new(rect.X, rect.Y, size.Width, size.Height);

                case ContentAlignment.TopRight:
                    return new(rect.Right - size.Width, rect.Y, size.Width, size.Height);

                case ContentAlignment.TopCenter:
                    return new(CenterWidth, rect.Y, size.Width, size.Height);

                case ContentAlignment.MiddleLeft:
                    return new(rect.X, CenterHeight, size.Width, size.Height);

                case ContentAlignment.MiddleCenter:
                    return new(CenterWidth, CenterHeight, size.Width, size.Height);

                case ContentAlignment.MiddleRight:
                    return new(rect.Right - size.Width, CenterHeight, size.Width, size.Height);

                case ContentAlignment.BottomLeft:
                    return new(rect.X, rect.Bottom - size.Height, size.Width, size.Height);

                case ContentAlignment.BottomCenter:
                    return new(CenterWidth, rect.Bottom - size.Height, size.Width, size.Height);

                case ContentAlignment.BottomRight:
                    return new(rect.Right - size.Width, rect.Bottom - size.Height, size.Width, size.Height);

                default:
                    return new(CenterWidth, CenterHeight, size.Width, size.Height);
            }
        }

    }
}