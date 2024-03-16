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

            Forms.GlassWindow.Show();

            foreach (Label lbl in AnimatedBox1.Controls.OfType<Label>())
                lbl.ForeColor = Color.White;

            SystemSounds.Exclamation.Play();

            BringToFront();

            Center();
        }

        public void AddData(string str, Exception ex, TreeView treeView)
        {
            if (ex is not null && ex.Data is not null && ex.Data.Keys is not null && ex.Data.Keys.Count > 0)
            {
                TreeNodeCollection temp = treeView?.Nodes?.Add($"{str} data").Nodes;

                foreach (DictionaryEntry x in ex.Data) temp?.Add($"{x.Key} = {x.Value}");
            }
        }

        public void AddException(string str, Exception ex, TreeView treeView, int win32Error = 0)
        {
            if (ex is not null)
            {
                treeView?.Nodes?.Add($"{str} message").Nodes?.Add(ex?.Message ?? "No message is included");

                treeView?.Nodes?.Add("Exception type").Nodes?.Add(ex?.GetType().ToString());

                if (win32Error != 0) { treeView?.Nodes?.Add($"{str} Marshal.GetLastWin32Error").Nodes?.Add(win32Error.ToString()); }

                if (!string.IsNullOrWhiteSpace(ex?.StackTrace))
                {
                    TreeNode n = treeView?.Nodes?.Add($"{str} stack trace");

                    foreach (string x in ex?.StackTrace.Split('\r')) if (!string.IsNullOrWhiteSpace(x)) n?.Nodes?.Add(x.Trim());
                }

                AddData(str, ex, treeView);

                if (ex?.TargetSite is not null)
                {
                    treeView?.Nodes?.Add($@"{str} target void\function").Nodes?.Add($"{ex?.Source}.{ex?.TargetSite?.Name}()");
                }

                if (ex?.TargetSite?.Module?.Assembly != null)
                {
                    if (!string.IsNullOrWhiteSpace(ex?.TargetSite?.Module?.Assembly?.FullName))
                        treeView?.Nodes?.Add($"{str} assembly").Nodes?.Add(ex?.TargetSite?.Module?.Assembly?.FullName);

                    if (!string.IsNullOrWhiteSpace(ex?.TargetSite?.Module?.Assembly?.Location))
                        treeView?.Nodes?.Add($"{str} assembly's file").Nodes?.Add(ex?.TargetSite?.Module?.Assembly?.Location);
                }

                treeView?.Nodes?.Add($"{str} HRESULT").Nodes?.Add(ex?.HResult.ToString());

                if (!string.IsNullOrWhiteSpace(ex?.HelpLink)) treeView?.Nodes?.Add($"{str} Microsoft help link").Nodes?.Add(ex?.HelpLink);
            }
        }

        public static IEnumerable<Exception> GetAllInnerExceptions(Exception exception)
        {
            List<Exception> exceptions = new();

            while (exception != null)
            {
                exceptions.Add(exception);
                exception = exception.InnerException;
            }

            return exceptions;
        }

        public void ThrowError(Exception ex, bool noRecovery = false, int win32Error = -1)
        {
            if (win32Error == -1) { win32Error = Marshal.GetLastWin32Error(); }

            Label7.Text = string.Format(Program.Lang.BugReport_Format, ex?.Message ?? ex.GetType().FullName);

            Label2.Text = $"{OS.Name_English}, {OS.Build}, {OS.Architecture_English}";

            Label3.Text = $"{Program.Version}{(Program.IsBeta ? $", {Program.Lang.Beta}" : string.Empty)}";

#if DEBUG
            Label3.Text += ", Build: Debug";
#else
            Label3.Text += ", Build: Release";
#endif

            label9.Text = Debugger.IsAttached ? Program.Lang.Yes : Program.Lang.No;

            AlertBox1.Visible = noRecovery;
            TreeView1.Nodes?.Clear();

            if (ex is not null)
            {
                AddException("Exception", ex, TreeView1);

                if (ex?.InnerException is not null)
                {
                    AddException("Inner exception", ex.InnerException, TreeView1, win32Error);

                    IEnumerable<Exception> exceptions = GetAllInnerExceptions(ex.InnerException);
                    if (exceptions.Count() > 0)
                    {
                        int i = 0;
                        foreach (Exception sub_ex in exceptions)
                        {
                            AddException($"Sub inner exception {i}", sub_ex.InnerException, TreeView1, win32Error);
                            i++;
                        }
                    }
                }
            }

            if (win32Error != 0)
            {
                Win32Exception win32Exception = new(win32Error);
                if (win32Exception != null) { AddException("Win32 exception", win32Exception, TreeView1, win32Error); }
            }

            TreeView1.ExpandAll();

            TreeView1.SelectedNode = TreeView1.Nodes[0];

            if (!System.IO.Directory.Exists($@"{SysPaths.appData}\Reports")) System.IO.Directory.CreateDirectory($@"{SysPaths.appData}\Reports");

            System.IO.File.WriteAllText($@"{SysPaths.appData}\Reports\{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second} {DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}.txt", GetDetails());

            ShowDialog();

            Forms.GlassWindow.Close();

            if (DialogResult == DialogResult.Abort) Program.ExitAfterException = true; else Program.ExitAfterException = false;
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
            Forms.GlassWindow.Close();
            Process.Start(Links.Issues);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GetDetails());
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new() { Filter = Program.Filters.Text, Title = Program.Lang.Filter_SaveText })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllText(dlg.FileName, GetDetails());
                }
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
            SB.AppendLine($"   WinPaletter.Language = \"{Program.Lang.Lang}\";");
            SB.AppendLine($"   WinPaletter.Debugging = {(Debugger.IsAttached ? "true" : "false")};");
            SB.AppendLine();

            SB.AppendLine("//Error details");
            SB.AppendLine("//...........................................................");

            foreach (TreeNode x in TreeView1.Nodes)
            {
                string prop = x.Text.Replace(" ", ".").Replace("'s", string.Empty).Replace(@"\", "_");

                if (x.Nodes?.Count == 1)
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
            if (System.IO.Directory.Exists($@"{SysPaths.appData}\Reports"))
            {
                Forms.GlassWindow.Close();
                Process.Start($@"{SysPaths.appData}\Reports");
            }
            else
            {
                MsgBox(string.Format(Program.Lang.Bug_NoReport, $@"{SysPaths.appData}\Reports"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TreeView1_DoubleClick(object sender, EventArgs e)
        {
            if (TreeView1.SelectedNode is not null) Clipboard.SetText(TreeView1?.SelectedNode?.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Forms.RescueTools.ShowDialog();
        }
    }
}