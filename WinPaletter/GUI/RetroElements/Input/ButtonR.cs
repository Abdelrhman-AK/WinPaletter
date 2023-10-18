using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace WinPaletter.UI.Retro
{

    [Description("Retro button with Windows 9x style")]
    public class ButtonR : Button
    {

        public ButtonR()
        {
            Font = new Font("Microsoft Sans Serif", 8f);
            ForeColor = Color.Black;
            BackColor = Color.FromArgb(192, 192, 192);
            Image = base.Image;
            DoubleBuffered = true;
            LostFocus += ButtonR_LostFocus;
        }

        #region Variables

        private MouseState State = MouseState.None;

        public enum MouseState
        {
            None,
            Over,
            Down
        }

        private bool Pressed;
        #endregion

        #region Properties

        private Image _Image;
        public new Image Image
        {
            get
            {
                return _Image;
            }
            set
            {
                _Image = value;
                Invalidate();
            }
        }

        public Color WindowFrame { get; set; } = Color.Black;
        public Color ButtonShadow { get; set; } = Color.FromArgb(128, 128, 128);
        public Color ButtonDkShadow { get; set; } = Color.Black;
        public Color ButtonHilight { get; set; } = Color.White;
        public Color ButtonLight { get; set; } = Color.FromArgb(192, 192, 192);
        public bool UseItAsScrollbar { get; set; } = false;
        public bool AppearsAsPressed { get; set; } = false;
        public bool HatchBrush { get; set; } = false;
        public int FocusRectWidth { get; set; } = 1;
        public int FocusRectHeight { get; set; } = 1;

        #endregion

        #region Events

        private void ButtonR_LostFocus(object sender, EventArgs e)
        {
            State = MouseState.None;
            Pressed = false;
            Invalidate();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            Invalidate();
            base.OnBackColorChanged(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseState.Over;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            State = MouseState.None;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            State = MouseState.Down;
            Pressed = true;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            State = MouseState.Over;
            Invalidate();
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            var B = new Bitmap(Width, Height);
            var G = Graphics.FromImage(B);
            G.SmoothingMode = SmoothingMode.HighSpeed;
            G.TextRenderingHint = Program.RenderingHint;
            DoubleBuffered = true;

            // ################################################################################# Customizer
            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            var rectinner = new Rectangle(1, 1, Width - 3, Height - 3);
            var rectdash = new Rectangle(4, 4, Width - 9, Height - 9);
            // #################################################################################

            G.Clear(BackColor);

            #region Button Render
            if (UseItAsScrollbar)
            {
                G.DrawLine(new Pen(ButtonHilight), new Point(0, 0), new Point(Width - 1, 0));
                G.DrawLine(new Pen(ButtonHilight), new Point(0, 1), new Point(0, Height - 1));
                G.DrawLine(new Pen(ButtonDkShadow), new Point(0, Height - 1), new Point(Width - 1, Height - 1));
                G.DrawLine(new Pen(ButtonDkShadow), new Point(Width - 1, 0), new Point(Width - 1, Height - 1));
                G.DrawLine(new Pen(ButtonLight), new Point(1, 1), new Point(Width - 2, 1));
                G.DrawLine(new Pen(ButtonLight), new Point(1, 2), new Point(1, Height - 2));
                G.DrawLine(new Pen(ButtonShadow), new Point(1, Height - 2), new Point(Width - 2, Height - 2));
                G.DrawLine(new Pen(ButtonShadow), new Point(Width - 2, 1), new Point(Width - 2, Height - 2));
            }
            else if (AppearsAsPressed)
            {
                G.Clear(ButtonHilight);

                if (HatchBrush)
                {
                    var hb = new HatchBrush(HatchStyle.Percent50, ButtonHilight, BackColor);
                    G.FillRectangle(hb, rect);
                }

                G.DrawLine(new Pen(ButtonDkShadow), new Point(0, 0), new Point(Width - 1, 0));
                G.DrawLine(new Pen(ButtonDkShadow), new Point(0, 1), new Point(0, Height - 1));
                G.DrawLine(new Pen(ButtonShadow), new Point(1, 1), new Point(Width - 2, 1));
                G.DrawLine(new Pen(ButtonShadow), new Point(1, 2), new Point(1, Height - 2));
                G.DrawLine(new Pen(ButtonHilight), new Point(0, Height - 1), new Point(Width - 1, Height - 1));
                G.DrawLine(new Pen(ButtonHilight), new Point(Width - 1, 0), new Point(Width - 1, Height - 1));
                G.DrawLine(new Pen(BackColor), new Point(1, Height - 2), new Point(Width - 2, Height - 2));
                G.DrawLine(new Pen(BackColor), new Point(Width - 2, 1), new Point(Width - 2, Height - 2));
            }

            else if (State == MouseState.Over | State == MouseState.None | !Enabled)
            {
                if (!Focused)
                {
                    G.DrawLine(new Pen(ButtonHilight), new Point(0, 0), new Point(Width - 1, 0));
                    G.DrawLine(new Pen(ButtonHilight), new Point(0, 1), new Point(0, Height - 1));
                    G.DrawLine(new Pen(ButtonDkShadow), new Point(0, Height - 1), new Point(Width - 1, Height - 1));
                    G.DrawLine(new Pen(ButtonDkShadow), new Point(Width - 1, 0), new Point(Width - 1, Height - 1));
                    G.DrawLine(new Pen(ButtonLight), new Point(1, 1), new Point(Width - 2, 1));
                    G.DrawLine(new Pen(ButtonLight), new Point(1, 2), new Point(1, Height - 2));
                    G.DrawLine(new Pen(ButtonShadow), new Point(1, Height - 2), new Point(Width - 2, Height - 2));
                    G.DrawLine(new Pen(ButtonShadow), new Point(Width - 2, 1), new Point(Width - 2, Height - 2));
                }
                else
                {
                    G.DrawRectangle(new Pen(ButtonDkShadow), rect);
                    G.DrawLine(new Pen(ButtonHilight), new Point(1, 1), new Point(Width - 2, 1));
                    G.DrawLine(new Pen(ButtonHilight), new Point(1, 2), new Point(1, Height - 2));
                    G.DrawLine(new Pen(ButtonDkShadow), new Point(1, Height - 2), new Point(Width - 2, Height - 2));
                    G.DrawLine(new Pen(ButtonDkShadow), new Point(Width - 2, 1), new Point(Width - 2, Height - 2));
                    G.DrawLine(new Pen(ButtonLight), new Point(2, 2), new Point(Width - 3, 2));
                    G.DrawLine(new Pen(ButtonLight), new Point(2, 3), new Point(2, Height - 3));
                    G.DrawLine(new Pen(ButtonShadow), new Point(2, Height - 3), new Point(Width - 3, Height - 3));
                    G.DrawLine(new Pen(ButtonShadow), new Point(Width - 3, 2), new Point(Width - 3, Height - 3));

                    if (Pressed & !(Font.FontFamily.Name.ToLower() == "marlett"))
                    {
                        var ur = new Rectangle(rectdash.X, rectdash.Y, rectdash.Width, FocusRectHeight);
                        var dr = new Rectangle(ur.X, rectdash.Y + rectdash.Height - ur.Height, ur.Width, ur.Height);
                        var lr = new Rectangle(rectdash.X, rectdash.Y, FocusRectWidth, rectdash.Height);
                        var rr = new Rectangle(rectdash.X + rectdash.Width - lr.Width, rectdash.Y, FocusRectWidth, rectdash.Height);
                        var hb = new HatchBrush(HatchStyle.Percent50, Color.Black, BackColor);
                        G.FillRectangle(hb, ur);
                        G.FillRectangle(hb, dr);
                        G.FillRectangle(hb, lr);
                        G.FillRectangle(hb, rr);
                        G.DrawRectangle(new Pen(WindowFrame), rect);
                    }

                }
            }
            else
            {
                G.DrawRectangle(new Pen(WindowFrame), rect);
                G.DrawRectangle(new Pen(ButtonShadow), rectinner);

                if (!(Font.FontFamily.Name.ToLower() == "marlett"))
                {
                    var ur = new Rectangle(rectdash.X, rectdash.Y, rectdash.Width, FocusRectHeight);
                    var dr = new Rectangle(ur.X, rectdash.Y + rectdash.Height - ur.Height, ur.Width, ur.Height);
                    var lr = new Rectangle(rectdash.X, rectdash.Y, FocusRectWidth, rectdash.Height);
                    var rr = new Rectangle(rectdash.X + rectdash.Width - lr.Width, rectdash.Y, FocusRectWidth, rectdash.Height);
                    var hb = new HatchBrush(HatchStyle.Percent50, Color.Black, BackColor);
                    G.FillRectangle(hb, ur);
                    G.FillRectangle(hb, dr);
                    G.FillRectangle(hb, lr);
                    G.FillRectangle(hb, rr);
                }

            }

            #endregion

            #region Text and Image Render
            var ButtonString = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            int imgX = default, imgY = default;

            try
            {
                if (Image is not null)
                    imgX = (int)Math.Round((Width - Image.Width) / 2d);
            }
            catch
            {
            }

            try
            {
                if (Image is not null)
                    imgY = (int)Math.Round((Height - Image.Height) / 2d);
            }
            catch
            {
            }

            Color FColor;
            if (Enabled)
                FColor = ForeColor;
            else
                FColor = base.BackColor.CB((float)-0.2d);

            if (Image is null)
            {

                if (TextAlign == ContentAlignment.MiddleCenter)
                {

                    var r = rect;

                    // Resetting positions to fix layout misadjust
                    // Never modify
                    if (Font.Name == "Marlett" & Text.Count() == 1)
                    {
                        var textSize = Text.Measure(Font);
                        int x = (int)Math.Round(rect.X + (rect.Width - textSize.Width) / 2f);
                        int y = (int)Math.Round(rect.Y + (rect.Height - textSize.Height) / 2f);
                        int w = (int)Math.Round(textSize.Width);
                        int h = (int)Math.Round(textSize.Height);

                        if (Font.Size >= 4.4d && Font.Size < 5.2d)
                        {
                            h += 2;
                        }

                        else if (Font.Size >= 5.2d && Font.Size < 5.6d)
                        {
                            x += 1;
                        }

                        else if (Font.Size >= 5.6d && Font.Size < 6)
                        {
                            w += 1;
                            x += 1;
                            y += 1;
                        }

                        else if (Font.Size >= 6 && Font.Size < 6.4d)
                        {
                            x += 1;
                            y += 1;
                        }

                        else if (Font.Size >= 6.4d && Font.Size < 6.8d)
                        {
                            y += 1;
                        }

                        else if (Font.Size >= 6.8d && Font.Size < 7.2d)
                        {
                            x += 1;
                            y += 2;
                        }

                        else if (Font.Size >= 7.2d && Font.Size <= 7.6d)
                        {
                            y += 1;
                        }

                        else if (Font.Size >= 8 && Font.Size < 8.4d)
                        {
                            x += 1;
                        }

                        else if (Font.Size >= 8.4d && Font.Size < 8.8d)
                        {
                            x += 1;
                            y += 1;
                        }

                        else if (Font.Size == 8.8d)
                        {

                            if (Text == "3")
                            {
                                x += 1;
                                y += 1;
                            }
                            else if (Text != "u")
                            {
                                y += 1;
                            }

                        }

                        r = new Rectangle(x, y, w, h);
                    }

                    if (!Enabled)
                        G.DrawString(Text, Font, new SolidBrush(base.BackColor.CB(0.8f)), new Rectangle(r.X + 1, r.Y + 1, r.Width, r.Height), ContentAlignment.MiddleCenter.ToStringFormat());
                    G.DrawString(Text, Font, new SolidBrush(FColor), r, ContentAlignment.MiddleCenter.ToStringFormat());
                }
                else
                {
                    if (!Enabled)
                        G.DrawString(Text, Font, new SolidBrush(base.BackColor.CB(0.8f)), new Rectangle(1, 1, Width, Height), base.TextAlign.ToStringFormat());
                    G.DrawString(Text, Font, new SolidBrush(FColor), new Rectangle(0, 0, Width - 1, Height - 1), base.TextAlign.ToStringFormat());
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

                            int alx = (int)Math.Round((Height - (Image.Height + 4 + base.Text.Measure(base.Font).Height)) / 2f);
                            if (string.IsNullOrEmpty(Text))
                            {
                                G.DrawImage(Image, new Rectangle(imgX, imgY, Image.Width, Image.Height));
                            }
                            else
                            {
                                G.DrawImage(Image, new Rectangle(imgX, alx, Image.Width, Image.Height));
                            }

                            if (!Enabled)
                                G.DrawString(Text, Font, new SolidBrush(base.BackColor.CB(0.8f)), new Rectangle(1, alx + 10 + Image.Height, Width, Height), ButtonString);
                            G.DrawString(Text, Font, new SolidBrush(FColor), new Rectangle(0, alx + 9 + Image.Height, Width, Height), ButtonString);
                            break;
                        }

                    case ContentAlignment.MiddleLeft:
                        {
                            ButtonString.Alignment = StringAlignment.Near;
                            ButtonString.LineAlignment = StringAlignment.Center;
                            int alx = (int)Math.Round((Width - (Image.Width + 4 + base.Text.Measure(base.Font).Width)) / 2f);
                            G.DrawImage(Image, new Rectangle(alx, imgY - 1, Image.Width, Image.Height));
                            if (!Enabled)
                                G.DrawString(Text, Font, new SolidBrush(base.BackColor.CB(0.8f)), new Rectangle(alx + 1 + Image.Width, 1, Width, Height), ButtonString);
                            G.DrawString(Text, Font, new SolidBrush(FColor), new Rectangle(alx + Image.Width, 0, Width, Height), ButtonString);
                            break;
                        }

                    case ContentAlignment.MiddleRight:
                        {
                            G.DrawImage(Image, new Rectangle(1, imgY - 1, Image.Width, Image.Height));
                            if (!Enabled)
                                G.DrawString(Text, Font, new SolidBrush(base.BackColor.CB(0.8f)), new Rectangle(8, 1, Width, Height), base.TextAlign.ToStringFormat());
                            G.DrawString(Text, Font, new SolidBrush(FColor), new Rectangle(7, 0, Width, Height), base.TextAlign.ToStringFormat());
                            break;
                        }

                    case ContentAlignment.BottomLeft:
                        {
                            G.DrawImage(Image, new Rectangle(1, imgY, Image.Width, Image.Height));
                            if (!Enabled)
                                G.DrawString(Text, Font, new SolidBrush(base.BackColor.CB(0.8f)), new Rectangle(1 + Image.Width + 2, 1, Width, Height), ContentAlignment.MiddleLeft.ToStringFormat());
                            G.DrawString(Text, Font, new SolidBrush(FColor), new Rectangle(Image.Width + 1, 0, Width, Height), ContentAlignment.MiddleLeft.ToStringFormat());
                            break;
                        }

                }
            }
            #endregion

            e.Graphics.DrawImage(B, new Point(0, 0));
            G.Dispose();
            B.Dispose();
        }

    }
}