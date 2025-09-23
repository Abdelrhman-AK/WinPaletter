﻿using Serilog.Events;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing WinPaletter appearance
    /// </summary>
    public class AppTheme : ICloneable
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled = false;

        /// <summary>Back color of forms</summary>
        public Color BackColor = DefaultColors.BackColor_Dark;

        /// <summary>Accent color for controls</summary>
        public Color AccentColor = DefaultColors.PrimaryColor_Dark;

        /// <summary> Secondary color for controls </summary>
        public Color SecondaryColor = DefaultColors.SecondaryColor_Dark;

        /// <summary> Tertiary color for controls </summary>
        public Color TertiaryColor = DefaultColors.TertiaryColor_Dark;

        /// <summary> Disabled color for controls </summary>
        public Color DisabledColor = DefaultColors.DisabledColor_Dark;

        /// <summary> Disabled back color for controls </summary>
        public Color DisabledBackColor = DefaultColors.DisabledBackColor_Dark;

        /// <summary>Make WinPaletter in dark mode</summary>
        public bool DarkMode = true;

        /// <summary>Make controls have rounded corners</summary>
        public bool RoundCorners = OS.WXP || OS.WVista || OS.W7 || OS.W11 || OS.W12;

        /// <summary>Enable animations</summary>
        public bool Animations = true;

        /// <summary>
        /// Creates a new AppTheme structure
        /// </summary>
        public AppTheme() { }

        /// <summary>
        /// Loads AppTheme data from registry
        /// </summary>
        /// <param name="default">Default AppTheme data structure</param>
        public void Load(AppTheme @default)
        {
            if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, $"Loading WinPaletter application theme (appearance) from registry.");

            Enabled = ReadReg(Settings.Structures.REG_Appearance, "CustomColors", @default.Enabled);
            Animations = ReadReg(Settings.Structures.REG_Appearance, "Animations", @default.Animations);
            BackColor = ReadReg(Settings.Structures.REG_Appearance, "BackColor", @default.BackColor);
            AccentColor = ReadReg(Settings.Structures.REG_Appearance, "AccentColor", @default.AccentColor);
            SecondaryColor = ReadReg(Settings.Structures.REG_Appearance, "SecondaryColor", @default.SecondaryColor);
            TertiaryColor = ReadReg(Settings.Structures.REG_Appearance, "TertiaryColor", @default.TertiaryColor);
            DisabledColor = ReadReg(Settings.Structures.REG_Appearance, "DisabledColor", @default.DisabledColor);
            DisabledBackColor = ReadReg(Settings.Structures.REG_Appearance, "DisabledBackColor", @default.DisabledBackColor);
            DarkMode = ReadReg(Settings.Structures.REG_Appearance, "CustomTheme", @default.DarkMode);
            RoundCorners = ReadReg(Settings.Structures.REG_Appearance, "RoundedCorners", @default.RoundCorners);
        }

        /// <summary>
        /// Saves AppTheme data into registry
        /// </summary>
        /// <param name="treeView">treeView used as theme log</param>
        public void Apply(TreeView treeView = null)
        {
            if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, $"Saving WinPaletter application theme (appearance) into registry.");

            SaveToggleState(treeView);

            WriteReg(treeView, Settings.Structures.REG_Appearance, "BackColor", BackColor.ToArgb());
            WriteReg(treeView, Settings.Structures.REG_Appearance, "AccentColor", AccentColor.ToArgb());
            WriteReg(treeView, Settings.Structures.REG_Appearance, "CustomTheme", DarkMode);
            WriteReg(treeView, Settings.Structures.REG_Appearance, "RoundedCorners", RoundCorners);
            WriteReg(treeView, Settings.Structures.REG_Appearance, "Animations", Animations);
            WriteReg(treeView, Settings.Structures.REG_Appearance, "SecondaryColor", SecondaryColor.ToArgb());
            WriteReg(treeView, Settings.Structures.REG_Appearance, "TertiaryColor", TertiaryColor.ToArgb());
            WriteReg(treeView, Settings.Structures.REG_Appearance, "DisabledColor", DisabledColor.ToArgb());
            WriteReg(treeView, Settings.Structures.REG_Appearance, "DisabledBackColor", DisabledBackColor.ToArgb());

            // Apply settings to program settings
            {
                if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, $"Applying WinPaletter application theme (appearance) settings to program settings.");

                ref Settings.Structures.Appearance Appearance = ref Program.Settings.Appearance;
                Appearance.CustomColors = Enabled;
                Appearance.BackColor = BackColor;
                Appearance.AccentColor = AccentColor;
                Appearance.CustomTheme_DarkMode = DarkMode;
                Appearance.RoundedCorners = RoundCorners;
                Appearance.Animations = Animations;
                Appearance.SecondaryColor = SecondaryColor;
                Appearance.TertiaryColor = TertiaryColor;
                Appearance.DisabledColor = DisabledColor;
                Appearance.DisabledBackColor = DisabledBackColor;
            }

            ApplyStyle();
        }

        /// <summary>
        /// Saves WinPaletterApplicationTheme toggle state into registry
        /// </summary>
        public void SaveToggleState(TreeView treeView = null)
        {
            WriteReg(treeView, Settings.Structures.REG_Appearance, "CustomColors", Enabled);
        }

        /// <summary>Operator to check if two AppTheme structures are equal</summary>
        public static bool operator ==(AppTheme First, AppTheme Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two AppTheme structures are not equal</summary>
        public static bool operator !=(AppTheme First, AppTheme Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones AppTheme structure</summary>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Checks if two AppTheme structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>Get hash code of AppTheme structure</summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
