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
    public struct Console : ICloneable
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled;

        /// <summary>Color table 0</summary>
        public Color ColorTable00;

        /// <summary>Color table 1</summary>
        public Color ColorTable01;

        /// <summary>Color table 2</summary>
        public Color ColorTable02;

        /// <summary>Color table 3</summary>
        public Color ColorTable03;

        /// <summary>Color table 4</summary>
        public Color ColorTable04;

        /// <summary>Color table 5</summary>
        public Color ColorTable05;

        /// <summary>Color table 6</summary>
        public Color ColorTable06;

        /// <summary>Color table 7</summary>
        public Color ColorTable07;

        /// <summary>Color table 8</summary>
        public Color ColorTable08;

        /// <summary>Color table 9</summary>
        public Color ColorTable09;

        /// <summary>Color table A</summary>
        public Color ColorTable10;

        /// <summary>Color table B</summary>
        public Color ColorTable11;

        /// <summary>Color table C</summary>
        public Color ColorTable12;

        /// <summary>Color table D</summary>
        public Color ColorTable13;

        /// <summary>Color table E</summary>
        public Color ColorTable14;

        /// <summary>Color table F</summary>
        public Color ColorTable15;

        /// <summary>Selected color table number as a popup foreground</summary>
        public int PopupForeground;

        /// <summary>Selected color table number as a popup background</summary>
        public int PopupBackground;

        /// <summary>Selected color table number as a screen foreground</summary>
        public int ScreenColorsForeground;

        /// <summary>Selected color table number as a screen foreground</summary>
        public int ScreenColorsBackground;

        /// <summary>Console carret size</summary>
        public int CursorSize;

        /// <summary>Console font name</summary>
        public string FaceName;

        /// <summary>Use raster (pixelated/retro) font</summary>
        public bool FontRaster;

        /// <summary>Console font size or raster console screen size</summary>
        public int FontSize;

        /// <summary>Console font weight</summary>
        public int FontWeight;

        /// <summary>Cursor type<br><b>- For Windows 10 1909 and higher</b></br></summary>
        public int W10_1909_CursorType;

        /// <summary>Cursor color<br><b>- For Windows 10 1909 and higher</b></br></summary>
        public Color W10_1909_CursorColor;

        /// <summary>Use enhanced terminal<br><b>- For Windows 10 1909 and higher</b></br></summary>
        public bool W10_1909_ForceV2;

        /// <summary>Use line selection<br><b>- For Windows 10 1909 and higher</b></br></summary>
        public bool W10_1909_LineSelection;

        /// <summary>Use terminal scrolling<br><b>- For Windows 10 1909 and higher</b></br></summary>
        public bool W10_1909_TerminalScrolling;

        /// <summary>Console window opacity<br><b>- For Windows 10 1909 and higher</b></br></summary>
        public int W10_1909_WindowAlpha;

        /// <summary>
        /// Load console structure data from registry
        /// </summary>
        /// <param name="RegKey">Registry key name under HKEY_CURRENT_USER\Console</param>
        /// <param name="Signature_Of_Enable">Name of console (for example: Terminal_CMD_Enabled). Used for getting Enabled property</param>
        /// <param name="default">Console structure that has default data</param>
        public void Load(string RegKey, string Signature_Of_Enable, Console @default)
        {
            object temp;
            string RegAddress = $@"HKEY_CURRENT_USER\Console{((string.IsNullOrEmpty(RegKey) ? string.Empty : $@"\{RegKey}"))}";

            temp = GetReg(RegAddress, "ColorTable00", @default.ColorTable00.Reverse().ToArgb());
            ColorTable00 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

            temp = GetReg(RegAddress, "ColorTable01", @default.ColorTable01.Reverse().ToArgb());
            ColorTable01 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

            temp = GetReg(RegAddress, "ColorTable02", @default.ColorTable02.Reverse().ToArgb());
            ColorTable02 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

            temp = GetReg(RegAddress, "ColorTable03", @default.ColorTable03.Reverse().ToArgb());
            ColorTable03 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

            temp = GetReg(RegAddress, "ColorTable04", @default.ColorTable04.Reverse().ToArgb());
            ColorTable04 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

            temp = GetReg(RegAddress, "ColorTable05", @default.ColorTable05.Reverse().ToArgb());
            ColorTable05 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

            temp = GetReg(RegAddress, "ColorTable06", @default.ColorTable06.Reverse().ToArgb());
            ColorTable06 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

            temp = GetReg(RegAddress, "ColorTable07", @default.ColorTable07.Reverse().ToArgb());
            ColorTable07 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

            temp = GetReg(RegAddress, "ColorTable08", @default.ColorTable08.Reverse().ToArgb());
            ColorTable08 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

            temp = GetReg(RegAddress, "ColorTable09", @default.ColorTable09.Reverse().ToArgb());
            ColorTable09 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

            temp = GetReg(RegAddress, "ColorTable10", @default.ColorTable10.Reverse().ToArgb());
            ColorTable10 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

            temp = GetReg(RegAddress, "ColorTable11", @default.ColorTable11.Reverse().ToArgb());
            ColorTable11 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

            temp = GetReg(RegAddress, "ColorTable12", @default.ColorTable12.Reverse().ToArgb());
            ColorTable12 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

            temp = GetReg(RegAddress, "ColorTable13", @default.ColorTable13.Reverse().ToArgb());
            ColorTable13 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

            temp = GetReg(RegAddress, "ColorTable14", @default.ColorTable14.Reverse().ToArgb());
            ColorTable14 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

            temp = GetReg(RegAddress, "ColorTable15", @default.ColorTable15.Reverse().ToArgb());
            ColorTable15 = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());

            temp = GetReg(RegAddress, "PopupColors", Convert.ToInt32($"{@default.PopupBackground:X}{@default.PopupForeground:X}", 16));
            string d = ((int)temp).ToString("X");
            if (d.Count() == 1)
                d = $"{0}{d}";
            PopupBackground = Convert.ToInt32(d[0].ToString(), 16);
            PopupForeground = Convert.ToInt32(d[1].ToString(), 16);

            temp = GetReg(RegAddress, "ScreenColors", Convert.ToInt32($"{@default.ScreenColorsBackground:X}{@default.ScreenColorsForeground:X}", 16));
            d = ((int)temp).ToString("X");
            if (d.Count() == 1)
                d = $"{0}{d}";
            ScreenColorsBackground = Convert.ToInt32(d[0].ToString(), 16);
            ScreenColorsForeground = Convert.ToInt32(d[1].ToString(), 16);

            CursorSize = Convert.ToInt32(GetReg(RegAddress, "CursorSize", 25));

            temp = GetReg(RegAddress, "FaceName", @default.FaceName);
            if (Manager.IsFontInstalled(temp.ToString()))
            {
                FaceName = temp.ToString();
            }
            else
            {
                FaceName = @default.FaceName;
            }

            temp = GetReg(RegAddress, "FontFamily", !@default.FontRaster ? 54 : 1);
            FontRaster = ((int)temp == 1 | (int)temp == 0) | (int)temp == 48;
            if (FaceName.ToLower() == "terminal")
                FontRaster = true;

            temp = GetReg(RegAddress, "FontSize", @default.FontSize);
            if ((int)temp == 0 & !FontRaster)
                FontSize = @default.FontSize;
            else
                FontSize = Convert.ToInt32(temp);

            FontWeight = Convert.ToInt32(GetReg(RegAddress, "FontWeight", 400));


            if (OS.W10_1909)
            {
                temp = GetReg(RegAddress, "CursorColor", Color.White.Reverse().ToArgb());
                W10_1909_CursorColor = Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(temp)).Reverse());
                W10_1909_CursorType = Convert.ToInt32(GetReg(RegAddress, "CursorType", 1));
                W10_1909_ForceV2 = Convert.ToBoolean(GetReg(RegAddress, "ForceV2", true));
                W10_1909_LineSelection = Convert.ToBoolean(GetReg(RegAddress, "LineSelection", false));
                W10_1909_TerminalScrolling = Convert.ToBoolean(GetReg(RegAddress, "TerminalScrolling", false));
                W10_1909_WindowAlpha = Convert.ToInt32(GetReg(RegAddress, "WindowAlpha", 255));
            }

            Enabled = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", Signature_Of_Enable, 0)) == 1;

        }

        /// <summary>
        /// ApplyToTM console structure data into registry
        /// </summary>
        /// <param name="scopeReg">It can be HKEY_CURRENT_USER or HKEY_USERS\...</param>
        /// <param name="RegKey">Registry key name under HKEY_CURRENT_USER\Console or HKEY_USERS\...\Console</param>
        /// <param name="Console">Console structure to be saved into registry</param>
        /// <param name="TreeView">TreeView used as a theme log</param>
        public static void Save_Console_To_Registry(string scopeReg, string RegKey, Console Console, TreeView TreeView = null)
        {
            string RegAddress = $@"{scopeReg}\Console{((string.IsNullOrEmpty(RegKey) ? string.Empty : $@"\{RegKey}"))}";

            try
            {
                if (scopeReg.ToUpper() == "HKEY_CURRENT_USER")
                {
                    if (!string.IsNullOrEmpty(RegKey))
                        Registry.CurrentUser.CreateSubKey($@"Console\{RegKey}", true).Close();
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
            EditReg(TreeView, RegAddress, "PopupColors", Convert.ToInt32($"{Console.PopupBackground:X}{Console.PopupForeground:X}", 16));
            EditReg(TreeView, RegAddress, "ScreenColors", Convert.ToInt32($"{Console.ScreenColorsBackground:X}{Console.ScreenColorsForeground:X}", 16));
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

            if (OS.W10_1909)
            {
                EditReg(TreeView, RegAddress, "CursorColor", Color.FromArgb(0, Console.W10_1909_CursorColor.Reverse()).ToArgb());
                EditReg(TreeView, RegAddress, "CursorType", Console.W10_1909_CursorType);
                EditReg(TreeView, RegAddress, "WindowAlpha", Console.W10_1909_WindowAlpha);
                EditReg(TreeView, RegAddress, "ForceV2", Console.W10_1909_ForceV2 ? 1 : 0);
                EditReg(TreeView, RegAddress, "LineSelection", Console.W10_1909_LineSelection ? 1 : 0);
                EditReg(TreeView, RegAddress, "TerminalScrolling", Console.W10_1909_TerminalScrolling ? 1 : 0);
            }

            EditReg(TreeView, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Console\TrueTypeFont", "000", Console.FaceName, RegistryValueKind.String);

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
        public readonly object Clone()
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
