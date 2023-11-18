using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Drawing;
using System.Linq;

namespace WinPaletter
{
    public partial class LogonUI8Colors
    {
        public LogonUI8Colors()
        {
            InitializeComponent();
        }
        private void LogonUI8Colors_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);

            Icon = Forms.Start8Selector.Icon;

            color0.Image = Color.FromArgb(34, 34, 34).ToBitmap(new Size(32, 32));
            color1.Image = Color.FromArgb(34, 34, 34).ToBitmap(new Size(32, 32));
            color2.Image = Color.FromArgb(34, 34, 34).ToBitmap(new Size(32, 32));
            color3.Image = Color.FromArgb(34, 34, 34).ToBitmap(new Size(32, 32));
            color4.Image = Color.FromArgb(42, 27, 0).ToBitmap(new Size(32, 32));
            color5.Image = Color.FromArgb(59, 0, 3).ToBitmap(new Size(32, 32));
            color6.Image = Color.FromArgb(65, 0, 49).ToBitmap(new Size(32, 32));
            color7.Image = Color.FromArgb(41, 0, 66).ToBitmap(new Size(32, 32));
            color8.Image = Color.FromArgb(30, 3, 84).ToBitmap(new Size(32, 32));
            color9.Image = Color.FromArgb(0, 31, 66).ToBitmap(new Size(32, 32));
            color10.Image = Color.FromArgb(3, 66, 82).ToBitmap(new Size(32, 32));
            color11.Image = Color.FromArgb(30, 144, 255).ToBitmap(new Size(32, 32));
            color12.Image = Color.FromArgb(4, 63, 0).ToBitmap(new Size(32, 32));
            color13.Image = Color.FromArgb(188, 90, 28).ToBitmap(new Size(32, 32));
            color14.Image = Color.FromArgb(155, 28, 29).ToBitmap(new Size(32, 32));
            color15.Image = Color.FromArgb(152, 28, 90).ToBitmap(new Size(32, 32));
            color16.Image = Color.FromArgb(88, 28, 152).ToBitmap(new Size(32, 32));
            color17.Image = Color.FromArgb(28, 74, 153).ToBitmap(new Size(32, 32));
            color18.Image = Color.FromArgb(69, 143, 221).ToBitmap(new Size(32, 32));
            color19.Image = Color.FromArgb(0, 141, 142).ToBitmap(new Size(32, 32));
            color20.Image = Color.FromArgb(120, 168, 33).ToBitmap(new Size(32, 32));
            color21.Image = Color.FromArgb(191, 142, 16).ToBitmap(new Size(32, 32));
            color22.Image = Color.FromArgb(219, 80, 171).ToBitmap(new Size(32, 32));
            color23.Image = Color.FromArgb(154, 154, 154).ToBitmap(new Size(32, 32));
            color24.Image = Color.FromArgb(88, 88, 88).ToBitmap(new Size(32, 32));

            foreach (UI.WP.RadioImage ri in Controls.OfType<UI.WP.RadioImage>())
            {
                if (Program.TM.Windows81.LogonUI == Conversions.ToDouble(ri.Name.Replace("color", string.Empty)))
                    ri.Checked = true;
                else
                    ri.Checked = false;
            }


        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            foreach (UI.WP.RadioImage ri in Controls.OfType<UI.WP.RadioImage>())
            {
                if (ri.Checked)
                {
                    Program.TM.Windows81.LogonUI = Conversions.ToInteger(ri.Name.Replace("color", string.Empty));
                    break;
                }
            }

            Close();
        }
    }
}