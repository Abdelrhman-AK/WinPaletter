using System.Drawing;

namespace WinPaletter
{
    /// <summary>
    /// Bitmap Manager (Helper for loading bitmaps quickly)
    /// </summary>
    public class Bitmap_Mgr
    {
        /// <summary>
        /// Load a bitmap from a file quickly by opening a stream and loading the image from it, to avoid file locks.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static Bitmap Load(string file)
        {
            if (System.IO.File.Exists(file))
            {
                try
                {
                    using (System.IO.Stream bmpStream = System.IO.File.Open(file, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    using (Image image = Image.FromStream(bmpStream))
                    {
                        Program.Log?.Debug("Loaded image from stream: {file}", file);
                        return new Bitmap(image);
                    }
                }
                catch
                {
                    // If the above fails, try loading the image directly from the file but this might cause file locks if image is not disposed properly.
                    try
                    {
                        using (Image image = Image.FromFile(file))
                        {
                            Program.Log?.Debug("Loaded image from file: {file}", file);
                            return new Bitmap(image);
                        }
                    }
                    catch { return null; }
                }
            }
            else { return null; }
        }
    }
}