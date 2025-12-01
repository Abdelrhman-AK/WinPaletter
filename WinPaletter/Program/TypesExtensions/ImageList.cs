using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace WinPaletter.TypesExtensions
{
    public static class ImageListExtensions
    {
        /// <summary>
        /// Adds an Image to the ImageList after converting it to 32-bit ARGB to preserve transparency.
        /// </summary>
        public static void AddWithAlpha(this ImageList list, Image image)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (image == null) throw new ArgumentNullException(nameof(image));

            list.ColorDepth = ColorDepth.Depth32Bit;

            Bitmap bmp = new(image.Width, image.Height, PixelFormat.Format32bppArgb);
            using (Graphics G = Graphics.FromImage(bmp))
            {
                G.Clear(Color.Transparent);
                G.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                G.DrawImage(image, 0, 0, image.Width, image.Height);
            }

            list.Images.Add(bmp);
        }

        /// <summary>
        /// Adds an Image with a key to the ImageList after converting to 32-bit ARGB.
        /// </summary>
        public static int AddWithAlpha(this ImageList list, string key, Image image)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (image == null) throw new ArgumentNullException(nameof(image));

            list.ColorDepth = ColorDepth.Depth32Bit;

            Bitmap bmp = new(image.Width, image.Height, PixelFormat.Format32bppArgb);
            using (Graphics G = Graphics.FromImage(bmp))
            {
                G.Clear(Color.Transparent);
                G.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                G.DrawImage(image, 0, 0, image.Width, image.Height);
            }

            list.Images.Add(key, bmp);
            return list.Images.IndexOfKey(key);
        }

        /// <summary>
        /// Adds multiple images (no keys) to the ImageList with proper ARGB conversion.
        /// </summary>
        public static void AddRangeWithAlpha(this ImageList list, IEnumerable<Image> images)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (images == null) return;

            foreach (var img in images) list.AddWithAlpha(img);
        }

        /// <summary>
        /// Adds multiple key-image pairs to the ImageList with proper ARGB conversion.
        /// </summary>
        public static void AddRangeWithAlpha(this ImageList list, IDictionary<string, Image> items)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (items == null) return;

            foreach (var kv in items) list.AddWithAlpha(kv.Key, kv.Value);
        }
    }
}
