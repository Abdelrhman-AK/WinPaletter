using System;
using System.Media;
using System.Windows.Forms;

namespace WinPaletter
{
    /// <summary>
    /// Theme license form for displaying the license of a theme.
    /// </summary>
    public partial class Store_ThemeLicense
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Store_ThemeLicense"/> class.
        /// </summary>
        public Store_ThemeLicense()
        {
            InitializeComponent();
        }

        private void Store_ThemeLicense_Load(object sender, EventArgs e)
        {
            this.Localize();
            ApplyStyle(this);
            TextBox1.Font = Fonts.ConsoleLarge;

            CustomSystemSounds.Exclamation.Play();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}