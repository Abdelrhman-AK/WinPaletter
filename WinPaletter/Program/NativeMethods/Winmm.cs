using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WinPaletter.NativeMethods
{
    /// <summary>
    /// Provides P/Invoke declarations for functions in the winmm.dll library.
    /// </summary>
    public class Winmm
    {
        /// <summary>
        /// Sends a command string to the MCI device specified in the command.
        /// </summary>
        /// <param name="command">The command string to be sent to the MCI device.</param>
        /// <param name="buffer">A Buffer that receives return information.</param>
        /// <param name="bufferSize">The size, in characters, of the Buffer.</param>
        /// <param name="hwndCallback">A handle to the callback window if the "notify" flag is specified in the command.</param>
        /// <returns>Returns zero if successful; otherwise, an error code.</returns>
        [DllImport("winmm.dll")]
        public static extern int mciSendString(string command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);
    }
}
