using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace WinPaletter.UI.Retro
{
    /// <summary>
    /// A control that wraps a standard <see cref="TextBox"/> and paints a Windows 9x
    /// sunken 3D border around it. The inner TextBox handles all text input natively.
    /// </summary>
    [Description("Retro TextBox with Windows 9x style")]
    [DefaultEvent("TextChanged")]
    public class TextBoxR : Control
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextBoxR"/> class.
        /// </summary>
        public TextBoxR()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);

            ForeColor = SystemColors.WindowText;
            BackColor = SystemColors.Window;
            Font = new Font("Microsoft Sans Serif", 8f);

            // Inner TextBox — borderless, positioned inside the painted border.
            TB = new()
            {
                Visible = true,
                Font = new Font("Microsoft Sans Serif", 8f),
                ForeColor = SystemColors.WindowText,
                BackColor = SystemColors.Window,
                MaxLength = _maxLength,
                Multiline = _multiline,
                ReadOnly = _readOnly,
                UseSystemPasswordChar = _useSystemPasswordChar,
                BorderStyle = BorderStyle.None,
                Cursor = Cursors.IBeam
            };

            TB.TextChanged += OnInnerTextChanged;
            TB.KeyDown += OnInnerKeyDown;

            UpdateInnerBounds();
        }

        #region Inner TextBox

        private TextBox _tb;

        /// <summary>
        /// The borderless inner <see cref="TextBox"/> that handles text input.
        /// Event subscriptions are managed through the setter to avoid leaks.
        /// </summary>
        private TextBox TB
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get => _tb;

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_tb != null)
                {
                    _tb.MouseDown -= TB_MouseDown;
                    _tb.MouseEnter -= TB_MouseEnter;
                    _tb.MouseLeave -= TB_MouseLeave;
                    _tb.LostFocus -= TB_LostFocus;
                }

                _tb = value;

                if (_tb != null)
                {
                    _tb.MouseDown += TB_MouseDown;
                    _tb.MouseEnter += TB_MouseEnter;
                    _tb.MouseLeave += TB_MouseLeave;
                    _tb.LostFocus += TB_LostFocus;
                }
            }
        }

        /// <summary>
        /// Repositions and resizes the inner TextBox to sit inside the 4px border padding.
        /// In single-line mode the outer control height is driven by the inner TextBox height.
        /// </summary>
        private void UpdateInnerBounds()
        {
            if (TB == null) return;

            TB.Location = new Point(4, 4);
            TB.Width = Math.Max(0, Width - 8);

            if (_multiline)
            {
                TB.Height = Math.Max(0, Height - 8);
            }
            else
            {
                Height = TB.Height + 10;
            }
        }

        #endregion

        #region Cached GDI resources

        // Cached pens — rebuilt only when the corresponding color property changes.
        private Pen _penShadow;
        private Pen _penDkShadow;
        private Pen _penHilight;
        private Pen _penLight;

        // Cached border segment endpoints — rebuilt on resize.
        private PointF[] _dkShadowSeg0, _dkShadowSeg1;
        private PointF[] _shadowSeg0, _shadowSeg1;
        private PointF[] _hilightSeg0, _hilightSeg1;
        private PointF[] _lightSeg0, _lightSeg1;

        private static void ReplacePen(ref Pen pen, Color color)
        {
            pen?.Dispose();
            pen = new Pen(color);
        }

        /// <summary>
        /// Recomputes all cached border segment endpoints from the current control size.
        /// Win9x sunken border layout (outer→inner, top-left then bottom-right):
        ///   DkShadow  outer top + left
        ///   Shadow    inner top + left
        ///   Hilight   inner bottom + right
        ///   Light     outer bottom + right
        /// </summary>
        private void RebuildGeometry()
        {
            float w = Width - 1f;
            float h = Height - 1f;

            _dkShadowSeg0 = [new(0, 0), new(w, 0)];
            _dkShadowSeg1 = [new(0, 0), new(0, h)];
            _shadowSeg0 = [new(1, 1), new(w - 1f, 1)];
            _shadowSeg1 = [new(1, 1), new(1, h - 1f)];
            _hilightSeg0 = [new(w - 1f, 1), new(w - 1f, h - 1f)];
            _hilightSeg1 = [new(1, h - 1f), new(w - 1f, h - 1f)];
            _lightSeg0 = [new(w, 0), new(w, h)];
            _lightSeg1 = [new(0, h), new(w, h)];
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the text alignment inside the TextBox.
        /// </summary>
        [Category("Options")]
        public HorizontalAlignment TextAlign
        {
            get => _textAlign;
            set { _textAlign = value; TB?.TextAlign = value; }
        }
        private HorizontalAlignment _textAlign = HorizontalAlignment.Left;

        /// <summary>
        /// Gets or sets the maximum number of characters the user can enter.
        /// </summary>
        [Category("Options")]
        public int MaxLength
        {
            get => _maxLength;
            set { _maxLength = value; TB?.MaxLength = value; }
        }
        private int _maxLength = 32767;

        /// <summary>
        /// Gets or sets a value indicating whether the text is read-only.
        /// </summary>
        [Category("Options")]
        public bool ReadOnly
        {
            get => _readOnly;
            set { _readOnly = value; TB?.ReadOnly = value; }
        }
        private bool _readOnly = false;

        /// <summary>
        /// Gets or sets a value indicating whether text is displayed as the password character.
        /// </summary>
        [Category("Options")]
        public bool UseSystemPasswordChar
        {
            get => _useSystemPasswordChar;
            set { _useSystemPasswordChar = value; TB?.UseSystemPasswordChar = value; }
        }
        private bool _useSystemPasswordChar = false;

        /// <summary>
        /// Gets or sets a value indicating whether this is a multiline TextBox.
        /// </summary>
        [Category("Options")]
        public bool Multiline
        {
            get => _multiline;
            set
            {
                _multiline = value;
                if (TB != null)
                {
                    TB.Multiline = value;
                    UpdateInnerBounds();
                }
            }
        }
        private bool _multiline = false;

        /// <summary>
        /// Gets or sets the text displayed in the TextBox.
        /// </summary>
        [Category("Options")]
        public override string Text
        {
            get => base.Text;
            set
            {
                base.Text = value;
                if (TB != null && TB.Text != value) TB.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the font used by the TextBox.
        /// </summary>
        [Category("Options")]
        public override Font Font
        {
            get => base.Font;
            set
            {
                base.Font = value;
                if (TB != null)
                {
                    TB?.Font = value;
                    UpdateInnerBounds();
                }
            }
        }

        /// <summary>
        /// Gets or sets the background color. Also applied to the inner TextBox.
        /// </summary>
        public new Color BackColor
        {
            get => base.BackColor;
            set
            {
                if (base.BackColor == value) return;
                base.BackColor = value;
                TB?.BackColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the foreground (text) color. Also applied to the inner TextBox.
        /// </summary>
        public new Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                if (base.ForeColor == value) return;
                base.ForeColor = value;
                TB?.ForeColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the inner top/left shadow color (second border layer).
        /// </summary>
        public Color ButtonShadow
        {
            get => _buttonShadow;
            set
            {
                if (_buttonShadow == value) return;
                _buttonShadow = value;
                ReplacePen(ref _penShadow, value);
                Invalidate();
            }
        }
        private Color _buttonShadow = SystemColors.ButtonShadow;

        /// <summary>
        /// Gets or sets the outer top/left dark shadow color (outermost border layer).
        /// </summary>
        public Color ButtonDkShadow
        {
            get => _buttonDkShadow;
            set
            {
                if (_buttonDkShadow == value) return;
                _buttonDkShadow = value;
                ReplacePen(ref _penDkShadow, value);
                Invalidate();
            }
        }
        private Color _buttonDkShadow = SystemColors.ControlDark;

        /// <summary>
        /// Gets or sets the inner bottom/right hilight color (second border layer).
        /// </summary>
        public Color ButtonHilight
        {
            get => _buttonHilight;
            set
            {
                if (_buttonHilight == value) return;
                _buttonHilight = value;
                ReplacePen(ref _penHilight, value);
                Invalidate();
            }
        }
        private Color _buttonHilight = SystemColors.ButtonHighlight;

        /// <summary>
        /// Gets or sets the outer bottom/right light color (outermost border layer).
        /// </summary>
        public Color ButtonLight
        {
            get => _buttonLight;
            set
            {
                if (_buttonLight == value) return;
                _buttonLight = value;
                ReplacePen(ref _penLight, value);
                Invalidate();
            }
        }
        private Color _buttonLight = SystemColors.ControlLight;

        #endregion

        #region Overrides — layout

        /// <summary>
        /// Adds the inner TextBox to the control's child collection when the handle is created.
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (!Controls.Contains(TB)) Controls.Add(TB);
        }

        /// <summary>
        /// Repositions the inner TextBox and rebuilds geometry when the control is resized.
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateInnerBounds();
            RebuildGeometry();
        }

        /// <summary>
        /// Rebuilds geometry when the font changes.
        /// </summary>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            RebuildGeometry();
        }

        #endregion

        #region Overrides — focus and mouse

        /// <summary>
        /// Focuses the inner TextBox on mouse down.
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            TB?.Focus();
            Invalidate();
        }

        /// <summary>
        /// Focuses the inner TextBox on mouse up.
        /// </summary>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            TB?.Focus();
            Invalidate();
        }

        /// <summary>
        /// Invalidates on mouse enter so the border can respond to hover if needed.
        /// </summary>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Invalidate();
        }

        /// <summary>
        /// Invalidates on mouse leave.
        /// </summary>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Invalidate();
        }

        private void TB_MouseDown(object sender, MouseEventArgs e) => Invalidate();
        private void TB_MouseEnter(object sender, EventArgs e) => Invalidate();
        private void TB_MouseLeave(object sender, EventArgs e) => Invalidate();
        private void TB_LostFocus(object sender, EventArgs e) => Invalidate();

        #endregion

        #region Overrides — text input

        /// <summary>
        /// Propagates the inner TextBox text to the outer control's Text property.
        /// Guards against a set loop by checking for equality first.
        /// </summary>
        private void OnInnerTextChanged(object sender, EventArgs e)
        {
            if (base.Text != TB?.Text) base.Text = TB?.Text;
        }

        /// <summary>
        /// Handles standard clipboard and undo keyboard shortcuts.
        /// </summary>
        private void OnInnerKeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control) return;

            switch (e.KeyCode)
            {
                case Keys.A: TB.SelectAll(); e.SuppressKeyPress = true; break;
                case Keys.C: TB.Copy(); e.SuppressKeyPress = true; break;
                case Keys.X: TB.Cut(); e.SuppressKeyPress = true; break;
                case Keys.V: TB.Paste(); e.SuppressKeyPress = true; break;
                case Keys.Z: TB.Undo(); e.SuppressKeyPress = true; break;
            }

            Invalidate();
        }

        #endregion

        #region Paint

        /// <summary>
        /// Paints the Win9x sunken 3D border directly onto the control surface.
        /// The inner TextBox renders its own text — this method draws only the border.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighSpeed;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            // Background — fills the area not covered by the inner TextBox.
            G.Clear(BackColor);

            // Win9x sunken border — outermost to innermost, top-left then bottom-right.
            // DkShadow and Shadow on top+left give the pressed/inset appearance.
            if (_penDkShadow != null) { G.DrawLine(_penDkShadow, _dkShadowSeg0[0], _dkShadowSeg0[1]); G.DrawLine(_penDkShadow, _dkShadowSeg1[0], _dkShadowSeg1[1]); }
            if (_penShadow != null) { G.DrawLine(_penShadow, _shadowSeg0[0], _shadowSeg0[1]); G.DrawLine(_penShadow, _shadowSeg1[0], _shadowSeg1[1]); }
            if (_penHilight != null) { G.DrawLine(_penHilight, _hilightSeg0[0], _hilightSeg0[1]); G.DrawLine(_penHilight, _hilightSeg1[0], _hilightSeg1[1]); }
            if (_penLight != null) { G.DrawLine(_penLight, _lightSeg0[0], _lightSeg0[1]); G.DrawLine(_penLight, _lightSeg1[0], _lightSeg1[1]); }
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Releases all cached GDI resources and the inner TextBox.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _penShadow?.Dispose();
                _penDkShadow?.Dispose();
                _penHilight?.Dispose();
                _penLight?.Dispose();
                _tb?.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}