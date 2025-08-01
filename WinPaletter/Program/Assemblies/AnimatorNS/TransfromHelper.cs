﻿using System;
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
            var k = e.CurrentTime;
            e.Matrix.Translate(-e.ClientRectangle.Width * k * animation.SlideCoeff.X, -e.ClientRectangle.Height * k * animation.SlideCoeff.Y);
            // Apply translation transformation based on slide coefficients //
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
            int a = (int)((sx * kx + sy * ky) * (1 - e.CurrentTime));

            // Loop over each pixel and set alpha to 0 for those covered by the blind effect //
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
            float opacity = 1f - animation.TransparencyCoeff * e.CurrentTime;
            if (opacity < 0f) opacity = 0f;
            if (opacity > 1f) opacity = 1f;

            var pixels = e.Pixels;
            int len = pixels.Length;
            // Instead of recalculating opacity for each pixel, calculate once //
            for (int i = 3; i < len; i += bytesPerPixel)
                pixels[i] = (byte)(pixels[i] * opacity); // Directly scale alpha channel for all pixels //
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

            // Compare pixel data and set alpha to 0 for identical pixels to save memory and reduce unnecessary drawing //
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