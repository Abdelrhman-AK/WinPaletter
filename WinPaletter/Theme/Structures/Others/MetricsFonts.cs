using Devcorp.Controls.VisualStyles;
using libmsstyle;
using Microsoft.Win32;
using Serilog.Events;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.NativeMethods.User32;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing Windows metrics and fonts
    /// </summary>
    public class MetricsFonts : ManagerBase<MetricsFonts>
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled { get; set; } = false;

        /// <summary>Window border width</summary>
        public int BorderWidth { get; set; } = 1;

        /// <summary>Titlebar (caption) height</summary>
        public int CaptionHeight { get; set; } = 22;

        /// <summary>Buttons in classic titlebar (caption) width</summary>
        public int CaptionWidth { get; set; } = 22;

        /// <summary>Horizontal spacing between desktop icons</summary>
        public int IconSpacing { get; set; } = 75;

        /// <summary>Vertical spacing between desktop icons</summary>
        public int IconVerticalSpacing { get; set; } = 75;

        /// <summary>Context menu height (if it is a horizontal menu)</summary>
        public int MenuHeight { get; set; } = 19;

        /// <summary>Context menu width (if it is a vertical menu)</summary>
        public int MenuWidth { get; set; } = 19;

        /// <summary>Padding width of a Window border</summary>
        public int PaddedBorderWidth { get; set; } = 4;

        /// <summary>Scroll bar height (if it is a horizontal scroll bar)</summary>
        public int ScrollHeight { get; set; } = 19;

        /// <summary>Scroll bar width (if it is a vertical scroll bar)</summary>
        public int ScrollWidth { get; set; } = 19;

        /// <summary>Titlebar (caption) height of a tool box window</summary>
        public int SmCaptionHeight { get; set; } = 22;

        /// <summary>Width of Buttons in classic titlebar (caption) of a tool box window</summary>
        public int SmCaptionWidth { get; set; } = 22;

        /// <summary>Size of desktop icons <c>(size x size)</c></summary>
        public int DesktopIconSize { get; set; } = 48;

        /// <summary>
        /// Size of shell icons (used in Windows XP)
        /// <br>Default: <b>32</b></br>
        /// </summary>
        public int ShellIconSize { get; set; } = 32;

        /// <summary>
        /// Size of small icons (used in Windows XP)
        /// <br>Default: <b>16</b></br>
        /// </summary>
        public int ShellSmallIconSize { get; set; } = 16;

        /// <summary>Make fonts pixelated like old versions of Windows (not ClearType)</summary>
        public bool Fonts_SingleBitPP { get; set; } = false;

        /// <summary>Titlebar (caption) font</summary>
        public Font CaptionFont { get; set; } = new("Segoe UI", 9f, FontStyle.Regular);

        /// <summary>Icons font</summary>
        public Font IconFont { get; set; } = new("Segoe UI", 9f, FontStyle.Regular);

        /// <summary>contextMenu font</summary>
        public Font MenuFont { get; set; } = new("Segoe UI", 9f, FontStyle.Regular);

        /// <summary>Message box font</summary>
        public Font MessageFont { get; set; } = new("Segoe UI", 9f, FontStyle.Regular);

        /// <summary>Titlebar (caption) font of a tool box window</summary>
        public Font SmCaptionFont { get; set; } = new("Segoe UI", 9f, FontStyle.Regular);

        /// <summary>Status bar (in the lower part of a window) font</summary>
        public Font StatusFont { get; set; } = new("Segoe UI", 9f, FontStyle.Regular);

        /// <summary>
        /// Font name that should substitutes MSShellDlg
        /// <br>Default value: <b>Microsoft Sans Serif</b></br>
        /// </summary>
        public string FontSubstitute_MSShellDlg { get; set; } = "Microsoft Sans Serif";

        /// <summary>
        /// Font name that should substitutes MSShellDlg2
        /// <br>Default value: <b>Tahoma</b></br>
        /// </summary>
        public string FontSubstitute_MSShellDlg2 { get; set; } = "Tahoma";

        /// <summary>
        /// Font name that should substitutes Segoe UI
        /// <br><b>Has no default value, it is empty</b></br>
        /// </summary>
        public string FontSubstitute_SegoeUI { get; set; } = string.Empty;

        /// <summary>
        /// Create a new MetricsFonts structure with default values
        /// </summary>
        public MetricsFonts() { }

        /// <summary>
        /// Overwrite current metrics values by values inside a visual styles File opened by Devcorp advanced UxTheme wrapper
        /// </summary>
        /// <param name="vs">Devcorp.Controls.VisualStyles.VisualStyleMetrics</param>
        public void Overwrite_Metrics(VisualStyleMetrics vs)
        {
            CaptionHeight = vs.Sizes.CaptionBarHeight;
            ScrollHeight = vs.Sizes.ScrollbarHeight;
            ScrollWidth = vs.Sizes.ScrollbarWidth;
            SmCaptionHeight = vs.Sizes.SMCaptionBarHeight;
            SmCaptionWidth = vs.Sizes.SMCaptionBarWidth;
        }

        /// <summary>
        /// Overwrite current fonts values by values inside a visual styles File opened by Devcorp advanced UxTheme wrapper
        /// </summary>
        /// <param name="vs">Devcorp.Controls.VisualStyles.VisualStyleMetrics</param>
        public void Overwrite_Fonts(VisualStyleMetrics vs)
        {
            CaptionFont = vs.Fonts.CaptionFont;
            IconFont = vs.Fonts.IconTitleFont;
            MenuFont = vs.Fonts.MenuFont;
            SmCaptionFont = vs.Fonts.SmallCaptionFont;
            MessageFont = vs.Fonts.MsgBoxFont;
            StatusFont = vs.Fonts.StatusFont;
        }

        /// <summary>
        /// Loads MetricsFonts data from registry
        /// </summary>
        /// <param name="default">Default MetricsFonts data structure</param>
        public void Load(MetricsFonts @default)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Loading Windows Metrics and Fonts settings from registry and User32.SystemParametersInfo");

            Enabled = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Metrics", string.Empty, @default.Enabled);
            BorderWidth = ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "BorderWidth", @default.BorderWidth * -15) / -15;
            CaptionHeight = ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionHeight", @default.CaptionHeight * -15) / -15;
            CaptionWidth = ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionWidth", @default.CaptionWidth * -15) / -15;
            IconSpacing = ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconSpacing", @default.IconSpacing * -15) / -15;
            IconVerticalSpacing = ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconVerticalSpacing", @default.IconVerticalSpacing * -15) / -15;
            MenuHeight = ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuHeight", @default.MenuHeight * -15) / -15;
            MenuWidth = ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuWidth", @default.MenuWidth * -15) / -15;
            PaddedBorderWidth = ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "PaddedBorderWidth", @default.PaddedBorderWidth * -15) / -15;
            ScrollHeight = ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollHeight", @default.ScrollHeight * -15) / -15;
            ScrollWidth = ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollWidth", @default.ScrollWidth * -15) / -15;
            SmCaptionHeight = ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionHeight", @default.SmCaptionHeight * -15) / -15;
            SmCaptionWidth = ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionWidth", @default.SmCaptionWidth * -15) / -15;

            // Get Shell Icon Size and Shell Small Icon Size only on Windows XP
            if (OS.WXP)
            {
                try { ShellIconSize = ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "Shell Icon Size", @default.ShellIconSize); }
                catch { ShellIconSize = @default.ShellIconSize; }

                try { ShellSmallIconSize = ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "Shell Small Icon Size", @default.ShellSmallIconSize); }
                catch { ShellSmallIconSize = @default.ShellSmallIconSize; }
            }

            DesktopIconSize = ReadReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\Bags\1\Desktop", "IconSize", @default.DesktopIconSize);
            FontSubstitute_MSShellDlg = ReadReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "MS Shell Dlg", @default.FontSubstitute_MSShellDlg);
            FontSubstitute_MSShellDlg2 = ReadReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "MS Shell Dlg 2", @default.FontSubstitute_MSShellDlg2);
            FontSubstitute_SegoeUI = ReadReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "Segoe UI", @default.FontSubstitute_SegoeUI);

            NONCLIENTMETRICS NCM = new();
            NCM.cbSize = (uint)Marshal.SizeOf(NCM);

            ICONMETRICS ICO = new();
            ICO.cbSize = (uint)Marshal.SizeOf(ICO);

            // Get fonts by using SystemParametersInfo
            SystemParametersInfo(null, SPI.SPI_GETICONMETRICS, (int)ICO.cbSize, ref ICO, SPIF.SPIF_NONE);
            SystemParametersInfo(null, SPI.SPI_GETNONCLIENTMETRICS, (int)NCM.cbSize, ref NCM, SPIF.SPIF_NONE);

            CaptionFont = Font.FromLogFont(NCM.lfCaptionFont);
            MenuFont = Font.FromLogFont(NCM.lfMenuFont);
            MessageFont = Font.FromLogFont(NCM.lfMessageFont);
            SmCaptionFont = Font.FromLogFont(NCM.lfSMCaptionFont);
            StatusFont = Font.FromLogFont(NCM.lfStatusFont);
            IconFont = Font.FromLogFont(ICO.lfFont);

            // Get font smoothing state by using both registry and SystemParametersInfo
            bool temp = false;
            SystemParametersInfo(SPI.SPI_GETFONTSMOOTHING, default, ref temp, SPIF.SPIF_NONE);
            Fonts_SingleBitPP = !temp || ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "FontSmoothingType", OS.WXP ? 1 : 2) != 2;
        }

        /// <summary>
        /// Saves MetricsFonts data into registry
        /// </summary>
        /// <param name="treeView">treeView used as theme log</param>
        public async void Apply(TreeView treeView = null)
        {
            try
            {
                Program.Log?.Write(LogEventLevel.Information, $"Saving Windows Metrics and Fonts settings into registry and by using User32.SystemParametersInfo");

                SaveToggleState(treeView);

                if (Enabled)
                {
                    #region Metrics/Fonts override by msstyles

                    // Get visual styles data from ThemeManager, used to override Metrics/Fonts
                    VisualStyles _vs = new();

                    if (Program.TM is not null)
                    {
                        switch (Program.WindowStyle)
                        {
                            case WindowStyle.W12:
                                _vs = Program.TM.Windows12.VisualStyles;
                                break;
                            case WindowStyle.W11:
                                _vs = Program.TM.Windows11.VisualStyles;
                                break;
                            case WindowStyle.W10:
                                _vs = Program.TM.Windows10.VisualStyles;
                                break;
                            case WindowStyle.W81:
                                _vs = Program.TM.Windows81.VisualStyles;
                                break;
                            case WindowStyle.W7:
                                _vs = Program.TM.Windows7.VisualStyles;
                                break;
                            case WindowStyle.WVista:
                                _vs = Program.TM.WindowsVista.VisualStyles;
                                break;
                            case WindowStyle.WXP:
                                _vs = Program.TM.WindowsXP.VisualStyles;
                                break;
                        }
                    }

                    // Override Metrics/Fonts by visual styles
                    if (_vs.Enabled && _vs.OverrideSizes)
                    {
                        if (File.Exists(_vs.ThemeFile))
                        {
                            try
                            {
                                using (VisualStyle vs = new(_vs.ThemeFile))
                                {
                                    this.CopyFrom(vs.MetricsFonts());
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
                                            Overwrite_Metrics(vs.Metrics);
                                            Overwrite_Fonts(vs.Metrics);
                                        }
                                        catch { } // Couldn't load visual styles File.
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    // Get current DPI and set it to 100% to avoid DPI scaling issues
                    float OldDPI = ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "AppliedDPI", Program.GetWindowsScreenScalingFactor());
                    WriteReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "AppliedDPI", 100);

                    // Convert fonts to LogFont
                    GDI32.LogFont lfCaptionFont = new();
                    CaptionFont.ToLogFont(lfCaptionFont);

                    GDI32.LogFont lfIconFont = new();
                    IconFont.ToLogFont(lfIconFont);

                    GDI32.LogFont lfMenuFont = new();
                    MenuFont.ToLogFont(lfMenuFont);

                    GDI32.LogFont lfMessageFont = new();
                    MessageFont.ToLogFont(lfMessageFont);

                    GDI32.LogFont lfSMCaptionFont = new();
                    SmCaptionFont.ToLogFont(lfSMCaptionFont);

                    GDI32.LogFont lfStatusFont = new();
                    StatusFont.ToLogFont(lfStatusFont);

                    WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "FontSmoothing", !Fonts_SingleBitPP ? 2 : 0);
                    WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "FontSmoothingType", !Fonts_SingleBitPP ? 2 : 1);

                    SystemParametersInfo(treeView, SPI.SPI_SETFONTSMOOTHING, !Fonts_SingleBitPP, default, SPIF.SPIF_UPDATEINIFILE);

                    MetricsFonts MF = Clone();

                    // Apply Metrics/Fonts in a new thread to avoid UI freeze when applying changes
                    await Task.Run(() =>
                    {
                        Program.Log?.Write(LogEventLevel.Information, $"Using User32.SystemParametersInfo to apply Metrics and Fonts settings asynchronously to avoid bugs of crashing WinPaletter and active apps.");
                        Program.Log?.Write(LogEventLevel.Information, $"You may notice that User32.SystemParametersInfo logs items are wrongly places. That is because of the asynchronous nature of this method. It is not a bug, it is a feature.");

                        NONCLIENTMETRICS NCM = new();
                        NCM.cbSize = (uint)Marshal.SizeOf(NCM);
                        ICONMETRICS ICO = new();
                        ICO.cbSize = (uint)Marshal.SizeOf(ICO);

                        SystemParametersInfo(treeView, SPI.SPI_GETICONMETRICS, (int)ICO.cbSize, ref ICO, SPIF.SPIF_NONE);

                        NCM.lfCaptionFont = lfCaptionFont;
                        NCM.lfSMCaptionFont = lfSMCaptionFont;
                        NCM.lfStatusFont = lfStatusFont;
                        NCM.lfMenuFont = lfMenuFont;
                        NCM.lfMessageFont = lfMessageFont;

                        NCM.iBorderWidth = MF.BorderWidth;
                        NCM.iScrollWidth = MF.ScrollWidth;
                        NCM.iScrollHeight = MF.ScrollHeight;
                        NCM.iCaptionWidth = MF.CaptionWidth;
                        NCM.iCaptionHeight = MF.CaptionHeight;
                        NCM.iSMCaptionWidth = MF.SmCaptionWidth;
                        NCM.iSMCaptionHeight = MF.SmCaptionHeight;
                        NCM.iMenuWidth = MF.MenuWidth;
                        NCM.iMenuHeight = MF.MenuHeight;
                        NCM.iPaddedBorderWidth = MF.PaddedBorderWidth;

                        ICO.iHorzSpacing = MF.IconSpacing;
                        ICO.iVertSpacing = MF.IconVerticalSpacing;
                        ICO.lfFont = lfIconFont;

                        // Broadcast changes to all windows
                        SystemParametersInfo(treeView, SPI.SPI_SETNONCLIENTMETRICS, Marshal.SizeOf(NCM), ref NCM, SPIF.SPIF_WRITEANDNOTIFY);
                        SystemParametersInfo(treeView, SPI.SPI_SETICONMETRICS, Marshal.SizeOf(ICO), ref ICO, SPIF.SPIF_WRITEANDNOTIFY);
                    }).ConfigureAwait(false);

                    // Apply Shell Icon Size and Shell Small Icon Size only on Windows XP
                    if (OS.WXP)
                    {
                        WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "Shell Icon Size", ShellIconSize, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "Shell Small Icon Size", ShellSmallIconSize, RegistryValueKind.String);
                    }

                    WriteReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\Bags\1\Desktop", "IconSize", DesktopIconSize, RegistryValueKind.String);

                    // Apply metrics on HKEY_USERS\.DEFAULT (New users and default user) if it is set to overwrite in WinPaletter settings
                    if (Program.Settings.ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                    {
                        Program.Log?.Write(LogEventLevel.Information, $"Applying Metrics and Fonts settings to HKEY_USERS\\.DEFAULT registry key.");

                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "CaptionFont", lfCaptionFont.ToBytes(), RegistryValueKind.Binary);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "IconFont", lfIconFont.ToBytes(), RegistryValueKind.Binary);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "MenuFont", lfMenuFont.ToBytes(), RegistryValueKind.Binary);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "MessageFont", lfMessageFont.ToBytes(), RegistryValueKind.Binary);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "SmCaptionFont", lfSMCaptionFont.ToBytes(), RegistryValueKind.Binary);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "StatusFont", lfStatusFont.ToBytes(), RegistryValueKind.Binary);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "BorderWidth", BorderWidth * -15, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "CaptionHeight", CaptionHeight * -15, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "CaptionWidth", CaptionWidth * -15, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "IconSpacing", IconSpacing * -15, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "IconVerticalSpacing", IconVerticalSpacing * -15, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "MenuHeight", MenuHeight * -15, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "MenuWidth", MenuWidth * -15, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "PaddedBorderWidth", PaddedBorderWidth * -15, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "ScrollHeight", ScrollHeight * -15, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "ScrollWidth", ScrollWidth * -15, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "SmCaptionHeight", SmCaptionHeight * -15, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "SmCaptionWidth", SmCaptionWidth * -15, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "Shell Icon Size", ShellIconSize, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "Shell Small Icon Size", ShellSmallIconSize, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Software\Microsoft\Windows\Shell\Bags\1\Desktop", "IconSize", DesktopIconSize, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "FontSmoothing", !Fonts_SingleBitPP ? 2 : 0);
                        WriteReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "FontSmoothingType", !Fonts_SingleBitPP ? 2 : 1);
                    }

                    // Apply font substitutes

                    Program.Log?.Write(LogEventLevel.Information, $"Applying font substitutes to HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\FontSubstitutes registry key.");

                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "MS Shell Dlg", FontSubstitute_MSShellDlg, RegistryValueKind.String);
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "MS Shell Dlg 2", FontSubstitute_MSShellDlg2, RegistryValueKind.String);

                    if (string.IsNullOrWhiteSpace(FontSubstitute_SegoeUI))
                    {
                        // Restore Segoe UI fonts
                        WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI (TrueType)", "segoeui.ttf", RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Bold (TrueType)", "segoeuib.ttf", RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Bold Italic (TrueType)", "segoeuiz.ttf", RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Black (TrueType)", "seguibl.ttf", RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Black Italic (TrueType)", "seguibli.ttf", RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Italic (TrueType)", "segoeuii.ttf", RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Light (TrueType)", "segoeuil.ttf", RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Light Italic (TrueType)", "seguili.ttf", RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semibold (TrueType)", "seguisb.ttf", RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semibold Italic (TrueType)", "seguisbi.ttf", RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semilight (TrueType)", "segoeuisl.ttf", RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semilight Italic (TrueType)", "seguisli.ttf", RegistryValueKind.String);
                    }
                    else
                    {
                        // Remove Segoe UI fonts to use font substitute correctly
                        WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI (TrueType)", string.Empty, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Bold (TrueType)", string.Empty, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Bold Italic (TrueType)", string.Empty, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Black (TrueType)", string.Empty, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Black Italic (TrueType)", string.Empty, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Italic (TrueType)", string.Empty, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Light (TrueType)", string.Empty, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Light Italic (TrueType)", string.Empty, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semibold (TrueType)", string.Empty, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semibold Italic (TrueType)", string.Empty, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semilight (TrueType)", string.Empty, RegistryValueKind.String);
                        WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semilight Italic (TrueType)", string.Empty, RegistryValueKind.String);
                    }

                    // Apply SegoeUI font substitutes
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "Segoe UI", FontSubstitute_SegoeUI, RegistryValueKind.String);

                    // Restore DPI
                    WriteReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "AppliedDPI", OldDPI);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Saves MetricsFonts toggle state into registry
        /// </summary>
        /// <param name="treeView"></param>
        public void SaveToggleState(TreeView treeView = null)
        {
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Metrics", string.Empty, Enabled);
        }

        /// <summary>
        /// Retrun MetricsFonts structure into a string in format of Microsoft theme File (*.theme)
        /// </summary>
        /// <param name="win32UI">Win32UI structure to be included in the string</param>
        public string ToString(Win32UI win32UI = null)
        {
            StringBuilder s = new();
            s.Clear();
            s.AppendLine($"; {string.Format(Program.Lang.Strings.MSTheme.Copyrights, DateTime.Now.Year)}");
            s.AppendLine($"; {string.Format(Program.Lang.Strings.MSTheme.ProgrammedBy, Application.CompanyName)}");
            s.AppendLine($"; {string.Format(Program.Lang.Strings.MSTheme.CreatedFromAppVer, Program.TM.Info.AppVersion)}");
            s.AppendLine($"; {string.Format(Program.Lang.Strings.MSTheme.CreatedBy, Program.TM.Info.Author)}");
            s.AppendLine($"; {string.Format(Program.Lang.Strings.MSTheme.ThemeName, Program.TM.Info.ThemeName)}");
            s.AppendLine($"; {string.Format(Program.Lang.Strings.MSTheme.ThemeVersion, Program.TM.Info.ThemeVersion)}");
            s.AppendLine(string.Empty);

            if (win32UI is not null)
            {
                s.AppendLine($"[Control Panel\\Colors]");
                s.AppendLine($"ActiveTitle={win32UI.ActiveTitle.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"Background={win32UI.Background.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"Hilight={win32UI.Hilight.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"HilightText={win32UI.HilightText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"TitleText={win32UI.TitleText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"Window={win32UI.Window.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"WindowText={win32UI.WindowText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"Scrollbar={win32UI.Scrollbar.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"InactiveTitle={win32UI.InactiveTitle.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"Menu={win32UI.Menu.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"WindowFrame={win32UI.WindowFrame.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"MenuText={win32UI.MenuText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"ActiveBorder={win32UI.ActiveBorder.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"InactiveBorder={win32UI.InactiveBorder.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"AppWorkspace={win32UI.AppWorkspace.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"ButtonFace={win32UI.ButtonFace.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"ButtonShadow={win32UI.ButtonShadow.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"GrayText={win32UI.GrayText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"ButtonText={win32UI.ButtonText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"InactiveTitleText={win32UI.InactiveTitleText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"ButtonHilight={win32UI.ButtonHilight.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"ButtonDkShadow={win32UI.ButtonDkShadow.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"ButtonLight={win32UI.ButtonLight.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"InfoText={win32UI.InfoText.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"InfoWindow={win32UI.InfoWindow.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"GradientActiveTitle={win32UI.GradientActiveTitle.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"GradientInactiveTitle={win32UI.GradientInactiveTitle.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"ButtonAlternateFace={win32UI.ButtonAlternateFace.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"HotTrackingColor={win32UI.HotTrackingColor.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"MenuHilight={win32UI.MenuHilight.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"MenuBar={win32UI.MenuBar.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine($"Desktop={win32UI.Desktop.ToString(Settings.Structures.NerdStats.Formats.RGB, false, true)}");
                s.AppendLine(string.Empty);
            }

            NONCLIENTMETRICS ncm = new();
            ncm.cbSize = (uint)Marshal.SizeOf(ncm);
            ncm.iCaptionWidth = CaptionWidth;
            ncm.iCaptionHeight = CaptionHeight;
            ncm.iSMCaptionWidth = SmCaptionWidth;
            ncm.iSMCaptionHeight = SmCaptionHeight;
            ncm.iBorderWidth = BorderWidth;
            ncm.iPaddedBorderWidth = PaddedBorderWidth;
            ncm.iMenuWidth = MenuWidth;
            ncm.iMenuHeight = MenuHeight;
            ncm.iScrollWidth = ScrollWidth;
            ncm.iScrollHeight = ScrollHeight;

            GDI32.LogFont lfCaptionFont = new();
            CaptionFont.ToLogFont(lfCaptionFont);
            ncm.lfCaptionFont = lfCaptionFont;

            GDI32.LogFont lfMenuFont = new();
            MenuFont.ToLogFont(lfMenuFont);
            ncm.lfMenuFont = lfMenuFont;

            GDI32.LogFont lfMessageFont = new();
            MessageFont.ToLogFont(lfMessageFont);
            ncm.lfMessageFont = lfMessageFont;

            GDI32.LogFont lfSMCaptionFont = new();
            SmCaptionFont.ToLogFont(lfSMCaptionFont);
            ncm.lfSMCaptionFont = lfSMCaptionFont;

            GDI32.LogFont lfStatusFont = new();
            StatusFont.ToLogFont(lfStatusFont);
            ncm.lfStatusFont = lfStatusFont;

            ICONMETRICS icm = new();
            icm.cbSize = (uint)Marshal.SizeOf(icm);
            icm.iHorzSpacing = IconSpacing;
            icm.iVertSpacing = IconVerticalSpacing;

            GDI32.LogFont lfIconFont = new();
            IconFont.ToLogFont(lfIconFont);
            icm.lfFont = lfIconFont;

            s.AppendLine(string.Format("[Metrics]"));
            s.AppendLine(string.Format($"IconMetrics={string.Join(" ", icm.ToByteArray())}"));
            s.AppendLine(string.Format($"NonClientMetrics={string.Join(" ", ncm.ToByteArray())}"));
            s.AppendLine(string.Empty);

            s.AppendLine(string.Format("[MasterThemeSelector]"));
            s.AppendLine(string.Format("MTSM=DABJDKT"));

            s.AppendLine(string.Empty);
            s.AppendLine($"[Control Panel\\Colors]");
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