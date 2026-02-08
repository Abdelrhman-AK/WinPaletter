using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;
using WinPaletter.Properties;

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
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.EXE })
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
                    MsgBox(Program.Localization.Strings.Aspects.Terminals.EmptyError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else if (!File.Exists(TextBox1.Text))
                {
                    MsgBox(Program.Localization.Strings.Aspects.Terminals.NotExist, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else if ((TextBox1.Text.ToLower() ?? string.Empty) == ("%%Startup".ToLower() ?? string.Empty) | (TextBox1.Text.ToLower() ?? string.Empty) == ("%SystemRoot%_System32_cmd.exe".ToLower() ?? string.Empty) | (TextBox1.Text.ToLower() ?? string.Empty) == ("%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe".ToLower() ?? string.Empty) | (TextBox1.Text.ToLower() ?? string.Empty) == ("%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe".ToLower() ?? string.Empty))
                {

                    MsgBox(Program.Localization.Strings.Aspects.Terminals.Reserved, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else if (Forms.ExternalTerminal.ComboBox1.Items.Contains(TextBox1.Text))
                {
                    MsgBox(Program.Localization.Strings.Aspects.Terminals.Exists, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Registry.CurrentUser.CreateSubKey($"Console\\%SystemDrive%{TextBox1.Text.Replace(@"\", "_").Split(':')[1]}", true).Close();
                    Forms.ExternalTerminal.FillTerminals(Forms.ExternalTerminal.ComboBox1);
                    MsgBox(Program.Localization.Strings.Aspects.Consoles.ExtTer_NewSuccess, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            catch (Exception ex)
            {
                MsgBox(Program.Localization.Strings.Aspects.Consoles.ExtTer_NewError, MessageBoxButtons.OK, MessageBoxIcon.Error, string.Empty, Program.Localization.Strings.General.CollapseNote, Program.Localization.Strings.General.ExpandNote, $"{Program.Localization.Strings.ThemeManager.Errors.ErrorDetails}{ex.Message}");
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void NewExtTerminal_Load(object sender, EventArgs e)
        {
            Icon = Resources.cmd;
        }
    }
}