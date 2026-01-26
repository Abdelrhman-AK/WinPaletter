using System;
using System.IO;
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
            this.Localize();
            ApplyStyle(this);
            Icon = FormsExtensions.Icon<MainForm>();

            TextBox1.Font = Fonts.ConsoleMedium;
            CustomSystemSounds.Exclamation.Play();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new() { Filter = Program.Filters.Text, Title = Program.Localization.Strings.Extensions.SaveText })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(dlg.FileName, TextBox1.Text);
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}