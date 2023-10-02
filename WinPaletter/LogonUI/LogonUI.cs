using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using static WinPaletter.PreviewHelpers;

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
            WPStyle.ApplyStyle(this);
            ApplyFromTM(My.Env.TM);
            Button12.Image = My.MyProject.Forms.MainFrm.Button20.Image.Resize(16, 16);
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
                var TMx = new Theme.Manager(Theme.Manager.Source.File, OpenFileDialog1.FileName);
                ApplyFromTM(TMx);
                TMx.Dispose();
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            var TMx = new Theme.Manager(Theme.Manager.Source.Registry);
            ApplyFromTM(TMx);
            TMx.Dispose();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            Theme.Manager TMx;
            switch (My.Env.PreviewStyle)
            {
                case WindowStyle.W11:
                    {
                        TMx = new Theme.Default().Windows11();
                        break;
                    }
                case WindowStyle.W10:
                    {
                        TMx = new Theme.Default().Windows10();
                        break;
                    }
                case WindowStyle.W81:
                    {
                        TMx = new Theme.Default().Windows81();
                        break;
                    }
                case WindowStyle.W7:
                    {
                        TMx = new Theme.Default().Windows7();
                        break;
                    }
                case WindowStyle.WVista:
                    {
                        TMx = new Theme.Default().WindowsVista();
                        break;
                    }
                case WindowStyle.WXP:
                    {
                        TMx = new Theme.Default().WindowsXP();
                        break;
                    }

                default:
                    {
                        TMx = new Theme.Default().Windows11();
                        break;
                    }
            }
            ApplyFromTM(TMx);
            TMx.Dispose();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Save(My.Env.TM);
            Close();
        }

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(My.Resources.Link_Wiki + "/Edit-LogonUI-screen#windows-11--10");
        }
    }
}