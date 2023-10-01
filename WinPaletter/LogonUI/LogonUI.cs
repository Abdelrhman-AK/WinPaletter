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
            Load_FromCP(My.Env.CP);
            Button12.Image = My.MyProject.Forms.MainFrm.Button20.Image.Resize(16, 16);
        }

        public void Load_FromCP(CP ColorPalette)
        {
            LogonUI_Acrylic_Toggle.Checked = !ColorPalette.LogonUI10x.DisableAcrylicBackgroundOnLogon;
            LogonUI_Background_Toggle.Checked = !ColorPalette.LogonUI10x.DisableLogonBackgroundImage;
            LogonUI_Lockscreen_Toggle.Checked = !ColorPalette.LogonUI10x.NoLockScreen;
        }

        public void Save(CP ColorPalette)
        {
            ColorPalette.LogonUI10x.DisableAcrylicBackgroundOnLogon = !LogonUI_Acrylic_Toggle.Checked;
            ColorPalette.LogonUI10x.DisableLogonBackgroundImage = !LogonUI_Background_Toggle.Checked;
            ColorPalette.LogonUI10x.NoLockScreen = !LogonUI_Lockscreen_Toggle.Checked;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var CPx = new CP(CP.CP_Type.File, OpenFileDialog1.FileName);
                Load_FromCP(CPx);
                CPx.Dispose();
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            var CPx = new CP(CP.CP_Type.Registry);
            Load_FromCP(CPx);
            CPx.Dispose();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            CP CPx;
            switch (My.Env.PreviewStyle)
            {
                case WindowStyle.W11:
                    {
                        CPx = new CP_Defaults().Default_Windows11();
                        break;
                    }
                case WindowStyle.W10:
                    {
                        CPx = new CP_Defaults().Default_Windows10();
                        break;
                    }
                case WindowStyle.W81:
                    {
                        CPx = new CP_Defaults().Default_Windows81();
                        break;
                    }
                case WindowStyle.W7:
                    {
                        CPx = new CP_Defaults().Default_Windows7();
                        break;
                    }
                case WindowStyle.WVista:
                    {
                        CPx = new CP_Defaults().Default_WindowsVista();
                        break;
                    }
                case WindowStyle.WXP:
                    {
                        CPx = new CP_Defaults().Default_WindowsXP();
                        break;
                    }

                default:
                    {
                        CPx = new CP_Defaults().Default_Windows11();
                        break;
                    }
            }
            Load_FromCP(CPx);
            CPx.Dispose();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Save(My.Env.CP);
            Close();
        }

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(My.Resources.Link_Wiki + "/Edit-LogonUI-screen#windows-11--10");
        }
    }
}