using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class ThemeLog : Form
    {
        private int elapsedSecs = 0;
        private Thread Apply_Thread;

        public ThemeLog()
        {
            InitializeComponent();
            FormClosing += ThemeLog_Closing;
        }

        /// <summary>
        /// Add node to treeView (WinTheme log)
        /// </summary>
        /// <param name="treeView">treeView used as a theme log</param>
        /// <param name="Text">Log node text</param>
        /// <param name="ImageKey">ImageKey used for icon for log node</param>
        public static void AddNode(TreeView treeView, string Text, string ImageKey)
        {
            if (treeView is not null && treeView.IsHandleCreated)
            {
                treeView?.Invoke(() =>
                {
                    TreeNode temp = treeView?.Nodes.Add(Text);
                    if (temp is not null)
                    {
                        temp.ImageKey = ImageKey;
                        temp.SelectedImageKey = ImageKey;
                    }

                    treeView.SelectedNode = treeView.Nodes[treeView.Nodes.Count - 1];
                });

                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"An item is added to themes log: `{Text}` with image key `{ImageKey}`");
            }
        }

        private async void ThemeLog_Load(object sender, EventArgs e)
        {
            // Task.Run is used as there is a bug of ex error: Icon is disposed while accessing it from different thread
            await Task.Run(() =>
            {
                // Ensure you are on the UI thread if accessing UI elements
                Invoke(() =>
                {
                    try
                    {
                        Icon = FormsExtensions.Icon<MainForm>();
                    }
                    catch { } // Ignore this exception when form or icon is disposed
                });
            });

            if (this is not null)
            {
                this.LoadLanguage();
                ApplyStyle(this);
                CheckForIllegalCrossThreadCalls = false;
                if (TreeView1 is not null) TreeView1.ImageList = ImageLists.ThemeLog;
            }
        }

        private void ThemeLog_Closing(object sender, FormClosingEventArgs e)
        {
            if (Apply_Thread.IsAlive)
            {
                if (MsgBox(Program.Lang.Strings.ThemeManager.Actions.CloseOnApplying0, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, Program.Lang.Strings.ThemeManager.Actions.CloseOnApplying1) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
                else
                {
                    Apply_Thread?.Abort();
                }
            }

            if (Program.ProgramsRunning(SysPaths.Explorer).Count == 0)
            {
                Task.Run(() => { Program.Explorer_exe?.Start(); });
            }
        }

        /// <summary>
        /// This will apply WinPaletter theme with showing theme log
        /// </summary>
        /// <param name="TM">WinPaletter theme manager</param>
        /// <param name="AdditionalStoreTips">If true, it will show additional tips for store apps</param>
        /// <param name="dontInvoke">If true, it will not invoke the controls</param>
        public void Apply_Theme(Theme.Manager TM = null, bool AdditionalStoreTips = false, bool dontInvoke = false)
        {
            TM ??= Program.TM;

            if (Program.Settings.ThemeLog.VerboseLevel != Settings.Structures.ThemeLog.VerboseLevels.None)
            {
                //Hide();
                Forms.MainForm?.tabsContainer1?.AddFormIntoTab(this);
            }

            Apply_Thread = new(() =>
            {
                bool LogEnabled = Program.Settings.ThemeLog.VerboseLevel != Settings.Structures.ThemeLog.VerboseLevels.None;

                animatedBox1.Color1 = TM.Info.Color1;
                animatedBox1.Color2 = TM.Info.Color2;

                TreeView1?.Nodes?.Clear();

                log_lbl?.SetText(string.Format(Program.Lang.Strings.ThemeManager.Actions.ApplyingTheme, TM.Info.ThemeName));

                Cursor = Cursors.WaitCursor;

                if (!dontInvoke)
                {
                    //Invoking is important to access controls from different thread
                    BeginInvoke(() =>
                    {
                        Button8.Visible = false;
                        Button14.Visible = false;
                        Button22.Visible = false;
                        Button25.Visible = false;
                    });
                }
                else
                {
                    Button8.Visible = false;
                    Button14.Visible = false;
                    Button22.Visible = false;
                    Button25.Visible = false;
                }


                // New method of restarting Explorer
                if (Program.Settings.ThemeApplyingBehavior.AutoRestartExplorer)
                {
                    if (User.SID == User.UserSID_OpenedWP && User.SID == User.AdminSID_GrantedUAC)
                    {
                        Program.ExplorerKiller?.Start();
                        Program.ExplorerKiller?.WaitForExit();
                    }
                }

                try
                {
                    TM?.Save(Theme.Manager.Source.Registry, string.Empty, LogEnabled ? TreeView1 : null);

                    if (LogEnabled)
                        AddNode(TreeView1, $"{DateTime.Now.ToLongTimeString()}: {Program.Lang.Strings.ThemeManager.Actions.Complete}", "info");

                    if (AdditionalStoreTips)
                        AddNode(TreeView1, Program.Lang.Strings.Store.LogoffRecommended, "info");
                }
                catch (Exception ex)
                {
                    AddNode(TreeView1, Program.Lang.Strings.ThemeManager.Errors.FatalError, "error");
                    Exceptions.ThemeApply.Add(new Tuple<string, Exception>(ex.Message, ex));
                }

                Program.TM = TM.Clone() as Theme.Manager;
                Program.TM_Original = TM.Clone() as Theme.Manager;

                Cursor = Cursors.Default;

                //Invoking is important to access controls from different thread
                if (!dontInvoke)
                {
                    BeginInvoke(() =>
                    {
                        log_lbl.Visible = true;
                        Button8.Visible = true;
                        Button22.Visible = true;
                        Button25.Visible = Exceptions.ThemeApply.Count == 0 && Program.Settings.ThemeLog.VerboseLevel != Settings.Structures.ThemeLog.VerboseLevels.Detailed;
                    });
                }
                else
                {
                    log_lbl.Visible = true;
                    Button8.Visible = true;
                    Button22.Visible = true;
                    Button25.Visible = Exceptions.ThemeApply.Count == 0 && Program.Settings.ThemeLog.VerboseLevel != Settings.Structures.ThemeLog.VerboseLevels.Detailed;
                }

                if (Exceptions.ThemeApply.Count != 0)
                {
                    log_lbl.SetText(Program.Lang.Strings.ThemeManager.Errors.ErrorHappened);

                    if (!dontInvoke)
                    {
                        //Invoking is important to access controls from different thread
                        BeginInvoke(() =>
                    {
                        Button14.Visible = true;
                    });
                    }
                    else
                    {
                        Button14.Visible = true;
                    }
                }
                else if (dontInvoke || (Program.Settings.ThemeLog.CountDown && Program.Settings.ThemeLog.VerboseLevel != Settings.Structures.ThemeLog.VerboseLevels.Detailed))
                {
                    log_lbl?.SetText(string.Format(Program.Lang.Strings.ThemeManager.Actions.LogClosure, Program.Settings.ThemeLog.CountDown_Seconds));
                    elapsedSecs = 1;

                    if (!dontInvoke)
                    {
                        //Invoking is important to access timer from different thread
                        BeginInvoke(() =>
                    {
                        timer1.Enabled = true;
                        timer1?.Start();
                    });
                    }
                    else
                    {
                        timer1.Enabled = true;
                        timer1?.Start();
                    }
                }
                else
                {
                    log_lbl?.SetText(Program.Lang.Strings.ThemeManager.Actions.LogTimerFinished);
                }

                //New method of restarting Explorer
                if (Program.Settings.ThemeApplyingBehavior.AutoRestartExplorer)
                {
                    if (User.SID == User.UserSID_OpenedWP && User.SID == User.AdminSID_GrantedUAC)
                    {
                        Program.Explorer_exe?.Start();
                    }
                    else
                    {
                        AddNode(TreeView1, $"{Program.Lang.Strings.Messages.RestartExplorerIssue0}. {Program.Lang.Strings.Messages.RestartExplorerIssue1}", "warning");
                    }
                }

                else if (LogEnabled) { AddNode(TreeView1, Program.Lang.Strings.ThemeManager.Tips.NoDefResExplorer, "warning"); }
            });

            Apply_Thread?.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            log_lbl.Text = string.Format(Program.Lang.Strings.ThemeManager.Actions.LogClosure, Program.Settings.ThemeLog.CountDown_Seconds - elapsedSecs);

            if (elapsedSecs + 1 <= Program.Settings.ThemeLog.CountDown_Seconds)
            {
                elapsedSecs += 1;
            }
            else
            {
                ((System.Windows.Forms.Timer)sender).Enabled = false;
                ((System.Windows.Forms.Timer)sender).Stop();
                this?.Close();
            }
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            log_lbl.Text = Program.Lang.Strings.ThemeManager.Actions.LogTimerFinished;
            timer1.Enabled = false;
            timer1?.Stop();

            using (SaveFileDialog dlg = new() { Filter = Program.Filters.Text, Title = Program.Lang.Strings.Extensions.SaveText })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder sb = new();
                    sb.Clear();

                    foreach (TreeNode N in TreeView1.Nodes)
                        sb.AppendLine($"[{N.ImageKey}]{"\t"} {N.Text}{"\r\n"}");

                    System.IO.File.WriteAllText(dlg.FileName, sb.ToString());
                }
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            log_lbl.Text = string.Empty;
            timer1.Enabled = false;
            timer1.Stop();
            Close();
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            log_lbl.Text = Program.Lang.Strings.ThemeManager.Actions.LogTimerFinished;
            timer1.Enabled = false;
            timer1.Stop();
            Forms.Saving_ex_list.ex_List = Exceptions.ThemeApply;
            Forms.Saving_ex_list.ShowDialog();
        }

        private void Button25_Click(object sender, EventArgs e)
        {
            log_lbl.Text = Program.Lang.Strings.ThemeManager.Actions.LogTimerFinished;
            timer1.Enabled = false;
            timer1.Stop();
            (sender as UI.WP.Button).Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Stop();
            Forms.SOS.ShowDialog();
        }
    }
}
