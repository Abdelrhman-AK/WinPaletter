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
    /// <summary>
    /// Structure responsible for managing classic Windows colors
    /// </summary>
    public struct Win32UI : ICloneable
    {
        /// <summary>If disabled, classic 3D effects will be made to menus and menu items selection</summary>
        public bool EnableTheming;

        /// <summary>Enable titlebar gradience</summary>
        public bool EnableGradient;

        /// <summary>Color of active window border</summary>
        public Color ActiveBorder;

        /// <summary>Active titlebar main color</summary>
        public Color ActiveTitle;

        ///
        public Color AppWorkspace;

        ///
        public Color Background;

        ///
        public Color ButtonAlternateFace;

        ///
        public Color ButtonDkShadow;

        ///
        public Color ButtonFace;

        ///
        public Color ButtonHilight;

        ///
        public Color ButtonLight;

        ///
        public Color ButtonShadow;

        ///
        public Color ButtonText;

        /// <summary>Second color for gradience in active titlebar</summary>
        public Color GradientActiveTitle;

        /// <summary>Second color for gradience in inactive titlebar</summary>
        public Color GradientInactiveTitle;

        /// <summary>Used in disabled items</summary>
        public Color GrayText;

        ///
        public Color HilightText;

        /// <summary>Color of selection rectangles and hyperlinks</summary>
        public Color HotTrackingColor;

        /// <summary>Color of inactive window border</summary>
        public Color InactiveBorder;

        /// <summary>Inactive titlebar main color</summary>
        public Color InactiveTitle;

        /// <summary>Inactive titlebar text</summary>
        public Color InactiveTitleText;

        ///
        public Color InfoText;

        ///
        public Color InfoWindow;

        /// <summary>Color of cascaded menu</summary>
        public Color Menu;

        /// <summary>Color of menu bar</summary>
        public Color MenuBar;

        ///
        public Color MenuText;

        /// <summary>Obsolete: Was used in Windows 9x</summary>
        public Color Scrollbar;

        /// <summary>Active titlebar text</summary>
        public Color TitleText;

        ///
        public Color Window;

        /// <summary>Color of rectangle surrounding a pressed button</summary>
        public Color WindowFrame;

        ///
        public Color WindowText;

        ///
        public Color Hilight;

        ///
        public Color MenuHilight;

        ///
        public Color Desktop;

        /// <summary>
        /// Enumeration of sources, of which data will be loaded or saved
        /// </summary>
        public enum Sources
        {
            ///
            Registry,
            ///
            VisualStyles
        }

        /// <summary>
        /// Loads Win32UI data from registry
        /// </summary>
        /// <param name="source">A source of which data will be loaded</param>
        /// <param name="vs">Devcorp.Controls.VisualStyles.VisualStyleMetrics</param>
        public void Load(Sources source = Sources.Registry, VisualStyleMetrics vs = default)
        {
            switch (source)
            {
                case Sources.Registry:
                    {

                        SystemParametersInfo((int)SPI.Effects.GETFLATMENU, 0, ref EnableTheming, SPIF.None);
                        SystemParametersInfo((int)SPI.Titlebars.GETGRADIENTCAPTIONS, 0, ref EnableGradient, SPIF.None);

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

                case Sources.VisualStyles:
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

        /// <summary>
        /// Enumeration in order of classic Windows items. Used in User32.SetSysColors()
        /// <br><b><i>(!) Never change their orders</i></b></br>
        /// </summary>
        public enum ColorsNumbers
        {
            /// <summary>Obsolete: Was used in Windows 9x</summary>
            Scrollbar,

            /// 
            Background,

            /// <summary>Active titlebar main color</summary>
            ActiveTitle,

            /// <summary>Inactive titlebar main color</summary>
            InactiveTitle,

            /// <summary>Color of cascaded menu</summary>
            Menu,

            /// 
            Window,

            /// <summary>Color of rectangle surrounding a pressed button</summary>
            WindowFrame,

            /// 
            MenuText,

            /// 
            WindowText,

            /// <summary>Active titlebar text</summary>
            TitleText,

            /// <summary>Color of active window border</summary>
            ActiveBorder,

            /// <summary>Color of inactive window border</summary>
            InactiveBorder,

            /// 
            AppWorkspace,

            /// 
            Hilight,

            /// 
            HilightText,

            /// 
            ButtonFace,

            /// 
            ButtonShadow,

            /// <summary>Used in disabled items</summary>
            GrayText,

            /// 
            ButtonText,

            /// <summary>Inactive titlebar text</summary>
            InactiveTitleText,

            /// 
            ButtonHilight,

            /// 
            ButtonDkShadow,

            /// 
            ButtonLight,

            /// 
            InfoText,

            /// 
            InfoWindow,

            /// 
            ButtonAlternateFace,

            /// <summary>Color of selection rectangles and hyperlinks</summary>
            HotTrackingColor,

            /// <summary>Second color for gradience in active titlebar</summary>
            GradientActiveTitle,

            /// <summary>Second color for gradience in inactive titlebar</summary>
            GradientInactiveTitle,

            /// 
            MenuHilight,

            /// <summary>Color of menu bar</summary>
            MenuBar
        }

        /// <summary>
        /// Saves Win32UI data into registry
        /// </summary>
        /// <param name="TreeView">TreeView used as theme log</param>
        public void Apply(TreeView TreeView = null)
        {
            bool isClassic = string.IsNullOrEmpty(UxTheme.GetCurrentVS().Item1);

            // Hiding forms is added as there is a bug occurs when a classic theme applied on classic Windows mode
            var fl = new List<Form>();
            fl.Clear();
            if (isClassic)
            {
                foreach (Form f in Application.OpenForms)
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

            SystemParametersInfo(TreeView, (int)SPI.Effects.SETFLATMENU, 0, EnableTheming, SPIF.UpdateINIFile);
            SystemParametersInfo(TreeView, (int)SPI.Titlebars.SETGRADIENTCAPTIONS, 0, EnableGradient, SPIF.UpdateINIFile);

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

            if (Program.Settings.ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
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

            if (Program.Settings.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
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

            else if (Program.Settings.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults)
            {
                Win32UI @default;
                if (Program.PreviewStyle == WindowStyle.W11)
                {
                    @default = Theme.Default.Windows11().Win32;
                }
                else if (Program.PreviewStyle == WindowStyle.W10)
                {
                    @default = Theme.Default.Windows10().Win32;
                }
                else if (Program.PreviewStyle == WindowStyle.W81)
                {
                    @default = Theme.Default.Windows81().Win32;
                }
                else if (Program.PreviewStyle == WindowStyle.W7)
                {
                    @default = Theme.Default.Windows7().Win32;
                }
                else if (Program.PreviewStyle == WindowStyle.WVista)
                {
                    @default = Theme.Default.WindowsVista().Win32;
                }
                else if (Program.PreviewStyle == WindowStyle.WXP)
                {
                    @default = Theme.Default.WindowsXP().Win32;
                }
                else
                {
                    @default = Theme.Default.Windows11().Win32;
                }

                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ActiveTitle", Color.FromArgb(0, @default.ActiveTitle).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ButtonFace", Color.FromArgb(0, @default.ButtonFace).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ButtonText", Color.FromArgb(0, @default.ButtonText).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "GrayText", Color.FromArgb(0, @default.GrayText).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "Hilight", Color.FromArgb(0, @default.Hilight).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "HilightText", Color.FromArgb(0, @default.HilightText).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "HotTrackingColor", Color.FromArgb(0, @default.HotTrackingColor).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "InactiveTitle", Color.FromArgb(0, @default.InactiveTitle).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "InactiveTitleText", Color.FromArgb(0, @default.InactiveTitleText).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "MenuHilight", Color.FromArgb(0, @default.MenuHilight).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "TitleText", Color.FromArgb(0, @default.TitleText).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "Window", Color.FromArgb(0, @default.Window).Reverse(true).ToArgb(), RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "WindowText", Color.FromArgb(0, @default.WindowText).Reverse(true).ToArgb(), RegistryValueKind.String);
            }

            else if (Program.Settings.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Erase)
            {
                DelReg_AdministratorDeflector(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors", "Standard");
            }
        }

        /// <summary>
        /// Broadcast user preference mask from current user to all users (and LogonUI screen)
        /// </summary>
        /// <param name="TreeView">TreeView used as theme log</param>
        public void Broadcast_UPM_ToDefUsers(TreeView TreeView = null)
        {
            if (Program.Settings.ThemeApplyingBehavior.UPM_HKU_DEFAULT)
            {
                byte[] source = (byte[])GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "UserPreferencesMask", null);
                if (source is not null)
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "UserPreferencesMask", source, RegistryValueKind.Binary);
            }
        }

        /// <summary>Operator to check if two Win32UI structures are equal</summary>
        public static bool operator ==(Win32UI First, Win32UI Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two Win32UI structures are not equal</summary>
        public static bool operator !=(Win32UI First, Win32UI Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones Win32UI structure</summary>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Checks if two AltTab structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>Get hash code of AltTab structure</summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
