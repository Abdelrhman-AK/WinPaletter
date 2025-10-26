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
    /// Structure responsible for managing Windows 8.1 appearance
    /// </summary>
    public class Windows81 : ManagerBase<Windows81>
    {
        /// <summary> Controls if Windows 8.1 colors editing is enabled or not </summary> 
        public bool Enabled { get; set; } = true;

        /// <summary>Start screen background ID. It can be any number from 1 to 20.</summary>
        public int Start;

        /// <summary>Main Windows color</summary>
        public Color ColorizationColor { get; set; } = Color.FromArgb(246, 195, 74);

        /// <summary>Control amount of main Windows color</summary>
        public int ColorizationColorBalance { get; set; } = 78;

        /// <summary>Start screen background color</summary>
        public Color StartColor { get; set; } = Color.FromArgb(30, 0, 84);

        /// <summary>Accent color for start screen and UWP apps</summary>
        public Color AccentColor { get; set; } = Color.FromArgb(72, 29, 178);

        /// <summary>
        /// Represents the visual styles configuration for the application.
        /// </summary>
        /// <remarks>This field provides access to the visual styles settings, which can be used to
        /// customize the appearance of user interface. It is initialized with default
        /// values.</remarks>
        public VisualStyles VisualStyles { get; set; } = new();

        /// <summary>Start screen background color (secondary)</summary>
        public Color PersonalColors_Background { get; set; } = Color.FromArgb(30, 0, 84);

        /// <summary>Accent color for start screen and UWP apps (secondary)</summary>
        public Color PersonalColors_Accent { get; set; } = Color.FromArgb(72, 29, 178);

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
            Program.Log?.Write(LogEventLevel.Information, $"Loading Windows 8.1 colors and appearance preferences from registry.");

            Enabled = ReadReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\Windows8.1", string.Empty, @default.Enabled);

            VisualStyles.Load("8.1", @default.VisualStyles);

            ColorizationColor = ReadReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", @default.ColorizationColor);
            ColorizationColorBalance = ReadReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", @default.ColorizationColorBalance);

            StartColor = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColor", @default.StartColor.Reverse()).Reverse();
            AccentColor = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColor", @default.AccentColor.Reverse()).Reverse();

            PersonalColors_Background = ReadReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Background", @default.PersonalColors_Background.ToString(Settings.Structures.NerdStats.Formats.HEX, false)).ToColorFromHex();
            PersonalColors_Accent = ReadReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Accent", @default.PersonalColors_Accent.ToString(Settings.Structures.NerdStats.Formats.HEX, false)).ToColorFromHex();

            Start = ReadReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "ForceStartBackground", 0);
        }

        /// <summary>
        /// Saves Windows81 data into registry
        /// </summary>
        /// <param name="TM">Theme manager</param>
        /// <param name="treeView">treeView used as theme log</param>
        public void Apply(Manager TM, TreeView treeView = null)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Saving Windows 8.1 colors and appearance preferences into registry.");

            SaveToggleState(treeView);

            if (Enabled)
            {
                WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "AutoColorization", 0);

                VisualStyles.Apply("8.1", treeView);

                WriteReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", ColorizationColor.ToArgb());
                WriteReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", ColorizationColorBalance);

                WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColor", StartColor.Reverse().ToArgb());
                WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultStartColor", StartColor.Reverse().ToArgb());
                WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultStartColor", StartColor.Reverse().ToArgb());
                WriteReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColor", AccentColor.Reverse().ToArgb());

                WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "ForceStartBackground", Start);
                WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Background", $"#{PersonalColors_Background.ToString(Settings.Structures.NerdStats.Formats.HEX, false)}", RegistryValueKind.String);
                WriteReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Accent", $"#{PersonalColors_Accent.ToString(Settings.Structures.NerdStats.Formats.HEX, false)}", RegistryValueKind.String);

                Program.RefreshDWM(TM);

                using (WindowsImpersonationContext wic = User.Identity.Impersonate())
                {
                    // Broadcast the system message to notify about the setting change
                    Program.Log?.Write(LogEventLevel.Information, "Broadcasting system message to notify about the setting change (User32.SendMessage(IntPtr.Zero, User32.WindowsMessages.WM_SETTINGCHANGE, IntPtr.Zero, IntPtr.Zero)).");

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
            WriteReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\Windows8.1", string.Empty, Enabled);
        }
    }
}
