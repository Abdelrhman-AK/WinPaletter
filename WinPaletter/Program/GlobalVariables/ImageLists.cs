﻿using System.Windows.Forms;

namespace WinPaletter.GlobalVariables
{
    /// <summary>
    /// ImageLists for the application (loaded at application startup) and used for the TreeView and ListView for logs.
    /// </summary>
    public static class ImageLists
    {
        /// <summary>
        /// ImageList for theme log mini-icons (Loaded at application startup)
        /// </summary>
        public static ImageList ThemeLog = new() { ImageSize = new(20, 20), ColorDepth = ColorDepth.Depth32Bit };

        /// <summary>
        /// ImageList for Languages Nodes (Loaded at application startup)
        /// </summary>
        public static ImageList Language = new() { ImageSize = new(16, 16), ColorDepth = ColorDepth.Depth32Bit };
    }
}
