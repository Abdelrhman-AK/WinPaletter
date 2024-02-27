using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
