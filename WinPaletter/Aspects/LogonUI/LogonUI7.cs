using ImageProcessor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.UI.Controllers;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter
{

    public partial class LogonUI7
    {
        public int ID;
        public bool openAsWin81;
        private Theme.Structures.LogonUI7 logonUI;

        public LogonUI7()
        {
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            pnl_preview.BackgroundImage?.Dispose();
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
            using (Theme.Manager TMx = new(Theme.Manager.Source.Registry))
            {
                LoadFromTM(TMx);
            }
        }

        private void LoadFromDefault(object sender, EventArgs e)
        {
            using (Theme.Manager TMx = Theme.Default.Get(Program.WindowStyle))
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
                MsgBox(Program.Lang.AspectDisabled_Apply_0, MessageBoxButtons.OK, MessageBoxIcon.Warning, Program.Lang.AspectDisabled_Apply_1);
                return;
            }

            Cursor = Cursors.WaitCursor;

            using (Theme.Manager TMx = new(Theme.Manager.Source.Registry))
            {
                if (Program.Settings.BackupTheme.Enabled && Program.Settings.BackupTheme.AutoBackupOnApplySingleAspect)
                {
                    string filename = Program.GetUniqueFileName($"{Program.Settings.BackupTheme.BackupPath}\\OnAspectApply", $"{TMx.Info.ThemeName}_{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.wpth");
                    TMx.Save(Theme.Manager.Source.File, filename);
                }

                ApplyToTM(TMx);
                ApplyToTM(Program.TM);
                ApplyToTM(Program.TM_Original);

                if (Program.WindowStyle == WindowStyle.W81)     // Apply LogonUI81
                    TMx.LogonUI81.Apply("8.1", true);
                else                                            // Apply LogonUI7
                    TMx.LogonUI7.Apply("7", false);
            }

            Cursor = Cursors.Default;
        }

        private void LogonUI7_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = Program.WindowStyle == WindowStyle.W81 ? Program.Lang.LockScreen : Program.Lang.LogonUIScreen,
                Enabled = Program.TM.LogonUI7.Enabled,
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

            ID = 0;

            LoadFromTM(Program.TM);
            ApplyPreview();
            using (LogonUI formIcon = new()) { Icon = formIcon.Icon; }

            if (Program.WindowStyle == WindowStyle.W81)
            {
                Button3.Visible = true;
                PictureBox11.Image = Assets.LogonUIRes.Win81;
                PictureBox4.Image = Assets.WinLogos.Win81;
            }
            else if (Program.WindowStyle == WindowStyle.W7)
            {
                Button3.Visible = false;
                PictureBox11.Image = Assets.LogonUIRes.Win7;
                PictureBox4.Image = Assets.WinLogos.Win7;
            }
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (e.Data.GetData(typeof(UI.Controllers.ColorItem).FullName) is UI.Controllers.ColorItem)
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

        public void LoadFromTM(Theme.Manager TM)
        {
            if (Program.WindowStyle == WindowStyle.W81)
            {
                logonUI = Program.TM.LogonUI81;

                pictureBox1.Visible = true;
                checkBox1.Visible = true;
                checkBox1.Checked = logonUI.NoLockScreen;

                AspectEnabled = !logonUI.Enabled;

                switch (logonUI.Mode)
                {
                    case Theme.Structures.LogonUI7.Sources.Default:
                        {
                            RadioButton1.Checked = true;
                            break;
                        }

                    case Theme.Structures.LogonUI7.Sources.Wallpaper:
                        {
                            RadioButton2.Checked = true;
                            break;
                        }

                    case Theme.Structures.LogonUI7.Sources.CustomImage:
                        {
                            RadioButton4.Checked = true;
                            break;
                        }

                    case Theme.Structures.LogonUI7.Sources.SolidColor:
                        {
                            RadioButton3.Checked = true;
                            break;
                        }
                }

                ID = logonUI.LockScreenSystemID;

                TextBox1.Text = logonUI.ImagePath;
                color_pick.BackColor = logonUI.Color;
                pnl_preview.BackColor = logonUI.Color;
                CheckBox8.Checked = logonUI.Grayscale;
                CheckBox7.Checked = logonUI.Blur;
                CheckBox6.Checked = logonUI.Noise;

                trackBarX1.Value = logonUI.Blur_Intensity;
                trackBarX2.Value = logonUI.Noise_Intensity;

                switch (logonUI.Noise_Mode)
                {
                    case BitmapExtensions.NoiseMode.Acrylic:
                        {
                            ComboBox1.SelectedIndex = 0;
                            break;
                        }

                    case BitmapExtensions.NoiseMode.Aero:
                        {
                            ComboBox1.SelectedIndex = 1;
                            break;
                        }
                }
            }

            else if (Program.WindowStyle == WindowStyle.W7)
            {
                logonUI = Program.TM.LogonUI7;

                pictureBox1.Visible = false;
                checkBox1.Visible = false;

                AspectEnabled = logonUI.Enabled;

                switch (logonUI.Mode)
                {
                    case Theme.Structures.LogonUI7.Sources.Default:
                        {
                            RadioButton1.Checked = true;
                            break;
                        }

                    case Theme.Structures.LogonUI7.Sources.Wallpaper:
                        {
                            RadioButton2.Checked = true;
                            break;
                        }

                    case Theme.Structures.LogonUI7.Sources.CustomImage:
                        {
                            RadioButton4.Checked = true;
                            break;
                        }

                    case Theme.Structures.LogonUI7.Sources.SolidColor:
                        {
                            RadioButton3.Checked = true;
                            break;
                        }
                }

                TextBox1.Text = logonUI.ImagePath;
                color_pick.BackColor = logonUI.Color;
                pnl_preview.BackColor = logonUI.Color;
                CheckBox8.Checked = logonUI.Grayscale;
                CheckBox7.Checked = logonUI.Blur;
                CheckBox6.Checked = logonUI.Noise;

                trackBarX1.Value = logonUI.Blur_Intensity;
                trackBarX2.Value = logonUI.Noise_Intensity;

                switch (logonUI.Noise_Mode)
                {
                    case BitmapExtensions.NoiseMode.Acrylic:
                        {
                            ComboBox1.SelectedIndex = 0;
                            break;
                        }

                    case BitmapExtensions.NoiseMode.Aero:
                        {
                            ComboBox1.SelectedIndex = 1;
                            break;
                        }
                }
            }
        }

        public void ApplyToTM(Theme.Manager TM)
        {
            if (Program.WindowStyle == WindowStyle.W81)
            {
                logonUI = Program.TM.LogonUI81;

                logonUI.Enabled = AspectEnabled;

                logonUI.NoLockScreen = checkBox1.Checked;

                if (RadioButton1.Checked)
                    logonUI.Mode = Theme.Structures.LogonUI7.Sources.Default;
                if (RadioButton2.Checked)
                    logonUI.Mode = Theme.Structures.LogonUI7.Sources.Wallpaper;
                if (RadioButton3.Checked)
                    logonUI.Mode = Theme.Structures.LogonUI7.Sources.SolidColor;
                if (RadioButton4.Checked)
                    logonUI.Mode = Theme.Structures.LogonUI7.Sources.CustomImage;

                logonUI.LockScreenSystemID = ID;

                logonUI.ImagePath = TextBox1.Text;
                logonUI.Color = color_pick.BackColor;

                logonUI.Grayscale = CheckBox8.Checked;
                logonUI.Blur = CheckBox7.Checked;
                logonUI.Noise = CheckBox6.Checked;

                logonUI.Blur_Intensity = trackBarX1.Value;
                logonUI.Noise_Intensity = trackBarX2.Value;

                if (ComboBox1.SelectedIndex == 0)
                    logonUI.Noise_Mode = BitmapExtensions.NoiseMode.Acrylic;
                if (ComboBox1.SelectedIndex == 1)
                    logonUI.Noise_Mode = BitmapExtensions.NoiseMode.Aero;
            }

            else if (Program.WindowStyle == WindowStyle.W7)
            {
                logonUI = Program.TM.LogonUI7;

                logonUI.Enabled = AspectEnabled;

                if (RadioButton1.Checked)
                    logonUI.Mode = Theme.Structures.LogonUI7.Sources.Default;
                if (RadioButton2.Checked)
                    logonUI.Mode = Theme.Structures.LogonUI7.Sources.Wallpaper;
                if (RadioButton3.Checked)
                    logonUI.Mode = Theme.Structures.LogonUI7.Sources.SolidColor;
                if (RadioButton4.Checked)
                    logonUI.Mode = Theme.Structures.LogonUI7.Sources.CustomImage;

                logonUI.ImagePath = TextBox1.Text;
                logonUI.Color = color_pick.BackColor;

                logonUI.Grayscale = CheckBox8.Checked;
                logonUI.Blur = CheckBox7.Checked;
                logonUI.Noise = CheckBox6.Checked;

                logonUI.Blur_Intensity = trackBarX1.Value;
                logonUI.Noise_Intensity = trackBarX2.Value;

                if (ComboBox1.SelectedIndex == 0)
                    logonUI.Noise_Mode = BitmapExtensions.NoiseMode.Acrylic;
                if (ComboBox1.SelectedIndex == 1)
                    logonUI.Noise_Mode = BitmapExtensions.NoiseMode.Aero;
            }
        }

        public Bitmap ReturnBK()
        {
            Bitmap bmpX = null;

            if (RadioButton1.Checked)
            {
                if (OS.W7 || OS.WVista)
                {
                    bmpX = PE.GetPNG(SysPaths.imageres, 5038);
                }

                else if (OS.W8x)
                {
                    string SysLock;
                    if (!(ID == 1) & !(ID == 3))
                    {
                        SysLock = $@"{SysPaths.Windows}\Web\Screen\img10{ID}.jpg";
                    }
                    else
                    {
                        SysLock = $@"{SysPaths.Windows}\Web\Screen\img10{ID}.png";
                    }

                    bmpX = Bitmap_Mgr.Load(SysLock);
                }
            }

            else if (RadioButton2.Checked)
            {
                using (Bitmap b = new Bitmap(Program.GetWallpaperFromRegistry()))
                {
                    bmpX = (Bitmap)b.Clone();
                }
            }

            else if (RadioButton3.Checked)
            {
                bmpX = color_pick.BackColor.ToBitmap(Screen.PrimaryScreen.Bounds.Size);
            }

            else if (RadioButton4.Checked & System.IO.File.Exists(TextBox1.Text))
            {
                bmpX = Bitmap_Mgr.Load(TextBox1.Text);
            }

            else
            {
                bmpX = Color.Black.ToBitmap(Screen.PrimaryScreen.Bounds.Size);

            }

            if (bmpX is not null)
            {
                return (Bitmap)ApplyEffects(bmpX.Resize(pnl_preview.Size));
            }
            else
            {
                return null;
            }
        }

        public void ApplyPreview()
        {
            Cursor = Cursors.AppStarting;
            pnl_preview.BackgroundImage = ReturnBK();
            Cursor = Cursors.Default;
        }

        public object ApplyEffects(Bitmap bmp)
        {
            Bitmap _bmp = bmp;

            if (CheckBox8.Checked) _bmp = _bmp.Grayscale();

            if (CheckBox7.Checked)
            {
                using (ImageFactory imgF = new())
                {
                    imgF.Load((Bitmap)_bmp.Clone());
                    imgF.GaussianBlur(trackBarX1.Value);
                    _bmp = imgF.Image.Clone() as Bitmap;
                }
            }

            if (CheckBox6.Checked)
            {
                switch (ComboBox1.SelectedIndex)
                {
                    case 0:
                        {
                            _bmp = _bmp.Noise(BitmapExtensions.NoiseMode.Acrylic, trackBarX2.Value / 100f);
                            break;
                        }
                    case 1:
                        {
                            _bmp = _bmp.Noise(BitmapExtensions.NoiseMode.Aero, trackBarX2.Value / 100f);
                            break;
                        }
                }
            }

            return _bmp;
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShown & RadioButton1.Checked)
                pnl_preview.BackgroundImage = ReturnBK();
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShown & RadioButton2.Checked)
                pnl_preview.BackgroundImage = ReturnBK();
        }

        private void RadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShown & RadioButton4.Checked)
                pnl_preview.BackgroundImage = ReturnBK();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (IsShown & RadioButton4.Checked & System.IO.File.Exists(TextBox1.Text))
                pnl_preview.BackgroundImage = ReturnBK();
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShown & RadioButton3.Checked)
                pnl_preview.BackgroundImage = ReturnBK();
        }

        private void CheckBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShown)
                pnl_preview.BackgroundImage = ReturnBK();
        }

        private void CheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShown)
                pnl_preview.BackgroundImage = ReturnBK();
        }

        private void CheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShown)
                pnl_preview.BackgroundImage = ReturnBK();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsShown & CheckBox6.Checked)
                pnl_preview.BackgroundImage = ReturnBK();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.Images, Title = Program.Lang.Filter_OpenImages })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    TextBox1.Text = dlg.FileName;
                }
            }
        }

        private void Color_pick_Click(object sender, EventArgs e)
        {

            if (e is DragEventArgs)
            {
                pnl_preview.BackgroundImage = ReturnBK();
                return;
            }

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    pnl_preview.BackgroundImage = ReturnBK();
                }
                return;
            }

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { pnl_preview, new string[] { nameof(pnl_preview.BackColor) } }
            };

            if (RadioButton3.Checked)
                pnl_preview.BackgroundImage = null;

            Color C = Forms.ColorPickerDlg.Pick(CList);

            ((UI.Controllers.ColorItem)sender).BackColor = Color.FromArgb(255, C);

            pnl_preview.BackgroundImage = ReturnBK();

            CList.Clear();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (Forms.LogonUI8_Pics.ShowDialog() == DialogResult.OK)
            {
                ApplyPreview();
            }
        }

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Links.Wiki.LogonUI_8x);
        }

        private void trackBarX1_ValueChanged(object sender, EventArgs e)
        {
            if (IsShown & CheckBox7.Checked) pnl_preview.BackgroundImage = ReturnBK();
        }

        private void trackBarX2_ValueChanged(object sender, EventArgs e)
        {
            if (IsShown & CheckBox6.Checked) pnl_preview.BackgroundImage = ReturnBK();
        }
    }
}