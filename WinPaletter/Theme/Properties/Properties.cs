using System.Drawing;
using WinPaletter.Theme.Structures;

namespace WinPaletter.Theme
{
    public partial class Manager
    {
        /// <summary>Object derived of structure that has data about WinPaletter theme information.</summary>
        public Info Info = new()
        {
            AppVersion = Program.Version,
            ThemeName = Program.Lang.CurrentMode,
            Description = string.Empty,
            ExportResThemePack = false,
            License = string.Empty,
            ThemeVersion = "1.0.0.0",
            Author = User.UserName,
            AuthorSocialMediaLink = string.Empty,
            Color1 = Color.FromArgb(0, 102, 204),
            Color2 = Color.FromArgb(122, 9, 9),
            DesignedFor_Win11 = true,
            DesignedFor_Win10 = true,
            DesignedFor_Win81 = true,
            DesignedFor_Win7 = true,
            DesignedFor_WinVista = true,
            DesignedFor_WinXP = true,
            Pattern = 1
        };

        /// <summary>Object derived of structure that has data about WinPaletter theme, and it can be customized.</summary>
        public AppTheme AppTheme = new()
        {
            Enabled = false,
            BackColor = Color.FromArgb(25, 25, 25),
            AccentColor = Color.FromArgb(0, 81, 210),
            DarkMode = true,
            RoundCorners = OS.WXP || OS.WVista | OS.W7 || OS.W11 || OS.W12
        };

        /// <summary>Object derived of structure that has data about Windows 12 colors and appearance, and it can be customized.</summary>
        public Windows10x Windows12 = new()
        {
            Color_Index0 = Color.FromArgb(153, 235, 255),
            Color_Index1 = Color.FromArgb(76, 194, 255),
            Color_Index2 = Color.FromArgb(0, 145, 248),
            Color_Index3 = Color.FromArgb(0, 120, 212),
            Color_Index4 = Color.FromArgb(0, 103, 192),
            Color_Index5 = Color.FromArgb(0, 62, 146),
            Color_Index6 = Color.FromArgb(0, 26, 104),
            Color_Index7 = Color.FromArgb(247, 99, 12),
            Titlebar_Active = Color.FromArgb(0, 120, 212),
            Titlebar_Inactive = Color.FromArgb(32, 32, 32),
            StartMenu_Accent = Color.FromArgb(0, 103, 192),
            WinMode_Light = true,
            AppMode_Light = true,
            Transparency = true,
            ApplyAccentOnTitlebars = false,
            ApplyAccentOnTaskbar = Windows10x.AccentTaskbarLevels.None,
            IncreaseTBTransparency = false,
            TB_Blur = true
        };

        /// <summary>Object derived of structure that has data about Windows 11 colors and appearance, and it can be customized.</summary>
        public Windows10x Windows11 = new()
        {
            Color_Index0 = Color.FromArgb(153, 235, 255),
            Color_Index1 = Color.FromArgb(76, 194, 255),
            Color_Index2 = Color.FromArgb(0, 145, 248),
            Color_Index3 = Color.FromArgb(0, 120, 212),
            Color_Index4 = Color.FromArgb(0, 103, 192),
            Color_Index5 = Color.FromArgb(0, 62, 146),
            Color_Index6 = Color.FromArgb(0, 26, 104),
            Color_Index7 = Color.FromArgb(247, 99, 12),
            Titlebar_Active = Color.FromArgb(0, 120, 212),
            Titlebar_Inactive = Color.FromArgb(32, 32, 32),
            StartMenu_Accent = Color.FromArgb(0, 103, 192),
            WinMode_Light = true,
            AppMode_Light = true,
            Transparency = true,
            ApplyAccentOnTitlebars = false,
            ApplyAccentOnTaskbar = Windows10x.AccentTaskbarLevels.None,
            IncreaseTBTransparency = false,
            TB_Blur = true
        };

        /// <summary>Object derived of structure that has data about Windows 10 colors and appearance, and it can be customized.</summary>
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
            AppMode_Light = true,
            Transparency = true,
            ApplyAccentOnTitlebars = false,
            ApplyAccentOnTaskbar = Windows10x.AccentTaskbarLevels.None,
            IncreaseTBTransparency = false,
            TB_Blur = true
        };

        /// <summary>Object derived of structure that has data about Windows 8.1 colors and appearance, and it can be customized.</summary>
        public Windows8x Windows81 = new()
        {
            ColorizationColor = Color.FromArgb(246, 195, 74),
            ColorizationColorBalance = 78,
            Start = 0,
            StartColor = Color.FromArgb(30, 0, 84),
            AccentColor = Color.FromArgb(72, 29, 178),
            Theme = Windows7.Themes.Aero,
            LogonUI = 0,
            PersonalColors_Background = Color.FromArgb(30, 0, 84),
            PersonalColors_Accent = Color.FromArgb(72, 29, 178),
            NoLockScreen = false,
            LockScreenType = Structures.LogonUI7.Sources.Default,
            LockScreenSystemID = 0
        };

        /// <summary>Object derived of structure that has data about Windows 7 colors and appearance, and it can be customized.</summary>
        public Windows7 Windows7 = new()
        {
            ColorizationColor = Color.FromArgb(116, 184, 252),
            ColorizationAfterglow = Color.FromArgb(116, 184, 252),
            ColorizationColorBalance = 8,
            ColorizationAfterglowBalance = 43,
            ColorizationBlurBalance = 49,
            ColorizationGlassReflectionIntensity = 0,
            EnableAeroPeek = true,
            AlwaysHibernateThumbnails = false,
            Theme = Windows7.Themes.Aero
        };

        /// <summary>Object derived of structure that has data about Windows Vista colors and appearance, and it can be customized.</summary>
        public WindowsVista WindowsVista = new()
        {
            ColorizationColor = Color.FromArgb(64, 158, 254),
            Theme = Windows7.Themes.Aero
        };

        /// <summary>Object derived of structure that has data about Windows XP colors and appearance, and it can be customized.</summary>
        public WindowsXP WindowsXP = new()
        {
            Theme = WindowsXP.Themes.LunaBlue,
            ColorScheme = "NormalColor",
            ThemeFile = PathsExt.Windows + @"\resources\Themes\Luna\Luna.msstyles"
        };

        /// <summary>Object derived of structure that has data about classic Windows colors, and it can be customized.</summary>
        public Structures.Win32UI Win32 = new()
        {
            EnableTheming = true,
            EnableGradient = true,
            ActiveBorder = Color.FromArgb(180, 180, 180),
            ActiveTitle = Color.FromArgb(153, 180, 209),
            AppWorkspace = Color.FromArgb(171, 171, 171),
            Background = Color.FromArgb(0, 0, 0),
            ButtonAlternateFace = Color.FromArgb(0, 0, 0),
            ButtonDkShadow = Color.FromArgb(105, 105, 105),
            ButtonFace = Color.FromArgb(240, 240, 240),
            ButtonHilight = Color.FromArgb(255, 255, 255),
            ButtonLight = Color.FromArgb(227, 227, 227),
            ButtonShadow = Color.FromArgb(160, 160, 160),
            ButtonText = Color.FromArgb(0, 0, 0),
            GradientActiveTitle = Color.FromArgb(185, 209, 234),
            GradientInactiveTitle = Color.FromArgb(215, 228, 242),
            GrayText = Color.FromArgb(109, 109, 109),
            HilightText = Color.FromArgb(255, 255, 255),
            HotTrackingColor = Color.FromArgb(0, 102, 204),
            InactiveBorder = Color.FromArgb(244, 247, 252),
            InactiveTitle = Color.FromArgb(191, 205, 219),
            InactiveTitleText = Color.FromArgb(0, 0, 0),
            InfoText = Color.FromArgb(0, 0, 0),
            InfoWindow = Color.FromArgb(255, 255, 225),
            Menu = Color.FromArgb(240, 240, 240),
            MenuBar = Color.FromArgb(240, 240, 240),
            MenuText = Color.FromArgb(0, 0, 0),
            Scrollbar = Color.FromArgb(200, 200, 200),
            TitleText = Color.FromArgb(0, 0, 0),
            Window = Color.FromArgb(255, 255, 255),
            WindowFrame = Color.FromArgb(100, 100, 100),
            WindowText = Color.FromArgb(0, 0, 0),
            Hilight = Color.FromArgb(0, 120, 215),
            MenuHilight = Color.FromArgb(0, 120, 215),
            Desktop = Color.FromArgb(0, 0, 0)
        };

        /// <summary>Object derived of structure that has data about Windows 10/11 LogonUI appearance, and it can be customized.</summary>
        public LogonUI10x LogonUI10x = new()
        {
            DisableAcrylicBackgroundOnLogon = false,
            DisableLogonBackgroundImage = false,
            NoLockScreen = false
        };

        /// <summary>Object derived of structure that has data about Windows 7 LogonUI appearance, and it can be customized.</summary>
        public Structures.LogonUI7 LogonUI7 = new()
        {
            Enabled = false,
            Mode = Structures.LogonUI7.Sources.Default,
            ImagePath = @"C:\Windows\Web\Wallpaper\Windows\img0.jpg",
            Color = Color.Black,
            Blur = false,
            Blur_Intensity = 0,
            Grayscale = false,
            Noise = false,
            Noise_Mode = BitmapExtensions.NoiseMode.Acrylic,
            Noise_Intensity = 0
        };

        /// <summary>Object derived of structure that has data about Windows XP LogonUI appearance, and it can be customized.</summary>
        public Structures.LogonUIXP LogonUIXP = new()
        {
            Enabled = true,
            Mode = Structures.LogonUIXP.Modes.Default,
            BackColor = Color.Black,
            ShowMoreOptions = false
        };

        /// <summary>Object derived of structure that has data about wallpaper, and it can be customized.</summary>
        public Wallpaper Wallpaper = new()
        {
            Enabled = false,
            ImageFile = PathsExt.Windows + @"\Web\Wallpaper\Windows\img0.jpg",
            WallpaperType = Wallpaper.WallpaperTypes.Picture,
            WallpaperStyle = Wallpaper.WallpaperStyles.Fill,
            Wallpaper_Slideshow_Images = new string[] { },
            Wallpaper_Slideshow_ImagesRootPath = string.Empty,
            Wallpaper_Slideshow_Interval = 60000,
            Wallpaper_Slideshow_Shuffle = false,
            SlideShow_Folder_or_ImagesList = true
        };

        /// <summary>
        /// Structure that has data about Wallpaper Tone, and it can be customized.
        /// <br></br>This property is targeting Windows 12 (to avoid overlapping).
        /// <br></br><br></br><br>- Wallpaper Tone is a feature by WinPaletter. It modifies images HSL filter to alter wallpaper colors.</br>
        /// </summary>
        public WallpaperTone WallpaperTone_W12 = new()
        {
            Enabled = false,
            Image = PathsExt.Windows + @"\Web\Wallpaper\Windows\img0.jpg",
            H = 0,
            S = 100,
            L = 100
        };

        /// <summary>
        /// Structure that has data about Wallpaper Tone, and it can be customized.
        /// <br></br>This property is targeting Windows 11 (to avoid overlapping).
        /// <br></br><br></br><br>- Wallpaper Tone is a feature by WinPaletter. It modifies images HSL filter to alter wallpaper colors.</br>
        /// </summary>
        public WallpaperTone WallpaperTone_W11 = new()
        {
            Enabled = false,
            Image = PathsExt.Windows + @"\Web\Wallpaper\Windows\img0.jpg",
            H = 0,
            S = 100,
            L = 100
        };

        /// <summary>
        /// Structure that has data about Wallpaper Tone, and it can be customized.
        /// <br></br>This property is targeting Windows 10 (to avoid overlapping).
        /// <br></br><br></br><br>- Wallpaper Tone is a feature by WinPaletter. It modifies images HSL filter to alter wallpaper colors.</br>
        /// </summary>
        public WallpaperTone WallpaperTone_W10 = new()
        {
            Enabled = false,
            Image = PathsExt.Windows + @"\Web\Wallpaper\Windows\img0.jpg",
            H = 0,
            S = 100,
            L = 100
        };

        /// <summary>
        /// Structure that has data about Wallpaper Tone, and it can be customized.
        /// <br></br>This property is targeting Windows 8.1 (to avoid overlapping).
        /// <br></br><br></br><br>- Wallpaper Tone is a feature by WinPaletter. It modifies images HSL filter to alter wallpaper colors.</br>
        /// </summary>
        public WallpaperTone WallpaperTone_W81 = new()
        {
            Enabled = false,
            Image = PathsExt.Windows + @"\Web\Wallpaper\Windows\img0.jpg",
            H = 0,
            S = 100,
            L = 100
        };

        /// <summary>
        /// Structure that has data about Wallpaper Tone, and it can be customized.
        /// <br></br>This property is targeting Windows 7 (to avoid overlapping).
        /// <br></br><br></br><br>- Wallpaper Tone is a feature by WinPaletter. It modifies images HSL filter to alter wallpaper colors.</br>
        /// </summary>
        public WallpaperTone WallpaperTone_W7 = new()
        {
            Enabled = false,
            Image = PathsExt.Windows + @"\Web\Wallpaper\Windows\img0.jpg",
            H = 0,
            S = 100,
            L = 100
        };

        /// <summary>
        /// Structure that has data about Wallpaper Tone, and it can be customized.
        /// <br></br>This property is targeting Windows Vista (to avoid overlapping).
        /// <br></br><br></br><br>- Wallpaper Tone is a feature by WinPaletter. It modifies images HSL filter to alter wallpaper colors.</br>
        /// </summary>
        public WallpaperTone WallpaperTone_WVista = new()
        {
            Enabled = false,
            Image = PathsExt.Windows + @"\Web\Wallpaper\Windows\img0.jpg",
            H = 0,
            S = 100,
            L = 100
        };

        /// <summary>
        /// Structure that has data about Wallpaper Tone, and it can be customized.
        /// <br></br>This property is targeting Windows XP (to avoid overlapping).
        /// <br></br><br></br><br>- Wallpaper Tone is a feature by WinPaletter. It modifies images HSL filter to alter wallpaper colors.</br>
        /// </summary>
        public WallpaperTone WallpaperTone_WXP = new()
        {
            Enabled = false,
            Image = PathsExt.Windows + @"\Web\Wallpaper\Bliss.bmp",
            H = 0,
            S = 100,
            L = 100
        };

        /// <summary>Object derived of structure that has data about metrics and fonts, and it can be customized.</summary>
        public MetricsFonts MetricsFonts = new()
        {
            Enabled = Program.GetWindowsScreenScalingFactor() == 100d,
            BorderWidth = 1,
            CaptionHeight = 22,
            CaptionWidth = 22,
            IconSpacing = 75,
            IconVerticalSpacing = 75,
            MenuHeight = 19,
            MenuWidth = 19,
            PaddedBorderWidth = 4,
            ScrollHeight = 19,
            ScrollWidth = 19,
            SmCaptionHeight = 22,
            SmCaptionWidth = 22,
            DesktopIconSize = 48,
            ShellIconSize = 32,
            ShellSmallIconSize = 16,
            Fonts_SingleBitPP = false,
            CaptionFont = new("Segoe UI", 9f, FontStyle.Regular),
            IconFont = new("Segoe UI", 9f, FontStyle.Regular),
            MenuFont = new("Segoe UI", 9f, FontStyle.Regular),
            MessageFont = new("Segoe UI", 9f, FontStyle.Regular),
            SmCaptionFont = new("Segoe UI", 9f, FontStyle.Regular),
            StatusFont = new("Segoe UI", 9f, FontStyle.Regular),
            FontSubstitute_MSShellDlg = "Microsoft Sans Serif",
            FontSubstitute_MSShellDlg2 = "Tahoma",
            FontSubstitute_SegoeUI = string.Empty
        };

        /// <summary>Object derived of structure that has data about Windows effects and animations, and it can be customized.</summary>
        public WinEffects WindowsEffects = new()
        {
            Enabled = true,
            WindowAnimation = true,
            WindowShadow = true,
            WindowUIEffects = true,
            AnimateControlsInsideWindow = true,
            MenuAnimation = true,
            MenuSelectionFade = true,
            MenuFade = WinEffects.MenuAnimType.Fade,
            MenuShowDelay = 400U,
            ComboBoxAnimation = true,
            ListBoxSmoothScrolling = true,
            TooltipAnimation = true,
            TooltipFade = WinEffects.MenuAnimType.Fade,
            IconsShadow = true,
            IconsDesktopTranslSel = true,
            ShowWinContentDrag = true,
            BalloonNotifications = false,
            PaintDesktopVersion = false,
            ShowSecondsInSystemClock = false,
            Win11ClassicContextMenu = false,
            SysListView32 = false,
            SnapCursorToDefButton = false,
            ShakeToMinimize = true,
            NotificationDuration = 5,
            FocusRectWidth = 1U,
            FocusRectHeight = 1U,
            KeyboardUnderline = false,
            Caret = 1U,
            AWT_Enabled = false,
            AWT_Delay = 0,
            AWT_BringActivatedWindowToTop = false,
            Win11BootDots = !OS.W12 && !OS.W11,
            Win11ExplorerBar = WinEffects.ExplorerBar.Default,
            DisableNavBar = false,
            AutoHideScrollBars = true,
            ColorFilter_Enabled = false,
            ColorFilter = WinEffects.ColorFilters.Grayscale,
            ClassicVolMixer = false,
            FullScreenStartMenu = false
        };

        /// <summary>Object derived of structure that has data about screen saver, and it can be customized.</summary>
        public ScreenSaver ScreenSaver = new()
        {
            Enabled = false,
            File = string.Empty,
            IsSecure = false,
            TimeOut = 60
        };

        /// <summary>Object derived of structure that has data about Windows sounds, and it can be customized.</summary>
        public Sounds Sounds = new()
        {
            Enabled = true,
            Snd_Imageres_SystemStart = (OS.W12 || OS.W11) ? "Default" : string.Empty,
            Snd_ChargerConnected = string.Empty,
            Snd_ChargerDisconnected = string.Empty,
            Snd_Win_WindowsLock = string.Empty
        };

        /// <summary>Object derived of structure that has data about Windows switcher (Alt+Tab) appearance, and it can be customized.</summary>
        public AltTab AltTab = new()
        {
            Enabled = true,
            Style = AltTab.Styles.Default,
            Win10Opacity = 95
        };

        /// <summary>Object derived of structure that has data about Command Prompt, and it can be customized.</summary>
        public Structures.Console CommandPrompt = new()
        {
            Enabled = false,
            ColorTable00 = Color.FromArgb(12, 12, 12),
            ColorTable01 = Color.FromArgb(0, 55, 218),
            ColorTable02 = Color.FromArgb(19, 161, 14),
            ColorTable03 = Color.FromArgb(58, 150, 221),
            ColorTable04 = Color.FromArgb(197, 15, 31),
            ColorTable05 = Color.FromArgb(136, 23, 152),
            ColorTable06 = Color.FromArgb(193, 156, 0),
            ColorTable07 = Color.FromArgb(204, 204, 204),
            ColorTable08 = Color.FromArgb(118, 118, 118),
            ColorTable09 = Color.FromArgb(59, 120, 255),
            ColorTable10 = Color.FromArgb(22, 198, 12),
            ColorTable11 = Color.FromArgb(97, 214, 214),
            ColorTable12 = Color.FromArgb(231, 72, 86),
            ColorTable13 = Color.FromArgb(180, 0, 158),
            ColorTable14 = Color.FromArgb(249, 241, 165),
            ColorTable15 = Color.FromArgb(242, 242, 242),
            PopupForeground = 5,
            PopupBackground = 15,
            ScreenColorsForeground = 7,
            ScreenColorsBackground = 0,
            CursorSize = 19,
            FaceName = "Consolas",
            FontRaster = false,
            FontSize = 18 * 65536,
            FontWeight = 400,
            W10_1909_CursorType = 0,
            W10_1909_CursorColor = Color.White,
            W10_1909_ForceV2 = true,
            W10_1909_LineSelection = false,
            W10_1909_TerminalScrolling = false,
            W10_1909_WindowAlpha = 255
        };

        /// <summary>Object derived of structure that has data about PowerShell x86, and it can be customized.</summary>
        public Structures.Console PowerShellx86 = new()
        {
            Enabled = false,
            ColorTable00 = Color.FromArgb(12, 12, 12),
            ColorTable01 = Color.FromArgb(0, 55, 218),
            ColorTable02 = Color.FromArgb(19, 161, 14),
            ColorTable03 = Color.FromArgb(58, 150, 221),
            ColorTable04 = Color.FromArgb(197, 15, 31),
            ColorTable05 = Color.FromArgb(1, 36, 86),
            ColorTable06 = Color.FromArgb(238, 237, 240),
            ColorTable07 = Color.FromArgb(204, 204, 204),
            ColorTable08 = Color.FromArgb(118, 118, 118),
            ColorTable09 = Color.FromArgb(59, 120, 255),
            ColorTable10 = Color.FromArgb(22, 198, 12),
            ColorTable11 = Color.FromArgb(97, 214, 214),
            ColorTable12 = Color.FromArgb(231, 72, 86),
            ColorTable13 = Color.FromArgb(180, 0, 158),
            ColorTable14 = Color.FromArgb(249, 241, 165),
            ColorTable15 = Color.FromArgb(242, 242, 242),
            PopupForeground = 15,
            PopupBackground = 3,
            ScreenColorsForeground = 6,
            ScreenColorsBackground = 5,
            CursorSize = 19,
            FaceName = "Consolas",
            FontRaster = false,
            FontSize = 16 * 65536,
            FontWeight = 400,
            W10_1909_CursorType = 0,
            W10_1909_CursorColor = Color.White,
            W10_1909_ForceV2 = true,
            W10_1909_LineSelection = false,
            W10_1909_TerminalScrolling = false,
            W10_1909_WindowAlpha = 255
        };

        /// <summary>Object derived of structure that has data about PowerShell x64, and it can be customized.</summary>
        public Structures.Console PowerShellx64 = new()
        {
            Enabled = false,
            ColorTable00 = Color.FromArgb(12, 12, 12),
            ColorTable01 = Color.FromArgb(0, 55, 218),
            ColorTable02 = Color.FromArgb(19, 161, 14),
            ColorTable03 = Color.FromArgb(58, 150, 221),
            ColorTable04 = Color.FromArgb(197, 15, 31),
            ColorTable05 = Color.FromArgb(1, 36, 86),
            ColorTable06 = Color.FromArgb(238, 237, 240),
            ColorTable07 = Color.FromArgb(204, 204, 204),
            ColorTable08 = Color.FromArgb(118, 118, 118),
            ColorTable09 = Color.FromArgb(59, 120, 255),
            ColorTable10 = Color.FromArgb(22, 198, 12),
            ColorTable11 = Color.FromArgb(97, 214, 214),
            ColorTable12 = Color.FromArgb(231, 72, 86),
            ColorTable13 = Color.FromArgb(180, 0, 158),
            ColorTable14 = Color.FromArgb(249, 241, 165),
            ColorTable15 = Color.FromArgb(242, 242, 242),
            PopupForeground = 15,
            PopupBackground = 3,
            ScreenColorsForeground = 6,
            ScreenColorsBackground = 5,
            CursorSize = 19,
            FaceName = "Consolas",
            FontRaster = false,
            FontSize = 16 * 65536,
            FontWeight = 400,
            W10_1909_CursorType = 0,
            W10_1909_CursorColor = Color.White,
            W10_1909_ForceV2 = true,
            W10_1909_LineSelection = false,
            W10_1909_TerminalScrolling = false,
            W10_1909_WindowAlpha = 255
        };

        /// <summary>Object derived of class that has data about Windows Terminal Stable, and it can be customized.</summary>
        public WinTerminal Terminal = new(string.Empty, WinTerminal.Mode.Empty);

        /// <summary>Object derived of class that has data about Windows Terminal Preview, and it can be customized.</summary>
        public WinTerminal TerminalPreview = new(string.Empty, WinTerminal.Mode.Empty);
    }
}