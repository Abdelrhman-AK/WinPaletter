using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinPaletter.Dialogs
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

        private void ThemeLog_Load(object sender, EventArgs e)
        {
            Icon = Forms.MainForm.Icon;
            this.LoadLanguage();
            ApplyStyle(this);
            CheckForIllegalCrossThreadCalls = false;
            TreeView1.ImageList = ImageLists.ThemeLog;
        }

        private void ThemeLog_Closing(object sender, FormClosingEventArgs e)
        {
            if (Apply_Thread.IsAlive)
            {
                if (MsgBox(Program.Lang.TM_CloseOnApplying0, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, Program.Lang.TM_CloseOnApplying1) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
                else
                {
                    Apply_Thread.Abort();
                }
            }
        }

        /// <summary>
        /// This will apply WinPaletter theme with showing theme log
        /// </summary>
        /// <param name="TM">WinPaletter theme manager</param>
        public void Apply_Theme(Theme.Manager TM = null, bool AdditionalStoreTips = false, bool dontInvoke = false)
        {
            TM ??= Program.TM;

            if (Program.Settings.ThemeLog.VerboseLevel != Settings.Structures.ThemeLog.VerboseLevels.None)
            {
                Show();

                //Hiding forms or adding this log into tabs causing a bug; resizing window is not working properly
                //Forms.MainForm.tabsContainer1.AddFormIntoTab(this);
            }

            Apply_Thread = new(() =>
            {
                bool LogEnabled = Program.Settings.ThemeLog.VerboseLevel != Settings.Structures.ThemeLog.VerboseLevels.None;

                animatedBox1.Color1 = TM.Info.Color1;
                animatedBox1.Color2 = TM.Info.Color2;

                TreeView1.Nodes.Clear();

                log_lbl.SetText(string.Format(Program.Lang.TM_ApplyingTheme, TM.Info.ThemeName));

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
                        Program.ExplorerKiller.Start();
                        Program.ExplorerKiller.WaitForExit();
                    }
                }

                try
                {
                    TM.Save(Theme.Manager.Source.Registry, string.Empty, LogEnabled ? TreeView1 : null);

                    if (LogEnabled)
                        Theme.Manager.AddNode(TreeView1, $"{DateTime.Now.ToLongTimeString()}: {Program.Lang.TM_AllDone}", "info");

                    if (AdditionalStoreTips)
                        Theme.Manager.AddNode(TreeView1, Program.Lang.Store_LogoffRecommended, "info");
                }
                catch (Exception ex)
                {
                    Theme.Manager.AddNode(TreeView1, Program.Lang.TM_FatalErrorHappened, "error");
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
                        Button25.Visible = true;
                    });
                }
                else
                {
                    log_lbl.Visible = true;
                    Button8.Visible = true;
                    Button22.Visible = true;
                    Button25.Visible = true;
                }

                if (Exceptions.ThemeApply.Count != 0)
                {
                    log_lbl.SetText(Program.Lang.TM_ErrorHappened);

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
                    log_lbl.SetText(string.Format(Program.Lang.TM_LogWillClose, Program.Settings.ThemeLog.CountDown_Seconds));
                    elapsedSecs = 1;

                    if (!dontInvoke)
                    {
                        //Invoking is important to access timer from different thread
                        BeginInvoke(() =>
                    {
                        timer1.Enabled = true;
                        timer1.Start();
                    });
                    }
                    else
                    {
                        timer1.Enabled = true;
                        timer1.Start();
                    }
                }
                else
                {
                    log_lbl.SetText(Program.Lang.TM_LogTimerFinished);
                }

                //New method of restarting Explorer
                if (Program.Settings.ThemeApplyingBehavior.AutoRestartExplorer)
                {
                    if (User.SID == User.UserSID_OpenedWP && User.SID == User.AdminSID_GrantedUAC)
                    {
                        Program.Explorer_exe.Start();
                    }
                    else
                    {
                        Theme.Manager.AddNode(TreeView1, $"{Program.Lang.RestartExplorerIssue0}. {Program.Lang.RestartExplorerIssue1}", "warning");
                    }
                }

                else if (LogEnabled) { Theme.Manager.AddNode(TreeView1, Program.Lang.NoDefResExplorer, "warning"); }
            });

            Apply_Thread.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            log_lbl.Text = string.Format(Program.Lang.TM_LogWillClose, Program.Settings.ThemeLog.CountDown_Seconds - elapsedSecs);

            if (elapsedSecs + 1 <= Program.Settings.ThemeLog.CountDown_Seconds)
            {
                elapsedSecs += 1;
            }
            else
            {
                ((System.Windows.Forms.Timer)sender).Enabled = false;
                ((System.Windows.Forms.Timer)sender).Stop();
                this.Close();
            }
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            log_lbl.Text = Program.Lang.TM_LogTimerFinished;
            timer1.Enabled = false;
            timer1.Stop();

            using (SaveFileDialog dlg = new() { Filter = Program.Filters.Text, Title = Program.Lang.Filter_SaveText })
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
            log_lbl.Text = Program.Lang.TM_LogTimerFinished;
            timer1.Enabled = false;
            timer1.Stop();
            Forms.Saving_ex_list.ex_List = Exceptions.ThemeApply;
            Forms.Saving_ex_list.ShowDialog();
        }

        private void Button25_Click(object sender, EventArgs e)
        {
            log_lbl.Text = Program.Lang.TM_LogTimerFinished;
            timer1.Enabled = false;
            timer1.Stop();
            (sender as UI.WP.Button).Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            log_lbl.Text = Program.Lang.TM_LogTimerFinished;
            timer1.Enabled = false;
            timer1.Stop();
            Forms.RescueTools.ShowDialog();
        }
    }
}
