using System;
using System.Media;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class Store_ThemeLicense
    {
        public Store_ThemeLicense()
        {
            InitializeComponent();
        }

        private void Store_ThemeLicense_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            TextBox1.Font = Fonts.ConsoleLarge;
            Icon = Forms.LicenseForm.Icon;
            SystemSounds.Exclamation.Play();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}