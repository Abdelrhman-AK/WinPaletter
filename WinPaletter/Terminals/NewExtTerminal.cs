using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace WinPaletter
{

    public partial class NewExtTerminal
    {
        public NewExtTerminal()
        {
            InitializeComponent();
        }
        private void Button16_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                TextBox1.Text = OpenFileDialog1.FileName;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(TextBox1.Text))
                {
                    WPStyle.MsgBox(Program.Lang.Terminal_External_Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else if (!System.IO.File.Exists(TextBox1.Text))
                {
                    WPStyle.MsgBox(Program.Lang.Terminal_External_NotExist, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else if ((TextBox1.Text.ToLower() ?? "") == ("%%Startup".ToLower() ?? "") | (TextBox1.Text.ToLower() ?? "") == ("%SystemRoot%_System32_cmd.exe".ToLower() ?? "") | (TextBox1.Text.ToLower() ?? "") == ("%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe".ToLower() ?? "") | (TextBox1.Text.ToLower() ?? "") == ("%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe".ToLower() ?? ""))
                {

                    WPStyle.MsgBox(Program.Lang.Terminal_External_Reversed, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else if (Forms.ExternalTerminal.ComboBox1.Items.Contains(TextBox1.Text))
                {
                    WPStyle.MsgBox(Program.Lang.Terminal_External_Exists, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else
                {
                    Registry.CurrentUser.CreateSubKey(string.Format(@"Console\%SystemDrive%_{0}", TextBox1.Text.Replace(@"\", "_").Trim(':')[1]), true).Close();

                    WPStyle.MsgBox(Program.Lang.ExtTer_NewSuccess, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Forms.ExternalTerminal.FillTerminals(Forms.ExternalTerminal.ComboBox1);

                }
            }

            catch (Exception ex)
            {

                WPStyle.MsgBox(Program.Lang.ExtTer_NewError, MessageBoxButtons.OK, MessageBoxIcon.Error, "", Program.Lang.CollapseNote, Program.Lang.ExpandNote, Program.Lang.ErrorDetails + ex.Message);
            }

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void NewExtTerminal_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            WPStyle.ApplyStyle(this);
            Icon = Forms.ExternalTerminal.Icon;
        }
    }
}