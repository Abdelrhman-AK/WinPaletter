using ImageProcessor;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter
{

    public partial class LogonUI7
    {
        private bool _Shown = false;
        private readonly Bitmap b;
        public int ID;

        public LogonUI7()
        {
            InitializeComponent();
        }

        private void LogonUI7_Load(object sender, EventArgs e)
        {
            ID = 0;
            this.LoadLanguage();
            ApplyStyle(this);
            _Shown = false;
            LoadFromTM(Program.TM);
            ApplyPreview();
            Icon = Forms.LogonUI.Icon;

            if (Program.PreviewStyle == WindowStyle.W81)
            {
                Button3.Visible = true;
                PictureBox11.Image = Properties.Resources.LogonUI8;
                PictureBox4.Image = Properties.Resources.Native8;
            }
            else if (Program.PreviewStyle == WindowStyle.W7)
            {
                Button3.Visible = false;
                PictureBox11.Image = Properties.Resources.LogonUI7;
                PictureBox4.Image = Properties.Resources.Native7;
            }

            Button12.Image = Forms.MainFrm.Button20.Image.Resize(16, 16);
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

        private void LogonUI7_Shown(object sender, EventArgs e)
        {
            _Shown = true;
        }

        public void LoadFromTM(Theme.Manager TM)
        {

            if (Program.PreviewStyle == WindowStyle.W81)
            {
                Toggle1.Checked = !TM.Windows81.NoLockScreen;

                switch (TM.Windows81.LockScreenType)
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

                ID = TM.Windows81.LockScreenSystemID;

                TextBox1.Text = TM.LogonUI7.ImagePath;
                color_pick.BackColor = TM.LogonUI7.Color;
                pnl_preview.BackColor = TM.LogonUI7.Color;
                CheckBox8.Checked = TM.LogonUI7.Grayscale;
                CheckBox7.Checked = TM.LogonUI7.Blur;
                CheckBox6.Checked = TM.LogonUI7.Noise;

                Trackbar1.Value = TM.LogonUI7.Blur_Intensity;
                Trackbar2.Value = TM.LogonUI7.Noise_Intensity;

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

            else if (Program.PreviewStyle == WindowStyle.W7)
            {

                Toggle1.Checked = TM.LogonUI7.Enabled;

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

                Trackbar1.Value = TM.LogonUI7.Blur_Intensity;
                Trackbar2.Value = TM.LogonUI7.Noise_Intensity;

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



        }

        public void LoadToTM(Theme.Manager TM)
        {

            if (Program.PreviewStyle == WindowStyle.W81)
            {
                TM.Windows81.NoLockScreen = !Toggle1.Checked;

                if (RadioButton1.Checked)
                    TM.Windows81.LockScreenType = Theme.Structures.LogonUI7.Sources.Default;
                if (RadioButton2.Checked)
                    TM.Windows81.LockScreenType = Theme.Structures.LogonUI7.Sources.Wallpaper;
                if (RadioButton3.Checked)
                    TM.Windows81.LockScreenType = Theme.Structures.LogonUI7.Sources.SolidColor;
                if (RadioButton4.Checked)
                    TM.Windows81.LockScreenType = Theme.Structures.LogonUI7.Sources.CustomImage;

                TM.Windows81.LockScreenSystemID = ID;

                TM.LogonUI7.ImagePath = TextBox1.Text;
                TM.LogonUI7.Color = color_pick.BackColor;

                TM.LogonUI7.Grayscale = CheckBox8.Checked;
                TM.LogonUI7.Blur = CheckBox7.Checked;
                TM.LogonUI7.Noise = CheckBox6.Checked;

                TM.LogonUI7.Blur_Intensity = Trackbar1.Value;
                TM.LogonUI7.Noise_Intensity = Trackbar2.Value;

                if (ComboBox1.SelectedIndex == 0)
                    TM.LogonUI7.Noise_Mode = BitmapExtensions.NoiseMode.Acrylic;
                if (ComboBox1.SelectedIndex == 1)
                    TM.LogonUI7.Noise_Mode = BitmapExtensions.NoiseMode.Aero;
            }

            else if (Program.PreviewStyle == WindowStyle.W7)
            {
                TM.LogonUI7.Enabled = Toggle1.Checked;

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

                TM.LogonUI7.Blur_Intensity = Trackbar1.Value;
                TM.LogonUI7.Noise_Intensity = Trackbar2.Value;

                if (ComboBox1.SelectedIndex == 0)
                    TM.LogonUI7.Noise_Mode = BitmapExtensions.NoiseMode.Acrylic;
                if (ComboBox1.SelectedIndex == 1)
                    TM.LogonUI7.Noise_Mode = BitmapExtensions.NoiseMode.Aero;
            }

        }

        public Bitmap ReturnBK()
        {
            Bitmap bmpX = null;

            if (RadioButton1.Checked)
            {
                if (OS.W7 | OS.WVista)
                {
                    bmpX = PE_Functions.GetPNGFromDLL(PathsExt.imageres, 5038);
                }

                else if (OS.W8 | OS.W81)
                {
                    string SysLock;
                    if (!(ID == 1) & !(ID == 3))
                    {
                        SysLock = string.Format(PathsExt.Windows + @"\Web\Screen\img10{0}.jpg", ID);
                    }
                    else
                    {
                        SysLock = string.Format(PathsExt.Windows + @"\Web\Screen\img10{0}.png", ID);
                    }

                    bmpX = Bitmap_Mgr.Load(SysLock);
                }
            }

            else if (RadioButton2.Checked)
            {
                using (var b = new Bitmap(Program.GetWallpaper()))
                {
                    bmpX = (Bitmap)b.Clone();
                }
            }

            else if (RadioButton3.Checked)
            {
                bmpX = (Bitmap)color_pick.BackColor.ToBitmap(Program.Computer.Screen.Bounds.Size);
            }

            else if (RadioButton4.Checked & System.IO.File.Exists(TextBox1.Text))
            {
                bmpX = Bitmap_Mgr.Load(TextBox1.Text);
            }

            else
            {
                bmpX = (Bitmap)Color.Black.ToBitmap(Program.Computer.Screen.Bounds.Size);

            }

            if (bmpX is not null)
            {
                return (Bitmap)ApplyEffects((Bitmap)bmpX.Resize(pnl_preview.Size));
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
            Bitmap _bmp;
            _bmp = bmp;

            try
            {
                if (CheckBox8.Checked)
                    _bmp = _bmp.Grayscale();

                if (CheckBox7.Checked)
                {
                    var imgF = new ImageFactory();
                    imgF.Load((Bitmap)_bmp.Clone());
                    imgF.GaussianBlur(Trackbar1.Value);
                    _bmp = (Bitmap)imgF.Image;
                }

                if (CheckBox6.Checked)
                {
                    switch (ComboBox1.SelectedIndex)
                    {
                        case 0:
                            {
                                _bmp = _bmp.Noise(BitmapExtensions.NoiseMode.Acrylic, (float)(Trackbar2.Value / 100d));
                                break;
                            }
                        case 1:
                            {
                                _bmp = _bmp.Noise(BitmapExtensions.NoiseMode.Aero, (float)(Trackbar2.Value / 100d));
                                break;
                            }
                    }
                }
            }
            catch
            {
            }

            return _bmp;
        }

        private void RadioButton1_CheckedChanged(object sender)
        {
            if (_Shown & RadioButton1.Checked)
                pnl_preview.BackgroundImage = ReturnBK();
        }

        private void RadioButton2_CheckedChanged(object sender)
        {
            if (_Shown & RadioButton2.Checked)
                pnl_preview.BackgroundImage = ReturnBK();
        }

        private void RadioButton4_CheckedChanged(object sender)
        {
            if (_Shown & RadioButton4.Checked)
                pnl_preview.BackgroundImage = ReturnBK();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (_Shown & RadioButton4.Checked & System.IO.File.Exists(TextBox1.Text))
                pnl_preview.BackgroundImage = ReturnBK();
        }

        private void RadioButton3_CheckedChanged(object sender)
        {
            if (_Shown & RadioButton3.Checked)
                pnl_preview.BackgroundImage = ReturnBK();
        }

        private void CheckBox8_CheckedChanged(object sender)
        {
            if (_Shown)
                pnl_preview.BackgroundImage = ReturnBK();
        }

        private void CheckBox7_CheckedChanged(object sender)
        {
            if (_Shown)
                pnl_preview.BackgroundImage = ReturnBK();
        }

        private void CheckBox6_CheckedChanged(object sender)
        {
            if (_Shown)
                pnl_preview.BackgroundImage = ReturnBK();
        }

        private void Trackbar1_Scroll(object sender)
        {
            ttl_h.Text = ((UI.WP.Trackbar)sender).Value.ToString();
            if (_Shown & CheckBox7.Checked)
                pnl_preview.BackgroundImage = ReturnBK();
        }

        private void NumericUpDown2_Click(object sender)
        {
            Button4.Text = ((UI.WP.Trackbar)sender).Value.ToString();
            if (_Shown & CheckBox6.Checked)
                pnl_preview.BackgroundImage = ReturnBK();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_Shown & CheckBox6.Checked)
                pnl_preview.BackgroundImage = ReturnBK();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            LoadToTM(Program.TM);
            Close();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (OpenImgDlg.ShowDialog() == DialogResult.OK)
            {
                TextBox1.Text = OpenImgDlg.FileName;
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

            var CList = new List<Control>() { (Control)sender, pnl_preview };

            if (RadioButton3.Checked)
                pnl_preview.BackgroundImage = null;

            var C = Forms.ColorPickerDlg.Pick(CList);

            ((UI.Controllers.ColorItem)sender).BackColor = Color.FromArgb(255, C);

            pnl_preview.BackgroundImage = ReturnBK();

            CList.Clear();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (Forms.LogonUI8_Pics.ShowDialog() == DialogResult.OK)
            {
                ApplyPreview();
            }
        }

        private void ttl_h_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar1.Maximum), Trackbar1.Minimum).ToString();
            Trackbar1.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar2.Maximum), Trackbar2.Minimum).ToString();
            Trackbar2.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var TMx = new Theme.Manager(Theme.Manager.Source.File, OpenFileDialog1.FileName);
                LoadFromTM(TMx);
                TMx.Dispose();
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            var TMx = new Theme.Manager(Theme.Manager.Source.Registry);
            LoadFromTM(TMx);
            TMx.Dispose();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            LoadFromTM(Theme.Default.Get(Program.PreviewStyle));
        }

        private void Toggle1_CheckedChanged(object sender, EventArgs e)
        {
            checker_img.Image = Conversions.ToBoolean(((UI.WP.Toggle)sender).Checked) ? Properties.Resources.checker_enabled : Properties.Resources.checker_disabled;
        }

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Properties.Resources.Link_Wiki + "/Edit-LogonUI-screen#windows-81-and-windows-7");
        }
    }
}