using System;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{
    public partial class AspectsTemplate : Form
    {
        public AspectsTemplate()
        {
            InitializeComponent();
        }

        private void AspectsTemplate_Load(object sender, EventArgs e)
        {
            DLLFunc.RemoveFormTitlebarTextAndIcon(Handle);
            ApplyStyle(this);
            pictureBox1.Image = this.Icon.ToBitmap();
        }

        private void btn_wp_theme_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            MsgBox(e.ClickedItem.Tag);
        }

    }
}
