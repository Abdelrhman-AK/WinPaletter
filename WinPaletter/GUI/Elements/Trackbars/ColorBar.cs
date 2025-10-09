using FluentTransitions;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
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
                Positions = [0f, 1f / 6.0f, 2f / 6.0f, 3f / 6.0f, 4f / 6.0f, 5f / 6.0f, 1f],
                Colors = [Color1, Color2, Color3, Color4, Color5, Color6, Color7]
            };
            cb_L = new() { Positions = [0f, 0.5f, 1f], Colors = [Color.Black, _AccentColor, Color.White] };

            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | (ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;

            Text = string.Empty;
            _alpha = 0;

            TabStop = true; // allow keyboard focus
        }

        #region Variables
        private bool CanAnimate => !DesignMode && Program.Style.Animations && this != null && Visible && Parent?.Visible == true && FindForm()?.Visible == true;

        private readonly float ButtonSize = 0;
        private RectangleF LSA;
        private RectangleF RSA;
        private RectangleF Shaft;
        private RectangleF Thumb;
        private bool ThumbDown;
        private RectangleF Circle;
        private float I1;

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

        private LinearGradientBrush? backBrushCache;

        public enum MouseState { None, Over, Down }
        public enum ModesList { Hue, Saturation, Light }
        #endregion

        #region Properties
        private Color _AccentColor = Program.Style.Schemes.Main.Colors.AccentAlt;
        public Color AccentColor
        {
            get => _AccentColor;
            set
            {
                if (value != _AccentColor)
                {
                    _AccentColor = value;
                    InvalidateBrushCache();
                    Invalidate();
                }
            }
        }

        private float _H = 0f;
        public float H { get => _H; set { if (value != _H) { _H = value; Invalidate(); } } }

        private float _S = 1f;
        public float S { get => _S; set { if (value != _S) { _S = value; Invalidate(); } } }

        private float _L = 0.5f;
        public float L { get => _L; set { if (value != _L) { _L = value; Invalidate(); } } }

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
                }
            }
        }

        private int _SmallChange = 1;
        public int SmallChange { get => _SmallChange; set => _SmallChange = Math.Max(1, value); }

        private int _LargeChange = 10;
        public int LargeChange { get => _LargeChange; set => _LargeChange = Math.Max(1, value); }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = string.Empty;

        private ModesList _mode = ModesList.Hue;
        public ModesList Mode
        {
            get => _mode;
            set
            {
                if (value != _mode)
                {
                    _mode = value;
                    InvalidateBrushCache();
                    Invalidate();
                }
            }
        }
        #endregion

        #region Events
        public event ScrollEventHandler? Scroll;
        public delegate void ScrollEventHandler(object sender);
        #endregion

        #region Layout

        private void InvalidateLayout()
        {
            LSA = new(0, 0, ButtonSize, Height);
            RSA = new(Width - ButtonSize, 0, ButtonSize, Height);
            Shaft = new(LSA.Right + 1 + 0.5f * Height, 0, Width - Height - 1, Height);
            Thumb = new(0, 1, Value / Maximum * Shaft.Width, Height - 3);
            Circle = new(Value / Maximum * Shaft.Width, 0, Height - 1, Height - 1);
            Scroll?.Invoke(this);
            InvalidatePosition();
        }

        private async void InvalidatePosition()
        {
            Thumb.Width = Value / Maximum * Width;
            await Task.Delay(10);
            Invalidate();
        }
        #endregion

        #region Mouse / Focus
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                State = MouseState.Down;
                if (CanAnimate) Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                else alpha = 0;

                if (Circle.Contains(e.Location)) { ThumbDown = true; return; }
                I1 = e.X < Circle.X ? _Value - (float)_LargeChange : _Value + (float)_LargeChange;
                Value = (int)Math.Min(Math.Max(I1, _Minimum), _Maximum);
                InvalidatePosition();
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (Circle.Contains(e.Location) && e.Button != MouseButtons.Left) State = MouseState.Over;
            else if (e.Button == MouseButtons.Left) State = MouseState.Down;
            else State = MouseState.None;

            if (ThumbDown)
            {
                Value = (int)Math.Min(Math.Max(e.X / (float)Width * Maximum, _Minimum), _Maximum);
                InvalidatePosition();
            }

            if (CanAnimate)
                Transition.With(this, nameof(alpha), Circle.Contains(PointToClient(MousePosition)) ? 255 : 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            else alpha = Circle.Contains(PointToClient(MousePosition)) ? 255 : 0;

            Invalidate();
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            ThumbDown = false;
            State = MouseState.None;
            if (CanAnimate) Transition.With(this, nameof(alpha), ContainsFocus ? 255 : 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            else alpha = ContainsFocus ? 255 : 0;
            base.OnMouseUp(e);
        }
        #endregion

        #region Keyboard
        protected override bool IsInputKey(Keys keyData)
        {
            return keyData is Keys.Left or Keys.Right or Keys.Home or Keys.End || base.IsInputKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.KeyCode)
            {
                case Keys.Left: Value -= SmallChange; break;
                case Keys.Right: Value += SmallChange; break;
                case Keys.Home: Value = Minimum; break;
                case Keys.End: Value = Maximum; break;
            }
        }
        #endregion

        #region Animator
        private int _alpha = 0;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int alpha { get => _alpha; set { _alpha = value; Invalidate(); } }
        #endregion

        #region Brush Cache
        private void InvalidateBrushCache()
        {
            backBrushCache?.Dispose();
            backBrushCache = null;
        }

        private LinearGradientBrush GetBackBrush(RectangleF rect)
        {
            InvalidateBrushCache();

            backBrushCache = Mode switch
            {
                ModesList.Hue => new(rect, Color.Black, Color.Black, 0f, false) { InterpolationColors = cb_H },
                ModesList.Saturation => new(rect, _AccentColor.ToHSL().WithS(0f).ToRGB(), _AccentColor.ToHSL().WithS(1f).ToRGB(), LinearGradientMode.Horizontal),
                ModesList.Light => new(rect, Color.Black, Color.Black, 0f, false) { InterpolationColors = cb_L = new() { Positions = [0f, 0.5f, 1f], Colors = [Color.Black, _AccentColor, Color.White] } },
                _ => new(rect, _AccentColor, _AccentColor, LinearGradientMode.Horizontal)
            };
            return backBrushCache;
        }
        #endregion

        #region Paint
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            InvokePaintBackground(this, e);

            Circle = new((Value / (float)Maximum) * Shaft.Width, 0, Height - 1, Height - 1);

            RectangleF middleRect = new(Circle.Width / 4f, (Height - Height * 0.25f) / 2f, Width - (Circle.Width / 4f) * 2f - 1, Height * 0.25f);
            using LinearGradientBrush back = GetBackBrush(middleRect);

            G.FillRoundedRect(back, middleRect);

            using (SolidBrush br = new(Program.Style.Schemes.Main.Colors.Line(parentLevel))) G.FillEllipse(br, Circle);

            RectangleF smallC1 = new(Circle.X + 5, Circle.Y + 5, Circle.Width - 10, Circle.Height - 10);
            RectangleF smallC2 = new(Circle.X + 4, Circle.Y + 4, Circle.Width - 8, Circle.Height - 8);

            Color color = Mode switch
            {
                ModesList.Hue => _AccentColor.ToHSL().WithH(Value).ToRGB(),
                ModesList.Saturation => _AccentColor.ToHSL().WithS(Value / (float)Maximum).ToRGB(),
                ModesList.Light => _AccentColor.ToHSL().WithL(Value / (float)Maximum).ToRGB(),
                _ => _AccentColor
            };

            using (SolidBrush br = new(color)) G.FillEllipse(br, smallC1);
            using (SolidBrush br = new(Color.FromArgb(alpha, color))) G.FillEllipse(br, smallC2);

            base.OnPaint(e);
        }
        #endregion

        int parentLevel = 0;
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            parentLevel = this.Level();
        }
    }
}
