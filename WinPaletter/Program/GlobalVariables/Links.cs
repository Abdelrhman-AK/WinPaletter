using System.Windows.Forms;

namespace WinPaletter.GlobalVariables
{
    /// <summary>
    /// Links to the GitHub repository, the store repository, and other useful links.
    /// </summary>
    public static class Links
    {
        private readonly static string getRaw = "?raw=true";
        private readonly static string branch = "blob/master";
        private readonly static string tree = "tree/master";
        private readonly static string branch_store = "blob/main";

        // Don't use "https://www." to avoid conflict with older versions of WinPaletter Store settings in registry
        private readonly static string GitHub = "https://github.com";
        private readonly static string Developer = Application.CompanyName;
        private readonly static string Product = Application.ProductName;
        private readonly static string ProductStore = $"{Product}-Store";
        private readonly static string Repository = $"{Developer}/{Product}";
        private readonly static string Store_Repository = $"{Developer}/{ProductStore}";

        /// <summary>
        /// Link to WinPaletter's GitHub repository.
        /// </summary>
        public readonly static string RepositoryURL = $"{GitHub}/{Repository}";

        /// <summary>
        /// Link to the license file in WinPaletter's GitHub repository.
        /// </summary>
        public readonly static string License = $"{RepositoryURL}/{branch}/License.md{getRaw}";

        /// <summary>
        /// Link to the releases page in WinPaletter's GitHub repository.
        /// </summary>
        public readonly static string Releases = $"{RepositoryURL}/releases";

        /// <summary>
        /// Link to the issues page in WinPaletter's GitHub repository.
        /// </summary>
        public readonly static string Issues = $"{RepositoryURL}/issues";

        /// <summary>
        /// Link to the changelog in WinPaletter's GitHub repository.
        /// </summary>
        public readonly static string Changelog = $"{RepositoryURL}/{branch}/CHANGELOG.md";

        /// <summary>
        /// Link to the updates configuration file in WinPaletter's GitHub repository.
        /// </summary>
        public readonly static string Updates = $"{RepositoryURL}/{branch}/UpdatesConfig/Updates{getRaw}";

        /// <summary>
        /// Link to the languages directory in WinPaletter's GitHub repository.
        /// </summary>
        public readonly static string Languages = $"{RepositoryURL}/{tree}/Languages";

        /// <summary>
        /// Link to PayPal to donate to the developer.
        /// </summary>
        public readonly static string PayPal = "https://paypal.me/AbdelrhmanAK";

        /// <summary>
        /// Link to the WinPaletter Store repository.
        /// </summary>
        /// Replace "https://www." with "https://" to avoid conflict with older versions of WinPaletter Store settings in registry
        public readonly static string Store_RepositoryURL = $"{GitHub}/{Store_Repository}";

        /// <summary>
        /// Link to the main database in the WinPaletter Store repository.
        /// </summary>
        /// Replace "https://www." with "https://" to avoid conflict with older versions of WinPaletter Store settings in registry
        public readonly static string Store_MainDB = $"{RepositoryURL}/{branch}/Store/store.wpdb{getRaw}";

        /// <summary>
        /// Link to the 2nd database in the WinPaletter Store repository.
        /// </summary>
        public readonly static string Store_2ndDB = $"{Store_RepositoryURL}/{branch_store}/store.wpdb{getRaw}";

        /// <summary>
        /// Links to WinPaletter Wiki pages.
        /// </summary>
        public static class Wiki
        {
            /// <summary>
            /// Link to the wiki in WinPaletter's GitHub repository.
            /// </summary>
            public readonly static string WikiURL = $"{RepositoryURL}/wiki";

            /// <summary>
            /// Link to the WinPaletter Windows 10/11 colors editing wiki.
            /// </summary>
            public readonly static string Win10xColors = $"{WikiURL}/Edit-Windows-colors-and-theme#windows-11--10";

            /// <summary>
            /// Link to the WinPaletter Windows 8.1 colors editing wiki.
            /// </summary>
            public readonly static string Win81Colors = $"{WikiURL}/Edit-Windows-colors-and-theme#windows-81";

            /// <summary>
            /// Link to the WinPaletter Windows Vista colors editing wiki.
            /// </summary>
            public readonly static string WinVistaColors = $"{WikiURL}/Edit-Windows-colors-and-theme#windows-vista";

            /// <summary>
            /// Link to the WinPaletter Windows XP themes editing wiki.
            /// </summary>
            public readonly static string WinXPThemes = $"{WikiURL}/Edit-Windows-colors-and-theme#windows-xp";

            /// <summary>
            /// Link to the WinPaletter Windows 7 colors editing wiki.
            /// </summary>
            public readonly static string Win7Colors_Registry = $"{WikiURL}/Edit-Windows-colors-and-theme#windows-7";

            /// <summary>
            /// Link to the WinPaletter Store source creation wiki.
            /// </summary>
            public readonly static string StoreCreateSource = $"{WikiURL}/Create-an-online-WinPaletter-Store-source-(server%5CGitHub-repository)-for-hosting-WinPaletter-themes";

            /// <summary>
            /// Link to the WinPaletter Store sources extension wiki.
            /// </summary>
            public readonly static string StoreExtension = $"{WikiURL}/WinPaletter-Sources-extension";

            /// <summary>
            /// Link to the WinPaletter Store themes upload wiki.
            /// </summary>
            public readonly static string StoreUpload = $"{WikiURL}/Upload-themes-to-WinPaletter-Store-repository";

            /// <summary>
            /// Link to the WinPaletter Store basics wiki.
            /// </summary>
            public readonly static string StoreBasics = $"{WikiURL}/WinPaletter-Store-basics";

            /// <summary>
            /// Link to Windows switcher (Alt+Tab) appearance wiki.
            /// </summary>
            public readonly static string AltTab = $"{WikiURL}/Edit-Windows-switcher-(Alt-Tab-appearance)";

            /// <summary>
            /// Link to WinPaletter application themes wiki.
            /// </summary>
            public readonly static string WinPaletterApplicationTheme = $"{WikiURL}/Edit-WinPaletter-application-theme";

            /// <summary>
            /// Link to WinPaletter cursors wiki.
            /// </summary>
            public readonly static string Cursors = $"{WikiURL}/Edit-WinPaletter-cursors";

            /// <summary>
            /// Link to WinPaletter theme info edit wiki.
            /// </summary>
            public readonly static string ThemeInfo = $"{WikiURL}/Edit-theme-info";

            /// <summary>
            /// Link to WinPaletter LognoUI modification for Windows 10/11 wiki.
            /// </summary>
            public readonly static string LogonUI_10x = $"{WikiURL}/Edit-LogonUI-screen#windows-11--10";

            /// <summary>
            /// Link to WinPaletter LognoUI modification for Windows 8.1/7 wiki.
            /// </summary>
            public readonly static string LogonUI_8x = $"{WikiURL}/Edit-LogonUI-screen#windows-81-and-windows-7";

            /// <summary>
            /// Link to WinPaletter LognoUI modification for Windows XP wiki.
            /// </summary>
            public readonly static string LogonUI_XP = $"{WikiURL}/Edit-LogonUI-screen#windows-xp";

            /// <summary>
            /// Link to WinPaletter metrics and fonts wiki.
            /// </summary>
            public readonly static string MetricsAndFonts = $"{WikiURL}/Edit-Windows-Metrics-and-Fonts";

            /// <summary>
            /// Link to WinPaletter screen saver wiki.
            /// </summary>
            public readonly static string ScreenSaver = $"{WikiURL}/Edit-Screen-Saver";

            /// <summary>
            /// Link to WinPaletter sounds wiki.
            /// </summary>
            public readonly static string Sounds = $"{WikiURL}/Edit-Sounds";

            /// <summary>
            /// Link to WinPaletter consoles (Command Prompt/PowerShell) wiki.
            /// </summary>
            public readonly static string Consoles = $"{WikiURL}/Edit-Windows-consoles-(Command-Prompt-and-PowerShell)";

            /// <summary>
            /// Link to WinPaletter terminals (Windows Terminal) wiki.
            /// </summary>
            public readonly static string Terminals = $"{WikiURL}/Edit-Windows-Terminals-(Windows-10-and-later)";

            /// <summary>
            /// Link to WinPaletter wallpaper wiki.
            /// </summary>
            public readonly static string Wallpaper = $"{WikiURL}/Edit-Wallpaper";

            /// <summary>
            /// Link to WinPaletter Windows Classic Palette wiki.
            /// </summary>
            public readonly static string ClassicColors = $"{WikiURL}/Edit-Windows-classic-colors";

            /// <summary>
            /// Link to WinPaletter Windows Effects wiki.
            /// </summary>
            public readonly static string WindowsEffects = $"{WikiURL}/Edit-Windows-Effects";

            /// <summary>
            /// Link to WinPaletter PE patching options wiki.
            /// </summary>
            public readonly static string PE = $"{WikiURL}/Advanced-options-to-patch-PE-files";

            /// <summary>
            /// Link to WinPaletter antiviruses issue wiki.
            /// </summary>
            public readonly static string AntivirusIssue = $"{WikiURL}/Antiviruses-or-browsers-download-issue";

            /// <summary>
            /// Link to WinPaletter language JSON file creation wiki.
            /// </summary>
            public readonly static string LanguageCreation = $"{WikiURL}/Language-creation";

            /// <summary>
            /// Link to WinPaletter language JSON file creation wiki (old methods).
            /// </summary>
            public readonly static string LanguageCreation_OldMethods = $"{WikiURL}/Language-creation-(old-methods)";

            /// <summary>
            /// Link to WinPaletter language JSON file creation wiki (old methods) - 3. Update your language file.
            /// </summary>
            public readonly static string LanguageCreation_OldMethods_UpdateLanguageFile = $"{LanguageCreation_OldMethods}#3-update-your-language-file-when-a-new-winpaletter-is-released";
        }
    }
}
