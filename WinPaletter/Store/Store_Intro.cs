using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace WinPaletter
{

    public partial class Store_Intro
    {
        public Store_Intro()
        {
            InitializeComponent();
        }
        private void Store_Intro_Load(object sender, EventArgs e)
        {
            CheckBox1.Checked = My.Env.Settings.Store.ShowTips;
            this.LoadLanguage();
            WPStyle.ApplyStyle(this);
            Icon = My.MyProject.Forms.Store.Icon;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (TablessControl1.SelectedIndex + 1 <= TablessControl1.TabPages.Count - 1)
                TablessControl1.SelectedIndex += 1;

            if ((Button1.Text ?? "") == (My.Env.Lang.Finish ?? ""))
            {
                Close();
                TablessControl1.SelectedIndex = 0;
                Button1.Text = My.Env.Lang.Next;
            }

            if (TablessControl1.SelectedIndex == TablessControl1.TabPages.Count - 1)
            {
                Button1.Text = My.Env.Lang.Finish;
                CheckBox1.Visible = true;
            }
            else
            {
                Button1.Text = My.Env.Lang.Next;
                CheckBox1.Visible = false;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (TablessControl1.SelectedIndex - 1 >= 0)
                TablessControl1.SelectedIndex -= 1;
            Button1.Text = My.Env.Lang.Next;
            CheckBox1.Visible = false;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Resources.Link_StoreOnlineSourceCreation);
        }

        private void Store_Intro_FormClosing(object sender, FormClosingEventArgs e)
        {
            My.Env.Settings.Store.ShowTips = CheckBox1.Checked;
            My.Env.Settings.Store.Save();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Resources.Link_StoreUpload);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Resources.Link_StoreSourcesExtension);
        }

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Properties.Resources.Link_Wiki + "/WinPaletter-Store-basics");
        }
    }
}