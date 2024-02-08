using System;
using System.Media;
using System.Windows.Forms;

namespace WinPaletter.Dialogs
{
    public partial class ArgsHelp : Form
    {
        public ArgsHelp()
        {
            InitializeComponent();
        }

        private void ArgsHelp_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            Icon = Forms.MainForm.Icon;

            TextBox1.Font = Fonts.ConsoleMedium;
            SystemSounds.Exclamation.Play();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new() { Filter = Program.Filters.Text, Title = Program.Lang.Filter_SaveText })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllText(dlg.FileName, TextBox1.Text);
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}