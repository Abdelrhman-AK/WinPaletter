using FluentTransitions;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.Interfaces;
using WinPaletter.Properties;

namespace WinPaletter.UI.WP
{
    [Description("Themed ProgressBar for WinPaletter UI")]
    public class ProgressBar : System.Windows.Forms.ProgressBar
    {
        public ProgressBar()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            taskbarList?.HrInit();
            StyleChanged += ProgressBar_StyleChanged;

            // Initialize animation engine
            _animationEngine = new AnimationEngine();
            _animationEngine.FrameUpdate += OnAnimationFrameUpdate;
        }

        #region Variables
        private bool CanAnimate => !DesignMode && Program.Style.Animations && this != null && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        readonly ITaskbarList3 taskbarList = !OS.WXP && !OS.WVista ? (ITaskbarList3)new CTaskbarList() : null;
        private IntPtr FormHwnd = IntPtr.Zero;
        private static readonly TextureBrush Noise = new(Resources.Noise);
        private DateTime _lastTaskbarUpdate = DateTime.MinValue;
        private bool _intendedVisible = true;

        // Animation engine - runs on separate thread
        private readonly AnimationEngine _animationEngine;

        // Animation fields (values updated by animation engine)
        private float _marqueeOffset = 0;
        private float _hoverOffset = 0;
        private int _alpha = 255;
        private bool _disposed = false;

        // Property fields
        private bool _highlightEffectEnabled = true;
        private int _highlightAnimationSpeed = 300;
        #endregion

        #region Animation Engine (Thread-Safe)
        private class AnimationEngine : IDisposable
        {
            private readonly Thread _animationThread;
            private volatile bool _isRunning;
            private volatile bool _disposed;
            private readonly object _lock = new();
            private readonly ManualResetEvent _stopEvent = new(false);
            private int _targetFPS = 60;
            private int _marqueeSpeed = 100;
            private int _highlightSpeed = 300;
            private bool _marqueeActive = false;
            private bool _highlightActive = false;

            // Animation values
            public float MarqueeOffset { get; private set; }
            public float HoverOffset { get; private set; }

            public event EventHandler FrameUpdate;

            public AnimationEngine()
            {
                _isRunning = true;
                _animationThread = new Thread(AnimationLoop)
                {
                    IsBackground = true,
                    Priority = ThreadPriority.AboveNormal // Higher priority for smooth animations
                };
                _animationThread.Start();
            }

            private void AnimationLoop()
            {
                while (_isRunning)
                {
                    try
                    {
                        int interval = 1000 / _targetFPS;
                        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

                        // Update animation values
                        UpdateAnimations();

                        // Trigger UI update on UI thread
                        FrameUpdate?.BeginInvoke(null, EventArgs.Empty, null, null);

                        // Wait for next frame
                        int elapsed = (int)stopwatch.ElapsedMilliseconds;
                        int sleepTime = Math.Max(1, interval - elapsed);

                        if (_stopEvent.WaitOne(sleepTime))
                            break;

                        // If we're running behind, skip the wait
                        if (elapsed > interval)
                            Thread.Yield();
                    }
                    catch (ThreadAbortException)
                    {
                        break;
                    }
                    catch
                    {
                        // Ignore exceptions in animation loop
                        Thread.Sleep(16);
                    }
                }
            }

            private void UpdateAnimations()
            {
                lock (_lock)
                {
                    // Update marquee animation
                    if (_marqueeActive)
                    {
                        float marqueeIncrement = 0.02f * (_marqueeSpeed / 100f);
                        MarqueeOffset += marqueeIncrement;
                        if (MarqueeOffset > 1) MarqueeOffset = 0;
                    }
                    else
                    {
                        MarqueeOffset = 0;
                    }

                    // Update hover animation
                    if (_highlightActive)
                    {
                        float hoverIncrement = 0.01f * (_highlightSpeed / 300f);
                        HoverOffset += hoverIncrement;
                        if (HoverOffset > 1) HoverOffset = 0;
                    }
                    else
                    {
                        HoverOffset = 0;
                    }
                }
            }

            public void SetMarqueeActive(bool active)
            {
                lock (_lock)
                {
                    _marqueeActive = active;
                    if (!active) MarqueeOffset = 0;
                }
            }

            public void SetHighlightActive(bool active)
            {
                lock (_lock)
                {
                    _highlightActive = active;
                    if (!active) HoverOffset = 0;
                }
            }

            public void SetMarqueeSpeed(int speed)
            {
                lock (_lock)
                {
                    _marqueeSpeed = Math.Max(1, speed);
                }
            }

            public void SetHighlightSpeed(int speed)
            {
                lock (_lock)
                {
                    _highlightSpeed = Math.Max(1, speed);
                }
            }

            public void SetFPS(int fps)
            {
                lock (_lock)
                {
                    _targetFPS = Math.Max(30, Math.Min(120, fps));
                }
            }

            public void Dispose()
            {
                if (_disposed) return;
                _disposed = true;
                _isRunning = false;
                _stopEvent.Set();
                _animationThread?.Join(500);
                _stopEvent?.Dispose();
            }
        }

        private void OnAnimationFrameUpdate(object sender, EventArgs e)
        {
            if (_disposed || !IsHandleCreated) return;

            // Update animation values from engine
            _marqueeOffset = _animationEngine.MarqueeOffset;
            _hoverOffset = _animationEngine.HoverOffset;

            // Invalidate UI if needed
            if (Style == ProgressBarStyle.Marquee || _highlightEffectEnabled)
            {
                Invalidate();
            }
        }
        #endregion

        #region Events/Overrides
        public new event EventHandler StyleChanged;
        public event EventHandler ValueChanged;

        protected override void OnHandleCreated(EventArgs e)
        {
            if (!DesignMode)
            {
                if (FindForm() != null)
                {
                    System.Windows.Forms.Form form = FindForm();

                    if (form.Parent == null)
                    {
                        FormHwnd = form.Handle;
                    }
                    else if (form.Parent is TabPage tabPage && tabPage.Parent != null)
                    {
                        form = tabPage.Parent.FindForm();
                        if (form != null) FormHwnd = form.Handle; else FormHwnd = Forms.MainForm.Handle;
                    }
                    else
                    {
                        FormHwnd = Forms.MainForm.Handle;
                    }
                }

                // Update animation engine state
                UpdateAnimationState();
            }

            base.OnHandleCreated(e);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            SetProgressState(TaskbarProgressBarState.NoProgress);
            SetProgressValue(0);

            // Stop animations
            _animationEngine.SetMarqueeActive(false);
            _animationEngine.SetHighlightActive(false);

            base.OnHandleDestroyed(e);
        }

        protected virtual new async void OnStyleChanged(EventArgs e)
        {
            if (!DesignMode)
            {
                if (Style == ProgressBarStyle.Marquee)
                {
                    _animationEngine.SetMarqueeActive(true);
                }
                else
                {
                    _animationEngine.SetMarqueeActive(false);
                    if (CanAnimate)
                        Transition.With(this, nameof(Value_Animation), _value).CriticalDamp(Program.AnimationSpan);
                    else
                        Value_Animation = _value;
                }

                // Update highlight state
                UpdateHighlightState();
            }

            await Task.Delay(10);
            Invalidate();
            StyleChanged?.Invoke(this, e);
            base.OnStyleChanged(e);
        }

        protected virtual void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
            UpdateHighlightState();
        }

        private void UpdateHighlightState()
        {
            if (!DesignMode && Style != ProgressBarStyle.Marquee)
            {
                bool shouldRun = _highlightEffectEnabled && _value > Minimum && _value < Maximum;
                _animationEngine.SetHighlightActive(shouldRun);
            }
            else
            {
                _animationEngine.SetHighlightActive(false);
            }
        }

        private void UpdateAnimationState()
        {
            if (!DesignMode)
            {
                bool isMarquee = Style == ProgressBarStyle.Marquee;
                _animationEngine.SetMarqueeActive(isMarquee);

                bool highlightShouldRun = _highlightEffectEnabled && !isMarquee && _value > Minimum && _value < Maximum;
                _animationEngine.SetHighlightActive(highlightShouldRun);

                // Set FPS based on animation speed
                int fps = Math.Max(30, Math.Min(60, 60000 / Math.Max(1, _highlightAnimationSpeed)));
                _animationEngine.SetFPS(fps);
            }
        }

        private void ProgressBar_StyleChanged(object sender, EventArgs e)
        {
            ThrottleTaskbarUpdate();
            Invalidate();
        }

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _animationEngine?.Dispose();
                }
                _disposed = true;
            }
            base.Dispose(disposing);
        }

        int parentLevel = 0;
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            parentLevel = this.Level();
        }

        #endregion

        #region Properties

        public new bool Visible
        {
            get => base.Visible;
            set
            {
                _intendedVisible = value;
                base.Visible = value;
                ApplyVisibilityState(value);
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (DesignMode) return;
            ApplyVisibilityState(_intendedVisible && base.Visible);
        }

        private void ApplyVisibilityState(bool isVisible)
        {
            if (DesignMode) return;

            if (isVisible)
            {
                _lastTaskbarUpdate = DateTime.MinValue;
                UpdateTaskbar();
                UpdateAnimationState();
            }
            else
            {
                SetProgressState(TaskbarProgressBarState.NoProgress);
                SetProgressValue(0);
                _animationEngine.SetMarqueeActive(false);
                _animationEngine.SetHighlightActive(false);
                _lastTaskbarUpdate = DateTime.MinValue;
            }
        }

        private int _value = 0;
        public new int Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    if (value < Minimum) { _value = Minimum; }
                    else if (value > Maximum) { _value = Maximum; }
                    else { _value = value; }

                    if (CanAnimate && Style != ProgressBarStyle.Marquee)
                    {
                        Transition.With(this, nameof(Value_Animation), _value).CriticalDamp(Program.AnimationSpan);
                    }
                    else { Value_Animation = _value; }

                    ThrottleTaskbarUpdate();
                    OnValueChanged(new EventArgs());
                }
            }
        }

        /// <summary>
        /// Enables or disables the moving highlight effect on the progress bar
        /// </summary>
        [Category("Behavior")]
        [Description("Enables or disables the moving highlight effect on the progress bar")]
        [DefaultValue(true)]
        public bool HighlightEffectEnabled
        {
            get => _highlightEffectEnabled;
            set
            {
                if (_highlightEffectEnabled != value)
                {
                    _highlightEffectEnabled = value;
                    UpdateHighlightState();
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the speed of the highlight animation.
        /// Higher values = faster animation, Lower values = slower animation
        /// Default is 300 which gives a moderate speed
        /// </summary>
        [Category("Behavior")]
        [Description("The speed of the highlight animation. Higher values = faster animation, Lower values = slower animation")]
        [DefaultValue(300)]
        public int HighlightAnimationSpeed
        {
            get => _highlightAnimationSpeed;
            set
            {
                if (_highlightAnimationSpeed != value && value > 0)
                {
                    _highlightAnimationSpeed = value;
                    _animationEngine.SetHighlightSpeed(value);

                    // Update FPS based on animation speed
                    int fps = Math.Max(30, Math.Min(60, 60000 / Math.Max(1, value)));
                    _animationEngine.SetFPS(fps);
                }
            }
        }

        private ProgressBarState _state = ProgressBarState.Normal;
        public ProgressBarState State
        {
            get => _state;
            set
            {
                if (value != _state)
                {
                    _state = value;

                    Color color;

                    switch (value)
                    {
                        case ProgressBarState.Normal:
                            {
                                color = Program.Style.Schemes.Tertiary.Colors.Line_Checked_Hover;
                                break;
                            }

                        case ProgressBarState.Error:
                            {
                                color = Program.Style.Schemes.Secondary.Colors.AccentAlt;
                                break;
                            }

                        case ProgressBarState.Pause:
                            {
                                color = Program.Style.Schemes.Main.Colors.AccentAlt;
                                break;
                            }

                        default:
                            {
                                color = Program.Style.Schemes.Tertiary.Colors.Line_Checked_Hover;
                                break;
                            }
                    }

                    if (CanAnimate) { Transition.With(this, nameof(StateColor), color).CriticalDamp(Program.AnimationSpan_Quick); }
                    else { StateColor = color; }

                    ThrottleTaskbarUpdate();
                    Invalidate();
                }
            }
        }

        private ProgressBarAppearance _appearance = ProgressBarAppearance.Bar;
        public ProgressBarAppearance Appearance
        {
            get => _appearance;
            set
            {
                if (value != _appearance)
                {
                    _appearance = value;
                    Invalidate();
                }
            }
        }

        private double Percentage => _animatedValue / (double)(Maximum - Minimum);
        private double Percentage_Actual => _value / (double)(Maximum - Minimum);

        private bool _TaskbarBroadcast = true;
        public bool TaskbarBroadcast
        {
            get => _TaskbarBroadcast;
            set
            {
                if (value != TaskbarBroadcast)
                {
                    _TaskbarBroadcast = value;

                    if (Parent != null && value == true)
                    {
                        foreach (ProgressBar PB in FindForm().GetAllControls().OfType<ProgressBar>()) { if (PB != this) PB.TaskbarBroadcast = false; }
                    }

                    ThrottleTaskbarUpdate();
                }
            }
        }

        private ProgressBarStyle _style;
        public new ProgressBarStyle Style
        {
            get => _style;
            set
            {
                if (_style != value)
                {
                    _style = value;
                    OnStyleChanged(EventArgs.Empty);
                }
            }
        }

        #region Properties for animation purposes only

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public int Value_Animation
        {
            get => _animatedValue;
            set
            {
                if (value != _animatedValue)
                {
                    _animatedValue = value;
                    Invalidate();
                }
            }
        }

        private int _animatedValue = 0;

        private Color _stateColor = Program.Style.Schemes.Tertiary.Colors.Line_Checked_Hover;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Color StateColor
        {
            get => _stateColor;
            set
            {
                if (value != _stateColor)
                {
                    _stateColor = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Alpha transparency for progress bar rendering (copied from Breadcrumb)
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public int Alpha
        {
            get => _alpha;
            set
            {
                if (value != _alpha)
                {
                    _alpha = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Indicates if marquee animation is active (based on Style)
        /// </summary>
        public bool IsMarquee => Style == ProgressBarStyle.Marquee && _animationEngine != null;

        /// <summary>
        /// Gets or sets the speed of the marquee animation.
        /// Higher values = faster animation, Lower values = slower animation.
        /// Default is 100.
        /// </summary>
        [Category("Behavior")]
        [Description("The speed of the marquee animation. Higher values = faster animation, Lower values = slower animation")]
        [DefaultValue(100)]
        public new int MarqueeAnimationSpeed
        {
            get => _marqueeAnimationSpeed;
            set
            {
                if (_marqueeAnimationSpeed != value && value > 0)
                {
                    _marqueeAnimationSpeed = value;
                    _animationEngine.SetMarqueeSpeed(value);
                }
            }
        }

        private int _marqueeAnimationSpeed = 100;

        #endregion

        #endregion

        #region Methods

        public new void PerformStep()
        {
            Value += Step;
        }

        public void PerformStep(int step)
        {
            Value += step;
        }

        private void ThrottleTaskbarUpdate()
        {
            if (DesignMode || FormHwnd == IntPtr.Zero)
                return;

            if ((DateTime.Now - _lastTaskbarUpdate).TotalMilliseconds >= 200)
            {
                _lastTaskbarUpdate = DateTime.Now;
                UpdateTaskbar();
            }
        }

        private void UpdateTaskbar()
        {
            if (DesignMode || FormHwnd == IntPtr.Zero) return;

            if (!Visible)
            {
                SetProgressState(TaskbarProgressBarState.NoProgress);
                return;
            }

            if (_TaskbarBroadcast)
            {
                if (Style == ProgressBarStyle.Marquee)
                {
                    SetProgressState(TaskbarProgressBarState.Indeterminate);
                }
                else
                {
                    if (State == ProgressBarState.Error)
                    {
                        SetProgressState(TaskbarProgressBarState.Error);
                    }
                    else if (State == ProgressBarState.Pause)
                    {
                        SetProgressState(TaskbarProgressBarState.Paused);
                    }
                    else if (State == ProgressBarState.Normal)
                    {
                        SetProgressState(TaskbarProgressBarState.Normal);
                    }
                    else
                    {
                        SetProgressState(TaskbarProgressBarState.NoProgress);
                    }

                    SetProgressValue((int)(Percentage_Actual * 100));
                }
            }
        }

        /// <summary>
        /// Sets the progress state of the taskbar.
        /// </summary>
        /// <param name="state">The progress state to set.</param>
        private void SetProgressState(TaskbarProgressBarState state)
        {
            if (DesignMode || FormHwnd == IntPtr.Zero) return;
            taskbarList?.SetProgressState(FormHwnd, state);
        }

        /// <summary>
        /// Sets the progress value of the taskbar, but in percentage.
        /// </summary>
        /// <param name="value">The progress value to set (0 to 100).</param>
        private void SetProgressValue(int value)
        {
            if (DesignMode || FormHwnd == IntPtr.Zero) return;

            int _val = value;

            if (State == ProgressBarState.Normal && (value == 0 || value == 100))
            {
                SetProgressState(TaskbarProgressBarState.NoProgress);
                _val = 0;
            }

            taskbarList?.SetProgressValue(FormHwnd, (ulong)_val, 100);
        }

        #endregion

        #region Enumerations
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
        /// Progress bar style
        /// </summary>
        public enum ProgressBarAppearance
        {
            ///
            Bar,

            ///
            Circle
        }

        /// <summary>
        /// Enumerates the possible states for the progress bar in the taskbar button.
        /// </summary>
        public enum TaskbarProgressBarState
        {
            /// <summary>
            /// No progress is displayed in the taskbar button.
            /// </summary>
            NoProgress = 0,

            /// <summary>
            /// The progress is indeterminate (e.g., a loading spinner) in the taskbar button.
            /// </summary>
            Indeterminate = 0x1,

            /// <summary>
            /// Normal progress is displayed in the taskbar button.
            /// </summary>
            Normal = 0x2,

            /// <summary>
            /// An error state is indicated in the taskbar button.
            /// </summary>
            Error = 0x4,

            /// <summary>
            /// The progress is paused in the taskbar button.
            /// </summary>
            Paused = 0x8
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
        #endregion

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //Leave it empty to make control background transparent
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_disposed) return;

            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            Rectangle rect = new(0, 0, Width - 1, Height - 1);

            if (Appearance == ProgressBarAppearance.Bar)
            {
                double _percent = Style == ProgressBarStyle.Continuous ? Percentage : 0.5d;
                double _startX = Style == ProgressBarStyle.Continuous ? 0 : Width * Percentage - 0.25d * Width;

                RectangleF rectValue = new((float)_startX, 0, rect.Width * (float)_percent, Height - 1);

                int radius = Height / 3;

                if (Program.Style.RoundedCorners && Dock == DockStyle.None) G.SetClip(rect.Round(radius));

                using (LinearGradientBrush br = new(rect, scheme.Colors.Back(parentLevel), scheme.Colors.Back_Hover(parentLevel), LinearGradientMode.ForwardDiagonal))
                {
                    if (Dock == DockStyle.None) G.FillRoundedRect(br, rect, radius);
                    else G.FillRectangle(br, rect);
                }

                // Marquee animation (controlled by Style property)
                if (Style == ProgressBarStyle.Marquee)
                {
                    float segmentWidth = rect.Width * 0.25f;
                    float offsetX = rect.Width * _marqueeOffset;

                    RectangleF marqueeRect = new(offsetX, 0, segmentWidth, rect.Height);

                    using (LinearGradientBrush brush = new(marqueeRect, Color.Transparent, Color.Transparent, LinearGradientMode.Horizontal))
                    {
                        ColorBlend cb = new()
                        {
                            Colors = [Color.Transparent, Color.FromArgb(_alpha, Program.Style.DarkMode ? _stateColor.Light(0.1f) : _stateColor.Dark(0.1f)), Color.Transparent],
                            Positions = [0f, 0.5f, 1f]
                        };
                        brush.InterpolationColors = cb;

                        if (offsetX + segmentWidth > rect.Width)
                        {
                            float rightWidth = rect.Width - offsetX;
                            RectangleF rightRect = new(offsetX, 0, rightWidth, rect.Height);
                            if (Dock == DockStyle.None) G.FillRoundedRect(brush, rightRect, radius);
                            else G.FillRectangle(brush, rightRect);

                            float leftWidth = segmentWidth - rightWidth;
                            RectangleF leftRect = new(0, 0, leftWidth, rect.Height);
                            using (LinearGradientBrush leftBrush = new(leftRect, Color.Transparent, Color.Transparent, LinearGradientMode.Horizontal))
                            {
                                leftBrush.InterpolationColors = cb;
                                if (Dock == DockStyle.None) G.FillRoundedRect(leftBrush, leftRect, radius);
                                else G.FillRectangle(leftBrush, leftRect);
                            }
                        }
                        else
                        {
                            if (Dock == DockStyle.None) G.FillRoundedRect(brush, marqueeRect, radius);
                            else G.FillRectangle(brush, marqueeRect);
                        }
                    }
                }
                else
                {
                    // Normal progress bar with hover highlight effect (copied from Breadcrumb)
                    using (LinearGradientBrush br = new(rect, Program.Style.DarkMode ? _stateColor.Dark(0.05f) : _stateColor.Light(), _stateColor, (float)Percentage * 360f, true))
                    {
                        if (Dock == DockStyle.None) G.FillRoundedRect(br, rectValue, radius);
                        else G.FillRectangle(br, rectValue);
                    }

                    if (Dock == DockStyle.None) G.FillRoundedRect(Noise, rectValue, radius);
                    else G.FillRectangle(Noise, rectValue);

                    // Add moving highlight effect (controlled by HighlightEffectEnabled)
                    if (_highlightEffectEnabled && _value > Minimum && _value < Maximum)
                    {
                        // Fixed width highlight
                        float highlightWidth = Math.Max(1, rectValue.Width * 0.3f);

                        // Calculate position based on hover offset (0 to 1)
                        // This makes the highlight move from left edge to right edge of the progress portion
                        float highlightLeft = (_hoverOffset * (rectValue.Width + highlightWidth)) - highlightWidth;

                        // Clamp to ensure highlight stays within progress bounds
                        highlightLeft = Math.Max(rectValue.Left - highlightWidth, Math.Min(highlightLeft, rectValue.Right));

                        RectangleF highlightRect = new(highlightLeft, rectValue.Top, highlightWidth, rectValue.Height);

                        // Calculate alpha based on how much of the highlight is within the progress area
                        float intersectLeft = Math.Max(highlightRect.Left, rectValue.Left);
                        float intersectRight = Math.Min(highlightRect.Right, rectValue.Right);
                        float intersectWidth = Math.Max(0, intersectRight - intersectLeft);

                        // Alpha is proportional to the visible portion
                        float visibleRatio = intersectWidth / highlightWidth;

                        // Create a region for clipping to the progress area
                        GraphicsPath clipPath;
                        if (Program.Style.RoundedCorners)
                        {
                            clipPath = rectValue.Round(radius);
                        }
                        else
                        {
                            clipPath = new();
                            clipPath.AddRectangle(rectValue);
                        }

                        if (Dock == DockStyle.None && Program.Style.RoundedCorners)
                        {
                            G.SetClip(clipPath);
                        }
                        else
                        {
                            G.SetClip(rectValue);
                        }

                        // Draw the highlight with alpha based on visibility
                        Color hilightColor = Program.Style.DarkMode ? _stateColor.Light() : _stateColor.Dark();
                        using (LinearGradientBrush brush = new(highlightRect, Color.Transparent, hilightColor, LinearGradientMode.Horizontal))
                        {
                            ColorBlend cb = new()
                            {
                                Colors = [Color.FromArgb(0, hilightColor), Color.FromArgb((int)(128 * visibleRatio), hilightColor), Color.FromArgb(0, hilightColor)],
                                Positions = [0f, 0.5f, 1f]
                            };
                            brush.InterpolationColors = cb;

                            if (Dock == DockStyle.None && Program.Style.RoundedCorners)
                                G.FillRoundedRect(brush, highlightRect, radius);
                            else
                                G.FillRectangle(brush, highlightRect);
                        }

                        G.ResetClip();
                        clipPath?.Dispose();
                    }
                }

                G.ResetClip();

                using (Pen P = new(scheme.Colors.Line_Hover(parentLevel)))
                using (Pen P_Value = new(Color.FromArgb(100, Program.Style.DarkMode ? _stateColor.Light() : _stateColor.Dark())))
                {
                    if (Dock == DockStyle.None)
                    {
                        G.DrawRoundedRect(P, rect, radius);
                        if (Style != ProgressBarStyle.Marquee) G.DrawRoundedRect(P_Value, rectValue, radius);
                    }
                    else
                    {
                        G.DrawRectangle(P, rect);
                        if (Style != ProgressBarStyle.Marquee) G.DrawRectangle(P_Value, Rectangle.Round(rectValue));
                    }
                }
            }

            else if (Appearance == ProgressBarAppearance.Circle)
            {
                float PenWidth = 0.15f * Math.Max(Width, Height);
                float _percent = Style == ProgressBarStyle.Continuous ? (float)Percentage : 0.5f;
                float _startAngle = Style == ProgressBarStyle.Continuous ? -90 : -90; // Start from top for marquee

                RectangleF CircleRect = new(PenWidth, PenWidth, rect.Width - PenWidth * 2, rect.Height - PenWidth * 2 + 1);

                if (CircleRect.Width < 10) { CircleRect.Width = 10; }
                if (CircleRect.Height < 10) { CircleRect.Height = 10; }

                // Draw background circle
                using (LinearGradientBrush brush2 = new(rect, scheme.Colors.Back(parentLevel), scheme.Colors.Back_Hover(parentLevel), LinearGradientMode.BackwardDiagonal))
                using (Pen pen_background = new(brush2, PenWidth))
                {
                    pen_background.StartCap = Program.Style.RoundedCorners ? LineCap.Round : LineCap.Flat;
                    pen_background.EndCap = pen_background.StartCap;
                    G.DrawArc(pen_background, CircleRect, 0, 360);
                }

                if (Style == ProgressBarStyle.Marquee)
                {
                    // Marquee circle animation - rotating segment
                    float sweepAngle = 120; // 120 degree segment
                    float startAngle = (_marqueeOffset * 360) - 60; // Rotate around

                    using (LinearGradientBrush br = new(rect, _stateColor, Program.Style.DarkMode ? _stateColor.Light() : _stateColor.LightLight(), (float)Percentage * 360f, true))
                    using (Pen pen = new(br, PenWidth))
                    {
                        pen.StartCap = Program.Style.RoundedCorners ? LineCap.Round : LineCap.Flat;
                        pen.EndCap = pen.StartCap;
                        G.DrawArc(pen, CircleRect, startAngle, sweepAngle);
                    }

                    // Add noise effect
                    using (Pen penNoise = new(Noise, PenWidth))
                    {
                        penNoise.StartCap = Program.Style.RoundedCorners ? LineCap.Round : LineCap.Flat;
                        penNoise.EndCap = penNoise.StartCap;
                        G.DrawArc(penNoise, CircleRect, startAngle, sweepAngle);
                    }
                }
                else
                {
                    // Normal progress circle
                    using (LinearGradientBrush br = new(rect, _stateColor, Program.Style.DarkMode ? _stateColor.Light() : _stateColor.LightLight(), (float)Percentage * 360f, true))
                    using (Pen pen = new(br, PenWidth))
                    {
                        pen.StartCap = Program.Style.RoundedCorners ? LineCap.Round : LineCap.Flat;
                        pen.EndCap = pen.StartCap;
                        G.DrawArc(pen, CircleRect, -90, _percent * 360);
                    }

                    // Add noise effect
                    using (Pen penNoise = new(Noise, PenWidth))
                    {
                        penNoise.StartCap = Program.Style.RoundedCorners ? LineCap.Round : LineCap.Flat;
                        penNoise.EndCap = penNoise.StartCap;
                        G.DrawArc(penNoise, CircleRect, -90, _percent * 360);
                    }
                }

                G.ResetClip();
            }

            base.OnPaint(e);
        }
    }
}