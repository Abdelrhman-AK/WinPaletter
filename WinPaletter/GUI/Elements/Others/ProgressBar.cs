using FluentTransitions;
using FluentTransitions.Methods;
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
        }

        #region Variables
        private bool CanAnimate => !DesignMode && Program.Style.Animations && this != null && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        readonly ITaskbarList3 taskbarList = !OS.WXP && !OS.WVista ? (ITaskbarList3)new CTaskbarList() : null;
        private IntPtr FormHwnd = IntPtr.Zero;
        private static readonly TextureBrush Noise = new(Resources.Noise);
        #endregion

        #region Events/Overrides
        public event EventHandler StyleChanged;
        public event EventHandler ValueChanged;

        protected override void OnHandleCreated(EventArgs e)
        {
            if (!DesignMode)
            {
                if (FindForm() != null)
                {
                    Form form = FindForm();

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
            }

            base.OnHandleCreated(e);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            SetProgressValue(0);
            SetProgressState(TaskbarProgressBarState.NoProgress);

            base.OnHandleDestroyed(e);
        }

        protected virtual async void OnStyleChanged(EventArgs e)
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

            await Task.Delay(10);
            Invalidate();
            StyleChanged?.Invoke(this, e);

            base.OnStyleChanged(e);
        }

        private void DoMarqueeAnimation1()
        {
            if (!DesignMode && Style == ProgressBarStyle.Marquee)
            {
                Transition t1 = Transition.With(this, nameof(Value_Animation), Maximum).HookOnCompletionInUiThread(SynchronizationContext.Current, DoMarqueeAnimation2).Build(new ThrowAndCatch(TimeSpan.FromMilliseconds(Program.AnimationDuration)));
                t1.Run();
            }
        }

        private void DoMarqueeAnimation2()
        {
            if (!DesignMode && Style == ProgressBarStyle.Marquee)
            {
                Transition t1 = Transition.With(this, nameof(Value_Animation), Minimum).HookOnCompletionInUiThread(SynchronizationContext.Current, DoMarqueeAnimation1).Build(new ThrowAndCatch(TimeSpan.FromMilliseconds(Program.AnimationDuration)));
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
            Invalidate();
        }

        protected override void Dispose(bool disposing)
        {
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
                        Transition.With(this, nameof(Value_Animation), _value).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
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
        #endregion

        #endregion

        #region Methods

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

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Visible) { UpdateTaskbar(); } else { SetProgressState(TaskbarProgressBarState.NoProgress); }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //Leave it empty to make control background transparent
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
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

                using (LinearGradientBrush br = new(rect, Program.Style.DarkMode ? StateColor.Dark(0.05f) : StateColor.Light(), StateColor, Percentage * 360f, true))
                {
                    if (Dock == DockStyle.None) G.FillRoundedRect(br, rectValue, radius);
                    else G.FillRectangle(br, rectValue);
                }

                if (Dock == DockStyle.None) G.FillRoundedRect(Noise, rectValue, radius);
                else G.FillRectangle(Noise, rectValue);

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
                float _startAngle = Style == ProgressBarStyle.Continuous ? -90 : Percentage * -360;

                RectangleF CircleRect = new(PenWidth, PenWidth, rect.Width - PenWidth * 2, rect.Height - PenWidth * 2 + 1);

                if (CircleRect.Width < 10) { CircleRect.Width = 10; }
                if (CircleRect.Height < 10) { CircleRect.Height = 10; }

                using (LinearGradientBrush br = new(rect, StateColor, Program.Style.DarkMode ? StateColor.Light() : StateColor.LightLight(), Percentage * 360f, true))
                {
                    using (Pen pen = new(br, PenWidth))
                    {
                        using (Pen penNoise = new(Noise, PenWidth))
                        {
                            pen.StartCap = Program.Style.RoundedCorners ? LineCap.Round : LineCap.Flat;
                            pen.EndCap = pen.StartCap;

                            using (LinearGradientBrush brush2 = new(rect, scheme.Colors.Back(parentLevel), scheme.Colors.Back_Hover(parentLevel), LinearGradientMode.BackwardDiagonal))
                            using (Pen pen_background = new(brush2, PenWidth))
                            {
                                G.DrawArc(pen_background, CircleRect, -90, 360);
                                G.DrawArc(pen, CircleRect, _startAngle, (float)(_percent * 360));
                                G.DrawArc(penNoise, CircleRect, _startAngle, (float)(_percent * 360));
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
