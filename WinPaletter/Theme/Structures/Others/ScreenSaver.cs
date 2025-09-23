using Microsoft.Win32;
using Serilog.Events;
using System;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing Windows screen saver
    /// </summary>
    public class ScreenSaver : ICloneable
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled = false;

        /// <summary>Lock Windows after closure of screen saver</summary>
        public bool IsSecure = false;

        /// <summary>Inactivity (idle) time after which the screen saver will start</summary>
        public int TimeOut = 60;

        /// <summary>Screen saver File</summary>
        public string File = string.Empty;

        /// <summary>
        /// Creates new ScreenSaver structure with default values
        /// </summary>
        public ScreenSaver() { }

        /// <summary>
        /// Loads ScreenSaver data from registry
        /// </summary>
        /// <param name="default">Default ScreenSaver data structure</param>
        public void Load(ScreenSaver @default)
        {
            if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, $"Loading Windows Screen Saver settings from registry.");

            Enabled = ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaveActive", @default.Enabled);
            IsSecure = ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaverIsSecure", @default.IsSecure);
            TimeOut = ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaveTimeOut", @default.TimeOut);
            File = ReadReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "SCRNSAVE.EXE", @default.File);
        }

        /// <summary>
        /// Saves ScreenSaver data into registry
        /// </summary>
        /// <param name="treeView">treeView used as theme log</param>
        public void Apply(TreeView treeView = null)
        {
            if (Program.Settings.AppLog.Enabled) Program.Log?.Write(LogEventLevel.Information, $"Saving Windows Screen Saver settings into registry.");

            SaveToggleState(treeView);

            WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaverIsSecure", IsSecure ? 1 : 0, RegistryValueKind.String);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaveTimeOut", TimeOut, RegistryValueKind.String);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "SCRNSAVE.EXE", File, RegistryValueKind.String);
        }

        /// <summary>
        /// Saves ScreenSaver toggle into registry
        /// </summary>
        /// <param name="treeView"></param>
        public void SaveToggleState(TreeView treeView = null)
        {
            WriteReg(treeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaveActive", Enabled ? 1 : 0, RegistryValueKind.String);
        }

        /// <summary>Operator to check if two ScreenSaver structures are equal</summary>
        public static bool operator ==(ScreenSaver First, ScreenSaver Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two ScreenSaver structures are not equal</summary>
        public static bool operator !=(ScreenSaver First, ScreenSaver Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones ScreenSaver structure</summary>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Checks if two ScreenSaver structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>Get hash code of ScreenSaver structure</summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
