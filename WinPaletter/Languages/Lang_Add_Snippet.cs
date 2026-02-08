using System;
using System.Globalization;
using System.Windows.Forms;

namespace WinPaletter
{
    /// <summary>
    /// A form that allows the user to select a language name as a snippet from a list of all available languages.
    /// </summary>
    public partial class Lang_Add_Snippet
    {
        /// <summary>
        /// The cultures available on the system.
        /// </summary>
        readonly CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

        /// <summary>
        /// The result of the form.
        /// </summary>
        public string result;

        /// <summary>
        /// Initializes a new instance of the <see cref="Lang_Add_Snippet"/> class.
        /// </summary>
        public Lang_Add_Snippet()
        {
            InitializeComponent();
        }

        private void Lang_Add_Snippet_Load(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;

            ComboBox1.Items.Clear();

            foreach (CultureInfo culture in cultures)
            {
                if (!ComboBox1.Items.Contains(culture.NativeName) & !string.IsNullOrWhiteSpace(culture.NativeName)) ComboBox1.Items.Add(culture.NativeName);
            }

            ComboBox1.SelectedItem = CultureInfo.CurrentCulture.NativeName;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            result = ComboBox1.SelectedItem.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}