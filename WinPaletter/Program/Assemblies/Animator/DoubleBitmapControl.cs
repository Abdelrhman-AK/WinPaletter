using System;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.UI.WP;

namespace AnimatorNS
{
    public partial class DoubleBitmapControl : Control, IFakeControl
    {
        Bitmap bgBmp;
        Bitmap frame;
        private Control _animatedControl;
        private AnimateMode _mode;

        Bitmap IFakeControl.BgBmp
        {
            get => bgBmp;
            set
            {
                if (bgBmp != null)
                    bgBmp.Dispose();
                bgBmp = value;
            }
        }
        Bitmap IFakeControl.Frame
        {
            get => frame;
            set
            {
                if (frame != null)
                    frame.Dispose();
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
            // Add safety check - if the control is being disposed, skip painting
            if (IsDisposed || Disposing)
                return;

            OnFramePainting(e);

            try
            {
                if (bgBmp != null && !IsDisposed && bgBmp.IsValid())
                    e.Graphics.DrawImage(bgBmp, 0, 0);

                if (frame != null && !IsDisposed)
                {
                    var ea = new TransfromNeededEventArg
                    {
                        ClientRectangle = new Rectangle(0, 0, Width, Height),
                        ClipRectangle = new Rectangle(0, 0, Width, Height)
                    };
                    OnTransfromNeeded(ea);
                    e.Graphics.SetClip(ea.ClipRectangle);
                    e.Graphics.Transform = ea.Matrix;
                    e.Graphics.DrawImage(frame, 0, 0);
                }
            }
            catch { }

            OnFramePainted(e);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (!Visible && !IsDisposed && Parent != null && !Parent.IsDisposed)
            {
                // When this control becomes invisible, force parent to repaint
                Parent.Invalidate();
                Parent.Update();

                // If parent is a TablessControl, force full refresh
                if (Parent is TablessControl)
                {
                    Parent.Refresh();
                    foreach (Control ctrl in Parent.Controls)
                    {
                        if (!ctrl.IsDisposed && ctrl != this)
                        {
                            ctrl.Invalidate();
                            ctrl.Update();
                        }
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Control parent = Parent;
                Rectangle bounds = Bounds;

                if (bgBmp != null)
                {
                    bgBmp.Dispose();
                    bgBmp = null;
                }
                if (frame != null)
                {
                    frame.Dispose();
                    frame = null;
                }

                // Force parent to repaint after disposal with special handling for TablessControl
                if (parent != null && !parent.IsDisposed)
                {
                    try
                    {
                        // Check if this is a TablessControl or contains one
                        bool isTablessParent = parent is WinPaletter.UI.WP.TablessControl;
                        Control tablessControl = null;

                        if (!isTablessParent)
                        {
                            Control current = parent;
                            while (current != null)
                            {
                                if (current is WinPaletter.UI.WP.TablessControl)
                                {
                                    tablessControl = current;
                                    break;
                                }
                                current = current.Parent;
                            }
                        }
                        else
                        {
                            tablessControl = parent;
                        }

                        if (tablessControl != null)
                        {
                            // Force TablessControl to repaint using Win32 calls
                            try
                            {
                                User32.InvalidateRect(tablessControl.Handle, IntPtr.Zero, true);
                                User32.UpdateWindow(tablessControl.Handle);
                            }
                            catch { }

                            tablessControl.Invalidate();
                            tablessControl.Update();
                            tablessControl.Refresh();

                            foreach (Control ctrl in tablessControl.Controls)
                            {
                                if (!ctrl.IsDisposed && ctrl != this)
                                {
                                    ctrl.Invalidate();
                                    ctrl.Update();
                                }
                            }
                        }
                        else
                        {
                            parent.Invalidate(bounds);
                            parent.Update();
                        }
                    }
                    catch { }
                }
            }
            base.Dispose(disposing);
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
            _animatedControl = control;
            Parent = control.Parent;

            // Store which control this fake is for
            Tag = control;
            Name = $"FakeControl_{control.GetHashCode()}";

            // Insert at the same position as the original control
            int i = control.Parent.Controls.GetChildIndex(control);
            control.Parent.Controls.SetChildIndex(this, i);
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