using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{

    [Description("Themed Button for WinPaletter UI")]
    public class Button : System.Windows.Forms.Button
    {

        public Button()
        {
            LineImage = LineColor;
            Timer = new Timer() { Enabled = false, Interval = 1 };
            Font = new Font("Segoe UI", 9f);
            ForeColor = Color.White;
            if (My.Env.Style.DarkMode)
                BackColor = Color.FromArgb(50, 50, 50);
            else
                BackColor = Color.FromArgb(225, 225, 225);
            LineColor = Color.FromArgb(0, 81, 210);
            Image = base.Image;
            DoubleBuffered = true;

            try
            {
                if (Image is not null)
                {
                    LineImage = Image.AverageColor();
                }
                else
                {
                    LineImage = LineColor;
                }
            }
            catch
            {
            }

            LostFocus += Button_LostFocus;
            HandleCreated += Button_HandleCreated;
            HandleDestroyed += Button_HandleDestroyed;
            Timer.Tick += Timer_Tick;
        }

        #region Variables

        private readonly TextureBrush Noise = new TextureBrush(My.Resources.GaussianBlur.Fade(0.6d));
        private Color LineImage;
        private Color BC;
        private readonly int Steps = 15;
        private readonly int Delay = 1;
        private bool _Shown = false;

        public MouseState State = MouseState.None;

        public enum MouseState
        {
            None,
            Over,
            Down
        }

        #endregion

        #region Properties
        private Color _LineColor = Color.FromArgb(0, 81, 210);
        public Color LineColor
        {
            get
            {
                return _LineColor;
            }

            set
            {
                if (!(value == _LineColor))
                {
                    _LineColor = value;
                    Refresh();
                }
            }
        }

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

                try
                {
                    if (Image is not null)
                    {
                        LineImage = Image.AverageColor();
                        LineColor = LineImage;
                    }
                    else
                    {
                        LineImage = LineColor;
                    }
                }
                catch
                {

                }

                Invalidate();
            }
        }

        public bool DrawOnGlass { get; set; } = false;

        protected override CreateParams CreateParams
        {
            get
            {
                var cpar = base.CreateParams;
                if (DrawOnGlass & !DesignMode)
                {
                    cpar.ExStyle = cpar.ExStyle | 0x20;
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

        #region OnMouse
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseState.Over;
            var C_Before = BackColor;
            var C_After = default(Color);

            switch (My.Env.Style.DarkMode)
            {
                case true:
                    {
                        C_After = LineColor.Dark(0.15f);
                        break;
                    }
                case false:
                    {
                        C_After = LineColor.Light((float)0.9d).CB(0.4f);
                        break;
                    }
            }

            if (!DesignMode)
                Visual.FadeColor(this, "BackColor", C_Before, C_After, Steps, Delay);

            if (_Shown)
            {
                Timer.Enabled = true;
                Timer.Start();
            }

            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            State = MouseState.None;

            var C_Before = BackColor;

            var C_After = this.GetParentColor().CB((float)(this.GetParentColor().IsDark() ? 0.04d : -0.03d));

            if (!DesignMode)
                Visual.FadeColor(this, "BackColor", C_Before, C_After, Steps, Delay);

            if (_Shown)
            {
                Timer.Enabled = true;
                Timer.Start();
            }

            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {

            var C_Before = BackColor;
            var C_After = default(Color);

            switch (My.Env.Style.DarkMode)
            {
                case true:
                    {
                        C_After = LineColor.Dark(0.3f);
                        break;
                    }
                case false:
                    {
                        C_After = LineColor.Light(0.75f);
                        break;
                    }
            }

            if (!DesignMode)
                Visual.FadeColor(this, "BackColor", C_Before, C_After, Steps, Delay);
            State = MouseState.Down;

            if (_Shown)
            {
                Timer.Enabled = true;
                Timer.Start();
            }

            Invalidate();

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            var C_Before = BackColor;
            var C_After = default(Color);

            switch (My.Env.Style.DarkMode)
            {
                case true:
                    {
                        C_After = LineColor.Dark(0.15f);
                        break;
                    }
                case false:
                    {
                        C_After = LineColor.Light((float)0.9d).CB(0.4f);
                        break;
                    }
            }

            if (!DesignMode)
                Visual.FadeColor(this, "BackColor", C_Before, C_After, Steps, Delay);

            State = MouseState.Over;

            if (_Shown)
            {
                Timer.Enabled = true;
                Timer.Start();
            }

            Invalidate();
        }
        #endregion

        #region OnKey
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            var C_Before = BackColor;
            var C_After = default(Color);

            switch (My.Env.Style.DarkMode)
            {
                case true:
                    {
                        C_After = LineColor.Light(0.3f);
                        break;
                    }
                case false:
                    {
                        C_After = LineColor.Light(0.75f);
                        break;
                    }
            }

            if (!DesignMode)
                Visual.FadeColor(this, "BackColor", C_Before, C_After, Steps, Delay);
            State = MouseState.Down;
            Invalidate();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            State = MouseState.None;

            var C_Before = BackColor;
            var C_After = this.GetParentColor().CB((float)(this.GetParentColor().IsDark() ? 0.04d : -0.03d));
            if (!DesignMode)
                Visual.FadeColor(this, "BackColor", C_Before, C_After, Steps, Delay);
            Invalidate();
        }
        #endregion

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            State = MouseState.None;

            var C_Before = BackColor;
            var C_After = this.GetParentColor().CB((float)(this.GetParentColor().IsDark() ? 0.04d : -0.03d));
            if (!DesignMode)
                Visual.FadeColor(this, "BackColor", C_Before, C_After, Steps, Delay);
            Invalidate();
        }

        private void Button_LostFocus(object sender, EventArgs e)
        {
            State = MouseState.None;
            Invalidate();
        }

        private void Button_HandleCreated(object sender, EventArgs e)
        {
            try
            {
                if (!DesignMode)
                {
                    try
                    {
                        FindForm().Load += Loaded;
                        FindForm().Shown += Showed;
                        FindForm().FormClosed += Closed;
                        Parent.Invalidated += ForceRefresh;
                        Parent.BackColorChanged += ForceRefresh;
                    }
                    catch
                    {
                    }
                }
                alpha = 0;
            }
            catch
            {
            }
        }

        private void Button_HandleDestroyed(object sender, EventArgs e)
        {
            try
            {
                if (!DesignMode)
                {
                    try
                    {
                        FindForm().Load -= Loaded;
                        FindForm().Shown -= Showed;
                        FindForm().FormClosed -= Closed;
                        Parent.Invalidated -= ForceRefresh;
                        Parent.BackColorChanged -= ForceRefresh;
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
        }

        public void Loaded(object sender, EventArgs e)
        {
            _Shown = false;
            Timer.Enabled = false;
            Timer.Stop();
        }

        public void Showed(object sender, EventArgs e)
        {
            _Shown = true;
        }

        public void Closed(object sender, EventArgs e)
        {
            _Shown = false;
            Timer.Enabled = false;
            Timer.Stop();
        }

        public void ForceRefresh(object sender, EventArgs e)
        {
            try
            {
                BC = this.GetParentColor().CB((float)(this.GetParentColor().IsDark() ? 0.05d : -0.03d));
                BackColor = BC;
                Invalidate();
            }
            catch
            {
            }
        }

        #endregion

        #region Animator

        private int alpha;
        private readonly int Factor = 15;
        private Timer Timer;

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!DesignMode)
                {

                    if (State == MouseState.Over)
                    {
                        if (alpha + Factor <= 255)
                        {
                            alpha += Factor;
                        }
                        else if (alpha + Factor > 255)
                        {
                            alpha = 255;
                            Timer.Enabled = false;
                            Timer.Stop();
                        }

                        if (_Shown)
                        {
                            System.Threading.Thread.Sleep(1);
                            Invalidate();
                        }
                    }

                    if (!(State == MouseState.Over))
                    {
                        if (alpha - Factor >= 0)
                        {
                            alpha -= Factor;
                        }
                        else if (alpha - Factor < 0)
                        {
                            alpha = 0;
                            Timer.Enabled = false;
                            Timer.Stop();
                        }

                        if (_Shown)
                        {
                            System.Threading.Thread.Sleep(1);
                            Invalidate();
                        }

                    }
                }
            }
            catch
            {
            }
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = TextRenderingHint.SystemDefault;
            DoubleBuffered = true;

            // ################################################################################# Customizer
            var Rect = new Rectangle(0, 0, Width - 1, Height - 1);
            var InnerRect = new Rectangle(1, 1, Width - 3, Height - 3);
            var ParentColor = this.GetParentColor();
            // #################################################################################

            if (DrawOnGlass)
            {
                G.Clear(Color.Transparent);
                using (var br = new SolidBrush(Color.FromArgb((int)Math.Round((255 - alpha) * 0.5d), BackColor)))
                {
                    G.FillRoundedRect(br, InnerRect);
                }
                using (var br = new SolidBrush(Color.FromArgb((int)Math.Round(alpha * 0.5d), BackColor)))
                {
                    G.FillRoundedRect(br, Rect);
                }
            }
            else
            {
                G.Clear(ParentColor);
                using (var br = new SolidBrush(Color.FromArgb(255 - alpha, BackColor)))
                {
                    G.FillRoundedRect(br, InnerRect);
                }
                using (var br = new SolidBrush(Color.FromArgb(alpha, BackColor)))
                {
                    G.FillRoundedRect(br, Rect);
                }
            }

            if (!(State == MouseState.None))
                G.FillRoundedRect(Noise, Rect);

            var c = default(Color);
            Color c1, c1x;

            switch (State)
            {
                case MouseState.None:
                    {
                        c = base.BackColor.CB((float)(ParentColor.IsDark() ? 0.02d : -0.02d));
                        break;
                    }

                case MouseState.Over:
                    {
                        c = base.BackColor.CB((float)(ParentColor.IsDark() ? 0.15d : -0.05d));
                        break;
                    }

                case MouseState.Down:
                    {
                        c = base.BackColor.CB((float)(ParentColor.IsDark() ? 0.08d : -0.03d));
                        break;
                    }

            }

            if (DrawOnGlass)
            {
                c1 = Color.FromArgb((int)Math.Round((255 - alpha) * 0.5d), c);
                c1x = Color.FromArgb((int)Math.Round(alpha * 0.5d), c);
            }
            else
            {
                c1 = Color.FromArgb(255 - alpha, c);
                c1x = Color.FromArgb(alpha, c);
            }

            using (var P = new Pen(c1))
            {
                G.DrawRoundedRect_LikeW11(P, InnerRect);
            }
            using (var P = new Pen(c1x))
            {
                G.DrawRoundedRect_LikeW11(P, Rect);
            }

            #region Text and Image Render
            var ButtonString = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
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
                    {
                        G.DrawString(Text, Font, br, new Rectangle(1, 0, Width, Height), base.TextAlign.ToStringFormat(RTL));
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
                            int alx = (int)Math.Round((Height - (img.Height + 4 + base.Text.Measure(base.Font).Height)) / 2f);

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
                                    G.DrawString(Text, Font, br, new Rectangle(0, alx + 9 + img.Height, Width, Height), ButtonString);
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
                            var RecText = new Rectangle(Bo, imgY, (int)Math.Round(base.Text.Measure(Font).Width + 15f - imgY), img.Height);
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

            #endregion

        }

    }

}