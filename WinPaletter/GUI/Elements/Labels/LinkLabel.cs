using System;
using System.ComponentModel;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter.UI.WP
{
    [Description("LinkLabel but with a proper hand cursor")]
    public class LinkLabel : System.Windows.Forms.LinkLabel
    {
        private const int IDC_HAND = 32649;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)User32.WindowsMessage.SetCursor)
            {
                int cursor = User32.LoadCursor(0, IDC_HAND);
                User32.SetCursor(cursor);
                m.Result = IntPtr.Zero;
                return;
            }

            base.WndProc(ref m);
        }
    }
}