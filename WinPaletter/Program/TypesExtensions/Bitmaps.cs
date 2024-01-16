using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace WinPaletter.TypesExtensions
{
    public static class BitmapExtensions
    {
        /// <summary>
        /// Return most used color from a bitmap
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
                                    Color pixel = bmp.GetPixel(x, y);
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
        /// Return most used color from an image
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

                    ImageAttributes att = new();
                    ColorMatrix m = new() { Matrix33 = 0.4f };
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
        /// Return noised bitmap (noise of Windows 10 Acrylic style or Windows 7 Aero glass)
        /// </summary>
        public static Bitmap Noise(this Bitmap bmp, NoiseMode NoiseMode, float opacity)
        {
            try
            {
                Graphics G = Graphics.FromImage(bmp);

                if (NoiseMode == NoiseMode.Acrylic)
                {
                    using (Bitmap b = Properties.Resources.Noise.Fade(opacity))
                    using (TextureBrush br = new(b))
                    {
                        G.FillRectangle(br, new Rectangle(0, 0, bmp.Width, bmp.Height));
                    }
                }
                else if (NoiseMode == NoiseMode.Aero)
                {
                    using (Bitmap b = Assets.Win7Preview.AeroGlass.Fade(opacity))
                    {
                        G.DrawImage(b, new Rectangle(0, 0, bmp.Width, bmp.Height));
                    }
                }

                G.Save();
                G.Dispose();
                return bmp;
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
        /// Replace color in image (pixels) by a color
        /// </summary>
        public static Bitmap ReplaceColor(this Bitmap inputImage, Color oldColor, Color NewColor)
        {
            using (Bitmap outputImage = new(inputImage.Width, inputImage.Height))
            using (Graphics G = Graphics.FromImage(outputImage))
            {
                for (int y = 0, loopTo = inputImage.Height - 1; y <= loopTo; y++)
                {
                    for (int x = 0, loopTo1 = inputImage.Width - 1; x <= loopTo1; x++)
                    {
                        Color PixelColor = inputImage.GetPixel(x, y);

                        if (Color.FromArgb(255, PixelColor) == Color.FromArgb(255, oldColor))
                        {
                            outputImage.SetPixel(x, y, Color.FromArgb(PixelColor.A, NewColor));
                        }
                        else
                        {
                            outputImage.SetPixel(x, y, PixelColor);
                        }

                    }
                }

                G.DrawImage(outputImage, 0, 0);
                return outputImage.Clone() as Bitmap;
            }
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
                decimal nPercent = 0;
                decimal nPercentW = 0;
                decimal nPercentH = 0;
                nPercentW = Size.Width / (decimal)sourceWidth;
                nPercentH = Size.Height / (decimal)sourceHeight;

                if (nPercentH < nPercentW)
                {
                    nPercent = nPercentH;
                    Convert.ToInt16((Size.Width - sourceWidth * nPercent) / 2);
                }
                else
                {
                    nPercent = nPercentW;
                    Convert.ToInt16((Size.Height - sourceHeight * nPercent) / 2);
                }

                int destWidth = (int)Math.Round(sourceWidth * nPercent);
                int destHeight = (int)Math.Round(sourceHeight * nPercent);

                Bitmap bmPhoto = new(Size.Width, Size.Height, PixelFormat.Format32bppArgb);
                bmPhoto.SetResolution(Bitmap.HorizontalResolution, Bitmap.VerticalResolution);
                Graphics grPhoto = Graphics.FromImage(bmPhoto);
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
            if (bmSource is null) return null;

            using (Bitmap B = new(TargetWidth, TargetHeight, PixelFormat.Format32bppArgb))
            {
                using (Graphics G = Graphics.FromImage(B))
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
                return B.Clone() as Bitmap;
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
        /// Return image tinted by a color
        /// </summary>
        public static Bitmap Tint(this Bitmap originalBitmap, Color tintColor)
        {
            // Create a color matrix for the tint effect
            float[][] matrixElements = {
            new float[] {tintColor.R / 255f, 0, 0, 0, 0},
            new float[] {0, tintColor.G / 255f, 0, 0, 0},
            new float[] {0, 0, tintColor.B / 255f, 0, 0},
            new float[] {0, 0, 0, tintColor.A / 255f, 0},
            new float[] {0, 0, 0, 0, 1}
        };

            ColorMatrix colorMatrix = new(matrixElements);

            // Apply the color matrix to an image attributes object
            ImageAttributes imageAttributes = new();
            imageAttributes.SetColorMatrix(colorMatrix);

            // Create a destination bitmap with the same size as the original
            Bitmap tintedBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height);

            // Draw the original bitmap onto the destination bitmap using the image attributes
            using (Graphics g = Graphics.FromImage(tintedBitmap))
            {
                g.DrawImage(originalBitmap,
                    new Rectangle(0, 0, originalBitmap.Width, originalBitmap.Height),
                    0, 0, originalBitmap.Width, originalBitmap.Height,
                    GraphicsUnit.Pixel, imageAttributes);
            }

            return tintedBitmap;
        }

        /// <summary>
        /// Fade bitmap (change opacity)
        /// </summary>
        public static Bitmap Fade(this Bitmap originalBitmap, float opacity)
        {
            if (originalBitmap == null) return null;

            using (Bitmap bmp = new(originalBitmap.Width, originalBitmap.Height))
            using (Graphics G = Graphics.FromImage(bmp))
            {
                ColorMatrix matrix = new() { Matrix33 = opacity };
                ImageAttributes attributes = new();
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                G.DrawImage(originalBitmap, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, originalBitmap.Width, originalBitmap.Height, GraphicsUnit.Pixel, attributes);
                return bmp.Clone() as Bitmap;

            }
        }

        /// <summary>
        /// Fade image (change opacity)
        /// </summary>
        public static Image Fade(this Image originalImage, float opacity)
        {
            return originalImage.Fade(opacity);
        }

        /// <summary>
        /// Return image in grayscale
        /// </summary>
        public static Bitmap Grayscale(this Image original)
        {
            return ((Bitmap)original).Grayscale();
        }

        /// <summary>
        /// Return bitmap in grayscale
        /// </summary>
        public static Bitmap Grayscale(this Bitmap original)
        {
            Bitmap newBitmap = new(original.Width, original.Height);

            using (Graphics G = Graphics.FromImage(newBitmap))
            {
                ColorMatrix colorMatrix = new(new float[][] { new float[] { 0.3f, 0.3f, 0.3f, 0f, 0f }, new float[] { 0.59f, 0.59f, 0.59f, 0f, 0f }, new float[] { 0.11f, 0.11f, 0.11f, 0f, 0f }, new float[] { 0f, 0f, 0f, 1f, 0f }, new float[] { 0f, 0f, 0f, 0f, 1f } });

                using (ImageAttributes attributes = new())
                {
                    attributes.SetColorMatrix(colorMatrix);
                    G.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
                }
            }

            return newBitmap;
        }

        /// <summary>
        /// Return inverted bitmap
        /// </summary>
        public static Bitmap Invert(this Bitmap bmp)
        {
            Bitmap bmpDest = null;

            using (Bitmap bmpSource = new(bmp))
            {
                bmpDest = new(bmpSource.Width, bmpSource.Height);

                for (int x = 0, loopTo = bmpSource.Width - 1; x <= loopTo; x++)
                {
                    for (int y = 0, loopTo1 = bmpSource.Height - 1; y <= loopTo1; y++)
                    {
                        Color clrPixel = bmpSource.GetPixel(x, y);
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
            using (Bitmap B = new(Size.Width, Size.Height))
            {
                Graphics G = Graphics.FromImage(B);
                G.SmoothingMode = SmoothingMode.HighSpeed;
                TextureBrush tb = new(bmp);
                G.FillRectangle(tb, new Rectangle(0, 0, Size.Width, Size.Height));
                G.Save();
                return (Bitmap)B.Clone();
            }
        }

        /// <summary>
        /// Make circular bitmap from a rectangular bitmap
        /// </summary>
        /// <returns></returns>
        public static Bitmap ToCircle(this Bitmap bitmap, bool DrawBorder = true)
        {
            Bitmap dstImage = new(bitmap.Width, bitmap.Height, bitmap.PixelFormat);
            Rectangle Rect = new(0, 0, bitmap.Width - 1, bitmap.Height - 1);

            using (Graphics g = Graphics.FromImage(dstImage))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                g.Clear(Color.Transparent);

                // adds the new ellipse & draws the image again 
                using (GraphicsPath path = new())
                {
                    path.AddEllipse(Rect);
                    g.SetClip(path);
                    g.DrawImage(bitmap, Rect);
                    g.ResetClip();
                }

                if (DrawBorder)
                {
                    using (Pen p = new(Color.FromArgb(128, 128, 128)))
                    {
                        g.DrawEllipse(p, Rect);
                    }
                }

                return dstImage;
            }
        }
    }
}
