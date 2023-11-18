using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace WinPaletter.NativeMethods
{
    public static class CursorExtensions
    {
        /// <summary>
        /// Converts a cursor into bitmap
        /// </summary>
        public static Bitmap ToBitmap(this Cursor cursor, System.Drawing.Size size)
        {
            User32.ICONINFO ii;
            IntPtr hCursor = cursor.Handle;

            User32.GetIconInfo(hCursor, out ii);

            Bitmap bmp = Bitmap.FromHbitmap(ii.hbmColor);
            GDI32.DeleteObject(ii.hbmColor);
            GDI32.DeleteObject(ii.hbmMask);

            BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);
            Bitmap dstBitmap = new(bmData.Width, bmData.Height, bmData.Stride, PixelFormat.Format32bppArgb, bmData.Scan0);
            bmp.UnlockBits(bmData);

            User32.DestroyIcon(hCursor);

            return new Bitmap(dstBitmap.Resize(size));
        }

        /// <summary>
        /// Converts a cursor file into bitmap, with desired frame
        /// </summary>
        public static Bitmap ToBitmap(string filePath, Size size, int index = 0)
        {
            IntPtr hCursor = User32.LoadImage(IntPtr.Zero, filePath, (int)User32.ImageType.IMAGE_CURSOR, 0, 0, (int)User32.LoadImageFlags.LR_LOADFROMFILE);

            using (Bitmap b = new(size.Width, size.Height))
            {
                using (Graphics g = Graphics.FromImage(b))
                {
                    IntPtr hdc = g.GetHdc();

                    User32.DrawIconEx(hdc, 0, 0, hCursor, size.Width, size.Height, index, IntPtr.Zero, (int)(User32.DrawIconExFlags.DI_NORMAL | User32.DrawIconExFlags.DI_IMAGE | User32.DrawIconExFlags.DI_MASK | User32.DrawIconExFlags.DI_NOMIRROR));

                    User32.DestroyIcon(hCursor);

                    g.ReleaseHdc(hdc);

                    b.MakeTransparent();

                    return new Bitmap(b);
                }
            }
        }

        public static int GetTotalFramesFromANIHeader(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // ANI file format: https://www.daubnet.com/en/file-format-ani
                // Skip to the position where the number of frames is stored
                fileStream.Seek(6, SeekOrigin.Begin);

                // Read the number of frames (little-endian format)
                int totalFrames = fileStream.ReadByte() | (fileStream.ReadByte() << 8);

                return totalFrames * 2;
            }
        }
    }
}