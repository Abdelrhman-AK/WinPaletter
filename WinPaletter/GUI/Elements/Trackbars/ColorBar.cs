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
            cb_H = new()
            {
                Positions = new[] { 0f, 1f / 6.0f, 2f / 6.0f, 3f / 6.0f, 4f / 6.0f, 5f / 6.0f, 1f },
                Colors = new[] { Color1, Color2, Color3, Color4, Color5, Color6, Color7 }
            };
            cb_L = new() { Positions = new[] { 0f, 1f / 2.0f, 1f }, Colors = new[] { Color.Black, _AccentColor, Color.White } };

            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | (ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);
            DoubleBuffered = true;
            BackColor = Color.Transparent;

            Height = 19;
            Text = string.Empty;

            _alpha = 0;
        }

        #region Variables
        private bool CanAnimate => !DesignMode && Program.Style.Animations && this != null && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        private readonly int ButtonSize = 0;
        private Rectangle LSA;
        private Rectangle RSA;
        private Rectangle Shaft;
        private Rectangle Thumb;
        private bool ThumbDown;
        private Rectangle Circle;
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
            get { return _AccentColor; }
            set
            {
                if (value != _AccentColor)
                {
                    _AccentColor = value;
                    Refresh();
                }
            }
        }

        private float _H = 0f;
        public float H
        {
            get => _H;
            set { if (value != _H) { _H = value; Refresh(); } }
        }

        private float _S = 1f;
        public float S
        {
            get => _S;
            set { if (value != _S) { _S = value; Refresh(); } }
        }

        private float _L = 0.5f;
        public float L
        {
            get => _L;
            set { if (value != _L) { _L = value; Refresh(); } }
        }

        private int _Minimum;
        public int Minimum
        {
            get { return _Minimum; }
            set
            {
                if (value != _Minimum)
                {
                    _Minimum = value;

                    if (value > _Maximum) _Maximum = value;

                    if (value > _Value) _Value = value;

                    InvalidateLayout();
                }
            }
        }

        private int _Maximum = 100;
        public int Maximum
        {
            get { return _Maximum; }
            set
            {
                if (value != _Maximum)
                {
                    _Maximum = value;

                    if (value < _Value) _Value = value;

                    if (value < _Minimum) _Minimum = value;

                    InvalidateLayout();
                }
            }
        }

        private int _Value;
        public int Value
        {
            get { return _Value; }
            set
            {
                if (value > _Maximum)
                {
                    value = _Maximum;
                }

                if (value < _Minimum)
                {
                    value = _Minimum;
                }

                if (_Value != value)
                {
                    _Value = value;
                    InvalidateLayout();
                    InvalidatePosition();
                    Scroll?.Invoke(this);
                }
            }
        }

        private int _SmallChange = 1;
        public int SmallChange
        {
            get { return _SmallChange; }
            set
            {
                if (value < 1) value = 1;

                if (value != _SmallChange)
                {
                    _SmallChange = value;
                }
            }
        }

        private int _LargeChange = 10;
        public int LargeChange
        {
            get { return _LargeChange; }
            set
            {
                if (value < 1) value = 1;

                if (value != _LargeChange)
                {
                    _LargeChange = value;
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = string.Empty;

        public ModesList Mode { get; set; } = ModesList.Hue;

        #endregion

        #region Events/Overrides

        public event ScrollEventHandler Scroll;

        public delegate void ScrollEventHandler(object sender);

        protected override void OnSizeChanged(EventArgs e)
        {
            Height = 19;
            InvalidateLayout();

            base.OnSizeChanged(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                State = MouseState.Down;

                if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
                else { alpha = 0; }

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

            base.OnMouseDown(e);
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

            if (CanAnimate)
            {
                FluentTransitions.Transition.With(this, nameof(alpha), Circle.Contains(this.PointToClient(MousePosition)) ? 255 : 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            }
            else { alpha = Circle.Contains(this.PointToClient(MousePosition)) ? 255 : 0; }

            base.OnMouseMove(e);
        }

        private void InvalidateLayout()
        {
            LSA = new(0, 0, ButtonSize, Height);
            RSA = new(Width - ButtonSize, 0, ButtonSize, Height);
            Shaft = new((int)Math.Round(LSA.Right + 1 + 0.5d * Height), 0, Width - Height - 1, Height);
            Thumb = new(0, 1, (int)Math.Round(Value / (double)Maximum * Shaft.Width), Height - 3);
            Circle = new((int)Math.Round(Value / (double)Maximum * Shaft.Width), 0, Height - 1, Height - 1);
            Scroll?.Invoke(this);
            InvalidatePosition();
        }

        private void InvalidatePosition()
        {
            Thumb.Width = (int)Math.Round(Value / (double)Maximum * Width);
            Refresh();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            ThumbDown = false;
            State = MouseState.None;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }

            base.OnMouseUp(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (Thumb.Contains(MousePosition))
            {
                State = MouseState.Over;

                if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
                else { alpha = 255; }

            }

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            State = MouseState.None;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }

            base.OnMouseLeave(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
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

            base.OnMouseWheel(e);
        }

        #endregion

        #region Animator
        private int _alpha = 0;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int alpha
        {
            get => _alpha;
            set { _alpha = value; Refresh(); }
        }
        #endregion

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //Leave it empty to make control background transparent
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {


            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            Color color;

            Rectangle middleRect = new(0, (int)Math.Round((Height - Height * 0.25d) / 2d), Width - 1, (int)Math.Round(Height * 0.25d));

            LinearGradientBrush back;

            switch (Mode)
            {
                case ModesList.Hue:
                    {
                        back = new(middleRect, Color.Black, Color.Black, 0f, false) { InterpolationColors = cb_H };
                        ColorsExtensions.HSL HSL_ = Color.FromArgb(0, 255, 240).ToHSL();
                        HSL_.H = (int)Math.Round(Value / (double)Maximum * 359d);
                        color = HSL_.ToRGB();
                        break;
                    }

                case ModesList.Saturation:
                    {
                        ColorsExtensions.HSL HSL_x1 = _AccentColor.ToHSL();
                        ColorsExtensions.HSL HSL_x2 = _AccentColor.ToHSL();
                        HSL_x1.S = 0f;
                        HSL_x2.S = 1f;
                        back = new(middleRect, HSL_x1.ToRGB(), HSL_x2.ToRGB(), LinearGradientMode.Horizontal);
                        ColorsExtensions.HSL HSL_ = _AccentColor.ToHSL();
                        HSL_.S = (float)(Value / (double)Maximum);
                        color = HSL_.ToRGB();
                        break;
                    }

                case ModesList.Light:
                    {
                        cb_L = new() { Positions = new[] { 0f, 1f / 2.0f, 1f }, Colors = new[] { Color.Black, _AccentColor, Color.White } };
                        back = new(middleRect, Color.Black, Color.Black, 0f, false) { InterpolationColors = cb_L };
                        ColorsExtensions.HSL HSL_ = _AccentColor.ToHSL();
                        HSL_.L = (float)(Value / (double)Maximum);
                        color = HSL_.ToRGB();
                        break;
                    }

                default:
                    {
                        color = scheme.Colors.Back_Max;
                        back = new(middleRect, color, color, (float)Paths.GradientMode.Horizontal);
                        break;
                    }

            }

            //Excluded to solve UI bug of short rounded rectangle height
            G.ExcludeClip(new Rectangle(-1, 0, 3, Height));
            G.ExcludeClip(new Rectangle(Width - 2, 0, 3, Height));

            G.FillRoundedRect(back, middleRect);

            G.ResetClip();

            Circle = new((int)Math.Round(Value / (double)Maximum * Shaft.Width), 0, Height - 1, Height - 1);

            G.FillEllipse(Program.Style.Schemes.Main.Brushes.Line, Circle);

            Rectangle smallC1 = new(Circle.X + 5, Circle.Y + 5, Circle.Width - 10, Circle.Height - 10);
            Rectangle smallC2 = new(Circle.X + 4, Circle.Y + 4, Circle.Width - 8, Circle.Height - 8);

            using (SolidBrush br = new(color)) { G.FillEllipse(br, smallC1); }

            using (SolidBrush br = new(Color.FromArgb(alpha, color))) { G.FillEllipse(br, smallC2); }

            base.OnPaint(e);
        }
    }
}