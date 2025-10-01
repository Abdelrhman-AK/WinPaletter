using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    public class SkeletonOverlay : Control
    {
        private Timer _timer;
        private float _offset; // animation offset
        private Control _target;
        private List<Control> _controls;

        public SkeletonOverlay()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);

            AnimationSpeed = 8; // pixels per tick

            _timer = new Timer();
            TickInterval = 16; // now safe, because _timer exists
            _timer.Tick += (s, e) => { _offset += AnimationSpeed; Invalidate(); };

            // Make control non-focusable and transparent to mouse events so underlying controls still react
            TabStop = false;
            Enabled = true;

            DoubleBuffered = true;
        }

        public int AnimationSpeed { get; set; } // movement per tick in pixels
        public int TickInterval
        {
            get => _timer.Interval;
            set => _timer.Interval = Math.Max(10, value);
        }

        // Control life
        public void Start()
        {
            if (DesignMode) return;

            if (_timer == null)  _timer = new Timer { Interval = TickInterval };
            _timer.Interval = TickInterval;
            _timer.Start();

            Program.Animator.ShowSync(this);

            Invalidate();
        }

        /// <summary>
        /// Stops the current operation and optionally destroys the associated resources.
        /// </summary>
        public void Stop()
        {
            _timer?.Stop();
            Program.Animator.HideSync(this);
            Invalidate();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _timer?.Stop();
                _timer?.Dispose();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Attaches the current control to the specified target control, overlaying it and matching its size, location,
        /// and appearance.
        /// </summary>
        /// <remarks>This method configures the current control to overlay the specified <paramref
        /// name="target"/> control, inheriting its size, location, and parent. Only visible child controls of the
        /// target, excluding container and scrollable controls, are considered for internal processing.</remarks>
        /// <param name="target">The control to which this control will be attached. Must not be <see langword="null"/>.</param>
        public void AttachTo(Control target)
        {
            if (target == null || DesignMode) return;

            _controls?.Clear();
            _target = target;
            BackColor = target.BackColor;

            _controls = [.. target.GetAllControls().Where(c => c.Visible && c != this && c is not TabControl && c is not TablessControl && c is not ContainerControl && c is not ScrollableControl)];

            // Overlay matches target
            Parent = target is TabPage ? target : target.Parent;
            Location = target.Location;
            Size = target.Size;
            Anchor = target.Anchor;
            Dock = DockStyle.Fill;

            BringToFront();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics G = e.Graphics;

            G.SmoothingMode = SmoothingMode.AntiAlias;

            if (_target == null) return;

            // Gradient shimmer setup
            int gradWidth = Math.Max(Width, 300);
            RectangleF rect = new(-gradWidth + (_offset % (gradWidth * 2)), 0, gradWidth * 2, Height);

            Color baseColor = Program.Style.Schemes.Main.Colors.Back();
            Color hoverColor = Color.FromArgb(150, baseColor.IsDark() ? baseColor.Light() : baseColor.Dark());
            Color lineColor = Program.Style.Schemes.Main.Colors.Line_Hover(1);

            using LinearGradientBrush gradient = new(rect, baseColor, hoverColor, LinearGradientMode.Horizontal)
            {
                InterpolationColors = new(3)
                {
                    Colors = [baseColor, hoverColor, baseColor],
                    Positions = [0f, 0.5f, 1f]
                }
            };

            foreach (Control c in _controls)
            {
                Rectangle screenRect = c.RectangleToScreen(c.ClientRectangle);
                Rectangle r = _target.RectangleToClient(screenRect);

                r.Width = Math.Max(1, r.Width);
                r.Height = Math.Max(1, r.Height);

                using (GraphicsPath path = r.Round(Program.Style.Radius))
                using (Pen P = new(lineColor))
                {
                    G.FillPath(gradient, path);
                    G.DrawRoundedRectBeveledReverse(P, r);
                }
            }
        }
    }

}
