﻿using Microsoft.Win32;
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
    public class Windows10x : ICloneable
    {
        /// <summary> Controls if Windows 10x colors editing is enabled or not </summary> 
        public bool Enabled = true;

        /// <summary>Color index 0 in registry value array 'AccentPalette' in 'HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent'</summary>
        public Color Color_Index0 = Color.FromArgb(153, 235, 255);

        /// <summary>Color index 1 in registry value array 'AccentPalette' in 'HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent'</summary>
        public Color Color_Index1 = Color.FromArgb(76, 194, 255);

        /// <summary>Color index 2 in registry value array 'AccentPalette' in 'HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent'</summary>
        public Color Color_Index2 = Color.FromArgb(0, 145, 248);

        /// <summary>Color index 3 in registry value array 'AccentPalette' in 'HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent'</summary>
        public Color Color_Index3 = Color.FromArgb(0, 120, 212);

        /// <summary>Color index 4 in registry value array 'AccentPalette' in 'HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent'</summary>
        public Color Color_Index4 = Color.FromArgb(0, 103, 192);

        /// <summary>Color index 5 in registry value array 'AccentPalette' in 'HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent'</summary>
        public Color Color_Index5 = Color.FromArgb(0, 62, 146);

        /// <summary>Color index 6 in registry value array 'AccentPalette' in 'HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent'</summary>
        public Color Color_Index6 = Color.FromArgb(0, 26, 104);

        /// <summary>Color index 7 in registry value array 'AccentPalette' in 'HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent'</summary>
        public Color Color_Index7 = Color.FromArgb(247, 99, 12);

        /// <summary>Light mode for Windows</summary>
        public bool WinMode_Light = true;

        /// <summary>Light mode for applications</summary>
        public bool AppMode_Light = true;

        /// <summary>Transparency effects (Mica/Acrylic)</summary>
        public bool Transparency = true;

        /// <summary>Active titlebar color</summary>
        public Color Titlebar_Active = Color.FromArgb(0, 120, 212);

        /// <summary>Inactive titlebar color</summary>
        public Color Titlebar_Inactive = Color.FromArgb(32, 32, 32);

        /// <summary>Start menu accent color. It is a <b>misnomer</b> as it may not be responsible for start menu if 'Transparency'/'WinMode_Light' are changed.</summary>
        public Color StartMenu_Accent = Color.FromArgb(0, 103, 192);

        /// <summary>Make accent can be applied on titlebars</summary>
        public bool ApplyAccentOnTitlebars = false;

        /// <summary>Choices to apply accents on taskbar alone, taskbar with start and action center, or finally with nothing.</summary>
        public AccentTaskbarLevels ApplyAccentOnTaskbar = Windows10x.AccentTaskbarLevels.None;

        /// <summary>
        /// Increase transparency of taskbar (and removes blur)
        /// <br></br>- Targeting Windows 10 only
        /// </summary>
        public bool IncreaseTBTransparency = false;

        /// <summary>
        /// Make taskbar blur. If false, it will reduce blur power.
        /// <br></br>- Targeting Windows 10 only
        /// </summary>
        public bool TB_Blur = true;

        /// <summary>
        /// Represents the visual styles configuration for the application.
        /// </summary>
        /// <remarks>This field provides access to the visual styles settings, which can be used to
        /// customize the appearance of user interface. It is initialized with default
        /// values.</remarks>
        public VisualStyles VisualStyles = new();

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
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Loading Windows {edition} colors and appearance preferences from registry.");

            Enabled = Convert.ToBoolean(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\Windows10x\{edition}", string.Empty, @default.Enabled));

            VisualStyles.Load(edition, @default.VisualStyles);

            byte[] x;
            object y;

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

            x = (byte[])GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentPalette", DefColorsBytes);

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
            y = GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColorMenu", @default.StartMenu_Accent.Reverse().ToArgb());
            StartMenu_Accent = Color.FromArgb(Convert.ToInt32(y)).Reverse();

            y = GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", @default.Titlebar_Active.Reverse().ToArgb());
            Titlebar_Active = Color.FromArgb(Convert.ToInt32(y)).Reverse();

            y = GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", @default.Titlebar_Active.Reverse().ToArgb());
            Titlebar_Active = Color.FromArgb(Convert.ToInt32(y)).Reverse();

            y = GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", @default.Titlebar_Inactive.Reverse().ToArgb());
            Titlebar_Inactive = Color.FromArgb(Convert.ToInt32(y)).Reverse();

            WinMode_Light = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", @default.WinMode_Light));
            AppMode_Light = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", @default.AppMode_Light));
            Transparency = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", @default.Transparency));
            IncreaseTBTransparency = Convert.ToBoolean(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "UseOLEDTaskbarTransparency", @default.IncreaseTBTransparency));

            switch (GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", @default.ApplyAccentOnTaskbar))
            {
                case 0:
                    {
                        ApplyAccentOnTaskbar = AccentTaskbarLevels.None;
                        break;
                    }
                case 1:
                    {
                        ApplyAccentOnTaskbar = AccentTaskbarLevels.Taskbar_Start_AC;
                        break;
                    }
                case 2:
                    {
                        ApplyAccentOnTaskbar = AccentTaskbarLevels.Taskbar;
                        break;
                    }

                default:
                    {
                        ApplyAccentOnTaskbar = AccentTaskbarLevels.None;
                        break;
                    }
            }

            ApplyAccentOnTitlebars = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", @default.ApplyAccentOnTitlebars));
            TB_Blur = !(Convert.ToInt32(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\DWM", "ForceEffectMode", (!@default.TB_Blur) ? 1 : 0)) == 1);
        }

        /// <summary>
        /// Saves Windows10x data into registry
        /// </summary>
        /// <param name="edition">String edition mark</param>
        /// <param name="treeView">treeView used as theme log</param>
        public void Apply(string edition, TreeView treeView = null)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Saving Windows {edition} colors and appearance preferences into registry.");

            SaveToggleState(edition, treeView);

            bool canApply = (OS.W12 && Program.WindowStyle == PreviewHelpers.WindowStyle.W12) || (OS.W11 && Program.WindowStyle == PreviewHelpers.WindowStyle.W11) || (OS.W10 && Program.WindowStyle == PreviewHelpers.WindowStyle.W10);

            if (Enabled && canApply)
            {
                VisualStyles.Apply(edition, treeView);

                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "AutoColorization", 0);

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
                            EditReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", 0);
                            break;
                        }

                    case AccentTaskbarLevels.Taskbar_Start_AC:
                        {
                            EditReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", 1);
                            break;
                        }

                    case AccentTaskbarLevels.Taskbar:
                        {
                            EditReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", 2);
                            break;
                        }

                    default:
                        {
                            EditReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", 0);
                            break;
                        }
                }

                EditReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", ApplyAccentOnTitlebars ? 1 : 0);

                // Some colors are saved reversed in registry
                EditReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentPalette", Colors, RegistryValueKind.Binary);
                EditReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColorMenu", StartMenu_Accent.Reverse().ToArgb());

                EditReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", Titlebar_Active.Reverse().ToArgb());
                EditReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", Titlebar_Active.Reverse().ToArgb());
                EditReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", Titlebar_Inactive.Reverse().ToArgb());

                if (!OS.W10)
                {
                    EditReg(treeView, @$"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\SystemProtectedUserData\{User.SID}\AnyoneRead\Colors", "AccentColor", Titlebar_Active.Reverse().ToArgb());
                    EditReg(treeView, @$"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\SystemProtectedUserData\{User.SID}\AnyoneRead\Colors", "StartColor", StartMenu_Accent.Reverse().ToArgb());
                }

                EditReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", WinMode_Light ? 1 : 0);
                EditReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", AppMode_Light ? 1 : 0);
                EditReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", Transparency ? 1 : 0);

                if (OS.W10)
                {
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "UseOLEDTaskbarTransparency", IncreaseTBTransparency ? 1 : 0);
                    EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\DWM", "ForceEffectMode", (!TB_Blur) ? 1 : 0);
                }

                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    // Broadcast the system message to notify about the setting change
                    Program.Log?.Write(Serilog.Events.LogEventLevel.Information, "Broadcasting system message to notify about the setting change (User32.SendMessage(IntPtr.Zero, User32.WindowsMessages.WM_SETTINGCHANGE, IntPtr.Zero, IntPtr.Zero)).");
                    
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
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\Windows10x\{edition}", string.Empty, Enabled);
        }

        /// <summary>Operator to check if two Windows10x structures are equal</summary>
        public static bool operator ==(Windows10x First, Windows10x Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two Windows10x structures are not equal</summary>
        public static bool operator !=(Windows10x First, Windows10x Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones Windows10x structure</summary>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Checks if two Windows10x structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>Get hash code of Windows10x structure</summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
