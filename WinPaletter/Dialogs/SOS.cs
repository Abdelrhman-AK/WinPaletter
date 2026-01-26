using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.Theme;

namespace WinPaletter.Dialogs
{
    public partial class SOS : Form
    {
        public SOS()
        {
            InitializeComponent();
        }

        private void RescueTools_Load(object sender, EventArgs e)
        {
            this.Localize();
            ApplyStyle(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.SendCommand(SysPaths.Explorer, false, true);
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
            if (MsgBox(OS.WXP || OS.WVista || OS.W7 ? Program.Localization.Strings.Messages.LogoffQuestion : Program.Localization.Strings.Messages.SignOutQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question, Program.Localization.Strings.Messages.LogoffAlert1) == DialogResult.Yes)
            {
                Forms.MainForm.LoggingOff = false;

                IntPtr intPtr = IntPtr.Zero;
                Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);
                if (File.Exists($@"{SysPaths.System32}\logoff.exe"))
                {
                    Forms.MainForm.LoggingOff = true;
                    Program.SendCommand($@"{SysPaths.System32}\logoff.exe", false);
                }
                else
                {
                    MsgBox(string.Format(Program.Localization.Strings.Messages.LogoffNotFound, SysPaths.System32), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MsgBox(Program.Localization.Strings.Messages.RestartQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question, Program.Localization.Strings.Messages.LogoffAlert1) == DialogResult.Yes)
            {
                Forms.MainForm.LoggingOff = false;

                IntPtr intPtr = IntPtr.Zero;
                Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);
                if (File.Exists($@"{SysPaths.System32}\shutdown.exe"))
                {
                    Forms.MainForm.LoggingOff = true;
                    Program.SendCommand($@"{SysPaths.System32}\shutdown.exe /r /t 0");
                }
                else
                {
                    MsgBox(string.Format(Program.Localization.Strings.Messages.ShutdownNotFound, SysPaths.System32), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MsgBox(Program.Localization.Strings.Messages.ShutdownQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question, Program.Localization.Strings.Messages.LogoffAlert1) == DialogResult.Yes)
            {
                Forms.MainForm.LoggingOff = false;

                IntPtr intPtr = IntPtr.Zero;
                Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);
                if (File.Exists($@"{SysPaths.System32}\shutdown.exe"))
                {
                    Forms.MainForm.LoggingOff = true;
                    Program.SendCommand($@"{SysPaths.System32}\shutdown.exe /s /t 0");
                }
                else
                {
                    MsgBox(string.Format(Program.Localization.Strings.Messages.ShutdownNotFound, SysPaths.System32), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (!OS.WXP)
            {
                Task.Run(new Action(() => { SFC(SysPaths.imageres, false, false); }));
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Task.Run(new Action(() => { SFC(string.Empty, true, false); }));
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (!OS.WXP)
            {
                Task.Run(new Action(() =>
                {
                    IntPtr intPtr = IntPtr.Zero;
                    Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);

                    using (Process process = new()
                    {
                        StartInfo = new()
                        {
                            FileName = $"{SysPaths.System32}\\cmd.exe",
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

                        switch (MsgBox(Program.Localization.Strings.Messages.RescueTools_UseWinUpdate, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, Program.Localization.Strings.Messages.RescueTools_UseInstallWIM))
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
                                        using (OpenFileDialog o = new() { Filter = Program.Filters.WinImages, Title = Program.Localization.Strings.Extensions.OpenWinImage })
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

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Forms.ThemeLog.Apply_Theme(Program.TM_FirstTime);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Forms.ThemeLog.Apply_Theme(Default.FromCurrentOS);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(Forms.Uninstall);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Process.Start("control");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (!OS.WXP && !OS.WVista && !OS.W7 && !OS.W8x)
            {
                Process.Start($"{SysPaths.Explorer}", "ms-settings:windowsupdate");
            }
            else if (OS.WXP)
            {
                if (File.Exists($"{SysPaths.ProgramFiles}\\Legacy Update\\LegacyUpdate.dll"))
                {
                    // Use legacy update if it is installed
                    Process.Start($"{SysPaths.System32}\\rundll32.exe", $"\"{SysPaths.ProgramFiles}\\Legacy Update\\LegacyUpdate.dll\",LaunchUpdateSite");
                }
                else
                {
                    // Use default update if legacy update is not installed
                    Process.Start($"{SysPaths.System32}\\rundll32.exe", $"{SysPaths.System32}\\muweb.dll,LaunchMUSite");
                }
            }
            else
            {
                Process.Start($"{SysPaths.System32}\\control.exe", "/name Microsoft.WindowsUpdate");
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (File.Exists($"{SysPaths.System32}\\restore\\rstrui.exe"))
            {
                Process.Start($"{SysPaths.System32}\\restore\\rstrui.exe");
            }
            else if (File.Exists($"{SysPaths.System32}\\rstrui.exe"))
            {
                Process.Start($"{SysPaths.System32}\\rstrui.exe");
            }
            else
            {
                Process.Start("control", "sysdm.cpl,,4");
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (MsgBox(Program.Localization.Strings.Messages.RestartRecoveryQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question, Program.Localization.Strings.Messages.LogoffAlert1) == DialogResult.Yes)
            {
                Forms.MainForm.LoggingOff = false;

                IntPtr intPtr = IntPtr.Zero;
                Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);

                if (OS.W8x || OS.W10 || OS.W11 || OS.W12)
                {
                    if (File.Exists($@"{SysPaths.System32}\shutdown.exe"))
                    {
                        Forms.MainForm.LoggingOff = true;
                        Program.SendCommand($@"{SysPaths.System32}\shutdown.exe /r /o /f /t 0");
                    }
                    else
                    {
                        MsgBox(string.Format(Program.Localization.Strings.Messages.ShutdownNotFound, SysPaths.System32), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else if (OS.W7)
                {
                    if (File.Exists($@"{SysPaths.System32}\bcdedit.exe") && File.Exists($@"{SysPaths.System32}\shutdown.exe"))
                    {
                        Forms.MainForm.LoggingOff = true;
                        Program.SendCommand($@"{SysPaths.System32}\bcdedit.exe /set " + "{current} recoverysequence {default}");
                        Program.SendCommand($@"{SysPaths.System32}\bcdedit.exe /set " + "{current} recoveryenabled Yes");
                        Program.SendCommand($@"{SysPaths.System32}\shutdown.exe /r /t 0");
                    }
                    else
                    {
                        if (!File.Exists($@"{SysPaths.System32}\shutdown.exe"))
                        {
                            MsgBox(string.Format(Program.Localization.Strings.Messages.ShutdownNotFound, SysPaths.System32), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            MsgBox(string.Format(Program.Localization.Strings.Messages.BcdeditNotFound, SysPaths.System32), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                else if (OS.WVista || OS.WXP)
                {
                    MsgBox(string.Format(Program.Localization.Strings.Messages.RecoveryNotAvailable, OS.WVista ? Program.Localization.Strings.Windows.WVista : Program.Localization.Strings.Windows.WXP), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if ((OS.W7 || OS.W8x) && File.Exists($"{SysPaths.System32}\\control.exe"))
            {
                Process.Start($"{SysPaths.System32}\\control.exe", "panel.dll, /name Microsoft.Troubleshooting");
            }
            else if (OS.W10 || OS.W11 || OS.W12)
            {
                Process.Start($"{SysPaths.Explorer}", "ms-settings:troubleshoot");
            }
            else if (OS.WVista || OS.WXP)
            {
                Process.Start("hh.exe", $"ms-its:{SysPaths.Windows}\\Help\\WindowsHelp.chm::/windows/troubleshoot.htm");
            }
        }
    }
}