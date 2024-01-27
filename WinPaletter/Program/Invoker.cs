using System;

namespace WinPaletter
{
    internal partial class Program
    {
        public static object Invoke(Delegate method)
        {
            return Forms.MainForm.Invoke(method, null);
        }
    }
}