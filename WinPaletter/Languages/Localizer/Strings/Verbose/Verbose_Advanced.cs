namespace WinPaletter
{
    public partial class Localizer
    {
        public string Verbose_RegAdd { get; set; } = "{0} > {1} = {2}, RegistryValueKind = {3}";
        public string Verbose_RegSkipped { get; set; } = "Skipped: {0}";
        public string Verbose_RegDelete { get; set; } = "Deleting: {0}";
        public string Verbose_DeleteTask { get; set; } = "Deleting task {0} from Task Scheduler";
        public string Verbose_PE_GettingAccess { get; set; } = "Trying to get authorized access to change '{0}' access/permissions";
        public string Verbose_PE_GetAccessToChangeResources { get; set; } = "Trying to get authorized access to change '{0}' resources";
        public string Verbose_PE_CreateBackup { get; set; } = "Creating '{0}' backup";
        public string Verbose_PE_GetBackupPermissions { get; set; } = "Trying to get '{0}' permissions backup";
        public string Verbose_PE_PatchingPE { get; set; } = "Patching '{0}' resources";
        public string Verbose_PE_RestoringPermissions { get; set; } = "Restoring '{0}' permissions from backup";
        public string Verbose_DeletingHighContrastThemes { get; set; } = "Deleting high contrast themes in '{0}'";
        public string Verbose_UxTheme_SSVS { get; set; } = "Setting visual styles: {0}.{1}({2}, {3}, {4}) returned {5}";
        public string Verbose_UxTheme_ET { get; set; } = "Enabling theming: {0}.{1}({2}) returned {3}";
        public string Verbose_User32_SPI { get; set; } = "Calling {0}.{1}({2}, {3}, {4}, {5}) returned {6}";
        public string Verbose_User32_SSC { get; set; } = "Calling {0}.{1}({2}, {3}) returned {4}";
        public string Verbose_User32_SMT { get; set; } = "Broadcasting all effects made to registry: {0}.{1}({2}, {3}, {4}, {5}, {6}, {7}, {8})";
        public string Verbose_SettingSlideshow { get; set; } = "Setting desktop slideshow data by modifying '{0}'";
        public string Verbose_SettingHSLImage { get; set; } = "Modifying HSL filters of selected image '{0}'";
        public string Verbose_EnableExplorerBar { get; set; } = "Enabling Explorer bar by renaming UIRibbon.dll into UIRibbon.dll_bak";
        public string Verbose_RestoreExplorerBar { get; set; } = "Restoring Explorer ribbon/bar by renaming UIRibbon.dll_bak into UIRibbon.dll";
        public string Verbose_GetInstanceLogonUIImg { get; set; } = "Getting an instance of LogonUI image into memory";
        public string Verbose_GetInstanceLockScreenImg { get; set; } = "Getting an instance of lock screen image into memory";
        public string Verbose_GrayscaleLogonUIImg { get; set; } = "Doing grayscale effect on LogonUI image";
        public string Verbose_GrayscaleLockScreenImg { get; set; } = "Doing grayscale effect on lock screen image";
        public string Verbose_BlurringLogonUIImg { get; set; } = "Blurring LogonUI image";
        public string Verbose_BlurringLockScreenImg { get; set; } = "Blurring lock screen image";
        public string Verbose_NoiseLogonUIImg { get; set; } = "Doing noise effect on LogonUI image";
        public string Verbose_NoiseLockScreenImg { get; set; } = "Doing noise effect on lock screen image";
        public string Verbose_LogonUIImgSaved { get; set; } = "Modified LogonUI image is saved into '{0}'";
        public string Verbose_LogonUIImgNUMSaved { get; set; } = "Modified LogonUI image number {1} is saved into '{0}'";
        public string Verbose_LockScreenImgSaved { get; set; } = "Modified lock screen image is saved into '{0}'";
        public string Verbose_RenderingCursor { get; set; } = "Rendering cursor '{0}'";
        public string Verbose_CursorRenderedInto { get; set; } = "Cursor is rendered into '{0}";
        public string Verbose_DelCursorWPFromReg { get; set; } = "Deleting registry value 'WinPaletter' from '{0}' to start cursors restoration";
    }
}
