using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WinPaletter.Theme
{
    public partial class Manager
    {
        /// <summary>
        /// Add node to TreeView (Theme log)
        /// </summary>
        /// <param name="TreeView">TreeView used as a theme log</param>
        /// <param name="Text">Log node text</param>
        /// <param name="ImageKey">ImageKey used for icon for log node</param>
        public static void AddNode(TreeView TreeView, string Text, string ImageKey)
        {
            if (TreeView is not null)
            {
                if (TreeView.InvokeRequired)
                {

                    try
                    {
                        TreeView.Invoke(new MethodInvoker(() =>
                        {
                            {
                                TreeNode temp = TreeView.Nodes.Add(Text);
                                temp.ImageKey = ImageKey;
                                temp.SelectedImageKey = ImageKey;
                            }
                            TreeView.SelectedNode = TreeView.Nodes[TreeView.Nodes.Count - 1];
                            //TreeView.Update();
                        }));
                    }
                    catch
                    {
                    }
                }

                else
                {

                    try
                    {
                        TreeView.Invoke(new MethodInvoker(() =>
                        {
                            {
                                TreeNode temp = TreeView.Nodes.Add(Text);
                                temp.ImageKey = ImageKey;
                                temp.SelectedImageKey = ImageKey;
                            }
                            TreeView.SelectedNode = TreeView.Nodes[TreeView.Nodes.Count - 1];
                            //TreeView.Update();
                        }));
                    }
                    catch
                    {
                    }
                }

            }
        }
        private void AddException(string Label, Exception Exception)
        {
            Exceptions.ThemeApply.Add(new Tuple<string, Exception>(Label, Exception));
        }

        /// <summary>
        /// Helps in executing apply Methods for WinPaletter theme structures, and counts execution time
        /// </summary>
        /// <param name="Void">Void that executes apply for a WinPaletter theme structure (feature)</param>
        /// <param name="TreeView">TreeView used as a theme log</param>
        /// <param name="StartStr">String used to inform user that applying feature has started</param>
        /// <param name="ErrorStr">String used to inform user that applying feature threw an error</param>
        /// <param name="TimeStr">String used to inform user about applying feature execution time</param>
        /// <param name="overallStopwatch">A stopwatch used to collect all milliseconds for other themes structures (To calculate whole theme applying duration)</param>
        /// <param name="Skip">Skip execution</param>
        /// <param name="SkipStr">String used to inform user that feature has been skipped</param>
        /// <param name="ExecuteEvenIfSkip">Execute even if skipped or structure is disabled</param>
        public void Execute(MethodInvoker Void, TreeView TreeView = null, string StartStr = "", string ErrorStr = "", string TimeStr = "", Stopwatch overallStopwatch = null, bool Skip = false, string SkipStr = "", bool ExecuteEvenIfSkip = false)
        {

            bool ReportProgress = TreeView is not null;
            Stopwatch sw = new();

            sw.Reset();
            sw.Stop();
            sw.Start();

            if (!Skip | ExecuteEvenIfSkip)
            {
                if (!ExecuteEvenIfSkip)
                {
                    if (!string.IsNullOrWhiteSpace(StartStr))
                        AddNode(TreeView, $"{DateTime.Now.ToLongTimeString()}: {StartStr}", "apply");
                }
                else if (!string.IsNullOrWhiteSpace(ErrorStr))
                    AddNode(TreeView, $"{DateTime.Now.ToLongTimeString()}: {SkipStr}", "skip");

                try
                {
                    Void();
                    if (ReportProgress & !string.IsNullOrWhiteSpace(TimeStr))
                        AddNode(TreeView, string.Format(TimeStr, sw.ElapsedMilliseconds / 1000d), "time");
                }
                catch (Exception ex)
                {
                    sw.Stop();
                    overallStopwatch.Stop();
                    _ErrorHappened = true;
                    if (ReportProgress)
                    {
                        if (!string.IsNullOrWhiteSpace(ErrorStr))
                            AddNode(TreeView, $"{DateTime.Now.ToLongTimeString()}: {ErrorStr}", "error");
                        AddException(ErrorStr, ex);
                    }
                    else
                    {
                        Forms.BugReport.ThrowError(ex);
                    }
                    sw.Start();
                    overallStopwatch.Start();
                }
            }
            else if (!string.IsNullOrWhiteSpace(ErrorStr))
                AddNode(TreeView, $"{DateTime.Now.ToLongTimeString()}: {SkipStr}", "skip");

            sw.Stop();
        }
    }
}