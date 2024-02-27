using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libmsstyle
{
    public class Transform
    {
        public TransformType Type { get; set; }
        public int TimingFunctionID { get; set; }
        public int StartTime { get; set; }
        public int DurationTime { get; set; }
        public TransformFlag Flags { get; set; }
        [TypeConverter(typeof(ExpandableObjectConverter))]

        public Transform3D Transform3DStructure { get; set; }
        [TypeConverter(typeof(ExpandableObjectConverter))]

        public Transform2D Transform2DStructure { get; set; }
        [TypeConverter(typeof(ExpandableObjectConverter))]

        public TransformOpacity TransformOpacity { get; set; }

        public int StructureSize
        {
            get
            {
                int header = 20; //header

                if (Type == TransformType.Roatate3D || Type == TransformType.Scale3D || Type == TransformType.Translate3d)
                {
                    header += 36;
                }
                else if (Type == TransformType.Translate2D || Type == TransformType.Scale2D)
                {
                    header += 24;
                }
                else if (Type == TransformType.Opacity)
                {
                    header += 8;
                }
                else
                {
                    throw new NotImplementedException("Unknown transform type: " + Type);
                }

                return header;
            }
        }
        public Transform()
        {
            Transform3DStructure = new Transform3D();
            Transform2DStructure = new Transform2D();
            TransformOpacity = new TransformOpacity();
        }

        public Transform(byte[] data, ref int start)
        {
            Type = (TransformType)BitConverter.ToInt32(data, start + 0);
            TimingFunctionID = BitConverter.ToInt32(data, start + 4);
            StartTime = BitConverter.ToInt32(data, start + 8);
            DurationTime = BitConverter.ToInt32(data, start + 12);
            Flags = (TransformFlag)BitConverter.ToInt32(data, start + 16);
            start += 20;
            if (Type == TransformType.Roatate3D || Type == TransformType.Scale3D || Type == TransformType.Translate3d)
            {
                Transform3DStructure = new Transform3D(data, start);
                start += 36;
            }
            else if (Type == TransformType.Translate2D || Type == TransformType.Scale2D)
            {
                Transform2DStructure = new Transform2D(data, start);
                start += 24;
            }
            else if (Type == TransformType.Opacity)
            {
                TransformOpacity = new TransformOpacity(data, start);
                start += 8;
            }
            else
            {
                throw new NotImplementedException("Unknown transform type: " + Type);
            }
        }

        public void Write(BinaryWriter bw)
        {
            bw.Write((int)Type);
            bw.Write(TimingFunctionID);
            bw.Write(StartTime);
            bw.Write(DurationTime);
            bw.Write((int)Flags);

            if (Type == TransformType.Roatate3D || Type == TransformType.Scale3D || Type == TransformType.Translate3d)
            {
                Transform3DStructure.Write(bw);
            }
            else if (Type == TransformType.Translate2D || Type == TransformType.Scale2D)
            {
                Transform2DStructure.Write(bw);
            }
            else if (Type == TransformType.Opacity)
            {
                TransformOpacity.Write(bw);
            }
            else
            {
                throw new NotImplementedException("Unknown transform type: " + Type);
            }
        }

        public override string ToString()
        {
            return "Transform";
        }
    }

    public enum TransformType
    {
        Translate2D = 0,
        Scale2D = 1,
        Opacity = 2,
        Clip = 3,
        Translate3d = 258,
        Scale3D = 259,
        Roatate3D = 260,
    }

    [Flags]
    public enum TransformFlag
    {
        None = 0,
        TargetValuesUser = 1,
        HasInitialValues = 2,
        HasOrginValues = 4
    }
}
