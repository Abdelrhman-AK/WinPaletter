using System;
using System.Drawing;
using System.Windows.Forms;
using static WinPaletter.NativeMethods.User32;

namespace WinPaletter.UI.WP
{
    public abstract class SmoothScrollableControlBase : ScrollableControl
    {
        private readonly Timer _scrollTimer;
        protected double _targetV, _targetH;
        private double _currentV, _currentH;
        private bool _positionInitialized;
        private const int SCROLL_INTERVAL = 15;
        private const double SCROLL_SPEED = 0.3;
        private const double SNAP_THRESHOLD = 0.5;

        public SmoothScrollableControlBase()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.DoubleBuffer();
            _scrollTimer = new Timer { Interval = SCROLL_INTERVAL };
            _scrollTimer.Tick += ScrollTimer_Tick;
            MouseEnter += (s, e) => Focus();
        }

        private void EnsurePositionInitialized()
        {
            if (!_positionInitialized)
            {
                _currentH = -AutoScrollPosition.X;
                _currentV = -AutoScrollPosition.Y;
                _targetH = _currentH;
                _targetV = _currentV;
                _positionInitialized = true;
            }
        }

        private void ScrollTimer_Tick(object sender, EventArgs e)
        {
            double diffH = _targetH - _currentH;
            double diffV = _targetV - _currentV;

            // Only animate if the difference is more than the snap threshold
            if (Math.Abs(diffH) > SNAP_THRESHOLD || Math.Abs(diffV) > SNAP_THRESHOLD)
            {
                _currentH += diffH * SCROLL_SPEED;
                _currentV += diffV * SCROLL_SPEED;
                AutoScrollPosition = new Point((int)Math.Round(_currentH), (int)Math.Round(_currentV));
            }
            else
            {
                // Force snap to target to finish the scroll completely
                _currentH = _targetH;
                _currentV = _targetV;
                AutoScrollPosition = new Point((int)Math.Round(_currentH), (int)Math.Round(_currentV));
                _scrollTimer.Stop();
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            EnsurePositionInitialized();

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
            if (m.Msg == (uint)WindowsMessage.MouseHWheel)
            {
                EnsurePositionInitialized();
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
        private double _currentV, _currentH;
        private bool _positionInitialized;
        private const int SCROLL_INTERVAL = 15;
        private const double SCROLL_SPEED = 0.25;
        private const double SNAP_THRESHOLD = 0.5;

        public SmoothFlowLayoutPanel()
        {
            this.DoubleBuffer();
            _scrollTimer = new() { Interval = SCROLL_INTERVAL };
            _scrollTimer.Tick += (s, e) =>
            {
                double diffH = _targetH - _currentH;
                double diffV = _targetV - _currentV;

                if (Math.Abs(diffH) > SNAP_THRESHOLD || Math.Abs(diffV) > SNAP_THRESHOLD)
                {
                    _currentH += diffH * SCROLL_SPEED;
                    _currentV += diffV * SCROLL_SPEED;
                    AutoScrollPosition = new Point((int)Math.Round(_currentH), (int)Math.Round(_currentV));
                }
                else
                {
                    _currentH = _targetH;
                    _currentV = _targetV;
                    AutoScrollPosition = new Point((int)Math.Round(_currentH), (int)Math.Round(_currentV));
                    _scrollTimer.Stop();
                }
            };
        }

        private void EnsurePositionInitialized()
        {
            if (!_positionInitialized)
            {
                _currentH = -AutoScrollPosition.X;
                _currentV = -AutoScrollPosition.Y;
                _targetH = _currentH;
                _targetV = _currentV;
                _positionInitialized = true;
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            EnsurePositionInitialized();

            if (HorizontalScroll.Visible && !VerticalScroll.Visible)
                _targetH = Math.Max(0, Math.Min(HorizontalScroll.Maximum - HorizontalScroll.LargeChange + 1, _targetH + (-e.Delta / 120.0 * 120)));
            else
                _targetV = Math.Max(0, Math.Min(VerticalScroll.Maximum - VerticalScroll.LargeChange + 1, _targetV + (-e.Delta / 120.0 * 60)));

            _scrollTimer.Start();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (uint)WindowsMessage.MouseHWheel)
            {
                EnsurePositionInitialized();
                int delta = (short)((ushort)m.WParam >> 16);
                _targetH = Math.Max(0, Math.Min(HorizontalScroll.Maximum - HorizontalScroll.LargeChange + 1, _targetH + (-delta / 120.0 * 120)));
                _scrollTimer.Start();
                return;
            }
            base.WndProc(ref m);
        }
    }
}