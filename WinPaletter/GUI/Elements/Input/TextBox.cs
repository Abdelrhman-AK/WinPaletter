using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
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
                Location = new(1, 0),
                Width = Width,
                Cursor = Cursors.IBeam,
                ScrollBars = Scrollbars,
                WordWrap = WordWrap,
                Height = _Multiline ? Height - 8 : Height + 8
            };

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();

            BackColor = Color.Transparent;
            DoubleBuffered = true;
        }

        #region Variables
        private bool CanAnimate => !DesignMode && Program.Style.Animations && this != null && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        public System.Windows.Forms.TextBox _TB;

        private System.Windows.Forms.TextBox TB
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _TB;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_TB != null)
                {
                    _TB.MouseDown -= TB_MouseDown;
                    _TB.MouseEnter -= TB_MouseEnter;
                    _TB.MouseUp -= TB_MouseEnter;
                    _TB.MouseLeave -= TB_MouseLeave;
                    _TB.LostFocus -= TB_LostFocus;
                }

                _TB = value;
                if (_TB != null)
                {
                    _TB.MouseDown += TB_MouseDown;
                    _TB.MouseEnter += TB_MouseEnter;
                    _TB.MouseUp += TB_MouseEnter;
                    _TB.MouseLeave += TB_MouseLeave;
                    _TB.LostFocus += TB_LostFocus;
                }
            }
        }

        private MouseState State = MouseState.None;

        public enum MouseState : byte
        {
            None = 0,
            Over = 1,
            Down = 2,
            Block = 3
        }

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
                    if (TB is not null) { TB.TextAlign = value; }
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
                    if (TB is not null) { TB.MaxLength = value; }
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
                    if (TB is not null) { TB.ReadOnly = value; }
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
                    if (TB is not null) { TB.UseSystemPasswordChar = value; }
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
                        TB.Multiline = value;

                        if (value)
                        {
                            TB.Height = Height - 8;
                        }
                        else
                        {
                            Height = TB.Height + 8;
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
                    if (TB is not null) { TB.Text = value; }
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
                    if (TB is not null)
                    {
                        TB.Font = value;
                        TB.Location = new(3, 4);
                        TB.Width = Width;

                        if (!_Multiline)
                        {
                            Height = TB.Height + 8;
                        }
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
                    TB.ScrollBars = value;
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
                    TB.WordWrap = value;
                }
            }
        }

        public int SelectionStart
        {
            get => TB.SelectionStart;
            set
            {
                if (value != TB.SelectionStart) { TB.SelectionStart = value; }
            }
        }

        public int SelectionLength
        {
            get => TB.SelectionLength;
            set
            {
                if (value != TB.SelectionLength) { TB.SelectionLength = value; }
            }
        }

        public string SelectedText
        {
            get => TB.SelectedText;
            set
            {
                if (value != TB.SelectedText) { TB.SelectedText = value; }
            }
        }

        #endregion

        #region Events

        public event KeyboardPressEventHandler KeyboardPress;

        public delegate void KeyboardPressEventHandler(object s, KeyPressEventArgs e);

        protected override void OnCreateControl()
        {
            if (!Controls.Contains(TB))
            {
                Controls.Add(TB);
            }

            base.OnCreateControl();
        }

        private void OnBaseTextChanged(object s, EventArgs e)
        {
            Text = TB.Text;
        }

        private void OnBaseKeyDown(object s, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                TB.SelectAll();
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                TB.Copy();
                e.SuppressKeyPress = true;
            }
        }

        public void OnKeyPress(object s, KeyPressEventArgs e)
        {
            KeyboardPress?.Invoke(s, e);
        }

        protected override void OnResize(EventArgs e)
        {
            TB.Location = new(4, 4);
            TB.Width = Width - TB.Location.X * 2;

            if (_Multiline)
            {
                TB.Height = Height - 8;
            }
            else
            {
                Height = TB.Height + 8;
            }

            base.OnResize(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            State = MouseState.Down;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            State = MouseState.Over;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 255; }

            TB.Focus();

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

        private void TB_MouseDown(object sender, MouseEventArgs e)
        {
            State = MouseState.Down;


            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }
        }

        private void TB_MouseEnter(object sender, EventArgs e)
        {
            State = MouseState.Over;


            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 255; }
        }

        private void TB_MouseLeave(object sender, EventArgs e)
        {
            State = MouseState.None;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }
        }

        private void TB_LostFocus(object sender, EventArgs e)
        {
            State = MouseState.None;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            alpha = 0;

            if (!DesignMode)
            {
                try
                {
                    TB.TextChanged += OnBaseTextChanged;
                    TB.KeyDown += OnBaseKeyDown;
                    TB.KeyPress += OnKeyPress;
                }
                catch { }
            }

            base.OnHandleCreated(e);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (!DesignMode)
            {
                try
                {
                    TB.TextChanged -= OnBaseTextChanged;
                    TB.KeyDown -= OnBaseKeyDown;
                    TB.KeyPress -= OnKeyPress;
                }
                catch { }
            }

            base.OnHandleDestroyed(e);
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            TB.ForeColor = ForeColor;
            Refresh();

            base.OnForeColorChanged(e);
        }

        #endregion

        #region Animator
        private int _alpha = 0;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int alpha
        {
            get => _alpha;
            set { _alpha = value; Refresh(); }
        }
        #endregion

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //Leave it empty to make control background transparent
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this == null) return;

            Graphics G = e.Graphics;
            DoubleBuffered = true;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = TextRenderingHint.SystemDefault;

            Rectangle OuterRect = new(0, 0, Width - 1, Height - 1);
            Rectangle InnerRect = new(1, 1, Width - 3, Height - 3);

            Color Line = scheme.Colors.Line_Checked;
            Color LineHover = scheme.Colors.Back_Hover;
            Color FadeInColor = Color.FromArgb(alpha, Line);
            Color FadeOutColor = Color.FromArgb(255 - alpha, LineHover);

            if (TB.Focused | Focused)
            {
                G.FillRoundedRect(scheme.Brushes.Back_Checked, OuterRect);

                G.DrawRoundedRect_LikeW11(scheme.Pens.Line_Checked_Hover, OuterRect);

                _TB.BackColor = scheme.Colors.Back_Checked;
            }

            else
            {
                G.FillRoundedRect(scheme.Brushes.Back, InnerRect);

                using (SolidBrush br = new(scheme.Colors.Back)) { G.FillRoundedRect(br, InnerRect); }

                using (SolidBrush br = new(Color.FromArgb(alpha, scheme.Colors.Back))) { G.FillRoundedRect(br, OuterRect); }

                using (Pen P = new(FadeInColor)) { G.DrawRoundedRect_LikeW11(P, OuterRect); }

                using (Pen P = new(FadeOutColor)) { G.DrawRoundedRect_LikeW11(P, InnerRect); }

                _TB.BackColor = scheme.Colors.Back;
            }

            base.OnPaint(e);
        }
    }
}