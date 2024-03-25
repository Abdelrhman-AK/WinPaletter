using Devcorp.Controls.VisualStyles;
using Microsoft.Win32;
using System;
using System.Drawing;
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
    public struct MetricsFonts : ICloneable
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled = false;

        /// <summary>Window border width</summary>
        public int BorderWidth = 1;

        /// <summary>Titlebar (caption) height</summary>
        public int CaptionHeight = 22;

        /// <summary>Buttons in classic titlebar (caption) width</summary>
        public int CaptionWidth = 22;

        /// <summary>Horizontal spacing between desktop icons</summary>
        public int IconSpacing = 75;

        /// <summary>Vertical spacing between desktop icons</summary>
        public int IconVerticalSpacing = 75;

        /// <summary>Context menu height (if it is a horizontal menu)</summary>
        public int MenuHeight = 19;

        /// <summary>Context menu width (if it is a vertical menu)</summary>
        public int MenuWidth = 19;

        /// <summary>Padding width of a Window border</summary>
        public int PaddedBorderWidth = 4;

        /// <summary>Scroll bar height (if it is a horizontal scroll bar)</summary>
        public int ScrollHeight = 19;

        /// <summary>Scroll bar width (if it is a vertical scroll bar)</summary>
        public int ScrollWidth = 19;

        /// <summary>Titlebar (caption) height of a tool box window</summary>
        public int SmCaptionHeight = 22;

        /// <summary>Width of Buttons in classic titlebar (caption) of a tool box window</summary>
        public int SmCaptionWidth = 22;

        /// <summary>Size of desktop icons <c>(size x size)</c></summary>
        public int DesktopIconSize = 48;

        /// <summary>
        /// Size of shell icons (used in Windows XP)
        /// <br>Default: <b>32</b></br>
        /// </summary>
        public int ShellIconSize = 32;

        /// <summary>
        /// Size of small icons (used in Windows XP)
        /// <br>Default: <b>16</b></br>
        /// </summary>
        public int ShellSmallIconSize = 16;

        /// <summary>Make fonts pixelated like old versions of Windows (not ClearType)</summary>
        public bool Fonts_SingleBitPP = false;

        /// <summary>Titlebar (caption) font</summary>
        public Font CaptionFont = new("Segoe UI", 9f, FontStyle.Regular);

        /// <summary>Icons font</summary>
        public Font IconFont = new("Segoe UI", 9f, FontStyle.Regular);

        /// <summary>contextMenu font</summary>
        public Font MenuFont = new("Segoe UI", 9f, FontStyle.Regular);

        /// <summary>Message box font</summary>
        public Font MessageFont = new("Segoe UI", 9f, FontStyle.Regular);

        /// <summary>Titlebar (caption) font of a tool box window</summary>
        public Font SmCaptionFont = new("Segoe UI", 9f, FontStyle.Regular);

        /// <summary>Status bar (in the lower part of a window) font</summary>
        public Font StatusFont = new("Segoe UI", 9f, FontStyle.Regular);

        /// <summary>
        /// Font name that should substitutes MSShellDlg
        /// <br>Default value: <b>Microsoft Sans Serif</b></br>
        /// </summary>
        public string FontSubstitute_MSShellDlg = "Microsoft Sans Serif";

        /// <summary>
        /// Font name that should substitutes MSShellDlg2
        /// <br>Default value: <b>Tahoma</b></br>
        /// </summary>
        public string FontSubstitute_MSShellDlg2 = "Tahoma";

        /// <summary>
        /// Font name that should substitutes Segoe UI
        /// <br><b>Has no default value, it is empty</b></br>
        /// </summary>
        public string FontSubstitute_SegoeUI = string.Empty;

        /// <summary>
        /// Create a new MetricsFonts structure with default values
        /// </summary>
        public MetricsFonts() { }

        /// <summary>Operator to check if two MetricsFonts structures are equal</summary>
        public static bool operator ==(MetricsFonts First, MetricsFonts Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two MetricsFonts structures are not equal</summary>
        public static bool operator !=(MetricsFonts First, MetricsFonts Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones MetricsFonts structure</summary>
        public readonly object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Overwrite current metrics values by values inside a visual styles file opened by Devcorp advanced UxTheme wrapper
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
        /// Overwrite current fonts values by values inside a visual styles file opened by Devcorp advanced UxTheme wrapper
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
            Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Metrics", string.Empty, @default.Enabled));
            BorderWidth = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "BorderWidth", @default.BorderWidth * -15)) / -15;
            CaptionHeight = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionHeight", @default.CaptionHeight * -15)) / -15;
            CaptionWidth = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionWidth", @default.CaptionWidth * -15)) / -15;
            IconSpacing = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconSpacing", @default.IconSpacing * -15)) / -15;
            IconVerticalSpacing = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconVerticalSpacing", @default.IconVerticalSpacing * -15)) / -15;
            MenuHeight = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuHeight", @default.MenuHeight * -15)) / -15;
            MenuWidth = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuWidth", @default.MenuWidth * -15)) / -15;
            PaddedBorderWidth = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "PaddedBorderWidth", @default.PaddedBorderWidth * -15)) / -15;
            ScrollHeight = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollHeight", @default.ScrollHeight * -15)) / -15;
            ScrollWidth = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollWidth", @default.ScrollWidth * -15)) / -15;
            SmCaptionHeight = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionHeight", @default.SmCaptionHeight * -15)) / -15;
            SmCaptionWidth = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionWidth", @default.SmCaptionWidth * -15)) / -15;

            if (OS.WXP)
            {
                try { ShellIconSize = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "Shell Icon Size", @default.ShellIconSize)); }
                catch { ShellIconSize = @default.ShellIconSize; }

                try { ShellSmallIconSize = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "Shell Small Icon Size", @default.ShellSmallIconSize)); }
                catch { ShellSmallIconSize = @default.ShellSmallIconSize; }
            }

            DesktopIconSize = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\Bags\1\Desktop", "IconSize", @default.DesktopIconSize));
            FontSubstitute_MSShellDlg = GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "MS Shell Dlg", @default.FontSubstitute_MSShellDlg).ToString();
            FontSubstitute_MSShellDlg2 = GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "MS Shell Dlg 2", @default.FontSubstitute_MSShellDlg2).ToString();
            FontSubstitute_SegoeUI = GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "Segoe UI", @default.FontSubstitute_SegoeUI).ToString();

            NONCLIENTMETRICS NCM = new();
            NCM.cbSize = (uint)Marshal.SizeOf(NCM);

            ICONMETRICS ICO = new();
            ICO.cbSize = (uint)Marshal.SizeOf(ICO);

            SystemParametersInfo(null, SPI.SPI_GETICONMETRICS, (int)ICO.cbSize, ref ICO, SPIF.SPIF_NONE);
            SystemParametersInfo(null, SPI.SPI_GETNONCLIENTMETRICS, (int)NCM.cbSize, ref NCM, SPIF.SPIF_NONE);

            CaptionFont = Font.FromLogFont(NCM.lfCaptionFont);
            MenuFont = Font.FromLogFont(NCM.lfMenuFont);
            MessageFont = Font.FromLogFont(NCM.lfMessageFont);
            SmCaptionFont = Font.FromLogFont(NCM.lfSMCaptionFont);
            StatusFont = Font.FromLogFont(NCM.lfStatusFont);
            IconFont = Font.FromLogFont(ICO.lfFont);

            bool temp = false;
            SystemParametersInfo(SPI.SPI_GETFONTSMOOTHING, default, ref temp, SPIF.SPIF_NONE);
            Fonts_SingleBitPP = !temp || Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "FontSmoothingType", OS.WXP ? 1 : 2)) != 2;
        }

        /// <summary>
        /// Saves MetricsFonts data into registry
        /// </summary>
        /// <param name="treeView">treeView used as theme log</param>
        public async void Apply(TreeView treeView = null)
        {
            SaveToggleState(treeView);

            if (Enabled)
            {
                #region Metrics/Fonts override by msstyles

                VisualStyles _vs = new();
                switch (Program.WindowStyle)
                {
                    case WindowStyle.W12:
                        _vs = Program.TM.VisualStyles_12;
                        break;
                    case WindowStyle.W11:
                        _vs = Program.TM.VisualStyles_11;
                        break;
                    case WindowStyle.W10:
                        _vs = Program.TM.VisualStyles_10;
                        break;
                    case WindowStyle.W81:
                        _vs = Program.TM.VisualStyles_81;
                        break;
                    case WindowStyle.W7:
                        _vs = Program.TM.VisualStyles_7;
                        break;
                    case WindowStyle.WVista:
                        _vs = Program.TM.VisualStyles_Vista;
                        break;
                    case WindowStyle.WXP:
                        _vs = Program.TM.VisualStyles_XP;
                        break;
                }

                if (_vs.Enabled && _vs.OverrideSizes)
                {
                    if (System.IO.File.Exists(_vs.ThemeFile))
                    {
                        try
                        {
                            using (libmsstyle.VisualStyle vs = new(_vs.ThemeFile))
                            {
                                this = vs.MetricsFonts();
                                Enabled = true;
                            }
                        }
                        catch
                        {
                            string theme = _vs.ThemeFile;
                            if (System.IO.Path.GetExtension(theme).ToLower() == ".msstyles")
                            {
                                System.IO.File.WriteAllText($@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme", $"[VisualStyles]{"\r\n"}Path={theme}{"\r\n"}ColorStyle=NormalColor{"\r\n"}Size=NormalSize");
                                theme = $@"{SysPaths.appData}\VisualStyles\Luna\win32uischeme.theme";
                            }

                            if (System.IO.File.Exists(theme))
                            {
                                using (VisualStyleFile vs = new(theme))
                                {
                                    try
                                    {
                                        Overwrite_Metrics(vs.Metrics);
                                        Overwrite_Fonts(vs.Metrics);
                                    }
                                    catch { } // Couldn't load visual styles file.
                                }
                            }
                        }
                    }
                }

                #endregion

                int OldDPI = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "AppliedDPI", Program.GetWindowsScreenScalingFactor()));
                EditReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "AppliedDPI", 100);

                NativeMethods.GDI32.LogFont lfCaptionFont = new();
                CaptionFont.ToLogFont(lfCaptionFont);

                NativeMethods.GDI32.LogFont lfIconFont = new();
                IconFont.ToLogFont(lfIconFont);

                NativeMethods.GDI32.LogFont lfMenuFont = new();
                MenuFont.ToLogFont(lfMenuFont);

                NativeMethods.GDI32.LogFont lfMessageFont = new();
                MessageFont.ToLogFont(lfMessageFont);

                NativeMethods.GDI32.LogFont lfSMCaptionFont = new();
                SmCaptionFont.ToLogFont(lfSMCaptionFont);

                NativeMethods.GDI32.LogFont lfStatusFont = new();
                StatusFont.ToLogFont(lfStatusFont);

                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "FontSmoothing", !Fonts_SingleBitPP ? 2 : 0);
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "FontSmoothingType", !Fonts_SingleBitPP ? 2 : 1);

                SystemParametersInfo(treeView, SPI.SPI_SETFONTSMOOTHING, !Fonts_SingleBitPP, default, SPIF.SPIF_UPDATEINIFILE);

                MetricsFonts MF = (MetricsFonts)Clone();

                await Task.Run(() =>
                {
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

                    SystemParametersInfo(treeView, SPI.SPI_SETNONCLIENTMETRICS, Marshal.SizeOf(NCM), ref NCM, SPIF.SPIF_WRITEANDNOTIFY);
                    SystemParametersInfo(treeView, SPI.SPI_SETICONMETRICS, Marshal.SizeOf(ICO), ref ICO, SPIF.SPIF_WRITEANDNOTIFY);
                });

                if (OS.WXP)
                {
                    EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "Shell Icon Size", ShellIconSize, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "Shell Small Icon Size", ShellSmallIconSize, RegistryValueKind.String);
                }

                EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\Bags\1\Desktop", "IconSize", DesktopIconSize, RegistryValueKind.String);

                if (Program.Settings.ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs == Settings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                {
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "CaptionFont", lfCaptionFont.ToBytes(), RegistryValueKind.Binary);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "IconFont", lfIconFont.ToBytes(), RegistryValueKind.Binary);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "MenuFont", lfMenuFont.ToBytes(), RegistryValueKind.Binary);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "MessageFont", lfMessageFont.ToBytes(), RegistryValueKind.Binary);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "SmCaptionFont", lfSMCaptionFont.ToBytes(), RegistryValueKind.Binary);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "StatusFont", lfStatusFont.ToBytes(), RegistryValueKind.Binary);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "BorderWidth", BorderWidth * -15, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "CaptionHeight", CaptionHeight * -15, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "CaptionWidth", CaptionWidth * -15, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "IconSpacing", IconSpacing * -15, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "IconVerticalSpacing", IconVerticalSpacing * -15, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "MenuHeight", MenuHeight * -15, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "MenuWidth", MenuWidth * -15, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "PaddedBorderWidth", PaddedBorderWidth * -15, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "ScrollHeight", ScrollHeight * -15, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "ScrollWidth", ScrollWidth * -15, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "SmCaptionHeight", SmCaptionHeight * -15, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "SmCaptionWidth", SmCaptionWidth * -15, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "Shell Icon Size", ShellIconSize, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "Shell Small Icon Size", ShellSmallIconSize, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Software\Microsoft\Windows\Shell\Bags\1\Desktop", "IconSize", DesktopIconSize, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "FontSmoothing", !Fonts_SingleBitPP ? 2 : 0);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "FontSmoothingType", !Fonts_SingleBitPP ? 2 : 1);
                }

                EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "MS Shell Dlg", FontSubstitute_MSShellDlg, RegistryValueKind.String);
                EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "MS Shell Dlg 2", FontSubstitute_MSShellDlg2, RegistryValueKind.String);

                if (string.IsNullOrWhiteSpace(FontSubstitute_SegoeUI))
                {
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI (TrueType)", "segoeui.ttf", RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Bold (TrueType)", "segoeuib.ttf", RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Bold Italic (TrueType)", "segoeuiz.ttf", RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Black (TrueType)", "seguibl.ttf", RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Black Italic (TrueType)", "seguibli.ttf", RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Italic (TrueType)", "segoeuii.ttf", RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Light (TrueType)", "segoeuil.ttf", RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Light Italic (TrueType)", "seguili.ttf", RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semibold (TrueType)", "seguisb.ttf", RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semibold Italic (TrueType)", "seguisbi.ttf", RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semilight (TrueType)", "segoeuisl.ttf", RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semilight Italic (TrueType)", "seguisli.ttf", RegistryValueKind.String);
                }
                else
                {
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI (TrueType)", string.Empty, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Bold (TrueType)", string.Empty, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Bold Italic (TrueType)", string.Empty, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Black (TrueType)", string.Empty, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Black Italic (TrueType)", string.Empty, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Italic (TrueType)", string.Empty, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Light (TrueType)", string.Empty, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Light Italic (TrueType)", string.Empty, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semibold (TrueType)", string.Empty, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semibold Italic (TrueType)", string.Empty, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semilight (TrueType)", string.Empty, RegistryValueKind.String);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semilight Italic (TrueType)", string.Empty, RegistryValueKind.String);
                }
                EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "Segoe UI", FontSubstitute_SegoeUI, RegistryValueKind.String);

                EditReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "AppliedDPI", OldDPI);
            }

        }

        /// <summary>
        /// Saves MetricsFonts toggle state into registry
        /// </summary>
        /// <param name="treeView"></param>
        public void SaveToggleState(TreeView treeView = null)
        {
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Metrics", string.Empty, Enabled);
        }

        /// <summary>Checks if two MetricsFonts structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Retrun MetricsFonts structure into a string in format of Microsoft theme file (*.theme)
        /// </summary>
        /// <param name="win32UI">Win32UI structure to be included in the string</param>
        public readonly string ToString(Theme.Structures.Win32UI? win32UI = null)
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

            if (win32UI is not null && win32UI.HasValue)
            {
                s.AppendLine($"[Control Panel\\Colors]");
                s.AppendLine($"ActiveTitle={win32UI.Value.ActiveTitle.ToWin32Reg()}");
                s.AppendLine($"Background={win32UI.Value.Background.ToWin32Reg()}");
                s.AppendLine($"Hilight={win32UI.Value.Hilight.ToWin32Reg()}");
                s.AppendLine($"HilightText={win32UI.Value.HilightText.ToWin32Reg()}");
                s.AppendLine($"TitleText={win32UI.Value.TitleText.ToWin32Reg()}");
                s.AppendLine($"Window={win32UI.Value.Window.ToWin32Reg()}");
                s.AppendLine($"WindowText={win32UI.Value.WindowText.ToWin32Reg()}");
                s.AppendLine($"Scrollbar={win32UI.Value.Scrollbar.ToWin32Reg()}");
                s.AppendLine($"InactiveTitle={win32UI.Value.InactiveTitle.ToWin32Reg()}");
                s.AppendLine($"Menu={win32UI.Value.Menu.ToWin32Reg()}");
                s.AppendLine($"WindowFrame={win32UI.Value.WindowFrame.ToWin32Reg()}");
                s.AppendLine($"MenuText={win32UI.Value.MenuText.ToWin32Reg()}");
                s.AppendLine($"ActiveBorder={win32UI.Value.ActiveBorder.ToWin32Reg()}");
                s.AppendLine($"InactiveBorder={win32UI.Value.InactiveBorder.ToWin32Reg()}");
                s.AppendLine($"AppWorkspace={win32UI.Value.AppWorkspace.ToWin32Reg()}");
                s.AppendLine($"ButtonFace={win32UI.Value.ButtonFace.ToWin32Reg()}");
                s.AppendLine($"ButtonShadow={win32UI.Value.ButtonShadow.ToWin32Reg()}");
                s.AppendLine($"GrayText={win32UI.Value.GrayText.ToWin32Reg()}");
                s.AppendLine($"ButtonText={win32UI.Value.ButtonText.ToWin32Reg()}");
                s.AppendLine($"InactiveTitleText={win32UI.Value.InactiveTitleText.ToWin32Reg()}");
                s.AppendLine($"ButtonHilight={win32UI.Value.ButtonHilight.ToWin32Reg()}");
                s.AppendLine($"ButtonDkShadow={win32UI.Value.ButtonDkShadow.ToWin32Reg()}");
                s.AppendLine($"ButtonLight={win32UI.Value.ButtonLight.ToWin32Reg()}");
                s.AppendLine($"InfoText={win32UI.Value.InfoText.ToWin32Reg()}");
                s.AppendLine($"InfoWindow={win32UI.Value.InfoWindow.ToWin32Reg()}");
                s.AppendLine($"GradientActiveTitle={win32UI.Value.GradientActiveTitle.ToWin32Reg()}");
                s.AppendLine($"GradientInactiveTitle={win32UI.Value.GradientInactiveTitle.ToWin32Reg()}");
                s.AppendLine($"ButtonAlternateFace={win32UI.Value.ButtonAlternateFace.ToWin32Reg()}");
                s.AppendLine($"HotTrackingColor={win32UI.Value.HotTrackingColor.ToWin32Reg()}");
                s.AppendLine($"MenuHilight={win32UI.Value.MenuHilight.ToWin32Reg()}");
                s.AppendLine($"MenuBar={win32UI.Value.MenuBar.ToWin32Reg()}");
                s.AppendLine($"Desktop={win32UI.Value.Desktop.ToWin32Reg()}");
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

        /// <summary>Get hash code of MetricsFonts structure</summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}