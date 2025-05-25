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
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
        }

        #region Variables
        private bool CanAnimate => !DesignMode && Program.Style.Animations && this != null && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        private Rectangle SideRect = new();

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
            get => _Value;
            set
            {
                if (value != _Value)
                {
                    if (value > Maximum) value = Maximum;
                    if (value < Minimum) value = Minimum;
                    _Value = value;
                    Invalidate();
                    ValueChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }


        private int _Max = 100;
        public int Maximum
        {
            get => _Max;
            set
            {
                if (value != _Max)
                {
                    _Max = value;
                    if (value < _Value) Value = value;
                    Invalidate();
                }
            }
        }


        private int _Min;
        public int Minimum
        {
            get => _Min;
            set
            {
                if (value != _Min)
                {
                    _Min = value;
                    if (value > _Value) Value = value;
                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = string.Empty;

        #endregion

        #region Events/Overrides

        public event ValueChangedEventHandler ValueChanged;

        public delegate void ValueChangedEventHandler(object sender, EventArgs e);

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                if (e.Delta <= -240)
                    Value = Math.Max(Minimum, Value - UpDownStep * 2);
                else
                    Value = Math.Max(Minimum, Value - UpDownStep);
            }
            else
            {
                if (e.Delta >= 240)
                    Value = Math.Min(Maximum, Value + UpDownStep * 2);
                else
                    Value = Math.Min(Maximum, Value + UpDownStep);
            }

            base.OnMouseWheel(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            State = MouseState.Over;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 255; }

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            State = MouseState.None;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }

            base.OnMouseLeave(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            State = MouseState.Over;


            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), ContainsFocus ? 255 : 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = ContainsFocus ? 255 : 0; }

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

            base.OnMouseUp(e);
        }

        protected override void OnResize(EventArgs e)
        {
            Invalidate();

            base.OnResize(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            State = MouseState.Down;

            if (Enabled & SideRect.Contains(e.Location))
            {
                if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
                else { alpha = 0; }
            }

            base.OnMouseDown(e);
        }

        int parentLevel = 0;
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            parentLevel = this.Level();
        }


        #endregion

        #region Animator
        private int _alpha = 0;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int alpha
        {
            get => _alpha;
            set { _alpha = value; Invalidate(); }
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
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;
            bool RTL = (int)RightToLeft == 1;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            // ################################################################################# Customizer
            Rectangle OuterRect = new(0, 0, Width - 1, Height - 1);
            Rectangle InnerRect = new(1, 1, Width - 3, Height - 3);
            SideRect = new(Width - 18, 0, 17, Height);

            if (RTL)
            {
                OuterRect.X = Width - OuterRect.X - OuterRect.Width;
                InnerRect.X = Width - InnerRect.X - InnerRect.Width;
                SideRect.X = Width - SideRect.X - SideRect.Width;
            }
            // #################################################################################

            using (SolidBrush br = new(scheme.Colors.Back(parentLevel)))
            {
                G.FillRoundedRect(br, InnerRect);
            }

            using (SolidBrush br = new(Color.FromArgb(alpha, scheme.Colors.Back_Checked))) { G.FillRoundedRect(br, OuterRect); }

            using (SolidBrush br = new(Color.FromArgb(alpha, scheme.Colors.Line_Checked_Hover))) { G.FillRoundedRect(br, SideRect); }

            using (Pen P = new(Color.FromArgb(255 - alpha, scheme.Colors.Line(parentLevel)))) { G.DrawRoundedRectBeveled(P, InnerRect); }

            using (Pen P = new(Color.FromArgb(alpha, scheme.Colors.Line_Checked_Hover))) { G.DrawRoundedRectBeveled(P, OuterRect); }

            using (SolidBrush SignBrush = new(Color.FromArgb(255 - alpha, scheme.Colors.Line_Checked_Hover)))
            {
                using (Font SignFont = new("Marlett", 11f))
                {
                    G.DrawString("t", SignFont, scheme.Brushes.Back_Checked, new Point(SideRect.Left, 0));
                    G.DrawString("u", SignFont, scheme.Brushes.Back_Checked, new Point(SideRect.Left, Height - 16));

                    G.DrawString("t", SignFont, SignBrush, new Point(SideRect.Left, 0));
                    G.DrawString("u", SignFont, SignBrush, new Point(SideRect.Left, Height - 16));
                }
            }

            using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
            {
                using (SolidBrush TextColor = new(ForeColor))
                {
                    G.DrawString($"{Value}", Fonts.ConsoleMedium, TextColor, new Rectangle(0, 1, Width - SideRect.Width, Height), sf);
                }
            }

            base.OnPaint(e);


        }
    }
}