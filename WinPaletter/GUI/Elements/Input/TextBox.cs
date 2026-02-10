using FluentTransitions;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
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
                Cursor = Cursors.IBeam,
                ScrollBars = _Scrollbars,
                WordWrap = _WordWrap,
            };

            // Initialize fixed TB height
            _fixedTextBoxHeight = CalculateTextBoxHeight();

            alpha = 0;

            SetTBSizes();

            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();

            BackColor = Color.Transparent;
            DoubleBuffered = true;
        }

        #region Variables

        // Fixed height for the inner TextBox (calculated from font)
        private int _fixedTextBoxHeight;
        private int leftOffset => Multiline ? 2 : 6;

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

                        // Update TB sizes and anchors based on multiline setting
                        UpdateTextBoxConfiguration();
                    }
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
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

                        // Recalculate fixed height for inner TextBox
                        _fixedTextBoxHeight = CalculateTextBoxHeight();

                        // Update TB configuration
                        UpdateTextBoxConfiguration();
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

        /// <summary>
        /// Calculates the proper height for the inner TextBox based on current font
        /// </summary>
        private int CalculateTextBoxHeight()
        {
            // For single-line TextBox, calculate height based on font
            // Using "Ay" to get proper height including ascenders and descenders
            return TextRenderer.MeasureText("Ay", TB?.Font ?? this.Font).Height + (_Multiline ? 0 : 3);
        }

        /// <summary>
        /// Updates the TextBox configuration based on current properties
        /// </summary>
        private void UpdateTextBoxConfiguration()
        {
            if (TB == null) return;

            if (_Multiline)
            {
                // For multiline: TB fills the entire container
                TB.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                TB.Location = new Point(leftOffset, 4);
                TB.Height = Math.Max(Height - 8, _fixedTextBoxHeight); // Minimum height is font height
                TB.Width = Width - (leftOffset * 2); // Account for both left and right
            }
            else
            {
                // For single line: fixed height, anchored left/right, centered vertically
                TB.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                TB.Height = _fixedTextBoxHeight;
                TB.Width = Width - (leftOffset * 2); // Account for both left and right

                // Center vertically
                CenterTextBoxVertically(leftOffset);
            }
        }

        /// <summary>
        /// Centers the TextBox vertically within the container
        /// </summary>
        private void CenterTextBoxVertically(int leftOffset)
        {
            if (TB == null || _Multiline) return;

            int y = (Height - TB.Height) / 2;
            // Ensure minimum padding
            y = Math.Max(4, y);
            TB.Location = new Point(leftOffset, y);
        }

        /// <summary>
        /// Updates TB sizes and position
        /// </summary>
        void SetTBSizes()
        {
            if (TB == null) return;

            // Update fixed height
            _fixedTextBoxHeight = CalculateTextBoxHeight();

            // Apply configuration
            UpdateTextBoxConfiguration();
        }

        public event KeyboardPressEventHandler KeyboardPress;

        public delegate void KeyboardPressEventHandler(object s, KeyPressEventArgs e);

        protected override void OnCreateControl()
        {
            if (!Controls.Contains(TB))
            {
                Controls.Add(TB);
                SetTBSizes();
            }

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
            base.OnResize(e);

            // Update TB when container is resized
            if (TB != null)
            {
                if (_Multiline)
                {
                    // Multiline: adjust TB height to fill container
                    TB.Height = Math.Max(Height - 8, _fixedTextBoxHeight);
                    TB.Width = Width - (leftOffset * 2);
                    TB.Location = new Point(leftOffset, 4);
                }
                else
                {
                    // Single line: maintain fixed height, adjust width, and center
                    TB.Height = _fixedTextBoxHeight;
                    TB.Width = Width - (leftOffset * 2);
                    CenterTextBoxVertically(leftOffset);
                }
            }

            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (CanAnimate) { Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (CanAnimate) { Transition.With(this, nameof(alpha), ContainsFocus ? 255 : 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = ContainsFocus ? 255 : 0; }

            tb.Focus();

            base.OnMouseUp(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (CanAnimate) { Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 255; }

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (CanAnimate) { Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
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
            if (CanAnimate) { Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
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

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            Rectangle OuterRect = new(0, 0, Width - 1, Height - 1);
            Rectangle InnerRect = new(1, 1, Width - 3, Height - 3);

            Color Line = scheme.Colors.Line_Checked;
            Color LineHover = scheme.Colors.Back_Hover(parentLevel);
            Color FadeInColor = Color.FromArgb(alpha, Line);
            Color FadeOutColor = Color.FromArgb(255 - alpha, LineHover);

            if (tb.Focused | Focused)
            {
                G.FillRoundedRect(scheme.Brushes.Back_Checked, OuterRect);

                if (Program.Style.DarkMode)
                {
                    G.DrawRoundedRectBeveledReverse(scheme.Pens.Line_Checked_Hover, OuterRect);
                }
                else
                {
                    G.DrawRoundedRectBeveled(scheme.Pens.Line_Checked_Hover, OuterRect);
                }

                tb.BackColor = scheme.Colors.Back_Checked;
            }

            else
            {
                using (SolidBrush br = new(scheme.Colors.Back(parentLevel))) { G.FillRoundedRect(br, InnerRect); }

                using (SolidBrush br = new(Color.FromArgb(alpha, scheme.Colors.Back(parentLevel)))) { G.FillRoundedRect(br, OuterRect); }

                if (Program.Style.DarkMode)
                {
                    using (Pen P = new(FadeInColor)) { G.DrawRoundedRectBeveledReverse(P, OuterRect); }
                    using (Pen P = new(FadeOutColor)) { G.DrawRoundedRectBeveledReverse(P, InnerRect); }
                }
                else
                {
                    using (Pen P = new(FadeInColor)) { G.DrawRoundedRectBeveled(P, OuterRect); }
                    using (Pen P = new(FadeOutColor)) { G.DrawRoundedRectBeveled(P, InnerRect); }
                }

                tb.BackColor = scheme.Colors.Back(parentLevel);
            }
        }
    }
}