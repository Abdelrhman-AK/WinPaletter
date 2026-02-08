using System;
using System.Windows.Forms;
using WinPaletter.UI.Retro;

namespace WinPaletter
{
    public partial class Win32UI_Fullscreen : UI.WP.Form
    {
        public Win32UI_Fullscreen()
        {
            InitializeComponent();
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

        private void retroDesktopColors1_EditorInvoker(object sender, EditorEventArgs e)
        {
            Forms.Win32UI.retroDesktopColors1_EditorInvoker(sender, e);
        }
    }
}
