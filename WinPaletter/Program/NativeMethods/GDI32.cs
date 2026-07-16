using Microsoft.Win32.SafeHandles;
using System;
using System.Drawing;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using static WinPaletter.NativeMethods.UxTheme;

namespace WinPaletter.NativeMethods
{
    /// <summary>
    /// Provides P/Invoke declarations for GDI32 (Graphics Device Interface) functions.
    /// </summary>
    public class GDI32
    {
        private const string _gdi32 = "gdi32.dll";

        /// <summary>
        /// Represents a logical font used for text rendering.
        /// </summary>
        /// <remarks>
        /// Initializes a new instance of the <see cref="LOGFONT"/> class.
        /// </remarks>
        /// <param name="lfFaceName">The typeface name of the font.</param>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class LOGFONT(string lfFaceName = null)
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
        [DllImport(_gdi32)]
        public static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        /// <summary>
        /// Retrieves device-specific information about the capabilities of a specified device.
        /// </summary>
        /// <param name="hDC">A handle to the DC.</param>
        /// <param name="nIndex">The item to be returned.</param>
        /// <returns>The value of the specified capability.</returns>
        [DllImport(_gdi32, CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

        /// <summary>
        /// Deletes the specified device context (DC).
        /// </summary>
        /// <remarks>After a device context is deleted, the handle is no longer valid and should not be
        /// used in subsequent GDI operations. Attempting to delete a device context that was not created by a
        /// compatible creation function may result in undefined behavior.</remarks>
        /// <param name="hdc">A handle to the device context to be deleted. This handle must have been created by a previous call to a
        /// device context creation function such as CreateCompatibleDC_SDH or CreateDC.</param>
        /// <returns>true if the device context is successfully deleted; otherwise, false.</returns>
        [DllImport(_gdi32)]
        public static extern bool DeleteDC(IntPtr hdc);

        /// <summary>
        /// Selects an object into the specified device context. The new object replaces the previous object of the same
        /// type.
        /// </summary>
        /// <remarks>The caller is responsible for restoring the original object by selecting it back into
        /// the device context after use. Selecting a bitmap into a device context that is currently selected with a
        /// different bitmap can cause memory leaks. This function is not thread safe; ensure proper synchronization
        /// when accessing device contexts from multiple threads.</remarks>
        /// <param name="hdc">A handle to the device context into which the object is to be selected.</param>
        /// <param name="h">A handle to the graphics object to be selected. This can be a pen, brush, font, bitmap, or region.</param>
        /// <returns>If the function succeeds, the return value is a handle to the object being replaced. If the function fails,
        /// the return value is zero or HGDI_ERROR, depending on the object type.</returns>
        [DllImport(_gdi32)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr h);

        /// <summary>
        /// Selects an object into the specified device context. The new object replaces the previous object of the same
        /// type.
        /// </summary>
        /// <remarks>The caller is responsible for restoring the original object by selecting it back into
        /// the device context after use. Selecting a bitmap into a device context that is currently selected with a
        /// different bitmap can cause memory leaks. This function is not thread safe; ensure proper synchronization
        /// when accessing device contexts from multiple threads.</remarks>
        /// <param name="hdc">A handle to the device context into which the object is to be selected.</param>
        /// <param name="h">A handle to the graphics object to be selected. This can be a pen, brush, font, bitmap, or region.</param>
        /// <returns>If the function succeeds, the return value is a handle to the object being replaced. If the function fails,
        /// the return value is zero or HGDI_ERROR, depending on the object type.</returns>
        [DllImport(_gdi32)]
        public static extern IntPtr SelectObject(SafeDeviceHandle hdc, IntPtr h);

        /// <summary>
        /// Deletes a logical GDI object, releasing all system resources associated with it.
        /// </summary>
        /// <remarks>After a GDI object is deleted, its handle becomes invalid and must not be used in
        /// subsequent GDI calls. Attempting to delete an object that is still selected into a device context can cause
        /// undefined behavior.</remarks>
        /// <param name="ho">A handle to the GDI object to be deleted. This handle must have been created by a GDI function and must not
        /// be used after deletion.</param>
        /// <returns>true if the object was deleted successfully; otherwise, false.</returns>
        [DllImport(_gdi32)]
        public static extern bool DeleteObject(IntPtr ho);

        /// <summary>
        /// Creates a logical brush with a solid color for use in GDI drawing operations.
        /// </summary>
        /// <remarks>The caller is responsible for releasing the brush by calling DeleteObject when it is
        /// no longer needed to avoid resource leaks.</remarks>
        /// <param name="crColor">The color value of the brush, specified as a COLORREF value. The low-order byte contains the red component,
        /// the next byte contains the green component, and the third byte contains the blue component.</param>
        /// <returns>A handle to the created logical brush. Returns IntPtr.Zero if the function fails.</returns>
        [DllImport(_gdi32, SetLastError = true)]
        public static extern IntPtr CreateSolidBrush(int crColor);

        /// <summary>
        /// Sets the background color for the specified device context.
        /// </summary>
        /// <remarks>The background color is used with text, hatched brushes, and other drawing operations
        /// that use the device context. If the function fails, the return value is CLR_INVALID. Use
        /// Marshal.GetLastWin32Error to obtain error information.</remarks>
        /// <param name="hdc">A handle to the device context whose background color is to be set.</param>
        /// <param name="crColor">The new background color value. This parameter must be a COLORREF value.</param>
        /// <returns>The previous background color as a COLORREF value if successful; otherwise, CLR_INVALID.</returns>
        [DllImport(_gdi32, SetLastError = true)]
        public static extern int SetBkColor(IntPtr hdc, int crColor);

        /// <summary>
        /// Sets the text color for the specified device context.
        /// </summary>
        /// <remarks>If the function fails, the return value is –1. To get extended error information,
        /// call GetLastError. This function affects text drawn after the call; previously drawn text is not
        /// affected.</remarks>
        /// <param name="hdc">A handle to the device context whose text color is to be set.</param>
        /// <param name="crColor">The new text color, specified as a COLORREF value. The color value must be in 0x00bbggrr format.</param>
        /// <returns>The previous text color as a COLORREF value if successful; otherwise, –1 to indicate failure.</returns>
        [DllImport(_gdi32, SetLastError = true)]
        public static extern int SetTextColor(IntPtr hdc, int crColor);

        /// <summary>
        /// Sets the text color for the specified device context.
        /// </summary>
        /// <remarks>If the function fails, the return value is –1. To get extended error information,
        /// call GetLastError. This function affects text drawn after the call; previously drawn text is not
        /// affected.</remarks>
        /// <param name="hdc">A handle to the device context whose text color is to be set.</param>
        /// <param name="crColor">The new text color, specified as a COLORREF value. The color value must be in 0x00bbggrr format.</param>
        /// <returns>The previous text color as a COLORREF value if successful; otherwise, –1 to indicate failure.</returns>
        [DllImport(_gdi32, SetLastError = true)]
        public static extern int SetTextColor(IntPtr hdc, uint crColor);

        /// <summary>
        /// Sets the background mix mode of the specified device context. The background mix mode determines how the background color is combined with the foreground color when drawing text or graphics.
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        [DllImport(_gdi32)]
        public static extern int SetBkMode(IntPtr hdc, int mode);

        /// <summary>
        /// Performs a bit-block transfer of color data from a source device context to a destination device context.
        /// </summary>
        /// <remarks>This method is a P/Invoke signature for the native BitBlt function in gdi32.dll. The
        /// device contexts must be compatible, and the caller is responsible for managing device context handles and
        /// ensuring proper cleanup. Some raster operations may require the source and destination device contexts to
        /// have the same color format. This method is not thread-safe.</remarks>
        /// <param name="hdcDest">A handle to the destination device context where the image will be drawn.</param>
        /// <param name="x">The x-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
        /// <param name="y">The y-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
        /// <param name="cx">The width, in logical units, of the rectangle to be transferred.</param>
        /// <param name="cy">The height, in logical units, of the rectangle to be transferred.</param>
        /// <param name="hdcSrc">A handle to the source device context from which the image will be copied.</param>
        /// <param name="x1">The x-coordinate, in logical units, of the upper-left corner of the source rectangle.</param>
        /// <param name="y1">The y-coordinate, in logical units, of the upper-left corner of the source rectangle.</param>
        /// <param name="rop">A raster-operation code that defines how the color data is combined between the source and destination.</param>
        /// <returns>true if the operation succeeds; otherwise, false.</returns>
        [DllImport(_gdi32)]
        public static extern bool BitBlt(IntPtr hdcDest, int x, int y, int cx, int cy, IntPtr hdcSrc, int x1, int y1, uint rop);

        /// <summary>
        /// Creates a logical font with the specified characteristics and returns a handle to the font. 
        /// The font can then be selected into a device context for text rendering.
        /// </summary>
        /// <param name="lplf">A LOGFONT object that defines the font characteristics.</param>
        /// <returns>A handle to the font if successful; otherwise, IntPtr.Zero.</returns>
        [DllImport(_gdi32, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr CreateFontIndirect([In] LOGFONT lplf);

        [DllImport(_gdi32)]
        public static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        /// <summary>
        /// Gets information about the specified graphics object. The function fills a buffer with information about the object, such as its type and attributes.
        /// </summary>
        /// <param name="hgdiobj"></param>
        /// <param name="cbBuffer"></param>
        /// <param name="lpvObject"></param>
        /// <returns></returns>
        [DllImport(_gdi32)]
        public static extern bool GetObject(IntPtr hgdiobj, int cbBuffer, ref LOGBRUSH lpvObject);

        /// <summary>
        /// Creates a memory device context (DC) compatible with the specified device. The memory DC can be used for off-screen drawing and bitmap manipulation.
        /// </summary>
        /// <param name="hdc"></param>
        /// <returns></returns>
        [DllImport(_gdi32, EntryPoint ="CreateCompatibleDC",  SetLastError = true)]
        public static extern SafeDeviceHandle CreateCompatibleDC_SDH(IntPtr hdc);

        /// <summary>
        /// Creates a memory device context (DC) compatible with the specified device. The memory DC can be used for off-screen drawing and bitmap manipulation.
        /// </summary>
        /// <param name="hdc"></param>
        /// <returns></returns>
        [DllImport(_gdi32, EntryPoint = "CreateCompatibleDC", SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        /// <summary>
        /// Creates a bitmap compatible with the device that is associated with the specified device context. The bitmap can be selected into a memory device context for off-screen drawing.
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <returns></returns>
        [DllImport(_gdi32, SetLastError = true)]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int cx, int cy);

        /// <summary>
        /// Selects an object into the specified device context. The new object replaces the previous object of the same type.
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="hObject"></param>
        /// <returns></returns>
        [DllImport(_gdi32)]
        public static extern IntPtr SelectObject(SafeDeviceHandle hdc, SafeHandle hObject);

        /// <summary>
        /// Creates a device-independent bitmap (DIB) that applications can write to directly and selects it into a
        /// device context.
        /// </summary>
        /// <remarks>The DIB created by this function can be selected into a device context for drawing
        /// operations. The caller is responsible for releasing the bitmap handle by calling DeleteObject when it is no
        /// longer needed. This function enables direct access to the bitmap's pixel data, which can improve performance
        /// for image manipulation tasks.</remarks>
        /// <param name="hdc">A handle to a device context. If this parameter is not NULL, the function uses the device context's color
        /// format to initialize the DIB.</param>
        /// <param name="pbmi">A reference to a BITMAPINFO structure that specifies the dimensions and color format of the DIB.</param>
        /// <param name="usage">The type of data contained in the color table. This parameter must be either DIB_RGB_COLORS or
        /// DIB_PAL_COLORS.</param>
        /// <param name="ppvBits">When the function returns, contains a pointer to the location of the DIB's bit values. This allows direct
        /// access to the bitmap's pixel data.</param>
        /// <param name="hSection">A handle to a file mapping object that the function will use to create the DIB. This parameter can be
        /// IntPtr.Zero if no file mapping is used.</param>
        /// <param name="offset">The offset, in bytes, from the beginning of the file mapping object referenced by hSection. This value is
        /// ignored if hSection is IntPtr.Zero.</param>
        /// <returns>A handle to the created device-independent bitmap (DIB) if successful; otherwise, IntPtr.Zero.</returns>
        [DllImport(_gdi32, SetLastError = true)]
        public static extern IntPtr CreateDIBSection(IntPtr hdc, ref BITMAPINFO pbmi, uint iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

        /// <summary>
        /// Creates a device-independent bitmap (DIB) that applications can write to directly and selects it into a
        /// device context.
        /// </summary>
        /// <remarks>The DIB created by this function can be selected into a device context for drawing
        /// operations. The caller is responsible for releasing the bitmap handle by calling DeleteObject when it is no
        /// longer needed. This function enables direct access to the bitmap's pixel data, which can improve performance
        /// for image manipulation tasks.</remarks>
        /// <param name="hdc">A handle to a device context. If this parameter is not NULL, the function uses the device context's color
        /// format to initialize the DIB.</param>
        /// <param name="pbmi">A reference to a BITMAPINFO structure that specifies the dimensions and color format of the DIB.</param>
        /// <param name="usage">The type of data contained in the color table. This parameter must be either DIB_RGB_COLORS or
        /// DIB_PAL_COLORS.</param>
        /// <param name="ppvBits">When the function returns, contains a pointer to the location of the DIB's bit values. This allows direct
        /// access to the bitmap's pixel data.</param>
        /// <param name="hSection">A handle to a file mapping object that the function will use to create the DIB. This parameter can be
        /// IntPtr.Zero if no file mapping is used.</param>
        /// <param name="offset">The offset, in bytes, from the beginning of the file mapping object referenced by hSection. This value is
        /// ignored if hSection is IntPtr.Zero.</param>
        /// <returns>A handle to the created device-independent bitmap (DIB) if successful; otherwise, IntPtr.Zero.</returns>
        [DllImport(_gdi32, EntryPoint = "CreateDIBSection", SetLastError = true)]
        public static extern SafeGDIHandle CreateDIBSection_AsSafeGDIHandle(IntPtr hdc, ref BITMAPINFO pbmi, uint iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

        /// <summary>
        /// Bit-block transfers the color data corresponding to a rectangle of pixels from the specified source device context into a destination device context.
        /// </summary>
        /// <param name="hdcDest"></param>
        /// <param name="xDest"></param>
        /// <param name="yDest"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="hdcSrc"></param>
        /// <param name="xSrc"></param>
        /// <param name="ySrc"></param>
        /// <param name="rop"></param>
        /// <returns></returns>
        [DllImport(_gdi32)]
        public static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int width, int height, SafeDeviceHandle hdcSrc, int xSrc, int ySrc, int rop);

        /// <summary>
        /// Creates a device-independent bitmap (DIB) section and selects it into a memory device context. This allows for direct manipulation of the bitmap's pixel data while using GDI drawing functions.
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="primaryHdc"></param>
        /// <param name="memoryHdc"></param>
        /// <returns></returns>
        public static SafeGDIHandle CreateDib(Rectangle bounds, IntPtr primaryHdc, SafeDeviceHandle memoryHdc)
        {
            BITMAPINFO info = new();
            info.biSize = Marshal.SizeOf(typeof(BITMAPINFO));
            info.biWidth = bounds.Width;
            info.biHeight = -bounds.Height;
            info.biPlanes = 1;
            info.biBitCount = 32;
            info.biCompression = 0;

            IntPtr bitsPointer;
            IntPtr dib = CreateDIBSection(primaryHdc, ref info, 0, out bitsPointer, IntPtr.Zero, 0);
            SafeGDIHandle result = new SafeGDIHandle(dib, true);
            SelectObject(memoryHdc, result);
            return result;
        }

        /// <summary>
        /// Retrieves a handle to one of the stock pens, brushes, fonts, or palettes.
        /// </summary>
        /// <param name="fnObject">The type of stock object.</param>
        /// <returns>If the function succeeds, the return value is a handle to the logical object. If the function fails, the return value is IntPtr.Zero.</returns>
        [DllImport(_gdi32, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr GetStockObject(StockObjects fnObject);

        /// <summary>
        /// Draws a rectangle in the specified device context using the current pen and brush. The rectangle is defined by the coordinates of its upper-left and lower-right corners.
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="nLeftRect"></param>
        /// <param name="nTopRect"></param>
        /// <param name="nRightRect"></param>
        /// <param name="nBottomRect"></param>
        /// <returns></returns>
        [DllImport(_gdi32)]
        public static extern bool Rectangle(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        /// <summary>
        /// Creates a logical pen with the specified style, width, and color. The pen can then be selected into a device context for drawing lines and shapes.
        /// </summary>
        /// <param name="fnPenStyle"></param>
        /// <param name="nWidth"></param>
        /// <param name="crColor"></param>
        /// <returns></returns>
        [DllImport(_gdi32)]
        public static extern IntPtr CreatePen(int fnPenStyle, int nWidth, int crColor);

        [DllImport(_gdi32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int AddFontResource(string lpszFilename);

        [DllImport(_gdi32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RemoveFontResource(string lpFileName);

        /// <summary>
        /// Predefined stock objects for GetStockObject.
        /// </summary>
        public enum StockObjects : int
        {
            WHITE_BRUSH = 0,
            LTGRAY_BRUSH = 1,
            GRAY_BRUSH = 2,
            DKGRAY_BRUSH = 3,
            BLACK_BRUSH = 4,
            NULL_BRUSH = 5,
            HOLLOW_BRUSH = 5, // Equivalent to NULL_BRUSH
            WHITE_PEN = 6,
            BLACK_PEN = 7,
            NULL_PEN = 8,
            OEM_FIXED_FONT = 10,
            ANSI_FIXED_FONT = 11,
            ANSI_VAR_FONT = 12,
            SYSTEM_FONT = 13,
            DEVICE_DEFAULT_FONT = 14,
            DEFAULT_PALETTE = 15,
            SYSTEM_FIXED_FONT = 16,
            DEFAULT_GUI_FONT = 17, // Often used for standard UI font metrics
            DC_BRUSH = 18,
            DC_PEN = 19
        }

        /// <summary>
        /// Defines the structure that contains information about a DIB (device-independent bitmap), including
        /// dimensions, color format, and other properties required for bitmap operations.
        /// </summary>
        /// <remarks>The BITMAPINFO structure is commonly used with Windows API functions that create or
        /// manipulate device-independent bitmaps. The bmiHeader field specifies the core bitmap information, while the
        /// color table is only present for certain bit depths. For 32-bpp bitmaps using BI_RGB compression, a color
        /// table is not required.</remarks>
        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFO
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;
            public int colors;
        }

        public sealed class SafeDeviceHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            private SafeDeviceHandle() : base(true) { }

            public SafeDeviceHandle(IntPtr handle, bool ownsHandle) : base(ownsHandle)
            {
                SetHandle(handle);
            }

            protected override bool ReleaseHandle()
            {
                return DeleteDC(handle);
            }
        }

        public sealed class SafeGDIHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            private SafeGDIHandle() : base(true) { }

            public SafeGDIHandle(IntPtr handle, bool ownsHandle) : base(ownsHandle)
            {
                SetHandle(handle);
            }

            protected override bool ReleaseHandle()
            {
                return DeleteObject(handle);
            }
        }

        /// <summary>
        /// Pen style constant for solid lines. This constant is used with GDI functions to specify that a pen should draw solid lines without any pattern or dashes.
        /// </summary>
        public const int PS_SOLID = 0;

        /// <summary>
        /// Background mode flag for opaque background drawing
        /// </summary>
        public const int OPAQUE = 2;

        /// <summary>
        /// Null brush constant. This constant is used with GDI functions to specify that a brush should not fill any area, effectively making it transparent. It is often used when drawing shapes or text where no background fill is desired.
        /// </summary>
        public const int NULL_BRUSH = 5;

        /// <summary>
        /// Background mode flag for transparent background drawing
        /// </summary>
        public const int TRANSPARENT = 1;

        /// <summary>
        /// Draw text flags: Top alignment (same as DT_LEFT)
        /// </summary>
        public const uint DT_TOP = 0x00000000;

        /// <summary>
        /// Draw text flags: Center alignment horizontally
        /// </summary>
        public const uint DT_CENTER = 0x00000001;

        /// <summary>
        /// Draw text flags: Right alignment horizontally
        /// </summary>
        public const uint DT_RIGHT = 0x00000002;

        /// <summary>
        /// Draw text flags: Bottom alignment vertically
        /// </summary>
        public const uint DT_BOTTOM = 0x00000008;

        /// <summary>
        /// Draw text flags: Expand tab characters
        /// </summary>
        public const uint DT_EXPANDTABS = 0x00000040;

        /// <summary>
        /// Draw text flags: Use tab stops specified in lParam
        /// </summary>
        public const uint DT_TABSTOP = 0x00000080;

        /// <summary>
        /// Draw text flags: No clipping of text outside the rectangle
        /// </summary>
        public const uint DT_NOCLIP = 0x00000100;

        /// <summary>
        /// Draw text flags: Include external leading in line height
        /// </summary>
        public const uint DT_EXTERNALLEADING = 0x00000200;

        /// <summary>
        /// Draw text flags: Use internal GDI text formatting
        /// </summary>
        public const uint DT_INTERNAL = 0x00001000;

        /// <summary>
        /// Draw text flags: Edit control style formatting
        /// </summary>
        public const uint DT_EDITCONTROL = 0x00002000;

        /// <summary>
        /// Draw text flags: Add ellipsis at the end of truncated text
        /// </summary>
        public const uint DT_END_ELLIPSIS = 0x00008000;

        /// <summary>
        /// Draw text flags: Allow modification of the string
        /// </summary>
        public const uint DT_MODIFYSTRING = 0x00010000;

        /// <summary>
        /// Draw text flags: Right-to-left reading order
        /// </summary>
        public const uint DT_RTLREADING = 0x00020000;

        /// <summary>
        /// Draw text flags: Add ellipsis at the end of word-truncated text
        /// </summary>
        public const uint DT_WORD_ELLIPSIS = 0x00040000;

        /// <summary>
        /// Draw text flags: No full-width character break
        /// </summary>
        public const uint DT_NOFULLWIDTHCHARBREAK = 0x00080000;

        /// <summary>
        /// Draw text flags: Hide prefix characters
        /// </summary>
        public const uint DT_HIDEPREFIX = 0x00100000;

        /// <summary>
        /// Draw text flags: Show only prefix characters
        /// </summary>
        public const uint DT_PREFIXONLY = 0x00200000;

        /// <summary>
        /// Brush style: Solid brush
        /// </summary>
        public const uint BS_SOLID = 0;

        /// <summary>
        /// Device Independent (DI) flags: Normal rendering
        /// </summary>
        public const uint DI_NORMAL = 0x0003;

        /// <summary>
        /// Device Independent (DI) flags: Compatible rendering
        /// </summary>
        public const uint DI_COMPAT = 0x0004;

        /// <summary>
        /// Draw text flags: Left alignment
        /// </summary>
        public const uint DT_LEFT = 0x00000000;

        /// <summary>
        /// Draw text flags: Vertically center
        /// </summary>
        public const uint DT_VCENTER = 0x00000004;

        /// <summary>
        /// Draw text flags: Word wrap/break
        /// </summary>
        public const uint DT_WORDBREAK = 0x00000010;

        /// <summary>
        /// Draw text flags: Single line text
        /// </summary>
        public const uint DT_SINGLELINE = 0x00000020;

        /// <summary>
        /// Draw text flags: Calculate rectangle size
        /// </summary>
        public const uint DT_CALCRECT = 0x00000400;

        /// <summary>
        /// Draw text flags: No prefix characters
        /// </summary>
        public const uint DT_NOPREFIX = 0x00000800;

        /// <summary>
        /// Draw text flags: Path ellipsis
        /// </summary>
        public const uint DT_PATH_ELLIPSIS = 0x00004000;


        /// <summary>
        /// Structure that defines the attributes of a logical brush, including its style, color, and hatch pattern. This structure is used with GDI functions to create and manipulate brushes for drawing operations.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct LOGBRUSH
        {
            public uint lbStyle;
            public int lbColor;
            public IntPtr lbHatch;
        }

        /// <summary>
        /// Defines the core header information for a device-independent bitmap (DIB).
        /// </summary>
        /// <remarks>The BITMAPINFOHEADER structure specifies dimensions, color format, and compression
        /// details for a bitmap image. It is commonly used when working with Windows GDI functions that require bitmap
        /// metadata, such as creating or reading bitmap files. The values in this structure must be set according to
        /// the bitmap's characteristics to ensure correct interpretation by consuming APIs. A negative value for the
        /// biHeight field indicates a top-down DIB, where the first scan line is the top row of the image.</remarks>
        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFOHEADER
        {
            /// <summary>
            /// Specifies the size, in bytes, of the structure.
            /// </summary>
            public uint biSize;

            /// <summary>
            /// Gets or sets the width of the bitmap, in pixels.
            /// </summary>
            public int biWidth;

            /// <summary>
            /// Specifies the height of the bitmap, in pixels.
            /// </summary>
            /// <remarks>If the value is negative, the bitmap is a top-down DIB with the origin at the
            /// upper-left corner. If the value is positive, the bitmap is a bottom-up DIB with the origin at the
            /// lower-left corner.</remarks>
            public int biHeight;

            /// <summary>
            /// Gets or sets the number of color planes for the bitmap image. This value is typically set to 1.
            /// </summary>
            /// <remarks>This property is primarily used for compatibility with certain bitmap file
            /// formats. For most modern bitmap images, the value should be 1.</remarks>
            public short biPlanes;

            /// <summary>
            /// Gets or sets the number of bits per pixel used to represent the color of a single pixel.
            /// </summary>
            /// <remarks>This value determines the color depth of the image. Common values include 1, 4, 8, 16, 24, and 32, corresponding to monochrome, 16-color, 256-color, high color, true color, and deep
            /// color formats, respectively.</remarks>
            public short biBitCount;

            /// <summary>
            /// Specifies the compression method used for the bitmap data.
            /// </summary>
            /// <remarks>The value corresponds to a predefined constant indicating the compression
            /// type, such as BI_RGB for no compression or BI_RLE8 for run-length encoding. Refer to the documentation
            /// for valid values and their meanings.</remarks>
            public uint biCompression;

            /// <summary>
            /// Gets or sets the size of the image, in bytes.
            /// </summary>
            /// <remarks>This value typically represents the size of the pixel data in the image,
            /// excluding any file headers or metadata. For uncompressed images, it may be set to zero if the size can
            /// be calculated from other fields.</remarks>
            public uint biSizeImage;

            /// <summary>
            /// Specifies the horizontal resolution of the image, in pixels per meter.
            /// </summary>
            /// <remarks>This value is typically used in bitmap file headers to indicate the intended
            /// display resolution. It may not affect image rendering on all platforms.</remarks>
            public int biXPelsPerMeter;

            /// <summary>
            /// Specifies the vertical resolution, in pixels per meter, of the image.
            /// </summary>
            /// <remarks>This value is typically used to indicate the intended display resolution for
            /// the image. A value of zero indicates that the resolution is unspecified.</remarks>
            public int biYPelsPerMeter;

            /// <summary>
            /// Specifies the number of color indexes in the color table that are actually used by the bitmap.
            /// </summary>
            /// <remarks>If this value is zero, all colors are required. This field is primarily used
            /// for optimizing the size of the color table in certain bitmap formats.</remarks>
            public uint biClrUsed;

            /// <summary>
            /// Specifies the number of color indexes that are considered important for displaying the bitmap.
            /// Applications can use this value to optimize color usage when rendering the image.
            /// </summary>
            /// <remarks>If this value is zero, all colors are considered important. This field is primarily used for certain bitmap formats and may be ignored by some applications.</remarks>
            public uint biClrImportant;
        }

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
