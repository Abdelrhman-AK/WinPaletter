using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    [Description("Themed TextBox for WinPaletter UI")]
    [DefaultEvent("TextChanged")]
    public class TextBox : Control
    {
        public TextBox()
        {
            TB = new()
            {
                Font = new("Segoe UI", 9f),
                Text = Text,
                ForeColor = ForeColor,
                MaxLength = _MaxLength,
                Multiline = _Multiline,
                ReadOnly = _ReadOnly,
                UseSystemPasswordChar = _UseSystemPasswordChar,
                BorderStyle = BorderStyle.None,
                Cursor = Cursors.IBeam,
                ScrollBars = _Scrollbars,
                WordWrap = _WordWrap,
            };

            alpha = 0;

            SetTBSizes();

            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();

            BackColor = Color.Transparent;
            DoubleBuffered = true;
        }

        #region Variables

        private bool CanAnimate => !DesignMode && Program.Style.Animations && this != null && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        public System.Windows.Forms.TextBox TB
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get => tb;

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (tb != null)
                {
                    tb.MouseDown -= TB_MouseDown;
                    tb.MouseEnter -= TB_MouseEnter;
                    tb.MouseUp -= TB_MouseUp;
                    tb.MouseLeave -= TB_MouseLeave;
                    tb.LostFocus -= TB_LostFocus;
                    tb.TextChanged -= OnBaseTextChanged;
                    tb.KeyDown -= OnBaseKeyDown;
                    tb.KeyPress -= OnKeyPress;
                }

                tb = value;

                if (tb != null)
                {
                    tb.MouseDown += TB_MouseDown;
                    tb.MouseEnter += TB_MouseEnter;
                    tb.MouseUp += TB_MouseUp;
                    tb.MouseLeave += TB_MouseLeave;
                    tb.LostFocus += TB_LostFocus;
                    tb.TextChanged += OnBaseTextChanged;
                    tb.KeyDown += OnBaseKeyDown;
                    tb.KeyPress += OnKeyPress;
                }
            }
        }
        private System.Windows.Forms.TextBox tb;

        #endregion

        #region Properties

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override Color BackColor { get; set; }

        private HorizontalAlignment _TextAlign = HorizontalAlignment.Left;

        [Category("Options")]
        public HorizontalAlignment TextAlign
        {
            get => _TextAlign;
            set
            {
                if (value != _TextAlign)
                {
                    _TextAlign = value;
                    if (tb is not null) { tb.TextAlign = value; }
                }
            }
        }

        private int _MaxLength = 32767;

        [Category("Options")]
        public int MaxLength
        {
            get => _MaxLength;
            set
            {
                if (value != _MaxLength)
                {
                    _MaxLength = value;
                    if (tb is not null) { tb.MaxLength = value; }
                }
            }
        }

        private bool _ReadOnly;

        [Category("Options")]
        public bool ReadOnly
        {
            get => _ReadOnly;
            set
            {
                if (value != _ReadOnly)
                {
                    _ReadOnly = value;
                    if (tb is not null) { tb.ReadOnly = value; }
                }
            }
        }

        private bool _UseSystemPasswordChar;

        [Category("Options")]
        public bool UseSystemPasswordChar
        {
            get => _UseSystemPasswordChar;
            set
            {
                if (value != _UseSystemPasswordChar)
                {
                    _UseSystemPasswordChar = value;
                    if (tb is not null) { tb.UseSystemPasswordChar = value; }
                }
            }
        }

        private bool _Multiline;

        [Category("Options")]
        public bool Multiline
        {
            get => _Multiline;
            set
            {
                if (value != _Multiline)
                {
                    _Multiline = value;
                    if (TB is not null)
                    {
                        tb.Multiline = value;

                        if (value)
                        {
                            tb.Height = Height - 8;
                        }
                        else
                        {
                            Height = tb.Height + 8;
                        }
                    }
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        [Category("Options")]
        public override string Text
        {
            get => base.Text;
            set
            {
                if (value != base.Text)
                {
                    base.Text = value;
                    if (TB is not null) { tb.Text = value; }
                }
            }
        }

        [Category("Options")]
        public override Font Font
        {
            get => base.Font;
            set
            {
                if (value != base.Font)
                {
                    base.Font = value;
                    if (tb is not null)
                    {
                        tb.Font = value;
                        SetTBSizes();
                    }
                }
            }
        }

        private ScrollBars _Scrollbars = ScrollBars.None;
        public ScrollBars Scrollbars
        {
            get => _Scrollbars;
            set
            {
                if (value != _Scrollbars)
                {
                    _Scrollbars = value;
                    tb.ScrollBars = value;
                }
            }
        }

        private bool _WordWrap = true;
        public bool WordWrap
        {
            get => _WordWrap;
            set
            {
                if (value != _WordWrap)
                {
                    _WordWrap = value;
                    tb.WordWrap = value;
                }
            }
        }

        public int SelectionStart
        {
            get => tb.SelectionStart;
            set
            {
                if (value != tb.SelectionStart) { tb.SelectionStart = value; }
            }
        }

        public int SelectionLength
        {
            get => tb.SelectionLength;
            set
            {
                if (value != tb.SelectionLength) { tb.SelectionLength = value; }
            }
        }

        public string SelectedText
        {
            get => tb.SelectedText;
            set
            {
                if (value != tb.SelectedText) { tb.SelectedText = value; }
            }
        }

        #endregion

        #region Events/Overrides

        void SetTBSizes()
        {
            tb.Location = new(4, 4);
            tb.Width = Width - tb.Location.X * 2;

            if (_Multiline)
            {
                tb.Height = Height - 8;
            }
            else
            {
                Height = tb.Height + 8;
            }
        }

        public event KeyboardPressEventHandler KeyboardPress;

        public delegate void KeyboardPressEventHandler(object s, KeyPressEventArgs e);

        protected override void OnCreateControl()
        {
            if (!Controls.Contains(TB)) Controls.Add(TB);

            base.OnCreateControl();
        }

        private void OnBaseTextChanged(object s, EventArgs e)
        {
            Text = tb.Text;
        }

        private void OnBaseKeyDown(object s, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                tb.SelectAll();
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                tb.Copy();
                e.SuppressKeyPress = true;
            }
        }

        public void OnKeyPress(object s, KeyPressEventArgs e)
        {
            KeyboardPress?.Invoke(s, e);
        }

        protected override void OnResize(EventArgs e)
        {
            SetTBSizes();

            base.OnResize(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 255; }

            tb.Focus();

            base.OnMouseUp(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 255; }

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }

            base.OnMouseLeave(e);
        }

        private void TB_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        private void TB_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void TB_MouseUp(object sender, MouseEventArgs e)
        {
            OnMouseUp(e);
        }

        private void TB_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void TB_LostFocus(object sender, EventArgs e)
        {
            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            tb.ForeColor = ForeColor;

            base.OnForeColorChanged(e);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            tb?.Dispose();
            Font?.Dispose();
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

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = Program.Style.RenderingHint;

            Rectangle OuterRect = new(0, 0, Width - 1, Height - 1);
            Rectangle InnerRect = new(1, 1, Width - 3, Height - 3);

            Color Line = scheme.Colors.Line_Checked;
            Color LineHover = Parent != null && Parent is not WP.GroupBox ? scheme.Colors.Back_Hover : scheme.Colors.Back_Hover_Level2;
            Color FadeInColor = Color.FromArgb(alpha, Line);
            Color FadeOutColor = Color.FromArgb(255 - alpha, LineHover);

            if (tb.Focused | Focused)
            {
                G.FillRoundedRect(scheme.Brushes.Back_Checked, OuterRect);

                G.DrawRoundedRect_LikeW11(scheme.Pens.Line_Checked_Hover, OuterRect);

                tb.BackColor = scheme.Colors.Back_Checked;
            }

            else
            {
                G.FillRoundedRect(Parent is not WP.GroupBox ? scheme.Brushes.Back : scheme.Brushes.Back_Level2, InnerRect);

                using (SolidBrush br = new(Color.FromArgb(alpha, scheme.Colors.Back_Level2))) { G.FillRoundedRect(br, OuterRect); }

                using (Pen P = new(FadeInColor)) { G.DrawRoundedRect_LikeW11(P, OuterRect); }

                using (Pen P = new(FadeOutColor)) { G.DrawRoundedRect_LikeW11(P, InnerRect); }

                tb.BackColor = scheme.Colors.Back_Level2;
            }
        }
    }
}