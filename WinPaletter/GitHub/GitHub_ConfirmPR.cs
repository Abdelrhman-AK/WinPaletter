using System;
using System.Linq;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class GitHub_ConfirmPR : UI.WP.Form
    {
        public GitHub_ConfirmPR()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog(string branch)
        {
            label6.Text = branch;
            return base.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
