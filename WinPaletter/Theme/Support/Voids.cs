using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WinPaletter.Theme
{
    public partial class Manager
    {
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
                                var temp = TreeView.Nodes.Add(Text);
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
                                var temp = TreeView.Nodes.Add(Text);
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
        public void Execute(MethodInvoker Sub, TreeView TreeView = null, string StartStr = "", string ErrorStr = "", string TimeStr = "", Stopwatch overallStopwatch = null, bool Skip = false, string SkipStr = "", bool ExecuteEvenIfSkip = false)
        {

            bool ReportProgress = TreeView is not null;
            var sw = new Stopwatch();
            sw.Reset();
            sw.Stop();
            sw.Start();

            if (!Skip | ExecuteEvenIfSkip)
            {
                if (!ExecuteEvenIfSkip)
                {
                    if (!string.IsNullOrWhiteSpace(StartStr))
                        AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), StartStr), "apply");
                }
                else if (!string.IsNullOrWhiteSpace(ErrorStr))
                    AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), SkipStr), "skip");

                try
                {
                    Sub();
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
                            AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), ErrorStr), "error");
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
                AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), SkipStr), "skip");

            sw.Stop();
        }
    }
}