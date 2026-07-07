using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace AnimatorNS
{
    /// <summary>
    /// DoubleBitmap displays animation
    /// </summary>
    public class Controller
    {
        protected Bitmap BgBmp
        {
            get
            {
                if (DoubleBitmap == null) return null;
                return (DoubleBitmap as IFakeControl)?.BgBmp;
            }
            set
            {
                if (DoubleBitmap == null) return;
                var fake = DoubleBitmap as IFakeControl;
                if (fake != null) fake.BgBmp = value;
            }
        }
        public Bitmap Frame
        {
            get
            {
                if (DoubleBitmap == null) return null;
                return (DoubleBitmap as IFakeControl)?.Frame;
            }
            set
            {
                if (DoubleBitmap == null) return;
                var fake = DoubleBitmap as IFakeControl;
                if (fake != null) fake.Frame = value;
            }
        }
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

        public void Dispose()
        {
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
            pixelsBuffer = null;
            Hide();

            // Clear bitmap cache to prevent memory leaks
            BitmapCache.Clear();
        }

        public void Hide()
        {
            if (DoubleBitmap != null)
            {
                Control db = DoubleBitmap;
                Control originalControl = AnimatedControl;
                AnimateMode originalMode = mode;
                Control parent = db.Parent;
                Rectangle bounds = db.Bounds;

                DoubleBitmap = null;

                try
                {
                    // Force immediate cleanup on UI thread
                    if (db.InvokeRequired)
                    {
                        try
                        {
                            db.BeginInvoke(new MethodInvoker(() =>
                            {
                                try
                                {
                                    // Remove from parent first
                                    if (db.Parent != null && !db.Parent.IsDisposed)
                                    {
                                        db.Parent.Controls.Remove(db);
                                    }

                                    // Restore original control
                                    if (originalControl != null && !originalControl.IsDisposed)
                                    {
                                        if (originalMode == AnimateMode.Hide)
                                            originalControl.Visible = false;
                                        else if (originalMode == AnimateMode.Show || originalMode == AnimateMode.Update)
                                            if (!originalControl.Visible)
                                                originalControl.Visible = true;
                                    }

                                    // Hide and dispose
                                    if (db is not null && !db.IsDisposed)
                                    {
                                        db?.Visible = false;
                                        db?.Dispose();
                                    }
                                }
                                catch { }
                            }));
                        }
                        catch { }
                    }
                    else
                    {
                        try
                        {
                            // Remove from parent first
                            if (db.Parent != null && !db.Parent.IsDisposed)
                            {
                                db.Parent.Controls.Remove(db);
                            }

                            // Restore original control
                            if (originalControl != null && !originalControl.IsDisposed)
                            {
                                if (originalMode == AnimateMode.Hide)
                                    originalControl.Visible = false;
                                else if (originalMode == AnimateMode.Show || originalMode == AnimateMode.Update)
                                    if (!originalControl.Visible)
                                        originalControl.Visible = true;
                            }

                            // Hide and dispose
                            if (!db.IsDisposed)
                            {
                                db.Visible = false;
                                db.Dispose();
                            }
                        }
                        catch { }
                    }

                    // Force parent repaint with BeginInvoke to avoid threading issues
                    if (parent != null && !parent.IsDisposed)
                    {
                        try
                        {
                            parent.BeginInvoke(new MethodInvoker(() =>
                            {
                                try
                                {
                                    if (!parent.IsDisposed)
                                    {
                                        parent.Invalidate(bounds);
                                        parent.Update();

                                        if (parent is WinPaletter.UI.WP.TablessControl)
                                        {
                                            parent.Refresh();
                                            foreach (Control ctrl in parent.Controls)
                                            {
                                                if (!ctrl.IsDisposed)
                                                {
                                                    ctrl.Invalidate();
                                                    ctrl.Update();
                                                }
                                            }
                                            try
                                            {
                                                User32.InvalidateRect(parent.Handle, IntPtr.Zero, true);
                                                User32.UpdateWindow(parent.Handle);
                                            }
                                            catch { }
                                        }
                                    }
                                }
                                catch { }
                            }));
                        }
                        catch { }
                    }
                }
                catch { }
            }
        }

        protected virtual Rectangle GetBounds()
        {
            return new Rectangle(
                AnimatedControl.Left - animation.Padding.Left,
                AnimatedControl.Top - animation.Padding.Top,
                AnimatedControl.Size.Width + animation.Padding.Left + animation.Padding.Right,
                AnimatedControl.Size.Height + animation.Padding.Top + animation.Padding.Bottom);
        }

        protected virtual Rectangle ControlRectToMyRect(Rectangle rect)
        {
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

            // Store the animated control reference in the fake control's Tag so we can identify which control this fake belongs to
            DoubleBitmap.Tag = control;

            // Store the mode for later reference
            if (DoubleBitmap is DoubleBitmapControl dbc)
            {
                // We can use the Name property to identify this fake control
                dbc.Name = $"FakeControl_{control.GetHashCode()}";
            }

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
            catch
            {
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
            // Check if DoubleBitmap is null (disposed) - if so, skip painting
            if (DoubleBitmap == null || DoubleBitmap.IsDisposed)
                return;

            var oldFrame = Frame;
            Frame = null;

            if (mode == AnimateMode.BeginUpdate)
                return;

            Frame = OnNonLinearTransfromNeeded();

            if (oldFrame != Frame && oldFrame != null)
            {
                oldFrame.Dispose();
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
            // Check if DoubleBitmap is null (disposed) - if so, skip
            if (DoubleBitmap == null || DoubleBitmap.IsDisposed) return;

            FramePainted?.Invoke(this, e);
        }

        protected virtual Bitmap GetBackground(Control ctrl, bool includeForeground = false, bool clip = false)
        {
            if (ctrl is Form) return GetScreenBackground();

            // Check if control and parent are valid
            if (ctrl == null || ctrl.IsDisposed || ctrl.Parent == null || ctrl.Parent.IsDisposed)
                return null;

            var bounds = GetBounds();
            int w = bounds.Width;
            int h = bounds.Height;

            // Ensure valid dimensions
            if (w <= 0) w = 1;
            if (h <= 0) h = 1;

            // Create a new bitmap directly instead of using cache to avoid sharing issues
            Bitmap bmp;
            try
            {
                bmp = new Bitmap(w, h);
            }
            catch
            {
                return null;
            }

            try
            {
                var clientRect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                using (Graphics G = Graphics.FromImage(bmp))
                {
                    // Set high quality rendering
                    G.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    G.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                    PaintEventArgs ea = new(G, clientRect);
                    if (clip)
                    {
                        if (CustomClipRect == default(Rectangle))
                            G.SetClip(new Rectangle(0, 0, w, h));
                        else
                            G.SetClip(CustomClipRect);
                    }

                    // Use a safer iteration approach with a copy of the controls list
                    Control.ControlCollection controls = ctrl.Parent.Controls;
                    if (controls == null) return bmp;

                    // Create a list of controls to iterate safely
                    List<Control> controlsList = [];
                    try
                    {
                        foreach (Control c in controls)
                        {
                            if (c != null && !c.IsDisposed) controlsList.Add(c);
                        }
                    }
                    catch { return bmp; }

                    for (int i = controlsList.Count - 1; i >= 0; i--)
                    {
                        var c = controlsList[i];
                        if (c == null || c.IsDisposed) continue;

                        if (c == ctrl && !includeForeground) break;

                        if (c.Visible && !c.IsDisposed && c.Bounds.IntersectsWith(bounds))
                        {
                            try
                            {
                                int cw = Math.Max(1, c.Width);
                                int ch = Math.Max(1, c.Height);
                                using (Bitmap cb = new(cw, ch))
                                {
                                    c.DrawToBitmap(cb, new Rectangle(0, 0, cw, ch));
                                    if (cb != null)
                                    {
                                        G.DrawImage(cb, c.Left - bounds.Left, c.Top - bounds.Top, c.Width, c.Height);
                                    }
                                }
                            }
                            catch { /* Skip this control if it can't be rendered */ }
                        }
                        if (c == ctrl) break;
                    }
                }
                return bmp;
            }
            catch
            {
                try { bmp?.Dispose(); } catch { }
                return null;
            }
        }

        private Bitmap GetScreenBackground()
        {
            // Check if DoubleBitmap is null (disposed) - if so, return null
            if (DoubleBitmap == null || DoubleBitmap.IsDisposed)
                return null;

            var size = Screen.PrimaryScreen.Bounds.Size;
            try
            {
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
            catch
            {
                return null;
            }
        }

        protected virtual Bitmap GetForeground(Control ctrl)
        {
            Bitmap bmp = null;

            if (!ctrl.IsDisposed)
            {
                if (ctrl.Parent == null)
                {
                    bmp = BitmapCache.GetOrCreate(ctrl.Width + animation.Padding.Horizontal, ctrl.Height + animation.Padding.Vertical, "foreground");
                    ctrl.DrawToBitmap(bmp, new Rectangle(animation.Padding.Left, animation.Padding.Top, ctrl.Width, ctrl.Height));
                }
                else
                {
                    // Check if DoubleBitmap is null (disposed) - if so, return null
                    if (DoubleBitmap == null || DoubleBitmap.IsDisposed)
                        return null;
                    bmp = BitmapCache.GetOrCreate(DoubleBitmap.Width, DoubleBitmap.Height, "foreground");
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
            catch { }
        }

        protected virtual Bitmap OnNonLinearTransfromNeeded()
        {
            if (ctrlBmp == null)
                return null;

            // Check if DoubleBitmap is null (disposed) - if so, return null
            if (DoubleBitmap == null || DoubleBitmap.IsDisposed)
                return null;

            if (NonLinearTransfromNeeded == null && !animation.IsNonLinearTransformNeeded)
                return ctrlBmp;

            Bitmap bmp = null;
            byte[] sourcePixels = null;
            byte[] pooledBuffer = null;
            try
            {
                bmp = (Bitmap)ctrlBmp.Clone();

                const int bytesPerPixel = 4;
                PixelFormat pxf = PixelFormat.Format32bppArgb;
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, pxf);
                IntPtr ptr = bmpData.Scan0;
                int numBytes = bmp.Width * bmp.Height * bytesPerPixel;

                // Use pooled buffer instead of instance field to reduce memory footprint
                pooledBuffer = AnimatorPools.GetByteArray(numBytes);
                System.Runtime.InteropServices.Marshal.Copy(ptr, pooledBuffer, 0, numBytes);

                // Create source pixels buffer for blur if needed
                if (animation.BlurRadius > 0f)
                {
                    sourcePixels = AnimatorPools.GetByteArray(numBytes);
                    System.Runtime.InteropServices.Marshal.Copy(ptr, sourcePixels, 0, numBytes);
                }

                // CRITICAL FIX: Check DoubleBitmap again before creating the event args
                if (DoubleBitmap == null || DoubleBitmap.IsDisposed)
                {
                    bmp.UnlockBits(bmpData);
                    return bmp; // Return the unmodified bitmap instead of proceeding
                }

                var e = new NonLinearTransfromNeededEventArg()
                {
                    CurrentTime = CurrentTime,
                    ClientRectangle = DoubleBitmap.ClientRectangle,
                    Pixels = pooledBuffer,
                    SourcePixels = sourcePixels,
                    Stride = bmpData.Stride
                };

                if (NonLinearTransfromNeeded != null)
                    NonLinearTransfromNeeded(this, e);
                else
                    e.UseDefaultTransform = true;

                if (e.UseDefaultTransform)
                {
                    TransfromHelper.DoBlind(e, animation);

                    // Apply zoom FIRST (using source pixels as reference)
                    TransfromHelper.DoZoom(e, animation, mode);

                    // Apply transparency AFTER zoom (so it affects the zoomed pixels)
                    TransfromHelper.DoTransparent(e, animation);
                }

                System.Runtime.InteropServices.Marshal.Copy(pooledBuffer, 0, ptr, numBytes);
                bmp.UnlockBits(bmpData);
            }
            catch
            {
                // Cleanup on exception
                if (bmp != null)
                {
                    try { bmp.Dispose(); } catch { }
                    bmp = null;
                }
            }
            finally
            {
                // Return buffers to pool
                if (pooledBuffer != null)
                    AnimatorPools.ReturnByteArray(pooledBuffer);
                if (sourcePixels != null)
                    AnimatorPools.ReturnByteArray(sourcePixels);
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
                ctrlBmp.Dispose();
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
            // Check if DoubleBitmap is null (disposed) - if so, skip
            if (DoubleBitmap == null || DoubleBitmap.IsDisposed)
                return;
            DoubleBitmap.Invalidate();
        }
    }
}