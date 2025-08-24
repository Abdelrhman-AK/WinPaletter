﻿using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for handling information about WinPaletter theme
    /// </summary>
    public class Info : ICloneable
    {
        /// <summary>WinPaletter version that designed this theme</summary>
        public string AppVersion = Program.Version;

        /// <summary>Name of current WinPaletter theme</summary>
        public string ThemeName = Program.Lang.Strings.General.MyTheme;

        /// <summary>Description_SysEventsSounds of current WinPaletter theme. It can include tags that are useful for search in WinPaletter Store.</summary>
        public string Description = string.Empty;

        /// <summary>Make saving this theme export theme resources pack that has sounds and images used in theme and are not located in system directories.</summary>
        public bool ExportResThemePack = false;

        /// <summary>License/credits of included sounds and images. Keep it empty if there are no license or credits or didn't use files.</summary>
        public string License = string.Empty;

        /// <summary>WinPaletter theme version</summary>
        public string ThemeVersion = "1.0.0.0";

        /// <summary>Person's name that designed this theme</summary>
        public string Author = User.Name;

        /// <summary>Person's social media link that designed this theme</summary>
        public string AuthorSocialMediaLink = string.Empty;

        /// <summary>Descriptive color 1. It should give the user a figure about your theme.</summary>
        public Color Color1 = Color.FromArgb(0, 102, 204);

        /// <summary>Descriptive color 2. It should give the user a figure about your theme.</summary>
        public Color Color2 = Color.FromArgb(122, 9, 9);

        /// <summary>Decorative pattern for your theme displayed in WinPaletter Store.</summary>
        public int Pattern = 1;

        /// <summary>
        /// This theme is designed especially for Windows 12
        /// <br>- This doesn't inhibit using the theme in other Windows editions, but the theme might not be applied correctly.</br>
        /// </summary>
        public bool DesignedFor_Win12 = true;

        /// <summary>
        /// This theme is designed especially for Windows 11
        /// <br>- This doesn't inhibit using the theme in other Windows editions, but the theme might not be applied correctly.</br>
        /// </summary>
        public bool DesignedFor_Win11 = true;

        /// <summary>
        /// This theme is designed especially for Windows 10
        /// <br>- This doesn't inhibit using the theme in other Windows editions, but the theme might not be applied correctly.</br>
        /// </summary>
        public bool DesignedFor_Win10 = true;

        /// <summary>
        /// This theme is designed especially for Windows 8.1
        /// <br>- This doesn't inhibit using the theme in other Windows editions, but the theme might not be applied correctly.</br>
        /// </summary>
        public bool DesignedFor_Win81 = true;

        /// <summary>
        /// This theme is designed especially for Windows 8.1
        /// <br>- This doesn't inhibit using the theme in other Windows editions, but the theme might not be applied correctly.</br>
        /// </summary>
        public bool DesignedFor_Win8 = true;

        /// <summary>
        /// This theme is designed especially for Windows 7
        /// <br>- This doesn't inhibit using the theme in other Windows editions, but the theme might not be applied correctly.</br>
        /// </summary>
        public bool DesignedFor_Win7 = true;

        /// <summary>
        /// This theme is designed especially for Windows Vista
        /// <br>- This doesn't inhibit using the theme in other Windows editions, but the theme might not be applied correctly.</br>
        /// </summary>
        public bool DesignedFor_WinVista = true;

        /// <summary>
        /// This theme is designed especially for Windows XP
        /// <br>- This doesn't inhibit using the theme in other Windows editions, but the theme might not be applied correctly.</br>
        /// </summary>
        public bool DesignedFor_WinXP = true;

        /// <summary>
        /// Creates a new instance of Info structure
        /// </summary>
        public Info() { }

        /// <summary>
        /// Loads theme info from registry
        /// </summary>
        public void Load()
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, "Loading WinPaletter theme information from registry.");

            ThemeName = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ThemeName", Program.Lang.Strings.General.MyTheme).ToString();
            ThemeVersion = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ThemeVersion", "1.0").ToString();
            Author = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "Author", User.Name).ToString();
            AuthorSocialMediaLink = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "AuthorSocialMediaLink", string.Empty).ToString();
            AppVersion = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "AppVersion", Program.Version).ToString();
            Description = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "Description", string.Empty).ToString();
            ExportResThemePack = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ExportResThemePack", false));
            License = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "License", string.Empty).ToString();
            object y = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Color1", Color.FromArgb(0, 102, 204).ToArgb());
            Color1 = Color.FromArgb(Convert.ToInt32(y));

            y = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Color2", Color.FromArgb(122, 9, 9).ToArgb());
            Color2 = Color.FromArgb(Convert.ToInt32(y));

            Pattern = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Pattern", 1));
            DesignedFor_Win12 = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win12", true));
            DesignedFor_Win11 = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win11", true));
            DesignedFor_Win10 = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win10", true));
            DesignedFor_Win81 = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win8.1", true));
            DesignedFor_Win8 = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win8", true));
            DesignedFor_Win7 = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win7", true));
            DesignedFor_WinVista = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_WinVista", true));
            DesignedFor_WinXP = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_WinXP", true));
        }

        /// <summary>
        /// Saves theme info into registry
        /// </summary>
        /// <param name="treeView">TreeView used as theme log</param>
        public void Apply(TreeView treeView = null)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, "Saving WinPaletter theme information into registry.");

            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ThemeName", ThemeName, RegistryValueKind.String);
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ThemeVersion", ThemeVersion, RegistryValueKind.String);
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "Author", Author, RegistryValueKind.String);
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "AuthorSocialMediaLink", AuthorSocialMediaLink, RegistryValueKind.String);
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "AppVersion", Program.Version, RegistryValueKind.String);
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "Description", Description, RegistryValueKind.String);
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ExportResThemePack", ExportResThemePack, RegistryValueKind.DWord);
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "License", License, RegistryValueKind.String);

            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Color1", Color1.ToArgb(), RegistryValueKind.DWord);
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Color2", Color2.ToArgb(), RegistryValueKind.DWord);
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Pattern", Pattern, RegistryValueKind.DWord);
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win12", DesignedFor_Win12 ? 1 : 0, RegistryValueKind.DWord);
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win11", DesignedFor_Win11 ? 1 : 0, RegistryValueKind.DWord);
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win10", DesignedFor_Win10 ? 1 : 0, RegistryValueKind.DWord);
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win8.1", DesignedFor_Win81 ? 1 : 0, RegistryValueKind.DWord);
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win8", DesignedFor_Win8 ? 1 : 0, RegistryValueKind.DWord);
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win7", DesignedFor_Win7 ? 1 : 0, RegistryValueKind.DWord);
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_WinVista", DesignedFor_WinVista ? 1 : 0, RegistryValueKind.DWord);
            EditReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_WinXP", DesignedFor_WinXP ? 1 : 0, RegistryValueKind.DWord);
        }

        /// <summary>Operator to check if two Info structures are equal</summary>
        public static bool operator ==(Info First, Info Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two Info structures are not equal</summary>
        public static bool operator !=(Info First, Info Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones Info structure</summary>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>Checks if two Info structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>Get hash code of Info structure</summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

}
