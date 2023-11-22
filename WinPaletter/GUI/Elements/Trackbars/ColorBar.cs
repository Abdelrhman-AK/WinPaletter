using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{

    [Description("Themed Trackbar for selecting colors (HSL) for WinPaletter UI")]
    [DefaultEvent("Scroll")]
    public class ColorBar : Control
    {

        public ColorBar()
        {
            cb_H = new ColorBlend()
            {
                Positions = new[] { 0f, 1f / 6.0f, 2f / 6.0f, 3f / 6.0f, 4f / 6.0f, 5f / 6.0f, 1f },
                Colors = new[] { Color1, Color2, Color3, Color4, Color5, Color6, Color7 }
            };
            cb_L = new ColorBlend() { Positions = new[] { 0f, 1f / 2.0f, 1f }, Colors = new[] { Color.Black, _AccentColor, Color.White } };
            Timer = new Timer() { Enabled = false, Interval = 1 };

            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);
            DoubleBuffered = true;
            BackColor = Color.Transparent;

            Height = 19;
            Text = string.Empty;
            MouseUp += ColorBar_MouseUp;
            MouseEnter += ColorBar_MouseEnter;
            MouseLeave += ColorBar_MouseLeave;
            MouseWheel += ColorBar_MouseWheel;
            HandleCreated += ColorBar_HandleCreated;
            HandleDestroyed += ColorBar_HandleDestroyed;
            Timer.Tick += Timer_Tick;
        }

        #region Variables

        private readonly int ButtonSize = 0;
        private readonly int ThumbSize = 35; // 14 minimum
        private Rectangle LSA;
        private Rectangle RSA;
        private Rectangle Shaft;
        private Rectangle Thumb;
        private bool ThumbDown;
        private Rectangle Circle;
        private bool _Shown = false;
        private int I1;

        private readonly Color Color1 = Color.FromArgb(255, 23, 0);
        private readonly Color Color2 = Color.FromArgb(253, 253, 0);
        private readonly Color Color3 = Color.FromArgb(58, 255, 0);
        private readonly Color Color4 = Color.FromArgb(0, 255, 240);
        private readonly Color Color5 = Color.FromArgb(0, 17, 255);
        private readonly Color Color6 = Color.FromArgb(255, 0, 212);
        private readonly Color Color7 = Color.FromArgb(255, 0, 23);
        private readonly ColorBlend cb_H;
        private ColorBlend cb_L;

        public MouseState State = MouseState.None;

        public enum MouseState
        {
            None,
            Over,
            Down
        }

        public enum ModesList
        {
            Hue,
            Saturation,
            Light
        }

        #endregion

        #region Properties

        private Color _AccentColor = Program.Style.Schemes.Main.Colors.AccentAlt;
        public Color AccentColor
        {
            get
            {
                return _AccentColor;
            }
            set
            {
                _AccentColor = value;
                Refresh();
            }
        }

        private int _H = 0;
        public int H
        {
            get
            {
                return _H;
            }
            set
            {
                _H = value;
                Refresh();
            }
        }

        private float _S = 1f;
        public float S
        {
            get
            {
                return _S;
            }
            set
            {
                _S = value;
                Refresh();
            }
        }

        private int _L = (int)Math.Round(0.5);
        public float L
        {
            get
            {
                return _L;
            }
            set
            {
                _L = (int)Math.Round(value);
                Refresh();
            }
        }

        private int _Minimum;
        public int Minimum
        {
            get
            {
                return _Minimum;
            }
            set
            {
                _Minimum = value;
                if (value > _Value)
                    _Value = value;
                if (value > _Maximum)
                    _Maximum = value;

                InvalidateLayout();
            }
        }

        private int _Maximum = 100;
        public int Maximum
        {
            get
            {
                return _Maximum;
            }
            set
            {
                _Maximum = value;
                if (value < _Value)
                    _Value = value;
                if (value < _Minimum)
                    _Minimum = value;
                InvalidateLayout();
            }
        }

        private int _Value;
        public int Value
        {
            get
            {

                return _Value;
            }
            set
            {
                if (value == _Value)
                    return;

                if (value > _Maximum)
                {
                    value = _Maximum;
                }

                if (value < _Minimum)
                {
                    value = _Minimum;
                }

                _Value = value;
                InvalidatePosition();

                Scroll?.Invoke(this);
            }
        }

        private int _SmallChange = 1;
        public int SmallChange
        {
            get
            {
                return _SmallChange;
            }
            set
            {
                if (value < 1)
                {
                    throw new Exception("Property value is not valid.");
                }

                _SmallChange = value;
            }
        }

        private int _LargeChange = 10;
        public int LargeChange
        {
            get
            {
                return _LargeChange;
            }
            set
            {
                if (value < 1)
                {
                    throw new Exception("Property value is not valid.");
                }

                _LargeChange = value;
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = string.Empty;

        public ModesList Mode { get; set; } = ModesList.Hue;

        protected override CreateParams CreateParams
        {
            get
            {
                var cpar = base.CreateParams;
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

        public event ScrollEventHandler Scroll;

        public delegate void ScrollEventHandler(object sender);

        protected override void OnSizeChanged(EventArgs e)
        {
            Height = 19;
            InvalidateLayout();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                State = MouseState.Down;
                Timer.Enabled = true;
                Timer.Start();

                if (Circle.Contains(e.Location))
                {
                    ThumbDown = true;
                    return;
                }
                else if (e.X < Circle.X)
                {

                    I1 = _Value - _LargeChange;
                }
                else
                {
                    I1 = _Value + _LargeChange;
                }

                Value = Math.Min(Math.Max(I1, _Minimum), _Maximum);

                InvalidatePosition();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (Circle.Contains(e.Location) & !(e.Button == MouseButtons.Left))
            {
                State = MouseState.Over;
            }
            else if (e.Button == MouseButtons.Left)
                State = MouseState.Down;
            else
                State = MouseState.None;

            Invalidate();

            if (ThumbDown)
            {
                Value = (int)Math.Round(Math.Min(Math.Max(e.X / (double)Width * Maximum, _Minimum), _Maximum));
                InvalidatePosition();
            }

            Timer.Enabled = true;
            Timer.Start();
        }

        private void InvalidateLayout()
        {
            LSA = new Rectangle(0, 0, ButtonSize, Height);
            RSA = new Rectangle(Width - ButtonSize, 0, ButtonSize, Height);
            Shaft = new Rectangle((int)Math.Round(LSA.Right + 1 + 0.5d * Height), 0, Width - Height - 1, Height);
            Thumb = new Rectangle(0, 1, (int)Math.Round(Value / (double)Maximum * Shaft.Width), Height - 3);
            Circle = new Rectangle((int)Math.Round(Value / (double)Maximum * Shaft.Width), 0, Height - 1, Height - 1);
            Scroll?.Invoke(this);
            InvalidatePosition();
        }

        private void InvalidatePosition()
        {
            Thumb.Width = (int)Math.Round(Value / (double)Maximum * Width);
            Refresh();
        }

        private void ColorBar_MouseUp(object sender, MouseEventArgs e)
        {
            ThumbDown = false;
            State = MouseState.None;
            Timer.Enabled = true;
            Timer.Start();
            Invalidate();
        }

        private void ColorBar_MouseEnter(object sender, EventArgs e)
        {
            if (Thumb.Contains(MousePosition))
            {
                State = MouseState.Over;
                Invalidate();
                Timer.Enabled = true;
                Timer.Start();
            }
        }

        private void ColorBar_MouseLeave(object sender, EventArgs e)
        {
            State = MouseState.None;
            Timer.Enabled = true;
            Timer.Start();
            Invalidate();
        }

        private void ColorBar_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                if (Value < Maximum)
                {
                    if (e.Delta <= -240)
                        Value += LargeChange;
                    else
                        Value += SmallChange;
                }
            }
            else if (Value > Minimum)
            {
                if (e.Delta >= 240)
                    Value -= LargeChange;
                else
                    Value -= SmallChange;
            }
        }

        private void ColorBar_HandleCreated(object sender, EventArgs e)
        {
            try
            {
                if (!DesignMode)
                {
                    FindForm().Load += Loaded;
                    FindForm().Shown += Showed;
                    Parent.BackColorChanged += RefreshColorPalette;
                    Parent.VisibleChanged += RefreshColorPalette;
                    Parent.EnabledChanged += RefreshColorPalette;
                    VisibleChanged += RefreshColorPalette;
                    EnabledChanged += RefreshColorPalette;
                }
            }
            catch
            {
            }

            try
            {
                alpha = 0;
            }
            catch
            {
            }
        }

        private void ColorBar_HandleDestroyed(object sender, EventArgs e)
        {
            try
            {
                if (!DesignMode)
                {
                    FindForm().Load -= Loaded;
                    FindForm().Shown -= Showed;
                    Parent.BackColorChanged -= RefreshColorPalette;
                    Parent.VisibleChanged -= RefreshColorPalette;
                    Parent.EnabledChanged -= RefreshColorPalette;
                    VisibleChanged -= RefreshColorPalette;
                    EnabledChanged -= RefreshColorPalette;
                }
            }
            catch
            {
            }
        }

        public void Loaded(object sender, EventArgs e)
        {
            _Shown = false;
        }

        public void Showed(object sender, EventArgs e)
        {
            _Shown = true;
            Invalidate();
        }

        public void RefreshColorPalette(object sender, EventArgs e)
        {
            Invalidate();
        }

        #endregion

        #region Animator

        private int alpha;
        private readonly int Factor = 25;
        private Timer Timer;

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!DesignMode)
            {

                if (State == MouseState.Over & _Shown)
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

                    System.Threading.Thread.Sleep(1);
                    Invalidate();
                }

                if (_Shown & (!(State == MouseState.Over) | State == MouseState.Down))
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

                    System.Threading.Thread.Sleep(1);
                    Invalidate();
                }
            }
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            Color color;

            var middleRect = new Rectangle(0, (int)Math.Round((Height - Height * 0.25d) / 2d), Width - 1, (int)Math.Round(Height * 0.25d));

            LinearGradientBrush back;

            switch (Mode)
            {
                case ModesList.Hue:
                    {
                        back = new LinearGradientBrush(middleRect, Color.Black, Color.Black, 0f, false) { InterpolationColors = cb_H };
                        var HSL_ = Color.FromArgb(0, 255, 240).ToHSL();
                        HSL_.H = (int)Math.Round(Value / (double)Maximum * 359d);
                        color = HSL_.ToRGB();
                        break;
                    }

                case ModesList.Saturation:
                    {
                        var HSL_x1 = _AccentColor.ToHSL();
                        var HSL_x2 = _AccentColor.ToHSL();
                        HSL_x1.S = 0f;
                        HSL_x2.S = 1f;
                        back = new LinearGradientBrush(middleRect, HSL_x1.ToRGB(), HSL_x2.ToRGB(), LinearGradientMode.Horizontal);
                        var HSL_ = _AccentColor.ToHSL();
                        HSL_.S = (float)(Value / (double)Maximum);
                        color = HSL_.ToRGB();
                        break;
                    }

                case ModesList.Light:
                    {
                        cb_L = new ColorBlend() { Positions = new[] { 0f, 1f / 2.0f, 1f }, Colors = new[] { Color.Black, _AccentColor, Color.White } };
                        back = new LinearGradientBrush(middleRect, Color.Black, Color.Black, 0f, false) { InterpolationColors = cb_L };
                        var HSL_ = _AccentColor.ToHSL();
                        HSL_.L = (float)(Value / (double)Maximum);
                        color = HSL_.ToRGB();
                        break;
                    }

                default:
                    {
                        color = scheme.Colors.Back_Max;
                        back = new LinearGradientBrush(middleRect, color, color, (float)Paths.GradientMode.Horizontal);
                        break;
                    }

            }

            //Excluded to solve UI bug of short rounded rectangle height
            G.ExcludeClip(new Rectangle(-1, 0, 3, Height));
            G.ExcludeClip(new Rectangle(Width - 2, 0, 3, Height));

            G.FillRoundedRect(back, middleRect);

            G.ResetClip();

            Circle = new Rectangle((int)Math.Round(Value / (double)Maximum * Shaft.Width), 0, Height - 1, Height - 1);

            G.FillEllipse(Program.Style.Schemes.Main.Brushes.Line, Circle);

            var smallC1 = new Rectangle(Circle.X + 5, Circle.Y + 5, Circle.Width - 10, Circle.Height - 10);
            var smallC2 = new Rectangle(Circle.X + 4, Circle.Y + 4, Circle.Width - 8, Circle.Height - 8);

            using (var br = new SolidBrush(color)) { G.FillEllipse(br, smallC1); }

            using (var br = new SolidBrush(Color.FromArgb(alpha, color))) { G.FillEllipse(br, smallC2); }
        }

    }

}