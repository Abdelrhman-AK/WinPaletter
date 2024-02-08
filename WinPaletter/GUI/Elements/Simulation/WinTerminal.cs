using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WinPaletter.UI.Simulation
{

    [Description("Simulated Windows Terminals")]
    [DefaultEvent("Click")]
    public class WinTerminal : ContainerControl
    {
        public WinTerminal()
        {
            Text = string.Empty;
            DoubleBuffered = true;
        }

        #region Variables
        Timer Timer = new() { Enabled = false, Interval = 500 };

        private TextureBrush Noise = new(Properties.Resources.Noise.Fade(0.15f));
        private Bitmap adaptedBack;
        private Bitmap adaptedBackBlurred;
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
                    Refresh();
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
                    Refresh();
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
                    Refresh();
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
                    Refresh();
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
                    Refresh();
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
                    Refresh();
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
                    Refresh();
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
                    Refresh();
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
                    Refresh();
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
                    Refresh();
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
                    Refresh();
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
                    Refresh();
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
                    Refresh();
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
                    Refresh();
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
                    Refresh();
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
                    Refresh();
                }
            }
        }

        private string _tabIconButItIsString = "";
        public string TabIconButItIsString
        {
            get => _tabIconButItIsString;
            set
            {
                if (value != _tabIconButItIsString)
                {
                    _tabIconButItIsString = value;
                    Refresh();
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
                    Refresh();
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
                    Refresh();
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

                try { Timer.Tick += Timer_Tick; } catch { }

                Timer.Enabled = true; Timer.Start();
            }
            else { Timer.Enabled = false; Timer.Stop(); }

            base.OnHandleCreated(e);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (!DesignMode) { try { Timer.Tick -= Timer_Tick; } catch { } }

            Timer.Enabled = false; Timer.Stop();

            base.OnHandleDestroyed(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            ProcessBack();

            base.OnSizeChanged(e);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            Refresh();

            base.OnFontChanged(e);
        }

        private void UpdateOpacityBackImageChanged()
        {
            if (BackImage is not null)
            {
                img = BackImage.Fade(OpacityBackImage / 100f);
                Refresh();
            }
        }

        private void ProcessBack()
        {
            GetBack();
            NoiseBack();
        }

        #endregion

        #region Methods

        private GraphicsPath RR(Rectangle r, int radius)
        {
            try
            {
                GraphicsPath path = new();
                int d = radius * 2;
                float f0 = 0.5f;
                float f1 = 2f - f0;

                Rectangle R1 = new((int)Math.Round(r.X + f0 * d), r.Y, d, d);
                Rectangle R2 = new((int)Math.Round(r.X + r.Width - f1 * d), r.Y, d, d);
                Rectangle R3 = new((int)Math.Round(r.X - f0 * d), (int)Math.Round(r.Y + r.Height - f0 * d), d, (int)Math.Round(f0 * d));
                Rectangle R4 = new((int)Math.Round(r.X + r.Width - f0 * d), (int)Math.Round(r.Y + r.Height - f0 * d), d, (int)Math.Round(f0 * d));

                path.AddArc(R4, 90f, 90f);
                path.AddLine(new Point(R4.X, R4.Y), new Point(R2.Right, R2.Bottom));
                path.AddArc(R2, 0f, -90);
                path.AddArc(R1, -90, -90);
                path.AddArc(R3, 0f, 90f);
                path.AddLine(new Point(R3.X + R3.Width, R3.Y + R3.Height), new Point(R4.X, R4.Y + R4.Height));

                path.CloseFigure();

                return path;
            }
            catch
            {
                return null;
            }
        }

        private GraphicsPath RRNoLine(Rectangle r, int radius)
        {
            try
            {
                GraphicsPath path = new();
                int d = radius * 2;
                float f0 = 0.5f;
                float f1 = 2f - f0;

                Rectangle R1 = new((int)Math.Round(r.X + f0 * d), r.Y, d, d);
                Rectangle R2 = new((int)Math.Round(r.X + r.Width - f1 * d), r.Y, d, d);
                Rectangle R3 = new((int)Math.Round(r.X - f0 * d), (int)Math.Round(r.Y + r.Height - f0 * d), d, (int)Math.Round(f0 * d));
                Rectangle R4 = new((int)Math.Round(r.X + r.Width - f0 * d), (int)Math.Round(r.Y + r.Height - f0 * d), d, (int)Math.Round(f0 * d));

                path.AddArc(R4, 90f, 90f);
                path.AddLine(new Point(R4.X, R4.Y), new Point(R2.Right, R2.Bottom));
                path.AddArc(R2, 0f, -90);
                path.AddArc(R1, -90, -90);
                path.AddArc(R3, 0f, 90f);
                path.AddLine(new Point(R3.X + R3.Width, R3.Y + R3.Height), new Point(R4.X, R4.Y + R4.Height));

                path.CloseFigure();

                return path;
            }
            catch
            {
                return null;
            }
        }

        private void FillSemiRect(Graphics Graphics, Brush Brush, Rectangle Rectangle, int Radius = -1)
        {
            try
            {
                if (Radius == -1)
                    Radius = 6;

                if (Graphics is null)
                    throw new ArgumentNullException("graphics");

                using (GraphicsPath path = RoundedSemiRectangle(Rectangle, Radius))
                {
                    Graphics.FillPath(Brush, path);
                }
            }
            catch
            {
            }
        }

        private GraphicsPath RoundedSemiRectangle(Rectangle r, int radius)
        {
            try
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
            catch
            {
                return null;
            }
        }

        private void FillSemiImg(Graphics Graphics, Image Image, Rectangle Rectangle, int Radius = -1, bool ForcedRoundCorner = false)
        {
            try
            {
                if (Radius == -1)
                    Radius = 6;

                if (Graphics is null)
                    throw new ArgumentNullException("graphics");

                if ((Program.Style.RoundedCorners | ForcedRoundCorner) & Radius > 0)
                {
                    using (GraphicsPath path = RoundedSemiRectangle(Rectangle, Radius))
                    {
                        Region reg = new(path);
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
            catch
            {
            }
        }

        private void GetBack()
        {
            if (Program.Wallpaper != null)
            {
                adaptedBack = Program.Wallpaper;
                adaptedBackBlurred = adaptedBack.Blur(13);
            }
        }

        private void NoiseBack()
        {
            using (Bitmap b = Properties.Resources.Noise.Fade(0.5f)) { Noise = new(b); }
        }

        #endregion

        #region Animator

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (IsFocused) { tick = !tick; Refresh(); }
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : TextRenderingHint.SystemDefault;

            if (PreviewVersion)
            {
                if (!Light)
                {
                    if (Color_Titlebar == Color.FromArgb(0, 0, 0, 0))
                        Color_Titlebar = Color.FromArgb(46, 46, 46);
                    if (Color_TabFocused == Color.FromArgb(0, 0, 0, 0))
                        Color_TabFocused = Color_Background;

                    if (Color_TabUnFocused == Color.FromArgb(0, 0, 0, 0))
                    {
                        if (Color_TabFocused == Color_Background)
                        {
                            Color_TabUnFocused = Color_Titlebar;
                        }
                        else
                        {
                            Color_TabUnFocused = Color_TabFocused.Dark();
                        }
                    }

                    if (Color_Titlebar_Unfocused == Color.FromArgb(0, 0, 0, 0))
                        Color_Titlebar_Unfocused = Color.FromArgb(46, 46, 46);
                }
                else
                {
                    if (Color_Titlebar == Color.FromArgb(0, 0, 0, 0))
                        Color_Titlebar = Color.FromArgb(232, 232, 232);
                    if (Color_TabFocused == Color.FromArgb(0, 0, 0, 0))
                        Color_TabFocused = Color_Background;

                    if (Color_TabUnFocused == Color.FromArgb(0, 0, 0, 0))
                    {
                        if (Color_TabFocused == Color_Background)
                        {
                            Color_TabUnFocused = Color_Titlebar;
                        }
                        else
                        {
                            Color_TabUnFocused = Color_TabFocused.Light();
                        }
                    }

                    if (Color_Titlebar_Unfocused == Color.FromArgb(0, 0, 0, 0))
                        Color_Titlebar_Unfocused = Color.FromArgb(255, 255, 255);
                }
            }
            else if (!Light)
            {
                Color_Titlebar = Color.FromArgb(10, 10, 10);
                Color_Titlebar_Unfocused = Color.FromArgb(10, 10, 10);
                Color_TabFocused = Color.FromArgb(40, 40, 40);
                Color_TabUnFocused = Color_Titlebar;
            }
            else
            {
                Color_Titlebar = Color.FromArgb(218, 218, 218);
                Color_Titlebar_Unfocused = Color.FromArgb(218, 218, 218);
                Color_TabFocused = Color.FromArgb(249, 249, 249);
                Color_TabUnFocused = Color_Titlebar;
            }

            Rectangle Rect = new(0, 0, Width - 1, Height - 1);
            Rectangle Rect_Titlebar = new(0, 0, Width - 1, 32);
            Rectangle Rect_Console = new(1, Rect_Titlebar.Bottom - 1, Width - 3, Height - Rect_Titlebar.Height);

            string s1 = Program.Lang.Terminal_ConsoleSample;
            string s2 = Program.Lang.Terminal_ThisIsASelection;
            string s3 = $"{PathsExt.System32}>";

            SizeF s1X = s1.Measure(Font) + new SizeF(5f, 0f);
            SizeF s2X = s2.Measure(Font) + new SizeF(5f, 0f);
            SizeF s3X = s3.Measure(Font) + new SizeF(2f, 0f);
            Rectangle Rect_ConsoleText0 = new(8, Rect_Titlebar.Bottom + 8, (int)Math.Round(s1X.Width), (int)Math.Round(s1X.Height));
            Rectangle Rect_ConsoleText1 = new(8, Rect_ConsoleText0.Bottom + 3, (int)Math.Round(s2X.Width), (int)Math.Round(s2X.Height));
            Rectangle Rect_ConsoleText2 = new(8, Rect_ConsoleText1.Bottom + Rect_ConsoleText1.Height + 3, (int)Math.Round(s3X.Width), (int)Math.Round(s3X.Height));

            Rectangle Rect_ConsoleCursor = new(Rect_ConsoleText2.Right, Rect_ConsoleText2.Y - 2, 50, Rect_ConsoleText2.Height - 1);

            if (UseAcrylic)
            {
                if (adaptedBackBlurred != null) G.DrawRoundImage(adaptedBackBlurred, Rect);
                G.FillRoundedRect(Noise, Rect);
                using (SolidBrush br = new(Color.FromArgb((int)Math.Round(_Opacity / 100f * 255f), Color_Background)))
                {
                    G.FillRoundedRect(br, Rect);
                }
                if (BackImage is not null)
                    G.DrawRoundImage(img, Rect);
            }
            else
            {
                if (adaptedBack != null) G.DrawRoundImage(adaptedBack, Rect);
                using (SolidBrush br = new(Color.FromArgb((int)Math.Round(_Opacity / 100f * 255f), Color_Background)))
                {
                    G.FillRoundedRect(br, Rect);
                }
                if (BackImage is not null)
                    G.DrawRoundImage(img, Rect);
            }

            if (UseAcrylicOnTitlebar & !DesignMode)
            {
                if (Program.Style.RoundedCorners)
                {
                    if (adaptedBackBlurred != null) FillSemiImg(G, adaptedBackBlurred.Clone(Rect_Titlebar, PixelFormat.Format32bppArgb), Rect_Titlebar);
                    FillSemiRect(G, Noise, Rect_Titlebar);
                }
                else
                {
                    if (adaptedBackBlurred != null) G.DrawImage(adaptedBackBlurred.Clone(Rect_Titlebar, PixelFormat.Format32bppArgb), Rect_Titlebar);
                    G.FillRectangle(Noise, Rect_Titlebar);
                }

                if (!Light)
                {
                    if (Program.Style.RoundedCorners)
                    {
                        using (SolidBrush br = new(Color.FromArgb(IsFocused ? 100 : 255, 35, 35, 35)))
                        {
                            FillSemiRect(G, br, Rect_Titlebar);
                        }
                    }
                    else
                    {
                        using (SolidBrush br = new(Color.FromArgb(IsFocused ? 100 : 255, 35, 35, 35)))
                        {
                            G.FillRectangle(br, Rect_Titlebar);
                        }
                    }
                }
                else if (Program.Style.RoundedCorners)
                {
                    using (SolidBrush br = new(Color.FromArgb(IsFocused ? 180 : 255, 232, 232, 232)))
                    {
                        FillSemiRect(G, br, Rect_Titlebar);
                    }
                }
                else
                {
                    using (SolidBrush br = new(Color.FromArgb(IsFocused ? 180 : 255, 232, 232, 232)))
                    {
                        G.FillRectangle(br, Rect_Titlebar);
                    }
                }

            }

            if (!UseAcrylicOnTitlebar)
            {
                if (Program.Style.RoundedCorners)
                {
                    using (SolidBrush br = new(IsFocused ? Color_Titlebar : Color_Titlebar_Unfocused))
                    {
                        FillSemiRect(G, br, Rect_Titlebar);
                    }
                }
                else
                {
                    using (SolidBrush br = new(IsFocused ? Color_Titlebar : Color_Titlebar_Unfocused))
                    {
                        G.FillRectangle(br, Rect_Titlebar);
                    }
                }
            }

            Color TabFocusedFinalColor;

            if (TabColor != Color.FromArgb(0, 0, 0, 0) && TabColor != Color.Empty)
            {
                TabFocusedFinalColor = TabColor;
            }
            else
            {
                TabFocusedFinalColor = Color_TabFocused;
            }

            int Radius = 5;
            int TabHeight = 22;
            Rectangle Rect_Tab0 = new(10, Rect_Titlebar.Bottom - TabHeight, 150, TabHeight);
            Rectangle Rect_Tab1 = Rect_Tab0;
            Rect_Tab1.X = Rect_Tab0.X + Rect_Tab0.Width - Radius;

            Rectangle IconRect0 = new(Rect_Tab0.X + 10, Rect_Tab0.Y + 3, 16, 16);
            Color FC0 = TabFocusedFinalColor.IsDark() ? Color.White : Color.Black;
            Rectangle RectText_Tab0 = new(IconRect0.Right + 1, IconRect0.Y + 1, Rect_Tab0.Width - 35 - IconRect0.Width, IconRect0.Height);
            Rectangle RectClose_Tab0 = new(RectText_Tab0.Right + 2, RectText_Tab0.Y - 1, 15, RectText_Tab0.Height);

            Rectangle IconRect1 = new(Rect_Tab1.X + 10, Rect_Tab1.Y + 3, 16, 16);
            Color FC1 = Color_TabUnFocused.IsDark() ? Color.White : Color.Black;
            Rectangle RectText_Tab1 = new(IconRect1.Right + 1, IconRect1.Y + 1, Rect_Tab1.Width - 35 - IconRect1.Width, IconRect1.Height);
            Rectangle RectClose_Tab1 = new(RectText_Tab1.Right + 2, RectText_Tab1.Y - 1, 15, RectText_Tab1.Height);

            if (IsFocused)
            {
                G.SmoothingMode = SmoothingMode.Default;
                using (SolidBrush br = new(TabFocusedFinalColor))
                {
                    G.FillPath(br, RR(Rect_Tab0, Radius));
                }
                G.SmoothingMode = SmoothingMode.AntiAlias;
                using (Pen P = new(TabFocusedFinalColor))
                {
                    G.DrawPath(P, RRNoLine(Rect_Tab0, Radius));
                }
                G.SmoothingMode = SmoothingMode.Default;

                if (!UseAcrylicOnTitlebar)
                {
                    using (SolidBrush br = new(Color_TabUnFocused))
                    {
                        G.FillPath(br, RR(Rect_Tab1, Radius));
                    }
                }
                else if (Color_TabUnFocused != Color_Titlebar)
                {
                    using (SolidBrush br = new(Color_TabUnFocused))
                    {
                        G.FillPath(br, RR(Rect_Tab1, Radius));
                    }
                }
            }

            Font fx;

            if (OS.W12 || OS.W11)
            {
                fx = new("Segoe Fluent Icons", 12f);
            }
            else
            {
                fx = new("Segoe MDL2 Assets", 12f);
            }

            if (TabIcon is not null)
            {
                G.DrawImage(TabIcon, IconRect0);
            }
            else
            {
                using (StringFormat sf = ContentAlignment.TopCenter.ToStringFormat())
                {
                    using (SolidBrush br = new(FC0))
                    {
                        G.DrawString(TabIconButItIsString, fx, br, IconRect0, sf);
                    }
                }
            }

            using (StringFormat sf = ContentAlignment.TopCenter.ToStringFormat())
            {
                using (SolidBrush br = new(FC1))
                {
                    G.DrawString(TabIconButItIsString, fx, br, IconRect1, sf);
                }
            }
            TextRenderer.DrawText(G, TabTitle, new Font("Segoe UI", 8f, FontStyle.Bold), RectText_Tab0, FC0, Color.Transparent, TextFormatFlags.WordEllipsis);
            TextRenderer.DrawText(G, Program.Lang.Terminal_Another, new Font("Segoe UI", 8f, FontStyle.Regular), RectText_Tab1, FC1, Color.Transparent, TextFormatFlags.WordEllipsis);

            using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
            {
                using (SolidBrush br = new(FC0))
                {
                    G.DrawString("", new Font("Segoe MDL2 Assets", 6f, FontStyle.Regular), br, RectClose_Tab0, sf);
                }
                using (SolidBrush br = new(FC1))
                {
                    G.DrawString("", new Font("Segoe MDL2 Assets", 6f, FontStyle.Regular), br, RectClose_Tab1, sf);
                }
            }

            using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat())
            {
                using (SolidBrush br = new(Color_Foreground))
                {
                    G.DrawString(s1, Font, br, Rect_ConsoleText0, sf);
                }

                using (SolidBrush br = new(Color.FromArgb(125, Color_Selection)))
                {
                    G.FillRectangle(br, Rect_ConsoleText1);
                }

                using (SolidBrush br = new(Color.FromArgb(255 - 125, Color_Foreground)))
                {
                    G.DrawString(s2, Font, br, Rect_ConsoleText1, sf);
                }

                using (SolidBrush br = new(Color_Foreground))
                {
                    G.DrawString(s3, Font, br, Rect_ConsoleText2, sf);
                }
            }
            if (tick & IsFocused)
            {
                G.SmoothingMode = SmoothingMode.HighSpeed;

                using (SolidBrush br = new(Color_Cursor))
                {

                    switch (CursorType)
                    {
                        case CursorShape_Enum.bar:
                            {
                                G.FillRectangle(br, new Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Y, 1, Rect_ConsoleCursor.Height));
                                break;
                            }

                        case CursorShape_Enum.doubleUnderscore:
                            {
                                G.FillRectangle(br, new Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Bottom, (int)Math.Round(Rect_ConsoleCursor.Height * 0.5d), 1));
                                G.FillRectangle(br, new Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Bottom - 3, (int)Math.Round(Rect_ConsoleCursor.Height * 0.5d), 1));
                                break;
                            }

                        case CursorShape_Enum.emptyBox:
                            {
                                using (Pen p = new(Color_Cursor))
                                {
                                    G.DrawRectangle(p, new Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Y, (int)Math.Round(Rect_ConsoleCursor.Height * 0.5d), Rect_ConsoleCursor.Height));
                                }

                                break;
                            }

                        case CursorShape_Enum.filledBox:
                            {
                                G.FillRectangle(br, new Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Y, (int)Math.Round(Rect_ConsoleCursor.Height * 0.5d), Rect_ConsoleCursor.Height));
                                break;
                            }

                        case CursorShape_Enum.underscore:
                            {
                                G.FillRectangle(br, new Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Bottom - 1, (int)Math.Round(Rect_ConsoleCursor.Height * 0.5d), 1));
                                break;
                            }

                        case CursorShape_Enum.vintage:
                            {
                                G.FillRectangle(br, new Rectangle(Rect_ConsoleCursor.X, (int)Math.Round(Rect_ConsoleCursor.Bottom - CursorHeight / 100d * Rect_ConsoleCursor.Height), (int)Math.Round(Rect_ConsoleCursor.Height * 0.5d), (int)Math.Round(CursorHeight / 100d * Rect_ConsoleCursor.Height)));
                                break;
                            }

                        default:
                            {
                                G.FillRectangle(br, new Rectangle(Rect_ConsoleCursor.X, Rect_ConsoleCursor.Y, 1, Rect_ConsoleCursor.Height));
                                break;
                            }

                    }
                }

                G.SmoothingMode = SmoothingMode.AntiAlias;
            }

            using (Pen P = new(Color.FromArgb(45, 45, 45)))
            {
                G.DrawRoundedRect(P, Rect);
            }

            base.OnPaint(e);
        }
    }
}