using FluentTransitions;
using Newtonsoft.Json.Linq;
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

            lock (_lock)
            {
                // Do NOT add an initial point — the graph starts empty/clean
                _currentSpeed = 0;
                _speedText = FormatSpeed(0);
                _targetMaxSpeed = -1;   // sentinel: "no data yet"
                _displayMaxSpeed = -1;  // sentinel: "no data yet"
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
        private string _speedText = $"0 {Program.Localization.Strings.General.ByteSizeUnit}/{Program.Localization.Strings.General.SecondUnit}";

        // Y-axis scale — target is set from data, display lerps toward it via a timer
        private double _targetMaxSpeed = 0.1;   // true max of current data
        private double _displayMaxSpeed = 0.1;  // what we actually draw against (lerped)

        // X animation (gap fill)
        private double _animFromProgress = 0;
        private double _animToProgress = 0;
        private float _xAnimProgress = 0;       // 0→1
        private Timer _xAnimTimer;
        private const int X_ANIM_DURATION = 350;

        // Scale animation timer (drives Y-axis lerp independently of data updates)
        private Timer _scaleAnimTimer;
        private const int SCALE_ANIM_INTERVAL = 16;
        private const double SCALE_LERP_UP = 0.25;  // fast expand
        private const double SCALE_LERP_DOWN = 0.12;  // moderate shrink

        // Marquee
        private Timer _marqueeTimer;
        private float _marqueeOffset = 0;

        // Reset animation
        private bool _isAnimatingReset = false;
        private float _resetAnimationProgress = 0;
        private Timer _resetAnimationTimer;
        private const int RESET_ANIMATION_DURATION = 300;

        // UI
        private RoundedCorners _corners = RoundedCorners.All;
        private int _parentLevel = 0;
        private bool _disposed = false;

        // Constants
        private const int TASKBAR_UPDATE_THROTTLE_MS = 200;

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
                if (Parent != null && value) DisableOtherTaskbarBroadcasts();
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
                lock (_lock) { _speedText = FormatSpeed(_currentSpeed); }
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
                    if (Style == ProgressBarStyle.Marquee) RestartMarqueeTimer();
                }
            }
        }

        [Category("Data")]
        [Description("The graphic data points of the graph")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public GraphicData[] Value
        {
            get { lock (_lock) { return _points.ToArray(); } }
            set
            {
                lock (_lock)
                {
                    _points.Clear();

                    if (value != null && value.Length > 0)
                    {
                        var sortedPoints = value.OrderBy(p => p.Progress).ToArray();
                        var uniquePoints = new List<GraphicData>();
                        double lastProgress = -1;

                        foreach (var point in sortedPoints)
                        {
                            if (point.Progress > lastProgress)
                            {
                                uniquePoints.Add(point);
                                lastProgress = point.Progress;
                            }
                            else if (point.Progress == lastProgress)
                            {
                                uniquePoints[uniquePoints.Count - 1] = point;
                            }
                        }

                        _points.AddRange(uniquePoints);

                        var lastPoint = _points[_points.Count - 1];
                        _currentSpeed = lastPoint.Speed;
                        _speedText = FormatSpeed(_currentSpeed);

                        RecalculateTargetMaxSpeed();

                        if (value.Length != uniquePoints.Count || value.Length != _points.Count)
                        {
                            if (CanAnimate) StartResetAnimation();
                        }
                    }
                    else
                    {
                        _currentSpeed = 0;
                        _targetMaxSpeed = 0.1;
                        _displayMaxSpeed = 0.1;
                        _speedText = FormatSpeed(0);
                    }
                }

                ThrottleTaskbarUpdate();
                Invalidate();
            }
        }

        [Browsable(false)]
        public double CurrentSpeed => _currentSpeed;

        [Browsable(false)]
        public double MaxSpeed => _targetMaxSpeed;

        [Browsable(false)]
        public double Progress
        {
            get
            {
                lock (_lock)
                {
                    return _points.Count > 0 ? _points[_points.Count - 1].Progress * 100 : 0;
                }
            }
        }

        #endregion

        #region GraphicData Structure

        [Serializable]
        public struct GraphicData
        {
            private double _progress;
            private double _speed;

            public GraphicData(double progress, double speed)
            {
                _progress = Math.Max(0, Math.Min(100, progress)) / 100.0;
                _speed = speed;
            }

            public GraphicData(double progress, double speed, bool normalized)
            {
                _progress = normalized
                    ? Math.Max(0, Math.Min(1, progress))
                    : Math.Max(0, Math.Min(100, progress)) / 100.0;
                _speed = speed;
            }

            public double Progress
            {
                get { return _progress; }
                set { _progress = Math.Max(0, Math.Min(1, value)); }
            }

            public double ProgressPercent
            {
                get { return _progress * 100; }
                set { _progress = Math.Max(0, Math.Min(100, value)) / 100.0; }
            }

            public double Speed
            {
                get { return _speed; }
                set { _speed = Math.Max(0, value); }
            }

            public override string ToString()
            {
                return string.Format("Progress: {0:F1}%, Speed: {1:F0}", ProgressPercent, Speed);
            }
        }

        #endregion

        #region Enums

        public enum ProgressBarState { Normal, Error, Pause }
        public enum ProgressBarStyle { Continuous, Marquee }
        public enum SpeedUnit { BytesPerSecond, ItemsPerSecond }

        #endregion

        #region Public Methods

        public void Add(double percent, double speed)
        {
            double previousLastProgress = 0;

            lock (_lock)
            {
                percent = Math.Max(0, Math.Min(100, percent));
                double progress = percent / 100.0;

                if (_points.Count > 0)
                    previousLastProgress = _points[_points.Count - 1].Progress;

                // Handle backwards progress
                if (_points.Count > 0 && progress < _points[_points.Count - 1].Progress)
                {
                    int indexToKeep = _points.FindLastIndex(p => p.Progress <= progress);
                    if (indexToKeep >= 0)
                    {
                        int toRemove = _points.Count - (indexToKeep + 1);
                        if (toRemove > 0)
                        {
                            _points.RemoveRange(indexToKeep + 1, toRemove);
                            if (CanAnimate) StartResetAnimation();
                        }
                    }
                    else
                    {
                        _points.Clear();
                        if (CanAnimate) StartResetAnimation();
                    }
                    previousLastProgress = progress;
                }

                _currentSpeed = SmoothSpeed(speed);
                _speedText = FormatSpeed(_currentSpeed);

                if (_points.Count > 0 && progress <= _points[_points.Count - 1].Progress)
                    _points[_points.Count - 1] = new GraphicData(progress, _currentSpeed, true);
                else
                    _points.Add(new GraphicData(progress, _currentSpeed, true));

                // Update target scale from actual data — can go up AND down
                RecalculateTargetMaxSpeed();

                ThrottleTaskbarUpdate();
            }

            // Kick off scale animation timer so _displayMaxSpeed lerps to _targetMaxSpeed
            EnsureScaleAnimTimerRunning();

            // Animate X if there's a visible gap
            double newLastProgress;
            lock (_lock) { newLastProgress = _points[_points.Count - 1].Progress; }

            if (CanAnimate && newLastProgress > previousLastProgress + 0.001)
                StartXAnimation(previousLastProgress, newLastProgress);
            else
                _xAnimProgress = 1f;

            if (percent >= 100)
            {
                SetProgressValue(0);
                SetProgressState(TaskbarProgressBarState.NoProgress);
            }

            Invalidate();
        }

        public void Reset(bool animate = false)
        {
            if (!animate) StopResetAnimation();

            lock (_lock)
            {
                _points.Clear();
                _currentSpeed = 0;
                _targetMaxSpeed = -1;   // sentinel
                _displayMaxSpeed = -1;  // sentinel
                _speedText = FormatSpeed(0);
            }

            SetProgressValue(0);
            SetProgressState(TaskbarProgressBarState.NoProgress);
            if (animate && CanAnimate) StartResetAnimation();
            Invalidate();
        }

        public GraphicData[] GetData()
        {
            lock (_lock) { return _points.ToArray(); }
        }

        public void SetData(GraphicData[] data)
        {
            lock (_lock)
            {
                _points.Clear();

                if (data != null && data.Length > 0)
                {
                    _points.AddRange(data);
                    var lastPoint = data[data.Length - 1];
                    _currentSpeed = lastPoint.Speed;
                    _speedText = FormatSpeed(_currentSpeed);
                    RecalculateTargetMaxSpeed();
                }
            }

            EnsureScaleAnimTimerRunning();
            ThrottleTaskbarUpdate();
            Invalidate();
        }

        #endregion

        #region Private Methods — Data

        /// <summary>
        /// Recomputes _targetMaxSpeed purely from current point data.
        /// This can go both up and down. Must be called inside _lock.
        /// </summary>
        private void RecalculateTargetMaxSpeed()
        {
            if (_points.Count == 0)
            {
                _targetMaxSpeed = -1;
                _displayMaxSpeed = -1;
                return;
            }

            double maxInPoints = 0;
            foreach (var p in _points)
                if (p.Speed > maxInPoints) maxInPoints = p.Speed;

            double trueMax = Math.Max(maxInPoints, _currentSpeed);
            if (trueMax <= 0) trueMax = 0.1;

            // No headroom — peak always maps to graphTop
            _targetMaxSpeed = trueMax;

            if (_displayMaxSpeed <= 0)
            {
                // First data ever (or after reset): set display scale to 2x so
                // the first point renders at 50% height, matching Windows behavior.
                // As faster speeds arrive, the scale will snap/shrink toward them.
                _displayMaxSpeed = trueMax * 2.0;
            }
            // For subsequent points, _displayMaxSpeed is managed by ScaleAnimTimer_Tick
            // which snaps up instantly and lerps down smoothly.
        }

        private double SmoothSpeed(double newSpeed)
        {
            const double alpha = 0.2;
            return (_currentSpeed * (1 - alpha)) + (newSpeed * alpha);
        }

        private string FormatSpeed(double speed)
        {
            if (speed <= 0) return _speedUnit == SpeedUnit.BytesPerSecond ?
                    $"0 {Program.Localization.Strings.General.ByteSizeUnit}/{Program.Localization.Strings.General.SecondUnit}" :
                    $"0 {Program.Localization.Strings.General.Item}/{Program.Localization.Strings.General.SecondUnit}";

            if (_speedUnit == SpeedUnit.ItemsPerSecond)
            {
                return speed < 10 ? string.Format("{0:F1} item/s", speed) : string.Format("{0:F0} item/s", speed);
            }
            else
            {
                string[] units = [ $"{Program.Localization.Strings.General.ByteSizeUnit}/{Program.Localization.Strings.General.SecondUnit}",
                    $"{Program.Localization.Strings.General.KBSizeUnit}/{Program.Localization.Strings.General.SecondUnit}",
                    $"{Program.Localization.Strings.General.MBSizeUnit}/{Program.Localization.Strings.General.SecondUnit}",
                    $"{Program.Localization.Strings.General.GBSizeUnit}/{Program.Localization.Strings.General.SecondUnit}",
                    $"{Program.Localization.Strings.General.TBSizeUnit}/{Program.Localization.Strings.General.SecondUnit}" ];
                int unit = 0;
                double displaySpeed = speed;
                while (displaySpeed >= 1024 && unit < units.Length - 1) { displaySpeed /= 1024; unit++; }
                return displaySpeed < 10
                    ? string.Format("{0:F1} {1}", displaySpeed, units[unit])
                    : string.Format("{0:F0} {1}", displaySpeed, units[unit]);
            }
        }

        #endregion

        #region Private Methods — Scale Animation Timer

        /// <summary>
        /// Starts a dedicated 60fps timer that lerps _displayMaxSpeed toward _targetMaxSpeed.
        /// Stops itself once the scale has converged.
        /// </summary>
        private void EnsureScaleAnimTimerRunning()
        {
            if (_disposed || DesignMode) return;
            if (_scaleAnimTimer != null && _scaleAnimTimer.Enabled) return;

            if (_scaleAnimTimer == null)
            {
                _scaleAnimTimer = new Timer { Interval = SCALE_ANIM_INTERVAL };
                _scaleAnimTimer.Tick += ScaleAnimTimer_Tick;
            }

            _scaleAnimTimer.Start();
        }

        private void ScaleAnimTimer_Tick(object sender, EventArgs e)
        {
            if (_disposed) { StopScaleAnimTimer(); return; }

            double target, display;
            lock (_lock) { target = _targetMaxSpeed; display = _displayMaxSpeed; }

            // Target is sentinel — no data, nothing to do
            if (target <= 0) { StopScaleAnimTimer(); return; }

            double delta = target - display;
            double absDelta = delta < 0 ? -delta : delta;

            // Stop when close enough (within 0.5%)
            if (absDelta < target * 0.005)
            {
                lock (_lock) { _displayMaxSpeed = target; }
                StopScaleAnimTimer();
                Invalidate();
                return;
            }

            double newDisplay;

            if (delta > 0)
            {
                // Scale expanding (new peak) — snap immediately so graph never clips above top
                newDisplay = target;
            }
            else
            {
                // Scale shrinking (speed dropped) — lerp smoothly downward
                newDisplay = display + delta * SCALE_LERP_DOWN;
            }

            newDisplay = Math.Max(0.1, newDisplay);
            lock (_lock) { _displayMaxSpeed = newDisplay; }
            Invalidate();
        }

        private void StopScaleAnimTimer()
        {
            if (_scaleAnimTimer != null)
            {
                _scaleAnimTimer.Stop();
            }
        }

        #endregion

        #region Private Methods — X Animation

        private void StartXAnimation(double fromProgress, double toProgress)
        {
            StopXAnimation();
            _animFromProgress = fromProgress;
            _animToProgress = toProgress;
            _xAnimProgress = 0;

            _xAnimTimer = new Timer { Interval = 16 };
            _xAnimTimer.Tick += (s, ev) =>
            {
                _xAnimProgress += 16f / X_ANIM_DURATION;
                if (_xAnimProgress >= 1f)
                {
                    _xAnimProgress = 1f;
                    StopXAnimation();
                }
                Invalidate();
            };
            _xAnimTimer.Start();
        }

        private void StopXAnimation()
        {
            if (_xAnimTimer != null)
            {
                _xAnimTimer.Stop();
                _xAnimTimer.Dispose();
                _xAnimTimer = null;
            }
            _xAnimProgress = 1f;
        }

        private static float EaseInOut(float t)
        {
            if (t < 0.5f) return 4 * t * t * t;
            float f = -2 * t + 2;
            return 1 - (f * f * f) / 2;
        }

        #endregion

        #region Private Methods — Reset Animation

        private void StartResetAnimation()
        {
            if (_disposed || DesignMode) return;
            StopResetAnimation();
            _isAnimatingReset = true;
            _resetAnimationProgress = 0;
            _resetAnimationTimer = new Timer { Interval = 16 };
            _resetAnimationTimer.Tick += (s, e) =>
            {
                _resetAnimationProgress += 16f / RESET_ANIMATION_DURATION;

                if (_resetAnimationProgress >= 1f)
                {
                    _resetAnimationProgress = 1f;
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

        #region Private Methods — UI

        private void InitializeTaskbar()
        {
            taskbarList?.HrInit();
        }

        private void DisableOtherTaskbarBroadcasts()
        {
            var form = FindForm();
            if (form == null) return;
            foreach (ProgressBar pb in form.GetAllControls().OfType<ProgressBar>())
                pb.TaskbarBroadcast = false;
            foreach (ProgressGraph pg in form.GetAllControls().OfType<ProgressGraph>())
                if (pg != this) pg.TaskbarBroadcast = false;
        }

        private void UpdateStateColor()
        {
            Color color = _state == ProgressBarState.Normal ? Program.Style.Schemes.Main.Colors.AccentAlt
                        : _state == ProgressBarState.Error ? Program.Style.Schemes.Secondary.Colors.AccentAlt
                        : _state == ProgressBarState.Pause ? Program.Style.Schemes.Tertiary.Colors.AccentAlt
                        : Program.Style.Schemes.Main.Colors.AccentAlt;

            if (CanAnimate)
                Transition.With(this, nameof(StateColor), color)
                    .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
            else
                StateColor = color;
        }

        private void UpdateRegion()
        {
            if (IsDisposed) return;
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            Region?.Dispose();
            if (DesignMode) { Region = new Region(rect); return; }
            _corners = this.UndockedCorners();
            using (GraphicsPath path = rect.Round(corners: _corners))
                Region = new Region(path);
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
                        SetProgressValue((int)(_points[_points.Count - 1].Progress * 100));
                }
            }
        }

        private void SetProgressStateBasedOnState()
        {
            if (State == ProgressBarState.Error) SetProgressState(TaskbarProgressBarState.Error);
            else if (State == ProgressBarState.Pause) SetProgressState(TaskbarProgressBarState.Paused);
            else if (State == ProgressBarState.Normal) SetProgressState(TaskbarProgressBarState.Normal);
            else SetProgressState(TaskbarProgressBarState.NoProgress);
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

        #region Timer Management — Marquee

        private void EnsureMarqueeTimerInitialized()
        {
            if (_marqueeTimer != null || _disposed || DesignMode) return;
            int interval = Math.Max(1, 2000 / Math.Max(1, _marqueeAnimationSpeed));
            _marqueeTimer = new Timer { Interval = interval };
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
            if (_marqueeTimer != null && !_marqueeTimer.Enabled) _marqueeTimer.Start();
        }

        private void StopMarqueeTimer()
        {
            _marqueeTimer?.Stop();
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
            using (Pen solidPen = new Pen(gridColor) { DashStyle = DashStyle.Solid })
            {
                int top = rect.Top, bottom = rect.Bottom, height = rect.Height;
                for (int i = 0; i <= 5; i++)
                {
                    float y = top + (height * i / 5f);
                    g.DrawLine(solidPen, rect.Left, y, rect.Right, y);
                }
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
            float marqueeLeft = -marqueeWidth + (rect.Width + marqueeWidth) * _marqueeOffset;
            RectangleF marqueeRect = new RectangleF(marqueeLeft, rect.Top, marqueeWidth, rect.Height);

            g.SetClip(rect);
            using (LinearGradientBrush brush = new LinearGradientBrush(
                marqueeRect, Color.Transparent, Color.Transparent, LinearGradientMode.Horizontal))
            {
                ColorBlend cb = new ColorBlend
                {
                    Colors = new[] { Color.Transparent, Color.FromArgb(80, graphColor), Color.Transparent },
                    Positions = new[] { 0f, 0.5f, 1f }
                };
                brush.InterpolationColors = cb;
                g.FillRectangle(brush, marqueeRect);
            }
            g.ResetClip();
        }

        /// <summary>
        /// Computes the Y pixel position for a given speed value using the current _displayMaxSpeed.
        /// graphBottom and graphHeight must match what DrawGraph uses.
        /// </summary>
        private float SpeedToY(double speed, int graphBottom, int graphHeight)
        {
            double maxSpeed = _displayMaxSpeed > 0 ? _displayMaxSpeed : 0.1;
            double ratio = speed / maxSpeed;
            if (ratio < 0) ratio = 0;
            if (ratio > 1) ratio = 1;
            return graphBottom - (float)(graphHeight * ratio);
        }

        private void DrawCurrentSpeedLine(Graphics g, Rectangle rect)
        {
            if (_state == ProgressBarState.Pause || _state == ProgressBarState.Error || Style == ProgressBarStyle.Marquee) return;

            lock (_lock)
            {
                if (_points.Count == 0) return;
                if (_displayMaxSpeed <= 0) return;

                int graphTop = rect.Top;
                int graphBottom = rect.Bottom - 1;  // must match DrawGraph
                int graphHeight = graphBottom - graphTop;

                // Use the SAME _displayMaxSpeed that DrawGraph used this frame
                float y = SpeedToY(_currentSpeed, graphBottom, graphHeight);
                y = Math.Max(graphTop, Math.Min(y, graphBottom));

                float resetProg = Math.Max(0f, Math.Min(1f, _resetAnimationProgress));
                int alpha = _isAnimatingReset ? (int)(255 * (1f - resetProg)) : 255;
                alpha = Math.Max(0, Math.Min(255, alpha));

                using (Pen pen = new(Color.FromArgb(alpha, ForeColor))) g.DrawLine(pen, rect.Left, y, rect.Right, y);

                SizeF textSize = g.MeasureString(_speedText, Fonts.Console);
                float textX = rect.Right - textSize.Width - 1;
                float textY = y - textSize.Height - 1;
                textX = Math.Max(rect.Left + 2, Math.Min(rect.Right - textSize.Width - 2, textX));
                textY = Math.Max(rect.Top + 2, Math.Min(rect.Bottom - textSize.Height - 2, textY));

                using (SolidBrush brush = new(Color.FromArgb(alpha, ForeColor))) g.DrawString(_speedText, Fonts.Console, brush, textX, textY);
            }
        }

        private void DrawGraph(Graphics g, Rectangle rect, Color graphColor)
        {
            lock (_lock)
            {
                if (Style == ProgressBarStyle.Marquee) { DrawMarquee(g, rect, graphColor); return; }
                if (_displayMaxSpeed <= 0 || _points.Count < 1) return;

                int graphTop = rect.Top;
                int graphBottom = rect.Bottom - 1;
                int graphHeight = graphBottom - graphTop;

                int n = _points.Count;
                PointF[] pts = new PointF[n];

                for (int i = 0; i < n; i++)
                {
                    double prog = _points[i].Progress;

                    if (i == n - 1 && _xAnimTimer != null)
                    {
                        float t = EaseInOut(_xAnimProgress);
                        prog = _animFromProgress + (_animToProgress - _animFromProgress) * t;
                        prog = Math.Max(_animFromProgress, Math.Min(_animToProgress, prog));
                    }

                    float x = rect.Left + (float)(rect.Width * Math.Min(1.0, prog));
                    float y = SpeedToY(_points[i].Speed, graphBottom, graphHeight);
                    y = Math.Max(graphTop, Math.Min(y, graphBottom));
                    pts[i] = new PointF(x, y);
                }

                // Curve ends at current progress (lastX); build stable curve so animation doesn't twist
                float lastX = pts[n - 1].X;
                float lastY = pts[n - 1].Y;

                using (GraphicsPath linePath = new GraphicsPath())
                {
                    if (n == 1)
                    {
                        linePath.AddLine(rect.Left, pts[0].Y, pts[0].X, pts[0].Y);
                    }
                    else
                    {
                        // Curve only through points 0..n-2 so the spline doesn't twist when last point animates
                        PointF[] curvePts = new PointF[n];
                        curvePts[0] = new PointF(rect.Left, pts[0].Y);
                        for (int i = 0; i < n - 1; i++)
                            curvePts[i + 1] = pts[i];
                        linePath.AddCurve(curvePts, 0.5f);
                        linePath.AddLine(pts[n - 2].X, pts[n - 2].Y, lastX, lastY);
                    }

                    if (linePath.PointCount == 0) return;

                    // Fill below graph (left to current progress)
                    using (GraphicsPath fillPath = new())
                    {
                        fillPath.AddPath(linePath, false);
                        fillPath.AddLine(lastX, lastY, lastX, graphBottom);
                        fillPath.AddLine(lastX, graphBottom, rect.Left, graphBottom);
                        fillPath.AddLine(rect.Left, graphBottom, rect.Left, pts[0].Y);
                        fillPath.CloseFigure();

                        int fillWidth = Math.Max(1, (int)(lastX - rect.Left));
                        using (SolidBrush brush = new(graphColor))
                        {
                            g.FillPath(brush, fillPath);
                        }
                    }

                    // Fill above graph (semi-transparent)
                    using (GraphicsPath fillPathAbove = new GraphicsPath())
                    {
                        fillPathAbove.AddPath(linePath, false);
                        fillPathAbove.AddLine(lastX, lastY, lastX, graphTop);
                        fillPathAbove.AddLine(lastX, graphTop, rect.Left, graphTop);
                        fillPathAbove.AddLine(rect.Left, graphTop, rect.Left, pts[0].Y);
                        fillPathAbove.CloseFigure();
                        using (SolidBrush b = new SolidBrush(Color.FromArgb(80, graphColor)))
                            g.FillPath(b, fillPathAbove);
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

        protected override void OnResize(EventArgs e) { base.OnResize(e); UpdateRegion(); }
        protected override void OnDockChanged(EventArgs e) { base.OnDockChanged(e); UpdateRegion(); }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (DesignMode) return;
            if (Visible)
            {
                ThrottleTaskbarUpdate();
                if (Style == ProgressBarStyle.Marquee && CanAnimate) StartMarqueeTimer();
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
                    StopXAnimation();
                    StopScaleAnimTimer();
                    _scaleAnimTimer?.Dispose();
                    _scaleAnimTimer = null;

                    if (_formHwnd != IntPtr.Zero && taskbarList != null)
                    {
                        try { taskbarList.SetProgressState(_formHwnd, TaskbarProgressBarState.NoProgress); }
                        catch { }
                    }
                }
                _disposed = true;
            }
            base.Dispose(disposing);
        }

        protected virtual new void OnStyleChanged(EventArgs e)
        {
            ThrottleTaskbarUpdate();
            if (Style == ProgressBarStyle.Marquee) StartMarqueeTimer();
            else StopMarqueeTimer();
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
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode
                ? TextRenderingHint.ClearTypeGridFit
                : Program.Style.TextRenderingHint;

            var scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            Color backColor = scheme.Colors.Back(_parentLevel);
            Color lineColor = scheme.Colors.Line_Hover(_parentLevel);
            Color gridColor = scheme.Colors.Line_Hover(_parentLevel);

            using (SolidBrush brush = new SolidBrush(backColor))
                G.FillRoundedRect(brush, rect);

            GraphicsPath clipPath = null;
            if (Program.Style.RoundedCorners)
            {
                clipPath = rect.Round(corners: _corners);
                G.SetClip(clipPath);
            }

            DrawGrid(G, rect, gridColor);
            DrawGraph(G, rect, _stateColor);

            G.ResetClip();
            clipPath?.Dispose();

            using (Pen pen = new Pen(lineColor))
                G.DrawRoundedRectBeveledReverse(pen, rect);

            DrawCurrentSpeedLine(G, rect);
        }

        #endregion

        #region Events

        public new event EventHandler StyleChanged;

        #endregion
    }
}