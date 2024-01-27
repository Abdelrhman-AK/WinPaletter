using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace WinPaletter
{
    internal partial class Program
    {
        public static void ThreadExceptionHandler(object sender, ThreadExceptionEventArgs e)
        {
            if (!Debugger.IsAttached)
                Forms.BugReport.ThrowError(e.Exception, true, System.Runtime.InteropServices.Marshal.GetLastWin32Error());
            else
                ExceptionDispatchInfo.Capture(e.Exception).Throw();
        }

        private static void Domain_UnhandledException(object sender, System.UnhandledExceptionEventArgs e)
        {
#if DEBUG
            if (!Debugger.IsAttached)
                Forms.BugReport.ThrowError(e.ExceptionObject as Exception, true, System.Runtime.InteropServices.Marshal.GetLastWin32Error());
            else
                ExceptionDispatchInfo.Capture(e.ExceptionObject as Exception).Throw();
#else
            if (!Debugger.IsAttached)
                Forms.BugReport.ThrowError(e.ExceptionObject as Exception, true, System.Runtime.InteropServices.Marshal.GetLastWin32Error());
            else
                ExceptionDispatchInfo.Capture(e.ExceptionObject as Exception).Throw();
#endif
        }

    }
}