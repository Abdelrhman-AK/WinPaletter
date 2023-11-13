using System;
using System.Drawing;
using System.Text;

namespace WinPaletter.TypesExtensions
{
    /// <summary>
    /// Extension methods for working with byte arrays.
    /// </summary>
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Converts a byte array to a <see cref="NativeMethods.GDI32.LogFont"/> structure.
        /// </summary>
        /// <param name="fontBytes">The byte array containing font information.</param>
        /// <returns>A <see cref="NativeMethods.GDI32.LogFont"/> structure representing the font.</returns>
        public static NativeMethods.GDI32.LogFont ToLogFont(this byte[] fontBytes)
        {
            NativeMethods.GDI32.LogFont lOGFONT = new()
            {
                lfHeight = BitConverter.ToInt32(fontBytes, 0),
                lfWidth = 0,
                lfEscapement = 0,
                lfOrientation = 0,
                lfWeight = BitConverter.ToInt32(fontBytes, 16),
                lfItalic = fontBytes[20],
                lfUnderline = fontBytes[21],
                lfStrikeOut = fontBytes[22],
                lfCharSet = fontBytes[23],
                lfOutPrecision = fontBytes[24],
                lfClipPrecision = fontBytes[25],
                lfQuality = fontBytes[26]
            };
            lOGFONT.lfClipPrecision = fontBytes[27];
            byte[] array = new byte[64];

            for (int i = 0; i <= 64 - 1; i++)
                array[i] = fontBytes[i + 28];

            lOGFONT.lfFaceName = Encoding.Unicode.GetString(array).TrimEnd(null);
            return lOGFONT;
        }

        /// <summary>
        /// Converts a byte array to a <see cref="Font"/> object.
        /// </summary>
        /// <param name="fontBytes">The byte array containing font information.</param>
        /// <returns>A <see cref="Font"/> object representing the font.</returns>
        public static Font ToFont(this byte[] fontBytes)
        {
            return Font.FromLogFont(fontBytes.ToLogFont());
        }

        /// <summary>
        /// Compares two byte arrays for equality.
        /// </summary>
        /// <param name="array1">The first byte array to compare.</param>
        /// <param name="array2">The second byte array to compare.</param>
        /// <returns>True if the byte arrays are equal; otherwise, false.</returns>
        public static bool Equals_Method2(this byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
