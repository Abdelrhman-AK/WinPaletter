using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.Assets;
using WinPaletter.Theme.Structures;
using WinPaletter.UI.Controllers;
using WinPaletter.UI.WP;

namespace WinPaletter
{
    /// <summary>
    /// This class is responsible for generating a color palette from an image.
    /// </summary>
    public partial class PaletteGenerateFromImage
    {
        /// <summary>
        /// List of colors generated from the image.
        /// </summary>
        private readonly List<Color> Colors_List = [];

        /// <summary>
        /// Backup of the current theme colors.
        /// </summary>
        private object backup;

        /// <summary>
        /// List of actions done to be used as a history.
        /// </summary>
        private readonly List<Action> actions = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="PaletteGenerateFromImage"/> class.
        /// </summary>
        public PaletteGenerateFromImage()
        {
            InitializeComponent();
        }

        private void PaletteGenerateFromImage_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            TextBox1.Text = Program.TM.Wallpaper.ImageFile;

            // Clear the list of actions and the list box.
            actions.Clear();
            listBox1.Items.Clear();

            listBox1.Font = Fonts.ConsoleMedium;

            // Backup the current theme colors.

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
                    ColorizationColor = Forms.Win7Colors.ColorizationColor_pick.BackColor,
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
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            // If the radio button is checked, get the colors from the wallpaper.
            if (((RadioImage)sender).Checked)
            {
                groupBox6.Enabled = true;
                GroupBox4.Enabled = true;
                Separator1.Enabled = true;
                GroupBox1.Enabled = true;
                groupBox5.Enabled = true;
                AlertBox1.Enabled = true;
                Button1.Enabled = true;
                groupBox2.Enabled = false;

                GetColors(Program.Wallpaper);
            }
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            // If the radio button is checked, get the colors from the wallpaper.
            if (((RadioImage)sender).Checked)
            {
                groupBox6.Enabled = true;
                GroupBox4.Enabled = true;
                Separator1.Enabled = true;
                GroupBox1.Enabled = true;
                groupBox5.Enabled = true;
                AlertBox1.Enabled = true;
                Button1.Enabled = true;
                groupBox2.Enabled = true;

                GetColors(BitmapMgr.Load(TextBox1.Text));
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            // If the radio button is checked, get the colors from the image.
            if (RadioButton2.Checked) GetColors(BitmapMgr.Load(TextBox1.Text));
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            // Open the file dialog to select an image.
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.Images, Title = Program.Lang.Strings.Extensions.OpenImages })
            {
                if (dlg.ShowDialog() == DialogResult.OK) TextBox1.Text = dlg.FileName;
            }
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (((UI.WP.RadioButton)sender).Checked)
            {
                if (RadioButton1.Checked)
                {
                    GetColors(Program.Wallpaper);
                }
                else
                {
                    GetColors(BitmapMgr.Load(TextBox1.Text));
                }
            }
        }

        /// <summary>
        /// Get the colors from the image.
        /// </summary>
        /// <param name="Source"></param>
        public void GetColors(Bitmap Source)
        {
            // Clear the list of colors and the color items.
            foreach (ColorItem ctrl in ImgPaletteContainer.Controls.OfType<ColorItem>()) ctrl?.Dispose();
            ImgPaletteContainer.Controls.Clear();

            // If the source is not null, get the colors from the image.
            if (Source is not null)
            {
                // Get the thumbnail of the image to speed up the process.
                Source = (Bitmap)Source.GetThumbnailImage(Forms.Win11Colors.windowsDesktop1.Width, Forms.Win11Colors.windowsDesktop1.Height, null, IntPtr.Zero);

                // Clear the list of colors.
                Colors_List.Clear();

                // Add the colors to the list of colors.
                foreach (Color C in Source.ToPalette(Math.Max(13, Trackbar1.Value), Trackbar2.Value, ignoreWhiteColorsToggle.Checked))
                {
                    // Add the color to the list of colors.
                    if (RadioButton3.Checked)
                    {
                        Colors_List.Add(Color.FromArgb(255, C));
                    }

                    // Add lighter colors to the list of colors.
                    else if (RadioButton5.Checked)
                    {
                        Colors_List.Add(Color.FromArgb(255, C).Light());
                    }

                    // Add lightest colors to the list of colors.
                    else if (RadioButton4.Checked)
                    {
                        Colors_List.Add(Color.FromArgb(255, C).LightLight());
                    }

                    // Add darker colors to the list of colors.
                    else if (RadioButton6.Checked)
                    {
                        Colors_List.Add(Color.FromArgb(255, C).Dark());
                    }

                    // Add darkest colors to the list of colors.
                    else if (RadioButton7.Checked)
                    {
                        Colors_List.Add(Color.FromArgb(255, C).Dark(0.8f));
                    }

                    // Add the original colors to the list of colors.
                    else
                    {
                        Colors_List.Add(Color.FromArgb(255, C));
                    }
                }

                // Sort the list of colors.
                Colors_List.Sort(new RGBColorComparer());

                // Add the colors to the color items as colors controls.
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
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (Colors_List != null && Colors_List.Count > 0)
            {
                // Generate a random list of unique numbers.
                List<int> arr = 0.GetUniqueRandomNumbers(Colors_List.Count);

                // Apply the colors to the theme and distribute them to the forms randomly.

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

                // Hide a tooltip to inform the user that there are no colors to apply.
                Program.ToolTip.ToolTipText = Program.Lang.Strings.Tips.PaletteSourceGeneration;
                Program.ToolTip.ToolTipTitle = Program.Lang.Strings.General.Error;
                Program.ToolTip.Image = Notifications.Error;

                Point location = new(0, ((Control)sender).Height + 2);

                Program.ToolTip.Show((Control)sender, Program.ToolTip.ToolTipTitle, Program.ToolTip.ToolTipText, Program.ToolTip.Image, location, 5000);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
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
                Forms.WinVistaColors.ColorizationColor_pick.BackColor = ((WindowsVista)backup).ColorizationColor;
                Forms.WinVistaColors.windowsDesktop1.TitlebarColor_Active = ((WindowsVista)backup).ColorizationColor;
                Forms.WinVistaColors.windowsDesktop1.TitlebarColor_Inactive = ((WindowsVista)backup).ColorizationColor;
            }

            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Apply the selected action from the list of actions.
            actions?.ElementAt(listBox1.SelectedIndex)?.Invoke();
        }

        private void trackBarX1_ValueChanged(object sender, EventArgs e)
        {
            // Update colors number to generate.

            if (RadioButton1.Checked)
            {
                GetColors(Program.Wallpaper);
            }
            else
            {
                GetColors(BitmapMgr.Load(TextBox1.Text));
            }
        }

        private void trackBarX2_ValueChanged(object sender, EventArgs e)
        {
            // Update colors quality to generate.

            if (RadioButton1.Checked)
            {
                GetColors(Program.Wallpaper);
            }
            else
            {
                GetColors(BitmapMgr.Load(TextBox1.Text));
            }
        }

        private void ignoreWhiteColorsToggle_CheckedChanged(object sender, EventArgs e)
        {
            // Update colors to include or exclude inverted colors.
            // If the radio button is checked, get the colors from the wallpaper, else get the colors from the image.
            if (RadioButton1.Checked)
            {
                GetColors(Program.Wallpaper);
            }
            else
            {
                GetColors(BitmapMgr.Load(TextBox1.Text));
            }
        }
    }
}