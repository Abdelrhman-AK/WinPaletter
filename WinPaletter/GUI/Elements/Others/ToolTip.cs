using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    [ProvideProperty("ToolTipText", typeof(Control))]
    [ProvideProperty("ToolTipTitle", typeof(Control))]
    public class ToolTip : System.Windows.Forms.ToolTip, IExtenderProvider
    {
        private sealed class ToolTipNativeWindow : NativeWindow
        {
            private readonly ToolTip _owner;

            internal ToolTipNativeWindow(ToolTip owner, IntPtr handle)
            {
                _owner = owner;
                AssignHandle(handle);
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == (int)NativeMethods.User32.WindowsMessage.Destroy) ReleaseHandle();
                base.WndProc(ref m);
            }
        }

        private bool CanAnimate => !DesignMode && Program.Style.Animations && this is not null;

        private readonly Dictionary<Control, ToolTipControlData> _controlData = [];
        private readonly Dictionary<Control, Control> _childToParent = [];
        private Control _activeControl;
        private int _activeDuration;
        private bool _relocatingTooltip;
        private const int _relocationThreshold = 5;
        private const int DefaultMaxWidth = 400;
        private readonly List<Control> _trackedAncestors = [];
        private Point _activeOffset;                        // offset from control's top-left in screen coordinates
        private bool _explicitPlacement;                    // true only for the one popup immediately following an explicit Show() call
        private Point _lastTooltipScreen;                   // last known screen position
        private bool _blurPendingForCurrentPopup;           // true once a popup has been requested and the blur backdrop still needs (re)capturing
        private Bitmap _blurBackground;
        private Rectangle _rectImageSide = Rectangle.Empty;
        private Rectangle _rectTitle = Rectangle.Empty;
        private Rectangle _rectText = Rectangle.Empty;
        private Size _cachedSize = Size.Empty;
        private bool _layoutDirty = true;
        private int _maxWidth = DefaultMaxWidth;
        private ToolTipNativeWindow _nativeSubclass;
        private bool _fadeInPending;
        private System.Windows.Forms.Timer _fadeOutTimer;
        private int _fadeOutStep;
        private IntPtr _fadingHwnd;
        private const int FadeSteps = 12;
        private const int FadeOutInterval = 10; // ms per step → ~120ms total
        private const int FadeInInterval = 12;  // ms per step → ~150ms total

        // System.Windows.Forms.ToolTip does not expose its native common-control window handle publicly - the "Handle" property on the base class is private, so it isn't visible
        // from a derived class. Reflection is the only way to reach it; reading the private property (rather than the underlying field) also preserves its create-on-demand
        // behaviour, the same way Control.Handle would.
        private static readonly System.Reflection.PropertyInfo _nativeHandleProperty = typeof(System.Windows.Forms.ToolTip).GetProperty("Handle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        private IntPtr Handle
        {
            get
            {
                try
                {
                    return _nativeHandleProperty != null ? (IntPtr)_nativeHandleProperty.GetValue(this, null) : IntPtr.Zero;
                }
                catch
                {
                    return IntPtr.Zero;
                }
            }
        }

        internal sealed class ToolTipControlData
        {
            public string Title { get; set; } = string.Empty;
            public string Text { get; set; } = string.Empty;
            public Image Image { get; set; }
            public Color BadgeColor { get; set; } = Color.Empty;
            public int Duration { get; set; } = 0;   // 0 = use AutoPopDelay
        }

        public ToolTip()
        {
            OwnerDraw = true;
            Draw += OnTooltipDraw;
            Popup += OnTooltipPopup;

            _font = BuildDefaultFont(9f, FontStyle.Regular);
            _font_Title = BuildDefaultFont(9f, FontStyle.Bold);
        }

        public ToolTip(IContainer container) : this()
        {
            container?.Add(this);
        }

        public new void SetToolTip(Control control, string caption)
        {
            if (control == null) return;

            // Marshal to the UI thread - GDI/window operations triggered downstream (Popup, Draw, blur capture) require it, and calling in from a background thread without
            // this guard is what produces a blank/empty tooltip.
            if (control.IsHandleCreated && control.InvokeRequired)
            {
                control.Invoke(new Action(() => SetToolTip(control, caption)));
                return;
            }

            EnsureControlData(control).Text = caption ?? string.Empty;
            base.SetToolTip(control, string.IsNullOrWhiteSpace(caption) ? string.Empty : ".");
            if (control is UI.WP.TextBox tb) SetToolTip(tb.TB, caption);
        }

        public void SetToolTip(Control control, string title, string text, Image image = null, Color badgeColor = default, int duration = 0)
        {
            if (control == null) return;

            if (control.IsHandleCreated && control.InvokeRequired)
            {
                control.Invoke(new Action(() => SetToolTip(control, title, text, image, badgeColor, duration)));
                return;
            }

            ToolTipControlData d = EnsureControlData(control);
            d.Title = title ?? string.Empty;
            d.Text = text ?? string.Empty;
            d.Image = image;
            d.BadgeColor = badgeColor;
            d.Duration = duration;
            bool hasContent = !string.IsNullOrWhiteSpace(title) || !string.IsNullOrWhiteSpace(text);
            base.SetToolTip(control, hasContent ? "." : string.Empty);
            if (control is UI.WP.TextBox tb) SetToolTip(tb.TB, title, text, image, badgeColor, duration);
        }

        public void Show(Control control, string title, string text, Image image, Point locationRelativeToControl, int duration = 5000)
        {
            if (control == null) return;

            if (control.IsHandleCreated && control.InvokeRequired)
            {
                control.Invoke(new Action(() => Show(control, title, text, image, locationRelativeToControl, duration)));
                return;
            }

            ToolTipTitle = title ?? string.Empty;
            ToolTipText = text ?? string.Empty;
            Image = image;
            BadgeColor = Color.Empty;

            // Set active control for tracking
            _activeControl = control;
            _activeDuration = duration > 0 ? duration : AutoPopDelay;
            if (_activeDuration <= 0) _activeDuration = 5000;

            // Store the explicit location offset. OnTooltipPopup fires synchronously inside base.Show() below, consumes this offset once, captures the blur snapshot and
            // starts tracking - so none of that needs to be duplicated here.
            _activeOffset = locationRelativeToControl;
            _explicitPlacement = true;

            base.Show(".", control, locationRelativeToControl.X, locationRelativeToControl.Y, _activeDuration);
        }

        public void Show(Control control, string title, string text, Color badgeColor, Point locationRelativeToControl, int duration = 5000)
        {
            if (control == null) return;

            if (control.IsHandleCreated && control.InvokeRequired)
            {
                control.Invoke(new Action(() => Show(control, title, text, badgeColor, locationRelativeToControl, duration)));
                return;
            }

            ToolTipTitle = title ?? string.Empty;
            ToolTipText = text ?? string.Empty;
            Image = null;
            BadgeColor = badgeColor;

            // Set active control for tracking
            _activeControl = control;
            _activeDuration = duration > 0 ? duration : AutoPopDelay;
            if (_activeDuration <= 0) _activeDuration = 5000;

            // Store the explicit location offset. OnTooltipPopup fires synchronously inside base.Show() below, consumes this offset once, captures the blur snapshot and
            // starts tracking - so none of that needs to be duplicated here.
            _activeOffset = locationRelativeToControl;
            _explicitPlacement = true;

            base.Show(".", control, locationRelativeToControl.X, locationRelativeToControl.Y, _activeDuration);
        }

        private static Font BuildDefaultFont(float size, FontStyle style)
        {
            string face = Fonts.Exists("Segoe UI") ? "Segoe UI" : "Tahoma";
            float sz = (face == "Tahoma" && size == 9f) ? 8.25f : size;
            return new Font(face, sz, style);
        }

        private bool TryLoadControlData(Control queryControl)
        {
            if (queryControl == null) return false;

            if (_controlData.TryGetValue(queryControl, out ToolTipControlData d))
            { ApplyControlData(d, queryControl); return true; }

            if (_childToParent.TryGetValue(queryControl, out Control logical)
                && _controlData.TryGetValue(logical, out d))
            { ApplyControlData(d, logical); return true; }

            Control cursor = queryControl.Parent;
            while (cursor != null)
            {
                if (_controlData.TryGetValue(cursor, out d))
                { ApplyControlData(d, cursor); return true; }
                cursor = cursor.Parent;
            }

            return false;
        }

        private void ApplyControlData(ToolTipControlData d, Control logicalControl)
        {
            ToolTipTitle = d.Title;
            ToolTipText = d.Text;
            Image = d.Image;
            BadgeColor = d.BadgeColor;
            _activeControl = logicalControl;
            _activeDuration = d.Duration > 0 ? d.Duration : AutoPopDelay;
            if (_activeDuration <= 0) _activeDuration = 5000;
        }

        private void StartFollowingControl(Control control)
        {
            StopFollowingControl();

            Control c = control;

            while (c != null)
            {
                c.LocationChanged += OnTrackedLocationChanged;
                c.VisibleChanged += OnTrackedVisibilityChanged;

                _trackedAncestors.Add(c);

                c = c.Parent;
            }

            // control.FindForm() only walks the managed Parent chain. When a "child" form is embedded into a TabPage by reparenting its native window directly (SetParent)
            // rather than through the WinForms Parent property, that managed chain stops at the embedded form and never reaches the real outer/main form, so the outer
            // form's Move/Resize never gets hooked and the tooltip stops following it. Resolving the true top-level window through the native ancestor chain and
            // mapping it back to its managed Control sidesteps that, since GetAncestor always reflects the real Win32 parent/child relationship regardless of how it was set.
            System.Windows.Forms.Form form = control.FindForm();

            if (control.IsHandleCreated)
            {
                IntPtr rootHwnd = NativeMethods.User32.GetAncestor(control.Handle, NativeMethods.User32.GA_ROOT);
                if (rootHwnd != IntPtr.Zero && Control.FromHandle(rootHwnd) is System.Windows.Forms.Form rootForm)
                {
                    form = rootForm;
                }
            }

            if (form != null)
            {
                form.Move += OnFormMoved;
                form.LocationChanged += OnFormMoved;
                form.Resize += OnFormMoved;
                form.SizeChanged += OnFormMoved;
                form.ClientSizeChanged += OnFormMoved;

                if (!_trackedAncestors.Contains(form))
                    _trackedAncestors.Add(form);
            }
        }

        private void StopFollowingControl()
        {
            foreach (Control c in _trackedAncestors)
            {
                if (c == null) continue;
                c.LocationChanged -= OnTrackedLocationChanged;
                c.VisibleChanged -= OnTrackedVisibilityChanged;

                if (c is System.Windows.Forms.Form form)
                {
                    form.Move -= OnFormMoved;
                    form.LocationChanged -= OnFormMoved;
                    form.Resize -= OnFormMoved;
                    form.SizeChanged -= OnFormMoved;
                    form.ClientSizeChanged -= OnFormMoved;
                }
            }

            _trackedAncestors.Clear();
        }

        private void OnFormMoved(object sender, EventArgs e)
        {
            OnTrackedLocationChanged(sender, e);
        }

        private void OnTrackedLocationChanged(object sender, EventArgs e)
        {
            if (_relocatingTooltip) return;

            // Nothing to relocate if the tooltip was never shown, or it is not currently visible (already auto-hidden, dismissed, etc). Bails out before doing any
            // PointToScreen/Hide/Show work, which also avoids needless blur recapture.
            if (Handle == IntPtr.Zero || !NativeMethods.User32.IsWindowVisible(Handle))
            {
                return;
            }

            _relocatingTooltip = true;

            try
            {
                if (_activeControl == null || _activeControl.IsDisposed)
                    return;

                Point newScreen;

                try
                {
                    newScreen = _activeControl.PointToScreen(_activeOffset);
                }
                catch
                {
                    return;
                }

                // Only relocate if the tooltip moved more than the threshold distance. This prevents shuttering from tiny position changes during form dragging.
                int deltaX = Math.Abs(newScreen.X - _lastTooltipScreen.X);
                int deltaY = Math.Abs(newScreen.Y - _lastTooltipScreen.Y);

                if (deltaX < _relocationThreshold && deltaY < _relocationThreshold) return;

                _lastTooltipScreen = newScreen;

                // Ensure we have a valid duration.
                int duration = _activeDuration > 0 ? _activeDuration : AutoPopDelay;
                if (duration <= 0) duration = 5000;

                // Hide current tooltip
                Hide(_activeControl);

                // Re-show at the same anchored offset. Mark this as an explicit placement so the upcoming Popup keeps the exact offset instead of recomputing one from
                // the current cursor position (which is what was making the tooltip drift to the wrong place instead of cleanly following the form). The blur backdrop is
                // recaptured from the Draw handler once the window has actually moved to its new screen position.
                _explicitPlacement = true;
                _blurPendingForCurrentPopup = true;
                base.Show(".", _activeControl, _activeOffset.X, _activeOffset.Y, duration);
            }
            finally
            {
                _relocatingTooltip = false;
            }
        }

        private void OnTrackedVisibilityChanged(object sender, EventArgs e)
        {
            if (sender is Control c && !c.Visible)
            {
                Hide(_activeControl ?? c);
                StopFollowingControl();
            }
        }

        private void CaptureBlur(Rectangle screenBounds)
        {
            _blurBackground?.Dispose();
            _blurBackground = null;

            if (screenBounds.Width <= 0 || screenBounds.Height <= 0) return;

            try
            {
                using (Bitmap capture = GraphicsExtensions.CaptureFromScreen(screenBounds))
                {
                    const int downscale = 2;
                    int w = Math.Max(1, capture.Width / downscale);
                    int h = Math.Max(1, capture.Height / downscale);

                    using (Bitmap small = new(w, h))
                    using (Graphics gSmall = Graphics.FromImage(small))
                    {
                        gSmall.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
                        gSmall.DrawImage(capture, 0, 0, w, h);

                        using (Bitmap blurred = small.Blur(3))
                        {
                            _blurBackground = new(capture.Width, capture.Height);
                            using (Graphics gOut = Graphics.FromImage(_blurBackground))
                            {
                                gOut.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                gOut.DrawImage(capture, 0, 0, capture.Width, capture.Height);
                                gOut.DrawImage(blurred, 0, 0, capture.Width, capture.Height);
                            }
                        }
                    }
                }
            }
            catch { /* screen capture may fail in restricted contexts */ }
        }

        private void InvalidateLayout() => _layoutDirty = true;

        private void EnsureLayout()
        {
            if (!_layoutDirty) return;
            _layoutDirty = false;

            const int pad = 2;
            const int vPad = 4; // breathing room above the first text line and below the last one
            const int titleTextGap = 2; // small extra gap between title and text when both are present
            const TextFormatFlags measureFlags = TextFormatFlags.WordBreak | TextFormatFlags.Left | TextFormatFlags.NoPrefix;

            bool hasImage = _image != null;
            bool hasBadge = !hasImage && _badgeColor != Color.Empty;
            bool hasTitleText = !string.IsNullOrWhiteSpace(_toolTipTitle);
            bool hasBodyText = !string.IsNullOrWhiteSpace(_toolTipText);
            bool hasAnyText = hasTitleText || hasBodyText;
            bool hasIcon = hasImage || hasBadge;

            int iconW = hasImage ? _image.Width : 24;
            int iconH = hasImage ? _image.Height : 24;
            int iconColumnWidth = hasIcon ? iconW + pad * 2 + pad * 3 : 0;

            // Cap the text measurement box to MaxWidth (minus room for the icon column), so long strings wrap onto multiple lines instead of stretching the tooltip.
            int availableTextWidth = Math.Max(40, _maxWidth - iconColumnWidth - pad * 2);
            Size proposedSize = new(availableTextWidth, int.MaxValue);

            Size sizeTitle = hasTitleText ? TextRenderer.MeasureText(_toolTipTitle, _font_Title, proposedSize, measureFlags) : Size.Empty;
            Size sizeText = hasBodyText ? TextRenderer.MeasureText(_toolTipText, _font, proposedSize, measureFlags) : Size.Empty;

            int textBlockWidth = Math.Min(availableTextWidth, Math.Max(sizeTitle.Width, sizeText.Width));
            int textX = pad + iconColumnWidth;

            // Stack title above text. Each rect is sized to exactly its own measured height, and the whole block is padded by vPad above the first line
            // and below the last one - so the gap stays identical whether there's a title only, text only, or both. Previously a single-line tooltip stretched its rect
            // to the full tooltip height with zero top padding, dumping all the slack as a gap underneath the text - that inconsistency is what this fixes.
            int cursorY = vPad;

            if (hasTitleText)
            {
                _rectTitle = new Rectangle(textX, cursorY, textBlockWidth, sizeTitle.Height);
                cursorY = _rectTitle.Bottom;

                if (hasBodyText) cursorY += titleTextGap;
            }
            else
            {
                _rectTitle = Rectangle.Empty;
            }

            if (hasBodyText)
            {
                _rectText = new Rectangle(textX, cursorY, textBlockWidth, sizeText.Height);
                cursorY = _rectText.Bottom;
            }
            else
            {
                _rectText = Rectangle.Empty;
            }

            int textContentHeight = cursorY + vPad;

            if (hasIcon)
            {
                _rectImageSide = new(pad, pad, iconW + pad * 2, iconH + pad * 2);
            }
            else
            {
                _rectImageSide = Rectangle.Empty;
            }

            int minHeightForIcon = hasIcon ? iconH + pad * 4 : 0;
            int contentHeight = hasAnyText ? Math.Max(textContentHeight, minHeightForIcon) : Math.Max(10 + vPad * 2, minHeightForIcon);
            int contentWidth = textX + textBlockWidth + pad;

            // If the icon column is taller than the text block, center the text vertically
            // within the extra space instead of leaving it pinned to the top.
            if (hasAnyText && contentHeight > textContentHeight)
            {
                int extra = (contentHeight - textContentHeight) / 2;
                if (!_rectTitle.IsEmpty) _rectTitle.Y += extra;
                if (!_rectText.IsEmpty) _rectText.Y += extra;
            }

            if (contentWidth < 10) contentWidth = 10;
            if (contentHeight < 10) contentHeight = 10;

            _cachedSize = new Size(Math.Min(contentWidth, _maxWidth), contentHeight);
        }

        private void EnsureLayered(IntPtr hwnd)
        {
            if (hwnd == IntPtr.Zero) return;
            Win32Control.AppendExtendedStyle(hwnd, Win32Control.ControlExtendedStyles.Layered);
        }

        private void SetOpacity(IntPtr hwnd, int alpha) // alpha 0-255
        {
            if (hwnd == IntPtr.Zero) return;
            NativeMethods.User32.SetLayeredWindowAttributes(hwnd, 0, (byte)alpha, NativeMethods.User32.LWA_ALPHA);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Size Size
        {
            get { EnsureLayout(); return _cachedSize; }
        }

        private Image _image;

        [DefaultValue(null)]
        [Category("Appearance")]
        public Image Image
        {
            get => _image;
            set
            {
                if (value == _image) return;
                _image = value;
                _imageColor = value != null ? value.AverageColor() : Color.Empty;
                if (value != null) ToolTipIcon = ToolTipIcon.Info;
                InvalidateLayout();
            }
        }

        private Color _imageColor = Color.Empty;

        private Color _badgeColor = Color.Empty;

        [DefaultValue(typeof(Color), "")]
        [Category("Appearance")]
        public Color BadgeColor
        {
            get => _badgeColor;
            set { if (value == _badgeColor) return; _badgeColor = value; InvalidateLayout(); }
        }

        [DefaultValue(DefaultMaxWidth)]
        [Category("Appearance")]
        public int MaxWidth
        {
            get => _maxWidth;
            set
            {
                int v = value > 0 ? value : DefaultMaxWidth;
                if (v == _maxWidth) return;
                _maxWidth = v;
                InvalidateLayout();
            }
        }

        private Font _font;

        [Category("Appearance")]
        public new Font Font
        {
            get => _font;
            set { if (value == _font) return; _font = value ?? BuildDefaultFont(9f, FontStyle.Regular); InvalidateLayout(); }
        }

        private Font _font_Title;

        [Category("Appearance")]
        public Font Font_Title
        {
            get => _font_Title;
            set { if (value == _font_Title) return; _font_Title = value ?? BuildDefaultFont(9f, FontStyle.Bold); InvalidateLayout(); }
        }

        private string _toolTipText = string.Empty;

        [DefaultValue("")]
        [Category("Appearance")]
        public string ToolTipText
        {
            get => _toolTipText;
            set { if (value == _toolTipText) return; _toolTipText = value ?? string.Empty; InvalidateLayout(); }
        }

        private string _toolTipTitle = string.Empty;

        [DefaultValue("")]
        [Category("Appearance")]
        public new string ToolTipTitle
        {
            get => _toolTipTitle;
            set
            {
                if (value == _toolTipTitle) return;
                _toolTipTitle = value ?? string.Empty;
                base.ToolTipTitle = _toolTipTitle;
                InvalidateLayout();
            }
        }

        private void OnTooltipPopup(object sender, PopupEventArgs e)
        {
            if (!TryLoadControlData(e.AssociatedControl))
            {
                string native = e.AssociatedControl != null ? GetToolTip(e.AssociatedControl) : string.Empty;
                if (!string.IsNullOrWhiteSpace(native) && string.IsNullOrWhiteSpace(_toolTipText) && string.IsNullOrWhiteSpace(_toolTipTitle))
                    ToolTipText = native;
            }

            if (_activeDuration <= 0)
                _activeDuration = AutoPopDelay > 0 ? AutoPopDelay : 5000;

            if (_activeControl != null)
            {
                if (_explicitPlacement)
                {
                    _explicitPlacement = false;
                }
                else
                {
                    Point controlScreenPos = _activeControl.PointToScreen(Point.Empty);
                    Point targetScreenPos = new(Cursor.Position.X + 16, Cursor.Position.Y + 16);
                    _activeOffset = new(targetScreenPos.X - controlScreenPos.X, targetScreenPos.Y - controlScreenPos.Y);
                }
            }

            EnsureLayout();
            e.ToolTipSize = _cachedSize;

            _blurPendingForCurrentPopup = true;
            _fadeInPending = true;

            // Schedule fade-out to begin just before AutoPopDelay expires.
            // FadeSteps * FadeOutInterval ms before the tooltip would vanish, start fading.
            if (CanAnimate)
            {
                _fadeOutTimer?.Stop();
                _fadeOutTimer?.Dispose();
                int delay = Math.Max(100, _activeDuration - (FadeSteps * FadeOutInterval));
                _fadeOutTimer = new System.Windows.Forms.Timer { Interval = delay };
                _fadeOutTimer.Tick += OnFadeOutDelayElapsed;
                _fadeOutTimer.Start();
            }

            if (_activeControl != null)
            {
                Point controlScreenPos = _activeControl.PointToScreen(Point.Empty);
                _lastTooltipScreen = new Point(controlScreenPos.X + _activeOffset.X, controlScreenPos.Y + _activeOffset.Y);
                StartFollowingControl(_activeControl);
            }
        }

        private void OnFadeOutDelayElapsed(object sender, EventArgs e)
        {
            _fadeOutTimer.Stop();

            _fadingHwnd = Handle;

            if (_fadingHwnd == IntPtr.Zero || !NativeMethods.User32.IsWindowVisible(_fadingHwnd))
                return;

            _fadeOutStep = FadeSteps;

            _fadeOutTimer.Interval = FadeOutInterval;
            _fadeOutTimer.Tick -= OnFadeOutDelayElapsed;
            _fadeOutTimer.Tick += OnFadeOutStep;
            _fadeOutTimer.Start();
        }

        private void OnFadeOutStep(object sender, EventArgs e)
        {
            if (_fadingHwnd == IntPtr.Zero)
            {
                _fadeOutTimer.Stop();
                return;
            }

            _fadeOutStep--;

            int alpha = (int)(255.0 * _fadeOutStep / FadeSteps);

            if (alpha <= 0)
            {
                _fadeOutTimer.Stop();
                _fadeOutTimer.Tick -= OnFadeOutStep;

                SetOpacity(_fadingHwnd, 0);

                Hide(_activeControl);

                SetOpacity(_fadingHwnd, 255);

                _fadingHwnd = IntPtr.Zero;

                return;
            }

            SetOpacity(_fadingHwnd, alpha);
        }

        private void OnTooltipDraw(object sender, DrawToolTipEventArgs e)
        {
            if (!TryLoadControlData(e.AssociatedControl))
            {
                string native = e.AssociatedControl != null ? GetToolTip(e.AssociatedControl) : string.Empty;
                if (!string.IsNullOrWhiteSpace(native) && string.IsNullOrWhiteSpace(_toolTipText) && string.IsNullOrWhiteSpace(_toolTipTitle))
                    ToolTipText = native;
            }

            EnsureLayout();

            // Capture the blur backdrop exactly once per popup, now that the tooltip window has been moved/sized to its real final screen position. Briefly hiding our own
            // window around the capture guarantees the screenshot shows the real desktop/app content behind it rather than our own (not-yet-painted) tooltip surface.
            // SWP_NOACTIVATE is essential here - plain ShowWindow activates the window by default, which steals focus from its owner (the parent/main form) and was
            // causing it to visibly deactivate then reactivate on every popup.

            // SWP_NOACTIVATE is the important one here - without it, hiding/showing the tooltip's native window steals activation from its owner (the parent/main form), which is what
            // was causing the owner to visibly deactivate then reactivate on every popup.

            if (CanAnimate && _blurPendingForCurrentPopup)
            {
                _blurPendingForCurrentPopup = false;

                IntPtr hwnd = Handle;

                if (hwnd != IntPtr.Zero)
                {
                    EnsureLayered(hwnd);

                    if (_nativeSubclass == null)
                        _nativeSubclass = new ToolTipNativeWindow(this, hwnd);

                    if (NativeMethods.User32.GetWindowRect(hwnd, out NativeMethods.UxTheme.RECT r))
                    {
                        NativeMethods.User32.SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0,
                            NativeMethods.User32.SetWindowsPosition.NoActivate | NativeMethods.User32.SetWindowsPosition.NoMove |
                            NativeMethods.User32.SetWindowsPosition.NoSize | NativeMethods.User32.SetWindowsPosition.NoZOrder |
                            NativeMethods.User32.SetWindowsPosition.HideWindow);

                        CaptureBlur(Rectangle.FromLTRB(r.left, r.top, r.right, r.bottom));

                        // Start fully transparent, then fade in via SetLayeredWindowAttributes steps
                        SetOpacity(hwnd, 0);

                        NativeMethods.User32.SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0,
                            NativeMethods.User32.SetWindowsPosition.NoActivate | NativeMethods.User32.SetWindowsPosition.NoMove |
                            NativeMethods.User32.SetWindowsPosition.NoSize | NativeMethods.User32.SetWindowsPosition.NoZOrder |
                            NativeMethods.User32.SetWindowsPosition.ShowWindow);
                    }

                    if (_fadeInPending)
                    {
                        _fadeInPending = false;
                        int step = 0;
                        IntPtr capturedHwnd = hwnd;
                        System.Windows.Forms.Timer fadeIn = new() { Interval = FadeInInterval };
                        fadeIn.Tick += (s, _) =>
                        {
                            step++;
                            int alpha = (int)(255.0 * step / FadeSteps);
                            if (step >= FadeSteps) alpha = 255;
                            SetOpacity(capturedHwnd, alpha);
                            if (step >= FadeSteps)
                            {
                                ((System.Windows.Forms.Timer)s).Stop();
                                ((System.Windows.Forms.Timer)s).Dispose();
                            }
                        };
                        fadeIn.Start();
                    }
                }
            }
            else
            {
                _blurPendingForCurrentPopup = false;
            }

            Graphics G = e.Graphics;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            // Resolve colours — if blur is on, alpha < 255 so the blurred background shows through the fill.
            int fillAlpha = CanAnimate ? 150 : 255;
            int borderAlpha = CanAnimate ? 200 : 255;

            Color backColor;
            Color foreColor;
            Color borderColor;

            if (_image == null)
            {
                Config.Scheme scheme = Program.Style.Schemes.Main;
                Color rawBack = scheme.Colors.Back_Hover();
                Color rawBorder = scheme.Colors.Line_Hover();
                backColor = Color.FromArgb(fillAlpha, rawBack);
                foreColor = Program.Style.DarkMode ? Color.White : Color.Black;
                borderColor = Color.FromArgb(borderAlpha, rawBorder);
            }
            else
            {
                using (Config.Colors_Collection colors = new(_imageColor, default, Program.Style.DarkMode))
                {
                    backColor = Color.FromArgb(fillAlpha, colors.Back_Checked);
                    foreColor = colors.ForeColor_Accent;
                    borderColor = Color.FromArgb(borderAlpha, colors.Line_Checked);
                }
            }

            BackColor = backColor;
            ForeColor = foreColor;

            // Layer 1: blurred screenshot (drawn first, behind everything)
            if (CanAnimate && _blurBackground != null) G.DrawImage(_blurBackground, 0, 0, e.Bounds.Width, e.Bounds.Height);

            // Layer 2: semi-opaque background + stock border erase
            e.DrawBackground();
            e.DrawBorder();

            // Layer 3: styled border
            using (Pen penBorder = new(borderColor))
            {
                Rectangle borders = new(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
                G.DrawRoundedRectBeveled(penBorder, borders, 1, true);
            }

            // Layer 4: icon / badge column, then text
            DrawIconColumn(G, e.Bounds, foreColor);
            DrawTextContent(G, foreColor);
        }

        private void DrawIconColumn(Graphics G, Rectangle bounds, Color foreColor)
        {
            const int pad = 2;

            if (_image != null)
            {
                Rectangle ri = new(_rectImageSide.X + (_rectImageSide.Width - _image.Width) / 2 + 1, bounds.Y + (bounds.Height - _image.Height) / 2, _image.Width, _image.Height);

                G.DrawImage(_image, ri);

                using (Pen sep = new(Color.FromArgb(50, foreColor)))
                {
                    G.DrawLine(sep, _rectImageSide.Right + pad, bounds.Top + pad * 2, _rectImageSide.Right + pad, bounds.Bottom - pad * 2);
                }
            }
            else if (_badgeColor != Color.Empty)
            {
                Rectangle rb = new(_rectImageSide.X + (_rectImageSide.Width - 24) / 2 + 1, bounds.Y + (bounds.Height - 24) / 2, 24, 24);

                using (SolidBrush br = new(_badgeColor))
                using (Pen pBadge = new(_badgeColor))
                using (Pen pSep = new(Color.FromArgb(50, foreColor)))
                {
                    G.FillRoundedRect(br, rb, 1, true);
                    G.DrawRoundedRectBeveled(pBadge, rb, 1, true);
                    G.DrawLine(pSep, _rectImageSide.Right + pad + 2, bounds.Top + pad * 2, _rectImageSide.Right + pad + 2, bounds.Bottom - pad * 2);
                }
            }
        }

        private void DrawTextContent(Graphics G, Color foreColor)
        {
            const TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.WordBreak | TextFormatFlags.NoPrefix;

            bool hasTitle = !string.IsNullOrWhiteSpace(_toolTipTitle) && !_rectTitle.IsEmpty;
            bool hasText = !string.IsNullOrWhiteSpace(_toolTipText) && !_rectText.IsEmpty;

            if (hasTitle)
                TextRenderer.DrawText(G, _toolTipTitle, _font_Title, _rectTitle, foreColor, Color.Transparent, flags);
            if (hasText)
                TextRenderer.DrawText(G, _toolTipText, _font, _rectText, foreColor, Color.Transparent, flags);
        }

        private ToolTipControlData EnsureControlData(Control control)
        {
            if (!_controlData.TryGetValue(control, out ToolTipControlData d))
            {
                d = new ToolTipControlData();
                _controlData[control] = d;
                control.Disposed += (s, _) =>
                {
                    Control c = (Control)s;
                    _controlData.Remove(c);
                    base.SetToolTip(c, string.Empty);
                };
            }
            return d;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Draw -= OnTooltipDraw;
                Popup -= OnTooltipPopup;

                StopFollowingControl();

                _nativeSubclass?.ReleaseHandle();
                _nativeSubclass = null;

                _fadeOutTimer?.Stop();
                _fadeOutTimer?.Dispose();
                _fadeOutTimer = null;

                _font?.Dispose();
                _font_Title?.Dispose();
                _blurBackground?.Dispose();

                _controlData.Clear();
                _childToParent.Clear();
            }
            base.Dispose(disposing);
        }
    }
}