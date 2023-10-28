using System.Runtime.InteropServices;

namespace WinPaletter.NativeMethods
{
    public class Dnsapi
    {
        [DllImport("dnsapi.dll", EntryPoint = "DnsFlushResolverCache")]
        public static extern uint DnsFlushResolverCache();
    }
}
