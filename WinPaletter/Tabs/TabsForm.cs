using System;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter.Tabs
{
    public partial class TabsForm : Form
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
        //            DWMAPI.RECT rect = (DWMAPI.RECT)m.GetLParam(typeof(DWMAPI.RECT));

        //            // Adjust the rectangle to make room for the custom control in the title bar
        //            rect.top -= tabsContainer1.Height - 6;

        //            // Update the non-client area rectangle
        //            Marshal.StructureToPtr(rect, m.LParam, true);
        //        }
        //    }
        //}

        public TabsForm()
        {
            InitializeComponent();
        }

        private void TabsForm_Load(object sender, EventArgs e)
        {
            //ControlBox = false;
            DLLFunc.RemoveFormTitlebarTextAndIcon(Handle);
            this.LoadLanguage();
            ApplyStyle(this);

            CheckForIllegalCrossThreadCalls = false;
        }

        private void tabsContainer1_DoubleClick(object sender, EventArgs e)
        {
            WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        }
    }
}