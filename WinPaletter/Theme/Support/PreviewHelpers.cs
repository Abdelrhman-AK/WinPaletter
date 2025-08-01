﻿using System;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.UI.Simulation;

namespace WinPaletter
{
    /// <summary>
    /// Helpers for WinPaletter theme preview
    /// </summary>
    public class PreviewHelpers
    {
        /// <summary>
        /// Style of selected Windows edition (used for theme preview)
        /// </summary>
        public enum WindowStyle
        {
            /// <summary>Windows 12 (Placeholder until Windows 12 release)</summary>
            W12,
            /// <summary>Windows 11</summary>
            W11,
            /// <summary>Windows 10</summary>
            W10,
            /// <summary>Windows 8.1</summary>
            W81,
            /// <summary>Windows 8</summary>
            W8,
            /// <summary>Windows 7</summary>
            W7,
            /// <summary>Windows Vista</summary>
            WVista,
            /// <summary>Windows WXP</summary>
            WXP
        }

        /// <summary>
        /// Change labels of Windows 11/10 in their forms according to preview.
        /// </summary>
        /// <param name="TM">WinPaletter theme manager</param>
        /// <param name="Style">Selected Windows edition</param>
        /// <param name="lbl1"></param>
        /// <param name="lbl2"></param>
        /// <param name="lbl3"></param>
        /// <param name="lbl4"></param>
        /// <param name="lbl5"></param>
        /// <param name="lbl6"></param>
        /// <param name="lbl7"></param>
        /// <param name="lbl8"></param>
        /// <param name="lbl9"></param>
        /// <param name="pic1"></param>
        /// <param name="pic2"></param>
        /// <param name="pic3"></param>
        /// <param name="pic4"></param>
        /// <param name="pic5"></param>
        /// <param name="pic6"></param>
        /// <param name="pic7"></param>
        /// <param name="pic8"></param>
        /// <param name="pic9"></param>
        public static void ApplyWin10xLegends(Theme.Manager TM, WindowStyle Style, Label lbl1, Label lbl2, Label lbl3, Label lbl4, Label lbl5, Label lbl6, Label lbl7, Label lbl8, Label lbl9, PictureBox pic1, PictureBox pic2, PictureBox pic3, PictureBox pic4, PictureBox pic5, PictureBox pic6, PictureBox pic7, PictureBox pic8, PictureBox pic9)
        {
            // Create new helper for Windows 11 Explorer patcher
            if (ExplorerPatcher.CanBeUsed) Program.EP = new();

            switch (Style)
            {
                case WindowStyle.W11:
                    {
                        lbl6.Text = Program.Lang.Strings.Legends.TM_11_SomePressedButtons;
                        lbl7.Text = string.Format(Program.Lang.Strings.Legends.TM_UWPBackground, Program.Lang.Strings.Windows.W11);
                        lbl8.Text = Program.Lang.Strings.Legends.TM_Undefined;
                        lbl9.Text = Program.Lang.Strings.Legends.TM_Undefined;
                        pic5.Image = Assets.Win10xLegends.Settings_Icons;
                        pic6.Image = Assets.Win10xLegends.PressedButton;
                        pic7.Image = Assets.Win10xLegends.UWPDlg;
                        pic8.Image = Assets.Win10xLegends.Undefined;
                        pic9.Image = Assets.Win10xLegends.Undefined;

                        switch (!TM.Windows11.WinMode_Light)
                        {
                            case true:   // '''''''''DarkMode_App
                                {
                                    lbl1.Text = Program.Lang.Strings.Legends.TM_11_StartMenu_Taskbar_AC;
                                    lbl2.Text = Program.Lang.Strings.Legends.TM_11_ACHover_Links;
                                    lbl3.Text = Program.Lang.Strings.Legends.TM_11_Lines_Toggles_Buttons;
                                    lbl4.Text = Program.Lang.Strings.Legends.TM_11_OverflowTray;
                                    lbl5.Text = Program.Lang.Strings.Legends.TM_11_Settings;

                                    pic1.Image = Assets.Win10xLegends.StartMenu_Taskbar_AC;
                                    pic2.Image = Assets.Win10xLegends.ACHover_Links;
                                    pic3.Image = Assets.Win10xLegends.Lines_Toggles_Buttons;
                                    pic4.Image = Assets.Win10xLegends.Overflow;
                                    break;
                                }
                            case false:   // '''''''''Light
                                {
                                    lbl1.Text = Program.Lang.Strings.Legends.TM_11_Taskbar_ACHover_Links;
                                    lbl2.Text = Program.Lang.Strings.Legends.TM_11_StartMenu_AC;
                                    lbl3.Text = Program.Lang.Strings.Legends.TM_11_UnreadBadge;
                                    lbl4.Text = Program.Lang.Strings.Legends.TM_11_Lines_Toggles_Buttons_Overflow;
                                    lbl5.Text = Program.Lang.Strings.Legends.TM_11_SettingsAndTaskbarAppUnderline;

                                    pic1.Image = Assets.Win10xLegends.Taskbar;
                                    pic2.Image = Assets.Win10xLegends.StartMenu_Taskbar_AC;
                                    pic3.Image = Assets.Win10xLegends.Badge;
                                    pic4.Image = Assets.Win10xLegends.Lines_Toggles_Buttons;
                                    break;
                                }
                        }

                        if (ExplorerPatcher.CanBeUsed)
                        {
                            switch (!TM.Windows11.WinMode_Light)
                            {
                                case true: // '''''''''DarkMode_App
                                    {

                                        if (Program.EP.UseTaskbar10)
                                        {
                                            lbl5.Text = Program.Lang.Strings.Legends.TM_10_Settings_Links_SomeBtns;

                                            if (Program.EP.UseStart10)
                                            {
                                                lbl1.Text = Program.Lang.Strings.Legends.TM_10_Taskbar;
                                                pic1.Image = Assets.Win10xLegends.Taskbar;
                                            }
                                            else
                                            {
                                                lbl1.Text = Program.Lang.Strings.Legends.TM_11_StartMenu_Taskbar_AC;
                                                pic1.Image = Assets.Win10xLegends.StartMenu_Taskbar_AC;
                                            }

                                            lbl3.Text = Program.Lang.Strings.Legends.TM_EP_ACButton_TaskbarAppLine;
                                            lbl6.Text = Program.Lang.Strings.Legends.TM_10_StartMenuIconHover;

                                            pic3.Image = Assets.Win10xLegends.AC;
                                            pic5.Image = Assets.Win10xLegends.Settings_Icons;
                                            pic6.Image = Assets.WinLogos.Win11;
                                        }

                                        if (Program.EP.UseStart10)
                                        {
                                            lbl4.Text = Program.Lang.Strings.Legends.TM_EP_StartMenu_OverflowMenus;
                                            pic4.Image = Assets.Win10xLegends.StartMenu;
                                        }

                                        break;
                                    }

                                case false: // '''''''''Light
                                    {

                                        if (Program.EP.UseTaskbar10)
                                        {
                                            lbl3.Text = Program.Lang.Strings.Legends.TM_EP_Taskbar_AppUnderline;
                                            lbl5.Text = Program.Lang.Strings.Legends.TM_10_Settings_Links_SomeBtns;
                                            lbl6.Text = Program.Lang.Strings.Legends.TM_10_StartMenuIconHover;

                                            pic3.Image = Assets.Win10xLegends.TaskbarApp;
                                            pic5.Image = Assets.Win10xLegends.Settings_Icons;
                                            pic6.Image = Assets.WinLogos.Win11;
                                        }

                                        if (Program.EP.UseStart10)
                                        {
                                            lbl2.Text = Program.Lang.Strings.Legends.TM_EP_ActionCenterBackground;
                                            lbl4.Text = Program.Lang.Strings.Legends.TM_EP_StartMenu_ActionCenterButtons;
                                            pic2.Image = Assets.Win10xLegends.AC;
                                            pic4.Image = Assets.Win10xLegends.StartMenu_Taskbar_AC;
                                        }

                                        break;
                                    }

                            }
                        }

                        break;
                    }

                case WindowStyle.W10:
                    {
                        lbl9.Text = Program.Lang.Strings.Legends.TM_Undefined;

                        switch (!TM.Windows10.WinMode_Light)
                        {
                            case true: // '''''''''DarkMode_App
                                {
                                    lbl2.Text = Program.Lang.Strings.Legends.TM_10_ACLinks;
                                    lbl3.Text = Program.Lang.Strings.Legends.TM_10_TaskbarAppUnderline;
                                    lbl5.Text = Program.Lang.Strings.Legends.TM_10_Settings_Links_SomeBtns;
                                    lbl6.Text = Program.Lang.Strings.Legends.TM_10_StartMenuIconHover;
                                    lbl7.Text = string.Format(Program.Lang.Strings.Legends.TM_UWPBackground, Program.Lang.Strings.Windows.W10);

                                    pic2.Image = Assets.Win10xLegends.ACHover_Links;
                                    pic3.Image = Assets.Win10xLegends.TaskbarApp;
                                    pic5.Image = Assets.Win10xLegends.Settings_Icons;
                                    pic6.Image = Assets.WinLogos.Win10;
                                    pic7.Image = Assets.Win10xLegends.UWPDlg;

                                    if (TM.Windows10.Transparency)
                                    {
                                        lbl1.Text = Program.Lang.Strings.Legends.TM_10_Hamburger;
                                        lbl4.Text = Program.Lang.Strings.Legends.TM_10_StartMenu_AC;
                                        lbl8.Text = Program.Lang.Strings.Legends.TM_10_Taskbar_StartContextMenu;

                                        pic1.Image = Assets.Win10xLegends.Hamburger;
                                        pic4.Image = Assets.Win10xLegends.StartMenu_Taskbar_AC;
                                        pic8.Image = Assets.Win10xLegends.Taskbar;

                                        if (TM.Windows10.ApplyAccentOnTaskbar != Theme.Structures.Windows10x.AccentTaskbarLevels.None)
                                        {
                                            lbl5.Text = Program.Lang.Strings.Legends.TM_10_Settings_Links_Taskbar_SomeBtns;
                                        }
                                    }

                                    else
                                    {
                                        lbl1.Text = Program.Lang.Strings.Legends.TM_10_Taskbar;
                                        pic1.Image = Assets.Win10xLegends.Taskbar;
                                        pic4.Image = Assets.Win10xLegends.StartMenu_Taskbar_AC;

                                        if (TM.Windows10.ApplyAccentOnTaskbar != Theme.Structures.Windows10x.AccentTaskbarLevels.None)
                                        {
                                            lbl4.Text = Program.Lang.Strings.Legends.TM_10_StartMenu_AC_TaskbarActiveApp;
                                        }
                                        else
                                        {
                                            lbl4.Text = Program.Lang.Strings.Legends.TM_10_StartMenu_AC;
                                        }

                                        lbl8.Text = Program.Lang.Strings.Legends.TM_10_StartContextMenu;
                                        pic8.Image = Assets.Win10xLegends.StartContextMenu;

                                    }

                                    break;
                                }

                            case false: // '''''''''Light
                                {
                                    if (TM.Windows10.Transparency)
                                    {
                                        lbl1.Text = Program.Lang.Strings.Legends.TM_10_Hamburger;
                                        lbl4.Text = Program.Lang.Strings.Legends.TM_10_StartMenu_AC;
                                        lbl6.Text = Program.Lang.Strings.Legends.TM_10_StartMenuIconHover;
                                        lbl7.Text = string.Format(Program.Lang.Strings.Legends.TM_UWPBackground, Program.Lang.Strings.Windows.W10);

                                        pic1.Image = Assets.Win10xLegends.Hamburger;
                                        pic4.Image = Assets.Win10xLegends.StartMenu_Taskbar_AC;
                                        pic5.Image = Assets.Win10xLegends.Settings_Icons;
                                        pic6.Image = Assets.WinLogos.Win10;
                                        pic7.Image = Assets.Win10xLegends.UWPDlg;
                                        pic8.Image = Assets.Win10xLegends.Taskbar;

                                        if (TM.Windows10.ApplyAccentOnTaskbar == Theme.Structures.Windows10x.AccentTaskbarLevels.None)
                                        {
                                            lbl2.Text = Program.Lang.Strings.Legends.TM_Undefined;
                                            lbl3.Text = Program.Lang.Strings.Legends.TM_Undefined;
                                            lbl5.Text = Program.Lang.Strings.Legends.TM_10_Settings_Links_TaskbarUndeline_SomeBtns;
                                            lbl8.Text = Program.Lang.Strings.Legends.TM_10_Taskbar_ACLinks_StartContextMenu;

                                            pic2.Image = Assets.Win10xLegends.Undefined;
                                            pic3.Image = Assets.Win10xLegends.Undefined;
                                        }

                                        else if (TM.Windows10.ApplyAccentOnTaskbar == Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar)
                                        {
                                            lbl2.Text = Program.Lang.Strings.Legends.TM_Undefined;
                                            lbl3.Text = Program.Lang.Strings.Legends.TM_10_TaskbarAppUnderline;
                                            lbl5.Text = Program.Lang.Strings.Legends.TM_10_Settings_Links_SomeBtns;
                                            lbl8.Text = Program.Lang.Strings.Legends.TM_10_Taskbar_ACLinks_StartContextMenu;

                                            pic2.Image = Assets.Win10xLegends.Undefined;
                                            pic3.Image = Assets.Win10xLegends.TaskbarApp;
                                        }

                                        else
                                        {
                                            lbl2.Text = Program.Lang.Strings.Legends.TM_10_ACLinks;
                                            lbl3.Text = Program.Lang.Strings.Legends.TM_10_TaskbarAppUnderline;
                                            lbl5.Text = Program.Lang.Strings.Legends.TM_10_Settings_Links_SomeBtns;
                                            lbl8.Text = Program.Lang.Strings.Legends.TM_10_Taskbar_StartContextMenu;

                                            pic2.Image = Assets.Win10xLegends.ACHover_Links;
                                            pic3.Image = Assets.Win10xLegends.TaskbarApp;

                                        }
                                    }
                                    else
                                    {
                                        lbl1.Text = Program.Lang.Strings.Legends.TM_10_Taskbar;
                                        lbl6.Text = Program.Lang.Strings.Legends.TM_10_StartMenuIconHover;
                                        lbl7.Text = string.Format(Program.Lang.Strings.Legends.TM_UWPBackground, Program.Lang.Strings.Windows.W10);

                                        pic1.Image = Assets.Win10xLegends.Taskbar;
                                        pic6.Image = Assets.WinLogos.Win10;
                                        pic7.Image = Assets.Win10xLegends.UWPDlg;

                                        if (TM.Windows10.ApplyAccentOnTaskbar == Theme.Structures.Windows10x.AccentTaskbarLevels.None)
                                        {
                                            lbl2.Text = Program.Lang.Strings.Legends.TM_Undefined;
                                            lbl3.Text = Program.Lang.Strings.Legends.TM_Undefined;
                                            lbl4.Text = Program.Lang.Strings.Legends.TM_10_StartMenu_AC;
                                            lbl5.Text = Program.Lang.Strings.Legends.TM_10_Settings_Links_TaskbarUndeline_SomeBtns;
                                            lbl8.Text = Program.Lang.Strings.Legends.TM_10_ACLinks_StartContextMenu;

                                            pic2.Image = Assets.Win10xLegends.Undefined;
                                            pic3.Image = Assets.Win10xLegends.Undefined;
                                            pic4.Image = Assets.Win10xLegends.StartMenu_Taskbar_AC;
                                            pic5.Image = Assets.Win10xLegends.Settings_Icons;
                                            pic8.Image = Assets.Win10xLegends.ACHover_Links;
                                        }

                                        else if (TM.Windows10.ApplyAccentOnTaskbar == Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar)
                                        {
                                            lbl2.Text = Program.Lang.Strings.Legends.TM_Undefined;
                                            lbl3.Text = Program.Lang.Strings.Legends.TM_10_TaskbarAppUnderline;
                                            lbl4.Text = Program.Lang.Strings.Legends.TM_10_TaskbarFocusedApp_StartButtonHover;
                                            lbl5.Text = Program.Lang.Strings.Legends.TM_10_Settings_Links_SomeBtns;
                                            lbl8.Text = Program.Lang.Strings.Legends.TM_10_ACLinks_StartContextMenu;

                                            pic2.Image = Assets.Win10xLegends.Undefined;
                                            pic3.Image = Assets.Win10xLegends.TaskbarApp;
                                            pic4.Image = Assets.Win10xLegends.TaskbarActiveIcon;
                                            pic5.Image = Assets.Win10xLegends.Settings_Icons;
                                            pic8.Image = Assets.Win10xLegends.ACHover_Links;
                                        }

                                        else
                                        {
                                            lbl2.Text = Program.Lang.Strings.Legends.TM_10_ACLinks;
                                            lbl3.Text = Program.Lang.Strings.Legends.TM_10_TaskbarAppUnderline;
                                            lbl4.Text = Program.Lang.Strings.Legends.TM_10_StartMenu_AC_TaskbarActiveApp;
                                            lbl5.Text = Program.Lang.Strings.Legends.TM_10_Settings_Links_SomeBtns;
                                            lbl8.Text = Program.Lang.Strings.Legends.TM_10_StartContextMenu;

                                            pic2.Image = Assets.Win10xLegends.ACHover_Links;
                                            pic3.Image = Assets.Win10xLegends.TaskbarApp;
                                            pic4.Image = Assets.Win10xLegends.StartMenu_Taskbar_AC;
                                            pic5.Image = Assets.Win10xLegends.Settings_Icons;
                                            pic8.Image = Assets.Win10xLegends.StartContextMenu;
                                        }
                                    }

                                    break;
                                }
                        }

                        break;
                    }
            }
        }

        /// <summary>
        /// Sets metrics for a classic window in preview
        /// </summary>
        /// <param name="TM">WinPaletter theme manager</param>
        /// <param name="Window">Classic window simulation control</param>
        public static void SetClassicWindowMetrics(Theme.Manager TM, UI.Retro.WindowR Window)
        {
            if (TM is not null)
            {
                Window.Metrics_BorderWidth = TM.MetricsFonts.BorderWidth;
                Window.Metrics_CaptionHeight = TM.MetricsFonts.CaptionHeight;
                Window.Metrics_CaptionWidth = TM.MetricsFonts.CaptionWidth;
                Window.Metrics_PaddedBorderWidth = TM.MetricsFonts.PaddedBorderWidth;
                Window.Font = TM.MetricsFonts.CaptionFont;
                Window.Refresh();
            }
        }

        /// <summary>
        /// Sets metrics for a window in preview
        /// </summary>
        /// <param name="TM">WinPaletter theme manager</param>
        /// <param name="Window">Window simulation control</param>
        public static void SetModernWindowMetrics(Theme.Manager TM, Window Window)
        {
            if (TM is not null)
            {
                Window.Font = TM.MetricsFonts.CaptionFont;
                Window.Metrics_BorderWidth = TM.MetricsFonts.BorderWidth;
                Window.Metrics_CaptionHeight = TM.MetricsFonts.CaptionHeight;
                Window.Metrics_PaddedBorderWidth = TM.MetricsFonts.PaddedBorderWidth;
                Window.Invalidate();
            }
        }

        /// <summary>
        /// Sets colors for a classic window in preview
        /// </summary>
        /// <param name="TM">WinPaletter theme manager</param>
        /// <param name="Window">Classic window simulation control</param>
        /// <param name="Active">Making Window control active or not</param>
        public static void SetClassicWindowColors(Theme.Manager TM, UI.Retro.WindowR Window, bool Active = true)
        {
            if (TM is not null)
            {
                Window.ButtonDkShadow = TM.Win32.ButtonDkShadow;
                Window.BackColor = TM.Win32.ButtonFace;
                Window.ButtonHilight = TM.Win32.ButtonHilight;
                Window.ButtonLight = TM.Win32.ButtonLight;
                Window.ButtonShadow = TM.Win32.ButtonShadow;
                Window.ButtonText = TM.Win32.ButtonText;

                if (Active)
                {
                    Window.ColorBorder = TM.Win32.ActiveBorder;
                    Window.ForeColor = TM.Win32.TitleText;
                    Window.Color1 = TM.Win32.ActiveTitle;
                    Window.Color2 = TM.Win32.GradientActiveTitle;
                }
                else
                {
                    Window.ColorBorder = TM.Win32.InactiveBorder;
                    Window.ForeColor = TM.Win32.InactiveTitleText;
                    Window.Color1 = TM.Win32.InactiveTitle;
                    Window.Color2 = TM.Win32.GradientInactiveTitle;
                }

                Window.ColorGradient = TM.Win32.EnableGradient;
            }
        }

        /// <summary>
        /// Sets colors for a classic raised panel in preview
        /// </summary>
        /// <param name="TM">WinPaletter theme manager</param>
        /// <param name="Panel">PanelRaisedR classic control</param>
        public static void SetClassicPanelRaisedRColors(Theme.Manager TM, UI.Retro.PanelRaisedR Panel)
        {
            Panel.BackColor = TM.Win32.ButtonFace;
            Panel.ButtonHilight = TM.Win32.ButtonHilight;
            Panel.ButtonLight = TM.Win32.ButtonLight;
            Panel.ButtonShadow = TM.Win32.ButtonShadow;
            Panel.ButtonDkShadow = TM.Win32.ButtonDkShadow;
            Panel.ForeColor = TM.Win32.TitleText;
        }

        /// <summary>
        /// Sets colors for a classic raised panel in preview
        /// </summary>
        /// <param name="TM">WinPaletter theme manager</param>
        /// <param name="Panel">PanelR classic control</param>
        public static void SetClassicPanelColors(Theme.Manager TM, UI.Retro.PanelR Panel)
        {
            Panel.BackColor = TM.Win32.ButtonFace;
            Panel.ButtonHilight = TM.Win32.ButtonHilight;
            Panel.ButtonLight = TM.Win32.ButtonLight;
            Panel.ButtonShadow = TM.Win32.ButtonShadow;
            Panel.ButtonDkShadow = TM.Win32.ButtonDkShadow;
            Panel.ForeColor = TM.Win32.TitleText;
        }

        /// <summary>
        /// Sets colors for a classic button in preview
        /// </summary>
        /// <param name="TM">WinPaletter theme manager</param>
        /// <param name="Button">ButtonR classic control</param>
        public static void SetClassicButtonColors(Theme.Manager TM, UI.Retro.ButtonR Button)
        {
            Button.ButtonDkShadow = TM.Win32.ButtonDkShadow;
            Button.ButtonHilight = TM.Win32.ButtonHilight;
            Button.ButtonLight = TM.Win32.ButtonLight;
            Button.ButtonShadow = TM.Win32.ButtonShadow;
            Button.BackColor = TM.Win32.ButtonFace;
            Button.ForeColor = TM.Win32.ButtonText;
            Button.WindowFrame = TM.Win32.WindowFrame;
            Button.FocusRectWidth = (int)TM.WindowsEffects.FocusRectWidth;
            Button.FocusRectHeight = (int)TM.WindowsEffects.FocusRectHeight;
        }

        /// <summary>
        /// Get modified wallpaper (Wallpaper Tone)
        /// </summary>
        /// <param name="WT">Wallpaper tone structure inside WinPaletter theme manager</param>
        /// <returns>Bitmap</returns>
        public static Bitmap GetTintedWallpaper(Theme.Structures.WallpaperTone WT)
        {
            if (!System.IO.File.Exists(WT.Image))
            {
                if (OS.WXP)
                {
                    WT.Image = $@"{SysPaths.Windows}\Web\Wallpaper\Bliss.bmp";
                }
                else
                {
                    WT.Image = $@"{SysPaths.Windows}\Web\Wallpaper\Windows\img0.jpg";
                }
            }

            using (ImageProcessor.ImageFactory ImgF = new())
            {
                ImgF.Load(WT.Image);
                ImgF.Hue(WT.H, true);
                ImgF.Saturation(WT.S - 100);
                ImgF.Brightness(WT.L - 100);

                return (Bitmap)ImgF.Image.Clone();
            }
        }

        /// <summary>
        /// Return correct height of a titlebar provided by a font
        /// </summary>
        public static int GetTitlebarTextHeight(Font font)
        {
            // Title scheme for measuring
            string TitleScheme = "ABCabc0123xYz.#";
            int Title_x_Height = (int)Math.Round(TitleScheme.Measure(font).Height);
            int Title_9_Height = (int)Math.Round(TitleScheme.Measure(new Font(font.Name, 9f, font.Style)).Height);

            // Return correct height of a titlebar
            return Math.Max(0, Title_x_Height - Title_9_Height - 5);
        }
    }
}