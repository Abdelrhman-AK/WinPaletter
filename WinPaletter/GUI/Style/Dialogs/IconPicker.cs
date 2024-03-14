using System;
using System.Text;

namespace WinPaletter.UI.Style
{
    /// <summary>
    /// Class to pick an icon from a PE file
    /// </summary>
    public static class IconPicker
    {
        private const int MAX_PATH = 0x00000104;

        /// <summary>
        /// Picks an icon from a PE file
        /// </summary>
        /// <param name="windowHandle"></param>
        /// <param name="PEfileName"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string ShowIconPicker(IntPtr windowHandle, string PEfileName, int index = 0)
        {
            int retval;
            var sb = new StringBuilder(PEfileName, MAX_PATH);
            retval = NativeMethods.Shell32.PickIconDlg(windowHandle, sb, sb.MaxCapacity, ref index);

            if (retval != 0)
            {
                if (System.IO.Path.GetExtension(sb.ToString()).ToLower() != ".ico")
                {
                    return $"{sb},{index}";
                }
                else
                {
                    return $"{sb}";
                }
            }

            // Return null if the user canceled the icon picker
            return null;
        }
    }
}
