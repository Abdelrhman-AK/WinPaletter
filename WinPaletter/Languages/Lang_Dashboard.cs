using System;
using System.ComponentModel;
using System.Diagnostics;

namespace WinPaletter
{
    public partial class Lang_Dashboard
    {
        public Lang_Dashboard()
        {
            InitializeComponent();
        }
        private void Lang_Dashboard_Load(object sender, EventArgs e)
        {
            Icon = Forms.Lang_JSON_Manage.Icon;
            this.LoadLanguage();
            ApplyStyle(this);
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Forms.Lang_JSON_Manage.ShowDialog();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Forms.Lang_JSON_Update.ShowDialog();
        }

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start($"{Properties.Resources.Link_Wiki}/Language-creation");
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Forms.Lang_JSON_GUI.ShowDialog();
        }
    }
}