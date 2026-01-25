using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using WinPaletter.Properties;

namespace WinPaletter.UI.WP
{
    [Description("Themed TabControl for WinPaletter UI with fade background")]
    public class TabControl : System.Windows.Forms.TabControl
    {
        public TabControl()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;

            ItemSize = new(35, 140);
            DrawMode = TabDrawMode.OwnerDrawFixed;
            SizeMode = TabSizeMode.Fixed;
            Font = new("Segoe UI", 9f);
        }

        #region Variables
        private bool CanAnimate => !DesignMode && Program.Style.Animations && this is not null && Visible && Parent is not null && Parent.Visible && FindForm() is not null && FindForm().Visible;

        private static readonly TextureBrush Noise = new(Resources.Noise.Fade(0.5f));
        private bool _needsLayoutRefresh;
        private readonly Dictionary<int, float> _tabAlpha = []; // stores 0..1 per tab
        private int _lastSelectedIndex = -1;
        private RectangleF _currentSideTape;
        private RectangleF _previousSideTape;
        private RectangleF _targetSideTape;
        private Timer _animationTimer;
        private int _animDuration = 200;
        private DateTime _animStartTime;
        private int _prevAnimatedIndex = -1; // previous tab being animated

        #endregion

        #region Overrides
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Initialize selected tab alpha
            if (SelectedIndex >= 0)
            {
                _tabAlpha[SelectedIndex] = 1f;
                _lastSelectedIndex = SelectedIndex;
                _currentSideTape = GetSideTapeRect(SelectedIndex);
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            // Clean up timer
            if (_animationTimer != null)
            {
                _animationTimer.Stop();
                _animationTimer.Tick -= AnimationTick;
                _animationTimer.Dispose();
                _animationTimer = null;
            }
            base.OnHandleDestroyed(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (!IsHandleCreated || SelectedIndex < 0)
                return;

            _needsLayoutRefresh = true;

            // Cancel animation safely
            _animationTimer?.Stop();
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            if (!_needsLayoutRefresh || SelectedIndex < 0)
                return;

            _needsLayoutRefresh = false;

            _currentSideTape = GetSideTapeRect(SelectedIndex);
            _previousSideTape = _currentSideTape;
            _targetSideTape = _currentSideTape;

            _tabAlpha.Clear();
            _tabAlpha[SelectedIndex] = 1f;
            _prevAnimatedIndex = -1;

            Invalidate();
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            if (SelectedIndex < 0) return;

            // Stop any running animation
            _animationTimer?.Stop();

            // Prepare previous tab for fade-out
            _prevAnimatedIndex = _lastSelectedIndex != SelectedIndex ? _lastSelectedIndex : -1;

            // Reset alpha for new tab
            _tabAlpha[SelectedIndex] = 0f;
            if (_prevAnimatedIndex != -1 && !_tabAlpha.ContainsKey(_prevAnimatedIndex))
                _tabAlpha[_prevAnimatedIndex] = 1f;

            _previousSideTape = _currentSideTape.IsEmpty ? GetSideTapeRect(SelectedIndex) : _currentSideTape;
            _targetSideTape = GetSideTapeRect(SelectedIndex);

            if (!CanAnimate)
            {
                // Instant update without animation
                _tabAlpha.Clear();
                _tabAlpha[SelectedIndex] = 1f;
                _currentSideTape = _targetSideTape;
                _prevAnimatedIndex = -1;
                Invalidate();
            }
            else
            {
                // Animate normally
                _animStartTime = DateTime.Now;

                if (_animationTimer == null)
                {
                    _animationTimer = new Timer { Interval = 15 };
                    _animationTimer.Tick += AnimationTick;
                }

                _animationTimer.Start();
            }

            _lastSelectedIndex = SelectedIndex;

            base.OnSelectedIndexChanged(e);
        }

        #endregion

        #region Animation
        private void AnimationTick(object sender, EventArgs e)
        {
            float elapsed = (float)(DateTime.Now - _animStartTime).TotalMilliseconds;
            float progress = Math.Min(elapsed / _animDuration, 1f);
            float eased = EaseOutCubic(progress);

            // Animate side tape
            RectangleF start = _previousSideTape;
            RectangleF end = _targetSideTape;

            float x = start.X + (end.X - start.X) * eased;
            float y = start.Y + (end.Y - start.Y) * eased;
            float w = start.Width + (end.Width - start.Width) * eased;
            float h = start.Height + (end.Height - start.Height) * eased;

            // Stretch effect with bounce at midpoint
            /*
                maxStretch	Behavior
                0.3f	    Small stretch, barely noticeable
                0.7         Moderate, clearly visible (current)
                1.0f	    Very long, doubles the size at midpoint
                1.5f	    Extreme, 150% longer at midpoint
             */
            float maxStretch = 1f;
            float stretchEase = 1f - Math.Abs(0.5f - eased) * 2f;
            float stretchFactor = maxStretch * stretchEase;

            if (Alignment == TabAlignment.Left || Alignment == TabAlignment.Right)
                h *= 1f + stretchFactor;
            else
                w *= 1f + stretchFactor;

            _currentSideTape = new RectangleF(x, y, w, h);

            // Animate alpha only for current and previous tab
            if (_prevAnimatedIndex != -1 && _prevAnimatedIndex != SelectedIndex)
                _tabAlpha[_prevAnimatedIndex] = 1f - eased;

            _tabAlpha[SelectedIndex] = eased;

            Invalidate();

            if (progress >= 1f)
            {
                // Finalize
                _tabAlpha.Clear();
                _tabAlpha[SelectedIndex] = 1f;
                _currentSideTape = _targetSideTape;
                _prevAnimatedIndex = -1;
                _animationTimer.Stop();
            }
        }

        private float EaseOutCubic(float t)
        {
            t -= 1f;
            return t * t * t + 1f;
        }
        #endregion

        #region Painting
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            InvokePaintBackground(this, e);

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;
            G.Clear(this.GetParentColor());

            Color sideTapeColor = scheme.Colors.ForeColor_Accent;
            Color bgColor = scheme.Colors.Back_Hover();
            Color lnColor = scheme.Colors.Line_Hover();
            bool RTL = RightToLeft == RightToLeft.Yes;

            for (int i = 0; i < TabPages.Count; i++)
            {
                RectangleF tabRect = GetTabRect(i);
                Color textColor = ForeColor;

                float alpha = _tabAlpha.ContainsKey(i) ? _tabAlpha[i] : 0f;
                if (alpha > 0)
                {
                    using (SolidBrush br = new(Color.FromArgb((int)(alpha * 255), bgColor)))
                    using (Pen p = new(Color.FromArgb((int)(alpha * 255), lnColor)))
                    {
                        G.FillRoundedRect(br, tabRect);
                        G.FillRoundedRect(Noise, tabRect);
                        G.DrawRoundedRect(p, tabRect);
                    }
                }

                // Animate side tape only for selected tab
                if (i == SelectedIndex && _currentSideTape.Width > 0 && _currentSideTape.Height > 0)
                {
                    using (SolidBrush br = new(sideTapeColor))
                    {
                        G.FillRoundedRect(br, _currentSideTape, 2);
                    }
                }


                DrawTabTextAndIcon(G, i, tabRect, ref textColor, RTL);
            }

            base.OnPaint(e);
        }

        private RectangleF GetSideTapeRect(int index)
        {
            if (index < 0 || index >= TabPages.Count)
                return RectangleF.Empty;

            Rectangle tabRect = GetTabRect(index);
            if (tabRect.Width <= 0 || tabRect.Height <= 0)
                return RectangleF.Empty;

            RectangleF tabRectF = tabRect;
            float sideThickness = 3f;

            if (Alignment == TabAlignment.Left || Alignment == TabAlignment.Right)
            {
                float sideLength = tabRectF.Height * 0.45f;
                float x = Alignment == TabAlignment.Left ? tabRectF.X + 4 : tabRectF.Right - 6;
                float y = tabRectF.Y + (tabRectF.Height - sideLength) / 2f;
                return new RectangleF(x, y, sideThickness, sideLength);
            }
            else
            {
                float sideLength = tabRectF.Width * 0.45f;
                float x = tabRectF.X + (tabRectF.Width - sideLength) / 2f;
                float y = tabRectF.Bottom - sideThickness - 1;
                return new RectangleF(x, y, sideLength, sideThickness);
            }
        }

        private void DrawTabTextAndIcon(Graphics g, int index, RectangleF tabRect, ref Color textColor, bool RTL)
        {
            Image img = (ImageList != null && index < ImageList.Images.Count) ? ImageList.Images[index] : null;

            if (img != null)
            {
                RectangleF imgRect = !RTL
                    ? new(tabRect.X + 10, tabRect.Y + (tabRect.Height - img.Height) / 2f + 1, img.Width, img.Height)
                    : new(tabRect.Right - img.Width - 8, tabRect.Y + (tabRect.Height - img.Height) / 2f + 1, img.Width, img.Height);

                RectangleF textRect;
                if (Alignment == TabAlignment.Left || Alignment == TabAlignment.Right)
                {
                    float xPoint = !RTL ? imgRect.Right + 5 : tabRect.X;
                    float fixer = imgRect.Width + 15;
                    textRect = new RectangleF(xPoint, tabRect.Y, tabRect.Width - fixer, tabRect.Height);
                    using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat(RTL))
                    using (SolidBrush br = new(textColor))
                        g.DrawString(TabPages[index].Text, Font, br, textRect, sf);
                }
                else
                {
                    textRect = tabRect;
                    using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat(RTL))
                    using (SolidBrush br = new(textColor))
                        g.DrawString(TabPages[index].Text, Font, br, textRect, sf);
                }

                if (RTL) img.RotateFlip(RotateFlipType.Rotate180FlipY);
                g.DrawImage(img, imgRect);
            }
            else
            {
                using (StringFormat sf = (Alignment == TabAlignment.Left || Alignment == TabAlignment.Right)
                    ? ContentAlignment.MiddleLeft.ToStringFormat(RTL)
                    : ContentAlignment.MiddleCenter.ToStringFormat(RTL))
                using (SolidBrush br = new(textColor))
                {
                    float leftOffset = (Alignment == TabAlignment.Left || Alignment == TabAlignment.Right) ? 10 : 0;
                    RectangleF textRect = new(tabRect.X + leftOffset, tabRect.Y, tabRect.Width - leftOffset, tabRect.Height);
                    g.DrawString(TabPages[index].Text, Font, br, textRect, sf);
                }
            }
        }
        #endregion
    }
}