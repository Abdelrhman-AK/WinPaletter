using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace libmsstyle
{
    //[Serializable]
    public class TimingFunction
    {
        public TimingFunctionType Type { get; set; }
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public CubicBezierTimingFunction CubicBezier { get; set; }

        public PropertyHeader Header;

        public TimingFunction(byte[] data, int start, PropertyHeader header)
        {
            this.Header = header;
            Type = (TimingFunctionType)BitConverter.ToInt32(data, start + 0);
            start += 4;
            if (Type == TimingFunctionType.CubicBezier)
            {
                //Cubic Bezier function
                CubicBezier = new CubicBezierTimingFunction(data, start);
            }
            else
            {
                throw new Exception("Unknown timing function type: " + Type);
            }
        }

        public void Write(BinaryWriter bw)
        {
            if (Type == TimingFunctionType.Undefined)
                Header.sizeInBytes = 4;
            else if (Type == TimingFunctionType.CubicBezier)
                Header.sizeInBytes = 4 + 16;

            bw.Write(Header.Serialize());
            WriteData(bw);
        }

        public void WriteData(BinaryWriter w)
        {
            w.Write((int)Type);
            if (Type == TimingFunctionType.CubicBezier)
            {
                CubicBezier.Write(w);
            }
            w.Write(0); //Write padding
        }

        public override string ToString()
        {
            return "Timing function";
        }
    }
    public enum TimingFunctionType : uint
    {
        Undefined = 0,
        CubicBezier = 1,
    }
}
