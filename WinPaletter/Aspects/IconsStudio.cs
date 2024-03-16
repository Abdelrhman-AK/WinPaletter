using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.UI.Simulation;
using static WinPaletter.Theme.Manager;

namespace WinPaletter
{
    public partial class IconsStudio : AspectsTemplate
    {
        public IconsStudio()
        {
            InitializeComponent();
        }

        private void LoadFromWPTH(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, Title = Program.Lang.Filter_OpenWinPaletterTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Theme.Manager TMx = new(Theme.Manager.Source.File, dlg.FileName))
                    {
                        LoadFromTM(TMx);
                    }
                }
            }
        }

        private void LoadFromCurrent(object sender, EventArgs e)
        {
            Theme.Manager TMx = new(Theme.Manager.Source.Registry);
            LoadFromTM(TMx);
            TMx.Dispose();
        }

        private void LoadFromDefault(object sender, EventArgs e)
        {
            Theme.Manager TMx = Theme.Default.Get(Program.WindowStyle);
            LoadFromTM(TMx);
            TMx.Dispose();
        }

        private void LoadIntoCurrentTheme(object sender, EventArgs e)
        {
            ApplyToTM(Program.TM);
            Close();
        }

        private void Apply(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            using (Theme.Manager TMx = new(Theme.Manager.Source.Registry))
            {
                if (Program.Settings.BackupTheme.Enabled && Program.Settings.BackupTheme.AutoBackupOnApplySingleAspect)
                {
                    string filename = Program.GetUniqueFileName($"{Program.Settings.BackupTheme.BackupPath}\\OnAspectApply", $"{TMx.Info.ThemeName}_{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.wpth");
                    TMx.Save(Source.File, filename);
                }

                ApplyToTM(TMx);
                ApplyToTM(Program.TM);
                ApplyToTM(Program.TM_Original);
                TMx.Icons.Apply();
            }

            Cursor = Cursors.Default;
        }

        private void IconsStudio_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = Program.Lang.WindowsIcons,
                Enabled = Program.TM.Icons.Enabled,
                Import_theme = false,
                Import_msstyles = false,
                GeneratePalette = false,
                GenerateMSTheme = false,
                Import_preset = false,
                CanSwitchMode = false,

                OnLoadIntoCurrentTheme = LoadIntoCurrentTheme,
                OnApply = Apply,
                OnImportFromDefault = LoadFromDefault,
                OnImportFromWPTH = LoadFromWPTH,
                OnImportFromCurrentApplied = LoadFromCurrent,
            };

            LoadData(data);

            PopulateShell32Icons();
            PopulateControlPanelIcons();
            PopulateExplorerIcons();

            pnl_preview.BackgroundImage = Program.Wallpaper;

            foreach (WinIcon winIcon in pnl_preview.GetAllControls().OfType<WinIcon>())
            {
                winIcon.IconSize = Program.TM.MetricsFonts.DesktopIconSize;
            }

            winIcon2.Text = User.UserName;
            winIcon1.Text = OS.W8x || OS.W10 || OS.W11 || OS.W12 ? "This PC" : "Computer";
            label3.Text = winIcon1.Text;

            using (Icon sysdrv = PE.GetIcon(SysPaths.imageres, -36))
            {
                if (sysdrv != null) pictureBox1.Image = sysdrv.ToBitmap();
            }

            using (Icon drv = PE.GetIcon($"{SysPaths.System32}\\shell32.dll", -9))
            {
                if (drv != null) pictureBox6.Image = drv.ToBitmap();
            }

            using (Icon drv = PE.GetIcon($"{SysPaths.System32}\\shell32.dll", -8))
            {
                if (drv != null) pictureBox9.Image = drv.ToBitmap();
            }

            LoadFromTM(Program.TM);
        }

        private void IconsStudio_FormClosing(object sender, FormClosingEventArgs e)
        {
            CleanUp();
        }

        void PopulateShell32Icons()
        {
            shell32Data.Rows.Clear();
            shell32Data.Visible = false;

            Cursor = Cursors.WaitCursor;

            string shell32 = $"{SysPaths.System32}\\shell32.dll";

            List<DataGridViewRow> rowsToAdd = new();
            rowsToAdd.Clear();

            for (int i = 0, count = PE.GetIconGroupCount(shell32); i < count; i++)
            {
                DataGridViewRow row = new();
                row.CreateCells(shell32Data, i, PE.GetIcon(shell32, i), null, string.Empty, Program.Lang.Browse);
                rowsToAdd.Add(row);
            }

            shell32Data.Rows.AddRange(rowsToAdd.ToArray());

            // remove default [x] image for data DataGridViewImageColumn columns
            foreach (DataGridViewColumn column in shell32Data.Columns)
            {
                if (column is DataGridViewImageColumn) (column as DataGridViewImageColumn).DefaultCellStyle.NullValue = null;
            }

            shell32Data.Visible = true;

            Cursor = Cursors.Default;
        }

        void PopulateControlPanelIcons()
        {
            cpData.Rows.Clear();
            cpData.Visible = false;

            Cursor = Cursors.WaitCursor;

            List<DataGridViewRow> rowsToAdd = new();
            rowsToAdd.Clear();

            for (int i = 0, count = Theme.Structures.Icons.ControlPanelCLSIDs.Count; i < count; i++)
            {
                DataGridViewRow row = new();

                string CLSID = Theme.Structures.Icons.ControlPanelCLSIDs[i].Item1;
                string name = Theme.Structures.Icons.ControlPanelCLSIDs[i].Item2;
                string defaultIconPath = Theme.Structures.Icons.ControlPanelCLSIDs[i].Item3;
                Icon icon;

                if (defaultIconPath != null && defaultIconPath.Contains(",") && System.IO.File.Exists(defaultIconPath.Split(',')[0]))
                {
                    string[] parts = defaultIconPath.Split(',');
                    icon = PE.GetIcon(parts[0], int.Parse(parts[1]));
                }
                else if (defaultIconPath != null && System.IO.File.Exists(defaultIconPath) && System.IO.Path.GetExtension(defaultIconPath).ToLower() == ".ico")
                {
                    icon = new(defaultIconPath);
                }
                else
                {
                    icon = null;
                }

                row.CreateCells(cpData, name, CLSID, icon, null, string.Empty, Program.Lang.Browse);

                rowsToAdd.Add(row);
            }

            cpData.Rows.AddRange(rowsToAdd.ToArray());

            // remove default [x] image for data DataGridViewImageColumn columns
            foreach (DataGridViewColumn column in cpData.Columns)
            {
                if (column is DataGridViewImageColumn) (column as DataGridViewImageColumn).DefaultCellStyle.NullValue = null;
            }

            cpData.Visible = true;

            Cursor = Cursors.Default;
        }

        void PopulateExplorerIcons()
        {
            explorerData.Rows.Clear();
            explorerData.Visible = false;

            Cursor = Cursors.WaitCursor;

            List<DataGridViewRow> rowsToAdd = new();
            rowsToAdd.Clear();

            for (int i = 0, count = Theme.Structures.Icons.ExplorerCLSIDs.Count; i < count; i++)
            {
                DataGridViewRow row = new();

                string CLSID = Theme.Structures.Icons.ExplorerCLSIDs[i].Item1;
                string name = Theme.Structures.Icons.ExplorerCLSIDs[i].Item2;
                string defaultIconPath = Theme.Structures.Icons.ExplorerCLSIDs[i].Item3;
                Icon icon;

                if (defaultIconPath != null && defaultIconPath.Contains(",") && System.IO.File.Exists(defaultIconPath.Split(',')[0]))
                {
                    string[] parts = defaultIconPath.Split(',');
                    icon = PE.GetIcon(parts[0], int.Parse(parts[1]));
                }
                else if (defaultIconPath != null && System.IO.File.Exists(defaultIconPath) && System.IO.Path.GetExtension(defaultIconPath).ToLower() == ".ico")
                {
                    icon = new(defaultIconPath);
                }
                else
                {
                    icon = null;
                }

                row.CreateCells(explorerData, name, CLSID, icon, null, string.Empty, Program.Lang.Browse);

                rowsToAdd.Add(row);
            }

            explorerData.Rows.AddRange(rowsToAdd.ToArray());

            // remove default [x] image for data DataGridViewImageColumn columns
            foreach (DataGridViewColumn column in explorerData.Columns)
            {
                if (column is DataGridViewImageColumn) (column as DataGridViewImageColumn).DefaultCellStyle.NullValue = null;
            }

            explorerData.Visible = true;

            Cursor = Cursors.Default;
        }

        void CleanUp()
        {
            if (explorerData != null)
            {
                for (int i = 0; i < explorerData.Rows.Count; i++)
                {
                    if (explorerData[2, i].Value is Icon icon0) icon0?.Dispose();
                    if (explorerData[3, i].Value is Icon icon1) icon1?.Dispose();
                }

                explorerData.Rows.Clear();
            }

            if (cpData != null)
            {
                for (int i = 0; i < cpData.Rows.Count; i++)
                {
                    if (cpData[2, i].Value is Icon icon0) icon0?.Dispose();
                    if (cpData[3, i].Value is Icon icon1) icon1?.Dispose();
                }

                cpData.Rows.Clear();
            }

            if (shell32Data != null)
            {
                for (int i = 0; i < shell32Data.Rows.Count; i++)
                {
                    if (shell32Data[1, i].Value is Icon icon0) icon0?.Dispose();
                    if (shell32Data[2, i].Value is Icon icon1) icon1?.Dispose();
                }

                shell32Data.Rows.Clear();
            }

            //winIcon1?.Icon?.Dispose();
            //winIcon2?.Icon?.Dispose();
            //winIcon3?.Icon?.Dispose();
            //winIcon4?.Icon?.Dispose();
            //winIcon5?.Icon?.Dispose();
            //winIcon6?.Icon?.Dispose();

            pictureBox1?.Image?.Dispose();
            pictureBox6?.Image?.Dispose();
            pictureBox9?.Image?.Dispose();
            pictureBox2?.Image?.Dispose();
            pictureBox5?.Image?.Dispose();
            pictureBox8?.Image?.Dispose();
        }

        public void LoadFromTM(Theme.Manager TM)
        {
            AspectEnabled = TM.Icons.Enabled;

            textBox3.Text = TM.Icons.Computer;
            textBox1.Text = TM.Icons.User;
            textBox2.Text = TM.Icons.RecycleBinEmpty;
            textBox6.Text = TM.Icons.RecycleBinFull;
            textBox5.Text = TM.Icons.Network;
            textBox4.Text = TM.Icons.ControlPanel;
            textBox7.Text = TM.Icons.SystemDriveIcon;

            checkBox1.Checked = !TM.Icons.Computer_HideInDesktop;
            checkBox2.Checked = !TM.Icons.User_HideInDesktop;
            checkBox3.Checked = !TM.Icons.RecycleBin_HideInDesktop;
            checkBox5.Checked = !TM.Icons.Network_HideInDesktop;
            checkBox4.Checked = !TM.Icons.ControlPanel_HideInDesktop;

            shell32Data.Visible = false;
            if (TM.Icons.Shell32Wrapper.Count > 0)
            {
                foreach (KeyValuePair<string, string> entry in TM.Icons.Shell32Wrapper)
                {
                    foreach (DataGridViewRow item in shell32Data.Rows)
                    {
                        if (item is DataGridViewRow row && row.Cells[0].Value.ToString() == entry.Key)
                        {
                            row.Cells[3].Value = entry.Value;
                        }
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow item in shell32Data.Rows)
                {
                    if (item is DataGridViewRow row)
                    {
                        row.Cells[3].Value = null;
                    }
                }
            }
            shell32Data.Visible = true;

            cpData.Visible = false;
            if (TM.Icons.ControlPanelWrapper.Count > 0)
            {
                foreach (KeyValuePair<string, string> entry in TM.Icons.ControlPanelWrapper)
                {
                    foreach (DataGridViewRow item in cpData.Rows)
                    {
                        if (item is DataGridViewRow row && row.Cells[1].Value.ToString() == entry.Key)
                        {
                            row.Cells[4].Value = entry.Value;
                        }
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow item in cpData.Rows)
                {
                    if (item is DataGridViewRow row)
                    {
                        row.Cells[4].Value = null;
                    }
                }
            }
            cpData.Visible = true;

            explorerData.Visible = false;
            if (TM.Icons.ExplorerWrapper.Count > 0)
            {
                foreach (KeyValuePair<string, string> entry in TM.Icons.ExplorerWrapper)
                {
                    foreach (DataGridViewRow item in explorerData.Rows)
                    {
                        if (item is DataGridViewRow row && row.Cells[1].Value.ToString() == entry.Key)
                        {
                            row.Cells[4].Value = entry.Value;
                        }
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow item in explorerData.Rows)
                {
                    if (item is DataGridViewRow row)
                    {
                        row.Cells[4].Value = null;
                    }
                }
            }
            explorerData.Visible = true;
        }

        public void ApplyToTM(Theme.Manager TM)
        {
            TM.Icons.Enabled = AspectEnabled;

            TM.Icons.Computer = textBox3.Text;
            TM.Icons.User = textBox1.Text;
            TM.Icons.RecycleBinEmpty = textBox2.Text;
            TM.Icons.RecycleBinFull = textBox6.Text;
            TM.Icons.Network = textBox5.Text;
            TM.Icons.ControlPanel = textBox4.Text;
            TM.Icons.SystemDriveIcon = textBox7.Text;

            TM.Icons.Computer_HideInDesktop = !checkBox1.Checked;
            TM.Icons.User_HideInDesktop = !checkBox2.Checked;
            TM.Icons.RecycleBin_HideInDesktop = !checkBox3.Checked;
            TM.Icons.Network_HideInDesktop = !checkBox5.Checked;
            TM.Icons.ControlPanel_HideInDesktop = !checkBox4.Checked;

            TM.Icons.Shell32Wrapper.Clear();
            TM.Icons.ControlPanelWrapper.Clear();
            TM.Icons.ExplorerWrapper.Clear();

            foreach (DataGridViewRow item in shell32Data.Rows)
            {
                if (item is DataGridViewRow row && row.Cells[3].Value is not null && !string.IsNullOrWhiteSpace(row.Cells[3].Value.ToString()))
                {
                    TM.Icons.Shell32Wrapper.Add(row.Cells[0].Value.ToString(), row.Cells[3].Value.ToString());
                }
            }

            foreach (DataGridViewRow item in cpData.Rows)
            {
                if (item is DataGridViewRow row && row.Cells[4].Value is not null && !string.IsNullOrWhiteSpace(row.Cells[4].Value.ToString()))
                {
                    TM.Icons.ControlPanelWrapper.Add(row.Cells[1].Value.ToString(), row.Cells[4].Value.ToString());
                }
            }

            foreach (DataGridViewRow item in explorerData.Rows)
            {
                if (item is DataGridViewRow row && row.Cells[4].Value is not null && !string.IsNullOrWhiteSpace(row.Cells[4].Value.ToString()))
                {
                    TM.Icons.ExplorerWrapper.Add(row.Cells[1].Value.ToString(), row.Cells[4].Value.ToString());
                }
            }
        }

        private void shell32Data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                string path = (shell32Data.Rows[e.RowIndex].Cells[3].Value ?? string.Empty).ToString();
                string filename;
                int index = 0;

                if (path != null && !path.Contains(",") && System.IO.File.Exists(path) && System.IO.Path.GetExtension(path).ToLower() == ".ico")
                {
                    filename = path;
                }
                else if (path != null && path.Contains(",") && System.IO.File.Exists(path.Split(',')[0]))
                {
                    filename = path.Split(',')[0];
                    int.TryParse(path.Split(',')[1], out index);
                }
                else
                {
                    filename = $"{SysPaths.System32}\\shell32.dll";
                }

                string result = IconPicker.ShowIconPicker(shell32Data.Handle, filename, index);
                if (result != null) { shell32Data.Rows[e.RowIndex].Cells[3].Value = Environment.ExpandEnvironmentVariables(result); }
            }
        }

        private void shell32Data_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                if (shell32Data.Rows.Count == 0) return;

                string data = (shell32Data.Rows[e.RowIndex].Cells[3].Value ?? string.Empty).ToString();

                string path;
                int index;

                if (data.Contains(","))
                {
                    string[] parts = data.Split(',');
                    path = parts[0];
                    int.TryParse(parts[1], out index);
                }
                else
                {
                    path = data;
                    index = 0;
                }

                path = Environment.ExpandEnvironmentVariables(path);

                if (System.IO.File.Exists(path) && System.IO.Path.GetExtension(path).ToLower() != ".ico")
                {
                    shell32Data.Rows[e.RowIndex].Cells[2].Value = PE.GetIcon(path, index);
                }
                else if (System.IO.File.Exists(path) && System.IO.Path.GetExtension(path).ToLower() == ".ico")
                {
                    shell32Data.Rows[e.RowIndex].Cells[2].Value = new Icon(path);
                }
                else
                {
                    shell32Data.Rows[e.RowIndex].Cells[2].Value = null;
                }

                if (e.RowIndex == 7) textBox8.Text = data;
                if (e.RowIndex == 8) textBox9.Text = data;
            }
        }

        private void cpData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                string path = (cpData.Rows[e.RowIndex].Cells[3].Value ?? string.Empty).ToString();
                string filename;
                int index = 0;

                if (path != null && !path.Contains(",") && System.IO.File.Exists(path) && System.IO.Path.GetExtension(path).ToLower() == ".ico")
                {
                    filename = path;
                }
                else if (path != null && path.Contains(",") && System.IO.File.Exists(path.Split(',')[0]))
                {
                    filename = path.Split(',')[0];
                    int.TryParse(path.Split(',')[1], out index);
                }
                else
                {
                    filename = $"{SysPaths.System32}\\shell32.dll";
                }

                string result = IconPicker.ShowIconPicker(cpData.Handle, filename, index);
                if (result != null) { cpData.Rows[e.RowIndex].Cells[4].Value = Environment.ExpandEnvironmentVariables(result); }
            }
        }

        private void cpData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                if (cpData.Rows.Count == 0) return;

                string data = (cpData.Rows[e.RowIndex].Cells[4].Value ?? string.Empty).ToString();

                string path;
                int index;

                if (data.Contains(","))
                {
                    string[] parts = data.Split(',');
                    path = parts[0];
                    int.TryParse(parts[1], out index);
                }
                else
                {
                    path = data;
                    index = 0;
                }

                path = Environment.ExpandEnvironmentVariables(path);

                if (System.IO.File.Exists(path) && System.IO.Path.GetExtension(path).ToLower() != ".ico")
                {
                    cpData.Rows[e.RowIndex].Cells[3].Value = PE.GetIcon(path, index);
                }
                else if (System.IO.File.Exists(path) && System.IO.Path.GetExtension(path).ToLower() == ".ico")
                {
                    cpData.Rows[e.RowIndex].Cells[3].Value = new Icon(path);
                }
                else
                {
                    cpData.Rows[e.RowIndex].Cells[3].Value = null;
                }
            }
        }

        private void explorerData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                string path = (explorerData.Rows[e.RowIndex].Cells[3].Value ?? string.Empty).ToString();
                string filename;
                int index = 0;

                if (path != null && !path.Contains(",") && System.IO.File.Exists(path) && System.IO.Path.GetExtension(path).ToLower() == ".ico")
                {
                    filename = path;
                }
                else if (path != null && path.Contains(",") && System.IO.File.Exists(path.Split(',')[0]))
                {
                    filename = path.Split(',')[0];
                    int.TryParse(path.Split(',')[1], out index);
                }
                else
                {
                    filename = SysPaths.imageres;
                }

                string result = IconPicker.ShowIconPicker(explorerData.Handle, filename, index);
                if (result != null) { explorerData.Rows[e.RowIndex].Cells[4].Value = Environment.ExpandEnvironmentVariables(result); }
            }
        }

        private void explorerData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                if (explorerData.Rows.Count == 0) return;

                string data = (explorerData.Rows[e.RowIndex].Cells[4].Value ?? string.Empty).ToString();

                string path;
                int index;

                if (data.Contains(","))
                {
                    string[] parts = data.Split(',');
                    path = parts[0];
                    int.TryParse(parts[1], out index);
                }
                else
                {
                    path = data;
                    index = 0;
                }

                path = Environment.ExpandEnvironmentVariables(path);

                if (System.IO.File.Exists(path) && System.IO.Path.GetExtension(path).ToLower() != ".ico")
                {
                    explorerData.Rows[e.RowIndex].Cells[3].Value = PE.GetIcon(path, index);
                }
                else if (System.IO.File.Exists(path) && System.IO.Path.GetExtension(path).ToLower() == ".ico")
                {
                    explorerData.Rows[e.RowIndex].Cells[3].Value = new Icon(path);
                }
                else
                {
                    explorerData.Rows[e.RowIndex].Cells[3].Value = null;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MsgBox(Program.Lang.IconsImport_Shell32_1, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, Program.Lang.IconsImport_Shell32_2) == DialogResult.Yes)
            {
                using (OpenFileDialog dlg = new() { Filter = Program.Filters.DLL, Title = Program.Lang.Filter_OpenDLL })
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        Cursor = Cursors.WaitCursor;

                        int count = PE.GetIconGroupCount(dlg.FileName);

                        if (count == 0)
                        {
                            try
                            {
                                Ressy.PortableExecutable PE = new(dlg.FileName);
                                count = PE.GetResourceIdentifiers().Where(x => x.Type.Code == Ressy.ResourceType.IconGroup.Code).Count();
                            }
                            catch { }
                        }

                        shell32Data.Visible = false;
                        for (int r = 0; r < shell32Data.RowCount; r++)
                        {
                            if (r < count) shell32Data[3, r].Value = $"{dlg.FileName},{r}"; else shell32Data[3, r].Value = null;
                        }
                        shell32Data.Visible = true;

                        Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            shell32Data.Visible = false;
            for (int r = 0; r < shell32Data.RowCount; r++)
            {
                shell32Data[3, r].Value = null;
            }
            shell32Data.Visible = true;

            Cursor = Cursors.Default;
        }

        void PickIconForDesktopIcons(UI.WP.TextBox textBox)
        {
            string path = textBox.Text;
            string filename;
            int index = 0;

            if (path != null && !path.Contains(",") && System.IO.File.Exists(path) && System.IO.Path.GetExtension(path).ToLower() == ".ico")
            {
                filename = path;
            }
            else if (path != null && path.Contains(",") && System.IO.File.Exists(path.Split(',')[0]))
            {
                filename = path.Split(',')[0];
                int.TryParse(path.Split(',')[1], out index);
            }
            else
            {
                filename = SysPaths.imageres;
            }

            string result = IconPicker.ShowIconPicker(shell32Data.Handle, filename, index);
            if (result != null) { textBox.Text = Environment.ExpandEnvironmentVariables(result); }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            PickIconForDesktopIcons(textBox3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PickIconForDesktopIcons(textBox1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            PickIconForDesktopIcons(textBox2);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            PickIconForDesktopIcons(textBox6);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            PickIconForDesktopIcons(textBox5);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            PickIconForDesktopIcons(textBox4);
        }

        void UpdateIconPreview(string data, WinIcon iconControl)
        {
            string path;
            int index;

            if (data.Contains(","))
            {
                string[] parts = data.Split(',');
                path = parts[0];
                int.TryParse(parts[1], out index);
            }
            else
            {
                path = data;
                index = 0;
            }

            path = Environment.ExpandEnvironmentVariables(path);

            if (System.IO.File.Exists(path) && System.IO.Path.GetExtension(path).ToLower() != ".ico")
            {
                iconControl.Icon = PE.GetIcon(path, index);
            }
            else if (System.IO.File.Exists(path) && System.IO.Path.GetExtension(path).ToLower() == ".ico")
            {
                iconControl.Icon = new Icon(path);
            }
            else
            {
                iconControl.Icon = null;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            UpdateIconPreview((sender as UI.WP.TextBox).Text, winIcon1);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            UpdateIconPreview((sender as UI.WP.TextBox).Text, winIcon2);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            UpdateIconPreview((sender as UI.WP.TextBox).Text, winIcon3);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            UpdateIconPreview((sender as UI.WP.TextBox).Text, winIcon6);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            UpdateIconPreview((sender as UI.WP.TextBox).Text, winIcon4);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            UpdateIconPreview((sender as UI.WP.TextBox).Text, winIcon5);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Theme.Default.Get(Program.WindowStyle)) { textBox3.Text = TMx.Icons.Computer; }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Theme.Default.Get(Program.WindowStyle)) { textBox1.Text = TMx.Icons.User; }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Theme.Default.Get(Program.WindowStyle)) { textBox2.Text = TMx.Icons.RecycleBinEmpty; }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Theme.Default.Get(Program.WindowStyle)) { textBox6.Text = TMx.Icons.RecycleBinFull; }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Theme.Default.Get(Program.WindowStyle)) { textBox5.Text = TMx.Icons.Network; }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Theme.Default.Get(Program.WindowStyle)) { textBox4.Text = TMx.Icons.ControlPanel; }
        }

        private void winIcon3_Click(object sender, EventArgs e)
        {
            winIcon6.Visible = !winIcon6.Visible;
            winIcon3.Visible = !winIcon3.Visible;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            explorerData.Visible = false;

            using (Theme.Manager TMx = Theme.Default.Get(Program.WindowStyle))
            {
                for (int r = 0; r < explorerData.RowCount; r++)
                {
                    string targetValue = null;
                    targetValue = TMx.Icons.ExplorerWrapper.FirstOrDefault(x => x.Key.ToLower() == explorerData[1, r].Value.ToString().ToLower()).Value;
                    explorerData[4, r].Value = targetValue;
                }
            }

            explorerData.Visible = true;

            Cursor = Cursors.Default;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            cpData.Visible = false;
            for (int r = 0; r < cpData.RowCount; r++)
            {
                cpData[4, r].Value = null;
            }
            cpData.Visible = true;

            Cursor = Cursors.Default;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            shell32Data[3, 29].Value = $"{SysPaths.System32}\\shell32.dll,49";
        }

        private void button20_Click(object sender, EventArgs e)
        {
            shell32Data[3, 28].Value = $"{SysPaths.System32}\\shell32.dll,49";
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            string path;
            int index;
            string data = (sender as UI.WP.TextBox).Text;

            if (data.Contains(","))
            {
                string[] parts = data.Split(',');
                path = parts[0];
                int.TryParse(parts[1], out index);
            }
            else
            {
                path = data;
                index = 0;
            }

            path = Environment.ExpandEnvironmentVariables(path);

            if (System.IO.File.Exists(path) && System.IO.Path.GetExtension(path).ToLower() != ".ico")
            {
                using (Icon ico = PE.GetIcon(path, index))
                {
                    pictureBox2.Image = ico.ToBitmap();
                }
            }
            else if (System.IO.File.Exists(path) && System.IO.Path.GetExtension(path).ToLower() == ".ico")
            {
                using (Icon ico = new(path))
                {
                    pictureBox2.Image = ico.ToBitmap();
                }
            }
            else
            {
                pictureBox2.Image = null;
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox7.Text = string.Empty;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            PickIconForDesktopIcons(textBox7);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            string path;
            int index;
            string data = (sender as UI.WP.TextBox).Text;

            if (data.Contains(","))
            {
                string[] parts = data.Split(',');
                path = parts[0];
                int.TryParse(parts[1], out index);
            }
            else
            {
                path = data;
                index = 0;
            }

            path = Environment.ExpandEnvironmentVariables(path);

            if (System.IO.File.Exists(path) && System.IO.Path.GetExtension(path).ToLower() != ".ico")
            {
                using (Icon ico = PE.GetIcon(path, index))
                {
                    pictureBox5.Image = ico.ToBitmap();
                }
            }
            else if (System.IO.File.Exists(path) && System.IO.Path.GetExtension(path).ToLower() == ".ico")
            {
                using (Icon ico = new(path))
                {
                    pictureBox5.Image = ico.ToBitmap();
                }
            }
            else
            {
                pictureBox5.Image = null;
            }

            if (data != shell32Data[3, 8].Value?.ToString()) shell32Data[3, 8].Value = data;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            string path;
            int index;
            string data = (sender as UI.WP.TextBox).Text;

            if (data.Contains(","))
            {
                string[] parts = data.Split(',');
                path = parts[0];
                int.TryParse(parts[1], out index);
            }
            else
            {
                path = data;
                index = 0;
            }

            path = Environment.ExpandEnvironmentVariables(path);

            if (System.IO.File.Exists(path) && System.IO.Path.GetExtension(path).ToLower() != ".ico")
            {
                using (Icon ico = PE.GetIcon(path, index))
                {
                    pictureBox8.Image = ico.ToBitmap();
                }
            }
            else if (System.IO.File.Exists(path) && System.IO.Path.GetExtension(path).ToLower() == ".ico")
            {
                using (Icon ico = new(path))
                {
                    pictureBox8.Image = ico.ToBitmap();
                }
            }
            else
            {
                pictureBox8.Image = null;
            }

            if (data != shell32Data[3, 7].Value?.ToString()) shell32Data[3, 7].Value = data;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            textBox9.Text = string.Empty;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            textBox8.Text = string.Empty;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            PickIconForDesktopIcons(textBox9);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            PickIconForDesktopIcons(textBox8);
        }
    }
}