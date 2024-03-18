using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using WinPaletter.Theme.Structures;

namespace WinPaletter.Theme
{
    public partial class Manager
    {
        private static RGBColorComparer colorComparer = new();

        /// <summary>
        /// Get all colors inside a .theme file
        /// </summary>
        /// <param name="Filename">.theme file</param>
        /// <returns></returns>
        public static List<Color> ListColorsFromMSTheme(string Filename)
        {
            if (System.IO.File.Exists(Filename))
            {
                List<Color> ls = new();
                ls.Clear();

                foreach (string x in System.IO.File.ReadAllText(Filename).Split('\r'))
                {
                    if (x.Contains("=") && x.Split('=').Count() >= 2 && x.Split('=')[1].Contains(" ") && x.Split('=')[1].Split(' ').Count() == 3)
                    {
                        string c = x.Split('=')[1];
                        bool inx = true;
                        foreach (string u in c.Split(' '))
                        {
                            if (!u.All(char.IsDigit)) inx = false;
                        }
                        if (inx) ls.Add(c.FromWin32RegToColor());
                    }
                }

                ls = ls.Distinct().ToList();
                ls.Sort(colorComparer);
                return ls;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get all colors inside from a string
        /// <br><b>Take a look at Properties.Resources.ClassicColorsDB</b></br>
        /// </summary>
        /// <param name="DB">DB that has .theme file data</param>
        /// <param name="ThemeName">Selected theme name</param>
        /// <returns></returns>
        public static List<Color> ListColorsFromString(string DB, string ThemeName)
        {
            if (string.IsNullOrWhiteSpace(DB) || !DB.Contains("|") || string.IsNullOrWhiteSpace(ThemeName)) { return null; }

            List<Color> ls = new();
            ls.Clear();

            string SelectedTheme = string.Empty;
            bool Found = false;

            foreach (string th in DB.Split('\n'))
            {
                if ((th.Split('|')[0].ToLower() ?? string.Empty) == (ThemeName.ToLower() ?? string.Empty))
                {
                    SelectedTheme = th.Replace("|", "\r\n");
                    Found = true;
                    break;
                }
            }

            if (!Found) { return null; }

            foreach (string x in SelectedTheme.Split('\r'))
            {
                if (x.Contains("=") && x.Split('=').Count() >= 2 && x.Split('=')[1].Contains(" ") && x.Split('=')[1].Split(' ').Count() == 3)
                {
                    string c = x.Split('=')[1];
                    bool inx = true;
                    foreach (string u in c.Split(' '))
                    {
                        if (!u.All(char.IsDigit)) inx = false;
                    }
                    if (inx) ls.Add(Color.FromArgb(255, Convert.ToInt32(c.Split(' ')[0]), Convert.ToInt32(c.Split(' ')[1]), Convert.ToInt32(c.Split(' ')[2])));
                }
            }

            ls = ls.Distinct().ToList();
            ls.Sort(colorComparer);
            return ls;
        }

        /// <summary>
        /// Get all colors inside current WinPaletter theme
        /// </summary>
        /// <returns></returns>
        public List<Color> Palette
        {
            get
            {
                List<Color> CL = new();
                CL.Clear();

                foreach (FieldInfo field in typeof(Windows10x).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (field.FieldType.Name.ToLower() == "color")
                    {
                        CL.Add((Color)field.GetValue(Windows11));
                        CL.Add((Color)field.GetValue(Windows10));
                    }
                }

                foreach (FieldInfo field in typeof(LogonUI10x).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (field.FieldType.Name.ToLower() == "color")
                    {
                        CL.Add((Color)field.GetValue(LogonUI10x));
                    }
                }

                foreach (FieldInfo field in typeof(Windows8x).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (field.FieldType.Name.ToLower() == "color")
                    {
                        CL.Add((Color)field.GetValue(Windows81));
                    }
                }

                foreach (FieldInfo field in typeof(Windows7).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (field.FieldType.Name.ToLower() == "color")
                    {
                        CL.Add((Color)field.GetValue(Windows7));
                    }
                }

                foreach (FieldInfo field in typeof(WindowsVista).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (field.FieldType.Name.ToLower() == "color")
                    {
                        CL.Add((Color)field.GetValue(WindowsVista));
                    }
                }

                foreach (FieldInfo field in typeof(WindowsXP).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (field.FieldType.Name.ToLower() == "color")
                    {
                        CL.Add((Color)field.GetValue(WindowsXP));
                    }
                }

                foreach (FieldInfo field in typeof(Theme.Structures.LogonUI7).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (field.FieldType.Name.ToLower() == "color")
                    {
                        CL.Add((Color)field.GetValue(LogonUI7));
                    }
                }

                foreach (FieldInfo field in typeof(Theme.Structures.LogonUIXP).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (field.FieldType.Name.ToLower() == "color")
                    {
                        CL.Add((Color)field.GetValue(LogonUIXP));
                    }
                }

                foreach (FieldInfo field in typeof(Theme.Structures.Win32UI).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (field.FieldType.Name.ToLower() == "color")
                    {
                        CL.Add((Color)field.GetValue(Win32));
                    }
                }

                foreach (FieldInfo field in typeof(WallpaperTone).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
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

                foreach (FieldInfo field in typeof(Theme.Structures.Console).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (field.FieldType.Name.ToLower() == "color")
                    {
                        CL.Add((Color)field.GetValue(CommandPrompt));
                        CL.Add((Color)field.GetValue(PowerShellx86));
                        CL.Add((Color)field.GetValue(PowerShellx64));
                    }
                }

                foreach (WinTerminal.Types.Scheme c in Terminal.Schemes)
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

                foreach (WinTerminal.Types.Scheme c in TerminalPreview.Schemes)
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

                foreach (WinTerminal.Types.Theme c in Terminal.Themes)
                {
                    CL.Add(c.Tab.Background);
                    CL.Add(c.Tab.UnfocusedBackground);
                    CL.Add(c.TabRow.Background);
                    CL.Add(c.TabRow.UnfocusedBackground);
                }

                foreach (WinTerminal.Types.Theme c in TerminalPreview.Themes)
                {
                    CL.Add(c.Tab.Background);
                    CL.Add(c.Tab.UnfocusedBackground);
                    CL.Add(c.TabRow.Background);
                    CL.Add(c.TabRow.UnfocusedBackground);
                }

                foreach (WinTerminal.Types.Profile c in Terminal.Profiles.List)
                    CL.Add(c.TabColor);

                foreach (WinTerminal.Types.Profile c in TerminalPreview.Profiles.List)
                    CL.Add(c.TabColor);

                CL.Add(Terminal.Profiles.Defaults.TabColor);
                CL.Add(TerminalPreview.Profiles.Defaults.TabColor);

                foreach (FieldInfo field in typeof(Theme.Structures.Cursor).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    if (field.FieldType.Name.ToLower() == "color")
                    {
                        CL.Add((Color)field.GetValue(Cursors.Cursor_Arrow));
                        CL.Add((Color)field.GetValue(Cursors.Cursor_Help));
                        CL.Add((Color)field.GetValue(Cursors.Cursor_AppLoading));
                        CL.Add((Color)field.GetValue(Cursors.Cursor_Busy));
                        CL.Add((Color)field.GetValue(Cursors.Cursor_Pen));
                        CL.Add((Color)field.GetValue(Cursors.Cursor_None));
                        CL.Add((Color)field.GetValue(Cursors.Cursor_Move));
                        CL.Add((Color)field.GetValue(Cursors.Cursor_Up));
                        CL.Add((Color)field.GetValue(Cursors.Cursor_NS));
                        CL.Add((Color)field.GetValue(Cursors.Cursor_EW));
                        CL.Add((Color)field.GetValue(Cursors.Cursor_NESW));
                        CL.Add((Color)field.GetValue(Cursors.Cursor_NWSE));
                        CL.Add((Color)field.GetValue(Cursors.Cursor_Link));
                        CL.Add((Color)field.GetValue(Cursors.Cursor_Pin));
                        CL.Add((Color)field.GetValue(Cursors.Cursor_Person));
                        CL.Add((Color)field.GetValue(Cursors.Cursor_IBeam));
                        CL.Add((Color)field.GetValue(Cursors.Cursor_Cross));
                    }
                }

                CL = CL.Distinct().ToList();

                CL.Sort(colorComparer);

                if (CL.Contains(Color.FromArgb(0, 0, 0, 0)))
                {
                    while (CL.Contains(Color.FromArgb(0, 0, 0, 0)))
                        CL.Remove(Color.FromArgb(0, 0, 0, 0));
                }

                return CL;
            }
        }

        /// <summary>
        /// Decompress a WinPaletter theme file
        /// </summary>
        private static IEnumerable<string> Decompress(string File)
        {
            IEnumerable<string> DecompressedData;

            try
            {
                DecompressedData = System.IO.File.ReadAllText(File).Decompress().Split('\n').ToList();
            }
            catch
            {
                DecompressedData = System.IO.File.ReadAllText(File).Split('\n').ToList();
            }

            return DecompressedData;
        }

        /// <summary>
        /// Checks if this type is a structure or not
        /// </summary>
        private bool IsStructure(Type type)
        {
            return type.IsValueType && !type.IsPrimitive && type.Namespace is not null && !type.Namespace.StartsWith("System.");
        }
    }
}