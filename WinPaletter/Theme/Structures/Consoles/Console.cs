using Microsoft.Win32;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
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
            if (Manager.IsFontInstalled(temp.ToString()))
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
}
