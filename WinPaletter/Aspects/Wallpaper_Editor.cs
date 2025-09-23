using Microsoft.VisualBasic.CompilerServices;
using Ookii.Dialogs.WinForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.Theme;
using WinPaletter.UI.Controllers;
using WinPaletter.UI.WP;
using static WinPaletter.PreviewHelpers;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

namespace WinPaletter
{
    public partial class Wallpaper_Editor
    {
        /// <summary>
        /// Represents the wallpaper tone configuration for the theme.
        /// </summary>
        /// <remarks>This field holds an instance of <see cref="Theme.Structures.WallpaperTone"/>  that
        /// defines the tone settings for the wallpaper in the current theme.</remarks>
        public Theme.Structures.WallpaperTone WT = new();
        private Bitmap img, img_filled, img_tile;
        private Bitmap img_untouched, img_tinted, img_tinted_filled, img_tinted_tile;
        private string img_path = string.Empty;

        private int index = 0;
        private readonly List<string> ImgLs1 = [];
        private readonly List<string> ImgLs2 = [];

        float previewWidthFactor, previewHeightFactor;

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Links.Wiki.Wallpaper);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            img?.Dispose();
            img_filled?.Dispose();
            img_tile?.Dispose();
            img_untouched?.Dispose();
            img_tinted?.Dispose();
            img_tinted_filled?.Dispose();
            img_tinted_tile?.Dispose();
        }

        public Wallpaper_Editor()
        {
            InitializeComponent();
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
            Manager TMx = new(Manager.Source.Registry);
            LoadFromTM(TMx);
            TMx.Dispose();
        }

        private void LoadFromDefault(object sender, EventArgs e)
        {
            Manager TMx = Default.Get(Program.WindowStyle);
            LoadFromTM(TMx);
            TMx.Dispose();
        }


        private void LoadIntoCurrentTheme(object sender, EventArgs e)
        {
            ApplyToTM(Program.TM);
            ApplyWT();
            Close();
        }

        private void Apply(object sender, EventArgs e)
        {
            if (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.Wallpaper)
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

                ApplyWT();

                TMx.Wallpaper.Apply(source_wallpapertone.Checked);
                TMx.Win32.Apply();

                if (source_wallpapertone.Checked) { WT.Apply(); }
            }

            Cursor = Cursors.Default;
        }

        private void Wallpaper_Editor_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = Program.Lang.Strings.Aspects.Wallpaper,
                Enabled = Program.TM.Wallpaper.Enabled,
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

            previewWidthFactor = pnl_preview.Width / 1920f;
            previewHeightFactor = pnl_preview.Height / 1080f;

            LoadData(data);

            LoadFromTM(Program.TM);
            index = 0;
            ApplyPreviewStyle();

            switch (Program.WindowStyle)
            {
                case WindowStyle.W12:
                    {
                        AlertBox3.Text = string.Format(Program.Lang.Strings.Tips.WallpaperTone_Notice, Program.Lang.Strings.Windows.W12);
                        break;
                    }

                case WindowStyle.W11:
                    {
                        AlertBox3.Text = string.Format(Program.Lang.Strings.Tips.WallpaperTone_Notice, Program.Lang.Strings.Windows.W11);
                        break;
                    }
                case WindowStyle.W10:
                    {
                        AlertBox3.Text = string.Format(Program.Lang.Strings.Tips.WallpaperTone_Notice, Program.Lang.Strings.Windows.W10);
                        break;
                    }
                case WindowStyle.W81:
                    {
                        AlertBox3.Text = string.Format(Program.Lang.Strings.Tips.WallpaperTone_Notice, Program.Lang.Strings.Windows.W81);
                        break;
                    }
                case WindowStyle.W8:
                    {
                        AlertBox3.Text = string.Format(Program.Lang.Strings.Tips.WallpaperTone_Notice, Program.Lang.Strings.Windows.W8);
                        break;
                    }
                case WindowStyle.W7:
                    {
                        AlertBox3.Text = string.Format(Program.Lang.Strings.Tips.WallpaperTone_Notice, Program.Lang.Strings.Windows.W7);
                        break;
                    }
                case WindowStyle.WVista:
                    {
                        AlertBox3.Text = string.Format(Program.Lang.Strings.Tips.WallpaperTone_Notice, Program.Lang.Strings.Windows.WVista);
                        break;
                    }
                case WindowStyle.WXP:
                    {
                        AlertBox3.Text = string.Format(Program.Lang.Strings.Tips.WallpaperTone_Notice, Program.Lang.Strings.Windows.WXP);
                        break;
                    }

                default:
                    {
                        AlertBox3.Text = string.Format(Program.Lang.Strings.Tips.WallpaperTone_Notice, Program.Lang.Strings.Windows.Undefined);
                        break;
                    }
            }
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
            AspectEnabled = TM.Wallpaper.Enabled;
            RadioButton1.Checked = TM.Wallpaper.SlideShow_Folder_or_ImagesList;
            RadioButton2.Checked = !TM.Wallpaper.SlideShow_Folder_or_ImagesList;

            if (WT.Enabled)
            {
                source_wallpapertone.Checked = true;
            }
            else
            {
                switch (TM.Wallpaper.WallpaperType)
                {

                    case Theme.Structures.Wallpaper.WallpaperTypes.Picture:
                        {
                            source_pic.Checked = true;
                            break;
                        }
                    case Theme.Structures.Wallpaper.WallpaperTypes.SolidColor:
                        {
                            source_color.Checked = true;
                            break;
                        }
                    case Theme.Structures.Wallpaper.WallpaperTypes.SlideShow:
                        {
                            source_slideshow.Checked = true;
                            break;
                        }

                    default:
                        {
                            source_pic.Checked = true;
                            break;
                        }
                }
            }

            TextBox1.Text = TM.Wallpaper.ImageFile;
            switch (TM.Wallpaper.WallpaperStyle)
            {
                case Theme.Structures.Wallpaper.WallpaperStyles.Tile:
                    {
                        style_tile.Checked = true;
                        break;
                    }
                case Theme.Structures.Wallpaper.WallpaperStyles.Centered:
                    {
                        style_center.Checked = true;
                        break;
                    }
                case Theme.Structures.Wallpaper.WallpaperStyles.Stretched:
                    {
                        style_stretch.Checked = true;
                        break;
                    }
                case Theme.Structures.Wallpaper.WallpaperStyles.Fill:
                    {
                        style_fill.Checked = true;
                        break;
                    }
                case Theme.Structures.Wallpaper.WallpaperStyles.Fit:
                    {
                        style_fit.Checked = true;
                        break;
                    }

                default:
                    {
                        style_fill.Checked = true;
                        break;
                    }
            }

            TextBox3.Text = WT.Image;

            HBar.Value = WT.H;
            SBar.Value = WT.S;
            LBar.Value = WT.L;

            TextBox2.Text = TM.Wallpaper.Wallpaper_Slideshow_ImagesRootPath;
            ListBox1.Items.Clear();
            ListBox1.Items.AddRange(TM.Wallpaper.Wallpaper_Slideshow_Images);
            trackBarX1.Value = TM.Wallpaper.Wallpaper_Slideshow_Interval;
            CheckBox3.Checked = TM.Wallpaper.Wallpaper_Slideshow_Shuffle;

            pnl_preview.BackColor = TM.Win32.Background;
            color_pick.BackColor = TM.Win32.Background;
        }

        public void ApplyToTM(Manager TM)
        {
            Cursor = Cursors.AppStarting;

            TM.Wallpaper.Enabled = AspectEnabled;
            TM.Wallpaper.SlideShow_Folder_or_ImagesList = RadioButton1.Checked;

            if (source_pic.Checked)
            {
                TM.Wallpaper.WallpaperType = Theme.Structures.Wallpaper.WallpaperTypes.Picture;
                WT.Enabled = false;
            }

            else if (source_color.Checked)
            {
                TM.Wallpaper.WallpaperType = Theme.Structures.Wallpaper.WallpaperTypes.SolidColor;
                WT.Enabled = false;
            }

            else if (source_slideshow.Checked)
            {
                TM.Wallpaper.WallpaperType = Theme.Structures.Wallpaper.WallpaperTypes.SlideShow;
                WT.Enabled = false;
            }

            else if (source_wallpapertone.Checked)
            {
                TM.Wallpaper.WallpaperType = Theme.Structures.Wallpaper.WallpaperTypes.Picture;
                WT.Enabled = true;

            }

            TM.Wallpaper.ImageFile = TextBox1.Text;

            if (style_tile.Checked)
            {
                TM.Wallpaper.WallpaperStyle = Theme.Structures.Wallpaper.WallpaperStyles.Tile;
            }
            else if (style_center.Checked)
            {
                TM.Wallpaper.WallpaperStyle = Theme.Structures.Wallpaper.WallpaperStyles.Centered;
            }
            else if (style_stretch.Checked)
            {
                TM.Wallpaper.WallpaperStyle = Theme.Structures.Wallpaper.WallpaperStyles.Stretched;
            }
            else if (style_fill.Checked)
            {
                TM.Wallpaper.WallpaperStyle = Theme.Structures.Wallpaper.WallpaperStyles.Fill;
            }
            else if (style_fit.Checked)
            {
                TM.Wallpaper.WallpaperStyle = Theme.Structures.Wallpaper.WallpaperStyles.Fit;
            }
            else
            {
                TM.Wallpaper.WallpaperStyle = Theme.Structures.Wallpaper.WallpaperStyles.Fill;
            }

            TM.Wallpaper.Wallpaper_Slideshow_ImagesRootPath = TextBox2.Text;
            TM.Wallpaper.Wallpaper_Slideshow_Images = [];
            TM.Wallpaper.Wallpaper_Slideshow_Images = ListBox1.Items.OfType<string>().Where(s => !string.IsNullOrEmpty(s)).ToArray();

            TM.Wallpaper.Wallpaper_Slideshow_Interval = trackBarX1.Value;
            TM.Wallpaper.Wallpaper_Slideshow_Shuffle = CheckBox3.Checked;

            TM.Win32.Background = color_pick.BackColor;

            Cursor = Cursors.Default;
        }

        public void ApplyWT()
        {
            WT = new()
            {
                Enabled = source_wallpapertone.Checked,
                Image = TextBox3.Text,
                H = HBar.Value,
                S = SBar.Value,
                L = LBar.Value
            };

            switch (Program.WindowStyle)
            {
                case WindowStyle.W12:
                    {
                        Program.TM.WallpaperTone_W12 = WT;
                        break;
                    }
                case WindowStyle.W11:
                    {
                        Program.TM.WallpaperTone_W11 = WT;
                        break;
                    }
                case WindowStyle.W10:
                    {
                        Program.TM.WallpaperTone_W10 = WT;
                        break;
                    }
                case WindowStyle.W81:
                    {
                        Program.TM.WallpaperTone_W81 = WT;
                        break;
                    }
                case WindowStyle.W8:
                    {
                        Program.TM.WallpaperTone_W8 = WT;
                        break;
                    }
                case WindowStyle.W7:
                    {
                        Program.TM.WallpaperTone_W7 = WT;
                        break;
                    }
                case WindowStyle.WVista:
                    {
                        Program.TM.WallpaperTone_WVista = WT;
                        break;
                    }
                case WindowStyle.WXP:
                    {
                        Program.TM.WallpaperTone_WXP = WT;
                        break;
                    }

                default:
                    {
                        Program.TM.WallpaperTone_W12 = WT;
                        break;
                    }

            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.Images, FileName = TextBox1.Text, Title = Program.Lang.Strings.Extensions.OpenImages })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    TextBox1.Text = dlg.FileName;
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (Program.WindowStyle == WindowStyle.WXP)
            {
                TextBox1.Text = $@"{SysPaths.Windows}\Web\Wallpaper\Bliss.bmp";
            }
            else
            {
                TextBox1.Text = $@"{SysPaths.Windows}\Web\Wallpaper\Windows\img0.jpg";
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            TextBox1.Text = ReadReg("HKEY_CURRENT_USER\\Control Panel\\Desktop", "Wallpaper", string.Empty).ToString();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (!OS.WXP)
            {
                using (VistaFolderBrowserDialog dlg = new() { SelectedPath = TextBox2.Text })
                {
                    if (dlg.ShowDialog() == DialogResult.OK) TextBox2.Text = dlg.SelectedPath;
                }
            }
            else
            {
                using (FolderBrowserDialog dlg = new() { SelectedPath = TextBox2.Text })
                {
                    if (dlg.ShowDialog() == DialogResult.OK) TextBox2.Text = dlg.SelectedPath;
                }
            }
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.Images, Multiselect = true, Title = Program.Lang.Strings.Extensions.OpenImagesFiles })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    foreach (string x in dlg.FileNames)
                    {
                        if (!ListBox1.Items.Contains(x)) ListBox1.Items.Add(x);
                    }
                }

                if (source_slideshow.Checked && RadioButton2.Checked)
                {
                    Set_SlideshowSource();
                    ApplyPreviewStyle();
                }
            }
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            if (ListBox1.SelectedItem is not null)
            {
                ArrayList items = new(ListBox1.SelectedItems);
                foreach (object item in items)
                    ListBox1.Items.Remove(item);
            }

            if (source_slideshow.Checked && RadioButton2.Checked)
            {
                Set_SlideshowSource();
                ApplyPreviewStyle();
            }
        }

        public void MoveItem(int direction)
        {
            if (ListBox1.SelectedItem is null || ListBox1.SelectedIndex < 0)
                return;
            int newIndex = ListBox1.SelectedIndex + direction;
            if (newIndex < 0 || newIndex >= ListBox1.Items.Count)
                return;
            object selected = ListBox1.SelectedItem;
            ListBox1.Items.Remove(selected);
            ListBox1.Items.Insert(newIndex, selected);
            ListBox1.SetSelected(newIndex, true);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            MoveItem(-1);

            if (source_slideshow.Checked && RadioButton2.Checked)
            {
                Set_SlideshowSource();
                ApplyPreviewStyle();
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            MoveItem(+1);

            if (source_slideshow.Checked && RadioButton2.Checked)
            {
                Set_SlideshowSource();
                ApplyPreviewStyle();
            }
        }

        private void Source_slideshow_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioImage)sender).Checked)
            {
                tablessControl1.SelectedIndex = 3;

                Set_SlideshowSource();
                ApplyPreviewStyle();
            }

            Panel1.Visible = true;
        }

        public void ApplyPreviewStyle()
        {
            Bitmap temp;

            if (source_color.Checked)
            {
                temp = null;
            }

            else if (!source_wallpapertone.Checked)
            {
                if (style_fill.Checked)
                {
                    temp = img_filled;
                }

                else if (style_tile.Checked)
                {
                    temp = img_tile;
                }

                else
                {
                    temp = img;

                }
            }
            else if (style_fill.Checked)
            {
                temp = img_tinted_filled;
            }

            else if (style_tile.Checked)
            {
                temp = img_tinted_tile;
            }

            else
            {
                temp = img_tinted;
            }

            Invoke(() =>
            {
                pnl_preview.Image = temp;

                if (style_fill.Checked)
                {
                    pnl_preview.SizeMode = PictureBoxSizeMode.CenterImage;
                }

                else if (style_fit.Checked)
                {
                    pnl_preview.SizeMode = PictureBoxSizeMode.Zoom;
                }

                else if (style_stretch.Checked)
                {
                    pnl_preview.SizeMode = PictureBoxSizeMode.StretchImage;
                }

                else if (style_center.Checked)
                {
                    pnl_preview.SizeMode = PictureBoxSizeMode.CenterImage;
                }

                else if (style_tile.Checked)
                {
                    pnl_preview.SizeMode = PictureBoxSizeMode.Normal;
                }
            });
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            img_path = TextBox1.Text;
            using (Bitmap b = BitmapMgr.Load(img_path)) img = b?.Resize((int)(b.Width * previewWidthFactor), (int)(b.Height * previewHeightFactor));
            img_filled = img?.FillInSize(pnl_preview.Size);
            img_tile = img?.Tile(pnl_preview.Size);

            ApplyPreviewStyle();
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (RadioButton1.Checked)
            {
                Set_SlideshowSource();
                ApplyPreviewStyle();
            }
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            using (Bitmap b = BitmapMgr.Load(TextBox3.Text)) img_untouched = b?.Resize((int)(b.Width * previewWidthFactor), (int)(b.Height * previewHeightFactor));

            ApplyHSLPreview();
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (((UI.WP.RadioButton)sender).Checked)
            {
                Set_SlideshowSource();
                ApplyPreviewStyle();
            }
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            if (RadioButton1.Checked)
            {
                if (index + 1 <= ImgLs1.Count - 1)
                    index += 1;
                else
                    index = 0;
            }
            else if (index + 1 <= ImgLs2.Count - 1)
                index += 1;
            else
                index = 0;

            Set_SlideshowSource();
            ApplyPreviewStyle();
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            if (RadioButton1.Checked)
            {
                if (index - 1 > 0)
                    index -= 1;
                else
                    index = ImgLs2.Count - 1;
            }
            else if (index - 1 > 0)
                index -= 1;
            else
                index = ImgLs2.Count - 1;
            Set_SlideshowSource();
            ApplyPreviewStyle();
        }



        private void Color_pick_DragDrop(object sender, DragEventArgs e)
        {
            pnl_preview.BackColor = color_pick.BackColor;
        }

        private void Color_pick_Click(object sender, EventArgs e)
        {
            if (e is DragEventArgs) return;

            ColorItem colorItem = (ColorItem)sender;
            Dictionary<Control, string[]> CList = new()
            {
                { colorItem, new string[] { nameof(colorItem.BackColor) } },
                { pnl_preview, new string[] { nameof(pnl_preview.BackColor) } }
            };

            Color C = Forms.ColorPickerDlg.Pick(CList);
            ((ColorItem)sender).BackColor = Color.FromArgb(255, C);

            CList.Clear();
        }

        public void Set_SlideshowSource()
        {
            Cursor = Cursors.AppStarting;

            if (source_slideshow.Checked)
            {
                if (RadioButton1.Checked)
                {
                    ImgLs1.Clear();
                    if (Directory.Exists(TextBox2.Text)) ImgLs1.AddRange(Directory.EnumerateFiles(TextBox2.Text, "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".bmp") || s.EndsWith(".jpg") || s.EndsWith(".png") || s.EndsWith(".gif")));

                    if (index > ImgLs1.Count - 1) index = 0;
                    img_path = ImgLs1.Count > 0 ? ImgLs1[index] : string.Empty;
                    using (Bitmap b = BitmapMgr.Load(img_path)) img = b?.Resize((int)(b.Width * previewWidthFactor), (int)(b.Height * previewHeightFactor));
                    img_filled = img?.FillInSize(pnl_preview.Size);
                    img_tile = img?.Tile(pnl_preview.Size);

                    Label3.Text = $"{index + 1}/{ImgLs1.Count}";
                }
                else
                {
                    ImgLs2.Clear();
                    foreach (string item in ListBox1.Items) { if (File.Exists(item)) ImgLs2.Add(item); }

                    if (index > ImgLs2.Count - 1) index = 0;

                    img_path = ImgLs2.Count > 0 ? ImgLs2[index] : string.Empty;
                    using (Bitmap b = BitmapMgr.Load(img_path)) img = b?.Resize((int)(b.Width * previewWidthFactor), (int)(b.Height * previewHeightFactor));
                    img_filled = img?.FillInSize(pnl_preview.Size);
                    img_tile = img?.Tile(pnl_preview.Size);

                    Label3.Text = $"{index + 1}/{ImgLs2.Count}";
                }
            }

            Cursor = Cursors.Default;
        }

        private void Style_fill_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioImage)sender).Checked)
                ApplyPreviewStyle();
        }

        public void ApplyHSLPreview()
        {
            Task.Run(() =>
            {
                if (source_wallpapertone.Enabled && img_untouched is not null)
                {
                    img_tinted = img_untouched.AdjustHSL(HBar.Value, SBar.Value / 100f, LBar.Value / 100f);
                    img_tinted_filled = img_tinted.FillInSize(pnl_preview.Size);
                    img_tinted_tile = img_tinted.Tile(pnl_preview.Size);

                    ApplyPreviewStyle();
                }
            });
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new() { Filter = Program.Filters.PNG, Title = Program.Lang.Strings.Extensions.SavePNG })
            {
                if (File.Exists(TextBox3.Text) && dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Bitmap bmp = BitmapMgr.Load(TextBox3.Text))
                    {
                        bmp.AdjustHSL(HBar.Value, SBar.Value / 100f, LBar.Value / 100f).Save(dlg.FileName);
                    }
                }
            }
        }

        private void colorBarX3_ValueChanged(object sender, EventArgs e)
        {
            ApplyHSLPreview();
        }

        private void source_pic_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioImage)sender).Checked)
            {
                tablessControl1.SelectedIndex = 0;
                if (img_path.ToUpper() != TextBox1.Text.ToUpper())
                {
                    img_path = TextBox1.Text;
                    using (Bitmap b = BitmapMgr.Load(img_path)) img = b?.Resize((int)(b.Width * previewWidthFactor), (int)(b.Height * previewHeightFactor));
                    img_filled = img?.FillInSize(pnl_preview.Size);
                    img_tile = img?.Tile(pnl_preview.Size);
                }

                ApplyPreviewStyle();
            }

            Panel1.Visible = false;
        }

        private void source_color_CheckedChanged(object sender, EventArgs e)
        {
            CanOpenColorsEffects = (sender as UI.WP.RadioImage).Checked;

            if (((RadioImage)sender).Checked)
            {
                tablessControl1.SelectedIndex = 1;

                ApplyPreviewStyle();
            }

            Panel1.Visible = false;
        }

        private void color_pick_ContextMenuMadeColorChangeInvoker(object sender, ColorItem.ContextMenuMadeColorChangeEventArgs e)
        {
            pnl_preview.BackColor = e.Color;
        }

        private void source_wallpapertone_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioImage)sender).Checked)
            {
                tablessControl1.SelectedIndex = 2;

                ApplyHSLPreview();

                ApplyPreviewStyle();
            }

            Panel1.Visible = false;
        }

        private void colorBarX1_ValueChanged(object sender, EventArgs e)
        {
            ColorsExtensions.HSL HSL_ = new();
            HSL_ = Color.FromArgb(0, 255, 240).ToHSL();
            HSL_.H = Conversions.ToInteger(((ColorBarX)sender).Value);
            HSL_.S = 1f;
            HSL_.L = 0.5f;

            SBar.AccentColor = HSL_.ToRGB();
            LBar.AccentColor = HSL_.ToRGB();

            ApplyHSLPreview();
        }

        private void colorBarX2_ValueChanged(object sender, EventArgs e)
        {
            ApplyHSLPreview();
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            string WallpaperPath = ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", string.Empty).ToString();

            if (!File.Exists(WallpaperPath))
            {
                if (Program.WindowStyle == WindowStyle.WXP)
                {
                    WallpaperPath = $@"{SysPaths.Windows}\Web\Wallpaper\Bliss.bmp";
                }
                else
                {
                    WallpaperPath = $@"{SysPaths.Windows}\Web\Wallpaper\Windows\img0.jpg";
                }
            }

            TextBox3.Text = WallpaperPath;
            ApplyHSLPreview();
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            if (Program.WindowStyle == WindowStyle.WXP)
            {
                TextBox3.Text = $@"{SysPaths.Windows}\Web\Wallpaper\Bliss.bmp";
            }
            else
            {
                TextBox3.Text = $@"{SysPaths.Windows}\Web\Wallpaper\Windows\img0.jpg";
            }

            if (!File.Exists(TextBox1.Text))
            {
                TextBox3.Text = Reg_IO.ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", Program.WindowStyle == WindowStyle.WXP ? $@"{SysPaths.Windows}\Web\Wallpaper\Bliss.bmp" : $@"{SysPaths.Windows}\Web\Wallpaper\Windows\img0.jpg").ToString();
            }
            ApplyHSLPreview();
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.Images, FileName = TextBox3.Text, Title = Program.Lang.Strings.Extensions.OpenImages })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    TextBox3.Text = dlg.FileName;
                    ApplyHSLPreview();
                }
            }
        }
    }
}