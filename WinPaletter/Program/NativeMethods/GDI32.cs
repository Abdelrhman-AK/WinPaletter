using System;
using System.Runtime.InteropServices;

namespace WinPaletter.NativeMethods
{
    /// <summary>
    /// Provides P/Invoke declarations for GDI32 (Graphics Device Interface) functions.
    /// </summary>
    public class GDI32
    {
        /// <summary>
        /// Adds a font resource to the system. The font resource is specified by the contents of a block of data.
        /// </summary>
        /// <param name="pbFont">A pointer to the font resource data block.</param>
        /// <param name="cbFont">The size, in bytes, of the font resource data block.</param>
        /// <param name="pdv">Reserved. Must be IntPtr.Zero.</param>
        /// <param name="pcFonts">A pointer to the number of fonts installed. If the function succeeds, the value pointed to by pcFonts is incremented by the number of fonts added.</param>
        /// <returns>
        /// If the function succeeds, the return value is a handle to the font added.
        /// If the function fails, the return value is IntPtr.Zero.
        /// </returns>
        [DllImport("gdi32.dll")]
        public static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        /// <summary>
        /// Retrieves device-specific information about the capabilities of a specified device.
        /// </summary>
        /// <param name="hDC">A handle to the DC.</param>
        /// <param name="nIndex">The item to be returned.</param>
        /// <returns>The value of the specified capability.</returns>
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

        /// <summary>
        /// Enumerates device capabilities.
        /// </summary>
        public enum DeviceCap
        {
            /// <summary>
            /// Vertical size, in pixels, of the client area for the current device context.
            /// </summary>
            VERTRES = 10,

            /// <summary>
            /// Vertical size, in pixels, of the desktop.
            /// </summary>
            DESKTOPVERTRES = 117
        }
    }
}
