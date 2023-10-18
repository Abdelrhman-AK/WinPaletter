using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
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
            Program.processExplorer.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.CMD_Wrapper.SendCommand($"{Program.PATH_System32}\\taskkill.exe /F /IM explorer.exe");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Core.RestartExplorer();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (MsgBox(Program.Lang.LogoffQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question, Program.Lang.LogoffAlert1, "", "", "", "", Program.Lang.LogoffAlert2, Ookii.Dialogs.WinForms.TaskDialogIcon.Information) == DialogResult.Yes)
            {
                Forms.MainFrm.LoggingOff = true;
                IntPtr intPtr = IntPtr.Zero;
                Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);
                if (System.IO.File.Exists(Program.PATH_System32 + @"\logoff.exe"))
                {
                    Interaction.Shell(Program.PATH_System32 + @"\logoff.exe", AppWinStyle.Hide);
                }
                else
                {
                    MsgBox(string.Format(Program.Lang.LogoffNotFound, Program.PATH_System32), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                if (System.IO.File.Exists(Program.PATH_System32 + @"\shutdown.exe"))
                {
                    Interaction.Shell(Program.PATH_System32 + @"\shutdown.exe /r /t 0", AppWinStyle.Hide);
                }
                else
                {
                    MsgBox(string.Format(Program.Lang.ShutdownNotFound, Program.PATH_System32), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                if (System.IO.File.Exists(Program.PATH_System32 + @"\shutdown.exe"))
                {
                    Interaction.Shell(Program.PATH_System32 + @"\shutdown.exe /s /t 0", AppWinStyle.Hide);
                }
                else
                {
                    MsgBox(string.Format(Program.Lang.ShutdownNotFound, Program.PATH_System32), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Task.Run(new Action(() => { SFC(Program.PATH_imageres, false, false); }));
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Task.Run(new Action(() => { SFC("", true, false); }));
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Task.Run(new Action(() => 
            {

                IntPtr intPtr = IntPtr.Zero;
                Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);

                using (var process = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = Program.PATH_System32 + @"\dism.exe",
                        Verb = "runas",
                        UseShellExecute = true
                    }
                })
                {
                    process.StartInfo.Arguments = "/Online /Cleanup-Image /CheckHealth";
                    process.Start();
                    process.WaitForExit();

                    process.StartInfo.Arguments = "/Online /Cleanup-Image /ScanHealth";
                    process.Start();
                    process.WaitForExit();

                    process.StartInfo.Arguments = "/Online /Cleanup-Image /RestoreHealth";
                    process.Start();
                    process.WaitForExit();
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
