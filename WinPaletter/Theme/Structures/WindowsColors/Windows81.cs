﻿using Microsoft.Win32;
using System;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.CMD;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing Windows 8.1 appearance
    /// </summary>
    public class Windows81 : ICloneable
    {
        /// <summary> Controls if Windows 8.1 colors editing is enabled or not </summary> 
        public bool Enabled = true;

        /// <summary>Start screen background ID. It can be any number from 1 to 20.</summary>
        public int Start;

        /// <summary>Main Windows color</summary>
        public Color ColorizationColor = Color.FromArgb(246, 195, 74);

        /// <summary>Control amount of main Windows color</summary>
        public int ColorizationColorBalance = 78;

        /// <summary>Start screen background color</summary>
        public Color StartColor = Color.FromArgb(30, 0, 84);

        /// <summary>Accent color for start screen and UWP apps</summary>
        public Color AccentColor = Color.FromArgb(72, 29, 178);

        /// <summary>
        /// WinTheme used for Windows 8.1
        /// <code>
        /// Aero
        /// AeroLite
        /// Basic
        /// Classic
        /// </code>
        /// </summary>
        public Windows7.Themes Theme = Windows7.Themes.Aero;

        /// <summary>Start screen background color (secondary)</summary>
        public Color PersonalColors_Background = Color.FromArgb(30, 0, 84);

        /// <summary>Accent color for start screen and UWP apps (secondary)</summary>
        public Color PersonalColors_Accent = Color.FromArgb(72, 29, 178);

        /// <summary>
        /// Creates new Windows81 data structure
        /// </summary>
        public Windows81() { }

        /// <summary>
        /// Loads Windows81 data from registry
        /// </summary>
        /// <param name="default">Default Windows81 data structure</param>
        public void Load(Windows81 @default)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Loading Windows 8.1 colors and appearance preferences from registry.");

            Enabled = Convert.ToBoolean(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\Windows8.1", string.Empty, @default.Enabled));

            if (OS.W81)
            {
                object y;

                string stringThemeName = UxTheme.GetCurrentVS().Item1;

                if (stringThemeName.ToString().Split('\\').Last().ToLower() == "aerolite.msstyles" | string.IsNullOrWhiteSpace(stringThemeName.ToString()))
                {
                    Theme = Windows7.Themes.AeroLite;
                }
                else
                {
                    Theme = Windows7.Themes.Aero;
                }

                y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", @default.ColorizationColor.ToArgb());
                ColorizationColor = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(y)));

                y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", @default.ColorizationColorBalance);
                ColorizationColorBalance = Convert.ToInt32(y);

                y = GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColor", Color.FromArgb(84, 0, 30).ToArgb());
                StartColor = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(y))).Reverse();

                y = GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColor", Color.FromArgb(178, 29, 72).ToArgb());
                AccentColor = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(y))).Reverse();

                string S;

                S = GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Background", @default.PersonalColors_Background.ToStringHex(false, true)).ToString();
                PersonalColors_Background = S.ToColorFromHex();

                S = GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Accent", @default.PersonalColors_Accent.ToStringHex(false, true)).ToString();
                PersonalColors_Accent = S.ToColorFromHex();

                Start = Convert.ToInt32(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "ForceStartBackground", 0));
            }
            else
            {
                Theme = @default.Theme;
                StartColor = @default.StartColor;
                AccentColor = @default.AccentColor;
                PersonalColors_Background = @default.PersonalColors_Background;
                PersonalColors_Accent = @default.PersonalColors_Accent;
                Start = @default.Start;
            }
        }

        /// <summary>
        /// Saves Windows81 data into registry
        /// </summary>
        /// <param name="TM">Theme manager</param>
        /// <param name="treeView">treeView used as theme log</param>
        public void Apply(Theme.Manager TM, TreeView treeView = null)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Saving Windows 8.1 colors and appearance preferences into registry.");

            SaveToggleState(treeView);

            if (Enabled)
            {
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "AutoColorization", 0);

                switch (Theme)
                {
                    case Windows7.Themes.Aero:
                        {
                            UxTheme.EnableTheming(1);
                            UxTheme.SetSystemVisualStyle($@"{SysPaths.Windows}\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0);
                            break;
                        }

                    case Windows7.Themes.AeroLite:
                        {
                            UxTheme.EnableTheming(1);
                            UxTheme.SetSystemVisualStyle($@"{SysPaths.Windows}\resources\Themes\Aero\AeroLite.msstyles", "NormalColor", "NormalSize", 0);

                            DelKey(treeView, "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\HighContrast\\Pre-High Contrast Scheme");

                            EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes", "CurrentTheme", string.Empty, RegistryValueKind.String);
                            EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes", "LastHighContrastTheme", string.Empty, RegistryValueKind.String);
                            break;
                        }
                }

                EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", ColorizationColor.ToArgb());
                EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", ColorizationColorBalance);

                EditReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColor", StartColor.Reverse().ToArgb());
                EditReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultStartColor", StartColor.Reverse().ToArgb());
                EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultStartColor", StartColor.Reverse().ToArgb());
                EditReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColor", AccentColor.Reverse().ToArgb());

                EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "ForceStartBackground", Start);
                EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Background", $"#{PersonalColors_Background.ToStringHex(false)}", RegistryValueKind.String);
                EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Accent", $"#{PersonalColors_Accent.ToStringHex(false)}", RegistryValueKind.String);

                Program.RefreshDWM(TM);

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
        /// Saves Windows81 toggle state into registry
        /// </summary>
        public void SaveToggleState(TreeView treeView = null)
        {
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\Windows8.1", string.Empty, Enabled);
        }

        /// <summary>Operator to check if two Windows81 structures are equal</summary>
        public static bool operator ==(Windows81 First, Windows81 Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two Windows81 structures are not equal</summary>
        public static bool operator !=(Windows81 First, Windows81 Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones Windows81 structure</summary>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Checks if two Windows81 structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>Get hash code of Windows81 structure</summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
