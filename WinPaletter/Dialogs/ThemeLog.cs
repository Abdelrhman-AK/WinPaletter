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
        private List<Form> OpenForms = new();
        private Thread Apply_Thread;

        public ThemeLog()
        {
            InitializeComponent();
            this.FormClosing += ThemeLog_Closing;
        }

        private void ThemeLog_Load(object sender, EventArgs e)
        {
            Icon = Forms.MainFrm.Icon;
            this.LoadLanguage();
            ApplyStyle(this);
            CheckForIllegalCrossThreadCalls = false;
            TreeView1.ImageList = Program.Notifications_IL;
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

            foreach (Form f in OpenForms)
            {
                f.Visible = true;
            }
        }

        /// <summary>
        /// This will apply WinPaletter theme with showing theme log
        /// </summary>
        /// <param name="TM">WinPaletter theme manager</param>
        public void Apply_Theme(Theme.Manager TM = null, bool AdditionalStoreTips = false)
        {
            TM ??= Program.TM;

            OpenForms.Clear();

            if (Program.Settings.ThemeLog.VerboseLevel != WPSettings.Structures.ThemeLog.VerboseLevels.None)
            {
                foreach (Form f in Application.OpenForms)
                {
                    if (f != this)
                    {
                        OpenForms.Add(f);
                        f.Visible = false;
                    }
                }

                this.Show();
            }

            Apply_Thread = new Thread(() =>
            {
                // New method of restarting explorer
                if (Program.Settings.ThemeApplyingBehavior.AutoRestartExplorer)
                    Program.CMD_Wrapper.SendCommand($"{Program.PATH_System32}\\taskkill.exe /F /IM explorer.exe");

                bool LogEnabled = Program.Settings.ThemeLog.VerboseLevel != WPSettings.Structures.ThemeLog.VerboseLevels.None;

                animatedBox1.Color = TM.Info.Color1;
                animatedBox1.Color1 = TM.Info.Color1;
                animatedBox1.Color2 = TM.Info.Color2;

                TreeView1.Nodes.Clear();

                log_lbl.Text = string.Format(Program.Lang.TM_ApplyingTheme, TM.Info.ThemeName);

                Cursor = Cursors.WaitCursor;

                Button8.Visible = false;
                Button14.Visible = false;
                Button22.Visible = false;
                Button25.Visible = false;
                button1.Visible = false;

                try
                {
                    TM.Save(Theme.Manager.Source.Registry, "", LogEnabled ? TreeView1 : null);

                    if (LogEnabled)
                        Theme.Manager.AddNode(TreeView1, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_AllDone), "info");

                    if (TM.MetricsFonts.Enabled & GetWindowsScreenScalingFactor() > 100d)
                        Theme.Manager.AddNode(TreeView1, string.Format("{0}", Program.Lang.TM_MetricsHighDPIAlert), "info");

                    if (AdditionalStoreTips)
                        Theme.Manager.AddNode(TreeView1, Program.Lang.Store_LogoffRecommended, "info");
                }
                catch (Exception ex)
                {
                    Theme.Manager.AddNode(TreeView1, Program.Lang.TM_FatalErrorHappened, "error");
                    Program.Saving_Exceptions.Add(new Tuple<string, Exception>(ex.Message, ex));
                }

                Program.TM_Original = new Theme.Manager(Theme.Manager.Source.Registry);

                Cursor = Cursors.Default;

                log_lbl.Visible = true;
                Button8.Visible = true;
                Button22.Visible = true;
                Button25.Visible = true;
                button1.Visible = true;

                if (Program.Saving_Exceptions.Count != 0)
                {
                    log_lbl.Text = Program.Lang.TM_ErrorHappened;
                    Button14.Visible = true;
                }
                else if (Program.Settings.ThemeLog.CountDown && Program.Settings.ThemeLog.VerboseLevel != WPSettings.Structures.ThemeLog.VerboseLevels.Detailed)
                {
                    log_lbl.Text = string.Format(Program.Lang.TM_LogWillClose, Program.Settings.ThemeLog.CountDown_Seconds);
                    elapsedSecs = 1;

                    //Invoking is important to access timer from different thread
                    this.Invoke(new Action(() => 
                    {
                        timer1.Enabled = true;
                        timer1.Start();
                    }));
                }
                else
                {
                    log_lbl.Text = Program.Lang.TM_LogTimerFinished;
                }

                //New method of restarting explorer
                if (Program.Settings.ThemeApplyingBehavior.AutoRestartExplorer)
                    Program.processExplorer.Start();
                else if (LogEnabled)
                    Theme.Manager.AddNode(TreeView1, Program.Lang.NoDefResExplorer, "warning");

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

            if (Forms.MainFrm.SaveFileDialog3.ShowDialog() == DialogResult.OK)
            {
                var sb = new StringBuilder();
                sb.Clear();

                foreach (TreeNode N in TreeView1.Nodes)
                    sb.AppendLine(string.Format("[{0}]{2} {1}{3}", N.ImageKey, N.Text, "\t", "\r\n"));

                System.IO.File.WriteAllText(Forms.MainFrm.SaveFileDialog3.FileName, sb.ToString());
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            log_lbl.Text = "";
            timer1.Enabled = false;
            timer1.Stop();
            this.Close();
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            log_lbl.Text = Program.Lang.TM_LogTimerFinished;
            timer1.Enabled = false;
            timer1.Stop();
            Forms.Saving_ex_list.ex_List = Program.Saving_Exceptions;
            Forms.Saving_ex_list.ShowDialog();
        }

        private void Button25_Click(object sender, EventArgs e)
        {
            log_lbl.Text = Program.Lang.TM_LogTimerFinished;
            timer1.Enabled = false;
            timer1.Stop();
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
