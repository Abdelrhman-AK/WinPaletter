using WinPaletter.Theme.Structures;

namespace WinPaletter.Theme
{
    public partial class Manager
    {
        ///<summary>
        /// Structure instance that contains data about WinPaletter theme information.
        ///</summary>
        [Newtonsoft.Json.JsonProperty(nameof(Info))]
        public Info Info { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about WinPaletter theme, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(AppTheme))]
        public AppTheme AppTheme { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows 12 colors and appearance, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(Windows12))]
        public Windows12 Windows12 { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows 12 LogonUI appearance, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(LogonUI12))]
        public LogonUI10x LogonUI12 { get; set; } = new("12");

        /// <summary>
        /// Structure instance that contains data about Windows 11 colors and appearance, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(Windows11))]
        public Windows11 Windows11 { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows 11 LogonUI appearance, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(LogonUI11))]
        public LogonUI10x LogonUI11 { get; set; } = new("11");

        /// <summary>
        /// Structure instance that contains data about Windows 10 colors and appearance, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(Windows10))]
        public Windows10 Windows10 { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows 10 LogonUI appearance, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(LogonUI10))]
        public LogonUI10x LogonUI10 { get; set; } = new("10");

        /// <summary>
        /// Structure instance that contains data about Windows 8.1 colors and appearance, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(Windows81))]
        public Windows81 Windows81 { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows 8.1 LogonUI appearance, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(LogonUI81))]
        public Structures.LogonUI81 LogonUI81 { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows 8 colors and appearance, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(Windows8))]
        public Windows8 Windows8 { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows 7 colors and appearance, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(Windows7))]
        public Windows7 Windows7 { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows 7 LogonUI appearance, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(LogonUI7))]
        public Structures.LogonUI7 LogonUI7 { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows Vista colors and appearance, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(WindowsVista))]
        public WindowsVista WindowsVista { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows XP colors and appearance, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(WindowsXP))]
        public WindowsXP WindowsXP { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows XP LogonUI appearance, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(LogonUIXP))]
        public Structures.LogonUIXP LogonUIXP { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about classic Windows colors, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(Win32))]
        public Structures.Win32UI Win32 { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows Accessibility, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(Accessibility))]
        public Accessibility Accessibility { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows wallpaper, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(Wallpaper))]
        public Wallpaper Wallpaper { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Wallpaper Tone, and it can be customized.
        /// <br></br>This property targets Windows 12 (to avoid overlapping).
        /// <br></br><br></br><br>- Wallpaper Tone is a feature by WinPaletter. It modifies images' HSL filter to alter wallpaper colors.</br>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(WallpaperTone_W12))]
        public WallpaperTone WallpaperTone_W12 { get; set; } = new("Win12");

        /// <summary>
        /// Structure instance that contains data about Wallpaper Tone, and it can be customized.
        /// <br></br>This property targets Windows 11 (to avoid overlapping).
        /// <br></br><br></br><br>- Wallpaper Tone is a feature by WinPaletter. It modifies images' HSL filter to alter wallpaper colors.</br>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(WallpaperTone_W11))]
        public WallpaperTone WallpaperTone_W11 { get; set; } = new("Win11");

        /// <summary>
        /// Structure instance that contains data about Wallpaper Tone, and it can be customized.
        /// <br></br>This property targets Windows 10 (to avoid overlapping).
        /// <br></br><br></br><br>- Wallpaper Tone is a feature by WinPaletter. It modifies images' HSL filter to alter wallpaper colors.</br>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(WallpaperTone_W10))]
        public WallpaperTone WallpaperTone_W10 { get; set; } = new("Win10");

        /// <summary>
        /// Structure instance that contains data about Wallpaper Tone, and it can be customized.
        /// <br></br>This property targets Windows 8.1 (to avoid overlapping).
        /// <br></br><br></br><br>- Wallpaper Tone is a feature by WinPaletter. It modifies images' HSL filter to alter wallpaper colors.</br>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(WallpaperTone_W81))]
        public WallpaperTone WallpaperTone_W81 { get; set; } = new("Win8.1");

        /// <summary>
        /// Structure instance that contains data about Wallpaper Tone, and it can be customized.
        /// <br></br>This property targets Windows 8 (to avoid overlapping).
        /// <br></br><br></br><br>- Wallpaper Tone is a feature by WinPaletter. It modifies images' HSL filter to alter wallpaper colors.</br>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(WallpaperTone_W8))]
        public WallpaperTone WallpaperTone_W8 { get; set; } = new("Win8");

        /// <summary>
        /// Structure instance that contains data about Wallpaper Tone, and it can be customized.
        /// <br></br>This property targets Windows 7 (to avoid overlapping).
        /// <br></br><br></br><br>- Wallpaper Tone is a feature by WinPaletter. It modifies images' HSL filter to alter wallpaper colors.</br>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(WallpaperTone_W7))]
        public WallpaperTone WallpaperTone_W7 { get; set; } = new("Win7");

        /// <summary>
        /// Structure instance that contains data about Wallpaper Tone, and it can be customized.
        /// <br></br>This property targets Windows Vista (to avoid overlapping).
        /// <br></br><br></br><br>- Wallpaper Tone is a feature by WinPaletter. It modifies images' HSL filter to alter wallpaper colors.</br>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(WallpaperTone_WVista))]
        public WallpaperTone WallpaperTone_WVista { get; set; } = new("WinVista");

        /// <summary>
        /// Structure instance that contains data about Wallpaper Tone, and it can be customized.
        /// <br></br>This property targets Windows XP (to avoid overlapping).
        /// <br></br><br></br><br>- Wallpaper Tone is a feature by WinPaletter. It modifies images' HSL filter to alter wallpaper colors.</br>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(WallpaperTone_WXP))]
        public WallpaperTone WallpaperTone_WXP { get; set; } = new("WinXP") { Image = $"{SysPaths.Windows}\\Web\\Wallpaper\\Bliss.bmp" };

        /// <summary>
        /// Structure instance that contains data about WinPaletter cursors, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(Cursors))]
        public Cursors Cursors { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows metrics and fonts, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(MetricsFonts))]
        public MetricsFonts MetricsFonts { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows effects and animations, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(WindowsEffects))]
        public WinEffects WindowsEffects { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about screen saver, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(ScreenSaver))]
        public ScreenSaver ScreenSaver { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows sounds, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(Sounds))]
        public Sounds Sounds { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows switcher (Alt+Tab) appearance, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(AltTab))]
        public AltTab AltTab { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows icons, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(Icons))]
        public Icons Icons { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Command Prompt, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(CommandPrompt))]
        public Console CommandPrompt { get; set; } = new("Terminal_CMD_Enabled", [string.Empty, "%SystemRoot%_System32_cmd.exe"]);

        /// <summary>
        /// Structure instance that contains data about PowerShell x86, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(PowerShellx86))]
        public Console PowerShellx86 { get; set; } = new("Terminal_PS_32_Enabled", [SysPaths.PS86_reg]) { PopupForeground = 15, PopupBackground = 3, ScreenColorsForeground = 6, ScreenColorsBackground = 5 };

        /// <summary>
        /// Structure instance that contains data about PowerShell x64, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(PowerShellx64))]
        public Console PowerShellx64 { get; set; } = new("Terminal_PS_64_Enabled", [SysPaths.PS64_reg]) { PopupForeground = 15, PopupBackground = 3, ScreenColorsForeground = 6, ScreenColorsBackground = 5 };

        /// <summary>
        /// class that contains data about Windows Terminal Stable, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(Terminal))]
        public WinTerminal Terminal { get; set; } = new();

        /// <summary>
        /// class that contains data about Windows Terminal Preview, and it can be customized.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(nameof(TerminalPreview))]
        public WinTerminal TerminalPreview { get; set; } = new();
    }
}