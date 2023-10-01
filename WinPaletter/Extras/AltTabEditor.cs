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

    public partial class AltTabEditor
    {
        public AltTabEditor()
        {
            InitializeComponent();
        }
        private void AltTabEditor_Load(object sender, EventArgs e)
        {
            this.LoadLanguage();
            WPStyle.ApplyStyle(this);
            Button12.Image = My.MyProject.Forms.MainFrm.Button20.Image.Resize(16, 16);
            ApplyFromCP(My.Env.CP);

            switch (My.Env.PreviewStyle)
            {
                case WindowStyle.W11:
                    {
                        RadioImage1.Image = My.Resources.Native11;
                        break;
                    }

                case WindowStyle.W10:
                    {
                        RadioImage1.Image = My.Resources.Native10;
                        break;
                    }

                case WindowStyle.W81:
                    {
                        RadioImage1.Image = My.Resources.Native8;
                        break;
                    }

                case WindowStyle.W7:
                    {
                        RadioImage1.Image = My.Resources.Native7;
                        break;
                    }

                case WindowStyle.WVista:
                    {
                        RadioImage1.Image = My.Resources.NativeVista;
                        break;
                    }

                default:
                    {
                        RadioImage1.Image = My.Resources.Native11;
                        break;
                    }

            }

            RadioImage2.Image = My.Resources.NativeXP;

            pnl_preview1.BackgroundImage = My.MyProject.Forms.MainFrm.pnl_preview.BackgroundImage;
            Classic_Preview1.BackgroundImage = My.MyProject.Forms.MainFrm.pnl_preview_classic.BackgroundImage;

            SetClassicPanelRaisedRColors(My.Env.CP, PanelRRaised1);
            SetClassicPanelColors(My.Env.CP, PanelR1);

            Panel1.BackColor = My.Env.CP.Win32.Hilight;

            switch (My.Env.PreviewStyle)
            {
                case WindowStyle.W11:
                    {
                        WinElement1.Style = UI.Simulation.WinElement.Styles.AltTab11;
                        WinElement1.DarkMode = !My.Env.CP.Windows11.WinMode_Light;
                        break;
                    }

                case WindowStyle.W10:
                    {
                        WinElement1.Style = UI.Simulation.WinElement.Styles.AltTab10;
                        WinElement1.DarkMode = !My.Env.CP.Windows10.WinMode_Light;
                        break;
                    }

                case WindowStyle.W81:
                    {
                        switch (My.Env.CP.Windows81.Theme)
                        {
                            case CP.Structures.Windows7.Themes.Aero:
                                {
                                    WinElement1.Style = UI.Simulation.WinElement.Styles.AltTab8Aero;
                                    WinElement1.BackColor = My.Env.CP.Windows81.PersonalColors_Background;
                                    WinElement1.Background2 = My.Env.CP.Windows81.PersonalColors_Background;
                                    break;
                                }

                            case CP.Structures.Windows7.Themes.AeroLite:
                                {
                                    WinElement1.Style = UI.Simulation.WinElement.Styles.AltTab8AeroLite;
                                    WinElement1.BackColor = My.Env.CP.Win32.Window;
                                    WinElement1.Background2 = My.Env.CP.Win32.Hilight;
                                    WinElement1.LinkColor = My.Env.CP.Win32.ButtonText;
                                    WinElement1.ForeColor = My.Env.CP.Win32.WindowText;
                                    break;
                                }

                        }

                        break;
                    }

                case WindowStyle.W7:
                    {
                        switch (My.Env.CP.Windows7.Theme)
                        {
                            case CP.Structures.Windows7.Themes.Aero:
                                {
                                    WinElement1.Style = UI.Simulation.WinElement.Styles.AltTab7Aero;
                                    break;
                                }

                            case CP.Structures.Windows7.Themes.AeroOpaque:
                                {
                                    WinElement1.Style = UI.Simulation.WinElement.Styles.AltTab7Opaque;
                                    break;
                                }

                            case CP.Structures.Windows7.Themes.Basic:
                                {
                                    WinElement1.Style = UI.Simulation.WinElement.Styles.AltTab7Basic;
                                    break;
                                }

                        }

                        WinElement1.BackColor = My.Env.CP.Windows7.ColorizationColor;
                        WinElement1.Background2 = My.Env.CP.Windows7.ColorizationAfterglow;
                        WinElement1.BackColorAlpha = My.Env.CP.Windows7.ColorizationBlurBalance;
                        WinElement1.Win7ColorBal = My.Env.CP.Windows7.ColorizationColorBalance;
                        WinElement1.Win7GlowBal = My.Env.CP.Windows7.ColorizationAfterglowBalance;
                        WinElement1.NoisePower = My.Env.CP.Windows7.ColorizationGlassReflectionIntensity;
                        WinElement1.Shadow = My.Env.CP.WindowsEffects.WindowShadow;
                        break;
                    }
            }

            Panel2.BackColor = PanelRRaised1.BackColor;
            LabelR1.Font = My.Env.CP.MetricsFonts.CaptionFont;

            GroupBox4.Enabled = WinElement1.Style == UI.Simulation.WinElement.Styles.AltTab10 | ExplorerPatcher.IsAllowed();
            AlertBox1.Visible = My.Env.PreviewStyle == WindowStyle.W7;

            if (ExplorerPatcher.IsAllowed())
            {
                try
                {
                    if (Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer", "AltTabSettings", 0)) == 3)
                    {
                        WinElement1.Style = UI.Simulation.WinElement.Styles.AltTab10;
                        WinElement1.DarkMode = !My.Env.CP.Windows11.WinMode_Light;
                    }
                }
                finally
                {
                    My.MyProject.Computer.Registry.CurrentUser.Close();
                }
            }

            if (WinElement1.Style == UI.Simulation.WinElement.Styles.AltTab7Basic)
            {
                WinElement1.Size = new Size(360, 100);
            }
            else
            {
                WinElement1.Size = new Size(450, 150);
            }

            WinElement1.Left = (int)Math.Round((WinElement1.Parent.Width - WinElement1.Width) / 2d);
            WinElement1.Top = (int)Math.Round((WinElement1.Parent.Height - WinElement1.Height) / 2d);

            tabs_preview_1.DoubleBuffer();
        }

        public void ApplyFromCP(CP CP)
        {
            {
                ref var temp = ref CP.AltTab;
                AltTabEnabled.Checked = temp.Enabled;
                RadioImage1.Checked = temp.Style == CP.Structures.AltTab.Styles.Default | temp.Style == CP.Structures.AltTab.Styles.EP_Win10;
                RadioImage2.Checked = temp.Style == CP.Structures.AltTab.Styles.ClassicNT;
                Trackbar1.Value = temp.Win10Opacity;
            }
        }

        public void ApplyToCP(CP CP)
        {
            {
                ref var temp = ref CP.AltTab;
                temp.Enabled = AltTabEnabled.Checked;
                temp.Style = RadioImage1.Checked ? CP.Structures.AltTab.Styles.Default : CP.Structures.AltTab.Styles.ClassicNT;
                if (ExplorerPatcher.IsAllowed() & WinElement1.Style == UI.Simulation.WinElement.Styles.AltTab10)
                    temp.Style = CP.Structures.AltTab.Styles.EP_Win10;
                temp.Win10Opacity = Trackbar1.Value;
            }
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var CPx = new CP(CP.CP_Type.File, OpenFileDialog1.FileName);
                ApplyFromCP(CPx);
                CPx.Dispose();
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            var CPx = new CP(CP.CP_Type.Registry);
            ApplyFromCP(CPx);
            CPx.Dispose();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            using (var _Def = CP_Defaults.From(My.Env.PreviewStyle))
            {
                ApplyFromCP(_Def);
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var CPx = new CP(CP.CP_Type.Registry);
            ApplyToCP(CPx);
            ApplyToCP(My.Env.CP);
            CPx.AltTab.Apply();
            CPx.Dispose();
            Cursor = Cursors.Default;
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            ApplyToCP(My.Env.CP);
            Close();
        }

        private void AltTabEnabled_CheckedChanged(object sender, EventArgs e)
        {
            checker_img.Image = ((UI.WP.Toggle)sender).Checked ? My.Resources.checker_enabled : My.Resources.checker_disabled;
        }

        private void Opacity_btn_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar1.Maximum), Trackbar1.Minimum).ToString();
            Trackbar1.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Trackbar1_Scroll(object sender)
        {
            opacity_btn.Text = ((UI.WP.Trackbar)sender).Value.ToString();
            if (WinElement1.Style == UI.Simulation.WinElement.Styles.AltTab10)
                WinElement1.BackColorAlpha = Trackbar1.Value;
        }

        private void RadioImage2_CheckedChanged(object sender)
        {
            if (RadioImage2.Checked)
                tabs_preview_1.SelectedIndex = 1;
        }

        private void RadioImage1_CheckedChanged(object sender)
        {
            if (RadioImage1.Checked)
                tabs_preview_1.SelectedIndex = 0;
        }

        private void Form_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(My.Resources.Link_Wiki + "/Edit-Windows-switcher-(Alt-Tab-appearance)");
        }
    }
}