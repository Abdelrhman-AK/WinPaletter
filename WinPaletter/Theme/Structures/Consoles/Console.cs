using Microsoft.Win32;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Windows console (Command Prompt, PowerShell x86, PowerShell x64 or other consoles)
    /// </summary>
    public class Console : ICloneable
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled = false;

        /// <summary>Color table 0</summary>
        public Color ColorTable00 = Color.FromArgb(12, 12, 12);

        /// <summary>Color table 1</summary>
        public Color ColorTable01 = Color.FromArgb(0, 55, 218);

        /// <summary>Color table 2</summary>
        public Color ColorTable02 = Color.FromArgb(19, 161, 14);

        /// <summary>Color table 3</summary>
        public Color ColorTable03 = Color.FromArgb(58, 150, 221);

        /// <summary>Color table 4</summary>
        public Color ColorTable04 = Color.FromArgb(197, 15, 31);

        /// <summary>Color table 5</summary>
        public Color ColorTable05 = Color.FromArgb(136, 23, 152);

        /// <summary>Color table 6</summary>
        public Color ColorTable06 = Color.FromArgb(193, 156, 0);

        /// <summary>Color table 7</summary>
        public Color ColorTable07 = Color.FromArgb(204, 204, 204);

        /// <summary>Color table 8</summary>
        public Color ColorTable08 = Color.FromArgb(118, 118, 118);

        /// <summary>Color table 9</summary>
        public Color ColorTable09 = Color.FromArgb(59, 120, 255);

        /// <summary>Color table A</summary>
        public Color ColorTable10 = Color.FromArgb(22, 198, 12);

        /// <summary>Color table B</summary>
        public Color ColorTable11 = Color.FromArgb(97, 214, 214);

        /// <summary>Color table C</summary>
        public Color ColorTable12 = Color.FromArgb(231, 72, 86);

        /// <summary>Color table D</summary>
        public Color ColorTable13 = Color.FromArgb(180, 0, 158);

        /// <summary>Color table E</summary>
        public Color ColorTable14 = Color.FromArgb(249, 241, 165);

        /// <summary>Color table F</summary>
        public Color ColorTable15 = Color.FromArgb(242, 242, 242);

        /// <summary>Selected color table number as a popup foreground</summary>
        public int PopupForeground = 5;

        /// <summary>Selected color table number as a popup background</summary>
        public int PopupBackground = 15;

        /// <summary>Selected color table number as a screen foreground</summary>
        public int ScreenColorsForeground = 7;

        /// <summary>Selected color table number as a screen foreground</summary>
        public int ScreenColorsBackground = 0;

        /// <summary>Console carret size</summary>
        public int CursorSize = 19;

        /// <summary>Console font name</summary>
        public string FaceName = "Consolas";

        /// <summary>Use raster (pixelated/retro) font</summary>
        public bool FontRaster = false;

        /// <summary>Console font height</summary>
        public int PixelHeight = 16;

        /// <summary>Console font width</summary>
        public int PixelWidth = 0;

        /// <summary>Console font weight</summary>
        public int FontWeight = 400;

        /// <summary>Cursor type<br><b>- For Windows 10 1909 and higher</b></br></summary>
        public int W10_1909_CursorType = 0;

        /// <summary>Cursor color<br><b>- For Windows 10 1909 and higher</b></br></summary>
        public Color W10_1909_CursorColor = Color.White;

        /// <summary>Use enhanced terminal<br><b>- For Windows 10 1909 and higher</b></br></summary>
        public bool W10_1909_ForceV2 = true;

        /// <summary>Use line selection<br><b>- For Windows 10 1909 and higher</b></br></summary>
        public bool W10_1909_LineSelection = false;

        /// <summary>Use terminal scrolling<br><b>- For Windows 10 1909 and higher</b></br></summary>
        public bool W10_1909_TerminalScrolling = false;

        /// <summary>Console window opacity<br><b>- For Windows 10 1909 and higher</b></br></summary>
        public int W10_1909_WindowAlpha = 255;

        /// <summary>
        /// Create console structure with default data
        /// </summary>
        public Console() { }

        /// <summary>
        /// Load console structure data from registry
        /// </summary>
        /// <param name="RegKey">Registry key name under HKEY_CURRENT_USER\Console</param>
        /// <param name="Signature_Of_Enable">Name of console (for example: Terminal_CMD_Enabled). Used for getting Enabled property</param>
        /// <param name="default">Console structure that has default data</param>
        public void Load(string RegKey, string Signature_Of_Enable, Console @default)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Loading console settings from registry.");

            Enabled = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", Signature_Of_Enable, 0)) == 1;

            string RegAddress = $@"HKEY_CURRENT_USER\Console{(string.IsNullOrEmpty(RegKey) ? string.Empty : $@"\{RegKey}")}";

            // Load color table (Windows stores colors in reverse byte order)
            ColorTable00 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(GetReg(RegAddress, "ColorTable00", @default.ColorTable00.Reverse().ToArgb()))).Reverse());
            ColorTable01 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(GetReg(RegAddress, "ColorTable01", @default.ColorTable01.Reverse().ToArgb()))).Reverse());
            ColorTable02 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(GetReg(RegAddress, "ColorTable02", @default.ColorTable02.Reverse().ToArgb()))).Reverse());
            ColorTable03 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(GetReg(RegAddress, "ColorTable03", @default.ColorTable03.Reverse().ToArgb()))).Reverse());
            ColorTable04 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(GetReg(RegAddress, "ColorTable04", @default.ColorTable04.Reverse().ToArgb()))).Reverse());
            ColorTable05 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(GetReg(RegAddress, "ColorTable05", @default.ColorTable05.Reverse().ToArgb()))).Reverse());
            ColorTable06 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(GetReg(RegAddress, "ColorTable06", @default.ColorTable06.Reverse().ToArgb()))).Reverse());
            ColorTable07 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(GetReg(RegAddress, "ColorTable07", @default.ColorTable07.Reverse().ToArgb()))).Reverse());
            ColorTable08 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(GetReg(RegAddress, "ColorTable08", @default.ColorTable08.Reverse().ToArgb()))).Reverse());
            ColorTable09 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(GetReg(RegAddress, "ColorTable09", @default.ColorTable09.Reverse().ToArgb()))).Reverse());
            ColorTable10 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(GetReg(RegAddress, "ColorTable10", @default.ColorTable10.Reverse().ToArgb()))).Reverse());
            ColorTable11 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(GetReg(RegAddress, "ColorTable11", @default.ColorTable11.Reverse().ToArgb()))).Reverse());
            ColorTable12 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(GetReg(RegAddress, "ColorTable12", @default.ColorTable12.Reverse().ToArgb()))).Reverse());
            ColorTable13 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(GetReg(RegAddress, "ColorTable13", @default.ColorTable13.Reverse().ToArgb()))).Reverse());
            ColorTable14 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(GetReg(RegAddress, "ColorTable14", @default.ColorTable14.Reverse().ToArgb()))).Reverse());
            ColorTable15 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(GetReg(RegAddress, "ColorTable15", @default.ColorTable15.Reverse().ToArgb()))).Reverse());

            // Popup colors (stored as single hex value "background + foreground")
            string popupHex = ((int)GetReg(RegAddress, "PopupColors",Convert.ToInt32($"{@default.PopupBackground:X}{@default.PopupForeground:X}", 16))).ToString("X").PadLeft(2, '0');
            PopupBackground = Convert.ToInt32(popupHex[0].ToString(), 16);
            PopupForeground = Convert.ToInt32(popupHex[1].ToString(), 16);

            // Screen colors (same storage format as popup)
            string screenHex = ((int)GetReg(RegAddress, "ScreenColors",Convert.ToInt32($"{@default.ScreenColorsBackground:X}{@default.ScreenColorsForeground:X}", 16))).ToString("X").PadLeft(2, '0');
            ScreenColorsBackground = Convert.ToInt32(screenHex[0].ToString(), 16);
            ScreenColorsForeground = Convert.ToInt32(screenHex[1].ToString(), 16);

            // Cursor size
            CursorSize = Convert.ToInt32(GetReg(RegAddress, "CursorSize", 25));

            // Font name (must exist in Fonts list, or fallback to default)
            string faceName = GetReg(RegAddress, "FaceName", @default.FaceName).ToString();
            if (Fonts.Exists(faceName) || faceName.Equals("__DefaultTTFont__", StringComparison.OrdinalIgnoreCase)) FaceName = faceName;
            else FaceName = @default.FaceName;

            // Font family (check raster vs TrueType)
            int fontFamily = (int)GetReg(RegAddress, "FontFamily", !@default.FontRaster ? 54 : 1);
            const int TMPF_TRUETYPE = 0x04;
            FontRaster = (fontFamily & TMPF_TRUETYPE) == 0;

            // Special handling for "__DefaultTTFont__" and "Terminal"
            if (FaceName.Equals("__DefaultTTFont__", StringComparison.OrdinalIgnoreCase))
            {
                FaceName = "Consolas";  // "__DefaultTTFont__" should map to a real TTF font
                FontRaster = false;
            }
            else if (FaceName.Equals("Terminal", StringComparison.OrdinalIgnoreCase))
            {
                FontRaster = true;
            }

            // Font size (High word = height, Low word = width)
            int fontSize = (int)GetReg(RegAddress, "FontSize", (@default.PixelHeight << 16));
            if (fontSize == 0 && !FontRaster)
            {
                PixelHeight = @default.PixelHeight;
                PixelWidth = @default.PixelWidth;
            }
            else
            {
                PixelHeight = (fontSize >> 16) & 0xFFFF;
                PixelWidth = fontSize & 0xFFFF;
            }

            // Font weight
            FontWeight = Convert.ToInt32(GetReg(RegAddress, "FontWeight", 400));

            // Cursor color
            W10_1909_CursorColor = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(GetReg(RegAddress, "CursorColor", Color.White.Reverse().ToArgb()))).Reverse());

            // Misc Windows 10+ settings
            W10_1909_CursorType = Convert.ToInt32(GetReg(RegAddress, "CursorType", 1));
            W10_1909_ForceV2 = Convert.ToBoolean(GetReg(RegAddress, "ForceV2", true));
            W10_1909_LineSelection = Convert.ToBoolean(GetReg(RegAddress, "LineSelection", false));
            W10_1909_TerminalScrolling = Convert.ToBoolean(GetReg(RegAddress, "TerminalScrolling", false));
            W10_1909_WindowAlpha = Convert.ToInt32(GetReg(RegAddress, "WindowAlpha", 255));
        }

        /// <summary>
        /// ApplyToTM console structure data into registry
        /// </summary>
        /// <param name="scopeReg">It can be HKEY_CURRENT_USER or HKEY_USERS\...</param>
        /// <param name="RegKey">Registry key name under HKEY_CURRENT_USER\Console or HKEY_USERS\...\Console</param>
        /// <param name="Console">Console structure to be saved into registry</param>
        /// <param name="treeView">TreeView used as a theme log</param>
        /// <param name="Signature_Of_Enable">Name of console (for example: Terminal_CMD_Enabled). Used for getting Enabled property</param>
        public static void Save_Console_To_Registry(string scopeReg, string RegKey, string Signature_Of_Enable, Console Console, TreeView treeView = null)
        {
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", Signature_Of_Enable, Console.Enabled);

            if (Console.Enabled)
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, $"Saving console settings into registry.");

                string RegAddress = $@"{scopeReg}\Console{(string.IsNullOrEmpty(RegKey) ? string.Empty : $@"\{RegKey}")}";

                try
                {
                    if (scopeReg.ToUpper() == "HKEY_CURRENT_USER")
                    {
                        if (!string.IsNullOrEmpty(RegKey)) Registry.CurrentUser.CreateSubKey($@"Console\{RegKey}", true).Close();
                    }
                }
                catch { } // Ignore creating a registry key

                EditReg(treeView, RegAddress, "EnableColorSelection", 1);

                // Windows stores color values in reverse order, so we need to reverse them
                EditReg(treeView, RegAddress, "ColorTable00", Color.FromArgb(0, Console.ColorTable00.Reverse()).ToArgb());
                EditReg(treeView, RegAddress, "ColorTable01", Color.FromArgb(0, Console.ColorTable01.Reverse()).ToArgb());
                EditReg(treeView, RegAddress, "ColorTable02", Color.FromArgb(0, Console.ColorTable02.Reverse()).ToArgb());
                EditReg(treeView, RegAddress, "ColorTable03", Color.FromArgb(0, Console.ColorTable03.Reverse()).ToArgb());
                EditReg(treeView, RegAddress, "ColorTable04", Color.FromArgb(0, Console.ColorTable04.Reverse()).ToArgb());
                EditReg(treeView, RegAddress, "ColorTable05", Color.FromArgb(0, Console.ColorTable05.Reverse()).ToArgb());
                EditReg(treeView, RegAddress, "ColorTable06", Color.FromArgb(0, Console.ColorTable06.Reverse()).ToArgb());
                EditReg(treeView, RegAddress, "ColorTable07", Color.FromArgb(0, Console.ColorTable07.Reverse()).ToArgb());
                EditReg(treeView, RegAddress, "ColorTable08", Color.FromArgb(0, Console.ColorTable08.Reverse()).ToArgb());
                EditReg(treeView, RegAddress, "ColorTable09", Color.FromArgb(0, Console.ColorTable09.Reverse()).ToArgb());
                EditReg(treeView, RegAddress, "ColorTable10", Color.FromArgb(0, Console.ColorTable10.Reverse()).ToArgb());
                EditReg(treeView, RegAddress, "ColorTable11", Color.FromArgb(0, Console.ColorTable11.Reverse()).ToArgb());
                EditReg(treeView, RegAddress, "ColorTable12", Color.FromArgb(0, Console.ColorTable12.Reverse()).ToArgb());
                EditReg(treeView, RegAddress, "ColorTable13", Color.FromArgb(0, Console.ColorTable13.Reverse()).ToArgb());
                EditReg(treeView, RegAddress, "ColorTable14", Color.FromArgb(0, Console.ColorTable14.Reverse()).ToArgb());
                EditReg(treeView, RegAddress, "ColorTable15", Color.FromArgb(0, Console.ColorTable15.Reverse()).ToArgb());
                EditReg(treeView, RegAddress, "PopupColors", Convert.ToInt32($"{Console.PopupBackground:X}{Console.PopupForeground:X}", 16));
                EditReg(treeView, RegAddress, "ScreenColors", Convert.ToInt32($"{Console.ScreenColorsBackground:X}{Console.ScreenColorsForeground:X}", 16));
                EditReg(treeView, RegAddress, "CursorSize", Console.CursorSize);

                if (Console.FontRaster)
                {
                    EditReg(treeView, RegAddress, "FaceName", "Terminal", RegistryValueKind.String);
                    EditReg(treeView, RegAddress, "FontFamily", 48);
                }
                else
                {
                    EditReg(treeView, RegAddress, "FaceName", Console.FaceName, RegistryValueKind.String);
                    EditReg(treeView, RegAddress, "FontFamily", 54);
                }

                EditReg(treeView, RegAddress, "FontSize", (Console.PixelHeight << 16) | (Console.PixelWidth & 0xFFFF));
                EditReg(treeView, RegAddress, "FontWeight", Console.FontWeight);

                if (OS.W10_1909)
                {
                    EditReg(treeView, RegAddress, "CursorColor", Color.FromArgb(0, Console.W10_1909_CursorColor.Reverse()).ToArgb());
                    EditReg(treeView, RegAddress, "CursorType", Console.W10_1909_CursorType);
                    EditReg(treeView, RegAddress, "WindowAlpha", Console.W10_1909_WindowAlpha);
                    EditReg(treeView, RegAddress, "ForceV2", Console.W10_1909_ForceV2 ? 1 : 0);
                    EditReg(treeView, RegAddress, "LineSelection", Console.W10_1909_LineSelection ? 1 : 0);
                    EditReg(treeView, RegAddress, "TerminalScrolling", Console.W10_1909_TerminalScrolling ? 1 : 0);
                }

                EditReg(treeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Console\TrueTypeFont", "000", Console.FaceName, RegistryValueKind.String);
            }
        }

        /// <summary>
        /// Operator to check if two consoles are equal
        /// </summary>
        public static bool operator ==(Console First, Console Second)
        {
            return First.Equals(Second);
        }

        /// <summary>
        /// Operator to check if two consoles are not equal
        /// </summary>
        public static bool operator !=(Console First, Console Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>
        /// Clone console structure
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Checks if two consoles are equal or not
        /// </summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Gets console hash code
        /// </summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
