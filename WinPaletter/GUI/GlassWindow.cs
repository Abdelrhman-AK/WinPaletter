using System;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{
    public class GlassWindow : Form
    {
        public GlassWindow()
        {
            BackColor = Color.Black;
            ControlBox = false;
            Font = new("Segoe UI", 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            TransparencyKey = Color.Black;
            WindowState = FormWindowState.Maximized;

            Load += new EventHandler(BK_Load);
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