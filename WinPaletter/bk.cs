using System;
using System.Windows.Forms;

namespace WinPaletter
{

    public partial class BK
    {
        public BK()
        {
            InitializeComponent();
        }
        private void BK_Load(object sender, EventArgs e)
        {
            if (OS.W7 | OS.WVista)
                FormBorderStyle = FormBorderStyle.Sizable;

            if (!OS.WVista)
            {
                this.DrawDWMEffect(Padding.Empty, false, FormDWMEffects.FormStyle.Acrylic);
            }
            else
            {
                this.DrawTransparentGray();
            }
        }

    }
}