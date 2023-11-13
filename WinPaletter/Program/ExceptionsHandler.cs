using System;
using System.Diagnostics;
using System.Threading;

namespace WinPaletter
{
    internal partial class Program
    {
        public static void ThreadExceptionHandler(object sender, ThreadExceptionEventArgs e)
        {
            try { Forms.BugReport.ThrowError(e.Exception, false, System.Runtime.InteropServices.Marshal.GetLastWin32Error()); }
            catch { throw e.Exception; }
        }

        private static void SecondChanceExceptionHandler(object sender, Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs e)
        {
            try
            {
                e.ExitApplication = false;
                Forms.BugReport.ThrowError(e.Exception, false, System.Runtime.InteropServices.Marshal.GetLastWin32Error());
            }
            catch { throw e.Exception; }
        }

        private static void Domain_UnhandledException(object sender, System.UnhandledExceptionEventArgs e)
        {
            try
            {
#if DEBUG
                if (!Debugger.IsAttached)
                    Forms.BugReport.ThrowError((Exception)e.ExceptionObject, true, System.Runtime.InteropServices.Marshal.GetLastWin32Error());

#else
            if (!Debugger.IsAttached)
                throw (Exception)e.ExceptionObject;
#endif
            }
            catch { throw (Exception)e.ExceptionObject; }
        }
    }
}
