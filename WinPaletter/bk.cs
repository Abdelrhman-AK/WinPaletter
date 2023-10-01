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
            if (My.Env.W7 | My.Env.WVista)
                FormBorderStyle = FormBorderStyle.Sizable;
            if (!My.Env.WVista)
            {
                this.DrawDWMEffect(default, false, FormDWMEffects.FormStyle.Acrylic);
            }
            else
            {
                this.DrawTransparentGray();
            }
        }

    }
}