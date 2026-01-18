using System;
using System.Media;
using System.Windows.Forms;

namespace WinPaletter.Dialogs
{
    public partial class WinEffectsAlert : Form
    {
        public WinEffectsAlert()
        {
            InitializeComponent();
        }

        private void WinEffectsAlert_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            Icon = FormsExtensions.Icon<WinEffecter>();

            checkBox1.Checked = !Program.Settings.ThemeApplyingBehavior.Show_WinEffects_Alert;
            Forms.GlassWindow.Show();

            CustomSystemSounds.Exclamation.Play();

            BringToFront();
            Activate();
            Focus();
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

        private void WinEffectsAlert_FormClosing(object sender, FormClosingEventArgs e)
        {
            Forms.GlassWindow.Close();
            Program.Settings.ThemeApplyingBehavior.Show_WinEffects_Alert = !checkBox1.Checked;
            Program.Settings.ThemeApplyingBehavior.Save();
        }
    }
}
