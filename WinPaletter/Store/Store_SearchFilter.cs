using System;

namespace WinPaletter
{
    public partial class Store_SearchFilter
    {
        public Store_SearchFilter()
        {
            InitializeComponent();
        }

        private void Store_SearchFilter_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            WPStyle.ApplyStyle(this);
            Icon = My.MyProject.Forms.Store.Icon;

            CheckBox1.Checked = My.Env.Settings.Store.Search_ThemeNames;
            CheckBox2.Checked = My.Env.Settings.Store.Search_AuthorsNames;
            CheckBox3.Checked = My.Env.Settings.Store.Search_Descriptions;

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            My.Env.Settings.Store.Search_ThemeNames = CheckBox1.Checked;
            My.Env.Settings.Store.Search_AuthorsNames = CheckBox2.Checked;
            My.Env.Settings.Store.Search_Descriptions = CheckBox3.Checked;
            My.Env.Settings.Store.Save();
            Close();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}