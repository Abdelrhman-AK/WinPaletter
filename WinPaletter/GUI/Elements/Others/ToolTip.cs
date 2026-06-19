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
        private bool CanAnimate => !DesignMode && Program.Style.Animations && this is not null;

        private readonly Dictionary<Control, ToolTipControlData> _controlData = [];
        private readonly Dictionary<Control, Control> _childToParent = [];
        private Control _activeControl;
        private int _activeDuration;
        private bool _relocatingTooltip;
        private const int _relocationThreshold = 5;
        private const int DefaultMaxWidth = 320;
        private readonly List<Control> _trackedAncestors = [];
        private Point _activeOffset;          // offset from control's top-left in screen coordinates
        private bool _explicitPlacement;      // true only for the one popup immediately following an explicit Show() call
        private Point _lastTooltipScreen;     // last known screen position
        private IntPtr _tooltipHwnd = IntPtr.Zero;
        private Bitmap _blurBackground;
        private Rectangle _rectImageSide = Rectangle.Empty;
        private Rectangle _rectTitle = Rectangle.Empty;
        private Rectangle _rectText = Rectangle.Empty;
        private Size _cachedSize = Size.Empty;
        private bool _layoutDirty = true;
        private int _maxWidth = DefaultMaxWidth;

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
            EnsureControlData(control).Text = caption ?? string.Empty;
            base.SetToolTip(control, string.IsNullOrWhiteSpace(caption) ? string.Empty : ".");
            if (control is UI.WP.TextBox tb) SetToolTip(tb.TB, caption);
        }

        public void SetToolTip(Control control, string title, string text, Image image = null, Color badgeColor = default, int duration = 0)
        {
            if (control == null) return;
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

            ToolTipTitle = title ?? string.Empty;
            ToolTipText = text ?? string.Empty;
            Image = image;
            BadgeColor = Color.Empty;

            // Set active control for tracking
            _activeControl = control;
            _activeDuration = duration > 0 ? duration : AutoPopDelay;
            if (_activeDuration <= 0) _activeDuration = 5000;

            // Store the explicit location offset. OnTooltipPopup fires synchronously inside
            // base.Show() below, consumes this offset once, captures the blur snapshot and
            // starts tracking - so none of that needs to be duplicated here.
            _activeOffset = locationRelativeToControl;
            _explicitPlacement = true;

            base.Show(".", control, locationRelativeToControl.X, locationRelativeToControl.Y, _activeDuration);
        }

        public void Show(Control control, string title, string text, Color badgeColor, Point locationRelativeToControl, int duration = 5000)
        {
            if (control == null) return;

            ToolTipTitle = title ?? string.Empty;
            ToolTipText = text ?? string.Empty;
            Image = null;
            BadgeColor = badgeColor;

            // Set active control for tracking
            _activeControl = control;
            _activeDuration = duration > 0 ? duration : AutoPopDelay;
            if (_activeDuration <= 0) _activeDuration = 5000;

            // Store the explicit location offset. OnTooltipPopup fires synchronously inside
            // base.Show() below, consumes this offset once, captures the blur snapshot and
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

        private IntPtr FindTooltipHwnd()
        {
            IntPtr found = IntPtr.Zero;
            int tid = NativeMethods.Kernel32.GetCurrentThreadId();

            NativeMethods.User32.EnumThreadWindows(tid, (hWnd, _) =>
            {
                System.Text.StringBuilder cls = new(64);
                NativeMethods.User32.GetClassName(hWnd, cls, 64);
                if (cls.ToString() == "tooltips_class32")
                {
                    found = hWnd;
                    return false;   // stop enumeration — we want our only tooltip
                }
                return true;
            }, IntPtr.Zero);

            return found;
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

            System.Windows.Forms.Form form = control.FindForm();

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

            // Nothing to relocate if the tooltip was never shown, or it is not currently
            // visible (already auto-hidden, dismissed, etc). Bails out before doing any
            // PointToScreen/Hide/Show work, which also avoids needless blur recapture.
            if (_tooltipHwnd == IntPtr.Zero || !NativeMethods.User32.IsWindowVisible(_tooltipHwnd))
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

                // Only relocate if the tooltip moved more than the threshold distance.
                // This prevents shuttering from tiny position changes during form dragging.
                int deltaX = Math.Abs(newScreen.X - _lastTooltipScreen.X);
                int deltaY = Math.Abs(newScreen.Y - _lastTooltipScreen.Y);

                if (deltaX < _relocationThreshold && deltaY < _relocationThreshold) return;

                _lastTooltipScreen = newScreen;

                // Ensure we have a valid duration.
                int duration = _activeDuration > 0 ? _activeDuration : AutoPopDelay;
                if (duration <= 0) duration = 5000;

                // Hide current tooltip
                Hide(_activeControl);

                // Recapture blur for new position
                if (CanAnimate)
                {
                    EnsureLayout();
                    CaptureBlur(new Rectangle(newScreen, _cachedSize));
                }

                // Show tooltip at new position with valid duration
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
                _tooltipHwnd = IntPtr.Zero;
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
                            _blurBackground = new Bitmap(capture.Width, capture.Height);
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
            const TextFormatFlags measureFlags = TextFormatFlags.WordBreak | TextFormatFlags.Left | TextFormatFlags.NoPrefix;

            bool hasImage = _image != null;
            bool hasBadge = !hasImage && _badgeColor != Color.Empty;

            int iconColumnWidth = 0;
            if (hasImage || hasBadge)
            {
                int iconW = hasImage ? _image.Width : 24;
                iconColumnWidth = iconW + pad * 2 + pad * 3;
            }

            // Cap the text measurement box to MaxWidth (minus room for the icon column),
            // so long strings wrap onto multiple lines instead of stretching the tooltip.
            int availableTextWidth = Math.Max(40, _maxWidth - iconColumnWidth - pad * 2);
            Size proposedSize = new(availableTextWidth, int.MaxValue);

            Size sizeTitle = string.IsNullOrWhiteSpace(_toolTipTitle) ? Size.Empty : TextRenderer.MeasureText(_toolTipTitle, _font_Title, proposedSize, measureFlags);
            Size sizeText = string.IsNullOrWhiteSpace(_toolTipText) ? Size.Empty : TextRenderer.MeasureText(_toolTipText, _font, proposedSize, measureFlags);

            int textBlockWidth = Math.Min(availableTextWidth, Math.Max(sizeTitle.Width, sizeText.Width));

            _rectTitle = sizeTitle == Size.Empty ? Rectangle.Empty : new(pad, pad, textBlockWidth, sizeTitle.Height);
            _rectText = sizeText == Size.Empty ? Rectangle.Empty : new(pad, pad, textBlockWidth, sizeText.Height);

            if (!_rectTitle.IsEmpty && !_rectText.IsEmpty) _rectText.Y = _rectTitle.Bottom;

            Rectangle combined;
            if (_rectTitle.IsEmpty && _rectText.IsEmpty) combined = new(pad, pad, 10, 10);
            else if (_rectTitle.IsEmpty) combined = _rectText;
            else if (_rectText.IsEmpty) combined = _rectTitle;
            else combined = Rectangle.Union(_rectTitle, _rectText);

            if (hasImage || hasBadge)
            {
                int iconW = hasImage ? _image.Width : 24;
                int iconH = hasImage ? _image.Height : 24;

                _rectImageSide = new(pad, pad, iconW + pad * 2, iconH + pad * 2);

                int textX = _rectImageSide.Right + pad * 3;
                combined.X = textX;
                _rectTitle.X = textX;
                _rectText.X = textX;

                combined.Height = Math.Max(combined.Height, _rectImageSide.Height);
                combined = Rectangle.Union(combined, _rectImageSide);
                combined.Width += pad;
            }
            else
            {
                _rectImageSide = Rectangle.Empty;
            }

            combined.Height += pad * 2;
            combined.Width = Math.Min(combined.Width, _maxWidth);

            if (!_rectTitle.IsEmpty && _rectText.IsEmpty) { _rectTitle.Y = 0; _rectTitle.Height = combined.Height; }
            else if (_rectTitle.IsEmpty && !_rectText.IsEmpty) { _rectText.Y = 0; _rectText.Height = combined.Height; }

            _cachedSize = combined.Size;
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

            // Ensure valid duration
            if (_activeDuration <= 0)
            {
                _activeDuration = AutoPopDelay > 0 ? AutoPopDelay : 5000;
            }

            // Resolve the screen offset for THIS popup. _explicitPlacement is true only for the
            // single popup that immediately follows a call to Show(); every other popup (normal
            // hover tooltips, and any popup after that) recomputes the cursor-relative offset
            // from scratch, instead of reusing whatever was left over from a previous session.
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

            // Capture the software-blur snapshot before the tooltip window paints over the screen
            // content. This runs on every popup (not only the first), so the backdrop always
            // matches what is actually behind the tooltip right now.
            if (CanAnimate)
            {
                Point screenTip;
                if (_activeControl != null)
                {
                    Point controlScreenPos = _activeControl.PointToScreen(Point.Empty);
                    screenTip = new Point(controlScreenPos.X + _activeOffset.X, controlScreenPos.Y + _activeOffset.Y);
                }
                else
                {
                    screenTip = new Point(Cursor.Position.X + 16, Cursor.Position.Y + 16);
                }
                CaptureBlur(new Rectangle(screenTip, _cachedSize));
            }

            e.ToolTipSize = _cachedSize;

            // The native tooltip HWND is created once and reused for the lifetime of this
            // component, so only enumerate thread windows to find it when we don't already
            // have it cached - avoids a full EnumThreadWindows pass on every single popup.
            if (_tooltipHwnd == IntPtr.Zero) _tooltipHwnd = FindTooltipHwnd();

            if (_activeControl != null)
            {
                Point controlScreenPos = _activeControl.PointToScreen(Point.Empty);
                _lastTooltipScreen = new Point(controlScreenPos.X + _activeOffset.X, controlScreenPos.Y + _activeOffset.Y);
                StartFollowingControl(_activeControl);
            }
        }

        private void OnTooltipDraw(object sender, DrawToolTipEventArgs e)
        {
            if (!TryLoadControlData(e.AssociatedControl))
            {
                string native = e.AssociatedControl != null ? GetToolTip(e.AssociatedControl) : string.Empty;
                if (!string.IsNullOrWhiteSpace(native) && string.IsNullOrWhiteSpace(_toolTipText) && string.IsNullOrWhiteSpace(_toolTipTitle))
                    ToolTipText = native;
            }

            // If Popup didn't manage to grab the HWND (race), try again here.
            if (_tooltipHwnd == IntPtr.Zero) _tooltipHwnd = FindTooltipHwnd();

            EnsureLayout();

            Graphics G = e.Graphics;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            // Resolve colours — if blur is on, alpha < 255 so the blurred background shows through the fill.
            int fillAlpha = CanAnimate ? 125 : 255;
            int borderAlpha = CanAnimate ? 180 : 255;

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