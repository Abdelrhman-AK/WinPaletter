using System.Windows.Forms;

namespace WinPaletter.GlobalVariables
{
    /// <summary>
    /// Links to the GitHub repository, the store repository, and other useful links.
    /// </summary>
    public static class Links
    {
        private static readonly string getRaw = "?raw=true";
        private static readonly string branch = "blob/master";
        private static readonly string tree = "tree/master";
        private static readonly string branch_store = "blob/main";

        // Don't use "https://www." to avoid conflict with older versions of WinPaletter Store settings in registry
        private static readonly string GitHub = "https://github.com";
        private static readonly string Developer = Application.CompanyName;
        private static readonly string Product = Application.ProductName;
        private static readonly string ProductStore = $"{Product}-Store";
        private static readonly string Repository = $"{Developer}/{Product}";
        private static readonly string Store_Repository = $"{Developer}/{ProductStore}";

        /// <summary>
        /// Link to WinPaletter's GitHub repository.
        /// </summary>
        public static readonly string RepositoryURL = $"{GitHub}/{Repository}";

        /// <summary>
        /// Link to the license File in WinPaletter's GitHub repository.
        /// </summary>
        public static readonly string License = $"{RepositoryURL}/{branch}/License.md{getRaw}";

        /// <summary>
        /// Link to the releases page in WinPaletter's GitHub repository.
        /// </summary>
        public static readonly string Releases = $"{RepositoryURL}/releases";

        /// <summary>
        /// Link to the issues page in WinPaletter's GitHub repository.
        /// </summary>
        public static readonly string Issues = $"{RepositoryURL}/issues";

        /// <summary>
        /// Link to the changelog in WinPaletter's GitHub repository.
        /// </summary>
        public static readonly string Changelog = $"{RepositoryURL}/{branch}/CHANGELOG.md";

        /// <summary>
        /// Link to the updates configuration File in WinPaletter's GitHub repository.
        /// </summary>
        public static readonly string Updates = $"{RepositoryURL}/{branch}/UpdatesConfig/Updates{getRaw}";

        /// <summary>
        /// Link to the languages directory in WinPaletter's GitHub repository.
        /// </summary>
        public static readonly string Languages = $"{RepositoryURL}/{tree}/Languages";

        /// <summary>
        /// Link to the WinPaletter Store repository.
        /// </summary>
        /// Replace "https://www." with "https://" to avoid conflict with older versions of WinPaletter Store settings in registry
        public static readonly string Store_RepositoryURL = $"{GitHub}/{Store_Repository}";

        /// <summary>
        /// Link to the main database in the WinPaletter Store repository.
        /// </summary>
        /// Replace "https://www." with "https://" to avoid conflict with older versions of WinPaletter Store settings in registry
        public static readonly string Store_MainDB = $"{RepositoryURL}/{branch}/Store/store.wpdb{getRaw}";

        /// <summary>
        /// Link to the 2nd database in the WinPaletter Store repository.
        /// </summary>
        public static readonly string Store_2ndDB = $"{Store_RepositoryURL}/{branch_store}/store.wpdb{getRaw}";

        /// <summary>
        /// Link to the Serilog repository's page.
        /// </summary>
        public static readonly string Serilog = $"{GitHub}/serilog/serilog";

        /// <summary>
        /// Link to the SecureUxTheme releases page.
        /// </summary>
        public static readonly string SecureUxThemeReleases = $"{GitHub}/namazso/SecureUxTheme/releases";

        /// <summary>
        /// Link to the SecureUxTheme login loop issue wiki page.
        /// </summary>
        public static readonly string SecureUxThemeLoginLoopIssue = $"{GitHub}/namazso/SecureUxTheme/wiki/Help:-Login-loop-after-installing-SecureUxTheme";

        /// <summary>
        /// Gets the URL for the UXTheme Multi-Patcher software download page.
        /// </summary>
        public static readonly string UxTheme_multi_patcher = "https://www.neowin.net/software/download-uxtheme-multi-patcher-90-for-windows-881/";

        /// <summary>
        /// Links to WinPaletter Wiki pages.
        /// </summary>
        public static class Wiki
        {
            /// <summary>
            /// Link to the wiki in WinPaletter's GitHub repository.
            /// </summary>
            public static readonly string WikiURL = $"{RepositoryURL}/wiki";

            /// <summary>
            /// Link to the WinPaletter Windows 10/11 colors editing wiki.
            /// </summary>
            public static readonly string Win10xColors = $"{WikiURL}/Edit-Windows-colors-and-theme#windows-11--10";

            /// <summary>
            /// Link to the WinPaletter Windows 8.1 colors editing wiki.
            /// </summary>
            public static readonly string Win81Colors = $"{WikiURL}/Edit-Windows-colors-and-theme#windows-81";

            /// <summary>
            /// Link to the WinPaletter Windows Vista colors editing wiki.
            /// </summary>
            public static readonly string WinVistaColors = $"{WikiURL}/Edit-Windows-colors-and-theme#windows-vista";

            /// <summary>
            /// Link to the WinPaletter Windows XP themes editing wiki.
            /// </summary>
            public static readonly string WinXPThemes = $"{WikiURL}/Edit-Windows-colors-and-theme#windows-xp";

            /// <summary>
            /// Link to the WinPaletter Windows 7 colors editing wiki.
            /// </summary>
            public static readonly string Win7Colors_Registry = $"{WikiURL}/Edit-Windows-colors-and-theme#windows-7";

            /// <summary>
            /// Link to the WinPaletter Store source creation wiki.
            /// </summary>
            public static readonly string StoreCreateSource = $"{WikiURL}/Create-an-online-WinPaletter-Store-source-(server%5CGitHub-repository)-for-hosting-WinPaletter-themes";

            /// <summary>
            /// Link to the WinPaletter Store sources extension wiki.
            /// </summary>
            public static readonly string StoreExtension = $"{WikiURL}/WinPaletter-Sources-extension";

            /// <summary>
            /// Link to the WinPaletter Store themes upload wiki.
            /// </summary>
            public static readonly string StoreUpload = $"{WikiURL}/Upload-themes-to-WinPaletter-Store-repository";

            /// <summary>
            /// Link to the WinPaletter Store basics wiki.
            /// </summary>
            public static readonly string StoreBasics = $"{WikiURL}/WinPaletter-Store-basics";

            /// <summary>
            /// Link to Windows switcher (Alt+Tab) appearance wiki.
            /// </summary>
            public static readonly string AltTab = $"{WikiURL}/Edit-Windows-switcher-(Alt-Tab-appearance)";

            /// <summary>
            /// Link to WinPaletter application themes wiki.
            /// </summary>
            public static readonly string WinPaletterApplicationTheme = $"{WikiURL}/Edit-WinPaletter-application-theme";

            /// <summary>
            /// Link to WinPaletter cursors wiki.
            /// </summary>
            public static readonly string Cursors = $"{WikiURL}/Edit-Windows-cursors";

            /// <summary>
            /// Link to WinPaletter theme info edit wiki.
            /// </summary>
            public static readonly string ThemeInfo = $"{WikiURL}/Edit-theme-info";

            /// <summary>
            /// Link to WinPaletter LognoUI modification for Windows 10/11 wiki.
            /// </summary>
            public static readonly string LogonUI_10x = $"{WikiURL}/Edit-LogonUI-screen#windows-11--10";

            /// <summary>
            /// Link to WinPaletter LognoUI modification for Windows 8.1/7 wiki.
            /// </summary>
            public static readonly string LogonUI_8x = $"{WikiURL}/Edit-LogonUI-screen#windows-81-and-windows-7";

            /// <summary>
            /// Link to WinPaletter LognoUI modification for Windows XP wiki.
            /// </summary>
            public static readonly string LogonUI_XP = $"{WikiURL}/Edit-LogonUI-screen#windows-xp";

            /// <summary>
            /// Link to WinPaletter metrics and fonts wiki.
            /// </summary>
            public static readonly string MetricsAndFonts = $"{WikiURL}/Edit-Windows-Metrics-and-Fonts";

            /// <summary>
            /// Link to WinPaletter screen saver wiki.
            /// </summary>
            public static readonly string ScreenSaver = $"{WikiURL}/Edit-Screen-Saver";

            /// <summary>
            /// Link to WinPaletter sounds wiki.
            /// </summary>
            public static readonly string Sounds = $"{WikiURL}/Edit-Sounds";

            /// <summary>
            /// Link to WinPaletter consoles (Command Prompt/PowerShell) wiki.
            /// </summary>
            public static readonly string Consoles = $"{WikiURL}/Edit-Windows-consoles-(Command-Prompt-and-PowerShell)";

            /// <summary>
            /// Link to WinPaletter terminals (Windows Terminal) wiki.
            /// </summary>
            public static readonly string Terminals = $"{WikiURL}/Edit-Windows-Terminals-(Windows-10-and-later)";

            /// <summary>
            /// Link to WinPaletter wallpaper wiki.
            /// </summary>
            public static readonly string Wallpaper = $"{WikiURL}/Edit-Wallpaper";

            /// <summary>
            /// Link to WinPaletter Windows Classic Palette wiki.
            /// </summary>
            public static readonly string ClassicColors = $"{WikiURL}/Edit-Windows-classic-colors";

            /// <summary>
            /// Link to WinPaletter high contrast wiki.
            /// </summary>
            public static readonly string HighContrast = $"{WikiURL}/Edit-High-Contrast";

            /// <summary>
            /// Link to WinPaletter Windows Effects wiki.
            /// </summary>
            public static readonly string WindowsEffects = $"{WikiURL}/Edit-Windows-Effects";

            /// <summary>
            /// Link to WinPaletter PE patching options wiki.
            /// </summary>
            public static readonly string PE = $"{WikiURL}/Advanced-options-to-patch-PE-files";

            /// <summary>
            /// Link to WinPaletter antiviruses issue wiki.
            /// </summary>
            public static readonly string AntivirusIssue = $"{WikiURL}/Antiviruses-or-browsers-download-issue";

            /// <summary>
            /// Link to WinPaletter language JSON File creation wiki.
            /// </summary>
            public static readonly string LanguageCreation = $"{WikiURL}/Language-creation";

            /// <summary>
            /// Link to WinPaletter language JSON File creation wiki (old methods).
            /// </summary>
            public static readonly string LanguageCreation_OldMethods = $"{WikiURL}/Language-creation-(old-methods)";

            /// <summary>
            /// Link to WinPaletter language JSON File creation wiki (old methods)
            /// </summary>
            public static readonly string LanguageCreation_OldMethods_UpdateLanguageFile = $"{LanguageCreation_OldMethods}#3-update-your-language-file-when-a-new-winpaletter-is-released";
        }
    }
}
