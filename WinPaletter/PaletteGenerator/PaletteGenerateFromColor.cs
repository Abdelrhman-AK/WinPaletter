using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinPaletter
{
    public partial class PaletteGenerateFromColor
    {

        private List<Color> Colors_List = new List<Color>();
        private Theme.Manager TM_Backup;

        private bool PickerOpened = false;

        public PaletteGenerateFromColor()
        {
            InitializeComponent();
        }

        private void PaletteGenerateFromImage_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            WPStyle.ApplyStyle(this);
            Icon = Forms.PaletteGenerateFromImage.Icon;
            TM_Backup = new Theme.Manager(Theme.Manager.Source.Registry);
        }
        private void SelectedColor_DragDrop(object sender, DragEventArgs e)
        {
            GetColors();
        }

        private void SelectedColor_Click(object sender, EventArgs e)
        {

            if (e is DragEventArgs)
                return;

            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                PickerOpened = true;
                SelectedColor.BackColor = Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                GetColors();
                PickerOpened = false;
            }

            else
            {
                var ctrls = new List<Control>() { SelectedColor };
                PickerOpened = true;
                SelectedColor.BackColor = Forms.ColorPickerDlg.Pick(ctrls);
                GetColors();
                PickerOpened = false;

            }

        }

        private void SelectedColor_BackColorChanged(object sender, EventArgs e)
        {
            if (!PickerOpened)
                GetColors();
        }

        private void Trackbar1_Scroll(object sender)
        {
            val1.Text = ((UI.WP.Trackbar)sender).Value.ToString();
            GetColors();
        }

        private void val1_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar1.Maximum), Trackbar1.Minimum).ToString();
            Trackbar1.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void CheckBox1_CheckedChanged(object sender)
        {
            GetColors();
        }

        private void RadioButton3_CheckedChanged(object sender)
        {
            if (((UI.WP.RadioButton)sender).Checked)
                GetColors();
        }

        public void GetColors()
        {

            foreach (UI.Controllers.ColorItem ctrl in ImgPaletteContainer.Controls.OfType<UI.Controllers.ColorItem>())
                ctrl.Dispose();
            ImgPaletteContainer.Controls.Clear();

            Colors_List.Clear();

            var _Color = SelectedColor.BackColor;
            var _ColorInverted = _Color.Invert();

            if (RadioButton5.Checked)
            {
                _Color = _Color.Light();
                _ColorInverted = _ColorInverted.Light();
            }

            else if (RadioButton4.Checked)
            {
                _Color = _Color.LightLight();
                _ColorInverted = _ColorInverted.LightLight();
            }

            else if (RadioButton6.Checked)
            {
                _Color = _Color.Dark();
                _ColorInverted = _ColorInverted.Dark();
            }

            else if (RadioButton7.Checked)
            {
                _Color = _Color.Dark(0.8f);
                _ColorInverted = _ColorInverted.Dark(0.8f);

            }

            Colors_List.Add(_Color);
            if (CheckBox1.Checked)
                Colors_List.Add(_ColorInverted);

            for (double i = 0d, loopTo = Trackbar1.Value / 2d; i <= loopTo; i++)
            {
                Colors_List.Add(_Color.Dark((float)(i / Trackbar1.Value)));
                Colors_List.Add(_Color.Light((float)(i / Trackbar1.Value)));

                if (CheckBox1.Checked)
                {
                    Colors_List.Add(_ColorInverted.Dark((float)(i / Trackbar1.Value)));
                    Colors_List.Add(_ColorInverted.Light((float)(i / Trackbar1.Value)));
                }
            }

            Colors_List.Sort(new RGBColorComparer());

            foreach (Color c in Colors_List)
            {
                UI.Controllers.ColorItem MiniColorItem = new UI.Controllers.ColorItem();
                MiniColorItem.Size = MiniColorItem.GetMiniColorItemSize();
                MiniColorItem.AllowDrop = false;
                MiniColorItem.PauseColorsHistory = true;
                MiniColorItem.BackColor = c;
                MiniColorItem.DefaultColor = MiniColorItem.BackColor;

                ImgPaletteContainer.Controls.Add(MiniColorItem);
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var arr = GetUniqueRandomNumbers(0, Colors_List.Count);

            switch (Program.PreviewStyle)
            {
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

        private static Random StaticRandom = new Random();

        public static List<int> GetUniqueRandomNumbers(int Start, int Count)
        {
            lock (StaticRandom)
                return Enumerable.Range(Start, Count).OrderBy(__ => StaticRandom.Next()).ToList();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            switch (Program.PreviewStyle)
            {
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