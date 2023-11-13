using System.Drawing;

namespace WinPaletter.TypesExtensions
{
    /// <summary>
    /// Extension methods for converting Font objects to byte arrays.
    /// </summary>
    public static class FontExtensions
    {
        /// <summary>
        /// Converts a Font object to a byte array.
        /// </summary>
        /// <param name="font">The Font object to convert.</param>
        /// <returns>A byte array representation of the Font object.</returns>
        public static byte[] ToByte(this Font font)
        {
            NativeMethods.GDI32.LogFont lf = new();
            font.ToLogFont(lf);
            return lf.ToByte();
        }
    }
}
