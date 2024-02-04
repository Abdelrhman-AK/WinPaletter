using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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
        /// Reference to the associated form's title bar extender.
        /// </summary>
        private TitlebarExtender TitlebarExtender => Form?.Controls?.OfType<TitlebarExtender>().FirstOrDefault();

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
        public Bitmap Image { get; set; }

        /// <summary>
        /// Reference to the associated tab page.
        /// </summary>
        public TabPage TabPage { get; set; }

        /// <summary>
        /// Reference to the associated form.
        /// </summary>
        public Form Form
        {
            get => _form;
            private set
            {
                UnsubscribeEvents();
                if (_form != null) _form.ParentChanged -= _form_ParentChanged;

                if (_form != value)
                {
                    _form = value;
                    SubscribeEvents();
                    _form.ParentChanged += _form_ParentChanged;
                }
            }
        }
        private Form _form = null;

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
                    Rectangle = new Rectangle(Rectangle.Left, value, Rectangle.Width, Rectangle.Height);
                    tabsContainer.Invalidate();
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

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tabsContainer">Tabs container reference.</param>
        /// <param name="tabPage">Reference to the associated tab page.</param>
        /// <param name="rectangle">Rectangle property for the tab's location.</param>
        public TabData(TabsContainer tabsContainer, TabPage tabPage, Rectangle rectangle)
        {
            this.tabsContainer = tabsContainer;
            TabPage = tabPage;
            Form = TabPage?.Controls?.OfType<Form>().FirstOrDefault();
            Text = tabPage.Text;
            Rectangle = rectangle;
            Image = new Icon(Form?.Icon ?? Forms.MainForm.Icon, 16, 16).ToBitmap();
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
            Image = Form?.Icon.ToBitmap() ?? Forms.MainForm.Icon.ToBitmap();
            tabsContainer?.Refresh();
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

                iconChangeDetector?.Dispose();
            }
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
            tabsContainer.OnFormShown(_form, new TabDataEventArgs(this));
            tabsContainer.Refresh();
        }

        /// <summary>
        /// Event handler for form text changed.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void _form_TextChanged(object sender, EventArgs e)
        {
            Text = _form.Text;
            tabsContainer?.OnFormTextChanged(_form, new TabDataEventArgs(this));
            tabsContainer?.Invalidate(this.Rectangle);
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
            tabsContainer.OnFormClosing(_form, new TabDataEventArgs(this));
        }

        /// <summary>
        /// Event handler for form closed.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void _form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Shown = false;
            tabsContainer.OnFormClosed(_form, new TabDataEventArgs(this));
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

        #region IDisposable Implementation

        /// <summary>
        /// Dispose method.
        /// </summary>
        public void Dispose()
        {
            UnsubscribeEvents();

            TabPage?.Dispose();
            Image?.Dispose();
        }

        #endregion
    }

    /// <summary>
    /// Event arguments for tab events.
    /// </summary>
    public class TabDataEventArgs : EventArgs
    {
        /// <summary>
        /// TabData associated with the event.
        /// </summary>
        public TabData TabData { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tabData">TabData associated with the event.</param>
        public TabDataEventArgs(TabData tabData)
        {
            TabData = tabData;
        }
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
        private TabData TabData;

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
            this.TabData = tabData;
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
                IntPtr currentIconHandle = NativeMethods.User32.SendMessage(Handle, 0x7F /*WM_GETICON*/, (IntPtr)1, IntPtr.Zero);

                // If the icon handle has changed, trigger the OnIconChanged event
                if (lastIconHandle == IntPtr.Zero || lastIconHandle != currentIconHandle)
                {
                    lastIconHandle = currentIconHandle;
                    TabData.OnIconChanged(new(TabData));
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
        }

        #endregion
    }
}