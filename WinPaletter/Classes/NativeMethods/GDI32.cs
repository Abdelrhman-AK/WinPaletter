using System;
using System.Runtime.InteropServices;

namespace WinPaletter.NativeMethods
{
    public class GDI32
    {
        [DllImport("gdi32.dll")]
        public static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, BitBltOp dwRop);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDIBSection(IntPtr hdc, ref BitmapInfo pbmi, uint iUsage, int ppvBits, IntPtr hSection, uint dwOffset);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern bool DeleteObject(IntPtr hObject);

        public enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BitmapInfo
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
            public byte bmiColors_rgbBlue;
            public byte bmiColors_rgbGreen;
            public byte bmiColors_rgbRed;
            public byte bmiColors_rgbReserved;
        }

        public enum BitBltOp : uint
        {
            SRCCOPY = 0xCC0020U,   // dest = source                   
            SRCPAINT = 0xEE0086U,   // dest = source OR dest           
            SRCAND = 0x8800C6U,   // dest = source AND dest          
            SRCINVERT = 0x660046U,   // dest = source XOR dest          
            SRCERASE = 0x440328U,   // dest = source AND (NOT dest )   
            NOTSRCCOPY = 0x330008U,   // dest = (NOT source)             
            NOTSRCERASE = 0x1100A6U,   // dest = (NOT src) AND (NOT dest) 
            MERGECOPY = 0xC000CAU,   // dest = (source AND pattern)     
            MERGEPAINT = 0xBB0226U,   // dest = (NOT source) OR dest     
            PATCOPY = 0xF00021U,   // dest = pattern                  
            PATPAINT = 0xFB0A09U,   // dest = DPSnoo                   
            PATINVERT = 0x5A0049U,   // dest = pattern XOR dest         
            DSTINVERT = 0x550009U,   // dest = (NOT dest)               
            BLACKNESS = 0x42U,   // dest = BLACK                    
            WHITENESS = 0xFF0062U,   // dest = WHITE                    

            NOMIRRORBITMAP = uint.MinValue + 0x00000000, // Do not Mirror the bitmap in this call 
            CAPTUREBLT = 0x40000000U      // Include layered windows 
        }
    }
}
