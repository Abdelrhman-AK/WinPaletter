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
            Timer = new Timer() { Enabled = false, Interval = 1 };

            TB = new System.Windows.Forms.TextBox()
            {
                Font = new Font("Segoe UI", 9f),
                Text = Text,
                ForeColor = ForeColor,
                MaxLength = _MaxLength,
                Multiline = _Multiline,
                ReadOnly = _ReadOnly,
                UseSystemPasswordChar = _UseSystemPasswordChar,
                BorderStyle = BorderStyle.None,
                Location = new Point(1, 0),
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

            HandleCreated += TextBox_HandleCreated;
            HandleDestroyed += TextBox_HandleDestroyed;
            Timer.Tick += Timer_Tick;
            ForeColorChanged += TextBox_ForeColorChanged;
        }

        #region Variables

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
        private bool _Shown = false;

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
            get
            {
                return _TextAlign;
            }
            set
            {
                _TextAlign = value;
                if (TB is not null)
                {
                    TB.TextAlign = value;
                }
            }
        }

        private int _MaxLength = 32767;

        [Category("Options")]
        public int MaxLength
        {
            get
            {
                return _MaxLength;
            }
            set
            {
                _MaxLength = value;
                if (TB is not null)
                {
                    TB.MaxLength = value;
                }
            }
        }

        private bool _ReadOnly;

        [Category("Options")]
        public bool ReadOnly
        {
            get
            {
                return _ReadOnly;
            }
            set
            {
                _ReadOnly = value;
                if (TB is not null)
                {
                    TB.ReadOnly = value;
                }
            }
        }

        private bool _UseSystemPasswordChar;

        [Category("Options")]
        public bool UseSystemPasswordChar
        {
            get
            {
                return _UseSystemPasswordChar;
            }
            set
            {
                _UseSystemPasswordChar = value;
                if (TB is not null)
                {
                    TB.UseSystemPasswordChar = value;
                }
            }
        }

        private bool _Multiline;

        [Category("Options")]
        public bool Multiline
        {
            get
            {
                return _Multiline;
            }
            set
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

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        [Category("Options")]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                if (TB is not null)
                {
                    TB.Text = value;
                }
            }
        }

        [Category("Options")]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                if (TB is not null)
                {
                    TB.Font = value;
                    TB.Location = new Point(3, 4);
                    TB.Width = Width;

                    if (!_Multiline)
                    {
                        Height = TB.Height + 8;
                    }
                }
            }
        }

        private ScrollBars _Scrollbars = ScrollBars.None;
        public ScrollBars Scrollbars
        {
            get
            {
                return _Scrollbars;
            }
            set
            {
                _Scrollbars = value;
                TB.ScrollBars = value;
            }
        }

        private bool _WordWrap = true;
        public bool WordWrap
        {
            get
            {
                return _WordWrap;
            }
            set
            {
                _WordWrap = value;
                TB.WordWrap = value;
            }
        }

        public int SelectionStart
        {
            get
            {
                return TB.SelectionStart;
            }
            set
            {
                TB.SelectionStart = value;
            }
        }

        public int SelectionLength
        {
            get
            {
                return TB.SelectionLength;
            }
            set
            {
                TB.SelectionLength = value;
            }
        }

        public string SelectedText
        {
            get
            {
                return TB.SelectedText;
            }
            set
            {
                TB.SelectedText = value;
            }
        }

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

        public event KeyboardPressEventHandler KeyboardPress;

        public delegate void KeyboardPressEventHandler(object s, KeyPressEventArgs e);

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (!Controls.Contains(TB))
            {
                Controls.Add(TB);
            }
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
            TB.Location = new Point(4, 4);
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
            base.OnMouseDown(e);
            State = MouseState.Down;
            _Shown = true;
            Timer.Enabled = true;
            Timer.Start();
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            State = MouseState.Over;
            _Shown = true;
            Timer.Enabled = true;
            Timer.Start();
            TB.Focus();
            Invalidate();
        }

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

        private void TB_MouseDown(object sender, MouseEventArgs e)
        {
            State = MouseState.Down;
            _Shown = true;
            Timer.Enabled = true;
            Timer.Start();
            Invalidate();
        }

        private void TB_MouseEnter(object sender, EventArgs e)
        {
            State = MouseState.Over;
            _Shown = true;
            Timer.Enabled = true;
            Timer.Start();
            Invalidate();
        }

        private void TB_MouseLeave(object sender, EventArgs e)
        {
            State = MouseState.None;
            _Shown = true;
            Timer.Enabled = true;
            Timer.Start();
            Invalidate();
        }

        private void TB_LostFocus(object sender, EventArgs e)
        {
            State = MouseState.None;
            Invalidate();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            try
            {
                if (!DesignMode)
                {
                    base.OnHandleCreated(e);
                    alpha = 0;
                }
            }
            catch
            {
            }
        }

        private void TextBox_HandleCreated(object sender, EventArgs e)
        {
            alpha = 0;
            if (!DesignMode)
            {
                try
                {
                    FindForm().Load += Loaded;
                    FindForm().Shown += Showed;
                    TB.TextChanged += OnBaseTextChanged;
                    TB.KeyDown += OnBaseKeyDown;
                    TB.KeyPress += OnKeyPress;
                }
                catch
                {
                }
            }
        }

        private void TextBox_HandleDestroyed(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                try
                {
                    FindForm().Load -= Loaded;
                    FindForm().Shown -= Showed;
                    TB.TextChanged -= OnBaseTextChanged;
                    TB.KeyDown -= OnBaseKeyDown;
                    TB.KeyPress -= OnKeyPress;
                }
                catch
                {
                }
            }
        }

        private void TextBox_ForeColorChanged(object sender, EventArgs e)
        {
            TB.ForeColor = ForeColor;
            Refresh();
        }

        public void Loaded(object sender, EventArgs e)
        {
            _Shown = false;
        }

        public void Showed(object sender, EventArgs e)
        {
            _Shown = true;
        }

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

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            DoubleBuffered = true;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = TextRenderingHint.SystemDefault;

            var OuterRect = new Rectangle(0, 0, Width - 1, Height - 1);
            var InnerRect = new Rectangle(1, 1, Width - 3, Height - 3);

            Color Line = scheme.Colors.Line_Checked;
            Color LineHover = scheme.Colors.Back_Hover;
            Color FadeInColor = Color.FromArgb(alpha, Line);
            Color FadeOutColor = Color.FromArgb(255 - alpha, LineHover);

            if (TB.Focused | Focused)
            {
                G.FillRoundedRect(scheme.Brushes.Back_Checked, OuterRect);

                G.DrawRoundedRect_LikeW11(scheme.Pens.Line_Hover, OuterRect);

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