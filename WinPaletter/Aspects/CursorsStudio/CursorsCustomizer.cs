using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using WinPaletter.Properties;

namespace WinPaletter
{
    public static class Paths
    {
        public enum CursorType
        {
            Arrow,
            Help,
            Busy,
            AppLoading,
            None,
            Move,
            Up,
            NS,
            EW,
            NESW,
            NWSE,
            Pen,
            Link,
            Pin,
            Person,
            IBeam,
            Cross
        }

        public enum GradientMode
        {
            Vertical,
            Horizontal,
            ForwardDiagonal,
            BackwardDiagonal,
            Circle
        }

        public static GradientMode ReturnGradientModeFromString(string String)
        {
            if (String.Trim().ToLower() == "vertical")
            {
                return GradientMode.Vertical;
            }

            else if (String.Trim().ToLower() == "horizontal")
            {
                return GradientMode.Horizontal;
            }

            else if (String.Trim().ToLower() == "forward diagonal")
            {
                return GradientMode.ForwardDiagonal;
            }

            else if (String.Trim().ToLower() == "backward diagonal")
            {
                return GradientMode.BackwardDiagonal;
            }

            else if (String.Trim().ToLower() == "circle")
            {
                return GradientMode.Circle;
            }

            else
            {
                return GradientMode.Vertical;

            }

        }

        public static string ReturnStringFromGradientMode(GradientMode GradientMode)
        {
            if (GradientMode == GradientMode.Horizontal)
            {
                return "Horizontal";
            }

            else if (GradientMode == GradientMode.Vertical)
            {
                return "Vertical";
            }

            else if (GradientMode == GradientMode.ForwardDiagonal)
            {
                return "Forward Diagonal";
            }

            else if (GradientMode == GradientMode.BackwardDiagonal)
            {
                return "Backward Diagonal";
            }

            else if (GradientMode == GradientMode.Circle)
            {
                return "Circle";
            }

            else
            {
                return null;

            }

        }

        public static Brush ReturnGradience(RectangleF Rectangle, Color Color1, Color Color2, GradientMode GradientMode, float Angle = 0f)
        {
            if (Rectangle.Width <= 0 || Rectangle.Height <= 0)
            {
                return new SolidBrush(Color1);
            }

            else if (GradientMode == GradientMode.Horizontal)
            {
                return new LinearGradientBrush(Rectangle, Color1, Color2, LinearGradientMode.Horizontal);
            }

            else if (GradientMode == GradientMode.Vertical)
            {
                return new LinearGradientBrush(Rectangle, Color1, Color2, LinearGradientMode.Vertical);
            }

            else if (GradientMode == GradientMode.ForwardDiagonal)
            {
                return new LinearGradientBrush(Rectangle, Color1, Color2, LinearGradientMode.ForwardDiagonal);
            }

            else if (GradientMode == GradientMode.BackwardDiagonal)
            {
                return new LinearGradientBrush(Rectangle, Color1, Color2, LinearGradientMode.BackwardDiagonal);
            }

            else if (GradientMode == GradientMode.Circle)
            {
                return new LinearGradientBrush(Rectangle, Color1, Color2, Angle, true);
            }

            else
            {
                return new SolidBrush(Color1);

            }

        }

        public static Bitmap Draw(CursorOptions CursorOptions)
        {
            Bitmap b = new((int)(32f * CursorOptions.Scale), (int)(32f * CursorOptions.Scale), PixelFormat.Format32bppPArgb);
            Graphics G = Graphics.FromImage(b);

            SmoothingMode smoothingMode = CursorOptions.ArrowStyle == ArrowStyle.Classic ? SmoothingMode.None : SmoothingMode.AntiAlias;
            G.SmoothingMode = smoothingMode;

            G.ScaleTransform(CursorOptions.Scale, CursorOptions.Scale);

            G.Clear(Color.Transparent);

            #region Rectangles Helpers
            RectangleF _Arrow = new(0, 0, b.Width, b.Height);
            RectangleF _Help = CursorOptions.ArrowStyle != ArrowStyle.Classic ? new(11, 6, b.Width, b.Height) : new(2, 0, b.Width, b.Height);
            RectangleF _Busy = new(CursorOptions.Scale, 0, 22, 22);
            RectangleF _CurRect = new(0, 8, b.Width, b.Height);
            RectangleF _Pin = new(15, 11, b.Width, b.Height);
            RectangleF _Person = new(19, 17, b.Width, b.Height);
            #endregion

            if (!CursorOptions.UseFromFile || !File.Exists(CursorOptions.File))
            {
                switch (CursorOptions.Cursor)
                {
                    case CursorType.Arrow:
                        {
                            using (GraphicsPath cursorPath = Arrow(_Arrow, CursorOptions.ArrowStyle, 1))
                            {
                                Brush BB, BL;
                                if (CursorOptions.PrimaryColorGradient)
                                {
                                    BB = ReturnGradience(cursorPath.GetBounds(), CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                                }
                                else
                                {
                                    BB = new SolidBrush(CursorOptions.PrimaryColor1);
                                }
                                if (CursorOptions.SecondaryColorGradient)
                                {
                                    BL = ReturnGradience(cursorPath.GetBounds(), CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                                }
                                else
                                {
                                    BL = new SolidBrush(CursorOptions.SecondaryColor1);
                                }

                                G.FillPath(BB, cursorPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.PrimaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    {
                                        G.FillPath(noise, cursorPath);
                                    }
                                }

                                using (Pen PL = new(BL, CursorOptions.BorderThickness)) { G.DrawPath(PL, cursorPath); }

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.SecondaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    using (Pen noisePen = new(noise, CursorOptions.BorderThickness))
                                    {
                                        G.DrawPath(noisePen, cursorPath);
                                    }
                                }

                                BB?.Dispose();
                                BL?.Dispose();
                            }

                            break;
                        }

                    case CursorType.Help:
                        {
                            using (GraphicsPath cursorPath = Arrow(_Arrow, CursorOptions.ArrowStyle, 1))
                            {
                                Brush BB, BL;
                                if (CursorOptions.PrimaryColorGradient)
                                {
                                    BB = ReturnGradience(cursorPath.GetBounds(), CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                                }
                                else
                                {
                                    BB = new SolidBrush(CursorOptions.PrimaryColor1);
                                }
                                if (CursorOptions.SecondaryColorGradient)
                                {
                                    BL = ReturnGradience(cursorPath.GetBounds(), CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                                }
                                else
                                {
                                    BL = new SolidBrush(CursorOptions.SecondaryColor1);
                                }

                                G.FillPath(BB, cursorPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.PrimaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    {
                                        G.FillPath(noise, cursorPath);
                                    }
                                }

                                using (Pen PL = new(BL, CursorOptions.BorderThickness)) { G.DrawPath(PL, cursorPath); }

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.SecondaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    using (Pen noisePen = new(noise, CursorOptions.BorderThickness))
                                    {
                                        G.DrawPath(noisePen, cursorPath);
                                    }
                                }

                                BB?.Dispose();
                                BL?.Dispose();
                            }

                            using (GraphicsPath helpPath = Help(_Help, CursorOptions.ArrowStyle, 1))
                            {
                                Brush BB_H, BL_H;
                                if (CursorOptions.PrimaryColorGradient)
                                {
                                    BB_H = ReturnGradience(helpPath.GetBounds(), CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                                }
                                else
                                {
                                    BB_H = new SolidBrush(CursorOptions.PrimaryColor1);
                                }
                                if (CursorOptions.SecondaryColorGradient)
                                {
                                    BL_H = ReturnGradience(helpPath.GetBounds(), CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                                }
                                else
                                {
                                    BL_H = new SolidBrush(CursorOptions.SecondaryColor1);
                                }

                                G.FillPath(CursorOptions.ArrowStyle != ArrowStyle.Classic ? BB_H : BL_H, helpPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.PrimaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    {
                                        G.FillPath(noise, helpPath);
                                    }
                                }

                                using (Pen PL_H = new(CursorOptions.ArrowStyle != ArrowStyle.Classic ? BL_H : BB_H, CursorOptions.BorderThickness))
                                {
                                    G.DrawPath(PL_H, helpPath);
                                }

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.SecondaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    using (Pen noisePen = new(noise, CursorOptions.BorderThickness))
                                    {
                                        G.DrawPath(noisePen, helpPath);
                                    }
                                }

                                BB_H.Dispose();
                                BL_H.Dispose();
                            }

                            break;
                        }

                    case CursorType.Busy:
                        {
                            G.SmoothingMode = SmoothingMode.None;

                            using (GraphicsPath busyPath = Busy(_Busy, CursorOptions.Angle, CursorOptions.CircleStyle, 1))
                            using (GraphicsPath busyLoaderPath = BusyLoader(_Busy, CursorOptions.Angle, CursorOptions.CircleStyle, 1))
                            {
                                Brush BC, BH;
                                if (CursorOptions.LoadingCircleBackGradient)
                                {
                                    BC = ReturnGradience(busyPath.GetBounds(), CursorOptions.LoadingCircleBack1, CursorOptions.LoadingCircleBack2, CursorOptions.LoadingCircleBackGradientMode, CursorOptions.Angle);
                                }
                                else
                                {
                                    BC = new SolidBrush(CursorOptions.LoadingCircleBack1);
                                }
                                if (CursorOptions.LoadingCircleHotGradient)
                                {
                                    BH = ReturnGradience(busyPath.GetBounds(), CursorOptions.LoadingCircleHot1, CursorOptions.LoadingCircleHot2, CursorOptions.LoadingCircleHotGradientMode, CursorOptions.Angle);
                                }
                                else
                                {
                                    BH = new SolidBrush(CursorOptions.LoadingCircleHot1);
                                }

                                if (CursorOptions.CircleStyle == CircleStyle.Classic)
                                {
                                    G.SmoothingMode = SmoothingMode.None;

                                    using (Pen PL = new(BH, CursorOptions.BorderThickness))
                                    {
                                        G.FillPath(BC, busyPath);
                                        G.DrawPath(PL, busyLoaderPath);
                                        G.DrawPath(PL, busyPath);
                                    }

                                    if (CursorOptions.LoadingCircleBackNoise)
                                    {
                                        using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.LoadingCircleBackNoiseOpacity))
                                        using (TextureBrush noise = new(bx))
                                        {
                                            G.FillPath(noise, busyPath);
                                        }
                                    }

                                    if (CursorOptions.LoadingCircleHotNoise)
                                    {
                                        using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.LoadingCircleHotNoiseOpacity))
                                        using (TextureBrush noise = new(bx))
                                        using (Pen noisePen = new(noise, CursorOptions.BorderThickness))
                                        {
                                            G.DrawPath(noisePen, busyLoaderPath);
                                            G.DrawPath(noisePen, busyPath);
                                        }
                                    }

                                    G.SmoothingMode = smoothingMode;
                                }

                                else if (CursorOptions.CircleStyle == CircleStyle.Aero)
                                {
                                    G.SmoothingMode = SmoothingMode.AntiAlias;

                                    G.FillPath(BC, busyPath);

                                    if (CursorOptions.LoadingCircleBackNoise)
                                    {
                                        using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.LoadingCircleBackNoiseOpacity))
                                        using (TextureBrush noise = new(bx))
                                        {
                                            G.FillPath(noise, busyPath);
                                        }
                                    }

                                    G.FillPath(BH, busyLoaderPath);

                                    if (CursorOptions.LoadingCircleHotNoise)
                                    {
                                        using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.LoadingCircleHotNoiseOpacity))
                                        using (TextureBrush noise = new(bx))
                                        {
                                            G.FillPath(noise, busyLoaderPath);
                                        }
                                    }

                                    G.SmoothingMode = smoothingMode;
                                }

                                else if (CursorOptions.CircleStyle == CircleStyle.Modern || CursorOptions.CircleStyle == CircleStyle.Fluid)
                                {
                                    G.SmoothingMode = SmoothingMode.AntiAlias;

                                    float PenWidth = 0.16f * Math.Max(_Arrow.Width, _Arrow.Height) / CursorOptions.Scale;
                                    PenWidth *= CursorOptions.CircleStyle == CircleStyle.Fluid ? 2.5f : 1f;

                                    float _percent = 0.3f;

                                    RectangleF CircleRect = new(PenWidth / 2f, PenWidth / 2f, 32 - PenWidth - 7, 32 - PenWidth - 7);

                                    using (Pen pen = new(BH, PenWidth))
                                    using (Pen pen2 = new(BC, PenWidth))
                                    {
                                        pen.StartCap = LineCap.Round;
                                        pen.EndCap = pen.StartCap;

                                        using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.LoadingCircleBackNoiseOpacity))
                                        using (TextureBrush noise = new(bx))
                                        using (Pen noisePen = new(noise, PenWidth))
                                        {
                                            G.DrawArc(pen2, CircleRect, -90, 360);

                                            if (CursorOptions.LoadingCircleBackNoise)
                                            {
                                                G.DrawArc(noisePen, CircleRect, -90, 360);
                                            }
                                        }

                                        using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.LoadingCircleHotNoiseOpacity))
                                        using (TextureBrush noise = new(bx))
                                        using (Pen noisePen = new(noise, PenWidth))
                                        {
                                            G.DrawArc(pen, CircleRect, CursorOptions.Angle, (int)Math.Round((double)(_percent * 360)));

                                            if (CursorOptions.LoadingCircleHotNoise)
                                            {
                                                G.DrawArc(noisePen, CircleRect, CursorOptions.Angle, (int)Math.Round((double)(_percent * 360)));
                                            }
                                        }
                                    }

                                    G.SmoothingMode = smoothingMode;
                                }

                                else if (CursorOptions.CircleStyle == CircleStyle.Aero7)
                                {
                                    // Fixer to get a near-correct Windows 7 sizes.
                                    int fixer = 2;
                                    int maxSide = (int)Math.Max((_Busy.Width - fixer) * CursorOptions.Scale, (_Busy.Height - fixer) * CursorOptions.Scale);
                                    _Busy.Width -= fixer;
                                    _Busy.Height -= fixer;

                                    // CursorOptions.Angle - 180f → shift starting angle to 180°.
                                    // +360f % 360f → ensures the result is in [0, 360) even if angle < 180.
                                    // /360f * 18 → scales to 18 frames.
                                    // (int) → floors to integer frame index.CursorOptions.Angle - 180f → shift starting angle to 180°.
                                    // +360f % 360f → ensures the result is in [0, 360) even if angle < 180.
                                    // /360f * 18 → scales to 18 frames.
                                    int frameNO = (int)(((CursorOptions.Angle - 180f + 360f) % 360f) / 360f * 18);
                                    int[] sizes = [12, 16, 20, 22, 25, 28, 34, 42];
                                    int correctSize = sizes.FirstOrDefault(s => maxSide <= s) == 0 ? sizes.Last() : sizes.First(s => maxSide <= s);

                                    ResourceManager rm = new($"{nameof(WinPaletter)}.{nameof(Assets)}.{nameof(Assets.AeroLoadingCircles)}", Assembly.GetExecutingAssembly());

                                    using (Bitmap frame = rm.GetObject($"_{correctSize}_{frameNO}") as Bitmap)
                                    using (Bitmap b_colorized = frame.Tint(CursorOptions.LoadingCircleBack1))
                                    {
                                        G.DrawImage(b_colorized, _Busy);
                                    }
                                }

                                BC.Dispose();
                                BH.Dispose();
                            }

                            G.SmoothingMode = smoothingMode;

                            break;
                        }

                    case CursorType.AppLoading:
                        {
                            using (GraphicsPath arrowPath = Arrow(_CurRect, CursorOptions.ArrowStyle, 1))
                            {
                                RectangleF _arrowDrawnRect;
                                _arrowDrawnRect = arrowPath.GetBounds();

                                RectangleF _LoadRect = new(_arrowDrawnRect.Right / 2 + 1f, 0, 14, 14);

                                Brush BB, BL;
                                if (CursorOptions.PrimaryColorGradient)
                                {
                                    BB = ReturnGradience(arrowPath.GetBounds(), CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode, CursorOptions.Angle);
                                }
                                else
                                {
                                    BB = new SolidBrush(CursorOptions.PrimaryColor1);
                                }
                                if (CursorOptions.SecondaryColorGradient)
                                {
                                    BL = ReturnGradience(arrowPath.GetBounds(), CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode, CursorOptions.Angle);
                                }
                                else
                                {
                                    BL = new SolidBrush(CursorOptions.SecondaryColor1);
                                }

                                G.FillPath(BB, arrowPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.PrimaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    {
                                        G.FillPath(noise, arrowPath);
                                    }
                                }

                                using (Pen PL = new(BL, CursorOptions.BorderThickness)) { G.DrawPath(PL, arrowPath); }

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.SecondaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    using (Pen noisePen = new(noise, CursorOptions.BorderThickness))
                                    {
                                        G.DrawPath(noisePen, arrowPath);
                                    }
                                }

                                BB?.Dispose();
                                BL?.Dispose();

                                if (CursorOptions.CircleStyle == CircleStyle.Classic)
                                {
                                    G.SmoothingMode = SmoothingMode.None;

                                    using (GraphicsPath appLoadingPath = AppLoading(_Busy, CursorOptions.Angle, CursorOptions.CircleStyle, 1))
                                    using (GraphicsPath appLoadingCirclePath = AppLoaderCircle(_Busy, CursorOptions.Angle, CursorOptions.CircleStyle, 1))
                                    {
                                        Brush BC, BH;
                                        if (CursorOptions.LoadingCircleBackGradient)
                                        {
                                            BC = ReturnGradience(appLoadingPath.GetBounds(), CursorOptions.LoadingCircleBack1, CursorOptions.LoadingCircleBack2, CursorOptions.LoadingCircleBackGradientMode, CursorOptions.Angle);
                                        }
                                        else
                                        {
                                            BC = new SolidBrush(CursorOptions.LoadingCircleBack1);
                                        }
                                        if (CursorOptions.LoadingCircleHotGradient)
                                        {
                                            BH = ReturnGradience(appLoadingPath.GetBounds(), CursorOptions.LoadingCircleHot1, CursorOptions.LoadingCircleHot2, CursorOptions.LoadingCircleHotGradientMode, CursorOptions.Angle);
                                        }
                                        else
                                        {
                                            BH = new SolidBrush(CursorOptions.LoadingCircleHot1);
                                        }

                                        using (Pen PLx = new(BH, CursorOptions.BorderThickness))
                                        {
                                            G.FillPath(BC, appLoadingPath);
                                            G.DrawPath(PLx, appLoadingCirclePath);
                                            G.DrawPath(PLx, appLoadingPath);
                                        }

                                        if (CursorOptions.LoadingCircleBackNoise)
                                        {
                                            using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.LoadingCircleBackNoiseOpacity))
                                            using (TextureBrush noise = new(bx))
                                            {
                                                G.FillPath(noise, appLoadingPath);
                                            }
                                        }

                                        if (CursorOptions.LoadingCircleHotNoise)
                                        {
                                            using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.LoadingCircleHotNoiseOpacity))
                                            using (TextureBrush noise = new(bx))
                                            using (Pen noisePen = new(noise, CursorOptions.BorderThickness))
                                            {
                                                G.DrawPath(noisePen, appLoadingCirclePath);
                                                G.DrawPath(noisePen, appLoadingPath);
                                            }
                                        }

                                        BC?.Dispose();
                                        BH?.Dispose();
                                    }

                                    G.SmoothingMode = smoothingMode;
                                }

                                else if (CursorOptions.CircleStyle == CircleStyle.Aero)
                                {
                                    G.SmoothingMode = SmoothingMode.AntiAlias;

                                    using (GraphicsPath appLoadingPath = AppLoading(_LoadRect, CursorOptions.Angle, CursorOptions.CircleStyle, 1))
                                    using (GraphicsPath appLoadingCirclePath = AppLoaderCircle(_LoadRect, CursorOptions.Angle, CursorOptions.CircleStyle, 1))
                                    {
                                        Brush BC, BH;
                                        if (CursorOptions.LoadingCircleBackGradient)
                                        {
                                            BC = ReturnGradience(appLoadingPath.GetBounds(), CursorOptions.LoadingCircleBack1, CursorOptions.LoadingCircleBack2, CursorOptions.LoadingCircleBackGradientMode, CursorOptions.Angle);
                                        }
                                        else
                                        {
                                            BC = new SolidBrush(CursorOptions.LoadingCircleBack1);
                                        }
                                        if (CursorOptions.LoadingCircleHotGradient)
                                        {
                                            BH = ReturnGradience(appLoadingPath.GetBounds(), CursorOptions.LoadingCircleHot1, CursorOptions.LoadingCircleHot2, CursorOptions.LoadingCircleHotGradientMode, CursorOptions.Angle);
                                        }
                                        else
                                        {
                                            BH = new SolidBrush(CursorOptions.LoadingCircleHot1);
                                        }

                                        G.FillPath(BC, appLoadingPath);

                                        if (CursorOptions.LoadingCircleBackNoise)
                                        {
                                            using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.LoadingCircleBackNoiseOpacity))
                                            using (TextureBrush noise = new(bx))
                                            {
                                                G.FillPath(noise, appLoadingPath);
                                            }
                                        }

                                        G.FillPath(BH, appLoadingCirclePath);

                                        if (CursorOptions.LoadingCircleHotNoise)
                                        {
                                            using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.LoadingCircleHotNoiseOpacity))
                                            using (TextureBrush noise = new(bx))
                                            {
                                                G.FillPath(noise, appLoadingCirclePath);
                                            }
                                        }

                                        BC?.Dispose();
                                        BH?.Dispose();
                                    }

                                    G.SmoothingMode = smoothingMode;
                                }

                                else if (CursorOptions.CircleStyle == CircleStyle.Modern || CursorOptions.CircleStyle == CircleStyle.Fluid)
                                {
                                    G.SmoothingMode = SmoothingMode.AntiAlias;

                                    float PenWidth = 0.3f * Math.Max(_LoadRect.Width, _LoadRect.Height);
                                    PenWidth *= CursorOptions.CircleStyle == CircleStyle.Fluid ? 2.5f : 1f;

                                    float _percent = 0.3f;

                                    float CircleWidth = 14;
                                    CircleWidth /= CursorOptions.CircleStyle == CircleStyle.Fluid ? 2.5f : 1f;

                                    RectangleF CircleRect = new(_arrowDrawnRect.Right + (CursorOptions.CircleStyle == CircleStyle.Fluid ? 2f : -1f), CursorOptions.CircleStyle == CircleStyle.Fluid ? 6 : 2, CircleWidth, CircleWidth);

                                    Brush BC, BH;
                                    if (CursorOptions.LoadingCircleBackGradient)
                                    {
                                        BC = ReturnGradience(CircleRect, CursorOptions.LoadingCircleBack1, CursorOptions.LoadingCircleBack2, CursorOptions.LoadingCircleBackGradientMode, CursorOptions.Angle);
                                    }
                                    else
                                    {
                                        BC = new SolidBrush(CursorOptions.LoadingCircleBack1);
                                    }
                                    if (CursorOptions.LoadingCircleHotGradient)
                                    {
                                        BH = ReturnGradience(CircleRect, CursorOptions.LoadingCircleHot1, CursorOptions.LoadingCircleHot2, CursorOptions.LoadingCircleHotGradientMode, CursorOptions.Angle);
                                    }
                                    else
                                    {
                                        BH = new SolidBrush(CursorOptions.LoadingCircleHot1);
                                    }

                                    using (Pen pen = new(BH, PenWidth))
                                    using (Pen pen2 = new(BC, PenWidth))
                                    {
                                        pen.StartCap = LineCap.Round;
                                        pen.EndCap = pen.StartCap;

                                        using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.LoadingCircleBackNoiseOpacity))
                                        using (TextureBrush noise = new(bx))
                                        using (Pen noisePen = new(noise, PenWidth))
                                        {
                                            G.DrawArc(pen2, CircleRect, -90, 360);

                                            if (CursorOptions.LoadingCircleBackNoise)
                                            {
                                                G.DrawArc(noisePen, CircleRect, -90, 360);
                                            }
                                        }

                                        using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.LoadingCircleHotNoiseOpacity))
                                        using (TextureBrush noise = new(bx))
                                        using (Pen noisePen = new(noise, PenWidth))
                                        {
                                            G.DrawArc(pen, CircleRect, CursorOptions.Angle, (int)Math.Round((double)(_percent * 360)));

                                            if (CursorOptions.LoadingCircleHotNoise)
                                            {
                                                G.DrawArc(noisePen, CircleRect, CursorOptions.Angle, (int)Math.Round((double)(_percent * 360)));
                                            }
                                        }

                                        BC?.Dispose();
                                        BH?.Dispose();
                                    }

                                    G.SmoothingMode = smoothingMode;
                                }

                                else if (CursorOptions.CircleStyle == CircleStyle.Aero7)
                                {
                                    // Fixer to get a near-correct Windows 7 sizes.
                                    int fixer = -2;
                                    int maxSide = (int)Math.Max((_LoadRect.Width - fixer) * CursorOptions.Scale, (_LoadRect.Height - fixer) * CursorOptions.Scale);
                                    _LoadRect.Width -= fixer;
                                    _LoadRect.Height -= fixer;
                                    _LoadRect.X += 1;
                                    _LoadRect.Y += 1;

                                    // CursorOptions.Angle - 180f → shift starting angle to 180°.
                                    // +360f % 360f → ensures the result is in [0, 360) even if angle < 180.
                                    // /360f * 18 → scales to 18 frames.
                                    // (int) → floors to integer frame index.CursorOptions.Angle - 180f → shift starting angle to 180°.
                                    // +360f % 360f → ensures the result is in [0, 360) even if angle < 180.
                                    // /360f * 18 → scales to 18 frames.
                                    int frameNO = (int)(((CursorOptions.Angle - 180f + 360f) % 360f) / 360f * 18);
                                    int[] sizes = [12, 16, 20, 22, 25, 28, 34, 42];
                                    int correctSize = sizes.FirstOrDefault(s => maxSide <= s) == 0 ? sizes.Last() : sizes.First(s => maxSide <= s);

                                    ResourceManager rm = new($"{nameof(WinPaletter)}.{nameof(Assets)}.{nameof(Assets.AeroLoadingCircles)}", Assembly.GetExecutingAssembly());

                                    using (Bitmap frame = rm.GetObject($"_{correctSize}_{frameNO}") as Bitmap)
                                    using (Bitmap b_colorized = frame.Tint(CursorOptions.LoadingCircleBack1))
                                    {
                                        G.DrawImage(b_colorized, _LoadRect);
                                    }
                                }
                            }

                            break;
                        }

                    case CursorType.None:
                        {
                            using (GraphicsPath nonePath = None(_Arrow, 1))
                            using (GraphicsPath noneBackgroundPath = NoneBackground(_Arrow, 1))
                            {
                                Brush BB, BL;
                                if (CursorOptions.PrimaryColorGradient)
                                {
                                    BB = ReturnGradience(nonePath.GetBounds(), CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                                }
                                else
                                {
                                    BB = new SolidBrush(CursorOptions.PrimaryColor1);
                                }
                                if (CursorOptions.SecondaryColorGradient)
                                {
                                    BL = ReturnGradience(nonePath.GetBounds(), CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                                }
                                else
                                {
                                    BL = new SolidBrush(CursorOptions.SecondaryColor1);
                                }

                                G.FillPath(BB, noneBackgroundPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.PrimaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    {
                                        G.FillPath(noise, noneBackgroundPath);
                                    }
                                }

                                G.FillPath(BL, nonePath);

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.SecondaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    {
                                        G.FillPath(noise, nonePath);
                                    }
                                }

                                BB?.Dispose();
                                BL?.Dispose();
                            }

                            break;
                        }

                    case CursorType.Move:
                        {
                            using (GraphicsPath movePath = Move(_Arrow, 1))
                            {
                                Brush BB, BL;
                                if (CursorOptions.PrimaryColorGradient)
                                {
                                    BB = ReturnGradience(movePath.GetBounds(), CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                                }
                                else
                                {
                                    BB = new SolidBrush(CursorOptions.PrimaryColor1);
                                }
                                if (CursorOptions.SecondaryColorGradient)
                                {
                                    BL = ReturnGradience(movePath.GetBounds(), CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                                }
                                else
                                {
                                    BL = new SolidBrush(CursorOptions.SecondaryColor1);
                                }

                                G.FillPath(BB, movePath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.PrimaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    {
                                        G.FillPath(noise, movePath);
                                    }
                                }

                                using (Pen PL = new(BL, CursorOptions.BorderThickness)) { G.DrawPath(PL, movePath); }

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.SecondaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    using (Pen noisePen = new(noise, CursorOptions.BorderThickness))
                                    {
                                        G.DrawPath(noisePen, movePath);
                                    }
                                }

                                BB?.Dispose();
                                BL?.Dispose();
                            }

                            break;
                        }

                    case CursorType.Up:
                        {
                            using (GraphicsPath upPath = Up(_Arrow, 1))
                            {
                                Brush BB, BL;
                                if (CursorOptions.PrimaryColorGradient)
                                {
                                    BB = ReturnGradience(upPath.GetBounds(), CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                                }
                                else
                                {
                                    BB = new SolidBrush(CursorOptions.PrimaryColor1);
                                }
                                if (CursorOptions.SecondaryColorGradient)
                                {
                                    BL = ReturnGradience(upPath.GetBounds(), CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                                }
                                else
                                {
                                    BL = new SolidBrush(CursorOptions.SecondaryColor1);
                                }

                                G.FillPath(BB, upPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.PrimaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    {
                                        G.FillPath(noise, upPath);
                                    }
                                }

                                using (Pen PL = new(BL, CursorOptions.BorderThickness)) { G.DrawPath(PL, upPath); }

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.SecondaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    using (Pen noisePen = new(noise, CursorOptions.BorderThickness))
                                    {
                                        G.DrawPath(noisePen, upPath);
                                    }
                                }

                                BB?.Dispose();
                                BL?.Dispose();
                            }

                            break;
                        }

                    case CursorType.NS:
                        {
                            using (GraphicsPath nsPath = NS(_Arrow, 1))
                            {
                                Brush BB, BL;
                                if (CursorOptions.PrimaryColorGradient)
                                {
                                    BB = ReturnGradience(nsPath.GetBounds(), CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                                }
                                else
                                {
                                    BB = new SolidBrush(CursorOptions.PrimaryColor1);
                                }
                                if (CursorOptions.SecondaryColorGradient)
                                {
                                    BL = ReturnGradience(nsPath.GetBounds(), CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                                }
                                else
                                {
                                    BL = new SolidBrush(CursorOptions.SecondaryColor1);
                                }

                                G.FillPath(BB, nsPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.PrimaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    {
                                        G.FillPath(noise, nsPath);
                                    }
                                }

                                using (Pen PL = new(BL, CursorOptions.BorderThickness)) { G.DrawPath(PL, nsPath); }

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.SecondaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    using (Pen noisePen = new(noise, CursorOptions.BorderThickness))
                                    {
                                        G.DrawPath(noisePen, nsPath);
                                    }
                                }

                                BB?.Dispose();
                                BL?.Dispose();
                            }

                            break;
                        }

                    case CursorType.EW:
                        {
                            using (GraphicsPath ewPath = EW(_Arrow, 1))
                            {
                                Brush BB, BL;
                                if (CursorOptions.PrimaryColorGradient)
                                {
                                    BB = ReturnGradience(ewPath.GetBounds(), CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                                }
                                else
                                {
                                    BB = new SolidBrush(CursorOptions.PrimaryColor1);
                                }
                                if (CursorOptions.SecondaryColorGradient)
                                {
                                    BL = ReturnGradience(ewPath.GetBounds(), CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                                }
                                else
                                {
                                    BL = new SolidBrush(CursorOptions.SecondaryColor1);
                                }

                                G.FillPath(BB, ewPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.PrimaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    {
                                        G.FillPath(noise, ewPath);
                                    }
                                }

                                using (Pen PL = new(BL, CursorOptions.BorderThickness)) { G.DrawPath(PL, ewPath); }

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.SecondaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    using (Pen noisePen = new(noise, CursorOptions.BorderThickness))
                                    {
                                        G.DrawPath(noisePen, ewPath);
                                    }
                                }

                                BB?.Dispose();
                                BL?.Dispose();
                            }

                            break;
                        }

                    case CursorType.NESW:
                        {
                            using (GraphicsPath neswPath = NESW(_Arrow, 1))
                            {
                                Brush BB, BL;
                                if (CursorOptions.PrimaryColorGradient)
                                {
                                    BB = ReturnGradience(neswPath.GetBounds(), CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                                }
                                else
                                {
                                    BB = new SolidBrush(CursorOptions.PrimaryColor1);
                                }
                                if (CursorOptions.SecondaryColorGradient)
                                {
                                    BL = ReturnGradience(neswPath.GetBounds(), CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                                }
                                else
                                {
                                    BL = new SolidBrush(CursorOptions.SecondaryColor1);
                                }

                                G.FillPath(BB, neswPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.PrimaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    {
                                        G.FillPath(noise, neswPath);
                                    }
                                }

                                using (Pen PL = new(BL, CursorOptions.BorderThickness)) { G.DrawPath(PL, neswPath); }

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.SecondaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    using (Pen noisePen = new(noise, CursorOptions.BorderThickness))
                                    {
                                        G.DrawPath(noisePen, neswPath);
                                    }
                                }

                                BB?.Dispose();
                                BL?.Dispose();
                            }

                            break;
                        }

                    case CursorType.NWSE:
                        {
                            using (GraphicsPath nwsePath = NWSE(_Arrow, 1))
                            {
                                Brush BB, BL;
                                if (CursorOptions.PrimaryColorGradient)
                                {
                                    BB = ReturnGradience(nwsePath.GetBounds(), CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                                }
                                else
                                {
                                    BB = new SolidBrush(CursorOptions.PrimaryColor1);
                                }
                                if (CursorOptions.SecondaryColorGradient)
                                {
                                    BL = ReturnGradience(nwsePath.GetBounds(), CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                                }
                                else
                                {
                                    BL = new SolidBrush(CursorOptions.SecondaryColor1);
                                }

                                G.FillPath(BB, nwsePath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.PrimaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    {
                                        G.FillPath(noise, nwsePath);
                                    }
                                }

                                using (Pen PL = new(BL, CursorOptions.BorderThickness)) { G.DrawPath(PL, nwsePath); }

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.SecondaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    using (Pen noisePen = new(noise, CursorOptions.BorderThickness))
                                    {
                                        G.DrawPath(noisePen, nwsePath);
                                    }
                                }

                                BB?.Dispose();
                                BL?.Dispose();
                            }

                            break;
                        }

                    case CursorType.Pen:
                        {
                            using (GraphicsPath penPath = Pen(_Arrow, 1))
                            using (GraphicsPath penBackgroundPath = PenBackground(_Arrow, 1))
                            {
                                Brush BB, BL;
                                if (CursorOptions.PrimaryColorGradient)
                                {
                                    BB = ReturnGradience(penPath.GetBounds(), CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                                }
                                else
                                {
                                    BB = new SolidBrush(CursorOptions.PrimaryColor1);
                                }
                                if (CursorOptions.SecondaryColorGradient)
                                {
                                    BL = ReturnGradience(penPath.GetBounds(), CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                                }
                                else
                                {
                                    BL = new SolidBrush(CursorOptions.SecondaryColor1);
                                }

                                G.FillPath(BB, penBackgroundPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.PrimaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    {
                                        G.FillPath(noise, penBackgroundPath);
                                    }
                                }

                                using (Pen PL = new(BL, CursorOptions.BorderThickness)) { G.DrawPath(PL, penPath); }

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.SecondaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    using (Pen noisePen = new(noise, CursorOptions.BorderThickness))
                                    {
                                        G.DrawPath(noisePen, penPath);
                                    }
                                }

                                BB?.Dispose();
                                BL?.Dispose();
                            }

                            break;
                        }

                    case CursorType.Link:
                        {
                            using (GraphicsPath handPath = Hand(_Arrow, CursorOptions.ArrowStyle, 1))
                            {
                                Brush BB, BL;
                                if (CursorOptions.PrimaryColorGradient)
                                {
                                    BB = ReturnGradience(handPath.GetBounds(), CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                                }
                                else
                                {
                                    BB = new SolidBrush(CursorOptions.PrimaryColor1);
                                }
                                if (CursorOptions.SecondaryColorGradient)
                                {
                                    BL = ReturnGradience(handPath.GetBounds(), CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                                }
                                else
                                {
                                    BL = new SolidBrush(CursorOptions.SecondaryColor1);
                                }

                                G.FillPath(BB, handPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.PrimaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    {
                                        G.FillPath(noise, handPath);
                                    }
                                }

                                using (Pen PL = new(BL, CursorOptions.BorderThickness)) { G.DrawPath(PL, handPath); }
                                using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.SecondaryNoiseOpacity))
                                using (TextureBrush noise = new(bx))
                                using (Pen noisePen = new(noise, CursorOptions.BorderThickness))
                                {
                                    G.DrawPath(noisePen, handPath);
                                }

                                BB?.Dispose();
                                BL?.Dispose();
                            }

                            break;
                        }

                    case CursorType.Pin:
                        {
                            using (GraphicsPath handPath = Hand(_Arrow, CursorOptions.ArrowStyle, 1))
                            using (GraphicsPath pinPath = Pin(_Pin, 1))
                            using (GraphicsPath pinCenterPointPath = Pin_CenterPoint(_Pin, 1))
                            {
                                Brush BB, BL;
                                if (CursorOptions.PrimaryColorGradient)
                                {
                                    BB = ReturnGradience(handPath.GetBounds(), CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                                }
                                else
                                {
                                    BB = new SolidBrush(CursorOptions.PrimaryColor1);
                                }
                                if (CursorOptions.SecondaryColorGradient)
                                {
                                    BL = ReturnGradience(handPath.GetBounds(), CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                                }
                                else
                                {
                                    BL = new SolidBrush(CursorOptions.SecondaryColor1);
                                }

                                Brush BB_P, BL_P;
                                if (CursorOptions.PrimaryColorGradient)
                                {
                                    BB_P = ReturnGradience(pinPath.GetBounds(), CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                                }
                                else
                                {
                                    BB_P = new SolidBrush(CursorOptions.PrimaryColor1);
                                }
                                if (CursorOptions.SecondaryColorGradient)
                                {
                                    BL_P = ReturnGradience(pinPath.GetBounds(), CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                                }
                                else
                                {
                                    BL_P = new SolidBrush(CursorOptions.SecondaryColor1);
                                }

                                G.FillPath(BB, handPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.PrimaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    {
                                        G.FillPath(noise, handPath);
                                    }
                                }

                                using (Pen PL = new(BL, CursorOptions.BorderThickness)) G.DrawPath(PL, handPath);

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.SecondaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    using (Pen noisePen = new(noise, CursorOptions.BorderThickness))
                                    {
                                        G.DrawPath(noisePen, handPath);
                                    }
                                }

                                G.FillPath(BB_P, pinPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.PrimaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    {
                                        G.FillPath(noise, pinPath);
                                    }
                                }

                                G.FillPath(BL_P, pinCenterPointPath);

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.PrimaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    {
                                        G.FillPath(noise, pinCenterPointPath);
                                    }
                                }

                                using (Pen P = new(BL_P, 2f)) { G.DrawPath(P, pinPath); }

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.SecondaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    using (Pen noisePen = new(noise, CursorOptions.BorderThickness))
                                    {
                                        G.DrawPath(noisePen, pinPath);
                                    }
                                }

                                BB?.Dispose();
                                BL?.Dispose();
                                BB_P.Dispose();
                                BL_P.Dispose();
                            }

                            break;
                        }

                    case CursorType.Person:
                        {
                            using (GraphicsPath handPath = Hand(_Arrow, CursorOptions.ArrowStyle, 1))
                            using (GraphicsPath personPath = Person(_Person, 1))
                            {
                                Brush BB, BL;
                                if (CursorOptions.PrimaryColorGradient)
                                {
                                    BB = ReturnGradience(handPath.GetBounds(), CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                                }
                                else
                                {
                                    BB = new SolidBrush(CursorOptions.PrimaryColor1);
                                }
                                if (CursorOptions.SecondaryColorGradient)
                                {
                                    BL = ReturnGradience(handPath.GetBounds(), CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                                }
                                else
                                {
                                    BL = new SolidBrush(CursorOptions.SecondaryColor1);
                                }

                                Brush BB_P, BL_P;
                                if (CursorOptions.PrimaryColorGradient)
                                {
                                    BB_P = ReturnGradience(personPath.GetBounds(), CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                                }
                                else
                                {
                                    BB_P = new SolidBrush(CursorOptions.PrimaryColor1);
                                }
                                if (CursorOptions.SecondaryColorGradient)
                                {
                                    BL_P = ReturnGradience(personPath.GetBounds(), CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                                }
                                else
                                {
                                    BL_P = new SolidBrush(CursorOptions.SecondaryColor1);
                                }

                                G.FillPath(BB, handPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.PrimaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    {
                                        G.FillPath(noise, handPath);
                                    }
                                }

                                using (Pen PL = new(BL, CursorOptions.BorderThickness)) G.DrawPath(PL, handPath);

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.SecondaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    using (Pen noisePen = new(noise, CursorOptions.BorderThickness))
                                    {
                                        G.DrawPath(noisePen, handPath);
                                    }
                                }

                                G.FillPath(BB_P, personPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.PrimaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    {
                                        G.FillPath(noise, personPath);
                                    }
                                }

                                using (Pen P = new(BL_P, 2f)) { G.DrawPath(P, personPath); }

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.SecondaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    using (Pen noisePen = new(noise, CursorOptions.BorderThickness))
                                    {
                                        G.DrawPath(noisePen, personPath);
                                    }
                                }

                                BB?.Dispose();
                                BL?.Dispose();
                                BB_P.Dispose();
                                BL_P.Dispose();
                            }

                            break;
                        }

                    case CursorType.IBeam:
                        {
                            using (GraphicsPath iBeamPath = IBeam(_Arrow, 1))
                            {
                                Brush BB, BL;
                                if (CursorOptions.PrimaryColorGradient)
                                {
                                    BB = ReturnGradience(iBeamPath.GetBounds(), CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                                }
                                else
                                {
                                    BB = new SolidBrush(CursorOptions.PrimaryColor1);
                                }
                                if (CursorOptions.SecondaryColorGradient)
                                {
                                    BL = ReturnGradience(iBeamPath.GetBounds(), CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                                }
                                else
                                {
                                    BL = new SolidBrush(CursorOptions.SecondaryColor1);
                                }

                                G.FillPath(BB, iBeamPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.PrimaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    {
                                        G.FillPath(noise, iBeamPath);
                                    }
                                }

                                using (Pen PL = new(BL, CursorOptions.BorderThickness)) G.DrawPath(PL, iBeamPath);

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.SecondaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    using (Pen noisePen = new(noise, CursorOptions.BorderThickness))
                                    {
                                        G.DrawPath(noisePen, iBeamPath);
                                    }
                                }

                                BB?.Dispose();
                                BL?.Dispose();
                            }

                            break;
                        }

                    case CursorType.Cross:
                        {
                            using (GraphicsPath crossPath = Cross(_Arrow, 1))
                            {
                                Brush BB, BL;
                                if (CursorOptions.PrimaryColorGradient)
                                {
                                    BB = ReturnGradience(crossPath.GetBounds(), CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                                }
                                else
                                {
                                    BB = new SolidBrush(CursorOptions.PrimaryColor1);
                                }
                                if (CursorOptions.SecondaryColorGradient)
                                {
                                    BL = ReturnGradience(crossPath.GetBounds(), CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                                }
                                else
                                {
                                    BL = new SolidBrush(CursorOptions.SecondaryColor1);
                                }

                                G.FillPath(BB, crossPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.PrimaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    {
                                        G.FillPath(noise, crossPath);
                                    }
                                }

                                using (Pen PL = new(BL, CursorOptions.BorderThickness)) G.DrawPath(PL, crossPath);

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (Bitmap bx = Resources.Noise_Opaque.Fade(CursorOptions.SecondaryNoiseOpacity))
                                    using (TextureBrush noise = new(bx))
                                    using (Pen noisePen = new(noise, CursorOptions.BorderThickness))
                                    {
                                        G.DrawPath(noisePen, crossPath);
                                    }
                                }

                                BB?.Dispose();
                                BL?.Dispose();
                            }

                            break;
                        }
                }
            }

            else
            {
                if (File.Exists(CursorOptions.File))
                {
                    if (Path.GetExtension(CursorOptions.File).ToUpper() == ".CUR")
                    {
                        using (FileStream stream = new(CursorOptions.File, FileMode.Open, FileAccess.Read))
                        {
                            EOIcoCurLoader cur = new(stream);

                            Bitmap ExtractedBMP = null;

                            for (uint i = 0; i <= cur.CountImages() - 1; i++)
                            {
                                if (cur.GetImage(i).Size.Width <= Math.Max(b.Size.Width, 32))
                                {
                                    ExtractedBMP = cur.GetImage(i);
                                    break;
                                }
                            }

                            if (ExtractedBMP is not null) G.DrawImage(ExtractedBMP, 0, 0);
                        }
                    }
                }
            }

            G.Flush();
            G.Save();

            Bitmap B_Final = new(b.Width, b.Height);
            using (Graphics G_Final = Graphics.FromImage(B_Final))
            {
                if (CursorOptions.Shadow_Enabled)
                {
                    Bitmap shadowedBMP = new(b);

                    for (int x = 0, loopTo = b.Width - 1; x <= loopTo; x++)
                    {
                        for (int y = 0, loopTo1 = b.Height - 1; y <= loopTo1; y++)
                            shadowedBMP.SetPixel(x, y, Color.FromArgb(b.GetPixel(x, y).A, CursorOptions.Shadow_Color));
                    }

                    using (Bitmap bmpBlurred = shadowedBMP?.Blur(CursorOptions.Shadow_Blur))
                    using (Bitmap bmpFaded = bmpBlurred?.Fade(CursorOptions.Shadow_Opacity))
                    {
                        G_Final.DrawImage(bmpFaded, new Rectangle(0 + CursorOptions.Shadow_OffsetX, 0 + CursorOptions.Shadow_OffsetY, b.Width, b.Height));
                    }
                }

                G_Final.DrawImage(b, new Rectangle(0, 0, b.Width, b.Height));
            }

            G.ResetTransform();

            b.Dispose();
            G.Dispose();

            return B_Final;
        }

        public enum ArrowStyle
        {
            Aero,
            Modern,
            Classic
        }

        public enum CircleStyle
        {
            Aero,
            Modern,
            Classic,
            Fluid,
            Aero7
        }

        public static GraphicsPath Arrow(RectangleF Rectangle, ArrowStyle Style, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                RectangleF R = new(Rectangle.X, Rectangle.Y, 12, 18);

                switch (Style)
                {
                    case ArrowStyle.Aero:
                        {
                            // #### Left Border
                            PointF LLine1 = new(R.X, R.Y);
                            PointF LLine2 = new(R.X, R.Y + R.Height - 2);
                            path.AddLine(LLine1, LLine2);

                            // #### Left Down Border
                            PointF DLLine1 = LLine2 + (Size)new Point(1, 0);
                            PointF DLLine2 = new(DLLine1.X + 3, DLLine1.Y - 3);
                            path.AddLine(DLLine1, DLLine2);

                            // #### Left Down Handle Border
                            PointF DLHLine1 = DLLine2;
                            PointF DLHLine2 = new(DLHLine1.X + 3, DLHLine1.Y + 5);
                            path.AddLine(DLHLine1, DLHLine2);

                            // #### Down Handle Border
                            PointF DHLine1 = DLHLine2;
                            PointF DHLine2 = new(DHLine1.X + 2, DHLine1.Y - 1);

                            // #### Right Down Handle Border
                            PointF DRHLine1 = DHLine2;
                            PointF DRHLine2 = new(DLHLine1.X + 3, DLHLine1.Y - 1);
                            path.AddLine(DRHLine1, DRHLine2);

                            // #### Right Down Border
                            PointF DRLine1 = DRHLine2;
                            PointF DRLine2 = new(R.X + R.Width - 1, DLHLine1.Y - 1);
                            path.AddLine(DRLine1, DRLine2);

                            // #### Right Border
                            PointF RLine1 = DRLine2 + (Size)new Point(0, -1);
                            PointF RLine2 = LLine1;
                            path.AddLine(RLine1, RLine2);
                            break;
                        }

                    case ArrowStyle.Classic:
                        {
                            // #### Left Border
                            PointF LLine1 = new(R.X, R.Y);
                            PointF LLine2 = new(R.X, R.Y + R.Height - 2);
                            path.AddLine(LLine1, LLine2);

                            // #### Left Down Border
                            PointF DLLine1 = LLine2 + (Size)new Point(1, -1);
                            PointF DLLine2 = new(DLLine1.X + 3, DLLine1.Y - 3);
                            path.AddLine(DLLine1, DLLine2);

                            // #### Left Down Handle Border
                            PointF DLHLine1 = DLLine2;
                            PointF DLHLine2 = new(DLHLine1.X + 4, DLHLine1.Y + 8);
                            path.AddLine(DLHLine1, DLHLine2);

                            // #### Down Handle Border
                            PointF DHLine1 = DLHLine2;
                            PointF DHLine2 = new(DHLine1.X + 2, DHLine1.Y - 1);
                            path.AddArc(DHLine1.X, DHLine1.Y - 1, DHLine2.X - DHLine1.X, 1, 180, -100);

                            // #### Right Down Handle Border
                            PointF DRHLine1 = DHLine2;
                            PointF DRHLine2 = new(DLHLine1.X + 3, DLHLine1.Y);
                            path.AddLine(DRHLine1, DRHLine2);

                            // #### Right Down Border
                            PointF DRLine1 = DRHLine2 + (Size)new Point(0, -1);
                            PointF DRLine2 = new(R.X + R.Width - 1, DRLine1.Y);
                            path.AddLine(DRLine1, DRLine2);

                            // #### Right Border
                            PointF RLine1 = DRLine2 + (Size)new Point(-1, -1);
                            PointF RLine2 = LLine1;
                            path.AddLine(RLine1, RLine2);
                            break;
                        }

                    case ArrowStyle.Modern:
                        {
                            // #### Left Border
                            PointF LLine1 = new(R.X, R.Y);
                            PointF LLine2 = new(R.X, R.Y + R.Height - 2);
                            path.AddLine(LLine1, LLine2);

                            // #### Left Down Border
                            PointF DLLine1 = LLine2 + (Size)new Point(1, 0);
                            PointF DLLine2 = new(DLLine1.X + 4, DLLine1.Y - 4);
                            path.AddLine(DLLine1, DLLine2);

                            // #### Right Down Border
                            PointF DRLine1 = DLLine2;
                            PointF DRLine2 = new(R.X + R.Width - 1, DRLine1.Y);
                            path.AddLine(DRLine1, DRLine2);

                            // #### Right Border
                            PointF RLine1 = DRLine2 + (Size)new Point(0, -1);
                            PointF RLine2 = LLine1;
                            path.AddLine(RLine1, RLine2);
                            break;
                        }
                }

                using (Matrix m = new())
                {
                    m.Scale(Scale, Scale, MatrixOrder.Append);
                    m.Translate(1f, 1f, MatrixOrder.Append);
                    path.Transform(m);
                }

                return (GraphicsPath)path.Clone();
            }
        }

        public static GraphicsPath Busy(RectangleF Rectangle, float Angle, CircleStyle Style, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            using (Matrix m = new())
            {
                switch (Style)
                {
                    case CircleStyle.Aero:
                        {
                            Rectangle.Width = 19;
                            Rectangle.Height = 19;

                            path.AddEllipse(Rectangle);
                            RectangleF R = new(Rectangle.X + 4, Rectangle.Y + 4, 11, 11);
                            path.AddEllipse(R);
                            path.CloseFigure();
                            break;
                        }

                    case CircleStyle.Classic:
                        {
                            Rectangle.Width = 22;
                            Rectangle.Height = 22;

                            path.AddLine(new PointF(Rectangle.X + 12, Rectangle.Y + 0), new PointF(Rectangle.X + 6, Rectangle.Y + 0));
                            path.AddLine(new PointF(Rectangle.X + 6, Rectangle.Y + 0), new PointF(Rectangle.X + 6, Rectangle.Y + 2));
                            path.AddLine(new PointF(Rectangle.X + 6, Rectangle.Y + 2), new PointF(Rectangle.X + 12, Rectangle.Y + 2));

                            path.AddLine(new PointF(Rectangle.X + 7, Rectangle.Y + 2), new PointF(Rectangle.X + 7, Rectangle.Y + 7));
                            path.AddLine(new PointF(Rectangle.X + 8, Rectangle.Y + 7), new PointF(Rectangle.X + 11, Rectangle.Y + 10));
                            path.AddLine(new PointF(Rectangle.X + 8, Rectangle.Y + 8), new PointF(Rectangle.X + 11, Rectangle.Y + 11));

                            path.AddLine(new PointF(Rectangle.X + 11, Rectangle.Y + 11), new PointF(Rectangle.X + 8, Rectangle.Y + 14));
                            path.AddLine(new PointF(Rectangle.X + 11, Rectangle.Y + 10), new PointF(Rectangle.X + 8, Rectangle.Y + 13));
                            path.AddLine(new PointF(Rectangle.X + 7, Rectangle.Y + 14), new PointF(Rectangle.X + 7, Rectangle.Y + 19));

                            path.AddLine(new PointF(Rectangle.X + 12, Rectangle.Y + 19), new PointF(Rectangle.X + 6, Rectangle.Y + 19));
                            path.AddLine(new PointF(Rectangle.X + 6, Rectangle.Y + 19), new PointF(Rectangle.X + 6, Rectangle.Y + 21));
                            path.AddLine(new PointF(Rectangle.X + 6, Rectangle.Y + 21), new PointF(Rectangle.X + 12, Rectangle.Y + 21));

                            path.StartFigure();
                            path.AddLine(new PointF(Rectangle.X + 7, Rectangle.Y), new PointF(Rectangle.X + 7, Rectangle.Y + 7));

                            path.StartFigure();
                            path.AddLine(new PointF(Rectangle.X + 7, Rectangle.Y + 19), new PointF(Rectangle.X + 7, Rectangle.Y + 21));

                            path.AddPath(MirrorRight(path), false);

                            if (Angle >= 270f)
                            {
                                using (Matrix m_rotate = new())
                                {
                                    RectangleF rectAnimation = path.GetBounds();
                                    m_rotate.RotateAt((Angle - 180f) * 3f, new PointF(rectAnimation.X + rectAnimation.Width / 2f, rectAnimation.Y + rectAnimation.Height / 2f));
                                    path.Transform(m_rotate);
                                }
                            }

                            break;
                        }
                }

                m.Scale(Scale, Scale, MatrixOrder.Append);
                m.Translate(1f, 1f, MatrixOrder.Append);
                path.Transform(m);

                return (GraphicsPath)path.Clone();
            }
        }

        public static GraphicsPath BusyLoader(RectangleF Rectangle, float Angle, CircleStyle Style, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            using (Matrix m = new())
            {
                Rectangle.X += 0;
                Rectangle.Y += 0;

                Rectangle.Width = 19;
                Rectangle.Height = 19;

                PointF CPointF = new(Rectangle.X + Rectangle.Width / 2f, Rectangle.Y + Rectangle.Height / 2f);
                RectangleF R = new(Rectangle.X + 4, Rectangle.Y + 4, 11, 11);

                int innerR = 15;
                int thickness = 10;
                int arcLength = 70;
                int outerR = innerR + thickness;

                switch (Style)
                {
                    case CircleStyle.Aero:
                        {
                            Rectangle.Width = 19;
                            Rectangle.Height = 19;

                            RectangleF outerRect = Rectangle;
                            RectangleF innerRect = R;
                            path.AddArc(outerRect, Angle, arcLength);
                            path.AddArc(innerRect, Angle + arcLength, -arcLength);
                            path.CloseFigure();
                            break;
                        }

                    case CircleStyle.Classic:
                        {
                            Rectangle.Width = 22;
                            Rectangle.Height = 22;

                            PointF PU1 = new(Rectangle.X + 12, Rectangle.Y + 5);

                            PointF PU3 = new(Rectangle.X + 10, Rectangle.Y + 5);
                            PointF PU4 = PU3 + (Size)new Point(2, 2);
                            path.AddLine(PU3, PU4);
                            path.CloseFigure();

                            PointF PL1 = new(Rectangle.X + 12, Rectangle.Y + 17);
                            PointF PL2 = PL1 - (Size)new Point(1, -1);

                            PointF PL3 = PL1 - (Size)new Point(0, 2);
                            PointF PL4 = PL3 - (Size)new Point(3, -3);

                            path.AddLine(PL1, PL2);
                            path.CloseFigure();

                            path.AddLine(PL3, PL4);
                            path.CloseFigure();

                            path.AddPath(MirrorRight(path), false);

                            PointF C1 = PU1 + (Size)new Point(1, 1);
                            path.AddLine(PU1, PU1 + (Size)new Point(1, 1));
                            path.CloseFigure();

                            PointF C2 = PU4 + (Size)new Point(0, 2);
                            path.AddLine(C2, C2 + (Size)new Point(-1, 1));
                            path.CloseFigure();

                            if (Angle >= 270f)
                            {
                                using (Matrix m_rotate = new())
                                {
                                    RectangleF rectAnimation = path.GetBounds();
                                    m_rotate.RotateAt((Angle - 180f) * 3f, new PointF(rectAnimation.X + rectAnimation.Width / 2f, rectAnimation.Y + rectAnimation.Height / 2f));
                                    path.Transform(m_rotate);
                                }
                            }

                            break;
                        }
                }

                m.Scale(Scale, Scale, MatrixOrder.Append);
                m.Translate(1f, 1f, MatrixOrder.Append);
                path.Transform(m);

                return (GraphicsPath)path.Clone();
            }
        }

        public static GraphicsPath AppLoading(RectangleF Rectangle, float Angle, CircleStyle Style, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            using (Matrix m = new())
            {
                Rectangle.Width = 15;
                Rectangle.Height = 15;

                switch (Style)
                {
                    case CircleStyle.Aero:
                        {
                            path.AddEllipse(Rectangle);
                            RectangleF R = new(Rectangle.X + 3, Rectangle.Y + 3, 9, 9);
                            path.AddEllipse(R);
                            path.CloseFigure();
                            break;
                        }

                    case CircleStyle.Classic:
                        {
                            RectangleF UpperRectangle = new(Rectangle.X + 12, Rectangle.Y + 9, 9, 2);
                            RectangleF LowerRectangle = new(Rectangle.X + 12, Rectangle.Y + 23, 9, 2);
                            RectangleF Container = new(UpperRectangle.X, UpperRectangle.Y, UpperRectangle.Width, LowerRectangle.Bottom - UpperRectangle.Top);

                            PointF pL1 = new(UpperRectangle.X + 1, UpperRectangle.Y + UpperRectangle.Height);
                            PointF pL2 = new(pL1.X, pL1.Y + 4);
                            path.AddLine(pL1, pL2);

                            PointF pL3 = pL2 + (Size)new Point(1, 0);
                            PointF pL4 = pL3 + (Size)new Point(1, 1);
                            path.AddLine(pL3, pL4);

                            PointF pL3x = pL3 + (Size)new Point(0, 1);
                            PointF pL4x = pL4 + (Size)new Point(0, 1);
                            path.AddLine(pL3x, pL4x);

                            PointF pL5 = pL4 + (Size)new Point(0, 2);
                            PointF pL6 = pL5 + (Size)new Point(-1, 1);
                            path.AddLine(pL5, pL6);

                            PointF pL5x = pL5 + (Size)new Point(0, -1);
                            PointF pL6x = pL6 + (Size)new Point(0, -1);
                            path.AddLine(pL5x, pL6x);

                            PointF pL8 = pL6 - (Size)new Point(1, 0);
                            PointF pL9 = pL8 + (Size)new Point(0, 4);
                            path.AddLine(pL8, pL9);

                            PointF pL10 = pL9 + (Size)new Point(7, 0);
                            PointF pL11 = pL10 - (Size)new Point(0, 4);
                            path.AddLine(pL10, pL11);

                            PointF pL12 = pL11 + (Size)new Point(-1, 0);
                            PointF pL13 = pL12 + (Size)new Point(-1, -1);
                            path.AddLine(pL12, pL13);

                            PointF pL12x = pL12 + (Size)new Point(0, -1);
                            PointF pL13x = pL13 + (Size)new Point(0, -1);
                            path.AddLine(pL12x, pL13x);

                            PointF pL15 = pL13 + (Size)new Point(0, -2);
                            PointF pl16 = pL15 + (Size)new Point(1, -1);
                            path.AddLine(pL15, pl16);

                            PointF pL15x = pL15 + (Size)new Point(1, 0);
                            PointF pl16x = pl16 + (Size)new Point(1, 0);
                            path.AddLine(pL15x, pl16x);

                            PointF pL17 = pl16x + (Size)new Point(0, 0);
                            PointF pL18 = pL17 + (Size)new Point(0, -4);
                            path.AddLine(pL17, pL18);

                            path.AddRectangle(UpperRectangle);

                            path.AddRectangle(LowerRectangle);

                            path.CloseFigure();

                            path.StartFigure();
                            path.AddLine(new PointF(UpperRectangle.X + 1, UpperRectangle.Y), new PointF(UpperRectangle.X + 1, UpperRectangle.Y + UpperRectangle.Height));

                            path.StartFigure();
                            path.AddLine(new PointF(UpperRectangle.X + UpperRectangle.Width - 1, UpperRectangle.Y), new PointF(UpperRectangle.X + UpperRectangle.Width - 1, UpperRectangle.Y + UpperRectangle.Height));

                            path.StartFigure();
                            path.AddLine(new PointF(LowerRectangle.X + 1, LowerRectangle.Y), new PointF(LowerRectangle.X + 1, LowerRectangle.Y + LowerRectangle.Height));

                            path.StartFigure();
                            path.AddLine(new PointF(LowerRectangle.X + LowerRectangle.Width - 1, LowerRectangle.Y), new PointF(LowerRectangle.X + LowerRectangle.Width - 1, LowerRectangle.Y + LowerRectangle.Height));

                            if (Angle >= 270f)
                            {
                                using (Matrix m_rotate = new())
                                {
                                    RectangleF rectAnimation = path.GetBounds();
                                    m_rotate.RotateAt((Angle - 180f) * 3f, new PointF(rectAnimation.X + rectAnimation.Width / 2f, rectAnimation.Y + rectAnimation.Height / 2f));
                                    path.Transform(m_rotate);
                                }
                            }

                            break;
                        }
                }

                m.Scale(Scale, Scale, MatrixOrder.Append);
                m.Translate(1f, 1f, MatrixOrder.Append);
                path.Transform(m);

                return (GraphicsPath)path.Clone();
            }
        }

        public static GraphicsPath AppLoaderCircle(RectangleF Rectangle, float Angle, CircleStyle Style, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            using (Matrix m = new())
            {
                Rectangle.Width = 15;
                Rectangle.Height = 15;

                PointF CPointF = new(Rectangle.X + Rectangle.Width / 2f, Rectangle.Y + Rectangle.Height / 2f);
                RectangleF R = new(Rectangle.X + 3, Rectangle.Y + 3, 9, 9);

                int innerR = 15;
                int thickness = 4;
                int arcLength = 70;
                int outerR = innerR + thickness;

                switch (Style)
                {
                    case CircleStyle.Aero:
                        {
                            RectangleF outerRect = Rectangle;
                            RectangleF innerRect = R;
                            path.AddArc(outerRect, Angle, arcLength);
                            path.AddArc(innerRect, Angle + arcLength, -arcLength);
                            break;
                        }

                    case CircleStyle.Classic:
                        {
                            RectangleF UpperRectangle = new(Rectangle.X + 12, Rectangle.Y + 9, 9, 2);
                            RectangleF LowerRectangle = new(Rectangle.X + 12, Rectangle.Y + 23, 9, 2);
                            RectangleF Container = new(UpperRectangle.X, UpperRectangle.Y, UpperRectangle.Width, LowerRectangle.Bottom - UpperRectangle.Top);

                            PointF PU1 = new(Rectangle.X + 17, Rectangle.Y + 14);
                            PointF PU3 = new(Rectangle.X + 15, Rectangle.Y + 16);
                            PointF PU4 = PU3 + (Size)new Point(1, 1);

                            path.AddLine(PU1, PU3);
                            path.CloseFigure();

                            path.AddLine(PU3, PU4);
                            path.CloseFigure();

                            path.StartFigure();
                            PointF PL1 = new(Rectangle.X + 16, Rectangle.Y + 20);
                            PointF PL2 = PL1 - (Size)new Point(2, -2);
                            path.AddLine(PL1, PL2);
                            path.CloseFigure();

                            PointF PL3 = PL1 + (Size)new Point(1, 1);
                            PointF PL4 = PL3 - (Size)new Point(1, -1);
                            path.AddLine(PL3, PL4);
                            path.CloseFigure();

                            PointF C1 = PL3 + (Size)new Point(1, 1);
                            path.AddLine(PL3, C1);
                            path.CloseFigure();

                            if (Angle >= 270f)
                            {
                                using (Matrix m_rotate = new())
                                {
                                    RectangleF rectAnimation = path.GetBounds();
                                    m_rotate.RotateAt((Angle - 180f) * 3f, new PointF(rectAnimation.X + rectAnimation.Width / 2f, rectAnimation.Y + rectAnimation.Height / 2f));
                                    path.Transform(m_rotate);
                                }
                            }

                            break;
                        }
                }

                path.CloseFigure();

                m.Scale(Scale, Scale, MatrixOrder.Append);
                m.Translate(1f, 1f, MatrixOrder.Append);
                path.Transform(m);

                return (GraphicsPath)path.Clone();
            }
        }

        public static GraphicsPath Move(RectangleF Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.Width = 21;
                Rectangle.Height = 21;
                Rectangle.X = 0;
                Rectangle.Y = 0;

                PointF UL1 = new(Rectangle.X + 11, Rectangle.Y);
                PointF UL2 = new(UL1.X - 4, Rectangle.Y + 4);
                path.AddLine(UL1, UL2);

                PointF ULX1 = new(UL2.X, UL2.Y + 1);
                PointF ULX2 = new(ULX1.X + 3, ULX1.Y);
                path.AddLine(ULX1, ULX2);

                PointF MUL1 = ULX2;
                PointF MUL2 = new(MUL1.X, ULX2.Y + 4);
                path.AddLine(MUL1, MUL2);

                PointF MULX1 = new(MUL2.X, MUL2.Y + 1);
                PointF MULX2 = new(MULX1.X - 5, MULX1.Y);
                path.AddLine(MULX1, MULX2);

                PointF LU1 = MULX2;
                PointF LU2 = new(MULX2.X, MULX2.Y - 3);
                path.AddLine(LU1, LU2);

                PointF LUX1 = new(LU2.X - 1, LU2.Y);
                PointF LUX2 = new(Rectangle.X, Rectangle.Y + 11);
                path.AddLine(LUX1, LUX2);

                PointF LDX1 = LUX2;
                PointF LDX2 = new(LDX1.X + 4, LDX1.Y + 4);
                path.AddLine(LDX1, LDX2);

                PointF LD1 = new(LDX2.X + 1, LDX2.Y);
                PointF LD2 = new(LD1.X, LD1.Y - 2);
                path.AddLine(LD1, LD2);

                PointF L1 = new(LD2.X, LD2.Y - 1);
                PointF L2 = new(L1.X + 5, L1.Y);
                path.AddLine(L1, L2);

                PointF DL1 = L2;
                PointF DL2 = new(L2.X, LD2.Y + 3);
                path.AddLine(DL1, DL2);

                PointF DX1 = new(DL2.X, DL2.Y + 1);
                PointF DX2 = new(DX1.X - 3, DX1.Y);
                path.AddLine(DX1, DX2);

                PointF DLX1 = new(DX2.X, DX2.Y + 1);
                PointF DLX2 = new(DLX1.X + 4, DLX1.Y + 4);
                path.AddLine(DLX1, DLX2);

                path.AddPath(MirrorRight(path), false);

                using (Matrix m = new())
                {
                    m.Scale(Scale, Scale, MatrixOrder.Append);
                    m.Translate(1f, 1f, MatrixOrder.Append);
                    path.Transform(m);
                }

                return (GraphicsPath)path.Clone();
            }
        }

        public static GraphicsPath Help(RectangleF Rectangle, ArrowStyle Style, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                FontFamily F;

                switch (Style)
                {
                    case ArrowStyle.Classic:
                        {
                            F = new("Marlett");
                            using (Font Fx = new(F, 21f, FontStyle.Bold)) { path.AddString("s", Fx.FontFamily, (int)Fx.Style, Fx.Size, Rectangle, StringFormat.GenericDefault); }

                            break;
                        }

                    default:
                        {
                            string fontName;

                            if (OS.WXP) { fontName = "Tahoma"; }
                            else if (OS.W7 | OS.WVista) { fontName = "Segoe UI"; }
                            else { fontName = Fonts.Exists("Segoe UI Black") ? "Segoe UI Black" : "Segoe UI"; }

                            F = new(Fonts.Exists(fontName) ? fontName : "Arial");

                            using (Font Fx = new(F, 15f, FontStyle.Bold)) { path.AddString("?", Fx.FontFamily, (int)Fx.Style, Fx.Size, Rectangle, StringFormat.GenericDefault); }

                            break;
                        }
                }

                using (Matrix m = new())
                {
                    m.Scale(Scale, Scale, MatrixOrder.Append);
                    m.Translate(1f, 1f, MatrixOrder.Append);
                    path.Transform(m);
                }

                F.Dispose();

                return (GraphicsPath)path.Clone();
            }
        }

        public static GraphicsPath None(RectangleF Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.Width = 17;
                Rectangle.Height = 17;
                Rectangle.X = 0;
                Rectangle.Y = 0;

                RectangleF R = new(Rectangle.X + 2, Rectangle.Y + 2, Rectangle.Width - 4, Rectangle.Height - 4);

                path.AddArc(R, 50f, 160f);
                path.CloseFigure();

                path.AddArc(R, 230f, 160f);
                path.CloseFigure();

                path.AddEllipse(Rectangle);

                using (Matrix m = new())
                {
                    m.Scale(Scale, Scale, MatrixOrder.Append);
                    m.Translate(1f, 1f, MatrixOrder.Append);
                    path.Transform(m);
                }

                return (GraphicsPath)path.Clone();
            }
        }

        public static GraphicsPath NoneBackground(RectangleF Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.Width = 17;
                Rectangle.Height = 17;
                Rectangle.X = 0;
                Rectangle.Y = 0;

                path.AddEllipse(Rectangle);

                using (Matrix m = new())
                {
                    m.Scale(Scale, Scale, MatrixOrder.Append);
                    m.Translate(1f, 1f, MatrixOrder.Append);
                    path.Transform(m);
                }

                return (GraphicsPath)path.Clone();
            }
        }

        public static GraphicsPath Up(RectangleF Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.Width = 9;
                Rectangle.Height = 19;
                Rectangle.X = 0;
                Rectangle.Y = 0;

                PointF UL1 = new(Rectangle.X + 4, Rectangle.Y);
                PointF UL2 = new(Rectangle.X, Rectangle.Y + 4);
                path.AddLine(UL1, UL2);

                PointF ULX1 = new(UL2.X, UL2.Y + 1);
                PointF ULX2 = new(ULX1.X + 3, ULX1.Y);
                path.AddLine(ULX1, ULX2);

                PointF MUL1 = ULX2;
                PointF MUL2 = new(MUL1.X, MUL1.Y + 12);
                path.AddLine(MUL1, MUL2);

                PointF D1 = new(MUL2.X, MUL2.Y + 1);
                PointF D2 = new(D1.X + 1, D1.Y);
                path.AddLine(D1, D2);

                path.AddPath(MirrorRight(path), false);

                using (Matrix m = new())
                {
                    m.Scale(Scale, Scale, MatrixOrder.Append);
                    m.Translate(1f, 1f, MatrixOrder.Append);
                    path.Transform(m);
                }

                return (GraphicsPath)path.Clone();
            }
        }

        public static GraphicsPath NS(RectangleF Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.Width = 9;
                Rectangle.Height = 23;
                Rectangle.X = 0;
                Rectangle.Y = 0;

                PointF UL1 = new(Rectangle.X + 4, Rectangle.Y);
                PointF UL2 = new(Rectangle.X, Rectangle.Y + 4);
                path.AddLine(UL1, UL2);

                PointF ULX1 = new(UL2.X, UL2.Y + 1);
                PointF ULX2 = new(ULX1.X + 3, ULX1.Y);
                path.AddLine(ULX1, ULX2);

                PointF MUL1 = ULX2;
                PointF MUL2 = new(MUL1.X, MUL1.Y + 12);
                path.AddLine(MUL1, MUL2);

                PointF DL1 = MUL2;
                PointF DL2 = new(MUL2.X - 3, MUL2.Y);
                path.AddLine(DL1, DL2);

                PointF DX1 = new(DL2.X, DL2.Y + 1);
                PointF DX2 = new(DX1.X + 4, DX1.Y + 4);
                path.AddLine(DX1, DX2);

                path.AddPath(MirrorRight(path), false);

                using (Matrix m = new())
                {
                    m.Scale(Scale, Scale, MatrixOrder.Append);
                    m.Translate(1f, 1f, MatrixOrder.Append);
                    path.Transform(m);
                }

                return (GraphicsPath)path.Clone();
            }
        }

        public static GraphicsPath NESW(RectangleF Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.Width = 17;
                Rectangle.Height = 17;
                Rectangle.X = 0;
                Rectangle.Y = 0;

                PointF UR1 = new(Rectangle.X + Rectangle.Width - 1, Rectangle.Y);
                PointF UR2 = new(UR1.X - 6, UR1.Y);
                path.AddLine(UR1, UR2);

                PointF RX1 = new(UR2.X, UR2.Y + 1);
                PointF RX2 = new(RX1.X + 1, RX1.Y + 1);
                path.AddLine(RX1, RX2);

                PointF LX1 = new(RX2.X + 1, RX2.Y + 1);
                PointF LX2 = new(LX1.X - 9, LX1.Y + 9);
                path.AddLine(LX1, LX2);

                PointF DX1 = new(LX2.X - 1, LX2.Y - 1);
                PointF DX2 = new(DX1.X - 1, DX1.Y - 1);
                path.AddLine(DX1, DX2);

                PointF L1 = new(DX2.X - 1, DX2.Y);
                PointF L2 = new(L1.X, L1.Y + 6);
                path.AddLine(L1, L2);

                PointF D1 = new(L2.X + 1, L2.Y);
                PointF D2 = new(D1.X + 5, D1.Y);
                path.AddLine(D1, D2);

                PointF DL1 = new(D2.X, D2.Y - 1);
                PointF DL2 = new(DL1.X - 1, DL1.Y - 1);
                path.AddLine(DL1, DL2);

                PointF LX3 = new(DL2.X - 1, DL2.Y - 1);
                PointF LX4 = new(LX3.X + 9, LX3.Y - 9);
                path.AddLine(LX3, LX4);

                PointF DR1 = new(LX4.X + 1, LX4.Y + 1);
                PointF DR2 = new(DR1.X + 1, DR1.Y + 1);
                path.AddLine(DR1, DR2);

                PointF R1 = new(DR2.X + 1, DR2.Y);
                PointF R2 = new(R1.X, R1.Y - 6);
                path.AddLine(R1, R2);

                using (Matrix m = new())
                {
                    m.Scale(Scale, Scale, MatrixOrder.Append);
                    m.Translate(1f, 1f, MatrixOrder.Append);
                    path.Transform(m);
                }

                return (GraphicsPath)path.Clone();
            }
        }

        public static GraphicsPath NWSE(RectangleF Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = NESW(Rectangle))
            {
                Rectangle.Width = 17;
                Rectangle.Height = 17;
                Rectangle.X = 0;
                Rectangle.Y = 0;

                Matrix flipXMatrix = new(-1, 0f, 0f, 1f, Rectangle.Width, -1);
                Matrix transformMatrix = new();
                transformMatrix.Multiply(flipXMatrix);
                path.Transform(transformMatrix);

                using (Matrix m = new())
                {
                    m.Scale(Scale, Scale, MatrixOrder.Append);
                    m.Translate(1f, 1f, MatrixOrder.Append);
                    path.Transform(m);
                }

                flipXMatrix.Dispose();
                transformMatrix.Dispose();

                return (GraphicsPath)path.Clone();
            }
        }

        public static GraphicsPath EW(RectangleF Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.Width = 23;
                Rectangle.Height = 9;
                Rectangle.X = 0;
                Rectangle.Y = 0;

                PointF L1 = new(Rectangle.X, Rectangle.Y + 4);
                PointF L2 = new(L1.X + 4, L1.Y - 4);
                path.AddLine(L1, L2);

                PointF LX1 = new(L2.X + 1, L2.Y);
                PointF LX2 = new(LX1.X, LX1.Y + 2);
                path.AddLine(LX1, LX2);

                PointF U1 = new(LX2.X, LX2.Y + 1);
                PointF U2 = new(U1.X + 12, U1.Y);
                path.AddLine(U1, U2);

                PointF RX1 = new(U2.X, U2.Y - 1);
                PointF RX2 = new(RX1.X, RX1.Y - 2);
                path.AddLine(RX1, RX2);

                PointF R1 = new(RX2.X + 1, RX2.Y);
                PointF R2 = new(R1.X + 4, R1.Y + 4);
                path.AddLine(R1, R2);

                PointF R3 = new(R2.X, R2.Y);
                PointF R4 = new(R3.X - 4, R3.Y + 4);
                path.AddLine(R3, R4);

                PointF RX3 = new(R4.X - 1, R4.Y);
                PointF RX4 = new(RX3.X, RX3.Y - 2);
                path.AddLine(RX3, RX4);

                PointF D1 = new(RX4.X, RX4.Y - 1);
                PointF D2 = new(D1.X - 12, D1.Y);
                path.AddLine(D1, D2);

                PointF LX3 = new(D2.X, D2.Y + 1);
                PointF LX4 = new(LX3.X, LX3.Y + 2);
                path.AddLine(LX3, LX4);

                PointF L3 = new(LX4.X - 1, LX4.Y);
                PointF L4 = new(L3.X - 4, L3.Y - 4);
                path.AddLine(L3, L4);

                using (Matrix m = new())
                {
                    m.Scale(Scale, Scale, MatrixOrder.Append);
                    m.Translate(1f, 1f, MatrixOrder.Append);
                    path.Transform(m);
                }

                return (GraphicsPath)path.Clone();
            }
        }

        public static GraphicsPath Pen(RectangleF Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.Width = 22;
                Rectangle.Height = 22;
                Rectangle.X = 0;
                Rectangle.Y = 0;

                PointF T1 = new(Rectangle.X, Rectangle.Y);
                PointF T2 = T1 + (Size)new Point(6, 2);
                path.AddLine(T1, T2);

                PointF R1 = new(T2.X, T2.Y);
                PointF R2 = new(R1.X + 15, R1.Y + 15);
                path.AddLine(R1, R2);

                PointF B1 = new(R2.X, R2.Y + 1);
                PointF B2 = new(B1.X - 3, B1.Y + 3);
                path.AddLine(B1, B2);

                PointF L1 = new(B2.X - 1, B2.Y);
                PointF L2 = new(L1.X - 15, L1.Y - 15);
                path.AddLine(L1, L2);

                PointF LX1 = new(L2.X, L2.Y);
                path.AddLine(LX1, T1);

                path.CloseFigure();

                PointF S1 = new(Rectangle.X + 14, Rectangle.Y + 18);
                PointF S2 = new(S1.X + 4, S1.Y - 4);
                path.AddLine(S2, S1);

                using (Matrix m = new())
                {
                    m.Scale(Scale, Scale, MatrixOrder.Append);
                    m.Translate(1f, 1f, MatrixOrder.Append);
                    path.Transform(m);
                }

                return (GraphicsPath)path.Clone();
            }
        }

        public static GraphicsPath PenBackground(RectangleF Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.Width = 22;
                Rectangle.Height = 22;
                Rectangle.X = 0;
                Rectangle.Y = 0;

                PointF T1 = new(Rectangle.X, Rectangle.Y);
                PointF T2 = T1 + (Size)new Point(6, 2);
                path.AddLine(T1, T2);

                PointF R1 = new(T2.X, T2.Y);
                PointF R2 = new(R1.X + 15, R1.Y + 15);
                path.AddLine(R1, R2);

                PointF B1 = new(R2.X, R2.Y + 1);
                PointF B2 = new(B1.X - 3, B1.Y + 3);
                path.AddLine(B1, B2);

                PointF L1 = new(B2.X - 1, B2.Y);
                PointF L2 = new(L1.X - 15, L1.Y - 15);
                path.AddLine(L1, L2);

                PointF LX1 = new(L2.X, L2.Y);
                path.AddLine(LX1, T1);

                path.CloseFigure();

                using (Matrix m = new())
                {
                    m.Scale(Scale, Scale, MatrixOrder.Append);
                    m.Translate(1f, 1f, MatrixOrder.Append);
                    path.Transform(m);
                }

                return (GraphicsPath)path.Clone();
            }
        }

        public static GraphicsPath Hand(RectangleF Rectangle, ArrowStyle Style, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.Width = 18;
                Rectangle.Height = 24;
                Rectangle.X = 0;
                Rectangle.Y = 0;

                switch (Style)
                {
                    case ArrowStyle.Classic:
                        {
                            PointF Index_LB1 = new(Rectangle.X + 5, Rectangle.Y + 14);
                            PointF Index_LB2 = new(Index_LB1.X, Index_LB1.Y - 11);
                            path.AddLine(Index_LB1, Index_LB2);

                            PointF Index_RB1 = new(Index_LB1.X + 3, Index_LB1.Y - 4);
                            PointF Index_RB2 = new(Index_RB1.X, Index_RB1.Y - 8);
                            path.AddArc(Index_LB2.X, Index_LB2.Y - 2, 3, 2, 180f, 180f);

                            path.AddLine(Index_RB1, Index_RB2);
                            path.AddArc(Index_RB2.X, Index_LB2.Y + 3, 3, 2, 180f, 180f);

                            PointF Middle_RB1 = new(Index_RB1.X + 3, Index_RB1.Y);
                            PointF Middle_RB2 = new(Middle_RB1.X, Middle_RB1.Y - 3);
                            path.AddLine(Middle_RB1, Middle_RB2);
                            path.AddArc(Middle_RB2.X, Index_LB2.Y + 4, 3, 2, 180f, 180f);

                            PointF Ring_RB1 = new(Middle_RB1.X + 3, Middle_RB1.Y + 1);
                            PointF Ring_RB2 = new(Ring_RB1.X, Ring_RB1.Y - 2);
                            path.AddLine(Ring_RB1, Ring_RB2);
                            path.AddArc(Ring_RB2.X, Index_LB2.Y + 6, 3, 2, 180f, 180f);

                            PointF FreeBorder1 = new(Ring_RB1.X + 3, Ring_RB1.Y - 1);
                            PointF FreeBorder2 = new(FreeBorder1.X, FreeBorder1.Y + 6);
                            path.AddLine(FreeBorder1, FreeBorder2);

                            PointF LW1 = FreeBorder2 + (Size)new Point(0, 1);
                            PointF RW1 = new(LW1.X - 14, LW1.Y);
                            RectangleF Btm = new(RW1.X + 3, RW1.Y - 8, 9, 13);
                            path.AddLine(FreeBorder2, new PointF(Btm.X + Btm.Width, Btm.Y + Btm.Height));
                            path.AddLine(new PointF(Btm.X + Btm.Width, Btm.Y + Btm.Height), new PointF(Btm.X, Btm.Y + Btm.Height));

                            PointF L1 = RW1 - (Size)new Point(0, 1);
                            PointF L2 = new(L1.X - 1, L1.Y - 3);
                            RectangleF Thumb = new(L2.X - 1, L2.Y - 3, 2, 3);
                            path.AddArc(Thumb, 90f, 180f);

                            PointF LastBorder1 = new(Thumb.X + Thumb.Width, Thumb.Y);
                            PointF LastBorder2 = new(LastBorder1.X + 2, LastBorder1.Y + 1);
                            path.AddLine(LastBorder1, LastBorder2);

                            path.CloseFigure();
                            break;
                        }

                    default:
                        {
                            PointF Index_LB1 = new(Rectangle.X + 5, Rectangle.Y + 14);
                            PointF Index_LB2 = new(Index_LB1.X, Index_LB1.Y - 12);
                            path.AddLine(Index_LB1, Index_LB2);

                            PointF Index_RB1 = new(Index_LB1.X + 3, Index_LB1.Y - 4);
                            PointF Index_RB2 = new(Index_RB1.X, Index_RB1.Y - 8);

                            path.AddArc(Index_LB2.X, Index_LB2.Y - 2, 3, 2, 180f, 180f);

                            path.AddLine(Index_RB1, Index_RB2);

                            path.AddArc(Index_RB2.X, Index_LB2.Y + 3, 3, 2, 180f, 180f);

                            PointF Middle_RB1 = new(Index_RB1.X + 3, Index_RB1.Y);
                            PointF Middle_RB2 = new(Middle_RB1.X, Middle_RB1.Y - 3);
                            path.AddLine(Middle_RB1, Middle_RB2);

                            path.AddArc(Middle_RB2.X, Index_LB2.Y + 4, 3, 2, 180f, 180f);

                            PointF Ring_RB1 = new(Middle_RB1.X + 3, Middle_RB1.Y);
                            PointF Ring_RB2 = new(Ring_RB1.X, Ring_RB1.Y - 2);
                            path.AddLine(Ring_RB1, Ring_RB2);

                            path.AddArc(Ring_RB2.X, Index_LB2.Y + 5, 3, 2, 180f, 180f);

                            PointF FreeBorder1 = new(Ring_RB1.X + 3, Ring_RB1.Y - 1);
                            PointF FreeBorder2 = new(FreeBorder1.X, FreeBorder1.Y + 8);
                            path.AddLine(FreeBorder1, FreeBorder2);

                            PointF LW1 = FreeBorder2 + (Size)new Point(0, 1);
                            PointF RW1 = new(LW1.X - 14, LW1.Y);
                            RectangleF Btm = new(RW1.X, RW1.Y - 8, 14, 13);
                            path.AddArc(Btm, 0f, 180f);

                            PointF L1 = RW1 - (Size)new Point(0, 1);
                            PointF L2 = new(L1.X - 2, L1.Y - 2);
                            RectangleF Thumb = new(L2.X - 1, L2.Y - 3, 2, 3);
                            path.AddArc(Thumb, 90f, 180f);

                            PointF LastBorder1 = new(Thumb.X + Thumb.Width, Thumb.Y);
                            PointF LastBorder2 = new(LastBorder1.X + 2, LastBorder1.Y + 1);
                            path.AddLine(LastBorder1, LastBorder2);

                            path.CloseFigure();
                            break;
                        }
                }

                using (Matrix m = new())
                {
                    m.Scale(Scale, Scale, MatrixOrder.Append);
                    m.Translate(1f, 1f, MatrixOrder.Append);
                    path.Transform(m);
                }

                return (GraphicsPath)path.Clone();
            }
        }

        public static GraphicsPath Pin(RectangleF Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.Width = 13;
                Rectangle.Height = 20;
                Rectangle.X = 15;
                Rectangle.Y = 11;

                RectangleF U = new(Rectangle.X, Rectangle.Y, 12, 10);
                path.AddArc(U, 180f, 180f);

                PointF C = new(Rectangle.X + 6, Rectangle.Y + 18);
                PointF p1 = new(Rectangle.X + 0, Rectangle.Y + 6);
                PointF p2 = new(Rectangle.X + 12, Rectangle.Y + 6);
                path.AddLine(p2, C);
                path.AddLine(C, p1);
                path.CloseFigure();

                using (Matrix m = new())
                {
                    m.Scale(Scale, Scale, MatrixOrder.Append);
                    m.Translate(1f, 1f, MatrixOrder.Append);
                    path.Transform(m);
                }

                return (GraphicsPath)path.Clone();
            }
        }

        public static GraphicsPath Person(RectangleF Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.Width = 10;
                Rectangle.Height = 13;
                Rectangle.X = 19;
                Rectangle.Y = 17;

                RectangleF Face = new(Rectangle.X, Rectangle.Y, 5, 6);
                path.AddEllipse(Face);

                RectangleF TrunkUpper = new(Face.X - 2, Face.Y + Face.Height, 9, 9);
                path.AddArc(TrunkUpper, 180f, 180f);

                RectangleF TrunkLower = new(TrunkUpper.X, TrunkUpper.Y + 3, 9, 3);
                path.AddArc(TrunkLower, 0f, 180f);

                using (Matrix m = new())
                {
                    m.Scale(Scale, Scale, MatrixOrder.Append);
                    m.Translate(1f, 1f, MatrixOrder.Append);
                    path.Transform(m);
                }

                return (GraphicsPath)path.Clone();
            }
        }

        public static GraphicsPath IBeam(RectangleF Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.X = 0;
                Rectangle.Y = 0;

                PointF L1 = new(Rectangle.X, Rectangle.Y);
                PointF L2 = new(L1.X, L1.Y + 2);
                path.AddLine(L1, L2);

                PointF BU1 = new(L2.X + 3, L2.Y);
                path.AddLine(L2, BU1);

                PointF LX = new(BU1.X, BU1.Y + 13);
                path.AddLine(BU1, LX);

                PointF BU2 = new(LX.X - 3, LX.Y);
                path.AddLine(LX, BU2);

                PointF L3 = new(BU2.X, BU2.Y + 2);
                path.AddLine(BU2, L3);

                PointF BL = new(L3.X + 3, L3.Y);
                path.AddLine(L3, BL);

                PointF XB = new(BL.X + 1, BL.Y - 1);
                path.AddLine(BL, XB);

                PointF Br = new(XB.X + 1, XB.Y + 1);
                path.AddLine(XB, Br);

                PointF RB = new(Br.X + 3, Br.Y);
                path.AddLine(Br, RB);

                PointF R1 = new(RB.X, RB.Y - 2);
                path.AddLine(RB, R1);

                PointF BU3 = new(R1.X - 3, R1.Y);
                path.AddLine(R1, BU3);

                PointF RX = new(BU3.X, BU3.Y - 13);
                path.AddLine(BU3, RX);

                PointF TU = new(RX.X + 3, RX.Y);
                path.AddLine(RX, TU);

                PointF RR = new(TU.X, TU.Y - 2);
                path.AddLine(TU, RR);

                PointF T = new(RR.X - 3, RR.Y);
                path.AddLine(RR, T);

                PointF Tx = new(T.X - 1, T.Y + 1);
                path.AddLine(T, Tx);

                PointF TXL = new(Tx.X - 1, Tx.Y - 1);
                path.AddLine(Tx, TXL);

                PointF TL = new(TXL.X - 3, TXL.Y);
                path.AddLine(TXL, TL);

                using (Matrix m = new())
                {
                    m.Scale(Scale, Scale, MatrixOrder.Append);
                    m.Translate(1f, 1f, MatrixOrder.Append);
                    path.Transform(m);
                }

                return (GraphicsPath)path.Clone();
            }
        }

        public static GraphicsPath Cross(RectangleF Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.Width = 19;
                Rectangle.Height = 19;

                PointF L1 = new(9, 0);
                PointF L2 = new(L1.X - 1, L1.Y);
                path.AddLine(L1, L2);

                PointF L3 = new(L2.X, L2.Y + 8);
                path.AddLine(L2, L3);

                PointF L4 = new(L3.X - 8, L3.Y);
                path.AddLine(L3, L4);

                PointF L5 = new(L4.X, L4.Y + 2);
                path.AddLine(L4, L5);

                PointF L6 = new(L5.X + 8, L5.Y);
                path.AddLine(L5, L6);

                PointF L7 = new(L6.X, L6.Y + 8);
                path.AddLine(L6, L7);

                PointF L8 = new(L7.X + 1, L7.Y);
                path.AddLine(L7, L8);

                path.AddPath(MirrorRight(path), false);

                using (Matrix m = new())
                {
                    m.Scale(Scale, Scale, MatrixOrder.Append);
                    m.Translate(1f, 1f, MatrixOrder.Append);
                    path.Transform(m);
                }
                return (GraphicsPath)path.Clone();
            }
        }

        public static GraphicsPath Pin_CenterPoint(RectangleF Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.Width = 13;
                Rectangle.Height = 20;
                Rectangle.X = 15;
                Rectangle.Y = 11;

                RectangleF o = new(Rectangle.X, Rectangle.Y, 12, 12);
                RectangleF o1 = new(Rectangle.X, Rectangle.Y, 6, 6);
                o1.X = (int)Math.Round(Rectangle.X + (o.Width - o1.Width) / 2d);
                o1.Y = (int)Math.Round(Rectangle.Y + (o.Height - o1.Height) / 2d);
                path.AddEllipse(o1);

                using (Matrix m = new())
                {
                    m.Scale(Scale, Scale, MatrixOrder.Append);
                    m.Translate(1f, 1f, MatrixOrder.Append);
                    path.Transform(m);
                }
                return (GraphicsPath)path.Clone();
            }
        }

        private static GraphicsPath MirrorRight(GraphicsPath path)
        {
            RectangleF r = path.GetBounds();
            using (GraphicsPath p = (GraphicsPath)path.Clone())
            {
                p.Transform(new Matrix(-1, 0f, 0f, 1f, 2f * (r.Left + r.Width), 0f));
                return (GraphicsPath)p.Clone();
            }
        }
    }

    public class CursorOptions
    {
        public CursorOptions()
        { }

        public CursorOptions(Theme.Structures.Cursor Cursor = default)
        {
            if (Cursor != null)
            {
                UseFromFile = Cursor.UseFromFile;
                File = Cursor.File;
                ArrowStyle = Cursor.ArrowStyle;
                CircleStyle = Cursor.CircleStyle;
                PrimaryColor1 = Cursor.PrimaryColor1;
                PrimaryColor2 = Cursor.PrimaryColor2;
                PrimaryColorGradient = Cursor.PrimaryColorGradient;
                PrimaryColorGradientMode = Cursor.PrimaryColorGradientMode;
                SecondaryColor1 = Cursor.SecondaryColor1;
                SecondaryColor2 = Cursor.SecondaryColor2;
                SecondaryColorGradient = Cursor.SecondaryColorGradient;
                SecondaryColorGradientMode = Cursor.SecondaryColorGradientMode;
                LoadingCircleBack1 = Cursor.LoadingCircleBack1;
                LoadingCircleBack2 = Cursor.LoadingCircleBack2;
                LoadingCircleBackGradient = Cursor.LoadingCircleBackGradient;
                LoadingCircleBackGradientMode = Cursor.LoadingCircleBackGradientMode;
                LoadingCircleHot1 = Cursor.LoadingCircleHot1;
                LoadingCircleHot2 = Cursor.LoadingCircleHot2;
                LoadingCircleHotGradient = Cursor.LoadingCircleHotGradient;
                LoadingCircleHotGradientMode = Cursor.LoadingCircleHotGradientMode;
                PrimaryNoise = Cursor.PrimaryColorNoise;
                PrimaryNoiseOpacity = Cursor.PrimaryColorNoiseOpacity;
                SecondaryNoise = Cursor.SecondaryColorNoise;
                SecondaryNoiseOpacity = Cursor.SecondaryColorNoiseOpacity;
                LoadingCircleBackNoise = Cursor.LoadingCircleBackNoise;
                LoadingCircleBackNoiseOpacity = Cursor.LoadingCircleBackNoiseOpacity;
                LoadingCircleHotNoise = Cursor.LoadingCircleHotNoise;
                LoadingCircleHotNoiseOpacity = Cursor.LoadingCircleHotNoiseOpacity;
                LoadingCircleHot_AnimationSpeed = Cursor.LoadingCircleHot_AnimationSpeed;
                Shadow_Enabled = Cursor.Shadow_Enabled;
                Shadow_Color = Cursor.Shadow_Color;
                Shadow_Blur = Cursor.Shadow_Blur;
                Shadow_Opacity = Cursor.Shadow_Opacity;
                Shadow_OffsetX = Cursor.Shadow_OffsetX;
                Shadow_OffsetY = Cursor.Shadow_OffsetY;
                BorderThickness = Cursor.BorderThickness;
            }
        }

        public bool UseFromFile;
        public string File;
        public Paths.CursorType Cursor;
        public Paths.ArrowStyle ArrowStyle;
        public Paths.CircleStyle CircleStyle;
        public Color PrimaryColor1;
        public Color PrimaryColor2;
        public bool PrimaryColorGradient;
        public Paths.GradientMode PrimaryColorGradientMode;
        public Color SecondaryColor1;
        public Color SecondaryColor2;
        public bool SecondaryColorGradient;
        public Paths.GradientMode SecondaryColorGradientMode;
        public Color LoadingCircleBack1;
        public Color LoadingCircleBack2;
        public bool LoadingCircleBackGradient;
        public Paths.GradientMode LoadingCircleBackGradientMode;
        public Color LoadingCircleHot1;
        public Color LoadingCircleHot2;
        public bool LoadingCircleHotGradient;
        public Paths.GradientMode LoadingCircleHotGradientMode;
        public bool PrimaryNoise;
        public float PrimaryNoiseOpacity;
        public bool SecondaryNoise;
        public float SecondaryNoiseOpacity;
        public bool LoadingCircleBackNoise;
        public float LoadingCircleBackNoiseOpacity;
        public bool LoadingCircleHotNoise;
        public float LoadingCircleHotNoiseOpacity;
        public int LoadingCircleHot_AnimationSpeed;
        public float Scale = 1f;
        public float Angle = 180f;
        public bool Shadow_Enabled = false;
        public Color Shadow_Color = Color.Black;
        public int Shadow_Blur = 5;
        public float Shadow_Opacity = 0.3f;
        public int Shadow_OffsetX = 2;
        public int Shadow_OffsetY = 2;
        public float BorderThickness = 1f;
    }
}