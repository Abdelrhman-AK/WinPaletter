using System;
using System.Drawing;
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

        private void OS_Dashboard_Load(object sender, EventArgs e)
        {
            // Set the form icon.
            Icon = FormsExtensions.Icon<MainForm>();

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

            // Set the form size and location.
            Size targetSize = Size;
            Point targetLocation = Forms.Home.winEdition.PointToScreen(Point.Empty) - new Size(Width - Forms.Home.winEdition.Width, 0);

            Size = Forms.Home.winEdition.Size;
            Location = Forms.Home.winEdition.PointToScreen(Point.Empty);

            // Animate the form.
            FluentTransitions.Transition
                .With(this, nameof(Width), targetSize.Width)
                .With(this, nameof(Height), targetSize.Height)
                .With(this, nameof(Left), targetLocation.X)
                .With(this, nameof(Top), targetLocation.Y)
                .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration * 0.6));
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
            else Program.WindowStyle = WindowStyle.W12;

            if (radioImage7.Checked)
                MsgBox(Program.Lang.Strings.Messages.Win12_Preview_Msg0, MessageBoxButtons.OK, MessageBoxIcon.Information, Program.Lang.Strings.Messages.Win12_Preview_Msg1);

            DialogResult = DialogResult.OK;
        }
    }
}