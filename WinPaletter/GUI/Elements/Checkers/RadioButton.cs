using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    [Description("Themed RadioButton for WinPaletter UI")]
    [DefaultEvent("CheckedChanged")]
    public class RadioButton : Control
    {
        public RadioButton()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | (ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);
            DoubleBuffered = true;
            BackColor = Color.Transparent;

            Font = new("Segoe UI", 9f);
            ForeColor = Color.White;

            _alpha = 0;
            _alpha2 = Checked ? 255 : 0;
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
                if (_Checked != value)
                {
                    _Checked = value;
                    if (_Checked) { UncheckOthersOnChecked(); }
                    CheckedChanged?.Invoke(this, new EventArgs());

                    if (CanAnimate)
                    {
                        FluentTransitions.Transition.With(this, nameof(alpha2), Checked ? 255 : 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                    }
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


        private int _focusAlpha = 255;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int FocusAlpha
        {
            get => _focusAlpha;
            set
            {
                _focusAlpha = value;
                Refresh();
            }
        }

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
            Checked = true;

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

        protected override void OnHandleCreated(EventArgs e)
        {
            if (FindForm() != null)
            {
                FindForm().Activated += Form_Activated;
                FindForm().Deactivate += Form_Deactivate; ;
            }

            base.OnHandleCreated(e);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (FindForm() != null)
            {
                FindForm().Activated -= Form_Activated;
                FindForm().Deactivate -= Form_Deactivate; ;
            }

            base.OnHandleDestroyed(e);
        }

        private void Form_Activated(object sender, EventArgs e)
        {
            if (CanAnimate)
            {
                FluentTransitions.Transition.With(this, nameof(FocusAlpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            }
            else
            {
                FocusAlpha = 255;
            }
        }

        private void Form_Deactivate(object sender, EventArgs e)
        {
            if (CanAnimate)
            {
                FluentTransitions.Transition.With(this, nameof(FocusAlpha), 100).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            }
            else
            {
                FocusAlpha = 100;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (FindForm() != null)
            {
                FindForm().Activated -= Form_Activated;
                FindForm().Deactivate -= Form_Deactivate; ;
            }
        }

        int parentLevel = 0;
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            parentLevel = this.Level();
        }


        #endregion

        #region Methods

        private void UncheckOthersOnChecked()
        {
            if (Parent is null)
                return;

            foreach (Control C in Parent.Controls)
            {
                if (C != this && C is RadioButton)
                {
                    ((RadioButton)C).Checked = false;
                }
            }
        }

        #endregion

        #region Animator

        private int _alpha = 0;
        private int _alpha2 = 255;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int alpha
        {
            get => _alpha;
            set { _alpha = value; Refresh(); }
        }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int alpha2
        {
            get => _alpha2;
            set { _alpha2 = value; Refresh(); }
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
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : TextRenderingHint.SystemDefault;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            // ################################################################################# Customizer
            bool RTL = (int)RightToLeft == 1;
            StringFormat format = ContentAlignment.MiddleLeft.ToStringFormat(RTL);

            int maxHeight = Math.Min(25, Height);
            int OuterCircleSize = maxHeight - 8;
            int InnerCircleSize = maxHeight - 10;
            int CheckCircleSize = maxHeight - 16;

            Rectangle OuterCircle = new(3, (Height - OuterCircleSize) / 2, OuterCircleSize, OuterCircleSize);
            Rectangle InnerCircle = new(OuterCircle.X + (OuterCircle.Width - InnerCircleSize) / 2, OuterCircle.Y + (OuterCircle.Height - InnerCircleSize) / 2, InnerCircleSize, InnerCircleSize);
            Rectangle CheckCircle = new(OuterCircle.X + (OuterCircle.Width - CheckCircleSize) / 2, OuterCircle.Y + (OuterCircle.Height - CheckCircleSize) / 2, CheckCircleSize, CheckCircleSize);
            Rectangle TextRect = new(maxHeight - 1, 1, Width - OuterCircle.Width, Height - 1);

            Rectangle OuterCircle_GradienceFix = new(OuterCircle.X, OuterCircle.Y, OuterCircle.Width + 1, OuterCircle.Height + 1);
            Rectangle InnerCircle_GradienceFix = new(InnerCircle.X, InnerCircle.Y, InnerCircle.Width + 1, InnerCircle.Height + 1);

            if (RTL)
            {
                format = new(StringFormatFlags.DirectionRightToLeft);
                OuterCircle.X = Width - OuterCircle.X - OuterCircle.Width;
                InnerCircle.X = Width - InnerCircle.X - InnerCircle.Width;
                CheckCircle.X = Width - CheckCircle.X - CheckCircle.Width;
                TextRect.Width -= OuterCircle.Width + 13;
            }

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            // #################################################################################

            using (LinearGradientBrush lgb0 = new(InnerCircle_GradienceFix, scheme.Colors.Back(parentLevel), scheme.Colors.Back_Hover(parentLevel), LinearGradientMode.Horizontal))
            using (LinearGradientBrush lgb1 = new(OuterCircle_GradienceFix, Color.FromArgb(Math.Max(FocusAlpha - alpha, 0), scheme.Colors.Line_Hover(parentLevel)), Color.FromArgb(Math.Max(FocusAlpha - alpha, 0), scheme.Colors.Line(parentLevel)), LinearGradientMode.Vertical))
            using (LinearGradientBrush lgb2 = new(OuterCircle_GradienceFix, Color.FromArgb(alpha, scheme.Colors.Line_Checked_Hover), Color.FromArgb(alpha, scheme.Colors.AccentAlt), LinearGradientMode.Horizontal))
            using (Pen P0 = new(lgb1))
            using (Pen P1 = new(lgb2))
            {
                G.FillEllipse(lgb0, InnerCircle);
                G.DrawEllipse(P0, InnerCircle);
                G.DrawEllipse(P1, OuterCircle);
            }

            using (LinearGradientBrush lgb0 = new(OuterCircle_GradienceFix, Color.FromArgb(alpha2, scheme.Colors.AccentAlt), Color.FromArgb(alpha2, scheme.Colors.Back_Checked_Hover), LinearGradientMode.ForwardDiagonal))
            using (LinearGradientBrush lgb1 = new(OuterCircle_GradienceFix, Color.FromArgb(alpha2, scheme.Colors.AccentAlt), Color.FromArgb(alpha2, scheme.Colors.Line_Checked_Hover), LinearGradientMode.ForwardDiagonal))
            using (SolidBrush br = new(Color.FromArgb(alpha2, scheme.Colors.Back(parentLevel))))
            using (SolidBrush lgb3 = new(Color.FromArgb(alpha, scheme.Colors.AccentAlt)))
            using (Pen P = new(lgb1))
            {
                G.FillEllipse(lgb0, OuterCircle);
                G.DrawEllipse(P, OuterCircle);
                if (_Checked) G.FillEllipse(lgb3, OuterCircle);
                G.FillEllipse(br, CheckCircle);
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