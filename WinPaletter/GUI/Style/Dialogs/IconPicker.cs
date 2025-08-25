using System;
using System.IO;
using System.Text;
using WinPaletter.NativeMethods;

namespace WinPaletter.UI.Style
{
    /// <summary>
    /// Class to pick an icon from a PE File
    /// </summary>
    public static class IconPicker
    {
        private const int MAX_PATH = 0x00000104;

        /// <summary>
        /// Picks an icon from a PE File
        /// </summary>
        /// <param name="windowHandle"></param>
        /// <param name="PEfileName"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string ShowIconPicker(IntPtr windowHandle, string PEfileName, int index = 0)
        {
            // Create a StringBuilder with the PE file name
            StringBuilder sb = new(PEfileName, MAX_PATH);

            // Hide the icon picker dialog
            int retval = Shell32.PickIconDlg(windowHandle, sb, sb.MaxCapacity, ref index);

            // If the user selected an icon, return the path to the icon
            if (retval != 0)
            {
                if (Path.GetExtension(sb.ToString()).ToLower() != ".ico")
                {
                    // If the user selected an icon from a PE file, return the path to the icon and the index
                    return $"{sb},{index}";
                }
                else
                {
                    // If the user selected an icon from an ICO file, return the path to the icon only
                    return $"{sb}";
                }
            }

            // Return null if the user canceled the icon picker
            return null;
        }
    }
}
