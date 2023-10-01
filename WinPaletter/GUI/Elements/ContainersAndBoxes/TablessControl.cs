using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{

    [Description("TabControl but without tabs for WinPaletter UI")]
    public class TablessControl : System.Windows.Forms.TabControl
    {
        public TablessControl()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x1328 && !DesignMode)
            {
                m.Result = (IntPtr)1;
            }
            else
            {
                base.WndProc(ref m);
            }
        }

    }

}