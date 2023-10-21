using Devcorp.Controls.VisualStyles;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static WinPaletter.Metrics;
using static WinPaletter.NativeMethods.User32;

namespace WinPaletter.Theme.Structures
{
    public struct MetricsFonts : ICloneable
    {
        public bool Enabled;
        public int BorderWidth;
        public int CaptionHeight;
        public int CaptionWidth;
        public int IconSpacing;
        public int IconVerticalSpacing;
        public int MenuHeight;
        public int MenuWidth;
        public int PaddedBorderWidth;
        public int ScrollHeight;
        public int ScrollWidth;
        public int SmCaptionHeight;
        public int SmCaptionWidth;
        public int DesktopIconSize;
        public int ShellIconSize;
        public int ShellSmallIconSize;
        public bool Fonts_SingleBitPP;

        public Font CaptionFont;
        public Font IconFont;
        public Font MenuFont;
        public Font MessageFont;
        public Font SmCaptionFont;
        public Font StatusFont;
        public string FontSubstitute_MSShellDlg;
        public string FontSubstitute_MSShellDlg2;
        public string FontSubstitute_SegoeUI;

        public static bool operator ==(MetricsFonts First, MetricsFonts Second)
        {
            return First.Equals(Second);
        }

        public static bool operator !=(MetricsFonts First, MetricsFonts Second)
        {
            return !First.Equals(Second);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public void Overwrite_Metrics(VisualStyleMetrics vs)
        {
            CaptionHeight = vs.Sizes.CaptionBarHeight;
            ScrollHeight = vs.Sizes.ScrollbarHeight;
            ScrollWidth = vs.Sizes.ScrollbarWidth;
            SmCaptionHeight = vs.Sizes.SMCaptionBarHeight;
            SmCaptionWidth = vs.Sizes.SMCaptionBarWidth;
        }

        public void Overwrite_Fonts(VisualStyleMetrics vs)
        {
            CaptionFont = vs.Fonts.CaptionFont;
            IconFont = vs.Fonts.IconTitleFont;
            MenuFont = vs.Fonts.MenuFont;
            SmCaptionFont = vs.Fonts.SmallCaptionFont;
            MessageFont = vs.Fonts.MsgBoxFont;
            StatusFont = vs.Fonts.StatusFont;
        }

        public void Load(MetricsFonts _DefMetricsFonts)
        {
            Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Metrics", "", _DefMetricsFonts.Enabled));
            BorderWidth = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "BorderWidth", _DefMetricsFonts.BorderWidth * -15)) / -15;
            CaptionHeight = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionHeight", _DefMetricsFonts.CaptionHeight * -15)) / -15;
            CaptionWidth = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionWidth", _DefMetricsFonts.CaptionWidth * -15)) / -15;
            IconSpacing = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconSpacing", _DefMetricsFonts.IconSpacing * -15)) / -15;
            IconVerticalSpacing = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconVerticalSpacing", _DefMetricsFonts.IconVerticalSpacing * -15)) / -15;
            MenuHeight = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuHeight", _DefMetricsFonts.MenuHeight * -15)) / -15;
            MenuWidth = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuWidth", _DefMetricsFonts.MenuWidth * -15)) / -15;
            PaddedBorderWidth = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "PaddedBorderWidth", _DefMetricsFonts.PaddedBorderWidth * -15)) / -15;
            ScrollHeight = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollHeight", _DefMetricsFonts.ScrollHeight * -15)) / -15;
            ScrollWidth = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollWidth", _DefMetricsFonts.ScrollWidth * -15)) / -15;
            SmCaptionHeight = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionHeight", _DefMetricsFonts.SmCaptionHeight * -15)) / -15;
            SmCaptionWidth = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionWidth", _DefMetricsFonts.SmCaptionWidth * -15)) / -15;

            if (OS.WXP)
            {
                try
                {
                    ShellIconSize = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "Shell Icon Size", _DefMetricsFonts.ShellIconSize));
                }
                catch
                {
                    ShellIconSize = _DefMetricsFonts.ShellIconSize;
                }

                try
                {
                    ShellSmallIconSize = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "Shell Small Icon Size", _DefMetricsFonts.ShellSmallIconSize));
                }
                catch
                {
                    ShellSmallIconSize = _DefMetricsFonts.ShellSmallIconSize;
                }
            }

            DesktopIconSize = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\Bags\1\Desktop", "IconSize", _DefMetricsFonts.DesktopIconSize));
            CaptionFont = ((byte[])GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionFont", _DefMetricsFonts.CaptionFont.ToByte())).ToFont();
            IconFont = ((byte[])GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconFont", _DefMetricsFonts.IconFont.ToByte())).ToFont();
            MenuFont = ((byte[])GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuFont", _DefMetricsFonts.MenuFont.ToByte())).ToFont();
            MessageFont = ((byte[])GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MessageFont", _DefMetricsFonts.MessageFont.ToByte())).ToFont();
            SmCaptionFont = ((byte[])GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionFont", _DefMetricsFonts.SmCaptionFont.ToByte())).ToFont();
            StatusFont = ((byte[])GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "StatusFont", _DefMetricsFonts.StatusFont.ToByte())).ToFont();
            FontSubstitute_MSShellDlg = GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "MS Shell Dlg", _DefMetricsFonts.FontSubstitute_MSShellDlg).ToString();
            FontSubstitute_MSShellDlg2 = GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "MS Shell Dlg 2", _DefMetricsFonts.FontSubstitute_MSShellDlg2).ToString();
            FontSubstitute_SegoeUI = GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "Segoe UI", _DefMetricsFonts.FontSubstitute_SegoeUI).ToString();

            if (GetWindowsScreenScalingFactor() > 100d)
            {
                CaptionFont = AdjustFont(CaptionFont, false);
                IconFont = AdjustFont(IconFont, false);
                MenuFont = AdjustFont(MenuFont, false);
                MessageFont = AdjustFont(MessageFont, false);
                SmCaptionFont = AdjustFont(SmCaptionFont, false);
                StatusFont = AdjustFont(StatusFont, false);
            }


            bool temp = false;
            Fixer.SystemParametersInfo((int)SPI.Fonts.GETFONTSMOOTHING, default, ref temp, (int)SPIF.None);
            Fonts_SingleBitPP = !temp || Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "FontSmoothingType", OS.WXP ? 1 : 2)) != 2;
        }

        private Font AdjustFont(Font Font, bool Reverse)
        {
            int DPI = (int)Math.Round(GetWindowsScreenScalingFactor());
            if (DPI > 0)
            {
                float font_size = (float)((double)Font.Size * (!Reverse ? 100d / DPI : DPI / 100d));
                if (font_size > 0f)
                {
                    return new Font(Font.Name, font_size, Font.Style, GraphicsUnit.Pixel);
                }
                else
                {
                    return Font;
                }
            }
            else
            {
                return Font;
            }
        }

        public void Apply(TreeView TreeView = null)
        {
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Metrics", "", Enabled);

            if (Enabled)
            {
                // If Core.GetWindowsScreenScalingFactor > 100 Then
                // CaptionFont = AdjustFont(CaptionFont, True)
                // IconFont = AdjustFont(IconFont, True)
                // MenuFont = AdjustFont(MenuFont, True)
                // MessageFont = AdjustFont(MessageFont, True)
                // SmCaptionFont = AdjustFont(SmCaptionFont, True)
                // StatusFont = AdjustFont(StatusFont, True)
                // End If

                int OldDPI = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "AppliedDPI", GetWindowsScreenScalingFactor()));
                EditReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "AppliedDPI", 100);

                var lfCaptionFont = new LogFont();
                CaptionFont.ToLogFont(lfCaptionFont);
                var lfIconFont = new LogFont();
                IconFont.ToLogFont(lfIconFont);
                var lfMenuFont = new LogFont();
                MenuFont.ToLogFont(lfMenuFont);
                var lfMessageFont = new LogFont();
                MessageFont.ToLogFont(lfMessageFont);
                var lfSMCaptionFont = new LogFont();
                SmCaptionFont.ToLogFont(lfSMCaptionFont);
                var lfStatusFont = new LogFont();
                StatusFont.ToLogFont(lfStatusFont);

                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "FontSmoothing", !Fonts_SingleBitPP ? 2 : 0);
                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "FontSmoothingType", !Fonts_SingleBitPP ? 2 : 1);

                if (TreeView is not null)
                    Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Fonts.SETFONTSMOOTHING.ToString(), !Fonts_SingleBitPP, "null", SPIF.UpdateINIFile.ToString()), "dll");
                SystemParametersInfo((int)SPI.Fonts.SETFONTSMOOTHING, !Fonts_SingleBitPP, default, (int)SPIF.UpdateINIFile);

                if (!Program.Settings.ThemeApplyingBehavior.DelayMetrics)
                {
                    NONCLIENTMETRICS NCM = new NONCLIENTMETRICS();
                    NCM.cbSize = Marshal.SizeOf(NCM);
                    ICONMETRICS ICO = new ICONMETRICS();
                    ICO.cbSize = (uint)Marshal.SizeOf(ICO);

                    SystemParametersInfo((int)SPI.Icons.GETICONMETRICS, (int)ICO.cbSize, ref ICO, SPIF.None);

                    {
                        NCM.lfCaptionFont = lfCaptionFont;
                        NCM.lfSMCaptionFont = lfSMCaptionFont;
                        NCM.lfStatusFont = lfStatusFont;
                        NCM.lfMenuFont = lfMenuFont;
                        NCM.lfMessageFont = lfMessageFont;

                        NCM.iBorderWidth = BorderWidth;
                        NCM.iScrollWidth = ScrollWidth;
                        NCM.iScrollHeight = ScrollHeight;
                        NCM.iCaptionWidth = CaptionWidth;
                        NCM.iCaptionHeight = CaptionHeight;
                        NCM.iSMCaptionWidth = SmCaptionWidth;
                        NCM.iSMCaptionHeight = SmCaptionHeight;
                        NCM.iMenuWidth = MenuWidth;
                        NCM.iMenuHeight = MenuHeight;
                        NCM.iPaddedBorderWidth = PaddedBorderWidth;
                    }

                    {
                        ICO.iHorzSpacing = IconSpacing;
                        ICO.iVertSpacing = IconVerticalSpacing;
                        ICO.lfFont = lfIconFont;
                    }

                    if (TreeView is not null)
                        Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Metrics.SETNONCLIENTMETRICS.ToString(), Marshal.SizeOf(NCM), NCM.ToString(), SPIF.UpdateINIFile.ToString()), "dll");
                    SystemParametersInfo((int)SPI.Metrics.SETNONCLIENTMETRICS, Marshal.SizeOf(NCM), ref NCM, SPIF.UpdateINIFile);

                    if (TreeView is not null)
                        Manager.AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Icons.SETICONMETRICS.ToString(), Marshal.SizeOf(ICO), ICO.ToString(), SPIF.UpdateINIFile.ToString()), "dll");
                    SystemParametersInfo((int)SPI.Icons.SETICONMETRICS, Marshal.SizeOf(ICO), ref ICO, SPIF.UpdateINIFile);
                }

                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionFont", lfCaptionFont.ToByte(), RegistryValueKind.Binary);
                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconFont", lfIconFont.ToByte(), RegistryValueKind.Binary);
                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuFont", lfMenuFont.ToByte(), RegistryValueKind.Binary);
                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MessageFont", lfMessageFont.ToByte(), RegistryValueKind.Binary);
                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionFont", lfSMCaptionFont.ToByte(), RegistryValueKind.Binary);
                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "StatusFont", lfStatusFont.ToByte(), RegistryValueKind.Binary);
                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "BorderWidth", BorderWidth * -15, RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionHeight", CaptionHeight * -15, RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionWidth", CaptionWidth * -15, RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconSpacing", IconSpacing * -15, RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconVerticalSpacing", IconVerticalSpacing * -15, RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuHeight", MenuHeight * -15, RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuWidth", MenuWidth * -15, RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "PaddedBorderWidth", PaddedBorderWidth * -15, RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollHeight", ScrollHeight * -15, RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollWidth", ScrollWidth * -15, RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionHeight", SmCaptionHeight * -15, RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionWidth", SmCaptionWidth * -15, RegistryValueKind.String);

                if (OS.WXP)
                {
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "Shell Icon Size", ShellIconSize, RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "Shell Small Icon Size", ShellSmallIconSize, RegistryValueKind.String);
                }

                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\Bags\1\Desktop", "IconSize", DesktopIconSize, RegistryValueKind.String);

                if (Program.Settings.ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                {
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "CaptionFont", lfCaptionFont.ToByte(), RegistryValueKind.Binary);
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "IconFont", lfIconFont.ToByte(), RegistryValueKind.Binary);
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "MenuFont", lfMenuFont.ToByte(), RegistryValueKind.Binary);
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "MessageFont", lfMessageFont.ToByte(), RegistryValueKind.Binary);
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "SmCaptionFont", lfSMCaptionFont.ToByte(), RegistryValueKind.Binary);
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "StatusFont", lfStatusFont.ToByte(), RegistryValueKind.Binary);
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "BorderWidth", BorderWidth * -15, RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "CaptionHeight", CaptionHeight * -15, RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "CaptionWidth", CaptionWidth * -15, RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "IconSpacing", IconSpacing * -15, RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "IconVerticalSpacing", IconVerticalSpacing * -15, RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "MenuHeight", MenuHeight * -15, RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "MenuWidth", MenuWidth * -15, RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "PaddedBorderWidth", PaddedBorderWidth * -15, RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "ScrollHeight", ScrollHeight * -15, RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "ScrollWidth", ScrollWidth * -15, RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "SmCaptionHeight", SmCaptionHeight * -15, RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "SmCaptionWidth", SmCaptionWidth * -15, RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "Shell Icon Size", ShellIconSize, RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "Shell Small Icon Size", ShellSmallIconSize, RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Software\Microsoft\Windows\Shell\Bags\1\Desktop", "IconSize", DesktopIconSize, RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "FontSmoothing", !Fonts_SingleBitPP ? 2 : 0);
                    EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "FontSmoothingType", !Fonts_SingleBitPP ? 2 : 1);
                }

                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "MS Shell Dlg", FontSubstitute_MSShellDlg, RegistryValueKind.String);
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "MS Shell Dlg 2", FontSubstitute_MSShellDlg2, RegistryValueKind.String);

                if (string.IsNullOrWhiteSpace(FontSubstitute_SegoeUI))
                {
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI (TrueType)", "segoeui.ttf", RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Bold (TrueType)", "segoeuib.ttf", RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Bold Italic (TrueType)", "segoeuiz.ttf", RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Black (TrueType)", "seguibl.ttf", RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Black Italic (TrueType)", "seguibli.ttf", RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Italic (TrueType)", "segoeuii.ttf", RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Light (TrueType)", "segoeuil.ttf", RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Light Italic (TrueType)", "seguili.ttf", RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semibold (TrueType)", "seguisb.ttf", RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semibold Italic (TrueType)", "seguisbi.ttf", RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semilight (TrueType)", "segoeuisl.ttf", RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semilight Italic (TrueType)", "seguisli.ttf", RegistryValueKind.String);
                }
                else
                {
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI (TrueType)", "", RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Bold (TrueType)", "", RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Bold Italic (TrueType)", "", RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Black (TrueType)", "", RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Black Italic (TrueType)", "", RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Italic (TrueType)", "", RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Light (TrueType)", "", RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Light Italic (TrueType)", "", RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semibold (TrueType)", "", RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semibold Italic (TrueType)", "", RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semilight (TrueType)", "", RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semilight Italic (TrueType)", "", RegistryValueKind.String);
                }
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "Segoe UI", FontSubstitute_SegoeUI, RegistryValueKind.String);

                EditReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "AppliedDPI", OldDPI);
            }

        }
    }
}
