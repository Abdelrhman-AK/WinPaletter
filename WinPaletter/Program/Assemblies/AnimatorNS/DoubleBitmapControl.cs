using System;
using System.Drawing;
using System.Windows.Forms;

namespace AnimatorNS
{
    public partial class DoubleBitmapControl : Control, IFakeControl
    {
        Bitmap bgBmp;
        Bitmap frame;

        Bitmap IFakeControl.BgBmp
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
        Bitmap IFakeControl.Frame
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

        public event EventHandler<TransfromNeededEventArg> TransfromNeeded;
        public event EventHandler<PaintEventArgs> FramePainted;
        public event EventHandler<PaintEventArgs> FramePainting;

        public DoubleBitmapControl()
        {
            InitializeComponent();
            Visible = false;
            SetStyle(ControlStyles.Selectable, false);
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            OnFramePainting(e);

            try
            {
                if (bgBmp != null)
                {
                    e.Graphics.DrawImage(bgBmp, 0, 0); // Draw background bitmap if present //
                }

                if (frame != null)
                {
                    // Only allocate TransfromNeededEventArg once per paint (reuse if possible in future) //
                    var ea = new TransfromNeededEventArg
                    {
                        ClientRectangle = new Rectangle(0, 0, Width, Height),
                        ClipRectangle = new Rectangle(0, 0, Width, Height)
                    };
                    OnTransfromNeeded(ea);
                    e.Graphics.SetClip(ea.ClipRectangle);
                    e.Graphics.Transform = ea.Matrix;
                    e.Graphics.DrawImage(frame, 0, 0); // Draw frame bitmap if present //
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
            Parent = control.Parent;
            int i = control.Parent.Controls.GetChildIndex(control);
            control.Parent.Controls.SetChildIndex(this, i);
            // Avoid repeated Rectangle allocations by constructing in place //
            Bounds = new Rectangle(
                control.Left - padding.Left,
                control.Top - padding.Top,
                control.Size.Width + padding.Left + padding.Right,
                control.Size.Height + padding.Top + padding.Bottom);
        }
    }

    public interface IFakeControl
    {
        Bitmap BgBmp { get; set; }
        Bitmap Frame { get; set; }
        event EventHandler<TransfromNeededEventArg> TransfromNeeded;
        event EventHandler<PaintEventArgs> FramePainting;
        event EventHandler<PaintEventArgs> FramePainted;
        void InitParent(Control animatedControl, Padding padding);
    }
}