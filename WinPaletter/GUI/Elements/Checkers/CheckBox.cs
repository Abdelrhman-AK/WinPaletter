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

            Font = new Font("Segoe UI", 9f);
            ForeColor = Color.White;

            _shown = false;
            _alpha = 0;
            _alpha2 = Checked ? 255 : 0;
        }

        #region Variables
        private bool _shown = false;

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
            get { return _Checked; }
            set
            {
                if (_Checked != value)
                {
                    _Checked = value;
                    CheckedChanged?.Invoke(this);
                    if (_shown)
                    {
                        FluentTransitions.Transition.With(this, nameof(alpha2), Checked ? 255 : 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                    }
                    else
                    {
                        alpha2 = Checked ? 255 : 0;
                        Refresh();
                    }
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; }

        protected override CreateParams CreateParams
        {
            get
            {
                var cpar = base.CreateParams;
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

        public event CheckedChangedEventHandler CheckedChanged;

        public delegate void CheckedChangedEventHandler(object sender);

        protected override void OnMouseDown(MouseEventArgs e)
        {
            State = MouseState.Down;
            FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            Invalidate();

            base.OnMouseDown(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            Checked = !Checked;
            FluentTransitions.Transition.With(this, nameof(alpha2), Checked ? 255 : 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));

            base.OnMouseClick(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            State = MouseState.Over;
            FluentTransitions.Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            Invalidate();

            base.OnMouseUp(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            State = MouseState.Over;
            FluentTransitions.Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            Invalidate();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            State = MouseState.None;
            FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            Invalidate();

            base.OnMouseLeave(e);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            try { if (!DesignMode) { FindForm().Shown += Showed; } }
            catch { }

            try { alpha = 0; alpha2 = Checked ? 255 : 0; }
            catch { }

            base.OnHandleCreated(e);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            try { if (!DesignMode) { FindForm().Shown -= Showed; } }
            catch { }

            base.OnHandleDestroyed(e);
        }

        public void Showed(object sender, EventArgs e) 
        {
            _shown = true;
            Invalidate();
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Parent is null) return;

            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : TextRenderingHint.SystemDefault;
            DoubleBuffered = true;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            // ################################################################################# Customizer
            bool RTL = (int)RightToLeft == 1;
            StringFormat format = ContentAlignment.MiddleLeft.ToStringFormat(RTL);

            var OuterRect = new Rectangle(3, 4, Height - 8, Height - 8);
            var InnerRect = new Rectangle(4, 5, Height - 10, Height - 10);
            var TextRect = new Rectangle(Height - 1, 1, Width - InnerRect.Width, Height - 1);

            #region Colors System
            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            Color BackRect_Color = scheme.Colors.Back;
            Color BackRect_LineColor = Color.FromArgb(255 - alpha, scheme.Colors.Back_Hover);

            Color BackRect_Color_Hover = Color.FromArgb(alpha, scheme.Colors.Back_Hover);
            Color BackRect_LineColor_Hover = Color.FromArgb(alpha, scheme.Colors.Line_Hover);

            Color Checked_Rect_Color = Color.FromArgb(alpha2, scheme.Colors.Back_Checked);
            Color Checked_Rect_Color_Hover = Color.FromArgb(alpha, scheme.Colors.Line_Checked_Hover);

            Color Checked_Dot_Color = Color.FromArgb(alpha2, scheme.Colors.AccentAlt);
            #endregion

            if (RTL)
            {
                format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
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
                using (Pen P = new(Color.FromArgb(255, Checked_Rect_Color_Hover))) { G.DrawRoundedRect(P, OuterRect); }
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
        }
    }
}