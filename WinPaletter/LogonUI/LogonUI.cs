using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace WinPaletter
{

    public partial class LogonUI
    {
        public LogonUI()
        {
            InitializeComponent();
        }
        private void LogonUI_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            ApplyFromTM(Program.TM);
            Button12.Image = Forms.MainFrm.Button20.Image.Resize(16, 16);
        }

        public void ApplyFromTM(Theme.Manager TM)
        {
            LogonUI_Acrylic_Toggle.Checked = !TM.LogonUI10x.DisableAcrylicBackgroundOnLogon;
            LogonUI_Background_Toggle.Checked = !TM.LogonUI10x.DisableLogonBackgroundImage;
            LogonUI_Lockscreen_Toggle.Checked = !TM.LogonUI10x.NoLockScreen;
        }

        public void Save(Theme.Manager TM)
        {
            TM.LogonUI10x.DisableAcrylicBackgroundOnLogon = !LogonUI_Acrylic_Toggle.Checked;
            TM.LogonUI10x.DisableLogonBackgroundImage = !LogonUI_Background_Toggle.Checked;
            TM.LogonUI10x.NoLockScreen = !LogonUI_Lockscreen_Toggle.Checked;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Theme.Manager TMx = new(Theme.Manager.Source.File, OpenFileDialog1.FileName);
                ApplyFromTM(TMx);
                TMx.Dispose();
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            Theme.Manager TMx = new(Theme.Manager.Source.Registry);
            ApplyFromTM(TMx);
            TMx.Dispose();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            ApplyFromTM(Theme.Default.Get(Program.PreviewStyle));
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Save(Program.TM);
            Close();
        }

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start($"{Properties.Resources.Link_Wiki}/Edit-LogonUI-screen#windows-11--10");
        }
    }
}