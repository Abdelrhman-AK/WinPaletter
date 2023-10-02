using System;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{

    public partial class Store_Hover
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

        public Store_Hover()
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
                            var bla = new Dwmapi.MARGINS();
                            {
                                ref var temp = ref bla;
                                temp.bottomHeight = 1;
                                temp.leftWidth = 1;
                                temp.rightWidth = 1;
                                temp.topHeight = 1;
                            }
                            Dwmapi.DwmExtendFrameIntoClientArea(Handle, ref bla);
                        }
                        break;
                    }
            }

            const uint WM_NCACTIVATE = 0x86U;

            if (m.Msg == WM_NCACTIVATE && m.WParam.ToInt32() == 0)
            {
                HandleDeactivate();
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

        public void HandleDeactivate()
        {
            if (_shown)
            {
                _shown = false;
                Close();
            }
        }
        #endregion

        private int index = 0;
        public Bitmap img0;
        public Bitmap img1;
        private bool _shown = false;

        private void Store_Hover_Load(object sender, EventArgs e)
        {
            Icon = My.MyProject.Forms.Store.Icon;
            this.LoadLanguage();
            WPStyle.ApplyStyle(this);
            this.DoubleBuffer();

            Point p;

            if (My.MyProject.Forms.Store.selectedItem is not null)
            {
                p = My.MyProject.Forms.Store.selectedItem.PointToScreen(Point.Empty) - (Size)new Point((int)Math.Round((Width - My.MyProject.Forms.Store.selectedItem.Width) / 2d), (int)Math.Round((Height - My.MyProject.Forms.Store.selectedItem.Height) / 2d));
            }

            else
            {
                p = MousePosition;

            }

            if (p.X + Width > My.MyProject.Computer.Screen.Bounds.Width)
                p = new Point(My.MyProject.Computer.Screen.Bounds.Width - Width, p.Y);
            if (p.Y + Height > My.MyProject.Computer.Screen.Bounds.Height)
                p = new Point(p.X, My.MyProject.Computer.Screen.Bounds.Height - Height);
            Location = p;

            _shown = false;
        }

        private void Store_Hover_Shown(object sender, EventArgs e)
        {
            _shown = true;
        }

        private void Store_Hover_MouseClick(object sender, MouseEventArgs e)
        {
            Close();
        }

        private void Store_Hover_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int)Keys.Right | e.KeyValue == (int)Keys.Left | e.KeyValue == (int)Keys.Up | e.KeyValue == (int)Keys.Down)
            {
                SwitchPreview();
            }
            else
            {
                Close();
            }
        }

        private void Store_Hover_MouseWheel(object sender, MouseEventArgs e)
        {
            SwitchPreview();
        }

        public void SwitchPreview()
        {
            if (index == 0)
            {
                index = 1;
                if (img1 is not null)
                    BackgroundImage = img1;
            }
            else
            {
                index = 0;
                if (img0 is not null)
                    BackgroundImage = img0;
            }
        }

        private void Store_Hover_FormClosed(object sender, FormClosedEventArgs e)
        {
            My.Env.RenderingHint = My.Env.TM.MetricsFonts.Fonts_SingleBitPP ? System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit : System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
        }

    }
}