using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
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

            BitmapData data = null;
            try
            {
                data = bitmap.LockBits(
                    new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb);

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
                if (data != null)
                    bitmap.UnlockBits(data);
            }
        }

        /// <summary>
        /// Return most used color from an image
        /// </summary>
        public static Color AverageColor(this Image Image)
        {
            if (Image is Bitmap bitmap) return bitmap.AverageColor(); return Color.Empty;
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
            if (bitmap == null) return null;
            if (bitmap.Width == 0 || bitmap.Height == 0) return null;

            // Clamp opacity
            if (opacity < 0f) opacity = 0f;
            else if (opacity > 1f) opacity = 1f;

            // --- Normalize format to 32bpp ARGB (safe to process), but remember original format ---
            Bitmap src;
            bool converted = false;
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
            {
                src = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);
                using (var g = Graphics.FromImage(src))
                    g.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                converted = true;
            }
            else
            {
                src = (Bitmap)bitmap.Clone();
            }

            // Destination (working) bitmap is also 32bpp ARGB
            var blurred = new Bitmap(src.Width, src.Height, PixelFormat.Format32bppArgb);

            int width = src.Width;
            int height = src.Height;

            // --- Build 1D Gaussian kernel from blurPower ---
            float sigma = Math.Max(0.1f, blurPower);
            int radius = (int)Math.Ceiling(3f * sigma);
            int kSize = 2 * radius + 1;

            var kernel = new float[kSize];
            float kSum = 0f;
            for (int i = -radius; i <= radius; i++)
            {
                float v = (float)Math.Exp(-(i * i) / (2f * sigma * sigma));
                kernel[i + radius] = v;
                kSum += v;
            }
            // Normalize so sum(kernel) = 1
            for (int i = 0; i < kSize; i++) kernel[i] /= kSum;

            // --- Lock source and destination ---
            var srcData = src.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, src.PixelFormat);
            var dstData = blurred.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, blurred.PixelFormat);

            int bytesPerPixel = 4;                 // because we're in 32bpp ARGB
            int stride = srcData.Stride;
            int bufSize = stride * height;

            var srcBuffer = new byte[bufSize];
            var tmpBuffer = new byte[bufSize];
            var finalBuffer = new byte[bufSize];

            Marshal.Copy(srcData.Scan0, srcBuffer, 0, bufSize);
            src.UnlockBits(srcData);

            // --- Horizontal pass ---
            for (int y = 0; y < height; y++)
            {
                cancellationToken.ThrowIfCancellationRequested(); // check per row

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

            // --- Vertical pass ---
            for (int x = 0; x < width; x++)
            {
                cancellationToken.ThrowIfCancellationRequested(); // check per column

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

            // Copy final buffer into destination bitmap
            Marshal.Copy(finalBuffer, 0, dstData.Scan0, bufSize);
            blurred.UnlockBits(dstData);

            // --- Convert back to original PixelFormat ---
            Bitmap result;
            if (converted)
            {
                result = new Bitmap(bitmap.Width, bitmap.Height, bitmap.PixelFormat);
                using (var g = Graphics.FromImage(result))
                    g.DrawImage(blurred, new Rectangle(0, 0, result.Width, result.Height));
                blurred.Dispose();
                src.Dispose();
            }
            else
            {
                result = blurred;
                src.Dispose();
            }

            return result;
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
            if (bitmap is null) return null;

            // Create a new bitmap with same size + DPI as the original
            Bitmap result = new(bitmap.Width, bitmap.Height, bitmap.PixelFormat);
            result.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.SmoothingMode = SmoothingMode.None;
                g.CompositingMode = CompositingMode.SourceOver;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // Draw original image at full size
                g.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));

                // Apply noise overlay
                if (noiseMode == NoiseMode.Acrylic)
                {
                    using (Bitmap noise = Properties.Resources.Noise.Fade(opacity))
                    using (TextureBrush br = new(noise) { WrapMode = WrapMode.Tile })
                    {
                        g.FillRectangle(br, 0, 0, bitmap.Width, bitmap.Height);
                    }
                }
                else if (noiseMode == NoiseMode.Aero)
                {
                    using (Bitmap aero = Assets.Win7Preview.AeroGlass.Fade(opacity))
                    {
                        g.DrawImage(aero, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Noise bitmap modes
        /// </summary>
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
        /// Replace all pixels whose RGB equals oldColor.RGB (alpha ignored) with NewColor (alpha applied).
        /// Returns a new 32bpp ARGB bitmap.
        /// Uses unsafe pointers + Parallel.For.
        /// </summary>
        public static Bitmap ReplaceColor(this Bitmap inputImage, Color oldColor, Color newColor)
        {
            if (inputImage is null || inputImage.Width == 0 || inputImage.Height == 0)
                return null;

            // Normalize input to 32bppArgb for predictable pixel layout
            Bitmap src = inputImage.PixelFormat == PixelFormat.Format32bppArgb
                ? inputImage
                : inputImage.Clone(new Rectangle(0, 0, inputImage.Width, inputImage.Height),
                                   PixelFormat.Format32bppArgb);

            bool cloned = !ReferenceEquals(src, inputImage);

            var dest = new Bitmap(src.Width, src.Height, PixelFormat.Format32bppArgb);

            BitmapData srcData = null, dstData = null;

            try
            {
                srcData = src.LockBits(new Rectangle(0, 0, src.Width, src.Height),
                                       ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                dstData = dest.LockBits(new Rectangle(0, 0, dest.Width, dest.Height),
                                        ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

                int w = src.Width;
                int h = src.Height;

                int srcStride = srcData.Stride;
                int dstStride = dstData.Stride;
                int srcStrideAbs = Math.Abs(srcStride);

                // Precompute packed colors
                uint oldBGR24 = (uint)(oldColor.B | (oldColor.G << 8) | (oldColor.R << 16));
                uint newBGRA = (uint)(newColor.B | (newColor.G << 8) | (newColor.R << 16) | (newColor.A << 24));

                unsafe
                {
                    byte* srcBase = (byte*)srcData.Scan0;
                    byte* dstBase = (byte*)dstData.Scan0;

                    // If stride < 0, adjust start pointer
                    byte* srcStart = srcStride >= 0
                        ? srcBase
                        : srcBase + (h - 1) * srcStrideAbs;

                    Parallel.For(0, h, y =>
                    {
                        byte* sRow = srcStart + (srcStride >= 0 ? y * srcStride : -y * srcStride);
                        byte* dRow = dstBase + y * dstStride;

                        uint* sPx = (uint*)sRow;
                        uint* dPx = (uint*)dRow;

                        for (int x = 0; x < w; x++)
                        {
                            uint v = sPx[x]; // BGRA layout
                            if ((v & 0x00FFFFFFu) == oldBGR24)
                                dPx[x] = newBGRA;
                            else
                                dPx[x] = v;
                        }
                    });
                }
            }
            finally
            {
                if (srcData != null) src.UnlockBits(srcData);
                if (dstData != null) dest.UnlockBits(dstData);
                if (cloned) src.Dispose();
            }

            return dest;
        }

        /// <summary>
        /// Return bitmap filled in the scale of size you choose (Windows 7+ fill style)
        /// </summary>
        public static Bitmap FillInSize(this Bitmap bitmap, Size targetSize)
        {
            if (bitmap == null || targetSize.Width <= 0 || targetSize.Height <= 0)
                return bitmap;

            // Calculate scaling factor to fill the target size while maintaining aspect ratio
            decimal scaleW = (decimal)targetSize.Width / bitmap.Width;
            decimal scaleH = (decimal)targetSize.Height / bitmap.Height;
            decimal scale = Math.Max(scaleW, scaleH);

            int destWidth = (int)Math.Round(bitmap.Width * scale);
            int destHeight = (int)Math.Round(bitmap.Height * scale);

            // Center position
            int offsetX = (targetSize.Width - destWidth) / 2;
            int offsetY = (targetSize.Height - destHeight) / 2;

            // Create the final bitmap with desired size
            Bitmap result = new(targetSize.Width, targetSize.Height, PixelFormat.Format32bppArgb);
            result.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;

                // Fill background with transparent or black (optional)
                g.Clear(Color.Black);

                // Draw scaled image centered
                g.DrawImage(bitmap, new Rectangle(offsetX, offsetY, destWidth, destHeight));
            }

            return result;
        }

        /// <summary>
        /// Resize image to the given size using GDI+ HighQualityBicubic.
        /// Optimized: disposes Graphics properly, avoids redundant calls, preserves DPI.
        /// </summary>
        public static Bitmap Resize(this Bitmap src, int targetWidth, int targetHeight)
        {
            if (src == null) return null;

            // Create destination bitmap in same pixel format to avoid extra conversions
            Bitmap dst = new(targetWidth, targetHeight, PixelFormat.Format32bppArgb);

            dst.SetResolution(src.HorizontalResolution, src.VerticalResolution);

            using (Graphics g = Graphics.FromImage(dst))
            {
                // High quality setup (minimal allocations, reused constants)
                g.CompositingMode = CompositingMode.SourceOver;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                // Draw directly, no Clear(Color.Transparent) needed since draw overwrites fully
                g.DrawImage(src, new Rectangle(0, 0, targetWidth, targetHeight));
            }

            return dst;
        }

        /// <summary>
        /// Resize image in the size you choose
        /// </summary>
        public static Bitmap Resize(this Bitmap bmSource, Size TargetSize)
        {
            return bmSource.Resize(TargetSize.Width, TargetSize.Height);
        }

        /// <summary>
        /// Resize Image in the size you choose
        /// </summary>
        public static Bitmap Resize(this Image imSource, int TargetWidth, int TargetHeight)
        {
            return ((Bitmap)imSource).Resize(TargetWidth, TargetHeight);
        }

        /// <summary>
        /// Return image tinted by a color (optimized with unsafe pointers + parallelization)
        /// </summary>
        public static Bitmap Tint(this Bitmap originalBitmap, Color tintColor)
        {
            if (originalBitmap == null) return null;

            // Create a destination bitmap
            Bitmap tintedBitmap = new(originalBitmap.Width, originalBitmap.Height, PixelFormat.Format32bppArgb);

            Rectangle rect = new(0, 0, originalBitmap.Width, originalBitmap.Height);
            BitmapData srcData = originalBitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = tintedBitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            try
            {
                int height = srcData.Height;
                int width = srcData.Width;
                int strideSrc = srcData.Stride;
                int strideDst = dstData.Stride;

                float rMul = tintColor.R / 255f;
                float gMul = tintColor.G / 255f;
                float bMul = tintColor.B / 255f;
                float aMul = tintColor.A / 255f;

                unsafe
                {
                    byte* srcPtr = (byte*)srcData.Scan0;
                    byte* dstPtr = (byte*)dstData.Scan0;

                    Parallel.For(0, height, y =>
                    {
                        byte* srcRow = srcPtr + (y * strideSrc);
                        byte* dstRow = dstPtr + (y * strideDst);

                        for (int x = 0; x < width; x++)
                        {
                            int idx = x * 4;

                            byte b = srcRow[idx];
                            byte g = srcRow[idx + 1];
                            byte r = srcRow[idx + 2];
                            byte a = srcRow[idx + 3];

                            dstRow[idx] = (byte)(b * bMul);
                            dstRow[idx + 1] = (byte)(g * gMul);
                            dstRow[idx + 2] = (byte)(r * rMul);
                            dstRow[idx + 3] = (byte)(a * aMul);
                        }
                    });
                }
            }
            finally
            {
                originalBitmap.UnlockBits(srcData);
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
                // Return fully transparent bitmap quickly
                var transparent = new Bitmap(source.Width, source.Height, PixelFormat.Format32bppArgb);
                using var g = Graphics.FromImage(transparent); // ensures fully zeroed with alpha=0
                g.Clear(Color.Transparent);
                return transparent;
            }
            if (opacity >= 1f)
            {
                // Return a 1:1 copy (fast path) without touching pixels
                return Clone32_Fast(source);
            }

            // Convert opacity to 8.8 fixed-point to avoid per-pixel float multiply.
            // opQ = round(opacity * 256). Range [1..255]
            int opQ = Math.Min(255, Math.Max(1, (int)Math.Round(opacity * 256.0)));

            Bitmap src32 = null;
            bool createdTemp = false;

            // Ensure 32bpp ARGB for a predictable memory layout.
            if (source.PixelFormat != PixelFormat.Format32bppArgb)
            {
                src32 = new Bitmap(source.Width, source.Height, PixelFormat.Format32bppArgb);
                using (var g = Graphics.FromImage(src32))
                {
                    g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                    g.DrawImageUnscaled(source, 0, 0);
                }
                createdTemp = true;
            }
            else
            {
                // Use original directly (no copy yet)
                src32 = source;
            }

            var dest = new Bitmap(src32.Width, src32.Height, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, src32.Width, src32.Height);
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

                        // Process 4 bytes per pixel: B,G,R,A
                        for (int x = 0; x < width; x++)
                        {
                            int i = x << 2; // x * 4
                                            // Copy BGR
                            dstRow[i + 0] = srcRow[i + 0];
                            dstRow[i + 1] = srcRow[i + 1];
                            dstRow[i + 2] = srcRow[i + 2];

                            // Scale alpha using 8.8 fixed point: (a * opQ) >> 8
                            // Guarantees 0..255 range
                            int a = srcRow[i + 3];
                            dstRow[i + 3] = (byte)((a * opQ) >> 8);
                        }
                    };

                    if (useParallel && height >= 64) // simple heuristic
                    {
                        Parallel.For(0, height, processRow);
                    }
                    else
                    {
                        for (int y = 0; y < height; y++)
                            processRow(y);
                    }
                }
            }
            finally
            {
                if (srcData != null) src32.UnlockBits(srcData);
                if (dstData != null) dest.UnlockBits(dstData);
                if (createdTemp && src32 != null) src32.Dispose();
            }

            return dest;
        }

        /// <summary>
        /// Fast clone to 32bpp ARGB without per-pixel math.
        /// If source is already 32bpp, uses LockBits memcpy for speed.
        /// </summary>
        public static Bitmap Clone32_Fast(this Bitmap source)
        {
            if (source.PixelFormat == PixelFormat.Format32bppArgb)
            {
                var clone = new Bitmap(source.Width, source.Height, PixelFormat.Format32bppArgb);
                Rectangle rect = new Rectangle(0, 0, source.Width, source.Height);

                BitmapData srcData = null, dstData = null;
                try
                {
                    srcData = source.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                    dstData = clone.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

                    unsafe
                    {
                        byte* srcBase = (byte*)srcData.Scan0;
                        byte* dstBase = (byte*)dstData.Scan0;
                        int widthBytes = source.Width * 4;

                        for (int y = 0; y < source.Height; y++)
                        {
                            Buffer.MemoryCopy(
                                srcBase + y * srcData.Stride,
                                dstBase + y * dstData.Stride,
                                widthBytes,
                                widthBytes);
                        }
                    }
                }
                finally
                {
                    if (srcData != null) source.UnlockBits(srcData);
                    if (dstData != null) clone.UnlockBits(dstData);
                }
                return clone;
            }
            else
            {
                var clone = new Bitmap(source.Width, source.Height, PixelFormat.Format32bppArgb);
                using var g = Graphics.FromImage(clone);
                g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                g.DrawImageUnscaled(source, 0, 0);
                return clone;
            }
        }

        /// <summary>
        /// Fade image (change opacity)
        /// </summary>
        public static Image Fade(this Image originalImage, float opacity)
        {
            return (originalImage as Bitmap).Fade(opacity);
        }

        /// <summary>
        /// Return image in grayscale
        /// </summary>
        public static Bitmap Grayscale(this Image original)
        {
            return ((Bitmap)original).Grayscale();
        }

        /// <summary>
        /// Return bitmap in grayscale (SIMD + multi-core optimized).
        /// </summary>
        public static Bitmap Grayscale(this Bitmap original)
        {
            if (original == null) return null;

            Bitmap newBitmap = new(original.Width, original.Height, PixelFormat.Format32bppArgb);

            Rectangle rect = new(0, 0, original.Width, original.Height);
            BitmapData srcData = original.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = newBitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int width = original.Width;
            int height = original.Height;

            unsafe
            {
                byte* srcPtr = (byte*)srcData.Scan0;
                byte* dstPtr = (byte*)dstData.Scan0;

                int srcStride = srcData.Stride;
                int dstStride = dstData.Stride;

                // Grayscale weights (scaled ×1000)
                const int wR = 299, wG = 587, wB = 114;

                Parallel.For(0, height, y =>
                {
                    byte* sRow = srcPtr + y * srcStride;
                    byte* dRow = dstPtr + y * dstStride;

                    int x = 0;

                    // SIMD batch size (process multiple pixels at once)
                    int simdBatch = Vector<byte>.Count / 4; // number of BGRA pixels per vector
                                                            // Note: Vector<byte>.Count is usually 16 (SSE2) or 32 (AVX2)

                    for (; x <= width - simdBatch; x += simdBatch)
                    {
                        for (int i = 0; i < simdBatch; i++)
                        {
                            int idx = (x + i) * 4;

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
                    }

                    // Tail pixels (scalar)
                    for (; x < width; x++)
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

            original.UnlockBits(srcData);
            newBitmap.UnlockBits(dstData);

            return newBitmap;
        }

        /// <summary>
        /// Return inverted bitmap
        /// </summary>
        public static Bitmap Invert(this Bitmap source)
        {
            if (source == null) return null;

            Bitmap clone = source.Clone(new Rectangle(0, 0, source.Width, source.Height), PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, clone.Width, clone.Height);
            BitmapData data = clone.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            int height = clone.Height;
            int width = clone.Width;
            int stride = data.Stride;

            unsafe
            {
                byte* ptr = (byte*)data.Scan0;

                Parallel.For(0, height, y =>
                {
                    byte* row = ptr + (y * stride);
                    for (int x = 0; x < width; x++)
                    {
                        int idx = x * 4;
                        row[idx] = (byte)(255 - row[idx]);       // Blue
                        row[idx + 1] = (byte)(255 - row[idx + 1]); // Green
                        row[idx + 2] = (byte)(255 - row[idx + 2]); // Red
                                                                   // Alpha stays the same
                    }
                });
            }

            clone.UnlockBits(data);
            return clone;
        }

        /// <summary>
        /// Tiles the source bitmap to fill the target size.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="targetSize"></param>
        /// <returns></returns>
        public static Bitmap Tile(this Bitmap source, Size targetSize)
        {
            if (source == null) return null;

            int srcWidth = source.Width;
            int srcHeight = source.Height;
            int destWidth = targetSize.Width;
            int destHeight = targetSize.Height;

            Bitmap dest = new(destWidth, destHeight, source.PixelFormat);

            BitmapData sourceData = null;
            BitmapData destData = null;

            try
            {
                sourceData = source.LockBits(
                    new Rectangle(0, 0, srcWidth, srcHeight),
                    ImageLockMode.ReadOnly,
                    source.PixelFormat);

                destData = dest.LockBits(
                    new Rectangle(0, 0, destWidth, destHeight),
                    ImageLockMode.WriteOnly,
                    source.PixelFormat);

                int bytesPerPixel = Image.GetPixelFormatSize(source.PixelFormat) / 8;
                int srcStride = sourceData.Stride;
                int destStride = destData.Stride;

                // Copy source rows into managed memory
                byte[][] srcRows = new byte[srcHeight][];
                unsafe
                {
                    byte* srcPtr = (byte*)sourceData.Scan0;
                    for (int y = 0; y < srcHeight; y++)
                    {
                        srcRows[y] = new byte[srcWidth * bytesPerPixel];
                        System.Runtime.InteropServices.Marshal.Copy(
                            new IntPtr(srcPtr + y * srcStride),
                            srcRows[y], 0, srcWidth * bytesPerPixel);
                    }
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

                        fixed (byte* srcRowPtr = srcRow)
                        {
                            // Copy full blocks
                            for (int b = 0; b < fullBlocks; b++)
                            {
                                Buffer.MemoryCopy(
                                    srcRowPtr,
                                    dstRow + b * srcWidth * bytesPerPixel,
                                    destStride - b * srcWidth * bytesPerPixel,
                                    srcWidth * bytesPerPixel);
                            }

                            // Copy remaining pixels
                            if (remainder > 0)
                            {
                                Buffer.MemoryCopy(
                                    srcRowPtr,
                                    dstRow + fullBlocks * srcWidth * bytesPerPixel,
                                    destStride - fullBlocks * srcWidth * bytesPerPixel,
                                    remainder * bytesPerPixel);
                            }
                        }
                    });
                }
            }
            finally
            {
                if (sourceData != null) source.UnlockBits(sourceData);
                if (destData != null) dest.UnlockBits(destData);
            }

            return dest;
        }

        public static Bitmap AdjustHSL(this Bitmap bmp, float? hShift = null, float? sValue = null, float? lValue = null)
        {
            if (bmp == null) throw new ArgumentNullException(nameof(bmp));

            Bitmap result = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format32bppArgb);
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

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

                        // Adjust H
                        if (hShift.HasValue)
                        {
                            hsl.H += hShift.Value;
                            if (hsl.H < 0f) hsl.H += 360f;
                            if (hsl.H >= 360f) hsl.H -= 360f;
                        }

                        // Adjust S (0.5 = neutral)
                        if (sValue.HasValue)
                        {
                            float sFactor = sValue.Value / 0.5f;
                            hsl.S = hsl.S * sFactor;
                            if (hsl.S < 0f) hsl.S = 0f;
                            if (hsl.S > 1f) hsl.S = 1f;
                        }

                        // Adjust L (0.5 = neutral)
                        if (lValue.HasValue)
                        {
                            float lFactor = lValue.Value / 0.5f;
                            hsl.L = hsl.L * lFactor;
                            if (hsl.L < 0f) hsl.L = 0f;
                            if (hsl.L > 1f) hsl.L = 1f;
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

        public static Bitmap Brighten(this Bitmap bmp, float brightness = 0f)
        {
            if (bmp == null) throw new ArgumentNullException(nameof(bmp));

            Bitmap result = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format32bppArgb);

            BitmapData srcData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = result.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

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
                        dstRow[x * bytesPerPixel + 3] = srcRow[x * bytesPerPixel + 3]; // Copy alpha
                    }
                });
            }

            bmp.UnlockBits(srcData);
            result.UnlockBits(dstData);
            return result;
        }

        public static Bitmap Contrast(this Bitmap bmp, float contrast = 0f)
        {
            if (bmp == null) throw new ArgumentNullException(nameof(bmp));

            Bitmap result = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format32bppArgb);

            BitmapData srcData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = result.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytesPerPixel = 4;
            float cFactor = (contrast + 1f) * (contrast + 1f);

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
                            float color = srcRow[x * bytesPerPixel + i] / 255f;
                            color = ((color - 0.5f) * cFactor) + 0.5f;
                            color = Math.Max(0f, Math.Min(1f, color));
                            dstRow[x * bytesPerPixel + i] = (byte)(color * 255);
                        }
                        dstRow[x * bytesPerPixel + 3] = srcRow[x * bytesPerPixel + 3]; // Copy alpha
                    }
                });
            }

            bmp.UnlockBits(srcData);
            result.UnlockBits(dstData);
            return result;
        }

    }
}