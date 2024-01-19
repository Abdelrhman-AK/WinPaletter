using System;

namespace WinPaletter
{
    internal partial class Program
    {
        public static object Invoke(Delegate method)
        {
            return Forms.MainFrm.Invoke(method, null);
        }
    }
}