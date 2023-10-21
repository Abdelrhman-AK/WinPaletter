using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using static WinPaletter.ProfilesList;

namespace WinPaletter
{

    public class WinTerminal : ICloneable
    {

        public bool Enabled { get; set; } = false;
        public List<TColor> Colors { get; set; }
        public List<ProfilesList> Profiles { get; set; }
        public ProfilesList DefaultProf { get; set; }
        public List<ThemesList> Themes { get; set; }
        public string Theme { get; set; } = "system";
        public bool UseAcrylicInTabRow { get; set; } = false;



        public WinTerminal(string str, Mode Mode, Version Version = Version.Stable)
        {
            switch (Mode)
            {
                case Mode.JSONFile:
                    {
                        if (File.Exists(str))
                        {
                            var St = new StreamReader(str);
                            string JSON_String = St.ReadToEnd();

                            try
                            {
                                JObject JSonFile = JObject.Parse(JSON_String);

                                if (JSonFile["useAcrylicInTabRow"] is not null)
                                    UseAcrylicInTabRow = (bool)JSonFile["useAcrylicInTabRow"];
                                if (JSonFile["theme"] is not null)
                                    Theme = JSonFile["theme"].ToString();


                                #region Getting Default Profile
                                DefaultProf = new ProfilesList();

                                if (JSonFile["profiles"] is not null)
                                {
                                    if (JSonFile["profiles"]["defaults"] is not null)
                                    {

                                        if (JSonFile["profiles"]["defaults"]["name"] is not null)
                                            DefaultProf.Name = JSonFile["profiles"]["defaults"]["name"].ToString();
                                        if (JSonFile["profiles"]["defaults"]["backgroundImage"] is not null)
                                            DefaultProf.BackgroundImage = JSonFile["profiles"]["defaults"]["backgroundImage"].ToString();
                                        if (JSonFile["profiles"]["defaults"]["cursorShape"] is not null)
                                            DefaultProf.CursorShape = ProfilesList.CursorShape_GetFromString(JSonFile["profiles"]["defaults"]["cursorShape"].ToString());

                                        if (JSonFile["profiles"]["defaults"]["font"] is not null)
                                        {
                                            if (JSonFile["profiles"]["defaults"]["font"]["weight"] is not null)
                                                DefaultProf.Font.Weight = ProfilesList.FontWeight_GetFromString(JSonFile["profiles"]["defaults"]["font"]["weight"].ToString());
                                            if (JSonFile["profiles"]["defaults"]["font"]["face"] is not null)
                                                DefaultProf.Font.Face = JSonFile["profiles"]["defaults"]["font"]["face"].ToString();
                                            if (JSonFile["profiles"]["defaults"]["font"]["size"] is not null)
                                                DefaultProf.Font.Size = (int)JSonFile["profiles"]["defaults"]["font"]["size"];
                                        }

                                        if (JSonFile["profiles"]["defaults"]["colorScheme"] is not null)
                                            DefaultProf.ColorScheme = JSonFile["profiles"]["defaults"]["colorScheme"].ToString();
                                        if (JSonFile["profiles"]["defaults"]["tabTitle"] is not null)
                                            DefaultProf.TabTitle = JSonFile["profiles"]["defaults"]["tabTitle"].ToString();
                                        if (JSonFile["profiles"]["defaults"]["icon"] is not null)
                                            DefaultProf.Icon = JSonFile["profiles"]["defaults"]["icon"].ToString();

                                        if (JSonFile["profiles"]["defaults"]["tabColor"] is not null)
                                        {
                                            DefaultProf.TabColor = HEX2RGB(JSonFile["profiles"]["defaults"]["tabColor"].ToString());
                                        }

                                        if (JSonFile["profiles"]["defaults"]["useAcrylic"] is not null)
                                            DefaultProf.UseAcrylic = (bool)JSonFile["profiles"]["defaults"]["useAcrylic"];
                                        if (JSonFile["profiles"]["defaults"]["cursorHeight"] is not null)
                                            DefaultProf.CursorHeight = (int)JSonFile["profiles"]["defaults"]["cursorHeight"];
                                        if (JSonFile["profiles"]["defaults"]["opacity"] is not null)
                                            DefaultProf.Opacity = (int)JSonFile["profiles"]["defaults"]["opacity"];
                                        if (JSonFile["profiles"]["defaults"]["backgroundImageOpacity"] is not null)
                                            DefaultProf.BackgroundImageOpacity = (int)JSonFile["profiles"]["defaults"]["backgroundImageOpacity"];
                                    }
                                }
                                #endregion

                                #region Getting Profiles
                                Profiles = new List<ProfilesList>();
                                Profiles.Clear();

                                if (JSonFile["profiles"] is not null)
                                {
                                    if (JSonFile["profiles"]["list"] is not null)
                                    {
                                        foreach (var item in JSonFile["profiles"]["list"])
                                        {
                                            var P = new ProfilesList();
                                            if (item["name"] is not null)
                                                P.Name = item["name"].ToString();
                                            if (item["backgroundImage"] is not null)
                                                P.BackgroundImage = item["backgroundImage"].ToString();
                                            if (item["cursorShape"] is not null)
                                                P.CursorShape = CursorShape_GetFromString(item["cursorShape"].ToString());

                                            if (item["font"] is not null)
                                            {
                                                if (item["font"]["weight"] is not null)
                                                    P.Font.Weight = FontWeight_GetFromString(item["font"]["weight"].ToString());
                                                if (item["font"]["face"] is not null)
                                                    P.Font.Face = item["font"]["face"].ToString();
                                                if (item["font"]["size"] is not null)
                                                    P.Font.Size = Conversions.ToInteger(item["font"]["size"]);
                                            }
                                            // 
                                            if (item["commandline"] is not null)
                                                P.Commandline = item["commandline"].ToString();
                                            if (item["colorScheme"] is not null)
                                                P.ColorScheme = item["colorScheme"].ToString();
                                            else
                                                P.ColorScheme = DefaultProf.ColorScheme;
                                            if (item["tabTitle"] is not null)
                                                P.TabTitle = item["tabTitle"].ToString();
                                            if (item["icon"] is not null)
                                                P.Icon = item["icon"].ToString();
                                            if (item["tabColor"] is not null)
                                                P.TabColor = HEX2RGB(item["tabColor"].ToString());
                                            if (item["useAcrylic"] is not null)
                                                P.UseAcrylic = Conversions.ToBoolean(item["useAcrylic"]);
                                            if (item["cursorHeight"] is not null)
                                                P.CursorHeight = Conversions.ToInteger(item["cursorHeight"]);
                                            if (item["opacity"] is not null)
                                                P.Opacity = Conversions.ToInteger(item["opacity"]);
                                            if (item["backgroundImageOpacity"] is not null)
                                                P.BackgroundImageOpacity = Conversions.ToSingle(item["backgroundImageOpacity"]);

                                            Profiles.Add(P);
                                        }
                                    }
                                }

                                #endregion

                                #region Getting All Colors Schemes
                                Colors = new List<TColor>();
                                Colors.Clear();

                                if (JSonFile["schemes"] is not null)
                                {
                                    foreach (var item in JSonFile["schemes"])
                                    {
                                        var TC = new TColor();

                                        if (item["background"] is not null)
                                            TC.Background = HEX2RGB(item["background"].ToString());
                                        if (item["black"] is not null)
                                            TC.Black = HEX2RGB(item["black"].ToString());
                                        if (item["blue"] is not null)
                                            TC.Blue = HEX2RGB(item["blue"].ToString());
                                        if (item["brightBlack"] is not null)
                                            TC.BrightBlack = HEX2RGB(item["brightBlack"].ToString());
                                        if (item["brightBlue"] is not null)
                                            TC.BrightBlue = HEX2RGB(item["brightBlue"].ToString());
                                        if (item["brightCyan"] is not null)
                                            TC.BrightCyan = HEX2RGB(item["brightCyan"].ToString());
                                        if (item["brightGreen"] is not null)
                                            TC.BrightGreen = HEX2RGB(item["brightGreen"].ToString());
                                        if (item["brightPurple"] is not null)
                                            TC.BrightPurple = HEX2RGB(item["brightPurple"].ToString());
                                        if (item["brightRed"] is not null)
                                            TC.BrightRed = HEX2RGB(item["brightRed"].ToString());
                                        if (item["brightWhite"] is not null)
                                            TC.BrightWhite = HEX2RGB(item["brightWhite"].ToString());
                                        if (item["brightYellow"] is not null)
                                            TC.BrightYellow = HEX2RGB(item["brightYellow"].ToString());
                                        if (item["cursorColor"] is not null)
                                            TC.CursorColor = HEX2RGB(item["cursorColor"].ToString());
                                        if (item["cyan"] is not null)
                                            TC.Cyan = HEX2RGB(item["cyan"].ToString());
                                        if (item["foreground"] is not null)
                                            TC.Foreground = HEX2RGB(item["foreground"].ToString());
                                        if (item["green"] is not null)
                                            TC.Green = HEX2RGB(item["green"].ToString());
                                        if (item["name"] is not null)
                                            TC.Name = item["name"].ToString();
                                        if (item["purple"] is not null)
                                            TC.Purple = HEX2RGB(item["purple"].ToString());
                                        if (item["red"] is not null)
                                            TC.Red = HEX2RGB(item["red"].ToString());
                                        if (item["selectionBackground"] is not null)
                                            TC.SelectionBackground = HEX2RGB(item["selectionBackground"].ToString());
                                        if (item["white"] is not null)
                                            TC.White = HEX2RGB(item["white"].ToString());
                                        if (item["yellow"] is not null)
                                            TC.Yellow = HEX2RGB(item["yellow"].ToString());

                                        Colors.Add(TC);
                                    }
                                }

                                else
                                {
                                    Colors.Add(new TColor()
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

                                #endregion

                                #region Getting All Themes
                                Themes = new List<ThemesList>();
                                Themes.Clear();

                                if (JSonFile["themes"] is not null)
                                {
                                    foreach (var item in JSonFile["themes"])
                                    {
                                        var Th = new ThemesList();
                                        if (item["name"] is not null)
                                            Th.Name = item["name"].ToString();

                                        if (item["tabRow"] is not null)
                                        {
                                            if (item["tabRow"]["background"] is not null)
                                                Th.Titlebar_Active = HEX2RGB(item["tabRow"]["background"].ToString());
                                            if (item["tabRow"]["unfocusedBackground"] is not null)
                                                Th.Titlebar_Inactive = HEX2RGB(item["tabRow"]["unfocusedBackground"].ToString());
                                        }

                                        if (item["tab"] is not null)
                                        {
                                            if (item["tab"]["background"] is not null)
                                                Th.Tab_Active = HEX2RGB(item["tab"]["background"].ToString());
                                            if (item["tab"]["unfocusedBackground"] is not null)
                                                Th.Tab_Inactive = HEX2RGB(item["tab"]["unfocusedBackground"].ToString());
                                        }

                                        if (item["window"] is not null)
                                        {
                                            if (item["window"]["applicationTheme"] is not null)
                                                Th.ApplicationTheme_light = item["window"]["applicationTheme"].ToString();
                                        }

                                        Themes.Add(Th);
                                    }
                                }
                            }

                            #endregion

                            catch (Exception ex)
                            {
                                Forms.BugReport.ThrowError(ex);
                            }

                            St.Close();
                        }
                        else
                        {
                            Profiles = new List<ProfilesList>();
                            Colors = new List<TColor>();
                            DefaultProf = new ProfilesList();
                            Themes = new List<ThemesList>();

                            Colors.Add(new TColor()
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

                case Mode.WinPaletterFile:
                    {
                        using (var TBx = new Theme.Manager(WinPaletter.Theme.Manager.Source.File, str))
                        {

                            switch (Version)
                            {
                                case Version.Stable:
                                    {
                                        var bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
                                        foreach (FieldInfo field in GetType().GetFields(bindingFlags))
                                        {
                                            var type = field.FieldType;
                                            field.SetValue(this, field.GetValue(TBx.TerminalPreview));
                                        }

                                        break;
                                    }

                                case Version.Preview:
                                    {
                                        var bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
                                        foreach (FieldInfo field in GetType().GetFields(bindingFlags))
                                        {
                                            var type = field.FieldType;
                                            field.SetValue(this, field.GetValue(TBx.TerminalPreview));
                                        }

                                        break;
                                    }

                            }
                        }

                        break;
                    }

                case Mode.Empty:
                    {

                        Profiles = new List<ProfilesList>();
                        Colors = new List<TColor>();
                        DefaultProf = new ProfilesList();
                        Themes = new List<ThemesList>();

                        Colors.Add(new TColor()
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
            Default,
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
                        string SettingsFile = "";

                        switch (Version)
                        {
                            case Version.Stable:
                                {
                                    if (!Program.Settings.WindowsTerminals.Path_Deflection)
                                    {
                                        SettingsFile = PathsExt.TerminalJSON;
                                    }
                                    else if (System.IO.File.Exists(Program.Settings.WindowsTerminals.Terminal_Stable_Path))
                                    {
                                        SettingsFile = Program.Settings.WindowsTerminals.Terminal_Stable_Path;
                                    }
                                    else
                                    {
                                        SettingsFile = PathsExt.TerminalJSON;
                                    }

                                    break;
                                }

                            case Version.Preview:
                                {
                                    if (!Program.Settings.WindowsTerminals.Path_Deflection)
                                    {
                                        SettingsFile = PathsExt.TerminalPreviewJSON;
                                    }
                                    else if (System.IO.File.Exists(Program.Settings.WindowsTerminals.Terminal_Stable_Path))
                                    {
                                        SettingsFile = Program.Settings.WindowsTerminals.Terminal_Stable_Path;
                                    }
                                    else
                                    {
                                        SettingsFile = PathsExt.TerminalPreviewJSON;
                                    }

                                    break;
                                }

                        }


                        var St = new StreamReader(SettingsFile);
                        string JSON_String = St.ReadToEnd();
                        JObject JSonFile = JObject.Parse(JSON_String);
                        JObject JSonFileUntouched = JObject.Parse(JSON_String);

                        #region Global Settings
                        JSonFile["useAcrylicInTabRow"] = UseAcrylicInTabRow;
                        JSonFile["theme"] = Theme;
                        #endregion

                        #region Schemes
                        if (JSonFile["schemes"] is not null)
                            ((JArray)JSonFile["schemes"]).Clear();
                        else
                            JSonFile["schemes"] = new JArray();
                        for (int x = 0, loopTo = Colors.Count - 1; x <= loopTo; x++)
                        {
                            var JS = new JObject();
                            JS["background"] = RGB2HEX(Colors[x].Background);
                            JS["black"] = RGB2HEX(Colors[x].Black);
                            JS["blue"] = RGB2HEX(Colors[x].Blue);
                            JS["brightBlack"] = RGB2HEX(Colors[x].BrightBlack);
                            JS["brightBlue"] = RGB2HEX(Colors[x].BrightBlue);
                            JS["brightCyan"] = RGB2HEX(Colors[x].BrightCyan);
                            JS["brightGreen"] = RGB2HEX(Colors[x].BrightGreen);
                            JS["brightPurple"] = RGB2HEX(Colors[x].BrightPurple);
                            JS["brightRed"] = RGB2HEX(Colors[x].BrightRed);
                            JS["brightWhite"] = RGB2HEX(Colors[x].BrightWhite);
                            JS["brightYellow"] = RGB2HEX(Colors[x].BrightYellow);
                            JS["cursorColor"] = RGB2HEX(Colors[x].CursorColor);
                            JS["cyan"] = RGB2HEX(Colors[x].Cyan);
                            JS["green"] = RGB2HEX(Colors[x].Green);
                            JS["name"] = Colors[x].Name;
                            JS["purple"] = RGB2HEX(Colors[x].Purple);
                            JS["red"] = RGB2HEX(Colors[x].Red);
                            JS["selectionBackground"] = RGB2HEX(Colors[x].SelectionBackground);
                            JS["white"] = RGB2HEX(Colors[x].White);
                            JS["yellow"] = RGB2HEX(Colors[x].Yellow);
                            JS["foreground"] = RGB2HEX(Colors[x].Foreground);

                            // # Check for properties reminants from the old JObj to be added to the new one
                            foreach (var item in JSonFileUntouched["schemes"])
                            {
                                if (item["name"].ToString().ToLower() == JS["name"].ToString().ToLower())
                                {
                                    foreach (var itemX in (IEnumerable)item)
                                    {
                                        bool Contains = JS.ContainsKey(itemX.ToString().Split(':')[0].Trim().Replace("\"", ""));
                                        if (!Contains)
                                            JS.Add(itemX);
                                    }
                                    break;
                                }

                            } ((JArray)JSonFile["schemes"]).Add(JS);
                        }

                        // # Check for reminants from the old JObj to be added to the new one
                        foreach (var x in (JArray)JSonFileUntouched["schemes"])
                        {
                            string name1 = x["name"].ToString();
                            bool Found = false;

                            foreach (var y in (JArray)JSonFile["schemes"])
                            {
                                string name2 = y["name"].ToString();

                                if ((name1 ?? "") == (name2 ?? ""))
                                {
                                    Found = true;
                                    break;
                                }

                            }

                            if (!Found)
                            {
                                ((JArray)JSonFile["schemes"]).Add(x);
                            }

                            Found = false;
                        }
                        #endregion

                        #region Defaults
                        JSonFile["profiles"]["defaults"]["backgroundImage"] = DefaultProf.BackgroundImage;
                        if (!string.IsNullOrEmpty(DefaultProf.ColorScheme))
                            JSonFile["profiles"]["defaults"]["colorScheme"] = DefaultProf.ColorScheme;
                        if (!string.IsNullOrEmpty(DefaultProf.TabTitle))
                            JSonFile["profiles"]["defaults"]["tabTitle"] = DefaultProf.TabTitle;
                        if (!string.IsNullOrEmpty(((int)DefaultProf.CursorShape).ToString()))
                            JSonFile["profiles"]["defaults"]["cursorShape"] = CursorShape_ReturnToString(DefaultProf.CursorShape);
                        if (!string.IsNullOrEmpty(DefaultProf.Icon))
                            JSonFile["profiles"]["defaults"]["icon"] = DefaultProf.Icon;
                        if (!(DefaultProf.CursorHeight == 0))
                            JSonFile["profiles"]["defaults"]["cursorHeight"] = DefaultProf.CursorHeight;
                        if (!(DefaultProf.Opacity == 0))
                            JSonFile["profiles"]["defaults"]["opacity"] = DefaultProf.Opacity;

                        if (DefaultProf.TabColor != Color.FromArgb(0, 0, 0, 0))
                        {
                            JSonFile["profiles"]["defaults"]["tabColor"] = RGB2HEX(DefaultProf.TabColor);
                        }
                        else if (((JObject)JSonFile["profiles"]["defaults"]).ContainsKey("tabColor"))
                            JSonFile["profiles"]["defaults"]["tabColor"] = null;


                        JSonFile["profiles"]["defaults"]["useAcrylic"] = DefaultProf.UseAcrylic;
                        JSonFile["profiles"]["defaults"]["backgroundImageOpacity"] = DefaultProf.BackgroundImageOpacity;


                        if (!((JObject)JSonFile["profiles"]["defaults"]).ContainsKey("font"))
                        {
                            // JFont.Add("font"]
                            var JFont = new JObject();
                            if (DefaultProf.Font.Weight! < 0)
                                JFont["weight"] = FontWeight_ReturnToString(DefaultProf.Font.Weight);
                            if (!string.IsNullOrEmpty(DefaultProf.Font.Face))
                                JFont["face"] = DefaultProf.Font.Face;
                            if (!(DefaultProf.Font.Size == 0))
                                JFont["size"] = DefaultProf.Font.Size;
                            JSonFile["profiles"]["defaults"]["font"] = JFont;
                        }

                        else
                        {
                            if (DefaultProf.Font.Weight! < 0)
                                JSonFile["profiles"]["defaults"]["font"]["weight"] = FontWeight_ReturnToString(DefaultProf.Font.Weight);
                            if (!string.IsNullOrEmpty(DefaultProf.Font.Face))
                                JSonFile["profiles"]["defaults"]["font"]["face"] = DefaultProf.Font.Face;
                            if (!(DefaultProf.Font.Size == 0))
                                JSonFile["profiles"]["defaults"]["font"]["size"] = DefaultProf.Font.Size;
                        }
                        #endregion

                        #region Profiles
                        if (JSonFile["profiles"]["list"] is not null)
                            ((JArray)JSonFile["profiles"]["list"]).Clear();
                        else
                            JSonFile["profiles"]["list"] = new JArray();

                        for (int x = 0, loopTo1 = Profiles.Count - 1; x <= loopTo1; x++)
                        {
                            var JS = new JObject();
                            JS["name"] = Profiles[x].Name;

                            JS["backgroundImage"] = Profiles[x].BackgroundImage;
                            JS["cursorShape"] = CursorShape_ReturnToString(Profiles[x].CursorShape);
                            if (!string.IsNullOrEmpty(Profiles[x].ColorScheme))
                                JS["colorScheme"] = Profiles[x].ColorScheme;
                            if (!string.IsNullOrEmpty(Profiles[x].TabTitle))
                                JS["tabTitle"] = Profiles[x].TabTitle;
                            if (!string.IsNullOrEmpty(Profiles[x].Icon))
                                JS["icon"] = Profiles[x].Icon;
                            if (!(Profiles[x].CursorHeight == 0))
                                JS["cursorHeight"] = Profiles[x].CursorHeight;
                            if (!(Profiles[x].Opacity == 0))
                                JS["opacity"] = Profiles[x].Opacity;
                            if (!(Profiles[x].BackgroundImageOpacity == 0f))
                                JS["backgroundImageOpacity"] = Profiles[x].BackgroundImageOpacity;

                            if (Profiles[x].TabColor != Color.FromArgb(0, 0, 0, 0))
                                JS["tabColor"] = RGB2HEX(Profiles[x].TabColor);

                            JS["useAcrylic"] = Profiles[x].UseAcrylic;

                            var JS_Font = new JObject();
                            JS_Font["weight"] = FontWeight_ReturnToString(Profiles[x].Font.Weight);
                            if (Profiles[x].Font.Face is not null)
                                JS_Font["face"] = Profiles[x].Font.Face;
                            if (Profiles[x].Font.Size != 0)
                                JS_Font["size"] = Profiles[x].Font.Size;
                            JS["font"] = JS_Font;

                            // # Check for properties reminants from the old JObj to be added to the new one
                            foreach (var item in JSonFileUntouched["profiles"]["list"])
                            {
                                if (item["name"].ToString().ToLower() == JS["name"].ToString().ToLower())
                                {
                                    foreach (var itemX in (IEnumerable)item)
                                    {
                                        if (itemX.ToString().Split(':')[0].Trim().Replace("\"", "") != "tabColor")
                                        {
                                            bool Contains = JS.ContainsKey(itemX.ToString().Split(':')[0].Trim().Replace("\"", ""));
                                            if (!Contains)
                                                JS.Add(itemX);
                                        }
                                    }
                                    break;
                                }

                            } ((JArray)JSonFile["profiles"]["list"]).Add(JS);
                        }

                        // # Check for reminants from the old JObj to be added to the new one
                        foreach (var x in (JArray)JSonFileUntouched["profiles"]["list"])
                        {
                            string name1 = x["name"].ToString();
                            bool Found = false;

                            foreach (var y in (JArray)JSonFile["profiles"]["list"])
                            {
                                string name2 = y["name"].ToString();

                                if ((name1 ?? "") == (name2 ?? ""))
                                {
                                    Found = true;
                                    break;
                                }

                            }

                            if (!Found)
                            {
                                ((JArray)JSonFile["profiles"]["list"]).Add(x);
                            }

                            Found = false;
                        }
                        #endregion

                        #region Themes

                        if (Themes.Count != 0)
                        {
                            if (JSonFile["themes"] is not null)
                                ((JArray)JSonFile["themes"]).Clear();
                            else
                                JSonFile["themes"] = new JArray();

                            for (int x = 0, loopTo2 = Themes.Count - 1; x <= loopTo2; x++)
                            {
                                var JS = new JObject();

                                if (!string.IsNullOrEmpty(Themes[x].Name))
                                    JS["name"] = Themes[x].Name;

                                var JS_Tabs = new JObject();
                                if (Themes[x].Tab_Active != Color.FromArgb(0, 0, 0, 0))
                                    JS_Tabs["background"] = RGB2HEX(Themes[x].Tab_Active);
                                if (Themes[x].Tab_Inactive != Color.FromArgb(0, 0, 0, 0))
                                    JS_Tabs["unfocusedBackground"] = RGB2HEX(Themes[x].Tab_Inactive);
                                JS["tab"] = JS_Tabs;

                                var JS_TabRow = new JObject();
                                if (Themes[x].Titlebar_Active != Color.FromArgb(0, 0, 0, 0))
                                    JS_TabRow["background"] = RGB2HEX(Themes[x].Titlebar_Active);
                                if (Themes[x].Titlebar_Inactive != Color.FromArgb(0, 0, 0, 0))
                                    JS_TabRow["unfocusedBackground"] = RGB2HEX(Themes[x].Titlebar_Inactive);
                                JS["tabRow"] = JS_TabRow;

                                var JS_Window = new JObject();
                                if (!string.IsNullOrEmpty(Themes[x].ApplicationTheme_light))
                                    JS_Window["applicationTheme"] = Themes[x].ApplicationTheme_light;
                                JS["window"] = JS_Window;

                                ((JArray)JSonFile["themes"]).Add(JS);
                            }
                        }
                        #endregion

                        St.Close();
                        TakeOwnership(File);
                        System.IO.File.WriteAllText(File, JSonFile.ToString());

                        return JSonFile.ToString();
                    }

                default:
                    {
                        return "";
                    }

            }
        }


        public static void TakeOwnership(string filepath)
        {
            var proc = new Process();
            proc.StartInfo.FileName = "takeown.exe";
            proc.StartInfo.Arguments = "/R /F \"" + filepath + "\"";
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
            proc.WaitForExit();
            proc.StartInfo.FileName = "icacls.exe";
            proc.StartInfo.Arguments = "\"" + filepath + "\" /grant *{GROUP_USERS_SID}:F /T";
            proc.Start();
            proc.WaitForExit();
        }

        public static Color HEX2RGB(string String)
        {
            try
            {
                if (String is not null)
                {
                    if (String.Replace("#", "").Count() / 2d == 3d)
                    {
                        return Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(String.Replace("#", ""), 16)));
                    }
                    else
                    {
                        int a = Convert.ToInt32(String.Substring(String.Count() - 2, 2), 16);
                        return Color.FromArgb(a, Color.FromArgb(Convert.ToInt32(String.Remove(String.Count() - 2, 2).Replace("#", ""), 16)));
                    }
                }
                else
                {
                    return Color.FromArgb(0, 0, 0, 0);
                }
            }
            catch
            {
                return Color.FromArgb(0, 0, 0, 0);
            }
        }

        public string RGB2HEX(Color Color)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}", Color.R, Color.G, Color.B);
        }

        public static bool operator ==(WinTerminal First, WinTerminal Second)
        {
            bool _equal = true;
            // MsgBox(Enumerable.SequenceEqual(First.Colors, Second.Colors))
            if (!(First.DefaultProf == Second.DefaultProf))
                _equal = false;
            if (!((First.Theme ?? "") == (Second.Theme ?? "")))
                _equal = false;
            if (!(First.UseAcrylicInTabRow == Second.UseAcrylicInTabRow))
                _equal = false;
            if (!(First.Enabled == Second.Enabled))
                _equal = false;
            return _equal;
        }

        public static bool operator !=(WinTerminal First, WinTerminal Second)
        {
            return !(First == Second);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class TColor : IComparable, ICloneable
    {

        public string Name { get; set; }
        public Color Background { get; set; } = "FF0C0C0C".FromHEXToColor(true);
        public Color Foreground { get; set; } = "FFCCCCCC".FromHEXToColor(true);
        public Color SelectionBackground { get; set; } = "FFFFFFFF".FromHEXToColor(true);
        public Color Black { get; set; } = "FF0C0C0C".FromHEXToColor(true);
        public Color Blue { get; set; } = "FF0037DA".FromHEXToColor(true);
        public Color BrightBlack { get; set; } = "FF767676".FromHEXToColor(true);
        public Color BrightBlue { get; set; } = "FF3B78FF".FromHEXToColor(true);
        public Color BrightCyan { get; set; } = "FF61D6D6".FromHEXToColor(true);
        public Color BrightGreen { get; set; } = "FF16C60C".FromHEXToColor(true);
        public Color BrightPurple { get; set; } = "FFB4009E".FromHEXToColor(true);
        public Color BrightRed { get; set; } = "FFE74856".FromHEXToColor(true);
        public Color BrightWhite { get; set; } = "FFF2F2F2".FromHEXToColor(true);
        public Color BrightYellow { get; set; } = "FFF9F1A5".FromHEXToColor(true);
        public Color CursorColor { get; set; } = "FFFFFFFF".FromHEXToColor(true);
        public Color Cyan { get; set; } = "FF3A96DD".FromHEXToColor(true);
        public Color Green { get; set; } = "FF13A10E".FromHEXToColor(true);
        public Color Purple { get; set; } = "FF881798".FromHEXToColor(true);
        public Color Red { get; set; } = "FFC50F1F".FromHEXToColor(true);
        public Color White { get; set; } = "FFCCCCCC".FromHEXToColor(true);
        public Color Yellow { get; set; } = "FFC19C00".FromHEXToColor(true);

        public int CompareTo(object obj)
        {
            if (this == (TColor)obj)
                return 1;
            else
                return 0;
        }

        public static bool operator ==(TColor First, TColor Second)
        {
            return First.Equals(Second);
        }

        public static bool operator !=(TColor First, TColor Second)
        {
            return !First.Equals(Second);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class ThemesList : IComparable, ICloneable
    {
        public string Name { get; set; }
        public Color Titlebar_Active { get; set; } = Color.FromArgb(0, 0, 0, 0);
        public Color Titlebar_Inactive { get; set; } = Color.FromArgb(0, 0, 0, 0);
        public Color Tab_Active { get; set; } = Color.FromArgb(0, 0, 0, 0);
        public Color Tab_Inactive { get; set; } = Color.FromArgb(0, 0, 0, 0);
        public string ApplicationTheme_light { get; set; } = "dark";

        public int CompareTo(object obj)
        {
            if (Equals((ThemesList)obj))
                return 1;
            else
                return 0;
        }

        public static bool operator ==(ThemesList First, ThemesList Second)
        {
            return First.Equals(Second);
        }

        public static bool operator !=(ThemesList First, ThemesList Second)
        {
            return !First.Equals(Second);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class FontsBase : ICloneable
    {
        public string Face { get; set; } = OS.W11 ? "Cascadia Mono" : "Consolas";
        public FontWeight_Enum Weight { get; set; } = FontWeight_Enum.normal;
        public int Size { get; set; } = 12;

        public static bool operator ==(FontsBase First, FontsBase Second)
        {
            return First.Equals(Second);
        }

        public static bool operator !=(FontsBase First, FontsBase Second)
        {
            return !First.Equals(Second);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class ProfilesList : IComparable, ICloneable
    {
        public string Name { get; set; }
        public string TabTitle { get; set; } = "";
        public string Icon { get; set; } = "";
        public string Commandline { get; set; }

        public Color TabColor { get; set; } = Color.FromArgb(0, 0, 0, 0);
        public bool UseAcrylic { get; set; } = false;
        public int Opacity { get; set; } = 100;

        public FontsBase Font { get; set; } = new FontsBase();
        public string BackgroundImage { get; set; } = "";
        public float BackgroundImageOpacity { get; set; } = 1f;

        public string ColorScheme { get; set; } = "Campbell";
        public CursorShape_Enum CursorShape { get; set; } = CursorShape_Enum.bar;
        public int CursorHeight { get; set; } = 25;

        public int CompareTo(object obj)
        {
            if (Equals((ProfilesList)obj))
                return 1;
            else
                return 0;
        }

        public static bool operator ==(ProfilesList First, ProfilesList Second)
        {
            return First.Equals(Second);
        }

        public static bool operator !=(ProfilesList First, ProfilesList Second)
        {
            return !First.Equals(Second);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        #region Helpers
        public enum BackgroundImageAlignment_Enum
        {
            bottom,
            bottomLeft,
            bottomRight,
            center,
            left,
            right,
            top,
            topLeft,
            topRight
        }
        public enum CursorShape_Enum
        {
            bar,
            doubleUnderscore,
            emptyBox,
            filledBox,
            underscore,
            vintage
        }
        public static string CursorShape_ReturnToString(CursorShape_Enum @int)
        {
            switch (@int)
            {
                case CursorShape_Enum.bar:
                    {
                        return "bar";
                    }

                case CursorShape_Enum.doubleUnderscore:
                    {
                        return "doubleUnderscore";
                    }

                case CursorShape_Enum.emptyBox:
                    {
                        return "emptyBox";
                    }

                case CursorShape_Enum.filledBox:
                    {
                        return "filledBox";
                    }

                case CursorShape_Enum.underscore:
                    {
                        return "underscore";
                    }

                case CursorShape_Enum.vintage:
                    {
                        return "vintage";
                    }

                default:
                    {
                        return "bar";
                    }

            }
        }
        public static CursorShape_Enum CursorShape_GetFromString(string str)
        {
            switch (str.ToLower() ?? "")
            {
                case var @case when @case == ("bar".ToLower() ?? ""):
                    {
                        return CursorShape_Enum.bar;
                    }

                case var case1 when case1 == ("doubleunderscore".ToLower() ?? ""):
                    {
                        return CursorShape_Enum.doubleUnderscore;
                    }

                case var case2 when case2 == ("emptybox".ToLower() ?? ""):
                    {
                        return CursorShape_Enum.emptyBox;
                    }

                case var case3 when case3 == ("filledbox".ToLower() ?? ""):
                    {
                        return CursorShape_Enum.filledBox;
                    }

                case var case4 when case4 == ("underscore".ToLower() ?? ""):
                    {
                        return CursorShape_Enum.underscore;
                    }

                case var case5 when case5 == ("vintage".ToLower() ?? ""):
                    {
                        return CursorShape_Enum.vintage;
                    }

                default:
                    {
                        return CursorShape_Enum.bar;
                    }

            }
        }
        public enum FontWeight_Enum   // replace _ by -
        {
            thin,
            extra_light,
            light,
            semi_light,
            normal,
            medium,
            semi_bold,
            bold,
            extra_bold,
            black,
            extra_black
        }
        public static string FontWeight_ReturnToString(FontWeight_Enum @int)
        {
            switch (@int)
            {
                case FontWeight_Enum.black:
                    {
                        return "black";
                    }

                case FontWeight_Enum.bold:
                    {
                        return "bold";
                    }

                case FontWeight_Enum.extra_black:
                    {
                        return "extra-black";
                    }

                case FontWeight_Enum.extra_bold:
                    {
                        return "extra-bold";
                    }

                case FontWeight_Enum.extra_light:
                    {
                        return "extra-light";
                    }

                case FontWeight_Enum.light:
                    {
                        return "light";
                    }

                case FontWeight_Enum.medium:
                    {
                        return "medium";
                    }

                case FontWeight_Enum.normal:
                    {
                        return "normal";
                    }

                case FontWeight_Enum.semi_bold:
                    {
                        return "semi-bold";
                    }

                case FontWeight_Enum.semi_light:
                    {
                        return "semi-light";
                    }

                case FontWeight_Enum.thin:
                    {
                        return "thin";
                    }

                default:
                    {
                        return "normal";
                    }

            }
        }
        public static FontWeight_Enum FontWeight_GetFromString(string str)
        {
            switch (str.ToLower() ?? "")
            {

                case var @case when @case == ("thin".ToLower() ?? ""):
                    {
                        return FontWeight_Enum.thin;
                    }

                case var case1 when case1 == ("extra-light".ToLower() ?? ""):
                    {
                        return FontWeight_Enum.extra_light;
                    }

                case var case2 when case2 == ("light".ToLower() ?? ""):
                    {
                        return FontWeight_Enum.light;
                    }

                case var case3 when case3 == ("semi-light".ToLower() ?? ""):
                    {
                        return FontWeight_Enum.semi_light;
                    }

                case var case4 when case4 == ("medium".ToLower() ?? ""):
                    {
                        return FontWeight_Enum.medium;
                    }

                case var case5 when case5 == ("normal".ToLower() ?? ""):
                    {
                        return FontWeight_Enum.normal;
                    }

                case var case6 when case6 == ("semi-bold".ToLower() ?? ""):
                    {
                        return FontWeight_Enum.semi_bold;
                    }

                case var case7 when case7 == ("bold".ToLower() ?? ""):
                    {
                        return FontWeight_Enum.bold;
                    }

                case var case8 when case8 == ("extra-bold".ToLower() ?? ""):
                    {
                        return FontWeight_Enum.extra_bold;
                    }

                case var case9 when case9 == ("black".ToLower() ?? ""):
                    {
                        return FontWeight_Enum.black;
                    }

                case var case10 when case10 == ("extra-black".ToLower() ?? ""):
                    {
                        return FontWeight_Enum.extra_black;
                    }

                default:
                    {
                        return FontWeight_Enum.normal;
                    }

            }
        }
        #endregion
    }
}