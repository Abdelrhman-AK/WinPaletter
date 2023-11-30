using ImageProcessor;
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
            Font = new("Segoe UI", 9f);
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
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
            get => _DarkMode;
            set
            {
                if (value != _DarkMode)
                {
                    _DarkMode = value;

                    if (!SuspendRefresh) Refresh();
                }
            }
        }

        private bool _AccentColor_Enabled = true;
        public bool AccentColor_Enabled
        {
            get => _AccentColor_Enabled;
            set
            {
                if (value != _AccentColor_Enabled)
                {
                    _AccentColor_Enabled = value;

                    if (!SuspendRefresh) Refresh();
                }
            }
        }

        private float _Win7Noise = 1f;
        public float Win7Noise
        {
            get => _Win7Noise;
            set
            {
                if (value != _Win7Noise)
                {
                    _Win7Noise = value;

                    if (Preview == Preview_Enum.W7Aero | Preview == Preview_Enum.W7Opaque | Preview == Preview_Enum.W7Basic)
                    {
                        try { Noise7 = Properties.Resources.AeroGlass.Fade((double)(Win7Noise / 100f)); } catch { }
                    }

                    if (!SuspendRefresh) Refresh();
                }
            }
        }

        private int _Metrics_CaptionHeight = 22;
        public int Metrics_CaptionHeight
        {
            get => _Metrics_CaptionHeight;
            set
            {
                if (value != _Metrics_CaptionHeight)
                {
                    _Metrics_CaptionHeight = value;
                    AdjustPadding();
                    if (!SuspendRefresh) Refresh();
                    MetricsChanged?.Invoke();
                }
            }
        }

        private int _Metrics_BorderWidth = 1;
        public int Metrics_BorderWidth
        {
            get => _Metrics_BorderWidth;
            set
            {
                if (value != _Metrics_BorderWidth)
                {
                    _Metrics_BorderWidth = value;
                    AdjustPadding();
                    if (!SuspendRefresh) Refresh();
                    MetricsChanged?.Invoke();
                }
            }
        }

        private int _Metrics_PaddedBorderWidth = 4;
        public int Metrics_PaddedBorderWidth
        {
            get => _Metrics_PaddedBorderWidth;
            set
            {
                if (value != _Metrics_PaddedBorderWidth)
                {
                    _Metrics_PaddedBorderWidth = value;
                    AdjustPadding();
                    if (!SuspendRefresh) Refresh();
                    MetricsChanged?.Invoke();
                }
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
                CreateParams cpar = base.CreateParams;
                if (!DesignMode)
                {
                    cpar.ExStyle |= 0x20;
                    return cpar;
                }
                else
                {
                    return cpar;
                }
            }
        }

        #endregion

        #region Events

        public event MetricsChangedEventHandler MetricsChanged;

        public delegate void MetricsChangedEventHandler();

        protected override void OnHandleCreated(EventArgs e)
        {
            if (!DesignMode) ProcessBack();

            base.OnHandleCreated(e);
        }

        protected override void OnBackgroundImageChanged(EventArgs e)
        {
            if (!DesignMode) ProcessBack();

            base.OnBackgroundImageChanged(e);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            Title_x_Height = (int)Math.Round(TitleScheme.Measure(Font).Height);
            Title_9_Height = (int)Math.Round(TitleScheme.Measure(new Font(Font.Name, 9f, Font.Style)).Height);

            AdjustPadding();

            base.OnFontChanged(e);
        }

        public void ProcessBack()
        {
            Bitmap Wallpaper = Parent.BackgroundImage is null ? Program.Wallpaper : (Bitmap)Parent.BackgroundImage;

            try { if (Wallpaper is not null) { AdaptedBack = Wallpaper.Clone(Bounds, Wallpaper.PixelFormat); } } catch { }

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
                                using (ImageFactory ImgF = new())
                                {
                                    ImgF.Load(b);
                                    ImgF.Saturation(15);
                                    ImgF.Brightness(-10);
                                    AdaptedBackBlurred = (Bitmap)ImgF.Image.Clone();
                                }
                            }
                        }

                        else { AdaptedBackBlurred = b; }
                    }
                }

                else if (AdaptedBack is not null) { AdaptedBackBlurred  = new Bitmap(AdaptedBack).Blur(3); }
            }
            catch { }

            try { Noise7 = Properties.Resources.AeroGlass.Fade(Win7Noise / 100); } catch { }
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
            Padding = new(ClientRect.Left + 1, ClientRect.Top + 1, ClientRect.Left + 1, ClientRect.Left + 1);
        }

        public void FillSemiRect(Graphics Graphics, Brush Brush, Rectangle Rectangle, int Radius = -1)
        {
            try
            {
                if (Radius == -1)
                    Radius = 6;

                if (Graphics is null)
                    return;

                Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                using (GraphicsPath path = RoundedSemiRectangle(Rectangle, Radius))
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
        #endregion

        #region Geometry/Design
        private string TitleScheme => "ABCabc0123xYz.#";

        private int Title_x_Height, Title_9_Height;
        private int SideSize
        {
            get
            {
                if (Preview == Preview_Enum.W11 || Preview == Preview_Enum.W10)
                {
                    return 1;
                }

                else if (Preview == Preview_Enum.WXP)
                {
                    return Math.Max(4, _Metrics_BorderWidth);
                }

                else
                {
                    return _Metrics_BorderWidth + _Metrics_PaddedBorderWidth;
                }
            }
        }
        private int IconSize => _Metrics_CaptionHeight <= 17 ? 12 : 14;
        private int TitleHeight => Math.Max(0, Title_x_Height - Title_9_Height - 5);
        private int CloseBtn_W => TitleHeight + _Metrics_CaptionHeight - 4;

        private Rectangle CloseButtonRect;
        private Rectangle RectAll => new(0, 0, Width - 1, Height - 1);
        private Rectangle Rect => new(RectAll.X + FreeMargin, RectAll.Y + FreeMargin, RectAll.Width - (FreeMargin * 2), RectAll.Height - (FreeMargin * 2));
        private Rectangle RectBorder => new(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2);
        private Rectangle TitlebarRect => new(Rect.X, Rect.Y, Rect.Width, TitleHeight + _Metrics_BorderWidth + _Metrics_CaptionHeight + _Metrics_PaddedBorderWidth + 3);
        private Rectangle IconRect
        {
            get
            {
                if (Preview == Preview_Enum.W11 || Preview == Preview_Enum.W10)
                {
                    return new(TitlebarRect.X + 4 + _Metrics_PaddedBorderWidth + _Metrics_BorderWidth, (int)Math.Round(Rect.Y + (TitlebarRect.Height - IconSize) / 2d), IconSize, IconSize);
                }

                else if (Preview == Preview_Enum.W8 | Preview == Preview_Enum.W8Lite)
                {
                    return new(TitlebarRect.X + 6 + _Metrics_PaddedBorderWidth + _Metrics_BorderWidth, (int)Math.Round(Rect.Y + (TitlebarRect.Height - IconSize) / 2d), IconSize, IconSize);
                }

                else if (Preview == Preview_Enum.W7Basic)
                {
                    return new Rectangle(ClientRect.X + 4, (int)Math.Round(ClientRect.Top + (ClientRect.Height - IconSize) / 2d), IconSize, IconSize);
                }

                else if (Preview == Preview_Enum.WXP)
                {
                    return new Rectangle(Rect.X + SideSize + 4, TitlebarRect.Bottom - 8 - 14, 14, 14);
                }

                else
                {
                    return new(TitlebarRect.X + 4 + _Metrics_PaddedBorderWidth + _Metrics_BorderWidth, (int)Math.Round(Rect.Y + (TitlebarRect.Height - IconSize) / 2d), IconSize, IconSize);
                }
            }
        }
        private Rectangle LabelRect
        {
            get
            {
                Rectangle resultRect;

                if (Preview == Preview_Enum.W8 || Preview == Preview_Enum.W8Lite)
                {
                    resultRect = new(Rect.X, Rect.Y + 2, TitlebarRect.Width - 1, TitlebarRect.Height - 3);
                }
                else if (Preview == Preview_Enum.W7Aero || Preview == Preview_Enum.W7Opaque)
                {
                    int offsetX = !ToolWindow ? IconRect.Right + 2 : IconRect.X - 2;
                    resultRect = new(offsetX, TitlebarRect.Y, TitlebarRect.Width - (IconRect.Right + 4), TitlebarRect.Height);
                }
                else if (Preview == Preview_Enum.W7Basic)
                {
                    int offsetX = !ToolWindow ? IconRect.Right + 3 : IconRect.X;
                    resultRect = new(offsetX, CloseButtonRect.Y, TitlebarRect.Width - (IconRect.Right + 4), CloseButtonRect.Height);
                }
                else if (Preview == Preview_Enum.WXP)
                {
                    int offsetX = Rect.X + SideSize + (ToolWindow ? 3 : 21);
                    resultRect = new(offsetX, TitlebarRect.Bottom - 5 - CloseBtn_W, Rect.Width - CloseBtn_W - SideSize * 2, CloseBtn_W);
                }
                else
                {
                    int offsetX = !ToolWindow ? IconRect.Right + 4 : IconRect.X;
                    resultRect = new(offsetX, TitlebarRect.Y, TitlebarRect.Width - (IconRect.Right + 4), TitlebarRect.Height);
                }

                return resultRect;
            }
        }
        private Rectangle ClientRect => new(Rect.X + SideSize + 1, Rect.Y + TitlebarRect.Height + 1, Rect.Width - SideSize * 2 - 2, Rect.Height - (SideSize + TitlebarRect.Height) - 2);
        private Rectangle ClientBorderRect => new(ClientRect.X - 1, ClientRect.Y - 1, ClientRect.Width + 2, ClientRect.Height + 2);
        private Rectangle GlassSide1 => new(Rect.X + 1, ClientBorderRect.Y, SideSize, (int)Math.Round(ClientBorderRect.Height * 0.5d));
        private Rectangle GlassSide2 => new(ClientBorderRect.Right - 1, GlassSide1.Y, GlassSide1.Width + 1, GlassSide1.Height);
        private Rectangle LeftEdge_XP => new(ClientRect.Left - SideSize, ClientRect.Y, SideSize, ClientRect.Height + SideSize);
        private Rectangle RightEdge_XP => new(ClientRect.Right + 1, ClientRect.Y, SideSize, ClientRect.Height + SideSize);
        private Rectangle ButtomEdge_XP => new(LeftEdge_XP.Left, LeftEdge_XP.Bottom - SideSize + 1, RightEdge_XP.Right - LeftEdge_XP.Left, SideSize + 1);

        Color Titlebar_Backcolor1 => Active ? Color.FromArgb(152, 180, 208) : Color.FromArgb(191, 205, 219);
        Color Titlebar_Backcolor2 => Active ? Color.FromArgb(186, 210, 234) : Color.FromArgb(215, 228, 242);
        Color Titlebar_OuterBorder => Active ? Color.FromArgb(52, 52, 52) : Color.FromArgb(76, 76, 76);
        Color Titlebar_InnerBorder => Active ? Color.FromArgb(255, 255, 255) : Color.FromArgb(226, 230, 239);
        Color Titlebar_Turquoise => Active ? Color.FromArgb(40, 207, 228) : Color.FromArgb(226, 230, 239);
        Color OuterBorder => Active ? Color.FromArgb(0, 0, 0) : Color.FromArgb(76, 76, 76);
        Color CloseUpperAccent1 => Active ? Color.FromArgb(233, 169, 156) : Color.FromArgb(189, 203, 218);
        Color CloseUpperAccent2 => Active ? Color.FromArgb(223, 149, 135) : default;
        Color CloseLowerAccent1 => Active ? Color.FromArgb(184, 67, 44) : default;
        Color CloseLowerAccent2 => Active ? Color.FromArgb(210, 127, 110) : Color.FromArgb(205, 219, 234);
        Color CloseOuterBorder => Active ? Color.FromArgb(67, 20, 34) : Color.FromArgb(131, 142, 168);
        Color CloseInnerBorder => Active ? Color.FromArgb(100, 255, 255, 255) : Color.FromArgb(209, 219, 229);
        Color InactiveWindows8 => !(Preview == Preview_Enum.W8Lite) ? Color.FromArgb(235, 235, 235) : Color.FromArgb((int)Math.Round(Win7ColorBal / 100d * 255d), AccentColor_Active.CB(0.8f));
        Color Windows8Color => Active ? Color.FromArgb((int)Math.Round(Win7ColorBal / 100d * 255d), AccentColor_Active) : InactiveWindows8;
        Color WindowsXPColor => Program.TM.Win32.ButtonFace;
        Color ClientAreaColor => !DesignMode ? Program.TM.Win32.ButtonFace : Color.White;
        Color CaptionColor
        {
            get
            {
                if (Preview == Preview_Enum.W11 || Preview == Preview_Enum.W10)
                {
                    if (AccentColor_Enabled)
                    {
                        if (Active)
                        {
                            return AccentColor_Active.IsDark() ? Color.White : Color.Black;
                        }
                        else
                        {
                            return AccentColor_Inactive.IsDark() ? Color.FromArgb(115, 115, 115) : Color.Black;
                        }
                    }

                    else
                    {
                        return Active ? (DarkMode ? Color.White : Color.Black) : Color.FromArgb(115, 115, 115);
                    }
                }

                else if (Preview == Preview_Enum.W8)
                {
                    return Color.Black;
                }

                else if (Preview == Preview_Enum.W7Basic)
                {
                    return Active ? Color.Black : Color.FromArgb(76, 76, 76);
                }

                else
                {
                    return Active ? Program.TM.Win32.TitleText : Program.TM.Win32.InactiveTitleText;
                }
            }
        }

        Bitmap CloseButtonImage
        {
            get
            {
                if (AccentColor_Enabled)
                {
                    if (Active)
                    {
                        return AccentColor_Active.IsDark() ? Properties.Resources.Win10x_Close_Dark : Properties.Resources.Win10x_Close_Light;
                    }
                    else
                    {
                        return AccentColor_Inactive.IsDark() ? Properties.Resources.Win10x_Close_Dark : Properties.Resources.Win10x_Close_Light;
                    }
                }

                else if (Active)
                {
                    if (Preview == Preview_Enum.W11)
                    {
                        return DarkMode ? Properties.Resources.Win10x_Close_Dark : Properties.Resources.Win10x_Close_Light;
                    }
                    else if (DarkMode)
                    {
                        return Properties.Resources.Win10x_Close_Dark;
                    }
                    else
                    {
                        return Properties.Resources.Win10x_Close_Light;
                    }
                }

                else
                {
                    return DarkMode ? Properties.Resources.Win10x_Close_Dark : Properties.Resources.Win10x_Close_Light;
                }
            }
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this == null) return;

            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = Program.Style.RenderingHint;
            DoubleBuffered = true;

            //Draw window itself
            if (Preview == Preview_Enum.W11)
            {
                {
                    if (Shadow & Active & !DesignMode) { G.DrawGlow(Rect, Color.FromArgb(150, 0, 0, 0), 5, 15); }

                    if (!AccentColor_Enabled && Active)
                    {
                        G.SetClip(TitlebarRect);
                        G.DrawRoundImage(AdaptedBackBlurred, Rect, Radius, true);
                        G.ResetClip();
                    }

                    G.ExcludeClip(TitlebarRect);

                    if (DarkMode) { using (SolidBrush br = new(Color.FromArgb(20, 20, 20))) { G.FillRoundedRect(br, Rect, Radius, true); } }

                    else { using (SolidBrush br = new(Color.FromArgb(240, 240, 240))) { G.FillRoundedRect(br, Rect, Radius, true); } }

                    G.ResetClip();


                    if (AccentColor_Enabled)
                    {
                        if (Active) { using (Pen P = new(Color.FromArgb(200, AccentColor_Active))) { G.DrawRoundedRect(P, Rect, Radius, true); } }

                        else { using (Pen P = new(Color.FromArgb(200, AccentColor_Inactive))) { G.DrawRoundedRect(P, Rect, Radius, true); } }
                    }

                    else if (DarkMode) { using (Pen P = new(Color.FromArgb(200, 100, 100, 100))) { G.DrawRoundedRect(P, Rect, Radius, true); } }

                    else { using (Pen P = new(Color.FromArgb(200, 220, 220, 220))) { G.DrawRoundedRect(P, Rect, Radius, true); } }

                    if (AccentColor_Enabled)
                    {
                        if (Active)
                        {
                            using (SolidBrush br = new(Color.FromArgb(255, AccentColor_Active))) { FillSemiRect(G, br, TitlebarRect, Radius); }

                            using (Pen P = new(Color.FromArgb(255, AccentColor_Active)))
                            {
                                G.DrawLine(P, new Point(TitlebarRect.X + 1, TitlebarRect.Y + TitlebarRect.Height), new Point(TitlebarRect.X + TitlebarRect.Width - 1, TitlebarRect.Y + TitlebarRect.Height));
                            }
                        }
                        else
                        {
                            using (SolidBrush br = new(Color.FromArgb(255, AccentColor_Inactive))) { FillSemiRect(G, br, TitlebarRect, Radius); }

                            using (Pen P = new(Color.FromArgb(255, AccentColor_Inactive)))
                            {
                                G.DrawLine(P, new Point(TitlebarRect.X + 1, TitlebarRect.Y + TitlebarRect.Height), new Point(TitlebarRect.X + TitlebarRect.Width - 1, TitlebarRect.Y + TitlebarRect.Height));
                            }
                        }
                    }

                    else
                    {
                        int a = Active ? DarkMode ? 180 : 245 : 255;

                        if (DarkMode) { using (SolidBrush br = new(Color.FromArgb(a, 32, 32, 32))) { FillSemiRect(G, br, TitlebarRect, Radius); } }

                        else { using (SolidBrush br = new(Color.FromArgb(a, 245, 245, 245))) { FillSemiRect(G, br, TitlebarRect, Radius); } }
                    }

                    //Close button
                    if (!ToolWindow)
                    {
                        using (Bitmap closeImg = new(CloseButtonImage))
                        {
                            CloseButtonRect = new(TitlebarRect.Right - closeImg.Width * 2, TitlebarRect.Top + (TitlebarRect.Height - closeImg.Height) / 2, closeImg.Width, closeImg.Height);
                            G.DrawImage(closeImg, CloseButtonRect);
                        }
                    }

                    else
                    {
                        CloseButtonRect = new(TitlebarRect.Right - 2 - (TitlebarRect.Height - 12), Rect.Y + 6, TitlebarRect.Height - 12, TitlebarRect.Height - 12);

                        using (SolidBrush br = new(Color.FromArgb(199, 80, 80)))
                        {
                            G.FillRectangle(br, CloseButtonRect);
                        }

                        if (CloseButtonRect.Width >= 12)
                        {
                            if (CloseButtonRect.Width % 2 == 0)
                            {
                                CloseButtonRect.X += 1;
                                CloseButtonRect.Y += 1;
                            }
                        }
                        else
                        {
                            CloseButtonRect.X += 1;
                        }

                        using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
                        {
                            using (SolidBrush br = new(Color.White))
                            {
                                G.DrawString("r", new Font("Marlett", 6.35f, FontStyle.Regular), br, new Rectangle(CloseButtonRect.X + 1, CloseButtonRect.Y + 1, CloseButtonRect.Width, CloseButtonRect.Height), sf);
                            }
                        }
                    }

                    //Window title
                    using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat())
                    {
                        using (SolidBrush br = new(CaptionColor))
                        {
                            G.DrawString(Text, Font, br, LabelRect, sf);
                        }
                    }
                }
            }

            else if (Preview == Preview_Enum.W10)
            {
                {
                    if (Shadow & Active & !DesignMode) { G.DrawGlow(Rect, Color.FromArgb(150, 0, 0, 0), 5, 15); }

                    if (DarkMode) { using (SolidBrush br = new(Color.FromArgb(20, 20, 20))) { G.FillRectangle(br, Rect); } }

                    else { using (SolidBrush br = new(Color.FromArgb(240, 240, 240))) { G.FillRectangle(br, Rect); } }

                    if (AccentColor_Enabled)
                    {
                        if (Active) { using (SolidBrush br = new(Color.FromArgb(255, AccentColor_Active))) { G.FillRectangle(br, TitlebarRect); } }

                        else { using (SolidBrush br = new(Color.FromArgb(255, AccentColor_Inactive))) { G.FillRectangle(br, TitlebarRect); } }
                    }

                    else if (Active)
                    {
                        if (DarkMode) { G.FillRectangle(Brushes.Black, TitlebarRect); }

                        else { G.FillRectangle(Brushes.White, TitlebarRect); }
                    }

                    else if (DarkMode) { using (SolidBrush br = new(Color.FromArgb(43, 43, 43))) { G.FillRectangle(br, TitlebarRect); } }

                    else { G.FillRectangle(Brushes.White, TitlebarRect); }

                    if (AccentColor_Enabled)
                    {
                        if (Active) { using (Pen P = new(Color.FromArgb(200, AccentColor_Active))) { G.DrawRectangle(P, Rect); } }

                        else { using (Pen P = new(Color.FromArgb(200, AccentColor_Inactive))) { G.DrawRectangle(P, Rect); } }

                    }
                    else if (DarkMode) { using (Pen P = new(Color.FromArgb(125, 100, 100, 100))) { G.DrawRectangle(P, Rect); } }

                    else { using (Pen P = new(Color.FromArgb(125, 220, 220, 220))) { G.DrawRectangle(P, Rect); } }

                    //Close button
                    if (!ToolWindow)
                    {
                        using (Bitmap closeImg = new(CloseButtonImage))
                        {
                            CloseButtonRect = new(TitlebarRect.Right - closeImg.Width * 2, TitlebarRect.Top + (TitlebarRect.Height - closeImg.Height) / 2, closeImg.Width, closeImg.Height);
                            G.DrawImage(closeImg, CloseButtonRect);
                        }
                    }

                    else
                    {
                        CloseButtonRect = new(TitlebarRect.Right - 2 - (TitlebarRect.Height - 12), Rect.Y + 6, TitlebarRect.Height - 12, TitlebarRect.Height - 12);

                        using (SolidBrush br = new(Color.FromArgb(199, 80, 80)))
                        {
                            G.FillRectangle(br, CloseButtonRect);
                        }

                        if (CloseButtonRect.Width >= 12)
                        {
                            if (CloseButtonRect.Width % 2 == 0)
                            {
                                CloseButtonRect.X += 1;
                                CloseButtonRect.Y += 1;
                            }
                        }
                        else
                        {
                            CloseButtonRect.X += 1;
                        }

                        using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
                        {
                            using (SolidBrush br = new(Color.White))
                            {
                                G.DrawString("r", new Font("Marlett", 6.35f, FontStyle.Regular), br, new Rectangle(CloseButtonRect.X + 1, CloseButtonRect.Y + 1, CloseButtonRect.Width, CloseButtonRect.Height), sf);
                            }
                        }
                    }

                    //Window title
                    using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat())
                    {
                        using (SolidBrush br = new(CaptionColor))
                        {
                            G.DrawString(Text, Font, br, LabelRect, sf);
                        }
                    }
                }
            }

            else if (Preview == Preview_Enum.W8 | Preview == Preview_Enum.W8Lite)
            {
                {
                    int CloseRectH = Metrics_CaptionHeight + TitleHeight - 2 + (Preview == Preview_Enum.W8Lite ? 1 : 0);

                    int CloseRectW = (int)Math.Round((!ToolWindow ? CloseRectH * 3 / 2d : CloseRectH) + (Preview == Preview_Enum.W8Lite ? 2 : 0));

                    if (!ToolWindow)
                    {
                        if (!(Preview == Preview_Enum.W8Lite)) { CloseButtonRect = new(ClientRect.Right - CloseRectW + 1, Rect.Y + 1, CloseRectW, CloseRectH); }

                        else { CloseButtonRect = new(ClientRect.Right - CloseRectW + 2, Rect.Y, CloseRectW, CloseRectH); }
                    }

                    else { CloseButtonRect = new(ClientRect.Right - CloseRectW + 1, Rect.Y + 1, CloseRectW, CloseRectH); }

                    Color BorderColor = Color.FromArgb(217, 217, 217);

                    using (SolidBrush br = new(BorderColor)) { G.FillRectangle(br, Rect); }
                    using (SolidBrush br = new(Windows8Color)) { G.FillRectangle(br, Rect); }
                    using (SolidBrush br = new(ClientAreaColor)) { G.FillRectangle(br, ClientRect); }

                    Bitmap CloseBtn;

                    if (!ToolWindow)
                    {
                        if (CloseButtonRect.Height >= 27) { CloseBtn = Properties.Resources.Win8_Close_3; }

                        else if (CloseButtonRect.Height >= 24) { CloseBtn = Properties.Resources.Win8_Close_2; }

                        else if (CloseButtonRect.Height >= 21) { CloseBtn = Properties.Resources.Win8_Close_1; }

                        else { CloseBtn = Properties.Resources.Win8_Close_0; }
                    }

                    else { CloseBtn = Properties.Resources.Win8_Close_ToolWindow; }

                    if (!(Preview == Preview_Enum.W8Lite))
                    {
                        using (Pen P = new(Color.FromArgb(170, BorderColor.CB((float)-0.2d)))) { G.DrawRectangle(P, ClientRect); }

                        using (Pen P = new(Color.FromArgb((int)Math.Round(Win7ColorBal / 100d * 255d), Windows8Color.CB((float)-0.2d)))) { G.DrawRectangle(P, ClientRect); }

                        G.SmoothingMode = SmoothingMode.HighSpeed;
                        using (SolidBrush br = new(Active ? Color.FromArgb(199, 80, 80) : Color.FromArgb(188, 188, 188))) { G.FillRectangle(br, CloseButtonRect); }
                        G.SmoothingMode = SmoothingMode.AntiAlias;

                        G.DrawImage(CloseBtn, new Rectangle((int)Math.Round(CloseButtonRect.X + (CloseButtonRect.Width - CloseBtn.Width) / 2d), (int)Math.Round(CloseButtonRect.Y + (CloseButtonRect.Height - CloseBtn.Height) / 2d), CloseBtn.Width, CloseBtn.Height));

                        using (Pen P = new(Color.FromArgb(200, BorderColor.CB((float)-0.3d)))) { G.DrawRectangle(P, Rect); }

                        using (Pen P = new(Color.FromArgb((int)Math.Round(Win7ColorBal / 100d * 255d), Windows8Color.CB((float)-0.3d)))) { G.DrawRectangle(P, Rect); }

                        //Window title
                        using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
                        {
                            using (SolidBrush br = new(CaptionColor))
                            {
                                G.DrawString(Text, Font, br, LabelRect, sf);
                            }
                        }
                    }

                    else
                    {
                        CloseBtn = CloseBtn.ReplaceColor(Color.FromArgb(255, 255, 255), Color.Black);

                        using (Pen P = new(Color.FromArgb((int)Math.Round(Win7ColorBal / 100d * 255d), Windows8Color).LightLight()))
                        {
                            G.DrawLine(P, new Point(ClientRect.X, ClientRect.Y), new Point(ClientRect.X + ClientRect.Width, ClientRect.Y));
                        }

                        using (Pen P = new(Color.FromArgb((int)Math.Round(Win7ColorBal / 100d * 255d), Windows8Color).LightLight()))
                        {
                            G.DrawLine(P, new Point(ClientRect.X, ClientRect.Y + ClientRect.Height), new Point(ClientRect.X + ClientRect.Width, ClientRect.Y + ClientRect.Height));
                        }

                        using (SolidBrush br = new(Active ? Color.FromArgb(195, 90, 80) : Color.Transparent)) { G.FillRectangle(br, CloseButtonRect); }

                        G.SmoothingMode = SmoothingMode.HighSpeed;
                        using (Pen P = new(Active ? Color.FromArgb(92, 58, 55) : Color.FromArgb(93, 96, 102))) { G.DrawRectangle(P, CloseButtonRect); }
                        G.SmoothingMode = SmoothingMode.AntiAlias;

                        G.DrawImage(CloseBtn, new Rectangle((int)Math.Round(CloseButtonRect.X + (CloseButtonRect.Width - CloseBtn.Width) / 2d), (int)Math.Round(CloseButtonRect.Y + (CloseButtonRect.Height - CloseBtn.Height) / 2d), CloseBtn.Width, CloseBtn.Height));

                        using (Pen P = new(Color.FromArgb(47, 48, 51))) { G.DrawRectangle(P, Rect); }

                        //Window title
                        using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
                        {
                            using (SolidBrush br = new(CaptionColor))
                            {
                                G.DrawString(Text, Font, br, LabelRect, sf);
                            }
                        }
                    }
                }
            }

            else if (Preview == Preview_Enum.W7Aero | Preview == Preview_Enum.W7Opaque | Preview == Preview_Enum.W7Basic)
            {
                {
                    if (Preview != Preview_Enum.W7Basic)
                    {
                        #region Aero
                        if (Shadow & Active & !DesignMode) { G.DrawGlow(Rect, Color.FromArgb(150, 0, 0, 0), 5, 15); }

                        int Radius = 5;

                        //Background aero effect
                        if (!(Preview == Preview_Enum.W7Opaque))
                        {
                            Bitmap bk = AdaptedBackBlurred;

                            decimal alpha = 1 - (decimal)Win7Alpha / 100;   // ColorBlurBalance
                            decimal ColBal = (decimal)Win7ColorBal / 100;   // ColorBalance
                            decimal GlowBal = (decimal)Win7GlowBal / 100;   // AfterGlowBalance

                            Color Color1 = Active ? AccentColor_Active : AccentColor_Inactive;
                            Color Color2 = Active ? AccentColor2_Active : AccentColor2_Inactive;
                            G.ExcludeClip(ClientBorderRect);
                            G.DrawAeroEffect(Rect, bk, Color1, ColBal, Color2, GlowBal, alpha, Radius, !ToolWindow);
                            G.ResetClip();
                        }

                        else if (!ToolWindow)
                        {
                            using (SolidBrush br = new(Color.White)) { G.FillRoundedRect(br, Rect, Radius, true); }

                            using (SolidBrush br = new(Color.FromArgb((int)Math.Round(255 * Win7Alpha / 100d), Active ? AccentColor_Active : AccentColor_Inactive)))
                            {
                                G.FillRoundedRect(br, Rect, Radius, true);
                            }
                        }

                        else
                        {
                            using (SolidBrush br = new(Color.White)) { G.FillRectangle(br, Rect); }

                            using (SolidBrush br = new(Color.FromArgb((int)Math.Round(255 * Win7Alpha / 100d), Active ? AccentColor_Active : AccentColor_Inactive)))
                            {
                                G.FillRectangle(br, Rect);
                            }
                        }

                        //Right and left glow panels
                        if (Active)
                        {
                            G.DrawImage(Properties.Resources.Win7Sides, GlassSide1);
                            G.DrawImage(Properties.Resources.Win7Sides, GlassSide2);

                            int TitleTopW = (int)Math.Round(Rect.Width * 0.6d);
                            int TitleTopH = (int)Math.Round(Rect.Height * 0.6d);

                            G.SetClip(RectBorder);
                            G.DrawImage(Properties.Resources.Win7_TitleTopL, new Rectangle(Rect.X + (ToolWindow ? -1 : 1), Rect.Y, TitleTopW, TitleTopH));
                            G.DrawImage(Properties.Resources.Win7_TitleTopR, new Rectangle(Rect.X + Rect.Width - TitleTopW + 1, Rect.Y, TitleTopW, TitleTopH));
                            G.ResetClip();
                        }

                        //Window borders
                        if (!ToolWindow)
                        {
                            G.DrawRoundImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), Rect, Radius, true);

                            using (Pen P = new(Color.FromArgb(Active ? 130 : 100, 25, 25, 25))) { G.DrawRoundedRect(P, Rect, Radius, true); }

                            using (Pen P = new(Color.FromArgb(100, 255, 255, 255))) { G.DrawRoundedRect(P, RectBorder, Radius, true); }

                            using (Pen P = new(Color.FromArgb((int)Math.Round(255d - 255 * Win7Alpha / 300d), base.BackColor.Light(0.2f)))) { G.DrawRoundedRect(P, ClientBorderRect, 1, true); }

                            using (SolidBrush br = new(ClientAreaColor)) { G.FillRoundedRect(br, ClientBorderRect, 1, true); }

                            using (Pen P = new(Color.FromArgb((int)Math.Round(255d - 255 * Win7Alpha / 300d), base.BackColor.Dark(0.2f)))) { G.DrawRoundedRect(P, ClientRect, 1, true); }
                        }

                        else
                        {
                            G.DrawImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), Rect);

                            using (Pen P = new(Color.FromArgb(Active ? 130 : 100, 25, 25, 25))) { G.DrawRectangle(P, Rect); }

                            using (Pen P = new(Color.FromArgb(100, 255, 255, 255))) { G.DrawRectangle(P, RectBorder); }

                            using (Pen P = new(Color.FromArgb((int)Math.Round(255d - 255 * Win7Alpha / 300d), base.BackColor.Light(0.2f)))) { G.DrawRectangle(P, ClientBorderRect); }

                            using (SolidBrush br = new(ClientAreaColor)) { G.FillRectangle(br, ClientBorderRect); }

                            using (Pen P = new(Color.FromArgb((int)Math.Round(255d - 255 * Win7Alpha / 300d), base.BackColor.Dark(0.2f)))) { G.DrawRectangle(P, ClientRect); }
                        }

                        //Close button
                        if (!ToolWindow)
                        {
                            Bitmap closeBtn;

                            if (Active)
                            {
                                if (!WinVista) { closeBtn = Properties.Resources.Win7_Close_Active; }

                                else { closeBtn = Properties.Resources.Vista_Close_Active; }
                            }
                            else if (!WinVista) { closeBtn = Properties.Resources.Win7_Close_inactive; }

                            else { closeBtn = Properties.Resources.Vista_Close_inactive; }

                            CloseButtonRect = new(Rect.X + Rect.Width - closeBtn.Width - 5, Rect.Y + 1, closeBtn.Width, closeBtn.Height);

                            G.DrawImage(closeBtn, CloseButtonRect);
                        }

                        else
                        {
                            int Btn_Height = Math.Max(10, _Metrics_CaptionHeight - 5);
                            int Btn_Width = Btn_Height;

                            CloseButtonRect = new(ClientBorderRect.Right - Btn_Width - 3, (int)Math.Round(Rect.Y + (TitlebarRect.Height - Btn_Height) / 2d), Btn_Width, Btn_Height);

                            if (Active)
                            {
                                float Factor = 0.5f;

                                float UH = Factor * CloseButtonRect.Height;
                                float LH = CloseButtonRect.Height - UH;
                                float Interlapping = UH / CloseButtonRect.Height * 10f;

                                Rectangle CloseRectUpperHalf = new(CloseButtonRect.X, CloseButtonRect.Y, CloseButtonRect.Width, (int)Math.Round(UH + Interlapping));
                                LinearGradientBrush CloseUpperPath = new(CloseRectUpperHalf, Color.FromArgb(50, CloseUpperAccent1), CloseUpperAccent2, LinearGradientMode.Vertical);

                                Rectangle CloseRectLowerHalf = new(CloseButtonRect.X, (int)Math.Round(CloseRectUpperHalf.Bottom - Interlapping), CloseButtonRect.Width, (int)Math.Round(LH));
                                LinearGradientBrush CloseLowerPath = new(CloseRectLowerHalf, CloseLowerAccent1, Color.FromArgb(50, CloseLowerAccent2), LinearGradientMode.Vertical);

                                G.FillRoundedRect(CloseUpperPath, CloseRectUpperHalf, 1, true);
                                G.FillRoundedRect(CloseLowerPath, CloseRectLowerHalf, 1, true);
                            }
                            else
                            {
                                LinearGradientBrush ClosePath = new(CloseButtonRect, Color.FromArgb(50, CloseUpperAccent1), Color.FromArgb(50, CloseLowerAccent2), LinearGradientMode.Vertical);
                                G.FillRectangle(ClosePath, CloseButtonRect);
                            }

                            Bitmap CloseBtn;

                            CloseBtn = Properties.Resources.Win7_Basic_Close_ToolWindow;

                            int xW = CloseButtonRect.Width % 2 == 0 ? CloseBtn.Width + 1 : CloseBtn.Width;
                            int xH = CloseButtonRect.Height % 2 == 0 ? CloseBtn.Height + 1 : CloseBtn.Height;

                            Rectangle closerenderrect = new((int)Math.Round(CloseButtonRect.X + (CloseButtonRect.Width - xW) / 2d), (int)Math.Round(CloseButtonRect.Y + (CloseButtonRect.Height - xH) / 2d), xW, xH);

                            G.DrawImage(CloseBtn, closerenderrect);

                            using (Pen P = new(CloseOuterBorder)) { G.DrawRoundedRect(P, CloseButtonRect, 1, true); }

                            using (Pen P = new(Color.FromArgb(50, CloseInnerBorder))) { G.DrawRectangle(P, new Rectangle(CloseButtonRect.X + 1, CloseButtonRect.Y + 1, CloseButtonRect.Width - 2, CloseButtonRect.Height - 2)); }

                        }

                        //Window title
                        int alpha_caption = Active ? 120 : 75;
                        using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat())
                        {
                            G.DrawGlowString(1, Text, Font, CaptionColor, Color.FromArgb(alpha_caption, Color.White), RectAll, LabelRect, sf);
                        }
                        #endregion
                    }

                    else
                    {
                        #region Basic
                        Rectangle UpperPart = new(Rect.X, Rect.Y, Rect.Width + 1, TitlebarRect.Height + 4);
                        Rectangle UpperPart_Modified = new((int)Math.Round(UpperPart.X + UpperPart.Width * 0.75d), UpperPart.Y, (int)Math.Round(UpperPart.Width * 0.75d), UpperPart.Height);

                        G.SetClip(UpperPart);

                        LinearGradientBrush pth_back = new(UpperPart, Titlebar_Backcolor1, Titlebar_Backcolor2, LinearGradientMode.Vertical);
                        LinearGradientBrush pth_line = new(UpperPart, Titlebar_InnerBorder, Titlebar_Turquoise, LinearGradientMode.Vertical);

                        // ### Render Titlebar
                        if (!ToolWindow)
                        {
                            G.FillRoundedRect(pth_back, Rect, Radius, true);
                            using (Pen P = new(Titlebar_OuterBorder)) { G.DrawRoundedRect(P, Rect, Radius, true); }
                            using (Pen P = new(Titlebar_InnerBorder)) { G.DrawRoundedRect(P, new Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2), Radius, true); }
                            G.SetClip(UpperPart_Modified);
                            using (Pen P = new(pth_line)) { G.DrawRoundedRect(P, new Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2), Radius, true); }
                        }
                        else
                        {
                            G.FillRectangle(pth_back, Rect);
                            using (Pen P = new(Titlebar_OuterBorder)) { G.DrawRectangle(P, Rect); }
                            using (Pen P = new(Titlebar_InnerBorder)) { G.DrawRectangle(P, new Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2)); }
                            G.SetClip(UpperPart_Modified);
                            using (Pen P = new(pth_line)) { G.DrawRectangle(P, new Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2)); }
                        }

                        G.ResetClip();
                        G.ExcludeClip(UpperPart);

                        using (SolidBrush br = new(Titlebar_Backcolor2)) { G.FillRectangle(br, Rect); }
                        using (Pen P = new(Titlebar_Turquoise)) { G.DrawRectangle(P, new Rectangle(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2)); }
                        using (Pen P = new(OuterBorder)) { G.DrawRectangle(P, Rect); }
                        using (Pen P = new(Titlebar_InnerBorder)) { G.DrawLine(P, new Point(Rect.X + 1, Rect.Y), new Point(Rect.X + 1, Rect.Y + Rect.Height - 2)); }

                        if (Active)
                        {
                            G.DrawImage(Properties.Resources.Win7Sides, GlassSide1);
                            G.DrawImage(Properties.Resources.Win7Sides, GlassSide2);
                        }

                        G.ResetClip();

                        G.FillRectangle(Brushes.White, ClientBorderRect);
                        using (Pen P = new(Color.FromArgb(186, 210, 234))) { G.DrawRectangle(P, ClientBorderRect); }
                        if (!WinVista) { using (Pen P = new(Color.FromArgb(130, 135, 144))) { G.DrawRectangle(P, ClientRect); } }

                        //Render close button
                        int Btn_Height = Math.Max(10, _Metrics_CaptionHeight - 5);
                        int Btn_Width;
                        Btn_Width = !ToolWindow ? (int)Math.Round(31d / 17d * Btn_Height) : Btn_Height;
                        Rectangle CloseRect = new(ClientBorderRect.Right - Btn_Width - 3, ClientBorderRect.Top - Btn_Height - 3, Btn_Width, Btn_Height);

                        if (Active)
                        {
                            float Factor = 0.45f;
                            if (ToolWindow)
                                Factor = 0.2f;

                            float UH = Factor * CloseRect.Height;
                            float LH = CloseRect.Height - UH;
                            float Interlapping = UH / CloseRect.Height * 10f;

                            Rectangle CloseRectUpperHalf = new(CloseRect.X, CloseRect.Y, CloseRect.Width, (int)Math.Round(UH + Interlapping));
                            Rectangle CloseRectLowerHalf = new(CloseRect.X, (int)Math.Round(CloseRectUpperHalf.Bottom - Interlapping), CloseRect.Width, (int)Math.Round(LH));

                            LinearGradientBrush CloseUpperPath = new(CloseRectUpperHalf, CloseUpperAccent1, CloseUpperAccent2, LinearGradientMode.Vertical);
                            LinearGradientBrush CloseLowerPath = new(CloseRectLowerHalf, CloseLowerAccent1, CloseLowerAccent2, LinearGradientMode.Vertical);

                            G.FillRoundedRect(CloseUpperPath, CloseRectUpperHalf, 1, true);
                            G.FillRoundedRect(CloseLowerPath, CloseRectLowerHalf, 1, true);
                        }

                        else
                        {
                            LinearGradientBrush ClosePath = new(CloseRect, CloseUpperAccent1, CloseLowerAccent2, LinearGradientMode.Vertical);
                            G.FillRectangle(ClosePath, CloseRect);
                        }

                        Bitmap CloseBtn;

                        if (!ToolWindow)
                        {
                            if (CloseRect.Height >= 22) { CloseBtn = Properties.Resources.Win7_Basic_Close_2; }
                            else if (CloseRect.Height >= 18) { CloseBtn = Properties.Resources.Win7_Basic_Close_1; }
                            else { CloseBtn = Properties.Resources.Win7_Basic_Close_0; }
                        }

                        else { CloseBtn = Properties.Resources.Win7_Basic_Close_ToolWindow; }

                        G.DrawImage(CloseBtn, new Point((int)Math.Round(CloseRect.X + (CloseRect.Width - CloseBtn.Width) / 2d + 1d), (int)Math.Round(CloseRect.Y + (CloseRect.Height - CloseBtn.Height) / 2d)));

                        using (Pen P = new(CloseOuterBorder)) { G.DrawRoundedRect(P, CloseRect, 1, true); }

                        using (Pen P = new(CloseInnerBorder)) { G.DrawRoundedRect(P, new Rectangle(CloseRect.X + 1, CloseRect.Y + 1, CloseRect.Width - 2, CloseRect.Height - 2), 1, true); }

                        //Window title
                        using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat())
                        {
                            using (SolidBrush br = new(CaptionColor))
                            {
                                G.DrawString(Text, Font, br, LabelRect, sf);
                            }
                        }
                        #endregion
                    }
                }
            }

            else if (Preview == Preview_Enum.WXP)
            {
                {
                    SmoothingMode sm = G.SmoothingMode;
                    G.SmoothingMode = SmoothingMode.HighSpeed;

                    using (SolidBrush br = new(WindowsXPColor)) { G.FillRectangle(br, ClientBorderRect); }

                    Program.resVS.Draw(G, TitlebarRect, VisualStylesRes.Element.Titlebar, Active, ToolWindow);

                    CloseButtonRect = new(ClientRect.Right - CloseBtn_W - RightEdge_XP.Width + 3, Rect.Y + TitlebarRect.Height - 4 - CloseBtn_W, CloseBtn_W, CloseBtn_W);

                    Program.resVS.Draw(G, LeftEdge_XP, VisualStylesRes.Element.LeftEdge, Active, ToolWindow);
                    Program.resVS.Draw(G, RightEdge_XP, VisualStylesRes.Element.RightEdge, Active, ToolWindow);
                    Program.resVS.Draw(G, ButtomEdge_XP, VisualStylesRes.Element.BottomEdge, Active, ToolWindow);
                    Program.resVS.Draw(G, CloseButtonRect, VisualStylesRes.Element.CloseButton, Active, ToolWindow);

                    G.SmoothingMode = sm;

                    //Window title
                    using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat())
                    {
                        using (SolidBrush br = new(Color.Black))
                        {
                            G.DrawString(Text, Font, br, new Rectangle(LabelRect.X + 1, LabelRect.Y, LabelRect.Width, LabelRect.Height), sf);
                        }

                        using (SolidBrush br = new(CaptionColor)) { G.DrawString(Text, Font, br, LabelRect, sf); }
                    }
                }
            }

            //Draw window icon
            if (!ToolWindow)
                G.DrawImage(Active ? Properties.Resources.SampleApp_Small_Active : Properties.Resources.SampleApp_Small_Inactive, IconRect);

            base.OnPaint(e);
        }
    }
}