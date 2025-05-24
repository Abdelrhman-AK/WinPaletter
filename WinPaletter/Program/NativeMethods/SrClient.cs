using System.Runtime.InteropServices;

namespace WinPaletter.NativeMethods
{
    /// <summary>
    /// Provides class implementation for interacting with the System Restore API.
    /// </summary>
    public class SrClient
    {
        /// <summary>
        /// Disable System Restore
        /// </summary>
        /// <param name="Drive"></param>
        /// <returns></returns>
        [DllImport("SrClient.dll")]
        public static extern int DisableSR([MarshalAs(UnmanagedType.LPWStr)] string Drive);

        /// <summary>
        /// Enable System Restore
        /// </summary>
        /// <param name="Drive"></param>
        /// <returns></returns>
        [DllImport("SrClient.dll")]
        public static extern int EnableSR([MarshalAs(UnmanagedType.LPWStr)] string Drive);
    }
}