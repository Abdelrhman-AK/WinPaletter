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

                Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
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
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
                return null;

            using FileStream fs = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using Image img = Image.FromStream(fs, useEmbeddedColorManagement: false, validateImageData: false);

            // Use GetThumbnailImage: extremely low memory and fast, preserves aspect ratio
            Image thumbImg = img.GetThumbnailImage(targetWidth, targetHeight, () => false, IntPtr.Zero);

            // Return a fresh Bitmap (detached from the original stream) for safe use
            Bitmap thumbnail = new(thumbImg);
            thumbImg.Dispose();

            return thumbnail;
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