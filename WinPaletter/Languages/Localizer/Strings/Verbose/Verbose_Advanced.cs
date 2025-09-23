namespace WinPaletter
{
    public partial class Localizer
    {
        public partial class Strings_Cls
        {
            public partial class ThemeManager_Cls
            {
                /// <summary>
                /// A class that contains all the advanced verbose strings used in the ThemeManager.
                /// </summary>
                public partial class Advanced_Cls
                {
                    public string RegAdd { get; set; } = "{0} > {1} = {2}, RegistryValueKind = {3}";
                    public string RegSkipped { get; set; } = "Skipped: {0}";
                    public string RegDelete { get; set; } = "Deleting: {0}";
                    public string DeleteTask { get; set; } = "Deleting task '{0}' from Task Scheduler";
                    public string PE_GettingAccess { get; set; } = "Trying to gain authorized access to change the access/permissions of '{0}'";
                    public string PE_GetAccessToChangeResources { get; set; } = "Trying to gain authorized access to change the resources of '{0}'";
                    public string PE_CreateBackup { get; set; } = "Creating '{0}' backup";
                    public string PE_GetBackupPermissions { get; set; } = "Trying to get '{0}' permissions backup";
                    public string PE_PatchingPE { get; set; } = "Patching '{0}' resources";
                    public string PE_RestoringPermissions { get; set; } = "Restoring '{0}' permissions from backup";
                    public string DeletingHighContrastThemes { get; set; } = "Deleting high contrast themes in '{0}'";
                    public string UxTheme_SettingVS { get; set; } = "Setting visual styles: {0}({1}, {2}, {3}, {4}) returned {5}";
                    public string UxTheme_EnableTheme { get; set; } = "Enabling theming: {0}.{1}({2}) returned {3}";
                    public string User32_SPI { get; set; } = "Calling {0}.{1}({2}, {3}, {4}, {5}) returned {6}";
                    public string User32_SSC { get; set; } = "Calling {0}.{1}({2}, {3}) returned {4}";
                    public string User32_SMT { get; set; } = "Broadcasting all effects made to registry: {0}.{1}({2}, {3}, {4}, {5}, {6}, {7}, {8})";
                    public string SettingSlideshow { get; set; } = "Setting desktop slideshow data by modifying '{0}'";
                    public string SettingHSLImage { get; set; } = "Modifying the HSL filter of the selected image '{0}'";
                    public string EnableExplorerBar { get; set; } = "Enabling the Explorer bar by renaming UIRibbon.dll to UIRibbon.dll_bak";
                    public string RestoreExplorerBar { get; set; } = "Restoring the Explorer ribbon/bar by renaming UIRibbon.dll_bak back to UIRibbon.dll";
                    public string GetInstanceLogonUIImg { get; set; } = "Loading an instance of {0} image into memory";
                    public string GrayscaleLogonUIImg { get; set; } = "Applying the grayscale effect to {0} image";
                    public string BlurringLogonUIImg { get; set; } = "Applying a blur effect to {0} image";
                    public string NoiseLogonUIImg { get; set; } = "Applying a noise effect to {0} image";
                    public string LogonUIImgSaved { get; set; } = "Modified {0} image is saved as '{1}'";
                    public string LogonUIImgNUMSaved { get; set; } = "Modified LogonUI image number {1} is saved as '{0}'";
                    public string RenderingCursor { get; set; } = "Rendering cursor '{0}'";
                    public string CursorRenderedInto { get; set; } = "Cursor is rendered as '{0}";
                    public string DelCursorWPFromReg { get; set; } = "Deleting registry value 'WinPaletter' from '{0}' to start cursors restoration";
                }
            }
        }
    }
}