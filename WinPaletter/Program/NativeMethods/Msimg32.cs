using System;
using System.Runtime.InteropServices;

namespace WinPaletter.NativeMethods
{
    internal class Msimg32
    {
        private const string _msimg32 = "msimg32.dll";

        [StructLayout(LayoutKind.Sequential)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        public static class BlendOperations
        {
            public const byte AC_SRC_OVER = 0x00;
            public const byte AC_SRC_ALPHA = 0x01;
        }

        [DllImport(_msimg32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AlphaBlend(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest, NativeMethods.GDI32.SafeDeviceHandle hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, BLENDFUNCTION blendFunction);

        public static NativeMethods.GDI32.SafeGDIHandle CreateAlphaDIBSection(IntPtr hdc, int width, int height, out IntPtr ppvBits)
        {
            NativeMethods.GDI32.BITMAPINFO bmi = new()
            {
                biSize = Marshal.SizeOf(typeof(NativeMethods.GDI32.BITMAPINFO)),
                biWidth = width,
                biHeight = -height, // Top-down
                biPlanes = 1,
                biBitCount = 32,
                biCompression = 0 // BI_RGB
            };

            return NativeMethods.GDI32.CreateDIBSection_AsSafeGDIHandle(hdc, ref bmi, 0, out ppvBits, IntPtr.Zero, 0);
        }
    }
}
