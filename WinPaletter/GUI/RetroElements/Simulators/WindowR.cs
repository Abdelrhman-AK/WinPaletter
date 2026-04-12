using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using WinPaletter.Templates;

namespace WinPaletter.UI.Retro
{
    /// <summary>
    /// A control that simulates a classic Windows 9x window with a title bar,
    /// control box, 3D border, and interactive color/metric editing.
    /// </summary>
    [Description("Retro window with Windows 9x style")]
    public class WindowR : Panel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowR"/> class.
        /// </summary>
        public WindowR()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            Font = new("Microsoft Sans Serif", 8f);
            BackColor = SystemColors.ButtonFace;
            ForeColor = SystemColors.ControlText;
            BorderStyle = BorderStyle.None;
            Text = "New window";

            _titleHeight = PreviewHelpers.GetTitlebarTextHeight(Font);

            _closeBtn = new() { Name = "CloseBtn", Text = "r", TextAlign = ContentAlignment.MiddleCenter };
            _minBtn = new() { Name = "MinBtn", Text = "1", TextAlign = ContentAlignment.MiddleCenter };
            _maxBtn = new() { Name = "MaxBtn", Text = "0", TextAlign = ContentAlignment.MiddleCenter };

            Controls.AddRange([_closeBtn, _maxBtn, _minBtn]);

            _closeBtn?.Visible = _controlBox;
            _minBtn?.Visible = _controlBox && _minimizeBox;
            _maxBtn?.Visible = _controlBox && _maximizeBox;

            AdjustControlBoxFontsSizes();
            AdjustButtonSizes();
            AdjustLocations();
            AdjustPadding();
        }

        #region Private fields

        // Title bar metrics.
        private float _titleHeight;

        // Drag state for window movement.
        private Point _dragStart = Point.Empty;
        private Point _dragCurrent = Point.Empty;

        // Grip drag flags — one per draggable border zone.
        private bool _dragging_captionHeight;
        private bool _dragging_paddingLeft;
        private bool _dragging_borderLeft;
        private bool _dragging_paddingRight;
        private bool _dragging_borderRight;
        private bool _dragging_paddingBottom;
        private bool _dragging_borderBottom;

        // Hover state — color editing.
        private bool _cursorOnColor1;
        private bool _cursorOnColor2;
        private bool _cursorOnTitleText;
        private bool _cursorOnShadow;
        private bool _cursorOnDkShadow;
        private bool _cursorOnHilight;
        private bool _cursorOnLight;
        private bool _cursorOnBorder;
        private bool _cursorOnFace;

        // Hover state — metric editing.
        private bool _cursorOnMetricsGrip;

        // Cached geometry — rebuilt in AdjustPadding.
        private RectangleF _rectOuter;         // full control (0,0,W-1,H-1)
        private RectangleF _rectBorder;        // ColorBorder rect (2,2,W-5,H-5)
        private RectangleF _titlebarRect;      // title bar area
        private RectangleF _titlebarTextRect;  // tight title text bounds
        private RectangleF _r0;                // left half of title bar (Color1)
        private RectangleF _r1;                // right half of title bar (Color2)
        private RectangleF _editingRect;       // metrics editing outline rect
        private RectangleF _gripTopLeft, _gripTopRight;
        private RectangleF _gripBottomLeft, _gripBottomRight;
        private RectangleF _gripTopCenter, _gripBottomCenter;
        private RectangleF _gripLeftCenter, _gripRightCenter;

        // Border line segments for hit-test and overlay drawing.
        private PointF[] _shadowSeg0, _shadowSeg1;
        private PointF[] _dkShadowSeg0, _dkShadowSeg1;
        private PointF[] _hilightSeg0, _hilightSeg1;
        private PointF[] _lightSeg0, _lightSeg1;

        // Cached pens — rebuilt only when the corresponding color changes.
        private Pen _penShadow;
        private Pen _penDkShadow;
        private Pen _penHilight;
        private Pen _penLight;
        private Pen _penBorder;
        private Pen _penFace;

        private SolidBrush _brushColor1;
        private SolidBrush _brushColor2;
        private LinearGradientBrush _brushColorGr;

        // Cached overlay color — recomputed when BackColor changes.
        private Color _overlayColor;

        // Grip square size in pixels.
        private const int GripSize = 6;

        // Cached border overlay color (same for all border segments).
        private static readonly Color BorderOverlay = Color.FromArgb(200, 128, 0, 0);

        #endregion

        #region Child controls

        private readonly ButtonR _closeBtn;
        private readonly ButtonR _minBtn;
        private readonly ButtonR _maxBtn;

        private float _btnHeight;
        private float _btnWidth;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the window caption text.
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        [Bindable(true)]
        public override string Text
        {
            get => base.Text;
            set
            {
                if (base.Text == value) return;
                base.Text = value;
                AdjustPadding();
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the window background color (also applied to control box buttons).
        /// </summary>
        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                if (base.BackColor == value) return;
                base.BackColor = value;
                _closeBtn?.BackColor = value;
                _minBtn?.BackColor = value;
                _maxBtn?.BackColor = value;
                ReplacePen(ref _penFace, value);
                _overlayColor = Color.FromArgb(100, value.IsDark() ? Color.White : Color.Black);
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the title bar displays a color gradient.
        /// </summary>
        public bool ColorGradient
        {
            get => _colorGradient;
            set { if (_colorGradient != value) { _colorGradient = value; Invalidate(); } }
        }
        private bool _colorGradient = true;

        /// <summary>
        /// Gets or sets a value indicating whether the window is active.
        /// </summary>
        public bool Active
        {
            get => _active;
            set { if (_active != value) { _active = value; Invalidate(); } }
        }
        private bool _active = true;

        /// <summary>
        /// Gets or sets the border color strip between the 3D edge and the client area.
        /// </summary>
        public Color ColorBorder
        {
            get => _colorBorder;
            set
            {
                if (_colorBorder == value) return;
                _colorBorder = value;
                ReplacePen(ref _penBorder, value);
                Invalidate();
            }
        }
        private Color _colorBorder = SystemColors.ActiveBorder;

        /// <summary>
        /// Gets or sets a value indicating whether the window border is flat (no 3D edge).
        /// </summary>
        public bool Flat
        {
            get => _flat;
            set { if (_flat != value) { _flat = value; Invalidate(); } }
        }
        private bool _flat = false;

        /// <summary>
        /// Gets or sets the primary title bar color.
        /// </summary>
        public Color Color1
        {
            get => _color1;
            set
            {
                if (_color1 != value)
                {
                    _color1 = value;
                    ReplaceBrush(ref _brushColor1, value);

                    bool rtl = RightToLeft == RightToLeft.Yes;
                    _brushColorGr?.Dispose();
                    _brushColorGr = new(_titlebarRect, rtl ? _color2 : _color1, rtl ? _color1 : _color2, LinearGradientMode.Horizontal);

                    Invalidate();
                }
            }
        }
        private Color _color1 = SystemColors.ActiveCaption;

        /// <summary>
        /// Gets or sets the gradient end color of the title bar.
        /// </summary>
        public Color Color2
        {
            get => _color2;
            set
            {
                if (_color2 != value)
                {
                    _color2 = value;
                    ReplaceBrush(ref _brushColor2, value);

                    bool rtl = RightToLeft == RightToLeft.Yes;
                    _brushColorGr?.Dispose();
                    _brushColorGr = new(_titlebarRect, rtl ? _color2 : _color1, rtl ? _color1 : _color2, LinearGradientMode.Horizontal);

                    Invalidate();
                }
            }
        }
        private Color _color2 = SystemColors.GradientActiveCaption;

        /// <summary>
        /// Gets or sets the button shadow color (also applied to control box buttons).
        /// </summary>
        public Color ButtonShadow
        {
            get => _buttonShadow;
            set
            {
                if (_buttonShadow == value) return;
                _buttonShadow = value;
                _closeBtn?.ButtonShadow = value;
                _minBtn?.ButtonShadow = value;
                _maxBtn?.ButtonShadow = value;
                ReplacePen(ref _penShadow, value);
                Invalidate();
            }
        }
        private Color _buttonShadow = SystemColors.ButtonShadow;

        /// <summary>
        /// Gets or sets the button dark shadow color (also applied to control box buttons).
        /// </summary>
        public Color ButtonDkShadow
        {
            get => _buttonDkShadow;
            set
            {
                if (_buttonDkShadow == value) return;
                _buttonDkShadow = value;
                _closeBtn?.ButtonDkShadow = value;
                _minBtn?.ButtonDkShadow = value;
                _maxBtn?.ButtonDkShadow = value;
                ReplacePen(ref _penDkShadow, value);
                Invalidate();
            }
        }
        private Color _buttonDkShadow = SystemColors.ControlDark;

        /// <summary>
        /// Gets or sets the button hilight color (also applied to control box buttons).
        /// </summary>
        public Color ButtonHilight
        {
            get => _buttonHilight;
            set
            {
                if (_buttonHilight == value) return;
                _buttonHilight = value;
                _closeBtn?.ButtonHilight = value;
                _minBtn?.ButtonHilight = value;
                _maxBtn?.ButtonHilight = value;
                ReplacePen(ref _penHilight, value);
                Invalidate();
            }
        }
        private Color _buttonHilight = SystemColors.ButtonHighlight;

        /// <summary>
        /// Gets or sets the button light color (also applied to control box buttons).
        /// </summary>
        public Color ButtonLight
        {
            get => _buttonLight;
            set
            {
                if (_buttonLight == value) return;
                _buttonLight = value;
                _closeBtn?.ButtonLight = value;
                _minBtn?.ButtonLight = value;
                _maxBtn?.ButtonLight = value;
                ReplacePen(ref _penLight, value);
                Invalidate();
            }
        }
        private Color _buttonLight = SystemColors.ControlLight;

        /// <summary>
        /// Gets or sets the control box button text (glyph) color.
        /// </summary>
        public Color ButtonText
        {
            get => _buttonText;
            set
            {
                if (_buttonText == value) return;
                _buttonText = value;
                _closeBtn?.ForeColor = value;
                _minBtn?.ForeColor = value;
                _maxBtn?.ForeColor = value;
                Invalidate();
            }
        }
        private Color _buttonText = SystemColors.ControlText;

        /// <summary>
        /// Gets or sets the caption bar height in pixels. Clamped to [16, 50].
        /// </summary>
        public int Metrics_CaptionHeight
        {
            get => _captionHeight;
            set
            {
                value = Math.Max(16, Math.Min(50, value));
                if (value == _captionHeight) return;
                _captionHeight = value;
                AdjustButtonSizes();
                AdjustControlBoxFontsSizes();
                AdjustLocations();
                AdjustPadding();
                Invalidate();
                if (!DesignMode && EnableEditingMetrics && _dragging_captionHeight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Metrics_CaptionHeight)));
            }
        }
        private int _captionHeight = SystemInformation.CaptionHeight;

        /// <summary>
        /// Gets or sets the caption button width in pixels. Clamped to [16, 50].
        /// </summary>
        public int Metrics_CaptionWidth
        {
            get => _captionWidth;
            set
            {
                value = Math.Max(16, Math.Min(50, value));
                if (value == _captionWidth) return;
                _captionWidth = value;
                AdjustButtonSizes();
                AdjustLocations();
                AdjustControlBoxFontsSizes();
                Invalidate();
            }
        }
        private int _captionWidth = 22;

        /// <summary>
        /// Gets or sets the window border width in pixels. Clamped to [0, 50].
        /// </summary>
        public int Metrics_BorderWidth
        {
            get => _borderWidth;
            set
            {
                value = Math.Max(0, Math.Min(50, value));
                if (value == _borderWidth) return;
                _borderWidth = value;
                AdjustLocations();
                AdjustPadding();
                Invalidate();
                if (!DesignMode && EnableEditingMetrics && (_dragging_borderLeft || _dragging_borderRight || _dragging_borderBottom))
                    EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Metrics_BorderWidth)));
            }
        }
        private int _borderWidth = 1;

        /// <summary>
        /// Gets or sets the padded border width in pixels. Clamped to [0, 50].
        /// </summary>
        public int Metrics_PaddedBorderWidth
        {
            get => _paddedBorderWidth;
            set
            {
                value = Math.Max(0, Math.Min(50, value));
                if (value == _paddedBorderWidth) return;
                _paddedBorderWidth = value;
                AdjustLocations();
                AdjustPadding();
                Invalidate();
                if (!DesignMode && EnableEditingMetrics && (_dragging_paddingLeft || _dragging_paddingRight || _dragging_paddingBottom))
                    EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Metrics_PaddedBorderWidth)));
            }
        }
        private int _paddedBorderWidth = 4;

        /// <summary>
        /// Gets or sets a value indicating whether the control box is visible.
        /// </summary>
        public bool ControlBox
        {
            get => _controlBox;
            set
            {
                if (_controlBox == value) return;
                _controlBox = value;
                _closeBtn?.Visible = value;
                _minBtn?.Visible = value && _minimizeBox;
                _maxBtn?.Visible = value && _maximizeBox;
                AdjustLocations();
                Invalidate();
            }
        }
        private bool _controlBox = true;

        /// <summary>
        /// Gets or sets a value indicating whether the minimize button is visible.
        /// </summary>
        public bool MinimizeBox
        {
            get => _minimizeBox;
            set
            {
                if (_minimizeBox == value) return;
                _minimizeBox = value;
                _minBtn?.Visible = _controlBox && value;
                AdjustLocations();
                Invalidate();
            }
        }
        private bool _minimizeBox = true;

        /// <summary>
        /// Gets or sets a value indicating whether the maximize button is visible.
        /// </summary>
        public bool MaximizeBox
        {
            get => _maximizeBox;
            set
            {
                if (_maximizeBox == value) return;
                _maximizeBox = value;
                _maxBtn?.Visible = _controlBox && value;
                AdjustLocations();
                Invalidate();
            }
        }
        private bool _maximizeBox = true;

        /// <summary>
        /// Gets or sets a value indicating whether colors can be edited interactively by clicking.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingColors { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether metrics can be edited by dragging grips.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingMetrics { get; set; } = false;

        // Computed edit-mode flags.
        private bool IsEditColor1 => EnableEditingColors && _cursorOnColor1;
        private bool IsEditColor2 => EnableEditingColors && _cursorOnColor2;
        private bool IsEditTitleText => EnableEditingColors && _cursorOnTitleText;
        private bool IsEditShadow => EnableEditingColors && _cursorOnShadow;
        private bool IsEditDkShadow => EnableEditingColors && _cursorOnDkShadow;
        private bool IsEditHilight => EnableEditingColors && _cursorOnHilight;
        private bool IsEditLight => EnableEditingColors && _cursorOnLight;
        private bool IsEditBorder => EnableEditingColors && _cursorOnBorder;
        private bool IsEditFace => EnableEditingColors && _cursorOnFace;
        private bool IsMetricsGrip => EnableEditingMetrics && _cursorOnMetricsGrip;
        private bool IsMetricsCaptionFont => EnableEditingMetrics && _cursorOnTitleText;

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

        /// <summary>
        /// Disposes the existing pen and allocates a replacement with the given color.
        /// </summary>
        private static void ReplacePen(ref Pen pen, Color color)
        {
            pen?.Dispose();
            pen = new(color);
        }

        /// <summary>
        /// Disposes the existing solid brush and allocates a replacement with the given color.
        /// </summary>
        private static void ReplaceBrush(ref SolidBrush brush, Color color)
        {
            brush?.Dispose();
            brush = new(color);
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

        #endregion

        #region Layout

        /// <summary>
        /// Resizes the control box buttons to match the current caption metrics.
        /// </summary>
        private void AdjustButtonSizes()
        {
            _btnHeight = Math.Max(_captionHeight + PreviewHelpers.GetTitlebarTextHeight(Font) - 4, 5f);
            _btnWidth = Math.Max(_captionWidth - 2, 5f);

            Size sz = new((int)_btnWidth, (int)_btnHeight);
            _closeBtn?.Size = sz;
            _minBtn?.Size = sz;
            _maxBtn?.Size = sz;
        }

        /// <summary>
        /// Repositions the control box buttons relative to the current border and padding metrics.
        /// </summary>
        private void AdjustLocations()
        {
            int top = _paddedBorderWidth + _borderWidth + 5;
            int left = Width - (int)_btnWidth - _paddedBorderWidth - _borderWidth - 5;

            _closeBtn?.Top = top;
            _minBtn?.Top = top;
            _maxBtn?.Top = top;
            _closeBtn?.Left = left;

            if (_minimizeBox && _maximizeBox)
            {
                _minBtn?.Left = _closeBtn.Left - 2 - (int)_btnWidth;
                _maxBtn?.Left = _minBtn.Left - (int)_btnWidth;
            }
            else if (_maximizeBox)
            {
                _maxBtn?.Left = _closeBtn.Left - 2 - (int)_btnWidth;
            }
            else if (_minimizeBox)
            {
                _minBtn?.Left = _closeBtn.Left - 2 - (int)_btnWidth;
            }
        }

        /// <summary>
        /// Scales the Marlett font on the control box buttons to the current caption size.
        /// </summary>
        public void AdjustControlBoxFontsSizes()
        {
            float i0 = Math.Abs(Math.Min(_captionHeight, _captionWidth));
            float iFx = i0 / Math.Abs(Math.Min(17f, 18f));
            Font f = new("Marlett", 6.8f * iFx);
            _closeBtn?.Font = f;
            _minBtn?.Font = f;
            _maxBtn?.Font = f;
        }

        /// <summary>
        /// Recomputes all cached geometry — title bar rect, text rect, grip positions,
        /// border segment endpoints — from the current metrics and control size.
        /// </summary>
        public void AdjustPadding()
        {
            float combined = _borderWidth + _paddedBorderWidth + 3f;
            float topPad = 4f + _paddedBorderWidth + _borderWidth + _captionHeight + PreviewHelpers.GetTitlebarTextHeight(Font);

            Padding = new((int)combined, (int)topPad, (int)combined, (int)combined);

            _editingRect = new(Padding.Left, Padding.Top, Width - Padding.Left * 2 - 1, Height - Padding.Bottom - Padding.Top - 1);

            float gs = GripSize * 0.5f;
            _gripTopLeft = new(_editingRect.X - gs, _editingRect.Y - gs, GripSize, GripSize);
            _gripTopRight = new(_editingRect.Right - gs, _editingRect.Y - gs, GripSize, GripSize);
            _gripBottomLeft = new(_editingRect.X - gs, _editingRect.Bottom - gs, GripSize, GripSize);
            _gripBottomRight = new(_editingRect.Right - gs, _editingRect.Bottom - gs, GripSize, GripSize);
            _gripTopCenter = new(_editingRect.X + _editingRect.Width / 2f - gs, _editingRect.Y - gs, GripSize, GripSize);
            _gripBottomCenter = new(_editingRect.X + _editingRect.Width / 2f - gs, _editingRect.Bottom - gs, GripSize, GripSize);
            _gripLeftCenter = new(_editingRect.X - gs, _editingRect.Y + _editingRect.Height / 2f - gs, GripSize, GripSize);
            _gripRightCenter = new(_editingRect.Right - gs, _editingRect.Y + _editingRect.Height / 2f - gs, GripSize, GripSize);

            _titlebarRect = new(combined, combined, Width - combined * 2f, _captionHeight + _titleHeight);

            SizeF textSz = Text.Measure(Font);
            float ty = _titlebarRect.Y + (_titlebarRect.Height - textSz.Height) / 2f;
            _titlebarTextRect = new(_titlebarRect.X, ty, textSz.Width, textSz.Height);

            _r0 = new(_titlebarRect.X, _titlebarRect.Y, _titlebarRect.Width / 2f, _titlebarRect.Height - 1f);
            _r1 = new(_titlebarRect.X + _titlebarRect.Width / 2f, _titlebarRect.Y, _titlebarRect.Width / 2f, _titlebarRect.Height - 1f);

            float w = Width - 1f;
            float h = Height - 1f;
            _rectOuter = new(0, 0, w, h);
            _rectBorder = new(2, 2, Width - 5f, Height - 5f);

            // Raised 3D border segments (Win9x button appearance):
            //   Light    outer top + left
            //   Hilight  inner top + left
            //   Shadow   inner bottom + right
            //   DkShadow outer bottom + right
            _lightSeg0 = [new(0, 0), new(w - 1f, 0)];
            _lightSeg1 = [new(0, 0), new(0, h - 1f)];
            _hilightSeg0 = [new(1, 1), new(w - 2f, 1)];
            _hilightSeg1 = [new(1, 1), new(1, h - 2f)];
            _shadowSeg0 = [new(w - 1f, 1), new(w - 1f, h - 1f)];
            _shadowSeg1 = [new(1, h - 1f), new(w - 1f, h - 1f)];
            _dkShadowSeg0 = [new(w, 0), new(w, h)];
            _dkShadowSeg1 = [new(0, h), new(w, h)];
        }

        #endregion

        #region Overrides — layout triggers

        /// <summary>
        /// Rebuilds title height and all layout when the font changes.
        /// </summary>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            _titleHeight = PreviewHelpers.GetTitlebarTextHeight(Font);
            AdjustControlBoxFontsSizes();
            AdjustButtonSizes();
            AdjustLocations();
            AdjustPadding();
        }

        /// <summary>
        /// Rebuilds layout when the control is resized.
        /// </summary>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            AdjustLocations();
            AdjustPadding();
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
        /// Begins window drag or metrics grip drag on mouse down.
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (DesignMode) return;

            if (_titlebarRect.Contains(e.Location) && e.Button == MouseButtons.Left)
            {
                _dragStart = MousePosition - new Size(Location);
                _dragCurrent = _dragStart;
            }

            if (!EnableEditingMetrics) return;

            // Map grip zones to drag flags.
            bool left = e.Button != MouseButtons.Right;
            bool padding = e.Button == MouseButtons.Right;

            ResetDragFlags();

            if (_gripTopCenter.Contains(e.Location)) { _dragging_captionHeight = true; }
            else if (_gripLeftCenter.Contains(e.Location)) { _dragging_paddingLeft = padding; _dragging_borderLeft = left; }
            else if (_gripRightCenter.Contains(e.Location)) { _dragging_paddingRight = padding; _dragging_borderRight = left; }
            else if (_gripBottomCenter.Contains(e.Location)) { _dragging_paddingBottom = padding; _dragging_borderBottom = left; }
            else if (_gripBottomLeft.Contains(e.Location)) { _dragging_paddingLeft = padding; _dragging_borderLeft = left; _dragging_paddingBottom = padding; _dragging_borderBottom = left; }
            else if (_gripBottomRight.Contains(e.Location)) { _dragging_paddingRight = padding; _dragging_borderRight = left; _dragging_paddingBottom = padding; _dragging_borderBottom = left; }
            else if (_gripTopLeft.Contains(e.Location)) { _dragging_captionHeight = true; _dragging_paddingLeft = padding; _dragging_borderLeft = left; }
            else if (_gripTopRight.Contains(e.Location)) { _dragging_captionHeight = true; _dragging_paddingRight = padding; _dragging_borderRight = left; }
        }

        /// <summary>
        /// Handles window dragging, color hover, and metric grip dragging.
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (DesignMode) return;

            // Window drag.
            if (e.Button == MouseButtons.Left && _titlebarRect.Contains(e.Location) && !_dragging_captionHeight)
            {
                _dragCurrent = MousePosition - new Size(_dragStart);
                Location = _dragCurrent;
                return;
            }

            if (EnableEditingColors)
            {
                UpdateColorHover(e.Location);
                Invalidate();
                return;
            }

            if (EnableEditingMetrics)
            {
                UpdateMetricsDrag(e.Location);
            }
        }

        /// <summary>
        /// Clears all drag flags on mouse up.
        /// </summary>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            ResetDragFlags();
        }

        /// <summary>
        /// Clears all hover and drag state on mouse leave.
        /// </summary>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (DesignMode) return;

            if (EnableEditingColors)
            {
                ResetColorHover();
                Invalidate();
            }

            if (EnableEditingMetrics)
            {
                _cursorOnMetricsGrip = false;
                _cursorOnTitleText = false;
                ResetDragFlags();
                Invalidate();
            }

            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Invokes the color editor or font dialog on click.
        /// </summary>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (DesignMode) return;

            if (_dragStart == _dragCurrent && EnableEditingColors)
            {
                if (IsEditColor1) EditorInvoker?.Invoke(this, new EditorEventArgs(_active ? nameof(RetroDesktopColors.ActiveTitle) : nameof(RetroDesktopColors.InactiveTitle)));
                else if (IsEditColor2) EditorInvoker?.Invoke(this, new EditorEventArgs(_active ? nameof(RetroDesktopColors.GradientActiveTitle) : nameof(RetroDesktopColors.GradientInactiveTitle)));
                else if (IsEditTitleText) EditorInvoker?.Invoke(this, new EditorEventArgs(_active ? nameof(RetroDesktopColors.TitleText) : nameof(RetroDesktopColors.InactiveTitleText)));
                else if (IsEditShadow) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonShadow)));
                else if (IsEditDkShadow) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonDkShadow)));
                else if (IsEditHilight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonHilight)));
                else if (IsEditLight) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonLight)));
                else if (IsEditBorder) EditorInvoker?.Invoke(this, new EditorEventArgs(_active ? nameof(RetroDesktopColors.ActiveBorder) : nameof(RetroDesktopColors.InactiveBorder)));
                else if (IsEditFace) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonFace)));
            }
            else if (IsMetricsCaptionFont)
            {
                using (FontDialog fd = new() { Font = Font })
                {
                    if (fd.ShowDialog() == DialogResult.OK)
                    {
                        Font = fd.Font;
                        EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Font)));
                    }
                }
            }
        }

        #endregion

        #region Mouse helpers

        /// <summary>
        /// Updates all color-edit hover flags from the current cursor position.
        /// Uses <see cref="OnSegment"/> for 1px border lines and rect containment for areas.
        /// </summary>
        private void UpdateColorHover(Point loc)
        {
            _cursorOnTitleText = _titlebarTextRect.Contains(loc);

            if (_cursorOnTitleText)
            {
                _cursorOnColor1 = false;
                _cursorOnColor2 = false;
                _cursorOnShadow = false;
                _cursorOnDkShadow = false;
                _cursorOnHilight = false;
                _cursorOnLight = false;
                _cursorOnBorder = false;
                _cursorOnFace = false;
                return;
            }

            _cursorOnColor1 = _r0.Contains(loc);
            _cursorOnColor2 = _r1.Contains(loc);

            if (_flat)
            {
                _cursorOnShadow = _rectOuter.BordersContains(loc);
                _cursorOnDkShadow = false;
                _cursorOnHilight = false;
                _cursorOnLight = false;
            }
            else
            {
                bool onDkShadow = OnSegment(_dkShadowSeg0, loc) || OnSegment(_dkShadowSeg1, loc);
                bool onShadow = !onDkShadow && (OnSegment(_shadowSeg0, loc) || OnSegment(_shadowSeg1, loc));
                bool onHilight = !onDkShadow && !onShadow && (OnSegment(_hilightSeg0, loc) || OnSegment(_hilightSeg1, loc));
                bool onLight = !onDkShadow && !onShadow && !onHilight && (OnSegment(_lightSeg0, loc) || OnSegment(_lightSeg1, loc));

                _cursorOnDkShadow = onDkShadow;
                _cursorOnShadow = onShadow;
                _cursorOnHilight = onHilight;
                _cursorOnLight = onLight;
            }

            _cursorOnBorder = _rectBorder.BordersContains(loc);
            _cursorOnFace = _rectBorder.Contains(loc) && !_cursorOnBorder && !_titlebarRect.Contains(loc);
        }

        /// <summary>
        /// Resets all color-edit hover flags to false.
        /// </summary>
        private void ResetColorHover()
        {
            _cursorOnColor1 = false;
            _cursorOnColor2 = false;
            _cursorOnTitleText = false;
            _cursorOnShadow = false;
            _cursorOnDkShadow = false;
            _cursorOnHilight = false;
            _cursorOnLight = false;
            _cursorOnBorder = false;
            _cursorOnFace = false;
        }

        /// <summary>
        /// Handles metric grip dragging and cursor shape updates.
        /// </summary>
        private void UpdateMetricsDrag(Point loc)
        {
            if (_dragging_captionHeight)
            {
                Metrics_CaptionHeight = loc.Y - (_paddedBorderWidth + _borderWidth + (int)(GripSize * 0.5f));
                return;
            }

            if (_dragging_paddingLeft) { Metrics_PaddedBorderWidth = loc.X - (int)(GripSize * 0.5f) - _borderWidth; return; }
            else if (_dragging_borderLeft) { Metrics_BorderWidth = loc.X - (int)(GripSize * 0.5f) - _paddedBorderWidth; return; }
            else if (_dragging_paddingRight) { Metrics_PaddedBorderWidth = Width - loc.X - (int)(GripSize * 0.5f) - _borderWidth; return; }
            else if (_dragging_borderRight) { Metrics_BorderWidth = Width - loc.X - (int)(GripSize * 0.5f) - _paddedBorderWidth; return; }
            else if (_dragging_paddingBottom) { Metrics_PaddedBorderWidth = Height - loc.Y - (int)(GripSize * 0.5f) - _borderWidth; return; }
            else if (_dragging_borderBottom) { Metrics_BorderWidth = Height - loc.Y - (int)(GripSize * 0.5f) - _paddedBorderWidth; return; }

            // No drag active — update hover state and cursor shape.
            _cursorOnTitleText = _titlebarTextRect.Contains(loc);
            _cursorOnMetricsGrip = !_titlebarRect.Contains(loc);

            if (_gripTopLeft.Contains(loc) || _gripBottomRight.Contains(loc)) Cursor = Cursors.SizeNWSE;
            else if (_gripTopRight.Contains(loc) || _gripBottomLeft.Contains(loc)) Cursor = Cursors.SizeNESW;
            else if (_gripTopCenter.Contains(loc) || _gripBottomCenter.Contains(loc)) Cursor = Cursors.SizeNS;
            else if (_gripLeftCenter.Contains(loc) || _gripRightCenter.Contains(loc)) Cursor = Cursors.SizeWE;
            else Cursor = Cursors.Default;

            Invalidate();
        }

        /// <summary>
        /// Resets all metric grip drag flags to false.
        /// </summary>
        private void ResetDragFlags()
        {
            _dragging_captionHeight = false;
            _dragging_paddingLeft = false;
            _dragging_borderLeft = false;
            _dragging_paddingRight = false;
            _dragging_borderRight = false;
            _dragging_paddingBottom = false;
            _dragging_borderBottom = false;
        }

        #endregion

        #region Paint

        /// <summary>
        /// Paints the complete window simulation: 3D border, color border, title bar,
        /// caption text, and all active color-edit or metric-edit overlays.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighSpeed;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            G.Clear(BackColor);

            // 1. Face hover overlay.
            if (IsEditFace)
            {
                using (HatchBrush hb = new(HatchStyle.Percent25, _overlayColor, Color.Transparent))
                {
                    G.FillRectangle(hb, _rectOuter.X, _rectOuter.Y, _rectOuter.Width, _rectOuter.Height);
                }
            }

            // 2. 3D border.
            if (!_flat)
            {
                G.DrawLine(_penLight, _lightSeg0[0], _lightSeg0[1]);
                G.DrawLine(_penLight, _lightSeg1[0], _lightSeg1[1]);
                G.DrawLine(_penHilight, _hilightSeg0[0], _hilightSeg0[1]);
                G.DrawLine(_penHilight, _hilightSeg1[0], _hilightSeg1[1]);
                G.DrawLine(_penShadow, _shadowSeg0[0], _shadowSeg0[1]);
                G.DrawLine(_penShadow, _shadowSeg1[0], _shadowSeg1[1]);
                G.DrawLine(_penDkShadow, _dkShadowSeg0[0], _dkShadowSeg0[1]);
                G.DrawLine(_penDkShadow, _dkShadowSeg1[0], _dkShadowSeg1[1]);

                // 3D border segment overlays.
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

                // Face-color strip between 3D edge and color border.
                if (_penFace != null)
                {
                    G.DrawRectangle(_penFace, _rectBorder.X, _rectBorder.Y, _rectBorder.Width, _rectBorder.Height);
                }
            }
            else
            {
                G.DrawRectangle(_penShadow, _rectOuter.X, _rectOuter.Y, _rectOuter.Width, _rectOuter.Height);

                if (IsEditShadow)
                {
                    using (Pen p = new(BorderOverlay))
                    {
                        G.DrawRectangle(p, _rectOuter.X, _rectOuter.Y, _rectOuter.Width, _rectOuter.Height);
                    }
                }
            }

            // 3. ColorBorder strip.
            if (_penBorder != null)
            {
                G.DrawRectangle(_penBorder, _rectBorder.X, _rectBorder.Y, _rectBorder.Width, _rectBorder.Height);
            }

            if (IsEditBorder)
            {
                using (Pen p = new(BorderOverlay))
                {
                    G.DrawRectangle(p, _rectBorder.X, _rectBorder.Y, _rectBorder.Width, _rectBorder.Height);
                }
            }

            // 4. Title bar fill.
            bool rtl = RightToLeft == RightToLeft.Yes;

            if (_colorGradient)
            {
                G.FillRectangle(_brushColorGr, _titlebarRect);
                G.FillRectangle(rtl ? _brushColor2 : _brushColor1, new RectangleF(_titlebarRect.X, _titlebarRect.Y, 1f, _titlebarRect.Height));

                if (IsEditColor1)
                {
                    Color ov = Color.FromArgb(128, _color1.IsDark() ? Color.White : Color.Black);
                    using (HatchBrush hb = new(HatchStyle.Percent25, ov, Color.Transparent))
                    using (Pen p = new(ov))
                    {
                        G.FillRectangle(hb, _r0);
                        G.DrawRectangle(p, _r0.X, _r0.Y, _r0.Width, _r0.Height);
                    }
                }
                else if (IsEditColor2)
                {
                    Color ov = Color.FromArgb(128, _color1.IsDark() ? Color.White : Color.Black);
                    using (HatchBrush hb = new(HatchStyle.Percent25, ov, Color.Transparent))
                    using (Pen p = new(ov))
                    {
                        G.FillRectangle(hb, _r1);
                        G.DrawRectangle(p, _r1.X, _r1.Y, _r1.Width, _r1.Height);
                    }
                }
            }
            else
            {
                G.FillRectangle(_brushColor1, _titlebarRect);

                if (IsEditColor1 || IsEditColor2)
                {
                    Color ov = Color.FromArgb(128, _color1.IsDark() ? Color.White : Color.Black);
                    using (HatchBrush hb = new(HatchStyle.Percent25, ov, Color.Transparent))
                    using (Pen p = new(ov))
                    {
                        G.FillRectangle(hb, _titlebarRect);
                        G.DrawRectangle(p, _titlebarRect.X, _titlebarRect.Y, _titlebarRect.Width, _titlebarRect.Height);
                    }
                }
            }

            // 5. Title bar text and its hover overlay.
            using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat(rtl))
            using (SolidBrush br = new(ForeColor))
            {
                if (IsEditTitleText || IsMetricsCaptionFont)
                {
                    Color ov = Color.FromArgb(128, _color1.IsDark() ? Color.White : Color.Black);
                    using (HatchBrush hb = new(HatchStyle.Percent25, ov, Color.Transparent))
                    using (Pen p = new(ov))
                    {
                        G.FillRectangle(hb, _titlebarTextRect);
                        G.DrawRectangle(p, _titlebarTextRect.X, _titlebarTextRect.Y, _titlebarTextRect.Width, _titlebarTextRect.Height);
                    }
                }

                G.DrawString(Text, Font, br, _titlebarRect, sf);
            }

            // 6. Metrics grip overlay.
            if (!DesignMode && IsMetricsGrip)
            {
                using (Pen p = new(BackColor.Invert()) { DashStyle = DashStyle.Dot })
                {
                    G.DrawRectangle(p, _editingRect.X, _editingRect.Y, _editingRect.Width, _editingRect.Height);
                }

                using (Pen p = new(BackColor.Invert()))
                {
                    void DrawGrip(RectangleF r)
                    {
                        G.FillRoundedRect(Brushes.White, r, 1, true);
                        G.DrawRoundedRect(p, r, 1, true);
                    }

                    DrawGrip(_gripTopLeft);
                    DrawGrip(_gripTopRight);
                    DrawGrip(_gripBottomLeft);
                    DrawGrip(_gripBottomRight);
                    DrawGrip(_gripTopCenter);
                    DrawGrip(_gripBottomCenter);
                    DrawGrip(_gripLeftCenter);
                    DrawGrip(_gripRightCenter);
                }
            }
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Releases all cached GDI resources and child controls.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _penShadow?.Dispose();
                _penDkShadow?.Dispose();
                _penHilight?.Dispose();
                _penLight?.Dispose();
                _penBorder?.Dispose();
                _penFace?.Dispose();
                _brushColor1?.Dispose();
                _brushColor2?.Dispose();
                _brushColorGr?.Dispose();
                _closeBtn?.Dispose();
                _minBtn?.Dispose();
                _maxBtn?.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}