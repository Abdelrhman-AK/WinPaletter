//#define debug

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Permissions;
using System.Windows.Forms;

namespace AnimatorNS
{
    /// <summary>
    /// DoubleBitmap displays animation
    /// </summary>
    public class Controller
    {
        protected Bitmap BgBmp { get { return (DoubleBitmap as IFakeControl).BgBmp; } set { (DoubleBitmap as IFakeControl).BgBmp = value; } }
        public Bitmap Frame { get { return (DoubleBitmap as IFakeControl).Frame; } set { (DoubleBitmap as IFakeControl).Frame = value; } }
        protected Bitmap ctrlBmp;
        public float CurrentTime { get; private set; }
        protected float TimeStep { get; private set; }

        public event EventHandler<TransfromNeededEventArg> TransfromNeeded;
        public event EventHandler<NonLinearTransfromNeededEventArg> NonLinearTransfromNeeded;
        public event EventHandler<PaintEventArgs> FramePainting;
        public event EventHandler<PaintEventArgs> FramePainted;
        public event EventHandler<MouseEventArgs> MouseDown;

        public Control DoubleBitmap { get; private set; }
        public Control AnimatedControl { get; set; }
        byte[] pixelsBuffer;
        protected Rectangle CustomClipRect;

        AnimateMode mode;
        Animation animation;

        // Cache commonly used rectangles to reduce allocations
        private Rectangle cachedBounds; // //
        private Rectangle cachedClientRect; // //

        public void Dispose()
        {
            // Unsubscribe events to avoid memory leaks
            if (DoubleBitmap is IFakeControl fakeControl)
            {
                fakeControl.FramePainting -= OnFramePainting;
                fakeControl.FramePainted -= OnFramePainting;
                fakeControl.TransfromNeeded -= OnTransfromNeeded;
            }
            if (DoubleBitmap != null)
            {
                DoubleBitmap.MouseDown -= OnMouseDown;
            }

            // Dispose bitmaps
            if (ctrlBmp != null)
            {
                ctrlBmp.Dispose();
                ctrlBmp = null;
            }
            if (BgBmp != null)
            {
                BgBmp.Dispose();
                BgBmp = null;
            }
            if (Frame != null)
            {
                Frame.Dispose();
                Frame = null;
            }

            AnimatedControl = null;
            pixelsBuffer = null; // Let GC collect this buffer

            Hide();

            // DoubleBitmap is not disposed by default, but can be if known not reused
            // if (DoubleBitmap != null)
            // {
            //     DoubleBitmap.Dispose();
            //     DoubleBitmap = null;
            // }
        }

        public void Hide()
        {
            if (DoubleBitmap != null)
            {
                try
                {
                    DoubleBitmap.BeginInvoke(new MethodInvoker(() =>
                    {
                        if (DoubleBitmap.Visible) DoubleBitmap.Hide();
                        DoubleBitmap.Parent = null;
                        //DoubleBitmap.Dispose(); // Consider enabling if not reused
                    }));
                }
                catch (Exception ex) // // Always log/handle exceptions
                {
#if debug
                    Console.WriteLine(ex);
#endif
                }
            }
        }

        protected virtual Rectangle GetBounds()
        {
            // Cache results to avoid repeated allocations //
            if (cachedBounds == Rectangle.Empty) // //
            {
                cachedBounds = new Rectangle(
                    AnimatedControl.Left - animation.Padding.Left,
                    AnimatedControl.Top - animation.Padding.Top,
                    AnimatedControl.Size.Width + animation.Padding.Left + animation.Padding.Right,
                    AnimatedControl.Size.Height + animation.Padding.Top + animation.Padding.Bottom);
            }
            return cachedBounds; // //
        }

        protected virtual Rectangle ControlRectToMyRect(Rectangle rect)
        {
            // Minimal allocation by not creating new Padding objects //
            return new Rectangle(
                animation.Padding.Left + rect.Left,
                animation.Padding.Top + rect.Top,
                rect.Width + animation.Padding.Left + animation.Padding.Right,
                rect.Height + animation.Padding.Top + animation.Padding.Bottom);
        }

        public Controller(Control control, AnimateMode mode, Animation animation, float timeStep, Rectangle controlClipRect)
        {
            if (control is Form)
                DoubleBitmap = new DoubleBitmapForm();
            else
                DoubleBitmap = new DoubleBitmapControl();

            (DoubleBitmap as IFakeControl).FramePainting += OnFramePainting;
            (DoubleBitmap as IFakeControl).FramePainted += OnFramePainting;
            (DoubleBitmap as IFakeControl).TransfromNeeded += OnTransfromNeeded;
            DoubleBitmap.MouseDown += OnMouseDown;

            this.animation = animation;
            this.AnimatedControl = control;
            this.mode = mode;
            this.CustomClipRect = controlClipRect;

            if (mode == AnimateMode.Show || mode == AnimateMode.BeginUpdate)
                timeStep = -timeStep;

            this.TimeStep = timeStep * (animation.TimeCoeff == 0f ? 1f : animation.TimeCoeff);
            if (this.TimeStep == 0f)
                this.TimeStep = 0.01f;

            try
            {
                switch (mode)
                {
                    case AnimateMode.Hide:
                        BgBmp = GetBackground(control);
                        (DoubleBitmap as IFakeControl).InitParent(control, animation.Padding);
                        ctrlBmp = GetForeground(control);
                        DoubleBitmap.Visible = true;
                        control.Visible = false;
                        break;

                    case AnimateMode.Show:
                        BgBmp = GetBackground(control);
                        (DoubleBitmap as IFakeControl).InitParent(control, animation.Padding);
                        DoubleBitmap.Visible = true;
                        DoubleBitmap.Refresh();
                        control.Visible = true;
                        ctrlBmp = GetForeground(control);
                        break;

                    case AnimateMode.BeginUpdate:
                    case AnimateMode.Update:
                        (DoubleBitmap as IFakeControl).InitParent(control, animation.Padding);
                        BgBmp = GetBackground(control, true);
                        DoubleBitmap.Visible = true;
                        break;
                }
            }
            catch (Exception ex) // // Always log/handle exceptions
            {
#if debug
                Console.WriteLine(ex);
#endif
                Dispose();
            }

            CurrentTime = timeStep > 0 ? animation.MinTime : animation.MaxTime;
        }

        protected virtual void OnMouseDown(object sender, MouseEventArgs e)
        {
            MouseDown?.Invoke(this, e);
        }

        protected virtual void OnFramePainting(object sender, PaintEventArgs e)
        {
            var oldFrame = Frame;
            Frame = null;

            if (mode == AnimateMode.BeginUpdate)
                return;

            Frame = OnNonLinearTransfromNeeded();

            if (oldFrame != Frame && oldFrame != null)
            {
                oldFrame.Dispose(); // Dispose previous frame aggressively //
                oldFrame = null;
            }

            var time = CurrentTime + TimeStep;
            if (time > animation.MaxTime) time = animation.MaxTime;
            if (time < animation.MinTime) time = animation.MinTime;
            CurrentTime = time;

            FramePainting?.Invoke(this, e);
        }

        protected virtual void OnFramePainted(object sender, PaintEventArgs e)
        {
            FramePainted?.Invoke(this, e);
        }

        protected virtual Bitmap GetBackground(Control ctrl, bool includeForeground = false, bool clip = false)
        {
            if (ctrl is Form)
                return GetScreenBackground(ctrl, includeForeground, clip);

            Rectangle bounds = GetBounds(); // Use cached bounds //
            int w = bounds.Width;
            int h = bounds.Height;
            if (w <= 0) w = 1;
            if (h <= 0) h = 1;

            // Try to reuse bitmaps if possible. For now, always dispose old and allocate new, but keep references for GC. //
            Bitmap bmp = new Bitmap(w, h);

            Rectangle clientRect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                PaintEventArgs ea = new PaintEventArgs(g, clientRect);
                if (clip)
                {
                    if (CustomClipRect == default(Rectangle))
                        g.SetClip(new Rectangle(0, 0, w, h));
                    else
                        g.SetClip(CustomClipRect);
                }

                // Instead of allocating bitmaps in every iteration, consider caching if reused, here we must allocate. //
                for (int i = ctrl.Parent.Controls.Count - 1; i >= 0; i--)
                {
                    var c = ctrl.Parent.Controls[i];
                    if (c == ctrl && !includeForeground) break;
                    if (c.Visible && !c.IsDisposed && c.Bounds.IntersectsWith(bounds))
                    {
                        using (Bitmap cb = new Bitmap(c.Width, c.Height))
                        {
                            c.DrawToBitmap(cb, new Rectangle(0, 0, c.Width, c.Height));
                            ea.Graphics.DrawImage(cb, c.Left - bounds.Left, c.Top - bounds.Top, c.Width, c.Height);
                        }
                    }
                    if (c == ctrl) break;
                }
            }
            return bmp;
        }

        private Bitmap GetScreenBackground(Control ctrl, bool includeForeground, bool clip)
        {
            var size = Screen.PrimaryScreen.Bounds.Size;
            // Avoid allocating temporary graphics objects if possible //
            using (Graphics temp = DoubleBitmap.CreateGraphics())
            {
                var bmp = new Bitmap(size.Width, size.Height, temp);
                using (Graphics gr = Graphics.FromImage(bmp))
                {
                    gr.CopyFromScreen(0, 0, 0, 0, size);
                }
                return bmp;
            }
        }

        protected virtual Bitmap GetForeground(Control ctrl)
        {
            Bitmap bmp = null;

            if (!ctrl.IsDisposed)
            {
                // This allocation is hard to avoid unless we always reuse the same bitmap
                if (ctrl.Parent == null)
                {
                    bmp = new Bitmap(ctrl.Width + animation.Padding.Horizontal, ctrl.Height + animation.Padding.Vertical);
                    ctrl.DrawToBitmap(bmp, new Rectangle(animation.Padding.Left, animation.Padding.Top, ctrl.Width, ctrl.Height));
                }
                else
                {
                    bmp = new Bitmap(DoubleBitmap.Width, DoubleBitmap.Height);
                    ctrl.DrawToBitmap(bmp, new Rectangle(ctrl.Left - DoubleBitmap.Left, ctrl.Top - DoubleBitmap.Top, ctrl.Width, ctrl.Height));
                }
            }
            return bmp;
        }

        protected virtual void OnTransfromNeeded(object sender, TransfromNeededEventArg e)
        {
            try
            {
                if (CustomClipRect != default(Rectangle))
                    e.ClipRectangle = ControlRectToMyRect(CustomClipRect);

                e.CurrentTime = CurrentTime;

                if (TransfromNeeded != null)
                    TransfromNeeded(this, e);
                else
                    e.UseDefaultMatrix = true;

                if (e.UseDefaultMatrix)
                {
                    TransfromHelper.DoSlide(e, animation);
                }
            }
            catch (Exception ex) // //
            {
#if debug
                Console.WriteLine(ex);
#endif
            }
        }

        protected virtual Bitmap OnNonLinearTransfromNeeded()
        {
            if (ctrlBmp == null)
                return null;

            if (NonLinearTransfromNeeded == null && !animation.IsNonLinearTransformNeeded)
                return ctrlBmp; // No allocation, return existing bitmap //

            Bitmap bmp = null;
            try
            {
                // Instead of cloning every time, try to reuse if possible
                bmp = (Bitmap)ctrlBmp.Clone(); // Still need to clone for pixel operations //

                const int bytesPerPixel = 4;
                PixelFormat pxf = PixelFormat.Format32bppArgb;
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, pxf);
                IntPtr ptr = bmpData.Scan0;
                int numBytes = bmp.Width * bmp.Height * bytesPerPixel;

                // Only allocate buffer if size changed //
                if (pixelsBuffer == null || pixelsBuffer.Length != numBytes)
                    pixelsBuffer = new byte[numBytes];
                System.Runtime.InteropServices.Marshal.Copy(ptr, pixelsBuffer, 0, numBytes);

                var e = new NonLinearTransfromNeededEventArg()
                {
                    CurrentTime = CurrentTime,
                    ClientRectangle = DoubleBitmap.ClientRectangle,
                    Pixels = pixelsBuffer,
                    Stride = bmpData.Stride
                };

                if (NonLinearTransfromNeeded != null)
                    NonLinearTransfromNeeded(this, e);
                else
                    e.UseDefaultTransform = true;

                if (e.UseDefaultTransform)
                {
                    TransfromHelper.DoBlind(e, animation);
                    TransfromHelper.DoTransparent(e, animation);
                }

                System.Runtime.InteropServices.Marshal.Copy(pixelsBuffer, 0, ptr, numBytes);
                bmp.UnlockBits(bmpData);
            }
            catch (Exception ex) // //
            {
#if debug
                Console.WriteLine(ex);
#endif
            }

            return bmp;
        }

        public void EndUpdate()
        {
            var bmp = GetBackground(AnimatedControl, true, true);

            if (animation.AnimateOnlyDifferences)
                TransfromHelper.CalcDifference(bmp, BgBmp);

            if (ctrlBmp != null)
            {
                ctrlBmp.Dispose(); // Dispose previous bitmap aggressively //
                ctrlBmp = null;
            }
            ctrlBmp = bmp;
            mode = AnimateMode.Update;
        }

        public bool IsCompleted
        {
            get { return (TimeStep >= 0f && CurrentTime >= animation.MaxTime) || (TimeStep <= 0f && CurrentTime <= animation.MinTime); }
        }

        internal void BuildNextFrame()
        {
            if (mode == AnimateMode.BeginUpdate)
                return;
            DoubleBitmap.Invalidate();
        }
    }
}