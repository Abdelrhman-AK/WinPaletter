using Devcorp.Controls.VisualStyles;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter
{

    public partial class MainFrm
    {
        private bool _Shown = false;
        private bool RaiseUpdate = false;
        private string ver = "";
        private int StableInt, BetaInt, UpdateChannel;
        private int ChannelFixer;
        private List<string> Updates_ls = new();
        public bool LoggingOff = false;

        public MainFrm()
        {
            InitializeComponent();
        }

        #region Preview Voids
        public void ApplyColorsToElements(Theme.Manager TM)
        {
            ApplyWinElementsColors(TM, Program.PreviewStyle, true, taskbar, start, ActionCenter, setting_icon_preview, Label8, lnk_preview);
            ApplyWindowStyles(TM, Program.PreviewStyle, Window1, Window2, W81_start, W81_logonui);
        }
        public void ApplyStylesToElements(Theme.Manager TM, bool AnimateThePreview = true)
        {
            bool ItWasVisible = tabs_preview.Visible;

            if (AnimateThePreview & ItWasVisible)
            {
                if (_Shown)
                {
                    if (tabs_preview.Visible)
                        Program.Animator.HideSync(tabs_preview);
                }
                else
                {
                    tabs_preview.Visible = false;
                }
            }

            Program.Wallpaper = Program.FetchSuitableWallpaper(TM, Program.PreviewStyle);
            pnl_preview.BackgroundImage = Program.Wallpaper;
            pnl_preview_classic.BackgroundImage = Program.Wallpaper;

            ApplyWinElementsStyle(TM, Program.PreviewStyle, taskbar, start, ActionCenter, Window1, Window2, Panel3, lnk_preview, ClassicTaskbar, ButtonR2, ButtonR3, ButtonR4, ClassicWindow1, ClassicWindow2, WXP_VS_ReplaceColors.Checked, WXP_VS_ReplaceMetrics.Checked, WXP_VS_ReplaceFonts.Checked);

            Button23.Visible = Program.PreviewStyle == WindowStyle.W7;

            AdjustPreview_ModernOrClassic(TM, Program.PreviewStyle, tabs_preview, WXP_Alert2);

            // ReValidateLivePreview(tabs_preview)

            if (AnimateThePreview & ItWasVisible)
            {
                if (_Shown)
                {
                    Program.Animator.ShowSync(tabs_preview);
                }
                else
                {
                    tabs_preview.Visible = true;
                }
            }
        }

        public void LoadFromTM(Theme.Manager TM)
        {
            themename_lbl.Text = string.Format("{0} ({1})", TM.Info.ThemeName, TM.Info.ThemeVersion);
            author_lbl.Text = string.Format("{0} {1}", Program.Lang.By, TM.Info.Author);

            ref WPSettings.Structures.Appearance Appearance = ref Program.Settings.Appearance;
            Appearance.CustomColors = TM.AppTheme.Enabled;
            Appearance.BackColor = TM.AppTheme.BackColor;
            Appearance.AccentColor = TM.AppTheme.AccentColor;
            Appearance.CustomTheme = TM.AppTheme.DarkMode;
            Appearance.RoundedCorners = TM.AppTheme.RoundCorners;
            ApplyStyle(this);

            W11_WinMode_Toggle.Checked = !TM.Windows11.WinMode_Light;
            W11_AppMode_Toggle.Checked = !TM.Windows11.AppMode_Light;
            W11_Transparency_Toggle.Checked = TM.Windows11.Transparency;
            W11_ShowAccentOnTitlebarAndBorders_Toggle.Checked = TM.Windows11.ApplyAccentOnTitlebars;
            switch (TM.Windows11.ApplyAccentOnTaskbar)
            {
                case Theme.Structures.Windows10x.AccentTaskbarLevels.None:
                    {
                        W11_Accent_None.Checked = true;
                        break;
                    }

                case Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC:
                    {
                        W11_Accent_StartTaskbar.Checked = true;
                        break;
                    }

                case Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar:
                    {
                        W11_Accent_Taskbar.Checked = true;
                        break;
                    }

            }
            W11_ActiveTitlebar_pick.BackColor = TM.Windows11.Titlebar_Active;
            W11_InactiveTitlebar_pick.BackColor = TM.Windows11.Titlebar_Inactive;
            W11_Color_Index5.BackColor = TM.Windows11.StartMenu_Accent;
            W11_Color_Index4.BackColor = TM.Windows11.Color_Index2;
            W11_Color_Index6.BackColor = TM.Windows11.Color_Index6;
            W11_Color_Index1.BackColor = TM.Windows11.Color_Index1;
            W11_Color_Index2.BackColor = TM.Windows11.Color_Index4;
            W11_TaskbarFrontAndFoldersOnStart_pick.BackColor = TM.Windows11.Color_Index5;
            W11_Color_Index0.BackColor = TM.Windows11.Color_Index0;
            W11_Color_Index3.BackColor = TM.Windows11.Color_Index3;
            W11_Color_Index7.BackColor = TM.Windows11.Color_Index7;

            W10_WinMode_Toggle.Checked = !TM.Windows10.WinMode_Light;
            W10_AppMode_Toggle.Checked = !TM.Windows10.AppMode_Light;
            W10_Transparency_Toggle.Checked = TM.Windows10.Transparency;
            W10_TBTransparency_Toggle.Checked = TM.Windows10.IncreaseTBTransparency;
            W10_TB_Blur.Checked = TM.Windows10.TB_Blur;
            W10_ShowAccentOnTitlebarAndBorders_Toggle.Checked = TM.Windows10.ApplyAccentOnTitlebars;
            switch (TM.Windows10.ApplyAccentOnTaskbar)
            {
                case Theme.Structures.Windows10x.AccentTaskbarLevels.None:
                    {
                        W10_Accent_None.Checked = true;
                        break;
                    }

                case Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC:
                    {
                        W10_Accent_StartTaskbar.Checked = true;
                        break;
                    }

                case Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar:
                    {
                        W10_Accent_Taskbar.Checked = true;
                        break;
                    }
            }
            W10_ActiveTitlebar_pick.BackColor = TM.Windows10.Titlebar_Active;
            W10_InactiveTitlebar_pick.BackColor = TM.Windows10.Titlebar_Inactive;
            W10_Color_Index5.BackColor = TM.Windows10.StartMenu_Accent;
            W10_Color_Index4.BackColor = TM.Windows10.Color_Index2;
            W10_Color_Index6.BackColor = TM.Windows10.Color_Index6;
            W10_Color_Index1.BackColor = TM.Windows10.Color_Index1;
            W10_Color_Index2.BackColor = TM.Windows10.Color_Index4;
            W10_TaskbarFrontAndFoldersOnStart_pick.BackColor = TM.Windows10.Color_Index5;
            W10_Color_Index0.BackColor = TM.Windows10.Color_Index0;
            W10_Color_Index3.BackColor = TM.Windows10.Color_Index3;
            W10_Color_Index7.BackColor = TM.Windows10.Color_Index7;

            switch (TM.Windows81.Theme)
            {
                case Theme.Structures.Windows7.Themes.Aero:
                    {
                        W81_theme_aero.Checked = true;
                        break;
                    }

                case Theme.Structures.Windows7.Themes.AeroLite:
                    {
                        W81_theme_aerolite.Checked = true;
                        break;
                    }
            }
            W81_ColorizationColor_pick.BackColor = TM.Windows81.ColorizationColor;
            W81_ColorizationBalance_bar.Value = TM.Windows81.ColorizationColorBalance;
            W81_ColorizationBalance_val.Text = TM.Windows81.ColorizationColorBalance.ToString();
            W81_start_pick.BackColor = TM.Windows81.StartColor;
            W81_accent_pick.BackColor = TM.Windows81.AccentColor;
            W81_personalcls_background_pick.BackColor = TM.Windows81.PersonalColors_Background;
            W81_personalcolor_accent_pick.BackColor = TM.Windows81.PersonalColors_Accent;

            W7_ColorizationColor_pick.BackColor = TM.Windows7.ColorizationColor;
            W7_ColorizationAfterglow_pick.BackColor = TM.Windows7.ColorizationAfterglow;
            W7_ColorizationColorBalance_bar.Value = TM.Windows7.ColorizationColorBalance;
            W7_ColorizationAfterglowBalance_bar.Value = TM.Windows7.ColorizationAfterglowBalance;
            W7_ColorizationBlurBalance_bar.Value = TM.Windows7.ColorizationBlurBalance;
            W7_ColorizationGlassReflectionIntensity_bar.Value = TM.Windows7.ColorizationGlassReflectionIntensity;
            W7_ColorizationColorBalance_val.Text = TM.Windows7.ColorizationColorBalance.ToString();
            W7_ColorizationAfterglowBalance_val.Text = TM.Windows7.ColorizationAfterglowBalance.ToString();
            W7_ColorizationBlurBalance_val.Text = TM.Windows7.ColorizationBlurBalance.ToString();
            W7_ColorizationGlassReflectionIntensity_val.Text = TM.Windows7.ColorizationGlassReflectionIntensity.ToString();
            W7_EnableAeroPeek_toggle.Checked = TM.Windows7.EnableAeroPeek;
            W7_AlwaysHibernateThumbnails_Toggle.Checked = TM.Windows7.AlwaysHibernateThumbnails;
            switch (TM.Windows7.Theme)
            {
                case Theme.Structures.Windows7.Themes.Aero:
                    {
                        W7_theme_aero.Checked = true;
                        break;
                    }

                case Theme.Structures.Windows7.Themes.AeroOpaque:
                    {
                        W7_theme_aeroopaque.Checked = true;
                        break;
                    }

                case Theme.Structures.Windows7.Themes.Basic:
                    {
                        W7_theme_basic.Checked = true;
                        break;
                    }

                case Theme.Structures.Windows7.Themes.Classic:
                    {
                        W7_theme_classic.Checked = true;
                        break;
                    }
            }

            WVista_ColorizationColor_pick.BackColor = TM.WindowsVista.ColorizationColor;
            WVista_ColorizationColorBalance_bar.Value = TM.WindowsVista.Alpha;
            WVista_ColorizationColorBalance_val.Text = TM.WindowsVista.Alpha.ToString();
            switch (TM.WindowsVista.Theme)
            {
                case Theme.Structures.Windows7.Themes.Aero:
                    {
                        WVista_theme_aero.Checked = true;
                        break;
                    }

                case Theme.Structures.Windows7.Themes.AeroOpaque:
                    {
                        WVista_theme_aeroopaque.Checked = true;
                        break;
                    }

                case Theme.Structures.Windows7.Themes.Basic:
                    {
                        WVista_theme_basic.Checked = true;
                        break;
                    }

                case Theme.Structures.Windows7.Themes.Classic:
                    {
                        WVista_theme_classic.Checked = true;
                        break;
                    }
            }

            switch (TM.WindowsXP.Theme)
            {
                case Theme.Structures.WindowsXP.Themes.LunaBlue:
                    {
                        WXP_Luna_Blue.Checked = true;
                        break;
                    }

                case Theme.Structures.WindowsXP.Themes.LunaOliveGreen:
                    {
                        WXP_Luna_OliveGreen.Checked = true;
                        break;
                    }

                case Theme.Structures.WindowsXP.Themes.LunaSilver:
                    {
                        WXP_Luna_Silver.Checked = true;
                        break;
                    }

                case Theme.Structures.WindowsXP.Themes.Custom:
                    {
                        WXP_CustomTheme.Checked = true;
                        break;
                    }

                case Theme.Structures.WindowsXP.Themes.Classic:
                    {
                        WXP_Classic.Checked = true;
                        break;
                    }

            }
            WXP_VS_textbox.Text = TM.WindowsXP.ThemeFile;
            if (WXP_VS_ColorsList.Items.Contains(TM.WindowsXP.ColorScheme))
                WXP_VS_ColorsList.SelectedItem = TM.WindowsXP.ColorScheme;

            ApplyMetroStartToButton(TM, W81_start);
            ApplyBackLogonUI(TM, W81_logonui);
        }

        public void ApplyDefaultTMValues()
        {
            using (Theme.Manager DefTM = Theme.Default.Get())
            {
                W11_ActiveTitlebar_pick.DefaultColor = DefTM.Windows11.Titlebar_Active;
                W11_InactiveTitlebar_pick.DefaultColor = DefTM.Windows11.Titlebar_Inactive;
                W11_Color_Index5.DefaultColor = DefTM.Windows11.StartMenu_Accent;
                W11_Color_Index4.DefaultColor = DefTM.Windows11.Color_Index2;
                W11_Color_Index6.DefaultColor = DefTM.Windows11.Color_Index6;
                W11_Color_Index1.DefaultColor = DefTM.Windows11.Color_Index1;
                W11_Color_Index2.DefaultColor = DefTM.Windows11.Color_Index4;
                W11_TaskbarFrontAndFoldersOnStart_pick.DefaultColor = DefTM.Windows11.Color_Index5;
                W11_Color_Index0.DefaultColor = DefTM.Windows11.Color_Index0;
                W11_Color_Index3.DefaultColor = DefTM.Windows11.Color_Index3;
                W11_Color_Index7.DefaultColor = DefTM.Windows11.Color_Index7;

                W10_ActiveTitlebar_pick.DefaultColor = DefTM.Windows10.Titlebar_Active;
                W10_InactiveTitlebar_pick.DefaultColor = DefTM.Windows10.Titlebar_Inactive;
                W10_Color_Index5.DefaultColor = DefTM.Windows10.StartMenu_Accent;
                W10_Color_Index4.DefaultColor = DefTM.Windows10.Color_Index2;
                W10_Color_Index6.DefaultColor = DefTM.Windows10.Color_Index6;
                W10_Color_Index1.DefaultColor = DefTM.Windows10.Color_Index1;
                W10_Color_Index2.DefaultColor = DefTM.Windows10.Color_Index4;
                W10_TaskbarFrontAndFoldersOnStart_pick.DefaultColor = DefTM.Windows10.Color_Index5;
                W10_Color_Index0.DefaultColor = DefTM.Windows10.Color_Index0;
                W10_Color_Index3.DefaultColor = DefTM.Windows10.Color_Index3;
                W10_Color_Index7.DefaultColor = DefTM.Windows10.Color_Index7;

                W81_ColorizationColor_pick.DefaultColor = DefTM.Windows7.ColorizationColor;
                W81_start_pick.DefaultColor = DefTM.Windows81.StartColor;
                W81_accent_pick.DefaultColor = DefTM.Windows81.AccentColor;
                W81_personalcls_background_pick.DefaultColor = DefTM.Windows81.PersonalColors_Background;
                W81_personalcolor_accent_pick.DefaultColor = DefTM.Windows81.PersonalColors_Accent;

                W7_ColorizationColor_pick.DefaultColor = DefTM.Windows7.ColorizationColor;
                W7_ColorizationAfterglow_pick.DefaultColor = DefTM.Windows7.ColorizationAfterglow;

                WVista_ColorizationColor_pick.DefaultColor = DefTM.WindowsVista.ColorizationColor;
            }
        }

        public void Update_Wallpaper_Preview()
        {
            Cursor = Cursors.AppStarting;
            Program.Wallpaper = Program.FetchSuitableWallpaper(Program.TM, Program.PreviewStyle);
            pnl_preview.BackgroundImage = Program.Wallpaper;
            pnl_preview_classic.BackgroundImage = Program.Wallpaper;
            ApplyColorsToElements(Program.TM);
            LoadFromTM(Program.TM);
            ApplyStylesToElements(Program.TM, false);
            ReValidateLivePreview(pnl_preview);
            ReValidateLivePreview(pnl_preview_classic);
            Cursor = Cursors.Default;
        }

        public void SelectLeftPanelIndex()
        {
            if (Program.PreviewStyle == WindowStyle.W12 || Program.PreviewStyle == WindowStyle.W11)
            {
                TablessControl1.SelectedIndex = 0;
            }
            else if (Program.PreviewStyle == WindowStyle.W10)
            {
                TablessControl1.SelectedIndex = 1;
            }
            else if (Program.PreviewStyle == WindowStyle.W81)
            {
                TablessControl1.SelectedIndex = 2;
            }
            else if (Program.PreviewStyle == WindowStyle.W7)
            {
                TablessControl1.SelectedIndex = 3;
            }
            else if (Program.PreviewStyle == WindowStyle.WVista)
            {
                TablessControl1.SelectedIndex = 4;
            }
            else if (Program.PreviewStyle == WindowStyle.WXP)
            {
                TablessControl1.SelectedIndex = 5;
            }
            else
            {
                TablessControl1.SelectedIndex = 0;
            }
        }

        public void UpdateLegends()
        {
            if (Program.PreviewStyle == WindowStyle.W12 || Program.PreviewStyle == WindowStyle.W11)
            {
                ApplyWin10xLegends(Program.TM, Program.PreviewStyle, W11_lbl1, W11_lbl2, W11_lbl3, W11_lbl4, W11_lbl5, W11_lbl6, W11_lbl7, W11_lbl8, W11_lbl9, W11_pic1, W11_pic2, W11_pic3, W11_pic4, W11_pic5, W11_pic6, W11_pic7, W11_pic8, W11_pic9);
            }

            else if (Program.PreviewStyle == WindowStyle.W10)
            {
                ApplyWin10xLegends(Program.TM, Program.PreviewStyle, W10_lbl1, W10_lbl2, W10_lbl3, W10_lbl4, W10_lbl5, W10_lbl6, W10_lbl7, W10_lbl8, W10_lbl9, W10_pic1, W10_pic2, W10_pic3, W10_pic4, W10_pic5, W10_pic6, W10_pic7, W10_pic8, W10_pic9);

            }
        }
        #endregion

        #region Misc

        public void UpdateHint(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(UI.WP.Button))
                status_lbl.Text = (string)(((UI.WP.Button)sender).Tag ?? "");

            else if (sender.GetType() == typeof(UI.WP.RadioImage))
                status_lbl.Text = (string)(((UI.WP.RadioImage)sender).Tag ?? "");

        }

        public void EraseHint(object sender, EventArgs e)
        {
            status_lbl.Text = "";
        }

        public void UpdateHint_Dashboard(object sender, EventArgs e)
        {
            status_lbl.Text = (string)(((UI.WP.Button)sender).Tag ?? "");
        }

        public void EraseHint_Dashboard(object sender, EventArgs e)
        {
            status_lbl.Text = "";
        }

        #endregion

        public void AutoUpdatesCheck()
        {
            if (OS.WXP || OS.WVista)
                return;

            StableInt = 0;
            BetaInt = 0;
            UpdateChannel = 0;
            ChannelFixer = 0;
            if (Program.Settings.Updates.Channel == WPSettings.Structures.Updates.Channels.Stable)
                ChannelFixer = 0;
            if (Program.Settings.Updates.Channel == WPSettings.Structures.Updates.Channels.Beta)
                ChannelFixer = 1;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Program.IsNetworkAvailable())
            {
                try
                {
                    var WebCL = new WebClient();
                    RaiseUpdate = false;
                    ver = "";

                    Updates_ls = WebCL.DownloadString(Properties.Resources.Link_Updates).CList();

                    for (int x = 0, loopTo = Updates_ls.Count - 1; x <= loopTo; x++)
                    {
                        if (!string.IsNullOrEmpty(Updates_ls[x]) & !(Updates_ls[x].IndexOf("#") == 0))
                        {
                            if (Updates_ls[x].Split(' ')[0] == "Stable")
                                StableInt = x;
                            if (Updates_ls[x].Split(' ')[0] == "Beta")
                                BetaInt = x;
                        }
                    }

                    if (ChannelFixer == 0)
                        UpdateChannel = StableInt;
                    if (ChannelFixer == 1)
                        UpdateChannel = BetaInt;

                    ver = Updates_ls[UpdateChannel].Split(' ')[1];

                    RaiseUpdate = ver.CompareTo(Program.Version) == +1;
                }
                catch
                {
                }
            }
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (RaiseUpdate)
            {
                Forms.Updates.ls = Updates_ls;
                NotifyUpdates.Visible = true;
                Button5.Image = Properties.Resources.Update_Dot;
                NotifyUpdates.ShowBalloonTip(10000, Application.ProductName, string.Format("{0}. {1} {2}", Program.Lang.NewUpdate, Program.Lang.Version, ver), ToolTipIcon.Info);
            }
        }

        private void NotifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            NotifyUpdates.Visible = false;

            if (Application.OpenForms[Forms.Updates.Name] is null)
            {
                Forms.Updates.ShowDialog();
            }
            else
            {
                Forms.Updates.Focus();
            }
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (e.Data.GetData(typeof(UI.Controllers.ColorItem).FullName) is UI.Controllers.ColorItem)
            {
                Focus();
                BringToFront();
            }
            else
            {
                return;
            }

            base.OnDragOver(e);
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            _Shown = false;
            Visible = false;
            LoggingOff = false;

            NotifyUpdates.Icon = Icon;

            Size = new Size(Conversions.ToInteger(Program.Settings.General.MainFormWidth), Conversions.ToInteger(Program.Settings.General.MainFormHeight));
            WindowState = (FormWindowState)Conversions.ToInteger(Program.Settings.General.MainFormStatus);

            Select_W11.Image = Properties.Resources.Native11;
            Select_W10.Image = Properties.Resources.Native10;
            Select_W81.Image = Properties.Resources.Native8;
            Select_W7.Image = Properties.Resources.Native7;
            Select_WVista.Image = Properties.Resources.NativeVista;
            Select_WXP.Image = Properties.Resources.NativeXP;

            if (!Program.Elevated)
                apply_btn.Image = Properties.Resources.WP_Admin;

            if (Program.PreviewStyle == WindowStyle.W12 || Program.PreviewStyle == WindowStyle.W11)
            {
                TablessControl1.SelectedIndex = 0;
                Button20.Image = Properties.Resources.add_win11;
                Select_W11.Checked = true;
            }

            else if (Program.PreviewStyle == WindowStyle.W10)
            {
                TablessControl1.SelectedIndex = 1;
                Button20.Image = Properties.Resources.add_win10;
                Select_W10.Checked = true;
            }

            else if (Program.PreviewStyle == WindowStyle.W81)
            {
                TablessControl1.SelectedIndex = 2;
                Button20.Image = Properties.Resources.add_win8;
                Select_W81.Checked = true;
            }

            else if (Program.PreviewStyle == WindowStyle.W7)
            {
                TablessControl1.SelectedIndex = 3;
                Button20.Image = Properties.Resources.add_win7;
                Select_W7.Checked = true;
            }

            else if (Program.PreviewStyle == WindowStyle.WVista)
            {
                TablessControl1.SelectedIndex = 4;
                Button20.Image = Properties.Resources.add_winvista;
                Select_WVista.Checked = true;
            }

            else if (Program.PreviewStyle == WindowStyle.WXP)
            {
                TablessControl1.SelectedIndex = 5;
                Button20.Image = Properties.Resources.add_winxp;
                Select_WXP.Checked = true;
            }

            else
            {
                TablessControl1.SelectedIndex = 0;
                Button20.Image = Properties.Resources.add_win11;
                Select_W11.Checked = true;
            }

            this.LoadLanguage();
            ApplyStyle(this);
            this.DoubleBuffer();
            LoadData();

            WXP_Alert2.Size = WXP_Alert2.Parent.Size - new Size(40, 40);
            WXP_Alert2.Location = new Point(20, 20);

            Visible = true;

            BetaBadge.Visible = Program.IsBeta;
        }

        public void LoadData()
        {
            UpdateLegends();
            ApplyColorsToElements(Program.TM);
            ApplyStylesToElements(Program.TM);
            LoadFromTM(Program.TM);
            ApplyDefaultTMValues();
            userButton.Tag = User.UserName;
            userButton.Image = User.ProfilePicture.Resize(38, 38);
        }

        private void MainFrm_Shown(object sender, EventArgs e)
        {
            _Shown = true;

            foreach (UI.WP.Button btn in MainToolbar.Controls.OfType<UI.WP.Button>())
            {
                btn.MouseEnter += UpdateHint;
                btn.Enter += UpdateHint;
                btn.MouseLeave += EraseHint;
                btn.Leave += EraseHint;
            }

            foreach (UI.WP.Button btn in GroupBox3.Controls.OfType<UI.WP.Button>())
            {
                btn.MouseEnter += UpdateHint_Dashboard;
                btn.Enter += UpdateHint_Dashboard;
                btn.MouseLeave += EraseHint_Dashboard;
                btn.Leave += EraseHint_Dashboard;
            }

            foreach (UI.WP.RadioImage btn in previewContainer.Controls.OfType<UI.WP.RadioImage>())
            {
                btn.MouseEnter += UpdateHint;
                btn.Enter += UpdateHint;
                btn.MouseLeave += EraseHint;
                btn.Leave += EraseHint;
            }

            if (Program.Settings.Updates.AutoCheck)
                AutoUpdatesCheck();

            if (Program.ShowWhatsNew)
                Forms.Whatsnew.ShowDialog();
        }

        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (UI.WP.Button btn in MainToolbar.Controls.OfType<UI.WP.Button>())
            {
                btn.MouseEnter -= UpdateHint;
                btn.Enter -= UpdateHint;
                btn.MouseLeave -= EraseHint;
                btn.Leave -= EraseHint;
            }

            foreach (UI.WP.Button btn in GroupBox3.Controls.OfType<UI.WP.Button>())
            {
                btn.MouseEnter -= UpdateHint_Dashboard;
                btn.Enter -= UpdateHint_Dashboard;
                btn.MouseLeave -= EraseHint_Dashboard;
                btn.Leave -= EraseHint_Dashboard;
            }

            foreach (UI.WP.RadioImage btn in previewContainer.Controls.OfType<UI.WP.RadioImage>())
            {
                btn.MouseEnter -= UpdateHint;
                btn.Enter -= UpdateHint;
                btn.MouseLeave -= EraseHint;
                btn.Leave -= EraseHint;
            }
        }

        private void MainFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                Program.Settings.General.MainFormWidth = Size.Width;
                Program.Settings.General.MainFormHeight = Size.Height;
            }
            if (WindowState != FormWindowState.Minimized)
            {
                Program.Settings.General.MainFormStatus = WindowState;
            }
            Program.Settings.General.Save();

            var old = new WPSettings(WPSettings.Mode.Registry);
            {
                ref WPSettings.Structures.Appearance Appearance = ref Program.Settings.Appearance;
                Appearance.CustomColors = old.Appearance.CustomColors;
                Appearance.BackColor = old.Appearance.BackColor;
                Appearance.AccentColor = old.Appearance.AccentColor;
                Appearance.CustomTheme = old.Appearance.CustomTheme;
                Appearance.RoundedCorners = old.Appearance.RoundedCorners;
                Appearance.Save();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (Program.TM != Program.TM_Original)
            {

                if (Program.Settings.ThemeApplyingBehavior.ShowSaveConfirmation && !LoggingOff)
                {

                    switch (Forms.ComplexSave.ShowDialog())
                    {
                        case DialogResult.Yes:
                            {

                                string[] r = Program.Settings.General.ComplexSaveResult.Split('.');
                                string r1 = r[0];
                                string r2 = r[1];

                                switch (r1 ?? "")
                                {
                                    case "0":              // ' Save
                                        {
                                            if (System.IO.File.Exists(SaveFileDialog1.FileName))
                                            {
                                                Program.TM.Save(Theme.Manager.Source.File, SaveFileDialog1.FileName);
                                                Program.TM_Original = (Theme.Manager)Program.TM.Clone();
                                            }
                                            else if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
                                            {
                                                Program.TM.Save(Theme.Manager.Source.File, SaveFileDialog1.FileName);
                                                Program.TM_Original = (Theme.Manager)Program.TM.Clone();
                                            }
                                            else
                                            {
                                                e.Cancel = true;
                                            }

                                            break;
                                        }
                                    case "1":              // ' Save As
                                        {
                                            if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
                                            {
                                                Program.TM.Save(Theme.Manager.Source.File, SaveFileDialog1.FileName);
                                                Program.TM_Original = (Theme.Manager)Program.TM.Clone();
                                            }
                                            else
                                            {
                                                e.Cancel = true;
                                            }

                                            break;
                                        }
                                }

                                switch (r2 ?? "")
                                {
                                    case "1":
                                        {
                                            Forms.ThemeLog.Apply_Theme();
                                            break;
                                        }

                                    case "2":
                                        {
                                            Forms.ThemeLog.Apply_Theme(Program.TM_FirstTime);
                                            break;
                                        }

                                    case "3":
                                        {
                                            Forms.ThemeLog.Apply_Theme(Theme.Default.Get());
                                            break;
                                        }

                                }

                                break;
                            }

                        case DialogResult.No:
                            {
                                e.Cancel = false;
                                if ((OS.W7 | OS.W8 | OS.W81) & Program.Settings.Miscellaneous.Win7LivePreview)
                                    Program.RefreshDWM(Program.TM_Original);
                                base.OnFormClosing(e);
                                break;
                            }

                        case DialogResult.Cancel:
                            {
                                e.Cancel = true;
                                break;
                            }
                    }
                }
                else
                {
                    e.Cancel = false;
                    base.OnFormClosing(e);
                }
            }
            else
            {
                e.Cancel = false;
                base.OnFormClosing(e);
            }
        }

        #region Windows 11
        private void W11_pick_DragDrop(object sender, DragEventArgs e)
        {
            if (((UI.Controllers.ColorItem)sender).AllowDrop && Program.PreviewStyle == WindowStyle.W11)
            {
                Program.TM.Windows11.Titlebar_Active = W11_ActiveTitlebar_pick.BackColor;
                Program.TM.Windows11.Titlebar_Inactive = W11_InactiveTitlebar_pick.BackColor;
                Program.TM.Windows11.StartMenu_Accent = W11_Color_Index5.BackColor;
                Program.TM.Windows11.Color_Index2 = W11_Color_Index4.BackColor;
                Program.TM.Windows11.Color_Index6 = W11_Color_Index6.BackColor;
                Program.TM.Windows11.Color_Index1 = W11_Color_Index1.BackColor;
                Program.TM.Windows11.Color_Index4 = W11_Color_Index2.BackColor;
                Program.TM.Windows11.Color_Index5 = W11_TaskbarFrontAndFoldersOnStart_pick.BackColor;
                Program.TM.Windows11.Color_Index0 = W11_Color_Index0.BackColor;
                Program.TM.Windows11.Color_Index3 = W11_Color_Index3.BackColor;
                Program.TM.Windows11.Color_Index7 = W11_Color_Index7.BackColor;
                ApplyColorsToElements(Program.TM);
            }
        }

        private void W11_ActiveTitlebar_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.Windows11.Titlebar_Active = ((UI.Controllers.ColorItem)sender).BackColor;
                    if (Program.PreviewStyle == WindowStyle.W11)
                        ApplyColorsToElements(Program.TM);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender, Window1 };

            var C = Forms.ColorPickerDlg.Pick(CList);
            Program.TM.Windows11.Titlebar_Active = Color.FromArgb(255, C);
            if (Program.PreviewStyle == WindowStyle.W11)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();

        }

        private void W11_InactiveTitlebar_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.Windows11.Titlebar_Inactive = ((UI.Controllers.ColorItem)sender).BackColor;
                    if (Program.PreviewStyle == WindowStyle.W11)
                        ApplyColorsToElements(Program.TM);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender, Window2 };

            var _Conditions = new Conditions() { Window_InactiveTitlebar = true };
            var C = Forms.ColorPickerDlg.Pick(CList, _Conditions);

            Program.TM.Windows11.Titlebar_Inactive = Color.FromArgb(255, C);

            if (Program.PreviewStyle == WindowStyle.W11)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W11_WinMode_Toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (_Shown)
            {
                Program.TM.Windows11.WinMode_Light = !((UI.WP.Toggle)sender).Checked;
                if (Program.PreviewStyle == WindowStyle.W11)
                {
                    UpdateLegends();
                    ApplyColorsToElements(Program.TM);
                }
            }
        }

        private void W11_AppMode_Toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (_Shown)
            {
                Program.TM.Windows11.AppMode_Light = !((UI.WP.Toggle)sender).Checked;
                if (Program.PreviewStyle == WindowStyle.W11)
                    ApplyColorsToElements(Program.TM);
            }
        }

        private void W11_Transparency_Toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (_Shown)
            {
                Program.TM.Windows11.Transparency = ((UI.WP.Toggle)sender).Checked;
                if (Program.PreviewStyle == WindowStyle.W11)
                {
                    UpdateLegends();
                    ApplyColorsToElements(Program.TM);
                }
            }
        }

        private void W11_ShowAccentOnTitlebarAndBorders_Toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (_Shown)
            {
                Program.TM.Windows11.ApplyAccentOnTitlebars = ((UI.WP.Toggle)sender).Checked;
                if (Program.PreviewStyle == WindowStyle.W11)
                    ApplyColorsToElements(Program.TM);
            }
        }

        private void W11_Accent_None_CheckedChanged(object sender)
        {
            if (_Shown & ((UI.WP.RadioImage)sender).Checked)
            {
                Program.TM.Windows11.ApplyAccentOnTaskbar = Theme.Structures.Windows10x.AccentTaskbarLevels.None;
                if (Program.PreviewStyle == WindowStyle.W11)
                {
                    UpdateLegends();
                    ApplyColorsToElements(Program.TM);
                }
            }
        }

        private void W11_Accent_Taskbar_CheckedChanged(object sender)
        {
            if (_Shown & ((UI.WP.RadioImage)sender).Checked)
            {
                Program.TM.Windows11.ApplyAccentOnTaskbar = Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar;
                if (Program.PreviewStyle == WindowStyle.W11)
                {
                    UpdateLegends();
                    ApplyColorsToElements(Program.TM);
                }
            }
        }

        private void W11_Accent_StartTaskbar_CheckedChanged(object sender)
        {
            if (_Shown & ((UI.WP.RadioImage)sender).Checked)
            {
                Program.TM.Windows11.ApplyAccentOnTaskbar = Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC;
                if (Program.PreviewStyle == WindowStyle.W11)
                {
                    UpdateLegends();
                    ApplyColorsToElements(Program.TM);
                }
            }
        }

        private void W11_Color_Index1_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.Windows11.Color_Index1 = ((UI.Controllers.ColorItem)sender).BackColor;
                    if (Program.PreviewStyle == WindowStyle.W11)
                        ApplyColorsToElements(Program.TM);
                }
                return;
            }

            var CList = new List<Control>();
            Color C;
            CList.Add((Control)sender);

            if (ExplorerPatcher.IsAllowed())
            {
                CList.Add(taskbar);

                if (!Program.TM.Windows11.WinMode_Light)
                {
                    CList.Add(ActionCenter);
                    var _Conditions = new Conditions() { AppUnderlineOnly = true, ActionCenterBtn = true };
                    C = Forms.ColorPickerDlg.Pick(CList, _Conditions);
                }
                else
                {
                    var _Conditions = new Conditions() { AppUnderlineWithTaskbar = true };
                    C = Forms.ColorPickerDlg.Pick(CList, _Conditions);
                }
            }
            else if (!Program.TM.Windows11.WinMode_Light)
            {
                CList.Add(ActionCenter);
                CList.Add(taskbar);

                var _Conditions = new Conditions() { AppUnderlineOnly = true, ActionCenterBtn = true };
                C = Forms.ColorPickerDlg.Pick(CList, _Conditions);
            }
            else
            {
                C = Forms.ColorPickerDlg.Pick(CList);
            }

            Program.TM.Windows11.Color_Index1 = Color.FromArgb(255, C);
            if (Program.PreviewStyle == WindowStyle.W11)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W11_TaskbarFrontAndFoldersOnStart_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.Windows11.Color_Index5 = ((UI.Controllers.ColorItem)sender).BackColor;
                    if (Program.PreviewStyle == WindowStyle.W11)
                        ApplyColorsToElements(Program.TM);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender };

            if (ExplorerPatcher.IsAllowed())
            {
                if (!Program.TM.Windows11.WinMode_Light)
                {
                    CList.Add(ActionCenter);
                    CList.Add(taskbar);
                    if (!Program.EP.UseStart10)
                    {
                        CList.Add(start);
                    }
                }
                else
                {
                    CList.Add(lnk_preview);
                }
            }

            else if (!Program.TM.Windows11.WinMode_Light)
            {
                CList.Add(taskbar);
                CList.Add(start);
                CList.Add(ActionCenter);
            }
            else
            {
                CList.Add(taskbar);
                CList.Add(lnk_preview);
            }

            var C = Forms.ColorPickerDlg.Pick(CList);
            Program.TM.Windows11.Color_Index5 = Color.FromArgb(255, C);
            if (Program.PreviewStyle == WindowStyle.W11)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W11_Color_Index0_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.Windows11.Color_Index0 = ((UI.Controllers.ColorItem)sender).BackColor;
                    if (Program.PreviewStyle == WindowStyle.W11)
                        ApplyColorsToElements(Program.TM);
                }
                return;
            }

            var CList = new List<Control>();
            Color C;

            CList.Add((Control)sender);

            if (Program.TM.Windows11.WinMode_Light)
            {
                CList.Add(start);
                CList.Add(ActionCenter);
                C = Forms.ColorPickerDlg.Pick(CList);
            }
            else
            {
                CList.Add(lnk_preview);
                var _Conditions = new Conditions() { AppUnderlineOnly = true };
                C = Forms.ColorPickerDlg.Pick(CList, _Conditions);
            }

            Program.TM.Windows11.Color_Index0 = Color.FromArgb(255, C);
            if (Program.PreviewStyle == WindowStyle.W11)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();

        }

        private void W11_Color_Index3_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.Windows11.Color_Index3 = ((UI.Controllers.ColorItem)sender).BackColor;
                    if (Program.PreviewStyle == WindowStyle.W11)
                        ApplyColorsToElements(Program.TM);
                }
                return;
            }

            var CList = new List<Control>();
            Color C;

            CList.Add((Control)sender);
            CList.Add(taskbar);
            CList.Add(setting_icon_preview);

            var _Conditions = new Conditions() { AppUnderlineOnly = true };
            C = Forms.ColorPickerDlg.Pick(CList, _Conditions);

            Program.TM.Windows11.Color_Index3 = Color.FromArgb(255, C);
            if (Program.PreviewStyle == WindowStyle.W11)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W11_Color_Index6_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.Windows11.Color_Index6 = ((UI.Controllers.ColorItem)sender).BackColor;
                    if (Program.PreviewStyle == WindowStyle.W11)
                        ApplyColorsToElements(Program.TM);
                }
                return;
            }

            var CList = new List<Control>();
            Color C;

            CList.Add((Control)sender);
            var _Conditions = new Conditions();

            if (!ExplorerPatcher.IsAllowed())
            {
                CList.Add(taskbar);
                _Conditions = new Conditions() { AppBackgroundOnly = true };
            }

            C = Forms.ColorPickerDlg.Pick(CList, _Conditions);

            Program.TM.Windows11.Color_Index6 = Color.FromArgb(255, C);
            if (Program.PreviewStyle == WindowStyle.W11)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W11_Color_Index5_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.Windows11.StartMenu_Accent = ((UI.Controllers.ColorItem)sender).BackColor;
                    if (Program.PreviewStyle == WindowStyle.W11)
                        ApplyColorsToElements(Program.TM);
                }
                return;
            }


            var CList = new List<Control>() { (Control)sender };

            var C = Forms.ColorPickerDlg.Pick(CList);
            Program.TM.Windows11.StartMenu_Accent = Color.FromArgb(255, C);
            if (Program.PreviewStyle == WindowStyle.W11)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();

        }

        private void W11_Color_Index4_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.Windows11.Color_Index2 = ((UI.Controllers.ColorItem)sender).BackColor;
                    if (Program.PreviewStyle == WindowStyle.W11)
                        ApplyColorsToElements(Program.TM);
                }
                return;
            }


            var CList = new List<Control>() { (Control)sender };

            var C = Forms.ColorPickerDlg.Pick(CList);
            Program.TM.Windows11.Color_Index2 = Color.FromArgb(255, C);
            if (Program.PreviewStyle == WindowStyle.W11)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W11_Color_Index2_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.Windows11.Color_Index4 = ((UI.Controllers.ColorItem)sender).BackColor;
                    if (Program.PreviewStyle == WindowStyle.W11)
                        ApplyColorsToElements(Program.TM);
                }
                return;
            }


            var CList = new List<Control>() { (Control)sender };

            Color C;

            if (ExplorerPatcher.IsAllowed())
            {
                if (Program.EP.UseStart10)
                {
                    CList.Add(start);
                    C = Forms.ColorPickerDlg.Pick(CList);
                }

                else if (Program.TM.Windows11.WinMode_Light)
                {
                    CList.Add(ActionCenter);

                    var _Conditions = new Conditions() { ActionCenterBtn = true };
                    C = Forms.ColorPickerDlg.Pick(CList, _Conditions);
                }
                else
                {
                    CList.Add(start);

                    var _Conditions = new Conditions() { StartColorOnly = true };
                    C = Forms.ColorPickerDlg.Pick(CList, _Conditions);
                }
            }

            else if (Program.TM.Windows11.WinMode_Light)
            {
                CList.Add(ActionCenter);
                CList.Add(taskbar);

                var _Conditions = new Conditions() { AppUnderlineOnly = true, ActionCenterBtn = true };

                C = Forms.ColorPickerDlg.Pick(CList, _Conditions);
            }
            else
            {
                CList.Add(start);

                var _Conditions = new Conditions() { StartColorOnly = true };
                C = Forms.ColorPickerDlg.Pick(CList, _Conditions);

            }


            Program.TM.Windows11.Color_Index4 = Color.FromArgb(255, C);
            if (Program.PreviewStyle == WindowStyle.W11)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W11_Color_Index7_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Program.TM.Windows11.Color_Index7 = Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                return;
            }

            var CList = new List<Control>() { (Control)sender };
            var C = Forms.ColorPickerDlg.Pick(CList);

            Program.TM.Windows11.Color_Index7 = Color.FromArgb(255, C);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W11_Button8_Click_1(object sender, EventArgs e)
        {
            MsgBox(Program.Lang.TitlebarColorNotice, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button38_Click(object sender, EventArgs e)
        {
            // Copycat from Windows 11 colors
            tabs_preview.Visible = false;
            Program.TM.Windows10 = (Theme.Structures.Windows10x)Program.TM.Windows11.Clone();
            LoadFromTM(Program.TM);
            ApplyColorsToElements(Program.TM);
            tabs_preview.Visible = true;
        }
        #endregion

        #region Windows 10
        private void W10_pick_DragDrop(object sender, DragEventArgs e)
        {
            if (((UI.Controllers.ColorItem)sender).AllowDrop && Program.PreviewStyle == WindowStyle.W10)
            {
                Program.TM.Windows10.Titlebar_Active = W10_ActiveTitlebar_pick.BackColor;
                Program.TM.Windows10.Titlebar_Inactive = W10_InactiveTitlebar_pick.BackColor;
                Program.TM.Windows10.StartMenu_Accent = W10_Color_Index5.BackColor;
                Program.TM.Windows10.Color_Index2 = W10_Color_Index4.BackColor;
                Program.TM.Windows10.Color_Index6 = W10_Color_Index6.BackColor;
                Program.TM.Windows10.Color_Index1 = W10_Color_Index1.BackColor;
                Program.TM.Windows10.Color_Index4 = W10_Color_Index2.BackColor;
                Program.TM.Windows10.Color_Index5 = W10_TaskbarFrontAndFoldersOnStart_pick.BackColor;
                Program.TM.Windows10.Color_Index0 = W10_Color_Index0.BackColor;
                Program.TM.Windows10.Color_Index3 = W10_Color_Index3.BackColor;
                Program.TM.Windows10.Color_Index7 = W10_Color_Index7.BackColor;
                ApplyColorsToElements(Program.TM);
            }
        }

        private void W10_ActiveTitlebar_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.Windows10.Titlebar_Active = ((UI.Controllers.ColorItem)sender).BackColor;
                    if (Program.PreviewStyle == WindowStyle.W10)
                        ApplyColorsToElements(Program.TM);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender, Window1 };

            var _Conditions = new Conditions() { Window_ActiveTitlebar = true };
            var C = Forms.ColorPickerDlg.Pick(CList, _Conditions);

            Program.TM.Windows10.Titlebar_Active = Color.FromArgb(255, C);
            if (Program.PreviewStyle == WindowStyle.W10)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W10_InactiveTitlebar_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.Windows10.Titlebar_Inactive = ((UI.Controllers.ColorItem)sender).BackColor;
                    if (Program.PreviewStyle == WindowStyle.W10)
                        ApplyColorsToElements(Program.TM);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender, Window2 };

            var _Conditions = new Conditions() { Window_InactiveTitlebar = true };
            var C = Forms.ColorPickerDlg.Pick(CList, _Conditions);

            Program.TM.Windows10.Titlebar_Inactive = Color.FromArgb(255, C);

            if (Program.PreviewStyle == WindowStyle.W10)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W10_WinMode_Toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (_Shown)
            {
                Program.TM.Windows10.WinMode_Light = !((UI.WP.Toggle)sender).Checked;
                if (Program.PreviewStyle == WindowStyle.W10)
                {
                    UpdateLegends();
                    ApplyColorsToElements(Program.TM);
                }
            }
        }

        private void W10_AppMode_Toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (_Shown)
            {
                Program.TM.Windows10.AppMode_Light = !((UI.WP.Toggle)sender).Checked;
                if (Program.PreviewStyle == WindowStyle.W10)
                    ApplyColorsToElements(Program.TM);
            }
        }

        private void W10_Transparency_Toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (_Shown)
            {
                Program.TM.Windows10.Transparency = ((UI.WP.Toggle)sender).Checked;
                if (Program.PreviewStyle == WindowStyle.W10)
                {
                    UpdateLegends();
                    ApplyColorsToElements(Program.TM);
                }
            }
        }

        private void W10_ShowAccentOnTitlebarAndBorders_Toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (_Shown)
            {
                Program.TM.Windows10.ApplyAccentOnTitlebars = ((UI.WP.Toggle)sender).Checked;
                if (Program.PreviewStyle == WindowStyle.W10)
                    ApplyColorsToElements(Program.TM);
            }
        }

        private void W10_Accent_None_CheckedChanged(object sender)
        {
            if (_Shown & ((UI.WP.RadioImage)sender).Checked)
            {
                Program.TM.Windows10.ApplyAccentOnTaskbar = Theme.Structures.Windows10x.AccentTaskbarLevels.None;
                if (Program.PreviewStyle == WindowStyle.W10)
                {
                    UpdateLegends();
                    ApplyColorsToElements(Program.TM);
                }
            }
        }

        private void W10_Accent_Taskbar_CheckedChanged(object sender)
        {
            if (_Shown & ((UI.WP.RadioImage)sender).Checked)
            {
                Program.TM.Windows10.ApplyAccentOnTaskbar = Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar;
                if (Program.PreviewStyle == WindowStyle.W10)
                {
                    UpdateLegends();
                    ApplyColorsToElements(Program.TM);
                }
            }
        }

        private void W10_Accent_StartTaskbar_CheckedChanged(object sender)
        {
            if (_Shown & ((UI.WP.RadioImage)sender).Checked)
            {
                Program.TM.Windows10.ApplyAccentOnTaskbar = Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC;
                if (Program.PreviewStyle == WindowStyle.W10)
                {
                    UpdateLegends();
                    ApplyColorsToElements(Program.TM);
                }
            }
        }

        private void W10_TBTransparency_Toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (_Shown)
            {
                Program.TM.Windows10.IncreaseTBTransparency = ((UI.WP.Toggle)sender).Checked;
                if (Program.PreviewStyle == WindowStyle.W10)
                    ApplyColorsToElements(Program.TM);
            }
        }

        private void W10_Color_Index1_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.Windows10.Color_Index1 = ((UI.Controllers.ColorItem)sender).BackColor;
                    if (Program.PreviewStyle == WindowStyle.W10)
                        ApplyColorsToElements(Program.TM);
                }
                return;
            }

            var CList = new List<Control>();
            Color C;
            CList.Add((Control)sender);

            var _Conditions = new Conditions();

            switch (!Program.TM.Windows10.WinMode_Light)
            {
                case true:
                    {
                        CList.Add(taskbar);  // 'AppUnderline
                        _Conditions.AppUnderlineOnly = true;
                        break;
                    }
                case false:
                    {
                        if (Program.TM.Windows10.ApplyAccentOnTaskbar != Theme.Structures.Windows10x.AccentTaskbarLevels.None)
                        {
                            CList.Add(taskbar);  // 'AppUnderline
                            _Conditions.AppUnderlineOnly = true;
                        }

                        break;
                    }
            }

            C = Forms.ColorPickerDlg.Pick(CList, _Conditions);

            Program.TM.Windows10.Color_Index1 = Color.FromArgb(255, C);
            if (Program.PreviewStyle == WindowStyle.W10)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W10_TaskbarFrontAndFoldersOnStart_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.Windows10.Color_Index5 = ((UI.Controllers.ColorItem)sender).BackColor;
                    if (Program.PreviewStyle == WindowStyle.W10)
                        ApplyColorsToElements(Program.TM);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender };

            if (Program.TM.Windows10.Transparency)
            {
            }
            // ColorControls_List.Add(start) ''Hamburger
            else
            {
                CList.Add(taskbar);
            }

            var C = Forms.ColorPickerDlg.Pick(CList);
            Program.TM.Windows10.Color_Index5 = Color.FromArgb(255, C);
            if (Program.PreviewStyle == WindowStyle.W10)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();

        }

        private void W10_Color_Index0_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.Windows10.Color_Index0 = ((UI.Controllers.ColorItem)sender).BackColor;
                    if (Program.PreviewStyle == WindowStyle.W10)
                        ApplyColorsToElements(Program.TM);
                }
                return;
            }

            var CList = new List<Control>();
            Color C;

            CList.Add((Control)sender);


            var _Conditions = new Conditions();

            switch (!Program.TM.Windows10.WinMode_Light)
            {
                case true:
                    {
                        CList.Add(ActionCenter); // 'Link
                        _Conditions.ActionCenterLink = true;
                        break;
                    }

                case false:
                    {
                        if (Program.TM.Windows10.ApplyAccentOnTaskbar == Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC)
                        {
                            CList.Add(ActionCenter); // 'Link
                            _Conditions.ActionCenterLink = true;
                        }

                        break;
                    }
            }

            C = Forms.ColorPickerDlg.Pick(CList, _Conditions);

            Program.TM.Windows10.Color_Index0 = Color.FromArgb(255, C);
            if (Program.PreviewStyle == WindowStyle.W10)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();

        }

        private void W10_Color_Index3_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.Windows10.Color_Index3 = ((UI.Controllers.ColorItem)sender).BackColor;
                    if (Program.PreviewStyle == WindowStyle.W10)
                        ApplyColorsToElements(Program.TM);
                }
                return;
            }

            var CList = new List<Control>();
            Color C;

            CList.Add((Control)sender);

            var _Conditions = new Conditions();

            switch (!Program.TM.Windows10.WinMode_Light)
            {
                case true:
                    {
                        if (Program.TM.Windows10.Transparency)
                        {
                            CList.Add(setting_icon_preview);
                            CList.Add(ActionCenter);
                            _Conditions.ActionCenterBtn = true;
                            CList.Add(lnk_preview);
                            if (Program.TM.Windows10.ApplyAccentOnTaskbar != Theme.Structures.Windows10x.AccentTaskbarLevels.None)
                            {
                                CList.Add(taskbar);  // 'AppBackground
                                _Conditions.AppBackgroundOnly = true;

                            }
                        }
                        else
                        {
                            CList.Add(setting_icon_preview);
                            CList.Add(ActionCenter);
                            _Conditions.ActionCenterBtn = true;
                            CList.Add(lnk_preview);
                        }

                        break;
                    }
                case false:
                    {
                        CList.Add(setting_icon_preview);
                        CList.Add(ActionCenter);
                        _Conditions.ActionCenterBtn = true;
                        CList.Add(lnk_preview);

                        if (Program.TM.Windows10.ApplyAccentOnTaskbar == Theme.Structures.Windows10x.AccentTaskbarLevels.None)
                        {
                            CList.Add(taskbar);  // 'AppBackground
                            _Conditions.AppBackgroundOnly = true;
                            _Conditions.AppUnderlineOnly = true;

                        }

                        break;
                    }
            }
            C = Forms.ColorPickerDlg.Pick(CList, _Conditions);

            Program.TM.Windows10.Color_Index3 = Color.FromArgb(255, C);
            if (Program.PreviewStyle == WindowStyle.W10)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W10_Color_Index6_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.Windows10.Color_Index6 = ((UI.Controllers.ColorItem)sender).BackColor;
                    if (Program.PreviewStyle == WindowStyle.W10)
                        ApplyColorsToElements(Program.TM);
                }
                return;
            }

            var CList = new List<Control>();
            Color C;

            CList.Add((Control)sender);

            var _Conditions = new Conditions();

            switch (!Program.TM.Windows10.WinMode_Light)
            {
                case true:
                    {

                        if (Program.TM.Windows10.Transparency)
                        {
                            CList.Add(taskbar);
                        }

                        break;
                    }

                case false:
                    {

                        if (Program.TM.Windows10.Transparency)
                        {
                            CList.Add(taskbar);

                            if (Program.TM.Windows10.ApplyAccentOnTaskbar != Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC)
                            {
                                CList.Add(ActionCenter); // 'ActionCenterLinks
                                _Conditions.ActionCenterLink = true;
                            }
                        }

                        else if (Program.TM.Windows10.ApplyAccentOnTaskbar != Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC)
                        {
                            CList.Add(ActionCenter); // 'ActionCenterLinks
                            _Conditions.ActionCenterLink = true;

                        }

                        break;
                    }
            }

            C = Forms.ColorPickerDlg.Pick(CList, _Conditions);

            Program.TM.Windows10.Color_Index6 = Color.FromArgb(255, C);
            if (Program.PreviewStyle == WindowStyle.W10)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W10_Color_Index5_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.Windows10.StartMenu_Accent = ((UI.Controllers.ColorItem)sender).BackColor;
                    if (Program.PreviewStyle == WindowStyle.W10)
                        ApplyColorsToElements(Program.TM);
                }
                return;
            }


            var CList = new List<Control>() { (Control)sender };


            var C = Forms.ColorPickerDlg.Pick(CList);
            Program.TM.Windows10.StartMenu_Accent = Color.FromArgb(255, C);
            if (Program.PreviewStyle == WindowStyle.W10)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();

        }

        private void W10_Color_Index4_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.Windows10.Color_Index2 = ((UI.Controllers.ColorItem)sender).BackColor;
                    if (Program.PreviewStyle == WindowStyle.W10)
                        ApplyColorsToElements(Program.TM);
                }
                return;
            }


            var CList = new List<Control>() { (Control)sender };

            if (Program.PreviewStyle == WindowStyle.W10)
            {
                // ColorControls_List.Add(taskbar) 'Start Icon Hover
            }

            var C = Forms.ColorPickerDlg.Pick(CList);
            Program.TM.Windows10.Color_Index2 = Color.FromArgb(255, C);
            if (Program.PreviewStyle == WindowStyle.W10)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();

        }

        private void W10_Color_Index2_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.Windows10.Color_Index4 = ((UI.Controllers.ColorItem)sender).BackColor;
                    if (Program.PreviewStyle == WindowStyle.W10)
                        ApplyColorsToElements(Program.TM);
                }
                return;
            }


            var CList = new List<Control>() { (Control)sender };

            Color C;


            var _Conditions = new Conditions();

            switch (!Program.TM.Windows10.WinMode_Light)
            {
                case true:
                    {

                        if (Program.TM.Windows10.Transparency)
                        {
                            CList.Add(start);
                            CList.Add(ActionCenter);
                        }
                        else
                        {
                            CList.Add(start);
                            CList.Add(ActionCenter);
                            CList.Add(taskbar);  // AppBackground
                            _Conditions.AppBackgroundOnly = true;
                        }

                        break;
                    }

                case false:
                    {
                        if (Program.TM.Windows10.Transparency)
                        {
                            CList.Add(start);
                            CList.Add(ActionCenter);
                        }
                        else if (Program.TM.Windows10.ApplyAccentOnTaskbar == Theme.Structures.Windows10x.AccentTaskbarLevels.None)
                        {
                            CList.Add(start);
                            CList.Add(ActionCenter);
                        }
                        else if (Program.TM.Windows10.ApplyAccentOnTaskbar == Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar)
                        {
                            CList.Add(start);
                            CList.Add(ActionCenter);
                            CList.Add(taskbar);  // Start ButtonR Hover
                            _Conditions.StartColorOnly = true;
                        }
                        else
                        {
                            CList.Add(start);
                            CList.Add(ActionCenter);
                            CList.Add(taskbar);  // AppBackground
                            _Conditions.AppBackgroundOnly = true;
                        }

                        break;
                    }
            }

            C = Forms.ColorPickerDlg.Pick(CList, _Conditions);

            Program.TM.Windows10.Color_Index4 = Color.FromArgb(255, C);
            if (Program.PreviewStyle == WindowStyle.W10)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W10_Color_Index7_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Program.TM.Windows10.Color_Index7 = Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                return;
            }

            var CList = new List<Control>() { (Control)sender };
            var C = Forms.ColorPickerDlg.Pick(CList);

            Program.TM.Windows10.Color_Index7 = Color.FromArgb(255, C);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W10_Button8_Click_1(object sender, EventArgs e)
        {
            MsgBox(Program.Lang.TitlebarColorNotice, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void W10_Button25_Click(object sender, EventArgs e)
        {
            MsgBox(Program.Lang.TM_AccentOnTaskbarTib, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void W10_TB_Blur_CheckedChanged(object sender, EventArgs e)
        {
            if (_Shown)
            {
                Program.TM.Windows10.TB_Blur = ((UI.WP.Toggle)sender).Checked;
                if (Program.PreviewStyle == WindowStyle.W10)
                    ApplyColorsToElements(Program.TM);
            }
        }

        private void Button37_Click(object sender, EventArgs e)
        {
            // Copycat from Windows 10 colors
            tabs_preview.Visible = false;
            Program.TM.Windows11 = (Theme.Structures.Windows10x)Program.TM.Windows10.Clone();
            LoadFromTM(Program.TM);
            ApplyColorsToElements(Program.TM);
            tabs_preview.Visible = true;
        }

        #endregion

        #region Windows 8.1
        private void W81_pick_DragDrop(object sender, DragEventArgs e)
        {
            if (((UI.Controllers.ColorItem)sender).AllowDrop && Program.PreviewStyle == WindowStyle.W81)
            {
                Program.TM.Windows81.ColorizationColor = W81_ColorizationColor_pick.BackColor;
                Program.TM.Windows81.StartColor = W81_start_pick.BackColor;
                Program.TM.Windows81.AccentColor = W81_accent_pick.BackColor;
                Program.TM.Windows81.PersonalColors_Background = W81_personalcls_background_pick.BackColor;
                Program.TM.Windows81.PersonalColors_Accent = W81_personalcolor_accent_pick.BackColor;
                ApplyColorsToElements(Program.TM);
            }
        }

        private void W8_ColorizationColor_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.Windows81.ColorizationColor = ((UI.Controllers.ColorItem)sender).BackColor;
                    if (Program.PreviewStyle == WindowStyle.W81)
                        ApplyColorsToElements(Program.TM);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender, start, taskbar, Window1, Window2 };

            var _Conditions = new Conditions() { Window_ActiveTitlebar = true, Window_InactiveTitlebar = true, LivePreview_Colorization = true };

            var C = Forms.ColorPickerDlg.Pick(CList, _Conditions);

            Program.TM.Windows81.ColorizationColor = Color.FromArgb(255, C);

            if (Program.PreviewStyle == WindowStyle.W81)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W8_ColorizationBalance_bar_Scroll(object sender)
        {
            if (_Shown)
            {
                W81_ColorizationBalance_val.Text = ((UI.WP.Trackbar)sender).Value.ToString();
                Program.TM.Windows81.ColorizationColorBalance = W81_ColorizationBalance_bar.Value;
                if (Program.PreviewStyle == WindowStyle.W81)
                    ApplyColorsToElements(Program.TM);
            }
        }

        private void W8_start_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.Windows81.StartColor = ((UI.Controllers.ColorItem)sender).BackColor;
                    if (Program.PreviewStyle == WindowStyle.W81)
                        ApplyColorsToElements(Program.TM);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender };

            var C = Forms.ColorPickerDlg.Pick(CList);

            Program.TM.Windows81.StartColor = Color.FromArgb(255, C);

            if (Program.PreviewStyle == WindowStyle.W81)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W8_accent_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.Windows81.AccentColor = ((UI.Controllers.ColorItem)sender).BackColor;
                    if (Program.PreviewStyle == WindowStyle.W81)
                        ApplyColorsToElements(Program.TM);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender };

            var C = Forms.ColorPickerDlg.Pick(CList);

            Program.TM.Windows81.AccentColor = Color.FromArgb(255, C);

            if (Program.PreviewStyle == WindowStyle.W81)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W8_personalcls_background_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.Windows81.PersonalColors_Background = ((UI.Controllers.ColorItem)sender).BackColor;
                    if (Program.PreviewStyle == WindowStyle.W81)
                        ApplyColorsToElements(Program.TM);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender };

            var C = Forms.ColorPickerDlg.Pick(CList);

            Program.TM.Windows81.PersonalColors_Background = Color.FromArgb(255, C);

            if (Program.PreviewStyle == WindowStyle.W81)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W8_personalcolor_accent_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.Windows81.PersonalColors_Accent = ((UI.Controllers.ColorItem)sender).BackColor;
                    if (Program.PreviewStyle == WindowStyle.W81)
                        ApplyColorsToElements(Program.TM);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender };

            var C = Forms.ColorPickerDlg.Pick(CList);

            Program.TM.Windows81.PersonalColors_Accent = Color.FromArgb(255, C);

            if (Program.PreviewStyle == WindowStyle.W81)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W8_ColorizationBalance_val_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), W81_ColorizationBalance_bar.Maximum), W81_ColorizationBalance_bar.Minimum).ToString();
            W81_ColorizationBalance_bar.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void W8_theme_aero_CheckedChanged(object sender)
        {
            if (W81_theme_aero.Checked)
            {
                Program.TM.Windows81.Theme = Theme.Structures.Windows7.Themes.Aero;
                if (Program.PreviewStyle == WindowStyle.W81)
                    ApplyColorsToElements(Program.TM);
            }
        }

        private void W8_theme_aerolite_CheckedChanged(object sender)
        {
            if (W81_theme_aerolite.Checked)
            {
                Program.TM.Windows81.Theme = Theme.Structures.Windows7.Themes.AeroLite;
                if (Program.PreviewStyle == WindowStyle.W81)
                    ApplyColorsToElements(Program.TM);
            }
        }

        private void W8_start_Click(object sender, EventArgs e)
        {
            Forms.Start8Selector.ShowDialog();
            if (Program.PreviewStyle == WindowStyle.W81)
                ApplyColorsToElements(Program.TM);
        }

        private void W8_logonui_Click(object sender, EventArgs e)
        {
            Forms.LogonUI8Colors.ShowDialog();
            if (Program.PreviewStyle == WindowStyle.W81)
                ApplyColorsToElements(Program.TM);
        }
        #endregion

        #region Windows 7
        private void W7_pick_DragDrop(object sender, DragEventArgs e)
        {
            if (((UI.Controllers.ColorItem)sender).AllowDrop && Program.PreviewStyle == WindowStyle.W7)
            {
                Program.TM.Windows7.ColorizationColor = W7_ColorizationColor_pick.BackColor;
                Program.TM.Windows7.ColorizationAfterglow = W7_ColorizationAfterglow_pick.BackColor;
                ApplyColorsToElements(Program.TM);
            }
        }

        private void W7_ColorizationColor_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.Windows7.ColorizationColor = ((UI.Controllers.ColorItem)sender).BackColor;
                    if (Program.PreviewStyle == WindowStyle.W7)
                        ApplyColorsToElements(Program.TM);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender, start, taskbar, Window1, Window2 };

            var _Conditions = new Conditions() { Win7 = true, Color1 = true, Background = true, LivePreview_Colorization = true };

            var C = Forms.ColorPickerDlg.Pick(CList, _Conditions);

            Program.TM.Windows7.ColorizationColor = Color.FromArgb(255, C);

            if (Program.PreviewStyle == WindowStyle.W7)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W7_ColorizationAfterglow_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.Windows7.ColorizationAfterglow = ((UI.Controllers.ColorItem)sender).BackColor;
                    ApplyColorsToElements(Program.TM);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender, start, taskbar, Window1, Window2 };

            var _Conditions = new Conditions() { Win7 = true, Color2 = true, Background2 = true, LivePreview_AfterGlow = true };

            var C = Forms.ColorPickerDlg.Pick(CList, _Conditions);

            Program.TM.Windows7.ColorizationAfterglow = Color.FromArgb(255, C);

            if (Program.PreviewStyle == WindowStyle.W7)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W7_EnableAeroPeek_toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (_Shown)
                Program.TM.Windows7.EnableAeroPeek = W7_EnableAeroPeek_toggle.Checked;
        }

        private void W7_AlwaysHibernateThumbnails_Toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (_Shown)
                Program.TM.Windows7.AlwaysHibernateThumbnails = W7_AlwaysHibernateThumbnails_Toggle.Checked;
        }

        private void W7_ColorizationColorBalance_bar_Scroll(object sender)
        {
            if (_Shown)
            {
                W7_ColorizationColorBalance_val.Text = ((UI.WP.Trackbar)sender).Value.ToString();
                Program.TM.Windows7.ColorizationColorBalance = W7_ColorizationColorBalance_bar.Value;
                if (Program.PreviewStyle == WindowStyle.W7)
                    ApplyColorsToElements(Program.TM);
            }
        }

        private void W7_ColorizationBlurBalance_bar_Scroll(object sender)
        {
            if (_Shown)
            {
                W7_ColorizationBlurBalance_val.Text = ((UI.WP.Trackbar)sender).Value.ToString();
                Program.TM.Windows7.ColorizationBlurBalance = W7_ColorizationBlurBalance_bar.Value;
                if (Program.PreviewStyle == WindowStyle.W7)
                    ApplyColorsToElements(Program.TM);
            }
        }

        private void W7_ColorizationGlassReflectionIntensity_bar_Scroll(object sender)
        {
            if (_Shown)
            {
                W7_ColorizationGlassReflectionIntensity_val.Text = ((UI.WP.Trackbar)sender).Value.ToString();
                Program.TM.Windows7.ColorizationGlassReflectionIntensity = W7_ColorizationGlassReflectionIntensity_bar.Value;
                if (Program.PreviewStyle == WindowStyle.W7)
                    ApplyColorsToElements(Program.TM);
            }
        }

        private void W7_theme_classic_CheckedChanged(object sender)
        {
            if (W7_theme_classic.Checked)
            {
                Program.TM.Windows7.Theme = Theme.Structures.Windows7.Themes.Classic;
                if (Program.PreviewStyle == WindowStyle.W7)
                {
                    ApplyColorsToElements(Program.TM);
                    ApplyStylesToElements(Program.TM, false);
                }
            }

        }

        private void W7_theme_basic_CheckedChanged(object sender)
        {
            if (W7_theme_basic.Checked)
            {
                Program.TM.Windows7.Theme = Theme.Structures.Windows7.Themes.Basic;
                if (Program.PreviewStyle == WindowStyle.W7)
                {
                    ApplyColorsToElements(Program.TM);
                    ApplyStylesToElements(Program.TM, false);
                }
            }
        }

        private void W7_theme_aeroopaque_CheckedChanged(object sender)
        {
            if (W7_theme_aeroopaque.Checked)
            {
                Program.TM.Windows7.Theme = Theme.Structures.Windows7.Themes.AeroOpaque;
                if (Program.PreviewStyle == WindowStyle.W7)
                {
                    ApplyColorsToElements(Program.TM);
                    ApplyStylesToElements(Program.TM, false);
                }
            }
        }

        private void W7_theme_Aero_CheckedChanged(object sender)
        {
            if (W7_theme_aero.Checked)
            {
                Program.TM.Windows7.Theme = Theme.Structures.Windows7.Themes.Aero;
                if (Program.PreviewStyle == WindowStyle.W7)
                {
                    ApplyColorsToElements(Program.TM);
                    ApplyStylesToElements(Program.TM, false);
                }
            }
        }

        private void W7_ColorizationAfterglowBalance_bar_Scroll(object sender)
        {
            if (_Shown)
            {
                W7_ColorizationAfterglowBalance_val.Text = ((UI.WP.Trackbar)sender).Value.ToString();
                Program.TM.Windows7.ColorizationAfterglowBalance = W7_ColorizationAfterglowBalance_bar.Value;
                if (Program.PreviewStyle == WindowStyle.W7)
                    ApplyColorsToElements(Program.TM);
            }
        }
        private void W7_ColorizationColorBalance_val_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), W7_ColorizationColorBalance_bar.Maximum), W7_ColorizationColorBalance_bar.Minimum).ToString();
            W7_ColorizationColorBalance_bar.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void W7_ColorizationAfterglowBalance_val_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), W7_ColorizationAfterglowBalance_bar.Maximum), W7_ColorizationAfterglowBalance_bar.Minimum).ToString();
            W7_ColorizationAfterglowBalance_bar.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void W7_ColorizationBlurBalance_val_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), W7_ColorizationBlurBalance_bar.Maximum), W7_ColorizationBlurBalance_bar.Minimum).ToString();
            W7_ColorizationBlurBalance_bar.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void W7_ColorizationGlassReflectionIntensity_val_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), W7_ColorizationGlassReflectionIntensity_bar.Maximum), W7_ColorizationGlassReflectionIntensity_bar.Minimum).ToString();
            W7_ColorizationGlassReflectionIntensity_bar.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        #endregion

        #region Windows Vista
        private void WVista_pick_DragDrop(object sender, DragEventArgs e)
        {
            if (((UI.Controllers.ColorItem)sender).AllowDrop && Program.PreviewStyle == WindowStyle.WVista)
            {
                Program.TM.WindowsVista.ColorizationColor = WVista_ColorizationColor_pick.BackColor;
                ApplyColorsToElements(Program.TM);
            }
        }

        private void WVista_ColorizationColor_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (ColorClipboard.Event == ColorClipboard.MenuEvent.Cut | ColorClipboard.Event == ColorClipboard.MenuEvent.Paste | ColorClipboard.Event == ColorClipboard.MenuEvent.Override)
                {
                    Program.TM.WindowsVista.ColorizationColor = ((UI.Controllers.ColorItem)sender).BackColor;
                    if (Program.PreviewStyle == WindowStyle.WVista)
                        ApplyColorsToElements(Program.TM);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender, start, taskbar, Window1, Window2 };

            var _Conditions = new Conditions() { Win7 = true, Color1 = true, Background = true, LivePreview_Colorization = true, LivePreview_AfterGlow = true };

            var C = Forms.ColorPickerDlg.Pick(CList, _Conditions);

            Program.TM.WindowsVista.ColorizationColor = Color.FromArgb(255, C);

            if (Program.PreviewStyle == WindowStyle.WVista)
                ApplyColorsToElements(Program.TM);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void WVista_ColorizationColorBalance_bar_Scroll(object sender)
        {
            if (_Shown)
            {
                WVista_ColorizationColorBalance_val.Text = ((UI.WP.Trackbar)sender).Value.ToString();
                Program.TM.WindowsVista.Alpha = (byte)WVista_ColorizationColorBalance_bar.Value;
                if (Program.PreviewStyle == WindowStyle.WVista)
                    ApplyColorsToElements(Program.TM);
            }
        }

        private void WVista_theme_classic_CheckedChanged(object sender)
        {
            if (WVista_theme_classic.Checked)
            {
                Program.TM.WindowsVista.Theme = Theme.Structures.Windows7.Themes.Classic;
                if (Program.PreviewStyle == WindowStyle.WVista)
                {
                    ApplyColorsToElements(Program.TM);
                    ApplyStylesToElements(Program.TM, false);
                }
            }

        }

        private void WVista_theme_basic_CheckedChanged(object sender)
        {
            if (WVista_theme_basic.Checked)
            {
                Program.TM.WindowsVista.Theme = Theme.Structures.Windows7.Themes.Basic;
                if (Program.PreviewStyle == WindowStyle.WVista)
                {
                    ApplyColorsToElements(Program.TM);
                    ApplyStylesToElements(Program.TM, false);
                }
            }
        }

        private void WVista_theme_aeroopaque_CheckedChanged(object sender)
        {
            if (WVista_theme_aeroopaque.Checked)
            {
                Program.TM.WindowsVista.Theme = Theme.Structures.Windows7.Themes.AeroOpaque;
                if (Program.PreviewStyle == WindowStyle.WVista)
                {
                    ApplyColorsToElements(Program.TM);
                    ApplyStylesToElements(Program.TM, false);
                }
            }
        }

        private void WVista_theme_Vista_CheckedChanged(object sender)
        {
            if (WVista_theme_aero.Checked)
            {
                Program.TM.WindowsVista.Theme = Theme.Structures.Windows7.Themes.Aero;
                if (Program.PreviewStyle == WindowStyle.WVista)
                {
                    ApplyColorsToElements(Program.TM);
                    ApplyStylesToElements(Program.TM, false);
                }
            }
        }

        private void WVista_ColorizationColorBalance_val_Click(object sender, EventArgs e)
        {
            string response = InputBox(Program.Lang.InputValue, ((UI.WP.Button)sender).Text, Program.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), WVista_ColorizationColorBalance_bar.Maximum), WVista_ColorizationColorBalance_bar.Minimum).ToString();
            WVista_ColorizationColorBalance_bar.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        #endregion

        #region Windows XP
        private void WXP_Luna_Blue_CheckedChanged(object sender)
        {
            if (WXP_Luna_Blue.Checked)
            {
                Program.TM.WindowsXP.Theme = Theme.Structures.WindowsXP.Themes.LunaBlue;
                if (Program.PreviewStyle == WindowStyle.WXP)
                    ApplyStylesToElements(Program.TM, false);
            }
        }

        private void WXP_Luna_OliveGreen_CheckedChanged(object sender)
        {
            if (WXP_Luna_OliveGreen.Checked)
            {
                Program.TM.WindowsXP.Theme = Theme.Structures.WindowsXP.Themes.LunaOliveGreen;
                if (Program.PreviewStyle == WindowStyle.WXP)
                    ApplyStylesToElements(Program.TM, false);
            }
        }

        private void WXP_Luna_Silver_CheckedChanged(object sender)
        {
            if (WXP_Luna_Silver.Checked)
            {
                Program.TM.WindowsXP.Theme = Theme.Structures.WindowsXP.Themes.LunaSilver;
                if (Program.PreviewStyle == WindowStyle.WXP)
                    ApplyStylesToElements(Program.TM, false);
            }
        }

        private void WXP_CustomTheme_CheckedChanged(object sender)
        {
            if (WXP_CustomTheme.Checked)
            {
                Program.TM.WindowsXP.Theme = Theme.Structures.WindowsXP.Themes.Custom;
                if (Program.PreviewStyle == WindowStyle.WXP)
                    ApplyStylesToElements(Program.TM, false);
            }
        }

        private void WXP_Classic_CheckedChanged(object sender)
        {
            if (WXP_Classic.Checked)
            {
                Program.TM.WindowsXP.Theme = Theme.Structures.WindowsXP.Themes.Classic;
                if (Program.PreviewStyle == WindowStyle.WXP)
                    ApplyStylesToElements(Program.TM, false);
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            string theme = "";

            if (System.IO.Path.GetExtension(WXP_VS_textbox.Text) == ".theme")
            {
                theme = WXP_VS_textbox.Text;
            }

            else if (System.IO.Path.GetExtension(WXP_VS_textbox.Text) == ".msstyles")
            {
                theme = PathsExt.appData + @"\VisualStyles\Luna\custom.theme";

                if (!System.IO.Directory.Exists(PathsExt.MSTheme_Dir))
                {
                    System.IO.Directory.CreateDirectory(PathsExt.MSTheme_Dir);
                }

                System.IO.File.WriteAllText(PathsExt.appData + @"\VisualStyles\Luna\custom.theme", string.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=NormalColor{1}Size=NormalSize", WXP_VS_textbox.Text, "\r\n"));

            }

            Program.TM.WindowsXP.ThemeFile = WXP_VS_textbox.Text;

            if ((System.IO.File.Exists(WXP_VS_textbox.Text) && System.IO.File.Exists(theme)) & !string.IsNullOrEmpty(theme))
            {

                var vs = new VisualStyleFile(theme);

                WXP_VS_ColorsList.Items.Clear();

                try
                {
                    foreach (var x in vs.ColorSchemes)
                        WXP_VS_ColorsList.Items.Add(x.Name);
                }
                catch
                {

                }

                if (WXP_VS_ColorsList.Items.Count > 0)
                    WXP_VS_ColorsList.SelectedIndex = 0;

                if (WXP_CustomTheme.Checked & Program.PreviewStyle == WindowStyle.WXP)
                    ApplyStylesToElements(Program.TM, false);
            }
        }

        private void Button30_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog2.ShowDialog() == DialogResult.OK)
            {
                WXP_VS_textbox.Text = OpenFileDialog2.FileName;
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_Shown && WXP_CustomTheme.Checked)
            {
                Program.TM.WindowsXP.ColorScheme = WXP_VS_ColorsList.SelectedItem.ToString();
                if (Program.PreviewStyle == WindowStyle.WXP)
                    ApplyStylesToElements(Program.TM, false);
            }
        }
        #endregion

        private void Button4_Click(object sender, EventArgs e)
        {
            Forms.ThemeLog.Apply_Theme();
        }

        private void Apply_btn_MouseEnter(object sender, EventArgs e)
        {
            if (Program.Settings.ThemeApplyingBehavior.AutoRestartExplorer)
            {
                status_lbl.Text = Program.Lang.ThisWillRestartExplorer;
                status_lbl.ForeColor = Program.Style.DarkMode ? Color.Gold : Color.Gold.Dark(0.1f);
            }
        }

        private void Apply_btn_MouseLeave(object sender, EventArgs e)
        {
            if (Program.Settings.ThemeApplyingBehavior.AutoRestartExplorer)
            {
                status_lbl.Text = "";
                status_lbl.ForeColor = Program.Style.DarkMode ? Color.White : Color.Black;
            }
        }

        private void Button19_MouseEnter(object sender, EventArgs e)
        {
            status_lbl.Text = Program.Lang.ThisWillRestartExplorer;
            status_lbl.ForeColor = Program.Style.DarkMode ? Color.Gold : Color.Gold.Dark(0.1f);
        }

        private void Button19_MouseLeave(object sender, EventArgs e)
        {
            status_lbl.Text = "";
            status_lbl.ForeColor = Program.Style.DarkMode ? Color.White : Color.Black;
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(SaveFileDialog1.FileName))
            {
                if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Program.TM.Save(Theme.Manager.Source.File, SaveFileDialog1.FileNames[0]);
                }
            }
            else
            {
                Program.TM.Save(Theme.Manager.Source.File, SaveFileDialog1.FileNames[0]);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {

                Forms.ComplexSave.GetResponse(SaveFileDialog1, () => Forms.ThemeLog.Apply_Theme(), () => Forms.ThemeLog.Apply_Theme(Program.TM_FirstTime), () => Forms.ThemeLog.Apply_Theme(Theme.Default.Get()));

                SaveFileDialog1.FileName = OpenFileDialog1.FileName;
                Program.TM = new Theme.Manager(Theme.Manager.Source.File, OpenFileDialog1.FileName);
                Program.TM_Original = (Theme.Manager)Program.TM.Clone();

                ApplyStylesToElements(Program.TM, false);
                LoadFromTM(Program.TM);
                ApplyColorsToElements(Program.TM);
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Program.TM.Save(Theme.Manager.Source.File, SaveFileDialog1.FileNames[0]);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {

            Forms.ComplexSave.GetResponse(SaveFileDialog1, () => Forms.ThemeLog.Apply_Theme(), () => Forms.ThemeLog.Apply_Theme(Program.TM_FirstTime), () => Forms.ThemeLog.Apply_Theme(Theme.Default.Get()));

            Program.TM = new Theme.Manager(Theme.Manager.Source.Registry);
            Program.TM_Original = (Theme.Manager)Program.TM.Clone();
            SaveFileDialog1.FileName = null;
            ApplyStylesToElements(Program.TM, false);
            LoadFromTM(Program.TM);
            ApplyColorsToElements(Program.TM);
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            Forms.EditInfo.Show();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            Forms.About.ShowDialog();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Forms.Updates.ShowDialog();
            Button5.Image = Properties.Resources.Update;
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            Forms.SettingsX.Show();
        }

        private void Button4_Click_1(object sender, EventArgs e)
        {
            Forms.Win32UI.Show();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Forms.Whatsnew.ShowDialog();
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            if (Program.PreviewStyle == WindowStyle.W12 || Program.PreviewStyle == WindowStyle.W11 || Program.PreviewStyle == WindowStyle.W10)
            {
                Forms.LogonUI.ShowDialog();
            }
            else if (Program.PreviewStyle == WindowStyle.W81 | Program.PreviewStyle == WindowStyle.W7)
            {
                Forms.LogonUI7.Show();
            }
            else if (Program.PreviewStyle == WindowStyle.WXP)
            {
                Forms.LogonUIXP.Show();
            }
            else if (Program.PreviewStyle == WindowStyle.WVista)
            {
                MsgBox(Program.Lang.VistaLogonNotSupported, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Forms.LogonUI.ShowDialog();
            }
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            Update_Wallpaper_Preview();
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            if (SaveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                tabs_preview.ToBitmap().Save(SaveFileDialog2.FileName);
            }
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            Program.TM = (Theme.Manager)Program.TM_Original.Clone();
            ApplyStylesToElements(Program.TM, false);
            LoadFromTM(Program.TM);
            ApplyColorsToElements(Program.TM);
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            Program.TM = (Theme.Manager)Program.TM_FirstTime.Clone();
            ApplyStylesToElements(Program.TM, false);
            LoadFromTM(Program.TM);
            ApplyColorsToElements(Program.TM);
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            Program.RestartExplorer();
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            Forms.ComplexSave.GetResponse(SaveFileDialog1, () => Forms.ThemeLog.Apply_Theme(), () => Forms.ThemeLog.Apply_Theme(Program.TM_FirstTime), () => Forms.ThemeLog.Apply_Theme(Theme.Default.Get()));

            Program.TM = (Theme.Manager)Theme.Default.Get().Clone();
            SaveFileDialog1.FileName = null;
            LoadFromTM(Program.TM);
            ApplyStylesToElements(Program.TM, false);
            ApplyColorsToElements(Program.TM);
        }

        private void Button21_Click(object sender, EventArgs e)
        {
            Forms.CursorsStudio.Show();
        }

        private void Button23_Click(object sender, EventArgs e)
        {
            if ((Button23.Text.ToLower() ?? "") == (Program.Lang.Hide.ToLower() ?? ""))
            {
                tabs_preview.Visible = false;
                Button23.Text = Program.Lang.Show;
            }
            else
            {
                tabs_preview.Visible = true;
                Button23.Text = Program.Lang.Hide;
            }
        }

        private void Button24_Click(object sender, EventArgs e)
        {
            Forms.TerminalsDashboard.ShowDialog();
        }

        private void MainFrm_ResizeBegin(object sender, EventArgs e)
        {
            SuspendLayout();
            // previewContainer.Visible = False
            // TablessControl1.Visible = False
        }

        private void MainFrm_ResizeEnd(object sender, EventArgs e)
        {
            ResumeLayout();
            // previewContainer.Visible = True
            // TablessControl1.Visible = True
        }

        private void Button27_Click(object sender, EventArgs e)
        {
            Forms.Metrics_Fonts.ShowDialog();
        }

        public void Select_Preview_Version()
        {

            Program.Animator.HideSync(TablessControl1, true);
            Program.Animator.HideSync(tabs_preview, true);

            UpdateLegends();

            ApplyColorsToElements(Program.TM);
            ApplyStylesToElements(Program.TM);
            ApplyDefaultTMValues();
            SelectLeftPanelIndex();

            if (Program.PreviewStyle == WindowStyle.W12)
            {
                Button20.Image = Properties.Resources.add_win12;
            }
            else if (Program.PreviewStyle == WindowStyle.W11)
            {
                Button20.Image = Properties.Resources.add_win11;
            }
            else if (Program.PreviewStyle == WindowStyle.W10)
            {
                Button20.Image = Properties.Resources.add_win10;
            }
            else if (Program.PreviewStyle == WindowStyle.W81)
            {
                Button20.Image = Properties.Resources.add_win8;
            }
            else if (Program.PreviewStyle == WindowStyle.W7)
            {
                Button20.Image = Properties.Resources.add_win7;
            }
            else if (Program.PreviewStyle == WindowStyle.WVista)
            {
                Button20.Image = Properties.Resources.add_winvista;
            }
            else if (Program.PreviewStyle == WindowStyle.WXP)
            {
                Button20.Image = Properties.Resources.add_winxp;
            }
            else
            {
                Button20.Image = Properties.Resources.add_win11;
            }


            Program.Animator.ShowSync(TablessControl1, true);
            Program.Animator.ShowSync(tabs_preview, true);

        }

        private void Select_W11_CheckedChanged(object sender)
        {
            if (_Shown & Select_W11.Checked)
            {
                Program.PreviewStyle = WindowStyle.W11;
                Select_Preview_Version();
            }
        }

        private void Select_W10_CheckedChanged(object sender)
        {
            if (_Shown & Select_W10.Checked)
            {
                Program.PreviewStyle = WindowStyle.W10;
                Select_Preview_Version();
            }
        }

        private void Select_W8_CheckedChanged(object sender)
        {
            if (_Shown & Select_W81.Checked)
            {
                Program.PreviewStyle = WindowStyle.W81;
                Select_Preview_Version();
            }
        }

        private void Select_W7_CheckedChanged(object sender)
        {
            if (_Shown & Select_W7.Checked)
            {
                Program.PreviewStyle = WindowStyle.W7;
                Select_Preview_Version();
            }
        }

        private void Select_WVista_CheckedChanged(object sender)
        {
            if (_Shown & Select_WVista.Checked)
            {
                Program.PreviewStyle = WindowStyle.WVista;
                Select_Preview_Version();
            }
        }

        private void Select_WXP_CheckedChanged(object sender)
        {
            if (_Shown & Select_WXP.Checked)
            {
                Program.PreviewStyle = WindowStyle.WXP;
                Select_Preview_Version();
            }
        }

        private void Button29_Click(object sender, EventArgs e)
        {
            Forms.WinEffecter.ShowDialog();
        }

        private void Button32_Click(object sender, EventArgs e)
        {
            if (Program.PreviewStyle != WindowStyle.WXP && Program.PreviewStyle != WindowStyle.WVista)
            {
                Forms.AltTabEditor.ShowDialog();
            }
            else
            {
                if (Program.PreviewStyle == WindowStyle.WXP)
                    MsgBox(string.Format(Program.Lang.AltTab_Unsupported, Program.Lang.OS_WinXP), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (Program.PreviewStyle == WindowStyle.WVista)
                    MsgBox(string.Format(Program.Lang.AltTab_Unsupported, Program.Lang.OS_WinVista), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void Button33_Click(object sender, EventArgs e)
        {
            Forms.ScreenSaver_Editor.ShowDialog();
        }

        private void Button34_Click(object sender, EventArgs e)
        {
            Forms.Sounds_Editor.ShowDialog();
        }

        private void Button35_Click(object sender, EventArgs e)
        {
            if (Program.PreviewStyle == WindowStyle.W12)
            {
                Forms.Wallpaper_Editor.WT = Program.TM.WallpaperTone_W12;
            }
            else if (Program.PreviewStyle == WindowStyle.W11)
            {
                Forms.Wallpaper_Editor.WT = Program.TM.WallpaperTone_W11;
            }
            else if (Program.PreviewStyle == WindowStyle.W10)
            {
                Forms.Wallpaper_Editor.WT = Program.TM.WallpaperTone_W10;
            }
            else if (Program.PreviewStyle == WindowStyle.W81)
            {
                Forms.Wallpaper_Editor.WT = Program.TM.WallpaperTone_W81;
            }
            else if (Program.PreviewStyle == WindowStyle.W7)
            {
                Forms.Wallpaper_Editor.WT = Program.TM.WallpaperTone_W7;
            }
            else if (Program.PreviewStyle == WindowStyle.WVista)
            {
                Forms.Wallpaper_Editor.WT = Program.TM.WallpaperTone_WVista;
            }
            else if (Program.PreviewStyle == WindowStyle.WXP)
            {
                Forms.Wallpaper_Editor.WT = Program.TM.WallpaperTone_WXP;
            }
            else
            {
                Forms.Wallpaper_Editor.WT = Program.TM.WallpaperTone_W12;
            }

            Forms.Wallpaper_Editor.Show();
        }

        private void Button26_Click(object sender, EventArgs e)
        {
            Forms.ApplicationThemer.FixLanguageDarkModeBug = false;
            Forms.ApplicationThemer.Show();
        }

        private void Button36_Click(object sender, EventArgs e)
        {
            Forms.Converter_Form.ShowDialog();
        }

        private void Button28_Click(object sender, EventArgs e)
        {

            if (MsgBox(Program.Lang.LogoffQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question, Program.Lang.LogoffAlert1, "", "", "", "", Program.Lang.LogoffAlert2, Ookii.Dialogs.WinForms.TaskDialogIcon.Information) == DialogResult.Yes)
            {
                LoggingOff = true;
                IntPtr intPtr = IntPtr.Zero;
                Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);
                if (System.IO.File.Exists(PathsExt.System32 + @"\logoff.exe"))
                {
                    Interaction.Shell(PathsExt.System32 + @"\logoff.exe", AppWinStyle.Hide);
                }
                else
                {
                    MsgBox(string.Format(Program.Lang.LogoffNotFound, PathsExt.System32), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

        }

        private void Button28_MouseEnter(object sender, EventArgs e)
        {
            status_lbl.Text = Program.Lang.LogoffNotice;
            status_lbl.ForeColor = Program.Style.DarkMode ? Color.Gold : Color.Gold.Dark(0.1f);
        }

        private void Button28_MouseLeave(object sender, EventArgs e)
        {
            status_lbl.Text = "";
            status_lbl.ForeColor = Program.Style.DarkMode ? Color.White : Color.Black;
        }

        private void Button39_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Resources.Link_Wiki);
        }

        private void Button40_Click(object sender, EventArgs e)
        {
            Forms.PaletteGenerateDashboard.ShowDialog();
        }

        private void button41_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Resources.Link_PayPal);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Forms.RescueTools.Show();
        }

        private void userButton_Click(object sender, EventArgs e)
        {
            User.Login(true);
        }

        private void Button30_Click_1(object sender, EventArgs e)
        {
            MsgBox(Program.Lang.Win11ColorsDescTip, MessageBoxButtons.OK, MessageBoxIcon.Information, Program.Lang.Win11ColorsDescTip2);
        }

        private void Button31_Click(object sender, EventArgs e)
        {
            if (OS.WXP)
            {
                if (MsgBox(string.Format(Program.Lang.Store_WontWork_Protocol, Program.Lang.OS_WinXP), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
            }
            else if (OS.WVista)
            {
                if (MsgBox(string.Format(Program.Lang.Store_WontWork_Protocol, Program.Lang.OS_WinVista), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
            }

            Forms.Store.Show();
        }
    }
}