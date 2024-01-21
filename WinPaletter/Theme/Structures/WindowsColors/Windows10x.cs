using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing Windows 10/11 colors and appearance
    /// </summary>
    public struct Windows10x : ICloneable
    {
        /// <summary> Controls if Windows 10x colors editing is enabled or not </summary> 
        public bool Enabled;

        /// <summary>Color index 0 in registry value array 'AccentPalette' in 'HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent'</summary>
        public Color Color_Index0;

        /// <summary>Color index 1 in registry value array 'AccentPalette' in 'HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent'</summary>
        public Color Color_Index1;

        /// <summary>Color index 2 in registry value array 'AccentPalette' in 'HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent'</summary>
        public Color Color_Index2;

        /// <summary>Color index 3 in registry value array 'AccentPalette' in 'HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent'</summary>
        public Color Color_Index3;

        /// <summary>Color index 4 in registry value array 'AccentPalette' in 'HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent'</summary>
        public Color Color_Index4;

        /// <summary>Color index 5 in registry value array 'AccentPalette' in 'HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent'</summary>
        public Color Color_Index5;

        /// <summary>Color index 6 in registry value array 'AccentPalette' in 'HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent'</summary>
        public Color Color_Index6;

        /// <summary>Color index 7 in registry value array 'AccentPalette' in 'HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent'</summary>
        public Color Color_Index7;

        /// <summary>Light mode for Windows</summary>
        public bool WinMode_Light;

        /// <summary>Light mode for applications</summary>
        public bool AppMode_Light;

        /// <summary>Transparency effects (Mica/Acrylic)</summary>
        public bool Transparency;

        /// <summary>Active titlebar color</summary>
        public Color Titlebar_Active;

        /// <summary>Inactive titlebar color</summary>
        public Color Titlebar_Inactive;

        /// <summary>Start menu accent color. It is a <b>misnomer</b> as it may not be responsible for start menu if 'Transparency'/'WinMode_Light' are changed.</summary>
        public Color StartMenu_Accent;

        /// <summary>Make accent can be applied on titlebars</summary>
        public bool ApplyAccentOnTitlebars;

        /// <summary>Choices to apply accents on taskbar alone, taskbar with start and action center, or finally with nothing.</summary>
        public AccentTaskbarLevels ApplyAccentOnTaskbar;

        /// <summary>
        /// Increase transparency of taskbar (and removes blur)
        /// <br></br>- Targeting Windows 10 only
        /// </summary>
        public bool IncreaseTBTransparency;

        /// <summary>
        /// Make taskbar blur. If false, it will reduce blur power.
        /// <br></br>- Targeting Windows 10 only
        /// </summary>
        public bool TB_Blur;

        //public Color Border;

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
        public void Load(string Edition, Windows10x @default)
        {
            Enabled = Convert.ToBoolean(GetReg($@"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\Windows10x\{Edition}", string.Empty, @default.Enabled));

            if (OS.W12 || OS.W11 || OS.W10)
            {
                List<Color> Colors = new();
                Colors.Clear();

                byte[] x;
                object y;

                byte[] DefColorsBytes = new[]
                    {
                        @default.Color_Index0.R, @default.Color_Index0.G, @default.Color_Index0.B, (byte)255,
                        @default.Color_Index1.R, @default.Color_Index1.G, @default.Color_Index1.B, (byte)255,
                        @default.Color_Index2.R, @default.Color_Index2.G, @default.Color_Index2.B, (byte)255,
                        @default.Color_Index3.R, @default.Color_Index3.G, @default.Color_Index3.B, (byte)255,
                        @default.Color_Index4.R, @default.Color_Index4.G, @default.Color_Index4.B, (byte)255,
                        @default.Color_Index5.R, @default.Color_Index5.G, @default.Color_Index5.B, (byte)255,
                        @default.Color_Index6.R, @default.Color_Index6.G, @default.Color_Index6.B, (byte)255,
                        @default.Color_Index7.R, @default.Color_Index7.G, @default.Color_Index7.B, (byte)255
                    };

                x = (byte[])GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentPalette", DefColorsBytes);
                Colors.Add(Color.FromArgb(255, x[0], x[1], x[2]));
                Colors.Add(Color.FromArgb(255, x[4], x[5], x[6]));
                Colors.Add(Color.FromArgb(255, x[8], x[9], x[10]));
                Colors.Add(Color.FromArgb(255, x[12], x[13], x[14]));
                Colors.Add(Color.FromArgb(255, x[16], x[17], x[18]));
                Colors.Add(Color.FromArgb(255, x[20], x[21], x[22]));
                Colors.Add(Color.FromArgb(255, x[24], x[25], x[26]));
                Colors.Add(Color.FromArgb(255, x[28], x[29], x[30]));
                Color_Index0 = Colors[0];
                Color_Index1 = Colors[1];
                Color_Index2 = Colors[2];
                Color_Index3 = Colors[3];
                Color_Index4 = Colors[4];
                Color_Index5 = Colors[5];
                Color_Index6 = Colors[6];
                Color_Index7 = Colors[7];

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

            else
            {
                Color_Index0 = @default.Color_Index0;
                Color_Index1 = @default.Color_Index1;
                Color_Index2 = @default.Color_Index2;
                Color_Index3 = @default.Color_Index3;
                Color_Index4 = @default.Color_Index4;
                Color_Index5 = @default.Color_Index5;
                Color_Index6 = @default.Color_Index6;
                StartMenu_Accent = @default.StartMenu_Accent;
                Titlebar_Active = @default.Titlebar_Active;
                Titlebar_Inactive = @default.Titlebar_Inactive;
                WinMode_Light = @default.WinMode_Light;
                AppMode_Light = @default.AppMode_Light;
                Transparency = @default.Transparency;
                ApplyAccentOnTaskbar = @default.ApplyAccentOnTaskbar;
                ApplyAccentOnTitlebars = @default.ApplyAccentOnTitlebars;
                IncreaseTBTransparency = @default.IncreaseTBTransparency;
            }

        }

        /// <summary>
        /// Saves Windows10x data into registry
        /// </summary>
        /// <param name="TreeView">TreeView used as theme log</param>
        public void Apply(string Edition, TreeView TreeView = null)
        {
            EditReg(TreeView, $@"HKEY_CURRENT_USER\Software\WinPaletter\Aspects\WindowsColorsThemes\Windows10x\{Edition}", string.Empty, Enabled);

            if (Enabled)
            {
                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "AutoColorization", 0);

                byte[] Colors = new[] { Color_Index0.R, Color_Index0.G, Color_Index0.B, Color_Index0.A,
                Color_Index1.R, Color_Index1.G, Color_Index1.B, Color_Index1.A,
                Color_Index2.R, Color_Index2.G, Color_Index2.B, Color_Index2.A,
                Color_Index3.R, Color_Index3.G, Color_Index3.B, Color_Index3.A,
                Color_Index4.R, Color_Index4.G, Color_Index4.B, Color_Index4.A,
                Color_Index5.R, Color_Index5.G, Color_Index5.B, Color_Index5.A,
                Color_Index6.R, Color_Index6.G, Color_Index6.B, Color_Index6.A,
                Color_Index7.R, Color_Index7.G, Color_Index7.B, Color_Index7.A };

                switch (ApplyAccentOnTaskbar)
                {
                    case AccentTaskbarLevels.None:
                        {
                            EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", 0);
                            break;
                        }

                    case AccentTaskbarLevels.Taskbar_Start_AC:
                        {
                            EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", 1);
                            break;
                        }

                    case AccentTaskbarLevels.Taskbar:
                        {
                            EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", 2);
                            break;
                        }

                    default:
                        {
                            EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", 0);
                            break;
                        }
                }

                //EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", Background.ToArgb());
                //EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", Background.ToArgb());

                EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", ApplyAccentOnTitlebars ? 1 : 0);

                EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentPalette", Colors, RegistryValueKind.Binary);
                EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColorMenu", StartMenu_Accent.Reverse().ToArgb());

                EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", Titlebar_Active.Reverse().ToArgb());
                EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", Titlebar_Active.Reverse().ToArgb());
                EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", Titlebar_Inactive.Reverse().ToArgb());

                EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", WinMode_Light ? 1 : 0);
                EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", AppMode_Light ? 1 : 0);
                EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", Transparency ? 1 : 0);

                if (OS.W10)
                {
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "UseOLEDTaskbarTransparency", IncreaseTBTransparency ? 1 : 0);
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\DWM", "ForceEffectMode", (!TB_Blur) ? 1 : 0);
                }
            }
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
