public partial class Localizer
{
    public partial class Strings_Cls
    {
        public partial class GitHubStrings_Cls
        {
            public string Overview_NoEmail { get; set; } = "There is no provided e-mail";
            public string Overview_NoLocation { get; set; } = "There is no provided location";
            public string Timing_Second { get; set; } = "second";
            public string Timing_Seconds { get; set; } = "seconds";
            public string Timing_Minute { get; set; } = "minute";
            public string Timing_Minutes { get; set; } = "minutes";
            public string Timing_Hour { get; set; } = "hour";
            public string Timing_Hours { get; set; } = "hours";
            public string Timing_Ago { get; set; } = "ago";
            public string Timing_Today { get; set; } = "Today";
            public string Timing_Yesterday { get; set; } = "Yesterday";
            public string Timing_Tomorrow { get; set; } = "Tomorrow";
            public string Branch { get; set; } = "Branch";
            public string LastUpdated { get; set; } = "Last updated";
            public string Branch_Ahead { get; set; } = "Ahead";
            public string Branch_Behind { get; set; } = "Behind";
            public string Committer { get; set; } = "Committer";
            public string LastCommitMsg { get; set; } = "Last commit message";
            public string API_RateLimited { get; set; } = "This limit will reset at {0}.";
            public string SelectUploadMethod { get; set; } = "Please select an upload method.";
            public string Explorer_FileNotFound { get; set; } = "{0} can't find '{1}'. Check the spelling and try again.";
            public string NewBranch_Error { get; set; } = "Couldn't create branch `{0}`. Try creating it manually in your browser.";
            public string NewBranch_AlreadyExists { get; set; } = "Your forked repository already has this branch. Try again with another branch name.";
            public string Branch_InvalidName { get; set; } = "Branch name `{0}` is invalid. Try another name.";
            public string Branch_NamingRules { get; set; } = "Branch names cannot be empty and must not contain spaces, control characters, or any of these symbols: ~ ^ : ? * [ \\ . They also cannot start or end with a slash (/), contain consecutive slashes (//), consecutive dots (..), or end with '.lock'.";
            public string Branch_CannotAccess_Protected { get; set; } = "You cannot access branch `{0}` because it is protected.";
            public string Branch_CannotAccess_Protected_Tip { get; set; } = "Protected branches help prevent accidental changes. To access this branch, either unprotect it or clone it as an unprotected branch if you want to keep a protected instance.";
            public string Branch_CannotDoOperation_Protected { get; set; } = "Cannot perform this operation on branch `{0}` because it is protected.";
            public string BranchProtection_ProtectConfirm { get; set; } = "Are you sure you want to protect the branch '{0}'?";
            public string BranchProtection_UnprotectConfirm { get; set; } = "Are you sure you want to unprotect the branch '{0}'?";
            public string BranchProtection_ProtectDetails { get; set; } = "Protected branches help prevent force pushes and deletion, and can enforce pull request reviews.";
            public string BranchProtection_UnprotectDetails { get; set; } = "Unprotecting this branch will remove restrictions such as preventing force pushes, deletions, and mandatory pull request reviews. Make sure this is safe before proceeding.";
            public string BranchProtection_ProtectFailed { get; set; } = "Failed to protect branch '{0}': {1}";
            public string BranchProtection_UnprotectFailed { get; set; } = "Failed to unprotect branch '{0}': {1}";
            public string Branch_Delete { get; set; } = "Are you sure you want to delete branch `{0}`?";
            public string Branch_Delete_Error { get; set; } = "Couldn't delete branch `{0}`. Try deleting it manually in your browser.";
            public string Branch_Rename_AlreadyExists { get; set; } = "Your forked repository already has this branch. Try again with another branch name.";
            public string Explorer_Properties { get; set; } = "Properties";
            public string Explorer_View { get; set; } = "View";
            public string Explorer_View_LargeIcons { get; set; } = "Large Icons";
            public string Explorer_View_SmallIcons { get; set; } = "Small Icons";
            public string Explorer_View_List { get; set; } = "List";
            public string Explorer_View_Details { get; set; } = "Details";
            public string Explorer_View_Tiles { get; set; } = "Tiles";
            public string Explorer_Type_Folder { get; set; } = "File folder";
            public string Explorer_DetailsHeader_Name { get; set; } = "Name";
            public string Explorer_DetailsHeader_Type { get; set; } = "Type";
            public string Explorer_DetailsHeader_Size { get; set; } = "Size";
            public string Explorer_DetailsHeader_URL { get; set; } = "URL";
            public string Explorer_Items { get; set; } = "items";
            public string Explorer_Item { get; set; } = "item";
            public string Explorer_Selected { get; set; } = "selected";
            public string ExplorerStatus_Forked { get; set; } = "Themes repository is forked.";
            public string ExplorerStatus_NotForked { get; set; } = "Themes repository is not forked.";
            public string ExplorerStatus_SyncAndForkToManage { get; set; } = "Fork to manage your uploaded themes.";
            public string Explorer_NotAllowedChars { get; set; } = "Not allowed characters";
            public string Explorer_ReversedWords { get; set; } = "Reserved or invalid names";
            public string Explorer_InvalidCharToolTip { get; set; } = "You have entered an invalid character or a reserved word.";
            public string Explorer_EntryExists { get; set; } = "You have entered a name of an existing entry";
            public string Explorer_Confirmation_DeleteFile { get; set; } = "Are you sure you want to permanently delete this file?";
            public string Explorer_Confirmation_DeleteFiles { get; set; } = "Are you sure you want to permanently delete the selected files?";
            public string Explorer_Confirmation_Title_DeleteFile { get; set; } = "Delete file";
            public string Explorer_Confirmation_Title_DeleteFiles { get; set; } = "Delete files";
            public string Explorer_Confirmation_DeleteFolder { get; set; } = "Are you sure you want to permanently delete this folder?";
            public string Explorer_Confirmation_DeleteFolders { get; set; } = "Are you sure you want to permanently delete the selected folders?";
            public string Explorer_Confirmation_Title_DeleteFolder { get; set; } = "Delete folder";
            public string Explorer_Confirmation_Title_DeleteFolders { get; set; } = "Delete folders";
            public string Explorer_Compare_FilesFrom { get; set; } = "Files from";
            public string Explorer_Compare_FilesAlreadyIn { get; set; } = "Files already in";
            public string Explorer_Compare_SelectFiles { get; set; } = "Select which file you want to keep";
            public string Explorer_Files { get; set; } = "files";
            public string Explorer_File { get; set; } = "file";
            public string Explorer_Conflict_Copying { get; set; } = "Copying";
            public string Explorer_Conflict_Moving { get; set; } = "Moving";
            public string Explorer_Conflict_From { get; set; } = "from";
            public string Explorer_Conflict_To { get; set; } = "to";
            public string Explorer_Conflict_DestHasFile { get; set; } = "The destination already has a file named \"{0}\".";
            public string Explorer_Conflict_DestHasFiles { get; set; } = "The destination already has these {0} files.";
            public string Explorer_Conflict_Replace2Files { get; set; } = "Replace the file in the destination";
            public string Explorer_Conflict_Skip2Files { get; set; } = "Skip this file";
            public string Explorer_Conflict_Compare2Files { get; set; } = "Compare info for both files";
            public string Explorer_Conflict_ReplaceFiles { get; set; } = "Replace the files in the destination";
            public string Explorer_Conflict_SkipFiles { get; set; } = "Skip these files";
            public string Explorer_Conflict_CompareFiles { get; set; } = "Compare info for the files";
        }
    }
}