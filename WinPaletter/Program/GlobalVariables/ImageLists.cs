using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.GlobalVariables
{
    public static class ImageLists
    {
        /// <summary>
        /// ImageList for theme log mini-icons (Loaded at application startup)
        /// </summary>
        public static ImageList ThemeLog = new ImageList() { ImageSize = new Size(20, 20), ColorDepth = ColorDepth.Depth32Bit };

        /// <summary>
        /// ImageList for Languages Nodes (Loaded at application startup)
        /// </summary>
        public static ImageList Language = new ImageList() { ImageSize = new Size(16, 16), ColorDepth = ColorDepth.Depth32Bit };
    }
}
