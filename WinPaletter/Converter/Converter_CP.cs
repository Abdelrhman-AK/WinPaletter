using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace WinPaletter
{

    public class Converter_CP
    {
        private readonly BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

        public class Structures
        {
            public struct Info
            {
                public string AppVersion;
                public string ThemeName;
                public string Description;
                public string ThemeVersion;
                public string Author;
                public string AuthorSocialMediaLink;

                public override string ToString()
                {
                    var tx = new List<string>();
                    tx.Clear();
                    tx.Add("<General>");
                    tx.Add("*Palette Name= " + ThemeName);
                    if (string.IsNullOrWhiteSpace(Description))
                    {
                        tx.Add("*Palette Description= ");
                    }
                    else
                    {
                        tx.Add("*Palette Description= " + Description.Replace("\r\n", "<br>"));
                    }
                    tx.Add("*Palette File Version= " + ThemeVersion);
                    tx.Add("*Author= " + Author);
                    tx.Add("*AuthorSocialMediaLink= " + AuthorSocialMediaLink);
                    tx.Add("</General>" + "\r\n");
                    return tx.CString();
                }
            }

            public struct Windows10x
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

                public string ToString(string Signature, string MiniSignature)
                {
                    var tx = new List<string>();
                    tx.Clear();
                    tx.Add(string.Format("<{0}>", Signature));
                    tx.Add(string.Format("*{0}_Color_Index0= {1}", MiniSignature, Color_Index0.ToArgb()));
                    tx.Add(string.Format("*{0}_Color_Index1= {1}", MiniSignature, Color_Index1.ToArgb()));
                    tx.Add(string.Format("*{0}_Color_Index2= {1}", MiniSignature, Color_Index2.ToArgb()));
                    tx.Add(string.Format("*{0}_Color_Index3= {1}", MiniSignature, Color_Index3.ToArgb()));
                    tx.Add(string.Format("*{0}_Color_Index4= {1}", MiniSignature, Color_Index4.ToArgb()));
                    tx.Add(string.Format("*{0}_Color_Index5= {1}", MiniSignature, Color_Index5.ToArgb()));
                    tx.Add(string.Format("*{0}_Color_Index6= {1}", MiniSignature, Color_Index6.ToArgb()));
                    tx.Add(string.Format("*{0}_Color_Index7= {1}", MiniSignature, Color_Index7.ToArgb()));
                    tx.Add(string.Format("*{0}_Titlebar_Active= {1}", MiniSignature, Titlebar_Active.ToArgb()));
                    tx.Add(string.Format("*{0}_Titlebar_Inactive= {1}", MiniSignature, Titlebar_Inactive.ToArgb()));
                    tx.Add(string.Format("*{0}_StartMenu_Accent= {1}", MiniSignature, StartMenu_Accent.ToArgb()));
                    tx.Add(string.Format("*{0}_WinMode_Light= {1}", MiniSignature, WinMode_Light));
                    tx.Add(string.Format("*{0}_AppMode_Light= {1}", MiniSignature, AppMode_Light));
                    tx.Add(string.Format("*{0}_Transparency= {1}", MiniSignature, Transparency));
                    tx.Add(string.Format("*{0}_IncreaseTBTransparency= {1}", MiniSignature, IncreaseTBTransparency));
                    tx.Add(string.Format("*{0}_TB_Blur= {1}", MiniSignature, TB_Blur));
                    tx.Add(string.Format("*{0}_ApplyAccentonTitlebars= {1}", MiniSignature, ApplyAccentOnTitlebars));
                    tx.Add(string.Format("*{0}_AccentOnStartTBAC= {1}", MiniSignature, (int)ApplyAccentOnTaskbar));
                    tx.Add(string.Format("</{0}>" + "\r\n", Signature));
                    return tx.CString();
                }

                public void FromListOfString(IEnumerable<string> Lines)
                {
                    foreach (string line in Lines)
                    {
                        string line_processed;

                        if (line.StartsWith("*Win_11_", (StringComparison)5))
                            line_processed = line.Remove(0, "*Win_11_".Count());

                        else if (line.StartsWith("*Win_10_", (StringComparison)5))
                            line_processed = line.Remove(0, "*Win_10_".Count());

                        else
                            line_processed = line.Remove(0, "*Win_11_".Count());

                        if (line_processed.StartsWith("Color_Index0= ", (StringComparison)5))
                            Color_Index0 = Color.FromArgb(Conversions.ToInteger(line_processed.Remove(0, "Color_Index0= ".Count())));
                        if (line_processed.StartsWith("Color_Index1= ", (StringComparison)5))
                            Color_Index1 = Color.FromArgb(Conversions.ToInteger(line_processed.Remove(0, "Color_Index1= ".Count())));
                        if (line_processed.StartsWith("Color_Index2= ", (StringComparison)5))
                            Color_Index2 = Color.FromArgb(Conversions.ToInteger(line_processed.Remove(0, "Color_Index2= ".Count())));
                        if (line_processed.StartsWith("Color_Index3= ", (StringComparison)5))
                            Color_Index3 = Color.FromArgb(Conversions.ToInteger(line_processed.Remove(0, "Color_Index3= ".Count())));
                        if (line_processed.StartsWith("Color_Index4= ", (StringComparison)5))
                            Color_Index4 = Color.FromArgb(Conversions.ToInteger(line_processed.Remove(0, "Color_Index4= ".Count())));
                        if (line_processed.StartsWith("Color_Index5= ", (StringComparison)5))
                            Color_Index5 = Color.FromArgb(Conversions.ToInteger(line_processed.Remove(0, "Color_Index5= ".Count())));
                        if (line_processed.StartsWith("Color_Index6= ", (StringComparison)5))
                            Color_Index6 = Color.FromArgb(Conversions.ToInteger(line_processed.Remove(0, "Color_Index6= ".Count())));
                        if (line_processed.StartsWith("Color_Index7= ", (StringComparison)5))
                            Color_Index7 = Color.FromArgb(Conversions.ToInteger(line_processed.Remove(0, "Color_Index7= ".Count())));
                        if (line_processed.StartsWith("WinMode_Light= ", (StringComparison)5))
                            WinMode_Light = Conversions.ToBoolean(line_processed.Remove(0, "WinMode_Light= ".Count()));
                        if (line_processed.StartsWith("AppMode_Light= ", (StringComparison)5))
                            AppMode_Light = Conversions.ToBoolean(line_processed.Remove(0, "AppMode_Light= ".Count()));
                        if (line_processed.StartsWith("Transparency= ", (StringComparison)5))
                            Transparency = Conversions.ToBoolean(line_processed.Remove(0, "Transparency= ".Count()));
                        if (line_processed.StartsWith("IncreaseTBTransparency= ", (StringComparison)5))
                            IncreaseTBTransparency = Conversions.ToBoolean(line_processed.Remove(0, "IncreaseTBTransparency= ".Count()));
                        if (line_processed.StartsWith("TB_Blur= ", (StringComparison)5))
                            TB_Blur = Conversions.ToBoolean(line_processed.Remove(0, "TB_Blur= ".Count()));
                        if (line_processed.StartsWith("Titlebar_Active= ", (StringComparison)5))
                            Titlebar_Active = Color.FromArgb(Conversions.ToInteger(line_processed.Remove(0, "Titlebar_Active= ".Count())));
                        if (line_processed.StartsWith("Titlebar_Inactive= ", (StringComparison)5))
                            Titlebar_Inactive = Color.FromArgb(Conversions.ToInteger(line_processed.Remove(0, "Titlebar_Inactive= ".Count())));
                        if (line_processed.StartsWith("StartMenu_Accent= ", (StringComparison)5))
                            StartMenu_Accent = Color.FromArgb(Conversions.ToInteger(line_processed.Remove(0, "StartMenu_Accent= ".Count())));
                        if (line_processed.StartsWith("ApplyAccentonTitlebars= ", (StringComparison)5))
                            ApplyAccentOnTitlebars = Conversions.ToBoolean(line_processed.Remove(0, "ApplyAccentonTitlebars= ".Count()));

                        if (line.StartsWith("AccentOnStartTBAC= ", (StringComparison)5))
                        {
                            switch (line.Remove(0, "AccentOnStartTBAC= ".Count()).ToLower() ?? "")
                            {
                                case "false":
                                    {
                                        ApplyAccentOnTaskbar = AccentTaskbarLevels.None;
                                        break;
                                    }

                                case "true":
                                    {
                                        ApplyAccentOnTaskbar = AccentTaskbarLevels.Taskbar_Start_AC;
                                        break;
                                    }

                                default:
                                    {
                                        switch (line.Remove(0, "AccentOnStartTBAC= ".Count()) ?? "")
                                        {
                                            case "0":
                                                {
                                                    ApplyAccentOnTaskbar = AccentTaskbarLevels.None;
                                                    break;
                                                }

                                            case "1":
                                                {
                                                    ApplyAccentOnTaskbar = AccentTaskbarLevels.Taskbar_Start_AC;
                                                    break;
                                                }

                                            case "2":
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

                                        break;
                                    }
                            }
                        }
                    }
                }
            }

            public struct Windows8
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
                public override string ToString()
                {
                    var tx = new List<string>();
                    tx.Clear();
                    tx.Add("<Metro>");
                    tx.Add("*Metro_ColorizationColor= " + ColorizationColor.ToArgb());
                    tx.Add("*Metro_ColorizationColorBalance= " + ColorizationColorBalance);
                    tx.Add("*Metro_PersonalColors_Background= " + PersonalColors_Background.ToArgb());
                    tx.Add("*Metro_PersonalColors_Accent= " + PersonalColors_Accent.ToArgb());
                    tx.Add("*Metro_StartColor= " + StartColor.ToArgb());
                    tx.Add("*Metro_AccentColor= " + AccentColor.ToArgb());
                    tx.Add("*Metro_Start= " + Start);
                    tx.Add("*Metro_Theme= " + (int)Theme);
                    tx.Add("*Metro_LogonUI= " + LogonUI);
                    tx.Add("*Metro_NoLockScreen= " + NoLockScreen);
                    tx.Add("*Metro_LockScreenType= " + (int)LockScreenType);
                    tx.Add("*Metro_LockScreenSystemID= " + LockScreenSystemID);
                    tx.Add("</Metro>" + "\r\n");

                    return tx.CString();
                }

                public void FromListOfString(IEnumerable<string> Lines)
                {
                    foreach (string line in Lines)
                    {
                        if (line.StartsWith("*Metro_ColorizationColor= ", (StringComparison)5))
                            ColorizationColor = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Metro_ColorizationColor= ".Count())));
                        if (line.StartsWith("*Metro_ColorizationColorBalance= ", (StringComparison)5))
                            ColorizationColorBalance = Conversions.ToInteger(line.Remove(0, "*Metro_ColorizationColorBalance= ".Count()));
                        if (line.StartsWith("*Metro_PersonalColors_Background= ", (StringComparison)5))
                            PersonalColors_Background = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Metro_PersonalColors_Background= ".Count())));
                        if (line.StartsWith("*Metro_PersonalColors_Accent= ", (StringComparison)5))
                            PersonalColors_Accent = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Metro_PersonalColors_Accent= ".Count())));
                        if (line.StartsWith("*Metro_StartColor= ", (StringComparison)5))
                            StartColor = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Metro_StartColor= ".Count())));
                        if (line.StartsWith("*Metro_AccentColor= ", (StringComparison)5))
                            AccentColor = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Metro_AccentColor= ".Count())));
                        if (line.StartsWith("*Metro_Start= ", (StringComparison)5))
                            Start = Conversions.ToInteger(line.Remove(0, "*Metro_Start= ".Count()));
                        if (line.StartsWith("*Metro_Theme= ", (StringComparison)5))
                            Theme = (Windows7.Themes)Conversions.ToInteger(line.Remove(0, "*Metro_Theme= ".Count()));
                        if (line.StartsWith("*Metro_LogonUI= ", (StringComparison)5))
                            LogonUI = Conversions.ToInteger(line.Remove(0, "*Metro_LogonUI= ".Count()));
                        if (line.StartsWith("*Metro_NoLockScreen= ", (StringComparison)5))
                            NoLockScreen = Conversions.ToBoolean(line.Remove(0, "*Metro_NoLockScreen= ".Count()));
                        if (line.StartsWith("*Metro_LockScreenType= ", (StringComparison)5))
                            LockScreenType = (LogonUI7.Modes)Conversions.ToInteger(line.Remove(0, "*Metro_LockScreenType= ".Count()));
                        if (line.StartsWith("*Metro_LockScreenSystemID= ", (StringComparison)5))
                            LockScreenSystemID = Conversions.ToInteger(line.Remove(0, "*Metro_LockScreenSystemID= ".Count()));
                    }
                }
            }

            public struct Windows7
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

                public enum Themes
                {
                    Aero,
                    AeroLite,
                    AeroOpaque,
                    Basic,
                    Classic
                }

                public override string ToString()
                {
                    var tx = new List<string>();
                    tx.Clear();
                    tx.Add("<Aero>");
                    tx.Add("*Aero_ColorizationColor= " + ColorizationColor.ToArgb());
                    tx.Add("*Aero_ColorizationAfterglow= " + ColorizationAfterglow.ToArgb());
                    tx.Add("*Aero_ColorizationColorBalance= " + ColorizationColorBalance);
                    tx.Add("*Aero_ColorizationAfterglowBalance= " + ColorizationAfterglowBalance);
                    tx.Add("*Aero_ColorizationBlurBalance= " + ColorizationBlurBalance);
                    tx.Add("*Aero_ColorizationGlassReflectionIntensity= " + ColorizationGlassReflectionIntensity);
                    tx.Add("*Aero_EnableAeroPeek= " + EnableAeroPeek);
                    tx.Add("*Aero_AlwaysHibernateThumbnails= " + AlwaysHibernateThumbnails);
                    tx.Add("*Aero_Theme= " + (int)Theme);
                    tx.Add("</Aero>" + "\r\n");
                    return tx.CString();
                }

                public void FromListOfString(IEnumerable<string> Lines)
                {
                    foreach (string line in Lines)
                    {
                        if (line.StartsWith("*Aero_ColorizationColor= ", (StringComparison)5))
                            ColorizationColor = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Aero_ColorizationColor= ".Count())));
                        if (line.StartsWith("*Aero_ColorizationAfterglow= ", (StringComparison)5))
                            ColorizationAfterglow = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Aero_ColorizationAfterglow= ".Count())));
                        if (line.StartsWith("*Aero_ColorizationColorBalance= ", (StringComparison)5))
                            ColorizationColorBalance = Conversions.ToInteger(line.Remove(0, "*Aero_ColorizationColorBalance= ".Count()));
                        if (line.StartsWith("*Aero_ColorizationAfterglowBalance= ", (StringComparison)5))
                            ColorizationAfterglowBalance = Conversions.ToInteger(line.Remove(0, "*Aero_ColorizationAfterglowBalance= ".Count()));
                        if (line.StartsWith("*Aero_ColorizationBlurBalance= ", (StringComparison)5))
                            ColorizationBlurBalance = Conversions.ToInteger(line.Remove(0, "*Aero_ColorizationBlurBalance= ".Count()));
                        if (line.StartsWith("*Aero_ColorizationGlassReflectionIntensity= ", (StringComparison)5))
                            ColorizationGlassReflectionIntensity = Conversions.ToInteger(line.Remove(0, "*Aero_ColorizationGlassReflectionIntensity= ".Count()));
                        if (line.StartsWith("*Aero_EnableAeroPeek= ", (StringComparison)5))
                            EnableAeroPeek = Conversions.ToBoolean(line.Remove(0, "*Aero_EnableAeroPeek= ".Count()));
                        if (line.StartsWith("*Aero_AlwaysHibernateThumbnails= ", (StringComparison)5))
                            AlwaysHibernateThumbnails = Conversions.ToBoolean(line.Remove(0, "*Aero_AlwaysHibernateThumbnails= ".Count()));
                        if (line.StartsWith("*Aero_Theme= ", (StringComparison)5))
                            Theme = (Themes)Conversions.ToInteger(line.Remove(0, "*Aero_Theme= ".Count()));
                    }
                }

            }

            public struct WindowsVista
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

                public override string ToString()
                {
                    var tx = new List<string>();
                    tx.Clear();
                    tx.Add("<Vista>");
                    tx.Add("*Vista_ColorizationColor= " + ColorizationColor.ToArgb());
                    tx.Add("*Vista_Alpha= " + Alpha);
                    tx.Add("*Vista_Theme= " + (int)Theme);
                    tx.Add("</Vista>" + "\r\n");
                    return tx.CString();
                }

                public void FromListOfString(IEnumerable<string> Lines)
                {
                    foreach (string line in Lines)
                    {
                        if (line.StartsWith("*Vista_ColorizationColor= ", (StringComparison)5))
                            ColorizationColor = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Vista_ColorizationColor= ".Count())));
                        if (line.StartsWith("*Vista_Alpha= ", (StringComparison)5))
                            Alpha = Conversions.ToByte(line.Remove(0, "*Vista_Alpha= ".Count()));
                        if (line.StartsWith("*Vista_Theme= ", (StringComparison)5))
                            Theme = (Windows7.Themes)Conversions.ToInteger(line.Remove(0, "*Vista_Theme= ".Count()));
                    }
                }

            }

            public struct WindowsXP
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

                public override string ToString()
                {
                    var tx = new List<string>();
                    tx.Clear();
                    tx.Add("<WinXP>");
                    tx.Add("*WinXP_Theme= " + (int)Theme);
                    tx.Add("*WinXP_ThemeFile= " + ThemeFile);
                    tx.Add("*WinXP_ColorScheme= " + ColorScheme);
                    tx.Add("</WinXP>" + "\r\n");
                    return tx.CString();
                }

                public void FromListOfString(IEnumerable<string> Lines)
                {
                    foreach (string line in Lines)
                    {
                        if (line.StartsWith("*WinXP_Theme= ", (StringComparison)5))
                            Theme = (Themes)Conversions.ToInteger(line.Remove(0, "*WinXP_Theme= ".Count()));
                        if (line.StartsWith("*WinXP_ThemeFile= ", (StringComparison)5))
                            ThemeFile = line.Remove(0, "*WinXP_ThemeFile= ".Count());
                        if (line.StartsWith("*WinXP_ColorScheme= ", (StringComparison)5))
                            ColorScheme = line.Remove(0, "*WinXP_ColorScheme= ".Count());
                    }
                }

            }

            public struct Win32UI
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

                public enum Method
                {
                    Registry,
                    File,
                    VisualStyles
                }

                public override string ToString()
                {
                    var tx = new List<string>();
                    tx.Clear();

                    tx.Add("<Win32UI>");
                    tx.Add("*Win32UI_EnableTheming= " + EnableTheming);
                    tx.Add("*Win32UI_EnableGradient= " + EnableGradient);
                    tx.Add("*Win32UI_ActiveBorder= " + ActiveBorder.ToArgb());
                    tx.Add("*Win32UI_ActiveTitle= " + ActiveTitle.ToArgb());
                    tx.Add("*Win32UI_AppWorkspace= " + AppWorkspace.ToArgb());
                    tx.Add("*Win32UI_Background= " + Background.ToArgb());
                    tx.Add("*Win32UI_ButtonAlternateFace= " + ButtonAlternateFace.ToArgb());
                    tx.Add("*Win32UI_ButtonDkShadow= " + ButtonDkShadow.ToArgb());
                    tx.Add("*Win32UI_ButtonFace= " + ButtonFace.ToArgb());
                    tx.Add("*Win32UI_ButtonHilight= " + ButtonHilight.ToArgb());
                    tx.Add("*Win32UI_ButtonLight= " + ButtonLight.ToArgb());
                    tx.Add("*Win32UI_ButtonShadow= " + ButtonShadow.ToArgb());
                    tx.Add("*Win32UI_ButtonText= " + ButtonText.ToArgb());
                    tx.Add("*Win32UI_GradientActiveTitle= " + GradientActiveTitle.ToArgb());
                    tx.Add("*Win32UI_GradientInactiveTitle= " + GradientInactiveTitle.ToArgb());
                    tx.Add("*Win32UI_GrayText= " + GrayText.ToArgb());
                    tx.Add("*Win32UI_HilightText= " + HilightText.ToArgb());
                    tx.Add("*Win32UI_HotTrackingColor= " + HotTrackingColor.ToArgb());
                    tx.Add("*Win32UI_InactiveBorder= " + InactiveBorder.ToArgb());
                    tx.Add("*Win32UI_InactiveTitle= " + InactiveTitle.ToArgb());
                    tx.Add("*Win32UI_InactiveTitleText= " + InactiveTitleText.ToArgb());
                    tx.Add("*Win32UI_InfoText= " + InfoText.ToArgb());
                    tx.Add("*Win32UI_InfoWindow= " + InfoWindow.ToArgb());
                    tx.Add("*Win32UI_Menu= " + Menu.ToArgb());
                    tx.Add("*Win32UI_MenuBar= " + MenuBar.ToArgb());
                    tx.Add("*Win32UI_MenuText= " + MenuText.ToArgb());
                    tx.Add("*Win32UI_Scrollbar= " + Scrollbar.ToArgb());
                    tx.Add("*Win32UI_TitleText= " + TitleText.ToArgb());
                    tx.Add("*Win32UI_Window= " + Window.ToArgb());
                    tx.Add("*Win32UI_WindowFrame= " + WindowFrame.ToArgb());
                    tx.Add("*Win32UI_WindowText= " + WindowText.ToArgb());
                    tx.Add("*Win32UI_Hilight= " + Hilight.ToArgb());
                    tx.Add("*Win32UI_MenuHilight= " + MenuHilight.ToArgb());
                    tx.Add("*Win32UI_Desktop= " + Desktop.ToArgb());
                    tx.Add("</Win32UI>" + "\r\n");

                    return tx.CString();
                }

                public void FromListOfString(IEnumerable<string> Lines)
                {
                    foreach (string line in Lines)
                    {
                        if (line.StartsWith("*Win32UI_EnableTheming= ", (StringComparison)5))
                            EnableTheming = Conversions.ToBoolean(line.Remove(0, "*Win32UI_EnableTheming= ".Count()));
                        if (line.StartsWith("*Win32UI_EnableGradient= ", (StringComparison)5))
                            EnableGradient = Conversions.ToBoolean(line.Remove(0, "*Win32UI_EnableGradient= ".Count()));
                        if (line.StartsWith("*Win32UI_ActiveBorder= ", (StringComparison)5))
                            ActiveBorder = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_ActiveBorder= ".Count())));
                        if (line.StartsWith("*Win32UI_ActiveTitle= ", (StringComparison)5))
                            ActiveTitle = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_ActiveTitle= ".Count())));
                        if (line.StartsWith("*Win32UI_AppWorkspace= ", (StringComparison)5))
                            AppWorkspace = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_AppWorkspace= ".Count())));
                        if (line.StartsWith("*Win32UI_Background= ", (StringComparison)5))
                            Background = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_Background= ".Count())));
                        if (line.StartsWith("*Win32UI_ButtonAlternateFace= ", (StringComparison)5))
                            ButtonAlternateFace = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_ButtonAlternateFace= ".Count())));
                        if (line.StartsWith("*Win32UI_ButtonDkShadow= ", (StringComparison)5))
                            ButtonDkShadow = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_ButtonDkShadow= ".Count())));
                        if (line.StartsWith("*Win32UI_ButtonFace= ", (StringComparison)5))
                            ButtonFace = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_ButtonFace= ".Count())));
                        if (line.StartsWith("*Win32UI_ButtonHilight= ", (StringComparison)5))
                            ButtonHilight = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_ButtonHilight= ".Count())));
                        if (line.StartsWith("*Win32UI_ButtonLight= ", (StringComparison)5))
                            ButtonLight = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_ButtonLight= ".Count())));
                        if (line.StartsWith("*Win32UI_ButtonShadow= ", (StringComparison)5))
                            ButtonShadow = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_ButtonShadow= ".Count())));
                        if (line.StartsWith("*Win32UI_ButtonText= ", (StringComparison)5))
                            ButtonText = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_ButtonText= ".Count())));
                        if (line.StartsWith("*Win32UI_GradientActiveTitle= ", (StringComparison)5))
                            GradientActiveTitle = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_GradientActiveTitle= ".Count())));
                        if (line.StartsWith("*Win32UI_GradientInactiveTitle= ", (StringComparison)5))
                            GradientInactiveTitle = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_GradientInactiveTitle= ".Count())));
                        if (line.StartsWith("*Win32UI_GrayText= ", (StringComparison)5))
                            GrayText = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_GrayText= ".Count())));
                        if (line.StartsWith("*Win32UI_HilightText= ", (StringComparison)5))
                            HilightText = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_HilightText= ".Count())));
                        if (line.StartsWith("*Win32UI_HotTrackingColor= ", (StringComparison)5))
                            HotTrackingColor = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_HotTrackingColor= ".Count())));
                        if (line.StartsWith("*Win32UI_InactiveBorder= ", (StringComparison)5))
                            InactiveBorder = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_InactiveBorder= ".Count())));
                        if (line.StartsWith("*Win32UI_InactiveTitle= ", (StringComparison)5))
                            InactiveTitle = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_InactiveTitle= ".Count())));
                        if (line.StartsWith("*Win32UI_InactiveTitleText= ", (StringComparison)5))
                            InactiveTitleText = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_InactiveTitleText= ".Count())));
                        if (line.StartsWith("*Win32UI_InfoText= ", (StringComparison)5))
                            InfoText = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_InfoText= ".Count())));
                        if (line.StartsWith("*Win32UI_InfoWindow= ", (StringComparison)5))
                            InfoWindow = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_InfoWindow= ".Count())));
                        if (line.StartsWith("*Win32UI_Menu= ", (StringComparison)5))
                            Menu = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_Menu= ".Count())));
                        if (line.StartsWith("*Win32UI_MenuBar= ", (StringComparison)5))
                            MenuBar = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_MenuBar= ".Count())));
                        if (line.StartsWith("*Win32UI_MenuText= ", (StringComparison)5))
                            MenuText = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_MenuText= ".Count())));
                        if (line.StartsWith("*Win32UI_Scrollbar= ", (StringComparison)5))
                            Scrollbar = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_Scrollbar= ".Count())));
                        if (line.StartsWith("*Win32UI_TitleText= ", (StringComparison)5))
                            TitleText = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_TitleText= ".Count())));
                        if (line.StartsWith("*Win32UI_Window= ", (StringComparison)5))
                            Window = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_Window= ".Count())));
                        if (line.StartsWith("*Win32UI_WindowFrame= ", (StringComparison)5))
                            WindowFrame = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_WindowFrame= ".Count())));
                        if (line.StartsWith("*Win32UI_WindowText= ", (StringComparison)5))
                            WindowText = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_WindowText= ".Count())));
                        if (line.StartsWith("*Win32UI_Hilight= ", (StringComparison)5))
                            Hilight = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_Hilight= ".Count())));
                        if (line.StartsWith("*Win32UI_MenuHilight= ", (StringComparison)5))
                            MenuHilight = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_MenuHilight= ".Count())));
                        if (line.StartsWith("*Win32UI_Desktop= ", (StringComparison)5))
                            Desktop = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Win32UI_Desktop= ".Count())));
                    }
                }

            }

            public struct WinEffects
            {
                public bool Enabled;

                public bool WindowAnimation;
                public bool WindowShadow;
                public bool WindowUIEffects;
                public bool ShowWinContentDrag;

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

                public override string ToString()
                {
                    var tx = new List<string>();
                    tx.Clear();
                    tx.Add("<WindowsEffects>");
                    tx.Add("*WinEffects_Enabled= " + Enabled);
                    tx.Add("*WinEffects_WindowAnimation= " + WindowAnimation);
                    tx.Add("*WinEffects_WindowShadow= " + WindowShadow);
                    tx.Add("*WinEffects_WindowUIEffects= " + WindowUIEffects);
                    tx.Add("*WinEffects_MenuAnimation= " + MenuAnimation);
                    tx.Add("*WinEffects_MenuFade= " + (int)MenuFade);
                    tx.Add("*WinEffects_MenuShowDelay= " + MenuShowDelay);
                    tx.Add("*WinEffects_MenuSelectionFade= " + MenuSelectionFade);
                    tx.Add("*WinEffects_ComboBoxAnimation= " + ComboBoxAnimation);
                    tx.Add("*WinEffects_ListboxSmoothScrolling= " + ListBoxSmoothScrolling);
                    tx.Add("*WinEffects_TooltipAnimation= " + TooltipAnimation);
                    tx.Add("*WinEffects_TooltipFade= " + (int)TooltipFade);
                    tx.Add("*WinEffects_IconsShadow= " + IconsShadow);
                    tx.Add("*WinEffects_IconsDesktopTranslSel= " + IconsDesktopTranslSel);
                    tx.Add("*WinEffects_ShowWinContentDrag= " + ShowWinContentDrag);
                    tx.Add("*WinEffects_KeyboardUnderline= " + KeyboardUnderline);
                    tx.Add("*WinEffects_FocusRectWidth= " + FocusRectWidth);
                    tx.Add("*WinEffects_FocusRectHeight= " + FocusRectHeight);
                    tx.Add("*WinEffects_Caret= " + Caret);
                    tx.Add("*WinEffects_NotificationDuration= " + NotificationDuration);
                    tx.Add("*WinEffects_ShakeToMinimize= " + ShakeToMinimize);
                    tx.Add("*WinEffects_AWT_Enabled= " + AWT_Enabled);
                    tx.Add("*WinEffects_AWT_BringActivatedWindowToTop= " + AWT_BringActivatedWindowToTop);
                    tx.Add("*WinEffects_AWT_Delay= " + AWT_Delay);
                    tx.Add("*WinEffects_SnapCursorToDefButton= " + SnapCursorToDefButton);
                    tx.Add("*WinEffects_Win11ClassicContextMenu= " + Win11ClassicContextMenu);
                    tx.Add("*WinEffects_SysListView32= " + SysListView32);
                    tx.Add("*WinEffects_ShowSecondsInSystemClock= " + ShowSecondsInSystemClock);
                    tx.Add("*WinEffects_BalloonNotifications= " + BalloonNotifications);
                    tx.Add("*WinEffects_PaintDesktopVersion= " + PaintDesktopVersion);
                    tx.Add("*WinEffects_Win11BootDots= " + Win11BootDots);
                    tx.Add("*WinEffects_Win11ExplorerBar= " + ((int)Win11ExplorerBar).ToString());
                    tx.Add("*WinEffects_DisableNavBar= " + DisableNavBar);
                    tx.Add("</WindowsEffects>" + "\r\n");
                    return tx.CString();
                }

                public void FromListOfString(IEnumerable<string> Lines)
                {
                    foreach (string line in Lines)
                    {
                        if (line.StartsWith("*WinEffects_Enabled= ", (StringComparison)5))
                            Enabled = Conversions.ToBoolean(line.Remove(0, "*WinEffects_Enabled= ".Count()));
                        if (line.StartsWith("*WinEffects_WindowAnimation= ", (StringComparison)5))
                            WindowAnimation = Conversions.ToBoolean(line.Remove(0, "*WinEffects_WindowAnimation= ".Count()));
                        if (line.StartsWith("*WinEffects_WindowShadow= ", (StringComparison)5))
                            WindowShadow = Conversions.ToBoolean(line.Remove(0, "*WinEffects_WindowShadow= ".Count()));
                        if (line.StartsWith("*WinEffects_WindowUIEffects= ", (StringComparison)5))
                            WindowUIEffects = Conversions.ToBoolean(line.Remove(0, "*WinEffects_WindowUIEffects= ".Count()));
                        if (line.StartsWith("*WinEffects_MenuAnimation= ", (StringComparison)5))
                            MenuAnimation = Conversions.ToBoolean(line.Remove(0, "*WinEffects_MenuAnimation= ".Count()));
                        if (line.StartsWith("*WinEffects_MenuFade= ", (StringComparison)5))
                            MenuFade = (MenuAnimType)Conversions.ToInteger(line.Remove(0, "*WinEffects_MenuFade= ".Count()));
                        if (line.StartsWith("*WinEffects_MenuShowDelay= ", (StringComparison)5))
                            MenuShowDelay = Conversions.ToUInteger(line.Remove(0, "*WinEffects_MenuShowDelay= ".Count()));
                        if (line.StartsWith("*WinEffects_MenuSelectionFade= ", (StringComparison)5))
                            MenuSelectionFade = Conversions.ToBoolean(line.Remove(0, "*WinEffects_MenuSelectionFade= ".Count()));
                        if (line.StartsWith("*WinEffects_ComboBoxAnimation= ", (StringComparison)5))
                            ComboBoxAnimation = Conversions.ToBoolean(line.Remove(0, "*WinEffects_ComboBoxAnimation= ".Count()));
                        if (line.StartsWith("*WinEffects_ListboxSmoothScrolling= ", (StringComparison)5))
                            ListBoxSmoothScrolling = Conversions.ToBoolean(line.Remove(0, "*WinEffects_ListboxSmoothScrolling= ".Count()));
                        if (line.StartsWith("*WinEffects_TooltipAnimation= ", (StringComparison)5))
                            TooltipAnimation = Conversions.ToBoolean(line.Remove(0, "*WinEffects_TooltipAnimation= ".Count()));
                        if (line.StartsWith("*WinEffects_TooltipFade= ", (StringComparison)5))
                            TooltipFade = (MenuAnimType)Conversions.ToInteger(line.Remove(0, "*WinEffects_TooltipFade= ".Count()));
                        if (line.StartsWith("*WinEffects_IconsShadow= ", (StringComparison)5))
                            IconsShadow = Conversions.ToBoolean(line.Remove(0, "*WinEffects_IconsShadow= ".Count()));
                        if (line.StartsWith("*WinEffects_IconsDesktopTranslSel= ", (StringComparison)5))
                            IconsDesktopTranslSel = Conversions.ToBoolean(line.Remove(0, "*WinEffects_IconsDesktopTranslSel= ".Count()));
                        if (line.StartsWith("*WinEffects_ShowWinContentDrag= ", (StringComparison)5))
                            ShowWinContentDrag = Conversions.ToBoolean(line.Remove(0, "*WinEffects_ShowWinContentDrag= ".Count()));
                        if (line.StartsWith("*WinEffects_KeyboardUnderline= ", (StringComparison)5))
                            KeyboardUnderline = Conversions.ToBoolean(line.Remove(0, "*WinEffects_KeyboardUnderline= ".Count()));
                        if (line.StartsWith("*WinEffects_FocusRectWidth= ", (StringComparison)5))
                            FocusRectWidth = Conversions.ToUInteger(line.Remove(0, "*WinEffects_FocusRectWidth= ".Count()));
                        if (line.StartsWith("*WinEffects_FocusRectHeight= ", (StringComparison)5))
                            FocusRectHeight = Conversions.ToUInteger(line.Remove(0, "*WinEffects_FocusRectHeight= ".Count()));
                        if (line.StartsWith("*WinEffects_Caret= ", (StringComparison)5))
                            Caret = Conversions.ToUInteger(line.Remove(0, "*WinEffects_Caret= ".Count()));
                        if (line.StartsWith("*WinEffects_NotificationDuration= ", (StringComparison)5))
                            NotificationDuration = Conversions.ToInteger(line.Remove(0, "*WinEffects_NotificationDuration= ".Count()));
                        if (line.StartsWith("*WinEffects_ShakeToMinimize= ", (StringComparison)5))
                            ShakeToMinimize = Conversions.ToBoolean(line.Remove(0, "*WinEffects_ShakeToMinimize= ".Count()));
                        if (line.StartsWith("*WinEffects_AWT_Enabled= ", (StringComparison)5))
                            AWT_Enabled = Conversions.ToBoolean(line.Remove(0, "*WinEffects_AWT_Enabled= ".Count()));
                        if (line.StartsWith("*WinEffects_AWT_BringActivatedWindowToTop= ", (StringComparison)5))
                            AWT_BringActivatedWindowToTop = Conversions.ToBoolean(line.Remove(0, "*WinEffects_AWT_BringActivatedWindowToTop= ".Count()));
                        if (line.StartsWith("*WinEffects_AWT_Delay= ", (StringComparison)5))
                            AWT_Delay = Conversions.ToInteger(line.Remove(0, "*WinEffects_AWT_Delay= ".Count()));
                        if (line.StartsWith("*WinEffects_SnapCursorToDefButton= ", (StringComparison)5))
                            SnapCursorToDefButton = Conversions.ToBoolean(line.Remove(0, "*WinEffects_SnapCursorToDefButton= ".Count()));
                        if (line.StartsWith("*WinEffects_Win11ClassicContextMenu= ", (StringComparison)5))
                            Win11ClassicContextMenu = Conversions.ToBoolean(line.Remove(0, "*WinEffects_Win11ClassicContextMenu= ".Count()));
                        if (line.StartsWith("*WinEffects_SysListView32= ", (StringComparison)5))
                            SysListView32 = Conversions.ToBoolean(line.Remove(0, "*WinEffects_SysListView32= ".Count()));
                        if (line.StartsWith("*WinEffects_ShowSecondsInSystemClock= ", (StringComparison)5))
                            ShowSecondsInSystemClock = Conversions.ToBoolean(line.Remove(0, "*WinEffects_ShowSecondsInSystemClock= ".Count()));
                        if (line.StartsWith("*WinEffects_BalloonNotifications= ", (StringComparison)5))
                            BalloonNotifications = Conversions.ToBoolean(line.Remove(0, "*WinEffects_BalloonNotifications= ".Count()));
                        if (line.StartsWith("*WinEffects_PaintDesktopVersion= ", (StringComparison)5))
                            PaintDesktopVersion = Conversions.ToBoolean(line.Remove(0, "*WinEffects_PaintDesktopVersion= ".Count()));
                        if (line.StartsWith("*WinEffects_Win11BootDots= ", (StringComparison)5))
                            Win11BootDots = Conversions.ToBoolean(line.Remove(0, "*WinEffects_Win11BootDots= ".Count()));
                        if (line.StartsWith("*WinEffects_Win11ExplorerBar= ", (StringComparison)5))
                            Win11ExplorerBar = (ExplorerBar)Conversions.ToInteger(line.Remove(0, "*WinEffects_Win11ExplorerBar= ".Count()));
                        if (line.StartsWith("*WinEffects_DisableNavBar= ", (StringComparison)5))
                            DisableNavBar = Conversions.ToBoolean(line.Remove(0, "*WinEffects_DisableNavBar= ".Count()));
                    }
                }

            }

            public struct WallpaperTone
            {
                public bool Enabled;
                public string Image;
                public int H, S, L;

                public void FromListOfString(IEnumerable<string> Lines) // As WallpaperTone
                {
                    if (Lines.Count() > 0)
                    {
                        // Dim WT As New WallpaperTone With {.Image = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg"}

                        foreach (string lin in Lines)
                        {
                            if (lin.StartsWith("Enabled= ", (StringComparison)5))
                                Enabled = Conversions.ToBoolean(lin.Remove(0, "Enabled= ".Count()));
                            if (lin.StartsWith("Image= ", (StringComparison)5))
                                Image = lin.Remove(0, "Image= ".Count());
                            if (lin.StartsWith("H= ", (StringComparison)5))
                                H = Conversions.ToInteger(lin.Remove(0, "H= ".Count()));
                            if (lin.StartsWith("S= ", (StringComparison)5))
                                S = Conversions.ToInteger(lin.Remove(0, "S= ".Count()));
                            if (lin.StartsWith("L= ", (StringComparison)5))
                                L = Conversions.ToInteger(lin.Remove(0, "L= ".Count()));
                        }
                    }

                    // Return WT
                    else
                    {
                        // Return New Structures.WallpaperTone With {
                        // .Enabled = False,
                        // .Image = My.PATH_Windows & "\Web\Wallpaper\Windows\img0.jpg",
                        // .H = 0, .S = 100, .L = 100}
                    }
                }

                public string ToString(string Signature)
                {
                    var tx = new List<string>();
                    tx.Clear();
                    tx.Add(string.Format("<WallpaperTone_{0}>", Signature));
                    tx.Add(string.Format("*WallpaperTone_{0}_Enabled= {1}", Signature, Enabled));
                    tx.Add(string.Format("*WallpaperTone_{0}_Image= {1}", Signature, Image));
                    tx.Add(string.Format("*WallpaperTone_{0}_H= {1}", Signature, H));
                    tx.Add(string.Format("*WallpaperTone_{0}_S= {1}", Signature, S));
                    tx.Add(string.Format("*WallpaperTone_{0}_L= {1}", Signature, L));
                    tx.Add(string.Format("</WallpaperTone_{0}>", Signature) + "\r\n");
                    return tx.CString();
                }
            }

            public struct MetricsFonts
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

                public Font CaptionFont;
                public Font IconFont;
                public Font MenuFont;
                public Font MessageFont;
                public Font SmCaptionFont;
                public Font StatusFont;
                public string FontSubstitute_MSShellDlg;
                public string FontSubstitute_MSShellDlg2;
                public string FontSubstitute_SegoeUI;

                public string AddFontsToThemeFile(string PropName, Font Font)
                {
                    var s = new List<string>();
                    s.Clear();
                    s.Add(string.Format("*Fonts_{0}_{1}= {2}", PropName, "Name", Font.Name));
                    s.Add(string.Format("*Fonts_{0}_{1}= {2}", PropName, "Size", Font.SizeInPoints));
                    s.Add(string.Format("*Fonts_{0}_{1}= {2}", PropName, "Style", Font.Style));
                    return s.CString();
                }

                public override string ToString()
                {
                    var tx = new List<string>();
                    tx.Clear();
                    tx.Add("<Metrics&Fonts>");
                    tx.Add("*MetricsFonts_Enabled= " + Enabled);
                    tx.Add("*Metrics_BorderWidth= " + BorderWidth);
                    tx.Add("*Metrics_CaptionHeight= " + CaptionHeight);
                    tx.Add("*Metrics_CaptionWidth= " + CaptionWidth);
                    tx.Add("*Metrics_IconSpacing= " + IconSpacing);
                    tx.Add("*Metrics_IconVerticalSpacing= " + IconVerticalSpacing);
                    tx.Add("*Metrics_MenuHeight= " + MenuHeight);
                    tx.Add("*Metrics_MenuWidth= " + MenuWidth);
                    tx.Add("*Metrics_PaddedBorderWidth= " + PaddedBorderWidth);
                    tx.Add("*Metrics_ScrollHeight= " + ScrollHeight);
                    tx.Add("*Metrics_ScrollWidth= " + ScrollWidth);
                    tx.Add("*Metrics_SmCaptionHeight= " + SmCaptionHeight);
                    tx.Add("*Metrics_SmCaptionWidth= " + SmCaptionWidth);
                    tx.Add("*Metrics_DesktopIconSize= " + DesktopIconSize);
                    tx.Add("*Metrics_ShellIconSize= " + ShellIconSize);
                    tx.Add("*FontSubstitute_MSShellDlg= " + FontSubstitute_MSShellDlg);
                    tx.Add("*FontSubstitute_MSShellDlg2= " + FontSubstitute_MSShellDlg2);
                    tx.Add("*FontSubstitute_SegoeUI= " + FontSubstitute_SegoeUI);
                    tx.Add(AddFontsToThemeFile("Caption", CaptionFont));
                    tx.Add(AddFontsToThemeFile("Icon", IconFont));
                    tx.Add(AddFontsToThemeFile("Menu", MenuFont));
                    tx.Add(AddFontsToThemeFile("Message", MessageFont));
                    tx.Add(AddFontsToThemeFile("SmCaption", SmCaptionFont));
                    tx.Add(AddFontsToThemeFile("Status", StatusFont));
                    tx.Add("</Metrics&Fonts>" + "\r\n");
                    return tx.CString();
                }

                public void FromListOfString(IEnumerable<string> Lines)
                {
                    var fonts = new List<string>();
                    fonts.Clear();

                    foreach (string line in Lines)
                    {
                        if (line.StartsWith("*MetricsFonts_Enabled= ", (StringComparison)5))
                            Enabled = Conversions.ToBoolean(line.Remove(0, "*MetricsFonts_Enabled= ".Count()));
                        if (line.StartsWith("*Metrics_BorderWidth= ", (StringComparison)5))
                            BorderWidth = Conversions.ToInteger(line.Remove(0, "*Metrics_BorderWidth= ".Count()));
                        if (line.StartsWith("*Metrics_CaptionHeight= ", (StringComparison)5))
                            CaptionHeight = Conversions.ToInteger(line.Remove(0, "*Metrics_CaptionHeight= ".Count()));
                        if (line.StartsWith("*Metrics_CaptionWidth= ", (StringComparison)5))
                            CaptionWidth = Conversions.ToInteger(line.Remove(0, "*Metrics_CaptionWidth= ".Count()));
                        if (line.StartsWith("*Metrics_IconSpacing= ", (StringComparison)5))
                            IconSpacing = Conversions.ToInteger(line.Remove(0, "*Metrics_IconSpacing= ".Count()));
                        if (line.StartsWith("*Metrics_IconVerticalSpacing= ", (StringComparison)5))
                            IconVerticalSpacing = Conversions.ToInteger(line.Remove(0, "*Metrics_IconVerticalSpacing= ".Count()));
                        if (line.StartsWith("*Metrics_MenuHeight= ", (StringComparison)5))
                            MenuHeight = Conversions.ToInteger(line.Remove(0, "*Metrics_MenuHeight= ".Count()));
                        if (line.StartsWith("*Metrics_MenuWidth= ", (StringComparison)5))
                            MenuWidth = Conversions.ToInteger(line.Remove(0, "*Metrics_MenuWidth= ".Count()));
                        if (line.StartsWith("*Metrics_PaddedBorderWidth= ", (StringComparison)5))
                            PaddedBorderWidth = Conversions.ToInteger(line.Remove(0, "*Metrics_PaddedBorderWidth= ".Count()));
                        if (line.StartsWith("*Metrics_ScrollHeight= ", (StringComparison)5))
                            ScrollHeight = Conversions.ToInteger(line.Remove(0, "*Metrics_ScrollHeight= ".Count()));
                        if (line.StartsWith("*Metrics_ScrollWidth= ", (StringComparison)5))
                            ScrollWidth = Conversions.ToInteger(line.Remove(0, "*Metrics_ScrollWidth= ".Count()));
                        if (line.StartsWith("*Metrics_SmCaptionHeight= ", (StringComparison)5))
                            SmCaptionHeight = Conversions.ToInteger(line.Remove(0, "*Metrics_SmCaptionHeight= ".Count()));
                        if (line.StartsWith("*Metrics_SmCaptionWidth= ", (StringComparison)5))
                            SmCaptionWidth = Conversions.ToInteger(line.Remove(0, "*Metrics_SmCaptionWidth= ".Count()));
                        if (line.StartsWith("*Metrics_DesktopIconSize= ", (StringComparison)5))
                            DesktopIconSize = Conversions.ToInteger(line.Remove(0, "*Metrics_DesktopIconSize= ".Count()));
                        if (line.StartsWith("*Metrics_ShellIconSize= ", (StringComparison)5))
                            ShellIconSize = Conversions.ToInteger(line.Remove(0, "*Metrics_ShellIconSize= ".Count()));
                        if (line.StartsWith("*Fonts_", (StringComparison)5))
                            fonts.Add(line.Remove(0, "*Fonts_".Count()));
                        if (line.StartsWith("*FontSubstitute_MSShellDlg= ", (StringComparison)5))
                            FontSubstitute_MSShellDlg = line.Remove(0, "*FontSubstitute_MSShellDlg= ".Count());
                        if (line.StartsWith("*FontSubstitute_MSShellDlg2= ", (StringComparison)5))
                            FontSubstitute_MSShellDlg2 = line.Remove(0, "*FontSubstitute_MSShellDlg2= ".Count());
                        if (line.StartsWith("*FontSubstitute_SegoeUI= ", (StringComparison)5))
                            FontSubstitute_SegoeUI = line.Remove(0, "*FontSubstitute_SegoeUI= ".Count());
                    }

                    if (fonts.Count > 0)
                    {
                        foreach (var x in fonts)
                        {
                            string Value = x.Replace(x.Split('=')[0] + "= ", "").Trim();
                            string FontName = x.Split('=')[0].ToString().Split('_')[0];
                            string Prop = x.Split('=')[0].ToString().Split('_')[1];

                            switch (FontName.ToLower() ?? "")
                            {
                                case var @case when @case == ("Caption".ToLower() ?? ""):
                                    {
                                        CaptionFont = SetToFont(Prop, Value, CaptionFont);
                                        break;
                                    }

                                case var case1 when case1 == ("Icon".ToLower() ?? ""):
                                    {
                                        IconFont = SetToFont(Prop, Value, IconFont);
                                        break;
                                    }

                                case var case2 when case2 == ("Menu".ToLower() ?? ""):
                                    {
                                        MenuFont = SetToFont(Prop, Value, MenuFont);
                                        break;
                                    }

                                case var case3 when case3 == ("Message".ToLower() ?? ""):
                                    {
                                        MessageFont = SetToFont(Prop, Value, MessageFont);
                                        break;
                                    }

                                case var case4 when case4 == ("SmCaption".ToLower() ?? ""):
                                    {
                                        SmCaptionFont = SetToFont(Prop, Value, SmCaptionFont);
                                        break;
                                    }

                                case var case5 when case5 == ("Status".ToLower() ?? ""):
                                    {
                                        StatusFont = SetToFont(Prop, Value, StatusFont);
                                        break;
                                    }

                            }
                        }
                    }

                }

                public Font SetToFont(string PropName, string PropValue, Font Font)
                {
                    var F = new Font(Font.Name, Font.Size, Font.Style);

                    switch (PropName.ToLower() ?? "")
                    {
                        case var @case when @case == ("Name".ToLower() ?? ""):
                            {
                                if (PropValue.ToUpper() == "MS SANS SERIF")
                                    PropValue = "Microsoft Sans Serif";
                                F = new Font(PropValue, Font.Size, Font.Style);
                                break;
                            }

                        case var case1 when case1 == ("Size".ToLower() ?? ""):
                            {
                                F = new Font(Font.Name, Conversions.ToSingle(PropValue), Font.Style);
                                break;
                            }

                        case var case2 when case2 == ("Style".ToLower() ?? ""):
                            {
                                F = new Font(Font.Name, Font.Size, ReturnFontStyle(PropValue));
                                break;
                            }

                    }

                    return F;
                }

                public FontStyle ReturnFontStyle(string Value)
                {

                    if (!Value.Contains(","))
                    {

                        switch (Value.ToLower() ?? "")
                        {
                            case var @case when @case == ("Bold".ToLower() ?? ""):
                                {
                                    return FontStyle.Bold;
                                }

                            case var case1 when case1 == ("Italic".ToLower() ?? ""):
                                {
                                    return FontStyle.Italic;
                                }

                            case var case2 when case2 == ("Regular".ToLower() ?? ""):
                                {
                                    return FontStyle.Regular;
                                }

                            case var case3 when case3 == ("Strikeout".ToLower() ?? ""):
                                {
                                    return FontStyle.Strikeout;
                                }

                            case var case4 when case4 == ("Underline".ToLower() ?? ""):
                                {
                                    return FontStyle.Underline;
                                }

                            default:
                                {
                                    return FontStyle.Regular;
                                }

                        }
                    }

                    else
                    {
                        var Collection = new FontStyle();

                        foreach (var x in Value.Split(','))
                        {
                            string val = x.Trim();

                            switch (val.ToLower() ?? "")
                            {
                                case var case5 when case5 == ("Bold".ToLower() ?? ""):
                                    {
                                        Collection = Collection + (int)FontStyle.Bold;
                                        break;
                                    }

                                case var case6 when case6 == ("Italic".ToLower() ?? ""):
                                    {
                                        Collection = Collection + (int)FontStyle.Italic;
                                        break;
                                    }

                                case var case7 when case7 == ("Regular".ToLower() ?? ""):
                                    {
                                        Collection = Collection + (int)FontStyle.Regular;
                                        break;
                                    }

                                case var case8 when case8 == ("Strikeout".ToLower() ?? ""):
                                    {
                                        Collection = Collection + (int)FontStyle.Strikeout;
                                        break;
                                    }

                                case var case9 when case9 == ("Underline".ToLower() ?? ""):
                                    {
                                        Collection = Collection + (int)FontStyle.Underline;
                                        break;
                                    }

                                default:
                                    {
                                        Collection = Collection + (int)FontStyle.Regular;
                                        break;
                                    }

                            }

                        }

                        return Collection;
                    }

                }

            }

            public struct AltTab
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

                public override string ToString()
                {
                    var tx = new List<string>();
                    tx.Clear();
                    tx.Add("<AltTab>");
                    tx.Add("*AltTab_Enabled= " + Enabled);
                    tx.Add("*AltTab_Style= " + (int)Style);
                    tx.Add("*AltTab_Win10Opacity= " + Win10Opacity);
                    tx.Add("</AltTab>" + "\r\n");
                    return tx.CString();
                }

                public void FromListOfString(IEnumerable<string> Lines)
                {
                    foreach (string line in Lines)
                    {
                        if (line.StartsWith("*AltTab_Enabled= ", (StringComparison)5))
                            Enabled = Conversions.ToBoolean(line.Remove(0, "*AltTab_Enabled= ".Count()));
                        if (line.StartsWith("*AltTab_Style= ", (StringComparison)5))
                            Style = (Styles)Conversions.ToInteger(line.Remove(0, "*AltTab_Style= ".Count()));
                        if (line.StartsWith("*AltTab_Win10Opacity= ", (StringComparison)5))
                            Win10Opacity = Conversions.ToInteger(line.Remove(0, "*AltTab_Win10Opacity= ".Count()));
                    }
                }

            }

            public struct LogonUI10x
            {
                public bool DisableAcrylicBackgroundOnLogon;
                public bool DisableLogonBackgroundImage;
                public bool NoLockScreen;

                public override string ToString()
                {
                    var tx = new List<string>();
                    tx.Clear();
                    tx.Add("<LogonUI_10_11>");
                    tx.Add("*LogonUI_DisableAcrylicBackgroundOnLogon= " + DisableAcrylicBackgroundOnLogon);
                    tx.Add("*LogonUI_DisableLogonBackgroundImage= " + DisableLogonBackgroundImage);
                    tx.Add("*LogonUI_NoLockScreen= " + NoLockScreen);
                    tx.Add("</LogonUI_10_11>" + "\r\n");
                    return tx.CString();
                }

                public void FromListOfString(IEnumerable<string> Lines)
                {
                    foreach (string line in Lines)
                    {
                        if (line.StartsWith("*LogonUI_DisableAcrylicBackgroundOnLogon= ", (StringComparison)5))
                            DisableAcrylicBackgroundOnLogon = Conversions.ToBoolean(line.Remove(0, "*LogonUI_DisableAcrylicBackgroundOnLogon= ".Count()));
                        if (line.StartsWith("*LogonUI_DisableLogonBackgroundImage= ", (StringComparison)5))
                            DisableLogonBackgroundImage = Conversions.ToBoolean(line.Remove(0, "*LogonUI_DisableLogonBackgroundImage= ".Count()));
                        if (line.StartsWith("*LogonUI_NoLockScreen= ", (StringComparison)5))
                            NoLockScreen = Conversions.ToBoolean(line.Remove(0, "*LogonUI_NoLockScreen= ".Count()));
                    }
                }

            }

            public struct LogonUI7
            {
                public bool Enabled;
                public Modes Mode;
                public string ImagePath;
                public Color Color;
                public bool Blur;
                public int Blur_Intensity;
                public bool Grayscale;
                public bool Noise;
                public NoiseMode Noise_Mode;
                public int Noise_Intensity;

                public enum NoiseMode
                {
                    Aero,
                    Acrylic
                }

                public enum Modes
                {
                    Default,
                    Wallpaper,
                    CustomImage,
                    SolidColor
                }

                public override string ToString()
                {
                    var tx = new List<string>();
                    tx.Clear();
                    tx.Add("<LogonUI_7_8>");
                    tx.Add("*LogonUI7_Enabled= " + Enabled);
                    tx.Add("*LogonUI7_Mode= " + (int)Mode);
                    tx.Add("*LogonUI7_ImagePath= " + ImagePath);
                    tx.Add("*LogonUI7_Color= " + Color.ToArgb());
                    tx.Add("*LogonUI7_Effect_Blur= " + Blur);
                    tx.Add("*LogonUI7_Effect_Blur_Intensity= " + Blur_Intensity);
                    tx.Add("*LogonUI7_Effect_Grayscale= " + Grayscale);
                    tx.Add("*LogonUI7_Effect_Noise= " + Noise);
                    tx.Add("*LogonUI7_Effect_Noise_Mode= " + (int)Noise_Mode);
                    tx.Add("*LogonUI7_Effect_Noise_Intensity= " + Noise_Intensity);
                    tx.Add("</LogonUI_7_8>" + "\r\n");
                    return tx.CString();
                }

                public void FromListOfString(IEnumerable<string> Lines)
                {
                    foreach (string line in Lines)
                    {
                        if (line.StartsWith("*LogonUI7_Color= ", (StringComparison)5))
                            Color = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*LogonUI7_Color= ".Count())));
                        if (line.StartsWith("*LogonUI7_Enabled= ", (StringComparison)5))
                            Enabled = Conversions.ToBoolean(line.Remove(0, "*LogonUI7_Enabled= ".Count()));
                        if (line.StartsWith("*LogonUI7_Mode= ", (StringComparison)5))
                            Mode = (Modes)Conversions.ToInteger(line.Remove(0, "*LogonUI7_Mode= ".Count()));
                        if (line.StartsWith("*LogonUI7_ImagePath= ", (StringComparison)5))
                            ImagePath = line.Remove(0, "*LogonUI7_ImagePath= ".Count());
                        if (line.StartsWith("*LogonUI7_Blur= ", (StringComparison)5))
                            Blur = Conversions.ToBoolean(line.Remove(0, "*LogonUI7_Blur= ".Count()));
                        if (line.StartsWith("*LogonUI7_Blur_Intensity= ", (StringComparison)5))
                            Blur_Intensity = Conversions.ToInteger(line.Remove(0, "*LogonUI7_Blur_Intensity= ".Count()));
                        if (line.StartsWith("*LogonUI7_Grayscale= ", (StringComparison)5))
                            Grayscale = Conversions.ToBoolean(line.Remove(0, "*LogonUI7_Grayscale= ".Count()));
                        if (line.StartsWith("*LogonUI7_Noise= ", (StringComparison)5))
                            Noise = Conversions.ToBoolean(line.Remove(0, "*LogonUI7_Noise= ".Count()));
                        if (line.StartsWith("*LogonUI7_Noise_Mode= ", (StringComparison)5))
                            Noise_Mode = (NoiseMode)Conversions.ToInteger(line.Remove(0, "*LogonUI7_Noise_Mode= ".Count()));
                        if (line.StartsWith("*LogonUI7_Noise_Intensity= ", (StringComparison)5))
                            Noise_Intensity = Conversions.ToInteger(line.Remove(0, "*LogonUI7_Noise_Intensity= ".Count()));
                    }
                }

            }

            public struct LogonUIXP
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

                public override string ToString()
                {
                    var tx = new List<string>();
                    tx.Clear();
                    tx.Add("<LogonUI_XP>");
                    tx.Add("*LogonUIXP_Enabled= " + Enabled);
                    tx.Add("*LogonUIXP_Mode= " + (int)Mode);
                    tx.Add("*LogonUIXP_BackColor= " + BackColor.ToArgb());
                    tx.Add("*LogonUIXP_ShowMoreOptions= " + ShowMoreOptions);
                    tx.Add("</LogonUI_XP>" + "\r\n");
                    return tx.CString();
                }

                public void FromListOfString(IEnumerable<string> Lines)
                {
                    foreach (string line in Lines)
                    {
                        if (line.StartsWith("*LogonUIXP_Enabled= ", (StringComparison)5))
                            Enabled = Conversions.ToBoolean(line.Remove(0, "*LogonUIXP_Enabled= ".Count()));
                        if (line.StartsWith("*LogonUIXP_Mode= ", (StringComparison)5))
                            Mode = Conversions.ToDouble(line.Remove(0, "*LogonUIXP_Mode= ".Count())) == 1d ? Modes.Default : Modes.Win2000;
                        if (line.StartsWith("*LogonUIXP_BackColor= ", (StringComparison)5))
                            BackColor = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*LogonUIXP_BackColor= ".Count())));
                        if (line.StartsWith("*LogonUIXP_ShowMoreOptions= ", (StringComparison)5))
                            ShowMoreOptions = Conversions.ToBoolean(line.Remove(0, "*LogonUIXP_ShowMoreOptions= ".Count()));
                    }
                }

            }

            public struct Console
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

                public string ToString(string Signature)
                {
                    var tx = new List<string>();
                    tx.Clear();
                    tx.Add(string.Format("<{0}>", Signature));
                    tx.Add(string.Format("*Terminal_{0}_Enabled= {1}", Signature, Enabled));
                    tx.Add(string.Format("*{0}_ColorTable00= {1}", Signature, ColorTable00.ToArgb()));
                    tx.Add(string.Format("*{0}_ColorTable01= {1}", Signature, ColorTable01.ToArgb()));
                    tx.Add(string.Format("*{0}_ColorTable02= {1}", Signature, ColorTable02.ToArgb()));
                    tx.Add(string.Format("*{0}_ColorTable03= {1}", Signature, ColorTable03.ToArgb()));
                    tx.Add(string.Format("*{0}_ColorTable04= {1}", Signature, ColorTable04.ToArgb()));
                    tx.Add(string.Format("*{0}_ColorTable05= {1}", Signature, ColorTable05.ToArgb()));
                    tx.Add(string.Format("*{0}_ColorTable06= {1}", Signature, ColorTable06.ToArgb()));
                    tx.Add(string.Format("*{0}_ColorTable07= {1}", Signature, ColorTable07.ToArgb()));
                    tx.Add(string.Format("*{0}_ColorTable08= {1}", Signature, ColorTable08.ToArgb()));
                    tx.Add(string.Format("*{0}_ColorTable09= {1}", Signature, ColorTable09.ToArgb()));
                    tx.Add(string.Format("*{0}_ColorTable10= {1}", Signature, ColorTable10.ToArgb()));
                    tx.Add(string.Format("*{0}_ColorTable11= {1}", Signature, ColorTable11.ToArgb()));
                    tx.Add(string.Format("*{0}_ColorTable12= {1}", Signature, ColorTable12.ToArgb()));
                    tx.Add(string.Format("*{0}_ColorTable13= {1}", Signature, ColorTable13.ToArgb()));
                    tx.Add(string.Format("*{0}_ColorTable14= {1}", Signature, ColorTable14.ToArgb()));
                    tx.Add(string.Format("*{0}_ColorTable15= {1}", Signature, ColorTable15.ToArgb()));
                    tx.Add(string.Format("*{0}_PopupForeground= {1}", Signature, PopupForeground));
                    tx.Add(string.Format("*{0}_PopupBackground= {1}", Signature, PopupBackground));
                    tx.Add(string.Format("*{0}_ScreenColorsForeground= {1}", Signature, ScreenColorsForeground));
                    tx.Add(string.Format("*{0}_ScreenColorsBackground= {1}", Signature, ScreenColorsBackground));
                    tx.Add(string.Format("*{0}_CursorSize= {1}", Signature, CursorSize));
                    tx.Add(string.Format("*{0}_FaceName= {1}", Signature, FaceName));
                    tx.Add(string.Format("*{0}_FontRaster= {1}", Signature, FontRaster));
                    tx.Add(string.Format("*{0}_FontSize= {1}", Signature, FontSize));
                    tx.Add(string.Format("*{0}_FontWeight= {1}", Signature, FontWeight));
                    tx.Add(string.Format("*{0}_1909_CursorType= {1}", Signature, W10_1909_CursorType));
                    tx.Add(string.Format("*{0}_1909_CursorColor= {1}", Signature, W10_1909_CursorColor.ToArgb()));
                    tx.Add(string.Format("*{0}_1909_ForceV2= {1}", Signature, W10_1909_ForceV2));
                    tx.Add(string.Format("*{0}_1909_LineSelection= {1}", Signature, W10_1909_LineSelection));
                    tx.Add(string.Format("*{0}_1909_TerminalScrolling= {1}", Signature, W10_1909_TerminalScrolling));
                    tx.Add(string.Format("*{0}_1909_WindowAlpha= {1}", Signature, W10_1909_WindowAlpha));
                    tx.Add(string.Format("</{0}>", Signature) + "\r\n");
                    return tx.CString();
                }

                public void FromListOfString(IEnumerable<string> Lines) // As Console
                {
                    // Dim [Console] As New Console

                    foreach (string line in Lines)
                    {
                        string line_processed = "";

                        if (line.StartsWith("*CMD_", (StringComparison)5))
                            line_processed = line.Remove(0, "*CMD_".Count());
                        else if (line.StartsWith("*PS_32_", (StringComparison)5))
                            line_processed = line.Remove(0, "*PS_32_".Count());
                        else if (line.StartsWith("*PS_64_", (StringComparison)5))
                            line_processed = line.Remove(0, "*PS_64_".Count());

                        if (line_processed.StartsWith("ColorTable00= ", (StringComparison)5))
                            ColorTable00 = Color.FromArgb(Conversions.ToInteger(line_processed.ToLower().Remove(0, "ColorTable00= ".Count())));
                        if (line_processed.StartsWith("ColorTable01= ", (StringComparison)5))
                            ColorTable01 = Color.FromArgb(Conversions.ToInteger(line_processed.ToLower().Remove(0, "ColorTable01= ".Count())));
                        if (line_processed.StartsWith("ColorTable02= ", (StringComparison)5))
                            ColorTable02 = Color.FromArgb(Conversions.ToInteger(line_processed.ToLower().Remove(0, "ColorTable02= ".Count())));
                        if (line_processed.StartsWith("ColorTable03= ", (StringComparison)5))
                            ColorTable03 = Color.FromArgb(Conversions.ToInteger(line_processed.ToLower().Remove(0, "ColorTable03= ".Count())));
                        if (line_processed.StartsWith("ColorTable04= ", (StringComparison)5))
                            ColorTable04 = Color.FromArgb(Conversions.ToInteger(line_processed.ToLower().Remove(0, "ColorTable04= ".Count())));
                        if (line_processed.StartsWith("ColorTable05= ", (StringComparison)5))
                            ColorTable05 = Color.FromArgb(Conversions.ToInteger(line_processed.ToLower().Remove(0, "ColorTable05= ".Count())));
                        if (line_processed.StartsWith("ColorTable06= ", (StringComparison)5))
                            ColorTable06 = Color.FromArgb(Conversions.ToInteger(line_processed.ToLower().Remove(0, "ColorTable06= ".Count())));
                        if (line_processed.StartsWith("ColorTable07= ", (StringComparison)5))
                            ColorTable07 = Color.FromArgb(Conversions.ToInteger(line_processed.ToLower().Remove(0, "ColorTable07= ".Count())));
                        if (line_processed.StartsWith("ColorTable08= ", (StringComparison)5))
                            ColorTable08 = Color.FromArgb(Conversions.ToInteger(line_processed.ToLower().Remove(0, "ColorTable08= ".Count())));
                        if (line_processed.StartsWith("ColorTable09= ", (StringComparison)5))
                            ColorTable09 = Color.FromArgb(Conversions.ToInteger(line_processed.ToLower().Remove(0, "ColorTable09= ".Count())));
                        if (line_processed.StartsWith("ColorTable10= ", (StringComparison)5))
                            ColorTable10 = Color.FromArgb(Conversions.ToInteger(line_processed.ToLower().Remove(0, "ColorTable10= ".Count())));
                        if (line_processed.StartsWith("ColorTable11= ", (StringComparison)5))
                            ColorTable11 = Color.FromArgb(Conversions.ToInteger(line_processed.ToLower().Remove(0, "ColorTable11= ".Count())));
                        if (line_processed.StartsWith("ColorTable12= ", (StringComparison)5))
                            ColorTable12 = Color.FromArgb(Conversions.ToInteger(line_processed.ToLower().Remove(0, "ColorTable12= ".Count())));
                        if (line_processed.StartsWith("ColorTable13= ", (StringComparison)5))
                            ColorTable13 = Color.FromArgb(Conversions.ToInteger(line_processed.ToLower().Remove(0, "ColorTable13= ".Count())));
                        if (line_processed.StartsWith("ColorTable14= ", (StringComparison)5))
                            ColorTable14 = Color.FromArgb(Conversions.ToInteger(line_processed.ToLower().Remove(0, "ColorTable14= ".Count())));
                        if (line_processed.StartsWith("ColorTable15= ", (StringComparison)5))
                            ColorTable15 = Color.FromArgb(Conversions.ToInteger(line_processed.ToLower().Remove(0, "ColorTable15= ".Count())));
                        if (line_processed.StartsWith("PopupForeground= ", (StringComparison)5))
                            PopupForeground = Conversions.ToInteger(line_processed.ToLower().Remove(0, "PopupForeground= ".Count()));
                        if (line_processed.StartsWith("PopupBackground= ", (StringComparison)5))
                            PopupBackground = Conversions.ToInteger(line_processed.ToLower().Remove(0, "PopupBackground= ".Count()));
                        if (line_processed.StartsWith("ScreenColorsForeground= ", (StringComparison)5))
                            ScreenColorsForeground = Conversions.ToInteger(line_processed.ToLower().Remove(0, "ScreenColorsForeground= ".Count()));
                        if (line_processed.StartsWith("ScreenColorsBackground= ", (StringComparison)5))
                            ScreenColorsBackground = Conversions.ToInteger(line_processed.ToLower().Remove(0, "ScreenColorsBackground= ".Count()));
                        if (line_processed.StartsWith("CursorSize= ", (StringComparison)5))
                            CursorSize = Conversions.ToInteger(line_processed.ToLower().Remove(0, "CursorSize= ".Count()));
                        if (line_processed.StartsWith("FaceName= ", (StringComparison)5))
                            FaceName = line_processed.ToLower().Remove(0, "FaceName= ".Count());
                        if (line_processed.StartsWith("FontRaster= ", (StringComparison)5))
                            FontRaster = Conversions.ToBoolean(line_processed.ToLower().Remove(0, "FontRaster= ".Count()));
                        if (line_processed.StartsWith("FontSize= ", (StringComparison)5))
                            FontSize = Conversions.ToInteger(line_processed.ToLower().Remove(0, "FontSize= ".Count()));
                        if (line_processed.StartsWith("FontWeight= ", (StringComparison)5))
                            FontWeight = Conversions.ToInteger(line_processed.ToLower().Remove(0, "FontWeight= ".Count()));
                        if (line_processed.StartsWith("1909_CursorType= ", (StringComparison)5))
                            W10_1909_CursorType = Conversions.ToInteger(line_processed.ToLower().Remove(0, "1909_CursorType= ".Count()));
                        if (line_processed.StartsWith("1909_CursorColor= ", (StringComparison)5))
                            W10_1909_CursorColor = Color.FromArgb(Conversions.ToInteger(line_processed.ToLower().Remove(0, "1909_CursorColor= ".Count())));
                        if (line_processed.StartsWith("1909_ForceV2= ", (StringComparison)5))
                            W10_1909_ForceV2 = Conversions.ToBoolean(line_processed.ToLower().Remove(0, "1909_ForceV2= ".Count()));
                        if (line_processed.StartsWith("1909_lin.ToLowereSelection= ", (StringComparison)5))
                            W10_1909_LineSelection = Conversions.ToBoolean(line_processed.ToLower().Remove(0, "1909_lin.ToLowereSelection= ".Count()));
                        if (line_processed.StartsWith("1909_TerminalScrollin.ToLowerg= ", (StringComparison)5))
                            W10_1909_TerminalScrolling = Conversions.ToBoolean(line_processed.ToLower().Remove(0, "1909_TerminalScrollin.ToLowerg= ".Count()));
                        if (line_processed.StartsWith("1909_WindowAlpha= ", (StringComparison)5))
                            W10_1909_WindowAlpha = Conversions.ToInteger(line_processed.ToLower().Remove(0, "1909_WindowAlpha= ".Count()));
                    }

                    // Return [Console]
                }

            }

            public struct Cursor
            {
                public ArrowStyles ArrowStyle;
                public CircleStyles CircleStyle;
                public Color PrimaryColor1;
                public Color PrimaryColor2;
                public bool PrimaryColorGradient;
                public GradientMode PrimaryColorGradientMode;
                public bool PrimaryColorNoise;
                public float PrimaryColorNoiseOpacity;
                public Color SecondaryColor1;
                public Color SecondaryColor2;
                public bool SecondaryColorGradient;
                public GradientMode SecondaryColorGradientMode;
                public bool SecondaryColorNoise;
                public float SecondaryColorNoiseOpacity;
                public Color LoadingCircleBack1;
                public Color LoadingCircleBack2;
                public bool LoadingCircleBackGradient;
                public GradientMode LoadingCircleBackGradientMode;
                public bool LoadingCircleBackNoise;
                public float LoadingCircleBackNoiseOpacity;
                public Color LoadingCircleHot1;
                public Color LoadingCircleHot2;
                public bool LoadingCircleHotGradient;
                public GradientMode LoadingCircleHotGradientMode;
                public bool LoadingCircleHotNoise;
                public float LoadingCircleHotNoiseOpacity;
                public bool Shadow_Enabled;
                public Color Shadow_Color;
                public int Shadow_Blur;
                public float Shadow_Opacity;
                public int Shadow_OffsetX;
                public int Shadow_OffsetY;

                public enum ArrowStyles
                {
                    Aero,
                    Modern,
                    Classic
                }

                public enum CircleStyles
                {
                    Aero,
                    Dot,
                    Classic
                }

                public enum GradientMode
                {
                    Vertical,
                    Horizontal,
                    ForwardDiagonal,
                    BackwardDiagonal,
                    Circle
                }

                public GradientMode ReturnGradientModeFromString(string String)
                {
                    if (String.Trim().ToLower() == "vertical")
                    {
                        return GradientMode.Vertical;
                    }

                    else if (String.Trim().ToLower() == "horizontal")
                    {
                        return GradientMode.Horizontal;
                    }

                    else if (String.Trim().ToLower() == "forward diagonal")
                    {
                        return GradientMode.ForwardDiagonal;
                    }

                    else if (String.Trim().ToLower() == "backward diagonal")
                    {
                        return GradientMode.BackwardDiagonal;
                    }

                    else if (String.Trim().ToLower() == "circle")
                    {
                        return GradientMode.Circle;
                    }

                    else
                    {
                        return default;

                    }

                }

                public string ReturnStringFromGradientMode(GradientMode GradientMode)
                {
                    if (GradientMode == GradientMode.Horizontal)
                    {
                        return "Horizontal";
                    }

                    else if (GradientMode == GradientMode.Vertical)
                    {
                        return "Vertical";
                    }

                    else if (GradientMode == GradientMode.ForwardDiagonal)
                    {
                        return "Forward Diagonal";
                    }

                    else if (GradientMode == GradientMode.BackwardDiagonal)
                    {
                        return "Backward Diagonal";
                    }

                    else if (GradientMode == GradientMode.Circle)
                    {
                        return "Circle";
                    }

                    else
                    {
                        return null;

                    }

                }

                public Brush ReturnGradience(Rectangle Rectangle, Color Color1, Color Color2, GradientMode GradientMode, float Angle = 0f)
                {

                    if (GradientMode == GradientMode.Horizontal)
                    {
                        return new LinearGradientBrush(Rectangle, Color1, Color2, LinearGradientMode.Horizontal);
                    }

                    else if (GradientMode == GradientMode.Vertical)
                    {
                        return new LinearGradientBrush(Rectangle, Color1, Color2, LinearGradientMode.Vertical);
                    }

                    else if (GradientMode == GradientMode.ForwardDiagonal)
                    {
                        return new LinearGradientBrush(Rectangle, Color1, Color2, LinearGradientMode.ForwardDiagonal);
                    }

                    else if (GradientMode == GradientMode.BackwardDiagonal)
                    {
                        return new LinearGradientBrush(Rectangle, Color1, Color2, LinearGradientMode.BackwardDiagonal);
                    }

                    else if (GradientMode == GradientMode.Circle)
                    {
                        return new LinearGradientBrush(Rectangle, Color1, Color2, Angle, true);
                    }

                    else
                    {
                        return new SolidBrush(Color1);

                    }

                }

                public void FromListOfString(IEnumerable<string> Lines) // As Cursor
                {

                    if (Lines.Count() > 0)
                    {
                        // Dim [Cursor] As New Cursor
                        foreach (string line in Lines)
                        {
                            string line_processed = "";
                            if (line.StartsWith("*Cursor_Arrow_", (StringComparison)5))
                                line_processed = line.Remove(0, "*Cursor_Arrow_".Count());
                            if (line.StartsWith("*Cursor_Help_", (StringComparison)5))
                                line_processed = line.Remove(0, "*Cursor_Help_".Count());
                            if (line.StartsWith("*Cursor_AppLoading_", (StringComparison)5))
                                line_processed = line.Remove(0, "*Cursor_AppLoading_".Count());
                            if (line.StartsWith("*Cursor_Busy_", (StringComparison)5))
                                line_processed = line.Remove(0, "*Cursor_Busy_".Count());
                            if (line.StartsWith("*Cursor_Move_", (StringComparison)5))
                                line_processed = line.Remove(0, "*Cursor_Move_".Count());
                            if (line.StartsWith("*Cursor_NS_", (StringComparison)5))
                                line_processed = line.Remove(0, "*Cursor_NS_".Count());
                            if (line.StartsWith("*Cursor_EW_", (StringComparison)5))
                                line_processed = line.Remove(0, "*Cursor_EW_".Count());
                            if (line.StartsWith("*Cursor_NESW_", (StringComparison)5))
                                line_processed = line.Remove(0, "*Cursor_NESW_".Count());
                            if (line.StartsWith("*Cursor_NWSE_", (StringComparison)5))
                                line_processed = line.Remove(0, "*Cursor_NWSE_".Count());
                            if (line.StartsWith("*Cursor_Up_", (StringComparison)5))
                                line_processed = line.Remove(0, "*Cursor_Up_".Count());
                            if (line.StartsWith("*Cursor_Pen_", (StringComparison)5))
                                line_processed = line.Remove(0, "*Cursor_Pen_".Count());
                            if (line.StartsWith("*Cursor_None_", (StringComparison)5))
                                line_processed = line.Remove(0, "*Cursor_None_".Count());
                            if (line.StartsWith("*Cursor_Link_", (StringComparison)5))
                                line_processed = line.Remove(0, "*Cursor_Link_".Count());
                            if (line.StartsWith("*Cursor_Pin_", (StringComparison)5))
                                line_processed = line.Remove(0, "*Cursor_Pin_".Count());
                            if (line.StartsWith("*Cursor_Person_", (StringComparison)5))
                                line_processed = line.Remove(0, "*Cursor_Person_".Count());
                            if (line.StartsWith("*Cursor_IBeam_", (StringComparison)5))
                                line_processed = line.Remove(0, "*Cursor_IBeam_".Count());
                            if (line.StartsWith("*Cursor_Cross_", (StringComparison)5))
                                line_processed = line.Remove(0, "*Cursor_Cross_".Count());

                            if (line_processed.StartsWith("ArrowStyle= ", (StringComparison)5))
                                ArrowStyle = (ArrowStyles)Conversions.ToInteger(line_processed.Remove(0, "ArrowStyle= ".Count()));
                            if (line_processed.StartsWith("CircleStyle= ", (StringComparison)5))
                                CircleStyle = (CircleStyles)Conversions.ToInteger(line_processed.Remove(0, "CircleStyle= ".Count()));
                            if (line_processed.StartsWith("PrimaryColor1= ", (StringComparison)5))
                                PrimaryColor1 = Color.FromArgb(Conversions.ToInteger(line_processed.Remove(0, "PrimaryColor1= ".Count())));
                            if (line_processed.StartsWith("PrimaryColor2= ", (StringComparison)5))
                                PrimaryColor2 = Color.FromArgb(Conversions.ToInteger(line_processed.Remove(0, "PrimaryColor2= ".Count())));
                            if (line_processed.StartsWith("PrimaryColorGradient= ", (StringComparison)5))
                                PrimaryColorGradient = Conversions.ToBoolean(line_processed.Remove(0, "PrimaryColorGradient= ".Count()));
                            if (line_processed.StartsWith("PrimaryColorGradientMode= ", (StringComparison)5))
                                PrimaryColorGradientMode = ReturnGradientModeFromString(line_processed.Remove(0, "PrimaryColorGradientMode= ".Count()));
                            if (line_processed.StartsWith("PrimaryColorNoise= ", (StringComparison)5))
                                PrimaryColorNoise = Conversions.ToBoolean(line_processed.Remove(0, "PrimaryColorNoise= ".Count()));
                            if (line_processed.StartsWith("PrimaryColorNoiseOpacity= ", (StringComparison)5))
                                PrimaryColorNoiseOpacity = Conversions.ToSingle(line_processed.Remove(0, "PrimaryColorNoiseOpacity= ".Count()));
                            if (line_processed.StartsWith("SecondaryColor1= ", (StringComparison)5))
                                SecondaryColor1 = Color.FromArgb(Conversions.ToInteger(line_processed.Remove(0, "SecondaryColor1= ".Count())));
                            if (line_processed.StartsWith("SecondaryColor2= ", (StringComparison)5))
                                SecondaryColor2 = Color.FromArgb(Conversions.ToInteger(line_processed.Remove(0, "SecondaryColor2= ".Count())));
                            if (line_processed.StartsWith("SecondaryColorGradient= ", (StringComparison)5))
                                SecondaryColorGradient = Conversions.ToBoolean(line_processed.Remove(0, "SecondaryColorGradient= ".Count()));
                            if (line_processed.StartsWith("SecondaryColorGradientMode= ", (StringComparison)5))
                                SecondaryColorGradientMode = ReturnGradientModeFromString(line_processed.Remove(0, "SecondaryColorGradientMode= ".Count()));
                            if (line_processed.StartsWith("SecondaryColorNoise= ", (StringComparison)5))
                                SecondaryColorNoise = Conversions.ToBoolean(line_processed.Remove(0, "SecondaryColorNoise= ".Count()));
                            if (line_processed.StartsWith("SecondaryColorNoiseOpacity= ", (StringComparison)5))
                                SecondaryColorNoiseOpacity = Conversions.ToSingle(line_processed.Remove(0, "SecondaryColorNoiseOpacity= ".Count()));
                            if (line_processed.StartsWith("LoadingCircleBack1= ", (StringComparison)5))
                                LoadingCircleBack1 = Color.FromArgb(Conversions.ToInteger(line_processed.Remove(0, "LoadingCircleBack1= ".Count())));
                            if (line_processed.StartsWith("LoadingCircleBack2= ", (StringComparison)5))
                                LoadingCircleBack2 = Color.FromArgb(Conversions.ToInteger(line_processed.Remove(0, "LoadingCircleBack2= ".Count())));
                            if (line_processed.StartsWith("LoadingCircleBackGradient= ", (StringComparison)5))
                                LoadingCircleBackGradient = Conversions.ToBoolean(line_processed.Remove(0, "LoadingCircleBackGradient= ".Count()));
                            if (line_processed.StartsWith("LoadingCircleBackGradientMode= ", (StringComparison)5))
                                LoadingCircleBackGradientMode = ReturnGradientModeFromString(line_processed.Remove(0, "LoadingCircleBackGradientMode= ".Count()));
                            if (line_processed.StartsWith("LoadingCircleBackNoise= ", (StringComparison)5))
                                LoadingCircleBackNoise = Conversions.ToBoolean(line_processed.Remove(0, "LoadingCircleBackNoise= ".Count()));
                            if (line_processed.StartsWith("LoadingCircleBackNoiseOpacity= ", (StringComparison)5))
                                LoadingCircleBackNoiseOpacity = Conversions.ToSingle(line_processed.Remove(0, "LoadingCircleBackNoiseOpacity= ".Count()));
                            if (line_processed.StartsWith("LoadingCircleHot1= ", (StringComparison)5))
                                LoadingCircleHot1 = Color.FromArgb(Conversions.ToInteger(line_processed.Remove(0, "LoadingCircleHot1= ".Count())));
                            if (line_processed.StartsWith("LoadingCircleHot2= ", (StringComparison)5))
                                LoadingCircleHot2 = Color.FromArgb(Conversions.ToInteger(line_processed.Remove(0, "LoadingCircleHot2= ".Count())));
                            if (line_processed.StartsWith("LoadingCircleHotGradient= ", (StringComparison)5))
                                LoadingCircleHotGradient = Conversions.ToBoolean(line_processed.Remove(0, "LoadingCircleHotGradient= ".Count()));
                            if (line_processed.StartsWith("LoadingCircleHotGradientMode= ", (StringComparison)5))
                                LoadingCircleHotGradientMode = ReturnGradientModeFromString(line_processed.Remove(0, "LoadingCircleHotGradientMode= ".Count()));
                            if (line_processed.StartsWith("LoadingCircleHotNoise= ", (StringComparison)5))
                                LoadingCircleHotNoise = Conversions.ToBoolean(line_processed.Remove(0, "LoadingCircleHotNoise= ".Count()));
                            if (line_processed.StartsWith("LoadingCircleHotNoiseOpacity= ", (StringComparison)5))
                                LoadingCircleHotNoiseOpacity = Conversions.ToSingle(line_processed.Remove(0, "LoadingCircleHotNoiseOpacity= ".Count()));
                            if (line_processed.StartsWith("Shadow_Enabled= ", (StringComparison)5))
                                Shadow_Enabled = Conversions.ToBoolean(line_processed.Remove(0, "Shadow_Enabled= ".Count()));
                            if (line_processed.StartsWith("Shadow_Color= ", (StringComparison)5))
                                Shadow_Color = Color.FromArgb(Conversions.ToInteger(line_processed.Remove(0, "Shadow_Color= ".Count())));
                            if (line_processed.StartsWith("Shadow_Blur= ", (StringComparison)5))
                                Shadow_Blur = Conversions.ToInteger(line_processed.Remove(0, "Shadow_Blur= ".Count()));
                            if (line_processed.StartsWith("Shadow_Opacity= ", (StringComparison)5))
                                Shadow_Opacity = (float)(Conversions.ToDouble(line_processed.Remove(0, "Shadow_Opacity= ".Count())) / 100d);
                            if (line_processed.StartsWith("Shadow_OffsetX= ", (StringComparison)5))
                                Shadow_OffsetX = Conversions.ToInteger(line_processed.Remove(0, "Shadow_OffsetX= ".Count()));
                            if (line_processed.StartsWith("Shadow_OffsetY= ", (StringComparison)5))
                                Shadow_OffsetY = Conversions.ToInteger(line_processed.Remove(0, "Shadow_OffsetY= ".Count()));
                        }

                        // Return [Cursor]
                    }
                }

                public string ToString(string Signature)
                {
                    var tx = new List<string>();
                    tx.Clear();
                    tx.Add(string.Format("<{0}>", Signature));
                    tx.Add(string.Format("*Cursor_{0}_ArrowStyle= {1}", Signature, (int)ArrowStyle));
                    tx.Add(string.Format("*Cursor_{0}_CircleStyle= {1}", Signature, (int)CircleStyle));
                    tx.Add(string.Format("*Cursor_{0}_PrimaryColor1= {1}", Signature, PrimaryColor1.ToArgb()));
                    tx.Add(string.Format("*Cursor_{0}_PrimaryColor2= {1}", Signature, PrimaryColor2.ToArgb()));
                    tx.Add(string.Format("*Cursor_{0}_PrimaryColorGradient= {1}", Signature, PrimaryColorGradient));
                    tx.Add(string.Format("*Cursor_{0}_PrimaryColorGradientMode= {1}", Signature, ReturnStringFromGradientMode(PrimaryColorGradientMode)));
                    tx.Add(string.Format("*Cursor_{0}_PrimaryColorNoise= {1}", Signature, PrimaryColorNoise));
                    tx.Add(string.Format("*Cursor_{0}_PrimaryColorNoiseOpacity= {1}", Signature, PrimaryColorNoiseOpacity));
                    tx.Add(string.Format("*Cursor_{0}_SecondaryColor1= {1}", Signature, SecondaryColor1.ToArgb()));
                    tx.Add(string.Format("*Cursor_{0}_SecondaryColor2= {1}", Signature, SecondaryColor2.ToArgb()));
                    tx.Add(string.Format("*Cursor_{0}_SecondaryColorGradient= {1}", Signature, SecondaryColorGradient));
                    tx.Add(string.Format("*Cursor_{0}_SecondaryColorGradientMode= {1}", Signature, ReturnStringFromGradientMode(SecondaryColorGradientMode)));
                    tx.Add(string.Format("*Cursor_{0}_SecondaryColorNoise= {1}", Signature, SecondaryColorNoise));
                    tx.Add(string.Format("*Cursor_{0}_SecondaryColorNoiseOpacity= {1}", Signature, SecondaryColorNoiseOpacity));
                    tx.Add(string.Format("*Cursor_{0}_LoadingCircleBack1= {1}", Signature, LoadingCircleBack1.ToArgb()));
                    tx.Add(string.Format("*Cursor_{0}_LoadingCircleBack2= {1}", Signature, LoadingCircleBack2.ToArgb()));
                    tx.Add(string.Format("*Cursor_{0}_LoadingCircleBackGradient= {1}", Signature, LoadingCircleBackGradient));
                    tx.Add(string.Format("*Cursor_{0}_LoadingCircleBackGradientMode= {1}", Signature, ReturnStringFromGradientMode(LoadingCircleBackGradientMode)));
                    tx.Add(string.Format("*Cursor_{0}_LoadingCircleBackNoise= {1}", Signature, LoadingCircleBackNoise));
                    tx.Add(string.Format("*Cursor_{0}_LoadingCircleBackNoiseOpacity= {1}", Signature, LoadingCircleBackNoiseOpacity));
                    tx.Add(string.Format("*Cursor_{0}_LoadingCircleHot1= {1}", Signature, LoadingCircleHot1.ToArgb()));
                    tx.Add(string.Format("*Cursor_{0}_LoadingCircleHot2= {1}", Signature, LoadingCircleHot2.ToArgb()));
                    tx.Add(string.Format("*Cursor_{0}_LoadingCircleHotGradient= {1}", Signature, LoadingCircleHotGradient));
                    tx.Add(string.Format("*Cursor_{0}_LoadingCircleHotGradientMode= {1}", Signature, ReturnStringFromGradientMode(LoadingCircleHotGradientMode)));
                    tx.Add(string.Format("*Cursor_{0}_LoadingCircleHotNoise= {1}", Signature, LoadingCircleHotNoise));
                    tx.Add(string.Format("*Cursor_{0}_LoadingCircleHotNoiseOpacity= {1}", Signature, LoadingCircleHotNoiseOpacity));
                    tx.Add(string.Format("*Cursor_{0}_Shadow_Enabled= {1}", Signature, Shadow_Enabled));
                    tx.Add(string.Format("*Cursor_{0}_Shadow_Color= {1}", Signature, Shadow_Color.ToArgb()));
                    tx.Add(string.Format("*Cursor_{0}_Shadow_Blur= {1}", Signature, Shadow_Blur));
                    tx.Add(string.Format("*Cursor_{0}_Shadow_Opacity= {1}", Signature, Shadow_Opacity * 100f));
                    tx.Add(string.Format("*Cursor_{0}_Shadow_OffsetX= {1}", Signature, Shadow_OffsetX));
                    tx.Add(string.Format("*Cursor_{0}_Shadow_OffsetY= {1}", Signature, Shadow_OffsetY));
                    tx.Add(string.Format("</{0}>", Signature) + "\r\n");
                    return tx.CString();
                }

            }
        }

        #region Properties
        public Structures.Info Info = new Structures.Info()
        {
            AppVersion = Program.AppVersion,
            ThemeName = "Current Mode",
            Description = "",
            ThemeVersion = "1.0.0.0",
            Author = Environment.UserName,
            AuthorSocialMediaLink = ""
        };

        public Structures.Windows10x Windows11 = new Structures.Windows10x()
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
            Titlebar_Inactive = Color.FromArgb(0, 0, 0),
            StartMenu_Accent = Color.FromArgb(0, 103, 192),
            WinMode_Light = true,
            AppMode_Light = true,
            Transparency = true,
            ApplyAccentOnTitlebars = false,
            ApplyAccentOnTaskbar = Structures.Windows10x.AccentTaskbarLevels.None,
            IncreaseTBTransparency = false,
            TB_Blur = true
        };

        public Structures.Windows10x Windows10 = new Structures.Windows10x()
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
            Titlebar_Inactive = Color.FromArgb(0, 0, 0),
            StartMenu_Accent = Color.FromArgb(0, 90, 158),
            WinMode_Light = false,
            AppMode_Light = true,
            Transparency = true,
            ApplyAccentOnTitlebars = false,
            ApplyAccentOnTaskbar = Structures.Windows10x.AccentTaskbarLevels.None,
            IncreaseTBTransparency = false,
            TB_Blur = true
        };

        public Structures.LogonUI10x LogonUI10x = new Structures.LogonUI10x() { DisableAcrylicBackgroundOnLogon = false, DisableLogonBackgroundImage = false, NoLockScreen = false };

        public Structures.Windows8 Windows8 = new Structures.Windows8()
        {
            ColorizationColor = Color.FromArgb(246, 195, 74),
            ColorizationColorBalance = 78,
            Start = 0,
            StartColor = Color.FromArgb(30, 0, 84),
            AccentColor = Color.FromArgb(72, 29, 178),
            Theme = Structures.Windows7.Themes.Aero,
            LogonUI = 0,
            PersonalColors_Background = Color.FromArgb(30, 0, 84),
            PersonalColors_Accent = Color.FromArgb(72, 29, 178),
            NoLockScreen = false,
            LockScreenType = Structures.LogonUI7.Modes.Default,
            LockScreenSystemID = 0
        };

        public Structures.Windows7 Windows7 = new Structures.Windows7()
        {
            ColorizationColor = Color.FromArgb(116, 184, 252),
            ColorizationAfterglow = Color.FromArgb(116, 184, 252),
            ColorizationColorBalance = 8,
            ColorizationAfterglowBalance = 43,
            ColorizationBlurBalance = 49,
            ColorizationGlassReflectionIntensity = 0,
            EnableAeroPeek = true,
            AlwaysHibernateThumbnails = false,
            Theme = Structures.Windows7.Themes.Aero
        };

        public Structures.WindowsVista WindowsVista = new Structures.WindowsVista()
        {
            ColorizationColor = Color.FromArgb(64, 158, 254),
            Theme = Structures.Windows7.Themes.Aero
        };

        public Structures.WindowsXP WindowsXP = new Structures.WindowsXP()
        {
            Theme = Structures.WindowsXP.Themes.LunaBlue,
            ColorScheme = "NormalColor",
            ThemeFile = Program.PATH_Windows + @"\resources\Themes\Luna\Luna.msstyles"
        };

        public Structures.LogonUI7 LogonUI7 = new Structures.LogonUI7()
        {
            Enabled = false,
            Mode = Structures.LogonUI7.Modes.Default,
            ImagePath = @"C:\Windows\Web\Wallpaper\Windows\img0.jpg",
            Color = Color.Black,
            Blur = false,
            Blur_Intensity = 0,
            Grayscale = false,
            Noise = false,
            Noise_Mode = Structures.LogonUI7.NoiseMode.Acrylic,
            Noise_Intensity = 0
        };

        public Structures.LogonUIXP LogonUIXP = new Structures.LogonUIXP()
        {
            Enabled = true,
            Mode = Structures.LogonUIXP.Modes.Default,
            BackColor = Color.Black,
            ShowMoreOptions = false
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

        public Structures.WallpaperTone WallpaperTone_W11 = new Structures.WallpaperTone()
        {
            Enabled = false,
            Image = Program.PATH_Windows + @"\Web\Wallpaper\Windows\img0.jpg",
            H = 0,
            S = 100,
            L = 100
        };

        public Structures.WallpaperTone WallpaperTone_W10 = new Structures.WallpaperTone()
        {
            Enabled = false,
            Image = Program.PATH_Windows + @"\Web\Wallpaper\Windows\img0.jpg",
            H = 0,
            S = 100,
            L = 100
        };

        public Structures.WallpaperTone WallpaperTone_W8 = new Structures.WallpaperTone()
        {
            Enabled = false,
            Image = Program.PATH_Windows + @"\Web\Wallpaper\Windows\img0.jpg",
            H = 0,
            S = 100,
            L = 100
        };

        public Structures.WallpaperTone WallpaperTone_W7 = new Structures.WallpaperTone()
        {
            Enabled = false,
            Image = Program.PATH_Windows + @"\Web\Wallpaper\Windows\img0.jpg",
            H = 0,
            S = 100,
            L = 100
        };

        public Structures.WallpaperTone WallpaperTone_WVista = new Structures.WallpaperTone()
        {
            Enabled = false,
            Image = Program.PATH_Windows + @"\Web\Wallpaper\Windows\img0.jpg",
            H = 0,
            S = 100,
            L = 100
        };

        public Structures.WallpaperTone WallpaperTone_WXP = new Structures.WallpaperTone()
        {
            Enabled = false,
            Image = Program.PATH_Windows + @"\Web\Wallpaper\Bliss.bmp",
            H = 0,
            S = 100,
            L = 100
        };

        public Structures.WinEffects WindowsEffects = new Structures.WinEffects()
        {
            Enabled = true,
            WindowAnimation = true,
            WindowShadow = true,
            WindowUIEffects = true,
            MenuAnimation = true,
            MenuSelectionFade = true,
            MenuFade = Structures.WinEffects.MenuAnimType.Fade,
            MenuShowDelay = 400U,
            ComboBoxAnimation = true,
            ListBoxSmoothScrolling = true,
            TooltipAnimation = true,
            TooltipFade = Structures.WinEffects.MenuAnimType.Fade,
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
            Win11BootDots = !Program.W11,
            Win11ExplorerBar = Structures.WinEffects.ExplorerBar.Default,
            DisableNavBar = false
        };

        public Structures.MetricsFonts MetricsFonts = new Structures.MetricsFonts()
        {
            Enabled = true,
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

        public Structures.AltTab AltTab = new Structures.AltTab() { Enabled = true, Style = Structures.AltTab.Styles.Default, Win10Opacity = 95 };

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

        public WinTerminal_Converter Terminal = new WinTerminal_Converter("", WinTerminal_Converter.Mode.Empty);

        public WinTerminal_Converter TerminalPreview = new WinTerminal_Converter("", WinTerminal_Converter.Mode.Empty);

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
            PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
            CircleStyle = Structures.Cursor.CircleStyles.Aero,
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
            PrimaryColorGradientMode = Structures.Cursor.GradientMode.Circle,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
            CircleStyle = (Structures.Cursor.CircleStyles)Structures.Cursor.ArrowStyles.Aero,
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
            PrimaryColorGradientMode = Structures.Cursor.GradientMode.Circle,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Structures.Cursor.GradientMode.Circle,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
            CircleStyle = Structures.Cursor.CircleStyles.Aero,
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
            PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
            CircleStyle = Structures.Cursor.CircleStyles.Aero,
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
            PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
            CircleStyle = Structures.Cursor.CircleStyles.Aero,
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
            PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
            CircleStyle = Structures.Cursor.CircleStyles.Aero,
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
            PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
            CircleStyle = Structures.Cursor.CircleStyles.Aero,
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
            PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
            CircleStyle = Structures.Cursor.CircleStyles.Aero,
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
            PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
            CircleStyle = Structures.Cursor.CircleStyles.Aero,
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
            PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
            CircleStyle = Structures.Cursor.CircleStyles.Aero,
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
            PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
            CircleStyle = Structures.Cursor.CircleStyles.Aero,
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
            PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.FromArgb(255, 0, 0),
            SecondaryColor2 = Color.FromArgb(255, 0, 0),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
            CircleStyle = Structures.Cursor.CircleStyles.Aero,
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
            PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
            CircleStyle = Structures.Cursor.CircleStyles.Aero,
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
            PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
            CircleStyle = Structures.Cursor.CircleStyles.Aero,
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
            PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
            CircleStyle = Structures.Cursor.CircleStyles.Aero,
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
            PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
            CircleStyle = Structures.Cursor.CircleStyles.Aero,
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
            PrimaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Structures.Cursor.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Structures.Cursor.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Structures.Cursor.ArrowStyles.Aero,
            CircleStyle = Structures.Cursor.CircleStyles.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };
        #endregion

        #endregion

        #region CP Handling (Loading/Applying)
        public Converter_CP(string File)
        {
            string txt = string.Join("\r\n", Decompress(File));
            bool JSON = IsValidJson(txt);
            bool WPTH = txt.StartsWith("<WinPaletter - ", (StringComparison)5);

            if (JSON)
            {
                LoadFromJSON(txt);
                Converter.Format = WP_Format.JSON;
            }

            else if (WPTH)
            {
                LoadFromOldWPTHFile(File);
                Converter.Format = WP_Format.WPTH;
            }

            else
            {
                Converter.Format = WP_Format.Error;

            }
        }

        public enum WP_Format
        {
            JSON,
            WPTH,
            Error
        }

        public void Save(WP_Format Format, string File, bool Compress, bool OldWPTH1069)
        {

            if (Format == WP_Format.WPTH)
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

                System.IO.File.WriteAllText(File, ToString(OldWPTH1069));
            }

            else if (Format == WP_Format.JSON)
            {

                if (Compress)
                {
                    System.IO.File.WriteAllText(File, ToJSON().Compress());
                }
                else
                {
                    System.IO.File.WriteAllText(File, ToJSON());
                }

            }

        }

        public void LoadFromOldWPTHFile(string File)
        {
            var txt = new List<string>();
            txt.Clear();
            txt = (List<string>)Decompress(File);

            // ## Checks if the loaded file is an old WPTH or not
            bool OldWPTH = false;
            foreach (string line in txt)
            {
                if (line.StartsWith("*Created from App Version= ", (StringComparison)5))
                {
                    Info.AppVersion = line.Remove(0, "*Created from App Version= ".Count());
                    OldWPTH = Info.AppVersion.CompareTo("1.0.6.9") == -1;
                    break;
                }
            }

            foreach (string lin in txt)
            {
                if (lin.StartsWith("*Palette Name= ", (StringComparison)5))
                    Info.ThemeName = lin.Remove(0, "*Palette Name= ".Count());
                if (lin.StartsWith("*Palette Description= ", (StringComparison)5))
                    Info.Description = lin.Remove(0, "*Palette Description= ".Count()).Replace("<br>", "\r\n");
                if (lin.StartsWith("*Palette File Version= ", (StringComparison)5))
                    Info.ThemeVersion = lin.Remove(0, "*Palette File Version= ".Count());
                if (lin.StartsWith("*Author= ", (StringComparison)5))
                    Info.Author = lin.Remove(0, "*Author= ".Count());
                if (lin.StartsWith("*AuthorSocialMediaLink= ", (StringComparison)5))
                    Info.AuthorSocialMediaLink = lin.Remove(0, "*AuthorSocialMediaLink= ".Count());
            }

            Windows11.FromListOfString(txt.Where(l => l.StartsWith("*Win_11", (StringComparison)5)));
            Windows10.FromListOfString(txt.Where(l => l.StartsWith("*Win_10", (StringComparison)5)));

            #region Windows 10x - Legacy WinPaletter - Before Vesion 1.0.6.9
            if (OldWPTH)
            {
                foreach (string line in txt)
                {
                    try
                    {
                        if (line.StartsWith("*WinMode_Light= ", (StringComparison)5))
                        {
                            Windows11.WinMode_Light = Conversions.ToBoolean(line.Remove(0, "*WinMode_Light= ".Count()));
                            Windows10.WinMode_Light = Windows11.WinMode_Light;
                        }

                        if (line.StartsWith("*AppMode_Light= ", (StringComparison)5))
                        {
                            Windows11.AppMode_Light = Conversions.ToBoolean(line.Remove(0, "*AppMode_Light= ".Count()));
                            Windows10.AppMode_Light = Windows11.AppMode_Light;
                        }


                        if (line.StartsWith("*Transparency= ", (StringComparison)5))
                        {
                            Windows11.Transparency = Conversions.ToBoolean(line.Remove(0, "*Transparency= ".Count()));
                            Windows10.Transparency = Windows11.Transparency;
                        }

                        if (line.StartsWith("*AccentColorOnTitlebarAndBorders= ", (StringComparison)5))
                        {
                            Windows11.ApplyAccentOnTitlebars = Conversions.ToBoolean(line.Remove(0, "*AccentColorOnTitlebarAndBorders= ".Count()));
                            Windows10.ApplyAccentOnTitlebars = Windows11.ApplyAccentOnTitlebars;
                        }

                        if (line.StartsWith("*Titlebar_Active= ", (StringComparison)5))
                        {
                            Windows11.Titlebar_Active = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Titlebar_Active= ".Count())));
                            Windows10.Titlebar_Active = Windows11.Titlebar_Active;
                        }

                        if (line.StartsWith("*Titlebar_Inactive= ", (StringComparison)5))
                        {
                            Windows11.Titlebar_Inactive = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Titlebar_Inactive= ".Count())));
                            Windows10.Titlebar_Inactive = Windows11.Titlebar_Inactive;
                        }

                        if (line.StartsWith("*ActionCenter_AppsLinks= ", (StringComparison)5))
                        {
                            Windows11.Color_Index0 = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*ActionCenter_AppsLinks= ".Count())));
                            Windows10.Color_Index0 = Windows11.Color_Index0;
                        }

                        if (line.StartsWith("*Taskbar_Icon_Underline= ", (StringComparison)5))
                        {
                            Windows11.Color_Index1 = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Taskbar_Icon_Underline= ".Count())));
                            Windows10.Color_Index1 = Windows11.Color_Index1;
                        }

                        if (line.StartsWith("*StartButton_Hover= ", (StringComparison)5))
                        {
                            Windows11.Color_Index2 = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*StartButton_Hover= ".Count())));
                            Windows10.Color_Index2 = Windows11.Color_Index2;
                        }

                        if (line.StartsWith("*SettingsIconsAndLinks= ", (StringComparison)5))
                        {
                            Windows11.Color_Index3 = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*SettingsIconsAndLinks= ".Count())));
                            Windows10.Color_Index3 = Windows11.Color_Index3;
                        }

                        if (line.StartsWith("*StartMenuBackground_ActiveTaskbarButton= ", (StringComparison)5))
                        {
                            Windows11.Color_Index4 = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*StartMenuBackground_ActiveTaskbarButton= ".Count())));
                            Windows10.Color_Index4 = Windows11.Color_Index4;
                        }

                        if (line.StartsWith("*StartListFolders_TaskbarFront= ", (StringComparison)5))
                        {
                            Windows11.Color_Index5 = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*StartListFolders_TaskbarFront= ".Count())));
                            Windows10.Color_Index5 = Windows11.Color_Index5;
                        }

                        if (line.StartsWith("*Taskbar_Background= ", (StringComparison)5))
                        {
                            Windows11.Color_Index6 = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Taskbar_Background= ".Count())));
                            Windows10.Color_Index6 = Windows11.Color_Index6;
                        }

                        if (line.StartsWith("*StartMenu_Accent= ", (StringComparison)5))
                        {
                            Windows11.StartMenu_Accent = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*StartMenu_Accent= ".Count())));
                            Windows10.StartMenu_Accent = Windows11.StartMenu_Accent;
                        }

                        if (line.StartsWith("*Undefined= ", (StringComparison)5))
                        {
                            Windows11.Color_Index7 = Color.FromArgb(Conversions.ToInteger(line.Remove(0, "*Undefined= ".Count())));
                            Windows10.Color_Index7 = Windows11.Color_Index7;
                        }

                        if (line.StartsWith("*AccentColorOnStartTaskbarAndActionCenter= ", (StringComparison)5))
                        {
                            switch (line.Remove(0, "*AccentColorOnStartTaskbarAndActionCenter= ".Count()).ToLower() ?? "")
                            {
                                case "false":
                                    {
                                        Windows11.ApplyAccentOnTaskbar = Structures.Windows10x.AccentTaskbarLevels.None;
                                        break;
                                    }

                                case "true":
                                    {
                                        Windows11.ApplyAccentOnTaskbar = Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC;
                                        break;
                                    }

                                default:
                                    {
                                        switch (line.Remove(0, "*AccentColorOnStartTaskbarAndActionCenter= ".Count()) ?? "")
                                        {
                                            case "0":
                                                {
                                                    Windows11.ApplyAccentOnTaskbar = Structures.Windows10x.AccentTaskbarLevels.None;
                                                    break;
                                                }

                                            case "1":
                                                {
                                                    Windows11.ApplyAccentOnTaskbar = Structures.Windows10x.AccentTaskbarLevels.Taskbar_Start_AC;
                                                    break;
                                                }

                                            case "2":
                                                {
                                                    Windows11.ApplyAccentOnTaskbar = Structures.Windows10x.AccentTaskbarLevels.Taskbar;
                                                    break;
                                                }

                                            default:
                                                {
                                                    Windows11.ApplyAccentOnTaskbar = Structures.Windows10x.AccentTaskbarLevels.None;
                                                    break;
                                                }
                                        }

                                        break;
                                    }
                            }

                            Windows10.ApplyAccentOnTaskbar = Windows11.ApplyAccentOnTaskbar;
                        }
                    }
                    catch
                    {
                        WPStyle.MsgBox("Error during loading from old wpth format (<1.0.6.9)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            #endregion

            Windows8.FromListOfString(txt.Where(l => l.StartsWith("*Metro", (StringComparison)5)));
            Windows7.FromListOfString(txt.Where(l => l.StartsWith("*Aero", (StringComparison)5)));
            WindowsVista.FromListOfString(txt.Where(l => l.StartsWith("*Vista", (StringComparison)5)));
            WindowsXP.FromListOfString(txt.Where(l => l.StartsWith("*WinXP", (StringComparison)5)));
            LogonUI10x.FromListOfString(txt.Where(l => l.StartsWith("*LogonUI_", (StringComparison)5)));
            LogonUI7.FromListOfString(txt.Where(l => l.StartsWith("*LogonUI7_", (StringComparison)5)));
            LogonUIXP.FromListOfString(txt.Where(l => l.StartsWith("*LogonUIXP_", (StringComparison)5)));
            Win32.FromListOfString(txt.Where(l => l.StartsWith("*Win32UI", (StringComparison)5)));
            WindowsEffects.FromListOfString(txt.Where(l => l.StartsWith("*WinEffects", (StringComparison)5)));

            MetricsFonts.FromListOfString(txt.Where(l => l.StartsWith("*Metrics", (StringComparison)5) || l.StartsWith("*Fonts_", (StringComparison)5) || l.StartsWith("*FontSubstitute_", (StringComparison)5)));


            AltTab.FromListOfString(txt.Where(l => l.StartsWith("*AltTab", (StringComparison)5)));

            try
            {
                CommandPrompt.Enabled = Conversions.ToBoolean(txt.Where(l => l.StartsWith("*Terminal_CMD_Enabled", (StringComparison)5)).ElementAtOrDefault(0).Remove(0, "*Terminal_CMD_Enabled= ".Count()));
            }
            catch
            {
            }
            try
            {
                PowerShellx86.Enabled = Conversions.ToBoolean(txt.Where(l => l.StartsWith("*Terminal_PS_32_Enabled", (StringComparison)5)).ElementAtOrDefault(0).Remove(0, "*Terminal_PS_32_Enabled= ".Count()));
            }
            catch
            {
            }
            try
            {
                PowerShellx64.Enabled = Conversions.ToBoolean(txt.Where(l => l.StartsWith("*Terminal_PS_64_Enabled", (StringComparison)5)).ElementAtOrDefault(0).Remove(0, "*Terminal_PS_64_Enabled= ".Count()));
            }
            catch
            {
            }

            CommandPrompt.FromListOfString(txt.Where(l => l.StartsWith("*CMD_", (StringComparison)5)));
            PowerShellx86.FromListOfString(txt.Where(l => l.StartsWith("*PS_32_", (StringComparison)5)));
            PowerShellx64.FromListOfString(txt.Where(l => l.StartsWith("*PS_64_", (StringComparison)5)));

            string str_stable, str_preview;
            str_stable = string.Join("\r\n", txt.Where(l => l.StartsWith("terminal.", (StringComparison)5)));
            str_preview = string.Join("\r\n", txt.Where(l => l.StartsWith("terminalpreview.", (StringComparison)5)));
            Terminal = new WinTerminal_Converter(str_stable, WinTerminal_Converter.Mode.WinPaletterFile, WinTerminal_Converter.Version.Stable);
            TerminalPreview = new WinTerminal_Converter(str_preview, WinTerminal_Converter.Mode.WinPaletterFile, WinTerminal_Converter.Version.Preview);

            WallpaperTone_W11.FromListOfString(txt.Where(l => l.StartsWith("*WallpaperTone_Win11_", (StringComparison)5)));
            WallpaperTone_W10.FromListOfString(txt.Where(l => l.StartsWith("*WallpaperTone_Win10_", (StringComparison)5)));
            WallpaperTone_W8.FromListOfString(txt.Where(l => l.StartsWith("*WallpaperTone_Win8.1_", (StringComparison)5)));
            WallpaperTone_W7.FromListOfString(txt.Where(l => l.StartsWith("*WallpaperTone_Win7_", (StringComparison)5)));
            WallpaperTone_WVista.FromListOfString(txt.Where(l => l.StartsWith("*WallpaperTone_WinVista_", (StringComparison)5)));
            WallpaperTone_WXP.FromListOfString(txt.Where(l => l.StartsWith("*WallpaperTone_WinXP_", (StringComparison)5)));

            try
            {
                Cursor_Enabled = Conversions.ToBoolean(txt.Where(l => l.StartsWith("*Cursor_Enabled", (StringComparison)5)).ElementAtOrDefault(0).Remove(0, "*Cursor_Enabled= ".Count()));
            }
            catch
            {
            }
            try
            {
                Cursor_Shadow = Conversions.ToBoolean(txt.Where(l => l.StartsWith("*Cursor_Shadow", (StringComparison)5)).ElementAtOrDefault(0).Remove(0, "*Cursor_Shadow= ".Count()));
            }
            catch
            {
            }
            try
            {
                Cursor_Trails = Conversions.ToInteger(txt.Where(l => l.StartsWith("*Cursor_Trails", (StringComparison)5)).ElementAtOrDefault(0).Remove(0, "*Cursor_Trails= ".Count()));
            }
            catch
            {
            }
            try
            {
                Cursor_Sonar = Conversions.ToBoolean(txt.Where(l => l.StartsWith("*Cursor_Sonar", (StringComparison)5)).ElementAtOrDefault(0).Remove(0, "*Cursor_Sonar= ".Count()));
            }
            catch
            {
            }

            Cursor_Arrow.FromListOfString(txt.Where(l => l.StartsWith("*Cursor_Arrow_", (StringComparison)5)));
            Cursor_Help.FromListOfString(txt.Where(l => l.StartsWith("*Cursor_Help_", (StringComparison)5)));
            Cursor_AppLoading.FromListOfString(txt.Where(l => l.StartsWith("*Cursor_AppLoading_", (StringComparison)5)));
            Cursor_Busy.FromListOfString(txt.Where(l => l.StartsWith("*Cursor_Busy_", (StringComparison)5)));
            Cursor_Move.FromListOfString(txt.Where(l => l.StartsWith("*Cursor_Move_", (StringComparison)5)));
            Cursor_NS.FromListOfString(txt.Where(l => l.StartsWith("*Cursor_NS_", (StringComparison)5)));
            Cursor_EW.FromListOfString(txt.Where(l => l.StartsWith("*Cursor_EW_", (StringComparison)5)));
            Cursor_NESW.FromListOfString(txt.Where(l => l.StartsWith("*Cursor_NESW_", (StringComparison)5)));
            Cursor_NWSE.FromListOfString(txt.Where(l => l.StartsWith("*Cursor_NWSE_", (StringComparison)5)));
            Cursor_Up.FromListOfString(txt.Where(l => l.StartsWith("*Cursor_Up_", (StringComparison)5)));
            Cursor_Pen.FromListOfString(txt.Where(l => l.StartsWith("*Cursor_Pen_", (StringComparison)5)));
            Cursor_None.FromListOfString(txt.Where(l => l.StartsWith("*Cursor_None_", (StringComparison)5)));
            Cursor_Link.FromListOfString(txt.Where(l => l.StartsWith("*Cursor_Link_", (StringComparison)5)));
            Cursor_Pin.FromListOfString(txt.Where(l => l.StartsWith("*Cursor_Pin_", (StringComparison)5)));
            Cursor_Person.FromListOfString(txt.Where(l => l.StartsWith("*Cursor_Person_", (StringComparison)5)));
            Cursor_IBeam.FromListOfString(txt.Where(l => l.StartsWith("*Cursor_IBeam_", (StringComparison)5)));
            Cursor_Cross.FromListOfString(txt.Where(l => l.StartsWith("*Cursor_Cross_", (StringComparison)5)));
        }

        public string ToString(bool OldWPTH1069)
        {
            var tx = new List<string>();
            tx.Clear();
            tx.Add("<WinPaletter - Programmed by Abdelrhman-AK>");
            tx.Add("*Created from App Version= " + Info.AppVersion);
            tx.Add("*Last Modified by App Version= " + Program.AppVersion + "\r\n");

            tx.Add(Info.ToString());

            #region Windows 10x - Legacy WinPaletter - Before Vesion 1.0.6.9
            if (OldWPTH1069)
            {
                try
                {
                    {
                        var temp = Program.W11 ? Windows11 : Windows10;
                        tx.Add("<LegacyWinPaletter_Windows11/10>");
                        tx.Add("*WinMode_Light= " + temp.WinMode_Light);
                        tx.Add("*AppMode_Light= " + temp.AppMode_Light);
                        tx.Add("*Transparency= " + temp.Transparency);
                        tx.Add("*AccentColorOnTitlebarAndBorders= " + temp.ApplyAccentOnTitlebars);
                        tx.Add("*AccentColorOnStartTaskbarAndActionCenter= " + ((int)temp.ApplyAccentOnTaskbar).ToString());
                        tx.Add("*Titlebar_Active= " + temp.Titlebar_Active.ToArgb());
                        tx.Add("*Titlebar_Inactive= " + temp.Titlebar_Inactive.ToArgb());
                        tx.Add("*ActionCenter_AppsLinks= " + temp.Color_Index0.ToArgb());
                        tx.Add("*Taskbar_Icon_Underline= " + temp.Color_Index1.ToArgb());
                        tx.Add("*StartButton_Hover= " + temp.Color_Index2.ToArgb());
                        tx.Add("*SettingsIconsAndLinks= " + temp.Color_Index3.ToArgb());
                        tx.Add("StartMenuBackground_ActiveTaskbarButton= " + temp.Color_Index4.ToArgb());
                        tx.Add("*StartListFolders_TaskbarFront= " + temp.Color_Index5.ToArgb());
                        tx.Add("*Taskbar_Background= " + temp.Color_Index6.ToArgb());
                        tx.Add("*StartMenu_Accent= " + temp.StartMenu_Accent.ToArgb());
                        tx.Add("*Undefined= " + temp.Color_Index7.ToArgb());
                        tx.Add("</LegacyWinPaletter_Windows11/10>" + "\r\n");
                    }
                }
                catch
                {
                }
            }
            #endregion

            tx.Add(Windows11.ToString("Windows11", "Win_11"));
            tx.Add(Windows10.ToString("Windows10", "Win_10"));
            tx.Add(LogonUI10x.ToString());

            tx.Add(Windows8.ToString());
            tx.Add(Windows7.ToString());
            tx.Add(LogonUI7.ToString());

            tx.Add(WindowsVista.ToString());
            tx.Add(WindowsXP.ToString());
            tx.Add(LogonUIXP.ToString());

            tx.Add(Win32.ToString());
            tx.Add(WindowsEffects.ToString());
            tx.Add(MetricsFonts.ToString());
            tx.Add(AltTab.ToString());

            tx.Add(WallpaperTone_W11.ToString("Win11"));
            tx.Add(WallpaperTone_W10.ToString("Win10"));
            tx.Add(WallpaperTone_W8.ToString("Win8.1"));
            tx.Add(WallpaperTone_W7.ToString("Win7"));
            tx.Add(WallpaperTone_WVista.ToString("WinVista"));
            tx.Add(WallpaperTone_WXP.ToString("WinXP"));

            tx.Add("<Terminals>");
            tx.Add(CommandPrompt.ToString("CMD"));
            tx.Add(PowerShellx86.ToString("PS_32"));
            tx.Add(PowerShellx64.ToString("PS_64"));
            try
            {
                if (Terminal is not null)
                    tx.Add(Terminal.ToString("WindowsTerminal_Stable", (WinTerminal_Converter.Version)WinTerminal.Version.Stable));
            }
            catch
            {
            }
            try
            {
                if (TerminalPreview is not null)
                    tx.Add(TerminalPreview.ToString("WindowsTerminal_Preview", (WinTerminal_Converter.Version)WinTerminal.Version.Preview));
            }
            catch
            {
            }
            tx.Add("</Terminals>" + "\r\n");

            tx.Add("<Cursors>");
            tx.Add("*Cursor_Enabled= " + Cursor_Enabled);
            tx.Add("*Cursor_Shadow= " + Cursor_Shadow);
            tx.Add("*Cursor_Sonar= " + Cursor_Sonar);
            tx.Add("*Cursor_Trails= " + Cursor_Trails);
            tx.Add(Cursor_Arrow.ToString("Arrow"));
            tx.Add(Cursor_Help.ToString("Help"));
            tx.Add(Cursor_AppLoading.ToString("AppLoading"));
            tx.Add(Cursor_Busy.ToString("Busy"));
            tx.Add(Cursor_Move.ToString("Move"));
            tx.Add(Cursor_NS.ToString("NS"));
            tx.Add(Cursor_EW.ToString("EW"));
            tx.Add(Cursor_NESW.ToString("NESW"));
            tx.Add(Cursor_NWSE.ToString("NWSE"));
            tx.Add(Cursor_Up.ToString("Up"));
            tx.Add(Cursor_Pen.ToString("Pen"));
            tx.Add(Cursor_None.ToString("None"));
            tx.Add(Cursor_Link.ToString("Link"));
            tx.Add(Cursor_Pin.ToString("Pin"));
            tx.Add(Cursor_Person.ToString("Person"));
            tx.Add(Cursor_IBeam.ToString("IBeam"));
            tx.Add(Cursor_Cross.ToString("Cross"));
            tx.Add("</Cursors>" + "\r\n");

            tx.Add("</WinPaletter>");

            return tx.CString();
        }

        public IEnumerable<string> Decompress(string File)
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

        public string ToJSON()
        {
            var JSON_Overall = new JObject();

            JSON_Overall.RemoveAll();

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

            return JSON_Overall.ToString();
        }

        public bool IsStructure(Type type)
        {
            return type.IsValueType && !type.IsPrimitive && type.Namespace is not null && !type.Namespace.StartsWith("System.");
        }

        public void LoadFromJSON(string JSON_Text)
        {
            JObject J = JObject.Parse(JSON_Text);

            foreach (FieldInfo field in GetType().GetFields(bindingFlags))
            {
                var type = field.FieldType;

                if (J[field.Name] is not null)
                {
                    field.SetValue(this, J[field.Name].ToObject(type));
                }
            }
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

    }
}