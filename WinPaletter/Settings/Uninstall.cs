using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.Theme;

namespace WinPaletter
{
    public partial class Uninstall
    {
        public Uninstall()
        {
            InitializeComponent();
        }
        private void Uninstall_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            Icon = Properties.Resources.Icon_Uninstall;

            if (OS.W12)
            {
                button11.Image = Assets.WinLogos.Win12.Resize(20, 20);
            }

            else if (OS.W11)
            {
                button11.Image = Assets.WinLogos.Win11.Resize(20, 20);
            }

            else if (OS.W10)
            {
                button11.Image = Assets.WinLogos.Win10.Resize(20, 20);
            }

            else if (OS.W8x)
            {
                button11.Image = Assets.WinLogos.Win81.Resize(20, 20);
            }

            else if (OS.W7)
            {
                button11.Image = Assets.WinLogos.Win7.Resize(20, 20);
            }

            else if (OS.WVista)
            {
                button11.Image = Assets.WinLogos.WinVista.Resize(20, 20);
            }

            else if (OS.WXP)
            {
                button11.Image = Assets.WinLogos.WinXP.Resize(20, 20);
            }

            else
            {
                button11.Image = Assets.WinLogos.Win11.Resize(20, 20);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (CheckBox1.Checked)
            {
                Program.DeleteFileAssociation(".wpth", "WinPaletter.ThemeFile");
                Program.DeleteFileAssociation(".wpsf", "WinPaletter.SettingsFile");
                Program.DeleteFileAssociation(".wptp", "WinPaletter.ThemeResourcesPack");
            }

            if (CheckBox3.Checked)
            {
                try { Registry.CurrentUser.DeleteSubKeyTree(@"Software\WinPaletter", false); }
                catch { Program.DeleteWinPaletteReg = false; }
                finally { Program.DeleteWinPaletteReg = true; }
            }

            if (checkBox4.Checked)
            {
                try
                {
                    if (!OS.WXP && System.IO.File.Exists($"{PathsExt.appData}\\WindowsStartup_Backup.wav"))
                    {
                        string file = $"{PathsExt.appData}\\WindowsStartup_Backup.wav";

                        byte[] CurrentSoundBytes = PE.GetResource(PathsExt.imageres, "WAVE", OS.WVista ? 5051 : 5080);
                        byte[] TargetSoundBytes = System.IO.File.ReadAllBytes(file);

                        if (!CurrentSoundBytes.Equals_Method2(TargetSoundBytes))
                        {
                            PE.ReplaceResource(PathsExt.imageres, "WAV", OS.WVista ? 5051 : 5080, TargetSoundBytes);
                        }
                    }
                }
                catch { }
            }

            if (checkBox5.Checked)
            {
                if (!OS.WXP)
                {
                    Theme.Manager.ResetCursorsToAero();
                    if (Program.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                        Theme.Manager.ResetCursorsToAero(@"HKEY_USERS\.DEFAULT");
                }

                else
                {
                    Theme.Manager.ResetCursorsToNone_XP();
                    if (Program.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                        Theme.Manager.ResetCursorsToNone_XP(@"HKEY_USERS\.DEFAULT");

                }
            }

            if (CheckBox2.Checked)
            {
                if (System.IO.Directory.Exists(PathsExt.appData))
                {
                    try { System.IO.Directory.Delete(PathsExt.appData, true); }
                    catch { }
                }

                if (System.IO.Directory.Exists(PathsExt.ProgramFilesData))
                {
                    try { System.IO.Directory.Delete(PathsExt.ProgramFilesData, true); }
                    catch { }
                }
            }

            Forms.SysEventsSndsInstaller.Uninstall();

            string guidText = Application.ProductName;
            string RegPath = @"Software\Microsoft\Windows\CurrentVersion\Uninstall";
            Registry.CurrentUser.OpenSubKey(RegPath, true).DeleteSubKeyTree(guidText, false);

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

        private void button10_Click(object sender, EventArgs e)
        {
            using (System.Windows.Forms.OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, Title = Program.Lang.Filter_OpenWinPaletterTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Theme.Manager TMx = new(Theme.Manager.Source.File, dlg.FileName))
                    {
                        TMx.Save(Theme.Manager.Source.Registry);

                        if (Program.Settings.ThemeApplyingBehavior.AutoRestartExplorer) Program.RestartExplorer();

                        MsgBox(Program.Lang.Done, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            using (Manager _Def = Theme.Default.Get())
            {
                _Def.Save(Theme.Manager.Source.Registry, string.Empty, null, true);

                if (Program.Settings.ThemeApplyingBehavior.AutoRestartExplorer) Program.RestartExplorer();

                MsgBox(Program.Lang.Done, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Forms.RescueTools.ShowDialog();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Process.Start($"{Properties.Resources.Link_Repository}issues");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Resources.Link_Wiki);
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

        private void button17_Click(object sender, EventArgs e)
        {
            using (Manager _Def = Theme.Default.Get())
            {
                _Def.MetricsFonts.Enabled = true;
                _Def.MetricsFonts.Apply();

                if (MsgBox(Program.Lang.LogoffQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question, Program.Lang.LogoffAlert1, string.Empty, string.Empty, string.Empty, string.Empty, Program.Lang.LogoffAlert2, Ookii.Dialogs.WinForms.TaskDialogIcon.Information) == DialogResult.Yes)
                {
                    Forms.Home.LoggingOff = true;
                    IntPtr intPtr = IntPtr.Zero;
                    Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);
                    if (System.IO.File.Exists($@"{PathsExt.System32}\logoff.exe"))
                    {
                        Interaction.Shell($@"{PathsExt.System32}\logoff.exe", AppWinStyle.Hide);
                    }
                    else
                    {
                        MsgBox(string.Format(Program.Lang.LogoffNotFound, PathsExt.System32), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Program.TM.MetricsFonts.Enabled = false;
            Program.TM.Save(Manager.Source.Registry);

            Program.TM_FirstTime.MetricsFonts.Enabled = false;
            Program.TM_Original.MetricsFonts.Enabled = false;

            MsgBox(Program.Lang.Done, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (!OS.WXP && !OS.WVista && !OS.W7 && !OS.W8 && !OS.W81)
            {
                Program.SendCommand($"{PathsExt.Explorer} ms-settings:display");
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
    }
}