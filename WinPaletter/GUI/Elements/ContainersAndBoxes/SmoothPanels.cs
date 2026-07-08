using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    public abstract class SmoothScrollableControlBase : ScrollableControl
    {
        private readonly Timer _scrollTimer;
        protected double _targetV, _targetH;
        private const int SCROLL_INTERVAL = 15;
        private const double SCROLL_SPEED = 0.3;

        public SmoothScrollableControlBase()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.DoubleBuffer();
            _scrollTimer = new Timer { Interval = SCROLL_INTERVAL };
            _scrollTimer.Tick += ScrollTimer_Tick;
            MouseEnter += (s, e) => Focus();
        }

        private void ScrollTimer_Tick(object sender, EventArgs e)
        {
            double currentH = -AutoScrollPosition.X;
            double currentV = -AutoScrollPosition.Y;
            double diffH = _targetH - currentH;
            double diffV = _targetV - currentV;

            // Only animate if the difference is more than 0.1 pixels
            if (Math.Abs(diffH) > 0.1 || Math.Abs(diffV) > 0.1)
            {
                AutoScrollPosition = new Point((int)(currentH + diffH * SCROLL_SPEED), (int)(currentV + diffV * SCROLL_SPEED));
            }
            else
            {
                // Force snap to target to finish the scroll completely
                AutoScrollPosition = new Point((int)_targetH, (int)_targetV);
                _scrollTimer.Stop();
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (HorizontalScroll.Visible && !VerticalScroll.Visible)
                UpdateTarget(ref _targetH, HorizontalScroll, -e.Delta, 120);
            else
                UpdateTarget(ref _targetV, VerticalScroll, -e.Delta, 30);

            if (!_scrollTimer.Enabled) _scrollTimer.Start();
        }

        protected void UpdateTarget(ref double target, ScrollProperties scroll, int delta, int multiplier)
        {
            double max = Math.Max(0, scroll.Maximum - scroll.LargeChange + 1);
            double increment = delta / 120.0 * SystemInformation.MouseWheelScrollLines * multiplier;
            target = Math.Max(0, Math.Min(max, target + increment));
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.User32.WM_MOUSEHWHEEL)
            {
                int delta = (short)((ushort)m.WParam >> 16);
                UpdateTarget(ref _targetH, HorizontalScroll, -delta, 120);
                if (!_scrollTimer.Enabled) _scrollTimer.Start();
                return;
            }
            if (m.Msg == 0x0115 || m.Msg == 0x0114) return;
            base.WndProc(ref m);
        }
    }

    public class SmoothPanel : SmoothScrollableControlBase { }

    public class SmoothFlowLayoutPanel : FlowLayoutPanel
    {
        private readonly Timer _scrollTimer;
        private double _targetV, _targetH;
        private const int SCROLL_INTERVAL = 15;
        private const double SCROLL_SPEED = 0.25;

        public SmoothFlowLayoutPanel()
        {
            this.DoubleBuffer();
            _scrollTimer = new() { Interval = SCROLL_INTERVAL };
            _scrollTimer.Tick += (s, e) => {
                double currentH = -AutoScrollPosition.X;
                double currentV = -AutoScrollPosition.Y;
                double diffH = _targetH - currentH;
                double diffV = _targetV - currentV;

                if (Math.Abs(diffH) > 0.1 || Math.Abs(diffV) > 0.1)
                    AutoScrollPosition = new Point((int)(currentH + diffH * SCROLL_SPEED), (int)(currentV + diffV * SCROLL_SPEED));
                else { AutoScrollPosition = new Point((int)_targetH, (int)_targetV); _scrollTimer.Stop(); }
            };
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (HorizontalScroll.Visible && !VerticalScroll.Visible)
                _targetH = Math.Max(0, Math.Min(HorizontalScroll.Maximum - HorizontalScroll.LargeChange + 1, _targetH + (-e.Delta / 120.0 * 120)));
            else
                _targetV = Math.Max(0, Math.Min(VerticalScroll.Maximum - VerticalScroll.LargeChange + 1, _targetV + (-e.Delta / 120.0 * 60)));

            _scrollTimer.Start();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.User32.WM_MOUSEHWHEEL)
            {
                int delta = (short)((ushort)m.WParam >> 16);
                _targetH = Math.Max(0, Math.Min(HorizontalScroll.Maximum - HorizontalScroll.LargeChange + 1, _targetH + (-delta / 120.0 * 120)));
                _scrollTimer.Start();
                return;
            }
            base.WndProc(ref m);
        }
    }
}