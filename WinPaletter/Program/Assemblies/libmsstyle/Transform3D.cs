using System;
using System.IO;
using System.Linq;

namespace libmsstyle
{
    public class Transform3D
    {
        public float rX { get; set; }
        public float rY { get; set; }
        public float rZ { get; set; }
        public float rInitialX { get; set; }
        public float rInitialY { get; set; }
        public float rInitialZ { get; set; }
        public float rOriginX { get; set; }
        public float rOriginY { get; set; }
        public float rOriginZ { get; set; }
        public Transform3D()
        {

        }

        public Transform3D(byte[] data, int start)
        {
            rX = BitConverter.ToSingle(data, start + 0);
            rY = BitConverter.ToSingle(data, start + 4);
            rZ = BitConverter.ToSingle(data, start + 8);
            rInitialX = BitConverter.ToSingle(data, start + 12);
            rInitialY = BitConverter.ToSingle(data, start + 16);
            rInitialZ = BitConverter.ToSingle(data, start + 20);
            rOriginX = BitConverter.ToSingle(data, start + 24);
            rOriginY = BitConverter.ToSingle(data, start + 28);
            rOriginZ = BitConverter.ToSingle(data, start + 32);
        }

        public void Write(BinaryWriter bw)
        {
            bw.Write(rX);
            bw.Write(rY);
            bw.Write(rZ);
            bw.Write(rInitialX);
            bw.Write(rInitialY);
            bw.Write(rInitialZ);
            bw.Write(rOriginX);
            bw.Write(rOriginY);
            bw.Write(rOriginZ);
        }
        public override string ToString()
        {
            return "3D Transform";
        }
    }
}
