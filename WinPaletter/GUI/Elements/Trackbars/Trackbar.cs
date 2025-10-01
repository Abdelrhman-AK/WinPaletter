using FluentTransitions;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    [Description("Themed TrackBar for WinPaletter UI")]
    [DefaultEvent("Scroll")]
    public class TrackBar : Control
    {
        public TrackBar()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | (ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, true); // allow focus by Tab key

            DoubleBuffered = true;
            BackColor = Color.Transparent;
            Height = 19;
            Text = string.Empty;

            _alpha = 0;
        }

        #region Variables
        private bool CanAnimate =>
            !DesignMode &&
            Program.Style.Animations &&
            this != null &&
            Visible &&
            Parent != null &&
            Parent.Visible &&
            FindForm() != null &&
            FindForm().Visible;

        private Rectangle Shaft;
        private Rectangle Circle;
        private bool ThumbDown;

        private int parentLevel = 0;
        #endregion

        #region Properties
        private int _Minimum;
        public int Minimum
        {
            get => _Minimum;
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
            get => _Maximum;
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
            get => _Value;
            set
            {
                if (value > _Maximum) value = _Maximum;
                if (value < _Minimum) value = _Minimum;

                if (_Value != value)
                {
                    _Value = value;
                    InvalidateLayout();
                    InvalidatePosition();
                    Scroll?.Invoke(this);
                    Invalidate();
                }
            }
        }

        private int _SmallChange = 1;
        public int SmallChange
        {
            get => _SmallChange;
            set => _SmallChange = Math.Max(1, value);
        }

        private int _LargeChange = 10;
        public int LargeChange
        {
            get => _LargeChange;
            set => _LargeChange = Math.Max(1, value);
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = string.Empty;
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
                if (CanAnimate)
                    Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                else
                    alpha = 0;

                if (Circle.Contains(e.Location))
                {
                    ThumbDown = true;
                    return;
                }

                int newVal = (e.X < Circle.X) ? _Value - _LargeChange : _Value + _LargeChange;
                Value = Math.Min(Math.Max(newVal, _Minimum), _Maximum);

                InvalidatePosition();
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (ThumbDown)
            {
                Value = (int)Math.Round(Math.Min(Math.Max(e.X / (double)Width * Maximum, _Minimum), _Maximum));
                InvalidatePosition();
            }

            if (CanAnimate)
                Transition.With(this, nameof(alpha), Circle.Contains(PointToClient(MousePosition)) ? 255 : 0)
                          .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            else
                alpha = Circle.Contains(PointToClient(MousePosition)) ? 255 : 0;

            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            ThumbDown = false;
            if (CanAnimate)
                Transition.With(this, nameof(alpha), ContainsFocus ? 255 : 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            else
                alpha = ContainsFocus ? 255 : 0;

            base.OnMouseUp(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Delta < 0)
                Value += (e.Delta <= -240) ? LargeChange : SmallChange;
            else
                Value -= (e.Delta >= 240) ? LargeChange : SmallChange;

            base.OnMouseWheel(e);
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            parentLevel = this.Level();
        }

        protected override bool IsInputKey(Keys keyData) =>
            keyData is Keys.Left or Keys.Right or Keys.Up or Keys.Down or Keys.PageUp or Keys.PageDown or Keys.Home or Keys.End
            ? true
            : base.IsInputKey(keyData);

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.Down: Value -= SmallChange; break;
                case Keys.Right:
                case Keys.Up: Value += SmallChange; break;
                case Keys.PageDown: Value -= LargeChange; break;
                case Keys.PageUp: Value += LargeChange; break;
                case Keys.Home: Value = Minimum; break;
                case Keys.End: Value = Maximum; break;
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            if (CanAnimate)
                Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            else
                alpha = 255;
            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            if (CanAnimate)
                Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            else
                alpha = 0;
            Invalidate();
        }
        #endregion

        #region Methods
        private void InvalidateLayout()
        {
            Shaft = new((int)Math.Round(0.5d * Height), 0, Width - Height - 1, Height);
            InvalidatePosition();
        }

        private void InvalidatePosition()
        {
            double progress = (Maximum > Minimum) ? (Value - Minimum) / (double)(Maximum - Minimum) : 0d;
            progress = Math.Max(0, Math.Min(1, progress));

            Circle = new(
                (int)Math.Round(progress * Shaft.Width),
                0,
                Height - 1,
                Height - 1
            );

            Invalidate();
        }
        #endregion

        #region Animator
        private int _alpha = 0;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int alpha
        {
            get => _alpha;
            set { _alpha = Math.Max(0, Math.Min(255, value)); Invalidate(); }
        }
        #endregion

        protected override void OnPaintBackground(PaintEventArgs pevent) => base.OnPaintBackground(pevent);

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;

            InvokePaintBackground(this, e);

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            double progress = (Maximum > Minimum) ? (Value - Minimum) / (double)(Maximum - Minimum) : 0d;
            progress = Math.Max(0, Math.Min(1, progress));

            RectangleF bar = new(0, (Height - Height * 0.25f) / 2f, Width - 1, Height * 0.25f);

            using (SolidBrush br0 = new(scheme.Colors.Back(parentLevel)))
                G.FillRoundedRect(br0, bar);

            int fillWidth = (int)Math.Round(progress * Shaft.Width);
            if (fillWidth > 0)
            {
                using (LinearGradientBrush lgb0 = new(bar, scheme.Colors.Back_Checked_Hover, scheme.Colors.AccentAlt, LinearGradientMode.Horizontal))
                    G.FillRoundedRect(lgb0, new RectangleF(1, bar.Y, fillWidth, bar.Height));
            }

            Circle = new(fillWidth, 0, Height - 1, Height - 1);
            using (SolidBrush br1 = new(scheme.Colors.Line_Hover(parentLevel)))
                G.FillEllipse(br1, Circle);

            Rectangle smallC1 = new(Circle.X + 5, Circle.Y + 5, Circle.Width - 10, Circle.Height - 10);
            Rectangle smallC2 = new(Circle.X + 4, Circle.Y + 4, Circle.Width - 8, Circle.Height - 8);

            G.FillEllipse(scheme.Brushes.AccentAlt, smallC1);
            using (SolidBrush br2 = new(Color.FromArgb(alpha, scheme.Colors.AccentAlt)))
                G.FillEllipse(br2, smallC2);

            base.OnPaint(e);
        }
    }
}