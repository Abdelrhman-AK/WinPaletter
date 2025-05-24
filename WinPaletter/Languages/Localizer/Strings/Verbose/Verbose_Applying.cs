namespace WinPaletter
{
    public partial class Localizer
    {
        public partial class Strings_Cls
        {
            public partial class ThemeManager_Cls
            {
                public partial class Actions_Cls
                {
                    public string RestorePoint0 { get; set; } = "WinPaletter is creating a system restore point before making changes to system preferences";
                    public string RestorePoint1 { get; set; } = "Each restore point uses 100–300 MB. Delete old ones if you apply many themes to save disk space.";
                    public string RestorePoint2 { get; set; } = "This process takes about a minute or less, depending on your device's performance";
                    public string RestorePoint3 { get; set; } = "A system restore point has been created. It took {0} seconds.";
                    public string ApplyingTheme { get; set; } = "Applying theme: {0}";
                    public string CloseOnApplying0 { get; set; } = "WinPaletter is still applying the theme. Do you want to close it anyway?";
                    public string CloseOnApplying1 { get; set; } = "If you close it, the theme won't be completely applied and it might be broken.";
                    public string ApplyOS { get; set; } = "WinPaletter will apply the theme as if you are using {0}";
                    public string Admin_Msg0 { get; set; } = "Writing to the registry without administrator rights through deflection";
                    public string Admin_Msg1 { get; set; } = "This deflection will take more time than if it were started as an administrator";
                    public string Applying_Started { get; set; } = "The applying process has just started";
                    public string SavingToggles { get; set; } = "Saving the states of theme aspects toggles into the registry";
                    public string Time { get; set; } = "It took {0} seconds";
                    public string Time_MultipleAspects { get; set; } = "They took {0} seconds";
                    public string Time_Cursors { get; set; } = "Applying the Windows cursors took a total of {0} seconds";
                    public string SavingInfo { get; set; } = "Saving theme information into the registry";
                    public string ThemeReset { get; set; } = "Resetting the theme to the default Windows theme to apply the new theme correctly";
                    public string Theme { get; set; } = "Applying {0} theme";
                    public string Applying_Feature_ForOS { get; set; } = "Applying {0}'s {1} preferences";
                    public string Applying_Feature { get; set; } = "Applying {0} preferences";
                    public string Applying_Feature_AllUsers { get; set; } = "Applying {0} preferences for all users";
                    public string AppliedWithErrors { get; set; } = "Theme application completed with error(s). It took {0} seconds.";
                    public string Applied { get; set; } = "The theme has just been applied. It took {0} seconds.";
                    public string RenderingImage_MayNotRespond { get; set; } = "WinPaletter may not respond while rendering images to be used as the {0}";
                    public string RenderingImages { get; set; } = "Rendering images for {0}:";
                    public string RenderingImage { get; set; } = "Rendering an image for {0}";
                    public string RenderingCursors { get; set; } = "Rendering Windows Cursors";
                    public string SavingCursorsColors { get; set; } = "Saving the Windows cursor colors to the registry";
                    public string KillingExplorer { get; set; } = "Killing Explorer (It will be restarted)";
                    public string ExplorerRestarted { get; set; } = "Explorer has been restarted. It took about {0} seconds to kill Explorer";
                    public string Complete { get; set; } = "All operations are complete";
                    public string LogClosure { get; set; } = "This log will close after {0} second(s)";
                    public string LogTimerFinished { get; set; } = "The theme has just been applied. You may close the log.";
                }
            }
        }
    }
}