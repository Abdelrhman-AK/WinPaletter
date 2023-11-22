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

        private static TextureBrush Noise = new(Properties.Resources.GaussianBlurOpaque.Fade(0.2d));

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
            var b = new Bitmap((int)Math.Round(32f * CursorOptions.Scale), (int)Math.Round(32f * CursorOptions.Scale), PixelFormat.Format32bppPArgb);
            var G = Graphics.FromImage(b);

            //G.SmoothingMode = (CursorOptions.ArrowStyle == ArrowStyle.Classic || CursorOptions.CircleStyle == CircleStyle.Classic) ? SmoothingMode.HighSpeed : SmoothingMode.HighQuality;

            G.SmoothingMode = SmoothingMode.HighQuality;

            G.Clear(Color.Transparent);

            #region Rectangles Helpers
            var _Arrow = new Rectangle(0, 0, b.Width, b.Height);
            var _Help = new Rectangle(11, 6, b.Width, b.Height);
            var _Busy = new Rectangle(0, 0, 22, 22);
            var _CurRect = new Rectangle(0, 8, b.Width, b.Height);
            var _LoadRect = new Rectangle(6, 0, (int)Math.Round(22f * CursorOptions.Scale), (int)Math.Round(22f * CursorOptions.Scale));
            var _Pin = new Rectangle(15, 11, b.Width, b.Height);
            var _Person = new Rectangle(19, 17, b.Width, b.Height);
            #endregion

            if (!CursorOptions.UseFromFile || !System.IO.File.Exists(CursorOptions.File))
            {
                switch (CursorOptions.Cursor)
                {
                    case CursorType.Arrow:
                        {
                            #region Arrow
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

                            var PL = new Pen(BL, CursorOptions.LineThickness);

                            G.FillPath(BB, DefaultCursor(_Arrow, CursorOptions.ArrowStyle, CursorOptions.Scale));

                            if (CursorOptions.PrimaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity));
                                G.FillPath(Noise, DefaultCursor(_Arrow, CursorOptions.ArrowStyle, CursorOptions.Scale));
                            }

                            G.DrawPath(PL, DefaultCursor(_Arrow, CursorOptions.ArrowStyle, CursorOptions.Scale));

                            if (CursorOptions.SecondaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity));
                                G.DrawPath(new Pen(Noise, CursorOptions.LineThickness), DefaultCursor(_Arrow, CursorOptions.ArrowStyle, CursorOptions.Scale));
                            }

                            break;
                        }
                    #endregion
                    case CursorType.Help:
                        {
                            #region Help
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
                            var PL = new Pen(BL, CursorOptions.LineThickness);

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
                            var PL_H = new Pen(BL_H, CursorOptions.LineThickness);


                            G.FillPath(BB, DefaultCursor(_Arrow, CursorOptions.ArrowStyle, CursorOptions.Scale));

                            if (CursorOptions.PrimaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity));
                                G.FillPath(Noise, DefaultCursor(_Arrow, CursorOptions.ArrowStyle, CursorOptions.Scale));
                            }

                            G.DrawPath(PL, DefaultCursor(_Arrow, CursorOptions.ArrowStyle, CursorOptions.Scale));

                            if (CursorOptions.SecondaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity));
                                G.DrawPath(new Pen(Noise, CursorOptions.LineThickness), DefaultCursor(_Arrow, CursorOptions.ArrowStyle, CursorOptions.Scale));
                            }

                            G.FillPath(BB_H, Help(_Help, CursorOptions.ArrowStyle, CursorOptions.Scale));

                            if (CursorOptions.PrimaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity));
                                G.FillPath(Noise, Help(_Help, CursorOptions.ArrowStyle, CursorOptions.Scale));
                            }

                            if (CursorOptions.ArrowStyle != ArrowStyle.Classic)
                                G.DrawPath(PL_H, Help(_Help, CursorOptions.ArrowStyle, CursorOptions.Scale));

                            if (CursorOptions.SecondaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity));
                                G.DrawPath(new Pen(Noise, CursorOptions.LineThickness), Help(_Help, CursorOptions.ArrowStyle, CursorOptions.Scale));
                            }

                            break;
                        }

                    #endregion
                    case CursorType.Busy:
                        {
                            #region Busy
                            Brush BC, BH;
                            if (CursorOptions.LoadingCircleBackGradient)
                            {
                                BC = ReturnGradience(_Arrow, CursorOptions.LoadingCircleBack1, CursorOptions.LoadingCircleBack2, CursorOptions.LoadingCircleBackGradientMode, CursorOptions._Angle);
                            }
                            else
                            {
                                BC = new SolidBrush(CursorOptions.LoadingCircleBack1);
                            }
                            if (CursorOptions.LoadingCircleHotGradient)
                            {
                                BH = ReturnGradience(_Arrow, CursorOptions.LoadingCircleHot1, CursorOptions.LoadingCircleHot2, CursorOptions.LoadingCircleHotGradientMode, CursorOptions._Angle);
                            }
                            else
                            {
                                BH = new SolidBrush(CursorOptions.LoadingCircleHot1);
                            }

                            if (CursorOptions.CircleStyle == CircleStyle.Classic)
                            {
                                var PL = new Pen(BH, CursorOptions.LineThickness);

                                G.FillPath(BC, Busy(_Busy, CursorOptions._Angle, CursorOptions.CircleStyle, CursorOptions.Scale));
                                G.DrawPath(PL, BusyLoader(_Busy, CursorOptions._Angle, CursorOptions.CircleStyle, CursorOptions.Scale));
                                G.DrawPath(PL, Busy(_Busy, CursorOptions._Angle, CursorOptions.CircleStyle, CursorOptions.Scale));

                                if (CursorOptions.LoadingCircleBackNoise)
                                {
                                    Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.LoadingCircleBackNoiseOpacity));
                                    G.FillPath(Noise, Busy(_Busy, CursorOptions._Angle, CursorOptions.CircleStyle, CursorOptions.Scale));
                                }

                                if (CursorOptions.LoadingCircleHotNoise)
                                {
                                    Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.LoadingCircleHotNoiseOpacity));
                                    G.DrawPath(new Pen(Noise, CursorOptions.LineThickness), BusyLoader(_Busy, CursorOptions._Angle, CursorOptions.CircleStyle, CursorOptions.Scale));
                                    G.DrawPath(new Pen(Noise, CursorOptions.LineThickness), Busy(_Busy, CursorOptions._Angle, CursorOptions.CircleStyle, CursorOptions.Scale));
                                }
                            }

                            else
                            {
                                G.FillPath(BC, Busy(_Busy, CursorOptions._Angle, CursorOptions.CircleStyle, CursorOptions.Scale));

                                if (CursorOptions.LoadingCircleBackNoise)
                                {
                                    Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.LoadingCircleBackNoiseOpacity));
                                    G.FillPath(Noise, Busy(_Busy, CursorOptions._Angle, CursorOptions.CircleStyle, CursorOptions.Scale));
                                }

                                G.FillPath(BH, BusyLoader(_Busy, CursorOptions._Angle, CursorOptions.CircleStyle, CursorOptions.Scale));

                                if (CursorOptions.LoadingCircleHotNoise)
                                {
                                    Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.LoadingCircleHotNoiseOpacity));
                                    G.FillPath(Noise, BusyLoader(_Busy, CursorOptions._Angle, CursorOptions.CircleStyle, CursorOptions.Scale));
                                }

                            }

                            break;
                        }


                    #endregion
                    case CursorType.AppLoading:
                        {
                            #region AppLoading
                            Brush BB, BL;
                            if (CursorOptions.PrimaryColorGradient)
                            {
                                BB = ReturnGradience(_Arrow, CursorOptions.PrimaryColor1, CursorOptions.PrimaryColor2, CursorOptions.PrimaryColorGradientMode, CursorOptions._Angle);
                            }
                            else
                            {
                                BB = new SolidBrush(CursorOptions.PrimaryColor1);
                            }
                            if (CursorOptions.SecondaryColorGradient)
                            {
                                BL = ReturnGradience(_Arrow, CursorOptions.SecondaryColor1, CursorOptions.SecondaryColor2, CursorOptions.SecondaryColorGradientMode, CursorOptions._Angle);
                            }
                            else
                            {
                                BL = new SolidBrush(CursorOptions.SecondaryColor1);
                            }
                            var PL = new Pen(BL, CursorOptions.LineThickness);

                            Brush BC, BH;
                            if (CursorOptions.LoadingCircleBackGradient)
                            {
                                BC = ReturnGradience(_LoadRect, CursorOptions.LoadingCircleBack1, CursorOptions.LoadingCircleBack2, CursorOptions.LoadingCircleBackGradientMode, CursorOptions._Angle);
                            }
                            else
                            {
                                BC = new SolidBrush(CursorOptions.LoadingCircleBack1);
                            }
                            if (CursorOptions.LoadingCircleHotGradient)
                            {
                                BH = ReturnGradience(_LoadRect, CursorOptions.LoadingCircleHot1, CursorOptions.LoadingCircleHot2, CursorOptions.LoadingCircleHotGradientMode, CursorOptions._Angle);
                            }
                            else
                            {
                                BH = new SolidBrush(CursorOptions.LoadingCircleHot1);
                            }

                            G.FillPath(BB, DefaultCursor(_CurRect, CursorOptions.ArrowStyle, CursorOptions.Scale));

                            if (CursorOptions.PrimaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity));
                                G.FillPath(Noise, DefaultCursor(_CurRect, CursorOptions.ArrowStyle, CursorOptions.Scale));
                            }

                            G.DrawPath(PL, DefaultCursor(_CurRect, CursorOptions.ArrowStyle, CursorOptions.Scale));

                            if (CursorOptions.SecondaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity));
                                G.DrawPath(new Pen(Noise, CursorOptions.LineThickness), DefaultCursor(_CurRect, CursorOptions.ArrowStyle, CursorOptions.Scale));
                            }

                            if (CursorOptions.CircleStyle == CircleStyle.Classic)
                            {
                                var PLx = new Pen(BH, CursorOptions.LineThickness);

                                G.FillPath(BC, AppLoading(_Busy, CursorOptions._Angle, CursorOptions.CircleStyle, CursorOptions.Scale));
                                G.DrawPath(PLx, AppLoaderCircle(_Busy, CursorOptions._Angle, CursorOptions.CircleStyle, CursorOptions.Scale));
                                G.DrawPath(PLx, AppLoading(_Busy, CursorOptions._Angle, CursorOptions.CircleStyle, CursorOptions.Scale));

                                if (CursorOptions.LoadingCircleBackNoise)
                                {
                                    Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.LoadingCircleBackNoiseOpacity));
                                    G.FillPath(Noise, AppLoading(_Busy, CursorOptions._Angle, CursorOptions.CircleStyle, CursorOptions.Scale));
                                }

                                if (CursorOptions.LoadingCircleHotNoise)
                                {
                                    Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.LoadingCircleHotNoiseOpacity));
                                    G.DrawPath(new Pen(Noise, CursorOptions.LineThickness), AppLoaderCircle(_Busy, CursorOptions._Angle, CursorOptions.CircleStyle, CursorOptions.Scale));
                                    G.DrawPath(new Pen(Noise, CursorOptions.LineThickness), AppLoading(_Busy, CursorOptions._Angle, CursorOptions.CircleStyle, CursorOptions.Scale));
                                }
                            }

                            else
                            {
                                G.FillPath(BC, AppLoading(_LoadRect, CursorOptions._Angle, CursorOptions.CircleStyle, CursorOptions.Scale));

                                if (CursorOptions.LoadingCircleBackNoise)
                                {
                                    Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.LoadingCircleBackNoiseOpacity));
                                    G.FillPath(Noise, AppLoading(_LoadRect, CursorOptions._Angle, CursorOptions.CircleStyle, CursorOptions.Scale));
                                }

                                G.FillPath(BH, AppLoaderCircle(_LoadRect, CursorOptions._Angle, CursorOptions.CircleStyle, CursorOptions.Scale));

                                if (CursorOptions.LoadingCircleHotNoise)
                                {
                                    Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.LoadingCircleHotNoiseOpacity));
                                    G.FillPath(Noise, AppLoaderCircle(_LoadRect, CursorOptions._Angle, CursorOptions.CircleStyle, CursorOptions.Scale));
                                }
                            }

                            break;
                        }

                    #endregion
                    case CursorType.None:
                        {
                            #region None
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
                            var PL = new Pen(BL, CursorOptions.LineThickness);


                            G.FillPath(BB, NoneBackground(_Arrow, CursorOptions.Scale));

                            if (CursorOptions.PrimaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity));
                                G.FillPath(Noise, NoneBackground(_Arrow, CursorOptions.Scale));
                            }

                            G.FillPath(BL, None(_Arrow, CursorOptions.Scale));

                            if (CursorOptions.SecondaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity));
                                G.FillPath(Noise, None(_Arrow, CursorOptions.Scale));
                            }

                            break;
                        }
                    #endregion
                    case CursorType.Move:
                        {
                            #region Move
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
                            var PL = new Pen(BL, CursorOptions.LineThickness);

                            G.FillPath(BB, Move(_Arrow, CursorOptions.Scale));

                            if (CursorOptions.PrimaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity));
                                G.FillPath(Noise, Move(_Arrow, CursorOptions.Scale));
                            }

                            G.DrawPath(PL, Move(_Arrow, CursorOptions.Scale));

                            if (CursorOptions.SecondaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity));
                                G.DrawPath(new Pen(Noise, CursorOptions.LineThickness), Move(_Arrow, CursorOptions.Scale));
                            }

                            break;
                        }
                    #endregion
                    case CursorType.Up:
                        {
                            #region Up
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
                            var PL = new Pen(BL, CursorOptions.LineThickness);

                            G.FillPath(BB, Up(_Arrow, CursorOptions.Scale));

                            if (CursorOptions.PrimaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity));
                                G.FillPath(Noise, Up(_Arrow, CursorOptions.Scale));
                            }

                            G.DrawPath(PL, Up(_Arrow, CursorOptions.Scale));

                            if (CursorOptions.SecondaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity));
                                G.DrawPath(new Pen(Noise, CursorOptions.LineThickness), Up(_Arrow, CursorOptions.Scale));
                            }

                            break;
                        }
                    #endregion
                    case CursorType.NS:
                        {
                            #region NS
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
                            var PL = new Pen(BL, CursorOptions.LineThickness);

                            G.FillPath(BB, NS(_Arrow, CursorOptions.Scale));

                            if (CursorOptions.PrimaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity));
                                G.FillPath(Noise, NS(_Arrow, CursorOptions.Scale));
                            }

                            G.DrawPath(PL, NS(_Arrow, CursorOptions.Scale));

                            if (CursorOptions.SecondaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity));
                                G.DrawPath(new Pen(Noise, CursorOptions.LineThickness), NS(_Arrow, CursorOptions.Scale));
                            }

                            break;
                        }
                    #endregion
                    case CursorType.EW:
                        {
                            #region EW
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
                            var PL = new Pen(BL, CursorOptions.LineThickness);

                            G.FillPath(BB, EW(_Arrow, CursorOptions.Scale));

                            if (CursorOptions.PrimaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity));
                                G.FillPath(Noise, EW(_Arrow, CursorOptions.Scale));
                            }

                            G.DrawPath(PL, EW(_Arrow, CursorOptions.Scale));

                            if (CursorOptions.SecondaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity));
                                G.DrawPath(new Pen(Noise, CursorOptions.LineThickness), EW(_Arrow, CursorOptions.Scale));
                            }

                            break;
                        }
                    #endregion
                    case CursorType.NESW:
                        {
                            #region NESW
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
                            var PL = new Pen(BL, CursorOptions.LineThickness);

                            G.FillPath(BB, NESW(_Arrow, CursorOptions.Scale));

                            if (CursorOptions.PrimaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity));
                                G.FillPath(Noise, NESW(_Arrow, CursorOptions.Scale));
                            }

                            G.DrawPath(PL, NESW(_Arrow, CursorOptions.Scale));

                            if (CursorOptions.SecondaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity));
                                G.DrawPath(new Pen(Noise, CursorOptions.LineThickness), NESW(_Arrow, CursorOptions.Scale));
                            }

                            break;
                        }

                    #endregion
                    case CursorType.NWSE:
                        {
                            #region NWSE
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
                            var PL = new Pen(BL, CursorOptions.LineThickness);

                            G.FillPath(BB, NWSE(_Arrow, CursorOptions.Scale));

                            if (CursorOptions.PrimaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity));
                                G.FillPath(Noise, NWSE(_Arrow, CursorOptions.Scale));
                            }

                            G.DrawPath(PL, NWSE(_Arrow, CursorOptions.Scale));

                            if (CursorOptions.SecondaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity));
                                G.DrawPath(new Pen(Noise, CursorOptions.LineThickness), NWSE(_Arrow, CursorOptions.Scale));
                            }

                            break;
                        }
                    #endregion
                    case CursorType.Pen:
                        {
                            #region Pen
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
                            var PL = new Pen(BL, CursorOptions.LineThickness);

                            G.FillPath(BB, PenBackground(_Arrow, CursorOptions.Scale));

                            if (CursorOptions.PrimaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity));
                                G.FillPath(Noise, PenBackground(_Arrow, CursorOptions.Scale));
                            }

                            G.DrawPath(PL, Pen(_Arrow, CursorOptions.Scale));

                            if (CursorOptions.SecondaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity));
                                G.DrawPath(new Pen(Noise, CursorOptions.LineThickness), Pen(_Arrow, CursorOptions.Scale));
                            }

                            break;
                        }
                    #endregion
                    case CursorType.Link:
                        {
                            #region Link
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
                            var PL = new Pen(BL, CursorOptions.LineThickness);

                            G.FillPath(BB, Hand(_Arrow, CursorOptions.ArrowStyle, CursorOptions.Scale));

                            if (CursorOptions.PrimaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity));
                                G.FillPath(Noise, Hand(_Arrow, CursorOptions.ArrowStyle, CursorOptions.Scale));
                            }

                            G.DrawPath(PL, Hand(_Arrow, CursorOptions.ArrowStyle, CursorOptions.Scale));

                            if (CursorOptions.SecondaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity));
                                G.DrawPath(new Pen(Noise, CursorOptions.LineThickness), Hand(_Arrow, CursorOptions.ArrowStyle, CursorOptions.Scale));
                            }

                            break;
                        }
                    #endregion
                    case CursorType.Pin:
                        {
                            #region Pin
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
                            var PL = new Pen(BL, CursorOptions.LineThickness);

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

                            G.FillPath(BB, Hand(_Arrow, CursorOptions.ArrowStyle, CursorOptions.Scale));

                            if (CursorOptions.PrimaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity));
                                G.FillPath(Noise, Hand(_Arrow, CursorOptions.ArrowStyle, CursorOptions.Scale));
                            }

                            G.DrawPath(PL, Hand(_Arrow, CursorOptions.ArrowStyle, CursorOptions.Scale));

                            if (CursorOptions.SecondaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity));
                                G.DrawPath(new Pen(Noise, CursorOptions.LineThickness), Hand(_Arrow, CursorOptions.ArrowStyle, CursorOptions.Scale));
                            }

                            G.FillPath(BB_P, Pin(_Pin, CursorOptions.Scale));

                            if (CursorOptions.PrimaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity));
                                G.FillPath(Noise, Pin(_Pin, CursorOptions.Scale));
                            }

                            G.FillPath(BL_P, Pin_CenterPoint(_Pin, CursorOptions.Scale));

                            if (CursorOptions.SecondaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity));
                                G.FillPath(Noise, Pin_CenterPoint(_Pin, CursorOptions.Scale));
                            }

                            G.DrawPath(new Pen(BL_P, 2f), Pin(_Pin, CursorOptions.Scale));

                            if (CursorOptions.SecondaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity));
                                G.DrawPath(new Pen(Noise, CursorOptions.LineThickness), Pin(_Pin, CursorOptions.Scale));
                            }

                            break;
                        }
                    #endregion
                    case CursorType.Person:
                        {
                            #region Person
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
                            var PL = new Pen(BL, CursorOptions.LineThickness);

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

                            G.FillPath(BB, Hand(_Arrow, CursorOptions.ArrowStyle, CursorOptions.Scale));

                            if (CursorOptions.PrimaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity));
                                G.FillPath(Noise, Hand(_Arrow, CursorOptions.ArrowStyle, CursorOptions.Scale));
                            }

                            G.DrawPath(PL, Hand(_Arrow, CursorOptions.ArrowStyle, CursorOptions.Scale));

                            if (CursorOptions.SecondaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity));
                                G.DrawPath(new Pen(Noise, CursorOptions.LineThickness), Hand(_Arrow, CursorOptions.ArrowStyle, CursorOptions.Scale));
                            }

                            G.FillPath(BB_P, Person(_Person, CursorOptions.Scale));

                            if (CursorOptions.PrimaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity));
                                G.FillPath(Noise, Person(_Person, CursorOptions.Scale));
                            }

                            G.DrawPath(new Pen(BL_P, 2f), Person(_Person, CursorOptions.Scale));

                            if (CursorOptions.SecondaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity));
                                G.DrawPath(new Pen(Noise, CursorOptions.LineThickness), Person(_Person, CursorOptions.Scale));
                            }

                            break;
                        }
                    #endregion
                    case CursorType.IBeam:
                        {
                            #region IBeam
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
                            var PL = new Pen(BL, CursorOptions.LineThickness);

                            G.FillPath(BB, IBeam(_Arrow, CursorOptions.Scale));

                            if (CursorOptions.PrimaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity));
                                G.FillPath(Noise, IBeam(_Arrow, CursorOptions.Scale));
                            }

                            G.DrawPath(PL, IBeam(_Arrow, CursorOptions.Scale));

                            if (CursorOptions.SecondaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity));
                                G.DrawPath(new Pen(Noise, CursorOptions.LineThickness), IBeam(_Arrow, CursorOptions.Scale));
                            }

                            break;
                        }
                    #endregion
                    case CursorType.Cross:
                        {
                            #region Cross
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
                            var PL = new Pen(BL, CursorOptions.LineThickness);

                            G.FillPath(BB, Cross(_Arrow, CursorOptions.Scale));

                            if (CursorOptions.PrimaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.PrimaryNoiseOpacity));
                                G.FillPath(Noise, Cross(_Arrow, CursorOptions.Scale));
                            }

                            G.DrawPath(PL, Cross(_Arrow, CursorOptions.Scale));

                            if (CursorOptions.SecondaryNoise)
                            {
                                Noise = new TextureBrush(Properties.Resources.GaussianBlurOpaque.Fade(CursorOptions.SecondaryNoiseOpacity));
                                G.DrawPath(new Pen(Noise, CursorOptions.LineThickness), Cross(_Arrow, CursorOptions.Scale));
                            }

                            break;
                        }
                        #endregion
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

            var B_Final = new Bitmap(b.Width, b.Height);
            var G_Final = Graphics.FromImage(B_Final);

            if (CursorOptions.Shadow_Enabled)
            {
                var shadowedBMP = new Bitmap(b);

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
            Dot,
            Classic
        }

        public static GraphicsPath DefaultCursor(Rectangle Rectangle, ArrowStyle Style, float Scale = 1f)
        {
            var path = new GraphicsPath();
            var R = new Rectangle(Rectangle.X, Rectangle.Y, 12, 18);

            switch (Style)
            {
                case ArrowStyle.Aero:
                    {
                        // #### Left Border
                        var LLine1 = new Point(R.X, R.Y);
                        var LLine2 = new Point(R.X, R.Y + R.Height - 2);
                        path.AddLine(LLine1, LLine2);

                        // #### Left Down Border
                        var DLLine1 = LLine2 + (Size)new Point(1, 0);
                        var DLLine2 = new Point(DLLine1.X + 3, DLLine1.Y - 3);
                        path.AddLine(DLLine1, DLLine2);

                        // #### Left Down Handle Border
                        var DLHLine1 = DLLine2;
                        var DLHLine2 = new Point(DLHLine1.X + 3, DLHLine1.Y + 5);
                        path.AddLine(DLHLine1, DLHLine2);

                        // #### Down Handle Border
                        var DHLine1 = DLHLine2;
                        var DHLine2 = new Point(DHLine1.X + 2, DHLine1.Y - 1);

                        // #### Right Down Handle Border
                        var DRHLine1 = DHLine2;
                        var DRHLine2 = new Point(DLHLine1.X + 3, DLHLine1.Y - 1);
                        path.AddLine(DRHLine1, DRHLine2);

                        // #### Right Down Border
                        var DRLine1 = DRHLine2;
                        var DRLine2 = new Point(R.X + R.Width - 1, DLHLine1.Y - 1);
                        path.AddLine(DRLine1, DRLine2);

                        // #### Right Border
                        var RLine1 = DRLine2 + (Size)new Point(0, -1);
                        var RLine2 = LLine1;
                        path.AddLine(RLine1, RLine2);
                        break;
                    }

                case ArrowStyle.Classic:
                    {
                        // #### Left Border
                        var LLine1 = new Point(R.X, R.Y);
                        var LLine2 = new Point(R.X, R.Y + R.Height - 2);
                        path.AddLine(LLine1, LLine2);

                        // #### Left Down Border
                        var DLLine1 = LLine2 + (Size)new Point(1, -1);
                        var DLLine2 = new Point(DLLine1.X + 3, DLLine1.Y - 3);
                        path.AddLine(DLLine1, DLLine2);

                        // #### Left Down Handle Border
                        var DLHLine1 = DLLine2;
                        var DLHLine2 = new Point(DLHLine1.X + 4, DLHLine1.Y + 8);
                        path.AddLine(DLHLine1, DLHLine2);

                        // #### Down Handle Border
                        var DHLine1 = DLHLine2;
                        var DHLine2 = new Point(DHLine1.X + 2, DHLine1.Y - 1);

                        // #### Right Down Handle Border
                        var DRHLine1 = DHLine2;
                        var DRHLine2 = new Point(DLHLine1.X + 3, DLHLine1.Y);
                        path.AddLine(DRHLine1, DRHLine2);

                        // #### Right Down Border
                        var DRLine1 = DRHLine2 + (Size)new Point(0, -1);
                        var DRLine2 = new Point(R.X + R.Width - 1, DRLine1.Y);
                        path.AddLine(DRLine1, DRLine2);

                        // #### Right Border
                        var RLine1 = DRLine2 + (Size)new Point(-1, -1);
                        var RLine2 = LLine1;
                        path.AddLine(RLine1, RLine2);
                        break;
                    }

                case ArrowStyle.Modern:
                    {
                        // #### Left Border
                        var LLine1 = new Point(R.X, R.Y);
                        var LLine2 = new Point(R.X, R.Y + R.Height - 2);
                        path.AddLine(LLine1, LLine2);

                        // #### Left Down Border
                        var DLLine1 = LLine2 + (Size)new Point(1, 0);
                        var DLLine2 = new Point(DLLine1.X + 4, DLLine1.Y - 4);
                        path.AddLine(DLLine1, DLLine2);

                        // #### Right Down Border
                        var DRLine1 = DLLine2;
                        var DRLine2 = new Point(R.X + R.Width - 1, DRLine1.Y);
                        path.AddLine(DRLine1, DRLine2);

                        // #### Right Border
                        var RLine1 = DRLine2 + (Size)new Point(0, -1);
                        var RLine2 = LLine1;
                        path.AddLine(RLine1, RLine2);
                        break;
                    }

            }

            // path.CloseFigure()

            var m = new Matrix();
            m.Scale(Scale, Scale, MatrixOrder.Append);
            m.Translate(1f, 1f, MatrixOrder.Append);
            path.Transform(m);
            return path;

        }

        public static GraphicsPath Busy(Rectangle Rectangle, float Angle, CircleStyle Style, float Scale = 1f)
        {
            Rectangle.Width = 22;
            Rectangle.Height = 22;
            var path = new GraphicsPath();

            switch (Style)
            {
                case CircleStyle.Aero:
                    {
                        path.AddEllipse(Rectangle.X, Rectangle.Y, 22, 22);
                        var R = new Rectangle(Rectangle.X + 5, Rectangle.Y + 5, 12, 12);
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
                            var m_rotate = new Matrix();
                            m_rotate.RotateAt((Angle - 180f) * 3f, new Point((int)Math.Round(Rectangle.X + Rectangle.Width / 2d), (int)Math.Round(Rectangle.Y + Rectangle.Height / 2d)));
                            path.Transform(m_rotate);
                        }

                        break;
                    }

                case CircleStyle.Dot:
                    {
                        path.AddEllipse(Rectangle.X, Rectangle.Y, 22, 22);
                        var R = new Rectangle(Rectangle.X + 5, Rectangle.Y + 5, 12, 12);
                        path.AddEllipse(R);
                        path.CloseFigure();
                        break;
                    }

            }

            var m = new Matrix();
            m.Scale(Scale, Scale, MatrixOrder.Append);
            m.Translate(1f, 1f, MatrixOrder.Append);
            path.Transform(m);

            return path;
        }

        public static GraphicsPath BusyLoader(Rectangle Rectangle, float Angle, CircleStyle Style, float Scale = 1f)
        {
            Rectangle.X += 0;
            Rectangle.Y += 0;

            Rectangle.Width = 22;
            Rectangle.Height = 22;

            var path = new GraphicsPath();
            var CPoint = new Point((int)Math.Round(Rectangle.X + Rectangle.Width / 2d), (int)Math.Round(Rectangle.Y + Rectangle.Height / 2d));
            var R = new Rectangle(Rectangle.X + 5, Rectangle.Y + 5, 12, 12);

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
                        var PU1 = new Point(Rectangle.X + 12, Rectangle.Y + 5);
                        var PU3 = new Point(Rectangle.X + 10, Rectangle.Y + 5);
                        var PU4 = PU3 + (Size)new Point(2, 2);

                        path.AddLine(PU1, PU1);
                        path.CloseFigure();
                        path.AddLine(PU3, PU4);
                        path.CloseFigure();

                        var PL1 = new Point(Rectangle.X + 12, Rectangle.Y + 17);
                        var PL2 = PL1 - (Size)new Point(1, -1);

                        var PL3 = PL1 - (Size)new Point(0, 2);
                        var PL4 = PL3 - (Size)new Point(3, -3);

                        path.AddLine(PL1, PL2);
                        path.CloseFigure();

                        path.AddLine(PL3, PL4);
                        path.CloseFigure();

                        path.AddPath(MirrorRight(path), false);

                        var C1 = PU1 + (Size)new Point(0, 4);
                        path.AddLine(C1, C1);
                        path.CloseFigure();

                        var C2 = C1 + (Size)new Point(0, 4);
                        path.AddLine(C2, C2);
                        path.CloseFigure();

                        if (Angle >= 270f)
                        {
                            var m_rotate = new Matrix();
                            m_rotate.RotateAt((Angle - 180f) * 3f, new Point((int)Math.Round(Rectangle.X + Rectangle.Width / 2d), (int)Math.Round(Rectangle.Y + Rectangle.Height / 2d)));
                            path.Transform(m_rotate);
                        }

                        break;
                    }

                case CircleStyle.Dot:
                    {
                        float x = (float)(0.85d * CPoint.X + 0.65d * R.Width * (double)(float)Math.Cos((double)(Angle / 180f) * Math.PI));
                        float y = (float)(0.85d * CPoint.Y + 0.65d * R.Height * (double)(float)Math.Sin((double)(Angle / 180f) * Math.PI));
                        x = Math.Max(Rectangle.Left, Math.Min(x, Rectangle.Right));
                        y = Math.Max(Rectangle.Top, Math.Min(y, Rectangle.Bottom));
                        path.AddEllipse(new Rectangle((int)Math.Round(x), (int)Math.Round(y), 5, 5));
                        break;
                    }

            }

            var m = new Matrix();
            m.Scale(Scale, Scale, MatrixOrder.Append);
            m.Translate(1f, 1f, MatrixOrder.Append);
            path.Transform(m);

            return path;
        }

        public static GraphicsPath AppLoading(Rectangle Rectangle, float Angle, CircleStyle Style, float Scale = 1f)
        {
            Rectangle.Width = 16;
            Rectangle.Height = 16;
            var path = new GraphicsPath();

            switch (Style)
            {
                case CircleStyle.Aero:
                    {
                        path.AddEllipse(Rectangle);
                        var R = new Rectangle(Rectangle.X + 4, Rectangle.Y + 4, 8, 8);
                        path.AddEllipse(R);
                        path.CloseFigure();
                        break;
                    }

                case CircleStyle.Classic:
                    {
                        var UpperRectangle = new Rectangle(Rectangle.X + 12, Rectangle.Y + 9, 9, 2);
                        var LowerRectangle = new Rectangle(Rectangle.X + 12, Rectangle.Y + 23, 9, 2);
                        var Container = new Rectangle(UpperRectangle.X, UpperRectangle.Y, UpperRectangle.Width, LowerRectangle.Bottom - UpperRectangle.Top);

                        var pL1 = new Point(UpperRectangle.X + 1, UpperRectangle.Y + UpperRectangle.Height);
                        var pL2 = new Point(pL1.X, pL1.Y + 4);
                        path.AddLine(pL1, pL2);

                        var pL3 = pL2 + (Size)new Point(1, 0);
                        var pL4 = pL3 + (Size)new Point(1, 1);
                        path.AddLine(pL3, pL4);

                        var pL5 = pL4 + (Size)new Point(0, 1);
                        path.AddLine(pL5, pL5);

                        var pL6 = pL5 + (Size)new Point(0, 1);
                        var pl7 = pL6 + (Size)new Point(-1, 1);
                        path.AddLine(pL6, pl7);

                        var pL8 = pl7 - (Size)new Point(1, 0);
                        var pL9 = pL8 + (Size)new Point(0, 4);
                        path.AddLine(pL8, pL9);

                        var pL10 = pL9 + (Size)new Point(7, 0);
                        var pL11 = pL10 - (Size)new Point(0, 4);
                        path.AddLine(pL10, pL11);

                        var pL12 = pL11 + (Size)new Point(-1, 0);
                        var pL13 = pL12 + (Size)new Point(-1, -1);
                        path.AddLine(pL12, pL13);

                        var pL14 = pL13 + (Size)new Point(0, -1);
                        path.AddLine(pL14, pL14);

                        var pL15 = pL14 + (Size)new Point(0, -1);
                        var pl16 = pL15 + (Size)new Point(1, -1);
                        path.AddLine(pL15, pl16);

                        var pL17 = pl16 + (Size)new Point(1, 0);
                        var pL18 = pL17 + (Size)new Point(0, -4);
                        path.AddLine(pL17, pL18);

                        path.AddRectangle(UpperRectangle);

                        path.AddRectangle(LowerRectangle);

                        path.CloseFigure();

                        var FixerL0 = pL3 + (Size)new Point(0, 1);
                        var FixerL1 = pL3 + (Size)new Point(0, 3);

                        var FixerR0 = pL12 - (Size)new Point(0, 1);
                        var FixerR1 = pL12 - (Size)new Point(0, 3);

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
                            var m_rotate = new Matrix();
                            m_rotate.RotateAt((Angle - 180f) * 3f, new Point((int)Math.Round(Container.X + Container.Width / 2d), (int)Math.Round(Container.Y + Container.Height / 2d)));
                            path.Transform(m_rotate);
                        }

                        break;
                    }

                case CircleStyle.Dot:
                    {
                        path.AddEllipse(Rectangle);
                        var R = new Rectangle(Rectangle.X + 4, Rectangle.Y + 4, 8, 8);
                        path.AddEllipse(R);
                        path.CloseFigure();
                        break;
                    }

            }


            var m = new Matrix();
            m.Scale(Scale, Scale, MatrixOrder.Append);
            m.Translate(1f, 1f, MatrixOrder.Append);
            path.Transform(m);

            return path;
        }

        public static GraphicsPath AppLoaderCircle(Rectangle Rectangle, float Angle, CircleStyle Style, float Scale = 1f)
        {
            Rectangle.Width = 16;
            Rectangle.Height = 16;

            var path = new GraphicsPath();
            var CPoint = new Point((int)Math.Round(Rectangle.X + Rectangle.Width / 2d), (int)Math.Round(Rectangle.Y + Rectangle.Height / 2d));
            var R = new Rectangle(Rectangle.X + 4, Rectangle.Y + 4, 8, 8);

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
                        var UpperRectangle = new Rectangle(Rectangle.X + 12, Rectangle.Y + 9, 9, 2);
                        var LowerRectangle = new Rectangle(Rectangle.X + 12, Rectangle.Y + 23, 9, 2);
                        var Container = new Rectangle(UpperRectangle.X, UpperRectangle.Y, UpperRectangle.Width, LowerRectangle.Bottom - UpperRectangle.Top);

                        var PU1 = new Point(Rectangle.X + 17, Rectangle.Y + 14);
                        var PU3 = new Point(Rectangle.X + 14, Rectangle.Y + 15);
                        var PU4 = PU3 + (Size)new Point(2, 2);

                        path.AddLine(PU1, PU3);
                        path.CloseFigure();
                        path.AddLine(PU3, PU4);
                        path.CloseFigure();


                        var PL1 = new Point(Rectangle.X + 16, Rectangle.Y + 20);
                        var PL2 = PL1 - (Size)new Point(2, -2);
                        path.AddLine(PL1, PL2);
                        path.CloseFigure();

                        var PL3 = PL1 + (Size)new Point(1, 1);
                        var PL4 = PL3 - (Size)new Point(1, -1);
                        path.AddLine(PL3, PL4);
                        path.CloseFigure();

                        var C1 = PL3 + (Size)new Point(1, 1);
                        path.AddLine(C1, C1);
                        path.CloseFigure();

                        if (Angle >= 270f)
                        {
                            var m_rotate = new Matrix();
                            m_rotate.RotateAt((Angle - 180f) * 3f, new Point((int)Math.Round(Container.X + Container.Width / 2d), (int)Math.Round(Container.Y + Container.Height / 2d)));
                            path.Transform(m_rotate);
                        }

                        break;
                    }

                case CircleStyle.Dot:
                    {
                        float x = (float)(0.85d * CPoint.X + 0.65d * R.Width * (double)(float)Math.Cos((double)(Angle / 180f) * Math.PI));
                        float y = (float)(0.85d * CPoint.Y + 0.65d * R.Height * (double)(float)Math.Sin((double)(Angle / 180f) * Math.PI));
                        x = Math.Max(Rectangle.Left, Math.Min(x, Rectangle.Right));
                        y = Math.Max(Rectangle.Top, Math.Min(y, Rectangle.Bottom));
                        path.AddEllipse(new Rectangle((int)Math.Round(x), (int)Math.Round(y), 5, 5));
                        break;
                    }

            }


            path.CloseFigure();

            var m = new Matrix();
            m.Scale(Scale, Scale, MatrixOrder.Append);
            m.Translate(1f, 1f, MatrixOrder.Append);
            path.Transform(m);

            return path;
        }

        public static GraphicsPath Move(Rectangle Rectangle, float Scale = 1f)
        {
            var path = new GraphicsPath();
            Rectangle.Width = 21;
            Rectangle.Height = 21;
            Rectangle.X = 0;
            Rectangle.Y = 0;

            var UL1 = new Point(Rectangle.X + 11, Rectangle.Y);
            var UL2 = new Point(UL1.X - 4, Rectangle.Y + 4);
            path.AddLine(UL1, UL2);

            var ULX1 = new Point(UL2.X, UL2.Y + 1);
            var ULX2 = new Point(ULX1.X + 3, ULX1.Y);
            path.AddLine(ULX1, ULX2);

            var MUL1 = ULX2;
            var MUL2 = new Point(MUL1.X, ULX2.Y + 4);
            path.AddLine(MUL1, MUL2);

            var MULX1 = new Point(MUL2.X, MUL2.Y + 1);
            var MULX2 = new Point(MULX1.X - 5, MULX1.Y);
            path.AddLine(MULX1, MULX2);

            var LU1 = MULX2;
            var LU2 = new Point(MULX2.X, MULX2.Y - 3);
            path.AddLine(LU1, LU2);

            var LUX1 = new Point(LU2.X - 1, LU2.Y);
            var LUX2 = new Point(Rectangle.X, Rectangle.Y + 11);
            path.AddLine(LUX1, LUX2);

            var LDX1 = LUX2;
            var LDX2 = new Point(LDX1.X + 4, LDX1.Y + 4);
            path.AddLine(LDX1, LDX2);

            var LD1 = new Point(LDX2.X + 1, LDX2.Y);
            var LD2 = new Point(LD1.X, LD1.Y - 2);
            path.AddLine(LD1, LD2);

            var L1 = new Point(LD2.X, LD2.Y - 1);
            var L2 = new Point(L1.X + 5, L1.Y);
            path.AddLine(L1, L2);

            var DL1 = L2;
            var DL2 = new Point(L2.X, LD2.Y + 3);
            path.AddLine(DL1, DL2);

            var DX1 = new Point(DL2.X, DL2.Y + 1);
            var DX2 = new Point(DX1.X - 3, DX1.Y);
            path.AddLine(DX1, DX2);

            var DLX1 = new Point(DX2.X, DX2.Y + 1);
            var DLX2 = new Point(DLX1.X + 4, DLX1.Y + 4);
            path.AddLine(DLX1, DLX2);

            path.AddPath(MirrorRight(path), false);

            var m = new Matrix();
            m.Scale(Scale, Scale, MatrixOrder.Append);
            m.Translate(1f, 1f, MatrixOrder.Append);
            path.Transform(m);

            return path;
        }

        public static GraphicsPath Help(Rectangle Rectangle, ArrowStyle Style, float Scale = 1f)
        {
            var path = new GraphicsPath();
            Rectangle.Width = 7;
            Rectangle.Height = 11;
            Rectangle.X = 11;
            Rectangle.Y = 6;

            FontFamily F;

            switch (Style)
            {
                case ArrowStyle.Classic:
                    {
                        F = new FontFamily("Marlett");

                        path.AddString("s", F, (int)FontStyle.Bold, 15f, Rectangle, StringFormat.GenericDefault);
                        break;
                    }

                default:
                    {

                        if (OS.WXP)
                        {
                            F = new FontFamily("Tahoma");
                        }

                        else if (OS.W7 | OS.WVista)
                        {
                            F = new FontFamily("Segoe UI");
                        }
                        else
                        {
                            F = new FontFamily("Segoe UI Black");
                        }

                        path.AddString("?", F, (int)FontStyle.Bold, 15f, Rectangle, StringFormat.GenericDefault);
                        break;
                    }

            }

            var m = new Matrix();
            m.Scale(Scale, Scale, MatrixOrder.Append);
            m.Translate(1f, 1f, MatrixOrder.Append);
            path.Transform(m);

            return path;
        }

        public static GraphicsPath None(Rectangle Rectangle, float Scale = 1f)
        {
            var path = new GraphicsPath();
            Rectangle.Width = 17;
            Rectangle.Height = 17;
            Rectangle.X = 0;
            Rectangle.Y = 0;

            var R = new Rectangle(Rectangle.X + 2, Rectangle.Y + 2, Rectangle.Width - 4, Rectangle.Height - 4);

            path.AddArc(R, 50f, 160f);
            path.CloseFigure();

            path.AddArc(R, 230f, 160f);
            path.CloseFigure();

            path.AddEllipse(Rectangle);

            var m = new Matrix();
            m.Scale(Scale, Scale, MatrixOrder.Append);
            m.Translate(1f, 1f, MatrixOrder.Append);
            path.Transform(m);

            return path;
        }

        public static GraphicsPath NoneBackground(Rectangle Rectangle, float Scale = 1f)
        {
            var path = new GraphicsPath();
            Rectangle.Width = 17;
            Rectangle.Height = 17;
            Rectangle.X = 0;
            Rectangle.Y = 0;

            path.AddEllipse(Rectangle);

            var m = new Matrix();
            m.Scale(Scale, Scale, MatrixOrder.Append);
            m.Translate(1f, 1f, MatrixOrder.Append);
            path.Transform(m);

            return path;
        }

        public static GraphicsPath Up(Rectangle Rectangle, float Scale = 1f)
        {
            var path = new GraphicsPath();
            Rectangle.Width = 9;
            Rectangle.Height = 19;
            Rectangle.X = 0;
            Rectangle.Y = 0;

            var UL1 = new Point(Rectangle.X + 4, Rectangle.Y);
            var UL2 = new Point(Rectangle.X, Rectangle.Y + 4);
            path.AddLine(UL1, UL2);

            var ULX1 = new Point(UL2.X, UL2.Y + 1);
            var ULX2 = new Point(ULX1.X + 3, ULX1.Y);
            path.AddLine(ULX1, ULX2);

            var MUL1 = ULX2;
            var MUL2 = new Point(MUL1.X, MUL1.Y + 12);
            path.AddLine(MUL1, MUL2);

            var D1 = new Point(MUL2.X, MUL2.Y + 1);
            var D2 = new Point(D1.X + 1, D1.Y);
            path.AddLine(D1, D2);

            path.AddPath(MirrorRight(path), false);

            var m = new Matrix();
            m.Scale(Scale, Scale, MatrixOrder.Append);
            m.Translate(1f, 1f, MatrixOrder.Append);
            path.Transform(m);

            return path;
        }

        public static GraphicsPath NS(Rectangle Rectangle, float Scale = 1f)
        {
            var path = new GraphicsPath();
            Rectangle.Width = 9;
            Rectangle.Height = 23;
            Rectangle.X = 0;
            Rectangle.Y = 0;


            var UL1 = new Point(Rectangle.X + 4, Rectangle.Y);
            var UL2 = new Point(Rectangle.X, Rectangle.Y + 4);
            path.AddLine(UL1, UL2);

            var ULX1 = new Point(UL2.X, UL2.Y + 1);
            var ULX2 = new Point(ULX1.X + 3, ULX1.Y);
            path.AddLine(ULX1, ULX2);

            var MUL1 = ULX2;
            var MUL2 = new Point(MUL1.X, MUL1.Y + 12);
            path.AddLine(MUL1, MUL2);

            var DL1 = MUL2;
            var DL2 = new Point(MUL2.X - 3, MUL2.Y);
            path.AddLine(DL1, DL2);

            var DX1 = new Point(DL2.X, DL2.Y + 1);
            var DX2 = new Point(DX1.X + 4, DX1.Y + 4);
            path.AddLine(DX1, DX2);

            path.AddPath(MirrorRight(path), false);

            var m = new Matrix();
            m.Scale(Scale, Scale, MatrixOrder.Append);
            m.Translate(1f, 1f, MatrixOrder.Append);
            path.Transform(m);

            return path;
        }

        public static GraphicsPath NESW(Rectangle Rectangle, float Scale = 1f)
        {
            var path = new GraphicsPath();
            Rectangle.Width = 17;
            Rectangle.Height = 17;
            Rectangle.X = 0;
            Rectangle.Y = 0;

            var UR1 = new Point(Rectangle.X + Rectangle.Width - 1, Rectangle.Y);
            var UR2 = new Point(UR1.X - 6, UR1.Y);
            path.AddLine(UR1, UR2);

            var RX1 = new Point(UR2.X, UR2.Y + 1);
            var RX2 = new Point(RX1.X + 1, RX1.Y + 1);
            path.AddLine(RX1, RX2);

            var LX1 = new Point(RX2.X + 1, RX2.Y + 1);
            var LX2 = new Point(LX1.X - 9, LX1.Y + 9);
            path.AddLine(LX1, LX2);

            var DX1 = new Point(LX2.X - 1, LX2.Y - 1);
            var DX2 = new Point(DX1.X - 1, DX1.Y - 1);
            path.AddLine(DX1, DX2);

            var L1 = new Point(DX2.X - 1, DX2.Y);
            var L2 = new Point(L1.X, L1.Y + 6);
            path.AddLine(L1, L2);

            var D1 = new Point(L2.X + 1, L2.Y);
            var D2 = new Point(D1.X + 5, D1.Y);
            path.AddLine(D1, D2);

            var DL1 = new Point(D2.X, D2.Y - 1);
            var DL2 = new Point(DL1.X - 1, DL1.Y - 1);
            path.AddLine(DL1, DL2);

            var LX3 = new Point(DL2.X - 1, DL2.Y - 1);
            var LX4 = new Point(LX3.X + 9, LX3.Y - 9);
            path.AddLine(LX3, LX4);

            var DR1 = new Point(LX4.X + 1, LX4.Y + 1);
            var DR2 = new Point(DR1.X + 1, DR1.Y + 1);
            path.AddLine(DR1, DR2);

            var R1 = new Point(DR2.X + 1, DR2.Y);
            var R2 = new Point(R1.X, R1.Y - 6);
            path.AddLine(R1, R2);

            var m = new Matrix();
            m.Scale(Scale, Scale, MatrixOrder.Append);
            m.Translate(1f, 1f, MatrixOrder.Append);
            path.Transform(m);

            return path;
        }

        public static GraphicsPath NWSE(Rectangle Rectangle, float Scale = 1f)
        {
            var path = NESW(Rectangle);
            Rectangle.Width = 17;
            Rectangle.Height = 17;
            Rectangle.X = 0;
            Rectangle.Y = 0;

            var flipXMatrix = new Matrix(-1, 0f, 0f, 1f, Rectangle.Width, -1);
            var transformMatrix = new Matrix();
            transformMatrix.Multiply(flipXMatrix);
            path.Transform(transformMatrix);

            var m = new Matrix();
            m.Scale(Scale, Scale, MatrixOrder.Append);
            m.Translate(1f, 1f, MatrixOrder.Append);
            path.Transform(m);

            return path;
        }

        public static GraphicsPath EW(Rectangle Rectangle, float Scale = 1f)
        {
            var path = new GraphicsPath();
            Rectangle.Width = 23;
            Rectangle.Height = 9;
            Rectangle.X = 0;
            Rectangle.Y = 0;

            var L1 = new Point(Rectangle.X, Rectangle.Y + 4);
            var L2 = new Point(L1.X + 4, L1.Y - 4);
            path.AddLine(L1, L2);

            var LX1 = new Point(L2.X + 1, L2.Y);
            var LX2 = new Point(LX1.X, LX1.Y + 2);
            path.AddLine(LX1, LX2);

            var U1 = new Point(LX2.X, LX2.Y + 1);
            var U2 = new Point(U1.X + 12, U1.Y);
            path.AddLine(U1, U2);

            var RX1 = new Point(U2.X, U2.Y - 1);
            var RX2 = new Point(RX1.X, RX1.Y - 2);
            path.AddLine(RX1, RX2);

            var R1 = new Point(RX2.X + 1, RX2.Y);
            var R2 = new Point(R1.X + 4, R1.Y + 4);
            path.AddLine(R1, R2);

            var R3 = new Point(R2.X, R2.Y);
            var R4 = new Point(R3.X - 4, R3.Y + 4);
            path.AddLine(R3, R4);

            var RX3 = new Point(R4.X - 1, R4.Y);
            var RX4 = new Point(RX3.X, RX3.Y - 2);
            path.AddLine(RX3, RX4);

            var D1 = new Point(RX4.X, RX4.Y - 1);
            var D2 = new Point(D1.X - 12, D1.Y);
            path.AddLine(D1, D2);

            var LX3 = new Point(D2.X, D2.Y + 1);
            var LX4 = new Point(LX3.X, LX3.Y + 2);
            path.AddLine(LX3, LX4);

            var L3 = new Point(LX4.X - 1, LX4.Y);
            var L4 = new Point(L3.X - 4, L3.Y - 4);
            path.AddLine(L3, L4);

            var m = new Matrix();
            m.Scale(Scale, Scale, MatrixOrder.Append);
            m.Translate(1f, 1f, MatrixOrder.Append);
            path.Transform(m);

            return path;
        }

        public static GraphicsPath Pen(Rectangle Rectangle, float Scale = 1f)
        {
            var path = new GraphicsPath();
            Rectangle.Width = 22;
            Rectangle.Height = 22;
            Rectangle.X = 0;
            Rectangle.Y = 0;

            var T1 = new Point(Rectangle.X, Rectangle.Y);
            var T2 = T1 + (Size)new Point(6, 2);
            path.AddLine(T1, T2);

            var R1 = new Point(T2.X, T2.Y);
            var R2 = new Point(R1.X + 15, R1.Y + 15);
            path.AddLine(R1, R2);

            var B1 = new Point(R2.X, R2.Y + 1);
            var B2 = new Point(B1.X - 3, B1.Y + 3);
            path.AddLine(B1, B2);

            var L1 = new Point(B2.X - 1, B2.Y);
            var L2 = new Point(L1.X - 15, L1.Y - 15);
            path.AddLine(L1, L2);

            var LX1 = new Point(L2.X, L2.Y);
            path.AddLine(LX1, T1);

            path.CloseFigure();

            var S1 = new Point(Rectangle.X + 14, Rectangle.Y + 18);
            var S2 = new Point(S1.X + 4, S1.Y - 4);
            path.AddLine(S2, S1);

            var m = new Matrix();
            m.Scale(Scale, Scale, MatrixOrder.Append);
            m.Translate(1f, 1f, MatrixOrder.Append);
            path.Transform(m);

            return path;
        }

        public static GraphicsPath PenBackground(Rectangle Rectangle, float Scale = 1f)
        {
            var path = new GraphicsPath();
            Rectangle.Width = 22;
            Rectangle.Height = 22;
            Rectangle.X = 0;
            Rectangle.Y = 0;

            var T1 = new Point(Rectangle.X, Rectangle.Y);
            var T2 = T1 + (Size)new Point(6, 2);
            path.AddLine(T1, T2);

            var R1 = new Point(T2.X, T2.Y);
            var R2 = new Point(R1.X + 15, R1.Y + 15);
            path.AddLine(R1, R2);

            var B1 = new Point(R2.X, R2.Y + 1);
            var B2 = new Point(B1.X - 3, B1.Y + 3);
            path.AddLine(B1, B2);

            var L1 = new Point(B2.X - 1, B2.Y);
            var L2 = new Point(L1.X - 15, L1.Y - 15);
            path.AddLine(L1, L2);

            var LX1 = new Point(L2.X, L2.Y);
            path.AddLine(LX1, T1);

            path.CloseFigure();


            var m = new Matrix();
            m.Scale(Scale, Scale, MatrixOrder.Append);
            m.Translate(1f, 1f, MatrixOrder.Append);
            path.Transform(m);

            return path;
        }

        public static GraphicsPath Hand(Rectangle Rectangle, ArrowStyle Style, float Scale = 1f)
        {
            var path = new GraphicsPath();
            Rectangle.Width = 18;
            Rectangle.Height = 24;
            Rectangle.X = 0;
            Rectangle.Y = 0;

            switch (Style)
            {
                case ArrowStyle.Classic:
                    {
                        var Index_LB1 = new Point(Rectangle.X + 5, Rectangle.Y + 14);
                        var Index_LB2 = new Point(Index_LB1.X, Index_LB1.Y - 11);
                        path.AddLine(Index_LB1, Index_LB2);

                        var Index_RB1 = new Point(Index_LB1.X + 3, Index_LB1.Y - 4);
                        var Index_RB2 = new Point(Index_RB1.X, Index_RB1.Y - 8);
                        path.AddArc(Index_LB2.X, Index_LB2.Y - 2, 3, 2, 180f, 180f);

                        path.AddLine(Index_RB1, Index_RB2);
                        path.AddArc(Index_RB2.X, Index_LB2.Y + 3, 3, 2, 180f, 180f);

                        var Middle_RB1 = new Point(Index_RB1.X + 3, Index_RB1.Y);
                        var Middle_RB2 = new Point(Middle_RB1.X, Middle_RB1.Y - 3);
                        path.AddLine(Middle_RB1, Middle_RB2);
                        path.AddArc(Middle_RB2.X, Index_LB2.Y + 4, 3, 2, 180f, 180f);

                        var Ring_RB1 = new Point(Middle_RB1.X + 3, Middle_RB1.Y + 1);
                        var Ring_RB2 = new Point(Ring_RB1.X, Ring_RB1.Y - 2);
                        path.AddLine(Ring_RB1, Ring_RB2);
                        path.AddArc(Ring_RB2.X, Index_LB2.Y + 6, 3, 2, 180f, 180f);

                        var FreeBorder1 = new Point(Ring_RB1.X + 3, Ring_RB1.Y - 1);
                        var FreeBorder2 = new Point(FreeBorder1.X, FreeBorder1.Y + 6);
                        path.AddLine(FreeBorder1, FreeBorder2);

                        var LW1 = FreeBorder2 + (Size)new Point(0, 1);
                        var RW1 = new Point(LW1.X - 14, LW1.Y);
                        var Btm = new Rectangle(RW1.X + 3, RW1.Y - 8, 9, 13);
                        path.AddLine(FreeBorder2, new Point(Btm.X + Btm.Width, Btm.Y + Btm.Height));
                        path.AddLine(new Point(Btm.X + Btm.Width, Btm.Y + Btm.Height), new Point(Btm.X, Btm.Y + Btm.Height));

                        var L1 = RW1 - (Size)new Point(0, 1);
                        var L2 = new Point(L1.X - 1, L1.Y - 3);
                        var Thumb = new Rectangle(L2.X - 1, L2.Y - 3, 2, 3);
                        path.AddArc(Thumb, 90f, 180f);

                        var LastBorder1 = new Point(Thumb.X + Thumb.Width, Thumb.Y);
                        var LastBorder2 = new Point(LastBorder1.X + 2, LastBorder1.Y + 1);
                        path.AddLine(LastBorder1, LastBorder2);

                        path.CloseFigure();
                        break;
                    }

                default:
                    {
                        var Index_LB1 = new Point(Rectangle.X + 5, Rectangle.Y + 14);
                        var Index_LB2 = new Point(Index_LB1.X, Index_LB1.Y - 12);
                        path.AddLine(Index_LB1, Index_LB2);

                        var Index_RB1 = new Point(Index_LB1.X + 3, Index_LB1.Y - 4);
                        var Index_RB2 = new Point(Index_RB1.X, Index_RB1.Y - 8);

                        path.AddArc(Index_LB2.X, Index_LB2.Y - 2, 3, 2, 180f, 180f);

                        path.AddLine(Index_RB1, Index_RB2);

                        path.AddArc(Index_RB2.X, Index_LB2.Y + 3, 3, 2, 180f, 180f);

                        var Middle_RB1 = new Point(Index_RB1.X + 3, Index_RB1.Y);
                        var Middle_RB2 = new Point(Middle_RB1.X, Middle_RB1.Y - 3);
                        path.AddLine(Middle_RB1, Middle_RB2);

                        path.AddArc(Middle_RB2.X, Index_LB2.Y + 4, 3, 2, 180f, 180f);

                        var Ring_RB1 = new Point(Middle_RB1.X + 3, Middle_RB1.Y);
                        var Ring_RB2 = new Point(Ring_RB1.X, Ring_RB1.Y - 2);
                        path.AddLine(Ring_RB1, Ring_RB2);

                        path.AddArc(Ring_RB2.X, Index_LB2.Y + 5, 3, 2, 180f, 180f);

                        var FreeBorder1 = new Point(Ring_RB1.X + 3, Ring_RB1.Y - 1);
                        var FreeBorder2 = new Point(FreeBorder1.X, FreeBorder1.Y + 8);
                        path.AddLine(FreeBorder1, FreeBorder2);

                        var LW1 = FreeBorder2 + (Size)new Point(0, 1);
                        var RW1 = new Point(LW1.X - 14, LW1.Y);
                        var Btm = new Rectangle(RW1.X, RW1.Y - 8, 14, 13);
                        path.AddArc(Btm, 0f, 180f);

                        var L1 = RW1 - (Size)new Point(0, 1);
                        var L2 = new Point(L1.X - 2, L1.Y - 2);
                        var Thumb = new Rectangle(L2.X - 1, L2.Y - 3, 2, 3);
                        path.AddArc(Thumb, 90f, 180f);
                        // path.AddRectangle(Thumb)

                        var LastBorder1 = new Point(Thumb.X + Thumb.Width, Thumb.Y);
                        var LastBorder2 = new Point(LastBorder1.X + 2, LastBorder1.Y + 1);
                        path.AddLine(LastBorder1, LastBorder2);

                        path.CloseFigure();
                        break;
                    }
            }

            var m = new Matrix();
            m.Scale(Scale, Scale, MatrixOrder.Append);
            m.Translate(1f, 1f, MatrixOrder.Append);
            path.Transform(m);

            return path;
        }

        public static GraphicsPath Pin(Rectangle Rectangle, float Scale = 1f)
        {
            var path = new GraphicsPath();
            Rectangle.Width = 13;
            Rectangle.Height = 20;
            Rectangle.X = 15;
            Rectangle.Y = 11;

            var U = new Rectangle(Rectangle.X, Rectangle.Y, 12, 10);
            path.AddArc(U, 180f, 180f);

            var C = new Point(Rectangle.X + 6, Rectangle.Y + 18);
            var p1 = new Point(Rectangle.X + 0, Rectangle.Y + 6);
            var p2 = new Point(Rectangle.X + 12, Rectangle.Y + 6);
            path.AddLine(p2, C);
            path.AddLine(C, p1);
            path.CloseFigure();

            var m = new Matrix();
            m.Scale(Scale, Scale, MatrixOrder.Append);
            m.Translate(1f, 1f, MatrixOrder.Append);
            path.Transform(m);

            return path;
        }

        public static GraphicsPath Person(Rectangle Rectangle, float Scale = 1f)
        {
            var path = new GraphicsPath();
            Rectangle.Width = 10;
            Rectangle.Height = 13;
            Rectangle.X = 19;
            Rectangle.Y = 17;

            var Face = new Rectangle(Rectangle.X, Rectangle.Y, 5, 6);
            path.AddEllipse(Face);

            var TrunkUpper = new Rectangle(Face.X - 2, Face.Y + Face.Height, 9, 9);
            path.AddArc(TrunkUpper, 180f, 180f);

            var TrunkLower = new Rectangle(TrunkUpper.X, TrunkUpper.Y + 3, 9, 3);
            path.AddArc(TrunkLower, 0f, 180f);

            var m = new Matrix();
            m.Scale(Scale, Scale, MatrixOrder.Append);
            m.Translate(1f, 1f, MatrixOrder.Append);
            path.Transform(m);

            return path;
        }

        public static GraphicsPath IBeam(Rectangle Rectangle, float Scale = 1f)
        {
            var path = new GraphicsPath();

            Rectangle.X = 0;
            Rectangle.Y = 0;

            var L1 = new Point(Rectangle.X, Rectangle.Y);
            var L2 = new Point(L1.X, L1.Y + 2);
            path.AddLine(L1, L2);

            var BU1 = new Point(L2.X + 3, L2.Y);
            path.AddLine(L2, BU1);

            var LX = new Point(BU1.X, BU1.Y + 13);
            path.AddLine(BU1, LX);

            var BU2 = new Point(LX.X - 3, LX.Y);
            path.AddLine(LX, BU2);

            var L3 = new Point(BU2.X, BU2.Y + 2);
            path.AddLine(BU2, L3);

            var Bl = new Point(L3.X + 3, L3.Y);
            path.AddLine(L3, Bl);

            var XB = new Point(Bl.X + 1, Bl.Y - 1);
            path.AddLine(Bl, XB);

            var Br = new Point(XB.X + 1, XB.Y + 1);
            path.AddLine(XB, Br);

            var RB = new Point(Br.X + 3, Br.Y);
            path.AddLine(Br, RB);

            var R1 = new Point(RB.X, RB.Y - 2);
            path.AddLine(RB, R1);

            var BU3 = new Point(R1.X - 3, R1.Y);
            path.AddLine(R1, BU3);

            var RX = new Point(BU3.X, BU3.Y - 13);
            path.AddLine(BU3, RX);

            var TU = new Point(RX.X + 3, RX.Y);
            path.AddLine(RX, TU);

            var RR = new Point(TU.X, TU.Y - 2);
            path.AddLine(TU, RR);

            var T = new Point(RR.X - 3, RR.Y);
            path.AddLine(RR, T);

            var Tx = new Point(T.X - 1, T.Y + 1);
            path.AddLine(T, Tx);

            var TXL = new Point(Tx.X - 1, Tx.Y - 1);
            path.AddLine(Tx, TXL);

            var TL = new Point(TXL.X - 3, TXL.Y);
            path.AddLine(TXL, TL);


            var m = new Matrix();
            m.Scale(Scale, Scale, MatrixOrder.Append);
            m.Translate(1f, 1f, MatrixOrder.Append);
            path.Transform(m);

            return path;
        }

        public static GraphicsPath Cross(Rectangle Rectangle, float Scale = 1f)
        {
            var path = new GraphicsPath();
            Rectangle.Width = 19;
            Rectangle.Height = 19;

            var L1 = new Point(9, 0);
            var L2 = new Point(L1.X - 1, L1.Y);
            path.AddLine(L1, L2);

            var L3 = new Point(L2.X, L2.Y + 8);
            path.AddLine(L2, L3);

            var L4 = new Point(L3.X - 8, L3.Y);
            path.AddLine(L3, L4);

            var L5 = new Point(L4.X, L4.Y + 2);
            path.AddLine(L4, L5);

            var L6 = new Point(L5.X + 8, L5.Y);
            path.AddLine(L5, L6);

            var L7 = new Point(L6.X, L6.Y + 8);
            path.AddLine(L6, L7);

            var L8 = new Point(L7.X + 1, L7.Y);
            path.AddLine(L7, L8);

            path.AddPath(MirrorRight(path), false);

            var m = new Matrix();
            m.Scale(Scale, Scale, MatrixOrder.Append);
            m.Translate(1f, 1f, MatrixOrder.Append);
            path.Transform(m);

            return path;
        }

        public static GraphicsPath Pin_CenterPoint(Rectangle Rectangle, float Scale = 1f)
        {
            var path = new GraphicsPath();
            Rectangle.Width = 13;
            Rectangle.Height = 20;
            Rectangle.X = 15;
            Rectangle.Y = 11;

            var o = new Rectangle(Rectangle.X, Rectangle.Y, 12, 12);
            var o1 = new Rectangle(Rectangle.X, Rectangle.Y, 6, 6);
            o1.X = (int)Math.Round(Rectangle.X + (o.Width - o1.Width) / 2d);
            o1.Y = (int)Math.Round(Rectangle.Y + (o.Height - o1.Height) / 2d);
            path.AddEllipse(o1);

            var m = new Matrix();
            m.Scale(Scale, Scale, MatrixOrder.Append);
            m.Translate(1f, 1f, MatrixOrder.Append);
            path.Transform(m);

            return path;
        }

        private static GraphicsPath MirrorRight(GraphicsPath path)
        {
            var r = path.GetBounds();
            GraphicsPath p = (GraphicsPath)path.Clone();
            p.Transform(new Matrix(-1, 0f, 0f, 1f, 2f * (r.Left + r.Width), 0f));
            return p;
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
        public float _Angle = 180f;
        public bool Shadow_Enabled = false;
        public Color Shadow_Color = Color.Black;
        public int Shadow_Blur = 5;
        public float Shadow_Opacity = 0.3f;
        public int Shadow_OffsetX = 2;
        public int Shadow_OffsetY = 2;
    }
}