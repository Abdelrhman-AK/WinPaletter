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
            using (System.Windows.Forms.OpenFileDialog dlg = new() { Filter = Program.Filters.EXE })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    TextBox1.Text = dlg.FileName;
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TextBox1.Text))
                {
                    MsgBox(Program.Lang.Terminal_External_Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else if (!System.IO.File.Exists(TextBox1.Text))
                {
                    MsgBox(Program.Lang.Terminal_External_NotExist, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else if ((TextBox1.Text.ToLower() ?? string.Empty) == ("%%Startup".ToLower() ?? string.Empty) | (TextBox1.Text.ToLower() ?? string.Empty) == ("%SystemRoot%_System32_cmd.exe".ToLower() ?? string.Empty) | (TextBox1.Text.ToLower() ?? string.Empty) == ("%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe".ToLower() ?? string.Empty) | (TextBox1.Text.ToLower() ?? string.Empty) == ("%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe".ToLower() ?? string.Empty))
                {

                    MsgBox(Program.Lang.Terminal_External_Reversed, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else if (Forms.ExternalTerminal.ComboBox1.Items.Contains(TextBox1.Text))
                {
                    MsgBox(Program.Lang.Terminal_External_Exists, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Registry.CurrentUser.CreateSubKey($"Console\\%SystemDrive%{(TextBox1.Text.Replace(@"\", "_").Split(':')[1])}", true).Close();
                    Forms.ExternalTerminal.FillTerminals(Forms.ExternalTerminal.ComboBox1);
                    MsgBox(Program.Lang.ExtTer_NewSuccess, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            catch (Exception ex)
            {
                MsgBox(Program.Lang.ExtTer_NewError, MessageBoxButtons.OK, MessageBoxIcon.Error, string.Empty, Program.Lang.CollapseNote, Program.Lang.ExpandNote, $"{Program.Lang.ErrorDetails}{ex.Message}");
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void NewExtTerminal_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            using (ExternalTerminal formIcon = new()) { Icon = formIcon.Icon; }
        }
    }
}