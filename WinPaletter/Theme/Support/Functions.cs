﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using WinPaletter.Theme.Structures;

namespace WinPaletter.Theme
{
    public partial class Manager
    {
        /// <summary>
        /// Get all colors inside a .theme file
        /// </summary>
        /// <param name="Filename">.theme file</param>
        /// <returns></returns>
        public static List<Color> GetPaletteFromMSTheme(string Filename)
        {
            if (System.IO.File.Exists(Filename))
            {

                var ls = new List<Color>();
                ls.Clear();

                var tx = System.IO.File.ReadAllText(Filename).CList();

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

        /// <summary>
        /// Get all colors inside from a string
        /// <br><b>Take a look at Properties.Resources.RetroThemesDB</b></br>
        /// </summary>
        /// <param name="String">String that has .theme file data</param>
        /// <param name="ThemeName">Selected theme name</param>
        /// <returns></returns>
        public static List<Color> GetPaletteFromString(string String, string ThemeName)
        {
            if (string.IsNullOrWhiteSpace(String) || !String.Contains("|") || string.IsNullOrWhiteSpace(ThemeName)) { return null; }

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

            if (!Found) { return null; }

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

        /// <summary>
        /// Get all colors inside current WinPaletter theme
        /// </summary>
        /// <returns></returns>
        public List<Color> Colors(bool DontMergeRepeatedColors = false)
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

            foreach (var field in typeof(Theme.Structures.LogonUI7).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType.Name.ToLower() == "color")
                {
                    CL.Add((Color)field.GetValue(LogonUI7));
                }
            }

            foreach (var field in typeof(Theme.Structures.LogonUIXP).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType.Name.ToLower() == "color")
                {
                    CL.Add((Color)field.GetValue(LogonUIXP));
                }
            }

            foreach (var field in typeof(Theme.Structures.Win32UI).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
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

            foreach (var field in typeof(Theme.Structures.Console).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
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

            foreach (var field in typeof(Theme.Structures.Cursor).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
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
        
        /// <summary>
        /// Checks if a font is installed or not (from its name)
        /// </summary>
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

        /// <summary>
        /// Checks if a font is installed or not (from its name and style)
        /// </summary>
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

        /// <summary>
        /// Decompress a WinPaletter theme file
        /// </summary>
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

        /// <summary>
        /// Checks if this type is a structure or not
        /// </summary>
        public bool IsStructure(Type type)
        {
            return type.IsValueType && !type.IsPrimitive && type.Namespace is not null && !type.Namespace.StartsWith("System.");
        }
    }
}