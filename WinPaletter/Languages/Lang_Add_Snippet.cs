using System;
using System.Globalization;
using System.Windows.Forms;

namespace WinPaletter
{

    public partial class Lang_Add_Snippet
    {
        public Lang_Add_Snippet()
        {
            InitializeComponent();
        }
        private void Lang_Add_Snippet_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);

            DialogResult = DialogResult.None;

            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            ComboBox1.Items.Clear();
            ComboBox2.Items.Clear();

            foreach (CultureInfo culture in cultures)
            {
                if (!ComboBox1.Items.Contains(culture.NativeName) & !string.IsNullOrWhiteSpace(culture.NativeName))
                    ComboBox1.Items.Add(culture.NativeName);
                if (!ComboBox2.Items.Contains(culture.Name) & !string.IsNullOrWhiteSpace(culture.Name))
                    ComboBox2.Items.Add(culture.Name);
            }

            ComboBox1.SelectedItem = CultureInfo.CurrentCulture.NativeName;
            ComboBox2.SelectedItem = CultureInfo.CurrentCulture.Name;

        }

        public string _Result;

        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                CultureInfo ci = CultureInfo.GetCultureInfo(ComboBox2.SelectedItem.ToString());
                _Result = ci.TextInfo.IsRightToLeft.ToString();
            }
            catch
            {
                _Result = "False";
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            _Result = ComboBox1.SelectedItem.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            _Result = ComboBox2.SelectedItem.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}