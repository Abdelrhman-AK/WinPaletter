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
            WPStyle.ApplyStyle(this);
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
                My.MyProject.Application.DeleteFileAssociation(".wpth", "WinPaletter.ThemeFile");
                My.MyProject.Application.DeleteFileAssociation(".wpsf", "WinPaletter.SettingsFile");
                My.MyProject.Application.DeleteFileAssociation(".wptp", "WinPaletter.ThemeResourcesPack");
            }

            if (CheckBox3.Checked)
            {
                Registry.CurrentUser.DeleteSubKeyTree(@"Software\WinPaletter", false);
            }

            try
            {
                if (!My.Env.WXP && System.IO.File.Exists(My.Env.PATH_appData + @"\WindowsStartup_Backup.wav"))
                {
                    PE.ReplaceResource(My.Env.PATH_imageres, "WAV", My.Env.WVista ? 5051 : 5080, System.IO.File.ReadAllBytes(My.Env.PATH_appData + @"\WindowsStartup_Backup.wav"));
                }
            }
            catch
            {
            }

            if (CheckBox2.Checked)
            {
                if (System.IO.Directory.Exists(My.Env.PATH_appData))
                {
                    System.IO.Directory.Delete(My.Env.PATH_appData, true);
                    if (!My.Env.WXP)
                    {
                        Theme.Manager.ResetCursorsToAero();
                        if (My.Env.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                            Theme.Manager.ResetCursorsToAero(@"HKEY_USERS\.DEFAULT");
                    }

                    else
                    {
                        Theme.Manager.ResetCursorsToNone_XP();
                        if (My.Env.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                            Theme.Manager.ResetCursorsToNone_XP(@"HKEY_USERS\.DEFAULT");

                    }
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
                    if (My.Env.Settings.ThemeApplyingBehavior.AutoRestartExplorer)
                        RestartExplorer();
                    TMx.Dispose();
                }
            }
            else if (RadioImage3.Checked)
            {
                using (var _Def = Theme.Default.From(My.Env.PreviewStyle))
                {
                    _Def.Save(Theme.Manager.Source.Registry);
                    if (My.Env.Settings.ThemeApplyingBehavior.AutoRestartExplorer)
                        RestartExplorer();
                }
            }

            string guidText = My.MyProject.Application.Info.ProductName;
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