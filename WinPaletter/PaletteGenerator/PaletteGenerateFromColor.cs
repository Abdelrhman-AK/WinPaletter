using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.UI.Controllers;

namespace WinPaletter
{
    public partial class PaletteGenerateFromColor
    {
        private List<Color> Colors_List = new();
        private object backup;
        private List<Action> actions = new();
        private bool PickerOpened = false;

        public PaletteGenerateFromColor()
        {
            InitializeComponent();
        }

        private void PaletteGenerateFromImage_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            using (PaletteGenerateFromImage formIcon = new()) { Icon = formIcon.Icon; }

            actions.Clear();
            listBox1.Items.Clear();

            if (Application.OpenForms.OfType<Win11Colors>().Count() > 0)
            {
                Left = Forms.Win11Colors.Left + 10;

                backup = new Theme.Structures.Windows10x()
                {
                    Titlebar_Active = Forms.Win11Colors.TActive.BackColor,
                    Titlebar_Inactive = Forms.Win11Colors.TInactive.BackColor,
                    StartMenu_Accent = Forms.Win11Colors.C7.BackColor,
                    Color_Index2 = Forms.Win11Colors.C6.BackColor,
                    Color_Index6 = Forms.Win11Colors.C8.BackColor,
                    Color_Index1 = Forms.Win11Colors.C3.BackColor,
                    Color_Index4 = Forms.Win11Colors.C4.BackColor,
                    Color_Index5 = Forms.Win11Colors.C1.BackColor,
                    Color_Index0 = Forms.Win11Colors.C2.BackColor,
                    Color_Index3 = Forms.Win11Colors.C5.BackColor,
                    Color_Index7 = Forms.Win11Colors.C9.BackColor,
                };
            }

            else if (Application.OpenForms.OfType<Win10Colors>().Count() > 0)
            {
                Left = Forms.Win10Colors.Left + 10;

                backup = new Theme.Structures.Windows10x()
                {
                    Titlebar_Active = Forms.Win10Colors.TActive.BackColor,
                    Titlebar_Inactive = Forms.Win10Colors.TInactive.BackColor,
                    StartMenu_Accent = Forms.Win10Colors.C7.BackColor,
                    Color_Index2 = Forms.Win10Colors.C6.BackColor,
                    Color_Index6 = Forms.Win10Colors.C8.BackColor,
                    Color_Index1 = Forms.Win10Colors.C3.BackColor,
                    Color_Index4 = Forms.Win10Colors.C4.BackColor,
                    Color_Index5 = Forms.Win10Colors.C1.BackColor,
                    Color_Index0 = Forms.Win10Colors.C2.BackColor,
                    Color_Index3 = Forms.Win10Colors.C5.BackColor,
                    Color_Index7 = Forms.Win10Colors.C9.BackColor,
                };
            }

            else if (Application.OpenForms.OfType<Win7Colors>().Count() > 0)
            {
                Left = Forms.Win7Colors.Left + 10;

                backup = new Theme.Structures.Windows7()
                {
                    ColorizationColor = Forms.Win7Colors.ColorizationColor_pick.BackColor,
                    ColorizationAfterglow = Forms.Win7Colors.ColorizationAfterglow_pick.BackColor
                };
            }

            else if (Application.OpenForms.OfType<Win81Colors>().Count() > 0)
            {
                Left = Forms.Win81Colors.Left + 10;

                backup = new Theme.Structures.Windows8x()
                {
                    ColorizationColor = Forms.Win7Colors.ColorizationColor_pick.BackColor,
                    PersonalColors_Background = Forms.Win81Colors.personalcls_background_pick.BackColor,
                    PersonalColors_Accent = Forms.Win81Colors.personalcolor_accent_pick.BackColor,
                    AccentColor = Forms.Win81Colors.accent_pick.BackColor,
                    StartColor = Forms.Win81Colors.start_pick.BackColor,
                };
            }

            GetColors();
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
                ColorItem colorItem = (ColorItem)sender;
                Dictionary<Control, string[]> CList = new()
                {
                    { colorItem, new string[] { nameof(colorItem.BackColor) }},
                };

                PickerOpened = true;
                SelectedColor.BackColor = Forms.ColorPickerDlg.Pick(CList);
                GetColors();
                PickerOpened = false;
            }

        }

        private void SelectedColor_BackColorChanged(object sender, EventArgs e)
        {
            if (!PickerOpened)
                GetColors();
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            GetColors();
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
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

            Color _Color = SelectedColor.BackColor;
            Color _ColorInverted = _Color.Invert();

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
                UI.Controllers.ColorItem MiniColorItem = new();
                MiniColorItem.Size = MiniColorItem.GetMiniColorItemSize();
                MiniColorItem.AllowDrop = false;
                MiniColorItem.PauseColorsHistory = true;
                MiniColorItem.BackColor = c;
                MiniColorItem.DefaultBackColor = MiniColorItem.BackColor;

                ImgPaletteContainer.Controls.Add(MiniColorItem);
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (Colors_List != null && Colors_List.Count > 0)
            {
                List<int> arr = GetUniqueRandomNumbers(0, Colors_List.Count);

                if (Application.OpenForms.OfType<Win11Colors>().Count() > 0)
                {
                    Color color0 = Colors_List[arr[0]];
                    Color color1 = Colors_List[arr[1]];
                    Color color2 = Colors_List[arr[2]];
                    Color color3 = Colors_List[arr[3]];
                    Color color4 = Colors_List[arr[4]];
                    Color color5 = Colors_List[arr[5]];
                    Color color6 = Colors_List[arr[6]];
                    Color color7 = Colors_List[arr[7]];
                    Color color8 = Colors_List[arr[8]];
                    Color color9 = Colors_List[arr[9]];
                    Color color10 = Colors_List[arr[10]];

                    actions.Add(() =>
                    {
                        Forms.Win11Colors.TActive.BackColor = color0;
                        Forms.Win11Colors.TInactive.BackColor = color1;
                        Forms.Win11Colors.C1.BackColor = color2;
                        Forms.Win11Colors.C2.BackColor = color3;
                        Forms.Win11Colors.C3.BackColor = color4;
                        Forms.Win11Colors.C4.BackColor = color5;
                        Forms.Win11Colors.C5.BackColor = color6;
                        Forms.Win11Colors.C6.BackColor = color7;
                        Forms.Win11Colors.C7.BackColor = color8;
                        Forms.Win11Colors.C8.BackColor = color9;
                        Forms.Win11Colors.C9.BackColor = color10;
                        Forms.Win11Colors.windowsDesktop1.TitlebarColor_Active = color0;
                        Forms.Win11Colors.windowsDesktop1.TitlebarColor_Inactive = color1;
                        Forms.Win11Colors.windowsDesktop1.Color1 = color2;
                        Forms.Win11Colors.windowsDesktop1.Color2 = color3;
                        Forms.Win11Colors.windowsDesktop1.Color3 = color4;
                        Forms.Win11Colors.windowsDesktop1.Color4 = color5;
                        Forms.Win11Colors.windowsDesktop1.Color5 = color6;
                        Forms.Win11Colors.windowsDesktop1.Color6 = color7;
                        Forms.Win11Colors.windowsDesktop1.Color7 = color8;
                        Forms.Win11Colors.windowsDesktop1.Color8 = color9;
                        Forms.Win11Colors.windowsDesktop1.Color9 = color10;
                    });

                    listBox1.Items.Add(DateTime.Now.ToLongTimeString());

                    actions.Last().Invoke();
                }

                else if (Application.OpenForms.OfType<Win10Colors>().Count() > 0)
                {
                    Color color0 = Colors_List[arr[0]];
                    Color color1 = Colors_List[arr[1]];
                    Color color2 = Colors_List[arr[2]];
                    Color color3 = Colors_List[arr[3]];
                    Color color4 = Colors_List[arr[4]];
                    Color color5 = Colors_List[arr[5]];
                    Color color6 = Colors_List[arr[6]];
                    Color color7 = Colors_List[arr[7]];
                    Color color8 = Colors_List[arr[8]];
                    Color color9 = Colors_List[arr[9]];
                    Color color10 = Colors_List[arr[10]];

                    actions.Add(() =>
                    {
                        Forms.Win10Colors.TActive.BackColor = color0;
                        Forms.Win10Colors.TInactive.BackColor = color1;
                        Forms.Win10Colors.C1.BackColor = color2;
                        Forms.Win10Colors.C2.BackColor = color3;
                        Forms.Win10Colors.C3.BackColor = color4;
                        Forms.Win10Colors.C4.BackColor = color5;
                        Forms.Win10Colors.C5.BackColor = color6;
                        Forms.Win10Colors.C6.BackColor = color7;
                        Forms.Win10Colors.C7.BackColor = color8;
                        Forms.Win10Colors.C8.BackColor = color9;
                        Forms.Win10Colors.C9.BackColor = color10;
                        Forms.Win10Colors.windowsDesktop1.TitlebarColor_Active = color0;
                        Forms.Win10Colors.windowsDesktop1.TitlebarColor_Inactive = color1;
                        Forms.Win10Colors.windowsDesktop1.Color1 = color2;
                        Forms.Win10Colors.windowsDesktop1.Color2 = color3;
                        Forms.Win10Colors.windowsDesktop1.Color3 = color4;
                        Forms.Win10Colors.windowsDesktop1.Color4 = color5;
                        Forms.Win10Colors.windowsDesktop1.Color5 = color6;
                        Forms.Win10Colors.windowsDesktop1.Color6 = color7;
                        Forms.Win10Colors.windowsDesktop1.Color7 = color8;
                        Forms.Win10Colors.windowsDesktop1.Color8 = color9;
                        Forms.Win10Colors.windowsDesktop1.Color9 = color10;
                    });

                    listBox1.Items.Add(DateTime.Now.ToLongTimeString());

                    actions.Last().Invoke();
                }

                else if (Application.OpenForms.OfType<Win7Colors>().Count() > 0)
                {
                    Color color0 = Colors_List[arr[0]];
                    Color color1 = Colors_List[arr[1]];

                    actions.Add(() =>
                    {
                        Forms.Win7Colors.ColorizationColor_pick.BackColor = color0;
                        Forms.Win7Colors.ColorizationAfterglow_pick.BackColor = color1;
                        Forms.Win7Colors.windowsDesktop1.TitlebarColor_Active = color0;
                        Forms.Win7Colors.windowsDesktop1.TitlebarColor_Inactive = color0;
                        Forms.Win7Colors.windowsDesktop1.AfterGlowColor_Active = color1;
                        Forms.Win7Colors.windowsDesktop1.AfterGlowColor_Inactive = color1;
                    });

                    listBox1.Items.Add(DateTime.Now.ToLongTimeString());

                    actions.Last().Invoke();
                }

                else if (Application.OpenForms.OfType<Win81Colors>().Count() > 0)
                {
                    Color color0 = Colors_List[arr[0]];
                    Color color1 = Colors_List[arr[1]];
                    Color color2 = Colors_List[arr[2]];

                    actions.Add(() =>
                    {
                        Forms.Win81Colors.ColorizationColor_pick.BackColor = color0;
                        Forms.Win81Colors.personalcolor_accent_pick.BackColor = color1;
                        Forms.Win81Colors.accent_pick.BackColor = color1;
                        Forms.Win81Colors.personalcls_background_pick.BackColor = color2;
                        Forms.Win81Colors.start_pick.BackColor = color2;
                    });

                    listBox1.Items.Add(DateTime.Now.ToLongTimeString());

                    actions.Last().Invoke();
                }

            }
            else
            {
                Program.ToolTip.ToolTipText = Program.Lang.SelectASourceToGenerate;
                Program.ToolTip.ToolTipTitle = Program.Lang.Error;
                Program.ToolTip.Image = Assets.Notifications.Error;

                Point location = new(0, ((Control)sender).Height + 2);

                Program.ToolTip.Show((Control)sender, Program.ToolTip.ToolTipTitle, Program.ToolTip.ToolTipText, Program.ToolTip.Image, location, 5000);
            }
        }

        private static Random StaticRandom = new Random();

        public static List<int> GetUniqueRandomNumbers(int Start, int Count)
        {
            lock (StaticRandom)
                return Enumerable.Range(Start, Count).OrderBy(__ => StaticRandom.Next()).ToList();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Win11Colors>().Count() > 0)
            {
                Forms.Win11Colors.TActive.BackColor = ((Theme.Structures.Windows10x)backup).Titlebar_Active;
                Forms.Win11Colors.TInactive.BackColor = ((Theme.Structures.Windows10x)backup).Titlebar_Inactive;
                Forms.Win11Colors.C1.BackColor = ((Theme.Structures.Windows10x)backup).Color_Index5;
                Forms.Win11Colors.C2.BackColor = ((Theme.Structures.Windows10x)backup).Color_Index0;
                Forms.Win11Colors.C3.BackColor = ((Theme.Structures.Windows10x)backup).Color_Index1;
                Forms.Win11Colors.C4.BackColor = ((Theme.Structures.Windows10x)backup).Color_Index4;
                Forms.Win11Colors.C5.BackColor = ((Theme.Structures.Windows10x)backup).Color_Index3;
                Forms.Win11Colors.C6.BackColor = ((Theme.Structures.Windows10x)backup).Color_Index2;
                Forms.Win11Colors.C7.BackColor = ((Theme.Structures.Windows10x)backup).StartMenu_Accent;
                Forms.Win11Colors.C8.BackColor = ((Theme.Structures.Windows10x)backup).Color_Index6;
                Forms.Win11Colors.C9.BackColor = ((Theme.Structures.Windows10x)backup).Color_Index7;
                Forms.Win11Colors.windowsDesktop1.TitlebarColor_Active = ((Theme.Structures.Windows10x)backup).Titlebar_Active;
                Forms.Win11Colors.windowsDesktop1.TitlebarColor_Inactive = ((Theme.Structures.Windows10x)backup).Titlebar_Inactive;
                Forms.Win11Colors.windowsDesktop1.Color1 = ((Theme.Structures.Windows10x)backup).Color_Index5;
                Forms.Win11Colors.windowsDesktop1.Color2 = ((Theme.Structures.Windows10x)backup).Color_Index0;
                Forms.Win11Colors.windowsDesktop1.Color3 = ((Theme.Structures.Windows10x)backup).Color_Index1;
                Forms.Win11Colors.windowsDesktop1.Color4 = ((Theme.Structures.Windows10x)backup).Color_Index4;
                Forms.Win11Colors.windowsDesktop1.Color5 = ((Theme.Structures.Windows10x)backup).Color_Index3;
                Forms.Win11Colors.windowsDesktop1.Color6 = ((Theme.Structures.Windows10x)backup).Color_Index2;
                Forms.Win11Colors.windowsDesktop1.Color7 = ((Theme.Structures.Windows10x)backup).StartMenu_Accent;
                Forms.Win11Colors.windowsDesktop1.Color8 = ((Theme.Structures.Windows10x)backup).Color_Index6;
                Forms.Win11Colors.windowsDesktop1.Color9 = ((Theme.Structures.Windows10x)backup).Color_Index7;
            }

            else if (Application.OpenForms.OfType<Win10Colors>().Count() > 0)
            {
                Forms.Win10Colors.TActive.BackColor = ((Theme.Structures.Windows10x)backup).Titlebar_Active;
                Forms.Win10Colors.TInactive.BackColor = ((Theme.Structures.Windows10x)backup).Titlebar_Inactive;
                Forms.Win10Colors.C1.BackColor = ((Theme.Structures.Windows10x)backup).Color_Index5;
                Forms.Win10Colors.C2.BackColor = ((Theme.Structures.Windows10x)backup).Color_Index0;
                Forms.Win10Colors.C3.BackColor = ((Theme.Structures.Windows10x)backup).Color_Index1;
                Forms.Win10Colors.C4.BackColor = ((Theme.Structures.Windows10x)backup).Color_Index4;
                Forms.Win10Colors.C5.BackColor = ((Theme.Structures.Windows10x)backup).Color_Index3;
                Forms.Win10Colors.C6.BackColor = ((Theme.Structures.Windows10x)backup).Color_Index2;
                Forms.Win10Colors.C7.BackColor = ((Theme.Structures.Windows10x)backup).StartMenu_Accent;
                Forms.Win10Colors.C8.BackColor = ((Theme.Structures.Windows10x)backup).Color_Index6;
                Forms.Win10Colors.C9.BackColor = ((Theme.Structures.Windows10x)backup).Color_Index7;
                Forms.Win10Colors.windowsDesktop1.TitlebarColor_Active = ((Theme.Structures.Windows10x)backup).Titlebar_Active;
                Forms.Win10Colors.windowsDesktop1.TitlebarColor_Inactive = ((Theme.Structures.Windows10x)backup).Titlebar_Inactive;
                Forms.Win10Colors.windowsDesktop1.Color1 = ((Theme.Structures.Windows10x)backup).Color_Index5;
                Forms.Win10Colors.windowsDesktop1.Color2 = ((Theme.Structures.Windows10x)backup).Color_Index0;
                Forms.Win10Colors.windowsDesktop1.Color3 = ((Theme.Structures.Windows10x)backup).Color_Index1;
                Forms.Win10Colors.windowsDesktop1.Color4 = ((Theme.Structures.Windows10x)backup).Color_Index4;
                Forms.Win10Colors.windowsDesktop1.Color5 = ((Theme.Structures.Windows10x)backup).Color_Index3;
                Forms.Win10Colors.windowsDesktop1.Color6 = ((Theme.Structures.Windows10x)backup).Color_Index2;
                Forms.Win10Colors.windowsDesktop1.Color7 = ((Theme.Structures.Windows10x)backup).StartMenu_Accent;
                Forms.Win10Colors.windowsDesktop1.Color8 = ((Theme.Structures.Windows10x)backup).Color_Index6;
                Forms.Win10Colors.windowsDesktop1.Color9 = ((Theme.Structures.Windows10x)backup).Color_Index7;
            }

            else if (Application.OpenForms.OfType<Win7Colors>().Count() > 0)
            {
                Forms.Win7Colors.ColorizationColor_pick.BackColor = ((Theme.Structures.Windows7)backup).ColorizationColor;
                Forms.Win7Colors.ColorizationAfterglow_pick.BackColor = ((Theme.Structures.Windows7)backup).ColorizationAfterglow;
                Forms.Win7Colors.windowsDesktop1.TitlebarColor_Active = ((Theme.Structures.Windows7)backup).ColorizationColor;
                Forms.Win7Colors.windowsDesktop1.TitlebarColor_Inactive = ((Theme.Structures.Windows7)backup).ColorizationColor;
                Forms.Win7Colors.windowsDesktop1.AfterGlowColor_Active = ((Theme.Structures.Windows7)backup).ColorizationAfterglow;
                Forms.Win7Colors.windowsDesktop1.AfterGlowColor_Inactive = ((Theme.Structures.Windows7)backup).ColorizationAfterglow;
            }

            else if (Application.OpenForms.OfType<Win81Colors>().Count() > 0)
            {
                Forms.Win81Colors.ColorizationColor_pick.BackColor = ((Theme.Structures.Windows8x)backup).ColorizationColor;
                Forms.Win81Colors.personalcls_background_pick.BackColor = ((Theme.Structures.Windows8x)backup).PersonalColors_Background;
                Forms.Win81Colors.accent_pick.BackColor = ((Theme.Structures.Windows8x)backup).AccentColor;
                Forms.Win81Colors.personalcolor_accent_pick.BackColor = ((Theme.Structures.Windows8x)backup).PersonalColors_Accent;
                Forms.Win81Colors.start_pick.BackColor = ((Theme.Structures.Windows8x)backup).StartColor;
            }

            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            actions?.ElementAt(listBox1.SelectedIndex)?.Invoke();
        }

        private void trackBarX1_ValueChanged(object sender, EventArgs e)
        {
            GetColors();
        }
    }
}