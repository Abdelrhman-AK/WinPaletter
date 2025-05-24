using System;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class WindowsTerminalCopycat
    {
        public WindowsTerminalCopycat()
        {
            InitializeComponent();
        }
        private void WindowsTerminalCopycat_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            Icon = FormsExtensions.Icon<WindowsTerminal>();

            if (ComboBox1.Items.Count > 0) { ComboBox1.SelectedIndex = 0; }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ComboBox1.SelectedItem.ToString()))
            {
                Forms.WindowsTerminal.CCat = ComboBox1.SelectedItem.ToString();
                DialogResult = DialogResult.OK;
            }
            else
            {
                Forms.WindowsTerminal.CCat = null;
                DialogResult = DialogResult.Cancel;
            }

            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}