using System;

namespace WinPaletter.Theme
{
    public partial class Manager : ICloneable
    {
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}