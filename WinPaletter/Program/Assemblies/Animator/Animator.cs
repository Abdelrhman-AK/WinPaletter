// Optimized for .NET Framework 2.0, improved memory usage and performance

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.UI.WP;
using Timer = System.Windows.Forms.Timer;

namespace AnimatorNS
{
    [ProvideProperty("Decoration", typeof(Control))]
    public class Animator : Component, IExtenderProvider
    {
        IContainer components = null;
        protected readonly List<QueueItem> queue = new List<QueueItem>();
        private Thread thread;
        private Timer timer;
        private volatile bool running = true;

        public event EventHandler<AnimationCompletedEventArg> AnimationCompleted;
        public event EventHandler AllAnimationsCompleted;
        public event EventHandler<TransfromNeededEventArg> TransfromNeeded;
        public event EventHandler<NonLinearTransfromNeededEventArg> NonLinearTransfromNeeded;
        public event EventHandler<MouseEventArgs> MouseDown;
        public event EventHandler<PaintEventArgs> FramePainted;

        [DefaultValue(1500)]
        public int MaxAnimationTime { get; set; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Animation DefaultAnimation { get; set; }

        [DefaultValue(typeof(Cursor), "Default")]
        public Cursor Cursor { get; set; }

        public bool IsCompleted
        {
            get { lock (queue) return queue.Count == 0; }
        }

        [DefaultValue(10)]
        public int Interval { get; set; }

        AnimationType animationType;
        public AnimationType AnimationType
        {
            get { return animationType; }
            set { animationType = value; InitDefaultAnimation(animationType); }
        }

        public Animator()
        {
            Init();
        }

        public Animator(IContainer container)
        {
            container.Add(this);
            Init();
        }

        protected virtual void Init()
        {
            AnimationType = AnimatorNS.AnimationType.Fade;
            DefaultAnimation = new Animation();
            MaxAnimationTime = 1500;
            TimeStep = 0.02f;
            Interval = 10;

            timer = new Timer();
            timer.Tick += timer_Tick;
            timer.Interval = 1;
            timer.Start();
        }

        private void Start()
        {
            thread = new Thread(Work);
            thread.IsBackground = true;
            thread.Name = "Animator thread";
            thread.Start();
        }

        Control invokerControl;

        void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            invokerControl = new Control();
            invokerControl.CreateControl();
            Start();
        }

        /// <summary>
        /// Forces strict cleanup of all fake controls
        /// </summary>
        public void ForceCleanupFakeControls()
        {
            try
            {
                List<QueueItem> items;
                lock (queue)
                {
                    items = new List<QueueItem>(queue);
                }

                foreach (var item in items)
                {
                    if (item.controller != null)
                    {
                        try { item.controller.Dispose(); } catch { }
                        item.controller = null;
                    }

                    if (item.control != null && !item.control.IsDisposed)
                    {
                        if (item.control is IFakeControl ||
                            item.control is DoubleBitmapControl ||
                            item.control is DoubleBitmapForm)
                        {
                            try
                            {
                                if (item.control.Parent != null && !item.control.Parent.IsDisposed)
                                {
                                    // Remove from parent
                                    item.control.Parent.Controls.Remove(item.control);

                                    // If parent is TablessControl, force repaint
                                    if (item.control.Parent is WinPaletter.UI.WP.TablessControl tablessParent)
                                    {
                                        tablessParent.Invalidate();
                                        tablessParent.Update();
                                        tablessParent.Refresh();
                                        try
                                        {
                                            User32.InvalidateRect(tablessParent.Handle, IntPtr.Zero, true);
                                            User32.UpdateWindow(tablessParent.Handle);
                                        }
                                        catch { }
                                    }
                                }
                                item.control.Visible = false;
                                item.control.Dispose();
                            }
                            catch { }
                        }
                    }
                }

                // Clear queue
                lock (queue)
                {
                    queue.Clear();
                }

                // Clear requests
                lock (requests)
                {
                    requests.Clear();
                }
            }
            catch { }
        }

        /// <summary>
        /// Ensures the worker thread, timer, and helper control are all
        /// cleaned up to avoid leaks and unnecessary GC pressure.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Stop worker loop
                running = false;

                // Force cleanup of all fake controls
                ForceCleanupFakeControls();

                // Clear any pending animations
                ClearQueue();

                // Gracefully stop background thread
                if (thread != null)
                {
                    try { thread.Join(100); } catch { /* ignore */ }
                    thread = null;
                }

                // Dispose timer
                if (timer != null)
                {
                    try
                    {
                        timer.Stop();
                        timer.Tick -= timer_Tick;
                        timer.Dispose();
                    }
                    catch { /* ignore */ }
                    timer = null;
                }

                // Dispose helper control used for marshaling to UI thread
                if (invokerControl != null)
                {
                    try { invokerControl.Dispose(); } catch { /* ignore */ }
                    invokerControl = null;
                }

                // Clear bitmap cache
                try { BitmapCache.Clear(); } catch { }

                // Clear object pools
                try { AnimatorPools.ClearAll(); } catch { }
            }

            base.Dispose(disposing);
        }

        readonly List<QueueItem> completed = new List<QueueItem>();
        readonly List<QueueItem> actived = new List<QueueItem>();

        // Reusable collections to reduce GC pressure
        readonly Dictionary<Control, QueueItem> controlDict = new Dictionary<Control, QueueItem>();

        void Work()
        {
            while (running)
            {
                Thread.Sleep(Interval);
                try
                {
                    int count;
                    completed.Clear();
                    actived.Clear();

                    lock (queue)
                    {
                        count = queue.Count;
                        bool wasActive = false;

                        foreach (var item in queue)
                        {
                            // Check if control is disposed - if so, mark as completed to clean up
                            if (item.control == null || item.control.IsDisposed)
                            {
                                completed.Add(item);
                                continue;
                            }

                            if (item.IsActive) wasActive = true;

                            if (item.controller != null && item.controller.IsCompleted)
                                completed.Add(item);
                            else if (item.IsActive)
                            {
                                if ((DateTime.Now - item.ActivateTime).TotalMilliseconds > MaxAnimationTime)
                                    completed.Add(item);
                                else
                                    actived.Add(item);
                            }
                        }
                        if (!wasActive)
                        {
                            foreach (var item in queue)
                            {
                                if (!item.IsActive)
                                {
                                    actived.Add(item);
                                    item.IsActive = true;
                                    break;
                                }
                            }
                        }
                    }

                    foreach (var item in completed)
                        OnCompleted(item);

                    foreach (var item in actived)
                    {
                        try
                        {
                            invokerControl.BeginInvoke(new MethodInvoker(() => DoAnimation(item)));
                        }
                        catch
                        {
                            OnCompleted(item);
                        }
                    }

                    if (count == 0)
                    {
                        if (completed.Count > 0)
                            OnAllAnimationsCompleted();
                        CheckRequests();
                    }
                }
                catch
                {
                    // form was closed
                }
            }
        }

        readonly List<QueueItem> toRemove = new List<QueueItem>();

        private void CheckRequests()
        {
            lock (requests)
            {
                toRemove.Clear();
                controlDict.Clear();

                foreach (var item in requests)
                {
                    if (item.control != null)
                    {
                        if (controlDict.ContainsKey(item.control))
                            toRemove.Add(controlDict[item.control]);
                        controlDict[item.control] = item;
                    }
                    else
                        toRemove.Add(item);
                }

                foreach (var item in controlDict.Values)
                {
                    if (item.control != null && !IsStateOK(item.control, item.mode))
                    {
                        if (invokerControl != null)
                            RepairState(item.control, item.mode);
                    }
                    else
                        toRemove.Add(item);
                }

                foreach (var item in toRemove)
                    requests.Remove(item);
            }
        }

        bool IsStateOK(Control control, AnimateMode mode)
        {
            switch (mode)
            {
                case AnimateMode.Hide: return !control.Visible;
                case AnimateMode.Show: return control.Visible;
            }
            return true;
        }

        void RepairState(Control control, AnimateMode mode)
        {
            invokerControl.Invoke(new MethodInvoker(() =>
            {
                try
                {
                    switch (mode)
                    {
                        case AnimateMode.Hide:
                            control.Visible = false;
                            break;
                        case AnimateMode.Show:
                            control.Visible = true;
                            break;
                    }
                }
                catch
                {
                    // form was closed
                }
            }));
        }

        private void DoAnimation(QueueItem item)
        {
            lock (item)
            {
                try
                {
                    // Check if control is disposed or invalid
                    if (item.control == null || item.control.IsDisposed || !item.control.IsHandleCreated)
                    {
                        if (item.controller != null)
                            item.controller.Dispose();
                        OnCompleted(item);
                        return;
                    }

                    if (item.controller == null)
                    {
                        item.controller = CreateDoubleBitmap(item.control, item.mode, item.animation, item.clipRectangle);
                    }

                    // Additional check: if controller was created but DoubleBitmap is null, handle gracefully
                    if (item.controller != null && item.controller.DoubleBitmap == null)
                    {
                        OnCompleted(item);
                        return;
                    }

                    if (item.controller.IsCompleted)
                        return;
                    item.controller.BuildNextFrame();
                }
                catch
                {
                    if (item.controller != null)
                        item.controller.Dispose();
                    OnCompleted(item);
                }
            }
        }

        private void InitDefaultAnimation(AnimatorNS.AnimationType animationType)
        {
            switch (animationType)
            {
                case AnimationType.Custom: break;
                case AnimationType.HorizSlide: DefaultAnimation = Animation.HorizSlide; break;
                case AnimationType.VertSlide: DefaultAnimation = Animation.VertSlide; break;
                case AnimationType.Fade: DefaultAnimation = Animation.Fade; break;
                case AnimationType.FadeZoom: DefaultAnimation = Animation.FadeZoom; break;
                case AnimationType.VertBlind: DefaultAnimation = Animation.VertBlind; break;
                case AnimationType.HorizBlind: DefaultAnimation = Animation.HorizBlind; break;
            }
        }

        [DefaultValue(0.02f)]
        public float TimeStep { get; set; }

        public void Show(Control control, bool parallel = false, Animation animation = null)
        {
            AddToQueue(control, AnimateMode.Show, parallel, animation);
        }

        public void ShowSync(Control control, bool parallel = false, Animation animation = null)
        {
            Show(control, parallel, animation);
            WaitAnimation(control);
        }

        public void Hide(Control control, bool parallel = false, Animation animation = null)
        {
            AddToQueue(control, AnimateMode.Hide, parallel, animation);
        }

        public void HideSync(Control control, bool parallel = false, Animation animation = null)
        {
            Hide(control, parallel, animation);
            WaitAnimation(control);
        }

        public void BeginUpdate(Control control, bool parallel = false, Animation animation = null, Rectangle clipRectangle = default(Rectangle))
        {
            AddToQueue(control, AnimateMode.BeginUpdate, parallel, animation, clipRectangle);

            bool wait;
            do
            {
                wait = false;
                lock (queue)
                {
                    foreach (var item in queue)
                        if (item.control == control && item.mode == AnimateMode.BeginUpdate && item.controller == null)
                            wait = true;
                }
                if (wait)
                    Application.DoEvents();
            } while (wait);
        }

        public void EndUpdate(Control control)
        {
            lock (queue)
            {
                foreach (var item in queue)
                    if (item.control == control && item.mode == AnimateMode.BeginUpdate)
                    {
                        item.controller.EndUpdate();
                        item.mode = AnimateMode.Update;
                    }
            }
        }

        public void EndUpdateSync(Control control)
        {
            EndUpdate(control);
            WaitAnimation(control);
        }

        public void WaitAllAnimations()
        {
            while (!IsCompleted)
                Application.DoEvents();
        }

        public void WaitAnimation(Control animatedControl)
        {
            while (true)
            {
                bool flag = false;
                lock (queue)
                {
                    foreach (var item in queue)
                        if (item.control == animatedControl)
                        {
                            flag = true;
                            break;
                        }
                }
                if (!flag)
                    return;
                Application.DoEvents();
            }
        }

        readonly List<QueueItem> requests = new List<QueueItem>();

        void OnCompleted(QueueItem item)
        {
            if (item.controller != null)
            {
                try
                {
                    if (item.control != null && !item.control.IsDisposed && item.control.IsHandleCreated)
                    {
                        if (item.mode == AnimateMode.Show && !item.control.Visible)
                        {
                            item.control.Visible = true;
                            item.control.Invalidate();
                            item.control.Update();
                        }
                        else if (item.mode == AnimateMode.Hide && item.control.Visible)
                        {
                            item.control.Visible = false;
                        }

                        if (item.control.Parent != null && !item.control.Parent.IsDisposed)
                        {
                            // Check for TablessControl in parent hierarchy
                            Control tablessControl = null;
                            Control current = item.control.Parent;
                            while (current != null)
                            {
                                if (current is WinPaletter.UI.WP.TablessControl)
                                {
                                    tablessControl = current;
                                    break;
                                }
                                current = current.Parent;
                            }

                            if (tablessControl != null)
                            {
                                // Force TablessControl to repaint
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
                                    if (!ctrl.IsDisposed && ctrl != item.control)
                                    {
                                        ctrl.Invalidate();
                                        ctrl.Update();
                                    }
                                }
                            }
                            else
                            {
                                item.control.Parent.Invalidate(item.control.Bounds);
                                item.control.Parent.Update();
                            }
                        }
                    }

                    item.controller.Dispose();
                }
                catch { }

                item.controller = null;
            }

            lock (queue)
                queue.Remove(item);

            OnAnimationCompleted(new AnimationCompletedEventArg { Animation = item.animation, Control = item.control, Mode = item.mode });
        }

        public void AddToQueue(Control control, AnimateMode mode, bool parallel = true, Animation animation = null, Rectangle clipRectangle = default(Rectangle))
        {
            if (animation == null)
                animation = DefaultAnimation;

            if (control is IFakeControl)
            {
                control.Visible = false;
                return;
            }

            // Check if control is disposed or being disposed
            if (control.IsDisposed || !control.IsHandleCreated)
            {
                OnCompleted(new QueueItem { control = control, mode = mode });
                return;
            }

            // IMPORTANT: Clean up any existing fake controls for this control
            // This is critical for TablessControl scenarios
            CleanupExistingFakeControls(control);

            var item = new QueueItem() { animation = animation, control = control, IsActive = parallel, mode = mode, clipRectangle = clipRectangle };

            // Check if there's already an animation for this control in the queue
            lock (queue)
            {
                foreach (var existing in queue)
                {
                    if (existing.control == control)
                    {
                        lock (requests)
                        {
                            requests.Remove(existing);
                        }
                        if (existing.controller != null)
                        {
                            try
                            {
                                existing.controller.Dispose();
                            }
                            catch { }
                            existing.controller = null;
                        }
                        queue.Remove(existing);
                        break;
                    }
                }
            }

            switch (mode)
            {
                case AnimateMode.Show:
                    if (control.Visible)
                    {
                        OnCompleted(new QueueItem { control = control, mode = mode });
                        return;
                    }
                    break;
                case AnimateMode.Hide:
                    if (!control.Visible)
                    {
                        OnCompleted(new QueueItem { control = control, mode = mode });
                        return;
                    }
                    break;
            }

            lock (queue)
                queue.Add(item);
            lock (requests)
                requests.Add(item);
        }

        /// <summary>
        /// Cleans up any existing fake controls for a given control
        /// </summary>
        private void CleanupExistingFakeControls(Control control)
        {
            try
            {
                if (control == null || control.IsDisposed)
                    return;

                // Check the parent (which could be TablessControl)
                Control parent = control.Parent;
                if (parent == null || parent.IsDisposed)
                    return;

                // Look for any DoubleBitmapControl or IFakeControl in the parent's children
                List<Control> toRemove = new List<Control>();
                foreach (Control child in parent.Controls)
                {
                    if (child is DoubleBitmapControl || child is DoubleBitmapForm || child is IFakeControl)
                    {
                        // Check if this fake control was created for our control
                        // We check bounds or other properties to identify it
                        if (child is DoubleBitmapControl dbc)
                        {
                            // Check if this fake control covers the same area as our control
                            Rectangle controlBounds = new Rectangle(
                                control.Left,
                                control.Top,
                                control.Width,
                                control.Height);

                            // If the fake control overlaps with our control, remove it
                            if (child.Bounds.IntersectsWith(controlBounds))
                            {
                                toRemove.Add(child);
                            }
                        }
                        else if (child is IFakeControl)
                        {
                            // For other fake controls, check if they overlap
                            if (child.Bounds.IntersectsWith(control.Bounds))
                            {
                                toRemove.Add(child);
                            }
                        }
                    }
                }

                // Remove and dispose fake controls
                foreach (Control fake in toRemove)
                {
                    try
                    {
                        if (!fake.IsDisposed)
                        {
                            fake.Visible = false;
                            parent.Controls.Remove(fake);
                            fake.Dispose();
                        }
                    }
                    catch { }
                }

                // Also check if there are any fake controls in the queue that need cleanup
                lock (queue)
                {
                    List<QueueItem> itemsToRemove = new List<QueueItem>();
                    foreach (var item in queue)
                    {
                        if (item.control == control && item.controller != null)
                        {
                            try
                            {
                                if (item.controller.DoubleBitmap != null &&
                                    !item.controller.DoubleBitmap.IsDisposed)
                                {
                                    // This fake control is still in the queue, force cleanup
                                    if (item.controller.DoubleBitmap.Parent != null &&
                                        !item.controller.DoubleBitmap.Parent.IsDisposed)
                                    {
                                        item.controller.DoubleBitmap.Parent.Controls.Remove(
                                            item.controller.DoubleBitmap);
                                    }
                                    item.controller.DoubleBitmap.Dispose();
                                }
                                item.controller.Dispose();
                            }
                            catch { }
                            item.controller = null;
                            itemsToRemove.Add(item);
                        }
                    }
                    foreach (var item in itemsToRemove)
                    {
                        queue.Remove(item);
                    }
                }
            }
            catch { }
        }

        private Controller CreateDoubleBitmap(Control control, AnimateMode mode, Animation animation, Rectangle clipRect)
        {
            var controller = new Controller(control, mode, animation, TimeStep, clipRect);
            controller?.TransfromNeeded += OnTransformNeeded;
            if (NonLinearTransfromNeeded != null)
                controller?.NonLinearTransfromNeeded += OnNonLinearTransfromNeeded;
            controller?.MouseDown += OnMouseDown;
            controller?.DoubleBitmap.Cursor = Cursor;
            controller?.FramePainted += OnFramePainted;
            return controller;
        }

        void OnFramePainted(object sender, PaintEventArgs e)
        {
            if (FramePainted != null)
                FramePainted(sender, e);
        }

        protected virtual void OnMouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                var db = (Controller)sender;
                var l = e.Location;
                l.Offset(db.DoubleBitmap.Left - db.AnimatedControl.Left, db.DoubleBitmap.Top - db.AnimatedControl.Top);
                if (MouseDown != null)
                    MouseDown(sender, new MouseEventArgs(e.Button, e.Clicks, l.X, l.Y, e.Delta));
            }
            catch { }
        }

        protected virtual void OnNonLinearTransfromNeeded(object sender, NonLinearTransfromNeededEventArg e)
        {
            if (NonLinearTransfromNeeded != null)
                NonLinearTransfromNeeded(this, e);
            else
                e.UseDefaultTransform = true;
        }

        protected virtual void OnTransformNeeded(object sender, TransfromNeededEventArg e)
        {
            if (TransfromNeeded != null)
                TransfromNeeded(this, e);
            else
                e.UseDefaultMatrix = true;
        }

        public void ClearQueue()
        {
            List<QueueItem> items;
            lock (queue)
            {
                items = new List<QueueItem>(queue);
                queue.Clear();
            }

            foreach (var item in items)
            {
                // Dispose controller first to ensure overlays are removed
                if (item.controller != null)
                {
                    try
                    {
                        // Ensure control state is restored before disposing
                        if (item.control != null && !item.control.IsDisposed)
                        {
                            if (item.mode == AnimateMode.Hide && !item.control.Visible)
                            {
                                // Already hidden
                            }
                            else if (item.mode == AnimateMode.Show && item.control.Visible)
                            {
                                // Already visible
                            }
                            else if (item.mode == AnimateMode.Hide)
                            {
                                // Force hide for Hide animations
                                if (item.control.InvokeRequired)
                                {
                                    item.control.Invoke(new MethodInvoker(() =>
                                    {
                                        try
                                        {
                                            if (!item.control.IsDisposed)
                                                item.control.Visible = false;
                                        }
                                        catch { }
                                    }));
                                }
                                else
                                {
                                    try
                                    {
                                        if (!item.control.IsDisposed)
                                            item.control.Visible = false;
                                    }
                                    catch { }
                                }
                            }
                            else if (item.mode == AnimateMode.Show || item.mode == AnimateMode.Update)
                            {
                                // Force show for Show/Update animations
                                if (item.control.InvokeRequired)
                                {
                                    item.control.Invoke(new MethodInvoker(() =>
                                    {
                                        try
                                        {
                                            if (!item.control.IsDisposed)
                                                item.control.Visible = true;
                                        }
                                        catch { }
                                    }));
                                }
                                else
                                {
                                    try
                                    {
                                        if (!item.control.IsDisposed)
                                            item.control.Visible = true;
                                    }
                                    catch { }
                                }
                            }
                        }

                        item.controller.Dispose();
                    }
                    catch { }
                    item.controller = null;
                }
                else
                {
                    // If controller is null, we still need to restore the control state
                    if (item.control != null && !item.control.IsDisposed)
                    {
                        try
                        {
                            if (item.control.InvokeRequired)
                            {
                                item.control.Invoke(new MethodInvoker(() =>
                                {
                                    try
                                    {
                                        if (!item.control.IsDisposed)
                                        {
                                            switch (item.mode)
                                            {
                                                case AnimateMode.Hide:
                                                    if (item.control.Visible)
                                                        item.control.Visible = false;
                                                    break;
                                                case AnimateMode.Show:
                                                case AnimateMode.Update:
                                                    if (!item.control.Visible)
                                                        item.control.Visible = true;
                                                    break;
                                            }
                                        }
                                    }
                                    catch { }
                                }));
                            }
                            else
                            {
                                switch (item.mode)
                                {
                                    case AnimateMode.Hide:
                                        if (item.control.Visible)
                                            item.control.Visible = false;
                                        break;
                                    case AnimateMode.Show:
                                    case AnimateMode.Update:
                                        if (!item.control.Visible)
                                            item.control.Visible = true;
                                        break;
                                }
                            }
                        }
                        catch { }
                    }
                }

                OnAnimationCompleted(new AnimationCompletedEventArg { Animation = item.animation, Control = item.control, Mode = item.mode });
            }

            if (items.Count > 0)
                OnAllAnimationsCompleted();
        }

        protected virtual void OnAnimationCompleted(AnimationCompletedEventArg e)
        {
            // Strict cleanup: Ensure any fake controls are properly disposed
            if (e.Control != null && !e.Control.IsDisposed)
            {
                // Check if this is a fake control that wasn't properly cleaned up
                if (e.Control is IFakeControl fakeControl)
                {
                    try
                    {
                        // Force cleanup of fake control
                        if (e.Control.Parent != null && !e.Control.Parent.IsDisposed)
                        {
                            // Remove from parent
                            e.Control.Parent.Controls.Remove(e.Control);
                        }

                        // Hide and dispose
                        e.Control.Visible = false;
                        e.Control.Dispose();
                    }
                    catch { }
                }

                // If the control is a DoubleBitmapControl or DoubleBitmapForm, ensure it's disposed
                if (e.Control is DoubleBitmapControl || e.Control is DoubleBitmapForm)
                {
                    try
                    {
                        if (!e.Control.IsDisposed)
                        {
                            e.Control.Visible = false;
                            if (e.Control.Parent != null && !e.Control.Parent.IsDisposed)
                            {
                                e.Control.Parent.Controls.Remove(e.Control);
                            }
                            e.Control.Dispose();
                        }
                    }
                    catch { }
                }

                // Ensure TablessControl parent is properly repainted
                if (e.Control.Parent is WinPaletter.UI.WP.TablessControl tablessParent)
                {
                    try
                    {
                        if (!tablessParent.IsDisposed)
                        {
                            tablessParent.Invalidate();
                            tablessParent.Update();
                            tablessParent.Refresh();

                            // Force repaint of all child controls
                            foreach (Control ctrl in tablessParent.Controls)
                            {
                                if (!ctrl.IsDisposed && ctrl != e.Control)
                                {
                                    ctrl.Invalidate();
                                    ctrl.Update();
                                }
                            }

                            // Use Win32 to force full redraw
                            try
                            {
                                User32.InvalidateRect(tablessParent.Handle, IntPtr.Zero, true);
                                User32.UpdateWindow(tablessParent.Handle);
                            }
                            catch { }
                        }
                    }
                    catch { }
                }
            }

            // Fire the event
            AnimationCompleted?.Invoke(this, e);
        }

        protected virtual void OnAllAnimationsCompleted()
        {
            // Strict cleanup: Force disposal of any remaining fake controls
            try
            {
                // Check all controls in the queue for any remaining fake controls
                List<QueueItem> itemsToCleanup = new List<QueueItem>();
                lock (queue)
                {
                    itemsToCleanup.AddRange(queue);
                }

                foreach (var item in itemsToCleanup)
                {
                    if (item.controller != null)
                    {
                        try
                        {
                            // Force dispose of controller which will clean up fake controls
                            item.controller.Dispose();
                        }
                        catch { }
                        item.controller = null;
                    }

                    // Check if the control is a fake control that needs cleanup
                    if (item.control != null && !item.control.IsDisposed)
                    {
                        if (item.control is IFakeControl ||
                            item.control is DoubleBitmapControl ||
                            item.control is DoubleBitmapForm)
                        {
                            try
                            {
                                // Force cleanup of fake control
                                if (item.control.Parent != null && !item.control.Parent.IsDisposed)
                                {
                                    item.control.Parent.Controls.Remove(item.control);
                                }
                                item.control.Visible = false;
                                item.control.Dispose();
                            }
                            catch { }
                        }
                    }
                }

                // Clean up requests list as well
                lock (requests)
                {
                    foreach (var item in requests)
                    {
                        if (item.control != null && !item.control.IsDisposed)
                        {
                            if (item.control is IFakeControl ||
                                item.control is DoubleBitmapControl ||
                                item.control is DoubleBitmapForm)
                            {
                                try
                                {
                                    if (item.control.Parent != null && !item.control.Parent.IsDisposed)
                                    {
                                        item.control.Parent.Controls.Remove(item.control);
                                    }
                                    item.control.Visible = false;
                                    item.control.Dispose();
                                }
                                catch { }
                            }
                        }
                    }
                    requests.Clear();
                }
            }
            catch { }

            // Fire the event
            AllAnimationsCompleted?.Invoke(this, EventArgs.Empty);
        }

        protected class QueueItem
        {
            public Animation animation;
            public Controller controller;
            public Control control;
            public DateTime ActivateTime { get; private set; }
            public AnimateMode mode;
            public Rectangle clipRectangle;

            public bool isActive;
            public bool IsActive
            {
                get { return isActive; }
                set
                {
                    if (isActive == value) return;
                    isActive = value;
                    if (value)
                        ActivateTime = DateTime.Now;
                }
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                if (control != null)
                    sb.Append(control.GetType().Name + " ");
                sb.Append(mode);
                return sb.ToString();
            }
        }

        public bool CanExtend(object extendee)
        {
            return extendee is Control;
        }
    }

    public enum DecorationType
    {
        None,
        BottomMirror,
        Custom
    }

    public class AnimationCompletedEventArg : EventArgs
    {
        public Animation Animation { get; set; }
        public Control Control { get; internal set; }
        public AnimateMode Mode { get; internal set; }
    }

    public class TransfromNeededEventArg : EventArgs
    {
        public TransfromNeededEventArg()
        {
            Matrix = new Matrix(1, 0, 0, 1, 0, 0);
        }

        public Matrix Matrix { get; set; }
        public float CurrentTime { get; internal set; }
        public Rectangle ClientRectangle { get; internal set; }
        public Rectangle ClipRectangle { get; internal set; }
        public Animation Animation { get; set; }
        public Control Control { get; internal set; }
        public AnimateMode Mode { get; internal set; }
        public bool UseDefaultMatrix { get; set; }
    }

    public class NonLinearTransfromNeededEventArg : EventArgs
    {
        public float CurrentTime { get; internal set; }
        public Rectangle ClientRectangle { get; internal set; }
        public byte[] Pixels { get; internal set; }
        public int Stride { get; internal set; }
        public Rectangle SourceClientRectangle { get; internal set; }
        public byte[] SourcePixels { get; internal set; }
        public int SourceStride { get; set; }
        public Animation Animation { get; set; }
        public Control Control { get; internal set; }
        public AnimateMode Mode { get; internal set; }
        public bool UseDefaultTransform { get; set; }
    }

    public enum AnimateMode
    {
        Show,
        Hide,
        Update,
        BeginUpdate
    }
}