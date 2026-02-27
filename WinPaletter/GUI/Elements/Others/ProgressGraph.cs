using FluentTransitions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.Interfaces;
using static WinPaletter.TypesExtensions.GraphicsExtensions;
using static WinPaletter.UI.WP.ProgressBar;

namespace WinPaletter.UI.WP
{
    [Description("Themed Progress Graph for WinPaletter UI")]
    public class ProgressGraph : UserControl
    {
        #region Constructor

        public ProgressGraph()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);

            DoubleBuffered = true;

            InitializeTaskbar();
            StyleChanged += ProgressBar_StyleChanged;

            // Initialize with starting point at 0% progress and 0 speed
            lock (_lock)
            {
                _points.Add(new GraphicData(0, 0, true)); // true = progress is already normalized (0-1)
                _currentSpeed = 0;
                _speedText = FormatSpeed(0);
                _realMaxSpeed = 0.1; // Keep the minimum
            }
        }

        #endregion

        #region Fields

        // Taskbar
        private readonly ITaskbarList3 taskbarList = !OS.WXP && !OS.WVista ? (ITaskbarList3)new CTaskbarList() : null;
        private IntPtr _formHwnd = IntPtr.Zero;
        private DateTime _lastTaskbarUpdate = DateTime.MinValue;

        // Data
        private readonly List<GraphicData> _points = [];
        private readonly object _lock = new();
        private double _currentSpeed;
        private string _speedText = "0 B/s";

        // Animation
        private Timer _marqueeTimer;
        private float _marqueeOffset = 0;
        private bool _disposed = false;
        private double _realMaxSpeed = 0.1;
        private double _displayMaxSpeed = 1;

        // UI
        private RoundedCorners _corners = RoundedCorners.All;
        private int _parentLevel = 0;

        // Animation for value resets
        private bool _isAnimatingReset = false;
        private float _resetAnimationProgress = 0;
        private Timer _resetAnimationTimer;

        // Constants
        private const int TASKBAR_UPDATE_THROTTLE_MS = 200;
        private const int RESET_ANIMATION_DURATION = 300; // ms

        #endregion

        #region Properties

        private bool CanAnimate =>
            !DesignMode &&
            Program.Style.Animations &&
            this != null &&
            Visible &&
            Parent != null &&
            Parent.Visible &&
            FindForm() != null &&
            FindForm().Visible;

        private bool _taskbarBroadcast = true;

        [Category("Behavior")]
        [Description("Determines whether the control broadcasts progress to the taskbar")]
        [DefaultValue(true)]
        public bool TaskbarBroadcast
        {
            get => _taskbarBroadcast;
            set
            {
                if (value == _taskbarBroadcast) return;

                _taskbarBroadcast = value;

                if (Parent != null && value)
                {
                    DisableOtherTaskbarBroadcasts();
                }

                ThrottleTaskbarUpdate();
            }
        }

        private ProgressBarState _state = ProgressBarState.Normal;

        [Category("Behavior")]
        [Description("The current state of the progress graph")]
        [DefaultValue(ProgressBarState.Normal)]
        public ProgressBarState State
        {
            get => _state;
            set
            {
                if (value == _state) return;

                _state = value;

                UpdateStateColor();
                ThrottleTaskbarUpdate();
                Invalidate();
            }
        }

        private ProgressBarStyle _style = ProgressBarStyle.Continuous;

        [Category("Behavior")]
        [Description("The style of the progress graph")]
        [DefaultValue(ProgressBarStyle.Continuous)]
        public ProgressBarStyle Style
        {
            get => _style;
            set
            {
                if (_style == value) return;

                _style = value;
                OnStyleChanged(EventArgs.Empty);
            }
        }

        private SpeedUnit _speedUnit = SpeedUnit.BytesPerSecond;

        [Category("Behavior")]
        [Description("The unit of speed measurement")]
        [DefaultValue(SpeedUnit.BytesPerSecond)]
        public SpeedUnit Unit
        {
            get => _speedUnit;
            set
            {
                if (_speedUnit == value) return;

                _speedUnit = value;

                // Refresh speed text with new unit
                lock (_lock)
                {
                    _speedText = FormatSpeed(_currentSpeed);
                }

                Invalidate();
            }
        }

        private Color _stateColor = Program.Style.Schemes.Main.Colors.AccentAlt;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Color StateColor
        {
            get => _stateColor;
            set
            {
                if (value == _stateColor) return;

                _stateColor = value;
                Invalidate();
            }
        }

        private int _marqueeAnimationSpeed = 100;

        [Category("Behavior")]
        [Description("The speed of the marquee animation. Higher values = faster animation")]
        [DefaultValue(100)]
        public int MarqueeAnimationSpeed
        {
            get => _marqueeAnimationSpeed;
            set
            {
                if (_marqueeAnimationSpeed != value && value > 0)
                {
                    _marqueeAnimationSpeed = value;

                    if (Style == ProgressBarStyle.Marquee)
                    {
                        RestartMarqueeTimer();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the graphic data points of the graph
        /// </summary>
        [Category("Data")]
        [Description("The graphic data points of the graph")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public GraphicData[] Value
        {
            get
            {
                lock (_lock)
                {
                    return _points.ToArray();
                }
            }
            set
            {
                lock (_lock)
                {
                    _points.Clear();

                    if (value != null && value.Length > 0)
                    {
                        // Sort points by progress to ensure correct order
                        var sortedPoints = value.OrderBy(p => p.Progress).ToArray();

                        // Handle points where progress is less than last
                        // (already handled by sorting, but we need to ensure uniqueness)
                        var uniquePoints = new List<GraphicData>();
                        double lastProgress = -1;

                        foreach (var point in sortedPoints)
                        {
                            // If progress is less than last, we skip (shouldn't happen after sorting)
                            // But we want to keep the latest point at each progress value
                            if (point.Progress > lastProgress)
                            {
                                uniquePoints.Add(point);
                                lastProgress = point.Progress;
                            }
                            else if (point.Progress == lastProgress)
                            {
                                // Replace the last point with this one (keep the most recent)
                                uniquePoints[uniquePoints.Count - 1] = point;
                            }
                            // If progress < lastProgress (shouldn't happen after sorting), skip
                        }

                        _points.AddRange(uniquePoints);

                        // Update current values based on last point
                        var lastPoint = _points[_points.Count - 1];
                        _currentSpeed = lastPoint.Speed;
                        _realMaxSpeed = Math.Max(_realMaxSpeed, _points.Max(p => p.Speed));
                        _speedText = FormatSpeed(_currentSpeed);

                        // Check if we need to trigger reset animation (if progress went backwards)
                        // Note: This is handled by the sorting, but we might want to animate the reset
                        if (value.Length != uniquePoints.Count || value.Length != _points.Count)
                        {
                            // Some points were removed due to ordering issues
                            if (CanAnimate)
                            {
                                StartResetAnimation();
                            }
                        }
                    }
                    else
                    {
                        _currentSpeed = 0;
                        _realMaxSpeed = 1;
                        _displayMaxSpeed = 1;
                        _speedText = FormatSpeed(0);
                    }
                }

                ThrottleTaskbarUpdate();
                Invalidate();
            }
        }

        /// <summary>
        /// Gets the current speed
        /// </summary>
        [Browsable(false)]
        public double CurrentSpeed => _currentSpeed;

        /// <summary>
        /// Gets the maximum speed recorded
        /// </summary>
        [Browsable(false)]
        public double MaxSpeed => _realMaxSpeed;

        /// <summary>
        /// Gets the current progress value (0-100)
        /// </summary>
        [Browsable(false)]
        public double Progress
        {
            get
            {
                lock (_lock)
                {
                    return _points.Count > 0 ? _points.Last().Progress * 100 : 0;
                }
            }
        }

        #endregion

        #region GraphicData Structure

        /// <summary>
        /// Represents a data point in the progress graph
        /// </summary>
        [Serializable]
        public struct GraphicData
        {
            private double _progress;
            private double _speed;

            /// <summary>
            /// Creates a new graphic data point
            /// </summary>
            /// <param name="progress">Progress value (0-100)</param>
            /// <param name="speed">Speed value</param>
            public GraphicData(double progress, double speed)
            {
                _progress = Math.Max(0, Math.Min(100, progress)) / 100.0;
                _speed = speed;
            }

            /// <summary>
            /// Creates a new graphic data point with progress as 0-1 range
            /// </summary>
            /// <param name="progress">Progress value (0-1)</param>
            /// <param name="speed">Speed value</param>
            /// <param name="normalized">Indicates progress is already normalized (0-1)</param>
            public GraphicData(double progress, double speed, bool normalized)
            {
                if (normalized)
                {
                    _progress = Math.Max(0, Math.Min(1, progress));
                }
                else
                {
                    _progress = Math.Max(0, Math.Min(100, progress)) / 100.0;
                }
                _speed = speed;
            }

            /// <summary>
            /// Gets or sets the progress value (0-1 range)
            /// </summary>
            public double Progress
            {
                readonly get => _progress;
                set => _progress = Math.Max(0, Math.Min(1, value));
            }

            /// <summary>
            /// Gets or sets the progress value as percentage (0-100)
            /// </summary>
            public double ProgressPercent
            {
                readonly get => _progress * 100;
                set => _progress = Math.Max(0, Math.Min(100, value)) / 100.0;
            }

            /// <summary>
            /// Gets or sets the speed value
            /// </summary>
            public double Speed
            {
                readonly get => _speed;
                set => _speed = Math.Max(0, value);
            }

            /// <summary>
            /// Returns a string representation of the graphic data
            /// </summary>
            public override readonly string ToString()
            {
                return $"Progress: {ProgressPercent:F1}%, Speed: {Speed:F0}";
            }
        }

        #endregion

        #region Enums

        /// <summary>
        /// Progress bar state
        /// </summary>
        public enum ProgressBarState
        {
            /// <summary>
            /// Indicates normal progress
            /// </summary>
            Normal,

            /// <summary>
            /// Indicates an error in the progress
            /// </summary>
            Error,

            /// <summary>
            /// Indicates paused progress
            /// </summary>
            Pause
        }

        /// <summary>
        /// Enumerates the styles available for a custom progress bar.
        /// </summary>
        public enum ProgressBarStyle
        {
            /// <summary>
            /// Represents a continuous progress bar style where the progress is displayed incrementally.
            /// </summary>
            Continuous,

            /// <summary>
            /// Represents a marquee progress bar style where the progress is shown as an animated, continuous pattern,
            /// indicating that the operation is in progress without specifying the completion percentage.
            /// </summary>
            Marquee
        }

        /// <summary>
        /// Speed unit for display
        /// </summary>
        public enum SpeedUnit
        {
            /// <summary>
            /// Bytes per second (B/s, KB/s, MB/s, GB/s)
            /// </summary>
            BytesPerSecond,

            /// <summary>
            /// Items per second (item/s)
            /// </summary>
            ItemsPerSecond
        }

        #endregion

        #region Public Methods

        public void Add(double percent, double speed)
        {
            lock (_lock)
            {
                percent = Math.Max(0, Math.Min(100, percent));
                double progress = percent / 100.0;

                // Check if new value is less than last
                if (_points.Count > 0 && progress < _points[_points.Count - 1].Progress)
                {
                    // Remove all points after this progress and animate
                    int indexToKeep = _points.FindLastIndex(p => p.Progress <= progress);

                    if (indexToKeep >= 0)
                    {
                        // Keep points up to indexToKeep, remove the rest
                        int pointsToRemove = _points.Count - (indexToKeep + 1);
                        if (pointsToRemove > 0)
                        {
                            _points.RemoveRange(indexToKeep + 1, pointsToRemove);

                            // Trigger reset animation
                            if (CanAnimate)
                            {
                                StartResetAnimation();
                            }
                        }
                    }
                    else
                    {
                        // New progress is less than all existing points, clear everything
                        _points.Clear();

                        // Trigger reset animation
                        if (CanAnimate)
                        {
                            StartResetAnimation();
                        }
                    }
                }

                _currentSpeed = SmoothSpeed(speed);

                // Improved max speed calculation for first values
                if (_points.Count == 0)
                {
                    // For first point, set max speed to something reasonable
                    // Use the current speed or a minimum value
                    _realMaxSpeed = Math.Max(0.1, speed * 1.2); // Add 20% headroom
                }
                else
                {
                    // For subsequent points, update max speed with headroom
                    _realMaxSpeed = Math.Max(_realMaxSpeed, speed * 1.1); // Add 10% headroom
                }

                _speedText = FormatSpeed(_currentSpeed);

                // Add new point
                if (_points.Count > 0 && progress <= _points[_points.Count - 1].Progress)
                {
                    _points[_points.Count - 1] = new GraphicData(progress, _currentSpeed, true);
                }
                else
                {
                    _points.Add(new GraphicData(progress, _currentSpeed, true));
                }

                ThrottleTaskbarUpdate();
            }

            // Reset taskbar when progress reaches 100%
            if (percent >= 100)
            {
                SetProgressValue(0);
                SetProgressState(TaskbarProgressBarState.NoProgress);
            }

            Invalidate();
        }

        /// <summary>
        /// Resets the graph to its initial state
        /// </summary>
        /// <param name="animate">Whether to animate the reset transition</param>
        public void Reset(bool animate = false)
        {
            if (!animate)
            {
                StopResetAnimation();
            }

            lock (_lock)
            {
                _points.Clear();
                _currentSpeed = 0;
                _realMaxSpeed = 0.1;
                _displayMaxSpeed = 0.1;
                _speedText = FormatSpeed(0);
            }

            SetProgressValue(0);
            SetProgressState(TaskbarProgressBarState.NoProgress);

            // Trigger reset animation if requested
            if (animate && CanAnimate)
            {
                StartResetAnimation();
            }

            Invalidate();
        }

        /// <summary>
        /// Gets all graphic data points
        /// </summary>
        public GraphicData[] GetData()
        {
            lock (_lock)
            {
                return _points.ToArray();
            }
        }

        /// <summary>
        /// Sets all graphic data points
        /// </summary>
        public void SetData(GraphicData[] data)
        {
            lock (_lock)
            {
                _points.Clear();

                if (data != null && data.Length > 0)
                {
                    _points.AddRange(data);

                    // Update current values based on last point
                    var lastPoint = data[data.Length - 1];
                    _currentSpeed = lastPoint.Speed;
                    _realMaxSpeed = Math.Max(_realMaxSpeed, _points.Max(p => p.Speed));
                    _speedText = FormatSpeed(_currentSpeed);
                }
            }

            ThrottleTaskbarUpdate();
            Invalidate();
        }

        #endregion

        #region Private Methods

        private void InitializeTaskbar()
        {
            taskbarList?.HrInit();
        }

        private void DisableOtherTaskbarBroadcasts()
        {
            var form = FindForm();
            if (form == null) return;

            foreach (ProgressBar pb in form.GetAllControls().OfType<ProgressBar>())
            {
                pb.TaskbarBroadcast = false;
            }

            foreach (ProgressGraph pg in form.GetAllControls().OfType<ProgressGraph>())
            {
                if (pg != this) pg.TaskbarBroadcast = false;
            }
        }

        private void UpdateStateColor()
        {
            Color color = _state switch
            {
                ProgressBarState.Normal => Program.Style.Schemes.Main.Colors.AccentAlt,
                ProgressBarState.Error => Program.Style.Schemes.Secondary.Colors.AccentAlt,
                ProgressBarState.Pause => Program.Style.Schemes.Tertiary.Colors.AccentAlt,
                _ => Program.Style.Schemes.Main.Colors.AccentAlt
            };

            if (CanAnimate)
            {
                Transition.With(this, nameof(StateColor), color)
                    .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
            }
            else
            {
                StateColor = color;
            }
        }

        private void UpdateRegion()
        {
            if (IsDisposed) return;

            Rectangle rect = new(0, 0, Width, Height);
            Region?.Dispose();

            if (DesignMode)
            {
                Region = new Region(rect);
                return;
            }

            _corners = this.UndockedCorners();
            using (GraphicsPath path = rect.Round(corners: _corners))
            {
                Region = new(path);
            }
        }

        private double SmoothSpeed(double newSpeed)
        {
            const double alpha = 0.2;
            return (_currentSpeed * (1 - alpha)) + (newSpeed * alpha);
        }

        private double GetGraphMaxSpeed()
        {
            if (_realMaxSpeed <= 0)
                _realMaxSpeed = 0.1; // Changed from 1 to 0.1

            double delta = _realMaxSpeed - _displayMaxSpeed;

            // Fast expand, slow shrink - but with adaptive rate
            double factor = delta > 0 ? 0.25 : 0.05;

            // Add a minimum speed floor to prevent going too low
            _displayMaxSpeed += delta * factor;

            // Ensure we never go below a minimum display speed
            return Math.Max(0.1, _displayMaxSpeed);
        }

        private string FormatSpeed(double speed)
        {
            if (speed <= 0) return _speedUnit == SpeedUnit.BytesPerSecond ? "0 B/s" : "0 item/s";

            if (_speedUnit == SpeedUnit.ItemsPerSecond)
            {
                // For items per second, just format the number
                return speed < 10
                    ? $"{speed:F1} item/s"
                    : $"{speed:F0} item/s";
            }
            else // BytesPerSecond
            {
                string[] units = { "B/s", "KB/s", "MB/s", "GB/s", "TB/s" };
                int unit = 0;
                double displaySpeed = speed;

                while (displaySpeed >= 1024 && unit < units.Length - 1)
                {
                    displaySpeed /= 1024;
                    unit++;
                }

                return displaySpeed < 10
                    ? $"{displaySpeed:F1} {units[unit]}"
                    : $"{displaySpeed:F0} {units[unit]}";
            }
        }

        private void StartResetAnimation()
        {
            if (_disposed || DesignMode) return;

            StopResetAnimation();

            _isAnimatingReset = true;
            _resetAnimationProgress = 0;

            _resetAnimationTimer = new System.Windows.Forms.Timer();
            _resetAnimationTimer.Interval = 16; // ~60 FPS
            _resetAnimationTimer.Tick += (s, e) =>
            {
                _resetAnimationProgress += 16f / RESET_ANIMATION_DURATION;

                if (_resetAnimationProgress >= 1)
                {
                    _resetAnimationProgress = 1;
                    _isAnimatingReset = false;
                    _resetAnimationTimer.Stop();
                }

                Invalidate();
            };

            _resetAnimationTimer.Start();
        }

        private void StopResetAnimation()
        {
            if (_resetAnimationTimer != null)
            {
                _resetAnimationTimer.Stop();
                _resetAnimationTimer.Dispose();
                _resetAnimationTimer = null;
            }

            _isAnimatingReset = false;
            _resetAnimationProgress = 0;
        }

        #endregion

        #region Taskbar Methods

        private void ThrottleTaskbarUpdate()
        {
            if (DesignMode || _formHwnd == IntPtr.Zero) return;

            if ((DateTime.Now - _lastTaskbarUpdate).TotalMilliseconds >= TASKBAR_UPDATE_THROTTLE_MS)
            {
                _lastTaskbarUpdate = DateTime.Now;
                UpdateTaskbar();
            }
        }

        private void UpdateTaskbar()
        {
            if (DesignMode || _formHwnd == IntPtr.Zero || !_taskbarBroadcast) return;

            if (Style == ProgressBarStyle.Marquee)
            {
                SetProgressState(TaskbarProgressBarState.Indeterminate);
            }
            else
            {
                SetProgressStateBasedOnState();

                lock (_lock)
                {
                    if (_points.Count > 0)
                    {
                        double lastProgress = _points[_points.Count - 1].Progress;
                        SetProgressValue((int)(lastProgress * 100));
                    }
                }
            }
        }

        private void SetProgressStateBasedOnState()
        {
            if (State == ProgressBarState.Error)
                SetProgressState(TaskbarProgressBarState.Error);
            else if (State == ProgressBarState.Pause)
                SetProgressState(TaskbarProgressBarState.Paused);
            else if (State == ProgressBarState.Normal)
                SetProgressState(TaskbarProgressBarState.Normal);
            else
                SetProgressState(TaskbarProgressBarState.NoProgress);
        }

        private void SetProgressState(TaskbarProgressBarState state)
        {
            if (DesignMode || _formHwnd == IntPtr.Zero) return;
            taskbarList?.SetProgressState(_formHwnd, state);
        }

        private void SetProgressValue(int value)
        {
            if (DesignMode || _formHwnd == IntPtr.Zero) return;

            int val = value;

            if (State == ProgressBarState.Normal && (value == 0 || value == 100))
            {
                SetProgressState(TaskbarProgressBarState.NoProgress);
                val = 0;
            }

            taskbarList?.SetProgressValue(_formHwnd, (ulong)val, 100);
        }

        #endregion

        #region Timer Management

        private void EnsureMarqueeTimerInitialized()
        {
            if (_marqueeTimer != null || _disposed || DesignMode) return;

            // Calculate interval based on MarqueeAnimationSpeed
            int interval = Math.Max(1, 2000 / Math.Max(1, _marqueeAnimationSpeed));

            _marqueeTimer = new System.Windows.Forms.Timer { Interval = interval };
            _marqueeTimer.Tick += (s, e) =>
            {
                if (_disposed || DesignMode) return;

                _marqueeOffset += 0.02f;
                if (_marqueeOffset > 1) _marqueeOffset = 0;
                Invalidate();
            };
        }

        private void StartMarqueeTimer()
        {
            if (_disposed || DesignMode || !CanAnimate) return;

            EnsureMarqueeTimerInitialized();

            if (_marqueeTimer != null && !_marqueeTimer.Enabled)
            {
                _marqueeTimer.Start();
            }
        }

        private void StopMarqueeTimer()
        {
            if (_marqueeTimer != null)
            {
                _marqueeTimer.Stop();
            }
            _marqueeOffset = 0;
        }

        private void RestartMarqueeTimer()
        {
            if (Style == ProgressBarStyle.Marquee && Visible && CanAnimate)
            {
                StopMarqueeTimer();
                StartMarqueeTimer();
            }
        }

        #endregion

        #region Drawing Methods

        private void DrawGrid(Graphics g, Rectangle rect, Color gridColor)
        {
            using (Pen solidPen = new(gridColor) { DashStyle = DashStyle.Solid })
            {
                int top = rect.Top;
                int bottom = rect.Bottom;
                int height = rect.Height;

                // Draw 6 horizontal lines (0%, 20%, 40%, 60%, 80%, 100%)
                for (int i = 0; i <= 5; i++)
                {
                    float y = top + (height * i / 5f);
                    g.DrawLine(solidPen, rect.Left, y, rect.Right, y);
                }

                // Draw vertical grid lines
                for (int i = 0; i <= 10; i++)
                {
                    float x = rect.Left + (rect.Width * i / 10f);
                    g.DrawLine(solidPen, x, top, x, bottom);
                }
            }
        }

        private void DrawMarquee(Graphics g, Rectangle rect, Color graphColor)
        {
            if (Style != ProgressBarStyle.Marquee) return;

            int marqueeWidth = (int)(rect.Width * 0.25d);

            // Calculate marquee position - starts from -marqueeWidth to rect.Width + 1
            float marqueeLeft = -marqueeWidth + (rect.Width + marqueeWidth) * _marqueeOffset;

            // Create the marquee rectangle that extends beyond the graph bounds
            RectangleF marqueeRect = new(marqueeLeft, rect.Top, marqueeWidth, rect.Height);

            // Set clip region to only show within the graph area
            Region originalClip = g.Clip;
            g.SetClip(rect);

            // Create a single consistent gradient
            using (LinearGradientBrush brush = new(
                marqueeRect,
                Color.Transparent,
                Color.Transparent,
                LinearGradientMode.Horizontal))
            {
                // One gradient style everywhere: Transparent - Color - Transparent
                ColorBlend cb = new()
                {
                    Colors = [
                        Color.Transparent,                  // Left edge transparent
                Color.FromArgb(80, graphColor),             // Middle solid
                Color.Transparent                           // Right edge transparent
                    ],
                    Positions = [0f, 0.5f, 1f]
                };
                brush.InterpolationColors = cb;

                // Draw the marquee
                g.FillRectangle(brush, marqueeRect);
            }

            // Restore original clip
            g.ResetClip();
        }

        private void DrawCurrentSpeedLine(Graphics g, Rectangle rect)
        {
            // Don't draw speed line in Pause or Error states or Marquee mode
            if (_state == ProgressBarState.Pause || _state == ProgressBarState.Error || Style == ProgressBarStyle.Marquee) return;

            lock (_lock)
            {
                if (_points.Count == 0) return;

                int graphTop = rect.Top;
                int graphBottom = rect.Bottom;
                int graphHeight = graphBottom - graphTop;

                double maxSpeed = _realMaxSpeed <= 0 ? 1 : _realMaxSpeed;

                double ratio = Math.Max(0, Math.Min(1, _currentSpeed / maxSpeed));
                float y = graphBottom - (float)(graphHeight * ratio);

                // Ensure Y stays within bounds (clamp to control edges)
                y = Math.Max(graphTop, Math.Min(y, graphBottom));

                // Apply reset animation if active
                if (_isAnimatingReset)
                {
                    // Fade out the line during reset
                    int alpha = (int)(255 * (1 - _resetAnimationProgress));
                    using (Pen pen = new(Color.FromArgb(alpha, ForeColor)))
                    {
                        g.DrawLine(pen, rect.Left, y, rect.Right, y);
                    }
                }
                else
                {
                    // Draw the speed line across the entire control
                    using (Pen pen = new(ForeColor))
                    {
                        g.DrawLine(pen, rect.Left, y, rect.Right, y);
                    }
                }

                // Draw the speed text above the line
                using (SolidBrush brush = new(ForeColor))
                {
                    SizeF textSize = g.MeasureString(_speedText, Fonts.Console);
                    float textX = rect.Right - textSize.Width - 1;

                    // Position text above the line
                    float textY = y - textSize.Height - 1;

                    // Ensure text stays within the control bounds
                    textX = Math.Max(rect.Left + 2, Math.Min(rect.Right - textSize.Width - 2, textX));
                    textY = Math.Max(rect.Top + 2, Math.Min(rect.Bottom - textSize.Height - 2, textY));

                    // Apply reset animation if active
                    if (_isAnimatingReset)
                    {
                        int alpha = (int)(255 * (1 - _resetAnimationProgress));
                        using (SolidBrush fadeBrush = new(Color.FromArgb(alpha, ForeColor)))
                        {
                            g.DrawString(_speedText, Fonts.Console, fadeBrush, textX, textY);
                        }
                    }
                    else
                    {
                        g.DrawString(_speedText, Fonts.Console, brush, textX, textY);
                    }
                }
            }
        }

        private void DrawGraph(Graphics g, Rectangle rect, Color graphColor)
        {
            lock (_lock)
            {
                if (Style == ProgressBarStyle.Marquee)
                {
                    DrawMarquee(g, rect, graphColor);
                    return;
                }

                if (_points.Count < 1) return;

                int graphTop = rect.Top;
                int graphBottom = rect.Bottom - 1;
                int graphHeight = graphBottom - graphTop;

                double maxSpeed = GetGraphMaxSpeed();

                // Apply smoothing to Y positions
                float[] yPositions = new float[_points.Count];
                float[] xPositions = new float[_points.Count];

                for (int i = 0; i < _points.Count; i++)
                {
                    xPositions[i] = rect.Left + (float)(rect.Width * _points[i].Progress);
                    double ratio = Math.Max(0, Math.Min(1, _points[i].Speed / maxSpeed));
                    yPositions[i] = graphBottom - (float)(graphHeight * ratio);
                    yPositions[i] = Math.Max(graphTop, Math.Min(yPositions[i], graphBottom));
                }

                // Apply moving average smoothing to Y positions
                float[] smoothedY = new float[_points.Count];
                int smoothWindow = 3;

                for (int i = 0; i < _points.Count; i++)
                {
                    float sum = 0;
                    int count = 0;

                    for (int j = Math.Max(0, i - smoothWindow);
                             j <= Math.Min(_points.Count - 1, i + smoothWindow); j++)
                    {
                        sum += yPositions[j];
                        count++;
                    }

                    smoothedY[i] = sum / count;
                }

                PointF[] points = new PointF[_points.Count];
                for (int i = 0; i < _points.Count; i++)
                {
                    points[i] = new PointF(xPositions[i], smoothedY[i]);
                }

                if (points.Length == 1)
                {
                    // Single point handling
                    using (Pen graphPen = new(graphColor, 1))
                    {
                        graphPen.StartCap = LineCap.Round;
                        graphPen.EndCap = LineCap.Round;
                        g.DrawLine(graphPen, points[0].X, graphTop, points[0].X, graphBottom);
                    }

                    using (SolidBrush fillBrush = new(graphColor))
                    using (GraphicsPath fillPath = new())
                    {
                        fillPath.AddLine(rect.Left, graphBottom, points[0].X, graphBottom);
                        fillPath.AddLine(points[0].X, graphBottom, points[0].X, graphTop);
                        fillPath.AddLine(points[0].X, graphTop, rect.Left, graphTop);
                        fillPath.CloseFigure();
                        g.FillPath(fillBrush, fillPath);
                    }
                }
                else
                {
                    // Draw smooth line using cardinal spline
                    using (Pen graphPen = new(graphColor, 1))
                    {
                        graphPen.StartCap = LineCap.Round;
                        graphPen.EndCap = LineCap.Round;
                        graphPen.LineJoin = LineJoin.Round;

                        // Use cardinal spline with tension 0.5 for smooth curves
                        g.DrawCurve(graphPen, points, 0.5f);
                    }

                    // Fill below the line (main color)
                    using (SolidBrush fillBrush = new(graphColor))
                    using (GraphicsPath fillPath = new())
                    {
                        fillPath.AddCurve(points, 0.5f);
                        fillPath.AddLine(points[points.Length - 1].X, graphBottom,
                                       points[0].X, graphBottom);
                        fillPath.CloseFigure();
                        g.FillPath(fillBrush, fillPath);
                    }

                    // Fill above the line (semi-transparent)
                    using (SolidBrush fillBrushAbove = new(Color.FromArgb(80, graphColor)))
                    using (GraphicsPath fillPathAbove = new())
                    {
                        fillPathAbove.AddCurve(points, 0.5f);
                        fillPathAbove.AddLine(points[points.Length - 1].X, graphTop,
                                            points[0].X, graphTop);
                        fillPathAbove.CloseFigure();
                        g.FillPath(fillBrushAbove, fillPathAbove);
                    }
                }
            }
        }

        #endregion

        #region Event Handlers

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            _parentLevel = this.Level();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateRegion();
        }

        protected override void OnDockChanged(EventArgs e)
        {
            base.OnDockChanged(e);
            UpdateRegion();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (DesignMode) return;

            if (Visible)
            {
                ThrottleTaskbarUpdate();
                if (Style == ProgressBarStyle.Marquee && CanAnimate)
                {
                    StartMarqueeTimer();
                }
            }
            else
            {
                SetProgressState(TaskbarProgressBarState.NoProgress);
                StopMarqueeTimer();
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (!DesignMode)
            {
                var form = FindForm();
                if (form != null)
                {
                    _formHwnd = form.Parent == null
                        ? form.Handle
                        : form.Parent is TabPage tabPage && tabPage.Parent != null
                            ? tabPage.Parent.FindForm()?.Handle ?? Forms.MainForm.Handle
                            : Forms.MainForm.Handle;
                }
            }

            taskbarList?.HrInit();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            SetProgressValue(0);
            SetProgressState(TaskbarProgressBarState.NoProgress);
            StopMarqueeTimer();
            StopResetAnimation();
            base.OnHandleDestroyed(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    StopMarqueeTimer();
                    _marqueeTimer?.Dispose();

                    StopResetAnimation();

                    if (_formHwnd != IntPtr.Zero && taskbarList != null)
                    {
                        try
                        {
                            taskbarList.SetProgressState(_formHwnd, TaskbarProgressBarState.NoProgress);
                        }
                        catch { }
                    }
                }
                _disposed = true;
            }
            base.Dispose(disposing);
        }

        protected new virtual void OnStyleChanged(EventArgs e)
        {
            ThrottleTaskbarUpdate();

            if (Style == ProgressBarStyle.Marquee)
            {
                StartMarqueeTimer();
            }
            else
            {
                StopMarqueeTimer();
            }

            Invalidate();
            StyleChanged?.Invoke(this, e);
        }

        private void ProgressBar_StyleChanged(object sender, EventArgs e)
        {
            ThrottleTaskbarUpdate();
            Invalidate();
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Leave empty for transparency
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode
                ? TextRenderingHint.ClearTypeGridFit
                : Program.Style.TextRenderingHint;

            var scheme = Enabled
                ? Program.Style.Schemes.Main
                : Program.Style.Schemes.Disabled;

            Rectangle rect = new(0, 0, Width - 1, Height - 1);

            Color backColor = scheme.Colors.Back(_parentLevel);
            Color lineColor = scheme.Colors.Line_Hover(_parentLevel);
            Color gridColor = scheme.Colors.Line_Hover(_parentLevel);

            // Draw background
            using (SolidBrush brush = new(backColor))
            {
                G.FillRoundedRect(brush, rect);
            }

            // Setup clipping for rounded corners
            GraphicsPath clipPath = null;
            if (Program.Style.RoundedCorners)
            {
                clipPath?.Dispose();
                clipPath = rect.Round(corners: _corners);
                G.SetClip(clipPath);
            }

            // Draw graph components
            DrawGrid(G, rect, gridColor);
            DrawGraph(G, rect, _stateColor);
            DrawCurrentSpeedLine(G, rect);

            // Reset clipping
            G.ResetClip();
            clipPath?.Dispose();

            // Draw border
            using (Pen pen = new(lineColor))
            {
                G.DrawRoundedRectBeveledReverse(pen, rect);
            }
        }

        #endregion

        #region Events

        public new event EventHandler StyleChanged;

        #endregion
    }
}