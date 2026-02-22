using FluentTransitions;
using System;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter
{
    /// <summary>
    /// A form to select the OS style.
    /// </summary>
    public partial class OS_Dashboard
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OS_Dashboard"/> class.
        /// </summary>
        public OS_Dashboard()
        {
            InitializeComponent();
        }

        Size targetSize;
        Point targetLocation;

        private void OS_Dashboard_Load(object sender, EventArgs e)
        {
            radioImage7.Enabled = OS.IsWin12Released;

            // Select the current OS style.
            switch (Program.WindowStyle)
            {
                case WindowStyle.W12:
                    radioImage7.Checked = true;
                    break;

                case WindowStyle.W11:
                    radioImage6.Checked = true;
                    break;

                case WindowStyle.W10:
                    radioImage5.Checked = true;
                    break;

                case WindowStyle.W81:
                    radioImage4.Checked = true;
                    break;

                case WindowStyle.W8:
                    radioImage8.Checked = true;
                    break;

                case WindowStyle.W7:
                    radioImage3.Checked = true;
                    break;

                case WindowStyle.WVista:
                    radioImage2.Checked = true;
                    break;

                case WindowStyle.WXP:
                    radioImage1.Checked = true;
                    break;

                default:
                    radioImage6.Checked = true;
                    break;
            }

            // Animate the form.
            Transition.With(this, nameof(Height), targetSize.Height).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration * 0.6));

            BackColor = Color.Black;

            DWM.DWMStyles style;

            if (OS.WXP || OS.W8x) style = DWM.DWMStyles.None;
            else if (OS.WVista || OS.W7) style = DWM.DWMStyles.Aero;
            else style = DWM.DWMStyles.Acrylic;

            this.DropEffect(Padding.Empty, true, style, true);
        }

        public DialogResult ShowDialog(Size parentButtonSize, Point parentButtonLocation)
        {
            targetSize = Size;
            targetLocation = parentButtonLocation - new Size(Width - parentButtonSize.Width, 0);

            Size = new(Width, 1);
            Location = parentButtonLocation;
            Left -= Width - parentButtonSize.Width;

            return this.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Set the OS style.

            if (radioImage7.Checked) Program.WindowStyle = WindowStyle.W12;
            else if (radioImage6.Checked) Program.WindowStyle = WindowStyle.W11;
            else if (radioImage5.Checked) Program.WindowStyle = WindowStyle.W10;
            else if (radioImage4.Checked) Program.WindowStyle = WindowStyle.W81;
            else if (radioImage8.Checked) Program.WindowStyle = WindowStyle.W8;
            else if (radioImage3.Checked) Program.WindowStyle = WindowStyle.W7;
            else if (radioImage2.Checked) Program.WindowStyle = WindowStyle.WVista;
            else if (radioImage1.Checked) Program.WindowStyle = WindowStyle.WXP;
            else Program.WindowStyle = WindowStyle.W11;

            DialogResult = DialogResult.OK;
        }
    }
}