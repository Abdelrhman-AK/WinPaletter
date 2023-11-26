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
                try
                {
                    if (_Checked != value)
                    {
                        _Checked = value;
                        if (_Checked) { UncheckOthersOnChecked(); }
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
                catch { }
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
            Checked = true;
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

        #region Voids/Functions

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

            Rectangle OuterCircle = new(3, 4, Height - 8, Height - 8);
            Rectangle InnerCircle = new(4, 5, Height - 10, Height - 10);
            Rectangle CheckCircle = new(7, 8, Height - 16, Height - 16);
            Rectangle TextRect = new(Height - 1, 1, Width - OuterCircle.Width, Height - 1);

            if (RTL)
            {
                format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                OuterCircle.X = Width - OuterCircle.X - OuterCircle.Width;
                InnerCircle.X = Width - InnerCircle.X - InnerCircle.Width;
                CheckCircle.X = Width - CheckCircle.X - CheckCircle.Width;
                TextRect.Width -= OuterCircle.Width + 13;
            }

            #region Colors System
            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            Color BackCircle_Color = scheme.Colors.Back;
            Color BackCircle_LineColor = Color.FromArgb(255 - alpha, scheme.Colors.Back_Hover);

            Color BackCircle_Color_Hover = Color.FromArgb(alpha, scheme.Colors.Back_Hover);
            Color BackCircle_LineColor_Hover = Color.FromArgb(alpha, scheme.Colors.Line_Hover);

            Color Checked_Circle_Color = Color.FromArgb(alpha2, scheme.Colors.Back_Checked);
            Color Checked_Circle_Color_Hover = Color.FromArgb(alpha, scheme.Colors.Line_Checked_Hover);

            Color Checked_Dot_Color = Color.FromArgb(alpha2, scheme.Colors.AccentAlt);
            #endregion
            // #################################################################################

            using (SolidBrush br = new(BackCircle_Color)) { G.FillEllipse(br, InnerCircle); }

            using (SolidBrush br = new(Checked_Circle_Color)) { G.FillEllipse(br, OuterCircle); }

            if (_Checked)
            {
                using (SolidBrush br = new(Checked_Circle_Color_Hover)) { G.FillEllipse(br, OuterCircle); }
                using (Pen P = new(Color.FromArgb(255, Checked_Circle_Color_Hover))) { G.DrawEllipse(P, OuterCircle); }
            }
            else
            {
                using (Pen P = new(BackCircle_LineColor)) { G.DrawEllipse(P, InnerCircle); }
                using (SolidBrush br = new(BackCircle_Color_Hover)) { G.FillEllipse(br, OuterCircle); }
                using (Pen P = new(BackCircle_LineColor_Hover)) { G.DrawEllipse(P, OuterCircle); }
            }

            using (SolidBrush br = new(Checked_Dot_Color)) { G.FillEllipse(br, CheckCircle); }

            #region Strings
            using (SolidBrush br = new(Color.FromArgb(255 - alpha2, ForeColor))) { G.DrawString(Text, Font, br, TextRect, format); }

            using (SolidBrush br = new(Checked_Dot_Color)) { G.DrawString(Text, Font, br, TextRect, format); }
            #endregion

            format.Dispose();
        }
    }
}