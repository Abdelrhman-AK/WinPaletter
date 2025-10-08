using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinPaletter.TypesExtensions
{
    public static class StreamsExtensions
    {
        /// <summary>
        /// Compares two streams for equality by reading their contents and checking for byte-by-byte equivalence.
        /// </summary>
        /// <remarks>Both streams must support seeking, as their positions will be reset to the beginning
        /// during the comparison. The method reads the streams in chunks, using the provided buffer size, and compares
        /// the data byte by byte.</remarks>
        /// <param name="s1">The first <see cref="Stream"/> to compare. Must be readable and seekable.</param>
        /// <param name="s2">The second <see cref="Stream"/> to compare. Must be readable and seekable.</param>
        /// <param name="buffer">A buffer used for reading chunks of data from the streams. The size of the buffer determines the chunk size
        /// for comparison.</param>
        /// <returns><see langword="true"/> if the contents of both streams are identical; otherwise, <see langword="false"/>.</returns>
        public static bool Equals(this Stream s1, Stream s2, byte[] buffer)
        {
            byte[] buffer2 = new byte[buffer.Length];

            // Reset positions only if supported
            if (s1.CanSeek) s1.Position = 0;
            if (s2.CanSeek) s2.Position = 0;

            int read1, read2;
            while (true)
            {
                read1 = s1.Read(buffer, 0, buffer.Length);
                read2 = s2.Read(buffer2, 0, buffer2.Length);

                if (read1 != read2) return false;
                if (read1 == 0) break; // End of both streams

                for (int i = 0; i < read1; i++)
                {
                    if (buffer[i] != buffer2[i]) return false;
                }
            }

            return true;
        }
    }
}
