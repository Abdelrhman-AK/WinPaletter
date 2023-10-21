using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace WinPaletter.UI.Simulation
{

    [Description("Simulated Windows elements")]
    public partial class WinElement : ContainerControl
    {
        public WinElement()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            this.MouseMove += WinElement_MouseMove;
            this.MouseDown += WinElement_MouseMove;
            this.MouseUp += WinElement_MouseMove;
            this.MouseLeave += WinElement_MouseLeave;
            this.HandleCreated += WinElement_HandleCreated;
        }

        #region Variables
        private TextureBrush Noise = new TextureBrush(Properties.Resources.GaussianBlur.Fade(0.15d));
        private Bitmap Noise7 = Properties.Resources.AeroGlass;
        private Bitmap Noise7Start = Properties.Resources.Start7Glass;
        private Bitmap adaptedBack;
        private Bitmap adaptedBackBlurred;
        private Rectangle Button1;
        private Rectangle Button2;
        private MouseState _State_Btn1, _State_Btn2;
        public enum MouseState
        {
            Normal,
            Hover,
            Pressed
        }
        private Styles _Style = Styles.Start11;
        public enum Styles
        {
            Start11,
            Taskbar11,
            ActionCenter11,
            AltTab11,
            Start10,
            Taskbar10,
            ActionCenter10,
            AltTab10,
            Start8,
            Taskbar8Aero,
            Taskbar8Lite,
            AltTab8Aero,
            AltTab8AeroLite,
            Start7Aero,
            Taskbar7Aero,
            Start7Opaque,
            Taskbar7Opaque,
            Start7Basic,
            Taskbar7Basic,
            AltTab7Aero,
            AltTab7Opaque,
            AltTab7Basic,
            StartVistaAero,
            TaskbarVistaAero,
            StartVistaOpaque,
            TaskbarVistaOpaque,
            StartVistaBasic,
            TaskbarVistaBasic,
            StartXP,
            TaskbarXP
        }
        #endregion

        #region Properties
        public Styles Style
        {
            get
            {
                return _Style;
            }
            set
            {
                _Style = value;
                ProcessBack();
                Refresh();
            }
        }

        private byte _BackColorAlpha = 130;
        public int BackColorAlpha
        {
            get
            {
                return _BackColorAlpha;
            }
            set
            {
                _BackColorAlpha = (byte)value;
                if (!SuspendRefresh)
                    Refresh();
            }
        }

        private float _NoisePower = 0.15f;
        public float NoisePower
        {
            get
            {
                return _NoisePower;
            }
            set
            {
                _NoisePower = value;

                if (Style == Styles.Taskbar7Aero)
                {
                    try
                    {
                        Noise7 = Properties.Resources.AeroGlass.Fade(NoisePower / 100f);
                    }
                    catch
                    {
                    }
                }

                if (Style == Styles.Start7Aero)
                {
                    try
                    {
                        Noise7Start = Properties.Resources.Start7Glass.Fade(NoisePower / 100f);
                    }
                    catch
                    {
                    }
                }

                if (!SuspendRefresh)
                    NoiseBack();
                if (!SuspendRefresh)
                    Refresh();
            }
        }

        private int _BlurPower = 8;
        public int BlurPower
        {
            get
            {
                return _BlurPower;
            }
            set
            {
                _BlurPower = value;
                BlurBack();
                if (!SuspendRefresh)
                    Refresh();
            }
        }

        private bool _Transparency = true;
        public bool Transparency
        {
            get
            {
                return _Transparency;
            }
            set
            {
                _Transparency = value;
                ProcessBack();
                if (!SuspendRefresh)
                    Refresh();
            }
        }

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

        private Color _AppUnderline;
        public Color AppUnderline
        {
            get
            {
                return _AppUnderline;
            }
            set
            {
                _AppUnderline = value;
                try
                {
                    if (!SuspendRefresh)
                        Refresh();
                }
                catch
                {
                }
            }
        }

        private Color _AppBackground;
        public Color AppBackground
        {
            get
            {
                return _AppBackground;
            }
            set
            {
                _AppBackground = value;
                try
                {
                    if (!SuspendRefresh)
                        Refresh();
                }
                catch
                {
                }
            }
        }

        private Color _ActionCenterButton_Normal;
        public Color ActionCenterButton_Normal
        {
            get
            {
                return _ActionCenterButton_Normal;
            }
            set
            {
                _ActionCenterButton_Normal = value;
                try
                {
                    if (!SuspendRefresh)
                        Refresh();
                }
                catch
                {
                }
            }
        }

        private Color _ActionCenterButton_Hover;
        public Color ActionCenterButton_Hover
        {
            get
            {
                return _ActionCenterButton_Hover;
            }
            set
            {
                _ActionCenterButton_Hover = value;
                try
                {
                    if (!SuspendRefresh)
                        Refresh();
                }
                catch
                {
                }
            }
        }

        private Color _ActionCenterButton_Pressed;
        public Color ActionCenterButton_Pressed
        {
            get
            {
                return _ActionCenterButton_Pressed;
            }
            set
            {
                _ActionCenterButton_Pressed = value;
                try
                {
                    if (!SuspendRefresh)
                        Refresh();
                }
                catch
                {
                }
            }
        }

        private Color _StartColor;
        public Color StartColor
        {
            get
            {
                return _StartColor;
            }
            set
            {
                _StartColor = value;
                if (!SuspendRefresh)
                    Refresh();
            }
        }

        private Color _LinkColor;
        public Color LinkColor
        {
            get
            {
                return _LinkColor;
            }
            set
            {
                _LinkColor = value;
                if (!SuspendRefresh)
                    Refresh();
            }
        }

        private Color _Background;
        public Color Background
        {
            get
            {
                return _Background;
            }
            set
            {
                _Background = value;
                if (!SuspendRefresh)
                    Refresh();
            }
        }

        private Color _Background2;
        public Color Background2
        {
            get
            {
                return _Background2;
            }
            set
            {
                _Background2 = value;
                if (!SuspendRefresh)
                    Refresh();
            }
        }

        private int _Win7ColorBal = (int)Math.Round(0.15);
        public int Win7ColorBal
        {
            get
            {
                return _Win7ColorBal;
            }
            set
            {
                _Win7ColorBal = value;
                if (!SuspendRefresh)
                    Refresh();
            }
        }

        private int _Win7GlowBal = (int)Math.Round(0.15);
        public int Win7GlowBal
        {
            get
            {
                return _Win7GlowBal;
            }
            set
            {
                _Win7GlowBal = value;
                if (!SuspendRefresh)
                    Refresh();
            }
        }
        public bool UseWin11ORB_WithWin10 { get; set; } = false;
        public bool UseWin11RoundedCorners_WithWin10_Level1 { get; set; } = false;
        public bool UseWin11RoundedCorners_WithWin10_Level2 { get; set; } = false;
        public bool Shadow { get; set; } = true;
        public bool SuspendRefresh { get; set; } = false;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | 0x20;
                return cp;
            }
        }
        #endregion

        #region Voids/Functions
        public void CopycatFrom(WinElement element)
        {
            Style = element.Style;
            _NoisePower = element.NoisePower;
            _BlurPower = element.BlurPower;
            _Transparency = element.Transparency;
            _DarkMode = element.DarkMode;
            _AppUnderline = element.AppUnderline;
            _AppBackground = element.AppBackground;
            _ActionCenterButton_Normal = element.ActionCenterButton_Normal;
            _ActionCenterButton_Hover = element.ActionCenterButton_Hover;
            _ActionCenterButton_Pressed = element.ActionCenterButton_Pressed;
            _StartColor = element.StartColor;
            _LinkColor = element.LinkColor;
            _BackColorAlpha = (byte)element.BackColorAlpha;
            BackColor = element.BackColor;
            _Background2 = element.Background2;
            _Win7ColorBal = element.Win7ColorBal;
            _Win7GlowBal = element.Win7GlowBal;
            UseWin11ORB_WithWin10 = element.UseWin11ORB_WithWin10;
            UseWin11RoundedCorners_WithWin10_Level1 = element.UseWin11RoundedCorners_WithWin10_Level1;
            UseWin11RoundedCorners_WithWin10_Level2 = element.UseWin11RoundedCorners_WithWin10_Level2;
            Shadow = element.Shadow;

            Dock = element.Dock;
            Size = element.Size;
            Location = element.Location;
            Text = element.Text;

            try
            {
                if (!SuspendRefresh)
                {
                    ProcessBack();
                    Refresh();
                }
            }
            catch
            {
            }
        }

        public void ProcessBack()
        {
            GetBack();
            BlurBack();
            // NoiseBack()
        }

        public void GetBack()
        {
            try
            {
                Bitmap Wallpaper;
                if (Parent.BackgroundImage is null)
                    Wallpaper = Program.Wallpaper;
                else
                    Wallpaper = (Bitmap)Parent.BackgroundImage;

                if (Wallpaper is not null)
                    adaptedBack = Wallpaper.Clone(Bounds, Wallpaper.PixelFormat);
            }
            catch { }

            try
            {
                //if (Style == Styles.Start7Aero | Style == Styles.Taskbar7Aero | Style == Styles.StartVistaAero | Style == Styles.TaskbarVistaAero | Style == Styles.AltTab7Aero)
                //{
                //    adaptedBackBlurred = new Bitmap(adaptedBack).Blur(1);
                //}
            }
            catch { }
        }

        public void BlurBack()
        {
            try
            {
                if (Style == Styles.Taskbar11 | Style == Styles.Start11 | Style == Styles.ActionCenter11 | Style == Styles.AltTab11)
                {

                    if (Transparency)
                    {
                        if (adaptedBack is not null)
                        {
                            Bitmap b = new Bitmap(adaptedBack).Blur(BlurPower);
                            if (DarkMode)
                            {
                                if (b is not null)
                                {
                                    using (var ImgF = new ImageProcessor.ImageFactory())
                                    {
                                        ImgF.Load(b);
                                        ImgF.Saturation(60);
                                        adaptedBackBlurred = (Bitmap)ImgF.Image.Clone();
                                    }
                                }
                            }

                            else
                            {
                                adaptedBackBlurred = b;
                            }
                        }
                    }
                }

                else if (Style == Styles.Taskbar10 | Style == Styles.Start10 | Style == Styles.ActionCenter10)
                {

                    if (Transparency)
                        adaptedBackBlurred = new Bitmap(adaptedBack).Blur(BlurPower);
                }

                else if (Style == Styles.Start7Aero | Style == Styles.Taskbar7Aero | Style == Styles.StartVistaAero | Style == Styles.TaskbarVistaAero | Style == Styles.AltTab7Aero)
                {
                    if (adaptedBack is not null)
                        adaptedBackBlurred = new Bitmap(adaptedBack).Blur(3);
                }
            }
            catch
            {
            }
        }

        public void NoiseBack()
        {

            if (Style == Styles.ActionCenter11 | Style == Styles.Start11 | Style == Styles.Taskbar11 | Style == Styles.AltTab11)
            {
                if (Transparency)
                    Noise = new TextureBrush(Properties.Resources.GaussianBlur.Fade(NoisePower));
            }

            else if (Style == Styles.ActionCenter10 | Style == Styles.Start10 | Style == Styles.Taskbar10)
            {
                if (Transparency)
                    Noise = new TextureBrush(Properties.Resources.GaussianBlur.Fade(NoisePower));
            }

            else if (Style == Styles.Start7Aero | Style == Styles.Taskbar7Aero | Style == Styles.AltTab7Aero | Style == Styles.AltTab7Opaque)
            {
                try
                {
                    Noise7 = Properties.Resources.AeroGlass.Fade(NoisePower / 100f);
                }
                catch
                {
                }
                try
                {
                    Noise7Start = Properties.Resources.Start7Glass.Fade(NoisePower / 100f);
                }
                catch
                {
                }
            }
        }
        #endregion

        #region Events
        private void WinElement_MouseMove(object sender, MouseEventArgs e)
        {
            if (Style == Styles.ActionCenter11)
            {

                if (Button1.Contains(PointToClient(MousePosition)))
                {
                    if (e.Button == MouseButtons.None)
                        _State_Btn1 = MouseState.Hover;
                    else
                        _State_Btn1 = MouseState.Pressed;
                    if (!SuspendRefresh)
                        Refresh();
                }
                else if (!(_State_Btn1 == MouseState.Normal))
                {
                    _State_Btn1 = MouseState.Normal;
                    if (!SuspendRefresh)
                        Refresh();
                }

                if (Button2.Contains(PointToClient(MousePosition)))
                {
                    if (e.Button == MouseButtons.None)
                        _State_Btn2 = MouseState.Hover;
                    else
                        _State_Btn2 = MouseState.Pressed;
                    if (!SuspendRefresh)
                        Refresh();
                }
                else if (!(_State_Btn2 == MouseState.Normal))
                {
                    _State_Btn2 = MouseState.Normal;
                    if (!SuspendRefresh)
                        Refresh();
                }

            }
        }
        private void WinElement_MouseLeave(object sender, EventArgs e)
        {
            if (Style == Styles.ActionCenter11)
            {
                _State_Btn1 = MouseState.Normal;
                _State_Btn2 = MouseState.Normal;
                if (!SuspendRefresh)
                    Refresh();
            }
        }
        private void WinElement_HandleCreated(object sender, EventArgs e)
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
                    SizeChanged += ProcessBack_EventHandler;
                }
                catch
                {
                }
                try
                {
                    LocationChanged += ProcessBack_EventHandler;
                }
                catch
                {
                }

                ProcessBack();
            }
        }
        public void ProcessBack_EventHandler(object sender, EventArgs e)
        {
            ProcessBack();
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = Config.RenderingHint;
            DoubleBuffered = true;
            var Rect = new Rectangle(-1, -1, Width + 2, Height + 2);
            var RRect = new Rectangle(0, 0, Width - 1, Height - 1);
            int Radius = 5;

            switch (Style)
            {
                case Styles.Start11:
                    #region Start 11
                    {
                        if (!DesignMode && Transparency && adaptedBackBlurred is not null)
                            G.DrawRoundImage(adaptedBackBlurred, RRect, Radius, true);

                        if (DarkMode)
                        {
                            using (var br = new SolidBrush(Color.FromArgb(85, 28, 28, 28)))
                            {
                                G.FillRoundedRect(br, RRect, Radius, true);
                            }
                        }
                        else
                        {
                            using (var br = new SolidBrush(Color.FromArgb(75, 255, 255, 255)))
                            {
                                G.FillRoundedRect(br, RRect, Radius, true);
                            }
                        }

                        using (var br = new SolidBrush(Color.FromArgb(Transparency ? BackColorAlpha : 255, Background)))
                        {
                            G.FillRoundedRect(br, RRect, Radius, true);
                        }
                        if (Transparency)
                            G.FillRoundedRect(Noise, RRect, Radius, true);
                        var SearchRect = new Rectangle(8, 10, Width - 8 * 2, 15);

                        G.DrawRoundImage(DarkMode ? Properties.Resources.Start11_Dark : Properties.Resources.Start11_Light, RRect, Radius, true);

                        Color SearchColor, SearchBorderColor;
                        if (DarkMode)
                        {
                            SearchColor = Color.FromArgb(150, 28, 28, 28);
                            SearchBorderColor = Color.FromArgb(150, 65, 65, 65);
                        }
                        else
                        {
                            SearchColor = Color.FromArgb(175, 255, 255, 255);
                            SearchBorderColor = Color.FromArgb(175, 200, 200, 200);
                        }

                        using (var br = new SolidBrush(SearchColor))
                        {
                            G.FillRoundedRect(br, SearchRect, 8, true);
                        }
                        using (var P = new Pen(SearchBorderColor))
                        {
                            G.DrawRoundedRect(P, SearchRect, 8, true);
                        }

                        using (var P = new Pen(Color.FromArgb(150, 90, 90, 90)))
                        {
                            G.DrawRoundedRect(P, RRect, Radius, true);
                        }

                        break;
                    }
                #endregion

                case Styles.ActionCenter11:
                    #region Action Center 11
                    {
                        if (!DesignMode && Transparency && adaptedBackBlurred is not null)
                            G.DrawRoundImage(adaptedBackBlurred, RRect, Radius, true);

                        if (DarkMode)
                        {
                            using (var br = new SolidBrush(Color.FromArgb(85, 28, 28, 28)))
                            {
                                G.FillRoundedRect(br, RRect, Radius, true);
                            }
                        }
                        else
                        {
                            using (var br = new SolidBrush(Color.FromArgb(75, 255, 255, 255)))
                            {
                                G.FillRoundedRect(br, RRect, Radius, true);
                            }
                        }

                        using (var br = new SolidBrush(Color.FromArgb(Transparency ? BackColorAlpha : 255, Background)))
                        {
                            G.FillRoundedRect(br, RRect, Radius, true);
                        }
                        if (Transparency)
                            G.FillRoundedRect(Noise, RRect, Radius, true);
                        Button1 = new Rectangle(8, 8, 49, 20);
                        Button2 = new Rectangle(62, 8, 49, 20);

                        G.DrawRoundImage(DarkMode ? Properties.Resources.AC_11_Dark : Properties.Resources.AC_11_Light, RRect, Radius, true);

                        Color Cx1 = default, Cx2 = default;

                        switch (_State_Btn1)
                        {
                            case var case2 when case2 == MouseState.Normal:
                                {
                                    Cx1 = ActionCenterButton_Normal;
                                    break;
                                }
                            case var case3 when case3 == MouseState.Hover:
                                {
                                    Cx1 = ActionCenterButton_Hover;
                                    break;
                                }
                            case var case4 when case4 == MouseState.Pressed:
                                {
                                    Cx1 = ActionCenterButton_Pressed;
                                    break;
                                }
                        }

                        switch (_State_Btn2)
                        {
                            case var case5 when case5 == MouseState.Normal:
                                {
                                    Cx2 = DarkMode ? Color.FromArgb(190, 70, 70, 70) : Color.FromArgb(180, 140, 140, 140);
                                    break;
                                }
                            case var case6 when case6 == MouseState.Hover:
                                {
                                    Cx2 = DarkMode ? Color.FromArgb(190, 90, 90, 90) : Color.FromArgb(210, 230, 230, 230);
                                    break;
                                }
                            case var case7 when case7 == MouseState.Pressed:
                                {
                                    Cx2 = DarkMode ? Color.FromArgb(190, 75, 75, 75) : Color.FromArgb(210, 210, 210, 210);
                                    break;
                                }
                        }

                        using (var br = new SolidBrush(Cx1))
                        {
                            G.FillRoundedRect(br, Button1, Radius, true);
                        }
                        using (var P = new Pen(Cx1.Light(0.15f)))
                        {
                            G.DrawRoundedRect_LikeW11(P, Button1, Radius, true);
                        }
                        using (var br = new SolidBrush(Cx2))
                        {
                            G.FillRoundedRect(br, Button2, Radius, true);
                        }
                        using (var P = new Pen(Cx2.CB(DarkMode ? 0.05d : -0.05d)))
                        {
                            G.DrawRoundedRect(P, Button2, Radius, true);
                        }
                        using (var P = new Pen(Color.FromArgb(150, 90, 90, 90)))
                        {
                            G.DrawRoundedRect(P, RRect, Radius, true);
                        }

                        break;
                    }
                #endregion

                case Styles.Taskbar11:
                    #region Taskbar 11
                    {
                        if (!DesignMode && Transparency && adaptedBackBlurred is not null)
                            G.DrawImage(adaptedBackBlurred, Rect);

                        if (DarkMode)
                        {
                            using (var br = new SolidBrush(Color.FromArgb(110, 28, 28, 28)))
                            {
                                G.FillRectangle(br, Rect);
                            }
                        }
                        else
                        {
                            using (var br = new SolidBrush(Color.FromArgb(90, 255, 255, 255)))
                            {
                                G.FillRectangle(br, Rect);
                            }
                        }

                        using (var br = new SolidBrush(Color.FromArgb(Transparency ? BackColorAlpha : 255, Background)))
                        {
                            G.FillRectangle(br, Rect);
                        }
                        if (Transparency)
                            G.FillRoundedRect(Noise, RRect, Radius, true);

                        var StartBtnRect = new Rectangle(8, 3, 36, 36);
                        var StartImgRect = new Rectangle(8, 3, 37, 37);

                        var App2BtnRect = new Rectangle(StartBtnRect.Right + 5, 3, 36, 36);
                        var App2ImgRect = new Rectangle(StartBtnRect.Right + 5, 3, 37, 37);
                        var App2BtnRectUnderline = new Rectangle(App2BtnRect.X + (App2BtnRect.Width - 8) / 2, App2BtnRect.Y + App2BtnRect.Height - 3, 8, 3);

                        var AppBtnRect = new Rectangle(App2BtnRect.Right + 5, 3, 36, 36);
                        var AppImgRect = new Rectangle(App2BtnRect.Right + 5, 3, 37, 37);
                        var AppBtnRectUnderline = new Rectangle(AppBtnRect.X + (AppBtnRect.Width - 18) / 2, AppBtnRect.Y + AppBtnRect.Height - 3, 18, 3);

                        Color BackC;
                        Color BorderC;

                        if (DarkMode)
                        {
                            BackC = Color.FromArgb(45, 130, 130, 130);
                            BorderC = Color.FromArgb(45, 130, 130, 130);
                        }
                        else
                        {
                            BackC = Color.FromArgb(35, 255, 255, 255);
                            BorderC = Color.FromArgb(35, 255, 255, 255);
                        }

                        using (var br = new SolidBrush(BackC))
                        {
                            G.FillRoundedRect(br, StartBtnRect, 3, true);
                        }
                        using (var P = new Pen(BorderC))
                        {
                            G.DrawRoundedRect_LikeW11(P, StartBtnRect, 3);
                        }
                        G.DrawImage(DarkMode ? Properties.Resources.StartBtn_11Dark : Properties.Resources.StartBtn_11Light, StartImgRect);

                        using (var br = new SolidBrush(BackC))
                        {
                            G.FillRoundedRect(br, AppBtnRect, 3, true);
                        }
                        using (var P = new Pen(BorderC))
                        {
                            G.DrawRoundedRect_LikeW11(P, AppBtnRect, 3);
                        }
                        G.DrawImage(Properties.Resources.SampleApp_Active, AppImgRect);
                        using (var br = new SolidBrush(_AppUnderline))
                        {
                            G.FillRoundedRect(br, AppBtnRectUnderline, 2, true);
                        }

                        G.DrawImage(Properties.Resources.SampleApp_Inactive, App2ImgRect);
                        using (var br = new SolidBrush(Color.FromArgb(255, BackC)))
                        {
                            G.FillRoundedRect(br, App2BtnRectUnderline, 2, true);
                        }

                        using (var P = new Pen(Color.FromArgb(100, 100, 100, 100)))
                        {
                            G.DrawLine(P, new Point(0, 0), new Point(Width - 1, 0));
                        }

                        break;
                    }
                #endregion

                case Styles.AltTab11:
                    #region Alt+Tab 11
                    {
                        if (!DesignMode && Transparency && adaptedBackBlurred is not null)
                            G.DrawRoundImage(adaptedBackBlurred, RRect, Radius, true);

                        if (Transparency)
                        {
                            if (DarkMode)
                            {
                                using (var br = new SolidBrush(Color.FromArgb(100, 175, 175, 175)))
                                {
                                    G.FillRoundedRect(br, RRect, Radius, true);
                                }
                            }
                            else
                            {
                                using (var br = new SolidBrush(Color.FromArgb(120, 185, 185, 185)))
                                {
                                    G.FillRoundedRect(br, RRect, Radius, true);
                                }
                            }

                            G.FillRoundedRect(Noise, RRect, Radius, true);
                        }
                        else if (DarkMode)
                        {
                            using (var br = new SolidBrush(Color.FromArgb(32, 32, 32)))
                            {
                                G.FillRoundedRect(br, RRect, Radius, true);
                            }
                            using (var P = new Pen(Color.FromArgb(65, 65, 65)))
                            {
                                G.DrawRoundedRect(P, RRect, Radius, true);
                            }
                        }
                        else
                        {
                            using (var br = new SolidBrush(Color.FromArgb(243, 243, 243)))
                            {
                                G.FillRoundedRect(br, RRect, Radius, true);
                            }
                            using (var P = new Pen(Color.FromArgb(171, 171, 171)))
                            {
                                G.DrawRoundedRect(P, RRect, Radius, true);
                            }
                        }

                        int AppHeight = (int)(0.75 * RRect.Height);
                        int _padding = (RRect.Height - AppHeight) / 2;

                        int appsNumber = 3;
                        int AllAppsWidthWithPadding = RRect.Width - 2 * _padding;
                        int AppWidth = (AllAppsWidthWithPadding - (appsNumber - 1) * _padding) / appsNumber;

                        var Rects = new List<Rectangle>();
                        Rects.Clear();

                        for (int x = 0, loopTo = appsNumber - 1; x <= loopTo; x++)
                        {
                            if (x == 0)
                            {
                                Rects.Add(new Rectangle(RRect.X + _padding, RRect.Y + _padding, AppWidth, AppHeight));
                            }
                            else
                            {
                                Rects.Add(new Rectangle(Rects[x - 1].Right + _padding, RRect.Y + _padding, AppWidth, AppHeight));
                            }
                        }

                        for (int x = 0, loopTo1 = Rects.Count - 1; x <= loopTo1; x++)
                        {
                            var r = Rects[x];

                            Color back = DarkMode ? Color.FromArgb(23, 23, 23) : Color.FromArgb(233, 234, 234);
                            Color back2 = DarkMode ? Color.FromArgb(39, 39, 39) : Color.FromArgb(255, 255, 255);

                            if (x == 0)
                            {
                                var surround = new Rectangle(r.X - 5, r.Y - 5, r.Width + 10, r.Height + 10);
                                using (var P = new Pen(Color.FromArgb(75, 182, 237), 3))
                                {
                                    G.DrawRoundedRect(P, surround, Radius * 2 + 5 / 2, true);
                                }
                            }

                            using (var br = new SolidBrush(back))
                            {
                                G.FillRoundedRect(br, r, Radius * 2, true);
                            }
                            G.DrawImage(Properties.Resources.SampleApp_Active, new Rectangle(r.X + 5, r.Y + 5, 20, 20));

                            using (var br = new SolidBrush(Color.FromArgb(150, back2)))
                            {
                                G.FillRectangle(br, new Rectangle(r.X + 5 + 20 + 5, r.Y + 5 + (20 - 4) / 2, 20, 4));
                            }

                            using (var br = new SolidBrush(back2))
                            {
                                G.FillRoundedRect(br, new Rectangle(r.X + 1, r.Y + 5 + 20 + 5, r.Width - 2, r.Height - 5 - 20 - 5), Radius * 2, true);
                            }
                        }

                        break;
                    }
                #endregion

                case Styles.Start10:
                    #region Start 10
                    {
                        if (!UseWin11RoundedCorners_WithWin10_Level1 & !UseWin11RoundedCorners_WithWin10_Level2)
                        {
                            if (!DesignMode && Transparency && adaptedBackBlurred is not null)
                                G.DrawImage(adaptedBackBlurred, Rect);
                            if (Transparency)
                                G.FillRectangle(Noise, Rect);
                            using (var br = new SolidBrush(Color.FromArgb(Transparency ? BackColorAlpha : 255, Background)))
                            {
                                G.FillRectangle(br, Rect);
                            }
                            G.DrawImage(DarkMode ? Properties.Resources.Start10_Dark : Properties.Resources.Start10_Light, new Rectangle(0, 0, Width - 1, Height - 1));
                        }

                        else if (UseWin11RoundedCorners_WithWin10_Level1)
                        {
                            if (!DesignMode && Transparency && adaptedBackBlurred is not null)
                                G.DrawImage(adaptedBackBlurred, Rect);
                            if (Transparency)
                                G.FillRectangle(Noise, Rect);
                            using (var br = new SolidBrush(Color.FromArgb(Transparency ? BackColorAlpha : 255, Background)))
                            {
                                G.FillRectangle(br, Rect);
                            }
                            G.DrawImage(DarkMode ? Properties.Resources.Start11_EP_Rounded_Dark : Properties.Resources.Start11_EP_Rounded_Light, new Rectangle(0, 0, Width - 1, Height - 1));
                        }

                        else if (UseWin11RoundedCorners_WithWin10_Level2)
                        {
                            if (!DesignMode && Transparency && adaptedBackBlurred is not null)
                                G.DrawImage(adaptedBackBlurred, Rect);
                            if (Transparency)
                                G.FillRoundedRect(Noise, Rect, Radius, true);
                            using (var br = new SolidBrush(Color.FromArgb(Transparency ? BackColorAlpha : 255, Background)))
                            {
                                G.FillRoundedRect(br, Rect, Radius, true);
                            }
                            G.DrawRoundImage(DarkMode ? Properties.Resources.Start11_EP_Rounded_Dark : Properties.Resources.Start11_EP_Rounded_Light, new Rectangle(0, 0, Width - 1, Height - 1), Radius, true);

                        }

                        break;
                    }

                #endregion

                case Styles.ActionCenter10:
                    #region Action Center 10
                    {
                        if (!DesignMode && Transparency && adaptedBackBlurred is not null)
                            G.DrawImage(adaptedBackBlurred, Rect);

                        if (Transparency)
                            G.FillRectangle(Noise, Rect);
                        using (var br = new SolidBrush(Color.FromArgb(Transparency ? BackColorAlpha : 255, Background)))
                        {
                            G.FillRectangle(br, Rect);
                        }

                        var rect1 = new Rectangle(85, 6, 30, 3);
                        var rect2 = new Rectangle(5, 190, 30, 3);
                        var rect3 = new Rectangle(42, 201, 34, 24);

                        using (var br = new SolidBrush(ActionCenterButton_Normal))
                        {
                            G.FillRectangle(br, rect3);
                        }
                        G.DrawImage(DarkMode ? Properties.Resources.AC_10_Dark : Properties.Resources.AC_10_Light, new Rectangle(0, 0, Width - 1, Height - 1));
                        using (var br = new SolidBrush(LinkColor))
                        {
                            G.FillRectangle(br, rect1);
                        }
                        using (var br = new SolidBrush(LinkColor))
                        {
                            G.FillRectangle(br, rect2);
                        }
                        using (var P = new Pen(Color.FromArgb(150, 100, 100, 100)))
                        {
                            G.DrawLine(P, new Point(0, 0), new Point(0, Height - 1));
                        }

                        using (var P = new Pen(Color.FromArgb(150, 76, 76, 76)))
                        {
                            G.DrawRectangle(P, Rect);
                        }

                        break;
                    }
                #endregion

                case Styles.Taskbar10:
                    #region Taskbar 10
                    {
                        G.SmoothingMode = SmoothingMode.HighSpeed;
                        if (!DesignMode && Transparency && adaptedBackBlurred is not null)
                            G.DrawImage(adaptedBackBlurred, Rect);
                        using (var br = new SolidBrush(Color.FromArgb(Transparency ? BackColorAlpha : 255, Background)))
                        {
                            G.FillRectangle(br, Rect);
                        }

                        var StartBtnRect = new Rectangle(-1, -1, 42, Height + 2);
                        var StartBtnImgRect = new Rectangle();

                        if (!UseWin11ORB_WithWin10)
                        {
                            StartBtnImgRect = new Rectangle(StartBtnRect.X + (StartBtnRect.Width - Properties.Resources.StartBtn_10Dark.Width) / 2, StartBtnRect.Y + (StartBtnRect.Height - Properties.Resources.StartBtn_10Dark.Height) / 2, Properties.Resources.StartBtn_10Dark.Width, Properties.Resources.StartBtn_10Dark.Height);
                        }
                        else
                        {
                            StartBtnImgRect = new Rectangle(StartBtnRect.X + (StartBtnRect.Width - Properties.Resources.StartBtn_11_EP.Width) / 2, StartBtnRect.Y + (StartBtnRect.Height - Properties.Resources.StartBtn_11_EP.Height) / 2, Properties.Resources.StartBtn_11_EP.Width, Properties.Resources.StartBtn_11_EP.Height);
                        }

                        var AppBtnRect = new Rectangle(StartBtnRect.Right, -1, 40, Height + 2);
                        var AppBtnImgRect = new Rectangle(AppBtnRect.X + (AppBtnRect.Width - Properties.Resources.SampleApp_Active.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - Properties.Resources.SampleApp_Active.Height) / 2 - 1, Properties.Resources.SampleApp_Active.Width, Properties.Resources.SampleApp_Active.Height);
                        var AppBtnRectUnderline = new Rectangle(AppBtnRect.X, AppBtnRect.Y + AppBtnRect.Height - 3, AppBtnRect.Width, 2);
                        var App2BtnRect = new Rectangle(AppBtnRect.Right, -1, 40, Height + 2);
                        var App2BtnImgRect = new Rectangle(App2BtnRect.X + (App2BtnRect.Width - Properties.Resources.SampleApp_Inactive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - Properties.Resources.SampleApp_Inactive.Height) / 2, Properties.Resources.SampleApp_Inactive.Width, Properties.Resources.SampleApp_Inactive.Height);
                        var App2BtnRectUnderline = new Rectangle(App2BtnRect.X + 14 / 2, App2BtnRect.Y + App2BtnRect.Height - 3, App2BtnRect.Width - 14, 2);
                        Color StartColor = _StartColor;
                        using (var br = new SolidBrush(StartColor))
                        {
                            G.FillRectangle(br, StartBtnRect);
                        }

                        if (!UseWin11ORB_WithWin10)
                        {
                            G.DrawImage(DarkMode ? Properties.Resources.StartBtn_10Dark : Properties.Resources.StartBtn_10Light, StartBtnImgRect);
                        }
                        else
                        {
                            G.DrawImage(Properties.Resources.StartBtn_11_EP, StartBtnImgRect);
                        }

                        Color AppColor = _AppBackground;
                        using (var br = new SolidBrush(AppColor))
                        {
                            G.FillRectangle(br, AppBtnRect);
                        }
                        using (var br = new SolidBrush(_AppUnderline.Light()))
                        {
                            G.FillRectangle(br, AppBtnRectUnderline);
                        }
                        G.DrawImage(Properties.Resources.SampleApp_Active, AppBtnImgRect);

                        using (var br = new SolidBrush(_AppUnderline.Light()))
                        {
                            G.FillRectangle(br, App2BtnRectUnderline);
                        }
                        G.DrawImage(Properties.Resources.SampleApp_Inactive, App2BtnImgRect);
                        break;
                    }
                #endregion

                case Styles.AltTab10:
                    #region Alt+Tab 10
                    {
                        int a = Math.Max(Math.Min(255, BackColorAlpha / 100 * 255), 0);

                        using (var br = new SolidBrush(Color.FromArgb(a, 23, 23, 23)))
                        {
                            G.FillRectangle(br, RRect);
                        }

                        int AppHeight = (int)(0.75 * RRect.Height);
                        int _padding = (RRect.Height - AppHeight) / 2;

                        int appsNumber = 3;
                        int AllAppsWidthWithPadding = RRect.Width - 2 * _padding;
                        int AppWidth = (AllAppsWidthWithPadding - (appsNumber - 1) * _padding) / appsNumber;

                        var Rects = new List<Rectangle>();
                        Rects.Clear();

                        for (int x = 0, loopTo2 = appsNumber - 1; x <= loopTo2; x++)
                        {
                            if (x == 0)
                            {
                                Rects.Add(new Rectangle(RRect.X + _padding, RRect.Y + _padding, AppWidth, AppHeight));
                            }
                            else
                            {
                                Rects.Add(new Rectangle(Rects[x - 1].Right + _padding, RRect.Y + _padding, AppWidth, AppHeight));
                            }
                        }

                        for (int x = 0, loopTo3 = Rects.Count - 1; x <= loopTo3; x++)
                        {
                            var r = Rects[x];

                            Color back = DarkMode ? Color.FromArgb(60, 60, 60) : Color.FromArgb(255, 255, 255);

                            if (x == 0)
                            {
                                var surround = new Rectangle(r.X - 5, r.Y - 5, r.Width + 10, r.Height + 10);
                                using (var P = new Pen(Color.White, 2))
                                {
                                    G.DrawRectangle(P, surround);
                                }
                            }

                            G.DrawImage(Properties.Resources.SampleApp_Active, new Rectangle(r.X + 5, r.Y + 5, 20, 20));

                            G.FillRectangle(Brushes.White, new Rectangle(r.X + 5 + 20 + 5, r.Y + 5 + (20 - 4) / 2, 20, 4));

                            using (var br = new SolidBrush(back))
                            {
                                G.FillRectangle(br, new Rectangle(r.X + 1, r.Y + 5 + 20 + 5, r.Width - 2, r.Height - 5 - 20 - 5));
                            }
                        }

                        break;
                    }
                #endregion

                case Styles.Taskbar8Aero:
                    #region Taskbar 8 Aero
                    {
                        Color c = Color.FromArgb(Win7ColorBal / 100 * 255, Background);
                        Color bc = Color.FromArgb(217, 217, 217);

                        using (var P = new Pen(Color.FromArgb(80, 0, 0, 0)))
                        {
                            G.DrawLine(P, new Point(0, 0), new Point(Width - 1, 0));
                        }
                        using (var br = new SolidBrush(Color.FromArgb(BackColorAlpha, bc)))
                        {
                            G.FillRectangle(br, Rect);
                        }
                        using (var br = new SolidBrush(Color.FromArgb((int)(BackColorAlpha * ((decimal)Win7ColorBal / 100)), c)))
                        {
                            G.FillRectangle(br, Rect);
                        }

                        var StartORB = new Bitmap(Properties.Resources.Win8ORB);
                        var StartBtnRect = new Rectangle((35 - 27) / 2 + 2, (35 - 27) / 2 - 1, 27, 27);
                        var AppBtnRect = new Rectangle(StartBtnRect.Right + 8, 0, 45, Height - 1);
                        var AppBtnRectInner = new Rectangle(AppBtnRect.X + 1, AppBtnRect.Y + 1, AppBtnRect.Width - 2, AppBtnRect.Height - 2);

                        var AppBtnImgRect = new Rectangle(AppBtnRect.X + (AppBtnRect.Width - Properties.Resources.SampleApp_Active.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - Properties.Resources.SampleApp_Active.Height) / 2, Properties.Resources.SampleApp_Active.Width, Properties.Resources.SampleApp_Active.Height);
                        var App2BtnRect = new Rectangle(AppBtnRect.Right + 2, 0, 45, Height - 1);
                        var App2BtnRectInner = new Rectangle(App2BtnRect.X + 1, App2BtnRect.Y + 1, App2BtnRect.Width - 2, App2BtnRect.Height - 2);
                        var App2BtnImgRect = new Rectangle(App2BtnRect.X + (App2BtnRect.Width - Properties.Resources.SampleApp_Inactive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - Properties.Resources.SampleApp_Inactive.Height) / 2, Properties.Resources.SampleApp_Inactive.Width, Properties.Resources.SampleApp_Inactive.Height);

                        G.DrawImage(StartORB, StartBtnRect);

                        using (var br = new SolidBrush(Color.FromArgb(100, Color.White)))
                        {
                            G.FillRectangle(br, AppBtnRect);
                        }
                        using (var P = new Pen(Color.FromArgb(200, c.CB(-0.5d))))
                        {
                            G.DrawRectangle(P, AppBtnRect);
                        }
                        using (var P = new Pen(Color.FromArgb(215, Color.White)))
                        {
                            G.DrawRectangle(P, AppBtnRectInner);
                        }

                        G.DrawImage(Properties.Resources.SampleApp_Active, AppBtnImgRect);

                        using (var br = new SolidBrush(Color.FromArgb(50, Color.White)))
                        {
                            G.FillRectangle(br, App2BtnRect);
                        }
                        using (var P = new Pen(Color.FromArgb(100, c.CB(-0.5d))))
                        {
                            G.DrawRectangle(P, App2BtnRect);
                        }
                        using (var P = new Pen(Color.FromArgb(100, Color.White)))
                        {
                            G.DrawRectangle(P, App2BtnRectInner);
                        }

                        G.DrawImage(Properties.Resources.SampleApp_Inactive, App2BtnImgRect);
                        break;
                    }
                #endregion

                case Styles.Taskbar8Lite:
                    #region Taskbar 8 Lite
                    {
                        Color c = Color.FromArgb(Win7ColorBal / 100 * 255, Background);
                        Color bc = Color.FromArgb(217, 217, 217);

                        using (var P = new Pen(Color.FromArgb(89, 89, 89)))
                        {
                            G.DrawRectangle(P, new Rectangle(0, 0, Width - 1, Height - 1));
                        }

                        using (var br = new SolidBrush(Color.FromArgb(255, bc)))
                        {
                            G.FillRectangle(br, Rect);
                        }
                        using (var br = new SolidBrush(Color.FromArgb((int)(BackColorAlpha * ((decimal)Win7ColorBal / 100)), c)))
                        {
                            G.FillRectangle(br, Rect);
                        }

                        var StartORB = new Bitmap(Properties.Resources.Win8ORB);
                        var StartBtnRect = new Rectangle((35 - 27) / 2 + 2, (35 - 27) / 2 - 1, 27, 27);
                        var AppBtnRect = new Rectangle(StartBtnRect.Right + 8, 0, 45, Height - 1);
                        var AppBtnRectInner = new Rectangle(AppBtnRect.X + 1, AppBtnRect.Y + 1, AppBtnRect.Width - 2, AppBtnRect.Height - 2);

                        var AppBtnImgRect = new Rectangle(AppBtnRect.X + (AppBtnRect.Width - Properties.Resources.SampleApp_Active.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - Properties.Resources.SampleApp_Active.Height) / 2, Properties.Resources.SampleApp_Active.Width, Properties.Resources.SampleApp_Active.Height);
                        var App2BtnRect = new Rectangle(AppBtnRect.Right + 2, 0, 45, Height - 1);
                        var App2BtnRectInner = new Rectangle(App2BtnRect.X + 1, App2BtnRect.Y + 1, App2BtnRect.Width - 2, App2BtnRect.Height - 2);
                        var App2BtnImgRect = new Rectangle(App2BtnRect.X + (App2BtnRect.Width - Properties.Resources.SampleApp_Inactive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - Properties.Resources.SampleApp_Inactive.Height) / 2, Properties.Resources.SampleApp_Inactive.Width, Properties.Resources.SampleApp_Inactive.Height);

                        G.DrawImage(StartORB, StartBtnRect);

                        using (var br = new SolidBrush(Color.FromArgb(255, bc.CB(0.5d))))
                        {
                            G.FillRectangle(br, AppBtnRect);
                        }
                        using (var br = new SolidBrush(Color.FromArgb(255 * (Win7ColorBal / 100), c.CB(0.5d))))
                        {
                            G.FillRectangle(br, AppBtnRect);
                        }
                        using (var P = new Pen(Color.FromArgb(100, bc.CB(-0.5d))))
                        {
                            G.DrawRectangle(P, AppBtnRect);
                        }
                        using (var P = new Pen(Color.FromArgb(100 * (Win7ColorBal / 100), c.CB(-0.5d))))
                        {
                            G.DrawRectangle(P, AppBtnRect);
                        }

                        G.DrawImage(Properties.Resources.SampleApp_Active, AppBtnImgRect);

                        using (var br = new SolidBrush(Color.FromArgb(255, bc.Light(0.1f))))
                        {
                            G.FillRectangle(br, App2BtnRect);
                        }
                        using (var br = new SolidBrush(Color.FromArgb(255 * (Win7ColorBal / 100), c.Light(0.1f))))
                        {
                            G.FillRectangle(br, App2BtnRect);
                        }
                        using (var P = new Pen(Color.FromArgb(100, bc.Dark(0.1f))))
                        {
                            G.DrawRectangle(P, App2BtnRect);
                        }
                        using (var P = new Pen(Color.FromArgb(100 * (Win7ColorBal / 100), c.Dark(0.1f))))
                        {
                            G.DrawRectangle(P, App2BtnRect);
                        }
                        G.DrawImage(Properties.Resources.SampleApp_Inactive, App2BtnImgRect);
                        break;
                    }
                #endregion

                case Styles.AltTab8Aero:
                    #region Alt+Tab 8 Aero
                    {
                        using (var br = new SolidBrush(Background))
                        {
                            G.FillRectangle(br, RRect);
                        }

                        int AppHeight = (int)(0.75 * RRect.Height);
                        int _padding = (RRect.Height - AppHeight) / 2;

                        int appsNumber = 3;
                        int AllAppsWidthWithPadding = RRect.Width - 2 * _padding;
                        int AppWidth = (AllAppsWidthWithPadding - (appsNumber - 1) * _padding) / appsNumber;

                        var Rects = new List<Rectangle>();
                        Rects.Clear();

                        for (int x = 0, loopTo4 = appsNumber - 1; x <= loopTo4; x++)
                        {
                            if (x == 0)
                            {
                                Rects.Add(new Rectangle(RRect.X + _padding, RRect.Y + _padding + AppHeight * 2 / 5, AppWidth, AppHeight * 3 / 5));
                            }
                            else
                            {
                                Rects.Add(new Rectangle(Rects[x - 1].Right + _padding, RRect.Y + _padding + AppHeight * 2 / 5, AppWidth, AppHeight * 3 / 5));
                            }
                        }

                        for (int x = 0, loopTo5 = Rects.Count - 1; x <= loopTo5; x++)
                        {
                            var r = Rects[x];

                            if (x == 0)
                            {
                                var surround = new Rectangle(r.X - 10, r.Y - 10, r.Width + 20, r.Height + 20);
                                using (var P = new Pen(Color.White, 2))
                                {
                                    G.DrawRectangle(P, surround);
                                }
                            }

                            G.FillRectangle(Brushes.White, r);
                            int icon_w = Properties.Resources.SampleApp_Active.Width;
                            var icon_rect = new Rectangle(r.X + r.Width - (int)(0.7 * icon_w), r.Y + r.Height - (int)(0.6 * icon_w), icon_w, icon_w);
                            G.DrawImage(Properties.Resources.SampleApp_Active, icon_rect);
                        }

                        var TextRect = new Rectangle(RRect.X + _padding, RRect.Y, RRect.Width - 2 * _padding, AppHeight * 2 / 5);
                        G.DrawString("______", Font, Brushes.White, TextRect, ContentAlignment.MiddleCenter.ToStringFormat());
                        break;
                    }
                #endregion

                case Styles.AltTab8AeroLite:
                    #region Alt+Tab 8 Opaque
                    {
                        using (var br = new SolidBrush(Background))
                        {
                            G.FillRectangle(br, RRect);
                        }

                        using (var P = new Pen(LinkColor, 2))
                        {
                            G.DrawRectangle(P, RRect);
                        }

                        int AppHeight = (int)(0.75 * RRect.Height);
                        int _padding = (RRect.Height - AppHeight) / 2;

                        int appsNumber = 3;
                        int AllAppsWidthWithPadding = RRect.Width - 2 * _padding;
                        int AppWidth = (AllAppsWidthWithPadding - (appsNumber - 1) * _padding) / appsNumber;

                        var Rects = new List<Rectangle>();
                        Rects.Clear();

                        for (int x = 0, loopTo6 = appsNumber - 1; x <= loopTo6; x++)
                        {
                            if (x == 0)
                            {
                                Rects.Add(new Rectangle(RRect.X + _padding, RRect.Y + _padding + AppHeight * 2 / 5, AppWidth, AppHeight * 3 / 5));
                            }
                            else
                            {
                                Rects.Add(new Rectangle(Rects[x - 1].Right + _padding, RRect.Y + _padding + AppHeight * 2 / 5, AppWidth, AppHeight * 3 / 5));
                            }
                        }

                        for (int x = 0, loopTo7 = Rects.Count - 1; x <= loopTo7; x++)
                        {
                            var r = Rects[x];

                            if (x == 0)
                            {
                                var surround = new Rectangle(r.X - 10, r.Y - 10, r.Width + 20, r.Height + 20);
                                using (var P = new Pen(Background2, 2))
                                {
                                    G.DrawRectangle(P, surround);
                                }
                            }

                            G.FillRectangle(Brushes.White, r);
                            int icon_w = Properties.Resources.SampleApp_Active.Width;
                            var icon_rect = new Rectangle(r.X + r.Width - (int)(0.7 * icon_w), r.Y + r.Height - (int)(0.6 * icon_w), icon_w, icon_w);
                            G.DrawImage(Properties.Resources.SampleApp_Active, icon_rect);
                        }

                        var TextRect = new Rectangle(RRect.X + _padding, RRect.Y, RRect.Width - 2 * _padding, AppHeight * 2 / 5);
                        using (var br = new SolidBrush(ForeColor))
                        {
                            G.DrawString("______", Font, br, TextRect, ContentAlignment.MiddleCenter.ToStringFormat());
                        }

                        break;
                    }
                #endregion

                case Styles.Start7Aero:
                    #region Start 7 Aero
                    {
                        var RestRect = new Rectangle(0, 14, Width - 5, Height - 10);

                        if (!DesignMode && adaptedBackBlurred is not null)
                        {

                            // To dismiss upper part above start menu and make there is no blur bug
                            G.SetClip(RestRect);
                            G.DrawImage(adaptedBackBlurred, Rect);
                            G.ResetClip();

                            decimal alphaX = 1 - (decimal)BackColorAlpha / 100;  // ColorBlurBalance
                            if (alphaX < 0)
                                alphaX = 0;
                            if (alphaX > 1)
                                alphaX = 1;

                            decimal ColBal = (decimal)Win7ColorBal / 100;   // ColorBalance
                            decimal GlowBal = (decimal)Win7GlowBal / 100;   // AfterGlowBalance
                            Color Color1 = Background;
                            Color Color2 = Background2;

                            G.DrawAeroEffect(RestRect, default, Color1, ColBal, Color2, GlowBal, alphaX, 5, true);
                        }

                        G.DrawRoundImage(Noise7Start, Rect, 5, true);

                        G.DrawRoundImage(Properties.Resources.Start7, Rect, 5, true);
                        break;
                    }
                #endregion

                case Styles.Start7Opaque:
                    #region Start 7 Opaque
                    {
                        var RestRect = new Rectangle(0, 14, Width - 5, Height - 10);
                        using (var br = new SolidBrush(Color.White))
                        {
                            G.FillRoundedRect(br, RestRect, 5, true);
                        }
                        using (var br = new SolidBrush(Color.FromArgb((int)(255 * ((decimal)BackColorAlpha / 100)), Background)))
                        {
                            G.FillRoundedRect(br, RestRect, 5, true);
                        }
                        G.DrawRoundImage(Noise7Start, Rect, 5, true);
                        G.DrawRoundImage(Properties.Resources.Start7, Rect, 5, true);
                        break;
                    }
                #endregion

                case Styles.Start7Basic:
                    #region Start 7 Basic
                    {
                        G.DrawImage(Properties.Resources.Start7Basic, Rect);
                        break;
                    }
                #endregion

                case Styles.Taskbar7Aero:
                    #region Taskbar 7 Aero
                    {
                        if (!DesignMode && adaptedBackBlurred is not null)
                        {
                            G.DrawRoundImage(adaptedBackBlurred, RRect, Radius, true);

                            decimal alphaX = 1 - (decimal)BackColorAlpha / 100;  // ColorBlurBalance
                            if (alphaX < 0)
                                alphaX = 0;
                            if (alphaX > 1)
                                alphaX = 1;

                            decimal ColBal = (decimal)Win7ColorBal / 100;        // ColorBalance
                            decimal GlowBal = (decimal)Win7GlowBal / 100;        // AfterGlowBalance
                            Color Color1 = Background;
                            Color Color2 = Background2;

                            G.DrawAeroEffect(Rect, adaptedBackBlurred, Color1, ColBal, Color2, GlowBal, alphaX, 0, false);
                        }

                        G.DrawImage(Properties.Resources.Win7TaskbarSides, Rect);

                        G.DrawRoundImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), Rect, Radius, true);

                        using (var P = new Pen(Color.FromArgb(80, 0, 0, 0)))
                        {
                            G.DrawLine(P, new Point(0, 0), new Point(Width - 1, 0));
                        }
                        using (var P = new Pen(Color.FromArgb(80, 255, 255, 255)))
                        {
                            G.DrawLine(P, new Point(0, 1), new Point(Width - 1, 1));
                        }

                        G.DrawImage(Properties.Resources.AeroPeek, new Rectangle(Width - 10, 0, 10, Height));

                        var StartORB = new Bitmap(Properties.Resources.Win7ORB);

                        var StartBtnRect = new Rectangle(3, -3, 39, 39);

                        var AppBtnRect = new Rectangle(StartBtnRect.Right + 5, 0, 45, 35);
                        var AppBtnImgRect = new Rectangle(AppBtnRect.X + (AppBtnRect.Width - Properties.Resources.SampleApp_Active.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - Properties.Resources.SampleApp_Active.Height) / 2, Properties.Resources.SampleApp_Active.Width, Properties.Resources.SampleApp_Active.Height);

                        var App2BtnRect = new Rectangle(AppBtnRect.Right + 1, 0, 45, 35);
                        var App2BtnImgRect = new Rectangle(App2BtnRect.X + (App2BtnRect.Width - Properties.Resources.SampleApp_Inactive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - Properties.Resources.SampleApp_Inactive.Height) / 2, Properties.Resources.SampleApp_Inactive.Width, Properties.Resources.SampleApp_Inactive.Height);

                        G.DrawImage(StartORB, StartBtnRect);

                        using (var P = new Pen(Color.FromArgb(150, 0, 0, 0)))
                        {
                            G.DrawRoundedRect(P, new Rectangle(AppBtnRect.X, AppBtnRect.Y, AppBtnRect.Width - 2, AppBtnRect.Height - 2), 2, true);
                        }
                        G.DrawImage(Properties.Resources.Taskbar_ActiveApp7, AppBtnRect);
                        G.DrawImage(Properties.Resources.SampleApp_Active, AppBtnImgRect);

                        using (var P = new Pen(Color.FromArgb(110, 0, 0, 0)))
                        {
                            G.DrawRoundedRect(P, new Rectangle(App2BtnRect.X, App2BtnRect.Y, App2BtnRect.Width - 2, App2BtnRect.Height - 2), 2, true);
                        }
                        G.DrawImage(Properties.Resources.Taskbar_InactiveApp7, App2BtnRect);
                        G.DrawImage(Properties.Resources.SampleApp_Inactive, App2BtnImgRect);
                        break;
                    }
                #endregion

                case Styles.Taskbar7Opaque:
                    #region Taskbar 7 Opaque
                    {
                        using (var br = new SolidBrush(Color.White))
                        {
                            G.FillRectangle(br, Rect);
                        }
                        using (var br = new SolidBrush(Color.FromArgb((int)(255 * ((decimal)BackColorAlpha / 100)), Background)))
                        {
                            G.FillRectangle(br, Rect);
                        }
                        G.DrawImage(Properties.Resources.Win7TaskbarSides, Rect);

                        G.DrawRoundImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), Rect, Radius, true);

                        using (var P = new Pen(Color.FromArgb(80, 0, 0, 0)))
                        {
                            G.DrawLine(P, new Point(0, 0), new Point(Width - 1, 0));
                        }
                        using (var P = new Pen(Color.FromArgb(80, 255, 255, 255)))
                        {
                            G.DrawLine(P, new Point(0, 1), new Point(Width - 1, 1));
                        }

                        G.DrawImage(Properties.Resources.AeroPeek, new Rectangle(Width - 10, 0, 10, Height));

                        var StartORB = new Bitmap(Properties.Resources.Win7ORB);

                        var StartBtnRect = new Rectangle(3, -3, 39, 39);

                        var AppBtnRect = new Rectangle(StartBtnRect.Right + 5, 0, 45, 35);
                        var AppBtnImgRect = new Rectangle(AppBtnRect.X + (AppBtnRect.Width - Properties.Resources.SampleApp_Active.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - Properties.Resources.SampleApp_Active.Height) / 2, Properties.Resources.SampleApp_Active.Width, Properties.Resources.SampleApp_Active.Height);

                        var App2BtnRect = new Rectangle(AppBtnRect.Right + 1, 0, 45, 35);
                        var App2BtnImgRect = new Rectangle(App2BtnRect.X + (App2BtnRect.Width - Properties.Resources.SampleApp_Inactive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - Properties.Resources.SampleApp_Inactive.Height) / 2, Properties.Resources.SampleApp_Inactive.Width, Properties.Resources.SampleApp_Inactive.Height);

                        G.DrawImage(StartORB, StartBtnRect);

                        using (var P = new Pen(Color.FromArgb(150, 0, 0, 0)))
                        {
                            G.DrawRoundedRect(P, new Rectangle(AppBtnRect.X, AppBtnRect.Y, AppBtnRect.Width - 2, AppBtnRect.Height - 2), 2, true);
                        }
                        G.DrawImage(Properties.Resources.Taskbar_ActiveApp7, AppBtnRect);
                        G.DrawImage(Properties.Resources.SampleApp_Active, AppBtnImgRect);

                        using (var P = new Pen(Color.FromArgb(110, 0, 0, 0)))
                        {
                            G.DrawRoundedRect(P, new Rectangle(App2BtnRect.X, App2BtnRect.Y, App2BtnRect.Width - 2, App2BtnRect.Height - 2), 2, true);
                        }
                        G.DrawImage(Properties.Resources.Taskbar_InactiveApp7, App2BtnRect);
                        G.DrawImage(Properties.Resources.SampleApp_Inactive, App2BtnImgRect);
                        break;
                    }
                #endregion

                case Styles.Taskbar7Basic:
                    #region Taskbar 7 Basic
                    {
                        G.DrawImage(Properties.Resources.BasicTaskbar, Rect);

                        G.DrawImage(Properties.Resources.AeroPeek, new Rectangle(Width - 10, 0, 10, Height));

                        var StartORB = new Bitmap(Properties.Resources.Win7ORB);

                        var StartBtnRect = new Rectangle(3, -3, 39, 39);

                        var AppBtnRect = new Rectangle(StartBtnRect.Right + 5, 0, 45, 35);
                        var AppBtnImgRect = new Rectangle(AppBtnRect.X + (AppBtnRect.Width - Properties.Resources.SampleApp_Active.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - Properties.Resources.SampleApp_Active.Height) / 2, Properties.Resources.SampleApp_Active.Width, Properties.Resources.SampleApp_Active.Height);

                        var App2BtnRect = new Rectangle(AppBtnRect.Right + 1, 0, 45, 35);
                        var App2BtnImgRect = new Rectangle(App2BtnRect.X + (App2BtnRect.Width - Properties.Resources.SampleApp_Inactive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - Properties.Resources.SampleApp_Inactive.Height) / 2, Properties.Resources.SampleApp_Inactive.Width, Properties.Resources.SampleApp_Inactive.Height);

                        G.DrawImage(StartORB, StartBtnRect);

                        using (var P = new Pen(Color.FromArgb(150, 0, 0, 0)))
                        {
                            G.DrawRoundedRect(P, new Rectangle(AppBtnRect.X, AppBtnRect.Y, AppBtnRect.Width - 2, AppBtnRect.Height - 2), 2, true);
                        }
                        G.DrawImage(Properties.Resources.Taskbar_ActiveApp7, AppBtnRect);
                        G.DrawImage(Properties.Resources.SampleApp_Active, AppBtnImgRect);

                        using (var P = new Pen(Color.FromArgb(110, 0, 0, 0)))
                        {
                            G.DrawRoundedRect(P, new Rectangle(App2BtnRect.X, App2BtnRect.Y, App2BtnRect.Width - 2, App2BtnRect.Height - 2), 2, true);
                        }
                        G.DrawImage(Properties.Resources.Taskbar_InactiveApp7, App2BtnRect);
                        G.DrawImage(Properties.Resources.SampleApp_Inactive, App2BtnImgRect);
                        break;
                    }
                #endregion

                case Styles.AltTab7Aero:
                    #region Alt+Tab 7 Aero
                    {
                        if (Shadow & !DesignMode)
                            G.DrawGlow(RRect, Color.FromArgb(150, 0, 0, 0), 5, 15);

                        var inner = new Rectangle(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2);
                        Color Color1 = Background;
                        Color Color2 = Background2;

                        if (!DesignMode && adaptedBackBlurred is not null)
                        {
                            decimal alpha = 1 - (decimal)BackColorAlpha / 100;   // ColorBlurBalance
                            decimal ColBal = (decimal)Win7ColorBal / 100;       // ColorBalance
                            decimal GlowBal = (decimal)Win7GlowBal / 100;       // AfterGlowBalance
                            G.DrawAeroEffect(RRect, adaptedBackBlurred, Color1, ColBal, Color2, GlowBal, alpha, Radius, true);
                        }

                        G.DrawRoundImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), RRect, Radius, true);
                        using (var P = new Pen(Color.FromArgb(200, 25, 25, 25)))
                        {
                            G.DrawRoundedRect(P, RRect, Radius, true);
                        }
                        using (var P = new Pen(Color.FromArgb(70, 200, 200, 200)))
                        {
                            G.DrawRoundedRect(P, inner, Radius, true);
                        }


                        int AppHeight = (int)(0.75 * RRect.Height);
                        int _padding = (RRect.Height - AppHeight) / 2;

                        int appsNumber = 3;
                        int AllAppsWidthWithPadding = RRect.Width - 2 * _padding;
                        int AppWidth = (AllAppsWidthWithPadding - (appsNumber - 1) * _padding) / appsNumber;

                        var Rects = new List<Rectangle>();
                        Rects.Clear();

                        for (int x = 0, loopTo8 = appsNumber - 1; x <= loopTo8; x++)
                        {
                            if (x == 0)
                            {
                                Rects.Add(new Rectangle(RRect.X + _padding, RRect.Y + _padding + AppHeight * 2 / 5, AppWidth, AppHeight * 3 / 5));
                            }
                            else
                            {
                                Rects.Add(new Rectangle(Rects[x - 1].Right + _padding, RRect.Y + _padding + AppHeight * 2 / 5, AppWidth, AppHeight * 3 / 5));
                            }
                        }

                        for (int x = 0, loopTo9 = Rects.Count - 1; x <= loopTo9; x++)
                        {
                            var r = Rects[x];

                            if (x == 0)
                            {
                                var surround = new Rectangle(r.X - 10, r.Y - 10, r.Width + 20, r.Height + 20);
                                using (var br = new SolidBrush(Color.FromArgb(75, 200, 200, 200)))
                                {
                                    G.FillRoundedRect(br, surround, 1, true);
                                }
                                G.DrawRoundImage(Properties.Resources.Win7_TitleTopL.Fade(0.35d), surround, 2, true);
                                G.DrawRoundImage(Properties.Resources.Win7_TitleTopR.Fade(0.35d), surround, 2, true);

                                using (var P = new Pen(Color1))
                                {
                                    G.DrawRoundedRect(P, surround, 1, true);
                                }
                                using (var P = new Pen(Color.FromArgb(229, 240, 250)))
                                {
                                    G.DrawRectangle(P, new Rectangle(surround.X + 1, surround.Y + 1, surround.Width - 2, surround.Height - 2));
                                }

                            }

                            G.FillRoundedRect(Brushes.White, r, 2, true);
                            G.DrawRoundedRect(Pens.Black, r, 2, true);

                            int icon_w = Properties.Resources.SampleApp_Active.Width;

                            var icon_rect = new Rectangle(r.X + r.Width - (int)(0.7 * icon_w), r.Y + r.Height - (int)(0.6 * icon_w), icon_w, icon_w);

                            G.DrawImage(Properties.Resources.SampleApp_Active, icon_rect);
                        }

                        var TextRect = new Rectangle(RRect.X + _padding, RRect.Y, RRect.Width - 2 * _padding, AppHeight * 2 / 5);
                        G.DrawGlowString(2, "______", Font, Color.Black, Color.FromArgb(185, 225, 225, 225), RRect, TextRect, ContentAlignment.MiddleCenter.ToStringFormat());
                        break;
                    }

                #endregion

                case Styles.AltTab7Opaque:
                    #region Alt+Tab 7 Opaque
                    {
                        if (Shadow & !DesignMode)
                        {
                            G.DrawGlow(RRect, Color.FromArgb(150, 0, 0, 0), 5, 15);
                        }
                        var inner = new Rectangle(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2);

                        using (var br = new SolidBrush(Color.White))
                        {
                            G.FillRoundedRect(br, RRect, Radius, true);
                        }
                        using (var br = new SolidBrush(Color.FromArgb((int)(255 * ((decimal)Win7ColorBal / 100)), Background)))
                        {
                            G.FillRoundedRect(br, RRect, Radius, true);
                        }

                        G.DrawRoundImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), RRect, Radius, true);
                        using (var P = new Pen(Color.FromArgb(200, 25, 25, 25)))
                        {
                            G.DrawRoundedRect(P, RRect, Radius, true);
                        }
                        using (var P = new Pen(Color.FromArgb(70, 200, 200, 200)))
                        {
                            G.DrawRoundedRect(P, inner, Radius, true);
                        }

                        int AppHeight = (int)(0.75 * RRect.Height);
                        int _padding = (RRect.Height - AppHeight) / 2;

                        int appsNumber = 3;
                        int AllAppsWidthWithPadding = RRect.Width - 2 * _padding;
                        int AppWidth = (AllAppsWidthWithPadding - (appsNumber - 1) * _padding) / appsNumber;

                        var Rects = new List<Rectangle>();
                        Rects.Clear();

                        for (int x = 0, loopTo10 = appsNumber - 1; x <= loopTo10; x++)
                        {
                            if (x == 0)
                            {
                                Rects.Add(new Rectangle(RRect.X + _padding, RRect.Y + _padding + AppHeight * 2 / 5, AppWidth, AppHeight * 3 / 5));
                            }
                            else
                            {
                                Rects.Add(new Rectangle(Rects[x - 1].Right + _padding, RRect.Y + _padding + AppHeight * 2 / 5, AppWidth, AppHeight * 3 / 5));
                            }
                        }

                        for (int x = 0, loopTo11 = Rects.Count - 1; x <= loopTo11; x++)
                        {
                            var r = Rects[x];

                            if (x == 0)
                            {
                                var surround = new Rectangle(r.X - 10, r.Y - 10, r.Width + 20, r.Height + 20);
                                using (var br = new SolidBrush(Color.FromArgb(75, 200, 200, 200)))
                                {
                                    G.FillRoundedRect(br, surround, 1, true);
                                }
                                G.DrawRoundImage(Properties.Resources.Win7_TitleTopL.Fade(0.35d), surround, 2, true);
                                G.DrawRoundImage(Properties.Resources.Win7_TitleTopR.Fade(0.35d), surround, 2, true);

                                using (var P = new Pen(Background))
                                {
                                    G.DrawRoundedRect(P, surround, 1, true);
                                }
                                using (var P = new Pen(Color.FromArgb(229, 240, 250)))
                                {
                                    G.DrawRectangle(P, new Rectangle(surround.X + 1, surround.Y + 1, surround.Width - 2, surround.Height - 2));
                                }

                            }

                            G.FillRoundedRect(Brushes.White, r, 2, true);
                            G.DrawRoundedRect(Pens.Black, r, 2, true);

                            int icon_w = Properties.Resources.SampleApp_Active.Width;

                            var icon_rect = new Rectangle(r.X + r.Width - (int)(0.7 * icon_w), r.Y + r.Height - (int)(0.6 * icon_w), icon_w, icon_w);

                            G.DrawImage(Properties.Resources.SampleApp_Active, icon_rect);
                        }

                        var TextRect = new Rectangle(RRect.X + _padding, RRect.Y, RRect.Width - 2 * _padding, AppHeight * 2 / 5);
                        G.DrawGlowString(2, "______", Font, Color.Black, Color.FromArgb(185, 225, 225, 225), RRect, TextRect, ContentAlignment.MiddleCenter.ToStringFormat());
                        break;
                    }

                #endregion

                case Styles.AltTab7Basic:
                    #region Alt+Tab 7 Basic
                    {
                        Color Titlebar_Background1 = Color.FromArgb(152, 180, 208);
                        Color Titlebar_BackColor2 = Color.FromArgb(186, 210, 234);
                        Color Titlebar_OuterBorder = Color.FromArgb(52, 52, 52);
                        Color Titlebar_InnerBorder = Color.FromArgb(255, 255, 255);
                        Color Titlebar_Turquoise = Color.FromArgb(40, 207, 228);
                        Color OuterBorder = Color.FromArgb(0, 0, 0);
                        var UpperPart = new Rectangle(RRect.X, RRect.Y, RRect.Width + 1, 25);
                        G.SetClip(UpperPart);
                        var pth_back = new LinearGradientBrush(UpperPart, Titlebar_Background1, Titlebar_BackColor2, LinearGradientMode.Vertical);
                        var pth_line = new LinearGradientBrush(UpperPart, Titlebar_InnerBorder, Titlebar_Turquoise, LinearGradientMode.Vertical);
                        // ### Render Titlebar
                        G.FillRectangle(pth_back, RRect);
                        using (var P = new Pen(Titlebar_OuterBorder))
                        {
                            G.DrawRectangle(P, RRect);
                        }
                        using (var P = new Pen(Titlebar_InnerBorder))
                        {
                            G.DrawRectangle(P, new Rectangle(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2));
                        }
                        G.SetClip(new Rectangle(UpperPart.X + (int)(UpperPart.Width * 0.75), UpperPart.Y, (int)(UpperPart.Width * 0.75), UpperPart.Height));
                        using (var P = new Pen(pth_line))
                        {
                            G.DrawRectangle(P, new Rectangle(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2));
                        }
                        G.ResetClip();
                        G.ExcludeClip(UpperPart);
                        // ### Render Rest of WindowR
                        using (var br = new SolidBrush(Titlebar_BackColor2))
                        {
                            G.FillRectangle(br, RRect);
                        }
                        using (var P = new Pen(Titlebar_Turquoise))
                        {
                            G.DrawRectangle(P, new Rectangle(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2));
                        }
                        using (var P = new Pen(OuterBorder))
                        {
                            G.DrawRectangle(P, RRect);
                        }
                        G.ResetClip();
                        using (var P = new Pen(Color.FromArgb(52, 52, 52)))
                        {
                            G.DrawRectangle(P, RRect);
                        }
                        using (var P = new Pen(Color.FromArgb(255, 225, 225, 225)))
                        {
                            G.DrawRectangle(P, new Rectangle(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2));
                        }


                        int AppHeight = Properties.Resources.Win7AltTabBasicButton.Height;
                        int _padding = 5;

                        int appsNumber = 3;
                        int AppWidth = Properties.Resources.Win7AltTabBasicButton.Width;

                        int _paddingOuter = (RRect.Width - AppWidth * appsNumber - _padding * (appsNumber - 1)) / 2;

                        var Rects = new List<Rectangle>();
                        Rects.Clear();

                        for (int x = 0, loopTo12 = appsNumber - 1; x <= loopTo12; x++)
                        {
                            if (x == 0)
                            {
                                Rects.Add(new Rectangle(RRect.X + _paddingOuter, RRect.Y + RRect.Height - 5 - AppHeight, AppWidth, AppHeight));
                            }
                            else
                            {
                                Rects.Add(new Rectangle(Rects[x - 1].Right + _padding, RRect.Y + RRect.Height - 5 - AppHeight, AppWidth, AppHeight));
                            }
                        }

                        for (int x = 0, loopTo13 = Rects.Count - 1; x <= loopTo13; x++)
                        {
                            var r = Rects[x];
                            if (x == 0)
                                G.DrawImage(Properties.Resources.Win7AltTabBasicButton, r);

                            var imgrect = new Rectangle(r.X + (r.Width - Properties.Resources.SampleApp_Active.Width) / 2, r.Y + (r.Height - Properties.Resources.SampleApp_Active.Height) / 2, Properties.Resources.SampleApp_Active.Width, Properties.Resources.SampleApp_Active.Height);

                            G.DrawImage(Properties.Resources.SampleApp_Active, imgrect);
                        }

                        var TextRect = new Rectangle(RRect.X + _padding, RRect.Y, RRect.Width - 2 * _padding, 30);
                        G.DrawString("______", Font, Brushes.Black, TextRect, ContentAlignment.MiddleCenter.ToStringFormat());
                        break;
                    }
                #endregion

                case Styles.StartVistaAero:
                    #region Start Vista Aero
                    {
                        var RestRect = new Rectangle(0, 14, Width - 6, Height - 14);

                        // To dismiss upper part above start menu and make there is no blur bug
                        G.SetClip(RestRect);
                        if (!DesignMode && adaptedBackBlurred is not null)
                            G.DrawImage(adaptedBackBlurred, Rect);
                        G.ResetClip();

                        using (var br = new SolidBrush(Color.FromArgb(BackColorAlpha, Background)))
                        {
                            G.FillRoundedRect(br, RestRect, 4, true);
                        }
                        G.DrawImage(Properties.Resources.Vista_StartAero, new Rectangle(0, 0, Width, Height));
                        break;
                    }
                #endregion

                case Styles.StartVistaOpaque:
                    #region Start Vista Opaque
                    {
                        var RestRect = new Rectangle(0, 14, Width - 6, Height - 14);
                        G.FillRoundedRect(Brushes.White, RestRect, 4, true);
                        using (var br = new SolidBrush(Color.FromArgb(BackColorAlpha, Background)))
                        {
                            G.FillRoundedRect(br, RestRect, 4, true);
                        }
                        G.DrawImage(Properties.Resources.Vista_StartAero, new Rectangle(0, 0, Width, Height));
                        break;
                    }
                #endregion

                case Styles.StartVistaBasic:
                    #region Start Vista Basic
                    {
                        G.DrawImage(Properties.Resources.Vista_StartBasic, new Rectangle(0, 0, Width, Height));
                        break;
                    }
                #endregion

                case Styles.TaskbarVistaAero:
                    #region Taskbar Vista Aero
                    {
                        if (!DesignMode && adaptedBackBlurred is not null)
                            G.DrawImage(adaptedBackBlurred, Rect);
                        using (var br = new SolidBrush(Color.FromArgb(BackColorAlpha, Background)))
                        {
                            G.FillRectangle(br, Rect);
                        }
                        G.FillRectangle(new TextureBrush(Properties.Resources.Vista_Taskbar), Rect);
                        Bitmap orb = Properties.Resources.Vista_StartLowerORB;
                        G.DrawImage(orb, new Rectangle(0, 0, orb.Width, Height));

                        var apprect1 = new Rectangle(Rect.X + 60, 1, 140, Rect.Height - 4);
                        var apprect2 = new Rectangle(apprect1.Right + 2, 1, 140, Rect.Height - 4);
                        var appIcon1 = new Rectangle(apprect1.X + 4, apprect1.Y + (apprect1.Height - 20) / 2, 20, 20);
                        var appIcon2 = new Rectangle(apprect2.X + 4, apprect2.Y + (apprect2.Height - 20) / 2, 20, 20);
                        var appLabel1 = new Rectangle(apprect1.X + 25, apprect1.Y, apprect1.Width - 30, apprect1.Height);
                        var appLabel2 = new Rectangle(apprect2.X + 25, apprect2.Y, apprect2.Width - 30, apprect2.Height);

                        G.DrawImage(Properties.Resources.Vista_ActiveApp, apprect1);
                        G.DrawImage(Properties.Resources.Vista_InactiveApp, apprect2);

                        G.DrawImage(Properties.Resources.SampleApp_Active, appIcon1);
                        G.DrawImage(Properties.Resources.SampleApp_Inactive, appIcon2);

                        G.DrawString(Program.Lang.AppPreview, Font, Brushes.White, appLabel1, ContentAlignment.MiddleLeft.ToStringFormat());
                        G.DrawString(Program.Lang.InactiveApp, Font, Brushes.White, appLabel2, ContentAlignment.MiddleLeft.ToStringFormat());
                        break;
                    }
                #endregion

                case Styles.TaskbarVistaOpaque:
                    #region Taskbar Vista Opaque
                    {
                        Bitmap orb = Properties.Resources.Vista_StartLowerORB;
                        G.FillRectangle(Brushes.White, Rect);
                        using (var br = new SolidBrush(Color.FromArgb(BackColorAlpha, Background)))
                        {
                            G.FillRectangle(br, Rect);
                        }
                        G.FillRectangle(new TextureBrush(Properties.Resources.Vista_Taskbar), Rect);
                        G.DrawImage(orb, new Rectangle(0, 0, orb.Width, Height));

                        var apprect1 = new Rectangle(Rect.X + 60, 1, 140, Rect.Height - 4);
                        var apprect2 = new Rectangle(apprect1.Right + 2, 1, 140, Rect.Height - 4);
                        var appIcon1 = new Rectangle(apprect1.X + 4, apprect1.Y + (apprect1.Height - 20) / 2, 20, 20);
                        var appIcon2 = new Rectangle(apprect2.X + 4, apprect2.Y + (apprect2.Height - 20) / 2, 20, 20);
                        var appLabel1 = new Rectangle(apprect1.X + 25, apprect1.Y, apprect1.Width - 30, apprect1.Height);
                        var appLabel2 = new Rectangle(apprect2.X + 25, apprect2.Y, apprect2.Width - 30, apprect2.Height);

                        G.DrawImage(Properties.Resources.Vista_ActiveApp, apprect1);
                        G.DrawImage(Properties.Resources.Vista_InactiveApp, apprect2);

                        G.DrawImage(Properties.Resources.SampleApp_Active, appIcon1);
                        G.DrawImage(Properties.Resources.SampleApp_Inactive, appIcon2);

                        G.DrawString(Program.Lang.AppPreview, Font, Brushes.White, appLabel1, ContentAlignment.MiddleLeft.ToStringFormat());
                        G.DrawString(Program.Lang.InactiveApp, Font, Brushes.White, appLabel2, ContentAlignment.MiddleLeft.ToStringFormat());
                        break;
                    }
                #endregion

                case Styles.TaskbarVistaBasic:
                    #region Taskbar Vista Basic
                    {
                        Bitmap orb = Properties.Resources.Vista_StartLowerORB;
                        G.FillRectangle(Brushes.Black, Rect);
                        G.FillRectangle(new TextureBrush(Properties.Resources.Vista_Taskbar), Rect);
                        G.DrawImage(orb, new Rectangle(0, 0, orb.Width, Height));

                        var apprect1 = new Rectangle(Rect.X + 60, 1, 140, Rect.Height - 4);
                        var apprect2 = new Rectangle(apprect1.Right + 2, 1, 140, Rect.Height - 4);
                        var appIcon1 = new Rectangle(apprect1.X + 4, apprect1.Y + (apprect1.Height - 20) / 2, 20, 20);
                        var appIcon2 = new Rectangle(apprect2.X + 4, apprect2.Y + (apprect2.Height - 20) / 2, 20, 20);
                        var appLabel1 = new Rectangle(apprect1.X + 25, apprect1.Y, apprect1.Width - 30, apprect1.Height);
                        var appLabel2 = new Rectangle(apprect2.X + 25, apprect2.Y, apprect2.Width - 30, apprect2.Height);

                        G.DrawImage(Properties.Resources.Vista_ActiveApp, apprect1);
                        G.DrawImage(Properties.Resources.Vista_InactiveApp, apprect2);

                        G.DrawImage(Properties.Resources.SampleApp_Active, appIcon1);
                        G.DrawImage(Properties.Resources.SampleApp_Inactive, appIcon2);

                        G.DrawString(Program.Lang.AppPreview, Font, Brushes.White, appLabel1, ContentAlignment.MiddleLeft.ToStringFormat());
                        G.DrawString(Program.Lang.InactiveApp, Font, Brushes.White, appLabel2, ContentAlignment.MiddleLeft.ToStringFormat());
                        break;
                    }
                #endregion

                case Styles.TaskbarXP:
                    #region Taskbar XP
                    {
                        try
                        {
                            SmoothingMode sm = G.SmoothingMode;
                            G.SmoothingMode = SmoothingMode.HighSpeed;
                            Program.resVS.Draw(G, Rect, VisualStylesRes.Element.Taskbar, true, false);
                            G.SmoothingMode = sm;
                        }
                        catch
                        {
                        }
                        break;
                    }
                    #endregion
            }
        }
    }
}