using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.Properties;
using static WinPaletter.NativeMethods.Shell32;

namespace WinPaletter.UI.Simulation
{
    [Description("Simulated Windows Terminals")]
    [DefaultEvent("Click")]
    public class WinTerminal : Control
    {
        public WinTerminal()
        {
            Text = string.Empty;
            DoubleBuffered = true;
        }

        #region Variables

        private bool IsInDesignMode
        {
            get
            {
                if (DesignMode)
                    return true;

                if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                    return true;

                ISite site = Site;
                if (site != null && site.DesignMode)
                    return true;

                Control parent = Parent;
                while (parent != null)
                {
                    if (parent.Site != null && parent.Site.DesignMode)
                        return true;
                    parent = parent.Parent;
                }

                return false;
            }
        }

        readonly Timer Timer = new() { Enabled = false, Interval = 500 };

        private static TextureBrush Noise;
        private Bitmap adaptedBack;
        private Bitmap adaptedBackBlurred;
        private Bitmap titlebarBlurClone;

        static WinTerminal()
        {
            Noise = new(Resources.Noise.Fade(0.15f));
        }

        ~WinTerminal()
        {
            Noise?.Dispose();
            adaptedBack?.Dispose();
            adaptedBackBlurred?.Dispose();
            titlebarBlurClone?.Dispose();
        }

        private bool tick = false;
        private Image img;

        public enum CursorShape_Enum
        {
            bar,
            doubleUnderscore,
            emptyBox,
            filledBox,
            underscore,
            vintage
        }

        #endregion

        #region Cached GDI Resources

        private static Font _tabTitleFont = new("Segoe UI", 8f, FontStyle.Bold);
        private static Font _tabRegularFont = new("Segoe UI", 8f, FontStyle.Regular);
        private static Font _closeIconFont = new("Segoe MDL2 Assets", 6f, FontStyle.Regular);
        private static Font _iconFont_W11 = new("Segoe Fluent Icons", 12f);
        private static Font _iconFont_W10 = new("Segoe MDL2 Assets", 12f);
        private static StringFormat _sf_tc = ContentAlignment.TopCenter.ToStringFormat();
        private static StringFormat _sf_mc = ContentAlignment.MiddleCenter.ToStringFormat();
        private static StringFormat _sf_ml = ContentAlignment.MiddleLeft.ToStringFormat();
        private static readonly Pen _borderPen = new(Color.FromArgb(45, 45, 45));
        private static readonly Color _transparent = Color.FromArgb(0, 0, 0, 0);

        #endregion

        #region Cached Layout & Colors

        private Rectangle _rect, _rectTitlebar, _rectConsole;
        private RectangleF _rectConsoleText0, _rectConsoleText1, _rectConsoleText2, _rectConsoleCursor;
        private SizeF _s1X, _s2X, _s3X;
        private string _s1, _s2, _s3, _anotherTab;
        private bool _layoutDirty = true;

        private Color _cachedTitlebar, _cachedTitlebarUnfocused, _cachedTabFocused, _cachedTabUnfocused;
        private bool _colorsDirty = true;

        #endregion

        #region Properties

        private float _Opacity = 1f;
        public float Opacity
        {
            get => _Opacity;
            set
            {
                if (value != _Opacity)
                {
                    _Opacity = value;
                    Invalidate();
                }
            }
        }

        private float _OpacityBackImage = 100f;
        public float OpacityBackImage
        {
            get => _OpacityBackImage;
            set
            {
                if (value != _OpacityBackImage)
                {
                    _OpacityBackImage = value;
                    UpdateOpacityBackImageChanged();
                    Invalidate();
                }
            }
        }

        private Image _BackImage;
        public Image BackImage
        {
            get => _BackImage;
            set
            {
                if (value != _BackImage)
                {
                    _BackImage = value;
                    UpdateOpacityBackImageChanged();
                    Invalidate();
                }
            }
        }

        private Color _color_Titlebar = Color.FromArgb(0, 0, 0, 0);
        public Color Color_Titlebar
        {
            get => _color_Titlebar;
            set
            {
                if (value != _color_Titlebar)
                {
                    _color_Titlebar = value;
                    _colorsDirty = true;
                    Invalidate();
                }
            }
        }

        private Color _color_Titlebar_Unfocused = Color.FromArgb(0, 0, 0, 0);
        public Color Color_Titlebar_Unfocused
        {
            get => _color_Titlebar_Unfocused;
            set
            {
                if (value != _color_Titlebar_Unfocused)
                {
                    _color_Titlebar_Unfocused = value;
                    _colorsDirty = true;
                    Invalidate();
                }
            }
        }

        private Color _color_TabFocused = Color.FromArgb(0, 0, 0, 0);
        public Color Color_TabFocused
        {
            get => _color_TabFocused;
            set
            {
                if (value != _color_TabFocused)
                {
                    _color_TabFocused = value;
                    _colorsDirty = true;
                    Invalidate();
                }
            }
        }

        private Color _color_TabUnFocused = Color.FromArgb(0, 0, 0, 0);
        public Color Color_TabUnFocused
        {
            get => _color_TabUnFocused;
            set
            {
                if (value != _color_TabUnFocused)
                {
                    _color_TabUnFocused = value;
                    _colorsDirty = true;
                    Invalidate();
                }
            }
        }

        private Color _color_Background = Color.Black;
        public Color Color_Background
        {
            get => _color_Background;
            set
            {
                if (value != _color_Background)
                {
                    _color_Background = value;
                    _colorsDirty = true;
                    Invalidate();
                }
            }
        }

        private Color _color_Foreground = Color.White;
        public Color Color_Foreground
        {
            get => _color_Foreground;
            set
            {
                if (value != _color_Foreground)
                {
                    _color_Foreground = value;
                    Invalidate();
                }
            }
        }

        private Color _color_Selection = Color.Gray;
        public Color Color_Selection
        {
            get => _color_Selection;
            set
            {
                if (value != _color_Selection)
                {
                    _color_Selection = value;
                    Invalidate();
                }
            }
        }

        private Color _color_Cursor = Color.White;
        public Color Color_Cursor
        {
            get => _color_Cursor;
            set
            {
                if (value != _color_Cursor)
                {
                    _color_Cursor = value;
                    Invalidate();
                }
            }
        }

        private CursorShape_Enum _cursorType = CursorShape_Enum.bar;
        public CursorShape_Enum CursorType
        {
            get => _cursorType;
            set
            {
                if (value != _cursorType)
                {
                    _cursorType = value;
                    Invalidate();
                }
            }
        }

        private int _cursorHeight = 25;
        public int CursorHeight
        {
            get => _cursorHeight;
            set
            {
                if (value != _cursorHeight)
                {
                    _cursorHeight = value;
                    Invalidate();
                }
            }
        }

        private bool _light = false;
        public bool Light
        {
            get => _light;
            set
            {
                if (value != _light)
                {
                    _light = value;
                    _colorsDirty = true;
                    Invalidate();
                }
            }
        }

        private bool _useAcrylicOnTitlebar = false;
        public bool UseAcrylicOnTitlebar
        {
            get => _useAcrylicOnTitlebar;
            set
            {
                if (value != _useAcrylicOnTitlebar)
                {
                    _useAcrylicOnTitlebar = value;
                    Invalidate();
                }
            }
        }

        private bool _useAcrylic = false;
        public bool UseAcrylic
        {
            get => _useAcrylic;
            set
            {
                if (value != _useAcrylic)
                {
                    _useAcrylic = value;
                    Invalidate();
                }
            }
        }

        private string _tabTitle = string.Empty;
        public string TabTitle
        {
            get => _tabTitle;
            set
            {
                if (value != _tabTitle)
                {
                    _tabTitle = value;
                    Invalidate();
                }
            }
        }

        private Color _tabColor = Color.FromArgb(0, 0, 0, 0);
        public Color TabColor
        {
            get => _tabColor;
            set
            {
                if (value != _tabColor)
                {
                    _tabColor = value;
                    Invalidate();
                }
            }
        }

        private bool _previewVersion = true;
        public bool PreviewVersion
        {
            get => _previewVersion;
            set
            {
                if (value != _previewVersion)
                {
                    _previewVersion = value;
                    _colorsDirty = true;
                    Invalidate();
                }
            }
        }

        private static string _tabIconButItIsString_Default = "";
        private string _tabIconButItIsString = _tabIconButItIsString_Default;
        public string TabIconButItIsString
        {
            get => _tabIconButItIsString;
            set
            {
                if (value != _tabIconButItIsString)
                {
                    _tabIconButItIsString = value;
                    Invalidate();
                }
            }
        }

        private bool _isFocused = true;
        public bool IsFocused
        {
            get => _isFocused;
            set
            {
                if (value != _isFocused)
                {
                    _isFocused = value;
                    Invalidate();
                }
            }
        }

        private Image _tabIcon = null;
        public Image TabIcon
        {
            get => _tabIcon;
            set
            {
                if (value != _tabIcon)
                {
                    _tabIcon = value;
                    Invalidate();
                }
            }
        }

        #endregion

        #region Events/Overrides

        protected override void OnHandleCreated(EventArgs e)
        {
            if (!DesignMode)
            {
                ProcessBack();

                Timer.Tick += Timer_Tick;
                Timer.Enabled = true; Timer.Start();
            }
            else { Timer.Enabled = false; Timer.Stop(); }

            base.OnHandleCreated(e);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (!DesignMode) { Timer.Tick -= Timer_Tick; }

            Timer.Enabled = false; Timer.Stop();

            base.OnHandleDestroyed(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            _layoutDirty = true;
            titlebarBlurClone?.Dispose();
            titlebarBlurClone = null;
            ProcessBack();

            base.OnSizeChanged(e);
        }

        protected override async void OnFontChanged(EventArgs e)
        {
            _layoutDirty = true;
            await Task.Delay(10);
            Invalidate();

            base.OnFontChanged(e);
        }

        private async void UpdateOpacityBackImageChanged()
        {
            if (BackImage is not null)
            {
                img = BackImage.Fade(OpacityBackImage / 100f);
                await Task.Delay(10);
                Invalidate();
            }
        }

        private void ProcessBack()
        {
            GetBack();
            NoiseBack();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Timer.Tick -= Timer_Tick;
                Timer.Dispose();

                _tabTitleFont?.Dispose();
                _tabRegularFont?.Dispose();
                _closeIconFont?.Dispose();

                img?.Dispose();
                adaptedBack?.Dispose();
                adaptedBackBlurred?.Dispose();
                titlebarBlurClone?.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion

        #region Methods

        private GraphicsPath RR(Rectangle r, int radius)
        {
            GraphicsPath path = new();
            int d = radius * 2;
            float f0 = 0.5f;
            float f1 = 2f - f0;

            RectangleF R1 = new(r.X + f0 * d, r.Y, d, d);
            RectangleF R2 = new(r.X + r.Width - f1 * d, r.Y, d, d);
            RectangleF R3 = new(r.X - f0 * d, r.Y + r.Height - f0 * d, d, f0 * d);
            RectangleF R4 = new(r.X + r.Width - f0 * d, r.Y + r.Height - f0 * d, d, f0 * d);

            path.AddArc(R4, 90f, 90f);
            path.AddLine(new PointF(R4.X, R4.Y), new PointF(R2.Right, R2.Bottom));
            path.AddArc(R2, 0f, -90);
            path.AddArc(R1, -90, -90);
            path.AddArc(R3, 0f, 90f);
            path.AddLine(new PointF(R3.X + R3.Width, R3.Y + R3.Height), new PointF(R4.X, R4.Y + R4.Height));

            path.CloseFigure();

            return path;
        }

        private void FillSemiRect(Graphics Graphics, Brush Brush, Rectangle Rectangle, int Radius = -1)
        {
            if (Radius == -1) Radius = 6;

            if (Graphics is null) return;

            using (GraphicsPath path = RoundedSemiRectangle(Rectangle, Radius))
            {
                Graphics.FillPath(Brush, path);
            }
        }

        private GraphicsPath RoundedSemiRectangle(Rectangle r, int radius)
        {
            GraphicsPath path = new();
            int d = radius * 2;

            path.AddLine(r.Left + d, r.Top, r.Right - d, r.Top);
            path.AddArc(Rectangle.FromLTRB(r.Right - d, r.Top, r.Right, r.Top + d), -90, 90f);

            path.AddLine(r.Right, r.Top, r.Right, r.Bottom);

            path.AddLine(r.Right, r.Bottom, r.Left, r.Bottom);

            path.AddLine(r.Left, r.Bottom - d, r.Left, r.Top + d);
            path.AddArc(Rectangle.FromLTRB(r.Left, r.Top, r.Left + d, r.Top + d), 180f, 90f);

            path.CloseFigure();
            return path;
        }

        private void FillSemiImg(Graphics Graphics, Image Image, Rectangle Rectangle, int Radius = -1, bool ForcedRoundCorner = false)
        {
            if (Radius == -1) Radius = 6;

            if (Graphics is null) return;

            if ((Program.Style.RoundedCorners | ForcedRoundCorner) & Radius > 0)
            {
                using (GraphicsPath path = RoundedSemiRectangle(Rectangle, Radius))
                using (Region reg = new(path))
                {
                    Graphics.Clip = reg;
                    Graphics.DrawImage(Image, Rectangle);
                    Graphics.ResetClip();
                }
            }
            else
            {
                Graphics.DrawImage(Image, Rectangle);
            }
        }

        private void GetBack()
        {
            if (IsInDesignMode) return;

            adaptedBack?.Dispose();
            adaptedBack = null;
            adaptedBack = Program.WallpaperMonitor.Get(Program.TM, Program.WindowStyle)?.Clone() as Bitmap;

            adaptedBackBlurred?.Dispose();
            adaptedBackBlurred = null;
            adaptedBackBlurred = adaptedBack?.Blur(13);

            titlebarBlurClone?.Dispose();
            titlebarBlurClone = null;

            if (adaptedBackBlurred != null && Width > 0 && Height > 0)
            {
                Rectangle tb = new(0, 0, Width - 1, 41);
                if (tb.Width > 0 && tb.Height > 0)  titlebarBlurClone = adaptedBackBlurred.Clone(tb, PixelFormat.Format32bppArgb);
            }
        }

        private void NoiseBack()
        {
            using (Bitmap b = Resources.Noise.Fade(0.5f)) { Noise = new(b); }
        }

        private void RebuildLayout()
        {
            _rect = new Rectangle(0, 0, Width - 1, Height - 1);
            _rectTitlebar = new Rectangle(0, 0, Width - 1, 41);
            _rectConsole = new Rectangle(1, _rectTitlebar.Bottom - 1, Width - 3, Height - _rectTitlebar.Bottom);

            _s1 = Program.Localization.Strings.Aspects.Terminals.ConsoleSample;
            _s2 = Program.Localization.Strings.Aspects.Terminals.ThisIsASelection;
            _s3 = $"{SysPaths.System32}>";
            _anotherTab = Program.Localization.Strings.Aspects.Terminals.Another;

            _s1X = _s1.Measure(Font) + new SizeF(5f, 0f);
            _s2X = _s2.Measure(Font) + new SizeF(5f, 0f);
            _s3X = _s3.Measure(Font) + new SizeF(2f, 0f);

            _rectConsoleText0 = new RectangleF(8, _rectTitlebar.Bottom + 8, _s1X.Width, _s1X.Height);
            _rectConsoleText1 = new RectangleF(8, _rectConsoleText0.Bottom + 3, _s2X.Width, _s2X.Height);
            _rectConsoleText2 = new RectangleF(8, _rectConsoleText1.Bottom + _rectConsoleText1.Height + 3, _s3X.Width, _s3X.Height);
            _rectConsoleCursor = new RectangleF(_rectConsoleText2.Right, _rectConsoleText2.Y - 2, 50, _rectConsoleText2.Height - 1);

            _layoutDirty = false;
        }

        private void RebuildColors()
        {
            if (PreviewVersion)
            {
                if (!Light)
                {
                    _cachedTitlebar = _color_Titlebar == _transparent ? Color.FromArgb(46, 46, 46) : _color_Titlebar;
                    _cachedTabFocused = _color_TabFocused == _transparent ? _color_Background : _color_TabFocused;

                    _cachedTabUnfocused = _color_TabUnFocused == _transparent
                        ? (_cachedTabFocused == _color_Background ? _cachedTitlebar : _cachedTabFocused.Dark())
                        : _color_TabUnFocused;

                    _cachedTitlebarUnfocused = _color_Titlebar_Unfocused == _transparent ? Color.FromArgb(46, 46, 46) : _color_Titlebar_Unfocused;
                }
                else
                {
                    _cachedTitlebar = _color_Titlebar == _transparent ? Color.FromArgb(232, 232, 232) : _color_Titlebar;
                    _cachedTabFocused = _color_TabFocused == _transparent ? _color_Background : _color_TabFocused;

                    _cachedTabUnfocused = _color_TabUnFocused == _transparent
                        ? (_cachedTabFocused == _color_Background ? _cachedTitlebar : _cachedTabFocused.Light())
                        : _color_TabUnFocused;

                    _cachedTitlebarUnfocused = _color_Titlebar_Unfocused == _transparent ? Color.FromArgb(255, 255, 255) : _color_Titlebar_Unfocused;
                }
            }
            else if (!Light)
            {
                _cachedTitlebar = Color.FromArgb(10, 10, 10);
                _cachedTitlebarUnfocused = Color.FromArgb(10, 10, 10);
                _cachedTabFocused = Color.FromArgb(40, 40, 40);
                _cachedTabUnfocused = _cachedTitlebar;
            }
            else
            {
                _cachedTitlebar = Color.FromArgb(218, 218, 218);
                _cachedTitlebarUnfocused = Color.FromArgb(218, 218, 218);
                _cachedTabFocused = Color.FromArgb(249, 249, 249);
                _cachedTabUnfocused = _cachedTitlebar;
            }

            _colorsDirty = false;
        }

        #endregion

        #region Animator

        private async void Timer_Tick(object sender, EventArgs e)
        {
            if (IsFocused) { tick = !tick; await Task.Delay(10); Invalidate(); }
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            if (_colorsDirty) RebuildColors();
            if (_layoutDirty) RebuildLayout();

            // Background
            if (UseAcrylic)
            {
                if (adaptedBackBlurred != null) G.DrawRoundImage(adaptedBackBlurred, _rect);
                G.FillRoundedRect(Noise, _rect);
                using (SolidBrush br = new(Color.FromArgb((int)(_Opacity / 100f * 255f), Color_Background))) G.FillRoundedRect(br, _rect);
                if (BackImage is not null) G.DrawRoundImage(img, _rect);
            }
            else
            {
                if (adaptedBack != null) G.DrawRoundImage(adaptedBack, _rect);
                using (SolidBrush br = new(Color.FromArgb((int)(_Opacity / 100f * 255f), Color_Background))) G.FillRoundedRect(br, _rect);
                if (BackImage is not null) G.DrawRoundImage(img, _rect);
            }

            // Titlebar
            if (UseAcrylicOnTitlebar & !DesignMode)
            {
                if (Program.Style.RoundedCorners)
                {
                    if (titlebarBlurClone != null) FillSemiImg(G, titlebarBlurClone, _rectTitlebar);
                    FillSemiRect(G, Noise, _rectTitlebar);
                }
                else
                {
                    if (titlebarBlurClone != null) G.DrawImage(titlebarBlurClone, _rectTitlebar);
                    G.FillRectangle(Noise, _rectTitlebar);
                }

                if (!Light)
                {
                    using (SolidBrush br = new(Color.FromArgb(IsFocused ? 100 : 255, 35, 35, 35)))
                    {
                        if (Program.Style.RoundedCorners) FillSemiRect(G, br, _rectTitlebar);
                        else G.FillRectangle(br, _rectTitlebar);
                    }
                }
                else
                {
                    using (SolidBrush br = new(Color.FromArgb(IsFocused ? 180 : 255, 232, 232, 232)))
                    {
                        if (Program.Style.RoundedCorners) FillSemiRect(G, br, _rectTitlebar);
                        else G.FillRectangle(br, _rectTitlebar);
                    }
                }
            }

            if (!UseAcrylicOnTitlebar)
            {
                using (SolidBrush br = new(IsFocused ? _cachedTitlebar : _cachedTitlebarUnfocused))
                {
                    if (Program.Style.RoundedCorners) FillSemiRect(G, br, _rectTitlebar);
                    else G.FillRectangle(br, _rectTitlebar);
                }
            }

            // Tabs
            Color TabFocusedFinalColor = (TabColor != _transparent && TabColor != Color.Empty) ? TabColor : _cachedTabFocused;

            int Radius = 5;
            int tabTopPadding = 9;
            int TabHeight = _rectTitlebar.Height - tabTopPadding;
            int iconSize = 16;

            Rectangle Rect_Tab0 = new(10, _rectTitlebar.Bottom - TabHeight, 220, TabHeight);
            Rectangle Rect_Tab1 = Rect_Tab0;
            Rect_Tab1.X = Rect_Tab0.X + Rect_Tab0.Width - Radius;

            Rectangle IconRect0 = new(Rect_Tab0.X + iconSize, Rect_Tab0.Y + (Rect_Tab0.Height - iconSize) / 2, iconSize, iconSize);
            int iconPadding = IconRect0.Left - Rect_Tab0.X;

            Color FC0 = TabFocusedFinalColor.IsDark() ? Color.White : Color.Black;
            Rectangle RectClose_Tab0 = new(Rect_Tab0.Right - iconPadding - iconSize + 2, IconRect0.Y, iconSize, iconSize);
            Rectangle RectText_Tab0 = new(IconRect0.Right + iconPadding / 2, IconRect0.Y + 1, RectClose_Tab0.Left - iconPadding / 2 - (IconRect0.Right + iconPadding / 2), IconRect0.Height);

            Rectangle IconRect1 = new(Rect_Tab1.X + iconPadding, Rect_Tab1.Y + (Rect_Tab1.Height - iconSize) / 2, iconSize, iconSize);
            Color FC1 = _cachedTabUnfocused.IsDark() ? Color.White : Color.Black;
            Rectangle RectClose_Tab1 = new(Rect_Tab1.Right - iconPadding - iconSize + 2, IconRect1.Y, iconSize, iconSize);
            Rectangle RectText_Tab1 = new(IconRect1.Right + iconPadding / 2, IconRect1.Y + 1, RectClose_Tab1.Left - iconPadding / 2 - (IconRect1.Right + iconPadding / 2), IconRect1.Height);

            if (IsFocused)
            {
                G.SmoothingMode = SmoothingMode.Default;
                using (SolidBrush br = new(TabFocusedFinalColor))
                using (GraphicsPath path = RR(Rect_Tab0, Radius))
                using (Pen P = new(TabFocusedFinalColor))
                {
                    G.FillPath(br, path);
                    G.SmoothingMode = SmoothingMode.AntiAlias;
                    G.DrawPath(P, path);
                    G.SmoothingMode = SmoothingMode.Default;
                }

                using (GraphicsPath path = RR(Rect_Tab1, Radius))
                {
                    if (!UseAcrylicOnTitlebar)
                    {
                        using (SolidBrush br = new(_cachedTabUnfocused)) G.FillPath(br, path);
                    }
                    else if (_cachedTabUnfocused != _cachedTitlebar)
                    {
                        using (SolidBrush br = new(_cachedTabUnfocused)) G.FillPath(br, path);
                    }
                }
            }

            Font iconFont = (OS.W12 || OS.W11) ? _iconFont_W11 : _iconFont_W10;

            if (TabIcon is not null)
            {
                G.DrawImage(TabIcon, IconRect0);
            }
            else
            {
                using (SolidBrush br = new(FC0))  G.DrawString(_tabIconButItIsString, iconFont, br, IconRect0, _sf_tc);
            }

            using (SolidBrush br = new(FC1)) G.DrawString(_tabIconButItIsString_Default, iconFont, br, IconRect1, _sf_tc);

            TextRenderer.DrawText(G, TabTitle, _tabTitleFont, RectText_Tab0, FC0, Color.Transparent, TextFormatFlags.WordEllipsis);
            TextRenderer.DrawText(G, _anotherTab, _tabRegularFont, RectText_Tab1, FC1, Color.Transparent, TextFormatFlags.WordEllipsis);

            using (SolidBrush br = new(FC0))  G.DrawString("", _closeIconFont, br, RectClose_Tab0, _sf_mc);
            using (SolidBrush br = new(FC1))  G.DrawString("", _closeIconFont, br, RectClose_Tab1, _sf_mc);

            using (SolidBrush br = new(Color_Foreground)) G.DrawString(_s1, Font, br, _rectConsoleText0, _sf_ml);

            using (SolidBrush br = new(Color.FromArgb(125, Color_Selection))) G.FillRectangle(br, _rectConsoleText1);

            using (SolidBrush br = new(Color.FromArgb(255 - 125, Color_Foreground))) G.DrawString(_s2, Font, br, _rectConsoleText1, _sf_ml);

            using (SolidBrush br = new(Color_Foreground)) G.DrawString(_s3, Font, br, _rectConsoleText2, _sf_ml);

            // Cursor tick
            if (tick & IsFocused)
            {
                GraphicsState state = G.Save();
                G.SetClip(_rectConsoleCursor, CombineMode.Intersect);
                G.SmoothingMode = SmoothingMode.HighSpeed;

                using (SolidBrush br = new(Color_Cursor))
                {
                    switch (CursorType)
                    {
                        case CursorShape_Enum.bar:
                            G.FillRectangle(br, new RectangleF(_rectConsoleCursor.X, _rectConsoleCursor.Y, 1, _rectConsoleCursor.Height));
                            break;

                        case CursorShape_Enum.doubleUnderscore:
                            G.FillRectangle(br, new RectangleF(_rectConsoleCursor.X, _rectConsoleCursor.Bottom, _rectConsoleCursor.Height * 0.5f, 1));
                            G.FillRectangle(br, new RectangleF(_rectConsoleCursor.X, _rectConsoleCursor.Bottom - 3, _rectConsoleCursor.Height * 0.5f, 1));
                            break;

                        case CursorShape_Enum.emptyBox:
                            using (Pen p = new(Color_Cursor))
                                G.DrawRectangle(p, _rectConsoleCursor.X, _rectConsoleCursor.Y, _rectConsoleCursor.Height * 0.5f, _rectConsoleCursor.Height);
                            break;

                        case CursorShape_Enum.filledBox:
                            G.FillRectangle(br, new RectangleF(_rectConsoleCursor.X, _rectConsoleCursor.Y, _rectConsoleCursor.Height * 0.5f, _rectConsoleCursor.Height));
                            break;

                        case CursorShape_Enum.underscore:
                            G.FillRectangle(br, new RectangleF(_rectConsoleCursor.X, _rectConsoleCursor.Bottom - 1, _rectConsoleCursor.Height * 0.5f, 1));
                            break;

                        case CursorShape_Enum.vintage:
                            G.FillRectangle(br, new RectangleF(_rectConsoleCursor.X, _rectConsoleCursor.Bottom - CursorHeight / 100f * _rectConsoleCursor.Height, _rectConsoleCursor.Height * 0.5f, CursorHeight / 100f * _rectConsoleCursor.Height));
                            break;

                        default:
                            G.FillRectangle(br, new RectangleF(_rectConsoleCursor.X, _rectConsoleCursor.Y, 1, _rectConsoleCursor.Height));
                            break;
                    }
                }

                G.Restore(state);
            }

            G.DrawRoundedRect(_borderPen, _rect);

            base.OnPaint(e);
        }
    }
}