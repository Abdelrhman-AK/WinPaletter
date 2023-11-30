using System.Drawing;
using System.IO;

namespace WinPaletter.TypesExtensions
{
    public static class IconsExtensions
    {

        public static byte[] ToByteArray(this Icon icon)
        {
            using (MemoryStream ms = new())
            {
                icon.Save(ms);
                byte[] b = ms.ToArray();
                ms.Close();
                return ms.ToArray();
            }
        }

        public static Icon ToIcon(this byte[] bytes)
        {
            using (MemoryStream ms = new(bytes))
            {
                return new Icon(ms);
            }
        }

    }

}
