// Author:          Evan Olds
// Migration Date:  December 27, 2011
// Original creation date unknown

using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace EOFC
{
    internal static class BitmapOps
    {
        public static void AddToAlpha(Bitmap bm, int amount)
        {
            Rectangle r = new(0, 0, bm.Width, bm.Height);
            BitmapData bd = bm.LockBits(r, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* pxls = (byte*)bd.Scan0.ToPointer();
                byte* imgend = pxls + (bm.Width * bm.Height * 4);
                while (pxls < imgend)
                {
                    int a = pxls[3];
                    a += amount;
                    if (a < 0) { a = 0; }
                    else if (a > 255) { a = 255; }
                    pxls[3] = (byte)a;
                    pxls += 4;
                }
            }
            bm.UnlockBits(bd);
        }

        private static uint ComputeSize(uint w, uint h, uint bpp, bool PaddedTo32)
        {
            // Compute the row size
            uint RowSize = w * bpp / 8;
            if (PaddedTo32 && RowSize % 4 != 0)
            {
                RowSize += (4 - (RowSize % 4));
            }
            return h * RowSize;
        }

        public static bool Get32BitPixelData(Bitmap bm, uint[] output)
        {
            Rectangle r = new(0, 0, bm.Width, bm.Height);
            int pxlCount = bm.Width * bm.Height;

            // The simple case is if it's already 32-bit
            if (PixelFormat.Format32bppArgb == bm.PixelFormat ||
                PixelFormat.Format32bppPArgb == bm.PixelFormat ||
                PixelFormat.Format32bppRgb == bm.PixelFormat)
            {
                BitmapData bd = bm.LockBits(r, ImageLockMode.ReadOnly, bm.PixelFormat);
                unsafe
                {
                    fixed (uint* dstPtr = output)
                    {
                        uint* dst = dstPtr;
                        uint* src = (uint*)bd.Scan0.ToPointer();
                        while (pxlCount-- > 0)
                        {
                            *dst++ = *src++;
                        }
                    }
                }
                bm.UnlockBits(bd);
                return true;
            }
            else if (PixelFormat.Format8bppIndexed == bm.PixelFormat)
            {
                System.Drawing.Imaging.ColorPalette cp = bm.Palette;
                BitmapData bd = bm.LockBits(r, ImageLockMode.ReadOnly, bm.PixelFormat);
                unsafe
                {
                    fixed (uint* pxls = output)
                    {
                        for (int y = 0; y < r.Height; y++)
                        {
                            byte* src8 = (byte*)bd.Scan0.ToPointer();
                            src8 += (bd.Stride * y);
                            uint* dst = &pxls[y * r.Width];

                            for (int x = 0; x < r.Width; x++)
                            {
                                *dst++ = (uint)cp.Entries[*src8++].ToArgb();
                            }
                        }
                    }
                }
                bm.UnlockBits(bd);
                return true;
            }

            return false;
        }

        public static unsafe int CountTransparentColumnsFromLeft(Bitmap bm)
        {
            int x = 0;
            int w = bm.Width;
            int h = bm.Height;
            Rectangle rect = new(0, 0, bm.Width, bm.Height);
            BitmapData bd = bm.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            uint* endimg = &((uint*)bd.Scan0.ToPointer())[w * h];
            for (x = 0; x < w; x++) // Loop through columns
            {
                uint* Pxls = &((uint*)bd.Scan0.ToPointer())[x];
                while (Pxls < endimg)
                {
                    if ((*Pxls & 0xFF000000) != 0)
                    {
                        // This column is not completely transparent
                        bm.UnlockBits(bd);
                        return x;
                    }
                    Pxls += w;
                }
            }

            bm.UnlockBits(bd);
            return x;
        }

        public static unsafe int CountTransparentColumnsFromRight(Bitmap bm)
        {
            int x = 0;
            int w = bm.Width;
            int h = bm.Height;
            Rectangle rect = new(0, 0, bm.Width, bm.Height);
            BitmapData bd = bm.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            uint* endimg = &((uint*)bd.Scan0.ToPointer())[w * h];
            for (x = w - 1; x >= 0; x--) // Loop through columns
            {
                uint* Pxls = &((uint*)bd.Scan0.ToPointer())[x];
                while (Pxls < endimg)
                {
                    if ((*Pxls & 0xFF000000) != 0)
                    {
                        // This column is not completely transparent
                        bm.UnlockBits(bd);
                        return w - x - 1;
                    }
                    Pxls += w;
                }
            }

            // If we get down here, all the columns, and therefore the entire image,
            // is transparent
            bm.UnlockBits(bd);
            return w;
        }

        public static unsafe int CountTransparentRowsFromBottom(Bitmap bm)
        {
            int y = 0;
            int w = bm.Width;
            int h = bm.Height;
            Rectangle rect = new(0, 0, bm.Width, bm.Height);
            BitmapData bd = bm.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                for (y = h - 1; y >= 0; y--) // Loop through rows
                {
                    uint* pxls = &((uint*)bd.Scan0.ToPointer())[y * w];
                    uint* rowend = pxls + w;
                    while (pxls < rowend)
                    {
                        if ((*pxls & 0xFF000000) != 0)
                        {
                            // This row is not completely transparent
                            bm.UnlockBits(bd);
                            return h - y - 1;
                        }
                        pxls++;
                    }
                }
            }

            // If we get down here, all the rows, and therefore the entire image,
            // is transparent.
            bm.UnlockBits(bd);
            return h;
        }

        public static unsafe int CountTransparentRowsFromTop(Bitmap bm)
        {
            int y = 0;
            int w = bm.Width;
            int h = bm.Height;
            Rectangle rect = new(0, 0, bm.Width, bm.Height);
            BitmapData bd = bm.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                for (y = 0; y < h; y++) // Loop through rows
                {
                    uint* pxls = &((uint*)bd.Scan0.ToPointer())[y * w];
                    uint* rowend = pxls + w;
                    while (pxls < rowend)
                    {
                        if ((*pxls & 0xFF000000) != 0)
                        {
                            // This row is not completely transparent
                            bm.UnlockBits(bd);
                            return y;
                        }
                        pxls++;
                    }
                }
            }

            // If we get down here, all the rows, and therefore the entire image,
            // is transparent.
            bm.UnlockBits(bd);
            return h;
        }

        /// <summary>
        /// Generates a copy of the bitmap that has all completely transparent rows 
        /// and columns of bordering pixels removed. If the specified bitmap is 
        /// completely transparent, this is a special case under which a copy of 
        /// the bitmap is returned.
        /// </summary>
        public static Bitmap CropTransparentBorders(Bitmap bm)
        {
            int x1 = CountTransparentColumnsFromLeft(bm);

            // Special case if the whole bitmap is transparent
            if (x1 == bm.Width)
            {
                return new Bitmap(bm);
            }

            int x2 = CountTransparentColumnsFromRight(bm);
            int y1 = CountTransparentRowsFromTop(bm);
            int y2 = CountTransparentRowsFromBottom(bm);

            int w = bm.Width - (x1 + x2) + 1;
            int h = bm.Height - (y1 + y2) + 1;

            // Allocate the new bitmap
            Bitmap bm2 = new(w, h, PixelFormat.Format32bppArgb);

            // Get the graphics object and render the subsection
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bm2);
            g.DrawImage(bm,
                new Rectangle(0, 0, w, h), // dest rect
                new Rectangle(x1, y1, w, h), // source rect
                GraphicsUnit.Pixel);
            g.Dispose();

            return bm2;
        }

        public static Bitmap CropTransparentSideBorders(Bitmap bm)
        {
            int x1 = CountTransparentColumnsFromLeft(bm);

            // Special case if the whole bitmap is transparent
            if (x1 == bm.Width)
            {
                return new Bitmap(bm);
            }

            int x2 = CountTransparentColumnsFromRight(bm);
            int w = bm.Width - (x1 + x2) + 1;

            // Allocate the new bitmap
            Bitmap bm2 = new(w, bm.Height, PixelFormat.Format32bppArgb);

            // Get the graphics object and render the subsection
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bm2);
            g.DrawImage(bm,
                new Rectangle(0, 0, w, bm.Height), // dest rect
                new Rectangle(x1, 0, w, bm.Height), // source rect
                GraphicsUnit.Pixel);
            g.Dispose();

            return bm2;
        }

        public static unsafe void FillRawBits32(Bitmap bm, uint* bits, int w, int h)
        {
            // Fills the "bits" array with 32-bit image data from the bitmap

            int PixelCount = w * h;
            Rectangle rect = new(0, 0, w, h);
            BitmapData bd = bm.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            uint* ui = (uint*)bd.Scan0.ToPointer();
            int i = 0;
            while (i < PixelCount)
            {
                bits[i] = ui[i];
                i++;
            }
            bm.UnlockBits(bd);
        }

        public static PixelFormat FormatFrombpp(int bpp)
        {
            switch (bpp)
            {
                case 32:
                    return PixelFormat.Format32bppArgb;

                case 24:
                    return PixelFormat.Format24bppRgb;

                case 16:
                    return PixelFormat.Format16bppRgb565;

                case 15:
                    return PixelFormat.Format16bppRgb555;

                case 8:
                    return PixelFormat.Format8bppIndexed;

                case 1:
                    return PixelFormat.Format1bppIndexed;
            }
            return PixelFormat.Undefined;
        }

        // Untested:
        public static unsafe Bitmap FromBitsNative(void* bits, int w, int h, int bpp)
        {
            // Generates the bitamp in its "native" format

            uint imgsize = ComputeSize((uint)w, (uint)h, (uint)bpp, true);
            PixelFormat pf = FormatFrombpp(bpp);
            Bitmap bm = new(w, h);
            BitmapData bd = bm.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, pf);
            byte* BMbits = (byte*)bd.Scan0.ToPointer();

            // Not the fastest way to copy a memory block, but the easiest
            for (uint i = 0; i < imgsize; i++)
            {
                BMbits[i] = ((byte*)bits)[i];
            }

            bm.UnlockBits(bd);
            return bm;
        }

        public static unsafe Bitmap FromRawBits24(void* bits, int w, int h,
            bool PaddedTo32Bit)
        {
            // Makes a 32-bit bitmap from the 24-bit data
            Bitmap bm = new(w, h);
            Rectangle rect = new(0, 0, w, h);
            BitmapData bd = bm.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            // Compute the source row size
            int SRowSize = w * 3;
            if (PaddedTo32Bit && SRowSize % 4 != 0)
            {
                SRowSize += (4 - (SRowSize % 4));
            }

            byte* bsrc = (byte*)bits;
            byte* bdest = (byte*)bd.Scan0.ToPointer();

            // Loop through rows
            for (int y = 0; y < h; y++)
            {
                uint* destrow = (uint*)(bdest + y * bd.Stride);
                byte* srcrow = &bsrc[y * SRowSize];

                if (y == h - 1) // We handle the last row in a different fashion
                {
                    // Although it could probably be done with no error, we don't want
                    // exceed the boundary of the image data by one byte.
                    for (int x = 0; x < w; x++)
                    {
                        ushort firsttwo = *((ushort*)&srcrow[x * 3]);
                        byte third = srcrow[x * 3 + 2];
                        destrow[x] = firsttwo | ((uint)third) << 16 | 0xFF000000;
                    }
                }
                else
                {
                    // Loop through the pixels in this row
                    for (int x = 0; x < w; x++)
                    {
                        destrow[x] = *((uint*)&srcrow[x * 3]) | 0xFF000000;
                    }
                }
            }
            bm.UnlockBits(bd);
            return bm;
        }

        public static unsafe Bitmap FromRawBits32(uint* bits, int w, int h)
        {
            int PixelCount = w * h;
            Bitmap bm = new(w, h);
            Rectangle rect = new(0, 0, w, h);
            BitmapData bd = bm.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            uint* ui = (uint*)bd.Scan0.ToPointer();
            int i = 0;
            while (i < PixelCount)
            {
                ui[i] = bits[i];
                i++;
            }
            bm.UnlockBits(bd);
            return bm;
        }

        public static unsafe Bitmap FromRawBits4(void* bits, uint* Palette, int w, int h,
            bool PaddedTo32Bit)
        {
            // Makes a 32-bit bitmap from the indices and palette data
            Bitmap bm = new(w, h);
            Rectangle rect = new(0, 0, w, h);
            BitmapData bd = bm.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            // Compute the source row size
            int SRowSize = w / 2;
            if (PaddedTo32Bit && SRowSize % 4 != 0)
            {
                SRowSize += (4 - (SRowSize % 4));
            }

            byte* bsrc = (byte*)bits;
            byte* bdest = (byte*)bd.Scan0.ToPointer();
            uint index = 0;

            // Loop through rows
            for (int y = 0; y < h; y++)
            {
                uint* destrow = (uint*)(bdest + y * bd.Stride);
                byte* srcrow = &bsrc[y * SRowSize];

                // Loop through the pixels in this row
                for (int x = 0; x < w; x++)
                {
                    bool even = (x % 2 == 0);
                    index = (uint)(srcrow[x / 2] & (even ? 0xF0 : 0x0F));
                    if (even)
                    {
                        index >>= 4;
                    }
                    destrow[x] = Palette[index] | 0xFF000000;
                }
            }
            bm.UnlockBits(bd);
            return bm;
        }

        public static unsafe Bitmap FromRawBits8(void* bits, uint* Palette, int w, int h,
            bool PaddedTo32Bit)
        {
            // Makes a 32-bit bitmap from the indices and palette data
            Bitmap bm = new(w, h);
            Rectangle rect = new(0, 0, w, h);
            BitmapData bd = bm.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            // Compute the source row size
            int SRowSize = w;
            if (PaddedTo32Bit && w % 4 != 0)
            {
                SRowSize += (4 - (w % 4));
            }

            byte* bsrc = (byte*)bits;
            byte* bdest = (byte*)bd.Scan0.ToPointer();

            // Loop through rows
            for (int y = 0; y < h; y++)
            {
                uint* destrow = (uint*)(bdest + y * bd.Stride);
                byte* srcrow = &bsrc[y * SRowSize];

                // Loop through the pixels in this row
                for (int x = 0; x < w; x++)
                {
                    destrow[x] = Palette[srcrow[x]] | 0xFF000000;
                }
            }
            bm.UnlockBits(bd);
            return bm;
        }

        public static unsafe Bitmap FromRawBitsBinary(void* bits, int w, int h, bool PaddedTo32Bit)
        {
            // Makes a 32-bit bitmap from the indices and palette data
            Bitmap bm = new(w, h);
            Rectangle rect = new(0, 0, w, h);
            BitmapData bd = bm.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            // Compute the source row size
            int SRowSize = w / 8;
            if (PaddedTo32Bit && SRowSize % 4 != 0)
            {
                SRowSize += (4 - (SRowSize % 4));
            }

            byte* bsrc = (byte*)bits;
            byte* bdest = (byte*)bd.Scan0.ToPointer();
            uint index = 0;

            // Loop through rows
            for (int y = 0; y < h; y++)
            {
                uint* destrow = (uint*)(bdest + y * bd.Stride);
                byte* srcrow = &bsrc[y * SRowSize];

                // Loop through the pixels in this row
                for (int x = 0; x < w; x++)
                {
                    index = srcrow[x / 8];
                    index = (index >> (7 - (x % 8))) & 1;
                    destrow[x] = (index == 0) ? 0xFF000000 : 0xFFFFFFFF;
                }
            }
            bm.UnlockBits(bd);
            return bm;
        }

        public static unsafe Bitmap FromRawBitsbpp(void* bits, uint* Palette, int w, int h,
            int bpp, bool PaddedTo32Bit)
        {
            if (bpp == 32)
            {
                return FromRawBits32((uint*)bits, w, h);
            }
            else if (bpp == 24)
            {
                return FromRawBits24((uint*)bits, w, h, PaddedTo32Bit);
            }
            else if (bpp == 16)
            {
                // Risky, untested
                return FromBitsNative(bits, w, h, bpp);
            }
            else if (bpp == 8)
            {
                return FromRawBits8(bits, Palette, w, h, PaddedTo32Bit);
            }
            else if (bpp == 4)
            {
                return FromRawBits4(bits, Palette, w, h, PaddedTo32Bit);
            }
            else if (bpp == 1)
            {
                return FromRawBitsBinary(bits, w, h, PaddedTo32Bit);
            }
            return null; // Unsupported bits per pixel
        }

        public static int GetBitsPerPixel(Bitmap bm)
        {
            switch (bm.PixelFormat)
            {
                case PixelFormat.Format32bppArgb:
                case PixelFormat.Format32bppPArgb:
                case PixelFormat.Format32bppRgb:
                    return 32;

                case PixelFormat.Format24bppRgb:
                    return 24;

                case PixelFormat.Format8bppIndexed:
                    return 8;
            }
            return 0;
        }

        public static void MakeOpaque(Bitmap bm)
        {
            int w = bm.Width;
            int h = bm.Height;
            int PixelCount = w * h;
            Rectangle rect = new(0, 0, w, h);
            BitmapData bd = bm.LockBits(rect,
                System.Drawing.Imaging.ImageLockMode.WriteOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            unsafe
            {
                uint* iImg = (uint*)bd.Scan0.ToPointer();
                int i = 0;
                while (i < PixelCount)
                {
                    iImg[i] |= 0xFF000000;
                    i++;
                }
            }
            bm.UnlockBits(bd);
        }

        public static void MakeSolidColor(Bitmap bm, uint IntColor)
        {
            int w = bm.Width;
            int h = bm.Height;
            int PixelCount = w * h;
            Rectangle rect = new(0, 0, w, h);
            BitmapData bd = bm.LockBits(rect,
                System.Drawing.Imaging.ImageLockMode.WriteOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            unsafe
            {
                uint* iImg = (uint*)bd.Scan0.ToPointer();
                int i = 0;
                while (i < PixelCount)
                {
                    iImg[i] = IntColor;
                    i++;
                }
            }
            bm.UnlockBits(bd);
        }

        public static unsafe bool MaskToAlpha(Bitmap srcBM, Bitmap BWMask)
        {
            // The dimensions of the image and the mask must be equal
            if (srcBM.Width != BWMask.Width || srcBM.Height != BWMask.Height)
            {
                return false;
            }

            // Gather information about the images and lock them
            int h = srcBM.Height;
            int w = srcBM.Width;
            Rectangle Rect = new(0, 0, w, h);
            uint* MaskBits = null;
            uint* NewBits = null;
            BitmapData MaskBD = null;
            BitmapData BD = null;
            try
            {
                MaskBD = BWMask.LockBits(Rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                MaskBits = (uint*)MaskBD.Scan0.ToPointer();

                BD = srcBM.LockBits(Rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                NewBits = (uint*)BD.Scan0.ToPointer();
            }
            catch (Exception)
            {
                return false;
            }

            // Go through the pixels, zeroing the alpha_hover whenever a white pixel is
            // encountered in the mask.
            int PxlCount = w * h;
            for (int i = 0; i < PxlCount; i++)
            {
                if ((MaskBits[i] & 0x00FFFFFF) == 0x00FFFFFF)
                {
                    NewBits[i] &= 0x00FFFFFF;
                }
            }

            // Unlock
            BWMask.UnlockBits(MaskBD);
            srcBM.UnlockBits(BD);
            return true;
        }

        /// <summary>
        /// Resizes the bitmap, filling with zero-alpha_hover pixels for expanded regions and
        /// croping regions when contracting dimensions. The upper left corner of the 
        /// image is used as the anchor point. A new bitmap is created and returned.
        /// </summary>
        public static unsafe Bitmap ResizeCropPad(Bitmap bm, int NewWidth, int NewHeight)
        {
            // Handle the special case first
            if (NewWidth == bm.Width && NewHeight == bm.Height)
            {
                return new Bitmap(bm);
            }

            // Gather information about the original image and lock it
            int OldHeight = bm.Height;
            int OldWidth = bm.Width;
            Rectangle OldRect = new(0, 0, OldWidth, OldHeight);
            BitmapData OldBD = bm.LockBits(OldRect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            uint* OldBits = (uint*)OldBD.Scan0.ToPointer();

            Bitmap NewBM = new(NewWidth, NewHeight);
            Rectangle NewRect = new(0, 0, NewWidth, NewHeight);
            BitmapData NewBD = NewBM.LockBits(NewRect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            uint* NewBits = (uint*)NewBD.Scan0.ToPointer();

            // Compute row copy size, in pixels
            int RowCopySize = (NewWidth > bm.Width) ? bm.Width : NewWidth;

            // Build the new image
            for (int y = 0; y < NewHeight; y++)
            {
                uint* dest = &NewBits[y * NewWidth];
                uint* src = &OldBits[y * OldWidth];

                if (y >= OldHeight)
                {
                    // If the y-value is outside the bounds of the original image, then
                    // this whole row gets zero pixels.
                    for (int x = 0; x < NewWidth; x++)
                    {
                        dest[x] = 0;
                    }
                }
                else
                {
                    for (int x = 0; x < NewWidth; x++)
                    {
                        if (x < RowCopySize)
                        {
                            dest[x] = src[x];
                        }
                        else
                        {
                            dest[x] = 0;
                        }
                    }
                }
            }

            NewBM.UnlockBits(NewBD);
            bm.UnlockBits(OldBD);
            return NewBM;
        }
    }
}