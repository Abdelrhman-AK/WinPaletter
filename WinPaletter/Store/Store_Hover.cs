using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WinPaletter
{
    /// <summary>
    /// Hover form for previewing themes in the store.
    /// </summary>
    public partial class Store_Hover
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Store_Hover"/> class.
        /// </summary>
        public Store_Hover()
        {
            InitializeComponent();
        }

        private int index = 0;

        /// <summary>
        /// Image to display in the preview (modern Windows elements).
        /// </summary>
        public Bitmap img0;

        /// <summary>
        /// Image to display in the preview (classic Windows elements).
        /// </summary>
        public Bitmap img1;

        private void Store_Hover_Load(object sender, EventArgs e)
        {
            Point p;
            if (Forms.Store.selectedItem is not null)
            {
                p = Forms.Store.selectedItem.PointToScreen(Point.Empty) - (Size)new Point((int)((Width - Forms.Store.selectedItem.Width) / 2f), (int)((Height - Forms.Store.selectedItem.Height) / 2f));
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

        /// <summary>
        /// Switches the preview image between the two available images (modern and classic Windows elements).
        /// </summary>
        private void SwitchPreview()
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
            Program.Style.TextRenderingHint = Program.TM.MetricsFonts.Fonts_SingleBitPP ? TextRenderingHint.SingleBitPerPixelGridFit : TextRenderingHint.ClearTypeGridFit;
        }
    }
}