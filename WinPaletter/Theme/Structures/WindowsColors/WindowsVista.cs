﻿using System;
using System.Drawing;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing Windows Vista appearance
    /// </summary>
    public struct WindowsVista : ICloneable
    {
        /// <summary> Controls if Windows Vista colors editing is enabled or not </summary> 
        public bool Enabled;

        /// <summary>Main Windows color</summary>
        public Color ColorizationColor;

        /// <summary>Control amount of main Windows color</summary>
        public byte Alpha;

        /// <summary>
        /// Theme used for Windows Vista
        /// <code>
        /// Aero
        /// AeroOpaque
        /// Basic
        /// Classic
        /// </code>
        /// </summary>
        public Windows7.Themes Theme;

        /// <summary>
        /// Loads WindowsVista data from registry
        /// </summary>
        /// <param name="default">Default WindowsVista data structure</param>
        public void Load(WindowsVista @default)
        {
            Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\WindowsVista", string.Empty, @default.Enabled));

            if (OS.WVista)
            {
                object y;

                y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", @default.ColorizationColor.ToArgb());
                ColorizationColor = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(y)));
                Alpha = Color.FromArgb(Convert.ToInt32(y)).A;

                bool Opaque = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", false));

                bool Classic = false;

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
        /// <param name="TreeView">TreeView used as theme log</param>
        public void Apply(TreeView TreeView = null)
        {
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\WindowsVista", string.Empty, Enabled);

            if (Enabled)
            {
                switch (Theme)
                {
                    case Windows7.Themes.Aero:
                        {
                            UxTheme.EnableTheming(1);
                            UxTheme.SetSystemVisualStyle($@"{PathsExt.Windows}\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0);

                            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 2);
                            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 1);
                            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 0);
                            break;
                        }

                    case Windows7.Themes.AeroOpaque:
                        {
                            UxTheme.EnableTheming(1);
                            UxTheme.SetSystemVisualStyle($@"{PathsExt.Windows}\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0);

                            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 2);
                            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 1);
                            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 1);
                            break;
                        }

                    case Windows7.Themes.Basic:
                        {
                            UxTheme.EnableTheming(1);
                            UxTheme.SetSystemVisualStyle($@"{PathsExt.Windows}\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0);

                            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 1);
                            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 0);
                            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 0);
                            break;
                        }

                    case Windows7.Themes.Classic:
                        {
                            UxTheme.EnableTheming(0);
                            break;
                        }
                }

                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", Color.FromArgb(Alpha, ColorizationColor).ToArgb(), Microsoft.Win32.RegistryValueKind.DWord);
            }
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
        public readonly object Clone()
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
