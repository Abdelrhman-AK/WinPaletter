using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.Theme;

namespace WinPaletter
{
    public partial class LogonUIXP
    {
        public LogonUIXP()
        {
            InitializeComponent();
            color_pick.DragDrop += ColorItem_DragDrop;
            FormClosing += LogonUIXP_FormClosing;
        }

        private void LogonUIXP_FormClosing(object sender, FormClosingEventArgs e)
        {
            color_pick.DragDrop -= ColorItem_DragDrop;
        }

        private void LogonUIXP_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            Icon = Forms.LogonUI.Icon;
            Button12.Image = Forms.MainFrm.Button20.Image.Resize(16, 16);
            ApplyFromTM(Program.TM);
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

        public void ApplyFromTM(Theme.Manager TM)
        {
            Toggle1.Checked = TM.LogonUIXP.Enabled;
            switch (TM.LogonUIXP.Mode)
            {
                case Theme.Structures.LogonUIXP.Modes.Default:
                    {
                        RadioImage1.Checked = true;
                        break;
                    }
                case Theme.Structures.LogonUIXP.Modes.Win2000:
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

            color_pick.BackColor = TM.LogonUIXP.BackColor;
            CheckBox1.Checked = TM.LogonUIXP.ShowMoreOptions;
            UpdateWin2000Preview(TM.LogonUIXP.BackColor);
        }

        public void ApplyToTM(Theme.Manager TM)
        {
            {
                TM.LogonUIXP.Enabled = Toggle1.Checked;

                if (RadioImage1.Checked)
                    TM.LogonUIXP.Mode = Theme.Structures.LogonUIXP.Modes.Default;
                else
                    TM.LogonUIXP.Mode = Theme.Structures.LogonUIXP.Modes.Win2000;

                TM.LogonUIXP.BackColor = color_pick.BackColor;
                TM.LogonUIXP.ShowMoreOptions = CheckBox1.Checked;
            }
        }

        private void UpdateWin2000Preview(Color color)
        {
            using (Bitmap b = new(Properties.Resources.LogonUI_2000.Width, Properties.Resources.LogonUI_2000.Height))
            {
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.Clear(color);
                    g.DrawImage(Properties.Resources.LogonUI_2000, 0, 0);
                    g.Save();
                }
                RadioImage2.Image = (Bitmap)b.Clone();
            }
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
            using (Manager _Def = Theme.Default.Get(Program.PreviewStyle))
            {
                ApplyFromTM(_Def);
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Theme.Manager TMx = new(Theme.Manager.Source.Registry);
            ApplyToTM(TMx);
            ApplyToTM(Program.TM);
            TMx.LogonUIXP.Apply();
            TMx.Dispose();
            Cursor = Cursors.Default;
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            ApplyToTM(Program.TM);
            Close();
        }

        private void color_pick_Click(object sender, EventArgs e)
        {
            if (e is DragEventArgs)
                return;

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                UpdateWin2000Preview(Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender));
                return;
            }

            List<Control> CList = new() { (Control)sender };
            Color C = Forms.ColorPickerDlg.Pick(CList);
            ((UI.Controllers.ColorItem)sender).BackColor = Color.FromArgb(255, C);
            UpdateWin2000Preview(C);
            CList.Clear();
        }

        private void ColorItem_DragDrop(object sender, DragEventArgs e)
        {
            UpdateWin2000Preview(color_pick.BackColor);
        }

        private void Toggle1_CheckedChanged(object sender, EventArgs e)
        {
            checker_img.Image = Conversions.ToBoolean(((UI.WP.Toggle)sender).Checked) ? Properties.Resources.checker_enabled : Properties.Resources.checker_disabled;
        }

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start($"{Properties.Resources.Link_Wiki}/Edit-LogonUI-screen#windows-xp");
        }
    }
}