using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.UI.WP
{
    public partial class TitlebarExtender : ContainerControl
    {
        public TitlebarExtender()
        {
            BackColor = Color.Black;
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            BackColor = Color.Black;

            base.OnBackColorChanged(e);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            if (!DesignMode)
            {
                Padding p = Padding.Empty;

                if (Dock == DockStyle.Top)
                {
                    p = new(0, Height, 0, 0);
                }
                else if (Dock == DockStyle.Bottom)
                {
                    p = new(0, 0, 0, Height);
                }
                else if (Dock == DockStyle.Left)
                {
                    p = new(Width, 0, 0, 0);
                }
                else if (Dock == DockStyle.Right)
                {
                    p = new(0, 0, Width, 0);
                }
                else if (Dock == DockStyle.Fill)
                {
                    p = new(0);
                }

                if (p != Padding.Empty) FindForm().DropEffect(p);
            }

            base.OnHandleCreated(e);
        }
    }
}
