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

    public partial class LogonUI81
    {
        public int ID;

        public LogonUI81()
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
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, Title = Program.Lang.Strings.Extensions.OpenWinPaletterTheme })
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
                MsgBox(Program.Lang.Strings.Aspects.Disabled_Apply_0, MessageBoxButtons.OK, MessageBoxIcon.Warning, Program.Lang.Strings.Aspects.Disabled_Apply_1);
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
                TMx.LogonUI81.Apply();
            }

            Cursor = Cursors.Default;
        }

        private void LogonUI81_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = Program.Lang.Strings.Aspects.LockScreen,
                Enabled = Program.TM.LogonUI81.Enabled,
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
            Icon = FormsExtensions.Icon<LogonUI>();

            PictureBox11.Image = Assets.LogonUIRes.Win81;
            PictureBox4.Image = Assets.WinLogos.Win81;
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

        public void LoadFromTM(Theme.Manager TM)
        {
            pictureBox1.Visible = true;
            checkBox1.Visible = true;
            checkBox1.Checked = TM.LogonUI81.NoLockScreen;

            AspectEnabled = TM.LogonUI81.Enabled;

            switch (TM.LogonUI81.Mode)
            {
                case Theme.Structures.LogonUI81.Sources.Default:
                    {
                        RadioButton1.Checked = true;
                        break;
                    }

                case Theme.Structures.LogonUI81.Sources.Wallpaper:
                    {
                        RadioButton2.Checked = true;
                        break;
                    }

                case Theme.Structures.LogonUI81.Sources.CustomImage:
                    {
                        RadioButton4.Checked = true;
                        break;
                    }

                case Theme.Structures.LogonUI81.Sources.SolidColor:
                    {
                        RadioButton3.Checked = true;
                        break;
                    }
            }

            ID = TM.LogonUI81.LockScreenSystemID;

            TextBox1.Text = TM.LogonUI81.ImagePath;
            color_pick.BackColor = TM.LogonUI81.Color;
            pnl_preview.BackColor = TM.LogonUI81.Color;
            CheckBox8.Checked = TM.LogonUI81.Grayscale;
            CheckBox7.Checked = TM.LogonUI81.Blur;
            CheckBox6.Checked = TM.LogonUI81.Noise;

            trackBarX1.Value = TM.LogonUI81.Blur_Intensity;
            trackBarX2.Value = TM.LogonUI81.Noise_Intensity;

            switch (TM.LogonUI81.Noise_Mode)
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

        public void ApplyToTM(Theme.Manager TM)
        {
            TM.LogonUI81.Enabled = AspectEnabled;

            TM.LogonUI81.NoLockScreen = checkBox1.Checked;

            if (RadioButton1.Checked)
                TM.LogonUI81.Mode = Theme.Structures.LogonUI81.Sources.Default;
            if (RadioButton2.Checked)
                TM.LogonUI81.Mode = Theme.Structures.LogonUI81.Sources.Wallpaper;
            if (RadioButton3.Checked)
                TM.LogonUI81.Mode = Theme.Structures.LogonUI81.Sources.SolidColor;
            if (RadioButton4.Checked)
                TM.LogonUI81.Mode = Theme.Structures.LogonUI81.Sources.CustomImage;

            TM.LogonUI81.LockScreenSystemID = ID;

            TM.LogonUI81.ImagePath = TextBox1.Text;
            TM.LogonUI81.Color = color_pick.BackColor;

            TM.LogonUI81.Grayscale = CheckBox8.Checked;
            TM.LogonUI81.Blur = CheckBox7.Checked;
            TM.LogonUI81.Noise = CheckBox6.Checked;

            TM.LogonUI81.Blur_Intensity = trackBarX1.Value;
            TM.LogonUI81.Noise_Intensity = trackBarX2.Value;

            if (ComboBox1.SelectedIndex == 0)
                TM.LogonUI81.Noise_Mode = BitmapExtensions.NoiseMode.Acrylic;
            if (ComboBox1.SelectedIndex == 1)
                TM.LogonUI81.Noise_Mode = BitmapExtensions.NoiseMode.Aero;
        }

        public Bitmap ReturnBK()
        {
            Bitmap bmpX = null;

            if (RadioButton1.Checked && OS.W8x)
            {
                string SysLock;
                if (ID != 1 & ID != 2)
                {
                    SysLock = $@"{SysPaths.Windows}\Web\Screen\img10{ID}.jpg";
                }
                else
                {
                    SysLock = $@"{SysPaths.Windows}\Web\Screen\img10{ID}.png";
                }

                bmpX = Bitmap_Mgr.Load(SysLock);
            }

            else if (RadioButton2.Checked)
            {
                using (Bitmap b = new(Program.GetWallpaperFromRegistry()))
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
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.Images, Title = Program.Lang.Strings.Extensions.OpenImages })
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
                Forms.SubMenu.ShowMenu((ColorItem)sender);
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

            ((ColorItem)sender).BackColor = Color.FromArgb(255, C);

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