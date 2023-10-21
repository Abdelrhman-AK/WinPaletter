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
            ApplyStyle(this);
            TextBox1.Font = Fonts.ConsoleLarge;
            TextBox1.Text = Properties.Resources.LICENSE;
            Program.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Exclamation);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Program.Settings.General.LicenseAccepted = true;
            Program.Settings.General.Save();
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Program.Settings.General.LicenseAccepted = false;
            Program.Settings.General.Save();
            using (var Prc = Process.GetCurrentProcess())
            {
                Prc.Kill();
            }
        }
    }
}