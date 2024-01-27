using System;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class Win32UI_Fullscreen : Form
    {
        public Win32UI_Fullscreen()
        {
            InitializeComponent();
            retroDesktopColors1.LoadMetrics(Program.TM);
            retroDesktopColors1.LoadColors(Forms.Win32UI.retroDesktopColors1);
        }

        private void Win32UI_Fullscreen_Load(object sender, EventArgs e)
        {
            retroDesktopColors1.LoadMetrics(Program.TM);
            retroDesktopColors1.LoadColors(Forms.Win32UI.retroDesktopColors1);
            KeyPreview = true;
        }

        private void Win32UI_Fullscreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }

        private void retroDesktopColors1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }

        private void retroDesktopColors1_EditorInvoker(object sender, UI.Retro.EditorEventArgs e)
        {
            Forms.Win32UI.retroDesktopColors1_EditorInvoker(sender, e);
        }
    }
}
