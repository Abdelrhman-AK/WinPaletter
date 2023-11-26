using System;
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
            Timer = new Timer() { Enabled = false, Interval = 1 };
            MouseEnter += CursorControl_MouseEnter;
            MouseLeave += CursorControl_MouseLeave;
            Click += CursorControl_Click;
            Timer.Tick += Timer_Tick;

        }

        #region Variables

        public bool _Focused = false;
        private Bitmap bmp;
        public float Angle = 180f;
        private bool AnimateOnClick = false;

        public MouseState State = MouseState.None;

        public enum MouseState
        {
            None,
            Over,
            Down
        }

        #endregion

        #region Properties
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

        #endregion

        #region Events

        protected override void OnMouseDown(MouseEventArgs e)
        {
            AnimateOnClick = true;
            _Focused = true;
            State = MouseState.Down;
            Timer.Enabled = true;
            Timer.Start();
            Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            State = MouseState.Over;
            Timer.Enabled = true;
            Timer.Start();
            Invalidate();
        }

        private void CursorControl_MouseEnter(object sender, EventArgs e)
        {
            State = MouseState.Over;
            Timer.Enabled = true;
            Timer.Start();
            Invalidate();
        }

        private void CursorControl_MouseLeave(object sender, EventArgs e)
        {
            State = MouseState.None;
            Timer.Enabled = true;
            Timer.Start();
            Invalidate();
        }

        private void CursorControl_Click(object sender, EventArgs e)
        {

            foreach (CursorControl c in Parent.Controls.OfType<CursorControl>())
            {
                if (c == sender)
                {
                    c._Focused = true;
                    c.Invalidate();
                }
                else
                {
                    c._Focused = false;
                    c.Invalidate();
                }
            }

        }

        #endregion

        #region Animator
        private int alpha;
        private readonly int Factor = 25;
        private Timer Timer;

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!DesignMode)
            {

                if (State == MouseState.Over)
                {
                    if (alpha + Factor <= 255)
                    {
                        alpha += Factor;
                    }
                    else if (alpha + Factor > 255)
                    {
                        alpha = 255;
                        Timer.Enabled = false;
                        Timer.Stop();
                        AnimateOnClick = false;
                    }

                    System.Threading.Thread.Sleep(1);
                    Invalidate();
                }

                if (!(State == MouseState.Over))
                {
                    if (alpha - Factor >= 0)
                    {
                        alpha -= Factor;
                    }
                    else if (alpha - Factor < 0)
                    {
                        alpha = 0;
                        Timer.Enabled = false;
                        Timer.Stop();
                        AnimateOnClick = false;
                    }

                    System.Threading.Thread.Sleep(1);
                    Invalidate();
                }
            }
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            var CurOptions = new CursorOptions()
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
                _Angle = Angle,
                Shadow_Enabled = Prop_Shadow_Enabled,
                Shadow_Blur = Prop_Shadow_Blur,
                Shadow_Color = Prop_Shadow_Color,
                Shadow_Opacity = Prop_Shadow_Opacity,
                Shadow_OffsetX = Prop_Shadow_OffsetX,
                Shadow_OffsetY = Prop_Shadow_OffsetY
            };

            bmp = new Bitmap((int)Math.Round(32f * Prop_Scale), (int)Math.Round(32f * Prop_Scale), PixelFormat.Format32bppPArgb);

            bmp = Paths.Draw(CurOptions);

            DoubleBuffered = true;

            var MainRect = new Rectangle(0, 0, Width - 1, Height - 1);
            var MainRectInner = new Rectangle(1, 1, Width - 3, Height - 3);

            var CenterRect = new Rectangle((int)Math.Round(MainRect.X + (MainRect.Width - bmp.Width) / 2d), (int)Math.Round(MainRect.Y + (MainRect.Height - bmp.Height) / 2d), bmp.Width, bmp.Height);


            var bkC = _Focused ? Program.Style.Schemes.Main.Colors.Back_Checked : Program.Style.Schemes.Main.Colors.Back;
            var bkCC = Color.FromArgb(alpha, Program.Style.Schemes.Main.Colors.Back_Checked);

            using (SolidBrush br = new(bkC))
            {
                e.Graphics.FillRoundedRect(br, MainRectInner);
            }
            using (SolidBrush br = new(bkCC))
            {
                e.Graphics.FillRoundedRect(br, MainRect);
            }

            var lC = Color.FromArgb(255 - alpha, _Focused ? Program.Style.Schemes.Main.Colors.Line_Checked : Program.Style.Schemes.Main.Colors.Line);
            var lCC = Color.FromArgb(alpha, Program.Style.Schemes.Main.Colors.Line_Checked_Hover);

            using (Pen P = new(lC))
            {
                e.Graphics.DrawRoundedRect_LikeW11(P, MainRectInner);
            }
            using (Pen P = new(lCC))
            {
                e.Graphics.DrawRoundedRect_LikeW11(P, MainRect);
            }

            if (CurOptions.UseFromFile && System.IO.File.Exists(CurOptions.File) && System.IO.Path.GetExtension(CurOptions.File).ToUpper() == ".ANI")
            {
                float _Angle = 0;
                if (CurOptions._Angle >= 180) { _Angle = CurOptions._Angle - 180f; }
                else if (CurOptions._Angle < 180) { _Angle = CurOptions._Angle + 180f; }

                int frames = GetTotalFramesFromANI(CurOptions.File);

                Size size = new((int)(CurOptions.Scale * 32), (int)(CurOptions.Scale * 32));
                Point location = new((Width - size.Width) / 2 - 1, (Height - size.Height) / 2 - 1);

                int AngleToFrame = (int)(_Angle / 360 * frames);

                IntPtr hCursor = User32.LoadCursorFromFile(CurOptions.File);

                IntPtr hdc = e.Graphics.GetHdc();
                User32.DrawIconEx(hdc, location.X, location.Y, hCursor, size.Width, size.Height, AngleToFrame, IntPtr.Zero, (int)(User32.DrawIconExFlags.DI_NORMAL | User32.DrawIconExFlags.DI_COMPAT));
                e.Graphics.ReleaseHdc(hdc);
                User32.DestroyIcon(hCursor);
            }
            else
            {
                e.Graphics.DrawImage(bmp, CenterRect);
            }

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