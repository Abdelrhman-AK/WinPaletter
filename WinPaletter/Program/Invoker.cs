using System;

namespace WinPaletter
{
    internal partial class Program
    {
        /// <summary>
        /// Invokes a method on the main form thread.
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static object Invoke(Delegate method)
        {
            return Forms.MainForm.Invoke(method, null);
        }
    }
}