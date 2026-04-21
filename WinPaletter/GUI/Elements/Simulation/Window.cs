using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;
using WinPaletter.Assets;
using WinPaletter.Properties;
using WinPaletter.Templates;
using WinPaletter.UI.Retro;

namespace WinPaletter.UI.Simulation
{
    [Description("A simulated window")]
    public class Window : UserControl
    {
        public Window()
        {
            AdjustPadding();
            Font = new Font("Segoe UI", 9f);
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Color.Transparent;
        }

        #region Variables

        // Checks design mode through multiple channels because DesignMode alone is unreliable when the control is hosted inside another designer-hosted control.
        private bool IsInDesignMode
        {
            get
            {
                if (DesignMode) return true;
                if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return true;

                ISite site = Site;
                if (site != null && site.DesignMode) return true;

                Control parent = Parent;
                while (parent != null)
                {
                    if (parent.Site != null && parent.Site.DesignMode) return true;
                    parent = parent.Parent;
                }

                return false;
            }
        }

        // Cached bitmaps derived from the parent/wallpaper background.
        // AdaptedBack is a cropped copy of the source; AdaptedBackBlurred is the post-processed version used for
        // acrylic/Aero rendering. Both are managed here and disposed when no longer needed.
        private Bitmap AdaptedBack, AdaptedBackBlurred;
        private Bitmap Noise7 = Win7Preview.AeroGlass;

        // Extra space around the outer window rect to allow the drop shadow to bleed without clipping.
        private readonly int FreeMargin = 8;

        private static readonly Font marlett_7_7 = new("Marlett", 7.7f, FontStyle.Bold);
        private static readonly Font marlett_6_35 = new("Marlett", 6.35f, FontStyle.Regular);

        public VisualStylesRes resVS;

        public enum Preview_Enum
        {
            W11,
            W11Lite,
            W10,
            W10Lite,
            W8,
            W8Lite,
            W7Aero,
            W7Opaque,
            W7Basic,
            WXP
        }

        // Grip handle size in pixels for the metrics editor overlay.
        readonly int GripSize = 6;

        // The dashed rectangle drawn around the full client area when the metrics editor is active.
        RectangleF editingRect;

        // Corner and edge drag handles for the metrics editor.
        RectangleF Grip_topLeft;
        RectangleF Grip_topRight;
        RectangleF Grip_bottomLeft;
        RectangleF Grip_bottomRight;
        RectangleF Grip_topCenter;
        RectangleF Grip_bottomCenter;
        RectangleF Grip_leftCenter;
        RectangleF Grip_rightCenter;

        // Tracks which grip is currently being dragged so OnMouseMove knows which metric to update.
        bool isMoving_Grip_topCenter = false;
        bool isMoving_Grip_padding_left = false;
        bool isMoving_Grip_borderWidth_left = false;
        bool isMoving_Grip_padding_right = false;
        bool isMoving_Grip_borderWidth_right = false;
        bool isMoving_Grip_padding_bottom = false;
        bool isMoving_Grip_borderWidth_bottom = false;

        #endregion

        #region Properties

        private bool _shadow = true;
        public bool Shadow
        {
            get => _shadow;
            set
            {
                if (value != _shadow)
                {
                    _shadow = value;
                    Invalidate();
                }
            }
        }

        private int _radius = 5;
        public int Radius
        {
            get => _radius;
            set
            {
                if (value != _radius)
                {
                    _radius = value;
                    Invalidate();
                }
            }
        }

        private Color _accentColor_Active = Color.FromArgb(0, 120, 212);
        public Color AccentColor_Active
        {
            get => _accentColor_Active;
            set
            {
                if (value != _accentColor_Active)
                {
                    _accentColor_Active = value;
                    Invalidate();
                }
            }
        }

        private Color _accentColor_Inactive = Color.FromArgb(32, 32, 32);
        public Color AccentColor_Inactive
        {
            get => _accentColor_Inactive;
            set
            {
                if (value != _accentColor_Inactive)
                {
                    _accentColor_Inactive = value;
                    Invalidate();
                }
            }
        }

        private Color _accentColor2_Active = Color.FromArgb(0, 120, 212);
        public Color AccentColor2_Active
        {
            get => _accentColor2_Active;
            set
            {
                if (value != _accentColor2_Active)
                {
                    _accentColor2_Active = value;
                    Invalidate();
                }
            }
        }

        private Color _accentColor2_Inactive = Color.FromArgb(32, 32, 32);
        public Color AccentColor2_Inactive
        {
            get => _accentColor2_Inactive;
            set
            {
                if (value != _accentColor2_Inactive)
                {
                    _accentColor2_Inactive = value;
                    Invalidate();
                }
            }
        }

        private bool _active = true;
        public bool Active
        {
            get => _active;
            set
            {
                if (value != _active)
                {
                    _active = value;
                    ProcessBack();
                    Invalidate();
                }
            }
        }

        private Preview_Enum _preview = Preview_Enum.W11;
        public Preview_Enum Preview
        {
            get => _preview;
            set
            {
                if (_preview != value)
                {
                    _preview = value;
                    Invalidate();
                }
            }
        }

        private int _win7Alpha = 100;
        public int Win7Alpha
        {
            get => _win7Alpha;
            set
            {
                if (value != _win7Alpha)
                {
                    _win7Alpha = value;
                    Invalidate();
                }
            }
        }

        private int _win7ColorBal = 100;
        public int Win7ColorBal
        {
            get => _win7ColorBal;
            set
            {
                if (value != _win7ColorBal)
                {
                    _win7ColorBal = value;
                    Invalidate();
                }
            }
        }

        private int _win7GlowBal = 100;
        public int Win7GlowBal
        {
            get => _win7GlowBal;
            set
            {
                if (value != _win7GlowBal)
                {
                    _win7GlowBal = value;
                    Invalidate();
                }
            }
        }

        private bool _toolWindow = false;
        public bool ToolWindow
        {
            get => _toolWindow;
            set
            {
                if (value != _toolWindow)
                {
                    _toolWindow = value;
                    Invalidate();
                }
            }
        }

        private bool _winVista = false;
        public bool WinVista
        {
            get => _winVista;
            set
            {
                if (value != _winVista)
                {
                    _winVista = value;
                    Invalidate();
                }
            }
        }

        private bool _DarkMode = true;
        public bool DarkMode
        {
            get => _DarkMode;
            set
            {
                if (value != _DarkMode)
                {
                    _DarkMode = value;
                    Invalidate();
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
                    ProcessBack();
                    Invalidate();
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
                        Noise7 = Win7Preview.AeroGlass.Fade(Win7Noise / 100f);
                    }

                    Invalidate();
                }
            }
        }

        private readonly int min_Metrics_CaptionHeight = 16;
        private readonly int max_Metrics_CaptionHeight = 50;

        private int _Metrics_CaptionHeight = 22;
        public int Metrics_CaptionHeight
        {
            get => _Metrics_CaptionHeight;
            set
            {
                if (value < min_Metrics_CaptionHeight) value = min_Metrics_CaptionHeight;
                if (value > max_Metrics_CaptionHeight) value = max_Metrics_CaptionHeight;

                if (value != _Metrics_CaptionHeight)
                {
                    _Metrics_CaptionHeight = value;
                    AdjustPadding();
                    Invalidate();
                    MetricsChanged?.Invoke();

                    if (!DesignMode && EnableEditingMetrics && isMoving_Grip_topCenter)
                        EditorInvoker?.Invoke(this, new(nameof(Metrics_CaptionHeight)));
                }
            }
        }

        private readonly int min_Metrics_BorderWidth = 0;
        private readonly int max_Metrics_BorderWidth = 50;

        private int _Metrics_BorderWidth = 1;
        public int Metrics_BorderWidth
        {
            get => _Metrics_BorderWidth;
            set
            {
                if (value < min_Metrics_BorderWidth) value = min_Metrics_BorderWidth;
                if (value > max_Metrics_BorderWidth) value = max_Metrics_BorderWidth;

                if (value != _Metrics_BorderWidth)
                {
                    _Metrics_BorderWidth = value;
                    AdjustPadding();
                    Invalidate();
                    MetricsChanged?.Invoke();

                    if (!DesignMode && EnableEditingMetrics && (isMoving_Grip_borderWidth_left || isMoving_Grip_borderWidth_right || isMoving_Grip_borderWidth_bottom))
                        EditorInvoker?.Invoke(this, new(nameof(Metrics_BorderWidth)));
                }
            }
        }

        private readonly int min_Metrics_PaddedBorderWidth = 0;
        private readonly int max_Metrics_PaddedBorderWidth = 50;

        private int _Metrics_PaddedBorderWidth = 4;
        public int Metrics_PaddedBorderWidth
        {
            get => _Metrics_PaddedBorderWidth;
            set
            {
                if (value < min_Metrics_PaddedBorderWidth) value = min_Metrics_PaddedBorderWidth;
                if (value > max_Metrics_PaddedBorderWidth) value = max_Metrics_PaddedBorderWidth;

                if (value != _Metrics_PaddedBorderWidth)
                {
                    _Metrics_PaddedBorderWidth = value;
                    AdjustPadding();
                    Invalidate();
                    MetricsChanged?.Invoke();

                    if (!DesignMode && EnableEditingMetrics && (isMoving_Grip_padding_right || isMoving_Grip_padding_left || isMoving_Grip_padding_bottom))
                        EditorInvoker?.Invoke(this, new(nameof(Metrics_PaddedBorderWidth)));
                }
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; }

        #endregion

        #region Events / Overrides

        public event MetricsChangedEventHandler MetricsChanged;
        public delegate void MetricsChangedEventHandler();

        protected override void OnBackgroundImageChanged(EventArgs e)
        {
            if (!DesignMode) ProcessBack();
            base.OnBackgroundImageChanged(e);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            Title_x_Height = TitleScheme.Measure(Font).Height;
            Title_9_Height = TitleScheme.Measure(new Font(Font.Name, 9f, Font.Style)).Height;
            AdjustPadding();
            base.OnFontChanged(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            AdjustPadding();
            base.OnSizeChanged(e);
        }

        // Regenerates the blurred/adapted background bitmaps used for W11 acrylic and W7 Aero glass.
        // Called whenever Active or AccentColor_Enabled changes, or the BackgroundImage is replaced.
        public void ProcessBack()
        {
            if (IsInDesignMode) return;
            if (Preview != Preview_Enum.W11 && Preview != Preview_Enum.W11Lite && Preview != Preview_Enum.W7Aero) return;

            Bitmap source = null;

            if (Parent?.BackgroundImage is Bitmap parentBmp)
                source = parentBmp;
            else
                source = Program.WallpaperMonitor.Get(Program.TM, Program.WindowStyle);

            if (source != null)
            {
                Rectangle srcRect = new(0, 0, source.Width, source.Height);
                if (srcRect.Contains_ButNotExceed(Bounds))
                {
                    AdaptedBack?.Dispose();
                    AdaptedBack = source.Clone(Bounds, source.PixelFormat);
                }
            }

            if (AdaptedBack == null) return;

            if (Preview == Preview_Enum.W11 || Preview == Preview_Enum.W11Lite)
            {
                if (Active && !AccentColor_Enabled)
                {
                    if (DarkMode)
                    {
                        using (Bitmap b = AdaptedBack.Blur(15))
                        {
                            if (b != null)
                            {
                                AdaptedBackBlurred?.Dispose();
                                AdaptedBackBlurred = b.Contrast(0.05f);
                            }
                        }
                    }
                    else
                    {
                        AdaptedBackBlurred?.Dispose();
                        AdaptedBackBlurred = AdaptedBack.Blur(15);
                    }
                }
            }
            else if (Preview == Preview_Enum.W7Aero)
            {
                AdaptedBackBlurred?.Dispose();
                AdaptedBackBlurred = AdaptedBack.Blur(3);
                Noise7 = Win7Preview.AeroGlass.Fade(Win7Noise / 100);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Noise7?.Dispose();
            AdaptedBack?.Dispose();
            AdaptedBackBlurred?.Dispose();
            resVS?.Dispose();
        }

        #endregion

        #region Methods

        // Recalculates the WinForms Padding to match the simulated window border metrics, and repositions all editor grip handles relative to the new client rect.
        public void AdjustPadding()
        {
            Padding = new((int)(ClientRect.Left + 1f), (int)(ClientRect.Top + 1f), (int)(ClientRect.Left + 1f), (int)(ClientRect.Left + 1f));

            editingRect = new(Padding.Left, Padding.Top, Width - Padding.Left * 2 - 1, Height - Padding.Bottom - Padding.Top - 1);

            float halfGrip = (int)(0.5 * GripSize);

            Grip_topLeft = new(editingRect.X - halfGrip, editingRect.Y - halfGrip, GripSize, GripSize);
            Grip_topRight = new(editingRect.X + editingRect.Width - halfGrip, editingRect.Y - halfGrip, GripSize, GripSize);
            Grip_bottomLeft = new(editingRect.X - halfGrip, editingRect.Y + editingRect.Height - halfGrip, GripSize, GripSize);
            Grip_bottomRight = new(editingRect.X + editingRect.Width - halfGrip, editingRect.Y + editingRect.Height - halfGrip, GripSize, GripSize);
            Grip_topCenter = new(editingRect.X + editingRect.Width / 2 - halfGrip, editingRect.Y - halfGrip, GripSize, GripSize);
            Grip_bottomCenter = new(editingRect.X + editingRect.Width / 2 - halfGrip, editingRect.Y + editingRect.Height - halfGrip, GripSize, GripSize);
            Grip_leftCenter = new(editingRect.X - halfGrip, editingRect.Y + editingRect.Height / 2 - halfGrip, GripSize, GripSize);
            Grip_rightCenter = new(editingRect.X + editingRect.Width - halfGrip, editingRect.Y + editingRect.Height / 2 - halfGrip, GripSize, GripSize);
        }

        // Fills a rectangle that is only rounded on the top two corners (used for the titlebar shape in W11/W10 so the bottom edge meets the client area flush).
        public void FillSemiRect(Graphics G, Brush Brush, RectangleF Rectangle, int Radius = -1)
        {
            if (Radius == -1) Radius = 6;
            if (G == null) return;

            G.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath path = RoundedSemiRectangle(Rectangle, Radius))
            {
                G.FillPath(Brush, path);
            }
        }

        // Constructs a GraphicsPath for a rectangle with rounded top-left and top-right corners only.
        public GraphicsPath RoundedSemiRectangle(RectangleF r, int radius)
        {
            GraphicsPath path = new();
            float d = radius * 2;

            path.AddLine(r.Left + d, r.Top, r.Right - d, r.Top);
            path.AddArc(Rectangle.FromLTRB((int)(r.Right - d), (int)r.Top, (int)r.Right, (int)(r.Top + d)), -90, 90f);
            path.AddLine(r.Right, r.Top, r.Right, r.Bottom);
            path.AddLine(r.Right, r.Bottom, r.Left, r.Bottom);
            path.AddLine(r.Left, r.Bottom - d, r.Left, r.Top + d);
            path.AddArc(Rectangle.FromLTRB((int)r.Left, (int)r.Top, (int)(r.Left + d), (int)(r.Top + d)), 180f, 90f);
            path.CloseFigure();

            return path;
        }

        #endregion

        #region Geometry / Design

        // Sample string used only for measuring caption font metrics; never displayed.
        private string TitleScheme => "ABCabc0123xYz.#";

        private float Title_x_Height, Title_9_Height;

        // Outer border thickness (in pixels) that accounts for OS-specific border conventions.
        private int SideSize
        {
            get
            {
                if (Preview == Preview_Enum.W11 || Preview == Preview_Enum.W10 || Preview == Preview_Enum.W11Lite)
                    return 1;

                if (Preview == Preview_Enum.WXP)
                    return Math.Max(4, _Metrics_BorderWidth);

                return _Metrics_BorderWidth + _Metrics_PaddedBorderWidth;
            }
        }

        private int IconSize => _Metrics_CaptionHeight <= 17 ? 12 : 14;

        // Extra vertical space above the caption text driven by font metrics.
        private float TitleHeight => Math.Max(0, Title_x_Height - Title_9_Height - 5);

        private float CloseBtn_W => TitleHeight + _Metrics_CaptionHeight - 4;

        private RectangleF CloseButtonRect;

        // The full control bounds (0,0 to Width-1, Height-1).
        private RectangleF RectAll => new(0, 0, Width - 1, Height - 1);

        // Window frame rect inset by FreeMargin to leave room for the shadow.
        private RectangleF Rect => new(RectAll.X + FreeMargin, RectAll.Y + FreeMargin, RectAll.Width - (FreeMargin * 2), RectAll.Height - (FreeMargin * 2));

        // Rect inset by 1px on every side, used for the inner border stroke.
        private RectangleF RectBorder => new(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2);

        // Full titlebar area including the top border and padded border pixels.
        private RectangleF TitlebarRect => new(Rect.X, Rect.Y, Rect.Width, TitleHeight + _Metrics_BorderWidth + _Metrics_CaptionHeight + _Metrics_PaddedBorderWidth + 3);

        private RectangleF IconRect
        {
            get
            {
                if (Preview == Preview_Enum.W11 || Preview == Preview_Enum.W10 || Preview == Preview_Enum.W11Lite || Preview == Preview_Enum.W10Lite)
                    return new(TitlebarRect.X + 4 + _Metrics_PaddedBorderWidth + _Metrics_BorderWidth, Rect.Y + (TitlebarRect.Height - IconSize) / 2f, IconSize, IconSize);

                if (Preview == Preview_Enum.W8 | Preview == Preview_Enum.W8Lite)
                    return new(TitlebarRect.X + 6 + _Metrics_PaddedBorderWidth + _Metrics_BorderWidth, Rect.Y + (TitlebarRect.Height - IconSize) / 2f, IconSize, IconSize);

                if (Preview == Preview_Enum.W7Basic)
                    return new(TitlebarRect.X + 4 + _Metrics_PaddedBorderWidth + _Metrics_BorderWidth, CloseButtonRect.Y + (CloseButtonRect.Height - IconSize) / 2f, IconSize, IconSize);

                if (Preview == Preview_Enum.WXP)
                    return new(Rect.X + SideSize + 4, TitlebarRect.Y + (TitlebarRect.Height - 14) / 2, 14, 14);

                return new(TitlebarRect.X + 4 + _Metrics_PaddedBorderWidth + _Metrics_BorderWidth, Rect.Y + (TitlebarRect.Height - IconSize) / 2f, IconSize, IconSize);
            }
        }

        private RectangleF LabelRect
        {
            get
            {
                if (Preview == Preview_Enum.W8 || Preview == Preview_Enum.W8Lite)
                    return new(Rect.X, Rect.Y + 2, TitlebarRect.Width - 1, TitlebarRect.Height - 3);

                if (Preview == Preview_Enum.W7Aero || Preview == Preview_Enum.W7Opaque)
                {
                    float offsetX = !ToolWindow ? IconRect.Right + 2 : IconRect.X - 2;
                    return new(offsetX, TitlebarRect.Y, TitlebarRect.Width - (IconRect.Right + 4), TitlebarRect.Height);
                }

                if (Preview == Preview_Enum.W7Basic)
                {
                    float offsetX = !ToolWindow ? IconRect.Right + 3 : IconRect.X;
                    return new(offsetX, CloseButtonRect.Y, TitlebarRect.Width - (IconRect.Right + 4), CloseButtonRect.Height);
                }

                if (Preview == Preview_Enum.WXP)
                {
                    float offsetX = Rect.X + SideSize + (ToolWindow ? 3 : 21);
                    return new(offsetX, TitlebarRect.Bottom - 2 - CloseBtn_W, Rect.Width - CloseBtn_W - SideSize * 2, CloseBtn_W);
                }

                {
                    float offsetX = !ToolWindow ? IconRect.Right + 4 : IconRect.X;
                    return new(offsetX, TitlebarRect.Y, TitlebarRect.Width - (IconRect.Right + 4), TitlebarRect.Height);
                }
            }
        }

        private RectangleF ClientRect => new(Rect.X + SideSize + 1, Rect.Y + TitlebarRect.Height + 1, Rect.Width - SideSize * 2 - 2, Rect.Height - (SideSize + TitlebarRect.Height) - 2);
        private RectangleF ClientBorderRect => new(ClientRect.X - 1, ClientRect.Y - 1, ClientRect.Width + 2, ClientRect.Height + 2);
        private RectangleF GlassSide1 => new(Rect.X + 1, ClientBorderRect.Y, SideSize, ClientBorderRect.Height * 0.5f);
        private RectangleF GlassSide2 => new(ClientBorderRect.Right - 1, GlassSide1.Y, GlassSide1.Width + 1, GlassSide1.Height);
        private RectangleF LeftEdge_XP => new(ClientRect.Left - SideSize, ClientRect.Y, SideSize, ClientRect.Height + SideSize);
        private RectangleF RightEdge_XP => new(ClientRect.Right + 1, ClientRect.Y, SideSize, ClientRect.Height + SideSize);
        private RectangleF ButtomEdge_XP => new(LeftEdge_XP.Left, LeftEdge_XP.Bottom - SideSize + 1, RightEdge_XP.Right - LeftEdge_XP.Left, SideSize + 1);

        // W7 Basic titlebar gradient colors.
        Color Titlebar_Backcolor1 => Active ? Color.FromArgb(152, 180, 208) : Color.FromArgb(191, 205, 219);
        Color Titlebar_Backcolor2 => Active ? Color.FromArgb(186, 210, 234) : Color.FromArgb(215, 228, 242);
        Color Titlebar_OuterBorder => Active ? Color.FromArgb(52, 52, 52) : Color.FromArgb(76, 76, 76);
        Color Titlebar_InnerBorder => Active ? Color.FromArgb(255, 255, 255) : Color.FromArgb(226, 230, 239);
        Color Titlebar_Turquoise => Active ? Color.FromArgb(40, 207, 228) : Color.FromArgb(226, 230, 239);
        Color OuterBorder => Active ? Color.FromArgb(0, 0, 0) : Color.FromArgb(76, 76, 76);

        // W7 Basic close button gradient colors.
        Color CloseUpperAccent1 => Active ? Color.FromArgb(233, 169, 156) : Color.FromArgb(189, 203, 218);
        Color CloseUpperAccent2 => Active ? Color.FromArgb(223, 149, 135) : default;
        Color CloseLowerAccent1 => Active ? Color.FromArgb(184, 67, 44) : default;
        Color CloseLowerAccent2 => Active ? Color.FromArgb(210, 127, 110) : Color.FromArgb(205, 219, 234);
        Color CloseOuterBorder => Active ? Color.FromArgb(67, 20, 34) : Color.FromArgb(131, 142, 168);
        Color CloseInnerBorder => Active ? Color.FromArgb(100, 255, 255, 255) : Color.FromArgb(209, 219, 229);

        // W8/8.1 window frame color; inactive state uses the accent at 80% brightness.
        Color InactiveWindows8 => !(Preview == Preview_Enum.W8Lite)
            ? Color.FromArgb(235, 235, 235)
            : Color.FromArgb((int)(Win7ColorBal / 100f * 255f), AccentColor_Active.CB(0.8f));

        Color Windows8Color => Active ? Color.FromArgb((int)(Win7ColorBal / 100d * 255d), AccentColor_Active) : InactiveWindows8;
        Color WindowsXPColor => Program.TM.Win32.ButtonFace;
        Color ClientAreaColor => !DesignMode ? Program.TM.Win32.ButtonFace : Color.White;

        Color CaptionColor
        {
            get
            {
                if (Preview == Preview_Enum.W11 || Preview == Preview_Enum.W10 || Preview == Preview_Enum.W11Lite)
                {
                    if (AccentColor_Enabled)
                        return Active
                            ? (AccentColor_Active.IsDark() ? Color.White : Color.Black)
                            : (AccentColor_Inactive.IsDark() ? Color.FromArgb(115, 115, 115) : Color.Black);

                    return Active ? (DarkMode ? Color.White : Color.Black) : Color.FromArgb(115, 115, 115);
                }

                if (Preview == Preview_Enum.W10Lite || Preview == Preview_Enum.W8)
                    return Color.Black;

                if (Preview == Preview_Enum.W7Basic)
                    return Active ? Color.Black : Color.FromArgb(76, 76, 76);

                return ForeColor;
            }
        }

        Bitmap CloseButtonImage
        {
            get
            {
                if (AccentColor_Enabled)
                    return Active
                        ? (AccentColor_Active.IsDark() ? Win10Preview.Win10x_Close_Dark : Win10Preview.Win10x_Close_Light)
                        : (AccentColor_Inactive.IsDark() ? Win10Preview.Win10x_Close_Dark : Win10Preview.Win10x_Close_Light);

                return DarkMode ? Win10Preview.Win10x_Close_Dark : Win10Preview.Win10x_Close_Light;
            }
        }

        #endregion

        #region Editors

        bool CursorOverTitlebar = false;
        bool CursorOverWindowAccent = false;

        private bool CursorOnMetricsGrip;
        private bool _MetricsEdit_Grip => EnableEditingMetrics && CursorOnMetricsGrip && !CursorOverTitlebar;
        private bool _MetricsEdit_CaptionFont => EnableEditingMetrics && CursorOverTitlebar;

        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);
        public event EditorInvokerEventHandler EditorInvoker;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingColors { get; set; } = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingMetrics { get; set; } = false;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!DesignMode && EnableEditingColors)
            {
                CursorOverTitlebar = (Preview == Preview_Enum.W11 || Preview == Preview_Enum.W10 || Preview == Preview_Enum.W11Lite || Preview == Preview_Enum.W10Lite)
                    && TitlebarRect.Contains(e.Location) && !isMoving_Grip_topCenter;

                CursorOverWindowAccent = (Preview == Preview_Enum.W8 || Preview == Preview_Enum.W8Lite || Preview == Preview_Enum.W7Aero || Preview == Preview_Enum.W7Opaque)
                    && Active && Rect.Contains(e.Location) && !ClientRect.Contains(e.Location);

                // Partial invalidate for performance: only repaint the titlebar band.
                Invalidate(new Rectangle((int)TitlebarRect.X, (int)TitlebarRect.Y, (int)(TitlebarRect.Width + 1f), (int)(TitlebarRect.Height + 1f)));
            }

            if (EnableEditingMetrics)
            {
                if (isMoving_Grip_topCenter)
                {
                    Metrics_CaptionHeight = e.Location.Y - (_Metrics_PaddedBorderWidth + _Metrics_BorderWidth) - GripSize * 2;
                }
                else if (isMoving_Grip_padding_left)
                {
                    Metrics_PaddedBorderWidth = e.Location.X - (int)(GripSize * 0.5) - _Metrics_BorderWidth;
                }
                else if (isMoving_Grip_borderWidth_left)
                {
                    Metrics_BorderWidth = e.Location.X - (int)(GripSize * 0.5) - _Metrics_PaddedBorderWidth;
                }
                else if (isMoving_Grip_padding_right)
                {
                    Metrics_PaddedBorderWidth = Width - e.Location.X - (int)(GripSize * 0.5) - _Metrics_BorderWidth;
                }
                else if (isMoving_Grip_borderWidth_right)
                {
                    Metrics_BorderWidth = Width - e.Location.X - (int)(GripSize * 0.5) - _Metrics_PaddedBorderWidth;
                }
                else if (isMoving_Grip_padding_bottom)
                {
                    Metrics_PaddedBorderWidth = Height - e.Location.Y - (int)(GripSize * 0.5) - _Metrics_BorderWidth;
                }
                else if (isMoving_Grip_borderWidth_bottom)
                {
                    Metrics_BorderWidth = Height - e.Location.Y - (int)(GripSize * 0.5) - _Metrics_PaddedBorderWidth;
                }
                else
                {
                    CursorOnMetricsGrip = true;
                    CursorOverTitlebar = TitlebarRect.Contains(e.Location);

                    if (Grip_topLeft.Contains(e.Location)) Cursor = Cursors.SizeNWSE;
                    else if (Grip_topRight.Contains(e.Location)) Cursor = Cursors.SizeNESW;
                    else if (Grip_bottomLeft.Contains(e.Location)) Cursor = Cursors.SizeNESW;
                    else if (Grip_bottomRight.Contains(e.Location)) Cursor = Cursors.SizeNWSE;
                    else if (Grip_topCenter.Contains(e.Location)) Cursor = Cursors.SizeNS;
                    else if (Grip_bottomCenter.Contains(e.Location)) Cursor = Cursors.SizeNS;
                    else if (Grip_leftCenter.Contains(e.Location)) Cursor = Cursors.SizeWE;
                    else if (Grip_rightCenter.Contains(e.Location)) Cursor = Cursors.SizeWE;
                    else Cursor = Cursors.Default;

                    Invalidate();
                }
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!DesignMode && EnableEditingColors)
            {
                CursorOverTitlebar = false;
                CursorOverWindowAccent = false;

                Invalidate(new Rectangle((int)TitlebarRect.X, (int)TitlebarRect.Y, (int)(TitlebarRect.Width + 1f), (int)(TitlebarRect.Height + 1f)));
            }
            else if (!DesignMode && EnableEditingMetrics)
            {
                CursorOnMetricsGrip = false;
                isMoving_Grip_topCenter = false;
                isMoving_Grip_padding_left = false;
                isMoving_Grip_borderWidth_left = false;
                isMoving_Grip_padding_right = false;
                isMoving_Grip_borderWidth_right = false;
                isMoving_Grip_padding_bottom = false;
                isMoving_Grip_borderWidth_bottom = false;
                CursorOverTitlebar = false;
                Cursor = Cursors.Default;

                Invalidate();
            }

            base.OnMouseLeave(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (!DesignMode && EnableEditingColors)
            {
                if (CursorOverTitlebar)
                {
                    EditorInvoker?.Invoke(this, new(
                        e.Button != MouseButtons.Right
                            ? (Active ? nameof(WindowsDesktop.TitlebarColor_Active) : nameof(WindowsDesktop.TitlebarColor_Inactive))
                            : nameof(WindowsDesktop.DarkMode_App)));
                }
                else if (CursorOverWindowAccent)
                {
                    EditorInvoker?.Invoke(this, new(
                        e.Button != MouseButtons.Right
                            ? nameof(WindowsDesktop.TitlebarColor_Active)
                            : nameof(WindowsDesktop.AfterGlowColor_Active)));
                }
            }
            else if (!DesignMode && _MetricsEdit_CaptionFont)
            {
                using (FontDialog fd = new FontDialog { Font = Font })
                {
                    if (fd.ShowDialog() == DialogResult.OK)
                    {
                        Font = fd.Font;
                        EditorInvoker?.Invoke(this, new(nameof(Font)));
                    }
                }
            }

            base.OnMouseClick(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            isMoving_Grip_topCenter = false;
            isMoving_Grip_padding_left = false;
            isMoving_Grip_borderWidth_left = false;
            isMoving_Grip_padding_right = false;
            isMoving_Grip_borderWidth_right = false;
            isMoving_Grip_padding_bottom = false;
            isMoving_Grip_borderWidth_bottom = false;
            base.OnMouseUp(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!DesignMode && _MetricsEdit_Grip)
            {
                if (Grip_topCenter.Contains(e.Location))
                {
                    isMoving_Grip_topCenter = true;
                    isMoving_Grip_padding_left = false;
                }
                else if (Grip_leftCenter.Contains(e.Location))
                {
                    isMoving_Grip_topCenter = false;
                    isMoving_Grip_padding_left = e.Button == MouseButtons.Right;
                    isMoving_Grip_borderWidth_left = e.Button != MouseButtons.Right;
                }
                else if (Grip_rightCenter.Contains(e.Location))
                {
                    isMoving_Grip_topCenter = false;
                    isMoving_Grip_padding_right = e.Button == MouseButtons.Right;
                    isMoving_Grip_borderWidth_right = e.Button != MouseButtons.Right;
                }
                else if (Grip_bottomCenter.Contains(e.Location))
                {
                    isMoving_Grip_topCenter = false;
                    isMoving_Grip_padding_bottom = e.Button == MouseButtons.Right;
                    isMoving_Grip_borderWidth_bottom = e.Button != MouseButtons.Right;
                }
                else if (Grip_bottomLeft.Contains(e.Location))
                {
                    isMoving_Grip_topCenter = false;
                    isMoving_Grip_padding_left = e.Button == MouseButtons.Right;
                    isMoving_Grip_borderWidth_left = e.Button != MouseButtons.Right;
                    isMoving_Grip_padding_bottom = e.Button == MouseButtons.Right;
                    isMoving_Grip_borderWidth_bottom = e.Button != MouseButtons.Right;
                }
                else if (Grip_bottomRight.Contains(e.Location))
                {
                    isMoving_Grip_topCenter = false;
                    isMoving_Grip_padding_right = e.Button == MouseButtons.Right;
                    isMoving_Grip_borderWidth_right = e.Button != MouseButtons.Right;
                    isMoving_Grip_padding_bottom = e.Button == MouseButtons.Right;
                    isMoving_Grip_borderWidth_bottom = e.Button != MouseButtons.Right;
                }
                else if (Grip_topLeft.Contains(e.Location))
                {
                    isMoving_Grip_topCenter = true;
                    isMoving_Grip_padding_left = e.Button == MouseButtons.Right;
                    isMoving_Grip_borderWidth_left = e.Button != MouseButtons.Right;
                }
                else if (Grip_topRight.Contains(e.Location))
                {
                    isMoving_Grip_topCenter = true;
                    isMoving_Grip_padding_right = e.Button == MouseButtons.Right;
                    isMoving_Grip_borderWidth_right = e.Button != MouseButtons.Right;
                }
            }

            base.OnMouseDown(e);
        }

        #endregion

        // Intentionally empty: transparent background is achieved by not painting anything here.
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            switch (Preview)
            {
                case Preview_Enum.W11:
                case Preview_Enum.W11Lite:
                    PaintW11(G);
                    break;

                case Preview_Enum.W10:
                case Preview_Enum.W10Lite:
                    PaintW10(G);
                    break;

                case Preview_Enum.W8:
                case Preview_Enum.W8Lite:
                    PaintW8(G);
                    break;

                case Preview_Enum.W7Aero:
                case Preview_Enum.W7Opaque:
                    PaintW7Aero(G);
                    break;

                case Preview_Enum.W7Basic:
                    PaintW7Basic(G);
                    break;

                case Preview_Enum.WXP:
                    PaintWXP(G);
                    break;
            }

            // Window icon is drawn on top of every style except tool windows.
            if (!ToolWindow) G.DrawImage(Active ? Resources.SampleApp_Small_Active : Resources.SampleApp_Small_Inactive, IconRect);

            // Metrics editor overlay: dashed bounding rect + drag handles.
            if (!DesignMode && _MetricsEdit_Grip) PaintMetricsGrips(G);

            // Titlebar hover highlight used by the metrics font editor.
            if (!DesignMode && EnableEditingMetrics && CursorOverTitlebar) PaintTitlebarEditorOverlay(G);

            base.OnPaint(e);
        }

        #region Paint sub-methods

        // Windows 11 (rounded corners, optional acrylic background).
        private void PaintW11(Graphics G)
        {
            if (Shadow && Active && !DesignMode)
                G.DrawGlow(Rect, Color.FromArgb(150, 0, 0, 0), 5, 15);

            // Acrylic background: blurred wallpaper visible only in the titlebar region.
            if (!AccentColor_Enabled && Active)
            {
                G.SetClip(TitlebarRect);
                G.DrawRoundImage(AdaptedBackBlurred, Rect, Radius, true);
                G.ResetClip();
            }

            // Fill client area (everything below the titlebar).
            G.ExcludeClip(new Rectangle((int)TitlebarRect.X, (int)TitlebarRect.Y, (int)TitlebarRect.Width, (int)TitlebarRect.Height));
            if (DarkMode)
            {
                using (SolidBrush br = new(Color.FromArgb(20, 20, 20)))
                    G.FillRoundedRect(br, Rect, Radius, true);
            }
            else
            {
                using (SolidBrush br = new(Color.FromArgb(240, 240, 240)))
                    G.FillRoundedRect(br, Rect, Radius, true);
            }
            G.ResetClip();

            // Outer border.
            if (AccentColor_Enabled)
            {
                Color borderColor = Active ? Color.FromArgb(200, AccentColor_Active) : Color.FromArgb(200, AccentColor_Inactive);
                using (Pen P = new(borderColor))
                    G.DrawRoundedRect(P, Rect, Radius, true);
            }
            else if (DarkMode)
            {
                using (Pen P = new(Color.FromArgb(200, 100, 100, 100)))
                    G.DrawRoundedRect(P, Rect, Radius, true);
            }
            else
            {
                using (Pen P = new(Color.FromArgb(200, 220, 220, 220)))
                    G.DrawRoundedRect(P, Rect, Radius, true);
            }

            // Titlebar fill.
            if (AccentColor_Enabled)
            {
                Color titleFill = Active ? Color.FromArgb(255, AccentColor_Active) : Color.FromArgb(255, AccentColor_Inactive);
                using (SolidBrush br = new(titleFill))
                    FillSemiRect(G, br, TitlebarRect, Radius);

                using (Pen P = new(titleFill))
                    G.DrawLine(P, new PointF(TitlebarRect.X + 1, TitlebarRect.Y + TitlebarRect.Height), new PointF(TitlebarRect.X + TitlebarRect.Width - 1, TitlebarRect.Y + TitlebarRect.Height));
            }
            else
            {
                int a = Active ? (DarkMode ? 180 : 245) : 255;
                Color fillColor = DarkMode ? Color.FromArgb(a, 32, 32, 32) : Color.FromArgb(a, 245, 245, 245);
                using (SolidBrush br = new(fillColor))
                    FillSemiRect(G, br, TitlebarRect, Radius);
            }

            // Titlebar color editor hover overlay.
            if (!DesignMode && EnableEditingColors && CursorOverTitlebar)
                PaintColorEditorOverlay_Rounded(G, TitlebarRect);

            // Close button.
            PaintCloseButton_W11(G);

            // Caption text.
            PaintCaptionText(G, LabelRect, ContentAlignment.MiddleLeft);
        }

        // Windows 10 (flat titlebar, no rounding).
        private void PaintW10(Graphics G)
        {
            if (Shadow && Active && !DesignMode)
                G.DrawGlow(Rect, Color.FromArgb(150, 0, 0, 0), 5, 15);

            // Base window fill.
            Color windowFill = DarkMode ? Color.FromArgb(20, 20, 20) : Color.FromArgb(240, 240, 240);
            using (SolidBrush br = new(windowFill))
                G.FillRectangle(br, Rect);

            // Titlebar fill (W10 has two sub-modes: full titlebar vs. accent-only frame).
            if (Preview == Preview_Enum.W10)
            {
                if (AccentColor_Enabled)
                {
                    Color titleFill = Active ? Color.FromArgb(255, AccentColor_Active) : Color.FromArgb(255, AccentColor_Inactive);
                    using (SolidBrush br = new(titleFill))
                        G.FillRectangle(br, TitlebarRect);
                }
                else if (Active)
                {
                    G.FillRectangle(DarkMode ? Brushes.Black : Brushes.White, TitlebarRect);
                }
                else if (DarkMode)
                {
                    using (SolidBrush br = new(Color.FromArgb(43, 43, 43)))
                        G.FillRectangle(br, TitlebarRect);
                }
                else
                {
                    G.FillRectangle(Brushes.White, TitlebarRect);
                }
            }
            else
            {
                // W10Lite: accent wraps the full frame, client area punched out.
                G.ExcludeClip(new Rectangle((int)ClientRect.X, (int)ClientRect.Y, (int)ClientRect.Width, (int)ClientRect.Height));
                using (SolidBrush br = new(AccentColor_Active))
                    G.FillRectangle(br, Rect);
                G.ResetClip();
            }

            // Outer border.
            if (AccentColor_Enabled)
            {
                Color borderColor = Active ? Color.FromArgb(200, AccentColor_Active) : Color.FromArgb(200, AccentColor_Inactive);
                using (Pen P = new(borderColor))
                    G.DrawRectangle(P, Rect.X, Rect.Y, Rect.Width, Rect.Height);
            }
            else if (DarkMode)
            {
                using (Pen P = new(Color.FromArgb(125, 100, 100, 100)))
                    G.DrawRectangle(P, Rect.X, Rect.Y, Rect.Width, Rect.Height);
            }
            else
            {
                using (Pen P = new(Color.FromArgb(125, 220, 220, 220)))
                    G.DrawRectangle(P, Rect.X, Rect.Y, Rect.Width, Rect.Height);
            }

            // Titlebar color editor hover overlay.
            if (!DesignMode && EnableEditingColors && CursorOverTitlebar)
                PaintColorEditorOverlay_Flat(G, TitlebarRect);

            // Close button.
            PaintCloseButton_W10(G);

            // Caption text.
            PaintCaptionText(G, LabelRect, ContentAlignment.MiddleLeft);
        }

        // Windows 8/8.1 (flat, accent-colored frame).
        private void PaintW8(Graphics G)
        {
            float closeH = _Metrics_CaptionHeight + TitleHeight - 2 + (Preview == Preview_Enum.W8Lite ? 1 : 0);
            float closeW = (!ToolWindow ? closeH * 3 / 2f : closeH) + (Preview == Preview_Enum.W8Lite ? 2 : 0);

            if (!ToolWindow)
            {
                CloseButtonRect = Preview != Preview_Enum.W8Lite
                    ? new RectangleF(ClientRect.Right - closeW + 1, Rect.Y + 1, closeW, closeH)
                    : new RectangleF(ClientRect.Right - closeW + 2, Rect.Y, closeW, closeH);
            }
            else
            {
                CloseButtonRect = new(ClientRect.Right - closeW + 1, Rect.Y + 1, closeW, closeH);
            }

            Color borderColor = Color.FromArgb(217, 217, 217);

            using (SolidBrush br = new(borderColor)) G.FillRectangle(br, Rect);
            using (SolidBrush br = new(Windows8Color)) G.FillRectangle(br, Rect);

            // Accent color editor hover overlay.
            if (!DesignMode && EnableEditingColors && CursorOverWindowAccent)
                PaintColorEditorOverlay_Flat(G, Rect);

            using (SolidBrush br = new(ClientAreaColor))
                G.FillRectangle(br, ClientRect);

            // Select the close button bitmap by caption height.
            Bitmap closeBtn;
            if (!ToolWindow)
            {
                if (CloseButtonRect.Height >= 27) closeBtn = Win81Preview.Close_3;
                else if (CloseButtonRect.Height >= 24) closeBtn = Win81Preview.Close_2;
                else if (CloseButtonRect.Height >= 21) closeBtn = Win81Preview.Close_1;
                else closeBtn = Win81Preview.Close_0;
            }
            else
            {
                closeBtn = Win81Preview.Close_ToolWindow;
            }

            if (Preview != Preview_Enum.W8Lite)
            {
                PaintW8Full(G, borderColor, closeBtn);
            }
            else
            {
                PaintW8Lite(G, closeBtn);
            }
        }

        private void PaintW8Full(Graphics G, Color borderColor, Bitmap closeBtn)
        {
            using (Pen P = new(Color.FromArgb(170, borderColor.CB(-0.2f))))
                G.DrawRectangle(P, ClientRect.X, ClientRect.Y, ClientRect.Width, ClientRect.Height);

            using (Pen P = new(Color.FromArgb((int)(Win7ColorBal / 100f * 255f), Windows8Color.CB(-0.2f))))
                G.DrawRectangle(P, ClientRect.X, ClientRect.Y, ClientRect.Width, ClientRect.Height);

            G.SmoothingMode = SmoothingMode.HighSpeed;
            using (SolidBrush br = new(Active ? Color.FromArgb(199, 80, 80) : Color.FromArgb(188, 188, 188)))
                G.FillRectangle(br, CloseButtonRect);
            G.SmoothingMode = SmoothingMode.AntiAlias;

            G.DrawImage(closeBtn, new Rectangle(
                (int)(CloseButtonRect.X + (CloseButtonRect.Width - closeBtn.Width) / 2f),
                (int)(CloseButtonRect.Y + (CloseButtonRect.Height - closeBtn.Height) / 2f),
                closeBtn.Width, closeBtn.Height));

            using (Pen P = new(Color.FromArgb(200, borderColor.CB(-0.3f))))
                G.DrawRectangle(P, Rect.X, Rect.Y, Rect.Width, Rect.Height);

            using (Pen P = new(Color.FromArgb((int)(Win7ColorBal / 100f * 255f), Windows8Color.CB(-0.3f))))
                G.DrawRectangle(P, Rect.X, Rect.Y, Rect.Width, Rect.Height);

            PaintCaptionText(G, LabelRect, ContentAlignment.MiddleCenter);
        }

        private void PaintW8Lite(Graphics G, Bitmap closeBtn)
        {
            closeBtn = closeBtn.ReplaceColor(Color.FromArgb(255, 255, 255), Color.Black);

            Color lineColor = Color.FromArgb((int)(Win7ColorBal / 100f * 255f), Windows8Color).LightLight();
            using (Pen P = new(lineColor))
            {
                G.DrawLine(P, new PointF(ClientRect.X, ClientRect.Y), new PointF(ClientRect.X + ClientRect.Width, ClientRect.Y));
                G.DrawLine(P, new PointF(ClientRect.X, ClientRect.Y + ClientRect.Height), new PointF(ClientRect.X + ClientRect.Width, ClientRect.Y + ClientRect.Height));
            }

            using (SolidBrush br = new(Active ? Color.FromArgb(195, 90, 80) : Color.Transparent))
                G.FillRectangle(br, CloseButtonRect);

            G.SmoothingMode = SmoothingMode.HighSpeed;
            using (Pen P = new(Active ? Color.FromArgb(92, 58, 55) : Color.FromArgb(93, 96, 102)))
                G.DrawRectangle(P, CloseButtonRect.X, CloseButtonRect.Y, CloseButtonRect.Width, CloseButtonRect.Height);
            G.SmoothingMode = SmoothingMode.AntiAlias;

            G.DrawImage(closeBtn, new RectangleF(
                CloseButtonRect.X + (CloseButtonRect.Width - closeBtn.Width) / 2f,
                CloseButtonRect.Y + (CloseButtonRect.Height - closeBtn.Height) / 2f,
                closeBtn.Width, closeBtn.Height));

            using (Pen P = new(Color.FromArgb(47, 48, 51)))
                G.DrawRectangle(P, Rect.X, Rect.Y, Rect.Width, Rect.Height);

            PaintCaptionText(G, LabelRect, ContentAlignment.MiddleCenter);
        }

        // Windows 7 Aero/Opaque glass effect.
        private void PaintW7Aero(Graphics G)
        {
            if (Shadow && Active && !DesignMode)
                G.DrawGlow(Rect, Color.FromArgb(150, 0, 0, 0), 5, 15);

            int aeroRadius = 5;

            if (Preview != Preview_Enum.W7Opaque)
            {
                float alpha = Active ? 1f - Win7Alpha / 100f : 0.25f;
                float colBal = Win7ColorBal / 100f;
                float glowBal = Win7GlowBal / 100f;

                Color color1 = Active ? AccentColor_Active : Color.FromArgb(128, AccentColor_Inactive);
                Color color2 = Active ? AccentColor2_Active : Color.Transparent;

                G.ExcludeClip(Rectangle.Round(ClientBorderRect));
                G.DrawAeroEffect(Rect, AdaptedBackBlurred, color1, colBal, color2, glowBal, alpha, aeroRadius, !ToolWindow);
                G.ResetClip();
            }
            else if (!ToolWindow)
            {
                using (SolidBrush br = new(Color.White))
                    G.FillRoundedRect(br, Rect, aeroRadius, true);

                using (SolidBrush br = new(Color.FromArgb((int)(255f * Win7ColorBal / 100f), Active ? AccentColor_Active : AccentColor_Active.Light())))
                    G.FillRoundedRect(br, Rect, aeroRadius, true);
            }
            else
            {
                using (SolidBrush br = new(Color.White))
                    G.FillRectangle(br, Rect);

                using (SolidBrush br = new(Color.FromArgb((int)(255f * Win7ColorBal / 100f), Active ? AccentColor_Active : AccentColor_Active.Light())))
                    G.FillRectangle(br, Rect);
            }

            // Shine panels on left and right glass edges.
            if (Active)
            {
                G.DrawImage(Win7Preview.WindowSides, GlassSide1);
                G.DrawImage(Win7Preview.WindowSides, GlassSide2);

                float titleTopW = Rect.Width * 0.6f;
                float titleTopH = Rect.Height * 0.6f;

                G.SetClip(RectBorder);
                G.DrawImage(Win7Preview.TitleTopL, new RectangleF(Rect.X + (ToolWindow ? -1 : 1), Rect.Y, titleTopW, titleTopH));
                G.DrawImage(Win7Preview.TitleTopR, new RectangleF(Rect.X + Rect.Width - titleTopW + 1, Rect.Y, titleTopW, titleTopH));
                G.ResetClip();
            }

            // Window frame borders.
            if (!ToolWindow)
            {
                if (Noise7 != null)
                    G.DrawRoundImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), Rect, aeroRadius, true);

                using (Pen P = new(Color.FromArgb(Active ? 130 : 100, 25, 25, 25)))
                    G.DrawRoundedRect(P, Rect, aeroRadius, true);

                using (Pen P = new(Color.FromArgb(100, 255, 255, 255)))
                    G.DrawRoundedRect(P, RectBorder, aeroRadius, true);

                using (Pen P = new(Color.FromArgb((int)(255f - 255 * Win7Alpha / 300f), base.BackColor.Light(0.2f))))
                    G.DrawRoundedRect(P, ClientBorderRect, 1, true);

                if (!DesignMode && EnableEditingColors && CursorOverWindowAccent)
                    PaintColorEditorOverlay_Rounded(G, Rect);

                using (SolidBrush br = new(ClientAreaColor))
                    G.FillRoundedRect(br, ClientBorderRect, 1, true);

                using (Pen P = new(Color.FromArgb((int)(255f - 255 * Win7Alpha / 300f), base.BackColor.Dark(0.2f))))
                    G.DrawRoundedRect(P, ClientRect, 1, true);
            }
            else
            {
                if (Noise7 != null)
                    G.DrawImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), Rect);

                using (Pen P = new(Color.FromArgb(Active ? 130 : 100, 25, 25, 25)))
                    G.DrawRectangle(P, Rect.X, Rect.Y, Rect.Width, Rect.Height);

                using (Pen P = new(Color.FromArgb(100, 255, 255, 255)))
                    G.DrawRectangle(P, RectBorder.X, RectBorder.Y, RectBorder.Width, RectBorder.Height);

                using (Pen P = new(Color.FromArgb((int)(255f - 255 * Win7Alpha / 300f), base.BackColor.Light(0.2f))))
                    G.DrawRectangle(P, ClientBorderRect.X, ClientBorderRect.Y, ClientBorderRect.Width, ClientBorderRect.Height);

                if (!DesignMode && EnableEditingColors && CursorOverWindowAccent)
                    PaintColorEditorOverlay_Rounded(G, Rect);

                using (SolidBrush br = new(ClientAreaColor))
                    G.FillRectangle(br, ClientBorderRect);

                using (Pen P = new(Color.FromArgb((int)(255f - 255 * Win7Alpha / 300f), base.BackColor.Dark(0.2f))))
                    G.DrawRectangle(P, ClientRect.X, ClientRect.Y, ClientRect.Width, ClientRect.Height);
            }

            // Close button (full-size Aero button or compact tool-window button).
            PaintCloseButton_W7Aero(G);

            // Glow caption text.
            int captionAlpha = Active ? 100 : 50;
            using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat())
                G.DrawGlowString(1, Text, Font, CaptionColor, Color.FromArgb(captionAlpha, Color.White), RectAll, LabelRect, sf);
        }

        // Windows 7 Basic (no glass, gradient titlebar).
        private void PaintW7Basic(Graphics G)
        {
            RectangleF upperPart = new(Rect.X, Rect.Y, Rect.Width + 1, TitlebarRect.Height + 4);
            RectangleF upperPart_Modified = new(upperPart.X + upperPart.Width * 0.75f, upperPart.Y, upperPart.Width * 0.75f, upperPart.Height);

            G.SetClip(upperPart);

            using (LinearGradientBrush backBrush = new(upperPart, Titlebar_Backcolor1, Titlebar_Backcolor2, LinearGradientMode.Vertical))
            using (LinearGradientBrush lineBrush = new(upperPart, Titlebar_InnerBorder, Titlebar_Turquoise, LinearGradientMode.Vertical))
            {
                if (!ToolWindow)
                {
                    G.FillRoundedRect(backBrush, Rect, _radius, true);
                    using (Pen P = new(Titlebar_OuterBorder)) G.DrawRoundedRect(P, Rect, _radius, true);
                    using (Pen P = new(Titlebar_InnerBorder)) G.DrawRoundedRect(P, new RectangleF(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2), _radius, true);
                    G.SetClip(upperPart_Modified);
                    using (Pen P = new(lineBrush)) G.DrawRoundedRect(P, new RectangleF(Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2), _radius, true);
                }
                else
                {
                    G.FillRectangle(backBrush, Rect);
                    using (Pen P = new(Titlebar_OuterBorder)) G.DrawRectangle(P, Rect.X, Rect.Y, Rect.Width, Rect.Height);
                    using (Pen P = new(Titlebar_InnerBorder)) G.DrawRectangle(P, Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2);
                    G.SetClip(upperPart_Modified);
                    using (Pen P = new(lineBrush)) G.DrawRectangle(P, Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2);
                }
            }

            G.ResetClip();
            G.ExcludeClip(new Rectangle((int)upperPart.X, (int)upperPart.Y, (int)upperPart.Width, (int)upperPart.Height));

            using (SolidBrush br = new(Titlebar_Backcolor2)) G.FillRectangle(br, Rect);
            using (Pen P = new(Titlebar_Turquoise)) G.DrawRectangle(P, Rect.X + 1, Rect.Y + 1, Rect.Width - 2, Rect.Height - 2);
            using (Pen P = new(OuterBorder)) G.DrawRectangle(P, Rect.X, Rect.Y, Rect.Width, Rect.Height);
            using (Pen P = new(Titlebar_InnerBorder)) G.DrawLine(P, new PointF(Rect.X + 1, Rect.Y), new PointF(Rect.X + 1, Rect.Y + Rect.Height - 2));

            if (Active)
            {
                G.DrawImage(Win7Preview.WindowSides, GlassSide1);
                G.DrawImage(Win7Preview.WindowSides, GlassSide2);
            }

            G.ResetClip();

            G.FillRectangle(Brushes.White, ClientBorderRect);
            using (Pen P = new(Color.FromArgb(186, 210, 234))) G.DrawRectangle(P, ClientBorderRect.X, ClientBorderRect.Y, ClientBorderRect.Width, ClientBorderRect.Height);
            if (!WinVista)
            {
                using (Pen P = new(Color.FromArgb(130, 135, 144)))
                    G.DrawRectangle(P, ClientRect.X, ClientRect.Y, ClientRect.Width, ClientRect.Height);
            }

            // Close button.
            PaintCloseButton_W7Basic(G);

            // Caption text.
            PaintCaptionText(G, LabelRect, ContentAlignment.MiddleLeft);
        }

        // Windows XP (uses VisualStylesRes to draw the native-looking chrome).
        private void PaintWXP(Graphics G)
        {
            SmoothingMode prevMode = G.SmoothingMode;
            G.SmoothingMode = SmoothingMode.HighSpeed;

            using (SolidBrush br = new(WindowsXPColor))
                G.FillRectangle(br, ClientBorderRect);

            resVS?.Draw(G, TitlebarRect, VisualStylesRes.Element.Titlebar, Active, ToolWindow);

            CloseButtonRect = new(ClientRect.Right - CloseBtn_W - RightEdge_XP.Width + 4, Rect.Y + TitlebarRect.Height - CloseBtn_W - 3, CloseBtn_W, CloseBtn_W);

            resVS?.Draw(G, LeftEdge_XP, VisualStylesRes.Element.LeftEdge, Active, ToolWindow);
            resVS?.Draw(G, RightEdge_XP, VisualStylesRes.Element.RightEdge, Active, ToolWindow);
            resVS?.Draw(G, ButtomEdge_XP, VisualStylesRes.Element.BottomEdge, Active, ToolWindow);
            resVS?.Draw(G, CloseButtonRect, VisualStylesRes.Element.CloseButton, Active, ToolWindow);

            G.SmoothingMode = prevMode;

            // XP caption drawn twice: dark shadow offset by 1px, then the actual color on top.
            using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat())
            {
                using (SolidBrush shadowBrush = new(Color.Black))
                    G.DrawString(Text, Font, shadowBrush, new RectangleF(LabelRect.X + 1, LabelRect.Y, LabelRect.Width, LabelRect.Height), sf);

                using (SolidBrush br = new(CaptionColor))
                    G.DrawString(Text, Font, br, LabelRect, sf);
            }
        }

        // Draws the W11 close button — differs between normal and lite sub-modes and tool windows.
        private void PaintCloseButton_W11(Graphics G)
        {
            if (!ToolWindow)
            {
                if (Preview == Preview_Enum.W11Lite)
                {
                    G.SetClip(Rect.Round(Radius));

                    RectangleF closeLite = new(TitlebarRect.Right - CloseBtn_W * 2, TitlebarRect.Y, CloseBtn_W * 2, TitlebarRect.Height);

                    using (SolidBrush br = new(Color.FromArgb(195, 90, 80)))
                    {
                        if (Active) G.FillRectangle(br, closeLite);
                    }

                    using (Pen P = new(DarkMode ? Color.FromArgb(51, 51, 51) : Color.FromArgb(0, 0, 0)))
                    {
                        G.DrawLine(P, closeLite.X, closeLite.Y + 1, closeLite.X, closeLite.Y + closeLite.Height);
                        G.DrawLine(P, closeLite.X, closeLite.Y + closeLite.Height, closeLite.X + closeLite.Width - 1, closeLite.Y + closeLite.Height);
                    }

                    using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
                    using (SolidBrush br = new(Color.Black))
                        G.DrawString("r", marlett_7_7, br, new RectangleF(closeLite.X + 1, closeLite.Y + 1, closeLite.Width, closeLite.Height), sf);

                    G.ResetClip();
                }
                else
                {
                    using (Bitmap closeImg = new(CloseButtonImage))
                    {
                        CloseButtonRect = new(TitlebarRect.Right - closeImg.Width * 2f, TitlebarRect.Top + (TitlebarRect.Height - closeImg.Height) / 2f, closeImg.Width, closeImg.Height);
                        G.DrawImage(closeImg, CloseButtonRect);
                    }
                }
            }
            else
            {
                PaintCloseButton_ToolWindow(G, true);
            }
        }

        // Draws the W10 close button.
        private void PaintCloseButton_W10(Graphics G)
        {
            if (!ToolWindow)
            {
                if (Preview == Preview_Enum.W10Lite)
                {
                    RectangleF closeLite = new(ClientRect.Right - CloseBtn_W * 2, TitlebarRect.Y, CloseBtn_W * 2, TitlebarRect.Height);

                    using (SolidBrush br = new(Color.FromArgb(195, 90, 80)))
                    {
                        if (Active) G.FillRectangle(br, closeLite);
                    }

                    using (Pen P = new(DarkMode ? Color.FromArgb(51, 51, 51) : Color.FromArgb(0, 0, 0)))
                    {
                        G.DrawLine(P, closeLite.X, closeLite.Y + 1, closeLite.X, closeLite.Y + closeLite.Height);
                        G.DrawLine(P, closeLite.X, closeLite.Y + closeLite.Height, closeLite.X + closeLite.Width - 1, closeLite.Y + closeLite.Height);
                    }

                    using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
                    using (SolidBrush br = new(Color.Black))
                        G.DrawString("r", marlett_7_7, br, new RectangleF(closeLite.X + 1, closeLite.Y + 1, closeLite.Width, closeLite.Height), sf);
                }
                else
                {
                    using (Bitmap closeImg = new(CloseButtonImage))
                    {
                        CloseButtonRect = new(TitlebarRect.Right - closeImg.Width * 2, TitlebarRect.Top + (TitlebarRect.Height - closeImg.Height) / 2, closeImg.Width, closeImg.Height);
                        G.DrawImage(closeImg, CloseButtonRect);
                    }
                }
            }
            else
            {
                PaintCloseButton_ToolWindow(G, false);
            }
        }

        // Draws the W7 Aero close button (full-size bitmap or compact gradient for tool windows).
        private void PaintCloseButton_W7Aero(Graphics G)
        {
            if (!ToolWindow)
            {
                Bitmap closeBtn = Active
                    ? (!WinVista ? Win7Preview.Close_Active : WinVistaPreview.Close_Active)
                    : (!WinVista ? Win7Preview.Close_inactive : WinVistaPreview.Close_Inactive);

                CloseButtonRect = new(Rect.X + Rect.Width - closeBtn.Width - 5, Rect.Y + 1, closeBtn.Width, closeBtn.Height);
                G.DrawImage(closeBtn, CloseButtonRect);
            }
            else
            {
                int btnH = Math.Max(10, _Metrics_CaptionHeight - 5);
                int btnW = btnH;

                CloseButtonRect = new(ClientBorderRect.Right - btnW - 3, Rect.Y + (TitlebarRect.Height - btnH) / 2f, btnW, btnH);

                if (Active)
                {
                    float factor = 0.5f;
                    float upperH = factor * CloseButtonRect.Height;
                    float lowerH = CloseButtonRect.Height - upperH;
                    float interlapping = upperH / CloseButtonRect.Height * 10f;

                    RectangleF upperRect = new(CloseButtonRect.X, CloseButtonRect.Y, CloseButtonRect.Width, upperH + interlapping);
                    RectangleF lowerRect = new(CloseButtonRect.X, upperRect.Bottom - interlapping, CloseButtonRect.Width, lowerH);

                    using (LinearGradientBrush upperBrush = new(upperRect, Color.FromArgb(50, CloseUpperAccent1), CloseUpperAccent2, LinearGradientMode.Vertical))
                    using (LinearGradientBrush lowerBrush = new(lowerRect, CloseLowerAccent1, Color.FromArgb(50, CloseLowerAccent2), LinearGradientMode.Vertical))
                    {
                        G.FillRoundedRect(upperBrush, upperRect, 1, true);
                        G.FillRoundedRect(lowerBrush, lowerRect, 1, true);
                    }
                }
                else
                {
                    using (LinearGradientBrush br = new(CloseButtonRect, Color.FromArgb(50, CloseUpperAccent1), Color.FromArgb(50, CloseLowerAccent2), LinearGradientMode.Vertical))
                        G.FillRectangle(br, CloseButtonRect);
                }

                Bitmap closeBtn = Win7Preview.Close_Basic_ToolWindow;

                int xW = CloseButtonRect.Width % 2 == 0 ? closeBtn.Width + 1 : closeBtn.Width;
                int xH = CloseButtonRect.Height % 2 == 0 ? closeBtn.Height + 1 : closeBtn.Height;

                RectangleF renderRect = new(
                    CloseButtonRect.X + (CloseButtonRect.Width - xW) / 2f,
                    CloseButtonRect.Y + (CloseButtonRect.Height - xH) / 2f,
                    xW, xH);

                G.DrawImage(closeBtn, renderRect);

                using (Pen P = new(CloseOuterBorder)) G.DrawRoundedRect(P, CloseButtonRect, 1, true);
                using (Pen P = new(Color.FromArgb(50, CloseInnerBorder))) G.DrawRectangle(P, CloseButtonRect.X + 1, CloseButtonRect.Y + 1, CloseButtonRect.Width - 2, CloseButtonRect.Height - 2);
            }
        }

        // Draws the W7 Basic close button (gradient rectangle + X bitmap).
        private void PaintCloseButton_W7Basic(Graphics G)
        {
            int btnH = Math.Max(10, _Metrics_CaptionHeight - 5);
            float btnW = !ToolWindow ? 31f / 17f * btnH : btnH;

            CloseButtonRect = new(ClientBorderRect.Right - btnW - 3, ClientBorderRect.Top - btnH - 3, btnW, btnH);

            if (Active)
            {
                float factor = ToolWindow ? 0.2f : 0.45f;
                float upperH = factor * CloseButtonRect.Height;
                float lowerH = CloseButtonRect.Height - upperH;
                float interlapping = upperH / CloseButtonRect.Height * 10f;

                RectangleF upperRect = new(CloseButtonRect.X, CloseButtonRect.Y, CloseButtonRect.Width, upperH + interlapping);
                RectangleF lowerRect = new(CloseButtonRect.X, upperRect.Bottom - interlapping, CloseButtonRect.Width, lowerH);

                using (LinearGradientBrush upperBrush = new(upperRect, CloseUpperAccent1, CloseUpperAccent2, LinearGradientMode.Vertical))
                using (LinearGradientBrush lowerBrush = new(lowerRect, CloseLowerAccent1, CloseLowerAccent2, LinearGradientMode.Vertical))
                {
                    G.FillRoundedRect(upperBrush, upperRect, 1, true);
                    G.FillRoundedRect(lowerBrush, lowerRect, 1, true);
                }
            }
            else
            {
                using (LinearGradientBrush br = new(CloseButtonRect, CloseUpperAccent1, CloseLowerAccent2, LinearGradientMode.Vertical))
                    G.FillRectangle(br, CloseButtonRect);
            }

            Bitmap closeBtn;
            if (!ToolWindow)
            {
                if (CloseButtonRect.Height >= 22) closeBtn = Win7Preview.Close_Basic_2;
                else if (CloseButtonRect.Height >= 18) closeBtn = Win7Preview.Close_Basic_1;
                else closeBtn = Win7Preview.Close_Basic_0;
            }
            else
            {
                closeBtn = Win7Preview.Close_Basic_ToolWindow;
            }

            G.DrawImage(closeBtn, new PointF(
                CloseButtonRect.X + (CloseButtonRect.Width - closeBtn.Width) / 2f + 1f,
                CloseButtonRect.Y + (CloseButtonRect.Height - closeBtn.Height) / 2f));

            using (Pen P = new(CloseOuterBorder)) G.DrawRoundedRect(P, CloseButtonRect, 1, true);
            using (Pen P = new(CloseInnerBorder)) G.DrawRoundedRect(P, new RectangleF(CloseButtonRect.X + 1, CloseButtonRect.Y + 1, CloseButtonRect.Width - 2, CloseButtonRect.Height - 2), 1, true);
        }

        // Draws the small tool-window close button shared between W11 and W10 styles.
        // isRounded controls whether the background uses FillRectangle (flat) or a rounded fill.
        private void PaintCloseButton_ToolWindow(Graphics G, bool isRounded)
        {
            CloseButtonRect = new(TitlebarRect.Right - 2 - (TitlebarRect.Height - 12), Rect.Y + 6, TitlebarRect.Height - 12, TitlebarRect.Height - 12);

            using (SolidBrush br = new(Color.FromArgb(199, 80, 80)))
                G.FillRectangle(br, CloseButtonRect);

            // Offset so the X glyph sits in the optical center after accounting for even/odd size.
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

            bool isLite = Preview == Preview_Enum.W11Lite || Preview == Preview_Enum.W10Lite;
            using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
            using (SolidBrush br = new(isLite ? Color.Black : Color.White))
                G.DrawString("r", marlett_6_35, br, new RectangleF(CloseButtonRect.X + 1, CloseButtonRect.Y + 1, CloseButtonRect.Width, CloseButtonRect.Height), sf);
        }

        // Draws the caption text at the given position and alignment.
        private void PaintCaptionText(Graphics G, RectangleF rect, ContentAlignment alignment)
        {
            using (StringFormat sf = alignment.ToStringFormat())
            using (SolidBrush br = new(CaptionColor))
                G.DrawString(Text, Font, br, rect, sf);
        }

        // Hatch overlay drawn over any area that can be clicked to open a color picker.
        // Used on flat rects (W10, W8 frame).
        private void PaintColorEditorOverlay_Flat(Graphics G, RectangleF area)
        {
            Color light = Color.FromArgb(80, 255, 255, 255);
            Color dark = Color.FromArgb(80, 0, 0, 0);

            using (Pen P = new(light))
            using (HatchBrush hb = new(HatchStyle.Percent25, light, dark))
            {
                G.FillRectangle(hb, area);
                G.DrawRectangle(P, area.X, area.Y, area.Width, area.Height);
            }
        }

        // Hatch overlay drawn over rounded-rect areas (W11 titlebar, W7 Aero frame).
        private void PaintColorEditorOverlay_Rounded(Graphics G, RectangleF area)
        {
            Color light = Color.FromArgb(80, 255, 255, 255);
            Color dark = Color.FromArgb(80, 0, 0, 0);

            using (Pen P = new(light))
            using (HatchBrush hb = new(HatchStyle.Percent25, light, dark))
            {
                G.FillRoundedRect(hb, area, Radius, true);
                G.DrawRoundedRect(P, area, Radius, true);
            }
        }

        // Dashed bounding rect + white square handles drawn when the metrics editor is active.
        private void PaintMetricsGrips(Graphics G)
        {
            using (Pen dottedPen = new(Color.Gray) { DashStyle = DashStyle.Dot })
                G.DrawRectangle(dottedPen, editingRect.X, editingRect.Y, editingRect.Width, editingRect.Height);

            using (Pen solidPen = new(Color.Gray) { DashStyle = DashStyle.Solid })
            {
                G.FillRoundedRect(Brushes.White, Grip_topLeft, 1, true);
                G.FillRoundedRect(Brushes.White, Grip_topRight, 1, true);
                G.FillRoundedRect(Brushes.White, Grip_bottomLeft, 1, true);
                G.FillRoundedRect(Brushes.White, Grip_bottomRight, 1, true);
                G.FillRoundedRect(Brushes.White, Grip_topCenter, 1, true);
                G.FillRoundedRect(Brushes.White, Grip_bottomCenter, 1, true);
                G.FillRoundedRect(Brushes.White, Grip_leftCenter, 1, true);
                G.FillRoundedRect(Brushes.White, Grip_rightCenter, 1, true);

                G.DrawRoundedRect(solidPen, Grip_topLeft, 1, true);
                G.DrawRoundedRect(solidPen, Grip_topRight, 1, true);
                G.DrawRoundedRect(solidPen, Grip_bottomLeft, 1, true);
                G.DrawRoundedRect(solidPen, Grip_bottomRight, 1, true);
                G.DrawRoundedRect(solidPen, Grip_topCenter, 1, true);
                G.DrawRoundedRect(solidPen, Grip_bottomCenter, 1, true);
                G.DrawRoundedRect(solidPen, Grip_leftCenter, 1, true);
                G.DrawRoundedRect(solidPen, Grip_rightCenter, 1, true);
            }
        }

        // Hatch overlay drawn over the titlebar when the metrics font editor is active
        // (hovering the titlebar opens a font dialog on click).
        private void PaintTitlebarEditorOverlay(Graphics G)
        {
            Color light = Color.FromArgb(80, 255, 255, 255);
            Color dark = Color.FromArgb(80, 0, 0, 0);

            using (Pen P = new(light))
            using (HatchBrush hb = new(HatchStyle.Percent25, light, dark))
            {
                G.FillRoundedRect(hb, TitlebarRect, Radius, true);
                G.DrawRoundedRect(P, TitlebarRect, Radius, true);
            }
        }

        #endregion
    }
}