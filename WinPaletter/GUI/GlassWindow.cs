using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter
{
    /// <summary>
    /// A form with glass effect using DWM API.
    /// </summary>
    public class GlassWindow : Form
    {
        private bool shownOverAParent = false;

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
            StartPosition = FormStartPosition.Manual;
            WindowState = FormWindowState.Maximized;
        }

        const int LWA_COLORKEY = 0x1;
        const int LWA_ALPHA = 0x2;

        public void ShowWithGlassFocusedOnParent(Form parent)
        {
            if (parent is null)
            {
                shownOverAParent = false;
                WindowState = FormWindowState.Maximized;
                Show();
                return;
            }

            shownOverAParent = true;

            // Acrlyic glass size fixer
            int fixer_Width = OS.W10 || OS.W11 || OS.W12 ? 15 : 0;
            int fixer_Height = OS.W10 || OS.W11 || OS.W12 ? 7 : 0;

            // Match size and position
            WindowState = FormWindowState.Normal;
            Location = parent.Location + new Size(fixer_Width / 2, 0);
            Size = parent.Size - new Size(fixer_Width, fixer_Height);

            // Make this glass window owned by the parent
            // So it stays above parent but below any dialog of parent
            Owner = parent;

            // Show without activating or changing Z-order
            Show();

            // Ensure it’s directly above parent but not topmost globally
            User32.SetWindowPos(Handle, parent.Handle, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
        }

        const uint SWP_NOSIZE = 0x0001;
        const uint SWP_NOMOVE = 0x0002;
        const uint SWP_NOACTIVATE = 0x0010;

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
                this.DropEffect(Padding.Empty, shownOverAParent, DWM.DWMStyles.Acrylic, true);

                if (shownOverAParent)
                {
                    // Make the form have rounded corners if the operating system is Windows 11 or 12
                    // It should be used as a fallback for the custom styling. Make both start by 'If' statement, not 'Else If'
                    if (OS.W12 || OS.W11)
                    {
                        bool useRoundedCorners = Program.Settings.Appearance.ManagedByTheme && Program.Settings.Appearance.CustomColors && !OS.WXP && !OS.WVista && !OS.W7 && !OS.W8x && !OS.W10;

                        int argpvAttribute = (int)DWMAPI.FormCornersType.Round;
                        DWMAPI.DwmSetWindowAttribute(Handle, DWMAPI.DWMWINDOWATTRIBUTE.WINDOW_CORNER_PREFERENCE, ref argpvAttribute, Marshal.SizeOf(typeof(int)));

                        // Apply rectangular window corners if custom styling is enabled and rounded corners are disabled
                        // Make both start by 'If' statement, not 'Else If'
                        if (useRoundedCorners && !Program.Settings.Appearance.RoundedCorners)
                        {
                            int argpvAttribute1 = (int)DWMAPI.FormCornersType.Rectangular;
                            DWMAPI.DwmSetWindowAttribute(Handle, DWMAPI.DWMWINDOWATTRIBUTE.WINDOW_CORNER_PREFERENCE, ref argpvAttribute1, Marshal.SizeOf(typeof(int)));
                        }
                    }
                }
            }
            else
            {
                // If the OS is Windows XP, 8 or 8.1, use transparent gray effect.
                this.DropEffect(Padding.Empty, shownOverAParent, DWM.DWMStyles.None);
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