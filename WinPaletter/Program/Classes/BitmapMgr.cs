﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace WinPaletter
{
    /// <summary>
    /// Optimized Bitmap Manager for loading bitmaps without file locks and with better performance
    /// </summary>
    public static class BitmapMgr
    {
        /// <summary>
        /// Loads a bitmap from file without locking the file and with optimized performance
        /// </summary>
        /// <param name="filePath">Path to the image file</param>
        /// <returns>Loaded Bitmap or null if failed</returns>
        public static Bitmap Load(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                Program.Log?.Debug("File to be loaded as image is not found or path is invalid: {file}", filePath);
                return null;
            }

            try
            {
                // Read all bytes first to immediately release file lock
                byte[] fileBytes;
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (var memoryStream = new MemoryStream())
                {
                    fileStream.CopyTo(memoryStream);
                    fileBytes = memoryStream.ToArray();
                }

                // Create bitmap from memory
                using (var memoryStream = new MemoryStream(fileBytes))
                {
                    // Use FromStream instead of Image.FromStream to avoid keeping stream open
                    var bitmap = (Bitmap)Image.FromStream(memoryStream, false, false);

                    // Create a new bitmap to ensure we have full control and can optimize format
                    var optimizedBitmap = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);

                    using (var g = Graphics.FromImage(optimizedBitmap))
                    {
                        g.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
                    }

                    bitmap?.Dispose();
                    return optimizedBitmap;
                }
            }
            catch (OutOfMemoryException ex)
            {
                Program.Log?.Error(ex, "Out of memory while loading image: {file}", filePath);
                return null;
            }
            catch (FileNotFoundException ex)
            {
                Program.Log?.Error(ex, "Image file is not found: {file}", filePath);
                return null;
            }
            catch (Exception ex)
            {
                Program.Log?.Error(ex, "Error loading image: {file}", filePath);
                return null;
            }
        }
    }
}