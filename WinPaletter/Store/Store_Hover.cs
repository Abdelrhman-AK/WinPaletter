using System;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter
{
    public partial class Store_Hover
    {

        public Store_Hover()
        {
            InitializeComponent();
        }

        private int index = 0;
        public Bitmap img0;
        public Bitmap img1;

        private void Store_Hover_Load(object sender, EventArgs e)
        {
            Icon = Forms.Store.Icon;

            Point p;
            if (Forms.Store.selectedItem is not null)
            {
                p = Forms.Store.selectedItem.PointToScreen(Point.Empty) - (Size)new Point((int)Math.Round((Width - Forms.Store.selectedItem.Width) / 2d), (int)Math.Round((Height - Forms.Store.selectedItem.Height) / 2d));
            }
            else
            {
                p = MousePosition;
            }

            if (p.X + Width > Screen.PrimaryScreen.Bounds.Width) p = new(Screen.PrimaryScreen.Bounds.Width - Width, p.Y);
            if (p.Y + Height > Screen.PrimaryScreen.Bounds.Height) p = new(p.X, Screen.PrimaryScreen.Bounds.Height - Height);
            Location = p;
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
                if (img1 is not null) BackgroundImage = img1;
            }
            else
            {
                index = 0;
                if (img0 is not null) BackgroundImage = img0;
            }
        }

        private void Store_Hover_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Style.RenderingHint = Program.TM.MetricsFonts.Fonts_SingleBitPP ? System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit : System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
        }
    }
}