using System;
using System.IO;

namespace libmsstyle
{
    public class TransformOpacity
    {
        public float Opacity { get; set; }
        public float InitialOpacity { get; set; }

        public TransformOpacity(byte[] data, int start)
        {
            Opacity = BitConverter.ToSingle(data, start + 0);
            InitialOpacity = BitConverter.ToSingle(data, start + 4);
        }

        public TransformOpacity()
        {

        }

        public void Write(BinaryWriter wr)
        {
            wr.Write(Opacity);
            wr.Write(InitialOpacity);
        }
        public override string ToString()
        {
            return "Opacity Transform";
        }
    }
}
