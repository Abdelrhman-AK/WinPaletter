using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinPaletter.Templates
{
    public partial class NoNetworkPanel : UserControl
    {
        public event EventHandler RetryClicked;
        public event EventHandler CloseClicked;

        public NoNetworkPanel()
        {
            InitializeComponent();
        }

        private void NoNetworkPanel_Load(object sender, EventArgs e)
        {
            label26.Text = Program.Localization.Strings.NoNetwork.Title1;
            label24.Text = Program.Localization.Strings.NoNetwork.Title2;
            label25.Text = Program.Localization.Strings.NoNetwork.Tip2;
            label22.Text = Program.Localization.Strings.NoNetwork.Title3;
            label23.Text = Program.Localization.Strings.NoNetwork.Tip3;
            label16.Text = Program.Localization.Strings.NoNetwork.Title4;
            label21.Text = Program.Localization.Strings.NoNetwork.Tip4;

            button22.Text = Program.Localization.Strings.NoNetwork.Troubleshoot;
            button1.Text = Program.Localization.Strings.NoNetwork.NetworkSettings;
            button23.Text = Program.Localization.Strings.General.Retry;
            button21.Text = Program.Localization.Strings.General.Close;
        }

        [Browsable(true)]
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        [Localizable(true)]
        public new string Text
        {
            get => base.Text;
            set
            {
                base.Text = value;
                banner.Text = value;
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (OS.WXP)
            {
                Program.SendCommand($"{SysPaths.Windows}\\Network Diagnostic\\xpnetdiag.exe", false);
            }
            else if (OS.WVista || OS.W7 || OS.W8x || OS.W10)
            {
                Process.Start($"{SysPaths.System32}\\msdt.exe", $"-path {SysPaths.Windows}\\diagnostics\\system\\networking -ep NetworkDiagnosticsPNI");
            }
            else
            {
                Program.SendCommand($"{SysPaths.Explorer} \"ms-contact-support:///?ActivationType=NetworkDiagnostics\"", false);
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            RetryClicked?.Invoke(this, EventArgs.Empty);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            CloseClicked?.Invoke(this, EventArgs.Empty);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (OS.WXP || OS.WVista || OS.W7)
            {
                // Open Network Connections
                Program.SendCommand($"{SysPaths.System32}\\ncpa.cpl", false);
            }
            else
            {
                // Open Network Settings modern page
                Process.Start("ms-settings:network");
            }
        }
    }
}
