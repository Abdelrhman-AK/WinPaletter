using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class BackupThemes_List : Form
    {
        public BackupThemes_List()
        {
            InitializeComponent();
        }

        Thread th;

        private void BackupThemes_List_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);

            PopulateThemesBackups();

            // Set up columns for the ListView
            listView1.View = View.Details;
            listView1.Columns.Clear();
            listView1.Columns.Add(Program.Lang.Backup_ThemeName, 150);
            listView1.Columns.Add(Program.Lang.Backup_FilePath, 400);
            listView1.Columns.Add(Program.Lang.Backup_CreationDateTime, 150);

            CheckForIllegalCrossThreadCalls = false;

            label3.Font = Fonts.ConsoleMedium;

            windowsDesktop1.WindowStyle = Program.WindowStyle;
        }

        private void PopulateThemesBackups()
        {
            listView1.Items.Clear();

            // Set the ImageList for the ListView
            listView1.SmallImageList = imageList1;

            if (!System.IO.Directory.Exists(Program.Settings.BackupTheme.BackupPath + "\\OnThemeApply"))
                System.IO.Directory.CreateDirectory(Program.Settings.BackupTheme.BackupPath + "\\OnThemeApply");

            if (!System.IO.Directory.Exists(Program.Settings.BackupTheme.BackupPath + "\\OnThemeOpen"))
                System.IO.Directory.CreateDirectory(Program.Settings.BackupTheme.BackupPath + "\\OnThemeOpen");

            if (!System.IO.Directory.Exists(Program.Settings.BackupTheme.BackupPath + "\\OnAppOpen"))
                System.IO.Directory.CreateDirectory(Program.Settings.BackupTheme.BackupPath + "\\OnAppOpen");
                                   
            string[] themes_onThemeApply = Directory.GetFiles(Program.Settings.BackupTheme.BackupPath + "\\OnThemeApply", "*.wpth").OrderByDescending(file => new FileInfo(file).CreationTime).ToArray();
            string[] themes_onThemeOpen = Directory.GetFiles(Program.Settings.BackupTheme.BackupPath + "\\OnThemeOpen", "*.wpth").OrderByDescending(file => new FileInfo(file).CreationTime).ToArray();
            string[] themes_onAppOpen = Directory.GetFiles(Program.Settings.BackupTheme.BackupPath + "\\OnAppOpen", "*.wpth").OrderByDescending(file => new FileInfo(file).CreationTime).ToArray();

            // Create three groups
            ListViewGroup group1 = new(Program.Lang.Backup_Group_AppOpen, HorizontalAlignment.Center);
            ListViewGroup group2 = new(Program.Lang.Backup_Group_ThemeApply, HorizontalAlignment.Center);
            ListViewGroup group3 = new(Program.Lang.Backup_Group_ThemeOpen, HorizontalAlignment.Center);

            // Add groups to the ListView
            listView1.Groups.Add(group2);
            listView1.Groups.Add(group1);
            listView1.Groups.Add(group3);

            // Populate the ListView
            AddBackups(listView1, imageList1, themes_onAppOpen, group1);
            AddBackups(listView1, imageList1, themes_onThemeApply, group2);
            AddBackups(listView1, imageList1, themes_onThemeOpen, group3);

            // Resize the columns
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            label3.Text = Directory.EnumerateFiles(Program.Settings.BackupTheme.BackupPath, "*", SearchOption.AllDirectories).Sum(fileInfo => new FileInfo(fileInfo).Length).SizeString();
        }

        static void AddBackups(ListView listView, ImageList imageList, string[] files, ListViewGroup group)
        {
            foreach (string file in files)
            {
                try
                {
                    using (Theme.Manager TMx = new(Theme.Manager.Source.File, file))
                    {
                        string name = TMx.Info.ThemeName;

                        Icon icon = Properties.Resources.fileextension;

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
                catch { }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (MsgBox(Program.Lang.Backup_RestoreQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Program.TM = new(Theme.Manager.Source.File, listView1.SelectedItems[0].SubItems[1].Text);
                    Program.TM_Original = Program.TM.Clone() as Theme.Manager;

                    Forms.MainFrm.tabsContainer1.SelectedIndex = 0;
                    Forms.Dashboard.LoadFromTM(Program.TM);
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
                label1.Visible = false;

                if (System.IO.File.Exists(listView1.SelectedItems[0].SubItems[1].Text))
                {
                    if (th != null && th.IsAlive) th.Abort();

                    th = new(() =>
                    {
                        Cursor = Cursors.AppStarting;
                        Program.Animator.HideSync(windowsDesktop1);
                        windowsDesktop1.LoadFromTM(new(Theme.Manager.Source.File, listView1.SelectedItems[0].SubItems[1].Text));
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
                label1.Visible = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (System.IO.File.Exists(listView1.SelectedItems[0].SubItems[1].Text))
                {
                    if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        System.IO.File.Copy(listView1.SelectedItems[0].SubItems[1].Text, SaveFileDialog1.FileName, true);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (MsgBox(Program.Lang.Backup_DeleteQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Program.Animator.HideSync(windowsDesktop1);
                    System.IO.File.Delete(listView1.SelectedItems[0].SubItems[1].Text);
                    if (System.IO.File.Exists(System.IO.Path.GetFileNameWithoutExtension(listView1.SelectedItems[0].SubItems[1].Text) + ".wptp"))
                    {
                        System.IO.File.Delete(System.IO.Path.GetFileNameWithoutExtension(listView1.SelectedItems[0].SubItems[1].Text) + ".wptp");
                    }

                    PopulateThemesBackups();
                }
            }
        }

        private void listView1_SizeChanged(object sender, EventArgs e)
        {
            try { listView1.Columns[1].Width = listView1.Width - listView1.Columns[0].Width - listView1.Columns[2].Width; } catch { }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                System.Diagnostics.Process.Start("explorer.exe", "/select, \"" + listView1.SelectedItems[0].SubItems[1].Text + "\"");
            }
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            if (MsgBox(Program.Lang.Backup_DeleteAllQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Program.Animator.HideSync(windowsDesktop1);
                System.IO.Directory.Delete(Program.Settings.BackupTheme.BackupPath + "\\OnThemeApply", true);
                System.IO.Directory.Delete(Program.Settings.BackupTheme.BackupPath + "\\OnThemeOpen", true);
                System.IO.Directory.Delete(Program.Settings.BackupTheme.BackupPath + "\\OnAppOpen", true);
                PopulateThemesBackups();
            }
        }

        private void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            Config.Scheme scheme = Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

            e.Graphics.FillRectangle(scheme.Brushes.Back, e.Bounds);

            TextRenderer.DrawText(e.Graphics, e.Header.Text, listView1.Font, e.Bounds, listView1.ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
        }

        private void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }
    }
}