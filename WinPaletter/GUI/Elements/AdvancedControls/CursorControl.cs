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

        public string Prop_File { get; set; } = string.Empty;
        public bool Prop_UseFromFile { get; set; } = false;
        public Paths.CursorType Prop_Cursor { get; set; } = Paths.CursorType.Arrow;
        public Paths.ArrowStyle Prop_ArrowStyle { get; set; } = Paths.ArrowStyle.Aero;
        public Paths.CircleStyle Prop_CircleStyle { get; set; } = Paths.CircleStyle.Aero;
        public Color Prop_PrimaryColor1 { get; set; } = Color.White;
        public Color Prop_PrimaryColor2 { get; set; } = Color.White;
        public bool Prop_PrimaryColorGradient { get; set; } = false;
        public Paths.GradientMode Prop_PrimaryColorGradientMode { get; set; } = Paths.GradientMode.Vertical;
        public bool Prop_PrimaryNoise { get; set; } = false;
        public float Prop_PrimaryNoiseOpacity { get; set; } = 0.25f;

        public Color Prop_SecondaryColor1 { get; set; } = Color.FromArgb(64, 65, 75);
        public Color Prop_SecondaryColor2 { get; set; } = Color.FromArgb(64, 65, 75);
        public bool Prop_SecondaryColorGradient { get; set; } = false;
        public Paths.GradientMode Prop_SecondaryColorGradientMode { get; set; } = Paths.GradientMode.Vertical;
        public bool Prop_SecondaryNoise { get; set; } = false;
        public float Prop_SecondaryNoiseOpacity { get; set; } = 0.25f;

        public Color Prop_LoadingCircleBack1 { get; set; } = Color.FromArgb(42, 151, 243);
        public Color Prop_LoadingCircleBack2 { get; set; } = Color.FromArgb(42, 151, 243);
        public bool Prop_LoadingCircleBackGradient { get; set; } = false;
        public Paths.GradientMode Prop_LoadingCircleBackGradientMode { get; set; } = Paths.GradientMode.Vertical;
        public bool Prop_LoadingCircleBackNoise { get; set; } = false;
        public float Prop_LoadingCircleBackNoiseOpacity { get; set; } = 0.25f;

        public Color Prop_LoadingCircleHot1 { get; set; } = Color.FromArgb(37, 204, 255);
        public Color Prop_LoadingCircleHot2 { get; set; } = Color.FromArgb(37, 204, 255);
        public bool Prop_LoadingCircleHotGradient { get; set; } = false;
        public Paths.GradientMode Prop_LoadingCircleHotGradientMode { get; set; } = Paths.GradientMode.Vertical;
        public bool Prop_LoadingCircleHotNoise { get; set; } = false;
        public float Prop_LoadingCircleHotNoiseOpacity { get; set; } = 0.25f;

        public bool Prop_Shadow_Enabled { get; set; } = false;
        public Color Prop_Shadow_Color { get; set; } = Color.Black;
        public int Prop_Shadow_Blur { get; set; } = 5;
        public float Prop_Shadow_Opacity { get; set; } = 0.3f;
        public int Prop_Shadow_OffsetX { get; set; } = 2;
        public int Prop_Shadow_OffsetY { get; set; } = 2;

        public float Prop_Scale { get; set; } = 1f;


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

        protected override void OnLocationChanged(EventArgs e)
        {
            if (CanAnimate) Invalidate();

            base.OnLocationChanged(e);
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

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this == null) return;

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

            bmp  = new((int)Math.Round(32f * Prop_Scale), (int)Math.Round(32f * Prop_Scale), PixelFormat.Format32bppPArgb);

            bmp = Paths.Draw(CurOptions);

            DoubleBuffered = true;

            Rectangle MainRect = new(0, 0, Width - 1, Height - 1);
            Rectangle MainRectInner = new(1, 1, Width - 3, Height - 3);
            Rectangle CenterRect = new((int)Math.Round(MainRect.X + (MainRect.Width - bmp.Width) / 2d), (int)Math.Round(MainRect.Y + (MainRect.Height - bmp.Height) / 2d), bmp.Width, bmp.Height);

            Color back = Color.FromArgb(alpha, scheme.Colors.Back_Checked_Hover);
            Color line = Color.FromArgb(255 - alpha, Focused ? scheme.Colors.Line_Checked : State != MouseState.Over ? scheme.Colors.Line : scheme.Colors.Line_Checked_Hover);
            Color line_hover = Color.FromArgb(alpha, scheme.Colors.Line_Checked_Hover);

            using (SolidBrush br = new(Color.FromArgb(_alpha2, scheme.Colors.Back_Checked))) { G.FillRoundedRect(br, MainRect); }

            using (SolidBrush br = new(Color.FromArgb(255 - _alpha2, scheme.Colors.Back))) { G.FillRoundedRect(br, MainRectInner); }

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