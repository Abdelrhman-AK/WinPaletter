﻿using System;
using System.Runtime.InteropServices;

namespace WinPaletter.NativeMethods
{
    /// <summary>
    /// Provides P/Invoke declarations for GDI32 (Graphics Device Interface) functions.
    /// </summary>
    public class GDI32
    {
        /// <summary>
        /// Represents a logical font used for text rendering.
        /// </summary>
        /// <remarks>
        /// Initializes a new instance of the <see cref="LogFont"/> class.
        /// </remarks>
        /// <param name="lfFaceName">The typeface name of the font.</param>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class LogFont(string lfFaceName = null)
        {
            /// <summary>
            /// Specifies the height, in logical units, of the font's character cell or character.
            /// </summary>
            public int lfHeight = 0;

            /// <summary>
            /// Specifies the average width, in logical units, of characters in the font.
            /// </summary>
            public int lfWidth = 0;

            /// <summary>
            /// Specifies the angle, in tenths of degrees, between the escapement vector and the x-axis of the device.
            /// </summary>
            public int lfEscapement = 0;

            /// <summary>
            /// Specifies the angle, in tenths of degrees, between each character's base line and the x-axis of the device.
            /// </summary>
            public int lfOrientation = 0;

            /// <summary>
            /// Specifies the weight of the font.
            /// </summary>
            public int lfWeight = 0;

            /// <summary>
            /// Specifies if the font is italicized (1 for true, 0 for false).
            /// </summary>
            public byte lfItalic = 0;

            /// <summary>
            /// Specifies if the font is underlined (1 for true, 0 for false).
            /// </summary>
            public byte lfUnderline = 0;

            /// <summary>
            /// Specifies if the font has a line through it (1 for true, 0 for false).
            /// </summary>
            public byte lfStrikeOut = 0;

            /// <summary>
            /// Specifies the character set of the font.
            /// </summary>
            public byte lfCharSet = 0;

            /// <summary>
            /// Specifies the output precision.
            /// </summary>
            public byte lfOutPrecision = 0;

            /// <summary>
            /// Specifies the clipping precision.
            /// </summary>
            public byte lfClipPrecision = 0;

            /// <summary>
            /// Specifies the output quality.
            /// </summary>
            public byte lfQuality = 0;

            /// <summary>
            /// Specifies the pitch and family of the font.
            /// </summary>
            public byte lfPitchAndFamily = 0;

            /// <summary>
            /// Specifies the typeface name of the font.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string lfFaceName = lfFaceName;
        }

        /// <summary>
        /// Represents font precision values.
        /// </summary>
        public enum FontPrecision : byte
        {
            /// <summary>
            /// Default font precision.
            /// </summary>
            OUT_DEFAULT_PRECIS = 0,

            /// <summary>
            /// String font precision.
            /// </summary>
            OUT_STRING_PRECIS = 1,

            /// <summary>
            /// Character font precision.
            /// </summary>
            OUT_CHARACTER_PRECIS = 2,

            /// <summary>
            /// Stroke font precision.
            /// </summary>
            OUT_STROKE_PRECIS = 3,

            /// <summary>
            /// TrueType font precision.
            /// </summary>
            OUT_TT_PRECIS = 4,

            /// <summary>
            /// Device font precision.
            /// </summary>
            OUT_DEVICE_PRECIS = 5,

            /// <summary>
            /// Raster font precision.
            /// </summary>
            OUT_RASTER_PRECIS = 6,

            /// <summary>
            /// TrueType only font precision.
            /// </summary>
            OUT_TT_ONLY_PRECIS = 7,

            /// <summary>
            /// Outline font precision.
            /// </summary>
            OUT_OUTLINE_PRECIS = 8,

            /// <summary>
            /// Screen outline font precision.
            /// </summary>
            OUT_SCREEN_OUTLINE_PRECIS = 9,

            /// <summary>
            /// PostScript only font precision.
            /// </summary>
            OUT_PS_ONLY_PRECIS = 10
        }

        /// <summary>
        /// Represents font clip precision values.
        /// </summary>
        public enum FontClipPrecision : byte
        {
            /// <summary>
            /// Default clip precision.
            /// </summary>
            CLIP_DEFAULT_PRECIS = 0,

            /// <summary>
            /// Character clip precision.
            /// </summary>
            CLIP_CHARACTER_PRECIS = 1,

            /// <summary>
            /// Stroke clip precision.
            /// </summary>
            CLIP_STROKE_PRECIS = 2,

            /// <summary>
            /// Mask for clip precision.
            /// </summary>
            CLIP_MASK = 0xF,

            /// <summary>
            /// Left-handed angles clip precision.
            /// </summary>
            CLIP_LH_ANGLES = 1 << 4,

            /// <summary>
            /// TrueType always clip precision.
            /// </summary>
            CLIP_TT_ALWAYS = 2 << 4,

            /// <summary>
            /// Disable font association clip precision.
            /// </summary>
            CLIP_DFA_DISABLE = 4 << 4,

            /// <summary>
            /// Embedded font clip precision.
            /// </summary>
            CLIP_EMBEDDED = 8 << 4
        }

        /// <summary>
        /// Represents font quality values.
        /// </summary>
        public enum FontQuality : byte
        {
            /// <summary>
            /// Default font quality.
            /// </summary>
            DEFAULT_QUALITY = 0,

            /// <summary>
            /// Draft font quality.
            /// </summary>
            DRAFT_QUALITY = 1,

            /// <summary>
            /// Proof font quality.
            /// </summary>
            PROOF_QUALITY = 2,

            /// <summary>
            /// Non-antialiased font quality.
            /// </summary>
            NONANTIALIASED_QUALITY = 3,

            /// <summary>
            /// Antialiased font quality.
            /// </summary>
            ANTIALIASED_QUALITY = 4,

            /// <summary>
            /// ClearType font quality.
            /// </summary>
            CLEARTYPE_QUALITY = 5,

            /// <summary>
            /// ClearType natural font quality.
            /// </summary>
            CLEARTYPE_NATURAL_QUALITY = 6
        }

        /// <summary>
        /// Represents font pitch and family values.
        /// </summary>
        public enum FontPitchAndFamily : byte
        {
            /// <summary>
            /// Default pitch.
            /// </summary>
            DEFAULT_PITCH = 0,

            /// <summary>
            /// Fixed pitch.
            /// </summary>
            FIXED_PITCH = 1,

            /// <summary>
            /// Variable pitch.
            /// </summary>
            VARIABLE_PITCH = 2,

            /// <summary>
            /// Don't care font family.
            /// </summary>
            FF_DONTCARE = 0 << 4,

            /// <summary>
            /// Roman font family.
            /// </summary>
            FF_ROMAN = 1 << 4,

            /// <summary>
            /// Swiss font family.
            /// </summary>
            FF_SWISS = 2 << 4,

            /// <summary>
            /// Modern font family.
            /// </summary>
            FF_MODERN = 3 << 4,

            /// <summary>
            /// Script font family.
            /// </summary>
            FF_SCRIPT = 4 << 4,

            /// <summary>
            /// Decorative font family.
            /// </summary>
            FF_DECORATIVE = 5 << 4
        }

        /// <summary>
        /// Create a compatible device context (DC) for the specified device context (DC).
        /// </summary>
        /// <param name="hdc"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        /// <summary>
        /// Creates a bitmap compatible with the specified device context (DC).
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int width, int height);

        /// <summary>
        /// Selects an object into the specified device context (DC). The object can be a bitmap, pen, brush, font, region, or palette.
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool SelectObject(IntPtr hdc, IntPtr obj);

        /// <summary>
        /// Releases the specified device context (DC) and any associated resources.
        /// </summary>
        /// <param name="hObject"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool DeleteObject(IntPtr hObject);

        /// <summary>
        /// Copies a bitmap from one device context (DC) to another.
        /// </summary>
        /// <param name="hdcDest"></param>
        /// <param name="nXDest"></param>
        /// <param name="nYDest"></param>
        /// <param name="nWidth"></param>
        /// <param name="nHeight"></param>
        /// <param name="hdcSource"></param>
        /// <param name="nXSrc"></param>
        /// <param name="nYSrc"></param>
        /// <param name="dwRop"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSource, int nXSrc, int nYSrc, uint dwRop);

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
