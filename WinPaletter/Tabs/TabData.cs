using System;
using System.Drawing;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.Windows.Forms;
using WinPaletter.UI.WP;

namespace WinPaletter.Tabs
{
    public class TabData : IDisposable
    {
        IconChangeMessageHandler iconChangeDetector;

        private TabsContainer tabsContainer;
        private TitlebarExtender TitlebarExtender => Form?.Controls?.OfType<TitlebarExtender>().FirstOrDefault();

        public string Text { get; set; }

        private Rectangle _rectangle = Rectangle.Empty;
        public Rectangle Rectangle
        {
            get => _rectangle;
            set
            {
                if (_rectangle != value)
                {
                    _rectangle = value;

                    if (TitlebarExtender != null && Selected && _rectangle != Rectangle.Empty)
                    {
                        TitlebarExtender.TabLocation = _rectangle;
                    }
                }
            }
        }

        public Bitmap Image { get; set; }

        public TabPage TabPage { get; set; }

        private Form _form = null;
        public Form Form
        {
            get => _form;
            private set
            {
                UnsubscripeEvents();

                if (_form != value)
                {
                    _form = value;
                    SubscripeEvents();
                }
            }
        }

        private bool _shown = false;
        public bool Shown => _shown;


        private bool _selected = false;
        public bool Selected
        {
            get => _selected;
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    if (TitlebarExtender != null && _selected && _rectangle != Rectangle.Empty)
                    {
                        TitlebarExtender.TabLocation = _rectangle;
                    }
                }
            }
        }

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

        private void SubscripeEvents()
        {
            if (_form != null)
            {
                _form.Shown += _form_Shown;
                _form.TextChanged += _form_TextChanged;
                _form.FormClosed += _form_FormClosed;

                iconChangeDetector = new(_form.Handle, this);
            }
        }

        private void UnsubscripeEvents()
        {
            if (_form != null)
            {
                _form.Shown -= _form_Shown;
                _form.TextChanged -= _form_TextChanged;
                _form.FormClosed -= _form_FormClosed;

                iconChangeDetector.Dispose();
            }
        }

        private void _form_Shown(object sender, EventArgs e)
        {
            _shown = true;
            tabsContainer.OnFormShown(_form, new TabDataEventArgs(this));
            tabsContainer.Refresh();
        }

        private void _form_TextChanged(object sender, EventArgs e)
        {
            Text = _form.Text;
            tabsContainer.OnFormTextChanged(_form, new TabDataEventArgs(this));
            tabsContainer.Refresh();
        }

        private void _form_FormClosed(object sender, FormClosedEventArgs e)
        {
            _shown = false;
            ((Form)sender).Parent?.Dispose();
            tabsContainer.OnFormClosed(_form, new TabDataEventArgs(this));
        }

        public delegate void IconChangedDelegate(object sender, TabDataEventArgs e);
        public event IconChangedDelegate IconChanged;

        public virtual void OnIconChanged(TabDataEventArgs e)
        {
            IconChanged?.Invoke(this, e);
            Image = Form?.Icon.ToBitmap() ?? Forms.MainFrm.Icon.ToBitmap();
            tabsContainer?.Refresh();
        }

        /// <summary>
        /// Create an instance of TabData
        /// </summary>
        /// <param name="tabPage"></param>
        /// <param name="rectangle"></param>
        public TabData(TabsContainer tabsContainer, TabPage tabPage, Rectangle rectangle)
        {
            this.tabsContainer = tabsContainer;
            TabPage = tabPage;
            Form = TabPage?.Controls?.OfType<Form>().FirstOrDefault();
            Text = tabPage.Text;
            Rectangle = rectangle;
            Image = new Icon(Form?.Icon ?? Forms.MainFrm.Icon, 16, 16).ToBitmap();
        }

        public void Dispose()
        {
            UnsubscripeEvents();
        }

    }

    public class TabDataEventArgs : EventArgs
    {
        public TabData TabData { get; }

        public TabDataEventArgs(TabData tabData)
        {
            TabData = tabData;
        }
    }

    public class IconChangeMessageHandler : NativeWindow, IDisposable
    {
        private const int WM_SETICON = 0x80;

        private TabData TabData;

        public IconChangeMessageHandler(IntPtr handle, TabData tabData)
        {
            this.TabData = tabData;
            AssignHandle(handle);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_SETICON)
            {
                TabData.OnIconChanged(new(TabData));
            }
        }

        public void Dispose()
        {
            ReleaseHandle();
        }
    }
}
