using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{

    [Description("Themed NumericUpDown for WinPaletter UI")]
    public class NumericUpDown : Control
    {

        public NumericUpDown()
        {
            Timer = new Timer() { Enabled = false, Interval = 1 };
            Font = new Font("Segoe UI", 9f);
            Enabled = true;
            DoubleBuffered = true;
            MouseDown += NumericUpDown_MouseDown;
            HandleCreated += NumericUpDown_HandleCreated;
            HandleDestroyed += NumericUpDown_HandleDestroyed;
            Timer.Tick += Timer_Tick;
        }

        #region Variables

        private bool _Shown = false;
        private Rectangle SideRect = new Rectangle();

        private MouseState State = MouseState.None;

        public enum MouseState
        {
            None,
            Over,
            Down
        }

        #endregion

        #region Properties

        public int UpDownStep { get; set; } = 1;

        private int _Value;
        public int Value
        {
            get
            {
                return _Value;
            }
            set
            {
                switch (value)
                {
                    case var @case when @case > Max:
                        {
                            value = Max;
                            Invalidate();
                            break;
                        }

                    case var case1 when case1 < Min:
                        {
                            value = Min;
                            Invalidate();
                            break;
                        }
                }
                _Value = value;
                Invalidate();
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private int _Max = 100;
        public int Max
        {
            get
            {
                return _Max;
            }
            set
            {
                switch (value)
                {
                    case var @case when @case < _Value:
                        {
                            _Value = value;
                            break;
                        }
                }
                _Max = value;
                Invalidate();
            }
        }

        private int _Min;
        public int Min
        {
            get
            {
                return _Min;
            }
            set
            {
                switch (value)
                {
                    case var @case when @case > _Value:
                        {
                            _Value = value;
                            break;
                        }
                }
                _Min = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = "";

        #endregion

        #region Animator

        private int alpha;
        private readonly int Factor = 20;
        private Timer Timer;

        private void Timer_Tick(object sender, EventArgs e)
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

        #endregion

        #region Events

        public event ValueChangedEventHandler ValueChanged;

        public delegate void ValueChangedEventHandler(object sender, EventArgs e);

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseState.Over;
            _Shown = true;
            Timer.Enabled = true;
            Timer.Start();
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            State = MouseState.None;
            _Shown = true;
            Timer.Enabled = true;
            Timer.Start();
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            State = MouseState.Over;
            _Shown = true;
            Timer.Enabled = true;
            Timer.Start();

            base.OnMouseUp(e);

            if (Enabled)
            {
                if (SideRect.Contains(e.Location) & e.Y < 10)
                {
                    Value += UpDownStep;
                }
                else if (SideRect.Contains(e.Location) & e.Y > 10)
                {
                    Value -= UpDownStep;
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            Invalidate();
        }

        private void NumericUpDown_MouseDown(object sender, MouseEventArgs e)
        {
            State = MouseState.Down;
            _Shown = true;

            if (Enabled & SideRect.Contains(e.Location))
            {
                Timer.Enabled = true;
                Timer.Start();
            }
        }

        private void NumericUpDown_HandleCreated(object sender, EventArgs e)
        {
            alpha = 0;

            if (!DesignMode)
            {
                try
                {
                    FindForm().Load += Loaded;
                    FindForm().Shown += Showed;
                    Parent.BackColorChanged += RefreshColorPalette;
                }
                catch
                {
                }
            }
        }

        private void NumericUpDown_HandleDestroyed(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                try
                {
                    FindForm().Load -= Loaded;
                    FindForm().Shown -= Showed;
                    Parent.BackColorChanged -= RefreshColorPalette;
                }
                catch
                {
                }
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
            if (_Shown)
            {
                Invalidate();
            }
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : TextRenderingHint.SystemDefault;
            DoubleBuffered = true;
            bool RTL = (int)RightToLeft == 1;

            // ################################################################################# Customizer
            var OuterRect = new Rectangle(0, 0, Width - 1, Height - 1);
            var InnerRect = new Rectangle(1, 1, Width - 3, Height - 3);
            SideRect = new Rectangle(Width - 16, 0, 15, Height);

            if (RTL)
            {
                OuterRect.X = Width - OuterRect.X - OuterRect.Width;
                InnerRect.X = Width - InnerRect.X - InnerRect.Width;
                SideRect.X = Width - SideRect.X - SideRect.Width;
            }

            // #################################################################################

            G.Clear(this.GetParentColor());

            using (var br = new SolidBrush(Color.FromArgb(255 - alpha, Program.Style.Colors.Back)))
            {
                G.FillRoundedRect(br, OuterRect);
            }
            using (var br = new SolidBrush(Color.FromArgb(alpha, Program.Style.Colors.Back_Checked)))
            {
                G.FillRoundedRect(br, OuterRect);
            }
            using (var br = new SolidBrush(Color.FromArgb(alpha, Program.Style.Colors.Border_Checked_Hover)))
            {
                G.FillRoundedRect(br, SideRect);
            }

            using (var P = new Pen(Color.FromArgb(255 - alpha, Program.Style.Colors.Border)))
            {
                G.DrawRoundedRect_LikeW11(P, InnerRect);
            }
            using (var P = new Pen(Color.FromArgb(alpha, Program.Style.Colors.Border_Checked_Hover)))
            {
                G.DrawRoundedRect_LikeW11(P, OuterRect);
            }

            if (Focused & State == MouseState.None)
            {
                using (var P = new Pen(Color.FromArgb(255, Program.Style.Colors.Border_Checked_Hover)))
                {
                    G.DrawRoundedRect(P, InnerRect);
                }
            }

            using (var TextColor = new SolidBrush(Program.Style.DarkMode ? Color.White : Color.Black))
            {
                G.DrawString(Value.ToString(), Font, TextColor, new Rectangle(0, 0, Width - 15, Height), ContentAlignment.MiddleCenter.ToStringFormat());
            }

            using (var SignColor = new SolidBrush(Program.Style.Colors.Back_Checked))
            using (var SignFont = new Font("Marlett", 11f))
            {
                G.DrawString("t", SignFont, SignColor, new Point(SideRect.Left - 1, 0));
                G.DrawString("u", SignFont, SignColor, new Point(SideRect.Left - 1, Height - 16));
            }
        }

    }

}