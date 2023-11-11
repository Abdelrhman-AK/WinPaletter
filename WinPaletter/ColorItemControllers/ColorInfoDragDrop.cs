using System;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{

    public partial class ColorInfoDragDrop
    {

        #region Form Shadow

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                if (!DWMAPI.IsCompositionEnabled())
                {
                    cp.ClassStyle = cp.ClassStyle | DWMAPI.CS_DROPSHADOW;
                    cp.ExStyle = cp.ExStyle | 33554432;
                    return cp;
                }
                else
                {
                    return cp;
                }
            }
        }

        public ColorInfoDragDrop()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case DWMAPI.WM_NCPAINT:
                    {
                        int val = 2;
                        if (DWMAPI.IsCompositionEnabled())
                        {
                            DWMAPI.DwmSetWindowAttribute(Handle, Program.Style.RoundedCorners ? 2 : 1, ref val, 4);
                            DWMAPI.MARGINS bla = new();
                            {
                                bla.bottomHeight = 1;
                                bla.leftWidth = 1;
                                bla.rightWidth = 1;
                                bla.topHeight = 1;
                            }
                            DWMAPI.DwmExtendFrameIntoClientArea(Handle, ref bla);
                        }
                        break;
                    }
            }

            base.WndProc(ref m);
        }
        #endregion

        private readonly float _dark = 0.7f;

        private void ColorInfoDragDrop_Load(object sender, EventArgs e)
        {
            Location = MousePosition + (Size)new Point(15, 15);

            this.LoadLanguage();
            ApplyStyle(this);

            Label6.Font = Fonts.ConsoleMedium;
            Label7.Font = Fonts.ConsoleMedium;
            Label8.Font = Fonts.ConsoleMedium;
            Label9.Font = Fonts.ConsoleMedium;
            Label10.Font = Fonts.ConsoleMedium;
            Label11.Font = Fonts.ConsoleMedium;
            Label12.Font = Fonts.ConsoleMedium;
            Label13.Font = Fonts.ConsoleMedium;
        }

        private void Color_From_BackColorChanged(object sender, EventArgs e)
        {
            var Color = ((Panel)sender).BackColor;
            BackColor = Program.Style.DarkMode ? Color.Dark(_dark) : Color.LightLight();

            Label6.Text = Color.ReturnFormat(ColorsExtensions.ColorFormat.RGB, true, Color.A < 255).Replace(" ", ", ");
            Label7.Text = Color.ReturnFormat(ColorsExtensions.ColorFormat.HEX, true, Color.A < 255).Replace(" ", ", ");
            Label8.Text = Color.ReturnFormat(ColorsExtensions.ColorFormat.HSL, true, Color.A < 255).Replace(" ", ", ");
            Label9.Text = Color.ReturnFormat(ColorsExtensions.ColorFormat.Dec, true, Color.A < 255).Replace(" ", ", ");

            Label6.ForeColor = Color.IsDark() ? System.Drawing.Color.White : System.Drawing.Color.Black;
            Label7.ForeColor = Label6.ForeColor;
            Label8.ForeColor = Label6.ForeColor;
            Label9.ForeColor = Label6.ForeColor;
        }

        private void Color_To_BackColorChanged(object sender, EventArgs e)
        {
            var Color = ((Panel)sender).BackColor;

            Label13.Text = Color.ReturnFormat(ColorsExtensions.ColorFormat.RGB, true, Color.A < 255).Replace(" ", ", ");
            Label12.Text = Color.ReturnFormat(ColorsExtensions.ColorFormat.HEX, true, Color.A < 255).Replace(" ", ", ");
            Label11.Text = Color.ReturnFormat(ColorsExtensions.ColorFormat.HSL, true, Color.A < 255).Replace(" ", ", ");
            Label10.Text = Color.ReturnFormat(ColorsExtensions.ColorFormat.Dec, true, Color.A < 255).Replace(" ", ", ");

            Label10.ForeColor = Color.IsDark() ? System.Drawing.Color.White : System.Drawing.Color.Black;
            Label11.ForeColor = Label10.ForeColor;
            Label12.ForeColor = Label10.ForeColor;
            Label13.ForeColor = Label10.ForeColor;
        }
    }
}