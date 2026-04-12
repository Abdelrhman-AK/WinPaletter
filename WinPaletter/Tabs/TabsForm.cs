using System;
using System.Windows.Forms;

namespace WinPaletter.Tabs
{
    /// <summary>
    /// The form that contains the tabs control.
    /// </summary>
    public partial class TabsForm : UI.WP.Form
    {
        //protected override void WndProc(ref Message m)
        //{
        //    base.WndProc(ref m);

        //    // Handle the WM_NCCALCSIZE message to adjust the client area
        //    if (m.Msg == 0x83 /*WM_NCCALCSIZE*/)
        //    {
        //        if (m.WParam.ToInt32() == 1 /*TRUE*/)
        //        {
        //            // Get the current non-client area rectangle
        //            NativeMethods.DWMAPI.MARGINS rect = (NativeMethods.DWMAPI.MARGINS)m.GetLParam(typeof(NativeMethods.DWMAPI.MARGINS));

        //            // Adjust the rectangle to make room for the custom control in the title bar
        //            rect.topHeight -= titlebarExtender1.Height - 6;

        //            // Update the non-client area rectangle
        //            System.Runtime.InteropServices.Marshal.StructureToPtr(rect, m.LParam, true);
        //        }
        //    }
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="TabsForm"/> class.
        /// </summary>
        public TabsForm()
        {
            InitializeComponent();
        }

        private void titlebarExtender1_DoubleClick(object sender, EventArgs e)
        {
            WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        }
    }
}