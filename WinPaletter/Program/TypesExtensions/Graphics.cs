using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;
using WinPaletter.UI.WP;

namespace WinPaletter.TypesExtensions
{
    /// <summary>
    /// Extension methods for <see cref="Graphics"/> class
    /// </summary>
    public static class GraphicsExtensions
    {
        /// <summary>
        /// Draws a string with a glowing effect onto the specified <see cref="Graphics"/> surface.
        /// </summary>
        /// <remarks>This method first renders the glow effect by drawing the text with a scaled-down transformation and
        /// applying the specified glow color. It then draws the actual text on top of the glow effect using the specified
        /// foreground color. The method ensures high-quality rendering by temporarily setting the <see
        /// cref="Graphics.InterpolationMode"/> to <see cref="InterpolationMode.HighQualityBicubic"/>.</remarks>
        /// <param name="G">The <see cref="Graphics"/> object on which to draw the glowing string.</param>
        /// <param name="glowSize">The size of the glow effect around the text. Must be a positive value.</param>
        /// <param name="text">The text to be drawn. If null or whitespace, an empty string is used.</param>
        /// <param name="font">The <see cref="Font"/> used to render the text. Cannot be null.</param>
        /// <param name="foreColor">The color of the text.</param>
        /// <param name="glowColor">The color of the glow effect surrounding the text.</param>
        /// <param name="clientRectangle">The bounds that defines the area where the glowing text will be drawn.</param>
        /// <param name="rectangle">The bounds that specifies the layout of the text within the <paramref name="clientRectangle"/>.</param>
        /// <param name="format">The <see cref="StringFormat"/> that specifies text layout information, such as alignment and line spacing. Cannot be null.</param>
        public static void DrawGlowString(this Graphics G, int glowSize, string text, Font font, Color foreColor, Color glowColor, RectangleF clientRectangle, RectangleF rectangle, StringFormat format)
        {
            if (G == null || font == null || format == null || clientRectangle.Width <= 0 || clientRectangle.Height <= 0) return;
            if (string.IsNullOrWhiteSpace(text)) return;
            if (glowSize <= 0) return;
            if (foreColor.A == 0 && glowColor.A == 0) return;

            const float glowFactor = 5f;
            float w = Math.Max(8f, clientRectangle.Width / glowFactor);
            float h = Math.Max(8f, clientRectangle.Height / glowFactor);
            float emSize = G.DpiY * font.SizeInPoints / 72f;

            int bitmapWidth = (int)w;
            int bitmapHeight = (int)h;

            using Bitmap b = new(bitmapWidth, bitmapHeight, PixelFormat.Format32bppArgb);
            using GraphicsPath gp = new();
            using Graphics gx = Graphics.FromImage(b);
            using Matrix m = new(1.0f / glowFactor, 0f, 0f, 1.0f / glowFactor, -(1.0f / glowFactor), -(1.0f / glowFactor));
            using SolidBrush br = new(foreColor);
            using Pen P = new(glowColor, glowSize);

            gp.AddString(text, font.FontFamily, (int)font.Style, emSize, rectangle, format);

            gx.Transform = m;
            gx.DrawPath(P, gp);
            gx.FillPath(P.Brush, gp);

            InterpolationMode oldInterpolationMode = G.InterpolationMode;
            G.InterpolationMode = InterpolationMode.HighQualityBicubic;

            try
            {
                G.DrawImage(b, clientRectangle, new Rectangle(0, 0, bitmapWidth, bitmapHeight), GraphicsUnit.Pixel);
                G.DrawString(text, font, br, rectangle, format);
            }
            finally
            {
                G.InterpolationMode = oldInterpolationMode;
            }
        }

        /// <summary>
        /// Draws a glowing effect around the specified bounds on the provided <see cref="Graphics"/> surface.
        /// </summary>
        /// <remarks>This method creates a glowing effect around the specified bounds by drawing a semi-transparent
        /// glow with the specified color, size, and fade factor. The glow can optionally follow a rounded bounds
        /// shape.</remarks>
        /// <param name="G">The <see cref="Graphics"/> object on which the glow effect will be drawn. Cannot be <see langword="null"/>.</param>
        /// <param name="rectangle">The <see cref="Rectangle"/> around which the glow effect will be applied. Must have a positive width and height.</param>
        /// <param name="glowColor">The <see cref="Color"/> of the glow effect.</param>
        /// <param name="glowSize">The size of the glow effect in pixels. Defaults to 5. Must be greater than 0.</param>
        /// <param name="glowFade">The fade factor of the glow effect, which determines the smoothness of the glow. Defaults to 7. Must be greater than
        /// 0.</param>
        /// <param name="rounded">A value indicating whether the glow effect should follow a rounded bounds shape. If <see langword="true"/>, the
        /// glow will be applied to a rounded bounds; otherwise, it will follow the standard bounds shape.</param>
        public static void DrawGlow(this Graphics G, RectangleF rectangle, Color glowColor, int glowSize = 5, int glowFade = 7, bool rounded = false)
        {
            if (G == null || rectangle.Width <= 0 || rectangle.Height <= 0) return;
            if (glowSize <= 0) glowSize = 1;
            if (glowFade <= 0) glowFade = 1;
            if (glowColor.A == 0) return;

            RectangleF glowRect = rounded ? GetRoundedRectangle(rectangle, glowSize) : new(rectangle.X - glowSize - 2, rectangle.Y - glowSize - 2, rectangle.Width + glowSize * 2 + 3, rectangle.Height + glowSize * 2 + 3);

            int bitmapWidth = Math.Max(1, (int)(glowRect.Width / glowFade));
            int bitmapHeight = Math.Max(1, (int)(glowRect.Height / glowFade));

            using Bitmap glowBitmap = new(bitmapWidth, bitmapHeight, PixelFormat.Format32bppArgb);
            using Graphics glowGraphics = Graphics.FromImage(glowBitmap);
            {
                Rectangle glowRect2 = new(1, 1, glowBitmap.Width, glowBitmap.Height);
                using SolidBrush glowBrush = new(glowColor);
                glowGraphics.FillRectangle(glowBrush, glowRect2);
            }
            G.DrawImage(glowBitmap, glowRect);
        }

        private static RectangleF GetRoundedRectangle(RectangleF rectangle, int radius)
        {
            int diameter = 2 * radius;
            RectangleF roundedRect = new(rectangle.X - radius, rectangle.Y - radius, rectangle.Width + diameter, rectangle.Height + diameter);
            return roundedRect;
        }

        /// <summary>
        /// Draws an Aero-style visual effect on the specified graphics surface within the given bounds.
        /// </summary>
        /// <remarks>This method applies a layered visual effect that combines a blurred background, color tinting, and
        /// optional glow. It supports both rectangular and rounded-corner shapes, depending on the value of <paramref
        /// name="roundedCorners"/>.</remarks>
        /// <param name="G">The <see cref="Graphics"/> object on which the effect will be drawn. Cannot be <see langword="null"/>.</param>
        /// <param name="rect">The <see cref="Rectangle"/> defining the area where the effect will be applied. Must have positive width and height.</param>
        /// <param name="backgroundBlurred">A pre-blurred <see cref="Bitmap"/> used as the background for the effect. Cannot be <see langword="null"/>.</param>
        /// <param name="colorizationColor">The primary color used for the effect, influencing the overall tint.</param>
        /// <param name="colorBalance">A value between 0 and 1 that determines the balance between <paramref name="colorizationColor"/> and the background. Higher
        /// values emphasize <paramref name="colorizationColor"/>.</param>
        /// <param name="glowColor">The secondary color used for blending with <paramref name="colorizationColor"/> to create a gradient effect.</param>
        /// <param name="glowBalance">A value between 0 and 1 that controls the intensity of the glow effect. Higher values produce a stronger glow.</param>
        /// <param name="alpha">A value between 0 and 1 that specifies the transparency of the effect. Higher values make the effect more opaque.</param>
        /// <param name="radius">The corner _radius for rounded rectangles. Ignored if <paramref name="roundedCorners"/> is <see langword="false"/>.</param>
        /// <param name="roundedCorners">A <see langword="bool"/> indicating whether the effect should use rounded corners. If <see langword="true"/>,
        /// rounded corners are applied.</param>
        /// <summary>
        /// Draws an emulated Windows Vista/7 Aero effect preview.
        /// </summary>
        public static void DrawAeroEffect(this Graphics G, RectangleF rect, Bitmap backgroundBlurred, Color colorizationColor, float colorBalance, Color glowColor, float glowBalance, float alpha, int radius, bool roundedCorners)
        {
            if (G == null || rect.Width <= 0 || rect.Height <= 0 || backgroundBlurred == null) return;

            // Clamp values inline
            colorBalance = colorBalance < 0f ? 0f : colorBalance > 1f ? 1f : colorBalance;
            glowBalance = glowBalance < 0f ? 0f : glowBalance > 1f ? 1f : glowBalance;
            alpha = alpha < 0f ? 0f : alpha > 1f ? 1f : alpha;

            RectangleF innerRect = new(rect.X, rect.Y, rect.Width - 1, rect.Height - 1);

            // Set smoothing for rounded corners
            if (roundedCorners && radius > 0)
            {
                using GraphicsPath path = innerRect.Round(radius);
                G.SetClip(path);
                try
                {
                    RenderAeroLayers(G, rect, backgroundBlurred, colorizationColor, colorBalance, glowColor, glowBalance, alpha);
                }
                finally
                {
                    G.ResetClip();
                }
            }
            else
            {
                RenderAeroLayers(G, rect, backgroundBlurred, colorizationColor, colorBalance, glowColor, glowBalance, alpha);
            }
        }

        /// <summary>
        /// Internal helper that renders Aero layers in correct order.
        /// </summary>
        private static void RenderAeroLayers(Graphics G, RectangleF rect, Bitmap backgroundBlurred, Color colorizationColor, float colorBalance, Color glowColor, float glowBalance, float alpha)
        {
            // Base blurred background
            G.DrawImage(backgroundBlurred, rect);

            // Colorization overlay (tint)
            if (colorBalance > 0f && colorizationColor.A > 0)
            {
                int tintAlpha = (int)(alpha * colorBalance * 255);
                if (tintAlpha > 0)
                {
                    using SolidBrush tintBrush = new(Color.FromArgb(tintAlpha, colorizationColor));
                    G.FillRectangle(tintBrush, rect);
                }
            }

            // Subtle darkening layer (DWM adds faint shadow)
            if (alpha > 0f)
            {
                int shadowAlpha = (int)(alpha * 60); // ~24% opacity
                using SolidBrush shadowBrush = new(Color.FromArgb(shadowAlpha, Color.Black));
                G.FillRectangle(shadowBrush, rect);
            }

            // Glow overlay (soft highlight, usually near text or edges)
            if (glowBalance > 0f && glowColor.A > 0)
            {
                int glowAlpha = (int)(alpha * glowBalance * 180); // softer intensity
                if (glowAlpha > 0)
                {
                    using SolidBrush glowBrush = new(Color.FromArgb(glowAlpha, glowColor));
                    G.FillRectangle(glowBrush, rect);
                }
            }

            // White highlight (faint top-light effect, optional)
            int whiteAlpha = (int)((1f - alpha) * 40); // subtle top-glass shine
            if (whiteAlpha > 0)
            {
                using LinearGradientBrush whiteBrush = new(new PointF(rect.Left, rect.Top), new PointF(rect.Left, rect.Bottom), Color.FromArgb(whiteAlpha, Color.White), Color.Transparent)
                {
                    InterpolationColors = new(3)
                    {
                        Colors = [Color.FromArgb(whiteAlpha, Color.White), Color.FromArgb(whiteAlpha / 2, Color.White), Color.Transparent],
                        Positions = [0f, 0.5f, 1f]
                    }
                };
                G.FillRectangle(whiteBrush, rect);
            }
        }

        [Flags]
        public enum RoundedCorners
        {
            None = 0,
            TopLeft = 1,
            TopRight = 2,
            BottomRight = 4,
            BottomLeft = 8,
            All = TopLeft | TopRight | BottomRight | BottomLeft
        }

        /// <summary>
        /// Creates a rounded bounds as a <see cref="GraphicsPath"/> with the specified corner _radius.
        /// </summary>
        /// <param name="rectangle">The <see cref="Rectangle"/> to round.</param>
        /// <param name="radius">The _radius of the corners. If set to -1, the _radius will default to half the smaller dimension of the bounds.</param>
        /// <returns>A <see cref="GraphicsPath"/> representing the rounded bounds.</returns>
        public static GraphicsPath Round(this Rectangle rectangle, int radius = -1, RoundedCorners corners = RoundedCorners.All)
        {
            return Round(new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height), radius, corners);
        }

        /// <summary>
        /// Creates a <see cref="GraphicsPath"/> representing a bounds with rounded corners.
        /// </summary>
        /// <remarks>This method adjusts the shape of the bounds based on its dimensions: <list
        /// type="bullet"> <item>If the bounds is too narrow or short, the result may be a capsule shape.</item>
        /// <item>If the _radius is zero or the bounds's dimensions are too small, the result will be a standard
        /// bounds.</item> </list></remarks>
        /// <param name="rectangle">The <see cref="RectangleF"/> that defines the bounds of the shape.</param>
        /// <param name="radius">The _radius of the rounded corners. If negative, a default _radius is used. The _radius is clamped to ensure it
        /// does not exceed half the width or height of the bounds.</param>
        /// <returns>A <see cref="GraphicsPath"/> representing the rounded bounds. If the bounds's dimensions are too small
        /// or the _radius is zero, the path will represent a standard bounds.</returns>
        public static GraphicsPath Round(this RectangleF rectangle, int radius = -1, RoundedCorners corners = RoundedCorners.All)
        {
            float width = rectangle.Width;
            float height = rectangle.Height;

            if (width <= 0f || height <= 0f)
            {
                GraphicsPath emptyPath = new() { FillMode = FillMode.Winding };
                return emptyPath;
            }

            if (radius < 0) radius = Program.Style.Radius;

            float r = Math.Min(radius, Math.Min(width, height) / 2f);
            if (r <= 0f || corners == RoundedCorners.None)
            {
                GraphicsPath rectPath = new() { FillMode = FillMode.Winding };
                rectPath.AddRectangle(rectangle);
                rectPath.CloseFigure();
                return rectPath;
            }

            float d = r * 2f;
            float left = rectangle.Left;
            float top = rectangle.Top;
            float right = rectangle.Right;
            float bottom = rectangle.Bottom;

            bool tl = corners.HasFlag(RoundedCorners.TopLeft);
            bool tr = corners.HasFlag(RoundedCorners.TopRight);
            bool br = corners.HasFlag(RoundedCorners.BottomRight);
            bool bl = corners.HasFlag(RoundedCorners.BottomLeft);

            // True capsule detection
            bool verticalCapsule = width <= r;
            bool horizontalCapsule = height <= r;

            GraphicsPath path = new() { FillMode = FillMode.Winding };
            path.StartFigure();

            if (verticalCapsule)
            {
                bool roundTop = tl || tr;
                bool roundBottom = bl || br;

                if (roundTop)
                    path.AddArc(left, top, width, height, 180, 180);
                else
                    path.AddLine(left, top, right, top);

                if (roundBottom)
                    path.AddArc(left, top, width, height, 0, 180);
                else
                    path.AddLine(right, bottom, left, bottom);

                path.CloseFigure();
                return path;
            }

            if (horizontalCapsule)
            {
                bool roundLeft = tl || bl;
                bool roundRight = tr || br;

                if (roundLeft)
                    path.AddArc(left, top, width, height, 90, 180);
                else
                    path.AddLine(left, top, left, bottom);

                if (roundRight)
                    path.AddArc(left, top, width, height, 270, 180);
                else
                    path.AddLine(right, bottom, right, top);

                path.CloseFigure();
                return path;
            }

            // Normal case — continuous perimeter drawing
            // Top edge
            if (tl)
                path.AddArc(left, top, d, d, 180, 90);
            else
                path.AddLine(left, top + r, left, top);

            path.AddLine(tl ? left + r : left, top, tr ? right - r : right, top);

            // Right edge
            if (tr)
                path.AddArc(right - d, top, d, d, 270, 90);
            else
                path.AddLine(right, top, right, top + r);

            path.AddLine(right, tr ? top + r : top, right, br ? bottom - r : bottom);

            // Bottom edge
            if (br)
                path.AddArc(right - d, bottom - d, d, d, 0, 90);
            else
                path.AddLine(right, bottom, right - r, bottom);

            path.AddLine(br ? right - r : right, bottom, bl ? left + r : left, bottom);

            // Left edge
            if (bl)
                path.AddArc(left, bottom - d, d, d, 90, 90);
            else
                path.AddLine(left, bottom, left, bottom - r);

            path.AddLine(left, bl ? bottom - r : bottom, left, tl ? top + r : top);

            path.CloseFigure();
            return path;
        }

        /// <summary>
        /// Fills a bounds with rounded corners using the specified brush.
        /// </summary>
        public static void FillRoundedRect(this Graphics G, Brush brush, Rectangle rectangle, int radius = -1, bool forcedRoundCorner = false, RoundedCorners corners = RoundedCorners.All)
        {
            FillRoundedRect(G, brush, new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height), radius, forcedRoundCorner, corners);
        }

        /// <summary>
        /// Fills a bounds with rounded corners or a standard bounds, depending on the specified parameters.
        /// </summary>
        public static void FillRoundedRect(this Graphics G, Brush brush, RectangleF rectangle, int radius = -1, bool forcedRoundCorner = false, RoundedCorners corners = RoundedCorners.All)
        {
            if (G == null || !IsValid(brush) || rectangle.IsEmpty || rectangle.Width <= 0 || rectangle.Height <= 0) return;

            if (radius == -1) radius = Program.Style.Radius;

            bool useRounded = (Program.Style.RoundedCorners || forcedRoundCorner) && radius > 0;

            if (useRounded)
            {
                using GraphicsPath path = rectangle.Round(radius, corners);
                G.FillPath(brush, path);
            }
            else
            {
                G.FillRectangle(brush, rectangle);
            }
        }

        /// <summary>
        /// Draws an image within a specified bounds, optionally applying rounded corners.
        /// </summary>
        public static void DrawRoundImage(this Graphics G, Image image, RectangleF rectangle, int radius = -1, bool forcedRoundCorner = false)
        {
            if (G == null || image == null || rectangle.IsEmpty || rectangle.Width <= 0 || rectangle.Height <= 0) return;

            if (radius == -1) radius = Program.Style.Radius;

            bool useRounded = (Program.Style.RoundedCorners || forcedRoundCorner) && radius > 0;

            if (!useRounded)
            {
                G.DrawImage(image, rectangle);
                return;
            }

            using GraphicsPath path = rectangle.Round(radius);

            SmoothingMode oldSmoothing = G.SmoothingMode;
            PixelOffsetMode oldPixelOffset = G.PixelOffsetMode;
            Region oldClip = G.Clip; // Save current clip region

            try
            {
                G.SmoothingMode = SmoothingMode.AntiAlias;
                G.PixelOffsetMode = PixelOffsetMode.HighQuality;

                G.SetClip(path, CombineMode.Intersect); // Use Intersect to preserve existing clips
                G.DrawImage(image, rectangle);
            }
            finally
            {
                // Restore original settings
                G.Clip = oldClip; // Restore previous clip
                oldClip?.Dispose(); // Dispose old clip
                G.SmoothingMode = oldSmoothing;
                G.PixelOffsetMode = oldPixelOffset;
            }
        }

        /// <summary>
        /// Draws a bounds with rounded corners on the specified <see cref="Graphics"/> surface.
        /// </summary>
        public static void DrawRoundedRect(this Graphics G, Pen pen, Rectangle rectangle, int radius = -1, bool forcedRoundCorner = false, RoundedCorners corners = RoundedCorners.All)
        {
            DrawRoundedRect(G, pen, new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height), radius, forcedRoundCorner, corners);
        }

        /// <summary>
        /// Draws a bounds with optional rounded corners on the specified <see cref="Graphics"/> surface.
        /// </summary>
        public static void DrawRoundedRect(this Graphics G, Pen pen, RectangleF rectangle, int radius = -1, bool forcedRoundCorner = false, RoundedCorners corners = RoundedCorners.All)
        {
            if (G == null || !IsValid(pen) || rectangle.IsEmpty || rectangle.Width <= 0 || rectangle.Height <= 0) return;

            if (radius == -1) radius = Program.Style.Radius;

            bool useRounded = (Program.Style.RoundedCorners || forcedRoundCorner) && radius > 0;

            if (useRounded)
            {
                using GraphicsPath path = rectangle.Round(radius, corners);
                G.DrawPath(pen, path);
            }
            else
            {
                G.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            }
        }

        /// <summary>
        /// Draws a rounded rectangle with a 1px WinUI3-style bevel highlight/shadow on the top or bottom edge.
        /// </summary>
        public static void DrawRoundedRectBeveled(this Graphics G, Pen pen, RectangleF rectangleF, float radius = -1, bool forcedRoundCorner = false, bool reverseBevel = false, RoundedCorners corners = RoundedCorners.All)
        {
            if (G == null || !IsValid(pen) || rectangleF.IsEmpty || rectangleF.Width <= 0 || rectangleF.Height <= 0) return;

            bool dark = Program.Style.DarkMode;
            bool useRoundedCorners = Program.Style.RoundedCorners || forcedRoundCorner;
            if (radius < 0) radius = Program.Style.Radius;

            // Clamp radius to prevent overlapping arcs
            radius = Math.Min(radius, Math.Min(rectangleF.Width, rectangleF.Height) / 2f);

            Color baseColor = pen.Brush is LinearGradientBrush lgb ? lgb.LinearColors[0] : pen.Color;
            Color bevelColor = baseColor.CB(dark ? 0.09f : -0.08f);
            bool drawTop = dark ^ reverseBevel;

            if (useRoundedCorners && radius > 0)
            {
                using GraphicsPath path = rectangleF.Round((int)radius, corners);
                G.DrawPath(pen, path);
            }
            else
            {
                G.DrawRectangle(pen, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
            }

            DrawBevel(G, pen.Width, rectangleF, useRoundedCorners ? radius : 0f, bevelColor, drawTop, reverseBevel);
        }

        /// <summary>
        /// Draws the 1px bevel highlight line.
        /// When <paramref name="insetBevel"/> is false (default), the line sits flat on the border edge —
        /// a straight segment capped between the two corner arc endpoints.
        /// When true, the line is a concentric 3-sided open path inset by penWidth/2:
        /// a left arc + straight horizontal segment + right arc, with endpoints touching the left/right borders.
        /// </summary>
        private static void DrawBevel(Graphics G, float penWidth, RectangleF rect, float radius, Color bevelColor, bool drawTop, bool insetBevel = true)
        {
            if (bevelColor.A == 0) return;

            float inset = insetBevel ? penWidth / 2f : 0f;
            float innerRadius = Math.Max(0f, radius - inset);

            // Horizontal extent of the straight segment (between the two arc tangent points)
            float x1 = rect.Left + radius;   // straight segment starts here (outer arc tangent)
            float x2 = rect.Right - radius;  // straight segment ends here

            if (x1 > x2) return; // rect too narrow to draw anything

            float edgeY = drawTop ? rect.Top : rect.Bottom;
            float bevelY = edgeY + (drawTop ? inset : -inset);

            // Horizontal fade: solid center, 1px alpha falloff at each end
            float lineLen = x2 - x1;

            if (!insetBevel || radius <= 0f)
            {
                // Simple straight line between arc tangent points
                if (x1 >= x2) return;

                float fadeStop = lineLen > 0f ? Math.Min(2f / lineLen, 0.49f) : 0f;

                RectangleF gradRect = new(x1, bevelY - 1f, Math.Max(lineLen, 1f), 2f);
                using LinearGradientBrush brush = new(gradRect, Color.Transparent, Color.Transparent, 0f)
                {
                    InterpolationColors = new ColorBlend(4)
                    {
                        Colors = [Color.Transparent, bevelColor, bevelColor, Color.Transparent],
                        Positions = [0f, fadeStop, 1f - fadeStop, 1f]
                    }
                };
                using Pen bevelPen = new(brush, 1f);
                G.DrawLine(bevelPen, x1, bevelY, x2, bevelY);
            }
            else
            {
                // 3-sided open path: left arc → straight → right arc
                // Arcs join the lateral (left/right) border lines to the horizontal bevel segment.
                // Each arc sweeps only far enough to reach bevelY — no full quarter-circle,
                // which prevents overlap with the border corner arcs.

                float arcDiam = innerRadius * 2f;

                // Vertical distance from border edge to bevelY (always positive)
                float dy = Math.Abs(bevelY - edgeY);

                // Sweep angle: how far the arc rotates to drop/rise by dy within innerRadius
                // Clamped to 90° max to guard against float error when dy ≈ innerRadius
                float sweepDeg = dy > 0f && innerRadius > 0f ? Math.Min((float)(Math.Asin(Math.Min(dy / innerRadius, 1.0)) * (180.0 / Math.PI)), 90f) : 0f;

                if (sweepDeg < 0.5f)
                {
                    // Arc too shallow to be worth drawing — fall back to straight line only
                    float fallbackX1 = rect.Left + inset;
                    float fallbackX2 = rect.Right - inset;
                    float fallbackLen = Math.Max(fallbackX2 - fallbackX1, 1f);
                    float fallbackFade = Math.Min(2f / fallbackLen, 0.49f);

                    RectangleF fallbackRect = new(fallbackX1, bevelY - 1f, fallbackLen, 2f);
                    using LinearGradientBrush fallbackBrush = new(fallbackRect, Color.Transparent, Color.Transparent, 0f)
                    {
                        InterpolationColors = new ColorBlend(4)
                        {
                            Colors = [Color.Transparent, bevelColor, bevelColor, Color.Transparent],
                            Positions = [0f, fallbackFade, 1f - fallbackFade, 1f]
                        }
                    };
                    using Pen fallbackPen = new(fallbackBrush, 1f);
                    G.DrawLine(fallbackPen, fallbackX1, bevelY, fallbackX2, bevelY);
                    return;
                }

                // Arc centers sit on the inner edge at the lateral border positions, at the same Y as the border corner arc centers (edgeY ± innerRadius)
                float arcCenterY = drawTop ? edgeY + innerRadius : edgeY - innerRadius;

                // GDI+ angles: 0° = 3-o'clock, clockwise positive
                // Top bevel:
                //   Left arc:  starts pointing left (180°), sweeps CW by sweepDeg → curves down-right to bevelY
                //   Right arc: starts pointing up-right (270° + (90° - sweepDeg)), sweeps CW by sweepDeg → curves down-right to border
                // Bottom bevel mirrors with negative sweeps
                float leftStartAngle = drawTop ? 180f + (90f - sweepDeg) : 90f + (90f - sweepDeg);
                float leftSweepAngle = drawTop ? sweepDeg : -sweepDeg;
                float rightStartAngle = drawTop ? 270f : 90f;
                float rightSweepAngle = drawTop ? sweepDeg : -sweepDeg;

                // Arc box origins: centered on lateral border inner face
                float arcBoxY = bevelY + (bevelY - (arcCenterY - innerRadius));
                float arcLeftX = rect.Left + inset - innerRadius;
                float arcRightX = rect.Right - inset - innerRadius;
                float newBevelY = bevelY + (bevelY - bevelY);

                using GraphicsPath path = new();

                // Left arc
                path.AddArc(arcLeftX, arcBoxY, arcDiam, arcDiam, leftStartAngle, leftSweepAngle);

                // Straight segment: full inner width at bevel line
                path.AddLine(rect.Left + inset, bevelY, rect.Right - inset, bevelY);

                // Right arc
                path.AddArc(arcRightX, arcBoxY, arcDiam, arcDiam, rightStartAngle, rightSweepAngle);

                // Horizontal gradient: solid center, 2px fade at each lateral end
                float pathX1 = rect.Left + inset;
                float totalWidth = Math.Max((rect.Right - inset) - pathX1, 1f);
                float fadeStop = Math.Min(2f / totalWidth, 0.49f);

                RectangleF gradRect = new(pathX1, bevelY - 1f, totalWidth, 2f);
                using LinearGradientBrush brush = new(gradRect, Color.Transparent, Color.Transparent, 0f)
                {
                    InterpolationColors = new ColorBlend(4)
                    {
                        Colors = [Color.Transparent, bevelColor, bevelColor, Color.Transparent],
                        Positions = [0f, fadeStop, 1f - fadeStop, 1f]
                    }
                };

                using (Pen bevelPen = new(brush))
                {
                    G.DrawPath(bevelPen, path);
                }
            }
        }

        public static void DrawRoundedRectBeveled(this Graphics G, Pen pen, Rectangle rectangle, float radius = -1, bool forcedRoundCorner = false, bool reverseBevel = false, RoundedCorners corners = RoundedCorners.All)
        {
            DrawRoundedRectBeveled(G, pen, (RectangleF)rectangle, radius, forcedRoundCorner, reverseBevel, corners);
        }

        public static void DrawRoundedRectBeveledReverse(this Graphics G, Pen pen, Rectangle rectangle, int radius = -1, bool forcedRoundCorner = false, RoundedCorners corners = RoundedCorners.All)
        {
            DrawRoundedRectBeveled(G, pen, rectangle, radius, forcedRoundCorner, reverseBevel: true, corners);
        }

        private static TextureBrush _noiseBrush;
        private static Bitmap _stamp;
        private static byte[] _radialAlpha;
        private static int _cachedHoverSize = -1;
        private static float _noiseStrength = 1f;

        private static void EnsureCache(int hoverSize)
        {
            if (_cachedHoverSize == hoverSize) return;

            _noiseBrush?.Dispose();
            _noiseBrush = new TextureBrush(Properties.Resources.Noise);

            _stamp?.Dispose();
            _stamp = new Bitmap(hoverSize, hoverSize, PixelFormat.Format32bppArgb);

            int total = hoverSize * hoverSize;
            _radialAlpha = new byte[total];
            float cx = hoverSize / 2f;
            float cy = hoverSize / 2f;
            float maxDist = cx;

            for (int i = 0; i < total; i++)
            {
                float dx = (i % hoverSize) - cx;
                float dy = (i / hoverSize) - cy;
                float dist = (float)Math.Sqrt(dx * dx + dy * dy);
                float t = 1f - Math.Min(dist / maxDist, 1f);
                _radialAlpha[i] = (byte)(t * t * 255f);
            }

            _cachedHoverSize = hoverSize;
        }

        private static void DrawHoverCore(Graphics G, Region clipRegion, Rectangle circle, Color color, float noiseStrength = -1f)
        {
            int hoverSize = circle.Width;
            if (noiseStrength == -1f) noiseStrength = _noiseStrength;

            G.SetClip(clipRegion, CombineMode.Replace);

            EnsureCache(hoverSize);

            using (Graphics sg = Graphics.FromImage(_stamp))
            {
                sg.Clear(Color.Transparent);
                sg.CompositingMode = CompositingMode.SourceCopy;

                using (GraphicsPath gp = new())
                {
                    gp.AddEllipse(0, 0, hoverSize, hoverSize);
                    sg.SetClip(gp);
                }

                using (SolidBrush sb = new(Color.FromArgb(255, color)))
                {
                    sg.FillRectangle(sb, 0, 0, hoverSize, hoverSize);
                }

                sg.ResetClip();
            }

            using (Bitmap noiseBmp = new(hoverSize, hoverSize, PixelFormat.Format32bppArgb))
            {
                using (Graphics ng = Graphics.FromImage(noiseBmp))
                {
                    ng.Clear(Color.Transparent);
                    ng.CompositingMode = CompositingMode.SourceCopy;

                    using (GraphicsPath gp = new())
                    {
                        gp.AddEllipse(0, 0, hoverSize, hoverSize);
                        ng.SetClip(gp);
                    }

                    _noiseBrush.TranslateTransform(-circle.X, -circle.Y);
                    ng.FillRectangle(_noiseBrush, 0, 0, hoverSize, hoverSize);
                    _noiseBrush.ResetTransform();
                }

                BitmapData stampData = _stamp.LockBits(new Rectangle(0, 0, hoverSize, hoverSize), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                BitmapData noiseData = noiseBmp.LockBits(new Rectangle(0, 0, hoverSize, hoverSize), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                float clampedStrength = Math.Max(0f, Math.Min(1f, noiseStrength));

                unsafe
                {
                    byte* sp = (byte*)stampData.Scan0;
                    byte* np = (byte*)noiseData.Scan0;
                    int total = hoverSize * hoverSize;

                    for (int i = 0; i < total; i++)
                    {
                        int idx = i * 4;
                        int baseAlpha = _radialAlpha[i] * color.A / 255;
                        int noiseLum = (np[idx] + np[idx + 1] + np[idx + 2]) / 3;
                        int noiseAlpha = (int)(noiseLum * _radialAlpha[i] / 255f * clampedStrength);
                        sp[idx + 3] = (byte)Math.Min(baseAlpha + noiseAlpha, 255);
                    }
                }

                _stamp.UnlockBits(stampData);
                noiseBmp.UnlockBits(noiseData);
            }

            G.DrawImage(_stamp, circle);
            G.ResetClip();
        }

        /// <summary>
        /// Draws a circular hover highlight at the current cursor position onto the specified Graphics, clipped to the provided path.
        /// </summary>
        /// <param name="G">Graphics surface used for rendering the hover highlight.</param>
        /// <param name="host">Control used to translate screen coordinates to client coordinates for the cursor position.</param>
        /// <param name="path">GraphicsPath that specifies the clipping region for the hover effect.</param>
        /// <param name="color">Base color of the hover; its alpha is combined with a radial alpha mask to produce the final transparency.</param>
        /// <param name="hoverSize">Diameter in pixels of the hover circle.</param>
        /// <param name="clipHeightOffset">Optional number of pixels to subtract from the path's bounding height before applying the clip.</param>
        public static void DrawHover(this Graphics G, Control host, GraphicsPath path, Color color, int hoverSize, int clipHeightOffset = 0, float noiseStrength = -1f)
        {
            Point clientMousePos = host.PointToClient(Cursor.Position);
            Rectangle circle = new(clientMousePos.X - hoverSize / 2, clientMousePos.Y - hoverSize / 2, hoverSize, hoverSize);
            if (noiseStrength == -1f) noiseStrength = _noiseStrength;

            using (GraphicsPath clipPath = new())
            {
                RectangleF bounds = path.GetBounds();
                clipPath.AddRectangle(new RectangleF(bounds.X, bounds.Y, bounds.Width, bounds.Height - clipHeightOffset));

                using (Region clipRegion = new(path))
                {
                    clipRegion.Intersect(clipPath.GetBounds());
                    DrawHoverCore(G, clipRegion, circle, color, noiseStrength);
                }
            }
        }

        /// <summary>
        /// Draws a circular hover highlight with noise texture at the specified position, clipped to the control bounds.
        /// </summary>
        /// <param name="G">Graphics surface used for rendering the hover highlight.</param>
        /// <param name="bounds">Bounding rectangle of the control; determines the clip region shape.</param>
        /// <param name="hoverRect">Rectangle that defines the position and size of the hover circle.</param>
        /// <param name="hoverPosition">Center point of the hover circle; used to anchor the noise texture.</param>
        /// <param name="color">Base color of the hover; its alpha is combined with the radial alpha mask.</param>
        public static void DrawHover(this Graphics G, Rectangle bounds, Rectangle hoverRect, Point hoverPosition, Color color, float noiseStrength = -1f)
        {
            int hoverSize = Math.Max(hoverRect.Width, hoverRect.Height);
            Rectangle circle = new(hoverPosition.X - hoverSize / 2, hoverPosition.Y - hoverSize / 2, hoverSize, hoverSize);
            if (noiseStrength == -1f) noiseStrength = _noiseStrength;

            GraphicsPath path = Program.Style.RoundedCorners ? bounds.Round() : new();
            if (!Program.Style.RoundedCorners) { path.AddRectangle(bounds); }

            using (Region clipRegion = new(path))
            {
                DrawHoverCore(G, clipRegion, circle, color, noiseStrength);
            }

            path.Dispose();
        }

        /// <summary>
        /// Determines whether the specified point is inside the given polygon.
        /// </summary>
        /// <remarks>The polygon is assumed to be a closed shape, and the vertices should be provided in order (either
        /// clockwise or counterclockwise). If the polygon is null or contains fewer than three vertices, the method returns
        /// <see langword="false"/>.</remarks>
        /// <param name="polygon">An array of <see cref="PointF"/> structures representing the vertices of the polygon. The polygon must have at least
        /// three vertices.</param>
        /// <param name="point">The <see cref="PointF"/> to test for containment within the polygon.</param>
        /// <returns><see langword="true"/> if the specified point is inside the polygon; otherwise, <see langword="false"/>.</returns>
        public static bool Contains(this PointF[] polygon, PointF point)
        {
            if (polygon == null || polygon.Length < 3) return false;
            bool inside = false;

            for (int i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++)
            {
                bool intersect = ((polygon[i].Y > point.Y) != (polygon[j].Y > point.Y)) && (point.X < (polygon[j].X - polygon[i].X) * (point.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X);
                if (intersect) inside = !inside;
            }

            return inside;
        }

        /// <summary>
        /// Determines whether the specified point is inside the given polygon.
        /// </summary>
        /// <remarks>The method uses the ray-casting algorithm to determine whether the point is inside the polygon.  The
        /// polygon is assumed to be a closed shape, and the vertices should be provided in order (either clockwise or
        /// counterclockwise). If the polygon is <see langword="null"/> or contains fewer than three vertices, the method
        /// returns <see langword="false"/>.</remarks>
        /// <param name="polygon">An array of <see cref="Point"/> structures representing the vertices of the polygon.  The polygon must have at least
        /// three vertices.</param>
        /// <param name="point">A <see cref="PointF"/> structure representing the point to test for containment within the polygon.</param>
        /// <returns><see langword="true"/> if the specified point is inside the polygon; otherwise, <see langword="false"/>.</returns>
        public static bool Contains(this Point[] polygon, PointF point)
        {
            if (polygon == null || polygon.Length < 3) return false;

            bool inside = false;

            for (int i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++)
            {
                bool intersect = ((polygon[i].Y > point.Y) != (polygon[j].Y > point.Y)) && (point.X < (polygon[j].X - polygon[i].X) * (point.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X);
                if (intersect) inside = !inside;
            }

            return inside;
        }

        /// <summary>
        /// Determines whether the specified <paramref name="targetRect"/> is fully contained within the boundaries of the
        /// <paramref name="parentRect"/> without exceeding them.
        /// </summary>
        /// <remarks>This method checks that all edges of the <paramref name="targetRect"/> (left, top, right, and bottom)
        /// are within the corresponding edges of the <paramref name="parentRect"/>.</remarks>
        /// <param name="parentRect">The bounds that serves as the boundary for containment.</param>
        /// <param name="targetRect">The bounds to check for containment within the <paramref name="parentRect"/>.</param>
        /// <returns><see langword="true"/> if the <paramref name="targetRect"/> is fully contained within the <paramref
        /// name="parentRect"/>; otherwise, <see langword="false"/>.</returns>
        public static bool Contains_ButNotExceed(this Rectangle parentRect, Rectangle targetRect)
        {
            return targetRect.Left >= parentRect.Left && targetRect.Top >= parentRect.Top && targetRect.Right <= parentRect.Right && targetRect.Bottom <= parentRect.Bottom;
        }

        /// <summary>
        /// Determines whether the specified point lies within the border of the bounds.
        /// </summary>
        /// <remarks>A point is considered to be within the border if it lies inside the bounds but outside an inner
        /// bounds that is shrunk inward by the specified border width.</remarks>
        /// <param name="rectangleF">The bounds to check against.</param>
        /// <param name="pointToCheck">The point to evaluate.</param>
        /// <param name="borderWidth">The width of the border to consider, in pixels. Must be greater than 0. Defaults to 1.</param>
        /// <returns><see langword="true"/> if the point is within the border of the bounds; otherwise, <see langword="false"/>.</returns>
        public static bool BordersContains(this RectangleF rectangleF, PointF pointToCheck, float borderWidth = 1f)
        {
            if (borderWidth <= 0f) return false;

            // Inflate inward to shrink the bounds by the border width
            RectangleF inner = RectangleF.Inflate(rectangleF, -borderWidth, -borderWidth);

            // If the point is inside the bounds but outside the inner shrunken bounds, it's on the border
            return rectangleF.Contains(pointToCheck) && !inner.Contains(pointToCheck);
        }

        /// <summary>
        /// Determines whether the specified point lies within the border of the bounds.
        /// </summary>
        /// <remarks>A point is considered to be within the border if it lies inside the bounds but outside an inner
        /// bounds that is shrunk inward by the specified border width.</remarks>
        /// <param name="rectangleF">The bounds to check against.</param>
        /// <param name="pointToCheck">The point to evaluate.</param>
        /// <param name="borderWidth">The width of the border to consider, in pixels. Must be greater than 0. Defaults to 1.</param>
        /// <returns><see langword="true"/> if the point is within the border of the bounds; otherwise, <see langword="false"/>.</returns>
        public static bool BordersContains(this RectangleF rectangleF, Point pointToCheck, float borderWidth = 1f)
        {
            return BordersContains(rectangleF, new PointF(pointToCheck.X, pointToCheck.Y), borderWidth);
        }


        /// <summary>
        /// Determines whether the specified point lies within the border of the bounds.
        /// </summary>
        /// <remarks>A point is considered to be within the border if it lies inside the bounds but outside an inner
        /// bounds that is shrunk inward by the specified border width.</remarks>
        /// <param name="rectangle">The bounds to check against.</param>
        /// <param name="pointToCheck">The point to evaluate.</param>
        /// <param name="borderWidth">The width of the border to consider, in pixels. Must be greater than 0. Defaults to 1.</param>
        /// <returns><see langword="true"/> if the point is within the border of the bounds; otherwise, <see langword="false"/>.</returns>
        public static bool BordersContains(this Rectangle rectangle, Point pointToCheck, float borderWidth = 1f)
        {
            return BordersContains(rectangle, new PointF(pointToCheck.X, pointToCheck.Y), borderWidth);
        }

        /// <summary>
        /// Returns a new <see cref="Rectangle"/> that is inflated by the specified amount in all directions.
        /// </summary>
        /// <remarks>The inflation is applied symmetrically to all sides of the bounds. A positive
        /// <paramref name="amount"/> increases the size of the bounds, while a negative value decreases it. The
        /// original bounds remains unchanged.</remarks>
        /// <param name="rect">The original <see cref="Rectangle"/> to be inflated.</param>
        /// <param name="amount">The amount, in pixels, by which to inflate the bounds. This value is added to both the width and height,
        /// and subtracted from the X and Y coordinates.</param>
        /// <returns>A new <see cref="Rectangle"/> that is larger or smaller than the original, depending on the value of
        /// <paramref name="amount"/>.</returns>
        public static Rectangle InflateReturn(this Rectangle rect, int amount)
        {
            return new Rectangle(rect.X - amount, rect.Y - amount, rect.Width + amount * 2, rect.Height + amount * 2);
        }

        /// <summary>
        /// Returns a new <see cref="Rectangle"/> that is inflated by the specified horizontal and vertical amounts.
        /// </summary>
        /// <remarks>The method adjusts the position and size of the bounds by subtracting <paramref
        /// name="dx"/> and <paramref name="dy"/>  from the X and Y coordinates, respectively, and adding twice the
        /// values of <paramref name="dx"/> and <paramref name="dy"/>  to the width and height. Negative values for
        /// <paramref name="dx"/> or <paramref name="dy"/> will shrink the bounds.</remarks>
        /// <param name="rect">The original <see cref="Rectangle"/> to be inflated.</param>
        /// <param name="dx">The amount to inflate the bounds horizontally. Can be negative to deflate.</param>
        /// <param name="dy">The amount to inflate the bounds vertically. Can be negative to deflate.</param>
        /// <returns>A new <see cref="Rectangle"/> that is larger or smaller than the original, depending on the specified
        /// inflation values.</returns>
        public static Rectangle InflateReturn(this Rectangle rect, int dx, int dy)
        {
            return new Rectangle(rect.X - dx, rect.Y - dy, rect.Width + dx * 2, rect.Height + dy * 2);
        }

        public enum TabStyle
        {
            /// <summary>Top corners rounded, flat bottom (standard tab)</summary>
            Rounded,
            /// <summary>Top corners sharp, flat bottom</summary>
            Sharp
        }

        /// <summary>
        /// Creates a <see cref="GraphicsPath"/> representing a tab shape based on the specified bounds, corner radius, and style.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="radius"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static GraphicsPath ToTabPath(this Rectangle rectangle, int radius = -1, TabStyle style = TabStyle.Rounded)
        {
            if (radius == -1) radius = Program.Style.Radius;
            if (radius == 0) style = TabStyle.Sharp;

            GraphicsPath path = new();

            switch (style)
            {
                case TabStyle.Rounded:
                    {
                        int bottom = rectangle.Bottom + 1;
                        int diameter = radius * 2;

                        path.AddArc(rectangle.X, rectangle.Y, diameter, diameter, 180, 90);
                        path.AddArc(rectangle.Right - diameter, rectangle.Y, diameter, diameter, 270, 90);
                        path.AddLine(rectangle.Right, rectangle.Y + radius, rectangle.Right, bottom);
                        path.AddLine(rectangle.X, bottom, rectangle.X, rectangle.Y + radius);
                        break;
                    }

                case TabStyle.Sharp:
                    {
                        int bottom = rectangle.Bottom + 1;

                        path.AddLine(rectangle.X, bottom, rectangle.X, rectangle.Y);
                        path.AddLine(rectangle.X, rectangle.Y, rectangle.Right, rectangle.Y);
                        path.AddLine(rectangle.Right, rectangle.Y, rectangle.Right, bottom);
                        break;
                    }
            }

            return path;
        }

        private static readonly Func<Brush, IntPtr> GetBrushHandle = CreateBrushGetter();
        private static readonly Func<Pen, IntPtr> GetPenHandle = CreatePenGetter();

        private static Func<Brush, IntPtr> CreateBrushGetter()
        {
            PropertyInfo pi = typeof(Brush).GetProperty("NativeBrush", BindingFlags.Instance | BindingFlags.NonPublic);

            ParameterExpression p = Expression.Parameter(typeof(Brush));
            UnaryExpression cast = Expression.Convert(Expression.Property(p, pi), typeof(IntPtr));

            return Expression.Lambda<Func<Brush, IntPtr>>(cast, p).Compile();
        }

        private static Func<Pen, IntPtr> CreatePenGetter()
        {
            PropertyInfo pi = typeof(Pen).GetProperty("NativePen", BindingFlags.Instance | BindingFlags.NonPublic);

            ParameterExpression p = Expression.Parameter(typeof(Pen));
            UnaryExpression cast = Expression.Convert(Expression.Property(p, pi), typeof(IntPtr));

            return Expression.Lambda<Func<Pen, IntPtr>>(cast, p).Compile();
        }

        public static bool IsValid(this Brush brush)
        {
            if (brush is null) return false;

            try
            {
                return GetBrushHandle(brush) != IntPtr.Zero;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValid(this Pen pen)
        {
            if (pen is null) return false;

            try
            {
                return GetPenHandle(pen) != IntPtr.Zero;
            }
            catch
            {
                return false;
            }
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

            using Graphics G = button.CreateGraphics();
            textSize = string.IsNullOrEmpty(button.Text) ? SizeF.Empty : TextRenderer.MeasureText(G, button.Text, button.Font);

            GetTextAndImageRectangles(bounds, imageSize, textSize, button.ImageAlign, button.TextAlign, button.TextImageRelation, out imageRect, out textRect);
        }

        /// <summary>
        /// Get the rectangles for text and image in a radio image button, based on its properties.
        /// </summary>
        /// <param name="button"></param>
        /// <param name="bounds"></param>
        /// <param name="imageRect"></param>
        /// <param name="textRect"></param>
        public static void GetTextAndImageRectangles(this RadioImage button, Rectangle bounds, out RectangleF imageRect, out RectangleF textRect)
        {
            Image img = button.Image;
            SizeF imageSize = img?.Size ?? SizeF.Empty;
            SizeF textSize;

            using Graphics G = button.CreateGraphics();
            textSize = string.IsNullOrEmpty(button.Text) ? SizeF.Empty : TextRenderer.MeasureText(G, button.Text, button.Font);

            GetTextAndImageRectangles(bounds, imageSize, textSize, button.ImageAlign, button.TextAlign, button.TextImageRelation, out imageRect, out textRect);
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
        public static void GetTextAndImageRectangles(RectangleF bounds, SizeF imageSize, SizeF textSize, ContentAlignment imageAlign, ContentAlignment textAlign, TextImageRelation relation, out RectangleF imageRect, out RectangleF textRect)
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

                        RectangleF imageRegion = new(totalRect.X, totalRect.Y, imageSize.Width, maxHeight);
                        RectangleF textRegion = new(imageRegion.Right + spacing, totalRect.Y, textSize.Width, maxHeight);

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

                        RectangleF textRegion = new(totalRect.X, totalRect.Y, textSize.Width, maxHeight);
                        RectangleF imageRegion = new(textRegion.Right + spacing, totalRect.Y, imageSize.Width, maxHeight);

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

                        RectangleF imageRegion = new(totalRect.X, totalRect.Y, maxWidth, imageSize.Height);
                        RectangleF textRegion = new(totalRect.X, imageRegion.Bottom + spacing, maxWidth, textSize.Height);

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

                        RectangleF textRegion = new(totalRect.X, totalRect.Y, maxWidth, textSize.Height);
                        RectangleF imageRegion = new(totalRect.X, textRegion.Bottom + spacing, maxWidth, imageSize.Height);

                        imageRect = AlignIn(imageRegion, imageSize, imageAlign);
                        textRect = AlignIn(textRegion, textSize, textAlign);
                    }
                    break;
            }
        }

        /// <summary>
        /// Aligns the content of given size inside the container bounds based on alignment.
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
        public static Bitmap CaptureFromScreen(Rectangle screenRect)
        {
            Bitmap bmp = new(screenRect.Width, screenRect.Height, PixelFormat.Format32bppArgb);
            using Graphics G = Graphics.FromImage(bmp);
            G.CopyFromScreen(screenRect.Left, screenRect.Top, 0, 0, screenRect.Size, CopyPixelOperation.SourceCopy);
            return bmp;
        }
    }
}