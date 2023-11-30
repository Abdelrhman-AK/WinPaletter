using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class PaletteGenerateFromImage
    {

        private List<Color> Colors_List = new();
        private Theme.Manager TM_Backup;

        public PaletteGenerateFromImage()
        {
            InitializeComponent();
        }

        private void PaletteGenerateFromImage_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            TM_Backup = new(Theme.Manager.Source.Registry);
            TextBox1.Text = Program.TM.Wallpaper.ImageFile;
        }

        private void RadioButton1_CheckedChanged(object sender)
        {
            if (((UI.WP.RadioImage)sender).Checked)
                GetColors(Program.Wallpaper);
        }

        private void RadioButton2_CheckedChanged(object sender)
        {
            if (((UI.WP.RadioImage)sender).Checked)
                GetColors(Bitmap_Mgr.Load(TextBox1.Text));
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (RadioButton2.Checked)
                GetColors(Bitmap_Mgr.Load(TextBox1.Text));
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
                TextBox1.Text = OpenFileDialog1.FileName;
        }

        private void CheckBox1_CheckedChanged(object sender)
        {
            if (RadioButton1.Checked)
            {
                GetColors(Program.Wallpaper);
            }
            else
            {
                GetColors(Bitmap_Mgr.Load(TextBox1.Text));
            }
        }

        private void RadioButton3_CheckedChanged(object sender)
        {
            if (((UI.WP.RadioButton)sender).Checked)
            {
                if (RadioButton1.Checked)
                {
                    GetColors(Program.Wallpaper);
                }
                else
                {
                    GetColors(Bitmap_Mgr.Load(TextBox1.Text));
                }
            }
        }

        private void Trackbar1_Scroll(object sender)
        {
            val1.Text = ((UI.WP.Trackbar)sender).Value.ToString();

            if (RadioButton1.Checked)
            {
                GetColors(Program.Wallpaper);
            }
            else
            {
                GetColors(Bitmap_Mgr.Load(TextBox1.Text));
            }
        }

        private void Trackbar2_Scroll(object sender)
        {
            val2.Text = ((UI.WP.Trackbar)sender).Value.ToString();

            if (RadioButton1.Checked)
            {
                GetColors(Program.Wallpaper);
            }
            else
            {
                GetColors(Bitmap_Mgr.Load(TextBox1.Text));
            }
        }

        private void val1_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar1.Maximum), Trackbar1.Minimum).ToString();
            Trackbar1.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void val2_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar2.Maximum), Trackbar2.Minimum).ToString();
            Trackbar2.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        public void GetColors(Bitmap Source)
        {

            foreach (UI.Controllers.ColorItem ctrl in ImgPaletteContainer.Controls.OfType<UI.Controllers.ColorItem>())
                ctrl.Dispose();
            ImgPaletteContainer.Controls.Clear();

            if (Source is not null)
            {
                Source = (Bitmap)Source.GetThumbnailImage(Forms.MainFrm.pnl_preview.Width, Forms.MainFrm.pnl_preview.Height, null, IntPtr.Zero);
                Colors_List.Clear();
                ColorThiefDotNet.ColorThief ColorThiefX = new();
                List<ColorThiefDotNet.QuantizedColor> Colors = ColorThiefX.GetPalette(Source, Math.Max(13, Trackbar1.Value), Trackbar2.Value, CheckBox1.Checked);

                foreach (ColorThiefDotNet.QuantizedColor C in Colors)
                {
                    if (RadioButton3.Checked)
                    {
                        Colors_List.Add(Color.FromArgb(255, C.Color.R, C.Color.G, C.Color.B));
                    }

                    else if (RadioButton5.Checked)
                    {
                        Colors_List.Add(Color.FromArgb(255, C.Color.R, C.Color.G, C.Color.B).Light());
                    }

                    else if (RadioButton4.Checked)
                    {
                        Colors_List.Add(Color.FromArgb(255, C.Color.R, C.Color.G, C.Color.B).LightLight());
                    }

                    else if (RadioButton6.Checked)
                    {
                        Colors_List.Add(Color.FromArgb(255, C.Color.R, C.Color.G, C.Color.B).Dark());
                    }

                    else if (RadioButton7.Checked)
                    {
                        Colors_List.Add(Color.FromArgb(255, C.Color.R, C.Color.G, C.Color.B).Dark(0.8f));
                    }

                    else
                    {
                        Colors_List.Add(Color.FromArgb(255, C.Color.R, C.Color.G, C.Color.B));

                    }
                }

                Colors_List.Sort(new RGBColorComparer());

                foreach (Color c in Colors_List)
                {
                    UI.Controllers.ColorItem MiniColorItem = new();
                    MiniColorItem.Size = MiniColorItem.GetMiniColorItemSize();
                    MiniColorItem.AllowDrop = false;
                    MiniColorItem.PauseColorsHistory = true;
                    MiniColorItem.BackColor = c;
                    MiniColorItem.DefaultBackColor = MiniColorItem.BackColor;

                    ImgPaletteContainer.Controls.Add(MiniColorItem);
                }
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var arr = GetUniqueRandomNumbers(0, Colors_List.Count);

            switch (Program.PreviewStyle)
            {
                case PreviewHelpers.WindowStyle.W12:
                    {
                        Program.TM.Windows12.Titlebar_Active = Colors_List[arr[0]];
                        Program.TM.Windows12.Titlebar_Inactive = Colors_List[arr[1]];
                        Program.TM.Windows12.StartMenu_Accent = Colors_List[arr[2]];
                        Program.TM.Windows12.Color_Index0 = Colors_List[arr[3]];
                        Program.TM.Windows12.Color_Index1 = Colors_List[arr[4]];
                        Program.TM.Windows12.Color_Index2 = Colors_List[arr[5]];
                        Program.TM.Windows12.Color_Index3 = Colors_List[arr[6]];
                        Program.TM.Windows12.Color_Index4 = Colors_List[arr[7]];
                        Program.TM.Windows12.Color_Index5 = Colors_List[arr[8]];
                        Program.TM.Windows12.Color_Index6 = Colors_List[arr[9]];
                        Program.TM.Windows12.Color_Index7 = Colors_List[arr[10]];
                        break;
                    }

                case PreviewHelpers.WindowStyle.W11:
                    {
                        Program.TM.Windows11.Titlebar_Active = Colors_List[arr[0]];
                        Program.TM.Windows11.Titlebar_Inactive = Colors_List[arr[1]];
                        Program.TM.Windows11.StartMenu_Accent = Colors_List[arr[2]];
                        Program.TM.Windows11.Color_Index0 = Colors_List[arr[3]];
                        Program.TM.Windows11.Color_Index1 = Colors_List[arr[4]];
                        Program.TM.Windows11.Color_Index2 = Colors_List[arr[5]];
                        Program.TM.Windows11.Color_Index3 = Colors_List[arr[6]];
                        Program.TM.Windows11.Color_Index4 = Colors_List[arr[7]];
                        Program.TM.Windows11.Color_Index5 = Colors_List[arr[8]];
                        Program.TM.Windows11.Color_Index6 = Colors_List[arr[9]];
                        Program.TM.Windows11.Color_Index7 = Colors_List[arr[10]];
                        break;
                    }

                case PreviewHelpers.WindowStyle.W10:
                    {
                        Program.TM.Windows10.Titlebar_Active = Colors_List[arr[0]];
                        Program.TM.Windows10.Titlebar_Inactive = Colors_List[arr[1]];
                        Program.TM.Windows10.StartMenu_Accent = Colors_List[arr[2]];
                        Program.TM.Windows10.Color_Index0 = Colors_List[arr[3]];
                        Program.TM.Windows10.Color_Index1 = Colors_List[arr[4]];
                        Program.TM.Windows10.Color_Index2 = Colors_List[arr[5]];
                        Program.TM.Windows10.Color_Index3 = Colors_List[arr[6]];
                        Program.TM.Windows10.Color_Index4 = Colors_List[arr[7]];
                        Program.TM.Windows10.Color_Index5 = Colors_List[arr[8]];
                        Program.TM.Windows10.Color_Index6 = Colors_List[arr[9]];
                        Program.TM.Windows10.Color_Index7 = Colors_List[arr[10]];
                        break;
                    }

                case PreviewHelpers.WindowStyle.W81:
                    {
                        Program.TM.Windows81.AccentColor = Colors_List[arr[0]];
                        Program.TM.Windows81.ColorizationColor = Colors_List[arr[1]];
                        Program.TM.Windows81.PersonalColors_Accent = Colors_List[arr[2]];
                        Program.TM.Windows81.PersonalColors_Background = Colors_List[arr[3]];
                        Program.TM.Windows81.StartColor = Colors_List[arr[4]];
                        break;
                    }

                case PreviewHelpers.WindowStyle.W7:
                    {
                        Program.TM.Windows7.ColorizationColor = Colors_List[arr[0]];
                        Program.TM.Windows7.ColorizationAfterglow = Colors_List[arr[1]];
                        break;
                    }

                case PreviewHelpers.WindowStyle.WVista:
                    {
                        Program.TM.WindowsVista.ColorizationColor = Colors_List[arr[0]];
                        break;
                    }

            }

            Forms.MainFrm.LoadFromTM(Program.TM);
            Forms.MainFrm.ApplyColorsToElements(Program.TM);
        }

        private static Random StaticRandom = new();

        public static List<int> GetUniqueRandomNumbers(int Start, int Count)
        {
            lock (StaticRandom)
                return Enumerable.Range(Start, Count).OrderBy(__ => StaticRandom.Next()).ToList();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            switch (Program.PreviewStyle)
            {
                case PreviewHelpers.WindowStyle.W12:
                    {
                        Program.TM.Windows12.Titlebar_Active = TM_Backup.Windows12.Titlebar_Active;
                        Program.TM.Windows12.StartMenu_Accent = TM_Backup.Windows12.StartMenu_Accent;
                        Program.TM.Windows12.Color_Index0 = TM_Backup.Windows12.Color_Index0;
                        Program.TM.Windows12.Color_Index1 = TM_Backup.Windows12.Color_Index1;
                        Program.TM.Windows12.Color_Index2 = TM_Backup.Windows12.Color_Index2;
                        Program.TM.Windows12.Color_Index3 = TM_Backup.Windows12.Color_Index3;
                        Program.TM.Windows12.Color_Index4 = TM_Backup.Windows12.Color_Index4;
                        Program.TM.Windows12.Color_Index5 = TM_Backup.Windows12.Color_Index5;
                        Program.TM.Windows12.Color_Index6 = TM_Backup.Windows12.Color_Index6;
                        Program.TM.Windows12.Color_Index7 = TM_Backup.Windows12.Color_Index7;
                        break;
                    }

                case PreviewHelpers.WindowStyle.W11:
                    {
                        Program.TM.Windows11.Titlebar_Active = TM_Backup.Windows11.Titlebar_Active;
                        Program.TM.Windows11.StartMenu_Accent = TM_Backup.Windows11.StartMenu_Accent;
                        Program.TM.Windows11.Color_Index0 = TM_Backup.Windows11.Color_Index0;
                        Program.TM.Windows11.Color_Index1 = TM_Backup.Windows11.Color_Index1;
                        Program.TM.Windows11.Color_Index2 = TM_Backup.Windows11.Color_Index2;
                        Program.TM.Windows11.Color_Index3 = TM_Backup.Windows11.Color_Index3;
                        Program.TM.Windows11.Color_Index4 = TM_Backup.Windows11.Color_Index4;
                        Program.TM.Windows11.Color_Index5 = TM_Backup.Windows11.Color_Index5;
                        Program.TM.Windows11.Color_Index6 = TM_Backup.Windows11.Color_Index6;
                        Program.TM.Windows11.Color_Index7 = TM_Backup.Windows11.Color_Index7;
                        break;
                    }

                case PreviewHelpers.WindowStyle.W10:
                    {
                        Program.TM.Windows10.Titlebar_Active = TM_Backup.Windows10.Titlebar_Active;
                        Program.TM.Windows10.StartMenu_Accent = TM_Backup.Windows10.StartMenu_Accent;
                        Program.TM.Windows10.Color_Index0 = TM_Backup.Windows10.Color_Index0;
                        Program.TM.Windows10.Color_Index1 = TM_Backup.Windows10.Color_Index1;
                        Program.TM.Windows10.Color_Index2 = TM_Backup.Windows10.Color_Index2;
                        Program.TM.Windows10.Color_Index3 = TM_Backup.Windows10.Color_Index3;
                        Program.TM.Windows10.Color_Index4 = TM_Backup.Windows10.Color_Index4;
                        Program.TM.Windows10.Color_Index5 = TM_Backup.Windows10.Color_Index5;
                        Program.TM.Windows10.Color_Index6 = TM_Backup.Windows10.Color_Index6;
                        Program.TM.Windows10.Color_Index7 = TM_Backup.Windows10.Color_Index7;
                        break;
                    }

                case PreviewHelpers.WindowStyle.W81:
                    {
                        Program.TM.Windows81.AccentColor = TM_Backup.Windows81.AccentColor;
                        Program.TM.Windows81.ColorizationColor = TM_Backup.Windows81.ColorizationColor;
                        Program.TM.Windows81.PersonalColors_Accent = TM_Backup.Windows81.PersonalColors_Accent;
                        Program.TM.Windows81.PersonalColors_Background = TM_Backup.Windows81.PersonalColors_Background;
                        Program.TM.Windows81.StartColor = TM_Backup.Windows81.StartColor;
                        break;
                    }

                case PreviewHelpers.WindowStyle.W7:
                    {
                        Program.TM.Windows7.ColorizationColor = TM_Backup.Windows7.ColorizationColor;
                        Program.TM.Windows7.ColorizationAfterglow = TM_Backup.Windows7.ColorizationAfterglow;
                        break;
                    }

                case PreviewHelpers.WindowStyle.WVista:
                    {
                        Program.TM.WindowsVista.ColorizationColor = TM_Backup.WindowsVista.ColorizationColor;
                        break;
                    }

            }

            Forms.MainFrm.LoadFromTM(Program.TM);
            Forms.MainFrm.ApplyColorsToElements(Program.TM);

            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}