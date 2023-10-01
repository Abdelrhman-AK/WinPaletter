using Microsoft.VisualBasic;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using static WinPaletter.CP;

namespace WinPaletter
{

    public class PreviewHelpers
    {
        private static readonly int Steps = 15;
        private static readonly int Delay = 1;

        public enum WindowStyle
        {
            W11,
            W10,
            W81,
            W7,
            WVista,
            WXP
        }
        public static void ApplyWin10xLegends(CP CP, WindowStyle Style, Label lbl1, Label lbl2, Label lbl3, Label lbl4, Label lbl5, Label lbl6, Label lbl7, Label lbl8, Label lbl9, PictureBox pic1, PictureBox pic2, PictureBox pic3, PictureBox pic4, PictureBox pic5, PictureBox pic6, PictureBox pic7, PictureBox pic8, PictureBox pic9)
        {

            if (ExplorerPatcher.IsAllowed())
                My.Env.EP = new ExplorerPatcher();

            switch (Style)
            {
                case WindowStyle.W11:
                    {
                        #region Win11
                        lbl6.Text = My.Env.Lang.CP_11_SomePressedButtons;
                        lbl7.Text = string.Format(My.Env.Lang.CP_UWPBackground, My.Env.Lang.OS_Win11);
                        lbl8.Text = My.Env.Lang.CP_Undefined;
                        lbl9.Text = My.Env.Lang.CP_Undefined;
                        pic5.Image = My.Resources.Mini_Settings_Icons;
                        pic6.Image = My.Resources.Mini_PressedButton;
                        pic7.Image = My.Resources.Mini_UWPDlg;
                        pic8.Image = My.Resources.Mini_Undefined;
                        pic9.Image = My.Resources.Mini_Undefined;

                        switch (!CP.Windows11.WinMode_Light)
                        {
                            case true:   // '''''''''DarkMode
                                {
                                    lbl1.Text = My.Env.Lang.CP_11_StartMenu_Taskbar_AC;
                                    lbl2.Text = My.Env.Lang.CP_11_ACHover_Links;
                                    lbl3.Text = My.Env.Lang.CP_11_Lines_Toggles_Buttons;
                                    lbl4.Text = My.Env.Lang.CP_11_OverflowTray;
                                    lbl5.Text = My.Env.Lang.CP_11_Settings;

                                    pic1.Image = My.Resources.Mini_StartMenu_Taskbar_AC;
                                    pic2.Image = My.Resources.Mini_ACHover_Links;
                                    pic3.Image = My.Resources.Mini_Lines_Toggles_Buttons;
                                    pic4.Image = My.Resources.Mini_Overflow;
                                    break;
                                }
                            case false:   // '''''''''Light
                                {
                                    lbl1.Text = My.Env.Lang.CP_11_Taskbar_ACHover_Links;
                                    lbl2.Text = My.Env.Lang.CP_11_StartMenu_AC;
                                    lbl3.Text = My.Env.Lang.CP_11_UnreadBadge;
                                    lbl4.Text = My.Env.Lang.CP_11_Lines_Toggles_Buttons_Overflow;
                                    lbl5.Text = My.Env.Lang.CP_11_SettingsAndTaskbarAppUnderline;

                                    pic1.Image = My.Resources.Mini_Taskbar;
                                    pic2.Image = My.Resources.Mini_StartMenu_Taskbar_AC;
                                    pic3.Image = My.Resources.Mini_Badge;
                                    pic4.Image = My.Resources.Mini_Lines_Toggles_Buttons;
                                    break;
                                }
                        }

                        if (ExplorerPatcher.IsAllowed())
                        {
                            switch (!CP.Windows11.WinMode_Light)
                            {
                                case true: // '''''''''DarkMode
                                    {

                                        if (My.Env.EP.UseTaskbar10)
                                        {
                                            lbl5.Text = My.Env.Lang.CP_10_Settings_Links_SomeBtns;

                                            if (My.Env.EP.UseStart10)
                                            {
                                                lbl1.Text = My.Env.Lang.CP_10_Taskbar;
                                                pic1.Image = My.Resources.Mini_Taskbar;
                                            }
                                            else
                                            {
                                                lbl1.Text = My.Env.Lang.CP_11_StartMenu_Taskbar_AC;
                                                pic1.Image = My.Resources.Mini_StartMenu_Taskbar_AC;
                                            }

                                            lbl3.Text = My.Env.Lang.CP_EP_ACButton_TaskbarAppLine;
                                            lbl6.Text = My.Env.Lang.CP_10_StartMenuIconHover;

                                            pic3.Image = My.Resources.Mini_AC;
                                            pic5.Image = My.Resources.Mini_Settings_Icons;
                                            pic6.Image = My.Resources.Native11;
                                        }

                                        if (My.Env.EP.UseStart10)
                                        {
                                            lbl4.Text = My.Env.Lang.CP_EP_StartMenu_OverflowMenus;
                                            pic4.Image = My.Resources.Mini_StartMenu;
                                        }

                                        break;
                                    }

                                case false: // '''''''''Light
                                    {

                                        if (My.Env.EP.UseTaskbar10)
                                        {
                                            lbl3.Text = My.Env.Lang.CP_EP_Taskbar_AppUnderline;
                                            lbl5.Text = My.Env.Lang.CP_10_Settings_Links_SomeBtns;
                                            lbl6.Text = My.Env.Lang.CP_10_StartMenuIconHover;

                                            pic3.Image = My.Resources.Mini_TaskbarApp;
                                            pic5.Image = My.Resources.Mini_Settings_Icons;
                                            pic6.Image = My.Resources.Native11;
                                        }

                                        if (My.Env.EP.UseStart10)
                                        {
                                            lbl2.Text = My.Env.Lang.CP_EP_ActionCenterBackground;
                                            lbl4.Text = My.Env.Lang.CP_EP_StartMenu_ActionCenterButtons;
                                            pic2.Image = My.Resources.Mini_AC;
                                            pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC;
                                        }

                                        break;
                                    }

                            }
                        }

                        break;
                    }

                #endregion
                case WindowStyle.W10:
                    {
                        #region Win10
                        lbl9.Text = My.Env.Lang.CP_Undefined;

                        switch (!CP.Windows10.WinMode_Light)
                        {
                            case true: // '''''''''DarkMode
                                {
                                    lbl2.Text = My.Env.Lang.CP_10_ACLinks;
                                    lbl3.Text = My.Env.Lang.CP_10_TaskbarAppUnderline;
                                    lbl5.Text = My.Env.Lang.CP_10_Settings_Links_SomeBtns;
                                    lbl6.Text = My.Env.Lang.CP_10_StartMenuIconHover;
                                    lbl7.Text = string.Format(My.Env.Lang.CP_UWPBackground, My.Env.Lang.OS_Win10);

                                    pic2.Image = My.Resources.Mini_ACHover_Links;
                                    pic3.Image = My.Resources.Mini_TaskbarApp;
                                    pic5.Image = My.Resources.Mini_Settings_Icons;
                                    pic6.Image = My.Resources.Native10;
                                    pic7.Image = My.Resources.Mini_UWPDlg;

                                    if (CP.Windows10.Transparency)
                                    {
                                        lbl1.Text = My.Env.Lang.CP_10_Hamburger;
                                        lbl4.Text = My.Env.Lang.CP_10_StartMenu_AC;
                                        lbl8.Text = My.Env.Lang.CP_10_Taskbar_StartContextMenu;

                                        pic1.Image = My.Resources.Mini_Hamburger;
                                        pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC;
                                        pic8.Image = My.Resources.Mini_Taskbar;

                                        if (CP.Windows10.ApplyAccentOnTaskbar != Structures.Windows10x.AccentTaskbarLevels.None)
                                        {
                                            lbl5.Text = My.Env.Lang.CP_10_Settings_Links_Taskbar_SomeBtns;
                                        }
                                    }

                                    else
                                    {
                                        lbl1.Text = My.Env.Lang.CP_10_Taskbar;
                                        pic1.Image = My.Resources.Mini_Taskbar;
                                        pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC;

                                        if (CP.Windows10.ApplyAccentOnTaskbar != Structures.Windows10x.AccentTaskbarLevels.None)
                                        {
                                            lbl4.Text = My.Env.Lang.CP_10_StartMenu_AC_TaskbarActiveApp;
                                        }
                                        else
                                        {
                                            lbl4.Text = My.Env.Lang.CP_10_StartMenu_AC;
                                        }

                                        lbl8.Text = My.Env.Lang.CP_10_StartContextMenu;
                                        pic8.Image = My.Resources.Mini_StartContextMenu;

                                    }

                                    break;
                                }

                            case false: // '''''''''Light
                                {
                                    if (CP.Windows10.Transparency)
                                    {
                                        lbl1.Text = My.Env.Lang.CP_10_Hamburger;
                                        lbl4.Text = My.Env.Lang.CP_10_StartMenu_AC;
                                        lbl6.Text = My.Env.Lang.CP_10_StartMenuIconHover;
                                        lbl7.Text = string.Format(My.Env.Lang.CP_UWPBackground, My.Env.Lang.OS_Win10);

                                        pic1.Image = My.Resources.Mini_Hamburger;
                                        pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC;
                                        pic5.Image = My.Resources.Mini_Settings_Icons;
                                        pic6.Image = My.Resources.Native10;
                                        pic7.Image = My.Resources.Mini_UWPDlg;
                                        pic8.Image = My.Resources.Mini_Taskbar;

                                        if (CP.Windows10.ApplyAccentOnTaskbar == Structures.Windows10x.AccentTaskbarLevels.None)
                                        {
                                            lbl2.Text = My.Env.Lang.CP_Undefined;
                                            lbl3.Text = My.Env.Lang.CP_Undefined;
                                            lbl5.Text = My.Env.Lang.CP_10_Settings_Links_TaskbarUndeline_SomeBtns;
                                            lbl8.Text = My.Env.Lang.CP_10_Taskbar_ACLinks_StartContextMenu;

                                            pic2.Image = My.Resources.Mini_Undefined;
                                            pic3.Image = My.Resources.Mini_Undefined;
                                        }

                                        else if (CP.Windows10.ApplyAccentOnTaskbar == Structures.Windows10x.AccentTaskbarLevels.Taskbar)
                                        {
                                            lbl2.Text = My.Env.Lang.CP_Undefined;
                                            lbl3.Text = My.Env.Lang.CP_10_TaskbarAppUnderline;
                                            lbl5.Text = My.Env.Lang.CP_10_Settings_Links_SomeBtns;
                                            lbl8.Text = My.Env.Lang.CP_10_Taskbar_ACLinks_StartContextMenu;

                                            pic2.Image = My.Resources.Mini_Undefined;
                                            pic3.Image = My.Resources.Mini_TaskbarApp;
                                        }

                                        else
                                        {
                                            lbl2.Text = My.Env.Lang.CP_10_ACLinks;
                                            lbl3.Text = My.Env.Lang.CP_10_TaskbarAppUnderline;
                                            lbl5.Text = My.Env.Lang.CP_10_Settings_Links_SomeBtns;
                                            lbl8.Text = My.Env.Lang.CP_10_Taskbar_StartContextMenu;

                                            pic2.Image = My.Resources.Mini_ACHover_Links;
                                            pic3.Image = My.Resources.Mini_TaskbarApp;

                                        }
                                    }
                                    else
                                    {
                                        lbl1.Text = My.Env.Lang.CP_10_Taskbar;
                                        lbl6.Text = My.Env.Lang.CP_10_StartMenuIconHover;
                                        lbl7.Text = string.Format(My.Env.Lang.CP_UWPBackground, My.Env.Lang.OS_Win10);

                                        pic1.Image = My.Resources.Mini_Taskbar;
                                        pic6.Image = My.Resources.Native10;
                                        pic7.Image = My.Resources.Mini_UWPDlg;

                                        if (CP.Windows10.ApplyAccentOnTaskbar == Structures.Windows10x.AccentTaskbarLevels.None)
                                        {
                                            lbl2.Text = My.Env.Lang.CP_Undefined;
                                            lbl3.Text = My.Env.Lang.CP_Undefined;
                                            lbl4.Text = My.Env.Lang.CP_10_StartMenu_AC;
                                            lbl5.Text = My.Env.Lang.CP_10_Settings_Links_TaskbarUndeline_SomeBtns;
                                            lbl8.Text = My.Env.Lang.CP_10_ACLinks_StartContextMenu;

                                            pic2.Image = My.Resources.Mini_Undefined;
                                            pic3.Image = My.Resources.Mini_Undefined;
                                            pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC;
                                            pic5.Image = My.Resources.Mini_Settings_Icons;
                                            pic8.Image = My.Resources.Mini_ACHover_Links;
                                        }

                                        else if (CP.Windows10.ApplyAccentOnTaskbar == Structures.Windows10x.AccentTaskbarLevels.Taskbar)
                                        {
                                            lbl2.Text = My.Env.Lang.CP_Undefined;
                                            lbl3.Text = My.Env.Lang.CP_10_TaskbarAppUnderline;
                                            lbl4.Text = My.Env.Lang.CP_10_TaskbarFocusedApp_StartButtonHover;
                                            lbl5.Text = My.Env.Lang.CP_10_Settings_Links_SomeBtns;
                                            lbl8.Text = My.Env.Lang.CP_10_ACLinks_StartContextMenu;

                                            pic2.Image = My.Resources.Mini_Undefined;
                                            pic3.Image = My.Resources.Mini_TaskbarApp;
                                            pic4.Image = My.Resources.Mini_TaskbarActiveIcon;
                                            pic5.Image = My.Resources.Mini_Settings_Icons;
                                            pic8.Image = My.Resources.Mini_ACHover_Links;
                                        }

                                        else
                                        {
                                            lbl2.Text = My.Env.Lang.CP_10_ACLinks;
                                            lbl3.Text = My.Env.Lang.CP_10_TaskbarAppUnderline;
                                            lbl4.Text = My.Env.Lang.CP_10_StartMenu_AC_TaskbarActiveApp;
                                            lbl5.Text = My.Env.Lang.CP_10_Settings_Links_SomeBtns;
                                            lbl8.Text = My.Env.Lang.CP_10_StartContextMenu;

                                            pic2.Image = My.Resources.Mini_ACHover_Links;
                                            pic3.Image = My.Resources.Mini_TaskbarApp;
                                            pic4.Image = My.Resources.Mini_StartMenu_Taskbar_AC;
                                            pic5.Image = My.Resources.Mini_Settings_Icons;
                                            pic8.Image = My.Resources.Mini_StartContextMenu;
                                        }
                                    }

                                    break;
                                }
                        }

                        break;
                    }
                    #endregion
            }

        }
        public static void ApplyWinElementsColors(CP CP, WindowStyle Style, bool AnimateColorChange, UI.Simulation.WinElement Taskbar, UI.Simulation.WinElement Start, UI.Simulation.WinElement ActionCenter, UI.WP.LabelAlt setting_icon_preview, UI.WP.LabelAlt settings_label, UI.WP.LabelAlt Link_preview)
        {

            if (ExplorerPatcher.IsAllowed())
                My.Env.EP = new ExplorerPatcher();

            My.Env.RenderingHint = CP.MetricsFonts.Fonts_SingleBitPP ? TextRenderingHint.SingleBitPerPixelGridFit : TextRenderingHint.ClearTypeGridFit;

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

                        Start.DarkMode = !CP.Windows11.WinMode_Light;
                        Taskbar.DarkMode = !CP.Windows11.WinMode_Light;
                        ActionCenter.DarkMode = !CP.Windows11.WinMode_Light;
                        Taskbar.Transparency = CP.Windows11.Transparency;
                        Start.Transparency = CP.Windows11.Transparency;
                        ActionCenter.Transparency = CP.Windows11.Transparency;

                        switch (!CP.Windows11.WinMode_Light)
                        {
                            case true:   // DarkMode
                                {
                                    AC_Alpha = 90;

                                    if (ExplorerPatcher.IsAllowed())
                                    {
                                        if (My.Env.EP.UseStart10)
                                        {
                                            S_Alpha = 185;
                                        }
                                        else
                                        {
                                            S_Alpha = 90;
                                        }

                                        if (My.Env.EP.UseTaskbar10)
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

                                    switch (CP.Windows11.ApplyAccentOnTaskbar)
                                    {
                                        case Structures.Windows10x.AccentTaskbarLevels.None:
                                            {
                                                TB_Color = Color.FromArgb(28, 28, 28);
                                                S_Color = Color.FromArgb(28, 28, 28);
                                                AC_Color = Color.FromArgb(28, 28, 28);
                                                break;
                                            }

                                        case Structures.Windows10x.AccentTaskbarLevels.Taskbar:
                                            {
                                                TB_Color = Color.FromArgb(Taskbar.Background.A, CP.Windows11.Color_Index5);
                                                S_Color = Color.FromArgb(28, 28, 28);
                                                AC_Color = Color.FromArgb(28, 28, 28);
                                                break;
                                            }

                                        case Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC:
                                            {
                                                TB_Color = Color.FromArgb(Taskbar.Background.A, CP.Windows11.Color_Index5);

                                                if (ExplorerPatcher.IsAllowed() & My.Env.EP.UseStart10)
                                                {
                                                    S_Color = Color.FromArgb(Start.Background.A, CP.Windows11.Color_Index4);
                                                }
                                                else
                                                {
                                                    S_Color = Color.FromArgb(Start.Background.A, CP.Windows11.Color_Index5);
                                                }

                                                AC_Color = Color.FromArgb(ActionCenter.Background.A, CP.Windows11.Color_Index5);
                                                break;
                                            }

                                    }

                                    AC_Normal = CP.Windows11.Color_Index1;
                                    AC_Hover = CP.Windows11.Color_Index0;
                                    AC_Pressed = CP.Windows11.Color_Index2;
                                    TB_UL_Color = CP.Windows11.Color_Index1;
                                    Settings_Label_Color = CP.Windows11.Color_Index3;
                                    Link_preview_Color = CP.Windows11.Color_Index0;
                                    break;
                                }

                            case false:   // Light
                                {
                                    AC_Alpha = 180;

                                    if (ExplorerPatcher.IsAllowed())
                                    {
                                        if (My.Env.EP.UseStart10)
                                        {
                                            S_Alpha = 210;
                                        }
                                        else
                                        {
                                            S_Alpha = 180;
                                        }

                                        if (My.Env.EP.UseTaskbar10)
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

                                    switch (CP.Windows11.ApplyAccentOnTaskbar)
                                    {
                                        case Structures.Windows10x.AccentTaskbarLevels.None:
                                            {
                                                TB_Color = Color.FromArgb(255, 255, 255);
                                                S_Color = Color.FromArgb(255, 255, 255);
                                                AC_Color = Color.FromArgb(255, 255, 255);
                                                break;
                                            }

                                        case Structures.Windows10x.AccentTaskbarLevels.Taskbar:
                                            {
                                                TB_Color = Color.FromArgb(Taskbar.Background.A, CP.Windows11.Color_Index5);
                                                S_Color = Color.FromArgb(255, 255, 255);
                                                AC_Color = Color.FromArgb(255, 255, 255);
                                                break;
                                            }

                                        case Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC:
                                            {
                                                TB_Color = Color.FromArgb(Taskbar.Background.A, CP.Windows11.Color_Index5);

                                                if (ExplorerPatcher.IsAllowed() & My.Env.EP.UseStart10)
                                                {
                                                    S_Color = Color.FromArgb(Start.Background.A, CP.Windows11.Color_Index4);
                                                }
                                                else
                                                {
                                                    S_Color = Color.FromArgb(Start.Background.A, CP.Windows11.Color_Index0);
                                                }

                                                AC_Color = Color.FromArgb(ActionCenter.Background.A, CP.Windows11.Color_Index0);
                                                break;
                                            }

                                    }

                                    AC_Normal = CP.Windows11.Color_Index4;
                                    AC_Hover = CP.Windows11.Color_Index5;
                                    AC_Pressed = CP.Windows11.Color_Index2;

                                    if (ExplorerPatcher.IsAllowed() & My.Env.EP.UseTaskbar10)
                                    {
                                        TB_UL_Color = CP.Windows11.Color_Index1;
                                    }
                                    else
                                    {
                                        TB_UL_Color = CP.Windows11.Color_Index3;
                                    }

                                    Settings_Label_Color = CP.Windows11.Color_Index3;
                                    Link_preview_Color = CP.Windows11.Color_Index5;
                                    break;
                                }
                        }

                        ActionCenter.BackColorAlpha = AC_Alpha;
                        Start.BackColorAlpha = S_Alpha;
                        Taskbar.BackColorAlpha = TB_Alpha;
                        Taskbar.BlurPower = TB_Blur;

                        if (AnimateColorChange)
                        {
                            Visual.FadeColor(Taskbar, "Background", Taskbar.Background, TB_Color, Steps, Delay);
                            Visual.FadeColor(Start, "Background", Start.Background, S_Color, Steps, Delay);
                            Visual.FadeColor(ActionCenter, "Background", ActionCenter.Background, AC_Color, Steps, Delay);
                            Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, AC_Normal, Steps, Delay);
                            Visual.FadeColor(ActionCenter, "ActionCenterButton_Hover", ActionCenter.ActionCenterButton_Hover, AC_Hover, Steps, Delay);
                            Visual.FadeColor(ActionCenter, "ActionCenterButton_Pressed", ActionCenter.ActionCenterButton_Pressed, AC_Pressed, Steps, Delay);
                            Visual.FadeColor(Taskbar, "AppUnderline", Taskbar.AppUnderline, TB_UL_Color, Steps, Delay);
                            Visual.FadeColor(setting_icon_preview, "ForeColor", setting_icon_preview.ForeColor, Settings_Label_Color, Steps, Delay);
                            Visual.FadeColor(Link_preview, "ForeColor", Link_preview.ForeColor, Link_preview_Color, Steps, Delay);
                            Visual.FadeColor(settings_label, "ForeColor", settings_label.ForeColor, CP.Windows11.AppMode_Light ? Color.Black : Color.White, Steps, Delay);
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
                            settings_label.ForeColor = CP.Windows11.AppMode_Light ? Color.Black : Color.White;
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

                        Start.DarkMode = !CP.Windows10.WinMode_Light;
                        Taskbar.DarkMode = !CP.Windows10.WinMode_Light;
                        ActionCenter.DarkMode = !CP.Windows10.WinMode_Light;
                        Taskbar.Transparency = CP.Windows10.Transparency;
                        Start.Transparency = CP.Windows10.Transparency && CP.Windows10.TB_Blur;
                        ActionCenter.Transparency = CP.Windows10.Transparency && CP.Windows10.TB_Blur;

                        if (!CP.Windows10.TB_Blur)
                        {
                            TB_Blur = 0;
                        }
                        else
                        {
                            TB_Blur = (byte)(!CP.Windows10.IncreaseTBTransparency ? 8 : 6);
                        }

                        if (CP.Windows10.Transparency)
                        {
                            if (!CP.Windows10.WinMode_Light)
                            {
                                TB_Alpha = (byte)(!CP.Windows10.IncreaseTBTransparency ? 150 : 75);
                                S_Alpha = 150;
                                AC_Alpha = 150;
                            }
                            else
                            {
                                TB_Alpha = (byte)(!CP.Windows10.IncreaseTBTransparency ? 200 : 125);
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

                        switch (!CP.Windows10.WinMode_Light)
                        {
                            case true:
                                {

                                    if (CP.Windows10.Transparency)
                                    {
                                        switch (CP.Windows10.ApplyAccentOnTaskbar)
                                        {
                                            case Structures.Windows10x.AccentTaskbarLevels.None:
                                                {
                                                    TB_Color = Color.FromArgb(16, 16, 16);
                                                    TB_StartBtnColor = Color.FromArgb(150, 150, 150, 150);
                                                    S_Color = Color.FromArgb(31, 31, 31);
                                                    AC_Color = Color.FromArgb(31, 31, 31);

                                                    TB_AppBack_Color = Color.FromArgb(150, 150, 150, 150);
                                                    AC_LinkColor = CP.Windows10.Color_Index0;
                                                    TB_UL_Color = CP.Windows10.Color_Index1;
                                                    Settings_Label_Color = CP.Windows10.Color_Index3;
                                                    Link_preview_Color = CP.Windows10.Color_Index3;
                                                    AC_Normal = CP.Windows10.Color_Index3;
                                                    break;
                                                }

                                            case Structures.Windows10x.AccentTaskbarLevels.Taskbar:
                                                {
                                                    TB_Color = CP.Windows10.Color_Index6;
                                                    TB_StartBtnColor = Color.FromArgb(0, 0, 0, 0);
                                                    S_Color = Color.FromArgb(31, 31, 31);
                                                    AC_Color = Color.FromArgb(31, 31, 31);

                                                    TB_AppBack_Color = Color.FromArgb(150, CP.Windows10.Color_Index3);
                                                    AC_LinkColor = CP.Windows10.Color_Index0;
                                                    TB_UL_Color = CP.Windows10.Color_Index1;
                                                    Settings_Label_Color = CP.Windows10.Color_Index3;
                                                    Link_preview_Color = CP.Windows10.Color_Index3;
                                                    AC_Normal = CP.Windows10.Color_Index3;
                                                    break;
                                                }

                                            case Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC:
                                                {
                                                    TB_Color = CP.Windows10.Color_Index6;
                                                    TB_StartBtnColor = Color.FromArgb(0, 0, 0, 0);
                                                    S_Color = CP.Windows10.Color_Index4;
                                                    AC_Color = CP.Windows10.Color_Index4;

                                                    TB_AppBack_Color = Color.FromArgb(150, CP.Windows10.Color_Index3);
                                                    AC_LinkColor = CP.Windows10.Color_Index0;
                                                    TB_UL_Color = CP.Windows10.Color_Index1;
                                                    Settings_Label_Color = CP.Windows10.Color_Index3;
                                                    Link_preview_Color = CP.Windows10.Color_Index3;
                                                    AC_Normal = CP.Windows10.Color_Index3;
                                                    break;
                                                }

                                        }
                                    }

                                    else
                                    {
                                        switch (CP.Windows10.ApplyAccentOnTaskbar)
                                        {
                                            case Structures.Windows10x.AccentTaskbarLevels.None:
                                                {
                                                    TB_Color = Color.FromArgb(16, 16, 16);
                                                    TB_StartBtnColor = Color.FromArgb(31, 31, 31);
                                                    S_Color = Color.FromArgb(31, 31, 31);
                                                    AC_Color = Color.FromArgb(31, 31, 31);
                                                    break;
                                                }

                                            case Structures.Windows10x.AccentTaskbarLevels.Taskbar:
                                                {
                                                    TB_Color = CP.Windows10.Color_Index5;
                                                    TB_StartBtnColor = CP.Windows10.Color_Index4;
                                                    S_Color = Color.FromArgb(31, 31, 31);
                                                    AC_Color = Color.FromArgb(31, 31, 31);
                                                    break;
                                                }

                                            case Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC:
                                                {
                                                    TB_Color = CP.Windows10.Color_Index5;
                                                    TB_StartBtnColor = CP.Windows10.Color_Index4;
                                                    S_Color = CP.Windows10.Color_Index4;
                                                    AC_Color = CP.Windows10.Color_Index4;
                                                    break;
                                                }
                                        }

                                        if (CP.Windows10.ApplyAccentOnTaskbar == Structures.Windows10x.AccentTaskbarLevels.None)
                                        {
                                            TB_AppBack_Color = Color.FromArgb(150, 100, 100, 100);
                                        }
                                        else
                                        {
                                            TB_AppBack_Color = CP.Windows10.Color_Index4;
                                        }

                                        AC_LinkColor = CP.Windows10.Color_Index0;
                                        TB_UL_Color = CP.Windows10.Color_Index1;
                                        Settings_Label_Color = CP.Windows10.Color_Index3;
                                        Link_preview_Color = CP.Windows10.Color_Index3;
                                        AC_Normal = CP.Windows10.Color_Index3;

                                    }

                                    break;
                                }

                            case false:
                                {
                                    if (CP.Windows10.Transparency)
                                    {

                                        switch (CP.Windows10.ApplyAccentOnTaskbar)
                                        {
                                            case Structures.Windows10x.AccentTaskbarLevels.None:
                                                {
                                                    TB_Color = Color.FromArgb(238, 238, 238);
                                                    TB_StartBtnColor = Color.Transparent;
                                                    S_Color = Color.FromArgb(228, 228, 228);
                                                    AC_Color = Color.FromArgb(228, 228, 228);

                                                    TB_AppBack_Color = Color.FromArgb(150, 238, 238, 238);
                                                    AC_LinkColor = CP.Windows10.Color_Index6;
                                                    TB_UL_Color = CP.Windows10.Color_Index3;
                                                    Settings_Label_Color = CP.Windows10.Color_Index3;
                                                    Link_preview_Color = CP.Windows10.Color_Index3;
                                                    AC_Normal = CP.Windows10.Color_Index3;
                                                    break;
                                                }

                                            case Structures.Windows10x.AccentTaskbarLevels.Taskbar:
                                                {
                                                    TB_Color = CP.Windows10.Color_Index6;
                                                    TB_StartBtnColor = Color.Transparent;
                                                    S_Color = Color.FromArgb(228, 228, 228);
                                                    AC_Color = Color.FromArgb(228, 228, 228);

                                                    TB_AppBack_Color = Color.FromArgb(150, CP.Windows10.Color_Index3);
                                                    AC_LinkColor = CP.Windows10.Color_Index6;
                                                    TB_UL_Color = CP.Windows10.Color_Index1;
                                                    Settings_Label_Color = CP.Windows10.Color_Index3;
                                                    Link_preview_Color = CP.Windows10.Color_Index3;
                                                    AC_Normal = CP.Windows10.Color_Index3;
                                                    break;
                                                }

                                            case Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC:
                                                {
                                                    TB_Color = CP.Windows10.Color_Index6;
                                                    TB_StartBtnColor = Color.Transparent;
                                                    S_Color = CP.Windows10.Color_Index4;
                                                    AC_Color = CP.Windows10.Color_Index4;

                                                    TB_AppBack_Color = Color.FromArgb(150, CP.Windows10.Color_Index3);
                                                    AC_LinkColor = CP.Windows10.Color_Index0;
                                                    TB_UL_Color = CP.Windows10.Color_Index1;
                                                    Settings_Label_Color = CP.Windows10.Color_Index3;
                                                    Link_preview_Color = CP.Windows10.Color_Index3;
                                                    AC_Normal = CP.Windows10.Color_Index3;
                                                    break;
                                                }

                                        }
                                    }

                                    else
                                    {

                                        switch (CP.Windows10.ApplyAccentOnTaskbar)
                                        {
                                            case Structures.Windows10x.AccentTaskbarLevels.None:
                                                {
                                                    TB_Color = Color.FromArgb(238, 238, 238);
                                                    TB_StartBtnColor = Color.FromArgb(241, 241, 241);
                                                    S_Color = Color.FromArgb(228, 228, 228);
                                                    AC_Color = Color.FromArgb(228, 228, 228);

                                                    TB_AppBack_Color = Color.FromArgb(252, 252, 252);
                                                    AC_LinkColor = CP.Windows10.Color_Index6;
                                                    TB_UL_Color = CP.Windows10.Color_Index3;
                                                    Settings_Label_Color = CP.Windows10.Color_Index3;
                                                    Link_preview_Color = CP.Windows10.Color_Index3;
                                                    AC_Normal = CP.Windows10.Color_Index3;
                                                    break;
                                                }

                                            case Structures.Windows10x.AccentTaskbarLevels.Taskbar:
                                                {
                                                    TB_Color = CP.Windows10.Color_Index5;
                                                    TB_StartBtnColor = CP.Windows10.Color_Index4;
                                                    S_Color = Color.FromArgb(228, 228, 228);
                                                    AC_Color = Color.FromArgb(228, 228, 228);

                                                    TB_AppBack_Color = CP.Windows10.Color_Index4;
                                                    AC_LinkColor = CP.Windows10.Color_Index6;
                                                    TB_UL_Color = CP.Windows10.Color_Index1;
                                                    Settings_Label_Color = CP.Windows10.Color_Index3;
                                                    Link_preview_Color = CP.Windows10.Color_Index3;
                                                    AC_Normal = CP.Windows10.Color_Index3;
                                                    break;
                                                }

                                            case Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC:
                                                {
                                                    TB_Color = CP.Windows10.Color_Index5;
                                                    TB_StartBtnColor = CP.Windows10.Color_Index4;
                                                    S_Color = CP.Windows10.Color_Index4;
                                                    AC_Color = CP.Windows10.Color_Index4;

                                                    TB_AppBack_Color = CP.Windows10.Color_Index4;
                                                    AC_LinkColor = CP.Windows10.Color_Index0;
                                                    TB_UL_Color = CP.Windows10.Color_Index1;
                                                    Settings_Label_Color = CP.Windows10.Color_Index3;
                                                    Link_preview_Color = CP.Windows10.Color_Index3;
                                                    AC_Normal = CP.Windows10.Color_Index3;
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
                            Visual.FadeColor(Taskbar, "Background", Taskbar.Background, TB_Color, Steps, Delay);
                            Visual.FadeColor(Taskbar, "StartColor", Taskbar.StartColor, TB_StartBtnColor, Steps, Delay);
                            Visual.FadeColor(Start, "Background", Start.Background, S_Color, Steps, Delay);
                            Visual.FadeColor(ActionCenter, "Background", ActionCenter.Background, AC_Color, Steps, Delay);
                            Visual.FadeColor(Taskbar, "AppBackground", Taskbar.AppBackground, TB_AppBack_Color, Steps, Delay);
                            Visual.FadeColor(ActionCenter, "LinkColor", ActionCenter.LinkColor, AC_LinkColor, Steps, Delay);
                            Visual.FadeColor(Taskbar, "AppUnderline", Taskbar.AppUnderline, TB_UL_Color, Steps, Delay);
                            Visual.FadeColor(setting_icon_preview, "ForeColor", setting_icon_preview.ForeColor, Settings_Label_Color, Steps, Delay);
                            Visual.FadeColor(Link_preview, "ForeColor", Link_preview.ForeColor, Link_preview_Color, Steps, Delay);
                            Visual.FadeColor(ActionCenter, "ActionCenterButton_Normal", ActionCenter.ActionCenterButton_Normal, AC_Normal, Steps, Delay);
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
                        switch (CP.Windows81.Theme)
                        {
                            case Structures.Windows7.Themes.Aero:
                                {
                                    Taskbar.Transparency = true;
                                    Taskbar.BackColorAlpha = 100;
                                    break;
                                }
                            case Structures.Windows7.Themes.AeroLite:
                                {
                                    Taskbar.Transparency = false;
                                    Taskbar.BackColorAlpha = 255;
                                    break;
                                }
                        }

                        if (AnimateColorChange)
                        {
                            Visual.FadeColor(Taskbar, "Background", Taskbar.Background, CP.Windows81.ColorizationColor, Steps, Delay);
                        }
                        else
                        {
                            Taskbar.Background = CP.Windows81.ColorizationColor;
                        }

                        Taskbar.Win7ColorBal = CP.Windows81.ColorizationColorBalance;
                        break;
                    }
                #endregion

                case WindowStyle.W7:
                    {
                        #region Win7
                        Start.Transparency = !(CP.Windows7.Theme == Structures.Windows7.Themes.Basic) & !(CP.Windows7.Theme == Structures.Windows7.Themes.Classic);
                        Taskbar.Transparency = !(CP.Windows7.Theme == Structures.Windows7.Themes.Basic) & !(CP.Windows7.Theme == Structures.Windows7.Themes.Classic);

                        switch (CP.Windows7.Theme)
                        {
                            case Structures.Windows7.Themes.Aero:
                                {
                                    Start.BackColorAlpha = CP.Windows7.ColorizationBlurBalance;
                                    Start.Win7ColorBal = CP.Windows7.ColorizationColorBalance;
                                    Start.Win7GlowBal = CP.Windows7.ColorizationAfterglowBalance;
                                    Start.Background = CP.Windows7.ColorizationColor;
                                    Start.Background2 = CP.Windows7.ColorizationAfterglow;
                                    Start.NoisePower = CP.Windows7.ColorizationGlassReflectionIntensity;
                                    Taskbar.BackColorAlpha = CP.Windows7.ColorizationBlurBalance;
                                    Taskbar.Win7ColorBal = CP.Windows7.ColorizationColorBalance;
                                    Taskbar.Win7GlowBal = CP.Windows7.ColorizationAfterglowBalance;
                                    Taskbar.Background = CP.Windows7.ColorizationColor;
                                    Taskbar.Background2 = CP.Windows7.ColorizationAfterglow;
                                    Taskbar.NoisePower = CP.Windows7.ColorizationGlassReflectionIntensity;
                                    break;
                                }

                            case Structures.Windows7.Themes.AeroOpaque:
                                {
                                    Taskbar.BackColorAlpha = CP.Windows7.ColorizationColorBalance;
                                    Taskbar.Background = CP.Windows7.ColorizationColor;
                                    Taskbar.Background2 = CP.Windows7.ColorizationColor;
                                    Taskbar.NoisePower = CP.Windows7.ColorizationGlassReflectionIntensity;
                                    Start.BackColorAlpha = CP.Windows7.ColorizationColorBalance;
                                    Start.Background = CP.Windows7.ColorizationColor;
                                    Start.Background2 = CP.Windows7.ColorizationColor;
                                    Start.NoisePower = CP.Windows7.ColorizationGlassReflectionIntensity;
                                    break;
                                }

                            case Structures.Windows7.Themes.Basic:
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
                        Start.Transparency = !(CP.WindowsVista.Theme == Structures.Windows7.Themes.Basic) & !(CP.WindowsVista.Theme == Structures.Windows7.Themes.Classic);
                        Taskbar.Transparency = !(CP.WindowsVista.Theme == Structures.Windows7.Themes.Basic) & !(CP.WindowsVista.Theme == Structures.Windows7.Themes.Classic);

                        switch (CP.WindowsVista.Theme)
                        {
                            case Structures.Windows7.Themes.Aero:
                                {
                                    Start.BackColorAlpha = (int)Math.Round(CP.WindowsVista.Alpha / 255d * 180d);
                                    Start.Win7ColorBal = (int)Math.Round((255 - CP.WindowsVista.Alpha) / 255d * 100d);
                                    // .Win7GlowBal = [CP].WindowsVista.ColorizationAfterglowBalance
                                    Start.Background = CP.WindowsVista.ColorizationColor;
                                    Start.Background2 = CP.WindowsVista.ColorizationColor;
                                    Start.NoisePower = 100f;
                                    Taskbar.BackColorAlpha = (int)Math.Round(CP.WindowsVista.Alpha / 255d * 180d);
                                    Taskbar.Win7ColorBal = (int)Math.Round((255 - CP.WindowsVista.Alpha) / 255d * 100d);
                                    // .Win7GlowBal = [CP].WindowsVista.ColorizationAfterglowBalance
                                    Taskbar.Background = CP.WindowsVista.ColorizationColor;
                                    Taskbar.Background2 = CP.WindowsVista.ColorizationColor;
                                    Taskbar.NoisePower = 100f;
                                    break;
                                }


                            case Structures.Windows7.Themes.AeroOpaque:
                                {
                                    Taskbar.BackColorAlpha = (int)Math.Round(CP.WindowsVista.Alpha / 255d * 200d);
                                    Taskbar.Background = CP.WindowsVista.ColorizationColor;
                                    Taskbar.Background2 = CP.WindowsVista.ColorizationColor;
                                    Taskbar.NoisePower = 100f;
                                    Start.BackColorAlpha = (int)Math.Round(CP.WindowsVista.Alpha / 255d * 200d);
                                    Start.Background = CP.WindowsVista.ColorizationColor;
                                    Start.Background2 = CP.WindowsVista.ColorizationColor;
                                    Start.NoisePower = 100f;
                                    break;
                                }

                            case Structures.Windows7.Themes.Basic:
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

            if (!IsFontInstalled("Segoe MDL2 Assets"))
            {
                setting_icon_preview.Font = new Font("Arial", 28f, FontStyle.Regular);
                setting_icon_preview.Text = "♣";
            }

        }
        public static void ApplyWinElementsStyle(CP CP, WindowStyle Style, UI.Simulation.WinElement Taskbar, UI.Simulation.WinElement Start, UI.Simulation.WinElement ActionCenter, UI.Simulation.Window Window1, UI.Simulation.Window Window2, Panel Settings_Container, Label Link_preview, UI.Retro.PanelRaisedR ClassicTaskbar, UI.Retro.ButtonR ClassicStartButton, UI.Retro.ButtonR ClassicAppButton1, UI.Retro.ButtonR ClassicAppButton2, UI.Retro.WindowR ClassicWindow1, UI.Retro.WindowR ClassicWindow2, bool WXP_VS_ReplaceColors, bool WXP_VS_ReplaceMetrics, bool WXP_VS_ReplaceFonts)
        {
            My.Env.RenderingHint = CP.MetricsFonts.Fonts_SingleBitPP ? TextRenderingHint.SingleBitPerPixelGridFit : TextRenderingHint.ClearTypeGridFit;

            Taskbar.SuspendRefresh = true;
            Start.SuspendRefresh = true;
            ActionCenter.SuspendRefresh = true;
            Window1.SuspendRefresh = true;
            Window2.SuspendRefresh = true;

            var AC_Style = ActionCenter.Style;
            var Start_Style = Start.Style;
            var Taskbar_Style = Taskbar.Style;
            var Window_Style = Window1.Preview;

            Settings_Container.Visible = Style == WindowStyle.W11 | Style == WindowStyle.W10;
            Link_preview.Visible = Style == WindowStyle.W11 | Style == WindowStyle.W10;
            Start.Visible = !(Style == WindowStyle.W81) & !(Style == WindowStyle.W10 & CP.WindowsEffects.FullScreenStartMenu);
            ActionCenter.Visible = Style == WindowStyle.W11 | Style == WindowStyle.W10;

            switch (Style)
            {
                case WindowStyle.W11:
                    {
                        Window_Style = UI.Simulation.Window.Preview_Enum.W11;

                        AC_Style = UI.Simulation.WinElement.Styles.ActionCenter11;

                        if (ExplorerPatcher.IsAllowed())
                        {
                            {
                                ref var temp = ref My.Env.EP;
                                if (!temp.UseStart10)
                                {
                                    Start_Style = UI.Simulation.WinElement.Styles.Start11;
                                }
                                else
                                {
                                    Start_Style = UI.Simulation.WinElement.Styles.Start10;
                                }

                                if (!temp.UseTaskbar10)
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
                        Window_Style = CP.Windows81.Theme == Structures.Windows7.Themes.AeroLite ? UI.Simulation.Window.Preview_Enum.W8Lite : UI.Simulation.Window.Preview_Enum.W8;
                        Taskbar_Style = CP.Windows81.Theme == Structures.Windows7.Themes.Aero ? UI.Simulation.WinElement.Styles.Taskbar8Aero : UI.Simulation.WinElement.Styles.Taskbar8Lite;
                        break;
                    }

                case WindowStyle.W7:
                    {
                        switch (CP.Windows7.Theme)
                        {
                            case Structures.Windows7.Themes.Aero:
                                {
                                    Window_Style = UI.Simulation.Window.Preview_Enum.W7Aero;
                                    Taskbar_Style = UI.Simulation.WinElement.Styles.Taskbar7Aero;
                                    Start_Style = UI.Simulation.WinElement.Styles.Start7Aero;
                                    break;
                                }

                            case Structures.Windows7.Themes.AeroOpaque:
                                {
                                    Window_Style = UI.Simulation.Window.Preview_Enum.W7Opaque;
                                    Taskbar_Style = UI.Simulation.WinElement.Styles.Taskbar7Opaque;
                                    Start_Style = UI.Simulation.WinElement.Styles.Start7Opaque;
                                    break;
                                }

                            case Structures.Windows7.Themes.Basic:
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
                        switch (CP.WindowsVista.Theme)     // Windows Vista uses the same aero of Windows 7
                        {
                            case Structures.Windows7.Themes.Aero:
                                {
                                    Window_Style = UI.Simulation.Window.Preview_Enum.W7Aero;
                                    Taskbar_Style = UI.Simulation.WinElement.Styles.TaskbarVistaAero;
                                    Start_Style = UI.Simulation.WinElement.Styles.StartVistaAero;
                                    break;
                                }

                            case Structures.Windows7.Themes.AeroOpaque:
                                {
                                    Window_Style = UI.Simulation.Window.Preview_Enum.W7Opaque;
                                    Taskbar_Style = UI.Simulation.WinElement.Styles.TaskbarVistaOpaque;
                                    Start_Style = UI.Simulation.WinElement.Styles.StartVistaOpaque;
                                    break;
                                }

                            case Structures.Windows7.Themes.Basic:
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

                        switch (CP.WindowsXP.Theme)
                        {
                            case Structures.WindowsXP.Themes.LunaBlue:
                                {
                                    My.Env.VS = My.Env.PATH_appData + @"\VisualStyles\Luna\luna.theme";
                                    System.IO.File.WriteAllText(My.Env.PATH_appData + @"\VisualStyles\Luna\luna.theme", string.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=NormalColor{1}Size=NormalSize", My.Env.PATH_appData + @"\VisualStyles\Luna\luna.msstyles", "\r\n"));
                                    My.Env.resVS = new VisualStylesRes(My.Env.VS);
                                    break;
                                }

                            case Structures.WindowsXP.Themes.LunaOliveGreen:
                                {
                                    My.Env.VS = My.Env.PATH_appData + @"\VisualStyles\Luna\luna.theme";
                                    System.IO.File.WriteAllText(My.Env.PATH_appData + @"\VisualStyles\Luna\luna.theme", string.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=HomeStead{1}Size=NormalSize", My.Env.PATH_appData + @"\VisualStyles\Luna\luna.msstyles", "\r\n"));
                                    My.Env.resVS = new VisualStylesRes(My.Env.VS);
                                    break;
                                }

                            case Structures.WindowsXP.Themes.LunaSilver:
                                {
                                    My.Env.VS = My.Env.PATH_appData + @"\VisualStyles\Luna\luna.theme";
                                    System.IO.File.WriteAllText(My.Env.PATH_appData + @"\VisualStyles\Luna\luna.theme", string.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=Metallic{1}Size=NormalSize", My.Env.PATH_appData + @"\VisualStyles\Luna\luna.msstyles", "\r\n"));
                                    My.Env.resVS = new VisualStylesRes(My.Env.VS);
                                    break;
                                }

                            case Structures.WindowsXP.Themes.Custom:
                                {
                                    if (System.IO.File.Exists(CP.WindowsXP.ThemeFile))
                                    {
                                        if (System.IO.Path.GetExtension(CP.WindowsXP.ThemeFile) == ".theme")
                                        {
                                            My.Env.VS = CP.WindowsXP.ThemeFile;
                                        }
                                        else if (System.IO.Path.GetExtension(CP.WindowsXP.ThemeFile) == ".msstyles")
                                        {
                                            My.Env.VS = My.Env.PATH_appData + @"\VisualStyles\Luna\luna.theme";
                                            System.IO.File.WriteAllText(My.Env.PATH_appData + @"\VisualStyles\Luna\luna.theme", string.Format("[VisualStyles]{1}Path={0}{1}ColorStyle={2}{1}Size=NormalSize", CP.WindowsXP.ThemeFile, "\r\n", CP.WindowsXP.ColorScheme));
                                        }
                                    }
                                    My.Env.resVS = new VisualStylesRes(My.Env.VS);
                                    break;
                                }

                            case Structures.WindowsXP.Themes.Classic:
                                {
                                    My.Env.VS = My.Env.PATH_appData + @"\VisualStyles\Luna\luna.theme";
                                    System.IO.File.WriteAllText(My.Env.PATH_appData + @"\VisualStyles\Luna\luna.theme", string.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=NormalColor{1}Size=NormalSize", My.Env.PATH_appData + @"\VisualStyles\Luna\luna.msstyles", "\r\n"));
                                    My.Env.resVS = new VisualStylesRes(My.Env.VS);
                                    break;
                                }

                        }

                        if (WXP_VS_ReplaceColors & CP.WindowsXP.Theme != Structures.WindowsXP.Themes.Classic)
                        {
                            if (System.IO.File.Exists(My.Env.VS) & !string.IsNullOrEmpty(My.Env.VS))
                            {
                                var vs = new Devcorp.Controls.VisualStyles.VisualStyleFile(My.Env.VS);
                                CP.Win32.Load(Structures.Win32UI.Method.VisualStyles, vs.Metrics);
                            }
                        }

                        if (WXP_VS_ReplaceMetrics & CP.WindowsXP.Theme != Structures.WindowsXP.Themes.Classic)
                        {
                            if (System.IO.File.Exists(My.Env.VS) & !string.IsNullOrEmpty(My.Env.VS))
                            {
                                var vs = new Devcorp.Controls.VisualStyles.VisualStyleFile(My.Env.VS);
                                CP.MetricsFonts.Overwrite_Metrics(vs.Metrics);
                            }
                        }

                        if (WXP_VS_ReplaceFonts & CP.WindowsXP.Theme != Structures.WindowsXP.Themes.Classic)
                        {
                            if (System.IO.File.Exists(My.Env.VS) & !string.IsNullOrEmpty(My.Env.VS))
                            {
                                var vs = new Devcorp.Controls.VisualStyles.VisualStyleFile(My.Env.VS);
                                CP.MetricsFonts.Overwrite_Fonts(vs.Metrics);
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

            SetModernWindowMetrics(CP, Window1);
            SetModernWindowMetrics(CP, Window2);
            SetClassicWindowMetrics(CP, ClassicWindow1);
            SetClassicWindowMetrics(CP, ClassicWindow2);
            SetClassicWindowColors(CP, ClassicWindow1);
            SetClassicWindowColors(CP, ClassicWindow2, false);
            SetClassicButtonColors(CP, ClassicStartButton);
            SetClassicButtonColors(CP, ClassicAppButton1);
            SetClassicButtonColors(CP, ClassicAppButton2);
            SetClassicPanelRaisedRColors(CP, ClassicTaskbar);

            if (Style != WindowStyle.WVista & Style != WindowStyle.WXP)
            {
                ClassicTaskbar.Height = 44;
                ClassicAppButton1.Image = My.Resources.SampleApp_Active;
                ClassicAppButton2.Image = My.Resources.SampleApp_Inactive;
                ClassicStartButton.Image = My.Resources.Native7.Resize(18, 16);
                ClassicAppButton1.ImageAlign = ContentAlignment.MiddleCenter;
                ClassicAppButton2.ImageAlign = ContentAlignment.MiddleCenter;
                ClassicAppButton1.Width = 48;
                ClassicAppButton2.Width = 48;
                ClassicAppButton1.Text = "";
                ClassicAppButton2.Text = "";
                ClassicAppButton2.Left = ClassicAppButton1.Right + 3;
                ClassicAppButton1.Font = new Font(CP.MetricsFonts.CaptionFont.Name, 8f, ClassicAppButton1.Font.Style);
                ClassicAppButton2.Font = new Font(CP.MetricsFonts.CaptionFont.Name, 8f, ClassicAppButton2.Font.Style);
                ClassicStartButton.Font = new Font(CP.MetricsFonts.CaptionFont.Name, 8.5f, ClassicStartButton.Font.Style);
                ClassicAppButton1.HatchBrush = false;
            }

            switch (Style)
            {
                case WindowStyle.W11:
                    {
                        if (My.Env.W11)
                            My.Env.EP = new ExplorerPatcher();

                        if (ExplorerPatcher.IsAllowed())
                        {
                            {
                                ref var temp1 = ref My.Env.EP;

                                if (!temp1.UseTaskbar10)
                                {
                                    Taskbar.BlurPower = 8;
                                    Taskbar.Height = 42;
                                    Taskbar.NoisePower = 0.3f;
                                }
                                else
                                {
                                    Taskbar.BlurPower = 8;
                                    Taskbar.Height = 35;
                                    Taskbar.UseWin11ORB_WithWin10 = !temp1.TaskbarButton10;
                                    Taskbar.NoisePower = 0f;
                                }

                                if (!temp1.UseStart10)
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

                                    switch (temp1.StartStyle)
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
                        Taskbar.BlurPower = !CP.Windows10.IncreaseTBTransparency ? 12 : 6;
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
                        Taskbar.NoisePower = (float)(CP.Windows7.ColorizationGlassReflectionIntensity / 100d);
                        Taskbar.Height = 34;

                        Start.BlurPower = 1;
                        Start.NoisePower = 0.5f;
                        Start.Width = 136;
                        Start.Height = 191;
                        Start.NoisePower = (float)(CP.Windows7.ColorizationGlassReflectionIntensity / 100d);
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
                        ClassicAppButton1.Image = My.Resources.SampleApp_Active.Resize(23, 23);
                        ClassicAppButton2.Image = My.Resources.SampleApp_Inactive.Resize(23, 23);
                        ClassicStartButton.Image = My.Resources.Native7.Resize(18, 16);
                        ClassicAppButton1.ImageAlign = ContentAlignment.BottomLeft;
                        ClassicAppButton2.ImageAlign = ContentAlignment.BottomLeft;
                        ClassicAppButton1.Width = 140;
                        ClassicAppButton2.Width = 140;
                        ClassicAppButton1.Text = ClassicWindow1.Text;
                        ClassicAppButton2.Text = ClassicWindow2.Text;
                        ClassicAppButton2.Left = ClassicAppButton1.Right + 3;
                        ClassicAppButton1.Font = new Font(CP.MetricsFonts.CaptionFont.Name, 8f, ClassicAppButton1.Font.Style);
                        ClassicAppButton2.Font = new Font(CP.MetricsFonts.CaptionFont.Name, 8f, ClassicAppButton2.Font.Style);
                        ClassicStartButton.Font = new Font(CP.MetricsFonts.CaptionFont.Name, 8.5f, ClassicStartButton.Font.Style);
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
                        ClassicAppButton1.Image = My.Resources.SampleApp_Active.Resize(23, 23);
                        ClassicAppButton2.Image = My.Resources.SampleApp_Inactive.Resize(23, 23);
                        ClassicStartButton.Image = My.Resources.NativeXP.Resize(18, 16);
                        ClassicAppButton1.ImageAlign = ContentAlignment.BottomLeft;
                        ClassicAppButton2.ImageAlign = ContentAlignment.BottomLeft;
                        ClassicAppButton1.Width = 140;
                        ClassicAppButton2.Width = 140;
                        ClassicAppButton1.Text = ClassicWindow1.Text;
                        ClassicAppButton2.Text = ClassicWindow2.Text;
                        ClassicAppButton2.Left = ClassicAppButton1.Right + 3;
                        ClassicAppButton1.Font = new Font(CP.MetricsFonts.CaptionFont.Name, 8f, ClassicAppButton1.Font.Style);
                        ClassicAppButton2.Font = new Font(CP.MetricsFonts.CaptionFont.Name, 8f, ClassicAppButton2.Font.Style);
                        ClassicStartButton.Font = new Font(CP.MetricsFonts.CaptionFont.Name, 8.5f, ClassicStartButton.Font.Style);
                        ClassicAppButton1.HatchBrush = true;
                        break;
                    }

            }

            if (Style == WindowStyle.W10 & !CP.WindowsEffects.FullScreenStartMenu | Style == WindowStyle.W11)
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
        public static void ApplyWindowStyles(CP CP, WindowStyle Style, UI.Simulation.Window Window1, UI.Simulation.Window Window2, UI.WP.Button StartButton = null, UI.WP.Button LogonUIButton = null)
        {
            Window1.Active = true;
            Window2.Active = false;

            if (ExplorerPatcher.IsAllowed())
                My.Env.EP = new ExplorerPatcher();

            Window1.SuspendRefresh = true;
            Window2.SuspendRefresh = true;

            switch (Style)
            {
                case WindowStyle.W11:
                    {
                        #region Win11
                        Window1.AccentColor_Enabled = CP.Windows11.ApplyAccentOnTitlebars;
                        Window2.AccentColor_Enabled = CP.Windows11.ApplyAccentOnTitlebars;

                        Window1.AccentColor_Active = CP.Windows11.Titlebar_Active;
                        Window2.AccentColor_Active = CP.Windows11.Titlebar_Active;

                        Window1.AccentColor_Inactive = CP.Windows11.Titlebar_Inactive;
                        Window2.AccentColor_Inactive = CP.Windows11.Titlebar_Inactive;

                        Window1.DarkMode = !CP.Windows11.AppMode_Light;
                        Window2.DarkMode = !CP.Windows11.AppMode_Light;

                        Window1.Shadow = CP.WindowsEffects.WindowShadow;
                        Window2.Shadow = CP.WindowsEffects.WindowShadow;
                        break;
                    }

                #endregion
                case WindowStyle.W10:
                    {
                        #region Win10
                        Window1.AccentColor_Enabled = CP.Windows10.ApplyAccentOnTitlebars;
                        Window2.AccentColor_Enabled = CP.Windows10.ApplyAccentOnTitlebars;

                        Window1.AccentColor_Active = CP.Windows10.Titlebar_Active;
                        Window2.AccentColor_Active = CP.Windows10.Titlebar_Active;

                        Window1.AccentColor_Inactive = CP.Windows10.Titlebar_Inactive;
                        Window2.AccentColor_Inactive = CP.Windows10.Titlebar_Inactive;

                        Window1.DarkMode = !CP.Windows10.AppMode_Light;
                        Window2.DarkMode = !CP.Windows10.AppMode_Light;

                        Window1.Shadow = CP.WindowsEffects.WindowShadow;
                        Window2.Shadow = CP.WindowsEffects.WindowShadow;
                        break;
                    }
                #endregion
                case WindowStyle.W81:
                    {
                        #region Win8.1
                        if ((My.Env.W8 | My.Env.W81) & My.Env.Settings.Miscellaneous.Win7LivePreview)
                        {
                            RefreshDWM(CP);
                        }

                        if (StartButton is not null)
                            ApplyMetroStartToButton(CP, StartButton);
                        if (LogonUIButton is not null)
                            ApplyBackLogonUI(CP, LogonUIButton);

                        switch (CP.Windows81.Theme)
                        {
                            case Structures.Windows7.Themes.Aero:
                                {
                                    Window1.Preview = UI.Simulation.Window.Preview_Enum.W8;
                                    Window2.Preview = UI.Simulation.Window.Preview_Enum.W8;
                                    break;
                                }
                            case Structures.Windows7.Themes.AeroLite:
                                {
                                    Window1.Preview = UI.Simulation.Window.Preview_Enum.W8Lite;
                                    Window2.Preview = UI.Simulation.Window.Preview_Enum.W8Lite;
                                    break;
                                }
                        }

                        Window1.AccentColor_Active = CP.Windows81.ColorizationColor;
                        Window1.Win7ColorBal = CP.Windows81.ColorizationColorBalance;

                        Window2.AccentColor_Active = CP.Windows81.ColorizationColor;
                        Window2.Win7ColorBal = CP.Windows81.ColorizationColorBalance;
                        break;
                    }

                #endregion
                case WindowStyle.W7:
                    {
                        #region Win7
                        if (My.Env.WVista & My.Env.Settings.Miscellaneous.Win7LivePreview)
                        {
                            RefreshDWM(CP);
                        }

                        Window1.Shadow = CP.WindowsEffects.WindowShadow;
                        Window2.Shadow = CP.WindowsEffects.WindowShadow;

                        switch (CP.Windows7.Theme)
                        {
                            case Structures.Windows7.Themes.Aero:
                                {
                                    Window1.Preview = UI.Simulation.Window.Preview_Enum.W7Aero;
                                    Window1.Win7Alpha = CP.Windows7.ColorizationBlurBalance;
                                    Window1.Win7ColorBal = CP.Windows7.ColorizationColorBalance;
                                    Window1.Win7GlowBal = CP.Windows7.ColorizationAfterglowBalance;
                                    Window1.AccentColor_Active = CP.Windows7.ColorizationColor;
                                    Window1.AccentColor2_Active = CP.Windows7.ColorizationAfterglow;
                                    Window1.AccentColor_Inactive = CP.Windows7.ColorizationColor;
                                    Window1.AccentColor2_Inactive = CP.Windows7.ColorizationAfterglow;
                                    Window1.Win7Noise = CP.Windows7.ColorizationGlassReflectionIntensity;
                                    Window2.Preview = UI.Simulation.Window.Preview_Enum.W7Aero;
                                    Window2.Win7Alpha = CP.Windows7.ColorizationBlurBalance;
                                    Window2.Win7ColorBal = CP.Windows7.ColorizationColorBalance;
                                    Window2.Win7GlowBal = CP.Windows7.ColorizationAfterglowBalance;
                                    Window2.AccentColor_Active = CP.Windows7.ColorizationColor;
                                    Window2.AccentColor2_Active = CP.Windows7.ColorizationAfterglow;
                                    Window2.AccentColor_Inactive = CP.Windows7.ColorizationColor;
                                    Window2.AccentColor2_Inactive = CP.Windows7.ColorizationAfterglow;
                                    Window2.Win7Noise = CP.Windows7.ColorizationGlassReflectionIntensity;
                                    break;
                                }

                            case Structures.Windows7.Themes.AeroOpaque:
                                {
                                    Window1.Preview = UI.Simulation.Window.Preview_Enum.W7Opaque;
                                    Window1.Win7Alpha = CP.Windows7.ColorizationColorBalance;
                                    Window1.AccentColor_Active = CP.Windows7.ColorizationColor;
                                    Window1.AccentColor_Inactive = CP.Windows7.ColorizationColor;
                                    Window1.Win7Noise = CP.Windows7.ColorizationGlassReflectionIntensity;
                                    Window2.Preview = UI.Simulation.Window.Preview_Enum.W7Opaque;
                                    Window2.Win7Alpha = CP.Windows7.ColorizationColorBalance;
                                    Window2.AccentColor_Active = CP.Windows7.ColorizationColor;
                                    Window2.AccentColor_Inactive = CP.Windows7.ColorizationColor;
                                    Window2.Win7Noise = CP.Windows7.ColorizationGlassReflectionIntensity;
                                    break;
                                }

                            case Structures.Windows7.Themes.Basic:
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
                #endregion
                case WindowStyle.WVista:
                    {
                        #region WinVista
                        if (My.Env.WVista & My.Env.Settings.Miscellaneous.Win7LivePreview)
                        {
                            RefreshDWM(CP);
                        }

                        Window1.Shadow = CP.WindowsEffects.WindowShadow;
                        Window2.Shadow = CP.WindowsEffects.WindowShadow;

                        switch (CP.WindowsVista.Theme)
                        {
                            case Structures.Windows7.Themes.Aero:
                                {
                                    Window1.Preview = UI.Simulation.Window.Preview_Enum.W7Aero;
                                    Window1.Win7Alpha = (int)Math.Round((255 - CP.WindowsVista.Alpha) / 255d * 100d);
                                    Window1.Win7ColorBal = (int)Math.Round((255 - CP.WindowsVista.Alpha) / 255d * 100d);
                                    // .Win7GlowBal = [CP].WindowsVista.ColorizationAfterglowBalance
                                    Window1.AccentColor_Active = Color.FromArgb(CP.WindowsVista.Alpha, CP.WindowsVista.ColorizationColor);
                                    Window1.AccentColor2_Active = Color.FromArgb(CP.WindowsVista.Alpha, CP.WindowsVista.ColorizationColor);
                                    Window1.AccentColor_Inactive = Color.FromArgb(100, CP.WindowsVista.ColorizationColor);
                                    Window1.AccentColor2_Inactive = Color.FromArgb(100, CP.WindowsVista.ColorizationColor);
                                    Window1.Win7Noise = 100f;
                                    Window2.Preview = UI.Simulation.Window.Preview_Enum.W7Aero;
                                    Window2.Win7Alpha = (int)Math.Round((255 - CP.WindowsVista.Alpha) / 255d * 100d);
                                    Window2.Win7ColorBal = (int)Math.Round((255 - CP.WindowsVista.Alpha) / 255d * 100d);
                                    // .Win7GlowBal = [CP].WindowsVista.ColorizationAfterglowBalance
                                    Window2.AccentColor_Active = CP.WindowsVista.ColorizationColor;
                                    Window2.AccentColor2_Active = CP.WindowsVista.ColorizationColor;
                                    Window2.AccentColor_Inactive = Color.FromArgb(100, CP.WindowsVista.ColorizationColor);
                                    Window2.AccentColor2_Inactive = Color.FromArgb(100, CP.WindowsVista.ColorizationColor);
                                    Window2.Win7Noise = 100f;
                                    break;
                                }

                            case Structures.Windows7.Themes.AeroOpaque:
                                {
                                    Window1.Preview = UI.Simulation.Window.Preview_Enum.W7Opaque;
                                    Window1.Win7Alpha = (int)Math.Round(CP.WindowsVista.Alpha / 255d * 100d);
                                    Window1.AccentColor_Active = CP.WindowsVista.ColorizationColor;
                                    Window1.AccentColor_Inactive = CP.WindowsVista.ColorizationColor;
                                    Window1.Win7Noise = 100f;
                                    Window2.Preview = UI.Simulation.Window.Preview_Enum.W7Opaque;
                                    Window2.Win7Alpha = (int)Math.Round(CP.WindowsVista.Alpha / 255d * 100d);
                                    Window2.AccentColor_Active = CP.WindowsVista.ColorizationColor;
                                    Window2.AccentColor_Inactive = CP.WindowsVista.ColorizationColor;
                                    Window2.Win7Noise = 100f;
                                    break;
                                }

                            case Structures.Windows7.Themes.Basic:
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
                    #endregion

            }

            Window1.SuspendRefresh = false;
            Window2.SuspendRefresh = false;

            Window1.Invalidate();
            Window2.Invalidate();
        }
        public static void AdjustPreview_ModernOrClassic(CP CP, WindowStyle Style, UI.WP.TablessControl tabs_preview, UI.WP.AlertBox WXP_Alert)
        {
            if (CP is not null)
            {
                bool condition0 = Style == WindowStyle.W7 && CP.Windows7.Theme == Structures.Windows7.Themes.Classic;
                bool condition1 = Style == WindowStyle.WVista && CP.WindowsVista.Theme == Structures.Windows7.Themes.Classic;
                bool condition2 = Style == WindowStyle.WXP && CP.WindowsXP.Theme == Structures.WindowsXP.Themes.Classic;
                WXP_Alert.Visible = Style == WindowStyle.WXP && My.Env.StartedWithClassicTheme;
                tabs_preview.SelectedIndex = condition0 | condition1 | condition2 ? 1 : 0;
            }
        }
        public static void SetClassicWindowMetrics(CP CP, UI.Retro.WindowR Window)
        {
            if (CP is not null)
            {
                Window.Metrics_BorderWidth = CP.MetricsFonts.BorderWidth;
                Window.Metrics_CaptionHeight = CP.MetricsFonts.CaptionHeight;
                Window.Metrics_CaptionWidth = CP.MetricsFonts.CaptionWidth;
                Window.Metrics_PaddedBorderWidth = CP.MetricsFonts.PaddedBorderWidth;
                Window.Font = CP.MetricsFonts.CaptionFont;
                Window.Refresh();
            }
        }
        public static void SetModernWindowMetrics(CP CP, UI.Simulation.Window Window)
        {
            if (CP is not null)
            {
                Window.Font = CP.MetricsFonts.CaptionFont;
                Window.Metrics_BorderWidth = CP.MetricsFonts.BorderWidth;
                Window.Metrics_CaptionHeight = CP.MetricsFonts.CaptionHeight;
                Window.Metrics_PaddedBorderWidth = CP.MetricsFonts.PaddedBorderWidth;
                Window.Invalidate();
            }
        }
        public static void SetClassicWindowColors(CP CP, UI.Retro.WindowR Window, bool Active = true)
        {
            if (CP is not null)
            {
                Window.ButtonDkShadow = CP.Win32.ButtonDkShadow;
                Window.BackColor = CP.Win32.ButtonFace;
                Window.ButtonHilight = CP.Win32.ButtonHilight;
                Window.ButtonLight = CP.Win32.ButtonLight;
                Window.ButtonShadow = CP.Win32.ButtonShadow;
                Window.ButtonText = CP.Win32.ButtonText;

                if (Active)
                {
                    Window.ColorBorder = CP.Win32.ActiveBorder;
                    Window.ForeColor = CP.Win32.TitleText;
                    Window.Color1 = CP.Win32.ActiveTitle;
                    Window.Color2 = CP.Win32.GradientActiveTitle;
                }
                else
                {
                    Window.ColorBorder = CP.Win32.InactiveBorder;
                    Window.ForeColor = CP.Win32.InactiveTitleText;
                    Window.Color1 = CP.Win32.InactiveTitle;
                    Window.Color2 = CP.Win32.GradientInactiveTitle;
                }

                Window.ColorGradient = CP.Win32.EnableGradient;
            }
        }
        public static void SetClassicPanelRaisedRColors(CP CP, UI.Retro.PanelRaisedR Panel)
        {
            Panel.BackColor = CP.Win32.ButtonFace;
            Panel.ButtonHilight = CP.Win32.ButtonHilight;
            Panel.ButtonLight = CP.Win32.ButtonLight;
            Panel.ButtonShadow = CP.Win32.ButtonShadow;
            Panel.ButtonDkShadow = CP.Win32.ButtonDkShadow;
            Panel.ForeColor = CP.Win32.TitleText;
        }
        public static void SetClassicPanelColors(CP CP, UI.Retro.PanelR Panel)
        {
            Panel.BackColor = CP.Win32.ButtonFace;
            Panel.ButtonHilight = CP.Win32.ButtonHilight;
            Panel.ButtonLight = CP.Win32.ButtonLight;
            Panel.ButtonShadow = CP.Win32.ButtonShadow;
            Panel.ButtonDkShadow = CP.Win32.ButtonDkShadow;
            Panel.ForeColor = CP.Win32.TitleText;
        }
        public static void SetClassicButtonColors(CP CP, UI.Retro.ButtonR Button)
        {
            Button.ButtonDkShadow = CP.Win32.ButtonDkShadow;
            Button.ButtonHilight = CP.Win32.ButtonHilight;
            Button.ButtonLight = CP.Win32.ButtonLight;
            Button.ButtonShadow = CP.Win32.ButtonShadow;
            Button.BackColor = CP.Win32.ButtonFace;
            Button.ForeColor = CP.Win32.ButtonText;
            Button.WindowFrame = CP.Win32.WindowFrame;
            Button.FocusRectWidth = (int)CP.WindowsEffects.FocusRectWidth;
            Button.FocusRectHeight = (int)CP.WindowsEffects.FocusRectHeight;
        }
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
        public static void ApplyMetroStartToButton(CP ColorPalette, UI.WP.Button W81_start)
        {
            switch (ColorPalette.Windows81.Start)
            {
                case 1:
                    {
                        W81_start.Image = My.MyProject.Forms.Start8Selector.img1.Image.Resize(48, 48);
                        break;
                    }
                case 2:
                    {
                        W81_start.Image = My.MyProject.Forms.Start8Selector.img2.Image.Resize(48, 48);
                        break;
                    }
                case 3:
                    {
                        W81_start.Image = My.MyProject.Forms.Start8Selector.img3.Image.Resize(48, 48);
                        break;
                    }
                case 4:
                    {
                        W81_start.Image = My.MyProject.Forms.Start8Selector.img4.Image.Resize(48, 48);
                        break;
                    }
                case 5:
                    {
                        W81_start.Image = My.MyProject.Forms.Start8Selector.img5.Image.Resize(48, 48);
                        break;
                    }
                case 6:
                    {
                        W81_start.Image = My.MyProject.Forms.Start8Selector.img6.Image.Resize(48, 48);
                        break;
                    }
                case 7:
                    {
                        W81_start.Image = My.MyProject.Forms.Start8Selector.img7.Image.Resize(48, 48);
                        break;
                    }
                case 8:
                    {
                        W81_start.Image = My.MyProject.Forms.Start8Selector.img8.Image.Resize(48, 48);
                        break;
                    }
                case 9:
                    {
                        W81_start.Image = My.MyProject.Forms.Start8Selector.img9.Image.Resize(48, 48);
                        break;
                    }
                case 10:
                    {
                        W81_start.Image = My.MyProject.Forms.Start8Selector.img10.Image.Resize(48, 48);
                        break;
                    }
                case 11:
                    {
                        W81_start.Image = My.MyProject.Forms.Start8Selector.img11.Image.Resize(48, 48);
                        break;
                    }
                case 12:
                    {
                        W81_start.Image = My.MyProject.Forms.Start8Selector.img12.Image.Resize(48, 48);
                        break;
                    }
                case 13:
                    {
                        W81_start.Image = My.MyProject.Forms.Start8Selector.img13.Image.Resize(48, 48);
                        break;
                    }
                case 14:
                    {
                        W81_start.Image = My.MyProject.Forms.Start8Selector.img14.Image.Resize(48, 48);
                        break;
                    }
                case 15:
                    {
                        W81_start.Image = My.MyProject.Forms.Start8Selector.img15.Image.Resize(48, 48);
                        break;
                    }
                case 16:
                    {
                        W81_start.Image = My.MyProject.Forms.Start8Selector.img16.Image.Resize(48, 48);
                        break;
                    }
                case 17:
                    {
                        W81_start.Image = My.MyProject.Forms.Start8Selector.img17.Image.Resize(48, 48);
                        break;
                    }
                case 18:
                    {
                        W81_start.Image = My.MyProject.Forms.Start8Selector.img18.Image.Resize(48, 48);
                        break;
                    }
                case 19:
                    {
                        W81_start.Image = (Image)ColorPalette.Windows81.PersonalColors_Background.ToBitmap(new Size(48, 48));
                        break;
                    }
                case 20:
                    {
                        W81_start.Image = My.Env.Wallpaper.Resize(48, 48);
                        break;
                    }

                default:
                    {
                        W81_start.Image = My.MyProject.Forms.Start8Selector.img1.Image.Resize(48, 48);
                        break;
                    }
            }
        }
        public static void ApplyBackLogonUI(CP ColorPalette, UI.WP.Button W8_logonui)
        {

            switch (ColorPalette.Windows81.LogonUI)
            {
                case 0:
                    {
                        W8_logonui.Image = (Image)Color.FromArgb(34, 34, 34).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 1:
                    {
                        W8_logonui.Image = (Image)Color.FromArgb(34, 34, 34).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 2:
                    {
                        W8_logonui.Image = (Image)Color.FromArgb(34, 34, 34).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 3:
                    {
                        W8_logonui.Image = (Image)Color.FromArgb(34, 34, 34).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 4:
                    {
                        W8_logonui.Image = (Image)Color.FromArgb(42, 27, 0).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 5:
                    {
                        W8_logonui.Image = (Image)Color.FromArgb(59, 0, 3).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 6:
                    {
                        W8_logonui.Image = (Image)Color.FromArgb(65, 0, 49).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 7:
                    {
                        W8_logonui.Image = (Image)Color.FromArgb(41, 0, 66).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 8:
                    {
                        W8_logonui.Image = (Image)Color.FromArgb(30, 3, 84).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 9:
                    {
                        W8_logonui.Image = (Image)Color.FromArgb(0, 31, 66).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 10:
                    {
                        W8_logonui.Image = (Image)Color.FromArgb(3, 66, 82).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 11:
                    {
                        W8_logonui.Image = (Image)Color.FromArgb(30, 144, 255).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 12:
                    {
                        W8_logonui.Image = (Image)Color.FromArgb(4, 63, 0).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 13:
                    {
                        W8_logonui.Image = (Image)Color.FromArgb(188, 90, 28).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 14:
                    {
                        W8_logonui.Image = (Image)Color.FromArgb(155, 28, 29).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 15:
                    {
                        W8_logonui.Image = (Image)Color.FromArgb(152, 28, 90).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 16:
                    {
                        W8_logonui.Image = (Image)Color.FromArgb(88, 28, 152).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 17:
                    {
                        W8_logonui.Image = (Image)Color.FromArgb(28, 74, 153).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 18:
                    {
                        W8_logonui.Image = (Image)Color.FromArgb(69, 143, 221).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 19:
                    {
                        W8_logonui.Image = (Image)Color.FromArgb(0, 141, 142).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 20:
                    {
                        W8_logonui.Image = (Image)Color.FromArgb(120, 168, 33).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 21:
                    {
                        W8_logonui.Image = (Image)Color.FromArgb(191, 142, 16).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 22:
                    {
                        W8_logonui.Image = (Image)Color.FromArgb(219, 80, 171).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 23:
                    {
                        W8_logonui.Image = (Image)Color.FromArgb(154, 154, 154).ToBitmap(new Size(48, 48));
                        break;
                    }

                case 24:
                    {
                        W8_logonui.Image = (Image)Color.FromArgb(88, 88, 88).ToBitmap(new Size(48, 48));
                        break;
                    }

                default:
                    {
                        W8_logonui.Image = (Image)Color.FromArgb(34, 34, 34).ToBitmap(new Size(48, 48));
                        break;
                    }

            }


        }
        public static Bitmap GetTintedWallpaper(Structures.WallpaperTone WT)
        {
            if (!System.IO.File.Exists(WT.Image))
            {
                if (My.Env.WXP)
                {
                    WT.Image = My.Env.PATH_Windows + @"\Web\Wallpaper\Bliss.bmp";
                }
                else
                {
                    WT.Image = My.Env.PATH_Windows + @"\Web\Wallpaper\Windows\img0.jpg";
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
    }
}