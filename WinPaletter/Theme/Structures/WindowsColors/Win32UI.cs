using Devcorp.Controls.VisualStyles;
using libmsstyle;
using Microsoft.Win32;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.NativeMethods.User32;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing classic Windows colors
    /// </summary>
    public class Win32UI : ManagerBase<Win32UI>
    {
        /// <summary> Controls if Classic Colors editing is enabled or not </summary> 
        public bool Enabled { get; set; } = true;

        /// <summary>If disabled, classic 3D effects will be made to menus and menu items selection</summary>
        public bool EnableTheming { get; set; } = true;

        /// <summary>Enable titlebar gradience</summary>
        public bool EnableGradient { get; set; } = true;

        /// <summary>Color of active window border</summary>
        public Color ActiveBorder { get; set; } = Color.FromArgb(180, 180, 180);

        /// <summary>Active titlebar main color</summary>
        public Color ActiveTitle { get; set; } = Color.FromArgb(153, 180, 209);

        ///
        public Color AppWorkspace { get; set; } = Color.FromArgb(171, 171, 171);

        ///
        public Color Background { get; set; } = Color.FromArgb(0, 0, 0);

        ///
        public Color ButtonAlternateFace { get; set; } = Color.FromArgb(0, 0, 0);

        ///
        public Color ButtonDkShadow { get; set; } = Color.FromArgb(105, 105, 105);

        ///
        public Color ButtonFace { get; set; } = Color.FromArgb(240, 240, 240);

        ///
        public Color ButtonHilight { get; set; } = Color.FromArgb(255, 255, 255);

        ///
        public Color ButtonLight { get; set; } = Color.FromArgb(227, 227, 227);

        ///
        public Color ButtonShadow { get; set; } = Color.FromArgb(160, 160, 160);

        ///
        public Color ButtonText { get; set; } = Color.FromArgb(0, 0, 0);

        /// <summary>Second color for gradience in active titlebar</summary>
        public Color GradientActiveTitle { get; set; } = Color.FromArgb(185, 209, 234);

        /// <summary>Second color for gradience in inactive titlebar</summary>
        public Color GradientInactiveTitle { get; set; } = Color.FromArgb(215, 228, 242);

        /// <summary>Used in disabled items</summary>
        public Color GrayText { get; set; } = Color.FromArgb(109, 109, 109);

        ///
        public Color HilightText { get; set; } = Color.FromArgb(255, 255, 255);

        /// <summary>Color of selection rectangles and hyperlinks</summary>
        public Color HotTrackingColor { get; set; } = Color.FromArgb(0, 102, 204);

        /// <summary>Color of inactive window border</summary>
        public Color InactiveBorder { get; set; } = Color.FromArgb(244, 247, 252);

        /// <summary>Inactive titlebar main color</summary>
        public Color InactiveTitle { get; set; } = Color.FromArgb(191, 205, 219);

        /// <summary>Inactive titlebar text</summary>
        public Color InactiveTitleText { get; set; } = Color.FromArgb(0, 0, 0);

        ///
        public Color InfoText { get; set; } = Color.FromArgb(0, 0, 0);

        ///
        public Color InfoWindow { get; set; } = Color.FromArgb(255, 255, 225);

        /// <summary>Color of cascaded menu</summary>
        public Color Menu { get; set; } = Color.FromArgb(240, 240, 240);

        /// <summary>Color of menu bar</summary>
        public Color MenuBar { get; set; } = Color.FromArgb(240, 240, 240);

        ///
        public Color MenuText { get; set; } = Color.FromArgb(0, 0, 0);

        /// <summary>Obsolete: was used in Windows 9x</summary>
        public Color Scrollbar { get; set; } = Color.FromArgb(200, 200, 200);

        /// <summary>Active titlebar text</summary>
        public Color TitleText { get; set; } = Color.FromArgb(0, 0, 0);

        ///
        public Color Window { get; set; } = Color.FromArgb(255, 255, 255);

        /// <summary>Color of rectangle surrounding a pressed button</summary>
        public Color WindowFrame { get; set; } = Color.FromArgb(100, 100, 100);

        ///
        public Color WindowText { get; set; } = Color.FromArgb(0, 0, 0);

        ///
        public Color Hilight { get; set; } = Color.FromArgb(0, 120, 215);

        ///
        public Color MenuHilight { get; set; } = Color.FromArgb(0, 120, 215);

        ///
        public Color Desktop { get; set; } = Color.FromArgb(0, 0, 0);

        /// <summary>
        /// Creates a new instance of Win32UI structure with default values
        /// </summary>
        public Win32UI() { }

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

        public static Color GetColor(SysColorIndex index)
        {
            int colorRef = GetSysColor((int)index);
            byte r = (byte)(colorRef & 0xFF);
            byte g = (byte)((colorRef >> 8) & 0xFF);
            byte b = (byte)((colorRef >> 16) & 0xFF);
            return Color.FromArgb(r, g, b);
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
                        Program.Log?.Write(LogEventLevel.Information, $"Loading Windows classic colors (Win32) from registry.");

                        Enabled = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\WindowsColorsThemes\ClassicColors", string.Empty, true);

                        // Set some flags like EnableTheming and EnableGradient
                        bool enableTheming = EnableTheming;
                        bool enableGradient = EnableGradient;
                        SystemParametersInfo(SPI.SPI_GETFLATMENU, 0, ref enableTheming, SPIF.SPIF_NONE);
                        SystemParametersInfo(SPI.SPI_GETGRADIENTCAPTIONS, 0, ref enableGradient, SPIF.SPIF_NONE);

                        ActiveTitle = GetColor(SysColorIndex.ActiveCaption);
                        AppWorkspace = GetColor(SysColorIndex.AppWorkspace);
                        Background = GetColor(SysColorIndex.Background);
                        ButtonDkShadow = GetColor(SysColorIndex.ButtonDkShadow);
                        ButtonFace = GetColor(SysColorIndex.ButtonFace);
                        ButtonHilight = GetColor(SysColorIndex.ButtonHilight);
                        ButtonLight = GetColor(SysColorIndex.ButtonLight);
                        ButtonShadow = GetColor(SysColorIndex.ButtonShadow);
                        ButtonText = GetColor(SysColorIndex.ButtonText);
                        GradientActiveTitle = GetColor(SysColorIndex.GradientActiveTitle);
                        GradientInactiveTitle = GetColor(SysColorIndex.GradientInactiveTitle);
                        GrayText = GetColor(SysColorIndex.GrayText);
                        Hilight = GetColor(SysColorIndex.Hilight);
                        HilightText = GetColor(SysColorIndex.HilightText);
                        HotTrackingColor = GetColor(SysColorIndex.HotTrackingColor);
                        ActiveBorder = GetColor(SysColorIndex.ActiveBorder);
                        InactiveBorder = GetColor(SysColorIndex.InactiveBorder);
                        InactiveTitle = GetColor(SysColorIndex.InactiveCaption);
                        InactiveTitleText = GetColor(SysColorIndex.InactiveCaptionText);
                        InfoText = GetColor(SysColorIndex.InfoText);
                        InfoWindow = GetColor(SysColorIndex.InfoWindow);
                        Menu = GetColor(SysColorIndex.Menu);
                        MenuBar = GetColor(SysColorIndex.MenuBar);
                        MenuText = GetColor(SysColorIndex.MenuText);
                        Scrollbar = GetColor(SysColorIndex.Scrollbar);
                        TitleText = GetColor(SysColorIndex.CaptionText);
                        Window = GetColor(SysColorIndex.Window);
                        WindowFrame = GetColor(SysColorIndex.WindowFrame);
                        WindowText = GetColor(SysColorIndex.WindowText);
                        MenuHilight = GetColor(SysColorIndex.MenuHilight);
                        Desktop = GetColor(SysColorIndex.Background);

                        ButtonAlternateFace = ReadReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonAlternateFace", ButtonAlternateFace.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)).ToColorFromWin32();

                        break;
                    }

                case Sources.VisualStyles:
                    {
                        Program.Log?.Write(LogEventLevel.Information, $"Loading Windows classic colors (Win32) from the provided Visual Styles.");

                        // There are some commented values that are not supported by DevCorp visual style processor

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
                        // ButtonText = _vs.Palette.MenuText
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
        /// Represents system color indices used to retrieve system-defined colors.
        /// </summary>
        /// <remarks>The <see cref="SysColorIndex"/> enumeration defines indices corresponding to various
        /// system-defined colors. These indices can be used with methods such as <see
        /// cref="System.Drawing.SystemColors"/> or platform-specific APIs to retrieve the actual color values. The
        /// colors represented by these indices are typically used for rendering user interface elements and may vary
        /// depending on the system theme or user settings.</remarks>
        public enum SysColorIndex
        {
            Scrollbar = 0,
            Background = 1,
            ActiveCaption = 2,
            InactiveCaption = 3,
            Menu = 4,
            Window = 5,
            WindowFrame = 6,
            MenuText = 7,
            WindowText = 8,
            CaptionText = 9,
            ActiveBorder = 10,
            InactiveBorder = 11,
            AppWorkspace = 12,
            Hilight = 13,
            HilightText = 14,
            ButtonFace = 15,
            ButtonShadow = 16,
            GrayText = 17,
            ButtonText = 18,
            InactiveCaptionText = 19,
            ButtonHilight = 20,
            ButtonDkShadow = 21,
            ButtonLight = 22,
            InfoText = 23,
            InfoWindow = 24,
            HotTrackingColor = 26,
            GradientActiveTitle = 27,
            GradientInactiveTitle = 28,
            MenuHilight = 29,
            MenuBar = 30,
            ButtonAlternateFace = 100,
        }

        /// <summary>
        /// Saves Win32UI data into registry
        /// </summary>
        /// <param name="treeView">treeView used as theme log</param>
        public void Apply(TreeView treeView = null)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Saving Windows classic colors (Win32) into registry and by using User32.SetSysColors and User32.SystemParametersInfo");

            SaveToggleState(treeView);

            if (Enabled)
            {
                #region Colors override by msstyles

                // Get the visual style from the current theme, helps to override colors if this is enabled
                VisualStyles _vs = new();

                if (Program.TM is not null)
                {
                    _vs = Program.WindowStyle switch
                    {
                        WindowStyle.W12 => Program.TM.Windows12.VisualStyles,
                        WindowStyle.W11 => Program.TM.Windows11.VisualStyles,
                        WindowStyle.W10 => Program.TM.Windows10.VisualStyles,
                        WindowStyle.W81 => Program.TM.Windows81.VisualStyles,
                        WindowStyle.W7 => Program.TM.Windows7.VisualStyles,
                        WindowStyle.WVista => Program.TM.WindowsVista.VisualStyles,
                        WindowStyle.WXP => Program.TM.WindowsXP.VisualStyles,
                        _ => Program.TM.Windows12.VisualStyles,
                    };
                }

                // If visual styles are enabled and colors are overriden by msstyles, then apply them
                if (_vs.Enabled && _vs.OverrideColors)
                {
                    Program.Log?.Write(LogEventLevel.Information, $"Applying colors from visual styles file instead of the aspect itself: {_vs.ThemeFile}");

                    if (File.Exists(_vs.ThemeFile))
                    {
                        try
                        {
                            using (VisualStyle vs = new(_vs.ThemeFile))
                            {
                                this.CopyFrom(vs.ClassicColors());
                                Enabled = true;
                            }
                        }
                        catch
                        {
                            string theme = _vs.ThemeFile;
                            if (Path.GetExtension(theme).ToLower() == ".msstyles")
                            {
                                File.WriteAllText($@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={theme}{"\r\n"}ColorStyle=NormalColor{"\r\n"}Size=NormalSize");
                                theme = $@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme";
                            }

                            if (File.Exists(theme))
                            {
                                using (VisualStyleFile vs = new(theme))
                                {
                                    try
                                    {
                                        Load(Sources.VisualStyles, vs.Metrics);
                                    }
                                    catch { } // Couldn't load visual styles File.
                                }
                            }
                        }
                    }
                }

                #endregion

                // Determine if the current theme is classic or not
                bool isClassic = string.IsNullOrEmpty(UxTheme.GetCurrentVS().Item1);

                List<System.Windows.Forms.Form> fl = [];
                fl.Clear();

                // Hiding forms is added as there is a bug occurs when a classic theme applied on classic Windows mode
                if (isClassic)
                {
                    Program.Log?.Write(LogEventLevel.Information, $"Hiding all visible forms to avoid a bug in classic mode when it is enabled.");

                    foreach (System.Windows.Forms.Form f in Application.OpenForms)
                    {
                        if (f.Visible)
                        {
                            f.SuspendLayout();
                            f.Visible = false;
                            fl.Add(f);
                        }
                    }
                }

                // Impersonate the selected user to apply the colors correctly
                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    List<int> C1 = [];
                    List<uint> C2 = [];

                    C1.Clear();
                    C2.Clear();

                    C1.Add((int)SysColorIndex.Hilight);
                    C2.Add((uint)ColorTranslator.ToWin32(Hilight));

                    C1.Add((int)SysColorIndex.HilightText);
                    C2.Add((uint)ColorTranslator.ToWin32(HilightText));

                    C1.Add((int)SysColorIndex.CaptionText);
                    C2.Add((uint)ColorTranslator.ToWin32(TitleText));

                    C1.Add((int)SysColorIndex.GrayText);
                    C2.Add((uint)ColorTranslator.ToWin32(GrayText));

                    C1.Add((int)SysColorIndex.InactiveBorder);
                    C2.Add((uint)ColorTranslator.ToWin32(InactiveBorder));

                    C1.Add((int)SysColorIndex.InactiveCaption);
                    C2.Add((uint)ColorTranslator.ToWin32(InactiveTitle));

                    C1.Add((int)SysColorIndex.ActiveCaption);
                    C2.Add((uint)ColorTranslator.ToWin32(ActiveTitle));

                    C1.Add((int)SysColorIndex.ActiveBorder);
                    C2.Add((uint)ColorTranslator.ToWin32(ActiveBorder));

                    C1.Add((int)SysColorIndex.AppWorkspace);
                    C2.Add((uint)ColorTranslator.ToWin32(AppWorkspace));

                    C1.Add((int)SysColorIndex.Background);
                    C2.Add((uint)ColorTranslator.ToWin32(Background));

                    C1.Add((int)SysColorIndex.GradientActiveTitle);
                    C2.Add((uint)ColorTranslator.ToWin32(GradientActiveTitle));

                    C1.Add((int)SysColorIndex.GradientInactiveTitle);
                    C2.Add((uint)ColorTranslator.ToWin32(GradientInactiveTitle));

                    C1.Add((int)SysColorIndex.InactiveCaptionText);
                    C2.Add((uint)ColorTranslator.ToWin32(InactiveTitleText));

                    C1.Add((int)SysColorIndex.InfoWindow);
                    C2.Add((uint)ColorTranslator.ToWin32(InfoWindow));

                    C1.Add((int)SysColorIndex.InfoText);
                    C2.Add((uint)ColorTranslator.ToWin32(InfoText));

                    C1.Add((int)SysColorIndex.Menu);
                    C2.Add((uint)ColorTranslator.ToWin32(Menu));

                    C1.Add((int)SysColorIndex.MenuText);
                    C2.Add((uint)ColorTranslator.ToWin32(MenuText));

                    C1.Add((int)SysColorIndex.Scrollbar);
                    C2.Add((uint)ColorTranslator.ToWin32(Scrollbar));

                    C1.Add((int)SysColorIndex.Window);
                    C2.Add((uint)ColorTranslator.ToWin32(Window));

                    C1.Add((int)SysColorIndex.WindowFrame);
                    C2.Add((uint)ColorTranslator.ToWin32(WindowFrame));

                    C1.Add((int)SysColorIndex.WindowText);
                    C2.Add((uint)ColorTranslator.ToWin32(WindowText));

                    C1.Add((int)SysColorIndex.HotTrackingColor);
                    C2.Add((uint)ColorTranslator.ToWin32(HotTrackingColor));

                    C1.Add((int)SysColorIndex.MenuHilight);
                    C2.Add((uint)ColorTranslator.ToWin32(MenuHilight));

                    C1.Add((int)SysColorIndex.MenuBar);
                    C2.Add((uint)ColorTranslator.ToWin32(MenuBar));

                    C1.Add((int)SysColorIndex.ButtonFace);
                    C2.Add((uint)ColorTranslator.ToWin32(ButtonFace));

                    C1.Add((int)SysColorIndex.ButtonHilight);
                    C2.Add((uint)ColorTranslator.ToWin32(ButtonHilight));

                    C1.Add((int)SysColorIndex.ButtonShadow);
                    C2.Add((uint)ColorTranslator.ToWin32(ButtonShadow));

                    C1.Add((int)SysColorIndex.ButtonText);
                    C2.Add((uint)ColorTranslator.ToWin32(ButtonText));

                    C1.Add((int)SysColorIndex.ButtonDkShadow);
                    C2.Add((uint)ColorTranslator.ToWin32(ButtonDkShadow));

                    C1.Add((int)SysColorIndex.ButtonAlternateFace);
                    C2.Add((uint)ColorTranslator.ToWin32(ButtonAlternateFace));

                    C1.Add((int)SysColorIndex.ButtonLight);
                    C2.Add((uint)ColorTranslator.ToWin32(ButtonLight));

                    // Broadcast the colors to all windows
                    SetSysColors(C1.Count, [.. C1], [.. C2]);

                    SystemParametersInfo(treeView, SPI.SPI_SETFLATMENU, 0, EnableTheming, SPIF.SPIF_UPDATEINIFILE);
                    SystemParametersInfo(treeView, SPI.SPI_SETGRADIENTCAPTIONS, 0, EnableGradient, SPIF.SPIF_UPDATEINIFILE);

                    wic.Undo();
                }

                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ActiveBorder", ActiveBorder.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ActiveTitle", ActiveTitle.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "AppWorkspace", AppWorkspace.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "Background", Background.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonAlternateFace", ButtonAlternateFace.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonDkShadow", ButtonDkShadow.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonFace", ButtonFace.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonHilight", ButtonHilight.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonLight", ButtonLight.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonShadow", ButtonShadow.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonText", ButtonText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "GradientActiveTitle", GradientActiveTitle.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "GradientInactiveTitle", GradientInactiveTitle.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "GrayText", GrayText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "HilightText", HilightText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "HotTrackingColor", HotTrackingColor.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "InactiveBorder", InactiveBorder.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitle", InactiveTitle.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitleText", InactiveTitleText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "InfoText", InfoText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "InfoWindow", InfoWindow.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "Menu", Menu.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "MenuBar", MenuBar.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "MenuText", MenuText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "Scrollbar", Scrollbar.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "TitleText", TitleText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "Window", Window.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "WindowFrame", WindowFrame.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "WindowText", WindowText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "Hilight", Hilight.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "MenuHilight", MenuHilight.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "Desktop", Desktop.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);

                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ActiveBorder", ActiveBorder.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ActiveTitle", ActiveTitle.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "AppWorkspace", AppWorkspace.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Background", Background.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonAlternateFace", ButtonAlternateFace.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonDkShadow", ButtonDkShadow.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonFace", ButtonFace.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonHilight", ButtonHilight.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonLight", ButtonLight.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonShadow", ButtonShadow.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonText", ButtonText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "GradientActiveTitle", GradientActiveTitle.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "GradientInactiveTitle", GradientInactiveTitle.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "GrayText", GrayText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "HilightText", HilightText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "HotTrackingColor", HotTrackingColor.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "InactiveBorder", InactiveBorder.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "InactiveTitle", InactiveTitle.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "InactiveTitleText", InactiveTitleText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "InfoText", InfoText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "InfoWindow", InfoWindow.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Menu", Menu.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "MenuBar", MenuBar.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "MenuText", MenuText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Scrollbar", Scrollbar.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "TitleText", TitleText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Window", Window.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "WindowFrame", WindowFrame.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "WindowText", WindowText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Hilight", Hilight.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "MenuHilight", MenuHilight.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Desktop", Desktop.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);

                // Fix bug in WinForms when classic theme applied on classic Windows mode
                if (isClassic)
                {
                    if (fl.Count > 0)
                    {
                        Program.Log?.Write(LogEventLevel.Information, $"Restoring visibility of all forms that were hidden before applying the classic theme colors.");

                        Thread.Sleep(100);
                        for (int i = 0, loopTo = fl.Count - 1; i <= loopTo; i++)
                        {
                            fl[i].Visible = true;
                            fl[i].ResumeLayout();
                            fl[i].Refresh();
                        }
                    }
                }

                // Apply the colors to the default user if overriden in WinPaletter settings
                if (Program.Settings.ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                {
                    Program.Log?.Write(LogEventLevel.Information, $"Applying classic colors to the default user registry (HKEY_USERS\\.DEFAULT) as it is enabled in WinPaletter settings.");

                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ActiveBorder", ActiveBorder.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ActiveTitle", ActiveTitle.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "AppWorkspace", AppWorkspace.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "Background", Background.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonAlternateFace", ButtonAlternateFace.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonDkShadow", ButtonDkShadow.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonFace", ButtonFace.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonHilight", ButtonHilight.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonLight", ButtonLight.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonShadow", ButtonShadow.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonText", ButtonText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "GradientActiveTitle", GradientActiveTitle.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "GradientInactiveTitle", GradientInactiveTitle.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "GrayText", GrayText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "HilightText", HilightText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "HotTrackingColor", HotTrackingColor.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "InactiveBorder", InactiveBorder.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "InactiveTitle", InactiveTitle.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "InactiveTitleText", InactiveTitleText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "InfoText", InfoText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "InfoWindow", InfoWindow.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "Menu", Menu.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "MenuBar", MenuBar.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "MenuText", MenuText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "Scrollbar", Scrollbar.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "TitleText", TitleText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "Window", Window.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "WindowFrame", WindowFrame.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "WindowText", WindowText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "Hilight", Hilight.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "MenuHilight", MenuHilight.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "Desktop", Desktop.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true), RegistryValueKind.String);
                }

                // Override some colors in the HKEY_Local_Machine registry if overriden in WinPaletter settings
                if (Program.Settings.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                {
                    Program.Log?.Write(LogEventLevel.Information, $"Applying classic colors to the HKEY_LOCAL_MACHINE registry as it is enabled in WinPaletter settings.");

                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ActiveTitle", Color.FromArgb(0, ActiveTitle).Reverse().ToArgb(), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ButtonFace", Color.FromArgb(0, ButtonFace).Reverse().ToArgb(), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ButtonText", Color.FromArgb(0, ButtonText).Reverse().ToArgb(), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "GrayText", Color.FromArgb(0, GrayText).Reverse().ToArgb(), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "Hilight", Color.FromArgb(0, Hilight).Reverse().ToArgb(), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "HilightText", Color.FromArgb(0, HilightText).Reverse().ToArgb(), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "HotTrackingColor", Color.FromArgb(0, HotTrackingColor).Reverse().ToArgb(), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "InactiveTitle", Color.FromArgb(0, InactiveTitle).Reverse().ToArgb(), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "InactiveTitleText", Color.FromArgb(0, InactiveTitleText).Reverse().ToArgb(), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "MenuHilight", Color.FromArgb(0, MenuHilight).Reverse().ToArgb(), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "TitleText", Color.FromArgb(0, TitleText).Reverse().ToArgb(), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "Window", Color.FromArgb(0, Window).Reverse().ToArgb(), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "WindowText", Color.FromArgb(0, WindowText).Reverse().ToArgb(), RegistryValueKind.String);
                }

                else if (Program.Settings.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults)
                {
                    Program.Log?.Write(LogEventLevel.Information, $"Restoring classic colors to the default values in the HKEY_LOCAL_MACHINE registry as it is enabled in WinPaletter settings.");

                    Win32UI @default;
                    if (Program.WindowStyle == WindowStyle.W12)
                    {
                        @default = Default.Windows12.Win32;
                    }
                    else if (Program.WindowStyle == WindowStyle.W11)
                    {
                        @default = Default.Windows11.Win32;
                    }
                    else if (Program.WindowStyle == WindowStyle.W10)
                    {
                        @default = Default.Windows10.Win32;
                    }
                    else if (Program.WindowStyle == WindowStyle.W81)
                    {
                        @default = Default.Windows81.Win32;
                    }
                    else if (Program.WindowStyle == WindowStyle.W7)
                    {
                        @default = Default.Windows7.Win32;
                    }
                    else if (Program.WindowStyle == WindowStyle.WVista)
                    {
                        @default = Default.WindowsVista.Win32;
                    }
                    else if (Program.WindowStyle == WindowStyle.WXP)
                    {
                        @default = Default.WindowsXP.Win32;
                    }
                    else
                    {
                        @default = Default.Windows11.Win32;
                    }

                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ActiveTitle", Color.FromArgb(0, @default.ActiveTitle).Reverse().ToArgb(), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ButtonFace", Color.FromArgb(0, @default.ButtonFace).Reverse().ToArgb(), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ButtonText", Color.FromArgb(0, @default.ButtonText).Reverse().ToArgb(), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "GrayText", Color.FromArgb(0, @default.GrayText).Reverse().ToArgb(), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "Hilight", Color.FromArgb(0, @default.Hilight).Reverse().ToArgb(), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "HilightText", Color.FromArgb(0, @default.HilightText).Reverse().ToArgb(), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "HotTrackingColor", Color.FromArgb(0, @default.HotTrackingColor).Reverse().ToArgb(), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "InactiveTitle", Color.FromArgb(0, @default.InactiveTitle).Reverse().ToArgb(), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "InactiveTitleText", Color.FromArgb(0, @default.InactiveTitleText).Reverse().ToArgb(), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "MenuHilight", Color.FromArgb(0, @default.MenuHilight).Reverse().ToArgb(), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "TitleText", Color.FromArgb(0, @default.TitleText).Reverse().ToArgb(), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "Window", Color.FromArgb(0, @default.Window).Reverse().ToArgb(), RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "WindowText", Color.FromArgb(0, @default.WindowText).Reverse().ToArgb(), RegistryValueKind.String);
                }

                else if (Program.Settings.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Erase)
                {
                    Program.Log?.Write(LogEventLevel.Information, $"Erasing classic colors from the HKEY_LOCAL_MACHINE registry as it is enabled in WinPaletter settings.");

                    DeleteValueAsAdministrator(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors", "Standard");
                }
            }
        }

        /// <summary>
        /// Saves ClassicColors toggle state into registry
        /// </summary>
        public void SaveToggleState(TreeView treeView = null)
        {
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\WindowsColorsThemes\ClassicColors", string.Empty, Enabled);
        }

        /// <summary>
        /// Broadcast user preference mask from current user to all users (and LogonUI screen)
        /// </summary>
        /// <param name="treeView">treeView used as theme log</param>
        public void Broadcast_UPM_ToDefUsers(TreeView treeView = null)
        {
            if (Program.Settings.ThemeApplyingBehavior.UPM_HKU_DEFAULT)
            {
                object source = ReadReg<object>(@"HKEY_CURRENT_USER\Control Panel\Desktop", "UserPreferencesMask", null);

                if (source is not null)
                {
                    byte[] bytes = [];

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
                        if (bytes is not null && bytes.Length > 0) WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "UserPreferencesMask", bytes, RegistryValueKind.Binary);
                    }
                }
            }
        }

        /// <summary>
        /// Retrun Win32UI structure into a string in format of Microsoft theme File (*.theme)
        /// </summary>
        /// <param name="metricsFonts">MetricsFonts structure to be included in the string</param>
        /// <param name="icons">Icons structure to be included in the string</param>
        public string ToString(MetricsFonts metricsFonts = null, Icons icons = null)
        {
            metricsFonts??= Program.TM.MetricsFonts;
            icons ??= Program.TM.Icons;

            StringBuilder s = new();
            s.Clear();
            s.AppendLine($"; {string.Format(Program.Localization.Strings.MSTheme.Copyrights, DateTime.Now.Year)}");
            s.AppendLine($"; {string.Format(Program.Localization.Strings.MSTheme.ProgrammedBy, Application.CompanyName)}");
            s.AppendLine($"; {string.Format(Program.Localization.Strings.MSTheme.CreatedFromAppVer, Program.TM.Info.AppVersion)}");
            s.AppendLine($"; {string.Format(Program.Localization.Strings.MSTheme.CreatedBy, Program.TM.Info.Author)}");
            s.AppendLine($"; {string.Format(Program.Localization.Strings.MSTheme.ThemeName, Program.TM.Info.ThemeName)}");
            s.AppendLine($"; {string.Format(Program.Localization.Strings.MSTheme.ThemeVersion, Program.TM.Info.ThemeVersion)}");
            s.AppendLine();

            // XP requires this section first
            s.AppendLine("[Theme]");
            s.AppendLine($"DisplayName={Program.TM.Info.ThemeName}");
            s.AppendLine(";Recycle Bin");
            s.AppendLine($"[CLSID\\{Icons.DesktopCLSIDs.Where(x => x.Item2 == "Recycle Bin").FirstOrDefault().Item1}\\DefaultIcon]");
            s.AppendLine($"full={SysPaths.Normalize(icons.RecycleBinFull)}");
            s.AppendLine($"empty={SysPaths.Normalize(icons.RecycleBinEmpty)}");
            s.AppendLine();

            s.AppendLine($"[Control Panel\\Colors]");
            s.AppendLine($"ActiveTitle={ActiveTitle.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"Background={Background.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"Hilight={Hilight.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"HilightText={HilightText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"TitleText={TitleText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"Window={Window.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"WindowText={WindowText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"Scrollbar={Scrollbar.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"InactiveTitle={InactiveTitle.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"Menu={Menu.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"WindowFrame={WindowFrame.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"MenuText={MenuText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"ActiveBorder={ActiveBorder.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"InactiveBorder={InactiveBorder.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"AppWorkspace={AppWorkspace.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"ButtonFace={ButtonFace.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"ButtonShadow={ButtonShadow.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"GrayText={GrayText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"ButtonText={ButtonText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"InactiveTitleText={InactiveTitleText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"ButtonHilight={ButtonHilight.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"ButtonDkShadow={ButtonDkShadow.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"ButtonLight={ButtonLight.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"InfoText={InfoText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"InfoWindow={InfoWindow.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"GradientActiveTitle={GradientActiveTitle.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"GradientInactiveTitle={GradientInactiveTitle.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"ButtonAlternateFace={ButtonAlternateFace.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"HotTrackingColor={HotTrackingColor.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"MenuHilight={MenuHilight.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"MenuBar={MenuBar.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine($"Desktop={Desktop.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
            s.AppendLine();

            NONCLIENTMETRICS ncm = new();
            ncm.cbSize = (uint)Marshal.SizeOf(ncm);
            ncm.iCaptionWidth = metricsFonts.CaptionWidth;
            ncm.iCaptionHeight = metricsFonts.CaptionHeight;
            ncm.iSMCaptionWidth = metricsFonts.SmCaptionWidth;
            ncm.iSMCaptionHeight = metricsFonts.SmCaptionHeight;
            ncm.iBorderWidth = metricsFonts.BorderWidth;
            ncm.iPaddedBorderWidth = metricsFonts.PaddedBorderWidth;
            ncm.iMenuWidth = metricsFonts.MenuWidth;
            ncm.iMenuHeight = metricsFonts.MenuHeight;
            ncm.iScrollWidth = metricsFonts.ScrollWidth;
            ncm.iScrollHeight = metricsFonts.ScrollHeight;

            GDI32.LogFont lfCaptionFont = new();
            metricsFonts.CaptionFont.ToLogFont(lfCaptionFont);
            ncm.lfCaptionFont = lfCaptionFont;

            GDI32.LogFont lfMenuFont = new();
            metricsFonts.MenuFont.ToLogFont(lfMenuFont);
            ncm.lfMenuFont = lfMenuFont;

            GDI32.LogFont lfMessageFont = new();
            metricsFonts.MessageFont.ToLogFont(lfMessageFont);
            ncm.lfMessageFont = lfMessageFont;

            GDI32.LogFont lfSMCaptionFont = new();
            metricsFonts.SmCaptionFont.ToLogFont(lfSMCaptionFont);
            ncm.lfSMCaptionFont = lfSMCaptionFont;

            GDI32.LogFont lfStatusFont = new();
            metricsFonts.StatusFont.ToLogFont(lfStatusFont);
            ncm.lfStatusFont = lfStatusFont;

            ICONMETRICS icm = new();
            icm.cbSize = (uint)Marshal.SizeOf(icm);
            icm.iHorzSpacing = metricsFonts.IconSpacing;
            icm.iVertSpacing = metricsFonts.IconVerticalSpacing;

            GDI32.LogFont lfIconFont = new();
            metricsFonts.IconFont.ToLogFont(lfIconFont);
            icm.lfFont = lfIconFont;

            s.AppendLine(string.Format("[Metrics]"));
            s.AppendLine(string.Format($"IconMetrics={string.Join(" ", icm.ToByteArray())}"));
            s.AppendLine(string.Format($"NonClientMetrics={string.Join(" ", ncm.ToByteArray())}"));
            s.AppendLine();

            s.AppendLine(string.Format("[MasterThemeSelector]"));
            s.AppendLine(string.Format("MTSM=DABJDKT"));

            s.AppendLine();
            s.AppendLine(@"[Control Panel\Desktop]");
            s.AppendLine("Wallpaper=");
            s.AppendLine("TileWallpaper=0");
            s.AppendLine("WallpaperStyle=10");
            s.AppendLine("Pattern=");
            s.AppendLine();

            s.AppendLine("[VisualStyles]");
            s.AppendLine("Path=");
            s.AppendLine("ColorStyle=@themeui.dll,-854");
            s.AppendLine("Size=@themeui.dll,-2019");
            s.AppendLine("Transparency=0");

            return s.ToString();
        }
    }
}
