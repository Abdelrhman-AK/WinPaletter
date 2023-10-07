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
            WPStyle.ApplyStyle(this);
            Load_Info(My.Env.TM);
            TextBox3.Font = My.MyProject.Application.ConsoleFontMedium;
            TextBox6.Font = My.MyProject.Application.ConsoleFontMedium;

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
                ref Localizer lang = ref My.Env.Lang;
                if (string.IsNullOrWhiteSpace(TextBox1.Text))
                {
                    WPStyle.MsgBox(lang.EmptyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(TextBox2.Text))
                {
                    WPStyle.MsgBox(lang.EmptyVer, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!TextBox2.Text.Replace(".", "").All(char.IsDigit))
                {
                    WPStyle.MsgBox(lang.WrongVerFormat, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(TextBox4.Text))
                {
                    WPStyle.MsgBox(lang.EmptyAuthorName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


            Save_Info(My.Env.TM);
            My.MyProject.Forms.MainFrm.themename_lbl.Text = string.Format("{0} ({1})", My.Env.TM.Info.ThemeName, My.Env.TM.Info.ThemeVersion);
            My.MyProject.Forms.MainFrm.author_lbl.Text = string.Format("{0} {1}", My.Env.Lang.By, My.Env.TM.Info.Author);

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
                ((UI.Controllers.ColorItem)sender).BackColor = My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                StoreItem1.TM.Info.Color1 = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                return;
            }

            var _conditions = new Conditions() { BackColor1 = true };
            var clist = new List<Control>() { color1, StoreItem1 };
            var c = My.MyProject.Forms.ColorPickerDlg.Pick(clist, _conditions);

            StoreItem1.TM.Info.Color1 = c;
            color1.BackColor = c;

            clist.Clear();
        }

        private void Color2_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                ((UI.Controllers.ColorItem)sender).BackColor = My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                StoreItem1.TM.Info.Color2 = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                return;
            }

            var _conditions = new Conditions() { BackColor2 = true };
            var clist = new List<Control>() { color2, StoreItem1 };
            var c = My.MyProject.Forms.ColorPickerDlg.Pick(clist, _conditions);

            StoreItem1.TM.Info.Color2 = c;
            color2.BackColor = c;

            clist.Clear();
        }

        private void CheckBox1_CheckedChanged(object sender)
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

        private void CheckBox2_CheckedChanged(object sender)
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

        private void CheckBox3_CheckedChanged(object sender)
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

        private void CheckBox4_CheckedChanged(object sender)
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

        private void CheckBox5_CheckedChanged(object sender)
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

        private void CheckBox6_CheckedChanged(object sender)
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
            Process.Start(Properties.Resources.Link_Wiki + "/Edit-theme-info");
        }
    }
}