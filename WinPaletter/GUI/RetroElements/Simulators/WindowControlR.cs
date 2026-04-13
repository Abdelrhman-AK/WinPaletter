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
    /// A control that simulates a classic Windows 9x window client area with a sunken 3D border.
    /// </summary>
    public class WindowControlR : Control
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowControlR"/> class.
        /// </summary>
        public WindowControlR()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            ForeColor = SystemColors.WindowText;
            BackColor = SystemColors.Window;
        }

        #region Private fields

        // Tight StringFormat shared between measure and draw so glyph bounds are identical.
        private static readonly StringFormat MeasureFormat = new(StringFormat.GenericTypographic)
        {
            FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.MeasureTrailingSpaces
        };

        // Cached overlay blend color — recomputed only when BackColor changes.
        private Color _overlayColor;

        // Cached pens — rebuilt only when the corresponding color property changes.
        private Pen _penShadow = new(SystemColors.ButtonShadow);
        private Pen _penDkShadow = new(SystemColors.ControlDark);
        private Pen _penHilight = new(SystemColors.ButtonHighlight);
        private Pen _penLight = new(SystemColors.ControlLight);

        // Cached geometry — rebuilt on resize or text/font change.
        private Rectangle _rectControl;  // full control bounds (W-1, H-1)
        private Rectangle _rectFace;     // inner client area (inside the 2px border)
        private Rectangle _rectHitText;  // tight glyph bounds for caption hit-test

        // Cached invalidation rectangles — rebuilt with geometry.
        private Rectangle _invFace;
        private Rectangle _invText;
        private Rectangle _invDkShadow;
        private Rectangle _invShadow;
        private Rectangle _invHilight;
        private Rectangle _invLight;

        // Border line segments — each PointF[2] is one line: [start, end].
        // Two segments per color (horizontal + vertical).
        private PointF[] _dkShadowSeg0, _dkShadowSeg1;  // outer top, outer left
        private PointF[] _shadowSeg0, _shadowSeg1;     // inner top, inner left
        private PointF[] _hilightSeg0, _hilightSeg1;    // inner right, inner bottom
        private PointF[] _lightSeg0, _lightSeg1;      // outer right, outer bottom

        // Hover state — one flag per hittable region.
        private bool _cursorOnDkShadow;
        private bool _cursorOnShadow;
        private bool _cursorOnHilight;
        private bool _cursorOnLight;
        private bool _cursorOnFace;
        private bool _cursorOnText;

        #endregion

        #region Properties

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
                Invalidate(_invShadow.IsEmpty ? _rectControl : _invShadow);
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
                Invalidate(_invDkShadow.IsEmpty ? _rectControl : _invDkShadow);
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
                Invalidate(_invHilight.IsEmpty ? _rectControl : _invHilight);
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
                Invalidate(_invLight.IsEmpty ? _rectControl : _invLight);
            }
        }
        private Color _buttonLight = SystemColors.ControlLight;

        /// <summary>
        /// Gets or sets the client area background color.
        /// </summary>
        public new Color BackColor
        {
            get => base.BackColor;
            set
            {
                if (base.BackColor == value) return;
                base.BackColor = value;
                Invalidate(_invFace.IsEmpty ? _rectControl : _invFace);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether colors can be edited interactively by clicking.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingColors { get; set; } = false;

        // Computed edit-mode flags — read-only shorthands used in paint and click.
        private bool IsEditDkShadow => EnableEditingColors && _cursorOnDkShadow;
        private bool IsEditShadow => EnableEditingColors && _cursorOnShadow;
        private bool IsEditHilight => EnableEditingColors && _cursorOnHilight;
        private bool IsEditLight => EnableEditingColors && _cursorOnLight;
        private bool IsEditFace => EnableEditingColors && _cursorOnFace;
        private bool IsEditText => EnableEditingColors && _cursorOnText;

        #endregion

        #region Editor event

        /// <summary>
        /// Raised when the user clicks a color region while <see cref="EnableEditingColors"/> is true.
        /// </summary>
        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);

        /// <summary>
        /// Raised when the user clicks a color region while <see cref="EnableEditingColors"/> is true.
        /// </summary>
        public event EditorInvokerEventHandler EditorInvoker;

        #endregion

        #region Geometry and resource cache

        /// <summary>
        /// Rebuilds all cached rectangles, border segments, and text rect from the current size.
        /// </summary>
        private void RebuildGeometry()
        {
            int w = Width - 1;
            int h = Height - 1;

            _rectControl = new Rectangle(0, 0, w, h);

            // Client area sits inside the 2px sunken border on all sides.
            _rectFace = new Rectangle(2, 2, w - 4, h - 4);

            // Win9x sunken border layout — outermost to innermost, top-left then bottom-right:
            //   DkShadow  outer top + left   (darkest)
            //   Shadow    inner top + left   (medium dark)
            //   Hilight   inner bottom+right (bright)
            //   Light     outer bottom+right (lightest)
            _dkShadowSeg0 = [new Point(0, 0), new Point(w, 0)];  // outer top
            _dkShadowSeg1 = [new Point(0, 0), new Point(0, h)];  // outer left
            _shadowSeg0 = [new Point(1, 1), new Point(w - 1, 1)];  // inner top
            _shadowSeg1 = [new Point(1, 1), new Point(1, h - 1)];  // inner left
            _hilightSeg0 = [new Point(w - 1, 1), new Point(w - 1, h - 1)]; // inner right
            _hilightSeg1 = [new Point(1, h - 1), new Point(w - 1, h - 1)]; // inner bottom
            _lightSeg0 = [new Point(w, 0), new Point(w, h)];  // outer right
            _lightSeg1 = [new Point(0, h), new Point(w, h)];  // outer bottom

            RebuildTextRect();

            _invFace = InflateSafe(_rectFace, 2);
            _invText = InflateSafe(_rectHitText, 2);
            _invDkShadow = InflateSafe(GetUnionLineRect(_dkShadowSeg0, _dkShadowSeg1), 2);
            _invShadow = InflateSafe(GetUnionLineRect(_shadowSeg0, _shadowSeg1), 2);
            _invHilight = InflateSafe(GetUnionLineRect(_hilightSeg0, _hilightSeg1), 2);
            _invLight = InflateSafe(GetUnionLineRect(_lightSeg0, _lightSeg1), 2);
        }

        /// <summary>
        /// Measures the caption text and rebuilds the hit-test rect.
        /// Called independently when Text or Font changes without a resize.
        /// </summary>
        private void RebuildTextRect()
        {
            if (string.IsNullOrEmpty(Text) || !IsHandleCreated)
            {
                _rectHitText = Rectangle.Empty;
                return;
            }

            SizeF measured;
            using (Graphics G = Graphics.FromHwnd(Handle))
            {
                measured = G.MeasureString(Text, Font, int.MaxValue, MeasureFormat);
            }

            _rectHitText = new Rectangle(4, 4, (int)Math.Ceiling(measured.Width), (int)Math.Ceiling(measured.Height));
        }

        private static Rectangle InflateSafe(Rectangle r, int amount)
        {
            if (r.IsEmpty) return Rectangle.Empty;
            return r.InflateReturn(amount);
        }

        private static Rectangle GetUnionLineRect(PointF[] seg0, PointF[] seg1)
        {
            Rectangle r0 = GetLineRect(seg0);
            Rectangle r1 = GetLineRect(seg1);
            if (r0.IsEmpty) return r1;
            if (r1.IsEmpty) return r0;
            return Rectangle.Union(r0, r1);
        }

        private static Rectangle GetLineRect(PointF[] seg)
        {
            if (seg is null || seg.Length < 2) return Rectangle.Empty;
            int x1 = (int)seg[0].X;
            int y1 = (int)seg[0].Y;
            int x2 = (int)seg[1].X;
            int y2 = (int)seg[1].Y;
            int left = Math.Min(x1, x2);
            int top = Math.Min(y1, y2);
            int right = Math.Max(x1, x2);
            int bottom = Math.Max(y1, y2);
            return new Rectangle(left, top, (right - left) + 1, (bottom - top) + 1);
        }

        private static bool OnSegment(PointF[] seg, Point p)
        {
            PointF a = seg[0];
            PointF b = seg[1];

            // Horizontal line
            if (a.Y == b.Y)
                return p.Y == a.Y && p.X >= Math.Min(a.X, b.X) && p.X <= Math.Max(a.X, b.X);

            // Vertical line
            if (a.X == b.X)
                return p.X == a.X && p.Y >= Math.Min(a.Y, b.Y) && p.Y <= Math.Max(a.Y, b.Y);

            return false;
        }

        /// <summary>
        /// Disposes the existing pen and allocates a replacement with the given color.
        /// </summary>
        private static void ReplacePen(ref Pen pen, Color color)
        {
            pen?.Dispose();
            pen = new Pen(color);
        }

        #endregion

        #region Overrides — layout triggers

        /// <summary>
        /// Rebuilds geometry when the control is resized.
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RebuildGeometry();
            Invalidate();
        }

        /// <summary>
        /// Rebuilds the caption hit-test rect when the font changes.
        /// </summary>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (IsHandleCreated) RebuildTextRect();
        }

        /// <summary>
        /// Rebuilds the caption hit-test rect when the text changes.
        /// </summary>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (IsHandleCreated) RebuildTextRect();
        }

        /// <summary>
        /// Rebuilds the cached overlay blend color when BackColor changes.
        /// </summary>
        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            _overlayColor = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
            Invalidate();
        }

        #endregion

        #region Overrides — mouse interaction

        /// <summary>
        /// Clears all hover state and invalidates only when state actually changes.
        /// </summary>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (DesignMode || !EnableEditingColors) return;

            if (_cursorOnDkShadow || _cursorOnShadow || _cursorOnHilight || _cursorOnLight || _cursorOnFace || _cursorOnText)
            {
                _cursorOnDkShadow = false;
                _cursorOnShadow = false;
                _cursorOnHilight = false;
                _cursorOnLight = false;
                _cursorOnFace = false;
                _cursorOnText = false;
                Invalidate();
            }
        }

        /// <summary>
        /// Updates hover state using segment proximity for borders and rect containment for
        /// face and text. Invalidates only when any flag actually changes.
        /// Priority order matches visual layering: DkShadow → Shadow → Hilight → Light → Text → Face.
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (DesignMode || !EnableEditingColors) return;

            bool onDkShadow = OnSegment(_dkShadowSeg0, e.Location) || OnSegment(_dkShadowSeg1, e.Location);
            bool onShadow = !onDkShadow && (OnSegment(_shadowSeg0, e.Location) || OnSegment(_shadowSeg1, e.Location));
            bool onHilight = !onDkShadow && !onShadow && (OnSegment(_hilightSeg0, e.Location) || OnSegment(_hilightSeg1, e.Location));
            bool onLight = !onDkShadow && !onShadow && !onHilight && (OnSegment(_lightSeg0, e.Location) || OnSegment(_lightSeg1, e.Location));
            bool onText = !onDkShadow && !onShadow && !onHilight && !onLight && _rectHitText.Contains(e.Location);
            bool onFace = !onDkShadow && !onShadow && !onHilight && !onLight && !onText && _rectFace.Contains(e.Location);

            if (onDkShadow != _cursorOnDkShadow || onShadow != _cursorOnShadow || onHilight != _cursorOnHilight || onLight != _cursorOnLight || onFace != _cursorOnFace || onText != _cursorOnText)
            {
                _cursorOnDkShadow = onDkShadow;
                _cursorOnShadow = onShadow;
                _cursorOnHilight = onHilight;
                _cursorOnLight = onLight;
                _cursorOnFace = onFace;
                _cursorOnText = onText;
                Invalidate();
            }
        }

        /// <summary>
        /// Invokes the color editor for the region under the cursor.
        /// </summary>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (DesignMode || !EnableEditingColors) return;

            if (IsEditDkShadow) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonDkShadow)));
            else if (IsEditShadow) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonShadow)));
            else if (IsEditHilight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonHilight)));
            else if (IsEditLight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonLight)));
            else if (IsEditText) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.WindowText)));
            else if (IsEditFace) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.Window)));
        }

        #endregion

        #region Paint

        /// <summary>
        /// Paints the Win9x sunken 3D border, client area fill, caption text,
        /// and any active color-edit overlays.
        /// Draw order: background → face overlay → border → border overlays → text → text overlay.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighSpeed;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            // 1. Client area background.
            G.Clear(BackColor);

            // 2. Face hover overlay — drawn before the border so border lines render on top.
            if (IsEditFace)
            {
                using (HatchBrush hb = new(HatchStyle.Percent25, _overlayColor, Color.Transparent))
                {
                    G.FillRectangle(hb, _rectFace);
                }
            }

            // 3. Win9x sunken border — outermost to innermost, top-left then bottom-right.
            G.DrawLine(_penDkShadow, _dkShadowSeg0[0], _dkShadowSeg0[1]);
            G.DrawLine(_penDkShadow, _dkShadowSeg1[0], _dkShadowSeg1[1]);
            G.DrawLine(_penShadow, _shadowSeg0[0], _shadowSeg0[1]);
            G.DrawLine(_penShadow, _shadowSeg1[0], _shadowSeg1[1]);
            G.DrawLine(_penHilight, _hilightSeg0[0], _hilightSeg0[1]);
            G.DrawLine(_penHilight, _hilightSeg1[0], _hilightSeg1[1]);
            G.DrawLine(_penLight, _lightSeg0[0], _lightSeg0[1]);
            G.DrawLine(_penLight, _lightSeg1[0], _lightSeg1[1]);

            // 4. Border segment hover overlays — 2px saturated red line over the active segment.
            if (IsEditDkShadow || IsEditShadow || IsEditHilight || IsEditLight)
            {
                using (Pen p = new(Color.FromArgb(200, 128, 0, 0)))
                {
                    if (IsEditDkShadow)
                    {
                        G.DrawLine(p, _dkShadowSeg0[0], _dkShadowSeg0[1]);
                        G.DrawLine(p, _dkShadowSeg1[0], _dkShadowSeg1[1]);
                    }
                    if (IsEditShadow)
                    {
                        G.DrawLine(p, _shadowSeg0[0], _shadowSeg0[1]);
                        G.DrawLine(p, _shadowSeg1[0], _shadowSeg1[1]);
                    }
                    if (IsEditHilight)
                    {
                        G.DrawLine(p, _hilightSeg0[0], _hilightSeg0[1]);
                        G.DrawLine(p, _hilightSeg1[0], _hilightSeg1[1]);
                    }
                    if (IsEditLight)
                    {
                        G.DrawLine(p, _lightSeg0[0], _lightSeg0[1]);
                        G.DrawLine(p, _lightSeg1[0], _lightSeg1[1]);
                    }
                }
            }

            // 5. Caption text — same MeasureFormat as RebuildTextRect so glyphs land in _rectHitText.
            if (!string.IsNullOrEmpty(Text))
            {
                using (SolidBrush br = new(ForeColor))
                {
                    G.DrawString(Text, Font, br, new PointF(4f, 4f), MeasureFormat);
                }
            }

            // 6. Caption text hover overlay.
            if (IsEditText)
            {
                using (HatchBrush hb = new(HatchStyle.Percent25, _overlayColor, Color.Transparent))
                using (Pen p = new(_overlayColor))
                {
                    G.FillRectangle(hb, _rectHitText);
                    G.DrawRectangle(p, _rectHitText);
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
                _penShadow?.Dispose();
                _penDkShadow?.Dispose();
                _penHilight?.Dispose();
                _penLight?.Dispose();
                MeasureFormat?.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}