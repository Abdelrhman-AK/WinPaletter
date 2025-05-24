namespace WinPaletter
{
    public partial class Localizer
    {
        /// <summary>
        /// A class that contains all the strings used in the application.
        /// </summary>
        public partial class Strings_Cls
        {
            /// <summary>
            /// A class that contains all the general strings used in the application.
            /// </summary>
            public General_Cls General { get; set; } = new();

            /// <summary>
            /// A class that contains all the strings used for describing the Windows editions.
            /// </summary>
            public WindowsEditions_Cls Windows { get; set; } = new();

            /// <summary>
            /// A class that contains all the strings used in the ThemeManager.
            /// </summary>
            public ThemeManager_Cls ThemeManager { get; set; } = new();

            /// <summary>
            /// A class that contains all the strings used for messages.
            /// </summary>
            public Messages_Cls Messages { get; set; } = new();

            /// <summary>
            /// A class that contains all the strings used for preview legends for Windows 10x.
            /// </summary>
            public Legends_Cls Legends { get; set; } = new();

            /// <summary>
            /// A class that contains all the strings used for describing the users.
            /// </summary>
            public Users_Cls Users { get; set; } = new();

            /// <summary>
            /// A class that contains all the strings used for updates.
            /// </summary>
            public Updates_Cls Updates { get; set; } = new();

            /// <summary>
            /// A class that contains all the strings used for WinPaletter Store.
            /// </summary>
            public Store_Cls Store { get; set; } = new();

            /// <summary>
            /// A class that contains all the strings used for the application tips.
            /// </summary>
            public AppTips_Cls Tips { get; set; } = new();

            /// <summary>
            /// A class that contains all the strings used for tabs.
            /// </summary>
            public Tabs_Cls Tabs { get; set; } = new();

            /// <summary>
            /// A class that contains all the strings used for the services installation/uninstallation.
            /// </summary>
            public Services_Cls Services { get; set; } = new();

            /// <summary>
            /// A class that contains all the strings used PE files patching process.
            /// </summary>
            public PE_Cls PE { get; set; } = new();

            /// <summary>
            /// A class that contains all the strings used for *.theme files text content header.
            /// </summary>
            public MSTheme_Cls MSTheme { get; set; } = new();

            /// <summary>
            /// A class that contains all the strings used for the language File identification.
            /// </summary>
            public Languages_Cls Languages { get; set; } = new();

            /// <summary>
            /// A class that contains all the strings used for descriping files extensions.
            /// </summary>
            public Extensions_Cls Extensions { get; set; } = new();

            /// <summary>
            /// A class that contains all the strings used for the previewer.
            /// </summary>
            public Previewer_Cls Previewer { get; set; } = new();

            /// <summary>
            /// A class that contains all the strings used for the converter.
            /// </summary>
            public Converter_Cls Converter { get; set; } = new();

            /// <summary>
            /// A class that contains all the strings used for the backup feature.
            /// </summary>
            public Backup_Cls Backup { get; set; } = new();

            /// <summary>
            /// A class that contains all the strings used for WinPaletter Aspects.
            /// </summary>
            public Aspects_Cls Aspects { get; set; } = new();
        }
    }

    public partial class Localizer
    {
        /// <summary>
        /// A class that contains all the strings used in the application.
        /// </summary>
        public partial class Strings_Cls
        {
            /// <summary>
            /// A class that contains all the strings used in the ThemeManager.
            /// </summary>
            public partial class ThemeManager_Cls
            {
                /// <summary>
                /// A class that contains all the actions strings used in the ThemeManager.
                /// </summary>
                public Actions_Cls Actions { get; set; } = new();

                /// <summary>
                /// A class that contains all the advanced strings used in the ThemeManager.
                /// </summary>
                public Advanced_Cls Advanced { get; set; } = new();

                /// <summary>
                /// A class that contains all the skip strings used in the ThemeManager.
                /// </summary>
                public Skip_Cls Skip { get; set; } = new();

                /// <summary>
                /// A class that contains all the errors strings used in the ThemeManager.
                /// </summary>
                public Errors_Cls Errors { get; set; } = new();

                /// <summary>
                /// A class that contains all the check strings used in the ThemeManager.
                /// </summary>
                public Check_Cls Check { get; set; } = new();
                /// <summary>
                /// A class that contains all the tips used in the ThemeManager.
                /// </summary>
                public Tips_Cls Tips { get; set; } = new();
            }

            public partial class Aspects_Cls
            {
                /// <summary>
                /// A class that contains all the strings used in the Consoles aspect.
                /// </summary>
                public Consoles_Cls Consoles { get; set; } = new();

                /// <summary>
                /// A class that contains all the strings used in the Terminals aspect.
                /// </summary>
                public Terminals_Cls Terminals { get; set; } = new();
            }
        }
    }
}