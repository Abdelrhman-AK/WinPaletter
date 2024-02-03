using Devcorp.Controls.VisualStyles;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WinPaletter.NativeMethods.User32;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing Windows metrics and fonts
    /// </summary>
    public struct MetricsFonts : ICloneable
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled;

        /// <summary>Window border width</summary>
        public int BorderWidth;

        /// <summary>Titlebar (caption) height</summary>
        public int CaptionHeight;

        /// <summary>Buttons in classic titlebar (caption) width</summary>
        public int CaptionWidth;

        /// <summary>Horizontal spacing between desktop icons</summary>
        public int IconSpacing;

        /// <summary>Vertical spacing between desktop icons</summary>
        public int IconVerticalSpacing;

        /// <summary>Context menu height (if it is a horizontal menu)</summary>
        public int MenuHeight;

        /// <summary>Context menu width (if it is a vertical menu)</summary>
        public int MenuWidth;

        /// <summary>Padding width of a Window border</summary>
        public int PaddedBorderWidth;

        /// <summary>Scroll bar height (if it is a horizontal scroll bar)</summary>
        public int ScrollHeight;

        /// <summary>Scroll bar width (if it is a vertical scroll bar)</summary>
        public int ScrollWidth;

        /// <summary>Titlebar (caption) height of a tool box window</summary>
        public int SmCaptionHeight;

        /// <summary>Width of Buttons in classic titlebar (caption) of a tool box window</summary>
        public int SmCaptionWidth;

        /// <summary>Size of desktop icons <c>(size x size)</c></summary>
        public int DesktopIconSize;

        /// <summary>
        /// Size of shell icons (used in Windows XP)
        /// <br>Default: <b>32</b></br>
        /// </summary>
        public int ShellIconSize;

        /// <summary>
        /// Size of small icons (used in Windows XP)
        /// <br>Default: <b>16</b></br>
        /// </summary>
        public int ShellSmallIconSize;

        /// <summary>Make fonts pixelated like old versions of Windows (not ClearType)</summary>
        public bool Fonts_SingleBitPP;

        /// <summary>Titlebar (caption) font</summary>
        public Font CaptionFont;

        /// <summary>Icons font</summary>
        public Font IconFont;

        /// <summary>contextMenu font</summary>
        public Font MenuFont;

        /// <summary>Message box font</summary>
        public Font MessageFont;

        /// <summary>Titlebar (caption) font of a tool box window</summary>
        public Font SmCaptionFont;

        /// <summary>Status bar (in the lower part of a window) font</summary>
        public Font StatusFont;

        /// <summary>
        /// Font name that should substitutes MSShellDlg
        /// <br>Default value: <b>Microsoft Sans Serif</b></br>
        /// </summary>
        public string FontSubstitute_MSShellDlg;

        /// <summary>
        /// Font name that should substitutes MSShellDlg2
        /// <br>Default value: <b>Tahoma</b></br>
        /// </summary>
        public string FontSubstitute_MSShellDlg2;

        /// <summary>
        /// Font name that should substitutes Segoe UI
        /// <br><b>Has no default value, it is empty</b></br>
        /// </summary>
        public string FontSubstitute_SegoeUI;

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
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Metrics", string.Empty, Enabled);

            if (Enabled)
            {
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
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "CaptionFont", lfCaptionFont.ToByte(), RegistryValueKind.Binary);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "IconFont", lfIconFont.ToByte(), RegistryValueKind.Binary);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "MenuFont", lfMenuFont.ToByte(), RegistryValueKind.Binary);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "MessageFont", lfMessageFont.ToByte(), RegistryValueKind.Binary);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "SmCaptionFont", lfSMCaptionFont.ToByte(), RegistryValueKind.Binary);
                    EditReg(treeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "StatusFont", lfStatusFont.ToByte(), RegistryValueKind.Binary);
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

        /// <summary>Checks if two MetricsFonts structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>Get hash code of MetricsFonts structure</summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}