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
        private CP CP_Backup;

        private bool PickerOpened = false;

        public PaletteGenerateFromColor()
        {
            InitializeComponent();
        }

        private void PaletteGenerateFromImage_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            WPStyle.ApplyStyle(this);
            Icon = My.MyProject.Forms.PaletteGenerateFromImage.Icon;
            CP_Backup = new CP(CP.CP_Type.Registry);
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
                SelectedColor.BackColor = My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                GetColors();
                PickerOpened = false;
            }

            else
            {
                var ctrls = new List<Control>() { SelectedColor };
                PickerOpened = true;
                SelectedColor.BackColor = My.MyProject.Forms.ColorPickerDlg.Pick(ctrls);
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
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
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

            switch (My.Env.PreviewStyle)
            {
                case PreviewHelpers.WindowStyle.W11:
                    {
                        My.Env.CP.Windows11.Titlebar_Active = Colors_List[arr[0]];
                        My.Env.CP.Windows11.Titlebar_Inactive = Colors_List[arr[1]];
                        My.Env.CP.Windows11.StartMenu_Accent = Colors_List[arr[2]];
                        My.Env.CP.Windows11.Color_Index0 = Colors_List[arr[3]];
                        My.Env.CP.Windows11.Color_Index1 = Colors_List[arr[4]];
                        My.Env.CP.Windows11.Color_Index2 = Colors_List[arr[5]];
                        My.Env.CP.Windows11.Color_Index3 = Colors_List[arr[6]];
                        My.Env.CP.Windows11.Color_Index4 = Colors_List[arr[7]];
                        My.Env.CP.Windows11.Color_Index5 = Colors_List[arr[8]];
                        My.Env.CP.Windows11.Color_Index6 = Colors_List[arr[9]];
                        My.Env.CP.Windows11.Color_Index7 = Colors_List[arr[10]];
                        break;
                    }

                case PreviewHelpers.WindowStyle.W10:
                    {
                        My.Env.CP.Windows10.Titlebar_Active = Colors_List[arr[0]];
                        My.Env.CP.Windows10.Titlebar_Inactive = Colors_List[arr[1]];
                        My.Env.CP.Windows10.StartMenu_Accent = Colors_List[arr[2]];
                        My.Env.CP.Windows10.Color_Index0 = Colors_List[arr[3]];
                        My.Env.CP.Windows10.Color_Index1 = Colors_List[arr[4]];
                        My.Env.CP.Windows10.Color_Index2 = Colors_List[arr[5]];
                        My.Env.CP.Windows10.Color_Index3 = Colors_List[arr[6]];
                        My.Env.CP.Windows10.Color_Index4 = Colors_List[arr[7]];
                        My.Env.CP.Windows10.Color_Index5 = Colors_List[arr[8]];
                        My.Env.CP.Windows10.Color_Index6 = Colors_List[arr[9]];
                        My.Env.CP.Windows10.Color_Index7 = Colors_List[arr[10]];
                        break;
                    }

                case PreviewHelpers.WindowStyle.W81:
                    {
                        My.Env.CP.Windows81.AccentColor = Colors_List[arr[0]];
                        My.Env.CP.Windows81.ColorizationColor = Colors_List[arr[1]];
                        My.Env.CP.Windows81.PersonalColors_Accent = Colors_List[arr[2]];
                        My.Env.CP.Windows81.PersonalColors_Background = Colors_List[arr[3]];
                        My.Env.CP.Windows81.StartColor = Colors_List[arr[4]];
                        break;
                    }

                case PreviewHelpers.WindowStyle.W7:
                    {
                        My.Env.CP.Windows7.ColorizationColor = Colors_List[arr[0]];
                        My.Env.CP.Windows7.ColorizationAfterglow = Colors_List[arr[1]];
                        break;
                    }

                case PreviewHelpers.WindowStyle.WVista:
                    {
                        My.Env.CP.WindowsVista.ColorizationColor = Colors_List[arr[0]];
                        break;
                    }

            }

            My.MyProject.Forms.MainFrm.ApplyCPValues(My.Env.CP);
            My.MyProject.Forms.MainFrm.ApplyColorsToElements(My.Env.CP);
        }

        private static Random StaticRandom = new Random();

        public static List<int> GetUniqueRandomNumbers(int Start, int Count)
        {
            lock (StaticRandom)
                return Enumerable.Range(Start, Count).OrderBy(__ => StaticRandom.Next()).ToList();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            switch (My.Env.PreviewStyle)
            {
                case PreviewHelpers.WindowStyle.W11:
                    {
                        My.Env.CP.Windows11.Titlebar_Active = CP_Backup.Windows11.Titlebar_Active;
                        My.Env.CP.Windows11.StartMenu_Accent = CP_Backup.Windows11.StartMenu_Accent;
                        My.Env.CP.Windows11.Color_Index0 = CP_Backup.Windows11.Color_Index0;
                        My.Env.CP.Windows11.Color_Index1 = CP_Backup.Windows11.Color_Index1;
                        My.Env.CP.Windows11.Color_Index2 = CP_Backup.Windows11.Color_Index2;
                        My.Env.CP.Windows11.Color_Index3 = CP_Backup.Windows11.Color_Index3;
                        My.Env.CP.Windows11.Color_Index4 = CP_Backup.Windows11.Color_Index4;
                        My.Env.CP.Windows11.Color_Index5 = CP_Backup.Windows11.Color_Index5;
                        My.Env.CP.Windows11.Color_Index6 = CP_Backup.Windows11.Color_Index6;
                        My.Env.CP.Windows11.Color_Index7 = CP_Backup.Windows11.Color_Index7;
                        break;
                    }

                case PreviewHelpers.WindowStyle.W10:
                    {
                        My.Env.CP.Windows10.Titlebar_Active = CP_Backup.Windows10.Titlebar_Active;
                        My.Env.CP.Windows10.StartMenu_Accent = CP_Backup.Windows10.StartMenu_Accent;
                        My.Env.CP.Windows10.Color_Index0 = CP_Backup.Windows10.Color_Index0;
                        My.Env.CP.Windows10.Color_Index1 = CP_Backup.Windows10.Color_Index1;
                        My.Env.CP.Windows10.Color_Index2 = CP_Backup.Windows10.Color_Index2;
                        My.Env.CP.Windows10.Color_Index3 = CP_Backup.Windows10.Color_Index3;
                        My.Env.CP.Windows10.Color_Index4 = CP_Backup.Windows10.Color_Index4;
                        My.Env.CP.Windows10.Color_Index5 = CP_Backup.Windows10.Color_Index5;
                        My.Env.CP.Windows10.Color_Index6 = CP_Backup.Windows10.Color_Index6;
                        My.Env.CP.Windows10.Color_Index7 = CP_Backup.Windows10.Color_Index7;
                        break;
                    }

                case PreviewHelpers.WindowStyle.W81:
                    {
                        My.Env.CP.Windows81.AccentColor = CP_Backup.Windows81.AccentColor;
                        My.Env.CP.Windows81.ColorizationColor = CP_Backup.Windows81.ColorizationColor;
                        My.Env.CP.Windows81.PersonalColors_Accent = CP_Backup.Windows81.PersonalColors_Accent;
                        My.Env.CP.Windows81.PersonalColors_Background = CP_Backup.Windows81.PersonalColors_Background;
                        My.Env.CP.Windows81.StartColor = CP_Backup.Windows81.StartColor;
                        break;
                    }

                case PreviewHelpers.WindowStyle.W7:
                    {
                        My.Env.CP.Windows7.ColorizationColor = CP_Backup.Windows7.ColorizationColor;
                        My.Env.CP.Windows7.ColorizationAfterglow = CP_Backup.Windows7.ColorizationAfterglow;
                        break;
                    }

                case PreviewHelpers.WindowStyle.WVista:
                    {
                        My.Env.CP.WindowsVista.ColorizationColor = CP_Backup.WindowsVista.ColorizationColor;
                        break;
                    }

            }

            My.MyProject.Forms.MainFrm.ApplyCPValues(My.Env.CP);
            My.MyProject.Forms.MainFrm.ApplyColorsToElements(My.Env.CP);

            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}