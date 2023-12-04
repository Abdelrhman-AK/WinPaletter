using System;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{
    public partial class WinColors_XP : Form
    {
        public WinColors_XP()
        {
            InitializeComponent();
        }

        private void WinColors_XP_Load(object sender, EventArgs e)
        {
            DLLFunc.RemoveFormTitlebarTextAndIcon(Handle);
            ApplyStyle(this);
            pictureBox1.Image = this.Icon.ToBitmap();

            //btn_wp_theme.SplitMenuStrip.Items.Add("0", Properties.Resources.add_win12);
            //btn_wp_theme.SplitMenuStrip.Items.Add("1", Properties.Resources.add_win11);
            //btn_wp_theme.SplitMenuStrip.Items.Add("2", Properties.Resources.add_win10);
            //btn_wp_theme.SplitMenuStrip.Items.Add("3", Properties.Resources.add_win8);

        }
    }
}
