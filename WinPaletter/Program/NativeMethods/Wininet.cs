using System.Runtime.InteropServices;

namespace WinPaletter.NativeMethods
{
    /// <summary>
    /// Provides P/Invoke declarations for Wininet (Windows Internet) functions.
    /// </summary>
    public class Wininet
    {
        /// <summary>
        /// P/Invoke declaration for the InternetGetConnectedState function.
        /// </summary>
        /// <param name="Description">A variable to receive additional information about the connection state.</param>
        /// <param name="ReservedValue">This parameter is reserved and must be 0.</param>
        /// <returns>Returns true if there is an active network connection; otherwise, false.</returns>
        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int Description, int ReservedValue);

        /// <summary>
        /// Checks whether there is an active network connection.
        /// </summary>
        /// <returns>Returns true if there is an active network connection; otherwise, false.</returns>
        public static bool CheckNet()
        {
            int desc;
            return Wininet.InternetGetConnectedState(out desc, 0);
        }
    }
}
