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
            Label2.Text = string.Format(Program.Lang.WhatsNewInVersion, Program.Version);
            this.LoadLanguage();
            ApplyStyle(this);
            TabControl1.SelectedIndex = 0;
            Button1.Text = Program.Lang.Next;
            label45.Font = Fonts.ConsoleLarge;
            label45.Text = $"{TabControl1.SelectedIndex + 1}/{TabControl1.TabCount}";
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            Button2.Enabled = true;
            if (((UI.WP.Button)sender).Text == Program.Lang.OK)
            {
                Close();
            }
            else
            {
                if (TabControl1.SelectedIndex + 1 <= TabControl1.TabPages.Count - 1)
                    TabControl1.SelectedIndex += 1;
                if (TabControl1.SelectedIndex == TabControl1.TabPages.Count - 1)
                {
                    Button1.Text = Program.Lang.OK;
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Button1.Text = Program.Lang.Next;
            if (TabControl1.SelectedIndex > 0)
                TabControl1.SelectedIndex -= 1;
            if (TabControl1.SelectedIndex == 0)
                Button2.Enabled = false;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Process.Start(Links.Changelog);
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label45.Text = $"{TabControl1.SelectedIndex + 1}/{TabControl1.TabCount}";
        }
    }
}