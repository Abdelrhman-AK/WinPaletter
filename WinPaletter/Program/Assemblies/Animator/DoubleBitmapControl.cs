using System;
using System.Diagnostics;
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

        /// <summary>
        /// The Animator instance that created this overlay. Used only as a fallback:
        /// if this overlay somehow survives being clicked by the user, we hand control back to the same Animator that produced it and let it run a
        /// normal ShowSync instead of tearing the overlay down by hand.
        /// </summary>
        public Animator OwnerAnimator { get; set; }

        private bool _fallbackTriggered;

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
            if (IsDisposed || Disposing)  return;

            OnFramePainting(e);

            try
            {
                Color fallback = Parent != null && !Parent.IsDisposed ? Parent.BackColor : BackColor;
                e.Graphics.Clear(fallback);

                if (bgBmp != null && !IsDisposed && bgBmp.IsValid()) e.Graphics.DrawImage(bgBmp, 0, 0);

                if (frame != null && !IsDisposed)
                {
                    TransfromNeededEventArg ea = new()
                    {
                        ClientRectangle = new(0, 0, Width, Height),
                        ClipRectangle = new(0, 0, Width, Height)
                    };
                    OnTransfromNeeded(ea);
                    e.Graphics.SetClip(ea.ClipRectangle);
                    e.Graphics.Transform = ea.Matrix;
                    e.Graphics.DrawImage(frame, 0, 0);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[DoubleBitmapControl] OnPaint failed for '{Name}': {ex}");
            }

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

                // Dispose bitmaps
                if (bgBmp != null)
                {
                    try { bgBmp.Dispose(); } catch { }
                    bgBmp = null;
                }
                if (frame != null)
                {
                    try { frame.Dispose(); } catch { }
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

                        if (tablessControl != null && !tablessControl.IsDisposed)
                        {
                            // Use BeginInvoke to avoid threading issues
                            tablessControl.BeginInvoke(new MethodInvoker(() =>
                            {
                                try
                                {
                                    if (!tablessControl.IsDisposed)
                                    {
                                        // Force TablessControl to repaint
                                        tablessControl.Invalidate();
                                        tablessControl.Update();
                                        tablessControl.Refresh();

                                        // Redraw all child controls
                                        foreach (Control ctrl in tablessControl.Controls)
                                        {
                                            if (!ctrl.IsDisposed && ctrl != this)
                                            {
                                                ctrl.Invalidate();
                                                ctrl.Update();
                                            }
                                        }

                                        try
                                        {
                                            User32.InvalidateRect(tablessControl.Handle, IntPtr.Zero, true);
                                            User32.UpdateWindow(tablessControl.Handle);
                                        }
                                        catch { }
                                    }
                                }
                                catch { }
                            }));
                        }
                        else if (!parent.IsDisposed)
                        {
                            parent.BeginInvoke(new MethodInvoker(() =>
                            {
                                try
                                {
                                    if (!parent.IsDisposed)
                                    {
                                        parent.Invalidate(bounds);
                                        parent.Update();
                                    }
                                }
                                catch { }
                            }));
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

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (_fallbackTriggered) return;

            Control realControl = Tag as Control;

            if (OwnerAnimator != null && realControl != null && !realControl.IsDisposed)
            {
                _fallbackTriggered = true;
                Debug.WriteLine($"[DoubleBitmapControl] MouseDown on '{Name}' while overlay still present - forcing Animator.ShowSync('{realControl.Name}')");

                BeginInvoke(new MethodInvoker(() =>
                {
                    try
                    {
                        OwnerAnimator.ShowSync(realControl);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"[DoubleBitmapControl] Fallback ShowSync failed: {ex}");
                    }
                }));
            }
            else
            {
                // Shouldn't normally happen - no owner Animator or real control known.
                // Fall back to an immediate, unanimated teardown so input is never
                // left blocked.
                Debug.WriteLine($"[DoubleBitmapControl] MouseDown on '{Name}' but no OwnerAnimator/Tag available - forcing immediate removal");
                _fallbackTriggered = true;
                BeginInvoke(new MethodInvoker(ForceImmediateRemoval));
            }
        }

        private void ForceImmediateRemoval()
        {
            try
            {
                if (IsDisposed || Disposing)
                    return;

                Control parent = Parent;
                Visible = false;

                if (parent != null && !parent.IsDisposed)
                {
                    parent.Controls.Remove(this);
                    parent.Invalidate();
                    parent.Update();
                }

                Dispose();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[DoubleBitmapControl] ForceImmediateRemoval failed: {ex}");
            }
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