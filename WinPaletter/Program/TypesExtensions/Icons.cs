using System.Drawing;
using System.IO;

namespace WinPaletter.TypesExtensions
{
    /// <summary>
    /// Extension methods for <see cref="Icon"/> class.
    /// </summary>
    public static class IconsExtensions
    {
        /// <summary>
        /// Convert <see cref="Icon"/> to <see cref="byte"/> array.
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this Icon icon)
        {
            using (MemoryStream ms = new())
            {
                icon.Save(ms);
                byte[] b = ms.ToArray();
                ms.Close();
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Convert <see cref="byte"/> array to <see cref="Icon"/>.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Icon ToIcon(this byte[] bytes)
        {
            using (MemoryStream ms = new(bytes))
            {
                return new Icon(ms);
            }
        }

    }

}
