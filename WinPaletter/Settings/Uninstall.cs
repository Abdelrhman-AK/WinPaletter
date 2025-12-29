using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using WinPaletter.Assets;
using WinPaletter.NativeMethods;
using WinPaletter.Properties;
using WinPaletter.Theme;

namespace WinPaletter
{
    /// <summary>
    /// Form for uninstalling the application
    /// </summary>
    public partial class Uninstall
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Uninstall"/> class.
        /// </summary>
        public Uninstall()
        {
            InitializeComponent();
        }
        private void Uninstall_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            Icon = Resources.Icon_Uninstall;

            if (OS.W12)
            {
                button11.Image = WinLogos.Win12.Resize(20, 20);
            }

            else if (OS.W11)
            {
                button11.Image = WinLogos.Win11.Resize(20, 20);
            }

            else if (OS.W10)
            {
                button11.Image = WinLogos.Win10.Resize(20, 20);
            }

            else if (OS.W81)
            {
                button11.Image = WinLogos.Win8_1.Resize(20, 20);
            }

            else if (OS.W8)
            {
                button11.Image = WinLogos.Win8.Resize(20, 20);
            }

            else if (OS.W7)
            {
                button11.Image = WinLogos.Win7.Resize(20, 20);
            }

            else if (OS.WVista)
            {
                button11.Image = WinLogos.WinVista.Resize(20, 20);
            }

            else if (OS.WXP)
            {
                button11.Image = WinLogos.WinXP.Resize(20, 20);
            }

            else
            {
                button11.Image = WinLogos.Win11.Resize(20, 20);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            // Unassociate file extensions
            if (CheckBox1.Checked)
            {
                Program.DeleteFileAssociation(".wpth", "WinPaletter.ThemeFile");
                Program.DeleteFileAssociation(".wpsf", "WinPaletter.SettingsFile");
                Program.DeleteFileAssociation(".wptp", "WinPaletter.ThemeResourcesPack");
            }

            // Delete the registry keys
            if (CheckBox3.Checked)
            {
                try { Registry.CurrentUser.DeleteSubKeyTree(@"Software\WinPaletter", false); }
                catch { Program.DeleteWinPaletteReg = false; }
                finally { Program.DeleteWinPaletteReg = true; }
            }

            // Restore the patched Windows startup sound if it was changed in imageres.dll
            if (checkBox4.Checked)
            {
                try
                {
                    if (!OS.WXP && File.Exists($"{SysPaths.appData}\\WindowsStartup_Backup.wav"))
                    {
                        string file = $"{SysPaths.appData}\\WindowsStartup_Backup.wav";

                        byte[] CurrentSoundBytes = PE.GetResource(SysPaths.imageres, "WAVE", OS.WVista ? 5051 : 5080);
                        byte[] TargetSoundBytes = File.ReadAllBytes(file);

                        if (!CurrentSoundBytes.Equals_Method2(TargetSoundBytes))
                        {
                            PE.ReplaceResource(SysPaths.imageres, "WAV", OS.WVista ? 5051 : 5080, TargetSoundBytes);
                        }
                    }
                }
                catch { } // Ignore restoring the sound if it fails
            }

            // Restore the cursors
            if (checkBox5.Checked)
            {
                if (!OS.WXP)
                {
                    Theme.Structures.Cursors.ResetCursorsToAero();
                    if (Program.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                        Theme.Structures.Cursors.ResetCursorsToAero(@"HKEY_USERS\.DEFAULT");
                }

                else
                {
                    Theme.Structures.Cursors.ResetCursorsToNone_XP();
                    if (Program.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                        Theme.Structures.Cursors.ResetCursorsToNone_XP(@"HKEY_USERS\.DEFAULT");

                }
            }

            // Uninstall the system events sounds service
            Forms.ServiceInstaller.Run("WinPaletter.SystemEventsSounds", Program.Lang.Strings.Services.Description_SysEventsSounds, SysPaths.SysEventsSounds, null, ServiceInstaller.RunMethods.Uninstall);

            // Delete the folders
            if (CheckBox2.Checked)
            {
                if (Directory.Exists(SysPaths.appData))
                {
                    try { Directory.Delete(SysPaths.appData, true); }
                    catch { } // Ignore deleting the folder if it fails
                }

                if (Directory.Exists(SysPaths.ProgramFilesData))
                {
                    try { Directory.Delete(SysPaths.ProgramFilesData, true); }
                    catch { } // Ignore deleting the folder if it fails
                }
            }

            // Restore system restore frequency
            DeleteValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\SystemRestore", "SystemRestorePointCreationFrequency");

            string guidText = Application.ProductName;

            DeleteKey($"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{guidText}");

            Program.UninstallDone = true;

            Close();

            Environment.ExitCode = 0;

            Program.ForceExit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tablessControl1.SelectedIndex = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tablessControl1.SelectedIndex = 2;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tablessControl1.SelectedIndex = 0;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            tablessControl1.SelectedIndex = 0;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Forms.BackupThemes_List.ShowDialog();
        }

        /// <summary>
        /// Restore a theme from a WinPaletter theme file before uninstalling
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, Title = Program.Lang.Strings.Extensions.OpenWinPaletterTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Manager TMx = new(Manager.Source.File, dlg.FileName))
                    {
                        TMx.Save(Manager.Source.Registry);

                        if (Program.Settings.ThemeApplyingBehavior.AutoRestartExplorer) Program.RestartExplorer();

                        MsgBox(Program.Lang.Strings.General.Done, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        /// <summary>
        /// Restore the default theme before uninstalling
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button11_Click(object sender, EventArgs e)
        {
            using (Manager _Def = Default.FromCurrentOS)
            {
                _Def.Save(Manager.Source.Registry, string.Empty, null, true);

                if (Program.Settings.ThemeApplyingBehavior.AutoRestartExplorer) Program.RestartExplorer();

                MsgBox(Program.Lang.Strings.General.Done, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Open the rescue tools form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button12_Click(object sender, EventArgs e)
        {
            Forms.SOS.ShowDialog();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Process.Start(Links.Issues);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Process.Start(Links.Wiki.WikiURL);
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox2.Checked)
            {
                checkBox4.Checked = true;
                checkBox5.Checked = true;
            }

            alertBox2.Visible = CheckBox2.Checked && (!checkBox4.Checked || !checkBox5.Checked);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            alertBox2.Visible = CheckBox2.Checked && (!checkBox4.Checked || !checkBox5.Checked);
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            alertBox2.Visible = CheckBox2.Checked && (!checkBox4.Checked || !checkBox5.Checked);
        }

        private void alertBox3_Click(object sender, EventArgs e)
        {
            button1.PerformClick();
        }

        private void alertBox4_Click(object sender, EventArgs e)
        {
            button3.PerformClick();
        }

        /// <summary>
        /// Restore correct metrics and fonts to fix issue of wrong metrics and fonts with a high DPI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button17_Click(object sender, EventArgs e)
        {
            using (Manager _Def = Default.FromCurrentOS)
            {
                _Def.MetricsFonts.Enabled = true;
                _Def.MetricsFonts.Apply();

                if (MsgBox(OS.WXP || OS.WVista || OS.W7 ? Program.Lang.Strings.Messages.LogoffQuestion : Program.Lang.Strings.Messages.SignOutQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question, Program.Lang.Strings.Messages.LogoffAlert1) == DialogResult.Yes)
                {
                    Forms.MainForm.LoggingOff = false;

                    IntPtr intPtr = IntPtr.Zero;
                    // Disable file system redirection to prevent the logoff.exe file from being redirected to SysWOW64
                    Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);
                    if (File.Exists($@"{SysPaths.System32}\logoff.exe"))
                    {
                        Forms.MainForm.LoggingOff = true;
                        Interaction.Shell($@"{SysPaths.System32}\logoff.exe", AppWinStyle.Hide);
                    }
                    else
                    {
                        MsgBox(string.Format(Program.Lang.Strings.Messages.LogoffNotFound, SysPaths.System32), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        /// <summary>
        /// Apply correct metrics and fonts to fix issue of wrong metrics and fonts with a high DPI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button18_Click(object sender, EventArgs e)
        {
            Program.TM.MetricsFonts.Enabled = false;
            Program.TM.Save(Manager.Source.Registry);

            Program.TM_FirstTime.MetricsFonts.Enabled = false;
            Program.TM_Original.MetricsFonts.Enabled = false;

            MsgBox(Program.Lang.Strings.General.Done, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Open the display settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button19_Click(object sender, EventArgs e)
        {
            if (!OS.WXP && !OS.WVista && !OS.W7 && !OS.W8x)
            {
                Program.SendCommand($"{SysPaths.Explorer} ms-settings:display");
            }
            else if (OS.WVista || OS.W7 || OS.W8x)
            {
                Program.SendCommand($"dpiscaling");
            }
            else
            {
                Program.SendCommand($"control.exe desk.cpl,@0,3");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tablessControl1.SelectedIndex = 2;
        }

        private void button16_Click(object sender, EventArgs e)
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
    }
}