using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static WinPaletter.TProfile;

namespace WinPaletter
{

    public class WinTerminal_Converter
    {
        public bool Enabled { get; set; } = false;
        public List<TColors> Colors { get; set; }
        public List<TProfile> Profiles { get; set; }
        public TProfile DefaultProf { get; set; }
        public List<TTheme> Themes { get; set; }
        public string Theme { get; set; } = "system";
        public bool UseAcrylicInTabRow { get; set; } = false;

        public WinTerminal_Converter(string str, Mode Mode, Version Version = Version.Stable)
        {

            switch (Mode)
            {
                case Mode.JSONFile:
                    {
                        break;
                    }


                case Mode.WinPaletterFile:
                    {
                        List<string> Collected = new();
                        List<string> lst = new();

                        lst.Clear();
                        lst = str.CList();

                        foreach (string lin in lst)
                        {

                            switch (Version)
                            {
                                case Version.Stable:
                                    {
                                        if (lin.StartsWith("terminal.", StringComparison.OrdinalIgnoreCase) & !lin.StartsWith("terminalpreview.", StringComparison.OrdinalIgnoreCase))
                                        {
                                            Collected.Add(lin.Remove(0, "terminal.".Count()));
                                        }

                                        break;
                                    }

                                case Version.Preview:
                                    {
                                        if (lin.StartsWith("terminalpreview.", StringComparison.OrdinalIgnoreCase) & !lin.StartsWith("terminal.", StringComparison.OrdinalIgnoreCase))
                                        {
                                            Collected.Add(lin.Remove(0, "terminalpreview.".Count()));
                                        }

                                        break;
                                    }

                            }


                        }

                        List<string> Defs = new();
                        List<string> CollectedColors = new(), EnumColors = new();
                        List<string> CollectedProfiles = new(), EnumProfiles = new();
                        List<string> CollectedThemes = new(), EnumThemes = new();

                        Defs.Clear();

                        CollectedColors.Clear();
                        CollectedProfiles.Clear();
                        CollectedThemes.Clear();

                        EnumColors.Clear();
                        EnumProfiles.Clear();
                        EnumThemes.Clear();

                        DefaultProf = new TProfile();
                        Colors = new List<TColors>();
                        Profiles = new List<TProfile>();
                        Themes = new List<TTheme>();

                        foreach (string lin in Collected)
                        {
                            if (lin.StartsWith("theme= ", StringComparison.OrdinalIgnoreCase))
                                Theme = lin.Remove(0, "theme= ".Count());
                            if (lin.StartsWith("useacrylicintabrow= ", StringComparison.OrdinalIgnoreCase))
                                UseAcrylicInTabRow = Conversions.ToBoolean(lin.Remove(0, "useAcrylicInTabRow= ".Count()));
                            if (lin.StartsWith("enabled= ", StringComparison.OrdinalIgnoreCase))
                                Enabled = Conversions.ToBoolean(lin.Remove(0, "enabled= ".Count()));

                            if (lin.StartsWith("default.", StringComparison.OrdinalIgnoreCase))
                                Defs.Add(lin.Remove(0, "default.".Count()));
                            if (lin.StartsWith("schemes.", StringComparison.OrdinalIgnoreCase))
                                CollectedColors.Add(lin.Remove(0, "schemes.".Count()));
                            if (lin.StartsWith("profiles.", StringComparison.OrdinalIgnoreCase))
                                CollectedProfiles.Add(lin.Remove(0, "profiles.".Count()));
                            if (lin.StartsWith("themes.", StringComparison.OrdinalIgnoreCase))
                                CollectedThemes.Add(lin.Remove(0, "themes.".Count()));
                        }

                        foreach (string lin in Defs)
                        {
                            string prop = lin.Split('=')[0].Trim();
                            string value = lin.Split('=')[1].Trim();

                            switch (prop.ToLower() ?? string.Empty)
                            {
                                case var @case when @case == ("name".ToLower() ?? string.Empty):
                                    {
                                        DefaultProf.Name = value;
                                        break;
                                    }

                                case var case1 when case1 == ("BackgroundImage".ToLower() ?? string.Empty):
                                    {
                                        DefaultProf.BackgroundImage = value;
                                        break;
                                    }

                                case var case2 when case2 == ("ColorScheme".ToLower() ?? string.Empty):
                                    {
                                        DefaultProf.ColorScheme = value;
                                        break;
                                    }

                                case var case3 when case3 == ("TabTitle".ToLower() ?? string.Empty):
                                    {
                                        DefaultProf.TabTitle = value;
                                        break;
                                    }

                                case var case4 when case4 == ("Icon".ToLower() ?? string.Empty):
                                    {
                                        DefaultProf.Icon = value;
                                        break;
                                    }

                                case var case5 when case5 == ("CursorShape".ToLower() ?? string.Empty):
                                    {
                                        DefaultProf.CursorShape = (CursorShape_Enum)Conversions.ToInteger(value);
                                        break;
                                    }

                                case var case6 when case6 == ("Font".ToLower() ?? string.Empty):
                                    {
                                        DefaultProf.Font.Face = value.Split(',')[0];
                                        DefaultProf.Font.Size = Conversions.ToInteger(value.Split(',')[1]);
                                        DefaultProf.Font.Weight = FontWeight_GetFromString(value.Split(',')[2]);
                                        break;
                                    }

                                case var case7 when case7 == ("TabColor".ToLower() ?? string.Empty):
                                    {
                                        DefaultProf.TabColor = Color.FromArgb(Conversions.ToInteger(value));
                                        break;
                                    }

                                case var case8 when case8 == ("UseAcrylic".ToLower() ?? string.Empty):
                                    {
                                        DefaultProf.UseAcrylic = Conversions.ToBoolean(value);
                                        break;
                                    }

                                case var case9 when case9 == ("CursorHeight".ToLower() ?? string.Empty):
                                    {
                                        DefaultProf.CursorHeight = Conversions.ToInteger(value);
                                        break;
                                    }

                                case var case10 when case10 == ("Opacity".ToLower() ?? string.Empty):
                                    {
                                        DefaultProf.Opacity = Conversions.ToInteger(value);
                                        break;
                                    }

                                case var case11 when case11 == ("BackgroundImageOpacity".ToLower() ?? string.Empty):
                                    {
                                        DefaultProf.BackgroundImageOpacity = Conversions.ToSingle(value);
                                        break;
                                    }
                            }
                        }

                        foreach (string x in CollectedProfiles)
                            EnumProfiles.Add(x.Split('.')[0]);
                        EnumProfiles = EnumProfiles.Distinct().ToList();
                        foreach (string x in EnumProfiles)
                        {
                            TProfile P = new TProfile();
                            foreach (string lin in CollectedProfiles)
                            {
                                if ((lin.Split('=')[0].Split('.')[0].Trim().ToLower() ?? string.Empty) == (x.ToLower() ?? string.Empty))
                                {

                                    string prop = lin.Split('=')[0].Split('.')[1].Trim();
                                    string value = lin.Split('=')[1].Trim();

                                    switch (prop.ToLower() ?? string.Empty)
                                    {
                                        case var case12 when case12 == ("Name".ToLower() ?? string.Empty):
                                            {
                                                P.Name = value;
                                                break;
                                            }

                                        case var case13 when case13 == ("TabTitle".ToLower() ?? string.Empty):
                                            {
                                                P.TabTitle = value;
                                                break;
                                            }

                                        case var case14 when case14 == ("Icon".ToLower() ?? string.Empty):
                                            {
                                                P.Icon = value;
                                                break;
                                            }

                                        case var case15 when case15 == ("TabColor".ToLower() ?? string.Empty):
                                            {
                                                P.TabColor = Color.FromArgb(Conversions.ToInteger(value));
                                                break;
                                            }

                                        case var case16 when case16 == ("UseAcrylic".ToLower() ?? string.Empty):
                                            {
                                                P.UseAcrylic = Conversions.ToBoolean(value);
                                                break;
                                            }

                                        case var case17 when case17 == ("Opacity".ToLower() ?? string.Empty):
                                            {
                                                P.Opacity = Conversions.ToInteger(value);
                                                break;
                                            }

                                        case var case18 when case18 == ("Font".ToLower() ?? string.Empty):
                                            {
                                                P.Font.Face = value.Split(',')[0];
                                                P.Font.Size = Conversions.ToInteger(value.Split(',')[1]);
                                                P.Font.Weight = FontWeight_GetFromString(value.Split(',')[2]);
                                                break;
                                            }

                                        case var case19 when case19 == ("BackgroundImage".ToLower() ?? string.Empty):
                                            {
                                                P.BackgroundImage = value;
                                                break;
                                            }

                                        case var case20 when case20 == ("BackgroundImageOpacity".ToLower() ?? string.Empty):
                                            {
                                                P.BackgroundImageOpacity = Conversions.ToSingle(value);
                                                break;
                                            }

                                        case var case21 when case21 == ("ColorScheme".ToLower() ?? string.Empty):
                                            {
                                                P.ColorScheme = value;
                                                break;
                                            }

                                        case var case22 when case22 == ("CursorShape".ToLower() ?? string.Empty):
                                            {
                                                P.CursorShape = (CursorShape_Enum)Conversions.ToInteger(value);
                                                break;
                                            }

                                        case var case23 when case23 == ("CursorHeight".ToLower() ?? string.Empty):
                                            {
                                                P.CursorHeight = Conversions.ToInteger(value);
                                                break;
                                            }

                                    }

                                }
                            }

                            Profiles.Add(P);
                        }

                        foreach (string x in CollectedColors)
                            EnumColors.Add(x.Split('.')[0]);
                        EnumColors = EnumColors.Distinct().ToList();
                        foreach (string x in EnumColors)
                        {
                            TColors TC = new TColors();

                            foreach (string lin in CollectedColors)
                            {
                                if ((lin.Split('=')[0].Split('.')[0].Trim().ToLower() ?? string.Empty) == (x.ToLower() ?? string.Empty))
                                {
                                    string prop = lin.Split('=')[0].Split('.')[1].Trim();
                                    string value = lin.Split('=')[1].Trim();

                                    switch (prop.ToLower() ?? string.Empty)
                                    {
                                        case var case24 when case24 == ("Name".ToLower() ?? string.Empty):
                                            {
                                                TC.Name = value;
                                                break;
                                            }

                                        case var case25 when case25 == ("Background".ToLower() ?? string.Empty):
                                            {
                                                TC.Background = Color.FromArgb(Conversions.ToInteger(value));
                                                break;
                                            }

                                        case var case26 when case26 == ("Foreground".ToLower() ?? string.Empty):
                                            {
                                                TC.Foreground = Color.FromArgb(Conversions.ToInteger(value));
                                                break;
                                            }

                                        case var case27 when case27 == ("SelectionBackground".ToLower() ?? string.Empty):
                                            {
                                                TC.SelectionBackground = Color.FromArgb(Conversions.ToInteger(value));
                                                break;
                                            }

                                        case var case28 when case28 == ("Black".ToLower() ?? string.Empty):
                                            {
                                                TC.Black = Color.FromArgb(Conversions.ToInteger(value));
                                                break;
                                            }

                                        case var case29 when case29 == ("Blue".ToLower() ?? string.Empty):
                                            {
                                                TC.Blue = Color.FromArgb(Conversions.ToInteger(value));
                                                break;
                                            }

                                        case var case30 when case30 == ("BrightBlack".ToLower() ?? string.Empty):
                                            {
                                                TC.BrightBlack = Color.FromArgb(Conversions.ToInteger(value));
                                                break;
                                            }

                                        case var case31 when case31 == ("BrightBlue".ToLower() ?? string.Empty):
                                            {
                                                TC.BrightBlue = Color.FromArgb(Conversions.ToInteger(value));
                                                break;
                                            }

                                        case var case32 when case32 == ("BrightCyan".ToLower() ?? string.Empty):
                                            {
                                                TC.BrightCyan = Color.FromArgb(Conversions.ToInteger(value));
                                                break;
                                            }

                                        case var case33 when case33 == ("BrightGreen".ToLower() ?? string.Empty):
                                            {
                                                TC.BrightGreen = Color.FromArgb(Conversions.ToInteger(value));
                                                break;
                                            }

                                        case var case34 when case34 == ("BrightPurple".ToLower() ?? string.Empty):
                                            {
                                                TC.BrightPurple = Color.FromArgb(Conversions.ToInteger(value));
                                                break;
                                            }

                                        case var case35 when case35 == ("BrightRed".ToLower() ?? string.Empty):
                                            {
                                                TC.BrightRed = Color.FromArgb(Conversions.ToInteger(value));
                                                break;
                                            }

                                        case var case36 when case36 == ("BrightWhite".ToLower() ?? string.Empty):
                                            {
                                                TC.BrightWhite = Color.FromArgb(Conversions.ToInteger(value));
                                                break;
                                            }

                                        case var case37 when case37 == ("BrightYellow".ToLower() ?? string.Empty):
                                            {
                                                TC.BrightYellow = Color.FromArgb(Conversions.ToInteger(value));
                                                break;
                                            }

                                        case var case38 when case38 == ("CursorColor".ToLower() ?? string.Empty):
                                            {
                                                TC.CursorColor = Color.FromArgb(Conversions.ToInteger(value));
                                                break;
                                            }

                                        case var case39 when case39 == ("Cyan".ToLower() ?? string.Empty):
                                            {
                                                TC.Cyan = Color.FromArgb(Conversions.ToInteger(value));
                                                break;
                                            }

                                        case var case40 when case40 == ("Green".ToLower() ?? string.Empty):
                                            {
                                                TC.Green = Color.FromArgb(Conversions.ToInteger(value));
                                                break;
                                            }

                                        case var case41 when case41 == ("Purple".ToLower() ?? string.Empty):
                                            {
                                                TC.Purple = Color.FromArgb(Conversions.ToInteger(value));
                                                break;
                                            }

                                        case var case42 when case42 == ("Red".ToLower() ?? string.Empty):
                                            {
                                                TC.Red = Color.FromArgb(Conversions.ToInteger(value));
                                                break;
                                            }

                                        case var case43 when case43 == ("White".ToLower() ?? string.Empty):
                                            {
                                                TC.White = Color.FromArgb(Conversions.ToInteger(value));
                                                break;
                                            }

                                        case var case44 when case44 == ("Yellow".ToLower() ?? string.Empty):
                                            {
                                                TC.Yellow = Color.FromArgb(Conversions.ToInteger(value));
                                                break;
                                            }

                                    }

                                }
                            }

                            Colors.Add(TC);
                        }

                        foreach (string x in CollectedThemes)
                            EnumThemes.Add(x.Split('.')[0]);
                        EnumThemes = EnumThemes.Distinct().ToList();
                        foreach (string x in EnumThemes)
                        {
                            TTheme Th = new TTheme();

                            foreach (string lin in CollectedThemes)
                            {
                                if ((lin.Split('=')[0].Split('.')[0].Trim().ToLower() ?? string.Empty) == (x.ToLower() ?? string.Empty))
                                {
                                    string prop = lin.Split('=')[0].Split('.')[1].Trim();
                                    string value = lin.Split('=')[1].Trim();

                                    switch (prop.ToLower() ?? string.Empty)
                                    {
                                        case var case45 when case45 == ("Name".ToLower() ?? string.Empty):
                                            {
                                                Th.Name = value;
                                                break;
                                            }

                                        case var case46 when case46 == ("Titlebar_Active".ToLower() ?? string.Empty):
                                            {
                                                Th.Titlebar_Active = Color.FromArgb(Conversions.ToInteger(value));
                                                break;
                                            }

                                        case var case47 when case47 == ("Titlebar_Inactive".ToLower() ?? string.Empty):
                                            {
                                                Th.Titlebar_Inactive = Color.FromArgb(Conversions.ToInteger(value));
                                                break;
                                            }

                                        case var case48 when case48 == ("Tab_Active".ToLower() ?? string.Empty):
                                            {
                                                Th.Tab_Active = Color.FromArgb(Conversions.ToInteger(value));
                                                break;
                                            }

                                        case var case49 when case49 == ("Tab_Inactive".ToLower() ?? string.Empty):
                                            {
                                                Th.Tab_Inactive = Color.FromArgb(Conversions.ToInteger(value));
                                                break;
                                            }

                                        case var case50 when case50 == ("applicationTheme_light".ToLower() ?? string.Empty):
                                            {
                                                Th.Style = value;
                                                break;
                                            }

                                    }

                                }
                            }

                            Themes.Add(Th);
                        }

                        if (Colors.Count == 0)
                        {
                            Colors.Add(new TColors()
                            {
                                Name = "Campbell",
                                Background = "FF0C0C0C".FromHEXToColor(true),
                                Black = "FF0C0C0C".FromHEXToColor(true),
                                Blue = "FF0037DA".FromHEXToColor(true),
                                BrightBlack = "FF767676".FromHEXToColor(true),
                                BrightBlue = "FF3B78FF".FromHEXToColor(true),
                                BrightCyan = "FF61D6D6".FromHEXToColor(true),
                                BrightGreen = "FF16C60C".FromHEXToColor(true),
                                BrightPurple = "FFB4009E".FromHEXToColor(true),
                                BrightRed = "FFE74856".FromHEXToColor(true),
                                BrightWhite = "FFF2F2F2".FromHEXToColor(true),
                                BrightYellow = "FFF9F1A5".FromHEXToColor(true),
                                CursorColor = "FFFFFFFF".FromHEXToColor(true),
                                Cyan = "FF3A96DD".FromHEXToColor(true),
                                Foreground = "FFCCCCCC".FromHEXToColor(true),
                                Green = "FF13A10E".FromHEXToColor(true),
                                Purple = "FF881798".FromHEXToColor(true),
                                Red = "FFC50F1F".FromHEXToColor(true),
                                SelectionBackground = "FFFFFFFF".FromHEXToColor(true),
                                White = "FFCCCCCC".FromHEXToColor(true),
                                Yellow = "FFC19C00".FromHEXToColor(true)
                            });
                        }

                        break;
                    }

                case Mode.Empty:
                    {

                        Profiles = new List<TProfile>();
                        Colors = new List<TColors>();
                        DefaultProf = new TProfile();
                        Themes = new List<TTheme>();

                        Colors.Add(new TColors()
                        {
                            Name = "Campbell",
                            Background = "FF0C0C0C".FromHEXToColor(true),
                            Black = "FF0C0C0C".FromHEXToColor(true),
                            Blue = "FF0037DA".FromHEXToColor(true),
                            BrightBlack = "FF767676".FromHEXToColor(true),
                            BrightBlue = "FF3B78FF".FromHEXToColor(true),
                            BrightCyan = "FF61D6D6".FromHEXToColor(true),
                            BrightGreen = "FF16C60C".FromHEXToColor(true),
                            BrightPurple = "FFB4009E".FromHEXToColor(true),
                            BrightRed = "FFE74856".FromHEXToColor(true),
                            BrightWhite = "FFF2F2F2".FromHEXToColor(true),
                            BrightYellow = "FFF9F1A5".FromHEXToColor(true),
                            CursorColor = "FFFFFFFF".FromHEXToColor(true),
                            Cyan = "FF3A96DD".FromHEXToColor(true),
                            Foreground = "FFCCCCCC".FromHEXToColor(true),
                            Green = "FF13A10E".FromHEXToColor(true),
                            Purple = "FF881798".FromHEXToColor(true),
                            Red = "FFC50F1F".FromHEXToColor(true),
                            SelectionBackground = "FFFFFFFF".FromHEXToColor(true),
                            White = "FFCCCCCC".FromHEXToColor(true),
                            Yellow = "FFC19C00".FromHEXToColor(true)
                        });
                        break;
                    }

            }
        }

        public enum Mode : int
        {
            JSONFile,
            WinPaletterFile,
            Empty
        }

        public enum Version : int
        {
            Stable,
            Preview
        }

        public string Save(string File, Mode Mode, Version Version = Version.Stable)
        {
            switch (Mode)
            {
                case Mode.JSONFile:
                    {
                        return string.Empty;
                    }

                case Mode.WinPaletterFile:
                    {

                        string First = string.Empty;

                        switch (Version)
                        {
                            case Version.Stable:
                                {
                                    First = "Terminal.";
                                    break;
                                }

                            case Version.Preview:
                                {
                                    First = "TerminalPreview.";
                                    break;
                                }

                        }

                        List<string> S = new();
                        S.Clear();

                        try
                        {
                            try
                            {
                                S.Add($"{First}{"theme"}= {Theme}");
                            }
                            catch
                            {
                            }
                            try
                            {
                                S.Add($"{First}{"Enabled"}= {Enabled}");
                            }
                            catch
                            {
                            }
                            try
                            {
                                S.Add($"{First}{"useAcrylicInTabRow"}= {UseAcrylicInTabRow}");
                            }
                            catch
                            {
                            }
                        }
                        catch
                        {
                        }

                        try
                        {
                            Type type1 = DefaultProf.GetType();
                            PropertyInfo[] properties1 = type1.GetProperties();

                            foreach (PropertyInfo property in properties1)
                            {
                                try
                                {
                                    if (property.GetValue(DefaultProf) is not null)
                                    {
                                        S.Add($"{First}{"Default"}.{property.Name}= {(ReturnPerfectValue(property.PropertyType, property.GetValue(DefaultProf)))}");
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                        catch
                        {
                        }

                        try
                        {

                            foreach (TColors c in Colors)
                            {
                                Type type2 = c.GetType();
                                PropertyInfo[] properties2 = type2.GetProperties();

                                foreach (PropertyInfo property in properties2)
                                {
                                    try
                                    {
                                        if (property.GetValue(c) is not null)
                                        {
                                            S.Add($"{First}{"Schemes"}.{(c.Name.Replace(" ", string.Empty).Replace(".", string.Empty))}.{property.Name}= {(ReturnPerfectValue(property.PropertyType, property.GetValue(c)))}");
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }

                            }
                        }
                        catch
                        {
                        }

                        try
                        {
                            foreach (TProfile c in Profiles)
                            {
                                Type type2 = c.GetType();
                                PropertyInfo[] properties2 = type2.GetProperties();
                                try
                                {
                                    foreach (PropertyInfo property in properties2)
                                    {
                                        if (property.GetValue(c) is not null & property.Name != "commandline")
                                        {
                                            S.Add($"{First}{"Profiles"}.{(c.Name.Replace(" ", string.Empty).Replace(".", string.Empty))}.{property.Name}= {(ReturnPerfectValue(property.PropertyType, property.GetValue(c)))}");
                                        }
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                        catch
                        {
                        }

                        try
                        {
                            foreach (TTheme c in Themes)
                            {
                                Type type2 = c.GetType();
                                PropertyInfo[] properties2 = type2.GetProperties();
                                foreach (PropertyInfo property in properties2)
                                {
                                    try
                                    {
                                        if (property.GetValue(c) is not null)
                                        {
                                            S.Add($"{First}{"Themes"}.{(c.Name.Replace(" ", string.Empty).Replace(".", string.Empty))}.{property.Name}= {(ReturnPerfectValue(property.PropertyType, property.GetValue(c)))}");
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                        catch
                        {
                        }

                        return S.CString();
                    }

                default:
                    {
                        return string.Empty;
                    }

            }

        }

        public string ToString(string Signature, Version Edition)
        {
            List<string> tx = new();
            tx.Clear();
            tx.Add($"<{Signature}>");
            tx.Add(Save(string.Empty, Mode.WinPaletterFile, Edition));
            tx.Add($"</{Signature}>");
            return tx.CString();
        }

        public string ReturnPerfectValue(Type Type, object Value)
        {
            switch (Type.Name.ToLower() ?? string.Empty)
            {
                case "color":
                    {
                        return ((Color)Value).ToArgb().ToString();
                    }

                case "padding":
                    {
                        {
                            Padding temp = (Padding)Value;
                            return $"{temp.Left},{temp.Top},{temp.Right},{temp.Bottom}";
                        }
                    }

                case "fontsbase":
                    {
                        {
                            TFont temp1 = (TFont)Value;
                            return $"{temp1.Face},{temp1.Size},{FontWeight_ReturnToString(temp1.Weight)}";
                        }
                    }

                default:
                    {
                        return Value.ToString();
                    }

            }
        }
    }
}