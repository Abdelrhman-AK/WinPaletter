using System;

namespace WinPaletter.Theme
{
    public partial class Manager : ICloneable
    {
        /// <summary>
        /// Clone WinPaletter theme
        /// </summary>
        /// <returns><see cref="Manager"/></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}