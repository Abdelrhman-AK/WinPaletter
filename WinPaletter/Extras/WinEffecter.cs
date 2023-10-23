using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter
{

    public partial class WinEffecter
    {
        public WinEffecter()
        {
            InitializeComponent();
        }
        private void WinEffecter_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            ApplyStyle(this);
            Button12.Image = Forms.MainFrm.Button20.Image.Resize(16, 16);
            ApplyFromTM(Program.TM);
            SetClassicButtonColors(Program.TM, ButtonR1);

        }

        public void ApplyFromTM(Theme.Manager TM)
        {
            {
                ref Theme.Structures.WinEffects Effects = ref TM.WindowsEffects;
                EffectsEnabled.Checked = Effects.Enabled;
                CheckBox1.Checked = Effects.WindowAnimation;
                CheckBox2.Checked = Effects.WindowShadow;
                CheckBox3.Checked = Effects.WindowUIEffects;
                CheckBox6.Checked = Effects.MenuAnimation;
                CheckBox27.Checked = Effects.AnimateControlsInsideWindow;
                if (Effects.MenuFade == Theme.Structures.WinEffects.MenuAnimType.Fade)
                    ComboBox1.SelectedIndex = 0;
                else
                    ComboBox1.SelectedIndex = 1;
                CheckBox5.Checked = Effects.MenuSelectionFade;
                Trackbar1.Value = (int)Effects.MenuShowDelay;
                CheckBox8.Checked = Effects.ComboBoxAnimation;
                CheckBox7.Checked = Effects.ListBoxSmoothScrolling;
                CheckBox9.Checked = Effects.TooltipAnimation;
                if (Effects.TooltipFade == Theme.Structures.WinEffects.MenuAnimType.Fade)
                    ComboBox2.SelectedIndex = 0;
                else
                    ComboBox2.SelectedIndex = 1;
                CheckBox4.Checked = Effects.IconsShadow;
                CheckBox10.Checked = Effects.IconsDesktopTranslSel;
                CheckBox11.Checked = Effects.ShowWinContentDrag;
                CheckBox12.Checked = Effects.KeyboardUnderline;
                Trackbar5.Value = Effects.NotificationDuration;
                Trackbar2.Value = (int)Effects.FocusRectWidth;
                Trackbar3.Value = (int)Effects.FocusRectHeight;
                Trackbar4.Value = (int)Effects.Caret;
                CheckBox13.Checked = Effects.AWT_Enabled;
                CheckBox14.Checked = Effects.AWT_BringActivatedWindowToTop;
                Trackbar6.Value = Effects.AWT_Delay;
                CheckBox15.Checked = Effects.SnapCursorToDefButton;
                CheckBox16.Checked = Effects.Win11ClassicContextMenu;
                CheckBox17.Checked = Effects.BalloonNotifications;
                CheckBox20.Checked = Effects.SysListView32;
                CheckBox19.Checked = Effects.ShowSecondsInSystemClock;
                CheckBox18.Checked = Effects.PaintDesktopVersion;
                CheckBox21.Checked = Effects.ShakeToMinimize;
                CheckBox22.Checked = Effects.Win11BootDots;
                CheckBox26.Checked = Effects.ClassicVolMixer;

                RadioButton1.Checked = Effects.Win11ExplorerBar == Theme.Structures.WinEffects.ExplorerBar.Default;
                RadioButton2.Checked = Effects.Win11ExplorerBar == Theme.Structures.WinEffects.ExplorerBar.Ribbon;
                RadioButton3.Checked = Effects.Win11ExplorerBar == Theme.Structures.WinEffects.ExplorerBar.Bar;
                CheckBox23.Checked = Effects.DisableNavBar;
                CheckBox24.Checked = Effects.AutoHideScrollBars;
                CheckBox25.Checked = Effects.FullScreenStartMenu;

                if (!Effects.ColorFilter_Enabled)
                {
                    RadioImage1.Checked = true;
                }
                else
                {
                    switch (Effects.ColorFilter)
                    {
                        case Theme.Structures.WinEffects.ColorFilters.Grayscale:
                            {
                                RadioImage5.Checked = true;
                                break;
                            }

                        case Theme.Structures.WinEffects.ColorFilters.Inverted:
                            {
                                RadioImage7.Checked = true;
                                break;
                            }

                        case Theme.Structures.WinEffects.ColorFilters.GrayscaleInverted:
                            {
                                RadioImage6.Checked = true;
                                break;
                            }

                        case Theme.Structures.WinEffects.ColorFilters.RedGreen_deuteranopia:
                            {
                                RadioImage2.Checked = true;
                                break;
                            }

                        case Theme.Structures.WinEffects.ColorFilters.RedGreen_protanopia:
                            {
                                RadioImage3.Checked = true;
                                break;
                            }

                        case Theme.Structures.WinEffects.ColorFilters.BlueYellow:
                            {
                                RadioImage4.Checked = true;
                                break;
                            }

                        default:
                            {
                                RadioImage1.Checked = true;
                                break;
                            }

                    }
                }

                Panel2.Width = (int)Effects.Caret;
            }


        }

        public void ApplyToTM(Theme.Manager TM)
        {
            {
                ref Theme.Structures.WinEffects Effects = ref TM.WindowsEffects;
                Effects.Enabled = EffectsEnabled.Checked;
                Effects.WindowAnimation = CheckBox1.Checked;
                Effects.WindowShadow = CheckBox2.Checked;
                Effects.WindowUIEffects = CheckBox3.Checked;
                Effects.AnimateControlsInsideWindow = CheckBox27.Checked;
                Effects.MenuAnimation = CheckBox6.Checked;
                if (ComboBox1.SelectedIndex == 0)
                    Effects.MenuFade = Theme.Structures.WinEffects.MenuAnimType.Fade;
                else
                    Effects.MenuFade = Theme.Structures.WinEffects.MenuAnimType.Scroll;
                Effects.MenuSelectionFade = CheckBox5.Checked;
                Effects.MenuShowDelay = (uint)Trackbar1.Value;
                Effects.ComboBoxAnimation = CheckBox8.Checked;
                Effects.ListBoxSmoothScrolling = CheckBox7.Checked;
                Effects.TooltipAnimation = CheckBox9.Checked;
                if (ComboBox2.SelectedIndex == 0)
                    Effects.TooltipFade = Theme.Structures.WinEffects.MenuAnimType.Fade;
                else
                    Effects.TooltipFade = Theme.Structures.WinEffects.MenuAnimType.Scroll;
                Effects.IconsShadow = CheckBox4.Checked;
                Effects.IconsDesktopTranslSel = CheckBox10.Checked;
                Effects.ShowWinContentDrag = CheckBox11.Checked;
                Effects.KeyboardUnderline = CheckBox12.Checked;
                Effects.NotificationDuration = Trackbar5.Value;
                Effects.FocusRectWidth = (uint)Trackbar2.Value;
                Effects.FocusRectHeight = (uint)Trackbar3.Value;
                Effects.Caret = (uint)Trackbar4.Value;
                Effects.AWT_Enabled = CheckBox13.Checked;
                Effects.AWT_BringActivatedWindowToTop = CheckBox14.Checked;
                Effects.AWT_Delay = Trackbar6.Value;
                Effects.SnapCursorToDefButton = CheckBox15.Checked;
                Effects.Win11ClassicContextMenu = CheckBox16.Checked;
                Effects.BalloonNotifications = CheckBox17.Checked;
                Effects.SysListView32 = CheckBox20.Checked;
                Effects.ShowSecondsInSystemClock = CheckBox19.Checked;
                Effects.PaintDesktopVersion = CheckBox18.Checked;
                Effects.ShakeToMinimize = CheckBox21.Checked;
                Effects.Win11BootDots = CheckBox22.Checked;
                Effects.ClassicVolMixer = CheckBox26.Checked;

                if (RadioButton1.Checked)
                {
                    Effects.Win11ExplorerBar = Theme.Structures.WinEffects.ExplorerBar.Default;
                }

                else if (RadioButton2.Checked)
                {
                    Effects.Win11ExplorerBar = Theme.Structures.WinEffects.ExplorerBar.Ribbon;
                }

                else if (RadioButton3.Checked)
                {
                    Effects.Win11ExplorerBar = Theme.Structures.WinEffects.ExplorerBar.Bar;

                }

                Effects.DisableNavBar = CheckBox23.Checked;
                Effects.AutoHideScrollBars = CheckBox24.Checked;
                Effects.FullScreenStartMenu = CheckBox25.Checked;

                if (RadioImage1.Checked)
                {
                    Effects.ColorFilter_Enabled = false;
                }

                else if (RadioImage5.Checked)
                {
                    Effects.ColorFilter_Enabled = true;
                    Effects.ColorFilter = Theme.Structures.WinEffects.ColorFilters.Grayscale;
                }

                else if (RadioImage7.Checked)
                {
                    Effects.ColorFilter_Enabled = true;
                    Effects.ColorFilter = Theme.Structures.WinEffects.ColorFilters.Inverted;
                }

                else if (RadioImage6.Checked)
                {
                    Effects.ColorFilter_Enabled = true;
                    Effects.ColorFilter = Theme.Structures.WinEffects.ColorFilters.GrayscaleInverted;
                }

                else if (RadioImage2.Checked)
                {
                    Effects.ColorFilter_Enabled = true;
                    Effects.ColorFilter = Theme.Structures.WinEffects.ColorFilters.RedGreen_deuteranopia;
                }

                else if (RadioImage3.Checked)
                {
                    Effects.ColorFilter_Enabled = true;
                    Effects.ColorFilter = Theme.Structures.WinEffects.ColorFilters.RedGreen_protanopia;
                }

                else if (RadioImage4.Checked)
                {
                    Effects.ColorFilter_Enabled = true;
                    Effects.ColorFilter = Theme.Structures.WinEffects.ColorFilters.BlueYellow;

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
            using (var _Def = Theme.Default.Get(Program.PreviewStyle))
            {
                ApplyFromTM(_Def);
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var TMx = new Theme.Manager(Theme.Manager.Source.Registry);
            ApplyToTM(TMx);
            ApplyToTM(Program.TM);
            Forms.MainFrm.ApplyColorsToElements(TMx);
            TMx.WindowsEffects.Apply();
            TMx.Win32.Broadcast_UPM_ToDefUsers();
            TMx.Dispose();
            Cursor = Cursors.Default;
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            ApplyToTM(Program.TM);
            Forms.MainFrm.ApplyColorsToElements(Program.TM);
            Forms.MainFrm.ApplyStylesToElements(Program.TM, false);
            Cursor = Cursors.Default;
            Close();
        }

        private void EffectsEnabled_CheckedChanged(object sender, EventArgs e)
        {
            checker_img.Image = Conversions.ToBoolean(((UI.WP.Toggle)sender).Checked) ? Properties.Resources.checker_enabled : Properties.Resources.checker_disabled;
        }

        private void MD_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar1.Maximum), Trackbar1.Minimum).ToString();
            Trackbar1.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Trackbar1_Scroll(object sender)
        {
            MD.Text = ((UI.WP.Trackbar)sender).Value.ToString();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar5.Maximum), Trackbar5.Minimum).ToString();
            Trackbar5.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar2.Maximum), Trackbar2.Minimum).ToString();
            Trackbar2.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar3.Maximum), Trackbar3.Minimum).ToString();
            Trackbar3.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar4.Maximum), Trackbar4.Minimum).ToString();
            Trackbar4.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar6.Maximum), Trackbar6.Minimum).ToString();
            Trackbar6.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Trackbar5_Scroll(object sender)
        {
            Button4.Text = ((UI.WP.Trackbar)sender).Value.ToString();
        }

        private void Trackbar2_Scroll(object sender)
        {
            Button1.Text = ((UI.WP.Trackbar)sender).Value.ToString();
            ButtonR1.FocusRectWidth = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
            ButtonR1.Refresh();
        }

        private void Trackbar3_Scroll(object sender)
        {
            Button2.Text = ((UI.WP.Trackbar)sender).Value.ToString();
            ButtonR1.FocusRectHeight = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
            ButtonR1.Refresh();
        }

        private void Trackbar4_Scroll(object sender)
        {
            Button3.Text = ((UI.WP.Trackbar)sender).Value.ToString();
            Panel2.Width = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
        }

        private void Trackbar6_Scroll(object sender)
        {
            Button5.Text = ((UI.WP.Trackbar)sender).Value.ToString();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Panel2.Visible = !Panel2.Visible;
        }

        private void RadioImage1_CheckedChanged(object sender)
        {
            if (Conversions.ToBoolean(((UI.WP.RadioImage)sender).Checked))
            {
                PictureBox33.Image = Properties.Resources.CF_Img_Normal;
                PictureBox32.Image = Properties.Resources.CF_Pie_Normal;

                R1.BackColor = Color.FromArgb(204, 50, 47);
                R2.BackColor = Color.FromArgb(233, 80, 63);
                R3.BackColor = Color.FromArgb(239, 142, 133);

                O1.BackColor = Color.FromArgb(220, 96, 44);
                O2.BackColor = Color.FromArgb(239, 153, 58);
                O3.BackColor = Color.FromArgb(247, 193, 114);

                Y1.BackColor = Color.FromArgb(231, 181, 64);
                Y2.BackColor = Color.FromArgb(248, 205, 72);
                Y3.BackColor = Color.FromArgb(250, 224, 121);

                G1.BackColor = Color.FromArgb(57, 122, 47);
                G2.BackColor = Color.FromArgb(117, 213, 113);
                G3.BackColor = Color.FromArgb(163, 228, 166);

                B1.BackColor = Color.FromArgb(29, 65, 211);
                B2.BackColor = Color.FromArgb(55, 119, 245);
                B3.BackColor = Color.FromArgb(118, 170, 248);

                P1.BackColor = Color.FromArgb(165, 39, 200);
                P2.BackColor = Color.FromArgb(195, 156, 219);
                P3.BackColor = Color.FromArgb(217, 195, 233);
            }
        }

        private void RadioImage5_CheckedChanged(object sender)
        {
            if (Conversions.ToBoolean(((UI.WP.RadioImage)sender).Checked))
            {
                PictureBox33.Image = Properties.Resources.CF_Img_Grayscale;
                PictureBox32.Image = Properties.Resources.CF_Pie_Grayscale;

                R1.BackColor = Color.FromArgb(93, 93, 93);
                R2.BackColor = Color.FromArgb(122, 122, 122);
                R3.BackColor = Color.FromArgb(169, 169, 169);

                O1.BackColor = Color.FromArgb(126, 126, 126);
                O2.BackColor = Color.FromArgb(166, 166, 166);
                O3.BackColor = Color.FromArgb(200, 200, 200);

                Y1.BackColor = Color.FromArgb(183, 182, 183);
                Y2.BackColor = Color.FromArgb(202, 202, 202);
                Y3.BackColor = Color.FromArgb(220, 220, 220);

                G1.BackColor = Color.FromArgb(93, 93, 93);
                G2.BackColor = Color.FromArgb(172, 172, 172);
                G3.BackColor = Color.FromArgb(202, 202, 202);

                B1.BackColor = Color.FromArgb(70, 70, 70);
                B2.BackColor = Color.FromArgb(113, 113, 113);
                B3.BackColor = Color.FromArgb(163, 163, 163);

                P1.BackColor = Color.FromArgb(93, 93, 93);
                P2.BackColor = Color.FromArgb(175, 174, 175);
                P3.BackColor = Color.FromArgb(206, 206, 206);
            }
        }

        private void RadioImage7_CheckedChanged(object sender)
        {
            if (Conversions.ToBoolean(((UI.WP.RadioImage)sender).Checked))
            {
                PictureBox33.Image = Properties.Resources.CF_Img_Normal.Invert();
                PictureBox32.Image = Properties.Resources.CF_Pie_Normal.Invert();

                R1.BackColor = Color.FromArgb(53, 208, 211);
                R2.BackColor = Color.FromArgb(28, 174, 193);
                R3.BackColor = Color.FromArgb(22, 111, 120);

                O1.BackColor = Color.FromArgb(39, 158, 214);
                O2.BackColor = Color.FromArgb(22, 101, 199);
                O3.BackColor = Color.FromArgb(16, 63, 139);

                Y1.BackColor = Color.FromArgb(29, 73, 192);
                Y2.BackColor = Color.FromArgb(15, 52, 184);
                Y3.BackColor = Color.FromArgb(13, 35, 132);

                G1.BackColor = Color.FromArgb(200, 131, 211);
                G2.BackColor = Color.FromArgb(136, 45, 140);
                G3.BackColor = Color.FromArgb(90, 32, 88);

                B1.BackColor = Color.FromArgb(231, 191, 47);
                B2.BackColor = Color.FromArgb(202, 134, 18);
                B3.BackColor = Color.FromArgb(135, 84, 15);

                P1.BackColor = Color.FromArgb(89, 220, 57);
                P2.BackColor = Color.FromArgb(61, 98, 40);
                P3.BackColor = Color.FromArgb(42, 61, 28);
            }
        }

        private void RadioImage6_CheckedChanged(object sender)
        {
            if (Conversions.ToBoolean(((UI.WP.RadioImage)sender).Checked))
            {
                PictureBox33.Image = Properties.Resources.CF_Img_Grayscale.Invert();
                PictureBox32.Image = Properties.Resources.CF_Pie_Grayscale.Invert();

                R1.BackColor = Color.FromArgb(160, 160, 160);
                R2.BackColor = Color.FromArgb(131, 131, 131);
                R3.BackColor = Color.FromArgb(85, 85, 85);

                O1.BackColor = Color.FromArgb(127, 127, 127);
                O2.BackColor = Color.FromArgb(88, 88, 88);
                O3.BackColor = Color.FromArgb(57, 57, 57);

                Y1.BackColor = Color.FromArgb(73, 73, 73);
                Y2.BackColor = Color.FromArgb(55, 55, 55);
                Y3.BackColor = Color.FromArgb(39, 39, 39);

                G1.BackColor = Color.FromArgb(160, 160, 160);
                G2.BackColor = Color.FromArgb(82, 82, 82);
                G3.BackColor = Color.FromArgb(55, 55, 55);

                B1.BackColor = Color.FromArgb(186, 186, 186);
                B2.BackColor = Color.FromArgb(140, 140, 140);
                B3.BackColor = Color.FromArgb(90, 90, 90);

                P1.BackColor = Color.FromArgb(160, 160, 160);
                P2.BackColor = Color.FromArgb(80, 80, 80);
                P3.BackColor = Color.FromArgb(51, 51, 51);
            }
        }

        private void RadioImage2_CheckedChanged(object sender)
        {
            if (Conversions.ToBoolean(((UI.WP.RadioImage)sender).Checked))
            {
                PictureBox33.Image = Properties.Resources.CF_Img_Red_green_green_weak_deuteranopia;
                PictureBox32.Image = Properties.Resources.CF_Pie_Red_green_green_weak_deuteranopia;

                R1.BackColor = Color.FromArgb(255, 50, 20);
                R2.BackColor = Color.FromArgb(255, 80, 35);
                R3.BackColor = Color.FromArgb(255, 142, 113);

                O1.BackColor = Color.FromArgb(255, 96, 22);
                O2.BackColor = Color.FromArgb(255, 153, 42);
                O3.BackColor = Color.FromArgb(255, 193, 103);

                Y1.BackColor = Color.FromArgb(229, 181, 54);
                Y2.BackColor = Color.FromArgb(239, 205, 62);
                Y3.BackColor = Color.FromArgb(241, 224, 114);

                G1.BackColor = Color.FromArgb(16, 122, 58);
                G2.BackColor = Color.FromArgb(60, 213, 130);
                G3.BackColor = Color.FromArgb(124, 228, 176);

                B1.BackColor = Color.FromArgb(40, 65, 218);
                B2.BackColor = Color.FromArgb(50, 119, 255);
                B3.BackColor = Color.FromArgb(111, 170, 255);

                P1.BackColor = Color.FromArgb(255, 39, 172);
                P2.BackColor = Color.FromArgb(224, 156, 210);
                P3.BackColor = Color.FromArgb(234, 195, 227);
            }
        }

        private void RadioImage3_CheckedChanged(object sender)
        {
            if (Conversions.ToBoolean(((UI.WP.RadioImage)sender).Checked))
            {
                PictureBox33.Image = Properties.Resources.CF_Img_Red_green_red_weak_protanopia;
                PictureBox32.Image = Properties.Resources.CF_Pie_Red_green_red_weak_protanopia;

                R1.BackColor = Color.FromArgb(204, 121, 137);
                R2.BackColor = Color.FromArgb(233, 151, 151);
                R3.BackColor = Color.FromArgb(239, 188, 190);

                O1.BackColor = Color.FromArgb(220, 152, 110);
                O2.BackColor = Color.FromArgb(239, 190, 97);
                O3.BackColor = Color.FromArgb(247, 215, 138);

                Y1.BackColor = Color.FromArgb(231, 200, 81);
                Y2.BackColor = Color.FromArgb(248, 219, 84);
                Y3.BackColor = Color.FromArgb(250, 231, 126);

                G1.BackColor = Color.FromArgb(57, 87, 0);
                G2.BackColor = Color.FromArgb(117, 162, 51);
                G3.BackColor = Color.FromArgb(163, 195, 123);

                B1.BackColor = Color.FromArgb(29, 53, 201);
                B2.BackColor = Color.FromArgb(55, 93, 215);
                B3.BackColor = Color.FromArgb(118, 149, 222);

                P1.BackColor = Color.FromArgb(165, 105, 255);
                P2.BackColor = Color.FromArgb(195, 176, 249);
                P3.BackColor = Color.FromArgb(217, 207, 251);
            }
        }

        private void RadioImage4_CheckedChanged(object sender)
        {
            if (Conversions.ToBoolean(((UI.WP.RadioImage)sender).Checked))
            {
                PictureBox33.Image = Properties.Resources.CF_Img_Blue_yellow_tritanopia;
                PictureBox32.Image = Properties.Resources.CF_Pie_Blue_yellow__tritanopia;

                R1.BackColor = Color.FromArgb(160, 60, 47);
                R2.BackColor = Color.FromArgb(180, 85, 63);
                R3.BackColor = Color.FromArgb(208, 146, 133);

                O1.BackColor = Color.FromArgb(150, 87, 44);
                O2.BackColor = Color.FromArgb(151, 126, 58);
                O3.BackColor = Color.FromArgb(179, 169, 114);

                Y1.BackColor = Color.FromArgb(138, 145, 64);
                Y2.BackColor = Color.FromArgb(145, 161, 72);
                Y3.BackColor = Color.FromArgb(174, 191, 121);

                G1.BackColor = Color.FromArgb(26, 91, 47);
                G2.BackColor = Color.FromArgb(77, 171, 113);
                G3.BackColor = Color.FromArgb(140, 202, 166);

                B1.BackColor = Color.FromArgb(132, 110, 211);
                B2.BackColor = Color.FromArgb(152, 156, 245);
                B3.BackColor = Color.FromArgb(181, 193, 248);

                P1.BackColor = Color.FromArgb(244, 101, 200);
                P2.BackColor = Color.FromArgb(227, 179, 219);
                P3.BackColor = Color.FromArgb(237, 210, 233);
            }
        }

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Properties.Resources.Link_Wiki + "/Edit-Windows-Effects");
        }
    }
}