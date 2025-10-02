using System;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter.UI.WP
{
    /// <summary>
    /// Represents a custom tab control that hides the tab headers, effectively creating a tabless control.
    /// </summary>
    /// <remarks>This control is a specialized version of <see cref="TabControl"/> that removes the visual
    /// display of tab headers, allowing developers to use the tab functionality without showing the tabs themselves. It
    /// is particularly useful for scenarios where the tabbed interface is managed programmatically or when a seamless
    /// UI is desired. <para> The control overrides certain behaviors of <see cref="TabControl"/> to achieve the tabless
    /// appearance, such as handling the <see cref="WndProc"/> method to suppress the rendering of tab headers.
    /// </para></remarks>
    public class TablessControl : System.Windows.Forms.TabControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TablessControl"/> class,  a custom control that supports double
        /// buffering and redraws itself when resized.
        /// </summary>
        /// <remarks>This control is designed to provide a smoother user experience by enabling double
        /// buffering  and custom painting. It is suitable for scenarios where flicker-free rendering is
        /// required.</remarks>
        public TablessControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            
            DoubleBuffered = true;

            // Prevent the built-in scroll buttons
            if (!DesignMode) Multiline = true;
        }

        /// <summary>
        /// Gets or sets the background color of the control.
        /// </summary>
        /// <remarks>Setting this property will cause the control to be redrawn to reflect the new
        /// background color.</remarks>
        public override Color BackColor
        {
            get => base.BackColor;
            set { base.BackColor = value; Invalidate(); }
        }

        private static IntPtr MakeLParam(int lo, int hi) => (IntPtr)((hi << 16) | (lo & 0xFFFF));

        /// <summary>
        /// Processes Windows messages sent to the control.
        /// </summary>
        /// <remarks>This method overrides the default message handling behavior to provide custom
        /// processing for specific messages. - For the `TCM_ADJUSTRECT` message, it adjusts the tab control's client
        /// area. - For the `WM_ERASEBKGND` message, it customizes the background erasure by filling the client area
        /// with the control's background color. For all other messages, the base implementation is invoked.</remarks>
        /// <param name="m">A <see cref="Message"/> object that represents the Windows message to process.</param>
        protected override void WndProc(ref Message m)
        {
            const int WM_ERASEBKGND = 0x14;
            const int TCM_ADJUSTRECT = 0x1328;
            const int TCM_SETPADDING = 0x132B;   // sets tab item padding
            const int WM_PAINT = 0x0F;

            // Hide the tab strip area completely
            if (!DesignMode && m.Msg == TCM_ADJUSTRECT)
            {
                m.Result = (IntPtr)1;
                return;
            }

            // Remove the up/down scroll buttons that Windows draws
            // by forcing the tab strip to have zero height before it paints.
            if (m.Msg == WM_PAINT)
            {
                User32.SendMessage(Handle, TCM_SETPADDING, IntPtr.Zero, MakeLParam(0, 1));
            }

            if (m.Msg == WM_ERASEBKGND)
            {
                using (var g = Graphics.FromHdc(m.WParam))
                using (var b = new SolidBrush(BackColor))
                    g.FillRectangle(b, ClientRectangle);

                m.Result = (IntPtr)1;
                return;
            }

            base.WndProc(ref m);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(Parent.BackColor);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Parent.BackColor);
        }

    }
}