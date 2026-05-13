using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace WinPaletter
{
    /// <summary>
    /// Optimized Thumbnail Manager for loading bitmaps without file locks and with better performance
    /// </summary>
    public static class BitmapMgr
    {
        /// <summary>
        /// Loads a bitmap from file without locking the file and with optimized performance
        /// </summary>
        /// <param name="filePath">FileSystem to the image file</param>
        /// <returns>Loaded Thumbnail or null if failed</returns>
        /// <summary>
        /// Loads a bitmap from file without locking the file and optimized for huge files.
        /// For extremely large images, creates a lightweight bitmap and avoids memory spikes.
        /// </summary>
        /// <param name="filePath">Path to the image file</param>
        /// <param name="maxDimension">Optional max dimension to downscale extremely large images</param>
        /// <returns>Loaded Bitmap or null if failed</returns>
        public static Bitmap Load(string filePath, int maxDimension = 4000)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath)) return null;

            try
            {
                // Open the file stream with shared read access
                using FileStream fs = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                using Image img = Image.FromStream(fs, useEmbeddedColorManagement: false, validateImageData: false);

                int width = img.Width;
                int height = img.Height;

                // Progressive downscale if image is huge
                if (Math.Max(width, height) > maxDimension)
                {
                    float scale = (float)maxDimension / Math.Max(width, height);
                    width = (int)(width * scale);
                    height = (int)(height * scale);
                }

                Bitmap bmp = new(width, height, PixelFormat.Format32bppArgb);
                bmp.SetResolution(img.HorizontalResolution, img.VerticalResolution);

                using Graphics g = Graphics.FromImage(bmp);
                g.CompositingMode = CompositingMode.SourceOver;
                g.CompositingQuality = CompositingQuality.HighSpeed;
                g.InterpolationMode = InterpolationMode.Low;
                g.SmoothingMode = SmoothingMode.None;
                g.PixelOffsetMode = PixelOffsetMode.None;

                g.DrawImage(img, new Rectangle(0, 0, width, height));

                return bmp;
            }
            catch (OutOfMemoryException)
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Out of memory while loading image: {filePath}");
                return null;
            }
            catch (FileNotFoundException)
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Image file not found: {filePath}");
                return null;
            }
            catch (Exception ex)
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Error loading image: {filePath}", ex);
                return null;
            }
        }

        /// <summary>
        /// Generates a fast thumbnail from a file, minimizing memory usage even for huge images.
        /// Uses low-quality downscaling to avoid freezing the UI or using excessive RAM.
        /// Thread-safe for background processing.
        /// </summary>
        /// <param name="filePath">Path to the image file.</param>
        /// <param name="targetWidth">Desired thumbnail width.</param>
        /// <param name="targetHeight">Desired thumbnail height.</param>
        /// <returns>Thumbnail bitmap, or null if file does not exist or is invalid.</returns>
        public static Bitmap Thumbnail(string filePath, int targetWidth, int targetHeight)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath)) return null;

            try
            {
                using FileStream fs = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                using Bitmap src = new(fs);

                // Fast path: extract embedded EXIF thumbnail (JPEG from cameras/editors)
                // Property 0x501B = PropertyTagThumbnailData
                PropertyItem exifThumb = Array.Find(src.PropertyItems, p => p.Id == 0x501B);
                if (exifThumb != null)
                {
                    try
                    {
                        using MemoryStream ms = new(exifThumb.Value);
                        using Bitmap embedded = new(ms);
                        return RenderThumbnail(embedded, targetWidth, targetHeight);
                    }
                    catch { /* Corrupt EXIF data — fall through to GetThumbnailImage */ }
                }

                // Standard path: let GDI+ pick the best internal strategy
                try
                {
                    Image thumbImg = src.GetThumbnailImage(targetWidth, targetHeight, () => false, IntPtr.Zero);
                    Bitmap result = new(thumbImg);
                    thumbImg.Dispose();
                    return result;
                }
                catch { /* No HWND context (background thread) or unsupported format — fall through */ }

                // Fallback: full manual downscale
                return RenderThumbnail(src, targetWidth, targetHeight);
            }
            catch (Exception ex)
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Error generating thumbnail: {filePath}", ex);
                return null;
            }
        }

        private static Bitmap RenderThumbnail(Bitmap src, int targetWidth, int targetHeight)
        {
            float scale = Math.Min((float)targetWidth / src.Width, (float)targetHeight / src.Height);
            int drawW = (int)(src.Width * scale);
            int drawH = (int)(src.Height * scale);
            int offsetX = (targetWidth - drawW) / 2;
            int offsetY = (targetHeight - drawH) / 2;

            Bitmap thumb = new(targetWidth, targetHeight, PixelFormat.Format32bppArgb);
            using Graphics G = Graphics.FromImage(thumb);
            G.InterpolationMode = InterpolationMode.Low;
            G.CompositingMode = CompositingMode.SourceCopy;
            G.CompositingQuality = CompositingQuality.HighSpeed;
            G.SmoothingMode = SmoothingMode.None;
            G.PixelOffsetMode = PixelOffsetMode.None;
            G.Clear(Color.Transparent);
            G.DrawImage(src, new Rectangle(offsetX, offsetY, drawW, drawH));
            return thumb;
        }

        /// <summary>
        /// Generates a fast thumbnail from a file, minimizing memory usage even for huge images.
        /// Uses low-quality downscaling to avoid freezing the UI or using excessive RAM.
        /// Thread-safe for background processing.
        /// </summary>
        /// <param name="filePath">Path to the image file.</param>
        /// <param name="size">Desired thumbnail size.</param>
        /// <returns>Thumbnail bitmap, or null if file does not exist or is invalid.</returns>
        public static Bitmap Thumbnail(string filePath, Size size) => Thumbnail(filePath, size.Width, size.Height);
    }
}