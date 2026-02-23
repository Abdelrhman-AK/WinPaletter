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
        }

        #endregion

        #region Fields

        // Taskbar
        private readonly ITaskbarList3 taskbarList = !OS.WXP && !OS.WVista ? (ITaskbarList3)new CTaskbarList() : null;
        private IntPtr _formHwnd = IntPtr.Zero;
        private DateTime _lastTaskbarUpdate = DateTime.MinValue;

        // Data
        private readonly List<GraphPoint> _points = [];
        private readonly object _lock = new();
        private double _currentSpeed;
        private string _speedText = "0 B/s";

        // UI
        private RoundedCorners _corners = RoundedCorners.All;
        private int _parentLevel = 0;
        private bool _disposed = false;

        // Constants
        private const int TASKBAR_UPDATE_THROTTLE_MS = 200;
        private const int TOP_MARGIN = 15;

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

        #endregion

        #region Data Structures

        private struct GraphPoint
        {
            public double Progress { get; }
            public double Speed { get; }

            public GraphPoint(double progress, double speed)
            {
                Progress = progress;
                Speed = speed;
            }
        }

        #endregion

        #region Public Methods

        public void Add(double percent, double speed)
        {
            lock (_lock)
            {
                percent = Math.Max(0, Math.Min(100, percent));
                double progress = percent / 100.0;

                _currentSpeed = SmoothSpeed(speed);
                _speedText = FormatSpeed(_currentSpeed);

                if (_points.Count > 0 && progress <= _points[_points.Count - 1].Progress)
                {
                    _points[_points.Count - 1] = new GraphPoint(progress, _currentSpeed);
                }
                else
                {
                    _points.Add(new GraphPoint(progress, _currentSpeed));
                }

                ThrottleTaskbarUpdate();
            }

            Invalidate();
        }

        public void Clear()
        {
            lock (_lock)
            {
                _points.Clear();
                _currentSpeed = 0;
                _speedText = "0 B/s";
            }

            SetProgressValue(0);
            SetProgressState(TaskbarProgressBarState.NoProgress);
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

        private double GetMaxSpeed()
        {
            double max = 0;
            for (int i = 0; i < _points.Count; i++)
            {
                if (_points[i].Speed > max)
                    max = _points[i].Speed;
            }
            return max;
        }

        private string FormatSpeed(double speed)
        {
            if (speed <= 0) return "0 B/s";

            string[] units = { "B/s", "KB/s", "MB/s", "GB/s" };
            int unit = 0;

            while (speed >= 1024 && unit < units.Length - 1)
            {
                speed /= 1024;
                unit++;
            }

            return speed < 10
                ? $"{speed:F1} {units[unit]}"
                : $"{speed:F0} {units[unit]}";
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

        #region Drawing Methods

        private void DrawCurrentSpeedLine(Graphics g, Rectangle rect)
        {
            lock (_lock)
            {
                if (_points.Count == 0) return;

                // Calculate the height for the 5 equal speed parts
                int availableHeight = rect.Height - TOP_MARGIN;
                float partHeight = availableHeight / 5f;

                int graphTop = rect.Top + TOP_MARGIN;
                int graphBottom = rect.Bottom - 1;
                int graphHeight = graphBottom - graphTop;

                double maxSpeed = GetMaxSpeed();
                if (maxSpeed <= 0) maxSpeed = 1;

                double ratio = Math.Max(0, Math.Min(1, _currentSpeed / maxSpeed));
                float y = graphBottom - (float)(graphHeight * ratio);

                // Draw the speed line
                using (Pen pen = new(ForeColor))
                {
                    g.DrawLine(pen, rect.Left, y, rect.Right, y);
                }

                // Draw the speed text above the line
                using (SolidBrush brush = new(ForeColor))
                {
                    SizeF textSize = g.MeasureString(_speedText, Fonts.Console);
                    float textX = rect.Right - textSize.Width - 2;

                    // Position text above the line
                    float textY = y - textSize.Height - 2;

                    // Ensure text stays within the control bounds
                    if (textY < rect.Top + 2)
                    {
                        textY = rect.Top + 2;
                    }

                    g.DrawString(_speedText, Fonts.Console, brush, textX, textY);
                }
            }
        }

        private void DrawGrid(Graphics g, Rectangle rect, Color gridColor)
        {
            using (Pen solidPen = new(gridColor) { DashStyle = DashStyle.Solid })
            {
                int top = rect.Top;
                int bottom = rect.Bottom;

                // Calculate available height for the 5 speed parts
                int availableHeight = rect.Height - TOP_MARGIN;
                float partHeight = availableHeight / 5f;

                // Draw horizontal grid lines through the entire height (including TOP_MARGIN)
                // These represent the boundaries between speed levels
                for (int i = 0; i <= 5; i++)
                {
                    float y = top + TOP_MARGIN + (i * partHeight);

                    // Ensure we don't draw below the control
                    if (y <= bottom)
                    {
                        // All lines are solid now
                        g.DrawLine(solidPen, rect.Left, y, rect.Right, y);
                    }
                }

                // Draw vertical grid lines through the entire height (including TOP_MARGIN)
                for (int i = 1; i < 6; i++)
                {
                    float x = rect.Left + (rect.Width * i / 6f);

                    // Draw the full vertical line including through TOP_MARGIN
                    g.DrawLine(solidPen, x, top, x, bottom);
                }
            }
        }

        private void DrawGraph(Graphics g, Rectangle rect, Color graphColor)
        {
            lock (_lock)
            {
                if (_points.Count < 2) return;

                int graphTop = rect.Top + TOP_MARGIN;
                int graphBottom = rect.Bottom - 1;
                int graphHeight = graphBottom - graphTop;

                double maxSpeed = GetMaxSpeed();
                if (maxSpeed <= 0) maxSpeed = 1;

                PointF[] points = new PointF[_points.Count];

                for (int i = 0; i < _points.Count; i++)
                {
                    float x = rect.Left + (float)(rect.Width * _points[i].Progress);
                    double ratio = Math.Max(0, Math.Min(1, _points[i].Speed / maxSpeed));

                    // Y coordinate is calculated within the graph area (below top margin)
                    float y = graphBottom - (float)(graphHeight * ratio);

                    // Ensure y stays within graph bounds
                    y = Math.Max(graphTop, Math.Min(y, graphBottom));

                    points[i] = new PointF(x, y);
                }

                // Draw the line
                using (Pen pen = new(graphColor)
                {
                    LineJoin = LineJoin.Round,
                    StartCap = LineCap.Round,
                    EndCap = LineCap.Round
                })
                {
                    g.DrawLines(pen, points);
                }

                // Fill under the line - only fills the graph area (below top margin)
                using (SolidBrush fill = new(Color.FromArgb(100, graphColor)))
                using (GraphicsPath path = new())
                {
                    path.AddLines(points);

                    int lastIndex = points.Length - 1;

                    // Down from last point to bottom of graph area
                    path.AddLine(
                        points[lastIndex].X,
                        points[lastIndex].Y,
                        points[lastIndex].X,
                        graphBottom);

                    // Across bottom of graph area
                    path.AddLine(
                        points[lastIndex].X,
                        graphBottom,
                        points[0].X,
                        graphBottom);

                    // Up to first point
                    path.AddLine(
                        points[0].X,
                        graphBottom,
                        points[0].X,
                        points[0].Y);

                    path.CloseFigure();
                    g.FillPath(fill, path);
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
            }
            else
            {
                SetProgressState(TaskbarProgressBarState.NoProgress);
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
            base.OnHandleDestroyed(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing && _formHwnd != IntPtr.Zero && taskbarList != null)
                {
                    try
                    {
                        taskbarList.SetProgressState(_formHwnd, TaskbarProgressBarState.NoProgress);
                    }
                    catch { }
                }
                _disposed = true;
            }
            base.Dispose(disposing);
        }

        protected virtual void OnStyleChanged(EventArgs e)
        {
            ThrottleTaskbarUpdate();
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
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = DesignMode
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
                g.FillRoundedRect(brush, rect);
            }

            // Setup clipping for rounded corners
            GraphicsPath clipPath = null;
            if (Program.Style.RoundedCorners)
            {
                clipPath?.Dispose();
                clipPath = rect.Round(corners: _corners);
                g.SetClip(clipPath);
            }

            // Draw graph components
            DrawGrid(g, rect, gridColor);
            DrawGraph(g, rect, _stateColor);
            DrawCurrentSpeedLine(g, rect);

            // Reset clipping
            g.ResetClip();
            clipPath?.Dispose();

            // Draw border
            using (Pen pen = new(lineColor))
            {
                g.DrawRoundedRectBeveledReverse(pen, rect);
            }
        }

        #endregion

        #region Events

        public new event EventHandler StyleChanged;

        #endregion
    }
}