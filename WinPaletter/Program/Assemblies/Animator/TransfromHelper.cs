using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace AnimatorNS
{
    /// <summary>
    /// Implements image transformations
    /// </summary>
    public static class TransfromHelper
    {
        const int bytesPerPixel = 4;

        public static void DoSlide(TransfromNeededEventArg e, Animation animation)
        {
            var k = Easing.Apply(animation.EasingFunction, e.CurrentTime);
            e.Matrix.Translate(-e.ClientRectangle.Width * k * animation.SlideCoeff.X, -e.ClientRectangle.Height * k * animation.SlideCoeff.Y);
        }

        public static void DoBlind(NonLinearTransfromNeededEventArg e, Animation animation)
        {
            if (animation.BlindCoeff == PointF.Empty)
                return;

            var pixels = e.Pixels;
            int sx = e.ClientRectangle.Width;
            int sy = e.ClientRectangle.Height;
            int s = e.Stride;
            float kx = animation.BlindCoeff.X;
            float ky = animation.BlindCoeff.Y;
            float easedProgress = Easing.Apply(animation.EasingFunction, e.CurrentTime);
            int a = (int)((sx * kx + sy * ky) * (1 - easedProgress));

            for (int y = 0; y < sy; y++)
            {
                int row = y * s;
                for (int x = 0; x < sx; x++)
                {
                    int i = row + x * bytesPerPixel;
                    var d = x * kx + y * ky - a;
                    if (d >= 0)
                        pixels[i + 3] = 0;
                }
            }
        }

        public static void DoTransparent(NonLinearTransfromNeededEventArg e, Animation animation)
        {
            if (animation.TransparencyCoeff == 0f)
                return;
            float easedProgress = Easing.Apply(animation.EasingFunction, e.CurrentTime);
            float opacity = 1f - animation.TransparencyCoeff * easedProgress;
            if (opacity < 0f) opacity = 0f;
            if (opacity > 1f) opacity = 1f;

            var pixels = e.Pixels;
            int len = pixels.Length;
            for (int i = 3; i < len; i += bytesPerPixel)
                pixels[i] = (byte)(pixels[i] * opacity);
        }

        /// <summary>
        /// Applies zoom transformation to the pixels - Optimized for performance
        /// For Show: zoom in from larger to normal (scale decreases from MaxZoomScale to 1)
        /// For Hide: zoom out from normal to larger (scale increases from 1 to MaxZoomScale)
        /// Both animations keep image ≥ 100% size and centered
        /// </summary>
        public static void DoZoom(NonLinearTransfromNeededEventArg e, Animation animation, AnimateMode mode)
        {
            if (animation.ZoomCoeff == 0f || e.SourcePixels == null)
                return;

            float easedProgress = Easing.Apply(animation.EasingFunction, e.CurrentTime);

            // Calculate scale based on mode
            float scale;
            if (mode == AnimateMode.Show || mode == AnimateMode.BeginUpdate)
            {
                // Show: zoom IN from larger to normal
                scale = 1f + (animation.MaxZoomScale - 1f) * easedProgress;
            }
            else // Hide or Update
            {
                // Hide: zoom OUT from normal to larger
                scale = 1f + (animation.MaxZoomScale - 1f) * easedProgress;
            }

            // Apply zoom coefficient to control intensity
            if (animation.ZoomCoeff < 1f)
            {
                scale = 1f + (scale - 1f) * animation.ZoomCoeff;
            }

            // Clamp scale to prevent extreme values
            scale = Math.Max(0.5f, Math.Min(2.5f, scale));

            // If scale is 1, no need to transform
            if (Math.Abs(scale - 1f) < 0.001f) return;

            var pixels = e.Pixels;
            var sourcePixels = e.SourcePixels;
            int sx = e.ClientRectangle.Width;
            int sy = e.ClientRectangle.Height;
            int stride = e.Stride;

            // Clear destination pixels
            Array.Clear(pixels, 0, pixels.Length);

            // Pre-calculate center and scale factor
            float centerX = sx / 2f;
            float centerY = sy / 2f;
            float invScale = 1f / scale;

            // For each pixel in the destination, find the corresponding source pixel
            for (int y = 0; y < sy; y++)
            {
                int destRow = y * stride;

                // Calculate source Y position centered (pre-calculated for this row)
                float srcYf = (y - centerY) * invScale + centerY;
                int srcY = (int)srcYf;
                srcY = srcY < 0 ? 0 : (srcY >= sy ? sy - 1 : srcY);
                int srcRow = srcY * stride;

                for (int x = 0; x < sx; x++)
                {
                    int destIdx = destRow + (x << 2); // x * 4 (bytesPerPixel)

                    // Calculate source X position centered
                    float srcXf = (x - centerX) * invScale + centerX;
                    int srcX = (int)srcXf;
                    srcX = srcX < 0 ? 0 : (srcX >= sx ? sx - 1 : srcX);
                    int srcIdx = srcRow + (srcX << 2); // srcX * 4

                    // Copy from source pixels - 4 bytes at once
                    pixels[destIdx] = sourcePixels[srcIdx];
                    pixels[destIdx + 1] = sourcePixels[srcIdx + 1];
                    pixels[destIdx + 2] = sourcePixels[srcIdx + 2];
                    pixels[destIdx + 3] = sourcePixels[srcIdx + 3];
                }
            }
        }

        public static void CalcDifference(Bitmap bmp1, Bitmap bmp2)
        {
            PixelFormat pxf = PixelFormat.Format32bppArgb;
            Rectangle rect = new Rectangle(0, 0, bmp1.Width, bmp1.Height);

            BitmapData bmpData1 = bmp1.LockBits(rect, ImageLockMode.ReadWrite, pxf);
            IntPtr ptr1 = bmpData1.Scan0;

            BitmapData bmpData2 = bmp2.LockBits(rect, ImageLockMode.ReadOnly, pxf);
            IntPtr ptr2 = bmpData2.Scan0;

            int numBytes = bmp1.Width * bmp1.Height * bytesPerPixel;
            byte[] pixels1 = new byte[numBytes];
            byte[] pixels2 = new byte[numBytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr1, pixels1, 0, numBytes);
            System.Runtime.InteropServices.Marshal.Copy(ptr2, pixels2, 0, numBytes);

            for (int i = 0; i < numBytes; i += bytesPerPixel)
            {
                if (pixels1[i + 0] == pixels2[i + 0] &&
                    pixels1[i + 1] == pixels2[i + 1] &&
                    pixels1[i + 2] == pixels2[i + 2])
                {
                    pixels1[i + 0] = 255;
                    pixels1[i + 1] = 255;
                    pixels1[i + 2] = 255;
                    pixels1[i + 3] = 0;
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(pixels1, 0, ptr1, numBytes);
            bmp1.UnlockBits(bmpData1);
            bmp2.UnlockBits(bmpData2);
        }
    }
}