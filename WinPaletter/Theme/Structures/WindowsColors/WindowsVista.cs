﻿using System;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.CMD;

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
        /// Theme type used for Windows Vista
        /// <code>
        /// Aero
        /// AeroOpaque
        /// Basic
        /// Classic
        /// </code>
        /// </summary>
        public Windows7.Themes Theme = Windows7.Themes.Aero;

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
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Loading Windows Vista colors and appearance preferences from registry.");

            Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\WindowsVista", string.Empty, @default.Enabled));

            if (OS.WVista)
            {
                object y;

                y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", @default.ColorizationColor.ToArgb());
                ColorizationColor = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(y)));
                Alpha = Color.FromArgb(Convert.ToInt32(y)).A;

                bool Opaque = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", false));

                bool Classic;

                try
                {
                    string stringThemeName = UxTheme.GetCurrentVS().Item1;
                    Classic = string.IsNullOrWhiteSpace(stringThemeName.ToString()) | !System.IO.File.Exists(stringThemeName.ToString());
                }
                catch // Couldn't get current visual styles, lets assume that it is not classic.
                {
                    Classic = false;
                }

                if (Classic)
                {
                    Theme = Windows7.Themes.Classic;
                }
                else if (DWMAPI.IsCompositionEnabled())
                {
                    Theme = !Opaque ? Windows7.Themes.Aero : Windows7.Themes.AeroOpaque;
                }
                else
                {
                    Theme = Windows7.Themes.Basic;
                }
            }


            else
            {
                ColorizationColor = @default.ColorizationColor;
                Alpha = @default.Alpha;
                Theme = @default.Theme;
            }
        }

        /// <summary>
        /// Saves WindowsVista data into registry
        /// </summary>
        /// <param name="treeView">treeView used as theme log</param>
        public void Apply(TreeView treeView = null)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Saving Windows Vista colors and appearance preferences into registry.");

            SaveToggleState(treeView);

            if (Enabled)
            {
                switch (Theme)
                {
                    case Windows7.Themes.Aero:
                        {
                            UxTheme.EnableTheming(1);
                            UxTheme.SetSystemVisualStyle($@"{SysPaths.Windows}\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0);

                            EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 2);
                            EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 1);
                            EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 0);
                            break;
                        }

                    case Windows7.Themes.AeroOpaque:
                        {
                            UxTheme.EnableTheming(1);
                            UxTheme.SetSystemVisualStyle($@"{SysPaths.Windows}\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0);

                            EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 2);
                            EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 1);
                            EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 1);
                            break;
                        }

                    case Windows7.Themes.Basic:
                        {
                            UxTheme.EnableTheming(1);
                            UxTheme.SetSystemVisualStyle($@"{SysPaths.Windows}\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0);

                            EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 1);
                            EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 0);
                            EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 0);
                            break;
                        }

                    case Windows7.Themes.Classic:
                        {
                            UxTheme.EnableTheming(0);
                            break;
                        }
                }

                EditReg(treeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", Color.FromArgb(Alpha, ColorizationColor).ToArgb(), Microsoft.Win32.RegistryValueKind.DWord);

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
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\WindowsVista", string.Empty, Enabled);
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
