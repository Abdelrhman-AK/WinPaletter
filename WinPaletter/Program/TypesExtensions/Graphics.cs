using ImageProcessor;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WinPaletter.TypesExtensions
{
    public static class GraphicsExtensions
    {
        public static void DrawGlowString(this Graphics G, int GlowSize, string Text, Font Font, Color ForeColor, Color GlowColor, Rectangle ClientRect, Rectangle Rect, StringFormat FormatX)
        {
            int w = (int)Math.Round(Math.Max(8d, ClientRect.Width / 5d));
            int h = (int)Math.Round(Math.Max(8d, ClientRect.Height / 5d));
            float emSize = G.DpiY * Font.SizeInPoints / 72f;
            if (Text is null | string.IsNullOrWhiteSpace(Text))
                Text = string.Empty;

            using (Bitmap b = new(w, h))
            using (GraphicsPath gp = new())
            using (Graphics gx = Graphics.FromImage(b))
            using (Matrix m = new(1.0f / 5f, 0f, 0f, 1.0f / 5f, -(1.0f / 5f), -(1.0f / 5f)))
            using (SolidBrush br = new(ForeColor))
            using (Pen P = new(GlowColor, GlowSize))
            {
                gp.AddString(Text, Font.FontFamily, (int)Font.Style, emSize, Rect, FormatX);

                gx.SmoothingMode = SmoothingMode.AntiAlias;
                gx.Transform = m;

                gx.DrawPath(P, gp);
                gx.FillPath(P.Brush, gp);

                G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                G.DrawImage(b, ClientRect, 0, 0, b.Width, b.Height, GraphicsUnit.Pixel);

                G.SmoothingMode = SmoothingMode.AntiAlias;
                G.DrawString(Text, Font, br, Rect, FormatX);
            }
        }

        public static void DrawGlow(this Graphics graphics, Rectangle rectangle, Color glowColor, int glowSize = 5, int glowFade = 7, bool rounded = false)
        {
            try
            {
                if (glowSize <= 0) glowSize = 1;
                if (glowFade <= 0) glowFade = 1;

                Rectangle glowRect = rounded ? GetRoundedRectangle(rectangle, glowSize) : new(rectangle.X - glowSize - 2, rectangle.Y - glowSize - 2, rectangle.Width + glowSize * 2 + 3, rectangle.Height + glowSize * 2 + 3);

                using (Bitmap glowBitmap = new Bitmap((int)Math.Round(glowRect.Width / (double)glowFade), (int)Math.Round(glowRect.Height / (double)glowFade)))
                using (Graphics glowGraphics = Graphics.FromImage(glowBitmap))
                {
                    Rectangle glowRect2 = new Rectangle(1, 1, glowBitmap.Width, glowBitmap.Height);

                    using (SolidBrush glowBrush = new SolidBrush(glowColor))
                    {
                        glowGraphics.FillRectangle(glowBrush, glowRect2);
                    }

                    graphics.DrawImage(glowBitmap, glowRect);
                }
            }
            catch (Exception) { }
        }

        private static Rectangle GetRoundedRectangle(Rectangle rectangle, int radius)
        {
            int diameter = 2 * radius;
            Rectangle roundedRect = new(rectangle.X - radius, rectangle.Y - radius, rectangle.Width + diameter, rectangle.Height + diameter);
            return roundedRect;
        }

        public static void DrawAeroEffect(this Graphics G, Rectangle Rect, Bitmap BackgroundBlurred, Color Color1, decimal ColorBalance, Color Color2, decimal GlowBalance, decimal alpha, int Radius, bool RoundedCorners)
        {
            Rectangle _rect = new(Rect.X, Rect.Y, Rect.Width - 1, Rect.Height - 1);

            if (RoundedCorners)
            {
                if (BackgroundBlurred is not null) G.DrawRoundImage(BackgroundBlurred, Rect, Radius, true);
                using (SolidBrush br = new(Color.FromArgb((int)(alpha * 255), 0, 0, 0))) { G.FillRoundedRect(br, _rect, Radius, true); }
                using (SolidBrush br = new(Color.FromArgb((int)(alpha * ColorBalance * 255), Color1))) { G.FillRoundedRect(br, _rect, Radius, true); }
            }
            else
            {
                if (BackgroundBlurred is not null) G.DrawImage(BackgroundBlurred, Rect);
                using (SolidBrush br = new(Color.FromArgb((int)(alpha * 255), 0, 0, 0))) { G.FillRectangle(br, _rect); }
                using (SolidBrush br = new(Color.FromArgb((int)(alpha * ColorBalance * 255), Color1))) { G.FillRectangle(br, _rect); }
            }

            if (BackgroundBlurred != null)
            {
                Bitmap glowImage;
                Bitmap colorImage;
                Bitmap whiteImage;

                using (ImageFactory imgF = new())
                {
                    imgF.Load(BackgroundBlurred);
                    imgF.Filter(ImageProcessor.Imaging.Filters.Photo.MatrixFilters.GreyScale);
                    imgF.Brightness(20);
                    imgF.Tint(Color1.Blend(Color2, (double)ColorBalance));
                    glowImage = imgF.Image.Clone() as Bitmap;
                }

                using (ImageFactory imgF = new())
                {
                    imgF.Load(BackgroundBlurred);
                    imgF.Brightness(40);
                    imgF.Tint(Color1);
                    imgF.Alpha(50);
                    colorImage = imgF.Image.Clone() as Bitmap;
                }

                using (ImageFactory imgF = new())
                {
                    imgF.Load(BackgroundBlurred);
                    imgF.Brightness(55);
                    imgF.Tint(Color.White);
                    imgF.Alpha(40);
                    whiteImage = imgF.Image.Clone() as Bitmap;
                }

                if (RoundedCorners)
                {
                    G.DrawRoundImage(glowImage.Fade((float)GlowBalance), _rect, Radius, true);
                    G.DrawRoundImage(colorImage.Fade((float)(ColorBalance * GlowBalance)), _rect, Radius, true);
                    G.DrawRoundImage(whiteImage.Fade((1f - (float)alpha) * (float)GlowBalance), _rect, Radius, true);
                }
                else
                {
                    G.DrawImage(glowImage.Fade((float)GlowBalance), _rect);
                    G.DrawImage(colorImage.Fade((float)(ColorBalance * GlowBalance)), _rect);
                    G.DrawImage(whiteImage.Fade((1f - (float)alpha) * (float)GlowBalance), _rect);
                }

                glowImage.Dispose();
                colorImage.Dispose();
                whiteImage.Dispose();
            }
        }

        public static void FillRoundedRect(this Graphics Graphics, Brush Brush, Rectangle Rectangle, int Radius = -1, bool ForcedRoundCorner = false)
        {
            try
            {
                if (Radius == -1)
                    Radius = Program.Style.Radius;

                if (Graphics is null)
                    return;

                Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                if ((Program.Style.RoundedCorners || ForcedRoundCorner) && Radius > 0)
                {
                    using (GraphicsPath path = Rectangle.Round(Radius))
                    {
                        Graphics.FillPath(Brush, path);
                    }
                }
                else
                {
                    Graphics.FillRectangle(Brush, Rectangle);
                }
            }
            catch { }
        }

        public static void DrawRoundImage(this Graphics Graphics, Image Image, Rectangle Rectangle, int Radius = -1, bool ForcedRoundCorner = false)
        {
            try
            {
                if (Radius == -1)
                    Radius = Program.Style.Radius;

                if (Graphics is null)
                    throw new ArgumentNullException("graphics");

                if ((Program.Style.RoundedCorners | ForcedRoundCorner) & Radius > 0)
                {
                    using (GraphicsPath path = Rectangle.Round(Radius))
                    {
                        Region reg = new(path);
                        Graphics.Clip = reg;
                        Graphics.DrawImage(Image, Rectangle);
                        Graphics.ResetClip();
                    }
                }
                else
                {
                    Graphics.DrawImage(Image, Rectangle);
                }
            }
            catch
            {
            }
        }

        public static GraphicsPath Round(this Rectangle r, int Radius = -1)
        {
            try
            {
                Radius = Radius == -1 ? Program.Style.Radius * 2 : Radius *= 2;

                GraphicsPath path = new();

                path.AddLine(r.Left + Radius, r.Top, r.Right - Radius, r.Top);
                path.AddArc(Rectangle.FromLTRB(r.Right - Radius, r.Top, r.Right, r.Top + Radius), -90, 90f);

                path.AddLine(r.Right, r.Top + Radius, r.Right, r.Bottom - Radius);
                path.AddArc(Rectangle.FromLTRB(r.Right - Radius, r.Bottom - Radius, r.Right, r.Bottom), 0f, 90f);

                path.AddLine(r.Right - Radius, r.Bottom, r.Left + Radius, r.Bottom);
                path.AddArc(Rectangle.FromLTRB(r.Left, r.Bottom - Radius, r.Left + Radius, r.Bottom), 90f, 90f);

                path.AddLine(r.Left, r.Bottom - Radius, r.Left, r.Top + Radius);
                path.AddArc(Rectangle.FromLTRB(r.Left, r.Top, r.Left + Radius, r.Top + Radius), 180f, 90f);

                path.CloseFigure();
                return path;
            }
            catch
            {
                return null;
            }
        }

        public static void DrawRoundedRect(this Graphics Graphics, Pen Pen, Rectangle Rectangle, int Radius = -1, bool ForcedRoundCorner = false)
        {
            try
            {
                if (Radius == -1)
                    Radius = Program.Style.Radius;

                Radius *= 2;

                Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                if ((Program.Style.RoundedCorners | ForcedRoundCorner) & Radius > 0)
                {
                    Graphics.DrawArc(Pen, Rectangle.X, Rectangle.Y, Radius, Radius, 180, 90);
                    Graphics.DrawLine(Pen, (int)Math.Round(Rectangle.X + Radius / 2d), Rectangle.Y, (int)Math.Round(Rectangle.X + Rectangle.Width - Radius / 2d), Rectangle.Y);
                    Graphics.DrawArc(Pen, Rectangle.X + Rectangle.Width - Radius, Rectangle.Y, Radius, Radius, 270, 90);
                    Graphics.DrawLine(Pen, Rectangle.X, (int)Math.Round(Rectangle.Y + Radius / 2d), Rectangle.X, (int)Math.Round(Rectangle.Y + Rectangle.Height - Radius / 2d));
                    Graphics.DrawLine(Pen, Rectangle.X + Rectangle.Width, (int)Math.Round(Rectangle.Y + Radius / 2d), Rectangle.X + Rectangle.Width, (int)Math.Round(Rectangle.Y + Rectangle.Height - Radius / 2d));
                    Graphics.DrawLine(Pen, (int)Math.Round(Rectangle.X + Radius / 2d), Rectangle.Y + Rectangle.Height, (int)Math.Round(Rectangle.X + Rectangle.Width - Radius / 2d), Rectangle.Y + Rectangle.Height);
                    Graphics.DrawArc(Pen, Rectangle.X, Rectangle.Y + Rectangle.Height - Radius, Radius, Radius, 90, 90);
                    Graphics.DrawArc(Pen, Rectangle.X + Rectangle.Width - Radius, Rectangle.Y + Rectangle.Height - Radius, Radius, Radius, 0, 90);
                }
                else
                {
                    Graphics.DrawRectangle(Pen, Rectangle);
                }
            }
            catch
            {
            }
        }

        public static void DrawRoundedRect_LikeW11(this Graphics Graphics, Pen PenX, Rectangle Rectangle, int Radius = -1, bool ForcedRoundCorner = false)
        {
            try
            {
                bool Dark = Program.Style.DarkMode;

                if (Radius == -1)
                    Radius = Program.Style.Radius;

                Radius *= 2;

                bool Rounded = (Program.Style.RoundedCorners || ForcedRoundCorner) && Radius > 0;

                Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                using (Pen Pen = new(PenX.Color, PenX.Width) { DashStyle = PenX.DashStyle, DashOffset = PenX.DashOffset })
                using (Pen Pen2 = new(PenX.Color, PenX.Width) { DashStyle = PenX.DashStyle, DashOffset = PenX.DashOffset })
                {
                    Pen SidePen = new(PenX.Color, PenX.Width) { DashStyle = PenX.DashStyle, DashOffset = PenX.DashOffset };

                    if (Dark)
                    {
                        Pen.Color = PenX.Color.CB(0.06f);
                        Pen2.Color = PenX.Color;
                    }
                    else
                    {
                        Pen.Color = PenX.Color;
                        Pen2.Color = PenX.Color.CB(-0.12f);
                    }

                    LinearGradientBrush G;
                    Color CColor = Pen2.Color.CB((float)(Dark ? 0.03d : -0.05d));

                    if (Dark)
                    {
                        G = new(Rectangle, CColor, Pen.Color, 180f);
                        ColorBlend cblend = new(5)
                        {
                            Colors = new Color[5] { CColor, Pen.Color, Pen.Color, Pen.Color, CColor },
                            Positions = Rounded ?
                            new float[5] { 0f, 0.1f, 0.5f, 0.9f, 1.0f } :
                            new float[5] { 0f, 0.1f, 0.5f, 0.9f, 1.0f }
                        };
                        G.InterpolationColors = cblend;
                    }
                    else
                    {
                        G = new(Rectangle, Pen.Color, CColor, 180f);
                        ColorBlend cblend = new(5)
                        {
                            Colors = new Color[5] { Pen.Color, CColor, CColor, CColor, Pen.Color },
                            Positions = new float[5] { 0f, 0.1f, 0.5f, 0.9f, 1.0f }
                        };
                        G.InterpolationColors = cblend;
                    }

                    using (Pen PenG = new(G, PenX.Width) { DashStyle = PenX.DashStyle, DashOffset = PenX.DashOffset })
                    {

                        Point P_T1 = new((int)Math.Round(Rectangle.X + Radius / 2d), Rectangle.Y);
                        Point P_T2 = new((int)Math.Round(Rectangle.X + Rectangle.Width - Radius / 2d), Rectangle.Y);

                        Point P_B1 = new((int)Math.Round(Rectangle.X + Radius / 2d), Rectangle.Y + Rectangle.Height);
                        Point P_B2 = new((int)Math.Round(Rectangle.X + Rectangle.Width - Radius / 2d), Rectangle.Y + Rectangle.Height);

                        Point P_LB = new(Rectangle.X, Rectangle.Y + Rectangle.Height);
                        Point P_RB = new(Rectangle.X + Rectangle.Width, Rectangle.Y + Rectangle.Height);

                        Point P_LT = new(Rectangle.X, Rectangle.Y);
                        Point P_RT = new(Rectangle.X + Rectangle.Width, Rectangle.Y);

                        Point P_L1 = new(Rectangle.X, (int)Math.Round(Rectangle.Y + Radius / 2d));
                        Point P_L2 = new(Rectangle.X, (int)Math.Round(Rectangle.Y + Rectangle.Height - Radius / 2.5d));

                        Point P_R1 = new(Rectangle.X + Rectangle.Width, (int)Math.Round(Rectangle.Y + Radius / 2d));
                        Point P_R2 = new(Rectangle.X + Rectangle.Width, (int)Math.Round(Rectangle.Y + Rectangle.Height - Radius / 2.5d));

                        if (Dark)
                        {
                            Graphics.DrawLine(Pen2, P_B1, P_B2);

                            if (Rounded)
                            {
                                Graphics.DrawArc(Pen2, Rectangle.X, Rectangle.Y + Rectangle.Height - Radius, Radius, Radius, 90, 90);
                                Graphics.DrawArc(Pen2, Rectangle.X + Rectangle.Width - Radius, Rectangle.Y + Rectangle.Height - Radius, Radius, Radius, 0, 90);
                            }
                            else
                            {
                                Graphics.DrawLine(Pen2, P_B1, P_LB);
                                Graphics.DrawLine(Pen2, P_LB, P_L1);

                                Graphics.DrawLine(Pen2, P_B2, P_RB);
                                Graphics.DrawLine(Pen2, P_RB, P_R1);
                            }

                            SidePen = Pen2;

                            Graphics.DrawLine(SidePen, P_L1, P_L2);

                            Graphics.DrawLine(SidePen, P_R1, P_R2);

                            if (Rounded)
                            {
                                Graphics.DrawArc(PenG, Rectangle.X, Rectangle.Y, Radius, Radius, 180, 90);
                                Graphics.DrawArc(PenG, Rectangle.X + Rectangle.Width - Radius, Rectangle.Y, Radius, Radius, 270, 90);
                            }
                            else
                            {
                                Graphics.DrawLine(Pen, P_T1, P_LT);
                                Graphics.DrawLine(SidePen, P_LT, P_L1);

                                Graphics.DrawLine(Pen, P_T2, P_RT);
                                Graphics.DrawLine(SidePen, P_RT, P_R1);
                            }

                            Graphics.DrawLine(PenG, P_T1, P_T2);
                        }

                        else
                        {
                            Graphics.DrawLine(Pen2, P_B1, P_B2);

                            if (Rounded)
                            {
                                Graphics.DrawArc(PenG, Rectangle.X, Rectangle.Y + Rectangle.Height - Radius, Radius, Radius, 90, 90);
                                Graphics.DrawArc(PenG, Rectangle.X + Rectangle.Width - Radius, Rectangle.Y + Rectangle.Height - Radius, Radius, Radius, 0, 90);
                            }
                            else
                            {
                                Graphics.DrawLine(Pen2, P_B1, P_LB);
                                Graphics.DrawLine(SidePen, P_LB, P_L1);

                                Graphics.DrawLine(Pen2, P_B2, P_RB);
                                Graphics.DrawLine(SidePen, P_RB, P_R1);
                            }

                            SidePen = Pen;

                            Graphics.DrawLine(SidePen, P_L1, P_L2);

                            Graphics.DrawLine(SidePen, P_R1, P_R2);

                            if (Rounded)
                            {
                                Graphics.DrawArc(Pen, Rectangle.X, Rectangle.Y, Radius, Radius, 180, 90);
                                Graphics.DrawArc(Pen, Rectangle.X + Rectangle.Width - Radius, Rectangle.Y, Radius, Radius, 270, 90);
                            }
                            else
                            {
                                Graphics.DrawLine(Pen, P_T1, P_LT);
                                Graphics.DrawLine(Pen, P_LT, P_L1);

                                Graphics.DrawLine(Pen, P_T2, P_RT);
                                Graphics.DrawLine(Pen, P_RT, P_R1);
                            }

                            Graphics.DrawLine(Pen, P_T1, P_T2);
                        }
                    }

                    SidePen.Dispose();
                }
            }
            catch
            {
            }
        }

        public static bool Contains(this PointF[] pointFs, PointF point)
        {
            // Check if the x-coordinate is between the x-coordinates of the bounding points
            bool isXBetween = point.X >= Math.Min(pointFs[0].X, pointFs[1].X) && point.X <= Math.Max(pointFs[0].X, pointFs[1].X);

            // Check if the y-coordinate is between the y-coordinates of the bounding points
            bool isYBetween = point.Y >= Math.Min(pointFs[0].Y, pointFs[1].Y) && point.Y <= Math.Max(pointFs[0].Y, pointFs[1].Y);

            // The point is between the bounding points if both x and y coordinates are between the corresponding coordinates of the bounding points
            return isXBetween && isYBetween;
        }

        public static bool Contains(this Point[] points, PointF point)
        {
            if (points.Length < 2)
                return false;

            for (int i = 0; i < points.Length - 1; i++)
            {
                if (points[i].X <= point.X && points[i + 1].X >= point.X)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Check if a targetRect lies in a parent rectangle, not exceeding it. 
        /// </summary>
        /// <param name="parentRect"></param>
        /// <param name="targetRect"></param>
        /// <returns></returns>
        public static bool Contains_ButNotExceed(this Rectangle parentRect, Rectangle targetRect)
        {
            // Check if the clone bounds are within the original bitmap size
            return targetRect.X >= parentRect.X && targetRect.Y >= parentRect.Y && targetRect.Right <= parentRect.Width && targetRect.Bottom <= parentRect.Height;
        }

        public static bool BordersContains(this Rectangle rectangle, Point pointToCheck)
        {
            bool isOnBorderX = pointToCheck.X == rectangle.Left || pointToCheck.X == rectangle.Right;
            bool isOnBorderY = pointToCheck.Y == rectangle.Top || pointToCheck.Y == rectangle.Bottom;

            return (isOnBorderX && pointToCheck.Y >= rectangle.Top && pointToCheck.Y <= rectangle.Bottom) ||
                   (isOnBorderY && pointToCheck.X >= rectangle.Left && pointToCheck.X <= rectangle.Right);
        }
    }
}
