using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter
{

    public partial class Wallpaper_Editor
    {

        public Theme.Structures.WallpaperTone WT = new Theme.Structures.WallpaperTone();
        private Bitmap img, img_filled, img_tile;
        private Bitmap img_untouched_forTint, img_tinted, img_tinted_filled, img_tinted_tile;

        private int index = 0;
        private List<string> ImgLs1 = new List<string>();
        private List<string> ImgLs2 = new List<string>();

        public Wallpaper_Editor()
        {
            InitializeComponent();
        }

        private void Wallpaper_Editor_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            WPStyle.ApplyStyle(this);
            Button12.Image = My.MyProject.Forms.MainFrm.Button20.Image.Resize(16, 16);
            ApplyFromTM(My.Env.TM);
            index = 0;
            ApplyPreviewStyle();

            switch (My.Env.PreviewStyle)
            {
                case WindowStyle.W11:
                    {
                        AlertBox3.Text = string.Format(My.Env.Lang.WallpaperTone_Notice, My.Env.Lang.OS_Win11);
                        break;
                    }
                case WindowStyle.W10:
                    {
                        AlertBox3.Text = string.Format(My.Env.Lang.WallpaperTone_Notice, My.Env.Lang.OS_Win10);
                        break;
                    }
                case WindowStyle.W81:
                    {
                        AlertBox3.Text = string.Format(My.Env.Lang.WallpaperTone_Notice, My.Env.Lang.OS_Win81);
                        break;
                    }
                case WindowStyle.W7:
                    {
                        AlertBox3.Text = string.Format(My.Env.Lang.WallpaperTone_Notice, My.Env.Lang.OS_Win7);
                        break;
                    }
                case WindowStyle.WVista:
                    {
                        AlertBox3.Text = string.Format(My.Env.Lang.WallpaperTone_Notice, My.Env.Lang.OS_WinVista);
                        break;
                    }
                case WindowStyle.WXP:
                    {
                        AlertBox3.Text = string.Format(My.Env.Lang.WallpaperTone_Notice, My.Env.Lang.OS_WinXP);
                        break;
                    }

                default:
                    {
                        AlertBox3.Text = string.Format(My.Env.Lang.WallpaperTone_Notice, My.Env.Lang.OS_WinUndefined);
                        break;
                    }
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

        public void ApplyFromTM(Theme.Manager TM)
        {
            {
                ref var temp = ref TM.Wallpaper;
                WallpaperEnabled.Checked = temp.Enabled;
                RadioButton1.Checked = temp.SlideShow_Folder_or_ImagesList;
                RadioButton2.Checked = !temp.SlideShow_Folder_or_ImagesList;

                if (WT.Enabled)
                {
                    source_wallpapertone.Checked = true;
                }
                else
                {
                    switch (temp.WallpaperType)
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

                TextBox1.Text = temp.ImageFile;
                switch (temp.WallpaperStyle)
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

                TextBox2.Text = temp.Wallpaper_Slideshow_ImagesRootPath;
                ListBox1.Items.Clear();
                ListBox1.Items.AddRange(temp.Wallpaper_Slideshow_Images);
                Trackbar1.Value = temp.Wallpaper_Slideshow_Interval;
                CheckBox3.Checked = temp.Wallpaper_Slideshow_Shuffle;

                pnl_preview.BackColor = TM.Win32.Background;
                color_pick.BackColor = TM.Win32.Background;
            }
        }

        public void ApplyToTM(Theme.Manager TM)
        {
            Cursor = Cursors.AppStarting;

            {
                ref var temp = ref TM.Wallpaper;
                temp.Enabled = WallpaperEnabled.Checked;
                temp.SlideShow_Folder_or_ImagesList = RadioButton1.Checked;

                if (source_pic.Checked)
                {
                    temp.WallpaperType = Theme.Structures.Wallpaper.WallpaperTypes.Picture;
                    WT.Enabled = false;
                }

                else if (source_color.Checked)
                {
                    temp.WallpaperType = Theme.Structures.Wallpaper.WallpaperTypes.SolidColor;
                    WT.Enabled = false;
                }

                else if (source_slideshow.Checked)
                {
                    temp.WallpaperType = Theme.Structures.Wallpaper.WallpaperTypes.SlideShow;
                    WT.Enabled = false;
                }

                else if (source_wallpapertone.Checked)
                {
                    temp.WallpaperType = Theme.Structures.Wallpaper.WallpaperTypes.Picture;
                    WT.Enabled = true;

                }

                temp.ImageFile = TextBox1.Text;

                if (style_tile.Checked)
                {
                    temp.WallpaperStyle = Theme.Structures.Wallpaper.WallpaperStyles.Tile;
                }
                else if (style_center.Checked)
                {
                    temp.WallpaperStyle = Theme.Structures.Wallpaper.WallpaperStyles.Centered;
                }
                else if (style_stretch.Checked)
                {
                    temp.WallpaperStyle = Theme.Structures.Wallpaper.WallpaperStyles.Stretched;
                }
                else if (style_fill.Checked)
                {
                    temp.WallpaperStyle = Theme.Structures.Wallpaper.WallpaperStyles.Fill;
                }
                else if (style_fit.Checked)
                {
                    temp.WallpaperStyle = Theme.Structures.Wallpaper.WallpaperStyles.Fit;
                }
                else
                {
                    temp.WallpaperStyle = Theme.Structures.Wallpaper.WallpaperStyles.Fill;
                }

                temp.Wallpaper_Slideshow_ImagesRootPath = TextBox2.Text;
                temp.Wallpaper_Slideshow_Images = new string[] { };
                temp.Wallpaper_Slideshow_Images = ListBox1.Items.OfType<string>().Where(s => !string.IsNullOrEmpty(s)).ToArray();

                temp.Wallpaper_Slideshow_Interval = Trackbar1.Value;
                temp.Wallpaper_Slideshow_Shuffle = CheckBox3.Checked;

            }

            TM.Win32.Background = color_pick.BackColor;

            Cursor = Cursors.Default;
        }

        public void ApplyWT()
        {
            WT = new Theme.Structures.WallpaperTone()
            {
                Enabled = source_wallpapertone.Checked,
                Image = TextBox3.Text,
                H = HBar.Value,
                S = SBar.Value,
                L = LBar.Value
            };

            switch (My.Env.PreviewStyle)
            {
                case WindowStyle.W11:
                    {
                        My.Env.TM.WallpaperTone_W11 = WT;
                        break;
                    }
                case WindowStyle.W10:
                    {
                        My.Env.TM.WallpaperTone_W10 = WT;
                        break;
                    }
                case WindowStyle.W81:
                    {
                        My.Env.TM.WallpaperTone_W81 = WT;
                        break;
                    }
                case WindowStyle.W7:
                    {
                        My.Env.TM.WallpaperTone_W7 = WT;
                        break;
                    }
                case WindowStyle.WVista:
                    {
                        My.Env.TM.WallpaperTone_WVista = WT;
                        break;
                    }
                case WindowStyle.WXP:
                    {
                        My.Env.TM.WallpaperTone_WXP = WT;
                        break;
                    }

                default:
                    {
                        My.Env.TM.WallpaperTone_W11 = WT;
                        break;
                    }

            }
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var TMx = new Theme.Manager(Theme.Manager.Source.File, OpenFileDialog1.FileName);
                ApplyFromTM(TMx);
                TMx.Dispose();
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            var TMx = new Theme.Manager(Theme.Manager.Source.Registry);
            ApplyFromTM(TMx);
            TMx.Dispose();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            using (var _Def = Theme.Default.From(My.Env.PreviewStyle))
            {
                ApplyFromTM(_Def);
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            ApplyToTM(My.Env.TM);
            ApplyWT();
            My.MyProject.Forms.MainFrm.ApplyStylesToElements(My.Env.TM);
            Close();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var TMx = new Theme.Manager(Theme.Manager.Source.Registry);
            ApplyToTM(TMx);
            ApplyToTM(My.Env.TM);
            ApplyWT();

            TMx.Wallpaper.Apply(source_wallpapertone.Checked);
            TMx.Win32.Apply();

            if (source_wallpapertone.Checked)
            {
                WT.Apply();
            }

            TMx.Dispose();
            Cursor = Cursors.Default;
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void WallpaperEnabled_CheckedChanged(object sender, EventArgs e)
        {
            checker_img.Image = ((UI.WP.Toggle)sender).Checked ? My.Resources.checker_enabled : My.Resources.checker_disabled;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (OpenImgDlg.ShowDialog() == DialogResult.OK)
            {
                TextBox1.Text = OpenImgDlg.FileName;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (My.Env.PreviewStyle == WindowStyle.WXP)
            {
                TextBox1.Text = My.Env.PATH_Windows + @"\Web\Wallpaper\Bliss.bmp";
            }
            else
            {
                TextBox1.Text = My.Env.PATH_Windows + @"\Web\Wallpaper\Windows\img0.jpg";
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var R1 = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            string WallpaperPath = R1.GetValue("Wallpaper").ToString();
            if (R1 is not null)
                R1.Close();

            TextBox1.Text = WallpaperPath;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (!My.Env.WXP)
            {
                var dlg = new Ookii.Dialogs.WinForms.VistaFolderBrowserDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                    TextBox2.Text = dlg.SelectedPath;
                dlg.Dispose();
            }
            else if (FolderBrowserDialog1.ShowDialog() == DialogResult.OK)
                TextBox2.Text = FolderBrowserDialog1.SelectedPath;
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            OpenImgDlg.Multiselect = true;
            if (OpenImgDlg.ShowDialog() == DialogResult.OK)
            {
                foreach (var x in OpenImgDlg.FileNames)
                {
                    if (!ListBox1.Items.Contains(x))
                        ListBox1.Items.Add(x);
                }
            }
            OpenImgDlg.Multiselect = false;

            if (source_slideshow.Checked && RadioButton2.Checked)
            {
                Set_SlideshowSource();
                ApplyPreviewStyle();
            }
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            if (ListBox1.SelectedItem is not null)
            {
                var items = new ArrayList(ListBox1.SelectedItems);
                foreach (var item in items)
                    ListBox1.Items.Remove(item);
            }

            if (source_slideshow.Checked && RadioButton2.Checked)
            {
                Set_SlideshowSource();
                ApplyPreviewStyle();
            }
        }

        private void Trackbar1_Scroll(object sender)
        {
            MD.Text = ((UI.WP.Trackbar)sender).Value.ToString();
        }

        private void MD_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar1.Maximum), Trackbar1.Minimum).ToString();
            Trackbar1.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        public void MoveItem(int direction)
        {
            if (ListBox1.SelectedItem is null || ListBox1.SelectedIndex < 0)
                return;
            int newIndex = ListBox1.SelectedIndex + direction;
            if (newIndex < 0 || newIndex >= ListBox1.Items.Count)
                return;
            var selected = ListBox1.SelectedItem;
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

        public Bitmap GetWall(string file)
        {
            if (File.Exists(file))
            {
                try
                {
                    using (var bmp = new Bitmap(Bitmap_Mgr.Load(file)))
                    {

                        float ScaleW = 1f;
                        float ScaleH = 1f;

                        if (bmp.Width > Screen.PrimaryScreen.Bounds.Size.Width | bmp.Height > Screen.PrimaryScreen.Bounds.Size.Height)
                        {
                            ScaleW = (float)(1920d / pnl_preview.Size.Width);
                            ScaleH = (float)(1080d / pnl_preview.Size.Height);
                        }

                        return (Bitmap)bmp.GetThumbnailImage((int)Math.Round(bmp.Width / ScaleW), (int)Math.Round(bmp.Height / ScaleH), null, IntPtr.Zero);
                    }
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        private void Source_pic_CheckedChanged(object sender)
        {
            if (((UI.WP.RadioImage)sender).Checked)
            {
                Set_PicSource();
                ApplyHSLPreview();
                ApplyPreviewStyle();
            }

            Panel1.Visible = false;
        }

        private void Source_slideshow_CheckedChanged(object sender)
        {
            if (((UI.WP.RadioImage)sender).Checked)
            {
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
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            Set_PicSource();
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
            Set_PicSource();
            ApplyHSLPreview();
        }

        private void RadioButton1_CheckedChanged(object sender)
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

        public void Set_PicSource()
        {
            Cursor = Cursors.AppStarting;

            if (source_pic.Checked)
            {
                if (File.Exists(TextBox1.Text))
                {
                    img = (Bitmap)GetWall(TextBox1.Text).GetThumbnailImage(My.MyProject.Computer.Screen.Bounds.Width, My.MyProject.Computer.Screen.Bounds.Height, null, IntPtr.Zero);
                    img_filled = (Bitmap)((Bitmap)img.Clone()).FillScale(pnl_preview.Size);
                    img_tile = ((Bitmap)img.Clone()).Tile(pnl_preview.Size);
                }
                else
                {
                    img = null;
                    img_filled = null;
                    img_tile = null;
                }
            }

            else if (source_wallpapertone.Checked)
            {
                if (File.Exists(TextBox3.Text))
                {
                    img_untouched_forTint = GetWall(TextBox3.Text);
                    ApplyHSLPreview();
                }

            }

            Cursor = Cursors.Default;
        }

        private void Color_pick_DragDrop(object sender, DragEventArgs e)
        {
            pnl_preview.BackColor = color_pick.BackColor;
        }

        private void Color_pick_Click(object sender, EventArgs e)
        {

            if (e is DragEventArgs)
                return;

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                var clr = My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    pnl_preview.BackColor = clr;
                }
                return;
            }

            var CList = new List<Control>() { pnl_preview };
            var C = My.MyProject.Forms.ColorPickerDlg.Pick(CList);
            ((UI.Controllers.ColorItem)sender).BackColor = Color.FromArgb(255, C);

            CList.Clear();
        }

        public void Set_SlideshowSource()
        {

            Cursor = Cursors.AppStarting;

            if (source_slideshow.Checked)
            {

                if (RadioButton1.Checked)
                {

                    if (Directory.Exists(TextBox2.Text))
                    {
                        ImgLs1.Clear();
                        ImgLs1.AddRange(Directory.EnumerateFiles(TextBox2.Text, "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".bmp") || s.EndsWith(".jpg") || s.EndsWith(".png") || s.EndsWith(".gif")));


                        if (index > ImgLs1.Count - 1)
                            index = 0;

                        img = GetWall(ImgLs1[index]);
                        img_filled = (Bitmap)((Bitmap)img.Clone()).FillScale(pnl_preview.Size);
                        img_tile = ((Bitmap)img.Clone()).Tile(pnl_preview.Size);

                        Label3.Text = index + 1 + "/" + ImgLs1.Count;
                    }
                    else
                    {
                        img = null;
                        img_filled = null;
                        img_tile = null;
                        Label3.Text = "0/0";

                    }
                }

                else
                {
                    ImgLs2.Clear();

                    foreach (string item in ListBox1.Items)
                    {
                        if (File.Exists(item))
                            ImgLs2.Add(item);
                    }

                    if (index > ImgLs2.Count - 1)
                        index = 0;
                    img = GetWall(ImgLs2[index]);
                    img_filled = (Bitmap)((Bitmap)img.Clone()).FillScale(pnl_preview.Size);
                    img_tile = ((Bitmap)img.Clone()).Tile(pnl_preview.Size);

                    Label3.Text = index + 1 + "/" + ImgLs2.Count;
                }
            }

            Cursor = Cursors.Default;
        }

        private void Style_fill_CheckedChanged(object sender)
        {
            if (((UI.WP.RadioImage)sender).Checked)
                ApplyPreviewStyle();
        }

        public void ApplyHSLPreview()
        {
            if (source_wallpapertone.Enabled && img_untouched_forTint is not null)
            {
                using (var ImgF = new ImageProcessor.ImageFactory())
                {
                    ImgF.Load(img_untouched_forTint);
                    ImgF.Hue(HBar.Value, true);
                    ImgF.Saturation(SBar.Value - 100);
                    ImgF.Brightness(LBar.Value - 100);

                    img_tinted = (Bitmap)ImgF.Image.Clone();
                    img_tinted_filled = (Bitmap)((Bitmap)img_tinted.Clone()).FillScale(pnl_preview.Size);
                    img_tinted_tile = ((Bitmap)img_tinted.Clone()).Tile(pnl_preview.Size);
                }

                ApplyPreviewStyle();
            }
        }

        private void ColorBar1_Scroll(object sender)
        {
            HB.Text = ((UI.WP.ColorBar)sender).Value.ToString();

            var HSL_ = new ColorsExtensions.HSL_Structure();
            HSL_ = Color.FromArgb(0, 255, 240).ToHSL();
            HSL_.H = Conversions.ToInteger(((UI.WP.ColorBar)sender).Value);
            HSL_.S = 1f;
            HSL_.L = 0.5f;

            SBar.AccentColor = HSL_.ToRGB();
            SBar.H = HSL_.H;

            LBar.AccentColor = HSL_.ToRGB();
            LBar.H = HSL_.H;

            ApplyHSLPreview();
        }

        private void ColorBar2_Scroll_1(object sender)
        {
            SB.Text = ((UI.WP.ColorBar)sender).Value.ToString();
            ApplyHSLPreview();
        }

        private void ColorBar3_Scroll(object sender)
        {
            LB.Text = ((UI.WP.ColorBar)sender).Value.ToString();
            ApplyHSLPreview();
        }

        private void HB_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), HBar.Maximum), HBar.Minimum).ToString();
            HBar.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
            ApplyHSLPreview();
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            if (File.Exists(TextBox3.Text) && SaveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                using (var ImgF = new ImageProcessor.ImageFactory())
                {
                    ImgF.Load(TextBox3.Text);
                    ImgF.Hue(HBar.Value, true);
                    ImgF.Saturation(SBar.Value - 100);
                    ImgF.Brightness(LBar.Value - 100);
                    ImgF.Image.Save(SaveFileDialog2.FileName);
                }
            }
        }

        private void Button23_Click(object sender, EventArgs e)
        {
            HBar.Value = 0;
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            SBar.Value = 100;
        }

        private void Button21_Click(object sender, EventArgs e)
        {
            LBar.Value = 100;
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            string WallpaperPath = GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", "").ToString();

            if (!File.Exists(WallpaperPath))
            {
                if (My.Env.PreviewStyle == WindowStyle.WXP)
                {
                    WallpaperPath = My.Env.PATH_Windows + @"\Web\Wallpaper\Bliss.bmp";
                }
                else
                {
                    WallpaperPath = My.Env.PATH_Windows + @"\Web\Wallpaper\Windows\img0.jpg";
                }
            }

            TextBox3.Text = WallpaperPath;
            ApplyHSLPreview();
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            if (My.Env.PreviewStyle == WindowStyle.WXP)
            {
                TextBox3.Text = My.Env.PATH_Windows + @"\Web\Wallpaper\Bliss.bmp";
            }
            else
            {
                TextBox3.Text = My.Env.PATH_Windows + @"\Web\Wallpaper\Windows\img0.jpg";
            }

            if (!File.Exists(TextBox1.Text))
            {
                TextBox3.Text = Reg_IO.GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", My.Env.PreviewStyle == WindowStyle.WXP ? My.Env.PATH_Windows + @"\Web\Wallpaper\Bliss.bmp" : My.Env.PATH_Windows + @"\Web\Wallpaper\Windows\img0.jpg").ToString();
            }
            ApplyHSLPreview();
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            if (OpenImgDlg.ShowDialog() == DialogResult.OK)
            {
                TextBox3.Text = OpenImgDlg.FileName;
                ApplyHSLPreview();
            }
        }

        private void SB_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), SBar.Maximum), SBar.Minimum).ToString();
            SBar.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
            ApplyHSLPreview();
        }

        private void LB_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), LBar.Maximum), LBar.Minimum).ToString();
            LBar.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
            ApplyHSLPreview();
        }

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(My.Resources.Link_Wiki + "/Edit-Wallpaper");
        }
    }
}