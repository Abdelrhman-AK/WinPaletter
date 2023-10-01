using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{

    public partial class LogonUIXP
    {
        public LogonUIXP()
        {
            InitializeComponent();
        }
        private void LogonUIXP_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            WPStyle.ApplyStyle(this);
            Icon = My.MyProject.Forms.LogonUI.Icon;
            Button12.Image = My.MyProject.Forms.MainFrm.Button20.Image.Resize(16, 16);
            ApplyFromCP(My.Env.CP);
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (e.Data.GetData(typeof(UI.Controllers.ColorItem).FullName) is UI.Controllers.ColorItem)
            {
                Focus();
                BringToFront();
            }
            else
            {
                return;
            }

            base.OnDragOver(e);
        }

        public void ApplyFromCP(CP CP)
        {
            {
                ref var temp = ref CP.LogonUIXP;
                Toggle1.Checked = temp.Enabled;
                switch (temp.Mode)
                {
                    case CP.Structures.LogonUIXP.Modes.Default:
                        {
                            RadioImage1.Checked = true;
                            break;
                        }
                    case CP.Structures.LogonUIXP.Modes.Win2000:
                        {
                            RadioImage2.Checked = true;
                            break;
                        }

                    default:
                        {
                            RadioImage1.Checked = true;
                            break;
                        }
                }
                color_pick.BackColor = temp.BackColor;
                CheckBox1.Checked = temp.ShowMoreOptions;
            }
        }

        public void ApplyToCP(CP CP)
        {
            {
                ref var temp = ref CP.LogonUIXP;
                temp.Enabled = Toggle1.Checked;
                if (RadioImage1.Checked)
                    temp.Mode = CP.Structures.LogonUIXP.Modes.Default;
                else
                    temp.Mode = CP.Structures.LogonUIXP.Modes.Win2000;
                temp.BackColor = color_pick.BackColor;
                temp.ShowMoreOptions = CheckBox1.Checked;
            }
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var CPx = new CP(CP.CP_Type.File, OpenFileDialog1.FileName);
                ApplyFromCP(CPx);
                CPx.Dispose();
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            var CPx = new CP(CP.CP_Type.Registry);
            ApplyFromCP(CPx);
            CPx.Dispose();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            using (var _Def = CP_Defaults.From(My.Env.PreviewStyle))
            {
                ApplyFromCP(_Def);
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var CPx = new CP(CP.CP_Type.Registry);
            ApplyToCP(CPx);
            ApplyToCP(My.Env.CP);
            CPx.LogonUIXP.Apply();
            CPx.Dispose();
            Cursor = Cursors.Default;
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            ApplyToCP(My.Env.CP);
            Close();
        }

        private void color_pick_Click(object sender, EventArgs e)
        {
            if (e is DragEventArgs)
                return;

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                return;
            }

            var CList = new List<Control>() { (Control)sender };
            var C = My.MyProject.Forms.ColorPickerDlg.Pick(CList);
            ((UI.Controllers.ColorItem)sender).BackColor = Color.FromArgb(255, C);
            CList.Clear();

        }

        private void Toggle1_CheckedChanged(object sender, EventArgs e)
        {
            checker_img.Image = Conversions.ToBoolean(((UI.WP.Toggle)sender).Checked) ? My.Resources.checker_enabled : My.Resources.checker_disabled;
        }

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(My.Resources.Link_Wiki + "/Edit-LogonUI-screen#windows-xp");
        }
    }
}