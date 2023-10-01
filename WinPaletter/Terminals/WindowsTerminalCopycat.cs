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
            WPStyle.ApplyStyle(this);
            Icon = My.MyProject.Forms.WindowsTerminal.Icon;

            try
            {
                ComboBox1.SelectedIndex = 0;
            }
            catch
            {
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ComboBox1.SelectedItem.ToString()))
            {
                My.MyProject.Forms.WindowsTerminal.CCat = ComboBox1.SelectedItem.ToString();
                DialogResult = DialogResult.OK;
            }
            else
            {
                My.MyProject.Forms.WindowsTerminal.CCat = null;
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