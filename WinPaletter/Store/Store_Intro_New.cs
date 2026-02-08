using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

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
            Icon = FormsExtensions.Icon<Store>();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (TablessControl1.SelectedIndex + 1 <= TablessControl1.TabPages.Count - 1)
                TablessControl1.SelectedIndex += 1;

            if ((Button1.Text ?? string.Empty) == (Program.Localization.Strings.General.Finish ?? string.Empty))
            {
                Close();
                TablessControl1.SelectedIndex = 0;
                Button1.Text = Program.Localization.Strings.General.Next;
            }

            if (TablessControl1.SelectedIndex == TablessControl1.TabPages.Count - 1)
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

        private void Button2_Click(object sender, EventArgs e)
        {
            if (TablessControl1.SelectedIndex - 1 >= 0)
                TablessControl1.SelectedIndex -= 1;
            Button1.Text = Program.Localization.Strings.General.Next;
            CheckBox1.Visible = false;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Process.Start(Links.Wiki.StoreCreateSource);
        }

        private void Store_Intro_New_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.Settings.Store.ShowNewXPIntro = CheckBox1.Checked;
            Program.Settings.Store.Save();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Process.Start(Links.Wiki.StoreUpload);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Process.Start(Links.Wiki.StoreExtension);
        }

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Links.Wiki.StoreBasics);
        }
    }
}