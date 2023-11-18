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
using static WinPaletter.TProfile;

namespace WinPaletter
{
    /// <summary>
    /// Class that has data of Windows Terminal settings
    /// </summary>
    public class WinTerminal : ICloneable
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled { get; set; } = false;

        /// <summary>List of terminal colors</summary>
        public List<TColors> Colors { get; set; }

        /// <summary>List of terminal profiles</summary>
        public List<TProfile> Profiles { get; set; }

        /// <summary>Default Windows Terminal profile</summary>
        public TProfile DefaultProf { get; set; }

        /// <summary>List of terminal themes</summary>
        public List<TTheme> Themes { get; set; }

        /// <summary>Current Windows Terminal theme</summary>
        public string Theme { get; set; } = "system";

        /// <summary>Controls if Windows Terminal titlebar has acrylic effect</summary>
        public bool UseAcrylicInTabRow { get; set; } = false;

        /// <summary>
        /// Create an instance of class has all data of Windows Terminal settings
        /// </summary>
        /// <param name="File">File to be opened, either JSON or WinPaletter theme file</param>
        /// <param name="Mode">Either Windows Terminal JSON settings file or WinPaletter theme file</param>
        /// <param name="Version">Either Stable or Preview</param>
        public WinTerminal(string File, Mode Mode, Version Version = Version.Stable)
        {
            switch (Mode)
            {
                case Mode.JSONFile:
                    {
                        if (System.IO.File.Exists(File))
                        {
                            var St = new StreamReader(File);
                            string JSON_String = St.ReadToEnd();

                            try
                            {
                                JObject JSonFile = JObject.Parse(JSON_String);

                                if (JSonFile["useAcrylicInTabRow"] is not null)
                                    UseAcrylicInTabRow = (bool)JSonFile["useAcrylicInTabRow"];
                                if (JSonFile["theme"] is not null)
                                    Theme = JSonFile["theme"].ToString();


                                #region Getting Default Profile
                                DefaultProf = new TProfile();

                                if (JSonFile["profiles"] is not null)
                                {
                                    if (JSonFile["profiles"]["defaults"] is not null)
                                    {

                                        if (JSonFile["profiles"]["defaults"]["name"] is not null)
                                            DefaultProf.Name = JSonFile["profiles"]["defaults"]["name"].ToString();
                                        if (JSonFile["profiles"]["defaults"]["backgroundImage"] is not null)
                                            DefaultProf.BackgroundImage = JSonFile["profiles"]["defaults"]["backgroundImage"].ToString();
                                        if (JSonFile["profiles"]["defaults"]["cursorShape"] is not null)
                                            DefaultProf.CursorShape = TProfile.CursorShape_GetFromString(JSonFile["profiles"]["defaults"]["cursorShape"].ToString());

                                        if (JSonFile["profiles"]["defaults"]["font"] is not null)
                                        {
                                            if (JSonFile["profiles"]["defaults"]["font"]["weight"] is not null)
                                                DefaultProf.Font.Weight = TProfile.FontWeight_GetFromString(JSonFile["profiles"]["defaults"]["font"]["weight"].ToString());
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
                                Profiles = new List<TProfile>();
                                Profiles.Clear();

                                if (JSonFile["profiles"] is not null)
                                {
                                    if (JSonFile["profiles"]["list"] is not null)
                                    {
                                        foreach (var item in JSonFile["profiles"]["list"])
                                        {
                                            var P = new TProfile();
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
                                Colors = new List<TColors>();
                                Colors.Clear();

                                if (JSonFile["schemes"] is not null)
                                {
                                    foreach (var item in JSonFile["schemes"])
                                    {
                                        var TC = new TColors();

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

                                #endregion

                                #region Getting All Themes
                                Themes = new List<TTheme>();
                                Themes.Clear();

                                if (JSonFile["themes"] is not null)
                                {
                                    foreach (var item in JSonFile["themes"])
                                    {
                                        var Th = new TTheme();
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
                                                Th.Style = item["window"]["applicationTheme"].ToString();
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
                        }

                        break;
                    }

                case Mode.WinPaletterFile:
                    {
                        using (var TBx = new Theme.Manager(WinPaletter.Theme.Manager.Source.File, File))
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

        /// <summary>
        /// Enumeration for ways by which WinPaletter can get Windows Terminal settings data
        /// </summary>
        public enum Mode : int
        {
            /// <summary>Default Windows Terminal settings</summary>
            Default,
            /// <summary>Windows Terminal JSON settings file</summary>
            JSONFile,
            /// <summary>WinPaletter theme  file</summary>
            WinPaletterFile,
            /// <summary>Empty data that has nothing; no profiles, no themes, ...</summary>
            Empty
        }

        /// <summary>
        /// Enumeration of Windows Terminal editions
        /// </summary>
        public enum Version : int
        {
            /// <summary></summary>
            Stable,
            /// <summary></summary>
            Preview
        }

        /// <summary>
        /// Save Windows Terminal settings data
        /// </summary>
        /// <param name="File">File into which data will be saved, either JSON or WinPaletter theme file</param>
        /// <param name="Mode">Either Windows Terminal JSON settings file or WinPaletter theme file</param>
        /// <param name="Version">Either Stable or Preview</param>
        /// <returns></returns>
        public string Save(string File, Mode Mode, Version Version = Version.Stable)
        {
            switch (Mode)
            {
                case Mode.JSONFile:
                    {
                        string SettingsFile = string.Empty;

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
                                        bool Contains = JS.ContainsKey(itemX.ToString().Split(':')[0].Trim().Replace("\"", string.Empty));
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

                                if ((name1 ?? string.Empty) == (name2 ?? string.Empty))
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
                                        if (itemX.ToString().Split(':')[0].Trim().Replace("\"", string.Empty) != "tabColor")
                                        {
                                            bool Contains = JS.ContainsKey(itemX.ToString().Split(':')[0].Trim().Replace("\"", string.Empty));
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

                                if ((name1 ?? string.Empty) == (name2 ?? string.Empty))
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
                                if (!string.IsNullOrEmpty(Themes[x].Style))
                                    JS_Window["applicationTheme"] = Themes[x].Style;
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
                        return string.Empty;
                    }

            }
        }

        /// <summary>
        /// Take ownership of file to current Windows user
        /// </summary>
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

        /// <summary>
        /// Convert HEX string into .NET RGB color structure
        /// </summary>
        /// <returns></returns>
        public static Color HEX2RGB(string HEX)
        {
            try
            {
                if (HEX is not null)
                {
                    if (HEX.Replace("#", string.Empty).Count() / 2d == 3d)
                    {
                        return Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(HEX.Replace("#", string.Empty), 16)));
                    }
                    else
                    {
                        int a = Convert.ToInt32(HEX.Substring(HEX.Count() - 2, 2), 16);
                        return Color.FromArgb(a, Color.FromArgb(Convert.ToInt32(HEX.Remove(HEX.Count() - 2, 2).Replace("#", string.Empty), 16)));
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

        /// <summary>
        /// Convert .NET RGB color structure into HEX string
        /// </summary>
        /// <returns></returns>
        public string RGB2HEX(Color Color)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}", Color.R, Color.G, Color.B);
        }

        /// <summary>
        /// Operator to check if two objects of Windows Terminal class are equal
        /// </summary>
        public static bool operator ==(WinTerminal First, WinTerminal Second)
        {
            bool _equal = true;
            // MsgBox(Enumerable.SequenceEqual(First.Colors, Second.Colors))
            if (!(First.DefaultProf == Second.DefaultProf))
                _equal = false;
            if (!((First.Theme ?? string.Empty) == (Second.Theme ?? string.Empty)))
                _equal = false;
            if (!(First.UseAcrylicInTabRow == Second.UseAcrylicInTabRow))
                _equal = false;
            if (!(First.Enabled == Second.Enabled))
                _equal = false;
            return _equal;
        }

        /// <summary>
        /// Operator to check if two objects of Windows Terminal class are not equal
        /// </summary>
        public static bool operator !=(WinTerminal First, WinTerminal Second)
        {
            return !(First == Second);
        }

        /// <summary>
        /// Clones current Windows Terminal object
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Checks if two objects of Windows Terminal class are equal or not
        /// </summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Gets current Windows Terminal object hash code
        /// </summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    /// <summary>
    /// Class that contains data for Windows Terminal colors
    /// </summary>
    public class TColors : IComparable, ICloneable
    {
        /// <summary>Color profile name</summary>
        public string Name { get; set; }

        /// <summary>Background color</summary>
        public Color Background { get; set; } = "FF0C0C0C".FromHEXToColor(true);

        /// <summary>Foreground color</summary>
        public Color Foreground { get; set; } = "FFCCCCCC".FromHEXToColor(true);

        /// <summary>Selection background color (i.e. highlight)</summary>
        public Color SelectionBackground { get; set; } = "FFFFFFFF".FromHEXToColor(true);

        /// <summary>The color that should represents black color</summary>
        public Color Black { get; set; } = "FF0C0C0C".FromHEXToColor(true);

        /// <summary>The color that should represents blue color</summary>
        public Color Blue { get; set; } = "FF0037DA".FromHEXToColor(true);

        /// <summary>The color that should represents bright black color</summary>
        public Color BrightBlack { get; set; } = "FF767676".FromHEXToColor(true);

        /// <summary>The color that should represents bright blue color</summary>
        public Color BrightBlue { get; set; } = "FF3B78FF".FromHEXToColor(true);

        /// <summary>The color that should represents bright cyan color</summary>
        public Color BrightCyan { get; set; } = "FF61D6D6".FromHEXToColor(true);

        /// <summary>The color that should represents bright green color</summary>
        public Color BrightGreen { get; set; } = "FF16C60C".FromHEXToColor(true);

        /// <summary>The color that should represents bright purple color</summary>
        public Color BrightPurple { get; set; } = "FFB4009E".FromHEXToColor(true);

        /// <summary>The color that should represents bright red color</summary>
        public Color BrightRed { get; set; } = "FFE74856".FromHEXToColor(true);

        /// <summary>The color that should represents bright white color</summary>
        public Color BrightWhite { get; set; } = "FFF2F2F2".FromHEXToColor(true);

        /// <summary>The color that should represents bright yellow color</summary>
        public Color BrightYellow { get; set; } = "FFF9F1A5".FromHEXToColor(true);

        /// <summary>Cursor (text carret) color</summary>
        public Color CursorColor { get; set; } = "FFFFFFFF".FromHEXToColor(true);

        /// <summary>The color that should represents cyan color</summary>
        public Color Cyan { get; set; } = "FF3A96DD".FromHEXToColor(true);

        /// <summary>The color that should represents green color</summary>
        public Color Green { get; set; } = "FF13A10E".FromHEXToColor(true);

        /// <summary>The color that should represents purple color</summary>
        public Color Purple { get; set; } = "FF881798".FromHEXToColor(true);

        /// <summary>The color that should represents red color</summary>
        public Color Red { get; set; } = "FFC50F1F".FromHEXToColor(true);

        /// <summary>The color that should represents white color</summary>
        public Color White { get; set; } = "FFCCCCCC".FromHEXToColor(true);

        /// <summary>The color that should represents yellow color</summary>
        public Color Yellow { get; set; } = "FFC19C00".FromHEXToColor(true);

        /// <summary>
        /// Compares two Windows Terminal colors objects
        /// </summary>
        /// <returns>1 or 0</returns>
        public int CompareTo(object obj)
        {
            if (this == (TColors)obj)
                return 1;
            else
                return 0;
        }

        /// <summary>
        /// Operator to check if two objects of Windows Terminal colors class are equal
        /// </summary>
        public static bool operator ==(TColors First, TColors Second)
        {
            return First.Equals(Second);
        }

        /// <summary>
        /// Operator to check if two objects of Windows Terminal colors class are not equal
        /// </summary>
        public static bool operator !=(TColors First, TColors Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>
        /// Clones Windows Terminal colors object
        /// </summary>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Checks if two objects of Windows Terminal colors class are equal or not
        /// </summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Gets Windows Terminal colors object hash code
        /// </summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }

    /// <summary>
    /// Class that contains data for Windows Terminal theme
    /// </summary>
    public class TTheme : IComparable, ICloneable
    {
        /// <summary>Theme name</summary>
        public string Name { get; set; }

        /// <summary>Active titlebar color</summary>
        public Color Titlebar_Active { get; set; } = Color.FromArgb(0, 0, 0, 0);

        /// <summary>Inactive titlebar color</summary>
        public Color Titlebar_Inactive { get; set; } = Color.FromArgb(0, 0, 0, 0);

        /// <summary>Active tab color</summary>
        public Color Tab_Active { get; set; } = Color.FromArgb(0, 0, 0, 0);

        /// <summary>Inactive tab color</summary>
        public Color Tab_Inactive { get; set; } = Color.FromArgb(0, 0, 0, 0);

        /// <summary>Style can be "dark", "light" or "system"</summary>
        public string Style { get; set; } = "dark";

        /// <summary>
        /// Compares two Windows Terminal themes objects
        /// </summary>
        /// <returns>1 or 0</returns>
        public int CompareTo(object obj)
        {
            if (Equals((TTheme)obj))
                return 1;
            else
                return 0;
        }

        /// <summary>
        /// Operator to check if two objects of Windows Terminal theme class are equal
        /// </summary>
        public static bool operator ==(TTheme First, TTheme Second)
        {
            return First.Equals(Second);
        }

        /// <summary>
        /// Operator to check if two objects of Windows Terminal theme class are not equal
        /// </summary>
        public static bool operator !=(TTheme First, TTheme Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>
        /// Clones current Windows Terminal theme object
        /// </summary>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Checks if two objects of Windows Terminal theme class are equal or not
        /// </summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Gets current current Windows Terminal theme object hash code
        /// </summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }

    /// <summary>
    /// Class that contains data for Windows Terminal font
    /// </summary>
    public class TFont : ICloneable
    {
        /// <summary>Font name</summary>
        public string Face { get; set; } = (OS.W12 || OS.W11) ? "Cascadia Mono" : "Consolas";

        /// <summary>Font weight</summary>
        public FontWeight_Enum Weight { get; set; } = FontWeight_Enum.normal;

        /// <summary>Font size</summary>
        public int Size { get; set; } = 12;

        /// <summary>
        /// Operator to check if two objects of Windows Terminal font class are equal
        /// </summary>
        public static bool operator ==(TFont First, TFont Second)
        {
            return First.Equals(Second);
        }

        /// <summary>
        /// Operator to check if two objects of Windows Terminal font class are not equal
        /// </summary>
        public static bool operator !=(TFont First, TFont Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>
        /// Clones current Windows Terminal font object
        /// </summary>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Checks if two objects of Windows Terminal font class are equal or not
        /// </summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Gets current current Windows Terminal font object hash code
        /// </summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    /// <summary>
    /// Class that contains data for Windows Terminal profile
    /// </summary>
    public class TProfile : IComparable, ICloneable
    {
        /// <summary>Name of Windows Terminal profile</summary>
        public string Name { get; set; }

        /// <summary>Windows Terminal profile title on tab</summary>
        public string TabTitle { get; set; } = string.Empty;

        /// <summary>
        /// Icon of profile on tab
        /// <br><b>- It can be a path to PNG file or string emoji for "Segoe Fluent Icons" font</b></br>
        /// </summary>
        public string Icon { get; set; } = string.Empty;

        /// <summary>
        /// Command that starts the profile
        /// <br><b>- For example: Command Prompt is launched by 'C:\Windows\System32\cmd.exe'</b></br>
        /// </summary>
        public string Commandline { get; set; }

        /// <summary>Windows Terminal profile color on tab</summary>
        public Color TabColor { get; set; } = Color.FromArgb(0, 0, 0, 0);

        /// <summary>Use acrylic background effect for this profile</summary>
        public bool UseAcrylic { get; set; } = false;

        /// <summary>Background opacity for this profile</summary>
        public int Opacity { get; set; } = 100;

        /// <summary>Object dependent on Windows Terminal font class. This object has data of font name, size and weight.</summary>
        public TFont Font { get; set; } = new TFont();

        /// <summary>Background image path for this profile</summary>
        public string BackgroundImage { get; set; } = string.Empty;

        /// <summary>Background image opacity for this profile</summary>
        public float BackgroundImageOpacity { get; set; } = 1f;

        /// <summary>Selected color scheme for this profile. 
        /// <br></br>It should be a 'Name' property for object made of TColors class
        /// <code>
        /// - For example:
        /// MyProfile.ColorScheme = "Campbell"
        /// </code>
        /// </summary>
        public string ColorScheme { get; set; } = "Campbell";

        /// <summary>Cursor (text carret) shape for this profile</summary>
        public CursorShape_Enum CursorShape { get; set; } = CursorShape_Enum.bar;

        /// <summary>Cursor (text carret) height for this profile</summary>
        public int CursorHeight { get; set; } = 25;

        /// <summary>
        /// Compares two objects of Windows Terminal profiles
        /// </summary>
        /// <returns>1 or 0</returns>
        public int CompareTo(object obj)
        {
            if (Equals((TProfile)obj))
                return 1;
            else
                return 0;
        }

        /// <summary>
        /// Operator to check if two objects of Windows Terminal profile class are equal
        /// </summary>
        public static bool operator ==(TProfile First, TProfile Second)
        {
            return First.Equals(Second);
        }

        /// Operator to check if two objects of Windows Terminal profile class are not equal
        public static bool operator !=(TProfile First, TProfile Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>
        /// Clones current Windows Terminal profile object
        /// </summary>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Checks if two objects of Windows Terminal profile class are equal or not
        /// </summary>
        /// <param name="obj"></param>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Gets current current Windows Terminal profile object hash code
        /// </summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region Helpers
        /// <summary>
        /// Enumeration for profile background image alignments
        /// </summary>
        public enum BackgroundImageAlignment_Enum
        {
            ///
            bottom,
            ///
            bottomLeft,
            ///
            bottomRight,
            ///
            center,
            ///
            left,
            ///
            right,
            ///
            top,
            ///
            topLeft,
            ///
            topRight
        }

        /// <summary>
        /// Enumeration for profile cursor (text carret) shapes
        /// </summary>
        public enum CursorShape_Enum
        {
            /// <summary>|</summary>
            bar,
            /// <summary>=</summary>
            doubleUnderscore,
            /// <summary>
            /// ┌┐<br></br>
            /// └┘
            /// </summary>
            emptyBox,
            /// <summary>▐</summary>
            filledBox,
            /// <summary>_</summary>
            underscore,
            /// <summary> ▄</summary>
            vintage
        }

        /// <summary>
        /// Get string name of a cursor style
        /// </summary>
        public static string CursorShape_ReturnToString(CursorShape_Enum shape)
        {
            switch (shape)
            {
                case CursorShape_Enum.bar:
                    return "bar";

                case CursorShape_Enum.doubleUnderscore:
                    return "doubleUnderscore";

                case CursorShape_Enum.emptyBox:
                    return "emptyBox";

                case CursorShape_Enum.filledBox:
                    return "filledBox";

                case CursorShape_Enum.underscore:
                    return "underscore";

                case CursorShape_Enum.vintage:
                    return "vintage";

                default:
                    return "bar";
            }
        }

        /// <summary>
        /// Get cursor shape from a string
        /// <code>
        /// "bar"
        /// "doubleUnderscore"
        /// "emptyBox"
        /// "filledBox"
        /// "underscore"
        /// "vintage"
        /// </code>
        /// </summary>
        /// <param name="str">string</param>
        public static CursorShape_Enum CursorShape_GetFromString(string str)
        {
            return (str.ToLower() ?? string.Empty) switch
            {
                "bar" => CursorShape_Enum.bar,
                "doubleunderscore" => CursorShape_Enum.doubleUnderscore,
                "emptybox" => CursorShape_Enum.emptyBox,
                "filledbox" => CursorShape_Enum.filledBox,
                "underscore" => CursorShape_Enum.underscore,
                "vintage" => CursorShape_Enum.vintage,
                _ => CursorShape_Enum.bar,
            };
        }

        /// <summary>
        /// Enumeration for font weights
        /// <br><b>(!) Replace _ by - when using it as string</b></br>
        /// </summary>
        public enum FontWeight_Enum
        {
            ///
            thin,
            ///
            extra_light,
            ///
            light,
            ///
            semi_light,
            ///
            normal,
            ///
            medium,
            ///
            semi_bold,
            ///
            bold,
            ///
            extra_bold,
            ///
            black,
            ///
            extra_black
        }

        /// <summary>
        /// Get string name of a font weight
        /// </summary>
        public static string FontWeight_ReturnToString(FontWeight_Enum Weight)
        {
            switch (Weight)
            {
                case FontWeight_Enum.black:
                    return "black";

                case FontWeight_Enum.bold:
                    return "bold";

                case FontWeight_Enum.extra_black:
                    return "extra-black";

                case FontWeight_Enum.extra_bold:
                    return "extra-bold";

                case FontWeight_Enum.extra_light:
                    return "extra-light";

                case FontWeight_Enum.light:
                    return "light";

                case FontWeight_Enum.medium:
                    return "medium";

                case FontWeight_Enum.normal:
                    return "normal";

                case FontWeight_Enum.semi_bold:
                    return "semi-bold";

                case FontWeight_Enum.semi_light:
                    return "semi-light";

                case FontWeight_Enum.thin:
                    return "thin";

                default:
                    return "normal";

            }
        }

        /// <summary>
        /// Get font weight from a string
        /// </summary>
        public static FontWeight_Enum FontWeight_GetFromString(string String)
        {
            return (String.ToLower() ?? string.Empty) switch
            {
                "thin" => FontWeight_Enum.thin,
                "extra-light" => FontWeight_Enum.extra_light,
                "light" => FontWeight_Enum.light,
                "semi-light" => FontWeight_Enum.semi_light,
                "medium" => FontWeight_Enum.medium,
                "normal" => FontWeight_Enum.normal,
                "semi-bold" => FontWeight_Enum.semi_bold,
                "bold" => FontWeight_Enum.bold,
                "extra-bold" => FontWeight_Enum.extra_bold,
                "black" => FontWeight_Enum.black,
                "extra-black" => FontWeight_Enum.extra_black,
                _ => FontWeight_Enum.normal,
            };
        }
        #endregion
    }
}