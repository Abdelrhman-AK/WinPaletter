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
            Checked = !Checked;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha2), Checked ? 255 : 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha2 = Checked ? 255 : 0; }

            base.OnMouseClick(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            State = MouseState.Over;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 255; }

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

            Rectangle OuterRect = new(3, 4, Height - 8, Height - 8);
            Rectangle InnerRect = new(4, 5, Height - 10, Height - 10);
            Rectangle TextRect = new(Height - 1, 1, Width - InnerRect.Width, Height - 1);

            #region Colors System
            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            Color BackRect_Color = scheme.Colors.Back;
            Color BackRect_LineColor = Color.FromArgb(Math.Max(FocusAlpha - alpha, 0), scheme.Colors.Back_Hover);

            Color BackRect_Color_Hover = Color.FromArgb(alpha, scheme.Colors.Back_Hover);
            Color BackRect_LineColor_Hover = Color.FromArgb(alpha, scheme.Colors.Line_Hover);

            Color Checked_Rect_Color = Color.FromArgb(alpha2, scheme.Colors.Back_Checked);
            Color Checked_Rect_Color_Hover = Color.FromArgb(alpha, scheme.Colors.Line_Checked_Hover);

            Color Checked_Dot_Color = Color.FromArgb(alpha2, scheme.Colors.AccentAlt);
            #endregion

            if (RTL)
            {
                format = new(StringFormatFlags.DirectionRightToLeft);
                OuterRect.X = Width - OuterRect.X - OuterRect.Width;
                InnerRect.X = Width - InnerRect.X - InnerRect.Width;
                TextRect.Width = Width - InnerRect.Width - 10;
                TextRect.X = 0;
            }

            #region Check Sign x,y system
            int x1_Left = InnerRect.X + 3;
            int y1_Left = (int)Math.Round(0.8d * InnerRect.Height);
            int x2_Left = x1_Left;
            int y2_Left = InnerRect.Y + InnerRect.Height - 3;

            int x1_Right = x2_Left;
            int y1_Right = y2_Left;
            int x2_Right = InnerRect.Right - 2;
            int y2_Right = y1_Left - 3;
            #endregion

            // #################################################################################

            using (SolidBrush br = new(BackRect_Color)) { G.FillRoundedRect(br, InnerRect); }
            using (SolidBrush br = new(Checked_Rect_Color)) { G.FillRoundedRect(br, OuterRect); }

            if (_Checked)
            {
                using (SolidBrush br = new(Checked_Rect_Color_Hover)) { G.FillRoundedRect(br, OuterRect); }
                using (Pen P = new(Color.FromArgb(FocusAlpha, Checked_Rect_Color_Hover))) { G.DrawRoundedRect(P, OuterRect); }
            }
            else
            {
                using (Pen P = new(BackRect_LineColor)) { G.DrawRoundedRect(P, InnerRect); }
                using (SolidBrush br = new(BackRect_Color_Hover)) { G.FillRoundedRect(br, OuterRect); }
                using (Pen P = new(BackRect_LineColor_Hover)) { G.DrawRoundedRect(P, OuterRect); }
            }

            using (Pen CheckSignPen = new(Checked_Dot_Color, 1.8f))
            {
                G.DrawLine(CheckSignPen, x1_Left, y1_Left, x2_Left, y2_Left);
                G.DrawLine(CheckSignPen, x1_Right, y1_Right, x2_Right, y2_Right);
            }

            #region Strings
            using (SolidBrush br = new(Color.FromArgb(255 - alpha2, ForeColor))) { G.DrawString(Text, Font, br, TextRect, format); }

            using (SolidBrush br = new(Checked_Dot_Color)) { G.DrawString(Text, Font, br, TextRect, format); }
            #endregion

            format.Dispose();

            base.OnPaint(e);
        }
    }
}