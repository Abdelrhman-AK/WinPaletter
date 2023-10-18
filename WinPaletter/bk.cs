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
            if (Program.W7 | Program.WVista)
                FormBorderStyle = FormBorderStyle.Sizable;

            if (!Program.WVista)
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