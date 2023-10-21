using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    public struct Info : ICloneable
    {
        public string AppVersion;
        public string ThemeName;
        public string Description;
        public bool ExportResThemePack;
        public string License;
        public string ThemeVersion;
        public string Author;
        public string AuthorSocialMediaLink;
        public Color Color1;
        public Color Color2;
        public int Pattern;
        public bool DesignedFor_Win11;
        public bool DesignedFor_Win10;
        public bool DesignedFor_Win81;
        public bool DesignedFor_Win7;
        public bool DesignedFor_WinVista;
        public bool DesignedFor_WinXP;

        public void Load()
        {
            ThemeName = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ThemeName", Program.Lang.CurrentMode).ToString();
            ThemeVersion = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ThemeVersion", "1.0").ToString();
            Author = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "Author", Environment.UserName).ToString();
            AuthorSocialMediaLink = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "AuthorSocialMediaLink", "").ToString();
            AppVersion = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "AppVersion", Program.Version).ToString();
            Description = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "Description", "").ToString();
            ExportResThemePack = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ExportResThemePack", false));
            License = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "License", "").ToString();
            var y = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Color1", Color.FromArgb(0, 102, 204).ToArgb());
            Color1 = Color.FromArgb(Convert.ToInt32(y));

            y = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Color2", Color.FromArgb(122, 9, 9).ToArgb());
            Color2 = Color.FromArgb(Convert.ToInt32(y));

            Pattern = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Pattern", 1));
            DesignedFor_Win11 = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win11", true));
            DesignedFor_Win10 = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win10", true));
            DesignedFor_Win81 = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win8.1", true));
            DesignedFor_Win7 = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win7", true));
            DesignedFor_WinVista = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_WinVista", true));
            DesignedFor_WinXP = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_WinXP", true));
        }
        public void Apply(TreeView TreeView = null)
        {
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ThemeName", ThemeName, RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ThemeVersion", ThemeVersion, RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "Author", Author, RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "AuthorSocialMediaLink", AuthorSocialMediaLink, RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "AppVersion", Program.Version, RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "Description", Description, RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "ExportResThemePack", ExportResThemePack, RegistryValueKind.DWord);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo", "License", License, RegistryValueKind.String);

            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Color1", Color1.ToArgb(), RegistryValueKind.DWord);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Color2", Color2.ToArgb(), RegistryValueKind.DWord);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "Pattern", Pattern, RegistryValueKind.DWord);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win11", DesignedFor_Win11.ToInteger(), RegistryValueKind.DWord);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win10", DesignedFor_Win10.ToInteger(), RegistryValueKind.DWord);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win8.1", DesignedFor_Win81.ToInteger(), RegistryValueKind.DWord);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_Win7", DesignedFor_Win7.ToInteger(), RegistryValueKind.DWord);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_WinVista", DesignedFor_WinVista.ToInteger(), RegistryValueKind.DWord);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\ThemeInfo\Store", "DesignedFor_WinXP", DesignedFor_WinXP.ToInteger(), RegistryValueKind.DWord);
        }

        public static bool operator ==(Info First, Info Second)
        {
            return First.Equals(Second);
        }

        public static bool operator !=(Info First, Info Second)
        {
            return !First.Equals(Second);
        }
        public object Clone()
        {
            return MemberwiseClone();
        }

    }

}
