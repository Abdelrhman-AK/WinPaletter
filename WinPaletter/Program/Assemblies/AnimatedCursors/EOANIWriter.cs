// Author:          Evan Olds
// Creation Date:   May 26, 2007
// Description:     Class to write animated cursor files to a stream.
// Notes:           The stream must support seeking.

using System;
using System.Drawing;
using System.IO;
using System.Text;

public class EOANIWriter
{
    private uint FramesListDataSize;
    private System.Drawing.Point HotSpot;
    private long initialStreamPosition;

    public BinaryWriter m_writer = null;

    // Position in the file where the 4-byte size value for the frames list resides
    private long FramesListSizePosition;

    private static readonly byte[] fourccACON = { (byte)'A', (byte)'C', (byte)'O', (byte)'N' };
    private static readonly byte[] fourccfram = { (byte)'f', (byte)'r', (byte)'a', (byte)'m' };
    private static readonly byte[] fourccIART = { (byte)'I', (byte)'A', (byte)'R', (byte)'T' };
    private static readonly byte[] fourccicon = { (byte)'i', (byte)'c', (byte)'o', (byte)'n' };
    private static readonly byte[] fourccINAM = { (byte)'I', (byte)'N', (byte)'A', (byte)'M' };
    private static readonly byte[] fourccINFO = { (byte)'I', (byte)'N', (byte)'F', (byte)'O' };
    private static readonly byte[] fourccLIST = { (byte)'L', (byte)'I', (byte)'S', (byte)'T' };
    private static readonly byte[] fourccrate = { (byte)'r', (byte)'a', (byte)'t', (byte)'e' };
    private static readonly byte[] fourccseq = { (byte)'s', (byte)'e', (byte)'q', (byte)' ' };
    private static readonly byte[] fourccRIFF = { (byte)'R', (byte)'I', (byte)'F', (byte)'F' };

    public EOANIWriter(Stream outputStream, uint frameCount, uint avgFrameRate, uint[] frameRates,
        uint[] frameNums, string NameInfo, string ArtistInfo, System.Drawing.Point CursorHotSpot)
    {
        if (!outputStream.CanSeek)
        {
            throw new NotSupportedException(
                "Writing animated cursors to streams that don't support seeking is not supported");
        }

        // Create a binary writer for the stream
        m_writer = new BinaryWriter(outputStream, Encoding.ASCII, true);

        FramesListDataSize = 4; // Starts with just "fram"
        initialStreamPosition = m_writer.BaseStream.Position;
        HotSpot = new System.Drawing.Point(CursorHotSpot.X, CursorHotSpot.Y);

        // Create the RIFF stuff for an animated cursor file
        m_writer.Write(fourccRIFF, 0, 4);
        m_writer.Write(0);  // Placeholder for RIFF chunk size
        m_writer.Write(fourccACON, 0, 4);

        // After writing "ACON" we'll write the INFO list. The function will only perform a write
        // if either string is non-null
        WriteInfoList(NameInfo, ArtistInfo);

        // Now write the "anih" chunk
        Writeanih(frameCount, avgFrameRate);

        // Write "rate" and "seq " chunks
        if (frameRates != null && frameNums != null)
        {
            this.Writerateseq(frameRates, frameNums);
        }

        m_writer.Write(fourccLIST, 0, 4);
        FramesListSizePosition = m_writer.BaseStream.Position;
        m_writer.Write(0);  // Placeholder for LIST chunk size
        m_writer.Write(fourccfram, 0, 4);

        // Update main RIFF chunk size value
        UpdateRIFFChunkSize();

        // We are now ready for frame writing functions to be called
    }

    private void UpdateRIFFChunkSize()
    {
        long BackupPos = m_writer.BaseStream.Position;
        m_writer.BaseStream.Position = initialStreamPosition + 4;
        uint newsize = (uint)(BackupPos - initialStreamPosition) - 8;
        m_writer.Write(newsize);
        m_writer.BaseStream.Position = BackupPos;
    }

    private void Writeanih(uint NumFrames, uint FrameRate)
    {
        // Write the block's signature
        byte[] anih4cc = { (byte)'a', (byte)'n', (byte)'i', (byte)'h' };
        m_writer.Write(anih4cc, 0, 4);

        // Not sure why, but this 44 byte block seems to start
        // with two integers that have values of 36.
        uint thirtysix = 36;
        m_writer.Write(thirtysix);
        m_writer.Write(thirtysix);

        // Write the number of frames and number of steps, which are 
        // the same in this case.
        m_writer.Write(NumFrames);
        m_writer.Write(NumFrames);

        // Write zeros for width, height, bitcount and planes
        for (int i = 0; i < 4; i++)
        {
            m_writer.Write((uint)0);
        }

        // Write display rate
        m_writer.Write(FrameRate);

        // Write flags
        // First bit being set indicates that frames are cursor files
        m_writer.Write((uint)1);
    }

    public bool WriteFrame(Bitmap frame)
    {
        uint FrameSize = 0;
        // The user should pass in a 32x32 bitmap, but we won't check this
        uint w = (uint)frame.Width;
        uint h = (uint)frame.Height;

        FrameSize = w * h * 4 + (w * h / 8) +
            6 +     // Size of icon file header
            16 +    // Size of icon directory entry
            40;     // Size of bitmap info header

        m_writer.Write(fourccicon, 0, 4);
        m_writer.Write(FrameSize);

        // WORK ON THIS TO MAKE IT ACCEPT DIFFERENT BITMAP SIZES !!??
        {
            EOIcoCurWriter writer = new EOIcoCurWriter(m_writer.BaseStream, 1, EOIcoCurWriter.IcoCurType.Cursor);
            writer.WriteBitmap(frame, null, HotSpot);

            // Update the data size of the written frame data
            FramesListDataSize += (FrameSize + 8);
            long BackupPos = m_writer.BaseStream.Position;
            m_writer.BaseStream.Position = this.FramesListSizePosition;
            m_writer.Write(FramesListDataSize);
            m_writer.BaseStream.Position = BackupPos;
        }

        // Update main RIFF chunk size
        UpdateRIFFChunkSize();

        return true;
    }

    private void WriteInfoList(string NameInfo, string ArtistInfo)
    {
        // If both strings are null, don't write anything
        if (NameInfo == null || ArtistInfo == null)
        {
            return;
        }

        uint len1 = 0;
        uint len2 = 0;

        // Strings must have a null terminating character written. If the length with
        // the null-terminating character is odd, then an additional null-byte will
        // be appended for even-length padding.
        if (NameInfo != null)
        {
            len1 = (uint)NameInfo.Length + 1 + 8;
            len1 += (len1 % 2);
        }
        if (ArtistInfo != null)
        {
            len2 = (uint)ArtistInfo.Length + 1 + 8;
            len2 += (len2 % 2);
        }

        // Write the list four character code
        m_writer.Write(fourccLIST, 0, 4);

        // Write the size of this chunk
        m_writer.Write(len1 + len2 + 4);

        // Write "INFO"
        m_writer.Write(fourccINFO, 0, 4);

        // Write the INAM chunk if a string was provided
        if (NameInfo != null)
        {
            WriteStringChunk(fourccINAM, NameInfo);
        }

        // Write the IART chunk if a string was provided
        if (ArtistInfo != null)
        {
            WriteStringChunk(fourccIART, ArtistInfo);
        }
    }

    // Writes the "rate" and "seq " chunks to the stream at the current position. Both
    // arrays are expected to have the same length and must be non-null.
    private void Writerateseq(uint[] FrameRates, uint[] FrameNums)
    {
        int i;

        // Write "rate"
        m_writer.Write(fourccrate, 0, 4);

        // Write the size of the rate chunk
        m_writer.Write((uint)FrameRates.Length * 4);

        for (i = 0; i < FrameRates.Length; i++)
        {
            m_writer.Write(FrameRates[i]);
        }

        // Write "seq "
        m_writer.Write(fourccseq, 0, 4);

        // Write the size of the sequence chunk
        m_writer.Write((uint)FrameNums.Length * 4);

        for (i = 0; i < FrameNums.Length; i++)
        {
            m_writer.Write(FrameNums[i]);
        }
    }

    private void WriteStringChunk(byte[] fourcc, string str)
    {
        m_writer.Write(fourcc, 0, 4);

        byte[] strBytes = Encoding.ASCII.GetBytes(str);
        uint len = (uint)strBytes.Length;
        if (1 == (len % 2))
        {
            // Pad to even size
            len++;
        }

        // Write the size/length, the string bytes, and the padding byte if needed
        m_writer.Write(len);
        m_writer.Write(strBytes, 0, strBytes.Length);
        if (len != (uint)strBytes.Length)
        {
            m_writer.Write((byte)0);
        }
    }
}
