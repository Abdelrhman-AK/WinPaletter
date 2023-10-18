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
            LostFocus += TextTranslationItem_LostFocus;
            GotFocus += TextTranslationItem_GotFocus;
        }

        #region Properties
        public Image Image { get; set; }

        private ContentAlignment _TextAlign = ContentAlignment.MiddleCenter;
        public ContentAlignment TextAlign
        {
            get
            {
                return _TextAlign;
            }
            set
            {
                _TextAlign = value;
                Invalidate();
            }
        }

        private ContentAlignment _ImageAlign = ContentAlignment.MiddleCenter;
        public ContentAlignment ImageAlign
        {
            get
            {
                return _ImageAlign;
            }
            set
            {
                _ImageAlign = value;
                Invalidate();
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
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
                Invalidate();
            }
        }

        public bool Pressed;
        public string Text_English { get; set; }
        public string Tag_English { get; set; }

        private string _SearchHighlight;
        public string SearchHighlight
        {
            get
            {
                return _SearchHighlight;
            }
            set
            {
                _SearchHighlight = value;
                Refresh();
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | 0x20;
                return cp;
            }
        }
        #endregion

        #region Events

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Pressed = true;
            BringToFront();
            Focus();
            Invalidate();
        }

        private void TextTranslationItem_LostFocus(object sender, EventArgs e)
        {
            Pressed = false;
            Invalidate();
        }

        private void TextTranslationItem_GotFocus(object sender, EventArgs e)
        {
            Pressed = true;
            BringToFront();
            Focus();
            Invalidate();
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = Program.RenderingHint;
            DoubleBuffered = true;
            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            var NotTranslatedColor = Program.Style.Colors.NotTranslatedColor;

            if (_SearchHighlight is not null && !string.IsNullOrWhiteSpace(_SearchHighlight) && Text.ToLower().Trim().Contains(_SearchHighlight.ToLower().Trim()))
            {
                using (var br = new SolidBrush(Program.Style.Colors.SearchColor))
                {
                    G.FillRectangle(br, rect);
                }
            }

            else if (!string.IsNullOrWhiteSpace(Text) && (Text.Trim() ?? "") == (Text_English.Trim() ?? ""))
            {
                using (var br = new SolidBrush(NotTranslatedColor))
                {
                    G.FillRectangle(br, rect);
                }
            }

            else if (Tag is not null && !string.IsNullOrWhiteSpace(Tag.ToString()) && (Tag.ToString().Trim() ?? "") == (Tag_English.Trim() ?? ""))
            {
                using (var br = new SolidBrush(NotTranslatedColor))
                {
                    G.FillRectangle(br, rect);
                }

            }

            if (Pressed)
            {
                G.FillRectangle(new SolidBrush(Program.Style.Colors.Back_Checked), rect);
                G.DrawRectangle(new Pen(Program.Style.DarkMode ? Color.White : Color.Black, 2f) { DashStyle = DashStyle.Dot }, rect);
            }
            else
            {
                G.DrawRectangle(new Pen(Color.FromArgb(100, Program.Style.DarkMode ? Color.White : Color.Black), 1f), rect);
            }

            #region Text and Image Render
            using (var ButtonString = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                bool RTL = (int)RightToLeft == 1;
                if (RTL)
                    ButtonString.FormatFlags = StringFormatFlags.DirectionRightToLeft;

                Bitmap img = null;
                if (Image is not null)
                {
                    if (Enabled)
                    {
                        img = (Bitmap)Image.Clone();
                    }
                    else
                    {
                        img = ((Bitmap)Image.Clone()).Grayscale();
                    }
                }

                int imgX = default, imgY = default;

                try
                {
                    if (img is not null)
                        imgX = (int)Math.Round((Width - img.Width) / 2d);
                }
                catch
                {
                }

                try
                {
                    if (img is not null)
                        imgY = (int)Math.Round((Height - img.Height) / 2d);
                }
                catch
                {
                }

                if (img is null)
                {
                    try
                    {
                        using (var br = new SolidBrush(ForeColor))
                        using (var sf = TextAlign.ToStringFormat(RTL))
                        {
                            var r = new Rectangle(1, 0, Width, Height);
                            G.DrawString(Text, Font, br, r, sf);
                        }
                    }
                    catch
                    {
                    }
                }
                else
                {

                    switch (ImageAlign)
                    {
                        case ContentAlignment.MiddleCenter:
                            {
                                ButtonString.Alignment = StringAlignment.Center;
                                ButtonString.LineAlignment = StringAlignment.Near;
                                int alx = (int)Math.Round((Height - (img.Height + 4 + Text.Measure(base.Font).Height)) / 2f);

                                try
                                {
                                    if (img is not null)
                                    {
                                        if (string.IsNullOrEmpty(Text))
                                        {
                                            G.DrawImage((Bitmap)img.Clone(), new Rectangle(imgX, imgY, img.Width, img.Height));
                                        }
                                        else
                                        {
                                            G.DrawImage((Bitmap)img.Clone(), new Rectangle(imgX, alx, img.Width, img.Height));
                                        }
                                    }

                                    using (var br = new SolidBrush(ForeColor))
                                    {
                                        var r = new Rectangle(0, alx + 9 + img.Height, Width, Height);
                                        G.DrawString(Text, Font, br, r, ButtonString);
                                    }
                                }
                                catch
                                {
                                }

                                break;
                            }

                        case ContentAlignment.MiddleLeft:
                            {
                                var Rec = new Rectangle(imgY, imgY, img.Width, img.Height);
                                int Bo = imgY + img.Width + imgY - 5;
                                var RecText = new Rectangle(Bo, imgY, (int)Math.Round(Text.Measure(Font).Width + 15f - imgY), img.Height);
                                var u = Rectangle.Union(Rec, RecText);
                                u.X = (int)Math.Round((Width - u.Width) / 2d);
                                int innerSpace = RecText.Left - Rec.Right;

                                if (!RTL)
                                {
                                    Rec.X = u.Left;
                                    RecText.X = u.Left + Rec.Width + innerSpace;
                                }
                                else
                                {
                                    Rec.X = u.Right - Rec.Width;
                                    RecText.X = u.Right - RecText.Width - Rec.Width - innerSpace;
                                }

                                G.DrawImage((Bitmap)img.Clone(), Rec);
                                using (var br = new SolidBrush(ForeColor))
                                {
                                    G.DrawString(Text, Font, br, RecText, ButtonString);
                                }

                                break;
                            }

                        case ContentAlignment.MiddleRight:
                            {
                                var Rec = new Rectangle(imgY, imgY, img.Width, img.Height);
                                int Bo = imgY + img.Width + imgY - 5;
                                var RecText = new Rectangle(Bo, imgY, Width - Bo, img.Height);
                                var u = Rectangle.Union(Rec, RecText);
                                int innerSpace = RecText.Left - Rec.Right;

                                if (!RTL)
                                {
                                    Rec.X = u.Left;
                                    RecText.X = u.Left + Rec.Width + innerSpace;
                                }
                                else
                                {
                                    Rec.X = u.Right - Rec.Width - 2;
                                    RecText.X = u.Right - RecText.Width - Rec.Width - innerSpace;
                                }

                                G.DrawImage((Bitmap)img.Clone(), Rec);
                                using (var br = new SolidBrush(ForeColor))
                                {
                                    G.DrawString(Text, Font, br, RecText, ButtonString);
                                }

                                break;
                            }
                    }
                }
            }
            #endregion

        }
    }

}