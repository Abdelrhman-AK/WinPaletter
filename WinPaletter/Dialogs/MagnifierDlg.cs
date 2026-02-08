using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class MagnifierDlg : UI.WP.Form
    {
        private MouseHook _mouseHook;

        public MagnifierDlg()
        {
            InitializeComponent();
        }

        private void MagnifierDlg_Load(object sender, EventArgs e)
        {
            // Initialize mouse hook bound to this form
            _mouseHook = new(this);
            _mouseHook.MouseMoved += MouseHook_MouseMoved;
            _mouseHook.Enabled = true;

            // Enable the magnifier control
            magnifier1.Enabled = true;
        }


        private void MouseHook_MouseMoved(object sender, MouseEventArgs e)
        {
            Location = e.Location + new Size(10, 10);
        }

        private void MagnifierDlg_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Clean up
            _mouseHook?.MouseMoved -= MouseHook_MouseMoved;
            _mouseHook?.Dispose();
            magnifier1.Enabled = false;
        }
    }
}