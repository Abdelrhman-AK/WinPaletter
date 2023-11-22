using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinPaletter
{

    public partial class BugReport
    {
        public BugReport()
        {
            InitializeComponent();
        }

        private void Center()
        {
            Location = new Point(Location.X - 15, Location.Y - 15);

            Task.Delay(10).ContinueWith(_ =>
            {
                BeginInvoke(new MethodInvoker(() =>
                {
                    var area = Screen.FromControl(this).WorkingArea;
                    var targetX = (area.Width / 2) - (Width / 2);
                    var targetY = (area.Height / 2) - (Height / 2);

                    FluentTransitions.Transition
                        .With(this, nameof(Left), targetX)
                        .With(this, nameof(Top), targetY)
                        .Spring(TimeSpan.FromSeconds(0.75));
                }));
            });
        }

        private void BugReport_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            var c = PictureBox1.Image.AverageColor().CB((float)(Program.Style.DarkMode ? -0.35d : 0.35d));
            AnimatedBox1.BackColor = c;

            Label2.Font = Fonts.ConsoleMedium;
            Label3.Font = Fonts.ConsoleMedium;
            TreeView1.Font = Fonts.ConsoleMedium;

            try { Forms.BK.Close(); }
            catch { }

            try { Forms.BK.Show(); }
            catch { }

            foreach (var lbl in AnimatedBox1.Controls.OfType<Label>())
                lbl.ForeColor = Color.White;

            Program.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Exclamation);

            BringToFront();

            Center();
        }

        public void AddData(string str, Exception Exception, TreeView Treeview)
        {
            try
            {
                if (Exception.Data.Keys.Count > 0)
                {
                    var temp = Treeview.Nodes.Add(str + " data").Nodes;

                    foreach (DictionaryEntry x in Exception.Data)
                        temp.Add(string.Format("{0} = {1}", x.Key.ToString(), x.Value.ToString()));
                }
            }
            catch { }
        }

        public void AddException(string str, Exception Exception, TreeView TreeView, int Win32Error = 0)
        {
            if (Exception is not null)
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(Exception.Message))
                    {

                        TreeView.Nodes.Add(str + " message").Nodes.Add(Exception.Message);

                        TreeView.Nodes.Add("Exception type").Nodes.Add(Exception.GetType().ToString());

                        if (Win32Error != 0) { TreeView.Nodes.Add(str + " Marshal.GetLastWin32Error").Nodes.Add(Win32Error.ToString()); }

                        if (!string.IsNullOrEmpty(Exception.StackTrace))
                        {
                            var n = TreeView.Nodes.Add(str + " stack trace");

                            foreach (var x in Exception.StackTrace.CList())
                                n.Nodes.Add(x);
                        }

                        AddData(str, Exception, TreeView);

                        if (Exception.TargetSite != null)
                        {
                            TreeView.Nodes.Add(str + @" target void\function").Nodes.Add($"{Exception.Source}.{Exception.TargetSite.Name}()");
                        }

                        if (Exception.TargetSite.Module.Assembly != null)
                        {
                            TreeView.Nodes.Add(str + " assembly").Nodes.Add(Exception.TargetSite.Module.Assembly.FullName);
                            TreeView.Nodes.Add(str + " assembly's file").Nodes.Add(Exception.TargetSite.Module.Assembly.Location);
                        }

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

        public void ThrowError(Exception Exception, bool NoRecovery = false, int Win32Error = -1)
        {
            if (Win32Error == -1) { Win32Error = Marshal.GetLastWin32Error(); }

            Label7.Text = string.Format(Program.Lang.BugReport_Title, Exception.GetType().ToString());

            Label2.Text = $"{OS.Name_English}, {OS.Build}, {OS.Architecture_English}";

            Label3.Text = Program.Version + (Program.IsBeta ? $", {Program.Lang.Beta}" : string.Empty);

            AlertBox1.Visible = NoRecovery;
            TreeView1.Nodes.Clear();

            if (Exception is not null)
            {
                AddException("Exception", Exception, TreeView1);
                if (Exception.InnerException != null) { AddException("Inner exception", Exception.InnerException, TreeView1, Win32Error); }
            }

            if (Win32Error != 0)
            {
                Win32Exception win32Exception = new();
                if (win32Exception != null) { AddException("Win32 exception", win32Exception, TreeView1, Win32Error); }
            }

            TreeView1.ExpandAll();

            if (!System.IO.Directory.Exists(PathsExt.appData + @"\Reports"))
                System.IO.Directory.CreateDirectory(PathsExt.appData + @"\Reports");

            System.IO.File.WriteAllText(string.Format(PathsExt.appData + @"\Reports\{0}.{1}.{2} {3}-{4}-{5}.txt", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year), GetDetails());

            ShowDialog();

            Forms.BK.Close();

            if (DialogResult == DialogResult.Abort)
                Program.ExitAfterException = true;
            else
                Program.ExitAfterException = false;
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
            Process.Start(Properties.Resources.Link_Repository + "issues");
            try
            {
                Forms.BK.Close();
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
            SB.AppendLine("```cs");
            SB.AppendLine("//General information");
            SB.AppendLine("//...........................................................");
            SB.AppendLine(string.Format("   Report.Date = \"{0}\";", DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString()));
            SB.AppendLine(string.Format("   OS = \"{0}\";", Label2.Text));
            SB.AppendLine(string.Format("   WinPaletter.Version = \"{0}\";", Label3.Text));
            SB.AppendLine();

            SB.AppendLine("//Error details");
            SB.AppendLine("//...........................................................");

            foreach (TreeNode x in TreeView1.Nodes)
            {
                string prop = x.Text.Replace(" ", ".").Replace("'s", string.Empty).Replace(@"\", "_");

                if (x.Nodes.Count == 1)
                {
                    if (int.TryParse(x.Nodes[0].Text, out int Number))
                    {
                        SB.AppendLine("   " + prop + $" = {Number};");
                    }
                    else
                    {
                        SB.AppendLine("   " + prop + " = \"" + x.Nodes[0].Text.Replace("\\", "\\\\") + "\";");
                    }
                }

                else
                {
                    SB.AppendLine("   " + prop + " =");
                    SB.AppendLine("   " + "{");

                    foreach (TreeNode y in x.Nodes) { SB.AppendLine("      " + y.Text.Replace("\\", "\\\\")); }

                    SB.AppendLine("   " + "};");
                }
            }

            SB.AppendLine("```");

            return SB.ToString();
        }

        private void Button6_Click(object sender, EventArgs e)
        {

            if (System.IO.Directory.Exists(PathsExt.appData + @"\Reports"))
            {
                Process.Start(PathsExt.appData + @"\Reports");
                try
                {
                    Forms.BK.Close();
                }
                catch
                {
                }
            }
            else
            {
                MsgBox(string.Format(Program.Lang.Bug_NoReport, PathsExt.appData + @"\Reports"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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