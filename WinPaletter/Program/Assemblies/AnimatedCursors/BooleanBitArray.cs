// Author:          Evan Olds
// Web site:        www.evanolds.com
// Creation Date:   August 10, 2009 (adapted from EOBooleanBitArray class that was created March 23, 2006)

using System;

namespace EOFC
{
    public class BooleanBitArray
    {
        private int m_bitSize;  // Size of array (in # of bits)
        private byte[] m_data;  // Allocated bit array

        /// <summary>
        /// Allocates the boolean bit array with the specified size, in bits (not bytes). All bits are 
        /// initialized to zero.
        /// </summary>
        public BooleanBitArray(int sizeInBits)
        {
            m_bitSize = sizeInBits;

            // Compute the size, in bytes
            int byteSize = m_bitSize / 8 + 1;

            // If the size is a multiple of 8, we don't need that extra byte
            if (0 == m_bitSize % 8)
            {
                byteSize = m_bitSize / 8;
            }
            m_data = new byte[byteSize];

            if (null == m_data)
            {
                throw new OutOfMemoryException(
                    $"Could not allocate {Convert.ToString(byteSize)} bytes of memory for boolean bit array");
            }
            Array.Clear(m_data, 0, m_data.Length);
        }

        public bool Get(int bitIndex)
        {
            byte BitCheck = (byte)(1 << (bitIndex % 8));
            return ((m_data[bitIndex / 8] & BitCheck) != 0);
        }

        public static unsafe bool Get(byte* data, int BitIndex)
        {
            byte BitCheck = (byte)(1 << (BitIndex % 8));
            if ((data[BitIndex / 8] & BitCheck) == 0)
            {
                return false;
            }
            return true;
        }

        public static bool Get(byte[] data, int bitIndex)
        {
            byte bitCheck = (byte)(1 << (bitIndex % 8));
            return ((data[bitIndex / 8] & bitCheck) != 0);
        }

        /// <summary>
        /// Sets the specified bit to the given value. Less signifcant bits within bytes 
        /// come first in this function. So, in an individual byte, a bit index of 0 refers 
        /// to the least significant bit, the bit that would be rightmost when the binary
        /// number is written out.
        /// </summary>
        public static unsafe void Set(byte* data, int BitIndex, bool value)
        {
            byte BitSet;

            if (value)
            {
                BitSet = (byte)(1 << (BitIndex % 8));
                data[BitIndex / 8] |= BitSet;
            }
            else
            {
                BitSet = (byte)(~(1 << (BitIndex % 8)));
                data[BitIndex / 8] &= BitSet;
            }
        }

        /// <summary>
        /// Sets the specified bit to the given value. Less signifcant bits within bytes 
        /// come first in this function. So, in an individual byte, a bit index of 0 refers 
        /// to the least significant bit, the bit that would be rightmost when the binary
        /// number is written out.
        /// </summary>
        public static void Set(byte[] data, int BitIndex, bool value)
        {
            byte BitSet;

            if (value)
            {
                BitSet = (byte)(1 << (BitIndex % 8));
                data[BitIndex / 8] |= BitSet;
            }
            else
            {
                BitSet = (byte)(~(1 << (BitIndex % 8)));
                data[BitIndex / 8] &= BitSet;
            }
        }

        public static void SetMSbFirst(byte[] data, int BitIndex, bool value)
        {
            byte BitSet;

            if (value)
            {
                BitSet = (byte)(1 << (7 - (BitIndex % 8)));
                data[BitIndex / 8] |= BitSet;
            }
            else
            {
                BitSet = (byte)(~(1 << (7 - (BitIndex % 8))));
                data[BitIndex / 8] &= BitSet;
            }
        }

        public bool this[int index]
        {
            get
            {
                return Get(index);
            }
            set
            {
                BooleanBitArray.Set(m_data, index, value);
            }
        }
    }
}

