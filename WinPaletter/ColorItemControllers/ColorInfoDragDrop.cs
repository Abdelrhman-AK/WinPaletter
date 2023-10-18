using System;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{

    public partial class ColorInfoDragDrop
    {

        #region Form Shadow

        private bool aeroEnabled;

        protected override CreateParams CreateParams
        {
            get
            {
                CheckAeroEnabled();
                var cp = base.CreateParams;
                if (!aeroEnabled)
                {
                    cp.ClassStyle = cp.ClassStyle | Dwmapi.CS_DROPSHADOW;
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
                case Dwmapi.WM_NCPAINT:
                    {
                        int val = 2;
                        if (aeroEnabled)
                        {
                            Dwmapi.DwmSetWindowAttribute(Handle, WPStyle.GetRoundedCorners() ? 2 : 1, ref val, 4);
                            Dwmapi.MARGINS bla = new();
                            {
                                bla.bottomHeight = 1;
                                bla.leftWidth = 1;
                                bla.rightWidth = 1;
                                bla.topHeight = 1;
                            }
                            Dwmapi.DwmExtendFrameIntoClientArea(Handle, ref bla);
                        }
                        break;
                    }
            }

            base.WndProc(ref m);
        }

        private void CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                var Com = default(bool);
                Dwmapi.DwmIsCompositionEnabled(ref Com);
                aeroEnabled = Com;
            }
            else
            {
                aeroEnabled = false;
            }
        }
        #endregion

        private readonly float _dark = 0.7f;

        private void ColorInfoDragDrop_Load(object sender, EventArgs e)
        {
            Location = MousePosition + (Size)new Point(15, 15);

            this.LoadLanguage();
            WPStyle.ApplyStyle(this);

            Label6.Font = Program.ConsoleFontMedium;
            Label7.Font = Program.ConsoleFontMedium;
            Label8.Font = Program.ConsoleFontMedium;
            Label9.Font = Program.ConsoleFontMedium;
            Label10.Font = Program.ConsoleFontMedium;
            Label11.Font = Program.ConsoleFontMedium;
            Label12.Font = Program.ConsoleFontMedium;
            Label13.Font = Program.ConsoleFontMedium;
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