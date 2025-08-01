using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace AnimatorNS
{
    [ProvideProperty("Decoration", typeof(Control))]
    public class Animator : Component, IExtenderProvider
    {
        IContainer components = null;

        // Object pools for lists to reduce allocations
        private readonly List<QueueItem> completed = new List<QueueItem>(8); // //
        private readonly List<QueueItem> actived = new List<QueueItem>(8); // //
        private readonly List<QueueItem> requests = new List<QueueItem>(8);

        protected List<QueueItem> queue = new List<QueueItem>(8);

        private Thread thread;
        private volatile bool running = true;
        private Timer timer;
        private Control invokerControl;

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
            AnimationType = AnimatorNS.AnimationType.VertSlide;
            DefaultAnimation = new Animation();
            MaxAnimationTime = 1500;
            TimeStep = 0.02f;
            Interval = 10;

            Disposed += Animator_Disposed;

            timer = new Timer();
            timer.Tick += timer_Tick;
            timer.Interval = 1;
            timer.Start();
        }

        private void Start()
        {
            thread = new Thread(Work)
            {
                IsBackground = true,
                Name = "Animator thread"
            };
            thread.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            // Use main form as invokerControl if available, otherwise skip Control allocation //
            invokerControl = Application.OpenForms.Count > 0 ? Application.OpenForms[0] : null; // //
            if (invokerControl != null && !invokerControl.IsHandleCreated)
                invokerControl.CreateControl();
            Start();
        }

        void Animator_Disposed(object sender, EventArgs e)
        {
            ClearQueue();
            running = false;
            if (thread != null && thread.IsAlive)
                thread.Join(500);
        }

        void Work()
        {
            while (running)
            {
                Thread.Sleep(Interval);
                try
                {
                    completed.Clear(); // reuse list //
                    actived.Clear(); // reuse list //
                    int count = 0;
                    bool wasActive = false;

                    lock (queue)
                    {
                        count = queue.Count;
                        foreach (var item in queue)
                        {
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
                    {
                        OnCompleted(item);
                    }

                    foreach (var item in actived)
                    {
                        try
                        {
                            invokerControl?.BeginInvoke((MethodInvoker)(() => DoAnimation(item)));
                        }
                        catch (Exception ex) // //
                        {
#if debug
                            Console.WriteLine(ex);
#endif
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
                catch (Exception ex) // //
                {
#if debug
                    Console.WriteLine(ex);
#endif
                    // Suppress thread exceptions (form might be closed, etc.)
                }
            }
        }

        private readonly List<QueueItem> requestsToRemove = new List<QueueItem>(8); // // Reuse list

        private void CheckRequests()
        {
            requestsToRemove.Clear(); // //
            lock (requests)
            {
                var dict = new Dictionary<Control, QueueItem>();
                foreach (var item in requests)
                {
                    if (item.control != null)
                    {
                        if (dict.ContainsKey(item.control))
                            requestsToRemove.Add(dict[item.control]); // //
                        dict[item.control] = item;
                    }
                    else
                        requestsToRemove.Add(item);
                }
                foreach (var item in dict.Values)
                {
                    if (item.control != null && !IsStateOK(item.control, item.mode))
                    {
                        invokerControl?.Invoke((MethodInvoker)(() => RepairState(item.control, item.mode)));
                    }
                    else
                        requestsToRemove.Add(item);
                }
                foreach (var item in requestsToRemove)
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
            catch (Exception ex) // //
            {
#if debug
                Console.WriteLine(ex);
#endif
                // Suppress
            }
        }

        private void DoAnimation(QueueItem item)
        {
            lock (item)
            {
                try
                {
                    if (item.controller == null)
                    {
                        item.controller = CreateDoubleBitmap(item.control, item.mode, item.animation, item.clipRectangle);
                    }
                    if (item.controller.IsCompleted)
                        return;
                    item.controller.BuildNextFrame();
                }
                catch (Exception ex) // //
                {
#if debug
                    Console.WriteLine(ex);
#endif
                    item.controller?.Dispose();
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
                case AnimationType.Transparent: DefaultAnimation = Animation.Transparent; break;
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

        public void BeginUpdate(Control control, bool parallel = false, Animation animation = null, Rectangle clipRectangle = default)
        {
            AddToQueue(control, AnimateMode.BeginUpdate, parallel, animation, clipRectangle);

            bool wait = false;
            do
            {
                wait = false;
                lock (queue)
                {
                    foreach (var item in queue)
                        if (item.control == control && item.mode == AnimateMode.BeginUpdate)
                            if (item.controller == null)
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

        void OnCompleted(QueueItem item)
        {
            item.controller?.Dispose();
            lock (queue)
                queue.Remove(item);

            OnAnimationCompleted(AnimationCompletedEventArg.GetPooled(item.animation, item.control, item.mode));
        }

        public void AddToQueue(Control control, AnimateMode mode, bool parallel = true, Animation animation = null, Rectangle clipRectangle = default)
        {
            if (animation == null)
                animation = DefaultAnimation;

            if (control is IFakeControl)
            {
                control.Visible = false;
                return;
            }

            var item = new QueueItem() { animation = animation, control = control, IsActive = parallel, mode = mode, clipRectangle = clipRectangle };

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

        private Controller CreateDoubleBitmap(Control control, AnimateMode mode, Animation animation, Rectangle clipRect)
        {
            var controller = new Controller(control, mode, animation, TimeStep, clipRect);
            controller.TransfromNeeded += OnTransformNeeded;
            if (NonLinearTransfromNeeded != null)
                controller.NonLinearTransfromNeeded += OnNonLinearTransfromNeeded;
            controller.MouseDown += OnMouseDown;
            controller.DoubleBitmap.Cursor = Cursor;
            controller.FramePainted += OnFramePainted;
            return controller;
        }

        void OnFramePainted(object sender, PaintEventArgs e)
        {
            FramePainted?.Invoke(sender, e);
        }

        protected virtual void OnMouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                var db = (Controller)sender;
                var l = e.Location;
                l.Offset(db.DoubleBitmap.Left - db.AnimatedControl.Left, db.DoubleBitmap.Top - db.AnimatedControl.Top);
                MouseDown?.Invoke(sender, new MouseEventArgs(e.Button, e.Clicks, l.X, l.Y, e.Delta));
            }
            catch (Exception ex) // //
            {
#if debug
                Console.WriteLine(ex);
#endif
            }
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
                items = new List<QueueItem>(queue); // Use one allocation here, but reuse for future calls if needed //
                queue.Clear();
            }
            foreach (var item in items)
            {
                item.control?.BeginInvoke((MethodInvoker)(() =>
                {
                    switch (item.mode)
                    {
                        case AnimateMode.Hide: item.control.Visible = false; break;
                        case AnimateMode.Show: item.control.Visible = true; break;
                    }
                }));
                OnAnimationCompleted(AnimationCompletedEventArg.GetPooled(item.animation, item.control, item.mode));
            }
            if (items.Count > 0)
                OnAllAnimationsCompleted();
        }

        protected virtual void OnAnimationCompleted(AnimationCompletedEventArg e)
        {
            AnimationCompleted?.Invoke(this, e);
            e.Release(); // return to pool
        }

        protected virtual void OnAllAnimationsCompleted()
        {
            AllAnimationsCompleted?.Invoke(this, EventArgs.Empty);
        }

        #region Nested type: QueueItem

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
                var sb = new StringBuilder();
                if (control != null)
                    sb.Append(control.GetType().Name + " ");
                sb.Append(mode);
                return sb.ToString();
            }
        }

        #endregion

        #region IExtenderProvider

        public bool CanExtend(object extendee)
        {
            return extendee is Control;
        }

        #endregion
    }

    public enum DecorationType
    {
        None,
        BottomMirror,
        Custom
    }

    // Pooled event arg example
    public class AnimationCompletedEventArg : EventArgs
    {
        private static readonly Stack<AnimationCompletedEventArg> Pool = new Stack<AnimationCompletedEventArg>(16);

        public Animation Animation { get; set; }
        public Control Control { get; internal set; }
        public AnimateMode Mode { get; internal set; }

        public static AnimationCompletedEventArg GetPooled(Animation anim, Control ctrl, AnimateMode mode)
        {
            AnimationCompletedEventArg arg = Pool.Count > 0 ? Pool.Pop() : new AnimationCompletedEventArg();
            arg.Animation = anim;
            arg.Control = ctrl;
            arg.Mode = mode;
            return arg;
        }

        public void Release()
        {
            Animation = null;
            Control = null;
            Mode = 0;
            Pool.Push(this);
        }
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