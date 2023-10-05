using System;
using System.Diagnostics;

namespace WinPaletter
{
    public partial class Whatsnew
    {
        public Whatsnew()
        {
            InitializeComponent();
        }
        private void Tutorial_Load(object sender, EventArgs e)
        {
            Label2.Text = string.Format(My.Env.Lang.WhatsNewInVersion, My.Env.AppVersion);
            this.LoadLanguage();
            WPStyle.ApplyStyle(this);
            TabControl1.SelectedIndex = 0;
            Button1.Text = My.Env.Lang.Next;
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            Button2.Enabled = true;

            if (((UI.WP.Button)sender).Text == My.Env.Lang.OK)
            {
                Close();
            }
            else
            {
                if (TabControl1.SelectedIndex + 1 <= TabControl1.TabPages.Count - 1)
                    TabControl1.SelectedIndex += 1;
                if (TabControl1.SelectedIndex == TabControl1.TabPages.Count - 1)
                {
                    Button1.Text = My.Env.Lang.OK;
                }
            }

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Button1.Text = My.Env.Lang.Next;
            if (TabControl1.SelectedIndex > 0)
                TabControl1.SelectedIndex -= 1;
            if (TabControl1.SelectedIndex == 0)
                Button2.Enabled = false;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Resources.Link_Changelog);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Resources.Link_Wiki + "/Color-picker-control#3-drag-and-drop");
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Resources.Link_Wiki + "/Palette-generator");
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Resources.Link_Wiki + "/Color-picker-control#4-previous-colors-like-undo-or-colors-history");
            Process.Start(Properties.Resources.Link_Wiki + "/Color-picker-control#1-cut-copy-paste-and-undo");
            Process.Start(Properties.Resources.Link_Wiki + "/Color-picker-control#6-color-history");
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Resources.Link_Wiki + "/Language-creation");
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Resources.Link_Wiki + "/Theme-log-verbose-level");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Resources.Link_Wiki + "/Get-WinPaletter");
        }
    }
}