using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.Assets;
using WinPaletter.Theme;
using WinPaletter.UI.WP;
using static WinPaletter.Theme.Manager;

namespace WinPaletter
{
    public partial class AccessibilityEditor
    {
        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Links.Wiki.HighContrast);
        }

        public AccessibilityEditor()
        {
            InitializeComponent();
        }

        private void LoadFromWPTH(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new() { Filter = Program.Filters.WinPaletterTheme, Title = Program.Lang.Strings.Extensions.OpenWinPaletterTheme })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (Manager TMx = new(Source.File, dlg.FileName))
                    {
                        LoadFromTM(TMx);
                    }
                }
            }
        }

        private void LoadFromCurrent(object sender, EventArgs e)
        {
            Manager TMx = new(Source.Registry);
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
            Close();
        }

        private void Apply(object sender, EventArgs e)
        {
            if (Program.Settings.AspectsControl.Enabled && !Program.Settings.AspectsControl.Accessibility)
            {
                MsgBox(Program.Lang.Strings.Aspects.Disabled_Apply_0, MessageBoxButtons.OK, MessageBoxIcon.Warning, Program.Lang.Strings.Aspects.Disabled_Apply_1);
                return;
            }

            Cursor = Cursors.WaitCursor;

            using (Manager TMx = new(Source.Registry))
            {
                if (Program.Settings.BackupTheme.Enabled && Program.Settings.BackupTheme.AutoBackupOnApplySingleAspect)
                {
                    string filename = Program.GetUniqueFileName($"{Program.Settings.BackupTheme.BackupPath}\\OnAspectApply", $"{TMx.Info.ThemeName}_{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.wpth");
                    TMx.Save(Source.File, filename);
                }

                ApplyToTM(TMx);
                ApplyToTM(Program.TM);
                ApplyToTM(Program.TM_Original);
                TMx.Accessibility.Apply();
            }

            Cursor = Cursors.Default;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            PictureBox33.Image?.Dispose();
            PictureBox32.Image?.Dispose();
        }

        private void HighContrastEditor_Load(object sender, EventArgs e)
        {
            DesignerData data = new(this)
            {
                AspectName = Program.Lang.Strings.Aspects.Accessibility,
                Enabled = Program.TM.Accessibility.Enabled,
                Import_theme = false,
                Import_msstyles = false,
                GeneratePalette = false,
                GenerateMSTheme = false,
                Import_preset = false,
                CanSwitchMode = false,

                OnLoadIntoCurrentTheme = LoadIntoCurrentTheme,
                OnApply = Apply,
                OnImportFromDefault = LoadFromDefault,
                OnImportFromWPTH = LoadFromWPTH,
                OnImportFromCurrentApplied = LoadFromCurrent,
            };

            LoadData(data);

            LoadFromTM(Program.TM);

        }

        public void LoadFromTM(Manager TM)
        {
            AspectEnabled = TM.Accessibility.Enabled;
            highContrastToggle.Checked = TM.Accessibility.HighContrast;

            if (!TM.Accessibility.ColorFilter_Enabled)
            {
                RadioImage1.Checked = true;
            }
            else
            {
                switch (TM.Accessibility.ColorFilter)
                {
                    case Theme.Structures.Accessibility.ColorFilters.Grayscale:
                        {
                            RadioImage5.Checked = true;
                            break;
                        }

                    case Theme.Structures.Accessibility.ColorFilters.Inverted:
                        {
                            RadioImage7.Checked = true;
                            break;
                        }

                    case Theme.Structures.Accessibility.ColorFilters.GrayscaleInverted:
                        {
                            RadioImage6.Checked = true;
                            break;
                        }

                    case Theme.Structures.Accessibility.ColorFilters.RedGreen_deuteranopia:
                        {
                            RadioImage2.Checked = true;
                            break;
                        }

                    case Theme.Structures.Accessibility.ColorFilters.RedGreen_protanopia:
                        {
                            RadioImage3.Checked = true;
                            break;
                        }

                    case Theme.Structures.Accessibility.ColorFilters.BlueYellow:
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
        }

        public void ApplyToTM(Manager TM)
        {
            TM.Accessibility.Enabled = AspectEnabled;
            TM.Accessibility.HighContrast = highContrastToggle.Checked;

            if (RadioImage1.Checked)
            {
                TM.Accessibility.ColorFilter_Enabled = false;
            }

            else if (RadioImage5.Checked)
            {
                TM.Accessibility.ColorFilter_Enabled = true;
                TM.Accessibility.ColorFilter = Theme.Structures.Accessibility.ColorFilters.Grayscale;
            }

            else if (RadioImage7.Checked)
            {
                TM.Accessibility.ColorFilter_Enabled = true;
                TM.Accessibility.ColorFilter = Theme.Structures.Accessibility.ColorFilters.Inverted;
            }

            else if (RadioImage6.Checked)
            {
                TM.Accessibility.ColorFilter_Enabled = true;
                TM.Accessibility.ColorFilter = Theme.Structures.Accessibility.ColorFilters.GrayscaleInverted;
            }

            else if (RadioImage2.Checked)
            {
                TM.Accessibility.ColorFilter_Enabled = true;
                TM.Accessibility.ColorFilter = Theme.Structures.Accessibility.ColorFilters.RedGreen_deuteranopia;
            }

            else if (RadioImage3.Checked)
            {
                TM.Accessibility.ColorFilter_Enabled = true;
                TM.Accessibility.ColorFilter = Theme.Structures.Accessibility.ColorFilters.RedGreen_protanopia;
            }

            else if (RadioImage4.Checked)
            {
                TM.Accessibility.ColorFilter_Enabled = true;
                TM.Accessibility.ColorFilter = Theme.Structures.Accessibility.ColorFilters.BlueYellow;
            }
        }

        private void RadioImage1_CheckedChanged(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(((RadioImage)sender).Checked))
            {
                PictureBox33.Image = Accessibility_Vision.CF_Img_Normal;
                PictureBox32.Image = Accessibility_Vision.CF_Pie_Normal;

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

        private void RadioImage7_CheckedChanged(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(((RadioImage)sender).Checked))
            {
                PictureBox33.Image = Accessibility_Vision.CF_Img_Normal.Invert();
                PictureBox32.Image = Accessibility_Vision.CF_Pie_Normal.Invert();

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

        private void RadioImage5_CheckedChanged(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(((RadioImage)sender).Checked))
            {
                PictureBox33.Image = Accessibility_Vision.CF_Img_Grayscale;
                PictureBox32.Image = Accessibility_Vision.CF_Pie_Grayscale;

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

        private void RadioImage6_CheckedChanged(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(((RadioImage)sender).Checked))
            {
                PictureBox33.Image = Accessibility_Vision.CF_Img_Grayscale.Invert();
                PictureBox32.Image = Accessibility_Vision.CF_Pie_Grayscale.Invert();

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

        private void RadioImage2_CheckedChanged(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(((RadioImage)sender).Checked))
            {
                PictureBox33.Image = Accessibility_Vision.CF_Img_Red_green_green_weak_deuteranopia;
                PictureBox32.Image = Accessibility_Vision.CF_Pie_Red_green_green_weak_deuteranopia;

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

        private void RadioImage3_CheckedChanged(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(((RadioImage)sender).Checked))
            {
                PictureBox33.Image = Accessibility_Vision.CF_Img_Red_green_red_weak_protanopia;
                PictureBox32.Image = Accessibility_Vision.CF_Pie_Red_green_red_weak_protanopia;

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

        private void RadioImage4_CheckedChanged(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(((RadioImage)sender).Checked))
            {
                PictureBox33.Image = Accessibility_Vision.CF_Img_Blue_yellow_tritanopia;
                PictureBox32.Image = Accessibility_Vision.CF_Pie_Blue_yellow__tritanopia;

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

        private void button25_Click(object sender, EventArgs e)
        {
            RadioImage1.Checked = true;
        }
    }
}