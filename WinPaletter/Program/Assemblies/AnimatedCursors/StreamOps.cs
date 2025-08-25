// Author:          Evan Olds
// Creation Date:   July 17, 2011
// Notes:
//    This class could use some cleaning up. Most functions are already provided by the 
//    BinaryReader/BinaryWriter class, so redundant functions should probably be 
//    eliminated. Also, untested stuff should either be tested or removed.

using System;
using System.IO;
using System.Text;

namespace EOFC
{
    internal static class StreamOps
    {
        public static int ReadInt(Stream inS)
        {
            byte[] buf = new byte[4];
            inS.Read(buf, 0, 4);
            return (int)(buf[0] | (((uint)buf[1]) << 8) |
                (((uint)buf[2]) << 16) | (((uint)buf[3]) << 24));
        }

        /// <summary>
        /// UNTESTED
        /// Reads sequential, null-terminated ANSI strings from a stream, returning the number of 
        /// strings that were read.
        /// </summary>
        public static int ReadNullTerminatedStrings(Stream s, string[] outArray, int index, int count)
        {
            int numRead = 0;
            for (int i = 0; i < count; i++)
            {
                StringBuilder sb = new();

                // Read until EOF or null terminator
                while (true)
                {
                    int intByte = s.ReadByte();
                    if (-1 == intByte)
                    {
                        return numRead;
                    }

                    // See if we hit a null-terminator
                    if (0 == intByte)
                    {
                        outArray[i + index] = sb.ToString();
                        numRead++;
                        break;
                    }
                    else
                    {
                        // Append the character to the string
                        sb.Append((char)intByte);
                    }
                }
            }

            return numRead;
        }

        public static long ReadLong(Stream s)
        {
            return ReadUInt(s) | (((long)ReadUInt(s)) << 32);
        }

        public static short ReadShort(Stream inS)
        {
            byte[] buf = new byte[2];
            inS.Read(buf, 0, 2);
            return (short)(buf[0] | (((uint)buf[1]) << 8));
        }

        public static uint ReadUInt(Stream inS)
        {
            byte[] buf = new byte[4];
            inS.Read(buf, 0, 4);
            return buf[0] | (((uint)buf[1]) << 8) |
                (((uint)buf[2]) << 16) | (((uint)buf[3]) << 24);
        }

        public static int ReadUInts(Stream inS, uint[] destinationArray, int index, int count)
        {
            byte[] buf = new byte[count * 4];
            if (null == buf)
            {
                return 0;
            }

            int bytesRead = inS.Read(buf, 0, count * 4);

            Buffer.BlockCopy(buf, 0, destinationArray, index * 4, count * 4);

            return bytesRead / 4;
        }

        public static ushort ReadUShort(Stream inS)
        {
            byte[] buf = new byte[2];
            inS.Read(buf, 0, 2);
            return (ushort)(buf[0] | (((uint)buf[1]) << 8));
        }

        public static unsafe void Write(BinaryWriter writer, void* data, int byteCount)
        {
            byte* b = (byte*)data;
            while (byteCount-- != 0)
            {
                writer.Write(*b);
                b++;
            }
        }
    }
}