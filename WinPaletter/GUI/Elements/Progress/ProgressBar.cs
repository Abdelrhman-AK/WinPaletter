using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.Interfaces;
using WinPaletter.NativeMethods;
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

            // Animators: each drives its own high-resolution multimedia timer, independent of
            // the UI thread's message queue backlog. Paint requests are coalesced through
            // Control.BeginInvoke so a busy UI thread never accumulates a backlog of repaints.
            _valueAnimator = new ProgressAnimator(this);
            _marqueeAnimator = new ProgressAnimator(this);
            _hoverAnimator = new ProgressAnimator(this);
            _stateColorAnimator = new ColorAnimator(this, Program.Style.Schemes.Tertiary.Colors.Line_Checked_Hover);
        }

        #region Variables
        private bool CanAnimate => !DesignMode && Program.Style.Animations && this != null && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        readonly ITaskbarList3 taskbarList = !OS.WXP && !OS.WVista ? (ITaskbarList3)new CTaskbarList() : null;
        private IntPtr FormHwnd = IntPtr.Zero;
        private static readonly TextureBrush Noise = new(Resources.Noise);
        private DateTime _lastTaskbarUpdate = DateTime.MinValue;
        private bool _intendedVisible = true;
        private bool _disposed = false;

        // Animators (replace the former Thread-based AnimationEngine and FluentTransitions calls)
        private readonly ProgressAnimator _valueAnimator;
        private readonly ProgressAnimator _marqueeAnimator;
        private readonly ProgressAnimator _hoverAnimator;
        private readonly ColorAnimator _stateColorAnimator;

        // Base loop durations at default speed values (100 for marquee, 300 for highlight), scaled by the speed properties below. Kept as constants rather than an FPS-based
        // per-tick increment since the new engine advances by elapsed wall-clock time, not ticks.
        private const double BaseMarqueeLoopMs = 2500;
        private const double BaseHoverLoopMs = 3000;

        private double MarqueeLoopDurationMs => BaseMarqueeLoopMs * (100.0 / Math.Max(1, _marqueeAnimationSpeed));
        private double HoverLoopDurationMs => BaseHoverLoopMs * (300.0 / Math.Max(1, _highlightAnimationSpeed));

        private int _alpha = 255;

        // Property fields
        private bool _highlightEffectEnabled = true;
        private int _highlightAnimationSpeed = 300;
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

                UpdateAnimationState();
            }

            base.OnHandleCreated(e);

            if (!DesignMode) RemoveNativeEdge();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            SetProgressState(TaskbarProgressBarState.NoProgress);
            SetProgressValue(0);

            _marqueeAnimator.StopMarquee();
            _hoverAnimator.StopMarquee();

            base.OnHandleDestroyed(e);
        }

        protected virtual new async void OnStyleChanged(EventArgs e)
        {
            if (!DesignMode)
            {
                if (Style != ProgressBarStyle.Marquee)
                {
                    if (CanAnimate)
                        _valueAnimator.AnimateTo(_value, Program.AnimationSpan.TotalMilliseconds);
                    else
                        _valueAnimator.SetImmediate(_value);
                }

                UpdateAnimationState();
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

        // NOTE: StartMarquee/StopMarquee below are only called when the running state actually
        // needs to change (guarded by IsMarquee). UpdateHighlightState runs on every OnValueChanged,
        // which during a heavy/rapid-progress operation can fire many times per second - calling
        // StartMarquee unconditionally there would reset the loop's stopwatch on every call and the
        // sweep would never visibly advance (looks "stuck"). Guarding makes it idempotent: once
        // running, repeated calls are a no-op and the loop keeps advancing on its own timer.
        private void UpdateHighlightState()
        {
            bool shouldRun = CanAnimate && _highlightEffectEnabled && Style != ProgressBarStyle.Marquee && _value > Minimum && _value < Maximum;

            if (shouldRun)
            {
                if (!_hoverAnimator.IsMarquee)
                    _hoverAnimator.StartMarquee(HoverLoopDurationMs);
            }
            else
            {
                _hoverAnimator.StopMarquee();
            }
        }

        private void UpdateAnimationState()
        {
            bool shouldRunMarquee = CanAnimate && Style == ProgressBarStyle.Marquee;

            if (shouldRunMarquee)
            {
                if (!_marqueeAnimator.IsMarquee)
                    _marqueeAnimator.StartMarquee(MarqueeLoopDurationMs);
            }
            else
            {
                _marqueeAnimator.StopMarquee();
            }

            UpdateHighlightState();
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
                    _valueAnimator?.Dispose();
                    _marqueeAnimator?.Dispose();
                    _hoverAnimator?.Dispose();
                    _stateColorAnimator?.Dispose();
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
                _marqueeAnimator.StopMarquee();
                _hoverAnimator.StopMarquee();
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
                        _valueAnimator.AnimateTo(_value, Program.AnimationSpan.TotalMilliseconds);
                    }
                    else
                    {
                        _valueAnimator.SetImmediate(_value);
                    }

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

                    bool shouldRun = CanAnimate && _highlightEffectEnabled && Style != ProgressBarStyle.Marquee && _value > Minimum && _value < Maximum;

                    // Speed change is a deliberate, infrequent user/style action, so a phase reset here is fine
                    // (unlike UpdateHighlightState, this isn't called on every OnValueChanged).
                    if (shouldRun)
                        _hoverAnimator.StartMarquee(HoverLoopDurationMs);
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

                    if (CanAnimate)
                    {
                        _stateColorAnimator.AnimateTo(color, Program.AnimationSpan_Quick.TotalMilliseconds);
                    }
                    else
                    {
                        _stateColorAnimator.SetImmediate(color);
                    }

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

        private double Percentage => _valueAnimator.CurrentValue / (double)(Maximum - Minimum);
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
            get => (int)Math.Round(_valueAnimator.CurrentValue);
            set => _valueAnimator.SetImmediate(value);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Color StateColor
        {
            get => _stateColorAnimator.CurrentValue;
            set => _stateColorAnimator.SetImmediate(value);
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
        public bool IsMarquee => Style == ProgressBarStyle.Marquee && _marqueeAnimator != null;

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

                    if (Style == ProgressBarStyle.Marquee && CanAnimate)
                        _marqueeAnimator.StartMarquee(MarqueeLoopDurationMs);
                }
            }
        }

        private int _marqueeAnimationSpeed = 100;

        // Strips the native sunken edge (WS_EX_CLIENTEDGE/STATICEDGE) that classic theme draws around msctls_progress32.
        private void RemoveNativeEdge()
        {
            if (!IsHandleCreated) return;

            Win32Control.RemoveExtendedStyle(Handle, Win32Control.ControlExtendedStyles.ClientEdge | Win32Control.ControlExtendedStyles.StaticEdge);
        }

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

            Color stateColor = _stateColorAnimator.CurrentValue;

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
                    float offsetX = rect.Width * (float)_marqueeAnimator.CurrentValue;

                    RectangleF marqueeRect = new(offsetX, 0, segmentWidth, rect.Height);

                    using (LinearGradientBrush brush = new(marqueeRect, Color.Transparent, Color.Transparent, LinearGradientMode.Horizontal))
                    {
                        ColorBlend cb = new()
                        {
                            Colors = [Color.Transparent, Color.FromArgb(_alpha, Program.Style.DarkMode ? stateColor.Light(0.1f) : stateColor.Dark(0.1f)), Color.Transparent],
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
                    using (LinearGradientBrush br = new(rect, Program.Style.DarkMode ? stateColor.Dark(0.05f) : stateColor.Light(), stateColor, (float)Percentage * 360f, true))
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
                        float highlightLeft = ((float)_hoverAnimator.CurrentValue * (rectValue.Width + highlightWidth)) - highlightWidth;

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
                        Color hilightColor = Program.Style.DarkMode ? stateColor.Light() : stateColor.Dark();
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
                using (Pen P_Value = new(Color.FromArgb(100, Program.Style.DarkMode ? stateColor.Light() : stateColor.Dark())))
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
                    float startAngle = ((float)_marqueeAnimator.CurrentValue * 360) - 60; // Rotate around

                    using (LinearGradientBrush br = new(rect, stateColor, Program.Style.DarkMode ? stateColor.Light() : stateColor.LightLight(), (float)Percentage * 360f, true))
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
                    using (LinearGradientBrush br = new(rect, stateColor, Program.Style.DarkMode ? stateColor.Light() : stateColor.LightLight(), (float)Percentage * 360f, true))
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

        /// <summary>
        /// Time-based animation driver for numeric values: computes progress by elapsed wall-clock time rather than tick count, so delayed or coalesced ticks never cause visible lag
        /// accumulation. Also used in a continuous looping mode for marquee and hover sweeps. The paint request is coalesced so a busy UI thread never builds a backlog of pending repaints.
        /// </summary>
        private sealed class ProgressAnimator : IDisposable
        {
            private readonly NativeMethods.Winmm.MultimediaTimer _timer;
            private readonly Control _owner;
            private readonly Stopwatch _stopwatch;
            private double _fromValue;
            private double _toValue;
            private double _durationMs;
            private volatile bool _paintPending;

            /// <summary>Current interpolated value, safe to read from any thread.</summary>
            public double CurrentValue { get; private set; }

            /// <summary>True while a one-shot value transition is in progress.</summary>
            public bool IsAnimating { get; private set; }

            /// <summary>Marquee mode: when true, CurrentValue represents a looping head position in [0..1].</summary>
            public bool IsMarquee { get; private set; }

            public ProgressAnimator(Control owner, uint tickIntervalMs = 8)
            {
                _owner = owner;
                _stopwatch = new();
                _timer = new(tickIntervalMs);
                _timer.Tick += Timer_Tick;
            }

            /// <summary>Starts an eased value transition from the current value to newValue.</summary>
            public void AnimateTo(double newValue, double durationMs)
            {
                IsMarquee = false;
                _fromValue = CurrentValue;
                _toValue = newValue;
                _durationMs = Math.Max(1, durationMs);
                IsAnimating = true;
                _stopwatch.Restart();
                _timer.Start();
            }

            /// <summary>Immediately sets the value with no animation and stops any running timer.</summary>
            public void SetImmediate(double value)
            {
                IsMarquee = false;
                IsAnimating = false;
                _fromValue = value;
                _toValue = value;
                CurrentValue = value;
                _timer.Stop();
                RequestPaint();
            }

            /// <summary>Starts a continuous looping animation, wrapping back to 0 every loopDurationMs.</summary>
            public void StartMarquee(double loopDurationMs)
            {
                IsMarquee = true;
                IsAnimating = false;
                _durationMs = Math.Max(1, loopDurationMs);
                _stopwatch.Restart();
                _timer.Start();
            }

            public void StopMarquee()
            {
                if (!IsMarquee) return;

                IsMarquee = false;
                CurrentValue = 0;
                _timer.Stop();
                RequestPaint();
            }

            private void Timer_Tick(object sender, EventArgs e)
            {
                double elapsedMs = _stopwatch.Elapsed.TotalMilliseconds;

                if (IsMarquee)
                {
                    double loopT = (elapsedMs % _durationMs) / _durationMs;
                    CurrentValue = loopT;
                }
                else if (IsAnimating)
                {
                    double t = Math.Min(1.0, elapsedMs / _durationMs);
                    double eased = EaseOutCubic(t);
                    CurrentValue = _fromValue + (_toValue - _fromValue) * eased;

                    if (t >= 1.0)
                    {
                        IsAnimating = false;
                        _timer.Stop();
                    }
                }

                RequestPaint();
            }

            private static double EaseOutCubic(double t)
            {
                double p = t - 1.0;
                return p * p * p + 1.0;
            }

            private void RequestPaint()
            {
                // Coalescing guard: never let more than one repaint sit queued on the UI thread.
                // If the UI thread is backed up, extra ticks are simply skipped until it catches up, and the next paint always reflects the current, time-correct CurrentValue.
                if (_paintPending) return;
                if (_owner == null || _owner.IsDisposed || !_owner.IsHandleCreated) return;

                _paintPending = true;

                _owner.BeginInvoke(new MethodInvoker(() =>
                {
                    _paintPending = false;

                    if (!_owner.IsDisposed)
                        _owner.Invalidate();
                }));
            }

            public void Dispose()
            {
                _timer.Tick -= Timer_Tick;
                _timer.Dispose();
            }
        }

        /// <summary>
        /// Time-based animation driver for Color values, following the same elapsed-time interpolation and paint-coalescing model as ProgressAnimator.
        /// </summary>
        private sealed class ColorAnimator : IDisposable
        {
            private readonly NativeMethods.Winmm.MultimediaTimer _timer;
            private readonly Control _owner;
            private readonly Stopwatch _stopwatch;
            private Color _fromColor;
            private Color _toColor;
            private double _durationMs;
            private volatile bool _paintPending;

            /// <summary>Current interpolated color, safe to read from any thread.</summary>
            public Color CurrentValue { get; private set; }

            /// <summary>True while a color transition is in progress.</summary>
            public bool IsAnimating { get; private set; }

            public ColorAnimator(Control owner, Color initialColor, uint tickIntervalMs = 8)
            {
                _owner = owner;
                CurrentValue = initialColor;
                _stopwatch = new();
                _timer = new(tickIntervalMs);
                _timer.Tick += Timer_Tick;
            }

            /// <summary>Starts an eased color transition from the current color to toColor.</summary>
            public void AnimateTo(Color toColor, double durationMs)
            {
                _fromColor = CurrentValue;
                _toColor = toColor;
                _durationMs = Math.Max(1, durationMs);
                IsAnimating = true;
                _stopwatch.Restart();
                _timer.Start();
            }

            /// <summary>Immediately sets the color with no animation and stops any running timer.</summary>
            public void SetImmediate(Color color)
            {
                IsAnimating = false;
                CurrentValue = color;
                _timer.Stop();
                RequestPaint();
            }

            private void Timer_Tick(object sender, EventArgs e)
            {
                double elapsedMs = _stopwatch.Elapsed.TotalMilliseconds;
                double t = Math.Min(1.0, elapsedMs / _durationMs);
                double eased = EaseOutCubic(t);

                int a = LerpChannel(_fromColor.A, _toColor.A, eased);
                int r = LerpChannel(_fromColor.R, _toColor.R, eased);
                int g = LerpChannel(_fromColor.G, _toColor.G, eased);
                int b = LerpChannel(_fromColor.B, _toColor.B, eased);

                CurrentValue = Color.FromArgb(a, r, g, b);

                if (t >= 1.0)
                {
                    IsAnimating = false;
                    _timer.Stop();
                }

                RequestPaint();
            }

            private static int LerpChannel(int from, int to, double t)
            {
                return (int)Math.Round(from + (to - from) * t);
            }

            private static double EaseOutCubic(double t)
            {
                double p = t - 1.0;
                return p * p * p + 1.0;
            }

            private void RequestPaint()
            {
                if (_paintPending) return;
                if (_owner == null || _owner.IsDisposed || !_owner.IsHandleCreated) return;

                _paintPending = true;

                _owner.BeginInvoke(new MethodInvoker(() =>
                {
                    _paintPending = false;

                    if (!_owner.IsDisposed)
                        _owner.Invalidate();
                }));
            }

            public void Dispose()
            {
                _timer.Tick -= Timer_Tick;
                _timer.Dispose();
            }
        }
    }
}