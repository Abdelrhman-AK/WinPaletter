using Devcorp.Controls.VisualStyles;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.NativeMethods.User32;
using static WinPaletter.PreviewHelpers;
using static WinPaletter.WinTerminal;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing classic Windows colors
    /// </summary>
    public struct Win32UI : ICloneable
    {
        /// <summary> Controls if Windows 10x colors editing is enabled or not </summary> 
        public bool Enabled;

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

        /// <summary>Obsolete: was used in Windows 9x</summary>
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
        /// Enumeration of sources, from/to which data will be loaded or saved
        /// </summary>
        public enum Sources
        {
            ///
            Registry,
            ///
            VisualStyles
        }

        private void setColorFromRegistry(string registryKey, string valueName, string defaultValue, ref Color targetColor)
        {
            string result = GetReg(registryKey, valueName, defaultValue) as string;
            if (result.Contains(' ') && result.Split(' ').Count() == 3) targetColor = Color.FromArgb(255, result.FromWin32RegToColor());
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
                        Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\WindowsColorsThemes\ClassicColors", string.Empty, true));

                        SystemParametersInfo(SPI.SPI_GETFLATMENU, 0, ref EnableTheming, SPIF.SPIF_NONE);
                        SystemParametersInfo(SPI.SPI_GETGRADIENTCAPTIONS, 0, ref EnableGradient, SPIF.SPIF_NONE);

                        using (Theme.Manager @default = Default.Get())
                        {
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "ActiveTitle", @default.Win32.ActiveTitle.ToWin32Reg(), ref ActiveTitle);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "AppWorkspace", @default.Win32.AppWorkspace.ToWin32Reg(), ref AppWorkspace);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "Background", @default.Win32.Background.ToWin32Reg(), ref Background);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonAlternateFace", @default.Win32.ButtonAlternateFace.ToWin32Reg(), ref ButtonAlternateFace);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonDkShadow", @default.Win32.ButtonDkShadow.ToWin32Reg(), ref ButtonDkShadow);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonFace", @default.Win32.ButtonFace.ToWin32Reg(), ref ButtonFace);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonHilight", @default.Win32.ButtonHilight.ToWin32Reg(), ref ButtonHilight);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonLight", @default.Win32.ButtonLight.ToWin32Reg(), ref ButtonLight);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonShadow", @default.Win32.ButtonShadow.ToWin32Reg(), ref ButtonShadow);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonText", @default.Win32.ButtonText.ToWin32Reg(), ref ButtonText);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "GradientActiveTitle", @default.Win32.GradientActiveTitle.ToWin32Reg(), ref GradientActiveTitle);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "GradientInactiveTitle", @default.Win32.GradientInactiveTitle.ToWin32Reg(), ref GradientInactiveTitle);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "GrayText", @default.Win32.GrayText.ToWin32Reg(), ref GrayText);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "HilightText", @default.Win32.HilightText.ToWin32Reg(), ref HilightText);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "HotTrackingColor", @default.Win32.HotTrackingColor.ToWin32Reg(), ref HotTrackingColor);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "ActiveBorder", @default.Win32.ActiveBorder.ToWin32Reg(), ref ActiveBorder);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "InactiveBorder", @default.Win32.InactiveBorder.ToWin32Reg(), ref InactiveBorder);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitle", @default.Win32.InactiveTitle.ToWin32Reg(), ref InactiveTitle);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitleText", @default.Win32.InactiveTitleText.ToWin32Reg(), ref InactiveTitleText);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "InfoText", @default.Win32.InfoText.ToWin32Reg(), ref InfoText);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "InfoWindow", @default.Win32.InfoWindow.ToWin32Reg(), ref InfoWindow);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "Menu", @default.Win32.Menu.ToWin32Reg(), ref Menu);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "MenuBar", @default.Win32.MenuBar.ToWin32Reg(), ref MenuBar);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "MenuText", @default.Win32.MenuText.ToWin32Reg(), ref MenuText);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "Scrollbar", @default.Win32.Scrollbar.ToWin32Reg(), ref Scrollbar);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "TitleText", @default.Win32.TitleText.ToWin32Reg(), ref TitleText);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "Window", @default.Win32.Window.ToWin32Reg(), ref Window);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "WindowFrame", @default.Win32.WindowFrame.ToWin32Reg(), ref WindowFrame);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "WindowText", @default.Win32.WindowText.ToWin32Reg(), ref WindowText);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "Hilight", @default.Win32.Hilight.ToWin32Reg(), ref Hilight);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "MenuHilight", @default.Win32.MenuHilight.ToWin32Reg(), ref MenuHilight);
                            setColorFromRegistry(@"HKEY_CURRENT_USER\Control Panel\Colors", "Desktop", @default.Win32.Desktop.ToWin32Reg(), ref Desktop);
                        }
                        break;
                    }

                case Sources.VisualStyles:
                    {
                        EnableTheming = vs.FlatMenus;
                        // ActiveBorder = ActiveBorder
                        ActiveTitle = vs.Colors.ActiveCaption;
                        // AppWorkspaceR = AppWorkspaceR
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
        /// <param name="treeView">treeView used as theme log</param>
        public void Apply(TreeView treeView = null)
        {
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\WindowsColorsThemes\ClassicColors", string.Empty, Enabled);

            if (Enabled)
            {
                bool isClassic = string.IsNullOrEmpty(NativeMethods.UxTheme.GetCurrentVS().Item1);

                List<Form> fl = new();
                fl.Clear();

                // Hiding forms is added as there is a bug occurs when a classic theme applied on classic Windows mode
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

                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    List<int> C1 = new();
                    List<uint> C2 = new();

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

                    SystemParametersInfo(treeView, SPI.SPI_SETFLATMENU, 0, EnableTheming, SPIF.SPIF_UPDATEINIFILE);
                    SystemParametersInfo(treeView, SPI.SPI_SETGRADIENTCAPTIONS, 0, EnableGradient, SPIF.SPIF_UPDATEINIFILE);

                    wic.Undo();
                }

                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ActiveBorder", ActiveBorder.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ActiveTitle", ActiveTitle.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "AppWorkspace", AppWorkspace.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "Background", Background.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonAlternateFace", ButtonAlternateFace.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonDkShadow", ButtonDkShadow.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonFace", ButtonFace.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonHilight", ButtonHilight.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonLight", ButtonLight.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonShadow", ButtonShadow.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonText", ButtonText.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "GradientActiveTitle", GradientActiveTitle.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "GradientInactiveTitle", GradientInactiveTitle.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "GrayText", GrayText.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "HilightText", HilightText.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "HotTrackingColor", HotTrackingColor.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "InactiveBorder", InactiveBorder.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitle", InactiveTitle.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitleText", InactiveTitleText.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "InfoText", InfoText.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "InfoWindow", InfoWindow.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "Menu", Menu.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "MenuBar", MenuBar.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "MenuText", MenuText.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "Scrollbar", Scrollbar.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "TitleText", TitleText.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "Window", Window.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "WindowFrame", WindowFrame.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "WindowText", WindowText.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "Hilight", Hilight.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "MenuHilight", MenuHilight.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "Desktop", Desktop.ToWin32Reg(), RegistryValueKind.String);

                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ActiveBorder", ActiveBorder.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ActiveTitle", ActiveTitle.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "AppWorkspace", AppWorkspace.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Background", Background.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonAlternateFace", ButtonAlternateFace.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonDkShadow", ButtonDkShadow.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonFace", ButtonFace.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonHilight", ButtonHilight.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonLight", ButtonLight.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonShadow", ButtonShadow.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonText", ButtonText.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "GradientActiveTitle", GradientActiveTitle.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "GradientInactiveTitle", GradientInactiveTitle.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "GrayText", GrayText.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "HilightText", HilightText.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "HotTrackingColor", HotTrackingColor.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "InactiveBorder", InactiveBorder.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "InactiveTitle", InactiveTitle.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "InactiveTitleText", InactiveTitleText.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "InfoText", InfoText.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "InfoWindow", InfoWindow.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Menu", Menu.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "MenuBar", MenuBar.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "MenuText", MenuText.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Scrollbar", Scrollbar.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "TitleText", TitleText.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Window", Window.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "WindowFrame", WindowFrame.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "WindowText", WindowText.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Hilight", Hilight.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "MenuHilight", MenuHilight.ToWin32Reg(), RegistryValueKind.String);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Desktop", Desktop.ToWin32Reg(), RegistryValueKind.String);

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

                if (Program.Settings.ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                {
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ActiveBorder", ActiveBorder.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ActiveTitle", ActiveTitle.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "AppWorkspace", AppWorkspace.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "Background", Background.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonAlternateFace", ButtonAlternateFace.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonDkShadow", ButtonDkShadow.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonFace", ButtonFace.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonHilight", ButtonHilight.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonLight", ButtonLight.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonShadow", ButtonShadow.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonText", ButtonText.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "GradientActiveTitle", GradientActiveTitle.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "GradientInactiveTitle", GradientInactiveTitle.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "GrayText", GrayText.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "HilightText", HilightText.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "HotTrackingColor", HotTrackingColor.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "InactiveBorder", InactiveBorder.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "InactiveTitle", InactiveTitle.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "InactiveTitleText", InactiveTitleText.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "InfoText", InfoText.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "InfoWindow", InfoWindow.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "Menu", Menu.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "MenuBar", MenuBar.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "MenuText", MenuText.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "Scrollbar", Scrollbar.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "TitleText", TitleText.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "Window", Window.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "WindowFrame", WindowFrame.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "WindowText", WindowText.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "Hilight", Hilight.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "MenuHilight", MenuHilight.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "Desktop", Desktop.ToWin32Reg(), RegistryValueKind.String);
                }

                if (Program.Settings.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                {
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ActiveTitle", Color.FromArgb(0, ActiveTitle).Reverse(true).ToArgb(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ButtonFace", Color.FromArgb(0, ButtonFace).Reverse(true).ToArgb(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ButtonText", Color.FromArgb(0, ButtonText).Reverse(true).ToArgb(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "GrayText", Color.FromArgb(0, GrayText).Reverse(true).ToArgb(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "Hilight", Color.FromArgb(0, Hilight).Reverse(true).ToArgb(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "HilightText", Color.FromArgb(0, HilightText).Reverse(true).ToArgb(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "HotTrackingColor", Color.FromArgb(0, HotTrackingColor).Reverse(true).ToArgb(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "InactiveTitle", Color.FromArgb(0, InactiveTitle).Reverse(true).ToArgb(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "InactiveTitleText", Color.FromArgb(0, InactiveTitleText).Reverse(true).ToArgb(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "MenuHilight", Color.FromArgb(0, MenuHilight).Reverse(true).ToArgb(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "TitleText", Color.FromArgb(0, TitleText).Reverse(true).ToArgb(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "Window", Color.FromArgb(0, Window).Reverse(true).ToArgb(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "WindowText", Color.FromArgb(0, WindowText).Reverse(true).ToArgb(), RegistryValueKind.String);
                }

                else if (Program.Settings.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults)
                {
                    Win32UI @default;
                    if (Program.WindowStyle == WindowStyle.W12)
                    {
                        @default = Theme.Default.Windows12().Win32;
                    }
                    else if (Program.WindowStyle == WindowStyle.W11)
                    {
                        @default = Theme.Default.Windows11().Win32;
                    }
                    else if (Program.WindowStyle == WindowStyle.W10)
                    {
                        @default = Theme.Default.Windows10().Win32;
                    }
                    else if (Program.WindowStyle == WindowStyle.W81)
                    {
                        @default = Theme.Default.Windows81().Win32;
                    }
                    else if (Program.WindowStyle == WindowStyle.W7)
                    {
                        @default = Theme.Default.Windows7().Win32;
                    }
                    else if (Program.WindowStyle == WindowStyle.WVista)
                    {
                        @default = Theme.Default.WindowsVista().Win32;
                    }
                    else if (Program.WindowStyle == WindowStyle.WXP)
                    {
                        @default = Theme.Default.WindowsXP().Win32;
                    }
                    else
                    {
                        @default = Theme.Default.Windows12().Win32;
                    }

                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ActiveTitle", Color.FromArgb(0, @default.ActiveTitle).Reverse(true).ToArgb(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ButtonFace", Color.FromArgb(0, @default.ButtonFace).Reverse(true).ToArgb(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ButtonText", Color.FromArgb(0, @default.ButtonText).Reverse(true).ToArgb(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "GrayText", Color.FromArgb(0, @default.GrayText).Reverse(true).ToArgb(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "Hilight", Color.FromArgb(0, @default.Hilight).Reverse(true).ToArgb(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "HilightText", Color.FromArgb(0, @default.HilightText).Reverse(true).ToArgb(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "HotTrackingColor", Color.FromArgb(0, @default.HotTrackingColor).Reverse(true).ToArgb(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "InactiveTitle", Color.FromArgb(0, @default.InactiveTitle).Reverse(true).ToArgb(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "InactiveTitleText", Color.FromArgb(0, @default.InactiveTitleText).Reverse(true).ToArgb(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "MenuHilight", Color.FromArgb(0, @default.MenuHilight).Reverse(true).ToArgb(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "TitleText", Color.FromArgb(0, @default.TitleText).Reverse(true).ToArgb(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "Window", Color.FromArgb(0, @default.Window).Reverse(true).ToArgb(), RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "WindowText", Color.FromArgb(0, @default.WindowText).Reverse(true).ToArgb(), RegistryValueKind.String);
                }

                else if (Program.Settings.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Erase)
                {
                    DelReg_AdministratorDeflector(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors", "Standard");
                }
            }
        }

        /// <summary>
        /// Broadcast user preference mask from current user to all users (and LogonUI screen)
        /// </summary>
        /// <param name="TreeView">treeView used as theme log</param>
        public void Broadcast_UPM_ToDefUsers(TreeView TreeView = null)
        {
            if (Program.Settings.ThemeApplyingBehavior.UPM_HKU_DEFAULT)
            {
                object source = GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "UserPreferencesMask", null);

                if (source is not null)
                {
                    byte[] bytes = new byte[] { };

                    try
                    {
                        bytes = source as byte[];
                    }
                    catch
                    {
                        // Couldn't cast registry source into byte[], so it will be dismissed.
                    }
                    finally 
                    {
                        if (bytes is not null && bytes.Length > 0) EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "UserPreferencesMask", bytes, RegistryValueKind.Binary);
                    }
                }
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
        public readonly object Clone()
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

        /// <summary>
        /// Retrun Win32UI structure into a string in format of Microsoft theme file (*.theme)
        /// </summary>
        /// <param name="metricsFonts">MetricsFonts structure to be included in the string</param>
        public readonly string ToString(Theme.Structures.MetricsFonts? metricsFonts = null)
        {
            StringBuilder s = new();
            s.Clear();
            s.AppendLine($"; {(string.Format(Program.Lang.OldMSTheme_Copyrights, DateTime.Now.Year))}");
            s.AppendLine($"; {(string.Format(Program.Lang.OldMSTheme_ProgrammedBy, Application.CompanyName))}");
            s.AppendLine($"; {(string.Format(Program.Lang.OldMSTheme_CreatedFromAppVer, Program.TM.Info.AppVersion))}");
            s.AppendLine($"; {(string.Format(Program.Lang.OldMSTheme_CreatedBy, Program.TM.Info.Author))}");
            s.AppendLine($"; {(string.Format(Program.Lang.OldMSTheme_ThemeName, Program.TM.Info.ThemeName))}");
            s.AppendLine($"; {(string.Format(Program.Lang.OldMSTheme_ThemeVersion, Program.TM.Info.ThemeVersion))}");
            s.AppendLine(string.Empty);

            s.AppendLine($"[Control Panel\\Colors]");
            s.AppendLine($"ActiveTitle={ActiveTitle.ToWin32Reg()}");
            s.AppendLine($"Background={Background.ToWin32Reg()}");
            s.AppendLine($"Hilight={Hilight.ToWin32Reg()}");
            s.AppendLine($"HilightText={HilightText.ToWin32Reg()}");
            s.AppendLine($"TitleText={TitleText.ToWin32Reg()}");
            s.AppendLine($"Window={Window.ToWin32Reg()}");
            s.AppendLine($"WindowText={WindowText.ToWin32Reg()}");
            s.AppendLine($"Scrollbar={Scrollbar.ToWin32Reg()}");
            s.AppendLine($"InactiveTitle={InactiveTitle.ToWin32Reg()}");
            s.AppendLine($"Menu={Menu.ToWin32Reg()}");
            s.AppendLine($"WindowFrame={WindowFrame.ToWin32Reg()}");
            s.AppendLine($"MenuText={MenuText.ToWin32Reg()}");
            s.AppendLine($"ActiveBorder={ActiveBorder.ToWin32Reg()}");
            s.AppendLine($"InactiveBorder={InactiveBorder.ToWin32Reg()}");
            s.AppendLine($"AppWorkspace={AppWorkspace.ToWin32Reg()}");
            s.AppendLine($"ButtonFace={ButtonFace.ToWin32Reg()}");
            s.AppendLine($"ButtonShadow={ButtonShadow.ToWin32Reg()}");
            s.AppendLine($"GrayText={GrayText.ToWin32Reg()}");
            s.AppendLine($"ButtonText={ButtonText.ToWin32Reg()}");
            s.AppendLine($"InactiveTitleText={InactiveTitleText.ToWin32Reg()}");
            s.AppendLine($"ButtonHilight={ButtonHilight.ToWin32Reg()}");
            s.AppendLine($"ButtonDkShadow={ButtonDkShadow.ToWin32Reg()}");
            s.AppendLine($"ButtonLight={ButtonLight.ToWin32Reg()}");
            s.AppendLine($"InfoText={InfoText.ToWin32Reg()}");
            s.AppendLine($"InfoWindow={InfoWindow.ToWin32Reg()}");
            s.AppendLine($"GradientActiveTitle={GradientActiveTitle.ToWin32Reg()}");
            s.AppendLine($"GradientInactiveTitle={GradientInactiveTitle.ToWin32Reg()}");
            s.AppendLine($"ButtonAlternateFace={ButtonAlternateFace.ToWin32Reg()}");
            s.AppendLine($"HotTrackingColor={HotTrackingColor.ToWin32Reg()}");
            s.AppendLine($"MenuHilight={MenuHilight.ToWin32Reg()}");
            s.AppendLine($"MenuBar={MenuBar.ToWin32Reg()}");
            s.AppendLine($"Desktop={Desktop.ToWin32Reg()}");
            s.AppendLine(string.Empty);

            if (metricsFonts is not null && metricsFonts.HasValue)
            {
                NONCLIENTMETRICS ncm = new();
                ncm.cbSize = (uint)Marshal.SizeOf(ncm);
                ncm.iCaptionWidth = metricsFonts.Value.CaptionWidth;
                ncm.iCaptionHeight = metricsFonts.Value.CaptionHeight;
                ncm.iSMCaptionWidth = metricsFonts.Value.SmCaptionWidth;
                ncm.iSMCaptionHeight = metricsFonts.Value.SmCaptionHeight;
                ncm.iBorderWidth = metricsFonts.Value.BorderWidth;
                ncm.iPaddedBorderWidth = metricsFonts.Value.PaddedBorderWidth;
                ncm.iMenuWidth = metricsFonts.Value.MenuWidth;
                ncm.iMenuHeight = metricsFonts.Value.MenuHeight;
                ncm.iScrollWidth = metricsFonts.Value.ScrollWidth;
                ncm.iScrollHeight = metricsFonts.Value.ScrollHeight;

                GDI32.LogFont lfCaptionFont = new();
                metricsFonts.Value.CaptionFont.ToLogFont(lfCaptionFont);
                ncm.lfCaptionFont = lfCaptionFont;

                GDI32.LogFont lfMenuFont = new();
                metricsFonts.Value.MenuFont.ToLogFont(lfMenuFont);
                ncm.lfMenuFont = lfMenuFont;

                GDI32.LogFont lfMessageFont = new();
                metricsFonts.Value.MessageFont.ToLogFont(lfMessageFont);
                ncm.lfMessageFont = lfMessageFont;

                GDI32.LogFont lfSMCaptionFont = new();
                metricsFonts.Value.SmCaptionFont.ToLogFont(lfSMCaptionFont);
                ncm.lfSMCaptionFont = lfSMCaptionFont;

                GDI32.LogFont lfStatusFont = new();
                metricsFonts.Value.StatusFont.ToLogFont(lfStatusFont);
                ncm.lfStatusFont = lfStatusFont;


                ICONMETRICS icm = new();
                icm.cbSize = (uint)Marshal.SizeOf(icm);
                icm.iHorzSpacing = metricsFonts.Value.IconSpacing;
                icm.iVertSpacing = metricsFonts.Value.IconVerticalSpacing;

                GDI32.LogFont lfIconFont = new();
                metricsFonts.Value.IconFont.ToLogFont(lfIconFont);
                icm.lfFont = lfIconFont;

                s.AppendLine(string.Format("[Metrics]"));
                s.AppendLine(string.Format($"IconMetrics={string.Join(" ", icm.ToByteArray())}"));
                s.AppendLine(string.Format($"NonClientMetrics={string.Join(" ", ncm.ToByteArray())}"));
                s.AppendLine(string.Empty);
            }

            s.AppendLine(string.Format("[MasterThemeSelector]"));
            s.AppendLine(string.Format("MTSM=DABJDKT"));

            s.AppendLine(string.Empty);
            s.AppendLine(@"[Control Panel\Desktop]");
            s.AppendLine("Wallpaper=");
            s.AppendLine("TileWallpaper=0");
            s.AppendLine("WallpaperStyle=10");
            s.AppendLine("Pattern=");
            s.AppendLine(string.Empty);

            s.AppendLine("[VisualStyles]");
            s.AppendLine("Path=");
            s.AppendLine("ColorStyle=@themeui.dll,-854");
            s.AppendLine("Size=@themeui.dll,-2019");
            s.AppendLine("Transparency=0");

            return s.ToString();
        }
    }
}
