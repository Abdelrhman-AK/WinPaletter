using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinPaletter
{

    public partial class EditInfo
    {
        public EditInfo()
        {
            InitializeComponent();
        }

        private void EditInfo_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            Load_Info(Program.TM);
            TextBox3.Font = Fonts.ConsoleMedium;
            TextBox6.Font = Fonts.ConsoleMedium;

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

        private void Button1_Click(object sender, EventArgs e)
        {
            {
                ref Localizer lang = ref Program.Lang;
                if (string.IsNullOrWhiteSpace(TextBox1.Text))
                {
                    MsgBox(lang.EmptyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(TextBox2.Text))
                {
                    MsgBox(lang.EmptyVer, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!TextBox2.Text.Replace(".", string.Empty).All(char.IsDigit))
                {
                    MsgBox(lang.WrongVerFormat, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(TextBox4.Text))
                {
                    MsgBox(lang.EmptyAuthorName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            Save_Info(Program.TM);

            Close();
        }

        public void Load_Info(Theme.Manager TM)
        {
            StoreItem1.TM = TM;
            TextBox1.Text = TM.Info.ThemeName;
            TextBox2.Text = TM.Info.ThemeVersion;
            TextBox3.Text = TM.Info.Description;
            TextBox4.Text = TM.Info.Author;
            TextBox5.Text = TM.Info.AuthorSocialMediaLink;
            TextBox6.Text = TM.Info.License;
            CheckBox7.Checked = TM.Info.ExportResThemePack;

            color1.BackColor = TM.Info.Color1;
            color2.BackColor = TM.Info.Color2;
            Trackbar1.Value = TM.Info.Pattern;

            CheckBox1.Checked = TM.Info.DesignedFor_Win11;
            CheckBox2.Checked = TM.Info.DesignedFor_Win10;
            CheckBox3.Checked = TM.Info.DesignedFor_Win81;
            CheckBox4.Checked = TM.Info.DesignedFor_Win7;
            CheckBox5.Checked = TM.Info.DesignedFor_WinVista;
            CheckBox6.Checked = TM.Info.DesignedFor_WinXP;
        }

        public void Save_Info(Theme.Manager TM)
        {
            TM.Info.ThemeName = string.Concat(TextBox1.Text.Split(System.IO.Path.GetInvalidFileNameChars())).Trim();
            TM.Info.ThemeVersion = TextBox2.Text;
            TM.Info.Description = TextBox3.Text;
            TM.Info.Author = TextBox4.Text;
            TM.Info.AuthorSocialMediaLink = TextBox5.Text;
            TM.Info.License = TextBox6.Text;
            TM.Info.ExportResThemePack = CheckBox7.Checked;

            TM.Info.Color1 = color1.BackColor;
            TM.Info.Color2 = color2.BackColor;
            TM.Info.Pattern = Trackbar1.Value;

            TM.Info.DesignedFor_Win11 = CheckBox1.Checked;
            TM.Info.DesignedFor_Win10 = CheckBox2.Checked;
            TM.Info.DesignedFor_Win81 = CheckBox3.Checked;
            TM.Info.DesignedFor_Win7 = CheckBox4.Checked;
            TM.Info.DesignedFor_WinVista = CheckBox5.Checked;
            TM.Info.DesignedFor_WinXP = CheckBox6.Checked;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Color1_2_DragDrop(object sender, DragEventArgs e)
        {
            StoreItem1.TM.Info.Color1 = color1.BackColor;
            StoreItem1.TM.Info.Color2 = color2.BackColor;
        }

        private void Color1_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                ((UI.Controllers.ColorItem)sender).BackColor = Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                StoreItem1.TM.Info.Color1 = ((UI.Controllers.ColorItem)sender).BackColor;
                return;
            }

            Dictionary<Control, string[]> CList = new()
            {
                { color1, new string[] { nameof(color1.BackColor) } },
                { StoreItem1, new string[] { nameof(StoreItem1.TM.Info.Color1) } },
            };

            Color C = Forms.ColorPickerDlg.Pick(CList);

            StoreItem1.TM.Info.Color1 = C;
            color1.BackColor = C;
            StoreItem1.Refresh();

            CList.Clear();
        }

        private void Color2_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                ((UI.Controllers.ColorItem)sender).BackColor = Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                StoreItem1.TM.Info.Color2 = ((UI.Controllers.ColorItem)sender).BackColor;
                return;
            }


            Dictionary<Control, string[]> CList = new()
            {
                { color2, new string[] { nameof(color2.BackColor) } },
                { StoreItem1, new string[] { nameof(StoreItem1.TM.Info.Color2) } },
            };

            Color C = Forms.ColorPickerDlg.Pick(CList);

            StoreItem1.TM.Info.Color2 = C;
            color2.BackColor = C;
            StoreItem1.Refresh();

            CList.Clear();
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!CheckAllOS())
                ((UI.WP.CheckBox)sender).Checked = true;
            try
            {
                if (StoreItem1.TM is not null)
                    StoreItem1.TM.Info.DesignedFor_Win11 = ((UI.WP.CheckBox)sender).Checked;
                StoreItem1.UpdateBadges();
            }
            catch
            {
            }
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!CheckAllOS())
                ((UI.WP.CheckBox)sender).Checked = true;
            try
            {
                if (StoreItem1.TM is not null)
                    StoreItem1.TM.Info.DesignedFor_Win10 = ((UI.WP.CheckBox)sender).Checked;
                StoreItem1.UpdateBadges();
            }
            catch
            {
            }
        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (!CheckAllOS())
                ((UI.WP.CheckBox)sender).Checked = true;
            try
            {
                if (StoreItem1.TM is not null)
                    StoreItem1.TM.Info.DesignedFor_Win81 = ((UI.WP.CheckBox)sender).Checked;
                StoreItem1.UpdateBadges();
            }
            catch
            {
            }
        }

        private void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (!CheckAllOS())
                ((UI.WP.CheckBox)sender).Checked = true;
            try
            {
                if (StoreItem1.TM is not null)
                    StoreItem1.TM.Info.DesignedFor_Win7 = ((UI.WP.CheckBox)sender).Checked;
                StoreItem1.UpdateBadges();
            }
            catch
            {
            }
        }

        private void CheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (!CheckAllOS())
                ((UI.WP.CheckBox)sender).Checked = true;
            try
            {
                if (StoreItem1.TM is not null)
                    StoreItem1.TM.Info.DesignedFor_WinVista = ((UI.WP.CheckBox)sender).Checked;
                StoreItem1.UpdateBadges();
            }
            catch
            {
            }
        }

        private void CheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (!CheckAllOS())
                ((UI.WP.CheckBox)sender).Checked = true;
            try
            {
                if (StoreItem1.TM is not null)
                    StoreItem1.TM.Info.DesignedFor_WinXP = ((UI.WP.CheckBox)sender).Checked;
                StoreItem1.UpdateBadges();
            }
            catch
            {
            }
        }

        public bool CheckAllOS()
        {
            return CheckBox1.Checked | CheckBox2.Checked | CheckBox3.Checked | CheckBox4.Checked | CheckBox5.Checked | CheckBox6.Checked;
        }

        private void Trackbar1_Scroll(object sender)
        {
            StoreItem1.UpdatePattern(Trackbar1.Value);
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            StoreItem1.TM.Info.ThemeName = ((UI.WP.TextBox)sender).Text;
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            StoreItem1.TM.Info.Author = ((UI.WP.TextBox)sender).Text;
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            StoreItem1.TM.Info.ThemeVersion = ((UI.WP.TextBox)sender).Text;
        }

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start($"{Properties.Resources.Link_Wiki}/Edit-theme-info");
        }

        private void pin_button_Click(object sender, EventArgs e)
        {
            Forms.MainFrm.tabsContainer1.AddFormIntoTab(this);
        }

        private void EditInfo_ParentChanged(object sender, EventArgs e)
        {
            if (this.Parent != null && Parent is TabPage)
            {
                pin_button.Visible = false;
            }
            else
            {
                pin_button.Visible = true;
            }
        }
    }
}