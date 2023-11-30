using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    [Description("Themed TrackBar for WinPaletter UI")]
    [DefaultEvent("Scroll")]
    public class Trackbar : Control
    {
        public Trackbar()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | (ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);

            DoubleBuffered = true;
            BackColor = Color.Transparent;
            Height = 19;
            Text = string.Empty;

            _alpha = 0;
            Invalidate();
        }

        #region Variables
        private bool CanAnimate => !DesignMode && Program.Style.Animations && this != null && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        private readonly int ButtonSize = 0;
        private Rectangle LSA;
        private Rectangle Shaft;
        private Rectangle Thumb;
        private bool ThumbDown;
        private Rectangle Circle;
        private int I1;

        public MouseState State = MouseState.None;

        public enum MouseState
        {
            None,
            Over,
            Down
        }

        #endregion

        #region Properties

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

                if (CanAnimate)
                {
                    FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                }
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

        protected override void OnMouseUp(MouseEventArgs e)
        {
            ThumbDown = false;
            State = MouseState.None;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 255; }

            base.OnMouseUp(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (Circle.Contains(this.PointToClient(MousePosition)))
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

        #region Voids
        private void InvalidateLayout()
        {
            LSA = new(0, 0, ButtonSize, Height);
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

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this == null) return;

            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            Rectangle bar = new(0, (int)Math.Round((Height - Height * 0.25d) / 2d), Width - 1, (int)Math.Round(Height * 0.25d));

            //Excluded to solve UI bug of short rounded rectangle height
            G.ExcludeClip(new Rectangle(-1, 0, 3, Height));
            G.ExcludeClip(new Rectangle(Width - 2, 0, 3, Height));

            G.FillRoundedRect(scheme.Brushes.Back_Max, bar);
            G.FillRoundedRect(scheme.Brushes.AccentAlt, new Rectangle(Thumb.X + 1, bar.Y, (int)Math.Round(Circle.Left + Circle.Width / 2d), bar.Height));

            G.ResetClip();

            Circle = new((int)Math.Round((Value / (double)Maximum) * Shaft.Width), 0, Height - 1, Height - 1);

            G.FillEllipse(scheme.Brushes.Line, Circle);

            Rectangle smallC1 = new(Circle.X + 5, Circle.Y + 5, Circle.Width - 10, Circle.Height - 10);
            Rectangle smallC2 = new(Circle.X + 4, Circle.Y + 4, Circle.Width - 8, Circle.Height - 8);

            G.FillEllipse(scheme.Brushes.AccentAlt, smallC1);

            using SolidBrush br = new(Color.FromArgb(alpha, scheme.Colors.AccentAlt));
            G.FillEllipse(br, smallC2);

            base.OnPaint(e);
        }
    }
}