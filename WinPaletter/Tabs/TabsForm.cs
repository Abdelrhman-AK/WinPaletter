using System;
using System.Windows.Forms;

namespace WinPaletter.Tabs
{
    /// <summary>
    /// The form that contains the tabs control.
    /// </summary>
    public partial class TabsForm : UI.WP.Form
    {
        private TitlebarControlHost _host;
        private Control _placeholder;

        /// <summary>
        /// Initializes a new instance of the <see cref="TabsForm"/> class.
        /// </summary>
        public TabsForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            _host = new(this);
            _placeholder = new();

            _host.SetControl(_placeholder, 1);

            base.OnLoad(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            _host?.Dispose();
            _placeholder?.Dispose();
        }
    }
}