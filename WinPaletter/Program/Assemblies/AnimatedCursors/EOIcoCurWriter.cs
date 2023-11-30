// Author:
//    Evan Olds
// Creation Date:
//    (unknown)
// Significant alterations August 2014

using EOFC;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

public class EOIcoCurWriter
{
    public enum IcoCurType
    {
        Icon = 1,
        Cursor = 2
    }

    #region Helpful structures
    [StructLayout(LayoutKind.Sequential)]
    private struct IcoHeader
    {
        public ushort Reserved;
        public ushort Type; // 1=icon, 2=cursor
        public ushort Count;

        public IcoHeader(IcoCurType type)
        {
            Reserved = 0;
            Type = (ushort)type;
            Count = 0;
        }
        public bool IsValid()
        {
            // If the reserved value is non-zero and/or the type is not 1 or 2,
            // then it's probably safe to assume that the header is invalid.
            if (Reserved != 0 || (Type != 1 && Type != 2))
            {
                return false;
            }
            return true;
        }
        public static unsafe int GetStructSize()
        {
            return sizeof(IcoHeader);
        }
        public void WriteToStream(BinaryWriter writer)
        {
            writer.Write(Reserved);
            writer.Write(Type);
            writer.Write(Count);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct DirectoryEntry
    {
        public byte bWidth;
        public byte bHeight;
        public byte bColorCount;
        public byte bReserved;
        public ushort Planes_XHotspot;     // "Union" for planes (icons) and xhotspot (cursors)
        public ushort BitCount_YHotspot;   // "Union" for bit count (icons) and yhotspot (cursors)
        public uint dwBytesInRes;
        public uint dwImageOffset;

        public static unsafe int GetStructSize()
        {
            return sizeof(DirectoryEntry);
        }

        public void WriteToStream(BinaryWriter writer)
        {
            writer.Write(bWidth);
            writer.Write(bHeight);
            writer.Write(bColorCount);
            writer.Write(bReserved);
            writer.Write(Planes_XHotspot);
            writer.Write(BitCount_YHotspot);
            writer.Write(dwBytesInRes);
            writer.Write(dwImageOffset);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct BITMAPINFOHEADER
    {
        public uint StructSize;
        public int Width;
        public int Height;
        public ushort Planes;
        public ushort BitCount;
        public uint biCompression;
        public uint biSizeImage;
        public int biXPelsPerMeter;
        public int biYPelsPerMeter;
        public uint biClrUsed;
        public uint biClrImportant;

        public unsafe BITMAPINFOHEADER(int width, int height, int bpp)
        {
            StructSize = (uint)sizeof(BITMAPINFOHEADER);
            Width = width;
            Height = height;
            Planes = 1;
            BitCount = (ushort)bpp;
            biCompression = 0;
            biSizeImage = (uint)(width * height * bpp / 8);
            biXPelsPerMeter = 0;
            biYPelsPerMeter = 0;
            biClrUsed = 0;// (uint)(1 << bpp);
            biClrImportant = 0;
        }

        public static unsafe int GetStructSize()
        {
            return sizeof(BITMAPINFOHEADER);
        }
        public void WriteToStream(BinaryWriter writer)
        {
            writer.Write(StructSize);
            writer.Write((uint)Width);
            writer.Write((uint)Height);
            writer.Write(Planes);
            writer.Write(BitCount);
            writer.Write(biCompression);
            writer.Write(biSizeImage);
            writer.Write((uint)biXPelsPerMeter);
            writer.Write((uint)biYPelsPerMeter);
            writer.Write(biClrUsed);
            writer.Write(biClrImportant);
        }
    };
    #endregion

    private DirectoryEntry[] m_Entries;
    private int m_NumWritten;
    private long m_StreamStart;
    private IcoCurType m_type;
    private BinaryWriter m_writer;

    public string ErrorMsg;

    public EOIcoCurWriter(Stream outputStream, int imageCount, IcoCurType type)
    {
        // Make sure the stream supports seeking
        if (!outputStream.CanSeek)
        {
            throw new ArgumentException(
                "Icon/cursor output stream does not support seeking. A stream that supports " +
                "seeking is required to write icon and cursor data.");
        }

        // Store the stream position
        m_StreamStart = outputStream.Position;

        // Create a binary writer
        m_writer = new BinaryWriter(outputStream, System.Text.Encoding.ASCII, true);

        // Store the type
        m_type = type;

        // Prepare and write the header
        IcoHeader hdr = new(type);
        hdr.Count = (ushort)imageCount;
        hdr.WriteToStream(m_writer);

        m_NumWritten = 0;

        m_Entries = new DirectoryEntry[imageCount];
    }

    private unsafe void MakeMask(Bitmap AlphaImg, ref byte[] maskdata, int MaskRowSize)
    {
        // Rows in AlphaImg will be right-side-up while rows in MaskRowSize are
        // upside down.
        int BitsPerMaskRow = MaskRowSize * 8;
        int w = AlphaImg.Width;
        int h = AlphaImg.Height;
        Rectangle rect = new(0, 0, w, h);
        BitmapData bd = AlphaImg.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
        for (int y = 0; y < h; y++)
        {
            byte* bimg = ((byte*)bd.Scan0.ToPointer()) + bd.Stride * y + 3;
            int i = BitsPerMaskRow * (h - 1 - y);

            for (int x = 0; x < w; x++)
            {
                if (*bimg > 127)
                {
                    EOFC.BooleanBitArray.SetMSbFirst(maskdata, i, false);
                }
                else
                {
                    EOFC.BooleanBitArray.SetMSbFirst(maskdata, i, true);
                }

                i++;
                bimg += 4;
            }
        }
        AlphaImg.UnlockBits(bd);
    }

    private int SizeComp(int w, int h, int bpp)
    {
        // Compute the row size
        int RowSize = w * bpp / 8;
        if (RowSize % 4 != 0)
        {
            RowSize += (4 - (RowSize % 4));
        }
        return h * RowSize;
    }

    private uint SizeComp(uint w, uint h, uint bpp)
    {
        // Compute the row size
        uint RowSize = w * bpp / 8;
        if (RowSize % 4 != 0)
        {
            RowSize += (4 - (RowSize % 4));
        }
        return h * RowSize;
    }

    /// <summary>
    /// Writes a bitmap to the output stream. The image data is written in its current format if 
    /// both dimensions are less than 256. Otherwise it's written as an embedded PNG.
    /// </summary>
    /// <param name="img">Image to write to the output stream. This image's data is written 
    /// in its current format if both dimensions are less than 256. Otherwise it's written as an 
    /// embedded PNG.</param>
    /// <param name="AlphaImgMask">A 32-bit image whose alpha channel is to be used to 
    /// create the icon/cursor transparency mask. If this parameter is null, then "img" 
    /// will be used to create the mask data, but it must be 32-bpp in this case.</param>
    /// <param name="hotSpot">Cursor's hotspot. This will only be used if this instance of 
    /// EOIcoCurWriter was constructed with cursor as the specified type.</param>
    /// <returns>True upon success, false on failure.</returns>
    public bool WriteBitmap(Bitmap img, Bitmap AlphaImgMask, Point hotSpot)
    {
        int w = 0, h = 0, bpp = 0, imgsize = 0;
        long SeekPos = 0;
        long ImgOffset = IcoHeader.GetStructSize() +
            DirectoryEntry.GetStructSize() * m_Entries.Length;

        // Make sure the incoming image is valid
        if (null == img || img.Width <= 0 || img.Height <= 0)
        {
            ErrorMsg = "Invalid image passed to \"WriteBitmap\".";
            return false;
        }

        // If the width or height of the image is >=256, write a PNG file
        MemoryStream pngStream = null;
        if (img.Width >= 256 || img.Height >= 256)
        {
            pngStream = new();
            img.Save(pngStream, ImageFormat.Png);
        }

        // 1) Write a directory entry at the appropriate position in the stream
        w = img.Width;
        h = img.Height;
        bpp = BitmapOps.GetBitsPerPixel(img);
        imgsize = SizeComp(w, h, bpp);
        int MaskRowSize = w / 8;
        int transsize = (w * h / 8); // Size of transparency mask
        if (MaskRowSize % 4 != 0)
        {
            // The transparency data has 32-bit aligned rows
            int ExtraPerRow = (4 - (MaskRowSize % 4));
            MaskRowSize += ExtraPerRow;
            transsize += ExtraPerRow * h;
        }
        if (m_NumWritten != 0)
        {
            // Unless this is the first image that we are writing, the position
            // will be determined from the previous directory entry.
            ImgOffset = m_Entries[m_NumWritten - 1].dwImageOffset +
                m_Entries[m_NumWritten - 1].dwBytesInRes;
        }
        m_Entries[m_NumWritten].bWidth = (w >= 256) ? (byte)0 : (byte)w;
        m_Entries[m_NumWritten].bHeight = (h >= 256) ? (byte)0 : (byte)h;
        m_Entries[m_NumWritten].bColorCount = 0;
        m_Entries[m_NumWritten].bReserved = 0;
        m_Entries[m_NumWritten].Planes_XHotspot = 1;
        m_Entries[m_NumWritten].BitCount_YHotspot = (ushort)bpp;
        if (IcoCurType.Cursor == m_type)
        {
            m_Entries[m_NumWritten].Planes_XHotspot = (ushort)hotSpot.X;
            m_Entries[m_NumWritten].BitCount_YHotspot = (ushort)hotSpot.Y;
        }
        m_Entries[m_NumWritten].dwBytesInRes = (uint)(BITMAPINFOHEADER.GetStructSize() +
            ((bpp == 8) ? 1024 : 0) + imgsize + transsize);
        if (null != pngStream)
        {
            m_Entries[m_NumWritten].dwBytesInRes = (uint)pngStream.Length;
        }
        m_Entries[m_NumWritten].dwImageOffset = (uint)ImgOffset;
        // Write icon/cursor directory entry
        SeekPos = m_StreamStart + IcoHeader.GetStructSize() + DirectoryEntry.GetStructSize() * m_NumWritten;
        m_writer.BaseStream.Seek(SeekPos, SeekOrigin.Begin);
        m_Entries[m_NumWritten].WriteToStream(m_writer);

        // 2) Seek to where we want to write image data. This will be at ImgOffset + m_StreamStart.
        m_writer.BaseStream.Seek(m_StreamStart + ImgOffset, SeekOrigin.Begin);

        // If we have a PNG image, write the data from the memory stream
        if (null != pngStream)
        {
            m_writer.Write(pngStream.ToArray(), 0, (int)pngStream.Length);
        }
        else
        {
            // Create and write the bitmap info header
            BITMAPINFOHEADER bih = new(w, h * 2, bpp);
            bih.WriteToStream(m_writer);

            // Write palette data if need be
            if (8 == bpp)
            {
                ColorPalette pal = img.Palette;
                int PalCnt = pal.Entries.Length;
                // Write palette data
                for (int palI = 0; palI < PalCnt; palI++)
                {
                    m_writer.Write(pal.Entries[palI].ToArgb());
                }
            }

            // Write bitmap data
            img.RotateFlip(RotateFlipType.RotateNoneFlipY);
            Rectangle rect = new(0, 0, w, h);
            BitmapData bd = img.LockBits(rect, ImageLockMode.ReadOnly, img.PixelFormat);
            unsafe
            {
                EOFC.StreamOps.Write(m_writer, bd.Scan0.ToPointer(), imgsize);
            }
            img.UnlockBits(bd);
            img.RotateFlip(RotateFlipType.RotateNoneFlipY);

            // 3) Write masking data
            // The mask is built based on the alpha channel in the "AlphaImgMask" parameter.
            // If "AlphaImgMask" is null, alpha information from "img" will be used.
            Bitmap refbm = (AlphaImgMask == null) ? img : AlphaImgMask;
            byte[] maskdata = new byte[transsize];
            MakeMask(refbm, ref maskdata, MaskRowSize);
            m_writer.Write(maskdata, 0, transsize);
        }

        // We've written another image
        m_NumWritten++;

        return true;
    }
}