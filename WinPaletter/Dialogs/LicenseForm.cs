using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class LicenseForm
    {
        public LicenseForm()
        {
            InitializeComponent();
        }
        private void LicenseForm_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            WPStyle.ApplyStyle(this);
            TextBox1.Font = My.MyProject.Application.ConsoleFontLarge;
            TextBox1.Text = My.Resources.LICENSE;
            My.MyProject.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Exclamation);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            My.Env.Settings.General.LicenseAccepted = true;
            My.Env.Settings.General.Save();
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            My.Env.Settings.General.LicenseAccepted = false;
            My.Env.Settings.General.Save();
            using (var Prc = Process.GetCurrentProcess())
            {
                Prc.Kill();
            }
        }
    }
}