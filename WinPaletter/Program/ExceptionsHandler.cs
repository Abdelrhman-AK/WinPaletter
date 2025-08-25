using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace WinPaletter
{
    internal partial class Program
    {
        /// <summary>
        /// Handles the exception thrown by the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void ThreadExceptionHandler(object sender, ThreadExceptionEventArgs e)
        {
            if (!Debugger.IsAttached)
                Forms.BugReport.ThrowError(e.Exception, true, Marshal.GetLastWin32Error());
            else
                ExceptionDispatchInfo.Capture(e.Exception).Throw();
        }

        /// <summary>
        /// Handles the exception thrown by the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Domain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
#if DEBUG
            if (!Debugger.IsAttached)
                Forms.BugReport.ThrowError(e.ExceptionObject as Exception, true, Marshal.GetLastWin32Error());
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