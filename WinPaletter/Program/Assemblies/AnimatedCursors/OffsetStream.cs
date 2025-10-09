using System.IO;

namespace EOFC
{
    internal sealed class OffsetStream(Stream underlyingStream) : Stream
    {
        private readonly Stream m_base = underlyingStream;

        /// <summary>
        /// Offset in bytes, counting from the beginning of the stream
        /// </summary>
        private readonly long m_pos = underlyingStream.Position;

        public override bool CanRead => m_base.CanRead;

        public override bool CanSeek => m_base.CanSeek;

        public override bool CanWrite => m_base.CanWrite;

        public override void Flush()
        {
            m_base.Flush();
        }

        public override long Length => m_base.Length - m_pos;

        public override long Position
        {
            get => m_base.Position - m_pos;
            set
            {
                // Documentation is sketchy for setting a position to a value less than zero, but 
                // I'm assuming that the desired behavior in this case is just to clamp to zero.
                if (value < 0)
                {
                    m_base.Position = 0;
                }
                else
                {
                    m_base.Position = m_pos + value;
                }
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return m_base.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return m_base.Seek(offset + m_pos, origin);
        }

        public override void SetLength(long value)
        {
            m_base.SetLength(value + m_pos);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            m_base.Write(buffer, offset, count);
        }
    }
}
