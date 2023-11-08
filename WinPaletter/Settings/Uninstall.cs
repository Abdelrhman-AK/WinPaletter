using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows.Forms;

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
                if (!OS.WXP && System.IO.File.Exists(PathsExt.appData + @"\WindowsStartup_Backup.wav"))
                {
                    PE.ReplaceResource(PathsExt.imageres, "WAV", OS.WVista ? 5051 : 5080, System.IO.File.ReadAllBytes(PathsExt.appData + @"\WindowsStartup_Backup.wav"));
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
                            if (Program.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                                Theme.Manager.ResetCursorsToAero(@"HKEY_USERS\.DEFAULT");
                        }

                        else
                        {
                            Theme.Manager.ResetCursorsToNone_XP();
                            if (Program.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
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
                    var TMx = new Theme.Manager(Theme.Manager.Source.File, OpenFileDialog1.FileName);
                    TMx.Save(Theme.Manager.Source.Registry);

                    if (Program.Settings.ThemeApplyingBehavior.AutoRestartExplorer)
                        Program.RestartExplorer();

                    TMx.Dispose();
                }
            }
            else if (RadioImage3.Checked)
            {
                using (var _Def = Theme.Default.Get())
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

            using (var Prc = Process.GetCurrentProcess())
            {
                Prc.Kill();
            }

        }

    }
}