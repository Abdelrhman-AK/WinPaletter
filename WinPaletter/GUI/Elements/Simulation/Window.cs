using ImageProcessor;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.Templates;
using WinPaletter.UI.Retro;

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
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Color.Transparent;
        }

        #region Variables

        private Bitmap AdaptedBack, AdaptedBackBlurred;
        private Bitmap Noise7 = Assets.Win7Preview.AeroGlass;

        private readonly int FreeMargin = 8;

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

        int GripSize = 6;
        Rectangle editingRect;
        Rectangle Grip_topLeft;
        Rectangle Grip_topRight;
        Rectangle Grip_bottomLeft;
        Rectangle Grip_bottomRight;
        Rectangle Grip_topCenter;
        Rectangle Grip_bottomCenter;
        Rectangle Grip_leftCenter;
        Rectangle Grip_rightCenter;

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
                        Noise7 = Assets.Win7Preview.AeroGlass.Fade(Win7Noise / 100f);
                    }

                    Invalidate();
                }
            }
        }


        private int min_Metrics_CaptionHeight = 16;
        private int max_Metrics_CaptionHeight = 50;

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

                    if (!DesignMode && EnableEditingMetrics && isMoving_Grip_topCenter) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Metrics_CaptionHeight)));
                }
            }
        }


        private int min_Metrics_BorderWidth = 0;
        private int max_Metrics_BorderWidth = 50;

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
                        EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Metrics_BorderWidth)));
                }
            }
        }


        private int min_Metrics_PaddedBorderWidth = 0;
        private int max_Metrics_PaddedBorderWidth = 50;

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
                        EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Metrics_PaddedBorderWidth)));
                }
            }
        }


        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Bindable(true)]
        public override string Text { get; set; }

        #endregion

        #region Events/Overrides

        public event MetricsChangedEventHandler MetricsChanged;

        public delegate void MetricsChangedEventHandler();

        //protected override void OnHandleCreated(EventArgs e)
        //{
        //    if (!DesignMode) ProcessBack();

        //    base.OnHandleCreated(e);
        //}

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

        protected override void OnSizeChanged(EventArgs e)
        {
            AdjustPadding();

            base.OnSizeChanged(e);
        }

        public void ProcessBack()
        {
            // Styles support transparency (Mica/AeroGlass)
            if (Preview == Preview_Enum.W11 || Preview == Preview_Enum.W11Lite || Preview == Preview_Enum.W7Aero)
            {
                Bitmap Wallpaper = Parent.BackgroundImage is null ? Program.Wallpaper : Parent.BackgroundImage as Bitmap;

                if (Wallpaper is not null)
                {
                    Rectangle imageBounds = new(0, 0, Wallpaper.Width, Wallpaper.Height);
                    if (imageBounds.Contains_ButNotExceed(Bounds)) AdaptedBack = Wallpaper.Clone(Bounds, Wallpaper.PixelFormat);
                }

                if (Preview == Preview_Enum.W11 || Preview == Preview_Enum.W11Lite)
                {
                    if (Active && !AccentColor_Enabled && AdaptedBack is not null)
                    {
                        Bitmap b = AdaptedBack.Blur(15);

                        if (DarkMode)
                        {
                            if (b is not null)
                            {
                                using (ImageFactory ImgF = new())
                                {
                                    ImgF.Load(b);
                                    ImgF.Saturation(-30);
                                    ImgF.Brightness(-20);
                                    AdaptedBackBlurred = ImgF.Image.Clone() as Bitmap;
                                }
                            }
                        }

                        else { AdaptedBackBlurred = b; }
                    }
                }

                else if (Preview == Preview_Enum.W7Aero)
                {
                    if (AdaptedBack is not null) { AdaptedBackBlurred = AdaptedBack.Blur(3); }

                    Noise7 = Assets.Win7Preview.AeroGlass.Fade(Win7Noise / 100);
                }
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

        public void AdjustPadding()
        {
            Padding = new(ClientRect.Left + 1, ClientRect.Top + 1, ClientRect.Left + 1, ClientRect.Left + 1);

            editingRect = new(Padding.Left, Padding.Top, Width - Padding.Left * 2 - 1, Height - Padding.Bottom - Padding.Top - 1);

            Grip_topLeft = new(editingRect.X - (int)(0.5 * GripSize), editingRect.Y - (int)(0.5 * GripSize), GripSize, GripSize);
            Grip_topRight = new(editingRect.X + editingRect.Width - (int)(0.5 * GripSize), editingRect.Y - (int)(0.5 * GripSize), GripSize, GripSize);
            Grip_bottomLeft = new(editingRect.X - (int)(0.5 * GripSize), editingRect.Y + editingRect.Height - (int)(0.5 * GripSize), GripSize, GripSize);
            Grip_bottomRight = new(editingRect.X + editingRect.Width - (int)(0.5 * GripSize), editingRect.Y + editingRect.Height - (int)(0.5 * GripSize), GripSize, GripSize);
            Grip_topCenter = new(editingRect.X + editingRect.Width / 2 - (int)(0.5 * GripSize), editingRect.Y - (int)(0.5 * GripSize), GripSize, GripSize);
            Grip_bottomCenter = new(editingRect.X + editingRect.Width / 2 - (int)(0.5 * GripSize), editingRect.Y + editingRect.Height - (int)(0.5 * GripSize), GripSize, GripSize);
            Grip_leftCenter = new(editingRect.X - (int)(0.5 * GripSize), editingRect.Y + editingRect.Height / 2 - (int)(0.5 * GripSize), GripSize, GripSize);
            Grip_rightCenter = new(editingRect.X + editingRect.Width - (int)(0.5 * GripSize), editingRect.Y + editingRect.Height / 2 - (int)(0.5 * GripSize), GripSize, GripSize);
        }

        public void FillSemiRect(Graphics G, Brush Brush, Rectangle Rectangle, int Radius = -1)
        {
            if (Radius == -1) Radius = 6;

            if (G is null) return;

            G.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath path = RoundedSemiRectangle(Rectangle, Radius))
            {
                G.FillPath(Brush, path);
            }
        }

        public GraphicsPath RoundedSemiRectangle(Rectangle r, int radius)
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

        #endregion

        #region Geometry/Design
        private string TitleScheme => "ABCabc0123xYz.#";

        private int Title_x_Height, Title_9_Height;
        private int SideSize
        {
            get
            {
                if (Preview == Preview_Enum.W11 || Preview == Preview_Enum.W10 || Preview == Preview_Enum.W11Lite) // there is no Preview == Preview_Enum.W10Lite as it looks like W8.1Lite
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
                if (Preview == Preview_Enum.W11 || Preview == Preview_Enum.W10 || Preview == Preview_Enum.W11Lite || Preview == Preview_Enum.W10Lite)
                {
                    return new(TitlebarRect.X + 4 + _Metrics_PaddedBorderWidth + _Metrics_BorderWidth, (int)Math.Round(Rect.Y + (TitlebarRect.Height - IconSize) / 2d), IconSize, IconSize);
                }

                else if (Preview == Preview_Enum.W8 | Preview == Preview_Enum.W8Lite)
                {
                    return new(TitlebarRect.X + 6 + _Metrics_PaddedBorderWidth + _Metrics_BorderWidth, (int)Math.Round(Rect.Y + (TitlebarRect.Height - IconSize) / 2d), IconSize, IconSize);
                }

                else if (Preview == Preview_Enum.W7Basic)
                {
                    return new(TitlebarRect.X + 4 + _Metrics_PaddedBorderWidth + _Metrics_BorderWidth, (int)Math.Round(CloseButtonRect.Y + (CloseButtonRect.Height - IconSize) / 2d), IconSize, IconSize);
                }

                else if (Preview == Preview_Enum.WXP)
                {
                    return new(Rect.X + SideSize + 4, TitlebarRect.Y + (TitlebarRect.Height - 14) / 2, 14, 14);
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
                    resultRect = new(offsetX, TitlebarRect.Bottom - 2 - CloseBtn_W, Rect.Width - CloseBtn_W - SideSize * 2, CloseBtn_W);
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
                if (Preview == Preview_Enum.W11 || Preview == Preview_Enum.W10 || Preview == Preview_Enum.W11Lite)
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

                else if (Preview == Preview_Enum.W10Lite || Preview == Preview_Enum.W8)
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
                        return AccentColor_Active.IsDark() ? Assets.Win10Preview.Win10x_Close_Dark : Assets.Win10Preview.Win10x_Close_Light;
                    }
                    else
                    {
                        return AccentColor_Inactive.IsDark() ? Assets.Win10Preview.Win10x_Close_Dark : Assets.Win10Preview.Win10x_Close_Light;
                    }
                }

                else
                {
                    return DarkMode ? Assets.Win10Preview.Win10x_Close_Dark : Assets.Win10Preview.Win10x_Close_Light;
                }
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

        protected override async void OnMouseMove(MouseEventArgs e)
        {
            if (!DesignMode && EnableEditingColors)
            {
                CursorOverTitlebar = (Preview == Preview_Enum.W11 || Preview == Preview_Enum.W10 || Preview == Preview_Enum.W11Lite || Preview == Preview_Enum.W10Lite) &&
                    TitlebarRect.Contains(e.Location) && !isMoving_Grip_topCenter;

                CursorOverWindowAccent = (Preview == Preview_Enum.W8 || Preview == Preview_Enum.W8Lite || Preview == Preview_Enum.W7Aero || Preview == Preview_Enum.W7Opaque) &&
                  Active && Rect.Contains(e.Location) && !ClientRect.Contains(e.Location);

                await Task.Delay(10);
                /* Don't make it controlled by conditions, they may be false and graphics won't be updated.
                   For high performance, we need to update graphics only in specified rectangle. */
                Invalidate(new Rectangle(TitlebarRect.X, TitlebarRect.Y, TitlebarRect.Width + 1, TitlebarRect.Height + 1));
            }

            if (EnableEditingMetrics)
            {
                if (isMoving_Grip_topCenter)
                {
                    Metrics_CaptionHeight = e.Location.Y - (_Metrics_PaddedBorderWidth + _Metrics_BorderWidth) - GripSize * 2;
                }
                if (isMoving_Grip_padding_left)
                {
                    Metrics_PaddedBorderWidth = (e.Location.X - (int)(GripSize * 0.5)) - _Metrics_BorderWidth;
                }
                else if (isMoving_Grip_borderWidth_left)
                {
                    Metrics_BorderWidth = (e.Location.X - (int)(GripSize * 0.5)) - _Metrics_PaddedBorderWidth;
                }
                else if (isMoving_Grip_padding_right)
                {
                    Metrics_PaddedBorderWidth = (Width - e.Location.X - (int)(GripSize * 0.5)) - _Metrics_BorderWidth;
                }
                else if (isMoving_Grip_borderWidth_right)
                {
                    Metrics_BorderWidth = (Width - e.Location.X - (int)(GripSize * 0.5)) - _Metrics_PaddedBorderWidth;
                }
                else if (isMoving_Grip_padding_bottom)
                {
                    Metrics_PaddedBorderWidth = (Height - e.Location.Y - (int)(GripSize * 0.5)) - _Metrics_BorderWidth;
                }
                else if (isMoving_Grip_borderWidth_bottom)
                {
                    Metrics_BorderWidth = (Height - e.Location.Y - (int)(GripSize * 0.5)) - _Metrics_PaddedBorderWidth;
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

                    await Task.Delay(10);
                    Invalidate();
                }
            }

            base.OnMouseMove(e);
        }

        protected override async void OnMouseLeave(EventArgs e)
        {
            if (!DesignMode && EnableEditingColors)
            {
                CursorOverTitlebar = false;
                CursorOverWindowAccent = false;

                await Task.Delay(10);
                Invalidate(new Rectangle(TitlebarRect.X, TitlebarRect.Y, TitlebarRect.Width + 1, TitlebarRect.Height + 1));
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

                await Task.Delay(10);
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
                    if (e.Button != MouseButtons.Right)
                    {
                        EditorInvoker?.Invoke(this, new EditorEventArgs(Active ? nameof(WindowsDesktop.TitlebarColor_Active) : nameof(WindowsDesktop.TitlebarColor_Inactive)));
                    }
                    else
                    {
                        EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(WindowsDesktop.DarkMode_App)));
                    }
                }

                else if (CursorOverWindowAccent)
                {
                    if (e.Button != MouseButtons.Right)
                    {
                        EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(WindowsDesktop.TitlebarColor_Active)));
                    }
                    else
                    {
                        EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(WindowsDesktop.AfterGlowColor_Active)));
                    }
                }
            }

            else if (!DesignMode && _MetricsEdit_CaptionFont)
            {
                using (FontDialog fd = new() { Font = this.Font })
                {
                    if (fd.ShowDialog() == DialogResult.OK)
                    {
                        Font = fd.Font;
                        EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Font)));
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

                    isMoving_Grip_padding_left = e.Button == MouseButtons.Right;
                    isMoving_Grip_borderWidth_left = e.Button != MouseButtons.Right;
                }
                else if (Grip_topRight.Contains(e.Location))
                {
                    isMoving_Grip_topCenter = true;

                    isMoving_Grip_padding_right = e.Button == MouseButtons.Right;
                    isMoving_Grip_borderWidth_right = e.Button != MouseButtons.Right;

                    isMoving_Grip_padding_right = e.Button == MouseButtons.Right;
                    isMoving_Grip_borderWidth_right = e.Button != MouseButtons.Right;
                }
            }

            base.OnMouseDown(e);
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
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            //Draw window itself
            if (Preview == Preview_Enum.W11 || Preview == Preview_Enum.W11Lite)
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

                #region Editor

                if (!DesignMode && EnableEditingColors && CursorOverTitlebar)
                {
                    Color color0 = Color.FromArgb(80, 255, 255, 255);
                    Color color1 = Color.FromArgb(80, 0, 0, 0);
                    using (Pen P = new(color0))
                    using (HatchBrush hb = new(HatchStyle.Percent25, color0, color1))
                    {
                        G.FillRoundedRect(hb, TitlebarRect, Radius, true);
                        G.DrawRoundedRect(P, TitlebarRect, Radius, true);
                    }
                }

                #endregion

                //Close button
                if (!ToolWindow)
                {
                    if (Preview == Preview_Enum.W11Lite)
                    {
                        G.SetClip(Rect.Round(Radius));

                        using (SolidBrush br = new(Color.FromArgb(195, 90, 80)))
                        {
                            Rectangle close_Lite = new(TitlebarRect.Right - CloseBtn_W * 2, TitlebarRect.Y, CloseBtn_W * 2, TitlebarRect.Height);
                            if (Active) G.FillRectangle(br, close_Lite);

                            using (Pen P = new(DarkMode ? Color.FromArgb(51, 51, 51) : Color.FromArgb(0, 0, 0)))
                            {
                                G.DrawLine(P, close_Lite.X, close_Lite.Y + 1, close_Lite.X, close_Lite.Y + close_Lite.Height);
                                G.DrawLine(P, close_Lite.X, close_Lite.Y + close_Lite.Height, close_Lite.X + close_Lite.Width - 1, close_Lite.Y + close_Lite.Height);
                            }

                            using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
                            {
                                using (SolidBrush br_close = new(Color.Black))
                                {
                                    G.DrawString("r", new Font("Marlett", 7.7f, FontStyle.Bold), br_close, new Rectangle(close_Lite.X + 1, close_Lite.Y + 1, close_Lite.Width, close_Lite.Height), sf);
                                }
                            }
                        }

                        G.ResetClip();
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
                        using (SolidBrush br = new(Preview == Preview_Enum.W11Lite ? Color.Black : Color.White))
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

            else if (Preview == Preview_Enum.W10 || Preview == Preview_Enum.W10Lite)
            {
                if (Shadow & Active & !DesignMode) { G.DrawGlow(Rect, Color.FromArgb(150, 0, 0, 0), 5, 15); }

                if (DarkMode) { using (SolidBrush br = new(Color.FromArgb(20, 20, 20))) { G.FillRectangle(br, Rect); } }

                else { using (SolidBrush br = new(Color.FromArgb(240, 240, 240))) { G.FillRectangle(br, Rect); } }

                if (Preview == Preview_Enum.W10)
                {
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
                }
                else
                {
                    G.ExcludeClip(ClientRect);
                    using (SolidBrush br = new(AccentColor_Active)) { G.FillRectangle(br, Rect); }
                    G.ResetClip();
                }

                if (AccentColor_Enabled)
                {
                    if (Active) { using (Pen P = new(Color.FromArgb(200, AccentColor_Active))) { G.DrawRectangle(P, Rect); } }

                    else { using (Pen P = new(Color.FromArgb(200, AccentColor_Inactive))) { G.DrawRectangle(P, Rect); } }
                }

                else if (DarkMode) { using (Pen P = new(Color.FromArgb(125, 100, 100, 100))) { G.DrawRectangle(P, Rect); } }

                else { using (Pen P = new(Color.FromArgb(125, 220, 220, 220))) { G.DrawRectangle(P, Rect); } }

                #region Editor

                if (!DesignMode && EnableEditingColors && CursorOverTitlebar)
                {
                    Color color0 = Color.FromArgb(80, 255, 255, 255);
                    Color color1 = Color.FromArgb(80, 0, 0, 0);
                    using (Pen P = new(color0))
                    using (HatchBrush hb = new(HatchStyle.Percent25, color0, color1))
                    {
                        G.FillRectangle(hb, TitlebarRect);
                        G.DrawRectangle(P, TitlebarRect);
                    }
                }

                #endregion

                //Close button
                if (!ToolWindow)
                {
                    if (Preview == Preview_Enum.W10Lite)
                    {
                        using (SolidBrush br = new(Color.FromArgb(195, 90, 80)))
                        {
                            Rectangle close_Lite = new(ClientRect.Right - CloseBtn_W * 2, TitlebarRect.Y, CloseBtn_W * 2, TitlebarRect.Height);
                            if (Active) G.FillRectangle(br, close_Lite);

                            using (Pen P = new(DarkMode ? Color.FromArgb(51, 51, 51) : Color.FromArgb(0, 0, 0)))
                            {
                                G.DrawLine(P, close_Lite.X, close_Lite.Y + 1, close_Lite.X, close_Lite.Y + close_Lite.Height);
                                G.DrawLine(P, close_Lite.X, close_Lite.Y + close_Lite.Height, close_Lite.X + close_Lite.Width - 1, close_Lite.Y + close_Lite.Height);
                            }

                            using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
                            {
                                using (SolidBrush br_close = new(Color.Black))
                                {
                                    G.DrawString("r", new Font("Marlett", 7.7f, FontStyle.Bold), br_close, new Rectangle(close_Lite.X + 1, close_Lite.Y + 1, close_Lite.Width, close_Lite.Height), sf);
                                }
                            }
                        }
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
                        using (SolidBrush br = new(Preview == Preview_Enum.W10Lite ? Color.Black : Color.White))
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

                    #region Editor

                    if (!DesignMode && EnableEditingColors && CursorOverWindowAccent)
                    {
                        Color color0 = Color.FromArgb(80, 255, 255, 255);
                        Color color1 = Color.FromArgb(80, 0, 0, 0);
                        using (Pen P = new(color0))
                        using (HatchBrush hb = new(HatchStyle.Percent25, color0, color1))
                        {
                            G.FillRectangle(hb, Rect);
                            G.DrawRectangle(P, Rect);
                        }
                    }

                    #endregion

                    using (SolidBrush br = new(ClientAreaColor)) { G.FillRectangle(br, ClientRect); }

                    Bitmap CloseBtn;

                    if (!ToolWindow)
                    {
                        if (CloseButtonRect.Height >= 27) { CloseBtn = Assets.Win81Preview.Close_3; }

                        else if (CloseButtonRect.Height >= 24) { CloseBtn = Assets.Win81Preview.Close_2; }

                        else if (CloseButtonRect.Height >= 21) { CloseBtn = Assets.Win81Preview.Close_1; }

                        else { CloseBtn = Assets.Win81Preview.Close_0; }
                    }

                    else { CloseBtn = Assets.Win81Preview.Close_ToolWindow; }

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

                            decimal alpha = Active ? 1 - (decimal)Win7Alpha / 100 : (decimal)0.25;   // ColorBlurBalance
                            decimal ColBal = (decimal)Win7ColorBal / 100;   // ColorBalance
                            decimal GlowBal = (decimal)Win7GlowBal / 100;   // AfterGlowBalance

                            Color Color1 = Active ? AccentColor_Active : Color.FromArgb(128, AccentColor_Inactive);
                            Color Color2 = Active ? AccentColor2_Active : Color.Transparent;
                            G.ExcludeClip(ClientBorderRect);
                            G.DrawAeroEffect(Rect, bk, Color1, ColBal, Color2, GlowBal, alpha, Radius, !ToolWindow);
                            G.ResetClip();
                        }

                        else if (!ToolWindow)
                        {
                            using (SolidBrush br = new(Color.White)) { G.FillRoundedRect(br, Rect, Radius, true); }

                            using (SolidBrush br = new(Color.FromArgb((int)Math.Round(255 * Win7ColorBal / 100d), Active ? AccentColor_Active : AccentColor_Active.Light())))
                            {
                                G.FillRoundedRect(br, Rect, Radius, true);
                            }
                        }

                        else
                        {
                            using (SolidBrush br = new(Color.White)) { G.FillRectangle(br, Rect); }

                            using (SolidBrush br = new(Color.FromArgb((int)Math.Round(255 * Win7ColorBal / 100d), Active ? AccentColor_Active : AccentColor_Active.Light())))
                            {
                                G.FillRectangle(br, Rect);
                            }
                        }

                        //Right and left glow panels
                        if (Active)
                        {
                            G.DrawImage(Assets.Win7Preview.WindowSides, GlassSide1);
                            G.DrawImage(Assets.Win7Preview.WindowSides, GlassSide2);

                            int TitleTopW = (int)Math.Round(Rect.Width * 0.6d);
                            int TitleTopH = (int)Math.Round(Rect.Height * 0.6d);

                            G.SetClip(RectBorder);
                            G.DrawImage(Assets.Win7Preview.TitleTopL, new Rectangle(Rect.X + (ToolWindow ? -1 : 1), Rect.Y, TitleTopW, TitleTopH));
                            G.DrawImage(Assets.Win7Preview.TitleTopR, new Rectangle(Rect.X + Rect.Width - TitleTopW + 1, Rect.Y, TitleTopW, TitleTopH));
                            G.ResetClip();
                        }

                        //Window borders
                        if (!ToolWindow)
                        {
                            if (Noise7 != null)
                                G.DrawRoundImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), Rect, Radius, true);

                            using (Pen P = new(Color.FromArgb(Active ? 130 : 100, 25, 25, 25))) { G.DrawRoundedRect(P, Rect, Radius, true); }

                            using (Pen P = new(Color.FromArgb(100, 255, 255, 255))) { G.DrawRoundedRect(P, RectBorder, Radius, true); }

                            using (Pen P = new(Color.FromArgb((int)Math.Round(255d - 255 * Win7Alpha / 300d), base.BackColor.Light(0.2f)))) { G.DrawRoundedRect(P, ClientBorderRect, 1, true); }

                            #region Editor

                            if (!DesignMode && EnableEditingColors && CursorOverWindowAccent)
                            {
                                Color color0 = Color.FromArgb(80, 255, 255, 255);
                                Color color1 = Color.FromArgb(80, 0, 0, 0);
                                using (Pen P = new(color0))
                                using (HatchBrush hb = new(HatchStyle.Percent25, color0, color1))
                                {
                                    G.FillRoundedRect(hb, Rect, Radius, true);
                                    G.DrawRoundedRect(P, Rect, Radius, true);
                                }
                            }

                            #endregion

                            using (SolidBrush br = new(ClientAreaColor)) { G.FillRoundedRect(br, ClientBorderRect, 1, true); }

                            using (Pen P = new(Color.FromArgb((int)Math.Round(255d - 255 * Win7Alpha / 300d), base.BackColor.Dark(0.2f)))) { G.DrawRoundedRect(P, ClientRect, 1, true); }
                        }

                        else
                        {
                            if (Noise7 != null)
                                G.DrawImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), Rect);

                            using (Pen P = new(Color.FromArgb(Active ? 130 : 100, 25, 25, 25))) { G.DrawRectangle(P, Rect); }

                            using (Pen P = new(Color.FromArgb(100, 255, 255, 255))) { G.DrawRectangle(P, RectBorder); }

                            using (Pen P = new(Color.FromArgb((int)Math.Round(255d - 255 * Win7Alpha / 300d), base.BackColor.Light(0.2f)))) { G.DrawRectangle(P, ClientBorderRect); }

                            #region Editor

                            if (!DesignMode && EnableEditingColors && CursorOverWindowAccent)
                            {
                                Color color0 = Color.FromArgb(80, 255, 255, 255);
                                Color color1 = Color.FromArgb(80, 0, 0, 0);
                                using (Pen P = new(color0))
                                using (HatchBrush hb = new(HatchStyle.Percent25, color0, color1))
                                {
                                    G.FillRoundedRect(hb, Rect, Radius, true);
                                    G.DrawRoundedRect(P, Rect, Radius, true);
                                }
                            }

                            #endregion

                            using (SolidBrush br = new(ClientAreaColor)) { G.FillRectangle(br, ClientBorderRect); }

                            using (Pen P = new(Color.FromArgb((int)Math.Round(255d - 255 * Win7Alpha / 300d), base.BackColor.Dark(0.2f)))) { G.DrawRectangle(P, ClientRect); }
                        }

                        //Close button
                        if (!ToolWindow)
                        {
                            Bitmap closeBtn;

                            if (Active)
                            {
                                if (!WinVista) { closeBtn = Assets.Win7Preview.Close_Active; }

                                else { closeBtn = Assets.WinVistaPreview.Close_Active; }
                            }
                            else if (!WinVista) { closeBtn = Assets.Win7Preview.Close_inactive; }

                            else { closeBtn = Assets.WinVistaPreview.Close_Inactive; }

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

                            CloseBtn = Assets.Win7Preview.Close_Basic_ToolWindow;

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
                            G.DrawImage(Assets.Win7Preview.WindowSides, GlassSide1);
                            G.DrawImage(Assets.Win7Preview.WindowSides, GlassSide2);
                        }

                        G.ResetClip();

                        G.FillRectangle(Brushes.White, ClientBorderRect);
                        using (Pen P = new(Color.FromArgb(186, 210, 234))) { G.DrawRectangle(P, ClientBorderRect); }
                        if (!WinVista) { using (Pen P = new(Color.FromArgb(130, 135, 144))) { G.DrawRectangle(P, ClientRect); } }

                        //Render close button
                        int Btn_Height = Math.Max(10, _Metrics_CaptionHeight - 5);
                        int Btn_Width;
                        Btn_Width = !ToolWindow ? (int)Math.Round(31d / 17d * Btn_Height) : Btn_Height;
                        CloseButtonRect = new(ClientBorderRect.Right - Btn_Width - 3, ClientBorderRect.Top - Btn_Height - 3, Btn_Width, Btn_Height);

                        if (Active)
                        {
                            float Factor = 0.45f;
                            if (ToolWindow)
                                Factor = 0.2f;

                            float UH = Factor * CloseButtonRect.Height;
                            float LH = CloseButtonRect.Height - UH;
                            float Interlapping = UH / CloseButtonRect.Height * 10f;

                            Rectangle CloseRectUpperHalf = new(CloseButtonRect.X, CloseButtonRect.Y, CloseButtonRect.Width, (int)Math.Round(UH + Interlapping));
                            Rectangle CloseRectLowerHalf = new(CloseButtonRect.X, (int)Math.Round(CloseRectUpperHalf.Bottom - Interlapping), CloseButtonRect.Width, (int)Math.Round(LH));

                            LinearGradientBrush CloseUpperPath = new(CloseRectUpperHalf, CloseUpperAccent1, CloseUpperAccent2, LinearGradientMode.Vertical);
                            LinearGradientBrush CloseLowerPath = new(CloseRectLowerHalf, CloseLowerAccent1, CloseLowerAccent2, LinearGradientMode.Vertical);

                            G.FillRoundedRect(CloseUpperPath, CloseRectUpperHalf, 1, true);
                            G.FillRoundedRect(CloseLowerPath, CloseRectLowerHalf, 1, true);
                        }

                        else
                        {
                            LinearGradientBrush ClosePath = new(CloseButtonRect, CloseUpperAccent1, CloseLowerAccent2, LinearGradientMode.Vertical);
                            G.FillRectangle(ClosePath, CloseButtonRect);
                        }

                        Bitmap CloseBtn;

                        if (!ToolWindow)
                        {
                            if (CloseButtonRect.Height >= 22) { CloseBtn = Assets.Win7Preview.Close_Basic_2; }
                            else if (CloseButtonRect.Height >= 18) { CloseBtn = Assets.Win7Preview.Close_Basic_1; }
                            else { CloseBtn = Assets.Win7Preview.Close_Basic_0; }
                        }

                        else { CloseBtn = Assets.Win7Preview.Close_Basic_ToolWindow; }

                        G.DrawImage(CloseBtn, new Point((int)Math.Round(CloseButtonRect.X + (CloseButtonRect.Width - CloseBtn.Width) / 2d + 1d), (int)Math.Round(CloseButtonRect.Y + (CloseButtonRect.Height - CloseBtn.Height) / 2d)));

                        using (Pen P = new(CloseOuterBorder)) { G.DrawRoundedRect(P, CloseButtonRect, 1, true); }

                        using (Pen P = new(CloseInnerBorder)) { G.DrawRoundedRect(P, new Rectangle(CloseButtonRect.X + 1, CloseButtonRect.Y + 1, CloseButtonRect.Width - 2, CloseButtonRect.Height - 2), 1, true); }

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
                SmoothingMode sm = G.SmoothingMode;
                G.SmoothingMode = SmoothingMode.HighSpeed;

                using (SolidBrush br = new(WindowsXPColor)) { G.FillRectangle(br, ClientBorderRect); }

                resVS?.Draw(G, TitlebarRect, VisualStylesRes.Element.Titlebar, Active, ToolWindow);

                CloseButtonRect = new(ClientRect.Right - CloseBtn_W - RightEdge_XP.Width + 4, Rect.Y + TitlebarRect.Height - CloseBtn_W - 3, CloseBtn_W, CloseBtn_W);

                resVS?.Draw(G, LeftEdge_XP, VisualStylesRes.Element.LeftEdge, Active, ToolWindow);
                resVS?.Draw(G, RightEdge_XP, VisualStylesRes.Element.RightEdge, Active, ToolWindow);
                resVS?.Draw(G, ButtomEdge_XP, VisualStylesRes.Element.BottomEdge, Active, ToolWindow);
                resVS?.Draw(G, CloseButtonRect, VisualStylesRes.Element.CloseButton, Active, ToolWindow);

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

            //Draw window icon
            if (!ToolWindow)
                G.DrawImage(Active ? Properties.Resources.SampleApp_Small_Active : Properties.Resources.SampleApp_Small_Inactive, IconRect);

            // Editor
            if (!DesignMode && _MetricsEdit_Grip)
            {
                using (Pen P = new(Color.Gray) { DashStyle = DashStyle.Dot })
                {
                    G.DrawRectangle(P, editingRect);
                }

                using (Pen P = new(Color.Gray) { DashStyle = DashStyle.Solid })
                {
                    G.FillRoundedRect(Brushes.White, Grip_topLeft, 1, true);
                    G.FillRoundedRect(Brushes.White, Grip_topRight, 1, true);
                    G.FillRoundedRect(Brushes.White, Grip_bottomLeft, 1, true);
                    G.FillRoundedRect(Brushes.White, Grip_bottomRight, 1, true);
                    G.FillRoundedRect(Brushes.White, Grip_topCenter, 1, true);
                    G.FillRoundedRect(Brushes.White, Grip_bottomCenter, 1, true);
                    G.FillRoundedRect(Brushes.White, Grip_leftCenter, 1, true);
                    G.FillRoundedRect(Brushes.White, Grip_rightCenter, 1, true);

                    G.DrawRoundedRect(P, Grip_topLeft, 1, true);
                    G.DrawRoundedRect(P, Grip_topRight, 1, true);
                    G.DrawRoundedRect(P, Grip_bottomLeft, 1, true);
                    G.DrawRoundedRect(P, Grip_bottomRight, 1, true);
                    G.DrawRoundedRect(P, Grip_topCenter, 1, true);
                    G.DrawRoundedRect(P, Grip_bottomCenter, 1, true);
                    G.DrawRoundedRect(P, Grip_leftCenter, 1, true);
                    G.DrawRoundedRect(P, Grip_rightCenter, 1, true);
                }
            }

            #region Editor

            if (!DesignMode && EnableEditingMetrics && CursorOverTitlebar)
            {
                Color color0 = Color.FromArgb(80, 255, 255, 255);
                Color color1 = Color.FromArgb(80, 0, 0, 0);
                using (Pen P = new(color0))
                using (HatchBrush hb = new(HatchStyle.Percent25, color0, color1))
                {
                    G.FillRoundedRect(hb, TitlebarRect, Radius, true);
                    G.DrawRoundedRect(P, TitlebarRect, Radius, true);
                }
            }

            #endregion

            base.OnPaint(e);
        }
    }
}