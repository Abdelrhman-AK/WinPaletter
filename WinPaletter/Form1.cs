using System;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ApplyStyle(this);
            //BackColor = Color.Black;
            //this.DropEffect();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Visible = false;

            Program.Settings.Appearance.AutoDarkMode = false;
            Program.Settings.Appearance.DarkMode = !Program.Settings.Appearance.DarkMode;
            Program.Settings.Appearance.Save();

            Program.Style = new(DefaultColors.PrimaryColor, DefaultColors.SecondaryColor, DefaultColors.TertiaryColor, DefaultColors.DisabledColor, DefaultColors.BackColorDark, DefaultColors.DisabledBackColor, Program.Settings.Appearance.DarkMode, true);

            GetRoundedCorners();
            GetDarkMode();
            ApplyStyle(this);

            //BackColor = Color.Black;
            //this.DropEffect();

            Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Forms.MainFrm.Show();
        }

    }
}