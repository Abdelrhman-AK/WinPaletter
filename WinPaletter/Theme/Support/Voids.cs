using Serilog.Events;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WinPaletter.Theme
{
    public partial class Manager
    {
        /// <summary>
        /// Add exception to the list of exceptions
        /// </summary>
        /// <param name="Label"></param>
        /// <param name="Exception"></param>
        private void AddException(string Label, Exception Exception)
        {
            Program.Log?.Write(LogEventLevel.Error, $"Exception while applying theme feature `{Label}`", Exception);
            Exceptions.ThemeApply.Add(new Tuple<string, Exception>(Label, Exception));
        }

        /// <summary>
        /// Helps in executing apply methods for WinPaletter theme structures, logs the progress in a treeView (if provided), measures the time taken to execute the apply method, and handles exceptions.
        /// </summary>
        /// <param name="method">method that executes apply for a WinPaletter theme structure (feature)</param>
        /// <param name="treeView">treeView used as a theme log</param>
        /// <param name="statingStr">String used to inform user that applying feature has started</param>
        /// <param name="errorStr">String used to inform user that applying feature threw an error</param>
        /// <param name="TimeStr">String used to inform user about applying feature execution time</param>
        /// <param name="overallStopwatch">A stopwatch used to collect all milliseconds for other themes structures (To calculate whole theme applying duration)</param>
        /// <param name="skip">Skip execution</param>
        /// <param name="skipStr">String used to inform user that feature has been skipped</param>
        public void Execute(MethodInvoker method, TreeView treeView = null, string statingStr = "", string errorStr = "", string TimeStr = "", Stopwatch overallStopwatch = null, bool skip = false, string skipStr = "")
        {
            bool ReportProgress = treeView is not null;
            Stopwatch sw = new();

            Program.Log?.Write(LogEventLevel.Debug, $"Executing method: {method.Method.Name} with statingStr: {statingStr}, errorStr: {errorStr}, TimeStr: {TimeStr}, skip: {skip}, skipStr: {skipStr}");

            sw.Reset();
            sw.Stop();
            sw.Start();

            if (!skip)
            {
                if (!string.IsNullOrWhiteSpace(statingStr)) ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {statingStr}", "apply");

                try
                {
                    method();
                    if (ReportProgress & !string.IsNullOrWhiteSpace(TimeStr))
                        ThemeLog.AddNode(treeView, string.Format(TimeStr, sw.ElapsedMilliseconds / 1000d), "time");
                }
                catch (Exception ex)
                {
                    sw.Stop();
                    overallStopwatch.Stop();
                    _ErrorHappened = true;
                    if (ReportProgress)
                    {
                        if (!string.IsNullOrWhiteSpace(errorStr))
                            ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {errorStr}", "error");
                        AddException(errorStr, ex);
                    }
                    else
                    {
                        Forms.BugReport.ThrowError(ex);
                    }
                    sw.Start();
                    overallStopwatch.Start();
                }
            }
            else if (!string.IsNullOrWhiteSpace(errorStr)) ThemeLog.AddNode(treeView, $"{DateTime.Now.ToLongTimeString()}: {skipStr}", "skip");

            Program.Log?.Write(LogEventLevel.Debug, $"Method {method.Method.Name} executed in {sw.ElapsedMilliseconds} ms");

            sw.Stop();
        }
    }
}