using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace WinPaletter.UI.Simulation
{

    [Description("A simulated window")]
    public class Window : Panel
    {

        public Window()
        {
            AdjustPadding();
            Font = new Font("Segoe UI", 9f);
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            HandleCreated += Window_HandleCreated;
            HandleDestroyed += Window_HandleDestroyed;
        }

        #region Variables

        private Bitmap AdaptedBack, AdaptedBackBlurred;
        private Bitmap Noise7 = Properties.Resources.AeroGlass;
        private readonly int FreeMargin = 8;

        public enum Preview_Enum
        {
            W11,
            W10,
            W8,
            W8Lite,
            W7Aero,
            W7Opaque,
            W7Basic,
            WXP
        }

        #endregion

        #region Properties

        public bool Shadow { get; set; } = true;
        public int Radius { get; set; } = 5;
        public Color AccentColor_Active { get; set; } = Color.FromArgb(0, 120, 212);
        public Color AccentColor_Inactive { get; set; } = Color.FromArgb(32, 32, 32);
        public Color AccentColor2_Active { get; set; } = Color.FromArgb(0, 120, 212);
        public Color AccentColor2_Inactive { get; set; } = Color.FromArgb(32, 32, 32);
        public bool Active { get; set; } = true;
        public Preview_Enum Preview { get; set; } = Preview_Enum.W11;
        public int Win7Alpha { get; set; } = 100;
        public int Win7ColorBal { get; set; } = 100;
        public int Win7GlowBal { get; set; } = 100;
        public bool ToolWindow { get; set; } = false;
        public bool WinVista { get; set; } = false;
        public bool SuspendRefresh { get; set; } = false;

        private bool _DarkMode = true;
        public bool DarkMode
        {
            get
            {
                return _DarkMode;
            }
            set
            {
                _DarkMode = value;
                if (!SuspendRefresh)
                    Refresh();
            }
        }

        private bool _AccentColor_Enabled = true;
        public bool AccentColor_Enabled
        {
            get
            {
                return _AccentColor_Enabled;
            }
            set
            {
                _AccentColor_Enabled = value;
                if (!SuspendRefresh)
                    Refresh();
            }
        }

        private float _Win7Noise = 1f;
        public float Win7Noise
        {
            get
            {
                return _Win7Noise;
            }
            set
            {
                _Win7Noise = value;
                if (Preview == Preview_Enum.W7Aero | Preview == Preview_Enum.W7Opaque | Preview == Preview_Enum.W7Basic)
                {
                    try
                    {
                        Noise7 = Properties.Resources.AeroGlass.Fade((double)(Win7Noise / 100f));
                    }
                    catch
                    {
                    }
                }
                if (!SuspendRefresh)
                    Refresh();
            }
        }

        private int _Metrics_CaptionHeight = 22;
        public int Metrics_CaptionHeight
        {
            get
            {
                return _Metrics_CaptionHeight;
            }
            set
            {
                _Metrics_CaptionHeight = value;
                AdjustPadding();
                if (!SuspendRefresh)
                    Refresh();
                MetricsChanged?.Invoke();
            }
        }

        private int _Metrics_BorderWidth = 1;
        public int Metrics_BorderWidth
        {
            get
            {
                return _Metrics_BorderWidth;
            }
            set
            {
                _Metrics_BorderWidth = value;
                AdjustPadding();
                if (!SuspendRefresh)
                    Refresh();
                MetricsChanged?.Invoke();
            }
        }

        private int _Metrics_PaddedBorderWidth = 4;
        public int Metrics_PaddedBorderWidth
        {
            get
            {
                return _Metrics_PaddedBorderWidth;
            }
            set
            {
                _Metrics_PaddedBorderWidth = value;
                AdjustPadding();
                if (!SuspendRefresh)
                    Refresh();
                MetricsChanged?.Invoke();
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | 0x20;
                return cp;
            }
        }

        #endregion

        #region Events

        public event MetricsChangedEventHandler MetricsChanged;

        public delegate void MetricsChangedEventHandler();

        private void Window_HandleCreated(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                try
                {
                    Parent.BackgroundImageChanged += ProcessBack_EventHandler;
                }
                catch
                {
                }
                try
                {
                    FontChanged += AdjustPadding_EventHandler;
                }
                catch
                {
                }
            }

            ProcessBack();
        }

        private void Window_HandleDestroyed(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                try
                {
                    Parent.BackgroundImageChanged -= ProcessBack_EventHandler;
                }
                catch
                {
                }
                try
                {
                    FontChanged -= AdjustPadding_EventHandler;
                }
                catch
                {
                }
            }
        }

        public void ProcessBack_EventHandler(object sender, EventArgs e)
        {
            ProcessBack();
        }

        public void AdjustPadding_EventHandler(object sender, EventArgs e)
        {
            AdjustPadding();
        }

        public void ProcessBack()
        {
            Bitmap Wallpaper;
            if (Parent.BackgroundImage is null)
                Wallpaper = Program.Wallpaper;
            else
                Wallpaper = (Bitmap)Parent.BackgroundImage;
            try
            {
                if (Wallpaper is not null)
                    AdaptedBack = Wallpaper.Clone(Bounds, Wallpaper.PixelFormat);
            }
            catch
            {
            }
            try
            {
                if (Preview == Preview_Enum.W11)
                {
                    if (AdaptedBack is not null)
                    {
                        Bitmap b = new Bitmap(AdaptedBack).Blur(15);
                        if (DarkMode)
                        {
                            if (b is not null)
                            {
                                using (var ImgF = new ImageProcessor.ImageFactory())
                                {
                                    ImgF.Load(b);
                                    ImgF.Saturation(15);
                                    ImgF.Brightness(-10);
                                    AdaptedBackBlurred = (Bitmap)ImgF.Image.Clone();
                                }
                            }
                        }

                        else
                        {
                            AdaptedBackBlurred = b;
                        }
                    }
                }
                else if (AdaptedBack is not null)
                    AdaptedBackBlurred = new Bitmap(AdaptedBack).Blur(3);
            }
            catch
            {
            }
            try
            {
                Noise7 = Properties.Resources.AeroGlass.Fade(Win7Noise / 100);
            }
            catch
            {
            }
        }

        #endregion

        #region Voids/Functions

        public void CopycatFrom(Window Window, bool IgnoreLocationSizesAndText = false)
        {
            Shadow = Window.Shadow;
            Radius = Window.Radius;
            AccentColor_Active = Window.AccentColor_Active;
            AccentColor_Inactive = Window.AccentColor_Inactive;
            AccentColor2_Active = Window.AccentColor2_Active;
            AccentColor2_Inactive = Window.AccentColor2_Inactive;
            Active = Window.Active;
            Preview = Window.Preview;
            Win7Alpha = Window.Win7Alpha;
            Win7ColorBal = Window.Win7ColorBal;
            Win7GlowBal = Window.Win7GlowBal;
            WinVista = Window.WinVista;
            _DarkMode = Window.DarkMode;
            _AccentColor_Enabled = Window.AccentColor_Enabled;
            _Win7Noise = Window.Win7Noise;

            if (!IgnoreLocationSizesAndText)
            {
                ToolWindow = Window.ToolWindow;
                Dock = Window.Dock;
                Size = Window.Size;
                Location = Window.Location;
                Text = Window.Text;
            }

            AdjustPadding();
            ProcessBack();
            Refresh();
        }

        public void SetMetrics(Window Window)
        {
            Window.Metrics_BorderWidth = Metrics_BorderWidth;
            Window.Metrics_CaptionHeight = Metrics_CaptionHeight;
            Window.Metrics_PaddedBorderWidth = Metrics_PaddedBorderWidth;
            Window.Refresh();
        }

        public void AdjustPadding()
        {
            int i, iTop;

            int TitleTextH, TitleTextH_9, TitleTextH_Sum;
            TitleTextH = (int)Math.Round("ABCabc0123xYz.#".Measure(Font).Height);
            TitleTextH_9 = (int)Math.Round("ABCabc0123xYz.#".Measure(new Font(Font.Name, 9f, Font.Style)).Height);
            TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9 - 5);

            if (Preview == Preview_Enum.W7Aero | Preview == Preview_Enum.W7Opaque | Preview == Preview_Enum.W7Basic | Preview == Preview_Enum.W8 | Preview == Preview_Enum.W8Lite | Preview == Preview_Enum.WXP)
            {
                i = FreeMargin + (!(Preview == Preview_Enum.WXP) ? _Metrics_PaddedBorderWidth : 0) + _Metrics_BorderWidth;
                iTop = i + TitleTextH_Sum + _Metrics_CaptionHeight;

                i += 4;
                iTop += 3 + ((Preview == Preview_Enum.W8 || Preview == Preview_Enum.W8Lite) ? 1 : 0);
            }
            else
            {
                i = FreeMargin;
                iTop = i + TitleTextH_Sum + _Metrics_PaddedBorderWidth + _Metrics_BorderWidth + _Metrics_CaptionHeight;

                i += 1;
                iTop += 4;
            }

            Padding = new Padding(i, iTop, i, i);
        }

        public void FillSemiRect(Graphics Graphics, Brush Brush, Rectangle Rectangle, int Radius = -1)
        {
            try
            {
                if (Radius == -1)
                    Radius = 6;

                if (Graphics is null)
                    throw new ArgumentNullException("graphics");
                Graphics.SmoothingMode = SmoothingMode.AntiAlias;

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

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = Config.RenderingHint;
            DoubleBuffered = true;

            if (Win7Alpha > 255)
                Win7Alpha = 255;
            if (Win7Alpha < 0)
                Win7Alpha = 0;

            var Rect = new Rectangle(FreeMargin, FreeMargin, Width - (FreeMargin * 2 + 1), Height - (FreeMargin * 2 + 1));
            var RectBK = new Rectangle(0, 0, Width, Height);
            int TitleTextH, TitleTextH_9, TitleTextH_Sum;
            TitleTextH = (int)Math.Round("ABCabc0123xYz.#".Measure(Font).Height);
            TitleTextH_9 = (int)Math.Round("ABCabc0123xYz.#".Measure(new Font(Font.Name, 9f, Font.Style)).Height);
            TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9 - 5);
            var TitlebarRect = new Rectangle(Rect.X, Rect.Y, Rect.Width, TitleTextH_Sum + _Metrics_BorderWidth + _Metrics_CaptionHeight + _Metrics_PaddedBorderWidth + 3);
            // If TitlebarRect.Height < 25 Then TitlebarRect.Height = 25
            int IconSize = 14;
            if (_Metrics_CaptionHeight <= 17)
                IconSize = 12;
            var IconRect = new Rectangle(Rect.X + 4 + _Metrics_PaddedBorderWidth + _Metrics_BorderWidth, (int)Math.Round(Rect.Y + (TitlebarRect.Height - IconSize) / 2d), IconSize, IconSize);
            var LabelRect = new Rectangle(IconRect.Right + 4, Rect.Y, TitlebarRect.Width - (IconRect.Right + 4), TitlebarRect.Height);
            if (ToolWindow)
                LabelRect.X = IconRect.X;
            var LabelRect8 = new Rectangle(Rect.X, Rect.Y + 2, TitlebarRect.Width - 1, TitlebarRect.Height - 3);
            var XRect = new Rectangle(Rect.Right - 35, Rect.Y, 35, TitlebarRect.Height);

            // G.Clear(Color.Transparent)


            if (Preview == Preview_Enum.W11)
            {
                #region Windows 11
                if (Shadow & Active & !DesignMode)
                {
                    G.DrawGlow(Rect, Color.FromArgb(150, 0, 0, 0), 5, 15);
                }

                if (!AccentColor_Enabled && Active)
                {
                    G.SetClip(TitlebarRect);
                    G.DrawRoundImage(AdaptedBackBlurred, Rect, Radius, true);
                    G.ResetClip();
                }

                G.ExcludeClip(TitlebarRect);
                if (DarkMode)
                {
                    using (var br = new SolidBrush(Color.FromArgb(20, 20, 20)))
                    {
                        G.FillRoundedRect(br, Rect, Radius, true);
                    }
                }
                else
                {
                    using (var br = new SolidBrush(Color.FromArgb(240, 240, 240)))
                    {
                        G.FillRoundedRect(br, Rect, Radius, true);
                    }
                }
                G.ResetClip();

                if (AccentColor_Enabled)
                {
                    if (Active)
                    {
                        using (var P = new Pen(Color.FromArgb(200, AccentColor_Active)))
                        {
                            G.DrawRoundedRect(P, Rect, Radius, true);
                        }
                    }
                    else
                    {
                        using (var P = new Pen(Color.FromArgb(200, AccentColor_Inactive)))
                        {
                            G.DrawRoundedRect(P, Rect, Radius, true);
                        }
                    }
                }
                else if (DarkMode)
                {
                    using (var P = new Pen(Color.FromArgb(200, 100, 100, 100)))
                    {
                        G.DrawRoundedRect(P, Rect, Radius, true);
                    }
                }
                else
                {
                    using (var P = new Pen(Color.FromArgb(200, 220, 220, 220)))
                    {
                        G.DrawRoundedRect(P, Rect, Radius, true);
                    }
                }

                if (AccentColor_Enabled)
                {
                    if (Active)
                    {
                        using (var br = new SolidBrush(Color.FromArgb(255, AccentColor_Active)))
                        {
                            FillSemiRect(G, br, TitlebarRect, Radius);
                        }
                        using (var P = new Pen(Color.FromArgb(255, AccentColor_Active)))
                        {
                            G.DrawLine(P, new Point(TitlebarRect.X + 1, TitlebarRect.Y + TitlebarRect.Height), new Point(TitlebarRect.X + TitlebarRect.Width - 1, TitlebarRect.Y + TitlebarRect.Height));
                        }
                    }
                    else
                    {
                        using (var br = new SolidBrush(Color.FromArgb(255, AccentColor_Inactive)))
                        {
                            FillSemiRect(G, br, TitlebarRect, Radius);
                        }
                        using (var P = new Pen(Color.FromArgb(255, AccentColor_Inactive)))
                        {
                            G.DrawLine(P, new Point(TitlebarRect.X + 1, TitlebarRect.Y + TitlebarRect.Height), new Point(TitlebarRect.X + TitlebarRect.Width - 1, TitlebarRect.Y + TitlebarRect.Height));
                        }
                    }
                }
                else
                {
                    int a = Active ? DarkMode ? 180 : 245 : 255;
                    if (DarkMode)
                    {
                        using (var br = new SolidBrush(Color.FromArgb(a, 32, 32, 32)))
                        {
                            FillSemiRect(G, br, TitlebarRect, Radius);
                        }
                    }
                    else
                    {
                        using (var br = new SolidBrush(Color.FromArgb(a, 245, 245, 245)))
                        {
                            FillSemiRect(G, br, TitlebarRect, Radius);
                        }
                    }
                }
            }
            #endregion

            else if (Preview == Preview_Enum.W10)
            {
                #region Windows 10
                if (Shadow & Active & !DesignMode)
                {
                    G.DrawGlow(Rect, Color.FromArgb(150, 0, 0, 0), 5, 15);
                }

                if (DarkMode)
                {
                    using (var br = new SolidBrush(Color.FromArgb(20, 20, 20)))
                    {
                        G.FillRectangle(br, Rect);
                    }
                }
                else
                {
                    using (var br = new SolidBrush(Color.FromArgb(240, 240, 240)))
                    {
                        G.FillRectangle(br, Rect);
                    }
                }

                if (AccentColor_Enabled)
                {
                    if (Active)
                    {
                        using (var br = new SolidBrush(Color.FromArgb(255, AccentColor_Active)))
                        {
                            G.FillRectangle(br, TitlebarRect);
                        }
                    }
                    else
                    {
                        using (var br = new SolidBrush(Color.FromArgb(255, AccentColor_Inactive)))
                        {
                            G.FillRectangle(br, TitlebarRect);
                        }
                    }
                }
                else if (Active)
                {
                    if (DarkMode)
                    {
                        G.FillRectangle(Brushes.Black, TitlebarRect);
                    }
                    else
                    {
                        G.FillRectangle(Brushes.White, TitlebarRect);
                    }
                }
                else if (DarkMode)
                {
                    using (var br = new SolidBrush(Color.FromArgb(43, 43, 43)))
                    {
                        G.FillRectangle(br, TitlebarRect);
                    }
                }
                else
                {
                    G.FillRectangle(Brushes.White, TitlebarRect);
                }

                if (AccentColor_Enabled)
                {
                    if (Active)
                    {
                        using (var P = new Pen(Color.FromArgb(200, AccentColor_Active)))
                        {
                            G.DrawRectangle(P, Rect);
                        }
                    }
                    else
                    {
                        using (var P = new Pen(Color.FromArgb(200, AccentColor_Inactive)))
                        {
                            G.DrawRectangle(P, Rect);
                        }
                    }
                }
                else if (DarkMode)
                {
                    using (var P = new Pen(Color.FromArgb(125, 100, 100, 100)))
                    {
                        G.DrawRectangle(P, Rect);
                    }
                }
                else
                {
                    using (var P = new Pen(Color.FromArgb(125, 220, 220, 220)))
                    {
                        G.DrawRectangle(P, Rect);
                    }
                }
            }
            #endregion

            else if (Preview == Preview_Enum.W8 | Preview == Preview_Enum.W8Lite)
            {
                #region Windows 8/8.1
                var InnerWindow_1 = new Rectangle();
                var InnerWindow_2 = new Rectangle();
                int Sum = Metrics_BorderWidth + Metrics_PaddedBorderWidth;
                if (Sum < 2)
                    Sum = 2;
                TitleTextH = (int)Math.Round("ABCabc0123xYz.#".Measure(Font).Height);
                TitleTextH_9 = (int)Math.Round("ABCabc0123xYz.#".Measure(new Font(Font.Name, 9f, Font.Style)).Height);
                TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9 - 5);
                int Sum_Ttl = Sum + Metrics_CaptionHeight + TitleTextH_Sum;

                IconRect.X += 2;

                InnerWindow_1 = new Rectangle(Rect.X + Sum + (!(Preview == Preview_Enum.W8Lite) ? 2 : 3), Rect.Y + Sum_Ttl + 3, Rect.Width - Sum * 2 - (!(Preview == Preview_Enum.W8Lite) ? 4 : 6), Rect.Height - (Sum + Sum_Ttl) - (!(Preview == Preview_Enum.W8Lite) ? 5 : 5));
                InnerWindow_2 = new Rectangle(InnerWindow_1.X + 1, InnerWindow_1.Y + 1, InnerWindow_1.Width - 2, InnerWindow_1.Height - 2);

                int CloseRectH = Metrics_CaptionHeight + TitleTextH_Sum - 2 + (Preview == Preview_Enum.W8Lite ? 1 : 0);

                int CloseRectW = (int)Math.Round((!ToolWindow ? CloseRectH * 3 / 2d : CloseRectH) + (Preview == Preview_Enum.W8Lite ? 2 : 0));

                var CloseRect = new Rectangle();

                if (!ToolWindow)
                {

                    if (!(Preview == Preview_Enum.W8Lite))
                    {
                        CloseRect = new Rectangle(InnerWindow_1.Right - CloseRectW + 1, Rect.Y + 1, CloseRectW, CloseRectH);
                    }
                    else
                    {
                        CloseRect = new Rectangle(InnerWindow_1.Right - CloseRectW + 2, Rect.Y, CloseRectW, CloseRectH);
                    }
                }

                else
                {
                    CloseRect = new Rectangle(InnerWindow_1.Right - CloseRectW + 1, Rect.Y + 1, CloseRectW, CloseRectH);
                }

                var InC = !(Preview == Preview_Enum.W8Lite) ? Color.FromArgb(235, 235, 235) : Color.FromArgb((int)Math.Round(Win7ColorBal / 100d * 255d), AccentColor_Active.CB(0.8f));

                var c = Active ? Color.FromArgb((int)Math.Round(Win7ColorBal / 100d * 255d), AccentColor_Active) : InC;

                var bc = Color.FromArgb(217, 217, 217);

                using (var br = new SolidBrush(bc))
                {
                    G.FillRectangle(br, Rect);
                }
                using (var br = new SolidBrush(c))
                {
                    G.FillRectangle(br, Rect);
                }

                using (var br = new SolidBrush(Color.White))
                {
                    G.FillRectangle(br, InnerWindow_1);
                }

                Image CloseBtn;

                if (!ToolWindow)
                {
                    if (CloseRect.Height >= 27)
                    {
                        CloseBtn = Properties.Resources.Win8_Close_3;
                    }
                    else if (CloseRect.Height >= 24)
                    {
                        CloseBtn = Properties.Resources.Win8_Close_2;
                    }
                    else if (CloseRect.Height >= 21)
                    {
                        CloseBtn = Properties.Resources.Win8_Close_1;
                    }
                    else
                    {
                        CloseBtn = Properties.Resources.Win8_Close_0;
                    }
                }

                else
                {
                    CloseBtn = Properties.Resources.Win8_Close_ToolWindow;
                }

                if (Preview == Preview_Enum.W8Lite)
                    CloseBtn = CloseBtn.ReplaceColor(Color.FromArgb(255, 255, 255), Color.Black);

                if (!(Preview == Preview_Enum.W8Lite))
                {
                    using (var P = new Pen(Color.FromArgb(170, bc.CB((float)-0.2d))))
                    {
                        G.DrawRectangle(P, InnerWindow_1);
                    }
                    using (var P = new Pen(Color.FromArgb((int)Math.Round(Win7ColorBal / 100d * 255d), c.CB((float)-0.2d))))
                    {
                        G.DrawRectangle(P, InnerWindow_1);
                    }

                    G.SmoothingMode = SmoothingMode.HighSpeed;
                    using (var br = new SolidBrush(Active ? Color.FromArgb(199, 80, 80) : Color.FromArgb(188, 188, 188)))
                    {
                        G.FillRectangle(br, CloseRect);
                    }
                    G.SmoothingMode = SmoothingMode.AntiAlias;

                    G.DrawImage(CloseBtn, new Rectangle((int)Math.Round(CloseRect.X + (CloseRect.Width - CloseBtn.Width) / 2d), (int)Math.Round(CloseRect.Y + (CloseRect.Height - CloseBtn.Height) / 2d), CloseBtn.Width, CloseBtn.Height));

                    using (var P = new Pen(Color.FromArgb(200, bc.CB((float)-0.3d))))
                    {
                        G.DrawRectangle(P, Rect);
                    }
                    using (var P = new Pen(Color.FromArgb((int)Math.Round(Win7ColorBal / 100d * 255d), c.CB((float)-0.3d))))
                    {
                        G.DrawRectangle(P, Rect);
                    }
                }

                else
                {

                    using (var P = new Pen(Color.FromArgb((int)Math.Round(Win7ColorBal / 100d * 255d), c).LightLight()))
                    {
                        G.DrawLine(P, new Point(InnerWindow_1.X, InnerWindow_1.Y), new Point(InnerWindow_1.X + InnerWindow_1.Width, InnerWindow_1.Y));
                    }

                    using (var P = new Pen(Color.FromArgb((int)Math.Round(Win7ColorBal / 100d * 255d), c).LightLight()))
                    {
                        G.DrawLine(P, new Point(InnerWindow_1.X, InnerWindow_1.Y + InnerWindow_1.Height), new Point(InnerWindow_1.X + InnerWindow_1.Width, InnerWindow_1.Y + InnerWindow_1.Height));
                    }

                    using (var br = new SolidBrush(Active ? Color.FromArgb(195, 90, 80) : Color.Transparent))
                    {
                        G.FillRectangle(br, CloseRect);
                    }

                    G.SmoothingMode = SmoothingMode.HighSpeed;
                    using (var P = new Pen(Active ? Color.FromArgb(92, 58, 55) : Color.FromArgb(93, 96, 102)))
                    {
                        G.DrawRectangle(P, CloseRect);
                    }
                    G.SmoothingMode = SmoothingMode.AntiAlias;

                    G.DrawImage(CloseBtn, new Rectangle((int)Math.Round(CloseRect.X + (CloseRect.Width - CloseBtn.Width) / 2d), (int)Math.Round(CloseRect.Y + (CloseRect.Height - CloseBtn.Height) / 2d), CloseBtn.Width, CloseBtn.Height));

                    G.DrawRectangle(new Pen(Color.FromArgb(47, 48, 51)), Rect);
                }
            }
            #endregion

            else if (Preview == Preview_Enum.W7Aero | Preview == Preview_Enum.W7Opaque | Preview == Preview_Enum.W7Basic)
            {
                #region Windows 7\Vista
                var InnerWindow_1 = new Rectangle();
                var InnerWindow_2 = new Rectangle();
                var RectSide1 = new Rectangle();
                var RectSide2 = new Rectangle();
                int Sum = Metrics_BorderWidth + Metrics_PaddedBorderWidth;
                if (Sum < 2)
                    Sum = 2;

                TitleTextH = (int)Math.Round("ABCabc0123xYz.#".Measure(Font).Height);
                TitleTextH_9 = (int)Math.Round("ABCabc0123xYz.#".Measure(new Font(Font.Name, 9f, Font.Style)).Height);
                TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9 - 5);
                int Sum_Ttl = Sum + Metrics_CaptionHeight + TitleTextH_Sum;

                InnerWindow_1 = new Rectangle(Rect.X + Sum + 1, Rect.Y + Sum_Ttl, Rect.Width - Sum * 2 - 2, Rect.Height - (Sum + Sum_Ttl) - 2);
                InnerWindow_2 = new Rectangle(InnerWindow_1.X + 1, InnerWindow_1.Y + 1, InnerWindow_1.Width - 2, InnerWindow_1.Height - 2);

                RectSide1 = new Rectangle(Rect.X + 1, InnerWindow_1.Y, Sum, (int)Math.Round(InnerWindow_1.Height * 0.5d));
                RectSide2 = new Rectangle(InnerWindow_1.Right - 1, RectSide1.Y, RectSide1.Width + 1, RectSide1.Height);


                if (Preview != Preview_Enum.W7Basic)
                {

                    #region Aero
                    if (Shadow & Active & !DesignMode)
                    {
                        G.DrawGlow(Rect, Color.FromArgb(150, 0, 0, 0), 5, 15);
                    }

                    int Radius = 5;

                    if (!(Preview == Preview_Enum.W7Opaque))
                    {
                        var bk = AdaptedBackBlurred;

                        decimal alpha = 1 - (decimal)Win7Alpha / 100;   // ColorBlurBalance
                        decimal ColBal = (decimal)Win7ColorBal / 100;   // ColorBalance
                        decimal GlowBal = (decimal)Win7GlowBal / 100;   // AfterGlowBalance

                        var Color1 = Active ? AccentColor_Active : AccentColor_Inactive;
                        var Color2 = Active ? AccentColor2_Active : AccentColor2_Inactive;
                        G.ExcludeClip(InnerWindow_1);
                        G.DrawAeroEffect(Rect, bk, Color1, ColBal, Color2, GlowBal, alpha, Radius, !ToolWindow);
                        G.ResetClip();
                    }

                    else if (!ToolWindow)
                    {
                        using (var br = new SolidBrush(Color.White))
                        {
                            G.FillRoundedRect(br, Rect, Radius, true);
                        }
                        using (var br = new SolidBrush(Color.FromArgb((int)Math.Round(255 * Win7Alpha / 100d), Active ? AccentColor_Active : AccentColor_Inactive)))
                        {
                            G.FillRoundedRect(br, Rect, Radius, true);
                        }
                    }
                    else
                    {
                        using (var br = new SolidBrush(Color.White))
                        {
                            G.FillRectangle(br, Rect);
                        }
                        using (var br = new SolidBrush(Color.FromArgb((int)Math.Round(255 * Win7Alpha / 100d), Active ? AccentColor_Active : AccentColor_Inactive)))
                        {
                            G.FillRectangle(br, Rect);
                        }

                    }

                    if (Active)
                    {
                        G.DrawImage(Properties.Resources.Win7Sides, RectSide1);
                        G.DrawImage(Properties.Resources.Win7Sides, RectSide2);

                        int TitleTopW = (int)Math.Round(Rect.Width * 0.6d);
                        int TitleTopH = (int)Math.Round(Rect.Height * 0.6d);

                        G.DrawImage(Properties.Resources.Win7_TitleTopL, new Rectangle(Rect.X + (ToolWindow ? 0 : 1), Rect.Y + (ToolWindow ? 1 : 0), TitleTopW, TitleTopH));
                        G.DrawImage(Properties.Resources.Win7_TitleTopR, new Rectangle(Rect.X + Rect.Width - TitleTopW, Rect.Y + (ToolWindow ? 1 : 0), TitleTopW, TitleTopH));
                    }

                    var inner = new Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2);

                    if (!ToolWindow)
                    {
                        G.DrawRoundImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), Rect, Radius, true);
                        using (var P = new Pen(Color.FromArgb(Active ? 130 : 100, 25, 25, 25)))
                        {
                            G.DrawRoundedRect(P, Rect, Radius, true);
                        }
                        using (var P = new Pen(Color.FromArgb(100, 255, 255, 255)))
                        {
                            G.DrawRoundedRect(P, inner, Radius, true);
                        }
                        // Using P As New Pen(Color.FromArgb(255 - 255 * Win7Alpha / 300, BackColor, 0.2))) : DrawRect(G, P, Rect, Radius, True) : End Using
                        using (var P = new Pen(Color.FromArgb((int)Math.Round(255d - 255 * Win7Alpha / 300d), base.BackColor.Light(0.2f))))
                        {
                            G.DrawRoundedRect(P, InnerWindow_1, 1, true);
                        }
                        using (var br = new SolidBrush(Color.White))
                        {
                            G.FillRoundedRect(br, InnerWindow_1, 1, true);
                        }
                        using (var P = new Pen(Color.FromArgb((int)Math.Round(255d - 255 * Win7Alpha / 300d), base.BackColor.Dark(0.2f))))
                        {
                            G.DrawRoundedRect(P, InnerWindow_2, 1, true);
                        }
                    }
                    else
                    {
                        G.DrawImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), Rect);
                        using (var P = new Pen(Color.FromArgb(Active ? 130 : 100, 25, 25, 25)))
                        {
                            G.DrawRectangle(P, Rect);
                        }
                        using (var P = new Pen(Color.FromArgb(100, 255, 255, 255)))
                        {
                            G.DrawRectangle(P, inner);
                        }
                        // Using P As New Pen(Color.FromArgb(255 - 255 * Win7Alpha / 300, BackColor, 0.2))) : G.DrawRectangle(P, Rect) : End Using
                        using (var P = new Pen(Color.FromArgb((int)Math.Round(255d - 255 * Win7Alpha / 300d), base.BackColor.Light(0.2f))))
                        {
                            G.DrawRectangle(P, InnerWindow_1);
                        }
                        using (var br = new SolidBrush(Color.White))
                        {
                            G.FillRectangle(br, InnerWindow_1);
                        }
                        using (var P = new Pen(Color.FromArgb((int)Math.Round(255d - 255 * Win7Alpha / 300d), base.BackColor.Dark(0.2f))))
                        {
                            G.DrawRectangle(P, InnerWindow_2);
                        }
                    }


                    if (!ToolWindow)
                    {
                        Image closeBtn;
                        var CloseRect = new Rectangle();

                        if (Active)
                        {
                            if (!WinVista)
                            {
                                closeBtn = Properties.Resources.Win7_Close_Active;
                            }
                            else
                            {
                                closeBtn = Properties.Resources.Vista_Close_Active;
                            }
                        }
                        else if (!WinVista)
                        {
                            closeBtn = Properties.Resources.Win7_Close_inactive;
                        }
                        else
                        {
                            closeBtn = Properties.Resources.Vista_Close_inactive;
                        }

                        CloseRect = new Rectangle(Rect.X + Rect.Width - closeBtn.Width - 5, Rect.Y + 1, closeBtn.Width, closeBtn.Height);

                        G.DrawImage(closeBtn, CloseRect);
                    }

                    else
                    {

                        Color CloseUpperAccent1;
                        var CloseUpperAccent2 = default(Color);
                        var CloseLowerAccent1 = default(Color);
                        Color CloseLowerAccent2;
                        Color CloseOuterBorder;
                        Color CloseInnerBorder;

                        if (Active)
                        {
                            CloseUpperAccent1 = Color.FromArgb(233, 169, 156);
                            CloseUpperAccent2 = Color.FromArgb(223, 149, 135);
                            CloseLowerAccent1 = Color.FromArgb(184, 67, 44);
                            CloseLowerAccent2 = Color.FromArgb(210, 127, 110);
                            CloseOuterBorder = Color.FromArgb(67, 20, 34);
                            CloseInnerBorder = Color.FromArgb(100, 255, 255, 255);
                        }
                        else
                        {
                            CloseUpperAccent1 = Color.FromArgb(50, 189, 203, 218);
                            CloseLowerAccent2 = Color.FromArgb(50, 205, 219, 234);
                            CloseOuterBorder = Color.FromArgb(131, 142, 168);
                            CloseInnerBorder = Color.FromArgb(50, 209, 219, 229);
                        }

                        int Btn_Height = Metrics_CaptionHeight + TitleTextH_Sum - 5;
                        int Btn_Width = Btn_Height;

                        var CloseRect = new Rectangle(InnerWindow_1.Right - Btn_Width - 3, (int)Math.Round(Rect.Y + (Sum_Ttl - Btn_Height) / 2d), Btn_Width, Btn_Height);

                        if (Active)
                        {
                            float Factor = 0.5f;

                            float UH = Factor * CloseRect.Height;
                            float LH = CloseRect.Height - UH;
                            float Interlapping = UH / CloseRect.Height * 10f;

                            var CloseRectUpperHalf = new Rectangle(CloseRect.X, CloseRect.Y, CloseRect.Width, (int)Math.Round(UH + Interlapping));
                            var CloseUpperPath = new LinearGradientBrush(CloseRectUpperHalf, CloseUpperAccent1, CloseUpperAccent2, LinearGradientMode.Vertical);

                            var CloseRectLowerHalf = new Rectangle(CloseRect.X, (int)Math.Round(CloseRectUpperHalf.Bottom - Interlapping), CloseRect.Width, (int)Math.Round(LH));
                            var CloseLowerPath = new LinearGradientBrush(CloseRectLowerHalf, CloseLowerAccent1, CloseLowerAccent2, LinearGradientMode.Vertical);

                            G.FillRoundedRect(CloseUpperPath, CloseRectUpperHalf, 1, true);
                            G.FillRoundedRect(CloseLowerPath, CloseRectLowerHalf, 1, true);
                        }
                        else
                        {
                            var ClosePath = new LinearGradientBrush(CloseRect, CloseUpperAccent1, CloseLowerAccent2, LinearGradientMode.Vertical);
                            G.FillRectangle(ClosePath, CloseRect);
                        }


                        Image CloseBtn;

                        if (!ToolWindow)
                        {
                            if (CloseRect.Height >= 22)
                            {
                                CloseBtn = Properties.Resources.Win7_Basic_Close_2;
                            }
                            else if (CloseRect.Height >= 18)
                            {
                                CloseBtn = Properties.Resources.Win7_Basic_Close_1;
                            }
                            else
                            {
                                CloseBtn = Properties.Resources.Win7_Basic_Close_0;
                            }
                        }
                        else
                        {
                            CloseBtn = Properties.Resources.Win7_Basic_Close_ToolWindow;
                        }

                        int xW = CloseRect.Width % 2 == 0 ? CloseBtn.Width + 1 : CloseBtn.Width;
                        int xH = CloseRect.Height % 2 == 0 ? CloseBtn.Height + 1 : CloseBtn.Height;


                        var closerenderrect = new Rectangle((int)Math.Round(CloseRect.X + (CloseRect.Width - xW) / 2d), (int)Math.Round(CloseRect.Y + (CloseRect.Height - xH) / 2d), xW, xH);

                        G.DrawImage(CloseBtn, closerenderrect);

                        using (var P = new Pen(CloseOuterBorder))
                        {
                            G.DrawRoundedRect(P, CloseRect, 1, true);
                        }
                        using (var P = new Pen(CloseInnerBorder))
                        {
                            G.DrawRectangle(P, new Rectangle(CloseRect.X + 1, CloseRect.Y + 1, CloseRect.Width - 2, CloseRect.Height - 2));
                        }

                    }
                }

                #endregion

                else
                {

                    #region Basic
                    Sum = Metrics_BorderWidth + Metrics_PaddedBorderWidth;
                    TitleTextH = (int)Math.Round("ABCabc0123xYz.#".Measure(Font).Height);
                    TitleTextH_9 = (int)Math.Round("ABCabc0123xYz.#".Measure(new Font(Font.Name, 9f, Font.Style)).Height);
                    TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9 - 5);
                    Sum_Ttl = Sum + Metrics_CaptionHeight + TitleTextH_Sum;

                    InnerWindow_1 = new Rectangle(Rect.X + Sum + 2, Rect.Y + Sum_Ttl + 3, Rect.Width - Sum * 2 - 4, Rect.Height - (Sum + Sum_Ttl) - 5);
                    InnerWindow_2 = new Rectangle(InnerWindow_1.X + 1, InnerWindow_1.Y + 1, InnerWindow_1.Width - 2, InnerWindow_1.Height - 2);
                    RectSide1 = new Rectangle(Rect.X + 1, InnerWindow_1.Y, Sum + 1, (int)Math.Round(InnerWindow_1.Height * 0.5d));
                    RectSide2 = new Rectangle(InnerWindow_1.Right - 1, RectSide1.Y, RectSide1.Width + 1, RectSide1.Height);


                    Color Titlebar_Backcolor1;
                    Color Titlebar_Backcolor2;
                    Color Titlebar_OuterBorder;
                    Color Titlebar_InnerBorder;
                    Color Titlebar_Turquoise;
                    Color OuterBorder;

                    Color CloseUpperAccent1;
                    var CloseUpperAccent2 = default(Color);
                    var CloseLowerAccent1 = default(Color);
                    Color CloseLowerAccent2;
                    Color CloseOuterBorder;
                    Color CloseInnerBorder;

                    if (Active)
                    {
                        Titlebar_Backcolor1 = Color.FromArgb(152, 180, 208);
                        Titlebar_Backcolor2 = Color.FromArgb(186, 210, 234);
                        Titlebar_OuterBorder = Color.FromArgb(52, 52, 52);
                        Titlebar_InnerBorder = Color.FromArgb(255, 255, 255);
                        Titlebar_Turquoise = Color.FromArgb(40, 207, 228);
                        OuterBorder = Color.FromArgb(0, 0, 0);

                        CloseUpperAccent1 = Color.FromArgb(233, 169, 156);
                        CloseUpperAccent2 = Color.FromArgb(223, 149, 135);
                        CloseLowerAccent1 = Color.FromArgb(184, 67, 44);
                        CloseLowerAccent2 = Color.FromArgb(210, 127, 110);
                        CloseOuterBorder = Color.FromArgb(67, 20, 34);
                        CloseInnerBorder = Color.FromArgb(100, 255, 255, 255);
                    }

                    else
                    {
                        Titlebar_Backcolor1 = Color.FromArgb(191, 205, 219);
                        Titlebar_Backcolor2 = Color.FromArgb(215, 228, 242);
                        Titlebar_OuterBorder = Color.FromArgb(76, 76, 76);
                        Titlebar_InnerBorder = Color.FromArgb(226, 230, 239);
                        Titlebar_Turquoise = Color.FromArgb(226, 230, 239);
                        OuterBorder = Color.FromArgb(76, 76, 76);

                        CloseUpperAccent1 = Color.FromArgb(189, 203, 218);
                        CloseLowerAccent2 = Color.FromArgb(205, 219, 234);
                        CloseOuterBorder = Color.FromArgb(131, 142, 168);
                        CloseInnerBorder = Color.FromArgb(209, 219, 229);
                    }

                    var UpperPart = new Rectangle(Rect.X, Rect.Y, Rect.Width + 1, Sum_Ttl + 4);

                    G.SetClip(UpperPart);

                    var pth_back = new LinearGradientBrush(UpperPart, Titlebar_Backcolor1, Titlebar_Backcolor2, LinearGradientMode.Vertical);
                    var pth_line = new LinearGradientBrush(UpperPart, Titlebar_InnerBorder, Titlebar_Turquoise, LinearGradientMode.Vertical);

                    // ### Render Titlebar
                    if (!ToolWindow)
                    {
                        G.FillRoundedRect(pth_back, Rect, Radius, true);
                        using (var P = new Pen(Titlebar_OuterBorder))
                        {
                            G.DrawRoundedRect(P, Rect, Radius, true);
                        }
                        using (var P = new Pen(Titlebar_InnerBorder))
                        {
                            G.DrawRoundedRect(P, new Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2), Radius, true);
                        }
                        G.SetClip(new Rectangle((int)Math.Round(UpperPart.X + UpperPart.Width * 0.75d), UpperPart.Y, (int)Math.Round(UpperPart.Width * 0.75d), UpperPart.Height));
                        using (var P = new Pen(pth_line))
                        {
                            G.DrawRoundedRect(P, new Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2), Radius, true);
                        }
                    }
                    else
                    {
                        G.FillRectangle(pth_back, Rect);
                        using (var P = new Pen(Titlebar_OuterBorder))
                        {
                            G.DrawRectangle(P, Rect);
                        }
                        using (var P = new Pen(Titlebar_InnerBorder))
                        {
                            G.DrawRectangle(P, new Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2));
                        }
                        G.SetClip(new Rectangle((int)Math.Round(UpperPart.X + UpperPart.Width * 0.75d), UpperPart.Y, (int)Math.Round(UpperPart.Width * 0.75d), UpperPart.Height));
                        using (var P = new Pen(pth_line))
                        {
                            G.DrawRectangle(P, new Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2));
                        }
                    }

                    G.ResetClip();
                    G.ExcludeClip(UpperPart);

                    // ### Render Rest of WindowR
                    using (var br = new SolidBrush(Titlebar_Backcolor2))
                    {
                        G.FillRectangle(br, Rect);
                    }
                    using (var P = new Pen(Titlebar_Turquoise))
                    {
                        G.DrawRectangle(P, new Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2));
                    }
                    using (var P = new Pen(OuterBorder))
                    {
                        G.DrawRectangle(P, Rect);
                    }
                    using (var P = new Pen(Titlebar_InnerBorder))
                    {
                        G.DrawLine(P, new Point(Rect.X + 1, Rect.Y), new Point(Rect.X + 1, Rect.Y + Rect.Height - 2));
                    }
                    if (Active)
                    {
                        G.DrawImage(Properties.Resources.Win7Sides, RectSide1);
                        G.DrawImage(Properties.Resources.Win7Sides, RectSide2);
                    }
                    G.ResetClip();
                    G.FillRectangle(Brushes.White, InnerWindow_1);
                    using (var P = new Pen(Color.FromArgb(186, 210, 234)))
                    {
                        G.DrawRectangle(P, InnerWindow_1);
                    }
                    if (!WinVista)
                    {
                        using (var P = new Pen(Color.FromArgb(130, 135, 144)))
                        {
                            G.DrawRectangle(P, InnerWindow_2);
                        }
                    }

                    // ### Render Close ButtonR
                    var CloseRect = new Rectangle();

                    int Btn_Height = Metrics_CaptionHeight + TitleTextH_Sum - 5;
                    int Btn_Width;

                    if (!ToolWindow)
                    {
                        Btn_Width = (int)Math.Round(31d / 17d * Btn_Height);
                    }
                    else
                    {
                        Btn_Width = Btn_Height;
                    }

                    CloseRect = new Rectangle(InnerWindow_1.Right - Btn_Width - 3, InnerWindow_1.Top - Btn_Height - 3, Btn_Width, Btn_Height);

                    if (Active)
                    {
                        float Factor = 0.45f;
                        if (ToolWindow)
                            Factor = 0.2f;

                        float UH = Factor * CloseRect.Height;
                        float LH = CloseRect.Height - UH;
                        float Interlapping = UH / CloseRect.Height * 10f;

                        var CloseRectUpperHalf = new Rectangle(CloseRect.X, CloseRect.Y, CloseRect.Width, (int)Math.Round(UH + Interlapping));
                        var CloseUpperPath = new LinearGradientBrush(CloseRectUpperHalf, CloseUpperAccent1, CloseUpperAccent2, LinearGradientMode.Vertical);

                        var CloseRectLowerHalf = new Rectangle(CloseRect.X, (int)Math.Round(CloseRectUpperHalf.Bottom - Interlapping), CloseRect.Width, (int)Math.Round(LH));
                        var CloseLowerPath = new LinearGradientBrush(CloseRectLowerHalf, CloseLowerAccent1, CloseLowerAccent2, LinearGradientMode.Vertical);

                        G.FillRoundedRect(CloseUpperPath, CloseRectUpperHalf, 1, true);
                        G.FillRoundedRect(CloseLowerPath, CloseRectLowerHalf, 1, true);
                    }
                    else
                    {
                        var ClosePath = new LinearGradientBrush(CloseRect, CloseUpperAccent1, CloseLowerAccent2, LinearGradientMode.Vertical);
                        G.FillRectangle(ClosePath, CloseRect);
                    }


                    Image CloseBtn;

                    if (!ToolWindow)
                    {
                        if (CloseRect.Height >= 22)
                        {
                            CloseBtn = Properties.Resources.Win7_Basic_Close_2;
                        }
                        else if (CloseRect.Height >= 18)
                        {
                            CloseBtn = Properties.Resources.Win7_Basic_Close_1;
                        }
                        else
                        {
                            CloseBtn = Properties.Resources.Win7_Basic_Close_0;
                        }
                    }
                    else
                    {
                        CloseBtn = Properties.Resources.Win7_Basic_Close_ToolWindow;
                    }


                    G.DrawImage(CloseBtn, new Point((int)Math.Round(CloseRect.X + (CloseRect.Width - CloseBtn.Width) / 2d + 1d), (int)Math.Round(CloseRect.Y + (CloseRect.Height - CloseBtn.Height) / 2d)));

                    using (var P = new Pen(CloseOuterBorder))
                    {
                        G.DrawRoundedRect(P, CloseRect, 1, true);
                    }
                    using (var P = new Pen(CloseInnerBorder))
                    {
                        G.DrawRoundedRect(P, new Rectangle(CloseRect.X + 1, CloseRect.Y + 1, CloseRect.Width - 2, CloseRect.Height - 2), 1, true);
                    }

                    IconRect = new Rectangle(InnerWindow_1.X + 4, (int)Math.Round(CloseRect.Top + (CloseRect.Height - IconSize) / 2d), IconSize, IconSize);

                    LabelRect = new Rectangle(IconRect.Right + 3, CloseRect.Y, UpperPart.Width - (IconRect.Right + 4), CloseRect.Height);

                    if (ToolWindow)
                        LabelRect.X = IconRect.X;
                    #endregion

                }
            }
            #endregion

            else if (Preview == Preview_Enum.WXP)
            {
                #region Windows XP
                var sm = G.SmoothingMode;
                G.SmoothingMode = SmoothingMode.HighSpeed;

                TitlebarRect = new Rectangle(Rect.X, Rect.Y, Rect.Width, TitleTextH_Sum + _Metrics_BorderWidth + _Metrics_CaptionHeight + 5);

                var innerRect = new Rectangle(Rect.X, Rect.Y + TitlebarRect.Height - 1, Rect.Width - 2, Rect.Height - TitlebarRect.Height - 1);

                using (var br = new SolidBrush(Program.resVS.Colors.Btnface))
                {
                    G.FillRectangle(br, innerRect);
                }

                Program.resVS.Draw(G, TitlebarRect, VisualStylesRes.Element.Titlebar, Active, ToolWindow);

                var LE = new Rectangle(Rect.X, Rect.Y + TitlebarRect.Height - 1, Math.Max(4, Metrics_BorderWidth), Rect.Height - TitlebarRect.Height - Math.Max(4, Metrics_BorderWidth) + 2);
                var RE = new Rectangle(Rect.X + Rect.Width - Math.Max(4, Metrics_BorderWidth) - 1, Rect.Y + TitlebarRect.Height - 1, Math.Max(4, Metrics_BorderWidth), Rect.Height - TitlebarRect.Height - Metrics_BorderWidth + 2);
                var BE = new Rectangle(Rect.X, Rect.Y + Rect.Height - Math.Max(4, Metrics_BorderWidth), Rect.Width - 1, Math.Max(4, Metrics_BorderWidth) + 1);
                int CloseBtn_W = TitleTextH_Sum + _Metrics_CaptionHeight - 4;
                var CB = new Rectangle(Rect.X + Rect.Width - CloseBtn_W - RE.Width - 2, Rect.Y + TitlebarRect.Height - 4 - CloseBtn_W, CloseBtn_W, CloseBtn_W);

                if (!ToolWindow)
                {
                    LabelRect = new Rectangle(Rect.X + LE.Width + 20, Rect.Y + TitlebarRect.Height - 4 - CloseBtn_W, Rect.Width - CloseBtn_W - LE.Width - RE.Width, CloseBtn_W);
                }
                else
                {
                    LabelRect = new Rectangle(Rect.X + LE.Width + 2, Rect.Y + TitlebarRect.Height - 4 - CloseBtn_W, Rect.Width - CloseBtn_W - LE.Width - RE.Width, CloseBtn_W);
                }

                IconRect = new Rectangle(Rect.X + LE.Width + 2, (int)Math.Round(Rect.Y + (TitlebarRect.Height - 14) / 2d), 14, 14);

                Program.resVS.Draw(G, TitlebarRect, VisualStylesRes.Element.Titlebar, Active, ToolWindow);
                Program.resVS.Draw(G, LE, VisualStylesRes.Element.LeftEdge, Active, ToolWindow);
                Program.resVS.Draw(G, RE, VisualStylesRes.Element.RightEdge, Active, ToolWindow);
                Program.resVS.Draw(G, BE, VisualStylesRes.Element.BottomEdge, Active, ToolWindow);
                Program.resVS.Draw(G, CB, VisualStylesRes.Element.CloseButton, Active, ToolWindow);

                G.SmoothingMode = sm;
                #endregion

            }

            Color ForeColorX;
            Bitmap closeImg;

            if (AccentColor_Enabled)
            {
                if (Active)
                {
                    ForeColorX = AccentColor_Active.IsDark() ? Color.White : Color.Black;
                    closeImg = AccentColor_Active.IsDark() ? Properties.Resources.Win10x_Close_Dark : Properties.Resources.Win10x_Close_Light;
                }
                else
                {
                    ForeColorX = AccentColor_Inactive.IsDark() ? Color.FromArgb(115, 115, 115) : Color.Black;
                    closeImg = AccentColor_Inactive.IsDark() ? Properties.Resources.Win10x_Close_Dark : Properties.Resources.Win10x_Close_Light;
                }
            }

            else if (Active)
            {
                if (Preview == Preview_Enum.W11)
                {
                    ForeColorX = DarkMode ? Color.White : Color.Black;
                    closeImg = DarkMode ? Properties.Resources.Win10x_Close_Dark : Properties.Resources.Win10x_Close_Light;
                }
                else if (DarkMode)
                {
                    ForeColorX = Color.White;
                    closeImg = Properties.Resources.Win10x_Close_Dark;
                }
                else
                {
                    ForeColorX = Color.Black;
                    closeImg = Properties.Resources.Win10x_Close_Light;
                }
            }
            else
            {
                ForeColorX = Color.FromArgb(115, 115, 115);
                closeImg = DarkMode ? Properties.Resources.Win10x_Close_Dark : Properties.Resources.Win10x_Close_Light;
            }

            if (!ToolWindow)
                G.DrawImage(Active ? Properties.Resources.SampleApp_Small_Active : Properties.Resources.SampleApp_Small_Inactive, IconRect);

            if (Preview == Preview_Enum.W11 | Preview == Preview_Enum.W10)
            {
                using (var br = new SolidBrush(ForeColorX))
                {
                    G.DrawString(Text, Font, br, LabelRect, ContentAlignment.MiddleLeft.ToStringFormat());
                }

                if (!ToolWindow)
                {
                    var r = new Rectangle((int)Math.Round(XRect.X + (XRect.Width - closeImg.Width) / 2d), (int)Math.Round(XRect.Y + (XRect.Height - closeImg.Height) / 2d), closeImg.Width, closeImg.Height);
                    G.DrawImage(closeImg, r);
                }
                else
                {
                    var XXRect = new Rectangle(Rect.X + Rect.Width - 2 - (TitlebarRect.Height - 12), Rect.Y + 6, TitlebarRect.Height - 12, TitlebarRect.Height - 12);

                    using (var br = new SolidBrush(Color.FromArgb(199, 80, 80)))
                    {
                        G.FillRectangle(br, XXRect);
                    }

                    if (XXRect.Width >= 12)
                    {
                        if (XXRect.Width % 2 == 0)
                        {
                            XXRect.X += 1;
                            XXRect.Y += 1;
                        }
                    }
                    else
                    {
                        XXRect.X += 1;
                    }

                    using (var br = new SolidBrush(Color.White))
                    {
                        G.DrawString("r", new Font("Marlett", 6.35f, FontStyle.Regular), br, new Rectangle(XXRect.X + 1, XXRect.Y + 1, XXRect.Width, XXRect.Height), ContentAlignment.MiddleCenter.ToStringFormat());
                    }
                }
            }

            else if (Preview == Preview_Enum.W8)
            {
                using (var br = new SolidBrush(Color.Black))
                {
                    G.DrawString(Text, Font, br, LabelRect8, ContentAlignment.MiddleCenter.ToStringFormat());
                }
            }

            else if (Preview == Preview_Enum.W8Lite)
            {
                if (Active)
                {
                    using (var br = new SolidBrush(Program.TM.Win32.TitleText))
                    {
                        G.DrawString(Text, Font, br, LabelRect8, ContentAlignment.MiddleCenter.ToStringFormat());
                    }
                }
                else
                {
                    using (var br = new SolidBrush(Program.TM.Win32.InactiveTitleText))
                    {
                        G.DrawString(Text, Font, br, LabelRect8, ContentAlignment.MiddleCenter.ToStringFormat());
                    }
                }
            }

            else if (Preview == Preview_Enum.W7Aero | Preview == Preview_Enum.W7Opaque)
            {
                var LabelRectModified = LabelRect;
                LabelRectModified.X -= 2;
                LabelRectModified.Y -= 1;
                int alpha = Active ? 120 : 75;
                G.DrawGlowString(1, Text, Font, Color.Black, Color.FromArgb(alpha, Color.White), RectBK, LabelRectModified, ContentAlignment.MiddleLeft.ToStringFormat());
            }

            else if (Preview == Preview_Enum.W7Basic)
            {
                using (var br = new SolidBrush(Active ? Color.Black : Color.FromArgb(76, 76, 76)))
                {
                    G.DrawString(Text, Font, br, LabelRect, ContentAlignment.MiddleLeft.ToStringFormat());
                }
            }

            else if (Preview == Preview_Enum.WXP)
            {
                using (var br = new SolidBrush(Color.Black))
                {
                    G.DrawString(Text, Font, br, new Rectangle(LabelRect.X + 1, LabelRect.Y, LabelRect.Width, LabelRect.Height), ContentAlignment.MiddleLeft.ToStringFormat());
                }
                using (var br = new SolidBrush(Color.White))
                {
                    G.DrawString(Text, Font, br, LabelRect, ContentAlignment.MiddleLeft.ToStringFormat());
                }

            }

        }

    }

}