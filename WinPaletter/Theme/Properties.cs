using System.Drawing;
using WinPaletter.Theme.Structures;

namespace WinPaletter.Theme
{
    public partial class Manager
    {
        ///<summary>
        /// Structure instance that contains data about WinPaletter theme information.
        ///</summary>
        public Info Info { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about WinPaletter theme, and it can be customized.
        /// </summary>
        public AppTheme AppTheme { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows 12 colors and appearance, and it can be customized.
        /// </summary>
        public Windows10x Windows12 { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows 12 LogonUI appearance, and it can be customized.
        /// </summary>
        public LogonUI10x LogonUI12 { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows 11 colors and appearance, and it can be customized.
        /// </summary>
        public Windows10x Windows11 { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows 11 LogonUI appearance, and it can be customized.
        /// </summary>
        public LogonUI10x LogonUI11 { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows 10 colors and appearance, and it can be customized.
        /// </summary>
        public Windows10x Windows10 = new()
        {
            Color_Index0 = Color.FromArgb(166, 216, 255),
            Color_Index1 = Color.FromArgb(118, 185, 237),
            Color_Index2 = Color.FromArgb(66, 156, 227),
            Color_Index3 = Color.FromArgb(0, 120, 215),
            Color_Index4 = Color.FromArgb(0, 90, 158),
            Color_Index5 = Color.FromArgb(0, 66, 117),
            Color_Index6 = Color.FromArgb(0, 38, 66),
            Color_Index7 = Color.FromArgb(247, 99, 12),
            Titlebar_Active = Color.FromArgb(0, 120, 215),
            Titlebar_Inactive = Color.FromArgb(43, 43, 43),
            StartMenu_Accent = Color.FromArgb(0, 90, 158),
            WinMode_Light = false,
        };

        /// <summary>
        /// Structure instance that contains data about Windows 10 LogonUI appearance, and it can be customized.
        /// </summary>
        public LogonUI10x LogonUI10 { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows 8.1 colors and appearance, and it can be customized.
        /// </summary>
        public Windows81 Windows81 { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows 8.1 LogonUI appearance, and it can be customized.
        /// </summary>
        public Structures.LogonUI81 LogonUI81 { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows 8 colors and appearance, and it can be customized.
        /// </summary>
        public Windows8 Windows8 { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows 7 colors and appearance, and it can be customized.
        /// </summary>
        public Windows7 Windows7 { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows 7 LogonUI appearance, and it can be customized.
        /// </summary>
        public Structures.LogonUI7 LogonUI7 { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows Vista colors and appearance, and it can be customized.
        /// </summary>
        public WindowsVista WindowsVista { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows XP colors and appearance, and it can be customized.
        /// </summary>
        public WindowsXP WindowsXP { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows XP LogonUI appearance, and it can be customized.
        /// </summary>
        public Structures.LogonUIXP LogonUIXP { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about classic Windows colors, and it can be customized.
        /// </summary>
        public Structures.Win32UI Win32 { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows Accessibility, and it can be customized.
        /// </summary>
        public Accessibility Accessibility { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows wallpaper, and it can be customized.
        /// </summary>
        public Wallpaper Wallpaper { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Wallpaper Tone, and it can be customized.
        /// <br></br>This property targets Windows 12 (to avoid overlapping).
        /// <br></br><br></br><br>- Wallpaper Tone is a feature by WinPaletter. It modifies images' HSL filter to alter wallpaper colors.</br>
        /// </summary>
        public WallpaperTone WallpaperTone_W12 { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Wallpaper Tone, and it can be customized.
        /// <br></br>This property targets Windows 11 (to avoid overlapping).
        /// <br></br><br></br><br>- Wallpaper Tone is a feature by WinPaletter. It modifies images' HSL filter to alter wallpaper colors.</br>
        /// </summary>
        public WallpaperTone WallpaperTone_W11 { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Wallpaper Tone, and it can be customized.
        /// <br></br>This property targets Windows 10 (to avoid overlapping).
        /// <br></br><br></br><br>- Wallpaper Tone is a feature by WinPaletter. It modifies images' HSL filter to alter wallpaper colors.</br>
        /// </summary>
        public WallpaperTone WallpaperTone_W10 { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Wallpaper Tone, and it can be customized.
        /// <br></br>This property targets Windows 8.1 (to avoid overlapping).
        /// <br></br><br></br><br>- Wallpaper Tone is a feature by WinPaletter. It modifies images' HSL filter to alter wallpaper colors.</br>
        /// </summary>
        public WallpaperTone WallpaperTone_W81 { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Wallpaper Tone, and it can be customized.
        /// <br></br>This property targets Windows 8 (to avoid overlapping).
        /// <br></br><br></br><br>- Wallpaper Tone is a feature by WinPaletter. It modifies images' HSL filter to alter wallpaper colors.</br>
        /// </summary>
        public WallpaperTone WallpaperTone_W8 { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Wallpaper Tone, and it can be customized.
        /// <br></br>This property targets Windows 7 (to avoid overlapping).
        /// <br></br><br></br><br>- Wallpaper Tone is a feature by WinPaletter. It modifies images' HSL filter to alter wallpaper colors.</br>
        /// </summary>
        public WallpaperTone WallpaperTone_W7 { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Wallpaper Tone, and it can be customized.
        /// <br></br>This property targets Windows Vista (to avoid overlapping).
        /// <br></br><br></br><br>- Wallpaper Tone is a feature by WinPaletter. It modifies images' HSL filter to alter wallpaper colors.</br>
        /// </summary>
        public WallpaperTone WallpaperTone_WVista { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Wallpaper Tone, and it can be customized.
        /// <br></br>This property targets Windows XP (to avoid overlapping).
        /// <br></br><br></br><br>- Wallpaper Tone is a feature by WinPaletter. It modifies images' HSL filter to alter wallpaper colors.</br>
        /// </summary>
        public WallpaperTone WallpaperTone_WXP = new() { Image = $"{SysPaths.Windows}\\Web\\Wallpaper\\Bliss.bmp" };

        /// <summary>
        /// Structure instance that contains data about WinPaletter cursors, and it can be customized.
        /// </summary>
        public Cursors Cursors { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows metrics and fonts, and it can be customized.
        /// </summary>
        public MetricsFonts MetricsFonts { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows effects and animations, and it can be customized.
        /// </summary>
        public WinEffects WindowsEffects { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about screen saver, and it can be customized.
        /// </summary>
        public ScreenSaver ScreenSaver { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows sounds, and it can be customized.
        /// </summary>
        public Sounds Sounds { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows switcher (Alt+Tab) appearance, and it can be customized.
        /// </summary>
        public AltTab AltTab { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Windows icons, and it can be customized.
        /// </summary>
        public Icons Icons { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about Command Prompt, and it can be customized.
        /// </summary>
        public Console CommandPrompt { get; set; } = new();

        /// <summary>
        /// Structure instance that contains data about PowerShell x86, and it can be customized.
        /// </summary>
        public Console PowerShellx86 { get; set; } = new() { PopupForeground = 15, PopupBackground = 3, ScreenColorsForeground = 6, ScreenColorsBackground = 5 };

        /// <summary>
        /// Structure instance that contains data about PowerShell x64, and it can be customized.
        /// </summary>
        public Console PowerShellx64 { get; set; } = new() { PopupForeground = 15, PopupBackground = 3, ScreenColorsForeground = 6, ScreenColorsBackground = 5 };

        /// <summary>
        /// class that contains data about Windows Terminal Stable, and it can be customized.
        /// </summary>
        public WinTerminal Terminal { get; set; } = new();

        /// <summary>
        /// class that contains data about Windows Terminal Preview, and it can be customized.
        /// </summary>
        public WinTerminal TerminalPreview { get; set; } = new();
    }
}