using System;
using System.Drawing;
using System.Text;
using static WinPaletter.NativeMethods.GDI32;

namespace WinPaletter.TypesExtensions
{
    /// <summary>
    /// Extension methods for <see cref="LogFont"/>
    /// </summary>
    public static class LogFontExtensions
    {
        /// <summary>
        /// Converts a logFont object to a byte array.
        /// </summary>
        /// <param name="logFont">The logFont object to convert.</param>
        /// <param name="size">The size of the byte array to return. Default is 92.</param>
        /// <returns>A byte array representation of the logFont object.</returns>
        public static byte[] ToBytes(this LogFont logFont, int size = 92)
        {
            byte[] b = new byte[size];

            // Copy the values of the logFont object to the byte array.
            for (int x = 0; x <= 3; x += +1) b[x] = BitConverter.GetBytes(logFont.lfHeight)[x];

            // Set the rest of the byte array to 0.
            for (int x = 4; x <= 15; x += +1) b[x] = 0;

            // Copy the values of the logFont object to the byte array.
            for (int x = 16; x <= 19; x += +1) b[x] = BitConverter.GetBytes(logFont.lfWeight)[x - 16];

            // Set other values of the byte array.
            b[20] = logFont.lfItalic;
            b[21] = logFont.lfUnderline;
            b[22] = logFont.lfStrikeOut;
            b[23] = logFont.lfCharSet;
            b[24] = logFont.lfOutPrecision;
            b[25] = logFont.lfClipPrecision;
            b[26] = logFont.lfQuality;
            b[27] = logFont.lfClipPrecision;

            int i = 0;

            foreach (byte x in Encoding.Default.GetBytes(logFont.lfFaceName))
            {
                b[28 + i] = x;
                i += 1;
            }

            return b;
        }

        /// <summary>
        /// Creates a <see cref="Font"/> object from a <see cref="LogFont"/> object.
        /// </summary>
        /// <param name="logFont"></param>
        /// <returns></returns>
        public static Font ToFont(this LogFont logFont)
        {
            try
            {
                return Font.FromLogFont(logFont);
            }
            catch
            {
                int deviceDpi = GetSystemDpi();
                float fontSize = Math.Max(-logFont.lfHeight * 72.0f / deviceDpi, 8f);

                return new(logFont.lfFaceName, fontSize, ConvertLogFontStyle(logFont));
            }
        }

        private static FontStyle ConvertLogFontStyle(LogFont logFont)
        {
            FontStyle style = FontStyle.Regular;

            if (logFont.lfWeight >= 700)
                style |= FontStyle.Bold;

            if (logFont.lfItalic != 0)
                style |= FontStyle.Italic;

            if (logFont.lfUnderline != 0)
                style |= FontStyle.Underline;

            if (logFont.lfStrikeOut != 0)
                style |= FontStyle.Strikeout;

            return style;
        }

        private static int GetSystemDpi()
        {
            using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
            {
                return (int)graphics.DpiX;
            }
        }
    }
}
