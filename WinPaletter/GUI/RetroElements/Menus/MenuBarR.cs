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
    /// A retro menu bar simulator with three items: Normal, Disabled, and Selected.
    /// Supports interactive color and metric editing.
    /// </summary>
    public class MenuBarR : Panel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuBarR"/> class.
        /// </summary>
        public MenuBarR()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            Font = new("Microsoft Sans Serif", 8f);
            BackColor = SystemColors.Control;
            ForeColor = SystemColors.ControlText;
        }

        #region Private fields

        // Localized item strings — evaluated lazily on each access so locale changes apply immediately.
        private string NormalStr => Program.Localization.Strings.Previewer.Normal;
        private string DisabledStr => Program.Localization.Strings.Previewer.Disabled;
        private string SelectedStr => Program.Localization.Strings.Previewer.Selected;

        // Cached geometry — rebuilt on resize, font change, or metric change.
        private Rectangle _rect;        // full control bounds
        private Rectangle _item0;       // Normal item bounds
        private Rectangle _item1;       // Disabled item bounds
        private Rectangle _item2;       // Selected item bounds
        private Rectangle _item0Text;   // Normal text hit-test bounds
        private Rectangle _item1Text;   // Disabled text hit-test bounds
        private Rectangle _item2Text;   // Selected text hit-test bounds
        private Rectangle _grip;        // resize grip bounds

        // Border segment endpoints for the selected item (non-flat mode).
        private PointF[] _shadowSeg0, _shadowSeg1;   // top, left
        private PointF[] _hilightSeg0, _hilightSeg1;  // right, bottom

        // Cached pens — rebuilt only when their source color changes.
        private Pen _penShadow;
        private Pen _penHilight;
        private Pen _penMenuHilight;

        // Cached brushes — rebuilt only when their source color changes.
        private SolidBrush _brushFore;
        private SolidBrush _brushDisabled;
        private SolidBrush _brushSelected;
        private SolidBrush _brushHilight;

        // Cached overlay blend color — recomputed when BackColor changes.
        private Color _overlayColor;

        // Grip drag state.
        private bool _draggingGrip;

        // Hover state — color editing.
        private bool _cursorOnFace;
        private bool _cursorOnShadow;
        private bool _cursorOnHilight;
        private bool _cursorOnText0;
        private bool _cursorOnText1;
        private bool _cursorOnGrayText;
        private bool _cursorOnMenuHilight;
        private bool _cursorOnSelHilight;
        private bool _cursorOnSelText;

        // Hover state — metric editing.
        private bool _cursorOnControl;
        private bool _cursorOnGrip;

        // Computed edit-mode flags.
        private bool IsEditFace => EnableEditingColors && _cursorOnFace;
        private bool IsEditShadow => EnableEditingColors && _cursorOnShadow;
        private bool IsEditHilight => EnableEditingColors && _cursorOnHilight;
        private bool IsEditText => EnableEditingColors && _cursorOnText0;
        private bool IsEditText1 => EnableEditingColors && _cursorOnText1;
        private bool IsEditGrayText => EnableEditingColors && _cursorOnGrayText;
        private bool IsEditMenuHilight => EnableEditingColors && _cursorOnMenuHilight;
        private bool IsEditSelHilight => EnableEditingColors && _cursorOnSelHilight;
        private bool IsEditSelText => EnableEditingColors && _cursorOnSelText;
        private bool IsMetricsHeight => !DesignMode && EnableEditingMetrics && (_cursorOnControl || _draggingGrip);
        private bool IsMetricsText => EnableEditingMetrics && _cursorOnText0;
        private bool IsMetricsGrayText => EnableEditingMetrics && _cursorOnGrayText;

        // Border segment overlay color — shared constant.
        private static readonly Color BorderOverlay = Color.FromArgb(200, 128, 0, 0);

        // Grip size in pixels.
        private const int GripSize = 6;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the background (button face) color.
        /// </summary>
        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                if (base.BackColor == value) return;
                base.BackColor = value;
                _overlayColor = Color.FromArgb(100, value.IsDark() ? Color.White : Color.Black);
                Invalidate();
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
                UpdateSelectedBrush();
                Invalidate();
            }
        }

        /// <summary>
        /// Gets the top-left corner of the Selected item — used by parent controls to anchor a drop-down.
        /// </summary>
        public Point SelectedItemLocation => _previewSelectionItem ? new Point(_item2.Left, _item2.Top) : Point.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the menu bar renders in flat (XP-style) mode.
        /// </summary>
        public bool Flat
        {
            get => _flat;
            set { if (_flat != value) { _flat = value; UpdateSelectedBrush(); Invalidate(); } }
        }
        private bool _flat = false;

        /// <summary>
        /// Gets or sets the menu bar background color used in flat mode.
        /// </summary>
        public Color MenuBar
        {
            get => _menuBar;
            set { if (_menuBar != value) { _menuBar = value; Invalidate(); } }
        }
        private Color _menuBar = SystemColors.MenuBar;

        /// <summary>
        /// Gets or sets the button hilight color used for the selected item border in non-flat mode.
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
        /// Gets or sets the button shadow color used for the selected item border in non-flat mode.
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
        /// Gets or sets the highlight fill color for the selected item in flat mode.
        /// </summary>
        public Color Hilight
        {
            get => _hilight;
            set
            {
                if (_hilight == value) return;
                _hilight = value;
                ReplaceBrush(ref _brushHilight, value);
                Invalidate();
            }
        }
        private Color _hilight = SystemColors.Highlight;

        /// <summary>
        /// Gets or sets the text color of the selected item in flat mode.
        /// </summary>
        public Color HilightText
        {
            get => _hilightText;
            set
            {
                if (_hilightText == value) return;
                _hilightText = value;
                UpdateSelectedBrush();
                Invalidate();
            }
        }
        private Color _hilightText = SystemColors.HighlightText;

        /// <summary>
        /// Gets or sets the border color of the selected item in flat mode.
        /// </summary>
        public Color MenuHilight
        {
            get => _menuHilight;
            set
            {
                if (_menuHilight == value) return;
                _menuHilight = value;
                ReplacePen(ref _penMenuHilight, value);
                Invalidate();
            }
        }
        private Color _menuHilight = SystemColors.MenuHighlight;

        /// <summary>
        /// Gets or sets the disabled (grayed) text color.
        /// </summary>
        public Color GrayText
        {
            get => _grayText;
            set
            {
                if (_grayText == value) return;
                _grayText = value;
                ReplaceBrush(ref _brushDisabled, value);
                Invalidate();
            }
        }
        private Color _grayText = SystemColors.GrayText;

        /// <summary>
        /// Gets or sets the menu bar height in pixels. Clamped to [15, 50].
        /// </summary>
        public int MenuHeight
        {
            get => _menuHeight;
            set
            {
                value = Math.Max(15, Math.Min(50, value));
                if (value == _menuHeight) return;
                _menuHeight = value;
                RebuildGeometry();
                Invalidate();
                if (!DesignMode && EnableEditingMetrics && _draggingGrip)
                    EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(MenuHeight)));
            }
        }
        private int _menuHeight = SystemInformation.MenuHeight;

        /// <summary>
        /// Gets or sets a value indicating whether the Selected item is rendered.
        /// </summary>
        public bool PreviewSelectionItem
        {
            get => _previewSelectionItem;
            set { if (_previewSelectionItem != value) { _previewSelectionItem = value; Invalidate(); } }
        }
        private bool _previewSelectionItem = true;

        /// <summary>
        /// Gets or sets a value indicating whether colors can be edited interactively.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingColors { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether metrics can be edited interactively.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingMetrics { get; set; } = false;

        #endregion

        #region Editor event

        /// <summary>
        /// Raised when the user clicks a color or metric region while editing is enabled.
        /// </summary>
        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);

        /// <summary>
        /// Raised when the user clicks a color or metric region while editing is enabled.
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

        /// <summary>
        /// Rebuilds <see cref="_brushSelected"/> from the current flat mode and color settings.
        /// </summary>
        private void UpdateSelectedBrush()
        {
            ReplaceBrush(ref _brushSelected, _flat ? _hilightText : ForeColor);
        }

        #endregion

        #region Geometry cache

        /// <summary>
        /// Recomputes all cached rectangles, segment endpoints, and grip position
        /// from the current control size, font, and metric values.
        /// </summary>
        private void RebuildGeometry()
        {
            Height = Math.Max(_menuHeight, Font.Height);

            SizeF s0 = NormalStr.Measure(Font);
            SizeF s1 = DisabledStr.Measure(Font);
            SizeF s2 = SelectedStr.Measure(Font);

            _item0 = new Rectangle(0, 0, (int)s0.Width, Height);
            _item1 = new Rectangle(_item0.Right, 0, (int)s1.Width, Height);
            _item2 = new Rectangle(_item1.Right, 0, (int)s2.Width, Height);

            // Text hit-test rects are shrunk by 6px on each axis so there is always
            // a clickable background margin around each label.
            _item0Text = CenterText(_item0, s0);
            _item1Text = CenterText(_item1, s1);
            _item2Text = CenterText(_item2, s2);

            _rect = new Rectangle(0, 0, Width - 1, Height - 1);

            // Selected item 3D border segments (non-flat mode):
            //   Shadow  = top + left   (pressed appearance)
            //   Hilight = right + bottom
            _shadowSeg0 = [new(_item2.Left, _item2.Top), new(_item2.Right - 1, _item2.Top)];
            _shadowSeg1 = [new(_item2.Left, _item2.Top), new(_item2.Left, _item2.Bottom - 1)];
            _hilightSeg0 = [new(_item2.Right - 1, _item2.Top), new(_item2.Right - 1, _item2.Bottom - 1)];
            _hilightSeg1 = [new(_item2.Left, _item2.Bottom - 1), new(_item2.Right - 1, _item2.Bottom - 1)];

            _grip = new Rectangle(_rect.X + _rect.Width / 2 - GripSize / 2, _rect.Bottom - GripSize, GripSize, GripSize);
        }

        /// <summary>
        /// Returns a rectangle centered inside <paramref name="item"/> that is
        /// 6px narrower and shorter than the measured text size.
        /// </summary>
        private static Rectangle CenterText(Rectangle item, SizeF measured)
        {
            int tw = (int)(measured.Width - 6);
            int th = (int)(measured.Height - 6);
            return new Rectangle(item.X + (item.Width - tw) / 2, item.Y + (item.Height - th) / 2, tw, th);
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
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RebuildGeometry();
        }

        /// <summary>
        /// Rebuilds the cached overlay color when BackColor changes.
        /// </summary>
        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            _overlayColor = Color.FromArgb(100, BackColor.IsDark() ? Color.White : Color.Black);
        }

        #endregion

        #region Overrides — mouse interaction

        /// <summary>
        /// Updates hover state and invalidates only when any flag changes.
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (DesignMode) return;

            if (_draggingGrip)
            {
                MenuHeight = e.Location.Y;
                Cursor = Cursors.SizeNS;
                return;
            }

            if (EnableEditingColors)
            {
                bool t0 = _item0Text.Contains(e.Location);
                bool t1 = !_flat && _previewSelectionItem && _item2Text.Contains(e.Location);
                bool st = _previewSelectionItem && _flat && _item2Text.Contains(e.Location);
                bool gt = _item1Text.Contains(e.Location);
                bool mh = _previewSelectionItem && _flat && _item2.BordersContains(e.Location);
                bool sh = _previewSelectionItem && _flat && _item2.Contains(e.Location) && !mh && !st;
                bool s = _previewSelectionItem && !_flat && (_item2.Contains(e.Location) && (e.Location.Y <= _item2.Top + 1 || e.Location.X <= _item2.Left + 1));
                bool h = _previewSelectionItem && !_flat && (_item2.Contains(e.Location) && (e.Location.Y >= _item2.Bottom - 2 || e.Location.X >= _item2.Right - 2));
                bool face = _rect.Contains(e.Location) && !t0 && !t1 && !st && !gt && !mh && !sh && !s && !h;

                if (t0 != _cursorOnText0 || t1 != _cursorOnText1 || st != _cursorOnSelText || gt != _cursorOnGrayText || mh != _cursorOnMenuHilight || sh != _cursorOnSelHilight || s != _cursorOnShadow || h != _cursorOnHilight || face != _cursorOnFace)
                {
                    _cursorOnText0 = t0;
                    _cursorOnText1 = t1;
                    _cursorOnSelText = st;
                    _cursorOnGrayText = gt;
                    _cursorOnMenuHilight = mh;
                    _cursorOnSelHilight = sh;
                    _cursorOnShadow = s;
                    _cursorOnHilight = h;
                    _cursorOnFace = face;
                    Invalidate();
                }
            }
            else if (EnableEditingMetrics)
            {
                bool onCtrl = _rect.Contains(e.Location);
                bool t0 = _item0Text.Contains(e.Location);
                bool gt = _item1Text.Contains(e.Location);
                bool onGrip = _grip.Contains(e.Location);

                if (onCtrl != _cursorOnControl || t0 != _cursorOnText0 || gt != _cursorOnGrayText || onGrip != _cursorOnGrip)
                {
                    _cursorOnControl = onCtrl;
                    _cursorOnText0 = t0;
                    _cursorOnGrayText = gt;
                    _cursorOnGrip = onGrip;
                    Cursor = onGrip ? Cursors.SizeNS : Cursors.Default;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Begins grip drag on mouse down.
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (!DesignMode && EnableEditingMetrics)
            {
                _draggingGrip = _cursorOnGrip;
            }
        }

        /// <summary>
        /// Ends grip drag on mouse up.
        /// </summary>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (!DesignMode && EnableEditingMetrics)
            {
                _draggingGrip = false;
            }
        }

        /// <summary>
        /// Clears hover state and invalidates only when any flag changes.
        /// </summary>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (DesignMode) return;

            if (EnableEditingColors)
            {
                if (_cursorOnFace || _cursorOnShadow || _cursorOnHilight || _cursorOnText0 || _cursorOnText1 || _cursorOnGrayText || _cursorOnMenuHilight || _cursorOnSelHilight || _cursorOnSelText)
                {
                    _cursorOnFace = false;
                    _cursorOnShadow = false;
                    _cursorOnHilight = false;
                    _cursorOnText0 = false;
                    _cursorOnText1 = false;
                    _cursorOnGrayText = false;
                    _cursorOnMenuHilight = false;
                    _cursorOnSelHilight = false;
                    _cursorOnSelText = false;
                    Invalidate();
                }
            }
            else if (EnableEditingMetrics)
            {
                if (_cursorOnControl || _cursorOnGrip)
                {
                    _cursorOnControl = false;
                    _cursorOnGrip = false;
                    Cursor = Cursors.Default;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Invokes the color editor or font dialog on click.
        /// </summary>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (DesignMode) return;

            if (EnableEditingColors)
            {
                if (IsEditShadow) EditorInvoker?.Invoke(this, new(nameof(RetroDesktopColors.ButtonShadow)));
                else if (IsEditHilight) EditorInvoker?.Invoke(this, new(nameof(RetroDesktopColors.ButtonHilight)));
                else if (IsEditFace) EditorInvoker?.Invoke(this, new(_flat ? nameof(RetroDesktopColors.MenuBar) : nameof(RetroDesktopColors.ButtonFace)));
                else if (IsEditText || IsEditText1) EditorInvoker?.Invoke(this, new(nameof(RetroDesktopColors.ButtonText)));
                else if (IsEditGrayText) EditorInvoker?.Invoke(this, new(nameof(RetroDesktopColors.GrayText)));
                else if (IsEditSelText) EditorInvoker?.Invoke(this, new(nameof(RetroDesktopColors.HilightText)));
                else if (IsEditMenuHilight) EditorInvoker?.Invoke(this, new(nameof(RetroDesktopColors.MenuHilight)));
                else if (IsEditSelHilight) EditorInvoker?.Invoke(this, new(nameof(RetroDesktopColors.Hilight)));
            }
            else if (EnableEditingMetrics && (IsMetricsText || IsMetricsGrayText))
            {
                using (FontDialog fd = new() { Font = Font })
                {
                    if (fd.ShowDialog() == DialogResult.OK)
                    {
                        Font = fd.Font;
                        EditorInvoker?.Invoke(this, new(nameof(Font)));
                    }
                }
            }
        }

        #endregion

        #region Paint

        /// <summary>
        /// Paints the menu bar background, item borders, text, and all active edit overlays.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = DesignMode
                ? TextRenderingHint.ClearTypeGridFit
                : Program.Style.TextRenderingHint;

            // 1. Background.
            G.Clear(_flat ? _menuBar : BackColor);

            // 2. Face hover overlay.
            if (IsEditFace)
            {
                using (HatchBrush hb = new(HatchStyle.Percent25, _overlayColor, Color.Transparent))
                {
                    G.FillRectangle(hb, _rect);
                }
            }

            // 3. Selected item.
            if (_previewSelectionItem)
            {
                if (_flat)
                {
                    if (_brushHilight != null)
                    {
                        G.FillRectangle(_brushHilight, _item2);
                    }

                    if (_penMenuHilight != null)
                    {
                        G.DrawRectangle(_penMenuHilight, _item2.X, _item2.Y, _item2.Width, _item2.Height - 1);
                    }

                    // Flat selection fill overlay.
                    if (IsEditSelHilight)
                    {
                        Color ov = Color.FromArgb(100, _hilight.IsDark() ? Color.White : Color.Black);
                        using (HatchBrush hb = new(HatchStyle.Percent25, ov, Color.Transparent))
                        {
                            G.FillRectangle(hb, _item2);
                        }
                    }

                    // Flat menu hilight border overlay.
                    if (IsEditMenuHilight)
                    {
                        using (Pen p = new(BorderOverlay))
                        {
                            G.DrawRectangle(p, _item2.X, _item2.Y, _item2.Width, _item2.Height - 1);
                        }
                    }
                }
                else
                {
                    // Non-flat: 3D selected item border.
                    if (_penShadow != null)
                    {
                        G.DrawLine(_penShadow, _shadowSeg0[0], _shadowSeg0[1]);
                        G.DrawLine(_penShadow, _shadowSeg1[0], _shadowSeg1[1]);
                    }

                    if (_penHilight != null)
                    {
                        G.DrawLine(_penHilight, _hilightSeg0[0], _hilightSeg0[1]);
                        G.DrawLine(_penHilight, _hilightSeg1[0], _hilightSeg1[1]);
                    }

                    // Non-flat border segment overlays.
                    if (IsEditShadow || IsEditHilight)
                    {
                        using (Pen p = new(BorderOverlay, 2f))
                        {
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
                        }
                    }
                }
            }

            // 4. Text overlays — drawn before text so labels render on top.
            DrawTextOverlay(G, IsEditText || IsMetricsText, _item0Text, _overlayColor);
            DrawTextOverlay(G, IsEditGrayText || IsMetricsGrayText, _item1Text, _overlayColor);

            if (IsEditText1 || IsEditSelText)
            {
                Color ov = IsEditText1 ? _overlayColor : Color.FromArgb(100, _hilight.IsDark() ? Color.White : Color.Black);
                DrawTextOverlay(G, true, _item2Text, ov);
            }

            // 5. Item text.
            using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
            {
                if (_brushFore != null) G.DrawString(NormalStr, Font, _brushFore, _item0, sf);
                if (_brushDisabled != null) G.DrawString(DisabledStr, Font, _brushDisabled, _item1, sf);
                if (_previewSelectionItem && _brushSelected != null)
                    G.DrawString(SelectedStr, Font, _brushSelected, _item2, sf);
            }

            // 6. Metrics grip overlay.
            if (IsMetricsHeight)
            {
                using (Pen p = new(BackColor.Invert()) { DashStyle = DashStyle.Dot })
                {
                    G.DrawRectangle(p, _rect);
                }

                using (Pen p = new(BackColor.Invert()))
                {
                    G.FillRoundedRect(Brushes.White, _grip, 1, true);
                    G.DrawRoundedRect(p, _grip, 1, true);
                }
            }
        }

        /// <summary>
        /// Draws a hatch fill and 1px outline over <paramref name="textRect"/> when
        /// <paramref name="condition"/> is true.
        /// </summary>
        private static void DrawTextOverlay(Graphics G, bool condition, Rectangle textRect, Color overlayColor)
        {
            if (!condition) return;

            using (HatchBrush hb = new(HatchStyle.Percent25, overlayColor, Color.Transparent))
            using (Pen p = new(overlayColor))
            {
                G.FillRectangle(hb, textRect);
                G.DrawRectangle(p, textRect);
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
                _penHilight?.Dispose();
                _penMenuHilight?.Dispose();
                _brushFore?.Dispose();
                _brushDisabled?.Dispose();
                _brushSelected?.Dispose();
                _brushHilight?.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}