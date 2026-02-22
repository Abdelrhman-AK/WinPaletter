using FluentTransitions;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
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

            // Don't initialize timers here - they'll be initialized when needed
        }

        #region Variables
        private bool CanAnimate => !DesignMode && Program.Style.Animations && this != null && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        readonly ITaskbarList3 taskbarList = !OS.WXP && !OS.WVista ? (ITaskbarList3)new CTaskbarList() : null;
        private IntPtr FormHwnd = IntPtr.Zero;
        private static readonly TextureBrush Noise = new(Resources.Noise);

        // Animation fields (copied from Breadcrumb)
        private System.Windows.Forms.Timer _marqueeTimer;
        private System.Windows.Forms.Timer _progressTimer;
        private float _marqueeOffset = 0;
        private float _hoverOffset = 0;
        private int _alpha = 255;
        private bool _disposed = false;

        // New property field
        private bool _highlightEffectEnabled = true;
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

                // Reinitialize timers when handle is recreated
                EnsureTimersInitialized();
            }

            base.OnHandleCreated(e);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            SetProgressValue(0);
            SetProgressState(TaskbarProgressBarState.NoProgress);

            // Stop timers but don't dispose them completely - they'll be recreated if needed
            StopAllTimers();

            base.OnHandleDestroyed(e);
        }

        protected virtual new async void OnStyleChanged(EventArgs e)
        {
            if (!DesignMode)
            {
                EnsureTimersInitialized();

                // Handle marquee timer based on Style
                if (Style == ProgressBarStyle.Marquee)
                {
                    StartMarqueeTimer();
                }
                else
                {
                    StopMarqueeTimer();
                    if (CanAnimate)
                    {
                        Transition.With(this, nameof(Value_Animation), _value).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                    }
                    else
                    {
                        Value_Animation = _value;
                    }
                }
            }

            await Task.Delay(10);
            Invalidate();
            StyleChanged?.Invoke(this, e);

            base.OnStyleChanged(e);
        }

        protected virtual void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);

            // Enable/disable progress timer based on value and highlight effect setting
            UpdateProgressTimerState();
        }

        private void UpdateProgressTimerState()
        {
            if (!DesignMode && Style != ProgressBarStyle.Marquee)
            {
                EnsureProgressTimerInitialized();

                if (_progressTimer != null)
                {
                    bool shouldRun = _highlightEffectEnabled && _value > Minimum && _value < Maximum;

                    if (shouldRun)
                    {
                        if (!_progressTimer.Enabled)
                        {
                            _progressTimer.Start();
                        }
                    }
                    else
                    {
                        if (_progressTimer.Enabled)
                        {
                            _progressTimer.Stop();
                        }
                        _hoverOffset = 0; // reset hover when out of range
                        Invalidate();
                    }
                }
            }
        }

        private void ProgressBar_StyleChanged(object sender, EventArgs e)
        {
            UpdateTaskbar();
            Invalidate();
        }

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    StopAllTimers();
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

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (!DesignMode)
            {
                if (Visible)
                {
                    EnsureTimersInitialized();
                    UpdateTaskbar();
                    if (Style == ProgressBarStyle.Marquee)
                        StartMarqueeTimer();
                    else
                        UpdateProgressTimerState();
                }
                else
                {
                    SetProgressState(TaskbarProgressBarState.NoProgress);
                    StopAllTimers();
                }
            }
        }

        #endregion

        #region Properties

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
                        Transition.With(this, nameof(Value_Animation), _value).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
                    }
                    else { Value_Animation = _value; }

                    UpdateTaskbar();
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
                    UpdateProgressTimerState();
                    Invalidate();
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
                                color = Program.Style.Schemes.Main.Colors.Line_Checked_Hover;
                                break;
                            }

                        case ProgressBarState.Error:
                            {
                                color = Program.Style.Schemes.Secondary.Colors.AccentAlt;
                                break;
                            }

                        case ProgressBarState.Pause:
                            {
                                color = Program.Style.Schemes.Tertiary.Colors.AccentAlt;
                                break;
                            }

                        default:
                            {
                                color = Program.Style.Schemes.Main.Colors.Line_Checked_Hover;
                                break;
                            }
                    }

                    if (CanAnimate) { Transition.With(this, nameof(StateColor), color).Rubberband(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick)); }
                    else { StateColor = color; }

                    UpdateTaskbar();
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

        private float Percentage => _animatedValue / (float)(Maximum - Minimum);
        private float Percentage_Actual => _value / (float)(Maximum - Minimum);

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

                    UpdateTaskbar();
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

        private Color _StateColor = Program.Style.Schemes.Main.Colors.Line_Checked_Hover;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Color StateColor
        {
            get => _StateColor;
            set
            {
                if (value != _StateColor)
                {
                    _StateColor = value;
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
        public bool IsMarquee => Style == ProgressBarStyle.Marquee && _marqueeTimer != null;

        #endregion

        public new bool Visible
        {
            get => base.Visible;
            set
            {
                // Let OnVisibleChanged handle the logic
                base.Visible = value;
            }
        }

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

        private void UpdateTaskbar()
        {
            if (DesignMode || FormHwnd == IntPtr.Zero) return;

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

        #region Timer Management

        /// <summary>
        /// Ensures all timers are properly initialized
        /// </summary>
        private void EnsureTimersInitialized()
        {
            if (_disposed || DesignMode) return;

            EnsureProgressTimerInitialized();
            // Don't auto-start marquee timer here - it will be started by Style property
        }

        /// <summary>
        /// Ensures the progress timer is initialized
        /// </summary>
        private void EnsureProgressTimerInitialized()
        {
            if (_progressTimer != null || _disposed || DesignMode) return;

            _progressTimer = new System.Windows.Forms.Timer { Interval = 35 };
            _progressTimer.Tick += (s, e) =>
            {
                if (_disposed) return;
                // Move the highlight across the progress bar
                _hoverOffset += 0.01f;
                if (_hoverOffset > 1) _hoverOffset = 0;
                Invalidate();
            };
        }

        /// <summary>
        /// Ensures the marquee timer is initialized
        /// </summary>
        private void EnsureMarqueeTimerInitialized()
        {
            if (_marqueeTimer != null || _disposed || DesignMode) return;

            _marqueeTimer = new System.Windows.Forms.Timer { Interval = 20 };
            _marqueeTimer.Tick += (s, e) =>
            {
                if (_disposed) return;
                _marqueeOffset += 0.02f; // Slightly faster for smoother animation
                if (_marqueeOffset > 1) _marqueeOffset = 0;
                Invalidate();
            };
        }

        /// <summary>
        /// Stops all timers without disposing them
        /// </summary>
        private void StopAllTimers()
        {
            if (_progressTimer != null)
            {
                _progressTimer.Stop();
                // Don't dispose - just stop
            }

            if (_marqueeTimer != null)
            {
                _marqueeTimer.Stop();
                // Don't dispose - just stop
            }

            _hoverOffset = 0;
            _marqueeOffset = 0;
        }

        /// <summary>
        /// Starts the marquee timer
        /// </summary>
        private void StartMarqueeTimer()
        {
            if (_disposed || DesignMode) return;

            EnsureMarqueeTimerInitialized();

            if (_marqueeTimer != null && !_marqueeTimer.Enabled)
            {
                _marqueeTimer.Start();
            }
        }

        /// <summary>
        /// Stops the marquee timer
        /// </summary>
        private void StopMarqueeTimer()
        {
            if (_marqueeTimer != null)
            {
                _marqueeTimer.Stop();
            }
            _marqueeOffset = 0;
            Invalidate();
        }

        #endregion

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
                float _percent = Style == ProgressBarStyle.Continuous ? Percentage : 0.5f;
                float _startX = Style == ProgressBarStyle.Continuous ? 0 : Width * Percentage - 0.25f * Width;

                RectangleF rectValue = new(_startX, 0, rect.Width * _percent, Height - 1);

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
                            Colors = [Color.Transparent, Color.FromArgb(_alpha, StateColor), Color.Transparent],
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
                    using (LinearGradientBrush br = new(rect, Program.Style.DarkMode ? StateColor.Dark(0.05f) : StateColor.Light(), StateColor, Percentage * 360f, true))
                    {
                        if (Dock == DockStyle.None) G.FillRoundedRect(br, rectValue, radius);
                        else G.FillRectangle(br, rectValue);
                    }

                    if (Dock == DockStyle.None) G.FillRoundedRect(Noise, rectValue, radius);
                    else G.FillRectangle(Noise, rectValue);

                    // Add moving highlight effect (controlled by HighlightEffectEnabled)
                    if (_highlightEffectEnabled && _value > Minimum && _value < Maximum)
                    {
                        float gradientWidth = Math.Min(100, rect.Width);
                        float highlightPos = (_hoverOffset * (rect.Width + gradientWidth)) - gradientWidth;
                        float visibleWidth = Math.Min(highlightPos + gradientWidth, rectValue.Width) - Math.Max(highlightPos, 0);

                        if (visibleWidth > 0)
                        {
                            float drawX = Math.Max(highlightPos, 0);
                            RectangleF highlightRect = new(drawX, 0, visibleWidth, rect.Height);

                            using (LinearGradientBrush brush = new(highlightRect, Color.Transparent, Program.Style.DarkMode ? StateColor.Light() : StateColor.Dark(), LinearGradientMode.Horizontal))
                            {
                                ColorBlend cb = new()
                                {
                                    Colors = [Color.Transparent, Color.FromArgb(_alpha, Program.Style.DarkMode ? StateColor.Light() : StateColor.Dark()), Color.Transparent],
                                    Positions = [0f, 0.5f, 1f]
                                };
                                brush.InterpolationColors = cb;
                                if (Dock == DockStyle.None) G.FillRoundedRect(brush, highlightRect, radius);
                                else G.FillRectangle(brush, highlightRect);
                            }
                        }
                    }
                }

                G.ResetClip();

                using (Pen P = new(scheme.Colors.Line_Hover(parentLevel)))
                using (Pen P_Value = new(Color.FromArgb((int)(Percentage * 255), StateColor)))
                {
                    if (Dock == DockStyle.None) G.DrawRoundedRect(P, rect, radius);
                    else G.DrawRectangle(P, rect);

                    if (Dock == DockStyle.None) G.DrawRoundedRect(P_Value, rectValue, radius);
                    else G.DrawRectangle(P_Value, rectValue.X, rectValue.Y, rectValue.Width, rectValue.Height);
                }
            }

            else if (Appearance == ProgressBarAppearance.Circle)
            {
                float PenWidth = 0.15f * Math.Max(Width, Height);
                float _percent = Style == ProgressBarStyle.Continuous ? Percentage : 0.5f;
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

                    using (LinearGradientBrush br = new(rect, StateColor, Program.Style.DarkMode ? StateColor.Light() : StateColor.LightLight(), Percentage * 360f, true))
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
                    using (LinearGradientBrush br = new(rect, StateColor, Program.Style.DarkMode ? StateColor.Light() : StateColor.LightLight(), Percentage * 360f, true))
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