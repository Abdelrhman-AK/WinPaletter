using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinPaletter
{

    public partial class BugReport
    {
        public BugReport()
        {
            InitializeComponent();
        }
        private void BugReport_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            WPStyle.ApplyStyle(this);
            var c = PictureBox1.Image.AverageColor().CB((float)(My.Env.Style.DarkMode ? -0.35d : 0.35d));
            AnimatedBox1.BackColor = c;
            this.DrawCustomTitlebar(c);

            Label2.Font = My.MyProject.Application.ConsoleFontMedium;
            Label3.Font = My.MyProject.Application.ConsoleFontMedium;
            TreeView1.Font = My.MyProject.Application.ConsoleFontMedium;

            try
            {
                My.MyProject.Forms.BK.Close();
            }
            catch
            {
            }
            try
            {
                My.MyProject.Forms.BK.Show();
            }
            catch
            {
            }

            foreach (var lbl in AnimatedBox1.Controls.OfType<Label>())
                lbl.ForeColor = Color.White;

            My.MyProject.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Exclamation);
        }

        public void AddData(string str, Exception Exception, TreeView Treeview)
        {

            try
            {
                {
                    var temp = Treeview.Nodes.Add(str + " data").Nodes;
                    if (Exception.Data.Keys.Count > 0)
                    {
                        foreach (DictionaryEntry x in Exception.Data)
                            temp.Add(string.Format("{0} = {1}", x.Key.ToString(), x.Value.ToString()));
                    }
                    else
                    {
                        temp.Add("There is no included data in " + str);
                    }

                }
            }
            catch
            {
            }
        }

        public void AddException(string str, Exception Exception, TreeView TreeView)
        {
            if (Exception is not null)
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(Exception.Message))
                    {

                        TreeView.Nodes.Add(str + " message").Nodes.Add(Exception.Message);

                        TreeView.Nodes.Add("Exception type").Nodes.Add(Exception.GetType().ToString());

                        var n = TreeView.Nodes.Add(str + " stack trace");

                        foreach (var x in Exception.StackTrace.CList())
                            n.Nodes.Add(x);

                        AddData(str, Exception, TreeView);

                        TreeView.Nodes.Add(str + @" target sub\function").Nodes.Add(Exception.TargetSite.Name + " @ " + Exception.Source);
                        TreeView.Nodes.Add(str + " assembly").Nodes.Add(Exception.TargetSite.Module.Assembly.FullName);
                        TreeView.Nodes.Add(str + " assembly's file").Nodes.Add(Exception.TargetSite.Module.Assembly.Location);
                        TreeView.Nodes.Add(str + " HRESULT").Nodes.Add(Exception.HResult.ToString());
                        if (!string.IsNullOrWhiteSpace(Exception.HelpLink))
                            TreeView.Nodes.Add(str + " Microsoft help link").Nodes.Add(Exception.HelpLink);

                    }
                }
                catch
                {
                }
            }
        }

        public void ThrowError(Exception Exception, bool NoRecovery = false)
        {

            string CV = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion";
            string sy = "." + Microsoft.Win32.Registry.GetValue(CV, "UBR", 0).ToString();
            if (sy == ".0")
                sy = "";

            string sx = System.Runtime.InteropServices.RuntimeInformation.OSDescription.Replace("Microsoft Windows ", "");
            sx = sx.Replace("S", "").Trim();

            Label7.Text = string.Format(My.Env.Lang.BugReport_Title, Exception.GetType().ToString());

            Label2.Text = System.Runtime.InteropServices.RuntimeInformation.OSDescription + " - " + sx + sy + " - " + (Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit");

            Label3.Text = My.Env.AppVersion;

            AlertBox1.Visible = NoRecovery;

            string IE = "";

            TreeView1.Nodes.Clear();
            if (Exception is not null)
            {
                AddException("Exception", Exception, TreeView1);

                if (Exception.InnerException is not null)
                {
                    var x = Exception.InnerException;
                    AddException("Inner exception", x, TreeView1);
                }
            }

            TreeView1.ExpandAll();

            if (!System.IO.Directory.Exists(My.Env.PATH_appData + @"\Reports"))
                System.IO.Directory.CreateDirectory(My.Env.PATH_appData + @"\Reports");

            System.IO.File.WriteAllText(string.Format(My.Env.PATH_appData + @"\Reports\{0}.{1}.{2} {3}-{4}-{5}.txt", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year), GetDetails());

            ShowDialog();

            My.MyProject.Forms.BK.Close();

            if (DialogResult == DialogResult.Abort)
                My.MyProject.Application.ExitAfterException = true;
            else
                My.MyProject.Application.ExitAfterException = false;

        }


        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
            using (var Prc = Process.GetCurrentProcess())
            {
                Prc.Kill();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Process.Start(My.Resources.Link_Repository + "issues");
            try
            {
                My.MyProject.Forms.BK.Close();
            }
            catch
            {
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GetDetails());
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(SaveFileDialog1.FileName, GetDetails());
            }
        }

        public string GetDetails()
        {
            var SB = new StringBuilder();
            SB.Clear();
            SB.AppendLine("```vbnet");
            SB.AppendLine("'General information");
            SB.AppendLine("'-----------------------------------------------------------");
            SB.AppendLine(string.Format("Report.Date = \"{0}\"", DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString()));
            SB.AppendLine(string.Format("OS = \"{0}\"", Label2.Text));
            SB.AppendLine(string.Format("WinPaletter.Version = \"{0}\"", Label3.Text));
            SB.AppendLine();

            SB.AppendLine("'Error details");
            SB.AppendLine("'-----------------------------------------------------------");

            foreach (TreeNode x in TreeView1.Nodes)
            {
                string prop = x.Text.Replace(" ", ".").Replace("'s", "").Replace(@"\", "_");

                if (x.Nodes.Count == 1)
                {
                    SB.AppendLine(prop + " = \"" + x.Nodes[0].Text + "\"");
                }
                else
                {
                    SB.AppendLine(prop + " = {");

                    foreach (TreeNode y in x.Nodes)
                        SB.AppendLine(y.Text);

                    SB.AppendLine("         }");
                }

            }

            SB.AppendLine("```");

            return SB.ToString();
        }

        private void Button6_Click(object sender, EventArgs e)
        {

            if (System.IO.Directory.Exists(My.Env.PATH_appData + @"\Reports"))
            {
                Process.Start(My.Env.PATH_appData + @"\Reports");
                try
                {
                    My.MyProject.Forms.BK.Close();
                }
                catch
                {
                }
            }
            else
            {
                WPStyle.MsgBox(string.Format(My.Env.Lang.Bug_NoReport, My.Env.PATH_appData + @"\Reports"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void TreeView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (TreeView1.SelectedNode is not null)
                    Clipboard.SetText(TreeView1.SelectedNode.Text);
            }
            catch
            {
            }
        }
    }
}