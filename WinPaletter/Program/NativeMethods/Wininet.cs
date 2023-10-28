using System.Runtime.InteropServices;

namespace WinPaletter.NativeMethods
{
    public class Wininet
    {
        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int Description, int ReservedValue);

        public static bool CheckNet()
        {
            int desc;
            return Wininet.InternetGetConnectedState(out desc, 0);
        }
    }
}
