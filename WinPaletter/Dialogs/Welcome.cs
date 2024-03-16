using System;
using System.Linq;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            using (MainForm formIcon = new()) { Icon = formIcon.Icon; }
            Button1.Text = Program.Lang.Next;

            this.LoadLanguage();
            ApplyStyle(this);

            Forms.GlassWindow.Show();

            checkBox1.Checked = Program.Settings.Miscellaneous.ShowWelcomeDialog;
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
                if (tabControl1.SelectedIndex + 1 <= tabControl1.TabPages.Count - 1)
                    tabControl1.SelectedIndex += 1;
                if (tabControl1.SelectedIndex == tabControl1.TabPages.Count - 1)
                {
                    Button1.Text = Program.Lang.OK;
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Button1.Text = Program.Lang.Next;
            if (tabControl1.SelectedIndex > 0)
                tabControl1.SelectedIndex -= 1;
            if (tabControl1.SelectedIndex == 0)
                Button2.Enabled = false;
        }

        private void btn_history_Click(object sender, EventArgs e)
        {
            Forms.BackupThemes_List.ShowDialog();
        }

        private void Welcome_FormClosing(object sender, FormClosingEventArgs e)
        {
            Forms.GlassWindow.Close();
            Program.Settings.Miscellaneous.ShowWelcomeDialog = checkBox1.Checked;
            Program.Settings.Miscellaneous.Save();
        }
    }
}
