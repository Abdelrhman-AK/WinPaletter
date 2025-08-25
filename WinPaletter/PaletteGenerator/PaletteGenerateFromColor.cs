using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.Assets;
using WinPaletter.Theme.Structures;
using WinPaletter.UI.Controllers;

namespace WinPaletter
{
    /// <summary>
    /// This form generates a color palette from a single color.
    /// </summary>
    public partial class PaletteGenerateFromColor
    {
        // List of colors generated from the selected color.
        private readonly List<Color> Colors_List = [];

        // Backup of the colors before the generation.
        private object backup;

        // List of actions to undo the color changes.
        private readonly List<Action> actions = [];

        // Flag to prevent the color picker from opening multiple times.
        private bool PickerOpened = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaletteGenerateFromColor"/> class.
        /// </summary>
        public PaletteGenerateFromColor()
        {
            InitializeComponent();
        }

        private void PaletteGenerateFromImage_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            Icon = FormsExtensions.Icon<PaletteGenerateFromImage>();

            // Clear the list of actions and the list box.
            actions.Clear();
            listBox1.Items.Clear();

            listBox1.Font = Fonts.ConsoleMedium;

            // Backup the colors before the generation.
            if (Application.OpenForms.OfType<Win12Colors>().Count() > 0)
            {
                Left = Forms.Win12Colors.Left + 10;

                backup = new Windows10x()
                {
                    Titlebar_Active = Forms.Win12Colors.TActive.BackColor,
                    Titlebar_Inactive = Forms.Win12Colors.TInactive.BackColor,
                    StartMenu_Accent = Forms.Win12Colors.C7.BackColor,
                    Color_Index2 = Forms.Win12Colors.C6.BackColor,
                    Color_Index6 = Forms.Win12Colors.C8.BackColor,
                    Color_Index1 = Forms.Win12Colors.C3.BackColor,
                    Color_Index4 = Forms.Win12Colors.C4.BackColor,
                    Color_Index5 = Forms.Win12Colors.C1.BackColor,
                    Color_Index0 = Forms.Win12Colors.C2.BackColor,
                    Color_Index3 = Forms.Win12Colors.C5.BackColor,
                    Color_Index7 = Forms.Win12Colors.C9.BackColor,
                };
            }

            else if (Application.OpenForms.OfType<Win11Colors>().Count() > 0)
            {
                Left = Forms.Win11Colors.Left + 10;

                backup = new Windows10x()
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

                backup = new Windows10x()
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

            else if (Application.OpenForms.OfType<Win81Colors>().Count() > 0)
            {
                Left = Forms.Win81Colors.Left + 10;

                backup = new Windows81()
                {
                    ColorizationColor = Forms.Win81Colors.ColorizationColor_pick.BackColor,
                    PersonalColors_Background = Forms.Win81Colors.personalcls_background_pick.BackColor,
                    PersonalColors_Accent = Forms.Win81Colors.personalcolor_accent_pick.BackColor,
                    AccentColor = Forms.Win81Colors.accent_pick.BackColor,
                    StartColor = Forms.Win81Colors.start_pick.BackColor,
                };
            }

            else if (Application.OpenForms.OfType<Win8Colors>().Count() > 0)
            {
                Left = Forms.Win8Colors.Left + 10;

                backup = new Windows8()
                {
                    ColorizationColor = Forms.Win8Colors.ColorizationColor_pick.BackColor,
                };
            }

            else if (Application.OpenForms.OfType<Win7Colors>().Count() > 0)
            {
                Left = Forms.Win7Colors.Left + 10;

                backup = new Windows7()
                {
                    ColorizationColor = Forms.Win7Colors.ColorizationColor_pick.BackColor,
                    ColorizationAfterglow = Forms.Win7Colors.ColorizationAfterglow_pick.BackColor
                };
            }

            else if (Application.OpenForms.OfType<WinVistaColors>().Count() > 0)
            {
                Left = Forms.WinVistaColors.Left + 10;
                backup = new Windows7()
                {
                    ColorizationColor = Forms.WinVistaColors.ColorizationColor_pick.BackColor,
                };
            }

            // Get the colors from the selected color.
            GetColors();
        }

        private void SelectedColor_DragDrop(object sender, DragEventArgs e)
        {
            GetColors();
        }

        private void SelectedColor_Click(object sender, EventArgs e)
        {
            if (e is DragEventArgs) return;

            // If the right mouse button is clicked, show colors context menu.
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                PickerOpened = true;
                SelectedColor.BackColor = Forms.SubMenu.ShowMenu((ColorItem)sender);
                GetColors();
                PickerOpened = false;
            }

            // If the left mouse button is clicked, show color picker dialog.
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
            if (!PickerOpened) GetColors();
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (((UI.WP.RadioButton)sender).Checked) GetColors();
        }

        /// <summary>
        /// Get the colors from the selected color (Generate the palette).
        /// </summary>
        public void GetColors()
        {
            // Clear the list of colors and the color items.
            foreach (ColorItem ctrl in ImgPaletteContainer.Controls.OfType<ColorItem>()) ctrl?.Dispose();
            ImgPaletteContainer.Controls.Clear();
            Colors_List.Clear();

            // Get the colors from the selected color.
            Color _Color = SelectedColor.BackColor;

            // Invert the color if the Invert checkbox is checked.
            Color _ColorInverted = _Color.Invert();

            // Make palette has lighter colors tone.
            if (RadioButton5.Checked)
            {
                _Color = _Color.Light();
                _ColorInverted = _ColorInverted.Light();
            }

            // Make palette has lightest colors tone.
            else if (RadioButton4.Checked)
            {
                _Color = _Color.LightLight();
                _ColorInverted = _ColorInverted.LightLight();
            }

            // Make palette has darker colors tone.
            else if (RadioButton6.Checked)
            {
                _Color = _Color.Dark();
                _ColorInverted = _ColorInverted.Dark();
            }

            // Make palette has darkest colors tone.
            else if (RadioButton7.Checked)
            {
                _Color = _Color.Dark(0.8f);
                _ColorInverted = _ColorInverted.Dark(0.8f);

            }

            // Add the main color into the list of colors.
            Colors_List.Add(_Color);

            // Add the inverted color into the list of colors if the Invert checkbox is checked.
            if (toggle1.Checked) Colors_List.Add(_ColorInverted);

            // Add the lighter and darker colors into the list of colors to complete palette generation.
            // Trackbar1.Value is the number of colors to generate.
            for (double i = 0d, loopTo = Trackbar1.Value / 2d; i <= loopTo; i++)
            {
                Colors_List.Add(_Color.Dark((float)(i / Trackbar1.Value)));
                Colors_List.Add(_Color.Light((float)(i / Trackbar1.Value)));

                // Add the inverted lighter and darker colors into the list of colors if the Invert checkbox is checked.
                if (toggle1.Checked)
                {
                    Colors_List.Add(_ColorInverted.Dark((float)(i / Trackbar1.Value)));
                    Colors_List.Add(_ColorInverted.Light((float)(i / Trackbar1.Value)));
                }
            }

            // Sort the list of colors.
            Colors_List.Sort(new RGBColorComparer());

            // Add the colors into the color items as colors controls.
            foreach (Color c in Colors_List)
            {
                ColorItem MiniColorItem = new();
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
                // Generate a list of unique random numbers.
                List<int> arr = 0.GetUniqueRandomNumbers(Colors_List.Count);

                // Distribute the colors to the opened forms and update the color items randomly.
                if (Application.OpenForms.OfType<Win12Colors>().Count() > 0)
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
                        Forms.Win12Colors.TActive.BackColor = color0;
                        Forms.Win12Colors.TInactive.BackColor = color1;
                        Forms.Win12Colors.C1.BackColor = color2;
                        Forms.Win12Colors.C2.BackColor = color3;
                        Forms.Win12Colors.C3.BackColor = color4;
                        Forms.Win12Colors.C4.BackColor = color5;
                        Forms.Win12Colors.C5.BackColor = color6;
                        Forms.Win12Colors.C6.BackColor = color7;
                        Forms.Win12Colors.C7.BackColor = color8;
                        Forms.Win12Colors.C8.BackColor = color9;
                        Forms.Win12Colors.C9.BackColor = color10;
                        Forms.Win12Colors.windowsDesktop1.TitlebarColor_Active = color0;
                        Forms.Win12Colors.windowsDesktop1.TitlebarColor_Inactive = color1;
                        Forms.Win12Colors.windowsDesktop1.Color1 = color2;
                        Forms.Win12Colors.windowsDesktop1.Color2 = color3;
                        Forms.Win12Colors.windowsDesktop1.Color3 = color4;
                        Forms.Win12Colors.windowsDesktop1.Color4 = color5;
                        Forms.Win12Colors.windowsDesktop1.Color5 = color6;
                        Forms.Win12Colors.windowsDesktop1.Color6 = color7;
                        Forms.Win12Colors.windowsDesktop1.Color7 = color8;
                        Forms.Win12Colors.windowsDesktop1.Color8 = color9;
                        Forms.Win12Colors.windowsDesktop1.Color9 = color10;
                    });

                    listBox1.Items.Add(DateTime.Now.ToLongTimeString());

                    actions.Last().Invoke();
                }

                else if (Application.OpenForms.OfType<Win11Colors>().Count() > 0)
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

                else if (Application.OpenForms.OfType<Win8Colors>().Count() > 0)
                {
                    Color color0 = Colors_List[arr[0]];

                    actions.Add(() =>
                    {
                        Forms.Win8Colors.ColorizationColor_pick.BackColor = color0;
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

                else if (Application.OpenForms.OfType<WinVistaColors>().Count() > 0)
                {
                    Color color0 = Colors_List[arr[0]];
                    Color color1 = Colors_List[arr[1]];

                    actions.Add(() =>
                    {
                        Forms.WinVistaColors.ColorizationColor_pick.BackColor = color0;
                        Forms.WinVistaColors.windowsDesktop1.TitlebarColor_Active = color0;
                        Forms.WinVistaColors.windowsDesktop1.TitlebarColor_Inactive = color0;
                    });

                    listBox1.Items.Add(DateTime.Now.ToLongTimeString());

                    actions.Last().Invoke();
                }

                Button2.Enabled = true;

            }
            else
            {
                Button2.Enabled = false;

                // Hide a tooltip notification as an error message as the palette generation has failed.
                Program.ToolTip.ToolTipText = Program.Lang.Strings.Tips.PaletteSourceGeneration;
                Program.ToolTip.ToolTipTitle = Program.Lang.Strings.General.Error;
                Program.ToolTip.Image = Notifications.Error;

                Point location = new(0, ((Control)sender).Height + 2);

                Program.ToolTip.Show((Control)sender, Program.ToolTip.ToolTipTitle, Program.ToolTip.ToolTipText, Program.ToolTip.Image, location, 5000);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            // Undo the color changes and restore the backup colors.

            if (Application.OpenForms.OfType<Win12Colors>().Count() > 0)
            {
                Forms.Win12Colors.TActive.BackColor = ((Windows10x)backup).Titlebar_Active;
                Forms.Win12Colors.TInactive.BackColor = ((Windows10x)backup).Titlebar_Inactive;
                Forms.Win12Colors.C1.BackColor = ((Windows10x)backup).Color_Index5;
                Forms.Win12Colors.C2.BackColor = ((Windows10x)backup).Color_Index0;
                Forms.Win12Colors.C3.BackColor = ((Windows10x)backup).Color_Index1;
                Forms.Win12Colors.C4.BackColor = ((Windows10x)backup).Color_Index4;
                Forms.Win12Colors.C5.BackColor = ((Windows10x)backup).Color_Index3;
                Forms.Win12Colors.C6.BackColor = ((Windows10x)backup).Color_Index2;
                Forms.Win12Colors.C7.BackColor = ((Windows10x)backup).StartMenu_Accent;
                Forms.Win12Colors.C8.BackColor = ((Windows10x)backup).Color_Index6;
                Forms.Win12Colors.C9.BackColor = ((Windows10x)backup).Color_Index7;
                Forms.Win12Colors.windowsDesktop1.TitlebarColor_Active = ((Windows10x)backup).Titlebar_Active;
                Forms.Win12Colors.windowsDesktop1.TitlebarColor_Inactive = ((Windows10x)backup).Titlebar_Inactive;
                Forms.Win12Colors.windowsDesktop1.Color1 = ((Windows10x)backup).Color_Index5;
                Forms.Win12Colors.windowsDesktop1.Color2 = ((Windows10x)backup).Color_Index0;
                Forms.Win12Colors.windowsDesktop1.Color3 = ((Windows10x)backup).Color_Index1;
                Forms.Win12Colors.windowsDesktop1.Color4 = ((Windows10x)backup).Color_Index4;
                Forms.Win12Colors.windowsDesktop1.Color5 = ((Windows10x)backup).Color_Index3;
                Forms.Win12Colors.windowsDesktop1.Color6 = ((Windows10x)backup).Color_Index2;
                Forms.Win12Colors.windowsDesktop1.Color7 = ((Windows10x)backup).StartMenu_Accent;
                Forms.Win12Colors.windowsDesktop1.Color8 = ((Windows10x)backup).Color_Index6;
                Forms.Win12Colors.windowsDesktop1.Color9 = ((Windows10x)backup).Color_Index7;
            }

            else if (Application.OpenForms.OfType<Win11Colors>().Count() > 0)
            {
                Forms.Win11Colors.TActive.BackColor = ((Windows10x)backup).Titlebar_Active;
                Forms.Win11Colors.TInactive.BackColor = ((Windows10x)backup).Titlebar_Inactive;
                Forms.Win11Colors.C1.BackColor = ((Windows10x)backup).Color_Index5;
                Forms.Win11Colors.C2.BackColor = ((Windows10x)backup).Color_Index0;
                Forms.Win11Colors.C3.BackColor = ((Windows10x)backup).Color_Index1;
                Forms.Win11Colors.C4.BackColor = ((Windows10x)backup).Color_Index4;
                Forms.Win11Colors.C5.BackColor = ((Windows10x)backup).Color_Index3;
                Forms.Win11Colors.C6.BackColor = ((Windows10x)backup).Color_Index2;
                Forms.Win11Colors.C7.BackColor = ((Windows10x)backup).StartMenu_Accent;
                Forms.Win11Colors.C8.BackColor = ((Windows10x)backup).Color_Index6;
                Forms.Win11Colors.C9.BackColor = ((Windows10x)backup).Color_Index7;
                Forms.Win11Colors.windowsDesktop1.TitlebarColor_Active = ((Windows10x)backup).Titlebar_Active;
                Forms.Win11Colors.windowsDesktop1.TitlebarColor_Inactive = ((Windows10x)backup).Titlebar_Inactive;
                Forms.Win11Colors.windowsDesktop1.Color1 = ((Windows10x)backup).Color_Index5;
                Forms.Win11Colors.windowsDesktop1.Color2 = ((Windows10x)backup).Color_Index0;
                Forms.Win11Colors.windowsDesktop1.Color3 = ((Windows10x)backup).Color_Index1;
                Forms.Win11Colors.windowsDesktop1.Color4 = ((Windows10x)backup).Color_Index4;
                Forms.Win11Colors.windowsDesktop1.Color5 = ((Windows10x)backup).Color_Index3;
                Forms.Win11Colors.windowsDesktop1.Color6 = ((Windows10x)backup).Color_Index2;
                Forms.Win11Colors.windowsDesktop1.Color7 = ((Windows10x)backup).StartMenu_Accent;
                Forms.Win11Colors.windowsDesktop1.Color8 = ((Windows10x)backup).Color_Index6;
                Forms.Win11Colors.windowsDesktop1.Color9 = ((Windows10x)backup).Color_Index7;
            }

            else if (Application.OpenForms.OfType<Win10Colors>().Count() > 0)
            {
                Forms.Win10Colors.TActive.BackColor = ((Windows10x)backup).Titlebar_Active;
                Forms.Win10Colors.TInactive.BackColor = ((Windows10x)backup).Titlebar_Inactive;
                Forms.Win10Colors.C1.BackColor = ((Windows10x)backup).Color_Index5;
                Forms.Win10Colors.C2.BackColor = ((Windows10x)backup).Color_Index0;
                Forms.Win10Colors.C3.BackColor = ((Windows10x)backup).Color_Index1;
                Forms.Win10Colors.C4.BackColor = ((Windows10x)backup).Color_Index4;
                Forms.Win10Colors.C5.BackColor = ((Windows10x)backup).Color_Index3;
                Forms.Win10Colors.C6.BackColor = ((Windows10x)backup).Color_Index2;
                Forms.Win10Colors.C7.BackColor = ((Windows10x)backup).StartMenu_Accent;
                Forms.Win10Colors.C8.BackColor = ((Windows10x)backup).Color_Index6;
                Forms.Win10Colors.C9.BackColor = ((Windows10x)backup).Color_Index7;
                Forms.Win10Colors.windowsDesktop1.TitlebarColor_Active = ((Windows10x)backup).Titlebar_Active;
                Forms.Win10Colors.windowsDesktop1.TitlebarColor_Inactive = ((Windows10x)backup).Titlebar_Inactive;
                Forms.Win10Colors.windowsDesktop1.Color1 = ((Windows10x)backup).Color_Index5;
                Forms.Win10Colors.windowsDesktop1.Color2 = ((Windows10x)backup).Color_Index0;
                Forms.Win10Colors.windowsDesktop1.Color3 = ((Windows10x)backup).Color_Index1;
                Forms.Win10Colors.windowsDesktop1.Color4 = ((Windows10x)backup).Color_Index4;
                Forms.Win10Colors.windowsDesktop1.Color5 = ((Windows10x)backup).Color_Index3;
                Forms.Win10Colors.windowsDesktop1.Color6 = ((Windows10x)backup).Color_Index2;
                Forms.Win10Colors.windowsDesktop1.Color7 = ((Windows10x)backup).StartMenu_Accent;
                Forms.Win10Colors.windowsDesktop1.Color8 = ((Windows10x)backup).Color_Index6;
                Forms.Win10Colors.windowsDesktop1.Color9 = ((Windows10x)backup).Color_Index7;
            }

            else if (Application.OpenForms.OfType<Win81Colors>().Count() > 0)
            {
                Forms.Win81Colors.ColorizationColor_pick.BackColor = ((Windows81)backup).ColorizationColor;
                Forms.Win81Colors.personalcls_background_pick.BackColor = ((Windows81)backup).PersonalColors_Background;
                Forms.Win81Colors.accent_pick.BackColor = ((Windows81)backup).AccentColor;
                Forms.Win81Colors.personalcolor_accent_pick.BackColor = ((Windows81)backup).PersonalColors_Accent;
                Forms.Win81Colors.start_pick.BackColor = ((Windows81)backup).StartColor;
            }

            else if (Application.OpenForms.OfType<Win8Colors>().Count() > 0)
            {
                Forms.Win8Colors.ColorizationColor_pick.BackColor = ((Windows8)backup).ColorizationColor;
            }

            else if (Application.OpenForms.OfType<Win7Colors>().Count() > 0)
            {
                Forms.Win7Colors.ColorizationColor_pick.BackColor = ((Windows7)backup).ColorizationColor;
                Forms.Win7Colors.ColorizationAfterglow_pick.BackColor = ((Windows7)backup).ColorizationAfterglow;
                Forms.Win7Colors.windowsDesktop1.TitlebarColor_Active = ((Windows7)backup).ColorizationColor;
                Forms.Win7Colors.windowsDesktop1.TitlebarColor_Inactive = ((Windows7)backup).ColorizationColor;
                Forms.Win7Colors.windowsDesktop1.AfterGlowColor_Active = ((Windows7)backup).ColorizationAfterglow;
                Forms.Win7Colors.windowsDesktop1.AfterGlowColor_Inactive = ((Windows7)backup).ColorizationAfterglow;
            }

            else if (Application.OpenForms.OfType<WinVistaColors>().Count() > 0)
            {
                Forms.WinVistaColors.ColorizationColor_pick.BackColor = ((Windows7)backup).ColorizationColor;
                Forms.WinVistaColors.windowsDesktop1.TitlebarColor_Active = ((Windows7)backup).ColorizationColor;
                Forms.WinVistaColors.windowsDesktop1.TitlebarColor_Inactive = ((Windows7)backup).ColorizationColor;
            }

            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void trackBarX1_ValueChanged(object sender, EventArgs e)
        {
            // Update the colors when the trackbar value changes.
            GetColors();
        }

        private void toggle1_CheckedChanged(object sender, EventArgs e)
        {
            GetColors();
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // Undo the color changes and restore from a saved action.
            actions?.ElementAt(listBox1.SelectedIndex)?.Invoke();
        }
    }
}