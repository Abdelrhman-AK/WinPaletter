using System;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class WindowsTerminalDecide
    {
        public WindowsTerminalDecide()
        {
            InitializeComponent();
        }

        private void WindowsTerminalDecide_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            Icon = Forms.WindowsTerminal.Icon;
            var c = PictureBox1.Image.AverageColor();
            var c1 = c.CB((float)(Program.Style.DarkMode ? -0.35d : 0.35d));
            var c2 = c.CB((float)(Program.Style.DarkMode ? -0.75d : 0.75d));
            Panel1.BackColor = c1;
            BackColor = c2;
            Program.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Exclamation);
            RadioImage1.Checked = Forms.WindowsTerminal.SaveState == WinTerminal.Version.Stable;
            RadioImage2.Checked = Forms.WindowsTerminal.SaveState == WinTerminal.Version.Preview;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (RadioImage1.Checked)
                Forms.WindowsTerminal.SaveState = WinTerminal.Version.Stable;
            if (RadioImage2.Checked)
                Forms.WindowsTerminal.SaveState = WinTerminal.Version.Preview;
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