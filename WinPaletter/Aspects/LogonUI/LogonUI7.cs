using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WinPaletter.Assets;
using WinPaletter.Theme;
using WinPaletter.UI.Controllers;

namespace WinPaletter
{

    public partial class LogonUI7
    {

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
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, Title = Program.Lang.Strings.Extensions.OpenWinPaletterTheme })
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
                MsgBox(Program.Lang.Strings.Aspects.Disabled_Apply_0, MessageBoxButtons.OK, MessageBoxIcon.Warning, Program.Lang.Strings.Aspects.Disabled_Apply_1);
                return;
            }

            Cursor = Cursors.WaitCursor;

            using (Manager TMx = new(Manager.Source.Registry))
            {
                if (Program.Settings.BackupTheme.Enabled && Program.Settings.BackupTheme.AutoBackupOnApplySingleAspect)
                {
                    string filename = Program.GetUniqueFileName($"{Program.Settings.BackupTheme.BackupPath}\\OnAspectApply", $"{TMx.Info.ThemeName}_{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.wpth");
                    TMx.Save(Manager.Source.File, filename);
                }

                ApplyToTM(TMx);
                ApplyToTM(Program.TM);
                ApplyToTM(Program.TM_Original);
                TMx.LogonUI7.Apply();
            }

            Cursor = Cursors.Default;
        }

        private void LogonUI7_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = Program.Lang.Strings.Aspects.LogonUI,
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

            LoadFromTM(Program.TM);
            ApplyPreview();
            Icon = FormsExtensions.Icon<LogonUI>();

            PictureBox11.Image = LogonUIRes.Win7;
            PictureBox4.Image = WinLogos.Win7;
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
            AspectEnabled = TM.LogonUI7.Enabled;

            switch (TM.LogonUI7.Mode)
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

            TextBox1.Text = TM.LogonUI7.ImagePath;
            color_pick.BackColor = TM.LogonUI7.Color;
            pnl_preview.BackColor = TM.LogonUI7.Color;
            CheckBox8.Checked = TM.LogonUI7.Grayscale;
            CheckBox7.Checked = TM.LogonUI7.Blur;
            CheckBox6.Checked = TM.LogonUI7.Noise;

            trackBarX1.Value = TM.LogonUI7.Blur_Intensity;
            trackBarX2.Value = TM.LogonUI7.Noise_Intensity;

            switch (TM.LogonUI7.Noise_Mode)
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

        public void ApplyToTM(Manager TM)
        {
            TM.LogonUI7.Enabled = AspectEnabled;

            if (RadioButton1.Checked)
                TM.LogonUI7.Mode = Theme.Structures.LogonUI7.Sources.Default;
            if (RadioButton2.Checked)
                TM.LogonUI7.Mode = Theme.Structures.LogonUI7.Sources.Wallpaper;
            if (RadioButton3.Checked)
                TM.LogonUI7.Mode = Theme.Structures.LogonUI7.Sources.SolidColor;
            if (RadioButton4.Checked)
                TM.LogonUI7.Mode = Theme.Structures.LogonUI7.Sources.CustomImage;

            TM.LogonUI7.ImagePath = TextBox1.Text;
            TM.LogonUI7.Color = color_pick.BackColor;

            TM.LogonUI7.Grayscale = CheckBox8.Checked;
            TM.LogonUI7.Blur = CheckBox7.Checked;
            TM.LogonUI7.Noise = CheckBox6.Checked;

            TM.LogonUI7.Blur_Intensity = trackBarX1.Value;
            TM.LogonUI7.Noise_Intensity = trackBarX2.Value;

            if (ComboBox1.SelectedIndex == 0)
                TM.LogonUI7.Noise_Mode = BitmapExtensions.NoiseMode.Acrylic;
            if (ComboBox1.SelectedIndex == 1)
                TM.LogonUI7.Noise_Mode = BitmapExtensions.NoiseMode.Aero;
        }

        public Bitmap ReturnBK()
        {
            Bitmap bmpX = null;

            if (RadioButton1.Checked && (OS.W7 || OS.WVista))
            {
                bmpX = PE.GetPNG(SysPaths.imageres, 5038);
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
            else if (RadioButton4.Checked & File.Exists(TextBox1.Text))
            {
                bmpX = BitmapMgr.Load(TextBox1.Text);
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

            if (CheckBox7.Checked) _bmp = _bmp.Blur(trackBarX1.Value);

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
            if (IsShown & RadioButton4.Checked & File.Exists(TextBox1.Text))
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

        private void color_pick_ContextMenuMadeColorChangeInvoker(object sender, ColorItem.ContextMenuMadeColorChangeEventArgs e)
        {
            pnl_preview.BackgroundImage = ReturnBK();
        }
    }
}