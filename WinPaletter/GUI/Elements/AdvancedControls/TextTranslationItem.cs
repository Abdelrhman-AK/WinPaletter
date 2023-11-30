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
        public Image Image { get; set; }

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

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cpar = base.CreateParams;
                if (!DesignMode)
                {
                    cpar.ExStyle |= 0x20;
                    return cpar;
                }
                else
                {
                    return cpar;
                }
            }
        }
        #endregion

        #region Events

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

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this == null) return;

            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = Program.Style.RenderingHint;
            DoubleBuffered = true;
            Rectangle rect = new(0, 0, Width - 1, Height - 1);

            if (_SearchHighlight is not null && !string.IsNullOrWhiteSpace(_SearchHighlight) && Text.ToLower().Trim().Contains(_SearchHighlight.ToLower().Trim()))
            {
                G.FillRectangle(Program.Style.Schemes.Tertiary.Brushes.AccentAlt, rect);
            }

            else if (!string.IsNullOrWhiteSpace(Text) && (Text.Trim() ?? string.Empty) == (Text_English.Trim() ?? string.Empty))
            {
                G.FillRectangle(Program.Style.Schemes.Tertiary.Brushes.AccentAlt, rect);
            }

            else if (Tag is not null && !string.IsNullOrWhiteSpace(Tag.ToString()) && (Tag.ToString().Trim() ?? string.Empty) == (Tag_English.Trim() ?? string.Empty))
            {
                G.FillRectangle(Program.Style.Schemes.Tertiary.Brushes.AccentAlt, rect);
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
            using (StringFormat ButtonString = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
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
                        using (SolidBrush br = new(ForeColor))
                        using (StringFormat sf = TextAlign.ToStringFormat(RTL))
                        {
                            Rectangle r = new(1, 0, Width, Height);
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

                                    using (SolidBrush br = new(ForeColor))
                                    {
                                        Rectangle r = new(0, alx + 9 + img.Height, Width, Height);
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
                                Rectangle Rec = new(imgY, imgY, img.Width, img.Height);
                                int Bo = imgY + img.Width + imgY - 5;
                                Rectangle RecText = new(Bo, imgY, (int)Math.Round(Text.Measure(Font).Width + 15f - imgY), img.Height);
                                Rectangle u = Rectangle.Union(Rec, RecText);
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
                                using (SolidBrush br = new(ForeColor))
                                {
                                    G.DrawString(Text, Font, br, RecText, ButtonString);
                                }

                                break;
                            }

                        case ContentAlignment.MiddleRight:
                            {
                                Rectangle Rec = new(imgY, imgY, img.Width, img.Height);
                                int Bo = imgY + img.Width + imgY - 5;
                                Rectangle RecText = new(Bo, imgY, Width - Bo, img.Height);
                                Rectangle u = Rectangle.Union(Rec, RecText);
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
                                using (SolidBrush br = new(ForeColor))
                                {
                                    G.DrawString(Text, Font, br, RecText, ButtonString);
                                }

                                break;
                            }
                    }
                }
            }
            #endregion

            base.OnPaint(e);
        }
    }

}