using Ookii.Dialogs.WinForms;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.UI.WP;

namespace WinPaletter
{
    /// <summary>
    /// Introduction form for the store.
    /// </summary>
    public partial class Store_Intro_New
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Store_Intro_New"/> class.
        /// </summary>
        public Store_Intro_New()
        {
            InitializeComponent();
        }

        private void Store_Intro_New_Load(object sender, EventArgs e)
        {
            CheckBox1.Checked = Program.Settings.Store.ShowNewXPIntro;
            groupBox7.UseDecorationPattern = true;
            groupBox9.UseDecorationPattern = true;

            Forms.GlassWindow.Show(Forms.MainForm);

            RadioImage1.Checked = Program.Settings.Store.Mode == Settings.Structures.Store.Modes.Online;
            RadioImage2.Checked = Program.Settings.Store.Mode == Settings.Structures.Store.Modes.Offline;
            radioImage3.Checked = Program.Settings.Store.Mode == Settings.Structures.Store.Modes.Hybrid;

            ListBox1.Items.Clear();
            foreach (string x in Program.Settings.Store.Online_Repositories)
            {
                if (!string.IsNullOrWhiteSpace(x))
                    ListBox1.Items.Add(x);
            }
            ListBox2.Items.Clear();
            foreach (string x in Program.Settings.Store.Offline_Directories)
            {
                if (!string.IsNullOrWhiteSpace(x))
                    ListBox2.Items.Add(x);
            }
            CheckBox29.Checked = Program.Settings.Store.Offline_SubFolders;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (Button1.Text == Program.Localization.Strings.General.Finish)
            {
                Program.Settings.Store.Mode = RadioImage1.Checked ? Settings.Structures.Store.Modes.Online :
                    RadioImage2.Checked ? Settings.Structures.Store.Modes.Offline : Settings.Structures.Store.Modes.Hybrid;

                Program.Settings.Store.Online_Repositories = [.. ListBox1.Items.OfType<string>().Where(s => !string.IsNullOrEmpty(s))];
                Program.Settings.Store.Offline_Directories = [.. ListBox2.Items.OfType<string>().Where(s => !string.IsNullOrEmpty(s))];
                Program.Settings.Store.Offline_SubFolders = CheckBox29.Checked;
                Program.Settings.Store.Save();

                Close();
                tabControl1.SelectedIndex = 0;
                Button1.Text = Program.Localization.Strings.General.Next;
                return;
            }

            Program.Animator.HideSync(tabControl1);
            if (tabControl1.SelectedIndex + 1 <= tabControl1.TabPages.Count - 1) tabControl1.SelectedIndex += 1;
            Program.Animator.ShowSync(tabControl1);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Program.Animator.HideSync(tabControl1);
            if (tabControl1.SelectedIndex - 1 >= 0) tabControl1.SelectedIndex -= 1;
            Button1.Text = Program.Localization.Strings.General.Next;
            CheckBox1.Visible = false;
            Program.Animator.ShowSync(tabControl1);
        }

        private void Store_Intro_New_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.Settings.Store.ShowNewXPIntro = CheckBox1.Checked;
            Program.Settings.Store.Save();

            Forms.GlassWindow.Close();
        }

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Links.Wiki.StoreBasics);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Process.Start(Links.Wiki.StoreCreateSource);
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            string inputText = string.Empty;
            if (ListBox1.SelectedItem is not null)
                inputText = ListBox1.SelectedItem.ToString();
            string response = InputBox(Program.Localization.Strings.Messages.InputThemeRepos, inputText, Program.Localization.Strings.Messages.InputThemeRepos_Notice);
            if (!ListBox1.Items.Contains(response))
                ListBox1.Items.Add(response);
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            if (ListBox1.SelectedItem is not null)
            {
                int i = ListBox1.SelectedIndex;

                if (!((ListBox1.SelectedItem.ToString().ToUpper() ?? string.Empty) == (Links.Store_2ndDB.ToUpper() ?? string.Empty)) & !((ListBox1.SelectedItem.ToString().ToUpper() ?? string.Empty) == (Links.Store_MainDB.ToUpper() ?? string.Empty)))
                {
                    ListBox1.Items.RemoveAt(i);
                    if (i < ListBox1.Items.Count - 1)
                        ListBox1.SelectedIndex = i;
                    else
                        ListBox1.SelectedIndex = ListBox1.Items.Count - 1;
                }
                else
                {
                    MsgBox(Program.Localization.Strings.Store.RemoveTip, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            if (!OS.WXP)
            {
                VistaFolderBrowserDialog dlg = new();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (!ListBox2.Items.Contains(dlg.SelectedPath))
                        ListBox2.Items.Add(dlg.SelectedPath);
                }
                dlg.Dispose();
            }
            else
            {
                using (FolderBrowserDialog dlg = new())
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (!ListBox2.Items.Contains(dlg.SelectedPath)) ListBox2.Items.Add(dlg.SelectedPath);
                    }
                }
            }
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            if (ListBox2.SelectedItem is not null)
            {
                int i = ListBox2.SelectedIndex;
                ListBox2.Items.RemoveAt(i);
                if (i < ListBox2.Items.Count - 1)
                    ListBox2.SelectedIndex = i;
                else
                    ListBox2.SelectedIndex = ListBox2.Items.Count - 1;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == tabControl1.TabPages.Count - 1)
            {
                Button1.Text = Program.Localization.Strings.General.Finish;
                CheckBox1.Visible = true;
            }
            else
            {
                Button1.Text = Program.Localization.Strings.General.Next;
                CheckBox1.Visible = false;
            }
        }
    }
}