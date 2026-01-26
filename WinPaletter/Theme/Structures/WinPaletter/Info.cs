using Microsoft.Win32;
using Serilog.Events;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for handling information about WinPaletter theme
    /// </summary>
    public class Info : ManagerBase<Info>
    {
        /// <summary>WinPaletter version that designed this theme</summary>
        public string AppVersion { get; set; } = Program.Version;

        /// <summary>Name of current WinPaletter theme</summary>
        public string ThemeName { get; set; } = Program.Localization.Strings.General.MyTheme;

        /// <summary>Description_SysEventsSounds of current WinPaletter theme. It can include tags that are useful for search in WinPaletter Store.</summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>Make saving this theme export theme resources pack that has sounds and images used in theme and are not located in system directories.</summary>
        public bool ExportResThemePack { get; set; } = false;

        /// <summary>License/credits of included sounds and images. Keep it empty if there are no license or credits or didn't use files.</summary>
        public string License { get; set; } = string.Empty;

        /// <summary>WinPaletter theme version</summary>
        public string ThemeVersion { get; set; } = "1.0.0.0";

        /// <summary>Person's name that designed this theme</summary>
        public string Author { get; set; } = User.Name;

        /// <summary>Person's social media link that designed this theme</summary>
        public string AuthorSocialMediaLink { get; set; } = string.Empty;

        /// <summary>Descriptive color 1. It should give the user a figure about your theme.</summary>
        public Color Color1 { get; set; } = Color.FromArgb(0, 102, 204);

        /// <summary>Descriptive color 2. It should give the user a figure about your theme.</summary>
        public Color Color2 { get; set; } = Color.FromArgb(122, 9, 9);

        /// <summary>Decorative pattern for your theme displayed in WinPaletter Store.</summary>
        public int Pattern { get; set; } = 1;

        /// <summary>
        /// This theme is designed especially for Windows 12
        /// <br>- This does not inhibit using the theme in other Windows editions, but the theme might not be applied correctly.</br>
        /// </summary>
        public bool DesignedFor_Win12 { get; set; } = true;

        /// <summary>
        /// This theme is designed especially for Windows 11
        /// <br>- This does not inhibit using the theme in other Windows editions, but the theme might not be applied correctly.</br>
        /// </summary>
        public bool DesignedFor_Win11 { get; set; } = true;

        /// <summary>
        /// This theme is designed especially for Windows 10
        /// <br>- This does not inhibit using the theme in other Windows editions, but the theme might not be applied correctly.</br>
        /// </summary>
        public bool DesignedFor_Win10 { get; set; } = true;

        /// <summary>
        /// This theme is designed especially for Windows 8.1
        /// <br>- This does not inhibit using the theme in other Windows editions, but the theme might not be applied correctly.</br>
        /// </summary>
        public bool DesignedFor_Win81 { get; set; } = true;

        /// <summary>
        /// This theme is designed especially for Windows 8.1
        /// <br>- This does not inhibit using the theme in other Windows editions, but the theme might not be applied correctly.</br>
        /// </summary>
        public bool DesignedFor_Win8 { get; set; } = true;

        /// <summary>
        /// This theme is designed especially for Windows 7
        /// <br>- This does not inhibit using the theme in other Windows editions, but the theme might not be applied correctly.</br>
        /// </summary>
        public bool DesignedFor_Win7 { get; set; } = true;

        /// <summary>
        /// This theme is designed especially for Windows Vista
        /// <br>- This does not inhibit using the theme in other Windows editions, but the theme might not be applied correctly.</br>
        /// </summary>
        public bool DesignedFor_WinVista { get; set; } = true;

        /// <summary>
        /// This theme is designed especially for Windows XP
        /// <br>- This does not inhibit using the theme in other Windows editions, but the theme might not be applied correctly.</br>
        /// </summary>
        public bool DesignedFor_WinXP { get; set; } = true;

        /// <summary>
        /// Creates a new instance of Info structure
        /// </summary>
        public Info() { }

        /// <summary>
        /// Loads theme info from registry
        /// </summary>
        public void Load()
        {
            Program.Log?.Write(LogEventLevel.Information, "Loading WinPaletter theme information from registry.");

            ThemeName = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ThemeName", Program.Localization.Strings.General.MyTheme);
            ThemeVersion = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ThemeVersion", "1.0");
            Author = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "Author", User.Name);
            AuthorSocialMediaLink = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "AuthorSocialMediaLink", string.Empty);
            AppVersion = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "AppVersion", Program.Version);
            Description = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "Description", string.Empty);
            ExportResThemePack = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ExportResThemePack", false);
            License = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "License", string.Empty);
            Color1 = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Color1", Default.FromCurrentOS.Info.Color1);
            Color2 = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Color2", Default.FromCurrentOS.Info.Color2);

            Pattern = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Pattern", 1);
            DesignedFor_Win12 = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win12", true);
            DesignedFor_Win11 = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win11", true);
            DesignedFor_Win10 = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win10", true);
            DesignedFor_Win81 = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win8.1", true);
            DesignedFor_Win8 = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win8", true);
            DesignedFor_Win7 = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win7", true);
            DesignedFor_WinVista = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_WinVista", true);
            DesignedFor_WinXP = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_WinXP", true);
        }

        /// <summary>
        /// Saves theme info into registry
        /// </summary>
        /// <param name="treeView">TreeView used as theme log</param>
        public void Apply(TreeView treeView = null)
        {
            Program.Log?.Write(LogEventLevel.Information, "Saving WinPaletter theme information into registry.");

            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ThemeName", ThemeName, RegistryValueKind.String);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ThemeVersion", ThemeVersion, RegistryValueKind.String);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "Author", Author, RegistryValueKind.String);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "AuthorSocialMediaLink", AuthorSocialMediaLink, RegistryValueKind.String);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "AppVersion", Program.Version, RegistryValueKind.String);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "Description", Description, RegistryValueKind.String);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ExportResThemePack", ExportResThemePack, RegistryValueKind.DWord);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "License", License, RegistryValueKind.String);

            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Color1", Color1.ToArgb(), RegistryValueKind.DWord);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Color2", Color2.ToArgb(), RegistryValueKind.DWord);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Pattern", Pattern, RegistryValueKind.DWord);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win12", DesignedFor_Win12 ? 1 : 0, RegistryValueKind.DWord);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win11", DesignedFor_Win11 ? 1 : 0, RegistryValueKind.DWord);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win10", DesignedFor_Win10 ? 1 : 0, RegistryValueKind.DWord);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win8.1", DesignedFor_Win81 ? 1 : 0, RegistryValueKind.DWord);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win8", DesignedFor_Win8 ? 1 : 0, RegistryValueKind.DWord);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win7", DesignedFor_Win7 ? 1 : 0, RegistryValueKind.DWord);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_WinVista", DesignedFor_WinVista ? 1 : 0, RegistryValueKind.DWord);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_WinXP", DesignedFor_WinXP ? 1 : 0, RegistryValueKind.DWord);
        }
    }
}
