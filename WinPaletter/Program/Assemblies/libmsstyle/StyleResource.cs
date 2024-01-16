using System;
using System.Linq;

namespace libmsstyle
{
    public enum StyleResourceType
    {
        None,
        Image,
        Atlas
    }

    public class StyleResource
    {
        public byte[] Data { get; }
        public int ResourceId { get; }
        public StyleResourceType Type { get; }

        public StyleResource(byte[] data, int resId, StyleResourceType type)
        {
            Data = data;
            ResourceId = resId;
            Type = type;
        }

        public override bool Equals(object obj)
        {
            if (obj is StyleResource other)
            {
                return ResourceId == other.ResourceId &&
                    Type == other.Type &&
                    object.ReferenceEquals(Data, other.Data);
            }
            else return false;
        }

        public override int GetHashCode()
        {
            int hash = 1337;
            hash = (hash * 4) ^ ResourceId;
            hash = (hash * 4) ^ (int)Type;
            hash = (hash * 4) ^ (Data != null ? Data.Length : 0);
            return hash;
        }
    }
}
