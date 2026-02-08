using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.Properties;
using WinPaletter.Theme;

namespace WinPaletter
{
    public partial class BackupThemes_List : UI.WP.Form
    {
        public BackupThemes_List()
        {
            InitializeComponent();
        }

        Thread th;
        private readonly Icon icon = Resources.fileextension;

        private void BackupThemes_List_Load(object sender, EventArgs e)
        {
            PopulateThemesBackups();

            // Set up columns for the ListView
            listView1.View = View.Details;
            listView1.Columns.Clear();
            listView1.Columns.Add(Program.Localization.Strings.General.ThemeName, 150);
            listView1.Columns.Add(Program.Localization.Strings.General.FilePath, 400);
            listView1.Columns.Add(Program.Localization.Strings.General.CreationDateTime, 150);

            label3.Font = Fonts.ConsoleMedium;

            windowsDesktop1.WindowStyle = Program.WindowStyle;
        }

        private void PopulateThemesBackups()
        {
            Cursor = Cursors.WaitCursor;

            listView1.Items.Clear();

            // Set the ImageList for the ListView
            listView1.SmallImageList = imageList1;

            if (!Directory.Exists($"{Program.Settings.BackupTheme.BackupPath}\\OnThemeApply"))
                Directory.CreateDirectory($"{Program.Settings.BackupTheme.BackupPath}\\OnThemeApply");

            if (!Directory.Exists($"{Program.Settings.BackupTheme.BackupPath}\\OnAspectApply"))
                Directory.CreateDirectory($"{Program.Settings.BackupTheme.BackupPath}\\OnAspectApply");

            if (!Directory.Exists($"{Program.Settings.BackupTheme.BackupPath}\\OnThemeOpen"))
                Directory.CreateDirectory($"{Program.Settings.BackupTheme.BackupPath}\\OnThemeOpen");

            if (!Directory.Exists($"{Program.Settings.BackupTheme.BackupPath}\\OnAppOpen"))
                Directory.CreateDirectory($"{Program.Settings.BackupTheme.BackupPath}\\OnAppOpen");

            if (!Directory.Exists($"{Program.Settings.BackupTheme.BackupPath}\\OnExceptionError"))
                Directory.CreateDirectory($"{Program.Settings.BackupTheme.BackupPath}\\OnExceptionError");

            string[] themes_onThemeApply = [.. Directory.GetFiles($"{Program.Settings.BackupTheme.BackupPath}\\OnThemeApply", "*.wpth").OrderByDescending(file => new FileInfo(file).CreationTime)];
            string[] themes_onAspectApply = [.. Directory.GetFiles($"{Program.Settings.BackupTheme.BackupPath}\\OnAspectApply", "*.wpth").OrderByDescending(file => new FileInfo(file).CreationTime)];
            string[] themes_onThemeOpen = [.. Directory.GetFiles($"{Program.Settings.BackupTheme.BackupPath}\\OnThemeOpen", "*.wpth").OrderByDescending(file => new FileInfo(file).CreationTime)];
            string[] themes_onAppOpen = [.. Directory.GetFiles($"{Program.Settings.BackupTheme.BackupPath}\\OnAppOpen", "*.wpth").OrderByDescending(file => new FileInfo(file).CreationTime)];
            string[] themes_onExErrors = [.. Directory.GetFiles($"{Program.Settings.BackupTheme.BackupPath}\\OnExceptionError", "*.wpth").OrderByDescending(file => new FileInfo(file).CreationTime)];

            // Create groups
            ListViewGroup group1 = new(Program.Localization.Strings.Backup.Group_ThemeApply, HorizontalAlignment.Center);
            ListViewGroup group2 = new(Program.Localization.Strings.Backup.Group_AspectApply, HorizontalAlignment.Center);
            ListViewGroup group3 = new(Program.Localization.Strings.Backup.Group_AppOpen, HorizontalAlignment.Center);
            ListViewGroup group4 = new(Program.Localization.Strings.Backup.Group_ThemeOpen, HorizontalAlignment.Center);
            ListViewGroup group5 = new(Program.Localization.Strings.Backup.Group_ThemeExError, HorizontalAlignment.Center);

            // Add groups to the ListView
            listView1.Groups.Add(group1);
            listView1.Groups.Add(group2);
            listView1.Groups.Add(group3);
            listView1.Groups.Add(group4);
            listView1.Groups.Add(group5);

            IEnumerable<string> backups = Directory.EnumerateFiles(Program.Settings.BackupTheme.BackupPath, "*", SearchOption.AllDirectories);
            label3.Text = backups.Sum(fileInfo => new FileInfo(fileInfo).Length).ToStringFileSize();
            label4.Text = $"{backups.Count()} {Program.Localization.Strings.General.BackupsCount}";

            Task.Run(() =>
            {
                // SetFormValues the ListView
                AddBackups(listView1, imageList1, themes_onThemeApply, group1);
                AddBackups(listView1, imageList1, themes_onAspectApply, group2);
                AddBackups(listView1, imageList1, themes_onAppOpen, group3);
                AddBackups(listView1, imageList1, themes_onThemeOpen, group4);
                AddBackups(listView1, imageList1, themes_onExErrors, group5);

                // Resize the columns
                listView1.AutoResizeColumns(listView1.Items.Count > 0 ? ColumnHeaderAutoResizeStyle.ColumnContent : ColumnHeaderAutoResizeStyle.HeaderSize);

                Cursor = Cursors.Default;
            });
        }

        void AddBackups(ListView listView, ImageList imageList, string[] files, ListViewGroup group)
        {
            foreach (string file in files)
            {
                try
                {
                    using (Manager TMx = new(Manager.Source.File, file, true, true))
                    {
                        string name = TMx.Info.ThemeName;
                        ListViewItem item = new(name);
                        item.SubItems.Add(file);
                        item.SubItems.Add(File.GetCreationTime(file).ToString());
                        item.ImageKey = name;
                        item.Group = group;

                        imageList.Images.Add(name, icon.ToBitmap());
                        listView.Items.Add(item);
                        listView.Items[listView.Items.Count - 1].ImageIndex = imageList.Images.Count - 1;
                    }
                }
                catch { } //Ignore an invalid theme
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (MsgBox(Program.Localization.Strings.Backup.RestoreQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Program.TM = new(Manager.Source.File, listView1.SelectedItems[0].SubItems[1].Text);
                    Program.TM_Original = Program.TM.Clone();

                    Forms.MainForm.tabsContainer1.SelectedIndex = 0;
                    Forms.Home.LoadFromTM(Program.TM);
                    Forms.Home.Text = Path.GetFileName(listView1.SelectedItems[0].SubItems[1].Text);
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                button1.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                label1.Visible = false;

                if (File.Exists(listView1.SelectedItems[0].SubItems[1].Text))
                {
                    if (th != null && th.IsAlive) th.Abort();

                    th = new(() =>
                    {
                        Cursor = Cursors.AppStarting;
                        Program.Animator.HideSync(windowsDesktop1);

                        using (Manager TMx = new(Manager.Source.File, listView1.SelectedItems[0].SubItems[1].Text, true, true))
                        {
                            windowsDesktop1.LoadFromTM(TMx);
                            windowsDesktop1.BackgroundImage = Program.WallpaperMonitor.Get(TMx, Program.WindowStyle);
                        }

                        Program.Animator.ShowSync(windowsDesktop1);
                        Cursor = Cursors.Default;
                    });

                    th.Start();
                }
            }
            else
            {
                button1.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                label1.Visible = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (File.Exists(listView1.SelectedItems[0].SubItems[1].Text))
                {
                    using (SaveFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, Title = Program.Localization.Strings.Extensions.SaveWinPaletterTheme })
                    {
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            File.Copy(listView1.SelectedItems[0].SubItems[1].Text, dlg.FileName, true);
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (MsgBox(Program.Localization.Strings.Backup.DeleteQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Program.Animator.HideSync(windowsDesktop1);
                    File.Delete(listView1.SelectedItems[0].SubItems[1].Text);
                    if (File.Exists($"{Path.GetFileNameWithoutExtension(listView1.SelectedItems[0].SubItems[1].Text)}.wptp"))
                    {
                        File.Delete($"{Path.GetFileNameWithoutExtension(listView1.SelectedItems[0].SubItems[1].Text)}.wptp");
                    }

                    PopulateThemesBackups();
                }
            }
        }

        private void listView1_SizeChanged(object sender, EventArgs e)
        {
            RefreshSize();
        }

        private void RefreshSize()
        {
            if (listView1.Columns.Count >= 3)
            {
                listView1.Columns[1].Width = listView1.Width - listView1.Columns[0].Width - listView1.Columns[2].Width;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Process.Start(SysPaths.Explorer, $"/select, \"{listView1.SelectedItems[0].SubItems[1].Text}\"");
            }
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            if (MsgBox(Program.Localization.Strings.Backup.DeleteAllQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Program.Animator.HideSync(windowsDesktop1);
                Directory.Delete($"{Program.Settings.BackupTheme.BackupPath}\\OnThemeApply", true);
                Directory.Delete($"{Program.Settings.BackupTheme.BackupPath}\\OnAspectApply", true);
                Directory.Delete($"{Program.Settings.BackupTheme.BackupPath}\\OnThemeOpen", true);
                Directory.Delete($"{Program.Settings.BackupTheme.BackupPath}\\OnAppOpen", true);
                Directory.Delete($"{Program.Settings.BackupTheme.BackupPath}\\OnExceptionError", true);
                PopulateThemesBackups();
            }

            RefreshSize();
        }

        private void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            using (SolidBrush br = new(scheme.Colors.Back((sender as ListView).Level())))
            {
                e.Graphics.FillRectangle(br, e.Bounds);
            }

            TextRenderer.DrawText(e.Graphics, e.Header.Text, listView1.Font, e.Bounds, listView1.ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
        }

        private void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (MsgBox(Program.Localization.Strings.Backup.RestoreQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Program.TM = new(Manager.Source.File, listView1.SelectedItems[0].SubItems[1].Text, false, true);
                    Program.TM_Original = Program.TM.Clone();
                    Forms.ThemeLog.Apply_Theme();

                    Forms.MainForm.tabsContainer1.SelectedIndex = 0;
                    Forms.Home.LoadFromTM(Program.TM);
                    Forms.Home.Text = Path.GetFileName(listView1.SelectedItems[0].SubItems[1].Text);
                }
            }
        }

        private void pin_button_Click(object sender, EventArgs e)
        {
            Forms.MainForm.tabsContainer1.AddFormIntoTab(this);
        }

        private void BackupThemes_List_ParentChanged(object sender, EventArgs e)
        {
            if (Parent != null && Parent is TabPage)
            {
                pin_button.Visible = false;
            }
            else
            {
                pin_button.Visible = true;
            }
        }
    }
}