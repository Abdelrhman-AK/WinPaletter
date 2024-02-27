using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libmsstyle
{
    public class Animation
    {
        public int SizeInBytes;
        public int PropertiesIndex;
        public int TransformsIndex;

        public AnimationFlags AnimationFlags { get; set; }
        public int TransformCount { get; set; }
        public int StaggerDelay { get; set; }
        public int StaggerDelayCap { get; set; }
        public float StaggerDelayFactor { get; set; }
        public int ZOrder { get; set; }
        public int BackgroundPartID { get; set; }
        public int TuningLevel { get; set; }
        public float Perspective { get; set; }
        private List<Transform> _transforms = new List<Transform>();
        public List<Transform> Transforms { get { return _transforms; } }
        public PropertyHeader Header;

        public Animation(byte[] data, ref int start, PropertyHeader header)
        {
            this.Header = header;
            SizeInBytes = BitConverter.ToInt32(data, start + 0); //copy of sizeInBytes of the VS record
            PropertiesIndex = BitConverter.ToInt32(data, start + 4);
            TransformsIndex = BitConverter.ToInt32(data, start + 8);
            start += 16; //skip past padding

            //Read properties structure
            AnimationFlags = (AnimationFlags)BitConverter.ToInt32(data, start + 0);
            TransformCount = BitConverter.ToInt32(data, start + 4);
            StaggerDelay = BitConverter.ToInt32(data, start + 8);
            StaggerDelayCap = BitConverter.ToInt32(data, start + 12);
            StaggerDelayFactor = BitConverter.ToSingle(data, start + 16);
            ZOrder = BitConverter.ToInt32(data, start + 20);
            BackgroundPartID = BitConverter.ToInt32(data, start + 24);
            TuningLevel = BitConverter.ToInt32(data, start + 28);
            Perspective = BitConverter.ToSingle(data, start + 32);

            start += 40; //skip past padding

            for (int i = 0; i < TransformCount; i++)
            {
                var t = new Transform(data, ref start);
                Transforms.Add(t);
                start += GetPaddingForSize(t.StructureSize);
            }
        }
        /// <summary>
        /// Create a new animation
        /// </summary>
        /// <param name="animationsHeader">Header of the animations class</param>
        /// <param name="part">Animation part</param>
        /// <param name="state">Animation state</param>
        public Animation(PropertyHeader animationsHeader, int part, int state)
        {
            PropertiesIndex = 16;
            TransformsIndex = 56;
            Header = new PropertyHeader(20000, animationsHeader.typeID);
            Header.stateID = state;
            Header.partID = part;
        }

        int GetPaddingForSize(int size)
        {
            // Makes it divisible by 8, msstyle alignment rules.
            const int sizeOfRecordHeader = 32;
            return ((size + 39) & -8) - sizeOfRecordHeader - size;
        }

        public void Write(BinaryWriter bw)
        {
            // Update the total size
            int total_size = 56;
            foreach (var item in Transforms)
            {
                total_size += item.StructureSize + GetPaddingForSize(item.StructureSize);
            }

            Header.sizeInBytes = total_size;

            bw.Write(Header.Serialize());
            WriteData(bw);
        }
        public void WriteData(BinaryWriter bw)
        {
            TransformCount = Transforms.Count;

            // Write animation header
            bw.Write(Header.sizeInBytes);
            bw.Write(16); // Property index (always seems to be the same)
            bw.Write(56);
            bw.Write(0);

            // Write animation structure
            bw.Write((int)AnimationFlags);
            bw.Write(Transforms.Count);
            bw.Write(StaggerDelay);
            bw.Write(StaggerDelayCap);
            bw.Write(StaggerDelayFactor);
            bw.Write(ZOrder);
            bw.Write(BackgroundPartID);
            bw.Write(TuningLevel);
            bw.Write(Perspective);
            bw.Write(0); //Write padding

            foreach (var item in Transforms)
            {
                item.Write(bw);

                // Write padding
                var padding = new byte[GetPaddingForSize(item.StructureSize)];
                bw.Write(padding);
            }
        }
    }
    [Flags]
    public enum AnimationFlags
    {
        None = 0,
        HasStagger = 1,
        IsRtlAware = 2,
        HasBackground = 3,
        HasPerspective = 4
    }

    public class AnimationStates
    {
        public string AnimationName;
        public Dictionary<int, string> AnimationStateDict;
        public AnimationStates(string name, Dictionary<int, string> states)
        {
            AnimationName = name;
            AnimationStateDict = states;
        }
    }
}