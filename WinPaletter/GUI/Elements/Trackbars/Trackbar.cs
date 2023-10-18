using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{

    [Description("Themed TrackBar for WinPaletter UI")]
    [DefaultEvent("Scroll")]
    public class Trackbar : Control
    {

        public Trackbar()
        {
            Timer = new Timer() { Enabled = false, Interval = 1 };
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);
            Height = 19;
            Text = "";
            MouseUp += Trackbar_MouseUp;
            MouseEnter += Trackbar_MouseEnter;
            MouseLeave += Trackbar_MouseLeave;
            MouseWheel += Trackbar_MouseWheel;
            HandleCreated += Trackbar_HandleCreated;
            HandleDestroyed += Trackbar_HandleDestroyed;
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
            get
            {
                return _Minimum;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Property value is not valid.");
                }

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
                if (value < 0)
                {
                    throw new Exception("Property value is not valid.");
                }

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
        public override string Text { get; set; } = "";

        #endregion

        #region Events

        public event ScrollEventHandler Scroll;

        public delegate void ScrollEventHandler(object sender);

        protected override void OnSizeChanged(EventArgs e)
        {
            Height = 19;
            InvalidateLayout();
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

        private void Trackbar_MouseUp(object sender, MouseEventArgs e)
        {
            ThumbDown = false;
            State = MouseState.None;
            Timer.Enabled = true;
            Timer.Start();
            Invalidate();
        }

        private void Trackbar_MouseEnter(object sender, EventArgs e)
        {
            if (Thumb.Contains(MousePosition))
            {
                State = MouseState.Over;
                Invalidate();
                Timer.Enabled = true;
                Timer.Start();
            }
        }

        private void Trackbar_MouseLeave(object sender, EventArgs e)
        {
            State = MouseState.None;
            Timer.Enabled = true;
            Timer.Start();
            Invalidate();
        }

        private void Trackbar_MouseWheel(object sender, MouseEventArgs e)
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

        private void Trackbar_HandleCreated(object sender, EventArgs e)
        {

            try
            {
                if (!DesignMode)
                {
                    FindForm().Load += Loaded;
                    FindForm().Shown += Showed;
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

        private void Trackbar_HandleDestroyed(object sender, EventArgs e)
        {
            try
            {
                if (!DesignMode)
                {
                    FindForm().Load -= Loaded;
                    FindForm().Shown -= Showed;
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

        #endregion

        #region Voids/Functions

        private double GetProgress()
        {
            return (_Value - _Minimum) / (double)(_Maximum - _Minimum);
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
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            G.Clear(BackColor);
            bool Dark = Program.Style.DarkMode;
            var c_back = Dark ? Color.FromArgb(60, 60, 60) : Color.FromArgb(210, 210, 210);
            var c_btn = Dark ? Color.FromArgb(165, 165, 165) : Color.FromArgb(100, 100, 100);

            var C = Program.Style.Colors.Core;

            var middleRect = new Rectangle(0, (int)Math.Round((Height - Height * 0.25d) / 2d), Width - 1, (int)Math.Round(Height * 0.25d));

            using (var br = new SolidBrush(c_back))
            {
                G.FillRoundedRect(br, middleRect);
            }

            Circle = new Rectangle((int)Math.Round(Value / (double)Maximum * Shaft.Width), 0, Height - 1, Height - 1);

            {
                using (var br = new SolidBrush(C))
                {
                    G.FillRoundedRect(br, new Rectangle(Thumb.X + 1, middleRect.Y, (int)Math.Round(Circle.Left + Circle.Width / 2d), middleRect.Height));
                }
            }

            using (var br = new SolidBrush(BackColor))
            {
                G.FillRectangle(br, new Rectangle(-1, 0, 4, Height));
            }

            using (var br = new SolidBrush(BackColor))
            {
                G.FillRectangle(br, new Rectangle(Width - 4, 0, 4, Height));
            }

            using (var br = new SolidBrush(Program.Style.Colors.Border))
            {
                G.FillEllipse(br, Circle);
            }

            var smallC1 = new Rectangle(Circle.X + 5, Circle.Y + 5, Circle.Width - 10, Circle.Height - 10);
            var smallC2 = new Rectangle(Circle.X + 4, Circle.Y + 4, Circle.Width - 8, Circle.Height - 8);

            using (var br = new SolidBrush(C))
            {
                G.FillEllipse(br, smallC1);
            }
            using (var br = new SolidBrush(Color.FromArgb(alpha, C)))
            {
                G.FillEllipse(br, smallC2);
            }
        }

    }

}