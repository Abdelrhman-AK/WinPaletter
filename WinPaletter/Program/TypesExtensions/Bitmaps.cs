﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace WinPaletter.TypesExtensions
{
    public static class BitmapExtensions
    {
        /// <summary>
        /// Return Most Used Color From image
        /// </summary>
        public static Color AverageColor(this Bitmap Bitmap)
        {
            try
            {
                using (Bitmap bmp = (Bitmap)Bitmap.Clone())
                {
                    int totalR = 0;
                    int totalG = 0;
                    int totalB = 0;

                    try
                    {
                        if (bmp is not null)
                        {
                            for (int x = 0, loopTo = bmp.Width - 1; x <= loopTo; x++)
                            {
                                for (int y = 0, loopTo1 = bmp.Height - 1; y <= loopTo1; y++)
                                {
                                    var pixel = bmp.GetPixel(x, y);
                                    totalR += pixel.R;
                                    totalG += pixel.G;
                                    totalB += pixel.B;
                                }
                            }
                        }
                    }
                    catch
                    {

                    }

                    if (bmp is not null)
                    {
                        int totalPixels = bmp.Height * bmp.Width;
                        int averageR = totalR / totalPixels;
                        int averageg = totalG / totalPixels;
                        int averageb = totalB / totalPixels;
                        return Color.FromArgb(averageR, averageg, averageb);
                    }
                    else
                    {
                        return Color.FromArgb(80, 80, 80);
                    }
                }
            }
            catch
            {
                return Color.Empty;
            }
        }

        /// <summary>
        /// Return Most Used Color From Image
        /// </summary>
        public static Color AverageColor(this Image Image)
        {
            return ((Bitmap)Image).AverageColor();
        }

        /// <summary>
        /// Return Blurred image
        /// </summary>
        public static Bitmap Blur(this Bitmap bitmap, int BlurForce = 2)
        {
            using (Bitmap img = new(bitmap))
            {
                using (Graphics G = Graphics.FromImage(img))
                {
                    G.SmoothingMode = SmoothingMode.AntiAlias;

                    var att = new ImageAttributes();
                    var m = new ColorMatrix() { Matrix33 = 0.4f };
                    att.SetColorMatrix(m);

                    for (double x = -BlurForce, loopTo = BlurForce; x <= loopTo; x += 0.5d)
                        G.DrawImage(img, new Rectangle((int)Math.Round(x), 0, img.Width - 1, img.Height - 1), 0, 0, img.Width - 1, img.Height - 1, GraphicsUnit.Pixel, att);

                    for (double y = -BlurForce, loopTo = BlurForce; y <= loopTo; y += 0.5d)
                        G.DrawImage(img, new Rectangle(0, (int)Math.Round(y), img.Width - 1, img.Height - 1), 0, 0, img.Width - 1, img.Height - 1, GraphicsUnit.Pixel, att);

                    G.Save();
                    att.Dispose();
                    return new Bitmap(img);
                }
            }
        }

        /// <summary>
        /// Return Noised bitmap (Noise of Windows 10 Acrylic)
        /// </summary>
        public static Bitmap Noise(this Bitmap bmp, NoiseMode NoiseMode, float opacity)
        {
            try
            {
                var g = Graphics.FromImage(bmp);

                if (NoiseMode == NoiseMode.Acrylic)
                {
                    TextureBrush br;
                    br = new TextureBrush(Properties.Resources.GaussianBlur.Fade((double)opacity));
                    g.FillRectangle(br, new Rectangle(0, 0, bmp.Width, bmp.Height));
                }
                else if (NoiseMode == NoiseMode.Aero)
                {
                    g.DrawImage(Properties.Resources.AeroGlass.Fade((double)opacity), new Rectangle(0, 0, bmp.Width, bmp.Height));
                }

                g.Save();
                return bmp;
                g.Dispose();
                bmp.Dispose();
            }
            catch
            {
                return null;
            }
        }
        public enum NoiseMode
        {
            Aero,
            Acrylic
        }

        /// <summary>
        /// Replace Color in image (Pixels) by color you choose
        /// </summary>
        public static Bitmap ReplaceColor(this Bitmap inputImage, Color oldColor, Color NewColor)
        {
            var outputImage = new Bitmap(inputImage.Width, inputImage.Height);
            var G = Graphics.FromImage(outputImage);


            for (int y = 0, loopTo = inputImage.Height - 1; y <= loopTo; y++)
            {

                for (int x = 0, loopTo1 = inputImage.Width - 1; x <= loopTo1; x++)
                {
                    var PixelColor = inputImage.GetPixel(x, y);

                    if (PixelColor == oldColor)
                    {
                        outputImage.SetPixel(x, y, NewColor);
                    }
                    else
                    {
                        outputImage.SetPixel(x, y, PixelColor);
                    }

                }
            }

            G.DrawImage(outputImage, 0, 0);
            G.Dispose();
            return outputImage;
            outputImage.Dispose();

        }

        /// <summary>
        /// Replace Color in Image (Pixels) by color you choose
        /// </summary>
        public static Image ReplaceColor(this Image inputImage, Color oldColor, Color NewColor)
        {
            return ((Bitmap)inputImage).ReplaceColor(oldColor, NewColor);
        }

        /// <summary>
        /// Return image filled in the scale of size you choose
        /// </summary>
        public static Bitmap FillScale(Bitmap Bitmap, Size Size)
        {
            try
            {
                int sourceWidth = Bitmap.Width;
                int sourceHeight = Bitmap.Height;
                int destX = 0;
                int destY = 0;
                decimal nPercent = 0;
                decimal nPercentW = 0;
                decimal nPercentH = 0;
                nPercentW = Size.Width / (decimal)sourceWidth;
                nPercentH = Size.Height / (decimal)sourceHeight;

                if (nPercentH < nPercentW)
                {
                    nPercent = nPercentH;
                    destX = Convert.ToInt16((Size.Width - sourceWidth * nPercent) / 2);
                }
                else
                {
                    nPercent = nPercentW;
                    destY = Convert.ToInt16((Size.Height - sourceHeight * nPercent) / 2);
                }

                int destWidth = (int)Math.Round(sourceWidth * nPercent);
                int destHeight = (int)Math.Round(sourceHeight * nPercent);

                var bmPhoto = new Bitmap(Size.Width, Size.Height, PixelFormat.Format32bppArgb);
                bmPhoto.SetResolution(Bitmap.HorizontalResolution, Bitmap.VerticalResolution);
                var grPhoto = Graphics.FromImage(bmPhoto);
                grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
                grPhoto.DrawImage(Bitmap, new Rectangle(0, 0, destWidth, destHeight));
                grPhoto.Dispose();
                Bitmap bm = bmPhoto.Clone(new Rectangle(0, 0, destWidth, destHeight), PixelFormat.Format32bppArgb);
                decimal f;

                if (nPercentH < nPercentW)
                {
                    f = Size.Width - bm.Width;
                    bm = bm.Resize(Size.Width, (int)Math.Round(Size.Height + f));
                    int x = (int)Math.Round(1d / 3d * (double)f);
                    int correctionFactor = bm.Height + x <= Size.Height ? Size.Height : bm.Height - x;
                    return bm.Clone(new Rectangle(0, (int)Math.Round(1d / 3d * (double)f), Size.Width, correctionFactor), PixelFormat.Format32bppArgb);
                }
                else
                {
                    f = Size.Height - bm.Height;
                    bm = bm.Resize(Size.Width, (int)Math.Round(Size.Height + f));
                    int x = (int)Math.Round(1d / 3d * (double)f);
                    int correctionFactor = bm.Width + x <= Size.Width ? Size.Width : bm.Width - x;
                    return bm.Clone(new Rectangle(x, 0, correctionFactor, Size.Height), PixelFormat.Format32bppArgb);
                }
            }
            catch
            {
                return Bitmap;
            }
        }

        /// <summary>
        /// Return Image filled in the scale of size you choose
        /// </summary>
        public static Image FillScale(this Image Image, Size Size)
        {
            if (Image != null)
                using (Bitmap bmp = new(Image))
                {
                    return FillScale(bmp, Size);
                }
            else
                return null;
        }

        /// <summary>
        /// Resize image in the size you choose
        /// </summary>
        public static Bitmap Resize(this Bitmap bmSource, int TargetWidth, int TargetHeight)
        {
            if (bmSource is null)
                return null;

            using (var B = new Bitmap(TargetWidth, TargetHeight, PixelFormat.Format32bppArgb))
            {
                using (var G = Graphics.FromImage(B))
                {
                    G.Clear(Color.Transparent);
                    G.CompositingQuality = CompositingQuality.HighQuality;
                    G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    G.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    G.SmoothingMode = SmoothingMode.AntiAlias;
                    G.CompositingMode = CompositingMode.SourceOver;
                    G.DrawImage(bmSource, 0, 0, TargetWidth, TargetHeight);
                }

                B.SetResolution(TargetWidth, TargetHeight);
                return (Bitmap)B.Clone();
            }
        }

        /// <summary>
        /// Resize image in the size you choose
        /// </summary>
        public static Image Resize(this Bitmap bmSource, Size TargetSize)
        {
            return bmSource.Resize(TargetSize.Width, TargetSize.Height);
        }

        /// <summary>
        /// Resize Image in the size you choose
        /// </summary>
        public static Image Resize(this Image imSource, int TargetWidth, int TargetHeight)
        {
            return ((Bitmap)imSource).Resize(TargetWidth, TargetHeight);
        }

        /// <summary>
        /// Resize Image in the size you choose
        /// </summary>
        public static Image Resize(this Image imSource, Size TargetSize)
        {
            return ((Bitmap)imSource).Resize(TargetSize.Width, TargetSize.Height);
        }

        /// <summary>
        /// Return Image Tinted by a color
        /// </summary>
        public static Bitmap Tint(this Image sourceImage, Color Color)
        {
            return ((Bitmap)sourceImage).Tint(Color);
        }

        /// <summary>
        /// Return image Tinted by a color
        /// </summary>
        public static Bitmap Tint(this Bitmap sourceBitmap, Color Color)
        {
            var sourceData = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            byte[] pixelBuffer = new byte[(sourceData.Stride * sourceData.Height)];
            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);
            float blue;
            float green;
            float red;
            var k = default(int);

            while (k + 4 < pixelBuffer.Length)
            {
                blue = pixelBuffer[k] + (255 - pixelBuffer[k]) * Color.B;
                green = pixelBuffer[k + 1] + (255 - pixelBuffer[k + 1]) * Color.G;
                red = pixelBuffer[k + 2] + (255 - pixelBuffer[k + 2]) * Color.R;

                if (blue > 255f)
                {
                    blue = 255f;
                }

                if (green > 255f)
                {
                    green = 255f;
                }

                if (red > 255f)
                {
                    red = 255f;
                }

                pixelBuffer[k] = (byte)Math.Round(blue);
                pixelBuffer[k + 1] = (byte)Math.Round(green);
                pixelBuffer[k + 2] = (byte)Math.Round(red);
                k += 4;
            }

            var resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);
            var resultData = resultBitmap.LockBits(new Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(pixelBuffer, 0, resultData.Scan0, pixelBuffer.Length);
            resultBitmap.UnlockBits(resultData);
            return resultBitmap;
        }

        /// <summary>
        /// Fade image (Change Opacity)
        /// </summary>
        public static Bitmap Fade(this Bitmap originalBitmap, double opacity)
        {
            if (originalBitmap == null) return null;
            using (Bitmap bmp = new(originalBitmap.Width, originalBitmap.Height))
            {
                using (var gfx = Graphics.FromImage(bmp))
                {
                    var matrix = new ColorMatrix() { Matrix33 = (float)opacity };
                    var attributes = new ImageAttributes();
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                    gfx.DrawImage(originalBitmap, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, originalBitmap.Width, originalBitmap.Height, GraphicsUnit.Pixel, attributes);
                }
                return (Bitmap)bmp.Clone();
            }
        }

        /// <summary>
        /// Fade Image (Change Opacity)
        /// </summary>
        public static Image Fade(this Image originalImage, double opacity)
        {
            return ((Bitmap)originalImage).Fade(opacity);
        }

        /// <summary>
        /// Return image in Grayscale
        /// </summary>
        public static Bitmap Grayscale(this Image original)
        {
            return ((Bitmap)original).Grayscale();
        }

        /// <summary>
        /// Return image in Grayscale
        /// </summary>
        public static Bitmap Grayscale(this Bitmap original)
        {
            var newBitmap = new Bitmap(original.Width, original.Height);

            using (var g = Graphics.FromImage(newBitmap))
            {
                var colorMatrix = new ColorMatrix(new float[][] { new float[] { 0.3f, 0.3f, 0.3f, 0f, 0f }, new float[] { 0.59f, 0.59f, 0.59f, 0f, 0f }, new float[] { 0.11f, 0.11f, 0.11f, 0f, 0f }, new float[] { 0f, 0f, 0f, 1f, 0f }, new float[] { 0f, 0f, 0f, 0f, 1f } });

                using (var attributes = new ImageAttributes())
                {
                    attributes.SetColorMatrix(colorMatrix);
                    g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
                }
            }

            return newBitmap;
        }

        /// <summary>
        /// Return Inverted image
        /// </summary>
        public static Bitmap Invert(this Bitmap bmp)
        {
            Bitmap bmpDest = null;

            using (var bmpSource = new Bitmap(bmp))
            {
                bmpDest = new Bitmap(bmpSource.Width, bmpSource.Height);

                for (int x = 0, loopTo = bmpSource.Width - 1; x <= loopTo; x++)
                {
                    for (int y = 0, loopTo1 = bmpSource.Height - 1; y <= loopTo1; y++)
                    {
                        var clrPixel = bmpSource.GetPixel(x, y);
                        clrPixel = Color.FromArgb(clrPixel.A, 255 - clrPixel.R, 255 - clrPixel.G, 255 - clrPixel.B);
                        bmpDest.SetPixel(x, y, clrPixel);
                    }
                }
            }

            return bmpDest;
        }

        /// <summary>
        /// Fill a bitmap as a tile in specified size
        /// </summary>
        /// <returns></returns>
        public static Bitmap Tile(this Bitmap bmp, Size Size)
        {
            using (var B = new Bitmap(Size.Width, Size.Height))
            {
                var G = Graphics.FromImage(B);
                G.SmoothingMode = SmoothingMode.HighSpeed;
                var tb = new TextureBrush(bmp);
                G.FillRectangle(tb, new Rectangle(0, 0, Size.Width, Size.Height));
                G.Save();
                return (Bitmap)B.Clone();
            }
        }

        /// <summary>
        /// makes circle bitmap from rectangle bitmap
        /// </summary>
        /// <returns></returns>
        public static Bitmap ToCircle(this Bitmap bitmap, bool DrawBorder = true)
        {
            Bitmap dstImage = new(bitmap.Width, bitmap.Height, bitmap.PixelFormat);

            using (Graphics g = Graphics.FromImage(dstImage))
            {
                PointF center = new(bitmap.Width / 2, bitmap.Height / 2);

                RectangleF r = new(center.X - bitmap.Width / 2, center.Y - bitmap.Width / 2,
                                             (bitmap.Height / 2) * 2, (bitmap.Height / 2) * 2);

                g.SmoothingMode = SmoothingMode.AntiAlias;

                g.Clear(Color.Transparent);

                // adds the new ellipse & draws the image again 
                using (GraphicsPath path = new())
                {
                    path.AddEllipse(r);
                    g.SetClip(path);
                    g.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
                    g.ResetClip();
                }

                if (DrawBorder)
                {
                    using (Pen p = new(Color.FromArgb(128, 128, 128)))
                    {
                        g.DrawEllipse(p, r);
                    }
                }

                return dstImage;
            }
        }

        /// <summary>
        /// makes circle bitmap from rectangle image
        /// </summary>
        /// <returns></returns>
        public static Bitmap ToCircle(this Image image, bool DrawBorder = true)
        {
            return ToCircle((Bitmap)image, DrawBorder);
        }

    }

}
