using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinPaletter.Dialogs
{
    public partial class SystemRestorePoints : WinPaletter.UI.WP.Form
    {
        private static readonly ImageList imageList = new() { ImageSize = new(24, 24), ColorDepth = ColorDepth.Depth32Bit };
        private bool isBusy = false;

        public SystemRestorePoints()
        {
            InitializeComponent();
            imageList.AddWithAlpha("Restorepoint", Properties.Resources.Restorepoint);
            respoints.SmallImageList = imageList;
        }

        private async void SystemRestorePoints_Load(object sender, EventArgs e)
        {
            Label9.Font = Fonts.ConsoleMedium;
            await RefreshListAsync();
        }

        private async Task RefreshListAsync()
        {
            if (isBusy) return;
            SetBusyState(true);

            respoints.BeginUpdate();
            respoints.Clear();

            if (respoints.Columns.Count == 0)
            {
                respoints.Columns.Add(Program.Localization.Strings.General.Name, 500);
                respoints.Columns.Add(Program.Localization.Strings.General.Sequence, 70);
                respoints.Columns.Add(Program.Localization.Strings.General.CreationDateTime, 200);
            }

            List<ListViewItem> items = await Task.Run(() =>
            {
                List<ListViewItem> localItems = [];

                foreach (RestorePointInfo rp in SystemRestoreHelper.GetWinPaletterRestorePoints())
                {
                    ListViewItem item = new() { Text = rp.Name, ImageKey = "Restorepoint", Tag = rp };
                    item.SubItems.Add(rp.SequenceNumber.ToString());
                    item.SubItems.Add(rp.CreationTime.ToString());
                    localItems.Add(item);
                }
                return localItems;
            });

            respoints.Items.AddRange([.. items]);
            respoints.EndUpdate();

            Label9.Text = (SystemRestoreHelper.GetSystemVolumeShadowStorageUsedBytes() ?? 0).ToStringFileSize();
            SetBusyState(false);
        }

        private void SetBusyState(bool busy)
        {
            isBusy = busy;
            Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
            button1.Enabled = !busy && respoints.SelectedItems.Count > 0;
            button2.Enabled = !busy && respoints.Items.Count > 0;
            Button4.Enabled = !busy;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (isBusy || respoints.SelectedItems.Count == 0) return;

            RestorePointInfo rp = respoints.SelectedItems[0].Tag as RestorePointInfo;

            if (MsgBox(string.Format(Program.Localization.Strings.Messages.SysRestore_Delete_Msg0, rp.Name), MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                string.Format(Program.Localization.Strings.Messages.SysRestore_Delete_Msg1, Program.SystemPartition)) == DialogResult.Yes)
            {
                SetBusyState(true);
                bool success = await Task.Run(() => SystemRestoreHelper.DeleteRestorePoint(rp.SequenceNumber));

                if (!success)
                {
                    if (MsgBox(string.Format(Program.Localization.Strings.Messages.SysRestore_Delete_Error0, rp.Name), MessageBoxButtons.YesNo, MessageBoxIcon.Error, Program.Localization.Strings.Messages.SysRestore_Delete_Error1) == DialogResult.Yes)
                        StartRstrui();
                }
                await RefreshListAsync();
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (isBusy || respoints.Items.Count == 0) return;

            if (MsgBox(Program.Localization.Strings.Messages.SysRestore_DeleteAll_Msg0, MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                string.Format(Program.Localization.Strings.Messages.SysRestore_DeleteAll_Msg1, (SystemRestoreHelper.GetSystemVolumeShadowStorageUsedBytes() ?? 0).ToStringFileSize(), Program.SystemPartition)) == DialogResult.Yes)
            {
                SetBusyState(true);
                int failed = await Task.Run(() =>
                {
                    int f = 0;
                    foreach (ListViewItem item in respoints.Items) if (item.Tag is RestorePointInfo rp && !SystemRestoreHelper.DeleteRestorePoint(rp.SequenceNumber)) f++;
                    return f;
                });

                if (failed > 0 && MsgBox(Program.Localization.Strings.Messages.SysRestore_DeleteAll_Error0, MessageBoxButtons.YesNo, MessageBoxIcon.Error, Program.Localization.Strings.Messages.SysRestore_Delete_Error1) == DialogResult.Yes)
                    StartRstrui();

                await RefreshListAsync();
            }
        }

        private void StartRstrui()
        {
            string path = $"{SysPaths.System32}\\restore\\rstrui.exe";
            if (!System.IO.File.Exists(path)) path = $"{SysPaths.System32}\\rstrui.exe";

            if (System.IO.File.Exists(path)) Process.Start(path);
            else Process.Start("control", "sysdm.cpl,,4");
        }

        private async void Button4_Click(object sender, EventArgs e)
        {
            string name = InputBox(Program.Localization.Strings.Messages.SysRestore_EnterName);
            if (!string.IsNullOrWhiteSpace(name) && SystemRestoreHelper.CreateRestorePoint(name)) await RefreshListAsync();
        }

        private void respoints_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (respoints.SelectedItems is not null) StartRstrui();
        }

        private void respoints_SelectedIndexChanged(object sender, EventArgs e) => button1.Enabled = !isBusy && respoints.SelectedItems.Count > 0;

        private void pin_button_Click(object sender, EventArgs e) => Forms.MainForm.AddTab(this);

        private void Button7_Click(object sender, EventArgs e) => Close();

        private void button3_Click(object sender, EventArgs e) => StartRstrui();
   }
}