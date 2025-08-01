using System;
using System.Drawing;
using System.Windows.Forms;

namespace AnimatorNS
{
    public partial class DoubleBitmapForm : Form, IFakeControl
    {
        Bitmap bgBmp;
        Bitmap frame;

        public event EventHandler<TransfromNeededEventArg> TransfromNeeded;
        public event EventHandler<PaintEventArgs> FramePainting;
        public event EventHandler<PaintEventArgs> FramePainted;

        Padding padding;
        Control control;

        public DoubleBitmapForm()
        {
            InitializeComponent();
            Visible = false;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            TopMost = true;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                unchecked
                {
                    cp.Style = (int)Flags.WindowStyles.WS_POPUP;
                }
                cp.ExStyle |= (int)Flags.WindowStyles.WS_EX_NOACTIVATE | (int)Flags.WindowStyles.WS_EX_TOOLWINDOW;
                cp.X = Location.X;
                cp.Y = Location.Y;
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            OnFramePainting(e);

            try
            {
                if (bgBmp != null)
                {
                    e.Graphics.DrawImage(bgBmp, -Location.X, -Location.Y); // Draw background bitmap if present //
                }

                if (frame != null && control != null)
                {
                    // Only allocate TransfromNeededEventArg once per paint (reuse if possible in future) //
                    var ea = new TransfromNeededEventArg();
                    ea.ClientRectangle = ea.ClipRectangle = new Rectangle(
                        control.Bounds.Left - padding.Left,
                        control.Bounds.Top - padding.Top,
                        control.Bounds.Width + padding.Horizontal,
                        control.Bounds.Height + padding.Vertical);
                    OnTransfromNeeded(ea);
                    e.Graphics.SetClip(ea.ClipRectangle);
                    e.Graphics.Transform = ea.Matrix;
                    var p = control.Location;
                    e.Graphics.DrawImage(frame, p.X - padding.Left, p.Y - padding.Top); // Draw frame bitmap if present //
                }
            }
            catch (Exception ex) // Always log exceptions in debug builds //
            {
#if debug
                Console.WriteLine(ex);
#endif
            }

            OnFramePainted(e);
        }

        private void OnTransfromNeeded(TransfromNeededEventArg ea)
        {
            TransfromNeeded?.Invoke(this, ea);
        }

        protected virtual void OnFramePainting(PaintEventArgs e)
        {
            FramePainting?.Invoke(this, e);
        }

        protected virtual void OnFramePainted(PaintEventArgs e)
        {
            FramePainted?.Invoke(this, e);
        }

        public void InitParent(Control control, Padding padding)
        {
            this.control = control;
            this.padding = padding;
            Location = Point.Empty;
            Size = Screen.PrimaryScreen.Bounds.Size;
            control.VisibleChanged += control_VisibleChanged;
        }

        void control_VisibleChanged(object sender, EventArgs e)
        {
            // No-op: can be used for future optimizations if needed
        }

        public Bitmap BgBmp
        {
            get => bgBmp;
            set
            {
                if (bgBmp != null)
                {
                    bgBmp.Dispose(); // Dispose old bgBmp to minimize memory usage and GC pressure //
                }
                bgBmp = value;
            }
        }

        public Bitmap Frame
        {
            get => frame;
            set
            {
                if (frame != null)
                {
                    frame.Dispose(); // Dispose old frame to minimize memory usage and GC pressure //
                }
                frame = value;
            }
        }
    }
}