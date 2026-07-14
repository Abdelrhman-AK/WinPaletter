using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.Properties;
using WinPaletter.Theme.Structures;
using static WinPaletter.NativeMethods.User32;

namespace WinPaletter.UI.WP
{
    [ToolboxItem(false)]
    public class ContextMenuStripRenderer : ToolStripRenderer
    {
        private readonly ContextMenuStrip _parentMenu;

        public ContextMenuStripRenderer(ContextMenuStrip parentMenu)
        {
            _parentMenu = parentMenu;
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            e.ArrowColor = Program.Style.DarkMode ? Color.White : Color.Black;
            base.OnRenderArrow(e);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle itemRectangle = e.Item.ContentRectangle;
            itemRectangle.Y -= 1;

            // Get the animated alpha for this item
            float alpha = _parentMenu.GetItemAlpha(e.Item);

            if (alpha > 0.01f)
            {
                int alphaInt = (int)(125 * alpha);
                Color backColor = Color.FromArgb(alphaInt, Program.Style.Schemes.Main.Colors.Back_Checked_Hover);
                Color lineColor = Color.FromArgb(alphaInt, Program.Style.Schemes.Main.Colors.Line_Checked_Hover);

                using (SolidBrush br = new(backColor))
                using (Pen P = new(lineColor))
                {
                    G.FillRoundedRect(br, itemRectangle);
                    G.DrawRoundedRectBeveled(P, itemRectangle);
                }
            }
            else
            {
                base.OnRenderMenuItemBackground(e);
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Text)) return;

            float alpha = _parentMenu.GetItemAlpha(e.Item);

            if (alpha > 0.01f)
            {
                Color accentColor = Program.Style.Schemes.Main.Colors.ForeColor_Accent;
                Color normalColor = Program.Style.DarkMode ? Color.White : Color.Black;

                e.TextColor = Color.FromArgb(
                    (int)(normalColor.R + (accentColor.R - normalColor.R) * alpha),
                    (int)(normalColor.G + (accentColor.G - normalColor.G) * alpha),
                    (int)(normalColor.B + (accentColor.B - normalColor.B) * alpha)
                );
            }
            else
            {
                e.TextColor = Program.Style.DarkMode ? Color.White : Color.Black;
            }

            if (e.ToolStrip is System.Windows.Forms.ContextMenuStrip strip)
            {
                e.Item.AutoSize = false;

                int textHeight = strip is ContextMenuStrip customStrip ? customStrip.ItemHeight : 24;
                Rectangle textRect = e.TextRectangle;

                if (textRect.Width <= 0 || textRect.Height <= 0)
                {
                    textRect = new Rectangle(
                        e.Item.ContentRectangle.Left + 5,
                        e.Item.ContentRectangle.Top + (e.Item.ContentRectangle.Height - textHeight) / 2,
                        e.Item.ContentRectangle.Width - 5,
                        textHeight);
                }
                else
                {
                    textRect = new Rectangle(textRect.Left, textRect.Top, textRect.Width, textHeight);
                }

                e.TextRectangle = textRect;
                e.Item.Height = textHeight + 3;
            }

            base.OnRenderItemText(e);
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            float alpha = _parentMenu.GetItemAlpha(e.Item);

            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle itemRectangle = e.Item.ContentRectangle;

            G.FillRoundedRect(Program.Style.Schemes.Tertiary.Brushes.Accent, itemRectangle);
            G.DrawRoundedRectBeveled(Program.Style.Schemes.Tertiary.Pens.AccentAlt, itemRectangle);

            if (alpha > 0.01f)
            {
                Color backColor = Color.FromArgb((int)(255 * alpha), Program.Style.Schemes.Tertiary.Colors.Accent);
                Color lineColor = Color.FromArgb((int)(255 * alpha), Program.Style.Schemes.Tertiary.Colors.AccentAlt);

                using (SolidBrush br = new(backColor))
                using (Pen P = new(lineColor))
                {
                    G.FillRoundedRect(br, itemRectangle);
                    G.DrawRoundedRectBeveled(P, itemRectangle);
                }
            }
        }

        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            Rectangle rectangle = new(0, 0, e.Image.Width, e.Image.Height);
            Rectangle bounds = new(0, 0, Math.Min(e.Item.ContentRectangle.Width, e.Item.ContentRectangle.Height), Math.Min(e.Item.ContentRectangle.Width, e.Item.ContentRectangle.Height));
            rectangle.X = 2 + e.Item.ContentRectangle.X + (bounds.Width - rectangle.Width) / 2;
            rectangle.Y = e.Item.ContentRectangle.Y + (bounds.Height - rectangle.Height) / 2;

            e.Graphics.DrawImage(e.Image, rectangle);
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle itemRectangle = e.Item.ContentRectangle;
            itemRectangle.Y += 1;

            using (Pen P = new(Program.Style.Schemes.Main.Colors.Line_Hover(e.ToolStrip.Level())))
            {
                G.DrawLine(P, itemRectangle.Location, itemRectangle.Location + new Size(e.Vertical ? 0 : itemRectangle.Width, e.Vertical ? itemRectangle.Height : 0));
            }

            base.OnRenderSeparator(e);
        }
    }

    public partial class ContextMenuStrip : System.Windows.Forms.ContextMenuStrip
    {
        private readonly Dictionary<ToolStripItem, AnimatedItemState> _itemStates = new();
        private readonly System.Windows.Forms.Timer _animationTimer;
        private ToolStripItem _previouslySelectedItem;

        AnimateWindowFlags AnimationType => Program.TM.WindowsEffects.MenuFade == WinEffects.MenuAnimType.Fade ? AnimateWindowFlags.AW_BLEND : AnimateWindowFlags.AW_HOR_POSITIVE;

        private class AnimatedItemState
        {
            public float Alpha { get; set; } = 0f;
            public float TargetAlpha { get; set; } = 0f;
            public DateTime LastUpdate { get; set; } = DateTime.Now;

            public bool NeedsUpdate => Math.Abs(Alpha - TargetAlpha) > 0.001f;

            public void Update(float deltaTime)
            {
                const float speed = 10f; // Animation speed - higher = faster
                float t = Math.Min(1f, deltaTime * speed);
                Alpha = Alpha + (TargetAlpha - Alpha) * t;

                // Snap to target when very close to avoid floating point issues
                if (Math.Abs(Alpha - TargetAlpha) < 0.002f) Alpha = TargetAlpha;
            }
        }

        public ContextMenuStrip()
        {
            AllowTransparency = true;
            AutoClose = true;
            DropShadowEnabled = true;

            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;

            Renderer = new ContextMenuStripRenderer(this);

            // Setup animation timer (16ms ≈ 60fps)
            _animationTimer = new System.Windows.Forms.Timer
            {
                Interval = 16
            };
            _animationTimer.Tick += AnimationTimer_Tick;
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            bool needsRedraw = false;
            DateTime now = DateTime.Now;

            foreach (var state in _itemStates.Values)
            {
                if (state.NeedsUpdate)
                {
                    float deltaTime = (float)(now - state.LastUpdate).TotalSeconds;
                    state.LastUpdate = now;
                    state.Update(deltaTime);

                    if (state.NeedsUpdate) needsRedraw = true;
                }
            }

            // Stop timer if all animations are complete
            if (!needsRedraw)
            {
                _animationTimer.Stop();
            }

            if (needsRedraw) Invalidate();
        }

        public float GetItemAlpha(ToolStripItem item)
        {
            if (item != null && _itemStates.TryGetValue(item, out var state))
            {
                return state.Alpha;
            }
            return 0f;
        }

        protected override void OnItemAdded(ToolStripItemEventArgs e)
        {
            base.OnItemAdded(e);

            if (e.Item is not ToolStripSeparator)
            {
                if (!_itemStates.ContainsKey(e.Item))
                {
                    _itemStates[e.Item] = new AnimatedItemState();
                }

                // Attach mouse events
                e.Item.MouseEnter += Item_MouseEnter;
                e.Item.MouseLeave += Item_MouseLeave;
            }
        }

        protected override void OnItemRemoved(ToolStripItemEventArgs e)
        {
            base.OnItemRemoved(e);

            if (e.Item != null)
            {
                e.Item.MouseEnter -= Item_MouseEnter;
                e.Item.MouseLeave -= Item_MouseLeave;
                _itemStates.Remove(e.Item);
            }
        }

        private void Item_MouseEnter(object sender, EventArgs e)
        {
            if (sender is ToolStripItem item)
            {
                if (_itemStates.TryGetValue(item, out var state))
                {
                    state.TargetAlpha = 1f;
                    state.LastUpdate = DateTime.Now;
                    StartAnimationIfNeeded();
                }
            }
        }

        private void Item_MouseLeave(object sender, EventArgs e)
        {
            if (sender is ToolStripItem item)
            {
                if (_itemStates.TryGetValue(item, out var state))
                {
                    state.TargetAlpha = 0f;
                    state.LastUpdate = DateTime.Now;
                    StartAnimationIfNeeded();
                }
            }
        }

        private void UpdateItemSelection(ToolStripItem item)
        {
            if (item == null || item is ToolStripSeparator) return;

            // Fade out previously selected item
            if (_previouslySelectedItem != null && _previouslySelectedItem != item)
            {
                if (_itemStates.TryGetValue(_previouslySelectedItem, out var prevState))
                {
                    prevState.TargetAlpha = 0f;
                    prevState.LastUpdate = DateTime.Now;
                }
            }

            // Fade in newly selected item
            if (_itemStates.TryGetValue(item, out var state))
            {
                state.TargetAlpha = 1f;
                state.LastUpdate = DateTime.Now;
            }

            _previouslySelectedItem = item;
            StartAnimationIfNeeded();
        }

        private void StartAnimationIfNeeded()
        {
            if (!_animationTimer.Enabled)
            {
                bool anyActive = false;
                foreach (var state in _itemStates.Values)
                {
                    if (state.NeedsUpdate)
                    {
                        anyActive = true;
                        break;
                    }
                }

                if (anyActive)
                {
                    _animationTimer.Start();
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message m, Keys keyData)
        {
            if (m.Msg == (int)User32.WindowsMessage.KeyDown)
            {
                Keys key = keyData & Keys.KeyCode;

                if (key == Keys.Enter)
                {
                    // Click the currently selected item
                    if (_previouslySelectedItem != null && _previouslySelectedItem is not ToolStripSeparator)
                    {
                        _previouslySelectedItem.PerformClick();
                        return true; // Mark as handled
                    }
                }
                else if (key == Keys.Up || key == Keys.Down)
                {
                    int currentIndex = -1;

                    // Find current selected item
                    if (_previouslySelectedItem != null)
                    {
                        currentIndex = Items.IndexOf(_previouslySelectedItem);
                    }
                    else
                    {
                        // If no selection, start from first item
                        currentIndex = -1;
                    }

                    int nextIndex = currentIndex;

                    if (key == Keys.Down)
                    {
                        // Find next non-separator item
                        do
                        {
                            nextIndex++;
                            if (nextIndex >= Items.Count) nextIndex = 0;
                        } while (nextIndex < Items.Count && nextIndex != currentIndex && Items[nextIndex] is ToolStripSeparator);
                    }
                    else if (key == Keys.Up)
                    {
                        // Find previous non-separator item
                        do
                        {
                            nextIndex--;
                            if (nextIndex < 0) nextIndex = Items.Count - 1;
                        } while (nextIndex >= 0 && nextIndex != currentIndex && Items[nextIndex] is ToolStripSeparator);
                    }

                    // Select the item if it's valid
                    if (nextIndex >= 0 && nextIndex < Items.Count && Items[nextIndex] is not ToolStripSeparator)
                    {
                        UpdateItemSelection(Items[nextIndex]);
                        return true; // Mark as handled
                    }
                }
            }

            return base.ProcessCmdKey(ref m, keyData);
        }

        private static readonly TextureBrush Noise = new(Resources.Noise.Fade(0.9f));

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public int ItemHeight { get; set; } = 24;

        int parentLevel = 0;
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            parentLevel = this.Level();
        }

        private Bitmap BlurredBackground;
        private bool _firstOpen = true;

        void UpdateBackdrop()
        {
            if (Width <= 0 || Height <= 0) return;

            BlurredBackground?.Dispose();

            using (Bitmap capture = GraphicsExtensions.CaptureFromScreen(Bounds))
            {
                int downscale = 2;
                int w = Math.Max(1, capture.Width / downscale);
                int h = Math.Max(1, capture.Height / downscale);

                using (Bitmap small = new(w, h))
                {
                    using (Graphics G = Graphics.FromImage(small))
                    {
                        G.InterpolationMode = InterpolationMode.Bilinear;
                        G.DrawImage(capture, 0, 0, w, h);
                    }

                    using (Bitmap blurredSmall = small.Blur(3))
                    {
                        BlurredBackground = new Bitmap(capture.Width, capture.Height);
                        using (Graphics G = Graphics.FromImage(BlurredBackground))
                        {
                            G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            G.DrawImage(capture, 0, 0, Width, Height);
                            G.DrawImage(blurredSmall, 0, 0, Width, Height);
                        }
                    }
                }
            }
        }

        protected override void OnOpening(CancelEventArgs e)
        {
            base.OnOpening(e);
            this.PerformLayout();
            UpdateBackdrop();

            // Reset all item alphas when opening
            foreach (var state in _itemStates.Values)
            {
                state.Alpha = 0f;
                state.TargetAlpha = 0f;
                state.LastUpdate = DateTime.Now;
            }
            _previouslySelectedItem = null;
        }

        protected override void OnOpened(EventArgs e)
        {
            base.OnOpened(e);

            if (_firstOpen && Program.Style.Animations && Handle != IntPtr.Zero)
            {
                _firstOpen = false;
                NativeMethods.User32.ShowWindow(Handle, NativeMethods.User32.SW_HIDE);
                AnimateWindow(Handle, 80, AnimationType | AnimateWindowFlags.AW_ACTIVATE);
                NativeMethods.User32.ShowWindow(Handle, NativeMethods.User32.SW_SHOWNA);
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)User32.WindowsMessage.ShowWindow && Program.Style.Animations && Handle != IntPtr.Zero)
            {
                bool showing = m.WParam.ToInt32() != 0;

                if (!showing || (showing && !_firstOpen))
                {
                    AnimateWindow(Handle, 80, AnimationType | (showing ? AnimateWindowFlags.AW_ACTIVATE : AnimateWindowFlags.AW_HIDE));
                }
            }

            base.WndProc(ref m);
        }

        protected override void OnClosed(ToolStripDropDownClosedEventArgs e)
        {
            base.OnClosed(e);

            _animationTimer.Stop();

            BlurredBackground?.Dispose();
            BlurredBackground = null;

            // Reset all alphas
            foreach (var state in _itemStates.Values)
            {
                state.Alpha = 0f;
                state.TargetAlpha = 0f;
            }
            _previouslySelectedItem = null;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new(0, 0, Width - 1, Height - 1);

            if (BlurredBackground != null)
            {
                G.DrawImage(BlurredBackground, 0, 0, Width, Height);
                G.FillRectangle(Noise, 0, 0, Width, Height);
            }

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            using (SolidBrush br = new(Color.FromArgb(180, scheme.Colors.Back(parentLevel))))
            using (Pen P = new(Color.FromArgb(128, 128, 128, 128)))
            {
                G.FillRoundedRect(br, rect);
                G.DrawRoundedRect(P, rect);
            }

            base.OnPaint(e);
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);

            int maxWidth = 0;
            foreach (ToolStripItem item in Items)
            {
                if (item is ToolStripSeparator) continue;
                if (item.GetPreferredSize(Size.Empty).Width > maxWidth) maxWidth = item.GetPreferredSize(Size.Empty).Width;
            }

            foreach (ToolStripItem item in Items)
            {
                if (item is ToolStripSeparator) continue;
                item.AutoSize = false;
                item.Size = new Size(maxWidth, ItemHeight + 3);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _animationTimer?.Stop();
                _animationTimer?.Dispose();
                BlurredBackground?.Dispose();

                foreach (var item in _itemStates.Keys)
                {
                    if (item != null)
                    {
                        item.MouseEnter -= Item_MouseEnter;
                        item.MouseLeave -= Item_MouseLeave;
                    }
                }
                _itemStates.Clear();
            }
            base.Dispose(disposing);
        }
    }
}