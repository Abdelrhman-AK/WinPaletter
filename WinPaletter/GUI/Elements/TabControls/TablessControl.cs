using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    [Designer(typeof(TransparentTabPagesHostDesigner))]
    [Description("TabControl but without tabs for WinPaletter UI")]
    public class TablessControl : System.Windows.Forms.TabControl
    {
        public new TransparentTabPageCollection TabPages { get; set; }

        public TablessControl()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;
            TabPages = new(this);
        }

        protected override void WndProc(ref Message m)
        {
            if (!DesignMode && m.Msg == 0x1328)
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