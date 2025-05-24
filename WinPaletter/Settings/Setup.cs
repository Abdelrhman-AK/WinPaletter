using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class Setup : Form
    {
        public Setup()
        {
            InitializeComponent();
        }

        private void Setup_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);

            textBox1.Font = Fonts.ConsoleLarge;
            textBox1.Text = Properties.Resources.LICENSE;

            // Get the assembly of the current executing code
            Assembly assembly = Assembly.GetExecutingAssembly();

            // Get the AssemblyCopyrightAttribute
            AssemblyCopyrightAttribute copyrightAttribute = (AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyCopyrightAttribute));

            copyrightsLabel.Text = $"{copyrightAttribute.Copyright}, {Application.CompanyName}";
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            tablessControl1.SelectedIndex = tablessControl1.SelectedIndex + 1 > tablessControl1.TabCount - 1 ? tablessControl1.TabCount - 1 : tablessControl1.SelectedIndex + 1;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            tablessControl1.SelectedIndex = tablessControl1.SelectedIndex - 1 < 0 ? 0 : tablessControl1.SelectedIndex - 1;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (!OS.WXP)
            {
                using (Ookii.Dialogs.WinForms.VistaFolderBrowserDialog FD = new())
                {
                    if (FD.ShowDialog() == DialogResult.OK) textBox2.Text = FD.SelectedPath;
                }
            }
            else
            {
                using (FolderBrowserDialog FD = new())
                {
                    if (FD.ShowDialog() == DialogResult.OK) textBox2.Text = FD.SelectedPath;
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Forms.SysEventsSndsInstaller.Install(true);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Forms.SysEventsSndsInstaller.Uninstall();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            Forms.SecureUxTheme_Setup.ShowDialog();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            Process.Start(Links.SecureUxThemeReleases);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            SystemRestoreHelper.CreateRestorePoint(Program.Lang.Strings.General.RestorePoint_FirstTime);
            toggle38.Checked = false;
            Cursor = Cursors.Default;

            MsgBox(Program.Lang.Strings.Messages.SysRestore_Msg2, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            SystemRestoreHelper.CreateRestorePoint(Program.Lang.Strings.General.RestorePoint_FirstTime);
            Cursor = Cursors.Default;

            MsgBox(Program.Lang.Strings.Messages.SysRestore_Msg2, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
