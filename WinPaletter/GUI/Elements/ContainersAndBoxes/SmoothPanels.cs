using System;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    public abstract class SmoothScrollableControlBase : ScrollableControl
    {
        private readonly Timer _scrollTimer;
        private double _targetV, _targetH;
        private const int SCROLL_INTERVAL = 15; // ms per tick
        private const double SCROLL_SPEED = 0.3; // fraction of remaining distance per tick

        public SmoothScrollableControlBase()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            _scrollTimer = new Timer { Interval = SCROLL_INTERVAL };
            _scrollTimer.Tick += ScrollTimer_Tick;
        }

        private void ScrollTimer_Tick(object sender, EventArgs e)
        {
            bool changed = false;

            // --- Vertical scrolling ---
            if (VerticalScroll.Visible)
            {
                _targetV = Math.Max(VerticalScroll.Minimum, Math.Min(VerticalScroll.Maximum - VerticalScroll.LargeChange + 1, _targetV));

                double current = VerticalScroll.Value;
                double diff = _targetV - current;
                if (Math.Abs(diff) > 1)
                {
                    double step = diff * SCROLL_SPEED;
                    VerticalScroll.Value = (int)Math.Round(current + step);
                    changed = true;
                }
                else
                    VerticalScroll.Value = (int)_targetV;
            }

            // --- Horizontal scrolling ---
            if (HorizontalScroll.Visible)
            {
                _targetH = Math.Max(HorizontalScroll.Minimum, Math.Min(HorizontalScroll.Maximum - HorizontalScroll.LargeChange + 1, _targetH));

                double current = HorizontalScroll.Value;
                double diff = _targetH - current;
                if (Math.Abs(diff) > 1)
                {
                    double step = diff * SCROLL_SPEED;
                    HorizontalScroll.Value = (int)Math.Round(current + step);
                    changed = true;
                }
                else
                    HorizontalScroll.Value = (int)_targetH;
            }

            if (!changed)
            {
                _scrollTimer.Stop();
                return;
            }

            PerformLayout();
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            _targetV = Math.Max(VerticalScroll.Minimum, Math.Min(VerticalScroll.Maximum - VerticalScroll.LargeChange + 1, _targetV));
            _targetH = Math.Max(HorizontalScroll.Minimum, Math.Min(HorizontalScroll.Maximum - HorizontalScroll.LargeChange + 1, _targetH));
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_MOUSEWHEEL = 0x020A;
            const int WM_MOUSEHWHEEL = 0x020E;
            const int WM_HSCROLL = 0x114;
            const int WM_VSCROLL = 0x115;
            const int SB_THUMBTRACK = 5;
            const int SB_THUMBPOSITION = 4;

            if (m.Msg == WM_HSCROLL || m.Msg == WM_VSCROLL)
            {
                long wParamLong = m.WParam.ToInt64();
                int wParamLow = (int)(wParamLong & 0xFFFF);
                if (wParamLow == SB_THUMBTRACK) m.WParam = (IntPtr)((wParamLong & ~0xFFFFL) | SB_THUMBPOSITION);

                if (_scrollTimer.Enabled) _scrollTimer.Stop();

                _targetV = VerticalScroll.Value;
                _targetH = HorizontalScroll.Value;

                base.WndProc(ref m);
                return;
            }

            // Vertical mouse wheel
            if (m.Msg == WM_MOUSEWHEEL && VerticalScroll.Visible)
            {
                short delta = (short)((m.WParam.ToInt64() >> 16) & 0xFFFF);
                int lines = SystemInformation.MouseWheelScrollLines;
                if (lines <= 0) lines = 3;

                double viewport = ClientSize.Height;
                double content = DisplayRectangle.Height;
                double ratio = viewport / Math.Max(1.0, content);
                double baseScroll = viewport * 0.15;
                double increment = -delta / 120.0 * lines * baseScroll * (1.0 + ratio);

                _targetV = Math.Max(VerticalScroll.Minimum, Math.Min(VerticalScroll.Maximum - VerticalScroll.LargeChange + 1, VerticalScroll.Value + increment));

                if (!_scrollTimer.Enabled) _scrollTimer.Start();

                return; // skip default
            }

            // Horizontal mouse wheel (SHIFT + Wheel or dedicated horizontal wheel)
            if (m.Msg == WM_MOUSEHWHEEL && HorizontalScroll.Visible)
            {
                short delta = (short)((m.WParam.ToInt64() >> 16) & 0xFFFF);

                double viewport = ClientSize.Width;
                double content = DisplayRectangle.Width;
                double ratio = viewport / Math.Max(1.0, content);
                double baseScroll = viewport * 0.15;
                double increment = delta / 120.0 * baseScroll * (1.0 + ratio);

                _targetH = Math.Max(HorizontalScroll.Minimum, Math.Min(HorizontalScroll.Maximum - HorizontalScroll.LargeChange + 1, HorizontalScroll.Value + increment));

                if (!_scrollTimer.Enabled) _scrollTimer.Start();

                return; // skip default
            }

            base.WndProc(ref m);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }
    }

    /// <summary>
    /// Smooth Panel (inherits both H and V smooth scroll)
    /// </summary>
    public class SmoothPanel : SmoothScrollableControlBase { }

    /// <summary>
    /// Smooth FlowLayoutPanel (with both vertical & horizontal easing)
    /// </summary>
    public class SmoothFlowLayoutPanel : FlowLayoutPanel
    {
        private readonly Timer _scrollTimer;
        private double _targetV, _targetH;
        private const int SCROLL_INTERVAL = 15;
        private const double SCROLL_SPEED = 0.25;

        public SmoothFlowLayoutPanel()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            _scrollTimer = new Timer { Interval = SCROLL_INTERVAL };
            _scrollTimer.Tick += ScrollTimer_Tick;
        }

        private void ScrollTimer_Tick(object sender, EventArgs e)
        {
            bool changed = false;

            if (VerticalScroll.Visible)
            {
                _targetV = Math.Max(VerticalScroll.Minimum, Math.Min(VerticalScroll.Maximum - VerticalScroll.LargeChange + 1, _targetV));
                double diff = _targetV - VerticalScroll.Value;
                if (Math.Abs(diff) > 1)
                {
                    VerticalScroll.Value = (int)Math.Round(VerticalScroll.Value + diff * SCROLL_SPEED);
                    changed = true;
                }
                else VerticalScroll.Value = (int)_targetV;
            }

            if (HorizontalScroll.Visible)
            {
                _targetH = Math.Max(HorizontalScroll.Minimum, Math.Min(HorizontalScroll.Maximum - HorizontalScroll.LargeChange + 1, _targetH));
                double diff = _targetH - HorizontalScroll.Value;
                if (Math.Abs(diff) > 1)
                {
                    HorizontalScroll.Value = (int)Math.Round(HorizontalScroll.Value + diff * SCROLL_SPEED);
                    changed = true;
                }
                else HorizontalScroll.Value = (int)_targetH;
            }

            if (!changed)
            {
                _scrollTimer.Stop();
                return;
            }

            PerformLayout();
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            _targetV = Math.Max(VerticalScroll.Minimum, Math.Min(VerticalScroll.Maximum - VerticalScroll.LargeChange + 1, _targetV));
            _targetH = Math.Max(HorizontalScroll.Minimum, Math.Min(HorizontalScroll.Maximum - HorizontalScroll.LargeChange + 1, _targetH));
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_MOUSEWHEEL = 0x020A;
            const int WM_MOUSEHWHEEL = 0x020E;
            const int WM_HSCROLL = 0x114;
            const int WM_VSCROLL = 0x115;
            const int SB_THUMBTRACK = 5;
            const int SB_THUMBPOSITION = 4;

            if (m.Msg == WM_HSCROLL || m.Msg == WM_VSCROLL)
            {
                long wParamLong = m.WParam.ToInt64();
                int wParamLow = (int)(wParamLong & 0xFFFF);
                if (wParamLow == SB_THUMBTRACK) m.WParam = (IntPtr)((wParamLong & ~0xFFFFL) | SB_THUMBPOSITION);

                if (_scrollTimer.Enabled)  _scrollTimer.Stop();

                _targetV = VerticalScroll.Value;
                _targetH = HorizontalScroll.Value;

                base.WndProc(ref m);
                return;
            }

            // Vertical scroll
            if (m.Msg == WM_MOUSEWHEEL && VerticalScroll.Visible)
            {
                short delta = (short)((m.WParam.ToInt64() >> 16) & 0xFFFF);
                int lines = SystemInformation.MouseWheelScrollLines;
                if (lines <= 0) lines = 3;

                double viewport = ClientSize.Height;
                double content = DisplayRectangle.Height;
                double ratio = viewport / Math.Max(1.0, content);
                double baseScroll = viewport * 0.15;
                double increment = -delta / 120.0 * lines * baseScroll * (1.0 + ratio);

                _targetV = Math.Max(VerticalScroll.Minimum, Math.Min(VerticalScroll.Maximum - VerticalScroll.LargeChange + 1, VerticalScroll.Value + increment));

                if (!_scrollTimer.Enabled) _scrollTimer.Start();

                return;
            }

            // Horizontal scroll
            if (m.Msg == WM_MOUSEHWHEEL && HorizontalScroll.Visible)
            {
                short delta = (short)((m.WParam.ToInt64() >> 16) & 0xFFFF);

                double viewport = ClientSize.Width;
                double content = DisplayRectangle.Width;
                double ratio = viewport / Math.Max(1.0, content);
                double baseScroll = viewport * 0.15;
                double increment = delta / 120.0 * baseScroll * (1.0 + ratio);

                _targetH = Math.Max(HorizontalScroll.Minimum, Math.Min(HorizontalScroll.Maximum - HorizontalScroll.LargeChange + 1, HorizontalScroll.Value + increment));

                if (!_scrollTimer.Enabled) _scrollTimer.Start();

                return;
            }

            base.WndProc(ref m);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }
    }
}
