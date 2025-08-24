using System;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{
    /// <summary>
    /// A form with glass effect using DWM API.
    /// </summary>
    public class GlassWindow : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GlassWindow"/> class.
        /// </summary>
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

            Load += new EventHandler(GlassWindow_Load);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Load" /> event.
        /// </summary>
        private void GlassWindow_Load(object sender, EventArgs e)
        {
            if ((OS.W7 || OS.WVista) && DWMAPI.IsCompositionEnabled())
            {
                // If the OS is Windows 7 or Vista and DWM is enabled, use Aero effect.
                DWMAPI.DWM_BLURBEHIND blurBehind = new(true);
                DWMAPI.DwmEnableBlurBehindWindow(Handle, blurBehind);
            }
            else if (!OS.WXP && !OS.W8 && !OS.W81)
            {
                // If the OS is not Windows XP, 8 or 8.1 or even DWM composition is disabled, use Acrylic effect.
                this.DropEffect(Padding.Empty, false, DWM.FormStyle.Acrylic);
            }
            else
            {
                // If the OS is Windows XP, 8 or 8.1, use transparent gray effect.
                this.DrawTransparentGray();
            }
        }
    }
}