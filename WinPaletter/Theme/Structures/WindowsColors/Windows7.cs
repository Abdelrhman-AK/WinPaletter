﻿using Serilog.Events;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing Windows 7 appearance
    /// </summary>
    public class Windows7 : ICloneable
    {
        /// <summary> Controls if Windows 7 colors editing is enabled or not </summary> 
        public bool Enabled = true;

        /// <summary>Main Windows color</summary>
        public Color ColorizationColor = Color.FromArgb(116, 184, 252);

        /// <summary>Glow or blur color</summary>
        public Color ColorizationAfterglow = Color.FromArgb(116, 184, 252);

        /// <summary>Control amount of main Windows color</summary>
        public int ColorizationColorBalance = 8;

        /// <summary>Control amount of glow color</summary>
        public int ColorizationAfterglowBalance = 43;

        /// <summary>Control amount of blur power for aero glass</summary>
        public int ColorizationBlurBalance = 49;

        /// <summary>Control amount of aero glass reflection</summary>
        public int ColorizationGlassReflectionIntensity = 0;

        /// <summary>
        /// Represents the visual styles configuration for the application.
        /// </summary>
        /// <remarks>This field provides access to the visual styles settings, which can be used to
        /// customize the appearance of user interface. It is initialized with default
        /// values.</remarks>
        public VisualStyles VisualStyles = new();

        /// <summary>
        /// Creates Windows7 data structure with default values
        /// </summary>
        public Windows7() { }

        /// <summary>
        /// Loads Windows7 data from registry
        /// </summary>
        /// <param name="default">Default Windows7 data structure</param>
        public void Load(Windows7 @default)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Loading Windows 7 colors and appearance preferences from registry.");

            Enabled = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\Windows7", string.Empty, @default.Enabled);

            ColorizationColor = Color.FromArgb(255, ReadReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", @default.ColorizationColor));
            ColorizationColorBalance = ReadReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", @default.ColorizationColorBalance);

            ColorizationAfterglow = Color.FromArgb(255, ReadReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglow", @default.ColorizationAfterglow));
            ColorizationAfterglowBalance = ReadReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglowBalance", @default.ColorizationAfterglowBalance);

            ColorizationBlurBalance = ReadReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationBlurBalance", @default.ColorizationBlurBalance);
            ColorizationGlassReflectionIntensity = ReadReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationGlassReflectionIntensity", @default.ColorizationGlassReflectionIntensity);

            VisualStyles.Load("7", @default.VisualStyles);
        }

        /// <summary>
        /// Saves Windows7 data into registry
        /// </summary>
        /// <param name="TM">Theme manager used for refreshing DWM colors</param>
        /// <param name="treeView">treeView used as theme log</param>
        public void Apply(Manager TM, TreeView treeView = null)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Saving Windows 7 colors and appearance preferences into registry.");

            SaveToggleState(treeView);

            if (Enabled)
            {
                VisualStyles.Apply("7", treeView);

                WriteReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglow", ColorizationAfterglow.ToArgb());
                WriteReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglowBalance", ColorizationAfterglowBalance);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationBlurBalance", ColorizationBlurBalance);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationGlassReflectionIntensity", ColorizationGlassReflectionIntensity);

                WriteReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", ColorizationColor.ToArgb());
                WriteReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", ColorizationColorBalance);
                WriteReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "EnableWindowColorization", 1);

                Program.RefreshDWM(TM);
            }
        }

        /// <summary>
        /// Saves Windows7 toggle state into registry
        /// </summary>
        public void SaveToggleState(TreeView treeView = null)
        {
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\Windows7", string.Empty, Enabled);
        }

        /// <summary>Operator to check if two Windows7 structures are equal</summary>
        public static bool operator ==(Windows7 First, Windows7 Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two Windows7 structures are not equal</summary>
        public static bool operator !=(Windows7 First, Windows7 Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones Windows7 structure</summary>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Checks if two Windows7 structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>Get hash code of Windows7 structure</summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
