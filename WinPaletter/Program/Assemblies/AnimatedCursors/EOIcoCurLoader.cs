// Author:          Evan Olds
// Creation Date:   February 10, 2008
// Description_SysEventsSounds:     Class for loading icon or cursor data from streams
using EOFC;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

public class EOIcoCurLoader
{
    public unsafe delegate void* EOIcoCurAllocator(int size);
    public unsafe delegate void EOIcoCurFree(void* ptr);

    private readonly long m_initialStreamPos;
    public string ErrorMsg;
    private Point HotSpot;

    private readonly BinaryReader m_reader = null;

    public enum Type
    {
        Type_Icon = 1,
        Type_Cursor = 2
    }

    #region Helpful structures
    private struct IcoHeader(Type type)
    {
        public ushort Reserved = 0;
        public ushort Type = (ushort)type; // 1=icon, 2=cursor
        public ushort Count = 0;

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
        public void ReadFromStream(BinaryReader reader)
        {
            Reserved = reader.ReadUInt16();
            Type = reader.ReadUInt16();
            Count = reader.ReadUInt16();
        }
    }

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

        public void ReadFromStream(BinaryReader reader)
        {
            bWidth = reader.ReadByte();
            bHeight = reader.ReadByte();
            bColorCount = reader.ReadByte();
            bReserved = reader.ReadByte();
            Planes_XHotspot = reader.ReadUInt16();
            BitCount_YHotspot = reader.ReadUInt16();
            dwBytesInRes = reader.ReadUInt32();
            dwImageOffset = reader.ReadUInt32();
        }
    }

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
        public void ReadFromStream(BinaryReader reader)
        {
            StructSize = reader.ReadUInt32();
            Width = reader.ReadInt32();
            Height = reader.ReadInt32();
            Planes = reader.ReadUInt16();
            BitCount = reader.ReadUInt16();
            biCompression = reader.ReadUInt32();
            biSizeImage = reader.ReadUInt32();
            biXPelsPerMeter = reader.ReadInt32();
            biYPelsPerMeter = reader.ReadInt32();
            biClrUsed = reader.ReadUInt32();
            biClrImportant = reader.ReadUInt32();
        }
    }
    #endregion

    /// <summary>
    /// Initializes an instance to load icons from the specified stream at its current position. 
    /// When constructed the stream must be at the icon/cursor data start position.
    /// </summary>
    /// <param name="icoCurStream">Stream to load icon/cursor data from. This stream's current 
    /// position must be set to the location where the icon/cursor data begins.</param>
    public EOIcoCurLoader(Stream icoCurStream)
    {
        if (!icoCurStream.CanRead)
        {
            throw new ArgumentException(
                "Cannot initialize EOIcoCurLoader with a stream that doesn't support reading");
        }

        // Create a binary reader for the stream
        m_reader = new BinaryReader(icoCurStream, Encoding.ASCII, true);

        m_initialStreamPos = m_reader.BaseStream.Position;
        ErrorMsg = "An unspecified error has occured";
    }

    /// <summary>
    /// Counts the number of images available in the icon/cursor stream.
    /// 
    /// Returns -2 if the stream data does not represent a valid icon or cursor.
    /// Returns -1 on all other errors.
    /// </summary>
    public int CountImages()
    {
        long oldPos = m_reader.BaseStream.Position;

        // Seek to the start of the stream
        m_reader.BaseStream.Position = m_initialStreamPos;

        byte[] data = new byte[6];
        try
        {
            m_reader.Read(data, 0, 6);
        }
        catch (Exception e)
        {
            ErrorMsg = $"Could not get 6 bytes from the beginning of the stream. The following exception was generated:\r\n{e}";
            return -1;
        }

        // A valid icon/cursor will start with two reserved bytes, which must be zero,
        // followed by a 16-bit unsigned integer which will be 1 for icons and 2 for
        // cursors.
        if (data[0] != 0 || data[1] != 0 || (data[2] != 1 && data[2] != 2) || data[3] != 0)
        {
            return -2;
        }

        int count = data[4] + data[5] * 256;
        m_reader.BaseStream.Position = oldPos;
        return count;
    }

    public unsafe Bitmap GetImage(uint ImageIndex)
    {
        int i;
        Bitmap bm = null;

        // Seek to the appropriate position in the stream
        m_reader.BaseStream.Position = m_initialStreamPos;

        // Load the icon header
        IcoHeader hdr = new();
        hdr.ReadFromStream(m_reader);

        // Make sure that the "ImageIndex" parameter is ok
        if (ImageIndex >= hdr.Count)
        {
            ErrorMsg = $"Invalid image index of {Convert.ToString(ImageIndex)} was passed to GetImage";
            return null;
        }

        // Allocate array of directory entries
        DirectoryEntry[] idEntries = new DirectoryEntry[hdr.Count];
        // Load each directory entry from the stream
        for (i = 0; i < hdr.Count; i++)
        {
            idEntries[i].ReadFromStream(m_reader);
        }

        // Pull out hotspot data, even though it may not be valid if this
        // resource is an icon and not a cursor.
        HotSpot = new(idEntries[ImageIndex].Planes_XHotspot,
            idEntries[ImageIndex].BitCount_YHotspot);

        // Make sure the image offset is within the length of the stream
        if (m_initialStreamPos + idEntries[ImageIndex].dwImageOffset >
            m_reader.BaseStream.Length)
        {
            ErrorMsg = "Directory entry is invalid. Image offset is outside of the bounds of " +
                "the input stream.";
            return null;
        }

        // Seek to appropriate position in stream
        m_reader.BaseStream.Position = m_initialStreamPos + idEntries[ImageIndex].dwImageOffset;

        // At this point there's a possibility that what follows is PNG image data.
        uint PNGsig = m_reader.ReadUInt32();
        if (0x474E5089 == PNGsig)
        {
            // Seek back to the beginning of the PNG image data
            m_reader.BaseStream.Seek(-4, SeekOrigin.Current);

            // Create an offset stream so .NET can load
            OffsetStream os = new(m_reader.BaseStream);

            try
            {
                bm = new(os);
                bm.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }
            catch (ArgumentException)
            {
                return null;
            }
            return bm;
        }
        m_reader.BaseStream.Seek(-4, SeekOrigin.Current);

        // Get the image info
        uint w = 0, h = 0, bpp = 0;
        GetImageDimensions(ImageIndex, ref w, ref h, ref bpp);

        // Load BITMAPINFOHEADER structure
        BITMAPINFOHEADER BIH = new();
        BIH.ReadFromStream(m_reader);

        // Read palette data, if it is present
        uint* Palette = stackalloc uint[256];
        if (bpp <= 8)
        {
            int clrcount = 1 << (int)bpp;
            for (i = 0; i < clrcount; i++)
            {
                Palette[i] = m_reader.ReadUInt32();
            }
        }

        byte[] imgBytes = new byte[SizeComp(w, h, bpp)];
        m_reader.Read(imgBytes, 0, imgBytes.Length);
        fixed (byte* bits = imgBytes)
        {
            bm = BitmapOps.FromRawBitsbpp(bits, Palette, (int)w, (int)h, (int)bpp, true);
        }
        if (bm != null && bpp != 32)
        {
            // Generate an alpha_hover channel from the mask
            int maskSize = (int)SizeComp(w, h, 1);
            byte[] maskBytes = new byte[maskSize];
            m_reader.Read(maskBytes, 0, maskBytes.Length);
            Bitmap bmMask = null;
            fixed (byte* mask = maskBytes)
            {
                bmMask = BitmapOps.FromRawBitsBinary(mask, (int)w, (int)h, true);
            }
            BitmapOps.MaskToAlpha(bm, bmMask);
        }

        bm.RotateFlip(RotateFlipType.Rotate180FlipX);

        return bm;
    }

    // Gets the dimensions and bits per pixel of the image at the specified index. The underlying
    // stream's position is preserved.
    public bool GetImageDimensions(uint ImageIndex, ref uint out_Width, ref uint out_Height,
        ref uint out_bpp)
    {
        // Back up current stream position
        long oldPos = m_reader.BaseStream.Position;

        // Seek to the initial stream position
        m_reader.BaseStream.Position = m_initialStreamPos;

        // Read in the File header
        IcoHeader hdr = new();
        hdr.ReadFromStream(m_reader);

        // Make sure that the "ImageIndex" parameter is ok
        if (ImageIndex >= hdr.Count)
        {
            ErrorMsg = $"Invalid image index passed to GetImageDimensions.\r\nImage index: {Convert.ToString(ImageIndex)}\r\nAvailable image count: {Convert.ToString(hdr.Count)}";
            return false;
        }

        // Read in directory entries
        DirectoryEntry[] de = new DirectoryEntry[hdr.Count];
        for (int x = 0; x < hdr.Count; x++)
        {
            de[x].ReadFromStream(m_reader);
        }

        // Seek to appropriate position in stream
        long SeekPos = m_initialStreamPos + de[ImageIndex].dwImageOffset;
        try
        {
            m_reader.BaseStream.Seek(SeekPos, SeekOrigin.Begin);
        }
        catch (Exception)
        {
            ErrorMsg = $"Could not seek to appropriate position in icon stream data.\r\nThe file data may be truncated, inaccessible or invalid.\r\nAttempted seek position: {Convert.ToString(SeekPos)}";
            // Seek back to original stream position
            m_reader.BaseStream.Seek(oldPos, SeekOrigin.Begin);
            return false;
        }

        // At this point there's a possibility that what follows is PNG image data.
        uint PNGsig = m_reader.ReadUInt32();
        if (0x474E5089 == PNGsig)
        {
            // Seek back to the beginning of the PNG image data
            m_reader.BaseStream.Seek(-4, SeekOrigin.Current);

            // Create an offset stream so .NET can load
            OffsetStream os = new(m_reader.BaseStream);

            try
            {
                using (Bitmap bm = new(os))
                {
                    out_Width = (uint)bm.Width;
                    out_Height = (uint)bm.Height;
                    out_bpp = PixelFormatTobpp(bm.PixelFormat);
                }
            }
            catch (ArgumentException)
            {
                return false;
            }
            return true;
        }
        m_reader.BaseStream.Seek(-4, SeekOrigin.Current);

        // Load BITMAPINFOHEADER structure
        BITMAPINFOHEADER bih = new();
        bih.ReadFromStream(m_reader);

        // Get bits per pixel
        out_bpp = bih.BitCount;

        // Get dimensions and "fix" them, if necessary
        out_Width = de[ImageIndex].bWidth;
        out_Height = de[ImageIndex].bHeight;
        // If the computed size does not match the indicated size, see if
        // we can convert 255 dimensions to 256
        uint LiteralSize = SizeComp(out_Width, out_Height, out_bpp) +
            SizeComp(out_Width, out_Height, 1);
        if (out_Width == 0 && out_Height == 0)
        {
            out_Width = (uint)bih.Width;
            out_Height = (uint)bih.Height / 2;
        }
        else if (LiteralSize != de[ImageIndex].dwBytesInRes)
        {
            if (out_Width == 255)
            {
                out_Width = 256;
            }
            if (out_Height == 255)
            {
                out_Height = 256;
            }
        }

        // If the width or height is zero, make it 256
        if (out_Width == 0)
        {
            out_Width = 256;
        }
        if (out_Height == 0)
        {
            out_Height = 256;
        }

        // Seek back to original stream position
        m_reader.BaseStream.Seek(oldPos, SeekOrigin.Begin);

        return true;
    }

    private uint PixelFormatTobpp(PixelFormat pf)
    {
        return pf switch
        {
            PixelFormat.Format32bppArgb => 32,
            PixelFormat.Format24bppRgb => 24,
            PixelFormat.Format16bppRgb565 => 16,
            PixelFormat.Format16bppRgb555 => 15,
            PixelFormat.Format8bppIndexed => 8,
            PixelFormat.Format4bppIndexed => 4,
            PixelFormat.Format1bppIndexed => 1,
            _ => 32,
        };
    }

    private uint SizeComp(uint w, uint h, uint bpp)
    {
        // Compute the row size
        uint RowSize = w * bpp / 8;
        if (RowSize % 4 != 0)
        {
            RowSize += 4 - (RowSize % 4);
        }
        return h * RowSize;
    }
}