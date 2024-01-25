using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Icon = Forms.MainFrm.Icon;

            TextBox1.Font = Fonts.ConsoleMedium;
            Program.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Exclamation);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
               System.IO.File.WriteAllText(SaveFileDialog1.FileName, TextBox1.Text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}