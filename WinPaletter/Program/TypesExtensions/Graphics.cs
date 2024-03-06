using ImageProcessor;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WinPaletter.TypesExtensions
{
    /// <summary>
    /// Extension methods for <see cref="Graphics"/> class
    /// </summary>
    public static class GraphicsExtensions
    {
        /// <summary>
        /// Draw a string with a glow effect (Looks like Aero label in titlebar)
        /// </summary>
        /// <param name="G"></param>
        /// <param name="glowSize"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="foreColor"></param>
        /// <param name="glowColor"></param>
        /// <param name="clientRectangle"></param>
        /// <param name="rectangle"></param>
        /// <param name="format"></param>
        public static void DrawGlowString(this Graphics G, int glowSize, string text, Font font, Color foreColor, Color glowColor, Rectangle clientRectangle, Rectangle rectangle, StringFormat format)
        {
            int w = (int)Math.Round(Math.Max(8d, clientRectangle.Width / 5d));
            int h = (int)Math.Round(Math.Max(8d, clientRectangle.Height / 5d));
            float emSize = G.DpiY * font.SizeInPoints / 72f;
            if (text is null | string.IsNullOrWhiteSpace(text)) text = string.Empty;

            using (Bitmap b = new(w, h))
            using (GraphicsPath gp = new())
            using (Graphics gx = Graphics.FromImage(b))
            using (Matrix m = new(1.0f / 5f, 0f, 0f, 1.0f / 5f, -(1.0f / 5f), -(1.0f / 5f)))
            using (SolidBrush br = new(foreColor))
            using (Pen P = new(glowColor, glowSize))
            {
                gp.AddString(text, font.FontFamily, (int)font.Style, emSize, rectangle, format);

                gx.Transform = m;

                gx.DrawPath(P, gp);
                gx.FillPath(P.Brush, gp);

                InterpolationMode oldInterpolationMode = G.InterpolationMode;

                G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                G.DrawImage(b, clientRectangle, 0, 0, b.Width, b.Height, GraphicsUnit.Pixel);

                G.DrawString(text, font, br, rectangle, format);

                G.InterpolationMode = oldInterpolationMode;
            }
        }

        /// <summary>
        /// Draw a glow effect around a rectangle
        /// </summary>
        /// <param name="G"></param>
        /// <param name="rectangle"></param>
        /// <param name="glowColor"></param>
        /// <param name="glowSize"></param>
        /// <param name="glowFade"></param>
        /// <param name="rounded"></param>
        public static void DrawGlow(this Graphics G, Rectangle rectangle, Color glowColor, int glowSize = 5, int glowFade = 7, bool rounded = false)
        {
            if (G is null) return;
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;

            if (glowSize <= 0) glowSize = 1;
            if (glowFade <= 0) glowFade = 1;

            Rectangle glowRect = rounded ? GetRoundedRectangle(rectangle, glowSize) : new(rectangle.X - glowSize - 2, rectangle.Y - glowSize - 2, rectangle.Width + glowSize * 2 + 3, rectangle.Height + glowSize * 2 + 3);

            using (Bitmap glowBitmap = new((int)Math.Round(glowRect.Width / (double)glowFade), (int)Math.Round(glowRect.Height / (double)glowFade)))
            using (Graphics glowGraphics = Graphics.FromImage(glowBitmap))
            {
                Rectangle glowRect2 = new(1, 1, glowBitmap.Width, glowBitmap.Height);

                using (SolidBrush glowBrush = new(glowColor)) { glowGraphics.FillRectangle(glowBrush, glowRect2); }

                G.DrawImage(glowBitmap, glowRect);
            }
        }

        private static Rectangle GetRoundedRectangle(Rectangle rectangle, int radius)
        {
            int diameter = 2 * radius;
            Rectangle roundedRect = new(rectangle.X - radius, rectangle.Y - radius, rectangle.Width + diameter, rectangle.Height + diameter);
            return roundedRect;
        }

        /// <summary>
        /// Draw DWMAero-like effect
        /// </summary>
        /// <param name="G"></param>
        /// <param name="rect"></param>
        /// <param name="backgroundBlurred"></param>
        /// <param name="color1"></param>
        /// <param name="colorBalance"></param>
        /// <param name="color2"></param>
        /// <param name="glowBalance"></param>
        /// <param name="alpha"></param>
        /// <param name="radius"></param>
        /// <param name="roundedCorners"></param>
        public static void DrawAeroEffect(this Graphics G, Rectangle rect, Bitmap backgroundBlurred, Color color1, decimal colorBalance, Color color2, decimal glowBalance, decimal alpha, int radius, bool roundedCorners)
        {
            if (G is null) return;
            if (rect.Width <= 0 || rect.Height <= 0) return;
            if (backgroundBlurred is null) return;

            Rectangle _rect = new(rect.X, rect.Y, rect.Width - 1, rect.Height - 1);

            if (roundedCorners)
            {
                if (backgroundBlurred is not null) G.DrawRoundImage(backgroundBlurred, rect, radius, true);
                using (SolidBrush br = new(Color.FromArgb((int)(alpha * 255), 0, 0, 0))) { G.FillRoundedRect(br, _rect, radius, true); }
                using (SolidBrush br = new(Color.FromArgb((int)(alpha * colorBalance * 255), color1))) { G.FillRoundedRect(br, _rect, radius, true); }
            }
            else
            {
                if (backgroundBlurred is not null) G.DrawImage(backgroundBlurred, rect);
                using (SolidBrush br = new(Color.FromArgb((int)(alpha * 255), 0, 0, 0))) { G.FillRectangle(br, _rect); }
                using (SolidBrush br = new(Color.FromArgb((int)(alpha * colorBalance * 255), color1))) { G.FillRectangle(br, _rect); }
            }

            if (backgroundBlurred != null)
            {
                Bitmap glowImage;
                Bitmap colorImage;
                Bitmap whiteImage;

                using (ImageFactory imgF = new())
                {
                    imgF.Load(backgroundBlurred);
                    imgF.Filter(ImageProcessor.Imaging.Filters.Photo.MatrixFilters.GreyScale);
                    imgF.Brightness(20);
                    imgF.Tint(color1.Blend(color2, (double)colorBalance));
                    glowImage = imgF.Image.Clone() as Bitmap;
                }

                using (ImageFactory imgF = new())
                {
                    imgF.Load(backgroundBlurred);
                    imgF.Brightness(40);
                    imgF.Tint(color1);
                    imgF.Alpha(50);
                    colorImage = imgF.Image.Clone() as Bitmap;
                }

                using (ImageFactory imgF = new())
                {
                    imgF.Load(backgroundBlurred);
                    imgF.Brightness(55);
                    imgF.Tint(Color.White);
                    imgF.Alpha(40);
                    whiteImage = imgF.Image.Clone() as Bitmap;
                }

                if (roundedCorners)
                {
                    G.DrawRoundImage(glowImage.Fade((float)glowBalance), _rect, radius, true);
                    G.DrawRoundImage(colorImage.Fade((float)(colorBalance * glowBalance)), _rect, radius, true);
                    G.DrawRoundImage(whiteImage.Fade((1f - (float)alpha) * (float)glowBalance), _rect, radius, true);
                }
                else
                {
                    G.DrawImage(glowImage.Fade((float)glowBalance), _rect);
                    G.DrawImage(colorImage.Fade((float)(colorBalance * glowBalance)), _rect);
                    G.DrawImage(whiteImage.Fade((1f - (float)alpha) * (float)glowBalance), _rect);
                }

                glowImage?.Dispose();
                colorImage?.Dispose();
                whiteImage?.Dispose();
            }
        }

        /// <summary>
        /// Fill a rounded rectangle
        /// </summary>
        /// <param name="G"></param>
        /// <param name="brush"></param>
        /// <param name="rectangle"></param>
        /// <param name="radius"></param>
        /// <param name="forcedRoundCorner"></param>
        public static void FillRoundedRect(this Graphics G, Brush brush, Rectangle rectangle, int radius = -1, bool forcedRoundCorner = false)
        {
            if (G is null) return;
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            if (brush is null) return;

            if (radius == -1) radius = Program.Style.Radius;

            if ((Program.Style.RoundedCorners || forcedRoundCorner) && radius > 0) { using (GraphicsPath path = rectangle.Round(radius)) { G.FillPath(brush, path); } }
            else { G.FillRectangle(brush, rectangle); }
        }

        /// <summary>
        /// Draw a rounded image
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="image"></param>
        /// <param name="rectangle"></param>
        /// <param name="radius"></param>
        /// <param name="forcedRoundCorner"></param>
        public static void DrawRoundImage(this Graphics graphics, Image image, Rectangle rectangle, int radius = -1, bool forcedRoundCorner = false)
        {
            if (graphics is null) return;
            if (image is null) return;
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;

            if (radius == -1) radius = Program.Style.Radius;

            if ((Program.Style.RoundedCorners | forcedRoundCorner) & radius > 0)
            {
                using (GraphicsPath path = rectangle.Round(radius))
                {
                    Region reg = new(path);
                    graphics.Clip = reg;
                    graphics.DrawImage(image, rectangle);
                    graphics.ResetClip();
                }
            }
            else
            {
                graphics.DrawImage(image, rectangle);
            }
        }

        /// <summary>
        /// Rounded <see cref="GraphicsPath"/>
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="Radius"></param>
        /// <returns></returns>
        public static GraphicsPath Round(this Rectangle rectangle, int Radius = -1)
        {
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return null;

            Radius = Radius == -1 ? Program.Style.Radius * 2 : Radius *= 2;

            GraphicsPath path = new();

            path.AddLine(rectangle.Left + Radius, rectangle.Top, rectangle.Right - Radius, rectangle.Top);
            path.AddArc(Rectangle.FromLTRB(rectangle.Right - Radius, rectangle.Top, rectangle.Right, rectangle.Top + Radius), -90, 90f);

            path.AddLine(rectangle.Right, rectangle.Top + Radius, rectangle.Right, rectangle.Bottom - Radius);
            path.AddArc(Rectangle.FromLTRB(rectangle.Right - Radius, rectangle.Bottom - Radius, rectangle.Right, rectangle.Bottom), 0f, 90f);

            path.AddLine(rectangle.Right - Radius, rectangle.Bottom, rectangle.Left + Radius, rectangle.Bottom);
            path.AddArc(Rectangle.FromLTRB(rectangle.Left, rectangle.Bottom - Radius, rectangle.Left + Radius, rectangle.Bottom), 90f, 90f);

            path.AddLine(rectangle.Left, rectangle.Bottom - Radius, rectangle.Left, rectangle.Top + Radius);
            path.AddArc(Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Left + Radius, rectangle.Top + Radius), 180f, 90f);

            path.CloseFigure();
            return path;
        }

        /// <summary>
        /// Draw a rounded rectangle
        /// </summary>
        /// <param name="G"></param>
        /// <param name="pen"></param>
        /// <param name="rectangle"></param>
        /// <param name="radius"></param>
        /// <param name="forcedRoundCorner"></param>
        public static void DrawRoundedRect(this Graphics G, Pen pen, Rectangle rectangle, int radius = -1, bool forcedRoundCorner = false)
        {
            if (G is null) return;
            if (pen is null) return;
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;

            if (radius == -1) radius = Program.Style.Radius;

            radius *= 2;

            if ((Program.Style.RoundedCorners | forcedRoundCorner) & radius > 0)
            {
                G.DrawArc(pen, rectangle.X, rectangle.Y, radius, radius, 180, 90);
                G.DrawLine(pen, (int)Math.Round(rectangle.X + radius / 2d), rectangle.Y, (int)Math.Round(rectangle.X + rectangle.Width - radius / 2d), rectangle.Y);
                G.DrawArc(pen, rectangle.X + rectangle.Width - radius, rectangle.Y, radius, radius, 270, 90);
                G.DrawLine(pen, rectangle.X, (int)Math.Round(rectangle.Y + radius / 2d), rectangle.X, (int)Math.Round(rectangle.Y + rectangle.Height - radius / 2d));
                G.DrawLine(pen, rectangle.X + rectangle.Width, (int)Math.Round(rectangle.Y + radius / 2d), rectangle.X + rectangle.Width, (int)Math.Round(rectangle.Y + rectangle.Height - radius / 2d));
                G.DrawLine(pen, (int)Math.Round(rectangle.X + radius / 2d), rectangle.Y + rectangle.Height, (int)Math.Round(rectangle.X + rectangle.Width - radius / 2d), rectangle.Y + rectangle.Height);
                G.DrawArc(pen, rectangle.X, rectangle.Y + rectangle.Height - radius, radius, radius, 90, 90);
                G.DrawArc(pen, rectangle.X + rectangle.Width - radius, rectangle.Y + rectangle.Height - radius, radius, radius, 0, 90);
            }
            else
            {
                G.DrawRectangle(pen, rectangle);
            }
        }

        /// <summary>
        /// Draw a rounded rectangle like Windows 11 buttons (top or bottom border has a different color tone)
        /// </summary>
        /// <param name="G"></param>
        /// <param name="pen"></param>
        /// <param name="rectangle"></param>
        /// <param name="radius"></param>
        /// <param name="forcedRoundCorner"></param>
        public static void DrawRoundedRect_LikeW11(this Graphics G, Pen pen, Rectangle rectangle, int radius = -1, bool forcedRoundCorner = false)
        {
            if (G is null) return;
            if (pen is null) return;
            if (pen.Brush is null) return;

            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            bool Dark = Program.Style.DarkMode;

            if (radius == -1) radius = Program.Style.Radius;
            radius *= 2;

            bool Rounded = (Program.Style.RoundedCorners || forcedRoundCorner) && radius > 0;

            Color penColor = pen.Brush is LinearGradientBrush lgb ? lgb.LinearColors[0] : pen.Color;

            Pen SidePen = new(penColor, pen.Width) { DashStyle = pen.DashStyle, DashOffset = pen.DashOffset };
            using (Pen Pen1 = new(penColor, pen.Width) { DashStyle = pen.DashStyle, DashOffset = pen.DashOffset })
            using (Pen Pen2 = new(penColor, pen.Width) { DashStyle = pen.DashStyle, DashOffset = pen.DashOffset })
            {
                if (Dark)
                {
                    Pen1.Color = penColor.CB(0.06f);
                    Pen2.Color = penColor;
                }
                else
                {
                    Pen1.Color = penColor;
                    Pen2.Color = penColor.CB(-0.12f);
                }

                LinearGradientBrush differentBorder;
                Color CColor = Pen2.Color.CB((float)(Dark ? 0.03d : -0.05d));

                if (Dark)
                {
                    differentBorder = new(rectangle, CColor, Pen1.Color, 180f);
                    ColorBlend cblend = new(5)
                    {
                        Colors = new Color[5] { CColor, Pen1.Color, Pen1.Color, Pen1.Color, CColor },
                        Positions = Rounded ?
                        new float[5] { 0f, 0.1f, 0.5f, 0.9f, 1.0f } :
                        new float[5] { 0f, 0.1f, 0.5f, 0.9f, 1.0f }
                    };
                    differentBorder.InterpolationColors = cblend;
                }
                else
                {
                    differentBorder = new(rectangle, Pen1.Color, CColor, 180f);
                    ColorBlend cblend = new(5)
                    {
                        Colors = new Color[5] { Pen1.Color, CColor, CColor, CColor, Pen1.Color },
                        Positions = new float[5] { 0f, 0.1f, 0.5f, 0.9f, 1.0f }
                    };
                    differentBorder.InterpolationColors = cblend;
                }

                using (Pen PenG = new(differentBorder, pen.Width) { DashStyle = pen.DashStyle, DashOffset = pen.DashOffset })
                {
                    Point P_T1 = new((int)Math.Round(rectangle.X + radius / 2d), rectangle.Y);
                    Point P_T2 = new((int)Math.Round(rectangle.X + rectangle.Width - radius / 2d), rectangle.Y);

                    Point P_B1 = new((int)Math.Round(rectangle.X + radius / 2d), rectangle.Y + rectangle.Height);
                    Point P_B2 = new((int)Math.Round(rectangle.X + rectangle.Width - radius / 2d), rectangle.Y + rectangle.Height);

                    Point P_LB = new(rectangle.X, rectangle.Y + rectangle.Height);
                    Point P_RB = new(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);

                    Point P_LT = new(rectangle.X, rectangle.Y);
                    Point P_RT = new(rectangle.X + rectangle.Width, rectangle.Y);

                    Point P_L1 = new(rectangle.X, (int)Math.Round(rectangle.Y + radius / 2d));
                    Point P_L2 = new(rectangle.X, (int)Math.Round(rectangle.Y + rectangle.Height - radius / 2.5d));

                    Point P_R1 = new(rectangle.X + rectangle.Width, (int)Math.Round(rectangle.Y + radius / 2d));
                    Point P_R2 = new(rectangle.X + rectangle.Width, (int)Math.Round(rectangle.Y + rectangle.Height - radius / 2.5d));

                    if (Dark)
                    {
                        G.DrawLine(Pen2, P_B1, P_B2);

                        if (Rounded)
                        {
                            G.DrawArc(Pen2, rectangle.X, rectangle.Y + rectangle.Height - radius, radius, radius, 90, 90);
                            G.DrawArc(Pen2, rectangle.X + rectangle.Width - radius, rectangle.Y + rectangle.Height - radius, radius, radius, 0, 90);
                        }
                        else
                        {
                            G.DrawLine(Pen2, P_B1, P_LB);
                            G.DrawLine(Pen2, P_LB, P_L1);

                            G.DrawLine(Pen2, P_B2, P_RB);
                            G.DrawLine(Pen2, P_RB, P_R1);
                        }

                        SidePen = Pen2;

                        G.DrawLine(SidePen, P_L1, P_L2);

                        G.DrawLine(SidePen, P_R1, P_R2);

                        if (Rounded)
                        {
                            G.DrawArc(PenG, rectangle.X, rectangle.Y, radius, radius, 180, 90);
                            G.DrawArc(PenG, rectangle.X + rectangle.Width - radius, rectangle.Y, radius, radius, 270, 90);
                        }
                        else
                        {
                            G.DrawLine(Pen1, P_T1, P_LT);
                            G.DrawLine(SidePen, P_LT, P_L1);

                            G.DrawLine(Pen1, P_T2, P_RT);
                            G.DrawLine(SidePen, P_RT, P_R1);
                        }

                        G.DrawLine(PenG, P_T1, P_T2);
                    }

                    else
                    {
                        G.DrawLine(Pen2, P_B1, P_B2);

                        if (Rounded)
                        {
                            G.DrawArc(PenG, rectangle.X, rectangle.Y + rectangle.Height - radius, radius, radius, 90, 90);
                            G.DrawArc(PenG, rectangle.X + rectangle.Width - radius, rectangle.Y + rectangle.Height - radius, radius, radius, 0, 90);
                        }
                        else
                        {
                            G.DrawLine(Pen2, P_B1, P_LB);
                            G.DrawLine(SidePen, P_LB, P_L1);

                            G.DrawLine(Pen2, P_B2, P_RB);
                            G.DrawLine(SidePen, P_RB, P_R1);
                        }

                        SidePen = Pen1;

                        G.DrawLine(SidePen, P_L1, P_L2);

                        G.DrawLine(SidePen, P_R1, P_R2);

                        if (Rounded)
                        {
                            G.DrawArc(Pen1, rectangle.X, rectangle.Y, radius, radius, 180, 90);
                            G.DrawArc(Pen1, rectangle.X + rectangle.Width - radius, rectangle.Y, radius, radius, 270, 90);
                        }
                        else
                        {
                            G.DrawLine(Pen1, P_T1, P_LT);
                            G.DrawLine(Pen1, P_LT, P_L1);

                            G.DrawLine(Pen1, P_T2, P_RT);
                            G.DrawLine(Pen1, P_RT, P_R1);
                        }

                        G.DrawLine(Pen1, P_T1, P_T2);
                    }
                }

                differentBorder?.Dispose();
            }

            SidePen?.Dispose();
        }

        /// <summary>
        /// Check if a point is inside a polygon (or sets of points)
        /// </summary>
        /// <param name="pointFs"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static bool Contains(this PointF[] pointFs, PointF point)
        {
            // Check if the x-coordinate is between the x-coordinates of the bounding points
            bool isXBetween = point.X >= Math.Min(pointFs[0].X, pointFs[1].X) && point.X <= Math.Max(pointFs[0].X, pointFs[1].X);

            // Check if the y-coordinate is between the y-coordinates of the bounding points
            bool isYBetween = point.Y >= Math.Min(pointFs[0].Y, pointFs[1].Y) && point.Y <= Math.Max(pointFs[0].Y, pointFs[1].Y);

            // The point is between the bounding points if both x and y coordinates are between the corresponding coordinates of the bounding points
            return isXBetween && isYBetween;
        }

        /// <summary>
        /// Check if a point is inside a polygon (or sets of points)
        /// </summary>
        /// <param name="points"></param>
        /// <param name="point"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Check if a point is located in the border of a rectangle but not inside it (supposing that rectangle border size is 1 pixel)
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="pointToCheck"></param>
        /// <returns></returns>
        public static bool BordersContains(this Rectangle rectangle, Point pointToCheck)
        {
            bool isOnBorderX = pointToCheck.X == rectangle.Left || pointToCheck.X == rectangle.Right;
            bool isOnBorderY = pointToCheck.Y == rectangle.Top || pointToCheck.Y == rectangle.Bottom;

            return (isOnBorderX && pointToCheck.Y >= rectangle.Top && pointToCheck.Y <= rectangle.Bottom) ||
                   (isOnBorderY && pointToCheck.X >= rectangle.Left && pointToCheck.X <= rectangle.Right);
        }
    }
}