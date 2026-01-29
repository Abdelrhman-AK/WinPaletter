using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.Assets;
using WinPaletter.Properties;
using WinPaletter.Templates;
using WinPaletter.UI.Retro;

namespace WinPaletter.UI.Simulation
{
    [Description("Simulated Windows elements")]
    public partial class WinElement : Control
    {
        public WinElement()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            DoubleBuffered = true;

            SetStyles();
        }

        #region Variables

        private static TextureBrush Noise = new(Resources.Noise.Fade(0.15f));
        private static Bitmap Noise7 = Win7Preview.AeroGlass;
        private static Bitmap Noise7Start = Win7Preview.StartGlass;

        private Bitmap back;
        private Bitmap back_blurred;

        private Rectangle Button1;
        private Rectangle Button2;
        private MouseState _State_Btn1, _State_Btn2;

        public VisualStylesRes resVS;
        private bool _w1125H2 = OS.W11_25H2;

        public enum MouseState
        {
            Normal,
            Hover,
            Pressed
        }
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
            Taskbar81Aero,
            Taskbar81Lite,
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

        Rectangle rect;

        #endregion

        #region Properties

        private Styles _Style = Styles.Start11;
        public Styles Style
        {
            get => _Style;
            set
            {
                if (value != _Style)
                {
                    _Style = value;
                    ProcessBack();
                    Invalidate();
                }
            }
        }


        private int _BackColorAlpha = 130;
        public int BackColorAlpha
        {
            get => _BackColorAlpha;
            set
            {
                if (value != _BackColorAlpha)
                {
                    _BackColorAlpha = value;
                    Invalidate();
                }
            }
        }


        private float _NoisePower = 0f;
        public float NoisePower
        {
            get => _NoisePower;
            set
            {
                if (value != _NoisePower)
                {
                    _NoisePower = value;
                    NoiseBack();
                    Invalidate();
                }
            }
        }


        private int _BlurPower = 8;
        public int BlurPower
        {
            get => _BlurPower;
            set
            {
                if (value != _BlurPower)
                {
                    _BlurPower = value;

                    BlurBack();
                    Invalidate();
                }
            }
        }


        private bool _Transparency = true;
        public bool Transparency
        {
            get => _Transparency;
            set
            {
                if (value != _Transparency)
                {
                    _Transparency = value;
                    ProcessBack();
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


        private Color _AppUnderline;
        public Color AppUnderline
        {
            get => _AppUnderline;
            set
            {
                if (value != _AppUnderline)
                {
                    _AppUnderline = value;

                    Invalidate();
                }
            }
        }


        private Color _AppBackground;
        public Color AppBackground
        {
            get => _AppBackground;
            set
            {
                if (value != _AppBackground)
                {
                    _AppBackground = value;

                    Invalidate();
                }
            }
        }


        private Color _ActionCenterButton_Normal;
        public Color ActionCenterButton_Normal
        {
            get => _ActionCenterButton_Normal;
            set
            {
                if (value != _ActionCenterButton_Normal)
                {
                    _ActionCenterButton_Normal = value;

                    Invalidate();
                }
            }
        }


        private Color _ActionCenterButton_Hover;
        public Color ActionCenterButton_Hover
        {
            get => _ActionCenterButton_Hover;
            set
            {
                if (value != _ActionCenterButton_Hover)
                {
                    _ActionCenterButton_Hover = value;

                    Invalidate();
                }
            }
        }


        private Color _ActionCenterButton_Pressed;
        public Color ActionCenterButton_Pressed
        {
            get => _ActionCenterButton_Pressed;
            set
            {
                if (value != _ActionCenterButton_Pressed)
                {
                    _ActionCenterButton_Pressed = value;

                    Invalidate();
                }
            }
        }


        private Color _StartColor;
        public Color StartColor
        {
            get => _StartColor;
            set
            {
                if (value != _StartColor)
                {
                    _StartColor = value;
                    Invalidate();
                }
            }
        }


        private Color _LinkColor;
        public Color LinkColor
        {
            get => _LinkColor;
            set
            {
                if (value != _LinkColor)
                {
                    _LinkColor = value;

                    Invalidate();
                }
            }
        }


        private Color _Background;
        public Color Background
        {
            get => _Background;
            set
            {
                if (value != _Background)
                {
                    _Background = value;
                    Invalidate();
                }
            }
        }


        private Color _Background2;
        public Color Background2
        {
            get => _Background2;
            set
            {
                if (value != _Background2)
                {
                    _Background2 = value;

                    Invalidate();
                }
            }
        }


        private int _Win7ColorBal = 0;
        public int Win7ColorBal
        {
            get => _Win7ColorBal;
            set
            {
                if (value != _Win7ColorBal)
                {
                    _Win7ColorBal = value;

                    Invalidate();
                }
            }
        }


        private int _Win7GlowBal = 0;
        public int Win7GlowBal
        {
            get => _Win7GlowBal;
            set
            {
                if (value != _Win7GlowBal)
                {
                    _Win7GlowBal = value;

                    Invalidate();
                }
            }
        }

        public bool UseWin11ORB_WithWin10 { get; set; } = false;
        public bool UseWin11RoundedCorners_WithWin10_Level1 { get; set; } = false;
        public bool UseWin11RoundedCorners_WithWin10_Level2 { get; set; } = false;
        public bool Shadow { get; set; } = true;

        #endregion

        #region Methods

        public void SetStyles()
        {
            rect = new(0, 0, Width - 1, Height - 1);
        }

        public void ProcessBack()
        {
            GetBack();
            BlurBack();
            NoiseBack();
        }

        public void GetBack()
        {
            Bitmap Wallpaper = Parent?.BackgroundImage as Bitmap ?? Program.ThumbnailWallpaper;

            if (Wallpaper != null && Bounds.Width > 0 && Bounds.Height > 0)
            {
                Rectangle imageBounds = new(0, 0, Wallpaper.Width, Wallpaper.Height);
                back = imageBounds.Contains_ButNotExceed(Bounds) ? Wallpaper?.Clone(Bounds, Wallpaper.PixelFormat) : null;
            }
            else back = null;
        }

        public void BlurBack()
        {
            if (Style == Styles.Taskbar11 || Style == Styles.Start11 || Style == Styles.ActionCenter11 || Style == Styles.AltTab11)
            {
                if (Transparency && back != null)
                {
                    if (DarkMode)
                    {
                        using (Bitmap bmpBlurred = back?.Blur(BlurPower))
                        {
                            back_blurred = bmpBlurred?.AdjustHSL(null, 0.6f);
                        }
                    }
                    else
                    {
                        back_blurred = back?.Blur(BlurPower);
                    }
                }
                else
                {
                    back_blurred = null;
                }
            }
            else if ((Style == Styles.Taskbar10 || Style == Styles.Start10 || Style == Styles.ActionCenter10) && Transparency)
            {
                back_blurred = back?.Blur(BlurPower);
            }
            else if ((Style == Styles.Start7Aero || Style == Styles.Taskbar7Aero || Style == Styles.StartVistaAero || Style == Styles.TaskbarVistaAero || Style == Styles.AltTab7Aero) && back != null)
            {
                back_blurred = back?.Blur(3);
            }
        }

        public void NoiseBack()
        {
            if (Style == Styles.ActionCenter11 || Style == Styles.Start11 || Style == Styles.Taskbar11 || Style == Styles.AltTab11 ||
                Style == Styles.ActionCenter10 || Style == Styles.Start10 || Style == Styles.Taskbar10)
            {
                if (Transparency)
                {
                    using (Bitmap b = Resources.Noise.Fade(_NoisePower))
                    {
                        Noise = new(b);
                    }
                }
            }
            else if (Style == Styles.Start7Aero || Style == Styles.Taskbar7Aero || Style == Styles.AltTab7Aero ||
                     Style == Styles.Start7Opaque || Style == Styles.Taskbar7Opaque || Style == Styles.AltTab7Opaque)
            {
                using (Bitmap b0 = Win7Preview.AeroGlass.Fade(_NoisePower / 100f))
                using (Bitmap b1 = Win7Preview.StartGlass.Fade(_NoisePower / 100f))
                {
                    Noise7 = new(b0);
                    Noise7Start = new(b1);
                }
            }
        }

        #endregion

        #region Events/Overrides

        protected override async void OnMouseMove(MouseEventArgs e)
        {
            OnMouseMove_Down_Up(e);

            if (!DesignMode && EnableEditingColors)
            {
                CursorOverWindowAccent = rect.Contains(e.Location) &&
                                        (Style == Styles.Start8 ||
                                        Style == Styles.Taskbar81Aero ||
                                        Style == Styles.Taskbar81Lite ||
                                        Style == Styles.Start7Aero ||
                                        Style == Styles.Taskbar7Aero ||
                                        Style == Styles.Start7Opaque ||
                                        Style == Styles.Taskbar7Opaque ||
                                        Style == Styles.StartVistaAero ||
                                        Style == Styles.TaskbarVistaAero ||
                                        Style == Styles.StartVistaOpaque ||
                                        Style == Styles.TaskbarVistaOpaque);

                CursorOverStart = rect.Contains(e.Location) && (Style == Styles.Start11 || Style == Styles.Start10);

                CursorOverActionCenterLink = (actionCenterLink0.Contains(e.Location) || actionCenterLink1.Contains(e.Location)) && (Style == Styles.ActionCenter10);
                CursorOverActionCenterButton = actionCenterButton.Contains(e.Location) && (Style == Styles.ActionCenter10);
                CursorOverActionCenter = rect.Contains(e.Location) && !CursorOverActionCenterLink && !CursorOverActionCenterButton && (Style == Styles.ActionCenter11 || Style == Styles.ActionCenter10);

                CursorOverTaskbarAppUnderline = taskbarAppUnderline.Contains(e.Location) && (Style == Styles.Taskbar10 || Style == Styles.Taskbar11);
                CursorOverTaskbarApp = taskbarApp.Contains(e.Location) && !CursorOverTaskbarAppUnderline && (Style == Styles.Taskbar10);
                CursorOverStartButton = startBtnRect.Contains(e.Location) && (Style == Styles.Taskbar10);
                CursorOverTaskbar = !CursorOverTaskbarApp && !CursorOverTaskbarAppUnderline && !CursorOverStartButton && rect.Contains(e.Location) && (Style == Styles.Taskbar11 || Style == Styles.Taskbar10);

                await Task.Delay(10);
                Invalidate();
            }

            base.OnMouseMove(e);
        }

        protected override async void OnMouseLeave(EventArgs e)
        {
            if (Style == Styles.ActionCenter11)
            {
                _State_Btn1 = MouseState.Normal;
                _State_Btn2 = MouseState.Normal;

                await Task.Delay(10);
                Invalidate();
            }

            if (!DesignMode && EnableEditingColors)
            {
                CursorOverWindowAccent = false;
                CursorOverStart = false;
                CursorOverTaskbar = false;
                CursorOverActionCenter = false;
                CursorOverTaskbarApp = false;
                CursorOverTaskbarAppUnderline = false;
                CursorOverActionCenterLink = false;
                CursorOverActionCenterButton = false;
                CursorOverStartButton = false;

                await Task.Delay(10);
                Invalidate();
            }
            base.OnMouseLeave(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (!DesignMode && EnableEditingColors)
            {
                if (CursorOverWindowAccent)
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

                else if (CursorOverStart) EditorInvoker?.Invoke(this, new EditorEventArgs("Windows10x_StartColor"));

                else if (CursorOverTaskbar) EditorInvoker?.Invoke(this, new EditorEventArgs("Windows10x_TaskbarColor"));

                else if (CursorOverTaskbarAppUnderline) EditorInvoker?.Invoke(this, new EditorEventArgs("Windows10x_TaskbarAppUnderlineColor"));

                else if (CursorOverActionCenter) EditorInvoker?.Invoke(this, new EditorEventArgs("Windows10x_ActionCenterColor"));


                else if (CursorOverTaskbarApp) EditorInvoker?.Invoke(this, new EditorEventArgs("Windows10_TaskbarAppColor"));

                else if (CursorOverActionCenterLink) EditorInvoker?.Invoke(this, new EditorEventArgs("Windows10_ActionCenterLinkColor"));

                else if (CursorOverActionCenterButton) EditorInvoker?.Invoke(this, new EditorEventArgs("Windows10_ActionCenterButtonColor"));

                else if (CursorOverStartButton) EditorInvoker?.Invoke(this, new EditorEventArgs("Windows10_StartButtonColor"));
            }

            base.OnMouseClick(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            OnMouseMove_Down_Up(e);

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            OnMouseMove_Down_Up(e);

            base.OnMouseUp(e);
        }

        private async void OnMouseMove_Down_Up(MouseEventArgs e)
        {
            if (Style == Styles.ActionCenter11)
            {
                if (Button1.Contains(PointToClient(MousePosition)))
                {
                    if (e.Button == MouseButtons.None)
                        _State_Btn1 = MouseState.Hover;
                    else
                        _State_Btn1 = MouseState.Pressed;

                    await Task.Delay(10);
                    Invalidate();
                }
                else if (_State_Btn1 != MouseState.Normal)
                {
                    _State_Btn1 = MouseState.Normal;

                    await Task.Delay(10);
                    Invalidate();
                }

                if (Button2.Contains(PointToClient(MousePosition)))
                {
                    if (e.Button == MouseButtons.None)
                        _State_Btn2 = MouseState.Hover;
                    else
                        _State_Btn2 = MouseState.Pressed;

                    await Task.Delay(10);
                    Invalidate();
                }
                else if (!(_State_Btn2 == MouseState.Normal))
                {
                    _State_Btn2 = MouseState.Normal;

                    await Task.Delay(10);
                    Invalidate();
                }
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            if (!DesignMode) ProcessBack();
            if (!DesignMode && Parent is not null) Parent.BackgroundImageChanged += (sender, e) => ProcessBack();

            base.OnHandleCreated(e);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (!DesignMode && Parent is not null) Parent.BackgroundImageChanged -= (sender, e) => ProcessBack();

            base.OnHandleDestroyed(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            SetStyles();

            if (!DesignMode) ProcessBack();

            base.OnSizeChanged(e);
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            if (!DesignMode) ProcessBack();

            base.OnLocationChanged(e);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            back?.Dispose();
            back_blurred?.Dispose();
        }

        #endregion

        #region Editors

        bool CursorOverWindowAccent = false;

        bool CursorOverStart = false;
        bool CursorOverTaskbar = false;
        bool CursorOverActionCenter = false;

        bool CursorOverTaskbarApp = false;
        bool CursorOverTaskbarAppUnderline = false;

        bool CursorOverActionCenterLink = false;
        bool CursorOverActionCenterButton = false;
        bool CursorOverStartButton = false;

        Rectangle startBtnRect => new(-1, -1, 42, Height + 2);

        Rectangle taskbarApp => new(startBtnRect.Right, -1, 40, Height + 2);

        Rectangle taskbarAppUnderline
        {
            get
            {
                if (Style == Styles.Taskbar11)
                {
                    Rectangle StartBtnRect = new(8, 3, 36, 36);
                    Rectangle App2BtnRect = new(StartBtnRect.Right + 5, 3, 36, 36);
                    Rectangle AppBtnRect = new(App2BtnRect.Right + 5, 3, 36, 36);

                    return new(AppBtnRect.X + (AppBtnRect.Width - 18) / 2, AppBtnRect.Y + AppBtnRect.Height - 3, 18, 3);
                }
                else if (Style == Styles.Taskbar10)
                {
                    Rectangle AppBtnRect = taskbarApp;
                    return new(AppBtnRect.X, AppBtnRect.Y + AppBtnRect.Height - 3, AppBtnRect.Width, 2);
                }

                return new Rectangle();
            }
        }

        Rectangle actionCenterLink0 = new(85, 6, 30, 3);
        Rectangle actionCenterLink1 = new(5, 190, 30, 3);
        Rectangle actionCenterButton = new(42, 201, 34, 24);

        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);
        public event EditorInvokerEventHandler EditorInvoker;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingColors { get; set; } = false;

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
            Rectangle Rect = new(-1, -1, Width + 2, Height + 2);
            Rectangle RRect = new(0, 0, Width - 1, Height - 1);
            int Radius = 5;

            switch (Style)
            {
                case Styles.Start11:
                    #region Start 11
                    {
                        if (!DesignMode && Transparency && back_blurred is not null)
                            G.DrawRoundImage(back_blurred, RRect, Radius, true);

                        if (DarkMode)
                        {
                            using (SolidBrush br = new(Color.FromArgb(85, 28, 28, 28)))
                            {
                                G.FillRoundedRect(br, RRect, Radius, true);
                            }
                        }
                        else
                        {
                            using (SolidBrush br = new(Color.FromArgb(75, 255, 255, 255)))
                            {
                                G.FillRoundedRect(br, RRect, Radius, true);
                            }
                        }

                        using (SolidBrush br = new(Color.FromArgb(Transparency ? BackColorAlpha : 255, Background)))
                        {
                            G.FillRoundedRect(br, RRect, Radius, true);
                        }
                        if (Transparency && Noise != null)
                            G.FillRoundedRect(Noise, RRect, Radius, true);
                        Rectangle SearchRect = new(8, _w1125H2 ? 7 : 10, Width - 8 * 2, _w1125H2 ? 16 : 15);

                        #region Editor

                        if (!DesignMode && EnableEditingColors && CursorOverStart)
                        {
                            Color color0 = Color.FromArgb(80, 255, 255, 255);
                            Color color1 = Color.FromArgb(80, 0, 0, 0);
                            using (Pen P = new(color0))
                            using (HatchBrush hb = new(HatchStyle.Percent25, color0, color1))
                            {
                                G.FillRoundedRect(hb, RRect, Radius, true);
                                G.DrawRoundedRect(P, RRect, Radius, true);
                            }
                        }

                        #endregion

                        if (_w1125H2)
                        {
                            G.DrawRoundImage(DarkMode ? Win11Preview.Start1125H2_Dark : Win11Preview.Start1125H2_Light, RRect, Radius, true);
                        }
                        else
                        {
                            G.DrawRoundImage(DarkMode ? Win11Preview.Start11_Dark : Win11Preview.Start11_Light, RRect, Radius, true);
                        }

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

                        using (SolidBrush br = new(SearchColor))
                        {
                            G.FillRoundedRect(br, SearchRect, 8, true);
                        }
                        using (Pen P = new(SearchBorderColor))
                        {
                            G.DrawRoundedRect(P, SearchRect, 8, true);
                        }

                        using (Pen P = new(Color.FromArgb(150, 90, 90, 90)))
                        {
                            G.DrawRoundedRect(P, RRect, Radius, true);
                        }

                        break;
                    }
                #endregion

                case Styles.ActionCenter11:
                    #region Action Center 11
                    {
                        if (!DesignMode && Transparency && back_blurred is not null) G.DrawRoundImage(back_blurred, RRect, Radius, true);

                        if (DarkMode)
                        {
                            using (SolidBrush br = new(Color.FromArgb(85, 28, 28, 28)))
                            {
                                G.FillRoundedRect(br, RRect, Radius, true);
                            }
                        }
                        else
                        {
                            using (SolidBrush br = new(Color.FromArgb(75, 255, 255, 255)))
                            {
                                G.FillRoundedRect(br, RRect, Radius, true);
                            }
                        }

                        using (SolidBrush br = new(Color.FromArgb(Transparency ? BackColorAlpha : 255, Background)))
                        {
                            G.FillRoundedRect(br, RRect, Radius, true);
                        }

                        if (Transparency && Noise != null) G.FillRoundedRect(Noise, RRect, Radius, true);

                        Button1 = new(8, 8, 49, 20);
                        Button2 = new(62, 8, 49, 20);

                        #region Editor

                        if (!DesignMode && EnableEditingColors && CursorOverActionCenter)
                        {
                            Color color0 = Color.FromArgb(80, 255, 255, 255);
                            Color color1 = Color.FromArgb(80, 0, 0, 0);
                            using (Pen P = new(color0))
                            using (HatchBrush hb = new(HatchStyle.Percent25, color0, color1))
                            {
                                G.FillRoundedRect(hb, RRect, Radius, true);
                                G.DrawRoundedRect(P, RRect, Radius, true);
                            }
                        }

                        #endregion

                        G.DrawRoundImage(DarkMode ? Win11Preview.AC_11_Dark : Win11Preview.AC_11_Light, RRect, Radius, true);

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

                        using (SolidBrush br = new(Cx1))
                        {
                            G.FillRoundedRect(br, Button1, Radius, true);
                        }
                        using (Pen P = new(Cx1.Light(0.15f)))
                        {
                            G.DrawRoundedRectBeveled(P, Button1, Radius, true);
                        }
                        using (SolidBrush br = new(Cx2))
                        {
                            G.FillRoundedRect(br, Button2, Radius, true);
                        }
                        using (Pen P = new(Cx2.CB(DarkMode ? 0.05f : -0.05f)))
                        {
                            G.DrawRoundedRect(P, Button2, Radius, true);
                        }
                        using (Pen P = new(Color.FromArgb(150, 90, 90, 90)))
                        {
                            G.DrawRoundedRect(P, RRect, Radius, true);
                        }

                        break;
                    }
                #endregion

                case Styles.Taskbar11:
                    #region Taskbar 11
                    {
                        if (!DesignMode && Transparency && back_blurred is not null)
                            G.DrawImage(back_blurred, Rect);

                        if (DarkMode)
                        {
                            using (SolidBrush br = new(Color.FromArgb(110, 28, 28, 28)))
                            {
                                G.FillRectangle(br, Rect);
                            }
                        }
                        else
                        {
                            using (SolidBrush br = new(Color.FromArgb(90, 255, 255, 255)))
                            {
                                G.FillRectangle(br, Rect);
                            }
                        }

                        using (SolidBrush br = new(Color.FromArgb(Transparency ? BackColorAlpha : 255, Background)))
                        {
                            G.FillRectangle(br, Rect);
                        }

                        if (Transparency && Noise != null)
                            G.FillRoundedRect(Noise, RRect, Radius, true);

                        #region Editor

                        if (!DesignMode && EnableEditingColors && CursorOverTaskbar)
                        {
                            Color color0 = Color.FromArgb(80, 255, 255, 255);
                            Color color1 = Color.FromArgb(80, 0, 0, 0);
                            using (HatchBrush hb = new(HatchStyle.Percent25, color0, color1))
                            {
                                G.FillRectangle(hb, RRect);
                            }
                        }

                        #endregion

                        Rectangle StartBtnRect = new(8, 3, 36, 36);
                        Rectangle StartImgRect = new(8, 3, 37, 37);

                        Rectangle App2BtnRect = new(StartBtnRect.Right + 5, 3, 36, 36);
                        Rectangle App2ImgRect = new(StartBtnRect.Right + 5, 3, 37, 37);
                        Rectangle App2BtnRectUnderline = new(App2BtnRect.X + (App2BtnRect.Width - 8) / 2, App2BtnRect.Y + App2BtnRect.Height - 3, 8, 3);

                        Rectangle AppBtnRect = new(App2BtnRect.Right + 5, 3, 36, 36);
                        Rectangle AppImgRect = new(App2BtnRect.Right + 5, 3, 37, 37);
                        Rectangle AppBtnRectUnderline = new(AppBtnRect.X + (AppBtnRect.Width - 18) / 2, AppBtnRect.Y + AppBtnRect.Height - 3, 18, 3);

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

                        using (SolidBrush br = new(BackC))
                        {
                            G.FillRoundedRect(br, StartBtnRect, 3, true);
                        }
                        using (Pen P = new(BorderC))
                        {
                            G.DrawRoundedRectBeveled(P, StartBtnRect, 3);
                        }
                        G.DrawImage(DarkMode ? Win11Preview.StartBtn_11Dark : Win11Preview.StartBtn_11Light, StartImgRect);

                        using (SolidBrush br = new(BackC))
                        {
                            G.FillRoundedRect(br, AppBtnRect, 3, true);
                        }
                        using (Pen P = new(BorderC))
                        {
                            G.DrawRoundedRectBeveled(P, AppBtnRect, 3);
                        }
                        G.DrawImage(Resources.SampleApp_Active, AppImgRect);
                        using (SolidBrush br = new(_AppUnderline))
                        {
                            G.FillRoundedRect(br, AppBtnRectUnderline, 2, true);
                        }

                        #region Editor

                        if (!DesignMode && EnableEditingColors && CursorOverTaskbarAppUnderline)
                        {
                            Color color0 = Color.FromArgb(80, 255, 255, 255);
                            Color color1 = Color.FromArgb(80, 0, 0, 0);
                            using (HatchBrush hb = new(HatchStyle.Percent25, color0, color1))
                            {
                                G.FillRoundedRect(hb, AppBtnRectUnderline, 2, true);
                            }
                        }

                        #endregion

                        G.DrawImage(Resources.SampleApp_Inactive, App2ImgRect);
                        using (SolidBrush br = new(Color.FromArgb(255, BackC)))
                        {
                            G.FillRoundedRect(br, App2BtnRectUnderline, 2, true);
                        }

                        using (Pen P = new(Color.FromArgb(100, 100, 100, 100)))
                        {
                            G.DrawLine(P, new Point(0, 0), new Point(Width - 1, 0));
                        }

                        break;
                    }
                #endregion

                case Styles.AltTab11:
                    #region Alt+Tab 11
                    {
                        if (!DesignMode && Transparency && back_blurred is not null)
                            G.DrawRoundImage(back_blurred, RRect, Radius, true);

                        if (Transparency)
                        {
                            if (DarkMode)
                            {
                                using (SolidBrush br = new(Color.FromArgb(100, 175, 175, 175)))
                                {
                                    G.FillRoundedRect(br, RRect, Radius, true);
                                }
                            }
                            else
                            {
                                using (SolidBrush br = new(Color.FromArgb(120, 185, 185, 185)))
                                {
                                    G.FillRoundedRect(br, RRect, Radius, true);
                                }
                            }

                            if (Noise != null)
                                G.FillRoundedRect(Noise, RRect, Radius, true);
                        }
                        else if (DarkMode)
                        {
                            using (SolidBrush br = new(Color.FromArgb(32, 32, 32)))
                            {
                                G.FillRoundedRect(br, RRect, Radius, true);
                            }
                            using (Pen P = new(Color.FromArgb(65, 65, 65)))
                            {
                                G.DrawRoundedRect(P, RRect, Radius, true);
                            }
                        }
                        else
                        {
                            using (SolidBrush br = new(Color.FromArgb(243, 243, 243)))
                            {
                                G.FillRoundedRect(br, RRect, Radius, true);
                            }
                            using (Pen P = new(Color.FromArgb(171, 171, 171)))
                            {
                                G.DrawRoundedRect(P, RRect, Radius, true);
                            }
                        }

                        int AppHeight = (int)(0.75 * RRect.Height);
                        int _padding = (RRect.Height - AppHeight) / 2;

                        int appsNumber = 3;
                        int AllAppsWidthWithPadding = RRect.Width - 2 * _padding;
                        int AppWidth = (AllAppsWidthWithPadding - (appsNumber - 1) * _padding) / appsNumber;

                        List<Rectangle> Rects = [];
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
                            Rectangle r = Rects[x];

                            Color back = DarkMode ? Color.FromArgb(23, 23, 23) : Color.FromArgb(233, 234, 234);
                            Color back2 = DarkMode ? Color.FromArgb(39, 39, 39) : Color.FromArgb(255, 255, 255);

                            if (x == 0)
                            {
                                Rectangle surround = new(r.X - 5, r.Y - 5, r.Width + 10, r.Height + 10);
                                using (Pen P = new(Color.FromArgb(75, 182, 237), 3))
                                {
                                    G.DrawRoundedRect(P, surround, Radius * 2 + 5 / 2, true);
                                }
                            }

                            using (SolidBrush br = new(back))
                            {
                                G.FillRoundedRect(br, r, Radius * 2, true);
                            }
                            G.DrawImage(x == 0 ? Resources.SampleApp_Active : Resources.SampleApp_Inactive, new Rectangle(r.X + 5, r.Y + 5, 20, 20));

                            using (SolidBrush br = new(Color.FromArgb(150, back2)))
                            {
                                G.FillRectangle(br, new Rectangle(r.X + 5 + 20 + 5, r.Y + 5 + (20 - 4) / 2, 20, 4));
                            }

                            using (SolidBrush br = new(back2))
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
                            if (!DesignMode && Transparency && back_blurred is not null)
                                G.DrawImage(back_blurred, Rect);
                            if (Transparency && Noise != null)
                                G.FillRectangle(Noise, Rect);
                            using (SolidBrush br = new(Color.FromArgb(Transparency ? BackColorAlpha : 255, Background)))
                            {
                                G.FillRectangle(br, Rect);
                            }

                            #region Editor

                            if (!DesignMode && EnableEditingColors && CursorOverStart)
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

                            G.DrawImage(DarkMode ? Win10Preview.Start10_Dark : Win10Preview.Start10_Light, new Rectangle(0, 0, Width - 1, Height - 1));
                        }

                        else if (UseWin11RoundedCorners_WithWin10_Level1)
                        {
                            if (!DesignMode && Transparency && back_blurred is not null)
                                G.DrawImage(back_blurred, Rect);
                            if (Transparency && Noise != null)
                                G.FillRectangle(Noise, Rect);
                            using (SolidBrush br = new(Color.FromArgb(Transparency ? BackColorAlpha : 255, Background)))
                            {
                                G.FillRectangle(br, Rect);
                            }

                            #region Editor

                            if (!DesignMode && EnableEditingColors && CursorOverStart)
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

                            G.DrawImage(DarkMode ? Win11Preview.Start11_EP_Rounded_Dark : Win11Preview.Start11_EP_Rounded_Light, new Rectangle(0, 0, Width - 1, Height - 1));
                        }

                        else if (UseWin11RoundedCorners_WithWin10_Level2)
                        {
                            if (!DesignMode && Transparency && back_blurred is not null)
                                G.DrawImage(back_blurred, Rect);
                            if (Transparency && Noise != null)
                                G.FillRoundedRect(Noise, Rect, Radius, true);
                            using (SolidBrush br = new(Color.FromArgb(Transparency ? BackColorAlpha : 255, Background)))
                            {
                                G.FillRoundedRect(br, Rect, Radius, true);
                            }

                            #region Editor

                            if (!DesignMode && EnableEditingColors && CursorOverStart)
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

                            G.DrawRoundImage(DarkMode ? Win11Preview.Start11_EP_Rounded_Dark : Win11Preview.Start11_EP_Rounded_Light, new Rectangle(0, 0, Width - 1, Height - 1), Radius, true);
                        }

                        break;
                    }

                #endregion

                case Styles.ActionCenter10:
                    #region Action Center 10
                    {
                        if (!DesignMode && Transparency && back_blurred is not null)
                            G.DrawImage(back_blurred, Rect);

                        if (Transparency && Noise != null)
                            G.FillRectangle(Noise, Rect);
                        using (SolidBrush br = new(Color.FromArgb(Transparency ? BackColorAlpha : 255, Background)))
                        {
                            G.FillRectangle(br, Rect);
                        }

                        Rectangle rect1 = new(85, 6, 30, 3);
                        Rectangle rect2 = new(5, 190, 30, 3);
                        Rectangle rect3 = new(42, 201, 34, 24);

                        using (SolidBrush br = new(ActionCenterButton_Normal))
                        {
                            G.FillRectangle(br, rect3);
                        }

                        #region Editor

                        if (!DesignMode && EnableEditingColors && CursorOverActionCenter)
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

                        if (!DesignMode && EnableEditingColors && CursorOverActionCenterButton)
                        {
                            Color color0 = Color.FromArgb(80, 255, 255, 255);
                            Color color1 = Color.FromArgb(80, 0, 0, 0);
                            using (HatchBrush hb = new(HatchStyle.Percent25, color0, color1))
                            {
                                G.FillRectangle(hb, rect3);
                            }
                        }

                        #endregion

                        G.DrawImage(DarkMode ? Win10Preview.AC_10_Dark : Win10Preview.AC_10_Light, new Rectangle(0, 0, Width - 1, Height - 1));

                        using (SolidBrush br = new(LinkColor))
                        {
                            G.FillRectangle(br, rect1);
                        }
                        using (SolidBrush br = new(LinkColor))
                        {
                            G.FillRectangle(br, rect2);
                        }

                        #region Editor

                        if (!DesignMode && EnableEditingColors && CursorOverActionCenterLink)
                        {
                            Color color0 = Color.FromArgb(80, 255, 255, 255);
                            Color color1 = Color.FromArgb(80, 0, 0, 0);
                            using (HatchBrush hb = new(HatchStyle.Percent25, color0, color1))
                            {
                                G.FillRectangle(hb, rect1);
                                G.FillRectangle(hb, rect2);
                            }
                        }

                        #endregion

                        using (Pen P = new(Color.FromArgb(150, 100, 100, 100)))
                        {
                            G.DrawLine(P, new Point(0, 0), new Point(0, Height - 1));
                        }

                        using (Pen P = new(Color.FromArgb(150, 76, 76, 76)))
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
                        if (!DesignMode && Transparency && back_blurred is not null) G.DrawImage(back_blurred, Rect);

                        using (SolidBrush br = new(Color.FromArgb(Transparency ? BackColorAlpha : 255, Background))) { G.FillRectangle(br, Rect); }

                        #region Editor

                        if (!DesignMode && EnableEditingColors && CursorOverTaskbar)
                        {
                            G.ExcludeClip(startBtnRect);

                            Color color0 = Color.FromArgb(80, 255, 255, 255);
                            Color color1 = Color.FromArgb(80, 0, 0, 0);
                            using (HatchBrush hb = new(HatchStyle.Percent25, color0, color1))
                            {
                                G.FillRectangle(hb, Rect);
                            }

                            G.ResetClip();
                        }

                        #endregion

                        Rectangle StartBtnRect = new(-1, -1, 42, Height + 2);
                        Rectangle StartBtnImgRect;

                        if (!UseWin11ORB_WithWin10)
                        {
                            StartBtnImgRect = new(StartBtnRect.X + (StartBtnRect.Width - Win10Preview.StartBtn_10Light.Width) / 2, StartBtnRect.Y + (StartBtnRect.Height - Win10Preview.StartBtn_10Light.Height) / 2, Win10Preview.StartBtn_10Light.Width, Win10Preview.StartBtn_10Light.Height);
                        }
                        else
                        {
                            StartBtnImgRect = new(StartBtnRect.X + (StartBtnRect.Width - Win11Preview.StartBtn_11_EP.Width) / 2, StartBtnRect.Y + (StartBtnRect.Height - Win11Preview.StartBtn_11_EP.Height) / 2, Win11Preview.StartBtn_11_EP.Width, Win11Preview.StartBtn_11_EP.Height);
                        }

                        Rectangle AppBtnRect = new(StartBtnRect.Right, -1, 40, Height + 2);
                        Rectangle AppBtnImgRect = new(AppBtnRect.X + (AppBtnRect.Width - Resources.SampleApp_Active.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - Resources.SampleApp_Active.Height) / 2 - 1, Resources.SampleApp_Active.Width, Resources.SampleApp_Active.Height);
                        Rectangle AppBtnRectUnderline = new(AppBtnRect.X, AppBtnRect.Y + AppBtnRect.Height - 3, AppBtnRect.Width, 2);
                        Rectangle App2BtnRect = new(AppBtnRect.Right, -1, 40, Height + 2);
                        Rectangle App2BtnImgRect = new(App2BtnRect.X + (App2BtnRect.Width - Resources.SampleApp_Inactive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - Resources.SampleApp_Inactive.Height) / 2, Resources.SampleApp_Inactive.Width, Resources.SampleApp_Inactive.Height);
                        Rectangle App2BtnRectUnderline = new(App2BtnRect.X + 14 / 2, App2BtnRect.Y + App2BtnRect.Height - 3, App2BtnRect.Width - 14, 2);

                        #region Editor

                        if (!DesignMode && EnableEditingColors && CursorOverStartButton)
                        {
                            Color color0 = Color.FromArgb(80, 255, 255, 255);
                            Color color1 = Color.FromArgb(80, 0, 0, 0);
                            using (HatchBrush hb = new(HatchStyle.Percent25, color0, color1))
                            {
                                G.FillRectangle(hb, startBtnRect);
                            }
                        }

                        #endregion

                        if (!UseWin11ORB_WithWin10)
                        {
                            G.DrawImage(Win10Preview.StartBtn_10Light.ReplaceColor(Color.Black, _StartColor), StartBtnImgRect);
                        }
                        else
                        {
                            G.DrawImage(Win11Preview.StartBtn_11_EP.ReplaceColor(Color.Black, _StartColor), StartBtnImgRect);
                        }

                        Color AppColor = _AppBackground;
                        using (SolidBrush br = new(AppColor))
                        {
                            G.FillRectangle(br, AppBtnRect);
                        }

                        #region Editor

                        if (!DesignMode && EnableEditingColors && CursorOverTaskbarApp)
                        {
                            Color color0 = Color.FromArgb(80, 255, 255, 255);
                            Color color1 = Color.FromArgb(80, 0, 0, 0);
                            using (HatchBrush hb = new(HatchStyle.Percent25, color0, color1))
                            {
                                G.FillRectangle(hb, AppBtnRect);
                            }
                        }

                        #endregion

                        using (SolidBrush br = new(_AppUnderline.Light()))
                        {
                            G.FillRectangle(br, AppBtnRectUnderline);
                        }


                        #region Editor

                        if (!DesignMode && EnableEditingColors && CursorOverTaskbarAppUnderline)
                        {
                            Color color0 = Color.FromArgb(80, 255, 255, 255);
                            Color color1 = Color.FromArgb(80, 0, 0, 0);
                            using (HatchBrush hb = new(HatchStyle.Percent25, color0, color1))
                            {
                                G.FillRectangle(hb, AppBtnRectUnderline);
                            }
                        }

                        #endregion

                        G.DrawImage(Resources.SampleApp_Active, AppBtnImgRect);

                        using (SolidBrush br = new(_AppUnderline.Light()))
                        {
                            G.FillRectangle(br, App2BtnRectUnderline);
                        }
                        G.DrawImage(Resources.SampleApp_Inactive, App2BtnImgRect);
                        break;
                    }
                #endregion

                case Styles.AltTab10:
                    #region Alt+Tab 10
                    {
                        float a = Math.Max(Math.Min(255, (float)BackColorAlpha / 100 * 255), 0);

                        using (SolidBrush br = new(Color.FromArgb((int)a, 23, 23, 23)))
                        {
                            G.FillRectangle(br, RRect);
                        }

                        int AppHeight = (int)(0.75 * RRect.Height);
                        int _padding = (RRect.Height - AppHeight) / 2;

                        int appsNumber = 3;
                        int AllAppsWidthWithPadding = RRect.Width - 2 * _padding;
                        int AppWidth = (AllAppsWidthWithPadding - (appsNumber - 1) * _padding) / appsNumber;

                        List<Rectangle> Rects = [];
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
                            Rectangle r = Rects[x];

                            Color back = DarkMode ? Color.FromArgb(60, 60, 60) : Color.FromArgb(255, 255, 255);

                            if (x == 0)
                            {
                                Rectangle surround = new(r.X - 5, r.Y - 5, r.Width + 10, r.Height + 10);
                                using (Pen P = new(Color.White, 2))
                                {
                                    G.DrawRectangle(P, surround);
                                }
                            }

                            G.DrawImage(x == 0 ? Resources.SampleApp_Active : Resources.SampleApp_Inactive, new Rectangle(r.X + 5, r.Y + 5, 20, 20));

                            G.FillRectangle(Brushes.White, new Rectangle(r.X + 5 + 20 + 5, r.Y + 5 + (20 - 4) / 2, 20, 4));

                            using (SolidBrush br = new(back))
                            {
                                G.FillRectangle(br, new Rectangle(r.X + 1, r.Y + 5 + 20 + 5, r.Width - 2, r.Height - 5 - 20 - 5));
                            }
                        }

                        break;
                    }
                #endregion

                case Styles.Taskbar81Aero:
                    #region Taskbar 8.1 Aero
                    {
                        Color c = Color.FromArgb(Win7ColorBal / 100 * 255, Background);
                        Color bc = Color.FromArgb(217, 217, 217);

                        using (Pen P = new(Color.FromArgb(80, 0, 0, 0)))
                        {
                            G.DrawLine(P, new Point(0, 0), new Point(Width - 1, 0));
                        }
                        using (SolidBrush br = new(Color.FromArgb(BackColorAlpha, bc)))
                        {
                            G.FillRectangle(br, Rect);
                        }
                        using (SolidBrush br = new(Color.FromArgb((int)(BackColorAlpha * ((decimal)Win7ColorBal / 100)), c)))
                        {
                            G.FillRectangle(br, Rect);
                        }

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

                        Bitmap StartORB = new(Win81Preview.ORB);
                        Rectangle StartBtnRect = new((35 - 27) / 2 + 2, (35 - 27) / 2 - 1, 27, 27);
                        Rectangle AppBtnRect = new(StartBtnRect.Right + 8, 0, 45, Height - 1);
                        Rectangle AppBtnRectInner = new(AppBtnRect.X + 1, AppBtnRect.Y + 1, AppBtnRect.Width - 2, AppBtnRect.Height - 2);

                        Rectangle AppBtnImgRect = new(AppBtnRect.X + (AppBtnRect.Width - Resources.SampleApp_Active.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - Resources.SampleApp_Active.Height) / 2, Resources.SampleApp_Active.Width, Resources.SampleApp_Active.Height);
                        Rectangle App2BtnRect = new(AppBtnRect.Right + 2, 0, 45, Height - 1);
                        Rectangle App2BtnRectInner = new(App2BtnRect.X + 1, App2BtnRect.Y + 1, App2BtnRect.Width - 2, App2BtnRect.Height - 2);
                        Rectangle App2BtnImgRect = new(App2BtnRect.X + (App2BtnRect.Width - Resources.SampleApp_Inactive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - Resources.SampleApp_Inactive.Height) / 2, Resources.SampleApp_Inactive.Width, Resources.SampleApp_Inactive.Height);

                        G.DrawImage(StartORB, StartBtnRect);

                        using (SolidBrush br = new(Color.FromArgb(100, Color.White)))
                        {
                            G.FillRectangle(br, AppBtnRect);
                        }
                        using (Pen P = new(Color.FromArgb(200, c.CB(-0.5f))))
                        {
                            G.DrawRectangle(P, AppBtnRect);
                        }
                        using (Pen P = new(Color.FromArgb(215, Color.White)))
                        {
                            G.DrawRectangle(P, AppBtnRectInner);
                        }

                        G.DrawImage(Resources.SampleApp_Active, AppBtnImgRect);

                        using (SolidBrush br = new(Color.FromArgb(50, Color.White)))
                        {
                            G.FillRectangle(br, App2BtnRect);
                        }
                        using (Pen P = new(Color.FromArgb(100, c.CB(-0.5f))))
                        {
                            G.DrawRectangle(P, App2BtnRect);
                        }
                        using (Pen P = new(Color.FromArgb(100, Color.White)))
                        {
                            G.DrawRectangle(P, App2BtnRectInner);
                        }

                        G.DrawImage(Resources.SampleApp_Inactive, App2BtnImgRect);
                        break;
                    }
                #endregion

                case Styles.Taskbar81Lite:
                    #region Taskbar 8.1 Lite
                    {
                        Color c = Color.FromArgb(Win7ColorBal / 100 * 255, Background);
                        Color bc = Color.FromArgb(217, 217, 217);

                        using (Pen P = new(Color.FromArgb(89, 89, 89)))
                        {
                            G.DrawRectangle(P, new Rectangle(0, 0, Width - 1, Height - 1));
                        }

                        using (SolidBrush br = new(Color.FromArgb(255, bc)))
                        {
                            G.FillRectangle(br, Rect);
                        }
                        using (SolidBrush br = new(Color.FromArgb((int)(BackColorAlpha * ((decimal)Win7ColorBal / 100)), c)))
                        {
                            G.FillRectangle(br, Rect);
                        }

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

                        Bitmap StartORB = new(Win81Preview.ORB);
                        Rectangle StartBtnRect = new((35 - 27) / 2 + 2, (35 - 27) / 2 - 1, 27, 27);
                        Rectangle AppBtnRect = new(StartBtnRect.Right + 8, 0, 45, Height - 1);
                        Rectangle AppBtnRectInner = new(AppBtnRect.X + 1, AppBtnRect.Y + 1, AppBtnRect.Width - 2, AppBtnRect.Height - 2);

                        Rectangle AppBtnImgRect = new(AppBtnRect.X + (AppBtnRect.Width - Resources.SampleApp_Active.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - Resources.SampleApp_Active.Height) / 2, Resources.SampleApp_Active.Width, Resources.SampleApp_Active.Height);
                        Rectangle App2BtnRect = new(AppBtnRect.Right + 2, 0, 45, Height - 1);
                        Rectangle App2BtnRectInner = new(App2BtnRect.X + 1, App2BtnRect.Y + 1, App2BtnRect.Width - 2, App2BtnRect.Height - 2);
                        Rectangle App2BtnImgRect = new(App2BtnRect.X + (App2BtnRect.Width - Resources.SampleApp_Inactive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - Resources.SampleApp_Inactive.Height) / 2, Resources.SampleApp_Inactive.Width, Resources.SampleApp_Inactive.Height);

                        G.DrawImage(StartORB, StartBtnRect);

                        using (SolidBrush br = new(Color.FromArgb(255, bc.CB(0.5f))))
                        {
                            G.FillRectangle(br, AppBtnRect);
                        }
                        using (SolidBrush br = new(Color.FromArgb(255 * (Win7ColorBal / 100), c.CB(0.5f))))
                        {
                            G.FillRectangle(br, AppBtnRect);
                        }
                        using (Pen P = new(Color.FromArgb(100, bc.CB(-0.5f))))
                        {
                            G.DrawRectangle(P, AppBtnRect);
                        }
                        using (Pen P = new(Color.FromArgb(100 * (Win7ColorBal / 100), c.CB(-0.5f))))
                        {
                            G.DrawRectangle(P, AppBtnRect);
                        }

                        G.DrawImage(Resources.SampleApp_Active, AppBtnImgRect);

                        using (SolidBrush br = new(Color.FromArgb(255, bc.Light(0.1f))))
                        {
                            G.FillRectangle(br, App2BtnRect);
                        }
                        using (SolidBrush br = new(Color.FromArgb(255 * (Win7ColorBal / 100), c.Light(0.1f))))
                        {
                            G.FillRectangle(br, App2BtnRect);
                        }
                        using (Pen P = new(Color.FromArgb(100, bc.Dark(0.1f))))
                        {
                            G.DrawRectangle(P, App2BtnRect);
                        }
                        using (Pen P = new(Color.FromArgb(100 * (Win7ColorBal / 100), c.Dark(0.1f))))
                        {
                            G.DrawRectangle(P, App2BtnRect);
                        }
                        G.DrawImage(Resources.SampleApp_Inactive, App2BtnImgRect);
                        break;
                    }
                #endregion

                case Styles.Taskbar8Aero:
                    #region Taskbar 8 Aero
                    {
                        Color c = Color.FromArgb(Win7ColorBal / 100 * 255, Background);
                        Color bc = Color.FromArgb(217, 217, 217);

                        using (Pen P = new(Color.FromArgb(80, 0, 0, 0)))
                        {
                            G.DrawLine(P, new Point(0, 0), new Point(Width - 1, 0));
                        }
                        using (SolidBrush br = new(Color.FromArgb(BackColorAlpha, bc)))
                        {
                            G.FillRectangle(br, Rect);
                        }
                        using (SolidBrush br = new(Color.FromArgb((int)(BackColorAlpha * ((decimal)Win7ColorBal / 100)), c)))
                        {
                            G.FillRectangle(br, Rect);
                        }

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

                        Rectangle AppBtnRect = new(20, 0, 45, Height - 1);
                        Rectangle AppBtnRectInner = new(AppBtnRect.X + 1, AppBtnRect.Y + 1, AppBtnRect.Width - 2, AppBtnRect.Height - 2);

                        Rectangle AppBtnImgRect = new(AppBtnRect.X + (AppBtnRect.Width - Resources.SampleApp_Active.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - Resources.SampleApp_Active.Height) / 2, Resources.SampleApp_Active.Width, Resources.SampleApp_Active.Height);
                        Rectangle App2BtnRect = new(AppBtnRect.Right + 2, 0, 45, Height - 1);
                        Rectangle App2BtnRectInner = new(App2BtnRect.X + 1, App2BtnRect.Y + 1, App2BtnRect.Width - 2, App2BtnRect.Height - 2);
                        Rectangle App2BtnImgRect = new(App2BtnRect.X + (App2BtnRect.Width - Resources.SampleApp_Inactive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - Resources.SampleApp_Inactive.Height) / 2, Resources.SampleApp_Inactive.Width, Resources.SampleApp_Inactive.Height);

                        using (SolidBrush br = new(Color.FromArgb(100, Color.White)))
                        {
                            G.FillRectangle(br, AppBtnRect);
                        }
                        using (Pen P = new(Color.FromArgb(200, c.CB(-0.5f))))
                        {
                            G.DrawRectangle(P, AppBtnRect);
                        }
                        using (Pen P = new(Color.FromArgb(215, Color.White)))
                        {
                            G.DrawRectangle(P, AppBtnRectInner);
                        }

                        G.DrawImage(Resources.SampleApp_Active, AppBtnImgRect);

                        using (SolidBrush br = new(Color.FromArgb(50, Color.White)))
                        {
                            G.FillRectangle(br, App2BtnRect);
                        }
                        using (Pen P = new(Color.FromArgb(100, c.CB(-0.5f))))
                        {
                            G.DrawRectangle(P, App2BtnRect);
                        }
                        using (Pen P = new(Color.FromArgb(100, Color.White)))
                        {
                            G.DrawRectangle(P, App2BtnRectInner);
                        }

                        G.DrawImage(Resources.SampleApp_Inactive, App2BtnImgRect);
                        break;
                    }
                #endregion

                case Styles.Taskbar8Lite:
                    #region Taskbar 8 Lite
                    {
                        Color c = Color.FromArgb(Win7ColorBal / 100 * 255, Background);
                        Color bc = Color.FromArgb(217, 217, 217);

                        using (Pen P = new(Color.FromArgb(89, 89, 89)))
                        {
                            G.DrawRectangle(P, new Rectangle(0, 0, Width - 1, Height - 1));
                        }

                        using (SolidBrush br = new(Color.FromArgb(255, bc)))
                        {
                            G.FillRectangle(br, Rect);
                        }
                        using (SolidBrush br = new(Color.FromArgb((int)(BackColorAlpha * ((decimal)Win7ColorBal / 100)), c)))
                        {
                            G.FillRectangle(br, Rect);
                        }

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

                        Rectangle AppBtnRect = new(20, 0, 45, Height - 1);
                        Rectangle AppBtnRectInner = new(AppBtnRect.X + 1, AppBtnRect.Y + 1, AppBtnRect.Width - 2, AppBtnRect.Height - 2);

                        Rectangle AppBtnImgRect = new(AppBtnRect.X + (AppBtnRect.Width - Resources.SampleApp_Active.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - Resources.SampleApp_Active.Height) / 2, Resources.SampleApp_Active.Width, Resources.SampleApp_Active.Height);
                        Rectangle App2BtnRect = new(AppBtnRect.Right + 2, 0, 45, Height - 1);
                        Rectangle App2BtnRectInner = new(App2BtnRect.X + 1, App2BtnRect.Y + 1, App2BtnRect.Width - 2, App2BtnRect.Height - 2);
                        Rectangle App2BtnImgRect = new(App2BtnRect.X + (App2BtnRect.Width - Resources.SampleApp_Inactive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - Resources.SampleApp_Inactive.Height) / 2, Resources.SampleApp_Inactive.Width, Resources.SampleApp_Inactive.Height);

                        using (SolidBrush br = new(Color.FromArgb(255, bc.CB(0.5f))))
                        {
                            G.FillRectangle(br, AppBtnRect);
                        }
                        using (SolidBrush br = new(Color.FromArgb(255 * (Win7ColorBal / 100), c.CB(0.5f))))
                        {
                            G.FillRectangle(br, AppBtnRect);
                        }
                        using (Pen P = new(Color.FromArgb(100, bc.CB(-0.5f))))
                        {
                            G.DrawRectangle(P, AppBtnRect);
                        }
                        using (Pen P = new(Color.FromArgb(100 * (Win7ColorBal / 100), c.CB(-0.5f))))
                        {
                            G.DrawRectangle(P, AppBtnRect);
                        }

                        G.DrawImage(Resources.SampleApp_Active, AppBtnImgRect);

                        using (SolidBrush br = new(Color.FromArgb(255, bc.Light(0.1f))))
                        {
                            G.FillRectangle(br, App2BtnRect);
                        }
                        using (SolidBrush br = new(Color.FromArgb(255 * (Win7ColorBal / 100), c.Light(0.1f))))
                        {
                            G.FillRectangle(br, App2BtnRect);
                        }
                        using (Pen P = new(Color.FromArgb(100, bc.Dark(0.1f))))
                        {
                            G.DrawRectangle(P, App2BtnRect);
                        }
                        using (Pen P = new(Color.FromArgb(100 * (Win7ColorBal / 100), c.Dark(0.1f))))
                        {
                            G.DrawRectangle(P, App2BtnRect);
                        }
                        G.DrawImage(Resources.SampleApp_Inactive, App2BtnImgRect);
                        break;
                    }
                #endregion

                case Styles.AltTab8Aero:
                    #region Alt+Tab 8 Aero
                    {
                        using (SolidBrush br = new(Background))
                        {
                            G.FillRectangle(br, RRect);
                        }

                        int AppHeight = (int)(0.75 * RRect.Height);
                        int _padding = (RRect.Height - AppHeight) / 2;

                        int appsNumber = 3;
                        int AllAppsWidthWithPadding = RRect.Width - 2 * _padding;
                        int AppWidth = (AllAppsWidthWithPadding - (appsNumber - 1) * _padding) / appsNumber;

                        List<Rectangle> Rects = [];
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
                            Rectangle r = Rects[x];

                            if (x == 0)
                            {
                                Rectangle surround = new(r.X - 10, r.Y - 10, r.Width + 20, r.Height + 20);
                                using (Pen P = new(Color.White, 2))
                                {
                                    G.DrawRectangle(P, surround);
                                }
                            }

                            G.FillRectangle(Brushes.White, r);
                            int icon_w = Resources.SampleApp_Active.Width;
                            Rectangle icon_rect = new(r.X + r.Width - (int)(0.7 * icon_w), r.Y + r.Height - (int)(0.6 * icon_w), icon_w, icon_w);
                            G.DrawImage(x == 0 ? Resources.SampleApp_Active : Resources.SampleApp_Inactive, icon_rect);
                        }

                        Rectangle TextRect = new(RRect.X + _padding, RRect.Y, RRect.Width - 2 * _padding, AppHeight * 2 / 5);
                        using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
                        {
                            G.DrawString(Program.Localization.Strings.Previewer.AppPreview, Font, Brushes.White, TextRect, sf);
                        }
                        break;
                    }
                #endregion

                case Styles.AltTab8AeroLite:
                    #region Alt+Tab 8 Opaque
                    {
                        using (SolidBrush br = new(Background))
                        {
                            G.FillRectangle(br, RRect);
                        }

                        using (Pen P = new(LinkColor, 2))
                        {
                            G.DrawRectangle(P, RRect);
                        }

                        int AppHeight = (int)(0.75 * RRect.Height);
                        int _padding = (RRect.Height - AppHeight) / 2;

                        int appsNumber = 3;
                        int AllAppsWidthWithPadding = RRect.Width - 2 * _padding;
                        int AppWidth = (AllAppsWidthWithPadding - (appsNumber - 1) * _padding) / appsNumber;

                        List<Rectangle> Rects = [];
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
                            Rectangle r = Rects[x];

                            if (x == 0)
                            {
                                Rectangle surround = new(r.X - 10, r.Y - 10, r.Width + 20, r.Height + 20);
                                using (Pen P = new(Background2, 2))
                                {
                                    G.DrawRectangle(P, surround);
                                }
                            }

                            G.FillRectangle(Brushes.White, r);
                            int icon_w = Resources.SampleApp_Active.Width;
                            Rectangle icon_rect = new(r.X + r.Width - (int)(0.7 * icon_w), r.Y + r.Height - (int)(0.6 * icon_w), icon_w, icon_w);
                            G.DrawImage(x == 0 ? Resources.SampleApp_Active : Resources.SampleApp_Inactive, icon_rect);
                        }

                        Rectangle TextRect = new(RRect.X + _padding, RRect.Y, RRect.Width - 2 * _padding, AppHeight * 2 / 5);
                        using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
                        {
                            using (SolidBrush br = new(ForeColor))
                            {
                                G.DrawString(Program.Localization.Strings.Previewer.AppPreview, Font, br, TextRect, sf);
                            }
                        }
                        break;
                    }
                #endregion

                case Styles.Start7Aero:
                    #region Start 7 Aero
                    {
                        Rectangle RestRect = new(0, 14, Width - 5, Height - 10);

                        if (!DesignMode && back_blurred is not null)
                        {

                            // To dismiss upper part above start menu and make there is no blur bug
                            G.SetClip(RestRect);
                            G.DrawImage(back_blurred, Rect);
                            G.ResetClip();

                            float alphaX = 1 - BackColorAlpha / 100f;  // ColorBlurBalance
                            if (alphaX < 0) alphaX = 0;
                            if (alphaX > 1) alphaX = 1;

                            float ColBal = Win7ColorBal / 100f;   // ColorBalance
                            float GlowBal = Win7GlowBal / 100f;   // AfterGlowBalance
                            Color Color1 = Background;
                            Color Color2 = Background2;

                            G.DrawAeroEffect(RestRect, back_blurred, Color1, ColBal, Color2, GlowBal, alphaX, 5, true);
                        }

                        if (Noise7Start != null)
                            G.DrawRoundImage(Noise7Start, Rect, 5, true);

                        #region Editor

                        if (!DesignMode && EnableEditingColors && CursorOverWindowAccent)
                        {
                            Color color0 = Color.FromArgb(80, 255, 255, 255);
                            Color color1 = Color.FromArgb(80, 0, 0, 0);
                            using (Pen P = new(color0))
                            using (HatchBrush hb = new(HatchStyle.Percent25, color0, color1))
                            {
                                G.FillRoundedRect(hb, RestRect, 5, true);
                                G.DrawRoundedRect(P, RestRect, 5, true);
                            }
                        }

                        #endregion

                        G.DrawRoundImage(Win7Preview.Start, Rect, 5, true);
                        break;
                    }
                #endregion

                case Styles.Start7Opaque:
                    #region Start 7 Opaque
                    {
                        Rectangle RestRect = new(0, 14, Width - 5, Height - 10);
                        using (SolidBrush br = new(Color.White))
                        {
                            G.FillRoundedRect(br, RestRect, 5, true);
                        }
                        using (SolidBrush br = new(Color.FromArgb((int)(255 * ((decimal)Win7ColorBal / 100)), Background)))
                        {
                            G.FillRoundedRect(br, RestRect, 5, true);
                        }

                        if (Noise7Start != null)
                            G.DrawRoundImage(Noise7Start, Rect, 5, true);

                        #region Editor

                        if (!DesignMode && EnableEditingColors && CursorOverWindowAccent)
                        {
                            Color color0 = Color.FromArgb(80, 255, 255, 255);
                            Color color1 = Color.FromArgb(80, 0, 0, 0);
                            using (Pen P = new(color0))
                            using (HatchBrush hb = new(HatchStyle.Percent25, color0, color1))
                            {
                                G.FillRoundedRect(hb, RestRect, 5, true);
                                G.DrawRoundedRect(P, RestRect, 5, true);
                            }
                        }

                        #endregion

                        G.DrawRoundImage(Win7Preview.Start, Rect, 5, true);
                        break;
                    }
                #endregion

                case Styles.Start7Basic:
                    #region Start 7 Basic
                    {
                        G.DrawImage(Win7Preview.StartBasic, Rect);
                        break;
                    }
                #endregion

                case Styles.Taskbar7Aero:
                    #region Taskbar 7 Aero
                    {
                        if (!DesignMode && back_blurred is not null)
                        {
                            G.DrawRoundImage(back_blurred, RRect, Radius, true);

                            float alphaX = 1 - BackColorAlpha / 100f;  // ColorBlurBalance
                            if (alphaX < 0) alphaX = 0;
                            if (alphaX > 1) alphaX = 1;

                            float ColBal = Win7ColorBal / 100f;        // ColorBalance
                            float GlowBal = Win7GlowBal / 100f;        // AfterGlowBalance
                            Color Color1 = Background;
                            Color Color2 = Background2;

                            G.DrawAeroEffect(Rect, back_blurred, Color1, ColBal, Color2, GlowBal, alphaX, 0, false);
                        }

                        G.DrawImage(Win7Preview.TaskbarSides, Rect);

                        if (Noise7Start != null)
                            G.DrawRoundImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), Rect, Radius, true);

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

                        using (Pen P = new(Color.FromArgb(80, 0, 0, 0)))
                        {
                            G.DrawLine(P, new Point(0, 0), new Point(Width - 1, 0));
                        }
                        using (Pen P = new(Color.FromArgb(80, 255, 255, 255)))
                        {
                            G.DrawLine(P, new Point(0, 1), new Point(Width - 1, 1));
                        }

                        G.DrawImage(Win7Preview.AeroPeek, new Rectangle(Width - 10, 0, 10, Height));

                        Bitmap StartORB = new(Win7Preview.ORB);

                        Rectangle StartBtnRect = new(3, -3, 39, 39);

                        Rectangle AppBtnRect = new(StartBtnRect.Right + 5, 0, 43, 34);
                        Rectangle AppBtnImgRect = new(AppBtnRect.X + (AppBtnRect.Width - Resources.SampleApp_Active.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - Resources.SampleApp_Active.Height) / 2 - 1, Resources.SampleApp_Active.Width, Resources.SampleApp_Active.Height);

                        Rectangle App2BtnRect = new(AppBtnRect.Right + 1, 0, 43, 34);
                        Rectangle App2BtnImgRect = new(App2BtnRect.X + (App2BtnRect.Width - Resources.SampleApp_Inactive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - Resources.SampleApp_Inactive.Height) / 2 - 1, Resources.SampleApp_Inactive.Width, Resources.SampleApp_Inactive.Height);

                        G.DrawImage(StartORB, StartBtnRect);

                        using (Pen P = new(Color.FromArgb(150, 0, 0, 0)))
                        {
                            G.DrawRoundedRect(P, new Rectangle(AppBtnRect.X, AppBtnRect.Y, AppBtnRect.Width - 2, AppBtnRect.Height - 2), 2, true);
                        }
                        G.DrawImage(Win7Preview.Taskbar_ActiveApp, AppBtnRect);
                        G.DrawImage(Resources.SampleApp_Active, AppBtnImgRect);

                        using (Pen P = new(Color.FromArgb(110, 0, 0, 0)))
                        {
                            G.DrawRoundedRect(P, new Rectangle(App2BtnRect.X, App2BtnRect.Y, App2BtnRect.Width - 2, App2BtnRect.Height - 2), 2, true);
                        }
                        G.DrawImage(Win7Preview.Taskbar_InactiveApp, App2BtnRect);
                        G.DrawImage(Resources.SampleApp_Inactive, App2BtnImgRect);
                        break;
                    }
                #endregion

                case Styles.Taskbar7Opaque:
                    #region Taskbar 7 Opaque
                    {
                        using (SolidBrush br = new(Color.White))
                        {
                            G.FillRectangle(br, Rect);
                        }
                        using (SolidBrush br = new(Color.FromArgb((int)(255 * ((decimal)Win7ColorBal / 100)), Background)))
                        {
                            G.FillRectangle(br, Rect);
                        }
                        G.DrawImage(Win7Preview.TaskbarSides, Rect);

                        if (Noise7Start != null)
                            G.DrawRoundImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), Rect, Radius, true);

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

                        using (Pen P = new(Color.FromArgb(80, 0, 0, 0)))
                        {
                            G.DrawLine(P, new Point(0, 0), new Point(Width - 1, 0));
                        }
                        using (Pen P = new(Color.FromArgb(80, 255, 255, 255)))
                        {
                            G.DrawLine(P, new Point(0, 1), new Point(Width - 1, 1));
                        }

                        G.DrawImage(Win7Preview.AeroPeek, new Rectangle(Width - 10, 0, 10, Height));

                        Bitmap StartORB = new(Win7Preview.ORB);

                        Rectangle StartBtnRect = new(3, -3, 39, 39);

                        Rectangle AppBtnRect = new(StartBtnRect.Right + 5, 0, 43, 34);
                        Rectangle AppBtnImgRect = new(AppBtnRect.X + (AppBtnRect.Width - Resources.SampleApp_Active.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - Resources.SampleApp_Active.Height) / 2 - 1, Resources.SampleApp_Active.Width, Resources.SampleApp_Active.Height);

                        Rectangle App2BtnRect = new(AppBtnRect.Right + 1, 0, 43, 34);
                        Rectangle App2BtnImgRect = new(App2BtnRect.X + (App2BtnRect.Width - Resources.SampleApp_Inactive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - Resources.SampleApp_Inactive.Height) / 2 - 1, Resources.SampleApp_Inactive.Width, Resources.SampleApp_Inactive.Height);

                        G.DrawImage(StartORB, StartBtnRect);

                        using (Pen P = new(Color.FromArgb(150, 0, 0, 0)))
                        {
                            G.DrawRoundedRect(P, new Rectangle(AppBtnRect.X, AppBtnRect.Y, AppBtnRect.Width - 2, AppBtnRect.Height - 2), 2, true);
                        }
                        G.DrawImage(Win7Preview.Taskbar_ActiveApp, AppBtnRect);
                        G.DrawImage(Resources.SampleApp_Active, AppBtnImgRect);

                        using (Pen P = new(Color.FromArgb(110, 0, 0, 0)))
                        {
                            G.DrawRoundedRect(P, new Rectangle(App2BtnRect.X, App2BtnRect.Y, App2BtnRect.Width - 2, App2BtnRect.Height - 2), 2, true);
                        }
                        G.DrawImage(Win7Preview.Taskbar_InactiveApp, App2BtnRect);
                        G.DrawImage(Resources.SampleApp_Inactive, App2BtnImgRect);
                        break;
                    }
                #endregion

                case Styles.Taskbar7Basic:
                    #region Taskbar 7 Basic
                    {
                        G.DrawImage(Win7Preview.BasicTaskbar, Rect);

                        G.DrawImage(Win7Preview.AeroPeek, new Rectangle(Width - 10, 0, 10, Height));

                        Bitmap StartORB = new(Win7Preview.ORB);

                        Rectangle StartBtnRect = new(3, -3, 39, 39);

                        Rectangle AppBtnRect = new(StartBtnRect.Right + 5, 0, 43, 34);
                        Rectangle AppBtnImgRect = new(AppBtnRect.X + (AppBtnRect.Width - Resources.SampleApp_Active.Width) / 2, AppBtnRect.Y + (AppBtnRect.Height - Resources.SampleApp_Active.Height) / 2 - 1, Resources.SampleApp_Active.Width, Resources.SampleApp_Active.Height);

                        Rectangle App2BtnRect = new(AppBtnRect.Right + 1, 0, 43, 34);
                        Rectangle App2BtnImgRect = new(App2BtnRect.X + (App2BtnRect.Width - Resources.SampleApp_Inactive.Width) / 2, App2BtnRect.Y + (App2BtnRect.Height - Resources.SampleApp_Inactive.Height) / 2 - 1, Resources.SampleApp_Inactive.Width, Resources.SampleApp_Inactive.Height);

                        G.DrawImage(StartORB, StartBtnRect);

                        using (Pen P = new(Color.FromArgb(150, 0, 0, 0)))
                        {
                            G.DrawRoundedRect(P, new Rectangle(AppBtnRect.X, AppBtnRect.Y, AppBtnRect.Width - 2, AppBtnRect.Height - 2), 2, true);
                        }
                        G.DrawImage(Win7Preview.Taskbar_ActiveApp, AppBtnRect);
                        G.DrawImage(Resources.SampleApp_Active, AppBtnImgRect);

                        using (Pen P = new(Color.FromArgb(110, 0, 0, 0)))
                        {
                            G.DrawRoundedRect(P, new Rectangle(App2BtnRect.X, App2BtnRect.Y, App2BtnRect.Width - 2, App2BtnRect.Height - 2), 2, true);
                        }
                        G.DrawImage(Win7Preview.Taskbar_InactiveApp, App2BtnRect);
                        G.DrawImage(Resources.SampleApp_Inactive, App2BtnImgRect);
                        break;
                    }
                #endregion

                case Styles.AltTab7Aero:
                    #region Alt+Tab 7 Aero
                    {
                        if (Shadow & !DesignMode) G.DrawGlow(RRect, Color.FromArgb(150, 0, 0, 0), 5, 15);

                        Rectangle inner = new(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2);
                        Color Color1 = Background;
                        Color Color2 = Background2;

                        if (!DesignMode && back_blurred is not null)
                        {
                            float alpha = 1 - BackColorAlpha / 100f;   // ColorBlurBalance
                            float ColBal = Win7ColorBal / 100f;        // ColorBalance
                            float GlowBal = Win7GlowBal / 100f;        // AfterGlowBalance
                            G.DrawAeroEffect(RRect, back_blurred, Color1, ColBal, Color2, GlowBal, alpha, Radius, true);
                        }

                        if (Noise7Start != null) G.DrawRoundImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), RRect, Radius, true);

                        using (Pen P = new(Color.FromArgb(200, 25, 25, 25))) { G.DrawRoundedRect(P, RRect, Radius, true); }

                        using (Pen P = new(Color.FromArgb(70, 200, 200, 200))) { G.DrawRoundedRect(P, inner, Radius, true); }

                        int AppHeight = (int)(0.75 * RRect.Height);
                        int _padding = (RRect.Height - AppHeight) / 2;

                        int appsNumber = 3;
                        int AllAppsWidthWithPadding = RRect.Width - 2 * _padding;
                        int AppWidth = (AllAppsWidthWithPadding - (appsNumber - 1) * _padding) / appsNumber;

                        List<Rectangle> Rects = [];
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
                            Rectangle r = Rects[x];

                            if (x == 0)
                            {
                                Rectangle surround = new(r.X - 10, r.Y - 10, r.Width + 20, r.Height + 20);
                                using (SolidBrush br = new(Color.FromArgb(75, 200, 200, 200)))
                                {
                                    G.FillRoundedRect(br, surround, 1, true);
                                }

                                using (Bitmap b0 = Win7Preview.TitleTopL.Fade(0.35f))
                                using (Bitmap b1 = Win7Preview.TitleTopR.Fade(0.35f))
                                {
                                    G.DrawRoundImage(b0, surround, 2, true);
                                    G.DrawRoundImage(b1, surround, 2, true);
                                }

                                using (Pen P = new(Color1))
                                {
                                    G.DrawRoundedRect(P, surround, 1, true);
                                }
                                using (Pen P = new(Color.FromArgb(229, 240, 250)))
                                {
                                    G.DrawRectangle(P, new Rectangle(surround.X + 1, surround.Y + 1, surround.Width - 2, surround.Height - 2));
                                }

                            }

                            G.FillRoundedRect(Brushes.White, r, 2, true);
                            G.DrawRoundedRect(Pens.Black, r, 2, true);

                            int icon_w = Resources.SampleApp_Active.Width;

                            Rectangle icon_rect = new(r.X + r.Width - (int)(0.7 * icon_w), r.Y + r.Height - (int)(0.6 * icon_w), icon_w, icon_w);

                            G.DrawImage(x == 0 ? Resources.SampleApp_Active : Resources.SampleApp_Inactive, icon_rect);
                        }

                        Rectangle TextRect = new(RRect.X + _padding, RRect.Y, RRect.Width - 2 * _padding, AppHeight * 2 / 5);
                        using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
                        {
                            G.DrawGlowString(2, Program.Localization.Strings.Previewer.AppPreview, Font, Color.Black, Color.FromArgb(185, 225, 225, 225), RRect, TextRect, sf);
                        }
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
                        Rectangle inner = new(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2);

                        using (SolidBrush br = new(Color.White))
                        {
                            G.FillRoundedRect(br, RRect, Radius, true);
                        }
                        using (SolidBrush br = new(Color.FromArgb((int)(255 * ((decimal)Win7ColorBal / 100)), Background)))
                        {
                            G.FillRoundedRect(br, RRect, Radius, true);
                        }

                        if (Noise7Start != null)
                            G.DrawRoundImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), RRect, Radius, true);

                        using (Pen P = new(Color.FromArgb(200, 25, 25, 25)))
                        {
                            G.DrawRoundedRect(P, RRect, Radius, true);
                        }
                        using (Pen P = new(Color.FromArgb(70, 200, 200, 200)))
                        {
                            G.DrawRoundedRect(P, inner, Radius, true);
                        }

                        int AppHeight = (int)(0.75 * RRect.Height);
                        int _padding = (RRect.Height - AppHeight) / 2;

                        int appsNumber = 3;
                        int AllAppsWidthWithPadding = RRect.Width - 2 * _padding;
                        int AppWidth = (AllAppsWidthWithPadding - (appsNumber - 1) * _padding) / appsNumber;

                        List<Rectangle> Rects = [];
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
                            Rectangle r = Rects[x];

                            if (x == 0)
                            {
                                Rectangle surround = new(r.X - 10, r.Y - 10, r.Width + 20, r.Height + 20);
                                using (SolidBrush br = new(Color.FromArgb(75, 200, 200, 200)))
                                {
                                    G.FillRoundedRect(br, surround, 1, true);
                                }

                                using (Bitmap b0 = Win7Preview.TitleTopL.Fade(0.35f))
                                using (Bitmap b1 = Win7Preview.TitleTopR.Fade(0.35f))
                                {
                                    G.DrawRoundImage(b0, surround, 2, true);
                                    G.DrawRoundImage(b1, surround, 2, true);
                                }

                                using (Pen P = new(Background))
                                {
                                    G.DrawRoundedRect(P, surround, 1, true);
                                }
                                using (Pen P = new(Color.FromArgb(229, 240, 250)))
                                {
                                    G.DrawRectangle(P, new Rectangle(surround.X + 1, surround.Y + 1, surround.Width - 2, surround.Height - 2));
                                }

                            }

                            G.FillRoundedRect(Brushes.White, r, 2, true);
                            G.DrawRoundedRect(Pens.Black, r, 2, true);

                            int icon_w = Resources.SampleApp_Active.Width;

                            Rectangle icon_rect = new(r.X + r.Width - (int)(0.7 * icon_w), r.Y + r.Height - (int)(0.6 * icon_w), icon_w, icon_w);

                            G.DrawImage(x == 0 ? Resources.SampleApp_Active : Resources.SampleApp_Inactive, icon_rect);
                        }

                        Rectangle TextRect = new(RRect.X + _padding, RRect.Y, RRect.Width - 2 * _padding, AppHeight * 2 / 5);
                        using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
                        {
                            G.DrawGlowString(2, Program.Localization.Strings.Previewer.AppPreview, Font, Color.Black, Color.FromArgb(185, 225, 225, 225), RRect, TextRect, sf);
                        }
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
                        Rectangle UpperPart = new(RRect.X, RRect.Y, RRect.Width + 1, 25);
                        G.SetClip(UpperPart);
                        LinearGradientBrush pth_back = new(UpperPart, Titlebar_Background1, Titlebar_BackColor2, LinearGradientMode.Vertical);
                        LinearGradientBrush pth_line = new(UpperPart, Titlebar_InnerBorder, Titlebar_Turquoise, LinearGradientMode.Vertical);
                        // ### Render Titlebar
                        G.FillRectangle(pth_back, RRect);
                        using (Pen P = new(Titlebar_OuterBorder))
                        {
                            G.DrawRectangle(P, RRect);
                        }
                        using (Pen P = new(Titlebar_InnerBorder))
                        {
                            G.DrawRectangle(P, new Rectangle(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2));
                        }
                        G.SetClip(new Rectangle(UpperPart.X + (int)(UpperPart.Width * 0.75), UpperPart.Y, (int)(UpperPart.Width * 0.75), UpperPart.Height));
                        using (Pen P = new(pth_line))
                        {
                            G.DrawRectangle(P, new Rectangle(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2));
                        }
                        G.ResetClip();
                        G.ExcludeClip(UpperPart);
                        // ### Render Rest of WindowR
                        using (SolidBrush br = new(Titlebar_BackColor2))
                        {
                            G.FillRectangle(br, RRect);
                        }
                        using (Pen P = new(Titlebar_Turquoise))
                        {
                            G.DrawRectangle(P, new Rectangle(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2));
                        }
                        using (Pen P = new(OuterBorder))
                        {
                            G.DrawRectangle(P, RRect);
                        }
                        G.ResetClip();
                        using (Pen P = new(Color.FromArgb(52, 52, 52)))
                        {
                            G.DrawRectangle(P, RRect);
                        }
                        using (Pen P = new(Color.FromArgb(255, 225, 225, 225)))
                        {
                            G.DrawRectangle(P, new Rectangle(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2));
                        }


                        int AppHeight = Win7Preview.AltTabBasicButton.Height;
                        int _padding = 5;

                        int appsNumber = 3;
                        int AppWidth = Win7Preview.AltTabBasicButton.Width;

                        int _paddingOuter = (RRect.Width - AppWidth * appsNumber - _padding * (appsNumber - 1)) / 2;

                        List<Rectangle> Rects = [];
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
                            Rectangle r = Rects[x];
                            if (x == 0)
                                G.DrawImage(Win7Preview.AltTabBasicButton, r);

                            Rectangle imgrect = new(r.X + (r.Width - Resources.SampleApp_Active.Width) / 2, r.Y + (r.Height - Resources.SampleApp_Active.Height) / 2, Resources.SampleApp_Active.Width, Resources.SampleApp_Active.Height);

                            G.DrawImage(x == 0 ? Resources.SampleApp_Active : Resources.SampleApp_Inactive, imgrect);
                        }

                        Rectangle TextRect = new(RRect.X + _padding, RRect.Y, RRect.Width - 2 * _padding, 30);
                        using (StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat())
                        {
                            G.DrawString(Program.Localization.Strings.Previewer.AppPreview, Font, Brushes.Black, TextRect, sf);
                        }
                        break;
                    }
                #endregion

                case Styles.StartVistaAero:
                    #region Start Vista Aero
                    {
                        Rectangle RestRect = new(0, 14, Width - 6, Height - 14);

                        // To dismiss upper part above start menu and make there is no blur bug
                        G.SetClip(RestRect);
                        if (!DesignMode && back_blurred is not null)
                            G.DrawImage(back_blurred, Rect);
                        G.ResetClip();

                        using (SolidBrush br = new(Color.FromArgb((int)(Win7ColorBal * 255f / 100f), Background)))
                        {
                            G.FillRoundedRect(br, RestRect, 4, true);
                        }

                        #region Editor

                        if (!DesignMode && EnableEditingColors && CursorOverWindowAccent)
                        {
                            Color color0 = Color.FromArgb(80, 255, 255, 255);
                            Color color1 = Color.FromArgb(80, 0, 0, 0);
                            using (Pen P = new(color0))
                            using (HatchBrush hb = new(HatchStyle.Percent25, color0, color1))
                            {
                                G.FillRoundedRect(hb, RestRect, 5, true);
                                G.DrawRoundedRect(P, RestRect, 5, true);
                            }
                        }

                        #endregion

                        G.DrawImage(WinVistaPreview.Start, new Rectangle(0, 0, Width, Height));
                        break;
                    }
                #endregion

                case Styles.StartVistaOpaque:
                    #region Start Vista Opaque
                    {
                        Rectangle RestRect = new(0, 14, Width - 6, Height - 14);
                        G.FillRoundedRect(Brushes.White, RestRect, 4, true);
                        using (SolidBrush br = new(Color.FromArgb((int)(Win7ColorBal * 255f / 100f), Background)))
                        {
                            G.FillRoundedRect(br, RestRect, 4, true);
                        }

                        #region Editor

                        if (!DesignMode && EnableEditingColors && CursorOverWindowAccent)
                        {
                            Color color0 = Color.FromArgb(80, 255, 255, 255);
                            Color color1 = Color.FromArgb(80, 0, 0, 0);
                            using (Pen P = new(color0))
                            using (HatchBrush hb = new(HatchStyle.Percent25, color0, color1))
                            {
                                G.FillRoundedRect(hb, RestRect, 5, true);
                                G.DrawRoundedRect(P, RestRect, 5, true);
                            }
                        }

                        #endregion

                        G.DrawImage(WinVistaPreview.Start, new Rectangle(0, 0, Width, Height));
                        break;
                    }
                #endregion

                case Styles.StartVistaBasic:
                    #region Start Vista Basic
                    {
                        G.DrawImage(WinVistaPreview.StartBasic, new Rectangle(0, 0, Width, Height));
                        break;
                    }
                #endregion

                case Styles.TaskbarVistaAero:
                    #region Taskbar Vista Aero
                    {
                        if (!DesignMode && back_blurred is not null)
                            G.DrawImage(back_blurred, Rect);
                        using (SolidBrush br = new(Color.FromArgb((int)(Win7ColorBal * 255f / 100f), Background)))
                        {
                            G.FillRectangle(br, Rect);
                        }
                        G.FillRectangle(new TextureBrush(WinVistaPreview.Taskbar), Rect);

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

                        Bitmap orb = WinVistaPreview.ORB;
                        G.DrawImage(orb, new Rectangle(0, 0, orb.Width, Height));

                        Rectangle apprect1 = new(Rect.X + 60, 1, 140, Rect.Height - 4);
                        Rectangle apprect2 = new(apprect1.Right + 2, 1, 140, Rect.Height - 4);
                        Rectangle appIcon1 = new(apprect1.X + 4, apprect1.Y + (apprect1.Height - 20) / 2, 20, 20);
                        Rectangle appIcon2 = new(apprect2.X + 4, apprect2.Y + (apprect2.Height - 20) / 2, 20, 20);
                        Rectangle appLabel1 = new(apprect1.X + 25, apprect1.Y, apprect1.Width - 30, apprect1.Height);
                        Rectangle appLabel2 = new(apprect2.X + 25, apprect2.Y, apprect2.Width - 30, apprect2.Height);

                        G.DrawImage(WinVistaPreview.Taskbar_ActiveApp, apprect1);
                        G.DrawImage(WinVistaPreview.Taskbar_InactiveApp, apprect2);

                        G.DrawImage(Resources.SampleApp_Active, appIcon1);
                        G.DrawImage(Resources.SampleApp_Inactive, appIcon2);

                        using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat())
                        {
                            G.DrawString(Program.Localization.Strings.Previewer.AppPreview, Font, Brushes.White, appLabel1, sf);
                            G.DrawString(Program.Localization.Strings.Previewer.InactiveApp, Font, Brushes.White, appLabel2, sf);
                        }
                        break;
                    }
                #endregion

                case Styles.TaskbarVistaOpaque:
                    #region Taskbar Vista Opaque
                    {
                        Bitmap orb = WinVistaPreview.ORB;
                        G.FillRectangle(Brushes.White, Rect);
                        using (SolidBrush br = new(Color.FromArgb((int)(Win7ColorBal * 255f / 100f), Background)))
                        {
                            G.FillRectangle(br, Rect);
                        }
                        G.FillRectangle(new TextureBrush(WinVistaPreview.Taskbar), Rect);

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

                        G.DrawImage(orb, new Rectangle(0, 0, orb.Width, Height));

                        Rectangle apprect1 = new(Rect.X + 60, 1, 140, Rect.Height - 4);
                        Rectangle apprect2 = new(apprect1.Right + 2, 1, 140, Rect.Height - 4);
                        Rectangle appIcon1 = new(apprect1.X + 4, apprect1.Y + (apprect1.Height - 20) / 2, 20, 20);
                        Rectangle appIcon2 = new(apprect2.X + 4, apprect2.Y + (apprect2.Height - 20) / 2, 20, 20);
                        Rectangle appLabel1 = new(apprect1.X + 25, apprect1.Y, apprect1.Width - 30, apprect1.Height);
                        Rectangle appLabel2 = new(apprect2.X + 25, apprect2.Y, apprect2.Width - 30, apprect2.Height);

                        G.DrawImage(WinVistaPreview.Taskbar_ActiveApp, apprect1);
                        G.DrawImage(WinVistaPreview.Taskbar_InactiveApp, apprect2);

                        G.DrawImage(Resources.SampleApp_Active, appIcon1);
                        G.DrawImage(Resources.SampleApp_Inactive, appIcon2);

                        using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat())
                        {
                            G.DrawString(Program.Localization.Strings.Previewer.AppPreview, Font, Brushes.White, appLabel1, sf);
                            G.DrawString(Program.Localization.Strings.Previewer.InactiveApp, Font, Brushes.White, appLabel2, sf);
                        }
                        break;
                    }
                #endregion

                case Styles.TaskbarVistaBasic:
                    #region Taskbar Vista Basic
                    {
                        Bitmap orb = WinVistaPreview.ORB;
                        G.FillRectangle(Brushes.Black, Rect);
                        G.FillRectangle(new TextureBrush(WinVistaPreview.Taskbar), Rect);
                        G.DrawImage(orb, new Rectangle(0, 0, orb.Width, Height));

                        Rectangle apprect1 = new(Rect.X + 60, 1, 140, Rect.Height - 4);
                        Rectangle apprect2 = new(apprect1.Right + 2, 1, 140, Rect.Height - 4);
                        Rectangle appIcon1 = new(apprect1.X + 4, apprect1.Y + (apprect1.Height - 20) / 2, 20, 20);
                        Rectangle appIcon2 = new(apprect2.X + 4, apprect2.Y + (apprect2.Height - 20) / 2, 20, 20);
                        Rectangle appLabel1 = new(apprect1.X + 25, apprect1.Y, apprect1.Width - 30, apprect1.Height);
                        Rectangle appLabel2 = new(apprect2.X + 25, apprect2.Y, apprect2.Width - 30, apprect2.Height);

                        G.DrawImage(WinVistaPreview.Taskbar_ActiveApp, apprect1);
                        G.DrawImage(WinVistaPreview.Taskbar_InactiveApp, apprect2);

                        G.DrawImage(Resources.SampleApp_Active, appIcon1);
                        G.DrawImage(Resources.SampleApp_Inactive, appIcon2);

                        using (StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat())
                        {
                            G.DrawString(Program.Localization.Strings.Previewer.AppPreview, Font, Brushes.White, appLabel1, sf);
                            G.DrawString(Program.Localization.Strings.Previewer.InactiveApp, Font, Brushes.White, appLabel2, sf);
                        }
                        break;
                    }
                #endregion

                case Styles.TaskbarXP:
                    #region Taskbar XP
                    {
                        SmoothingMode sm = G.SmoothingMode;
                        G.SmoothingMode = SmoothingMode.HighSpeed;
                        resVS?.Draw(G, Rect, VisualStylesRes.Element.Taskbar, true, false);
                        G.SmoothingMode = sm;

                        break;
                    }
                    #endregion
            }

            base.OnPaint(e);


        }
    }
}