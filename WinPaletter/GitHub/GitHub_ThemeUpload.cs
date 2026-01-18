using Ookii.Dialogs.WinForms;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class GitHub_ThemeUpload : Form
    {
        private bool _syncingToggles;

        public GitHub_ThemeUpload()
        {
            InitializeComponent();
        }

        private void GitHub_ThemeUpload_Load(object sender, EventArgs e)
        {
            ApplyStyle(this);
            this.LoadLanguage();
        }

        private void toggle_uploadOne_CheckedChanged(object sender, EventArgs e)
        {
            alertBox1.Visible = ReadyToUpload();

            if (_syncingToggles || !toggle_uploadOne.Checked) return;
            _syncingToggles = true;

            toggle_uploadMultiple.Checked = false;
            toggle_uploadCurrent.Checked = false;
            toggle_wpTheme.Checked = false;

            _syncingToggles = false;
        }

        private void toggle_uploadMultiple_CheckedChanged(object sender, EventArgs e)
        {
            alertBox1.Visible = ReadyToUpload();

            if (_syncingToggles || !toggle_uploadMultiple.Checked) return;
            _syncingToggles = true;

            toggle_uploadOne.Checked = false;
            toggle_uploadCurrent.Checked = false;
            toggle_wpTheme.Checked = false;

            _syncingToggles = false;

        }

        private void toggle_wpTheme_CheckedChanged(object sender, EventArgs e)
        {
            alertBox1.Visible = ReadyToUpload();

            if (_syncingToggles || !toggle_wpTheme.Checked) return;
            _syncingToggles = true;

            toggle_uploadOne.Checked = false;
            toggle_uploadMultiple.Checked = false;
            toggle_uploadCurrent.Checked = false;

            _syncingToggles = false;

        }

        private void toggle_uploadCurrent_CheckedChanged(object sender, EventArgs e)
        {
            alertBox1.Visible = ReadyToUpload();

            if (_syncingToggles || !toggle_uploadCurrent.Checked) return;
            _syncingToggles = true;

            toggle_uploadOne.Checked = false;
            toggle_uploadMultiple.Checked = false;
            toggle_wpTheme.Checked = false;

            _syncingToggles = false;

        }

        private void Button16_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, FileName = textBox1.Text, Title = Program.Lang.Strings.Extensions.OpenWinPaletterTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;

                    string file = dlg.FileName;
                    string themeResPack = System.IO.Path.GetDirectoryName(file) + "\\" + System.IO.Path.GetFileNameWithoutExtension(file) + ".wptp";

                    if (IsValidTheme(file))
                    {
                        textBox1.Text = file;
                        textBox2.Text = string.Empty;

                        if (System.IO.File.Exists(themeResPack))
                        {
                            using (Theme.Manager TM = new(Theme.Manager.Source.File, file, true, true))
                            {
                                if (TM.Info.ExportResThemePack)
                                {
                                    textBox2.Text = themeResPack;
                                }
                            }
                        }
                    }

                    Cursor = Cursors.Default;
                }
            }
        }

        private bool IsValidTheme(string file, bool silent = false)
        {
            Theme.Manager.Editions edition = Theme.Manager.GetEdition(file);

            // Decompress the theme File content_list
            List<string> content_list = Theme.Manager.Decompress(file) as List<string>;

            // Check if the theme File content_list is null or empty
            if (content_list is null || content_list.Count == 0) return false;

            string content = string.Join("\r\n", content_list);

            if (edition == Theme.Manager.Editions.JSON && Theme.Manager.IsValidJson(content))
            {
                return true;
            }
            else if (edition == Theme.Manager.Editions.Legacy)
            {
                Program.Log?.Write(LogEventLevel.Error, $"The used wpth file has the old format (obsolete.)");

                // Display a message box for old format themes
                if (!silent) MsgBox(Program.Lang.Strings.Converter.Detect_Old_OnLoading0, MessageBoxButtons.OK, MessageBoxIcon.Error, Program.Lang.Strings.Converter.Detect_Old_OnLoadingTip);
                return false;
            }
            else
            {
                Program.Log?.Write(LogEventLevel.Error, $"The used wpth file is invalid.");

                // Display a message box for invalid JSON
                if (!silent) MsgBox(Program.Lang.Strings.Converter.Error_Phrasing, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string selectedPath = string.Empty;

            if (!OS.WXP)
            {
                using (VistaFolderBrowserDialog FD = new())
                {
                    if (FD.ShowDialog() == DialogResult.OK) selectedPath = FD.SelectedPath;
                }
            }
            else
            {
                using (FolderBrowserDialog FD = new())
                {
                    if (FD.ShowDialog() == DialogResult.OK) selectedPath = FD.SelectedPath;
                }
            }

            if (!string.IsNullOrEmpty(selectedPath)) textBox3.Text = selectedPath;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            List<string> files = [];

            if (toggle_uploadOne.Checked)
            {
                string themeFile = textBox1.Text;
                string themeResPack = textBox2.Text;

                if (string.IsNullOrEmpty(themeFile) || !System.IO.File.Exists(themeFile))
                {
                    MsgBox(Program.Lang.Strings.Messages.ThemeFileNotExist, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (!IsValidTheme(themeFile))
                {
                    return;
                }
                else if (!string.IsNullOrEmpty(themeResPack) && !System.IO.File.Exists(themeResPack)) // Theme Resource Pack is optional
                {
                    MsgBox(Program.Lang.Strings.Messages.ThemeResPackNotExist, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                files.Add(themeFile);
                if (!string.IsNullOrEmpty(themeResPack)) files.Add(themeResPack);
            }
            else if (toggle_uploadMultiple.Checked)
            {
                string directory = textBox3.Text;

                if (string.IsNullOrEmpty(directory) || !System.IO.Directory.Exists(directory))
                {
                    MsgBox(Program.Lang.Strings.Messages.ThemesDirectoryNotExist, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                foreach (string file in System.IO.Directory.GetFiles(directory, "*.wpth", CheckBox1.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
                {
                    if (IsValidTheme(file, true))
                    {
                        string themeResPack = System.IO.Path.GetDirectoryName(file) + "\\" + System.IO.Path.GetFileNameWithoutExtension(file) + ".wptp";

                        if (System.IO.File.Exists(themeResPack))
                        {
                            using (Theme.Manager TM = new(Theme.Manager.Source.File, file, true, true))
                            {
                                if (!TM.Info.ExportResThemePack) themeResPack = string.Empty;
                            }
                        }
                        else
                        {
                            themeResPack = string.Empty;
                        }

                        files.Add(file);
                        if (!string.IsNullOrEmpty(themeResPack)) files.Add(themeResPack);
                    }
                }

                if (files.Count == 0)
                {
                    MsgBox(Program.Lang.Strings.Messages.NoValidThemesFound, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else if (toggle_wpTheme.Checked || toggle_uploadCurrent.Checked)
            {
                string tempThemePath;
                string themeResPack;
                string tempResPackPath;

                string correctThemeName(string themeName)
                {
                    // Replace invalid filename characters with '.'
                    foreach (char ch in Path.GetInvalidFileNameChars())
                    {
                        themeName = themeName.Replace(ch, '.');
                    }

                    foreach (string ch in GitHub.FileSystem.InvalidFileNameChars)
                    {
                        themeName = themeName.Replace(ch, ".");
                    }

                    // Replace spaces with underscores
                    themeName = themeName.Replace(" ", "_");

                    // Avoid Windows reserved names automatically
                    if (GitHub.FileSystem.InvalidFileNames.Contains(themeName, StringComparer.OrdinalIgnoreCase)) themeName = "." + themeName;

                    // Collapse multiple consecutive dots
                    while (themeName.Contains("..")) themeName = themeName.Replace("..", ".");

                    return themeName;
                }

                if (toggle_wpTheme.Checked)
                {
                    string themeName = correctThemeName(Program.TM.Info.ThemeName);
                    tempThemePath = Path.Combine(Path.GetTempPath(), themeName + ".wpth");
                    themeResPack = System.IO.Path.GetDirectoryName(tempThemePath) + "\\" + System.IO.Path.GetFileNameWithoutExtension(tempThemePath) + ".wptp";

                    Program.TM.Save(Theme.Manager.Source.File, tempThemePath);

                    if (Program.TM.Info.ExportResThemePack && System.IO.File.Exists(themeResPack))
                    {
                        tempResPackPath = themeResPack;
                    }
                    else
                    {
                        tempResPackPath = string.Empty;
                    }
                }
                else
                {
                    using (Theme.Manager TM = new(Theme.Manager.Source.Registry))
                    {
                        string themeName = correctThemeName(TM.Info.ThemeName);
                        tempThemePath = Path.Combine(Path.GetTempPath(), themeName + ".wpth");
                        themeResPack = System.IO.Path.GetDirectoryName(tempThemePath) + "\\" + System.IO.Path.GetFileNameWithoutExtension(tempThemePath) + ".wptp";

                        TM.Save(Theme.Manager.Source.File, tempThemePath);

                        if (TM.Info.ExportResThemePack && System.IO.File.Exists(themeResPack))
                        {
                            tempResPackPath = themeResPack;
                        }
                        else
                        {
                            tempResPackPath = string.Empty;
                        }
                    }
                }

                files.Add(tempThemePath);
                if (!string.IsNullOrEmpty(tempResPackPath)) files.Add(tempResPackPath);
            }
            else
            {
                MsgBox(Program.Lang.Strings.GitHubStrings.SelectUploadMethod, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (files.Count > 0)
            {
                Close();
                await Forms.GitHub_Mgr.UploadListAsync(files);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool ReadyToUpload()
        {
            if (toggle_uploadOne.Checked)
            {
                string themeFile = textBox1.Text;
                string themeResPack = textBox2.Text;

                if (string.IsNullOrEmpty(themeFile) || !System.IO.File.Exists(themeFile))
                {
                    return false;
                }
                else if (!IsValidTheme(themeFile, true))
                {
                    return false;
                }
                else if (!string.IsNullOrEmpty(themeResPack) && !System.IO.File.Exists(themeResPack)) // Theme Resource Pack is optional
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (toggle_uploadMultiple.Checked)
            {
                string directory = textBox3.Text;

                if (string.IsNullOrEmpty(directory) || !System.IO.Directory.Exists(directory))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (toggle_wpTheme.Checked || toggle_uploadCurrent.Checked)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            alertBox1.Visible = ReadyToUpload();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            alertBox1.Visible = ReadyToUpload();
        }
    }
}
