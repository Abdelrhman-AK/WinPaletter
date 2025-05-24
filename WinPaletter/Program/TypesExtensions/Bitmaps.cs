using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace WinPaletter.TypesExtensions
{
    /// <summary>
    /// Extensions for <see cref="Bitmap"/> class
    /// </summary>
    public static class BitmapExtensions
    {
        /// <summary>
        /// Return most used color from a bitmap
        /// </summary>
        public static Color AverageColor(this Bitmap bitmap)
        {
            if (bitmap is null || bitmap.Width == 0 || bitmap.Height == 0) return Color.Empty;

            try
            {
                int totalR = 0, totalG = 0, totalB = 0;

                for (int x = 0; x < bitmap.Width; x++)
                {
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        Color pixel = bitmap.GetPixel(x, y);
                        totalR += pixel.R;
                        totalG += pixel.G;
                        totalB += pixel.B;
                    }
                }

                int totalPixels = bitmap.Height * bitmap.Width;
                int averageR = totalR / totalPixels;
                int averageG = totalG / totalPixels;
                int averageB = totalB / totalPixels;

                return Color.FromArgb(averageR, averageG, averageB);
            }
            catch { return Color.Empty; }
        }

        /// <summary>
        /// Return most used color from an image
        /// </summary>
        public static Color AverageColor(this Image Image)
        {
            if (Image is Bitmap bitmap) return bitmap.AverageColor();
            return Color.Empty;
        }

        /// <summary>
        /// Return Blurred image
        /// </summary>
        public static Bitmap Blur(this Bitmap bitmap, float blurForce = 2.0f)
        {
            if (bitmap is null) return null;
            if (bitmap.Size.Width == 0 || bitmap.Size.Height == 0) return null;

            Bitmap img = new(bitmap);
            int imgWidth = img.Width;
            int imgHeight = img.Height;

            using (Graphics G = Graphics.FromImage(img))
            using (ImageAttributes att = new())
            {
                G.SmoothingMode = SmoothingMode.AntiAlias;

                // Create a color matrix for the blur effect
                ColorMatrix m = new() { Matrix33 = 0.4f };
                att.SetColorMatrix(m);

                // Loop through the blur force and draw the image in x-coordinates with a slight offset
                for (float x = -blurForce; x <= blurForce; x += 0.5f)
                {
                    PointF[] destPointsX = [new(x, 0), new(x + imgWidth, 0), new(x, imgHeight)];
                    RectangleF srcRectX = new(0, 0, imgWidth, imgHeight);
                    G.DrawImage(img, destPointsX, srcRectX, GraphicsUnit.Pixel, att);
                }

                // Loop through the blur force and draw the image in y-coordinates with a slight offset
                for (float y = -blurForce; y <= blurForce; y += 0.5f)
                {
                    PointF[] destPointsY = [new(0, y), new(imgWidth, y), new(0, y + imgHeight)];
                    RectangleF srcRectY = new(0, 0, imgWidth, imgHeight);
                    G.DrawImage(img, destPointsY, srcRectY, GraphicsUnit.Pixel, att);
                }

                G.Save();

                return img;
            }
        }

        /// <summary>
        /// Return noised bitmap (noise of Windows 10 Acrylic style or Windows 7 Aero glass)
        /// </summary>
        public static Bitmap Noise(this Bitmap bitmap, NoiseMode NoiseMode, float opacity)
        {
            if (bitmap is null) return null;
            Bitmap img = new(bitmap);

            using (Graphics G = Graphics.FromImage(img))
            {
                G.SmoothingMode = SmoothingMode.AntiAlias;

                if (NoiseMode == NoiseMode.Acrylic)
                {
                    using (Bitmap b = Properties.Resources.Noise.Fade(opacity))
                    using (TextureBrush br = new(b))
                    {
                        G.FillRectangle(br, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                    }
                }
                else if (NoiseMode == NoiseMode.Aero)
                {
                    using (Bitmap b = Assets.Win7Preview.AeroGlass.Fade(opacity))
                    {
                        G.DrawImage(b, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                    }
                }

                G.Save();
            }

            return img;
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
        /// Replace color in image (pixels) by a color
        /// </summary>
        public static Bitmap ReplaceColor(this Bitmap inputImage, Color oldColor, Color NewColor)
        {
            Bitmap outputImage = new(inputImage.Width, inputImage.Height);
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
                return outputImage;
            }
        }

        /// <summary>
        /// Return bitmap filled in the scale of size you choose (looks like Windows 7+ fill wallpaper style)
        /// </summary>
        public static Bitmap FillScale(this Bitmap bitmap, Size size)
        {
            try
            {
                // Check if the input bitmap is null
                if (bitmap == null) return bitmap;

                // Get the current screen size
                Size screenSize = Screen.PrimaryScreen.Bounds.Size;

                // Calculate scaling factors for width and height to maintain aspect ratio
                decimal nPercentW = screenSize.Width / (decimal)bitmap.Width;
                decimal nPercentH = screenSize.Height / (decimal)bitmap.Height;

                // Choose the maximum scaling factor to maintain the aspect ratio and fill the screen
                decimal nPercent = Math.Max(nPercentW, nPercentH);

                // Calculate the destination width and height based on the scaling factor
                int destWidth = (int)Math.Round(bitmap.Width * nPercent);
                int destHeight = (int)Math.Round(bitmap.Height * nPercent);

                // Create a new bitmap with the screen size and resolution
                Bitmap bmPhoto = new(screenSize.Width, screenSize.Height, PixelFormat.Format32bppArgb);

                bmPhoto.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);

                // Calculate the starting position for centering the image on the screen
                int x = Math.Max((screenSize.Width - destWidth) / 2, 0);
                int y = Math.Max((screenSize.Height - destHeight) / 2, 0);

                // Create a graphics object from the new bitmap
                using (Graphics grPhoto = Graphics.FromImage(bmPhoto))
                {
                    // Set the interpolation mode for better quality scaling
                    grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    // Draw the original image onto the new bitmap with the calculated size and center it on the screen
                    grPhoto.DrawImage(bitmap, new Rectangle(x, y, destWidth, destHeight));
                }

                // Return the final image that fills the screen
                return bmPhoto.Resize(size);
            }
            catch
            {
                // Return original bitmap in case of an exception
                return bitmap;
            }
        }

        /// <summary>
        /// Resize image in the size you choose
        /// </summary>
        public static Bitmap Resize(this Bitmap bmSource, int TargetWidth, int TargetHeight)
        {
            if (bmSource is null) return null;

            Bitmap B = new(TargetWidth, TargetHeight, PixelFormat.Format32bppArgb);
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

            return B;
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
        /// Return image tinted by a color
        /// </summary>
        public static Bitmap Tint(this Bitmap originalBitmap, Color tintColor)
        {
            // Create a color matrix for the tint effect
            float[][] matrixElements = [
            [tintColor.R / 255f, 0, 0, 0, 0],
            [0, tintColor.G / 255f, 0, 0, 0],
            [0, 0, tintColor.B / 255f, 0, 0],
            [0, 0, 0, tintColor.A / 255f, 0],
            [0, 0, 0, 0, 1]
        ];

            ColorMatrix colorMatrix = new(matrixElements);

            // Apply the color matrix to an image attributes object
            ImageAttributes imageAttributes = new();
            imageAttributes.SetColorMatrix(colorMatrix);

            // Create a destination bitmap with the same size as the original
            Bitmap tintedBitmap = new(originalBitmap.Width, originalBitmap.Height);

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

            Bitmap bmp = new(originalBitmap.Width, originalBitmap.Height);
            using (Graphics G = Graphics.FromImage(bmp))
            {
                ColorMatrix matrix = new() { Matrix33 = opacity };
                ImageAttributes attributes = new();
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                G.DrawImage(originalBitmap, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, originalBitmap.Width, originalBitmap.Height, GraphicsUnit.Pixel, attributes);

                return bmp;
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
        /// Return bitmap in grayscale
        /// </summary>
        public static Bitmap Grayscale(this Bitmap original)
        {
            Bitmap newBitmap = new(original.Width, original.Height);

            using (Graphics G = Graphics.FromImage(newBitmap))
            {
                ColorMatrix colorMatrix = new([[0.3f, 0.3f, 0.3f, 0f, 0f], [0.59f, 0.59f, 0.59f, 0f, 0f], [0.11f, 0.11f, 0.11f, 0f, 0f], [0f, 0f, 0f, 1f, 0f], [0f, 0f, 0f, 0f, 1f]]);

                using (ImageAttributes attributes = new())
                {
                    attributes.SetColorMatrix(colorMatrix);
                    G.DrawImage(original, new(0, 0, original.Width, original.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
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
            Bitmap B = new(Size.Width, Size.Height);
            using (Graphics G = Graphics.FromImage(B))
            {
                G.SmoothingMode = SmoothingMode.HighSpeed;
                TextureBrush tb = new(bmp);
                G.FillRectangle(tb, new Rectangle(0, 0, Size.Width, Size.Height));
                G.Save();
            }

            return B;
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

        /// <summary>
        /// Split bitmap into parts, provided by the number of parts.
        /// <br>This is used for splitting visual styles bitmaps.</br>
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="parts"></param>
        /// <returns></returns>
        public static List<Bitmap> Split(this Bitmap bitmap, int parts)
        {
            if (bitmap is null) return null;
            if (parts < 1) return null;

            List<Bitmap> list = [];
            int partHeight = bitmap.Height / parts;

            for (int i = 0; i < parts; i++)
            {
                int y = i * partHeight;
                int h = i == parts - 1 ? bitmap.Height - y : partHeight;
                list.Add(bitmap.Clone(new Rectangle(0, y, bitmap.Width, h), bitmap.PixelFormat));
            }

            return list;
        }
    }
}