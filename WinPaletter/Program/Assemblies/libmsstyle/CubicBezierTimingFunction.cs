using System;
using System.IO;

namespace libmsstyle
{
    public class CubicBezierTimingFunction
    {
        public float RX0 { get; set; }
        public float RY0 { get; set; }

        public float RX1 { get; set; }
        public float RY1 { get; set; }

        public CubicBezierTimingFunction(byte[] data, int start)
        {
            RX0 = BitConverter.ToSingle(data, start + 0);
            RY0 = BitConverter.ToSingle(data, start + 4);
            RX1 = BitConverter.ToSingle(data, start + 8);
            RY1 = BitConverter.ToSingle(data, start + 12);
        }

        public CubicBezierTimingFunction()
        {

        }

        internal void Write(BinaryWriter w)
        {
            w.Write(RX0);
            w.Write(RY0);
            w.Write(RX1);
            w.Write(RY1);
        }
        public override string ToString()
        {
            return "Cubic Bezier function";
        }
    }
}
