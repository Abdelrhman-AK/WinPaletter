using FluentTransitions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.UI.WP;

namespace WinPaletter.Tabs
{
    /// <summary>
    /// Class that has data of tabs in <see cref="TabsContainer"/>.
    /// </summary>
    public class TabData : IDisposable
    {
        #region Fields and Properties

        /// <summary>
        /// Icon change message handler.
        /// </summary>
        private IconChangeMessageHandler iconChangeDetector;

        /// <summary>
        /// Tabs container reference.
        /// </summary>
        private readonly TabsContainer tabsContainer;

        /// <summary>
        /// Tracks active animations per-property so they can be cancelled or replaced.
        /// </summary>
        private readonly Dictionary<string, FluentTransitions.TransitionDefinition> activeTransitions = new();

        /// <summary>
        /// Reference to the associated form's title bar extender.
        /// </summary>
        private TitlebarExtender TitlebarExtender => Form?.Controls?.OfType<TitlebarExtender>()?.FirstOrDefault();

        /// <summary>
        /// Text property for the tab.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Rectangle property for the tab's location.
        /// </summary>
        public Rectangle Rectangle
        {
            get => _rectangle;
            set
            {
                if (_rectangle != value)
                {
                    _rectangle = value;

                    // Update title bar extender if selected and rectangle is not empty
                    if (TitlebarExtender != null && Selected && _rectangle != Rectangle.Empty)
                    {
                        TitlebarExtender.TabLocation = _rectangle;
                    }
                }
            }
        }
        private Rectangle _rectangle = Rectangle.Empty;

        /// <summary>
        /// Image property for the tab.
        /// </summary>
        public Bitmap Image
        {
            get => _image;
            set
            {
                if (_image != value)
                {
                    _image?.Dispose();
                    _image = value;
                }
            }
        }
        private Bitmap _image;

        /// <summary>
        /// Reference to the associated tab page.
        /// </summary>
        public TabPage TabPage { get; set; }

        /// <summary>
        /// Reference to the associated form.
        /// </summary>
        public System.Windows.Forms.Form Form
        {
            get => _form;
            private set
            {
                if (_form == value) return;

                if (_form != null)
                {
                    UnsubscribeEvents();
                    _form.ParentChanged -= _form_ParentChanged;
                }

                _form = value;

                if (_form != null)
                {
                    SubscribeEvents();
                    _form.ParentChanged += _form_ParentChanged;
                }
            }
        }
        private System.Windows.Forms.Form _form = null;

        /// <summary>
        /// Flag indicating whether the form is shown.
        /// </summary>
        public bool Shown { get; private set; } = false;

        /// <summary>
        /// Flag indicating whether the tab is selected.
        /// </summary>
        public bool Selected
        {
            get => _selected;
            set
            {
                if (_selected != value)
                {
                    _selected = value;

                    // Update title bar extender if selected and rectangle is not empty
                    if (TitlebarExtender != null && _selected && _rectangle != Rectangle.Empty)
                    {
                        TitlebarExtender.TabLocation = _rectangle;
                    }

                    // Cancel existing selection transition before starting new one
                    CancelTransition(nameof(SelectionAlpha));

                    // Animate selection alpha if container allows animation
                    if (tabsContainer != null && tabsContainer.CanAnimate_Global)
                    {
                        var transition = Transition.With(this, nameof(SelectionAlpha), value ? 255 : 0);
                        transition.CriticalDamp(Program.AnimationSpan);
                        TrackTransition(nameof(SelectionAlpha), transition);
                    }
                    else
                    {
                        SelectionAlpha = value ? 255 : 0;
                    }
                }
            }
        }
        private bool _selected = false;

        /// <summary>
        /// Property representing the top position of the tab.
        /// </summary>
        public int TabTop
        {
            get => Rectangle.Top;
            set
            {
                if (Rectangle.Top != value)
                {
                    // Update rectangle on current thread
                    _rectangle = new(Rectangle.Left, value, Rectangle.Width, Rectangle.Height);

                    // Marshal invalidate to UI thread if needed to prevent blocking
                    if (tabsContainer.InvokeRequired)
                    {
                        tabsContainer.BeginInvoke(() => tabsContainer.Invalidate());
                    }
                    else
                    {
                        tabsContainer.Invalidate();
                    }
                }
            }
        }

        /// <summary>
        /// Left position of the tab. Animatable for drag-reorder transitions.
        /// </summary>
        public int TabLeft
        {
            get => Rectangle.Left;
            set
            {
                if (_rectangle.Left != value)
                {
                    _rectangle = new Rectangle(value, _rectangle.Y, _rectangle.Width, _rectangle.Height);

                    if (tabsContainer.InvokeRequired)
                    {
                        tabsContainer.BeginInvoke(() => tabsContainer.Invalidate());
                    }
                    else
                    {
                        tabsContainer.Invalidate();
                    }
                }
            }
        }

        /// <summary>
        /// Width of the tab. Animatable for dynamic width transitions.
        /// </summary>
        public int TabWidth
        {
            get => Rectangle.Width;
            set
            {
                if (_rectangle.Width != value)
                {
                    _rectangle = new Rectangle(_rectangle.X, _rectangle.Y, value, _rectangle.Height);

                    if (tabsContainer.InvokeRequired)
                    {
                        tabsContainer.BeginInvoke(() => tabsContainer.Invalidate());
                    }
                    else
                    {
                        tabsContainer.Invalidate();
                    }
                }
            }
        }

        /// <summary>
        /// Flag indicating whether the tab is being removed.
        /// </summary>
        public bool IsRemoving
        {
            get => _isRemoving;
            set
            {
                if (value != _isRemoving)
                {
                    _isRemoving = value;
                }
            }
        }
        private bool _isRemoving = false;

        /// <summary>
        /// Flag indicating whether the tab is hovered.
        /// </summary>
        public bool Hovered
        {
            get => _hovered;
            set
            {
                if (_hovered != value)
                {
                    _hovered = value;

                    // Cancel existing hover transition before starting new one
                    CancelTransition(nameof(HoverAlpha));

                    if (Program.Style.Animations)
                    {
                        var transition = Transition.With(this, nameof(HoverAlpha), value ? 255 : 0);
                        transition.CriticalDamp(Program.AnimationSpan);
                        TrackTransition(nameof(HoverAlpha), transition);
                    }
                    else
                    {
                        HoverAlpha = value ? 255 : 0;
                    }
                }
            }
        }
        private bool _hovered;

        /// <summary>
        /// Alpha of hover effect over a tab
        /// </summary>
        public int HoverAlpha
        {
            get => _hoverAlpha;
            set
            {
                if (_hoverAlpha != value)
                {
                    _hoverAlpha = value;

                    // Marshal to UI thread if needed to prevent blocking
                    if (tabsContainer.InvokeRequired)
                    {
                        tabsContainer.BeginInvoke(() => tabsContainer.Invalidate(_rectangle.InflateReturn(1, 0)));
                    }
                    else
                    {
                        tabsContainer.Invalidate(_rectangle.InflateReturn(1, 0));
                    }
                }
            }
        }
        private int _hoverAlpha = 0;

        /// <summary>
        /// Alpha of selection effect over a tab
        /// </summary>
        public int SelectionAlpha
        {
            get => _selectionAlpha;
            set
            {
                if (_selectionAlpha != value)
                {
                    _selectionAlpha = value;

                    // Marshal to UI thread if needed to prevent blocking
                    if (tabsContainer.InvokeRequired)
                    {
                        tabsContainer.BeginInvoke(() => tabsContainer.Invalidate(_rectangle.InflateReturn(1, 0)));
                    }
                    else
                    {
                        tabsContainer.Invalidate(_rectangle.InflateReturn(1, 0));
                    }
                }
            }
        }
        private int _selectionAlpha = 255;

        /// <summary>
        /// Alpha of removal effect over a tab
        /// </summary>
        public int RemovingAlpha
        {
            get => _removingAlpha;
            set
            {
                if (_removingAlpha != value)
                {
                    _removingAlpha = value;

                    // Marshal to UI thread if needed to prevent blocking
                    if (tabsContainer.InvokeRequired)
                    {
                        tabsContainer.BeginInvoke(() => tabsContainer.Invalidate(_rectangle.InflateReturn(1, 0)));
                    }
                    else
                    {
                        tabsContainer.Invalidate(_rectangle.InflateReturn(1, 0));
                    }
                }
            }
        }
        private int _removingAlpha = 255;

        /// <summary>
        /// Alpha of close button hover effect over a tab
        /// </summary>
        public int CloseButtonAlpha
        {
            get => _closeButtonAlpha;
            set
            {
                if (_closeButtonAlpha != value)
                {
                    _closeButtonAlpha = value;

                    // Cancel existing close button alpha transition before starting new one
                    CancelTransition(nameof(CloseButtonAlpha));

                    // Marshal to UI thread if needed to prevent blocking
                    if (tabsContainer.InvokeRequired)
                    {
                        tabsContainer.BeginInvoke(() => tabsContainer.Invalidate(_rectangle.InflateReturn(1, 0)));
                    }
                    else
                    {
                        tabsContainer.Invalidate(_rectangle.InflateReturn(1, 0));
                    }
                }
            }
        }
        private int _closeButtonAlpha = 0;

        // Progress related
        private int _progressValue = 0; // 0..100
        /// <summary>
        /// Progress value for this tab (0-100). If TaskbarBroadcasting ProgressBar exists on the form,
        /// its value will be reflected here.
        /// </summary>
        public int ProgressValue
        {
            get => _progressValue;
            set
            {
                int v = Math.Max(0, Math.Min(100, value));
                if (_progressValue != v)
                {
                    _progressValue = v;
                    if (tabsContainer != null)
                    {
                        if (tabsContainer.InvokeRequired)
                            tabsContainer.BeginInvoke(() => tabsContainer.Invalidate(_rectangle));
                        else
                            tabsContainer.Invalidate(_rectangle);
                    }
                }
            }
        }

        private bool _progressMarquee = false;
        /// <summary>
        /// If true, show marquee animation instead of continuous fill.
        /// </summary>
        public bool ProgressMarquee
        {
            get => _progressMarquee;
            set
            {
                if (_progressMarquee != value)
                {
                    _progressMarquee = value;
                    if (tabsContainer != null)
                    {
                        if (tabsContainer.InvokeRequired)
                            tabsContainer.BeginInvoke(() => tabsContainer.Invalidate(_rectangle));
                        else
                            tabsContainer.Invalidate(_rectangle);

                        try
                        {
                            if (_progressMarquee)
                                tabsContainer.StartProgressTimer();
                            else
                            {
                                bool any = false;
                                if (tabsContainer?.TabDataList != null)
                                {
                                    for (int _ii = 0; _ii < tabsContainer.TabDataList.Count; _ii++)
                                    {
                                        var _t = tabsContainer.TabDataList[_ii];
                                        if (_t != null && _t.ProgressMarquee)
                                        {
                                            any = true;
                                            break;
                                        }
                                    }
                                }
                                if (!any) tabsContainer.StopProgressTimer();
                            }
                        }
                        catch { }
                    }
                }
            }
        }

        /// <summary>
        /// Animate the ProgressValue to the target using FluentTransitions when possible.
        /// Falls back to setting the value directly when animations are disabled.
        /// </summary>
        /// <param name="target">Target progress value (0..100)</param>
        private void AnimateProgressTo(int target)
        {
            try
            {
                CancelTransition(nameof(ProgressValue));

                if (tabsContainer != null && tabsContainer.CanAnimate_Global)
                {
                    var tr = Transition.With(this, nameof(ProgressValue), target);
                    tr.CriticalDamp(Program.AnimationSpan_Quick);
                    TrackTransition(nameof(ProgressValue), tr);
                }
                else
                {
                    ProgressValue = target;
                }
            }
            catch { ProgressValue = target; }
        }

        private bool _progressEnabled = false;
        /// <summary>
        /// Whether the progress should be drawn. False when value equals Minimum or Maximum and not marquee.
        /// </summary>
        public bool ProgressEnabled
        {
            get => _progressEnabled;
            set
            {
                if (_progressEnabled != value)
                {
                    _progressEnabled = value;
                    if (tabsContainer != null)
                    {
                        if (tabsContainer.InvokeRequired)
                            tabsContainer.BeginInvoke(() => tabsContainer.Invalidate(_rectangle));
                        else
                            tabsContainer.Invalidate(_rectangle);
                    }
                }
            }
        }

        // tracked progressbar controls for unsubscribing
        private List<UI.WP.ProgressBar> trackedProgressBars = new();

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new TabData and attach it to the owning container, tab page and bounds.
        /// </summary>
        /// <param name="tabsContainer">Owning <see cref="TabsContainer"/>.</param>
        /// <param name="tabPage">Associated <see cref="TabPage"/>.</param>
        /// <param name="rectangle">Initial bounds for the tab.</param>
        public TabData(TabsContainer tabsContainer, TabPage tabPage, Rectangle rectangle)
        {
            this.tabsContainer = tabsContainer;
            TabPage = tabPage;
            Form = TabPage?.Controls?.OfType<System.Windows.Forms.Form>().FirstOrDefault();
            Text = tabPage.Text;
            Rectangle = rectangle;
            Image = new Icon(Form?.Icon ?? Properties.Resources.Icon, 16, 16).ToBitmap();
        }

        #endregion

        #region Events and Delegates

        /// <summary>
        /// Delegate void for icon change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void IconChangedDelegate(object sender, TabDataEventArgs e);

        /// <summary>
        /// Event for icon change.
        /// </summary>
        public event IconChangedDelegate IconChanged;

        /// <summary>
        /// Event handler for icon change.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        public virtual void OnIconChanged(TabDataEventArgs e)
        {
            IconChanged?.Invoke(this, e);
            try
            {
                Image = Form?.Icon?.ToBitmap() ?? Properties.Resources.Icon.ToBitmap();
            }
            catch // If the form is disposed, the icon will throw an exception
            {
                Image = Properties.Resources.Icon.ToBitmap();
            }

            // Marshal to UI thread if needed to prevent blocking
            if (tabsContainer != null && tabsContainer.InvokeRequired)
            {
                tabsContainer.BeginInvoke(() => tabsContainer.Invalidate(_rectangle));
            }
            else
            {
                tabsContainer?.Invalidate(_rectangle);
            }
        }

        #endregion

        #region Event Subscription and Unsubscription

        /// <summary>
        /// Subscribe to form events.
        /// </summary>
        private void SubscribeEvents()
        {
            if (_form != null)
            {
                _form.Shown += _form_Shown;
                _form.TextChanged += _form_TextChanged;
                _form.FormClosing += _form_FormClosing;
                _form.FormClosed += _form_FormClosed;
                iconChangeDetector = new(_form.Handle, this);

                // Attach to progress bars on the form that broadcast to taskbar so we can reflect progress on the tab
                try
                {
                    foreach (UI.WP.ProgressBar pb in _form.GetAllControls().OfType<UI.WP.ProgressBar>())
                    {
                        if (!trackedProgressBars.Contains(pb))
                        {
                            pb.ValueChanged += ProgressBar_ValueChanged;
                            pb.StyleChanged += ProgressBar_StyleChanged;
                            trackedProgressBars.Add(pb);

                            // Initialize from current state
                            if (pb.TaskbarBroadcast)
                            {
                                ProgressMarquee = pb.IsMarquee;
                                if (pb.IsMarquee)
                                {
                                    ProgressEnabled = true;
                                    AnimateProgressTo(0);
                                }
                                else
                                {
                                    bool enabled = pb.Value > pb.Minimum && pb.Value < pb.Maximum;
                                    ProgressEnabled = enabled;
                                    if (enabled && pb.Maximum > pb.Minimum)
                                    {
                                        double pct = (pb.Value - pb.Minimum) / (double)(pb.Maximum - pb.Minimum) * 100.0;
                                        AnimateProgressTo((int)Math.Round(pct));
                                    }
                                    else
                                    {
                                        AnimateProgressTo(0);
                                    }
                                }
                            }
                        }
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// Unsubscribe from form events.
        /// </summary>
        private void UnsubscribeEvents()
        {
            if (_form != null)
            {
                _form.Shown -= _form_Shown;
                _form.TextChanged -= _form_TextChanged;
                _form.FormClosing -= _form_FormClosing;
                _form.FormClosed -= _form_FormClosed;

                // detach progress bar handlers
                try
                {
                    for (int i = 0; i < trackedProgressBars.Count; i++)
                    {
                        var pb = trackedProgressBars[i];
                        try { pb.ValueChanged -= ProgressBar_ValueChanged; } catch { }
                        try { pb.StyleChanged -= ProgressBar_StyleChanged; } catch { }
                    }
                    trackedProgressBars.Clear();
                }
                catch { }

                iconChangeDetector?.Dispose();
            }
        }

        #endregion

        #region Voids

        /// <summary>
        /// Hide the tab with animation
        /// </summary>
        /// <param name="animate"></param>
        /// <param name="animationDuration"></param>
        /// <param name="afterAnimationValue"></param>
        /// <param name="HookOnCompletion"></param>
        /// <returns></returns>
        private async Task Animate(bool animate, int animationDuration, int afterAnimationValue, Action HookOnCompletion = null)
        {
            if (tabsContainer is not null && tabsContainer.IsHandleCreated)
            {
                if (tabsContainer.CanAnimate_Global)
                {
                    var tcs = new TaskCompletionSource<bool>();

                    Transition
                        .With(this, nameof(SelectionAlpha), _selected ? 255 : 0)
                        .With(this, nameof(TabTop), afterAnimationValue)
                        .HookOnCompletion(() =>
                        {
                            HookOnCompletion?.Invoke();
                            tcs.TrySetResult(true);
                        })
                        .CriticalDamp(TimeSpan.FromMilliseconds(animate ? animationDuration : 1));

                    await tcs.Task;
                }
                else
                {
                    SelectionAlpha = _selected ? 255 : 0;
                    TabTop = afterAnimationValue;
                    HookOnCompletion?.Invoke();
                }
            }
        }

        /// <summary>
        /// Hide the tab with animation
        /// </summary>
        /// <param name="animate"></param>
        /// <param name="HookOnCompletion"></param>
        /// <returns></returns>
        public async Task Hide(bool animate, Action HookOnCompletion = null)
        {
            await Animate(animate, Program.AnimationDuration_Quick, tabsContainer.Height, HookOnCompletion);
        }

        /// <summary>
        /// Show the tab with animation
        /// </summary>
        /// <returns></returns>
        public async Task Show(Action HookOnCompletion = null)
        {
            await Animate(tabsContainer.CanAnimate_Global, Program.AnimationDuration, tabsContainer._upperTabPadding, HookOnCompletion);
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Event handler for form shown.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void _form_Shown(object sender, EventArgs e)
        {
            Shown = true;
            tabsContainer.OnFormShown(_form, new(this));

            // Marshal to UI thread if needed to prevent blocking
            if (tabsContainer.InvokeRequired)
            {
                tabsContainer.BeginInvoke(() => tabsContainer.Invalidate(_rectangle));
            }
            else
            {
                tabsContainer.Invalidate(_rectangle);
            }

            if (Forms.MainForm is not null) Forms.MainForm.BackgroundImage = null;
        }

        /// <summary>
        /// Event handler for form text changed.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void _form_TextChanged(object sender, EventArgs e)
        {
            Text = _form.Text;
            if (TabPage is not null) TabPage.Text = _form.Text;
            if (_form is not null) tabsContainer?.OnFormTextChanged(_form, new(this));

            // Marshal to UI thread if needed to prevent blocking
            if (tabsContainer != null && tabsContainer.InvokeRequired)
            {
                tabsContainer.BeginInvoke(() => tabsContainer.Invalidate(_rectangle));
            }
            else
            {
                tabsContainer?.Invalidate(_rectangle);
            }
        }

        /// <summary>
        /// Event handler for form closing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void _form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Shown = false;
            if (_form is not null) tabsContainer?.OnFormClosing(_form, new(this));
        }

        /// <summary>
        /// Event handler for form closed.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void _form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Shown = false;
            if (_form is not null) tabsContainer?.OnFormClosed(_form, new(this));
        }

        private void ProgressBar_ValueChanged(object sender, EventArgs e)
        {
            if (sender is UI.WP.ProgressBar pb)
            {
                try
                {
                    if (pb.TaskbarBroadcast)
                    {
                        if (pb.IsMarquee)
                        {
                            ProgressMarquee = true;
                            ProgressEnabled = true;
                            AnimateProgressTo(0);
                        }
                        else
                        {
                            ProgressMarquee = false;
                            bool enabled = pb.Value > pb.Minimum && pb.Value < pb.Maximum;
                            ProgressEnabled = enabled;
                            if (enabled && pb.Maximum > pb.Minimum)
                            {
                                double pct = (pb.Value - pb.Minimum) / (double)(pb.Maximum - pb.Minimum) * 100.0;
                                AnimateProgressTo((int)Math.Round(pct));
                            }
                            else
                            {
                                AnimateProgressTo(0);
                            }
                        }
                    }
                }
                catch { }
            }
        }

        private void ProgressBar_StyleChanged(object sender, EventArgs e)
        {
            if (sender is UI.WP.ProgressBar pb)
            {
                try
                {
                    if (pb.TaskbarBroadcast)
                    {
                        ProgressMarquee = pb.IsMarquee;
                        if (pb.IsMarquee)
                        {
                            ProgressEnabled = true;
                            AnimateProgressTo(0);
                        }
                        else
                        {
                            bool enabled = pb.Value > pb.Minimum && pb.Value < pb.Maximum;
                            ProgressEnabled = enabled;
                            if (enabled && pb.Maximum > pb.Minimum)
                            {
                                double pct = (pb.Value - pb.Minimum) / (double)(pb.Maximum - pb.Minimum) * 100.0;
                                AnimateProgressTo((int)Math.Round(pct));
                            }
                            else
                            {
                                AnimateProgressTo(0);
                            }
                        }
                    }
                }
                catch { }
            }
        }

        private void _form_ParentChanged(object sender, EventArgs e)
        {
            if (_form.Parent == null)
            {
                UnsubscribeEvents();
            }
            else if (_form.Parent is TabPage)
            {
                SubscribeEvents();
            }
        }

        #endregion

        #region Transition Management

        /// <summary>
        /// Track a transition to allow cancellation.
        /// </summary>
        private void TrackTransition(string propertyName, FluentTransitions.TransitionDefinition transition)
        {
            if (activeTransitions.ContainsKey(propertyName))
            {
                activeTransitions[propertyName].Flash(1, TimeSpan.FromMilliseconds(1));
            }
            activeTransitions[propertyName] = transition;
            transition.HookOnCompletion(() => activeTransitions.Remove(propertyName));
        }

        /// <summary>
        /// Cancel an active transition for a property.
        /// </summary>
        public void CancelTransition(string propertyName)
        {
            if (activeTransitions.TryGetValue(propertyName, out var transition))
            {
                transition.Flash(1, TimeSpan.FromMilliseconds(1));
                activeTransitions.Remove(propertyName);
            }
        }

        /// <summary>
        /// Cancel all active transitions.
        /// </summary>
        private void CancelAllTransitions()
        {
            foreach (var transition in activeTransitions.Values)
            {
                transition.Flash(1, TimeSpan.FromMilliseconds(1));
            }
            activeTransitions.Clear();
        }

        #endregion

        #region IDisposable Implementation

        /// <summary>
        /// Dispose managed resources, cancel animations and unsubscribe events.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected dispose pattern implementation.
        /// </summary>
        /// <param name="disposing">True when called from Dispose(), false from finalizer.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose managed resources
                try { CancelAllTransitions(); } catch { }
                try { UnsubscribeEvents(); } catch { }

                try { TabPage?.Dispose(); } catch { }
                TabPage = null;

                try { Image?.Dispose(); } catch { }
                Image = null;

                try { iconChangeDetector?.Dispose(); } catch { }
                iconChangeDetector = null;

                Form = null;
            }
        }

        #endregion
    }

    /// <summary>
    /// Event arguments carrying the related <see cref="TabData"/> instance.
    /// </summary>
    public class TabDataEventArgs : EventArgs
    {
        /// <summary>
        /// The related TabData.
        /// </summary>
        public TabData TabData { get; }

        /// <summary>
        /// Create args for the provided <paramref name="tabData"/>.
        /// </summary>
        public TabDataEventArgs(TabData tabData) => TabData = tabData;
    }

    /// <summary>
    /// Handles the WM_SETICON message for icon changes.
    /// </summary>
    public class IconChangeMessageHandler : NativeWindow, IDisposable
    {
        #region Constants

        /// <summary>
        /// Windows message constant for setting the icon.
        /// </summary>
        private const int WM_SETICON = 0x80;

        #endregion

        #region Fields

        /// <summary>
        /// Reference to the TabData.
        /// </summary>
        private readonly TabData TabData;

        /// <summary>
        /// Last known icon handle.
        /// </summary>
        private IntPtr lastIconHandle = IntPtr.Zero;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="handle">Handle to the window.</param>
        /// <param name="tabData">Reference to the associated TabData.</param>
        public IconChangeMessageHandler(IntPtr handle, TabData tabData)
        {
            TabData = tabData;
            AssignHandle(handle);
        }

        #endregion

        #region NativeWindow Overrides

        /// <summary>
        /// Overrides the window procedure to handle WM_SETICON message.
        /// </summary>
        /// <param name="m">Message object.</param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // Check for the WM_SETICON message
            if (m.Msg == WM_SETICON)
            {
                // Get the current icon handle
                IntPtr currentIconHandle = User32.SendMessage(Handle, 0x7F /*WM_GETICON*/, (IntPtr)1, IntPtr.Zero);

                // If the icon handle has changed, trigger the OnIconChanged event
                if (lastIconHandle == IntPtr.Zero || lastIconHandle != currentIconHandle)
                {
                    lastIconHandle = currentIconHandle;
                    TabData?.OnIconChanged(new(TabData));
                }
            }
        }

        #endregion

        #region IDisposable Implementation

        /// <summary>
        /// Disposes the IconChangeMessageHandler.
        /// </summary>
        public void Dispose()
        {
            ReleaseHandle();
            lastIconHandle = IntPtr.Zero;
        }

        #endregion
    }
}