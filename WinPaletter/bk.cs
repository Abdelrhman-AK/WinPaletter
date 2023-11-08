using System;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{

    public partial class BK
    {
        public BK()
        {
            InitializeComponent();
        }
        private void BK_Load(object sender, EventArgs e)
        {
            if ((OS.W7 || OS.WVista) && DWMAPI.IsCompositionEnabled())
            {
                DWMAPI.DWM_BLURBEHIND blurBehind = new(true);
                DWMAPI.DwmEnableBlurBehindWindow(Handle, blurBehind);
            }
            else if (!OS.WXP && !OS.W8 && !OS.W81)
            {
                this.DropEffect(Padding.Empty, false, DWM.FormStyle.Acrylic);
            }
            else
            {
                this.DrawTransparentGray();
            }
        }
    }
}