using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

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

        public static Brush ReturnGradience(Rectangle Rectangle, Color Color1, Color Color2, GradientMode GradientMode, float Angle = 0f)
        {

            if (GradientMode == GradientMode.Horizontal)
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
            Bitmap b = new((int)Math.Round(32f * CursorOptions.Scale), (int)Math.Round(32f * CursorOptions.Scale), PixelFormat.Format32bppPArgb);
            Graphics G = Graphics.FromImage(b);

            //G.SmoothingMode = (CursorOptions.ArrowStyle == ArrowStyle.Classic || CursorOptions.CircleStyle == CircleStyle.Classic) ? SmoothingMode.HighSpeed : SmoothingMode.HighQuality;

            G.SmoothingMode = SmoothingMode.AntiAlias;

            G.Clear(Color.Transparent);

            #region Rectangles Helpers
            Rectangle _Arrow = new(0, 0, b.Width, b.Height);
            Rectangle _Help = new(11, 6, b.Width, b.Height);
            Rectangle _Busy = new(0, 0, 22, 22);
            Rectangle _CurRect = new(0, 8, b.Width, b.Height);
            Rectangle _LoadRect = new(6, 0, (int)Math.Round(22f * CursorOptions.Scale), (int)Math.Round(22f * CursorOptions.Scale));
            Rectangle _Pin = new(15, 11, b.Width, b.Height);
            Rectangle _Person = new(19, 17, b.Width, b.Height);
            #endregion

            if (!CursorOptions.UseFromFile || !System.IO.File.Exists(CursorOptions.File))
            {
                switch (CursorOptions.Cursor)
                {
                    case CursorType.Arrow:
                        {
                            Brush BB, BL;
                            if (CursorOptions.PrimaryColorGradient)
                            {
                                BB = ReturnGradience(_Arrow, CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                            }
                            else
                            {
                                BB = new SolidBrush(CursorOptions.PrimaryColor1);
                            }
                            if (CursorOptions.SecondaryColorGradient)
                            {
                                BL = ReturnGradience(_Arrow, CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                            }
                            else
                            {
                                BL = new SolidBrush(CursorOptions.SecondaryColor1);
                            }

                            using (GraphicsPath cursorPath = DefaultCursor(_Arrow, CursorOptions.ArrowStyle, CursorOptions.Scale))
                            {
                                G.FillPath(BB, cursorPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity)))
                                    {
                                        G.FillPath(noise, cursorPath);
                                    }
                                }

                                using (Pen PL = new(BL, CursorOptions.LineThickness)) { G.DrawPath(PL, cursorPath); }

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity)))
                                    using (Pen noisePen = new(noise, CursorOptions.LineThickness))
                                    {
                                        G.DrawPath(noisePen, cursorPath);
                                    }
                                }
                            }

                            BB.Dispose();
                            BL.Dispose();


                            break;
                        }

                    case CursorType.Help:
                        {
                            Brush BB, BL;
                            if (CursorOptions.PrimaryColorGradient)
                            {
                                BB = ReturnGradience(_Arrow, CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                            }
                            else
                            {
                                BB = new SolidBrush(CursorOptions.PrimaryColor1);
                            }
                            if (CursorOptions.SecondaryColorGradient)
                            {
                                BL = ReturnGradience(_Arrow, CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                            }
                            else
                            {
                                BL = new SolidBrush(CursorOptions.SecondaryColor1);
                            }

                            Brush BB_H, BL_H;
                            if (CursorOptions.PrimaryColorGradient)
                            {
                                BB_H = ReturnGradience(_Help, CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                            }
                            else
                            {
                                BB_H = new SolidBrush(CursorOptions.PrimaryColor1);
                            }
                            if (CursorOptions.SecondaryColorGradient)
                            {
                                BL_H = ReturnGradience(_Help, CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                            }
                            else
                            {
                                BL_H = new SolidBrush(CursorOptions.SecondaryColor1);
                            }

                            using (GraphicsPath cursorPath = DefaultCursor(_Arrow, CursorOptions.ArrowStyle, CursorOptions.Scale))
                            {
                                G.FillPath(BB, cursorPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity)))
                                    {
                                        G.FillPath(noise, cursorPath);
                                    }
                                }

                                using (Pen PL = new(BL, CursorOptions.LineThickness)) { G.DrawPath(PL, cursorPath); }

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity)))
                                    using (Pen noisePen = new(noise, CursorOptions.LineThickness))
                                    {
                                        G.DrawPath(noisePen, cursorPath);
                                    }
                                }
                            }

                            using (GraphicsPath helpPath = Help(_Help, CursorOptions.ArrowStyle, CursorOptions.Scale))
                            {
                                G.FillPath(BB_H, helpPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity)))
                                    {
                                        G.FillPath(noise, helpPath);
                                    }
                                }

                                if (CursorOptions.ArrowStyle != ArrowStyle.Classic)
                                    using (Pen PL_H = new(BL_H, CursorOptions.LineThickness))
                                    {
                                        G.DrawPath(PL_H, helpPath);
                                    }

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity)))
                                    using (Pen noisePen = new(noise, CursorOptions.LineThickness))
                                    {
                                        G.DrawPath(noisePen, helpPath);
                                    }
                                }
                            }

                            BB.Dispose();
                            BL.Dispose();
                            BB_H.Dispose();
                            BL_H.Dispose();

                            break;
                        }

                    case CursorType.Busy:
                        {
                            Brush BC, BH;
                            if (CursorOptions.LoadingCircleBackGradient)
                            {
                                BC = ReturnGradience(_Arrow, CursorOptions.LoadingCircleBack1, CursorOptions.LoadingCircleBack2, CursorOptions.LoadingCircleBackGradientMode, CursorOptions.Angle);
                            }
                            else
                            {
                                BC = new SolidBrush(CursorOptions.LoadingCircleBack1);
                            }
                            if (CursorOptions.LoadingCircleHotGradient)
                            {
                                BH = ReturnGradience(_Arrow, CursorOptions.LoadingCircleHot1, CursorOptions.LoadingCircleHot2, CursorOptions.LoadingCircleHotGradientMode, CursorOptions.Angle);
                            }
                            else
                            {
                                BH = new SolidBrush(CursorOptions.LoadingCircleHot1);
                            }

                            using (GraphicsPath busyPath = Busy(_Busy, CursorOptions.Angle, CursorOptions.CircleStyle, CursorOptions.Scale))
                            using (GraphicsPath busyLoaderPath = BusyLoader(_Busy, CursorOptions.Angle, CursorOptions.CircleStyle, CursorOptions.Scale))
                            {
                                if (CursorOptions.CircleStyle == CircleStyle.Classic)
                                {
                                    using (Pen PL = new(BH, CursorOptions.LineThickness))
                                    {
                                        G.FillPath(BC, busyPath);
                                        G.DrawPath(PL, busyLoaderPath);
                                        G.DrawPath(PL, busyPath);
                                    }

                                    if (CursorOptions.LoadingCircleBackNoise)
                                    {
                                        using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.LoadingCircleBackNoiseOpacity)))
                                        {
                                            G.FillPath(noise, busyPath);
                                        }
                                    }

                                    if (CursorOptions.LoadingCircleHotNoise)
                                    {
                                        using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.LoadingCircleHotNoiseOpacity)))
                                        using (Pen noisePen = new(noise, CursorOptions.LineThickness))
                                        {
                                            G.DrawPath(noisePen, busyLoaderPath);
                                            G.DrawPath(noisePen, busyPath);
                                        }
                                    }
                                }

                                else if (CursorOptions.CircleStyle == CircleStyle.Aero)
                                {
                                    G.FillPath(BC, busyPath);

                                    if (CursorOptions.LoadingCircleBackNoise)
                                    {
                                        using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.LoadingCircleBackNoiseOpacity)))
                                        {
                                            G.FillPath(noise, busyPath);
                                        }
                                    }

                                    G.FillPath(BH, busyLoaderPath);

                                    if (CursorOptions.LoadingCircleHotNoise)
                                    {
                                        using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.LoadingCircleHotNoiseOpacity)))
                                        {
                                            G.FillPath(noise, busyLoaderPath);
                                        }
                                    }
                                }

                                else if (CursorOptions.CircleStyle == CircleStyle.Modern)
                                {
                                    float PenWidth = 0.17f * Math.Max(_Arrow.Width, _Arrow.Height);
                                    float _percent = 0.3f;

                                    Rectangle CircleRect = new(_Arrow.X + (int)PenWidth - 2, _Arrow.Y + (int)PenWidth - 2, _Arrow.Width - (int)PenWidth * 2 - 4, _Arrow.Height - (int)PenWidth * 2 - 4);

                                    using (Pen pen = new(BH, PenWidth))
                                    using (Pen pen2 = new(BC, PenWidth))
                                    {
                                        pen.StartCap = LineCap.Round;
                                        pen.EndCap = pen.StartCap;

                                        using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.LoadingCircleBackNoiseOpacity)))
                                        using (Pen noisePen = new(noise, PenWidth))
                                        {
                                            G.DrawArc(pen2, CircleRect, -90, 360);

                                            if (CursorOptions.LoadingCircleBackNoise)
                                            {
                                                G.DrawArc(noisePen, CircleRect, -90, 360);
                                            }
                                        }

                                        using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.LoadingCircleHotNoiseOpacity)))
                                        using (Pen noisePen = new(noise, PenWidth))
                                        {
                                            G.DrawArc(pen, CircleRect, CursorOptions.Angle, (int)Math.Round((double)(_percent * 360)));

                                            if (CursorOptions.LoadingCircleHotNoise)
                                            {
                                                G.DrawArc(noisePen, CircleRect, CursorOptions.Angle, (int)Math.Round((double)(_percent * 360)));
                                            }
                                        }
                                    }
                                }

                                else if (CursorOptions.CircleStyle == CircleStyle.Fluid)
                                {
                                    float PenWidth = 0.3f * Math.Max(_Arrow.Width, _Arrow.Height);
                                    float PenWidth2 = 0.17f * Math.Max(_Arrow.Width, _Arrow.Height);

                                    float _percent = 0.2f;

                                    Rectangle CircleRect = new(_Arrow.X + (int)PenWidth - 2, _Arrow.Y + (int)PenWidth - 2, _Arrow.Width - (int)PenWidth * 2 - 4, _Arrow.Height - (int)PenWidth * 2 - 4);

                                    using (Pen pen = new(BH, PenWidth))
                                    using (Pen pen2 = new(BC, PenWidth))
                                    {
                                        pen.StartCap = LineCap.Round;
                                        pen.EndCap = pen.StartCap;

                                        using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.LoadingCircleBackNoiseOpacity)))
                                        using (Pen noisePen = new(noise, PenWidth))
                                        {
                                            G.DrawArc(pen2, CircleRect, -90, 360);

                                            if (CursorOptions.LoadingCircleBackNoise)
                                            {
                                                G.DrawArc(noisePen, CircleRect, -90, 360);
                                            }
                                        }

                                        using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.LoadingCircleHotNoiseOpacity)))
                                        using (Pen noisePen = new(noise, PenWidth))
                                        {
                                            G.DrawArc(pen, CircleRect, CursorOptions.Angle, (int)Math.Round((double)(_percent * 360)));

                                            if (CursorOptions.LoadingCircleHotNoise)
                                            {
                                                G.DrawArc(noisePen, CircleRect, CursorOptions.Angle, (int)Math.Round((double)(_percent * 360)));
                                            }
                                        }
                                    }
                                }
                            }

                            BC.Dispose();
                            BH.Dispose();

                            break;
                        }

                    case CursorType.AppLoading:
                        {
                            Brush BB, BL;
                            if (CursorOptions.PrimaryColorGradient)
                            {
                                BB = ReturnGradience(_Arrow, CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode, CursorOptions.Angle);
                            }
                            else
                            {
                                BB = new SolidBrush(CursorOptions.PrimaryColor1);
                            }
                            if (CursorOptions.SecondaryColorGradient)
                            {
                                BL = ReturnGradience(_Arrow, CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode, CursorOptions.Angle);
                            }
                            else
                            {
                                BL = new SolidBrush(CursorOptions.SecondaryColor1);
                            }

                            Brush BC, BH;
                            if (CursorOptions.LoadingCircleBackGradient)
                            {
                                BC = ReturnGradience(_LoadRect, CursorOptions.LoadingCircleBack1, CursorOptions.LoadingCircleBack2, CursorOptions.LoadingCircleBackGradientMode, CursorOptions.Angle);
                            }
                            else
                            {
                                BC = new SolidBrush(CursorOptions.LoadingCircleBack1);
                            }
                            if (CursorOptions.LoadingCircleHotGradient)
                            {
                                BH = ReturnGradience(_LoadRect, CursorOptions.LoadingCircleHot1, CursorOptions.LoadingCircleHot2, CursorOptions.LoadingCircleHotGradientMode, CursorOptions.Angle);
                            }
                            else
                            {
                                BH = new SolidBrush(CursorOptions.LoadingCircleHot1);
                            }

                            using (GraphicsPath cursorPath = DefaultCursor(_CurRect, CursorOptions.ArrowStyle, CursorOptions.Scale))
                            {
                                G.FillPath(BB, cursorPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity)))
                                    {
                                        G.FillPath(noise, cursorPath);
                                    }
                                }

                                using (Pen PL = new(BL, CursorOptions.LineThickness)) { G.DrawPath(PL, cursorPath); }

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity)))
                                    using (Pen noisePen = new(noise, CursorOptions.LineThickness))
                                    {
                                        G.DrawPath(noisePen, cursorPath);
                                    }
                                }

                                if (CursorOptions.CircleStyle == CircleStyle.Classic)
                                {
                                    using (GraphicsPath appLoadingPath = AppLoading(_Busy, CursorOptions.Angle, CursorOptions.CircleStyle, CursorOptions.Scale))
                                    using (GraphicsPath appLoadingCirclePath = AppLoaderCircle(_Busy, CursorOptions.Angle, CursorOptions.CircleStyle, CursorOptions.Scale))
                                    {
                                        using (Pen PLx = new(BH, CursorOptions.LineThickness))
                                        {
                                            G.FillPath(BC, appLoadingPath);
                                            G.DrawPath(PLx, appLoadingCirclePath);
                                            G.DrawPath(PLx, appLoadingPath);
                                        }

                                        if (CursorOptions.LoadingCircleBackNoise)
                                        {
                                            using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.LoadingCircleBackNoiseOpacity)))
                                            {
                                                G.FillPath(noise, appLoadingPath);
                                            }
                                        }

                                        if (CursorOptions.LoadingCircleHotNoise)
                                        {
                                            using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.LoadingCircleHotNoiseOpacity)))
                                            using (Pen noisePen = new(noise, CursorOptions.LineThickness))
                                            {
                                                G.DrawPath(noisePen, appLoadingCirclePath);
                                                G.DrawPath(noisePen, appLoadingPath);
                                            }
                                        }
                                    }
                                }

                                else if (CursorOptions.CircleStyle == CircleStyle.Aero)
                                {
                                    using (GraphicsPath appLoadingPath = AppLoading(_LoadRect, CursorOptions.Angle, CursorOptions.CircleStyle, CursorOptions.Scale))
                                    using (GraphicsPath appLoadingCirclePath = AppLoaderCircle(_LoadRect, CursorOptions.Angle, CursorOptions.CircleStyle, CursorOptions.Scale))
                                    {
                                        G.FillPath(BC, appLoadingPath);

                                        if (CursorOptions.LoadingCircleBackNoise)
                                        {
                                            using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.LoadingCircleBackNoiseOpacity)))
                                            {
                                                G.FillPath(noise, appLoadingPath);
                                            }
                                        }

                                        G.FillPath(BH, appLoadingCirclePath);

                                        if (CursorOptions.LoadingCircleHotNoise)
                                        {
                                            using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.LoadingCircleHotNoiseOpacity)))
                                            {
                                                G.FillPath(noise, appLoadingCirclePath);
                                            }
                                        }
                                    }
                                }

                                else if (CursorOptions.CircleStyle == CircleStyle.Modern)
                                {
                                    float PenWidth = 0.2f * Math.Max(_LoadRect.Width, _LoadRect.Height);
                                    float _percent = 0.3f;

                                    Rectangle CircleRect = new(_LoadRect.X + (int)PenWidth, _LoadRect.Y + (int)PenWidth, _LoadRect.Width - (int)PenWidth * 2 - 2, _LoadRect.Height - (int)PenWidth * 2 - 2);

                                    using (Pen pen = new(BH, PenWidth))
                                    using (Pen pen2 = new(BC, PenWidth))
                                    {
                                        pen.StartCap = LineCap.Round;
                                        pen.EndCap = pen.StartCap;

                                        using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.LoadingCircleBackNoiseOpacity)))
                                        using (Pen noisePen = new(noise, PenWidth))
                                        {
                                            G.DrawArc(pen2, CircleRect, -90, 360);

                                            if (CursorOptions.LoadingCircleBackNoise)
                                            {
                                                G.DrawArc(noisePen, CircleRect, -90, 360);
                                            }
                                        }

                                        using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.LoadingCircleHotNoiseOpacity)))
                                        using (Pen noisePen = new(noise, PenWidth))
                                        {
                                            G.DrawArc(pen, CircleRect, CursorOptions.Angle, (int)Math.Round((double)(_percent * 360)));

                                            if (CursorOptions.LoadingCircleHotNoise)
                                            {
                                                G.DrawArc(noisePen, CircleRect, CursorOptions.Angle, (int)Math.Round((double)(_percent * 360)));
                                            }
                                        }
                                    }
                                }

                                else if (CursorOptions.CircleStyle == CircleStyle.Fluid)
                                {
                                    Rectangle _rect = new(_Arrow.Right - _LoadRect.Width, _LoadRect.Y, _LoadRect.Width, _LoadRect.Height);

                                    float PenWidth = 0.32f * Math.Max(_rect.Width, _rect.Height);
                                    float _percent = 0.2f;

                                    Rectangle CircleRect = new(_LoadRect.X + (int)PenWidth, _LoadRect.Y + (int)PenWidth - 2, _LoadRect.Width - (int)PenWidth * 2 - 1, _LoadRect.Height - (int)PenWidth * 2 - 1);

                                    PenWidth = 0.35f * Math.Max(_rect.Width, _rect.Height);

                                    using (Pen pen = new(BH, PenWidth))
                                    using (Pen pen2 = new(BC, PenWidth))
                                    {
                                        pen.StartCap = LineCap.Round;
                                        pen.EndCap = pen.StartCap;

                                        using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.LoadingCircleBackNoiseOpacity)))
                                        using (Pen noisePen = new(noise, PenWidth))
                                        {
                                            G.DrawArc(pen2, CircleRect, -90, 360);

                                            if (CursorOptions.LoadingCircleBackNoise)
                                            {
                                                G.DrawArc(noisePen, CircleRect, -90, 360);
                                            }
                                        }

                                        using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.LoadingCircleHotNoiseOpacity)))
                                        using (Pen noisePen = new(noise, PenWidth))
                                        {
                                            G.DrawArc(pen, CircleRect, CursorOptions.Angle, (int)Math.Round((double)(_percent * 360)));

                                            if (CursorOptions.LoadingCircleHotNoise)
                                            {
                                                G.DrawArc(noisePen, CircleRect, CursorOptions.Angle, (int)Math.Round((double)(_percent * 360)));
                                            }
                                        }
                                    }
                                }
                            }

                            BB.Dispose();
                            BL.Dispose();
                            BC.Dispose();
                            BH.Dispose();

                            break;
                        }

                    case CursorType.None:
                        {
                            Brush BB, BL;
                            if (CursorOptions.PrimaryColorGradient)
                            {
                                BB = ReturnGradience(_Arrow, CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                            }
                            else
                            {
                                BB = new SolidBrush(CursorOptions.PrimaryColor1);
                            }
                            if (CursorOptions.SecondaryColorGradient)
                            {
                                BL = ReturnGradience(_Arrow, CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                            }
                            else
                            {
                                BL = new SolidBrush(CursorOptions.SecondaryColor1);
                            }

                            using (GraphicsPath nonePath = None(_Arrow, CursorOptions.Scale))
                            using (GraphicsPath noneBackgroundPath = NoneBackground(_Arrow, CursorOptions.Scale))
                            {
                                G.FillPath(BB, noneBackgroundPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity)))
                                    {
                                        G.FillPath(noise, noneBackgroundPath);
                                    }
                                }

                                G.FillPath(BL, nonePath);

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity)))
                                    {
                                        G.FillPath(noise, nonePath);
                                    }
                                }
                            }

                            BB.Dispose();
                            BL.Dispose();


                            break;
                        }

                    case CursorType.Move:
                        {
                            Brush BB, BL;
                            if (CursorOptions.PrimaryColorGradient)
                            {
                                BB = ReturnGradience(_Arrow, CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                            }
                            else
                            {
                                BB = new SolidBrush(CursorOptions.PrimaryColor1);
                            }
                            if (CursorOptions.SecondaryColorGradient)
                            {
                                BL = ReturnGradience(_Arrow, CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                            }
                            else
                            {
                                BL = new SolidBrush(CursorOptions.SecondaryColor1);
                            }

                            using (GraphicsPath movePath = Move(_Arrow, CursorOptions.Scale))
                            {
                                G.FillPath(BB, movePath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity)))
                                    {
                                        G.FillPath(noise, movePath);
                                    }
                                }

                                using (Pen PL = new(BL, CursorOptions.LineThickness)) { G.DrawPath(PL, movePath); }

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity)))
                                    using (Pen noisePen = new(noise, CursorOptions.LineThickness))
                                    {
                                        G.DrawPath(noisePen, movePath);
                                    }
                                }
                            }

                            BB.Dispose();
                            BL.Dispose();

                            break;
                        }

                    case CursorType.Up:
                        {
                            Brush BB, BL;
                            if (CursorOptions.PrimaryColorGradient)
                            {
                                BB = ReturnGradience(_Arrow, CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                            }
                            else
                            {
                                BB = new SolidBrush(CursorOptions.PrimaryColor1);
                            }
                            if (CursorOptions.SecondaryColorGradient)
                            {
                                BL = ReturnGradience(_Arrow, CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                            }
                            else
                            {
                                BL = new SolidBrush(CursorOptions.SecondaryColor1);
                            }

                            using (GraphicsPath upPath = Up(_Arrow, CursorOptions.Scale))
                            {
                                G.FillPath(BB, upPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity)))
                                    {
                                        G.FillPath(noise, upPath);
                                    }
                                }

                                using (Pen PL = new(BL, CursorOptions.LineThickness)) { G.DrawPath(PL, upPath); }

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity)))
                                    using (Pen noisePen = new(noise, CursorOptions.LineThickness))
                                    {
                                        G.DrawPath(noisePen, upPath);
                                    }
                                }
                            }

                            BB.Dispose();
                            BL.Dispose();

                            break;
                        }

                    case CursorType.NS:
                        {
                            Brush BB, BL;
                            if (CursorOptions.PrimaryColorGradient)
                            {
                                BB = ReturnGradience(_Arrow, CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                            }
                            else
                            {
                                BB = new SolidBrush(CursorOptions.PrimaryColor1);
                            }
                            if (CursorOptions.SecondaryColorGradient)
                            {
                                BL = ReturnGradience(_Arrow, CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                            }
                            else
                            {
                                BL = new SolidBrush(CursorOptions.SecondaryColor1);
                            }

                            using (GraphicsPath nsPath = NS(_Arrow, CursorOptions.Scale))
                            {
                                G.FillPath(BB, nsPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity)))
                                    {
                                        G.FillPath(noise, nsPath);
                                    }
                                }

                                using (Pen PL = new(BL, CursorOptions.LineThickness)) { G.DrawPath(PL, nsPath); }

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity)))
                                    using (Pen noisePen = new(noise, CursorOptions.LineThickness))
                                    {
                                        G.DrawPath(noisePen, nsPath);
                                    }
                                }
                            }

                            BB.Dispose();
                            BL.Dispose();

                            break;
                        }

                    case CursorType.EW:
                        {
                            Brush BB, BL;
                            if (CursorOptions.PrimaryColorGradient)
                            {
                                BB = ReturnGradience(_Arrow, CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                            }
                            else
                            {
                                BB = new SolidBrush(CursorOptions.PrimaryColor1);
                            }
                            if (CursorOptions.SecondaryColorGradient)
                            {
                                BL = ReturnGradience(_Arrow, CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                            }
                            else
                            {
                                BL = new SolidBrush(CursorOptions.SecondaryColor1);
                            }

                            using (GraphicsPath ewPath = EW(_Arrow, CursorOptions.Scale))
                            {
                                G.FillPath(BB, ewPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity)))
                                    {
                                        G.FillPath(noise, ewPath);
                                    }
                                }

                                using (Pen PL = new(BL, CursorOptions.LineThickness)) { G.DrawPath(PL, ewPath); }

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity)))
                                    using (Pen noisePen = new(noise, CursorOptions.LineThickness))
                                    {
                                        G.DrawPath(noisePen, ewPath);
                                    }
                                }
                            }

                            BB.Dispose();
                            BL.Dispose();

                            break;
                        }

                    case CursorType.NESW:
                        {
                            Brush BB, BL;
                            if (CursorOptions.PrimaryColorGradient)
                            {
                                BB = ReturnGradience(_Arrow, CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                            }
                            else
                            {
                                BB = new SolidBrush(CursorOptions.PrimaryColor1);
                            }
                            if (CursorOptions.SecondaryColorGradient)
                            {
                                BL = ReturnGradience(_Arrow, CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                            }
                            else
                            {
                                BL = new SolidBrush(CursorOptions.SecondaryColor1);
                            }

                            using (GraphicsPath neswPath = NESW(_Arrow, CursorOptions.Scale))
                            {
                                G.FillPath(BB, neswPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity)))
                                    {
                                        G.FillPath(noise, neswPath);
                                    }
                                }

                                using (Pen PL = new(BL, CursorOptions.LineThickness)) { G.DrawPath(PL, neswPath); }

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity)))
                                    using (Pen noisePen = new(noise, CursorOptions.LineThickness))
                                    {
                                        G.DrawPath(noisePen, neswPath);
                                    }
                                }
                            }

                            BB.Dispose();
                            BL.Dispose();

                            break;
                        }

                    case CursorType.NWSE:
                        {
                            Brush BB, BL;
                            if (CursorOptions.PrimaryColorGradient)
                            {
                                BB = ReturnGradience(_Arrow, CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                            }
                            else
                            {
                                BB = new SolidBrush(CursorOptions.PrimaryColor1);
                            }
                            if (CursorOptions.SecondaryColorGradient)
                            {
                                BL = ReturnGradience(_Arrow, CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                            }
                            else
                            {
                                BL = new SolidBrush(CursorOptions.SecondaryColor1);
                            }

                            using (GraphicsPath nwsePath = NWSE(_Arrow, CursorOptions.Scale))
                            {
                                G.FillPath(BB, nwsePath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity)))
                                    {
                                        G.FillPath(noise, nwsePath);
                                    }
                                }

                                using (Pen PL = new(BL, CursorOptions.LineThickness)) { G.DrawPath(PL, nwsePath); }

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity)))
                                    using (Pen noisePen = new(noise, CursorOptions.LineThickness))
                                    {
                                        G.DrawPath(noisePen, nwsePath);
                                    }
                                }
                            }

                            BB.Dispose();
                            BL.Dispose();

                            break;
                        }

                    case CursorType.Pen:
                        {
                            Brush BB, BL;
                            if (CursorOptions.PrimaryColorGradient)
                            {
                                BB = ReturnGradience(_Arrow, CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                            }
                            else
                            {
                                BB = new SolidBrush(CursorOptions.PrimaryColor1);
                            }
                            if (CursorOptions.SecondaryColorGradient)
                            {
                                BL = ReturnGradience(_Arrow, CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                            }
                            else
                            {
                                BL = new SolidBrush(CursorOptions.SecondaryColor1);
                            }

                            using (GraphicsPath penPath = Pen(_Arrow, CursorOptions.Scale))
                            using (GraphicsPath penBackgroundPath = PenBackground(_Arrow, CursorOptions.Scale))
                            {
                                G.FillPath(BB, penBackgroundPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity)))
                                    {
                                        G.FillPath(noise, penBackgroundPath);
                                    }
                                }

                                using (Pen PL = new(BL, CursorOptions.LineThickness)) { G.DrawPath(PL, penPath); }

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity)))
                                    using (Pen noisePen = new(noise, CursorOptions.LineThickness))
                                    {
                                        G.DrawPath(noisePen, penPath);
                                    }
                                }
                            }

                            BB.Dispose();
                            BL.Dispose();

                            break;
                        }

                    case CursorType.Link:
                        {
                            Brush BB, BL;
                            if (CursorOptions.PrimaryColorGradient)
                            {
                                BB = ReturnGradience(_Arrow, CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                            }
                            else
                            {
                                BB = new SolidBrush(CursorOptions.PrimaryColor1);
                            }
                            if (CursorOptions.SecondaryColorGradient)
                            {
                                BL = ReturnGradience(_Arrow, CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                            }
                            else
                            {
                                BL = new SolidBrush(CursorOptions.SecondaryColor1);
                            }

                            using (GraphicsPath handPath = Hand(_Arrow, CursorOptions.ArrowStyle, CursorOptions.Scale))
                            {
                                G.FillPath(BB, handPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity)))
                                    {
                                        G.FillPath(noise, handPath);
                                    }
                                }

                                using (Pen PL = new(BL, CursorOptions.LineThickness)) { G.DrawPath(PL, handPath); }

                                using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity)))
                                using (Pen noisePen = new(noise, CursorOptions.LineThickness))
                                {
                                    G.DrawPath(noisePen, handPath);
                                }
                            }

                            BB.Dispose();
                            BL.Dispose();

                            break;
                        }

                    case CursorType.Pin:
                        {
                            Brush BB, BL;
                            if (CursorOptions.PrimaryColorGradient)
                            {
                                BB = ReturnGradience(_Arrow, CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                            }
                            else
                            {
                                BB = new SolidBrush(CursorOptions.PrimaryColor1);
                            }
                            if (CursorOptions.SecondaryColorGradient)
                            {
                                BL = ReturnGradience(_Arrow, CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                            }
                            else
                            {
                                BL = new SolidBrush(CursorOptions.SecondaryColor1);
                            }

                            Brush BB_P, BL_P;
                            if (CursorOptions.PrimaryColorGradient)
                            {
                                BB_P = ReturnGradience(_Pin, CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                            }
                            else
                            {
                                BB_P = new SolidBrush(CursorOptions.PrimaryColor1);
                            }
                            if (CursorOptions.SecondaryColorGradient)
                            {
                                BL_P = ReturnGradience(_Pin, CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                            }
                            else
                            {
                                BL_P = new SolidBrush(CursorOptions.SecondaryColor1);
                            }

                            using (Pen PL = new(BL, CursorOptions.LineThickness))
                            using (GraphicsPath handPath = Hand(_Arrow, CursorOptions.ArrowStyle, CursorOptions.Scale))
                            using (GraphicsPath pinPath = Pin(_Pin, CursorOptions.Scale))
                            using (GraphicsPath pinCenterPointPath = Pin_CenterPoint(_Pin, CursorOptions.Scale))
                            {
                                G.FillPath(BB, handPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity)))
                                    {
                                        G.FillPath(noise, handPath);
                                    }
                                }

                                G.DrawPath(PL, handPath);

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity)))
                                    using (Pen noisePen = new(noise, CursorOptions.LineThickness))
                                    {
                                        G.DrawPath(noisePen, handPath);
                                    }
                                }

                                G.FillPath(BB_P, pinPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity)))
                                    {
                                        G.FillPath(noise, pinPath);
                                    }
                                }

                                G.FillPath(BL_P, pinCenterPointPath);

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity)))
                                    {
                                        G.FillPath(noise, pinCenterPointPath);
                                    }
                                }

                                using (Pen P = new(BL_P, 2f)) { G.DrawPath(P, pinPath); }

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity)))
                                    using (Pen noisePen = new(noise, CursorOptions.LineThickness))
                                    {
                                        G.DrawPath(noisePen, pinPath);
                                    }
                                }
                            }

                            BB.Dispose();
                            BL.Dispose();
                            BB_P.Dispose();
                            BL_P.Dispose();

                            break;
                        }

                    case CursorType.Person:
                        {
                            Brush BB, BL;
                            if (CursorOptions.PrimaryColorGradient)
                            {
                                BB = ReturnGradience(_Arrow, CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                            }
                            else
                            {
                                BB = new SolidBrush(CursorOptions.PrimaryColor1);
                            }
                            if (CursorOptions.SecondaryColorGradient)
                            {
                                BL = ReturnGradience(_Arrow, CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                            }
                            else
                            {
                                BL = new SolidBrush(CursorOptions.SecondaryColor1);
                            }

                            Brush BB_P, BL_P;
                            if (CursorOptions.PrimaryColorGradient)
                            {
                                BB_P = ReturnGradience(_Person, CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                            }
                            else
                            {
                                BB_P = new SolidBrush(CursorOptions.PrimaryColor1);
                            }
                            if (CursorOptions.SecondaryColorGradient)
                            {
                                BL_P = ReturnGradience(_Person, CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                            }
                            else
                            {
                                BL_P = new SolidBrush(CursorOptions.SecondaryColor1);
                            }

                            using (Pen PL = new(BL, CursorOptions.LineThickness))
                            using (GraphicsPath handPath = Hand(_Arrow, CursorOptions.ArrowStyle, CursorOptions.Scale))
                            using (GraphicsPath personPath = Person(_Person, CursorOptions.Scale))
                            {
                                G.FillPath(BB, handPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity)))
                                    {
                                        G.FillPath(noise, handPath);
                                    }
                                }

                                G.DrawPath(PL, handPath);

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity)))
                                    using (Pen noisePen = new(noise, CursorOptions.LineThickness))
                                    {
                                        G.DrawPath(noisePen, handPath);
                                    }
                                }

                                G.FillPath(BB_P, personPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity)))
                                    {
                                        G.FillPath(noise, personPath);
                                    }
                                }

                                using (Pen P = new(BL_P, 2f)) { G.DrawPath(P, personPath); }

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity)))
                                    using (Pen noisePen = new(noise, CursorOptions.LineThickness))
                                    {
                                        G.DrawPath(noisePen, personPath);
                                    }
                                }
                            }

                            BB.Dispose();
                            BL.Dispose();
                            BB_P.Dispose();
                            BL_P.Dispose();


                            break;
                        }

                    case CursorType.IBeam:
                        {
                            Brush BB, BL;
                            if (CursorOptions.PrimaryColorGradient)
                            {
                                BB = ReturnGradience(_Arrow, CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                            }
                            else
                            {
                                BB = new SolidBrush(CursorOptions.PrimaryColor1);
                            }
                            if (CursorOptions.SecondaryColorGradient)
                            {
                                BL = ReturnGradience(_Arrow, CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                            }
                            else
                            {
                                BL = new SolidBrush(CursorOptions.SecondaryColor1);
                            }

                            using (Pen PL = new(BL, CursorOptions.LineThickness))
                            using (GraphicsPath iBeamPath = IBeam(_Arrow, CursorOptions.Scale))
                            {
                                G.FillPath(BB, iBeamPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity)))
                                    {
                                        G.FillPath(noise, iBeamPath);
                                    }
                                }

                                G.DrawPath(PL, iBeamPath);

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity)))
                                    using (Pen noisePen = new(noise, CursorOptions.LineThickness))
                                    {
                                        G.DrawPath(noisePen, iBeamPath);
                                    }
                                }
                            }

                            BB.Dispose();
                            BL.Dispose();

                            break;
                        }

                    case CursorType.Cross:
                        {
                            Brush BB, BL;
                            if (CursorOptions.PrimaryColorGradient)
                            {
                                BB = ReturnGradience(_Arrow, CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode);
                            }
                            else
                            {
                                BB = new SolidBrush(CursorOptions.PrimaryColor1);
                            }
                            if (CursorOptions.SecondaryColorGradient)
                            {
                                BL = ReturnGradience(_Arrow, CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode);
                            }
                            else
                            {
                                BL = new SolidBrush(CursorOptions.SecondaryColor1);
                            }

                            using (Pen PL = new(BL, CursorOptions.LineThickness))
                            using (GraphicsPath crossPath = Cross(_Arrow, CursorOptions.Scale))
                            {
                                G.FillPath(BB, crossPath);

                                if (CursorOptions.PrimaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity)))
                                    {
                                        G.FillPath(noise, crossPath);
                                    }
                                }

                                G.DrawPath(PL, crossPath);

                                if (CursorOptions.SecondaryNoise)
                                {
                                    using (TextureBrush noise = new(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity)))
                                    using (Pen noisePen = new(noise, CursorOptions.LineThickness))
                                    {
                                        G.DrawPath(noisePen, crossPath);
                                    }
                                }
                            }

                            BB.Dispose();
                            BL.Dispose();

                            break;
                        }
                }
            }

            else
            {
                if (System.IO.File.Exists(CursorOptions.File))
                {
                    if (System.IO.Path.GetExtension(CursorOptions.File).ToUpper() == ".CUR")
                    {
                        using (System.IO.FileStream stream = new(CursorOptions.File, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                        {
                            EOIcoCurLoader cur = new(stream);

                            Bitmap ExtractedBMP = null;

                            for (uint i = 0; i <= cur.CountImages() - 1; i++)
                            {
                                if (cur.GetImage(i).Size.Width <= b.Size.Width)
                                {
                                    ExtractedBMP = cur.GetImage(i);
                                    break;
                                }
                            }

                            G.DrawImage(ExtractedBMP, 0, 0);
                        }
                    }
                }
            }

            G.Flush();
            G.Save();

            Bitmap B_Final = new(b.Width, b.Height);
            Graphics G_Final = Graphics.FromImage(B_Final);

            if (CursorOptions.Shadow_Enabled)
            {
                Bitmap shadowedBMP = new(b);

                for (int x = 0, loopTo = b.Width - 1; x <= loopTo; x++)
                {
                    for (int y = 0, loopTo1 = b.Height - 1; y <= loopTo1; y++)
                        shadowedBMP.SetPixel(x, y, Color.FromArgb(b.GetPixel(x, y).A, CursorOptions.Shadow_Color));
                }

                using (ImageProcessor.ImageFactory ImgF = new())
                {
                    ImgF.Load(shadowedBMP);
                    ImgF.GaussianBlur(CursorOptions.Shadow_Blur);
                    ImgF.Alpha((int)(CursorOptions.Shadow_Opacity * 100f));
                    G_Final.DrawImage(ImgF.Image, new Rectangle(0 + CursorOptions.Shadow_OffsetX, 0 + CursorOptions.Shadow_OffsetY, b.Width, b.Height));
                }
            }

            G_Final.DrawImage(b, new Rectangle(0, 0, b.Width, b.Height));

            b.Dispose();
            G.Dispose();

            return new Bitmap(B_Final);
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
            Fluid
        }

        public static GraphicsPath DefaultCursor(Rectangle Rectangle, ArrowStyle Style, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle R = new(Rectangle.X, Rectangle.Y, 12, 18);

                switch (Style)
                {
                    case ArrowStyle.Aero:
                        {
                            // #### Left Border
                            Point LLine1 = new(R.X, R.Y);
                            Point LLine2 = new(R.X, R.Y + R.Height - 2);
                            path.AddLine(LLine1, LLine2);

                            // #### Left Down Border
                            Point DLLine1 = LLine2 + (Size)new Point(1, 0);
                            Point DLLine2 = new(DLLine1.X + 3, DLLine1.Y - 3);
                            path.AddLine(DLLine1, DLLine2);

                            // #### Left Down Handle Border
                            Point DLHLine1 = DLLine2;
                            Point DLHLine2 = new(DLHLine1.X + 3, DLHLine1.Y + 5);
                            path.AddLine(DLHLine1, DLHLine2);

                            // #### Down Handle Border
                            Point DHLine1 = DLHLine2;
                            Point DHLine2 = new(DHLine1.X + 2, DHLine1.Y - 1);

                            // #### Right Down Handle Border
                            Point DRHLine1 = DHLine2;
                            Point DRHLine2 = new(DLHLine1.X + 3, DLHLine1.Y - 1);
                            path.AddLine(DRHLine1, DRHLine2);

                            // #### Right Down Border
                            Point DRLine1 = DRHLine2;
                            Point DRLine2 = new(R.X + R.Width - 1, DLHLine1.Y - 1);
                            path.AddLine(DRLine1, DRLine2);

                            // #### Right Border
                            Point RLine1 = DRLine2 + (Size)new Point(0, -1);
                            Point RLine2 = LLine1;
                            path.AddLine(RLine1, RLine2);
                            break;
                        }

                    case ArrowStyle.Classic:
                        {
                            // #### Left Border
                            Point LLine1 = new(R.X, R.Y);
                            Point LLine2 = new(R.X, R.Y + R.Height - 2);
                            path.AddLine(LLine1, LLine2);

                            // #### Left Down Border
                            Point DLLine1 = LLine2 + (Size)new Point(1, -1);
                            Point DLLine2 = new(DLLine1.X + 3, DLLine1.Y - 3);
                            path.AddLine(DLLine1, DLLine2);

                            // #### Left Down Handle Border
                            Point DLHLine1 = DLLine2;
                            Point DLHLine2 = new(DLHLine1.X + 4, DLHLine1.Y + 8);
                            path.AddLine(DLHLine1, DLHLine2);

                            // #### Down Handle Border
                            Point DHLine1 = DLHLine2;
                            Point DHLine2 = new(DHLine1.X + 2, DHLine1.Y - 1);

                            // #### Right Down Handle Border
                            Point DRHLine1 = DHLine2;
                            Point DRHLine2 = new(DLHLine1.X + 3, DLHLine1.Y);
                            path.AddLine(DRHLine1, DRHLine2);

                            // #### Right Down Border
                            Point DRLine1 = DRHLine2 + (Size)new Point(0, -1);
                            Point DRLine2 = new(R.X + R.Width - 1, DRLine1.Y);
                            path.AddLine(DRLine1, DRLine2);

                            // #### Right Border
                            Point RLine1 = DRLine2 + (Size)new Point(-1, -1);
                            Point RLine2 = LLine1;
                            path.AddLine(RLine1, RLine2);
                            break;
                        }

                    case ArrowStyle.Modern:
                        {
                            // #### Left Border
                            Point LLine1 = new(R.X, R.Y);
                            Point LLine2 = new(R.X, R.Y + R.Height - 2);
                            path.AddLine(LLine1, LLine2);

                            // #### Left Down Border
                            Point DLLine1 = LLine2 + (Size)new Point(1, 0);
                            Point DLLine2 = new(DLLine1.X + 4, DLLine1.Y - 4);
                            path.AddLine(DLLine1, DLLine2);

                            // #### Right Down Border
                            Point DRLine1 = DLLine2;
                            Point DRLine2 = new(R.X + R.Width - 1, DRLine1.Y);
                            path.AddLine(DRLine1, DRLine2);

                            // #### Right Border
                            Point RLine1 = DRLine2 + (Size)new Point(0, -1);
                            Point RLine2 = LLine1;
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

        public static GraphicsPath Busy(Rectangle Rectangle, float Angle, CircleStyle Style, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            using (Matrix m = new())
            {
                Rectangle.Width = 22;
                Rectangle.Height = 22;

                switch (Style)
                {
                    case CircleStyle.Aero:
                        {
                            path.AddEllipse(Rectangle.X, Rectangle.Y, 22, 22);
                            Rectangle R = new(Rectangle.X + 5, Rectangle.Y + 5, 12, 12);
                            path.AddEllipse(R);
                            path.CloseFigure();
                            break;
                        }

                    case CircleStyle.Classic:
                        {
                            path.AddLine(new Point(Rectangle.X + 12, Rectangle.Y + 0), new Point(Rectangle.X + 6, Rectangle.Y + 0));
                            path.AddLine(new Point(Rectangle.X + 6, Rectangle.Y + 0), new Point(Rectangle.X + 6, Rectangle.Y + 2));
                            path.AddLine(new Point(Rectangle.X + 6, Rectangle.Y + 2), new Point(Rectangle.X + 12, Rectangle.Y + 2));

                            path.AddLine(new Point(Rectangle.X + 7, Rectangle.Y + 2), new Point(Rectangle.X + 7, Rectangle.Y + 7));
                            path.AddLine(new Point(Rectangle.X + 7, Rectangle.Y + 7), new Point(Rectangle.X + 11, Rectangle.Y + 10));
                            path.AddLine(new Point(Rectangle.X + 11, Rectangle.Y + 11), new Point(Rectangle.X + 7, Rectangle.Y + 14));
                            path.AddLine(new Point(Rectangle.X + 7, Rectangle.Y + 14), new Point(Rectangle.X + 7, Rectangle.Y + 19));

                            path.AddLine(new Point(Rectangle.X + 12, Rectangle.Y + 19), new Point(Rectangle.X + 6, Rectangle.Y + 19));
                            path.AddLine(new Point(Rectangle.X + 6, Rectangle.Y + 19), new Point(Rectangle.X + 6, Rectangle.Y + 21));
                            path.AddLine(new Point(Rectangle.X + 6, Rectangle.Y + 21), new Point(Rectangle.X + 12, Rectangle.Y + 21));

                            path.AddPath(MirrorRight(path), false);

                            if (Angle >= 270f)
                            {
                                using (Matrix m_rotate = new())
                                {
                                    m_rotate.RotateAt((Angle - 180f) * 3f, new Point((int)Math.Round(Rectangle.X + Rectangle.Width / 2d), (int)Math.Round(Rectangle.Y + Rectangle.Height / 2d)));
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

        public static GraphicsPath BusyLoader(Rectangle Rectangle, float Angle, CircleStyle Style, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            using (Matrix m = new())
            {
                Rectangle.X += 0;
                Rectangle.Y += 0;

                Rectangle.Width = 22;
                Rectangle.Height = 22;

                Point CPoint = new((int)Math.Round(Rectangle.X + Rectangle.Width / 2d), (int)Math.Round(Rectangle.Y + Rectangle.Height / 2d));
                Rectangle R = new(Rectangle.X + 5, Rectangle.Y + 5, 12, 12);

                int innerR = 15;
                int thickness = 10;
                int arcLength = 70;
                int outerR = innerR + thickness;

                switch (Style)
                {
                    case CircleStyle.Aero:
                        {
                            var outerRect = Rectangle;
                            var innerRect = R;
                            path.AddArc(outerRect, Angle, arcLength);
                            path.AddArc(innerRect, Angle + arcLength, -arcLength);
                            path.CloseFigure();
                            break;
                        }

                    case CircleStyle.Classic:
                        {
                            Point PU1 = new(Rectangle.X + 12, Rectangle.Y + 5);
                            Point PU3 = new(Rectangle.X + 10, Rectangle.Y + 5);
                            Point PU4 = PU3 + (Size)new Point(2, 2);

                            path.AddLine(PU1, PU1);
                            path.CloseFigure();
                            path.AddLine(PU3, PU4);
                            path.CloseFigure();

                            Point PL1 = new(Rectangle.X + 12, Rectangle.Y + 17);
                            Point PL2 = PL1 - (Size)new Point(1, -1);

                            Point PL3 = PL1 - (Size)new Point(0, 2);
                            Point PL4 = PL3 - (Size)new Point(3, -3);

                            path.AddLine(PL1, PL2);
                            path.CloseFigure();

                            path.AddLine(PL3, PL4);
                            path.CloseFigure();

                            path.AddPath(MirrorRight(path), false);

                            Point C1 = PU1 + (Size)new Point(0, 4);
                            path.AddLine(C1, C1);
                            path.CloseFigure();

                            Point C2 = C1 + (Size)new Point(0, 4);
                            path.AddLine(C2, C2);
                            path.CloseFigure();

                            if (Angle >= 270f)
                            {
                                using (Matrix m_rotate = new())
                                {
                                    m_rotate.RotateAt((Angle - 180f) * 3f, new Point((int)Math.Round(Rectangle.X + Rectangle.Width / 2d), (int)Math.Round(Rectangle.Y + Rectangle.Height / 2d)));
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

        public static GraphicsPath AppLoading(Rectangle Rectangle, float Angle, CircleStyle Style, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            using (Matrix m = new())
            {
                Rectangle.Width = 16;
                Rectangle.Height = 16;

                switch (Style)
                {
                    case CircleStyle.Aero:
                        {
                            path.AddEllipse(Rectangle);
                            Rectangle R = new(Rectangle.X + 4, Rectangle.Y + 4, 8, 8);
                            path.AddEllipse(R);
                            path.CloseFigure();
                            break;
                        }

                    case CircleStyle.Classic:
                        {
                            Rectangle UpperRectangle = new(Rectangle.X + 12, Rectangle.Y + 9, 9, 2);
                            Rectangle LowerRectangle = new(Rectangle.X + 12, Rectangle.Y + 23, 9, 2);
                            Rectangle Container = new(UpperRectangle.X, UpperRectangle.Y, UpperRectangle.Width, LowerRectangle.Bottom - UpperRectangle.Top);

                            Point pL1 = new(UpperRectangle.X + 1, UpperRectangle.Y + UpperRectangle.Height);
                            Point pL2 = new(pL1.X, pL1.Y + 4);
                            path.AddLine(pL1, pL2);

                            Point pL3 = pL2 + (Size)new Point(1, 0);
                            Point pL4 = pL3 + (Size)new Point(1, 1);
                            path.AddLine(pL3, pL4);

                            Point pL5 = pL4 + (Size)new Point(0, 1);
                            path.AddLine(pL5, pL5);

                            Point pL6 = pL5 + (Size)new Point(0, 1);
                            Point pl7 = pL6 + (Size)new Point(-1, 1);
                            path.AddLine(pL6, pl7);

                            Point pL8 = pl7 - (Size)new Point(1, 0);
                            Point pL9 = pL8 + (Size)new Point(0, 4);
                            path.AddLine(pL8, pL9);

                            Point pL10 = pL9 + (Size)new Point(7, 0);
                            Point pL11 = pL10 - (Size)new Point(0, 4);
                            path.AddLine(pL10, pL11);

                            Point pL12 = pL11 + (Size)new Point(-1, 0);
                            Point pL13 = pL12 + (Size)new Point(-1, -1);
                            path.AddLine(pL12, pL13);

                            Point pL14 = pL13 + (Size)new Point(0, -1);
                            path.AddLine(pL14, pL14);

                            Point pL15 = pL14 + (Size)new Point(0, -1);
                            Point pl16 = pL15 + (Size)new Point(1, -1);
                            path.AddLine(pL15, pl16);

                            Point pL17 = pl16 + (Size)new Point(1, 0);
                            Point pL18 = pL17 + (Size)new Point(0, -4);
                            path.AddLine(pL17, pL18);

                            path.AddRectangle(UpperRectangle);

                            path.AddRectangle(LowerRectangle);

                            path.CloseFigure();

                            Point FixerL0 = pL3 + (Size)new Point(0, 1);
                            Point FixerL1 = pL3 + (Size)new Point(0, 3);

                            Point FixerR0 = pL12 - (Size)new Point(0, 1);
                            Point FixerR1 = pL12 - (Size)new Point(0, 3);

                            path.AddLine(FixerL0, FixerL0);
                            path.CloseFigure();

                            path.AddLine(FixerL1, FixerL1);
                            path.CloseFigure();

                            path.AddLine(FixerR0, FixerR0);
                            path.CloseFigure();

                            path.AddLine(FixerR1, FixerR1);
                            path.CloseFigure();

                            if (Angle >= 270f)
                            {
                                using (Matrix m_rotate = new())
                                {
                                    m_rotate.RotateAt((Angle - 180f) * 3f, new Point((int)Math.Round(Container.X + Container.Width / 2d), (int)Math.Round(Container.Y + Container.Height / 2d)));
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

        public static GraphicsPath AppLoaderCircle(Rectangle Rectangle, float Angle, CircleStyle Style, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            using (Matrix m = new())
            {
                Rectangle.Width = 16;
                Rectangle.Height = 16;

                Point CPoint = new((int)Math.Round(Rectangle.X + Rectangle.Width / 2d), (int)Math.Round(Rectangle.Y + Rectangle.Height / 2d));
                Rectangle R = new(Rectangle.X + 4, Rectangle.Y + 4, 8, 8);

                int innerR = 15;
                int thickness = 6;
                int arcLength = 70;
                int outerR = innerR + thickness;

                switch (Style)
                {
                    case CircleStyle.Aero:
                        {
                            var outerRect = Rectangle;
                            var innerRect = R;
                            path.AddArc(outerRect, Angle, arcLength);
                            path.AddArc(innerRect, Angle + arcLength, -arcLength);
                            break;
                        }

                    case CircleStyle.Classic:
                        {
                            Rectangle UpperRectangle = new(Rectangle.X + 12, Rectangle.Y + 9, 9, 2);
                            Rectangle LowerRectangle = new(Rectangle.X + 12, Rectangle.Y + 23, 9, 2);
                            Rectangle Container = new(UpperRectangle.X, UpperRectangle.Y, UpperRectangle.Width, LowerRectangle.Bottom - UpperRectangle.Top);

                            Point PU1 = new(Rectangle.X + 17, Rectangle.Y + 14);
                            Point PU3 = new(Rectangle.X + 14, Rectangle.Y + 15);
                            Point PU4 = PU3 + (Size)new Point(2, 2);

                            path.AddLine(PU1, PU3);
                            path.CloseFigure();
                            path.AddLine(PU3, PU4);
                            path.CloseFigure();

                            Point PL1 = new(Rectangle.X + 16, Rectangle.Y + 20);
                            Point PL2 = PL1 - (Size)new Point(2, -2);
                            path.AddLine(PL1, PL2);
                            path.CloseFigure();

                            Point PL3 = PL1 + (Size)new Point(1, 1);
                            Point PL4 = PL3 - (Size)new Point(1, -1);
                            path.AddLine(PL3, PL4);
                            path.CloseFigure();

                            Point C1 = PL3 + (Size)new Point(1, 1);
                            path.AddLine(C1, C1);
                            path.CloseFigure();

                            if (Angle >= 270f)
                            {
                                using (Matrix m_rotate = new())
                                {
                                    m_rotate.RotateAt((Angle - 180f) * 3f, new Point((int)Math.Round(Container.X + Container.Width / 2d), (int)Math.Round(Container.Y + Container.Height / 2d)));
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

        public static GraphicsPath Move(Rectangle Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.Width = 21;
                Rectangle.Height = 21;
                Rectangle.X = 0;
                Rectangle.Y = 0;

                Point UL1 = new(Rectangle.X + 11, Rectangle.Y);
                Point UL2 = new(UL1.X - 4, Rectangle.Y + 4);
                path.AddLine(UL1, UL2);

                Point ULX1 = new(UL2.X, UL2.Y + 1);
                Point ULX2 = new(ULX1.X + 3, ULX1.Y);
                path.AddLine(ULX1, ULX2);

                Point MUL1 = ULX2;
                Point MUL2 = new(MUL1.X, ULX2.Y + 4);
                path.AddLine(MUL1, MUL2);

                Point MULX1 = new(MUL2.X, MUL2.Y + 1);
                Point MULX2 = new(MULX1.X - 5, MULX1.Y);
                path.AddLine(MULX1, MULX2);

                Point LU1 = MULX2;
                Point LU2 = new(MULX2.X, MULX2.Y - 3);
                path.AddLine(LU1, LU2);

                Point LUX1 = new(LU2.X - 1, LU2.Y);
                Point LUX2 = new(Rectangle.X, Rectangle.Y + 11);
                path.AddLine(LUX1, LUX2);

                Point LDX1 = LUX2;
                Point LDX2 = new(LDX1.X + 4, LDX1.Y + 4);
                path.AddLine(LDX1, LDX2);

                Point LD1 = new(LDX2.X + 1, LDX2.Y);
                Point LD2 = new(LD1.X, LD1.Y - 2);
                path.AddLine(LD1, LD2);

                Point L1 = new(LD2.X, LD2.Y - 1);
                Point L2 = new(L1.X + 5, L1.Y);
                path.AddLine(L1, L2);

                Point DL1 = L2;
                Point DL2 = new(L2.X, LD2.Y + 3);
                path.AddLine(DL1, DL2);

                Point DX1 = new(DL2.X, DL2.Y + 1);
                Point DX2 = new(DX1.X - 3, DX1.Y);
                path.AddLine(DX1, DX2);

                Point DLX1 = new(DX2.X, DX2.Y + 1);
                Point DLX2 = new(DLX1.X + 4, DLX1.Y + 4);
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

        public static GraphicsPath Help(Rectangle Rectangle, ArrowStyle Style, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.Width = 7;
                Rectangle.Height = 11;
                Rectangle.X = 11;
                Rectangle.Y = 6;

                FontFamily F;

                switch (Style)
                {
                    case ArrowStyle.Classic:
                        {
                            F = new("Marlett");
                            using (Font font = new(F, 15f, FontStyle.Bold)) { path.AddString("s", F, (int)FontStyle.Bold, 15f, Rectangle, StringFormat.GenericDefault); }

                            break;
                        }

                    default:
                        {

                            if (OS.WXP) { F = new("Tahoma"); }
                            else if (OS.W7 | OS.WVista) { F = new("Segoe UI"); }
                            else { F = new("Segoe UI Black"); }

                            using (Font font = new(F, 15f, FontStyle.Bold)) { path.AddString("?", F, (int)FontStyle.Bold, 15f, Rectangle, StringFormat.GenericDefault); }

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

        public static GraphicsPath None(Rectangle Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.Width = 17;
                Rectangle.Height = 17;
                Rectangle.X = 0;
                Rectangle.Y = 0;

                Rectangle R = new(Rectangle.X + 2, Rectangle.Y + 2, Rectangle.Width - 4, Rectangle.Height - 4);

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

        public static GraphicsPath NoneBackground(Rectangle Rectangle, float Scale = 1f)
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

        public static GraphicsPath Up(Rectangle Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.Width = 9;
                Rectangle.Height = 19;
                Rectangle.X = 0;
                Rectangle.Y = 0;

                Point UL1 = new(Rectangle.X + 4, Rectangle.Y);
                Point UL2 = new(Rectangle.X, Rectangle.Y + 4);
                path.AddLine(UL1, UL2);

                Point ULX1 = new(UL2.X, UL2.Y + 1);
                Point ULX2 = new(ULX1.X + 3, ULX1.Y);
                path.AddLine(ULX1, ULX2);

                Point MUL1 = ULX2;
                Point MUL2 = new(MUL1.X, MUL1.Y + 12);
                path.AddLine(MUL1, MUL2);

                Point D1 = new(MUL2.X, MUL2.Y + 1);
                Point D2 = new(D1.X + 1, D1.Y);
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

        public static GraphicsPath NS(Rectangle Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.Width = 9;
                Rectangle.Height = 23;
                Rectangle.X = 0;
                Rectangle.Y = 0;

                Point UL1 = new(Rectangle.X + 4, Rectangle.Y);
                Point UL2 = new(Rectangle.X, Rectangle.Y + 4);
                path.AddLine(UL1, UL2);

                Point ULX1 = new(UL2.X, UL2.Y + 1);
                Point ULX2 = new(ULX1.X + 3, ULX1.Y);
                path.AddLine(ULX1, ULX2);

                Point MUL1 = ULX2;
                Point MUL2 = new(MUL1.X, MUL1.Y + 12);
                path.AddLine(MUL1, MUL2);

                Point DL1 = MUL2;
                Point DL2 = new(MUL2.X - 3, MUL2.Y);
                path.AddLine(DL1, DL2);

                Point DX1 = new(DL2.X, DL2.Y + 1);
                Point DX2 = new(DX1.X + 4, DX1.Y + 4);
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

        public static GraphicsPath NESW(Rectangle Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.Width = 17;
                Rectangle.Height = 17;
                Rectangle.X = 0;
                Rectangle.Y = 0;

                Point UR1 = new(Rectangle.X + Rectangle.Width - 1, Rectangle.Y);
                Point UR2 = new(UR1.X - 6, UR1.Y);
                path.AddLine(UR1, UR2);

                Point RX1 = new(UR2.X, UR2.Y + 1);
                Point RX2 = new(RX1.X + 1, RX1.Y + 1);
                path.AddLine(RX1, RX2);

                Point LX1 = new(RX2.X + 1, RX2.Y + 1);
                Point LX2 = new(LX1.X - 9, LX1.Y + 9);
                path.AddLine(LX1, LX2);

                Point DX1 = new(LX2.X - 1, LX2.Y - 1);
                Point DX2 = new(DX1.X - 1, DX1.Y - 1);
                path.AddLine(DX1, DX2);

                Point L1 = new(DX2.X - 1, DX2.Y);
                Point L2 = new(L1.X, L1.Y + 6);
                path.AddLine(L1, L2);

                Point D1 = new(L2.X + 1, L2.Y);
                Point D2 = new(D1.X + 5, D1.Y);
                path.AddLine(D1, D2);

                Point DL1 = new(D2.X, D2.Y - 1);
                Point DL2 = new(DL1.X - 1, DL1.Y - 1);
                path.AddLine(DL1, DL2);

                Point LX3 = new(DL2.X - 1, DL2.Y - 1);
                Point LX4 = new(LX3.X + 9, LX3.Y - 9);
                path.AddLine(LX3, LX4);

                Point DR1 = new(LX4.X + 1, LX4.Y + 1);
                Point DR2 = new(DR1.X + 1, DR1.Y + 1);
                path.AddLine(DR1, DR2);

                Point R1 = new(DR2.X + 1, DR2.Y);
                Point R2 = new(R1.X, R1.Y - 6);
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

        public static GraphicsPath NWSE(Rectangle Rectangle, float Scale = 1f)
        {
            using (var path = NESW(Rectangle))
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

        public static GraphicsPath EW(Rectangle Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.Width = 23;
                Rectangle.Height = 9;
                Rectangle.X = 0;
                Rectangle.Y = 0;

                Point L1 = new(Rectangle.X, Rectangle.Y + 4);
                Point L2 = new(L1.X + 4, L1.Y - 4);
                path.AddLine(L1, L2);

                Point LX1 = new(L2.X + 1, L2.Y);
                Point LX2 = new(LX1.X, LX1.Y + 2);
                path.AddLine(LX1, LX2);

                Point U1 = new(LX2.X, LX2.Y + 1);
                Point U2 = new(U1.X + 12, U1.Y);
                path.AddLine(U1, U2);

                Point RX1 = new(U2.X, U2.Y - 1);
                Point RX2 = new(RX1.X, RX1.Y - 2);
                path.AddLine(RX1, RX2);

                Point R1 = new(RX2.X + 1, RX2.Y);
                Point R2 = new(R1.X + 4, R1.Y + 4);
                path.AddLine(R1, R2);

                Point R3 = new(R2.X, R2.Y);
                Point R4 = new(R3.X - 4, R3.Y + 4);
                path.AddLine(R3, R4);

                Point RX3 = new(R4.X - 1, R4.Y);
                Point RX4 = new(RX3.X, RX3.Y - 2);
                path.AddLine(RX3, RX4);

                Point D1 = new(RX4.X, RX4.Y - 1);
                Point D2 = new(D1.X - 12, D1.Y);
                path.AddLine(D1, D2);

                Point LX3 = new(D2.X, D2.Y + 1);
                Point LX4 = new(LX3.X, LX3.Y + 2);
                path.AddLine(LX3, LX4);

                Point L3 = new(LX4.X - 1, LX4.Y);
                Point L4 = new(L3.X - 4, L3.Y - 4);
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

        public static GraphicsPath Pen(Rectangle Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.Width = 22;
                Rectangle.Height = 22;
                Rectangle.X = 0;
                Rectangle.Y = 0;

                Point T1 = new(Rectangle.X, Rectangle.Y);
                Point T2 = T1 + (Size)new Point(6, 2);
                path.AddLine(T1, T2);

                Point R1 = new(T2.X, T2.Y);
                Point R2 = new(R1.X + 15, R1.Y + 15);
                path.AddLine(R1, R2);

                Point B1 = new(R2.X, R2.Y + 1);
                Point B2 = new(B1.X - 3, B1.Y + 3);
                path.AddLine(B1, B2);

                Point L1 = new(B2.X - 1, B2.Y);
                Point L2 = new(L1.X - 15, L1.Y - 15);
                path.AddLine(L1, L2);

                Point LX1 = new(L2.X, L2.Y);
                path.AddLine(LX1, T1);

                path.CloseFigure();

                Point S1 = new(Rectangle.X + 14, Rectangle.Y + 18);
                Point S2 = new(S1.X + 4, S1.Y - 4);
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

        public static GraphicsPath PenBackground(Rectangle Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.Width = 22;
                Rectangle.Height = 22;
                Rectangle.X = 0;
                Rectangle.Y = 0;

                Point T1 = new(Rectangle.X, Rectangle.Y);
                Point T2 = T1 + (Size)new Point(6, 2);
                path.AddLine(T1, T2);

                Point R1 = new(T2.X, T2.Y);
                Point R2 = new(R1.X + 15, R1.Y + 15);
                path.AddLine(R1, R2);

                Point B1 = new(R2.X, R2.Y + 1);
                Point B2 = new(B1.X - 3, B1.Y + 3);
                path.AddLine(B1, B2);

                Point L1 = new(B2.X - 1, B2.Y);
                Point L2 = new(L1.X - 15, L1.Y - 15);
                path.AddLine(L1, L2);

                Point LX1 = new(L2.X, L2.Y);
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

        public static GraphicsPath Hand(Rectangle Rectangle, ArrowStyle Style, float Scale = 1f)
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
                            Point Index_LB1 = new(Rectangle.X + 5, Rectangle.Y + 14);
                            Point Index_LB2 = new(Index_LB1.X, Index_LB1.Y - 11);
                            path.AddLine(Index_LB1, Index_LB2);

                            Point Index_RB1 = new(Index_LB1.X + 3, Index_LB1.Y - 4);
                            Point Index_RB2 = new(Index_RB1.X, Index_RB1.Y - 8);
                            path.AddArc(Index_LB2.X, Index_LB2.Y - 2, 3, 2, 180f, 180f);

                            path.AddLine(Index_RB1, Index_RB2);
                            path.AddArc(Index_RB2.X, Index_LB2.Y + 3, 3, 2, 180f, 180f);

                            Point Middle_RB1 = new(Index_RB1.X + 3, Index_RB1.Y);
                            Point Middle_RB2 = new(Middle_RB1.X, Middle_RB1.Y - 3);
                            path.AddLine(Middle_RB1, Middle_RB2);
                            path.AddArc(Middle_RB2.X, Index_LB2.Y + 4, 3, 2, 180f, 180f);

                            Point Ring_RB1 = new(Middle_RB1.X + 3, Middle_RB1.Y + 1);
                            Point Ring_RB2 = new(Ring_RB1.X, Ring_RB1.Y - 2);
                            path.AddLine(Ring_RB1, Ring_RB2);
                            path.AddArc(Ring_RB2.X, Index_LB2.Y + 6, 3, 2, 180f, 180f);

                            Point FreeBorder1 = new(Ring_RB1.X + 3, Ring_RB1.Y - 1);
                            Point FreeBorder2 = new(FreeBorder1.X, FreeBorder1.Y + 6);
                            path.AddLine(FreeBorder1, FreeBorder2);

                            Point LW1 = FreeBorder2 + (Size)new Point(0, 1);
                            Point RW1 = new(LW1.X - 14, LW1.Y);
                            Rectangle Btm = new(RW1.X + 3, RW1.Y - 8, 9, 13);
                            path.AddLine(FreeBorder2, new Point(Btm.X + Btm.Width, Btm.Y + Btm.Height));
                            path.AddLine(new Point(Btm.X + Btm.Width, Btm.Y + Btm.Height), new Point(Btm.X, Btm.Y + Btm.Height));

                            Point L1 = RW1 - (Size)new Point(0, 1);
                            Point L2 = new(L1.X - 1, L1.Y - 3);
                            Rectangle Thumb = new(L2.X - 1, L2.Y - 3, 2, 3);
                            path.AddArc(Thumb, 90f, 180f);

                            Point LastBorder1 = new(Thumb.X + Thumb.Width, Thumb.Y);
                            Point LastBorder2 = new(LastBorder1.X + 2, LastBorder1.Y + 1);
                            path.AddLine(LastBorder1, LastBorder2);

                            path.CloseFigure();
                            break;
                        }

                    default:
                        {
                            Point Index_LB1 = new(Rectangle.X + 5, Rectangle.Y + 14);
                            Point Index_LB2 = new(Index_LB1.X, Index_LB1.Y - 12);
                            path.AddLine(Index_LB1, Index_LB2);

                            Point Index_RB1 = new(Index_LB1.X + 3, Index_LB1.Y - 4);
                            Point Index_RB2 = new(Index_RB1.X, Index_RB1.Y - 8);

                            path.AddArc(Index_LB2.X, Index_LB2.Y - 2, 3, 2, 180f, 180f);

                            path.AddLine(Index_RB1, Index_RB2);

                            path.AddArc(Index_RB2.X, Index_LB2.Y + 3, 3, 2, 180f, 180f);

                            Point Middle_RB1 = new(Index_RB1.X + 3, Index_RB1.Y);
                            Point Middle_RB2 = new(Middle_RB1.X, Middle_RB1.Y - 3);
                            path.AddLine(Middle_RB1, Middle_RB2);

                            path.AddArc(Middle_RB2.X, Index_LB2.Y + 4, 3, 2, 180f, 180f);

                            Point Ring_RB1 = new(Middle_RB1.X + 3, Middle_RB1.Y);
                            Point Ring_RB2 = new(Ring_RB1.X, Ring_RB1.Y - 2);
                            path.AddLine(Ring_RB1, Ring_RB2);

                            path.AddArc(Ring_RB2.X, Index_LB2.Y + 5, 3, 2, 180f, 180f);

                            Point FreeBorder1 = new(Ring_RB1.X + 3, Ring_RB1.Y - 1);
                            Point FreeBorder2 = new(FreeBorder1.X, FreeBorder1.Y + 8);
                            path.AddLine(FreeBorder1, FreeBorder2);

                            Point LW1 = FreeBorder2 + (Size)new Point(0, 1);
                            Point RW1 = new(LW1.X - 14, LW1.Y);
                            Rectangle Btm = new(RW1.X, RW1.Y - 8, 14, 13);
                            path.AddArc(Btm, 0f, 180f);

                            Point L1 = RW1 - (Size)new Point(0, 1);
                            Point L2 = new(L1.X - 2, L1.Y - 2);
                            Rectangle Thumb = new(L2.X - 1, L2.Y - 3, 2, 3);
                            path.AddArc(Thumb, 90f, 180f);

                            Point LastBorder1 = new(Thumb.X + Thumb.Width, Thumb.Y);
                            Point LastBorder2 = new(LastBorder1.X + 2, LastBorder1.Y + 1);
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

        public static GraphicsPath Pin(Rectangle Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.Width = 13;
                Rectangle.Height = 20;
                Rectangle.X = 15;
                Rectangle.Y = 11;

                Rectangle U = new(Rectangle.X, Rectangle.Y, 12, 10);
                path.AddArc(U, 180f, 180f);

                Point C = new(Rectangle.X + 6, Rectangle.Y + 18);
                Point p1 = new(Rectangle.X + 0, Rectangle.Y + 6);
                Point p2 = new(Rectangle.X + 12, Rectangle.Y + 6);
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

        public static GraphicsPath Person(Rectangle Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.Width = 10;
                Rectangle.Height = 13;
                Rectangle.X = 19;
                Rectangle.Y = 17;

                Rectangle Face = new(Rectangle.X, Rectangle.Y, 5, 6);
                path.AddEllipse(Face);

                Rectangle TrunkUpper = new(Face.X - 2, Face.Y + Face.Height, 9, 9);
                path.AddArc(TrunkUpper, 180f, 180f);

                Rectangle TrunkLower = new(TrunkUpper.X, TrunkUpper.Y + 3, 9, 3);
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

        public static GraphicsPath IBeam(Rectangle Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.X = 0;
                Rectangle.Y = 0;

                Point L1 = new(Rectangle.X, Rectangle.Y);
                Point L2 = new(L1.X, L1.Y + 2);
                path.AddLine(L1, L2);

                Point BU1 = new(L2.X + 3, L2.Y);
                path.AddLine(L2, BU1);

                Point LX = new(BU1.X, BU1.Y + 13);
                path.AddLine(BU1, LX);

                Point BU2 = new(LX.X - 3, LX.Y);
                path.AddLine(LX, BU2);

                Point L3 = new(BU2.X, BU2.Y + 2);
                path.AddLine(BU2, L3);

                Point BL = new(L3.X + 3, L3.Y);
                path.AddLine(L3, BL);

                Point XB = new(BL.X + 1, BL.Y - 1);
                path.AddLine(BL, XB);

                Point Br = new(XB.X + 1, XB.Y + 1);
                path.AddLine(XB, Br);

                Point RB = new(Br.X + 3, Br.Y);
                path.AddLine(Br, RB);

                Point R1 = new(RB.X, RB.Y - 2);
                path.AddLine(RB, R1);

                Point BU3 = new(R1.X - 3, R1.Y);
                path.AddLine(R1, BU3);

                Point RX = new(BU3.X, BU3.Y - 13);
                path.AddLine(BU3, RX);

                Point TU = new(RX.X + 3, RX.Y);
                path.AddLine(RX, TU);

                Point RR = new(TU.X, TU.Y - 2);
                path.AddLine(TU, RR);

                Point T = new(RR.X - 3, RR.Y);
                path.AddLine(RR, T);

                Point Tx = new(T.X - 1, T.Y + 1);
                path.AddLine(T, Tx);

                Point TXL = new(Tx.X - 1, Tx.Y - 1);
                path.AddLine(Tx, TXL);

                Point TL = new(TXL.X - 3, TXL.Y);
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

        public static GraphicsPath Cross(Rectangle Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.Width = 19;
                Rectangle.Height = 19;

                Point L1 = new(9, 0);
                Point L2 = new(L1.X - 1, L1.Y);
                path.AddLine(L1, L2);

                Point L3 = new(L2.X, L2.Y + 8);
                path.AddLine(L2, L3);

                Point L4 = new(L3.X - 8, L3.Y);
                path.AddLine(L3, L4);

                Point L5 = new(L4.X, L4.Y + 2);
                path.AddLine(L4, L5);

                Point L6 = new(L5.X + 8, L5.Y);
                path.AddLine(L5, L6);

                Point L7 = new(L6.X, L6.Y + 8);
                path.AddLine(L6, L7);

                Point L8 = new(L7.X + 1, L7.Y);
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

        public static GraphicsPath Pin_CenterPoint(Rectangle Rectangle, float Scale = 1f)
        {
            using (GraphicsPath path = new())
            {
                Rectangle.Width = 13;
                Rectangle.Height = 20;
                Rectangle.X = 15;
                Rectangle.Y = 11;

                Rectangle o = new(Rectangle.X, Rectangle.Y, 12, 12);
                Rectangle o1 = new(Rectangle.X, Rectangle.Y, 6, 6);
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
                Shadow_Enabled = Cursor.Shadow_Enabled;
                Shadow_Color = Cursor.Shadow_Color;
                Shadow_Blur = Cursor.Shadow_Blur;
                Shadow_Opacity = Cursor.Shadow_Opacity;
                Shadow_OffsetX = Cursor.Shadow_OffsetX;
                Shadow_OffsetY = Cursor.Shadow_OffsetY;
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
        public float LineThickness = 1f;
        public float Scale = 1f;
        public float Angle = 180f;
        public bool Shadow_Enabled = false;
        public Color Shadow_Color = Color.Black;
        public int Shadow_Blur = 5;
        public float Shadow_Opacity = 0.3f;
        public int Shadow_OffsetX = 2;
        public int Shadow_OffsetY = 2;
    }
}