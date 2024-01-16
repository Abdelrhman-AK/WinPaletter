using Microsoft.Win32;
using System;
using System.Windows.Forms;
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
                Registry.CurrentUser.DeleteSubKeyTree(@"Software\WinPaletter", false);
            }

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

            if (CheckBox2.Checked)
            {
                Forms.SysEventsSndsInstaller.Uninstall();

                try
                {
                    if (System.IO.Directory.Exists(PathsExt.appData))
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

                        try { System.IO.Directory.Delete(PathsExt.appData, true); }
                        catch { }
                    }
                }
                catch { }

                if (System.IO.Directory.Exists(PathsExt.ProgramFilesData))
                {
                    try { System.IO.Directory.Delete(PathsExt.ProgramFilesData, true); }
                    catch { }
                }
            }

            if (RadioImage1.Checked)
            {
            }
            // # Nothing

            else if (RadioImage2.Checked)
            {
                if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Theme.Manager TMx = new(Theme.Manager.Source.File, OpenFileDialog1.FileName);
                    TMx.Save(Theme.Manager.Source.Registry);

                    if (Program.Settings.ThemeApplyingBehavior.AutoRestartExplorer)
                        Program.RestartExplorer();

                    TMx.Dispose();
                }
            }
            else if (RadioImage3.Checked)
            {
                using (Manager _Def = Theme.Default.Get())
                {
                    _Def.Save(Theme.Manager.Source.Registry);
                    if (Program.Settings.ThemeApplyingBehavior.AutoRestartExplorer)
                        Program.RestartExplorer();
                }
            }

            string guidText = Application.ProductName;
            string RegPath = @"Software\Microsoft\Windows\CurrentVersion\Uninstall";
            Registry.CurrentUser.OpenSubKey(RegPath, true).DeleteSubKeyTree(guidText, false);


            Close();

            Environment.ExitCode = 0;

            Program.ForceExit();
        }
    }
}