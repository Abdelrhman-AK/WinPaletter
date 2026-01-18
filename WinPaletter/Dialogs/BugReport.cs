using FluentTransitions;
using Serilog.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.Properties;
using WinPaletter.Theme;

namespace WinPaletter
{

    public partial class BugReport
    {
        public BugReport()
        {
            InitializeComponent();
        }

        bool collapsed = true;
        int previousHeight = 540;

        private int CollapsedHeight
        {
            get
            {
                int upperPaddingDifference = separatorH1.Bottom - AnimatedBox1.Bottom;

                return separatorH1.Bottom + upperPaddingDifference + bottom_buttons.Height + 40;
            }
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

                    Transition
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
            Color c = PictureBox1.Image.AverageColor().CB(Program.Style.DarkMode ? -0.35f : 0.35f);
            AnimatedBox1.BackColor = c;

            TreeView1.Font = Fonts.ConsoleMedium;

            Forms.GlassWindow.Show();

            foreach (Label lbl in AnimatedBox1.Controls.OfType<Label>())
                lbl.ForeColor = Color.White;

            CustomSystemSounds.Exclamation.Play();

            Height = CollapsedHeight;

            Rectangle area = Screen.FromControl(this).WorkingArea;
            int targetX = (area.Width / 2) - (Width / 2);
            int targetY = (area.Height / 2) - (Height / 2);

            Location = new Point(targetX, targetY);

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
            List<Exception> exceptions = [];

            while (exception != null)
            {
                exceptions.Add(exception);
                exception = exception.InnerException;
            }

            return exceptions;
        }

        public void ThrowError(Exception ex, bool noRecovery = false, int win32Error = -1)
        {
            // Try is used to avoid loop in case of error in the backup function
            try
            {
                // If theme backup option is enabled, backup it before throwing error
                if (Program.Settings.BackupTheme.Enabled && Program.Settings.BackupTheme.AutoBackupOnExError && Program.TM is not null)
                {
                    string filename = Program.GetUniqueFileName($"{Program.Settings.BackupTheme.BackupPath}\\OnExceptionError", $"{Program.TM.Info.ThemeName}_{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.wpth");
                    Program.TM.Save(Manager.Source.File, filename);
                }
            }
            catch { }

            if (win32Error == -1) { win32Error = Marshal.GetLastWin32Error(); }

            Label7.Text = ex.GetType().FullName + (!string.IsNullOrWhiteSpace(ex?.Message) ? $": {ex?.Message}" : string.Empty);

            AlertBox1.Visible = noRecovery;
            TreeView1.Nodes?.Clear();

            TreeNode n = TreeView1?.Nodes.Add("Information");
            n?.Nodes.Add("OS").Nodes.Add($"{OS.Name_English}, {OS.Build}, {OS.Architecture_English}");

#if DEBUG
            n?.Nodes.Add("WinPaletter version").Nodes.Add($"{Program.Version}{(Program.IsBeta ? $", {Program.Lang.Strings.General.Beta}" : string.Empty)}, Build: Debug");
#else
            n?.Nodes.Add("WinPaletter version").Nodes.Add($"{Program.Version}{(Program.IsBeta ? $", {Program.Lang.Strings.General.Beta}" : string.Empty)}, Build: Release");
#endif

            n?.Nodes.Add("WinPaletter language").Nodes.Add(Program.Lang.Information.Lang);
            n?.Nodes.Add("Debugger is attached?").Nodes.Add(Debugger.IsAttached ? Program.Lang.Strings.General.Yes : Program.Lang.Strings.General.No);

            if (ex is not null)
            {
                AddException("Exception", ex, TreeView1);

                if (ex?.GetBaseException() is not null && ex?.GetBaseException() != ex)
                {
                    Exception baseEx = ex.GetBaseException();
                    AddException("Base exception", baseEx, TreeView1, win32Error);

                    IEnumerable<Exception> exceptions = GetAllInnerExceptions(baseEx);
                    if (exceptions.Count() > 0)
                    {
                        int i = 0;
                        foreach (Exception sub_ex in exceptions)
                        {
                            AddException($"Base exception: sub inner exception {i}", sub_ex.InnerException, TreeView1, win32Error);
                            i++;
                        }
                    }
                }

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

            n?.Collapse();

            TreeView1.SelectedNode = TreeView1.Nodes[0];

            if (!Directory.Exists($@"{SysPaths.appData}\Reports")) Directory.CreateDirectory($@"{SysPaths.appData}\Reports");

            string exLogPath = $@"{SysPaths.appData}\Reports\{DateTime.Now:HHmmss_ddMMyy}.txt";

            File.WriteAllText(exLogPath, GetDetails());

            Program.Log?.Write(LogEventLevel.Error, $"{ex}:\r\n{GetDetails().Trim()}");

            Program.Log?.Write(LogEventLevel.Information, $"Exception error full details text file is saved as {exLogPath}");

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
            using (SaveFileDialog dlg = new() { Filter = Program.Filters.Text, Title = Program.Lang.Strings.Extensions.SaveText })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(dlg.FileName, GetDetails());
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
            SB.AppendLine($"   OS = \"{OS.Name_English}, {OS.Build}, {OS.Architecture_English}\";");

#if DEBUG
            SB.AppendLine($"   WinPaletter.Version = \"{Program.Version}{(Program.IsBeta ? $", {Program.Lang.Strings.General.Beta}" : string.Empty)}, Build: Debug\";");
#else
            SB.AppendLine($"   WinPaletter.Version = \"{Program.Version}{(Program.IsBeta ? $", {Program.Lang.Strings.General.Beta}" : string.Empty)}, Build: Release\";");
#endif


            SB.AppendLine($"   WinPaletter.Language = \"{Program.Lang.Information.Lang}\";");
            SB.AppendLine($"   WinPaletter.Debugging = {(Debugger.IsAttached ? "true" : "false")};");
            SB.AppendLine();

            SB.AppendLine("//Error details");
            SB.AppendLine("//...........................................................");

            foreach (TreeNode x in TreeView1.Nodes)
            {
                if (x == TreeView1.Nodes[0]) continue;

                string prop = x.Text.Replace(" ", ".").Replace("'s", string.Empty).Replace(@"\", "_");

                if (x.Nodes?.Count == 1)
                {
                    if (int.TryParse(x.Nodes[0].Text, out int Number))
                    {
                        SB.AppendLine($"   {prop} = {Number};");
                    }
                    else
                    {
                        SB.AppendLine($"   {prop} = \"{x.Nodes[0].Text.Replace("\\", "\\\\")}\";");
                    }
                }

                else
                {
                    SB.AppendLine($"   {prop} =");
                    SB.AppendLine("   " + "{");

                    foreach (TreeNode y in x.Nodes) { SB.AppendLine($"      {y.Text.Replace("\\", "\\\\")}"); }

                    SB.AppendLine("   " + "};");
                }
            }

            SB.AppendLine("```");

            return SB.ToString();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (Directory.Exists($@"{SysPaths.appData}\Reports"))
            {
                Forms.GlassWindow.Close();
                Process.Start($@"{SysPaths.appData}\Reports");
            }
            else
            {
                MsgBox(string.Format(Program.Lang.Strings.Messages.Bug_NoReport, $@"{SysPaths.appData}\Reports"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TreeView1_DoubleClick(object sender, EventArgs e)
        {
            if (TreeView1.SelectedNode is not null) Clipboard.SetText(TreeView1?.SelectedNode?.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Forms.SOS.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (collapsed)
            {
                collapsed = false;
                GroupBox3.Visible = false;
                (sender as UI.WP.Button).ImageGlyph = Resources.Glyph_Up;

                Transition
                    .With(this, nameof(Height), previousHeight)
                    .With(this, nameof(Top), Top - (previousHeight - Height) / 2)
                    .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));

                Program.Animator.HideSync(label2);

                // Make async delay to make fading in groupbox is consistent with form animation
                Task.Run(() =>
                {
                    Thread.Sleep(Program.AnimationDuration / 3);
                    Invoke(() => Program.Animator.ShowSync(GroupBox3));
                });
            }
            else
            {
                collapsed = true;
                previousHeight = Height;
                (sender as UI.WP.Button).ImageGlyph = Resources.Glyph_Down;

                Program.Animator.Hide(GroupBox3);
                Program.Animator.Show(label2);

                Transition
                    .With(this, nameof(Height), CollapsedHeight)
                    .With(this, nameof(Top), Top + (Height - CollapsedHeight) / 2)
                    .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration));
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, FileName = string.IsNullOrWhiteSpace(Forms.Home.File) ? Program.TM.Info.ThemeName + ".wpth" : Forms.Home.File, Title = Program.Lang.Strings.Extensions.SaveWinPaletterTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Forms.Home.File = dlg.FileNames[0];
                    Program.TM.Save(Manager.Source.File, Forms.Home.File);
                    Forms.Home.Text = Path.GetFileName(Forms.Home.File);
                    Forms.Home.LoadFromTM(Program.TM);
                }
            }
        }
    }
}