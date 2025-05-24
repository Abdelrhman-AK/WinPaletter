namespace WinPaletter
{
    public partial class Localizer
    {
        public partial class Strings_Cls
        {
            public partial class ThemeManager_Cls
            {
                /// <summary>
                /// A class that contains all the errors strings used in the ThemeManager.
                /// </summary>
                public partial class Errors_Cls
                {
                    public string Error { get; set; } = "An error occurred while applying the {0}. You can press 'Show Errors' to view more details once WinPaletter completes.";
                    public string Error_AllUsers { get; set; } = "An error occurred while applying the {0} for all users. You can press 'Show Errors' to view more details once WinPaletter completes.";
                    public string SavingInfo { get; set; } = "An error occurred while saving the theme information to the registry";
                    public string ThemeReset { get; set; } = "An error occurred while resetting the theme to the default Windows theme";
                    public string RestoreCursors { get; set; } = "An error occurred while resetting the cursors to Aero. However, the process will continue.";
                    public string RestoreCursorsErrorPressOK { get; set; } = "Pressing OK will display the details of the exception error";
                    public string FatalError { get; set; } = "A fatal error occurred, and WinPaletter will not be able to continue applying the theme. Press 'Show Errors' for details.";
                    public string ExplorerRestart { get; set; } = "An error occurred while restarting Explorer. Open the rescue tools and launch Explorer.";
                    public string ErrorDetails { get; set; } = "Error details: ";
                    public string ErrorHappened { get; set; } = "Errors has occurred. Press 'Show Errors' for details.";
                }
            }
        }
    }
}