using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using WinPaletter.Assets;
using WinPaletter.Properties;
using static WinPaletter.TypesExtensions.ColorsExtensions;

namespace WinPaletter.TypesExtensions
{
    /// <summary>
    /// Extensions for <see cref="Bitmap"/> class
    /// </summary>
    public static class BitmapExtensions
    {
        /// <summary>
        /// Calculates the average color of the pixels in the specified bitmap.
        /// </summary>
        /// <remarks>This method locks the bitmap in memory for reading and processes its pixels directly.
        /// It uses a 32bpp ARGB pixel format and calculates the average color by iterating over the pixels with the
        /// specified step size.</remarks>
        /// <param name="bitmap">The bitmap from which to calculate the average color. Must not be null, and must have a non-zero width and
        /// height.</param>
        /// <param name="step">The step size for sampling pixels. A larger step reduces the number of pixels sampled, improving performance
        /// at the cost of accuracy. Defaults to 1, meaning every pixel is sampled.</param>
        /// <returns>A <see cref="Color"/> representing the average color of the sampled pixels in the bitmap. Returns <see
        /// cref="Color.Empty"/> if the bitmap is null, has zero width or height, or if an error occurs during
        /// processing.</returns>
        public static Color AverageColor(this Bitmap bitmap, int step = 1)
        {
            if (bitmap == null || bitmap.Width == 0 || bitmap.Height == 0) return Color.Empty;

            // Clone the bitmap to avoid locking the original object being used elsewhere
            using (Bitmap clone = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format32bppArgb))
            {
                BitmapData data = null;
                try
                {
                    data = clone.LockBits(new Rectangle(0, 0, clone.Width, clone.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                    long totalR = 0, totalG = 0, totalB = 0;
                    long totalPixels = 0;

                    int stride = data.Stride;
                    int height = data.Height;
                    int width = data.Width;

                    unsafe
                    {
                        byte* scan0 = (byte*)data.Scan0;
                        for (int y = 0; y < height; y += step)
                        {
                            byte* row = scan0 + (y * stride);
                            for (int x = 0; x < width; x += step)
                            {
                                int idx = x * 4; // BGRA
                                totalB += row[idx];
                                totalG += row[idx + 1];
                                totalR += row[idx + 2];
                                totalPixels++;
                            }
                        }
                    }

                    int avgR = (int)(totalR / totalPixels);
                    int avgG = (int)(totalG / totalPixels);
                    int avgB = (int)(totalB / totalPixels);

                    return Color.FromArgb(avgR, avgG, avgB);
                }
                catch
                {
                    return Color.Empty;
                }
                finally
                {
                    if (data != null) clone.UnlockBits(data);
                }
            }
        }

        /// <summary>
        /// Applies a Gaussian blur effect to the specified bitmap image, with cancellation support.
        /// </summary>
        /// <remarks>
        /// The method normalizes the input bitmap to a 32bpp ARGB format for processing and
        /// converts it back to the original pixel format if necessary. The Gaussian blur is applied using a separable
        /// kernel for improved performance. The opacity parameter allows blending the blurred result with the original
        /// image. Cancellation can be requested via the provided <paramref name="cancellationToken"/>.
        /// </remarks>
        /// <param name="bitmap">The source <see cref="Bitmap"/> to which the Gaussian blur will be applied. Cannot be <see langword="null"/>.</param>
        /// <param name="blurPower">The intensity of the blur effect. Must be greater than 0. Higher values result in a stronger blur. Defaults to 2.0f.</param>
        /// <param name="opacity">The opacity of the blur effect, where <see langword="0.0f"/> represents fully transparent and <see langword="1.0f"/> represents fully opaque. Values outside the range [0.0f, 1.0f] will be clamped. Defaults to 1.0f.</param>
        /// <param name="cancellationToken">The token that can be used to request cancellation of the operation.</param>
        /// <returns>A new <see cref="Bitmap"/> instance with the Gaussian blur applied. Returns <see langword="null"/> if the input <paramref name="bitmap"/> is <see langword="null"/> or has zero width or height.</returns>
        /// <exception cref="OperationCanceledException">Thrown if the operation is canceled via <paramref name="cancellationToken"/>.</exception>
        public static Bitmap Blur(this Bitmap bitmap, float blurPower = 2.0f, float opacity = 1.0f, CancellationToken cancellationToken = default)
        {
            if (bitmap == null || bitmap.Width == 0 || bitmap.Height == 0) return null;

            // Clamp opacity manually
            if (opacity < 0f) opacity = 0f;
            else if (opacity > 1f) opacity = 1f;

            // Clone the original bitmap to avoid "object is being used elsewhere"
            using (Bitmap src = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format32bppArgb))
            {
                int width = src.Width;
                int height = src.Height;

                using (Bitmap blurred = new(width, height, PixelFormat.Format32bppArgb))
                {
                    // Gaussian kernel
                    float sigma = Math.Max(0.1f, blurPower);
                    int radius = (int)Math.Ceiling(3f * sigma);
                    int kSize = 2 * radius + 1;

                    float[] kernel = new float[kSize];
                    float kSum = 0f;
                    for (int i = -radius; i <= radius; i++)
                    {
                        float v = (float)Math.Exp(-(i * i) / (2f * sigma * sigma));
                        kernel[i + radius] = v;
                        kSum += v;
                    }
                    for (int i = 0; i < kSize; i++) kernel[i] /= kSum;

                    // Lock bits
                    var srcData = src.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, src.PixelFormat);
                    var dstData = blurred.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, blurred.PixelFormat);

                    int bytesPerPixel = 4;
                    int stride = srcData.Stride;
                    int bufSize = stride * height;

                    byte[] srcBuffer = new byte[bufSize];
                    byte[] tmpBuffer = new byte[bufSize];
                    byte[] finalBuffer = new byte[bufSize];

                    Marshal.Copy(srcData.Scan0, srcBuffer, 0, bufSize);
                    src.UnlockBits(srcData);

                    // Horizontal pass
                    for (int y = 0; y < height; y++)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        int row = y * stride;
                        for (int x = 0; x < width; x++)
                        {
                            float b = 0f, g = 0f, r = 0f, a = 0f, wsum = 0f;
                            for (int k = -radius; k <= radius; k++)
                            {
                                int px = x + k;
                                if (px < 0) px = 0;
                                else if (px >= width) px = width - 1;

                                int idx = row + px * bytesPerPixel;
                                float w = kernel[k + radius];
                                b += srcBuffer[idx + 0] * w;
                                g += srcBuffer[idx + 1] * w;
                                r += srcBuffer[idx + 2] * w;
                                a += srcBuffer[idx + 3] * w;
                                wsum += w;
                            }
                            int outIdx = row + x * bytesPerPixel;
                            tmpBuffer[outIdx + 0] = (byte)(b / wsum);
                            tmpBuffer[outIdx + 1] = (byte)(g / wsum);
                            tmpBuffer[outIdx + 2] = (byte)(r / wsum);
                            tmpBuffer[outIdx + 3] = (byte)(a / wsum);
                        }
                    }

                    // Vertical pass
                    for (int x = 0; x < width; x++)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        int colOffset = x * bytesPerPixel;
                        for (int y = 0; y < height; y++)
                        {
                            float b = 0f, g = 0f, r = 0f, a = 0f, wsum = 0f;
                            for (int k = -radius; k <= radius; k++)
                            {
                                int py = y + k;
                                if (py < 0) py = 0;
                                else if (py >= height) py = height - 1;

                                int idx = py * stride + colOffset;
                                float w = kernel[k + radius];
                                b += tmpBuffer[idx + 0] * w;
                                g += tmpBuffer[idx + 1] * w;
                                r += tmpBuffer[idx + 2] * w;
                                a += tmpBuffer[idx + 3] * w;
                                wsum += w;
                            }

                            int outIdx = y * stride + colOffset;
                            if (opacity < 1f)
                            {
                                finalBuffer[outIdx + 0] = (byte)(srcBuffer[outIdx + 0] * (1f - opacity) + (b / wsum) * opacity);
                                finalBuffer[outIdx + 1] = (byte)(srcBuffer[outIdx + 1] * (1f - opacity) + (g / wsum) * opacity);
                                finalBuffer[outIdx + 2] = (byte)(srcBuffer[outIdx + 2] * (1f - opacity) + (r / wsum) * opacity);
                                finalBuffer[outIdx + 3] = (byte)(srcBuffer[outIdx + 3] * (1f - opacity) + (a / wsum) * opacity);
                            }
                            else
                            {
                                finalBuffer[outIdx + 0] = (byte)(b / wsum);
                                finalBuffer[outIdx + 1] = (byte)(g / wsum);
                                finalBuffer[outIdx + 2] = (byte)(r / wsum);
                                finalBuffer[outIdx + 3] = (byte)(a / wsum);
                            }
                        }
                    }

                    // Copy final buffer
                    Marshal.Copy(finalBuffer, 0, dstData.Scan0, bufSize);
                    blurred.UnlockBits(dstData);

                    return (Bitmap)blurred.Clone();
                }
            }
        }

        /// <summary>
        /// Applies a noise effect to the specified <see cref="Bitmap"/> based on the given noise mode and opacity.
        /// </summary>
        /// <remarks>The method creates a clone of the input <see cref="Bitmap"/> and applies the
        /// specified noise effect to the clone. The <paramref name="noiseMode"/> determines the type of noise texture
        /// used, and the <paramref name="opacity"/> controls the transparency of the effect.</remarks>
        /// <param name="bitmap">The source <see cref="Bitmap"/> to which the noise effect will be applied. Cannot be <see langword="null"/>.</param>
        /// <param name="noiseMode">The type of noise effect to apply. Supported values are <see cref="NoiseMode.Acrylic"/> and <see
        /// cref="NoiseMode.Aero"/>.</param>
        /// <param name="opacity">The opacity level of the noise effect, specified as a float between 0.0 (completely transparent) and 1.0
        /// (completely opaque).</param>
        /// <returns>A new <see cref="Bitmap"/> instance with the applied noise effect, or <see langword="null"/> if the input
        /// <paramref name="bitmap"/> is <see langword="null"/>.</returns>
        public static Bitmap Noise(this Bitmap bitmap, NoiseMode noiseMode, float opacity)
        {
            if (bitmap == null) return null;

            // Clamp opacity manually
            if (opacity < 0f) opacity = 0f;
            else if (opacity > 1f) opacity = 1f;

            // Clone the original bitmap to avoid locking issues
            using (Bitmap source = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format32bppArgb))
            {
                Bitmap result = new(source.Width, source.Height, source.PixelFormat);
                result.SetResolution(source.HorizontalResolution, source.VerticalResolution);

                using (Graphics g = Graphics.FromImage(result))
                {
                    g.SmoothingMode = SmoothingMode.None;
                    g.CompositingMode = CompositingMode.SourceOver;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    // Draw original bitmap
                    g.DrawImage(source, new Rectangle(0, 0, source.Width, source.Height));

                    // Apply noise overlay
                    if (noiseMode == NoiseMode.Acrylic)
                    {
                        using (Bitmap noise = (Bitmap)Resources.Noise.Clone())
                        using (Bitmap fadedNoise = noise.Fade(opacity))
                        using (TextureBrush br = new(fadedNoise) { WrapMode = WrapMode.Tile })
                        {
                            g.FillRectangle(br, 0, 0, source.Width, source.Height);
                        }
                    }
                    else if (noiseMode == NoiseMode.Aero)
                    {
                        using (Bitmap aero = (Bitmap)Win7Preview.AeroGlass.Clone())
                        using (Bitmap fadedAero = aero.Fade(opacity))
                        {
                            g.DrawImage(fadedAero, new Rectangle(0, 0, source.Width, source.Height));
                        }
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Specifies the type of noise effect to apply in a visual style.
        /// </summary>
        /// <remarks>This enumeration defines the available noise effects, such as Aero glass reflection or the acrylic
        /// noise effect introduced in Windows 10.</remarks>
        public enum NoiseMode
        {
            /// <summary>
            /// Aero glass reflection
            /// </summary>
            Aero,
            /// <summary>
            /// Acrylic noise effect of Windows 10 and later
            /// </summary>
            Acrylic
        }

        /// <summary>
        /// Replace occurrences of a color in an image while preserving anti-aliased edges (alpha).
        /// </summary>
        /// <param name="inputImage">Input bitmap (not null/empty).</param>
        /// <param name="oldColor">Color to replace (RGB used for matching).</param>
        /// <param name="newColor">Replacement color (RGB used for new color; alpha of destination preserved).</param>
        /// <param name="tolerance">Max per-channel difference to still consider a "match" (0 = exact). Recommended 8-20.</param>
        public static Bitmap ReplaceColor(this Bitmap inputImage, Color oldColor, Color newColor, int tolerance = 16)
        {
            if (inputImage == null || inputImage.Width == 0 || inputImage.Height == 0) return null;

            using (Bitmap src = inputImage.Clone(new Rectangle(0, 0, inputImage.Width, inputImage.Height), PixelFormat.Format32bppArgb))
            {
                int w = src.Width;
                int h = src.Height;
                Bitmap dest = new(w, h, PixelFormat.Format32bppArgb);

                BitmapData srcData = null, dstData = null;
                try
                {
                    srcData = src.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                    dstData = dest.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

                    int srcStride = srcData.Stride;
                    int dstStride = dstData.Stride;
                    int srcStrideAbs = Math.Abs(srcStride);

                    int oldR = oldColor.R, oldG = oldColor.G, oldB = oldColor.B;
                    int newR = newColor.R, newG = newColor.G, newB = newColor.B;

                    unsafe
                    {
                        byte* srcBase = (byte*)srcData.Scan0;
                        byte* dstBase = (byte*)dstData.Scan0;
                        byte* srcStart = srcStride >= 0 ? srcBase : srcBase + (h - 1) * srcStrideAbs;

                        Parallel.For(0, h, y =>
                        {
                            byte* sRow = srcStart + (srcStride >= 0 ? y * srcStride : -y * srcStride);
                            byte* dRow = dstBase + y * dstStride;
                            uint* sPx = (uint*)sRow;
                            uint* dPx = (uint*)dRow;

                            for (int x = 0; x < w; x++)
                            {
                                uint v = sPx[x];
                                byte b = (byte)(v & 0xFF);
                                byte g = (byte)((v >> 8) & 0xFF);
                                byte r = (byte)((v >> 16) & 0xFF);
                                byte a = (byte)((v >> 24) & 0xFF);

                                if (a == 0)
                                {
                                    // fully transparent: keep as-is
                                    dPx[x] = v;
                                    continue;
                                }

                                if (a == 255)
                                {
                                    // opaque: compare directly to oldColor (cheap)
                                    if (Math.Abs(r - oldR) <= tolerance &&
                                        Math.Abs(g - oldG) <= tolerance &&
                                        Math.Abs(b - oldB) <= tolerance)
                                    {
                                        uint outR = (uint)newR;
                                        uint outG = (uint)newG;
                                        uint outB = (uint)newB;
                                        dPx[x] = ((uint)255 << 24) | (outR << 16) | (outG << 8) | outB;
                                    }
                                    else
                                        dPx[x] = v;

                                    continue;
                                }

                                // semi-transparent: first try premultiplied expectation (fast, integer)
                                int expR = (oldR * a + 127) / 255;
                                int expG = (oldG * a + 127) / 255;
                                int expB = (oldB * a + 127) / 255;

                                if (Math.Abs(r - expR) <= tolerance && Math.Abs(g - expG) <= tolerance && Math.Abs(b - expB) <= tolerance)
                                {
                                    // set new color premultiplied by original alpha so edges remain smooth
                                    float blendFactor = a / 255f;  // 0..1
                                    blendFactor = (float)Math.Pow(blendFactor, 0.1f); // reduce anti-alias power (adjust exponent)
                                    uint outR = (uint)(r + (newR - r) * blendFactor);
                                    uint outG = (uint)(g + (newG - g) * blendFactor);
                                    uint outB = (uint)(b + (newB - b) * blendFactor);
                                    dPx[x] = ((uint)a << 24) | (outR << 16) | (outG << 8) | outB;

                                    continue;
                                }

                                // fallback: try un-premultiplying the stored channels and comparing to oldColor (slower)
                                int unR = (r * 255 + a / 2) / a;
                                int unG = (g * 255 + a / 2) / a;
                                int unB = (b * 255 + a / 2) / a;

                                if (Math.Abs(unR - oldR) <= tolerance && Math.Abs(unG - oldG) <= tolerance && Math.Abs(unB - oldB) <= tolerance)
                                {
                                    // set new color premultiplied by original alpha so edges remain smooth
                                    float blendFactor = a / 255f;  // 0..1
                                    blendFactor = (float)Math.Pow(blendFactor, 0.1f); // reduce anti-alias power (adjust exponent)
                                    uint outR = (uint)(r + (newR - r) * blendFactor);
                                    uint outG = (uint)(g + (newG - g) * blendFactor);
                                    uint outB = (uint)(b + (newB - b) * blendFactor);
                                    dPx[x] = ((uint)a << 24) | (outR << 16) | (outG << 8) | outB;

                                    continue;
                                }

                                // not a match: copy as-is
                                dPx[x] = v;
                            }
                        });
                    }
                }
                finally
                {
                    if (srcData != null) src.UnlockBits(srcData);
                    if (dstData != null) dest.UnlockBits(dstData);
                }

                return dest;
            }
        }

        /// <summary>
        /// Resizes the specified bitmap to fill the target size while maintaining the original aspect ratio.
        /// </summary>
        /// <remarks>The method scales the input bitmap to ensure that the entire target size is filled, potentially
        /// cropping parts of the image to maintain the aspect ratio. The resulting image is centered within the target size,
        /// and any unused areas are filled with black.</remarks>
        /// <param name="bitmap">The source <see cref="Bitmap"/> to resize. If null, the method returns null.</param>
        /// <param name="targetSize">The desired size of the output bitmap. Both <see cref="Size.Width"/> and <see cref="Size.Height"/> must be greater
        /// than zero.</param>
        /// <returns>A new <see cref="Bitmap"/> resized to fill the target size while preserving the aspect ratio of the original image.
        /// If the input <paramref name="bitmap"/> is null or the <paramref name="targetSize"/> dimensions are invalid, the
        /// original bitmap is returned.</returns>
        public static Bitmap FillInSize(this Bitmap bitmap, Size targetSize)
        {
            if (bitmap == null || targetSize.Width <= 0 || targetSize.Height <= 0)
                return bitmap;

            // Clone the original bitmap to avoid "object is being used elsewhere"
            using (Bitmap src = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format32bppArgb))
            {
                // Calculate scaling factor to fill the target size while maintaining aspect ratio
                decimal scaleW = (decimal)targetSize.Width / src.Width;
                decimal scaleH = (decimal)targetSize.Height / src.Height;
                decimal scale = Math.Max(scaleW, scaleH);

                int destWidth = (int)(src.Width * scale);
                int destHeight = (int)(src.Height * scale);

                // Center position
                int offsetX = (targetSize.Width - destWidth) / 2;
                int offsetY = (targetSize.Height - destHeight) / 2;

                Bitmap result = new(targetSize.Width, targetSize.Height, PixelFormat.Format32bppArgb);
                result.SetResolution(src.HorizontalResolution, src.VerticalResolution);

                using (Graphics g = Graphics.FromImage(result))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;

                    // Fill background (optional)
                    g.Clear(Color.Black);

                    // Draw scaled bitmap centered
                    g.DrawImage(src, new Rectangle(offsetX, offsetY, destWidth, destHeight));
                }

                return result;
            }
        }

        /// <summary>
        /// Resizes a bitmap safely, automatically handling very large images by scaling in multiple steps.
        /// Chooses optimal pixel format based on alpha usage to save memory.
        /// </summary>
        /// <param name="src">Source bitmap.</param>
        /// <param name="targetWidth">Target width.</param>
        /// <param name="targetHeight">Target height.</param>
        /// <returns>Resized bitmap, or null if src is null.</returns>
        public static Bitmap Resize(this Bitmap src, int targetWidth, int targetHeight)
        {
            if (src == null) return null;
            if (targetWidth <= 0 || targetHeight <= 0) throw new ArgumentOutOfRangeException("Width and height must be greater than 0.");

            // Determine optimal pixel format
            PixelFormat format = PixelFormat.Format32bppArgb;
            bool hasAlpha = false;

            // Quick alpha check: sample few pixels
            for (int x = 0; x < src.Width && !hasAlpha; x += src.Width / 10)
                for (int y = 0; y < src.Height && !hasAlpha; y += src.Height / 10)
                {
                    Color c = src.GetPixel(x, y);
                    if (c.A < 255) hasAlpha = true;
                }

            if (!hasAlpha) format = PixelFormat.Format24bppRgb; // no alpha needed, saves 25% memory

            const int MAX_STEP_PIXELS = 2000; // maximum dimension per step

            Bitmap current = src;
            Bitmap temp = null;

            try
            {
                // Progressive scaling loop
                while (current.Width > MAX_STEP_PIXELS || current.Height > MAX_STEP_PIXELS)
                {
                    int stepWidth = current.Width > targetWidth ? Math.Max(targetWidth, current.Width / 2) : current.Width;
                    int stepHeight = current.Height > targetHeight ? Math.Max(targetHeight, current.Height / 2) : current.Height;

                    temp = new Bitmap(stepWidth, stepHeight, format);
                    temp.SetResolution(current.HorizontalResolution, current.VerticalResolution);

                    using (Graphics G = Graphics.FromImage(temp))
                    {
                        G.CompositingMode = CompositingMode.SourceOver;
                        G.CompositingQuality = CompositingQuality.HighQuality;
                        G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        G.SmoothingMode = SmoothingMode.HighQuality;
                        G.PixelOffsetMode = PixelOffsetMode.HighQuality;

                        G.DrawImage(current, new Rectangle(0, 0, stepWidth, stepHeight));
                    }

                    if (current != src) current.Dispose();

                    current = temp;
                    temp = null;
                }

                // Final resize to exact target dimensions
                Bitmap final = new Bitmap(targetWidth, targetHeight, format);
                final.SetResolution(current.HorizontalResolution, current.VerticalResolution);

                using (Graphics G = Graphics.FromImage(final))
                {
                    G.CompositingMode = CompositingMode.SourceOver;
                    G.CompositingQuality = CompositingQuality.HighQuality;
                    G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    G.SmoothingMode = SmoothingMode.HighQuality;
                    G.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    G.DrawImage(current, new Rectangle(0, 0, targetWidth, targetHeight));
                }

                if (current != src) current.Dispose();

                return final;
            }
            finally
            {
                temp?.Dispose();
            }
        }

        /// <summary>
        /// Applies a tint to the specified bitmap using the provided color.
        /// The tint multiplies the red, green, and blue components by the
        /// corresponding components of <paramref name="tintColor"/>, while
        /// preserving the original alpha channel to keep smooth edges intact.
        /// </summary>
        /// <param name="originalBitmap">The source bitmap. Cannot be null.</param>
        /// <param name="tintColor">The color whose R, G, and B values are used as multipliers (0–255).</param>
        /// <returns>A new <see cref="Bitmap"/> with the tint applied, or <c>null</c> if <paramref name="originalBitmap"/> is null.</returns>
        public static Bitmap Tint(this Bitmap originalBitmap, Color tintColor)
        {
            if (originalBitmap == null) return null;

            // Clone to ensure we can safely read the pixels
            using Bitmap clone = originalBitmap.Clone(new Rectangle(0, 0, originalBitmap.Width, originalBitmap.Height), PixelFormat.Format32bppArgb);

            Bitmap tintedBitmap = new(clone.Width, clone.Height, PixelFormat.Format32bppArgb);
            Rectangle rect = new(0, 0, clone.Width, clone.Height);

            BitmapData srcData = clone.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = tintedBitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            try
            {
                int width = srcData.Width;
                int height = srcData.Height;
                int strideSrc = srcData.Stride;
                int strideDst = dstData.Stride;

                // Normalized multipliers for R, G, B only (leave alpha alone)
                float rMul = tintColor.R / 255f;
                float gMul = tintColor.G / 255f;
                float bMul = tintColor.B / 255f;

                unsafe
                {
                    byte* srcBase = (byte*)srcData.Scan0;
                    byte* dstBase = (byte*)dstData.Scan0;

                    Parallel.For(0, height, y =>
                    {
                        byte* srcRow = srcBase + y * strideSrc;
                        byte* dstRow = dstBase + y * strideDst;

                        for (int x = 0; x < width; x++)
                        {
                            int idx = x * 4;

                            // Multiply R, G, B and round, clamp to 255
                            dstRow[idx] = (byte)Math.Min(255, srcRow[idx] * bMul); // B
                            dstRow[idx + 1] = (byte)Math.Min(255, srcRow[idx + 1] * gMul); // G
                            dstRow[idx + 2] = (byte)Math.Min(255, srcRow[idx + 2] * rMul); // R

                            // Preserve original alpha to keep anti-aliased edges
                            dstRow[idx + 3] = srcRow[idx + 3];
                        }
                    });
                }
            }
            finally
            {
                clone.UnlockBits(srcData);
                tintedBitmap.UnlockBits(dstData);
            }

            return tintedBitmap;
        }

        /// <summary>
        /// Fast opacity fade using unsafe LockBits (returns a new bitmap).
        /// - Normalizes source to 32bpp ARGB once if needed.
        /// - Uses integer math for alpha scaling (no floats in the inner loop).
        /// - Copies B,G,R as-is; scales A by opacity.
        /// </summary>
        /// <param name="source">Input bitmap (not modified)</param>
        /// <param name="opacity">0..1</param>
        /// <param name="useParallel">Parallelize across rows for large images</param>
        public static Bitmap Fade(this Bitmap source, float opacity, bool useParallel = true)
        {
            if (source == null) return null;

            // Clamp opacity
            if (opacity <= 0f)
            {
                var transparent = new Bitmap(source.Width, source.Height, PixelFormat.Format32bppArgb);
                using (var g = Graphics.FromImage(transparent))
                    g.Clear(Color.Transparent);
                return transparent;
            }

            if (opacity >= 1f)
            {
                return source.Clone(new Rectangle(0, 0, source.Width, source.Height), PixelFormat.Format32bppArgb);
            }

            // Convert opacity to 8.8 fixed-point to avoid per-pixel float multiply.
            int opQ = Math.Min(255, Math.Max(1, (int)(opacity * 256f)));

            // Always clone the source to avoid locking a bitmap in use elsewhere
            Bitmap src32 = source.PixelFormat == PixelFormat.Format32bppArgb
                ? source.Clone(new Rectangle(0, 0, source.Width, source.Height), PixelFormat.Format32bppArgb)
                : new Bitmap(source.Width, source.Height, PixelFormat.Format32bppArgb);

            if (source.PixelFormat != PixelFormat.Format32bppArgb)
            {
                using (var g = Graphics.FromImage(src32))
                {
                    g.CompositingMode = CompositingMode.SourceCopy;
                    g.DrawImageUnscaled(source, 0, 0);
                }
            }

            Bitmap dest = new(src32.Width, src32.Height, PixelFormat.Format32bppArgb);
            Rectangle rect = new(0, 0, src32.Width, src32.Height);

            BitmapData srcData = null, dstData = null;

            try
            {
                srcData = src32.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                dstData = dest.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

                int width = rect.Width;
                int height = rect.Height;
                int srcStride = srcData.Stride;
                int dstStride = dstData.Stride;

                unsafe
                {
                    byte* srcBase = (byte*)srcData.Scan0;
                    byte* dstBase = (byte*)dstData.Scan0;

                    Action<int> processRow = y =>
                    {
                        byte* srcRow = srcBase + y * srcStride;
                        byte* dstRow = dstBase + y * dstStride;

                        for (int x = 0; x < width; x++)
                        {
                            int i = x << 2;
                            dstRow[i + 0] = srcRow[i + 0]; // B
                            dstRow[i + 1] = srcRow[i + 1]; // G
                            dstRow[i + 2] = srcRow[i + 2]; // R
                            dstRow[i + 3] = (byte)((srcRow[i + 3] * opQ) >> 8); // A
                        }
                    };

                    if (useParallel && height >= 64)
                        Parallel.For(0, height, processRow);
                    else
                        for (int y = 0; y < height; y++)
                            processRow(y);
                }
            }
            finally
            {
                if (srcData != null) src32.UnlockBits(srcData);
                if (dstData != null) dest.UnlockBits(dstData);
                src32.Dispose();
            }

            return dest;
        }

        /// <summary>
        /// Converts the specified <see cref="Bitmap"/> to a grayscale image.
        /// </summary>
        /// <remarks>The method processes the image pixel by pixel, applying a weighted formula to calculate the grayscale
        /// value based on the red, green, and blue components of each pixel. The alpha channel is preserved in the resulting
        /// image. The method ensures the image is processed in a 32bpp ARGB format for compatibility and safety.</remarks>
        /// <param name="original">The original <see cref="Bitmap"/> to be converted. Must not be <see langword="null"/>.</param>
        /// <returns>A new <see cref="Bitmap"/> instance representing the grayscale version of the original image. Returns <see
        /// langword="null"/> if the <paramref name="original"/> is <see langword="null"/>.</returns>
        public static Bitmap Grayscale(this Bitmap original)
        {
            if (original == null) return null;

            // Always clone to 32bpp ARGB for safety
            Bitmap src32 = original.PixelFormat == PixelFormat.Format32bppArgb
                ? original.Clone(new Rectangle(0, 0, original.Width, original.Height), PixelFormat.Format32bppArgb)
                : new Bitmap(original.Width, original.Height, PixelFormat.Format32bppArgb);

            if (original.PixelFormat != PixelFormat.Format32bppArgb)
            {
                using (var g = Graphics.FromImage(src32))
                {
                    g.CompositingMode = CompositingMode.SourceCopy;
                    g.DrawImageUnscaled(original, 0, 0);
                }
            }

            Bitmap newBitmap = new(src32.Width, src32.Height, PixelFormat.Format32bppArgb);

            Rectangle rect = new(0, 0, src32.Width, src32.Height);
            BitmapData srcData = null, dstData = null;

            try
            {
                srcData = src32.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                dstData = newBitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

                int width = rect.Width;
                int height = rect.Height;
                int srcStride = srcData.Stride;
                int dstStride = dstData.Stride;

                const int wR = 299, wG = 587, wB = 114; // Grayscale weights ×1000

                unsafe
                {
                    byte* srcBase = (byte*)srcData.Scan0;
                    byte* dstBase = (byte*)dstData.Scan0;

                    Parallel.For(0, height, y =>
                    {
                        byte* sRow = srcBase + y * srcStride;
                        byte* dRow = dstBase + y * dstStride;

                        for (int x = 0; x < width; x++)
                        {
                            int idx = x * 4;

                            byte b = sRow[idx + 0];
                            byte g = sRow[idx + 1];
                            byte r = sRow[idx + 2];
                            byte a = sRow[idx + 3];

                            byte gray = (byte)((r * wR + g * wG + b * wB) / 1000);

                            dRow[idx + 0] = gray;
                            dRow[idx + 1] = gray;
                            dRow[idx + 2] = gray;
                            dRow[idx + 3] = a;
                        }
                    });
                }
            }
            finally
            {
                if (srcData != null) src32.UnlockBits(srcData);
                if (dstData != null) newBitmap.UnlockBits(dstData);
                src32.Dispose();
            }

            return newBitmap;
        }

        /// <summary>
        /// Creates a new <see cref="Bitmap"/> where the colors of the source image are inverted.
        /// </summary>
        /// <remarks>The method processes the image at the pixel level, inverting the red, green, and blue
        /// color channels while leaving the alpha channel unchanged. The returned image is always in the 32bpp ARGB
        /// pixel format.</remarks>
        /// <param name="source">The source <see cref="Bitmap"/> to invert. Must not be null.</param>
        /// <returns>A new <see cref="Bitmap"/> with inverted colors, or <see langword="null"/> if <paramref name="source"/> is
        /// <see langword="null"/>.</returns>
        public static Bitmap Invert(this Bitmap source)
        {
            if (source == null) return null;

            // Ensure 32bpp ARGB clone
            Bitmap clone = source.PixelFormat == PixelFormat.Format32bppArgb
                ? source.Clone(new Rectangle(0, 0, source.Width, source.Height), PixelFormat.Format32bppArgb)
                : new Bitmap(source.Width, source.Height, PixelFormat.Format32bppArgb);

            if (source.PixelFormat != PixelFormat.Format32bppArgb)
            {
                using (var g = Graphics.FromImage(clone))
                {
                    g.CompositingMode = CompositingMode.SourceCopy;
                    g.DrawImageUnscaled(source, 0, 0);
                }
            }

            Rectangle rect = new(0, 0, clone.Width, clone.Height);
            BitmapData data = null;

            try
            {
                data = clone.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

                int width = clone.Width;
                int height = clone.Height;
                int stride = data.Stride;

                unsafe
                {
                    byte* basePtr = (byte*)data.Scan0;

                    Parallel.For(0, height, y =>
                    {
                        byte* row = basePtr + y * stride;
                        for (int x = 0; x < width; x++)
                        {
                            int idx = x * 4;
                            row[idx] = (byte)(255 - row[idx]);       // Blue
                            row[idx + 1] = (byte)(255 - row[idx + 1]); // Green
                            row[idx + 2] = (byte)(255 - row[idx + 2]); // Red
                                                                       // Alpha remains unchanged
                        }
                    });
                }
            }
            finally
            {
                if (data != null) clone.UnlockBits(data);
            }

            return clone;
        }

        /// <summary>
        /// Creates a tiled version of the source bitmap that fills the specified target size.
        /// </summary>
        /// <remarks>The method creates a new bitmap of the specified size and fills it by repeating the source bitmap
        /// both horizontally and vertically. The source bitmap is converted to 32bpp ARGB format if it is not already in that
        /// format. The resulting bitmap is always in 32bpp ARGB format.</remarks>
        /// <param name="source">The source <see cref="Bitmap"/> to tile. Cannot be null.</param>
        /// <param name="targetSize">The dimensions of the resulting tiled bitmap.</param>
        /// <returns>A new <see cref="Bitmap"/> of the specified size, filled by repeating the source bitmap. Returns <see
        /// langword="null"/> if <paramref name="source"/> is <see langword="null"/>.</returns>
        public static Bitmap Tile(this Bitmap source, Size targetSize)
        {
            if (source == null) return null;

            int srcWidth = source.Width;
            int srcHeight = source.Height;
            int destWidth = targetSize.Width;
            int destHeight = targetSize.Height;

            // Always use 32bppArgb for simplicity and consistency
            Bitmap src32 = source.PixelFormat == PixelFormat.Format32bppArgb
                ? source
                : source.Clone(new Rectangle(0, 0, srcWidth, srcHeight), PixelFormat.Format32bppArgb);

            Bitmap dest = new(destWidth, destHeight, PixelFormat.Format32bppArgb);

            BitmapData srcData = null;
            BitmapData destData = null;

            try
            {
                srcData = src32.LockBits(new Rectangle(0, 0, srcWidth, srcHeight),
                                         ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                destData = dest.LockBits(new Rectangle(0, 0, destWidth, destHeight),
                                         ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

                int bytesPerPixel = 4; // 32bppArgb
                int srcStride = srcData.Stride;
                int destStride = destData.Stride;

                // Copy source rows into managed memory
                byte[][] srcRows = new byte[srcHeight][];
                for (int y = 0; y < srcHeight; y++)
                {
                    srcRows[y] = new byte[srcWidth * bytesPerPixel];
                    Marshal.Copy(
                        srcData.Scan0 + y * srcStride, srcRows[y], 0, srcWidth * bytesPerPixel);
                }

                unsafe
                {
                    byte* destPtr = (byte*)destData.Scan0;

                    Parallel.For(0, destHeight, y =>
                    {
                        byte* dstRow = destPtr + y * destStride;
                        byte[] srcRow = srcRows[y % srcHeight];

                        int fullBlocks = destWidth / srcWidth;
                        int remainder = destWidth % srcWidth;

                        for (int b = 0; b < fullBlocks; b++)
                        {
                            Marshal.Copy(srcRow, 0,
                                new IntPtr(dstRow + b * srcWidth * bytesPerPixel),
                                srcRow.Length);
                        }

                        if (remainder > 0)
                        {
                            Marshal.Copy(srcRow, 0,
                                new IntPtr(dstRow + fullBlocks * srcWidth * bytesPerPixel),
                                remainder * bytesPerPixel);
                        }
                    });
                }
            }
            finally
            {
                if (srcData != null) src32.UnlockBits(srcData);
                if (destData != null) dest.UnlockBits(destData);

                if (src32 != source) src32.Dispose();
            }

            return dest;
        }

        /// <summary>
        /// Adjusts the hue, saturation, and lightness (HSL) of the specified bitmap image.
        /// </summary>
        /// <remarks>This method creates a new bitmap with the same dimensions as the source bitmap and
        /// applies the specified HSL adjustments to each pixel. The adjustments are applied independently for hue,
        /// saturation, and lightness, and any combination of these adjustments can be applied by providing non-<see
        /// langword="null"/> values for the corresponding parameters.</remarks>
        /// <param name="bmp">The source <see cref="Bitmap"/> to adjust. Cannot be <see langword="null"/>.</param>
        /// <param name="hShift">The amount to shift the hue, in degrees. Positive values shift the hue clockwise, and negative values shift
        /// it counterclockwise. If <see langword="null"/>, the hue remains unchanged.</param>
        /// <param name="sValue">The saturation adjustment factor. A value of <c>0.5</c> leaves the saturation unchanged, values greater than
        /// <c>0.5</c> increase saturation, and values less than <c>0.5</c> decrease it. If <see langword="null"/>, the
        /// saturation remains unchanged.</param>
        /// <param name="lValue">The lightness adjustment factor. A value of <c>0.5</c> leaves the lightness unchanged, values greater than
        /// <c>0.5</c> increase lightness, and values less than <c>0.5</c> decrease it. If <see langword="null"/>, the
        /// lightness remains unchanged.</param>
        /// <returns>A new <see cref="Bitmap"/> with the adjusted HSL values. The original bitmap is not modified.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="bmp"/> is <see langword="null"/>.</exception>
        public static Bitmap AdjustHSL(this Bitmap bmp, float? hShift = null, float? sValue = null, float? lValue = null)
        {
            if (bmp == null) throw new ArgumentNullException(nameof(bmp));

            Bitmap result = new(bmp.Width, bmp.Height, PixelFormat.Format32bppArgb);
            Rectangle rect = new(0, 0, bmp.Width, bmp.Height);

            BitmapData srcData = bmp.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = result.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int width = bmp.Width, height = bmp.Height, bytesPerPixel = 4;

            unsafe
            {
                byte* srcPtr = (byte*)srcData.Scan0;
                byte* dstPtr = (byte*)dstData.Scan0;
                int strideSrc = srcData.Stride;
                int strideDst = dstData.Stride;

                Parallel.For(0, height, y =>
                {
                    byte* pSrcRow = srcPtr + y * strideSrc;
                    byte* pDstRow = dstPtr + y * strideDst;

                    for (int x = 0; x < width; x++)
                    {
                        byte b = pSrcRow[x * bytesPerPixel + 0];
                        byte g = pSrcRow[x * bytesPerPixel + 1];
                        byte r = pSrcRow[x * bytesPerPixel + 2];
                        byte a = pSrcRow[x * bytesPerPixel + 3];

                        HSL hsl = Color.FromArgb(a, r, g, b).ToHSL();

                        // Adjust Hue
                        if (hShift.HasValue)
                        {
                            hsl.H = (hsl.H + hShift.Value) % 360f;
                            if (hsl.H < 0f) hsl.H += 360f;
                        }

                        // Adjust Saturation (0.5 = neutral)
                        if (sValue.HasValue)
                        {
                            float sFactor = sValue.Value / 0.5f;
                            hsl.S *= sFactor;
                            hsl.S = Math.Min(Math.Max(hsl.S, 0f), 1f);
                        }

                        // Adjust Lightness (0.5 = neutral)
                        if (lValue.HasValue)
                        {
                            float lFactor = lValue.Value / 0.5f;
                            hsl.L *= lFactor;
                            hsl.L = Math.Min(Math.Max(hsl.L, 0f), 1f);
                        }

                        Color c = hsl.ToRGB();
                        pDstRow[x * bytesPerPixel + 0] = c.B;
                        pDstRow[x * bytesPerPixel + 1] = c.G;
                        pDstRow[x * bytesPerPixel + 2] = c.R;
                        pDstRow[x * bytesPerPixel + 3] = c.A;
                    }
                });
            }

            bmp.UnlockBits(srcData);
            result.UnlockBits(dstData);

            return result;
        }

        /// <summary>
        /// Adjusts the brightness of the specified bitmap and returns a new bitmap with the applied adjustment.
        /// </summary>
        /// <remarks>This method processes the bitmap pixel by pixel, adjusting the brightness of the red,
        /// green, and blue channels  independently. The alpha channel is preserved. The operation is performed in
        /// parallel for improved performance  on large images.</remarks>
        /// <param name="bmp">The source <see cref="Bitmap"/> to adjust. Cannot be <see langword="null"/>.</param>
        /// <param name="brightness">The brightness adjustment factor. A value of <c>0</c> applies no change. Positive values increase
        /// brightness,  while negative values decrease it. The value is clamped to the range [-1, 1].</param>
        /// <returns>A new <see cref="Bitmap"/> with the brightness adjustment applied. The original bitmap remains unchanged.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="bmp"/> is <see langword="null"/>.</exception>
        public static Bitmap Brighten(this Bitmap bmp, float brightness = 0f)
        {
            if (bmp == null) throw new ArgumentNullException(nameof(bmp));

            Bitmap result = new(bmp.Width, bmp.Height, PixelFormat.Format32bppArgb);

            BitmapData srcData = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            BitmapData dstData = result.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb);

            int bytesPerPixel = 4; // 32bppArgb

            unsafe
            {
                byte* srcPtr = (byte*)srcData.Scan0;
                byte* dstPtr = (byte*)dstData.Scan0;

                int height = bmp.Height;
                int width = bmp.Width;

                Parallel.For(0, height, y =>
                {
                    byte* srcRow = srcPtr + y * srcData.Stride;
                    byte* dstRow = dstPtr + y * dstData.Stride;

                    for (int x = 0; x < width; x++)
                    {
                        for (int i = 0; i < 3; i++) // B, G, R
                        {
                            float color = srcRow[x * bytesPerPixel + i] / 255f + brightness;
                            color = Math.Max(0f, Math.Min(1f, color));
                            dstRow[x * bytesPerPixel + i] = (byte)(color * 255);
                        }
                        dstRow[x * bytesPerPixel + 3] = srcRow[x * bytesPerPixel + 3]; // Alpha
                    }
                });
            }

            bmp.UnlockBits(srcData);
            result.UnlockBits(dstData);

            return result;
        }

        /// <summary>
        /// Adjusts the contrast of the specified <see cref="Bitmap"/> image.
        /// </summary>
        /// <remarks>This method processes the image pixel by pixel, adjusting the contrast of the red,
        /// green, and blue channels while preserving the alpha channel. The operation is performed in parallel for
        /// improved performance on large images.</remarks>
        /// <param name="bmp">The source <see cref="Bitmap"/> to adjust. Cannot be <see langword="null"/>.</param>
        /// <param name="contrast">The contrast adjustment factor. A value of <c>0</c> applies no adjustment, positive values increase
        /// contrast, and negative values decrease contrast. The value is relative and does not have a fixed range.</param>
        /// <returns>A new <see cref="Bitmap"/> with the adjusted contrast. The original <paramref name="bmp"/> is not modified.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="bmp"/> is <see langword="null"/>.</exception>
        public static Bitmap Contrast(this Bitmap bmp, float contrast = 0f)
        {
            if (bmp == null) throw new ArgumentNullException(nameof(bmp));

            Bitmap result = new(bmp.Width, bmp.Height, PixelFormat.Format32bppArgb);

            BitmapData srcData = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            BitmapData dstData = result.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb);

            int bytesPerPixel = 4;
            float cFactor = (contrast + 1f) * (contrast + 1f); // Contrast factor

            unsafe
            {
                byte* srcPtr = (byte*)srcData.Scan0;
                byte* dstPtr = (byte*)dstData.Scan0;

                int height = bmp.Height;
                int width = bmp.Width;

                Parallel.For(0, height, y =>
                {
                    byte* srcRow = srcPtr + y * srcData.Stride;
                    byte* dstRow = dstPtr + y * dstData.Stride;

                    for (int x = 0; x < width; x++)
                    {
                        for (int i = 0; i < 3; i++) // B, G, R channels
                        {
                            float color = srcRow[x * bytesPerPixel + i] / 255f;
                            color = ((color - 0.5f) * cFactor) + 0.5f;
                            color = Math.Max(0f, Math.Min(1f, color));
                            dstRow[x * bytesPerPixel + i] = (byte)(color * 255);
                        }

                        dstRow[x * bytesPerPixel + 3] = srcRow[x * bytesPerPixel + 3]; // Alpha
                    }
                });
            }

            bmp.UnlockBits(srcData);
            result.UnlockBits(dstData);

            return result;
        }

        /// <summary>
        /// Resizes the specified bitmap to the given width and height.
        /// </summary>
        /// <param name="bitmap">The bitmap to resize. Cannot be <see langword="null"/>.</param>
        /// <param name="width">The desired width of the resized bitmap, in pixels. Must be greater than 0.</param>
        /// <param name="height">The desired height of the resized bitmap, in pixels. Must be greater than 0.</param>
        /// <returns>A new <see cref="Bitmap"/> instance with the specified dimensions.</returns>
        public static Bitmap Resize(this Bitmap bitmap, Size size)
        {
            return Resize(bitmap, size.Width, size.Height);
        }

        /// <summary>
        /// Resizes the specified bitmap to the given dimensions, maintaining the aspect ratio by filling in the remaining
        /// space.
        /// </summary>
        /// <param name="bitmap">The source <see cref="Bitmap"/> to resize. Cannot be <see langword="null"/>.</param>
        /// <param name="width">The target width of the resized bitmap, in pixels. Must be greater than 0.</param>
        /// <param name="height">The target height of the resized bitmap, in pixels. Must be greater than 0.</param>
        /// <returns>A new <see cref="Bitmap"/> resized to the specified dimensions, with the original content scaled and centered to
        /// fit.</returns>
        public static Bitmap FillInSize(this Bitmap bitmap, int width, int height)
        {
            return FillInSize(bitmap, new Size(width, height));
        }

        /// <summary>
        /// Creates a tiled version of the specified bitmap with the given dimensions.
        /// </summary>
        /// <param name="bitmap">The source <see cref="Bitmap"/> to be tiled. Cannot be <see langword="null"/>.</param>
        /// <param name="width">The width of the resulting tiled image, in pixels. Must be greater than 0.</param>
        /// <param name="height">The height of the resulting tiled image, in pixels. Must be greater than 0.</param>
        /// <returns>A new <see cref="Bitmap"/> containing the tiled version of the source image.</returns>
        public static Bitmap Tile(this Bitmap bitmap, int width, int height)
        {
            return Tile(bitmap, new Size(width, height));
        }

        public class PaletteGeneratorSettings
        {
            public int ColorCount { get; set; } = 300;
            public int ColorQuality { get; set; } = 10;
            public bool IgnoreWhiteColors { get; set; } = false;
            public byte WhiteThreshold { get; set; } = 240;
            public bool SortByDominance { get; set; } = true;
            public bool DisableTransparency { get; set; } = true;
            public bool IncludeVariations { get; set; } = true;
            public IProgress<float>? Progress { get; set; }
            public IProgress<string>? Status { get; set; }
            public CancellationToken CancellationToken { get; set; } = default;
            public Color? AccentColor { get; set; } = null;
            public float AccentWeight { get; set; } = 2f;
        }

        /// <summary>
        /// Extracts a palette of dominant colors from the specified bitmap image, optionally enhanced with vivid color variations.
        /// </summary>
        public static async Task<List<Color>> ToPalette(this Bitmap bmp, PaletteGeneratorSettings settings)
        {
            if (bmp == null) throw new ArgumentNullException(nameof(bmp));
            if (settings.ColorCount < 1) throw new ArgumentOutOfRangeException(nameof(settings.ColorCount));
            if (settings.ColorQuality < 1) settings.ColorQuality = 1;

            return await Task.Run(async () =>
            {
                settings.Status?.Report($"{Program.Lang.Strings.General.SamplingPixels}...");

                using Bitmap clone = new(bmp.Width, bmp.Height, PixelFormat.Format32bppArgb);
                using (Graphics g = Graphics.FromImage(clone)) g.DrawImage(bmp, new Rectangle(0, 0, clone.Width, clone.Height));

                BitmapData data = clone.LockBits(new Rectangle(0, 0, clone.Width, clone.Height), ImageLockMode.ReadOnly, clone.PixelFormat);

                int bytesPerPixel = 4;
                int width = data.Width;
                int height = data.Height;
                int stride = data.Stride;
                List<Color> pixels = new(width * height / (settings.ColorQuality * settings.ColorQuality));

                int batchSize = 50;

                for (int yStart = 0; yStart < height; yStart += batchSize)
                {
                    settings.CancellationToken.ThrowIfCancellationRequested();

                    int yEnd = Math.Min(yStart + batchSize, height);
                    List<Color> batchPixels = new();

                    unsafe
                    {
                        byte* ptr = (byte*)data.Scan0;
                        for (int y = yStart; y < yEnd; y++)
                        {
                            if (y % settings.ColorQuality != 0) continue;

                            for (int x = 0; x < width; x += settings.ColorQuality)
                            {
                                byte* pixelPtr = ptr + y * stride + x * bytesPerPixel;
                                byte b = pixelPtr[0];
                                byte g = pixelPtr[1];
                                byte r = pixelPtr[2];
                                byte a = pixelPtr[3];

                                if (settings.IgnoreWhiteColors && r >= settings.WhiteThreshold && g >= settings.WhiteThreshold && b >= settings.WhiteThreshold)
                                    continue;

                                if (settings.DisableTransparency && a < 255) a = 255;

                                if (a > 0) batchPixels.Add(Color.FromArgb(a, r, g, b));
                            }
                        }
                    }

                    lock (pixels) pixels.AddRange(batchPixels);
                    settings.Progress?.Report((float)yEnd / height * 0.5f);
                    await Task.Delay(1, settings.CancellationToken);
                }

                clone.UnlockBits(data);

                if (pixels.Count == 0) return new List<Color>();

                settings.Status?.Report($"{Program.Lang.Strings.General.ClusteringColors}...");
                List<Color> centroids = LockFreeKMeansAsync(pixels, settings.ColorCount, settings);

                // Map all centroids toward accent color if provided
                if (settings?.AccentColor.HasValue == true)
                {
                    Color target = settings.AccentColor.Value;
                    float targetHue = target.ToHSV().H;

                    List<Color> transformed = new(centroids.Count);

                    foreach (var c in centroids)
                    {
                        (float, float, float) hsv = c.ToHSV();
                        Color newC = ColorsExtensions.HSVToColor(targetHue, hsv.Item2, hsv.Item3);
                        transformed.Add(Color.FromArgb(c.A, newC.R, newC.G, newC.B));
                    }

                    centroids = transformed;
                }

                // Sort by dominance if requested
                if (settings.SortByDominance && centroids.Count > 0)
                {
                    settings.Status?.Report($"{Program.Lang.Strings.General.SortingByDominance}...");
                    Dictionary<Color, int> colorFrequency = pixels
                        .GroupBy(px => centroids.OrderBy(c => px.Distance(c)).First())
                        .ToDictionary(g => g.Key, g => g.Count());

                    centroids = colorFrequency.OrderByDescending(kv => kv.Value).Select(kv => kv.Key).ToList();
                }

                // Include variations if requested
                if (settings.IncludeVariations && centroids.Count > 0)
                {
                    settings.Status?.Report($"{Program.Lang.Strings.General.GeneratingColorsVariations}...");
                    List<Color> strongColors = centroids
                        .OrderByDescending(c => c.GetSaturation() * (1f - Math.Abs(c.GetBrightness() - 0.5f)))
                        .Take(Math.Min(centroids.Count / 2 + 1, 10))
                        .ToList();

                    List<Color> variations = new();
                    foreach (var c in strongColors)
                    {
                        variations.Add(c.CB(0.7f));
                        variations.Add(c.CB(-0.7f));
                    }

                    Color avgTone = Color.FromArgb(
                        255,
                        (int)centroids.Average(c => c.R),
                        (int)centroids.Average(c => c.G),
                        (int)centroids.Average(c => c.B)
                    );

                    variations.Add(avgTone);
                    variations.Add(avgTone.CB(0.5f));
                    variations.Add(avgTone.CB(-0.5f));

                    centroids.AddRange(variations);
                    centroids = centroids.GroupBy(c => c.ToArgb()).Select(g => g.First()).Take(settings.ColorCount * 2).ToList();
                }

                settings.Progress?.Report(1f);
                settings.Status?.Report($"{Program.Lang.Strings.General.Done}");
                return centroids;

            }, settings.CancellationToken);
        }

        private static List<Color> LockFreeKMeansAsync(List<Color> pixels, int k, PaletteGeneratorSettings? settings = null)
        {
            if (pixels == null || pixels.Count == 0) return new List<Color>();

            Random rnd = new();
            int kEffective = Math.Min(k, pixels.Count);

            List<Color> centroids = pixels.OrderBy(p => rnd.Next()).Take(kEffective).ToList();
            Color? accent = settings?.AccentColor;
            float accentWeight = settings?.AccentWeight ?? 2f;
            int iterations = settings?.ColorCount ?? 10;
            int totalSteps = iterations * pixels.Count;
            int completedSteps = 0;

            for (int iter = 0; iter < iterations; iter++)
            {
                settings?.CancellationToken.ThrowIfCancellationRequested();

                List<List<Color>> clusters = new(kEffective);
                for (int i = 0; i < kEffective; i++) clusters.Add(new List<Color>());

                Parallel.ForEach(pixels, new ParallelOptions { CancellationToken = settings?.CancellationToken ?? default }, px =>
                {
                    int bestIndex = 0;
                    float minDist = float.MaxValue;

                    for (int i = 0; i < kEffective; i++)
                    {
                        float dist = px.Distance(centroids[i]);

                        if (accent.HasValue)
                        {
                            float accentDist = px.Distance(accent.Value);
                            dist /= (1f + accentWeight / (1f + accentDist));
                        }

                        if (dist < minDist)
                        {
                            minDist = dist;
                            bestIndex = i;
                        }
                    }

                    lock (clusters[bestIndex])
                        clusters[bestIndex].Add(px);

                    Interlocked.Increment(ref completedSteps);
                    settings?.Progress?.Report(0.5f + (float)completedSteps / totalSteps * 0.5f);
                });

                Parallel.For(0, kEffective, new ParallelOptions { CancellationToken = settings?.CancellationToken ?? default }, i =>
                {
                    var cluster = clusters[i];
                    if (cluster.Count == 0) return;

                    int r = (int)cluster.Average(c => c.R);
                    int g = (int)cluster.Average(c => c.G);
                    int b = (int)cluster.Average(c => c.B);
                    int a = (int)cluster.Average(c => c.A);

                    centroids[i] = Color.FromArgb(a, r, g, b);
                });

                Thread.Yield();
            }

            settings?.Progress?.Report(1f);
            return centroids;
        }

        /// <summary>
        /// Creates a new circular bitmap that fits the smallest dimension of the source image.
        /// The background is always transparent.
        /// </summary>
        /// <param name="source">Source bitmap.</param>
        /// <returns>Circular bitmap cropped and masked to the smallest dimension.</returns>
        public static Bitmap ToCircular(this Bitmap source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            int size = Math.Min(source.Width, source.Height);
            int x = (source.Width - size) / 2;
            int y = (source.Height - size) / 2;

            Bitmap output = new(size, size, PixelFormat.Format32bppArgb);

            using (Graphics G = Graphics.FromImage(output))
            {
                G.SmoothingMode = SmoothingMode.AntiAlias;
                G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                G.PixelOffsetMode = PixelOffsetMode.HighQuality;
                G.Clear(Color.Transparent);

                using (GraphicsPath path = new())
                {
                    path.AddEllipse(0, 0, size, size);
                    G.SetClip(path);

                    G.DrawImage(source, -x, -y, source.Width, source.Height);

                    using (Pen pen = new(Program.Style.Schemes.Main.Colors.Line(5), 5f))
                    {
                        G.DrawEllipse(pen, -x, -y, source.Width, source.Height);
                    }
                }
            }

            return output;
        }
    }
}