using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using FluentTransitions;

namespace WinPaletter.UI.WP
{
    /// <summary>
    /// A fully transparent control-box button (close / minimize / maximize / restore / help).
    /// <br></br>
    /// <br></br>Rendering uses two bitmap layers sourced from the private static <see cref="ControlBoxBitmaps"/>
    /// <br></br>singleton, which is lazily initialised on first use and shared across all instances:
    /// <br></br>  - Overlay    : the glyph icon. Never animated. Regenerated when button type, DPI, dark-mode or active-window state changes.
    /// <br></br> - Background : the chrome behind the glyph. Regenerated when button type, mouse state, DPI, dark-mode, active-window, close-layout or control size changes.
    /// <br></br> Cross-fades between mouse states via FluentTransitions CriticalDamping on the public <see cref="FadeAlpha"/> property.
    /// <br></br>
    /// <br></br>Call <see cref="ReloadBitmaps"/> after a visual-style change to discard and re-create the shared atlas so all existing instances pick up the new theme automatically.
    /// <br></br>
    /// <br></br> The control base is completely transparent — no background is ever painted — so both layers composite correctly over whatever is rendered beneath the parent window.
    /// </summary>
    public sealed class ControlBoxButton : Control
    {
        private static ControlBoxBitmaps _sharedBitmaps = null;

        private static ControlBoxBitmaps SharedBitmaps
        {
            get
            {
                if (_sharedBitmaps == null) _sharedBitmaps = new();
                return _sharedBitmaps;
            }
        }

        /// <summary>
        /// Disposes the shared <see cref="ControlBoxBitmaps"/> atlas and forces all existing <see cref="ControlBoxButton"/> instances to regenerate their cached
        /// bitmaps from the new visual style on their next paint cycle.
        /// <br></br>Call this from a <c>WM_THEMECHANGED</c> / <c>WM_DWMCOMPOSITIONCHANGED</c> handler.
        /// </summary>
        public static void ReloadBitmaps()
        {
            _sharedBitmaps?.Dispose();
            _sharedBitmaps = new();
        }

        private static readonly TimeSpan FadeDuration = TimeSpan.FromMilliseconds(160);

        private ControlBoxBitmaps.CaptionButton _button = ControlBoxBitmaps.CaptionButton.Close;
        private ControlBoxBitmaps.ButtonState _mouseState = ControlBoxBitmaps.ButtonState.Normal;
        private ControlBoxBitmaps.GlyphDpi _glyphDpi = ControlBoxBitmaps.GlyphDpi.Dpi96;
        private ControlBoxBitmaps.CloseButtonLayout _closeLayout = ControlBoxBitmaps.CloseButtonLayout.WithMinMax;
        private bool _isActive = true;

        // Per-instance cached bitmaps — owned and disposed by this control
        private Bitmap _overlayBmp = null;   // glyph icon layer
        private Bitmap _bgBmp = null;   // chrome layer for the current state
        private Bitmap _fadeBmp = null;   // chrome layer snapshot from the previous state

        private float _fadeAlpha = 0f;

        /// <summary>
        /// Opacity of the outgoing (previous) background chrome layer.
        /// <br></br> FluentTransitions CriticalDamping drives this from 1 → 0 on each mouse-state change. The incoming background is drawn fully opaque beneath it, so it appears to reveal itself as the old layer fades away.
        /// </summary>
        public float FadeAlpha
        {
            get => _fadeAlpha;
            set
            {
                _fadeAlpha = Math.Max(0f, Math.Min(1f, value));

                if (_fadeAlpha <= 0f)
                {
                    _fadeBmp?.Dispose();
                    _fadeBmp = null;
                }

                Invalidate();
            }
        }

        public ControlBoxButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw, true);

            BackColor = Color.Transparent;
            Size = new Size(46, 32);

            UI.Style.Config.DarkModeChanged += OnDarkModeChanged;
        }

        /// <summary>Which caption button this control represents.</summary>
        public ControlBoxBitmaps.CaptionButton Button
        {
            get => _button;
            set
            {
                if (_button == value) return;
                _button = value;
                RegenerateOverlay();
                StartBackgroundFade();
            }
        }

        /// <summary>DPI scale used to select the correct glyph strip from the atlas.</summary>
        public ControlBoxBitmaps.GlyphDpi GlyphDpi
        {
            get => _glyphDpi;
            set
            {
                if (_glyphDpi == value) return;
                _glyphDpi = value;
                // DPI changes are instant — no fade, both layers regenerate immediately
                RegenerateOverlay();
                RegenerateBackground();
                Invalidate();
            }
        }

        /// <summary>
        /// Vista/7 only: whether the close button sits next to min/max or is alone.
        /// Has no visible effect on Win8+.
        /// </summary>
        public ControlBoxBitmaps.CloseButtonLayout CloseLayout
        {
            get => _closeLayout;
            set
            {
                if (_closeLayout == value) return;
                _closeLayout = value;
                StartBackgroundFade();
            }
        }

        /// <summary>
        /// Whether the parent window is the active (foreground) window.
        /// Affects both the background chrome part ID and the glyph selection.
        /// </summary>
        public bool IsActiveWindow
        {
            get => _isActive;
            set
            {
                if (_isActive == value) return;
                _isActive = value;
                RegenerateOverlay();
                StartBackgroundFade();
            }
        }

        /// <summary>
        /// Push a new mouse interaction state. Triggers a CriticalDamping fade from
        /// the current background chrome to the new state's chrome.
        /// Can also be called externally, e.G. from a parent form's WndProc when
        /// handling non-client-area WM_NCHITTEST-driven hover tracking.
        /// </summary>
        public void SetMouseState(ControlBoxBitmaps.ButtonState state)
        {
            if (_mouseState == state) return;
            _mouseState = state;
            StartBackgroundFade();
        }

        private void OnDarkModeChanged()
        {
            // Both layers depend on dark mode — overlay for glyph variant selection,
            // background for the _Dark part IDs in the atlas (Win10/11).
            RegenerateOverlay();
            RegenerateBackground();
            StartBackgroundFade();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (_mouseState != ControlBoxBitmaps.ButtonState.Disabled) SetMouseState(ControlBoxBitmaps.ButtonState.Hot);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (_mouseState != ControlBoxBitmaps.ButtonState.Disabled) SetMouseState(ControlBoxBitmaps.ButtonState.Normal);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left && _mouseState != ControlBoxBitmaps.ButtonState.Disabled) SetMouseState(ControlBoxBitmaps.ButtonState.Pressed);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (_mouseState != ControlBoxBitmaps.ButtonState.Disabled)
            {
                SetMouseState(ClientRectangle.Contains(e.Location) ? ControlBoxBitmaps.ButtonState.Hot : ControlBoxBitmaps.ButtonState.Normal);
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_THEMECHANGED = 0x031A;
            if (m.Msg == WM_THEMECHANGED)
            {
                ReloadBitmaps();
                RegenerateOverlay();
                RegenerateBackground();
                Invalidate();
            }
            base.WndProc(ref m);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Leave it empty to make control background transparent
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            Rectangle dest = ClientRectangle;

            // Layer 1 — outgoing chrome (fades out as FadeAlpha → 0)
            if (_fadeBmp != null && _fadeAlpha > 0f)
            {
                using ImageAttributes ia = BuildAlphaAttributes(_fadeAlpha); G.DrawImage(_fadeBmp, dest, 0, 0, _fadeBmp.Width, _fadeBmp.Height, GraphicsUnit.Pixel, ia);
            }

            // Layer 2 — incoming chrome (fully opaque; reveals itself as old layer fades away)
            if (_bgBmp != null) G.DrawImage(_bgBmp, dest);

            // Layer 3 — glyph overlay (always fully opaque, centred, never animated)
            if (_overlayBmp != null)
            {
                int ox = (dest.Width - _overlayBmp.Width) / 2;
                int oy = (dest.Height - _overlayBmp.Height) / 2;
                G.DrawImageUnscaled(_overlayBmp, ox, oy);
            }
        }

        // Bitmap generation — OVERLAY
        // Invalidating parameters: button | glyphDpi | _darkMode | isActive
        private void RegenerateOverlay()
        {
            _overlayBmp?.Dispose();
            _overlayBmp = null;

            using ControlBoxBitmaps.ControlBoxButtonBitmap pair = SharedBitmaps.GetBitmap(_button, _mouseState, _glyphDpi, Program.Style.DarkMode, _closeLayout, _isActive);
            // Overlay is the glyph only — background half is discarded
            _overlayBmp = pair.Overlay != null ? SafeClone(pair.Overlay) : null;
        }

        // Bitmap generation — BACKGROUND
        // Invalidating parameters: button | mouseState | glyphDpi | _darkMode | isActive | closeLayout | ClientSize
        //
        // NOTE: On Win10/11, the atlas contains separate background strips for dark mode
        // (e.G. Button_Active_bk_Dark vs Button_Active_bk). The darkGlyph parameter passed to ControlBoxBitmaps.GetBitmap routes ResolveBackgroundPartId to those dark strips,
        // so _darkMode must be forwarded here — not only to the overlay — to get visually distinct backgrounds in light vs dark mode.
        private void RegenerateBackground()
        {
            _bgBmp?.Dispose();
            _bgBmp = null;

            Size targetSize = ClientSize.Width > 0 && ClientSize.Height > 0 ? ClientSize : Size;

            using (ControlBoxBitmaps.ControlBoxButtonBitmap pair = SharedBitmaps.GetBitmap(_button, _mouseState, targetSize, _glyphDpi, Program.Style.DarkMode, _closeLayout, _isActive))
            {
                // Background is the chrome only — overlay half is discarded
                _bgBmp = pair.Background != null ? SafeClone(pair.Background) : null;
            }
        }

        private void StartBackgroundFade()
        {
            // Snapshot the current chrome as the outgoing layer before overwriting _bgBmp
            _fadeBmp?.Dispose();
            _fadeBmp = _bgBmp != null ? SafeClone(_bgBmp) : null;

            // Regenerate the incoming chrome for the new state
            RegenerateBackground();

            if (_fadeBmp == null)
            {
                // No outgoing layer to animate — just repaint directly
                Invalidate();
                return;
            }

            // Reset to 1 so any in-flight transition always restarts from fully visible
            _fadeAlpha = 1f;

            // CriticalDamping: reaches 0 as fast as possible without overshooting
            Transition.With(this, nameof(FadeAlpha), 0f).CriticalDamp(FadeDuration);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            RegenerateBackground();
            Invalidate();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            RegenerateOverlay();
            RegenerateBackground();
            Invalidate();
        }

        private static ImageAttributes BuildAlphaAttributes(float alpha)
        {
            float a = Math.Max(0f, Math.Min(1f, alpha));
            float[][] matrix =
            {
                [1f, 0f, 0f, 0f, 0f],
                [0f, 1f, 0f, 0f, 0f],
                [0f, 0f, 1f, 0f, 0f],
                [0f, 0f, 0f, a,  0f],
                [0f, 0f, 0f, 0f, 1f]
            };
            ImageAttributes ia = new();
            ia.SetColorMatrix(new(matrix));
            return ia;
        }

        private static Bitmap SafeClone(Bitmap source)
        {
            if (source == null || !source.IsValid()) return null;
            Bitmap clone = new(source.Width, source.Height, PixelFormat.Format32bppArgb);
            using (Graphics G = Graphics.FromImage(clone)) G.DrawImageUnscaled(source, 0, 0);
            return clone;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UI.Style.Config.DarkModeChanged -= OnDarkModeChanged;
                _overlayBmp?.Dispose();
                _bgBmp?.Dispose();
                _fadeBmp?.Dispose();
                // _sharedBitmaps is static and NOT disposed here
            }
            base.Dispose(disposing);
        }
    }
}