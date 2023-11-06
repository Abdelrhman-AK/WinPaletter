using System.Drawing;
using System.IO;

namespace WinPaletter.TypesExtensions
{
    public static class IconsExtensions
    {

        public static byte[] ToByteArray(this Icon icon)
        {
            using (var ms = new MemoryStream())
            {
                icon.Save(ms);
                byte[] b = ms.ToArray();
                ms.Close();
                return ms.ToArray();
            }
        }

        public static Icon ToIcon(this byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            {
                return new Icon(ms);
            }
        }

    }

}
