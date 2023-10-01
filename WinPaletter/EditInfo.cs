using Microsoft.VisualBasic;
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
            Load_Info(My.Env.CP);
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
                ref var temp = ref My.Env.Lang;
                if (string.IsNullOrWhiteSpace(TextBox1.Text))
                {
                    WPStyle.MsgBox(temp.EmptyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(TextBox2.Text))
                {
                    WPStyle.MsgBox(temp.EmptyVer, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!TextBox2.Text.Replace(".", "").All(char.IsDigit))
                {
                    WPStyle.MsgBox(temp.WrongVerFormat, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(TextBox4.Text))
                {
                    WPStyle.MsgBox(temp.EmptyAuthorName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


            Save_Info(My.Env.CP);
            My.MyProject.Forms.MainFrm.themename_lbl.Text = string.Format("{0} ({1})", My.Env.CP.Info.ThemeName, My.Env.CP.Info.ThemeVersion);
            My.MyProject.Forms.MainFrm.author_lbl.Text = string.Format("{0} {1}", My.Env.Lang.By, My.Env.CP.Info.Author);

            Close();
        }

        public void Load_Info(CP CP)
        {
            StoreItem1.CP = CP;
            TextBox1.Text = CP.Info.ThemeName;
            TextBox2.Text = CP.Info.ThemeVersion;
            TextBox3.Text = CP.Info.Description;
            TextBox4.Text = CP.Info.Author;
            TextBox5.Text = CP.Info.AuthorSocialMediaLink;
            TextBox6.Text = CP.Info.License;
            CheckBox7.Checked = CP.Info.ExportResThemePack;

            color1.BackColor = CP.Info.Color1;
            color2.BackColor = CP.Info.Color2;
            Trackbar1.Value = CP.Info.Pattern;

            CheckBox1.Checked = CP.Info.DesignedFor_Win11;
            CheckBox2.Checked = CP.Info.DesignedFor_Win10;
            CheckBox3.Checked = CP.Info.DesignedFor_Win81;
            CheckBox4.Checked = CP.Info.DesignedFor_Win7;
            CheckBox5.Checked = CP.Info.DesignedFor_WinVista;
            CheckBox6.Checked = CP.Info.DesignedFor_WinXP;
        }

        public void Save_Info(CP CP)
        {
            CP.Info.ThemeName = string.Concat(TextBox1.Text.Split(System.IO.Path.GetInvalidFileNameChars())).Trim();
            CP.Info.ThemeVersion = TextBox2.Text;
            CP.Info.Description = TextBox3.Text;
            CP.Info.Author = TextBox4.Text;
            CP.Info.AuthorSocialMediaLink = TextBox5.Text;
            CP.Info.License = TextBox6.Text;
            CP.Info.ExportResThemePack = CheckBox7.Checked;

            CP.Info.Color1 = color1.BackColor;
            CP.Info.Color2 = color2.BackColor;
            CP.Info.Pattern = Trackbar1.Value;

            CP.Info.DesignedFor_Win11 = CheckBox1.Checked;
            CP.Info.DesignedFor_Win10 = CheckBox2.Checked;
            CP.Info.DesignedFor_Win81 = CheckBox3.Checked;
            CP.Info.DesignedFor_Win7 = CheckBox4.Checked;
            CP.Info.DesignedFor_WinVista = CheckBox5.Checked;
            CP.Info.DesignedFor_WinXP = CheckBox6.Checked;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Color1_2_DragDrop(object sender, DragEventArgs e)
        {
            StoreItem1.CP.Info.Color1 = color1.BackColor;
            StoreItem1.CP.Info.Color2 = color2.BackColor;
        }

        private void Color1_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                ((UI.Controllers.ColorItem)sender).BackColor = My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                StoreItem1.CP.Info.Color1 = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                return;
            }

            var _conditions = new Conditions() { BackColor1 = true };
            var clist = new List<Control>() { color1, StoreItem1 };
            var c = My.MyProject.Forms.ColorPickerDlg.Pick(clist, _conditions);

            StoreItem1.CP.Info.Color1 = c;
            color1.BackColor = c;

            clist.Clear();
        }

        private void Color2_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                ((UI.Controllers.ColorItem)sender).BackColor = My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                StoreItem1.CP.Info.Color2 = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                return;
            }

            var _conditions = new Conditions() { BackColor2 = true };
            var clist = new List<Control>() { color2, StoreItem1 };
            var c = My.MyProject.Forms.ColorPickerDlg.Pick(clist, _conditions);

            StoreItem1.CP.Info.Color2 = c;
            color2.BackColor = c;

            clist.Clear();
        }

        private void CheckBox1_CheckedChanged(object sender)
        {
            if (!CheckAllOS())
                ((UI.WP.CheckBox)sender).Checked = true;
            try
            {
                if (StoreItem1.CP is not null)
                    StoreItem1.CP.Info.DesignedFor_Win11 = ((UI.WP.CheckBox)sender).Checked;
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
                if (StoreItem1.CP is not null)
                    StoreItem1.CP.Info.DesignedFor_Win10 = ((UI.WP.CheckBox)sender).Checked;
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
                if (StoreItem1.CP is not null)
                    StoreItem1.CP.Info.DesignedFor_Win81 = ((UI.WP.CheckBox)sender).Checked;
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
                if (StoreItem1.CP is not null)
                    StoreItem1.CP.Info.DesignedFor_Win7 = ((UI.WP.CheckBox)sender).Checked;
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
                if (StoreItem1.CP is not null)
                    StoreItem1.CP.Info.DesignedFor_WinVista = ((UI.WP.CheckBox)sender).Checked;
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
                if (StoreItem1.CP is not null)
                    StoreItem1.CP.Info.DesignedFor_WinXP = ((UI.WP.CheckBox)sender).Checked;
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
            StoreItem1.CP.Info.ThemeName = ((UI.WP.TextBox)sender).Text;
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            StoreItem1.CP.Info.Author = ((UI.WP.TextBox)sender).Text;
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            StoreItem1.CP.Info.ThemeVersion = ((UI.WP.TextBox)sender).Text;
        }

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(My.Resources.Link_Wiki + "/Edit-theme-info");
        }
    }
}