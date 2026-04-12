using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using WinPaletter.Templates;

namespace WinPaletter.UI.Retro
{
    /// <summary>
    /// A control that simulates a classic Windows 9x button with full 3D border rendering,
    /// focus rectangle, pressed state, scrollbar thumb mode, and interactive color editing.
    /// </summary>
    [Description("Retro button with Windows 9x style")]
    public class ButtonR : Button
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonR"/> class.
        /// </summary>
        public ButtonR()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            Font = new Font("Microsoft Sans Serif", 8f);
            ForeColor = Color.Black;
            BackColor = Color.FromArgb(192, 192, 192);

            LostFocus += OnLostFocus;

            RebuildGeometry();
        }

        #region Private fields

        // Visual state.
        private MouseState _state = MouseState.None;
        private bool _pressed = false;

        // Cached geometry — rebuilt on resize, font change, and state change.
        private Rectangle _rect;       // full button bounds (W-1, H-1)
        private Rectangle _rectInner;  // inset by 1px for pressed inner border
        private Rectangle _rectDash;   // focus rect region (inset by 4px)
        private Rectangle _textRect;   // tight text hit-test bounds

        // Border segment endpoints — two segments per color layer.
        // SetPoints picks the correct set based on mode and state.
        private Point[] _hilightSeg0, _hilightSeg1;
        private Point[] _lightSeg0, _lightSeg1;
        private Point[] _shadowSeg0, _shadowSeg1;
        private Point[] _dkShadowSeg0, _dkShadowSeg1;

        // Cached pens — rebuilt only when their source color changes.
        private Pen _penWindowFrame;
        private Pen _penShadow;
        private Pen _penDkShadow;
        private Pen _penHilight;
        private Pen _penLight;
        private Pen _penBackColor;

        // Cached overlay color — recomputed when BackColor changes.
        private Color _overlayColor;

        // Shared border segment overlay color.
        private static readonly Color BorderOverlay = Color.FromArgb(200, 128, 0, 0);

        // Hover state — color editing.
        private bool _cursorOnWindowFrame;
        private bool _cursorOnShadow;
        private bool _cursorOnDkShadow;
        private bool _cursorOnHilight;
        private bool _cursorOnLight;
        private bool _cursorOnFace;
        private bool _cursorOnText;

        // Computed edit-mode flags.
        private bool IsEditWindowFrame => EnableEditingColors && _cursorOnWindowFrame;
        private bool IsEditShadow => EnableEditingColors && _cursorOnShadow;
        private bool IsEditDkShadow => EnableEditingColors && _cursorOnDkShadow;
        private bool IsEditHilight => EnableEditingColors && _cursorOnHilight;
        private bool IsEditLight => EnableEditingColors && _cursorOnLight;
        private bool IsEditFace => EnableEditingColors && _cursorOnFace;
        private bool IsEditText => EnableEditingColors && _cursorOnText;

        private bool IsMarlett => Font.FontFamily.Name.Equals("Marlett", StringComparison.OrdinalIgnoreCase);

        #endregion

        #region Mouse state enum

        /// <summary>
        /// Represents the current mouse interaction state of the button.
        /// </summary>
        public enum MouseState
        {
            /// <summary>Mouse is not over the button.</summary>
            None,
            /// <summary>Mouse is hovering over the button.</summary>
            Over,
            /// <summary>Mouse button is held down.</summary>
            Down
        }

        #endregion

        #region Properties

        private Image _image;

        /// <summary>
        /// Gets or sets the image displayed on the button.
        /// </summary>
        public new Image Image
        {
            get => _image;
            set { _image = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the window frame color (outermost focused/pressed border).
        /// </summary>
        public Color WindowFrame
        {
            get => _windowFrame;
            set
            {
                if (_windowFrame == value) return;
                _windowFrame = value;
                ReplacePen(ref _penWindowFrame, value);
                Invalidate();
            }
        }
        private Color _windowFrame = SystemColors.WindowFrame;

        /// <summary>
        /// Gets or sets the button shadow color (inner bottom/right).
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
        /// Gets or sets the button dark shadow color (outer bottom/right).
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
        /// Gets or sets the button hilight color (outer top/left).
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
        /// Gets or sets the button light color (inner top/left).
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

        /// <summary>
        /// Gets or sets the background color. Also rebuilds the cached back-color pen.
        /// </summary>
        public new Color BackColor
        {
            get => base.BackColor;
            set
            {
                if (base.BackColor == value) return;
                base.BackColor = value;
                ReplacePen(ref _penBackColor, value);
                _overlayColor = Color.FromArgb(100, value.IsDark() ? Color.White : Color.Black);
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this button renders as a scrollbar thumb.
        /// In this mode the border order is reversed: hilight on top-left, shadow on bottom-right.
        /// </summary>
        public bool UseItAsScrollbar
        {
            get => _useItAsScrollbar;
            set { if (_useItAsScrollbar != value) { _useItAsScrollbar = value; RebuildGeometry(); Invalidate(); } }
        }
        private bool _useItAsScrollbar = false;

        /// <summary>
        /// Gets or sets a value indicating whether the button permanently renders in the pressed state.
        /// </summary>
        public bool AppearsAsPressed
        {
            get => _appearsAsPressed;
            set { if (_appearsAsPressed != value) { _appearsAsPressed = value; RebuildGeometry(); Invalidate(); } }
        }
        private bool _appearsAsPressed = false;

        /// <summary>
        /// Gets or sets a value indicating whether a Percent50 hatch fill is drawn over the face
        /// when <see cref="AppearsAsPressed"/> is true.
        /// </summary>
        public bool HatchBrush
        {
            get => _hatchBrush;
            set { if (_hatchBrush != value) { _hatchBrush = value; Invalidate(); } }
        }
        private bool _hatchBrush = false;

        /// <summary>
        /// Gets or sets the height of the focus rectangle bars (top and bottom strips).
        /// </summary>
        public int FocusRectHeight
        {
            get => _focusRectHeight;
            set { if (_focusRectHeight != value) { _focusRectHeight = value; Invalidate(); } }
        }
        private int _focusRectHeight = 1;

        /// <summary>
        /// Gets or sets the width of the focus rectangle bars (left and right strips).
        /// </summary>
        public int FocusRectWidth
        {
            get => _focusRectWidth;
            set { if (_focusRectWidth != value) { _focusRectWidth = value; Invalidate(); } }
        }
        private int _focusRectWidth = 1;

        /// <summary>
        /// Gets or sets a value indicating whether colors can be edited interactively by right-clicking.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingColors { get; set; } = false;

        #endregion

        #region Editor event

        /// <summary>
        /// Raised when the user right-clicks a color region while <see cref="EnableEditingColors"/> is true.
        /// </summary>
        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);

        /// <summary>
        /// Raised when the user right-clicks a color region while <see cref="EnableEditingColors"/> is true.
        /// </summary>
        public event EditorInvokerEventHandler EditorInvoker;

        #endregion

        #region Resource helpers

        private static void ReplacePen(ref Pen pen, Color color)
        {
            pen?.Dispose();
            pen = new Pen(color);
        }

        /// <summary>
        /// Draws a Percent25 hatch fill and 1px outline over <paramref name="r"/>.
        /// </summary>
        private static void DrawOverlay(Graphics G, Rectangle r, Color overlayColor)
        {
            using (HatchBrush hb = new(HatchStyle.Percent25, overlayColor, Color.Transparent))
            using (Pen p = new(overlayColor))
            {
                G.FillRectangle(hb, r);
                G.DrawRectangle(p, r);
            }
        }

        private static bool OnSegment(Point[] seg, Point p)
        {
            Point a = seg[0];
            Point b = seg[1];

            // Horizontal line
            if (a.Y == b.Y) return p.Y == a.Y && p.X >= Math.Min(a.X, b.X) && p.X <= Math.Max(a.X, b.X);

            // Vertical line
            if (a.X == b.X) return p.X == a.X && p.Y >= Math.Min(a.Y, b.Y) && p.Y <= Math.Max(a.Y, b.Y);

            return false;
        }

        /// <summary>
        /// Draws the border segment overlay lines for whichever of the four layers is active.
        /// Called from each render branch after the border lines are drawn.
        /// </summary>
        private void DrawBorderOverlays(Graphics G)
        {
            if (!IsEditShadow && !IsEditDkShadow && !IsEditHilight && !IsEditLight) return;

            using (Pen p = new(BorderOverlay))
            {
                if (IsEditHilight) { G.DrawLine(p, _hilightSeg0[0], _hilightSeg0[1]); G.DrawLine(p, _hilightSeg1[0], _hilightSeg1[1]); }
                if (IsEditLight) { G.DrawLine(p, _lightSeg0[0], _lightSeg0[1]); G.DrawLine(p, _lightSeg1[0], _lightSeg1[1]); }
                if (IsEditShadow) { G.DrawLine(p, _shadowSeg0[0], _shadowSeg0[1]); G.DrawLine(p, _shadowSeg1[0], _shadowSeg1[1]); }
                if (IsEditDkShadow) { G.DrawLine(p, _dkShadowSeg0[0], _dkShadowSeg0[1]); G.DrawLine(p, _dkShadowSeg1[0], _dkShadowSeg1[1]); }
            }
        }

        #endregion

        #region Geometry cache

        /// <summary>
        /// Recomputes all cached rectangles and border segment endpoints from the current
        /// control size, state, and mode flags. Must be called whenever any of these change.
        /// </summary>
        private void RebuildGeometry()
        {
            _rect = new Rectangle(0, 0, Width - 1, Height - 1);
            _rectInner = new Rectangle(1, 1, Width - 3, Height - 3);
            _rectDash = new Rectangle(4, 4, Width - 9, Height - 9);

            SizeF ts = Text.Measure(Font);
            _textRect = new Rectangle(_rect.X + (_rect.Width - (int)ts.Width) / 2, _rect.Y + (_rect.Height - (int)ts.Height) / 2, (int)ts.Width, (int)ts.Height);

            // Select segment layout based on mode and state.
            if (_useItAsScrollbar)
            {
                // Scrollbar thumb: hilight on top+left, shadow on bottom+right (opposite of normal button).
                _hilightSeg0 = [new(0, 0), new(Width - 1, 0)];
                _hilightSeg1 = [new(0, 1), new(0, Height - 1)];
                _dkShadowSeg0 = [new(0, Height - 1), new(Width - 1, Height - 1)];
                _dkShadowSeg1 = [new(Width - 1, 0), new(Width - 1, Height - 1)];
                _lightSeg0 = [new(1, 1), new(Width - 2, 1)];
                _lightSeg1 = [new(1, 2), new(1, Height - 2)];
                _shadowSeg0 = [new(1, Height - 2), new(Width - 2, Height - 2)];
                _shadowSeg1 = [new(Width - 2, 1), new(Width - 2, Height - 2)];
            }
            else if (_appearsAsPressed)
            {
                // Pressed appearance: shadow layers on top+left, hilight on bottom+right.
                _dkShadowSeg0 = [new(0, 0), new(Width - 1, 0)];
                _dkShadowSeg1 = [new(0, 1), new(0, Height - 1)];
                _shadowSeg0 = [new(1, 1), new(Width - 2, 1)];
                _shadowSeg1 = [new(1, 2), new(1, Height - 2)];
                _hilightSeg0 = [new(0, Height - 1), new(Width - 1, Height - 1)];
                _hilightSeg1 = [new(Width - 1, 0), new(Width - 1, Height - 1)];
                _lightSeg0 = [new(1, Height - 2), new(Width - 2, Height - 2)];
                _lightSeg1 = [new(Width - 2, 1), new(Width - 2, Height - 2)];
            }
            else if (!Focused)
            {
                // Normal unfocused button.
                _hilightSeg0 = [new(0, 0), new(Width - 1, 0)];
                _hilightSeg1 = [new(0, 1), new(0, Height - 1)];
                _dkShadowSeg0 = [new(0, Height - 1), new(Width - 1, Height - 1)];
                _dkShadowSeg1 = [new(Width - 1, 0), new(Width - 1, Height - 1)];
                _lightSeg0 = [new(1, 1), new(Width - 2, 1)];
                _lightSeg1 = [new(1, 2), new(1, Height - 2)];
                _shadowSeg0 = [new(1, Height - 2), new(Width - 2, Height - 2)];
                _shadowSeg1 = [new(Width - 2, 1), new(Width - 2, Height - 2)];
            }
            else
            {
                // Focused button: border shifts inward by 1px to leave room for the WindowFrame outline.
                _hilightSeg0 = [new(1, 1), new(Width - 2, 1)];
                _hilightSeg1 = [new(1, 2), new(1, Height - 2)];
                _dkShadowSeg0 = [new(1, Height - 2), new(Width - 2, Height - 2)];
                _dkShadowSeg1 = [new(Width - 2, 1), new(Width - 2, Height - 2)];
                _lightSeg0 = [new(2, 2), new(Width - 3, 2)];
                _lightSeg1 = [new(2, 3), new(2, Height - 3)];
                _shadowSeg0 = [new(2, Height - 3), new(Width - 3, Height - 3)];
                _shadowSeg1 = [new(Width - 3, 2), new(Width - 3, Height - 3)];
            }
        }

        #endregion

        #region Overrides — layout triggers

        /// <summary>
        /// Rebuilds geometry on resize.
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
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

        /// <summary>
        /// Rebuilds geometry when the displayed text changes.
        /// </summary>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            RebuildGeometry();
        }

        /// <summary>
        /// Rebuilds the cached overlay and back-color pen when BackColor changes.
        /// </summary>
        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            _overlayColor = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
            ReplacePen(ref _penBackColor, BackColor);
        }

        #endregion

        #region Overrides — mouse and focus

        private void OnLostFocus(object sender, EventArgs e)
        {
            _state = MouseState.None;
            _pressed = false;
            RebuildGeometry();
            Invalidate();
        }

        /// <summary>
        /// Sets hover state on mouse enter.
        /// </summary>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _state = MouseState.Over;
            Invalidate();
        }

        /// <summary>
        /// Clears hover state and all color-edit flags on mouse leave.
        /// </summary>
        protected override void OnMouseLeave(EventArgs e)
        {
            if (!DesignMode && EnableEditingColors)
            {
                _cursorOnWindowFrame = false;
                _cursorOnShadow = false;
                _cursorOnDkShadow = false;
                _cursorOnHilight = false;
                _cursorOnLight = false;
                _cursorOnFace = false;
                _cursorOnText = false;
            }

            base.OnMouseLeave(e);
            _state = MouseState.None;
            Invalidate();
        }

        /// <summary>
        /// Sets pressed state on left-button down (or any button when color editing is off).
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!EnableEditingColors || e.Button == MouseButtons.Left)
            {
                _state = MouseState.Down;
                _pressed = true;
                RebuildGeometry();
            }

            Invalidate();
            base.OnMouseDown(e);
        }

        /// <summary>
        /// On right-click up invokes the color editor for the zone under the cursor.
        /// Left-click up restores hover state.
        /// </summary>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!DesignMode && EnableEditingColors && e.Button != MouseButtons.Left)
            {
                if (IsEditShadow) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonShadow)));
                else if (IsEditDkShadow) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonDkShadow)));
                else if (IsEditHilight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonHilight)));
                else if (IsEditLight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonLight)));
                else if (IsEditFace) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonFace)));
                else if (IsEditText) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonText)));
                else if (IsEditWindowFrame) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.WindowFrame)));
            }

            _state = MouseState.Over;
            RebuildGeometry();
            Invalidate();
            base.OnMouseUp(e);
        }

        /// <summary>
        /// Updates color-edit hover flags using <see cref="OnSegment(Point[], Point)"/> for border lines
        /// and rect containment for face and text. Invalidates only when state changes.
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            // Set editing flags on the button
            if (!DesignMode && EnableEditingColors)
            {
                // Set editing flags based on the mouse position
                bool onShadow0 = OnSegment(_shadowSeg0, e.Location);
                bool onShadow1 = OnSegment(_shadowSeg1, e.Location);
                bool onDkShadow0 = OnSegment(_dkShadowSeg0, e.Location);
                bool onDkShadow1 = OnSegment(_dkShadowSeg1, e.Location);
                bool onHilight0 = OnSegment(_hilightSeg0, e.Location);
                bool onHilight1 = OnSegment(_hilightSeg1, e.Location);
                bool onLight0 = OnSegment(_lightSeg0, e.Location);
                bool onLight1 = OnSegment(_lightSeg1, e.Location);

                if ((_state == MouseState.Over | _state == MouseState.None | !Enabled) && Focused)
                {
                    _cursorOnWindowFrame = _rect.BordersContains(e.Location);

                    _cursorOnShadow = !_cursorOnWindowFrame && (onShadow0 || onShadow1);
                    _cursorOnDkShadow = !_cursorOnWindowFrame && (onDkShadow0 || onDkShadow1);
                    _cursorOnHilight = !_cursorOnWindowFrame && (onHilight0 || onHilight1);
                    _cursorOnLight = !_cursorOnWindowFrame && (onLight0 || onLight1);
                }
                else
                {
                    _cursorOnWindowFrame = false;

                    _cursorOnShadow = onShadow0 || onShadow1;
                    _cursorOnDkShadow = onDkShadow0 || onDkShadow1;
                    _cursorOnHilight = onHilight0 || onHilight1;
                    _cursorOnLight = onLight0 || onLight1;
                }

                _cursorOnText = _textRect.Contains(e.Location);
                _cursorOnFace = _rect.Contains(e.Location) && !_cursorOnShadow && !_cursorOnDkShadow && !_cursorOnHilight && !_cursorOnLight && !_cursorOnText && !_cursorOnWindowFrame;

                Invalidate();
            }

            base.OnMouseMove(e);
        }

        #endregion

        #region Paint

        /// <summary>
        /// Paints the button directly onto the control surface.
        /// Render branches: scrollbar thumb → appears-as-pressed → normal/focused → clicked.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighSpeed;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            G.Clear(BackColor);

            if (_useItAsScrollbar)
            {
                PaintScrollbarThumb(G);
            }
            else if (_appearsAsPressed)
            {
                PaintAppearsAsPressed(G);
            }
            else if (_state == MouseState.Down)
            {
                PaintClicked(G);
            }
            else if (Focused)
            {
                PaintFocused(G);
            }
            else
            {
                PaintNormal(G);
            }

            PaintTextAndImage(G);
        }

        /// <summary>
        /// Paints the face hover overlay when the cursor is over the face region.
        /// </summary>
        private void PaintFaceOverlay(Graphics G)
        {
            if (!IsEditFace) return;
            using (HatchBrush hb = new(HatchStyle.Percent25, _overlayColor, Color.Transparent))
            {
                G.FillRectangle(hb, _rect);
            }
        }

        /// <summary>
        /// Draws all four 3D border lines using the current cached pens and segment arrays.
        /// </summary>
        private void PaintBorderLines(Graphics G)
        {
            if (_penHilight != null) { G.DrawLine(_penHilight, _hilightSeg0[0], _hilightSeg0[1]); G.DrawLine(_penHilight, _hilightSeg1[0], _hilightSeg1[1]); }
            if (_penLight != null) { G.DrawLine(_penLight, _lightSeg0[0], _lightSeg0[1]); G.DrawLine(_penLight, _lightSeg1[0], _lightSeg1[1]); }
            if (_penShadow != null) { G.DrawLine(_penShadow, _shadowSeg0[0], _shadowSeg0[1]); G.DrawLine(_penShadow, _shadowSeg1[0], _shadowSeg1[1]); }
            if (_penDkShadow != null) { G.DrawLine(_penDkShadow, _dkShadowSeg0[0], _dkShadowSeg0[1]); G.DrawLine(_penDkShadow, _dkShadowSeg1[0], _dkShadowSeg1[1]); }
        }

        /// <summary>
        /// Draws the four-strip focus rectangle using a Percent50 hatch pattern.
        /// Skipped for Marlett (control-box glyph) fonts.
        /// </summary>
        private void PaintFocusRect(Graphics G)
        {
            if (IsMarlett) return;

            Rectangle ur = new(_rectDash.X, _rectDash.Y, _rectDash.Width, _focusRectHeight);
            Rectangle dr = new(ur.X, _rectDash.Bottom - _focusRectHeight, ur.Width, _focusRectHeight);
            Rectangle lr = new(_rectDash.X, _rectDash.Y, _focusRectWidth, _rectDash.Height);
            Rectangle rr = new(_rectDash.Right - _focusRectWidth, _rectDash.Y, _focusRectWidth, _rectDash.Height);

            using (HatchBrush hb = new(HatchStyle.Percent50, Color.Black, BackColor))
            {
                G.FillRectangle(hb, ur);
                G.FillRectangle(hb, dr);
                G.FillRectangle(hb, lr);
                G.FillRectangle(hb, rr);
            }
        }

        private void PaintScrollbarThumb(Graphics G)
        {
            PaintFaceOverlay(G);
            PaintBorderLines(G);
            DrawBorderOverlays(G);
        }

        private void PaintAppearsAsPressed(Graphics G)
        {
            G.Clear(_buttonHilight);

            PaintFaceOverlay(G);

            if (_hatchBrush)
            {
                using (HatchBrush hb = new(HatchStyle.Percent50, _buttonHilight, BackColor))
                {
                    G.FillRectangle(hb, _rect);
                }
            }

            // Pressed appearance: DkShadow+Shadow on top-left, Hilight on bottom-right, BackColor inner bottom-right.
            if (_penDkShadow != null) { G.DrawLine(_penDkShadow, _dkShadowSeg0[0], _dkShadowSeg0[1]); G.DrawLine(_penDkShadow, _dkShadowSeg1[0], _dkShadowSeg1[1]); }
            if (_penShadow != null) { G.DrawLine(_penShadow, _shadowSeg0[0], _shadowSeg0[1]); G.DrawLine(_penShadow, _shadowSeg1[0], _shadowSeg1[1]); }
            if (_penHilight != null) { G.DrawLine(_penHilight, _hilightSeg0[0], _hilightSeg0[1]); G.DrawLine(_penHilight, _hilightSeg1[0], _hilightSeg1[1]); }
            if (_penBackColor != null) { G.DrawLine(_penBackColor, _lightSeg0[0], _lightSeg0[1]); G.DrawLine(_penBackColor, _lightSeg1[0], _lightSeg1[1]); }

            DrawBorderOverlays(G);
        }

        private void PaintNormal(Graphics g)
        {
            PaintFaceOverlay(g);
            PaintBorderLines(g);
            DrawBorderOverlays(g);
        }

        private void PaintFocused(Graphics G)
        {
            PaintFaceOverlay(G);

            // WindowFrame outline at the very edge.
            if (_penWindowFrame != null) G.DrawRectangle(_penWindowFrame, _rect);

            PaintBorderLines(G);

            if (_pressed) PaintFocusRect(G);

            DrawBorderOverlays(G);

            if (IsEditWindowFrame)
            {
                using (Pen p = new(BorderOverlay))
                {
                    G.DrawRectangle(p, _rect);
                }
            }
        }

        private void PaintClicked(Graphics G)
        {
            PaintFaceOverlay(G);

            if (_penWindowFrame != null) G.DrawRectangle(_penWindowFrame, _rect);
            if (_penShadow != null) G.DrawRectangle(_penShadow, _rectInner);

            PaintFocusRect(G);
            DrawBorderOverlays(G);
        }

        /// <summary>
        /// Draws the button text and/or image with the correct alignment and disabled styling.
        /// Also draws the text region color-edit overlay when active.
        /// </summary>
        private void PaintTextAndImage(Graphics G)
        {
            if (IsEditText) DrawOverlay(G, _textRect, _overlayColor);

            using (SolidBrush br = new(Enabled ? ForeColor : base.BackColor.CB(-0.2f)))
            {
                if (_image == null)
                {
                    RectangleF r = _rect;

                    if (TextAlign == ContentAlignment.MiddleCenter && IsMarlett && Text.Length == 1)
                    {
                        SizeF ts = Text.Measure(Font);
                        r = new RectangleF(_rect.X + (_rect.Width - ts.Width) / 2f + 0.5f, _rect.Y + (_rect.Height - ts.Height) / 2f + 1f, ts.Width, ts.Height);
                    }

                    using (StringFormat sf = (TextAlign == ContentAlignment.MiddleCenter ? ContentAlignment.MiddleCenter : base.TextAlign).ToStringFormat())
                    {
                        G.DrawString(Text, Font, br, r, sf);
                    }
                }
                else
                {
                    float imgX = (_rect.Width - _image.Width) / 2f;
                    float imgY = (_rect.Height - _image.Height) / 2f;

                    using (StringFormat sf = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    {
                        switch (ImageAlign)
                        {
                            case ContentAlignment.MiddleCenter:
                                {
                                    sf.LineAlignment = StringAlignment.Near;
                                    float alx = (_rect.Height - (_image.Height + 4 + Text.Measure(Font).Height)) / 2f;
                                    if (string.IsNullOrEmpty(Text)) G.DrawImage(_image, new RectangleF(imgX, imgY, _image.Width, _image.Height));
                                    else G.DrawImage(_image, new RectangleF(imgX, alx, _image.Width, _image.Height));
                                    G.DrawString(Text, Font, br, new RectangleF(0, alx + 9 + _image.Height, Width, Height), sf);
                                    break;
                                }
                            case ContentAlignment.MiddleLeft:
                                {
                                    sf.Alignment = StringAlignment.Near;
                                    float alx = (_rect.Width - (_image.Width + 4 + Text.Measure(Font).Width)) / 2f;
                                    G.DrawImage(_image, new RectangleF(alx, imgY - 1, _image.Width, _image.Height));
                                    G.DrawString(Text, Font, br, new RectangleF(alx + _image.Width, 0, Width, Height), sf);
                                    break;
                                }
                            case ContentAlignment.MiddleRight:
                                {
                                    G.DrawImage(_image, new RectangleF(1, imgY - 1, _image.Width, _image.Height));
                                    using (StringFormat sf2 = base.TextAlign.ToStringFormat())
                                    {
                                        G.DrawString(Text, Font, br, new Rectangle(7, 0, Width, Height), sf2);
                                    }
                                    break;
                                }
                            case ContentAlignment.BottomLeft:
                                {
                                    G.DrawImage(_image, new RectangleF(1, imgY, _image.Width, _image.Height));
                                    using (StringFormat sf2 = ContentAlignment.MiddleLeft.ToStringFormat())
                                    {
                                        G.DrawString(Text, Font, br, new Rectangle(_image.Width + 1, 0, Width, Height), sf2);
                                    }
                                    break;
                                }
                        }
                    }
                }
            }
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Releases all cached GDI resources.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _penWindowFrame?.Dispose();
                _penShadow?.Dispose();
                _penDkShadow?.Dispose();
                _penHilight?.Dispose();
                _penLight?.Dispose();
                _penBackColor?.Dispose();
                _image?.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}