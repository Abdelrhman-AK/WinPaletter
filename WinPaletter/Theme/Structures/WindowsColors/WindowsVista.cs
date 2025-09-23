﻿using Microsoft.Win32;
using Serilog.Events;
using System;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing Windows Vista appearance
    /// </summary>
    public class WindowsVista : ICloneable
    {
        /// <summary> Controls if Windows Vista colors editing is enabled or not </summary> 
        public bool Enabled = true;

        /// <summary>Main Windows color</summary>
        public Color ColorizationColor = Color.FromArgb(64, 158, 254);

        /// <summary>Control amount of main Windows color</summary>
        public byte Alpha;

        /// <summary>
        /// Represents the visual styles configuration for the application.
        /// </summary>
        /// <remarks>This field provides access to the visual styles settings, which can be used to
        /// customize the appearance of user interface. It is initialized with default
        /// values.</remarks>
        public VisualStyles VisualStyles = new();

        /// <summary>
        /// Creates new WindowsVista data structure with default values
        /// </summary>
        public WindowsVista() { }

        /// <summary>
        /// Loads WindowsVista data from registry
        /// </summary>
        /// <param name="default">Default WindowsVista data structure</param>
        public void Load(WindowsVista @default)
        {
            if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, $"Loading Windows Vista colors and appearance preferences from registry.");

            Enabled = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\WindowsVista", string.Empty, @default.Enabled);

            ColorizationColor = Color.FromArgb(255, ReadReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", @default.ColorizationColor));
            Alpha = ReadReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", @default.ColorizationColor).A;

            VisualStyles.Load("Vista", @default.VisualStyles);
        }

        /// <summary>
        /// Saves WindowsVista data into registry
        /// </summary>
        /// <param name="treeView">treeView used as theme log</param>
        public void Apply(TreeView treeView = null)
        {
            if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, $"Saving Windows Vista colors and appearance preferences into registry.");

            SaveToggleState(treeView);

            if (Enabled)
            {
                VisualStyles.Apply("Vista", treeView);

                WriteReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", Color.FromArgb(Alpha, ColorizationColor).ToArgb(), RegistryValueKind.DWord);

                // Broadcast the system message to notify about the setting change
                User32.SendMessage(IntPtr.Zero, User32.WindowsMessages.WM_SETTINGCHANGE, IntPtr.Zero, IntPtr.Zero);
            }
        }

        /// <summary>
        /// Saves WindowsVista toggle state into registry
        /// </summary>
        /// <param name="treeView"></param>
        public void SaveToggleState(TreeView treeView = null)
        {
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\WindowsVista", string.Empty, Enabled);
        }

        /// <summary>Operator to check if two WindowsVista structures are equal</summary>
        public static bool operator ==(WindowsVista First, WindowsVista Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two WindowsVista structures are not equal</summary>
        public static bool operator !=(WindowsVista First, WindowsVista Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones WindowsVista structure</summary>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Checks if two WindowsVista structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>Get hash code of WindowsVista structure</summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
