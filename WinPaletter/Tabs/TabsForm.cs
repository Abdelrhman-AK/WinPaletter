using System.Windows.Forms;
using WinPaletter.NativeMethods;

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
        //    if (m.Msg == (int)User32.WindowsMessage.NCCalcSize /*WM_NCCALCSIZE*/)
        //    {
        //        if (m.WParam.ToInt32() == 1 /*TRUE*/)
        //        {
        //            // Get the current non-client area rectangle
        //            UxTheme.RECT rect = (UxTheme.RECT)m.GetLParam(typeof(UxTheme.RECT));

        //            // Adjust the rectangle to make room for the custom control in the title bar
        //            rect.top -= tabsContainer1.Height - 4;

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

        /// <summary>
        /// Adds the specified Form to the tabs container as a new tab.
        /// </summary>
        /// <param name="form">The Form to add as a new tab.</param>
        public void AddTab(Form form)
        {
            if (form == null || form.IsDisposed) return;
            tabsContainer1?.AddFormIntoTab(form);
        }

        private void TabsForm_Localized()
        {
            tabsContainer1.UpdateMenuStrings();
        }
    }
}