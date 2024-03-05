using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WinPaletter.TypesExtensions
{
    /// <summary>
    /// Extension methods for working with byte arrays.
    /// </summary>
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Get a subarray of the byte array, provided by the start index and the length.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static byte[] SubArray(this byte[] source, int startIndex, int length)
        {
            if (startIndex < 0 || startIndex >= source.Length || length <= 0 || startIndex + length > source.Length)
            {
                throw new ArgumentException("Invalid startIndex or length");
            }

            byte[] result = new byte[length];
            Array.Copy(source, startIndex, result, 0, length);
            return result;
        }

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
                lfQuality = fontBytes[26],
                lfPitchAndFamily = fontBytes[27]
            };

            byte[] array = new byte[fontBytes.Length - 28];

            for (int i = 0; i <= array.Length - 1; i++) array[i] = fontBytes[i + 28];

            lOGFONT.lfFaceName = Encoding.Default.GetString(array).TrimEnd(null);

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

        /// <summary>
        /// Converts a byte array to the struct type specified by the template parameter
        /// </summary>
        /// <typeparam name="T">The type of the struct</typeparam>
        /// <param name="data">The byte array containing the serialized data</param>
        /// <returns>The deserialized struct</returns>
        public static T ToObject<T>(this byte[] data)
        {
            // Check if the data array is large enough to hold the structure
            if (data.Length < Marshal.SizeOf(typeof(T)))
            {
                int pendingLength = Marshal.SizeOf(typeof(T)) - data.Length;

                List<byte> bytes = data.ToList();
                for (int i = 0; i < pendingLength; i++)
                {
                    bytes.Add(0);
                }
                data = bytes.ToArray();
            }

            // Pin the managed memory while, copy out the data, then unpin it
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            T theStructure = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();

            return theStructure;
        }
    }
}
