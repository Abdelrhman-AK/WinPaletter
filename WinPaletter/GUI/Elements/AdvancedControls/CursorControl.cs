using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter.UI.Controllers
{
    public class CursorControl : ContainerControl
    {
        public CursorControl()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
        }

        #region Variables
        private bool CanAnimate => !DesignMode && Program.Style.Animations && this != null && Visible && Parent != null && Parent.Visible && FindForm() != null && FindForm().Visible;

        private Bitmap bmp;
        public float Angle = 180f;

        public MouseState State = MouseState.None;

        public enum MouseState
        {
            None,
            Over,
            Down
        }

        #endregion

        #region Properties
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public new Color BackColor { get => Color.Transparent; set {; } }

        private string _prop_File = string.Empty;
        public string Prop_File
        {
            get => _prop_File;
            set
            {
                if (value != _prop_File)
                {
                    _prop_File = value;
                    Refresh();
                }
            }
        }

        private bool _prop_UseFromFile = false;
        public bool Prop_UseFromFile
        {
            get => _prop_UseFromFile;
            set
            {
                if (value != _prop_UseFromFile)
                {
                    _prop_UseFromFile = value;
                    Refresh();
                }
            }
        }

        private Paths.CursorType _prop_Cursor = Paths.CursorType.Arrow;
        public Paths.CursorType Prop_Cursor
        {
            get => _prop_Cursor;
            set
            {
                if (value != _prop_Cursor)
                {
                    _prop_Cursor = value;
                    Refresh();
                }
            }
        }

        private Paths.ArrowStyle _prop_ArrowStyle = Paths.ArrowStyle.Aero;
        public Paths.ArrowStyle Prop_ArrowStyle
        {
            get => _prop_ArrowStyle;
            set
            {
                if (value != _prop_ArrowStyle)
                {
                    _prop_ArrowStyle = value;
                    Refresh();
                }
            }
        }

        private Paths.CircleStyle _prop_CircleStyle = Paths.CircleStyle.Aero;
        public Paths.CircleStyle Prop_CircleStyle
        {
            get => _prop_CircleStyle;
            set
            {
                if (value != _prop_CircleStyle)
                {
                    _prop_CircleStyle = value;
                    Refresh();
                }
            }
        }

        private Color _prop_PrimaryColor1 = Color.White;
        public Color Prop_PrimaryColor1
        {
            get => _prop_PrimaryColor1;
            set
            {
                if (value != _prop_PrimaryColor1)
                {
                    _prop_PrimaryColor1 = value;
                    Refresh();
                }
            }
        }

        private Color _prop_PrimaryColor2 = Color.White;
        public Color Prop_PrimaryColor2
        {
            get => _prop_PrimaryColor2;
            set
            {
                if (value != _prop_PrimaryColor2)
                {
                    _prop_PrimaryColor2 = value;
                    Refresh();
                }
            }
        }

        private bool _prop_PrimaryColorGradient = false;
        public bool Prop_PrimaryColorGradient
        {
            get => _prop_PrimaryColorGradient;
            set
            {
                if (value != _prop_PrimaryColorGradient)
                {
                    _prop_PrimaryColorGradient = value;
                    Refresh();
                }
            }
        }

        private Paths.GradientMode _prop_PrimaryColorGradientMode = Paths.GradientMode.Vertical;
        public Paths.GradientMode Prop_PrimaryColorGradientMode
        {
            get => _prop_PrimaryColorGradientMode;
            set
            {
                if (value != _prop_PrimaryColorGradientMode)
                {
                    _prop_PrimaryColorGradientMode = value;
                    Refresh();
                }
            }
        }

        private bool _prop_PrimaryNoise = false;
        public bool Prop_PrimaryNoise
        {
            get => _prop_PrimaryNoise;
            set
            {
                if (value != _prop_PrimaryNoise)
                {
                    _prop_PrimaryNoise = value;
                    Refresh();
                }
            }
        }

        private float _prop_PrimaryNoiseOpacity = 0.25f;
        public float Prop_PrimaryNoiseOpacity
        {
            get => _prop_PrimaryNoiseOpacity;
            set
            {
                if (value != _prop_PrimaryNoiseOpacity)
                {
                    _prop_PrimaryNoiseOpacity = value;
                    Refresh();
                }
            }
        }

        private Color _prop_SecondaryColor1 = Color.FromArgb(64, 65, 75);
        public Color Prop_SecondaryColor1
        {
            get => _prop_SecondaryColor1;
            set
            {
                if (value != _prop_SecondaryColor1)
                {
                    _prop_SecondaryColor1 = value;
                    Refresh();
                }
            }
        }

        private Color _prop_SecondaryColor2 = Color.FromArgb(64, 65, 75);
        public Color Prop_SecondaryColor2
        {
            get => _prop_SecondaryColor2;
            set
            {
                if (value != _prop_SecondaryColor2)
                {
                    _prop_SecondaryColor2 = value;
                    Refresh();
                }
            }
        }

        private bool _prop_SecondaryColorGradient = false;
        public bool Prop_SecondaryColorGradient
        {
            get => _prop_SecondaryColorGradient;
            set
            {
                if (value != _prop_SecondaryColorGradient)
                {
                    _prop_SecondaryColorGradient = value;
                    Refresh();
                }
            }
        }

        private Paths.GradientMode _prop_SecondaryColorGradientMode = Paths.GradientMode.Vertical;
        public Paths.GradientMode Prop_SecondaryColorGradientMode
        {
            get => _prop_SecondaryColorGradientMode;
            set
            {
                if (value != _prop_SecondaryColorGradientMode)
                {
                    _prop_SecondaryColorGradientMode = value;
                    Refresh();
                }
            }
        }

        private bool _prop_SecondaryNoise = false;
        public bool Prop_SecondaryNoise
        {
            get => _prop_SecondaryNoise;
            set
            {
                if (value != _prop_SecondaryNoise)
                {
                    _prop_SecondaryNoise = value;
                    Refresh();
                }
            }
        }

        private float _prop_SecondaryNoiseOpacity = 0.25f;
        public float Prop_SecondaryNoiseOpacity
        {
            get => _prop_SecondaryNoiseOpacity;
            set
            {
                if (value != _prop_SecondaryNoiseOpacity)
                {
                    _prop_SecondaryNoiseOpacity = value;
                    Refresh();
                }
            }
        }

        private Color _prop_LoadingCircleBack1 = Color.FromArgb(42, 151, 243);
        public Color Prop_LoadingCircleBack1
        {
            get => _prop_LoadingCircleBack1;
            set
            {
                if (value != _prop_LoadingCircleBack1)
                {
                    _prop_LoadingCircleBack1 = value;
                    Refresh();
                }
            }
        }

        private Color _prop_LoadingCircleBack2 = Color.FromArgb(42, 151, 243);
        public Color Prop_LoadingCircleBack2
        {
            get => _prop_LoadingCircleBack2;
            set
            {
                if (value != _prop_LoadingCircleBack2)
                {
                    _prop_LoadingCircleBack2 = value;
                    Refresh();
                }
            }
        }

        private bool _prop_LoadingCircleBackGradient = false;
        public bool Prop_LoadingCircleBackGradient
        {
            get => _prop_LoadingCircleBackGradient;
            set
            {
                if (value != _prop_LoadingCircleBackGradient)
                {
                    _prop_LoadingCircleBackGradient = value;
                    Refresh();
                }
            }
        }

        private Paths.GradientMode _prop_LoadingCircleBackGradientMode = Paths.GradientMode.Vertical;
        public Paths.GradientMode Prop_LoadingCircleBackGradientMode
        {
            get => _prop_LoadingCircleBackGradientMode;
            set
            {
                if (value != _prop_LoadingCircleBackGradientMode)
                {
                    _prop_LoadingCircleBackGradientMode = value;
                    Refresh();
                }
            }
        }

        private bool _prop_LoadingCircleBackNoise = false;
        public bool Prop_LoadingCircleBackNoise
        {
            get => _prop_LoadingCircleBackNoise;
            set
            {
                if (value != _prop_LoadingCircleBackNoise)
                {
                    _prop_LoadingCircleBackNoise = value;
                    Refresh();
                }
            }
        }

        private float _prop_LoadingCircleBackNoiseOpacity = 0.25f;
        public float Prop_LoadingCircleBackNoiseOpacity
        {
            get => _prop_LoadingCircleBackNoiseOpacity;
            set
            {
                if (value != _prop_LoadingCircleBackNoiseOpacity)
                {
                    _prop_LoadingCircleBackNoiseOpacity = value;
                    Refresh();
                }
            }
        }

        private Color _prop_LoadingCircleHot1 = Color.FromArgb(37, 204, 255);
        public Color Prop_LoadingCircleHot1
        {
            get => _prop_LoadingCircleHot1;
            set
            {
                if (value != _prop_LoadingCircleHot1)
                {
                    _prop_LoadingCircleHot1 = value;
                    Refresh();
                }
            }
        }

        private Color _prop_LoadingCircleHot2 = Color.FromArgb(37, 204, 255);
        public Color Prop_LoadingCircleHot2
        {
            get => _prop_LoadingCircleHot2;
            set
            {
                if (value != _prop_LoadingCircleHot2)
                {
                    _prop_LoadingCircleHot2 = value;
                    Refresh();
                }
            }
        }

        private bool _prop_LoadingCircleHotGradient = false;
        public bool Prop_LoadingCircleHotGradient
        {
            get => _prop_LoadingCircleHotGradient;
            set
            {
                if (value != _prop_LoadingCircleHotGradient)
                {
                    _prop_LoadingCircleHotGradient = value;
                    Refresh();
                }
            }
        }

        private Paths.GradientMode _prop_LoadingCircleHotGradientMode = Paths.GradientMode.Vertical;
        public Paths.GradientMode Prop_LoadingCircleHotGradientMode
        {
            get => _prop_LoadingCircleHotGradientMode;
            set
            {
                if (value != _prop_LoadingCircleHotGradientMode)
                {
                    _prop_LoadingCircleHotGradientMode = value;
                    Refresh();
                }
            }
        }

        private bool _prop_LoadingCircleHotNoise = false;
        public bool Prop_LoadingCircleHotNoise
        {
            get => _prop_LoadingCircleHotNoise;
            set
            {
                if (value != _prop_LoadingCircleHotNoise)
                {
                    _prop_LoadingCircleHotNoise = value;
                    Refresh();
                }
            }
        }

        private float _prop_LoadingCircleHotNoiseOpacity = 0.25f;
        public float Prop_LoadingCircleHotNoiseOpacity
        {
            get => _prop_LoadingCircleHotNoiseOpacity;
            set
            {
                if (value != _prop_LoadingCircleHotNoiseOpacity)
                {
                    _prop_LoadingCircleHotNoiseOpacity = value;
                    Refresh();
                }
            }
        }

        private bool _prop_Shadow_Enabled = false;
        public bool Prop_Shadow_Enabled
        {
            get => _prop_Shadow_Enabled;
            set
            {
                if (value != _prop_Shadow_Enabled)
                {
                    _prop_Shadow_Enabled = value;
                    Refresh();
                }
            }
        }

        private Color _prop_Shadow_Color = Color.Black;
        public Color Prop_Shadow_Color
        {
            get => _prop_Shadow_Color;
            set
            {
                if (value != _prop_Shadow_Color)
                {
                    _prop_Shadow_Color = value;
                    Refresh();
                }
            }
        }

        private int _prop_Shadow_Blur = 5;
        public int Prop_Shadow_Blur
        {
            get => _prop_Shadow_Blur;
            set
            {
                if (value != _prop_Shadow_Blur)
                {
                    _prop_Shadow_Blur = value;
                    Refresh();
                }
            }
        }

        private float _prop_Shadow_Opacity = 0.3f;
        public float Prop_Shadow_Opacity
        {
            get => _prop_Shadow_Opacity;
            set
            {
                if (value != _prop_Shadow_Opacity)
                {
                    _prop_Shadow_Opacity = value;
                    Refresh();
                }
            }
        }

        private int _prop_Shadow_OffsetX = 2;
        public int Prop_Shadow_OffsetX
        {
            get => _prop_Shadow_OffsetX;
            set
            {
                if (value != _prop_Shadow_OffsetX)
                {
                    _prop_Shadow_OffsetX = value;
                    Refresh();
                }
            }
        }

        private int _prop_Shadow_OffsetY = 2;
        public int Prop_Shadow_OffsetY
        {
            get => _prop_Shadow_OffsetY;
            set
            {
                if (value != _prop_Shadow_OffsetY)
                {
                    _prop_Shadow_OffsetY = value;
                    Refresh();
                }
            }
        }

        private float _prop_Scale = 1f;
        public float Prop_Scale
        {
            get => _prop_Scale;
            set
            {
                if (value != _prop_Scale)
                {
                    _prop_Scale = value;
                    Refresh();
                }
            }
        }

        private bool _focused = false;
        public bool Focused
        {
            get => _focused;
            set
            {
                if (_focused != value)
                {
                    _focused = value;

                    if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha2), _focused ? 255 : 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
                    else { alpha2 = _focused ? 255 : 0; }
                }
            }
        }

        private int _focusAlpha = 255;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int FocusAlpha
        {
            get => _focusAlpha;
            set
            {
                _focusAlpha = value;
                Refresh();
            }
        }
        #endregion

        #region Events/Overrides
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focused = true;
            State = MouseState.Down;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }

            base.OnMouseDown(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (CanAnimate) Invalidate();

            base.OnSizeChanged(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            State = MouseState.Over;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 255; }

            base.OnMouseUp(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            State = MouseState.Over;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 255; }

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            State = MouseState.None;

            if (CanAnimate) { FluentTransitions.Transition.With(this, nameof(alpha), 0).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration)); }
            else { alpha = 0; }

            base.OnMouseLeave(e);
        }

        protected override void OnClick(EventArgs e)
        {
            foreach (CursorControl c in Parent.Controls.OfType<CursorControl>())
            {
                if (c == this)
                {
                    c.Focused = true;
                    c.Invalidate();
                }
                else
                {
                    c.Focused = false;
                    c.Invalidate();
                }
            }

            base.OnClick(e);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            if (FindForm() != null)
            {
                FindForm().Activated += Form_Activated;
                FindForm().Deactivate += Form_Deactivate; ;
            }

            base.OnHandleCreated(e);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (FindForm() != null)
            {
                FindForm().Activated -= Form_Activated;
                FindForm().Deactivate -= Form_Deactivate; ;
            }

            base.OnHandleDestroyed(e);
        }

        private void Form_Activated(object sender, EventArgs e)
        {
            if (CanAnimate)
            {
                FluentTransitions.Transition.With(this, nameof(FocusAlpha), 255).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            }
            else
            {
                FocusAlpha = 255;
            }
        }

        private void Form_Deactivate(object sender, EventArgs e)
        {
            if (CanAnimate)
            {
                FluentTransitions.Transition.With(this, nameof(FocusAlpha), 100).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            }
            else
            {
                FocusAlpha = 100;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            bmp?.Dispose();
        }

        #endregion

        #region Animator
        private int _alpha = 0;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int alpha
        {
            get => _alpha;
            set { _alpha = value; Refresh(); }
        }

        private int _alpha2 = 0;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int alpha2
        {
            get => _alpha2;
            set { _alpha2 = value; Refresh(); }
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

            //Makes background drawn properly, and transparent
            InvokePaintBackground(this, e);

            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            CursorOptions CurOptions = new()
            {
                UseFromFile = Prop_UseFromFile,
                File = Prop_File,
                Cursor = Prop_Cursor,
                ArrowStyle = Prop_ArrowStyle,
                CircleStyle = Prop_CircleStyle,
                PrimaryColor1 = Prop_PrimaryColor1,
                PrimaryColor2 = Prop_PrimaryColor2,
                PrimaryColorGradient = Prop_PrimaryColorGradient,
                PrimaryColorGradientMode = Prop_PrimaryColorGradientMode,
                SecondaryColor1 = Prop_SecondaryColor1,
                SecondaryColor2 = Prop_SecondaryColor2,
                SecondaryColorGradient = Prop_SecondaryColorGradient,
                SecondaryColorGradientMode = Prop_SecondaryColorGradientMode,
                LoadingCircleBack1 = Prop_LoadingCircleBack1,
                LoadingCircleBack2 = Prop_LoadingCircleBack2,
                LoadingCircleBackGradient = Prop_LoadingCircleBackGradient,
                LoadingCircleBackGradientMode = Prop_LoadingCircleBackGradientMode,
                LoadingCircleHot1 = Prop_LoadingCircleHot1,
                LoadingCircleHot2 = Prop_LoadingCircleHot2,
                LoadingCircleHotGradient = Prop_LoadingCircleHotGradient,
                LoadingCircleHotGradientMode = Prop_LoadingCircleHotGradientMode,
                PrimaryNoise = Prop_PrimaryNoise,
                PrimaryNoiseOpacity = Prop_PrimaryNoiseOpacity,
                SecondaryNoise = Prop_SecondaryNoise,
                SecondaryNoiseOpacity = Prop_SecondaryNoiseOpacity,
                LoadingCircleBackNoise = Prop_LoadingCircleBackNoise,
                LoadingCircleBackNoiseOpacity = Prop_LoadingCircleBackNoiseOpacity,
                LoadingCircleHotNoise = Prop_LoadingCircleHotNoise,
                LoadingCircleHotNoiseOpacity = Prop_LoadingCircleHotNoiseOpacity,
                LineThickness = 1f,
                Scale = Prop_Scale,
                Angle = Angle,
                Shadow_Enabled = Prop_Shadow_Enabled,
                Shadow_Blur = Prop_Shadow_Blur,
                Shadow_Color = Prop_Shadow_Color,
                Shadow_Opacity = Prop_Shadow_Opacity,
                Shadow_OffsetX = Prop_Shadow_OffsetX,
                Shadow_OffsetY = Prop_Shadow_OffsetY
            };

            bmp = new((int)Math.Round(32f * Prop_Scale), (int)Math.Round(32f * Prop_Scale), PixelFormat.Format32bppPArgb);

            bmp = Paths.Draw(CurOptions);

            Rectangle MainRect = new(0, 0, Width - 1, Height - 1);
            Rectangle MainRectInner = new(1, 1, Width - 3, Height - 3);
            Rectangle CenterRect = new((int)Math.Round(MainRect.X + (MainRect.Width - bmp.Width) / 2d), (int)Math.Round(MainRect.Y + (MainRect.Height - bmp.Height) / 2d), bmp.Width, bmp.Height);

            Color back = Color.FromArgb(alpha, scheme.Colors.Back_Checked_Hover);
            Color line = Color.FromArgb(255 - alpha, Focused ? scheme.Colors.Line_Checked : State != MouseState.Over ? scheme.Colors.Line : scheme.Colors.Line_Checked_Hover);
            Color line_hover = Color.FromArgb(alpha, scheme.Colors.Line_Checked_Hover);

            using (SolidBrush br = new(Color.FromArgb(_alpha2, scheme.Colors.Back_Checked))) { G.FillRoundedRect(br, MainRect); }

            using (SolidBrush br = new(Color.FromArgb(Math.Max(FocusAlpha - _alpha2, 0), scheme.Colors.Back))) { G.FillRoundedRect(br, MainRectInner); }

            using (SolidBrush br = new(back)) { G.FillRoundedRect(br, MainRect); }

            using (Pen P = new(line)) { G.DrawRoundedRect_LikeW11(P, MainRectInner); }

            using (Pen P = new(line_hover)) { G.DrawRoundedRect_LikeW11(P, MainRect); }

            if (CurOptions.UseFromFile && System.IO.File.Exists(CurOptions.File) && System.IO.Path.GetExtension(CurOptions.File).ToUpper() == ".ANI")
            {
                float _Angle = 0;
                if (CurOptions.Angle >= 180) { _Angle = CurOptions.Angle - 180f; }
                else if (CurOptions.Angle < 180) { _Angle = CurOptions.Angle + 180f; }

                int frames = GetTotalFramesFromANI(CurOptions.File);

                Size size = new((int)(CurOptions.Scale * 32), (int)(CurOptions.Scale * 32));
                Point location = new((Width - size.Width) / 2 - 1, (Height - size.Height) / 2 - 1);

                int AngleToFrame = (int)(_Angle / 360 * frames);

                IntPtr hCursor = User32.LoadCursorFromFile(CurOptions.File);

                IntPtr hdc = G.GetHdc();
                User32.DrawIconEx(hdc, location.X, location.Y, hCursor, size.Width, size.Height, AngleToFrame, IntPtr.Zero, (int)(User32.DrawIconExFlags.DI_NORMAL | User32.DrawIconExFlags.DI_COMPAT));
                G.ReleaseHdc(hdc);
                User32.DestroyIcon(hCursor);
            }
            else
            {
                G.DrawImage(bmp, CenterRect);
            }

            base.OnPaint(e);
        }

        private static int GetTotalFramesFromANI(string filePath)
        {
            using (FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read))
            {
                // ANI file format: https://www.daubnet.com/en/file-format-ani
                // Skip to the position where the number of frames is stored
                fileStream.Seek(6, SeekOrigin.Begin);

                // Read the number of frames (little-endian format)
                int totalFrames = fileStream.ReadByte() | (fileStream.ReadByte() << 8);

                return totalFrames * 2 + 1;
            }
        }
    }
}