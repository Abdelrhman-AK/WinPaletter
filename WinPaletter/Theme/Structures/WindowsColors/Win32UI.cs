using Devcorp.Controls.VisualStyles;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.NativeMethods.User32;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter.Theme.Structures
{
    public struct Win32UI : ICloneable
    {
        public bool EnableTheming;
        public bool EnableGradient;
        public Color ActiveBorder;
        public Color ActiveTitle;
        public Color AppWorkspace;
        public Color Background;
        public Color ButtonAlternateFace;
        public Color ButtonDkShadow;
        public Color ButtonFace;
        public Color ButtonHilight;
        public Color ButtonLight;
        public Color ButtonShadow;
        public Color ButtonText;
        public Color GradientActiveTitle;
        public Color GradientInactiveTitle;
        public Color GrayText;
        public Color HilightText;
        public Color HotTrackingColor;
        public Color InactiveBorder;
        public Color InactiveTitle;
        public Color InactiveTitleText;
        public Color InfoText;
        public Color InfoWindow;
        public Color Menu;
        public Color MenuBar;
        public Color MenuText;
        public Color Scrollbar;
        public Color TitleText;
        public Color Window;
        public Color WindowFrame;
        public Color WindowText;
        public Color Hilight;
        public Color MenuHilight;
        public Color Desktop;

        public static bool operator ==(Win32UI First, Win32UI Second)
        {
            return First.Equals(Second);
        }

        public static bool operator !=(Win32UI First, Win32UI Second)
        {
            return !First.Equals(Second);
        }
        public enum Method
        {
            Registry,
            File,
            VisualStyles
        }
        public void Load(Method Method = Method.Registry, VisualStyleMetrics vs = default)
        {
            switch (Method)
            {
                case Method.Registry:
                    {

                        Fixer.SystemParametersInfo((int)SPI.Effects.GETFLATMENU, 0, ref EnableTheming, (int)SPIF.None);
                        Fixer.SystemParametersInfo((int)SPI.Titlebars.GETGRADIENTCAPTIONS, 0, ref EnableGradient, (int)SPIF.None);

                        {
                            var temp = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "ActiveTitle", "153 180 209");
                            if (temp.ToString().Split(' ').Count() == 3)
                                ActiveTitle = Color.FromArgb(255, Convert.ToInt32(temp.ToString().Split(' ')[0]), Convert.ToInt32(temp.ToString().Split(' ')[1]), Convert.ToInt32(temp.ToString().Split(' ')[2]));
                        }

                        {
                            var temp1 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "AppWorkspace", "171 171 171");
                            if (temp1.ToString().Split(' ').Count() == 3)
                                AppWorkspace = Color.FromArgb(255, Convert.ToInt32(temp1.ToString().Split(' ')[0]), Convert.ToInt32(temp1.ToString().Split(' ')[1]), Convert.ToInt32(temp1.ToString().Split(' ')[2]));
                        }

                        {
                            var temp2 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "Background", "0 0 0");
                            if (temp2.ToString().Split(' ').Count() == 3)
                                Background = Color.FromArgb(255, Convert.ToInt32(temp2.ToString().Split(' ')[0]), Convert.ToInt32(temp2.ToString().Split(' ')[1]), Convert.ToInt32(temp2.ToString().Split(' ')[2]));
                        }

                        {
                            var temp3 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonAlternateFace", "0 0 0");
                            if (temp3.ToString().Split(' ').Count() == 3)
                                ButtonAlternateFace = Color.FromArgb(255, Convert.ToInt32(temp3.ToString().Split(' ')[0]), Convert.ToInt32(temp3.ToString().Split(' ')[1]), Convert.ToInt32(temp3.ToString().Split(' ')[2]));
                        }

                        {
                            var temp4 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonDkShadow", "105 105 105");
                            if (temp4.ToString().Split(' ').Count() == 3)
                                ButtonDkShadow = Color.FromArgb(255, Convert.ToInt32(temp4.ToString().Split(' ')[0]), Convert.ToInt32(temp4.ToString().Split(' ')[1]), Convert.ToInt32(temp4.ToString().Split(' ')[2]));
                        }

                        {
                            var temp5 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonFace", "240 240 240");
                            if (temp5.ToString().Split(' ').Count() == 3)
                                ButtonFace = Color.FromArgb(255, Convert.ToInt32(temp5.ToString().Split(' ')[0]), Convert.ToInt32(temp5.ToString().Split(' ')[1]), Convert.ToInt32(temp5.ToString().Split(' ')[2]));
                        }

                        {
                            var temp6 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonHilight", "255 255 255");
                            if (temp6.ToString().Split(' ').Count() == 3)
                                ButtonHilight = Color.FromArgb(255, Convert.ToInt32(temp6.ToString().Split(' ')[0]), Convert.ToInt32(temp6.ToString().Split(' ')[1]), Convert.ToInt32(temp6.ToString().Split(' ')[2]));
                        }

                        {
                            var temp7 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonLight", "227 227 227");
                            if (temp7.ToString().Split(' ').Count() == 3)
                                ButtonLight = Color.FromArgb(255, Convert.ToInt32(temp7.ToString().Split(' ')[0]), Convert.ToInt32(temp7.ToString().Split(' ')[1]), Convert.ToInt32(temp7.ToString().Split(' ')[2]));
                        }

                        {
                            var temp8 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonShadow", "160 160 160");
                            if (temp8.ToString().Split(' ').Count() == 3)
                                ButtonShadow = Color.FromArgb(255, Convert.ToInt32(temp8.ToString().Split(' ')[0]), Convert.ToInt32(temp8.ToString().Split(' ')[1]), Convert.ToInt32(temp8.ToString().Split(' ')[2]));
                        }

                        {
                            var temp9 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonText", "0 0 0");
                            if (temp9.ToString().Split(' ').Count() == 3)
                                ButtonText = Color.FromArgb(255, Convert.ToInt32(temp9.ToString().Split(' ')[0]), Convert.ToInt32(temp9.ToString().Split(' ')[1]), Convert.ToInt32(temp9.ToString().Split(' ')[2]));
                        }

                        {
                            var temp10 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "GradientActiveTitle", "185 209 234");
                            if (temp10.ToString().Split(' ').Count() == 3)
                                GradientActiveTitle = Color.FromArgb(255, Convert.ToInt32(temp10.ToString().Split(' ')[0]), Convert.ToInt32(temp10.ToString().Split(' ')[1]), Convert.ToInt32(temp10.ToString().Split(' ')[2]));
                        }

                        {
                            var temp11 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "GradientInactiveTitle", "215 228 242");
                            if (temp11.ToString().Split(' ').Count() == 3)
                                GradientInactiveTitle = Color.FromArgb(255, Convert.ToInt32(temp11.ToString().Split(' ')[0]), Convert.ToInt32(temp11.ToString().Split(' ')[1]), Convert.ToInt32(temp11.ToString().Split(' ')[2]));
                        }

                        {
                            var temp12 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "GrayText", "109 109 109");
                            if (temp12.ToString().Split(' ').Count() == 3)
                                GrayText = Color.FromArgb(255, Convert.ToInt32(temp12.ToString().Split(' ')[0]), Convert.ToInt32(temp12.ToString().Split(' ')[1]), Convert.ToInt32(temp12.ToString().Split(' ')[2]));
                        }

                        {
                            var temp13 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "HilightText", "255 255 255");
                            if (temp13.ToString().Split(' ').Count() == 3)
                                HilightText = Color.FromArgb(255, Convert.ToInt32(temp13.ToString().Split(' ')[0]), Convert.ToInt32(temp13.ToString().Split(' ')[1]), Convert.ToInt32(temp13.ToString().Split(' ')[2]));
                        }

                        {
                            var temp14 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "HotTrackingColor", "0 102 204");
                            if (temp14.ToString().Split(' ').Count() == 3)
                                HotTrackingColor = Color.FromArgb(255, Convert.ToInt32(temp14.ToString().Split(' ')[0]), Convert.ToInt32(temp14.ToString().Split(' ')[1]), Convert.ToInt32(temp14.ToString().Split(' ')[2]));
                        }

                        {
                            var temp15 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "ActiveBorder", "244 247 252");
                            if (temp15.ToString().Split(' ').Count() == 3)
                                ActiveBorder = Color.FromArgb(255, Convert.ToInt32(temp15.ToString().Split(' ')[0]), Convert.ToInt32(temp15.ToString().Split(' ')[1]), Convert.ToInt32(temp15.ToString().Split(' ')[2]));
                        }

                        {
                            var temp16 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "InactiveBorder", "244 247 252");
                            if (temp16.ToString().Split(' ').Count() == 3)
                                InactiveBorder = Color.FromArgb(255, Convert.ToInt32(temp16.ToString().Split(' ')[0]), Convert.ToInt32(temp16.ToString().Split(' ')[1]), Convert.ToInt32(temp16.ToString().Split(' ')[2]));
                        }

                        {
                            var temp17 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitle", "191 205 219");
                            if (temp17.ToString().Split(' ').Count() == 3)
                                InactiveTitle = Color.FromArgb(255, Convert.ToInt32(temp17.ToString().Split(' ')[0]), Convert.ToInt32(temp17.ToString().Split(' ')[1]), Convert.ToInt32(temp17.ToString().Split(' ')[2]));
                        }

                        {
                            var temp18 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitleText", "0 0 0");
                            if (temp18.ToString().Split(' ').Count() == 3)
                                InactiveTitleText = Color.FromArgb(255, Convert.ToInt32(temp18.ToString().Split(' ')[0]), Convert.ToInt32(temp18.ToString().Split(' ')[1]), Convert.ToInt32(temp18.ToString().Split(' ')[2]));
                        }

                        {
                            var temp19 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "InfoText", "0 0 0");
                            if (temp19.ToString().Split(' ').Count() == 3)
                                InfoText = Color.FromArgb(255, Convert.ToInt32(temp19.ToString().Split(' ')[0]), Convert.ToInt32(temp19.ToString().Split(' ')[1]), Convert.ToInt32(temp19.ToString().Split(' ')[2]));
                        }

                        {
                            var temp20 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "InfoWindow", "255 255 225");
                            if (temp20.ToString().Split(' ').Count() == 3)
                                InfoWindow = Color.FromArgb(255, Convert.ToInt32(temp20.ToString().Split(' ')[0]), Convert.ToInt32(temp20.ToString().Split(' ')[1]), Convert.ToInt32(temp20.ToString().Split(' ')[2]));
                        }

                        {
                            var temp21 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "Menu", "240 240 240");
                            if (temp21.ToString().Split(' ').Count() == 3)
                                Menu = Color.FromArgb(255, Convert.ToInt32(temp21.ToString().Split(' ')[0]), Convert.ToInt32(temp21.ToString().Split(' ')[1]), Convert.ToInt32(temp21.ToString().Split(' ')[2]));
                        }

                        {
                            var temp22 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "MenuBar", "240 240 240");
                            if (temp22.ToString().Split(' ').Count() == 3)
                                MenuBar = Color.FromArgb(255, Convert.ToInt32(temp22.ToString().Split(' ')[0]), Convert.ToInt32(temp22.ToString().Split(' ')[1]), Convert.ToInt32(temp22.ToString().Split(' ')[2]));
                        }

                        {
                            var temp23 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "MenuText", "0 0 0");
                            if (temp23.ToString().Split(' ').Count() == 3)
                                MenuText = Color.FromArgb(255, Convert.ToInt32(temp23.ToString().Split(' ')[0]), Convert.ToInt32(temp23.ToString().Split(' ')[1]), Convert.ToInt32(temp23.ToString().Split(' ')[2]));
                        }

                        {
                            var temp24 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "Scrollbar", "200 200 200");
                            if (temp24.ToString().Split(' ').Count() == 3)
                                Scrollbar = Color.FromArgb(255, Convert.ToInt32(temp24.ToString().Split(' ')[0]), Convert.ToInt32(temp24.ToString().Split(' ')[1]), Convert.ToInt32(temp24.ToString().Split(' ')[2]));
                        }

                        {
                            var temp25 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "TitleText", "0 0 0");
                            if (temp25.ToString().Split(' ').Count() == 3)
                                TitleText = Color.FromArgb(255, Convert.ToInt32(temp25.ToString().Split(' ')[0]), Convert.ToInt32(temp25.ToString().Split(' ')[1]), Convert.ToInt32(temp25.ToString().Split(' ')[2]));
                        }

                        {
                            var temp26 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "Window", "255 255 255");
                            if (temp26.ToString().Split(' ').Count() == 3)
                                Window = Color.FromArgb(255, Convert.ToInt32(temp26.ToString().Split(' ')[0]), Convert.ToInt32(temp26.ToString().Split(' ')[1]), Convert.ToInt32(temp26.ToString().Split(' ')[2]));
                        }

                        {
                            var temp27 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "WindowFrame", "100 100 100");
                            if (temp27.ToString().Split(' ').Count() == 3)
                                WindowFrame = Color.FromArgb(255, Convert.ToInt32(temp27.ToString().Split(' ')[0]), Convert.ToInt32(temp27.ToString().Split(' ')[1]), Convert.ToInt32(temp27.ToString().Split(' ')[2]));
                        }

                        {
                            var temp28 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "WindowText", "0 0 0");
                            if (temp28.ToString().Split(' ').Count() == 3)
                                WindowText = Color.FromArgb(255, Convert.ToInt32(temp28.ToString().Split(' ')[0]), Convert.ToInt32(temp28.ToString().Split(' ')[1]), Convert.ToInt32(temp28.ToString().Split(' ')[2]));
                        }

                        {
                            var temp29 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "Hilight", "0 120 215");
                            if (temp29.ToString().Split(' ').Count() == 3)
                                Hilight = Color.FromArgb(255, Convert.ToInt32(temp29.ToString().Split(' ')[0]), Convert.ToInt32(temp29.ToString().Split(' ')[1]), Convert.ToInt32(temp29.ToString().Split(' ')[2]));
                        }

                        {
                            var temp30 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "MenuHilight", "0 120 215");
                            if (temp30.ToString().Split(' ').Count() == 3)
                                MenuHilight = Color.FromArgb(255, Convert.ToInt32(temp30.ToString().Split(' ')[0]), Convert.ToInt32(temp30.ToString().Split(' ')[1]), Convert.ToInt32(temp30.ToString().Split(' ')[2]));
                        }

                        {
                            var temp31 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "Desktop", "0 0 0");
                            if (temp31.ToString().Split(' ').Count() == 3)
                                Desktop = Color.FromArgb(255, Convert.ToInt32(temp31.ToString().Split(' ')[0]), Convert.ToInt32(temp31.ToString().Split(' ')[1]), Convert.ToInt32(temp31.ToString().Split(' ')[2]));
                        }

                        break;
                    }

                case Method.VisualStyles:
                    {
                        EnableTheming = vs.FlatMenus;
                        // ActiveBorder = ActiveBorder
                        ActiveTitle = vs.Colors.ActiveCaption;
                        // AppWorkspace = AppWorkspace
                        Background = vs.Colors.Background;
                        // ButtonAlternateFace = btnaltface_pick.BackColor
                        ButtonDkShadow = vs.Colors.DkShadow3d;
                        ButtonFace = vs.Colors.Btnface;
                        ButtonHilight = vs.Colors.BtnHighlight;
                        ButtonLight = vs.Colors.Light3d;
                        ButtonShadow = vs.Colors.BtnShadow;
                        // ButtonText = vs.Colors.MenuText
                        GradientActiveTitle = vs.Colors.GradientActiveCaption;
                        GradientInactiveTitle = vs.Colors.GradientInactiveCaption;
                        GrayText = vs.Colors.GrayText;
                        HilightText = vs.Colors.HighlightText;
                        HotTrackingColor = vs.Colors.HotTracking;
                        // InactiveBorder = InactiveBorder
                        InactiveTitle = vs.Colors.InactiveCaption;
                        InactiveTitleText = vs.Colors.InactiveCaptionText;
                        // InfoText = InfoText
                        // InfoWindow = InfoWindow
                        Menu = vs.Colors.Menu;
                        MenuBar = vs.Colors.MenuBar;
                        MenuText = vs.Colors.MenuText;
                        // Scrollbar = Scrollbar
                        TitleText = vs.Colors.CaptionText;
                        Window = vs.Colors.Window;
                        // WindowFrame = Frame
                        WindowText = vs.Colors.WindowText;
                        Hilight = vs.Colors.Highlight;
                        MenuHilight = vs.Colors.MenuHilight;
                        Desktop = vs.Colors.Background;
                        break;
                    }

            }
        }

        // Never change their orders
        public enum ColorsNumbers
        {
            Scrollbar,
            Background,
            ActiveTitle,
            InactiveTitle,
            Menu,
            Window,
            WindowFrame,
            MenuText,
            WindowText,
            TitleText,
            ActiveBorder,
            InactiveBorder,
            AppWorkspace,
            Hilight,
            HilightText,
            ButtonFace,
            ButtonShadow,
            GrayText,
            ButtonText,
            InactiveTitleText,
            ButtonHilight,
            ButtonDkShadow,
            ButtonLight,
            InfoText,
            InfoWindow,
            ButtonAlternateFace,
            HotTrackingColor,
            GradientActiveTitle,
            GradientInactiveTitle,
            MenuHilight,
            MenuBar
        }

        public void Apply(TreeView TreeView = null)
        {
            var vsFile = new System.Text.StringBuilder(260);
            var colorName = new System.Text.StringBuilder(260);
            var sizeName = new System.Text.StringBuilder(260);
            UxTheme.GetCurrentThemeName(vsFile, 260, colorName, 260, sizeName, 260);
            bool isClassic = string.IsNullOrEmpty(vsFile.ToString());

            // Hiding forms is added as there is a bug occurs when a classic theme applied on classic Windows mode
            var fl = new List<Form>();
            fl.Clear();
            if (isClassic)
            {
                foreach (Form f in My.MyProject.Application.OpenForms)
                {
                    if (f.Visible)
                    {
                        f.SuspendLayout();
                        f.Visible = false;
                        fl.Add(f);
                    }
                }
            }

            var C1 = new List<int>();
            var C2 = new List<uint>();

            C1.Clear();
            C2.Clear();

            C1.Add((int)ColorsNumbers.Hilight);
            C2.Add((uint)ColorTranslator.ToWin32(Hilight));

            C1.Add((int)ColorsNumbers.HilightText);
            C2.Add((uint)ColorTranslator.ToWin32(HilightText));

            C1.Add((int)ColorsNumbers.TitleText);
            C2.Add((uint)ColorTranslator.ToWin32(TitleText));

            C1.Add((int)ColorsNumbers.GrayText);
            C2.Add((uint)ColorTranslator.ToWin32(GrayText));

            C1.Add((int)ColorsNumbers.InactiveBorder);
            C2.Add((uint)ColorTranslator.ToWin32(InactiveBorder));

            C1.Add((int)ColorsNumbers.InactiveTitle);
            C2.Add((uint)ColorTranslator.ToWin32(InactiveTitle));

            C1.Add((int)ColorsNumbers.ActiveTitle);
            C2.Add((uint)ColorTranslator.ToWin32(ActiveTitle));

            C1.Add((int)ColorsNumbers.ActiveBorder);
            C2.Add((uint)ColorTranslator.ToWin32(ActiveBorder));

            C1.Add((int)ColorsNumbers.AppWorkspace);
            C2.Add((uint)ColorTranslator.ToWin32(AppWorkspace));

            C1.Add((int)ColorsNumbers.Background);
            C2.Add((uint)ColorTranslator.ToWin32(Background));

            C1.Add((int)ColorsNumbers.GradientActiveTitle);
            C2.Add((uint)ColorTranslator.ToWin32(GradientActiveTitle));

            C1.Add((int)ColorsNumbers.GradientInactiveTitle);
            C2.Add((uint)ColorTranslator.ToWin32(GradientInactiveTitle));

            C1.Add((int)ColorsNumbers.InactiveTitleText);
            C2.Add((uint)ColorTranslator.ToWin32(InactiveTitleText));

            C1.Add((int)ColorsNumbers.InfoWindow);
            C2.Add((uint)ColorTranslator.ToWin32(InfoWindow));

            C1.Add((int)ColorsNumbers.InfoText);
            C2.Add((uint)ColorTranslator.ToWin32(InfoText));

            C1.Add((int)ColorsNumbers.Menu);
            C2.Add((uint)ColorTranslator.ToWin32(Menu));

            C1.Add((int)ColorsNumbers.MenuText);
            C2.Add((uint)ColorTranslator.ToWin32(MenuText));

            C1.Add((int)ColorsNumbers.Scrollbar);
            C2.Add((uint)ColorTranslator.ToWin32(Scrollbar));

            C1.Add((int)ColorsNumbers.Window);
            C2.Add((uint)ColorTranslator.ToWin32(Window));

            C1.Add((int)ColorsNumbers.WindowFrame);
            C2.Add((uint)ColorTranslator.ToWin32(WindowFrame));

            C1.Add((int)ColorsNumbers.WindowText);
            C2.Add((uint)ColorTranslator.ToWin32(WindowText));

            C1.Add((int)ColorsNumbers.HotTrackingColor);
            C2.Add((uint)ColorTranslator.ToWin32(HotTrackingColor));

            C1.Add((int)ColorsNumbers.MenuHilight);
            C2.Add((uint)ColorTranslator.ToWin32(MenuHilight));

            C1.Add((int)ColorsNumbers.MenuBar);
            C2.Add((uint)ColorTranslator.ToWin32(MenuBar));

            C1.Add((int)ColorsNumbers.ButtonFace);
            C2.Add((uint)ColorTranslator.ToWin32(ButtonFace));

            C1.Add((int)ColorsNumbers.ButtonHilight);
            C2.Add((uint)ColorTranslator.ToWin32(ButtonHilight));

            C1.Add((int)ColorsNumbers.ButtonShadow);
            C2.Add((uint)ColorTranslator.ToWin32(ButtonShadow));

            C1.Add((int)ColorsNumbers.ButtonText);
            C2.Add((uint)ColorTranslator.ToWin32(ButtonText));

            C1.Add((int)ColorsNumbers.ButtonDkShadow);
            C2.Add((uint)ColorTranslator.ToWin32(ButtonDkShadow));

            C1.Add((int)ColorsNumbers.ButtonAlternateFace);
            C2.Add((uint)ColorTranslator.ToWin32(ButtonAlternateFace));

            C1.Add((int)ColorsNumbers.ButtonLight);
            C2.Add((uint)ColorTranslator.ToWin32(ButtonLight));

            SetSysColors(C1.Count, C1.ToArray(), C2.ToArray());

            SystemParametersInfo((int)SPI.Effects.SETFLATMENU, 0, EnableTheming, (int)SPIF.UpdateINIFile);
            SystemParametersInfo((int)SPI.Titlebars.SETGRADIENTCAPTIONS, 0, EnableGradient, (int)SPIF.UpdateINIFile);

            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ActiveBorder", ActiveBorder.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ActiveTitle", ActiveTitle.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "AppWorkspace", AppWorkspace.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "Background", Background.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonAlternateFace", ButtonAlternateFace.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonDkShadow", ButtonDkShadow.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonFace", ButtonFace.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonHilight", ButtonHilight.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonLight", ButtonLight.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonShadow", ButtonShadow.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonText", ButtonText.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "GradientActiveTitle", GradientActiveTitle.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "GradientInactiveTitle", GradientInactiveTitle.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "GrayText", GrayText.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "HilightText", HilightText.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "HotTrackingColor", HotTrackingColor.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "InactiveBorder", InactiveBorder.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitle", InactiveTitle.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitleText", InactiveTitleText.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "InfoText", InfoText.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "InfoWindow", InfoWindow.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "Menu", Menu.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "MenuBar", MenuBar.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "MenuText", MenuText.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "Scrollbar", Scrollbar.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "TitleText", TitleText.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "Window", Window.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "WindowFrame", WindowFrame.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "WindowText", WindowText.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "Hilight", Hilight.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "MenuHilight", MenuHilight.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "Desktop", Desktop.ToWin32Reg(), RegistryValueKind.String);

            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ActiveBorder", ActiveBorder.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ActiveTitle", ActiveTitle.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "AppWorkspace", AppWorkspace.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Background", Background.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonAlternateFace", ButtonAlternateFace.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonDkShadow", ButtonDkShadow.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonFace", ButtonFace.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonHilight", ButtonHilight.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonLight", ButtonLight.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonShadow", ButtonShadow.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonText", ButtonText.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "GradientActiveTitle", GradientActiveTitle.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "GradientInactiveTitle", GradientInactiveTitle.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "GrayText", GrayText.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "HilightText", HilightText.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "HotTrackingColor", HotTrackingColor.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "InactiveBorder", InactiveBorder.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "InactiveTitle", InactiveTitle.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "InactiveTitleText", InactiveTitleText.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "InfoText", InfoText.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "InfoWindow", InfoWindow.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Menu", Menu.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "MenuBar", MenuBar.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "MenuText", MenuText.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Scrollbar", Scrollbar.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "TitleText", TitleText.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Window", Window.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "WindowFrame", WindowFrame.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "WindowText", WindowText.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Hilight", Hilight.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "MenuHilight", MenuHilight.ToWin32Reg(), RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Desktop", Desktop.ToWin32Reg(), RegistryValueKind.String);

            if (isClassic)
            {
                if (fl.Count > 0)
                {
                    System.Threading.Thread.Sleep(100);
                    for (int i = 0, loopTo = fl.Count - 1; i <= loopTo; i++)
                    {
                        fl[i].Visible = true;
                        fl[i].ResumeLayout();
                        fl[i].Refresh();
                    }
                }
            }

            if (My.Env.Settings.ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
            {
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ActiveBorder", ActiveBorder.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ActiveTitle", ActiveTitle.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "AppWorkspace", AppWorkspace.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "Background", Background.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonAlternateFace", ButtonAlternateFace.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonDkShadow", ButtonDkShadow.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonFace", ButtonFace.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonHilight", ButtonHilight.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonLight", ButtonLight.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonShadow", ButtonShadow.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonText", ButtonText.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "GradientActiveTitle", GradientActiveTitle.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "GradientInactiveTitle", GradientInactiveTitle.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "GrayText", GrayText.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "HilightText", HilightText.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "HotTrackingColor", HotTrackingColor.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "InactiveBorder", InactiveBorder.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "InactiveTitle", InactiveTitle.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "InactiveTitleText", InactiveTitleText.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "InfoText", InfoText.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "InfoWindow", InfoWindow.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "Menu", Menu.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "MenuBar", MenuBar.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "MenuText", MenuText.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "Scrollbar", Scrollbar.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "TitleText", TitleText.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "Window", Window.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "WindowFrame", WindowFrame.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "WindowText", WindowText.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "Hilight", Hilight.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "MenuHilight", MenuHilight.ToWin32Reg(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "Desktop", Desktop.ToWin32Reg(), RegistryValueKind.String);
            }

            if (My.Env.Settings.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
            {
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ActiveTitle", Color.FromArgb(0, ActiveTitle).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ButtonFace", Color.FromArgb(0, ButtonFace).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ButtonText", Color.FromArgb(0, ButtonText).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "GrayText", Color.FromArgb(0, GrayText).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "Hilight", Color.FromArgb(0, Hilight).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "HilightText", Color.FromArgb(0, HilightText).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "HotTrackingColor", Color.FromArgb(0, HotTrackingColor).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "InactiveTitle", Color.FromArgb(0, InactiveTitle).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "InactiveTitleText", Color.FromArgb(0, InactiveTitleText).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "MenuHilight", Color.FromArgb(0, MenuHilight).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "TitleText", Color.FromArgb(0, TitleText).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "Window", Color.FromArgb(0, Window).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "WindowText", Color.FromArgb(0, WindowText).Reverse(true).ToArgb(), RegistryValueKind.String);
            }

            else if (My.Env.Settings.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults)
            {
                Win32UI _DefWin32;
                if (My.Env.PreviewStyle == WindowStyle.W11)
                {
                    _DefWin32 = new Theme.Default().Windows11().Win32;
                }
                else if (My.Env.PreviewStyle == WindowStyle.W10)
                {
                    _DefWin32 = new Theme.Default().Windows10().Win32;
                }
                else if (My.Env.PreviewStyle == WindowStyle.W81)
                {
                    _DefWin32 = new Theme.Default().Windows81().Win32;
                }
                else if (My.Env.PreviewStyle == WindowStyle.W7)
                {
                    _DefWin32 = new Theme.Default().Windows7().Win32;
                }
                else if (My.Env.PreviewStyle == WindowStyle.WVista)
                {
                    _DefWin32 = new Theme.Default().WindowsVista().Win32;
                }
                else if (My.Env.PreviewStyle == WindowStyle.WXP)
                {
                    _DefWin32 = new Theme.Default().WindowsXP().Win32;
                }
                else
                {
                    _DefWin32 = new Theme.Default().Windows11().Win32;
                }

                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ActiveTitle", Color.FromArgb(0, _DefWin32.ActiveTitle).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ButtonFace", Color.FromArgb(0, _DefWin32.ButtonFace).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ButtonText", Color.FromArgb(0, _DefWin32.ButtonText).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "GrayText", Color.FromArgb(0, _DefWin32.GrayText).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "Hilight", Color.FromArgb(0, _DefWin32.Hilight).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "HilightText", Color.FromArgb(0, _DefWin32.HilightText).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "HotTrackingColor", Color.FromArgb(0, _DefWin32.HotTrackingColor).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "InactiveTitle", Color.FromArgb(0, _DefWin32.InactiveTitle).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "InactiveTitleText", Color.FromArgb(0, _DefWin32.InactiveTitleText).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "MenuHilight", Color.FromArgb(0, _DefWin32.MenuHilight).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "TitleText", Color.FromArgb(0, _DefWin32.TitleText).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "Window", Color.FromArgb(0, _DefWin32.Window).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "WindowText", Color.FromArgb(0, _DefWin32.WindowText).Reverse(true).ToArgb(), RegistryValueKind.String);
            }

            else if (My.Env.Settings.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Erase)
            {
                DelReg_AdministratorDeflector(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors", "Standard");
            }


        }

        public void Update_UPM_DEFAULT(TreeView TreeView = null)
        {
            if (My.Env.Settings.ThemeApplyingBehavior.UPM_HKU_DEFAULT)
            {
                byte[] source = (byte[])GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "UserPreferencesMask", null);
                if (source is not null)
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "UserPreferencesMask", source, RegistryValueKind.Binary);
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
