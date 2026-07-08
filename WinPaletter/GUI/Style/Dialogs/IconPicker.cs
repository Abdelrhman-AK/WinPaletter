using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using WinPaletter.NativeMethods;

namespace WinPaletter.UI.Style
{
    public partial class Dialogs
    {
        private const int MAX_PATH = 260;
    
        public static string PickIcon(IntPtr windowHandle, string PEfileName, int index = 0)
        {
            using (Dark.DarkWin32 darkWin32 = new())
            {
                StringBuilder sb = new(Environment.ExpandEnvironmentVariables(PEfileName), MAX_PATH);

                if (Shell32.PickIconDlg(windowHandle, sb, sb.MaxCapacity, ref index) != 0)
                {
                    return Path.GetExtension(sb.ToString()).ToLower() != ".ico" ? $"{sb},{index}" : sb.ToString();
                }

                return null;
            }
        }
    }
}