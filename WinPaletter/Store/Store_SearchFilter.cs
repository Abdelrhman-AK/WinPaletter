using System;

namespace WinPaletter
{
    /// <summary>
    /// Search filter form for the store.
    /// </summary>
    public partial class Store_SearchFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Store_SearchFilter"/> class.
        /// </summary>
        public Store_SearchFilter()
        {
            InitializeComponent();
        }

        private void Store_SearchFilter_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            Icon = FormsExtensions.Icon<Store>();

            CheckBox1.Checked = Program.Settings.Store.Search_ThemeNames;
            CheckBox2.Checked = Program.Settings.Store.Search_AuthorsNames;
            CheckBox3.Checked = Program.Settings.Store.Search_Descriptions;

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Program.Settings.Store.Search_ThemeNames = CheckBox1.Checked;
            Program.Settings.Store.Search_AuthorsNames = CheckBox2.Checked;
            Program.Settings.Store.Search_Descriptions = CheckBox3.Checked;
            Program.Settings.Store.Save();
            Close();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}