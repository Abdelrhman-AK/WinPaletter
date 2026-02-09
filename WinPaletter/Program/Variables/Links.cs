using System.Windows.Forms;

namespace WinPaletter.GlobalVariables
{
    /// <summary>
    /// Links to the GitHub repository, the store repository, and other useful links.
    /// </summary>
    public static class Links
    {
        private static string getRaw => "?raw=true";
        private static string branch => "blob/master";
        private static string tree => "tree/master";
        private static string branch_store => "blob/main";

        // Don't use "https://www." to avoid conflict with older versions of WinPaletter Store settings in registry
        private static string GitHub => "https://github.com";
        private static string Developer => Application.CompanyName;
        private static string Product => Application.ProductName;
        private static string ProductStore => $"{Product}-Store";
        private static string Repository => $"{Developer}/{Product}";
        private static string Store_Repository => $"{Developer}/{ProductStore}";

        /// <summary>
        /// Link to WinPaletter's GitHub repository.
        /// </summary>
        public static string RepositoryURL => $"{GitHub}/{Repository}";

        /// <summary>
        /// Link to the license File in WinPaletter's GitHub repository.
        /// </summary>
        public static string License => $"{RepositoryURL}/{branch}/License.md{getRaw}";

        /// <summary>
        /// Link to the releases page in WinPaletter's GitHub repository.
        /// </summary>
        public static string Releases => $"{RepositoryURL}/releases";

        /// <summary>
        /// Link to the issues page in WinPaletter's GitHub repository.
        /// </summary>
        public static string Issues => $"{RepositoryURL}/issues";

        /// <summary>
        /// Link to the changelog in WinPaletter's GitHub repository.
        /// </summary>
        public static string Changelog => $"{RepositoryURL}/{branch}/CHANGELOG.md";

        /// <summary>
        /// Link to the updates configuration File in WinPaletter's GitHub repository.
        /// </summary>
        public static string Updates => $"{RepositoryURL}/{branch}/UpdatesConfig/Updates{getRaw}";

        /// <summary>
        /// Link to the languages directory in WinPaletter's GitHub repository.
        /// </summary>
        public static string Languages => $"{RepositoryURL}/{tree}/Languages";

        /// <summary>
        /// Link to the WinPaletter Store repository.
        /// </summary>
        /// Replace "https://www." with "https://" to avoid conflict with older versions of WinPaletter Store settings in registry
        public static string Store_RepositoryURL => $"{GitHub}/{Store_Repository}";

        /// <summary>
        /// Link to the main database in the WinPaletter Store repository.
        /// </summary>
        /// Replace "https://www." with "https://" to avoid conflict with older versions of WinPaletter Store settings in registry
        public static string Store_MainDB => $"{RepositoryURL}/{branch}/Store/store.wpdb{getRaw}";

        /// <summary>
        /// Link to the 2nd database in the WinPaletter Store repository.
        /// </summary>
        public static string Store_2ndDB => $"{Store_RepositoryURL}/{branch_store}/store.wpdb{getRaw}";

        /// <summary>
        /// Link to the Serilog repository's page.
        /// </summary>
        public static string Serilog => $"{GitHub}/serilog/serilog";

        /// <summary>
        /// Link to the SecureUxTheme releases page.
        /// </summary>
        public static string SecureUxThemeReleases => $"{GitHub}/namazso/SecureUxTheme/releases";

        /// <summary>
        /// Link to the SecureUxTheme login loop issue wiki page.
        /// </summary>
        public static string SecureUxThemeLoginLoopIssue => $"{GitHub}/namazso/SecureUxTheme/wiki/Help:-Login-loop-after-installing-SecureUxTheme";

        /// <summary>
        /// Gets the URL for the UXTheme Multi-Patcher software download page.
        /// </summary>
        public static string UxTheme_multi_patcher => "https://www.neowin.net/software/download-uxtheme-multi-patcher-90-for-windows-881/";

        /// <summary>
        /// Gets the URL for GitHub profile settings page.
        /// </summary>
        public static string GitHubProfileSettings => $"{GitHub}/settings/profile";

        /// <summary>
        /// Links to WinPaletter Wiki pages.
        /// </summary>
        public static class Wiki
        {
            /// <summary>
            /// Link to the wiki in WinPaletter's GitHub repository.
            /// </summary>
            public static string WikiURL => $"{RepositoryURL}/wiki";

            /// <summary>
            /// Link to the WinPaletter Windows 10/11 colors editing wiki.
            /// </summary>
            public static string Win10xColors => $"{WikiURL}/Edit-Windows-colors-and-theme#windows-11--10";

            /// <summary>
            /// Link to the WinPaletter Windows 8.1 colors editing wiki.
            /// </summary>
            public static string Win81Colors => $"{WikiURL}/Edit-Windows-colors-and-theme#windows-81";

            /// <summary>
            /// Link to the WinPaletter Windows Vista colors editing wiki.
            /// </summary>
            public static string WinVistaColors => $"{WikiURL}/Edit-Windows-colors-and-theme#windows-vista";

            /// <summary>
            /// Link to the WinPaletter Windows XP themes editing wiki.
            /// </summary>
            public static string WinXPThemes => $"{WikiURL}/Edit-Windows-colors-and-theme#windows-xp";

            /// <summary>
            /// Link to the WinPaletter Windows 7 colors editing wiki.
            /// </summary>
            public static string Win7Colors_Registry => $"{WikiURL}/Edit-Windows-colors-and-theme#windows-7";

            /// <summary>
            /// Link to the WinPaletter Store source creation wiki.
            /// </summary>
            public static string StoreCreateSource => $"{WikiURL}/Create-an-online-WinPaletter-Store-source-(server%5CGitHub-repository)-for-hosting-WinPaletter-themes";

            /// <summary>
            /// Link to the WinPaletter Store sources extension wiki.
            /// </summary>
            public static string StoreExtension => $"{WikiURL}/WinPaletter-Sources-extension";

            /// <summary>
            /// Link to the WinPaletter Store themes upload wiki.
            /// </summary>
            public static string StoreUpload => $"{WikiURL}/Upload-themes-to-WinPaletter-Store-repository";

            /// <summary>
            /// Link to the WinPaletter Store basics wiki.
            /// </summary>
            public static string StoreBasics => $"{WikiURL}/WinPaletter-Store-basics";

            /// <summary>
            /// Link to Windows switcher (Alt+Tab) appearance wiki.
            /// </summary>
            public static string AltTab => $"{WikiURL}/Edit-Windows-switcher-(Alt-Tab-appearance)";

            /// <summary>
            /// Link to WinPaletter application themes wiki.
            /// </summary>
            public static string WinPaletterApplicationTheme => $"{WikiURL}/Edit-WinPaletter-application-theme";

            /// <summary>
            /// Link to WinPaletter cursors wiki.
            /// </summary>
            public static string Cursors => $"{WikiURL}/Edit-Windows-cursors";

            /// <summary>
            /// Link to WinPaletter theme info edit wiki.
            /// </summary>
            public static string ThemeInfo => $"{WikiURL}/Edit-theme-info";

            /// <summary>
            /// Link to WinPaletter LognoUI modification for Windows 10/11 wiki.
            /// </summary>
            public static string LogonUI_10x => $"{WikiURL}/Edit-LogonUI-screen#windows-11--10";

            /// <summary>
            /// Link to WinPaletter LognoUI modification for Windows 8.1/7 wiki.
            /// </summary>
            public static string LogonUI_8x => $"{WikiURL}/Edit-LogonUI-screen#windows-81-and-windows-7";

            /// <summary>
            /// Link to WinPaletter LognoUI modification for Windows XP wiki.
            /// </summary>
            public static string LogonUI_XP => $"{WikiURL}/Edit-LogonUI-screen#windows-xp";

            /// <summary>
            /// Link to WinPaletter metrics and fonts wiki.
            /// </summary>
            public static string MetricsAndFonts => $"{WikiURL}/Edit-Windows-Metrics-and-Fonts";

            /// <summary>
            /// Link to WinPaletter screen saver wiki.
            /// </summary>
            public static string ScreenSaver => $"{WikiURL}/Edit-Screen-Saver";

            /// <summary>
            /// Link to WinPaletter sounds wiki.
            /// </summary>
            public static string Sounds => $"{WikiURL}/Edit-Sounds";

            /// <summary>
            /// Link to WinPaletter consoles (Command Prompt/PowerShell) wiki.
            /// </summary>
            public static string Consoles => $"{WikiURL}/Edit-Windows-consoles-(Command-Prompt-and-PowerShell)";

            /// <summary>
            /// Link to WinPaletter terminals (Windows Terminal) wiki.
            /// </summary>
            public static string Terminals => $"{WikiURL}/Edit-Windows-Terminals-(Windows-10-and-later)";

            /// <summary>
            /// Link to WinPaletter wallpaper wiki.
            /// </summary>
            public static string Wallpaper => $"{WikiURL}/Edit-Wallpaper";

            /// <summary>
            /// Link to WinPaletter Windows Classic Palette wiki.
            /// </summary>
            public static string ClassicColors => $"{WikiURL}/Edit-Windows-classic-colors";

            /// <summary>
            /// Link to WinPaletter high contrast wiki.
            /// </summary>
            public static string HighContrast => $"{WikiURL}/Edit-High-Contrast";

            /// <summary>
            /// Link to WinPaletter Windows Effects wiki.
            /// </summary>
            public static string WindowsEffects => $"{WikiURL}/Edit-Windows-Effects";

            /// <summary>
            /// Link to WinPaletter PE patching options wiki.
            /// </summary>
            public static string PE => $"{WikiURL}/Advanced-options-to-patch-PE-files";

            /// <summary>
            /// Link to WinPaletter antiviruses issue wiki.
            /// </summary>
            public static string AntivirusIssue => $"{WikiURL}/Antiviruses-or-browsers-download-issue";

            /// <summary>
            /// Link to WinPaletter language JSON File creation wiki.
            /// </summary>
            public static string LanguageCreation => $"{WikiURL}/Language-creation";

            /// <summary>
            /// Link to WinPaletter language JSON File creation wiki (old methods).
            /// </summary>
            public static string LanguageCreation_OldMethods => $"{WikiURL}/Language-creation-(old-methods)";

            /// <summary>
            /// Link to WinPaletter language JSON File creation wiki (old methods)
            /// </summary>
            public static string LanguageCreation_OldMethods_UpdateLanguageFile => $"{LanguageCreation_OldMethods}#3-update-your-language-file-when-a-new-winpaletter-is-released";
        }
    }
}
