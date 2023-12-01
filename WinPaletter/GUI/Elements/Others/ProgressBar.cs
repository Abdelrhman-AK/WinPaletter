using FluentTransitions;
using FluentTransitions.Methods;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

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
            taskbarList.HrInit();
            StyleChanged += ProgressBar_StyleChanged;
        }

        #region Variables
        private bool CanAnimate => !DesignMode && Program.Style.Animations && this != null && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        readonly Interfaces.ITaskbarList3 taskbarList = (Interfaces.ITaskbarList3)new Interfaces.CTaskbarList();
        private IntPtr FormHwnd = IntPtr.Zero;
        private readonly TextureBrush Noise = new(Properties.Resources.GaussianBlur);
        #endregion

        #region Events
        public event EventHandler StyleChanged;
        public event EventHandler ValueChanged;

        protected override void OnHandleCreated(EventArgs e)
        {
            if (!DesignMode) FormHwnd = FindForm().Handle;

            base.OnHandleCreated(e);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            SetProgressValue(0);
            SetProgressState(TaskbarProgressBarState.NoProgress);

            base.OnHandleDestroyed(e);
        }

        protected virtual void OnStyleChanged(EventArgs e)
        {
            if (!DesignMode && Style == ProgressBarStyle.Marquee)
            {
                Value_Animation = Appearance == ProgressBarAppearance.Circle ? _value : Maximum - (Maximum - Minimum) / 2;
            }
            else
            {
                Value_Animation = _value;
            }

            DoMarqueeAnimation1();
            Refresh();
            StyleChanged?.Invoke(this, e);

            base.OnStyleChanged(e);
        }

        private void DoMarqueeAnimation1()
        {
            if (!DesignMode && Style == ProgressBarStyle.Marquee)
            {
                Transition t1 = Transition.With(this, nameof(Value_Animation), Maximum).HookOnCompletionInUiThread(SynchronizationContext.Current, DoMarqueeAnimation2).Build(new FluentTransitions.Methods.ThrowAndCatch(TimeSpan.FromMilliseconds(AnimationDuration)));
                t1.Run();
            }
        }

        private void DoMarqueeAnimation2()
        {
            if (!DesignMode && Style == ProgressBarStyle.Marquee)
            {
                Transition t1 = Transition.With(this, nameof(Value_Animation), Minimum).HookOnCompletionInUiThread(SynchronizationContext.Current, DoMarqueeAnimation1).Build(new ThrowAndCatch(TimeSpan.FromMilliseconds(AnimationDuration)));
                t1.Run();
            }
        }

        protected virtual void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        private void ProgressBar_StyleChanged(object sender, EventArgs e)
        {
            UpdateTaskbar();
            Refresh();
        }
        #endregion

        #region Properties

        private int _value = 0;
        public int Value
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
                        FluentTransitions.Transition.With(this, nameof(Value_Animation), _value).CriticalDamp(TimeSpan.FromMilliseconds(AnimationDuration));
                    }
                    else { Value_Animation = _value; }

                    UpdateTaskbar();

                    OnValueChanged(new EventArgs());
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
                                color = Program.Style.Schemes.Tertiary.Colors.AccentAlt;
                                break;
                            }

                        case ProgressBarState.Pause:
                            {
                                color = Program.Style.Schemes.Main.Colors.Back_Hover;
                                break;
                            }

                        default:
                            {
                                color = Program.Style.Schemes.Main.Colors.Line_Checked_Hover;
                                break;
                            }
                    }

                    if (!DesignMode) { FluentTransitions.Transition.With(this, nameof(StateColor), color).Rubberband(TimeSpan.FromMilliseconds(AnimationDuration)); }
                    else { StateColor = color; }

                    UpdateTaskbar();
                    Refresh();
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
                    Refresh();
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
        public int AnimationDuration { get; set; } = 1000;

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
                    Refresh();
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
                    Refresh();
                }
            }
        }
        #endregion

        #endregion

        #region Voids

        public new void PerformStep()
        {
            Value += Step;
        }

        public new void PerformStep(int step)
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
            taskbarList.SetProgressState(FormHwnd, state);
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

            taskbarList.SetProgressValue(FormHwnd, (ulong)_val, 100);
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
            if (this == null) return;

            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            Rectangle rect = new(0, 0, Width, Height - 1);

            if (Appearance == ProgressBarAppearance.Bar)
            {
                float _percent = Style == ProgressBarStyle.Continuous ? Percentage : 0.5f;
                float _startX = Style == ProgressBarStyle.Continuous ? 0 : Width * Percentage - 0.25f * Width;

                Rectangle rectValue = new((int)_startX, 0, (int)(rect.Width * _percent), Height - 1);

                int radius = Height / 3;

                if (Program.Style.RoundedCorners) G.SetClip(rect.Round(radius));

                G.FillRoundedRect(scheme.Brushes.Back, rect, radius);
                using (SolidBrush br = new(StateColor)) { G.FillRoundedRect(br, rectValue, radius); }
                G.FillRoundedRect(Noise, rectValue, radius);

                G.ResetClip();

                G.DrawRoundedRect_LikeW11(scheme.Pens.Line, rect, radius);
            }

            else if (Appearance == ProgressBarAppearance.Circle)
            {
                float PenWidth = 0.15f * Math.Max(Width, Height);
                float _percent = Style == ProgressBarStyle.Continuous ? Percentage : 0.5f;
                float _startAngle = Style == ProgressBarStyle.Continuous ? -90 : Percentage * -360;

                Rectangle CircleRect = new((int)PenWidth, (int)PenWidth, rect.Width - (int)PenWidth * 2, rect.Height - (int)PenWidth * 2 + 1);

                if (CircleRect.Width < 10) { CircleRect.Width = 10; }
                if (CircleRect.Height < 10) { CircleRect.Height = 10; }

                Color StateColor2 = Program.Style.DarkMode ? StateColor.Light() : StateColor.Dark();

                using (LinearGradientBrush brush = new(rect, StateColor, StateColor2, LinearGradientMode.ForwardDiagonal))
                {
                    using (Pen pen = new(brush, PenWidth))
                    {
                        using (Pen penNoise = new(Noise, PenWidth))
                        {
                            pen.StartCap = Program.Style.RoundedCorners ? LineCap.Round : LineCap.Flat;
                            pen.EndCap = pen.StartCap;

                            using (LinearGradientBrush brush2 = new(rect, scheme.Colors.Back, scheme.Colors.Back_Hover, LinearGradientMode.BackwardDiagonal))
                            {
                                using (Pen pen2 = new(brush2, PenWidth))
                                {
                                    G.DrawArc(pen2, CircleRect, -90, 360);
                                    G.DrawArc(pen, CircleRect, _startAngle, (int)Math.Round((double)(_percent * 360)));
                                    G.DrawArc(penNoise, CircleRect, _startAngle, (int)Math.Round((double)(_percent * 360)));
                                }
                            }
                        }
                    }
                }

                G.ResetClip();
            }

            base.OnPaint(e);
        }
    }
}
