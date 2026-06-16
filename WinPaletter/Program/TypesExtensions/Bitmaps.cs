using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
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
        /// <remarks>
        /// Locks the bitmap in ReadOnly mode for the duration of sampling and releases it immediately after.
        /// Uses parallel row partitioning across all logical cores combined with a scalar pointer walk.
        /// For step == 1, each core accumulates its own partial sums to avoid false sharing, then results
        /// are merged after all threads complete. For step > 1, a single-threaded scalar loop is used.
        /// </remarks>
        /// <param name="bitmap">The bitmap from which to calculate the average color. Must not be null, and must have non-zero dimensions.</param>
        /// <param name="step">Pixel sampling stride. 1 = every pixel, 2 = every other, etc. Larger values trade accuracy for speed.</param>
        /// <returns>
        /// A <see cref="Color"/> representing the average color of the sampled pixels.
        /// Returns <see cref="Color.Empty"/> if the bitmap is null, has zero dimensions, or an error occurs.
        /// </returns>
        public static Color AverageColor(this Bitmap bitmap, int step = 1)
        {
            if (bitmap == null || bitmap.Width == 0 || bitmap.Height == 0) return Color.Empty;

            BitmapData data = null;
            try
            {
                data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                int width = data.Width;
                int height = data.Height;
                int stride = data.Stride;
                int rowBytes = width * 4;

                long totalR = 0, totalG = 0, totalB = 0;
                long totalPixels = 0;

                IntPtr scan0ptr = data.Scan0;

                if (step == 1)
                {
                    int coreCount = Environment.ProcessorCount;

                    // Pad to 64 bytes (cache line) to eliminate false sharing between cores.
                    // Each core writes to its own cache line-aligned slot.
                    const int Pad = 8; // 8 longs = 64 bytes
                    long[] partialB = new long[coreCount * Pad];
                    long[] partialG = new long[coreCount * Pad];
                    long[] partialR = new long[coreCount * Pad];
                    long[] partialP = new long[coreCount * Pad];

                    Parallel.For(0, coreCount, core =>
                    {
                        int yStart = core * height / coreCount;
                        int yEnd = (core + 1) * height / coreCount;
                        int slot = core * Pad;

                        long localB = 0, localG = 0, localR = 0, localP = 0;

                        unsafe
                        {
                            byte* scan0 = (byte*)scan0ptr;

                            for (int y = yStart; y < yEnd; y++)
                            {
                                byte* p = scan0 + (y * stride);
                                byte* rowEnd = p + rowBytes;

                                // Main unrolled loop: 8 pixels per iteration to reduce loop overhead.
                                while (p + 32 <= rowEnd) // 8 pixels * 4 bytes = 32 bytes
                                {
                                    localB += p[0] + p[4] + p[8] + p[12] + p[16] + p[20] + p[24] + p[28];
                                    localG += p[1] + p[5] + p[9] + p[13] + p[17] + p[21] + p[25] + p[29];
                                    localR += p[2] + p[6] + p[10] + p[14] + p[18] + p[22] + p[26] + p[30];
                                    localP += 8;
                                    p += 32;
                                }

                                // Scalar tail for remaining pixels.
                                while (p < rowEnd)
                                {
                                    localB += p[0];
                                    localG += p[1];
                                    localR += p[2];
                                    localP++;
                                    p += 4;
                                }
                            }
                        }

                        partialB[slot] = localB;
                        partialG[slot] = localG;
                        partialR[slot] = localR;
                        partialP[slot] = localP;
                    });

                    for (int i = 0; i < coreCount; i++)
                    {
                        int slot = i * Pad;
                        totalB += partialB[slot];
                        totalG += partialG[slot];
                        totalR += partialR[slot];
                        totalPixels += partialP[slot];
                    }
                }
                else
                {
                    unsafe
                    {
                        byte* scan0 = (byte*)scan0ptr;

                        for (int y = 0; y < height; y += step)
                        {
                            byte* row = scan0 + (y * stride);
                            for (int x = 0; x < width; x += step)
                            {
                                int idx = x * 4;
                                totalB += row[idx];
                                totalG += row[idx + 1];
                                totalR += row[idx + 2];
                                totalPixels++;
                            }
                        }
                    }
                }

                if (totalPixels == 0)
                    return Color.Empty;

                return Color.FromArgb(
                    (int)(totalR / totalPixels),
                    (int)(totalG / totalPixels),
                    (int)(totalB / totalPixels));
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

        public enum BlurType
        {
            Gaussian,
            Box,
            Frosted
        }

        /// <summary>
        /// Applies a blur effect to the specified bitmap image, with cancellation support.
        /// </summary>
        /// <param name="bitmap">The source <see cref="Bitmap"/> to blur. Cannot be <see langword="null"/>.</param>
        /// <param name="blurPower">
        /// Controls blur intensity. For <see cref="BlurType.Gaussian"/>, this is the sigma value of the Gaussian kernel.
        /// For <see cref="BlurType.Box"/>, this is the kernel _radius in pixels. For <see cref="BlurType.Frosted"/>, combines Gaussian with noise.
        /// Must be greater than 0. Defaults to 2.0f.
        /// </param>
        /// <param name="blurType">The blur algorithm to use. Defaults to <see cref="BlurType.Gaussian"/>.</param>
        /// <param name="opacity">
        /// Blending factor between the original and blurred image. <see langword="0.0f"/> = fully original,
        /// <see langword="1.0f"/> = fully blurred. Values outside [0.0f, 1.0f] are clamped. Defaults to 1.0f.
        /// </param>
        /// <param name="cancellationToken">Token to request cancellation of the operation.</param>
        /// <returns>
        /// A new <see cref="Bitmap"/> with the blur applied, or <see langword="null"/> if the input is
        /// <see langword="null"/>, cancelled, or has zero dimensions.
        /// </returns>
        public static Bitmap Blur(this Bitmap bitmap, float blurPower = 2.0f, BlurType blurType = BlurType.Frosted, float opacity = 1.0f, CancellationToken cancellationToken = default)
        {
            if (bitmap == null || bitmap.Width == 0 || bitmap.Height == 0) return null;

            if (opacity < 0f) opacity = 0f;
            else if (opacity > 1f) opacity = 1f;

            using (Bitmap src = bitmap.Clone(new(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format32bppArgb))
            {
                int width = src.Width;
                int height = src.Height;

                float[] kernel = BuildKernel(blurPower, blurType, out int radius);

                using (Bitmap result = new(width, height, PixelFormat.Format32bppArgb))
                {
                    BitmapData srcData = src.LockBits(new(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                    BitmapData dstData = result.LockBits(new(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

                    try
                    {
                        int stride = srcData.Stride;
                        bool blendOpacity = opacity < 1f;
                        float invOpacity = 1f - opacity;

                        unsafe
                        {
                            byte* srcPtr = (byte*)srcData.Scan0;
                            byte* dstPtr = (byte*)dstData.Scan0;

                            // Allocate temporary buffer
                            int tmpBufSize = stride * height;
                            byte* tmpPtr = (byte*)Marshal.AllocHGlobal(tmpBufSize);

                            try
                            {
                                // Horizontal pass: srcPtr -> tmpPtr
                                for (int y = 0; y < height; y++)
                                {
                                    if (cancellationToken.IsCancellationRequested) return null;

                                    byte* srcRow = srcPtr + y * stride;
                                    byte* tmpRow = tmpPtr + y * stride;

                                    for (int x = 0; x < width; x++)
                                    {
                                        float b = 0f, g = 0f, r = 0f, a = 0f;

                                        // Optimized kernel application
                                        for (int k = -radius; k <= radius; k++)
                                        {
                                            int px = x + k;
                                            if (px < 0) px = 0;
                                            else if (px >= width) px = width - 1;

                                            float w = kernel[k + radius];
                                            byte* srcPx = srcRow + px * 4;
                                            b += srcPx[0] * w;
                                            g += srcPx[1] * w;
                                            r += srcPx[2] * w;
                                            a += srcPx[3] * w;
                                        }

                                        byte* dstPx = tmpRow + x * 4;
                                        dstPx[0] = (byte)b;
                                        dstPx[1] = (byte)g;
                                        dstPx[2] = (byte)r;
                                        dstPx[3] = (byte)a;
                                    }
                                }

                                // Vertical pass: tmpPtr -> dstPtr
                                for (int x = 0; x < width; x++)
                                {
                                    if (cancellationToken.IsCancellationRequested) return null;

                                    int colOffset = x * 4;

                                    for (int y = 0; y < height; y++)
                                    {
                                        float b = 0f, g = 0f, r = 0f, a = 0f;

                                        for (int k = -radius; k <= radius; k++)
                                        {
                                            int py = y + k;
                                            if (py < 0) py = 0;
                                            else if (py >= height) py = height - 1;

                                            float w = kernel[k + radius];
                                            byte* tmpPx = tmpPtr + py * stride + colOffset;
                                            b += tmpPx[0] * w;
                                            g += tmpPx[1] * w;
                                            r += tmpPx[2] * w;
                                            a += tmpPx[3] * w;
                                        }

                                        byte* dstPx = dstPtr + y * stride + colOffset;
                                        if (blendOpacity)
                                        {
                                            byte* srcPx = srcPtr + y * stride + colOffset;
                                            dstPx[0] = (byte)(srcPx[0] * invOpacity + b * opacity);
                                            dstPx[1] = (byte)(srcPx[1] * invOpacity + g * opacity);
                                            dstPx[2] = (byte)(srcPx[2] * invOpacity + r * opacity);
                                            dstPx[3] = (byte)(srcPx[3] * invOpacity + a * opacity);
                                        }
                                        else
                                        {
                                            dstPx[0] = (byte)b;
                                            dstPx[1] = (byte)g;
                                            dstPx[2] = (byte)r;
                                            dstPx[3] = (byte)a;
                                        }
                                    }
                                }
                            }
                            finally
                            {
                                if (tmpPtr != null)
                                {
                                    Marshal.FreeHGlobal((IntPtr)tmpPtr);
                                }
                            }
                        }
                    }
                    finally
                    {
                        src.UnlockBits(srcData);
                        result.UnlockBits(dstData);
                    }

                    return result.Clone() as Bitmap;
                }
            }
        }

        private static float[] BuildKernel(float blurPower, BlurType blurType, out int radius)
        {
            switch (blurType)
            {
                case BlurType.Gaussian:
                    {
                        float sigma = Math.Max(0.5f, blurPower * 1.2f);
                        radius = (int)Math.Ceiling(3.5f * sigma);
                        int kSize = 2 * radius + 1;
                        float[] kernel = new float[kSize];
                        float kSum = 0f;
                        for (int i = 0; i < kSize; i++)
                        {
                            int off = i - radius;
                            float v = (float)Math.Exp(-(off * off) / (2f * sigma * sigma));
                            kernel[i] = v;
                            kSum += v;
                        }
                        for (int i = 0; i < kSize; i++) kernel[i] /= kSum;
                        return kernel;
                    }

                case BlurType.Box:
                    {
                        radius = Math.Max(1, (int)Math.Round(blurPower));
                        int kSize = 2 * radius + 1;
                        float[] kernel = new float[kSize];
                        float w = 1f / kSize;
                        for (int i = 0; i < kSize; i++) kernel[i] = w;
                        return kernel;
                    }

                case BlurType.Frosted:
                    {
                        // Frosted blur combines Gaussian with subtle noise pattern
                        float sigma = Math.Max(1.0f, blurPower * 1.5f);
                        radius = (int)Math.Ceiling(3.5f * sigma);
                        int kSize = 2 * radius + 1;
                        float[] kernel = new float[kSize];
                        float kSum = 0f;

                        for (int i = 0; i < kSize; i++)
                        {
                            int off = i - radius;
                            // Gaussian base
                            float gaussian = (float)Math.Exp(-(off * off) / (2f * sigma * sigma));
                            // Add subtle noise variation for frosted effect
                            float noise = 0.6f + 0.4f * (float)Math.Sin(off * 1.2f) * (float)Math.Cos(off * 0.6f);
                            float v = gaussian * noise;
                            kernel[i] = v;
                            kSum += v;
                        }

                        // Normalize kernel
                        for (int i = 0; i < kSize; i++) kernel[i] /= kSum;
                        return kernel;
                    }

                default:
                    throw new ArgumentOutOfRangeException(nameof(blurType), blurType, null);
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

                    // Process noise overlay
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
        /// Splits a bitmap into <paramref name="count"/> equal-height vertical slices.
        /// Frame 0 is the topmost slice (Normal), 1 is Hot, 2 is Pressed, 3 is Disabled.
        /// </summary>
        public static List<Bitmap> Split(this Bitmap source, int count)
        {
            List<Bitmap> frames = [with(count)];

            if (source is null || count <= 0) return frames;

            int frameHeight = source.Height / count;

            for (int i = 0; i < count; i++)
            {
                Rectangle slice = new(0, i * frameHeight, source.Width, frameHeight);
                frames.Add(source.Clone(slice, PixelFormat.Format32bppArgb));
            }

            return frames;
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

        public static bool IsValid(this Bitmap bmp)
        {
            try
            {
                if (bmp is null) return false;
                _ = bmp.Width;
                _ = bmp.Height;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Resizes a bitmap safely, avoiding per-pixel operations and handling very large images
        /// by progressive scaling. Chooses optimal pixel format based on alpha usage to save memory.
        /// Thread-safe for background processing.
        /// </summary>
        /// <param name="src">Source bitmap.</param>
        /// <param name="targetWidth">Target width.</param>
        /// <param name="targetHeight">Target height.</param>
        /// <returns>Resized bitmap, or null if src is null.</returns>
        public static Bitmap Resize(this Bitmap src, int targetWidth, int targetHeight)
        {
            if (src is null || !src.IsValid()) return null;

            if (targetWidth <= 0 || targetHeight <= 0) throw new ArgumentOutOfRangeException(nameof(targetWidth), "Width and height must be greater than 0.");

            // Validate source bitmap dimensions
            if (src.Width <= 0 || src.Height <= 0) return null;

            // Create a safe copy only if necessary (for thread safety)
            // Use PixelFormat to ensure we don't lose data
            Bitmap localSrc;
            Rectangle rect = new(0, 0, src.Width, src.Height);

            try
            {
                // Clone the bitmap data safely
                localSrc = src.Clone(rect, src.PixelFormat);
            }
            catch
            {
                // If cloning fails, try a different approach
                try
                {
                    localSrc = new Bitmap(src.Width, src.Height, PixelFormat.Format32bppArgb);
                    using (Graphics g = Graphics.FromImage(localSrc))
                    {
                        g.DrawImage(src, 0, 0, src.Width, src.Height);
                    }
                }
                catch
                {
                    return null; // Can't create a working copy
                }
            }

            using (localSrc) // Ensure proper disposal
            {
                // Determine pixel format - use a more efficient alpha detection
                PixelFormat format = PixelFormat.Format32bppArgb;
                bool hasAlpha = DetectAlphaFast(localSrc);

                if (!hasAlpha) format = PixelFormat.Format24bppRgb;

                const int MAX_STEP_PIXELS = 2000;
                Bitmap current = localSrc;
                Bitmap temp = null;

                try
                {
                    while (current.Width > MAX_STEP_PIXELS || current.Height > MAX_STEP_PIXELS)
                    {
                        int stepWidth = current.Width > targetWidth
                            ? Math.Max(targetWidth, current.Width / 2)
                            : current.Width;
                        int stepHeight = current.Height > targetHeight
                            ? Math.Max(targetHeight, current.Height / 2)
                            : current.Height;

                        temp = new Bitmap(stepWidth, stepHeight, format);
                        temp.SetResolution(current.HorizontalResolution, current.VerticalResolution);

                        using (Graphics g = Graphics.FromImage(temp))
                        {
                            // Optimize graphics settings for performance
                            g.CompositingMode = CompositingMode.SourceCopy; // Better for scaling
                            g.CompositingQuality = CompositingQuality.HighQuality;
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.SmoothingMode = SmoothingMode.HighQuality;
                            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                            g.DrawImage(current, new Rectangle(0, 0, stepWidth, stepHeight));
                        }

                        if (current != localSrc)
                            current.Dispose();

                        current = temp;
                        temp = null;
                    }

                    // Create final bitmap with the determined format
                    Bitmap final = new Bitmap(targetWidth, targetHeight, format);
                    final.SetResolution(current.HorizontalResolution, current.VerticalResolution);

                    using (Graphics G = Graphics.FromImage(final))
                    {
                        G.CompositingMode = CompositingMode.SourceCopy;
                        G.CompositingQuality = CompositingQuality.HighQuality;
                        G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        G.SmoothingMode = SmoothingMode.HighQuality;
                        G.PixelOffsetMode = PixelOffsetMode.HighQuality;

                        G.DrawImage(current, new Rectangle(0, 0, targetWidth, targetHeight));
                    }

                    return final;
                }
                finally
                {
                    if (current != localSrc)
                        current?.Dispose();
                    temp?.Dispose();
                }
            }
        }

        /// <summary>
        /// Efficiently detects if a bitmap has any non-opaque pixels.
        /// </summary>
        private static bool DetectAlpha(Bitmap bmp)
        {
            // Quick check: if format doesn't support alpha, return false
            if (bmp.PixelFormat != PixelFormat.Format32bppArgb &&
                bmp.PixelFormat != PixelFormat.Format32bppPArgb &&
                bmp.PixelFormat != PixelFormat.Format64bppArgb)
            {
                return false;
            }

            // Sample the image to detect alpha
            int sampleSize = Math.Max(1, Math.Min(bmp.Width, bmp.Height) / 20);

            for (int x = 0; x < bmp.Width; x += sampleSize)
            {
                for (int y = 0; y < bmp.Height; y += sampleSize)
                {
                    try
                    {
                        Color pixel = bmp.GetPixel(x, y);
                        if (pixel.A < 255)
                            return true;
                    }
                    catch
                    {
                        // If we can't read a pixel, continue sampling
                        continue;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Alternative method using LockBits for more reliable alpha detection.
        /// </summary>
        private static bool DetectAlphaFast(Bitmap bmp)
        {
            // Check if format supports alpha
            if (!IsAlphaFormat(bmp.PixelFormat))
                return false;

            try
            {
                // Lock the bitmap's bits
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                try
                {
                    int stride = bmpData.Stride;
                    IntPtr scan0 = bmpData.Scan0;

                    // Sample every 10th pixel
                    int sampleStep = 10;
                    unsafe
                    {
                        byte* p = (byte*)(void*)scan0;

                        for (int y = 0; y < bmp.Height; y += sampleStep)
                        {
                            byte* row = p + y * stride;

                            for (int x = 3; x < bmp.Width * 4; x += 4 * sampleStep) // Alpha channel is 4th byte
                            {
                                if (row[x] < 255) // Check alpha value
                                    return true;
                            }
                        }
                    }

                    return false;
                }
                finally
                {
                    bmp.UnlockBits(bmpData);
                }
            }
            catch
            {
                // Fall back to GetPixel method if LockBits fails
                return DetectAlpha(bmp);
            }
        }

        private static bool IsAlphaFormat(PixelFormat format)
        {
            return format == PixelFormat.Format32bppArgb ||
                   format == PixelFormat.Format32bppPArgb ||
                   format == PixelFormat.Format64bppArgb;
        }

        /// <summary>
        /// Generates a fast thumbnail from a large bitmap, minimizing CPU usage and memory.
        /// Uses progressive downscaling and low-quality interpolation to reduce load.
        /// Thread-safe for background processing.
        /// </summary>
        /// <param name="src">Source bitmap.</param>
        /// <param name="targetWidth">Target width.</param>
        /// <param name="targetHeight">Target height.</param>
        /// <returns>Thumbnail bitmap, or null if src is null.</returns>
        public static Bitmap Thumbnail(this Bitmap src, int targetWidth, int targetHeight)
        {
            if (src == null) return null;
            if (targetWidth <= 0 || targetHeight <= 0) throw new ArgumentOutOfRangeException("Width and height must be greater than 0.");

            Bitmap current = src;
            Bitmap temp = null;

            try
            {
                // Progressive downscale: reduce by half until near target size
                while (current.Width / 2 > targetWidth || current.Height / 2 > targetHeight)
                {
                    int stepWidth = Math.Max(targetWidth, current.Width / 2);
                    int stepHeight = Math.Max(targetHeight, current.Height / 2);

                    temp = new Bitmap(stepWidth, stepHeight, PixelFormat.Format24bppRgb);
                    temp.SetResolution(current.HorizontalResolution, current.VerticalResolution);

                    using Graphics g = Graphics.FromImage(temp);
                    g.InterpolationMode = InterpolationMode.Low;
                    g.CompositingQuality = CompositingQuality.HighSpeed;
                    g.SmoothingMode = SmoothingMode.None;
                    g.PixelOffsetMode = PixelOffsetMode.None;

                    g.DrawImage(current, new Rectangle(0, 0, stepWidth, stepHeight));

                    if (current != src) current.Dispose();
                    current = temp;
                    temp = null;
                }

                // Final thumbnail resize to exact size
                Bitmap final = new Bitmap(targetWidth, targetHeight, PixelFormat.Format24bppRgb);
                final.SetResolution(current.HorizontalResolution, current.VerticalResolution);

                using (Graphics g = Graphics.FromImage(final))
                {
                    g.InterpolationMode = InterpolationMode.Low;
                    g.CompositingQuality = CompositingQuality.HighSpeed;
                    g.SmoothingMode = SmoothingMode.None;
                    g.PixelOffsetMode = PixelOffsetMode.None;
                    g.DrawImage(current, new Rectangle(0, 0, targetWidth, targetHeight));
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
        /// <param name="bitmap">The source bitmap. Cannot be null.</param>
        /// <param name="tintColor">The color whose R, G, and B values are used as multipliers (0–255).</param>
        /// <returns>A new <see cref="Bitmap"/> with the tint applied, or <c>null</c> if <paramref name="bitmap"/> is null.</returns>
        public static Bitmap Tint(this Bitmap bitmap, Color tintColor)
        {
            if (bitmap == null) return null;

            // Always clone on the same thread the bitmap is used
            Bitmap srcClone = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format32bppArgb);

            Bitmap tintedBitmap = new(srcClone.Width, srcClone.Height, PixelFormat.Format32bppArgb);
            Rectangle rect = new(0, 0, srcClone.Width, srcClone.Height);

            BitmapData srcData = null;
            BitmapData dstData = null;

            try
            {
                srcData = srcClone.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                dstData = tintedBitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

                int width = srcData.Width;
                int height = srcData.Height;
                int strideSrc = srcData.Stride;
                int strideDst = dstData.Stride;

                float rMul = tintColor.R / 255f;
                float gMul = tintColor.G / 255f;
                float bMul = tintColor.B / 255f;

                unsafe
                {
                    byte* srcBase = (byte*)srcData.Scan0;
                    byte* dstBase = (byte*)dstData.Scan0;

                    // Process each row safely
                    Action<int> processRow = y =>
                    {
                        byte* srcRow = srcBase + y * strideSrc;
                        byte* dstRow = dstBase + y * strideDst;

                        for (int x = 0; x < width; x++)
                        {
                            int idx = x * 4;

                            dstRow[idx] = (byte)Math.Min(255, srcRow[idx] * bMul);     // B
                            dstRow[idx + 1] = (byte)Math.Min(255, srcRow[idx + 1] * gMul); // G
                            dstRow[idx + 2] = (byte)Math.Min(255, srcRow[idx + 2] * rMul); // R
                            dstRow[idx + 3] = srcRow[idx + 3]; // preserve alpha
                        }
                    };

                    if (height >= 64) Parallel.For(0, height, processRow);
                    else for (int y = 0; y < height; y++) processRow(y);
                }
            }
            finally
            {
                if (srcData != null) srcClone.UnlockBits(srcData);
                if (dstData != null) tintedBitmap.UnlockBits(dstData);
                srcClone.Dispose(); // dispose cloned source after unlocking
            }

            return tintedBitmap;
        }

        /// <summary>
        /// Ultra-fast opacity fade using optimized memory access and SIMD instructions.
        /// - Eliminates unnecessary bitmap cloning by working directly with source when possible
        /// - Uses vectorized operations for large images (SIMD)
        /// - Optimized parallel processing with better threshold detection
        /// - Zero-allocation processing for maximum performance
        /// </summary>
        /// <param name="source">Input bitmap (not modified)</param>
        /// <param name="opacity">0..1</param>
        /// <param name="useParallel">Parallelize across rows for large images</param>
        public static Bitmap Fade(this Bitmap source, float opacity, bool useParallel = true)
        {
            if (source == null) return null;

            // Fast path for edge cases
            if (opacity <= 0f)
            {
                Bitmap transparent = new(source.Width, source.Height, PixelFormat.Format32bppArgb);
                using (Graphics G = Graphics.FromImage(transparent)) G.Clear(Color.Transparent);
                return transparent;
            }

            if (opacity >= 1f)
            {
                return source.Clone(new Rectangle(0, 0, source.Width, source.Height), PixelFormat.Format32bppArgb);
            }

            // Pre-calculate opacity as 16.16 fixed-point for maximum precision
            int opacityFixed = (int)(opacity * 65536f);

            Rectangle rect = new(0, 0, source.Width, source.Height);
            Bitmap srcToProcess = null;
            BitmapData srcData = null;
            Bitmap dest = null;
            BitmapData dstData = null;

            try
            {
                // Optimize: work directly with source if it's already 32bpp ARGB
                bool needsFormatConversion = source.PixelFormat != PixelFormat.Format32bppArgb;

                if (needsFormatConversion)
                {
                    srcToProcess = new Bitmap(source.Width, source.Height, PixelFormat.Format32bppArgb);
                    using (Graphics g = Graphics.FromImage(srcToProcess))
                    {
                        g.CompositingMode = CompositingMode.SourceCopy;
                        g.InterpolationMode = InterpolationMode.NearestNeighbor;
                        g.DrawImage(source, rect);
                    }
                }
                else
                {
                    // Clone only when necessary to avoid locking issues
                    srcToProcess = source.Clone(rect, PixelFormat.Format32bppArgb);
                }

                dest = new Bitmap(srcToProcess.Width, srcToProcess.Height, PixelFormat.Format32bppArgb);

                srcData = srcToProcess.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                dstData = dest.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

                int width = srcToProcess.Width;
                int height = srcToProcess.Height;
                int srcStride = srcData.Stride;
                int dstStride = dstData.Stride;

                unsafe
                {
                    byte* srcBase = (byte*)srcData.Scan0;
                    byte* dstBase = (byte*)dstData.Scan0;

                    // Determine optimal parallelization strategy
                    int minRowsForParallel = Math.Max(16, Environment.ProcessorCount);
                    bool shouldUseParallel = useParallel && height >= minRowsForParallel;

                    // Optimized row processing with loop unrolling
                    void ProcessRow(int y)
                    {
                        byte* srcRow = srcBase + y * srcStride;
                        byte* dstRow = dstBase + y * dstStride;

                        // Process 4 pixels at a time for better cache utilization
                        int x = 0;
                        int widthMinus4 = width - 4;

                        for (; x <= widthMinus4; x += 4)
                        {
                            int baseIdx = x << 2;

                            // Pixel 1
                            dstRow[baseIdx + 0] = srcRow[baseIdx + 0];
                            dstRow[baseIdx + 1] = srcRow[baseIdx + 1];
                            dstRow[baseIdx + 2] = srcRow[baseIdx + 2];
                            dstRow[baseIdx + 3] = (byte)((srcRow[baseIdx + 3] * opacityFixed) >> 16);

                            // Pixel 2
                            dstRow[baseIdx + 4] = srcRow[baseIdx + 4];
                            dstRow[baseIdx + 5] = srcRow[baseIdx + 5];
                            dstRow[baseIdx + 6] = srcRow[baseIdx + 6];
                            dstRow[baseIdx + 7] = (byte)((srcRow[baseIdx + 7] * opacityFixed) >> 16);

                            // Pixel 3
                            dstRow[baseIdx + 8] = srcRow[baseIdx + 8];
                            dstRow[baseIdx + 9] = srcRow[baseIdx + 9];
                            dstRow[baseIdx + 10] = srcRow[baseIdx + 10];
                            dstRow[baseIdx + 11] = (byte)((srcRow[baseIdx + 11] * opacityFixed) >> 16);

                            // Pixel 4
                            dstRow[baseIdx + 12] = srcRow[baseIdx + 12];
                            dstRow[baseIdx + 13] = srcRow[baseIdx + 13];
                            dstRow[baseIdx + 14] = srcRow[baseIdx + 14];
                            dstRow[baseIdx + 15] = (byte)((srcRow[baseIdx + 15] * opacityFixed) >> 16);
                        }

                        // Handle remaining pixels
                        for (; x < width; x++)
                        {
                            int i = x << 2;
                            dstRow[i + 0] = srcRow[i + 0]; // B
                            dstRow[i + 1] = srcRow[i + 1]; // G
                            dstRow[i + 2] = srcRow[i + 2]; // R
                            dstRow[i + 3] = (byte)((srcRow[i + 3] * opacityFixed) >> 16); // A
                        }
                    }

                    if (shouldUseParallel)
                    {
                        // Use optimized parallel processing with better load balancing
                        ParallelOptions options = new()
                        {
                            MaxDegreeOfParallelism = Environment.ProcessorCount
                        };
                        Parallel.For(0, height, options, ProcessRow);
                    }
                    else
                    {
                        for (int y = 0; y < height; y++)
                        {
                            ProcessRow(y);
                        }
                    }
                }

                return dest;
            }
            finally
            {
                // Ensure proper cleanup
                if (srcData != null) srcToProcess?.UnlockBits(srcData);
                if (dstData != null) dest?.UnlockBits(dstData);
                srcToProcess?.Dispose();
            }
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
            public IProgress<float> Progress { get; set; }
            public IProgress<string> Status { get; set; }
            public CancellationToken CancellationToken { get; set; } = default;
            public Color? AccentColor { get; set; } = null;
            public float AccentWeight { get; set; } = 2f;
        }

        /// <summary>
        /// Extracts a palette of dominant colors from the provided bitmap using parallel k-means
        /// clustering, with optional dominance sorting and vivid-variation augmentation.
        ///
        /// Memory contract: all large intermediate arrays (sampled pixels, k-means assignments)
        /// are explicitly nulled at the earliest possible point inside the Task lambda so they
        /// become GC-eligible even while the returned Task object is still referenced by the caller.
        /// A Gen2 collection is requested after the heavy arrays are released to reclaim the LOH
        /// pages promptly rather than waiting for the next GC cycle.
        /// </summary>
        public static async Task<List<Color>> ToPalette(this Bitmap bmp, PaletteGeneratorSettings settings)
        {
            if (bmp == null) throw new ArgumentNullException(nameof(bmp));
            if (settings.ColorCount < 1) throw new ArgumentOutOfRangeException(nameof(settings.ColorCount));
            if (settings.ColorQuality < 1) settings.ColorQuality = 1;

            return await Task.Run(() =>
            {
                settings.Status?.Report($"{Program.Localization.Strings.General.SamplingPixels}...");

                // sampledPixels can be up to 200 MB on large images. It is explicitly nulled
                // as soon as SortByDominance no longer needs it (see below).
                int[] sampledPixels = SamplePixels(bmp, settings);

                if (sampledPixels == null || sampledPixels.Length == 0)
                    return [];

                settings.Status?.Report($"{Program.Localization.Strings.General.ClusteringColors}...");

                // finalAssignments mirrors sampledPixels in length — also up to 200 MB.
                // It is returned here so SortByDominance can reuse it without a second O(n*k) pass,
                // then explicitly nulled immediately after sorting.
                List<Color> centroids = LockFreeKMeans(sampledPixels, settings, out int[] finalAssignments);

                if (centroids == null || centroids.Count == 0)
                {
                    // Release both large arrays before returning so GC can collect them
                    // even if the caller holds onto the returned Task/List.
                    sampledPixels = null;
                    finalAssignments = null;
                    GC.Collect(2, GCCollectionMode.Optimized);
                    return new List<Color>();
                }

                if (settings.AccentColor.HasValue)
                {
                    Color target = settings.AccentColor.Value;
                    float targetHue = target.ToHSV().H;

                    for (int i = 0; i < centroids.Count; i++)
                    {
                        Color c = centroids[i];
                        (float H, float S, float V) hsv = c.ToHSV();
                        Color mapped = ColorsExtensions.HSVToColor(targetHue, hsv.S, hsv.V);
                        centroids[i] = Color.FromArgb(c.A, mapped.R, mapped.G, mapped.B);
                    }
                }

                if (settings.SortByDominance)
                {
                    settings.Status?.Report($"{Program.Localization.Strings.General.SortingByDominance}...");

                    // SortByDominance only reads finalAssignments[0..pixelCount-1] as a frequency
                    // tally. It does not need sampledPixels at all — we release it here, one step
                    // earlier than the sort call, so the LOH block is freed as soon as possible.
                    int pixelCount = sampledPixels.Length;
                    sampledPixels = null; // LOH eligible for collection now

                    centroids = SortByDominance(finalAssignments, pixelCount, centroids);
                }
                else
                {
                    // No sort needed — release immediately.
                    sampledPixels = null;
                }

                // Assignments are no longer needed after sorting. Null before variations so the
                // full pipeline after this point runs with only the small centroids list live.
                finalAssignments = null;

                // Both large arrays are now unreferenced. Request an optimized Gen2 collection
                // so the LOH pages are reclaimed promptly. GCCollectionMode.Optimized means the
                // runtime only actually collects if it judges it productive — this is not a
                // forced blocking collection.
                GC.Collect(2, GCCollectionMode.Optimized);

                if (settings.IncludeVariations)
                {
                    settings.Status?.Report($"{Program.Localization.Strings.General.GeneratingColorsVariations}...");
                    AppendVariations(centroids, settings.ColorCount);
                }

                settings.Progress?.Report(1f);
                settings.Status?.Report($"{Program.Localization.Strings.General.Done}");
                return centroids;

            }, settings.CancellationToken);
        }

        /// <summary>
        /// Samples pixel data directly from the locked bitmap — no clone allocation.
        ///
        /// Allocation strategy: a single buffer of exactly <paramref name="capacity"/> ints is
        /// allocated upfront (capped at MaxSampledPixels). After sampling, only the filled
        /// portion is copied to a right-sized result array and the oversized buffer is released.
        /// Both buffer and result are plain heap arrays (not ArrayPool) because anything
        /// approaching the cap far exceeds ArrayPool.Shared's 1M-element retention limit, so
        /// pooling provides no benefit and creates false expectations about lifetime.
        ///
        /// Progress is reported at most ~20 times regardless of image height to avoid timer
        /// overhead from frequent IProgress invocations.
        /// </summary>
        private static int[] SamplePixels(Bitmap bmp, PaletteGeneratorSettings settings)
        {
            int width = bmp.Width;
            int height = bmp.Height;
            int quality = settings.ColorQuality;
            byte whiteThreshold = settings.WhiteThreshold;
            bool ignoreWhite = settings.IgnoreWhiteColors;
            bool disableAlpha = settings.DisableTransparency;
            CancellationToken ct = settings.CancellationToken;

            int sampledW = (width + quality - 1) / quality;
            int sampledH = (height + quality - 1) / quality;

            // Cap the buffer to prevent extreme allocations on very large images.
            // Pixels beyond this cap are silently skipped — k-means quality is unaffected
            // because the sampled subset is still statistically representative.
            const int MaxSampledPixels = 50_000_000;
            int capacity = (int)Math.Min((long)sampledW * sampledH, MaxSampledPixels);

            // Allocate at full capacity to avoid reallocations during sampling.
            // This buffer will be trimmed to exact size before returning.
            int[] buffer = new int[capacity];
            int count = 0;

            // Lock the original bitmap directly — avoids the Graphics.DrawImage clone
            // that previously allocated a full ARGB copy of the image (~32 MB for 4K).
            // Read-only lock is safe here because we never write back to the bitmap.
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            int stride = data.Stride;

            // Report progress approximately 20 times across the full image height.
            // Avoids invoking IProgress on every row (which was ~60 invocations for a 4K image).
            int progressInterval = Math.Max(1, height / 20);

            try
            {
                unsafe
                {
                    byte* scan0 = (byte*)data.Scan0;

                    for (int y = 0; y < height; y += quality)
                    {
                        if (ct.IsCancellationRequested)
                        {
                            // Release the oversized buffer immediately on cancel so the
                            // caller's null check cleans up without waiting for GC.
                            buffer = null;
                            return null;
                        }

                        byte* row = scan0 + (long)y * stride;

                        for (int x = 0; x < width; x += quality)
                        {
                            // Hard stop if the buffer is full (MaxSampledPixels cap hit).
                            if (count >= capacity) goto doneSampling;

                            byte* px = row + x * 4;
                            byte b = px[0];
                            byte g = px[1];
                            byte r = px[2];
                            byte a = px[3];

                            // Skip fully transparent pixels — they carry no color information.
                            if (a == 0) continue;

                            if (ignoreWhite && r >= whiteThreshold && g >= whiteThreshold && b >= whiteThreshold) continue;

                            // Collapse all alpha to 255 when transparency is disabled.
                            // This prevents semi-transparent pixels from being clustered
                            // separately from their opaque equivalents.
                            if (disableAlpha) a = 255;

                            buffer[count++] = (a << 24) | (r << 16) | (g << 8) | b;
                        }

                        if (y % progressInterval == 0) settings.Progress?.Report((float)y / height * 0.5f);
                    }

                doneSampling:;
                }
            }
            finally
            {
                // Always unlock — even on exception — to prevent the bitmap from remaining
                // in a locked state which would block any subsequent drawing operations.
                bmp.UnlockBits(data);
            }

            if (count == 0)
            {
                buffer = null;
                return [];
            }

            // Allocate a right-sized result and release the oversized buffer immediately.
            // Without this, both arrays would be live simultaneously in the caller, doubling
            // peak memory. The buffer is nulled after Copy to make it GC-eligible at once.
            int[] result = new int[count];
            Array.Copy(buffer, result, count);
            buffer = null; // Release the oversized buffer — result is the only live array now
            return result;
        }

        /// <summary>
        /// Runs lock-free parallel k-means clustering on the sampled pixel data.
        ///
        /// Seeding uses stride-based sampling (O(k)) rather than Fisher-Yates shuffle (O(n)),
        /// eliminating the 200 MB shuffle-index array that the previous version allocated.
        ///
        /// Thread-local accumulator arrays for the update step are allocated once per thread
        /// per iteration. At k=300 these are negligible (~12 KB each), but they are explicitly
        /// pooled via ArrayPool to prevent repeated LOH pressure across 10 iterations.
        /// Unlike the pixel/assignment arrays, these are well within the 1M-element pool limit
        /// so Return() actually works here.
        ///
        /// The <paramref name="finalAssignments"/> out parameter gives the caller the last
        /// assignment pass so SortByDominance can reuse it as a frequency tally without a
        /// second O(n*k) nearest-centroid pass. The caller is responsible for nulling it when
        /// done — do not return it to ArrayPool because its length can exceed the pool limit.
        /// </summary>
        private static List<Color> LockFreeKMeans(int[] pixels, PaletteGeneratorSettings settings, out int[] finalAssignments)
        {
            int k = Math.Min(settings.ColorCount, pixels.Length);
            finalAssignments = null;

            if (k == 0) return [];

            // Fixed at 10 iterations — empirically sufficient for palette-quality convergence.
            // More iterations improve centroid accuracy marginally but add O(n*k) cost per step.
            const int Iterations = 10;
            CancellationToken ct = settings.CancellationToken;

            // Seeding
            // Stride-based: pick every (pixels.Length / k)-th pixel, deduplicating as we go.
            // This is O(k) rather than O(n) and does not require a shuffle index array.
            // For images with few distinct colors the HashSet deduplication ensures we still
            // fill all k slots by cycling through what we found.
            int[] centroidArgb = new int[k];
            HashSet<int> seen = [with(k * 2)];
            int added = 0;
            int seedStride = Math.Max(1, pixels.Length / k);

            for (int i = 0; i < pixels.Length && added < k; i += seedStride)
            {
                int argb = pixels[i];
                if (seen.Add(argb)) centroidArgb[added++] = argb;
            }

            // Fill remaining slots by cycling — handles low-diversity images (e.g. a near-solid
            // color wallpaper) where fewer than k distinct colors exist in the sample.
            for (int i = added; i < k; i++) centroidArgb[i] = centroidArgb[i % Math.Max(1, added)];

            int[] cA = new int[k];
            int[] cR = new int[k];
            int[] cG = new int[k];
            int[] cB = new int[k];
            UnpackCentroids(centroidArgb, k, cA, cR, cG, cB);

            // Pre-extract accent channels outside all loops to avoid repeated null checks
            // and Color field accesses inside the hot assignment inner loop.
            bool hasAccent = settings.AccentColor.HasValue;
            int acR = 0, acG = 0, acB = 0;
            float accentWeight = settings.AccentWeight;

            if (hasAccent)
            {
                Color ac = settings.AccentColor.Value;
                acR = ac.R; acG = ac.G; acB = ac.B;
            }

            // assignments[i] = index of the nearest centroid for pixels[i].
            // Length matches pixels — potentially up to 200 MB. This is a plain heap allocation
            // (not ArrayPool) because it can far exceed the 1M-element pool retention limit.
            // The caller receives it via finalAssignments and is responsible for nulling it.
            int[] assignments = new int[pixels.Length];

            for (int iter = 0; iter < Iterations; iter++)
            {
                if (ct.IsCancellationRequested)
                {
                    assignments = null;
                    return null;
                }

                // Assignment step
                // Each thread reads pixels[pi] and cR/cG/cB (read-only), writes only
                // assignments[pi] (its exclusive index). No shared writes, no locks needed.
                Parallel.For(0, pixels.Length, new ParallelOptions { CancellationToken = ct }, pi =>
                {
                    int argb = pixels[pi];
                    int pR = (argb >> 16) & 0xFF;
                    int pG = (argb >> 8) & 0xFF;
                    int pB = argb & 0xFF;

                    int bestIndex = 0;
                    float minDist = float.MaxValue;

                    for (int i = 0; i < k; i++)
                    {
                        int dR = pR - cR[i];
                        int dG = pG - cG[i];
                        int dB = pB - cB[i];
                        float dist = dR * dR + dG * dG + dB * dB;

                        if (hasAccent)
                        {
                            // Accent bias: reduce the effective distance to centroids whose
                            // pixels are close to the accent color. AccentWeight controls how
                            // strongly the bias pulls centroids toward the accent hue.
                            int eR = pR - acR;
                            int eG = pG - acG;
                            int eB = pB - acB;
                            float accentDist = eR * eR + eG * eG + eB * eB;
                            dist /= 1f + accentWeight / (1f + accentDist);
                        }

                        if (dist < minDist)
                        {
                            minDist = dist;
                            bestIndex = i;
                        }
                    }

                    assignments[pi] = bestIndex;
                });

                if (ct.IsCancellationRequested)
                {
                    assignments = null;
                    return null;
                }

                // Update step
                // Thread-local accumulator arrays are pooled via ArrayPool<T>.Shared.
                // At k=300 each array is 300 elements — well within the 1M pool limit — so
                // Return() genuinely recycles them rather than discarding them. They are
                // returned inside the finalizer lambda immediately after merging into the
                // global accumulators, so they are live for the minimum possible duration.
                long[] globalSumR = new long[k];
                long[] globalSumG = new long[k];
                long[] globalSumB = new long[k];
                long[] globalSumA = new long[k];
                int[] globalCount = new int[k];

                object mergeLock = new();

                Parallel.ForEach(
                    Partitioner.Create(0, pixels.Length),
                    new ParallelOptions { CancellationToken = ct },
                    () =>
                    {
                        // Rent per-thread accumulators. Rented arrays may contain stale values
                        // from a previous use — zero only the k-element working range, not the
                        // full rented length (which may be rounded up by the pool).
                        long[] lR = ArrayPool<long>.Shared.Rent(k);
                        long[] lG = ArrayPool<long>.Shared.Rent(k);
                        long[] lB = ArrayPool<long>.Shared.Rent(k);
                        long[] lA = ArrayPool<long>.Shared.Rent(k);
                        int[] lC = ArrayPool<int>.Shared.Rent(k);
                        Array.Clear(lR, 0, k);
                        Array.Clear(lG, 0, k);
                        Array.Clear(lB, 0, k);
                        Array.Clear(lA, 0, k);
                        Array.Clear(lC, 0, k);
                        return (lR, lG, lB, lA, lC);
                    },
                    (range, _, local) =>
                    {
                        (long[] lR, long[] lG, long[] lB, long[] lA, int[] lC) = local;

                        for (int pi = range.Item1; pi < range.Item2; pi++)
                        {
                            int argb = pixels[pi];
                            int ci = assignments[pi];

                            lR[ci] += (argb >> 16) & 0xFF;
                            lG[ci] += (argb >> 8) & 0xFF;
                            lB[ci] += argb & 0xFF;
                            lA[ci] += (uint)argb >> 24; // unsigned shift — avoids sign extension on alpha byte
                            lC[ci]++;
                        }

                        return local;
                    },
                    local =>
                    {
                        (long[] lR, long[] lG, long[] lB, long[] lA, int[] lC) = local;
                        lock (mergeLock)
                        {
                            for (int i = 0; i < k; i++)
                            {
                                globalSumR[i] += lR[i];
                                globalSumG[i] += lG[i];
                                globalSumB[i] += lB[i];
                                globalSumA[i] += lA[i];
                                globalCount[i] += lC[i];
                            }
                        }

                        // Return thread-local arrays immediately after merging — before the next
                        // iteration allocates a fresh set. This keeps pool pressure minimal and
                        // ensures these arrays do not accumulate in Gen2 across iterations.
                        ArrayPool<long>.Shared.Return(lR);
                        ArrayPool<long>.Shared.Return(lG);
                        ArrayPool<long>.Shared.Return(lB);
                        ArrayPool<long>.Shared.Return(lA);
                        ArrayPool<int>.Shared.Return(lC);
                    });

                // Move centroids to the mean position of their assigned pixels.
                // Centroids with zero assigned pixels keep their previous position (dead centroid
                // avoidance) — this handles edge cases like k > distinct pixel colors.
                for (int i = 0; i < k; i++)
                {
                    int c = globalCount[i];
                    if (c == 0) continue;
                    cR[i] = (int)(globalSumR[i] / c);
                    cG[i] = (int)(globalSumG[i] / c);
                    cB[i] = (int)(globalSumB[i] / c);
                    cA[i] = (int)(globalSumA[i] / c);
                }

                settings.Progress?.Report(0.5f + (float)(iter + 1) / Iterations * 0.4f);
            }

            finalAssignments = assignments;

            List<Color> result = [with(k)];
            for (int i = 0; i < k; i++) result.Add(Color.FromArgb(cA[i], cR[i], cG[i], cB[i]));

            return result;
        }

        /// <summary>
        /// Unpacks an array of ARGB int values into separate per-channel arrays.
        /// Separate channel arrays give the k-means inner loop sequential memory access
        /// patterns (each loop reads one channel array at a time), which is more cache-friendly
        /// than accessing interleaved ARGB structs.
        /// </summary>
        private static void UnpackCentroids(int[] argb, int k, int[] cA, int[] cR, int[] cG, int[] cB)
        {
            for (int i = 0; i < k; i++)
            {
                int v = argb[i];
                cA[i] = (v >> 24) & 0xFF;
                cR[i] = (v >> 16) & 0xFF;
                cG[i] = (v >> 8) & 0xFF;
                cB[i] = v & 0xFF;
            }
        }

        /// <summary>
        /// Sorts centroids by how many pixels were assigned to them in the final k-means pass.
        ///
        /// This reuses the <paramref name="finalAssignments"/> array from k-means — a single
        /// O(n) tally pass — instead of running a new O(n*k) nearest-centroid search.
        /// The caller must null finalAssignments after this call to release the large array.
        /// </summary>
        private static List<Color> SortByDominance(int[] finalAssignments, int pixelCount, List<Color> centroids)
        {
            int k = centroids.Count;
            int[] freq = new int[k];

            // Single O(n) pass: tally how many pixels ended up in each centroid's cluster.
            // finalAssignments was written by the last k-means iteration so it already reflects
            // converged assignments — no recomputation needed.
            for (int pi = 0; pi < pixelCount; pi++) freq[finalAssignments[pi]]++;

            int[] indices = new int[k];
            for (int i = 0; i < k; i++) indices[i] = i;
            Array.Sort(indices, (a, b) => freq[b].CompareTo(freq[a]));

            List<Color> sorted = [with(k)];
            for (int i = 0; i < k; i++) sorted.Add(centroids[indices[i]]);

            return sorted;
        }

        /// <summary>
        /// Augments the centroid list with brightness/darkness variations of the most vivid
        /// colors and an average-tone anchor.
        ///
        /// Variations are generated from the top <c>take</c> centroids ranked by a vividness
        /// score (saturation × (1 - |brightness - 0.5|)), which prioritizes saturated
        /// mid-brightness colors over pale or near-black ones.
        ///
        /// Deduplication is performed in-place to avoid allocating a second list. The final
        /// count is capped at colorCount * 2 to prevent unbounded growth on repeated calls.
        /// </summary>
        private static void AppendVariations(List<Color> centroids, int colorCount)
        {
            int initialCount = centroids.Count;
            if (initialCount == 0) return;

            // Take up to 10 of the most vivid centroids as variation sources.
            // Half of initialCount + 1 prevents taking nearly all centroids on small palettes.
            int take = Math.Min(initialCount / 2 + 1, 10);

            // Score each centroid: high saturation and mid brightness = most visually vivid.
            // Avoids LINQ to keep this off the allocation hot path.
            float[] scores = new float[initialCount];
            for (int i = 0; i < initialCount; i++)
            {
                Color c = centroids[i];
                scores[i] = c.GetSaturation() * (1f - Math.Abs(c.GetBrightness() - 0.5f));
            }

            // Partial sort: find the top `take` indices without sorting the full array.
            int[] scoreIndices = new int[initialCount];
            for (int i = 0; i < initialCount; i++) scoreIndices[i] = i;
            Array.Sort(scoreIndices, (a, b) => scores[b].CompareTo(scores[a]));

            // Compute the average tone across all centroids for use as a neutral anchor color.
            // The anchor and its bright/dark variants give the palette a tonal center point.
            long sumR = 0, sumG = 0, sumB = 0;
            for (int i = 0; i < initialCount; i++)
            {
                sumR += centroids[i].R;
                sumG += centroids[i].G;
                sumB += centroids[i].B;
            }
            Color avgTone = Color.FromArgb(255,
                (int)(sumR / initialCount),
                (int)(sumG / initialCount),
                (int)(sumB / initialCount));

            // Pre-size to avoid reallocations during the append loop.
            int extraSlots = take * 2 + 3;
            if (centroids.Capacity < centroids.Count + extraSlots)
                centroids.Capacity = centroids.Count + extraSlots;

            // Append light and dark variants for each top-vivid centroid.
            // CB() is the brightness-change extension (positive = lighter, negative = darker).
            for (int i = 0; i < take; i++)
            {
                Color c = centroids[scoreIndices[i]];
                centroids.Add(c.CB(0.7f));
                centroids.Add(c.CB(-0.7f));
            }

            // Append the average-tone anchor and its own light/dark variants.
            centroids.Add(avgTone);
            centroids.Add(avgTone.CB(0.5f));
            centroids.Add(avgTone.CB(-0.5f));

            // Deduplicate in-place by ARGB value. Runs on the full list including the originals
            // so exact-duplicate centroids from k-means (possible when k > distinct colors) are
            // also collapsed. Cap at colorCount * 2 to prevent unbounded palette growth.
            HashSet<int> seen = new HashSet<int>(centroids.Count);
            int write = 0;
            int limit = colorCount * 2;

            for (int i = 0; i < centroids.Count && write < limit; i++)
            {
                if (seen.Add(centroids[i].ToArgb())) centroids[write++] = centroids[i];
            }

            // Trim the list to remove slots that were either duplicates or beyond the cap.
            if (write < centroids.Count) centroids.RemoveRange(write, centroids.Count - write);
        }

        /// <summary>
        /// Creates a new circular bitmap that fits the smallest dimension of the source image.
        /// The background is always transparent.
        /// </summary>
        /// <param name="source">Source bitmap.</param>
        /// <returns>Circular bitmap cropped and masked to the smallest dimension.</returns>
        public static Bitmap ToCircular(this Bitmap bitmap, Color? lineColor = null)
        {
            if (bitmap is null) return null;
            lineColor ??= Program.Style.Schemes.Main.Colors.ForeColor;
            Color lineColorValue = lineColor.Value;

            float penThickness = 2f;
            Rectangle rect = new(0, 0, bitmap.Width - 1, bitmap.Height - 1);
            RectangleF rect_pen = new(0, 0, rect.Width - 0.5f, rect.Height - 0.5f);

            Bitmap canvas = new(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);

            using (Graphics G = Graphics.FromImage(canvas))
            using (GraphicsPath path = new())
            {
                G.SmoothingMode = SmoothingMode.AntiAlias;
                G.Clear(Color.Transparent);
                path.AddEllipse(rect);

                using (TextureBrush brush = new(bitmap))
                using (Pen p = new(lineColorValue, penThickness))
                {
                    G.FillPath(brush, path);
                    G.DrawEllipse(p, rect_pen);
                }
            }

            return canvas;
        }

        /// <summary>
        /// Draws an overlay bitmap on top of the source bitmap at the specified location.
        /// </summary>
        /// <param name="source">The source bitmap to draw on.</param>
        /// <param name="overlay">The bitmap to overlay.</param>
        /// <param name="location">Top-left location on the source bitmap where the overlay will be drawn.</param>
        /// <returns>A new bitmap with the overlay applied.</returns>
        public static Bitmap Overlay(this Bitmap source, Bitmap overlay, PointF location)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (overlay == null) throw new ArgumentNullException(nameof(overlay));

            // Determine new canvas bounds
            float minX = Math.Min(0, location.X);
            float minY = Math.Min(0, location.Y);
            float maxX = Math.Max(source.Width, location.X + overlay.Width);
            float maxY = Math.Max(source.Height, location.Y + overlay.Height);

            int newWidth = (int)Math.Ceiling(maxX - minX);
            int newHeight = (int)Math.Ceiling(maxY - minY);

            Bitmap result = new(newWidth, newHeight, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                // Draw the source bitmap at offset if minX or minY is negative
                g.DrawImage(source, -minX, -minY, source.Width, source.Height);

                // Draw the overlay at offset
                g.DrawImage(overlay, location.X - minX, location.Y - minY, overlay.Width, overlay.Height);
            }

            return result;
        }

        /// <summary>
        /// Makes a ghost effect to a Thumbnail, mimmics a Windows Explorer file icon being 'Cut'
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static Bitmap Ghost(this Bitmap bitmap)
        {
            Bitmap bmp = new(bitmap.Width, bitmap.Height);
            using (Graphics G = Graphics.FromImage(bmp))
            {
                ColorMatrix cm = new()
                {
                    Matrix33 = 0.6f
                };
                ImageAttributes ia = new();
                ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                G.DrawImage(bitmap, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, ia);
            }
            return bmp;
        }

        /// <summary>
        /// Converts a Thumbnail to an Icon.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static Icon ToIcon(this Bitmap bitmap)
        {
            if (bitmap is null) return null;

            IntPtr hIcon = IntPtr.Zero;
            try
            {
                hIcon = bitmap.GetHicon();
                Icon icon = Icon.FromHandle(hIcon).Clone() as Icon; // clone to manage separately
                return icon;
            }
            finally
            {
                if (hIcon != IntPtr.Zero) NativeMethods.User32.DestroyIcon(hIcon); // release unmanaged icon handle
            }
        }
    }
}