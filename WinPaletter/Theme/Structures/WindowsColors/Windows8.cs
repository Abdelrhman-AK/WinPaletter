﻿using Serilog.Events;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing Windows 8 appearance
    /// </summary>
    public class Windows8 : ICloneable
    {
        /// <summary> Controls if Windows 8 colors editing is enabled or not </summary> 
        public bool Enabled = true;

        /// <summary>Main Windows color</summary>
        public Color ColorizationColor = Color.FromArgb(76, 159, 253);

        /// <summary>Control amount of main Windows color</summary>
        public int ColorizationColorBalance = 78;

        /// <summary>Start screen background ID.</summary>
        public int StartBackground = 0;

        /// <summary>Color scheme ID.</summary>
        public int ColorSet_Version3 = 8;

        /// <summary>
        /// Represents the visual styles configuration for the application.
        /// </summary>
        /// <remarks>This field provides access to the visual styles settings, which can be used to
        /// customize the appearance of user interface. It is initialized with default
        /// values.</remarks>
        public VisualStyles VisualStyles = new();

        /// <summary>
        /// Creates new Windows8 data structure
        /// </summary>
        public Windows8() { }

        /// <summary>
        /// Loads Windows8 data from registry
        /// </summary>
        /// <param name="default">Default Windows8 data structure</param>
        public void Load(Windows8 @default)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Loading Windows 8.1 colors and appearance preferences from registry.");

            Enabled = Convert.ToBoolean(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\Windows8", string.Empty, @default.Enabled));

            VisualStyles.Load("8", @default.VisualStyles);

            object y;
            y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", @default.ColorizationColor.ToArgb());
            ColorizationColor = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(y)));

            y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", @default.ColorizationColorBalance);
            ColorizationColorBalance = Convert.ToInt32(y);

            StartBackground = (int)GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentId_v8.00", @default.StartBackground);
            ColorSet_Version3 = (int)GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Accent", "ColorSet_Version3", @default.ColorSet_Version3);
        }

        /// <summary>
        /// Saves Windows8 data into registry
        /// </summary>
        /// <param name="TM">Theme manager used to apply theme</param>
        /// <param name="treeView">treeView used as theme log</param>
        public void Apply(Manager TM, TreeView treeView = null)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Saving Windows 8.1 colors and appearance preferences into registry.");

            SaveToggleState(treeView);

            if (Enabled)
            {
                EditReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "AutoColorization", 0);

                VisualStyles.Apply("8", treeView);

                EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", ColorizationColor.ToArgb());
                EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", ColorizationColorBalance);

                EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Accent\NoRoam", "UseCustomColorSet", 0);
                EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "NoChangingStartMenuBackground", 0);

                EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Accent", "ColorSet_Version3", ColorSet_Version3);

                // Affects whole system, not just current user and includes lock screen background
                EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultColorSet", ColorSet_Version3);

                EditReg(treeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentId_v8.00", StartBackground);

                Program.RefreshDWM(TM);
            }
        }

        /// <summary>
        /// Saves Windows8 toggle state into registry
        /// </summary>
        public void SaveToggleState(TreeView treeView = null)
        {
            EditReg(treeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\Windows8", string.Empty, Enabled);
        }

        /// <summary>Operator to check if two Windows8 structures are equal</summary>
        public static bool operator ==(Windows8 First, Windows8 Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two Windows8 structures are not equal</summary>
        public static bool operator !=(Windows8 First, Windows8 Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones Windows8 structure</summary>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Checks if two Windows8 structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>Get hash code of Windows8 structure</summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
