using Ressy;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.Theme;
using WinPaletter.UI.Simulation;
using static WinPaletter.Theme.Manager;

namespace WinPaletter
{
    public partial class IconsStudio : AspectsTemplate
    {
        private static readonly Dictionary<int, Bitmap> _shell32IconCache = [];
        private static readonly Dictionary<string, Bitmap> _controlPanelIconCache = new(StringComparer.OrdinalIgnoreCase);
        private static readonly Dictionary<string, Bitmap> _explorerIconCache = new(StringComparer.OrdinalIgnoreCase);

        public IconsStudio()
        {
            InitializeComponent();
        }

        private void LoadFromWPTH(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, Title = Program.Localization.Strings.Extensions.OpenWinPaletterTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Manager TMx = new(Source.File, dlg.FileName))
                    {
                        LoadFromTM(TMx);
                    }
                }
            }
        }

        private void LoadFromCurrent(object sender, EventArgs e)
        {
            Manager TMx = new(Source.Registry);
            LoadFromTM(TMx);
            TMx.Dispose();
        }

        private void LoadFromDefault(object sender, EventArgs e)
        {
            Manager TMx = Default.FromOS(Program.WindowStyle);
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
            if (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.Icons)
            {
                MsgBox(Program.Localization.Strings.Aspects.Disabled_Apply_0, MessageBoxButtons.OK, MessageBoxIcon.Warning, Program.Localization.Strings.Aspects.Disabled_Apply_1);
                return;
            }

            Cursor = Cursors.WaitCursor;

            if (Program.Settings.BackupTheme.Enabled && Program.Settings.BackupTheme.AutoBackupOnApplySingleAspect)
            {
                using (Manager TMx = new(Manager.Source.Registry))
                {
                    string filename = Program.GetUniqueFileName($"{Program.Settings.BackupTheme.BackupPath}\\OnAspectApply", $"{TMx.Info.ThemeName}_{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.wpth");
                    TMx.Save(Manager.Source.File, filename);
                }
            }

            ApplyToTM(Program.TM);
            ApplyToTM(Program.TM_Original);
            Program.TM.Icons.Apply();

            Cursor = Cursors.Default;
        }

        private void IconsStudio_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = Program.Localization.Strings.Aspects.Icons,
                Enabled = Program.TM.Icons.Enabled,
                Import_theme = false,
                Import_msstyles = false,
                GeneratePalette = false,
                GenerateMSTheme = false,
                Import_preset = false,
                CanSwitchMode = false,
                CanOpenColorsEffects = false,

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

            //pnl_preview.BackgroundImage = Program.TM.Wallpaper.Enabled ? Program.WallpaperMonitor.FetchSuitableWallpaper(Program.TM, Program.WindowStyle) : Program.ThumbnailWallpaper;

            foreach (WinIcon winIcon in pnl_preview.GetAllControls().OfType<WinIcon>())
            {
                winIcon.IconSize = Program.TM.MetricsFonts.DesktopIconSize;
            }

            winIcon2.Text = User.Name;
            winIcon1.Text = OS.W8x || OS.W10 || OS.W11 || OS.W12 ? "This PC" : "Computer";
            label3.Text = winIcon1.Text;

            if (!OS.WXP)
            {
                using (Icon sysdrv = PE.GetIcon(SysPaths.imageres, -36))
                {
                    if (sysdrv != null) pictureBox1.Image = sysdrv.ToBitmap();
                }
            }

            using (Icon drv = PE.GetIcon($"{SysPaths.System32}\\shell32.dll", -9))
            {
                if (drv != null)
                {
                    pictureBox6.Image = drv.ToBitmap();
                    if (OS.WXP) pictureBox1.Image = drv.ToBitmap();
                }
            }

            using (Icon drv = PE.GetIcon($"{SysPaths.System32}\\shell32.dll", -8))
            {
                if (drv != null) pictureBox9.Image = drv.ToBitmap();
            }

            LoadFromTM(Program.TM);
        }

        // Reusable helper to dispose any previous icon safely
        private static void DisposeIcon(WinIcon iconControl)
        {
            if (iconControl != null && iconControl.Icon != null)
            {
                try
                {
                    iconControl.Icon.Dispose();
                }
                catch
                {
                    // ignore if already disposed
                }
                finally
                {
                    iconControl.Icon = null;
                }
            }
        }

        // Helper to safely dispose the previous image in a PictureBox
        private static void DisposeImage(PictureBox box)
        {
            if (box != null && box.Image != null)
            {
                try
                {
                    box.Image.Dispose();
                }
                catch
                {
                    // Ignore if already disposed
                }
                finally
                {
                    box.Image = null;
                }
            }
        }

        private void IconsStudio_FormClosing(object sender, FormClosingEventArgs e)
        {
            CleanUp();
        }

        void PopulateShell32Icons()
        {
            shell32Data.SuspendLayout();
            shell32Data.Rows.Clear();

            Cursor = Cursors.WaitCursor;

            try
            {
                string shell32 = System.IO.Path.Combine(SysPaths.System32, "shell32.dll");
                int count = PE.GetIconGroupCount(shell32);

                // Reuse list with preallocated capacity to reduce reallocations
                List<DataGridViewRow> rowsToAdd = new(count);

                // Batch load icons with caching
                for (int i = 0; i < count; i++)
                {
                    if (!_shell32IconCache.TryGetValue(i, out Bitmap iconBmp))
                    {
                        using Icon ico = PE.GetIcon(shell32, i);
                        iconBmp = ico?.ToBitmap();
                        if (iconBmp != null) _shell32IconCache[i] = iconBmp;
                    }

                    // Create row
                    DataGridViewRow row = new();
                    row.CreateCells(shell32Data, i, iconBmp, null, string.Empty, Program.Localization.Strings.General.Browse);
                    rowsToAdd.Add(row);
                }

                // Add all rows in one go
                shell32Data.Rows.AddRange([.. rowsToAdd]);

                // Ensure no default [X] icon shows up
                foreach (DataGridViewColumn column in shell32Data.Columns)
                {
                    if (column is DataGridViewImageColumn imgCol) imgCol.DefaultCellStyle.NullValue = null;
                }
            }
            catch (Exception ex)
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Failed to load shell32 icons: {ex.Message}");
            }
            finally
            {
                shell32Data.ResumeLayout();
                Cursor = Cursors.Default;
            }
        }

        void PopulateControlPanelIcons()
        {
            cpData.SuspendLayout();
            cpData.Visible = false;
            cpData.Rows.Clear();

            Cursor = Cursors.WaitCursor;

            try
            {
                List<Tuple<string, string, string>> clsidList = Theme.Structures.Icons.ControlPanelCLSIDs;
                int count = clsidList.Count;
                List<DataGridViewRow> rowsToAdd = new(count);

                for (int i = 0; i < count; i++)
                {
                    var (clsid, name, defaultIconPath) = clsidList[i];

                    Bitmap iconBmp = null;

                    if (!string.IsNullOrWhiteSpace(defaultIconPath))
                    {
                        string resolvedPath = Environment.ExpandEnvironmentVariables(defaultIconPath);

                        // Cache by path
                        if (_controlPanelIconCache.TryGetValue(resolvedPath, out var cached))
                        {
                            iconBmp = cached;
                        }
                        else
                        {
                            iconBmp = TryExtractBitmap(resolvedPath);
                            if (iconBmp != null) _controlPanelIconCache[resolvedPath] = iconBmp;
                        }
                    }

                    DataGridViewRow row = new();
                    row.CreateCells(cpData, name, clsid, iconBmp, null, string.Empty, Program.Localization.Strings.General.Browse);
                    rowsToAdd.Add(row);
                }

                cpData.Rows.AddRange(rowsToAdd.ToArray());

                foreach (DataGridViewColumn column in cpData.Columns)
                {
                    if (column is DataGridViewImageColumn imgCol) imgCol.DefaultCellStyle.NullValue = null;
                }
            }
            catch (Exception ex)
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Failed to load Control Panel icons: {ex.Message}");
            }
            finally
            {
                cpData.ResumeLayout();
                cpData.Visible = true;
                Cursor = Cursors.Default;
            }
        }

        void PopulateExplorerIcons()
        {
            explorerData.SuspendLayout();
            explorerData.Visible = false;
            explorerData.Rows.Clear();

            Cursor = Cursors.WaitCursor;

            try
            {
                List<Tuple<string, string, string>> clsidList = Theme.Structures.Icons.ExplorerCLSIDs;
                int count = clsidList.Count;

                List<DataGridViewRow> rowsToAdd = new(count);

                for (int i = 0; i < count; i++)
                {
                    var (clsid, name, defaultIconPath) = clsidList[i];

                    Bitmap iconBmp = null;

                    if (!string.IsNullOrWhiteSpace(defaultIconPath))
                    {
                        string resolvedPath = Environment.ExpandEnvironmentVariables(defaultIconPath);

                        if (_explorerIconCache.TryGetValue(resolvedPath, out var cached))
                        {
                            iconBmp = cached;
                        }
                        else
                        {
                            iconBmp = TryExtractBitmap(resolvedPath);
                            if (iconBmp != null) _explorerIconCache[resolvedPath] = iconBmp;
                        }
                    }

                    DataGridViewRow row = new();
                    row.CreateCells(explorerData, name, clsid, iconBmp, null, string.Empty, Program.Localization.Strings.General.Browse);
                    rowsToAdd.Add(row);
                }

                explorerData.Rows.AddRange(rowsToAdd.ToArray());

                foreach (DataGridViewColumn column in explorerData.Columns)
                {
                    if (column is DataGridViewImageColumn imgCol) imgCol.DefaultCellStyle.NullValue = null;
                }
            }
            catch (Exception ex)
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"ailed to load Explorer icons: {ex.Message}");
            }
            finally
            {
                explorerData.ResumeLayout();
                explorerData.Visible = true;
                Cursor = Cursors.Default;
            }
        }

        private static Bitmap TryExtractBitmap(string iconPath)
        {
            try
            {
                string[] parts = iconPath.Split(',');
                string path = parts[0].Trim('"', ' ');
                int index = (parts.Length > 1 && int.TryParse(parts[1], out int parsed)) ? parsed : 0;

                if (!File.Exists(path)) return null;

                if (System.IO.Path.GetExtension(path).Equals(".ico", StringComparison.OrdinalIgnoreCase))
                {
                    using Icon ico = new(path);
                    return ico.ToBitmap();
                }

                using Icon extracted = PE.GetIcon(path, index);
                return extracted?.ToBitmap();
            }
            catch
            {
                return null;
            }
        }

        void CleanUp()
        {
            static void DisposeIconsAndClear(DataGridView dgv, params int[] iconColumns)
            {
                if (dgv == null || dgv.IsDisposed) return;

                // Marshal back to UI thread if necessary
                if (dgv.InvokeRequired)
                {
                    dgv.Invoke(new Action(() => DisposeIconsAndClear(dgv, iconColumns)));
                    return;
                }

                try
                {
                    dgv.SuspendLayout();

                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        foreach (int colIndex in iconColumns)
                        {
                            if (colIndex < dgv.ColumnCount && row.Cells[colIndex].Value is Icon icon)
                            {
                                icon.Dispose();
                                row.Cells[colIndex].Value = null;
                            }
                        }
                    }

                    dgv.Rows.Clear();
                }
                catch (ObjectDisposedException)
                {
                    // Ignore if control is being disposed during cleanup
                }
                finally
                {
                    if (!dgv.IsDisposed) dgv.ResumeLayout();
                }
            }

            DisposeIconsAndClear(explorerData, 2, 3);
            DisposeIconsAndClear(cpData, 2, 3);
            DisposeIconsAndClear(shell32Data, 1, 2);

            ClearCache(_shell32IconCache);
            ClearCache(_controlPanelIconCache);
            ClearCache(_explorerIconCache);
        }

        private static void ClearCache(Dictionary<int, Bitmap> cache)
        {
            foreach (var kv in cache) kv.Value?.Dispose();
            cache.Clear();
        }

        private static void ClearCache(Dictionary<string, Bitmap> cache)
        {
            foreach (var kv in cache) kv.Value?.Dispose();
            cache.Clear();
        }

        public void LoadFromTM(Manager TM)
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

        public void ApplyToTM(Manager TM)
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

                if (path != null && !path.Contains(",") && File.Exists(path) && System.IO.Path.GetExtension(path).ToLower() == ".ico")
                {
                    filename = path;
                }
                else if (path != null && path.Contains(",") && File.Exists(path.Split(',')[0]))
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
            if (e.ColumnIndex != 3 || e.RowIndex < 0 || shell32Data.Rows.Count == 0) return;

            DataGridViewRow row = shell32Data.Rows[e.RowIndex];
            string rawValue = (row.Cells[3].Value ?? string.Empty).ToString().Trim();

            if (string.IsNullOrEmpty(rawValue))
            {
                DisposeIconAtCell(row, 2);
                row.Cells[2].Value = null;
                return;
            }

            string path;
            int index = 0;

            int commaIndex = rawValue.IndexOf(',');
            if (commaIndex > 0)
            {
                path = rawValue.Substring(0, commaIndex);
                if (commaIndex + 1 < rawValue.Length) int.TryParse(rawValue.Substring(commaIndex + 1), out index);
            }
            else
            {
                path = rawValue;
            }

            path = Environment.ExpandEnvironmentVariables(path);
            Icon newIcon = null;

            try
            {
                if (File.Exists(path))
                {
                    string ext = System.IO.Path.GetExtension(path).ToLowerInvariant();
                    newIcon = ext == ".ico" ? new Icon(path) : PE.GetIcon(path, index);
                }
            }
            catch
            {
                newIcon = null; // ignore bad paths or file read errors
            }

            // Dispose old icon to prevent leaks
            DisposeIconAtCell(row, 2);
            row.Cells[2].Value = newIcon;

            if (e.RowIndex == 7) textBox8.Text = rawValue;
            else if (e.RowIndex == 8) textBox9.Text = rawValue;
        }

        private static void DisposeIconAtCell(DataGridViewRow row, int columnIndex)
        {
            if (row != null && row.Cells[columnIndex].Value is Icon oldIcon)
            {
                oldIcon.Dispose();
                row.Cells[columnIndex].Value = null;
            }
        }

        private void cpData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                string path = (cpData.Rows[e.RowIndex].Cells[3].Value ?? string.Empty).ToString();
                string filename;
                int index = 0;

                if (path != null && !path.Contains(",") && File.Exists(path) && System.IO.Path.GetExtension(path).ToLower() == ".ico")
                {
                    filename = path;
                }
                else if (path != null && path.Contains(",") && File.Exists(path.Split(',')[0]))
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

                if (File.Exists(path) && System.IO.Path.GetExtension(path).ToLower() != ".ico")
                {
                    cpData.Rows[e.RowIndex].Cells[3].Value = PE.GetIcon(path, index);
                }
                else if (File.Exists(path) && System.IO.Path.GetExtension(path).ToLower() == ".ico")
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

                if (path != null && !path.Contains(",") && File.Exists(path) && System.IO.Path.GetExtension(path).ToLower() == ".ico")
                {
                    filename = path;
                }
                else if (path != null && path.Contains(",") && File.Exists(path.Split(',')[0]))
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
            if (e.ColumnIndex != 4 || e.RowIndex < 0 || explorerData.Rows.Count == 0) return;

            DataGridViewRow row = explorerData.Rows[e.RowIndex];
            string rawValue = (row.Cells[4].Value ?? string.Empty).ToString().Trim();

            if (string.IsNullOrEmpty(rawValue))
            {
                DisposeIconAtCell(row, 3);
                row.Cells[3].Value = null;
                return;
            }

            string path;
            int index = 0;

            int commaIndex = rawValue.IndexOf(',');
            if (commaIndex > 0)
            {
                path = rawValue.Substring(0, commaIndex);
                if (commaIndex + 1 < rawValue.Length) int.TryParse(rawValue.Substring(commaIndex + 1), out index);
            }
            else
            {
                path = rawValue;
            }

            path = Environment.ExpandEnvironmentVariables(path);
            Icon newIcon = null;

            try
            {
                if (File.Exists(path))
                {
                    string ext = System.IO.Path.GetExtension(path).ToLowerInvariant();
                    newIcon = ext == ".ico" ? new Icon(path) : PE.GetIcon(path, index);
                }
            }
            catch
            {
                newIcon = null; // Ignore invalid paths or load errors
            }

            // Dispose old icon before setting the new one
            DisposeIconAtCell(row, 3);
            row.Cells[3].Value = newIcon;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MsgBox(Program.Localization.Strings.Messages.IconsImport_Shell32_1, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, Program.Localization.Strings.Messages.IconsImport_Shell32_2) == DialogResult.Yes)
            {
                using (OpenFileDialog dlg = new() { Filter = Program.Filters.DLL, Title = Program.Localization.Strings.Extensions.OpenDLL })
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        Cursor = Cursors.WaitCursor;

                        int count = PE.GetIconGroupCount(dlg.FileName);

                        if (count == 0)
                        {
                            try
                            {
                                PortableExecutable PE = new(dlg.FileName);
                                count = PE.GetResourceIdentifiers().Where(x => x.Type.Code == ResourceType.IconGroup.Code).Count();
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

            if (path != null && !path.Contains(",") && File.Exists(path) && System.IO.Path.GetExtension(path).ToLower() == ".ico")
            {
                filename = path;
            }
            else if (path != null && path.Contains(",") && File.Exists(path.Split(',')[0]))
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
            if (iconControl == null) return;

            if (string.IsNullOrWhiteSpace(data))
            {
                DisposeIcon(iconControl);
                iconControl.Icon = null;
                return;
            }

            string path;
            int index = 0;

            int commaIndex = data.IndexOf(',');
            if (commaIndex > 0)
            {
                path = data.Substring(0, commaIndex);
                if (commaIndex + 1 < data.Length)
                    int.TryParse(data.Substring(commaIndex + 1), out index);
            }
            else
            {
                path = data;
            }

            path = Environment.ExpandEnvironmentVariables(path);
            Icon newIcon = null;

            try
            {
                if (File.Exists(path))
                {
                    string ext = System.IO.Path.GetExtension(path).ToLowerInvariant();
                    newIcon = ext == ".ico" ? new Icon(path) : PE.GetIcon(path, index, iconControl.IconSize);
                }
            }
            catch
            {
                newIcon = null; // ignore bad files
            }

            // Safely dispose the old icon before replacing it
            DisposeIcon(iconControl);
            iconControl.Icon = newIcon;

            float scaleX = iconControl.IconSize / 48f; // original icon size = 32
            float scaleY = iconControl.IconSize / 48f;

            int originalWidth = 82;
            int originalHeight = 90;

            float newWidth = originalWidth * scaleX;
            float newHeight = originalHeight * scaleY;

            iconControl.Size = new Size((int)newWidth, (int)newHeight);
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
            using (Manager TMx = Default.FromOS(Program.WindowStyle)) { textBox3.Text = TMx.Icons.Computer; }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (Manager TMx = Default.FromOS(Program.WindowStyle)) { textBox1.Text = TMx.Icons.User; }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (Manager TMx = Default.FromOS(Program.WindowStyle)) { textBox2.Text = TMx.Icons.RecycleBinEmpty; }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            using (Manager TMx = Default.FromOS(Program.WindowStyle)) { textBox6.Text = TMx.Icons.RecycleBinFull; }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            using (Manager TMx = Default.FromOS(Program.WindowStyle)) { textBox5.Text = TMx.Icons.Network; }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            using (Manager TMx = Default.FromOS(Program.WindowStyle)) { textBox4.Text = TMx.Icons.ControlPanel; }
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

            using (Manager TMx = Default.FromOS(Program.WindowStyle))
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

        private void button17_Click(object sender, EventArgs e)
        {
            textBox7.Text = string.Empty;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            PickIconForDesktopIcons(textBox7);
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            HandleTextBoxIconChange(sender as UI.WP.TextBox, pictureBox2, null, -1, -1);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            HandleTextBoxIconChange(sender as UI.WP.TextBox, pictureBox8, shell32Data, 3, 7);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            HandleTextBoxIconChange(sender as UI.WP.TextBox, pictureBox5, shell32Data, 3, 8);
        }

        private void HandleTextBoxIconChange(UI.WP.TextBox textBox, PictureBox targetBox, DataGridView dataGrid, int columnIndex, int rowIndex)
        {
            if (textBox == null || targetBox == null) return;

            string data = (textBox.Text ?? string.Empty).Trim();

            if (string.IsNullOrEmpty(data))
            {
                DisposeImage(targetBox);
                targetBox.Image = null;
                return;
            }

            string path;
            int index = 0;

            int commaIndex = data.IndexOf(',');
            if (commaIndex > 0)
            {
                path = data.Substring(0, commaIndex);
                if (commaIndex + 1 < data.Length) int.TryParse(data.Substring(commaIndex + 1), out index);
            }
            else
            {
                path = data;
            }

            path = Environment.ExpandEnvironmentVariables(path);
            Image newImage = null;

            try
            {
                if (File.Exists(path))
                {
                    string ext = System.IO.Path.GetExtension(path).ToLowerInvariant();
                    using (Icon ico = (ext == ".ico" ? new Icon(path) : PE.GetIcon(path, index)))
                    {
                        newImage = ico.ToBitmap();
                    }
                }
            }
            catch
            {
                newImage = null;
            }

            // Safely replace PictureBox image
            DisposeImage(targetBox);
            targetBox.Image = newImage;

            // Sync the corresponding DataGridView cell if needed
            if (dataGrid != null && rowIndex >= 0 && rowIndex < dataGrid.Rows.Count && columnIndex >= 0 && columnIndex < dataGrid.ColumnCount)
            {
                string current = dataGrid[columnIndex, rowIndex].Value?.ToString();
                if (current != data) dataGrid[columnIndex, rowIndex].Value = data;
            }
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