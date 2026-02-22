using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.Assets;
using WinPaletter.Theme;
using WinPaletter.UI.Controllers;

namespace WinPaletter
{
    public partial class LogonUIXP
    {
        public LogonUIXP()
        {
            InitializeComponent();
            color_pick.DragDrop += ColorItem_DragDrop;
            FormClosing += LogonUIXP_FormClosing;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            RadioImage2.Image?.Dispose();
        }


        private void LoadFromWPTH(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, Title = Program.Localization.Strings.Extensions.OpenWinPaletterTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Manager TMx = new(Manager.Source.File, dlg.FileName))
                    {
                        LoadFromTM(TMx);
                    }
                }
            }
        }

        private void LoadFromCurrent(object sender, EventArgs e)
        {
            using (Manager TMx = new(Manager.Source.Registry))
            {
                LoadFromTM(TMx);
            }
        }

        private void LoadFromDefault(object sender, EventArgs e)
        {
            using (Manager TMx = Default.FromOS(Program.WindowStyle))
            {
                LoadFromTM(TMx);
            }
        }

        private void LoadIntoCurrentTheme(object sender, EventArgs e)
        {
            ApplyToTM(Program.TM);
            Close();
        }

        private void Apply(object sender, EventArgs e)
        {
            if (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.LogonUI)
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
            Program.TM.LogonUIXP.Apply();

            Cursor = Cursors.Default;
        }

        private void LogonUIXP_FormClosing(object sender, FormClosingEventArgs e)
        {
            color_pick.DragDrop -= ColorItem_DragDrop;
        }

        private void LogonUIXP_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = Program.Localization.Strings.Aspects.LogonUI,
                Enabled = Program.TM.LogonUIXP.Enabled,
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

            LoadFromTM(Program.TM);
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (e.Data.GetData(typeof(ColorItem).FullName) is ColorItem)
            {
                Focus();
                BringToFront();
            }
            else
            {
                return;
            }

            base.OnDragOver(e);
        }

        public void LoadFromTM(Manager TM)
        {
            AspectEnabled = TM.LogonUIXP.Enabled;
            switch (TM.LogonUIXP.Mode)
            {
                case Theme.Structures.LogonUIXP.Modes.Default:
                    {
                        RadioImage1.Checked = true;
                        break;
                    }
                case Theme.Structures.LogonUIXP.Modes.Win2000:
                    {
                        RadioImage2.Checked = true;
                        break;
                    }

                default:
                    {
                        RadioImage1.Checked = true;
                        break;
                    }
            }

            color_pick.BackColor = TM.LogonUIXP.BackColor;
            CheckBox1.Checked = TM.LogonUIXP.ShowMoreOptions;
            UpdateWin2000Preview(TM.LogonUIXP.BackColor);
        }

        public void ApplyToTM(Manager TM)
        {
            TM.LogonUIXP.Enabled = AspectEnabled;

            if (RadioImage1.Checked)
                TM.LogonUIXP.Mode = Theme.Structures.LogonUIXP.Modes.Default;
            else
                TM.LogonUIXP.Mode = Theme.Structures.LogonUIXP.Modes.Win2000;

            TM.LogonUIXP.BackColor = color_pick.BackColor;
            TM.LogonUIXP.ShowMoreOptions = CheckBox1.Checked;
        }

        /// <summary>
        /// Updates the Windows 2000 preview image with the specified background color.
        /// </summary>
        /// <remarks>This method clears the current preview image, applies the specified background color,
        /// and redraws the Windows 2000 UI elements onto the preview. The updated image is then  assigned to the
        /// preview display.</remarks>
        /// <param name="color">The <see cref="Color"/> to use as the background for the preview image.</param>
        public void UpdateWin2000Preview(Color color)
        {
            using (Bitmap b = new(LogonUIRes.Win2000.Width, LogonUIRes.Win2000.Height))
            {
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.Clear(color);
                    g.DrawImage(LogonUIRes.Win2000, 0, 0);
                    g.Save();
                }
                RadioImage2.Image = (Bitmap)b.Clone();
            }
        }

        private void color_pick_Click(object sender, EventArgs e)
        {
            if (e is DragEventArgs)
                return;

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } }
            };

            Color C = Forms.ColorPickerDlg.Pick(CList);
            ((ColorItem)sender).BackColor = Color.FromArgb(255, C);
            UpdateWin2000Preview(C);
            CList.Clear();
        }

        private void ColorItem_DragDrop(object sender, DragEventArgs e)
        {
            UpdateWin2000Preview(color_pick.BackColor);
        }

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Links.Wiki.LogonUI_XP);
        }

        private void RadioImage2_CheckedChanged(object sender, EventArgs e)
        {
            GroupBox1.Enabled = RadioImage2.Checked;
        }

        private void RadioImage1_CheckedChanged(object sender, EventArgs e)
        {
            GroupBox1.Enabled = !RadioImage1.Checked;
        }

        private void color_pick_ContextMenuMadeColorChangeInvoker(object sender, ColorItem.ContextMenuMadeColorChangeEventArgs e)
        {
            UpdateWin2000Preview(e.ColorItem.BackColor);
        }
    }
}