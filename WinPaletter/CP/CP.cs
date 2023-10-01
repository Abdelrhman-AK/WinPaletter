using Devcorp.Controls.VisualStyles;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.CP.Structures;
using static WinPaletter.Metrics;
using static WinPaletter.NativeMethods.User32;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter
{

    public class CP : IDisposable, ICloneable
    {

        private bool _ErrorHappened = false;
        private readonly BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
        private readonly Converter _Converter = new Converter();

        #region IDisposable Support
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
            }
            disposedValue = true;
        }

        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        public object Clone()
        {
            return MemberwiseClone();
        }

        public enum CP_Type
        {
            Registry,
            File,
            Empty
        }

        public class Structures
        {
            public struct Info : ICloneable
            {
                public string AppVersion;
                public string ThemeName;
                public string Description;
                public bool ExportResThemePack;
                public string License;
                public string ThemeVersion;
                public string Author;
                public string AuthorSocialMediaLink;
                public Color Color1;
                public Color Color2;
                public int Pattern;
                public bool DesignedFor_Win11;
                public bool DesignedFor_Win10;
                public bool DesignedFor_Win81;
                public bool DesignedFor_Win7;
                public bool DesignedFor_WinVista;
                public bool DesignedFor_WinXP;

                public void Load()
                {
                    ThemeName = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ThemeName", My.Env.Lang.CurrentMode).ToString();
                    ThemeVersion = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ThemeVersion", "1.0").ToString();
                    Author = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "Author", Environment.UserName).ToString();
                    AuthorSocialMediaLink = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "AuthorSocialMediaLink", "").ToString();
                    AppVersion = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "AppVersion", My.Env.AppVersion).ToString();
                    Description = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "Description", "").ToString();
                    ExportResThemePack = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ExportResThemePack", false));
                    License = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "License", "").ToString();
                    var y = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Color1", Color.FromArgb(0, 102, 204).ToArgb());
                    Color1 = Color.FromArgb(Convert.ToInt32(y));

                    y = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Color2", Color.FromArgb(122, 9, 9).ToArgb());
                    Color2 = Color.FromArgb(Convert.ToInt32(y));

                    Pattern = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Pattern", 1));
                    DesignedFor_Win11 = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win11", true));
                    DesignedFor_Win10 = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win10", true));
                    DesignedFor_Win81 = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win8.1", true));
                    DesignedFor_Win7 = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win7", true));
                    DesignedFor_WinVista = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_WinVista", true));
                    DesignedFor_WinXP = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_WinXP", true));
                }
                public void Apply(TreeView TreeView = null)
                {
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ThemeName", ThemeName, RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ThemeVersion", ThemeVersion, RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "Author", Author, RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "AuthorSocialMediaLink", AuthorSocialMediaLink, RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "AppVersion", My.Env.AppVersion, RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "Description", Description, RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ExportResThemePack", ExportResThemePack, RegistryValueKind.DWord);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "License", License, RegistryValueKind.String);

                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Color1", Color1.ToArgb(), RegistryValueKind.DWord);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Color2", Color2.ToArgb(), RegistryValueKind.DWord);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Pattern", Pattern, RegistryValueKind.DWord);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win11", DesignedFor_Win11.ToInteger(), RegistryValueKind.DWord);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win10", DesignedFor_Win10.ToInteger(), RegistryValueKind.DWord);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win8.1", DesignedFor_Win81.ToInteger(), RegistryValueKind.DWord);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win7", DesignedFor_Win7.ToInteger(), RegistryValueKind.DWord);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_WinVista", DesignedFor_WinVista.ToInteger(), RegistryValueKind.DWord);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_WinXP", DesignedFor_WinXP.ToInteger(), RegistryValueKind.DWord);
                }

                public static bool operator ==(Info First, Info Second)
                {
                    return First.Equals(Second);
                }

                public static bool operator !=(Info First, Info Second)
                {
                    return !First.Equals(Second);
                }
                public object Clone()
                {
                    return MemberwiseClone();
                }

            }
            public struct AppTheme : ICloneable
            {
                public bool Enabled;
                public Color BackColor;
                public Color AccentColor;
                public bool DarkMode;
                public bool RoundCorners;

                public void Load(AppTheme _DefAppTheme)
                {
                    Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Settings", "Appearance_Custom", _DefAppTheme.Enabled));
                    BackColor = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Settings", ".BackColor", _DefAppTheme.BackColor.ToArgb())));
                    AccentColor = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Settings", "AccentColor", _DefAppTheme.AccentColor.ToArgb())));
                    DarkMode = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Settings", "Custom_Dark", _DefAppTheme.DarkMode));
                    RoundCorners = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Settings", "RoundedCorners", _DefAppTheme.RoundCorners));
                }
                public void Apply(TreeView TreeView = null)
                {
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Settings", "Appearance_Custom", Enabled);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Settings", ".BackColor", BackColor.ToArgb());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Settings", "AccentColor", AccentColor.ToArgb());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Settings", "Custom_Dark", DarkMode);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Settings", "RoundedCorners", RoundCorners);

                    {
                        ref var temp = ref My.Env.Settings.Appearance;
                        temp.CustomColors = Enabled;
                        temp.BackColor = BackColor;
                        temp.AccentColor = AccentColor;
                        temp.CustomTheme = DarkMode;
                        temp.RoundedCorners = RoundCorners;
                    }

                    WPStyle.ApplyStyle();
                }

                public static bool operator ==(AppTheme First, AppTheme Second)
                {
                    return First.Equals(Second);
                }

                public static bool operator !=(AppTheme First, AppTheme Second)
                {
                    return !First.Equals(Second);
                }
                public object Clone()
                {
                    return MemberwiseClone();
                }
            }
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

                public enum AccentTaskbarLevels
                {
                    None,
                    Taskbar_Start_AC,
                    Taskbar
                }

                public void Load(Windows10x _DefWin, byte[] DefColorsBytes)
                {
                    if (My.Env.W10 | My.Env.W11 | My.Env.W12)
                    {
                        var Colors = new List<Color>();
                        Colors.Clear();

                        byte[] x;
                        object y;

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

                        y = GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColorMenu", _DefWin.StartMenu_Accent.Reverse().ToArgb());
                        StartMenu_Accent = Color.FromArgb(Convert.ToInt32(y)).Reverse();

                        y = GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", _DefWin.Titlebar_Active.Reverse().ToArgb());
                        Titlebar_Active = Color.FromArgb(Convert.ToInt32(y)).Reverse();

                        y = GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", _DefWin.Titlebar_Active.Reverse().ToArgb());
                        Titlebar_Active = Color.FromArgb(Convert.ToInt32(y)).Reverse();

                        y = GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", _DefWin.Titlebar_Inactive.Reverse().ToArgb());
                        Titlebar_Inactive = Color.FromArgb(Convert.ToInt32(y)).Reverse();

                        WinMode_Light = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", _DefWin.WinMode_Light));
                        AppMode_Light = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", _DefWin.AppMode_Light));
                        Transparency = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", _DefWin.Transparency));
                        IncreaseTBTransparency = Convert.ToBoolean(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "UseOLEDTaskbarTransparency", _DefWin.IncreaseTBTransparency));

                        switch (GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "ColorPrevalence", _DefWin.ApplyAccentOnTaskbar))
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

                        ApplyAccentOnTitlebars = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", _DefWin.ApplyAccentOnTitlebars));
                        TB_Blur = !Convert.ToInt32(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\DWM", "ForceEffectMode", (!_DefWin.TB_Blur).ToInteger())).ToBoolean();
                    }

                    else
                    {
                        Color_Index0 = _DefWin.Color_Index0;
                        Color_Index1 = _DefWin.Color_Index1;
                        Color_Index2 = _DefWin.Color_Index2;
                        Color_Index3 = _DefWin.Color_Index3;
                        Color_Index4 = _DefWin.Color_Index4;
                        Color_Index5 = _DefWin.Color_Index5;
                        Color_Index6 = _DefWin.Color_Index6;
                        StartMenu_Accent = _DefWin.StartMenu_Accent;
                        Titlebar_Active = _DefWin.Titlebar_Active;
                        Titlebar_Inactive = _DefWin.Titlebar_Inactive;
                        WinMode_Light = _DefWin.WinMode_Light;
                        AppMode_Light = _DefWin.AppMode_Light;
                        Transparency = _DefWin.Transparency;
                        ApplyAccentOnTaskbar = _DefWin.ApplyAccentOnTaskbar;
                        ApplyAccentOnTitlebars = _DefWin.ApplyAccentOnTitlebars;
                        IncreaseTBTransparency = _DefWin.IncreaseTBTransparency;
                    }

                }
                public void Apply(TreeView TreeView = null)
                {
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "AutoColorization", 0);

                    byte[] Colors = new[] { Color_Index0.R, Color_Index0.G, Color_Index0.B, Color_Index0.A, Color_Index1.R, Color_Index1.G, Color_Index1.B, Color_Index1.A, Color_Index2.R, Color_Index2.G, Color_Index2.B, Color_Index2.A, Color_Index3.R, Color_Index3.G, Color_Index3.B, Color_Index3.A, Color_Index4.R, Color_Index4.G, Color_Index4.B, Color_Index4.A, Color_Index5.R, Color_Index5.G, Color_Index5.B, Color_Index5.A, Color_Index6.R, Color_Index6.G, Color_Index6.B, Color_Index6.A, Color_Index7.R, Color_Index7.G, Color_Index7.B, Color_Index7.A };







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

                    EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", Titlebar_Active.ToArgb());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", ApplyAccentOnTitlebars.ToInteger());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentPalette", Colors, RegistryValueKind.Binary);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColorMenu", StartMenu_Accent.Reverse().ToArgb());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", Titlebar_Active.ToArgb());


                    EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColorMenu", Titlebar_Active.Reverse().ToArgb());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", Titlebar_Active.Reverse().ToArgb());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", Titlebar_Inactive.Reverse().ToArgb());

                    EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", WinMode_Light.ToInteger());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", AppMode_Light.ToInteger());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", Transparency.ToInteger());

                    if (My.Env.W10)
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
            public struct Windows8x : ICloneable
            {
                public int Start;
                public Color ColorizationColor;
                public int ColorizationColorBalance;
                public Color StartColor;
                public Color AccentColor;
                public Windows7.Themes Theme;
                public int LogonUI;
                public Color PersonalColors_Background;
                public Color PersonalColors_Accent;
                public bool NoLockScreen;
                public LogonUI7.Modes LockScreenType;
                public int LockScreenSystemID;

                public static bool operator ==(Windows8x First, Windows8x Second)
                {
                    return First.Equals(Second);
                }

                public static bool operator !=(Windows8x First, Windows8x Second)
                {
                    return !First.Equals(Second);
                }
                public object Clone()
                {
                    return MemberwiseClone();
                }

                public void Load(Windows8x _DefWin)
                {
                    if (My.Env.W8 | My.Env.W81)
                    {
                        object y;

                        var stringThemeName = new System.Text.StringBuilder(260);
                        UxTheme.GetCurrentThemeName(stringThemeName, 260, null, 0, null, 0);

                        if (stringThemeName.ToString().Split('\\').Last().ToLower() == "aerolite.msstyles" | string.IsNullOrWhiteSpace(stringThemeName.ToString()))
                        {
                            Theme = Windows7.Themes.AeroLite;
                        }
                        else
                        {
                            Theme = Windows7.Themes.Aero;
                        }

                        y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", _DefWin.ColorizationColor.ToArgb());
                        ColorizationColor = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(y)));

                        y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", _DefWin.ColorizationColorBalance);
                        ColorizationColorBalance = Convert.ToInt32(y);

                        y = GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColor", Color.FromArgb(84, 0, 30).ToArgb());
                        StartColor = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(y))).Reverse();

                        y = GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColor", Color.FromArgb(178, 29, 72).ToArgb());
                        AccentColor = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(y))).Reverse();

                        string S;

                        S = GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Background", _DefWin.PersonalColors_Background.HEX(false, true)).ToString();
                        PersonalColors_Background = S.FromHEXToColor();

                        S = GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Accent", _DefWin.PersonalColors_Accent.HEX(false, true)).ToString();
                        PersonalColors_Accent = S.FromHEXToColor();

                        Start = Convert.ToInt32(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "ForceStartBackground", 0));
                        LogonUI = Convert.ToInt32(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultColorSet", 0));
                        LockScreenType = (LogonUI7.Modes)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Mode", LogonUI7.Modes.Default_));
                        LockScreenSystemID = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Metro_LockScreenSystemID", 0));
                        NoLockScreen = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", false));
                    }
                    else
                    {
                        Theme = _DefWin.Theme;
                        StartColor = _DefWin.StartColor;
                        AccentColor = _DefWin.AccentColor;
                        PersonalColors_Background = _DefWin.PersonalColors_Background;
                        PersonalColors_Accent = _DefWin.PersonalColors_Accent;
                        Start = _DefWin.Start;
                        LogonUI = _DefWin.LogonUI;
                        NoLockScreen = _DefWin.NoLockScreen;
                        LockScreenType = _DefWin.LockScreenType;
                        LockScreenSystemID = _DefWin.LockScreenSystemID;
                    }
                }

                public void Apply(TreeView TreeView = null)
                {
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "AutoColorization", 0);

                    try
                    {
                        switch (Theme)
                        {
                            case Windows7.Themes.Aero:
                                {
                                    UxTheme.EnableTheming(1);
                                    if (TreeView is not null)
                                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 1), "dll");

                                    UxTheme.SetSystemVisualStyle(My.Env.PATH_Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0);
                                    if (TreeView is not null)
                                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_UxTheme_SSVS, "UxTheme", "SetSystemVisualStyle", My.Env.PATH_Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0), "dll");
                                    break;
                                }

                            case Windows7.Themes.AeroLite:
                                {
                                    UxTheme.EnableTheming(1);
                                    if (TreeView is not null)
                                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 1), "dll");

                                    UxTheme.SetSystemVisualStyle(My.Env.PATH_Windows + @"\resources\Themes\Aero\AeroLite.msstyles", "NormalColor", "NormalSize", 0);
                                    if (TreeView is not null)
                                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_UxTheme_SSVS, "UxTheme", "SetSystemVisualStyle", My.Env.PATH_Windows + @"\resources\Themes\Aero\AeroLite.msstyles", "NormalColor", "NormalSize", 0), "dll");

                                    try
                                    {
                                        My.MyProject.Computer.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\HighContrast", true).DeleteSubKeyTree("Pre-High Contrast Scheme", false);
                                        if (TreeView is not null)
                                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_DeletingHighContrastThemes, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\HighContrast"), "reg_del");
                                    }
                                    catch
                                    {
                                        // Do nothing
                                        My.MyProject.Computer.Registry.CurrentUser.Close();
                                    }
                                    finally
                                    {
                                        My.MyProject.Computer.Registry.CurrentUser.Close();
                                    }

                                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes", "CurrentTheme", "", RegistryValueKind.String);
                                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes", "LastHighContrastTheme", "", RegistryValueKind.String);
                                    break;
                                }

                        }
                    }
                    catch
                    {
                    }

                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", ColorizationColor.ToArgb());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", ColorizationColorBalance);

                    EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "StartColor", StartColor.Reverse().ToArgb());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultStartColor", StartColor.Reverse().ToArgb());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "AccentColor", AccentColor.Reverse().ToArgb());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultColorSet", LogonUI);

                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "ForceStartBackground", Start);
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Accent", "DefaultColorSet", LogonUI);
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Background", "#" + PersonalColors_Background.HEX(false), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "PersonalColors_Accent", "#" + PersonalColors_Accent.HEX(false), RegistryValueKind.String);
                }
            }
            public struct Windows7 : ICloneable
            {
                public Color ColorizationColor;
                public Color ColorizationAfterglow;
                public bool EnableAeroPeek;
                public bool AlwaysHibernateThumbnails;
                public int ColorizationColorBalance;
                public int ColorizationAfterglowBalance;
                public int ColorizationBlurBalance;
                public int ColorizationGlassReflectionIntensity;
                public Themes Theme;

                public static bool operator ==(Windows7 First, Windows7 Second)
                {
                    return First.Equals(Second);
                }

                public static bool operator !=(Windows7 First, Windows7 Second)
                {
                    return !First.Equals(Second);
                }

                public enum Themes
                {
                    Aero,
                    AeroLite,
                    AeroOpaque,
                    Basic,
                    Classic
                }

                public void Load(Windows7 _DefWin)
                {
                    if (My.Env.W7 | My.Env.W8 | My.Env.W81)
                    {
                        object y;

                        y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", _DefWin.ColorizationColor.ToArgb());
                        ColorizationColor = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(y)));

                        y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", _DefWin.ColorizationColorBalance);
                        ColorizationColorBalance = Convert.ToInt32(y);

                        if (My.Env.W7)
                        {
                            y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglow", _DefWin.ColorizationAfterglow.ToArgb());
                            ColorizationAfterglow = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(y)));

                            y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglowBalance", _DefWin.ColorizationAfterglowBalance);
                            ColorizationAfterglowBalance = Convert.ToInt32(y);

                            y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationBlurBalance", _DefWin.ColorizationBlurBalance);
                            ColorizationBlurBalance = Convert.ToInt32(y);

                            y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationGlassReflectionIntensity", _DefWin.ColorizationGlassReflectionIntensity);
                            ColorizationGlassReflectionIntensity = Convert.ToInt32(y);

                            bool Com = default, Opaque;
                            Dwmapi.DwmIsCompositionEnabled(ref Com);

                            Opaque = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", false));

                            bool Classic = false;

                            try
                            {
                                var stringThemeName = new System.Text.StringBuilder(260);
                                UxTheme.GetCurrentThemeName(stringThemeName, 260, null, 0, null, 0);
                                Classic = string.IsNullOrWhiteSpace(stringThemeName.ToString()) | !File.Exists(stringThemeName.ToString());
                            }
                            catch
                            {
                                Classic = false;
                            }

                            if (Classic)
                            {
                                Theme = Themes.Classic;
                            }
                            else if (Com)
                            {
                                if (!Opaque)
                                    Theme = Themes.Aero;
                                else
                                    Theme = Themes.AeroOpaque;
                            }
                            else
                            {
                                Theme = Themes.Basic;
                            }

                        }

                        EnableAeroPeek = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "EnableAeroPeek", _DefWin.EnableAeroPeek));

                        AlwaysHibernateThumbnails = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "AlwaysHibernateThumbnails", _DefWin.AlwaysHibernateThumbnails));
                    }

                    else
                    {
                        ColorizationColor = _DefWin.ColorizationColor;
                        ColorizationColorBalance = _DefWin.ColorizationColorBalance;
                        ColorizationAfterglow = _DefWin.ColorizationAfterglow;
                        ColorizationAfterglowBalance = _DefWin.ColorizationAfterglowBalance;
                        ColorizationBlurBalance = _DefWin.ColorizationBlurBalance;
                        ColorizationGlassReflectionIntensity = _DefWin.ColorizationGlassReflectionIntensity;
                        Theme = _DefWin.Theme;
                        EnableAeroPeek = _DefWin.EnableAeroPeek;
                        AlwaysHibernateThumbnails = _DefWin.AlwaysHibernateThumbnails;
                    }
                }

                public void Apply(TreeView TreeView = null)
                {
                    switch (Theme)
                    {
                        case Themes.Aero:
                            {
                                UxTheme.EnableTheming(1);
                                if (TreeView is not null)
                                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 1), "dll");

                                UxTheme.SetSystemVisualStyle(My.Env.PATH_Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0);
                                if (TreeView is not null)
                                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_UxTheme_SSVS, "UxTheme", "SetSystemVisualStyle", My.Env.PATH_Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0), "dll");

                                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 2);
                                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 1);
                                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 0);
                                break;
                            }

                        case Themes.AeroOpaque:
                            {
                                UxTheme.EnableTheming(1);
                                if (TreeView is not null)
                                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 1), "dll");

                                UxTheme.SetSystemVisualStyle(My.Env.PATH_Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0);
                                if (TreeView is not null)
                                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_UxTheme_SSVS, "UxTheme", "SetSystemVisualStyle", My.Env.PATH_Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0), "dll");

                                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 2);
                                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 1);
                                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 1);
                                break;
                            }

                        case Themes.Basic:
                            {
                                UxTheme.EnableTheming(1);
                                if (TreeView is not null)
                                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 1), "dll");

                                UxTheme.SetSystemVisualStyle(My.Env.PATH_Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0);
                                if (TreeView is not null)
                                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_UxTheme_SSVS, "UxTheme", "SetSystemVisualStyle", My.Env.PATH_Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0), "dll");

                                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 1);
                                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 0);
                                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 0);
                                break;
                            }

                        case Themes.Classic:
                            {
                                UxTheme.EnableTheming(0);
                                if (TreeView is not null)
                                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 0), "dll");
                                break;
                            }

                    }

                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglow", ColorizationAfterglow.ToArgb());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationAfterglowBalance", ColorizationAfterglowBalance);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationBlurBalance", ColorizationBlurBalance);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationGlassReflectionIntensity", ColorizationGlassReflectionIntensity);

                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", ColorizationColor.ToArgb());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColorBalance", ColorizationColorBalance);

                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "EnableAeroPeek", EnableAeroPeek.ToInteger());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "AlwaysHibernateThumbnails", AlwaysHibernateThumbnails.ToInteger());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "EnableWindowColorization", 1);
                }

                public object Clone()
                {
                    return MemberwiseClone();
                }
            }
            public struct WindowsVista : ICloneable
            {
                public Color ColorizationColor;
                public byte Alpha;
                public Windows7.Themes Theme;

                public static bool operator ==(WindowsVista First, WindowsVista Second)
                {
                    return First.Equals(Second);
                }

                public static bool operator !=(WindowsVista First, WindowsVista Second)
                {
                    return !First.Equals(Second);
                }

                public void Load(WindowsVista _DefWin)
                {
                    if (My.Env.WVista)
                    {
                        object y;

                        y = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", _DefWin.ColorizationColor.ToArgb());
                        ColorizationColor = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(y)));
                        Alpha = Color.FromArgb(Convert.ToInt32(y)).A;

                        bool Com = default, Opaque;
                        Dwmapi.DwmIsCompositionEnabled(ref Com);

                        Opaque = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", false));

                        bool Classic = false;

                        try
                        {
                            var stringThemeName = new System.Text.StringBuilder(260);
                            UxTheme.GetCurrentThemeName(stringThemeName, 260, null, 0, null, 0);
                            Classic = string.IsNullOrWhiteSpace(stringThemeName.ToString()) | !File.Exists(stringThemeName.ToString());
                        }
                        catch
                        {
                            Classic = false;
                        }

                        if (Classic)
                        {
                            Theme = Windows7.Themes.Classic;
                        }
                        else if (Com)
                        {
                            if (!Opaque)
                                Theme = Windows7.Themes.Aero;
                            else
                                Theme = Windows7.Themes.AeroOpaque;
                        }
                        else
                        {
                            Theme = Windows7.Themes.Basic;
                        }
                    }


                    else
                    {
                        ColorizationColor = _DefWin.ColorizationColor;
                        Alpha = _DefWin.Alpha;
                        Theme = _DefWin.Theme;
                    }
                }

                public void Apply(TreeView TreeView = null)
                {
                    switch (Theme)
                    {
                        case Windows7.Themes.Aero:
                            {
                                UxTheme.EnableTheming(1);
                                if (TreeView is not null)
                                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 1), "dll");

                                UxTheme.SetSystemVisualStyle(My.Env.PATH_Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0);
                                if (TreeView is not null)
                                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_UxTheme_SSVS, "UxTheme", "SetSystemVisualStyle", My.Env.PATH_Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0), "dll");

                                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 2);
                                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 1);
                                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 0);
                                break;
                            }

                        case Windows7.Themes.AeroOpaque:
                            {
                                UxTheme.EnableTheming(1);
                                if (TreeView is not null)
                                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 1), "dll");

                                UxTheme.SetSystemVisualStyle(My.Env.PATH_Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0);
                                if (TreeView is not null)
                                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_UxTheme_SSVS, "UxTheme", "SetSystemVisualStyle", My.Env.PATH_Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0), "dll");

                                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 2);
                                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 1);
                                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 1);
                                break;
                            }

                        case Windows7.Themes.Basic:
                            {
                                UxTheme.EnableTheming(1);
                                if (TreeView is not null)
                                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 1), "dll");

                                UxTheme.SetSystemVisualStyle(My.Env.PATH_Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0);
                                if (TreeView is not null)
                                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_UxTheme_SSVS, "UxTheme", "SetSystemVisualStyle", My.Env.PATH_Windows + @"\resources\Themes\Aero\Aero.msstyles", "NormalColor", "NormalSize", 0), "dll");

                                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "CompositionPolicy", 1);
                                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "Composition", 0);
                                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationOpaqueBlend", 0);
                                break;
                            }

                        case Windows7.Themes.Classic:
                            {
                                UxTheme.EnableTheming(0);
                                if (TreeView is not null)
                                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 0), "dll");
                                break;
                            }

                    }

                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", Color.FromArgb(Alpha, ColorizationColor).ToArgb());

                }

                public object Clone()
                {
                    return MemberwiseClone();
                }
            }
            public struct WindowsXP : ICloneable
            {
                public Themes Theme;
                public string ThemeFile;
                public string ColorScheme;

                public enum Themes
                {
                    LunaBlue,
                    LunaOliveGreen,
                    LunaSilver,
                    Classic,
                    Custom
                }

                public void Load(WindowsXP _DefWin)
                {
                    if (My.Env.WXP)
                    {
                        var vsFile = new System.Text.StringBuilder(260);
                        var colorName = new System.Text.StringBuilder(260);
                        var sizeName = new System.Text.StringBuilder(260);

                        UxTheme.GetCurrentThemeName(vsFile, 260, colorName, 260, sizeName, 260);

                        if ((vsFile.ToString().ToLower() ?? "") == (My.Env.PATH_Windows.ToLower() + @"\resources\Themes\Luna\Luna.msstyles".ToLower() ?? ""))
                        {
                            if (colorName.ToString().ToLower() == "normalcolor")
                            {
                                Theme = Themes.LunaBlue;
                            }
                            else if (colorName.ToString().ToLower() == "homestead")
                            {
                                Theme = Themes.LunaOliveGreen;
                            }
                            else if (colorName.ToString().ToLower() == "metallic")
                            {
                                Theme = Themes.LunaSilver;
                            }
                            else
                            {
                                Theme = Themes.LunaBlue;
                            }

                            ThemeFile = vsFile.ToString();
                            ColorScheme = colorName.ToString();
                        }

                        else if (File.Exists(vsFile.ToString()) && Path.GetExtension(vsFile.ToString()) == ".theme" | Path.GetExtension(vsFile.ToString()) == ".msstyles")
                        {
                            Theme = Themes.Custom;
                            ThemeFile = vsFile.ToString();
                            ColorScheme = colorName.ToString();
                        }

                        else if (string.IsNullOrEmpty(vsFile.ToString()))
                        {
                            Theme = Themes.Classic;
                            ThemeFile = My.Env.PATH_Windows.ToLower() + @"\resources\Themes\Luna.theme";
                            ColorScheme = "NormalColor";
                        }

                        else
                        {
                            Theme = Themes.Custom;
                            ThemeFile = "";
                            ColorScheme = "";

                        }
                    }

                    else
                    {
                        Theme = _DefWin.Theme;
                        ThemeFile = _DefWin.ThemeFile;
                        ColorScheme = _DefWin.ColorScheme;
                    }
                }

                public void Apply(TreeView TreeView = null)
                {
                    try
                    {
                        switch (Theme)
                        {
                            case Themes.LunaBlue:
                                {
                                    UxTheme.EnableTheming(1);
                                    if (TreeView is not null)
                                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 1), "dll");

                                    UxTheme.SetSystemVisualStyle(My.Env.PATH_Windows + @"\resources\Themes\Luna\Luna.msstyles", "NormalColor", "NormalSize", 0);
                                    if (TreeView is not null)
                                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_UxTheme_SSVS, "UxTheme", "SetSystemVisualStyle", My.Env.PATH_Windows + @"\resources\Themes\Luna\Luna.msstyles", "NormalColor", "NormalSize", 0), "dll");

                                    My.Env.StartedWithClassicTheme = false;
                                    break;
                                }

                            case Themes.LunaOliveGreen:
                                {
                                    UxTheme.EnableTheming(1);
                                    if (TreeView is not null)
                                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 1), "dll");

                                    UxTheme.SetSystemVisualStyle(My.Env.PATH_Windows + @"\resources\Themes\Luna\Luna.msstyles", "HomeStead", "NormalSize", 0);
                                    if (TreeView is not null)
                                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_UxTheme_SSVS, "UxTheme", "SetSystemVisualStyle", My.Env.PATH_Windows + @"\resources\Themes\Luna\Luna.msstyles", "HomeStead", "NormalSize", 0), "dll");
                                    My.Env.StartedWithClassicTheme = false;
                                    break;
                                }

                            case Themes.LunaSilver:
                                {
                                    UxTheme.EnableTheming(1);
                                    if (TreeView is not null)
                                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 1), "dll");

                                    UxTheme.SetSystemVisualStyle(My.Env.PATH_Windows + @"\resources\Themes\Luna\Luna.msstyles", "Metallic", "NormalSize", 0);
                                    if (TreeView is not null)
                                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_UxTheme_SSVS, "UxTheme", "SetSystemVisualStyle", My.Env.PATH_Windows + @"\resources\Themes\Luna\Luna.msstyles", "Metallic", "NormalSize", 0), "dll");
                                    My.Env.StartedWithClassicTheme = false;
                                    break;
                                }

                            case Themes.Classic:
                                {
                                    UxTheme.EnableTheming(0);
                                    if (TreeView is not null)
                                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 0), "dll");
                                    My.Env.StartedWithClassicTheme = true;
                                    break;
                                }

                            case Themes.Custom:
                                {

                                    if (File.Exists(ThemeFile) && Path.GetExtension(ThemeFile) == ".theme" | Path.GetExtension(ThemeFile) == ".msstyles")
                                    {
                                        UxTheme.EnableTheming(1);
                                        if (TreeView is not null)
                                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_UxTheme_ET, "UxTheme", "EnableTheming", 1), "dll");

                                        UxTheme.SetSystemVisualStyle(ThemeFile, ColorScheme, "NormalSize", 0);
                                        My.Env.StartedWithClassicTheme = false;
                                        if (TreeView is not null)
                                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_UxTheme_SSVS, "UxTheme", "SetSystemVisualStyle", ThemeFile, ColorScheme, "NormalSize", 0), "dll");
                                    }

                                    break;
                                }

                        }

                        var vsFile = new System.Text.StringBuilder(260);
                        var colorName = new System.Text.StringBuilder(260);
                        var sizeName = new System.Text.StringBuilder(260);

                        UxTheme.GetCurrentThemeName(vsFile, 260, colorName, 260, sizeName, 260);

                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Software\Microsoft\Windows\CurrentVersion\ThemeManager", "DllName", vsFile.ToString(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Software\Microsoft\Windows\CurrentVersion\ThemeManager", "ColorName", colorName.ToString(), RegistryValueKind.String);
                    }
                    catch
                    {
                    }
                }

                public static bool operator ==(WindowsXP First, WindowsXP Second)
                {
                    return First.Equals(Second);
                }

                public static bool operator !=(WindowsXP First, WindowsXP Second)
                {
                    return !First.Equals(Second);
                }

                public object Clone()
                {
                    return MemberwiseClone();
                }
            }
            public struct Win32UI : ICloneable
            {
                public bool EnableTheming;
                public bool EnableGradient;
                public Color ActiveBorder;
                public Color ActiveTitle;
                public Color AppWorkspace;
                public Color Background;
                public Color ButtonAlternateFace;
                public Color ButtonDkShadow;
                public Color ButtonFace;
                public Color ButtonHilight;
                public Color ButtonLight;
                public Color ButtonShadow;
                public Color ButtonText;
                public Color GradientActiveTitle;
                public Color GradientInactiveTitle;
                public Color GrayText;
                public Color HilightText;
                public Color HotTrackingColor;
                public Color InactiveBorder;
                public Color InactiveTitle;
                public Color InactiveTitleText;
                public Color InfoText;
                public Color InfoWindow;
                public Color Menu;
                public Color MenuBar;
                public Color MenuText;
                public Color Scrollbar;
                public Color TitleText;
                public Color Window;
                public Color WindowFrame;
                public Color WindowText;
                public Color Hilight;
                public Color MenuHilight;
                public Color Desktop;

                public static bool operator ==(Win32UI First, Win32UI Second)
                {
                    return First.Equals(Second);
                }

                public static bool operator !=(Win32UI First, Win32UI Second)
                {
                    return !First.Equals(Second);
                }
                public enum Method
                {
                    Registry,
                    File,
                    VisualStyles
                }
                public void Load(Method Method = Method.Registry, VisualStyleMetrics vs = default)
                {
                    switch (Method)
                    {
                        case Method.Registry:
                            {

                                Fixer.SystemParametersInfo((int)SPI.Effects.GETFLATMENU, 0, ref EnableTheming, (int)SPIF.None);
                                Fixer.SystemParametersInfo((int)SPI.Titlebars.GETGRADIENTCAPTIONS, 0, ref EnableGradient, (int)SPIF.None);

                                {
                                    var temp = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "ActiveTitle", "153 180 209");
                                    if (temp.ToString().Split(' ').Count() == 3)
                                        ActiveTitle = Color.FromArgb(255, Convert.ToInt32(temp.ToString().Split(' ')[0]), Convert.ToInt32(temp.ToString().Split(' ')[1]), Convert.ToInt32(temp.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp1 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "AppWorkspace", "171 171 171");
                                    if (temp1.ToString().Split(' ').Count() == 3)
                                        AppWorkspace = Color.FromArgb(255, Convert.ToInt32(temp1.ToString().Split(' ')[0]), Convert.ToInt32(temp1.ToString().Split(' ')[1]), Convert.ToInt32(temp1.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp2 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "Background", "0 0 0");
                                    if (temp2.ToString().Split(' ').Count() == 3)
                                        Background = Color.FromArgb(255, Convert.ToInt32(temp2.ToString().Split(' ')[0]), Convert.ToInt32(temp2.ToString().Split(' ')[1]), Convert.ToInt32(temp2.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp3 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonAlternateFace", "0 0 0");
                                    if (temp3.ToString().Split(' ').Count() == 3)
                                        ButtonAlternateFace = Color.FromArgb(255, Convert.ToInt32(temp3.ToString().Split(' ')[0]), Convert.ToInt32(temp3.ToString().Split(' ')[1]), Convert.ToInt32(temp3.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp4 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonDkShadow", "105 105 105");
                                    if (temp4.ToString().Split(' ').Count() == 3)
                                        ButtonDkShadow = Color.FromArgb(255, Convert.ToInt32(temp4.ToString().Split(' ')[0]), Convert.ToInt32(temp4.ToString().Split(' ')[1]), Convert.ToInt32(temp4.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp5 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonFace", "240 240 240");
                                    if (temp5.ToString().Split(' ').Count() == 3)
                                        ButtonFace = Color.FromArgb(255, Convert.ToInt32(temp5.ToString().Split(' ')[0]), Convert.ToInt32(temp5.ToString().Split(' ')[1]), Convert.ToInt32(temp5.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp6 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonHilight", "255 255 255");
                                    if (temp6.ToString().Split(' ').Count() == 3)
                                        ButtonHilight = Color.FromArgb(255, Convert.ToInt32(temp6.ToString().Split(' ')[0]), Convert.ToInt32(temp6.ToString().Split(' ')[1]), Convert.ToInt32(temp6.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp7 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonLight", "227 227 227");
                                    if (temp7.ToString().Split(' ').Count() == 3)
                                        ButtonLight = Color.FromArgb(255, Convert.ToInt32(temp7.ToString().Split(' ')[0]), Convert.ToInt32(temp7.ToString().Split(' ')[1]), Convert.ToInt32(temp7.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp8 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonShadow", "160 160 160");
                                    if (temp8.ToString().Split(' ').Count() == 3)
                                        ButtonShadow = Color.FromArgb(255, Convert.ToInt32(temp8.ToString().Split(' ')[0]), Convert.ToInt32(temp8.ToString().Split(' ')[1]), Convert.ToInt32(temp8.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp9 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonText", "0 0 0");
                                    if (temp9.ToString().Split(' ').Count() == 3)
                                        ButtonText = Color.FromArgb(255, Convert.ToInt32(temp9.ToString().Split(' ')[0]), Convert.ToInt32(temp9.ToString().Split(' ')[1]), Convert.ToInt32(temp9.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp10 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "GradientActiveTitle", "185 209 234");
                                    if (temp10.ToString().Split(' ').Count() == 3)
                                        GradientActiveTitle = Color.FromArgb(255, Convert.ToInt32(temp10.ToString().Split(' ')[0]), Convert.ToInt32(temp10.ToString().Split(' ')[1]), Convert.ToInt32(temp10.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp11 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "GradientInactiveTitle", "215 228 242");
                                    if (temp11.ToString().Split(' ').Count() == 3)
                                        GradientInactiveTitle = Color.FromArgb(255, Convert.ToInt32(temp11.ToString().Split(' ')[0]), Convert.ToInt32(temp11.ToString().Split(' ')[1]), Convert.ToInt32(temp11.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp12 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "GrayText", "109 109 109");
                                    if (temp12.ToString().Split(' ').Count() == 3)
                                        GrayText = Color.FromArgb(255, Convert.ToInt32(temp12.ToString().Split(' ')[0]), Convert.ToInt32(temp12.ToString().Split(' ')[1]), Convert.ToInt32(temp12.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp13 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "HilightText", "255 255 255");
                                    if (temp13.ToString().Split(' ').Count() == 3)
                                        HilightText = Color.FromArgb(255, Convert.ToInt32(temp13.ToString().Split(' ')[0]), Convert.ToInt32(temp13.ToString().Split(' ')[1]), Convert.ToInt32(temp13.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp14 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "HotTrackingColor", "0 102 204");
                                    if (temp14.ToString().Split(' ').Count() == 3)
                                        HotTrackingColor = Color.FromArgb(255, Convert.ToInt32(temp14.ToString().Split(' ')[0]), Convert.ToInt32(temp14.ToString().Split(' ')[1]), Convert.ToInt32(temp14.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp15 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "ActiveBorder", "244 247 252");
                                    if (temp15.ToString().Split(' ').Count() == 3)
                                        ActiveBorder = Color.FromArgb(255, Convert.ToInt32(temp15.ToString().Split(' ')[0]), Convert.ToInt32(temp15.ToString().Split(' ')[1]), Convert.ToInt32(temp15.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp16 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "InactiveBorder", "244 247 252");
                                    if (temp16.ToString().Split(' ').Count() == 3)
                                        InactiveBorder = Color.FromArgb(255, Convert.ToInt32(temp16.ToString().Split(' ')[0]), Convert.ToInt32(temp16.ToString().Split(' ')[1]), Convert.ToInt32(temp16.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp17 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitle", "191 205 219");
                                    if (temp17.ToString().Split(' ').Count() == 3)
                                        InactiveTitle = Color.FromArgb(255, Convert.ToInt32(temp17.ToString().Split(' ')[0]), Convert.ToInt32(temp17.ToString().Split(' ')[1]), Convert.ToInt32(temp17.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp18 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitleText", "0 0 0");
                                    if (temp18.ToString().Split(' ').Count() == 3)
                                        InactiveTitleText = Color.FromArgb(255, Convert.ToInt32(temp18.ToString().Split(' ')[0]), Convert.ToInt32(temp18.ToString().Split(' ')[1]), Convert.ToInt32(temp18.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp19 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "InfoText", "0 0 0");
                                    if (temp19.ToString().Split(' ').Count() == 3)
                                        InfoText = Color.FromArgb(255, Convert.ToInt32(temp19.ToString().Split(' ')[0]), Convert.ToInt32(temp19.ToString().Split(' ')[1]), Convert.ToInt32(temp19.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp20 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "InfoWindow", "255 255 225");
                                    if (temp20.ToString().Split(' ').Count() == 3)
                                        InfoWindow = Color.FromArgb(255, Convert.ToInt32(temp20.ToString().Split(' ')[0]), Convert.ToInt32(temp20.ToString().Split(' ')[1]), Convert.ToInt32(temp20.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp21 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "Menu", "240 240 240");
                                    if (temp21.ToString().Split(' ').Count() == 3)
                                        Menu = Color.FromArgb(255, Convert.ToInt32(temp21.ToString().Split(' ')[0]), Convert.ToInt32(temp21.ToString().Split(' ')[1]), Convert.ToInt32(temp21.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp22 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "MenuBar", "240 240 240");
                                    if (temp22.ToString().Split(' ').Count() == 3)
                                        MenuBar = Color.FromArgb(255, Convert.ToInt32(temp22.ToString().Split(' ')[0]), Convert.ToInt32(temp22.ToString().Split(' ')[1]), Convert.ToInt32(temp22.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp23 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "MenuText", "0 0 0");
                                    if (temp23.ToString().Split(' ').Count() == 3)
                                        MenuText = Color.FromArgb(255, Convert.ToInt32(temp23.ToString().Split(' ')[0]), Convert.ToInt32(temp23.ToString().Split(' ')[1]), Convert.ToInt32(temp23.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp24 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "Scrollbar", "200 200 200");
                                    if (temp24.ToString().Split(' ').Count() == 3)
                                        Scrollbar = Color.FromArgb(255, Convert.ToInt32(temp24.ToString().Split(' ')[0]), Convert.ToInt32(temp24.ToString().Split(' ')[1]), Convert.ToInt32(temp24.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp25 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "TitleText", "0 0 0");
                                    if (temp25.ToString().Split(' ').Count() == 3)
                                        TitleText = Color.FromArgb(255, Convert.ToInt32(temp25.ToString().Split(' ')[0]), Convert.ToInt32(temp25.ToString().Split(' ')[1]), Convert.ToInt32(temp25.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp26 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "Window", "255 255 255");
                                    if (temp26.ToString().Split(' ').Count() == 3)
                                        Window = Color.FromArgb(255, Convert.ToInt32(temp26.ToString().Split(' ')[0]), Convert.ToInt32(temp26.ToString().Split(' ')[1]), Convert.ToInt32(temp26.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp27 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "WindowFrame", "100 100 100");
                                    if (temp27.ToString().Split(' ').Count() == 3)
                                        WindowFrame = Color.FromArgb(255, Convert.ToInt32(temp27.ToString().Split(' ')[0]), Convert.ToInt32(temp27.ToString().Split(' ')[1]), Convert.ToInt32(temp27.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp28 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "WindowText", "0 0 0");
                                    if (temp28.ToString().Split(' ').Count() == 3)
                                        WindowText = Color.FromArgb(255, Convert.ToInt32(temp28.ToString().Split(' ')[0]), Convert.ToInt32(temp28.ToString().Split(' ')[1]), Convert.ToInt32(temp28.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp29 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "Hilight", "0 120 215");
                                    if (temp29.ToString().Split(' ').Count() == 3)
                                        Hilight = Color.FromArgb(255, Convert.ToInt32(temp29.ToString().Split(' ')[0]), Convert.ToInt32(temp29.ToString().Split(' ')[1]), Convert.ToInt32(temp29.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp30 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "MenuHilight", "0 120 215");
                                    if (temp30.ToString().Split(' ').Count() == 3)
                                        MenuHilight = Color.FromArgb(255, Convert.ToInt32(temp30.ToString().Split(' ')[0]), Convert.ToInt32(temp30.ToString().Split(' ')[1]), Convert.ToInt32(temp30.ToString().Split(' ')[2]));
                                }

                                {
                                    var temp31 = GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "Desktop", "0 0 0");
                                    if (temp31.ToString().Split(' ').Count() == 3)
                                        Desktop = Color.FromArgb(255, Convert.ToInt32(temp31.ToString().Split(' ')[0]), Convert.ToInt32(temp31.ToString().Split(' ')[1]), Convert.ToInt32(temp31.ToString().Split(' ')[2]));
                                }

                                break;
                            }

                        case Method.VisualStyles:
                            {
                                EnableTheming = vs.FlatMenus;
                                // ActiveBorder = ActiveBorder
                                ActiveTitle = vs.Colors.ActiveCaption;
                                // AppWorkspace = AppWorkspace
                                Background = vs.Colors.Background;
                                // ButtonAlternateFace = btnaltface_pick.BackColor
                                ButtonDkShadow = vs.Colors.DkShadow3d;
                                ButtonFace = vs.Colors.Btnface;
                                ButtonHilight = vs.Colors.BtnHighlight;
                                ButtonLight = vs.Colors.Light3d;
                                ButtonShadow = vs.Colors.BtnShadow;
                                // ButtonText = vs.Colors.MenuText
                                GradientActiveTitle = vs.Colors.GradientActiveCaption;
                                GradientInactiveTitle = vs.Colors.GradientInactiveCaption;
                                GrayText = vs.Colors.GrayText;
                                HilightText = vs.Colors.HighlightText;
                                HotTrackingColor = vs.Colors.HotTracking;
                                // InactiveBorder = InactiveBorder
                                InactiveTitle = vs.Colors.InactiveCaption;
                                InactiveTitleText = vs.Colors.InactiveCaptionText;
                                // InfoText = InfoText
                                // InfoWindow = InfoWindow
                                Menu = vs.Colors.Menu;
                                MenuBar = vs.Colors.MenuBar;
                                MenuText = vs.Colors.MenuText;
                                // Scrollbar = Scrollbar
                                TitleText = vs.Colors.CaptionText;
                                Window = vs.Colors.Window;
                                // WindowFrame = Frame
                                WindowText = vs.Colors.WindowText;
                                Hilight = vs.Colors.Highlight;
                                MenuHilight = vs.Colors.MenuHilight;
                                Desktop = vs.Colors.Background;
                                break;
                            }

                    }
                }

                // Never change their orders
                public enum ColorsNumbers
                {
                    Scrollbar,
                    Background,
                    ActiveTitle,
                    InactiveTitle,
                    Menu,
                    Window,
                    WindowFrame,
                    MenuText,
                    WindowText,
                    TitleText,
                    ActiveBorder,
                    InactiveBorder,
                    AppWorkspace,
                    Hilight,
                    HilightText,
                    ButtonFace,
                    ButtonShadow,
                    GrayText,
                    ButtonText,
                    InactiveTitleText,
                    ButtonHilight,
                    ButtonDkShadow,
                    ButtonLight,
                    InfoText,
                    InfoWindow,
                    ButtonAlternateFace,
                    HotTrackingColor,
                    GradientActiveTitle,
                    GradientInactiveTitle,
                    MenuHilight,
                    MenuBar
                }

                public void Apply(TreeView TreeView = null)
                {
                    var vsFile = new System.Text.StringBuilder(260);
                    var colorName = new System.Text.StringBuilder(260);
                    var sizeName = new System.Text.StringBuilder(260);
                    UxTheme.GetCurrentThemeName(vsFile, 260, colorName, 260, sizeName, 260);
                    bool isClassic = string.IsNullOrEmpty(vsFile.ToString());

                    // Hiding forms is added as there is a bug occurs when a classic theme applied on classic Windows mode
                    var fl = new List<Form>();
                    fl.Clear();
                    if (isClassic)
                    {
                        foreach (Form f in My.MyProject.Application.OpenForms)
                        {
                            if (f.Visible)
                            {
                                f.SuspendLayout();
                                f.Visible = false;
                                fl.Add(f);
                            }
                        }
                    }

                    var C1 = new List<int>();
                    var C2 = new List<uint>();

                    C1.Clear();
                    C2.Clear();

                    C1.Add((int)ColorsNumbers.Hilight);
                    C2.Add((uint)ColorTranslator.ToWin32(Hilight));

                    C1.Add((int)ColorsNumbers.HilightText);
                    C2.Add((uint)ColorTranslator.ToWin32(HilightText));

                    C1.Add((int)ColorsNumbers.TitleText);
                    C2.Add((uint)ColorTranslator.ToWin32(TitleText));

                    C1.Add((int)ColorsNumbers.GrayText);
                    C2.Add((uint)ColorTranslator.ToWin32(GrayText));

                    C1.Add((int)ColorsNumbers.InactiveBorder);
                    C2.Add((uint)ColorTranslator.ToWin32(InactiveBorder));

                    C1.Add((int)ColorsNumbers.InactiveTitle);
                    C2.Add((uint)ColorTranslator.ToWin32(InactiveTitle));

                    C1.Add((int)ColorsNumbers.ActiveTitle);
                    C2.Add((uint)ColorTranslator.ToWin32(ActiveTitle));

                    C1.Add((int)ColorsNumbers.ActiveBorder);
                    C2.Add((uint)ColorTranslator.ToWin32(ActiveBorder));

                    C1.Add((int)ColorsNumbers.AppWorkspace);
                    C2.Add((uint)ColorTranslator.ToWin32(AppWorkspace));

                    C1.Add((int)ColorsNumbers.Background);
                    C2.Add((uint)ColorTranslator.ToWin32(Background));

                    C1.Add((int)ColorsNumbers.GradientActiveTitle);
                    C2.Add((uint)ColorTranslator.ToWin32(GradientActiveTitle));

                    C1.Add((int)ColorsNumbers.GradientInactiveTitle);
                    C2.Add((uint)ColorTranslator.ToWin32(GradientInactiveTitle));

                    C1.Add((int)ColorsNumbers.InactiveTitleText);
                    C2.Add((uint)ColorTranslator.ToWin32(InactiveTitleText));

                    C1.Add((int)ColorsNumbers.InfoWindow);
                    C2.Add((uint)ColorTranslator.ToWin32(InfoWindow));

                    C1.Add((int)ColorsNumbers.InfoText);
                    C2.Add((uint)ColorTranslator.ToWin32(InfoText));

                    C1.Add((int)ColorsNumbers.Menu);
                    C2.Add((uint)ColorTranslator.ToWin32(Menu));

                    C1.Add((int)ColorsNumbers.MenuText);
                    C2.Add((uint)ColorTranslator.ToWin32(MenuText));

                    C1.Add((int)ColorsNumbers.Scrollbar);
                    C2.Add((uint)ColorTranslator.ToWin32(Scrollbar));

                    C1.Add((int)ColorsNumbers.Window);
                    C2.Add((uint)ColorTranslator.ToWin32(Window));

                    C1.Add((int)ColorsNumbers.WindowFrame);
                    C2.Add((uint)ColorTranslator.ToWin32(WindowFrame));

                    C1.Add((int)ColorsNumbers.WindowText);
                    C2.Add((uint)ColorTranslator.ToWin32(WindowText));

                    C1.Add((int)ColorsNumbers.HotTrackingColor);
                    C2.Add((uint)ColorTranslator.ToWin32(HotTrackingColor));

                    C1.Add((int)ColorsNumbers.MenuHilight);
                    C2.Add((uint)ColorTranslator.ToWin32(MenuHilight));

                    C1.Add((int)ColorsNumbers.MenuBar);
                    C2.Add((uint)ColorTranslator.ToWin32(MenuBar));

                    C1.Add((int)ColorsNumbers.ButtonFace);
                    C2.Add((uint)ColorTranslator.ToWin32(ButtonFace));

                    C1.Add((int)ColorsNumbers.ButtonHilight);
                    C2.Add((uint)ColorTranslator.ToWin32(ButtonHilight));

                    C1.Add((int)ColorsNumbers.ButtonShadow);
                    C2.Add((uint)ColorTranslator.ToWin32(ButtonShadow));

                    C1.Add((int)ColorsNumbers.ButtonText);
                    C2.Add((uint)ColorTranslator.ToWin32(ButtonText));

                    C1.Add((int)ColorsNumbers.ButtonDkShadow);
                    C2.Add((uint)ColorTranslator.ToWin32(ButtonDkShadow));

                    C1.Add((int)ColorsNumbers.ButtonAlternateFace);
                    C2.Add((uint)ColorTranslator.ToWin32(ButtonAlternateFace));

                    C1.Add((int)ColorsNumbers.ButtonLight);
                    C2.Add((uint)ColorTranslator.ToWin32(ButtonLight));

                    SetSysColors(C1.Count, C1.ToArray(), C2.ToArray());

                    SystemParametersInfo((int)SPI.Effects.SETFLATMENU, 0, EnableTheming, (int)SPIF.UpdateINIFile);
                    SystemParametersInfo((int)SPI.Titlebars.SETGRADIENTCAPTIONS, 0, EnableGradient, (int)SPIF.UpdateINIFile);

                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ActiveBorder", ActiveBorder.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ActiveTitle", ActiveTitle.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "AppWorkspace", AppWorkspace.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "Background", Background.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonAlternateFace", ButtonAlternateFace.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonDkShadow", ButtonDkShadow.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonFace", ButtonFace.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonHilight", ButtonHilight.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonLight", ButtonLight.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonShadow", ButtonShadow.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "ButtonText", ButtonText.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "GradientActiveTitle", GradientActiveTitle.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "GradientInactiveTitle", GradientInactiveTitle.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "GrayText", GrayText.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "HilightText", HilightText.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "HotTrackingColor", HotTrackingColor.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "InactiveBorder", InactiveBorder.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitle", InactiveTitle.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "InactiveTitleText", InactiveTitleText.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "InfoText", InfoText.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "InfoWindow", InfoWindow.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "Menu", Menu.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "MenuBar", MenuBar.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "MenuText", MenuText.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "Scrollbar", Scrollbar.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "TitleText", TitleText.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "Window", Window.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "WindowFrame", WindowFrame.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "WindowText", WindowText.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "Hilight", Hilight.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "MenuHilight", MenuHilight.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Colors", "Desktop", Desktop.ToWin32Reg(), RegistryValueKind.String);

                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ActiveBorder", ActiveBorder.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ActiveTitle", ActiveTitle.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "AppWorkspace", AppWorkspace.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Background", Background.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonAlternateFace", ButtonAlternateFace.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonDkShadow", ButtonDkShadow.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonFace", ButtonFace.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonHilight", ButtonHilight.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonLight", ButtonLight.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonShadow", ButtonShadow.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "ButtonText", ButtonText.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "GradientActiveTitle", GradientActiveTitle.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "GradientInactiveTitle", GradientInactiveTitle.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "GrayText", GrayText.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "HilightText", HilightText.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "HotTrackingColor", HotTrackingColor.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "InactiveBorder", InactiveBorder.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "InactiveTitle", InactiveTitle.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "InactiveTitleText", InactiveTitleText.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "InfoText", InfoText.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "InfoWindow", InfoWindow.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Menu", Menu.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "MenuBar", MenuBar.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "MenuText", MenuText.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Scrollbar", Scrollbar.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "TitleText", TitleText.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Window", Window.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "WindowFrame", WindowFrame.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "WindowText", WindowText.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Hilight", Hilight.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "MenuHilight", MenuHilight.ToWin32Reg(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\Colors", "Desktop", Desktop.ToWin32Reg(), RegistryValueKind.String);

                    if (isClassic)
                    {
                        if (fl.Count > 0)
                        {
                            System.Threading.Thread.Sleep(100);
                            for (int i = 0, loopTo = fl.Count - 1; i <= loopTo; i++)
                            {
                                fl[i].Visible = true;
                                fl[i].ResumeLayout();
                                fl[i].Refresh();
                            }
                        }
                    }

                    if (My.Env.Settings.ThemeApplyingBehavior.ClassicColors_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                    {
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ActiveBorder", ActiveBorder.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ActiveTitle", ActiveTitle.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "AppWorkspace", AppWorkspace.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "Background", Background.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonAlternateFace", ButtonAlternateFace.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonDkShadow", ButtonDkShadow.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonFace", ButtonFace.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonHilight", ButtonHilight.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonLight", ButtonLight.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonShadow", ButtonShadow.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "ButtonText", ButtonText.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "GradientActiveTitle", GradientActiveTitle.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "GradientInactiveTitle", GradientInactiveTitle.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "GrayText", GrayText.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "HilightText", HilightText.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "HotTrackingColor", HotTrackingColor.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "InactiveBorder", InactiveBorder.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "InactiveTitle", InactiveTitle.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "InactiveTitleText", InactiveTitleText.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "InfoText", InfoText.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "InfoWindow", InfoWindow.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "Menu", Menu.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "MenuBar", MenuBar.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "MenuText", MenuText.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "Scrollbar", Scrollbar.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "TitleText", TitleText.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "Window", Window.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "WindowFrame", WindowFrame.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "WindowText", WindowText.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "Hilight", Hilight.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "MenuHilight", MenuHilight.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Colors", "Desktop", Desktop.ToWin32Reg(), RegistryValueKind.String);
                    }

                    if (My.Env.Settings.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                    {
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ActiveTitle", Color.FromArgb(0, ActiveTitle).Reverse(true).ToArgb(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ButtonFace", Color.FromArgb(0, ButtonFace).Reverse(true).ToArgb(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ButtonText", Color.FromArgb(0, ButtonText).Reverse(true).ToArgb(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "GrayText", Color.FromArgb(0, GrayText).Reverse(true).ToArgb(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "Hilight", Color.FromArgb(0, Hilight).Reverse(true).ToArgb(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "HilightText", Color.FromArgb(0, HilightText).Reverse(true).ToArgb(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "HotTrackingColor", Color.FromArgb(0, HotTrackingColor).Reverse(true).ToArgb(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "InactiveTitle", Color.FromArgb(0, InactiveTitle).Reverse(true).ToArgb(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "InactiveTitleText", Color.FromArgb(0, InactiveTitleText).Reverse(true).ToArgb(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "MenuHilight", Color.FromArgb(0, MenuHilight).Reverse(true).ToArgb(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "TitleText", Color.FromArgb(0, TitleText).Reverse(true).ToArgb(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "Window", Color.FromArgb(0, Window).Reverse(true).ToArgb(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "WindowText", Color.FromArgb(0, WindowText).Reverse(true).ToArgb(), RegistryValueKind.String);
                    }

                    else if (My.Env.Settings.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults)
                    {
                        Win32UI _DefWin32;
                        if (My.Env.PreviewStyle == WindowStyle.W11)
                        {
                            _DefWin32 = new CP_Defaults().Default_Windows11().Win32;
                        }
                        else if (My.Env.PreviewStyle == WindowStyle.W10)
                        {
                            _DefWin32 = new CP_Defaults().Default_Windows10().Win32;
                        }
                        else if (My.Env.PreviewStyle == WindowStyle.W81)
                        {
                            _DefWin32 = new CP_Defaults().Default_Windows81().Win32;
                        }
                        else if (My.Env.PreviewStyle == WindowStyle.W7)
                        {
                            _DefWin32 = new CP_Defaults().Default_Windows7().Win32;
                        }
                        else if (My.Env.PreviewStyle == WindowStyle.WVista)
                        {
                            _DefWin32 = new CP_Defaults().Default_WindowsVista().Win32;
                        }
                        else if (My.Env.PreviewStyle == WindowStyle.WXP)
                        {
                            _DefWin32 = new CP_Defaults().Default_WindowsXP().Win32;
                        }
                        else
                        {
                            _DefWin32 = new CP_Defaults().Default_Windows11().Win32;
                        }

                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ActiveTitle", Color.FromArgb(0, _DefWin32.ActiveTitle).Reverse(true).ToArgb(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ButtonFace", Color.FromArgb(0, _DefWin32.ButtonFace).Reverse(true).ToArgb(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "ButtonText", Color.FromArgb(0, _DefWin32.ButtonText).Reverse(true).ToArgb(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "GrayText", Color.FromArgb(0, _DefWin32.GrayText).Reverse(true).ToArgb(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "Hilight", Color.FromArgb(0, _DefWin32.Hilight).Reverse(true).ToArgb(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "HilightText", Color.FromArgb(0, _DefWin32.HilightText).Reverse(true).ToArgb(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "HotTrackingColor", Color.FromArgb(0, _DefWin32.HotTrackingColor).Reverse(true).ToArgb(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "InactiveTitle", Color.FromArgb(0, _DefWin32.InactiveTitle).Reverse(true).ToArgb(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "InactiveTitleText", Color.FromArgb(0, _DefWin32.InactiveTitleText).Reverse(true).ToArgb(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "MenuHilight", Color.FromArgb(0, _DefWin32.MenuHilight).Reverse(true).ToArgb(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "TitleText", Color.FromArgb(0, _DefWin32.TitleText).Reverse(true).ToArgb(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "Window", Color.FromArgb(0, _DefWin32.Window).Reverse(true).ToArgb(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard", "WindowText", Color.FromArgb(0, _DefWin32.WindowText).Reverse(true).ToArgb(), RegistryValueKind.String);
                    }

                    else if (My.Env.Settings.ThemeApplyingBehavior.ClassicColors_HKLM_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Erase)
                    {
                        DelReg_AdministratorDeflector(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors", "Standard");
                    }


                }

                public void Update_UPM_DEFAULT(TreeView TreeView = null)
                {
                    if (My.Env.Settings.ThemeApplyingBehavior.UPM_HKU_DEFAULT)
                    {
                        byte[] source = (byte[])GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "UserPreferencesMask", null);
                        if (source is not null)
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "UserPreferencesMask", source, RegistryValueKind.Binary);
                    }
                }

                public object Clone()
                {
                    return MemberwiseClone();
                }
            }
            public struct LogonUI10x : ICloneable
            {
                public bool DisableAcrylicBackgroundOnLogon;
                public bool DisableLogonBackgroundImage;
                public bool NoLockScreen;

                public static bool operator ==(LogonUI10x First, LogonUI10x Second)
                {
                    return First.Equals(Second);
                }

                public static bool operator !=(LogonUI10x First, LogonUI10x Second)
                {
                    return !First.Equals(Second);
                }

                public object Clone()
                {
                    return MemberwiseClone();
                }

                public void Load(LogonUI10x _DefLogonUI)
                {
                    if (My.Env.W10 | My.Env.W11)
                    {
                        var Def = My.Env.W11 ? new CP_Defaults().Default_Windows11() : new CP_Defaults().Default_Windows10();

                        DisableAcrylicBackgroundOnLogon = Convert.ToBoolean(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableAcrylicBackgroundOnLogon", _DefLogonUI.DisableAcrylicBackgroundOnLogon));
                        DisableLogonBackgroundImage = Convert.ToBoolean(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableLogonBackgroundImage", _DefLogonUI.DisableLogonBackgroundImage));
                        NoLockScreen = Convert.ToBoolean(GetReg(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", _DefLogonUI.NoLockScreen));
                    }

                    else
                    {
                        DisableAcrylicBackgroundOnLogon = _DefLogonUI.DisableAcrylicBackgroundOnLogon;
                        DisableLogonBackgroundImage = _DefLogonUI.DisableLogonBackgroundImage;
                        NoLockScreen = _DefLogonUI.NoLockScreen;
                    }
                }

                public void Apply(TreeView TreeView = null)
                {
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableAcrylicBackgroundOnLogon", DisableAcrylicBackgroundOnLogon.ToInteger());
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableLogonBackgroundImage", DisableLogonBackgroundImage.ToInteger());
                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", NoLockScreen.ToInteger());
                }
            }
            public struct LogonUI7 : ICloneable
            {
                public bool Enabled;
                public Modes Mode;
                public string ImagePath;
                public Color Color;
                public bool Blur;
                public int Blur_Intensity;
                public bool Grayscale;
                public bool Noise;
                public BitmapExtensions.NoiseMode Noise_Mode;
                public int Noise_Intensity;

                public static bool operator ==(LogonUI7 First, LogonUI7 Second)
                {
                    return First.Equals(Second);
                }

                public static bool operator !=(LogonUI7 First, LogonUI7 Second)
                {
                    return !First.Equals(Second);
                }
                public object Clone()
                {
                    return MemberwiseClone();
                }
                public enum Modes
                {
                    Default_,
                    Wallpaper,
                    CustomImage,
                    SolidColor
                }

                public void Load(LogonUI7 _DefLogonUI)
                {
                    if (My.Env.W7 | My.Env.W8 | My.Env.W81)
                    {

                        ImagePath = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "ImagePath", "").ToString();
                        Color = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Color", Color.Black.ToArgb())));
                        Blur = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Blur", false));
                        Blur_Intensity = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Blur_Intensity", 0));
                        Grayscale = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Grayscale", false));
                        Noise = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Noise", false));
                        Noise_Mode = (BitmapExtensions.NoiseMode)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Noise_Mode", BitmapExtensions.NoiseMode.Acrylic));
                        Noise_Intensity = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Noise_Intensity", 0));

                        if (My.Env.W7)
                        {
                            bool b1 = Convert.ToBoolean(GetReg(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background", "OEMBackground", false));
                            bool b2 = Convert.ToBoolean(GetReg(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\System", "UseOEMBackground", false));
                            Enabled = b1 | b2;
                            Mode = (Modes)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Mode", Modes.Default_));
                        }
                    }

                    else
                    {
                        Enabled = _DefLogonUI.Enabled;
                        Mode = _DefLogonUI.Mode;
                        ImagePath = _DefLogonUI.ImagePath;
                        Color = _DefLogonUI.Color;
                        Blur = _DefLogonUI.Blur;
                        Blur_Intensity = _DefLogonUI.Blur_Intensity;
                        Grayscale = _DefLogonUI.Grayscale;
                        Noise = _DefLogonUI.Noise;
                        Noise_Mode = _DefLogonUI.Noise_Mode;
                        Noise_Intensity = _DefLogonUI.Noise_Intensity;
                    }
                }
            }
            public struct LogonUIXP : ICloneable
            {
                public bool Enabled;
                public Modes Mode;
                public Color BackColor;
                public bool ShowMoreOptions;

                public enum Modes
                {
                    Win2000,
                    Default
                }

                public void Load(LogonUIXP _DefLogonUI)
                {
                    if (My.Env.WXP)
                    {

                        Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\WinXP", "", _DefLogonUI.Enabled));

                        switch (GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "LogonType", _DefLogonUI.Mode))
                        {
                            case 1:
                                {
                                    Mode = Modes.Default;
                                    break;
                                }

                            default:
                                {
                                    Mode = Modes.Win2000;
                                    break;
                                }
                        }

                        {
                            var temp = GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "Background", "0 0 0");
                            if (temp.ToString().Split(' ').Count() == 3)
                            {
                                BackColor = Color.FromArgb(255, Convert.ToInt32(temp.ToString().Split(' ')[0]), Convert.ToInt32(temp.ToString().Split(' ')[1]), Convert.ToInt32(temp.ToString().Split(' ')[2]));
                            }
                            else
                            {
                                BackColor = _DefLogonUI.BackColor;
                            }
                        }

                        ShowMoreOptions = Convert.ToBoolean(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "ShowLogonOptions", _DefLogonUI.ShowMoreOptions));
                    }

                    else
                    {
                        Mode = _DefLogonUI.Mode;
                        BackColor = _DefLogonUI.BackColor;
                        ShowMoreOptions = _DefLogonUI.ShowMoreOptions;
                    }
                }

                public void Apply(TreeView TreeView = null)
                {
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI\WinXP", "", Enabled);

                    if (Enabled & My.Env.WXP)
                    {
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "LogonType", Mode == Modes.Default ? 1 : 0, RegistryValueKind.DWord);
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "Background", BackColor.ToWin32Reg(), RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", "ShowLogonOptions", ShowMoreOptions.ToInteger(), RegistryValueKind.DWord);
                    }
                }

                public static bool operator ==(LogonUIXP First, LogonUIXP Second)
                {
                    return First.Equals(Second);
                }

                public static bool operator !=(LogonUIXP First, LogonUIXP Second)
                {
                    return !First.Equals(Second);
                }
                public object Clone()
                {
                    return MemberwiseClone();
                }
            }
            public struct Wallpaper : ICloneable
            {
                public bool Enabled;
                public bool SlideShow_Folder_or_ImagesList;

                public string ImageFile;
                public WallpaperStyles WallpaperStyle;
                public WallpaperTypes WallpaperType;

                public string Wallpaper_Slideshow_ImagesRootPath;
                public string[] Wallpaper_Slideshow_Images;
                public int Wallpaper_Slideshow_Interval;
                public bool Wallpaper_Slideshow_Shuffle;

                public enum WallpaperStyles : int
                {
                    Centered = 0,
                    Tile = 1,
                    Stretched = 2,
                    Fit = 6,
                    Fill = 10
                }
                public enum WallpaperTypes
                {
                    Picture,
                    SolidColor,
                    SlideShow
                }

                public void Load(Wallpaper _DefWallpaper)
                {
                    Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "", _DefWallpaper.Enabled));
                    SlideShow_Folder_or_ImagesList = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "SlideShow_Folder_or_ImagesList", _DefWallpaper.SlideShow_Folder_or_ImagesList));
                    Wallpaper_Slideshow_ImagesRootPath = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "Wallpaper_Slideshow_ImagesRootPath", _DefWallpaper.Wallpaper_Slideshow_ImagesRootPath).ToString();
                    Wallpaper_Slideshow_Images = (string[])GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "Wallpaper_Slideshow_Images", _DefWallpaper.Wallpaper_Slideshow_Images);

                    ImageFile = GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", _DefWallpaper.ImageFile).ToString();

                    string slideshow_img = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Themes\TranscodedWallpaper";
                    string spotlight_img = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\LocalState\Assets";

                    // Necessary to remember last wallpaper that is not from slideshow and not a spotlight image
                    if (ImageFile.StartsWith(slideshow_img, My.Env._ignore) || ImageFile.StartsWith(spotlight_img, My.Env._ignore) || !File.Exists(ImageFile))
                    {
                        ImageFile = GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "CurrentWallpaperPath", _DefWallpaper.ImageFile).ToString();
                    }

                    if (GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "TileWallpaper", "0").ToString() == "1")
                    {
                        WallpaperStyle = WallpaperStyles.Tile;
                    }
                    else
                    {
                        WallpaperStyle = (WallpaperStyles)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "WallpaperStyle", _DefWallpaper.WallpaperStyle));
                    }

                    WallpaperType = (WallpaperTypes)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "BackgroundType", _DefWallpaper.WallpaperType));

                    Wallpaper_Slideshow_Interval = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Personalization\Desktop Slideshow", "Interval", _DefWallpaper.Wallpaper_Slideshow_Interval));
                    Wallpaper_Slideshow_Shuffle = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Control Panel\Personalization\Desktop Slideshow", "Shuffle", _DefWallpaper.Wallpaper_Slideshow_Shuffle));

                }

                public void Apply(bool SkipSettingWallpaper = false, TreeView TreeView = null)
                {
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "", Enabled);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "SlideShow_Folder_or_ImagesList", SlideShow_Folder_or_ImagesList);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "Wallpaper_Slideshow_ImagesRootPath", Wallpaper_Slideshow_ImagesRootPath, RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Wallpaper", "Wallpaper_Slideshow_Images", Wallpaper_Slideshow_Images, RegistryValueKind.MultiString);

                    if (Enabled)
                    {
                        string slideshow_ini = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Themes\slideshow.ini";
                        string slideshow_img = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Themes\TranscodedWallpaper";

                        if (File.Exists(slideshow_ini))
                        {
                            File.SetAttributes(slideshow_ini, FileAttributes.Normal);
                            File.WriteAllText(slideshow_ini, "");
                            File.SetAttributes(slideshow_ini, FileAttributes.Hidden);
                        }

                        // Setting WallpaperStyle must be before setting wallpaper itself
                        if (WallpaperStyle == WallpaperStyles.Tile)
                        {
                            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "TileWallpaper", "1", RegistryValueKind.String);
                        }
                        else
                        {
                            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "TileWallpaper", "0", RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "WallpaperStyle", (int)WallpaperStyle, RegistryValueKind.String);
                        }

                        if (!SkipSettingWallpaper)
                        {

                            if (WallpaperType == WallpaperTypes.SolidColor)
                            {
                                if (TreeView is not null)
                                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Desktop.SETDESKWALLPAPER.ToString(), 0, "", SPIF.UpdateINIFile.ToString()), "dll");
                                User32.SystemParametersInfo((int)SPI.Desktop.SETDESKWALLPAPER, 0, "", (int)SPIF.UpdateINIFile);
                                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", "", RegistryValueKind.String);
                            }

                            else if (WallpaperType == WallpaperTypes.Picture)
                            {
                                if (My.Env.WXP | My.Env.WVista | My.Env.W7 && File.Exists(ImageFile) && !new FileInfo(ImageFile).FullName.StartsWith(My.Env.PATH_Windows + @"\Web", My.Env._ignore))
                                {
                                    using (var bmp = new Bitmap(Bitmap_Mgr.Load(ImageFile)))
                                    {
                                        if (bmp.RawFormat != System.Drawing.Imaging.ImageFormat.Bmp)
                                        {
                                            if (MsgBox(My.Env.Lang.CP_Wallpaper_NonBMP0,  MessageBoxButtons.YesNo, MessageBoxIcon.Question, My.Env.Lang.CP_Wallpaper_NonBMP1) == DialogResult.Yes)
                                            {
                                                bmp.Save(ImageFile, System.Drawing.Imaging.ImageFormat.Bmp);
                                            }
                                        }
                                    }
                                }

                                if (TreeView is not null)
                                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Desktop.SETDESKWALLPAPER.ToString(), 0, ImageFile, SPIF.UpdateINIFile.ToString()), "dll");
                                User32.SystemParametersInfo((int)SPI.Desktop.SETDESKWALLPAPER, 0, ImageFile, (int)SPIF.UpdateINIFile);
                                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", ImageFile, RegistryValueKind.String);

                                // Necessary to make both WinPaletter and Windows remember last wallpaper that is not from slideshow and not a spotlight image
                                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "CurrentWallpaperPath", ImageFile, RegistryValueKind.String);
                            }

                            else if (WallpaperType == WallpaperTypes.SlideShow)
                            {
                                if (TreeView is not null)
                                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Desktop.SETDESKWALLPAPER.ToString(), 0, slideshow_img, SPIF.UpdateINIFile.ToString()), "dll");
                                User32.SystemParametersInfo((int)SPI.Desktop.SETDESKWALLPAPER, 0, slideshow_img, (int)SPIF.UpdateINIFile);
                                EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", slideshow_img, RegistryValueKind.String);

                            }
                        }

                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "BackgroundType", WallpaperType);

                        if (!My.Env.WXP && !My.Env.WVista)
                        {

                            if (!SkipSettingWallpaper)
                            {
                                using (var _ini = new INI(slideshow_ini))
                                {

                                    if (TreeView is not null)
                                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_SettingSlideshow, slideshow_ini), "dll");

                                    if (WallpaperType == WallpaperTypes.SlideShow && SlideShow_Folder_or_ImagesList && Directory.Exists(Wallpaper_Slideshow_ImagesRootPath))
                                    {
                                        _ini.IniWriteValue("Slideshow", "ImagesRootPath", Wallpaper_Slideshow_ImagesRootPath);
                                    }

                                    _ini.IniWriteValue("Slideshow", "Interval", Wallpaper_Slideshow_Interval.ToString());
                                    _ini.IniWriteValue("Slideshow", "Shuffle", Wallpaper_Slideshow_Shuffle.ToString());

                                    if (WallpaperType == WallpaperTypes.SlideShow && !SlideShow_Folder_or_ImagesList)
                                    {
                                        if (Directory.Exists(Wallpaper_Slideshow_Images[0]))
                                        {
                                            _ini.IniWriteValue("Slideshow", "ImagesRootPath", new FileInfo(Wallpaper_Slideshow_Images[0]).Directory.FullName);
                                        }

                                        for (int i = 0, loopTo = Wallpaper_Slideshow_Images.Count() - 1; i <= loopTo; i++)
                                            _ini.IniWriteValue("Slideshow", "Item" + i + "Path", Wallpaper_Slideshow_Images[i]);
                                    }

                                }
                            }

                            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Personalization\Desktop Slideshow", "Interval", Wallpaper_Slideshow_Interval);
                            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Personalization\Desktop Slideshow", "Shuffle", Wallpaper_Slideshow_Shuffle);
                        }
                    }
                }

                public static bool operator ==(Wallpaper First, Wallpaper Second)
                {
                    return First.Equals(Second);
                }


                public static bool operator !=(Wallpaper First, Wallpaper Second)
                {
                    return !First.Equals(Second);
                }

                public object Clone()
                {
                    return MemberwiseClone();
                }
            }
            public struct WallpaperTone : ICloneable
            {
                public bool Enabled;
                public string Image;
                public int H, S, L;

                public void Load(string SubKey)
                {
                    string wallpaper;

                    if (SubKey.ToLower() == "winxp")
                    {
                        wallpaper = My.Env.PATH_Windows + @"\Web\Wallpaper\Bliss.bmp";
                    }
                    else
                    {
                        wallpaper = My.Env.PATH_Windows + @"\Web\Wallpaper\Windows\img0.jpg";
                    }

                    if (!File.Exists(wallpaper))
                        wallpaper = GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", wallpaper).ToString();

                    Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "Enabled", false));
                    Image = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "Image", wallpaper).ToString();
                    H = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "H", 0));
                    S = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "S", 100));
                    L = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "L", 100));
                }

                public static void Save_To_Registry(WallpaperTone WT, string SubKey, TreeView TreeView = null)
                {
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "Enabled", WT.Enabled);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "Image", WT.Image, RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "H", WT.H);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "S", WT.S);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\WallpaperTone\" + SubKey, "L", WT.L);
                }

                public void Apply(TreeView TreeView = null)
                {
                    if (!File.Exists(Image))
                        throw new IOException("Couldn't Find image");

                    string path;
                    if (!My.Env.WXP & !My.Env.WVista)
                    {
                        path = Path.Combine(My.Env.PATH_appData, "TintedWallpaper.bmp");
                    }
                    else
                    {
                        path = Path.Combine(My.Env.PATH_Windows, @"Web\Wallpaper\TintedWallpaper.bmp");
                    }

                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_SettingHSLImage, path), "pe_patch");

                    using (var ImgF = new ImageProcessor.ImageFactory())
                    {
                        ImgF.Load(Image);
                        ImgF.Hue(H, true);
                        ImgF.Saturation(S - 100);
                        ImgF.Brightness(L - 100);
                        ImgF.Image.Save(path, System.Drawing.Imaging.ImageFormat.Bmp);
                    }

                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Desktop.SETDESKWALLPAPER.ToString(), 0, path, SPIF.UpdateINIFile.ToString()), "dll");
                    User32.SystemParametersInfo((int)SPI.Desktop.SETDESKWALLPAPER, 0, path, (int)SPIF.UpdateINIFile);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", path, RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "BackgroundType", (int)Wallpaper.WallpaperTypes.Picture);

                    My.MyProject.Forms.MainFrm.Update_Wallpaper_Preview();
                }

                public static bool operator ==(WallpaperTone First, WallpaperTone Second)
                {
                    return First.Equals(Second);
                }

                public static bool operator !=(WallpaperTone First, WallpaperTone Second)
                {
                    return !First.Equals(Second);
                }

                public object Clone()
                {
                    return MemberwiseClone();
                }
            }
            public struct MetricsFonts : ICloneable
            {
                public bool Enabled;
                public int BorderWidth;
                public int CaptionHeight;
                public int CaptionWidth;
                public int IconSpacing;
                public int IconVerticalSpacing;
                public int MenuHeight;
                public int MenuWidth;
                public int PaddedBorderWidth;
                public int ScrollHeight;
                public int ScrollWidth;
                public int SmCaptionHeight;
                public int SmCaptionWidth;
                public int DesktopIconSize;
                public int ShellIconSize;
                public int ShellSmallIconSize;
                public bool Fonts_SingleBitPP;

                public Font CaptionFont;
                public Font IconFont;
                public Font MenuFont;
                public Font MessageFont;
                public Font SmCaptionFont;
                public Font StatusFont;
                public string FontSubstitute_MSShellDlg;
                public string FontSubstitute_MSShellDlg2;
                public string FontSubstitute_SegoeUI;

                public static bool operator ==(MetricsFonts First, MetricsFonts Second)
                {
                    return First.Equals(Second);
                }

                public static bool operator !=(MetricsFonts First, MetricsFonts Second)
                {
                    return !First.Equals(Second);
                }

                public object Clone()
                {
                    return MemberwiseClone();
                }

                public void Overwrite_Metrics(VisualStyleMetrics vs)
                {
                    CaptionHeight = vs.Sizes.CaptionBarHeight;
                    ScrollHeight = vs.Sizes.ScrollbarHeight;
                    ScrollWidth = vs.Sizes.ScrollbarWidth;
                    SmCaptionHeight = vs.Sizes.SMCaptionBarHeight;
                    SmCaptionWidth = vs.Sizes.SMCaptionBarWidth;
                }

                public void Overwrite_Fonts(VisualStyleMetrics vs)
                {
                    CaptionFont = vs.Fonts.CaptionFont;
                    IconFont = vs.Fonts.IconTitleFont;
                    MenuFont = vs.Fonts.MenuFont;
                    SmCaptionFont = vs.Fonts.SmallCaptionFont;
                    MessageFont = vs.Fonts.MsgBoxFont;
                    StatusFont = vs.Fonts.StatusFont;
                }

                public void Load(MetricsFonts _DefMetricsFonts)
                {
                    Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Metrics", "", _DefMetricsFonts.Enabled));
                    BorderWidth = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "BorderWidth", _DefMetricsFonts.BorderWidth * -15)) / -15;
                    CaptionHeight = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionHeight", _DefMetricsFonts.CaptionHeight * -15)) / -15;
                    CaptionWidth = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionWidth", _DefMetricsFonts.CaptionWidth * -15)) / -15;
                    IconSpacing = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconSpacing", _DefMetricsFonts.IconSpacing * -15)) / -15;
                    IconVerticalSpacing = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconVerticalSpacing", _DefMetricsFonts.IconVerticalSpacing * -15)) / -15;
                    MenuHeight = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuHeight", _DefMetricsFonts.MenuHeight * -15)) / -15;
                    MenuWidth = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuWidth", _DefMetricsFonts.MenuWidth * -15)) / -15;
                    PaddedBorderWidth = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "PaddedBorderWidth", _DefMetricsFonts.PaddedBorderWidth * -15)) / -15;
                    ScrollHeight = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollHeight", _DefMetricsFonts.ScrollHeight * -15)) / -15;
                    ScrollWidth = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollWidth", _DefMetricsFonts.ScrollWidth * -15)) / -15;
                    SmCaptionHeight = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionHeight", _DefMetricsFonts.SmCaptionHeight * -15)) / -15;
                    SmCaptionWidth = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionWidth", _DefMetricsFonts.SmCaptionWidth * -15)) / -15;

                    if (My.Env.WXP)
                    {
                        try
                        {
                            ShellIconSize = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "Shell Icon Size", _DefMetricsFonts.ShellIconSize));
                        }
                        catch
                        {
                            ShellIconSize = _DefMetricsFonts.ShellIconSize;
                        }

                        try
                        {
                            ShellSmallIconSize = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "Shell Small Icon Size", _DefMetricsFonts.ShellSmallIconSize));
                        }
                        catch
                        {
                            ShellSmallIconSize = _DefMetricsFonts.ShellSmallIconSize;
                        }
                    }

                    DesktopIconSize = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\Bags\1\Desktop", "IconSize", _DefMetricsFonts.DesktopIconSize));
                    CaptionFont = ((byte[])GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionFont", _DefMetricsFonts.CaptionFont.ToByte())).ToFont();
                    IconFont = ((byte[])GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconFont", _DefMetricsFonts.IconFont.ToByte())).ToFont();
                    MenuFont = ((byte[])GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuFont", _DefMetricsFonts.MenuFont.ToByte())).ToFont();
                    MessageFont = ((byte[])GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MessageFont", _DefMetricsFonts.MessageFont.ToByte())).ToFont();
                    SmCaptionFont = ((byte[])GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionFont", _DefMetricsFonts.SmCaptionFont.ToByte())).ToFont();
                    StatusFont = ((byte[])GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "StatusFont", _DefMetricsFonts.StatusFont.ToByte())).ToFont();
                    FontSubstitute_MSShellDlg = GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "MS Shell Dlg", _DefMetricsFonts.FontSubstitute_MSShellDlg).ToString();
                    FontSubstitute_MSShellDlg2 = GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "MS Shell Dlg 2", _DefMetricsFonts.FontSubstitute_MSShellDlg2).ToString();
                    FontSubstitute_SegoeUI = GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "Segoe UI", _DefMetricsFonts.FontSubstitute_SegoeUI).ToString();

                    if (GetWindowsScreenScalingFactor() > 100d)
                    {
                        CaptionFont = AdjustFont(CaptionFont, false);
                        IconFont = AdjustFont(IconFont, false);
                        MenuFont = AdjustFont(MenuFont, false);
                        MessageFont = AdjustFont(MessageFont, false);
                        SmCaptionFont = AdjustFont(SmCaptionFont, false);
                        StatusFont = AdjustFont(StatusFont, false);
                    }


                    bool temp = false;
                    Fixer.SystemParametersInfo((int)SPI.Fonts.GETFONTSMOOTHING, default, ref temp, (int)SPIF.None);
                    Fonts_SingleBitPP = !temp || Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "FontSmoothingType", My.Env.WXP ? 1 : 2)) != 2;
                }

                private Font AdjustFont(Font Font, bool Reverse)
                {
                    int DPI = (int)Math.Round(GetWindowsScreenScalingFactor());
                    if (DPI > 0)
                    {
                        float font_size = (float)((double)Font.Size * (!Reverse ? 100d / DPI : DPI / 100d));
                        if (font_size > 0f)
                        {
                            return new Font(Font.Name, font_size, Font.Style, GraphicsUnit.Pixel);
                        }
                        else
                        {
                            return Font;
                        }
                    }
                    else
                    {
                        return Font;
                    }
                }

                public void Apply(TreeView TreeView = null)
                {
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Metrics", "", Enabled);

                    if (Enabled)
                    {
                        // If Core.GetWindowsScreenScalingFactor > 100 Then
                        // CaptionFont = AdjustFont(CaptionFont, True)
                        // IconFont = AdjustFont(IconFont, True)
                        // MenuFont = AdjustFont(MenuFont, True)
                        // MessageFont = AdjustFont(MessageFont, True)
                        // SmCaptionFont = AdjustFont(SmCaptionFont, True)
                        // StatusFont = AdjustFont(StatusFont, True)
                        // End If

                        int OldDPI = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "AppliedDPI", GetWindowsScreenScalingFactor()));
                        EditReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "AppliedDPI", 100);

                        var lfCaptionFont = new LogFont();
                        CaptionFont.ToLogFont(lfCaptionFont);
                        var lfIconFont = new LogFont();
                        IconFont.ToLogFont(lfIconFont);
                        var lfMenuFont = new LogFont();
                        MenuFont.ToLogFont(lfMenuFont);
                        var lfMessageFont = new LogFont();
                        MessageFont.ToLogFont(lfMessageFont);
                        var lfSMCaptionFont = new LogFont();
                        SmCaptionFont.ToLogFont(lfSMCaptionFont);
                        var lfStatusFont = new LogFont();
                        StatusFont.ToLogFont(lfStatusFont);

                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "FontSmoothing", !Fonts_SingleBitPP ? 2 : 0);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "FontSmoothingType", !Fonts_SingleBitPP ? 2 : 1);

                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Fonts.SETFONTSMOOTHING.ToString(), !Fonts_SingleBitPP, "null", SPIF.UpdateINIFile.ToString()), "dll");
                        SystemParametersInfo((int)SPI.Fonts.SETFONTSMOOTHING, !Fonts_SingleBitPP, default, (int)SPIF.UpdateINIFile);

                        if (!My.Env.Settings.ThemeApplyingBehavior.DelayMetrics)
                        {
                            NONCLIENTMETRICS NCM = new NONCLIENTMETRICS();
                            NCM.cbSize = Marshal.SizeOf(NCM);
                            ICONMETRICS ICO = new ICONMETRICS();
                            ICO.cbSize = (uint)Marshal.SizeOf(ICO);

                            SystemParametersInfo((int)SPI.Icons.GETICONMETRICS, (int)ICO.cbSize, ref ICO, SPIF.None);

                            {
                                ref var temp = ref NCM;
                                temp.lfCaptionFont = lfCaptionFont;
                                temp.lfSMCaptionFont = lfSMCaptionFont;
                                temp.lfStatusFont = lfStatusFont;
                                temp.lfMenuFont = lfMenuFont;
                                temp.lfMessageFont = lfMessageFont;

                                temp.iBorderWidth = BorderWidth;
                                temp.iScrollWidth = ScrollWidth;
                                temp.iScrollHeight = ScrollHeight;
                                temp.iCaptionWidth = CaptionWidth;
                                temp.iCaptionHeight = CaptionHeight;
                                temp.iSMCaptionWidth = SmCaptionWidth;
                                temp.iSMCaptionHeight = SmCaptionHeight;
                                temp.iMenuWidth = MenuWidth;
                                temp.iMenuHeight = MenuHeight;
                                temp.iPaddedBorderWidth = PaddedBorderWidth;
                            }

                            {
                                ref var temp1 = ref ICO;
                                temp1.iHorzSpacing = IconSpacing;
                                temp1.iVertSpacing = IconVerticalSpacing;
                                temp1.lfFont = lfIconFont;
                            }

                            if (TreeView is not null)
                                AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Metrics.SETNONCLIENTMETRICS.ToString(), Marshal.SizeOf(NCM), NCM.ToString(), SPIF.UpdateINIFile.ToString()), "dll");
                            SystemParametersInfo((int)SPI.Metrics.SETNONCLIENTMETRICS, Marshal.SizeOf(NCM), ref NCM, SPIF.UpdateINIFile);

                            if (TreeView is not null)
                                AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Icons.SETICONMETRICS.ToString(), Marshal.SizeOf(ICO), ICO.ToString(), SPIF.UpdateINIFile.ToString()), "dll");
                            SystemParametersInfo((int)SPI.Icons.SETICONMETRICS, Marshal.SizeOf(ICO), ref ICO, SPIF.UpdateINIFile);
                        }

                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionFont", lfCaptionFont.ToByte(), RegistryValueKind.Binary);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconFont", lfIconFont.ToByte(), RegistryValueKind.Binary);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuFont", lfMenuFont.ToByte(), RegistryValueKind.Binary);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MessageFont", lfMessageFont.ToByte(), RegistryValueKind.Binary);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionFont", lfSMCaptionFont.ToByte(), RegistryValueKind.Binary);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "StatusFont", lfStatusFont.ToByte(), RegistryValueKind.Binary);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "BorderWidth", BorderWidth * -15, RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionHeight", CaptionHeight * -15, RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "CaptionWidth", CaptionWidth * -15, RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconSpacing", IconSpacing * -15, RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "IconVerticalSpacing", IconVerticalSpacing * -15, RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuHeight", MenuHeight * -15, RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "MenuWidth", MenuWidth * -15, RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "PaddedBorderWidth", PaddedBorderWidth * -15, RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollHeight", ScrollHeight * -15, RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "ScrollWidth", ScrollWidth * -15, RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionHeight", SmCaptionHeight * -15, RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "SmCaptionWidth", SmCaptionWidth * -15, RegistryValueKind.String);

                        if (My.Env.WXP)
                        {
                            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "Shell Icon Size", ShellIconSize, RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "Shell Small Icon Size", ShellSmallIconSize, RegistryValueKind.String);
                        }

                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\Bags\1\Desktop", "IconSize", DesktopIconSize, RegistryValueKind.String);

                        if (My.Env.Settings.ThemeApplyingBehavior.Metrics_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                        {
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "CaptionFont", lfCaptionFont.ToByte(), RegistryValueKind.Binary);
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "IconFont", lfIconFont.ToByte(), RegistryValueKind.Binary);
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "MenuFont", lfMenuFont.ToByte(), RegistryValueKind.Binary);
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "MessageFont", lfMessageFont.ToByte(), RegistryValueKind.Binary);
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "SmCaptionFont", lfSMCaptionFont.ToByte(), RegistryValueKind.Binary);
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "StatusFont", lfStatusFont.ToByte(), RegistryValueKind.Binary);
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "BorderWidth", BorderWidth * -15, RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "CaptionHeight", CaptionHeight * -15, RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "CaptionWidth", CaptionWidth * -15, RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "IconSpacing", IconSpacing * -15, RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "IconVerticalSpacing", IconVerticalSpacing * -15, RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "MenuHeight", MenuHeight * -15, RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "MenuWidth", MenuWidth * -15, RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "PaddedBorderWidth", PaddedBorderWidth * -15, RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "ScrollHeight", ScrollHeight * -15, RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "ScrollWidth", ScrollWidth * -15, RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "SmCaptionHeight", SmCaptionHeight * -15, RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "SmCaptionWidth", SmCaptionWidth * -15, RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "Shell Icon Size", ShellIconSize, RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics", "Shell Small Icon Size", ShellSmallIconSize, RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Software\Microsoft\Windows\Shell\Bags\1\Desktop", "IconSize", DesktopIconSize, RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "FontSmoothing", !Fonts_SingleBitPP ? 2 : 0);
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "FontSmoothingType", !Fonts_SingleBitPP ? 2 : 1);
                        }

                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "MS Shell Dlg", FontSubstitute_MSShellDlg, RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "MS Shell Dlg 2", FontSubstitute_MSShellDlg2, RegistryValueKind.String);

                        if (string.IsNullOrWhiteSpace(FontSubstitute_SegoeUI))
                        {
                            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI (TrueType)", "segoeui.ttf", RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Bold (TrueType)", "segoeuib.ttf", RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Bold Italic (TrueType)", "segoeuiz.ttf", RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Black (TrueType)", "seguibl.ttf", RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Black Italic (TrueType)", "seguibli.ttf", RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Italic (TrueType)", "segoeuii.ttf", RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Light (TrueType)", "segoeuil.ttf", RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Light Italic (TrueType)", "seguili.ttf", RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semibold (TrueType)", "seguisb.ttf", RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semibold Italic (TrueType)", "seguisbi.ttf", RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semilight (TrueType)", "segoeuisl.ttf", RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semilight Italic (TrueType)", "seguisli.ttf", RegistryValueKind.String);
                        }
                        else
                        {
                            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI (TrueType)", "", RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Bold (TrueType)", "", RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Bold Italic (TrueType)", "", RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Black (TrueType)", "", RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Black Italic (TrueType)", "", RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Italic (TrueType)", "", RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Light (TrueType)", "", RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Light Italic (TrueType)", "", RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semibold (TrueType)", "", RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semibold Italic (TrueType)", "", RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semilight (TrueType)", "", RegistryValueKind.String);
                            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", "Segoe UI Semilight Italic (TrueType)", "", RegistryValueKind.String);
                        }
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", "Segoe UI", FontSubstitute_SegoeUI, RegistryValueKind.String);

                        EditReg(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "AppliedDPI", OldDPI);
                    }

                }
            }
            public struct WinEffects : ICloneable
            {
                public bool Enabled;

                public bool WindowAnimation;
                public bool WindowShadow;
                public bool WindowUIEffects;
                public bool ShowWinContentDrag;
                public bool AnimateControlsInsideWindow;

                public bool MenuAnimation;
                public MenuAnimType MenuFade;
                public bool MenuSelectionFade;
                public uint MenuShowDelay;            // Microsoft uses this as DWORD, which its equivalent is UInteger, not Integer

                public bool ComboBoxAnimation;
                public bool ListBoxSmoothScrolling;

                public bool TooltipAnimation;
                public MenuAnimType TooltipFade;

                public bool IconsShadow;
                public bool IconsDesktopTranslSel;

                public bool KeyboardUnderline;
                public uint FocusRectWidth;           // Microsoft uses this as DWORD, which its equivalent is UInteger, not Integer
                public uint FocusRectHeight;          // Microsoft uses this as DWORD, which its equivalent is UInteger, not Integer
                public uint Caret;
                public int NotificationDuration;
                public bool ShakeToMinimize;
                public bool AWT_Enabled;
                public bool AWT_BringActivatedWindowToTop;
                public int AWT_Delay;
                public bool SnapCursorToDefButton;

                public bool Win11ClassicContextMenu;
                public bool SysListView32;
                public bool ShowSecondsInSystemClock;
                public bool BalloonNotifications;
                public bool PaintDesktopVersion;

                public bool Win11BootDots;
                public ExplorerBar Win11ExplorerBar;
                public bool DisableNavBar;

                public bool AutoHideScrollBars;
                public bool FullScreenStartMenu;
                public bool ColorFilter_Enabled;
                public ColorFilters ColorFilter;

                public bool ClassicVolMixer;

                public enum ExplorerBar
                {
                    Default,
                    Ribbon,
                    Bar
                }

                public enum ColorFilters
                {
                    Grayscale,
                    Inverted,
                    GrayscaleInverted,
                    RedGreen_deuteranopia,
                    RedGreen_protanopia,
                    BlueYellow
                }

                public enum MenuAnimType
                {
                    Fade,
                    Scroll
                }

                public void Load(WinEffects _DefEffects)
                {
                    Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\WindowsEffects", "", true));

                    if (Fixer.SystemParametersInfo((int)SPI.Effects.GETDROPSHADOW, 0, ref WindowShadow, (int)SPIF.None) == 0)
                        WindowShadow = _DefEffects.WindowShadow;
                    if (Fixer.SystemParametersInfo((int)SPI.Effects.GETUIEFFECTS, 0, ref WindowUIEffects, (int)SPIF.None) == 0)
                        WindowUIEffects = _DefEffects.WindowUIEffects;
                    if (Fixer.SystemParametersInfo((int)SPI.Effects.GETCLIENTAREAANIMATION, 0, ref AnimateControlsInsideWindow, (int)SPIF.None) == 0)
                        AnimateControlsInsideWindow = _DefEffects.AnimateControlsInsideWindow;
                    if (Fixer.SystemParametersInfo((int)SPI.Effects.GETMENUANIMATION, 0, ref MenuAnimation, (int)SPIF.None) == 0)
                        MenuAnimation = _DefEffects.MenuAnimation;
                    if (Fixer.SystemParametersInfo((int)SPI.Effects.GETSELECTIONFADE, 0, ref MenuSelectionFade, (int)SPIF.None) == 0)
                        MenuSelectionFade = _DefEffects.MenuSelectionFade;
                    if (Fixer.SystemParametersInfo((int)SPI.Effects.GETMENUSHOWDELAY, 0, ref MenuShowDelay, (int)SPIF.None) == 0)
                        MenuShowDelay = _DefEffects.MenuShowDelay;
                    if (Fixer.SystemParametersInfo((int)SPI.Effects.GETCOMBOBOXANIMATION, 0, ref ComboBoxAnimation, (int)SPIF.None) == 0)
                        ComboBoxAnimation = _DefEffects.ComboBoxAnimation;
                    if (Fixer.SystemParametersInfo((int)SPI.Effects.GETLISTBOXSMOOTHSCROLLING, 0, ref ListBoxSmoothScrolling, (int)SPIF.None) == 0)
                        ListBoxSmoothScrolling = _DefEffects.ListBoxSmoothScrolling;
                    if (Fixer.SystemParametersInfo((int)SPI.Effects.GETTOOLTIPANIMATION, 0, ref TooltipAnimation, (int)SPIF.None) == 0)
                        TooltipAnimation = _DefEffects.TooltipAnimation;
                    if (Fixer.SystemParametersInfo((int)SPI.Effects.GETDRAGFULLWINDOWS, 0, ref ShowWinContentDrag, (int)SPIF.None) == 0)
                        ShowWinContentDrag = _DefEffects.ShowWinContentDrag;
                    if (Fixer.SystemParametersInfo((int)SPI.Effects.GETMENUUNDERLINES, 0, ref KeyboardUnderline, (int)SPIF.None) == 0)
                        KeyboardUnderline = _DefEffects.KeyboardUnderline;
                    if (Fixer.SystemParametersInfo((int)SPI.FocusRect.GETFOCUSBORDERWIDTH, 0, ref FocusRectWidth, (int)SPIF.None) == 0)
                        FocusRectWidth = _DefEffects.FocusRectWidth;
                    if (Fixer.SystemParametersInfo((int)SPI.FocusRect.GETFOCUSBORDERHEIGHT, 0, ref FocusRectHeight, (int)SPIF.None) == 0)
                        FocusRectHeight = _DefEffects.FocusRectHeight;
                    if (Fixer.SystemParametersInfo((int)SPI.Effects.GETCARETWIDTH, 0, ref Caret, (int)SPIF.None) == 0)
                        Caret = _DefEffects.Caret;
                    if (Fixer.SystemParametersInfo((int)SPI.Effects.GETACTIVEWINDOWTRACKING, 0, ref AWT_Enabled, (int)SPIF.None) == 0)
                        AWT_Enabled = _DefEffects.AWT_Enabled;
                    if (Fixer.SystemParametersInfo((int)SPI.Effects.GETACTIVEWNDTRKZORDER, 0, ref AWT_BringActivatedWindowToTop, (int)SPIF.None) == 0)
                        AWT_BringActivatedWindowToTop = _DefEffects.AWT_BringActivatedWindowToTop;
                    if (Fixer.SystemParametersInfo((int)SPI.Effects.GETACTIVEWNDTRKTIMEOUT, 0, ref AWT_Delay, (int)SPIF.None) == 0)
                        AWT_Delay = _DefEffects.AWT_Delay;
                    if (Fixer.SystemParametersInfo((int)SPI.Cursors.GETSNAPTODEFBUTTON, 0, ref SnapCursorToDefButton, (int)SPIF.None) == 0)
                        SnapCursorToDefButton = _DefEffects.SnapCursorToDefButton;

                    ANIMATIONINFO anim = new ANIMATIONINFO();
                    anim.cbSize = (uint)Marshal.SizeOf(anim);

                    if (SystemParametersInfo((int)SPI.Effects.GETANIMATION, (int)anim.cbSize, ref anim, SPIF.None) == 1)
                    {
                        WindowAnimation = anim.IMinAnimate.ToBoolean();
                    }
                    else
                    {
                        WindowAnimation = _DefEffects.WindowAnimation;
                    }

                    var x = default(bool);

                    if (Fixer.SystemParametersInfo((int)SPI.Effects.GETMENUFADE, 0, ref x, (int)SPIF.None) == 1)
                    {
                        MenuFade = x ? MenuAnimType.Fade : MenuAnimType.Scroll;
                    }
                    else
                    {
                        MenuFade = _DefEffects.MenuFade;
                    }

                    if (Fixer.SystemParametersInfo((int)SPI.Effects.GETTOOLTIPFADE, 0, ref x, (int)SPIF.None) == 1)
                    {
                        TooltipFade = x ? MenuAnimType.Fade : MenuAnimType.Scroll;
                    }
                    else
                    {
                        TooltipFade = _DefEffects.TooltipFade;
                    }

                    IconsShadow = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ListviewShadow", _DefEffects.IconsShadow));
                    IconsDesktopTranslSel = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ListviewAlphaSelect", _DefEffects.IconsDesktopTranslSel));
                    NotificationDuration = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Control Panel\Accessibility", "MessageDuration", _DefEffects.NotificationDuration));
                    ShowSecondsInSystemClock = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ShowSecondsInSystemClock", _DefEffects.ShowSecondsInSystemClock));
                    BalloonNotifications = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Explorer", "EnableLegacyBalloonNotifications", _DefEffects.BalloonNotifications));
                    PaintDesktopVersion = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "PaintDesktopVersion", _DefEffects.PaintDesktopVersion));
                    ClassicVolMixer = !Convert.ToBoolean(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\MTCUVC", "EnableMtcUvc", !_DefEffects.ClassicVolMixer));

                    bool temp = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "DisallowShaking", !_DefEffects.ShakeToMinimize));
                    ShakeToMinimize = !temp;

                    try
                    {
                        if (My.MyProject.Computer.Registry.CurrentUser.OpenSubKey(@"Software\Classes\CLSID").OpenSubKey(@"{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\InprocServer32") is not null)
                        {
                            Win11ClassicContextMenu = true;
                        }
                        else
                        {
                            Win11ClassicContextMenu = false;
                        }
                    }
                    catch
                    {
                        Win11ClassicContextMenu = _DefEffects.Win11ClassicContextMenu;
                    }
                    finally
                    {
                        My.MyProject.Computer.Registry.CurrentUser.Close();
                    }

                    try
                    {
                        if (My.MyProject.Computer.Registry.CurrentUser.OpenSubKey(@"Software\Classes\CLSID").OpenSubKey(@"{1eeb5b5a-06fb-4732-96b3-975c0194eb39}\InprocServer32") is not null)
                        {
                            SysListView32 = true;
                        }
                        else
                        {
                            SysListView32 = false;
                        }
                    }
                    catch
                    {
                        SysListView32 = _DefEffects.SysListView32;
                    }
                    finally
                    {
                        My.MyProject.Computer.Registry.CurrentUser.Close();
                    }

                    if (GetReg(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\BootControl", "BootProgressAnimation", null) is null)
                    {
                        Win11BootDots = !My.Env.W11;
                    }

                    else
                    {
                        switch (GetReg(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\BootControl", "BootProgressAnimation", My.Env.W11 ? 1 : 0))
                        {
                            case 0:
                                {
                                    Win11BootDots = true;
                                    break;
                                }

                            case 1:
                                {
                                    Win11BootDots = false;
                                    break;
                                }

                            default:
                                {
                                    Win11BootDots = false;
                                    break;
                                }

                        }
                    }

                    Win11ExplorerBar = (ExplorerBar)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\WindowsEffects", "Win11ExplorerBar", _DefEffects.Win11ExplorerBar));

                    try
                    {
                        if (My.MyProject.Computer.Registry.CurrentUser.OpenSubKey(@"Software\Classes\CLSID").OpenSubKey(@"{056440FD-8568-48e7-A632-72157243B55B}\InprocServer32") is not null)
                        {
                            DisableNavBar = true;
                        }
                        else
                        {
                            DisableNavBar = false;
                        }
                    }
                    catch
                    {
                        DisableNavBar = _DefEffects.DisableNavBar;
                    }
                    finally
                    {
                        My.MyProject.Computer.Registry.CurrentUser.Close();
                    }

                    AutoHideScrollBars = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Control Panel\Accessibility", "DynamicScrollbars", _DefEffects.AutoHideScrollBars));
                    FullScreenStartMenu = Convert.ToBoolean(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Policies\Microsoft\Windows\Explorer", "ForceStartSize", _DefEffects.FullScreenStartMenu ? 2 : 0)) == 2) ? true : false;
                    ColorFilter_Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\ColorFiltering", "Active", _DefEffects.ColorFilter_Enabled));
                    ColorFilter = (ColorFilters)GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\ColorFiltering", "FilterType", _DefEffects.ColorFilter);
                }

                public void Apply(TreeView TreeView = null)
                {
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\WindowsEffects", "", Enabled);

                    if (Enabled)
                    {
                        ANIMATIONINFO anim = new ANIMATIONINFO();
                        anim.cbSize = (uint)Marshal.SizeOf(anim);
                        anim.IMinAnimate = WindowAnimation.ToInteger();

                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETANIMATION.ToString(), anim.cbSize, anim.ToString(), SPIF.UpdateINIFile.ToString()), "dll");
                        SystemParametersInfo((int)SPI.Effects.SETANIMATION, (int)anim.cbSize, ref anim, SPIF.UpdateINIFile);

                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETDROPSHADOW.ToString(), 0, WindowShadow, SPIF.UpdateINIFile.ToString()), "dll");
                        SystemParametersInfo((int)SPI.Effects.SETDROPSHADOW, 0, WindowShadow, (int)SPIF.UpdateINIFile);

                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETUIEFFECTS.ToString(), 0, WindowUIEffects, SPIF.UpdateINIFile.ToString()), "dll");
                        SystemParametersInfo((int)SPI.Effects.SETUIEFFECTS, 0, WindowUIEffects, (int)SPIF.UpdateINIFile);

                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETCLIENTAREAANIMATION.ToString(), 0, AnimateControlsInsideWindow, SPIF.UpdateINIFile.ToString()), "dll");
                        SystemParametersInfo((int)SPI.Effects.SETCLIENTAREAANIMATION, 0, AnimateControlsInsideWindow, (int)SPIF.UpdateINIFile);

                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETDRAGFULLWINDOWS.ToString(), 0, ShowWinContentDrag, SPIF.UpdateINIFile.ToString()), "dll");
                        SystemParametersInfo((int)SPI.Effects.SETDRAGFULLWINDOWS, ShowWinContentDrag, 0, (int)SPIF.UpdateINIFile);        // use uiParam not pvParam

                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETMENUANIMATION.ToString(), 0, MenuAnimation, SPIF.UpdateINIFile.ToString()), "dll");
                        SystemParametersInfo((int)SPI.Effects.SETMENUANIMATION, 0, MenuAnimation, (int)SPIF.UpdateINIFile);

                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETMENUFADE.ToString(), 0, MenuFade == MenuAnimType.Fade, SPIF.UpdateINIFile.ToString()), "dll");
                        SystemParametersInfo((int)SPI.Effects.SETMENUFADE, 0, MenuFade == MenuAnimType.Fade, (int)SPIF.UpdateINIFile);

                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETMENUSHOWDELAY.ToString(), MenuShowDelay, 0, SPIF.UpdateINIFile.ToString()), "dll");
                        SystemParametersInfo((int)SPI.Effects.SETMENUSHOWDELAY, MenuShowDelay, 0, (int)SPIF.UpdateINIFile);               // use uiParam not pvParam

                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETSELECTIONFADE.ToString(), 0, MenuSelectionFade, SPIF.UpdateINIFile.ToString()), "dll");
                        SystemParametersInfo((int)SPI.Effects.SETSELECTIONFADE, 0, MenuSelectionFade, (int)SPIF.UpdateINIFile);

                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETCOMBOBOXANIMATION.ToString(), 0, ComboBoxAnimation, SPIF.UpdateINIFile.ToString()), "dll");
                        SystemParametersInfo((int)SPI.Effects.SETCOMBOBOXANIMATION, 0, ComboBoxAnimation, (int)SPIF.UpdateINIFile);

                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETLISTBOXSMOOTHSCROLLING.ToString(), 0, ListBoxSmoothScrolling, SPIF.UpdateINIFile.ToString()), "dll");
                        SystemParametersInfo((int)SPI.Effects.SETLISTBOXSMOOTHSCROLLING, 0, ListBoxSmoothScrolling, (int)SPIF.UpdateINIFile);

                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETTOOLTIPANIMATION.ToString(), 0, TooltipAnimation, SPIF.UpdateINIFile.ToString()), "dll");
                        SystemParametersInfo((int)SPI.Effects.SETTOOLTIPANIMATION, 0, TooltipAnimation, (int)SPIF.UpdateINIFile);

                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETTOOLTIPFADE.ToString(), 0, TooltipFade == MenuAnimType.Fade, SPIF.UpdateINIFile.ToString()), "dll");
                        SystemParametersInfo((int)SPI.Effects.SETTOOLTIPFADE, 0, TooltipFade == MenuAnimType.Fade, (int)SPIF.UpdateINIFile);

                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETMENUUNDERLINES.ToString(), 0, KeyboardUnderline, SPIF.UpdateINIFile.ToString()), "dll");
                        SystemParametersInfo((int)SPI.Effects.SETMENUUNDERLINES, 0, KeyboardUnderline, (int)SPIF.UpdateINIFile);

                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.FocusRect.SETFOCUSBORDERWIDTH.ToString(), 0, FocusRectWidth, SPIF.UpdateINIFile.ToString()), "dll");
                        SystemParametersInfo((int)SPI.FocusRect.SETFOCUSBORDERWIDTH, 0, FocusRectWidth, (int)SPIF.UpdateINIFile);

                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.FocusRect.SETFOCUSBORDERHEIGHT.ToString(), 0, FocusRectHeight, SPIF.UpdateINIFile.ToString()), "dll");
                        SystemParametersInfo((int)SPI.FocusRect.SETFOCUSBORDERHEIGHT, 0, FocusRectHeight, (int)SPIF.UpdateINIFile);

                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETCARETWIDTH.ToString(), 0, Caret, SPIF.UpdateINIFile.ToString()), "dll");
                        SystemParametersInfo((int)SPI.Effects.SETCARETWIDTH, 0, Caret, (int)SPIF.UpdateINIFile);

                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETACTIVEWINDOWTRACKING.ToString(), 0, AWT_Enabled, SPIF.UpdateINIFile.ToString()), "dll");
                        SystemParametersInfo((int)SPI.Effects.SETACTIVEWINDOWTRACKING, 0, AWT_Enabled, (int)SPIF.UpdateINIFile);

                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETACTIVEWNDTRKZORDER.ToString(), 0, AWT_BringActivatedWindowToTop, SPIF.UpdateINIFile.ToString()), "dll");
                        SystemParametersInfo((int)SPI.Effects.SETACTIVEWNDTRKZORDER, 0, AWT_BringActivatedWindowToTop, (int)SPIF.UpdateINIFile);

                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Effects.SETACTIVEWNDTRKTIMEOUT.ToString(), 0, AWT_Delay, SPIF.UpdateINIFile.ToString()), "dll");
                        SystemParametersInfo((int)SPI.Effects.SETACTIVEWNDTRKTIMEOUT, 0, AWT_Delay, (int)SPIF.UpdateINIFile);

                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Cursors.SETSNAPTODEFBUTTON.ToString(), 0, SnapCursorToDefButton, SPIF.UpdateINIFile.ToString()), "dll");
                        SystemParametersInfo((int)SPI.Cursors.SETSNAPTODEFBUTTON, SnapCursorToDefButton, 0, (int)SPIF.UpdateINIFile);     // use uiParam not pvParam

                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ListviewShadow", IconsShadow.ToInteger());
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ListviewAlphaSelect", IconsDesktopTranslSel.ToInteger());
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "MenuShowDelay", MenuShowDelay, RegistryValueKind.String);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Accessibility", "MessageDuration", NotificationDuration);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "DisallowShaking", (!ShakeToMinimize).ToInteger());
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ShowSecondsInSystemClock", ShowSecondsInSystemClock.ToInteger());
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "PaintDesktopVersion", PaintDesktopVersion.ToInteger());
                        EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\MTCUVC", "EnableMtcUvc", !ClassicVolMixer);

                        EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Accessibility", "DynamicScrollbars", AutoHideScrollBars);

                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\ColorFiltering", "Active", ColorFilter_Enabled);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\ColorFiltering", "FilterType", (int)ColorFilter);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows NT\CurrentVersion\Accessibility", "Configuration", ColorFilter_Enabled ? "colorfiltering" : "", RegistryValueKind.String);

                        if (My.Env.Settings.ThemeApplyingBehavior.UPM_HKU_DEFAULT)
                        {
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "PaintDesktopVersion", PaintDesktopVersion.ToInteger());
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "CaretWidth", Caret);
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "MenuShowDelay", MenuShowDelay);
                            EditReg(TreeView, @"HKEY_USERS\.DEFAULT\Control Panel\Mouse", "SnapToDefaultButton", SnapCursorToDefButton.ToInteger());
                        }

                        try
                        {
                            if (My.MyProject.Computer.Registry.CurrentUser.OpenSubKey(@"Software\ExplorerPatcher") is not null)
                            {
                                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\ExplorerPatcher", "FileExplorerCommandUI", Win11ExplorerBar);
                            }
                        }
                        catch
                        {
                            // Do nothing
                            My.MyProject.Computer.Registry.CurrentUser.Close();
                        }
                        finally
                        {
                            My.MyProject.Computer.Registry.CurrentUser.Close();
                        }

                        try
                        {
                            if (My.MyProject.Computer.Registry.CurrentUser.OpenSubKey(@"Software\StartIsBack") is not null)
                            {
                                EditReg(TreeView, @"HKEY_CURRENT_USER\Software\StartIsBack", "FrameStyle", Win11ExplorerBar);
                            }
                        }
                        catch
                        {
                            // Do nothing
                            My.MyProject.Computer.Registry.CurrentUser.Close();
                        }
                        finally
                        {
                            My.MyProject.Computer.Registry.CurrentUser.Close();
                        }

                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\WindowsEffects", "Win11ExplorerBar", Win11ExplorerBar);

                        if (My.Env.W11)
                            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\BootControl", "BootProgressAnimation", (!Win11BootDots).ToInteger());

                        if (My.Env.W8 | My.Env.W81 || My.Env.W10)
                        {
                            switch (Win11ExplorerBar)
                            {
                                case ExplorerBar.Bar:
                                    {
                                        if (File.Exists(My.Env.PATH_System32 + @"\UIRibbon.dll"))
                                        {
                                            if (TreeView is not null)
                                                AddNode(TreeView, My.Env.Lang.Verbose_EnableExplorerBar, "file_rename");

                                            Takeown_File(My.Env.PATH_System32 + @"\UIRibbon.dll");
                                            Move_File(My.Env.PATH_System32 + @"\UIRibbon.dll", My.Env.PATH_System32 + @"\UIRibbon.dll_bak");

                                            // DelReg_AdministratorDeflector("HKEY_LOCAL_MACHINE\SOFTWARE\Classes\CLSID", "{926749fa-2615-4987-8845-c33e65f2b957}")

                                        }

                                        break;
                                    }

                                default:
                                    {
                                        if (File.Exists(My.Env.PATH_System32 + @"\UIRibbon.dll_bak"))
                                        {
                                            if (TreeView is not null)
                                                AddNode(TreeView, My.Env.Lang.Verbose_RestoreExplorerBar, "file_rename");

                                            Takeown_File(My.Env.PATH_System32 + @"\UIRibbon.dll_bak");
                                            Takeown_File(My.Env.PATH_System32 + @"\UIRibbon.dll");
                                            Move_File(My.Env.PATH_System32 + @"\UIRibbon.dll_bak", My.Env.PATH_System32 + @"\UIRibbon.dll");
                                        }

                                        break;
                                    }

                                    // TakeOwn_Reg(Registry.LocalMachine.OpenSubKey("SOFTWARE\Classes\CLSID"), "{926749fa-2615-4987-8845-c33e65f2b957}")
                                    // EditReg_CMD([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Classes\CLSID\{926749fa-2615-4987-8845-c33e65f2b957}", "", "%SystemRoot%\system32\UIRibbon.dll", RegistryValueKind.ExpandString)
                                    // EditReg_CMD([TreeView], "HKEY_LOCAL_MACHINE\SOFTWARE\Classes\CLSID\{926749fa-2615-4987-8845-c33e65f2b957}", "ThreadingModel", "Apartment", RegistryValueKind.String)

                            }
                        }

                        // Try ... Catch is used as sometimes access to HKEY_CURRENT_USER\Software\Policies is denied
                        try
                        {
                            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Explorer", "EnableLegacyBalloonNotifications", BalloonNotifications.ToInteger());
                        }
                        catch
                        {
                            EditReg_CMD(TreeView, @"HKEY_CURRENT_USER\Software\Policies\Microsoft\Windows\Explorer", "EnableLegacyBalloonNotifications", BalloonNotifications.ToInteger());
                        }

                        // Try ... Catch is used as sometimes access to HKEY_CURRENT_USER\Software\Policies is denied
                        try
                        {
                            EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Policies\Microsoft\Windows\Explorer", "ForceStartSize", FullScreenStartMenu ? 2 : 0);
                        }
                        catch
                        {
                            EditReg_CMD(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Policies\Microsoft\Windows\Explorer", "ForceStartSize", FullScreenStartMenu ? 2 : 0);
                        }

                        if (My.Env.W11)
                        {
                            try
                            {
                                if (Win11ClassicContextMenu)
                                {
                                    if (TreeView is not null)
                                        AddNode(TreeView, @"HKEY_CURRENT_USER\Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2} > InprocServer32", "reg_add");
                                    My.MyProject.Computer.Registry.CurrentUser.CreateSubKey(@"Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}", true).CreateSubKey("InprocServer32", true).SetValue("", "", RegistryValueKind.String);
                                }
                                else
                                {
                                    if (TreeView is not null)
                                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_RegDelete, @"HKEY_CURRENT_USER\Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}"), "reg_delete");
                                    My.MyProject.Computer.Registry.CurrentUser.OpenSubKey(@"Software\Classes\CLSID", true).DeleteSubKeyTree("{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}", false);
                                }
                            }
                            catch
                            {
                                // Do nothing
                                My.MyProject.Computer.Registry.CurrentUser.Close();
                            }
                            finally
                            {
                                My.MyProject.Computer.Registry.CurrentUser.Close();
                            }
                        }

                        if (!My.Env.WXP && !My.Env.WVista)
                        {
                            try
                            {
                                if (SysListView32)
                                {
                                    if (TreeView is not null)
                                        AddNode(TreeView, @"HKEY_CURRENT_USER\Software\Classes\CLSID\{1eeb5b5a-06fb-4732-96b3-975c0194eb39} > InprocServer32", "reg_add");
                                    My.MyProject.Computer.Registry.CurrentUser.CreateSubKey(@"Software\Classes\CLSID\{1eeb5b5a-06fb-4732-96b3-975c0194eb39}", true).CreateSubKey("InprocServer32", true).SetValue("", "", RegistryValueKind.String);
                                }
                                else
                                {
                                    if (TreeView is not null)
                                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_RegDelete, @"HKEY_CURRENT_USER\Software\Classes\CLSID\{1eeb5b5a-06fb-4732-96b3-975c0194eb39}"), "reg_delete");
                                    My.MyProject.Computer.Registry.CurrentUser.OpenSubKey(@"Software\Classes\CLSID", true).DeleteSubKeyTree("{1eeb5b5a-06fb-4732-96b3-975c0194eb39}", false);
                                }
                            }
                            catch
                            {
                                // Do nothing
                                My.MyProject.Computer.Registry.CurrentUser.Close();
                            }
                            finally
                            {
                                My.MyProject.Computer.Registry.CurrentUser.Close();
                            }
                        }

                        try
                        {
                            if (DisableNavBar)
                            {
                                if (TreeView is not null)
                                    AddNode(TreeView, @"HKEY_CURRENT_USER\Software\Classes\CLSID\{056440FD-8568-48e7-A632-72157243B55B}, InprocServer32", "reg_add");
                                My.MyProject.Computer.Registry.CurrentUser.CreateSubKey(@"Software\Classes\CLSID\{056440FD-8568-48e7-A632-72157243B55B}", true).CreateSubKey("InprocServer32", true).SetValue("", "", RegistryValueKind.String);
                            }
                            else
                            {
                                if (TreeView is not null)
                                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_RegDelete, @"HKEY_CURRENT_USER\Software\Classes\CLSID\{056440FD-8568-48e7-A632-72157243B55B}"), "reg_delete");
                                My.MyProject.Computer.Registry.CurrentUser.OpenSubKey(@"Software\Classes\CLSID", true).DeleteSubKeyTree("{056440FD-8568-48e7-A632-72157243B55B}", false);
                            }
                        }
                        catch
                        {
                            // Do nothing
                            My.MyProject.Computer.Registry.CurrentUser.Close();
                        }
                        finally
                        {
                            My.MyProject.Computer.Registry.CurrentUser.Close();
                        }

                    }
                }

                public static bool operator ==(WinEffects First, WinEffects Second)
                {
                    return First.Equals(Second);
                }

                public static bool operator !=(WinEffects First, WinEffects Second)
                {
                    return !First.Equals(Second);
                }

                public object Clone()
                {
                    return MemberwiseClone();
                }
            }
            public struct ScreenSaver : ICloneable
            {
                public bool Enabled;
                public bool IsSecure;
                public int TimeOut;
                public string File;

                public void Load(ScreenSaver _DefScrSaver)
                {
                    Enabled = Convert.ToBoolean(Conversion.Val(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaveActive", _DefScrSaver.Enabled.ToInteger())));
                    IsSecure = Convert.ToBoolean(Conversion.Val(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaverIsSecure", _DefScrSaver.IsSecure.ToInteger())));
                    TimeOut = (int)Math.Round(Conversion.Val(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaveTimeOut", _DefScrSaver.TimeOut)));
                    File = GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "SCRNSAVE.EXE", _DefScrSaver.File).ToString();
                }

                public void Apply(TreeView TreeView = null)
                {
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaveActive", Enabled.ToInteger(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaverIsSecure", IsSecure.ToInteger(), RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaveTimeOut", TimeOut, RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Control Panel\Desktop", "SCRNSAVE.EXE", File, RegistryValueKind.String);
                }

                public static bool operator ==(ScreenSaver First, ScreenSaver Second)
                {
                    return First.Equals(Second);
                }

                public static bool operator !=(ScreenSaver First, ScreenSaver Second)
                {
                    return !First.Equals(Second);
                }
                public object Clone()
                {
                    return MemberwiseClone();
                }
            }
            public struct Sounds : ICloneable, IDisposable
            {
                public bool Enabled;

                public string Snd_Win_Default;
                public string Snd_Win_AppGPFault;
                public string Snd_Win_CCSelect;
                public string Snd_Win_ChangeTheme;
                public string Snd_Win_Close;
                public string Snd_Win_CriticalBatteryAlarm;
                public string Snd_Win_DeviceConnect;
                public string Snd_Win_DeviceDisconnect;
                public string Snd_Win_DeviceFail;
                public string Snd_Win_FaxBeep;
                public string Snd_Win_LowBatteryAlarm;
                public string Snd_Win_MailBeep;
                public string Snd_Win_Maximize;
                public string Snd_Win_MenuCommand;
                public string Snd_Win_MenuPopup;
                public string Snd_Win_MessageNudge;
                public string Snd_Win_Minimize;
                public string Snd_Win_Notification_Default;
                public string Snd_Win_Notification_IM;
                public string Snd_Win_Notification_Looping_Alarm;
                public string Snd_Win_Notification_Looping_Alarm10;
                public string Snd_Win_Notification_Looping_Alarm2;
                public string Snd_Win_Notification_Looping_Alarm3;
                public string Snd_Win_Notification_Looping_Alarm4;
                public string Snd_Win_Notification_Looping_Alarm5;
                public string Snd_Win_Notification_Looping_Alarm6;
                public string Snd_Win_Notification_Looping_Alarm7;
                public string Snd_Win_Notification_Looping_Alarm8;
                public string Snd_Win_Notification_Looping_Alarm9;
                public string Snd_Win_Notification_Looping_Call;
                public string Snd_Win_Notification_Looping_Call10;
                public string Snd_Win_Notification_Looping_Call2;
                public string Snd_Win_Notification_Looping_Call3;
                public string Snd_Win_Notification_Looping_Call4;
                public string Snd_Win_Notification_Looping_Call5;
                public string Snd_Win_Notification_Looping_Call6;
                public string Snd_Win_Notification_Looping_Call7;
                public string Snd_Win_Notification_Looping_Call8;
                public string Snd_Win_Notification_Looping_Call9;
                public string Snd_Win_Notification_Mail;
                public string Snd_Win_Notification_Proximity;
                public string Snd_Win_Notification_Reminder;
                public string Snd_Win_Notification_SMS;
                public string Snd_Win_Open;
                public string Snd_Win_PrintComplete;
                public string Snd_Win_ProximityConnection;
                public string Snd_Win_RestoreDown;
                public string Snd_Win_RestoreUp;
                public string Snd_Win_ShowBand;
                public string Snd_Win_SystemAsterisk;
                public string Snd_Win_SystemExclamation;
                public string Snd_Win_SystemExit;
                public string Snd_Win_SystemStart;
                public string Snd_Imageres_SystemStart;
                public string Snd_Win_SystemHand;
                public string Snd_Win_SystemNotification;
                public string Snd_Win_SystemQuestion;
                public string Snd_Win_WindowsLogoff;
                public string Snd_Win_WindowsLogon;
                public string Snd_Win_WindowsUAC;
                public string Snd_Win_WindowsUnlock;
                public string Snd_Explorer_ActivatingDocument;
                public string Snd_Explorer_BlockedPopup;
                public string Snd_Explorer_EmptyRecycleBin;
                public string Snd_Explorer_FeedDiscovered;
                public string Snd_Explorer_MoveMenuItem;
                public string Snd_Explorer_Navigating;
                public string Snd_Explorer_SecurityBand;
                public string Snd_Explorer_SearchProviderDiscovered;
                public string Snd_Explorer_FaxError;
                public string Snd_Explorer_FaxLineRings;
                public string Snd_Explorer_FaxNew;
                public string Snd_Explorer_FaxSent;
                public string Snd_NetMeeting_PersonJoins;
                public string Snd_NetMeeting_PersonLeaves;
                public string Snd_NetMeeting_ReceiveCall;
                public string Snd_NetMeeting_ReceiveRequestToJoin;
                public string Snd_SpeechRec_DisNumbersSound;
                public string Snd_SpeechRec_HubOffSound;
                public string Snd_SpeechRec_HubOnSound;
                public string Snd_SpeechRec_HubSleepSound;
                public string Snd_SpeechRec_MisrecoSound;
                public string Snd_SpeechRec_PanelSound;

                public bool Snd_Win_SystemExit_TaskMgmt;
                public bool Snd_Win_WindowsLogoff_TaskMgmt;
                public bool Snd_Win_WindowsLogon_TaskMgmt;
                public bool Snd_Win_WindowsUnlock_TaskMgmt;
                public string Snd_ChargerConnected;

                public void Load(Sounds _DefSounds)
                {
                    Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "", _DefSounds.Enabled));
                    Snd_Imageres_SystemStart = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Imageres.dll_Startup", _DefSounds.Snd_Imageres_SystemStart).ToString();
                    Snd_ChargerConnected = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_ChargerConnected", _DefSounds.Snd_ChargerConnected).ToString();

                    Snd_Win_SystemExit_TaskMgmt = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_SystemExit_TaskMgmt", _DefSounds.Snd_Win_SystemExit_TaskMgmt));
                    Snd_Win_WindowsLogoff_TaskMgmt = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_WindowsLogoff_TaskMgmt", _DefSounds.Snd_Win_WindowsLogoff_TaskMgmt));
                    Snd_Win_WindowsLogon_TaskMgmt = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_WindowsLogon_TaskMgmt", _DefSounds.Snd_Win_WindowsLogon_TaskMgmt));
                    Snd_Win_WindowsUnlock_TaskMgmt = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_WindowsUnlock_TaskMgmt", _DefSounds.Snd_Win_WindowsUnlock_TaskMgmt));

                    string Scope_Win = @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\.Default\{0}\.Current";
                    Snd_Win_Default = GetReg(string.Format(Scope_Win, ".Default"), "", _DefSounds.Snd_Win_Default).ToString();
                    Snd_Win_AppGPFault = GetReg(string.Format(Scope_Win, "AppGPFault"), "", _DefSounds.Snd_Win_AppGPFault).ToString();
                    Snd_Win_CCSelect = GetReg(string.Format(Scope_Win, "CCSelect"), "", _DefSounds.Snd_Win_CCSelect).ToString();
                    Snd_Win_ChangeTheme = GetReg(string.Format(Scope_Win, "ChangeTheme"), "", _DefSounds.Snd_Win_ChangeTheme).ToString();
                    Snd_Win_Close = GetReg(string.Format(Scope_Win, "Close"), "", _DefSounds.Snd_Win_Close).ToString();
                    Snd_Win_CriticalBatteryAlarm = GetReg(string.Format(Scope_Win, "CriticalBatteryAlarm"), "", _DefSounds.Snd_Win_CriticalBatteryAlarm).ToString();
                    Snd_Win_DeviceConnect = GetReg(string.Format(Scope_Win, "DeviceConnect"), "", _DefSounds.Snd_Win_DeviceConnect).ToString();
                    Snd_Win_DeviceDisconnect = GetReg(string.Format(Scope_Win, "DeviceDisconnect"), "", _DefSounds.Snd_Win_DeviceDisconnect).ToString();
                    Snd_Win_DeviceFail = GetReg(string.Format(Scope_Win, "DeviceFail"), "", _DefSounds.Snd_Win_DeviceFail).ToString();
                    Snd_Win_FaxBeep = GetReg(string.Format(Scope_Win, "FaxBeep"), "", _DefSounds.Snd_Win_FaxBeep).ToString();
                    Snd_Win_LowBatteryAlarm = GetReg(string.Format(Scope_Win, "LowBatteryAlarm"), "", _DefSounds.Snd_Win_LowBatteryAlarm).ToString();
                    Snd_Win_MailBeep = GetReg(string.Format(Scope_Win, "MailBeep"), "", _DefSounds.Snd_Win_MailBeep).ToString();
                    Snd_Win_Maximize = GetReg(string.Format(Scope_Win, "Maximize"), "", _DefSounds.Snd_Win_Maximize).ToString();
                    Snd_Win_MenuCommand = GetReg(string.Format(Scope_Win, "MenuCommand"), "", _DefSounds.Snd_Win_MenuCommand).ToString();
                    Snd_Win_MenuPopup = GetReg(string.Format(Scope_Win, "MenuPopup"), "", _DefSounds.Snd_Win_MenuPopup).ToString();
                    Snd_Win_MessageNudge = GetReg(string.Format(Scope_Win, "MessageNudge"), "", _DefSounds.Snd_Win_MessageNudge).ToString();
                    Snd_Win_Minimize = GetReg(string.Format(Scope_Win, "Minimize"), "", _DefSounds.Snd_Win_Minimize).ToString();
                    Snd_Win_Notification_Default = GetReg(string.Format(Scope_Win, "Notification.Default"), "", _DefSounds.Snd_Win_Notification_Default).ToString();
                    Snd_Win_Notification_IM = GetReg(string.Format(Scope_Win, "Notification.IM"), "", _DefSounds.Snd_Win_Notification_IM).ToString();
                    Snd_Win_Notification_Looping_Alarm = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm).ToString();
                    Snd_Win_Notification_Looping_Alarm2 = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm2"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm2).ToString();
                    Snd_Win_Notification_Looping_Alarm3 = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm3"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm3).ToString();
                    Snd_Win_Notification_Looping_Alarm4 = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm4"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm4).ToString();
                    Snd_Win_Notification_Looping_Alarm5 = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm5"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm5).ToString();
                    Snd_Win_Notification_Looping_Alarm6 = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm6"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm6).ToString();
                    Snd_Win_Notification_Looping_Alarm7 = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm7"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm7).ToString();
                    Snd_Win_Notification_Looping_Alarm8 = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm8"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm8).ToString();
                    Snd_Win_Notification_Looping_Alarm9 = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm9"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm9).ToString();
                    Snd_Win_Notification_Looping_Alarm10 = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm10"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm10).ToString();
                    Snd_Win_Notification_Looping_Call = GetReg(string.Format(Scope_Win, "Notification.Looping.Call"), "", _DefSounds.Snd_Win_Notification_Looping_Call).ToString();
                    Snd_Win_Notification_Looping_Call2 = GetReg(string.Format(Scope_Win, "Notification.Looping.Call2"), "", _DefSounds.Snd_Win_Notification_Looping_Call2).ToString();
                    Snd_Win_Notification_Looping_Call3 = GetReg(string.Format(Scope_Win, "Notification.Looping.Call3"), "", _DefSounds.Snd_Win_Notification_Looping_Call3).ToString();
                    Snd_Win_Notification_Looping_Call4 = GetReg(string.Format(Scope_Win, "Notification.Looping.Call4"), "", _DefSounds.Snd_Win_Notification_Looping_Call4).ToString();
                    Snd_Win_Notification_Looping_Call5 = GetReg(string.Format(Scope_Win, "Notification.Looping.Call5"), "", _DefSounds.Snd_Win_Notification_Looping_Call5).ToString();
                    Snd_Win_Notification_Looping_Call6 = GetReg(string.Format(Scope_Win, "Notification.Looping.Call6"), "", _DefSounds.Snd_Win_Notification_Looping_Call6).ToString();
                    Snd_Win_Notification_Looping_Call7 = GetReg(string.Format(Scope_Win, "Notification.Looping.Call7"), "", _DefSounds.Snd_Win_Notification_Looping_Call7).ToString();
                    Snd_Win_Notification_Looping_Call8 = GetReg(string.Format(Scope_Win, "Notification.Looping.Call8"), "", _DefSounds.Snd_Win_Notification_Looping_Call8).ToString();
                    Snd_Win_Notification_Looping_Call9 = GetReg(string.Format(Scope_Win, "Notification.Looping.Call9"), "", _DefSounds.Snd_Win_Notification_Looping_Call9).ToString();
                    Snd_Win_Notification_Looping_Call10 = GetReg(string.Format(Scope_Win, "Notification.Looping.Call10"), "", _DefSounds.Snd_Win_Notification_Looping_Call10).ToString();
                    Snd_Win_Notification_Mail = GetReg(string.Format(Scope_Win, "Notification.Mail"), "", _DefSounds.Snd_Win_Notification_Mail).ToString();
                    Snd_Win_Notification_Proximity = GetReg(string.Format(Scope_Win, "Notification.Proximity"), "", _DefSounds.Snd_Win_Notification_Proximity).ToString();
                    Snd_Win_Notification_Reminder = GetReg(string.Format(Scope_Win, "Notification.Reminder"), "", _DefSounds.Snd_Win_Notification_Reminder).ToString();
                    Snd_Win_Notification_SMS = GetReg(string.Format(Scope_Win, "Notification.SMS"), "", _DefSounds.Snd_Win_Notification_SMS).ToString();
                    Snd_Win_Open = GetReg(string.Format(Scope_Win, "Open"), "", _DefSounds.Snd_Win_Open).ToString();
                    Snd_Win_PrintComplete = GetReg(string.Format(Scope_Win, "PrintComplete"), "", _DefSounds.Snd_Win_PrintComplete).ToString();
                    Snd_Win_ProximityConnection = GetReg(string.Format(Scope_Win, "ProximityConnection"), "", _DefSounds.Snd_Win_ProximityConnection).ToString();
                    Snd_Win_RestoreDown = GetReg(string.Format(Scope_Win, "RestoreDown"), "", _DefSounds.Snd_Win_RestoreDown).ToString();
                    Snd_Win_RestoreUp = GetReg(string.Format(Scope_Win, "RestoreUp"), "", _DefSounds.Snd_Win_RestoreUp).ToString();
                    Snd_Win_ShowBand = GetReg(string.Format(Scope_Win, "ShowBand"), "", _DefSounds.Snd_Win_ShowBand).ToString();
                    Snd_Win_SystemAsterisk = GetReg(string.Format(Scope_Win, "SystemAsterisk"), "", _DefSounds.Snd_Win_SystemAsterisk).ToString();
                    Snd_Win_SystemExclamation = GetReg(string.Format(Scope_Win, "SystemExclamation"), "", _DefSounds.Snd_Win_SystemExclamation).ToString();
                    Snd_Win_SystemExit = GetReg(string.Format(Scope_Win, "SystemExit"), "", _DefSounds.Snd_Win_SystemExit).ToString();
                    Snd_Win_SystemStart = GetReg(string.Format(Scope_Win, "SystemStart"), "", _DefSounds.Snd_Win_SystemStart).ToString();
                    Snd_Win_SystemHand = GetReg(string.Format(Scope_Win, "SystemHand"), "", _DefSounds.Snd_Win_SystemHand).ToString();
                    Snd_Win_SystemNotification = GetReg(string.Format(Scope_Win, "SystemNotification"), "", _DefSounds.Snd_Win_SystemNotification).ToString();
                    Snd_Win_SystemQuestion = GetReg(string.Format(Scope_Win, "SystemQuestion"), "", _DefSounds.Snd_Win_SystemQuestion).ToString();
                    Snd_Win_WindowsLogoff = GetReg(string.Format(Scope_Win, "WindowsLogoff"), "", _DefSounds.Snd_Win_WindowsLogoff).ToString();
                    Snd_Win_WindowsLogon = GetReg(string.Format(Scope_Win, "WindowsLogon"), "", _DefSounds.Snd_Win_WindowsLogon).ToString();
                    Snd_Win_WindowsUAC = GetReg(string.Format(Scope_Win, "WindowsUAC"), "", _DefSounds.Snd_Win_WindowsUAC).ToString();
                    Snd_Win_WindowsUnlock = GetReg(string.Format(Scope_Win, "WindowsUnlock"), "", _DefSounds.Snd_Win_WindowsUnlock).ToString();

                    string Scope_Explorer = @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\Explorer\{0}\.Current";
                    Snd_Explorer_ActivatingDocument = GetReg(string.Format(Scope_Explorer, "ActivatingDocument"), "", _DefSounds.Snd_Explorer_ActivatingDocument).ToString();
                    Snd_Explorer_BlockedPopup = GetReg(string.Format(Scope_Explorer, "BlockedPopup"), "", _DefSounds.Snd_Explorer_BlockedPopup).ToString();
                    Snd_Explorer_EmptyRecycleBin = GetReg(string.Format(Scope_Explorer, "EmptyRecycleBin"), "", _DefSounds.Snd_Explorer_EmptyRecycleBin).ToString();
                    Snd_Explorer_FeedDiscovered = GetReg(string.Format(Scope_Explorer, "FeedDiscovered"), "", _DefSounds.Snd_Explorer_FeedDiscovered).ToString();
                    Snd_Explorer_MoveMenuItem = GetReg(string.Format(Scope_Explorer, "MoveMenuItem"), "", _DefSounds.Snd_Explorer_MoveMenuItem).ToString();
                    Snd_Explorer_Navigating = GetReg(string.Format(Scope_Explorer, "Navigating"), "", _DefSounds.Snd_Explorer_Navigating).ToString();
                    Snd_Explorer_SecurityBand = GetReg(string.Format(Scope_Explorer, "SecurityBand"), "", _DefSounds.Snd_Explorer_SecurityBand).ToString();
                    Snd_Explorer_SearchProviderDiscovered = GetReg(string.Format(Scope_Explorer, "SearchProviderDiscovered"), "", _DefSounds.Snd_Explorer_SearchProviderDiscovered).ToString();
                    Snd_Explorer_FaxError = GetReg(string.Format(Scope_Explorer, "FaxError"), "", _DefSounds.Snd_Explorer_FaxError).ToString();
                    Snd_Explorer_FaxLineRings = GetReg(string.Format(Scope_Explorer, "FaxLineRings"), "", _DefSounds.Snd_Explorer_FaxLineRings).ToString();
                    Snd_Explorer_FaxNew = GetReg(string.Format(Scope_Explorer, "FaxNew"), "", _DefSounds.Snd_Explorer_FaxNew).ToString();
                    Snd_Explorer_FaxSent = GetReg(string.Format(Scope_Explorer, "FaxSent"), "", _DefSounds.Snd_Explorer_FaxSent).ToString();

                    string Scope_NetMeeting = @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\Conf\{0}\.Current";
                    Snd_NetMeeting_PersonJoins = GetReg(string.Format(Scope_NetMeeting, "Person Joins"), "", _DefSounds.Snd_NetMeeting_PersonJoins).ToString();
                    Snd_NetMeeting_PersonLeaves = GetReg(string.Format(Scope_NetMeeting, "Person Leaves"), "", _DefSounds.Snd_NetMeeting_PersonLeaves).ToString();
                    Snd_NetMeeting_ReceiveCall = GetReg(string.Format(Scope_NetMeeting, "Receive Call"), "", _DefSounds.Snd_NetMeeting_ReceiveCall).ToString();
                    Snd_NetMeeting_ReceiveRequestToJoin = GetReg(string.Format(Scope_NetMeeting, "Receive Request to Join"), "", _DefSounds.Snd_NetMeeting_ReceiveRequestToJoin).ToString();

                    string Scope_SpeechRec = @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\sapisvr\{0}\.current";
                    Snd_SpeechRec_DisNumbersSound = GetReg(string.Format(Scope_SpeechRec, "DisNumbersSound"), "", _DefSounds.Snd_SpeechRec_DisNumbersSound).ToString();
                    Snd_SpeechRec_HubOffSound = GetReg(string.Format(Scope_SpeechRec, "HubOffSound"), "", _DefSounds.Snd_SpeechRec_HubOffSound).ToString();
                    Snd_SpeechRec_HubOnSound = GetReg(string.Format(Scope_SpeechRec, "HubOnSound"), "", _DefSounds.Snd_SpeechRec_HubOnSound).ToString();
                    Snd_SpeechRec_HubSleepSound = GetReg(string.Format(Scope_SpeechRec, "HubSleepSound"), "", _DefSounds.Snd_SpeechRec_HubSleepSound).ToString();
                    Snd_SpeechRec_MisrecoSound = GetReg(string.Format(Scope_SpeechRec, "MisrecoSound"), "", _DefSounds.Snd_SpeechRec_MisrecoSound).ToString();
                    Snd_SpeechRec_PanelSound = GetReg(string.Format(Scope_SpeechRec, "PanelSound"), "", _DefSounds.Snd_SpeechRec_PanelSound).ToString();

                }

                public void Apply(TreeView TreeView = null)
                {
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "", Enabled);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Imageres.dll_Startup", Snd_Imageres_SystemStart, RegistryValueKind.String);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_ChargerConnected", Snd_ChargerConnected, RegistryValueKind.String);

                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_SystemExit_TaskMgmt", Snd_Win_SystemExit_TaskMgmt);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_WindowsLogoff_TaskMgmt", Snd_Win_WindowsLogoff_TaskMgmt);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_WindowsLogon_TaskMgmt", Snd_Win_WindowsLogon_TaskMgmt);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_WindowsUnlock_TaskMgmt", Snd_Win_WindowsUnlock_TaskMgmt);

                    if (Enabled)
                    {
                        string[] destination_StartupSnd = new[] { @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\System", @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\BootAnimation" };

                        if (string.IsNullOrWhiteSpace(Snd_Imageres_SystemStart))
                        {
                            EditReg_CMD(TreeView, destination_StartupSnd[0], "DisableStartupSound", 1);
                            EditReg_CMD(TreeView, destination_StartupSnd[1], "DisableStartupSound", 1);
                        }

                        else if (File.Exists(Snd_Imageres_SystemStart))
                        {
                            EditReg_CMD(TreeView, destination_StartupSnd[0], "DisableStartupSound", 0);
                            EditReg_CMD(TreeView, destination_StartupSnd[1], "DisableStartupSound", 0);
                        }

                        else if (Snd_Imageres_SystemStart.Trim().ToUpper() == "DEFAULT")
                        {
                            EditReg_CMD(TreeView, destination_StartupSnd[0], "DisableStartupSound", 0);
                            EditReg_CMD(TreeView, destination_StartupSnd[1], "DisableStartupSound", 0);
                        }

                        else if (!(Snd_Imageres_SystemStart.Trim().ToUpper() == "CURRENT"))
                        {
                            EditReg_CMD(TreeView, destination_StartupSnd[0], "DisableStartupSound", (!My.Env.W11).ToInteger());
                            EditReg_CMD(TreeView, destination_StartupSnd[1], "DisableStartupSound", (!My.Env.W11).ToInteger());
                        }

                        else
                        {
                            EditReg_CMD(TreeView, destination_StartupSnd[0], "DisableStartupSound", 1);
                            EditReg_CMD(TreeView, destination_StartupSnd[1], "DisableStartupSound", 1);

                        }

                        if (!My.Env.WXP)
                        {

                            if (File.Exists(Snd_Imageres_SystemStart) && Path.GetExtension(Snd_Imageres_SystemStart).ToUpper() == ".WAV")
                            {

                                byte[] CurrentSoundBytes = PE.GetResource(My.Env.PATH_imageres, "WAVE", My.Env.WVista ? 5051 : 5080);
                                byte[] TargetSoundBytes = File.ReadAllBytes(Snd_Imageres_SystemStart);

                                if (!CurrentSoundBytes.Equals(TargetSoundBytes))
                                {
                                    PE.ReplaceResource(TreeView, My.Env.PATH_imageres, "WAVE", My.Env.WVista ? 5051 : 5080, TargetSoundBytes);
                                }
                            }

                            else if (Snd_Imageres_SystemStart.Trim().ToUpper() == "DEFAULT")
                            {

                                byte[] CurrentSoundBytes = PE.GetResource(My.Env.PATH_imageres, "WAVE", My.Env.WVista ? 5051 : 5080);
                                byte[] OriginalSoundBytes = File.ReadAllBytes(My.Env.PATH_appData + @"\WindowsStartup_Backup.wav");

                                if (!CurrentSoundBytes.Equals(OriginalSoundBytes))
                                {
                                    PE.ReplaceResource(TreeView, My.Env.PATH_imageres, "WAVE", My.Env.WVista ? 5051 : 5080, OriginalSoundBytes);
                                }

                                if (My.Env.Settings.ThemeApplyingBehavior.SFC_on_restoring_StartupSound)
                                    SFC(My.Env.PATH_imageres);

                            }

                        }

                        if (My.Env.W8 | My.Env.W81 | My.Env.W10 | My.Env.W11 | My.Env.W12)
                        {
                            if (Snd_Win_SystemExit_TaskMgmt && File.Exists(Snd_Win_SystemExit) && Path.GetExtension(Snd_Win_SystemExit).ToUpper() == ".WAV")
                            {
                                TaskMgmt(TaskType.Shutdown, Actions.Add, Snd_Win_SystemExit, TreeView);
                            }
                            else
                            {
                                TaskMgmt(TaskType.Shutdown, Actions.Delete, "", TreeView);
                            }

                            if (Snd_Win_WindowsLogoff_TaskMgmt && File.Exists(Snd_Win_WindowsLogoff) && Path.GetExtension(Snd_Win_WindowsLogoff).ToUpper() == ".WAV")
                            {
                                TaskMgmt(TaskType.Logoff, Actions.Add, Snd_Win_WindowsLogoff, TreeView);
                            }
                            else
                            {
                                TaskMgmt(TaskType.Logoff, Actions.Delete, "", TreeView);
                            }

                            if (Snd_Win_WindowsLogon_TaskMgmt && File.Exists(Snd_Win_WindowsLogon) && Path.GetExtension(Snd_Win_WindowsLogon).ToUpper() == ".WAV")
                            {
                                TaskMgmt(TaskType.Logon, Actions.Add, Snd_Win_WindowsLogon, TreeView);
                            }
                            else
                            {
                                TaskMgmt(TaskType.Logon, Actions.Delete, "", TreeView);
                            }

                            if (Snd_Win_WindowsUnlock_TaskMgmt && File.Exists(Snd_Win_WindowsUnlock) && Path.GetExtension(Snd_Win_WindowsUnlock).ToUpper() == ".WAV")
                            {
                                TaskMgmt(TaskType.Unlock, Actions.Add, Snd_Win_WindowsUnlock, TreeView);
                            }
                            else
                            {
                                TaskMgmt(TaskType.Unlock, Actions.Delete, "", TreeView);
                            }
                        }

                        if (File.Exists(Snd_ChargerConnected) && Path.GetExtension(Snd_ChargerConnected).ToUpper() == ".WAV")
                        {
                            TaskMgmt(TaskType.ChargerConnected, Actions.Add, Snd_ChargerConnected, TreeView);
                        }
                        else
                        {
                            TaskMgmt(TaskType.ChargerConnected, Actions.Delete, "", TreeView);
                        }

                        string[] Scope_Win = new[] { @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\.Default\{0}\.Current", @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\.Default\{0}\.Modified" };
                        string[] Scope_Explorer = new[] { @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\Explorer\{0}\.Current", @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\Explorer\{0}\.Modified" };
                        string[] Scope_SpeechRec = new[] { @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\sapisvr\{0}\.Current", @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\sapisvr\{0}\.Modified" };
                        string[] Scope_NetMeeting = new[] { @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\Conf\{0}\.Current", @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\Conf\{0}\.Modified" };

                        foreach (string Scope in Scope_Win)
                        {
                            EditReg(TreeView, string.Format(Scope, ".Default"), "", Snd_Win_Default, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "AppGPFault"), "", Snd_Win_AppGPFault, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "CCSelect"), "", Snd_Win_CCSelect, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "ChangeTheme"), "", Snd_Win_ChangeTheme, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Close"), "", Snd_Win_Close, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "CriticalBatteryAlarm"), "", Snd_Win_CriticalBatteryAlarm, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "DeviceConnect"), "", Snd_Win_DeviceConnect, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "DeviceDisconnect"), "", Snd_Win_DeviceDisconnect, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "DeviceFail"), "", Snd_Win_DeviceFail, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "FaxBeep"), "", Snd_Win_FaxBeep, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "LowBatteryAlarm"), "", Snd_Win_LowBatteryAlarm, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "MailBeep"), "", Snd_Win_MailBeep, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Maximize"), "", Snd_Win_Maximize, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "MenuCommand"), "", Snd_Win_MenuCommand, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "MenuPopup"), "", Snd_Win_MenuPopup, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "MessageNudge"), "", Snd_Win_MessageNudge, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Minimize"), "", Snd_Win_Minimize, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Notification.Default"), "", Snd_Win_Notification_Default, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Notification.IM"), "", Snd_Win_Notification_IM, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Notification.Looping.Alarm"), "", Snd_Win_Notification_Looping_Alarm, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Notification.Looping.Alarm2"), "", Snd_Win_Notification_Looping_Alarm2, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Notification.Looping.Alarm3"), "", Snd_Win_Notification_Looping_Alarm3, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Notification.Looping.Alarm4"), "", Snd_Win_Notification_Looping_Alarm4, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Notification.Looping.Alarm5"), "", Snd_Win_Notification_Looping_Alarm5, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Notification.Looping.Alarm6"), "", Snd_Win_Notification_Looping_Alarm6, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Notification.Looping.Alarm7"), "", Snd_Win_Notification_Looping_Alarm7, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Notification.Looping.Alarm8"), "", Snd_Win_Notification_Looping_Alarm8, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Notification.Looping.Alarm9"), "", Snd_Win_Notification_Looping_Alarm9, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Notification.Looping.Alarm10"), "", Snd_Win_Notification_Looping_Alarm10, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Notification.Looping.Call"), "", Snd_Win_Notification_Looping_Call, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Notification.Looping.Call2"), "", Snd_Win_Notification_Looping_Call2, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Notification.Looping.Call3"), "", Snd_Win_Notification_Looping_Call3, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Notification.Looping.Call4"), "", Snd_Win_Notification_Looping_Call4, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Notification.Looping.Call5"), "", Snd_Win_Notification_Looping_Call5, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Notification.Looping.Call6"), "", Snd_Win_Notification_Looping_Call6, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Notification.Looping.Call7"), "", Snd_Win_Notification_Looping_Call7, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Notification.Looping.Call8"), "", Snd_Win_Notification_Looping_Call8, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Notification.Looping.Call9"), "", Snd_Win_Notification_Looping_Call9, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Notification.Looping.Call10"), "", Snd_Win_Notification_Looping_Call10, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Notification.Mail"), "", Snd_Win_Notification_Mail, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Notification.Proximity"), "", Snd_Win_Notification_Proximity, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Notification.Reminder"), "", Snd_Win_Notification_Reminder, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Notification.SMS"), "", Snd_Win_Notification_SMS, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Open"), "", Snd_Win_Open, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "PrintComplete"), "", Snd_Win_PrintComplete, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "ProximityConnection"), "", Snd_Win_ProximityConnection, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "RestoreDown"), "", Snd_Win_RestoreDown, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "RestoreUp"), "", Snd_Win_RestoreUp, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "ShowBand"), "", Snd_Win_ShowBand, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "SystemAsterisk"), "", Snd_Win_SystemAsterisk, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "SystemExclamation"), "", Snd_Win_SystemExclamation, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "SystemExit"), "", Snd_Win_SystemExit, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "SystemStart"), "", Snd_Win_SystemStart, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "SystemHand"), "", Snd_Win_SystemHand, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "SystemNotification"), "", Snd_Win_SystemNotification, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "SystemQuestion"), "", Snd_Win_SystemQuestion, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "WindowsLogoff"), "", Snd_Win_WindowsLogoff, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "WindowsLogon"), "", Snd_Win_WindowsLogon, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "WindowsUAC"), "", Snd_Win_WindowsUAC, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "WindowsUnlock"), "", Snd_Win_WindowsUnlock, RegistryValueKind.String);
                        }

                        foreach (string Scope in Scope_Explorer)
                        {
                            EditReg(TreeView, string.Format(Scope, "ActivatingDocument"), "", Snd_Explorer_ActivatingDocument, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "BlockedPopup"), "", Snd_Explorer_BlockedPopup, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "EmptyRecycleBin"), "", Snd_Explorer_EmptyRecycleBin, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "FeedDiscovered"), "", Snd_Explorer_FeedDiscovered, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "MoveMenuItem"), "", Snd_Explorer_MoveMenuItem, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Navigating"), "", Snd_Explorer_Navigating, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "SecurityBand"), "", Snd_Explorer_SecurityBand, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "SearchProviderDiscovered"), "", Snd_Explorer_SearchProviderDiscovered, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "FaxError"), "", Snd_Explorer_FaxError, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "FaxLineRings"), "", Snd_Explorer_FaxLineRings, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "FaxNew"), "", Snd_Explorer_FaxNew, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "FaxSent"), "", Snd_Explorer_FaxSent, RegistryValueKind.String);
                        }

                        foreach (string Scope in Scope_NetMeeting)
                        {
                            EditReg(TreeView, string.Format(Scope, "Person Joins"), "", Snd_NetMeeting_PersonJoins, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Person Leaves"), "", Snd_NetMeeting_PersonLeaves, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Receive Call"), "", Snd_NetMeeting_ReceiveCall, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "Receive Request to Join"), "", Snd_NetMeeting_ReceiveRequestToJoin, RegistryValueKind.String);
                        }

                        foreach (string Scope in Scope_SpeechRec)
                        {
                            EditReg(TreeView, string.Format(Scope, "DisNumbersSound"), "", Snd_SpeechRec_DisNumbersSound, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "HubOffSound"), "", Snd_SpeechRec_HubOffSound, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "HubOnSound"), "", Snd_SpeechRec_HubOnSound, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "HubSleepSound"), "", Snd_SpeechRec_HubSleepSound, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "MisrecoSound"), "", Snd_SpeechRec_MisrecoSound, RegistryValueKind.String);
                            EditReg(TreeView, string.Format(Scope, "PanelSound"), "", Snd_SpeechRec_PanelSound, RegistryValueKind.String);
                        }

                    }

                }

                public static bool operator ==(Sounds First, Sounds Second)
                {
                    return First.Equals(Second);
                }

                public static bool operator !=(Sounds First, Sounds Second)
                {
                    return !First.Equals(Second);
                }
                public object Clone()
                {
                    return MemberwiseClone();
                }

                public void Dispose()
                {
                    GC.Collect();
                    GC.SuppressFinalize(this);
                }
            }
            public struct AltTab : ICloneable
            {
                public bool Enabled;
                public Styles Style;
                public int Win10Opacity;

                public enum Styles
                {
                    Default,
                    ClassicNT,
                    Placeholder,
                    EP_Win10
                }

                public void Load(AltTab _DefAltTab)
                {
                    Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\AltTab", "", _DefAltTab.Enabled));
                    Style = (Styles)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer", "AltTabSettings", _DefAltTab.Style));
                    Win10Opacity = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MultitaskingView\AltTabViewHost", "Grid_backgroundPercent", _DefAltTab.Win10Opacity));
                    if (Win10Opacity == default)
                        Win10Opacity = _DefAltTab.Win10Opacity;
                }

                public void Apply(TreeView TreeView = null)
                {
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\AltTab", "", Enabled);

                    if (Enabled)
                    {
                        EditReg(TreeView, @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer", "AltTabSettings", Style);
                        EditReg(TreeView, @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MultitaskingView\AltTabViewHost", "Grid_backgroundPercent", Win10Opacity);
                    }
                }

                public static bool operator ==(AltTab First, AltTab Second)
                {
                    return First.Equals(Second);
                }

                public static bool operator !=(AltTab First, AltTab Second)
                {
                    return !First.Equals(Second);
                }

                public object Clone()
                {
                    return MemberwiseClone();
                }
            }
            public struct Console : ICloneable
            {
                public bool Enabled;
                public Color ColorTable00;
                public Color ColorTable01;
                public Color ColorTable02;
                public Color ColorTable03;
                public Color ColorTable04;
                public Color ColorTable05;
                public Color ColorTable06;
                public Color ColorTable07;
                public Color ColorTable08;
                public Color ColorTable09;
                public Color ColorTable10;
                public Color ColorTable11;
                public Color ColorTable12;
                public Color ColorTable13;
                public Color ColorTable14;
                public Color ColorTable15;
                public int PopupForeground;
                public int PopupBackground;
                public int ScreenColorsForeground;
                public int ScreenColorsBackground;
                public int CursorSize;
                public string FaceName;
                public bool FontRaster;
                public int FontSize;
                public int FontWeight;
                public int W10_1909_CursorType;
                public Color W10_1909_CursorColor;
                public bool W10_1909_ForceV2;
                public bool W10_1909_LineSelection;
                public bool W10_1909_TerminalScrolling;
                public int W10_1909_WindowAlpha;

                public void Load(string RegKey, string Signature_Of_Enable, Console Defaults)
                {
                    object temp;
                    string RegAddress = @"HKEY_CURRENT_USER\Console" + (string.IsNullOrEmpty(RegKey) ? "" : @"\" + RegKey);

                    temp = GetReg(RegAddress, "ColorTable00", Defaults.ColorTable00.Reverse().ToArgb());
                    ColorTable00 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

                    temp = GetReg(RegAddress, "ColorTable01", Defaults.ColorTable01.Reverse().ToArgb());
                    ColorTable01 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

                    temp = GetReg(RegAddress, "ColorTable02", Defaults.ColorTable02.Reverse().ToArgb());
                    ColorTable02 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

                    temp = GetReg(RegAddress, "ColorTable03", Defaults.ColorTable03.Reverse().ToArgb());
                    ColorTable03 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

                    temp = GetReg(RegAddress, "ColorTable04", Defaults.ColorTable04.Reverse().ToArgb());
                    ColorTable04 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

                    temp = GetReg(RegAddress, "ColorTable05", Defaults.ColorTable05.Reverse().ToArgb());
                    ColorTable05 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

                    temp = GetReg(RegAddress, "ColorTable06", Defaults.ColorTable06.Reverse().ToArgb());
                    ColorTable06 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

                    temp = GetReg(RegAddress, "ColorTable07", Defaults.ColorTable07.Reverse().ToArgb());
                    ColorTable07 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

                    temp = GetReg(RegAddress, "ColorTable08", Defaults.ColorTable08.Reverse().ToArgb());
                    ColorTable08 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

                    temp = GetReg(RegAddress, "ColorTable09", Defaults.ColorTable09.Reverse().ToArgb());
                    ColorTable09 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

                    temp = GetReg(RegAddress, "ColorTable10", Defaults.ColorTable10.Reverse().ToArgb());
                    ColorTable10 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

                    temp = GetReg(RegAddress, "ColorTable11", Defaults.ColorTable11.Reverse().ToArgb());
                    ColorTable11 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

                    temp = GetReg(RegAddress, "ColorTable12", Defaults.ColorTable12.Reverse().ToArgb());
                    ColorTable12 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

                    temp = GetReg(RegAddress, "ColorTable13", Defaults.ColorTable13.Reverse().ToArgb());
                    ColorTable13 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

                    temp = GetReg(RegAddress, "ColorTable14", Defaults.ColorTable14.Reverse().ToArgb());
                    ColorTable14 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

                    temp = GetReg(RegAddress, "ColorTable15", Defaults.ColorTable15.Reverse().ToArgb());
                    ColorTable15 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

                    temp = GetReg(RegAddress, "PopupColors", Convert.ToInt32(Defaults.PopupBackground.ToString("X") + Defaults.PopupForeground.ToString("X"), 16));
                    string d = ((int)temp).ToString("X");
                    if (d.Count() == 1)
                        d = 0 + d;
                    PopupBackground = Convert.ToInt32(d[0].ToString(), 16);
                    PopupForeground = Convert.ToInt32(d[1].ToString(), 16);

                    temp = GetReg(RegAddress, "ScreenColors", Convert.ToInt32(Defaults.ScreenColorsBackground.ToString("X") + Defaults.ScreenColorsForeground.ToString("X"), 16));
                    d = ((int)temp).ToString("X");
                    if (d.Count() == 1)
                        d = 0 + d;
                    ScreenColorsBackground = Convert.ToInt32(d[0].ToString(), 16);
                    ScreenColorsForeground = Convert.ToInt32(d[1].ToString(), 16);

                    CursorSize = Convert.ToInt32(GetReg(RegAddress, "CursorSize", 25));

                    temp = GetReg(RegAddress, "FaceName", Defaults.FaceName);
                    if (IsFontInstalled(temp.ToString()))
                    {
                        FaceName = temp.ToString();
                    }
                    else
                    {
                        FaceName = Defaults.FaceName;
                    }

                    temp = GetReg(RegAddress, "FontFamily", !Defaults.FontRaster ? 54 : 1);
                    FontRaster = ((int)temp == 1 | (int)temp == 0) | (int)temp == 48;
                    if (FaceName.ToLower() == "terminal")
                        FontRaster = true;

                    temp = GetReg(RegAddress, "FontSize", Defaults.FontSize);
                    if ((int)temp == 0 & !FontRaster)
                        FontSize = Defaults.FontSize;
                    else
                        FontSize = Convert.ToInt32(temp);

                    FontWeight = Convert.ToInt32(GetReg(RegAddress, "FontWeight", 400));


                    if (My.Env.W10_1909)
                    {
                        temp = GetReg(RegAddress, "CursorColor", Color.White.Reverse().ToArgb());
                        W10_1909_CursorColor = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());
                        W10_1909_CursorType = Convert.ToInt32(GetReg(RegAddress, "CursorType", 1));
                        W10_1909_ForceV2 = Convert.ToBoolean(GetReg(RegAddress, "ForceV2", true));
                        W10_1909_LineSelection = Convert.ToBoolean(GetReg(RegAddress, "LineSelection", false));
                        W10_1909_TerminalScrolling = Convert.ToBoolean(GetReg(RegAddress, "TerminalScrolling", false));
                        W10_1909_WindowAlpha = Convert.ToInt32(GetReg(RegAddress, "WindowAlpha", 255));
                    }

                    Enabled = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", Signature_Of_Enable, 0)).ToBoolean();

                }
                public static void Save_Console_To_Registry(string scopeReg, string RegKey, Console Console, TreeView TreeView = null)
                {

                    string RegAddress = scopeReg + @"\Console" + (string.IsNullOrEmpty(RegKey) ? "" : @"\" + RegKey);

                    try
                    {
                        if (scopeReg.ToUpper() == "HKEY_CURRENT_USER")
                        {
                            if (!string.IsNullOrEmpty(RegKey))
                                Registry.CurrentUser.CreateSubKey(@"Console\" + RegKey, true).Close();
                        }
                    }
                    catch
                    {
                    }

                    EditReg(TreeView, RegAddress, "EnableColorSelection", 1);
                    EditReg(TreeView, RegAddress, "ColorTable00", Color.FromArgb(0, Console.ColorTable00.Reverse()).ToArgb());
                    EditReg(TreeView, RegAddress, "ColorTable01", Color.FromArgb(0, Console.ColorTable01.Reverse()).ToArgb());
                    EditReg(TreeView, RegAddress, "ColorTable02", Color.FromArgb(0, Console.ColorTable02.Reverse()).ToArgb());
                    EditReg(TreeView, RegAddress, "ColorTable03", Color.FromArgb(0, Console.ColorTable03.Reverse()).ToArgb());
                    EditReg(TreeView, RegAddress, "ColorTable04", Color.FromArgb(0, Console.ColorTable04.Reverse()).ToArgb());
                    EditReg(TreeView, RegAddress, "ColorTable05", Color.FromArgb(0, Console.ColorTable05.Reverse()).ToArgb());
                    EditReg(TreeView, RegAddress, "ColorTable06", Color.FromArgb(0, Console.ColorTable06.Reverse()).ToArgb());
                    EditReg(TreeView, RegAddress, "ColorTable07", Color.FromArgb(0, Console.ColorTable07.Reverse()).ToArgb());
                    EditReg(TreeView, RegAddress, "ColorTable08", Color.FromArgb(0, Console.ColorTable08.Reverse()).ToArgb());
                    EditReg(TreeView, RegAddress, "ColorTable09", Color.FromArgb(0, Console.ColorTable09.Reverse()).ToArgb());
                    EditReg(TreeView, RegAddress, "ColorTable10", Color.FromArgb(0, Console.ColorTable10.Reverse()).ToArgb());
                    EditReg(TreeView, RegAddress, "ColorTable11", Color.FromArgb(0, Console.ColorTable11.Reverse()).ToArgb());
                    EditReg(TreeView, RegAddress, "ColorTable12", Color.FromArgb(0, Console.ColorTable12.Reverse()).ToArgb());
                    EditReg(TreeView, RegAddress, "ColorTable13", Color.FromArgb(0, Console.ColorTable13.Reverse()).ToArgb());
                    EditReg(TreeView, RegAddress, "ColorTable14", Color.FromArgb(0, Console.ColorTable14.Reverse()).ToArgb());
                    EditReg(TreeView, RegAddress, "ColorTable15", Color.FromArgb(0, Console.ColorTable15.Reverse()).ToArgb());
                    EditReg(TreeView, RegAddress, "PopupColors", Convert.ToInt32(Console.PopupBackground.ToString("X") + Console.PopupForeground.ToString("X"), 16));
                    EditReg(TreeView, RegAddress, "ScreenColors", Convert.ToInt32(Console.ScreenColorsBackground.ToString("X") + Console.ScreenColorsForeground.ToString("X"), 16));
                    EditReg(TreeView, RegAddress, "CursorSize", Console.CursorSize);

                    if (Console.FontRaster)
                    {
                        EditReg(TreeView, RegAddress, "FaceName", "Terminal", RegistryValueKind.String);
                        EditReg(TreeView, RegAddress, "FontFamily", 48);
                    }
                    else
                    {
                        EditReg(TreeView, RegAddress, "FaceName", Console.FaceName, RegistryValueKind.String);
                        EditReg(TreeView, RegAddress, "FontFamily", Console.FontRaster ? 1 : 54);
                    }

                    EditReg(TreeView, RegAddress, "FontSize", Console.FontSize);
                    EditReg(TreeView, RegAddress, "FontWeight", Console.FontWeight);

                    if (My.Env.W10_1909)
                    {
                        EditReg(TreeView, RegAddress, "CursorColor", Color.FromArgb(0, Console.W10_1909_CursorColor.Reverse()).ToArgb());
                        EditReg(TreeView, RegAddress, "CursorType", Console.W10_1909_CursorType);
                        EditReg(TreeView, RegAddress, "WindowAlpha", Console.W10_1909_WindowAlpha);
                        EditReg(TreeView, RegAddress, "ForceV2", Console.W10_1909_ForceV2.ToInteger());
                        EditReg(TreeView, RegAddress, "LineSelection", Console.W10_1909_LineSelection.ToInteger());
                        EditReg(TreeView, RegAddress, "TerminalScrolling", Console.W10_1909_TerminalScrolling.ToInteger());
                    }

                    EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Console\TrueTypeFont", "000", Console.FaceName, RegistryValueKind.String);

                }

                public static bool operator ==(Console First, Console Second)
                {
                    return First.Equals(Second);
                }

                public static bool operator !=(Console First, Console Second)
                {
                    return !First.Equals(Second);
                }

                public object Clone()
                {
                    return MemberwiseClone();
                }
            }
            public struct Cursor : ICloneable
            {
                public Paths.ArrowStyle ArrowStyle;
                public Paths.CircleStyle CircleStyle;
                public Color PrimaryColor1;
                public Color PrimaryColor2;
                public bool PrimaryColorGradient;
                public Paths.GradientMode PrimaryColorGradientMode;
                public bool PrimaryColorNoise;
                public float PrimaryColorNoiseOpacity;
                public Color SecondaryColor1;
                public Color SecondaryColor2;
                public bool SecondaryColorGradient;
                public Paths.GradientMode SecondaryColorGradientMode;
                public bool SecondaryColorNoise;
                public float SecondaryColorNoiseOpacity;
                public Color LoadingCircleBack1;
                public Color LoadingCircleBack2;
                public bool LoadingCircleBackGradient;
                public Paths.GradientMode LoadingCircleBackGradientMode;
                public bool LoadingCircleBackNoise;
                public float LoadingCircleBackNoiseOpacity;
                public Color LoadingCircleHot1;
                public Color LoadingCircleHot2;
                public bool LoadingCircleHotGradient;
                public Paths.GradientMode LoadingCircleHotGradientMode;
                public bool LoadingCircleHotNoise;
                public float LoadingCircleHotNoiseOpacity;
                public bool Shadow_Enabled;
                public Color Shadow_Color;
                public int Shadow_Blur;
                public float Shadow_Opacity;
                public int Shadow_OffsetX;
                public int Shadow_OffsetY;

                public void Load(string subKey)
                {
                    ArrowStyle = (Paths.ArrowStyle)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "ArrowStyle", Paths.ArrowStyle.Aero));
                    CircleStyle = (Paths.CircleStyle)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "CircleStyle", Paths.CircleStyle.Aero));

                    PrimaryColor1 = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "PrimaryColor1", Color.White.ToArgb())));
                    PrimaryColor2 = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "PrimaryColor2", Color.White.ToArgb())));
                    SecondaryColor1 = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "SecondaryColor1", (subKey.ToLower() != "none" ? Color.Black : Color.Red).ToArgb())));
                    SecondaryColor2 = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "SecondaryColor2", (subKey.ToLower() != "none" ? Color.FromArgb(64, 65, 75) : Color.Red).ToArgb())));
                    LoadingCircleBack1 = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleBack1", Color.FromArgb(42, 151, 243).ToArgb())));
                    LoadingCircleBack2 = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleBack2", Color.FromArgb(42, 151, 243).ToArgb())));
                    LoadingCircleHot1 = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleHot1", Color.FromArgb(37, 204, 255).ToArgb())));
                    LoadingCircleHot2 = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleHot2", Color.FromArgb(37, 204, 255).ToArgb())));

                    PrimaryColorGradient = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "PrimaryColorGradient", false));
                    SecondaryColorGradient = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "SecondaryColorGradient", true));
                    LoadingCircleBackGradient = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleBackGradient", false));
                    LoadingCircleHotGradient = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleHotGradient", false));

                    PrimaryColorNoise = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "PrimaryColorNoise", false));
                    SecondaryColorNoise = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "SecondaryColorNoise", false));
                    LoadingCircleBackNoise = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleBackNoise", false));
                    LoadingCircleHotNoise = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleHotNoise", false));

                    PrimaryColorGradientMode = (Paths.GradientMode)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "PrimaryColorGradientMode", (int)Paths.GradientMode.Circle));
                    SecondaryColorGradientMode = (Paths.GradientMode)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "SecondaryColorGradientMode", (int)Paths.GradientMode.Vertical));
                    LoadingCircleBackGradientMode = (Paths.GradientMode)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleBackGradientMode", (int)Paths.GradientMode.Circle));
                    LoadingCircleHotGradientMode = (Paths.GradientMode)Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleHotGradientMode", (int)Paths.GradientMode.Circle));

                    PrimaryColorNoiseOpacity = float.Parse(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "PrimaryColorNoiseOpacity", 25).ToString()) / 100;
                    SecondaryColorNoiseOpacity = float.Parse(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "SecondaryColorNoiseOpacity", 25).ToString()) / 100;
                    LoadingCircleBackNoiseOpacity = float.Parse(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleBackNoiseOpacity", 25).ToString()) / 100;
                    LoadingCircleHotNoiseOpacity = float.Parse(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleHotNoiseOpacity", 25).ToString()) / 100;

                    Shadow_Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "Shadow_Enabled", false));
                    Shadow_Color = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "Shadow_Color", Color.Black.ToArgb())));
                    Shadow_Blur = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "Shadow_Blur", 5));
                    Shadow_Opacity = float.Parse(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "Shadow_Opacity", 30).ToString()) / 100;
                    Shadow_OffsetX = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "Shadow_OffsetX", 2));
                    Shadow_OffsetY = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "Shadow_OffsetY", 2));
                }
                public static void Save_Cursors_To_Registry(string subKey, Cursor Cursor, TreeView TreeView = null)
                {
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "ArrowStyle", (int)Cursor.ArrowStyle);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "CircleStyle", (int)Cursor.CircleStyle);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "PrimaryColor1", Cursor.PrimaryColor1.ToArgb());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "PrimaryColor2", Cursor.PrimaryColor2.ToArgb());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "PrimaryColorGradient", Cursor.PrimaryColorGradient.ToInteger());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "PrimaryColorGradientMode", (int)Cursor.PrimaryColorGradientMode);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "PrimaryColorNoise", Cursor.PrimaryColorNoise.ToInteger());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "PrimaryColorNoiseOpacity", Cursor.PrimaryColorNoiseOpacity * 100f);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "SecondaryColor1", Cursor.SecondaryColor1.ToArgb());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "SecondaryColor2", Cursor.SecondaryColor2.ToArgb());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "SecondaryColorGradient", Cursor.SecondaryColorGradient.ToInteger());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "SecondaryColorGradientMode", (int)Cursor.SecondaryColorGradientMode);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "SecondaryColorNoise", Cursor.SecondaryColorNoise.ToInteger());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "SecondaryColorNoiseOpacity", Cursor.SecondaryColorNoiseOpacity * 100f);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleBack1", Cursor.LoadingCircleBack1.ToArgb());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleBack2", Cursor.LoadingCircleBack2.ToArgb());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleBackGradient", Cursor.LoadingCircleBackGradient.ToInteger());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleBackGradientMode", (int)Cursor.LoadingCircleBackGradientMode);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleBackNoise", Cursor.LoadingCircleBackNoise.ToInteger());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleBackNoiseOpacity", Cursor.LoadingCircleBackNoiseOpacity * 100f);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleHot1", Cursor.LoadingCircleHot1.ToArgb());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleHot2", Cursor.LoadingCircleHot2.ToArgb());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleHotGradient", Cursor.LoadingCircleHotGradient.ToInteger());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleHotGradientMode", (int)Cursor.LoadingCircleHotGradientMode);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleHotNoise", Cursor.LoadingCircleHotNoise.ToInteger());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "LoadingCircleHotNoiseOpacity", Cursor.LoadingCircleHotNoiseOpacity * 100f);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "Shadow_Enabled", Cursor.Shadow_Enabled);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "Shadow_Color", Cursor.Shadow_Color.ToArgb());
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "Shadow_Blur", Cursor.Shadow_Blur);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "Shadow_Opacity", (int)Math.Round(Cursor.Shadow_Opacity * 100f));
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "Shadow_OffsetX", Cursor.Shadow_OffsetX);
                    EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Cursors\" + subKey, "Shadow_OffsetY", Cursor.Shadow_OffsetY);
                }

                public static bool operator ==(Cursor First, Cursor Second)
                {
                    return First.Equals(Second);
                }

                public static bool operator !=(Cursor First, Cursor Second)
                {
                    return !First.Equals(Second);
                }
                public object Clone()
                {
                    return MemberwiseClone();
                }
            }
        }

        #region Properties
        public Info Info = new Info()
        {
            AppVersion = My.Env.AppVersion,
            ThemeName = My.Env.Lang.CurrentMode,
            Description = "",
            ExportResThemePack = false,
            License = "",
            ThemeVersion = "1.0.0.0",
            Author = Environment.UserName,
            AuthorSocialMediaLink = "",
            Color1 = Color.FromArgb(0, 102, 204),
            Color2 = Color.FromArgb(122, 9, 9),
            DesignedFor_Win11 = true,
            DesignedFor_Win10 = true,
            DesignedFor_Win81 = true,
            DesignedFor_Win7 = true,
            DesignedFor_WinVista = true,
            DesignedFor_WinXP = true,
            Pattern = 1
        };

        public AppTheme AppTheme = new AppTheme()
        {
            Enabled = false,
            BackColor = Color.FromArgb(25, 25, 25),
            AccentColor = Color.FromArgb(0, 81, 210),
            DarkMode = true,
            RoundCorners = My.Env.WXP | My.Env.WVista | My.Env.W7 | My.Env.W11
        };

        public Windows10x Windows11 = new Windows10x()
        {
            Color_Index0 = Color.FromArgb(153, 235, 255),
            Color_Index1 = Color.FromArgb(76, 194, 255),
            Color_Index2 = Color.FromArgb(0, 145, 248),
            Color_Index3 = Color.FromArgb(0, 120, 212),
            Color_Index4 = Color.FromArgb(0, 103, 192),
            Color_Index5 = Color.FromArgb(0, 62, 146),
            Color_Index6 = Color.FromArgb(0, 26, 104),
            Color_Index7 = Color.FromArgb(247, 99, 12),
            Titlebar_Active = Color.FromArgb(0, 120, 212),
            Titlebar_Inactive = Color.FromArgb(32, 32, 32),
            StartMenu_Accent = Color.FromArgb(0, 103, 192),
            WinMode_Light = true,
            AppMode_Light = true,
            Transparency = true,
            ApplyAccentOnTitlebars = false,
            ApplyAccentOnTaskbar = Windows10x.AccentTaskbarLevels.None,
            IncreaseTBTransparency = false,
            TB_Blur = true
        };

        public Windows10x Windows10 = new Windows10x()
        {
            Color_Index0 = Color.FromArgb(166, 216, 255),
            Color_Index1 = Color.FromArgb(118, 185, 237),
            Color_Index2 = Color.FromArgb(66, 156, 227),
            Color_Index3 = Color.FromArgb(0, 120, 215),
            Color_Index4 = Color.FromArgb(0, 90, 158),
            Color_Index5 = Color.FromArgb(0, 66, 117),
            Color_Index6 = Color.FromArgb(0, 38, 66),
            Color_Index7 = Color.FromArgb(247, 99, 12),
            Titlebar_Active = Color.FromArgb(0, 120, 215),
            Titlebar_Inactive = Color.FromArgb(43, 43, 43),
            StartMenu_Accent = Color.FromArgb(0, 90, 158),
            WinMode_Light = false,
            AppMode_Light = true,
            Transparency = true,
            ApplyAccentOnTitlebars = false,
            ApplyAccentOnTaskbar = Windows10x.AccentTaskbarLevels.None,
            IncreaseTBTransparency = false,
            TB_Blur = true
        };

        public Windows8x Windows81 = new Windows8x()
        {
            ColorizationColor = Color.FromArgb(246, 195, 74),
            ColorizationColorBalance = 78,
            Start = 0,
            StartColor = Color.FromArgb(30, 0, 84),
            AccentColor = Color.FromArgb(72, 29, 178),
            Theme = Windows7.Themes.Aero,
            LogonUI = 0,
            PersonalColors_Background = Color.FromArgb(30, 0, 84),
            PersonalColors_Accent = Color.FromArgb(72, 29, 178),
            NoLockScreen = false,
            LockScreenType = Structures.LogonUI7.Modes.Default_,
            LockScreenSystemID = 0
        };

        public Windows7 Windows7 = new Windows7()
        {
            ColorizationColor = Color.FromArgb(116, 184, 252),
            ColorizationAfterglow = Color.FromArgb(116, 184, 252),
            ColorizationColorBalance = 8,
            ColorizationAfterglowBalance = 43,
            ColorizationBlurBalance = 49,
            ColorizationGlassReflectionIntensity = 0,
            EnableAeroPeek = true,
            AlwaysHibernateThumbnails = false,
            Theme = Windows7.Themes.Aero
        };

        public WindowsVista WindowsVista = new WindowsVista()
        {
            ColorizationColor = Color.FromArgb(64, 158, 254),
            Theme = Windows7.Themes.Aero
        };

        public WindowsXP WindowsXP = new WindowsXP()
        {
            Theme = WindowsXP.Themes.LunaBlue,
            ColorScheme = "NormalColor",
            ThemeFile = My.Env.PATH_Windows + @"\resources\Themes\Luna\Luna.msstyles"
        };

        public Structures.Win32UI Win32 = new Structures.Win32UI()
        {
            EnableTheming = true,
            EnableGradient = true,
            ActiveBorder = Color.FromArgb(180, 180, 180),
            ActiveTitle = Color.FromArgb(153, 180, 209),
            AppWorkspace = Color.FromArgb(171, 171, 171),
            Background = Color.FromArgb(0, 0, 0),
            ButtonAlternateFace = Color.FromArgb(0, 0, 0),
            ButtonDkShadow = Color.FromArgb(105, 105, 105),
            ButtonFace = Color.FromArgb(240, 240, 240),
            ButtonHilight = Color.FromArgb(255, 255, 255),
            ButtonLight = Color.FromArgb(227, 227, 227),
            ButtonShadow = Color.FromArgb(160, 160, 160),
            ButtonText = Color.FromArgb(0, 0, 0),
            GradientActiveTitle = Color.FromArgb(185, 209, 234),
            GradientInactiveTitle = Color.FromArgb(215, 228, 242),
            GrayText = Color.FromArgb(109, 109, 109),
            HilightText = Color.FromArgb(255, 255, 255),
            HotTrackingColor = Color.FromArgb(0, 102, 204),
            InactiveBorder = Color.FromArgb(244, 247, 252),
            InactiveTitle = Color.FromArgb(191, 205, 219),
            InactiveTitleText = Color.FromArgb(0, 0, 0),
            InfoText = Color.FromArgb(0, 0, 0),
            InfoWindow = Color.FromArgb(255, 255, 225),
            Menu = Color.FromArgb(240, 240, 240),
            MenuBar = Color.FromArgb(240, 240, 240),
            MenuText = Color.FromArgb(0, 0, 0),
            Scrollbar = Color.FromArgb(200, 200, 200),
            TitleText = Color.FromArgb(0, 0, 0),
            Window = Color.FromArgb(255, 255, 255),
            WindowFrame = Color.FromArgb(100, 100, 100),
            WindowText = Color.FromArgb(0, 0, 0),
            Hilight = Color.FromArgb(0, 120, 215),
            MenuHilight = Color.FromArgb(0, 120, 215),
            Desktop = Color.FromArgb(0, 0, 0)
        };

        public LogonUI10x LogonUI10x = new LogonUI10x() { DisableAcrylicBackgroundOnLogon = false, DisableLogonBackgroundImage = false, NoLockScreen = false };

        public Structures.LogonUI7 LogonUI7 = new Structures.LogonUI7()
        {
            Enabled = false,
            Mode = Structures.LogonUI7.Modes.Default_,
            ImagePath = @"C:\Windows\Web\Wallpaper\Windows\img0.jpg",
            Color = Color.Black,
            Blur = false,
            Blur_Intensity = 0,
            Grayscale = false,
            Noise = false,
            Noise_Mode = BitmapExtensions.NoiseMode.Acrylic,
            Noise_Intensity = 0
        };

        public Structures.LogonUIXP LogonUIXP = new Structures.LogonUIXP()
        {
            Enabled = true,
            Mode = Structures.LogonUIXP.Modes.Default,
            BackColor = Color.Black,
            ShowMoreOptions = false
        };

        public Wallpaper Wallpaper = new Wallpaper()
        {
            Enabled = false,
            ImageFile = My.Env.PATH_Windows + @"\Web\Wallpaper\Windows\img0.jpg",
            WallpaperType = Wallpaper.WallpaperTypes.Picture,
            WallpaperStyle = Wallpaper.WallpaperStyles.Fill,
            Wallpaper_Slideshow_Images = new string[] { },
            Wallpaper_Slideshow_ImagesRootPath = "",
            Wallpaper_Slideshow_Interval = 60000,
            Wallpaper_Slideshow_Shuffle = false,
            SlideShow_Folder_or_ImagesList = true
        };

        public WallpaperTone WallpaperTone_W11 = new WallpaperTone()
        {
            Enabled = false,
            Image = My.Env.PATH_Windows + @"\Web\Wallpaper\Windows\img0.jpg",
            H = 0,
            S = 100,
            L = 100
        };

        public WallpaperTone WallpaperTone_W10 = new WallpaperTone()
        {
            Enabled = false,
            Image = My.Env.PATH_Windows + @"\Web\Wallpaper\Windows\img0.jpg",
            H = 0,
            S = 100,
            L = 100
        };

        public WallpaperTone WallpaperTone_W81 = new WallpaperTone()
        {
            Enabled = false,
            Image = My.Env.PATH_Windows + @"\Web\Wallpaper\Windows\img0.jpg",
            H = 0,
            S = 100,
            L = 100
        };

        public WallpaperTone WallpaperTone_W7 = new WallpaperTone()
        {
            Enabled = false,
            Image = My.Env.PATH_Windows + @"\Web\Wallpaper\Windows\img0.jpg",
            H = 0,
            S = 100,
            L = 100
        };

        public WallpaperTone WallpaperTone_WVista = new WallpaperTone()
        {
            Enabled = false,
            Image = My.Env.PATH_Windows + @"\Web\Wallpaper\Windows\img0.jpg",
            H = 0,
            S = 100,
            L = 100
        };

        public WallpaperTone WallpaperTone_WXP = new WallpaperTone()
        {
            Enabled = false,
            Image = My.Env.PATH_Windows + @"\Web\Wallpaper\Bliss.bmp",
            H = 0,
            S = 100,
            L = 100
        };

        public MetricsFonts MetricsFonts = new MetricsFonts()
        {
            Enabled = GetWindowsScreenScalingFactor() == 100d,
            BorderWidth = 1,
            CaptionHeight = 22,
            CaptionWidth = 22,
            IconSpacing = 75,
            IconVerticalSpacing = 75,
            MenuHeight = 19,
            MenuWidth = 19,
            PaddedBorderWidth = 4,
            ScrollHeight = 19,
            ScrollWidth = 19,
            SmCaptionHeight = 22,
            SmCaptionWidth = 22,
            DesktopIconSize = 48,
            ShellIconSize = 32,
            ShellSmallIconSize = 16,
            Fonts_SingleBitPP = false,
            CaptionFont = new Font("Segoe UI", 9f, FontStyle.Regular),
            IconFont = new Font("Segoe UI", 9f, FontStyle.Regular),
            MenuFont = new Font("Segoe UI", 9f, FontStyle.Regular),
            MessageFont = new Font("Segoe UI", 9f, FontStyle.Regular),
            SmCaptionFont = new Font("Segoe UI", 9f, FontStyle.Regular),
            StatusFont = new Font("Segoe UI", 9f, FontStyle.Regular),
            FontSubstitute_MSShellDlg = "Microsoft Sans Serif",
            FontSubstitute_MSShellDlg2 = "Tahoma",
            FontSubstitute_SegoeUI = ""
        };

        public WinEffects WindowsEffects = new WinEffects()
        {
            Enabled = true,
            WindowAnimation = true,
            WindowShadow = true,
            WindowUIEffects = true,
            AnimateControlsInsideWindow = true,
            MenuAnimation = true,
            MenuSelectionFade = true,
            MenuFade = WinEffects.MenuAnimType.Fade,
            MenuShowDelay = 400U,
            ComboBoxAnimation = true,
            ListBoxSmoothScrolling = true,
            TooltipAnimation = true,
            TooltipFade = WinEffects.MenuAnimType.Fade,
            IconsShadow = true,
            IconsDesktopTranslSel = true,
            ShowWinContentDrag = true,
            BalloonNotifications = false,
            PaintDesktopVersion = false,
            ShowSecondsInSystemClock = false,
            Win11ClassicContextMenu = false,
            SysListView32 = false,
            SnapCursorToDefButton = false,
            ShakeToMinimize = true,
            NotificationDuration = 5,
            FocusRectWidth = 1U,
            FocusRectHeight = 1U,
            KeyboardUnderline = false,
            Caret = 1U,
            AWT_Enabled = false,
            AWT_Delay = 0,
            AWT_BringActivatedWindowToTop = false,
            Win11BootDots = !My.Env.W11,
            Win11ExplorerBar = WinEffects.ExplorerBar.Default,
            DisableNavBar = false,
            AutoHideScrollBars = true,
            ColorFilter_Enabled = false,
            ColorFilter = WinEffects.ColorFilters.Grayscale,
            ClassicVolMixer = false,
            FullScreenStartMenu = false
        };

        public ScreenSaver ScreenSaver = new ScreenSaver()
        {
            Enabled = false,
            File = "",
            IsSecure = false,
            TimeOut = 60
        };

        public Sounds Sounds = new Sounds()
        {
            Enabled = true,
            Snd_Imageres_SystemStart = My.Env.W11 ? "Default" : "",
            Snd_Win_SystemExit_TaskMgmt = !My.Env.WXP & !My.Env.WVista & !My.Env.W7,
            Snd_Win_WindowsLogoff_TaskMgmt = !My.Env.WXP & !My.Env.WVista & !My.Env.W7,
            Snd_Win_WindowsLogon_TaskMgmt = !My.Env.WXP & !My.Env.WVista & !My.Env.W7,
            Snd_Win_WindowsUnlock_TaskMgmt = !My.Env.WXP & !My.Env.WVista & !My.Env.W7
        };

        public AltTab AltTab = new AltTab() { Enabled = true, Style = AltTab.Styles.Default, Win10Opacity = 95 };

        public Structures.Console CommandPrompt = new Structures.Console()
        {
            Enabled = false,
            ColorTable00 = Color.FromArgb(12, 12, 12),
            ColorTable01 = Color.FromArgb(0, 55, 218),
            ColorTable02 = Color.FromArgb(19, 161, 14),
            ColorTable03 = Color.FromArgb(58, 150, 221),
            ColorTable04 = Color.FromArgb(197, 15, 31),
            ColorTable05 = Color.FromArgb(136, 23, 152),
            ColorTable06 = Color.FromArgb(193, 156, 0),
            ColorTable07 = Color.FromArgb(204, 204, 204),
            ColorTable08 = Color.FromArgb(118, 118, 118),
            ColorTable09 = Color.FromArgb(59, 120, 255),
            ColorTable10 = Color.FromArgb(22, 198, 12),
            ColorTable11 = Color.FromArgb(97, 214, 214),
            ColorTable12 = Color.FromArgb(231, 72, 86),
            ColorTable13 = Color.FromArgb(180, 0, 158),
            ColorTable14 = Color.FromArgb(249, 241, 165),
            ColorTable15 = Color.FromArgb(242, 242, 242),
            PopupForeground = 5,
            PopupBackground = 15,
            ScreenColorsForeground = 7,
            ScreenColorsBackground = 0,
            CursorSize = 19,
            FaceName = "Consolas",
            FontRaster = false,
            FontSize = 18 * 65536,
            FontWeight = 400,
            W10_1909_CursorType = 0,
            W10_1909_CursorColor = Color.White,
            W10_1909_ForceV2 = true,
            W10_1909_LineSelection = false,
            W10_1909_TerminalScrolling = false,
            W10_1909_WindowAlpha = 255
        };

        public Structures.Console PowerShellx86 = new Structures.Console()
        {
            Enabled = false,
            ColorTable00 = Color.FromArgb(12, 12, 12),
            ColorTable01 = Color.FromArgb(0, 55, 218),
            ColorTable02 = Color.FromArgb(19, 161, 14),
            ColorTable03 = Color.FromArgb(58, 150, 221),
            ColorTable04 = Color.FromArgb(197, 15, 31),
            ColorTable05 = Color.FromArgb(1, 36, 86),
            ColorTable06 = Color.FromArgb(238, 237, 240),
            ColorTable07 = Color.FromArgb(204, 204, 204),
            ColorTable08 = Color.FromArgb(118, 118, 118),
            ColorTable09 = Color.FromArgb(59, 120, 255),
            ColorTable10 = Color.FromArgb(22, 198, 12),
            ColorTable11 = Color.FromArgb(97, 214, 214),
            ColorTable12 = Color.FromArgb(231, 72, 86),
            ColorTable13 = Color.FromArgb(180, 0, 158),
            ColorTable14 = Color.FromArgb(249, 241, 165),
            ColorTable15 = Color.FromArgb(242, 242, 242),
            PopupForeground = 15,
            PopupBackground = 3,
            ScreenColorsForeground = 6,
            ScreenColorsBackground = 5,
            CursorSize = 19,
            FaceName = "Consolas",
            FontRaster = false,
            FontSize = 16 * 65536,
            FontWeight = 400,
            W10_1909_CursorType = 0,
            W10_1909_CursorColor = Color.White,
            W10_1909_ForceV2 = true,
            W10_1909_LineSelection = false,
            W10_1909_TerminalScrolling = false,
            W10_1909_WindowAlpha = 255
        };

        public Structures.Console PowerShellx64 = new Structures.Console()
        {
            Enabled = false,
            ColorTable00 = Color.FromArgb(12, 12, 12),
            ColorTable01 = Color.FromArgb(0, 55, 218),
            ColorTable02 = Color.FromArgb(19, 161, 14),
            ColorTable03 = Color.FromArgb(58, 150, 221),
            ColorTable04 = Color.FromArgb(197, 15, 31),
            ColorTable05 = Color.FromArgb(1, 36, 86),
            ColorTable06 = Color.FromArgb(238, 237, 240),
            ColorTable07 = Color.FromArgb(204, 204, 204),
            ColorTable08 = Color.FromArgb(118, 118, 118),
            ColorTable09 = Color.FromArgb(59, 120, 255),
            ColorTable10 = Color.FromArgb(22, 198, 12),
            ColorTable11 = Color.FromArgb(97, 214, 214),
            ColorTable12 = Color.FromArgb(231, 72, 86),
            ColorTable13 = Color.FromArgb(180, 0, 158),
            ColorTable14 = Color.FromArgb(249, 241, 165),
            ColorTable15 = Color.FromArgb(242, 242, 242),
            PopupForeground = 15,
            PopupBackground = 3,
            ScreenColorsForeground = 6,
            ScreenColorsBackground = 5,
            CursorSize = 19,
            FaceName = "Consolas",
            FontRaster = false,
            FontSize = 16 * 65536,
            FontWeight = 400,
            W10_1909_CursorType = 0,
            W10_1909_CursorColor = Color.White,
            W10_1909_ForceV2 = true,
            W10_1909_LineSelection = false,
            W10_1909_TerminalScrolling = false,
            W10_1909_WindowAlpha = 255
        };

        public WinTerminal Terminal = new WinTerminal("", WinTerminal.Mode.Empty);

        public WinTerminal TerminalPreview = new WinTerminal("", WinTerminal.Mode.Empty);

        #region Cursors
        public bool Cursor_Enabled = false;

        public bool Cursor_Shadow = false;

        public bool Cursor_Sonar = false;

        public int Cursor_Trails = 0;

        public Structures.Cursor Cursor_Arrow = new Structures.Cursor()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Structures.Cursor Cursor_AppLoading = new Structures.Cursor()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Circle,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Structures.Cursor Cursor_Busy = new Structures.Cursor()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Circle,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Circle,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Structures.Cursor Cursor_Help = new Structures.Cursor()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Structures.Cursor Cursor_Move = new Structures.Cursor()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Structures.Cursor Cursor_NS = new Structures.Cursor()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Structures.Cursor Cursor_EW = new Structures.Cursor()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Structures.Cursor Cursor_NESW = new Structures.Cursor()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Structures.Cursor Cursor_NWSE = new Structures.Cursor()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Structures.Cursor Cursor_Up = new Structures.Cursor()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Structures.Cursor Cursor_Pen = new Structures.Cursor()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Structures.Cursor Cursor_None = new Structures.Cursor()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.FromArgb(255, 0, 0),
            SecondaryColor2 = Color.FromArgb(255, 0, 0),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Structures.Cursor Cursor_Link = new Structures.Cursor()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Structures.Cursor Cursor_Pin = new Structures.Cursor()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Structures.Cursor Cursor_Person = new Structures.Cursor()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Structures.Cursor Cursor_IBeam = new Structures.Cursor()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Structures.Cursor Cursor_Cross = new Structures.Cursor()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };
        #endregion

        #endregion

        #region Functions
        public static List<Color> GetPaletteFromMSTheme(string Filename)
        {
            if (File.Exists(Filename))
            {

                var ls = new List<Color>();
                ls.Clear();

                var tx = File.ReadAllText(Filename).CList();

                foreach (string x in tx)
                {
                    try
                    {
                        if (x.Contains("="))
                        {
                            if (x.Split('=')[1].Contains(" "))
                            {
                                if (x.Split('=')[1].Split(' ').Count() == 3)
                                {
                                    string c = x.Split('=')[1];
                                    bool inx = true;
                                    foreach (var u in c.Split(' '))
                                    {
                                        if (!u.All(char.IsDigit))
                                            inx = false;
                                    }
                                    if (inx)
                                        ls.Add(c.FromWin32RegToColor());
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                ls = ls.Distinct().ToList();
                ls.Sort(new RGBColorComparer());
                return ls;
            }
            else
            {
                return null;
            }
        }
        public static List<Color> GetPaletteFromString(string String, string ThemeName)
        {

            if (string.IsNullOrWhiteSpace(String))
            {
                return null;
                return default;
            }

            if (!String.Contains("|"))
            {
                return null;
                return default;
            }

            if (string.IsNullOrWhiteSpace(ThemeName))
            {
                return null;
                return default;
            }

            var ls = new List<Color>();
            ls.Clear();

            var AllThemes = String.CList();
            string SelectedTheme = "";
            bool Found = false;

            foreach (string th in AllThemes)
            {
                if ((th.Split('|')[0].ToLower() ?? "") == (ThemeName.ToLower() ?? ""))
                {
                    SelectedTheme = th.Replace("|", "\r\n");
                    Found = true;
                    break;
                }
            }

            if (!Found)
            {
                return null;
                return default;
            }

            var SelectedThemeList = SelectedTheme.CList();

            foreach (string x in SelectedThemeList)
            {
                try
                {
                    if (x.Contains("="))
                    {
                        if (x.Split('=')[1].Contains(" "))
                        {
                            if (x.Split('=')[1].Split(' ').Count() == 3)
                            {
                                string c = x.Split('=')[1];
                                bool inx = true;
                                foreach (var u in c.Split(' '))
                                {
                                    if (!u.All(char.IsDigit))
                                        inx = false;
                                }
                                if (inx)
                                    ls.Add(Color.FromArgb(255, Convert.ToInt32(c.Split(' ')[0]), Convert.ToInt32(c.Split(' ')[1]), Convert.ToInt32(c.Split(' ')[2])));
                            }
                        }
                    }
                }
                catch
                {
                }
            }

            ls = ls.Distinct().ToList();
            ls.Sort(new RGBColorComparer());
            return ls;
        }
        public List<Color> ListColors(bool DontMergeRepeatedColors = false)
        {

            var CL = new List<Color>();
            CL.Clear();

            foreach (var field in typeof(Windows10x).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType.Name.ToLower() == "color")
                {
                    CL.Add((Color)field.GetValue(Windows11));
                    CL.Add((Color)field.GetValue(Windows10));
                }
            }

            foreach (var field in typeof(LogonUI10x).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType.Name.ToLower() == "color")
                {
                    CL.Add((Color)field.GetValue(LogonUI10x));
                }
            }

            foreach (var field in typeof(Windows8x).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType.Name.ToLower() == "color")
                {
                    CL.Add((Color)field.GetValue(Windows81));
                }
            }

            foreach (var field in typeof(Windows7).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType.Name.ToLower() == "color")
                {
                    CL.Add((Color)field.GetValue(Windows7));
                }
            }

            foreach (var field in typeof(WindowsVista).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType.Name.ToLower() == "color")
                {
                    CL.Add((Color)field.GetValue(WindowsVista));
                }
            }

            foreach (var field in typeof(WindowsXP).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType.Name.ToLower() == "color")
                {
                    CL.Add((Color)field.GetValue(WindowsXP));
                }
            }

            foreach (var field in typeof(Structures.LogonUI7).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType.Name.ToLower() == "color")
                {
                    CL.Add((Color)field.GetValue(LogonUI7));
                }
            }

            foreach (var field in typeof(Structures.LogonUIXP).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType.Name.ToLower() == "color")
                {
                    CL.Add((Color)field.GetValue(LogonUIXP));
                }
            }

            foreach (var field in typeof(Structures.Win32UI).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType.Name.ToLower() == "color")
                {
                    CL.Add((Color)field.GetValue(Win32));
                }
            }

            foreach (var field in typeof(WallpaperTone).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType.Name.ToLower() == "color")
                {
                    CL.Add((Color)field.GetValue(WallpaperTone_W11));
                    CL.Add((Color)field.GetValue(WallpaperTone_W10));
                    CL.Add((Color)field.GetValue(WallpaperTone_W81));
                    CL.Add((Color)field.GetValue(WallpaperTone_W7));
                    CL.Add((Color)field.GetValue(WallpaperTone_WVista));
                    CL.Add((Color)field.GetValue(WallpaperTone_WXP));
                }
            }

            foreach (var field in typeof(Structures.Console).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType.Name.ToLower() == "color")
                {
                    CL.Add((Color)field.GetValue(CommandPrompt));
                    CL.Add((Color)field.GetValue(PowerShellx86));
                    CL.Add((Color)field.GetValue(PowerShellx64));
                }
            }

            foreach (var c in Terminal.Colors)
            {
                CL.Add(c.Background);
                CL.Add(c.Foreground);
                CL.Add(c.SelectionBackground);
                CL.Add(c.Black);
                CL.Add(c.Blue);
                CL.Add(c.BrightBlack);
                CL.Add(c.BrightBlue);
                CL.Add(c.BrightCyan);
                CL.Add(c.BrightGreen);
                CL.Add(c.BrightPurple);
                CL.Add(c.BrightRed);
                CL.Add(c.BrightWhite);
                CL.Add(c.BrightYellow);
                CL.Add(c.CursorColor);
                CL.Add(c.Cyan);
                CL.Add(c.Green);
                CL.Add(c.Purple);
                CL.Add(c.Red);
                CL.Add(c.White);
                CL.Add(c.Yellow);
            }

            foreach (var c in TerminalPreview.Colors)
            {
                CL.Add(c.Background);
                CL.Add(c.Foreground);
                CL.Add(c.SelectionBackground);
                CL.Add(c.Black);
                CL.Add(c.Blue);
                CL.Add(c.BrightBlack);
                CL.Add(c.BrightBlue);
                CL.Add(c.BrightCyan);
                CL.Add(c.BrightGreen);
                CL.Add(c.BrightPurple);
                CL.Add(c.BrightRed);
                CL.Add(c.BrightWhite);
                CL.Add(c.BrightYellow);
                CL.Add(c.CursorColor);
                CL.Add(c.Cyan);
                CL.Add(c.Green);
                CL.Add(c.Purple);
                CL.Add(c.Red);
                CL.Add(c.White);
                CL.Add(c.Yellow);
            }

            foreach (var c in Terminal.Themes)
            {
                CL.Add(c.Titlebar_Inactive);
                CL.Add(c.Titlebar_Active);
                CL.Add(c.Tab_Active);
                CL.Add(c.Tab_Inactive);
            }

            foreach (var c in TerminalPreview.Themes)
            {
                CL.Add(c.Titlebar_Inactive);
                CL.Add(c.Titlebar_Active);
                CL.Add(c.Tab_Active);
                CL.Add(c.Tab_Inactive);
            }

            foreach (var c in Terminal.Profiles)
                CL.Add(c.TabColor);

            foreach (var c in TerminalPreview.Profiles)
                CL.Add(c.TabColor);

            CL.Add(Terminal.DefaultProf.TabColor);
            CL.Add(TerminalPreview.DefaultProf.TabColor);

            foreach (var field in typeof(Structures.Cursor).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType.Name.ToLower() == "color")
                {
                    CL.Add((Color)field.GetValue(Cursor_Arrow));
                    CL.Add((Color)field.GetValue(Cursor_Help));
                    CL.Add((Color)field.GetValue(Cursor_AppLoading));
                    CL.Add((Color)field.GetValue(Cursor_Busy));
                    CL.Add((Color)field.GetValue(Cursor_Pen));
                    CL.Add((Color)field.GetValue(Cursor_None));
                    CL.Add((Color)field.GetValue(Cursor_Move));
                    CL.Add((Color)field.GetValue(Cursor_Up));
                    CL.Add((Color)field.GetValue(Cursor_NS));
                    CL.Add((Color)field.GetValue(Cursor_EW));
                    CL.Add((Color)field.GetValue(Cursor_NESW));
                    CL.Add((Color)field.GetValue(Cursor_NWSE));
                    CL.Add((Color)field.GetValue(Cursor_Link));
                    CL.Add((Color)field.GetValue(Cursor_Pin));
                    CL.Add((Color)field.GetValue(Cursor_Person));
                    CL.Add((Color)field.GetValue(Cursor_IBeam));
                    CL.Add((Color)field.GetValue(Cursor_Cross));
                }
            }

            if (!DontMergeRepeatedColors)
                CL = CL.Distinct().ToList();

            CL.Sort(new RGBColorComparer());

            if (CL.Contains(Color.FromArgb(0, 0, 0, 0)))
            {
                while (CL.Contains(Color.FromArgb(0, 0, 0, 0)))
                    CL.Remove(Color.FromArgb(0, 0, 0, 0));
            }

            return CL;
        }
        public static bool IsFontInstalled(string fontName)
        {
            bool installed = IsFontInstalled(fontName, FontStyle.Regular);

            if (!installed)
            {
                installed = IsFontInstalled(fontName, FontStyle.Bold);
            }

            if (!installed)
            {
                installed = IsFontInstalled(fontName, FontStyle.Italic);
            }

            return installed;
        }
        public static bool IsFontInstalled(string fontName, FontStyle style)
        {
            bool installed = false;
            const float emSize = 8.0f;

            try
            {

                using (var testFont = new Font(fontName, emSize, style))
                {
                    installed = 0 == string.Compare(fontName, testFont.Name, StringComparison.InvariantCultureIgnoreCase);
                }
            }
            catch
            {
            }

            return installed;
        }
        public static void AddNode(TreeView TreeView, string Text, string ImageKey)
        {
            if (TreeView is not null)
            {
                if (TreeView.InvokeRequired)
                {

                    try
                    {
                        TreeView.Invoke(new MethodInvoker(() =>
                            {
                                {
                                    var temp = TreeView.Nodes.Add(Text);
                                    temp.ImageKey = ImageKey;
                                    temp.SelectedImageKey = ImageKey;
                                }
                                TreeView.SelectedNode = TreeView.Nodes[TreeView.Nodes.Count - 1];
                                TreeView.Update();
                            }));
                    }
                    catch
                    {
                    }
                }

                else
                {

                    try
                    {
                        TreeView.Invoke(new MethodInvoker(() =>
                            {
                                {
                                    var temp = TreeView.Nodes.Add(Text);
                                    temp.ImageKey = ImageKey;
                                    temp.SelectedImageKey = ImageKey;
                                }
                                TreeView.SelectedNode = TreeView.Nodes[TreeView.Nodes.Count - 1];
                                TreeView.Update();
                            }));
                    }
                    catch
                    {
                    }
                }

            }
        }
        private void AddException(string Label, Exception Exception)
        {
            My.Env.Saving_Exceptions.Add(new Tuple<string, Exception>(Label, Exception));
        }
        public void Execute(MethodInvoker Sub, TreeView TreeView = null, string StartStr = "", string ErrorStr = "", string TimeStr = "", Stopwatch overallStopwatch = null, bool Skip = false, string SkipStr = "", bool ExecuteEvenIfSkip = false)
        {

            bool ReportProgress = TreeView is not null;
            var sw = new Stopwatch();
            sw.Reset();
            sw.Stop();
            sw.Start();

            if (!Skip | ExecuteEvenIfSkip)
            {
                if (!ExecuteEvenIfSkip)
                {
                    if (!string.IsNullOrWhiteSpace(StartStr))
                        AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), StartStr), "apply");
                }
                else if (!string.IsNullOrWhiteSpace(ErrorStr))
                    AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), SkipStr), "skip");

                try
                {
                    Sub();
                    if (ReportProgress & !string.IsNullOrWhiteSpace(TimeStr))
                        AddNode(TreeView, string.Format(TimeStr, sw.ElapsedMilliseconds / 1000d), "time");
                }
                catch (Exception ex)
                {
                    sw.Stop();
                    overallStopwatch.Stop();
                    _ErrorHappened = true;
                    if (ReportProgress)
                    {
                        if (!string.IsNullOrWhiteSpace(ErrorStr))
                            AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), ErrorStr), "error");
                        AddException(ErrorStr, ex);
                    }
                    else
                    {
                        My.MyProject.Forms.BugReport.ThrowError(ex);
                    }
                    sw.Start();
                    overallStopwatch.Start();
                }
            }
            else if (!string.IsNullOrWhiteSpace(ErrorStr))
                AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), SkipStr), "skip");

            sw.Stop();
        }
        #endregion

        #region CP Handling (Loading/Applying)
        public CP(CP_Type CP_Type, string File = "", bool IgnoreExtractionThemePack = false)
        {

            switch (CP_Type)
            {
                case CP_Type.Registry:
                    {

                        using (var _Def = CP_Defaults.From(My.Env.PreviewStyle))
                        {
                            My.Env.Loading_Exceptions.Clear();

                            #region Registry
                            Info.Load();
                            Windows11.Load(new CP_Defaults().Default_Windows11().Windows11, new CP_Defaults().Default_Windows11Accents_Bytes);
                            Windows10.Load(new CP_Defaults().Default_Windows10().Windows10, new CP_Defaults().Default_Windows10Accents_Bytes);
                            Windows81.Load(_Def.Windows81);
                            Windows7.Load(_Def.Windows7);
                            WindowsVista.Load(_Def.WindowsVista);
                            WindowsXP.Load(_Def.WindowsXP);
                            WindowsEffects.Load(_Def.WindowsEffects);
                            LogonUI10x.Load(_Def.LogonUI10x);
                            LogonUI7.Load(_Def.LogonUI7);
                            LogonUIXP.Load(_Def.LogonUIXP);
                            Win32.Load();
                            MetricsFonts.Load(_Def.MetricsFonts);
                            AltTab.Load(_Def.AltTab);
                            ScreenSaver.Load(_Def.ScreenSaver);
                            Sounds.Load(_Def.Sounds);
                            AppTheme.Load(_Def.AppTheme);

                            WallpaperTone_W11.Load("Win11");
                            WallpaperTone_W10.Load("Win10");
                            WallpaperTone_W81.Load("Win8.1");
                            WallpaperTone_W7.Load("Win7");
                            WallpaperTone_WVista.Load("WinVista");
                            WallpaperTone_WXP.Load("WinXP");
                            Wallpaper.Load(_Def.Wallpaper);

                            CommandPrompt.Load("", "Terminal_CMD_Enabled", _Def.CommandPrompt);
                            if (Directory.Exists(My.Env.PATH_PS86_app))
                            {
                                try
                                {
                                    Registry.CurrentUser.CreateSubKey(@"Console\" + My.Env.PATH_PS86_reg, true).Close();
                                }
                                catch
                                {
                                }
                                PowerShellx86.Load(My.Env.PATH_PS86_reg, "Terminal_PS_32_Enabled", _Def.PowerShellx86);
                            }
                            else
                            {
                                PowerShellx86 = _Def.PowerShellx86;
                            }
                            if (Directory.Exists(My.Env.PATH_PS64_app))
                            {
                                try
                                {
                                    Registry.CurrentUser.CreateSubKey(@"Console\" + My.Env.PATH_PS64_reg, true).Close();
                                }
                                catch
                                {
                                }
                                PowerShellx64.Load(My.Env.PATH_PS64_reg, "Terminal_PS_64_Enabled", _Def.PowerShellx64);
                            }
                            else
                            {
                                PowerShellx64 = _Def.PowerShellx64;
                            }


                            #region Windows Terminal
                            Terminal.Enabled = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_Stable_Enabled", 0)).ToBoolean();
                            TerminalPreview.Enabled = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_Preview_Enabled", 0)).ToBoolean();

                            if (My.Env.W10 | My.Env.W11)
                            {
                                string TerDir;
                                string TerPreDir;

                                if (!My.Env.Settings.WindowsTerminals.Path_Deflection)
                                {
                                    TerDir = My.Env.PATH_TerminalJSON;
                                    TerPreDir = My.Env.PATH_TerminalPreviewJSON;
                                }
                                else
                                {
                                    if (System.IO.File.Exists(My.Env.Settings.WindowsTerminals.Terminal_Stable_Path))
                                    {
                                        TerDir = My.Env.Settings.WindowsTerminals.Terminal_Stable_Path;
                                    }
                                    else
                                    {
                                        TerDir = My.Env.PATH_TerminalJSON;
                                    }

                                    if (System.IO.File.Exists(My.Env.Settings.WindowsTerminals.Terminal_Preview_Path))
                                    {
                                        TerPreDir = My.Env.Settings.WindowsTerminals.Terminal_Preview_Path;
                                    }
                                    else
                                    {
                                        TerPreDir = My.Env.PATH_TerminalPreviewJSON;
                                    }
                                }


                                if (System.IO.File.Exists(TerDir))
                                {
                                    Terminal = new WinTerminal(TerDir, WinTerminal.Mode.JSONFile);
                                }
                                else
                                {
                                    Terminal = new WinTerminal("", WinTerminal.Mode.Empty);
                                }

                                if (System.IO.File.Exists(TerPreDir))
                                {
                                    TerminalPreview = new WinTerminal(TerPreDir, WinTerminal.Mode.JSONFile, WinTerminal.Version.Preview);
                                }
                                else
                                {
                                    TerminalPreview = new WinTerminal("", WinTerminal.Mode.Empty, WinTerminal.Version.Preview);
                                }
                            }

                            else
                            {
                                Terminal = new WinTerminal("", WinTerminal.Mode.Empty);
                                TerminalPreview = new WinTerminal("", WinTerminal.Mode.Empty, WinTerminal.Version.Preview);
                            }
                            #endregion

                            #region Cursors
                            Cursor_Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors", "", false));

                            if (Fixer.SystemParametersInfo((int)SPI.Cursors.GETCURSORSHADOW, 0, ref Cursor_Shadow, (int)SPIF.None) == 0)
                                Cursor_Shadow = _Def.Cursor_Shadow;
                            if (Fixer.SystemParametersInfo((int)SPI.Cursors.GETMOUSETRAILS, 0, ref Cursor_Trails, (int)SPIF.None) == 0)
                                Cursor_Trails = _Def.Cursor_Trails;
                            if (Fixer.SystemParametersInfo((int)SPI.Cursors.GETMOUSESONAR, 0, ref Cursor_Sonar, (int)SPIF.None) == 0)
                                Cursor_Sonar = _Def.Cursor_Sonar;

                            Cursor_Arrow.Load("Arrow");
                            Cursor_Help.Load("Help");
                            Cursor_AppLoading.Load("AppLoading");
                            Cursor_Busy.Load("Busy");
                            Cursor_Move.Load("Move");
                            Cursor_NS.Load("NS");
                            Cursor_EW.Load("EW");
                            Cursor_NESW.Load("NESW");
                            Cursor_NWSE.Load("NWSE");
                            Cursor_Up.Load("Up");
                            Cursor_Pen.Load("Pen");
                            Cursor_None.Load("None");
                            Cursor_Link.Load("Link");
                            Cursor_Pin.Load("Pin");
                            Cursor_Person.Load("Person");
                            Cursor_IBeam.Load("IBeam");
                            Cursor_Cross.Load("Cross");
                            #endregion

                            if (My.Env.Loading_Exceptions.Count > 0)
                            {
                                My.MyProject.Forms.Saving_ex_list.ex_List = My.Env.Loading_Exceptions;
                                My.MyProject.Forms.Saving_ex_list.ShowDialog();
                            }
                            #endregion
                        }

                        break;
                    }

                case CP_Type.File:
                    {

                        #region File
                        using (var CPx = CP_Defaults.GetDefault())
                        {
                            foreach (FieldInfo field in GetType().GetFields(bindingFlags))
                            {
                                var type = field.FieldType;
                                try
                                {
                                    field.SetValue(this, field.GetValue(CPx));
                                }
                                catch
                                {
                                }
                            }

                        Start:
                            ;


                            if (!System.IO.File.Exists(File))
                                return;

                            // Rough method to get theme name to create its proper resources pack folder
                            foreach (var line in Decompress(File))
                            {
                                if (line.Trim().StartsWith("\"ThemeName\":", My.Env._ignore))
                                {
                                    Info.ThemeName = line.Split(':')[1].ToString().Replace("\"", "").Replace(",", "").Trim();
                                    break;
                                }
                            }

                            var txt = new List<string>();
                            txt.Clear();
                            string Pack = new FileInfo(File).DirectoryName + @"\" + Path.GetFileNameWithoutExtension(File) + ".wptp";
                            bool Pack_IsValid = System.IO.File.Exists(Pack) && new FileInfo(Pack).Length > 0L && _Converter.FetchFile(File) == Converter_CP.WP_Format.JSON;
                            string cache = My.Env.PATH_ThemeResPackCache + @"\" + string.Concat(Info.ThemeName.Replace(" ", "").Split(Path.GetInvalidFileNameChars()));

                            // Extract theme resources pack
                            try
                            {
                                if (Pack_IsValid & !IgnoreExtractionThemePack)
                                {
                                    if (!Directory.Exists(cache))
                                        Directory.CreateDirectory(cache);

                                    using (var s = new FileStream(Pack, FileMode.Open, FileAccess.Read))
                                    {
                                        using (var archive = new ZipArchive(s, ZipArchiveMode.Read))
                                        {
                                            foreach (ZipArchiveEntry entry in archive.Entries)
                                            {
                                                if (entry.FullName.Contains(@"\"))
                                                {
                                                    string dest = Path.Combine(cache, entry.FullName);
                                                    string dest_dir = dest.Replace(@"\" + dest.Split('\\').Last(), "");
                                                    if (!Directory.Exists(dest_dir))
                                                        Directory.CreateDirectory(dest_dir);
                                                }
                                                entry.ExtractToFile(Path.Combine(cache, entry.FullName), true);
                                            }
                                        }

                                        s.Close();
                                        s.Dispose();
                                    }

                                }
                            }

                            catch (Exception ex)
                            {
                                Pack_IsValid = false;
                                My.MyProject.Forms.BugReport.ThrowError(ex);
                            }

                            txt = (List<string>)Decompress(File);

                            if (IsValidJson(string.Join("\r\n", txt)))
                            {

                                // Replace %WinPaletterAppData% variable with a valid AppData folder path
                                for (int x = 0, loopTo = txt.Count - 1; x <= loopTo; x++)
                                {
                                    if (txt[x].Contains(":"))
                                    {
                                        string[] arr = txt[x].Split(':');
                                        if (arr.Count() == 2 && arr[1].Contains("%WinPaletterAppData%"))
                                        {
                                            txt[x] = arr[0] + ":" + arr[1].Replace("%WinPaletterAppData%", My.Env.PATH_appData.Replace(@"\", @"\\"));
                                        }
                                    }
                                }

                                JObject J = JObject.Parse(string.Join("\r\n", txt));

                                // This will get the new added features to fix bug (null values on opening a theme file)
                                try
                                {
                                    JObject J_Original = JObject.Parse(CPx.ToString(true));
                                    foreach (var item in J_Original)
                                    {
                                        if (J[item.Key] is null && J_Original[item.Key] is not null)
                                            J[item.Key] = J_Original[item.Key];
                                        if (!(item.Value is JValue))
                                        {
                                            foreach (KeyValuePair<string, JToken> prop in (JObject)item.Value)
                                            {
                                                try
                                                {
                                                    if (J[item.Key][prop.Key] is null && J_Original[item.Key] is not null && J_Original[item.Key][prop.Key] is not null)
                                                    {
                                                        J[item.Key][prop.Key] = J_Original[item.Key][prop.Key];
                                                    }
                                                }
                                                catch
                                                {
                                                }
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                }

                                foreach (FieldInfo field in GetType().GetFields(bindingFlags))
                                {
                                    var type = field.FieldType;
                                    var JSet = new JsonSerializerSettings();

                                    if (J[field.Name] is not null)
                                    {
                                        field.SetValue(this, J[field.Name].ToObject(type));
                                    }
                                }
                            }

                            else if (_Converter.FetchFile(File) == Converter_CP.WP_Format.WPTH)
                            {
                                if (MsgBox(My.Env.Lang.Convert_Detect_Old_OnLoading0, MessageBoxButtons.YesNo, MessageBoxIcon.Question, My.Env.Lang.Convert_Detect_Old_OnLoading1, "", "", "", "", My.Env.Lang.Convert_Detect_Old_OnLoading2, Ookii.Dialogs.WinForms.TaskDialogIcon.Information) == DialogResult.Yes)
                                {
                                    _Converter.Convert(File, File, My.Env.Settings.FileTypeManagement.CompressThemeFile, false);
                                    goto Start;
                                }
                            }
                            else
                            {
                                WPStyle.MsgBox(My.Env.Lang.Convert_Error_Phrasing, MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }

                        break;
                    }

                    #endregion

            }
        }

        public void Save(CP_Type SaveTo, string File = "", TreeView TreeView = null, bool ResetToDefault = false)
        {

            switch (SaveTo)
            {
                case CP_Type.Registry:
                    {

                        #region Registry
                        bool ReportProgress = My.Env.Settings.ThemeLog.VerboseLevel != WPSettings.Structures.ThemeLog.VerboseLevels.None && TreeView is not null;
                        bool ReportProgress_Detailed = ReportProgress && My.Env.Settings.ThemeLog.VerboseLevel == WPSettings.Structures.ThemeLog.VerboseLevels.Detailed;

                        _ErrorHappened = false;

                        var sw_all = new Stopwatch();
                        sw_all.Reset();
                        sw_all.Start();


                        if (ReportProgress)
                        {
                            My.Env.Saving_Exceptions.Clear();
                            TreeView.Visible = false;
                            TreeView.Nodes.Clear();
                            TreeView.Visible = true;
                            string OS;
                            if (My.Env.W11)
                            {
                                OS = My.Env.Lang.OS_Win11;
                            }
                            else if (My.Env.W10)
                            {
                                OS = My.Env.Lang.OS_Win10;
                            }
                            else if (My.Env.W8)
                            {
                                OS = My.Env.Lang.OS_Win8;
                            }
                            else if (My.Env.W81)
                            {
                                OS = My.Env.Lang.OS_Win81;
                            }
                            else if (My.Env.W7)
                            {
                                OS = My.Env.Lang.OS_Win7;
                            }
                            else if (My.Env.WVista)
                            {
                                OS = My.Env.Lang.OS_WinVista;
                            }
                            else if (My.Env.WXP)
                            {
                                OS = My.Env.Lang.OS_WinXP;
                            }
                            else
                            {
                                OS = My.Env.Lang.OS_WinUndefined;
                            }

                            AddNode(TreeView, string.Format("{0}", string.Format(My.Env.Lang.CP_ApplyFrom, OS)), "info");

                            AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), My.Env.Lang.CP_Applying_Started), "info");

                            if (!My.Env.isElevated)
                            {
                                AddNode(TreeView, string.Format("{0}}", My.Env.Lang.CP_Admin_Msg0), "admin");
                                AddNode(TreeView, string.Format("{0}", My.Env.Lang.CP_Admin_Msg1), "admin");
                            }

                        }

                        // Reset to default Windows theme
                        if (ResetToDefault)
                        {






                            Execute(() => { using (var def = CP_Defaults.GetDefault()) { def.LogonUI10x.NoLockScreen = false; def.LogonUI7.Enabled = false; def.Windows81.NoLockScreen = false; def.LogonUIXP.Enabled = true; if (!My.Env.WXP) ResetCursorsToAero(); else ResetCursorsToNone_XP(); def.CommandPrompt.Enabled = true; def.PowerShellx86.Enabled = true; def.PowerShellx64.Enabled = true; def.MetricsFonts.Enabled = true; def.WindowsEffects.Enabled = true; def.AltTab.Enabled = true; def.ScreenSaver.Enabled = true; def.Sounds.Enabled = true; def.AppTheme.Enabled = true; def.Wallpaper.Enabled = false; def.Save(CP_Type.Registry); } }, TreeView, My.Env.Lang.CP_ThemeReset, My.Env.Lang.CP_ThemeReset_Error, My.Env.Lang.CP_Time, sw_all);
                        }

                        // Theme info
                        Execute(() => Info.Apply(ReportProgress_Detailed ? TreeView : null), TreeView, My.Env.Lang.CP_SavingInfo, My.Env.Lang.CP_SavingInfo_Error, My.Env.Lang.CP_Time, sw_all);

                        // WinPaletter application theme
                        Execute(() => AppTheme.Apply(ReportProgress_Detailed ? TreeView : null), TreeView, My.Env.Lang.CP_Applying_AppTheme, My.Env.Lang.CP_Error_AppTheme, My.Env.Lang.CP_Time, sw_all, !AppTheme.Enabled, My.Env.Lang.CP_Skip_AppTheme, true);

                        // Wallpaper
                        // Make Wallpaper before the following LogonUI items, to make a logonUI that depends on current wallpaper gets the correct file
                        this.Execute(new MethodInvoker(() => Wallpaper.Apply(false, ReportProgress_Detailed ? TreeView : null)), TreeView, My.Env.Lang.CP_Applying_Wallpaper, My.Env.Lang.CP_Error_Wallpaper, My.Env.Lang.CP_Time, sw_all, !Wallpaper.Enabled, My.Env.Lang.CP_Skip_Wallpaper);

                        if (My.Env.W11)
                        {
                            this.Execute(new MethodInvoker(() => Windows11.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, My.Env.Lang.CP_Applying_Win11, My.Env.Lang.CP_W11_Error, My.Env.Lang.CP_Time, sw_all);

                            this.Execute(new MethodInvoker(() => LogonUI10x.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, My.Env.Lang.CP_Applying_LogonUI11, My.Env.Lang.CP_LogonUI11_Error, My.Env.Lang.CP_Time, sw_all);
                        }

                        if (My.Env.W10)
                        {
                            this.Execute(new MethodInvoker(() => Windows10.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, My.Env.Lang.CP_Applying_Win10, My.Env.Lang.CP_W10_Error, My.Env.Lang.CP_Time, sw_all);

                            this.Execute(new MethodInvoker(() => LogonUI10x.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, My.Env.Lang.CP_Applying_LogonUI10, My.Env.Lang.CP_LogonUI10_Error, My.Env.Lang.CP_Time, sw_all);
                        }

                        if (My.Env.W8 | My.Env.W81)
                        {
                            this.Execute(new MethodInvoker(() =>
                                    {
                                        Windows81.Apply(ReportProgress_Detailed ? TreeView : null);
                                        RefreshDWM(this);
                                    }), TreeView, My.Env.Lang.CP_Applying_Win81, My.Env.Lang.CP_W81_Error, My.Env.Lang.CP_Time, sw_all);


                            this.Execute(new MethodInvoker(() => Apply_LogonUI_8(TreeView)), TreeView, My.Env.Lang.CP_Applying_LogonUI8, My.Env.Lang.CP_LogonUI8_Error, My.Env.Lang.CP_Time, sw_all);
                        }

                        if (My.Env.W7)
                        {
                            this.Execute(new MethodInvoker(() =>
                                    {
                                        Windows7.Apply(ReportProgress_Detailed ? TreeView : null);
                                        RefreshDWM(this);
                                    }), TreeView, My.Env.Lang.CP_Applying_Win7, My.Env.Lang.CP_W7_Error, My.Env.Lang.CP_Time, sw_all);

                            this.Execute(new MethodInvoker(() => Apply_LogonUI7(LogonUI7, "LogonUI", TreeView)), TreeView, My.Env.Lang.CP_Applying_LogonUI7, My.Env.Lang.CP_LogonUI7_Error, My.Env.Lang.CP_Time, sw_all);
                        }

                        if (My.Env.WVista)
                        {
                            this.Execute(new MethodInvoker(() =>
                                    {
                                        WindowsVista.Apply(ReportProgress_Detailed ? TreeView : null);
                                        RefreshDWM(this);
                                    }), TreeView, My.Env.Lang.CP_Applying_WinVista, My.Env.Lang.CP_WVista_Error, My.Env.Lang.CP_Time, sw_all);
                        }

                        if (My.Env.WXP)
                        {
                            this.Execute(new MethodInvoker(() => WindowsXP.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, My.Env.Lang.CP_Applying_WinXP, My.Env.Lang.CP_WXP_Error, My.Env.Lang.CP_Time, sw_all);

                            this.Execute(new MethodInvoker(() => LogonUIXP.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, My.Env.Lang.CP_Applying_LogonUIXP, My.Env.Lang.CP_LogonUIXP_Error, My.Env.Lang.CP_Time, sw_all);
                        }

                        // Win32UI
                        this.Execute(new MethodInvoker(() => Win32.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, My.Env.Lang.CP_Applying_Win32UI, My.Env.Lang.CP_WIN32UI_Error, My.Env.Lang.CP_Time, sw_all);

                        // WindowsEffects
                        this.Execute(new MethodInvoker(() => WindowsEffects.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, My.Env.Lang.CP_Applying_WinEffects, My.Env.Lang.CP_WinEffects_Error, My.Env.Lang.CP_Time, sw_all);

                        // Metrics\Fonts
                        this.Execute(new MethodInvoker(() => MetricsFonts.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, My.Env.Lang.CP_Applying_Metrics, My.Env.Lang.CP_Error_Metrics, My.Env.Lang.CP_Time_They, sw_all, !MetricsFonts.Enabled, My.Env.Lang.CP_Skip_Metrics);

                        // AltTab
                        this.Execute(new MethodInvoker(() => AltTab.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, My.Env.Lang.CP_Applying_AltTab, My.Env.Lang.CP_Error_AltTab, My.Env.Lang.CP_Time, sw_all, !AltTab.Enabled, My.Env.Lang.CP_Skip_AltTab, true);

                        // WallpaperTone
                        this.Execute(new MethodInvoker(() =>
                                {
                                    WallpaperTone.Save_To_Registry(WallpaperTone_W11, "Win11", ReportProgress_Detailed ? TreeView : null);
                                    WallpaperTone.Save_To_Registry(WallpaperTone_W10, "Win10", ReportProgress_Detailed ? TreeView : null);
                                    WallpaperTone.Save_To_Registry(WallpaperTone_W81, "Win8.1", ReportProgress_Detailed ? TreeView : null);
                                    WallpaperTone.Save_To_Registry(WallpaperTone_W7, "Win7", ReportProgress_Detailed ? TreeView : null);
                                    WallpaperTone.Save_To_Registry(WallpaperTone_WVista, "WinVista", ReportProgress_Detailed ? TreeView : null);
                                    WallpaperTone.Save_To_Registry(WallpaperTone_WXP, "WinXP", ReportProgress_Detailed ? TreeView : null);

                                    if (Wallpaper.Enabled)
                                    {
                                        if (My.Env.W11 & WallpaperTone_W11.Enabled)
                                            WallpaperTone_W11.Apply(ReportProgress_Detailed ? TreeView : null);
                                        if (My.Env.W10 & WallpaperTone_W10.Enabled)
                                            WallpaperTone_W10.Apply(ReportProgress_Detailed ? TreeView : null);
                                        if (My.Env.W81 & WallpaperTone_W81.Enabled)
                                            WallpaperTone_W81.Apply(ReportProgress_Detailed ? TreeView : null);
                                        if (My.Env.W7 & WallpaperTone_W7.Enabled)
                                            WallpaperTone_W7.Apply(ReportProgress_Detailed ? TreeView : null);
                                        if (My.Env.WVista & WallpaperTone_WVista.Enabled)
                                            WallpaperTone_WVista.Apply(ReportProgress_Detailed ? TreeView : null);
                                        if (My.Env.WXP & WallpaperTone_WXP.Enabled)
                                            WallpaperTone_WXP.Apply(ReportProgress_Detailed ? TreeView : null);
                                    }

                                }), TreeView, My.Env.Lang.CP_Applying_WallpaperTone, My.Env.Lang.CP_WallpaperTone_Error, My.Env.Lang.CP_Time, sw_all);

                        #region Consoles
                        EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_CMD_Enabled", CommandPrompt.Enabled);
                        EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_PS_32_Enabled", PowerShellx86.Enabled);
                        EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_PS_64_Enabled", PowerShellx64.Enabled);
                        EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_Stable_Enabled", Terminal.Enabled);
                        EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_Preview_Enabled", TerminalPreview.Enabled);

                        this.Execute(new MethodInvoker(() => Apply_CommandPrompt(ReportProgress_Detailed ? TreeView : null)), TreeView, My.Env.Lang.CP_Applying_CMD, My.Env.Lang.CP_CMD_Error, My.Env.Lang.CP_Time, sw_all, !CommandPrompt.Enabled, My.Env.Lang.CP_Skip_CMD);

                        this.Execute(new MethodInvoker(() => Apply_PowerShell86(ReportProgress_Detailed ? TreeView : null)), TreeView, My.Env.Lang.CP_Applying_PS32, My.Env.Lang.CP_PS32_Error, My.Env.Lang.CP_Time, sw_all, !PowerShellx86.Enabled, My.Env.Lang.CP_Skip_PS32);

                        this.Execute(new MethodInvoker(() => Apply_PowerShell64(ReportProgress_Detailed ? TreeView : null)), TreeView, My.Env.Lang.CP_Applying_PS64, My.Env.Lang.CP_PS64_Error, My.Env.Lang.CP_Time, sw_all, !PowerShellx64.Enabled, My.Env.Lang.CP_Skip_PS64);
                        #endregion

                        #region Windows Terminal
                        var sw = new Stopwatch();
                        sw.Reset();
                        sw.Start();
                        if (My.Env.W10 | My.Env.W11)
                        {

                            if (ReportProgress)
                            {
                                if (Terminal.Enabled & TerminalPreview.Enabled)
                                {
                                    AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), My.Env.Lang.CP_Check_Terminals), "info");
                                }

                                else if (Terminal.Enabled)
                                {
                                    AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), My.Env.Lang.CP_Skip_TerminalPreview), "skip");
                                    AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), My.Env.Lang.CP_Check_TerminalStable), "info");
                                }

                                else if (TerminalPreview.Enabled)
                                {
                                    AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), My.Env.Lang.CP_Skip_TerminalStable), "skip");
                                    AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), My.Env.Lang.CP_Check_TerminalPreview), "info");
                                }

                                else
                                {
                                    AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), My.Env.Lang.CP_Skip_Terminals), "skip");

                                }

                            }

                            string TerDir;
                            string TerPreDir;

                            if (!My.Env.Settings.WindowsTerminals.Path_Deflection)
                            {
                                TerDir = My.Env.PATH_TerminalJSON;
                                TerPreDir = My.Env.PATH_TerminalPreviewJSON;
                            }
                            else
                            {
                                if (System.IO.File.Exists(My.Env.Settings.WindowsTerminals.Terminal_Stable_Path))
                                {
                                    TerDir = My.Env.Settings.WindowsTerminals.Terminal_Stable_Path;
                                }
                                else
                                {
                                    TerDir = My.Env.PATH_TerminalJSON;
                                }

                                if (System.IO.File.Exists(My.Env.Settings.WindowsTerminals.Terminal_Preview_Path))
                                {
                                    TerPreDir = My.Env.Settings.WindowsTerminals.Terminal_Preview_Path;
                                }
                                else
                                {
                                    TerPreDir = My.Env.PATH_TerminalPreviewJSON;
                                }
                            }

                            if (Terminal.Enabled)
                            {
                                if (System.IO.File.Exists(TerDir))
                                {

                                    try
                                    {
                                        AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), My.Env.Lang.CP_Applying_TerminalStable), "info");
                                        Terminal.Save(TerDir, WinTerminal.Mode.JSONFile);
                                        if (ReportProgress)
                                            AddNode(TreeView, string.Format(My.Env.Lang.CP_Time, sw.ElapsedMilliseconds / 1000d), "time");
                                    }
                                    catch (Exception ex)
                                    {
                                        sw.Stop();
                                        sw_all.Stop();
                                        _ErrorHappened = true;
                                        if (ReportProgress)
                                        {
                                            AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), My.Env.Lang.CP_Error_TerminalStable), "error");
                                            AddException(My.Env.Lang.CP_Error_TerminalStable, ex);
                                        }
                                        else
                                        {
                                            My.MyProject.Forms.BugReport.ThrowError(ex);
                                        }

                                        sw.Start();
                                        sw_all.Start();
                                    }
                                }


                                else if (!My.Env.Settings.WindowsTerminals.Path_Deflection)
                                {
                                    AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), My.Env.Lang.CP_Skip_TerminalStable_NotInstalled), "skip");
                                }
                                else
                                {
                                    AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), My.Env.Lang.CP_Skip_TerminalStable_DeflectionNotFound), "skip");

                                }
                            }

                            if (TerminalPreview.Enabled)
                            {
                                if (System.IO.File.Exists(TerPreDir))
                                {

                                    try
                                    {
                                        AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), My.Env.Lang.CP_Applying_TerminalPreview), "info");
                                        TerminalPreview.Save(TerPreDir, WinTerminal.Mode.JSONFile, WinTerminal.Version.Preview);
                                        if (ReportProgress)
                                            AddNode(TreeView, string.Format(My.Env.Lang.CP_Time, sw.ElapsedMilliseconds / 1000d), "time");
                                    }
                                    catch (Exception ex)
                                    {
                                        sw.Stop();
                                        sw_all.Stop();
                                        _ErrorHappened = true;
                                        if (ReportProgress)
                                        {
                                            AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), My.Env.Lang.CP_Error_TerminalPreview), "error");
                                            AddException(My.Env.Lang.CP_Error_TerminalPreview, ex);
                                        }
                                        else
                                        {
                                            My.MyProject.Forms.BugReport.ThrowError(ex);
                                        }

                                        sw.Start();
                                        sw_all.Start();
                                    }
                                }

                                else if (!My.Env.Settings.WindowsTerminals.Path_Deflection)
                                {
                                    AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), My.Env.Lang.CP_Skip_TerminalPreview_NotInstalled), "skip");
                                }
                                else
                                {
                                    AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), My.Env.Lang.CP_Skip_TerminalPreview_DeflectionNotFound), "skip");
                                }
                            }
                        }

                        else
                        {
                            AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), My.Env.Lang.CP_Skip_Terminals_NotSupported), "skip");
                        }
                        sw.Stop();
                        #endregion

                        // ScreenSaver
                        this.Execute(new MethodInvoker(() => ScreenSaver.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, My.Env.Lang.CP_Applying_ScreenSaver, My.Env.Lang.CP_Error_ScreenSaver, My.Env.Lang.CP_Time, sw_all);

                        // Sounds
                        this.Execute(new MethodInvoker(() => Sounds.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, My.Env.Lang.CP_Applying_Sounds, My.Env.Lang.CP_Error_Sounds, My.Env.Lang.CP_Time, sw_all, !Sounds.Enabled, My.Env.Lang.CP_Skip_Sounds);

                        // Cursors
                        this.Execute(new MethodInvoker(() => Apply_Cursors(TreeView)), TreeView, "", My.Env.Lang.CP_Error_Cursors, My.Env.Lang.CP_Time_Cursors, sw_all);

                        // Update LogonUI wallpaper in HKEY_USERS\.DEFAULT
                        if (My.Env.Settings.ThemeApplyingBehavior.Desktop_HKU_DEFAULT == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                        {

                            this.Execute(new MethodInvoker(() =>
                                    {
                                        EditReg(ReportProgress_Detailed ? TreeView : null, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "Wallpaper", GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", ""), RegistryValueKind.String);
                                        EditReg(ReportProgress_Detailed ? TreeView : null, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "WallpaperStyle", GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "WallpaperStyle", "2"), RegistryValueKind.String);
                                        EditReg(ReportProgress_Detailed ? TreeView : null, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "TileWallpaper", GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "TileWallpaper", "0"), RegistryValueKind.String);
                                        EditReg(ReportProgress_Detailed ? TreeView : null, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "Pattern", GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Pattern", ""), RegistryValueKind.String);
                                    }), TreeView, My.Env.Lang.CP_Applying_DesktopAllUsers, My.Env.Lang.CP_Error_SetDesktop, My.Env.Lang.CP_Time);
                        }

                        else if (My.Env.Settings.ThemeApplyingBehavior.Desktop_HKU_DEFAULT == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults)
                        {

                            this.Execute(new MethodInvoker(() =>
                                    {
                                        EditReg(ReportProgress_Detailed ? TreeView : null, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "Wallpaper", "", RegistryValueKind.String);
                                        EditReg(ReportProgress_Detailed ? TreeView : null, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "WallpaperStyle", "2", RegistryValueKind.String);
                                        EditReg(ReportProgress_Detailed ? TreeView : null, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "TileWallpaper", "0", RegistryValueKind.String);
                                        EditReg(ReportProgress_Detailed ? TreeView : null, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "Pattern", "", RegistryValueKind.String);
                                    }), TreeView, My.Env.Lang.CP_Applying_DesktopAllUsers, My.Env.Lang.CP_Error_SetDesktop, My.Env.Lang.CP_Time);

                        }

                        // Update User Preference Mask for HKEY_USERS\.DEFAULT
                        // Always make it the last operation
                        try
                        {
                            Win32.Update_UPM_DEFAULT(ReportProgress_Detailed ? TreeView : null);
                        }
                        catch
                        {
                        }

                        if (ReportProgress_Detailed)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SMT, "User32", "SendMessageTimeout", "HWND_BROADCAST", "WM_SETTINGCHANGE", "UIntPtr.Zero", "Marshal.StringToHGlobalAnsi(\"Environment\")", "SMTO_ABORTIFHUNG", MSG_TIMEOUT, "RESULT"), "dll");
                        User32.SendMessageTimeout(HWND_BROADCAST, WM_SETTINGCHANGE, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("Environment"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, (uint)MSG_TIMEOUT, out RESULT);

                        if (ReportProgress)
                        {
                            if (!_ErrorHappened)
                            {
                                AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), string.Format(My.Env.Lang.CP_Applied, sw_all.ElapsedMilliseconds / 1000d)), "success");
                            }
                            else
                            {
                                AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), string.Format(My.Env.Lang.CP_AppliedWithErrors, sw_all.ElapsedMilliseconds / 1000d)), "warning");
                            }
                        }

                        sw_all.Reset();
                        sw_all.Stop();
                        break;
                    }
                #endregion

                case CP_Type.File:
                    {
                        if (System.IO.File.Exists(File))
                        {
                            try
                            {
                                FileSystem.Kill(File);
                            }
                            catch
                            {
                            }
                        }

                        if (Info.ExportResThemePack)
                        {
                            PackThemeResources((CP)Clone(), File, new FileInfo(File).DirectoryName + @"\" + Path.GetFileNameWithoutExtension(File) + ".wptp");
                        }
                        else
                        {
                            System.IO.File.WriteAllText(File, ToString());
                        }

                        break;
                    }

            }
        }

        public string ToString(bool IgnoreCompression = false)
        {
            var JSON_Overall = new JObject();
            JSON_Overall.RemoveAll();

            Info.AppVersion = My.Env.AppVersion;

            foreach (FieldInfo field in GetType().GetFields(bindingFlags))
            {
                var type = field.FieldType;

                if (IsStructure(type))
                {
                    JSON_Overall.Add(field.Name, DeserializeProps(type, field.GetValue(this)));
                }
                else
                {
                    JSON_Overall.Add(field.Name, JToken.FromObject(field.GetValue(this)));
                }

            }

            if (My.Env.Settings.FileTypeManagement.CompressThemeFile && !IgnoreCompression)
            {
                return JSON_Overall.ToString().Compress();
            }
            else
            {
                return JSON_Overall.ToString();
            }
        }

        public bool IsStructure(Type type)
        {
            return type.IsValueType && !type.IsPrimitive && type.Namespace is not null && !type.Namespace.StartsWith("System.");
        }

        public void PackThemeResources(CP CP, string CP_File, string Package)
        {
            string cache = @"%WinPaletterAppData%\ThemeResPack_Cache\" + string.Concat(CP.Info.ThemeName.Replace(" ", "").Split(Path.GetInvalidFileNameChars())) + @"\";
            var filesList = new Dictionary<string, string>();
            filesList.Clear();
            string x;
            string ZipEntry;

            if (File.Exists(Package))
                File.Delete(Package);
            using (var archive = ZipFile.Open(Package, ZipArchiveMode.Create))
            {
                if (CP.LogonUI7.Enabled && CP.LogonUI7.Mode == Structures.LogonUI7.Modes.CustomImage || !CP.Windows81.NoLockScreen && CP.Windows81.LockScreenType == Structures.LogonUI7.Modes.CustomImage)
                {
                    x = CP.LogonUI7.ImagePath;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\Web", My.Env._ignore))
                    {
                        ZipEntry = cache + "LogonUI" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.LogonUI7.ImagePath = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }

                if (CP.Terminal.Enabled)
                {
                    x = CP.Terminal.DefaultProf.BackgroundImage;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\Web", My.Env._ignore))
                    {
                        ZipEntry = cache + "winterminal_defprofile_backimg" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Terminal.DefaultProf.BackgroundImage = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Terminal.DefaultProf.Icon;
                    if (!string.IsNullOrWhiteSpace(x) && !(x.Length <= 1) && !x.StartsWith(My.Env.PATH_Windows + @"\Web", My.Env._ignore))
                    {
                        ZipEntry = cache + "winterminal_defprofile_icon" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Terminal.DefaultProf.Icon = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    foreach (var i in CP.Terminal.Profiles)
                    {
                        x = i.BackgroundImage;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\Web", My.Env._ignore))
                        {
                            ZipEntry = cache + "winterminal_profile(" + string.Concat(i.Name.Replace(" ", "").Split(Path.GetInvalidFileNameChars())) + ")_backimg" + Path.GetExtension(x);
                            if (File.Exists(x))
                                i.BackgroundImage = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }

                        x = i.Icon;
                        if (!string.IsNullOrWhiteSpace(x) && !(x.Length <= 1) && !x.StartsWith(My.Env.PATH_Windows + @"\Web", My.Env._ignore))
                        {
                            ZipEntry = cache + "winterminal_profile(" + string.Concat(i.Name.Replace(" ", "").Split(Path.GetInvalidFileNameChars())) + ")_icon" + Path.GetExtension(x);
                            if (File.Exists(x))
                                i.Icon = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }
                    }
                }

                if (CP.TerminalPreview.Enabled)
                {
                    x = CP.TerminalPreview.DefaultProf.BackgroundImage;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\Web", My.Env._ignore))
                    {
                        ZipEntry = cache + "winterminal_preview_defprofile_backimg" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.TerminalPreview.DefaultProf.BackgroundImage = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.TerminalPreview.DefaultProf.Icon;
                    if (!string.IsNullOrWhiteSpace(x) && !(x.Length <= 1) && !x.StartsWith(My.Env.PATH_Windows + @"\Web", My.Env._ignore))
                    {
                        ZipEntry = cache + "winterminal_preview_defprofile_icon" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.TerminalPreview.DefaultProf.Icon = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    foreach (var i in CP.TerminalPreview.Profiles)
                    {
                        x = i.BackgroundImage;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\Web", My.Env._ignore))
                        {
                            ZipEntry = cache + "winterminal_preview_profile(" + string.Concat(i.Name.Replace(" ", "").Split(Path.GetInvalidFileNameChars())) + ")_backimg" + Path.GetExtension(x);
                            if (File.Exists(x))
                                i.BackgroundImage = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }

                        x = i.Icon;
                        if (!string.IsNullOrWhiteSpace(x) && !(x.Length <= 1) && !x.StartsWith(My.Env.PATH_Windows + @"\Web", My.Env._ignore))
                        {
                            ZipEntry = cache + "winterminal_preview_profile(" + string.Concat(i.Name.Replace(" ", "").Split(Path.GetInvalidFileNameChars())) + ")_icon" + Path.GetExtension(x);
                            if (File.Exists(x))
                                i.Icon = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }
                    }
                }

                if (CP.WallpaperTone_W11.Enabled)
                {
                    x = CP.WallpaperTone_W11.Image;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\Web", My.Env._ignore))
                    {
                        ZipEntry = cache + "wt_w11" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.WallpaperTone_W11.Image = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }

                if (CP.WallpaperTone_W10.Enabled)
                {
                    x = CP.WallpaperTone_W10.Image;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\Web", My.Env._ignore))
                    {
                        ZipEntry = cache + "wt_w10" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.WallpaperTone_W10.Image = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }

                if (CP.WallpaperTone_W81.Enabled)
                {
                    x = CP.WallpaperTone_W81.Image;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\Web", My.Env._ignore))
                    {
                        ZipEntry = cache + "wt_w81" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.WallpaperTone_W81.Image = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }

                if (CP.WallpaperTone_W7.Enabled)
                {
                    x = CP.WallpaperTone_W7.Image;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\Web", My.Env._ignore))
                    {
                        ZipEntry = cache + "wt_w7" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.WallpaperTone_W7.Image = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }

                if (CP.WallpaperTone_WVista.Enabled)
                {
                    x = CP.WallpaperTone_WVista.Image;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\Web", My.Env._ignore))
                    {
                        ZipEntry = cache + "wt_wvista" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.WallpaperTone_WVista.Image = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }

                if (CP.WallpaperTone_WXP.Enabled)
                {
                    x = CP.WallpaperTone_WXP.Image;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\Web", My.Env._ignore))
                    {
                        ZipEntry = cache + "wt_wxp" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.WallpaperTone_WXP.Image = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }

                if (CP.ScreenSaver.Enabled)
                {
                    x = CP.ScreenSaver.File;
                    if (!string.IsNullOrWhiteSpace(x))
                    {
                        ZipEntry = cache + "scrsvr" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.ScreenSaver.File = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }

                #region Sounds
                if (CP.Sounds.Enabled)
                {
                    x = CP.Sounds.Snd_Win_Default;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Default" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Default = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_AppGPFault;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_AppGPFault" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_AppGPFault = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_CCSelect;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_CCSelect" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_CCSelect = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_ChangeTheme;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_ChangeTheme" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_ChangeTheme = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Close;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Close" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Close = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_CriticalBatteryAlarm;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_CriticalBatteryAlarm" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_CriticalBatteryAlarm = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_DeviceConnect;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_DeviceConnect" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_DeviceConnect = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_DeviceDisconnect;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_DeviceDisconnect" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_DeviceDisconnect = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_DeviceFail;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_DeviceFail" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_DeviceFail = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_FaxBeep;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_FaxBeep" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_FaxBeep = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_LowBatteryAlarm;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_LowBatteryAlarm" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_LowBatteryAlarm = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_MailBeep;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_MailBeep" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_MailBeep = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Maximize;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Maximize" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Maximize = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_MenuCommand;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_MenuCommand" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_MenuCommand = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_MenuPopup;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_MenuPopup" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_MenuPopup = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_MessageNudge;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_MessageNudge" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_MessageNudge = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Minimize;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Minimize" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Minimize = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Notification_Default;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Default" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Notification_Default = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Notification_IM;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_IM" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Notification_IM = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Notification_Looping_Alarm;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Notification_Looping_Alarm = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Notification_Looping_Alarm10;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm10" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Notification_Looping_Alarm10 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Notification_Looping_Alarm2;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm2" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Notification_Looping_Alarm2 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Notification_Looping_Alarm3;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm3" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Notification_Looping_Alarm3 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Notification_Looping_Alarm4;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm4" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Notification_Looping_Alarm4 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Notification_Looping_Alarm5;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm5" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Notification_Looping_Alarm5 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Notification_Looping_Alarm6;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm6" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Notification_Looping_Alarm6 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Notification_Looping_Alarm7;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm7" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Notification_Looping_Alarm7 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Notification_Looping_Alarm8;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm8" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Notification_Looping_Alarm8 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Notification_Looping_Alarm9;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm9" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Notification_Looping_Alarm9 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Notification_Looping_Call;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Notification_Looping_Call = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Notification_Looping_Call10;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call10" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Notification_Looping_Call10 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Notification_Looping_Call2;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call2" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Notification_Looping_Call2 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Notification_Looping_Call3;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call3" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Notification_Looping_Call3 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Notification_Looping_Call4;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call4" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Notification_Looping_Call4 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Notification_Looping_Call5;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call5" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Notification_Looping_Call5 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Notification_Looping_Call6;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call6" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Notification_Looping_Call6 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Notification_Looping_Call7;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call7" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Notification_Looping_Call7 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Notification_Looping_Call8;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call8" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Notification_Looping_Call8 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Notification_Looping_Call9;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call9" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Notification_Looping_Call9 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Notification_Mail;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Mail" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Notification_Mail = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Notification_Proximity;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Proximity" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Notification_Proximity = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Notification_Reminder;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Reminder" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Notification_Reminder = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Notification_SMS;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_SMS" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Notification_SMS = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_Open;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Open" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_Open = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_PrintComplete;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_PrintComplete" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_PrintComplete = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_ProximityConnection;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_ProximityConnection" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_ProximityConnection = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_RestoreDown;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_RestoreDown" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_RestoreDown = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_RestoreUp;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_RestoreUp" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_RestoreUp = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_ShowBand;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_ShowBand" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_ShowBand = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_SystemAsterisk;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_SystemAsterisk" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_SystemAsterisk = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_SystemExclamation;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_SystemExclamation" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_SystemExclamation = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_SystemExit;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_SystemExit" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_SystemExit = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_SystemStart;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_SystemStart" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_SystemStart = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Imageres_SystemStart;
                    if (!string.IsNullOrWhiteSpace(x))  // Don't include the condition: Not x.StartsWith(My.PATH_Windows & "\media", My._ignore)
                    {
                        ZipEntry = cache + "Snd_Imageres_SystemStart" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Imageres_SystemStart = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_SystemHand;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_SystemHand" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_SystemHand = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_SystemNotification;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_SystemNotification" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_SystemNotification = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_SystemQuestion;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_SystemQuestion" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_SystemQuestion = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_WindowsLogoff;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_WindowsLogoff" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_WindowsLogoff = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_WindowsLogon;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_WindowsLogon" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_WindowsLogon = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_WindowsUAC;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_WindowsUAC" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_WindowsUAC = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Win_WindowsUnlock;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_WindowsUnlock" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Win_WindowsUnlock = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Explorer_ActivatingDocument;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Explorer_ActivatingDocument" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Explorer_ActivatingDocument = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Explorer_BlockedPopup;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Explorer_BlockedPopup" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Explorer_BlockedPopup = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Explorer_EmptyRecycleBin;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Explorer_EmptyRecycleBin" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Explorer_EmptyRecycleBin = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Explorer_FeedDiscovered;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Explorer_FeedDiscovered" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Explorer_FeedDiscovered = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Explorer_MoveMenuItem;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Explorer_MoveMenuItem" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Explorer_MoveMenuItem = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Explorer_Navigating;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Explorer_Navigating" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Explorer_Navigating = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Explorer_SecurityBand;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Explorer_SecurityBand" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Explorer_SecurityBand = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Explorer_SearchProviderDiscovered;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Explorer_SearchProviderDiscovered" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Explorer_SearchProviderDiscovered = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Explorer_FaxError;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Explorer_FaxError" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Explorer_FaxError = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Explorer_FaxLineRings;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Explorer_FaxLineRings" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Explorer_FaxLineRings = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Explorer_FaxNew;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Explorer_FaxNew" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Explorer_FaxNew = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_Explorer_FaxSent;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_Explorer_FaxSent" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_Explorer_FaxSent = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_NetMeeting_PersonJoins;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_NetMeeting_PersonJoins" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_NetMeeting_PersonJoins = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_NetMeeting_PersonLeaves;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_NetMeeting_PersonLeaves" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_NetMeeting_PersonLeaves = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_NetMeeting_ReceiveCall;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_NetMeeting_ReceiveCall" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_NetMeeting_ReceiveCall = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_NetMeeting_ReceiveRequestToJoin;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_NetMeeting_ReceiveRequestToJoin" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_NetMeeting_ReceiveRequestToJoin = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_SpeechRec_DisNumbersSound;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_SpeechRec_DisNumbersSound" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_SpeechRec_DisNumbersSound = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_SpeechRec_HubOffSound;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_SpeechRec_HubOffSound" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_SpeechRec_HubOffSound = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_SpeechRec_HubOnSound;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_SpeechRec_HubOnSound" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_SpeechRec_HubOnSound = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_SpeechRec_HubSleepSound;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_SpeechRec_HubSleepSound" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_SpeechRec_HubSleepSound = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_SpeechRec_MisrecoSound;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_SpeechRec_MisrecoSound" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_SpeechRec_MisrecoSound = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = CP.Sounds.Snd_SpeechRec_PanelSound;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\media", My.Env._ignore))
                    {
                        ZipEntry = cache + "Snd_SpeechRec_PanelSound" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Sounds.Snd_SpeechRec_PanelSound = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }
                #endregion

                if (CP.Wallpaper.Enabled && CP.Wallpaper.WallpaperType == Wallpaper.WallpaperTypes.Picture)
                {
                    x = CP.Wallpaper.ImageFile;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\Web", My.Env._ignore))
                    {
                        ZipEntry = cache + "wallpaper_file" + Path.GetExtension(x);
                        if (File.Exists(x))
                            CP.Wallpaper.ImageFile = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }

                foreach (var _file in filesList)
                {
                    if (File.Exists(_file.Value))
                        archive.CreateEntryFromFile(_file.Value, _file.Key.Split('\\').Last(), CompressionLevel.Optimal);
                }

                if (CP.WindowsXP.Theme == WindowsXP.Themes.Custom)
                {
                    x = CP.WindowsXP.ThemeFile;
                    if (!string.IsNullOrWhiteSpace(x) && File.Exists(x) && !x.StartsWith(My.Env.PATH_Windows + @"\Resources\Themes\Luna", My.Env._ignore))
                    {
                        ZipEntry = cache + @"WXP_VS\" + Path.GetFileName(x);
                        if (File.Exists(x))
                            CP.WindowsXP.ThemeFile = ZipEntry;
                        string DirName = new FileInfo(x).Directory.FullName;
                        foreach (string file in Directory.EnumerateFiles(DirName, "*.*", SearchOption.AllDirectories))
                        {
                            if (File.Exists(file))
                                archive.CreateEntryFromFile(file, "WXP_VS" + file.Replace(DirName, ""), CompressionLevel.Optimal);
                        }
                    }
                }

                if (CP.Wallpaper.Enabled && CP.Wallpaper.WallpaperType == Wallpaper.WallpaperTypes.SlideShow)
                {
                    if (CP.Wallpaper.SlideShow_Folder_or_ImagesList)
                    {
                        x = CP.Wallpaper.Wallpaper_Slideshow_ImagesRootPath;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\Web", My.Env._ignore))
                        {
                            CP.Wallpaper.Wallpaper_Slideshow_ImagesRootPath = cache + "wallpapers_slideshow";

                            foreach (var image in Directory.EnumerateFiles(x, "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".bmp") || s.EndsWith(".jpg") || s.EndsWith(".png") || s.EndsWith(".gif")))


                            {


                                if (File.Exists(image))
                                    archive.CreateEntryFromFile(image, @"wallpapers_slideshow\" + new FileInfo(image).Name, CompressionLevel.Optimal);

                            }

                        }
                    }

                    else
                    {
                        string[] arr = CP.Wallpaper.Wallpaper_Slideshow_Images.ToArray();
                        if (arr.Count() > 0)
                        {
                            if (!arr[0].StartsWith(My.Env.PATH_Windows + @"\Web", My.Env._ignore))
                            {
                                CP.Wallpaper.Wallpaper_Slideshow_ImagesRootPath = cache + "WallpapersList";
                                CP.Wallpaper.Wallpaper_Slideshow_Images = new string[] { };
                                for (int x0 = 0, loopTo = arr.Count() - 1; x0 <= loopTo; x0++)
                                {
                                    x = arr[x0];
                                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(My.Env.PATH_Windows + @"\Web", My.Env._ignore))
                                    {
                                        ZipEntry = cache + @"WallpapersList\wallpaperlist_" + x0 + "_file" + Path.GetExtension(x);
                                        if (File.Exists(x))
                                        {
                                            CP.Wallpaper.Wallpaper_Slideshow_Images = CP.Wallpaper.Wallpaper_Slideshow_Images.Append(ZipEntry).ToArray();
                                            archive.CreateEntryFromFile(x, @"WallpapersList\wallpaperlist_" + x0 + "_file" + Path.GetExtension(x), CompressionLevel.Optimal);
                                        }
                                    }
                                }
                            }
                        }
                    }

                }

                File.WriteAllText(CP_File, CP.ToString());
            }

        }

        public static IEnumerable<string> Decompress(string File)
        {
            IEnumerable<string> DecompressedData;

            try
            {
                DecompressedData = System.IO.File.ReadAllText(File).Decompress().CList();
            }
            catch
            {
                DecompressedData = System.IO.File.ReadAllText(File).CList();
            }

            return DecompressedData;
        }

        private JObject DeserializeProps(Type StructureType, object Structure)
        {
            var j = new JObject();

            j.RemoveAll();

            foreach (var field in StructureType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                JToken result;

                try
                {
                    result = JToken.FromObject(field.GetValue(Structure));
                }
                catch
                {
                    result = default;
                }

                j.Add(field.Name, result);
            }

            return j;
        }

        private static bool IsValidJson(string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput))
            {
                return false;
            }
            strInput = strInput.Trim();
            if (strInput.StartsWith("{") && strInput.EndsWith("}") || strInput.StartsWith("[") && strInput.EndsWith("]")) // For object
            {
                // For array
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    // Exception in parsing json
                    return false;
                }
                catch (Exception ex) // some other exception
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Applying Subs
        public static void Apply_LogonUI7(Structures.LogonUI7 LogonElement, string RegEntryHint = "LogonUI", TreeView TreeView = null)
        {

            bool ReportProgress = My.Env.Settings.ThemeLog.VerboseLevel != WPSettings.Structures.ThemeLog.VerboseLevels.None && TreeView is not null;
            bool ReportProgress_Detailed = ReportProgress && My.Env.Settings.ThemeLog.VerboseLevel == WPSettings.Structures.ThemeLog.VerboseLevels.Detailed;

            EditReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background", "OEMBackground", LogonElement.Enabled.ToInteger());
            EditReg(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\System", "UseOEMBackground", LogonElement.Enabled.ToInteger());

            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\" + RegEntryHint, "Mode", (int)LogonElement.Mode);
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\" + RegEntryHint, "ImagePath", LogonElement.ImagePath, RegistryValueKind.String);
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\" + RegEntryHint, "Color", LogonElement.Color.ToArgb());
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\" + RegEntryHint, "Blur", LogonElement.Blur.ToInteger());
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\" + RegEntryHint, "Blur_Intensity", LogonElement.Blur_Intensity);
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\" + RegEntryHint, "Grayscale", LogonElement.Grayscale.ToInteger());
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\" + RegEntryHint, "Noise", LogonElement.Noise.ToInteger());
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\" + RegEntryHint, "Noise_Mode", (int)LogonElement.Noise_Mode);
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\" + RegEntryHint, "Noise_Intensity", LogonElement.Noise_Intensity);

            if (LogonElement.Enabled)
            {
                IntPtr wow64Value = IntPtr.Zero;
                Kernel32.Wow64DisableWow64FsRedirection(ref wow64Value);

                string DirX = My.Env.PATH_System32 + @"\oobe\info\backgrounds";

                Directory.CreateDirectory(DirX);

                foreach (string fileX in My.MyProject.Computer.FileSystem.GetFiles(DirX))
                {
                    try
                    {
                        FileSystem.Kill(fileX);
                    }
                    catch
                    {
                    }
                }

                var bmpList = new List<Bitmap>();
                bmpList.Clear();

                if (ReportProgress_Detailed)
                    AddNode(TreeView, My.Env.Lang.Verbose_GetInstanceLogonUIImg, "info");

                switch (LogonElement.Mode)
                {
                    case Structures.LogonUI7.Modes.Default_:
                        {
                            for (int i = 5031; i <= 5043; i += +1)
                                bmpList.Add(PE_Functions.GetPNGFromDLL(My.Env.PATH_imageres, i, "IMAGE", My.MyProject.Computer.Screen.Bounds.Size.Width, My.MyProject.Computer.Screen.Bounds.Size.Height));
                            break;
                        }

                    case Structures.LogonUI7.Modes.CustomImage:
                        {
                            if (File.Exists(LogonElement.ImagePath))
                            {
                                bmpList.Add((Bitmap)Bitmap_Mgr.Load(LogonElement.ImagePath).Resize(My.MyProject.Computer.Screen.Bounds.Size));
                            }
                            else
                            {
                                bmpList.Add((Bitmap)Color.Black.ToBitmap(My.MyProject.Computer.Screen.Bounds.Size));
                            }

                            break;
                        }

                    case Structures.LogonUI7.Modes.SolidColor:
                        {
                            bmpList.Add((Bitmap)LogonElement.Color.ToBitmap(My.MyProject.Computer.Screen.Bounds.Size));
                            break;
                        }

                    case Structures.LogonUI7.Modes.Wallpaper:
                        {
                            using (Bitmap b = new Bitmap(My.MyProject.Application.GetWallpaper()))
                            {
                                bmpList.Add((Bitmap)b.Resize(My.MyProject.Computer.Screen.Bounds.Size).Clone());
                            }

                            break;
                        }

                }

                if (ReportProgress)
                    AddNode(TreeView, string.Format(My.Env.Lang.CP_RenderingCustomLogonUI_MayNotRespond), "info");

                for (int x = 0, loopTo = bmpList.Count - 1; x <= loopTo; x++)
                {
                    if (ReportProgress)
                        AddNode(TreeView, string.Format("{3}: " + My.Env.Lang.CP_RenderingCustomLogonUI_Progress + " {2} ({0}/{1})", x + 1, bmpList.Count, bmpList[x].Width + "x" + bmpList[x].Height, DateTime.Now.ToLongTimeString()), "info");

                    if (LogonElement.Grayscale)
                    {
                        if (ReportProgress_Detailed)
                            AddNode(TreeView, My.Env.Lang.Verbose_GrayscaleLogonUIImg, "apply");
                        bmpList[x] = bmpList[x].Grayscale();
                    }


                    if (LogonElement.Blur)
                    {
                        if (ReportProgress_Detailed)
                            AddNode(TreeView, My.Env.Lang.Verbose_BlurringLogonUIImg, "apply");

                        var imgF = new ImageProcessor.ImageFactory();
                        using (var b = new Bitmap(bmpList[x]))
                        {
                            imgF.Load(b);
                            imgF.GaussianBlur(LogonElement.Blur_Intensity);
                            bmpList[x] = (Bitmap)imgF.Image;
                        }

                    }

                    if (LogonElement.Noise)
                    {
                        if (ReportProgress_Detailed)
                            AddNode(TreeView, My.Env.Lang.Verbose_NoiseLogonUIImg, "apply");

                        bmpList[x] = bmpList[x].Noise(LogonElement.Noise_Mode, (float)(LogonElement.Noise_Intensity / 100d));
                    }
                }

                if (bmpList.Count == 1)
                {
                    bmpList[0].Save(DirX + @"\backgroundDefault.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    if (ReportProgress_Detailed)
                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_LogonUIImgSaved, DirX + @"\backgroundDefault.jpg"), "info");
                }
                else
                {
                    for (int x = 0, loopTo1 = bmpList.Count - 1; x <= loopTo1; x++)
                    {
                        bmpList[x].Save(DirX + string.Format(@"\background{0}x{1}.jpg", bmpList[x].Width, bmpList[x].Height), System.Drawing.Imaging.ImageFormat.Jpeg);
                        if (ReportProgress_Detailed)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_LogonUIImgNUMSaved, DirX + string.Format(@"\background{0}x{1}.jpg", bmpList[x].Width, bmpList[x].Height), x + 1), "info");

                    }
                }

                Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero);
            }
        }

        public void Apply_LogonUI_8(TreeView TreeView = null)
        {

            bool ReportProgress = My.Env.Settings.ThemeLog.VerboseLevel != WPSettings.Structures.ThemeLog.VerboseLevels.None && TreeView is not null;
            bool ReportProgress_Detailed = ReportProgress && My.Env.Settings.ThemeLog.VerboseLevel == WPSettings.Structures.ThemeLog.VerboseLevels.Detailed;

            string lockimg = My.Env.PATH_appData + @"\LockScreen.png";

            EditReg(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", Windows81.NoLockScreen.ToInteger());
            EditReg(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "LockScreenImage", lockimg, RegistryValueKind.String);

            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Mode", (int)Windows81.LockScreenType);
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Metro_LockScreenSystemID", Windows81.LockScreenSystemID);
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "ImagePath", LogonUI7.ImagePath, RegistryValueKind.String);
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Color", LogonUI7.Color.ToArgb());
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Blur", LogonUI7.Blur.ToInteger());
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Blur_Intensity", LogonUI7.Blur_Intensity);
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Grayscale", LogonUI7.Grayscale.ToInteger());
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Noise", LogonUI7.Noise.ToInteger());
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Noise_Mode", (int)LogonUI7.Noise_Mode);
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Noise_Intensity", LogonUI7.Noise_Intensity);

            if (!Windows81.NoLockScreen)
            {
                Bitmap bmp;

                if (ReportProgress_Detailed)
                    AddNode(TreeView, My.Env.Lang.Verbose_GetInstanceLockScreenImg, "info");

                switch (Windows81.LockScreenType)
                {
                    case Structures.LogonUI7.Modes.Default_:
                        {
                            string syslock = "";

                            if (File.Exists(string.Format(My.Env.PATH_Windows + @"\Web\Screen\img10{0}.png", My.Env.CP.Windows81.LockScreenSystemID)))
                            {
                                syslock = string.Format(My.Env.PATH_Windows + @"\Web\Screen\img10{0}.png", My.Env.CP.Windows81.LockScreenSystemID);
                            }

                            else if (File.Exists(string.Format(My.Env.PATH_Windows + @"\Web\Screen\img10{0}.jpg", My.Env.CP.Windows81.LockScreenSystemID)))
                            {
                                syslock = string.Format(My.Env.PATH_Windows + @"\Web\Screen\img10{0}.jpg", My.Env.CP.Windows81.LockScreenSystemID);

                            }

                            if (File.Exists(syslock))
                            {
                                bmp = Bitmap_Mgr.Load(syslock);
                            }
                            else
                            {
                                bmp = (Bitmap)Color.Black.ToBitmap(My.MyProject.Computer.Screen.Bounds.Size);
                            }

                            break;
                        }

                    case Structures.LogonUI7.Modes.CustomImage:
                        {
                            if (File.Exists(LogonUI7.ImagePath))
                            {
                                bmp = Bitmap_Mgr.Load(LogonUI7.ImagePath);
                            }
                            else
                            {
                                bmp = (Bitmap)Color.Black.ToBitmap(My.MyProject.Computer.Screen.Bounds.Size);
                            }

                            break;
                        }

                    case Structures.LogonUI7.Modes.SolidColor:
                        {
                            bmp = (Bitmap)LogonUI7.Color.ToBitmap(My.MyProject.Computer.Screen.Bounds.Size);
                            break;
                        }

                    case Structures.LogonUI7.Modes.Wallpaper:
                        {
                            using (var b = new Bitmap(My.MyProject.Application.GetWallpaper()))
                            {
                                bmp = (Bitmap)b.Clone();
                            }

                            break;
                        }

                    default:
                        {
                            bmp = (Bitmap)Color.Black.ToBitmap(My.MyProject.Computer.Screen.Bounds.Size);
                            break;
                        }

                }

                if (ReportProgress)
                    AddNode(TreeView, string.Format(My.Env.Lang.CP_RenderingCustomLogonUI_MayNotRespond), "info");

                if (ReportProgress)
                    AddNode(TreeView, string.Format("{0}:  " + My.Env.Lang.CP_RenderingCustomLogonUI, DateTime.Now.ToLongTimeString()), "info");

                if (LogonUI7.Grayscale)
                {
                    if (ReportProgress_Detailed)
                        AddNode(TreeView, My.Env.Lang.Verbose_GrayscaleLockScreenImg, "apply");
                    bmp = bmp.Grayscale();
                }

                if (LogonUI7.Blur)
                {
                    if (ReportProgress_Detailed)
                        AddNode(TreeView, My.Env.Lang.Verbose_BlurringLockScreenImg, "apply");
                    var imgF = new ImageProcessor.ImageFactory();
                    using (var b = new Bitmap(bmp))
                    {
                        imgF.Load(b);
                        imgF.GaussianBlur(LogonUI7.Blur_Intensity);
                        bmp = (Bitmap)imgF.Image;
                    }

                }

                if (LogonUI7.Noise)
                {
                    if (ReportProgress_Detailed)
                        AddNode(TreeView, My.Env.Lang.Verbose_NoiseLockScreenImg, "apply");
                    bmp = bmp.Noise(LogonUI7.Noise_Mode, (float)(LogonUI7.Noise_Intensity / 100d));
                }

                if (File.Exists(lockimg))
                    FileSystem.Kill(lockimg);

                if (ReportProgress_Detailed)
                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_LockScreenImgSaved, lockimg), "info");
                bmp.Save(lockimg);

            }

        }

        public void Apply_CommandPrompt(TreeView TreeView = null)
        {
            if (CommandPrompt.Enabled)
            {
                Structures.Console.Save_Console_To_Registry("HKEY_CURRENT_USER", "", CommandPrompt, TreeView);
                if (My.Env.Settings.ThemeApplyingBehavior.CMD_OverrideUserPreferences)
                    Structures.Console.Save_Console_To_Registry("HKEY_CURRENT_USER", "%SystemRoot%_System32_cmd.exe", CommandPrompt, TreeView);

                if (My.Env.Settings.ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                {
                    Structures.Console.Save_Console_To_Registry(@"HKEY_USERS\.DEFAULT", "", CommandPrompt, TreeView);
                    Structures.Console.Save_Console_To_Registry(@"HKEY_USERS\.DEFAULT", "%SystemRoot%_System32_cmd.exe", CommandPrompt, TreeView);
                }
            }
        }

        public void Apply_PowerShell86(TreeView TreeView = null)
        {
            if (PowerShellx86.Enabled & Directory.Exists(Environment.GetEnvironmentVariable("WINDIR") + @"\System32\WindowsPowerShell\v1.0"))
            {

                Structures.Console.Save_Console_To_Registry("HKEY_CURRENT_USER", "%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", PowerShellx86, TreeView);


                if (My.Env.Settings.ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                {
                    Structures.Console.Save_Console_To_Registry(@"HKEY_USERS\.DEFAULT", "%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", PowerShellx86, TreeView);
                }

            }
        }

        public void Apply_PowerShell64(TreeView TreeView = null)
        {
            if (PowerShellx64.Enabled & Directory.Exists(Environment.GetEnvironmentVariable("WINDIR") + @"\SysWOW64\WindowsPowerShell\v1.0"))
            {

                Structures.Console.Save_Console_To_Registry("HKEY_CURRENT_USER", "%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", PowerShellx64, TreeView);

                if (My.Env.Settings.ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                {
                    Structures.Console.Save_Console_To_Registry(@"HKEY_USERS\.DEFAULT", "%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", PowerShellx64, TreeView);
                }
            }
        }

        public void Apply_Cursors(TreeView TreeView = null)
        {
            bool ReportProgress = My.Env.Settings.ThemeLog.VerboseLevel != WPSettings.Structures.ThemeLog.VerboseLevels.None && TreeView is not null;
            bool ReportProgress_Detailed = ReportProgress && My.Env.Settings.ThemeLog.VerboseLevel == WPSettings.Structures.ThemeLog.VerboseLevels.Detailed;

            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors", "", Cursor_Enabled);

            var sw = new Stopwatch();
            if (ReportProgress)
                AddNode(TreeView, string.Format("{0}: " + My.Env.Lang.CP_SavingCursorsColors, DateTime.Now.ToLongTimeString()), "info");

            sw.Reset();
            sw.Start();

            Structures.Cursor.Save_Cursors_To_Registry("Arrow", Cursor_Arrow, ReportProgress_Detailed ? TreeView : null);
            Structures.Cursor.Save_Cursors_To_Registry("Help", Cursor_Help, ReportProgress_Detailed ? TreeView : null);
            Structures.Cursor.Save_Cursors_To_Registry("AppLoading", Cursor_AppLoading, ReportProgress_Detailed ? TreeView : null);
            Structures.Cursor.Save_Cursors_To_Registry("Busy", Cursor_Busy, ReportProgress_Detailed ? TreeView : null);
            Structures.Cursor.Save_Cursors_To_Registry("Move", Cursor_Move, ReportProgress_Detailed ? TreeView : null);
            Structures.Cursor.Save_Cursors_To_Registry("NS", Cursor_NS, ReportProgress_Detailed ? TreeView : null);
            Structures.Cursor.Save_Cursors_To_Registry("EW", Cursor_EW, ReportProgress_Detailed ? TreeView : null);
            Structures.Cursor.Save_Cursors_To_Registry("NESW", Cursor_NESW, ReportProgress_Detailed ? TreeView : null);
            Structures.Cursor.Save_Cursors_To_Registry("NWSE", Cursor_NWSE, ReportProgress_Detailed ? TreeView : null);
            Structures.Cursor.Save_Cursors_To_Registry("Up", Cursor_Up, ReportProgress_Detailed ? TreeView : null);
            Structures.Cursor.Save_Cursors_To_Registry("Pen", Cursor_Pen, ReportProgress_Detailed ? TreeView : null);
            Structures.Cursor.Save_Cursors_To_Registry("None", Cursor_None, ReportProgress_Detailed ? TreeView : null);
            Structures.Cursor.Save_Cursors_To_Registry("Link", Cursor_Link, ReportProgress_Detailed ? TreeView : null);
            Structures.Cursor.Save_Cursors_To_Registry("Pin", Cursor_Pin, ReportProgress_Detailed ? TreeView : null);
            Structures.Cursor.Save_Cursors_To_Registry("Person", Cursor_Person, ReportProgress_Detailed ? TreeView : null);
            Structures.Cursor.Save_Cursors_To_Registry("IBeam", Cursor_IBeam, ReportProgress_Detailed ? TreeView : null);
            Structures.Cursor.Save_Cursors_To_Registry("Cross", Cursor_Cross, ReportProgress_Detailed ? TreeView : null);

            if (ReportProgress)
                AddNode(TreeView, string.Format(My.Env.Lang.CP_Time, sw.ElapsedMilliseconds / 1000d), "time");
            sw.Stop();

            if (Cursor_Enabled)
            {
                this.Execute(new MethodInvoker(() => ExportCursors(this, TreeView)), TreeView, My.Env.Lang.CP_RenderingCursors, My.Env.Lang.CP_RenderingCursors_Error, My.Env.Lang.CP_Time);

                if (My.Env.Settings.ThemeApplyingBehavior.AutoApplyCursors)
                {
                    this.Execute(new MethodInvoker(() =>
                        {
                            if (TreeView is not null)
                                AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Cursors.SETCURSORSHADOW.ToString(), 0, Cursor_Shadow, SPIF.UpdateINIFile.ToString()), "dll");
                            SystemParametersInfo((int)SPI.Cursors.SETCURSORSHADOW, 0, Cursor_Shadow, (int)SPIF.UpdateINIFile);

                            if (TreeView is not null)
                                AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Cursors.SETMOUSESONAR.ToString(), 0, Cursor_Sonar, SPIF.UpdateINIFile.ToString()), "dll");
                            SystemParametersInfo((int)SPI.Cursors.SETMOUSESONAR, 0, Cursor_Sonar, (int)SPIF.UpdateINIFile);

                            if (TreeView is not null)
                                AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Cursors.SETMOUSETRAILS.ToString(), 0, Cursor_Trails, SPIF.UpdateINIFile.ToString()), "dll");
                            SystemParametersInfo((int)SPI.Cursors.SETMOUSETRAILS, Cursor_Trails, 0, (int)SPIF.UpdateINIFile);

                            ApplyCursorsToReg("HKEY_CURRENT_USER", ReportProgress_Detailed ? TreeView : null);

                            if (My.Env.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                            {
                                EditReg(@"HKEY_USERS\.DEFAULT\Control Panel\Mouse", "MouseTrails", Cursor_Trails);
                                ApplyCursorsToReg(@"HKEY_USERS\.DEFAULT", ReportProgress_Detailed ? TreeView : null);
                            }

                        }), TreeView, My.Env.Lang.CP_ApplyingCursors, My.Env.Lang.CP_CursorsApplying_Error, My.Env.Lang.CP_Time);
                }
                else if (ReportProgress)
                    AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), My.Env.Lang.CP_Restricted_Cursors), "error");
            }

            else if (My.Env.Settings.ThemeApplyingBehavior.ResetCursorsToAero)
            {
                if (!My.Env.WXP)
                {
                    ResetCursorsToAero("HKEY_CURRENT_USER", ReportProgress_Detailed ? TreeView : null);
                    if (My.Env.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                        ResetCursorsToAero(@"HKEY_USERS\.DEFAULT");
                }

                else
                {
                    ResetCursorsToNone_XP("HKEY_CURRENT_USER", ReportProgress_Detailed ? TreeView : null);
                    if (My.Env.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                        ResetCursorsToNone_XP(@"HKEY_USERS\.DEFAULT");

                }

            }

        }
        #endregion

        #region Cursors Render
        public void ExportCursors(CP CP, TreeView TreeView = null)
        {
            bool ReportProgress = My.Env.Settings.ThemeLog.VerboseLevel != WPSettings.Structures.ThemeLog.VerboseLevels.None && TreeView is not null;
            bool ReportProgress_Detailed = ReportProgress && My.Env.Settings.ThemeLog.VerboseLevel == WPSettings.Structures.ThemeLog.VerboseLevels.Detailed;

            try
            {
                RenderCursor(Paths.CursorType.Arrow, CP, ReportProgress_Detailed ? TreeView : null);
            }
            catch
            {
            }
            try
            {
                RenderCursor(Paths.CursorType.Help, CP, ReportProgress_Detailed ? TreeView : null);
            }
            catch
            {
            }
            try
            {
                RenderCursor(Paths.CursorType.AppLoading, CP, ReportProgress_Detailed ? TreeView : null);
            }
            catch
            {
            }
            try
            {
                RenderCursor(Paths.CursorType.Busy, CP, ReportProgress_Detailed ? TreeView : null);
            }
            catch
            {
            }
            try
            {
                RenderCursor(Paths.CursorType.Pen, CP, ReportProgress_Detailed ? TreeView : null);
            }
            catch
            {
            }
            try
            {
                RenderCursor(Paths.CursorType.None, CP, ReportProgress_Detailed ? TreeView : null);
            }
            catch
            {
            }
            try
            {
                RenderCursor(Paths.CursorType.Move, CP, ReportProgress_Detailed ? TreeView : null);
            }
            catch
            {
            }
            try
            {
                RenderCursor(Paths.CursorType.Up, CP, ReportProgress_Detailed ? TreeView : null);
            }
            catch
            {
            }
            try
            {
                RenderCursor(Paths.CursorType.NS, CP, ReportProgress_Detailed ? TreeView : null);
            }
            catch
            {
            }
            try
            {
                RenderCursor(Paths.CursorType.EW, CP, ReportProgress_Detailed ? TreeView : null);
            }
            catch
            {
            }
            try
            {
                RenderCursor(Paths.CursorType.NESW, CP, ReportProgress_Detailed ? TreeView : null);
            }
            catch
            {
            }
            try
            {
                RenderCursor(Paths.CursorType.NWSE, CP, ReportProgress_Detailed ? TreeView : null);
            }
            catch
            {
            }
            try
            {
                RenderCursor(Paths.CursorType.Link, CP, ReportProgress_Detailed ? TreeView : null);
            }
            catch
            {
            }
            try
            {
                RenderCursor(Paths.CursorType.Pin, CP, ReportProgress_Detailed ? TreeView : null);
            }
            catch
            {
            }
            try
            {
                RenderCursor(Paths.CursorType.Person, CP, ReportProgress_Detailed ? TreeView : null);
            }
            catch
            {
            }
            try
            {
                RenderCursor(Paths.CursorType.IBeam, CP, ReportProgress_Detailed ? TreeView : null);
            }
            catch
            {
            }
            try
            {
                RenderCursor(Paths.CursorType.Cross, CP, ReportProgress_Detailed ? TreeView : null);
            }
            catch
            {
            }
        }

        public void RenderCursor(Paths.CursorType Type, CP CP, TreeView TreeView = null)
        {

            string CurName = "";

            switch (Type)
            {
                case Paths.CursorType.Arrow:
                    {
                        CurName = "Arrow";
                        break;
                    }

                case Paths.CursorType.Help:
                    {
                        CurName = "Help";
                        break;
                    }

                case Paths.CursorType.Busy:
                    {
                        CurName = "Busy";
                        break;
                    }

                case Paths.CursorType.AppLoading:
                    {
                        CurName = "AppLoading";
                        break;
                    }

                case Paths.CursorType.None:
                    {
                        CurName = "None";
                        break;
                    }

                case Paths.CursorType.Move:
                    {
                        CurName = "Move";
                        break;
                    }

                case Paths.CursorType.Up:
                    {
                        CurName = "Up";
                        break;
                    }

                case Paths.CursorType.NS:
                    {
                        CurName = "NS";
                        break;
                    }

                case Paths.CursorType.EW:
                    {
                        CurName = "EW";
                        break;
                    }

                case Paths.CursorType.NESW:
                    {
                        CurName = "NESW";
                        break;
                    }

                case Paths.CursorType.NWSE:
                    {
                        CurName = "NWSE";
                        break;
                    }

                case Paths.CursorType.Pen:
                    {
                        CurName = "Pen";
                        break;
                    }

                case Paths.CursorType.Link:
                    {
                        CurName = "Link";
                        break;
                    }

                case Paths.CursorType.Pin:
                    {
                        CurName = "Pin";
                        break;
                    }

                case Paths.CursorType.Person:
                    {
                        CurName = "Person";
                        break;
                    }

                case Paths.CursorType.IBeam:
                    {
                        CurName = "IBeam";
                        break;
                    }

                case Paths.CursorType.Cross:
                    {
                        CurName = "Cross";
                        break;
                    }

            }

            if (TreeView is not null)
                AddNode(TreeView, string.Format(My.Env.Lang.Verbose_RenderingCursor, CurName), "pe_patch");

            if (!(Type == Paths.CursorType.Busy) & !(Type == Paths.CursorType.AppLoading))
            {

                if (!Directory.Exists(My.Env.PATH_CursorsWP))
                    Directory.CreateDirectory(My.Env.PATH_CursorsWP);
                string Path = string.Format(My.Env.PATH_CursorsWP + @"\{0}.cur", CurName);

                var fs = new FileStream(Path, FileMode.Create);
                var EO = new EOIcoCurWriter(fs, 7, EOIcoCurWriter.IcoCurType.Cursor);

                for (float i = 1f; i <= 4f; i += 0.5f)
                {
                    var bmp = new Bitmap((int)Math.Round(32f * i), (int)Math.Round(32f * i), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                    var HotPoint = new Point(1, 1);

                    switch (Type)
                    {
                        case Paths.CursorType.Arrow:
                            {
                                var CurOptions = new CursorOptions(Cursor_Arrow) { Cursor = Paths.CursorType.Arrow, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1, 1);
                                break;
                            }

                        case Paths.CursorType.Help:
                            {
                                var CurOptions = new CursorOptions(Cursor_Help) { Cursor = Paths.CursorType.Help, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1, 1);
                                break;
                            }

                        case Paths.CursorType.None:
                            {
                                var CurOptions = new CursorOptions(Cursor_None) { Cursor = Paths.CursorType.None, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(8f * i), 1 + (int)Math.Round(8f * i));
                                break;
                            }

                        case Paths.CursorType.Move:
                            {
                                var CurOptions = new CursorOptions(Cursor_Move) { Cursor = Paths.CursorType.Move, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(11f * i), 1 + (int)Math.Round(11f * i));
                                break;
                            }

                        case Paths.CursorType.Up:
                            {
                                var CurOptions = new CursorOptions(Cursor_Up) { Cursor = Paths.CursorType.Up, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(4f * i), 1);
                                break;
                            }

                        case Paths.CursorType.NS:
                            {
                                var CurOptions = new CursorOptions(Cursor_NS) { Cursor = Paths.CursorType.NS, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(4f * i), 1 + (int)Math.Round(11f * i));
                                break;
                            }

                        case Paths.CursorType.EW:
                            {
                                var CurOptions = new CursorOptions(Cursor_EW) { Cursor = Paths.CursorType.EW, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point((int)Math.Round(1f + 11f * i), (int)Math.Round(1f + 4f * i));
                                break;
                            }

                        case Paths.CursorType.NESW:
                            {
                                var CurOptions = new CursorOptions(Cursor_NESW) { Cursor = Paths.CursorType.NESW, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(8f * i), 1 + (int)Math.Round(8f * i));
                                break;
                            }

                        case Paths.CursorType.NWSE:
                            {
                                var CurOptions = new CursorOptions(Cursor_NWSE) { Cursor = Paths.CursorType.NWSE, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(8f * i), 1 + (int)Math.Round(8f * i));
                                break;
                            }

                        case Paths.CursorType.Pen:
                            {
                                var CurOptions = new CursorOptions(Cursor_Pen) { Cursor = Paths.CursorType.Pen, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1, 1);
                                break;
                            }

                        case Paths.CursorType.Link:
                            {
                                var CurOptions = new CursorOptions(Cursor_Link) { Cursor = Paths.CursorType.Link, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(6f * i), CurOptions.ArrowStyle != Paths.ArrowStyle.Classic ? 1 : 2);
                                break;
                            }

                        case Paths.CursorType.Pin:
                            {
                                var CurOptions = new CursorOptions(Cursor_Pin) { Cursor = Paths.CursorType.Pin, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(6f * i), CurOptions.ArrowStyle != Paths.ArrowStyle.Classic ? 1 : 2);
                                break;
                            }

                        case Paths.CursorType.Person:
                            {
                                var CurOptions = new CursorOptions(Cursor_Person) { Cursor = Paths.CursorType.Person, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(6f * i), CurOptions.ArrowStyle != Paths.ArrowStyle.Classic ? 1 : 2);
                                break;
                            }

                        case Paths.CursorType.IBeam:
                            {
                                var CurOptions = new CursorOptions(Cursor_IBeam) { Cursor = Paths.CursorType.IBeam, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(4f * i), 1 + (int)Math.Round(9f * i));
                                break;
                            }

                        case Paths.CursorType.Cross:
                            {
                                var CurOptions = new CursorOptions(Cursor_Cross) { Cursor = Paths.CursorType.Cross, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(9f * i), 1 + (int)Math.Round(9f * i));
                                break;
                            }

                    }

                    EO.WriteBitmap(bmp, null, HotPoint);

                }

                fs.Close();

                if (TreeView is not null)
                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_CursorRenderedInto, Path), "info");
            }

            else
            {
                var HotPoint = new Point(1, 1);

                for (float i = 1f; i <= 4f; i += 1f)
                {
                    var BMPList = new List<Bitmap>();
                    BMPList.Clear();

                    #region Add angles bitmaps from 180 deg to 180 deg (Cycle)

                    for (int ang = 180; ang <= 360; ang += +10)
                    {
                        Bitmap bm;

                        if (Type == Paths.CursorType.AppLoading)
                        {
                            var CurOptions = new CursorOptions(Cursor_AppLoading) { Cursor = Paths.CursorType.AppLoading, LineThickness = 1f, Scale = i, _Angle = ang };
                            bm = new Bitmap(Paths.Draw(CurOptions));
                            HotPoint = new Point(1, 1 + (int)Math.Round(8f * i));
                        }

                        else
                        {
                            var CurOptions = new CursorOptions(Cursor_Busy) { Cursor = Paths.CursorType.Busy, LineThickness = 1f, Scale = i, _Angle = ang };
                            bm = new Bitmap(Paths.Draw(CurOptions));

                            HotPoint = new Point((CurOptions.CircleStyle != Paths.CircleStyle.Classic ? 1 : 2) + (int)Math.Round(11f * i), 1 + (int)Math.Round(11f * i));

                        }

                        BMPList.Add(bm);
                    }

                    for (int ang = 0; ang <= 180; ang += +10)
                    {
                        Bitmap bm;

                        if (Type == Paths.CursorType.AppLoading)
                        {
                            var CurOptions = new CursorOptions(Cursor_AppLoading) { Cursor = Paths.CursorType.AppLoading, LineThickness = 1f, Scale = i, _Angle = ang };
                            bm = new Bitmap(Paths.Draw(CurOptions));
                            HotPoint = new Point(1, 1 + (int)Math.Round(8f * i));
                        }

                        else
                        {
                            var CurOptions = new CursorOptions(Cursor_Busy) { Cursor = Paths.CursorType.Busy, LineThickness = 1f, Scale = i, _Angle = ang };
                            bm = new Bitmap(Paths.Draw(CurOptions));
                            HotPoint = new Point((CurOptions.CircleStyle != Paths.CircleStyle.Classic ? 1 : 2) + (int)Math.Round(11f * i), 1 + (int)Math.Round(11f * i));

                        }

                        BMPList.Add(bm);
                    }

                    #endregion

                    int Count = BMPList.Count;
                    uint[] frameRates = new uint[Count];
                    uint[] seqNums = new uint[Count];
                    int Speed = 2;

                    for (int ixx = 0, loopTo = Count - 1; ixx <= loopTo; ixx++)
                    {
                        frameRates[ixx] = Convert.ToUInt32(Speed);
                        seqNums[ixx] = (uint)ixx;
                    }

                    if (!Directory.Exists(My.Env.PATH_CursorsWP))
                        Directory.CreateDirectory(My.Env.PATH_CursorsWP);
                    var fs = new FileStream(string.Format(My.Env.PATH_CursorsWP + @"\{0}_{1}x.ani", CurName, i), FileMode.Create);

                    var AN = new EOANIWriter(fs, (uint)Count, (uint)Speed, frameRates, seqNums, null, null, HotPoint);

                    for (int ix = 0, loopTo1 = Count - 1; ix <= loopTo1; ix++)
                        AN.WriteFrame32(BMPList[ix]);

                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_CursorRenderedInto, string.Format(My.Env.PATH_CursorsWP + @"\{0}_{1}x.ani", CurName, i)), "info");

                    fs.Close();
                }

            }

        }

        public void ApplyCursorsToReg(string scopeReg = "HKEY_CURRENT_USER", TreeView TreeView = null)
        {
            string Path = My.Env.PATH_CursorsWP;

            string RegValue;
            RegValue = string.Format(@"{0}\{1}", Path, "Arrow.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "Help.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "AppLoading_1x.ani");
            RegValue += string.Format(@",{0}\{1}", Path, "Busy_1x.ani");
            RegValue += string.Format(@",{0}\{1}", Path, "Cross.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "IBeam.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "Pen.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "None.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "NS.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "EW.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "NWSE.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "NESW.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "Move.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "Up.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "Link.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "Pin.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "Person.cur");

            EditReg(scopeReg + @"\Control Panel\Cursors\Schemes", "WinPaletter", RegValue, RegistryValueKind.String);
            EditReg(scopeReg + @"\Control Panel\Cursors", "", "WinPaletter", RegistryValueKind.String);
            EditReg(scopeReg + @"\Control Panel\Cursors", "CursorBaseSize", 32, RegistryValueKind.DWord);
            EditReg(scopeReg + @"\Control Panel\Cursors", "Scheme Source", 1, RegistryValueKind.DWord);

            string x = string.Format(@"{0}\{1}", Path, "AppLoading_1x.ani");
            EditReg(scopeReg + @"\Control Panel\Cursors", "AppStarting", x, RegistryValueKind.String);

            if (TreeView is not null)
                AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_APPSTARTING.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_APPSTARTING);

            x = string.Format(@"{0}\{1}", Path, "Arrow.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "Arrow", x, RegistryValueKind.String);
            if (TreeView is not null)
                AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_NORMAL.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_NORMAL);

            x = string.Format(@"{0}\{1}", Path, "Cross.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "Crosshair", x, RegistryValueKind.String);
            if (TreeView is not null)
                AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_CROSS.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_CROSS);

            x = string.Format(@"{0}\{1}", Path, "Link.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "Hand", x, RegistryValueKind.String);
            if (TreeView is not null)
                AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_HAND.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_HAND);

            x = string.Format(@"{0}\{1}", Path, "Help.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "Help", x, RegistryValueKind.String);
            if (TreeView is not null)
                AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_HELP.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_HELP);

            x = string.Format(@"{0}\{1}", Path, "IBeam.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "IBeam", x, RegistryValueKind.String);
            if (TreeView is not null)
                AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_IBEAM.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_IBEAM);

            x = string.Format(@"{0}\{1}", Path, "None.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "No", x, RegistryValueKind.String);
            if (TreeView is not null)
                AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_NO.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_NO);

            x = string.Format(@"{0}\{1}", Path, "Pen.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "NWPen", x, RegistryValueKind.String);
            // User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_)

            x = string.Format(@"{0}\{1}", Path, "Person.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "Person", x, RegistryValueKind.String);
            // User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

            x = string.Format(@"{0}\{1}", Path, "Pin.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "Pin", x, RegistryValueKind.String);
            // User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

            x = string.Format(@"{0}\{1}", Path, "Move.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "SizeAll", x, RegistryValueKind.String);
            if (TreeView is not null)
                AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZEALL.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZEALL);

            x = string.Format(@"{0}\{1}", Path, "NESW.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "SizeNESW", x, RegistryValueKind.String);
            if (TreeView is not null)
                AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZENESW.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZENESW);

            x = string.Format(@"{0}\{1}", Path, "NS.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "SizeNS", x, RegistryValueKind.String);
            if (TreeView is not null)
                AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZENS.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZENS);

            x = string.Format(@"{0}\{1}", Path, "NWSE.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "SizeNWSE", x, RegistryValueKind.String);
            if (TreeView is not null)
                AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZENWSE.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZENWSE);

            x = string.Format(@"{0}\{1}", Path, "EW.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "SizeWE", x, RegistryValueKind.String);
            if (TreeView is not null)
                AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZEWE.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZEWE);

            x = string.Format(@"{0}\{1}", Path, "Up.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "UpArrow", x, RegistryValueKind.String);
            if (TreeView is not null)
                AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_UP.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_UP);

            x = string.Format(@"{0}\{1}", Path, "Busy_1x.ani");
            EditReg(scopeReg + @"\Control Panel\Cursors", "Wait", x, RegistryValueKind.String);
            if (TreeView is not null)
                AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_WAIT.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_WAIT);

            if (TreeView is not null)
                AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Cursors.SETCURSORS.ToString(), 0, 0, SPIF.UpdateINIFile.ToString()), "dll");
            SystemParametersInfo((int)SPI.Cursors.SETCURSORS, 0, 0, (int)(SPIF.UpdateINIFile | SPIF.UpdateINIFile));
        }

        public static void ResetCursorsToAero(string scopeReg = "HKEY_CURRENT_USER", TreeView TreeView = null)
        {
            try
            {
                string path = @"%SystemRoot%\Cursors";

                if (scopeReg.ToUpper() == "HKEY_CURRENT_USER")
                {
                    if (Registry.CurrentUser.OpenSubKey(@"Control Panel\Cursors\Schemes", false) is not null)
                    {
                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(My.Env.Lang.Verbose_DelCursorWPFromReg, @"HKEY_CURRENT_USER\Control Panel\Cursors\Schemes"), "reg_delete");
                        var rx = Registry.CurrentUser.OpenSubKey(@"Control Panel\Cursors\Schemes", true);
                        rx.DeleteValue("WinPaletter", false);
                        rx.Close();
                    }
                }

                EditReg(scopeReg + @"\Control Panel\Cursors", "", "Windows Default", RegistryValueKind.String);
                EditReg(scopeReg + @"\Control Panel\Cursors", "CursorBaseSize", 32, RegistryValueKind.DWord);
                EditReg(scopeReg + @"\Control Panel\Cursors", "Scheme Source", 2, RegistryValueKind.DWord);

                string x = string.Format(@"{0}\{1}", path, "aero_working.ani");
                EditReg(scopeReg + @"\Control Panel\Cursors", "AppStarting", x, RegistryValueKind.String);
                if (File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_APPSTARTING.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_APPSTARTING);
                }

                x = string.Format(@"{0}\{1}", path, "aero_arrow.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "Arrow", x, RegistryValueKind.String);
                if (File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_NORMAL.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_NORMAL);
                }

                x = string.Format("");
                EditReg(scopeReg + @"\Control Panel\Cursors", "Crosshair", x, RegistryValueKind.String);
                if (File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_CROSS.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_CROSS);
                }

                x = string.Format(@"{0}\{1}", path, "aero_link.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "Hand", x, RegistryValueKind.String);
                if (File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_HAND.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_HAND);
                }

                x = string.Format(@"{0}\{1}", path, "aero_helpsel.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "Help", x, RegistryValueKind.String);
                if (File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_HELP.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_HELP);
                }

                x = string.Format("");
                EditReg(scopeReg + @"\Control Panel\Cursors", "IBeam", x, RegistryValueKind.String);
                if (File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_IBEAM.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_IBEAM);
                }

                x = string.Format(@"{0}\{1}", path, "aero_unavail.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "No", x, RegistryValueKind.String);
                if (File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_NO.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_NO);
                }

                x = string.Format(@"{0}\{1}", path, "aero_pen.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "NWPen", x, RegistryValueKind.String);
                // If IO.File.Exists(X) then User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_)

                x = string.Format(@"{0}\{1}", path, "aero_person.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "Person", x, RegistryValueKind.String);
                // If IO.File.Exists(X) then User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

                x = string.Format(@"{0}\{1}", path, "aero_pin.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "Pin", x, RegistryValueKind.String);
                // If IO.File.Exists(X) then User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

                x = string.Format(@"{0}\{1}", path, "aero_move.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeAll", x, RegistryValueKind.String);
                if (File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZEALL.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZEALL);
                }

                x = string.Format(@"{0}\{1}", path, "aero_nesw.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeNESW", x, RegistryValueKind.String);
                if (File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZENESW.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZENESW);
                }

                x = string.Format(@"{0}\{1}", path, "aero_ns.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeNS", x, RegistryValueKind.String);
                if (File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZENS.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZENS);
                }

                x = string.Format(@"{0}\{1}", path, "aero_nwse.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeNWSE", x, RegistryValueKind.String);
                if (File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZENWSE.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZENWSE);
                }

                x = string.Format(@"{0}\{1}", path, "aero_ew.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeWE", x, RegistryValueKind.String);
                if (File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZEWE.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZEWE);
                }

                x = string.Format(@"{0}\{1}", path, "aero_up.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "UpArrow", x, RegistryValueKind.String);
                if (File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_UP.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_UP);
                }

                x = string.Format(@"{0}\{1}", path, "aero_busy.ani");
                EditReg(scopeReg + @"\Control Panel\Cursors", "Wait", x, RegistryValueKind.String);
                if (File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_WAIT.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_WAIT);
                }

                if (TreeView is not null)
                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Cursors.SETCURSORS.ToString(), 0, 0, SPIF.UpdateINIFile.ToString()), "dll");
                SystemParametersInfo((int)SPI.Cursors.SETCURSORS, 0, 0, (int)(SPIF.UpdateINIFile | SPIF.UpdateINIFile));
            }

            catch (Exception ex)
            {

                if (MsgBox(My.Env.Lang.CP_RestoreCursorsError, MessageBoxButtons.OKCancel, MessageBoxIcon.Error, My.Env.Lang.CP_RestoreCursorsErrorPressOK, "", "", "", "", My.Env.Lang.CP_RestoreCursorsTip, Ookii.Dialogs.WinForms.TaskDialogIcon.Information) == DialogResult.OK)
                    My.MyProject.Forms.BugReport.ThrowError(ex);

            }

        }

        public static void ResetCursorsToNone_XP(string scopeReg = "HKEY_CURRENT_USER", TreeView TreeView = null)
        {
            try
            {
                string path = @"%SystemRoot%\Cursors";

                if (scopeReg.ToUpper() == "HKEY_CURRENT_USER")
                {
                    try
                    {
                        if (Registry.CurrentUser.OpenSubKey(@"Control Panel\Cursors\Schemes", false) is not null)
                        {
                            if (TreeView is not null)
                                AddNode(TreeView, string.Format(My.Env.Lang.Verbose_DelCursorWPFromReg, @"HKEY_CURRENT_USER\Control Panel\Cursors\Schemes"), "reg_delete");
                            var rx = Registry.CurrentUser.OpenSubKey(@"Control Panel\Cursors\Schemes", true);
                            rx.DeleteValue("WinPaletter", false);
                            rx.Close();
                        }
                    }
                    finally
                    {
                        Registry.CurrentUser.Close();
                    }
                }

                EditReg(scopeReg + @"\Control Panel\Cursors", "", "Windows Default", RegistryValueKind.String);
                EditReg(scopeReg + @"\Control Panel\Cursors", "CursorBaseSize", 32, RegistryValueKind.DWord);
                EditReg(scopeReg + @"\Control Panel\Cursors", "Scheme Source", 2, RegistryValueKind.DWord);

                string x = "";
                EditReg(scopeReg + @"\Control Panel\Cursors", "AppStarting", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_APPSTARTING.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_APPSTARTING);

                EditReg(scopeReg + @"\Control Panel\Cursors", "Arrow", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_NORMAL.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_NORMAL);

                EditReg(scopeReg + @"\Control Panel\Cursors", "Crosshair", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_CROSS.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_CROSS);

                EditReg(scopeReg + @"\Control Panel\Cursors", "Hand", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_HAND.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_HAND);

                EditReg(scopeReg + @"\Control Panel\Cursors", "Help", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_HELP.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_HELP);

                EditReg(scopeReg + @"\Control Panel\Cursors", "IBeam", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_IBEAM.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_IBEAM);

                EditReg(scopeReg + @"\Control Panel\Cursors", "No", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_NO.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_NO);

                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeAll", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZEALL.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZEALL);

                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeNESW", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZENESW.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZENESW);

                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeNS", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZENS.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZENS);

                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeNWSE", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZENWSE.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZENWSE);

                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeWE", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZEWE.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZEWE);

                EditReg(scopeReg + @"\Control Panel\Cursors", "UpArrow", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_UP.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_UP);

                EditReg(scopeReg + @"\Control Panel\Cursors", "Wait", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_WAIT.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_WAIT);

                if (TreeView is not null)
                    AddNode(TreeView, string.Format(My.Env.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Cursors.SETCURSORS.ToString(), 0, 0, SPIF.UpdateINIFile.ToString()), "dll");
                SystemParametersInfo((int)SPI.Cursors.SETCURSORS, 0, 0, (int)(SPIF.UpdateINIFile | SPIF.UpdateINIFile));
            }

            catch (Exception ex)
            {

                if (MsgBox(My.Env.Lang.CP_RestoreCursorsError, MessageBoxButtons.OKCancel, MessageBoxIcon.Error, My.Env.Lang.CP_RestoreCursorsErrorPressOK, "", "", "", "", My.Env.Lang.CP_RestoreCursorsTip, Ookii.Dialogs.WinForms.TaskDialogIcon.Information) == DialogResult.OK)
                    My.MyProject.Forms.BugReport.ThrowError(ex);

            }

        }
        #endregion

        #region Comparisons
        public override bool Equals(object obj)
        {
            bool _Equals = true;

            if (Info != ((CP)obj).Info)
                _Equals = false;
            if (Windows11 != ((CP)obj).Windows11)
                _Equals = false;
            if (LogonUI10x != ((CP)obj).LogonUI10x)
                _Equals = false;
            if (Windows81 != ((CP)obj).Windows81)
                _Equals = false;
            if (Windows7 != ((CP)obj).Windows7)
                _Equals = false;
            if (WindowsVista != ((CP)obj).WindowsVista)
                _Equals = false;
            if (WindowsXP != ((CP)obj).WindowsXP)
                _Equals = false;
            if (LogonUI7 != ((CP)obj).LogonUI7)
                _Equals = false;
            if (LogonUIXP != ((CP)obj).LogonUIXP)
                _Equals = false;
            if (Win32 != ((CP)obj).Win32)
                _Equals = false;
            if (WindowsEffects != ((CP)obj).WindowsEffects)
                _Equals = false;
            if (MetricsFonts != ((CP)obj).MetricsFonts)
                _Equals = false;
            if (AltTab != ((CP)obj).AltTab)
                _Equals = false;
            if (WallpaperTone_W11 != ((CP)obj).WallpaperTone_W11)
                _Equals = false;
            if (WallpaperTone_W10 != ((CP)obj).WallpaperTone_W10)
                _Equals = false;
            if (WallpaperTone_W81 != ((CP)obj).WallpaperTone_W81)
                _Equals = false;
            if (WallpaperTone_W7 != ((CP)obj).WallpaperTone_W7)
                _Equals = false;
            if (WallpaperTone_WVista != ((CP)obj).WallpaperTone_WVista)
                _Equals = false;
            if (WallpaperTone_WXP != ((CP)obj).WallpaperTone_WXP)
                _Equals = false;
            if (ScreenSaver != ((CP)obj).ScreenSaver)
                _Equals = false;
            if (Sounds != ((CP)obj).Sounds)
                _Equals = false;
            if (Wallpaper != ((CP)obj).Wallpaper)
                _Equals = false;
            if (AppTheme != ((CP)obj).AppTheme)
                _Equals = false;

            if (Cursor_Enabled != ((CP)obj).Cursor_Enabled)
                _Equals = false;
            if (Cursor_Arrow != ((CP)obj).Cursor_Arrow)
                _Equals = false;
            if (Cursor_Help != ((CP)obj).Cursor_Help)
                _Equals = false;
            if (Cursor_AppLoading != ((CP)obj).Cursor_AppLoading)
                _Equals = false;
            if (Cursor_Busy != ((CP)obj).Cursor_Busy)
                _Equals = false;
            if (Cursor_Move != ((CP)obj).Cursor_Move)
                _Equals = false;
            if (Cursor_NS != ((CP)obj).Cursor_NS)
                _Equals = false;
            if (Cursor_EW != ((CP)obj).Cursor_EW)
                _Equals = false;
            if (Cursor_NESW != ((CP)obj).Cursor_NESW)
                _Equals = false;
            if (Cursor_NWSE != ((CP)obj).Cursor_NWSE)
                _Equals = false;
            if (Cursor_Up != ((CP)obj).Cursor_Up)
                _Equals = false;
            if (Cursor_Pen != ((CP)obj).Cursor_Pen)
                _Equals = false;
            if (Cursor_None != ((CP)obj).Cursor_None)
                _Equals = false;
            if (Cursor_Link != ((CP)obj).Cursor_Link)
                _Equals = false;
            if (Cursor_Pin != ((CP)obj).Cursor_Pin)
                _Equals = false;
            if (Cursor_Person != ((CP)obj).Cursor_Person)
                _Equals = false;
            if (Cursor_IBeam != ((CP)obj).Cursor_IBeam)
                _Equals = false;
            if (Cursor_Cross != ((CP)obj).Cursor_Cross)
                _Equals = false;

            if (CommandPrompt != ((CP)obj).CommandPrompt)
                _Equals = false;
            if (PowerShellx86 != ((CP)obj).PowerShellx86)
                _Equals = false;
            if (PowerShellx64 != ((CP)obj).PowerShellx64)
                _Equals = false;
            // If Terminal <> DirectCast(obj, CP).Terminal Then _Equals = False
            // If TerminalPreview <> DirectCast(obj, CP).TerminalPreview Then _Equals = False

            return _Equals;
        }

        public static bool operator ==(CP CP1, CP CP2) => (bool)CP1.Equals(CP2);
        public static bool operator !=(CP CP1, CP CP2) => !(CP1 == CP2);

        #endregion

    }
}