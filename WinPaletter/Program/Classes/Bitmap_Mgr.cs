using System.Drawing;

namespace WinPaletter
{
    public class Bitmap_Mgr
    {
        public static Bitmap Load(string file)
        {
            if (System.IO.File.Exists(file))
            {
                try
                {
                    using (System.IO.Stream bmpStream = System.IO.File.Open(file, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    using (Image image = Image.FromStream(bmpStream))
                    {
                        return new Bitmap(image);
                    }
                }
                catch
                {
                    try
                    {
                        using (Image image = Image.FromFile(file)) { return new Bitmap(image); }
                    }
                    catch { return null; }
                }
            }
            else { return null; }
        }
    }
}