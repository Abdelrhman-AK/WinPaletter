using FluentTransitions;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.Properties;

namespace WinPaletter.UI.WP
{
    [Description("AnimatedBox with two colors for WinPaletter UI")]
    [DefaultEvent("Click")]
    public class AnimatedBox : ContainerControl
    {
        public AnimatedBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Text = string.Empty;
            BackColor = Color.Transparent;
            SetColors();
        }

        #region Variables
        private readonly Color LineColor = Color.FromArgb(120, 150, 150, 150);
        private Color C1, C2;
        private float _Angle = 0f;
        private bool _Focused = true;
        private static readonly TextureBrush Noise = new(Resources.Noise.Fade(0.9f));

        // Cache for radial alpha mask (faded at peripheries)
        private static byte[] _radialAlphaMask;
        private static int _cachedMaskSize = -1;

        public enum Styles
        {
            SwapColors,
            MixedColors
        }
        #endregion

        #region Properties

        private Color _Color1 = Color.DodgerBlue;
        public Color Color1
        {
            get => _Color1;
            set
            {
                if (value != _Color1)
                {
                    _Color1 = value;
                    SetColors();
                }
            }
        }

        private Color _Color2 = Color.Crimson;
        public Color Color2
        {
            get => _Color2;
            set
            {
                if (value != _Color2)
                {
                    _Color2 = value;
                    SetColors();
                }
            }
        }

        private Color _Color = default;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Color Color
        {
            get
            {
                if (_Color == default)
                {
                    return C1;
                }
                else
                {
                    return _Color;
                }
            }

            set
            {
                if (value != _Color)
                {
                    _Color = value;
                    SetColors();
                }
            }
        }

        private void SetColors()
        {
            C1 = Program.Style.DarkMode ? Color1.Dark(0.15f) : Color1.Light(0.6f);
            C2 = Program.Style.DarkMode ? Color2.Dark(0.15f) : Color2.Light(0.6f);
        }

        public Styles Style { get; set; } = Styles.SwapColors;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; } = string.Empty;

        #endregion

        #region Animator

        private readonly Timer Timer = new() { Enabled = false, Interval = 40 };
        private async void Timer_Tick(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                bool needInvalidate = false;

                if (_Angle + 3f > 360f)
                {
                    _Angle = 0f;

                    if (Style == Styles.SwapColors)
                    {
                        if (Color == C1 || Color == Color1)
                        {
                            await Task.Run(() => Transition.With(this, nameof(Color), C2).CriticalDamp(Program.AnimationSpan));
                        }
                        else
                        {
                            await Task.Run(() => Transition.With(this, nameof(Color), C1).CriticalDamp(Program.AnimationSpan));
                        }
                        needInvalidate = true;
                    }
                }
                else
                {
                    _Angle += 3f;
                    needInvalidate = true;
                }

                if (needInvalidate)
                    Invalidate();
            }
            else
            {
                Timer.Enabled = false;
                Timer.Stop();
            }
        }

        #endregion

        #region Events/Overrides

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (!DesignMode)
            {
                Timer.Tick += Timer_Tick;
                HookFormEvents();
                ParentChanged += AnimatedBox_ParentChanged;
                VisibleChanged += AnimatedBox_VisibleChanged;
                HookParentEvents(Parent);
                UpdateTimerState();
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (!DesignMode)
            {
                Timer.Tick -= Timer_Tick;
                UnhookFormEvents();
                ParentChanged -= AnimatedBox_ParentChanged;
                VisibleChanged -= AnimatedBox_VisibleChanged;
            }

            base.OnHandleDestroyed(e);
        }

        private System.Windows.Forms.Form _hookedForm;

        /// <summary>
        /// Hooks form events for both current form and topmost parent form.
        /// </summary>
        private void HookFormEvents()
        {
            UnhookFormEvents();

            // Start from this control
            Control current = this;
            System.Windows.Forms.Form topForm = null;

            while (current != null)
            {
                if (current is System.Windows.Forms.Form f)
                {
                    topForm = f; // found a Form in the hierarchy
                }

                // Step up the hierarchy
                current = current.Parent;
            }

            // If still null, fallback to FindForm()
            if (topForm == null)
                topForm = FindForm();

            _hookedForm = topForm;

            if (_hookedForm != null)
            {
                // If the form's handle isn't created yet, defer hooking
                if (!_hookedForm.IsHandleCreated)
                {
                    _hookedForm.HandleCreated += (s, e) => HookFormEvents();
                    return; // exit for now; the events will be hooked once handle is ready
                }

                _hookedForm.Activated -= Form_Activated;
                _hookedForm.Deactivate -= Form_Deactivate;
                _hookedForm.Shown -= Form_Shown;
                _hookedForm.Resize -= Form_Resize;

                _hookedForm.Activated += Form_Activated;
                _hookedForm.Deactivate += Form_Deactivate;
                _hookedForm.Shown += Form_Shown;
                _hookedForm.Resize += Form_Resize;
            }
        }

        /// <summary>
        /// Safely unhooks previously attached form event handlers.
        /// </summary>
        private void UnhookFormEvents()
        {
            if (_hookedForm != null)
            {
                _hookedForm.Activated -= Form_Activated;
                _hookedForm.Deactivate -= Form_Deactivate;
                _hookedForm.Shown -= Form_Shown;
                _hookedForm.Resize -= Form_Resize;
                _hookedForm = null;
            }
        }

        /// <summary>
        /// Recursively determines whether the control (or any of its parents)
        /// resides inside an inactive TabPage.
        /// </summary>
        private bool IsInInactiveTabPage()
        {
            Control current = this;

            while (current != null)
            {
                if (current is TabPage page && page.Parent is TabControl tab)
                {
                    if (tab.SelectedTab != page)
                        return true; // inside inactive tab
                }
                current = current.Parent;
            }

            return false;
        }

        /// <summary>
        /// Evaluates all relevant visibility and state conditions to decide
        /// if animation should be active or stopped.
        /// </summary>
        private void UpdateTimerState()
        {
            bool shouldAnimate = Enabled && Visible && _Focused;

            // Stop if inside inactive tab
            if (IsInInactiveTabPage())
                shouldAnimate = false;

            // Stop if any parent is invisible
            Control p = Parent;
            while (shouldAnimate && p != null)
            {
                if (!p.Visible)
                {
                    shouldAnimate = false;
                    break;
                }
                p = p.Parent;
            }

            // Process result
            if (shouldAnimate)
            {
                if (!Timer.Enabled)
                {
                    Timer.Enabled = true;
                    Timer.Start();
                }
            }
            else
            {
                if (Timer.Enabled)
                {
                    Timer.Enabled = false;
                    Timer.Stop();
                }
            }

            Invalidate();
        }

        /// <summary>
        /// Hooks into TabControl events and parent visibility changes dynamically.
        /// </summary>
        private void HookParentEvents(Control control)
        {
            while (control != null)
            {
                // Hook into TabControls
                if (control is TabPage page && page.Parent is TabControl tab)
                {
                    tab.SelectedIndexChanged -= Tab_SelectedIndexChanged;
                    tab.SelectedIndexChanged += Tab_SelectedIndexChanged;
                    tab.Selecting -= Tab_Selecting;
                    tab.Selecting += Tab_Selecting;
                }

                // Hook into any parent visibility change
                control.VisibleChanged -= Parent_VisibleChanged;
                control.VisibleChanged += Parent_VisibleChanged;

                control = control.Parent;
            }
        }

        private void AnimatedBox_ParentChanged(object sender, EventArgs e)
        {
            HookParentEvents(Parent);
            HookFormEvents();
            UpdateTimerState();
        }

        private void Parent_VisibleChanged(object sender, EventArgs e)
        {
            UpdateTimerState();
        }

        private void AnimatedBox_VisibleChanged(object sender, EventArgs e)
        {
            UpdateTimerState();
        }

        private void Tab_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTimerState();
        }

        private void Tab_Selecting(object sender, TabControlCancelEventArgs e)
        {
            // Stop animations immediately when switching starts
            Timer.Stop();
            Timer.Enabled = false;
        }

        private void Form_Activated(object sender, EventArgs e)
        {
            _Focused = true;
            UpdateTimerState();
        }

        private void Form_Deactivate(object sender, EventArgs e)
        {
            _Focused = false;
            UpdateTimerState();
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            _Focused = true;
            SetColors();
            UpdateTimerState();
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            // Stop when minimized, resume when restored
            if (FindForm() is System.Windows.Forms.Form f)
            {
                _Focused = f.WindowState != FormWindowState.Minimized;
                UpdateTimerState();
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            e.Control.DoubleBuffer();

            base.OnControlAdded(e);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Timer?.Dispose();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);

            if (Enabled)
            {
                _Focused = true;
                Timer.Enabled = true;
                Timer.Start();
            }
            else
            {
                _Focused = false;
                Timer.Enabled = false;
                Timer.Stop();
            }
        }

        #endregion

        #region Radial Alpha Mask Cache
        private void EnsureRadialMask(int size)
        {
            if (_cachedMaskSize == size && _radialAlphaMask != null) return;

            int total = size * size;
            _radialAlphaMask = new byte[total];
            float center = size / 2f;
            float maxRadius = center;

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    float dx = x - center;
                    float dy = y - center;
                    float dist = (float)Math.Sqrt(dx * dx + dy * dy);
                    float t = 1f - Math.Min(dist / maxRadius, 1f);
                    // Square the falloff for smoother fade at edges
                    _radialAlphaMask[y * size + x] = (byte)(t * t * 255f);
                }
            }

            _cachedMaskSize = size;
        }
        #endregion

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //Leave it empty to make control background transparent
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighSpeed;
            Rectangle Rect = new(0, 0, Width, Height);
            Rectangle BorderRect = new(Rect.X, Rect.Y, Rect.Width - 1, Rect.Height - 1);

            if (!DesignMode)
            {
                if (_Focused)
                {
                    if (Style == Styles.MixedColors)
                    {
                        // MixedColors: fill whole control with gradient and full noise overlay
                        using (LinearGradientBrush l = new(Rect, C1, C2, _Angle, false))
                        {
                            l.WrapMode = WrapMode.TileFlipXY;

                            if (Dock == DockStyle.None)
                            {
                                G.FillRoundedRect(l, Rect);
                                G.FillRoundedRect(Noise, Rect);
                                using (Pen P = new(LineColor))
                                {
                                    G.DrawRoundedRect(P, BorderRect);
                                }
                            }
                            else
                            {
                                G.FillRectangle(l, Rect);
                                G.FillRectangle(Noise, Rect);
                            }
                        }
                    }
                    else // SwapColors
                    {
                        // SwapColors: gradient with noise following gradient's alpha pattern
                        Color Cx2 = BackColor;

                        using (LinearGradientBrush l = new(Rect, Color, Cx2, _Angle, false))
                        {
                            l.WrapMode = WrapMode.TileFlipXY;

                            if (Dock == DockStyle.None)
                            {
                                G.FillRoundedRect(l, Rect);

                                // Apply noise that follows the gradient's alpha pattern
                                DrawFadedNoise(G, Rect, Noise, Color, Cx2, _Angle);

                                using (Pen P = new(LineColor))
                                {
                                    G.DrawRoundedRect(P, BorderRect);
                                }
                            }
                            else
                            {
                                G.FillRectangle(l, Rect);
                                DrawFadedNoise(G, Rect, Noise, Color, Cx2, _Angle);
                            }
                        }
                    }
                }

                // Apply texture pattern
                using (TextureBrush tb = new(Program.Style.Texture ?? Assets.Store.Pattern1))
                {
                    G.FillRectangle(tb, Rect);
                }
            }
        }

        /// <summary>
        /// Draws noise texture faded according to the gradient's alpha pattern.
        /// The noise is opaque where the gradient is opaque, and transparent where the gradient is transparent.
        /// </summary>
        private void DrawFadedNoise(Graphics g, Rectangle rect, TextureBrush noiseBrush, Color color1, Color color2, float angle)
        {
            int w = rect.Width;
            int h = rect.Height;

            // Create a temporary bitmap for the noise
            using (Bitmap noiseBmp = new(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                // First, create the noise image at full opacity
                using (Bitmap fullNoise = new(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
                {
                    using (Graphics ng = Graphics.FromImage(fullNoise))
                    {
                        ng.Clear(Color.Transparent);
                        noiseBrush.TranslateTransform(rect.X, rect.Y);
                        ng.FillRectangle(noiseBrush, 0, 0, w, h);
                        noiseBrush.ResetTransform();
                    }

                    // Create a bitmap to hold the gradient's alpha values
                    using (Bitmap gradientAlpha = new(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
                    {
                        // Draw the gradient to extract its alpha pattern
                        using (Graphics gg = Graphics.FromImage(gradientAlpha))
                        {
                            gg.Clear(Color.Transparent);
                            using (LinearGradientBrush l = new(rect, color1, color2, angle, false))
                            {
                                l.WrapMode = WrapMode.TileFlipXY;
                                gg.FillRectangle(l, rect);
                            }
                        }

                        // Lock bits for all three bitmaps
                        System.Drawing.Imaging.BitmapData noiseData = fullNoise.LockBits(
                            new Rectangle(0, 0, w, h),
                            System.Drawing.Imaging.ImageLockMode.ReadOnly,
                            System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                        System.Drawing.Imaging.BitmapData gradientData = gradientAlpha.LockBits(
                            new Rectangle(0, 0, w, h),
                            System.Drawing.Imaging.ImageLockMode.ReadOnly,
                            System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                        System.Drawing.Imaging.BitmapData dstData = noiseBmp.LockBits(
                            new Rectangle(0, 0, w, h),
                            System.Drawing.Imaging.ImageLockMode.WriteOnly,
                            System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                        try
                        {
                            int stride = noiseData.Stride;

                            unsafe
                            {
                                byte* noisePtr = (byte*)noiseData.Scan0;
                                byte* gradientPtr = (byte*)gradientData.Scan0;
                                byte* dstPtr = (byte*)dstData.Scan0;

                                for (int y = 0; y < h; y++)
                                {
                                    for (int x = 0; x < w; x++)
                                    {
                                        int index = y * stride + x * 4;

                                        // Copy RGB from noise
                                        dstPtr[index] = noisePtr[index];         // B
                                        dstPtr[index + 1] = noisePtr[index + 1]; // G
                                        dstPtr[index + 2] = noisePtr[index + 2]; // R

                                        // Use gradient's alpha to modulate noise alpha
                                        float gradientAlphaX = gradientPtr[index + 3] / 255f;
                                        float noiseAlpha = noisePtr[index + 3] / 255f;

                                        // Noise alpha follows gradient alpha
                                        float finalAlpha = noiseAlpha * gradientAlphaX;

                                        // Boost visibility slightly
                                        finalAlpha = Math.Min(finalAlpha * 1.5f, 1f);

                                        dstPtr[index + 3] = (byte)(finalAlpha * 255f);
                                    }
                                }
                            }
                        }
                        finally
                        {
                            fullNoise.UnlockBits(noiseData);
                            gradientAlpha.UnlockBits(gradientData);
                            noiseBmp.UnlockBits(dstData);
                        }
                    }
                }

                // Draw the processed noise bitmap
                g.DrawImage(noiseBmp, rect);
            }
        }
    }
}