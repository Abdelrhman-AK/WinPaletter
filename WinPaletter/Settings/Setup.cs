using System;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{
    public partial class Setup : Form
    {
        public Setup()
        {
            InitializeComponent();
        }

        private void Setup_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            DLLFunc.RemoveFormTitlebarTextAndIcon(Handle);
            Icon = FormsExtensions.Icon<MainForm>();

            labelAlt1.Text = Text;
            next_btn.Text = Program.Lang.Strings.General.Next;

            textBox1.Font = Fonts.ConsoleLarge;
            textBox1.Text = Properties.Resources.LICENSE;

            // Get the assembly of the current executing code
            Assembly assembly = Assembly.GetExecutingAssembly();

            // Get the AssemblyCopyrightAttribute
            AssemblyCopyrightAttribute copyrightAttribute = (AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyCopyrightAttribute));

            copyrightsLabel.Text = $"{copyrightAttribute.Copyright}, {Application.CompanyName}";

            Program.Settings.General.SetupCompleted = false;
            Program.Settings.General.Save();

            LoadSettings();

            Forms.GlassWindow.Show();
        }

        void LoadSettings()
        {
            radioImage1.Checked = Program.Settings.ThemeApplyingBehavior.CreateSystemRestore;

            toggle31.Checked = Program.Settings.BackupTheme.Enabled;
            textBox2.Text = Program.Settings.BackupTheme.BackupPath;
            toggle33.Checked = Program.Settings.BackupTheme.AutoBackupOnAppOpen;
            toggle32.Checked = Program.Settings.BackupTheme.AutoBackupOnApply;
            toggle36.Checked = Program.Settings.BackupTheme.AutoBackupOnApplySingleAspect;
            toggle34.Checked = Program.Settings.BackupTheme.AutoBackupOnThemeLoad;
            toggle39.Checked = Program.Settings.BackupTheme.AutoBackupOnExError;

            toggle8.Checked = Program.Settings.ThemeApplyingBehavior.AutoRestartExplorer;
            toggle12.Checked = Program.Settings.ThemeApplyingBehavior.Show_WinEffects_Alert;

            groupBox50.Enabled = toggle31.Checked;
            groupBox51.Enabled = toggle31.Checked;
        }

        void SaveSettings()
        {
            Program.Settings.ThemeApplyingBehavior.CreateSystemRestore = radioImage1.Checked | radioImage2.Checked;

            Program.Settings.BackupTheme.Enabled = toggle31.Checked;
            Program.Settings.BackupTheme.BackupPath = textBox2.Text;
            Program.Settings.BackupTheme.AutoBackupOnAppOpen = toggle33.Checked;
            Program.Settings.BackupTheme.AutoBackupOnApply = toggle32.Checked;
            Program.Settings.BackupTheme.AutoBackupOnApplySingleAspect = toggle36.Checked;
            Program.Settings.BackupTheme.AutoBackupOnThemeLoad = toggle34.Checked;
            Program.Settings.BackupTheme.AutoBackupOnExError = toggle39.Checked;

            Program.Settings.ThemeApplyingBehavior.AutoRestartExplorer = toggle8.Checked;
            Program.Settings.ThemeApplyingBehavior.Show_WinEffects_Alert = toggle12.Checked;

            Program.Settings.Save(Settings.Source.Registry);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (tablessControl1.SelectedIndex == tablessControl1.TabCount - 1)
            {
                SaveSettings();

                Program.Settings.General.SetupCompleted = true;
                Program.Settings.General.Save();

                DialogResult = DialogResult.OK;
                Close();
                return;
            }
            else if (tablessControl1.SelectedIndex == tablessControl1.TabCount - 2 && (radioImage1.Checked || radioImage3.Checked))
            {
                Task.Run(() =>
                {
                    Invoke(() =>
                    {
                        progressBar1.Visible = true;
                        label9.Visible = true;
                        next_btn.Enabled = false;
                        back_btn.Enabled = false;
                        button1.Enabled = false;
                        ControlBox = false;
                    });

                    SystemRestoreHelper.CreateRestorePoint(Program.Lang.Strings.General.RestorePoint_FirstTime);

                    Invoke(() =>
                    {
                        progressBar1.Visible = false;
                        label9.Visible = false;
                        next_btn.Enabled = true;
                        back_btn.Enabled = true;
                        button1.Enabled = true;
                        ControlBox = true;
                    });
                });
            }

            tablessControl1.SelectedIndex = tablessControl1.SelectedIndex + 1 > tablessControl1.TabCount - 1 ? tablessControl1.TabCount - 1 : tablessControl1.SelectedIndex + 1;

            // Enable or disable the next button based on the license tab and radio button state
            if (tablessControl1.SelectedIndex == 1 && !radioButton1.Checked) next_btn.Enabled = false;
            else next_btn.Enabled = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            tablessControl1.SelectedIndex = tablessControl1.SelectedIndex - 1 < 0 ? 0 : tablessControl1.SelectedIndex - 1;
            next_btn.Enabled = true;
            next_btn.Text = tablessControl1.SelectedIndex >= tablessControl1.TabCount - 2 ? Program.Lang.Strings.General.Finish : Program.Lang.Strings.General.Next;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (!OS.WXP)
            {
                using (Ookii.Dialogs.WinForms.VistaFolderBrowserDialog FD = new() { SelectedPath = textBox2.Text })
                {
                    if (FD.ShowDialog() == DialogResult.OK) textBox2.Text = FD.SelectedPath;
                }
            }
            else
            {
                using (FolderBrowserDialog FD = new() { SelectedPath = textBox2.Text })
                {
                    if (FD.ShowDialog() == DialogResult.OK) textBox2.Text = FD.SelectedPath;
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Forms.ServiceInstaller.Run("WinPaletter.SystemEventsSounds", Program.Lang.Strings.Services.Description_SysEventsSounds, SysPaths.SysEventsSounds, Properties.Resources.WinPaletter_SysEventsSounds, ServiceInstaller.RunMethods.Install);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Forms.ServiceInstaller.Run("WinPaletter.SystemEventsSounds", Program.Lang.Strings.Services.Description_SysEventsSounds, SysPaths.SysEventsSounds, null, ServiceInstaller.RunMethods.Uninstall);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            Forms.SecureUxTheme_Setup.ShowDialog();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            Process.Start(Links.SecureUxThemeReleases);
        }

        private void tablessControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            back_btn.Enabled = tablessControl1.SelectedIndex > 0;
            next_btn.Text = tablessControl1.SelectedIndex >= tablessControl1.TabCount - 2 ? Program.Lang.Strings.General.Finish : Program.Lang.Strings.General.Next;
        }

        private void radioButtons_CheckedChanged(object sender, EventArgs e)
        {
            if (tablessControl1.SelectedIndex == 1)
            {
                // Enable or disable the next button based on the license tab and radio button state
                next_btn.Enabled = radioButton1.Checked;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            Program.Settings.General.SetupCompleted = false;
            Program.Settings.General.Save();

            Close();
        }

        private void Setup_FormClosed(object sender, FormClosedEventArgs e)
        {
            Forms.GlassWindow.Close();
        }

        private void toggle31_CheckedChanged(object sender, EventArgs e)
        {
            groupBox50.Enabled = toggle31.Checked;
            groupBox51.Enabled = toggle31.Checked;
        }
    }
}
