using System.Runtime.InteropServices;

namespace WinPaletter.NativeMethods
{
    /// <summary>
    /// Provides P/Invoke declarations for functions in the dnsapi.dll library.
    /// </summary>
    public class Dnsapi
    {
        /// <summary>
        /// Flushes the resolver cache for DNS client.
        /// </summary>
        /// <returns>
        /// The function returns ERROR_SUCCESS (zero) if the cache is successfully flushed.
        /// If the function succeeds, the return value is ERROR_SUCCESS.
        /// If the function fails, the return value is a nonzero error code.
        /// </returns>
        [DllImport("dnsapi.dll", EntryPoint = "DnsFlushResolverCache")]
        public static extern uint DnsFlushResolverCache();
    }
}
