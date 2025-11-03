using System;
using System.Drawing;
using System.IO;

namespace WinPaletter.TypesExtensions
{
    public static class IconsExtensions
    {
        /// <summary>
        /// Convert Icon to byte array
        /// </summary>
        public static byte[] ToBytes(this Icon icon)
        {
            using MemoryStream ms = new();
            icon.Save(ms);
            return ms.ToArray();
        }

        /// <summary>
        /// Convert byte array to Icon
        /// </summary>
        public static Icon ToIcon(this byte[] bytes)
        {
            using MemoryStream ms = new(bytes);
            return new Icon(ms);
        }

        public static Icon FromSize(this Icon icon, int desiredSize)
        {
            if (icon == null) throw new ArgumentNullException(nameof(icon));

            byte[] src = icon.ToBytes();

            // Check if this is a "real ICO" with multiple images
            int count = BitConverter.ToUInt16(src, 4);
            if (count <= 1)
            {
                // Single image (likely PE-extracted), just return a clone
                return (Icon)icon.Clone();
            }

            int bestIndex = 0;
            int smallestDiff = int.MaxValue;
            int bestQuality = 0;

            for (int i = 0; i < count; i++)
            {
                int width = src[6 + (16 * i) + 0];
                int height = src[6 + (16 * i) + 1];
                if (width == 0) width = 256;
                if (height == 0) height = 256;
                int diff = Math.Abs(width - desiredSize) + Math.Abs(height - desiredSize);

                int length = BitConverter.ToInt32(src, 6 + (16 * i) + 8);
                int offset = BitConverter.ToInt32(src, 6 + (16 * i) + 12);

                int quality = 0;
                if (length >= 4)
                {
                    // PNG signature
                    if (src[offset] == 0x89 && src[offset + 1] == 0x50 &&
                        src[offset + 2] == 0x4E && src[offset + 3] == 0x47)
                    {
                        quality = 32; // treat PNG as 32-bit
                    }
                    else if (length >= 26) // BMP header inside ICO
                    {
                        quality = BitConverter.ToUInt16(src, offset + 14);
                    }
                }

                // pick closest size first, then highest color depth
                if (diff < smallestDiff || (diff == smallestDiff && quality > bestQuality))
                {
                    bestIndex = i;
                    smallestDiff = diff;
                    bestQuality = quality;
                }
            }

            // Extract only the chosen icon
            int chosenLength = BitConverter.ToInt32(src, 6 + (16 * bestIndex) + 8);
            int chosenOffset = BitConverter.ToInt32(src, 6 + (16 * bestIndex) + 12);

            using MemoryStream ms = new(6 + 16 + chosenLength);
            using BinaryWriter bw = new(ms);

            bw.Write(src, 0, 4);
            bw.Write((short)1);
            bw.Write(src, 6 + (16 * bestIndex), 12);
            bw.Write(22);
            bw.Write(src, chosenOffset, chosenLength);
            ms.Seek(0, SeekOrigin.Begin);

            return new Icon(ms);
        }
    }
}
