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
            WindowState = FormWindowState.Maximized;
        }

        const int LWA_COLORKEY = 0x1;
        const int LWA_ALPHA = 0x2;

        protected override void OnLoad(EventArgs e)
        {
            if ((OS.W7 || OS.WVista) && DWMAPI.IsCompositionEnabled())
            {
                // If the OS is Windows 7 or Vista and DWM is enabled, use Aero effect.
                DWMAPI.DWM_BLURBEHIND blurBehind = new(true);
                DWMAPI.DwmEnableBlurBehindWindow(Handle, blurBehind);
            }
            else if (!OS.WXP && !OS.W8x)
            {
                // If the OS is not Windows XP, 8 or 8.1 or even DWM composition is disabled, use Acrylic effect.
                this.DropEffect(Padding.Empty, false, DWM.BackdropStyles.Acrylic, true);
            }
            else
            {
                // If the OS is Windows XP, 8 or 8.1, use transparent gray effect.
                this.DropEffect(Padding.Empty, false, DWM.BackdropStyles.None);
            }

            base.OnLoad(e);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            const int GWL_EXSTYLE = -20;
            const int WS_EX_LAYERED = 0x80000;
            const int WS_EX_TRANSPARENT = 0x20;
            const int WS_EX_NOACTIVATE = 0x08000000;

            int exStyle = User32.GetWindowLong(Handle, GWL_EXSTYLE);
            exStyle |= WS_EX_LAYERED | WS_EX_TRANSPARENT | WS_EX_NOACTIVATE;
            User32.SetWindowLong(Handle, GWL_EXSTYLE, exStyle);

            // 50% transparent gray
            User32.SetLayeredWindowAttributes(Handle, 0, 128, LWA_ALPHA);
        }
    }
}