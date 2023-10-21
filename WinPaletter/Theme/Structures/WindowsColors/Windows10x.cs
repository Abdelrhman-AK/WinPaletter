using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    public struct Windows10x : ICloneable
    {
        public Color Color_Index0;
        public Color Color_Index1;
        public Color Color_Index2;
        public Color Color_Index3;
        public Color Color_Index4;
        public Color Color_Index5;
        public Color Color_Index6;
        public Color Color_Index7;
        public bool WinMode_Light;
        public bool AppMode_Light;
        public bool Transparency;
        public Color Titlebar_Active;
        public Color Titlebar_Inactive;
        public Color StartMenu_Accent;
        public bool ApplyAccentOnTitlebars;
        public AccentTaskbarLevels ApplyAccentOnTaskbar;
        public bool IncreaseTBTransparency;
        public bool TB_Blur;
        //public Color Border;

        public enum AccentTaskbarLevels
        {
            None,
            Taskbar_Start_AC,
            Taskbar
        }

        public void Load(Windows10x Def)
        {
            if (OS.W10 | OS.W11 | OS.W12)
            {
                var Colors = new List<Color>();
                Colors.Clear();

                byte[] x;
                object y;

                byte[] DefColorsBytes = new[] 
                    { 
                        Def.Color_Index0.R, Def.Color_Index0.G, Def.Color_Index0.B, (byte)255, 
                        Def.Color_Index1.R, Def.Color_Index1.G, Def.Color_Index1.B, (byte)255, 
                        Def.Color_Index2.R, Def.Color_Index2.G, Def.Color_Index2.B, (byte)255,
                        Def.Color_Index3.R, Def.Color_Index3.G, Def.Color_Index3.B, (byte)255, 
                        Def.Color_Index4.R, Def.Color_Index4.G, Def.Color_Index4.B, (byte)255, 
                        Def.Color_Index5.R, Def.Color_Index5.G, Def.Color_Index5.B, (byte)255, 
                        Def.Color_Index6.R, Def.Color_Index6.G, Def.Color_Index6.B, (byte)255, 
                        Def.Color_Index7.R, Def.Color_Index7.G, Def.Color_Index7.B, (byte)255 
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

                y = GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColorMenu", Def.StartMenu_Accent.Reverse().ToArgb());
                StartMenu_Accent = Color.FromArgb(Convert.ToInt32(y)).Reverse();

                y = GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", Def.Titlebar_Active.Reverse().ToArgb());
                Titlebar_Active = Color.FromArgb(Convert.ToInt32(y)).Reverse();

                y = GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", Def.Titlebar_Active.Reverse().ToArgb());
                Titlebar_Active = Color.FromArgb(Convert.ToInt32(y)).Reverse();

                y = GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", Def.Titlebar_Inactive.Reverse().ToArgb());
                Titlebar_Inactive = Color.FromArgb(Convert.ToInt32(y)).Reverse();

                WinMode_Light = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", Def.WinMode_Light));
                AppMode_Light = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", Def.AppMode_Light));
                Transparency = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", Def.Transparency));
                IncreaseTBTransparency = Convert.ToBoolean(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "UseOLEDTaskbarTransparency", Def.IncreaseTBTransparency));

                switch (GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", Def.ApplyAccentOnTaskbar))
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

                ApplyAccentOnTitlebars = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", Def.ApplyAccentOnTitlebars));
                TB_Blur = !Convert.ToInt32(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\DWM", "ForceEffectMode", (!Def.TB_Blur).ToInteger())).ToBoolean();
            }

            else
            {
                Color_Index0 = Def.Color_Index0;
                Color_Index1 = Def.Color_Index1;
                Color_Index2 = Def.Color_Index2;
                Color_Index3 = Def.Color_Index3;
                Color_Index4 = Def.Color_Index4;
                Color_Index5 = Def.Color_Index5;
                Color_Index6 = Def.Color_Index6;
                StartMenu_Accent = Def.StartMenu_Accent;
                Titlebar_Active = Def.Titlebar_Active;
                Titlebar_Inactive = Def.Titlebar_Inactive;
                WinMode_Light = Def.WinMode_Light;
                AppMode_Light = Def.AppMode_Light;
                Transparency = Def.Transparency;
                ApplyAccentOnTaskbar = Def.ApplyAccentOnTaskbar;
                ApplyAccentOnTitlebars = Def.ApplyAccentOnTitlebars;
                IncreaseTBTransparency = Def.IncreaseTBTransparency;
            }

        }
        public void Apply(TreeView TreeView = null)
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

            //EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", Titlebar_Active.ToArgb());
            //EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", Titlebar_Active.ToArgb());

            EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", ApplyAccentOnTitlebars.ToInteger());
            EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentPalette", Colors, RegistryValueKind.Binary);
            EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColorMenu", StartMenu_Accent.Reverse().ToArgb());

            EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", Titlebar_Active.Reverse().ToArgb());
            EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", Titlebar_Active.Reverse().ToArgb());
            EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", Titlebar_Inactive.Reverse().ToArgb());

            EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", WinMode_Light.ToInteger());
            EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", AppMode_Light.ToInteger());
            EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", Transparency.ToInteger());

            if (OS.W10)
            {
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "UseOLEDTaskbarTransparency", IncreaseTBTransparency.ToInteger());
                EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\DWM", "ForceEffectMode", (!TB_Blur).ToInteger());
            }

        }

        public static bool operator ==(Windows10x First, Windows10x Second)
        {
            return First.Equals(Second);
        }

        public static bool operator !=(Windows10x First, Windows10x Second)
        {
            return !First.Equals(Second);
        }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
