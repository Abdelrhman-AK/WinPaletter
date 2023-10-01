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
            Text = "";
            DoubleBuffered = true;
            HandleCreated += WinTerminal_HandleCreated;
            HandleDestroyed += WinTerminal_HandleDestroyed;
            Timer.Tick += Timer_Tick;
        }

        #region Variables

        private TextureBrush Noise = new TextureBrush(My.Resources.GaussianBlur.Fade(0.15d));
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
            get
            {
                return _Opacity;
            }

            set
            {
                if (!(value == _Opacity))
                {
                    _Opacity = value;
                    Invalidate();
                }
            }
        }

        private float _OpacityBackImage = 100f;
        public float OpacityBackImage
        {
            get
            {
                return _OpacityBackImage;
            }

            set
            {
                if (!(value == _OpacityBackImage))
                {
                    _OpacityBackImage = value;
                    Invalidate();
                }
            }
        }

        private Image _BackImage;
        public Image BackImage
        {
            get
            {
                return _BackImage;
            }

            set
            {
                if (value != _BackImage)
                {
                    _BackImage = value;
                    Invalidate();
                    UpdateOpacityBackImageChanged();
                }
            }
        }

        public Color Color_Titlebar { get; set; } = Color.FromArgb(0, 0, 0, 0);
        public Color Color_Titlebar_Unfocused { get; set; } = Color.FromArgb(0, 0, 0, 0);
        public Color Color_TabFocused { get; set; } = Color.FromArgb(0, 0, 0, 0);
        public Color Color_TabUnFocused { get; set; } = Color.FromArgb(0, 0, 0, 0);
        public Color Color_Background { get; set; } = Color.Black;
        public Color Color_Foreground { get; set; } = Color.White;
        public Color Color_Selection { get; set; } = Color.Gray;
        public Color Color_Cursor { get; set; } = Color.White;
        public CursorShape_Enum CursorType { get; set; } = CursorShape_Enum.bar;
        public int CursorHeight { get; set; } = 25;
        public bool Light { get; set; } = false;
        public bool UseAcrylicOnTitlebar { get; set; } = false;
        public bool UseAcrylic { get; set; } = false;
        public string TabTitle { get; set; } = "";
        public Image TabIcon { get; set; }
        public Color TabColor { get; set; } = Color.FromArgb(0, 0, 0, 0);
        public bool PreviewVersion { get; set; } = true;
        public string TabIconButItIsString { get; set; } = "";
        public bool IsFocused { get; set; } = true;

        #endregion

        #region Events

        private void WinTerminal_HandleCreated(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Timer.Enabled = true;
                Timer.Start();

                try
                {
                    SizeChanged += ProcessBack_EventHandler;
                }
                catch
                {
                }
                ProcessBack();
            }
            else
            {
                Timer.Enabled = false;
                Timer.Stop();
            }
        }

        private void WinTerminal_HandleDestroyed(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                try
                {
                    SizeChanged -= ProcessBack_EventHandler;
                }
                catch
                {
                }
            }
        }

        public void UpdateOpacityBackImageChanged()
        {
            if (BackImage is not null)
            {
                img = BackImage.Fade((double)(OpacityBackImage / 100f));
                Refresh();
            }
        }

        private void ProcessBack_EventHandler(object sender, EventArgs e)
        {
            ProcessBack();
        }

        public void ProcessBack()
        {
            GetBack();
            NoiseBack();
        }

        #endregion

        #region Subs/Functions

        public GraphicsPath RR(Rectangle r, int radius)
        {
            try
            {
                var path = new GraphicsPath();
                int d = radius * 2;
                float f0 = 0.5f;
                float f1 = 2f - f0;

                var R1 = new Rectangle((int)Math.Round(r.X + f0 * d), r.Y, d, d);
                var R2 = new Rectangle((int)Math.Round(r.X + r.Width - f1 * d), r.Y, d, d);
                var R3 = new Rectangle((int)Math.Round(r.X - f0 * d), (int)Math.Round(r.Y + r.Height - f0 * d), d, (int)Math.Round(f0 * d));
                var R4 = new Rectangle((int)Math.Round(r.X + r.Width - f0 * d), (int)Math.Round(r.Y + r.Height - f0 * d), d, (int)Math.Round(f0 * d));

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

        public GraphicsPath RRNoLine(Rectangle r, int radius)
        {
            try
            {
                var path = new GraphicsPath();
                int d = radius * 2;
                float f0 = 0.5f;
                float f1 = 2f - f0;

                var R1 = new Rectangle((int)Math.Round(r.X + f0 * d), r.Y, d, d);
                var R2 = new Rectangle((int)Math.Round(r.X + r.Width - f1 * d), r.Y, d, d);
                var R3 = new Rectangle((int)Math.Round(r.X - f0 * d), (int)Math.Round(r.Y + r.Height - f0 * d), d, (int)Math.Round(f0 * d));
                var R4 = new Rectangle((int)Math.Round(r.X + r.Width - f0 * d), (int)Math.Round(r.Y + r.Height - f0 * d), d, (int)Math.Round(f0 * d));

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

        public void FillSemiRect(Graphics Graphics, Brush Brush, Rectangle Rectangle, int Radius = -1)
        {
            try
            {
                if (Radius == -1)
                    Radius = 6;

                if (Graphics is null)
                    throw new ArgumentNullException("graphics");

                using (var path = RoundedSemiRectangle(Rectangle, Radius))
                {
                    Graphics.FillPath(Brush, path);
                }
            }
            catch
            {
            }
        }

        public GraphicsPath RoundedSemiRectangle(Rectangle r, int radius)
        {
            try
            {
                var path = new GraphicsPath();
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

        public void FillSemiImg(Graphics Graphics, Image Image, Rectangle Rectangle, int Radius = -1, bool ForcedRoundCorner = false)
        {
            try
            {
                if (Radius == -1)
                    Radius = 6;

                if (Graphics is null)
                    throw new ArgumentNullException("graphics");

                if ((WPStyle.GetRoundedCorners() | ForcedRoundCorner) & Radius > 0)
                {
                    using (var path = RoundedSemiRectangle(Rectangle, Radius))
                    {
                        var reg = new Region(path);
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

        public void GetBack()
        {
            adaptedBack = My.Env.Wallpaper;
            adaptedBackBlurred = BitmapExtensions.Blur(new Bitmap(adaptedBack), 13);
        }

        public void NoiseBack()
        {
            Noise = new TextureBrush(My.Resources.GaussianBlur.Fade(0.5d));
        }

        #endregion

        #region Animator

        Timer Timer = new Timer() { Enabled = false, Interval = 500 };

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (IsFocused)
            {
                if (tick)
                {
                    tick = false;
                }
                else
                {
                    tick = true;
                }

                Refresh();
            }
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : TextRenderingHint.SystemDefault;

            DoubleBuffered = true;

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


            var Rect = new Rectangle(0, 0, Width - 1, Height - 1);
            var Rect_Titlebar = new Rectangle(0, 0, Width - 1, 32);
            var Rect_Console = new Rectangle(1, Rect_Titlebar.Bottom - 1, Width - 3, Height - Rect_Titlebar.Height);

            string s1 = My.Env.Lang.Terminal_ConsoleSample;
            string s2 = My.Env.Lang.Terminal_ThisIsASelection;
            string s3 = My.Env.PATH_System32 + ">";

            var s1X = s1.Measure(Font) + new SizeF(5f, 0f);
            var s2X = s2.Measure(Font) + new SizeF(2f, 0f);
            var s3X = s3.Measure(Font) + new SizeF(2f, 0f);
            var Rect_ConsoleText0 = new Rectangle(8, Rect_Titlebar.Bottom + 8, (int)Math.Round(s1X.Width), (int)Math.Round(s1X.Height));
            var Rect_ConsoleText1 = new Rectangle(8, Rect_ConsoleText0.Bottom + 3, (int)Math.Round(s2X.Width), (int)Math.Round(s2X.Height));
            var Rect_ConsoleText2 = new Rectangle(8, Rect_ConsoleText1.Bottom + Rect_ConsoleText1.Height + 3, (int)Math.Round(s3X.Width), (int)Math.Round(s3X.Height));

            var Rect_ConsoleCursor = new Rectangle(Rect_ConsoleText2.Right, Rect_ConsoleText2.Y, 50, Rect_ConsoleText2.Height - 1);

            if (UseAcrylic)
            {
                G.DrawRoundImage(adaptedBackBlurred, Rect);
                G.FillRoundedRect(Noise, Rect);
                using (var br = new SolidBrush(Color.FromArgb((int)Math.Round(_Opacity / 100f * 255f), Color_Background)))
                {
                    G.FillRoundedRect(br, Rect);
                }
                if (BackImage is not null)
                    G.DrawRoundImage(img, Rect);
            }
            else
            {
                G.DrawRoundImage(adaptedBack, Rect);
                using (var br = new SolidBrush(Color.FromArgb((int)Math.Round(_Opacity / 100f * 255f), Color_Background)))
                {
                    G.FillRoundedRect(br, Rect);
                }
                if (BackImage is not null)
                    G.DrawRoundImage(img, Rect);
            }

            if (UseAcrylicOnTitlebar & !DesignMode)
            {
                if (WPStyle.GetRoundedCorners())
                {
                    FillSemiImg(G, adaptedBackBlurred.Clone(Rect_Titlebar, PixelFormat.Format32bppArgb), Rect_Titlebar);
                    FillSemiRect(G, Noise, Rect_Titlebar);
                }
                else
                {
                    G.DrawImage(adaptedBackBlurred.Clone(Rect_Titlebar, PixelFormat.Format32bppArgb), Rect_Titlebar);
                    G.FillRectangle(Noise, Rect_Titlebar);
                }

                if (!Light)
                {
                    if (WPStyle.GetRoundedCorners())
                    {
                        using (var br = new SolidBrush(Color.FromArgb(IsFocused ? 100 : 255, 35, 35, 35)))
                        {
                            FillSemiRect(G, br, Rect_Titlebar);
                        }
                    }
                    else
                    {
                        using (var br = new SolidBrush(Color.FromArgb(IsFocused ? 100 : 255, 35, 35, 35)))
                        {
                            G.FillRectangle(br, Rect_Titlebar);
                        }
                    }
                }
                else if (WPStyle.GetRoundedCorners())
                {
                    using (var br = new SolidBrush(Color.FromArgb(IsFocused ? 180 : 255, 232, 232, 232)))
                    {
                        FillSemiRect(G, br, Rect_Titlebar);
                    }
                }
                else
                {
                    using (var br = new SolidBrush(Color.FromArgb(IsFocused ? 180 : 255, 232, 232, 232)))
                    {
                        G.FillRectangle(br, Rect_Titlebar);
                    }
                }

            }

            if (!UseAcrylicOnTitlebar)
            {
                if (WPStyle.GetRoundedCorners())
                {
                    using (var br = new SolidBrush(IsFocused ? Color_Titlebar : Color_Titlebar_Unfocused))
                    {
                        FillSemiRect(G, br, Rect_Titlebar);
                    }
                }
                else
                {
                    using (var br = new SolidBrush(IsFocused ? Color_Titlebar : Color_Titlebar_Unfocused))
                    {
                        G.FillRectangle(br, Rect_Titlebar);
                    }
                }
            }

            Color TabFocusedFinalColor;

            if (TabColor != Color.FromArgb(0, 0, 0, 0))
            {
                TabFocusedFinalColor = TabColor;
            }
            else
            {
                TabFocusedFinalColor = Color_TabFocused;
            }

            int Radius = 5;
            int TabHeight = 22;
            var Rect_Tab0 = new Rectangle(10, Rect_Titlebar.Bottom - TabHeight, 150, TabHeight);
            var Rect_Tab1 = Rect_Tab0;
            Rect_Tab1.X = Rect_Tab0.X + Rect_Tab0.Width - Radius;

            var IconRect0 = new Rectangle(Rect_Tab0.X + 10, Rect_Tab0.Y + 3, 16, 16);
            var FC0 = TabFocusedFinalColor.IsDark() ? Color.White : Color.Black;
            var RectText_Tab0 = new Rectangle(IconRect0.Right + 1, IconRect0.Y + 1, Rect_Tab0.Width - 35 - IconRect0.Width, IconRect0.Height);
            var RectClose_Tab0 = new Rectangle(RectText_Tab0.Right + 2, RectText_Tab0.Y - 1, 15, RectText_Tab0.Height);

            var IconRect1 = new Rectangle(Rect_Tab1.X + 10, Rect_Tab1.Y + 3, 16, 16);
            var FC1 = Color_TabUnFocused.IsDark() ? Color.White : Color.Black;
            var RectText_Tab1 = new Rectangle(IconRect1.Right + 1, IconRect1.Y + 1, Rect_Tab1.Width - 35 - IconRect1.Width, IconRect1.Height);
            var RectClose_Tab1 = new Rectangle(RectText_Tab1.Right + 2, RectText_Tab1.Y - 1, 15, RectText_Tab1.Height);

            if (IsFocused)
            {
                G.SmoothingMode = SmoothingMode.Default;
                using (var br = new SolidBrush(TabFocusedFinalColor))
                {
                    G.FillPath(br, RR(Rect_Tab0, Radius));
                }
                G.SmoothingMode = SmoothingMode.AntiAlias;
                using (var P = new Pen(TabFocusedFinalColor))
                {
                    G.DrawPath(P, RRNoLine(Rect_Tab0, Radius));
                }
                G.SmoothingMode = SmoothingMode.Default;

                if (!UseAcrylicOnTitlebar)
                {
                    using (var br = new SolidBrush(Color_TabUnFocused))
                    {
                        G.FillPath(br, RR(Rect_Tab1, Radius));
                    }
                }
                else if (Color_TabUnFocused != Color_Titlebar)
                {
                    using (var br = new SolidBrush(Color_TabUnFocused))
                    {
                        G.FillPath(br, RR(Rect_Tab1, Radius));
                    }
                }
            }

            Font fx;

            if (My.Env.W11)
            {
                fx = new Font("Segoe Fluent Icons", 12f);
            }
            else
            {
                fx = new Font("Segoe MDL2 Assets", 12f);
            }

            if (TabIcon is not null)
            {
                G.DrawImage(TabIcon, IconRect0);
            }
            else
            {
                using (var br = new SolidBrush(FC0))
                {
                    G.DrawString(TabIconButItIsString, fx, br, IconRect0, ContentAlignment.TopCenter.ToStringFormat());
                }
            }

            using (var br = new SolidBrush(FC1))
            {
                G.DrawString(TabIconButItIsString, fx, br, IconRect1, ContentAlignment.TopCenter.ToStringFormat());
            }

            TextRenderer.DrawText(G, TabTitle, new Font("Segoe UI", 8f, FontStyle.Bold), RectText_Tab0, FC0, Color.Transparent, TextFormatFlags.WordEllipsis);
            TextRenderer.DrawText(G, My.Env.Lang.Terminal_Another, new Font("Segoe UI", 8f, FontStyle.Regular), RectText_Tab1, FC1, Color.Transparent, TextFormatFlags.WordEllipsis);


            using (var br = new SolidBrush(FC0))
            {
                G.DrawString("", new Font("Segoe MDL2 Assets", 6f, FontStyle.Regular), br, RectClose_Tab0, ContentAlignment.MiddleCenter.ToStringFormat());
            }
            using (var br = new SolidBrush(FC1))
            {
                G.DrawString("", new Font("Segoe MDL2 Assets", 6f, FontStyle.Regular), br, RectClose_Tab1, ContentAlignment.MiddleCenter.ToStringFormat());
            }

            using (var br = new SolidBrush(Color_Foreground))
            {
                G.DrawString(s1, Font, br, Rect_ConsoleText0, ContentAlignment.TopLeft.ToStringFormat());
            }

            using (var br = new SolidBrush(Color.FromArgb(125, Color_Selection)))
            {
                G.FillRectangle(br, Rect_ConsoleText1);
            }

            using (var br = new SolidBrush(Color.FromArgb(255 - 125, Color_Foreground)))
            {
                G.DrawString(s2, Font, br, Rect_ConsoleText1, ContentAlignment.TopLeft.ToStringFormat());
            }

            using (var br = new SolidBrush(Color_Foreground))
            {
                G.DrawString(s3, Font, br, Rect_ConsoleText2, ContentAlignment.TopLeft.ToStringFormat());
            }

            if (tick & IsFocused)
            {
                G.SmoothingMode = SmoothingMode.HighSpeed;

                using (var br = new SolidBrush(Color_Cursor))
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
                                using (var p = new Pen(Color_Cursor))
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

            using (var P = new Pen(Color.FromArgb(45, 45, 45)))
            {
                G.DrawRoundedRect(P, Rect);
            }
        }

    }

}