using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter
{

    public partial class Metrics_Fonts
    {
        public Metrics_Fonts()
        {
            InitializeComponent();
        }

        private void EditFonts_Load(object sender, EventArgs e)
        {
            MenuStrip1.Renderer = new UI.WP.StripRenderer();  // Removes the inferior white line from menu strip
            MenuStrip2.Renderer = new UI.WP.StripRenderer();

            pnl_preview1.BackgroundImage = My.MyProject.Forms.MainFrm.pnl_preview.BackgroundImage;
            pnl_preview2.BackgroundImage = My.MyProject.Forms.MainFrm.pnl_preview.BackgroundImage;
            pnl_preview3.BackgroundImage = My.MyProject.Forms.MainFrm.pnl_preview.BackgroundImage;
            pnl_preview4.BackgroundImage = My.MyProject.Forms.MainFrm.pnl_preview.BackgroundImage;

            Classic_Preview1.BackgroundImage = My.MyProject.Forms.MainFrm.pnl_preview.BackgroundImage;
            Classic_Preview3.BackgroundImage = My.MyProject.Forms.MainFrm.pnl_preview.BackgroundImage;
            Classic_Preview4.BackgroundImage = My.MyProject.Forms.MainFrm.pnl_preview.BackgroundImage;

            this.LoadLanguage();
            WPStyle.ApplyStyle(this);
            ApplyFromTM(My.Env.TM);

            Window1.CopycatFrom(My.MyProject.Forms.MainFrm.Window1, true);
            Window2.CopycatFrom(My.MyProject.Forms.MainFrm.Window2, true);
            Window4.CopycatFrom(My.MyProject.Forms.MainFrm.Window1, true);
            Window6.CopycatFrom(My.MyProject.Forms.MainFrm.Window1, true);

            SetClassicWindowColors(My.Env.TM, WindowR1);
            SetClassicWindowColors(My.Env.TM, WindowR2, false);
            SetClassicWindowColors(My.Env.TM, WindowR3);
            SetClassicWindowColors(My.Env.TM, WindowR5);
            SetClassicPanelColors(My.Env.TM, PanelR1);
            SetClassicPanelColors(My.Env.TM, PanelR2);
            SetClassicButtonColors(My.Env.TM, ButtonR1);
            SetClassicButtonColors(My.Env.TM, ButtonR2);
            SetClassicButtonColors(My.Env.TM, ButtonR3);
            SetClassicButtonColors(My.Env.TM, ButtonR10);
            SetClassicButtonColors(My.Env.TM, ButtonR11);
            SetClassicButtonColors(My.Env.TM, ButtonR12);

            ScrollBarR2.ButtonHilight = My.Env.TM.Win32.ButtonHilight;
            ScrollBarR2.BackColor = My.Env.TM.Win32.ButtonFace;
            ScrollBarR1.ButtonHilight = My.Env.TM.Win32.ButtonHilight;
            ScrollBarR1.BackColor = My.Env.TM.Win32.ButtonFace;

            Label13.ForeColor = My.Env.TM.Win32.ButtonText;
            Label14.ForeColor = My.Env.TM.Win32.ButtonText;
            Refresh17BitPreference();

            this.DoubleBuffer();

            bool condition0 = My.Env.PreviewStyle == WindowStyle.W7 && My.Env.TM.Windows7.Theme == Theme.Structures.Windows7.Themes.Classic;
            bool condition1 = My.Env.PreviewStyle == WindowStyle.WVista && My.Env.TM.WindowsVista.Theme == Theme.Structures.Windows7.Themes.Classic;
            bool condition2 = My.Env.PreviewStyle == WindowStyle.WXP && My.Env.TM.WindowsXP.Theme == Theme.Structures.WindowsXP.Themes.Classic;

            if (condition0 | condition1 | condition2)
            {
                tabs_preview_1.SelectedIndex = 1;
                tabs_preview_2.SelectedIndex = 1;
                tabs_preview_3.SelectedIndex = 1;
            }
            else
            {
                tabs_preview_1.SelectedIndex = 0;
                tabs_preview_2.SelectedIndex = 0;
                tabs_preview_3.SelectedIndex = 0;
            }

            FakeIcon1.Icon = My.MyProject.Forms.MainFrm.Icon;                  // Properties.Resources.fileextension 'Shell32.GetSystemIcon(Shell32.SHSTOCKICONID.RECYCLER, Shell32.SHGSI.ICON)
            FakeIcon2.Icon = Properties.Resources.fileextension;    // Properties.Resources.settingsfile 'Shell32.GetSystemIcon(Shell32.SHSTOCKICONID.FOLDER, Shell32.SHGSI.ICON)
            FakeIcon3.Icon = Properties.Resources.ThemesResIcon;    // Properties.Resources.icons8_command_line 'Shell32.GetSystemIcon(Shell32.SHSTOCKICONID.APPLICATION, Shell32.SHGSI.ICON)

            if (My.Env.WXP)
            {
                PictureBox35.Image = SystemIcons.Information.ToBitmap();
                PictureBox36.Image = SystemIcons.Information.ToBitmap();
            }
            else
            {
                var ico = DLLFunc.GetSystemIcon(Shell32.SHSTOCKICONID.INFO, Shell32.SHGSI.ICON);
                if (ico is not null)
                {
                    PictureBox35.Image = ico.ToBitmap();
                    PictureBox36.Image = ico.ToBitmap();
                }
                else
                {
                    PictureBox35.Image = SystemIcons.Information.ToBitmap();
                    PictureBox36.Image = SystemIcons.Information.ToBitmap();
                }
            }

            bool Win7 = Window6.Preview == UI.Simulation.Window.Preview_Enum.W7Aero | Window6.Preview == UI.Simulation.Window.Preview_Enum.W7Opaque | Window6.Preview == UI.Simulation.Window.Preview_Enum.W7Basic;
            bool Win8 = Window6.Preview == UI.Simulation.Window.Preview_Enum.W8 | Window6.Preview == UI.Simulation.Window.Preview_Enum.W8Lite;
            bool WinXP = Window6.Preview == UI.Simulation.Window.Preview_Enum.WXP;

            if (!Win7 & !Win8 & !WinXP)
            {
                msgLbl.ForeColor = Window6.DarkMode ? Color.White : Color.Black;
                MenuStrip1.BackColor = Window6.DarkMode ? Color.FromArgb(35, 35, 35) : Color.FromArgb(255, 255, 255);
                MenuStrip1.ForeColor = Window6.DarkMode ? Color.White : Color.Black;
            }
            else
            {
                msgLbl.ForeColor = Color.Black;
                MenuStrip1.BackColor = Color.FromArgb(255, 255, 255);
                MenuStrip1.ForeColor = Color.Black;
            }

            Button12.Image = My.MyProject.Forms.MainFrm.Button20.Image.Resize(16, 16);
            AlertBox10.Text = My.Env.Lang.CP_MetricsHighDPIAlert;

            AlertBox11.Text = My.MyProject.Forms.MainFrm.WXP_Alert2.Text;
            AlertBox11.Visible = My.MyProject.Forms.MainFrm.WXP_Alert2.Visible;
            AlertBox11.Size = AlertBox11.Parent.Size - new Size(40, 40);
            AlertBox11.Location = new Point(20, 20);

            AlertBox12.Text = AlertBox11.Text;
            AlertBox12.Visible = AlertBox11.Visible;
            AlertBox12.Size = AlertBox11.Size;
            AlertBox12.Location = AlertBox11.Location;

            AlertBox13.Text = AlertBox11.Text;
            AlertBox13.Visible = AlertBox11.Visible;
            AlertBox13.Size = AlertBox11.Size;
            AlertBox13.Location = AlertBox11.Location;

            My.MyProject.Forms.MainFrm.Visible = false;
        }

        public void Refresh17BitPreference()
        {

            if (My.Env.TM.Win32.EnableTheming)
            {
                MenuStrip2.BackColor = My.Env.TM.Win32.MenuBar;
            }
            else
            {
                MenuStrip2.BackColor = My.Env.TM.Win32.Menu;
            }

            ToolStripMenuItem1.ForeColor = My.Env.TM.Win32.MenuText;
            ToolStripMenuItem4.ForeColor = My.Env.TM.Win32.MenuText;

        }

        public void ApplyFromTM(Theme.Manager TM)
        {
            MetricsEnabled.Checked = TM.MetricsFonts.Enabled;

            Label1.Font = TM.MetricsFonts.CaptionFont;
            Window1.Font = TM.MetricsFonts.CaptionFont;
            WindowR1.Font = TM.MetricsFonts.CaptionFont;
            WindowR3.Font = TM.MetricsFonts.CaptionFont;
            WindowR5.Font = TM.MetricsFonts.CaptionFont;

            Label1.Text = TM.MetricsFonts.CaptionFont.Name;

            Label2.Font = TM.MetricsFonts.IconFont;
            FakeIcon1.Font = TM.MetricsFonts.IconFont;
            FakeIcon2.Font = TM.MetricsFonts.IconFont;
            FakeIcon3.Font = TM.MetricsFonts.IconFont;
            Label2.Text = TM.MetricsFonts.IconFont.Name;

            Label3.Font = TM.MetricsFonts.MenuFont;
            MenuStrip1.Font = TM.MetricsFonts.MenuFont;
            MenuStrip2.Font = TM.MetricsFonts.MenuFont;
            Label3.Text = TM.MetricsFonts.MenuFont.Name;

            Label5.Font = TM.MetricsFonts.SmCaptionFont;
            Window2.Font = TM.MetricsFonts.SmCaptionFont;
            WindowR2.Font = TM.MetricsFonts.SmCaptionFont;
            Label5.Text = TM.MetricsFonts.SmCaptionFont.Name;

            Label4.Font = TM.MetricsFonts.MessageFont;
            msgLbl.Font = TM.MetricsFonts.MessageFont;
            Label13.Font = TM.MetricsFonts.MessageFont;
            Label4.Text = TM.MetricsFonts.MessageFont.Name;

            Label6.Font = TM.MetricsFonts.StatusFont;
            statusLbl.Font = TM.MetricsFonts.StatusFont;
            Label14.Font = TM.MetricsFonts.StatusFont;
            Label6.Text = TM.MetricsFonts.StatusFont.Name;
            PanelR1.Height = Math.Max(GetTitleTextHeight(TM.MetricsFonts.StatusFont), 20);

            TextBox1.Text = TM.MetricsFonts.FontSubstitute_MSShellDlg;
            TextBox2.Text = TM.MetricsFonts.FontSubstitute_MSShellDlg2;
            TextBox3.Text = TM.MetricsFonts.FontSubstitute_SegoeUI;

            CheckBox1.Checked = TM.MetricsFonts.Fonts_SingleBitPP;

            Trackbar1.Value = TM.MetricsFonts.BorderWidth;
            Trackbar2.Value = TM.MetricsFonts.CaptionHeight;
            Trackbar3.Value = TM.MetricsFonts.CaptionWidth;
            Trackbar6.Value = TM.MetricsFonts.IconSpacing;
            Trackbar4.Value = TM.MetricsFonts.IconVerticalSpacing;
            Trackbar9.Value = TM.MetricsFonts.MenuHeight;
            Trackbar8.Value = TM.MetricsFonts.MenuWidth;
            Trackbar12.Value = TM.MetricsFonts.PaddedBorderWidth;
            Trackbar11.Value = TM.MetricsFonts.ScrollHeight;
            Trackbar10.Value = TM.MetricsFonts.ScrollWidth;
            Trackbar14.Value = TM.MetricsFonts.SmCaptionHeight;
            Trackbar13.Value = TM.MetricsFonts.SmCaptionWidth;
            Trackbar7.Value = TM.MetricsFonts.DesktopIconSize;
            Trackbar5.Value = TM.MetricsFonts.ShellIconSize;
            Trackbar15.Value = TM.MetricsFonts.ShellSmallIconSize;

            WindowR1.Metrics_CaptionWidth = TM.MetricsFonts.CaptionWidth;
            WindowR3.Metrics_CaptionWidth = TM.MetricsFonts.CaptionWidth;
            WindowR5.Metrics_CaptionWidth = TM.MetricsFonts.CaptionWidth;

            if (TM.WindowsEffects.IconsShadow)
            {
                FakeIcon1.ColorGlow = Color.FromArgb(75, 0, 0, 0);
            }
            else
            {
                FakeIcon1.ColorGlow = Color.FromArgb(0, 0, 0, 0);
            }
            FakeIcon2.ColorGlow = FakeIcon1.ColorGlow;
            FakeIcon3.ColorGlow = FakeIcon1.ColorGlow;

            WindowR1.Refresh();
            WindowR2.Refresh();
            WindowR3.Refresh();
            WindowR5.Refresh();

            WPStyle.CtrlTheme theme;
            Color statusBackColor, StatusForeColor;

            if (My.Env.PreviewStyle == WindowStyle.W11)
            {

                if (TM.Windows11.AppMode_Light)
                {
                    theme = WPStyle.CtrlTheme.Default;
                    StatusForeColor = Color.Black;
                    statusBackColor = Color.White;
                }
                else
                {
                    theme = WPStyle.CtrlTheme.DarkExplorer;
                    StatusForeColor = Color.White;
                    statusBackColor = Color.FromArgb(28, 28, 28);
                }
            }

            else if (My.Env.PreviewStyle == WindowStyle.W10)
            {

                if (TM.Windows10.AppMode_Light)
                {
                    theme = WPStyle.CtrlTheme.Default;
                    StatusForeColor = Color.Black;
                    statusBackColor = Color.White;
                }
                else
                {
                    theme = WPStyle.CtrlTheme.DarkExplorer;
                    StatusForeColor = Color.White;
                    statusBackColor = Color.FromArgb(28, 28, 28);
                }
            }

            else
            {
                StatusForeColor = Color.Black;
                statusBackColor = Color.White;
                theme = WPStyle.CtrlTheme.Default;
            }

            WPStyle.SetControlTheme(MenuStrip1.Handle, theme);
            WPStyle.SetControlTheme(VScrollBar1.Handle, theme);
            WPStyle.SetControlTheme(HScrollBar1.Handle, theme);
            WPStyle.SetControlTheme(StatusStrip1.Handle, theme);

            statusLbl.ForeColor = StatusForeColor;
            StatusStrip1.BackColor = statusBackColor;
        }

        public void ApplyToTM(Theme.Manager TM)
        {
            TM.MetricsFonts.Enabled = MetricsEnabled.Checked;

            TM.MetricsFonts.CaptionFont = Label1.Font;
            TM.MetricsFonts.IconFont = Label2.Font;
            TM.MetricsFonts.MenuFont = Label3.Font;
            TM.MetricsFonts.MessageFont = Label4.Font;
            TM.MetricsFonts.SmCaptionFont = Label5.Font;
            TM.MetricsFonts.StatusFont = Label6.Font;

            TM.MetricsFonts.BorderWidth = Trackbar1.Value;
            TM.MetricsFonts.CaptionHeight = Trackbar2.Value;
            TM.MetricsFonts.CaptionWidth = Trackbar3.Value;
            TM.MetricsFonts.IconSpacing = Trackbar6.Value;
            TM.MetricsFonts.IconVerticalSpacing = Trackbar4.Value;
            TM.MetricsFonts.MenuHeight = Trackbar9.Value;
            TM.MetricsFonts.MenuWidth = Trackbar8.Value;
            TM.MetricsFonts.PaddedBorderWidth = Trackbar12.Value;
            TM.MetricsFonts.ScrollHeight = Trackbar11.Value;
            TM.MetricsFonts.ScrollWidth = Trackbar10.Value;
            TM.MetricsFonts.SmCaptionHeight = Trackbar14.Value;
            TM.MetricsFonts.SmCaptionWidth = Trackbar13.Value;
            TM.MetricsFonts.DesktopIconSize = Trackbar7.Value;
            TM.MetricsFonts.ShellIconSize = Trackbar5.Value;
            TM.MetricsFonts.ShellSmallIconSize = Trackbar15.Value;
            TM.MetricsFonts.Fonts_SingleBitPP = CheckBox1.Checked;

            TM.MetricsFonts.FontSubstitute_MSShellDlg = TextBox1.Text;
            TM.MetricsFonts.FontSubstitute_MSShellDlg2 = TextBox2.Text;
            TM.MetricsFonts.FontSubstitute_SegoeUI = TextBox3.Text;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            FontDialog1.Font = Label1.Font;
            if (FontDialog1.ShowDialog() == DialogResult.OK)
            {
                Label1.Font = FontDialog1.Font;
                Window1.Font = FontDialog1.Font;
                WindowR1.Font = FontDialog1.Font;
                Window4.Font = FontDialog1.Font;
                WindowR3.Font = FontDialog1.Font;
                Window6.Font = FontDialog1.Font;
                WindowR5.Font = FontDialog1.Font;
                Label1.Text = FontDialog1.Font.Name;
                Window1.Refresh();
                WindowR1.Refresh();
                Window4.Refresh();
                WindowR3.Refresh();
                Window6.Refresh();
                WindowR5.Refresh();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            FontDialog1.Font = Label2.Font;
            if (FontDialog1.ShowDialog() == DialogResult.OK)
            {
                Label2.Font = FontDialog1.Font;
                FakeIcon1.Font = FontDialog1.Font;
                FakeIcon2.Font = FontDialog1.Font;
                FakeIcon3.Font = FontDialog1.Font;
                Label2.Text = FontDialog1.Font.Name;

            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            FontDialog1.Font = Label3.Font;
            if (FontDialog1.ShowDialog() == DialogResult.OK)
            {
                Label3.Font = FontDialog1.Font;
                MenuStrip1.Font = FontDialog1.Font;
                MenuStrip2.Font = FontDialog1.Font;
                Label3.Text = FontDialog1.Font.Name;
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            FontDialog1.Font = Label4.Font;
            if (FontDialog1.ShowDialog() == DialogResult.OK)
            {
                Label4.Font = FontDialog1.Font;
                msgLbl.Font = FontDialog1.Font;
                Label13.Font = FontDialog1.Font;
                Label4.Text = FontDialog1.Font.Name;

            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            FontDialog1.Font = Label5.Font;
            if (FontDialog1.ShowDialog() == DialogResult.OK)
            {
                Label5.Font = FontDialog1.Font;
                Window2.Font = FontDialog1.Font;
                WindowR2.Font = FontDialog1.Font;
                Label5.Text = FontDialog1.Font.Name;
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            FontDialog1.Font = Label6.Font;
            if (FontDialog1.ShowDialog() == DialogResult.OK)
            {
                Label6.Font = FontDialog1.Font;
                Label14.Font = FontDialog1.Font;
                statusLbl.Font = FontDialog1.Font;
                Label6.Text = FontDialog1.Font.Name;
                PanelR1.Height = Math.Max(GetTitleTextHeight(FontDialog1.Font), 20);
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            ApplyToTM(My.Env.TM);
            Close();
            SetModernWindowMetrics(My.Env.TM, My.MyProject.Forms.MainFrm.Window1);
            SetModernWindowMetrics(My.Env.TM, My.MyProject.Forms.MainFrm.Window2);
            SetClassicWindowMetrics(My.Env.TM, My.MyProject.Forms.MainFrm.ClassicWindow1);
            SetClassicWindowMetrics(My.Env.TM, My.MyProject.Forms.MainFrm.ClassicWindow2);
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
            ApplyToTM(My.Env.TM);
            SetModernWindowMetrics(TMx, My.MyProject.Forms.MainFrm.Window1);
            SetModernWindowMetrics(TMx, My.MyProject.Forms.MainFrm.Window2);
            SetClassicWindowMetrics(TMx, My.MyProject.Forms.MainFrm.ClassicWindow1);
            SetClassicWindowMetrics(TMx, My.MyProject.Forms.MainFrm.ClassicWindow2);

            TMx.MetricsFonts.Apply();
            TMx.Dispose();
            Cursor = Cursors.Default;
        }

        private void Trackbar2_Scroll(object sender)
        {
            ttl_h.Text = ((UI.WP.Trackbar)sender).Value.ToString();

            Window1.Metrics_CaptionHeight = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
            WindowR1.Metrics_CaptionHeight = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
            WindowR3.Metrics_CaptionHeight = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
            WindowR5.Metrics_CaptionHeight = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
        }

        private void Trackbar3_Scroll(object sender)
        {
            ttl_w.Text = ((UI.WP.Trackbar)sender).Value.ToString();
            WindowR1.Metrics_CaptionWidth = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
            WindowR3.Metrics_CaptionWidth = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
            WindowR5.Metrics_CaptionWidth = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
        }

        private void Trackbar9_Scroll(object sender)
        {
            m_h.Text = ((UI.WP.Trackbar)sender).Value.ToString();
            MenuStrip1.Height = Conversions.ToInteger(Math.Max(((UI.WP.Trackbar)sender).Value, GetTitleTextHeight(MenuStrip1.Font)));
            MenuStrip2.Height = MenuStrip1.Height;
            PanelR2.Refresh();
        }

        private void Trackbar8_Scroll(object sender)
        {
            m_w.Text = ((UI.WP.Trackbar)sender).Value.ToString();
        }

        private void Trackbar1_Scroll(object sender)
        {
            ttl_b.Text = ((UI.WP.Trackbar)sender).Value.ToString();
            Window1.Metrics_BorderWidth = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
            WindowR1.Metrics_BorderWidth = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
            WindowR2.Metrics_BorderWidth = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
            WindowR3.Metrics_BorderWidth = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
            WindowR5.Metrics_BorderWidth = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
        }

        private void Trackbar12_Scroll(object sender)
        {
            ttl_p.Text = ((UI.WP.Trackbar)sender).Value.ToString();
            Window1.Metrics_PaddedBorderWidth = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
            WindowR1.Metrics_PaddedBorderWidth = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
            WindowR2.Metrics_PaddedBorderWidth = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
            WindowR3.Metrics_PaddedBorderWidth = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
            WindowR5.Metrics_PaddedBorderWidth = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
        }

        private void Trackbar11_Scroll(object sender)
        {
            s_h.Text = ((UI.WP.Trackbar)sender).Value.ToString();
            HScrollBar1.Height = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
            ScrollBarR1.Height = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
            ButtonR1.Height = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
            ScrollBarR1.Refresh();
        }

        private void Trackbar10_Scroll(object sender)
        {
            s_w.Text = ((UI.WP.Trackbar)sender).Value.ToString();
            VScrollBar1.Width = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
            ScrollBarR2.Width = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
            ButtonR12.Width = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
            ScrollBarR2.Refresh();
        }

        private void Trackbar14_Scroll(object sender)
        {
            tw_h.Text = ((UI.WP.Trackbar)sender).Value.ToString();
            Window2.Metrics_CaptionHeight = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
            WindowR2.Metrics_CaptionHeight = Conversions.ToInteger(((UI.WP.Trackbar)sender).Value);
        }

        private void Trackbar13_Scroll(object sender)
        {
            tw_w.Text = ((UI.WP.Trackbar)sender).Value.ToString();
            WindowR2.Metrics_CaptionWidth = ((UI.WP.Trackbar)sender).Value;
        }

        private void Trackbar7_Scroll(object sender)
        {
            if (Trackbar7.Value < Trackbar7.Minimum)
            {
                Trackbar7.Value = Trackbar7.Minimum;
            }

            if (Trackbar7.Value > Trackbar7.Maximum)
            {
                Trackbar7.Value = Trackbar7.Maximum;
            }

            i_d_s.Text = ((UI.WP.Trackbar)sender).Value.ToString();
            FakeIcon1.IconSize = ((UI.WP.Trackbar)sender).Value;
            FakeIcon2.IconSize = ((UI.WP.Trackbar)sender).Value;
            FakeIcon3.IconSize = ((UI.WP.Trackbar)sender).Value;

            FakeIcon1.Height = ((UI.WP.Trackbar)sender).Value + 35;
            FakeIcon2.Height = ((UI.WP.Trackbar)sender).Value + 35;
            FakeIcon3.Height = ((UI.WP.Trackbar)sender).Value + 35;
            FakeIcon2.Top = FakeIcon1.Bottom + (Trackbar4.Value - 45);

            FakeIcon1.Width = ((UI.WP.Trackbar)sender).Value + 15 + Trackbar6.Value / 16;
            FakeIcon2.Width = ((UI.WP.Trackbar)sender).Value + 15 + Trackbar6.Value / 16;
            FakeIcon3.Width = ((UI.WP.Trackbar)sender).Value + 15 + Trackbar6.Value / 16;
            FakeIcon3.Left = FakeIcon1.Right + 2;

        }

        private void Trackbar6_Scroll(object sender)
        {
            i_s_h.Text = ((UI.WP.Trackbar)sender).Value.ToString();

            FakeIcon1.Width = Trackbar7.Value + 15 + ((UI.WP.Trackbar)sender).Value / 16;
            FakeIcon2.Width = Trackbar7.Value + 15 + ((UI.WP.Trackbar)sender).Value / 16;
            FakeIcon3.Width = Trackbar7.Value + 15 + ((UI.WP.Trackbar)sender).Value / 16;
            FakeIcon3.Left = FakeIcon1.Right + 2;

            FakeIcon3.SendToBack();
            FakeIcon1.BringToFront();
        }

        private void Trackbar4_Scroll(object sender)
        {
            i_s_v.Text = ((UI.WP.Trackbar)sender).Value.ToString();

            FakeIcon2.Top = FakeIcon1.Bottom + (Trackbar4.Value - 45);

            FakeIcon2.SendToBack();
            FakeIcon1.BringToFront();
        }

        private void Label1_FontChanged(object sender, EventArgs e)
        {
            ((Label)sender).Text = ((Label)sender).Font.FontFamily.Name;
        }

        private void Trackbar5_Scroll(object sender)
        {
            i_s_s.Text = ((UI.WP.Trackbar)sender).Value.ToString();
        }

        private void Window1_MetricsChanged()
        {
            Window1.SetMetrics(Window4);
            Window1.SetMetrics(Window6);
        }

        private void Window1_FontChanged(object sender, EventArgs e)
        {
            Window4.Font = Window1.Font;
            Window6.Font = Window1.Font;
        }

        private void MenuStrip1_FontChanged(object sender, EventArgs e)
        {
            MenuStrip1.Height = Math.Max(Trackbar9.Value, GetTitleTextHeight(MenuStrip1.Font));
        }

        public int GetTitleTextHeight(Font Font)
        {
            int TitleTextH; // , TitleTextH_9, TitleTextH_Sum As Integer
            TitleTextH = (int)Math.Round("ABCabc0123xYz.#".Measure(Font).Height);
            // TitleTextH_9 = "ABCabc0123xYz.#".Measure(New Font([Font].Name, 9, [Font].Style)).Height
            // TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9)

            return Font.Height; // TitleTextH 'TitleTextH_Sum
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
            using (var _Def = Theme.Default.From(My.Env.PreviewStyle))
            {
                ApplyFromTM(_Def);
            }
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar2.Maximum), Trackbar2.Minimum).ToString();
            Trackbar2.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Button13_Click_1(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar3.Maximum), Trackbar3.Minimum).ToString();
            Trackbar3.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar1.Maximum), Trackbar1.Minimum).ToString();
            Trackbar1.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar12.Maximum), Trackbar12.Minimum).ToString();
            Trackbar12.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Tw_h_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar14.Maximum), Trackbar14.Minimum).ToString();
            Trackbar14.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Tw_w_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar13.Maximum), Trackbar13.Minimum).ToString();
            Trackbar13.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void I_s_v_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar4.Maximum), Trackbar4.Minimum).ToString();
            Trackbar4.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void I_s_h_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar6.Maximum), Trackbar6.Minimum).ToString();
            Trackbar6.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void I_d_s_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar7.Maximum), Trackbar7.Minimum).ToString();
            Trackbar7.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void I_s_s_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar5.Maximum), Trackbar5.Minimum).ToString();
            Trackbar5.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Mh_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar9.Maximum), Trackbar9.Minimum).ToString();
            Trackbar9.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Mw_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar8.Maximum), Trackbar8.Minimum).ToString();
            Trackbar8.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Sh_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar11.Maximum), Trackbar11.Minimum).ToString();
            Trackbar11.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Sw_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar10.Maximum), Trackbar10.Minimum).ToString();
            Trackbar10.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void Button13_Click_2(object sender, EventArgs e)
        {
            if (tabs_preview_1.SelectedIndex == 0)
            {
                tabs_preview_1.SelectedIndex = 1;
                tabs_preview_2.SelectedIndex = 1;
                tabs_preview_3.SelectedIndex = 1;
            }
            else
            {
                tabs_preview_1.SelectedIndex = 0;
                tabs_preview_2.SelectedIndex = 0;
                tabs_preview_3.SelectedIndex = 0;
            }
        }

        private void Metrics_Fonts_FormClosed(object sender, FormClosedEventArgs e)
        {
            My.Env.RenderingHint = My.Env.TM.MetricsFonts.Fonts_SingleBitPP ? TextRenderingHint.SingleBitPerPixelGridFit : TextRenderingHint.ClearTypeGridFit;
            My.MyProject.Forms.MainFrm.Visible = true;
        }

        private void Button14_Click_1(object sender, EventArgs e)
        {
            var F = new Font(TextBox1.Text, 9f, FontStyle.Regular);
            FontDialog2.Font = F;
            if (FontDialog2.ShowDialog() == DialogResult.OK)
            {
                TextBox1.Text = FontDialog2.Font.Name;
            }
        }

        private void Button15_Click_1(object sender, EventArgs e)
        {
            var F = new Font(TextBox2.Text, 9f, FontStyle.Regular);
            FontDialog2.Font = F;
            if (FontDialog2.ShowDialog() == DialogResult.OK)
            {
                TextBox2.Text = FontDialog2.Font.Name;
            }
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            var F = new Font(TextBox3.Text, 9f, FontStyle.Regular);
            FontDialog2.Font = F;
            if (FontDialog2.ShowDialog() == DialogResult.OK)
            {
                TextBox3.Text = FontDialog2.Font.Name;
            }
        }

        private void Button16_Click_1(object sender, EventArgs e)
        {
            TextBox1.Text = "Microsoft Sans Serif";
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            TextBox2.Text = "Tahoma";
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            TextBox3.Text = "";
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

            if (Theme.Manager.IsFontInstalled(((UI.WP.TextBox)sender).Text.ToString(), FontStyle.Regular))
            {
                ((UI.WP.TextBox)sender).Font = new Font(((UI.WP.TextBox)sender).Text.ToString(), 9f, FontStyle.Regular);
            }
            else
            {
                ((UI.WP.TextBox)sender).Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular);
            }

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

            if (Theme.Manager.IsFontInstalled(((UI.WP.TextBox)sender).Text.ToString(), FontStyle.Regular))
            {
                ((UI.WP.TextBox)sender).Font = new Font(((UI.WP.TextBox)sender).Text.ToString(), 9f, FontStyle.Regular);
            }
            else
            {
                ((UI.WP.TextBox)sender).Font = new Font("Tahoma", 9f, FontStyle.Regular);
            }

        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {

            if (Theme.Manager.IsFontInstalled(((UI.WP.TextBox)sender).Text.ToString(), FontStyle.Regular))
            {
                ((UI.WP.TextBox)sender).Font = new Font(((UI.WP.TextBox)sender).Text.ToString(), 9f, FontStyle.Regular);
            }
            else
            {
                ((UI.WP.TextBox)sender).Font = new Font("Segoe UI", 9f, FontStyle.Regular);
            }

        }

        private void Button20_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.VS2Metrics.ShowDialog();
        }

        private void MetricsEnabled_CheckedChanged(object sender, EventArgs e)
        {
            checker_img.Image = Conversions.ToBoolean(((UI.WP.Toggle)sender).Checked) ? Properties.Resources.checker_enabled : Properties.Resources.checker_disabled;
        }

        private void Trackbar15_Scroll(object sender)
        {
            i_s_s_s.Text = ((UI.WP.Trackbar)sender).Value.ToString();
        }

        private void I_s_s_s_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), Trackbar15.Maximum), Trackbar15.Minimum).ToString();
            Trackbar15.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void CheckBox1_CheckedChanged(object sender)
        {
            My.Env.RenderingHint = CheckBox1.Checked ? TextRenderingHint.SingleBitPerPixelGridFit : TextRenderingHint.ClearTypeGridFit;
            Window1.Refresh();
            Window2.Refresh();
            Window4.Refresh();
            Window6.Refresh();
            WindowR1.Refresh();
            WindowR2.Refresh();
            WindowR3.Refresh();
            WindowR5.Refresh();
        }

        private void Metrics_Fonts_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(Properties.Resources.Link_Wiki + "/Edit-Windows-Metrics-and-Fonts");
        }
    }
}