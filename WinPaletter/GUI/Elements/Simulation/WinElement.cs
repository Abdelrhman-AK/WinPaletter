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

        #region Fields

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

        // These are shared across instances; they represent the noise/glass textures used by
        // all style variants. Callers must dispose before replacing.
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

        public enum MouseState { Normal, Hover, Pressed }

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
                if (value == _Style) return;
                _Style = value;
                ProcessBack();
                Invalidate();
            }
        }

        private int _BackColorAlpha = 130;
        public int BackColorAlpha
        {
            get => _BackColorAlpha;
            set
            {
                if (value == _BackColorAlpha) return;
                _BackColorAlpha = value;
                Invalidate();
            }
        }

        private float _NoisePower = 0f;
        public float NoisePower
        {
            get => _NoisePower;
            set
            {
                if (value == _NoisePower) return;
                _NoisePower = value;
                NoiseBack();
                Invalidate();
            }
        }

        private int _BlurPower = 8;
        public int BlurPower
        {
            get => _BlurPower;
            set
            {
                if (value == _BlurPower) return;
                _BlurPower = value;
                BlurBack();
                Invalidate();
            }
        }

        private bool _Transparency = true;
        public bool Transparency
        {
            get => _Transparency;
            set
            {
                if (value == _Transparency) return;
                _Transparency = value;
                ProcessBack();
                Invalidate();
            }
        }

        private bool _DarkMode = true;
        public bool DarkMode
        {
            get => _DarkMode;
            set
            {
                if (value == _DarkMode) return;
                _DarkMode = value;
                Invalidate();
            }
        }

        private Color _AppUnderline;
        public Color AppUnderline
        {
            get => _AppUnderline;
            set
            {
                if (value == _AppUnderline) return;
                _AppUnderline = value;
                Invalidate();
            }
        }

        private Color _AppBackground;
        public Color AppBackground
        {
            get => _AppBackground;
            set
            {
                if (value == _AppBackground) return;
                _AppBackground = value;
                Invalidate();
            }
        }

        private Color _ActionCenterButton_Normal;
        public Color ActionCenterButton_Normal
        {
            get => _ActionCenterButton_Normal;
            set
            {
                if (value == _ActionCenterButton_Normal) return;
                _ActionCenterButton_Normal = value;
                Invalidate();
            }
        }

        private Color _ActionCenterButton_Hover;
        public Color ActionCenterButton_Hover
        {
            get => _ActionCenterButton_Hover;
            set
            {
                if (value == _ActionCenterButton_Hover) return;
                _ActionCenterButton_Hover = value;
                Invalidate();
            }
        }

        private Color _ActionCenterButton_Pressed;
        public Color ActionCenterButton_Pressed
        {
            get => _ActionCenterButton_Pressed;
            set
            {
                if (value == _ActionCenterButton_Pressed) return;
                _ActionCenterButton_Pressed = value;
                Invalidate();
            }
        }

        private Color _StartColor;
        public Color StartColor
        {
            get => _StartColor;
            set
            {
                if (value == _StartColor) return;
                _StartColor = value;
                Invalidate();
            }
        }

        private Color _LinkColor;
        public Color LinkColor
        {
            get => _LinkColor;
            set
            {
                if (value == _LinkColor) return;
                _LinkColor = value;
                Invalidate();
            }
        }

        private Color _Background;
        public Color Background
        {
            get => _Background;
            set
            {
                if (value == _Background) return;
                _Background = value;
                Invalidate();
            }
        }

        private Color _Background2;
        public Color Background2
        {
            get => _Background2;
            set
            {
                if (value == _Background2) return;
                _Background2 = value;
                Invalidate();
            }
        }

        private int _Win7ColorBal = 0;
        public int Win7ColorBal
        {
            get => _Win7ColorBal;
            set
            {
                if (value == _Win7ColorBal) return;
                _Win7ColorBal = value;
                Invalidate();
            }
        }

        private int _Win7GlowBal = 0;
        public int Win7GlowBal
        {
            get => _Win7GlowBal;
            set
            {
                if (value == _Win7GlowBal) return;
                _Win7GlowBal = value;
                Invalidate();
            }
        }

        public bool UseWin11ORB_WithWin10 { get; set; } = false;
        public bool UseWin11RoundedCorners_WithWin10_Level1 { get; set; } = false;
        public bool UseWin11RoundedCorners_WithWin10_Level2 { get; set; } = false;
        public bool Shadow { get; set; } = true;

        #endregion

        #region Background Processing

        public void SetStyles()
        {
            rect = new(0, 0, Width - 1, Height - 1);
        }

        public void ProcessBack()
        {
            if (IsInDesignMode) return;
            GetBack();
            BlurBack();
            NoiseBack();
        }

        public void GetBack()
        {
            if (IsInDesignMode) return;

            Bitmap wallpaper = Parent?.BackgroundImage as Bitmap ?? Program.WallpaperMonitor.Get(Program.TM, Program.WindowStyle);

            back?.Dispose();
            back = null;

            if (wallpaper != null && Bounds.Width > 0 && Bounds.Height > 0)
            {
                Rectangle imageBounds = new(0, 0, wallpaper.Width, wallpaper.Height);
                if (imageBounds.Contains_ButNotExceed(Bounds)) back = wallpaper.Clone(Bounds, wallpaper.PixelFormat);
            }
        }

        public void BlurBack()
        {
            if (IsInDesignMode) return;

            back_blurred?.Dispose();
            back_blurred = null;

            if (back == null || !Transparency) return;

            int blurAmount = BlurPower;

            // Aero glass styles use a lighter blur to preserve glass look
            if (Style is Styles.Start7Aero or Styles.Taskbar7Aero or Styles.StartVistaAero or Styles.TaskbarVistaAero or Styles.AltTab7Aero)
            {
                blurAmount = 3;
            }

            // Enhanced Acrylic blur for Windows 10/11
            Bitmap blurred = null;
            if (Style is Styles.Taskbar11 or Styles.Start11 or Styles.ActionCenter11 or Styles.AltTab11 or 
                Styles.Taskbar10 or Styles.Start10 or Styles.ActionCenter10 or Styles.AltTab10)
            {
                // Use Frosted blur for authentic Acrylic effect
                blurred = back.Blur(blurAmount, TypesExtensions.BitmapExtensions.BlurType.Frosted, 1.0f);
                
                if (blurred != null)
                {
                    // Apply Acrylic-specific HSL adjustments
                    if (Style is Styles.Taskbar11 or Styles.Start11 or Styles.ActionCenter11 or Styles.AltTab11)
                    {
                        // Windows 11 Acrylic: more subtle adjustments
                        if (DarkMode)
                        {
                            using (Bitmap adjusted = blurred.AdjustHSL(
                                hShift: 0f,           // No hue shift
                                sValue: 0.4f,         // Reduce saturation to 40%
                                lValue: 0.5f          // Slightly reduce lightness
                            ))
                                using (Bitmap contrasted = adjusted.Contrast(0.05f))
                                {
                                    back_blurred = new Bitmap(contrasted);
                                }
                            }
                        else
                        {
                            using (Bitmap adjusted = blurred.AdjustHSL(
                                hShift: 3f,            // Slight warm hue shift
                                sValue: 0.5f,          // Moderate saturation reduction
                                lValue: 0.6f           // Increase lightness
                            ))
                            {
                                using (Bitmap contrasted = adjusted.Contrast(0.02f))
                                {
                                    back_blurred = new Bitmap(contrasted);
                                }
                            }
                        }
                    }
                    else
                    {
                        // Windows 10 Acrylic: more pronounced blur effect
                        if (DarkMode)
                        {
                            using (Bitmap adjusted = blurred.AdjustHSL(
                                hShift: 0f,           // No hue shift
                                sValue: 0.35f,        // Reduce saturation to 35%
                                lValue: 0.45f         // Reduce lightness more
                            ))
                            {
                                using (Bitmap contrasted = adjusted.Contrast(0.08f))
                                {
                                    back_blurred = new Bitmap(contrasted);
                                }
                            }
                        }
                        else
                        {
                            using (Bitmap adjusted = blurred.AdjustHSL(
                                hShift: 2f,            // Very slight warm hue shift
                                sValue: 0.45f,         // Moderate saturation reduction
                                lValue: 0.55f          // Increase lightness
                            ))
                            {
                                using (Bitmap contrasted = adjusted.Contrast(0.04f))
                                {
                                    back_blurred = new Bitmap(contrasted);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                // Fallback to standard blur for other styles
                blurred = back.Blur(blurAmount);
                back_blurred = blurred;
            }
        }

        public void NoiseBack()
        {
            if (IsInDesignMode) return;

            if (Style is Styles.ActionCenter11 or Styles.Start11 or Styles.Taskbar11 or Styles.AltTab11 or Styles.ActionCenter10 or Styles.Start10 or Styles.Taskbar10)
            {
                // Win10/11 noise is a semi-transparent texture brush tiled over the background
                if (!Transparency) return;

                using Bitmap b = Resources.Noise.Fade(_NoisePower);
                Noise?.Dispose();
                Noise = new TextureBrush(b);
            }
            else if (Style is Styles.Start7Aero or Styles.Taskbar7Aero or Styles.AltTab7Aero or Styles.Start7Opaque or Styles.Taskbar7Opaque or Styles.AltTab7Opaque)
            {
                // Win7 uses separate glass bitmaps for the taskbar and Start menu
                using Bitmap b0 = Win7Preview.AeroGlass.Fade(_NoisePower / 100f);
                using Bitmap b1 = Win7Preview.StartGlass.Fade(_NoisePower / 100f);

                Noise7?.Dispose();
                Noise7Start?.Dispose();

                Noise7 = new Bitmap(b0);
                Noise7Start = new Bitmap(b1);
            }
        }

        #endregion

        #region Mouse Events

        protected override async void OnMouseMove(MouseEventArgs e)
        {
            OnMouseMove_Down_Up(e);

            if (!DesignMode && EnableEditingColors)
            {
                UpdateEditorHitZones(e.Location);
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
                ClearEditorHitZones();
                await Task.Delay(10);
                Invalidate();
            }

            base.OnMouseLeave(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (!DesignMode && EnableEditingColors) FireEditorInvoker(e);

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

        // Shared handler for move/down/up — updates the AC11 button hover/pressed states
        private async void OnMouseMove_Down_Up(MouseEventArgs e)
        {
            if (Style != Styles.ActionCenter11) return;

            Point cursor = PointToClient(MousePosition);

            _State_Btn1 = UpdateButtonState(_State_Btn1, Button1, cursor, e.Button);
            _State_Btn2 = UpdateButtonState(_State_Btn2, Button2, cursor, e.Button);

            await Task.Delay(10);
            Invalidate();
        }

        // Returns the next MouseState for a button given current cursor position and button press
        private static MouseState UpdateButtonState(MouseState current, Rectangle bounds, Point cursor, MouseButtons pressed)
        {
            if (bounds.Contains(cursor)) return pressed == MouseButtons.None ? MouseState.Hover : MouseState.Pressed;

            if (current != MouseState.Normal) return MouseState.Normal;

            return current;
        }

        #endregion

        #region Control Lifetime

        protected override void OnHandleCreated(EventArgs e)
        {
            if (!DesignMode)
            {
                ProcessBack();
                Parent?.BackgroundImageChanged += OnParentBackgroundImageChanged;
            }

            base.OnHandleCreated(e);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (!DesignMode && Parent != null) Parent.BackgroundImageChanged -= OnParentBackgroundImageChanged;

            base.OnHandleDestroyed(e);
        }

        private void OnParentBackgroundImageChanged(object sender, EventArgs e) => ProcessBack();

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

        #region Editor Hit-Testing

        bool CursorOverWindowAccent = false;
        bool CursorOverStart = false;
        bool CursorOverTaskbar = false;
        bool CursorOverActionCenter = false;
        bool CursorOverTaskbarApp = false;
        bool CursorOverTaskbarAppUnderline = false;
        bool CursorOverActionCenterLink = false;
        bool CursorOverActionCenterButton = false;
        bool CursorOverStartButton = false;

        // Computed hit rectangles used both for editor highlighting and hit-testing
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

        readonly Rectangle actionCenterLink0 = new(85, 6, 30, 3);
        readonly Rectangle actionCenterLink1 = new(5, 190, 30, 3);
        readonly Rectangle actionCenterButton = new(42, 201, 34, 24);

        // Updates all cursor-over booleans based on mouse position — called on every mouse move
        private void UpdateEditorHitZones(Point location)
        {
            CursorOverWindowAccent = rect.Contains(location) &&
                (Style == Styles.Start8 ||
                 Style == Styles.Taskbar81Aero || Style == Styles.Taskbar81Lite ||
                 Style == Styles.Start7Aero || Style == Styles.Taskbar7Aero ||
                 Style == Styles.Start7Opaque || Style == Styles.Taskbar7Opaque ||
                 Style == Styles.StartVistaAero || Style == Styles.TaskbarVistaAero ||
                 Style == Styles.StartVistaOpaque || Style == Styles.TaskbarVistaOpaque);

            CursorOverStart = rect.Contains(location) && (Style == Styles.Start11 || Style == Styles.Start10);

            CursorOverActionCenterLink = (actionCenterLink0.Contains(location) || actionCenterLink1.Contains(location)) && Style == Styles.ActionCenter10;

            CursorOverActionCenterButton = actionCenterButton.Contains(location) && Style == Styles.ActionCenter10;

            CursorOverActionCenter = rect.Contains(location) && !CursorOverActionCenterLink && !CursorOverActionCenterButton && (Style == Styles.ActionCenter11 || Style == Styles.ActionCenter10);

            CursorOverTaskbarAppUnderline = taskbarAppUnderline.Contains(location) && (Style == Styles.Taskbar10 || Style == Styles.Taskbar11);

            CursorOverTaskbarApp = taskbarApp.Contains(location) && !CursorOverTaskbarAppUnderline && Style == Styles.Taskbar10;

            CursorOverStartButton = startBtnRect.Contains(location) && Style == Styles.Taskbar10;

            CursorOverTaskbar = !CursorOverTaskbarApp && !CursorOverTaskbarAppUnderline && !CursorOverStartButton && rect.Contains(location) && (Style == Styles.Taskbar11 || Style == Styles.Taskbar10);
        }

        private void ClearEditorHitZones()
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
        }

        // Dispatches an EditorInvoker event for the zone the user clicked
        private void FireEditorInvoker(MouseEventArgs e)
        {
            if (CursorOverWindowAccent)
            {
                string key = e.Button != MouseButtons.Right ? nameof(WindowsDesktop.TitlebarColor_Active) : nameof(WindowsDesktop.AfterGlowColor_Active);

                EditorInvoker?.Invoke(this, new EditorEventArgs(key));
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

        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);
        public event EditorInvokerEventHandler EditorInvoker;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public bool EnableEditingColors { get; set; } = false;

        #endregion

        #region Paint Entry Point

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Intentionally empty — control paints its own background in OnPaint
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.TextRenderingHint = DesignMode ? TextRenderingHint.ClearTypeGridFit : Program.Style.TextRenderingHint;

            // Rect extends 1px beyond bounds on each side — used for edge-bleeding fills
            Rectangle Rect = new(-1, -1, Width + 2, Height + 2);
            // RRect stays inside the control boundary — used for rounded fills/borders
            Rectangle RRect = new(0, 0, Width - 1, Height - 1);
            const int Radius = 5;

            switch (Style)
            {
                case Styles.Start11: Paint_Start11(G, Rect, RRect, Radius); break;
                case Styles.ActionCenter11: Paint_ActionCenter11(G, Rect, RRect, Radius); break;
                case Styles.Taskbar11: Paint_Taskbar11(G, Rect, RRect, Radius); break;
                case Styles.AltTab11: Paint_AltTab11(G, Rect, RRect, Radius); break;
                case Styles.Start10: Paint_Start10(G, Rect, RRect, Radius); break;
                case Styles.ActionCenter10: Paint_ActionCenter10(G, Rect, RRect); break;
                case Styles.Taskbar10: Paint_Taskbar10(G, Rect, RRect); break;
                case Styles.AltTab10: Paint_AltTab10(G, Rect, RRect); break;
                case Styles.Taskbar81Aero: Paint_Taskbar81Aero(G, Rect, RRect); break;
                case Styles.Taskbar81Lite: Paint_Taskbar81Lite(G, Rect, RRect); break;
                case Styles.Taskbar8Aero: Paint_Taskbar8Aero(G, Rect, RRect); break;
                case Styles.Taskbar8Lite: Paint_Taskbar8Lite(G, Rect, RRect); break;
                case Styles.AltTab8Aero: Paint_AltTab8Aero(G, Rect, RRect); break;
                case Styles.AltTab8AeroLite: Paint_AltTab8AeroLite(G, Rect, RRect); break;
                case Styles.Start7Aero: Paint_Start7Aero(G, Rect, RRect); break;
                case Styles.Start7Opaque: Paint_Start7Opaque(G, Rect, RRect); break;
                case Styles.Start7Basic: Paint_Start7Basic(G, Rect); break;
                case Styles.Taskbar7Aero: Paint_Taskbar7Aero(G, Rect, RRect, Radius); break;
                case Styles.Taskbar7Opaque: Paint_Taskbar7Opaque(G, Rect, RRect, Radius); break;
                case Styles.Taskbar7Basic: Paint_Taskbar7Basic(G, Rect, RRect); break;
                case Styles.AltTab7Aero: Paint_AltTab7Aero(G, Rect, RRect, Radius); break;
                case Styles.AltTab7Opaque: Paint_AltTab7Opaque(G, Rect, RRect, Radius); break;
                case Styles.AltTab7Basic: Paint_AltTab7Basic(G, Rect, RRect); break;
                case Styles.StartVistaAero: Paint_StartVistaAero(G, Rect, RRect); break;
                case Styles.StartVistaOpaque: Paint_StartVistaOpaque(G, Rect, RRect); break;
                case Styles.StartVistaBasic: Paint_StartVistaBasic(G, Rect); break;
                case Styles.TaskbarVistaAero: Paint_TaskbarVistaAero(G, Rect, RRect); break;
                case Styles.TaskbarVistaOpaque: Paint_TaskbarVistaOpaque(G, Rect, RRect); break;
                case Styles.TaskbarVistaBasic: Paint_TaskbarVistaBasic(G, Rect); break;
                case Styles.TaskbarXP: Paint_TaskbarXP(G, Rect); break;
            }

            base.OnPaint(e);
        }

        #endregion

        #region Paint Helpers

        // Cached colors for the editor selection overlay — same values used across all highlight variants
        private static readonly Color EditorHighlight_Light = Color.FromArgb(80, 255, 255, 255);
        private static readonly Color EditorHighlight_Dark = Color.FromArgb(80, 0, 0, 0);

        // Clamps a ColorBlurBalance float to [0, 1]
        private static float ClampAlpha(float raw) => raw < 0f ? 0f : raw > 1f ? 1f : raw;

        // Draws the blurred wallpaper slice into a rounded rectangle, if available
        private void DrawBlurredBack_Rounded(Graphics G, Rectangle RRect, int radius)
        {
            if (!DesignMode && Transparency && back_blurred != null) G.DrawRoundImage(back_blurred, RRect, radius, true);
        }

        // Draws the blurred wallpaper slice into a plain rectangle, if available
        private void DrawBlurredBack_Flat(Graphics G, Rectangle Rect)
        {
            if (!DesignMode && Transparency && back_blurred != null) G.DrawImage(back_blurred, Rect);
        }

        // Fills a rounded rect with the noise texture, if available
        private void DrawNoise_Rounded(Graphics G, Rectangle RRect, int radius)
        {
            if (Transparency && Noise != null) G.FillRoundedRect(Noise, RRect, radius, true);
        }

        // Fills a flat rect with the noise texture, if available
        private void DrawNoise_Flat(Graphics G, Rectangle Rect)
        {
            if (Transparency && Noise != null) G.FillRectangle(Noise, Rect);
        }

        // Paints the standard Win11/Win10 glass base layer: blur + dark/light tint + accent color
        private void DrawWin11GlassBase_Rounded(Graphics G, Rectangle RRect, int radius)
        {
            DrawBlurredBack_Rounded(G, RRect, radius);

            int tintAlpha = DarkMode ? 85 : 75;
            Color tint = DarkMode ? Color.FromArgb(tintAlpha, 28, 28, 28) : Color.FromArgb(tintAlpha, 255, 255, 255);

            using (SolidBrush br = new(tint)) G.FillRoundedRect(br, RRect, radius, true);

            using (SolidBrush br = new(Color.FromArgb(Transparency ? BackColorAlpha : 255, Background))) G.FillRoundedRect(br, RRect, radius, true);
        }

        // Renders a hatch-pattern editor highlight over a rounded region
        private static void DrawEditorHighlight_Rounded(Graphics G, Rectangle RRect, int radius)
        {
            using Pen pen = new(EditorHighlight_Light);
            using HatchBrush hb = new(HatchStyle.Percent25, EditorHighlight_Light, EditorHighlight_Dark);
            G.FillRoundedRect(hb, RRect, radius, true);
            G.DrawRoundedRect(pen, RRect, radius, true);
        }

        // Renders a hatch-pattern editor highlight over a flat region
        private static void DrawEditorHighlight_Flat(Graphics G, Rectangle Rect)
        {
            using Pen pen = new(EditorHighlight_Light);
            using HatchBrush hb = new(HatchStyle.Percent25, EditorHighlight_Light, EditorHighlight_Dark);
            G.FillRectangle(hb, Rect);
            G.DrawRectangle(pen, Rect);
        }

        // Renders a hatch-pattern highlight over a flat region, no border pen
        private static void DrawEditorHighlight_FlatFill(Graphics G, Rectangle Rect)
        {
            using HatchBrush hb = new(HatchStyle.Percent25, EditorHighlight_Light, EditorHighlight_Dark);
            G.FillRectangle(hb, Rect);
        }

        // Renders a hatch-pattern highlight over a rounded region, no border pen
        private static void DrawEditorHighlight_RoundedFill(Graphics G, Rectangle RRect, int radius)
        {
            using HatchBrush hb = new(HatchStyle.Percent25, EditorHighlight_Light, EditorHighlight_Dark);
            G.FillRoundedRect(hb, RRect, radius, true);
        }

        // Builds the standard list of app thumbnail rectangles for Alt+Tab dialogs
        private static List<Rectangle> BuildAltTabRects(Rectangle RRect, int appsNumber)
        {
            int AppHeight = (int)(0.75 * RRect.Height);
            int padding = (RRect.Height - AppHeight) / 2;
            int AllWidth = RRect.Width - 2 * padding;
            int AppWidth = (AllWidth - (appsNumber - 1) * padding) / appsNumber;

            List<Rectangle> rects = new(appsNumber);
            for (int i = 0; i < appsNumber; i++)
            {
                int x = i == 0 ? RRect.X + padding : rects[i - 1].Right + padding;
                rects.Add(new Rectangle(x, RRect.Y + padding, AppWidth, AppHeight));
            }

            return rects;
        }

        // Builds the standard list of tall app thumbnail rectangles for Win7/8 Alt+Tab dialogs,
        // where the thumbnails occupy the lower portion and a text label sits above them
        private static (List<Rectangle> rects, int padding, int AppHeight) BuildAltTab7Rects(Rectangle RRect, int appsNumber)
        {
            int AppHeight = (int)(0.75 * RRect.Height);
            int padding = (RRect.Height - AppHeight) / 2;
            int AllWidth = RRect.Width - 2 * padding;
            int AppWidth = (AllWidth - (appsNumber - 1) * padding) / appsNumber;

            List<Rectangle> rects = [with(appsNumber)];
            for (int i = 0; i < appsNumber; i++)
            {
                int x = i == 0 ? RRect.X + padding : rects[i - 1].Right + padding;

                // Thumbnails sit in the lower 3/5; top 2/5 is reserved for the app title label
                rects.Add(new Rectangle(x, RRect.Y + padding + AppHeight * 2 / 5, AppWidth, AppHeight * 3 / 5));
            }

            return (rects, padding, AppHeight);
        }

        #endregion

        #region Windows 11 Paint Methods

        private void Paint_Start11(Graphics G, Rectangle Rect, Rectangle RRect, int Radius)
        {
            DrawWin11GlassBase_Rounded(G, RRect, Radius);
            DrawNoise_Rounded(G, RRect, Radius);

            if (!DesignMode && EnableEditingColors && CursorOverStart) DrawEditorHighlight_Rounded(G, RRect, Radius);

            Bitmap preview = _w1125H2 ? (DarkMode ? Win11Preview.Start1125H2_Dark : Win11Preview.Start1125H2_Light) : (DarkMode ? Win11Preview.Start11_Dark : Win11Preview.Start11_Light);

            G.DrawRoundImage(preview, RRect, Radius, true);

            // Draw the search bar that sits near the top of the Start menu
            Rectangle SearchRect = new(8, _w1125H2 ? 7 : 10, Width - 16, _w1125H2 ? 16 : 15);

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

            using (SolidBrush br = new(SearchColor)) G.FillRoundedRect(br, SearchRect, 8, true);

            using (Pen P = new(SearchBorderColor)) G.DrawRoundedRect(P, SearchRect, 8, true);

            using (Pen P = new(Color.FromArgb(150, 90, 90, 90))) G.DrawRoundedRect(P, RRect, Radius, true);
        }

        private void Paint_ActionCenter11(Graphics G, Rectangle Rect, Rectangle RRect, int Radius)
        {
            DrawWin11GlassBase_Rounded(G, RRect, Radius);
            DrawNoise_Rounded(G, RRect, Radius);

            Button1 = new(8, 8, 49, 20);
            Button2 = new(62, 8, 49, 20);

            if (!DesignMode && EnableEditingColors && CursorOverActionCenter) DrawEditorHighlight_Rounded(G, RRect, Radius);

            G.DrawRoundImage(DarkMode ? Win11Preview.AC_11_Dark : Win11Preview.AC_11_Light, RRect, Radius, true);

            // Resolve button colors from hover/pressed state
            Color Cx1 = _State_Btn1 switch
            {
                MouseState.Hover => ActionCenterButton_Hover,
                MouseState.Pressed => ActionCenterButton_Pressed,
                _ => ActionCenterButton_Normal
            };

            Color Cx2 = _State_Btn2 switch
            {
                MouseState.Hover => DarkMode ? Color.FromArgb(190, 90, 90, 90) : Color.FromArgb(210, 230, 230, 230),
                MouseState.Pressed => DarkMode ? Color.FromArgb(190, 75, 75, 75) : Color.FromArgb(210, 210, 210, 210),
                _ => DarkMode ? Color.FromArgb(190, 70, 70, 70) : Color.FromArgb(180, 140, 140, 140)
            };

            using (SolidBrush br = new(Cx1)) G.FillRoundedRect(br, Button1, Radius, true);

            using (Pen P = new(Cx1.Light(0.15f))) G.DrawRoundedRectBeveled(P, Button1, Radius, true);

            using (SolidBrush br = new(Cx2)) G.FillRoundedRect(br, Button2, Radius, true);

            using (Pen P = new(Cx2.CB(DarkMode ? 0.05f : -0.05f))) G.DrawRoundedRect(P, Button2, Radius, true);

            using (Pen P = new(Color.FromArgb(150, 90, 90, 90))) G.DrawRoundedRect(P, RRect, Radius, true);
        }

        private void Paint_Taskbar11(Graphics G, Rectangle Rect, Rectangle RRect, int Radius)
        {
            DrawBlurredBack_Flat(G, Rect);

            int tintAlpha = DarkMode ? 110 : 90;
            Color tint = DarkMode ? Color.FromArgb(tintAlpha, 28, 28, 28) : Color.FromArgb(tintAlpha, 255, 255, 255);

            using (SolidBrush br = new(tint)) G.FillRectangle(br, Rect);

            using (SolidBrush br = new(Color.FromArgb(Transparency ? BackColorAlpha : 255, Background))) G.FillRectangle(br, Rect);

            DrawNoise_Rounded(G, RRect, Radius);

            if (!DesignMode && EnableEditingColors && CursorOverTaskbar) DrawEditorHighlight_FlatFill(G, RRect);

            // Layout: Start button, an active pinned app, and an inactive pinned app
            Rectangle StartBtnRect = new(8, 3, 36, 36);
            Rectangle StartImgRect = new(8, 3, 37, 37);

            Rectangle App2BtnRect = new(StartBtnRect.Right + 5, 3, 36, 36);
            Rectangle App2ImgRect = new(StartBtnRect.Right + 5, 3, 37, 37);
            Rectangle App2BtnRectUnderline = new(App2BtnRect.X + (App2BtnRect.Width - 8) / 2, App2BtnRect.Y + App2BtnRect.Height - 3, 8, 3);

            Rectangle AppBtnRect = new(App2BtnRect.Right + 5, 3, 36, 36);
            Rectangle AppImgRect = new(App2BtnRect.Right + 5, 3, 37, 37);
            Rectangle AppBtnRectUnderline = new(AppBtnRect.X + (AppBtnRect.Width - 18) / 2, AppBtnRect.Y + AppBtnRect.Height - 3, 18, 3);

            Color BackC = DarkMode ? Color.FromArgb(45, 130, 130, 130) : Color.FromArgb(35, 255, 255, 255);

            using (SolidBrush br = new(BackC)) G.FillRoundedRect(br, StartBtnRect, 3, true);
            using (Pen P = new(BackC)) G.DrawRoundedRectBeveled(P, StartBtnRect, 3);

            G.DrawImage(DarkMode ? Win11Preview.StartBtn_11Dark : Win11Preview.StartBtn_11Light, StartImgRect);

            using (SolidBrush br = new(BackC)) G.FillRoundedRect(br, AppBtnRect, 3, true);
            using (Pen P = new(BackC)) G.DrawRoundedRectBeveled(P, AppBtnRect, 3);

            G.DrawImage(Resources.SampleApp_Active, AppImgRect);

            using (SolidBrush br = new(_AppUnderline)) G.FillRoundedRect(br, AppBtnRectUnderline, 2, true);

            if (!DesignMode && EnableEditingColors && CursorOverTaskbarAppUnderline) DrawEditorHighlight_RoundedFill(G, AppBtnRectUnderline, 2);

            G.DrawImage(Resources.SampleApp_Inactive, App2ImgRect);

            using (SolidBrush br = new(Color.FromArgb(255, BackC))) G.FillRoundedRect(br, App2BtnRectUnderline, 2, true);

            // Top edge separator line
            using (Pen P = new(Color.FromArgb(100, 100, 100, 100))) G.DrawLine(P, new Point(0, 0), new Point(Width - 1, 0));
        }

        private void Paint_AltTab11(Graphics G, Rectangle Rect, Rectangle RRect, int Radius)
        {
            DrawBlurredBack_Rounded(G, RRect, Radius);

            if (Transparency)
            {
                Color fill = DarkMode ? Color.FromArgb(100, 175, 175, 175) : Color.FromArgb(120, 185, 185, 185);

                using (SolidBrush br = new(fill)) G.FillRoundedRect(br, RRect, Radius, true);

                DrawNoise_Rounded(G, RRect, Radius);
            }
            else if (DarkMode)
            {
                using (SolidBrush br = new(Color.FromArgb(32, 32, 32))) G.FillRoundedRect(br, RRect, Radius, true);

                using (Pen P = new(Color.FromArgb(65, 65, 65))) G.DrawRoundedRect(P, RRect, Radius, true);
            }
            else
            {
                using (SolidBrush br = new(Color.FromArgb(243, 243, 243))) G.FillRoundedRect(br, RRect, Radius, true);

                using (Pen P = new(Color.FromArgb(171, 171, 171))) G.DrawRoundedRect(P, RRect, Radius, true);
            }

            List<Rectangle> rects = BuildAltTabRects(RRect, 3);
            int _padding = (RRect.Height - (int)(0.75 * RRect.Height)) / 2;

            for (int i = 0; i < rects.Count; i++)
            {
                Rectangle r = rects[i];
                Color back = DarkMode ? Color.FromArgb(23, 23, 23) : Color.FromArgb(233, 234, 234);
                Color back2 = DarkMode ? Color.FromArgb(39, 39, 39) : Color.FromArgb(255, 255, 255);

                if (i == 0)
                {
                    Rectangle surround = new(r.X - 5, r.Y - 5, r.Width + 10, r.Height + 10);
                    using Pen P = new(Color.FromArgb(75, 182, 237), 3);
                    G.DrawRoundedRect(P, surround, Radius * 2 + 5 / 2, true);
                }

                using (SolidBrush br = new(back)) G.FillRoundedRect(br, r, Radius * 2, true);

                G.DrawImage(i == 0 ? Resources.SampleApp_Active : Resources.SampleApp_Inactive, new Rectangle(r.X + 5, r.Y + 5, 20, 20));

                using (SolidBrush br = new(Color.FromArgb(150, back2))) G.FillRectangle(br, new Rectangle(r.X + 30, r.Y + 5 + (20 - 4) / 2, 20, 4));

                using (SolidBrush br = new(back2)) G.FillRoundedRect(br, new Rectangle(r.X + 1, r.Y + 30, r.Width - 2, r.Height - 30), Radius * 2, true);
            }
        }

        #endregion

        #region Windows 10 Paint Methods

        private void Paint_Start10(Graphics G, Rectangle Rect, Rectangle RRect, int Radius)
        {
            if (!UseWin11RoundedCorners_WithWin10_Level1 && !UseWin11RoundedCorners_WithWin10_Level2)
            {
                DrawBlurredBack_Flat(G, Rect);
                DrawNoise_Flat(G, Rect);

                using (SolidBrush br = new(Color.FromArgb(Transparency ? BackColorAlpha : 255, Background))) G.FillRectangle(br, Rect);

                if (!DesignMode && EnableEditingColors && CursorOverStart) DrawEditorHighlight_Flat(G, Rect);

                G.DrawImage(DarkMode ? Win10Preview.Start10_Dark : Win10Preview.Start10_Light, new Rectangle(0, 0, Width - 1, Height - 1));
            }
            else if (UseWin11RoundedCorners_WithWin10_Level1)
            {
                DrawBlurredBack_Flat(G, Rect);
                DrawNoise_Flat(G, Rect);

                using (SolidBrush br = new(Color.FromArgb(Transparency ? BackColorAlpha : 255, Background))) G.FillRectangle(br, Rect);

                if (!DesignMode && EnableEditingColors && CursorOverStart) DrawEditorHighlight_Flat(G, Rect);

                G.DrawImage(DarkMode ? Win11Preview.Start11_EP_Rounded_Dark : Win11Preview.Start11_EP_Rounded_Light, new Rectangle(0, 0, Width - 1, Height - 1));
            }
            else // Level2 — rounded background + rounded preview image
            {
                DrawBlurredBack_Flat(G, Rect);
                DrawNoise_Rounded(G, Rect, Radius);

                using (SolidBrush br = new(Color.FromArgb(Transparency ? BackColorAlpha : 255, Background))) G.FillRoundedRect(br, Rect, Radius, true);

                if (!DesignMode && EnableEditingColors && CursorOverStart) DrawEditorHighlight_Flat(G, Rect);

                G.DrawRoundImage(DarkMode ? Win11Preview.Start11_EP_Rounded_Dark : Win11Preview.Start11_EP_Rounded_Light, new Rectangle(0, 0, Width - 1, Height - 1), Radius, true);
            }
        }

        private void Paint_ActionCenter10(Graphics G, Rectangle Rect, Rectangle RRect)
        {
            DrawBlurredBack_Flat(G, Rect);
            DrawNoise_Flat(G, Rect);

            using (SolidBrush br = new(Color.FromArgb(Transparency ? BackColorAlpha : 255, Background)))
                G.FillRectangle(br, Rect);

            Rectangle rect1 = new(85, 6, 30, 3);
            Rectangle rect2 = new(5, 190, 30, 3);
            Rectangle rect3 = new(42, 201, 34, 24);

            // Draw the quick-action button before the overlay image so the overlay masks the right areas
            using (SolidBrush br = new(ActionCenterButton_Normal)) G.FillRectangle(br, rect3);

            if (!DesignMode && EnableEditingColors && CursorOverActionCenter) DrawEditorHighlight_Flat(G, Rect);

            if (!DesignMode && EnableEditingColors && CursorOverActionCenterButton) DrawEditorHighlight_FlatFill(G, rect3);

            G.DrawImage(DarkMode ? Win10Preview.AC_10_Dark : Win10Preview.AC_10_Light, new Rectangle(0, 0, Width - 1, Height - 1));

            // Link color strips are drawn on top of the overlay image
            using (SolidBrush br = new(LinkColor)) G.FillRectangle(br, rect1);
            using (SolidBrush br = new(LinkColor)) G.FillRectangle(br, rect2);

            if (!DesignMode && EnableEditingColors && CursorOverActionCenterLink)
            {
                DrawEditorHighlight_FlatFill(G, rect1);
                DrawEditorHighlight_FlatFill(G, rect2);
            }

            using (Pen P = new(Color.FromArgb(150, 100, 100, 100))) G.DrawLine(P, new Point(0, 0), new Point(0, Height - 1));

            using (Pen P = new(Color.FromArgb(150, 76, 76, 76))) G.DrawRectangle(P, Rect);
        }

        private void Paint_Taskbar10(Graphics G, Rectangle Rect, Rectangle RRect)
        {
            G.SmoothingMode = SmoothingMode.HighSpeed;

            DrawBlurredBack_Flat(G, Rect);

            using (SolidBrush br = new(Color.FromArgb(Transparency ? BackColorAlpha : 255, Background))) G.FillRectangle(br, Rect);

            if (!DesignMode && EnableEditingColors && CursorOverTaskbar)
            {
                G.ExcludeClip(startBtnRect);
                DrawEditorHighlight_FlatFill(G, Rect);
                G.ResetClip();
            }

            Rectangle StartBtnRect = new(-1, -1, 42, Height + 2);
            Rectangle StartBtnImgRect = !UseWin11ORB_WithWin10
                ? new(StartBtnRect.X + (StartBtnRect.Width - Win10Preview.StartBtn_10Light.Width) / 2,
                      StartBtnRect.Y + (StartBtnRect.Height - Win10Preview.StartBtn_10Light.Height) / 2,
                      Win10Preview.StartBtn_10Light.Width, Win10Preview.StartBtn_10Light.Height)
                : new(StartBtnRect.X + (StartBtnRect.Width - Win11Preview.StartBtn_11_EP.Width) / 2,
                      StartBtnRect.Y + (StartBtnRect.Height - Win11Preview.StartBtn_11_EP.Height) / 2,
                      Win11Preview.StartBtn_11_EP.Width, Win11Preview.StartBtn_11_EP.Height);

            Rectangle AppBtnRect = new(StartBtnRect.Right, -1, 40, Height + 2);
            Rectangle AppBtnImgRect = new(
                AppBtnRect.X + (AppBtnRect.Width - Resources.SampleApp_Active.Width) / 2,
                AppBtnRect.Y + (AppBtnRect.Height - Resources.SampleApp_Active.Height) / 2 - 1,
                Resources.SampleApp_Active.Width, Resources.SampleApp_Active.Height);
            Rectangle AppBtnRectUnderline = new(AppBtnRect.X, AppBtnRect.Y + AppBtnRect.Height - 3, AppBtnRect.Width, 2);

            Rectangle App2BtnRect = new(AppBtnRect.Right, -1, 40, Height + 2);
            Rectangle App2BtnImgRect = new(
                App2BtnRect.X + (App2BtnRect.Width - Resources.SampleApp_Inactive.Width) / 2,
                App2BtnRect.Y + (App2BtnRect.Height - Resources.SampleApp_Inactive.Height) / 2,
                Resources.SampleApp_Inactive.Width, Resources.SampleApp_Inactive.Height);
            Rectangle App2BtnRectUnderline = new(
                App2BtnRect.X + 7, App2BtnRect.Y + App2BtnRect.Height - 3,
                App2BtnRect.Width - 14, 2);

            if (!DesignMode && EnableEditingColors && CursorOverStartButton) DrawEditorHighlight_FlatFill(G, startBtnRect);

            Bitmap startIcon = !UseWin11ORB_WithWin10
                ? Win10Preview.StartBtn_10Light.ReplaceColor(Color.Black, _StartColor)
                : Win11Preview.StartBtn_11_EP.ReplaceColor(Color.Black, _StartColor);

            G.DrawImage(startIcon, StartBtnImgRect);

            using (SolidBrush br = new(_AppBackground)) G.FillRectangle(br, AppBtnRect);

            if (!DesignMode && EnableEditingColors && CursorOverTaskbarApp) DrawEditorHighlight_FlatFill(G, AppBtnRect);

            using (SolidBrush br = new(_AppUnderline.Light())) G.FillRectangle(br, AppBtnRectUnderline);

            if (!DesignMode && EnableEditingColors && CursorOverTaskbarAppUnderline) DrawEditorHighlight_FlatFill(G, AppBtnRectUnderline);

            G.DrawImage(Resources.SampleApp_Active, AppBtnImgRect);

            using (SolidBrush br = new(_AppUnderline.Light())) G.FillRectangle(br, App2BtnRectUnderline);

            G.DrawImage(Resources.SampleApp_Inactive, App2BtnImgRect);
        }

        private void Paint_AltTab10(Graphics G, Rectangle Rect, Rectangle RRect)
        {
            float a = Math.Max(Math.Min(255, (float)BackColorAlpha / 100 * 255), 0);

            using (SolidBrush br = new(Color.FromArgb((int)a, 23, 23, 23))) G.FillRectangle(br, RRect);

            List<Rectangle> rects = BuildAltTabRects(RRect, 3);

            for (int i = 0; i < rects.Count; i++)
            {
                Rectangle r = rects[i];
                Color back = DarkMode ? Color.FromArgb(60, 60, 60) : Color.FromArgb(255, 255, 255);

                if (i == 0)
                {
                    Rectangle surround = new(r.X - 5, r.Y - 5, r.Width + 10, r.Height + 10);
                    using Pen P = new(Color.White, 2);
                    G.DrawRectangle(P, surround);
                }

                G.DrawImage(i == 0 ? Resources.SampleApp_Active : Resources.SampleApp_Inactive, new Rectangle(r.X + 5, r.Y + 5, 20, 20));

                G.FillRectangle(Brushes.White, new Rectangle(r.X + 30, r.Y + 5 + (20 - 4) / 2, 20, 4));

                using (SolidBrush br = new(back)) G.FillRectangle(br, new Rectangle(r.X + 1, r.Y + 30, r.Width - 2, r.Height - 30));
            }
        }

        #endregion

        #region Windows 8 / 8.1 Paint Methods

        private void Paint_Taskbar81Aero(Graphics G, Rectangle Rect, Rectangle RRect)
        {
            Color c = Color.FromArgb(Win7ColorBal / 100 * 255, Background);
            Color bc = Color.FromArgb(217, 217, 217);
            float colorRatio = Win7ColorBal / 100f;

            using (Pen P = new(Color.FromArgb(80, 0, 0, 0))) G.DrawLine(P, new Point(0, 0), new Point(Width - 1, 0));

            using (SolidBrush br = new(Color.FromArgb(BackColorAlpha, bc))) G.FillRectangle(br, Rect);

            using (SolidBrush br = new(Color.FromArgb((int)(BackColorAlpha * colorRatio), c))) G.FillRectangle(br, Rect);

            if (!DesignMode && EnableEditingColors && CursorOverWindowAccent) DrawEditorHighlight_Flat(G, Rect);

            DrawTaskbar8_AppButtons(G, Rect, RRect, c, isAero: true);
        }

        private void Paint_Taskbar81Lite(Graphics G, Rectangle Rect, Rectangle RRect)
        {
            Color c = Color.FromArgb(Win7ColorBal / 100 * 255, Background);
            Color bc = Color.FromArgb(217, 217, 217);
            float colorRatio = Win7ColorBal / 100f;

            using (Pen P = new(Color.FromArgb(89, 89, 89))) G.DrawRectangle(P, new Rectangle(0, 0, Width - 1, Height - 1));

            using (SolidBrush br = new(Color.FromArgb(255, bc))) G.FillRectangle(br, Rect);

            using (SolidBrush br = new(Color.FromArgb((int)(BackColorAlpha * colorRatio), c))) G.FillRectangle(br, Rect);

            if (!DesignMode && EnableEditingColors && CursorOverWindowAccent) DrawEditorHighlight_Flat(G, Rect);

            DrawTaskbar8_AppButtons(G, Rect, RRect, c, isAero: false);
        }

        private void Paint_Taskbar8Aero(Graphics G, Rectangle Rect, Rectangle RRect)
        {
            Color c = Color.FromArgb(Win7ColorBal / 100 * 255, Background);
            Color bc = Color.FromArgb(217, 217, 217);
            float colorRatio = Win7ColorBal / 100f;

            using (Pen P = new(Color.FromArgb(80, 0, 0, 0))) G.DrawLine(P, new Point(0, 0), new Point(Width - 1, 0));

            using (SolidBrush br = new(Color.FromArgb(BackColorAlpha, bc))) G.FillRectangle(br, Rect);

            using (SolidBrush br = new(Color.FromArgb((int)(BackColorAlpha * colorRatio), c))) G.FillRectangle(br, Rect);

            if (!DesignMode && EnableEditingColors && CursorOverWindowAccent) DrawEditorHighlight_Flat(G, Rect);

            DrawTaskbar8_AppButtons_NoORB(G, Rect, RRect, c, isAero: true);
        }

        private void Paint_Taskbar8Lite(Graphics G, Rectangle Rect, Rectangle RRect)
        {
            Color c = Color.FromArgb(Win7ColorBal / 100 * 255, Background);
            Color bc = Color.FromArgb(217, 217, 217);
            float colorRatio = Win7ColorBal / 100f;

            using (Pen P = new(Color.FromArgb(89, 89, 89))) G.DrawRectangle(P, new Rectangle(0, 0, Width - 1, Height - 1));

            using (SolidBrush br = new(Color.FromArgb(255, bc))) G.FillRectangle(br, Rect);

            using (SolidBrush br = new(Color.FromArgb((int)(BackColorAlpha * colorRatio), c))) G.FillRectangle(br, Rect);

            if (!DesignMode && EnableEditingColors && CursorOverWindowAccent) DrawEditorHighlight_Flat(G, Rect);

            DrawTaskbar8_AppButtons_NoORB(G, Rect, RRect, c, isAero: false);
        }

        // Shared button rendering for Win 8.1 Aero/Lite taskbars (includes ORB)
        private void DrawTaskbar8_AppButtons(Graphics G, Rectangle Rect, Rectangle RRect, Color c, bool isAero)
        {
            Color bc = Color.FromArgb(217, 217, 217);

            using Bitmap StartORB = new(Win81Preview.ORB);
            Rectangle StartBtnRect = new((35 - 27) / 2 + 2, (35 - 27) / 2 - 1, 27, 27);
            Rectangle AppBtnRect = new(StartBtnRect.Right + 8, 0, 45, Height - 1);
            Rectangle AppBtnRectInner = new(AppBtnRect.X + 1, AppBtnRect.Y + 1, AppBtnRect.Width - 2, AppBtnRect.Height - 2);
            Rectangle AppBtnImgRect = new(AppBtnRect.X + (AppBtnRect.Width - Resources.SampleApp_Active.Width) / 2,
                AppBtnRect.Y + (AppBtnRect.Height - Resources.SampleApp_Active.Height) / 2,
                Resources.SampleApp_Active.Width, Resources.SampleApp_Active.Height);
            Rectangle App2BtnRect = new(AppBtnRect.Right + 2, 0, 45, Height - 1);
            Rectangle App2BtnRectInner = new(App2BtnRect.X + 1, App2BtnRect.Y + 1, App2BtnRect.Width - 2, App2BtnRect.Height - 2);
            Rectangle App2BtnImgRect = new(App2BtnRect.X + (App2BtnRect.Width - Resources.SampleApp_Inactive.Width) / 2,
                App2BtnRect.Y + (App2BtnRect.Height - Resources.SampleApp_Inactive.Height) / 2,
                Resources.SampleApp_Inactive.Width, Resources.SampleApp_Inactive.Height);

            G.DrawImage(StartORB, StartBtnRect);

            if (isAero)
            {
                using (SolidBrush br = new(Color.FromArgb(100, Color.White))) G.FillRectangle(br, AppBtnRect);
                using (Pen P = new(Color.FromArgb(200, c.CB(-0.5f)))) G.DrawRectangle(P, AppBtnRect);
                using (Pen P = new(Color.FromArgb(215, Color.White))) G.DrawRectangle(P, AppBtnRectInner);

                G.DrawImage(Resources.SampleApp_Active, AppBtnImgRect);

                using (SolidBrush br = new(Color.FromArgb(50, Color.White))) G.FillRectangle(br, App2BtnRect);
                using (Pen P = new(Color.FromArgb(100, c.CB(-0.5f)))) G.DrawRectangle(P, App2BtnRect);
                using (Pen P = new(Color.FromArgb(100, Color.White))) G.DrawRectangle(P, App2BtnRectInner);
            }
            else // Lite
            {
                int colorScaled255 = 255 * (Win7ColorBal / 100);
                int colorScaled100 = 100 * (Win7ColorBal / 100);

                using (SolidBrush br = new(Color.FromArgb(255, bc.CB(0.5f)))) G.FillRectangle(br, AppBtnRect);
                using (SolidBrush br = new(Color.FromArgb(colorScaled255, c.CB(0.5f)))) G.FillRectangle(br, AppBtnRect);
                using (Pen P = new(Color.FromArgb(100, bc.CB(-0.5f)))) G.DrawRectangle(P, AppBtnRect);
                using (Pen P = new(Color.FromArgb(colorScaled100, c.CB(-0.5f)))) G.DrawRectangle(P, AppBtnRect);

                G.DrawImage(Resources.SampleApp_Active, AppBtnImgRect);

                using (SolidBrush br = new(Color.FromArgb(255, bc.Light(0.1f)))) G.FillRectangle(br, App2BtnRect);
                using (SolidBrush br = new(Color.FromArgb(colorScaled255, c.Light(0.1f)))) G.FillRectangle(br, App2BtnRect);
                using (Pen P = new(Color.FromArgb(100, bc.Dark(0.1f)))) G.DrawRectangle(P, App2BtnRect);
                using (Pen P = new(Color.FromArgb(colorScaled100, c.Dark(0.1f)))) G.DrawRectangle(P, App2BtnRect);
            }

            G.DrawImage(Resources.SampleApp_Inactive, App2BtnImgRect);
        }

        // Shared button rendering for Win 8 Aero/Lite taskbars (no ORB — Win8 had no Start button)
        private void DrawTaskbar8_AppButtons_NoORB(Graphics G, Rectangle Rect, Rectangle RRect, Color c, bool isAero)
        {
            Color bc = Color.FromArgb(217, 217, 217);

            Rectangle AppBtnRect = new(20, 0, 45, Height - 1);
            Rectangle AppBtnRectInner = new(AppBtnRect.X + 1, AppBtnRect.Y + 1, AppBtnRect.Width - 2, AppBtnRect.Height - 2);
            Rectangle AppBtnImgRect = new(AppBtnRect.X + (AppBtnRect.Width - Resources.SampleApp_Active.Width) / 2,
                AppBtnRect.Y + (AppBtnRect.Height - Resources.SampleApp_Active.Height) / 2,
                Resources.SampleApp_Active.Width, Resources.SampleApp_Active.Height);
            Rectangle App2BtnRect = new(AppBtnRect.Right + 2, 0, 45, Height - 1);
            Rectangle App2BtnRectInner = new(App2BtnRect.X + 1, App2BtnRect.Y + 1, App2BtnRect.Width - 2, App2BtnRect.Height - 2);
            Rectangle App2BtnImgRect = new(App2BtnRect.X + (App2BtnRect.Width - Resources.SampleApp_Inactive.Width) / 2,
                App2BtnRect.Y + (App2BtnRect.Height - Resources.SampleApp_Inactive.Height) / 2,
                Resources.SampleApp_Inactive.Width, Resources.SampleApp_Inactive.Height);

            if (isAero)
            {
                using (SolidBrush br = new(Color.FromArgb(100, Color.White))) G.FillRectangle(br, AppBtnRect);
                using (Pen P = new(Color.FromArgb(200, c.CB(-0.5f)))) G.DrawRectangle(P, AppBtnRect);
                using (Pen P = new(Color.FromArgb(215, Color.White))) G.DrawRectangle(P, AppBtnRectInner);

                G.DrawImage(Resources.SampleApp_Active, AppBtnImgRect);

                using (SolidBrush br = new(Color.FromArgb(50, Color.White))) G.FillRectangle(br, App2BtnRect);
                using (Pen P = new(Color.FromArgb(100, c.CB(-0.5f)))) G.DrawRectangle(P, App2BtnRect);
                using (Pen P = new(Color.FromArgb(100, Color.White))) G.DrawRectangle(P, App2BtnRectInner);
            }
            else // Lite
            {
                int colorScaled255 = 255 * (Win7ColorBal / 100);
                int colorScaled100 = 100 * (Win7ColorBal / 100);

                using (SolidBrush br = new(Color.FromArgb(255, bc.CB(0.5f)))) G.FillRectangle(br, AppBtnRect);
                using (SolidBrush br = new(Color.FromArgb(colorScaled255, c.CB(0.5f)))) G.FillRectangle(br, AppBtnRect);
                using (Pen P = new(Color.FromArgb(100, bc.CB(-0.5f)))) G.DrawRectangle(P, AppBtnRect);
                using (Pen P = new(Color.FromArgb(colorScaled100, c.CB(-0.5f)))) G.DrawRectangle(P, AppBtnRect);

                G.DrawImage(Resources.SampleApp_Active, AppBtnImgRect);

                using (SolidBrush br = new(Color.FromArgb(255, bc.Light(0.1f)))) G.FillRectangle(br, App2BtnRect);
                using (SolidBrush br = new(Color.FromArgb(colorScaled255, c.Light(0.1f)))) G.FillRectangle(br, App2BtnRect);
                using (Pen P = new(Color.FromArgb(100, bc.Dark(0.1f)))) G.DrawRectangle(P, App2BtnRect);
                using (Pen P = new(Color.FromArgb(colorScaled100, c.Dark(0.1f)))) G.DrawRectangle(P, App2BtnRect);
            }

            G.DrawImage(Resources.SampleApp_Inactive, App2BtnImgRect);
        }

        private void Paint_AltTab8Aero(Graphics G, Rectangle Rect, Rectangle RRect)
        {
            using (SolidBrush br = new(Background)) G.FillRectangle(br, RRect);

            var (rects, padding, AppHeight) = BuildAltTab7Rects(RRect, 3);

            for (int i = 0; i < rects.Count; i++)
            {
                Rectangle r = rects[i];

                if (i == 0)
                {
                    Rectangle surround = new(r.X - 10, r.Y - 10, r.Width + 20, r.Height + 20);
                    using Pen P = new(Color.White, 2);
                    G.DrawRectangle(P, surround);
                }

                G.FillRectangle(Brushes.White, r);

                int icon_w = Resources.SampleApp_Active.Width;
                Rectangle icon_rect = new(r.X + r.Width - (int)(0.7 * icon_w), r.Y + r.Height - (int)(0.6 * icon_w), icon_w, icon_w);

                G.DrawImage(i == 0 ? Resources.SampleApp_Active : Resources.SampleApp_Inactive, icon_rect);
            }

            Rectangle TextRect = new(RRect.X + padding, RRect.Y, RRect.Width - 2 * padding, AppHeight * 2 / 5);
            using StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat();
            G.DrawString(Program.Localization.Strings.Previewer.AppPreview, Font, Brushes.White, TextRect, sf);
        }

        private void Paint_AltTab8AeroLite(Graphics G, Rectangle Rect, Rectangle RRect)
        {
            using (SolidBrush br = new(Background)) G.FillRectangle(br, RRect);

            using (Pen P = new(LinkColor, 2)) G.DrawRectangle(P, RRect);

            var (rects, padding, AppHeight) = BuildAltTab7Rects(RRect, 3);

            for (int i = 0; i < rects.Count; i++)
            {
                Rectangle r = rects[i];

                if (i == 0)
                {
                    Rectangle surround = new(r.X - 10, r.Y - 10, r.Width + 20, r.Height + 20);
                    using Pen P = new(Background2, 2);
                    G.DrawRectangle(P, surround);
                }

                G.FillRectangle(Brushes.White, r);

                int icon_w = Resources.SampleApp_Active.Width;
                Rectangle icon_rect = new(r.X + r.Width - (int)(0.7 * icon_w), r.Y + r.Height - (int)(0.6 * icon_w), icon_w, icon_w);

                G.DrawImage(i == 0 ? Resources.SampleApp_Active : Resources.SampleApp_Inactive, icon_rect);
            }

            Rectangle TextRect = new(RRect.X + padding, RRect.Y, RRect.Width - 2 * padding, AppHeight * 2 / 5);
            using StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat();
            using SolidBrush textBr = new(ForeColor);
            G.DrawString(Program.Localization.Strings.Previewer.AppPreview, Font, textBr, TextRect, sf);
        }

        #endregion

        #region Windows 7 Paint Methods

        private void Paint_Start7Aero(Graphics G, Rectangle Rect, Rectangle RRect)
        {
            // The blurred area excludes the rounded top of the popup to avoid edge artifacts
            Rectangle RestRect = new(0, 14, Width - 5, Height - 10);

            if (!DesignMode && back_blurred != null)
            {
                G.SetClip(RestRect);
                G.DrawImage(back_blurred, Rect);
                G.ResetClip();

                float alphaX = ClampAlpha(1 - BackColorAlpha / 100f);
                float ColBal = Win7ColorBal / 100f;
                float GlowBal = Win7GlowBal / 100f;

                G.DrawAeroEffect(RestRect, back_blurred, Background, ColBal, Background2, GlowBal, alphaX, 5, true);
            }

            if (Noise7Start != null) G.DrawRoundImage(Noise7Start, Rect, 5, true);

            if (!DesignMode && EnableEditingColors && CursorOverWindowAccent) DrawEditorHighlight_Rounded(G, RestRect, 5);

            G.DrawRoundImage(Win7Preview.Start, Rect, 5, true);
        }

        private void Paint_Start7Opaque(Graphics G, Rectangle Rect, Rectangle RRect)
        {
            Rectangle RestRect = new(0, 14, Width - 5, Height - 10);

            using (SolidBrush br = new(Color.White)) G.FillRoundedRect(br, RestRect, 5, true);

            using (SolidBrush br = new(Color.FromArgb((int)(255 * (Win7ColorBal / 100f)), Background))) G.FillRoundedRect(br, RestRect, 5, true);

            if (Noise7Start != null) G.DrawRoundImage(Noise7Start, Rect, 5, true);

            if (!DesignMode && EnableEditingColors && CursorOverWindowAccent) DrawEditorHighlight_Rounded(G, RestRect, 5);

            G.DrawRoundImage(Win7Preview.Start, Rect, 5, true);
        }

        private void Paint_Start7Basic(Graphics G, Rectangle Rect)
        {
            G.DrawImage(Win7Preview.StartBasic, Rect);
        }

        private void Paint_Taskbar7Aero(Graphics G, Rectangle Rect, Rectangle RRect, int Radius)
        {
            if (!DesignMode && back_blurred != null)
            {
                G.DrawRoundImage(back_blurred, RRect, Radius, true);

                float alphaX = ClampAlpha(1 - BackColorAlpha / 100f);
                float ColBal = Win7ColorBal / 100f;
                float GlowBal = Win7GlowBal / 100f;

                G.DrawAeroEffect(Rect, back_blurred, Background, ColBal, Background2, GlowBal, alphaX, 0, false);
            }

            G.DrawImage(Win7Preview.TaskbarSides, Rect);

            if (Noise7Start != null) G.DrawRoundImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), Rect, Radius, true);

            if (!DesignMode && EnableEditingColors && CursorOverWindowAccent) DrawEditorHighlight_Flat(G, Rect);

            DrawTaskbar7_EdgeLines(G);
            G.DrawImage(Win7Preview.AeroPeek, new Rectangle(Width - 10, 0, 10, Height));
            DrawTaskbar7_AppButtons(G);
        }

        private void Paint_Taskbar7Opaque(Graphics G, Rectangle Rect, Rectangle RRect, int Radius)
        {
            using (SolidBrush br = new(Color.White)) G.FillRectangle(br, Rect);

            using (SolidBrush br = new(Color.FromArgb((int)(255 * (Win7ColorBal / 100f)), Background))) G.FillRectangle(br, Rect);

            G.DrawImage(Win7Preview.TaskbarSides, Rect);

            if (Noise7Start != null) G.DrawRoundImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), Rect, Radius, true);

            if (!DesignMode && EnableEditingColors && CursorOverWindowAccent) DrawEditorHighlight_Flat(G, Rect);

            DrawTaskbar7_EdgeLines(G);
            G.DrawImage(Win7Preview.AeroPeek, new Rectangle(Width - 10, 0, 10, Height));
            DrawTaskbar7_AppButtons(G);
        }

        private void Paint_Taskbar7Basic(Graphics G, Rectangle Rect, Rectangle RRect)
        {
            G.DrawImage(Win7Preview.BasicTaskbar, Rect);
            G.DrawImage(Win7Preview.AeroPeek, new Rectangle(Width - 10, 0, 10, Height));
            DrawTaskbar7_AppButtons(G);
        }

        // The two horizontal lines that appear at the top of the Win7 taskbar
        private void DrawTaskbar7_EdgeLines(Graphics G)
        {
            using (Pen P = new(Color.FromArgb(80, 0, 0, 0))) G.DrawLine(P, new Point(0, 0), new Point(Width - 1, 0));

            using (Pen P = new(Color.FromArgb(80, 255, 255, 255))) G.DrawLine(P, new Point(0, 1), new Point(Width - 1, 1));
        }

        // ORB + two app buttons shared by all three Win7 taskbar variants
        private void DrawTaskbar7_AppButtons(Graphics G)
        {
            using Bitmap StartORB = new(Win7Preview.ORB);
            Rectangle StartBtnRect = new(3, -3, 39, 39);

            Rectangle AppBtnRect = new(StartBtnRect.Right + 5, 0, 43, 34);
            Rectangle AppBtnImgRect = new(AppBtnRect.X + (AppBtnRect.Width - Resources.SampleApp_Active.Width) / 2,
                AppBtnRect.Y + (AppBtnRect.Height - Resources.SampleApp_Active.Height) / 2 - 1,
                Resources.SampleApp_Active.Width, Resources.SampleApp_Active.Height);

            Rectangle App2BtnRect = new(AppBtnRect.Right + 1, 0, 43, 34);
            Rectangle App2BtnImgRect = new(App2BtnRect.X + (App2BtnRect.Width - Resources.SampleApp_Inactive.Width) / 2,
                App2BtnRect.Y + (App2BtnRect.Height - Resources.SampleApp_Inactive.Height) / 2 - 1,
                Resources.SampleApp_Inactive.Width, Resources.SampleApp_Inactive.Height);

            G.DrawImage(StartORB, StartBtnRect);

            using (Pen P = new(Color.FromArgb(150, 0, 0, 0))) G.DrawRoundedRect(P, new Rectangle(AppBtnRect.X, AppBtnRect.Y, AppBtnRect.Width - 2, AppBtnRect.Height - 2), 2, true);

            G.DrawImage(Win7Preview.Taskbar_ActiveApp, AppBtnRect);
            G.DrawImage(Resources.SampleApp_Active, AppBtnImgRect);

            using (Pen P = new(Color.FromArgb(110, 0, 0, 0))) G.DrawRoundedRect(P, new Rectangle(App2BtnRect.X, App2BtnRect.Y, App2BtnRect.Width - 2, App2BtnRect.Height - 2), 2, true);

            G.DrawImage(Win7Preview.Taskbar_InactiveApp, App2BtnRect);
            G.DrawImage(Resources.SampleApp_Inactive, App2BtnImgRect);
        }

        private void Paint_AltTab7Aero(Graphics G, Rectangle Rect, Rectangle RRect, int Radius)
        {
            if (Shadow && !DesignMode) G.DrawGlow(RRect, Color.FromArgb(150, 0, 0, 0), 5, 15);

            Rectangle inner = new(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2);

            if (!DesignMode && back_blurred != null)
            {
                float alpha = ClampAlpha(1 - BackColorAlpha / 100f);
                float ColBal = Win7ColorBal / 100f;
                float GlowBal = Win7GlowBal / 100f;
                G.DrawAeroEffect(RRect, back_blurred, Background, ColBal, Background2, GlowBal, alpha, Radius, true);
            }

            if (Noise7Start != null) G.DrawRoundImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), RRect, Radius, true);

            using (Pen P = new(Color.FromArgb(200, 25, 25, 25))) G.DrawRoundedRect(P, RRect, Radius, true);
            using (Pen P = new(Color.FromArgb(70, 200, 200, 200))) G.DrawRoundedRect(P, inner, Radius, true);

            DrawAltTab7_Thumbnails(G, RRect, Radius, accentColor: Background, useAeroSurround: true);
        }

        private void Paint_AltTab7Opaque(Graphics G, Rectangle Rect, Rectangle RRect, int Radius)
        {
            if (Shadow && !DesignMode) G.DrawGlow(RRect, Color.FromArgb(150, 0, 0, 0), 5, 15);

            Rectangle inner = new(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2);

            using (SolidBrush br = new(Color.White)) G.FillRoundedRect(br, RRect, Radius, true);

            using (SolidBrush br = new(Color.FromArgb((int)(255 * (Win7ColorBal / 100f)), Background))) G.FillRoundedRect(br, RRect, Radius, true);

            if (Noise7Start != null) G.DrawRoundImage(Noise7.Clone(Bounds, PixelFormat.Format32bppArgb), RRect, Radius, true);

            using (Pen P = new(Color.FromArgb(200, 25, 25, 25))) G.DrawRoundedRect(P, RRect, Radius, true);
            using (Pen P = new(Color.FromArgb(70, 200, 200, 200))) G.DrawRoundedRect(P, inner, Radius, true);

            DrawAltTab7_Thumbnails(G, RRect, Radius, accentColor: Background, useAeroSurround: false);
        }

        // Renders the thumbnail strip and glow-text label shared by Aero/Opaque Alt+Tab dialogs.
        // accentColor is used for the selection border; useAeroSurround selects which color
        // the selection highlight pen uses (Background vs. a fixed Aero blue).
        private void DrawAltTab7_Thumbnails(Graphics G, Rectangle RRect, int Radius, Color accentColor, bool useAeroSurround)
        {
            var (rects, padding, AppHeight) = BuildAltTab7Rects(RRect, 3);

            for (int i = 0; i < rects.Count; i++)
            {
                Rectangle r = rects[i];

                if (i == 0)
                {
                    Rectangle surround = new(r.X - 10, r.Y - 10, r.Width + 20, r.Height + 20);

                    using (SolidBrush br = new(Color.FromArgb(75, 200, 200, 200))) G.FillRoundedRect(br, surround, 1, true);

                    using Bitmap b0 = Win7Preview.TitleTopL.Fade(0.35f);
                    using Bitmap b1 = Win7Preview.TitleTopR.Fade(0.35f);
                    G.DrawRoundImage(b0, surround, 2, true);
                    G.DrawRoundImage(b1, surround, 2, true);

                    // Aero uses the window accent color; Opaque uses the background color
                    Color surroundPenColor = useAeroSurround ? Background : accentColor;
                    using (Pen P = new(surroundPenColor)) G.DrawRoundedRect(P, surround, 1, true);

                    using (Pen P = new(Color.FromArgb(229, 240, 250))) G.DrawRectangle(P, new Rectangle(surround.X + 1, surround.Y + 1, surround.Width - 2, surround.Height - 2));
                }

                G.FillRoundedRect(Brushes.White, r, 2, true);
                G.DrawRoundedRect(Pens.Black, r, 2, true);

                int icon_w = Resources.SampleApp_Active.Width;
                Rectangle icon_rect = new(r.X + r.Width - (int)(0.7 * icon_w), r.Y + r.Height - (int)(0.6 * icon_w), icon_w, icon_w);

                G.DrawImage(i == 0 ? Resources.SampleApp_Active : Resources.SampleApp_Inactive, icon_rect);
            }

            Rectangle TextRect = new(RRect.X + padding, RRect.Y, RRect.Width - 2 * padding, AppHeight * 2 / 5);
            using StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat();
            G.DrawGlowString(2, Program.Localization.Strings.Previewer.AppPreview, Font, Color.Black, Color.FromArgb(185, 225, 225, 225), RRect, TextRect, sf);
        }

        private void Paint_AltTab7Basic(Graphics G, Rectangle Rect, Rectangle RRect)
        {
            // Reproduces the Win7 Basic (non-Aero) dialog chrome
            Color Titlebar_Background1 = Color.FromArgb(152, 180, 208);
            Color Titlebar_BackColor2 = Color.FromArgb(186, 210, 234);
            Color Titlebar_OuterBorder = Color.FromArgb(52, 52, 52);
            Color Titlebar_InnerBorder = Color.FromArgb(255, 255, 255);
            Color Titlebar_Turquoise = Color.FromArgb(40, 207, 228);
            Color OuterBorder = Color.FromArgb(0, 0, 0);

            Rectangle UpperPart = new(RRect.X, RRect.Y, RRect.Width + 1, 25);

            using LinearGradientBrush pth_back = new(UpperPart, Titlebar_Background1, Titlebar_BackColor2, LinearGradientMode.Vertical);
            using LinearGradientBrush pth_line = new(UpperPart, Titlebar_InnerBorder, Titlebar_Turquoise, LinearGradientMode.Vertical);

            G.SetClip(UpperPart);
            G.FillRectangle(pth_back, RRect);

            using (Pen P = new(Titlebar_OuterBorder)) G.DrawRectangle(P, RRect);
            using (Pen P = new(Titlebar_InnerBorder)) G.DrawRectangle(P, new Rectangle(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2));

            // The turquoise gradient line only appears in the right quarter of the title bar
            G.SetClip(new Rectangle(UpperPart.X + (int)(UpperPart.Width * 0.75), UpperPart.Y, (int)(UpperPart.Width * 0.75), UpperPart.Height));
            using (Pen P = new(pth_line)) G.DrawRectangle(P, new Rectangle(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2));

            G.ResetClip();
            G.ExcludeClip(UpperPart);

            using (SolidBrush br = new(Titlebar_BackColor2)) G.FillRectangle(br, RRect);
            using (Pen P = new(Titlebar_Turquoise)) G.DrawRectangle(P, new Rectangle(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2));
            using (Pen P = new(OuterBorder)) G.DrawRectangle(P, RRect);

            G.ResetClip();
            using (Pen P = new(Color.FromArgb(52, 52, 52))) G.DrawRectangle(P, RRect);
            using (Pen P = new(Color.FromArgb(255, 225, 225, 225))) G.DrawRectangle(P, new Rectangle(RRect.X + 1, RRect.Y + 1, RRect.Width - 2, RRect.Height - 2));

            // App icon buttons at the bottom of the dialog
            int _padding = 5;
            int AppHeight = Win7Preview.AltTabBasicButton.Height;
            int AppWidth = Win7Preview.AltTabBasicButton.Width;
            int _paddingOuter = (RRect.Width - AppWidth * 3 - _padding * 2) / 2;

            List<Rectangle> rects = new(3);
            for (int i = 0; i < 3; i++)
            {
                int x = i == 0 ? RRect.X + _paddingOuter : rects[i - 1].Right + _padding;
                rects.Add(new Rectangle(x, RRect.Y + RRect.Height - 5 - AppHeight, AppWidth, AppHeight));
            }

            for (int i = 0; i < rects.Count; i++)
            {
                Rectangle r = rects[i];

                if (i == 0) G.DrawImage(Win7Preview.AltTabBasicButton, r);

                Rectangle imgrect = new(r.X + (r.Width - Resources.SampleApp_Active.Width) / 2, r.Y + (r.Height - Resources.SampleApp_Active.Height) / 2, Resources.SampleApp_Active.Width, Resources.SampleApp_Active.Height);

                G.DrawImage(i == 0 ? Resources.SampleApp_Active : Resources.SampleApp_Inactive, imgrect);
            }

            Rectangle TextRect = new(RRect.X + _padding, RRect.Y, RRect.Width - 2 * _padding, 30);
            using StringFormat sf = ContentAlignment.MiddleCenter.ToStringFormat();
            G.DrawString(Program.Localization.Strings.Previewer.AppPreview, Font, Brushes.Black, TextRect, sf);
        }

        #endregion

        #region Windows Vista Paint Methods

        private void Paint_StartVistaAero(Graphics G, Rectangle Rect, Rectangle RRect)
        {
            Rectangle RestRect = new(0, 14, Width - 6, Height - 14);

            // Clip to RestRect before drawing the blurred wallpaper to avoid bleeding into the
            // rounded top portion of the Start popup
            G.SetClip(RestRect);
            if (!DesignMode && back_blurred != null) G.DrawImage(back_blurred, Rect);
            G.ResetClip();

            using (SolidBrush br = new(Color.FromArgb((int)(Win7ColorBal * 255f / 100f), Background))) G.FillRoundedRect(br, RestRect, 4, true);

            if (!DesignMode && EnableEditingColors && CursorOverWindowAccent) DrawEditorHighlight_Rounded(G, RestRect, 5);

            G.DrawImage(WinVistaPreview.Start, new Rectangle(0, 0, Width, Height));
        }

        private void Paint_StartVistaOpaque(Graphics G, Rectangle Rect, Rectangle RRect)
        {
            Rectangle RestRect = new(0, 14, Width - 6, Height - 14);

            G.FillRoundedRect(Brushes.White, RestRect, 4, true);

            using (SolidBrush br = new(Color.FromArgb((int)(Win7ColorBal * 255f / 100f), Background))) G.FillRoundedRect(br, RestRect, 4, true);

            if (!DesignMode && EnableEditingColors && CursorOverWindowAccent) DrawEditorHighlight_Rounded(G, RestRect, 5);

            G.DrawImage(WinVistaPreview.Start, new Rectangle(0, 0, Width, Height));
        }

        private void Paint_StartVistaBasic(Graphics G, Rectangle Rect)
        {
            G.DrawImage(WinVistaPreview.StartBasic, new Rectangle(0, 0, Width, Height));
        }

        private void Paint_TaskbarVistaAero(Graphics G, Rectangle Rect, Rectangle RRect)
        {
            if (!DesignMode && back_blurred != null) G.DrawImage(back_blurred, Rect);

            using (SolidBrush br = new(Color.FromArgb((int)(Win7ColorBal * 255f / 100f), Background))) G.FillRectangle(br, Rect);

            using (TextureBrush tb = new(WinVistaPreview.Taskbar)) G.FillRectangle(tb, Rect);

            if (!DesignMode && EnableEditingColors && CursorOverWindowAccent) DrawEditorHighlight_Flat(G, Rect);

            DrawTaskbarVista_Content(G, Rect);
        }

        private void Paint_TaskbarVistaOpaque(Graphics G, Rectangle Rect, Rectangle RRect)
        {
            G.FillRectangle(Brushes.White, Rect);

            using (SolidBrush br = new(Color.FromArgb((int)(Win7ColorBal * 255f / 100f), Background))) G.FillRectangle(br, Rect);

            using (TextureBrush tb = new(WinVistaPreview.Taskbar)) G.FillRectangle(tb, Rect);

            if (!DesignMode && EnableEditingColors && CursorOverWindowAccent) DrawEditorHighlight_Flat(G, Rect);

            DrawTaskbarVista_Content(G, Rect);
        }

        private void Paint_TaskbarVistaBasic(Graphics G, Rectangle Rect)
        {
            G.FillRectangle(Brushes.Black, Rect);
            using (TextureBrush tb = new(WinVistaPreview.Taskbar)) G.FillRectangle(tb, Rect);
            DrawTaskbarVista_Content(G, Rect);
        }

        // ORB, app buttons, and text labels shared across all three Vista taskbar variants
        private void DrawTaskbarVista_Content(Graphics G, Rectangle Rect)
        {
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

            using StringFormat sf = ContentAlignment.MiddleLeft.ToStringFormat();
            G.DrawString(Program.Localization.Strings.Previewer.AppPreview, Font, Brushes.White, appLabel1, sf);
            G.DrawString(Program.Localization.Strings.Previewer.InactiveApp, Font, Brushes.White, appLabel2, sf);
        }

        #endregion

        #region Windows XP Paint Methods

        private void Paint_TaskbarXP(Graphics G, Rectangle Rect)
        {
            SmoothingMode previous = G.SmoothingMode;
            G.SmoothingMode = SmoothingMode.HighSpeed;
            resVS?.Draw(G, Rect, VisualStylesRes.Element.Taskbar, true, false);
            G.SmoothingMode = previous;
        }

        #endregion
    }
}