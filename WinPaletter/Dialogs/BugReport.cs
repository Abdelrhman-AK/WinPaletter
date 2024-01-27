using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
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
            Location = new(Location.X - 15, Location.Y - 15);

            Task.Delay(10).ContinueWith(_ =>
            {
                BeginInvoke(new MethodInvoker(() =>
                {
                    Rectangle area = Screen.FromControl(this).WorkingArea;
                    int targetX = (area.Width / 2) - (Width / 2);
                    int targetY = (area.Height / 2) - (Height / 2);

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
            Color c = PictureBox1.Image.AverageColor().CB((float)(Program.Style.DarkMode ? -0.35d : 0.35d));
            AnimatedBox1.BackColor = c;

            Label2.Font = Fonts.ConsoleMedium;
            Label3.Font = Fonts.ConsoleMedium;
            label9.Font = Fonts.ConsoleMedium;

            TreeView1.Font = Fonts.ConsoleMedium;

            try { Forms.BK.Close(); }
            catch { }

            try { Forms.BK.Show(); }
            catch { }

            foreach (Label lbl in AnimatedBox1.Controls.OfType<Label>())
                lbl.ForeColor = Color.White;

            SystemSounds.Exclamation.Play();

            BringToFront();

            Center();
        }

        public void AddData(string str, Exception Exception, TreeView Treeview)
        {
            try
            {
                if (Exception.Data.Keys.Count > 0)
                {
                    TreeNodeCollection temp = Treeview.Nodes.Add($"{str} data").Nodes;

                    foreach (DictionaryEntry x in Exception.Data)
                        temp.Add($"{x.Key.ToString()} = {x.Value.ToString()}");
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

                        TreeView.Nodes.Add($"{str} message").Nodes.Add(Exception.Message);

                        TreeView.Nodes.Add("Exception type").Nodes.Add(Exception.GetType().ToString());

                        if (Win32Error != 0) { TreeView.Nodes.Add($"{str} Marshal.GetLastWin32Error").Nodes.Add(Win32Error.ToString()); }

                        if (!string.IsNullOrEmpty(Exception.StackTrace))
                        {
                            TreeNode n = TreeView.Nodes.Add($"{str} stack trace");

                            foreach (string x in Exception.StackTrace.CList())
                                n.Nodes.Add(x);
                        }

                        AddData(str, Exception, TreeView);

                        if (Exception.TargetSite != null)
                        {
                            TreeView.Nodes.Add($@"{str} target void\function").Nodes.Add($"{Exception.Source}.{Exception.TargetSite.Name}()");
                        }

                        if (Exception.TargetSite.Module.Assembly != null)
                        {
                            TreeView.Nodes.Add($"{str} assembly").Nodes.Add(Exception.TargetSite.Module.Assembly.FullName);
                            TreeView.Nodes.Add($"{str} assembly's file").Nodes.Add(Exception.TargetSite.Module.Assembly.Location);
                        }

                        TreeView.Nodes.Add($"{str} HRESULT").Nodes.Add(Exception.HResult.ToString());

                        if (!string.IsNullOrWhiteSpace(Exception.HelpLink))
                            TreeView.Nodes.Add($"{str} Microsoft help link").Nodes.Add(Exception.HelpLink);
                    }
                }
                catch
                {
                }
            }
        }

        public static IEnumerable<Exception> GetAllInnerExceptions(Exception exception)
        {
            List<Exception> exceptions = new List<Exception>();

            while (exception != null)
            {
                exceptions.Add(exception);
                exception = exception.InnerException;
            }

            return exceptions;
        }

        public void ThrowError(Exception Exception, bool NoRecovery = false, int Win32Error = -1)
        {
            if (Win32Error == -1) { Win32Error = Marshal.GetLastWin32Error(); }

            Label7.Text = string.Format(Program.Lang.BugReport_Title, Exception.GetType().ToString());

            Label2.Text = $"{OS.Name_English}, {OS.Build}, {OS.Architecture_English}";

            Label3.Text = $"{Program.Version}{(Program.IsBeta ? $", {Program.Lang.Beta}" : string.Empty)}";

#if DEBUG
            Label3.Text += ", Build: Debug";
#else
            Label3.Text += ", Build: Release";
#endif

            label9.Text = Debugger.IsAttached ? Program.Lang.Yes : Program.Lang.No;

            AlertBox1.Visible = NoRecovery;
            TreeView1.Nodes.Clear();

            if (Exception is not null)
            {
                AddException("Exception", Exception, TreeView1);
                if (Exception.InnerException != null)
                {
                    AddException("Inner exception", Exception.InnerException, TreeView1, Win32Error);

                    try
                    {
                        IEnumerable<Exception> exceptions = GetAllInnerExceptions(Exception.InnerException);
                        if (exceptions.Count() > 0)
                        {
                            int i = 0;
                            foreach (Exception ex in exceptions)
                            {
                                AddException($"Sub inner exception {i}", Exception.InnerException, TreeView1, Win32Error);
                                i++;
                            }
                        }
                    }
                    catch { }
                }
            }

            if (Win32Error != 0)
            {
                Win32Exception win32Exception = new();
                if (win32Exception != null) { AddException("Win32 exception", win32Exception, TreeView1, Win32Error); }
            }

            TreeView1.ExpandAll();

            TreeView1.SelectedNode = TreeView1.Nodes[0];

            if (!System.IO.Directory.Exists($@"{PathsExt.appData}\Reports"))
                System.IO.Directory.CreateDirectory($@"{PathsExt.appData}\Reports");

            System.IO.File.WriteAllText($@"{PathsExt.appData}\Reports\{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second} {DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}.txt", GetDetails());

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
            Program.ForceExit();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Process.Start($"{Properties.Resources.Link_Repository}issues");
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
            StringBuilder SB = new();
            SB.Clear();
            SB.AppendLine("```cs");
            SB.AppendLine("//General information");
            SB.AppendLine("//...........................................................");
            SB.AppendLine($"   Report.Date = \"{$"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}"}\";");
            SB.AppendLine($"   OS = \"{Label2.Text}\";");
            SB.AppendLine($"   WinPaletter.Version = \"{Label3.Text}\";");
            SB.AppendLine($"   WinPaletter.Language = \"{Program.Lang.Name}\";");
            SB.AppendLine($"   WinPaletter.Debugging = {(Debugger.IsAttached ? "true" : "false")};");
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
                        SB.AppendLine($"   {prop} = {Number};");
                    }
                    else
                    {
                        SB.AppendLine($"   {prop} = \"{(x.Nodes[0].Text.Replace("\\", "\\\\"))}\";");
                    }
                }

                else
                {
                    SB.AppendLine($"   {prop} =");
                    SB.AppendLine("   " + "{");

                    foreach (TreeNode y in x.Nodes) { SB.AppendLine($"      {(y.Text.Replace("\\", "\\\\"))}"); }

                    SB.AppendLine("   " + "};");
                }
            }

            SB.AppendLine("```");

            return SB.ToString();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists($@"{PathsExt.appData}\Reports"))
            {
                Process.Start($@"{PathsExt.appData}\Reports");
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
                MsgBox(string.Format(Program.Lang.Bug_NoReport, $@"{PathsExt.appData}\Reports"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button7_Click(object sender, EventArgs e)
        {
            Forms.RescueTools.ShowDialog();
        }
    }
}