using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
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
        /// <param name="clientRectangle">The rectangle that defines the area where the glowing text will be drawn.</param>
        /// <param name="rectangle">The rectangle that specifies the layout of the text within the <paramref name="clientRectangle"/>.</param>
        /// <param name="format">The <see cref="StringFormat"/> that specifies text layout information, such as alignment and line spacing. Cannot be
        /// null.</param>
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
        /// Draws a glowing effect around the specified rectangle on the provided <see cref="Graphics"/> surface.
        /// </summary>
        /// <remarks>This method creates a glowing effect around the specified rectangle by drawing a semi-transparent
        /// glow with the specified color, size, and fade factor. The glow can optionally follow a rounded rectangle
        /// shape.</remarks>
        /// <param name="G">The <see cref="Graphics"/> object on which the glow effect will be drawn. Cannot be <see langword="null"/>.</param>
        /// <param name="rectangle">The <see cref="Rectangle"/> around which the glow effect will be applied. Must have a positive width and height.</param>
        /// <param name="glowColor">The <see cref="Color"/> of the glow effect.</param>
        /// <param name="glowSize">The size of the glow effect in pixels. Defaults to 5. Must be greater than 0.</param>
        /// <param name="glowFade">The fade factor of the glow effect, which determines the smoothness of the glow. Defaults to 7. Must be greater than
        /// 0.</param>
        /// <param name="rounded">A value indicating whether the glow effect should follow a rounded rectangle shape. If <see langword="true"/>, the
        /// glow will be applied to a rounded rectangle; otherwise, it will follow the standard rectangle shape.</param>
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
        /// Draws an Aero-style visual effect on the specified graphics surface within the given rectangle.
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
        /// <param name="radius">The corner radius for rounded rectangles. Ignored if <paramref name="roundedCorners"/> is <see langword="false"/>.</param>
        /// <param name="roundedCorners">A <see langword="bool"/> indicating whether the effect should use rounded corners. If <see langword="true"/>,
        /// rounded corners are applied.</param>
        /// <summary>
        /// Draws an emulated Windows Vista/7 Aero effect preview.
        /// </summary>
        public static void DrawAeroEffect(this Graphics G, Rectangle rect, Bitmap backgroundBlurred, Color colorizationColor, float colorBalance, Color glowColor, float glowBalance, float alpha, int radius, bool roundedCorners)
        {
            if (G is null) return;
            if (rect.Width <= 0 || rect.Height <= 0) return;
            if (backgroundBlurred is null) return;

            // Clamp values inline (no Math.Clamp available)
            if (colorBalance < 0f) colorBalance = 0f;
            if (colorBalance > 1f) colorBalance = 1f;
            if (glowBalance < 0f) glowBalance = 0f;
            if (glowBalance > 1f) glowBalance = 1f;
            if (alpha < 0f) alpha = 0f;
            if (alpha > 1f) alpha = 1f;

            Rectangle innerRect = new(rect.X, rect.Y, rect.Width - 1, rect.Height - 1);

            // Set smoothing for rounded corners
            if (roundedCorners && radius > 0)
            {
                using (GraphicsPath path = innerRect.Round(radius))
                {
                    G.SetClip(path);
                    RenderAeroLayers(G, rect, backgroundBlurred, colorizationColor, colorBalance, glowColor, glowBalance, alpha);
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
        private static void RenderAeroLayers(Graphics G, Rectangle rect, Bitmap backgroundBlurred, Color colorizationColor, float colorBalance, Color glowColor, float glowBalance, float alpha)
        {
            // Base blurred background
            G.DrawImage(backgroundBlurred, rect);

            // --- Colorization overlay (tint) ---
            if (colorBalance > 0f)
            {
                int tintAlpha = (int)(alpha * colorBalance * 255);
                using (SolidBrush tintBrush = new(Color.FromArgb(tintAlpha, colorizationColor)))
                {
                    G.FillRectangle(tintBrush, rect);
                }
            }

            // --- Subtle darkening layer (DWM adds faint shadow) ---
            if (alpha > 0f)
            {
                int shadowAlpha = (int)(alpha * 60); // ~24% opacity
                using (SolidBrush shadowBrush = new(Color.FromArgb(shadowAlpha, Color.Black)))
                {
                    G.FillRectangle(shadowBrush, rect);
                }
            }

            // --- Glow overlay (soft highlight, usually near text or edges) ---
            if (glowBalance > 0f)
            {
                int glowAlpha = (int)(alpha * glowBalance * 180); // softer intensity
                using (SolidBrush glowBrush = new(Color.FromArgb(glowAlpha, glowColor)))
                {
                    G.FillRectangle(glowBrush, rect);
                }
            }

            // --- White highlight (faint top-light effect, optional) ---
            int whiteAlpha = (int)((1f - alpha) * 40); // subtle top-glass shine
            if (whiteAlpha > 0)
            {
                using (LinearGradientBrush whiteBrush = new(
                           new Point(rect.Left, rect.Top),
                           new Point(rect.Left, rect.Bottom),
                           Color.FromArgb(whiteAlpha, Color.White),
                           Color.Transparent))
                {
                    G.FillRectangle(whiteBrush, rect);
                }
            }
        }

        /// <summary>
        /// Creates a rounded rectangle as a <see cref="GraphicsPath"/> with the specified corner radius.
        /// </summary>
        /// <param name="rectangle">The <see cref="Rectangle"/> to round.</param>
        /// <param name="radius">The radius of the corners. If set to -1, the radius will default to half the smaller dimension of the rectangle.</param>
        /// <returns>A <see cref="GraphicsPath"/> representing the rounded rectangle.</returns>
        public static GraphicsPath Round(this Rectangle rectangle, int radius = -1) => Round(new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height), radius);

        /// <summary>
        /// Creates a <see cref="GraphicsPath"/> representing a rectangle with rounded corners.
        /// </summary>
        /// <remarks>This method adjusts the shape of the rectangle based on its dimensions: <list
        /// type="bullet"> <item>If the rectangle is too narrow or short, the result may be a capsule shape.</item>
        /// <item>If the radius is zero or the rectangle's dimensions are too small, the result will be a standard
        /// rectangle.</item> </list></remarks>
        /// <param name="rectangle">The <see cref="RectangleF"/> that defines the bounds of the shape.</param>
        /// <param name="radius">The radius of the rounded corners. If negative, a default radius is used. The radius is clamped to ensure it
        /// does not exceed half the width or height of the rectangle.</param>
        /// <returns>A <see cref="GraphicsPath"/> representing the rounded rectangle. If the rectangle's dimensions are too small
        /// or the radius is zero, the path will represent a standard rectangle.</returns>
        public static GraphicsPath Round(this RectangleF rectangle, int radius = -1)
        {
            GraphicsPath path = new();

            if (rectangle.Width <= 0 || rectangle.Height <= 0) return path;

            if (radius < 0) radius = Program.Style.Radius;

            float width = rectangle.Width;
            float height = rectangle.Height;

            if (width <= 1f || height <= 1f)
            {
                path.AddRectangle(rectangle);
                return path;
            }

            float r = Math.Min(radius, Math.Min(width, height) / 2f);
            if (r <= 0f)
            {
                path.AddRectangle(rectangle);
                return path;
            }

            float left = rectangle.Left;
            float top = rectangle.Top;
            float right = rectangle.Right;
            float bottom = rectangle.Bottom;
            float d = r * 2f;

            // Capsule vertically (very narrow width)
            if (width <= r)
            {
                path.AddArc(left, top, width, height, 90, 180);    // left half
                path.AddArc(left, top, width, height, 270, 180);   // right half
                path.CloseFigure();
                return path;
            }

            // Capsule horizontally (very short height)
            if (height <= r)
            {
                path.AddArc(left, top, width, height, 180, 180);   // top half
                path.AddArc(left, top, width, height, 0, 180);     // bottom half
                path.CloseFigure();
                return path;
            }

            // Normal rounded rectangle
            path.AddArc(left, top, d, d, 180, 90);              // top-left
            path.AddArc(right - d, top, d, d, 270, 90);         // top-right
            path.AddArc(right - d, bottom - d, d, d, 0, 90);    // bottom-right
            path.AddArc(left, bottom - d, d, d, 90, 90);        // bottom-left
            path.CloseFigure();

            return path;
        }

        /// <summary>
        /// Fills a rectangle with rounded corners using the specified brush.
        /// </summary>
        /// <remarks>This method provides a convenient way to fill a rectangle with rounded corners. The radius determines
        /// the curvature of the corners,  and the <paramref name="forcedRoundCorner"/> parameter ensures rounded corners are
        /// applied even in edge cases.</remarks>
        /// <param name="G">The <see cref="Graphics"/> object used to draw the filled rounded rectangle.</param>
        /// <param name="brush">The <see cref="Brush"/> used to fill the rectangle.</param>
        /// <param name="rectangle">The <see cref="Rectangle"/> that defines the bounds of the rounded rectangle.</param>
        /// <param name="radius">The radius of the corners. If set to -1, a default radius is used. Must be non-negative or -1.</param>
        /// <param name="forcedRoundCorner">A value indicating whether to force the corners to be rounded, even if the radius is zero or the rectangle is too
        /// small.</param>
        public static void FillRoundedRect(this Graphics G, Brush brush, Rectangle rectangle, int radius = -1, bool forcedRoundCorner = false)
        {
            FillRoundedRect(G, brush, new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height), radius, forcedRoundCorner);
        }

        /// <summary>
        /// Fills a rectangle with rounded corners or a standard rectangle, depending on the specified parameters.
        /// </summary>
        /// <remarks>If rounded corners are enabled in <c>Program.Style.RoundedCorners</c> or <paramref
        /// name="forcedRoundCorner"/> is <see langword="true"/>,  the method fills the rectangle with rounded corners using the
        /// specified or default radius. Otherwise, it fills a standard rectangle.</remarks>
        /// <param name="G">The <see cref="Graphics"/> object used to draw the filled shape. Cannot be <see langword="null"/>.</param>
        /// <param name="brush">The <see cref="Brush"/> used to fill the rectangle. Must be a valid, non-<see langword="null"/> brush.</param>
        /// <param name="rectangle">The <see cref="RectangleF"/> structure that defines the bounds of the rectangle to fill. Must have positive width
        /// and height.</param>
        /// <param name="radius">The radius of the rounded corners. If set to -1, the default radius from <c>Program.Style.Radius</c> is used. 
        /// Ignored if rounded corners are not enabled and <paramref name="forcedRoundCorner"/> is <see langword="false"/>.</param>
        /// <param name="forcedRoundCorner">A value indicating whether to force the use of rounded corners, even if rounded corners are disabled in
        /// <c>Program.Style.RoundedCorners</c>.</param>
        public static void FillRoundedRect(this Graphics G, Brush brush, RectangleF rectangle, int radius = -1, bool forcedRoundCorner = false)
        {
            if (G is null || !brush.IsValid() || rectangle.IsEmpty || rectangle.Width <= 0 || rectangle.Height <= 0) return;

            if (radius == -1) radius = Program.Style.Radius;

            if ((Program.Style.RoundedCorners || forcedRoundCorner) && radius > 0)
            {
                using (GraphicsPath path = rectangle.Round(radius))
                {
                    G?.FillPath(brush, path);
                }
            }
            else
            {
                G?.FillRectangle(brush, rectangle);
            }
        }

        /// <summary>
        /// Draws an image within a specified rectangle, optionally applying rounded corners.
        /// </summary>
        /// <remarks>If rounded corners are applied, the method uses anti-aliasing and high-quality rendering settings to
        /// ensure smooth edges. If rounded corners are not applied, the image is drawn directly within the specified
        /// rectangle.</remarks>
        /// <param name="G">The <see cref="Graphics"/> object used to draw the image. Cannot be <see langword="null"/>.</param>
        /// <param name="image">The <see cref="Image"/> to be drawn. Cannot be <see langword="null"/>.</param>
        /// <param name="rectangle">The <see cref="Rectangle"/> that defines the bounds in which the image will be drawn. Must have positive width and
        /// height.</param>
        /// <param name="radius">The radius of the rounded corners, in pixels. If set to -1, a default radius is used.  Ignored if rounded corners
        /// are not applied.</param>
        /// <param name="forcedRoundCorner">A value indicating whether rounded corners should be applied regardless of the global style settings. If <see
        /// langword="true"/>, rounded corners are applied if <paramref name="radius"/> is greater than 0.</param>
        public static void DrawRoundImage(this Graphics G, Image image, Rectangle rectangle, int radius = -1, bool forcedRoundCorner = false)
        {
            if (G == null || image == null || rectangle.IsEmpty || rectangle.Width <= 0 || rectangle.Height <= 0) return;

            if (radius == -1) radius = Program.Style.Radius;

            bool useRounded = (Program.Style.RoundedCorners || forcedRoundCorner) && radius > 0;

            if (useRounded)
            {
                using GraphicsPath path = rectangle.Round(radius);

                // Save old quality settings
                SmoothingMode oldSmoothing = G.SmoothingMode;
                CompositingQuality oldCompositing = G.CompositingQuality;
                InterpolationMode oldInterpolation = G.InterpolationMode;
                PixelOffsetMode oldPixelOffset = G.PixelOffsetMode;

                // Save state for clipping
                GraphicsState state = G.Save();

                try
                {
                    G.SmoothingMode = SmoothingMode.AntiAlias;
                    G.CompositingQuality = CompositingQuality.HighQuality;
                    G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    G.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    G.SetClip(path, CombineMode.Replace);
                    G.DrawImage(image, rectangle);
                }
                finally
                {
                    G.ResetClip();

                    // Always restore state and quality settings
                    G.Restore(state);
                    G.SmoothingMode = oldSmoothing;
                    G.CompositingQuality = oldCompositing;
                    G.InterpolationMode = oldInterpolation;
                    G.PixelOffsetMode = oldPixelOffset;
                }
            }
            else
            {
                G.DrawImage(image, rectangle);
            }
        }

        /// <summary>
        /// Draws a rectangle with rounded corners on the specified <see cref="Graphics"/> surface.
        /// </summary>
        /// <remarks>This method provides a convenient way to draw a rounded rectangle by specifying its bounds and corner
        /// radius. If the radius is too large for the rectangle's dimensions, the corners may not appear fully
        /// rounded.</remarks>
        /// <param name="G">The <see cref="Graphics"/> object on which to draw the rounded rectangle.</param>
        /// <param name="pen">The <see cref="Pen"/> used to outline the rectangle.</param>
        /// <param name="rectangle">The <see cref="Rectangle"/> structure that defines the bounds of the rectangle.</param>
        /// <param name="radius">The radius of the rounded corners. If set to -1, a default radius is used.  Must be a non-negative value or -1.</param>
        /// <param name="forcedRoundCorner">A value indicating whether to force the corners to be rounded, even if the radius is zero or smaller than the
        /// rectangle's dimensions.</param>
        public static void DrawRoundedRect(this Graphics G, Pen pen, Rectangle rectangle, int radius = -1, bool forcedRoundCorner = false)
        {
            DrawRoundedRect(G, pen, new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height), radius, forcedRoundCorner);
        }

        /// <summary>
        /// Draws a rectangle with optional rounded corners on the specified <see cref="Graphics"/> surface.
        /// </summary>
        /// <remarks>If the radius is greater than 0 and rounded corners are enabled (either globally or via <paramref
        /// name="forcedRoundCorner"/>), the rectangle is drawn with rounded corners. Otherwise, a standard rectangle with sharp
        /// corners is drawn.</remarks>
        /// <param name="G">The <see cref="Graphics"/> object on which to draw the rectangle. Cannot be <see langword="null"/>.</param>
        /// <param name="pen">The <see cref="Pen"/> used to draw the rectangle. Must be a valid, non-null pen.</param>
        /// <param name="rectangle">The <see cref="RectangleF"/> structure that defines the bounds of the rectangle to draw. Must have positive width
        /// and height.</param>
        /// <param name="radius">The radius of the rounded corners, in pixels. If set to -1, a default radius is used. A value of 0 or less results
        /// in a rectangle with sharp corners.</param>
        /// <param name="forcedRoundCorner">A <see langword="bool"/> value indicating whether to force rounded corners, even if the global style does not
        /// specify rounded corners.</param>
        public static void DrawRoundedRect(this Graphics G, Pen pen, RectangleF rectangle, int radius = -1, bool forcedRoundCorner = false)
        {
            if (G is null || !pen.IsValid() || rectangle.IsEmpty || rectangle.Width <= 0 || rectangle.Height <= 0) return;

            if (radius == -1) radius = Program.Style.Radius;

            if ((Program.Style.RoundedCorners | forcedRoundCorner) & radius > 0)
            {
                using (GraphicsPath path = rectangle.Round(radius))
                {
                    G?.DrawPath(pen, path);
                }
            }
            else
            {
                G?.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            }
        }

        /// <summary>
        /// Draws a rounded rectangle with an optional beveled edge effect.
        /// </summary>
        /// <remarks>This method draws a rectangle with rounded corners if enabled, and optionally applies a beveled edge
        /// effect along the top or bottom edge. The bevel color is derived from the base color of the <paramref name="pen"/>
        /// and adjusted based on the current style's dark mode setting.</remarks>
        /// <param name="G">The <see cref="Graphics"/> object used to draw the rectangle. Cannot be <see langword="null"/>.</param>
        /// <param name="pen">The <see cref="Pen"/> used to draw the rectangle. Must be valid and non-<see langword="null"/>.</param>
        /// <param name="rectangle">The <see cref="Rectangle"/> that defines the bounds of the rounded rectangle. Must have positive width and height.</param>
        /// <param name="radius">The radius of the rounded corners. If set to -1, the default radius from the current style is used.  The radius is
        /// clamped to half the width or height of the rectangle to prevent overlapping arcs.</param>
        /// <param name="forcedRoundCorner">A value indicating whether rounded corners should be forced, even if the current style does not enable them.</param>
        /// <param name="reverseBevel">A value indicating whether the bevel effect should be reversed. If <see langword="true"/>, the bevel is drawn on the
        /// opposite edge.</param>
        public static void DrawRoundedRectBeveled(this Graphics G, Pen pen, Rectangle rectangle, int radius = -1, bool forcedRoundCorner = false, bool reverseBevel = false)
        {
            if (G is null || !pen.IsValid() || rectangle.IsEmpty || rectangle.Width <= 0 || rectangle.Height <= 0) return;

            bool dark = Program.Style.DarkMode;
            bool useRoundedCorners = Program.Style.RoundedCorners || forcedRoundCorner;
            if (radius == -1) radius = Program.Style.Radius;

            // Clamp radius to half of width/height to avoid overlapping arcs
            radius = Math.Min(radius, Math.Min(rectangle.Width, rectangle.Height) / 2);

            Color baseColor = pen.Brush is LinearGradientBrush lgb ? lgb.LinearColors[0] : pen.Color;
            Color bevelColor = baseColor.CB(dark ? 0.09f : -0.08f);

            bool drawTop = dark ^ reverseBevel;

            // Create a path for rounded rectangle
            using GraphicsPath path = useRoundedCorners ? rectangle.Round(radius) : new GraphicsPath() { };
            if (!useRoundedCorners) path.AddRectangle(rectangle);

            // Draw the main rounded rectangle
            G.DrawPath(pen, path);

            if (path != null && path.PointCount > 0 && pen is not null)
            {
                // Draw the bevel effect along top or bottom edge
                float bevelY = drawTop ? rectangle.Top : rectangle.Bottom;
                float halfRadius = radius / 2f;
                float x1 = rectangle.Left + radius;
                float x2 = rectangle.Right - radius;

                using LinearGradientBrush bevelBrush = new(new RectangleF(rectangle.Left, bevelY - 1, rectangle.Width, 2), bevelColor, Color.Transparent, 90f)
                {
                    InterpolationColors = new ColorBlend(3)
                    {
                        Colors = [Color.Transparent, bevelColor, Color.Transparent],
                        Positions = [0f, 0.5f, 1f]
                    }
                };

                using Pen bevelPen = new(bevelBrush, pen.Width)
                {
                    DashStyle = pen.DashStyle,
                    DashOffset = pen.DashOffset
                };

                G.DrawLine(bevelPen, x1, bevelY, x2, bevelY);
            }
        }


        /// <summary>
        /// Draws a beveled rectangle with rounded corners in reverse bevel style.
        /// </summary>
        /// <remarks>This method draws a rectangle with rounded corners and a reverse bevel effect. The reverse bevel
        /// style creates an inverted appearance compared to a standard beveled rectangle. The method delegates the drawing to
        /// an internal implementation with the reverse bevel flag enabled.</remarks>
        /// <param name="G">The <see cref="Graphics"/> object used to draw the rectangle.</param>
        /// <param name="pen">The <see cref="Pen"/> used to outline the rectangle.</param>
        /// <param name="rectangle">The <see cref="Rectangle"/> that defines the bounds of the rectangle to be drawn.</param>
        /// <param name="radius">The radius of the rounded corners. If set to -1, a default radius is used. Must be non-negative or -1.</param>
        /// <param name="forcedRoundCorner">A value indicating whether to force the corners to be rounded, even if the radius is zero or the rectangle is too
        /// small.</param>
        public static void DrawRoundedRectBeveledReverse(this Graphics G, Pen pen, Rectangle rectangle, int radius = -1, bool forcedRoundCorner = false)
        {
            DrawRoundedRectBeveled(G, pen, rectangle, radius, forcedRoundCorner, true);
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
        /// <param name="parentRect">The rectangle that serves as the boundary for containment.</param>
        /// <param name="targetRect">The rectangle to check for containment within the <paramref name="parentRect"/>.</param>
        /// <returns><see langword="true"/> if the <paramref name="targetRect"/> is fully contained within the <paramref
        /// name="parentRect"/>; otherwise, <see langword="false"/>.</returns>
        public static bool Contains_ButNotExceed(this Rectangle parentRect, Rectangle targetRect) => targetRect.Left >= parentRect.Left && targetRect.Top >= parentRect.Top && targetRect.Right <= parentRect.Right && targetRect.Bottom <= parentRect.Bottom;

        /// <summary>
        /// Determines whether the specified point lies within the border of the rectangle.
        /// </summary>
        /// <remarks>A point is considered to be within the border if it lies inside the rectangle but outside an inner
        /// rectangle that is shrunk inward by the specified border width.</remarks>
        /// <param name="rectangle">The rectangle to check against.</param>
        /// <param name="pointToCheck">The point to evaluate.</param>
        /// <param name="borderWidth">The width of the border to consider, in pixels. Must be greater than 0. Defaults to 1.</param>
        /// <returns><see langword="true"/> if the point is within the border of the rectangle; otherwise, <see langword="false"/>.</returns>
        public static bool BordersContains(this Rectangle rectangle, Point pointToCheck, float borderWidth = 1f)
        {
            if (borderWidth <= 0f) return false;

            // Inflate inward to shrink the rectangle by the border width
            RectangleF inner = RectangleF.Inflate(rectangle, -borderWidth, -borderWidth);

            // If the point is inside the rectangle but outside the inner shrunken rectangle, it's on the border
            return rectangle.Contains(pointToCheck) && !inner.Contains(pointToCheck);
        }

        /// <summary>
        /// Returns a new <see cref="Rectangle"/> that is inflated by the specified amount in all directions.
        /// </summary>
        /// <remarks>The inflation is applied symmetrically to all sides of the rectangle. A positive
        /// <paramref name="amount"/> increases the size of the rectangle, while a negative value decreases it. The
        /// original rectangle remains unchanged.</remarks>
        /// <param name="rect">The original <see cref="Rectangle"/> to be inflated.</param>
        /// <param name="amount">The amount, in pixels, by which to inflate the rectangle. This value is added to both the width and height,
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
        /// <remarks>The method adjusts the position and size of the rectangle by subtracting <paramref
        /// name="dx"/> and <paramref name="dy"/>  from the X and Y coordinates, respectively, and adding twice the
        /// values of <paramref name="dx"/> and <paramref name="dy"/>  to the width and height. Negative values for
        /// <paramref name="dx"/> or <paramref name="dy"/> will shrink the rectangle.</remarks>
        /// <param name="rect">The original <see cref="Rectangle"/> to be inflated.</param>
        /// <param name="dx">The amount to inflate the rectangle horizontally. Can be negative to deflate.</param>
        /// <param name="dy">The amount to inflate the rectangle vertically. Can be negative to deflate.</param>
        /// <returns>A new <see cref="Rectangle"/> that is larger or smaller than the original, depending on the specified
        /// inflation values.</returns>
        public static Rectangle InflateReturn(this Rectangle rect, int dx, int dy)
        {
            return new Rectangle(rect.X - dx, rect.Y - dy, rect.Width + dx * 2, rect.Height + dy * 2);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Brush"/> instance is valid and not disposed.
        /// </summary>
        /// <remarks>
        /// This method checks whether the <paramref name="brush"/> is a valid, usable instance by
        /// verifying that it is not <see langword="null"/> and that accessing its properties does not throw exceptions
        /// typically associated with disposed or invalid objects. If the <paramref name="brush"/> is disposed or invalid,
        /// the method returns <see langword="false"/>.
        /// </remarks>
        /// <param name="brush">The <see cref="Brush"/> instance to validate.</param>
        /// <returns><see langword="true"/> if the <paramref name="brush"/> is not <see langword="null"/> and has not been
        /// disposed; otherwise, <see langword="false"/>.</returns>
        public static bool IsValid(this Brush brush)
        {
            if (brush == null) return false;

            try
            {
                switch (brush)
                {
                    case SolidBrush sb:
                        _ = sb.Color; // throws if disposed
                        break;

                    case LinearGradientBrush lgb:
                        _ = lgb.Rectangle; // throws if disposed
                        break;

                    case TextureBrush tb:
                        _ = tb.Image; // throws if disposed
                        if (tb.Image == null) return false;
                        break;

                    case HatchBrush hb:
                        _ = hb.HatchStyle; // throws if disposed
                        _ = hb.ForegroundColor;
                        _ = hb.BackgroundColor;
                        break;

                    case PathGradientBrush pgb:
                        _ = pgb.CenterPoint; // throws if disposed
                        _ = pgb.SurroundColors;
                        if (pgb.SurroundColors == null || pgb.SurroundColors.Length == 0) return false;
                        break;

                    default:
                        // Generic fallback — just accessing object should be safe
                        _ = brush;
                        break;
                }
            }
            catch (ObjectDisposedException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (ExternalException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Determines whether the specified <see cref="Pen"/> instance is valid and not disposed.
        /// </summary>
        /// <remarks>
        /// This method checks whether the <paramref name="pen"/> is a valid, usable instance by
        /// verifying that it is not <see langword="null"/> and that accessing its properties does not throw exceptions
        /// typically associated with disposed or invalid objects. If the <paramref name="pen"/> is disposed or invalid,
        /// the method returns <see langword="false"/>.
        /// </remarks>
        /// <param name="pen">The <see cref="Pen"/> instance to validate.</param>
        /// <returns><see langword="true"/> if the <paramref name="pen"/> is not <see langword="null"/> and has not been
        /// disposed; otherwise, <see langword="false"/>.</returns>
        public static bool IsValid(this Pen pen)
        {
            if (pen == null) return false;

            try
            {
                // Access properties that will throw if disposed
                _ = pen.Color;
                _ = pen.Width;
                _ = pen.Alignment;

                // If pen was created from a brush, validate that brush too
                if (pen.Brush != null && !pen.Brush.IsValid())
                    return false;

                // Check dash pattern validity
                if (pen.DashStyle == System.Drawing.Drawing2D.DashStyle.Custom)
                {
                    float[] pattern = pen.DashPattern;
                    if (pattern == null || pattern.Length == 0) return false;

                    foreach (float value in pattern)
                    {
                        if (value <= 0 || float.IsNaN(value) || float.IsInfinity(value)) return false;
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (ExternalException)
            {
                return false;
            }

            return true;
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
        public static void GetTextAndImageRectangles(this RadioImage button, Rectangle bounds, out RectangleF imageRect, out RectangleF textRect)
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
        public static Bitmap CaptureFromScreen(Rectangle screenRect)
        {
            Bitmap bmp = new(screenRect.Width, screenRect.Height);
            using (Graphics G = Graphics.FromImage(bmp))
            {
                // copy from screen coordinates -> bitmap (destination origin 0,0)
                G.CopyFromScreen(screenRect.Left, screenRect.Top, 0, 0, screenRect.Size, CopyPixelOperation.SourceCopy);
            }
            return bmp;
        }
    }
}