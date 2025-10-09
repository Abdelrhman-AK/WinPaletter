using Ookii.Dialogs.WinForms;
using System;
using System.Reflection;
using System.Windows.Forms;
using WinPaletter.Properties;

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
            NativeMethods.Helpers.RemoveFormTitlebarTextAndIcon(Handle);
            Icon = FormsExtensions.Icon<MainForm>();

            labelAlt1.Text = Text;
            next_btn.Text = Program.Lang.Strings.General.Next;

            textBox1.Font = Fonts.ConsoleLarge;
            textBox1.Text = Resources.LICENSE;

            // Get the assembly of the current executing code
            Assembly assembly = Assembly.GetExecutingAssembly();

            // Get the AssemblyCopyrightAttribute
            AssemblyCopyrightAttribute copyrightAttribute = (AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyCopyrightAttribute));

            copyrightsLabel.Text = $"{copyrightAttribute.Copyright}, {Application.CompanyName}";

            Program.Settings.General.SetupCompleted = false;
            Program.Settings.General.Save();

            progressBar1.Maximum = (tablessControl1.TabCount - 1) * 100;

            LoadSettings();

            Forms.GlassWindow.Show();
        }

        void LoadSettings()
        {
            radioImage2.Checked = Program.Settings.ThemeApplyingBehavior.CreateSystemRestore;

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
            Program.Settings.ThemeApplyingBehavior.CreateSystemRestore = radioImage2.Checked;

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
            else if (tablessControl1.SelectedIndex == tablessControl1.TabCount - 2 && (checkBox1.Checked))
            {
                if (!OS.WXP)
                {
                    ProgressDialog dlg = new()
                    {
                        Animation = AnimationResource.GetShellAnimation(ShellAnimation.FlyingPapers),
                        Text = Program.Lang.Strings.General.RestorePoint_FirstTime_DialogTitle,
                        Description = Program.Lang.Strings.General.RestorePoint_FirstTime_Desc,
                        ProgressBarStyle = Ookii.Dialogs.WinForms.ProgressBarStyle.MarqueeProgressBar,
                        ShowCancelButton = false,
                        MinimizeBox = false,
                        WindowTitle = Application.ProductName,
                    };
                    dlg.DoWork += (s, args) =>
                    {
                        // Create a system restore point
                        SystemRestoreHelper.CreateRestorePoint(Program.Lang.Strings.General.RestorePoint_FirstTime);
                    };

                    dlg.ShowDialog();
                }
                else
                {
                    SystemRestoreHelper.CreateRestorePoint(Program.Lang.Strings.General.RestorePoint_FirstTime);
                }

                tablessControl1.SelectedIndex += 1;

                return;
            }

            // Enable or disable the next button based on the license tab and radio button state
            if (tablessControl1.SelectedIndex + 1 == 1 && !radioButton1.Checked) next_btn.Enabled = false;
            else next_btn.Enabled = true;

            Program.Animator.HideSync(tablessControl1);
            tablessControl1.SelectedIndex = tablessControl1.SelectedIndex + 1 > tablessControl1.TabCount - 1 ? tablessControl1.TabCount - 1 : tablessControl1.SelectedIndex + 1;
            Program.Animator.ShowSync(tablessControl1);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            next_btn.Enabled = true;
            next_btn.Text = tablessControl1.SelectedIndex >= tablessControl1.TabCount - 2 ? Program.Lang.Strings.General.Finish : Program.Lang.Strings.General.Next;

            Program.Animator.HideSync(tablessControl1);
            tablessControl1.SelectedIndex = tablessControl1.SelectedIndex - 1 < 0 ? 0 : tablessControl1.SelectedIndex - 1;
            Program.Animator.ShowSync(tablessControl1);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (!OS.WXP)
            {
                using (VistaFolderBrowserDialog FD = new() { SelectedPath = textBox2.Text })
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
            Forms.ServiceInstaller.Run("WinPaletter.SystemEventsSounds", Program.Lang.Strings.Services.Description_SysEventsSounds, SysPaths.SysEventsSounds, Resources.WinPaletter_SysEventsSounds, ServiceInstaller.RunMethods.Install);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Forms.ServiceInstaller.Run("WinPaletter.SystemEventsSounds", Program.Lang.Strings.Services.Description_SysEventsSounds, SysPaths.SysEventsSounds, null, ServiceInstaller.RunMethods.Uninstall);
        }

        private void tablessControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            back_btn.Enabled = tablessControl1.SelectedIndex > 0;
            next_btn.Text = tablessControl1.SelectedIndex >= tablessControl1.TabCount - 2 ? Program.Lang.Strings.General.Finish : Program.Lang.Strings.General.Next;

            progressBar1.Value = tablessControl1.SelectedIndex * 100;
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
            if (MsgBox(Program.Lang.Strings.Messages.ExitWinPaletter, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SaveSettings();
                Program.Settings.General.SetupCompleted = false;
                Program.Settings.General.Save();
                Application.Exit();
            }
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
