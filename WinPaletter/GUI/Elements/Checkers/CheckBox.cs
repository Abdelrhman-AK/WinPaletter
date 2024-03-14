using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    [Description("Themed CheckBox for WinPaletter UI")]
    [DefaultEvent("CheckedChanged")]
    public class CheckBox : Control
    {
        public CheckBox()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | (ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);
            DoubleBuffered = true;
            BackColor = Color.Transparent;

            Font = new("Segoe UI", 9f);
            ForeColor = Color.White;

            alpha = 0; alpha2 = Checked ? 255 : 0;
        }

        #region Variables
        private bool CanAnimate => !DesignMode && Program.Style.Animations && this != null && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        public MouseState State = MouseState.None;

        public enum MouseState
        {
            None,
            Over,
            Down
        }

        #endregion

        #region Properties

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public new Color BackColor { get => Color.Transparent; set {; } }

        private bool _Checked;
        public bool Checked
        {
            get => _Checked;
            set
            {
                if (value != _Checked)
                {
                    _Checked = value;
                    CheckedChanged?.Invoke(this, new EventArgs());

                    if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha2), Checked ? 255 : 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
                    else { alpha2 = Checked ? 255 : 0; }
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; }

        #endregion

        #region Events/Overrides

        public event CheckedChangedEventHandler CheckedChanged;

        public delegate void CheckedChangedEventHandler(object sender, EventArgs e);

        protected override void OnMouseDown(MouseEventArgs e)
        {
            State = MouseState.Down;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }

            base.OnMouseDown(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            Checked = !Checked;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha2), Checked ? 255 : 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha2 = Checked ? 255 : 0; }

            base.OnMouseClick(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            State = MouseState.Over;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), ContainsFocus ? 255 : 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = ContainsFocus ? 255 : 0; }

            base.OnMouseUp(e);
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

        int parentLevel = 0;
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            parentLevel = this.Level();
        }


        #endregion

        #region Animator

        private int _alpha = 0;
        private int _alpha2 = 255;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int alpha
        {
            get => _alpha;
            set { _alpha = value; Invalidate(); }
        }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int alpha2
        {
            get => _alpha2;
            set { _alpha2 = value; Invalidate(); }
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

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            // ################################################################################# Customizer
            bool RTL = (int)RightToLeft == 1;
            StringFormat format = ContentAlignment.MiddleLeft.ToStringFormat(RTL);

            int maxHeight = Math.Min(25, Height);
            int OuterRectSize = maxHeight - 8;
            int InnerRectSize = maxHeight - 10;

            Rectangle OuterRect = new(3, (Height - OuterRectSize) / 2, OuterRectSize, OuterRectSize);
            Rectangle InnerRect = new(OuterRect.X + (OuterRect.Width - InnerRectSize) / 2, OuterRect.Y + (OuterRect.Height - InnerRectSize) / 2, InnerRectSize, InnerRectSize);
            Rectangle TextRect = new(maxHeight - 1, 1, Width - OuterRect.Width, Height - 1);

            Rectangle OuterRect_GradienceFix = new(OuterRect.X, OuterRect.Y, OuterRect.Width + 1, OuterRect.Height + 1);
            Rectangle InnerRect_GradienceFix = new(InnerRect.X, InnerRect.Y, InnerRect.Width + 1, InnerRect.Height + 1);

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            if (RTL)
            {
                format = new(StringFormatFlags.DirectionRightToLeft);
                OuterRect.X = Width - OuterRect.X - OuterRect.Width;
                InnerRect.X = Width - InnerRect.X - InnerRect.Width;
                TextRect.Width = Width - InnerRect.Width - 10;
                TextRect.X = 0;
            }

            #region Check sign x, y coordinates
            int x1_Left = InnerRect.X + 4;
            int y1_Left = (int)Math.Round(0.8d * InnerRect.Height);
            int x2_Left = x1_Left;
            int y2_Left = InnerRect.Y + InnerRect.Height - 3;

            int x1_Right = x2_Left;
            int y1_Right = y2_Left;
            int x2_Right = InnerRect.Right - 2;
            int y2_Right = y1_Left - 3;
            #endregion

            // #################################################################################

            using (LinearGradientBrush lgb0 = new(InnerRect_GradienceFix, scheme.Colors.Back(parentLevel), scheme.Colors.Back_Hover(parentLevel), LinearGradientMode.Horizontal))
            using (LinearGradientBrush lgb1 = new(OuterRect_GradienceFix, Color.FromArgb(Math.Max(255 - alpha, 0), scheme.Colors.Line_Hover(parentLevel)), Color.FromArgb(Math.Max(255 - alpha, 0), scheme.Colors.Line(parentLevel)), LinearGradientMode.Vertical))
            using (LinearGradientBrush lgb2 = new(OuterRect_GradienceFix, Color.FromArgb(alpha, scheme.Colors.Line_Checked_Hover), Color.FromArgb(alpha, scheme.Colors.Accent), LinearGradientMode.Horizontal))
            using (Pen P0 = new(lgb1))
            using (Pen P1 = new(lgb2))
            {
                G.FillRoundedRect(lgb0, InnerRect);
                G.DrawRoundedRect(P0, InnerRect);
                G.DrawRoundedRect(P1, OuterRect);
            }

            using (LinearGradientBrush lgb0 = new(OuterRect_GradienceFix, Color.FromArgb(alpha2, scheme.Colors.AccentAlt), Color.FromArgb(alpha2, scheme.Colors.Back_Checked_Hover), LinearGradientMode.ForwardDiagonal))
            using (LinearGradientBrush lgb1 = new(OuterRect_GradienceFix, Color.FromArgb(alpha2, scheme.Colors.AccentAlt), Color.FromArgb(alpha2, scheme.Colors.Line_Checked_Hover), LinearGradientMode.ForwardDiagonal))
            using (SolidBrush lgb3 = new(Color.FromArgb(alpha, scheme.Colors.AccentAlt)))
            using (Pen P = new(lgb1))
            {
                G.FillRoundedRect(lgb0, OuterRect);
                G.DrawRoundedRect(P, OuterRect);
                if (_Checked) G.FillRoundedRect(lgb3, OuterRect);

                using (Pen CheckSignPen = new(Color.FromArgb(alpha2, scheme.Colors.Back(parentLevel)), 1.9f))
                {
                    Point[] leftCheckPoints = new Point[] { new(x1_Left, y1_Left), new(x2_Left, y2_Left) };
                    Point[] rightCheckPoints = new Point[] { new(x1_Right, y1_Right), new(x2_Right, y2_Right) };

                    G.DrawCurve(CheckSignPen, leftCheckPoints);
                    G.DrawCurve(CheckSignPen, rightCheckPoints);
                }
            }

            #region Strings

            using (SolidBrush br = new(Color.FromArgb(255 - alpha2, ForeColor))) { G.DrawString(Text, Font, br, TextRect, format); }

            using (SolidBrush br = new(Color.FromArgb(alpha2, scheme.Colors.ForeColor_Accent))) { G.DrawString(Text, Font, br, TextRect, format); }

            #endregion

            format.Dispose();

            base.OnPaint(e);
        }
    }
}