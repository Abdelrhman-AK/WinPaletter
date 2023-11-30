using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter.Dialogs
{
    public partial class RescueTools : Form
    {
        public RescueTools()
        {
            InitializeComponent();
        }

        private void RescueTools_Load(object sender, EventArgs e)
        {
            Icon = Forms.MainFrm.Icon;
            this.LoadLanguage();
            ApplyStyle(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.Explorer_exe.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.ExplorerKiller.Start();
            Program.ExplorerKiller.WaitForExit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.RestartExplorer();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (MsgBox(Program.Lang.LogoffQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question, Program.Lang.LogoffAlert1, string.Empty, string.Empty, string.Empty, string.Empty, Program.Lang.LogoffAlert2, Ookii.Dialogs.WinForms.TaskDialogIcon.Information) == DialogResult.Yes)
            {
                Forms.MainFrm.LoggingOff = true;
                IntPtr intPtr = IntPtr.Zero;
                Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);
                if (System.IO.File.Exists(PathsExt.System32 + @"\logoff.exe"))
                {
                    Interaction.Shell(PathsExt.System32 + @"\logoff.exe", AppWinStyle.Hide);
                }
                else
                {
                    MsgBox(string.Format(Program.Lang.LogoffNotFound, PathsExt.System32), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MsgBox(Program.Lang.RestartQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question, Program.Lang.LogoffAlert1) == DialogResult.Yes)
            {
                Forms.MainFrm.LoggingOff = true;
                IntPtr intPtr = IntPtr.Zero;
                Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);
                if (System.IO.File.Exists(PathsExt.System32 + @"\shutdown.exe"))
                {
                    Interaction.Shell(PathsExt.System32 + @"\shutdown.exe /r /t 0", AppWinStyle.Hide);
                }
                else
                {
                    MsgBox(string.Format(Program.Lang.ShutdownNotFound, PathsExt.System32), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MsgBox(Program.Lang.ShutdownQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question, Program.Lang.LogoffAlert1) == DialogResult.Yes)
            {
                Forms.MainFrm.LoggingOff = true;
                IntPtr intPtr = IntPtr.Zero;
                Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);
                if (System.IO.File.Exists(PathsExt.System32 + @"\shutdown.exe"))
                {
                    Interaction.Shell(PathsExt.System32 + @"\shutdown.exe /s /t 0", AppWinStyle.Hide);
                }
                else
                {
                    MsgBox(string.Format(Program.Lang.ShutdownNotFound, PathsExt.System32), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Task.Run(new Action(() => { SFC(PathsExt.imageres, false, false); }));
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Task.Run(new Action(() => { SFC(string.Empty, true, false); }));
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Task.Run(new Action(() =>
            {
                IntPtr intPtr = IntPtr.Zero;
                Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);

                using (Process process = new()
                {
                    StartInfo = new()
                    {
                        FileName = PathsExt.System32 + "\\cmd.exe",
                        Verb = "runas",
                        UseShellExecute = true
                    }
                })
                {
                    process.StartInfo.Arguments = "/c dism.exe /Online /Cleanup-Image /CheckHealth && pause";
                    process.Start();
                    process.WaitForExit();

                    process.StartInfo.Arguments = "/c dism.exe /Online /Cleanup-Image /ScanHealth && pause";
                    process.Start();
                    process.WaitForExit();

                    switch (MsgBox(Program.Lang.RT_UseWinUpdate, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, Program.Lang.RT_UseInstallWIM))
                    {
                        case DialogResult.Yes:
                            {
                                process.StartInfo.Arguments = "/c dism.exe /Online /Cleanup-Image /RestoreHealth && pause";
                                process.Start();
                                process.WaitForExit();
                                break;
                            }

                        case DialogResult.No:
                            {
                                string file = string.Empty;
                                DialogResult result = DialogResult.None;

                                //Cross thread issue
                                Invoke(() =>
                                {
                                    using (OpenFileDialog o = new() { Filter = "install.wim|install.wim|install.esd|install.esd|*.wim|*.wim|*.esd|*.esd" })
                                    {
                                        result = o.ShowDialog();
                                        file = o.FileName;
                                    }
                                });

                                if (result == DialogResult.OK)
                                {
                                    process.StartInfo.Arguments = $"/c dism.exe /Online /Cleanup-Image /RestoreHealth /Source:{file} && pause";
                                    process.Start();
                                    process.WaitForExit();
                                }

                                break;
                            }

                        case DialogResult.Cancel:
                            {
                                break;
                            }
                    }
                }

                Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero);

            }));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Forms.ThemeLog.Apply_Theme(Program.TM_FirstTime);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Forms.ThemeLog.Apply_Theme(Theme.Default.Get());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Forms.Uninstall.Show();
        }
    }
}
