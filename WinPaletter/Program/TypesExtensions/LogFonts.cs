using System;
using System.Text;

namespace WinPaletter.TypesExtensions
{
    /// <summary>
    /// Extension methods for converting LogFont objects to byte arrays.
    /// </summary>
    public static class LogFontExtensions
    {
        /// <summary>
        /// Converts a LogFont object to a byte array.
        /// </summary>
        /// <param name="LogFont">The LogFont object to convert.</param>
        /// <returns>A byte array representation of the LogFont object.</returns>
        public static byte[] ToByte(this NativeMethods.GDI32.LogFont LogFont)
        {
            byte[] b = new byte[92];

            for (int x = 0; x <= 3; x += +1)
                b[x] = BitConverter.GetBytes(LogFont.lfHeight)[x];

            for (int x = 4; x <= 15; x += +1)
                b[x] = 0;

            for (int x = 16; x <= 19; x += +1)
                b[x] = BitConverter.GetBytes(LogFont.lfWeight)[x - 16];

            b[20] = LogFont.lfItalic;
            b[21] = LogFont.lfUnderline;
            b[22] = LogFont.lfStrikeOut;
            b[23] = LogFont.lfCharSet;
            b[24] = LogFont.lfOutPrecision;
            b[25] = LogFont.lfClipPrecision;
            b[26] = LogFont.lfQuality;
            b[27] = LogFont.lfClipPrecision;

            int i = 0;

            foreach (byte x in Encoding.Unicode.GetBytes(LogFont.lfFaceName))
            {
                b[28 + i] = x;
                i += 1;
            }

            return b;
        }
    }
}
