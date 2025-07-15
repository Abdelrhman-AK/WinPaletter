using Cyotek.Windows.Forms;
using ImageProcessor;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net.NetworkInformation;
using System.Windows.Forms;

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
            if (rectangle.IsEmpty) return;
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            if (brush is null) return;

            if (radius == -1) radius = Program.Style.Radius;

            if ((Program.Style.RoundedCorners || forcedRoundCorner) && radius > 0)
            {
                using (GraphicsPath path = rectangle.Round(radius))
                {
                    /* A try is used as sometimes the returned path is null and when if statement is used to return if path is null doesn't work 
                       and it doesn't detect if path is null or not due to odd reason !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! */
                    try { G?.FillPath(brush, path); } catch { }
                }
            }
            else
            {
                G?.FillRectangle(brush, rectangle);
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
        public static void FillRoundedRect(this Graphics G, Brush brush, RectangleF rectangle, int radius = -1, bool forcedRoundCorner = false)
        {
            if (G is null) return;
            if (rectangle.IsEmpty) return;
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;
            if (brush is null) return;

            if (radius == -1) radius = Program.Style.Radius;

            if ((Program.Style.RoundedCorners || forcedRoundCorner) && radius > 0)
            {
                using (GraphicsPath path = rectangle.Round(radius))
                {
                    /* A try is used as sometimes the returned path is null and when if statement is used to return if path is null doesn't work 
                       and it doesn't detect if path is null or not due to odd reason !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! */
                    try { G?.FillPath(brush, path); } catch { }
                }
            }
            else
            {
                G?.FillRectangle(brush, rectangle);
            }
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
            GraphicsPath path = new();

            if (rectangle.Width <= 0 || rectangle.Height <= 0) return path;

            Radius = Radius == -1 ? Program.Style.Radius * 2 : Radius *= 2;

            path?.AddLine(rectangle.Left + Radius, rectangle.Top, rectangle.Right - Radius, rectangle.Top);
            path?.AddArc(Rectangle.FromLTRB(rectangle.Right - Radius, rectangle.Top, rectangle.Right, rectangle.Top + Radius), -90, 90f);

            path?.AddLine(rectangle.Right, rectangle.Top + Radius, rectangle.Right, rectangle.Bottom - Radius);
            path?.AddArc(Rectangle.FromLTRB(rectangle.Right - Radius, rectangle.Bottom - Radius, rectangle.Right, rectangle.Bottom), 0f, 90f);

            path?.AddLine(rectangle.Right - Radius, rectangle.Bottom, rectangle.Left + Radius, rectangle.Bottom);
            path?.AddArc(Rectangle.FromLTRB(rectangle.Left, rectangle.Bottom - Radius, rectangle.Left + Radius, rectangle.Bottom), 90f, 90f);

            path?.AddLine(rectangle.Left, rectangle.Bottom - Radius, rectangle.Left, rectangle.Top + Radius);
            path?.AddArc(Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Left + Radius, rectangle.Top + Radius), 180f, 90f);

            path?.CloseFigure();
            return path;
        }

        /// <summary>
        /// Rounded <see cref="GraphicsPath"/>
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="Radius"></param>
        /// <returns></returns>
        public static GraphicsPath Round(this RectangleF rectangle, int Radius = -1)
        {
            GraphicsPath path = new();

            if (rectangle.Width <= 0 || rectangle.Height <= 0) return path;

            Radius = Radius == -1 ? Program.Style.Radius * 2 : Radius *= 2;

            path?.AddLine(rectangle.Left + Radius, rectangle.Top, rectangle.Right - Radius, rectangle.Top);
            path?.AddArc(RectangleF.FromLTRB(rectangle.Right - Radius, rectangle.Top, rectangle.Right, rectangle.Top + Radius), -90, 90f);

            path?.AddLine(rectangle.Right, rectangle.Top + Radius, rectangle.Right, rectangle.Bottom - Radius);
            path?.AddArc(RectangleF.FromLTRB(rectangle.Right - Radius, rectangle.Bottom - Radius, rectangle.Right, rectangle.Bottom), 0f, 90f);

            path?.AddLine(rectangle.Right - Radius, rectangle.Bottom, rectangle.Left + Radius, rectangle.Bottom);
            path?.AddArc(RectangleF.FromLTRB(rectangle.Left, rectangle.Bottom - Radius, rectangle.Left + Radius, rectangle.Bottom), 90f, 90f);

            path?.AddLine(rectangle.Left, rectangle.Bottom - Radius, rectangle.Left, rectangle.Top + Radius);
            path?.AddArc(RectangleF.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Left + Radius, rectangle.Top + Radius), 180f, 90f);

            path?.CloseFigure();
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
        /// Draw a rounded rectangle
        /// </summary>
        /// <param name="G"></param>
        /// <param name="pen"></param>
        /// <param name="rectangle"></param>
        /// <param name="radius"></param>
        /// <param name="forcedRoundCorner"></param>
        public static void DrawRoundedRect(this Graphics G, Pen pen, RectangleF rectangle, int radius = -1, bool forcedRoundCorner = false)
        {
            if (G is null) return;
            if (pen is null) return;
            if (rectangle.Width <= 0 || rectangle.Height <= 0) return;

            if (radius == -1) radius = Program.Style.Radius;

            radius *= 2;

            if ((Program.Style.RoundedCorners | forcedRoundCorner) & radius > 0)
            {
                G.DrawArc(pen, rectangle.X, rectangle.Y, radius, radius, 180, 90);
                G.DrawLine(pen, rectangle.X + radius / 2f, rectangle.Y, rectangle.X + rectangle.Width - radius / 2f, rectangle.Y);

                G.DrawArc(pen, rectangle.X + rectangle.Width - radius, rectangle.Y, radius, radius, 270, 90);
                G.DrawLine(pen, rectangle.X, rectangle.Y + radius / 2f, rectangle.X, rectangle.Y + rectangle.Height - radius / 2f);

                G.DrawLine(pen, rectangle.X + rectangle.Width, rectangle.Y + radius / 2f, rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height - radius / 2f);
                G.DrawLine(pen, rectangle.X + radius / 2f, rectangle.Y + rectangle.Height, rectangle.X + rectangle.Width - radius / 2f, rectangle.Y + rectangle.Height);

                G.DrawArc(pen, rectangle.X, rectangle.Y + rectangle.Height - radius, radius, radius, 90, 90);
                G.DrawArc(pen, rectangle.X + rectangle.Width - radius, rectangle.Y + rectangle.Height - radius, radius, radius, 0, 90);
            }
            else
            {
                G.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            }
        }

        private static readonly float[] Positions = [0f, 0.0005f, 0.5f, 0.9995f, 1.0f];

        /// <summary>
        /// Draws a rounded rectangle similar to Windows 11 buttons (with top or bottom bevel effect).
        /// </summary>
        public static void DrawRoundedRectBeveled(this Graphics G, Pen pen, Rectangle rectangle, int radius = -1, bool forcedRoundCorner = false, bool ReverseBevel = false)
        {
            if (G == null || pen?.Brush == null || rectangle.Width <= 0 || rectangle.Height <= 0)
                return;

            bool dark = Program.Style.DarkMode;
            bool useRoundedCorners = (Program.Style.RoundedCorners || forcedRoundCorner);
            if (radius == -1)
                radius = Program.Style.Radius;

            radius *= 2;

            Color baseColor = pen.Brush is LinearGradientBrush lgb ? lgb.LinearColors[0] : pen.Color;
            Color bevelColor = baseColor.CB(dark ? 0.09f : -0.15f);

            bool drawTop = dark ^ ReverseBevel;

            if (useRoundedCorners && radius > 0)
            {
                float halfRadius = radius / 2f;
                float x1 = rectangle.X + halfRadius;
                float x2 = rectangle.X + rectangle.Width - halfRadius;
                float yTop = rectangle.Y;
                float yBottom = rectangle.Y + rectangle.Height;

                using var bevelBrush = new LinearGradientBrush(rectangle, bevelColor, Color.Transparent, 180f)
                {
                    InterpolationColors = new ColorBlend(5)
                    {
                        Colors = [Color.Transparent, bevelColor, bevelColor, bevelColor, Color.Transparent],
                        Positions = Positions
                    }
                };
                using var bevelPen = new Pen(bevelBrush, pen.Width)
                {
                    DashStyle = pen.DashStyle,
                    DashOffset = pen.DashOffset
                };

                G.DrawRoundedRect(pen, rectangle, (int)halfRadius, forcedRoundCorner);
                G.DrawLine(bevelPen, x1, drawTop ? yTop : yBottom, x2, drawTop ? yTop : yBottom);
            }
            else
            {
                using var bevelBrush = new SolidBrush(bevelColor);
                using var bevelPen = new Pen(bevelBrush, pen.Width);

                G.DrawRectangle(pen, rectangle);
                G.DrawLine(bevelPen, rectangle.Left, drawTop ? rectangle.Top : rectangle.Bottom, rectangle.Right, drawTop ? rectangle.Top : rectangle.Bottom);
            }
        }

        /// <summary>
        /// Draws a rounded rectangle similar to Windows 11 buttons (with top or bottom bevel effect but reversed to what <see cref="DrawRoundedRectBeveled"/> does.
        /// </summary>
        public static void DrawRoundedRectBeveledReverse(this Graphics G, Pen pen, Rectangle rectangle, int radius = -1, bool forcedRoundCorner = false)
        {
            DrawRoundedRectBeveled(G, pen, rectangle, radius, forcedRoundCorner, true);
        }

        /// <summary>
        /// Check if a point is inside a polygon (or sets of points) or not
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
        /// Check if a point is inside a polygon (or sets of points) or not
        /// </summary>
        /// <param name="points"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static bool Contains(this Point[] points, PointF point)
        {
            if (points.Length < 2) return false;

            for (int i = 0; i < points.Length - 1; i++)
            {
                if (points[i].X <= point.X && points[i + 1].X >= point.X) return true;
            }

            return false;
        }

        /// <summary>
        /// Check if a rectangle lies in a parent rectangle, not exceeding it. 
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
        /// Check if a point is located in the border of a rectangle and not inside it (supposing that rectangle border size is 1 pixel)
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

        /// <summary>
        /// Get the rectangles for text and image in a button, based on its properties.
        /// </summary>
        /// <param name="button"></param>
        /// <param name="bounds"></param>
        /// <param name="imageRect"></param>
        /// <param name="textRect"></param>
        public static void GetTextAndImageRectangles(this UI.WP.Button button, Rectangle bounds, out RectangleF imageRect, out RectangleF textRect)
        {
            Image img = !button.ImageGlyphEnabled ? button.Image : button.ImageGlyph;
            SizeF imageSize = img?.Size ?? SizeF.Empty;
            SizeF textSize;

            using (Graphics G = button.CreateGraphics())
            {
                textSize = string.IsNullOrEmpty(button.Text) ? SizeF.Empty : TextRenderer.MeasureText(G, button.Text, button.Font);
            }

            GetTextAndImageRectangles(
                bounds,
                imageSize,
                textSize,
                button.ImageAlign,
                button.TextAlign,
                button.TextImageRelation,
                out imageRect,
                out textRect);
        }

        /// <summary>
        /// Get the rectangles for text and image in a radio image button, based on its properties.
        /// </summary>
        /// <param name="button"></param>
        /// <param name="bounds"></param>
        /// <param name="imageRect"></param>
        /// <param name="textRect"></param>
        public static void GetTextAndImageRectangles(this UI.WP.RadioImage button, Rectangle bounds, out RectangleF imageRect, out RectangleF textRect)
        {
            Image img = button.Image;
            SizeF imageSize = img?.Size ?? SizeF.Empty;
            SizeF textSize;

            using (Graphics G = button.CreateGraphics())
            {
                textSize = string.IsNullOrEmpty(button.Text) ? SizeF.Empty : TextRenderer.MeasureText(G, button.Text, button.Font);
            }

            GetTextAndImageRectangles(
                bounds,
                imageSize,
                textSize,
                button.ImageAlign,
                button.TextAlign,
                button.TextImageRelation,
                out imageRect,
                out textRect);
        }

        /// <summary>
        /// Get the rectangles for text and image based on provided properties.
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="imageSize"></param>
        /// <param name="textSize"></param>
        /// <param name="imageAlign"></param>
        /// <param name="textAlign"></param>
        /// <param name="relation"></param>
        /// <param name="imageRect"></param>
        /// <param name="textRect"></param>
        public static void GetTextAndImageRectangles(
          RectangleF bounds,
          SizeF imageSize,
          SizeF textSize,
          ContentAlignment imageAlign,
          ContentAlignment textAlign,
          TextImageRelation relation,
          out RectangleF imageRect,
          out RectangleF textRect)
        {
            imageRect = RectangleF.Empty;
            textRect = RectangleF.Empty;

            bool hasImage = imageSize != SizeF.Empty && imageSize.Width > 0 && imageSize.Height > 0;
            bool hasText = textSize != SizeF.Empty && textSize.Width > 0 && textSize.Height > 0;

            // If no image, just align text to full bounds based on textAlign
            if (!hasImage && hasText)
            {
                textRect = AlignIn(bounds, textSize, textAlign);
                return;
            }

            // If no text, just align image to full bounds based on imageAlign
            if (hasImage && !hasText)
            {
                imageRect = AlignIn(bounds, imageSize, imageAlign);
                return;
            }

            // If neither image nor text, both rects remain empty
            if (!hasImage && !hasText)
                return;

            // Both image and text present - proceed with relation logic
            float spacing = 6f;

            switch (relation)
            {
                case TextImageRelation.Overlay:
                    imageRect = AlignIn(bounds, imageSize, imageAlign);
                    textRect = AlignIn(bounds, textSize, textAlign);
                    break;

                case TextImageRelation.ImageBeforeText:
                    {
                        float totalWidth = imageSize.Width + spacing + textSize.Width;
                        float maxHeight = Math.Max(imageSize.Height, textSize.Height);

                        // Use imageAlign to align the block
                        RectangleF totalRect = AlignIn(bounds, new SizeF(totalWidth, maxHeight), imageAlign);

                        RectangleF imageRegion = new RectangleF(totalRect.X, totalRect.Y, imageSize.Width, maxHeight);
                        RectangleF textRegion = new RectangleF(imageRegion.Right + spacing, totalRect.Y, textSize.Width, maxHeight);

                        imageRect = AlignIn(imageRegion, imageSize, imageAlign);
                        textRect = AlignIn(textRegion, textSize, textAlign);
                    }
                    break;

                case TextImageRelation.TextBeforeImage:
                    {
                        float totalWidth = imageSize.Width + spacing + textSize.Width;
                        float maxHeight = Math.Max(imageSize.Height, textSize.Height);

                        // Use textAlign to align the block
                        RectangleF totalRect = AlignIn(bounds, new SizeF(totalWidth, maxHeight), textAlign);

                        RectangleF textRegion = new RectangleF(totalRect.X, totalRect.Y, textSize.Width, maxHeight);
                        RectangleF imageRegion = new RectangleF(textRegion.Right + spacing, totalRect.Y, imageSize.Width, maxHeight);

                        imageRect = AlignIn(imageRegion, imageSize, imageAlign);
                        textRect = AlignIn(textRegion, textSize, textAlign);
                    }
                    break;

                case TextImageRelation.ImageAboveText:
                    {
                        float totalHeight = imageSize.Height + spacing + textSize.Height;
                        float maxWidth = Math.Max(imageSize.Width, textSize.Width);

                        // Use imageAlign to align the block
                        RectangleF totalRect = AlignIn(bounds, new SizeF(maxWidth, totalHeight), imageAlign);

                        RectangleF imageRegion = new RectangleF(totalRect.X, totalRect.Y, maxWidth, imageSize.Height);
                        RectangleF textRegion = new RectangleF(totalRect.X, imageRegion.Bottom + spacing, maxWidth, textSize.Height);

                        imageRect = AlignIn(imageRegion, imageSize, imageAlign);
                        textRect = AlignIn(textRegion, textSize, textAlign);
                    }
                    break;

                case TextImageRelation.TextAboveImage:
                    {
                        float totalHeight = imageSize.Height + spacing + textSize.Height;
                        float maxWidth = Math.Max(imageSize.Width, textSize.Width);

                        // Use textAlign to align the block
                        RectangleF totalRect = AlignIn(bounds, new SizeF(maxWidth, totalHeight), textAlign);

                        RectangleF textRegion = new RectangleF(totalRect.X, totalRect.Y, maxWidth, textSize.Height);
                        RectangleF imageRegion = new RectangleF(totalRect.X, textRegion.Bottom + spacing, maxWidth, imageSize.Height);

                        imageRect = AlignIn(imageRegion, imageSize, imageAlign);
                        textRect = AlignIn(textRegion, textSize, textAlign);
                    }
                    break;
            }
        }

        /// <summary>
        /// Aligns the content of given size inside the container rectangle based on alignment.
        /// </summary>
        private static RectangleF AlignIn(RectangleF container, SizeF content, ContentAlignment align)
        {
            float x = container.X;
            float y = container.Y;

            switch (align)
            {
                case ContentAlignment.TopLeft:
                    break;
                case ContentAlignment.TopCenter:
                    x += (container.Width - content.Width) / 2f;
                    break;
                case ContentAlignment.TopRight:
                    x += container.Width - content.Width;
                    break;
                case ContentAlignment.MiddleLeft:
                    y += (container.Height - content.Height) / 2f;
                    break;
                case ContentAlignment.MiddleCenter:
                    x += (container.Width - content.Width) / 2f;
                    y += (container.Height - content.Height) / 2f;
                    break;
                case ContentAlignment.MiddleRight:
                    x += container.Width - content.Width;
                    y += (container.Height - content.Height) / 2f;
                    break;
                case ContentAlignment.BottomLeft:
                    y += container.Height - content.Height;
                    break;
                case ContentAlignment.BottomCenter:
                    x += (container.Width - content.Width) / 2f;
                    y += container.Height - content.Height;
                    break;
                case ContentAlignment.BottomRight:
                    x += container.Width - content.Width;
                    y += container.Height - content.Height;
                    break;
            }

            return new RectangleF(x, y, content.Width, content.Height);
        }

        /// <summary>
        /// Capture a screenshot of a part of screen.
        /// </summary>
        /// <param name="rectangleToScreen"></param>
        /// <returns></returns>
        public static Bitmap CaptureFromScreen(Rectangle rectangleToScreen)
        {
            using (Bitmap bmp = new(rectangleToScreen.Width, rectangleToScreen.Height))
            using (Graphics G = Graphics.FromImage(bmp))
            {
                G.CopyFromScreen(rectangleToScreen.Location, Point.Empty, rectangleToScreen.Size);
                return bmp.Clone() as Bitmap;
            }
        }
    }
}