using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace WinPaletter
{

    /// <summary>
    /// Helpers for WinPaletter theme preview
    /// </summary>
    public class PreviewHelpers
    {
        private static readonly int Steps = 15;
        private static readonly int Delay = 1;

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
            /// <summary>Windows 7</summary>
            W7,
            /// <summary>Windows Vista</summary>
            WVista,
            /// <summary>Windows XP</summary>
            WXP
        }

        /// <summary>
        /// Change labels of Windows 11/10 in WinPaletter main form according to preview
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
            if (ExplorerPatcher.IsAllowed())
                Program.EP = new ExplorerPatcher();

            switch (Style)
            {
                case WindowStyle.W11:
                    {
                        lbl6.Text = Program.Lang.TM_11_SomePressedButtons;
                        lbl7.Text = string.Format(Program.Lang.TM_UWPBackground, Program.Lang.OS_Win11);
                        lbl8.Text = Program.Lang.TM_Undefined;
                        lbl9.Text = Program.Lang.TM_Undefined;
                        pic5.Image = Properties.Resources.Mini_Settings_Icons;
                        pic6.Image = Properties.Resources.Mini_PressedButton;
                        pic7.Image = Properties.Resources.Mini_UWPDlg;
                        pic8.Image = Properties.Resources.Mini_Undefined;
                        pic9.Image = Properties.Resources.Mini_Undefined;

                        switch (!TM.Windows11.WinMode_Light)
                        {
                            case true:   // '''''''''DarkMode
                                {
                                    lbl1.Text = Program.Lang.TM_11_StartMenu_Taskbar_AC;
                                    lbl2.Text = Program.Lang.TM_11_ACHover_Links;
                                    lbl3.Text = Program.Lang.TM_11_Lines_Toggles_Buttons;
                                    lbl4.Text = Program.Lang.TM_11_OverflowTray;
                                    lbl5.Text = Program.Lang.TM_11_Settings;

                                    pic1.Image = Properties.Resources.Mini_StartMenu_Taskbar_AC;
                                    pic2.Image = Properties.Resources.Mini_ACHover_Links;
                                    pic3.Image = Properties.Resources.Mini_Lines_Toggles_Buttons;
                                    pic4.Image = Properties.Resources.Mini_Overflow;
                                    break;
                                }
                            case false:   // '''''''''Light
                                {
                                    lbl1.Text = Program.Lang.TM_11_Taskbar_ACHover_Links;
                                    lbl2.Text = Program.Lang.TM_11_StartMenu_AC;
                                    lbl3.Text = Program.Lang.TM_11_UnreadBadge;
                                    lbl4.Text = Program.Lang.TM_11_Lines_Toggles_Buttons_Overflow;
                                    lbl5.Text = Program.Lang.TM_11_SettingsAndTaskbarAppUnderline;

                                    pic1.Image = Properties.Resources.Mini_Taskbar;
                                    pic2.Image = Properties.Resources.Mini_StartMenu_Taskbar_AC;
                                    pic3.Image = Properties.Resources.Mini_Badge;
                                    pic4.Image = Properties.Resources.Mini_Lines_Toggles_Buttons;
                                    break;
                                }
                        }

                        if (ExplorerPatcher.IsAllowed())
                        {
                            switch (!TM.Windows11.WinMode_Light)
                            {
                                case true: // '''''''''DarkMode
                                    {

                                        if (Program.EP.UseTaskbar10)
                                        {
                                            lbl5.Text = Program.Lang.TM_10_Settings_Links_SomeBtns;

                                            if (Program.EP.UseStart10)
                                            {
                                                lbl1.Text = Program.Lang.TM_10_Taskbar;
                                                pic1.Image = Properties.Resources.Mini_Taskbar;
                                            }
                                            else
                                            {
                                                lbl1.Text = Program.Lang.TM_11_StartMenu_Taskbar_AC;
                                                pic1.Image = Properties.Resources.Mini_StartMenu_Taskbar_AC;
                                            }

                                            lbl3.Text = Program.Lang.TM_EP_ACButton_TaskbarAppLine;
                                            lbl6.Text = Program.Lang.TM_10_StartMenuIconHover;

                                            pic3.Image = Properties.Resources.Mini_AC;
                                            pic5.Image = Properties.Resources.Mini_Settings_Icons;
                                            pic6.Image = Properties.Resources.Native11;
                                        }

                                        if (Program.EP.UseStart10)
                                        {
                                            lbl4.Text = Program.Lang.TM_EP_StartMenu_OverflowMenus;
                                            pic4.Image = Properties.Resources.Mini_StartMenu;
                                        }

                                        break;
                                    }

                                case false: // '''''''''Light
                                    {

                                        if (Program.EP.UseTaskbar10)
                                        {
                                            lbl3.Text = Program.Lang.TM_EP_Taskbar_AppUnderline;
                                            lbl5.Text = Program.Lang.TM_10_Settings_Links_SomeBtns;
                                            lbl6.Text = Program.Lang.TM_10_StartMenuIconHover;

                                            pic3.Image = Properties.Resources.Mini_TaskbarApp;
                                            pic5.Image = Properties.Resources.Mini_Settings_Icons;
                                            pic6.Image = Properties.Resources.Native11;
                                        }

                                        if (Program.EP.UseStart10)
                                        {
                                            lbl2.Text = Program.Lang.TM_EP_ActionCenterBackground;
                                            lbl4.Text = Program.Lang.TM_EP_StartMenu_ActionCenterButtons;
                                            pic2.Image = Properties.Resources.Mini_AC;
                                            pic4.Image = Properties.Resources.Mini_StartMenu_Taskbar_AC;
                                        }

                                        break;
                                    }

                            }
                        }

                        break;
                    }

                case WindowStyle.W10:
                    {
                        lbl9.Text = Program.Lang.TM_Undefined;

                        switch (!TM.Windows10.WinMode_Light)
                        {
                            case true: // '''''''''DarkMode
                                {
                                    lbl2.Text = Program.Lang.TM_10_ACLinks;
                                    lbl3.Text = Program.Lang.TM_10_TaskbarAppUnderline;
                                    lbl5.Text = Program.Lang.TM_10_Settings_Links_SomeBtns;
                                    lbl6.Text = Program.Lang.TM_10_StartMenuIconHover;
                                    lbl7.Text = string.Format(Program.Lang.TM_UWPBackground, Program.Lang.OS_Win10);

                                    pic2.Image = Properties.Resources.Mini_ACHover_Links;
                                    pic3.Image = Properties.Resources.Mini_TaskbarApp;
                                    pic5.Image = Properties.Resources.Mini_Settings_Icons;
                                    pic6.Image = Properties.Resources.Native10;
                                    pic7.Image = Properties.Resources.Mini_UWPDlg;

                                    if (TM.Windows10.Transparency)
                                    {
                                        lbl1.Text = Program.Lang.TM_10_Hamburger;
                                        lbl4.Text = Program.Lang.TM_10_StartMenu_AC;
                                        lbl8.Text = Program.Lang.TM_10_Taskbar_StartContextMenu;

                                        pic1.Image = Properties.Resources.Mini_Hamburger;
                                        pic4.Image = Properties.Resources.Mini_StartMenu_Taskbar_AC;
                                        pic8.Image = Properties.Resources.Mini_Taskbar;

                                        if (TM.Windows10.ApplyAccentOnTaskbar != Theme.Structures.Windows10x.AccentTaskbarLevels.None)
                                        {
                                            lbl5.Text = Program.Lang.TM_10_Settings_Links_Taskbar_SomeBtns;
                                        }
                                    }

                                    else
                                    {
                                        lbl1.Text = Program.Lang.TM_10_Taskbar;
                                        pic1.Image = Properties.Resources.Mini_Taskbar;
                                        pic4.Image = Properties.Resources.Mini_StartMenu_Taskbar_AC;

                                        if (TM.Windows10.ApplyAccentOnTaskbar != Theme.Structures.Windows10x.AccentTaskbarLevels.None)
                                        {
                                            lbl4.Text = Program.Lang.TM_10_StartMenu_AC_TaskbarActiveApp;
                                        }
                                        else
                                        {
                                            lbl4.Text = Program.Lang.TM_10_StartMenu_AC;
                                        }

                                        lbl8.Text = Program.Lang.TM_10_StartContextMenu;
                                        pic8.Image = Properties.Resources.Mini_StartContextMenu;

                                    }

                                    break;
                                }

                            case false: // '''''''''Light
                                {
                                    if (TM.Windows10.Transparency)
                                    {
                                        lbl1.Text = Program.Lang.TM_10_Hamburger;
                                        lbl4.Text = Program.Lang.TM_10_StartMenu_AC;
                                        lbl6.Text = Program.Lang.TM_10_StartMenuIconHover;
                                        lbl7.Text = string.Format(Program.Lang.TM_UWPBackground, Program.Lang.OS_Win10);

                                        pic1.Image = Properties.Resources.Mini_Hamburger;
                                        pic4.Image = Properties.Resources.Mini_StartMenu_Taskbar_AC;
                                        pic5.Image = Properties.Resources.Mini_Settings_Icons;
                                        pic6.Image = Properties.Resources.Native10;
                                        pic7.Image = Properties.Resources.Mini_UWPDlg;
                                        pic8.Image = Properties.Resources.Mini_Taskbar;

                                        if (TM.Windows10.ApplyAccentOnTaskbar == Theme.Structures.Windows10x.AccentTaskbarLevels.None)
                                        {
                                            lbl2.Text = Program.Lang.TM_Undefined;
                                            lbl3.Text = Program.Lang.TM_Undefined;
                                            lbl5.Text = Program.Lang.TM_10_Settings_Links_TaskbarUndeline_SomeBtns;
                                            lbl8.Text = Program.Lang.TM_10_Taskbar_ACLinks_StartContextMenu;

                                            pic2.Image = Properties.Resources.Mini_Undefined;
                                            pic3.Image = Properties.Resources.Mini_Undefined;
                                        }

                                        else if (TM.Windows10.ApplyAccentOnTaskbar == Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar)
                                        {
                                            lbl2.Text = Program.Lang.TM_Undefined;
                                            lbl3.Text = Program.Lang.TM_10_TaskbarAppUnderline;
                                            lbl5.Text = Program.Lang.TM_10_Settings_Links_SomeBtns;
                                            lbl8.Text = Program.Lang.TM_10_Taskbar_ACLinks_StartContextMenu;

                                            pic2.Image = Properties.Resources.Mini_Undefined;
                                            pic3.Image = Properties.Resources.Mini_TaskbarApp;
                                        }

                                        else
                                        {
                                            lbl2.Text = Program.Lang.TM_10_ACLinks;
                                            lbl3.Text = Program.Lang.TM_10_TaskbarAppUnderline;
                                            lbl5.Text = Program.Lang.TM_10_Settings_Links_SomeBtns;
                                            lbl8.Text = Program.Lang.TM_10_Taskbar_StartContextMenu;

                                            pic2.Image = Properties.Resources.Mini_ACHover_Links;
                                            pic3.Image = Properties.Resources.Mini_TaskbarApp;

                                        }
                                    }
                                    else
                                    {
                                        lbl1.Text = Program.Lang.TM_10_Taskbar;
                                        lbl6.Text = Program.Lang.TM_10_StartMenuIconHover;
                                        lbl7.Text = string.Format(Program.Lang.TM_UWPBackground, Program.Lang.OS_Win10);

                                        pic1.Image = Properties.Resources.Mini_Taskbar;
                                        pic6.Image = Properties.Resources.Native10;
                                        pic7.Image = Properties.Resources.Mini_UWPDlg;

                                        if (TM.Windows10.ApplyAccentOnTaskbar == Theme.Structures.Windows10x.AccentTaskbarLevels.None)
                                        {
                                            lbl2.Text = Program.Lang.TM_Undefined;
                                            lbl3.Text = Program.Lang.TM_Undefined;
                                            lbl4.Text = Program.Lang.TM_10_StartMenu_AC;
                                            lbl5.Text = Program.Lang.TM_10_Settings_Links_TaskbarUndeline_SomeBtns;
                                            lbl8.Text = Program.Lang.TM_10_ACLinks_StartContextMenu;

                                            pic2.Image = Properties.Resources.Mini_Undefined;
                                            pic3.Image = Properties.Resources.Mini_Undefined;
                                            pic4.Image = Properties.Resources.Mini_StartMenu_Taskbar_AC;
                                            pic5.Image = Properties.Resources.Mini_Settings_Icons;
                                            pic8.Image = Properties.Resources.Mini_ACHover_Links;
                                        }

                                        else if (TM.Windows10.ApplyAccentOnTaskbar == Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar)
                                        {
                                            lbl2.Text = Program.Lang.TM_Undefined;
                                            lbl3.Text = Program.Lang.TM_10_TaskbarAppUnderline;
                                            lbl4.Text = Program.Lang.TM_10_TaskbarFocusedApp_StartButtonHover;
                                            lbl5.Text = Program.Lang.TM_10_Settings_Links_SomeBtns;
                                            lbl8.Text = Program.Lang.TM_10_ACLinks_StartContextMenu;

                                            pic2.Image = Properties.Resources.Mini_Undefined;
                                            pic3.Image = Properties.Resources.Mini_TaskbarApp;
                                            pic4.Image = Properties.Resources.Mini_TaskbarActiveIcon;
                                            pic5.Image = Properties.Resources.Mini_Settings_Icons;
                                            pic8.Image = Properties.Resources.Mini_ACHover_Links;
                                        }

                                        else
                                        {
                                            lbl2.Text = Program.Lang.TM_10_ACLinks;
                                            lbl3.Text = Program.Lang.TM_10_TaskbarAppUnderline;
                                            lbl4.Text = Program.Lang.TM_10_StartMenu_AC_TaskbarActiveApp;
                                            lbl5.Text = Program.Lang.TM_10_Settings_Links_SomeBtns;
                                            lbl8.Text = Program.Lang.TM_10_StartContextMenu;

                                            pic2.Image = Properties.Resources.Mini_ACHover_Links;
                                            pic3.Image = Properties.Resources.Mini_TaskbarApp;
                                            pic4.Image = Properties.Resources.Mini_StartMenu_Taskbar_AC;
                                            pic5.Image = Properties.Resources.Mini_Settings_Icons;
                                            pic8.Image = Properties.Resources.Mini_StartContextMenu;
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
        /// Apply colors into taskbar, start and action center in preview
        /// </summary>
        /// <param name="TM">WinPaletter theme manager</param>
        /// <param name="Style">Selected Windows edition</param>
        /// <param name="AnimateColorChange"></param>
        /// <param name="Taskbar"></param>
        /// <param name="Start"></param>
        /// <param name="ActionCenter"></param>
        /// <param name="setting_icon_preview"></param>
        /// <param name="settings_label"></param>
        /// <param name="Link_preview"></param>
        public static void ApplyWinElementsColors(Theme.Manager TM, WindowStyle Style, bool AnimateColorChange, UI.Simulation.WinElement Taskbar, UI.Simulation.WinElement Start, UI.Simulation.WinElement ActionCenter, UI.WP.LabelAlt setting_icon_preview, UI.WP.LabelAlt settings_label, UI.WP.LabelAlt Link_preview)
        {
            if (ExplorerPatcher.IsAllowed())
                Program.EP = new ExplorerPatcher();

            Config.RenderingHint = TM.MetricsFonts.Fonts_SingleBitPP ? TextRenderingHint.SingleBitPerPixelGridFit : TextRenderingHint.ClearTypeGridFit;

            Taskbar.SuspendRefresh = true;
            Start.SuspendRefresh = true;
            ActionCenter.SuspendRefresh = true;

            switch (Style)
            {
                case WindowStyle.W11:
                    {
                        #region Win11
                        byte TB_Alpha = default, S_Alpha = default, AC_Alpha = default;
                        var TB_Blur = default(byte);
                        Color TB_Color = default, S_Color = default, AC_Color = default;
                        var TB_UL_Color = default(Color);
                        Color Settings_Label_Color = default, Link_preview_Color = default;
                        Color AC_Normal = default, AC_Hover = default, AC_Pressed = default;

                        Start.DarkMode = !TM.Windows11.WinMode_Light;
                        Taskbar.DarkMode = !TM.Windows11.WinMode_Light;
                        ActionCenter.DarkMode = !TM.Windows11.WinMode_Light;
                        Taskbar.Transparency = TM.Windows11.Transparency;
                        Start.Transparency = TM.Windows11.Transparency;
                        ActionCenter.Transparency = TM.Windows11.Transparency;

                        switch (!TM.Windows11.WinMode_Light)
                        {
                            case true:   // DarkMode
                                {
                                    AC_Alpha = 90;

                                    if (ExplorerPatcher.IsAllowed())
                                    {
                                        if (Program.EP.UseStart10)
                                        {
                                            S_Alpha = 185;
                                        }
                                        else
                                        {
                                            S_Alpha = 90;
                                        }

                                        if (Program.EP.UseTaskbar10)
                                        {
                                            TB_Alpha = 185;
                                            TB_Blur = 8;
                                        }
                                        else
                                        {
                                            TB_Alpha = 105;
                                            TB_Blur = 8;
                                        }
                                    }
                                    else
                                    {
                                        TB_Alpha = 105;
                                        TB_Blur = 8;
                                        S_Alpha = 90;
                                    }

                                    switch (TM.Windows11.ApplyAccentOnTaskbar)
                                    {
                                        case Theme.Structures.Windows10x.AccentTaskbarLevels.None:
                                            {
                                                TB_Color = Color.FromArgb(28, 28, 28);
                                                S_Color = Color.FromArgb(28, 28, 28);
                                                AC_Color = Color.FromArgb(28, 28, 28);
                                                break;
                                            }

                                        case Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar:
                                            {
                                                TB_Color = Color.FromArgb(Taskbar.Background.A, TM.Windows11.Color_Index5);
                                                S_Color = Color.FromArgb(28, 28, 28);
                                                AC_Color = Color.FromArgb(28, 28, 28);
                                                break;
                                            }

                                        case Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC:
                                            {
                                                TB_Color = Color.FromArgb(Taskbar.Background.A, TM.Windows11.Color_Index5);

                                                if (ExplorerPatcher.IsAllowed() & Program.EP.UseStart10)
                                                {
                                                    S_Color = Color.FromArgb(Start.Background.A, TM.Windows11.Color_Index4);
                                                }
                                                else
                                                {
                                                    S_Color = Color.FromArgb(Start.Background.A, TM.Windows11.Color_Index5);
                                                }

                                                AC_Color = Color.FromArgb(ActionCenter.Background.A, TM.Windows11.Color_Index5);
                                                break;
                                            }

                                    }

                                    AC_Normal = TM.Windows11.Color_Index1;
                                    AC_Hover = TM.Windows11.Color_Index0;
                                    AC_Pressed = TM.Windows11.Color_Index2;
                                    TB_UL_Color = TM.Windows11.Color_Index1;
                                    Settings_Label_Color = TM.Windows11.Color_Index3;
                                    Link_preview_Color = TM.Windows11.Color_Index0;
                                    break;
                                }

                            case false:   // Light
                                {
                                    AC_Alpha = 180;

                                    if (ExplorerPatcher.IsAllowed())
                                    {
                                        if (Program.EP.UseStart10)
                                        {
                                            S_Alpha = 210;
                                        }
                                        else
                                        {
                                            S_Alpha = 180;
                                        }

                                        if (Program.EP.UseTaskbar10)
                                        {
                                            TB_Alpha = 210;
                                            TB_Blur = 8;
                                        }
                                        else
                                        {
                                            TB_Alpha = 180;
                                            TB_Blur = 8;
                                        }
                                    }
                                    else
                                    {
                                        TB_Blur = 8;
                                        TB_Alpha = 180;
                                        S_Alpha = 180;
                                    }

                                    switch (TM.Windows11.ApplyAccentOnTaskbar)
                                    {
                                        case Theme.Structures.Windows10x.AccentTaskbarLevels.None:
                                            {
                                                TB_Color = Color.FromArgb(255, 255, 255);
                                                S_Color = Color.FromArgb(255, 255, 255);
                                                AC_Color = Color.FromArgb(255, 255, 255);
                                                break;
                                            }

                                        case Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar:
                                            {
                                                TB_Color = Color.FromArgb(Taskbar.Background.A, TM.Windows11.Color_Index5);
                                                S_Color = Color.FromArgb(255, 255, 255);
                                                AC_Color = Color.FromArgb(255, 255, 255);
                                                break;
                                            }

                                        case Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC:
                                            {
                                                TB_Color = Color.FromArgb(Taskbar.Background.A, TM.Windows11.Color_Index5);

                                                if (ExplorerPatcher.IsAllowed() & Program.EP.UseStart10)
                                                {
                                                    S_Color = Color.FromArgb(Start.Background.A, TM.Windows11.Color_Index4);
                                                }
                                                else
                                                {
                                                    S_Color = Color.FromArgb(Start.Background.A, TM.Windows11.Color_Index0);
                                                }

                                                AC_Color = Color.FromArgb(ActionCenter.Background.A, TM.Windows11.Color_Index0);
                                                break;
                                            }

                                    }

                                    AC_Normal = TM.Windows11.Color_Index4;
                                    AC_Hover = TM.Windows11.Color_Index5;
                                    AC_Pressed = TM.Windows11.Color_Index2;

                                    if (ExplorerPatcher.IsAllowed() & Program.EP.UseTaskbar10)
                                    {
                                        TB_UL_Color = TM.Windows11.Color_Index1;
                                    }
                                    else
                                    {
                                        TB_UL_Color = TM.Windows11.Color_Index3;
                                    }

                                    Settings_Label_Color = TM.Windows11.Color_Index3;
                                    Link_preview_Color = TM.Windows11.Color_Index5;
                                    break;
                                }
                        }

                        ActionCenter.BackColorAlpha = AC_Alpha;
                        Start.BackColorAlpha = S_Alpha;
                        Taskbar.BackColorAlpha = TB_Alpha;
                        Taskbar.BlurPower = TB_Blur;

                        if (AnimateColorChange)
                        {
                            FluentTransitions.Transition.With(Taskbar, nameof(Taskbar.Background), TB_Color).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                            FluentTransitions.Transition.With(Start, nameof(Start.Background), S_Color).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                            FluentTransitions.Transition.With(ActionCenter, nameof(ActionCenter.Background), AC_Color).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                            FluentTransitions.Transition.With(ActionCenter, nameof(ActionCenter.ActionCenterButton_Normal), AC_Normal).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                            FluentTransitions.Transition.With(ActionCenter, nameof(ActionCenter.ActionCenterButton_Hover), AC_Hover).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                            FluentTransitions.Transition.With(ActionCenter, nameof(ActionCenter.ActionCenterButton_Pressed), AC_Pressed).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                            FluentTransitions.Transition.With(Taskbar, nameof(Taskbar.AppUnderline), TB_UL_Color).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                            FluentTransitions.Transition.With(setting_icon_preview, nameof(setting_icon_preview.ForeColor), Settings_Label_Color).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                            FluentTransitions.Transition.With(Link_preview, nameof(Link_preview.ForeColor), Link_preview_Color).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                            FluentTransitions.Transition.With(settings_label, nameof(settings_label.ForeColor), TM.Windows11.AppMode_Light ? Color.Black : Color.White).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                        }
                        else
                        {
                            Taskbar.Background = TB_Color;
                            Start.Background = S_Color;
                            ActionCenter.Background = AC_Color;
                            ActionCenter.ActionCenterButton_Normal = AC_Normal;
                            ActionCenter.ActionCenterButton_Hover = AC_Hover;
                            ActionCenter.ActionCenterButton_Pressed = AC_Pressed;
                            Taskbar.AppUnderline = TB_UL_Color;
                            setting_icon_preview.ForeColor = Settings_Label_Color;
                            Link_preview.ForeColor = Link_preview_Color;
                            settings_label.ForeColor = TM.Windows11.AppMode_Light ? Color.Black : Color.White;
                        }

                        break;
                    }
                #endregion

                case WindowStyle.W10:
                    {
                        #region Win10
                        byte TB_Alpha, S_Alpha, AC_Alpha;
                        byte TB_Blur;
                        Color TB_Color = default, S_Color = default, AC_Color = default;
                        var TB_StartBtnColor = default(Color);

                        var TB_UL_Color = default(Color);
                        var TB_AppBack_Color = default(Color);
                        var AC_LinkColor = default(Color);

                        Color Settings_Label_Color = default, Link_preview_Color = default;
                        var AC_Normal = default(Color);

                        Start.DarkMode = !TM.Windows10.WinMode_Light;
                        Taskbar.DarkMode = !TM.Windows10.WinMode_Light;
                        ActionCenter.DarkMode = !TM.Windows10.WinMode_Light;
                        Taskbar.Transparency = TM.Windows10.Transparency;
                        Start.Transparency = TM.Windows10.Transparency && TM.Windows10.TB_Blur;
                        ActionCenter.Transparency = TM.Windows10.Transparency && TM.Windows10.TB_Blur;

                        if (!TM.Windows10.TB_Blur)
                        {
                            TB_Blur = 0;
                        }
                        else
                        {
                            TB_Blur = (byte)(!TM.Windows10.IncreaseTBTransparency ? 12 : 8);
                        }

                        if (TM.Windows10.Transparency)
                        {
                            if (!TM.Windows10.WinMode_Light)
                            {
                                TB_Alpha = (byte)(!TM.Windows10.IncreaseTBTransparency ? 150 : 75);
                                S_Alpha = 150;
                                AC_Alpha = 150;
                            }
                            else
                            {
                                TB_Alpha = (byte)(!TM.Windows10.IncreaseTBTransparency ? 200 : 125);
                                S_Alpha = 200;
                                AC_Alpha = 200;
                            }
                        }
                        else
                        {
                            TB_Alpha = 255;
                            S_Alpha = 255;
                            AC_Alpha = 255;
                        }

                        switch (!TM.Windows10.WinMode_Light)
                        {
                            case true:
                                {

                                    if (TM.Windows10.Transparency)
                                    {
                                        switch (TM.Windows10.ApplyAccentOnTaskbar)
                                        {
                                            case Theme.Structures.Windows10x.AccentTaskbarLevels.None:
                                                {
                                                    TB_Color = Color.FromArgb(16, 16, 16);
                                                    TB_StartBtnColor = Color.FromArgb(150, 150, 150, 150);
                                                    S_Color = Color.FromArgb(31, 31, 31);
                                                    AC_Color = Color.FromArgb(31, 31, 31);

                                                    TB_AppBack_Color = Color.FromArgb(150, 150, 150, 150);
                                                    AC_LinkColor = TM.Windows10.Color_Index0;
                                                    TB_UL_Color = TM.Windows10.Color_Index1;
                                                    Settings_Label_Color = TM.Windows10.Color_Index3;
                                                    Link_preview_Color = TM.Windows10.Color_Index3;
                                                    AC_Normal = TM.Windows10.Color_Index3;
                                                    break;
                                                }

                                            case Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar:
                                                {
                                                    TB_Color = TM.Windows10.Color_Index6;
                                                    TB_StartBtnColor = Color.FromArgb(0, 0, 0, 0);
                                                    S_Color = Color.FromArgb(31, 31, 31);
                                                    AC_Color = Color.FromArgb(31, 31, 31);

                                                    TB_AppBack_Color = Color.FromArgb(150, TM.Windows10.Color_Index3);
                                                    AC_LinkColor = TM.Windows10.Color_Index0;
                                                    TB_UL_Color = TM.Windows10.Color_Index1;
                                                    Settings_Label_Color = TM.Windows10.Color_Index3;
                                                    Link_preview_Color = TM.Windows10.Color_Index3;
                                                    AC_Normal = TM.Windows10.Color_Index3;
                                                    break;
                                                }

                                            case Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC:
                                                {
                                                    TB_Color = TM.Windows10.Color_Index6;
                                                    TB_StartBtnColor = Color.FromArgb(0, 0, 0, 0);
                                                    S_Color = TM.Windows10.Color_Index4;
                                                    AC_Color = TM.Windows10.Color_Index4;

                                                    TB_AppBack_Color = Color.FromArgb(150, TM.Windows10.Color_Index3);
                                                    AC_LinkColor = TM.Windows10.Color_Index0;
                                                    TB_UL_Color = TM.Windows10.Color_Index1;
                                                    Settings_Label_Color = TM.Windows10.Color_Index3;
                                                    Link_preview_Color = TM.Windows10.Color_Index3;
                                                    AC_Normal = TM.Windows10.Color_Index3;
                                                    break;
                                                }

                                        }
                                    }

                                    else
                                    {
                                        switch (TM.Windows10.ApplyAccentOnTaskbar)
                                        {
                                            case Theme.Structures.Windows10x.AccentTaskbarLevels.None:
                                                {
                                                    TB_Color = Color.FromArgb(16, 16, 16);
                                                    TB_StartBtnColor = Color.FromArgb(31, 31, 31);
                                                    S_Color = Color.FromArgb(31, 31, 31);
                                                    AC_Color = Color.FromArgb(31, 31, 31);
                                                    break;
                                                }

                                            case Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar:
                                                {
                                                    TB_Color = TM.Windows10.Color_Index5;
                                                    TB_StartBtnColor = TM.Windows10.Color_Index4;
                                                    S_Color = Color.FromArgb(31, 31, 31);
                                                    AC_Color = Color.FromArgb(31, 31, 31);
                                                    break;
                                                }

                                            case Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC:
                                                {
                                                    TB_Color = TM.Windows10.Color_Index5;
                                                    TB_StartBtnColor = TM.Windows10.Color_Index4;
                                                    S_Color = TM.Windows10.Color_Index4;
                                                    AC_Color = TM.Windows10.Color_Index4;
                                                    break;
                                                }
                                        }

                                        if (TM.Windows10.ApplyAccentOnTaskbar == Theme.Structures.Windows10x.AccentTaskbarLevels.None)
                                        {
                                            TB_AppBack_Color = Color.FromArgb(150, 100, 100, 100);
                                        }
                                        else
                                        {
                                            TB_AppBack_Color = TM.Windows10.Color_Index4;
                                        }

                                        AC_LinkColor = TM.Windows10.Color_Index0;
                                        TB_UL_Color = TM.Windows10.Color_Index1;
                                        Settings_Label_Color = TM.Windows10.Color_Index3;
                                        Link_preview_Color = TM.Windows10.Color_Index3;
                                        AC_Normal = TM.Windows10.Color_Index3;

                                    }

                                    break;
                                }

                            case false:
                                {
                                    if (TM.Windows10.Transparency)
                                    {

                                        switch (TM.Windows10.ApplyAccentOnTaskbar)
                                        {
                                            case Theme.Structures.Windows10x.AccentTaskbarLevels.None:
                                                {
                                                    TB_Color = Color.FromArgb(238, 238, 238);
                                                    TB_StartBtnColor = Color.Transparent;
                                                    S_Color = Color.FromArgb(228, 228, 228);
                                                    AC_Color = Color.FromArgb(228, 228, 228);

                                                    TB_AppBack_Color = Color.FromArgb(150, 238, 238, 238);
                                                    AC_LinkColor = TM.Windows10.Color_Index6;
                                                    TB_UL_Color = TM.Windows10.Color_Index3;
                                                    Settings_Label_Color = TM.Windows10.Color_Index3;
                                                    Link_preview_Color = TM.Windows10.Color_Index3;
                                                    AC_Normal = TM.Windows10.Color_Index3;
                                                    break;
                                                }

                                            case Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar:
                                                {
                                                    TB_Color = TM.Windows10.Color_Index6;
                                                    TB_StartBtnColor = Color.Transparent;
                                                    S_Color = Color.FromArgb(228, 228, 228);
                                                    AC_Color = Color.FromArgb(228, 228, 228);

                                                    TB_AppBack_Color = Color.FromArgb(150, TM.Windows10.Color_Index3);
                                                    AC_LinkColor = TM.Windows10.Color_Index6;
                                                    TB_UL_Color = TM.Windows10.Color_Index1;
                                                    Settings_Label_Color = TM.Windows10.Color_Index3;
                                                    Link_preview_Color = TM.Windows10.Color_Index3;
                                                    AC_Normal = TM.Windows10.Color_Index3;
                                                    break;
                                                }

                                            case Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC:
                                                {
                                                    TB_Color = TM.Windows10.Color_Index6;
                                                    TB_StartBtnColor = Color.Transparent;
                                                    S_Color = TM.Windows10.Color_Index4;
                                                    AC_Color = TM.Windows10.Color_Index4;

                                                    TB_AppBack_Color = Color.FromArgb(150, TM.Windows10.Color_Index3);
                                                    AC_LinkColor = TM.Windows10.Color_Index0;
                                                    TB_UL_Color = TM.Windows10.Color_Index1;
                                                    Settings_Label_Color = TM.Windows10.Color_Index3;
                                                    Link_preview_Color = TM.Windows10.Color_Index3;
                                                    AC_Normal = TM.Windows10.Color_Index3;
                                                    break;
                                                }

                                        }
                                    }

                                    else
                                    {

                                        switch (TM.Windows10.ApplyAccentOnTaskbar)
                                        {
                                            case Theme.Structures.Windows10x.AccentTaskbarLevels.None:
                                                {
                                                    TB_Color = Color.FromArgb(238, 238, 238);
                                                    TB_StartBtnColor = Color.FromArgb(241, 241, 241);
                                                    S_Color = Color.FromArgb(228, 228, 228);
                                                    AC_Color = Color.FromArgb(228, 228, 228);

                                                    TB_AppBack_Color = Color.FromArgb(252, 252, 252);
                                                    AC_LinkColor = TM.Windows10.Color_Index6;
                                                    TB_UL_Color = TM.Windows10.Color_Index3;
                                                    Settings_Label_Color = TM.Windows10.Color_Index3;
                                                    Link_preview_Color = TM.Windows10.Color_Index3;
                                                    AC_Normal = TM.Windows10.Color_Index3;
                                                    break;
                                                }

                                            case Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar:
                                                {
                                                    TB_Color = TM.Windows10.Color_Index5;
                                                    TB_StartBtnColor = TM.Windows10.Color_Index4;
                                                    S_Color = Color.FromArgb(228, 228, 228);
                                                    AC_Color = Color.FromArgb(228, 228, 228);

                                                    TB_AppBack_Color = TM.Windows10.Color_Index4;
                                                    AC_LinkColor = TM.Windows10.Color_Index6;
                                                    TB_UL_Color = TM.Windows10.Color_Index1;
                                                    Settings_Label_Color = TM.Windows10.Color_Index3;
                                                    Link_preview_Color = TM.Windows10.Color_Index3;
                                                    AC_Normal = TM.Windows10.Color_Index3;
                                                    break;
                                                }

                                            case Theme.Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC:
                                                {
                                                    TB_Color = TM.Windows10.Color_Index5;
                                                    TB_StartBtnColor = TM.Windows10.Color_Index4;
                                                    S_Color = TM.Windows10.Color_Index4;
                                                    AC_Color = TM.Windows10.Color_Index4;

                                                    TB_AppBack_Color = TM.Windows10.Color_Index4;
                                                    AC_LinkColor = TM.Windows10.Color_Index0;
                                                    TB_UL_Color = TM.Windows10.Color_Index1;
                                                    Settings_Label_Color = TM.Windows10.Color_Index3;
                                                    Link_preview_Color = TM.Windows10.Color_Index3;
                                                    AC_Normal = TM.Windows10.Color_Index3;
                                                    break;
                                                }
                                        }

                                    }

                                    break;
                                }
                        }

                        ActionCenter.BackColorAlpha = AC_Alpha;
                        Start.BackColorAlpha = S_Alpha;
                        Taskbar.BackColorAlpha = TB_Alpha;
                        Taskbar.BlurPower = TB_Blur;

                        if (AnimateColorChange)
                        {
                            FluentTransitions.Transition.With(Taskbar, nameof(Taskbar.Background), TB_Color).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                            FluentTransitions.Transition.With(Taskbar, nameof(Taskbar.StartColor), TB_StartBtnColor).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                            FluentTransitions.Transition.With(Start, nameof(Start.Background), S_Color).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                            FluentTransitions.Transition.With(ActionCenter, nameof(ActionCenter.Background), AC_Color).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                            FluentTransitions.Transition.With(Taskbar, nameof(Taskbar.AppBackground), TB_AppBack_Color).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                            FluentTransitions.Transition.With(ActionCenter, nameof(ActionCenter.LinkColor), AC_LinkColor).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                            FluentTransitions.Transition.With(Taskbar, nameof(Taskbar.AppUnderline), TB_UL_Color).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                            FluentTransitions.Transition.With(setting_icon_preview, nameof(setting_icon_preview.ForeColor), Settings_Label_Color).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                            FluentTransitions.Transition.With(Link_preview, nameof(Link_preview.ForeColor), Link_preview_Color).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                            FluentTransitions.Transition.With(ActionCenter, nameof(ActionCenter.ActionCenterButton_Normal), AC_Normal).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                        }
                        else
                        {
                            Taskbar.Background = TB_Color;
                            Taskbar.StartColor = TB_StartBtnColor;
                            Start.Background = S_Color;
                            ActionCenter.Background = AC_Color;
                            Taskbar.AppBackground = TB_AppBack_Color;
                            ActionCenter.LinkColor = AC_LinkColor;
                            Taskbar.AppUnderline = TB_UL_Color;
                            setting_icon_preview.ForeColor = Settings_Label_Color;
                            Link_preview.ForeColor = Link_preview_Color;
                            ActionCenter.ActionCenterButton_Normal = AC_Normal;
                        }

                        break;
                    }

                #endregion

                case WindowStyle.W81:
                    {
                        #region Win8.1
                        switch (TM.Windows81.Theme)
                        {
                            case Theme.Structures.Windows7.Themes.Aero:
                                {
                                    Taskbar.Transparency = true;
                                    Taskbar.BackColorAlpha = 100;
                                    break;
                                }
                            case Theme.Structures.Windows7.Themes.AeroLite:
                                {
                                    Taskbar.Transparency = false;
                                    Taskbar.BackColorAlpha = 255;
                                    break;
                                }
                        }

                        if (AnimateColorChange)
                        {
                            FluentTransitions.Transition.With(Taskbar, nameof(Taskbar.Background), TM.Windows81.ColorizationColor).Linear(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
                        }
                        else
                        {
                            Taskbar.Background = TM.Windows81.ColorizationColor;
                        }

                        Taskbar.Win7ColorBal = TM.Windows81.ColorizationColorBalance;
                        break;
                    }
                #endregion

                case WindowStyle.W7:
                    {
                        #region Win7
                        Start.Transparency = !(TM.Windows7.Theme == Theme.Structures.Windows7.Themes.Basic) & !(TM.Windows7.Theme == Theme.Structures.Windows7.Themes.Classic);
                        Taskbar.Transparency = !(TM.Windows7.Theme == Theme.Structures.Windows7.Themes.Basic) & !(TM.Windows7.Theme == Theme.Structures.Windows7.Themes.Classic);

                        switch (TM.Windows7.Theme)
                        {
                            case Theme.Structures.Windows7.Themes.Aero:
                                {
                                    Start.BackColorAlpha = TM.Windows7.ColorizationBlurBalance;
                                    Start.Win7ColorBal = TM.Windows7.ColorizationColorBalance;
                                    Start.Win7GlowBal = TM.Windows7.ColorizationAfterglowBalance;
                                    Start.Background = TM.Windows7.ColorizationColor;
                                    Start.Background2 = TM.Windows7.ColorizationAfterglow;
                                    Start.NoisePower = TM.Windows7.ColorizationGlassReflectionIntensity;
                                    Taskbar.BackColorAlpha = TM.Windows7.ColorizationBlurBalance;
                                    Taskbar.Win7ColorBal = TM.Windows7.ColorizationColorBalance;
                                    Taskbar.Win7GlowBal = TM.Windows7.ColorizationAfterglowBalance;
                                    Taskbar.Background = TM.Windows7.ColorizationColor;
                                    Taskbar.Background2 = TM.Windows7.ColorizationAfterglow;
                                    Taskbar.NoisePower = TM.Windows7.ColorizationGlassReflectionIntensity;
                                    break;
                                }

                            case Theme.Structures.Windows7.Themes.AeroOpaque:
                                {
                                    Taskbar.BackColorAlpha = TM.Windows7.ColorizationColorBalance;
                                    Taskbar.Background = TM.Windows7.ColorizationColor;
                                    Taskbar.Background2 = TM.Windows7.ColorizationColor;
                                    Taskbar.NoisePower = TM.Windows7.ColorizationGlassReflectionIntensity;
                                    Start.BackColorAlpha = TM.Windows7.ColorizationColorBalance;
                                    Start.Background = TM.Windows7.ColorizationColor;
                                    Start.Background2 = TM.Windows7.ColorizationColor;
                                    Start.NoisePower = TM.Windows7.ColorizationGlassReflectionIntensity;
                                    break;
                                }

                            case Theme.Structures.Windows7.Themes.Basic:
                                {
                                    Taskbar.Background = Color.FromArgb(166, 190, 218);
                                    Taskbar.BackColorAlpha = 100;
                                    Start.Background = Color.FromArgb(166, 190, 218);
                                    Start.BackColorAlpha = 100;
                                    Start.NoisePower = 0f;
                                    Taskbar.NoisePower = 0f;
                                    break;
                                }
                        }

                        break;
                    }
                #endregion

                case WindowStyle.WVista:
                    {
                        #region WinVista
                        Start.Transparency = !(TM.WindowsVista.Theme == Theme.Structures.Windows7.Themes.Basic) & !(TM.WindowsVista.Theme == Theme.Structures.Windows7.Themes.Classic);
                        Taskbar.Transparency = !(TM.WindowsVista.Theme == Theme.Structures.Windows7.Themes.Basic) & !(TM.WindowsVista.Theme == Theme.Structures.Windows7.Themes.Classic);

                        switch (TM.WindowsVista.Theme)
                        {
                            case Theme.Structures.Windows7.Themes.Aero:
                                {
                                    Start.BackColorAlpha = (int)Math.Round(TM.WindowsVista.Alpha / 255d * 180d);
                                    Start.Win7ColorBal = (int)Math.Round((255 - TM.WindowsVista.Alpha) / 255d * 100d);
                                    // .Win7GlowBal = [Manager].WindowsVista.ColorizationAfterglowBalance
                                    Start.Background = TM.WindowsVista.ColorizationColor;
                                    Start.Background2 = TM.WindowsVista.ColorizationColor;
                                    Start.NoisePower = 100f;
                                    Taskbar.BackColorAlpha = (int)Math.Round(TM.WindowsVista.Alpha / 255d * 180d);
                                    Taskbar.Win7ColorBal = (int)Math.Round((255 - TM.WindowsVista.Alpha) / 255d * 100d);
                                    // .Win7GlowBal = [Manager].WindowsVista.ColorizationAfterglowBalance
                                    Taskbar.Background = TM.WindowsVista.ColorizationColor;
                                    Taskbar.Background2 = TM.WindowsVista.ColorizationColor;
                                    Taskbar.NoisePower = 100f;
                                    break;
                                }


                            case Theme.Structures.Windows7.Themes.AeroOpaque:
                                {
                                    Taskbar.BackColorAlpha = (int)Math.Round(TM.WindowsVista.Alpha / 255d * 200d);
                                    Taskbar.Background = TM.WindowsVista.ColorizationColor;
                                    Taskbar.Background2 = TM.WindowsVista.ColorizationColor;
                                    Taskbar.NoisePower = 100f;
                                    Start.BackColorAlpha = (int)Math.Round(TM.WindowsVista.Alpha / 255d * 200d);
                                    Start.Background = TM.WindowsVista.ColorizationColor;
                                    Start.Background2 = TM.WindowsVista.ColorizationColor;
                                    Start.NoisePower = 100f;
                                    break;
                                }

                            case Theme.Structures.Windows7.Themes.Basic:
                                {
                                    Taskbar.Background = Color.FromArgb(166, 190, 218);
                                    Taskbar.BackColorAlpha = 100;
                                    Start.Background = Color.FromArgb(166, 190, 218);
                                    Start.BackColorAlpha = 100;
                                    Start.NoisePower = 0f;
                                    Taskbar.NoisePower = 0f;
                                    break;
                                }

                        }

                        break;
                    }
                    #endregion

            }

            Taskbar.SuspendRefresh = false;
            Start.SuspendRefresh = false;
            ActionCenter.SuspendRefresh = false;

            Taskbar.Invalidate();
            Start.Invalidate();
            ActionCenter.Invalidate();

            if (!Theme.Manager.IsFontInstalled("Segoe MDL2 Assets"))
            {
                setting_icon_preview.Font = new Font("Arial", 28f, FontStyle.Regular);
                setting_icon_preview.Text = "♣";
            }

        }

        /// <summary>
        /// Apply styles into taskbar, start, action center  and windows in preview
        /// </summary>
        /// <param name="TM">WinPaletter theme manager</param>
        /// <param name="Style">Selected Windows edition</param>
        /// <param name="Taskbar"></param>
        /// <param name="Start"></param>
        /// <param name="ActionCenter"></param>
        /// <param name="Window1">Active window simulation control</param>
        /// <param name="Window2">Inactive window simulation control</param>
        /// <param name="Settings_Container"></param>
        /// <param name="Link_preview"></param>
        /// <param name="ClassicTaskbar">Classic taskbar simulation control</param>
        /// <param name="ClassicStartButton">Start button in classic taskbar simulation control</param>
        /// <param name="ClassicAppButton1">Active app in classic taskbar simulation control</param>
        /// <param name="ClassicAppButton2">Inctive app in classic taskbar simulation control</param>
        /// <param name="ClassicWindow1">Active classic window simulation control</param>
        /// <param name="ClassicWindow2">Inactive classic window simulation control</param>
        /// <param name="WXP_VS_ReplaceColors">Use colors in Visual Styles file for Windows XP instead of registry</param>
        /// <param name="WXP_VS_ReplaceMetrics">Use metrics in Visual Styles file for Windows XP instead of registry</param>
        /// <param name="WXP_VS_ReplaceFonts">Use fonts in Visual Styles file for Windows XP instead of registry</param>
        public static void ApplyWinElementsStyle(Theme.Manager TM, WindowStyle Style, UI.Simulation.WinElement Taskbar, UI.Simulation.WinElement Start, UI.Simulation.WinElement ActionCenter, UI.Simulation.Window Window1, UI.Simulation.Window Window2, Panel Settings_Container, Label Link_preview, UI.Retro.PanelRaisedR ClassicTaskbar, UI.Retro.ButtonR ClassicStartButton, UI.Retro.ButtonR ClassicAppButton1, UI.Retro.ButtonR ClassicAppButton2, UI.Retro.WindowR ClassicWindow1, UI.Retro.WindowR ClassicWindow2, bool WXP_VS_ReplaceColors, bool WXP_VS_ReplaceMetrics, bool WXP_VS_ReplaceFonts)
        {
            Config.RenderingHint = TM.MetricsFonts.Fonts_SingleBitPP ? TextRenderingHint.SingleBitPerPixelGridFit : TextRenderingHint.ClearTypeGridFit;

            Taskbar.SuspendRefresh = true;
            Start.SuspendRefresh = true;
            ActionCenter.SuspendRefresh = true;
            Window1.SuspendRefresh = true;
            Window2.SuspendRefresh = true;

            var AC_Style = ActionCenter.Style;
            var Start_Style = Start.Style;
            var Taskbar_Style = Taskbar.Style;
            var Window_Style = Window1.Preview;

            Settings_Container.Visible = Style == WindowStyle.W12 || Style == WindowStyle.W11 || Style == WindowStyle.W10;
            Link_preview.Visible = Style == WindowStyle.W12 || Style == WindowStyle.W11 || Style == WindowStyle.W10;
            Start.Visible = !(Style == WindowStyle.W81) & !(Style == WindowStyle.W10 && TM.WindowsEffects.FullScreenStartMenu);
            ActionCenter.Visible = Style == WindowStyle.W12 || Style == WindowStyle.W11 || Style == WindowStyle.W10;

            switch (Style)
            {
                case WindowStyle.W11:
                    {
                        Window_Style = UI.Simulation.Window.Preview_Enum.W11;

                        AC_Style = UI.Simulation.WinElement.Styles.ActionCenter11;

                        if (ExplorerPatcher.IsAllowed())
                        {
                            {
                                ref ExplorerPatcher EP = ref Program.EP;
                                if (!EP.UseStart10)
                                {
                                    Start_Style = UI.Simulation.WinElement.Styles.Start11;
                                }
                                else
                                {
                                    Start_Style = UI.Simulation.WinElement.Styles.Start10;
                                }

                                if (!EP.UseTaskbar10)
                                {
                                    Taskbar_Style = UI.Simulation.WinElement.Styles.Taskbar11;
                                }
                                else
                                {
                                    Taskbar_Style = UI.Simulation.WinElement.Styles.Taskbar10;
                                }
                            }
                        }
                        else
                        {
                            Taskbar_Style = UI.Simulation.WinElement.Styles.Taskbar11;
                            Start_Style = UI.Simulation.WinElement.Styles.Start11;
                        }

                        break;
                    }

                case WindowStyle.W10:
                    {
                        Window_Style = UI.Simulation.Window.Preview_Enum.W10;
                        AC_Style = UI.Simulation.WinElement.Styles.ActionCenter10;
                        Taskbar_Style = UI.Simulation.WinElement.Styles.Taskbar10;
                        Start_Style = UI.Simulation.WinElement.Styles.Start10;
                        break;
                    }

                case WindowStyle.W81:
                    {
                        Window_Style = TM.Windows81.Theme == Theme.Structures.Windows7.Themes.AeroLite ? UI.Simulation.Window.Preview_Enum.W8Lite : UI.Simulation.Window.Preview_Enum.W8;
                        Taskbar_Style = TM.Windows81.Theme == Theme.Structures.Windows7.Themes.Aero ? UI.Simulation.WinElement.Styles.Taskbar8Aero : UI.Simulation.WinElement.Styles.Taskbar8Lite;
                        break;
                    }

                case WindowStyle.W7:
                    {
                        switch (TM.Windows7.Theme)
                        {
                            case Theme.Structures.Windows7.Themes.Aero:
                                {
                                    Window_Style = UI.Simulation.Window.Preview_Enum.W7Aero;
                                    Taskbar_Style = UI.Simulation.WinElement.Styles.Taskbar7Aero;
                                    Start_Style = UI.Simulation.WinElement.Styles.Start7Aero;
                                    break;
                                }

                            case Theme.Structures.Windows7.Themes.AeroOpaque:
                                {
                                    Window_Style = UI.Simulation.Window.Preview_Enum.W7Opaque;
                                    Taskbar_Style = UI.Simulation.WinElement.Styles.Taskbar7Opaque;
                                    Start_Style = UI.Simulation.WinElement.Styles.Start7Opaque;
                                    break;
                                }

                            case Theme.Structures.Windows7.Themes.Basic:
                                {
                                    Window_Style = UI.Simulation.Window.Preview_Enum.W7Basic;
                                    Taskbar_Style = UI.Simulation.WinElement.Styles.Taskbar7Basic;
                                    Start_Style = UI.Simulation.WinElement.Styles.Start7Basic;
                                    break;
                                }

                        }

                        break;
                    }

                case WindowStyle.WVista:
                    {
                        switch (TM.WindowsVista.Theme)     // Windows Vista uses the same aero of Windows 7
                        {
                            case Theme.Structures.Windows7.Themes.Aero:
                                {
                                    Window_Style = UI.Simulation.Window.Preview_Enum.W7Aero;
                                    Taskbar_Style = UI.Simulation.WinElement.Styles.TaskbarVistaAero;
                                    Start_Style = UI.Simulation.WinElement.Styles.StartVistaAero;
                                    break;
                                }

                            case Theme.Structures.Windows7.Themes.AeroOpaque:
                                {
                                    Window_Style = UI.Simulation.Window.Preview_Enum.W7Opaque;
                                    Taskbar_Style = UI.Simulation.WinElement.Styles.TaskbarVistaOpaque;
                                    Start_Style = UI.Simulation.WinElement.Styles.StartVistaOpaque;
                                    break;
                                }

                            case Theme.Structures.Windows7.Themes.Basic:
                                {
                                    Window_Style = UI.Simulation.Window.Preview_Enum.W7Basic;
                                    Taskbar_Style = UI.Simulation.WinElement.Styles.TaskbarVistaBasic;
                                    Start_Style = UI.Simulation.WinElement.Styles.StartVistaBasic;
                                    break;
                                }

                        }

                        break;
                    }

                case WindowStyle.WXP:
                    {
                        Window_Style = UI.Simulation.Window.Preview_Enum.WXP;
                        Taskbar_Style = UI.Simulation.WinElement.Styles.TaskbarXP;
                        Start_Style = UI.Simulation.WinElement.Styles.StartXP;

                        switch (TM.WindowsXP.Theme)
                        {
                            case Theme.Structures.WindowsXP.Themes.LunaBlue:
                                {
                                    PathsExt.MSTheme = PathsExt.MSTheme_Luna_theme;
                                    System.IO.File.WriteAllText(PathsExt.MSTheme_Luna_theme, string.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=NormalColor{1}Size=NormalSize", PathsExt.appData + @"\VisualStyles\Luna\luna.msstyles", "\r\n"));
                                    Program.resVS = new VisualStylesRes(PathsExt.MSTheme);
                                    break;
                                }

                            case Theme.Structures.WindowsXP.Themes.LunaOliveGreen:
                                {
                                    PathsExt.MSTheme = PathsExt.MSTheme_Luna_theme;
                                    System.IO.File.WriteAllText(PathsExt.MSTheme_Luna_theme, string.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=HomeStead{1}Size=NormalSize", PathsExt.appData + @"\VisualStyles\Luna\luna.msstyles", "\r\n"));
                                    Program.resVS = new VisualStylesRes(PathsExt.MSTheme);
                                    break;
                                }

                            case Theme.Structures.WindowsXP.Themes.LunaSilver:
                                {
                                    PathsExt.MSTheme = PathsExt.MSTheme_Luna_theme;
                                    System.IO.File.WriteAllText(PathsExt.MSTheme_Luna_theme, string.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=Metallic{1}Size=NormalSize", PathsExt.appData + @"\VisualStyles\Luna\luna.msstyles", "\r\n"));
                                    Program.resVS = new VisualStylesRes(PathsExt.MSTheme);
                                    break;
                                }

                            case Theme.Structures.WindowsXP.Themes.Custom:
                                {
                                    if (System.IO.File.Exists(TM.WindowsXP.ThemeFile))
                                    {
                                        if (System.IO.Path.GetExtension(TM.WindowsXP.ThemeFile) == ".theme")
                                        {
                                            PathsExt.MSTheme = TM.WindowsXP.ThemeFile;
                                        }
                                        else if (System.IO.Path.GetExtension(TM.WindowsXP.ThemeFile) == ".msstyles")
                                        {
                                            PathsExt.MSTheme = PathsExt.MSTheme_Luna_theme;
                                            System.IO.File.WriteAllText(PathsExt.MSTheme_Luna_theme, string.Format("[VisualStyles]{1}Path={0}{1}ColorStyle={2}{1}Size=NormalSize", TM.WindowsXP.ThemeFile, "\r\n", TM.WindowsXP.ColorScheme));
                                        }
                                    }
                                    Program.resVS = new VisualStylesRes(PathsExt.MSTheme);
                                    break;
                                }

                            case Theme.Structures.WindowsXP.Themes.Classic:
                                {
                                    PathsExt.MSTheme = PathsExt.MSTheme_Luna_theme;
                                    System.IO.File.WriteAllText(PathsExt.MSTheme_Luna_theme, string.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=NormalColor{1}Size=NormalSize", PathsExt.appData + @"\VisualStyles\Luna\luna.msstyles", "\r\n"));
                                    Program.resVS = new VisualStylesRes(PathsExt.MSTheme);
                                    break;
                                }

                        }

                        if (WXP_VS_ReplaceColors & TM.WindowsXP.Theme != Theme.Structures.WindowsXP.Themes.Classic)
                        {
                            if (System.IO.File.Exists(PathsExt.MSTheme) & !string.IsNullOrEmpty(PathsExt.MSTheme))
                            {
                                var vs = new Devcorp.Controls.VisualStyles.VisualStyleFile(PathsExt.MSTheme);
                                TM.Win32.Load(Theme.Structures.Win32UI.Sources.VisualStyles, vs.Metrics);
                            }
                        }

                        if (WXP_VS_ReplaceMetrics & TM.WindowsXP.Theme != Theme.Structures.WindowsXP.Themes.Classic)
                        {
                            if (System.IO.File.Exists(PathsExt.MSTheme) & !string.IsNullOrEmpty(PathsExt.MSTheme))
                            {
                                var vs = new Devcorp.Controls.VisualStyles.VisualStyleFile(PathsExt.MSTheme);
                                TM.MetricsFonts.Overwrite_Metrics(vs.Metrics);
                            }
                        }

                        if (WXP_VS_ReplaceFonts & TM.WindowsXP.Theme != Theme.Structures.WindowsXP.Themes.Classic)
                        {
                            if (System.IO.File.Exists(PathsExt.MSTheme) & !string.IsNullOrEmpty(PathsExt.MSTheme))
                            {
                                var vs = new Devcorp.Controls.VisualStyles.VisualStyleFile(PathsExt.MSTheme);
                                TM.MetricsFonts.Overwrite_Fonts(vs.Metrics);
                            }
                        }

                        break;
                    }

            }
            Window1.Preview = Window_Style;
            Start.Style = Start_Style;
            Taskbar.Style = Taskbar_Style;
            ActionCenter.Style = AC_Style;
            Window1.WinVista = Style == WindowStyle.WVista;
            Window2.WinVista = Style == WindowStyle.WVista;
            Window2.Preview = Window1.Preview;

            SetModernWindowMetrics(TM, Window1);
            SetModernWindowMetrics(TM, Window2);
            SetClassicWindowMetrics(TM, ClassicWindow1);
            SetClassicWindowMetrics(TM, ClassicWindow2);
            SetClassicWindowColors(TM, ClassicWindow1);
            SetClassicWindowColors(TM, ClassicWindow2, false);
            SetClassicButtonColors(TM, ClassicStartButton);
            SetClassicButtonColors(TM, ClassicAppButton1);
            SetClassicButtonColors(TM, ClassicAppButton2);
            SetClassicPanelRaisedRColors(TM, ClassicTaskbar);

            if (Style != WindowStyle.WVista & Style != WindowStyle.WXP)
            {
                ClassicTaskbar.Height = 44;
                ClassicAppButton1.Image = Properties.Resources.SampleApp_Active;
                ClassicAppButton2.Image = Properties.Resources.SampleApp_Inactive;
                ClassicStartButton.Image = Properties.Resources.Native7.Resize(18, 16);
                ClassicAppButton1.ImageAlign = ContentAlignment.MiddleCenter;
                ClassicAppButton2.ImageAlign = ContentAlignment.MiddleCenter;
                ClassicAppButton1.Width = 48;
                ClassicAppButton2.Width = 48;
                ClassicAppButton1.Text = string.Empty;
                ClassicAppButton2.Text = string.Empty;
                ClassicAppButton2.Left = ClassicAppButton1.Right + 3;
                ClassicAppButton1.Font = new Font(TM.MetricsFonts.CaptionFont.Name, 8f, ClassicAppButton1.Font.Style);
                ClassicAppButton2.Font = new Font(TM.MetricsFonts.CaptionFont.Name, 8f, ClassicAppButton2.Font.Style);
                ClassicStartButton.Font = new Font(TM.MetricsFonts.CaptionFont.Name, 8.5f, ClassicStartButton.Font.Style);
                ClassicAppButton1.HatchBrush = false;
            }

            switch (Style)
            {
                case WindowStyle.W11:
                    {
                        if (OS.W12 || OS.W11)
                            Program.EP = new ExplorerPatcher();

                        if (ExplorerPatcher.IsAllowed())
                        {
                            {
                                ref ExplorerPatcher EP = ref Program.EP;

                                if (!EP.UseTaskbar10)
                                {
                                    Taskbar.BlurPower = 8;
                                    Taskbar.Height = 42;
                                    Taskbar.NoisePower = 0.3f;
                                }
                                else
                                {
                                    Taskbar.BlurPower = 8;
                                    Taskbar.Height = 35;
                                    Taskbar.UseWin11ORB_WithWin10 = !EP.TaskbarButton10;
                                    Taskbar.NoisePower = 0f;
                                }

                                if (!EP.UseStart10)
                                {
                                    Start.BlurPower = 6;
                                    Start.NoisePower = 0.3f;
                                    Start.Size = new Size(135, 200);
                                    Start.Location = new Point(10, Taskbar.Bottom - Taskbar.Height - Start.Height - 10);
                                }
                                else
                                {
                                    Start.BlurPower = 7;
                                    Start.NoisePower = 0.3f;

                                    switch (EP.StartStyle)
                                    {
                                        case ExplorerPatcher.StartStyles.NotRounded:
                                            {
                                                Start.Size = new Size(182, 201);
                                                Start.Left = 0;
                                                Start.Top = Taskbar.Bottom - Taskbar.Height - Start.Height;
                                                Start.UseWin11RoundedCorners_WithWin10_Level1 = false;
                                                Start.UseWin11RoundedCorners_WithWin10_Level2 = false;
                                                break;
                                            }

                                        case ExplorerPatcher.StartStyles.RoundedCornersDockedMenu:
                                            {
                                                Start.Size = new Size(182, 201);
                                                Start.Left = 0;
                                                Start.Top = Taskbar.Bottom - Taskbar.Height - Start.Height;
                                                Start.UseWin11RoundedCorners_WithWin10_Level1 = true;
                                                Start.UseWin11RoundedCorners_WithWin10_Level2 = false;
                                                break;
                                            }

                                        case ExplorerPatcher.StartStyles.RoundedCornersFloatingMenu:
                                            {
                                                Start.Size = new Size(182, 201);
                                                Start.Location = new Point(10, Taskbar.Bottom - Taskbar.Height - Start.Height - 10);
                                                Start.UseWin11RoundedCorners_WithWin10_Level1 = false;
                                                Start.UseWin11RoundedCorners_WithWin10_Level2 = true;
                                                break;
                                            }
                                    }
                                }
                            }
                        }

                        else
                        {
                            Taskbar.BlurPower = 8;
                            Taskbar.Height = 42;
                            Taskbar.NoisePower = 0.3f;
                            // ########################
                            Start.BlurPower = 6;
                            Start.NoisePower = 0.3f;
                            Start.Size = new Size(135, 200);
                            Start.Location = new Point(10, Taskbar.Bottom - Taskbar.Height - Start.Height - 10);
                        }

                        ActionCenter.Dock = default;
                        ActionCenter.BlurPower = 6;
                        ActionCenter.NoisePower = 0.3f;
                        ActionCenter.Size = new Size(120, 85);
                        ActionCenter.Location = new Point(ActionCenter.Parent.Width - ActionCenter.Width - 10, ActionCenter.Parent.Height - ActionCenter.Height - Taskbar.Height - 10);
                        break;
                    }

                case WindowStyle.W10:
                    {
                        ActionCenter.Dock = DockStyle.Right;
                        ActionCenter.BlurPower = 7;
                        ActionCenter.NoisePower = 0.3f;
                        // ########################
                        Taskbar.BlurPower = !TM.Windows10.IncreaseTBTransparency ? 12 : 6;
                        // ########################
                        Start.BlurPower = 7;
                        Start.NoisePower = 0.3f;
                        // ########################

                        Taskbar.Height = 35;
                        Taskbar.UseWin11ORB_WithWin10 = false;
                        Start.Size = new Size(182, 201);
                        Start.Left = 0;
                        Start.Top = Taskbar.Bottom - Taskbar.Height - Start.Height;
                        Start.UseWin11RoundedCorners_WithWin10_Level1 = false;
                        Start.UseWin11RoundedCorners_WithWin10_Level2 = false;
                        break;
                    }

                case WindowStyle.W81:
                    {
                        Settings_Container.Visible = false;
                        Link_preview.Visible = false;
                        Start.Visible = false;
                        ActionCenter.Visible = false;
                        Taskbar.BlurPower = 0;
                        Taskbar.Height = 34;

                        Start.BlurPower = 0;
                        Start.Top = Taskbar.Top - Start.Height;
                        Start.Left = 0;
                        break;
                    }

                case WindowStyle.W7:
                    {
                        Settings_Container.Visible = false;
                        Link_preview.Visible = false;
                        ActionCenter.Visible = false;

                        Taskbar.BlurPower = 1;
                        Taskbar.NoisePower = (float)(TM.Windows7.ColorizationGlassReflectionIntensity / 100d);
                        Taskbar.Height = 34;

                        Start.BlurPower = 1;
                        Start.NoisePower = 0.5f;
                        Start.Width = 136;
                        Start.Height = 191;
                        Start.NoisePower = (float)(TM.Windows7.ColorizationGlassReflectionIntensity / 100d);
                        Start.Left = 0;
                        Start.Top = Taskbar.Top - Start.Height;
                        break;
                    }

                case WindowStyle.WVista:
                    {
                        Settings_Container.Visible = false;
                        Link_preview.Visible = false;
                        ActionCenter.Visible = false;
                        Taskbar.Height = 30;

                        Start.Width = 136;
                        Start.Height = 191;
                        Start.Left = 0;
                        Start.Top = Taskbar.Top - Start.Height;

                        ClassicTaskbar.Height = Taskbar.Height;
                        ClassicAppButton1.Image = Properties.Resources.SampleApp_Active.Resize(23, 23);
                        ClassicAppButton2.Image = Properties.Resources.SampleApp_Inactive.Resize(23, 23);
                        ClassicStartButton.Image = Properties.Resources.Native7.Resize(18, 16);
                        ClassicAppButton1.ImageAlign = ContentAlignment.BottomLeft;
                        ClassicAppButton2.ImageAlign = ContentAlignment.BottomLeft;
                        ClassicAppButton1.Width = 140;
                        ClassicAppButton2.Width = 140;
                        ClassicAppButton1.Text = ClassicWindow1.Text;
                        ClassicAppButton2.Text = ClassicWindow2.Text;
                        ClassicAppButton2.Left = ClassicAppButton1.Right + 3;
                        ClassicAppButton1.Font = new Font(TM.MetricsFonts.CaptionFont.Name, 8f, ClassicAppButton1.Font.Style);
                        ClassicAppButton2.Font = new Font(TM.MetricsFonts.CaptionFont.Name, 8f, ClassicAppButton2.Font.Style);
                        ClassicStartButton.Font = new Font(TM.MetricsFonts.CaptionFont.Name, 8.5f, ClassicStartButton.Font.Style);
                        ClassicAppButton1.HatchBrush = true;
                        break;
                    }

                case WindowStyle.WXP:
                    {
                        Taskbar.Height = 30;
                        Start.Width = 150;
                        Start.Height = 190;
                        Start.Left = 0;
                        Start.Top = Taskbar.Top - Start.Height;

                        ClassicTaskbar.Height = Taskbar.Height;
                        ClassicAppButton1.Image = Properties.Resources.SampleApp_Active.Resize(23, 23);
                        ClassicAppButton2.Image = Properties.Resources.SampleApp_Inactive.Resize(23, 23);
                        ClassicStartButton.Image = Properties.Resources.NativeXP.Resize(18, 16);
                        ClassicAppButton1.ImageAlign = ContentAlignment.BottomLeft;
                        ClassicAppButton2.ImageAlign = ContentAlignment.BottomLeft;
                        ClassicAppButton1.Width = 140;
                        ClassicAppButton2.Width = 140;
                        ClassicAppButton1.Text = ClassicWindow1.Text;
                        ClassicAppButton2.Text = ClassicWindow2.Text;
                        ClassicAppButton2.Left = ClassicAppButton1.Right + 3;
                        ClassicAppButton1.Font = new Font(TM.MetricsFonts.CaptionFont.Name, 8f, ClassicAppButton1.Font.Style);
                        ClassicAppButton2.Font = new Font(TM.MetricsFonts.CaptionFont.Name, 8f, ClassicAppButton2.Font.Style);
                        ClassicStartButton.Font = new Font(TM.MetricsFonts.CaptionFont.Name, 8.5f, ClassicStartButton.Font.Style);
                        ClassicAppButton1.HatchBrush = true;
                        break;
                    }

            }

            if (Style == WindowStyle.W12 || Style == WindowStyle.W11 || (Style == WindowStyle.W10 & !TM.WindowsEffects.FullScreenStartMenu))
            {
                Window1.Left = (int)Math.Round(Start.Right + (Window1.Parent.Width - (Start.Width + Start.Left) - (ActionCenter.Width + (ActionCenter.Parent.Width - ActionCenter.Right)) - Window1.Width) / 2d);
            }
            else
            {
                Window1.Left = (int)Math.Round((Window1.Parent.Width - Window1.Width) / 2d);
            }

            Window1.Top = (int)Math.Round((Window1.Parent.Height - Taskbar.Height - (Window1.Height + Window2.Height)) / 2d);
            Window2.Top = Window1.Bottom;
            Window2.Left = Window1.Left;

            Taskbar.SuspendRefresh = false;
            Start.SuspendRefresh = false;
            ActionCenter.SuspendRefresh = false;
            Window1.SuspendRefresh = false;
            Window2.SuspendRefresh = false;

            Taskbar.NoiseBack();
            Start.NoiseBack();
            ActionCenter.NoiseBack();

            Taskbar.ProcessBack();
            Start.ProcessBack();
            ActionCenter.ProcessBack();
            Window1.ProcessBack();
            Window2.ProcessBack();

            Taskbar.Invalidate();
            Start.Invalidate();
            ActionCenter.Invalidate();
            Window1.Invalidate();
            Window2.Invalidate();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TM">WinPaletter theme manager</param>
        /// <param name="Style">Selected Windows edition</param>
        /// <param name="Window1">Active window simulation control</param>
        /// <param name="Window2">Inactive window simulation control</param>
        /// <param name="StartButton">Start button that opens start background for Windows 8.1 in main form</param>
        /// <param name="LogonUIButton">LogonUI button that opens colors for Windows 8 LogonUI in main form</param>
        public static void ApplyWindowStyles(Theme.Manager TM, WindowStyle Style, UI.Simulation.Window Window1, UI.Simulation.Window Window2, UI.WP.Button StartButton = null, UI.WP.Button LogonUIButton = null)
        {
            Window1.Active = true;
            Window2.Active = false;

            if (ExplorerPatcher.IsAllowed())
                Program.EP = new ExplorerPatcher();

            Window1.SuspendRefresh = true;
            Window2.SuspendRefresh = true;

            switch (Style)
            {
                case WindowStyle.W12:
                    {
                        Window1.AccentColor_Enabled = TM.Windows12.ApplyAccentOnTitlebars;
                        Window2.AccentColor_Enabled = TM.Windows12.ApplyAccentOnTitlebars;

                        Window1.AccentColor_Active = TM.Windows12.Titlebar_Active;
                        Window2.AccentColor_Active = TM.Windows12.Titlebar_Active;

                        Window1.AccentColor_Inactive = TM.Windows12.Titlebar_Inactive;
                        Window2.AccentColor_Inactive = TM.Windows12.Titlebar_Inactive;

                        Window1.DarkMode = !TM.Windows12.AppMode_Light;
                        Window2.DarkMode = !TM.Windows12.AppMode_Light;

                        Window1.Shadow = TM.WindowsEffects.WindowShadow;
                        Window2.Shadow = TM.WindowsEffects.WindowShadow;
                        break;
                    }

                case WindowStyle.W11:
                    {
                        Window1.AccentColor_Enabled = TM.Windows11.ApplyAccentOnTitlebars;
                        Window2.AccentColor_Enabled = TM.Windows11.ApplyAccentOnTitlebars;

                        Window1.AccentColor_Active = TM.Windows11.Titlebar_Active;
                        Window2.AccentColor_Active = TM.Windows11.Titlebar_Active;

                        Window1.AccentColor_Inactive = TM.Windows11.Titlebar_Inactive;
                        Window2.AccentColor_Inactive = TM.Windows11.Titlebar_Inactive;

                        Window1.DarkMode = !TM.Windows11.AppMode_Light;
                        Window2.DarkMode = !TM.Windows11.AppMode_Light;

                        Window1.Shadow = TM.WindowsEffects.WindowShadow;
                        Window2.Shadow = TM.WindowsEffects.WindowShadow;
                        break;
                    }

                case WindowStyle.W10:
                    {
                        Window1.AccentColor_Enabled = TM.Windows10.ApplyAccentOnTitlebars;
                        Window2.AccentColor_Enabled = TM.Windows10.ApplyAccentOnTitlebars;

                        Window1.AccentColor_Active = TM.Windows10.Titlebar_Active;
                        Window2.AccentColor_Active = TM.Windows10.Titlebar_Active;

                        Window1.AccentColor_Inactive = TM.Windows10.Titlebar_Inactive;
                        Window2.AccentColor_Inactive = TM.Windows10.Titlebar_Inactive;

                        Window1.DarkMode = !TM.Windows10.AppMode_Light;
                        Window2.DarkMode = !TM.Windows10.AppMode_Light;

                        Window1.Shadow = TM.WindowsEffects.WindowShadow;
                        Window2.Shadow = TM.WindowsEffects.WindowShadow;
                        break;
                    }

                case WindowStyle.W81:
                    {
                        if ((OS.W8 || OS.W81) & Program.Settings.Miscellaneous.Win7LivePreview)
                        {
                            Program.RefreshDWM(TM);
                        }

                        if (StartButton is not null)
                            ApplyMetroStartToButton(TM, StartButton);
                        if (LogonUIButton is not null)
                            ApplyBackLogonUI(TM, LogonUIButton);

                        switch (TM.Windows81.Theme)
                        {
                            case Theme.Structures.Windows7.Themes.Aero:
                                {
                                    Window1.Preview = UI.Simulation.Window.Preview_Enum.W8;
                                    Window2.Preview = UI.Simulation.Window.Preview_Enum.W8;
                                    break;
                                }
                            case Theme.Structures.Windows7.Themes.AeroLite:
                                {
                                    Window1.Preview = UI.Simulation.Window.Preview_Enum.W8Lite;
                                    Window2.Preview = UI.Simulation.Window.Preview_Enum.W8Lite;
                                    break;
                                }
                        }

                        Window1.AccentColor_Active = TM.Windows81.ColorizationColor;
                        Window1.Win7ColorBal = TM.Windows81.ColorizationColorBalance;

                        Window2.AccentColor_Active = TM.Windows81.ColorizationColor;
                        Window2.Win7ColorBal = TM.Windows81.ColorizationColorBalance;
                        break;
                    }

                case WindowStyle.W7:
                    {
                        if (OS.W7 & Program.Settings.Miscellaneous.Win7LivePreview)
                        {
                            Program.RefreshDWM(TM);
                        }

                        Window1.Shadow = TM.WindowsEffects.WindowShadow;
                        Window2.Shadow = TM.WindowsEffects.WindowShadow;

                        switch (TM.Windows7.Theme)
                        {
                            case Theme.Structures.Windows7.Themes.Aero:
                                {
                                    Window1.Preview = UI.Simulation.Window.Preview_Enum.W7Aero;
                                    Window1.Win7Alpha = TM.Windows7.ColorizationBlurBalance;
                                    Window1.Win7ColorBal = TM.Windows7.ColorizationColorBalance;
                                    Window1.Win7GlowBal = TM.Windows7.ColorizationAfterglowBalance;
                                    Window1.AccentColor_Active = TM.Windows7.ColorizationColor;
                                    Window1.AccentColor2_Active = TM.Windows7.ColorizationAfterglow;
                                    Window1.AccentColor_Inactive = TM.Windows7.ColorizationColor;
                                    Window1.AccentColor2_Inactive = TM.Windows7.ColorizationAfterglow;
                                    Window1.Win7Noise = TM.Windows7.ColorizationGlassReflectionIntensity;
                                    Window2.Preview = UI.Simulation.Window.Preview_Enum.W7Aero;
                                    Window2.Win7Alpha = TM.Windows7.ColorizationBlurBalance;
                                    Window2.Win7ColorBal = TM.Windows7.ColorizationColorBalance;
                                    Window2.Win7GlowBal = TM.Windows7.ColorizationAfterglowBalance;
                                    Window2.AccentColor_Active = TM.Windows7.ColorizationColor;
                                    Window2.AccentColor2_Active = TM.Windows7.ColorizationAfterglow;
                                    Window2.AccentColor_Inactive = TM.Windows7.ColorizationColor;
                                    Window2.AccentColor2_Inactive = TM.Windows7.ColorizationAfterglow;
                                    Window2.Win7Noise = TM.Windows7.ColorizationGlassReflectionIntensity;
                                    break;
                                }

                            case Theme.Structures.Windows7.Themes.AeroOpaque:
                                {
                                    Window1.Preview = UI.Simulation.Window.Preview_Enum.W7Opaque;
                                    Window1.Win7Alpha = TM.Windows7.ColorizationColorBalance;
                                    Window1.AccentColor_Active = TM.Windows7.ColorizationColor;
                                    Window1.AccentColor_Inactive = TM.Windows7.ColorizationColor;
                                    Window1.Win7Noise = TM.Windows7.ColorizationGlassReflectionIntensity;
                                    Window2.Preview = UI.Simulation.Window.Preview_Enum.W7Opaque;
                                    Window2.Win7Alpha = TM.Windows7.ColorizationColorBalance;
                                    Window2.AccentColor_Active = TM.Windows7.ColorizationColor;
                                    Window2.AccentColor_Inactive = TM.Windows7.ColorizationColor;
                                    Window2.Win7Noise = TM.Windows7.ColorizationGlassReflectionIntensity;
                                    break;
                                }

                            case Theme.Structures.Windows7.Themes.Basic:
                                {
                                    Window1.Preview = UI.Simulation.Window.Preview_Enum.W7Basic;
                                    Window1.Win7Alpha = 100;
                                    Window1.AccentColor_Active = Color.FromArgb(166, 190, 218);
                                    Window1.Win7Noise = 0f;
                                    Window2.Preview = UI.Simulation.Window.Preview_Enum.W7Basic;
                                    Window2.Win7Alpha = 100;
                                    Window2.AccentColor_Inactive = Color.FromArgb(166, 190, 218);
                                    Window2.Win7Noise = 0f;
                                    break;
                                }
                        }

                        break;
                    }

                case WindowStyle.WVista:
                    {
                        if (OS.WVista & Program.Settings.Miscellaneous.Win7LivePreview)
                        {
                            Program.RefreshDWM(TM);
                        }

                        Window1.Shadow = TM.WindowsEffects.WindowShadow;
                        Window2.Shadow = TM.WindowsEffects.WindowShadow;

                        switch (TM.WindowsVista.Theme)
                        {
                            case Theme.Structures.Windows7.Themes.Aero:
                                {
                                    Window1.Preview = UI.Simulation.Window.Preview_Enum.W7Aero;
                                    Window1.Win7Alpha = (int)Math.Round((255 - TM.WindowsVista.Alpha) / 255d * 100d);
                                    Window1.Win7ColorBal = (int)Math.Round((255 - TM.WindowsVista.Alpha) / 255d * 100d);
                                    // .Win7GlowBal = [Manager].WindowsVista.ColorizationAfterglowBalance
                                    Window1.AccentColor_Active = Color.FromArgb(TM.WindowsVista.Alpha, TM.WindowsVista.ColorizationColor);
                                    Window1.AccentColor2_Active = Color.FromArgb(TM.WindowsVista.Alpha, TM.WindowsVista.ColorizationColor);
                                    Window1.AccentColor_Inactive = Color.FromArgb(100, TM.WindowsVista.ColorizationColor);
                                    Window1.AccentColor2_Inactive = Color.FromArgb(100, TM.WindowsVista.ColorizationColor);
                                    Window1.Win7Noise = 100f;
                                    Window2.Preview = UI.Simulation.Window.Preview_Enum.W7Aero;
                                    Window2.Win7Alpha = (int)Math.Round((255 - TM.WindowsVista.Alpha) / 255d * 100d);
                                    Window2.Win7ColorBal = (int)Math.Round((255 - TM.WindowsVista.Alpha) / 255d * 100d);
                                    // .Win7GlowBal = [Manager].WindowsVista.ColorizationAfterglowBalance
                                    Window2.AccentColor_Active = TM.WindowsVista.ColorizationColor;
                                    Window2.AccentColor2_Active = TM.WindowsVista.ColorizationColor;
                                    Window2.AccentColor_Inactive = Color.FromArgb(100, TM.WindowsVista.ColorizationColor);
                                    Window2.AccentColor2_Inactive = Color.FromArgb(100, TM.WindowsVista.ColorizationColor);
                                    Window2.Win7Noise = 100f;
                                    break;
                                }

                            case Theme.Structures.Windows7.Themes.AeroOpaque:
                                {
                                    Window1.Preview = UI.Simulation.Window.Preview_Enum.W7Opaque;
                                    Window1.Win7Alpha = (int)Math.Round(TM.WindowsVista.Alpha / 255d * 100d);
                                    Window1.AccentColor_Active = TM.WindowsVista.ColorizationColor;
                                    Window1.AccentColor_Inactive = TM.WindowsVista.ColorizationColor;
                                    Window1.Win7Noise = 100f;
                                    Window2.Preview = UI.Simulation.Window.Preview_Enum.W7Opaque;
                                    Window2.Win7Alpha = (int)Math.Round(TM.WindowsVista.Alpha / 255d * 100d);
                                    Window2.AccentColor_Active = TM.WindowsVista.ColorizationColor;
                                    Window2.AccentColor_Inactive = TM.WindowsVista.ColorizationColor;
                                    Window2.Win7Noise = 100f;
                                    break;
                                }

                            case Theme.Structures.Windows7.Themes.Basic:
                                {
                                    Window1.Preview = UI.Simulation.Window.Preview_Enum.W7Basic;
                                    Window1.Win7Alpha = 100;
                                    Window1.AccentColor_Active = Color.FromArgb(166, 190, 218);
                                    Window1.Win7Noise = 0f;
                                    Window2.Preview = UI.Simulation.Window.Preview_Enum.W7Basic;
                                    Window2.Win7Alpha = 100;
                                    Window2.AccentColor_Inactive = Color.FromArgb(166, 190, 218);
                                    Window2.Win7Noise = 0f;
                                    break;
                                }

                        }

                        break;
                    }
            }

            Window1.SuspendRefresh = false;
            Window2.SuspendRefresh = false;

            Window1.Invalidate();
            Window2.Invalidate();
        }

        /// <summary>
        /// Select modern or classic theme for preview
        /// </summary>
        /// <param name="TM">WinPaletter theme manager</param>
        /// <param name="Style">Selected Windows edition</param>
        /// <param name="tabs_preview">Tabs control that has 2 tabs, first tab is modern preview and the second is classic preview</param>
        /// <param name="WXP_Alert">AlertBox control that shows alert that WinPaletter is started with classic theme</param>
        public static void AdjustPreview_ModernOrClassic(Theme.Manager TM, WindowStyle Style, UI.WP.TablessControl tabs_preview, UI.WP.AlertBox WXP_Alert)
        {
            if (TM is not null)
            {
                bool condition0 = Style == WindowStyle.W7 && TM.Windows7.Theme == Theme.Structures.Windows7.Themes.Classic;
                bool condition1 = Style == WindowStyle.WVista && TM.WindowsVista.Theme == Theme.Structures.Windows7.Themes.Classic;
                bool condition2 = Style == WindowStyle.WXP && TM.WindowsXP.Theme == Theme.Structures.WindowsXP.Themes.Classic;
                WXP_Alert.Visible = Style == WindowStyle.WXP && Program.StartedWithClassicTheme;
                tabs_preview.SelectedIndex = condition0 | condition1 | condition2 ? 1 : 0;
            }
        }

        /// <summary>
        /// Sets metrics for classic window in preview
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
        /// Sets metrics for window in preview
        /// </summary>
        /// <param name="TM">WinPaletter theme manager</param>
        /// <param name="Window">Window simulation control</param>
        public static void SetModernWindowMetrics(Theme.Manager TM, UI.Simulation.Window Window)
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
        /// Sets colors for classic window in preview
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
        /// Sets colors for classic raised panel in preview
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
        /// Sets colors for classic raised panel in preview
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
        /// Sets colors for classic buttons in preview
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
        /// Refreshes all controls and subcontrols
        /// </summary>
        /// <param name="Parent">Parent that has all controls and subcontrols</param>
        public static void ReValidateLivePreview(Control Parent)
        {
            Parent.Refresh();

            foreach (Control ctrl in Parent.Controls)
            {
                ctrl.Refresh();
                if (ctrl.HasChildren)
                {
                    foreach (Control c in ctrl.Controls)
                        ReValidateLivePreview(c);
                }
            }
        }

        /// <summary>
        /// Apply selected Windows 8.1 start background to main form W81_start button
        /// </summary>
        /// <param name="TM">WinPaletter theme manager</param>
        /// <param name="W81_start">Main form Windows 8.1 start button</param>
        public static void ApplyMetroStartToButton(Theme.Manager TM, UI.WP.Button W81_start)
        {
            switch (TM.Windows81.Start)
            {
                case 1:
                    {
                        W81_start.Image = Forms.Start8Selector.img1.Image.Resize(48, 48);
                        break;
                    }
                case 2:
                    {
                        W81_start.Image = Forms.Start8Selector.img2.Image.Resize(48, 48);
                        break;
                    }
                case 3:
                    {
                        W81_start.Image = Forms.Start8Selector.img3.Image.Resize(48, 48);
                        break;
                    }
                case 4:
                    {
                        W81_start.Image = Forms.Start8Selector.img4.Image.Resize(48, 48);
                        break;
                    }
                case 5:
                    {
                        W81_start.Image = Forms.Start8Selector.img5.Image.Resize(48, 48);
                        break;
                    }
                case 6:
                    {
                        W81_start.Image = Forms.Start8Selector.img6.Image.Resize(48, 48);
                        break;
                    }
                case 7:
                    {
                        W81_start.Image = Forms.Start8Selector.img7.Image.Resize(48, 48);
                        break;
                    }
                case 8:
                    {
                        W81_start.Image = Forms.Start8Selector.img8.Image.Resize(48, 48);
                        break;
                    }
                case 9:
                    {
                        W81_start.Image = Forms.Start8Selector.img9.Image.Resize(48, 48);
                        break;
                    }
                case 10:
                    {
                        W81_start.Image = Forms.Start8Selector.img10.Image.Resize(48, 48);
                        break;
                    }
                case 11:
                    {
                        W81_start.Image = Forms.Start8Selector.img11.Image.Resize(48, 48);
                        break;
                    }
                case 12:
                    {
                        W81_start.Image = Forms.Start8Selector.img12.Image.Resize(48, 48);
                        break;
                    }
                case 13:
                    {
                        W81_start.Image = Forms.Start8Selector.img13.Image.Resize(48, 48);
                        break;
                    }
                case 14:
                    {
                        W81_start.Image = Forms.Start8Selector.img14.Image.Resize(48, 48);
                        break;
                    }
                case 15:
                    {
                        W81_start.Image = Forms.Start8Selector.img15.Image.Resize(48, 48);
                        break;
                    }
                case 16:
                    {
                        W81_start.Image = Forms.Start8Selector.img16.Image.Resize(48, 48);
                        break;
                    }
                case 17:
                    {
                        W81_start.Image = Forms.Start8Selector.img17.Image.Resize(48, 48);
                        break;
                    }
                case 18:
                    {
                        W81_start.Image = Forms.Start8Selector.img18.Image.Resize(48, 48);
                        break;
                    }
                case 19:
                    {
                        W81_start.Image = TM.Windows81.PersonalColors_Background.ToBitmap(new Size(48, 48));
                        break;
                    }
                case 20:
                    {
                        W81_start.Image = Program.Wallpaper.Resize(48, 48);
                        break;
                    }

                default:
                    {
                        W81_start.Image = Forms.Start8Selector.img1.Image.Resize(48, 48);
                        break;
                    }
            }
        }

        /// <summary>
        /// Apply selected Windows 8.1 LogonUI background to main form W8_logonui button
        /// </summary>
        /// <param name="TM">WinPaletter theme manager</param>
        /// <param name="W8_logonui">Main form Windows 8.1 LogonUI button</param>
        public static void ApplyBackLogonUI(Theme.Manager TM, UI.WP.Button W8_logonui)
        {

            switch (TM.Windows81.LogonUI)
            {
                case 0:
                    {
                        W8_logonui.Image = Color.FromArgb(34, 34, 34).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 1:
                    {
                        W8_logonui.Image = Color.FromArgb(34, 34, 34).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 2:
                    {
                        W8_logonui.Image = Color.FromArgb(34, 34, 34).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 3:
                    {
                        W8_logonui.Image = Color.FromArgb(34, 34, 34).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 4:
                    {
                        W8_logonui.Image = Color.FromArgb(42, 27, 0).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 5:
                    {
                        W8_logonui.Image = Color.FromArgb(59, 0, 3).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 6:
                    {
                        W8_logonui.Image = Color.FromArgb(65, 0, 49).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 7:
                    {
                        W8_logonui.Image = Color.FromArgb(41, 0, 66).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 8:
                    {
                        W8_logonui.Image = Color.FromArgb(30, 3, 84).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 9:
                    {
                        W8_logonui.Image = Color.FromArgb(0, 31, 66).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 10:
                    {
                        W8_logonui.Image = Color.FromArgb(3, 66, 82).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 11:
                    {
                        W8_logonui.Image = Color.FromArgb(30, 144, 255).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 12:
                    {
                        W8_logonui.Image = Color.FromArgb(4, 63, 0).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 13:
                    {
                        W8_logonui.Image = Color.FromArgb(188, 90, 28).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 14:
                    {
                        W8_logonui.Image = Color.FromArgb(155, 28, 29).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 15:
                    {
                        W8_logonui.Image = Color.FromArgb(152, 28, 90).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 16:
                    {
                        W8_logonui.Image = Color.FromArgb(88, 28, 152).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 17:
                    {
                        W8_logonui.Image = Color.FromArgb(28, 74, 153).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 18:
                    {
                        W8_logonui.Image = Color.FromArgb(69, 143, 221).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 19:
                    {
                        W8_logonui.Image = Color.FromArgb(0, 141, 142).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 20:
                    {
                        W8_logonui.Image = Color.FromArgb(120, 168, 33).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 21:
                    {
                        W8_logonui.Image = Color.FromArgb(191, 142, 16).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 22:
                    {
                        W8_logonui.Image = Color.FromArgb(219, 80, 171).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 23:
                    {
                        W8_logonui.Image = Color.FromArgb(154, 154, 154).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 24:
                    {
                        W8_logonui.Image = Color.FromArgb(88, 88, 88).ToBitmap(new Size(48, 48));
                        break;
                    }

                default:
                    {
                        W8_logonui.Image = Color.FromArgb(34, 34, 34).ToBitmap(new Size(48, 48));
                        break;
                    }

            }


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
                    WT.Image = PathsExt.Windows + @"\Web\Wallpaper\Bliss.bmp";
                }
                else
                {
                    WT.Image = PathsExt.Windows + @"\Web\Wallpaper\Windows\img0.jpg";
                }
            }

            using (var ImgF = new ImageProcessor.ImageFactory())
            {
                ImgF.Load(WT.Image);
                ImgF.Hue(WT.H, true);
                ImgF.Saturation(WT.S - 100);
                ImgF.Brightness(WT.L - 100);

                return (Bitmap)ImgF.Image.Clone();
            }

        }

        /// <summary>
        /// Return height of a titlebar provided by a font
        /// </summary>
        public static int GetTitlebarTextHeight(Font font)
        {
            string TitleScheme = "ABCabc0123xYz.#";
            int Title_x_Height = (int)Math.Round(TitleScheme.Measure(font).Height);
            int Title_9_Height = (int)Math.Round(TitleScheme.Measure(new Font(font.Name, 9f, font.Style)).Height);
            return Math.Max(0, Title_x_Height - Title_9_Height - 5);
        }
    }
}