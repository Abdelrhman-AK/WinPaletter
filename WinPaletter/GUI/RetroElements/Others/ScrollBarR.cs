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
    /// Retro ScrollBar with Windows 9x style.
    /// </summary>
    [Description("Retro ScrollBar with Windows 9x style")]
    public class ScrollBarR : Panel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScrollBarR"/> class.
        /// </summary>
        public ScrollBarR()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            BackColor = Color.FromArgb(192, 192, 192);
            BorderStyle = BorderStyle.None;

            Controls.Add(_btnUp);
            Controls.Add(_btnDown);
            Controls.Add(_btnScroll);

            _btnUp.Click += (s, e) => Value--;
            _btnDown.Click += (s, e) => Value++;

            _btnScroll.MouseDown += BtnScroll_MouseDown;
            _btnScroll.MouseMove += BtnScroll_MouseMove;
            _btnScroll.MouseUp += BtnScroll_MouseUp;

            AdjustLayout();
        }

        #region Private fields

        // Scroll handle drag state.
        private bool _draggingScroll;
        private int _dragOffset;

        // Metrics grip drag state.
        private bool _draggingGrip;
        private Point _gripAnchor = Point.Empty;

        // Cached full-control rect for paint and hit-test.
        private Rectangle _rectControl;

        // Hover state.
        private bool _cursorOnFace;
        private bool _cursorOnHilight;
        private bool _cursorOnGrip;

        #endregion

        #region Child controls

        private readonly ButtonR _btnUp = new() { Text = "t", Font = new Font("Marlett", 8.8f) };
        private readonly ButtonR _btnDown = new() { Text = "u", Font = new Font("Marlett", 8.8f) };
        private readonly ButtonR _btnScroll = new() { Text = string.Empty, Height = 50, UseItAsScrollbar = true };

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the highlight color of the scrollbar checker pattern.
        /// </summary>
        public Color ButtonHilight
        {
            get => _buttonHilight;
            set
            {
                if (_buttonHilight == value) return;
                _buttonHilight = value;
                Invalidate();
            }
        }
        private Color _buttonHilight = SystemColors.ButtonHighlight;

        /// <summary>
        /// Gets or sets the current scroll position.
        /// </summary>
        public int Value
        {
            get => _value;
            set
            {
                int clamped = Math.Max(_minimum, Math.Min(_maximum, value));
                if (clamped == _value) return;
                _value = clamped;
                AdjustLocationFromValue();
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        private int _value = 0;

        /// <summary>
        /// Gets or sets the maximum scroll value.
        /// </summary>
        public int Maximum
        {
            get => _maximum;
            set
            {
                _maximum = value;
                if (_maximum < _minimum) _minimum = _maximum;
                Value = Math.Min(Value, _maximum);
            }
        }
        private int _maximum = 100;

        /// <summary>
        /// Gets or sets the minimum scroll value.
        /// </summary>
        public int Minimum
        {
            get => _minimum;
            set
            {
                _minimum = value;
                if (_minimum > _maximum) _maximum = _minimum;
                Value = Math.Max(Value, _minimum);
            }
        }
        private int _minimum = 0;

        /// <summary>
        /// Gets or sets the orientation of the scrollbar.
        /// </summary>
        public Orientation Orientation
        {
            get => _orientation;
            set
            {
                if (_orientation == value) return;
                _orientation = value;
                AdjustLayout();
            }
        }
        private Orientation _orientation = Orientation.Vertical;

        /// <summary>
        /// Gets or sets the width of the scrollbar. Clamped to [13, 50].
        /// </summary>
        public new int Width
        {
            get => base.Width;
            set
            {
                value = Math.Max(13, Math.Min(50, value));
                if (value == base.Width) return;
                base.Width = value;
                AdjustLayout();
                AdjustLocationFromValue();
                Invalidate();
                if (EnableEditingMetrics && _draggingGrip) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(WindowMetrics.ScrollWidth)));
            }
        }

        /// <summary>
        /// Gets or sets the height of the scrollbar. Clamped to [13, 50].
        /// </summary>
        public new int Height
        {
            get => base.Height;
            set
            {
                value = Math.Max(13, Math.Min(50, value));
                if (value == base.Height) return;
                base.Height = value;
                AdjustLayout();
                AdjustLocationFromValue();
                Invalidate();
                if (EnableEditingMetrics && _draggingGrip) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(WindowMetrics.ScrollHeight)));
            }
        }

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

        // Computed edit-mode flags.
        private bool IsEditFace => EnableEditingColors && _cursorOnFace;
        private bool IsEditHilight => EnableEditingColors && _cursorOnHilight;
        private bool IsEditGrip => !DesignMode && EnableEditingMetrics && _cursorOnGrip;

        #endregion

        #region Events

        /// <summary>
        /// Raised when the user clicks a color or metric region while editing is enabled.
        /// </summary>
        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);

        /// <summary>
        /// Raised when the user clicks a color or metric region while editing is enabled.
        /// </summary>
        public event EditorInvokerEventHandler EditorInvoker;

        /// <summary>
        /// Raised when <see cref="Value"/> changes.
        /// </summary>
        public event EventHandler ValueChanged;

        #endregion

        #region Scroll handle drag

        private void BtnScroll_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _draggingScroll = true;
            _dragOffset = _orientation == Orientation.Vertical ? _btnScroll.Height - e.Location.Y : _btnScroll.Width - e.Location.X;
        }

        private void BtnScroll_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_draggingScroll) return;

            Point mp = PointToClient(MousePosition);

            if (_orientation == Orientation.Vertical)
            {
                int y = mp.Y - _dragOffset;
                _btnScroll.Top = Math.Max(_btnUp.Bottom, Math.Min(_btnDown.Top - _btnScroll.Height, y));
            }
            else
            {
                int x = mp.X - _dragOffset;
                _btnScroll.Left = Math.Max(_btnUp.Right, Math.Min(_btnDown.Left - _btnScroll.Width, x));
            }

            AdjustValueFromLocation();
        }

        private void BtnScroll_MouseUp(object sender, MouseEventArgs e)
        {
            _draggingScroll = false;
        }

        #endregion

        #region Control overrides

        /// <summary>
        /// Rebuilds layout when the control is resized.
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustLayout();
        }

        /// <summary>
        /// Rebuilds layout when the dock style changes.
        /// </summary>
        protected override void OnDockChanged(EventArgs e)
        {
            base.OnDockChanged(e);
            AdjustLayout();
        }

        /// <summary>
        /// Handles grip dragging and hover-state updates.
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (DesignMode) return;

            if (EnableEditingMetrics && _draggingGrip)
            {
                if (Dock == DockStyle.Left || Dock == DockStyle.Right)
                {
                    int delta = MousePosition.X - _gripAnchor.X;
                    Width -= delta;
                }
                else
                {
                    int delta = MousePosition.Y - _gripAnchor.Y;
                    Height -= delta;
                }
                _gripAnchor = MousePosition;
            }

            if (EnableEditingColors)
            {
                bool onFace = e.Location.Y % 2 == 0 && e.Location.X % 2 == 0;
                bool onHilight = !onFace;

                if (onFace != _cursorOnFace || onHilight != _cursorOnHilight)
                {
                    _cursorOnFace = onFace;
                    _cursorOnHilight = onHilight;
                    Invalidate();
                }
            }
            else if (EnableEditingMetrics)
            {
                bool onGrip = _rectControl.Contains(e.Location);

                if (onGrip != _cursorOnGrip)
                {
                    _cursorOnGrip = onGrip;
                    Invalidate();
                }

                bool onTopBottom = _rectControl.BordersContains(e.Location) && (e.Location.Y == 0 || e.Location.Y == Height);
                bool onLeftRight = _rectControl.BordersContains(e.Location) && (e.Location.X == 0 || e.Location.X == Width);

                Cursor = onTopBottom ? Cursors.SizeNS : onLeftRight ? Cursors.SizeWE : Cursors.Default;
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
                _draggingGrip = _rectControl.BordersContains(e.Location);
                _gripAnchor = MousePosition;
            }
        }

        /// <summary>
        /// Clears hover and drag state on mouse leave.
        /// </summary>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (DesignMode) return;

            if (EnableEditingColors)
            {
                if (_cursorOnFace || _cursorOnHilight)
                {
                    _cursorOnFace = false;
                    _cursorOnHilight = false;
                    Invalidate(_rectControl);
                }
            }
            else if (EnableEditingMetrics)
            {
                if (_cursorOnGrip || _draggingGrip)
                {
                    _cursorOnGrip = false;
                    _draggingGrip = false;
                    Invalidate(_rectControl);
                }
            }
        }

        /// <summary>
        /// Ends grip drag on mouse up.
        /// </summary>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (!DesignMode && EnableEditingMetrics && _draggingGrip)
            {
                _draggingGrip = false;
                Invalidate();
            }
        }

        /// <summary>
        /// Invokes the color editor for the region under the cursor.
        /// </summary>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (!EnableEditingColors || e.Button != MouseButtons.Left) return;

            if (IsEditFace)
                EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonFace)));
            else if (IsEditHilight)
                EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(RetroDesktopColors.ButtonHilight)));
        }

        /// <summary>
        /// Scrolls by one step on mouse wheel.
        /// </summary>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            Value += e.Delta > 0 ? -1 : 1;
        }

        /// <summary>
        /// Releases child control resources.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _btnUp?.Dispose();
                _btnDown?.Dispose();
                _btnScroll?.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion

        #region Layout and value helpers

        /// <summary>
        /// Repositions and sizes child controls to match the current orientation and dimensions.
        /// </summary>
        private void AdjustLayout()
        {
            if (_orientation == Orientation.Vertical)
            {
                _btnUp.Dock = DockStyle.Top;
                _btnDown.Dock = DockStyle.Bottom;
                _btnUp.Height = base.Width;
                _btnDown.Height = base.Width;
                _btnUp.Text = "t";
                _btnDown.Text = "u";
                _btnScroll.Width = base.Width;
                _btnScroll.Left = 0;
                _btnScroll.Height = Math.Max(13, base.Height / 4);
            }
            else
            {
                _btnUp.Dock = DockStyle.Left;
                _btnDown.Dock = DockStyle.Right;
                _btnUp.Width = base.Height;
                _btnDown.Width = base.Height;
                _btnUp.Text = "3";
                _btnDown.Text = "4";
                _btnScroll.Height = base.Height;
                _btnScroll.Top = 0;
                _btnScroll.Width = Math.Max(13, base.Width / 4);
            }

            _rectControl = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
            AdjustLocationFromValue();
        }

        /// <summary>
        /// Moves the scroll handle to the position that corresponds to <see cref="Value"/>.
        /// </summary>
        private void AdjustLocationFromValue()
        {
            if (_maximum == _minimum) return;

            if (_orientation == Orientation.Vertical)
            {
                int track = _btnDown.Top - _btnUp.Bottom - _btnScroll.Height;
                int pos = _btnUp.Bottom + (int)(_value / (float)(_maximum - _minimum) * track);
                _btnScroll.Top = Math.Max(_btnUp.Bottom, Math.Min(_btnDown.Top - _btnScroll.Height, pos));
            }
            else
            {
                int track = _btnDown.Left - _btnUp.Right - _btnScroll.Width;
                int pos = _btnUp.Right + (int)(_value / (float)(_maximum - _minimum) * track);
                _btnScroll.Left = Math.Max(_btnUp.Right, Math.Min(_btnDown.Left - _btnScroll.Width, pos));
            }
        }

        /// <summary>
        /// Updates <see cref="Value"/> from the current scroll handle position.
        /// </summary>
        private void AdjustValueFromLocation()
        {
            if (_orientation == Orientation.Vertical)
            {
                int track = _btnDown.Top - _btnUp.Bottom - _btnScroll.Height;
                if (track <= 0) return;
                Value = (int)((_btnScroll.Top - _btnUp.Bottom) / (float)track * (_maximum - _minimum));
            }
            else
            {
                int track = _btnDown.Left - _btnUp.Right - _btnScroll.Width;
                if (track <= 0) return;
                Value = (int)((_btnScroll.Left - _btnUp.Right) / (float)track * (_maximum - _minimum));
            }
        }

        #endregion

        #region Paint

        /// <summary>
        /// Paints the Win9x checker-pattern background and any active edit overlays.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighSpeed;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            // 1. Flat background.
            G.Clear(BackColor);

            // 2. Win9x checker pattern: Percent50 hatch where foreground = hilight, background = face.
            using (HatchBrush hb = new(HatchStyle.Percent50, _buttonHilight, BackColor))
            {
                G.FillRectangle(hb, _rectControl);
            }

            // 3. Face color-edit overlay (even pixels).
            if (IsEditFace)
            {
                using (HatchBrush hb = new(HatchStyle.Percent50, BackColor.Invert(), Color.Transparent))
                {
                    G.FillRectangle(hb, _rectControl);
                }
            }

            // 4. Hilight color-edit overlay (odd pixels).
            if (IsEditHilight)
            {
                using (HatchBrush hb = new(HatchStyle.Percent50, Color.Transparent, _buttonHilight.Invert()))
                {
                    G.FillRectangle(hb, _rectControl);
                }
            }

            // 5. Metrics grip outline.
            if (IsEditGrip)
            {
                using (Pen p = new(BackColor.Invert()) { DashStyle = DashStyle.Dot })
                {
                    G.DrawRectangle(p, _rectControl);
                }
            }
        }

        #endregion
    }
}