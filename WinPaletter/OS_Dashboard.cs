using System;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter
{
    public partial class OS_Dashboard
    {
        private readonly int _Speed = 10;

        #region Form Shadow
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (!DWMAPI.IsCompositionEnabled())
                {
                    cp.ClassStyle |= DWMAPI.CS_DROPSHADOW;
                    cp.ExStyle |= 33554432;
                    return cp;
                }
                else
                {
                    return cp;
                }
            }
        }

        public OS_Dashboard()
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

        private void OS_Dashboard_Load(object sender, EventArgs e)
        {
            Icon = Forms.MainForm.Icon;

            this.LoadLanguage();
            ApplyStyle(this);

            switch (Program.WindowStyle)
            {
                case WindowStyle.W12:
                    radioImage6.Checked = true;
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

            Size targetSize = Size;
            Point targetLocation = Forms.Home.winEdition.PointToScreen(Point.Empty) - new Size(Width, 0);

            Size = Forms.Home.winEdition.Size;
            Location = Forms.Home.winEdition.PointToScreen(Point.Empty);

            FluentTransitions.Transition
                .With(this, nameof(this.Width), targetSize.Width)
                .With(this, nameof(this.Height), targetSize.Height)
                .With(this, nameof(this.Left), targetLocation.X)
                .With(this, nameof(this.Top), targetLocation.Y)
                .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration * 0.6));

            User32.AnimateWindow(Handle, _Speed, User32.AnimateWindowFlags.AW_ACTIVATE | User32.AnimateWindowFlags.AW_BLEND);

            Invalidate();
        }

        private void SubMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            User32.AnimateWindow(Handle, _Speed, User32.AnimateWindowFlags.AW_HIDE | User32.AnimateWindowFlags.AW_BLEND);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (radioImage6.Checked) Program.WindowStyle = WindowStyle.W11;
            else if (radioImage5.Checked) Program.WindowStyle = WindowStyle.W10;
            else if (radioImage4.Checked) Program.WindowStyle = WindowStyle.W81;
            else if (radioImage3.Checked) Program.WindowStyle = WindowStyle.W7;
            else if (radioImage2.Checked) Program.WindowStyle = WindowStyle.WVista;
            else if (radioImage1.Checked) Program.WindowStyle = WindowStyle.WXP;
            else Program.WindowStyle = WindowStyle.W12;

            DialogResult = DialogResult.OK;
        }
    }
}