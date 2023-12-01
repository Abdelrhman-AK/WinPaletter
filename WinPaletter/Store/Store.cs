using Devcorp.Controls.VisualStyles;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.Theme;
using WinPaletter.UI.Controllers;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter
{
    public partial class Store
    {

        #region Variables
        private bool StartedAsOnlineOrOffline = true;
        private bool FinishedLoadingInitialTMs;
        private Dictionary<string, Theme.Manager> TMList = new();

        private readonly int w = (int)Math.Round(528d * 0.6d);
        private readonly int h = (int)Math.Round(297d * 0.6d);

        private UI.Controllers.StoreItem hoveredItem;
        public UI.Controllers.StoreItem selectedItem;

        private bool _Shown = false;
        private readonly List<UI.Controllers.CursorControl> AnimateList = new();
        private float Angle = 180f;
        private readonly float Increment = 5f;
        private int Cycles = 0;
        private WebClient WebCL;

        private readonly Converter _Converter = new();
        private bool ApplyOrEditToggle = true;
        private Settings.Structures.Appearance oldAppearance = Program.Settings.Appearance;

        public Store()
        {
            WebCL = new();
            InitializeComponent();
        }
        #endregion

        #region Preview Voids

        public void Adjust_Preview(Theme.Manager TM)
        {

            Program.Wallpaper = Program.FetchSuitableWallpaper(TM, Program.PreviewStyle);
            pnl_preview.BackgroundImage = Program.Wallpaper;
            pnl_preview_classic.BackgroundImage = Program.Wallpaper;

            ApplyWinElementsColors(TM, Program.PreviewStyle, false, taskbar, start, ActionCenter, setting_icon_preview, Label8, lnk_preview);
            ApplyWindowStyles(TM, Program.PreviewStyle, Window1, Window2);
            ApplyWinElementsStyle(TM, Program.PreviewStyle, taskbar, start, ActionCenter, Window1, Window2, Panel3, lnk_preview, ClassicTaskbar, ButtonR2, ButtonR3, ButtonR4, ClassicWindow1, ClassicWindow2, Forms.MainFrm.WXP_VS_ReplaceColors.Checked, Forms.MainFrm.WXP_VS_ReplaceMetrics.Checked, Forms.MainFrm.WXP_VS_ReplaceFonts.Checked);

            AdjustPreview_ModernOrClassic(TM, Program.PreviewStyle, tabs_preview, WXP_Alert2);
        }

        private void Menu_Window_SizeChanged(object sender, EventArgs e)
        {
            RetroShadow1.Size = Menu_Window.Size;
            RetroShadow1.Location = Menu_Window.Location + (Size)new Point(6, 5);

            Bitmap b = new(RetroShadow1.Width, RetroShadow1.Height);
            Graphics G = Graphics.FromImage(b);
            G.DrawGlow(new Rectangle(5, 5, b.Width - 10 - 1, b.Height - 10 - 1), Color.FromArgb(128, 0, 0, 0));
            G.Save();
            RetroShadow1.Image = b;
            G.Dispose();

            RetroShadow1.BringToFront();
            Menu_Window.BringToFront();
        }


        public void SetClassicMetrics(Theme.Manager TM)
        {
            try
            {
                if ((Program.PreviewStyle == WindowStyle.WXP && Forms.MainFrm.WXP_VS_ReplaceMetrics.Checked) & TM.WindowsXP.Theme != Theme.Structures.WindowsXP.Themes.Classic)
                {
                    if (System.IO.File.Exists(PathsExt.MSTheme) & !string.IsNullOrEmpty(PathsExt.MSTheme))
                    {
                        using (VisualStyleFile vs = new(PathsExt.MSTheme))
                        {
                            TM.MetricsFonts.Overwrite_Metrics(vs.Metrics);
                        }
                    }

                    if (System.IO.File.Exists(PathsExt.MSTheme) & !string.IsNullOrEmpty(PathsExt.MSTheme))
                    {
                        using (VisualStyleFile vs = new(PathsExt.MSTheme))
                        {
                            TM.MetricsFonts.Overwrite_Fonts(vs.Metrics);
                        }
                    }
                }
            }
            catch
            {
            }

            PanelR2.Width = TM.MetricsFonts.ScrollWidth;
            menucontainer0.Height = TM.MetricsFonts.MenuHeight;

            menucontainer0.Height = Math.Max(TM.MetricsFonts.MenuHeight, Forms.Metrics_Fonts.GetTitleTextHeight(TM.MetricsFonts.MenuFont));

            LabelR1.Font = TM.MetricsFonts.MenuFont;
            LabelR2.Font = TM.MetricsFonts.MenuFont;
            LabelR3.Font = TM.MetricsFonts.MenuFont;

            LabelR9.Font = TM.MetricsFonts.MenuFont;
            LabelR5.Font = TM.MetricsFonts.MenuFont;
            LabelR6.Font = TM.MetricsFonts.MenuFont;

            menucontainer1.Height = Forms.Metrics_Fonts.GetTitleTextHeight(TM.MetricsFonts.MenuFont) + 3;
            highlight.Height = menucontainer1.Height + 1;
            menucontainer3.Height = menucontainer1.Height + 1;
            Menu_Window.Height = menucontainer1.Height + highlight.Height + menucontainer3.Height + Menu_Window.Padding.Top + Menu_Window.Padding.Bottom;

            LabelR4.Font = TM.MetricsFonts.MessageFont;

            LabelR1.Width = (int)Math.Round(LabelR1.Text.Measure(TM.MetricsFonts.MenuFont).Width + 5f);
            LabelR2.Width = (int)Math.Round(LabelR2.Text.Measure(TM.MetricsFonts.MenuFont).Width + 5f);
            PanelR1.Width = (int)Math.Round(LabelR3.Text.Measure(TM.MetricsFonts.MenuFont).Width + 5f + PanelR1.Padding.Left + PanelR1.Padding.Right);

            int iP = 3 + TM.MetricsFonts.PaddedBorderWidth + TM.MetricsFonts.BorderWidth;
            int iT = 4 + TM.MetricsFonts.PaddedBorderWidth + TM.MetricsFonts.BorderWidth + TM.MetricsFonts.CaptionHeight + GetTitlebarTextHeight(TM.MetricsFonts.CaptionFont);
            System.Windows.Forms.Padding _Padding = new(iP, iT, iP, iP);

            foreach (UI.Retro.WindowR WindowR in ClassicColorsPreview.GetAllControls().OfType<UI.Retro.WindowR>())
            {
                if (!WindowR.UseItAsMenu)
                {
                    SetClassicWindowMetrics(TM, WindowR);
                    WindowR.Padding = _Padding;
                }
            }



            WindowR3.Height = 85 + TM.MetricsFonts.PaddedBorderWidth + TM.MetricsFonts.BorderWidth + GetTitlebarTextHeight(WindowR3.Font);
            WindowR2.Height = 120 + TM.MetricsFonts.PaddedBorderWidth + TM.MetricsFonts.BorderWidth + GetTitlebarTextHeight(WindowR2.Font) + TM.MetricsFonts.MenuHeight;

            Menu_Window.Top = WindowR2.Top + menucontainer0.Top + menucontainer0.Height;
            Menu_Window.Left = Math.Min(WindowR2.Left + menucontainer0.Left + PanelR1.Left + (+3), WindowR2.Right - TM.MetricsFonts.PaddedBorderWidth - TM.MetricsFonts.BorderWidth);

            WindowR3.Top = WindowR2.Top + TextBoxR1.Top + TextBoxR1.Font.Height + 10;
            WindowR3.Left = WindowR2.Left + TextBoxR1.Left + 15;

            LabelR13.Top = WindowR4.Top + WindowR4.Metrics_CaptionHeight + 2;
            LabelR13.Left = WindowR4.Right - WindowR4.Metrics_CaptionWidth - 2;

            RetroShadow1.Visible = TM.WindowsEffects.WindowShadow;

            ButtonR5.FocusRectWidth = (int)TM.WindowsEffects.FocusRectWidth;
            ButtonR5.FocusRectHeight = (int)TM.WindowsEffects.FocusRectHeight;
            ButtonR5.Refresh();
        }

        public void ApplyRetroPreview(Theme.Manager TM)
        {
            try
            {
                if ((Program.PreviewStyle == WindowStyle.WXP && Forms.MainFrm.WXP_VS_ReplaceColors.Checked) & TM.WindowsXP.Theme != Theme.Structures.WindowsXP.Themes.Classic)
                {
                    if (System.IO.File.Exists(PathsExt.MSTheme) & !string.IsNullOrEmpty(PathsExt.MSTheme))
                    {
                        using (VisualStyleFile vs = new(PathsExt.MSTheme))
                        {
                            TM.Win32.Load(Theme.Structures.Win32UI.Sources.VisualStyles, vs.Metrics);
                        }
                    }
                }
            }
            catch
            {
            }

            WindowR1.ColorGradient = TM.Win32.EnableGradient;
            WindowR1.Color1 = TM.Win32.InactiveTitle;
            WindowR1.Color2 = TM.Win32.GradientInactiveTitle;
            WindowR1.ForeColor = TM.Win32.InactiveTitleText;
            WindowR1.ColorBorder = TM.Win32.InactiveBorder;

            WindowR2.ColorGradient = TM.Win32.EnableGradient;
            WindowR3.ColorGradient = TM.Win32.EnableGradient;
            WindowR4.ColorGradient = TM.Win32.EnableGradient;

            WindowR2.Color1 = TM.Win32.ActiveTitle;
            WindowR3.Color1 = TM.Win32.ActiveTitle;
            WindowR4.Color1 = TM.Win32.ActiveTitle;

            WindowR2.Color2 = TM.Win32.GradientActiveTitle;
            WindowR3.Color2 = TM.Win32.GradientActiveTitle;
            WindowR4.Color2 = TM.Win32.GradientActiveTitle;

            WindowR2.ForeColor = TM.Win32.TitleText;
            WindowR3.ForeColor = TM.Win32.TitleText;
            WindowR4.ForeColor = TM.Win32.TitleText;

            WindowR2.ColorBorder = TM.Win32.ActiveBorder;
            WindowR3.ColorBorder = TM.Win32.ActiveBorder;
            WindowR4.ColorBorder = TM.Win32.ActiveBorder;

            foreach (UI.Retro.WindowR WindowR in ClassicColorsPreview.GetAllControls().OfType<UI.Retro.WindowR>())
            {
                if (!WindowR.UseItAsMenu)
                    WindowR.BackColor = TM.Win32.ButtonFace;

                WindowR.ButtonDkShadow = TM.Win32.ButtonDkShadow;
                WindowR.ButtonHilight = TM.Win32.ButtonHilight;
                WindowR.ButtonHilight = TM.Win32.ButtonHilight;
                WindowR.ButtonLight = TM.Win32.ButtonLight;
                WindowR.ButtonShadow = TM.Win32.ButtonShadow;
                WindowR.ButtonText = TM.Win32.ButtonText;
            }

            foreach (UI.Retro.ButtonR ButtonR in ClassicColorsPreview.GetAllControls().OfType<UI.Retro.ButtonR>())
            {
                ButtonR.WindowFrame = TM.Win32.WindowFrame;
                ButtonR.BackColor = TM.Win32.ButtonFace;
                ButtonR.ButtonDkShadow = TM.Win32.ButtonDkShadow;
                ButtonR.ButtonHilight = TM.Win32.ButtonHilight;
                ButtonR.ButtonLight = TM.Win32.ButtonLight;
                ButtonR.ButtonShadow = TM.Win32.ButtonShadow;
                ButtonR.ForeColor = TM.Win32.ButtonText;
            }

            foreach (UI.Retro.PanelRaisedR PanelRaisedR in ClassicColorsPreview.GetAllControls().OfType<UI.Retro.PanelRaisedR>())
            {
                PanelRaisedR.ButtonHilight = TM.Win32.ButtonHilight;
                PanelRaisedR.ButtonShadow = TM.Win32.ButtonShadow;
            }

            TextBoxR1.BackColor = TM.Win32.Window;
            TextBoxR1.ForeColor = TM.Win32.WindowText;
            TextBoxR1.ButtonDkShadow = TM.Win32.ButtonDkShadow;
            TextBoxR1.ButtonHilight = TM.Win32.ButtonHilight;
            TextBoxR1.ButtonLight = TM.Win32.ButtonLight;
            TextBoxR1.ButtonShadow = TM.Win32.ButtonShadow;
            TextBoxR1.Refresh();

            PanelR2.BackColor = TM.Win32.ButtonFace;
            PanelR2.ButtonHilight = TM.Win32.ButtonHilight;

            Menu_Window.ButtonFace = TM.Win32.ButtonFace;
            Menu_Window.ButtonDkShadow = TM.Win32.ButtonDkShadow;
            Menu_Window.ButtonHilight = TM.Win32.ButtonHilight;
            Menu_Window.ButtonLight = TM.Win32.ButtonLight;
            Menu_Window.ButtonShadow = TM.Win32.ButtonShadow;
            Menu_Window.BackColor = TM.Win32.Menu;
            Menu_Window.Refresh();

            PanelR1.BackColor = TM.Win32.Menu;
            PanelR1.ButtonHilight = TM.Win32.ButtonHilight;
            PanelR1.ButtonShadow = TM.Win32.ButtonShadow;

            programcontainer.BackColor = TM.Win32.AppWorkspace;

            ClassicColorsPreview.BackColor = TM.Win32.Background;

            menucontainer0.BackColor = TM.Win32.MenuBar;

            highlight.BackColor = TM.Win32.Hilight;

            menuhilight.BackColor = TM.Win32.MenuHilight;

            LabelR6.ForeColor = TM.Win32.MenuText;
            LabelR1.ForeColor = TM.Win32.MenuText;

            LabelR5.ForeColor = TM.Win32.HilightText;

            LabelR2.ForeColor = TM.Win32.GrayText;

            LabelR9.ForeColor = TM.Win32.GrayText;

            LabelR4.ForeColor = TM.Win32.WindowText;

            LabelR13.BackColor = TM.Win32.InfoWindow;
            LabelR13.ForeColor = TM.Win32.InfoText;

            Refresh17BitPreference(TM);

            RetroShadow1.Refresh();
        }

        public void Refresh17BitPreference(Theme.Manager TM)
        {

            if (TM.Win32.EnableTheming)
            {
                // Theming Enabled (Menus Has colors and borders)
                Menu_Window.Flat = true;
                PanelR1.Flat = true;
                PanelR1.BackColor = TM.Win32.MenuHilight;
                PanelR1.ButtonShadow = TM.Win32.Hilight;

                menuhilight.BackColor = TM.Win32.MenuHilight;  // Filling of selected item

                highlight.BackColor = TM.Win32.Hilight; // Outer Border of selected item

                menucontainer0.BackColor = TM.Win32.MenuBar;

                LabelR3.ForeColor = TM.Win32.HilightText;
            }
            else
            {
                // Theming Disabled (Menus are retro 3d)
                Menu_Window.Flat = false;
                PanelR1.Flat = false;
                PanelR1.BackColor = TM.Win32.Menu;
                PanelR1.ButtonShadow = TM.Win32.ButtonShadow;

                menuhilight.BackColor = TM.Win32.Hilight; // Both will have same color

                highlight.BackColor = TM.Win32.Hilight; // Both will have same color

                menucontainer0.BackColor = TM.Win32.Menu;

                LabelR3.ForeColor = TM.Win32.MenuText;
            }

            Menu_Window.Invalidate();
            PanelR1.Invalidate();
            menuhilight.Invalidate();
            highlight.Invalidate();

        }

        public void ApplyCMDPreview(UI.Simulation.WinCMD CMD, Theme.Structures.Console Console, bool PS)
        {
            CMD.CMD_ColorTable00 = Console.ColorTable00;
            CMD.CMD_ColorTable01 = Console.ColorTable01;
            CMD.CMD_ColorTable02 = Console.ColorTable02;
            CMD.CMD_ColorTable03 = Console.ColorTable03;
            CMD.CMD_ColorTable04 = Console.ColorTable04;
            CMD.CMD_ColorTable05 = Console.ColorTable05;
            CMD.CMD_ColorTable06 = Console.ColorTable06;
            CMD.CMD_ColorTable07 = Console.ColorTable07;
            CMD.CMD_ColorTable08 = Console.ColorTable08;
            CMD.CMD_ColorTable09 = Console.ColorTable09;
            CMD.CMD_ColorTable10 = Console.ColorTable10;
            CMD.CMD_ColorTable11 = Console.ColorTable11;
            CMD.CMD_ColorTable12 = Console.ColorTable12;
            CMD.CMD_ColorTable13 = Console.ColorTable13;
            CMD.CMD_ColorTable14 = Console.ColorTable14;
            CMD.CMD_ColorTable15 = Console.ColorTable15;
            CMD.CMD_PopupForeground = Console.PopupForeground;
            CMD.CMD_PopupBackground = Console.PopupBackground;
            CMD.CMD_ScreenColorsForeground = Console.ScreenColorsForeground;
            CMD.CMD_ScreenColorsBackground = Console.ScreenColorsBackground;

            if (!Console.FontRaster)
            {
                {
                    Font temp = Font.FromLogFont(new NativeMethods.GDI32.LogFont() { lfFaceName = Console.FaceName, lfWeight = Console.FontWeight });
                    CMD.Font = new(temp.FontFamily, (int)Math.Round(Console.FontSize / 65536d), temp.Style);
                }
            }

            CMD.PowerShell = PS;
            CMD.Raster = Console.FontRaster;
            switch (Console.FontSize)
            {
                case 393220:
                    {
                        CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._4x6;
                        break;
                    }

                case 524294:
                    {
                        CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._6x8;
                        break;
                    }


                case 524296:
                    {
                        CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._8x8;
                        break;
                    }

                case 524304:
                    {
                        CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._16x8;
                        break;
                    }

                case 786437:
                    {
                        CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._5x12;
                        break;
                    }

                case 786439:
                    {
                        CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._7x12;
                        break;
                    }

                case 0:
                    {
                        CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._8x12;
                        break;
                    }

                case 786448:
                    {
                        CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._16x12;
                        break;
                    }

                case 1048588:
                    {
                        CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._12x16;
                        break;
                    }

                case 1179658:
                    {
                        CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._10x18;
                        break;
                    }

                default:
                    {
                        CMD.RasterSize = UI.Simulation.WinCMD.Raster_Sizes._8x12;
                        break;
                    }

            }

            CMD.Refresh();
        }

        public void LoadCursorsFromTM(Theme.Manager TM)
        {
            CursorTM_to_Cursor(Arrow, TM.Cursor_Arrow);
            CursorTM_to_Cursor(Help, TM.Cursor_Help);
            CursorTM_to_Cursor(AppLoading, TM.Cursor_AppLoading);
            CursorTM_to_Cursor(Busy, TM.Cursor_Busy);
            CursorTM_to_Cursor(Move_Cur, TM.Cursor_Move);
            CursorTM_to_Cursor(NS, TM.Cursor_NS);
            CursorTM_to_Cursor(EW, TM.Cursor_EW);
            CursorTM_to_Cursor(NESW, TM.Cursor_NESW);
            CursorTM_to_Cursor(NWSE, TM.Cursor_NWSE);
            CursorTM_to_Cursor(Up, TM.Cursor_Up);
            CursorTM_to_Cursor(Pen, TM.Cursor_Pen);
            CursorTM_to_Cursor(None, TM.Cursor_None);
            CursorTM_to_Cursor(Link, TM.Cursor_Link);
            CursorTM_to_Cursor(Pin, TM.Cursor_Pin);
            CursorTM_to_Cursor(Person, TM.Cursor_Person);
            CursorTM_to_Cursor(IBeam, TM.Cursor_IBeam);
            CursorTM_to_Cursor(Cross, TM.Cursor_Cross);

            foreach (UI.Controllers.CursorControl i in Cursors_Container.Controls)
            {
                if (i is UI.Controllers.CursorControl)
                {
                    i.Invalidate();
                }
            }
        }

        public void CursorTM_to_Cursor(UI.Controllers.CursorControl CursorControl, Theme.Structures.Cursor Cursor)
        {
            CursorControl.Prop_ArrowStyle = Cursor.ArrowStyle;
            CursorControl.Prop_CircleStyle = Cursor.CircleStyle;
            CursorControl.Prop_PrimaryColor1 = Cursor.PrimaryColor1;
            CursorControl.Prop_PrimaryColor2 = Cursor.PrimaryColor2;
            CursorControl.Prop_PrimaryColorGradient = Cursor.PrimaryColorGradient;
            CursorControl.Prop_PrimaryColorGradientMode = Cursor.PrimaryColorGradientMode;
            CursorControl.Prop_PrimaryNoise = Cursor.PrimaryColorNoise;
            CursorControl.Prop_PrimaryNoiseOpacity = Cursor.PrimaryColorNoiseOpacity;
            CursorControl.Prop_SecondaryColor1 = Cursor.SecondaryColor1;
            CursorControl.Prop_SecondaryColor2 = Cursor.SecondaryColor2;
            CursorControl.Prop_SecondaryColorGradient = Cursor.SecondaryColorGradient;
            CursorControl.Prop_SecondaryColorGradientMode = Cursor.SecondaryColorGradientMode;
            CursorControl.Prop_SecondaryNoise = Cursor.SecondaryColorNoise;
            CursorControl.Prop_SecondaryNoiseOpacity = Cursor.SecondaryColorNoiseOpacity;
            CursorControl.Prop_LoadingCircleBack1 = Cursor.LoadingCircleBack1;
            CursorControl.Prop_LoadingCircleBack2 = Cursor.LoadingCircleBack2;
            CursorControl.Prop_LoadingCircleBackGradient = Cursor.LoadingCircleBackGradient;
            CursorControl.Prop_LoadingCircleBackGradientMode = Cursor.LoadingCircleBackGradientMode;
            CursorControl.Prop_LoadingCircleBackNoise = Cursor.LoadingCircleBackNoise;
            CursorControl.Prop_LoadingCircleBackNoiseOpacity = Cursor.LoadingCircleBackNoiseOpacity;
            CursorControl.Prop_LoadingCircleHot1 = Cursor.LoadingCircleHot1;
            CursorControl.Prop_LoadingCircleHot2 = Cursor.LoadingCircleHot2;
            CursorControl.Prop_LoadingCircleHotGradient = Cursor.LoadingCircleHotGradient;
            CursorControl.Prop_LoadingCircleHotGradientMode = Cursor.LoadingCircleHotGradientMode;
            CursorControl.Prop_LoadingCircleHotNoise = Cursor.LoadingCircleHotNoise;
            CursorControl.Prop_LoadingCircleHotNoiseOpacity = Cursor.LoadingCircleHotNoiseOpacity;
            CursorControl.Prop_Shadow_Enabled = Cursor.Shadow_Enabled;
            CursorControl.Prop_Shadow_Color = Cursor.Shadow_Color;
            CursorControl.Prop_Shadow_Blur = Cursor.Shadow_Blur;
            CursorControl.Prop_Shadow_Opacity = Cursor.Shadow_Opacity;
            CursorControl.Prop_Shadow_OffsetX = Cursor.Shadow_OffsetX;
            CursorControl.Prop_Shadow_OffsetY = Cursor.Shadow_OffsetY;
        }
        #endregion

        #region Store form events
        private void Store_Load(object sender, EventArgs e)
        {
            Tabs.SelectedIndex = 0;
            oldAppearance = Program.Settings.Appearance;

            CenterToScreen();
            UpdateExtendedTitlebar();

            DLLFunc.RemoveFormTitlebarTextAndIcon(Handle);
            ShowIcon = false;
            FinishedLoadingInitialTMs = false;
            _Shown = false;

            this.LoadLanguage();
            ApplyStyle(this, true);

            CheckForIllegalCrossThreadCalls = false;         // Prevent exception error of cross-thread

            this.DoubleBuffer();
            Cursors_Container.DoubleBuffer();

            taskbar.CopycatFrom(Forms.MainFrm.taskbar);
            ActionCenter.CopycatFrom(Forms.MainFrm.ActionCenter);
            start.CopycatFrom(Forms.MainFrm.start);
            Window1.CopycatFrom(Forms.MainFrm.Window1);
            Window2.CopycatFrom(Forms.MainFrm.Window2);

            Apply_btn.Image = Forms.MainFrm.apply_btn.Image;
            RestartExplorer.Image = Forms.MainFrm.Button19.Image;

            WXP_Alert2.Text = Forms.MainFrm.WXP_Alert2.Text;
            WXP_Alert2.Size = WXP_Alert2.Parent.Size - new Size(40, 40);
            WXP_Alert2.Location = new(20, 20);

            pnl_preview.BackgroundImage = Forms.MainFrm.pnl_preview.BackgroundImage;
            pnl_preview_classic.BackgroundImage = pnl_preview.BackgroundImage;

            Status_lbl.Font = Fonts.ConsoleMedium;
            themeSize_lbl.Font = Fonts.ConsoleLarge;
            respacksize_lbl.Font = Fonts.ConsoleLarge;
            desc_txt.Font = Fonts.ConsoleLarge;
            Theme_MD5_lbl.Font = Fonts.Console;
        }

        private void Store_Shown(object sender, EventArgs e)
        {
            ShowIcon = true;
            _Shown = true;
            RemoveAllStoreItems(store_container);
            FilesFetcher.RunWorkerAsync();

            if (Program.Settings.Store.ShowTips)
                Forms.Store_Intro.ShowDialog();
        }

        private void Store_FormClosing(object sender, FormClosingEventArgs e)
        {
            Visible = false;

            // To prevent effect of a store theme on the other forms
            Program.Style.RenderingHint = Program.TM.MetricsFonts.Fonts_SingleBitPP ? System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit : System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            Program.Settings.Appearance.CustomColors = true;
            Program.Settings.Appearance.CustomTheme_DarkMode = oldAppearance.CustomTheme_DarkMode;
            Program.Settings.Appearance.RoundedCorners = oldAppearance.RoundedCorners;
            Program.Settings.Appearance.BackColor = oldAppearance.BackColor;
            Program.Settings.Appearance.AccentColor = oldAppearance.AccentColor;
            ApplyStyle(this);
            Program.Settings.Appearance.CustomColors = oldAppearance.CustomColors;

            Status_pnl.Visible = true;
            Status_lbl.SetText(Program.Lang.Store_CleaningFromMemory);
            Status_lbl.Refresh();

            FilesFetcher.CancelAsync();
            store_container.Visible = false;
            RemoveAllStoreItems(store_container);
            store_container.Visible = true;
            Tabs.SelectedIndex = 0;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Status_lbl.SetText(string.Empty);
        }

        #endregion

        #region Backgroundworkers to load Store themes managers
        public void OnlineMode()
        {
            Dnsapi.DnsFlushResolverCache();

            List<string> response = new();
            response.Clear();
            List<string> repos_list = new();
            repos_list.Clear();
            List<string> items = new();
            items.Clear();

            // Check by Ping if repos DB URL is accessible or not
            foreach (string DB in Program.Settings.Store.Online_Repositories)
            {
                string var = string.Empty;

                if (!DB.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                    var = $"https://{DB}";
                else
                    var = DB;

                Status_lbl.SetText(string.Format(Program.Lang.Store_Ping, var));

                if (Program.Ping(var))
                {
                    repos_list.Add(var);
                }
                else
                {
                    Status_lbl.SetText(string.Format(Program.Lang.Store_PingFailed, var));
                }

            }

            // Loop through all valid repos DBs
            foreach (string DB in repos_list)
            {

                // Try to generate a folder name dependent on the URL
                string reposName;
                if (DB.ToUpper().Contains("GITHUB.COM"))
                {
                    string[] x = DB.Replace("https://", string.Empty).Replace("http://", string.Empty).Split('/');
                    reposName = $"{x[1]}_{x[2]}";
                    reposName = string.Join("_", reposName.Split(System.IO.Path.GetInvalidFileNameChars()));
                }
                else
                {
                    reposName = string.Join("_", DB.Replace("https://", string.Empty).Replace("http://", string.Empty).Split(System.IO.Path.GetInvalidFileNameChars()));
                }

                // Get text of the DB from URL
                Status_lbl.SetText(string.Format(Program.Lang.Store_Accessing, DB));
                response.Clear();
                response = WebCL.DownloadString(DB).CList();
                items.Clear();

                // Add valid lines (Correct format) in a themes list
                foreach (string item in response)
                {
                    bool valid = true;
                    if (item.Contains("|") && item.Split('|').Count() >= 3)
                    {
                        foreach (string x in item.Split('|'))
                        {
                            if (string.IsNullOrWhiteSpace(x))
                            {
                                valid = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        valid = false;
                    }

                    if (valid)
                        items.Add(item);

                }

                FilesFetcher.ReportProgress(0);
                BeginInvoke(new Action(() => ProgressBar1.Visible = true));

                int i = 0;
                int allProgress = items.Count * 2;

                // Loop through valid lines from the themes list
                foreach (string item in items)
                {
                    string[] item_splitted = item.Split('|');

                    string MD5_ThemeFile = item_splitted[0].ToUpper();
                    string MD5_PackFile = item_splitted[1].ToUpper();
                    string URL_ThemeFile = item_splitted[2];
                    string URL_PackFile = string.Empty;
                    if (item_splitted.Count() == 4)
                        URL_PackFile = item_splitted[3];

                    // Create a folder inside AppData folder
                    string temp = URL_ThemeFile.Replace("?raw=true", string.Empty);
                    string FileName = temp.Split('/').Last();
                    temp = temp.Replace($"/{FileName}", string.Empty);
                    string FolderName = temp.Split('/').Last();
                    string Dir = PathsExt.StoreCache;
                    if (!string.IsNullOrWhiteSpace(FolderName))
                        Dir += $@"\{reposName}\{FolderName}";
                    if (!System.IO.Directory.Exists(Dir))
                        System.IO.Directory.CreateDirectory(Dir);

                    Status_lbl.SetText(string.Empty);

                    // Download the theme (*.wpth)
                    if (System.IO.File.Exists($@"{Dir}\{FileName}"))
                    {
                        // If it exists, check MD5, if it is changed, redownload the theme
                        if ((CalculateMD5($@"{Dir}\{FileName}") ?? string.Empty) != (MD5_ThemeFile ?? string.Empty))
                        {
                            System.IO.File.Delete($@"{Dir}\{FileName}");
                            Status_lbl.SetText(string.Format(Program.Lang.Store_UpdateTheme, FileName, URL_ThemeFile));
                            try
                            {
                                WebCL.DownloadFile(URL_ThemeFile, $@"{Dir}\{FileName}");
                            }
                            catch
                            {
                            }
                        }
                    }
                    else
                    {
                        Status_lbl.SetText(string.Format(Program.Lang.Store_DownloadTheme, FileName, URL_ThemeFile));
                        try
                        {
                            WebCL.DownloadFile(URL_ThemeFile, $@"{Dir}\{FileName}");
                        }
                        catch
                        {
                        }
                    }

                    i += 1;
                    if (allProgress > 0)
                        FilesFetcher.ReportProgress((int)Math.Round(i / (double)allProgress * 100d));

                    // Convert themes managers into StoreItems, and exclude the old formats of WPTH
                    if (System.IO.File.Exists($@"{Dir}\{FileName}") && _Converter.GetFormat($@"{Dir}\{FileName}") == Converter_CP.WP_Format.JSON)
                    {
                        try
                        {
                            Status_lbl.SetText(string.Format(Program.Lang.Store_LoadingTheme, FileName));

                            using (Theme.Manager TM = new(Theme.Manager.Source.File, $@"{Dir}\{FileName}", true))
                            {

                                UI.Controllers.StoreItem ctrl = new()
                                {
                                    FileName = $@"{Dir}\{FileName}",
                                    TM = TM,
                                    MD5_ThemeFile = MD5_ThemeFile,
                                    MD5_PackFile = MD5_PackFile,
                                    DoneByWinPaletter = (DB.ToUpper() ?? string.Empty) == (Properties.Resources.Link_StoreMainDB.ToUpper() ?? string.Empty),
                                    Size = new(w, h),
                                    URL_ThemeFile = URL_ThemeFile,
                                    URL_PackFile = URL_PackFile
                                };

                                if (ctrl.DoneByWinPaletter)
                                    ctrl.TM.Info.Author = Application.ProductName;

                                ctrl.Click += StoreItem_Clicked;
                                ctrl.ThemeManagerChanged += StoreItem_ThemeManagerChanged;
                                ctrl.MouseEnter += StoreItem_MouseEnter;
                                ctrl.MouseLeave += StoreItem_MouseLeave;

                                BeginInvoke(new Action(() => store_container.Controls.Add(ctrl)));

                            }
                        }

                        catch (Exception)
                        {

                        }
                    }

                    Status_lbl.SetText(string.Empty);

                    i += 1;

                    if (allProgress > 0)
                        FilesFetcher.ReportProgress((int)Math.Round(i / (double)allProgress * 100d));

                }

                // Finalizing
                BeginInvoke(new Action(() => ProgressBar1.Visible = false));

                TMList.Clear();
            }

            FinishedLoadingInitialTMs = true;
        }

        public void OfflineMode()
        {
            BeginInvoke(new Action(() =>
                {
                    ProgressBar1.Visible = true;
                    store_container.Visible = false;
                }));

            int i = 0;
            int allProgress = 0;


            foreach (string folder in Program.Settings.Store.Offline_Directories)
            {

                if (System.IO.Directory.Exists(folder))
                {
                    Status_lbl.SetText($"Accessing themes from folder \"{folder}\"");
                    allProgress += System.IO.Directory.GetFiles(folder, "*.wpth", Program.Settings.Store.Offline_SubFolders ? System.IO.SearchOption.AllDirectories : System.IO.SearchOption.TopDirectoryOnly).Count();
                }

            }

            allProgress *= 2;

            foreach (string folder in Program.Settings.Store.Offline_Directories)
            {

                if (System.IO.Directory.Exists(folder))
                {

                    foreach (string file in System.IO.Directory.GetFiles(folder, "*.wpth", Program.Settings.Store.Offline_SubFolders ? System.IO.SearchOption.AllDirectories : System.IO.SearchOption.TopDirectoryOnly))
                    {

                        try
                        {
                            if (!TMList.ContainsKey(file))
                            {

                                Status_lbl.SetText($"Enumerating themes: \"{file}\"");

                                using (Theme.Manager TMx = new(Theme.Manager.Source.File, file, true))
                                {
                                    TMList.Add(file, TMx);
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }

                        i += 1;

                        if (allProgress > 0)
                            FilesFetcher.ReportProgress((int)Math.Round(i / (double)allProgress * 100d));
                    }
                }
            }


            foreach (KeyValuePair<string, Manager> StoreItem in TMList)
            {
                Status_lbl.SetText($"Loading theme \"{StoreItem.Value.Info.ThemeName}\"");

                UI.Controllers.StoreItem ctrl = new()
                {
                    FileName = StoreItem.Key,
                    TM = StoreItem.Value,
                    MD5_ThemeFile = CalculateMD5(StoreItem.Key),
                    DoneByWinPaletter = false,
                    Size = new(w, h),
                    URL_ThemeFile = new System.IO.FileInfo(StoreItem.Key).FullName
                };

                if (ctrl.DoneByWinPaletter)
                    ctrl.TM.Info.Author = Application.ProductName;

                ctrl.Click += StoreItem_Clicked;
                ctrl.ThemeManagerChanged += StoreItem_ThemeManagerChanged;
                ctrl.MouseEnter += StoreItem_MouseEnter;
                ctrl.MouseLeave += StoreItem_MouseLeave;

                BeginInvoke(new Action(() => store_container.Controls.Add(ctrl)));

                i += 1;

                if (allProgress > 0)
                    FilesFetcher.ReportProgress((int)Math.Round(i / (double)allProgress * 100d));
            }

            BeginInvoke(new Action(() =>
                {
                    ProgressBar1.Visible = false;
                    store_container.Visible = true;
                }));

            Status_lbl.SetText(string.Empty);

            TMList.Clear();

            FinishedLoadingInitialTMs = true;
        }

        private void FilesFetcher_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            if (Program.Settings.Store.Online_or_Offline)
            {

                if (!Program.IsNetworkAvailable())
                {
                    Status_lbl.SetText(Program.Lang.Store_NoNetwork);

                    if (MsgBox(Program.Lang.Store_NoNetwork, MessageBoxButtons.YesNo, MessageBoxIcon.Question, Program.Lang.Store_TryOffline) == DialogResult.Yes)
                    {
                        StartedAsOnlineOrOffline = false;
                        OfflineMode();
                        return;
                    }
                }

                else
                {
                    StartedAsOnlineOrOffline = true;
                    OnlineMode();
                }
            }

            else
            {
                StartedAsOnlineOrOffline = false;
                OfflineMode();
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            Program.Animator.HideSync(Status_pnl);
        }

        private void FilesFetcher_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            try
            {
                ProgressBar1.Value = Math.Max(Math.Min(e.ProgressPercentage, ProgressBar1.Maximum), ProgressBar1.Minimum);
            }
            catch
            {
            }
        }

        private void FilesFetcher_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {

        }

        #endregion

        #region Store item events

        public void StoreItem_Clicked(object sender, EventArgs e)
        {

            switch (((MouseEventArgs)e).Button)
            {
                case MouseButtons.Right:
                    {
                        {
                            StoreItem temp = (UI.Controllers.StoreItem)sender;
                            Forms.Store_Hover.Close();

                            selectedItem = (UI.Controllers.StoreItem)sender;

                            Forms.Store_Hover.Show();

                            Adjust_Preview(temp.TM);
                            tabs_preview.SelectedIndex = 0;
                            Forms.Store_Hover.img0 = tabs_preview.ToBitmap();
                            tabs_preview.SelectedIndex = 1;
                            Forms.Store_Hover.img1 = tabs_preview.ToBitmap();
                            Forms.Store_Hover.BackgroundImage = Forms.Store_Hover.img0;

                        }

                        break;
                    }

                default:
                    {
                        selectedItem = (UI.Controllers.StoreItem)sender;
                        Cursor = Cursors.AppStarting;
                        StoreItem1.TM = selectedItem.TM;
                        StoreItem1.DoneByWinPaletter = selectedItem.DoneByWinPaletter;
                        Theme_MD5_lbl.Text = $"MD5: {selectedItem.MD5_ThemeFile}";

                        {
                            UI.Controllers.StoreItem StoreItem = selectedItem;
                            Program.Animator.HideSync(Tabs);
                            search_panel.Visible = false;

                            Titlebar_lbl.Text = $"{StoreItem.TM.Info.ThemeName} - {Program.Lang.By} {StoreItem.TM.Info.Author}";
                            if (Theme.Manager.IsFontInstalled(StoreItem.TM.MetricsFonts.CaptionFont.Name))
                            {
                                Titlebar_lbl.Font = new(StoreItem.TM.MetricsFonts.CaptionFont.Name, Titlebar_lbl.Font.Size, Titlebar_lbl.Font.Style);
                            }
                            else
                            {
                                Titlebar_lbl.Font = new("Segoe UI", Titlebar_lbl.Font.Size, Titlebar_lbl.Font.Style);
                            }

                            if (StoreItem.TM.AppTheme.Enabled)
                            {
                                Program.Settings.Appearance.CustomColors = StoreItem.TM.AppTheme.Enabled;
                                Program.Settings.Appearance.CustomTheme_DarkMode = StoreItem.TM.AppTheme.DarkMode;
                                Program.Settings.Appearance.RoundedCorners = StoreItem.TM.AppTheme.RoundCorners;
                                Program.Settings.Appearance.BackColor = StoreItem.TM.AppTheme.BackColor;
                                Program.Settings.Appearance.AccentColor = StoreItem.TM.AppTheme.AccentColor;
                                ApplyStyle(this, true);
                            }

                            if (StoreItem.TM.AppTheme.Enabled)
                            {
                                Label14.ForeColor = StoreItem.TM.AppTheme.DarkMode ? Color.White.CB((float)-0.3d) : Color.Black.CB(0.3f);
                            }
                            else
                            {
                                Label14.ForeColor = Program.Style.DarkMode ? Color.White.CB((float)-0.3d) : Color.Black.CB(0.3f);
                            }
                            Label6.ForeColor = Label14.ForeColor;
                            Theme_MD5_lbl.ForeColor = Label14.ForeColor;

                            Adjust_Preview(StoreItem.TM);
                            ApplyRetroPreview(StoreItem.TM);
                            SetClassicMetrics(StoreItem.TM);
                            ApplyCMDPreview(CMD1, StoreItem.TM.CommandPrompt, false);
                            ApplyCMDPreview(CMD2, StoreItem.TM.PowerShellx86, true);
                            ApplyCMDPreview(CMD3, StoreItem.TM.PowerShellx64, true);
                            LoadCursorsFromTM(StoreItem.TM);
                            Program.Style.RenderingHint = StoreItem.TM.MetricsFonts.Fonts_SingleBitPP ? System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit : System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                            foreach (UI.Controllers.CursorControl i in Cursors_Container.Controls)
                            {
                                if (i is UI.Controllers.CursorControl)
                                {
                                    if (i.Prop_Cursor == Paths.CursorType.AppLoading | i.Prop_Cursor == Paths.CursorType.Busy)
                                        AnimateList.Add(i);
                                }
                            }

                            themeSize_lbl.Text = new System.IO.FileInfo(StoreItem.FileName).Length.SizeString();

                            if (!string.IsNullOrWhiteSpace(StoreItem.MD5_PackFile) && StoreItem.MD5_PackFile != "0")
                            {
                                Task.Run(() =>
                                        {
                                            respacksize_lbl.SetText(Program.Lang.Store_Calculating);
                                            long Pack_Size = GetFileSizeFromURL(StoreItem.URL_PackFile);
                                            respacksize_lbl.SetText(Pack_Size > 0L ? Pack_Size.SizeString() : 0.SizeString());
                                        });
                            }
                            else
                            {
                                respacksize_lbl.Text = 0.SizeString();
                            }

                            desc_txt.Text = StoreItem.TM.Info.Description;

                            if (Program.Version.CompareTo(StoreItem.TM.Info.AppVersion) != -1)
                            {
                                VersionAlert_lbl.Visible = false;
                            }
                            else
                            {
                                VersionAlert_lbl.Visible = true;
                                VersionAlert_lbl.Text = string.Format(Program.Lang.Store_LowAppVersionAlert, StoreItem.TM.Info.AppVersion, Program.Version);
                            }

                            List<string> os_list = new();
                            os_list.Clear();

                            if (StoreItem.TM.Info.DesignedFor_Win11)
                                os_list.Add(Program.Lang.OS_Win11);
                            if (StoreItem.TM.Info.DesignedFor_Win10)
                                os_list.Add(Program.Lang.OS_Win10);
                            if (StoreItem.TM.Info.DesignedFor_Win81)
                                os_list.Add(Program.Lang.OS_Win81);
                            if (StoreItem.TM.Info.DesignedFor_Win7)
                                os_list.Add(Program.Lang.OS_Win7);
                            if (StoreItem.TM.Info.DesignedFor_WinVista)
                                os_list.Add(Program.Lang.OS_WinVista);
                            if (StoreItem.TM.Info.DesignedFor_WinXP)
                                os_list.Add(Program.Lang.OS_WinXP);

                            string os_format = string.Empty;
                            if (os_list.Count == 1)
                            {
                                os_format = os_list[0];
                            }
                            else if (os_list.Count == 2)
                            {
                                os_format = $"{os_list[0]} && {os_list[1]}";
                            }
                            else if (os_list.Count > 2)
                            {
                                for (int i = 0, loopTo = os_list.Count - 3; i <= loopTo; i++)
                                    os_format += $"{os_list[i]}, ";
                                os_format += $"{os_list[os_list.Count - 2]} && {os_list[os_list.Count - 1]}";
                            }
                            SupportedOS_lbl.Text = os_format;
                            if (os_list.Count < 6)
                            {
                                Label26.Text = Program.Lang.Store_ThemeDesignedFor0;
                            }
                            else
                            {
                                Label26.Text = Program.Lang.Store_ThemeDesignedFor1;
                            }

                            if (StoreItem.TM.AppTheme.Enabled)
                            {
                                desc_txt.ForeColor = StoreItem.TM.AppTheme.DarkMode ? Color.White : Color.Black;
                            }
                            else
                            {
                                desc_txt.ForeColor = Program.Style.DarkMode ? Color.White : Color.Black;
                            }

                            CMD1.Visible = StoreItem.TM.CommandPrompt.Enabled;
                            CMD2.Visible = StoreItem.TM.PowerShellx86.Enabled;
                            CMD3.Visible = StoreItem.TM.PowerShellx64.Enabled;
                            Panel1.Visible = StoreItem.TM.Cursor_Enabled;
                            author_url_button.Visible = !string.IsNullOrWhiteSpace(StoreItem.TM.Info.AuthorSocialMediaLink);

                            Tabs.SelectedIndex = 1;

                            Program.Animator.ShowSync(Tabs);

                            // ' '' ''FluentTransitions.Transition.With(Titlebar_panel, nameof(BackColor), Titlebar_panel.BackColor, .Manager.Info.Color2, 10, 15)
                        }


                        Cursor = Cursors.Default;
                        break;
                    }

            }
        }

        public void StoreItem_MouseEnter(object sender, EventArgs e)
        {
            hoveredItem = (UI.Controllers.StoreItem)sender;

        }

        public void StoreItem_MouseLeave(object sender, EventArgs e)
        {

        }

        public void StoreItem_ThemeManagerChanged(object sender, EventArgs e)
        {
            if (FinishedLoadingInitialTMs)
            {
                {
                    StoreItem temp = (UI.Controllers.StoreItem)sender;
                    Adjust_Preview(temp.TM);
                    temp.Refresh();
                }
            }
        }
        #endregion

        #region Voids\Functions

        #region    Store
        public void Apply_Theme()
        {
            ref Settings.Structures.Appearance Appearance = ref Program.Settings.Appearance;
            Appearance.CustomColors = selectedItem.TM.AppTheme.Enabled;
            Appearance.BackColor = selectedItem.TM.AppTheme.BackColor;
            Appearance.AccentColor = selectedItem.TM.AppTheme.AccentColor;
            Appearance.CustomTheme_DarkMode = selectedItem.TM.AppTheme.DarkMode;
            Appearance.RoundedCorners = selectedItem.TM.AppTheme.RoundCorners;
            oldAppearance = Appearance;
            ApplyStyle(null, true);

            using (Theme.Manager TMx = new(Theme.Manager.Source.File, selectedItem.FileName))
            {
                if (selectedItem.DoneByWinPaletter)
                    TMx.Info.Author = Application.CompanyName;

                Forms.ThemeLog.Apply_Theme(TMx, true);

                Program.TM_Original = (Theme.Manager)TMx.Clone();
            }

            UpdateExtendedTitlebar();
        }

        public void DoActionsAfterPackDownload()
        {
            if (ApplyOrEditToggle)
            {
                // Apply button is pressed
                Forms.Store_CPToggles.TM = selectedItem.TM;
                if (Forms.Store_CPToggles.ShowDialog() == DialogResult.OK)
                {
                    Apply_Theme();
                    if (selectedItem.DoneByWinPaletter)
                        Program.TM.Info.Author = Application.CompanyName;
                    Program.TM = selectedItem.TM;
                    Program.TM_Original = (Theme.Manager)Program.TM.Clone();
                    Forms.MainFrm.ApplyStylesToElements(Program.TM, false);
                    Forms.MainFrm.ApplyColorsToElements(Program.TM);
                    Forms.MainFrm.LoadFromTM(Program.TM);
                    UpdateTitlebarColors();
                }
            }
            else
            {
                // Edit button is pressed
                WindowState = FormWindowState.Minimized;
                Forms.ComplexSave.GetResponse(Forms.MainFrm.SaveFileDialog1, null, null, null);
                Program.TM_Original = (Theme.Manager)Program.TM.Clone();
                Program.TM = new(Theme.Manager.Source.File, selectedItem.FileName);
                if (selectedItem.DoneByWinPaletter)
                    Program.TM.Info.Author = Application.CompanyName;
                Forms.MainFrm.ApplyStylesToElements(Program.TM, false);
                Forms.MainFrm.LoadFromTM(Program.TM);
                Forms.MainFrm.ApplyColorsToElements(Program.TM);
            }
        }

        public void RemoveAllStoreItems(FlowLayoutPanel Container)
        {
            for (int x = 0, loopTo = Container.Controls.Count - 1; x <= loopTo; x++)
            {

                if (Container.Controls[0] is UI.Controllers.StoreItem)
                {
                    ((UI.Controllers.StoreItem)Container.Controls[0]).MouseClick -= StoreItem_Clicked;
                    ((UI.Controllers.StoreItem)Container.Controls[0]).ThemeManagerChanged -= StoreItem_ThemeManagerChanged;
                    ((UI.Controllers.StoreItem)Container.Controls[0]).MouseEnter -= StoreItem_MouseEnter;
                    ((UI.Controllers.StoreItem)Container.Controls[0]).MouseLeave -= StoreItem_MouseLeave;
                }

                Container.Controls[0].Dispose();
            }
            Container.Controls.Clear();
        }

        public void PerformSearch()
        {
            string search_text = search_box.Text.TrimStart().TrimEnd().Trim().Replace(" ", string.Empty).ToUpper();

            if (string.IsNullOrWhiteSpace(search_text))
                return;

            Dictionary<string, UI.Controllers.StoreItem> lst = new();
            lst.Clear();

            foreach (StoreItem st_itm in store_container.Controls.OfType<UI.Controllers.StoreItem>())
                lst.Add(st_itm.FileName, st_itm);

            RemoveAllStoreItems(search_results);

            int found_sum = 0;

            foreach (KeyValuePair<string, StoreItem> st_item in lst)
            {
                if ((Program.Settings.Store.Search_ThemeNames && st_item.Value.TM.Info.ThemeName.TrimStart().TrimEnd().Trim().Replace(" ", string.Empty).ToUpper().Contains(search_text)) | (Program.Settings.Store.Search_AuthorsNames && st_item.Value.TM.Info.Author.TrimStart().TrimEnd().Trim().Replace(" ", string.Empty).ToUpper().Contains(search_text)) | (Program.Settings.Store.Search_Descriptions && st_item.Value.TM.Info.Description.TrimStart().TrimEnd().Trim().Replace(" ", string.Empty).ToUpper().Contains(search_text)))

                {

                    found_sum += 1;

                    UI.Controllers.StoreItem ctrl = new()
                    {
                        FileName = st_item.Key,
                        TM = st_item.Value.TM,
                        MD5_ThemeFile = CalculateMD5(st_item.Key),
                        DoneByWinPaletter = st_item.Value.DoneByWinPaletter,
                        Size = new(w, h),
                        URL_ThemeFile = st_item.Value.URL_ThemeFile
                    };

                    if (ctrl.DoneByWinPaletter)
                        ctrl.TM.Info.Author = Application.ProductName;

                    ctrl.Click += StoreItem_Clicked;
                    ctrl.ThemeManagerChanged += StoreItem_ThemeManagerChanged;
                    ctrl.MouseEnter += StoreItem_MouseEnter;
                    ctrl.MouseLeave += StoreItem_MouseLeave;

                    BeginInvoke(new Action(() => search_results.Controls.Add(ctrl)));

                }
            }

            Titlebar_lbl.Text = $"Search results ({found_sum})";

            Tabs.SelectedIndex = 2;

            lst.Clear();
        }
        #endregion

        #region    Helpers
        private string CalculateMD5(string path)
        {
            if (System.IO.File.Exists(path))
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] hash = md5.ComputeHash(System.IO.File.ReadAllBytes(path));
                    string result = BitConverter.ToString(hash).Replace("-", string.Empty);
                    return result.ToUpper();
                }
            }
            else
            {
                return "0";
            }

        }

        public long GetFileSizeFromURL(string url)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(url))
                {
                    long result = 0L;
                    WebRequest req = WebRequest.Create(url);
                    req.Method = "HEAD";
                    long contentLength = default;

                    using (WebResponse resp = req.GetResponse())
                    {

                        if (long.TryParse(resp.Headers.Get("Content-Length"), out contentLength))
                        {
                            result = contentLength;
                        }
                    }

                    return result;
                }
                else
                {
                    return 0L;
                }
            }
            catch
            {
                return 0L;
            }
        }

        public void UpdateExtendedTitlebar()
        {
            System.Windows.Forms.Padding Pd = new(0, Titlebar_panel.Height, 0, 0);
            Titlebar_panel.BackColor = Color.FromArgb(0, 0, 0);
            bool CompositionEnabled = DWMAPI.IsCompositionEnabled();

            if (OS.W12 || OS.W11 || OS.W10)
            {
                CompositionEnabled &= Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", true));
            }

            if (CompositionEnabled)
            {
                Titlebar_lbl.DrawOnGlass = true;
                Titlebar_panel.BackColor = Color.Black;

                if (OS.W12 || OS.W11)
                {
                    this.DrawMica(Pd);
                }

                else if (OS.W10 || OS.W81 || OS.W8 || OS.W7 || OS.WVista)
                {
                    this.DrawAero(Pd);
                    if (OS.W10)
                        DLLFunc.DarkTitlebar(Handle, Program.Style.DarkMode);
                }

                else
                {
                    this.DrawMica(Pd);
                }
            }

            else if (OS.W7 || OS.WVista)
            {
                Titlebar_lbl.DrawOnGlass = false;

                if (OS.W7)
                {
                    if (Program.TM.Windows7.Theme != Theme.Structures.Windows7.Themes.Classic)
                    {
                        Titlebar_panel.BackColor = Color.FromArgb(185, 209, 234);
                    }
                    else
                    {
                        Titlebar_panel.BackColor = Program.TM.Win32.ButtonFace;
                    }
                }

                else if (OS.WVista)
                {
                    if (Program.TM.WindowsVista.Theme != Theme.Structures.Windows7.Themes.Classic)
                    {
                        Titlebar_panel.BackColor = Color.FromArgb(185, 209, 234);
                    }
                    else
                    {
                        Titlebar_panel.BackColor = Program.TM.Win32.ButtonFace;
                    }

                }
            }

            else if (OS.WXP)
            {
                Titlebar_lbl.DrawOnGlass = false;

                if (Program.TM.WindowsXP.Theme != Theme.Structures.WindowsXP.Themes.Classic)
                {
                    Titlebar_panel.BackColor = Program.Style.Schemes.Main.Colors.Back;
                }
                else
                {
                    Titlebar_panel.BackColor = Program.TM.Win32.ButtonFace;
                }
            }

            else
            {
                Titlebar_lbl.DrawOnGlass = true;
                if (OS.W12 || OS.W11 || OS.W10)
                    DLLFunc.DarkTitlebar(Handle, Program.Style.DarkMode);
                this.DrawAero(Pd);


            }

            Titlebar_lbl.DrawOnGlass = Titlebar_lbl.DrawOnGlass;

            UpdateTitlebarColors();
        }

        public void UpdateTitlebarColors()
        {
            Titlebar_lbl.ForeColor = Program.Style.DarkMode ? Color.White : Color.Black;
            search_box.ForeColor = Program.Style.DarkMode ? Color.White : Color.Black;
            back_btn.Image = Program.Style.DarkMode ? Properties.Resources.Store_BackBtn : Properties.Resources.Store_BackBtn.Invert();
        }
        #endregion

        #endregion

        #region Timers
        private void Cursor_Timer_Tick(object sender, EventArgs e)
        {
            if (!_Shown)
                return;

            try
            {
                foreach (UI.Controllers.CursorControl i in AnimateList)
                {
                    i.Angle = Angle;
                    i.Refresh();

                    if (Angle + Increment >= 360f)
                        Angle = 0f;
                    Angle += Increment;

                    if (Angle == 180f & Cycles >= 2)
                    {
                        i.Angle = 180f;
                        Cursor_Timer.Enabled = false;
                        Cursor_Timer.Stop();
                    }
                    else if (Angle == 180f)
                    {
                        Cycles += 1;
                    }

                }
            }
            catch
            {
            }
        }
        #endregion

        #region Buttons Events
        private void Back_btn_Click(object sender, EventArgs e)
        {

            Program.Animator.HideSync(Tabs);
            Program.Style.RenderingHint = Program.TM.MetricsFonts.Fonts_SingleBitPP ? System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit : System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            if (selectedItem is not null && selectedItem.TM.AppTheme.Enabled)
            {
                Program.Settings = new(Settings.Mode.Registry);
                GetRoundedCorners();
                GetDarkMode();
                ApplyStyle(this, true);
            }

            RemoveAllStoreItems(search_results);

            Titlebar_lbl.Font = new("Segoe UI", Titlebar_lbl.Font.Size, Titlebar_lbl.Font.Style);
            Tabs.SelectedIndex = 0;
            Program.Animator.HideSync(back_btn);

            Titlebar_lbl.Text = Text;
            Program.Animator.ShowSync(Tabs);
        }

        #region    Applying row
        private void Apply_Edit_btn_Click(object sender, EventArgs e)
        {
            ApplyOrEditToggle = sender == Apply_btn;

            if (!string.IsNullOrWhiteSpace(selectedItem.TM.Info.License))
            {
                Forms.Store_ThemeLicense.TextBox1.Text = selectedItem.TM.Info.License;
                if (!(Forms.Store_ThemeLicense.ShowDialog() == DialogResult.OK))
                    return;
            }

            if (StartedAsOnlineOrOffline)
            {
                string temp = selectedItem.URL_PackFile.Replace("?raw=true", string.Empty);
                string FileName = temp.Split('/').Last();
                temp = temp.Replace($"/{FileName}", string.Empty);
                string FolderName = temp.Split('/').Last();
                string Dir;
                if (System.IO.File.Exists(selectedItem.FileName))
                {
                    Dir = new System.IO.FileInfo(selectedItem.FileName).Directory.FullName;
                }
                else
                {
                    Dir = selectedItem.FileName.Replace($@"\{selectedItem.FileName.Split('\\').Last()}", string.Empty);
                }
                if (!System.IO.Directory.Exists(Dir))
                    System.IO.Directory.CreateDirectory(Dir);

                if (selectedItem.MD5_PackFile != "0")
                {
                    if (System.IO.File.Exists($@"{Dir}\{FileName}") && (CalculateMD5($@"{Dir}\{FileName}") ?? string.Empty) != (selectedItem.MD5_PackFile ?? string.Empty) || !System.IO.File.Exists($@"{Dir}\{FileName}"))
                    {
                        try
                        {
                            Forms.Store_DownloadProgress.URL = selectedItem.URL_PackFile;
                            Forms.Store_DownloadProgress.File = $@"{Dir}\{FileName}";
                            Forms.Store_DownloadProgress.ThemeName = selectedItem.TM.Info.ThemeName;
                            Forms.Store_DownloadProgress.ThemeVersion = selectedItem.TM.Info.ThemeVersion;
                            if (Forms.Store_DownloadProgress.ShowDialog() == DialogResult.OK)
                                DoActionsAfterPackDownload();
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        DoActionsAfterPackDownload();
                    }
                }

                else
                {
                    if (System.IO.File.Exists($@"{Dir}\{FileName}"))
                    {
                        try
                        {
                            FileSystem.Kill($@"{Dir}\{FileName}");
                        }
                        catch
                        {
                        }
                    }

                    DoActionsAfterPackDownload();
                }
            }
            else
            {
                DoActionsAfterPackDownload();
            }

        }

        private void RestartExplorer_Click(object sender, EventArgs e)
        {
            Program.RestartExplorer();
        }

        #endregion

        #region    Search
        private void Search_btn_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }
        private void Search_filter_btn_Click(object sender, EventArgs e)
        {
            Forms.Store_SearchFilter.ShowDialog();
        }

        #endregion

        #region    Cursors
        private void Cur_anim_btn_Click(object sender, EventArgs e)
        {
            Angle = 180f;
            Cycles = 0;
            Cursor_Timer.Enabled = true;
            Cursor_Timer.Start();
        }

        private void Cur_tip_btn_Click(object sender, EventArgs e)
        {
            MsgBox(Program.Lang.ScalingTip, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #endregion

        #region Major Tab
        private void Tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Tabs.SelectedIndex != 0)
            {
                Program.Animator.ShowSync(back_btn);
            }
            else
            {
                Program.Animator.HideSync(back_btn);
            }

            search_panel.Visible = Tabs.SelectedIndex == 0 | Tabs.SelectedIndex == 2;
        }

        #endregion

        #region Others
        private void CursorsSize_Bar_Scroll(object sender)
        {
            if (!_Shown) return;

            foreach (UI.Controllers.CursorControl i in Cursors_Container.Controls)
            {
                i.Prop_Scale = (float)((UI.WP.Trackbar)sender).Value / 100f;
                i.Width = (int)Math.Round(32f * i.Prop_Scale + 32f);
                i.Height = i.Width;
                i.Refresh();
            }

            Label17.Text = $"{Program.Lang.Scaling} ({(float)((UI.WP.Trackbar)sender).Value / 100f}x)";
        }

        private void Search_box_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                PerformSearch();
        }

        private Point newPoint = new();
        private Point oldPoint = new();

        private void CustomTitlebar_MouseDown(object sender, MouseEventArgs e)
        {
            oldPoint = MousePosition - (Size)Location;
        }

        private void CustomTitlebar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                newPoint = MousePosition - (Size)oldPoint;
                Location = newPoint;
            }
        }

        private void Author_url_button_Click(object sender, EventArgs e)
        {

            if (MsgBox(Program.Lang.Store_AuthorURLRedirect, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, selectedItem.TM.Info.AuthorSocialMediaLink) == DialogResult.Yes)
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(selectedItem.TM.Info.AuthorSocialMediaLink))
                        Process.Start(selectedItem.TM.Info.AuthorSocialMediaLink);
                }
                catch
                {
                }
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(selectedItem.TM.Info.License))
            {
                Forms.Store_ThemeLicense.TextBox1.Text = selectedItem.TM.Info.License;
                if (!(Forms.Store_ThemeLicense.ShowDialog() == DialogResult.OK))
                    return;
            }

            using (Ookii.Dialogs.WinForms.VistaFolderBrowserDialog FD = new())
            {
                if (FD.ShowDialog() == DialogResult.OK)
                {
                    string filename = $@"{FD.SelectedPath}\{new System.IO.FileInfo(selectedItem.FileName).Name}";

                    if (!System.IO.Directory.Exists(FD.SelectedPath))
                        System.IO.Directory.CreateDirectory(FD.SelectedPath);
                    if (System.IO.File.Exists(filename))
                        System.IO.File.Delete(filename);

                    System.IO.File.Copy(selectedItem.FileName, filename);

                    if (selectedItem.MD5_PackFile != "0")
                    {
                        string themepackfilename = $@"{FD.SelectedPath}\{new System.IO.FileInfo(selectedItem.FileName).Name}";
                        themepackfilename = themepackfilename.Replace(themepackfilename.Split('.').Last(), "wptp");

                        Forms.Store_DownloadProgress.URL = selectedItem.URL_PackFile;
                        Forms.Store_DownloadProgress.File = themepackfilename;
                        Forms.Store_DownloadProgress.ThemeName = selectedItem.TM.Info.ThemeName;
                        Forms.Store_DownloadProgress.ThemeVersion = selectedItem.TM.Info.ThemeVersion;
                        Forms.Store_DownloadProgress.ShowDialog();
                    }
                }
            }

        }

        #endregion

    }
}