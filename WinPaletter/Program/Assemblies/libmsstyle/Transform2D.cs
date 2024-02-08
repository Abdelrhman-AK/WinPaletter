using System;
using System.IO;

namespace libmsstyle
{
    public class Transform2D
    {
        public float rX { get; set; }
        public float rY { get; set; }
        public float rInitialX { get; set; }
        public float rInitialY { get; set; }
        public float rOriginX { get; set; }
        public float rOriginY { get; set; }

        public Transform2D(byte[] data, int start)
        {
            rX = BitConverter.ToSingle(data, start + 0);
            rY = BitConverter.ToSingle(data, start + 4);
            rInitialX = BitConverter.ToSingle(data, start + 8);
            rInitialY = BitConverter.ToSingle(data, start + 12);
            rOriginX = BitConverter.ToSingle(data, start + 16);
            rOriginY = BitConverter.ToSingle(data, start + 20);
        }

        public Transform2D()
        {

        }

        public void Write(BinaryWriter bw)
        {
            bw.Write(rX);
            bw.Write(rY);
            bw.Write(rInitialX);
            bw.Write(rInitialY);
            bw.Write(rOriginX);
            bw.Write(rOriginY);
        }
        public override string ToString()
        {
            return "2D Transform";
        }
    }
}
