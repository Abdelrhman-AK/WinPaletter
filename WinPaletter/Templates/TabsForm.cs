using System;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter.Templates
{
    public partial class TabsForm : Form
    {

        public TabsForm()
        {
            InitializeComponent();
        }

        private void TabsForm_Load(object sender, EventArgs e)
        {
            DLLFunc.RemoveFormTitlebarTextAndIcon(Handle);
            this.LoadLanguage();
            ApplyStyle(this);

            CheckForIllegalCrossThreadCalls = false;

            panel1.Visible = Program.IsBeta;
        }

        private void tabsContainer1_DoubleClick(object sender, EventArgs e)
        {
            WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        }
    }
}