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
using System.Text;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.CP;
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
        private List<string> Updates_ls = new List<string>();
        private bool LoggingOff = false;
        private readonly Converter _Converter = new Converter();
        private int elapsedSecs = 0;
        private int OldWidth;

        public MainFrm()
        {
            InitializeComponent();
        }

        #region Preview Subs
        public void ApplyColorsToElements(CP CP)
        {
            ApplyWinElementsColors(CP, My.Env.PreviewStyle, true, taskbar, start, ActionCenter, setting_icon_preview, Label8, lnk_preview);
            ApplyWindowStyles(CP, My.Env.PreviewStyle, Window1, Window2, W81_start, W81_logonui);
        }
        public void ApplyStylesToElements(CP CP, bool AnimateThePreview = true)
        {
            bool ItWasVisible = tabs_preview.Visible;

            if (AnimateThePreview & ItWasVisible)
            {
                if (_Shown)
                {
                    if (tabs_preview.Visible)
                        My.Env.Animator.HideSync(tabs_preview);
                }
                else
                {
                    tabs_preview.Visible = false;
                }
            }

            My.Env.Wallpaper = My.MyProject.Application.FetchSuitableWallpaper(CP, My.Env.PreviewStyle);
            pnl_preview.BackgroundImage = My.Env.Wallpaper;
            pnl_preview_classic.BackgroundImage = My.Env.Wallpaper;

            ApplyWinElementsStyle(CP, My.Env.PreviewStyle, taskbar, start, ActionCenter, Window1, Window2, Panel3, lnk_preview, ClassicTaskbar, ButtonR2, ButtonR3, ButtonR4, ClassicWindow1, ClassicWindow2, WXP_VS_ReplaceColors.Checked, WXP_VS_ReplaceMetrics.Checked, WXP_VS_ReplaceFonts.Checked);

            Button23.Visible = My.Env.PreviewStyle == WindowStyle.W7;

            AdjustPreview_ModernOrClassic(CP, My.Env.PreviewStyle, tabs_preview, WXP_Alert2);

            // ReValidateLivePreview(tabs_preview)

            if (AnimateThePreview & ItWasVisible)
            {
                if (_Shown)
                {
                    My.Env.Animator.ShowSync(tabs_preview);
                }
                else
                {
                    tabs_preview.Visible = true;
                }
            }
        }
        public void ApplyCPValues(CP CP)
        {
            themename_lbl.Text = string.Format("{0} ({1})", CP.Info.ThemeName, CP.Info.ThemeVersion);
            author_lbl.Text = string.Format("{0} {1}", My.Env.Lang.By, CP.Info.Author);

            {
                ref var temp = ref My.Env.Settings.Appearance;
                temp.CustomColors = CP.AppTheme.Enabled;
                temp.BackColor = CP.AppTheme.BackColor;
                temp.AccentColor = CP.AppTheme.AccentColor;
                temp.CustomTheme = CP.AppTheme.DarkMode;
                temp.RoundedCorners = CP.AppTheme.RoundCorners;
            }
            WPStyle.ApplyStyle(this);

            W11_WinMode_Toggle.Checked = !CP.Windows11.WinMode_Light;
            W11_AppMode_Toggle.Checked = !CP.Windows11.AppMode_Light;
            W11_Transparency_Toggle.Checked = CP.Windows11.Transparency;
            W11_ShowAccentOnTitlebarAndBorders_Toggle.Checked = CP.Windows11.ApplyAccentOnTitlebars;
            switch (CP.Windows11.ApplyAccentOnTaskbar)
            {
                case Structures.Windows10x.AccentTaskbarLevels.None:
                    {
                        W11_Accent_None.Checked = true;
                        break;
                    }

                case Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC:
                    {
                        W11_Accent_StartTaskbar.Checked = true;
                        break;
                    }

                case Structures.Windows10x.AccentTaskbarLevels.Taskbar:
                    {
                        W11_Accent_Taskbar.Checked = true;
                        break;
                    }

            }
            W11_ActiveTitlebar_pick.BackColor = CP.Windows11.Titlebar_Active;
            W11_InactiveTitlebar_pick.BackColor = CP.Windows11.Titlebar_Inactive;
            W11_Color_Index5.BackColor = CP.Windows11.StartMenu_Accent;
            W11_Color_Index4.BackColor = CP.Windows11.Color_Index2;
            W11_Color_Index6.BackColor = CP.Windows11.Color_Index6;
            W11_Color_Index1.BackColor = CP.Windows11.Color_Index1;
            W11_Color_Index2.BackColor = CP.Windows11.Color_Index4;
            W11_TaskbarFrontAndFoldersOnStart_pick.BackColor = CP.Windows11.Color_Index5;
            W11_Color_Index0.BackColor = CP.Windows11.Color_Index0;
            W11_Color_Index3.BackColor = CP.Windows11.Color_Index3;
            W11_Color_Index7.BackColor = CP.Windows11.Color_Index7;

            W10_WinMode_Toggle.Checked = !CP.Windows10.WinMode_Light;
            W10_AppMode_Toggle.Checked = !CP.Windows10.AppMode_Light;
            W10_Transparency_Toggle.Checked = CP.Windows10.Transparency;
            W10_TBTransparency_Toggle.Checked = CP.Windows10.IncreaseTBTransparency;
            W10_TB_Blur.Checked = CP.Windows10.TB_Blur;
            W10_ShowAccentOnTitlebarAndBorders_Toggle.Checked = CP.Windows10.ApplyAccentOnTitlebars;
            switch (CP.Windows10.ApplyAccentOnTaskbar)
            {
                case Structures.Windows10x.AccentTaskbarLevels.None:
                    {
                        W10_Accent_None.Checked = true;
                        break;
                    }

                case Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC:
                    {
                        W10_Accent_StartTaskbar.Checked = true;
                        break;
                    }

                case Structures.Windows10x.AccentTaskbarLevels.Taskbar:
                    {
                        W10_Accent_Taskbar.Checked = true;
                        break;
                    }
            }
            W10_ActiveTitlebar_pick.BackColor = CP.Windows10.Titlebar_Active;
            W10_InactiveTitlebar_pick.BackColor = CP.Windows10.Titlebar_Inactive;
            W10_Color_Index5.BackColor = CP.Windows10.StartMenu_Accent;
            W10_Color_Index4.BackColor = CP.Windows10.Color_Index2;
            W10_Color_Index6.BackColor = CP.Windows10.Color_Index6;
            W10_Color_Index1.BackColor = CP.Windows10.Color_Index1;
            W10_Color_Index2.BackColor = CP.Windows10.Color_Index4;
            W10_TaskbarFrontAndFoldersOnStart_pick.BackColor = CP.Windows10.Color_Index5;
            W10_Color_Index0.BackColor = CP.Windows10.Color_Index0;
            W10_Color_Index3.BackColor = CP.Windows10.Color_Index3;
            W10_Color_Index7.BackColor = CP.Windows10.Color_Index7;

            switch (CP.Windows81.Theme)
            {
                case Structures.Windows7.Themes.Aero:
                    {
                        W81_theme_aero.Checked = true;
                        break;
                    }

                case Structures.Windows7.Themes.AeroLite:
                    {
                        W81_theme_aerolite.Checked = true;
                        break;
                    }
            }
            W81_ColorizationColor_pick.BackColor = CP.Windows81.ColorizationColor;
            W81_ColorizationBalance_bar.Value = CP.Windows81.ColorizationColorBalance;
            W81_ColorizationBalance_val.Text = CP.Windows81.ColorizationColorBalance.ToString();
            W81_start_pick.BackColor = CP.Windows81.StartColor;
            W81_accent_pick.BackColor = CP.Windows81.AccentColor;
            W81_personalcls_background_pick.BackColor = CP.Windows81.PersonalColors_Background;
            W81_personalcolor_accent_pick.BackColor = CP.Windows81.PersonalColors_Accent;

            W7_ColorizationColor_pick.BackColor = CP.Windows7.ColorizationColor;
            W7_ColorizationAfterglow_pick.BackColor = CP.Windows7.ColorizationAfterglow;
            W7_ColorizationColorBalance_bar.Value = CP.Windows7.ColorizationColorBalance;
            W7_ColorizationAfterglowBalance_bar.Value = CP.Windows7.ColorizationAfterglowBalance;
            W7_ColorizationBlurBalance_bar.Value = CP.Windows7.ColorizationBlurBalance;
            W7_ColorizationGlassReflectionIntensity_bar.Value = CP.Windows7.ColorizationGlassReflectionIntensity;
            W7_ColorizationColorBalance_val.Text = CP.Windows7.ColorizationColorBalance.ToString();
            W7_ColorizationAfterglowBalance_val.Text = CP.Windows7.ColorizationAfterglowBalance.ToString();
            W7_ColorizationBlurBalance_val.Text = CP.Windows7.ColorizationBlurBalance.ToString();
            W7_ColorizationGlassReflectionIntensity_val.Text = CP.Windows7.ColorizationGlassReflectionIntensity.ToString();
            W7_EnableAeroPeek_toggle.Checked = CP.Windows7.EnableAeroPeek;
            W7_AlwaysHibernateThumbnails_Toggle.Checked = CP.Windows7.AlwaysHibernateThumbnails;
            switch (CP.Windows7.Theme)
            {
                case Structures.Windows7.Themes.Aero:
                    {
                        W7_theme_aero.Checked = true;
                        break;
                    }

                case Structures.Windows7.Themes.AeroOpaque:
                    {
                        W7_theme_aeroopaque.Checked = true;
                        break;
                    }

                case Structures.Windows7.Themes.Basic:
                    {
                        W7_theme_basic.Checked = true;
                        break;
                    }

                case Structures.Windows7.Themes.Classic:
                    {
                        W7_theme_classic.Checked = true;
                        break;
                    }
            }

            WVista_ColorizationColor_pick.BackColor = CP.WindowsVista.ColorizationColor;
            WVista_ColorizationColorBalance_bar.Value = CP.WindowsVista.Alpha;
            WVista_ColorizationColorBalance_val.Text = CP.WindowsVista.Alpha.ToString();
            switch (CP.WindowsVista.Theme)
            {
                case Structures.Windows7.Themes.Aero:
                    {
                        WVista_theme_aero.Checked = true;
                        break;
                    }

                case Structures.Windows7.Themes.AeroOpaque:
                    {
                        WVista_theme_aeroopaque.Checked = true;
                        break;
                    }

                case Structures.Windows7.Themes.Basic:
                    {
                        WVista_theme_basic.Checked = true;
                        break;
                    }

                case Structures.Windows7.Themes.Classic:
                    {
                        WVista_theme_classic.Checked = true;
                        break;
                    }
            }

            switch (CP.WindowsXP.Theme)
            {
                case Structures.WindowsXP.Themes.LunaBlue:
                    {
                        WXP_Luna_Blue.Checked = true;
                        break;
                    }

                case Structures.WindowsXP.Themes.LunaOliveGreen:
                    {
                        WXP_Luna_OliveGreen.Checked = true;
                        break;
                    }

                case Structures.WindowsXP.Themes.LunaSilver:
                    {
                        WXP_Luna_Silver.Checked = true;
                        break;
                    }

                case Structures.WindowsXP.Themes.Custom:
                    {
                        WXP_CustomTheme.Checked = true;
                        break;
                    }

                case Structures.WindowsXP.Themes.Classic:
                    {
                        WXP_Classic.Checked = true;
                        break;
                    }

            }
            WXP_VS_textbox.Text = CP.WindowsXP.ThemeFile;
            if (WXP_VS_ColorsList.Items.Contains(CP.WindowsXP.ColorScheme))
                WXP_VS_ColorsList.SelectedItem = CP.WindowsXP.ColorScheme;

            ApplyMetroStartToButton(CP, W81_start);
            ApplyBackLogonUI(CP, W81_logonui);
        }
        public void ApplyDefaultCPValues()
        {
            CP DefCP;

            if (My.Env.W11)
            {
                DefCP = new CP_Defaults().Default_Windows11();
            }
            else if (My.Env.W10)
            {
                DefCP = new CP_Defaults().Default_Windows10();
            }
            else if (My.Env.W81)
            {
                DefCP = new CP_Defaults().Default_Windows81();
            }
            else if (My.Env.W7)
            {
                DefCP = new CP_Defaults().Default_Windows7();
            }
            else if (My.Env.WVista)
            {
                DefCP = new CP_Defaults().Default_WindowsVista();
            }
            else if (My.Env.WXP)
            {
                DefCP = new CP_Defaults().Default_WindowsXP();
            }
            else
            {
                DefCP = new CP_Defaults().Default_Windows11();
            }

            W11_ActiveTitlebar_pick.DefaultColor = DefCP.Windows11.Titlebar_Active;
            W11_InactiveTitlebar_pick.DefaultColor = DefCP.Windows11.Titlebar_Inactive;
            W11_Color_Index5.DefaultColor = DefCP.Windows11.StartMenu_Accent;
            W11_Color_Index4.DefaultColor = DefCP.Windows11.Color_Index2;
            W11_Color_Index6.DefaultColor = DefCP.Windows11.Color_Index6;
            W11_Color_Index1.DefaultColor = DefCP.Windows11.Color_Index1;
            W11_Color_Index2.DefaultColor = DefCP.Windows11.Color_Index4;
            W11_TaskbarFrontAndFoldersOnStart_pick.DefaultColor = DefCP.Windows11.Color_Index5;
            W11_Color_Index0.DefaultColor = DefCP.Windows11.Color_Index0;
            W11_Color_Index3.DefaultColor = DefCP.Windows11.Color_Index3;
            W11_Color_Index7.DefaultColor = DefCP.Windows11.Color_Index7;

            W10_ActiveTitlebar_pick.DefaultColor = DefCP.Windows10.Titlebar_Active;
            W10_InactiveTitlebar_pick.DefaultColor = DefCP.Windows10.Titlebar_Inactive;
            W10_Color_Index5.DefaultColor = DefCP.Windows10.StartMenu_Accent;
            W10_Color_Index4.DefaultColor = DefCP.Windows10.Color_Index2;
            W10_Color_Index6.DefaultColor = DefCP.Windows10.Color_Index6;
            W10_Color_Index1.DefaultColor = DefCP.Windows10.Color_Index1;
            W10_Color_Index2.DefaultColor = DefCP.Windows10.Color_Index4;
            W10_TaskbarFrontAndFoldersOnStart_pick.DefaultColor = DefCP.Windows10.Color_Index5;
            W10_Color_Index0.DefaultColor = DefCP.Windows10.Color_Index0;
            W10_Color_Index3.DefaultColor = DefCP.Windows10.Color_Index3;
            W10_Color_Index7.DefaultColor = DefCP.Windows10.Color_Index7;

            W81_ColorizationColor_pick.DefaultColor = DefCP.Windows7.ColorizationColor;
            W81_start_pick.DefaultColor = DefCP.Windows81.StartColor;
            W81_accent_pick.DefaultColor = DefCP.Windows81.AccentColor;
            W81_personalcls_background_pick.DefaultColor = DefCP.Windows81.PersonalColors_Background;
            W81_personalcolor_accent_pick.DefaultColor = DefCP.Windows81.PersonalColors_Accent;

            W7_ColorizationColor_pick.DefaultColor = DefCP.Windows7.ColorizationColor;
            W7_ColorizationAfterglow_pick.DefaultColor = DefCP.Windows7.ColorizationAfterglow;

            WVista_ColorizationColor_pick.DefaultColor = DefCP.WindowsVista.ColorizationColor;

            DefCP.Dispose();
        }
        public void Update_Wallpaper_Preview()
        {
            Cursor = Cursors.AppStarting;
            My.Env.Wallpaper = My.MyProject.Application.FetchSuitableWallpaper(My.Env.CP, My.Env.PreviewStyle);
            pnl_preview.BackgroundImage = My.Env.Wallpaper;
            pnl_preview_classic.BackgroundImage = My.Env.Wallpaper;
            ApplyColorsToElements(My.Env.CP);
            ApplyCPValues(My.Env.CP);
            ApplyStylesToElements(My.Env.CP, false);
            ReValidateLivePreview(pnl_preview);
            ReValidateLivePreview(pnl_preview_classic);
            Cursor = Cursors.Default;
        }
        public void SelectLeftPanelIndex()
        {
            if (My.Env.PreviewStyle == WindowStyle.W11)
            {
                TablessControl1.SelectedIndex = 0;
            }
            else if (My.Env.PreviewStyle == WindowStyle.W10)
            {
                TablessControl1.SelectedIndex = 1;
            }
            else if (My.Env.PreviewStyle == WindowStyle.W81)
            {
                TablessControl1.SelectedIndex = 2;
            }
            else if (My.Env.PreviewStyle == WindowStyle.W7)
            {
                TablessControl1.SelectedIndex = 3;
            }
            else if (My.Env.PreviewStyle == WindowStyle.WVista)
            {
                TablessControl1.SelectedIndex = 4;
            }
            else if (My.Env.PreviewStyle == WindowStyle.WXP)
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
            if (My.Env.PreviewStyle == WindowStyle.W11)
            {
                ApplyWin10xLegends(My.Env.CP, My.Env.PreviewStyle, W11_lbl1, W11_lbl2, W11_lbl3, W11_lbl4, W11_lbl5, W11_lbl6, W11_lbl7, W11_lbl8, W11_lbl9, W11_pic1, W11_pic2, W11_pic3, W11_pic4, W11_pic5, W11_pic6, W11_pic7, W11_pic8, W11_pic9);
            }

            else if (My.Env.PreviewStyle == WindowStyle.W10)
            {
                ApplyWin10xLegends(My.Env.CP, My.Env.PreviewStyle, W10_lbl1, W10_lbl2, W10_lbl3, W10_lbl4, W10_lbl5, W10_lbl6, W10_lbl7, W10_lbl8, W10_lbl9, W10_pic1, W10_pic2, W10_pic3, W10_pic4, W10_pic5, W10_pic6, W10_pic7, W10_pic8, W10_pic9);

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
            if (My.Env.WXP || My.Env.WVista)
                return;

            StableInt = 0;
            BetaInt = 0;
            UpdateChannel = 0;
            ChannelFixer = 0;
            if (My.Env.Settings.Updates.Channel == WPSettings.Structures.Updates.Channels.Stable)
                ChannelFixer = 0;
            if (My.Env.Settings.Updates.Channel == WPSettings.Structures.Updates.Channels.Beta)
                ChannelFixer = 1;
            BackgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (IsNetworkAvailable())
            {
                try
                {
                    var WebCL = new WebClient();
                    RaiseUpdate = false;
                    ver = "";

                    Updates_ls = WebCL.DownloadString(My.Resources.Link_Updates).CList();

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

                    RaiseUpdate = ver.CompareTo(My.Env.AppVersion) == +1;
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
                My.MyProject.Forms.Updates.ls = Updates_ls;
                NotifyUpdates.Visible = true;
                Button5.Image = My.Resources.Update_Dot;
                NotifyUpdates.ShowBalloonTip(10000, My.MyProject.Application.Info.Title, string.Format("{0}. {1} {2}", My.Env.Lang.NewUpdate, My.Env.Lang.Version, ver), ToolTipIcon.Info);
            }
        }

        private void NotifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            NotifyUpdates.Visible = false;

            if (My.MyProject.Application.OpenForms[My.MyProject.Forms.Updates.Name] is null)
            {
                My.MyProject.Forms.Updates.ShowDialog();
            }
            else
            {
                My.MyProject.Forms.Updates.Focus();
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
            TreeView1.ImageList = My.Env.Notifications_IL;

            Size = new Size(Conversions.ToInteger(My.Env.Settings.General.MainFormWidth), Conversions.ToInteger(My.Env.Settings.General.MainFormHeight));
            WindowState = (FormWindowState)Conversions.ToInteger(My.Env.Settings.General.MainFormStatus);

            Select_W11.Image = My.Resources.Native11;
            Select_W10.Image = My.Resources.Native10;
            Select_W81.Image = My.Resources.Native8;
            Select_W7.Image = My.Resources.Native7;
            Select_WVista.Image = My.Resources.NativeVista;
            Select_WXP.Image = My.Resources.NativeXP;
            if (!My.Env.isElevated)
                apply_btn.Image = My.Resources.WP_Admin;

            if (My.Env.PreviewStyle == WindowStyle.W11)
            {
                TablessControl1.SelectedIndex = 0;
                Button20.Image = My.Resources.add_win11;
                Select_W11.Checked = true;
            }

            else if (My.Env.PreviewStyle == WindowStyle.W10)
            {
                TablessControl1.SelectedIndex = 1;
                Button20.Image = My.Resources.add_win10;
                Select_W10.Checked = true;
            }

            else if (My.Env.PreviewStyle == WindowStyle.W81)
            {
                TablessControl1.SelectedIndex = 2;
                Button20.Image = My.Resources.add_win8;
                Select_W81.Checked = true;
            }

            else if (My.Env.PreviewStyle == WindowStyle.W7)
            {
                TablessControl1.SelectedIndex = 3;
                Button20.Image = My.Resources.add_win7;
                Select_W7.Checked = true;
            }

            else if (My.Env.PreviewStyle == WindowStyle.WVista)
            {
                TablessControl1.SelectedIndex = 4;
                Button20.Image = My.Resources.add_winvista;
                Select_WVista.Checked = true;
            }

            else if (My.Env.PreviewStyle == WindowStyle.WXP)
            {
                TablessControl1.SelectedIndex = 5;
                Button20.Image = My.Resources.add_winxp;
                Select_WXP.Checked = true;
            }

            else
            {
                TablessControl1.SelectedIndex = 0;
                Button20.Image = My.Resources.add_win11;
                Select_W11.Checked = true;
            }

            this.LoadLanguage();
            WPStyle.ApplyStyle(this);
            this.DoubleBuffer();
            UpdateLegends();
            ApplyColorsToElements(My.Env.CP);
            ApplyStylesToElements(My.Env.CP);
            ApplyCPValues(My.Env.CP);
            ApplyDefaultCPValues();

            WXP_Alert2.Size = WXP_Alert2.Parent.Size - new Size(40, 40);
            WXP_Alert2.Location = new Point(20, 20);

            Visible = true;

            BetaBadge.Visible = My.Env.IsBeta;
            if (My.Env.IsBeta)
            {
                status_lbl.Width = BetaBadge.Left - 5 - status_lbl.Left;
            }
            else
            {
                status_lbl.Width = BetaBadge.Right - status_lbl.Left;
            }
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

            if (My.Env.Settings.Updates.AutoCheck)
                AutoUpdatesCheck();

            if (My.MyProject.Application.ShowWhatsNew)
                My.MyProject.Forms.Whatsnew.ShowDialog();
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
                My.Env.Settings.General.MainFormWidth = Size.Width;
                My.Env.Settings.General.MainFormHeight = Size.Height;
            }
            if (WindowState != FormWindowState.Minimized)
            {
                My.Env.Settings.General.MainFormStatus = WindowState;
            }
            My.Env.Settings.General.Save();

            var old = new WPSettings(WPSettings.Mode.Registry);
            {
                ref var temp = ref My.Env.Settings.Appearance;
                temp.CustomColors = old.Appearance.CustomColors;
                temp.BackColor = old.Appearance.BackColor;
                temp.AccentColor = old.Appearance.AccentColor;
                temp.CustomTheme = old.Appearance.CustomTheme;
                temp.RoundedCorners = old.Appearance.RoundedCorners;
                temp.Save();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (My.Env.CP != My.Env.CP_Original)
            {

                if (My.Env.Settings.ThemeApplyingBehavior.ShowSaveConfirmation && !LoggingOff)
                {

                    switch (My.MyProject.Forms.ComplexSave.ShowDialog())
                    {
                        case DialogResult.Yes:
                            {

                                string[] r = My.Env.Settings.General.ComplexSaveResult.Split('.');
                                string r1 = r[0];
                                string r2 = r[1];

                                switch (r1 ?? "")
                                {
                                    case "0":              // ' Save
                                        {
                                            if (System.IO.File.Exists(SaveFileDialog1.FileName))
                                            {
                                                My.Env.CP.Save(CP_Type.File, SaveFileDialog1.FileName);
                                                My.Env.CP_Original = (CP)My.Env.CP.Clone();
                                            }
                                            else if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
                                            {
                                                My.Env.CP.Save(CP_Type.File, SaveFileDialog1.FileName);
                                                My.Env.CP_Original = (CP)My.Env.CP.Clone();
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
                                                My.Env.CP.Save(CP_Type.File, SaveFileDialog1.FileName);
                                                My.Env.CP_Original = (CP)My.Env.CP.Clone();
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
                                            Apply_Theme();
                                            break;
                                        }

                                    case "2":
                                        {
                                            Apply_Theme(My.Env.CP_FirstTime);
                                            break;
                                        }

                                    case "3":
                                        {
                                            Apply_Theme(CP_Defaults.GetDefault());
                                            break;
                                        }

                                }

                                break;
                            }

                        case DialogResult.No:
                            {
                                e.Cancel = false;
                                if ((My.Env.W7 | My.Env.W8 | My.Env.W81) & My.Env.Settings.Miscellaneous.Win7LivePreview)
                                    RefreshDWM(My.Env.CP_Original);
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
            if (((UI.Controllers.ColorItem)sender).AllowDrop && My.Env.PreviewStyle == WindowStyle.W11)
            {
                My.Env.CP.Windows11.Titlebar_Active = W11_ActiveTitlebar_pick.BackColor;
                My.Env.CP.Windows11.Titlebar_Inactive = W11_InactiveTitlebar_pick.BackColor;
                My.Env.CP.Windows11.StartMenu_Accent = W11_Color_Index5.BackColor;
                My.Env.CP.Windows11.Color_Index2 = W11_Color_Index4.BackColor;
                My.Env.CP.Windows11.Color_Index6 = W11_Color_Index6.BackColor;
                My.Env.CP.Windows11.Color_Index1 = W11_Color_Index1.BackColor;
                My.Env.CP.Windows11.Color_Index4 = W11_Color_Index2.BackColor;
                My.Env.CP.Windows11.Color_Index5 = W11_TaskbarFrontAndFoldersOnStart_pick.BackColor;
                My.Env.CP.Windows11.Color_Index0 = W11_Color_Index0.BackColor;
                My.Env.CP.Windows11.Color_Index3 = W11_Color_Index3.BackColor;
                My.Env.CP.Windows11.Color_Index7 = W11_Color_Index7.BackColor;
                ApplyColorsToElements(My.Env.CP);
            }
        }

        private void W11_ActiveTitlebar_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.Windows11.Titlebar_Active = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    if (My.Env.PreviewStyle == WindowStyle.W11)
                        ApplyColorsToElements(My.Env.CP);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender, Window1 };

            var C = My.MyProject.Forms.ColorPickerDlg.Pick(CList);
            My.Env.CP.Windows11.Titlebar_Active = Color.FromArgb(255, C);
            if (My.Env.PreviewStyle == WindowStyle.W11)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();

        }

        private void W11_InactiveTitlebar_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.Windows11.Titlebar_Inactive = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    if (My.Env.PreviewStyle == WindowStyle.W11)
                        ApplyColorsToElements(My.Env.CP);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender, Window2 };

            var _Conditions = new Conditions() { Window_InactiveTitlebar = true };
            var C = My.MyProject.Forms.ColorPickerDlg.Pick(CList, _Conditions);

            My.Env.CP.Windows11.Titlebar_Inactive = Color.FromArgb(255, C);

            if (My.Env.PreviewStyle == WindowStyle.W11)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W11_WinMode_Toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (_Shown)
            {
                My.Env.CP.Windows11.WinMode_Light = !((UI.WP.Toggle)sender).Checked;
                if (My.Env.PreviewStyle == WindowStyle.W11)
                {
                    UpdateLegends();
                    ApplyColorsToElements(My.Env.CP);
                }
            }
        }

        private void W11_AppMode_Toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (_Shown)
            {
                My.Env.CP.Windows11.AppMode_Light = !((UI.WP.Toggle)sender).Checked;
                if (My.Env.PreviewStyle == WindowStyle.W11)
                    ApplyColorsToElements(My.Env.CP);
            }
        }

        private void W11_Transparency_Toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (_Shown)
            {
                My.Env.CP.Windows11.Transparency = ((UI.WP.Toggle)sender).Checked;
                if (My.Env.PreviewStyle == WindowStyle.W11)
                {
                    UpdateLegends();
                    ApplyColorsToElements(My.Env.CP);
                }
            }
        }

        private void W11_ShowAccentOnTitlebarAndBorders_Toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (_Shown)
            {
                My.Env.CP.Windows11.ApplyAccentOnTitlebars = ((UI.WP.Toggle)sender).Checked;
                if (My.Env.PreviewStyle == WindowStyle.W11)
                    ApplyColorsToElements(My.Env.CP);
            }
        }

        private void W11_Accent_None_CheckedChanged(object sender)
        {
            if (_Shown & ((UI.WP.RadioImage)sender).Checked)
            {
                My.Env.CP.Windows11.ApplyAccentOnTaskbar = Structures.Windows10x.AccentTaskbarLevels.None;
                if (My.Env.PreviewStyle == WindowStyle.W11)
                {
                    UpdateLegends();
                    ApplyColorsToElements(My.Env.CP);
                }
            }
        }

        private void W11_Accent_Taskbar_CheckedChanged(object sender)
        {
            if (_Shown & ((UI.WP.RadioImage)sender).Checked)
            {
                My.Env.CP.Windows11.ApplyAccentOnTaskbar = Structures.Windows10x.AccentTaskbarLevels.Taskbar;
                if (My.Env.PreviewStyle == WindowStyle.W11)
                {
                    UpdateLegends();
                    ApplyColorsToElements(My.Env.CP);
                }
            }
        }

        private void W11_Accent_StartTaskbar_CheckedChanged(object sender)
        {
            if (_Shown & ((UI.WP.RadioImage)sender).Checked)
            {
                My.Env.CP.Windows11.ApplyAccentOnTaskbar = Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC;
                if (My.Env.PreviewStyle == WindowStyle.W11)
                {
                    UpdateLegends();
                    ApplyColorsToElements(My.Env.CP);
                }
            }
        }

        private void W11_Color_Index1_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.Windows11.Color_Index1 = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    if (My.Env.PreviewStyle == WindowStyle.W11)
                        ApplyColorsToElements(My.Env.CP);
                }
                return;
            }

            var CList = new List<Control>();
            Color C;
            CList.Add((Control)sender);

            if (ExplorerPatcher.IsAllowed())
            {
                CList.Add(taskbar);

                if (!My.Env.CP.Windows11.WinMode_Light)
                {
                    CList.Add(ActionCenter);
                    var _Conditions = new Conditions() { AppUnderlineOnly = true, ActionCenterBtn = true };
                    C = My.MyProject.Forms.ColorPickerDlg.Pick(CList, _Conditions);
                }
                else
                {
                    var _Conditions = new Conditions() { AppUnderlineWithTaskbar = true };
                    C = My.MyProject.Forms.ColorPickerDlg.Pick(CList, _Conditions);
                }
            }
            else if (!My.Env.CP.Windows11.WinMode_Light)
            {
                CList.Add(ActionCenter);
                CList.Add(taskbar);

                var _Conditions = new Conditions() { AppUnderlineOnly = true, ActionCenterBtn = true };
                C = My.MyProject.Forms.ColorPickerDlg.Pick(CList, _Conditions);
            }
            else
            {
                C = My.MyProject.Forms.ColorPickerDlg.Pick(CList);
            }

            My.Env.CP.Windows11.Color_Index1 = Color.FromArgb(255, C);
            if (My.Env.PreviewStyle == WindowStyle.W11)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W11_TaskbarFrontAndFoldersOnStart_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.Windows11.Color_Index5 = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    if (My.Env.PreviewStyle == WindowStyle.W11)
                        ApplyColorsToElements(My.Env.CP);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender };

            if (ExplorerPatcher.IsAllowed())
            {
                if (!My.Env.CP.Windows11.WinMode_Light)
                {
                    CList.Add(ActionCenter);
                    CList.Add(taskbar);
                    if (!My.Env.EP.UseStart10)
                    {
                        CList.Add(start);
                    }
                }
                else
                {
                    CList.Add(lnk_preview);
                }
            }

            else if (!My.Env.CP.Windows11.WinMode_Light)
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

            var C = My.MyProject.Forms.ColorPickerDlg.Pick(CList);
            My.Env.CP.Windows11.Color_Index5 = Color.FromArgb(255, C);
            if (My.Env.PreviewStyle == WindowStyle.W11)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W11_Color_Index0_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.Windows11.Color_Index0 = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    if (My.Env.PreviewStyle == WindowStyle.W11)
                        ApplyColorsToElements(My.Env.CP);
                }
                return;
            }

            var CList = new List<Control>();
            Color C;

            CList.Add((Control)sender);

            if (My.Env.CP.Windows11.WinMode_Light)
            {
                CList.Add(start);
                CList.Add(ActionCenter);
                C = My.MyProject.Forms.ColorPickerDlg.Pick(CList);
            }
            else
            {
                CList.Add(lnk_preview);
                var _Conditions = new Conditions() { AppUnderlineOnly = true };
                C = My.MyProject.Forms.ColorPickerDlg.Pick(CList, _Conditions);
            }

            My.Env.CP.Windows11.Color_Index0 = Color.FromArgb(255, C);
            if (My.Env.PreviewStyle == WindowStyle.W11)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();

        }

        private void W11_Color_Index3_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.Windows11.Color_Index3 = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    if (My.Env.PreviewStyle == WindowStyle.W11)
                        ApplyColorsToElements(My.Env.CP);
                }
                return;
            }

            var CList = new List<Control>();
            Color C;

            CList.Add((Control)sender);
            CList.Add(taskbar);
            CList.Add(setting_icon_preview);

            var _Conditions = new Conditions() { AppUnderlineOnly = true };
            C = My.MyProject.Forms.ColorPickerDlg.Pick(CList, _Conditions);

            My.Env.CP.Windows11.Color_Index3 = Color.FromArgb(255, C);
            if (My.Env.PreviewStyle == WindowStyle.W11)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W11_Color_Index6_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.Windows11.Color_Index6 = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    if (My.Env.PreviewStyle == WindowStyle.W11)
                        ApplyColorsToElements(My.Env.CP);
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

            C = My.MyProject.Forms.ColorPickerDlg.Pick(CList, _Conditions);

            My.Env.CP.Windows11.Color_Index6 = Color.FromArgb(255, C);
            if (My.Env.PreviewStyle == WindowStyle.W11)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W11_Color_Index5_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.Windows11.StartMenu_Accent = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    if (My.Env.PreviewStyle == WindowStyle.W11)
                        ApplyColorsToElements(My.Env.CP);
                }
                return;
            }


            var CList = new List<Control>() { (Control)sender };

            var C = My.MyProject.Forms.ColorPickerDlg.Pick(CList);
            My.Env.CP.Windows11.StartMenu_Accent = Color.FromArgb(255, C);
            if (My.Env.PreviewStyle == WindowStyle.W11)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();

        }

        private void W11_Color_Index4_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.Windows11.Color_Index2 = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    if (My.Env.PreviewStyle == WindowStyle.W11)
                        ApplyColorsToElements(My.Env.CP);
                }
                return;
            }


            var CList = new List<Control>() { (Control)sender };

            var C = My.MyProject.Forms.ColorPickerDlg.Pick(CList);
            My.Env.CP.Windows11.Color_Index2 = Color.FromArgb(255, C);
            if (My.Env.PreviewStyle == WindowStyle.W11)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W11_Color_Index2_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.Windows11.Color_Index4 = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    if (My.Env.PreviewStyle == WindowStyle.W11)
                        ApplyColorsToElements(My.Env.CP);
                }
                return;
            }


            var CList = new List<Control>() { (Control)sender };

            Color C;

            if (ExplorerPatcher.IsAllowed())
            {
                if (My.Env.EP.UseStart10)
                {
                    CList.Add(start);
                    C = My.MyProject.Forms.ColorPickerDlg.Pick(CList);
                }

                else if (My.Env.CP.Windows11.WinMode_Light)
                {
                    CList.Add(ActionCenter);

                    var _Conditions = new Conditions() { ActionCenterBtn = true };
                    C = My.MyProject.Forms.ColorPickerDlg.Pick(CList, _Conditions);
                }
                else
                {
                    CList.Add(start);

                    var _Conditions = new Conditions() { StartColorOnly = true };
                    C = My.MyProject.Forms.ColorPickerDlg.Pick(CList, _Conditions);
                }
            }

            else if (My.Env.CP.Windows11.WinMode_Light)
            {
                CList.Add(ActionCenter);
                CList.Add(taskbar);

                var _Conditions = new Conditions() { AppUnderlineOnly = true, ActionCenterBtn = true };

                C = My.MyProject.Forms.ColorPickerDlg.Pick(CList, _Conditions);
            }
            else
            {
                CList.Add(start);

                var _Conditions = new Conditions() { StartColorOnly = true };
                C = My.MyProject.Forms.ColorPickerDlg.Pick(CList, _Conditions);

            }


            My.Env.CP.Windows11.Color_Index4 = Color.FromArgb(255, C);
            if (My.Env.PreviewStyle == WindowStyle.W11)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W11_Color_Index7_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.Env.CP.Windows11.Color_Index7 = My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                return;
            }

            var CList = new List<Control>() { (Control)sender };
            var C = My.MyProject.Forms.ColorPickerDlg.Pick(CList);

            My.Env.CP.Windows11.Color_Index7 = Color.FromArgb(255, C);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W11_Button8_Click_1(object sender, EventArgs e)
        {
            WPStyle.MsgBox(My.Env.Lang.TitlebarColorNotice, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button38_Click(object sender, EventArgs e)
        {
            // Copycat from Windows 11 colors
            tabs_preview.Visible = false;
            My.Env.CP.Windows10 = (Structures.Windows10x)My.Env.CP.Windows11.Clone();
            ApplyCPValues(My.Env.CP);
            ApplyColorsToElements(My.Env.CP);
            tabs_preview.Visible = true;
        }
        #endregion

        #region Windows 10
        private void W10_pick_DragDrop(object sender, DragEventArgs e)
        {
            if (((UI.Controllers.ColorItem)sender).AllowDrop && My.Env.PreviewStyle == WindowStyle.W10)
            {
                My.Env.CP.Windows10.Titlebar_Active = W10_ActiveTitlebar_pick.BackColor;
                My.Env.CP.Windows10.Titlebar_Inactive = W10_InactiveTitlebar_pick.BackColor;
                My.Env.CP.Windows10.StartMenu_Accent = W10_Color_Index5.BackColor;
                My.Env.CP.Windows10.Color_Index2 = W10_Color_Index4.BackColor;
                My.Env.CP.Windows10.Color_Index6 = W10_Color_Index6.BackColor;
                My.Env.CP.Windows10.Color_Index1 = W10_Color_Index1.BackColor;
                My.Env.CP.Windows10.Color_Index4 = W10_Color_Index2.BackColor;
                My.Env.CP.Windows10.Color_Index5 = W10_TaskbarFrontAndFoldersOnStart_pick.BackColor;
                My.Env.CP.Windows10.Color_Index0 = W10_Color_Index0.BackColor;
                My.Env.CP.Windows10.Color_Index3 = W10_Color_Index3.BackColor;
                My.Env.CP.Windows10.Color_Index7 = W10_Color_Index7.BackColor;
                ApplyColorsToElements(My.Env.CP);
            }
        }

        private void W10_ActiveTitlebar_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.Windows10.Titlebar_Active = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    if (My.Env.PreviewStyle == WindowStyle.W10)
                        ApplyColorsToElements(My.Env.CP);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender, Window1 };

            var C = My.MyProject.Forms.ColorPickerDlg.Pick(CList);
            My.Env.CP.Windows10.Titlebar_Active = Color.FromArgb(255, C);
            if (My.Env.PreviewStyle == WindowStyle.W10)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W10_InactiveTitlebar_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.Windows10.Titlebar_Inactive = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    if (My.Env.PreviewStyle == WindowStyle.W10)
                        ApplyColorsToElements(My.Env.CP);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender, Window2 };

            var _Conditions = new Conditions() { Window_InactiveTitlebar = true };
            var C = My.MyProject.Forms.ColorPickerDlg.Pick(CList, _Conditions);

            My.Env.CP.Windows10.Titlebar_Inactive = Color.FromArgb(255, C);

            if (My.Env.PreviewStyle == WindowStyle.W10)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W10_WinMode_Toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (_Shown)
            {
                My.Env.CP.Windows10.WinMode_Light = !((UI.WP.Toggle)sender).Checked;
                if (My.Env.PreviewStyle == WindowStyle.W10)
                {
                    UpdateLegends();
                    ApplyColorsToElements(My.Env.CP);
                }
            }
        }

        private void W10_AppMode_Toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (_Shown)
            {
                My.Env.CP.Windows10.AppMode_Light = !((UI.WP.Toggle)sender).Checked;
                if (My.Env.PreviewStyle == WindowStyle.W10)
                    ApplyColorsToElements(My.Env.CP);
            }
        }

        private void W10_Transparency_Toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (_Shown)
            {
                My.Env.CP.Windows10.Transparency = ((UI.WP.Toggle)sender).Checked;
                if (My.Env.PreviewStyle == WindowStyle.W10)
                {
                    UpdateLegends();
                    ApplyColorsToElements(My.Env.CP);
                }
            }
        }

        private void W10_ShowAccentOnTitlebarAndBorders_Toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (_Shown)
            {
                My.Env.CP.Windows10.ApplyAccentOnTitlebars = ((UI.WP.Toggle)sender).Checked;
                if (My.Env.PreviewStyle == WindowStyle.W10)
                    ApplyColorsToElements(My.Env.CP);
            }
        }

        private void W10_Accent_None_CheckedChanged(object sender)
        {
            if (_Shown & ((UI.WP.RadioImage)sender).Checked)
            {
                My.Env.CP.Windows10.ApplyAccentOnTaskbar = Structures.Windows10x.AccentTaskbarLevels.None;
                if (My.Env.PreviewStyle == WindowStyle.W10)
                {
                    UpdateLegends();
                    ApplyColorsToElements(My.Env.CP);
                }
            }
        }

        private void W10_Accent_Taskbar_CheckedChanged(object sender)
        {
            if (_Shown & ((UI.WP.RadioImage)sender).Checked)
            {
                My.Env.CP.Windows10.ApplyAccentOnTaskbar = Structures.Windows10x.AccentTaskbarLevels.Taskbar;
                if (My.Env.PreviewStyle == WindowStyle.W10)
                {
                    UpdateLegends();
                    ApplyColorsToElements(My.Env.CP);
                }
            }
        }

        private void W10_Accent_StartTaskbar_CheckedChanged(object sender)
        {
            if (_Shown & ((UI.WP.RadioImage)sender).Checked)
            {
                My.Env.CP.Windows10.ApplyAccentOnTaskbar = Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC;
                if (My.Env.PreviewStyle == WindowStyle.W10)
                {
                    UpdateLegends();
                    ApplyColorsToElements(My.Env.CP);
                }
            }
        }

        private void W10_TBTransparency_Toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (_Shown)
            {
                My.Env.CP.Windows10.IncreaseTBTransparency = ((UI.WP.Toggle)sender).Checked;
                if (My.Env.PreviewStyle == WindowStyle.W10)
                    ApplyColorsToElements(My.Env.CP);
            }
        }

        private void W10_Color_Index1_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.Windows10.Color_Index1 = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    if (My.Env.PreviewStyle == WindowStyle.W10)
                        ApplyColorsToElements(My.Env.CP);
                }
                return;
            }

            var CList = new List<Control>();
            Color C;
            CList.Add((Control)sender);

            var _Conditions = new Conditions();

            switch (!My.Env.CP.Windows10.WinMode_Light)
            {
                case true:
                    {
                        CList.Add(taskbar);  // 'AppUnderline
                        _Conditions.AppUnderlineOnly = true;
                        break;
                    }
                case false:
                    {
                        if (My.Env.CP.Windows10.ApplyAccentOnTaskbar != Structures.Windows10x.AccentTaskbarLevels.None)
                        {
                            CList.Add(taskbar);  // 'AppUnderline
                            _Conditions.AppUnderlineOnly = true;
                        }

                        break;
                    }
            }

            C = My.MyProject.Forms.ColorPickerDlg.Pick(CList, _Conditions);

            My.Env.CP.Windows10.Color_Index1 = Color.FromArgb(255, C);
            if (My.Env.PreviewStyle == WindowStyle.W10)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W10_TaskbarFrontAndFoldersOnStart_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.Windows10.Color_Index5 = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    if (My.Env.PreviewStyle == WindowStyle.W10)
                        ApplyColorsToElements(My.Env.CP);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender };

            if (My.Env.CP.Windows10.Transparency)
            {
            }
            // ColorControls_List.Add(start) ''Hamburger
            else
            {
                CList.Add(taskbar);
            }

            var C = My.MyProject.Forms.ColorPickerDlg.Pick(CList);
            My.Env.CP.Windows10.Color_Index5 = Color.FromArgb(255, C);
            if (My.Env.PreviewStyle == WindowStyle.W10)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();

        }

        private void W10_Color_Index0_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.Windows10.Color_Index0 = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    if (My.Env.PreviewStyle == WindowStyle.W10)
                        ApplyColorsToElements(My.Env.CP);
                }
                return;
            }

            var CList = new List<Control>();
            Color C;

            CList.Add((Control)sender);


            var _Conditions = new Conditions();

            switch (!My.Env.CP.Windows10.WinMode_Light)
            {
                case true:
                    {
                        CList.Add(ActionCenter); // 'Link
                        _Conditions.ActionCenterLink = true;
                        break;
                    }

                case false:
                    {
                        if (My.Env.CP.Windows10.ApplyAccentOnTaskbar == Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC)
                        {
                            CList.Add(ActionCenter); // 'Link
                            _Conditions.ActionCenterLink = true;
                        }

                        break;
                    }
            }

            C = My.MyProject.Forms.ColorPickerDlg.Pick(CList, _Conditions);

            My.Env.CP.Windows10.Color_Index0 = Color.FromArgb(255, C);
            if (My.Env.PreviewStyle == WindowStyle.W10)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();

        }

        private void W10_Color_Index3_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.Windows10.Color_Index3 = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    if (My.Env.PreviewStyle == WindowStyle.W10)
                        ApplyColorsToElements(My.Env.CP);
                }
                return;
            }

            var CList = new List<Control>();
            Color C;

            CList.Add((Control)sender);

            var _Conditions = new Conditions();

            switch (!My.Env.CP.Windows10.WinMode_Light)
            {
                case true:
                    {
                        if (My.Env.CP.Windows10.Transparency)
                        {
                            CList.Add(setting_icon_preview);
                            CList.Add(ActionCenter);
                            _Conditions.ActionCenterBtn = true;
                            CList.Add(lnk_preview);
                            if (My.Env.CP.Windows10.ApplyAccentOnTaskbar != Structures.Windows10x.AccentTaskbarLevels.None)
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

                        if (My.Env.CP.Windows10.ApplyAccentOnTaskbar == Structures.Windows10x.AccentTaskbarLevels.None)
                        {
                            CList.Add(taskbar);  // 'AppBackground
                            _Conditions.AppBackgroundOnly = true;
                            _Conditions.AppUnderlineOnly = true;

                        }

                        break;
                    }
            }
            C = My.MyProject.Forms.ColorPickerDlg.Pick(CList, _Conditions);

            My.Env.CP.Windows10.Color_Index3 = Color.FromArgb(255, C);
            if (My.Env.PreviewStyle == WindowStyle.W10)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W10_Color_Index6_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.Windows10.Color_Index6 = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    if (My.Env.PreviewStyle == WindowStyle.W10)
                        ApplyColorsToElements(My.Env.CP);
                }
                return;
            }

            var CList = new List<Control>();
            Color C;

            CList.Add((Control)sender);

            var _Conditions = new Conditions();

            switch (!My.Env.CP.Windows10.WinMode_Light)
            {
                case true:
                    {

                        if (My.Env.CP.Windows10.Transparency)
                        {
                            CList.Add(taskbar);
                        }

                        break;
                    }

                case false:
                    {

                        if (My.Env.CP.Windows10.Transparency)
                        {
                            CList.Add(taskbar);

                            if (My.Env.CP.Windows10.ApplyAccentOnTaskbar != Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC)
                            {
                                CList.Add(ActionCenter); // 'ActionCenterLinks
                                _Conditions.ActionCenterLink = true;
                            }
                        }

                        else if (My.Env.CP.Windows10.ApplyAccentOnTaskbar != Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC)
                        {
                            CList.Add(ActionCenter); // 'ActionCenterLinks
                            _Conditions.ActionCenterLink = true;

                        }

                        break;
                    }
            }

            C = My.MyProject.Forms.ColorPickerDlg.Pick(CList, _Conditions);

            My.Env.CP.Windows10.Color_Index6 = Color.FromArgb(255, C);
            if (My.Env.PreviewStyle == WindowStyle.W10)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W10_Color_Index5_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.Windows10.StartMenu_Accent = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    if (My.Env.PreviewStyle == WindowStyle.W10)
                        ApplyColorsToElements(My.Env.CP);
                }
                return;
            }


            var CList = new List<Control>() { (Control)sender };


            var C = My.MyProject.Forms.ColorPickerDlg.Pick(CList);
            My.Env.CP.Windows10.StartMenu_Accent = Color.FromArgb(255, C);
            if (My.Env.PreviewStyle == WindowStyle.W10)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();

        }

        private void W10_Color_Index4_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.Windows10.Color_Index2 = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    if (My.Env.PreviewStyle == WindowStyle.W10)
                        ApplyColorsToElements(My.Env.CP);
                }
                return;
            }


            var CList = new List<Control>() { (Control)sender };

            if (My.Env.PreviewStyle == WindowStyle.W10)
            {
                // ColorControls_List.Add(taskbar) 'Start Icon Hover
            }

            var C = My.MyProject.Forms.ColorPickerDlg.Pick(CList);
            My.Env.CP.Windows10.Color_Index2 = Color.FromArgb(255, C);
            if (My.Env.PreviewStyle == WindowStyle.W10)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();

        }

        private void W10_Color_Index2_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.Windows10.Color_Index4 = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    if (My.Env.PreviewStyle == WindowStyle.W10)
                        ApplyColorsToElements(My.Env.CP);
                }
                return;
            }


            var CList = new List<Control>() { (Control)sender };

            Color C;


            var _Conditions = new Conditions();

            switch (!My.Env.CP.Windows10.WinMode_Light)
            {
                case true:
                    {

                        if (My.Env.CP.Windows10.Transparency)
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
                        if (My.Env.CP.Windows10.Transparency)
                        {
                            CList.Add(start);
                            CList.Add(ActionCenter);
                        }
                        else if (My.Env.CP.Windows10.ApplyAccentOnTaskbar == Structures.Windows10x.AccentTaskbarLevels.None)
                        {
                            CList.Add(start);
                            CList.Add(ActionCenter);
                        }
                        else if (My.Env.CP.Windows10.ApplyAccentOnTaskbar == Structures.Windows10x.AccentTaskbarLevels.Taskbar)
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

            C = My.MyProject.Forms.ColorPickerDlg.Pick(CList, _Conditions);

            My.Env.CP.Windows10.Color_Index4 = Color.FromArgb(255, C);
            if (My.Env.PreviewStyle == WindowStyle.W10)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W10_Color_Index7_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.Env.CP.Windows10.Color_Index7 = My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                return;
            }

            var CList = new List<Control>() { (Control)sender };
            var C = My.MyProject.Forms.ColorPickerDlg.Pick(CList);

            My.Env.CP.Windows10.Color_Index7 = Color.FromArgb(255, C);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W10_Button8_Click_1(object sender, EventArgs e)
        {
            WPStyle.MsgBox(My.Env.Lang.TitlebarColorNotice, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void W10_Button25_Click(object sender, EventArgs e)
        {
            WPStyle.MsgBox(My.Env.Lang.CP_AccentOnTaskbarTib, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void W10_TB_Blur_CheckedChanged(object sender, EventArgs e)
        {
            if (_Shown)
            {
                My.Env.CP.Windows10.TB_Blur = ((UI.WP.Toggle)sender).Checked;
                if (My.Env.PreviewStyle == WindowStyle.W10)
                    ApplyColorsToElements(My.Env.CP);
            }
        }

        private void Button37_Click(object sender, EventArgs e)
        {
            // Copycat from Windows 10 colors
            tabs_preview.Visible = false;
            My.Env.CP.Windows11 = (Structures.Windows10x)My.Env.CP.Windows10.Clone();
            ApplyCPValues(My.Env.CP);
            ApplyColorsToElements(My.Env.CP);
            tabs_preview.Visible = true;
        }

        #endregion

        #region Windows 8.1
        private void W81_pick_DragDrop(object sender, DragEventArgs e)
        {
            if (((UI.Controllers.ColorItem)sender).AllowDrop && My.Env.PreviewStyle == WindowStyle.W81)
            {
                My.Env.CP.Windows81.ColorizationColor = W81_ColorizationColor_pick.BackColor;
                My.Env.CP.Windows81.StartColor = W81_start_pick.BackColor;
                My.Env.CP.Windows81.AccentColor = W81_accent_pick.BackColor;
                My.Env.CP.Windows81.PersonalColors_Background = W81_personalcls_background_pick.BackColor;
                My.Env.CP.Windows81.PersonalColors_Accent = W81_personalcolor_accent_pick.BackColor;
                ApplyColorsToElements(My.Env.CP);
            }
        }

        private void W8_ColorizationColor_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.Windows81.ColorizationColor = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    if (My.Env.PreviewStyle == WindowStyle.W81)
                        ApplyColorsToElements(My.Env.CP);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender, start, taskbar, Window1, Window2 };

            var _Conditions = new Conditions() { Window_ActiveTitlebar = true, Window_InactiveTitlebar = true, Win7LivePreview_Colorization = true };

            var C = My.MyProject.Forms.ColorPickerDlg.Pick(CList, _Conditions);

            My.Env.CP.Windows81.ColorizationColor = Color.FromArgb(255, C);

            if (My.Env.PreviewStyle == WindowStyle.W81)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W8_ColorizationBalance_bar_Scroll(object sender)
        {
            if (_Shown)
            {
                W81_ColorizationBalance_val.Text = ((UI.WP.Trackbar)sender).Value.ToString();
                My.Env.CP.Windows81.ColorizationColorBalance = W81_ColorizationBalance_bar.Value;
                if (My.Env.PreviewStyle == WindowStyle.W81)
                    ApplyColorsToElements(My.Env.CP);
            }
        }

        private void W8_start_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.Windows81.StartColor = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    if (My.Env.PreviewStyle == WindowStyle.W81)
                        ApplyColorsToElements(My.Env.CP);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender };

            var C = My.MyProject.Forms.ColorPickerDlg.Pick(CList);

            My.Env.CP.Windows81.StartColor = Color.FromArgb(255, C);

            if (My.Env.PreviewStyle == WindowStyle.W81)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W8_accent_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.Windows81.AccentColor = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    if (My.Env.PreviewStyle == WindowStyle.W81)
                        ApplyColorsToElements(My.Env.CP);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender };

            var C = My.MyProject.Forms.ColorPickerDlg.Pick(CList);

            My.Env.CP.Windows81.AccentColor = Color.FromArgb(255, C);

            if (My.Env.PreviewStyle == WindowStyle.W81)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W8_personalcls_background_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.Windows81.PersonalColors_Background = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    if (My.Env.PreviewStyle == WindowStyle.W81)
                        ApplyColorsToElements(My.Env.CP);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender };

            var C = My.MyProject.Forms.ColorPickerDlg.Pick(CList);

            My.Env.CP.Windows81.PersonalColors_Background = Color.FromArgb(255, C);

            if (My.Env.PreviewStyle == WindowStyle.W81)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W8_personalcolor_accent_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.Windows81.PersonalColors_Accent = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    if (My.Env.PreviewStyle == WindowStyle.W81)
                        ApplyColorsToElements(My.Env.CP);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender };

            var C = My.MyProject.Forms.ColorPickerDlg.Pick(CList);

            My.Env.CP.Windows81.PersonalColors_Accent = Color.FromArgb(255, C);

            if (My.Env.PreviewStyle == WindowStyle.W81)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W8_ColorizationBalance_val_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), W81_ColorizationBalance_bar.Maximum), W81_ColorizationBalance_bar.Minimum).ToString();
            W81_ColorizationBalance_bar.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void W8_theme_aero_CheckedChanged(object sender)
        {
            if (W81_theme_aero.Checked)
            {
                My.Env.CP.Windows81.Theme = Structures.Windows7.Themes.Aero;
                if (My.Env.PreviewStyle == WindowStyle.W81)
                    ApplyColorsToElements(My.Env.CP);
            }
        }

        private void W8_theme_aerolite_CheckedChanged(object sender)
        {
            if (W81_theme_aerolite.Checked)
            {
                My.Env.CP.Windows81.Theme = Structures.Windows7.Themes.AeroLite;
                if (My.Env.PreviewStyle == WindowStyle.W81)
                    ApplyColorsToElements(My.Env.CP);
            }
        }

        private void W8_start_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.Start8Selector.ShowDialog();
            if (My.Env.PreviewStyle == WindowStyle.W81)
                ApplyColorsToElements(My.Env.CP);
        }

        private void W8_logonui_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.LogonUI8Colors.ShowDialog();
            if (My.Env.PreviewStyle == WindowStyle.W81)
                ApplyColorsToElements(My.Env.CP);
        }
        #endregion

        #region Windows 7
        private void W7_pick_DragDrop(object sender, DragEventArgs e)
        {
            if (((UI.Controllers.ColorItem)sender).AllowDrop && My.Env.PreviewStyle == WindowStyle.W7)
            {
                My.Env.CP.Windows7.ColorizationColor = W7_ColorizationColor_pick.BackColor;
                My.Env.CP.Windows7.ColorizationAfterglow = W7_ColorizationAfterglow_pick.BackColor;
                ApplyColorsToElements(My.Env.CP);
            }
        }

        private void W7_ColorizationColor_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.Windows7.ColorizationColor = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    if (My.Env.PreviewStyle == WindowStyle.W7)
                        ApplyColorsToElements(My.Env.CP);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender, start, taskbar, Window1, Window2 };

            var _Conditions = new Conditions() { Win7 = true, Color1 = true, BackColor1 = true, Win7LivePreview_Colorization = true };

            var C = My.MyProject.Forms.ColorPickerDlg.Pick(CList, _Conditions);

            My.Env.CP.Windows7.ColorizationColor = Color.FromArgb(255, C);

            if (My.Env.PreviewStyle == WindowStyle.W7)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W7_ColorizationAfterglow_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.Windows7.ColorizationAfterglow = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    ApplyColorsToElements(My.Env.CP);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender, start, taskbar, Window1, Window2 };

            var _Conditions = new Conditions() { Win7 = true, Color2 = true, BackColor2 = true, Win7LivePreview_AfterGlow = true };

            var C = My.MyProject.Forms.ColorPickerDlg.Pick(CList, _Conditions);

            My.Env.CP.Windows7.ColorizationAfterglow = Color.FromArgb(255, C);

            if (My.Env.PreviewStyle == WindowStyle.W7)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void W7_EnableAeroPeek_toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (_Shown)
                My.Env.CP.Windows7.EnableAeroPeek = W7_EnableAeroPeek_toggle.Checked;
        }

        private void W7_AlwaysHibernateThumbnails_Toggle_CheckedChanged(object sender, EventArgs e)
        {
            if (_Shown)
                My.Env.CP.Windows7.AlwaysHibernateThumbnails = W7_AlwaysHibernateThumbnails_Toggle.Checked;
        }

        private void W7_ColorizationColorBalance_bar_Scroll(object sender)
        {
            if (_Shown)
            {
                W7_ColorizationColorBalance_val.Text = ((UI.WP.Trackbar)sender).Value.ToString();
                My.Env.CP.Windows7.ColorizationColorBalance = W7_ColorizationColorBalance_bar.Value;
                if (My.Env.PreviewStyle == WindowStyle.W7)
                    ApplyColorsToElements(My.Env.CP);
            }
        }

        private void W7_ColorizationBlurBalance_bar_Scroll(object sender)
        {
            if (_Shown)
            {
                W7_ColorizationBlurBalance_val.Text = ((UI.WP.Trackbar)sender).Value.ToString();
                My.Env.CP.Windows7.ColorizationBlurBalance = W7_ColorizationBlurBalance_bar.Value;
                if (My.Env.PreviewStyle == WindowStyle.W7)
                    ApplyColorsToElements(My.Env.CP);
            }
        }

        private void W7_ColorizationGlassReflectionIntensity_bar_Scroll(object sender)
        {
            if (_Shown)
            {
                W7_ColorizationGlassReflectionIntensity_val.Text = ((UI.WP.Trackbar)sender).Value.ToString();
                My.Env.CP.Windows7.ColorizationGlassReflectionIntensity = W7_ColorizationGlassReflectionIntensity_bar.Value;
                if (My.Env.PreviewStyle == WindowStyle.W7)
                    ApplyColorsToElements(My.Env.CP);
            }
        }

        private void W7_theme_classic_CheckedChanged(object sender)
        {
            if (W7_theme_classic.Checked)
            {
                My.Env.CP.Windows7.Theme = Structures.Windows7.Themes.Classic;
                if (My.Env.PreviewStyle == WindowStyle.W7)
                {
                    ApplyColorsToElements(My.Env.CP);
                    ApplyStylesToElements(My.Env.CP, false);
                }
            }

        }

        private void W7_theme_basic_CheckedChanged(object sender)
        {
            if (W7_theme_basic.Checked)
            {
                My.Env.CP.Windows7.Theme = Structures.Windows7.Themes.Basic;
                if (My.Env.PreviewStyle == WindowStyle.W7)
                {
                    ApplyColorsToElements(My.Env.CP);
                    ApplyStylesToElements(My.Env.CP, false);
                }
            }
        }

        private void W7_theme_aeroopaque_CheckedChanged(object sender)
        {
            if (W7_theme_aeroopaque.Checked)
            {
                My.Env.CP.Windows7.Theme = Structures.Windows7.Themes.AeroOpaque;
                if (My.Env.PreviewStyle == WindowStyle.W7)
                {
                    ApplyColorsToElements(My.Env.CP);
                    ApplyStylesToElements(My.Env.CP, false);
                }
            }
        }

        private void W7_theme_Aero_CheckedChanged(object sender)
        {
            if (W7_theme_aero.Checked)
            {
                My.Env.CP.Windows7.Theme = Structures.Windows7.Themes.Aero;
                if (My.Env.PreviewStyle == WindowStyle.W7)
                {
                    ApplyColorsToElements(My.Env.CP);
                    ApplyStylesToElements(My.Env.CP, false);
                }
            }
        }

        private void W7_ColorizationAfterglowBalance_bar_Scroll(object sender)
        {
            if (_Shown)
            {
                W7_ColorizationAfterglowBalance_val.Text = ((UI.WP.Trackbar)sender).Value.ToString();
                My.Env.CP.Windows7.ColorizationAfterglowBalance = W7_ColorizationAfterglowBalance_bar.Value;
                if (My.Env.PreviewStyle == WindowStyle.W7)
                    ApplyColorsToElements(My.Env.CP);
            }
        }
        private void W7_ColorizationColorBalance_val_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), W7_ColorizationColorBalance_bar.Maximum), W7_ColorizationColorBalance_bar.Minimum).ToString();
            W7_ColorizationColorBalance_bar.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void W7_ColorizationAfterglowBalance_val_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), W7_ColorizationAfterglowBalance_bar.Maximum), W7_ColorizationAfterglowBalance_bar.Minimum).ToString();
            W7_ColorizationAfterglowBalance_bar.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void W7_ColorizationBlurBalance_val_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), W7_ColorizationBlurBalance_bar.Maximum), W7_ColorizationBlurBalance_bar.Minimum).ToString();
            W7_ColorizationBlurBalance_bar.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        private void W7_ColorizationGlassReflectionIntensity_val_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), W7_ColorizationGlassReflectionIntensity_bar.Maximum), W7_ColorizationGlassReflectionIntensity_bar.Minimum).ToString();
            W7_ColorizationGlassReflectionIntensity_bar.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        #endregion

        #region Windows Vista
        private void WVista_pick_DragDrop(object sender, DragEventArgs e)
        {
            if (((UI.Controllers.ColorItem)sender).AllowDrop && My.Env.PreviewStyle == WindowStyle.WVista)
            {
                My.Env.CP.WindowsVista.ColorizationColor = WVista_ColorizationColor_pick.BackColor;
                ApplyColorsToElements(My.Env.CP);
            }
        }

        private void WVista_ColorizationColor_pick_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                My.MyProject.Forms.SubMenu.ShowMenu((UI.Controllers.ColorItem)sender);
                if (My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Cut | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Paste | My.MyProject.Application.ColorEvent == My.MyApplication.MenuEvent.Override)
                {
                    My.Env.CP.WindowsVista.ColorizationColor = (Color)((UI.Controllers.ColorItem)sender).BackColor;
                    if (My.Env.PreviewStyle == WindowStyle.WVista)
                        ApplyColorsToElements(My.Env.CP);
                }
                return;
            }

            var CList = new List<Control>() { (Control)sender, start, taskbar, Window1, Window2 };

            var _Conditions = new Conditions() { Win7 = true, Color1 = true, BackColor1 = true, Win7LivePreview_Colorization = true };

            var C = My.MyProject.Forms.ColorPickerDlg.Pick(CList, _Conditions);

            My.Env.CP.WindowsVista.ColorizationColor = Color.FromArgb(255, C);

            if (My.Env.PreviewStyle == WindowStyle.WVista)
                ApplyColorsToElements(My.Env.CP);

            ((UI.Controllers.ColorItem)sender).BackColor = C;
            ((UI.Controllers.ColorItem)sender).Invalidate();

            CList.Clear();
        }

        private void WVista_ColorizationColorBalance_bar_Scroll(object sender)
        {
            if (_Shown)
            {
                WVista_ColorizationColorBalance_val.Text = ((UI.WP.Trackbar)sender).Value.ToString();
                My.Env.CP.WindowsVista.Alpha = (byte)WVista_ColorizationColorBalance_bar.Value;
                if (My.Env.PreviewStyle == WindowStyle.WVista)
                    ApplyColorsToElements(My.Env.CP);
            }
        }

        private void WVista_theme_classic_CheckedChanged(object sender)
        {
            if (WVista_theme_classic.Checked)
            {
                My.Env.CP.WindowsVista.Theme = Structures.Windows7.Themes.Classic;
                if (My.Env.PreviewStyle == WindowStyle.WVista)
                {
                    ApplyColorsToElements(My.Env.CP);
                    ApplyStylesToElements(My.Env.CP, false);
                }
            }

        }

        private void WVista_theme_basic_CheckedChanged(object sender)
        {
            if (WVista_theme_basic.Checked)
            {
                My.Env.CP.WindowsVista.Theme = Structures.Windows7.Themes.Basic;
                if (My.Env.PreviewStyle == WindowStyle.WVista)
                {
                    ApplyColorsToElements(My.Env.CP);
                    ApplyStylesToElements(My.Env.CP, false);
                }
            }
        }

        private void WVista_theme_aeroopaque_CheckedChanged(object sender)
        {
            if (WVista_theme_aeroopaque.Checked)
            {
                My.Env.CP.WindowsVista.Theme = Structures.Windows7.Themes.AeroOpaque;
                if (My.Env.PreviewStyle == WindowStyle.WVista)
                {
                    ApplyColorsToElements(My.Env.CP);
                    ApplyStylesToElements(My.Env.CP, false);
                }
            }
        }

        private void WVista_theme_Vista_CheckedChanged(object sender)
        {
            if (WVista_theme_aero.Checked)
            {
                My.Env.CP.WindowsVista.Theme = Structures.Windows7.Themes.Aero;
                if (My.Env.PreviewStyle == WindowStyle.WVista)
                {
                    ApplyColorsToElements(My.Env.CP);
                    ApplyStylesToElements(My.Env.CP, false);
                }
            }
        }

        private void WVista_ColorizationColorBalance_val_Click(object sender, EventArgs e)
        {
            string response = WPStyle.InputBox(My.Env.Lang.InputValue, ((UI.WP.Button)sender).Text, My.Env.Lang.ItMustBeNumerical);
            ((UI.WP.Button)sender).Text = Math.Max(Math.Min(Conversion.Val(response), WVista_ColorizationColorBalance_bar.Maximum), WVista_ColorizationColorBalance_bar.Minimum).ToString();
            WVista_ColorizationColorBalance_bar.Value = (int)Math.Round(Conversion.Val(((UI.WP.Button)sender).Text));
        }

        #endregion

        #region Windows XP
        private void WXP_Luna_Blue_CheckedChanged(object sender)
        {
            if (WXP_Luna_Blue.Checked)
            {
                My.Env.CP.WindowsXP.Theme = Structures.WindowsXP.Themes.LunaBlue;
                if (My.Env.PreviewStyle == WindowStyle.WXP)
                    ApplyStylesToElements(My.Env.CP, false);
            }
        }

        private void WXP_Luna_OliveGreen_CheckedChanged(object sender)
        {
            if (WXP_Luna_OliveGreen.Checked)
            {
                My.Env.CP.WindowsXP.Theme = Structures.WindowsXP.Themes.LunaOliveGreen;
                if (My.Env.PreviewStyle == WindowStyle.WXP)
                    ApplyStylesToElements(My.Env.CP, false);
            }
        }

        private void WXP_Luna_Silver_CheckedChanged(object sender)
        {
            if (WXP_Luna_Silver.Checked)
            {
                My.Env.CP.WindowsXP.Theme = Structures.WindowsXP.Themes.LunaSilver;
                if (My.Env.PreviewStyle == WindowStyle.WXP)
                    ApplyStylesToElements(My.Env.CP, false);
            }
        }

        private void WXP_CustomTheme_CheckedChanged(object sender)
        {
            if (WXP_CustomTheme.Checked)
            {
                My.Env.CP.WindowsXP.Theme = Structures.WindowsXP.Themes.Custom;
                if (My.Env.PreviewStyle == WindowStyle.WXP)
                    ApplyStylesToElements(My.Env.CP, false);
            }
        }

        private void WXP_Classic_CheckedChanged(object sender)
        {
            if (WXP_Classic.Checked)
            {
                My.Env.CP.WindowsXP.Theme = Structures.WindowsXP.Themes.Classic;
                if (My.Env.PreviewStyle == WindowStyle.WXP)
                    ApplyStylesToElements(My.Env.CP, false);
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
                theme = My.Env.PATH_appData + @"\VisualStyles\Luna\custom.theme";
                System.IO.File.WriteAllText(My.Env.PATH_appData + @"\VisualStyles\Luna\custom.theme", string.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=NormalColor{1}Size=NormalSize", WXP_VS_textbox.Text, "\r\n"));

            }

            My.Env.CP.WindowsXP.ThemeFile = WXP_VS_textbox.Text;

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

                if (WXP_CustomTheme.Checked & My.Env.PreviewStyle == WindowStyle.WXP)
                    ApplyStylesToElements(My.Env.CP, false);
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
                My.Env.CP.WindowsXP.ColorScheme = WXP_VS_ColorsList.SelectedItem.ToString();
                if (My.Env.PreviewStyle == WindowStyle.WXP)
                    ApplyStylesToElements(My.Env.CP, false);
            }
        }
        #endregion

        private void Button4_Click(object sender, EventArgs e)
        {
            Apply_Theme();
        }

        public void Apply_Theme(CP CP = null)
        {
            if (CP is null)
                CP = My.Env.CP;

            Cursor = Cursors.WaitCursor;
            OldWidth = TablessControl1.Width;

            log_lbl.Visible = false;
            log_lbl.Text = "";
            Button8.Visible = false;
            Button14.Visible = false;
            Button22.Visible = false;
            Button25.Visible = false;

            if (My.Env.Settings.ThemeLog.Enabled())
            {
                TablessControl1.SelectedIndex = TablessControl1.TabCount - 1;
                TablessControl1.Refresh();
                if (My.Env.Settings.ThemeLog.VerboseLevel == WPSettings.Structures.ThemeLog.VerboseLevels.Detailed)
                {
                    previewContainer.Visible = false;
                    TablessControl1.Width = MainToolbar.Width;
                }
            }

            CP.Save(CP_Type.Registry, "", My.Env.Settings.ThemeLog.Enabled() ? TreeView1 : null);

            if (My.Env.PreviewStyle == WindowStyle.WXP)
                Update_Wallpaper_Preview();

            My.Env.CP_Original = new CP(CP_Type.Registry);

            Cursor = Cursors.Default;

            if (My.Env.Settings.ThemeApplyingBehavior.AutoRestartExplorer)
            {
                RestartExplorer(My.Env.Settings.ThemeLog.Enabled() ? TreeView1 : null);
            }
            else if (My.Env.Settings.ThemeLog.Enabled())
                AddNode(TreeView1, My.Env.Lang.NoDefResExplorer, "warning");

            if (My.Env.Settings.ThemeLog.Enabled())
                AddNode(TreeView1, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), My.Env.Lang.CP_AllDone), "info");

            if (CP.MetricsFonts.Enabled & GetWindowsScreenScalingFactor() > 100d)
                AddNode(TreeView1, string.Format("{0}", My.Env.Lang.CP_MetricsHighDPIAlert), "info");

            log_lbl.Visible = true;
            Button8.Visible = true;
            Button22.Visible = true;
            Button25.Visible = true;

            if (!(My.Env.Saving_Exceptions.Count == 0))
            {
                log_lbl.Text = My.Env.Lang.CP_ErrorHappened;
                Button14.Visible = true;
            }
            else if (My.Env.Settings.ThemeLog.CountDown && !(My.Env.Settings.ThemeLog.VerboseLevel == WPSettings.Structures.ThemeLog.VerboseLevels.Detailed))
            {
                log_lbl.Text = string.Format(My.Env.Lang.CP_LogWillClose, My.Env.Settings.ThemeLog.CountDown_Seconds);
                elapsedSecs = 1;
                Timer1.Enabled = true;
                Timer1.Start();
            }

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            log_lbl.Text = string.Format(My.Env.Lang.CP_LogWillClose, My.Env.Settings.ThemeLog.CountDown_Seconds - elapsedSecs);

            if (elapsedSecs + 1 <= My.Env.Settings.ThemeLog.CountDown_Seconds)
            {
                elapsedSecs += 1;
            }
            else
            {
                log_lbl.Text = "";
                Timer1.Enabled = false;
                Timer1.Stop();
                if (!previewContainer.Visible)
                    previewContainer.Visible = true;
                TablessControl1.Width = OldWidth;
                SelectLeftPanelIndex();
            }

        }

        private void Button14_Click(object sender, EventArgs e)
        {
            log_lbl.Text = "";
            Timer1.Enabled = false;
            Timer1.Stop();
            My.MyProject.Forms.Saving_ex_list.ex_List = My.Env.Saving_Exceptions;
            My.MyProject.Forms.Saving_ex_list.ShowDialog();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            log_lbl.Text = "";
            Timer1.Enabled = false;
            Timer1.Stop();
            if (!previewContainer.Visible)
                previewContainer.Visible = true;
            TablessControl1.Width = OldWidth;
            SelectLeftPanelIndex();
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            log_lbl.Text = "";
            Timer1.Enabled = false;
            Timer1.Stop();

            if (SaveFileDialog3.ShowDialog() == DialogResult.OK)
            {
                var sb = new StringBuilder();
                sb.Clear();

                foreach (TreeNode N in TreeView1.Nodes)
                    sb.AppendLine(string.Format("[{0}]{2} {1}{3}", N.ImageKey, N.Text, Constants.vbTab, "\r\n"));

                System.IO.File.WriteAllText(SaveFileDialog3.FileName, sb.ToString());

            }
        }

        private void Apply_btn_MouseEnter(object sender, EventArgs e)
        {
            if (My.Env.Settings.ThemeApplyingBehavior.AutoRestartExplorer)
            {
                status_lbl.Text = My.Env.Lang.ThisWillRestartExplorer;
                status_lbl.ForeColor = My.Env.Style.DarkMode ? Color.Gold : Color.Gold.Dark(0.1f);
            }
        }

        private void Apply_btn_MouseLeave(object sender, EventArgs e)
        {
            if (My.Env.Settings.ThemeApplyingBehavior.AutoRestartExplorer)
            {
                status_lbl.Text = "";
                status_lbl.ForeColor = My.Env.Style.DarkMode ? Color.White : Color.Black;
            }
        }

        private void Button19_MouseEnter(object sender, EventArgs e)
        {
            status_lbl.Text = My.Env.Lang.ThisWillRestartExplorer;
            status_lbl.ForeColor = My.Env.Style.DarkMode ? Color.Gold : Color.Gold.Dark(0.1f);
        }

        private void Button19_MouseLeave(object sender, EventArgs e)
        {
            status_lbl.Text = "";
            status_lbl.ForeColor = My.Env.Style.DarkMode ? Color.White : Color.Black;
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(SaveFileDialog1.FileName))
            {
                if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    My.Env.CP.Save(CP_Type.File, SaveFileDialog1.FileNames[0]);
                }
            }
            else
            {
                My.Env.CP.Save(CP_Type.File, SaveFileDialog1.FileNames[0]);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {

                My.MyProject.Forms.ComplexSave.GetResponse(SaveFileDialog1, () => Apply_Theme(), () => Apply_Theme(My.Env.CP_FirstTime), () => Apply_Theme(CP_Defaults.GetDefault()));

                SaveFileDialog1.FileName = OpenFileDialog1.FileName;
                My.Env.CP = new CP(CP_Type.File, OpenFileDialog1.FileName);
                My.Env.CP_Original = (CP)My.Env.CP.Clone();

                ApplyStylesToElements(My.Env.CP, false);
                ApplyCPValues(My.Env.CP);
                ApplyColorsToElements(My.Env.CP);
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                My.Env.CP.Save(CP_Type.File, SaveFileDialog1.FileNames[0]);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {

            My.MyProject.Forms.ComplexSave.GetResponse(SaveFileDialog1, () => Apply_Theme(), () => Apply_Theme(My.Env.CP_FirstTime), () => Apply_Theme(CP_Defaults.GetDefault()));

            My.Env.CP = new CP(CP_Type.Registry);
            My.Env.CP_Original = (CP)My.Env.CP.Clone();
            SaveFileDialog1.FileName = null;
            ApplyStylesToElements(My.Env.CP, false);
            ApplyCPValues(My.Env.CP);
            ApplyColorsToElements(My.Env.CP);
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.EditInfo.Show();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.About.ShowDialog();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.Updates.ShowDialog();
            Button5.Image = My.Resources.Update;
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.SettingsX.ShowDialog();
        }

        private void Button4_Click_1(object sender, EventArgs e)
        {
            My.MyProject.Forms.Win32UI.Show();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.Whatsnew.ShowDialog();
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            if (My.Env.PreviewStyle == WindowStyle.W11 | My.Env.PreviewStyle == WindowStyle.W10)
            {
                My.MyProject.Forms.LogonUI.ShowDialog();
            }
            else if (My.Env.PreviewStyle == WindowStyle.W81 | My.Env.PreviewStyle == WindowStyle.W7)
            {
                My.MyProject.Forms.LogonUI7.Show();
            }
            else if (My.Env.PreviewStyle == WindowStyle.WXP)
            {
                My.MyProject.Forms.LogonUIXP.Show();
            }
            else if (My.Env.PreviewStyle == WindowStyle.WVista)
            {
                WPStyle.MsgBox(My.Env.Lang.VistaLogonNotSupported, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                My.MyProject.Forms.LogonUI.ShowDialog();
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
            My.Env.CP = (CP)My.Env.CP_Original.Clone();
            ApplyStylesToElements(My.Env.CP, false);
            ApplyCPValues(My.Env.CP);
            ApplyColorsToElements(My.Env.CP);
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            My.Env.CP = (CP)My.Env.CP_FirstTime.Clone();
            ApplyStylesToElements(My.Env.CP, false);
            ApplyCPValues(My.Env.CP);
            ApplyColorsToElements(My.Env.CP);
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            RestartExplorer();
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.ComplexSave.GetResponse(SaveFileDialog1, () => Apply_Theme(), () => Apply_Theme(My.Env.CP_FirstTime), () => Apply_Theme(CP_Defaults.GetDefault()));

            My.Env.CP = (CP)CP_Defaults.GetDefault().Clone();
            SaveFileDialog1.FileName = null;
            ApplyCPValues(My.Env.CP);
            ApplyStylesToElements(My.Env.CP, false);
            ApplyColorsToElements(My.Env.CP);
        }

        private void Button21_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.CursorsStudio.Show();
        }

        private void Button23_Click(object sender, EventArgs e)
        {
            if ((Button23.Text.ToLower() ?? "") == (My.Env.Lang.Hide.ToLower() ?? ""))
            {
                tabs_preview.Visible = false;
                Button23.Text = My.Env.Lang.Show;
            }
            else
            {
                tabs_preview.Visible = true;
                Button23.Text = My.Env.Lang.Hide;
            }
        }

        private void Button24_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.TerminalsDashboard.ShowDialog();
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
            My.MyProject.Forms.Metrics_Fonts.ShowDialog();
        }

        public void Select_Preview_Version()
        {

            My.Env.Animator.HideSync(TablessControl1, true);
            My.Env.Animator.HideSync(tabs_preview, true);

            UpdateLegends();

            ApplyColorsToElements(My.Env.CP);
            ApplyStylesToElements(My.Env.CP);
            ApplyDefaultCPValues();
            SelectLeftPanelIndex();

            if (My.Env.PreviewStyle == WindowStyle.W11)
            {
                Button20.Image = My.Resources.add_win11;
            }
            else if (My.Env.PreviewStyle == WindowStyle.W10)
            {
                Button20.Image = My.Resources.add_win10;
            }
            else if (My.Env.PreviewStyle == WindowStyle.W81)
            {
                Button20.Image = My.Resources.add_win8;
            }
            else if (My.Env.PreviewStyle == WindowStyle.W7)
            {
                Button20.Image = My.Resources.add_win7;
            }
            else if (My.Env.PreviewStyle == WindowStyle.WVista)
            {
                Button20.Image = My.Resources.add_winvista;
            }
            else if (My.Env.PreviewStyle == WindowStyle.WXP)
            {
                Button20.Image = My.Resources.add_winxp;
            }
            else
            {
                Button20.Image = My.Resources.add_win11;
            }


            My.Env.Animator.ShowSync(TablessControl1, true);
            My.Env.Animator.ShowSync(tabs_preview, true);

        }

        private void Select_W11_CheckedChanged(object sender)
        {
            if (_Shown & Select_W11.Checked)
            {
                My.Env.PreviewStyle = WindowStyle.W11;
                Select_Preview_Version();
            }
        }

        private void Select_W10_CheckedChanged(object sender)
        {
            if (_Shown & Select_W10.Checked)
            {
                My.Env.PreviewStyle = WindowStyle.W10;
                Select_Preview_Version();
            }
        }

        private void Select_W8_CheckedChanged(object sender)
        {
            if (_Shown & Select_W81.Checked)
            {
                My.Env.PreviewStyle = WindowStyle.W81;
                Select_Preview_Version();
            }
        }

        private void Select_W7_CheckedChanged(object sender)
        {
            if (_Shown & Select_W7.Checked)
            {
                My.Env.PreviewStyle = WindowStyle.W7;
                Select_Preview_Version();
            }
        }

        private void Select_WVista_CheckedChanged(object sender)
        {
            if (_Shown & Select_WVista.Checked)
            {
                My.Env.PreviewStyle = WindowStyle.WVista;
                Select_Preview_Version();
            }
        }

        private void Select_WXP_CheckedChanged(object sender)
        {
            if (_Shown & Select_WXP.Checked)
            {
                My.Env.PreviewStyle = WindowStyle.WXP;
                Select_Preview_Version();
            }
        }

        private void Button29_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.WinEffecter.ShowDialog();
        }

        private void Button32_Click(object sender, EventArgs e)
        {
            if (My.Env.PreviewStyle != WindowStyle.WXP && My.Env.PreviewStyle != WindowStyle.WVista)
            {
                My.MyProject.Forms.AltTabEditor.ShowDialog();
            }
            else
            {
                if (My.Env.PreviewStyle == WindowStyle.WXP)
                    WPStyle.MsgBox(string.Format(My.Env.Lang.AltTab_Unsupported, My.Env.Lang.OS_WinXP), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (My.Env.PreviewStyle == WindowStyle.WVista)
                    WPStyle.MsgBox(string.Format(My.Env.Lang.AltTab_Unsupported, My.Env.Lang.OS_WinVista), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void Button25_Click(object sender, EventArgs e)
        {
            log_lbl.Text = "";
            Timer1.Enabled = false;
            Timer1.Stop();
        }

        private void Button33_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.ScreenSaver_Editor.ShowDialog();
        }

        private void Button34_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.Sounds_Editor.ShowDialog();
        }

        private void Button35_Click(object sender, EventArgs e)
        {
            if (My.Env.PreviewStyle == WindowStyle.W11)
            {
                My.MyProject.Forms.Wallpaper_Editor.WT = My.Env.CP.WallpaperTone_W11;
            }
            else if (My.Env.PreviewStyle == WindowStyle.W10)
            {
                My.MyProject.Forms.Wallpaper_Editor.WT = My.Env.CP.WallpaperTone_W10;
            }
            else if (My.Env.PreviewStyle == WindowStyle.W81)
            {
                My.MyProject.Forms.Wallpaper_Editor.WT = My.Env.CP.WallpaperTone_W81;
            }
            else if (My.Env.PreviewStyle == WindowStyle.W7)
            {
                My.MyProject.Forms.Wallpaper_Editor.WT = My.Env.CP.WallpaperTone_W7;
            }
            else if (My.Env.PreviewStyle == WindowStyle.WVista)
            {
                My.MyProject.Forms.Wallpaper_Editor.WT = My.Env.CP.WallpaperTone_WVista;
            }
            else if (My.Env.PreviewStyle == WindowStyle.WXP)
            {
                My.MyProject.Forms.Wallpaper_Editor.WT = My.Env.CP.WallpaperTone_WXP;
            }
            else
            {
                My.MyProject.Forms.Wallpaper_Editor.WT = My.Env.CP.WallpaperTone_W11;
            }

            My.MyProject.Forms.Wallpaper_Editor.Show();
        }

        private void Button26_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.ApplicationThemer.FixLanguageDarkModeBug = false;
            My.MyProject.Forms.ApplicationThemer.Show();
        }

        private void Button36_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.Converter_Form.ShowDialog();
        }

        private void Button28_Click(object sender, EventArgs e)
        {

            if (MsgBox(My.Env.Lang.LogoffQuestion, MessageBoxButtons.YesNo, MessageBoxIcon.Question, My.Env.Lang.LogoffAlert1, "", "", "", "", My.Env.Lang.LogoffAlert2, Ookii.Dialogs.WinForms.TaskDialogIcon.Information) == DialogResult.Yes)
            {
                LoggingOff = true;
                IntPtr intPtr = IntPtr.Zero;
                Kernel32.Wow64DisableWow64FsRedirection(ref intPtr);
                if (System.IO.File.Exists(My.Env.PATH_System32 + @"\logoff.exe"))
                {
                    Interaction.Shell(My.Env.PATH_System32 + @"\logoff.exe", AppWinStyle.Hide);
                }
                else
                {
                    WPStyle.MsgBox(string.Format(My.Env.Lang.LogoffNotFound, My.Env.PATH_System32), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

        }

        private void Button28_MouseEnter(object sender, EventArgs e)
        {
            status_lbl.Text = My.Env.Lang.LogoffNotice;
            status_lbl.ForeColor = My.Env.Style.DarkMode ? Color.Gold : Color.Gold.Dark(0.1f);
        }

        private void Button28_MouseLeave(object sender, EventArgs e)
        {
            status_lbl.Text = "";
            status_lbl.ForeColor = My.Env.Style.DarkMode ? Color.White : Color.Black;
        }

        private void Button39_Click(object sender, EventArgs e)
        {
            Process.Start(My.Resources.Link_Wiki);
        }

        private void Button40_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.PaletteGenerateDashboard.ShowDialog();
        }

        private void Button30_Click_1(object sender, EventArgs e)
        {
            WPStyle.MsgBox(My.Env.Lang.Win11ColorsDescTip, MessageBoxButtons.OK, MessageBoxIcon.Information, My.Env.Lang.Win11ColorsDescTip2);
        }

        private void Button31_Click(object sender, EventArgs e)
        {
            if (My.Env.WXP)
            {
                if (WPStyle.MsgBox(string.Format(My.Env.Lang.Store_WontWork_Protocol, My.Env.Lang.OS_WinXP), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
            }
            else if (My.Env.WVista)
            {
                if (WPStyle.MsgBox(string.Format(My.Env.Lang.Store_WontWork_Protocol, My.Env.Lang.OS_WinVista), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
            }

            My.MyProject.Forms.Store.Show();
        }
    }
}