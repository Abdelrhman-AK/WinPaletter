namespace WinPaletter
{
    public partial class Localizer
    {
        public partial class Strings_Cls
        {
            public partial class AppTips_Cls
            {
                public string OS_PreviewingAs { get; set; } = "Review the theme as if you are using {0}";
                public string TitlebarColorNotice { get; set; } = "Windows volume slider, UAC dialogs and LogonUI background follow active titlebar color";
                public string PaletteExtraction { get; set; } = "Extracting the palette from the image depends on your device's performance, maximum palette colors number, image quality, and its resolution...";
                public string WallpaperTone_Notice { get; set; } = "You are currently editing preferences for {0}. To change preferences for another OS, switch the OS in the main form.";
                public string VisualStyles_WrongPlatform { get; set; } = "This Visual Styles file is for {0} and does not work with the selected {1} in the main form. Applying it could cause a system crash, so WinPaletter won't apply it to prevent this.";
                public string ClickToEdit { get; set; } = "Click to edit current value";
                public string ClickToReset { get; set; } = "Reset to default Windows value";
                public string PressEnterToUseValue { get; set; } = "Press 'Enter' to use this value.";
                public string PressEscToDismissEditing { get; set; } = "Press 'Esc' to dismiss editing value.";
                public string PaletteSourceGeneration { get; set; } = "Couldn't find a palette.\r\nPlease select a source above to generate a palette from it";

                public string PinToTabs_Title { get; set; } = "Pin {0} into tabs";
                public string PinToTabs_Desc { get; set; } = "Pins the selected form to the tab bar for quick access and easier navigation.";

                public string NewTheme_Title { get; set; } = "Create new WinPaletter theme file";
                public string NewTheme_Desc { get; set; } = "Creates a new WinPaletter theme file using the current Windows visual settings as the base.";

                public string RestoreDefault_Title { get; set; } = "Restore Windows default theme";
                public string RestoreDefault_Desc { get; set; } = "Creates a new WinPaletter theme file using the original Windows default theme, restoring system colors, styles, and appearance settings to their factory state.";

                public string OpenTheme_Title { get; set; } = "Open a WinPaletter theme file";
                public string OpenTheme_Desc { get; set; } = "Loads a saved WinPaletter theme file into the application so its colors, styles, and settings can be viewed, edited, or applied.";

                public string SaveTheme_Title { get; set; } = "Save WinPaletter theme file";
                public string SaveTheme_Desc { get; set; } = "Writes the current theme configuration to a WinPaletter theme file so it can be reused, shared, or restored later.";

                public string SaveAsTheme_Title { get; set; } = "Save WinPaletter theme file as ...";
                public string SaveAsTheme_Desc { get; set; } = "Saves the current theme configuration to a new file with a user-defined name and location, allowing the theme to be stored independently without overwriting existing file.";

                public string EditThemeInfo_Title { get; set; } = "Edit theme information";
                public string EditThemeInfo_Desc { get; set; } = "Opens the theme metadata editor, allowing modification of details such as name, author, version, and description.";

                public string Backups_Title { get; set; } = "Themes backups";
                public string Backups_Desc { get; set; } = "Manages automatically created backups of themes, allowing recovery of previous versions or restoration of themes in case of accidental changes or data loss.";

                public string WallStudio_Title { get; set; } = "WallStudio";
                public string WallStudio_Desc { get; set; } = "Elevate your desktop experience by personalizing it with a theme based on your most cherished image. Using your wallpaper - whether a photo, art, or favorite image - WallStudio analyzes the image and extracts colors from it to create a full Windows theme.";

                public string Settings_Title { get; set; } = "Settings";
                public string Settings_Desc { get; set; } = "Opens WinPaletter settings form where global preferences, behavior options, and customization rules for WinPaletter can be configured.";

                public string Updates_Title { get; set; } = "Updates";
                public string Updates_Desc { get; set; } = "Checks for new versions of the application and allows downloading and installing updates to keep WinPaletter up to date with the latest features and fixes.";

                public string Store_Title { get; set; } = "WinPaletter Store";
                public string Store_Desc { get; set; } = "Opens the built-in theme store where users can browse, preview, and download official or community-shared WinPaletter themes.";

                public string GitHubManager_Title { get; set; } = "Manage and publish my themes";
                public string GitHubManager_Desc { get; set; } = "Connects to GitHub using Octokit to manage your theme repository, enabling you to create, update, organize, and publish WinPaletter themes directly to WinPaletter Themes Store.";

                public string SOS_Title { get; set; } = "SOS";
                public string SOS_Desc { get; set; } = "Opens emergency recovery tools to quickly restore system appearance, undo problematic theme changes, or recover from a broken or unstable visual configuration.";

                public string LayoutToggle_Title { get; set; } = "Compact\\Expand layout";
                public string LayoutToggle_Desc { get; set; } = "Toggles the interface layout between a compact view with reduced spacing and an expanded view with full controls for better readability.";

                public string Wiki_Title { get; set; } = "Help (Wiki)";
                public string Wiki_Desc { get; set; } = "Opens the WinPaletter documentation and wiki, providing guides, explanations, and reference material for using features and customizing themes effectively.";

                public string Releases_Title { get; set; } = "Releases (changelog)";
                public string Releases_Desc { get; set; } = "Shows the release history and changelog, detailing new features, improvements, and fixes across different versions of WinPaletter.";

                public string Logs_Title { get; set; } = "Logs";
                public string Logs_Desc { get; set; } = "Opens WinPaletter's log folder to inspect runtime events, errors, and diagnostic information for troubleshooting and debugging.";

                public string About_Title { get; set; } = "About";
                public string About_Desc { get; set; } = "Displays application information such as version, build details, credits, and links related to WinPaletter.";

                public string UserButton_Desc { get; set; } = "Manages linked user accounts, including the current Windows user account that WinPaletter modifies, and the connected GitHub account, allowing you to switch accounts or manage theme publishing.";
                public string UserButton_WinAccountPart { get; set; } = "Windows User Account";
                public string UserButton_GitHubPart { get; set; } = "GitHub Account";
            }
        }
    }
}