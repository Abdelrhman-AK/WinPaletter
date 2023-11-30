using System;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{

    public partial class Store_Hover
    {

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

        public Store_Hover()
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

            const uint WM_NCACTIVATE = 0x86U;

            if (m.Msg == WM_NCACTIVATE && m.WParam.ToInt32() == 0)
            {
                HandleDeactivate();
            }

            base.WndProc(ref m);
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
            Icon = Forms.Store.Icon;
            this.LoadLanguage();
            ApplyStyle(this);
            this.DoubleBuffer();

            Point p;

            if (Forms.Store.selectedItem is not null)
            {
                p = Forms.Store.selectedItem.PointToScreen(Point.Empty) - (Size)new Point((int)Math.Round((Width - Forms.Store.selectedItem.Width) / 2d), (int)Math.Round((Height - Forms.Store.selectedItem.Height) / 2d));
            }

            else
            {
                p = MousePosition;

            }

            if (p.X + Width > Program.Computer.Screen.Bounds.Width)
                p = new(Program.Computer.Screen.Bounds.Width - Width, p.Y);
            if (p.Y + Height > Program.Computer.Screen.Bounds.Height)
                p = new(p.X, Program.Computer.Screen.Bounds.Height - Height);
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
            Program.Style.RenderingHint = Program.TM.MetricsFonts.Fonts_SingleBitPP ? System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit : System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
        }

    }
}