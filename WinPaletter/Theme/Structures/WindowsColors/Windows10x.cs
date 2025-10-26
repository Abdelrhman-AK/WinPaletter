﻿using Microsoft.Win32;
using Serilog.Events;
using System;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing Windows 10/11 colors and appearance
    /// </summary>
    public class Windows10x : ManagerBase<Windows10x>
    {
        /// <summary> Controls if Windows 10x colors editing is enabled or not </summary> 
        public bool Enabled { get; set; } = true;

        /// <summary>Color index 0 in registry value array 'AccentPalette' in 'HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent'</summary>
        public Color Color_Index0 { get; set; } = Color.FromArgb(153, 235, 255);

        /// <summary>Color index 1 in registry value array 'AccentPalette' in 'HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent'</summary>
        public Color Color_Index1 { get; set; } = Color.FromArgb(76, 194, 255);

        /// <summary>Color index 2 in registry value array 'AccentPalette' in 'HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent'</summary>
        public Color Color_Index2 { get; set; } = Color.FromArgb(0, 145, 248);

        /// <summary>Color index 3 in registry value array 'AccentPalette' in 'HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent'</summary>
        public Color Color_Index3 { get; set; } = Color.FromArgb(0, 120, 212);

        /// <summary>Color index 4 in registry value array 'AccentPalette' in 'HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent'</summary>
        public Color Color_Index4 { get; set; } = Color.FromArgb(0, 103, 192);

        /// <summary>Color index 5 in registry value array 'AccentPalette' in 'HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent'</summary>
        public Color Color_Index5 { get; set; } = Color.FromArgb(0, 62, 146);

        /// <summary>Color index 6 in registry value array 'AccentPalette' in 'HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent'</summary>
        public Color Color_Index6 { get; set; } = Color.FromArgb(0, 26, 104);

        /// <summary>Color index 7 in registry value array 'AccentPalette' in 'HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent'</summary>
        public Color Color_Index7 { get; set; } = Color.FromArgb(247, 99, 12);

        /// <summary>Light mode for Windows</summary>
        public bool WinMode_Light { get; set; } = true;

        /// <summary>Light mode for applications</summary>
        public bool AppMode_Light { get; set; } = true;

        /// <summary>Transparency effects (Mica/Acrylic)</summary>
        public bool Transparency { get; set; } = true;

        /// <summary>Active titlebar color</summary>
        public Color Titlebar_Active { get; set; } = Color.FromArgb(0, 120, 212);

        /// <summary>Inactive titlebar color</summary>
        public Color Titlebar_Inactive { get; set; } = Color.FromArgb(32, 32, 32);

        /// <summary>Start menu accent color. It is a <b>misnomer</b> as it may not be responsible for start menu if 'Transparency'/'WinMode_Light' are changed.</summary>
        public Color StartMenu_Accent { get; set; } = Color.FromArgb(0, 103, 192);

        /// <summary>Make accent can be applied on titlebars</summary>
        public bool ApplyAccentOnTitlebars { get; set; } = false;

        /// <summary>Choices to apply accents on taskbar alone, taskbar with start and action center, or finally with nothing.</summary>
        public AccentTaskbarLevels ApplyAccentOnTaskbar { get; set; } = Windows10x.AccentTaskbarLevels.None;

        /// <summary>
        /// Increase transparency of taskbar (and removes blur)
        /// <br></br>- Targeting Windows 10 only
        /// </summary>
        public bool IncreaseTBTransparency { get; set; } = false;

        /// <summary>
        /// Make taskbar blur. If false, it will reduce blur power.
        /// <br></br>- Targeting Windows 10 only
        /// </summary>
        public bool TB_Blur { get; set; } = true;

        /// <summary>
        /// Represents the visual styles configuration for the application.
        /// </summary>
        /// <remarks>This field provides access to the visual styles settings, which can be used to
        /// customize the appearance of user interface. It is initialized with default
        /// values.</remarks>
        public VisualStyles VisualStyles { get; set; } = new();

        /// <summary>
        /// Creates a new Windows10x data structure with default values
        /// </summary>
        public Windows10x() { }

        /// <summary>
        /// Enumeration for levels of accent applying
        /// </summary>
        public enum AccentTaskbarLevels
        {
            /// <summary>Applies accent on nothing.</summary>
            None,
            /// <summary>Applies accent on taskbar, start menu and action center.</summary>
            Taskbar_Start_AC,
            /// <summary>Applies accent on taskbar only.</summary>
            Taskbar
        }

        /// <summary>
        /// Loads Windows10x data from registry
        /// </summary>
        /// <param name="default">Default Windows10x data structure</param>
        /// <param name="edition">String edition mark</param>
        public void Load(string edition, Windows10x @default)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Loading Windows {edition} colors and appearance preferences from registry.");

            Enabled = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\Windows10x\{edition}", string.Empty, @default.Enabled);

            VisualStyles.Load(edition, @default.VisualStyles);

            byte[] x;

            byte[] DefColorsBytes =
                [
                    @default.Color_Index0.R, @default.Color_Index0.G, @default.Color_Index0.B, 255,
                        @default.Color_Index1.R, @default.Color_Index1.G, @default.Color_Index1.B, 255,
                        @default.Color_Index2.R, @default.Color_Index2.G, @default.Color_Index2.B, 255,
                        @default.Color_Index3.R, @default.Color_Index3.G, @default.Color_Index3.B, 255,
                        @default.Color_Index4.R, @default.Color_Index4.G, @default.Color_Index4.B, 255,
                        @default.Color_Index5.R, @default.Color_Index5.G, @default.Color_Index5.B, 255,
                        @default.Color_Index6.R, @default.Color_Index6.G, @default.Color_Index6.B, 255,
                        @default.Color_Index7.R, @default.Color_Index7.G, @default.Color_Index7.B, 255
                ];

            x = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentPalette", DefColorsBytes);

            // Use 255 as alpha value for all colors as it is not used in Windows 10/11
            Color_Index0 = Color.FromArgb(/*x[3]*/ 255, x[0], x[1], x[2]);
            Color_Index1 = Color.FromArgb(/*x[7]*/ 255, x[4], x[5], x[6]);
            Color_Index2 = Color.FromArgb(/*x[11]*/ 255, x[8], x[9], x[10]);
            Color_Index3 = Color.FromArgb(/*x[15]*/ 255, x[12], x[13], x[14]);
            Color_Index4 = Color.FromArgb(/*x[19]*/ 255, x[16], x[17], x[18]);
            Color_Index5 = Color.FromArgb(/*x[23]*/ 255, x[20], x[21], x[22]);
            Color_Index6 = Color.FromArgb(/*x[27]*/ 255, x[24], x[25], x[26]);
            Color_Index7 = Color.FromArgb(/*x[31]*/ 255, x[28], x[29], x[30]);

            // Some colors are saved reversed in registry
            StartMenu_Accent = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColorMenu", @default.StartMenu_Accent.Reverse()).Reverse();

            Titlebar_Active = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", @default.Titlebar_Active.Reverse()).Reverse();
            Titlebar_Active = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", @default.Titlebar_Active.Reverse()).Reverse();
            Titlebar_Inactive = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", @default.Titlebar_Inactive.Reverse()).Reverse();

            WinMode_Light = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", @default.WinMode_Light);
            AppMode_Light = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", @default.AppMode_Light);
            Transparency = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", @default.Transparency);
            IncreaseTBTransparency = ReadReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "UseOLEDTaskbarTransparency", @default.IncreaseTBTransparency);

            ApplyAccentOnTaskbar = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", @default.ApplyAccentOnTaskbar);
            ApplyAccentOnTitlebars = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", @default.ApplyAccentOnTitlebars);

            TB_Blur = !(ReadReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\DWM", "ForceEffectMode", (!@default.TB_Blur) ? 1 : 0) == 1);
        }

        /// <summary>
        /// Saves Windows10x data into registry
        /// </summary>
        /// <param name="edition">String edition mark</param>
        /// <param name="treeView">treeView used as theme log</param>
        public void Apply(string edition, TreeView treeView = null)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Saving Windows {edition} colors and appearance preferences into registry.");

            SaveToggleState(edition, treeView);

            bool canApply = (OS.W12 && Program.WindowStyle == PreviewHelpers.WindowStyle.W12) || (OS.W11 && Program.WindowStyle == PreviewHelpers.WindowStyle.W11) || (OS.W10 && Program.WindowStyle == PreviewHelpers.WindowStyle.W10);

            if (Enabled && canApply)
            {
                VisualStyles.Apply(edition, treeView);

                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "AutoColorization", 0);

                // 255 is used as alpha value for all colors as it is not used in Windows 10/11
                byte[] Colors =
                [
                    Color_Index0.R , Color_Index0.G, Color_Index0.B, /*Color_Index0.A*/ 255,
                    Color_Index1.R , Color_Index1.G, Color_Index1.B, /*Color_Index1.A*/ 255,
                    Color_Index2.R , Color_Index2.G, Color_Index2.B, /*Color_Index2.A*/ 255,
                    Color_Index3.R , Color_Index3.G, Color_Index3.B, /*Color_Index3.A*/ 255,
                    Color_Index4.R , Color_Index4.G, Color_Index4.B, /*Color_Index4.A*/ 255,
                    Color_Index5.R , Color_Index5.G, Color_Index5.B, /*Color_Index5.A*/ 255,
                    Color_Index6.R , Color_Index6.G, Color_Index6.B, /*Color_Index6.A*/ 255,
                    Color_Index7.R , Color_Index7.G, Color_Index7.B, /*Color_Index7.A*/ 255
                ];

                switch (ApplyAccentOnTaskbar)
                {
                    case AccentTaskbarLevels.None:
                        {
                            WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", 0);
                            break;
                        }

                    case AccentTaskbarLevels.Taskbar_Start_AC:
                        {
                            WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", 1);
                            break;
                        }

                    case AccentTaskbarLevels.Taskbar:
                        {
                            WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", 2);
                            break;
                        }

                    default:
                        {
                            WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", 0);
                            break;
                        }
                }

                WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", ApplyAccentOnTitlebars ? 1 : 0);

                // Some colors are saved reversed in registry
                WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentPalette", Colors, RegistryValueKind.Binary);
                WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColorMenu", StartMenu_Accent.Reverse().ToArgb());

                WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", Titlebar_Active.Reverse().ToArgb());
                WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", Titlebar_Active.Reverse().ToArgb());
                WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", Titlebar_Inactive.Reverse().ToArgb());

                if (!OS.W10)
                {
                    WriteReg(treeView, @$"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\SystemProtectedUserData\{User.SID}\AnyoneRead\Colors", "AccentColor", Titlebar_Active.Reverse().ToArgb());
                    WriteReg(treeView, @$"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\SystemProtectedUserData\{User.SID}\AnyoneRead\Colors", "StartColor", StartMenu_Accent.Reverse().ToArgb());

                }

                WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", WinMode_Light ? 1 : 0);
                WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", AppMode_Light ? 1 : 0);
                WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", Transparency ? 1 : 0);

                if (OS.W10)
                {
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "UseOLEDTaskbarTransparency", IncreaseTBTransparency ? 1 : 0);
                    WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\DWM", "ForceEffectMode", (!TB_Blur) ? 1 : 0);
                }

                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    // Broadcast the system message to notify about the setting change

                    Program.Log?.Write(LogEventLevel.Information, "Broadcasting system message to notify about the setting change (User32.UpdatePerUserSystemParameters(1, true)).");
                    User32.UpdatePerUserSystemParameters(1, true);

                    Program.Log?.Write(LogEventLevel.Information, "Broadcasting system message to notify about the setting change (User32.SendMessage(IntPtr.Zero, User32.WindowsMessages.WM_SETTINGCHANGE, IntPtr.Zero, IntPtr.Zero)).");
                    User32.SendMessage(IntPtr.Zero, User32.WindowsMessages.WM_SETTINGCHANGE, IntPtr.Zero, IntPtr.Zero);
                    User32.NotifySettingChanged("ImmersiveColorSet");  // for theme/accent
                    User32.NotifySettingChanged("WindowsThemeElement"); // Win8-style themes

                    wic.Undo();
                }
            }
        }

        /// <summary>
        /// Saves Windows10x toggle state into registry
        /// </summary>
        public void SaveToggleState(string edition, TreeView treeView = null)
        {
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\Windows10x\{edition}", string.Empty, Enabled);
        }
    }
}
