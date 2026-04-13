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
    /// A control that simulates a classic Windows 9x context menu with three items:
    /// Normal, Selected, and Disabled. Supports interactive color editing.
    /// </summary>
    [Description("Retro context menu with Windows 9x style")]
    public class ContextMenuR : Panel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContextMenuR"/> class.
        /// </summary>
        public ContextMenuR()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            Font = new("Microsoft Sans Serif", 8f);
            BackColor = SystemColors.Menu;
            ForeColor = SystemColors.MenuText;
            BorderStyle = BorderStyle.None;
        }

        #region Private fields

        // Localized item strings — evaluated on each access so locale changes apply immediately.
        private string StrMenuItem => Program.Localization.Strings.Previewer.MenuItem;
        private string StrSelection => Program.Localization.Strings.Previewer.Selection;
        private string StrDisabledItem => Program.Localization.Strings.Previewer.DisabledItem;

        // Cached geometry — rebuilt on resize or font change.
        private Rectangle _rectOuter;    // full control bounds (W-1, H-1)
        private Rectangle _rectBorder;   // ColorBorder rect  (2,2,W-5,H-5)
        private Rectangle _item0;        // Normal item row
        private Rectangle _item1;        // Selected item row
        private Rectangle _item2;        // Disabled item row
        private Rectangle _item0Text;    // Normal text hit-test bounds
        private Rectangle _item1Text;    // Selected text hit-test bounds
        private Rectangle _item2Text;    // Disabled text hit-test bounds

        // Cached invalidation rectangles — rebuilt with geometry.
        private Rectangle _invAll;
        private Rectangle _invOuter;
        private Rectangle _invBorder;
        private Rectangle _invSelection;
        private Rectangle _invSelectionText;
        private Rectangle _invItemText;
        private Rectangle _invGrayText;
        private Rectangle _invLight;
        private Rectangle _invHilight;
        private Rectangle _invShadow;
        private Rectangle _invDkShadow;

        // 3D border segment endpoints (raised Win9x appearance).
        // Layout: Light (outer top+left) → Hilight (inner top+left)
        //       → Shadow (inner bottom+right) → DkShadow (outer bottom+right)
        private PointF[] _lightSeg0, _lightSeg1;
        private PointF[] _hilightSeg0, _hilightSeg1;
        private PointF[] _shadowSeg0, _shadowSeg1;
        private PointF[] _dkShadowSeg0, _dkShadowSeg1;

        // Cached pens — rebuilt only when their source color changes.
        private Pen _penShadow;
        private Pen _penDkShadow;
        private Pen _penHilight;
        private Pen _penLight;
        private Pen _penMenuHilight;   // flat-mode selection border

        // Cached brushes — rebuilt only when their source color changes.
        private SolidBrush _brushFore;
        private SolidBrush _brushHilight;
        private SolidBrush _brushHilightText;
        private SolidBrush _brushGrayText;

        // Cached overlay blend color — recomputed only when BackColor changes.
        private Color _overlayColor;

        // Shared border segment highlight color.
        private static readonly Color BorderOverlay = Color.FromArgb(200, 128, 0, 0);

        // Hover state — one flag per hittable region.
        private bool _cursorOnShadow;
        private bool _cursorOnDkShadow;
        private bool _cursorOnHilight;
        private bool _cursorOnLight;
        private bool _cursorOnColorBorder;
        private bool _cursorOnFace;
        private bool _cursorOnSelHilight;
        private bool _cursorOnSelMenuHilight;
        private bool _cursorOnSelText;
        private bool _cursorOnItemText;
        private bool _cursorOnGrayText;

        // Computed edit-mode flags.
        private bool IsEditShadow => EnableEditingColors && _cursorOnShadow;
        private bool IsEditDkShadow => EnableEditingColors && _cursorOnDkShadow;
        private bool IsEditHilight => EnableEditingColors && _cursorOnHilight;
        private bool IsEditLight => EnableEditingColors && _cursorOnLight;
        private bool IsEditColorBorder => EnableEditingColors && _cursorOnColorBorder;
        private bool IsEditFace => EnableEditingColors && _cursorOnFace;
        private bool IsEditSelHilight => EnableEditingColors && _cursorOnSelHilight;
        private bool IsEditSelMenuHilight => EnableEditingColors && _cursorOnSelMenuHilight;
        private bool IsEditSelText => EnableEditingColors && _cursorOnSelText;
        private bool IsEditItemText => EnableEditingColors && _cursorOnItemText;
        private bool IsEditGrayText => EnableEditingColors && _cursorOnGrayText;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the menu background (face) color.
        /// </summary>
        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                if (base.BackColor == value) return;
                base.BackColor = value;
                _overlayColor = Color.FromArgb(100, value.IsDark() ? Color.White : Color.Black);
                Invalidate(_invBorder.IsEmpty ? _invAll : _invBorder);
            }
        }

        /// <summary>
        /// Gets or sets the normal menu item text color.
        /// </summary>
        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                if (base.ForeColor == value) return;
                base.ForeColor = value;
                ReplaceBrush(ref _brushFore, value);
                Invalidate(_invItemText.IsEmpty ? _invAll : _invItemText);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the menu renders in flat (XP-style) mode.
        /// </summary>
        public bool Flat
        {
            get => _flat;
            set { if (_flat != value) { _flat = value; Invalidate(_invAll.IsEmpty ? _invOuter : _invAll); } }
        }
        private bool _flat = false;

        /// <summary>
        /// Gets or sets the button shadow color (inner bottom/right in non-flat mode).
        /// </summary>
        public Color ButtonShadow
        {
            get => _buttonShadow;
            set
            {
                if (_buttonShadow == value) return;
                _buttonShadow = value;
                ReplacePen(ref _penShadow, value);
                Invalidate(_flat ? _invOuter : (_invShadow.IsEmpty ? _invAll : _invShadow));
            }
        }
        private Color _buttonShadow = SystemColors.ButtonShadow;

        /// <summary>
        /// Gets or sets the button dark shadow color (outer bottom/right in non-flat mode).
        /// </summary>
        public Color ButtonDkShadow
        {
            get => _buttonDkShadow;
            set
            {
                if (_buttonDkShadow == value) return;
                _buttonDkShadow = value;
                ReplacePen(ref _penDkShadow, value);
                Invalidate(_flat ? _invOuter : (_invDkShadow.IsEmpty ? _invAll : _invDkShadow));
            }
        }
        private Color _buttonDkShadow = SystemColors.ControlDark;

        /// <summary>
        /// Gets or sets the button hilight color (inner top/left in non-flat mode).
        /// </summary>
        public Color ButtonHilight
        {
            get => _buttonHilight;
            set
            {
                if (_buttonHilight == value) return;
                _buttonHilight = value;
                ReplacePen(ref _penHilight, value);
                Invalidate(_flat ? _invOuter : (_invHilight.IsEmpty ? _invAll : _invHilight));
            }
        }
        private Color _buttonHilight = SystemColors.ButtonHighlight;

        /// <summary>
        /// Gets or sets the button light color (outer top/left in non-flat mode).
        /// </summary>
        public Color ButtonLight
        {
            get => _buttonLight;
            set
            {
                if (_buttonLight == value) return;
                _buttonLight = value;
                ReplacePen(ref _penLight, value);
                Invalidate(_flat ? _invOuter : (_invLight.IsEmpty ? _invAll : _invLight));
            }
        }
        private Color _buttonLight = SystemColors.ControlLight;

        /// <summary>
        /// Gets or sets the selection highlight fill color.
        /// </summary>
        public Color Hilight
        {
            get => _hilight;
            set
            {
                if (_hilight == value) return;
                _hilight = value;
                ReplaceBrush(ref _brushHilight, value);
                Invalidate(_invSelection.IsEmpty ? _invAll : _invSelection);
            }
        }
        private Color _hilight = SystemColors.Highlight;

        /// <summary>
        /// Gets or sets the selected item text color.
        /// </summary>
        public Color HilightText
        {
            get => _hilightText;
            set
            {
                if (_hilightText == value) return;
                _hilightText = value;
                ReplaceBrush(ref _brushHilightText, value);
                Invalidate(_invSelectionText.IsEmpty ? _invSelection : _invSelectionText);
            }
        }
        private Color _hilightText = SystemColors.HighlightText;

        /// <summary>
        /// Gets or sets the selected item border color in flat mode.
        /// </summary>
        public Color MenuHilight
        {
            get => _menuHilight;
            set
            {
                if (_menuHilight == value) return;
                _menuHilight = value;
                ReplacePen(ref _penMenuHilight, value);
                Invalidate(_invSelection.IsEmpty ? _invAll : _invSelection);
            }
        }
        private Color _menuHilight = SystemColors.MenuHighlight;

        /// <summary>
        /// Gets or sets the disabled item text color.
        /// </summary>
        public Color GrayText
        {
            get => _grayText;
            set
            {
                if (_grayText == value) return;
                _grayText = value;
                ReplaceBrush(ref _brushGrayText, value);
                Invalidate(_invGrayText.IsEmpty ? _invAll : _invGrayText);
            }
        }
        private Color _grayText = SystemColors.GrayText;

        /// <summary>
        /// Gets or sets a value indicating whether colors can be edited interactively by clicking.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingColors { get; set; } = false;

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

        #region Resource helpers

        private static void ReplacePen(ref Pen pen, Color color)
        {
            pen?.Dispose();
            pen = new Pen(color);
        }

        private static void ReplaceBrush(ref SolidBrush brush, Color color)
        {
            brush?.Dispose();
            brush = new SolidBrush(color);
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
        /// Draws a Percent25 hatch fill and a 1px outline over <paramref name="r"/>
        /// using the given <paramref name="overlayColor"/>.
        /// </summary>
        private static void DrawOverlay(Graphics g, Rectangle r, Color overlayColor)
        {
            using (HatchBrush hb = new(HatchStyle.Percent25, overlayColor, Color.Transparent))
            using (Pen p = new(overlayColor))
            {
                g.FillRectangle(hb, r);
                g.DrawRectangle(p, r);
            }
        }

        #endregion

        #region Geometry cache

        /// <summary>
        /// Recomputes all cached rectangles and border segment endpoints, then
        /// self-sizes the control height to fit the three item rows exactly.
        /// </summary>
        private void RebuildGeometry()
        {
            SizeF s0 = StrMenuItem.Measure(Font);
            SizeF s1 = StrSelection.Measure(Font);
            SizeF s2 = StrDisabledItem.Measure(Font);

            int rowH = (int)s0.Height + 4;

            // Self-size height to fit exactly three rows plus the 2px 3D border on each side.
            Height = rowH + (rowH + 1) + rowH + 2 + 2 + 2 + 2;

            float w = Width - 1f;
            float h = Height - 1f;

            _rectOuter = new Rectangle(0, 0, (int)w, (int)h);
            _rectBorder = new Rectangle(2, 2, Width - 5, Height - 5);

            int ix = _rectBorder.X + 1;
            int iw = _rectBorder.Width - 2;

            _item0 = new Rectangle(ix, _rectBorder.Y + 1, iw, rowH);
            _item1 = new Rectangle(ix, _item0.Bottom + 1, iw, rowH + 1);
            _item2 = new Rectangle(ix, _item1.Bottom + 1, iw, rowH);

            // 15px left indent leaves room for a menu icon gutter; text is vertically centered.
            _item0Text = new Rectangle(_item0.X + 15, _item0.Y + (_item0.Height - (int)s0.Height) / 2, (int)s0.Width + 2, (int)s0.Height);
            _item1Text = new Rectangle(_item1.X + 15, _item1.Y + (_item1.Height - (int)s1.Height) / 2, (int)s1.Width + 2, (int)s1.Height);
            _item2Text = new Rectangle(_item2.X + 15, _item2.Y + (_item2.Height - (int)s2.Height) / 2, (int)s2.Width + 2, (int)s2.Height);

            // Raised 3D border segments — outermost to innermost, top-left then bottom-right.
            _lightSeg0 = [new(0, 0), new(w - 1f, 0)];  // outer top
            _lightSeg1 = [new(0, 0), new(0, h - 1f)];  // outer left
            _hilightSeg0 = [new(1, 1), new(w - 2f, 1)];  // inner top
            _hilightSeg1 = [new(1, 1), new(1, h - 2f)];  // inner left
            _shadowSeg0 = [new(w - 1f, 1), new(w - 1f, h - 1f)];  // inner right
            _shadowSeg1 = [new(1, h - 1f), new(w - 1f, h - 1f)]; // inner bottom
            _dkShadowSeg0 = [new(w, 0), new(w, h)];  // outer right
            _dkShadowSeg1 = [new(0, h), new(w, h)];  // outer bottom

            _invOuter = InflateSafe(_rectOuter, 2);
            _invBorder = InflateSafe(_rectBorder, 2);
            _invSelection = InflateSafe(_item1, 2);
            _invSelectionText = InflateSafe(_item1Text, 2);
            _invItemText = InflateSafe(_item0Text, 2);
            _invGrayText = InflateSafe(_item2Text, 2);
            _invLight = InflateSafe(GetUnionLineRect(_lightSeg0, _lightSeg1), 2);
            _invHilight = InflateSafe(GetUnionLineRect(_hilightSeg0, _hilightSeg1), 2);
            _invShadow = InflateSafe(GetUnionLineRect(_shadowSeg0, _shadowSeg1), 2);
            _invDkShadow = InflateSafe(GetUnionLineRect(_dkShadowSeg0, _dkShadowSeg1), 2);
            _invAll = _invOuter.IsEmpty ? _invBorder : _invOuter;
        }

        #endregion

        #region Overrides — layout triggers

        /// <summary>
        /// Rebuilds geometry when the font changes.
        /// </summary>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            RebuildGeometry();
        }

        /// <summary>
        /// Rebuilds geometry when the control is resized.
        /// </summary>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            RebuildGeometry();
        }

        /// <summary>
        /// Rebuilds the cached overlay blend color when BackColor changes.
        /// </summary>
        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            _overlayColor = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
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

        #endregion

        #region Overrides — mouse interaction

        /// <summary>
        /// Updates hover state using segment proximity for 1px border lines and rect
        /// containment for area zones. Invalidates only when any flag actually changes.
        /// Priority: DkShadow → Shadow → Hilight → Light → ColorBorder →
        ///           SelText → SelMenuHilight → SelHilight → ItemText → GrayText → Face.
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (DesignMode || !EnableEditingColors) return;

            bool onDkShadow, onShadow, onHilight, onLight, onColorBorder;

            if (_flat)
            {
                // Flat mode: single shadow rect is the only border — no 3D layers, no ColorBorder strip.
                onShadow = _rectOuter.BordersContains(e.Location);
                onDkShadow = false;
                onHilight = false;
                onLight = false;
                onColorBorder = false;
            }
            else
            {
                // Non-flat: four-layer 3D edge + ColorBorder strip inside it.
                onDkShadow = OnSegment(_dkShadowSeg0, e.Location) || OnSegment(_dkShadowSeg1, e.Location);
                onShadow = !onDkShadow && (OnSegment(_shadowSeg0, e.Location) || OnSegment(_shadowSeg1, e.Location));
                onHilight = !onDkShadow && !onShadow && (OnSegment(_hilightSeg0, e.Location) || OnSegment(_hilightSeg1, e.Location));
                onLight = !onDkShadow && !onShadow && !onHilight && (OnSegment(_lightSeg0, e.Location) || OnSegment(_lightSeg1, e.Location));
                onColorBorder = !onDkShadow && !onShadow && !onHilight && !onLight && _rectBorder.BordersContains(e.Location);
            }

            // Selection item zone — priority order: text > menu-hilight border > fill.
            bool onSelText = _item1Text.Contains(e.Location);
            bool onSelMH = !onSelText && _flat && _item1.BordersContains(e.Location);
            bool onSelH = !onSelText && !onSelMH && _item1.Contains(e.Location);

            bool onItemText = !onSelText && !onSelH && _item0Text.Contains(e.Location);
            bool onGrayText = !onSelText && !onSelH && _item2Text.Contains(e.Location);

            bool onFace = !onDkShadow && !onShadow && !onHilight && !onLight && !onColorBorder && !onSelH && !onSelMH && !onSelText && !onItemText && !onGrayText && _rectBorder.Contains(e.Location);

            if (onDkShadow != _cursorOnDkShadow || onShadow != _cursorOnShadow || onHilight != _cursorOnHilight || onLight != _cursorOnLight || onColorBorder != _cursorOnColorBorder || onFace != _cursorOnFace || onSelH != _cursorOnSelHilight || onSelMH != _cursorOnSelMenuHilight || onSelText != _cursorOnSelText || onItemText != _cursorOnItemText || onGrayText != _cursorOnGrayText)
            {
                _cursorOnDkShadow = onDkShadow;
                _cursorOnShadow = onShadow;
                _cursorOnHilight = onHilight;
                _cursorOnLight = onLight;
                _cursorOnColorBorder = onColorBorder;
                _cursorOnFace = onFace;
                _cursorOnSelHilight = onSelH;
                _cursorOnSelMenuHilight = onSelMH;
                _cursorOnSelText = onSelText;
                _cursorOnItemText = onItemText;
                _cursorOnGrayText = onGrayText;
                Invalidate();
            }
        }

        /// <summary>
        /// Clears all hover state and invalidates only when any flag was set.
        /// </summary>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (DesignMode || !EnableEditingColors) return;

            if (_cursorOnDkShadow || _cursorOnShadow || _cursorOnHilight || _cursorOnLight || _cursorOnColorBorder || _cursorOnFace || _cursorOnSelHilight || _cursorOnSelMenuHilight || _cursorOnSelText || _cursorOnItemText || _cursorOnGrayText)
            {
                _cursorOnDkShadow = false;
                _cursorOnShadow = false;
                _cursorOnHilight = false;
                _cursorOnLight = false;
                _cursorOnColorBorder = false;
                _cursorOnFace = false;
                _cursorOnSelHilight = false;
                _cursorOnSelMenuHilight = false;
                _cursorOnSelText = false;
                _cursorOnItemText = false;
                _cursorOnGrayText = false;
                Invalidate();
            }
        }

        /// <summary>
        /// Invokes the color editor for the topmost region under the cursor.
        /// </summary>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (DesignMode || !EnableEditingColors) return;

            if (IsEditDkShadow) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonDkShadow)));
            else if (IsEditShadow) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonShadow)));
            else if (IsEditHilight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonHilight)));
            else if (IsEditLight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonLight)));
            else if (IsEditColorBorder) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ActiveBorder)));
            else if (IsEditSelText) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.HilightText)));
            else if (IsEditSelMenuHilight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.MenuHilight)));
            else if (IsEditSelHilight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.Hilight)));
            else if (IsEditItemText) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonText)));
            else if (IsEditGrayText) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.GrayText)));
            else if (IsEditFace) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonFace)));
        }

        #endregion

        #region Paint

        /// <summary>
        /// Paints the context menu: background, 3D border, ColorBorder strip,
        /// selection highlight, item text, and all active color-edit overlays.
        /// Draw order: background → face overlay → 3D border → ColorBorder strip
        ///           → selection fill → overlays → text.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighSpeed;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            // 1. Background.
            G.Clear(BackColor);

            // 2. Face hover overlay — covers the inner content area only.
            if (IsEditFace)
            {
                using (HatchBrush hb = new(HatchStyle.Percent25, _overlayColor, Color.Transparent))
                {
                    G.FillRectangle(hb, _rectBorder);
                }
            }

            // 3. Border — mutually exclusive: either flat (single rect) or 3D (four layers + ColorBorder strip).
            if (_flat)
            {
                // Flat mode: one-pixel shadow outline around the whole control.
                if (_penShadow != null) G.DrawRectangle(_penShadow, _rectOuter);

                if (IsEditShadow)
                {
                    using (Pen p = new(BorderOverlay))
                    {
                        G.DrawRectangle(p, _rectOuter);
                    }
                }
            }
            else
            {
                // 3D mode: four border layers drawn outermost to innermost.
                // Draw order: Light (outer top+left) → Hilight (inner top+left)
                //           → Shadow (inner bottom+right) → DkShadow (outer bottom+right).
                if (_penLight != null) { G.DrawLine(_penLight, _lightSeg0[0], _lightSeg0[1]); G.DrawLine(_penLight, _lightSeg1[0], _lightSeg1[1]); }
                if (_penHilight != null) { G.DrawLine(_penHilight, _hilightSeg0[0], _hilightSeg0[1]); G.DrawLine(_penHilight, _hilightSeg1[0], _hilightSeg1[1]); }
                if (_penShadow != null) { G.DrawLine(_penShadow, _shadowSeg0[0], _shadowSeg0[1]); G.DrawLine(_penShadow, _shadowSeg1[0], _shadowSeg1[1]); }
                if (_penDkShadow != null) { G.DrawLine(_penDkShadow, _dkShadowSeg0[0], _dkShadowSeg0[1]); G.DrawLine(_penDkShadow, _dkShadowSeg1[0], _dkShadowSeg1[1]); }

                // 3D border segment hover overlays.
                if (IsEditLight || IsEditHilight || IsEditShadow || IsEditDkShadow)
                {
                    using (Pen p = new(BorderOverlay))
                    {
                        if (IsEditLight) { G.DrawLine(p, _lightSeg0[0], _lightSeg0[1]); G.DrawLine(p, _lightSeg1[0], _lightSeg1[1]); }
                        if (IsEditHilight) { G.DrawLine(p, _hilightSeg0[0], _hilightSeg0[1]); G.DrawLine(p, _hilightSeg1[0], _hilightSeg1[1]); }
                        if (IsEditShadow) { G.DrawLine(p, _shadowSeg0[0], _shadowSeg0[1]); G.DrawLine(p, _shadowSeg1[0], _shadowSeg1[1]); }
                        if (IsEditDkShadow) { G.DrawLine(p, _dkShadowSeg0[0], _dkShadowSeg0[1]); G.DrawLine(p, _dkShadowSeg1[0], _dkShadowSeg1[1]); }
                    }
                }
            }

            // 4. Selection highlight fill — always drawn regardless of flat/3D mode.
            if (_brushHilight != null) G.FillRectangle(_brushHilight, _item1);

            // Flat mode adds a MenuHilight border around the selection rect.
            if (_flat && _penMenuHilight != null) G.DrawRectangle(_penMenuHilight, _item1);

            // 5. Selection highlight overlays.
            if (IsEditSelHilight)
            {
                Color ov = Color.FromArgb(100, _hilight.IsDark() ? Color.White : Color.Black);
                using (HatchBrush hb = new(HatchStyle.Percent25, ov, Color.Transparent))
                using (Pen p = new(ov))
                {
                    G.FillRectangle(hb, _item1);
                    G.DrawRectangle(p, _item1);
                }
            }

            if (IsEditSelMenuHilight)
            {
                using (Pen p = new(BorderOverlay))
                {
                    G.DrawRectangle(p, _item1);
                }
            }

            // 6. Text region overlays — drawn before text so labels render on top.
            if (IsEditSelText)
            {
                Color ov = Color.FromArgb(100, _hilight.IsDark() ? Color.White : Color.Black);
                DrawOverlay(G, _item1Text, ov);
            }

            if (IsEditItemText) DrawOverlay(G, _item0Text, _overlayColor);
            if (IsEditGrayText) DrawOverlay(G, _item2Text, _overlayColor);

            // 7. Item text.
            using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat())
            {
                if (_brushFore != null) G.DrawString(StrMenuItem, Font, _brushFore, _item0Text, sf);
                if (_brushHilightText != null) G.DrawString(StrSelection, Font, _brushHilightText, _item1Text, sf);
                if (_brushGrayText != null) G.DrawString(StrDisabledItem, Font, _brushGrayText, _item2Text, sf);
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
                _penMenuHilight?.Dispose();
                _brushFore?.Dispose();
                _brushHilight?.Dispose();
                _brushHilightText?.Dispose();
                _brushGrayText?.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}