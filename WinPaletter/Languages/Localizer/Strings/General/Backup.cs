using System;
using System.Linq;

namespace WinPaletter
{
    public partial class Localizer
    {
        public string Backup_RestoreQuestion { get; set; } = "Are you sure you want to restore this backup?";
        public string Backup_DeleteQuestion { get; set; } = "Are you sure you want to delete this backup?";
        public string Backup_Group_AppOpen { get; set; } = "Backed up upon opening WinPaletter";
        public string Backup_Group_ThemeApply { get; set; } = "Backed up before applying the theme";
        public string Backup_Group_ThemeOpen { get; set; } = "Backed up upon opening a WinPaletter theme";
        public string Backup_DeleteAllQuestion { get; set; } = "Are you sure you want to delete all backups?";
        public string Backup_ThemeName { get; set; } = "Theme name";
        public string Backup_FilePath { get; set; } = "File path";
        public string Backup_CreationDateTime { get; set; } = "Creation date\\time";
        public string Backup_NO { get; set; } = "backup(s)";

    }
}
