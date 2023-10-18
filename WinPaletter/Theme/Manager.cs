using Microsoft.VisualBasic;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.Theme.Structures;
using static WinPaletter.NativeMethods.User32;

namespace WinPaletter.Theme
{

    /// <summary>
    /// This class is responsible for managing WinPaletter theme
    /// </summary>

    public class Manager : IDisposable, ICloneable
    {

        #region Variables
        private bool _ErrorHappened = false;
        private readonly BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
        private readonly Converter _Converter = new Converter();
        public enum Source
        {
            Registry,
            File,
            Empty
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
            }
            disposedValue = true;
        }

        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Clone support
        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion

        #region Properties
        public Info Info = new()
        {
            AppVersion = Program.AppVersion,
            ThemeName = Program.Lang.CurrentMode,
            Description = "",
            ExportResThemePack = false,
            License = "",
            ThemeVersion = "1.0.0.0",
            Author = Environment.UserName,
            AuthorSocialMediaLink = "",
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

        public AppTheme AppTheme = new()
        {
            Enabled = false,
            BackColor = Color.FromArgb(25, 25, 25),
            AccentColor = Color.FromArgb(0, 81, 210),
            DarkMode = true,
            RoundCorners = Program.WXP | Program.WVista | Program.W7 | Program.W11
        };

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
            LockScreenType = Theme.Structures.LogonUI7.Modes.Default,
            LockScreenSystemID = 0
        };

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

        public WindowsVista WindowsVista = new()
        {
            ColorizationColor = Color.FromArgb(64, 158, 254),
            Theme = Windows7.Themes.Aero
        };

        public WindowsXP WindowsXP = new()
        {
            Theme = WindowsXP.Themes.LunaBlue,
            ColorScheme = "NormalColor",
            ThemeFile = Program.PATH_Windows + @"\resources\Themes\Luna\Luna.msstyles"
        };

        public Theme.Structures.Win32UI Win32 = new()
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

        public LogonUI10x LogonUI10x = new()
        {
            DisableAcrylicBackgroundOnLogon = false,
            DisableLogonBackgroundImage = false,
            NoLockScreen = false
        };

        public Theme.Structures.LogonUI7 LogonUI7 = new()
        {
            Enabled = false,
            Mode = Theme.Structures.LogonUI7.Modes.Default,
            ImagePath = @"C:\Windows\Web\Wallpaper\Windows\img0.jpg",
            Color = Color.Black,
            Blur = false,
            Blur_Intensity = 0,
            Grayscale = false,
            Noise = false,
            Noise_Mode = BitmapExtensions.NoiseMode.Acrylic,
            Noise_Intensity = 0
        };

        public Theme.Structures.LogonUIXP LogonUIXP = new()
        {
            Enabled = true,
            Mode = Theme.Structures.LogonUIXP.Modes.Default,
            BackColor = Color.Black,
            ShowMoreOptions = false
        };

        public Wallpaper Wallpaper = new()
        {
            Enabled = false,
            ImageFile = Program.PATH_Windows + @"\Web\Wallpaper\Windows\img0.jpg",
            WallpaperType = Wallpaper.WallpaperTypes.Picture,
            WallpaperStyle = Wallpaper.WallpaperStyles.Fill,
            Wallpaper_Slideshow_Images = new string[] { },
            Wallpaper_Slideshow_ImagesRootPath = "",
            Wallpaper_Slideshow_Interval = 60000,
            Wallpaper_Slideshow_Shuffle = false,
            SlideShow_Folder_or_ImagesList = true
        };

        public WallpaperTone WallpaperTone_W11 = new()
        {
            Enabled = false,
            Image = Program.PATH_Windows + @"\Web\Wallpaper\Windows\img0.jpg",
            H = 0,
            S = 100,
            L = 100
        };

        public WallpaperTone WallpaperTone_W10 = new()
        {
            Enabled = false,
            Image = Program.PATH_Windows + @"\Web\Wallpaper\Windows\img0.jpg",
            H = 0,
            S = 100,
            L = 100
        };

        public WallpaperTone WallpaperTone_W81 = new()
        {
            Enabled = false,
            Image = Program.PATH_Windows + @"\Web\Wallpaper\Windows\img0.jpg",
            H = 0,
            S = 100,
            L = 100
        };

        public WallpaperTone WallpaperTone_W7 = new()
        {
            Enabled = false,
            Image = Program.PATH_Windows + @"\Web\Wallpaper\Windows\img0.jpg",
            H = 0,
            S = 100,
            L = 100
        };

        public WallpaperTone WallpaperTone_WVista = new()
        {
            Enabled = false,
            Image = Program.PATH_Windows + @"\Web\Wallpaper\Windows\img0.jpg",
            H = 0,
            S = 100,
            L = 100
        };

        public WallpaperTone WallpaperTone_WXP = new()
        {
            Enabled = false,
            Image = Program.PATH_Windows + @"\Web\Wallpaper\Bliss.bmp",
            H = 0,
            S = 100,
            L = 100
        };

        public MetricsFonts MetricsFonts = new()
        {
            Enabled = GetWindowsScreenScalingFactor() == 100d,
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
            CaptionFont = new Font("Segoe UI", 9f, FontStyle.Regular),
            IconFont = new Font("Segoe UI", 9f, FontStyle.Regular),
            MenuFont = new Font("Segoe UI", 9f, FontStyle.Regular),
            MessageFont = new Font("Segoe UI", 9f, FontStyle.Regular),
            SmCaptionFont = new Font("Segoe UI", 9f, FontStyle.Regular),
            StatusFont = new Font("Segoe UI", 9f, FontStyle.Regular),
            FontSubstitute_MSShellDlg = "Microsoft Sans Serif",
            FontSubstitute_MSShellDlg2 = "Tahoma",
            FontSubstitute_SegoeUI = ""
        };

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
            Win11BootDots = !Program.W11,
            Win11ExplorerBar = WinEffects.ExplorerBar.Default,
            DisableNavBar = false,
            AutoHideScrollBars = true,
            ColorFilter_Enabled = false,
            ColorFilter = WinEffects.ColorFilters.Grayscale,
            ClassicVolMixer = false,
            FullScreenStartMenu = false
        };

        public ScreenSaver ScreenSaver = new()
        {
            Enabled = false,
            File = "",
            IsSecure = false,
            TimeOut = 60
        };

        public Sounds Sounds = new()
        {
            Enabled = true,
            Snd_Imageres_SystemStart = Program.W11 ? "Default" : "",
            Snd_Win_SystemExit_TaskMgmt = !Program.WXP & !Program.WVista & !Program.W7,
            Snd_Win_WindowsLogoff_TaskMgmt = !Program.WXP & !Program.WVista & !Program.W7,
            Snd_Win_WindowsLogon_TaskMgmt = !Program.WXP & !Program.WVista & !Program.W7,
            Snd_Win_WindowsUnlock_TaskMgmt = !Program.WXP & !Program.WVista & !Program.W7,
            Snd_ChargerConnected = ""
        };

        public AltTab AltTab = new()
        {
            Enabled = true,
            Style = AltTab.Styles.Default,
            Win10Opacity = 95
        };

        public Theme.Structures.Console CommandPrompt = new()
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

        public Theme.Structures.Console PowerShellx86 = new()
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

        public Theme.Structures.Console PowerShellx64 = new()
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

        public WinTerminal Terminal = new WinTerminal("", WinTerminal.Mode.Empty);

        public WinTerminal TerminalPreview = new WinTerminal("", WinTerminal.Mode.Empty);

        #region Cursors
        public bool Cursor_Enabled = false;

        public bool Cursor_Shadow = false;

        public bool Cursor_Sonar = false;

        public int Cursor_Trails = 0;

        public Theme.Structures.Cursor Cursor_Arrow = new()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Theme.Structures.Cursor Cursor_AppLoading = new()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Circle,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Theme.Structures.Cursor Cursor_Busy = new()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Circle,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Circle,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Theme.Structures.Cursor Cursor_Help = new()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Theme.Structures.Cursor Cursor_Move = new()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Theme.Structures.Cursor Cursor_NS = new()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Theme.Structures.Cursor Cursor_EW = new()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Theme.Structures.Cursor Cursor_NESW = new()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Theme.Structures.Cursor Cursor_NWSE = new()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Theme.Structures.Cursor Cursor_Up = new()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Theme.Structures.Cursor Cursor_Pen = new()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Theme.Structures.Cursor Cursor_None = new()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.FromArgb(255, 0, 0),
            SecondaryColor2 = Color.FromArgb(255, 0, 0),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Theme.Structures.Cursor Cursor_Link = new()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Theme.Structures.Cursor Cursor_Pin = new()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Theme.Structures.Cursor Cursor_Person = new()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Theme.Structures.Cursor Cursor_IBeam = new()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };

        public Theme.Structures.Cursor Cursor_Cross = new()
        {
            PrimaryColor1 = Color.White,
            PrimaryColor2 = Color.White,
            PrimaryColorGradient = false,
            PrimaryColorGradientMode = Paths.GradientMode.Vertical,
            PrimaryColorNoise = false,
            PrimaryColorNoiseOpacity = 0.25f,
            SecondaryColor1 = Color.Black,
            SecondaryColor2 = Color.FromArgb(64, 65, 75),
            SecondaryColorGradient = true,
            SecondaryColorGradientMode = Paths.GradientMode.Vertical,
            SecondaryColorNoise = false,
            SecondaryColorNoiseOpacity = 0.25f,
            LoadingCircleBack1 = Color.FromArgb(42, 151, 243),
            LoadingCircleBack2 = Color.FromArgb(42, 151, 243),
            LoadingCircleBackGradient = false,
            LoadingCircleBackGradientMode = Paths.GradientMode.Circle,
            LoadingCircleBackNoise = false,
            LoadingCircleBackNoiseOpacity = 0.25f,
            LoadingCircleHot1 = Color.FromArgb(37, 204, 255),
            LoadingCircleHot2 = Color.FromArgb(37, 204, 255),
            LoadingCircleHotGradient = false,
            LoadingCircleHotGradientMode = Paths.GradientMode.Circle,
            LoadingCircleHotNoise = false,
            LoadingCircleHotNoiseOpacity = 0.25f,
            ArrowStyle = Paths.ArrowStyle.Aero,
            CircleStyle = Paths.CircleStyle.Aero,
            Shadow_Enabled = false,
            Shadow_Color = Color.Black,
            Shadow_Blur = 5,
            Shadow_Opacity = 0.3f,
            Shadow_OffsetX = 2,
            Shadow_OffsetY = 2
        };
        #endregion

        #endregion

        #region Functions
        public static List<Color> GetPaletteFromMSTheme(string Filename)
        {
            if (System.IO.File.Exists(Filename))
            {

                var ls = new List<Color>();
                ls.Clear();

                var tx = File.ReadAllText(Filename).CList();

                foreach (string x in tx)
                {
                    try
                    {
                        if (x.Contains("="))
                        {
                            if (x.Split('=')[1].Contains(" "))
                            {
                                if (x.Split('=')[1].Split(' ').Count() == 3)
                                {
                                    string c = x.Split('=')[1];
                                    bool inx = true;
                                    foreach (var u in c.Split(' '))
                                    {
                                        if (!u.All(char.IsDigit))
                                            inx = false;
                                    }
                                    if (inx)
                                        ls.Add(c.FromWin32RegToColor());
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                ls = ls.Distinct().ToList();
                ls.Sort(new RGBColorComparer());
                return ls;
            }
            else
            {
                return null;
            }
        }
        public static List<Color> GetPaletteFromString(string String, string ThemeName)
        {

            if (string.IsNullOrWhiteSpace(String))
            {
                return null;
                return default;
            }

            if (!String.Contains("|"))
            {
                return null;
                return default;
            }

            if (string.IsNullOrWhiteSpace(ThemeName))
            {
                return null;
                return default;
            }

            var ls = new List<Color>();
            ls.Clear();

            var AllThemes = String.CList();
            string SelectedTheme = "";
            bool Found = false;

            foreach (string th in AllThemes)
            {
                if ((th.Split('|')[0].ToLower() ?? "") == (ThemeName.ToLower() ?? ""))
                {
                    SelectedTheme = th.Replace("|", "\r\n");
                    Found = true;
                    break;
                }
            }

            if (!Found)
            {
                return null;
                return default;
            }

            var SelectedThemeList = SelectedTheme.CList();

            foreach (string x in SelectedThemeList)
            {
                try
                {
                    if (x.Contains("="))
                    {
                        if (x.Split('=')[1].Contains(" "))
                        {
                            if (x.Split('=')[1].Split(' ').Count() == 3)
                            {
                                string c = x.Split('=')[1];
                                bool inx = true;
                                foreach (var u in c.Split(' '))
                                {
                                    if (!u.All(char.IsDigit))
                                        inx = false;
                                }
                                if (inx)
                                    ls.Add(Color.FromArgb(255, Convert.ToInt32(c.Split(' ')[0]), Convert.ToInt32(c.Split(' ')[1]), Convert.ToInt32(c.Split(' ')[2])));
                            }
                        }
                    }
                }
                catch
                {
                }
            }

            ls = ls.Distinct().ToList();
            ls.Sort(new RGBColorComparer());
            return ls;
        }
        public List<Color> ListColors(bool DontMergeRepeatedColors = false)
        {

            var CL = new List<Color>();
            CL.Clear();

            foreach (var field in typeof(Windows10x).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType.Name.ToLower() == "color")
                {
                    CL.Add((Color)field.GetValue(Windows11));
                    CL.Add((Color)field.GetValue(Windows10));
                }
            }

            foreach (var field in typeof(LogonUI10x).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType.Name.ToLower() == "color")
                {
                    CL.Add((Color)field.GetValue(LogonUI10x));
                }
            }

            foreach (var field in typeof(Windows8x).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType.Name.ToLower() == "color")
                {
                    CL.Add((Color)field.GetValue(Windows81));
                }
            }

            foreach (var field in typeof(Windows7).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType.Name.ToLower() == "color")
                {
                    CL.Add((Color)field.GetValue(Windows7));
                }
            }

            foreach (var field in typeof(WindowsVista).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType.Name.ToLower() == "color")
                {
                    CL.Add((Color)field.GetValue(WindowsVista));
                }
            }

            foreach (var field in typeof(WindowsXP).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType.Name.ToLower() == "color")
                {
                    CL.Add((Color)field.GetValue(WindowsXP));
                }
            }

            foreach (var field in typeof(Theme.Structures.LogonUI7).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType.Name.ToLower() == "color")
                {
                    CL.Add((Color)field.GetValue(LogonUI7));
                }
            }

            foreach (var field in typeof(Theme.Structures.LogonUIXP).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType.Name.ToLower() == "color")
                {
                    CL.Add((Color)field.GetValue(LogonUIXP));
                }
            }

            foreach (var field in typeof(Theme.Structures.Win32UI).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType.Name.ToLower() == "color")
                {
                    CL.Add((Color)field.GetValue(Win32));
                }
            }

            foreach (var field in typeof(WallpaperTone).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType.Name.ToLower() == "color")
                {
                    CL.Add((Color)field.GetValue(WallpaperTone_W11));
                    CL.Add((Color)field.GetValue(WallpaperTone_W10));
                    CL.Add((Color)field.GetValue(WallpaperTone_W81));
                    CL.Add((Color)field.GetValue(WallpaperTone_W7));
                    CL.Add((Color)field.GetValue(WallpaperTone_WVista));
                    CL.Add((Color)field.GetValue(WallpaperTone_WXP));
                }
            }

            foreach (var field in typeof(Theme.Structures.Console).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType.Name.ToLower() == "color")
                {
                    CL.Add((Color)field.GetValue(CommandPrompt));
                    CL.Add((Color)field.GetValue(PowerShellx86));
                    CL.Add((Color)field.GetValue(PowerShellx64));
                }
            }

            foreach (var c in Terminal.Colors)
            {
                CL.Add(c.Background);
                CL.Add(c.Foreground);
                CL.Add(c.SelectionBackground);
                CL.Add(c.Black);
                CL.Add(c.Blue);
                CL.Add(c.BrightBlack);
                CL.Add(c.BrightBlue);
                CL.Add(c.BrightCyan);
                CL.Add(c.BrightGreen);
                CL.Add(c.BrightPurple);
                CL.Add(c.BrightRed);
                CL.Add(c.BrightWhite);
                CL.Add(c.BrightYellow);
                CL.Add(c.CursorColor);
                CL.Add(c.Cyan);
                CL.Add(c.Green);
                CL.Add(c.Purple);
                CL.Add(c.Red);
                CL.Add(c.White);
                CL.Add(c.Yellow);
            }

            foreach (var c in TerminalPreview.Colors)
            {
                CL.Add(c.Background);
                CL.Add(c.Foreground);
                CL.Add(c.SelectionBackground);
                CL.Add(c.Black);
                CL.Add(c.Blue);
                CL.Add(c.BrightBlack);
                CL.Add(c.BrightBlue);
                CL.Add(c.BrightCyan);
                CL.Add(c.BrightGreen);
                CL.Add(c.BrightPurple);
                CL.Add(c.BrightRed);
                CL.Add(c.BrightWhite);
                CL.Add(c.BrightYellow);
                CL.Add(c.CursorColor);
                CL.Add(c.Cyan);
                CL.Add(c.Green);
                CL.Add(c.Purple);
                CL.Add(c.Red);
                CL.Add(c.White);
                CL.Add(c.Yellow);
            }

            foreach (var c in Terminal.Themes)
            {
                CL.Add(c.Titlebar_Inactive);
                CL.Add(c.Titlebar_Active);
                CL.Add(c.Tab_Active);
                CL.Add(c.Tab_Inactive);
            }

            foreach (var c in TerminalPreview.Themes)
            {
                CL.Add(c.Titlebar_Inactive);
                CL.Add(c.Titlebar_Active);
                CL.Add(c.Tab_Active);
                CL.Add(c.Tab_Inactive);
            }

            foreach (var c in Terminal.Profiles)
                CL.Add(c.TabColor);

            foreach (var c in TerminalPreview.Profiles)
                CL.Add(c.TabColor);

            CL.Add(Terminal.DefaultProf.TabColor);
            CL.Add(TerminalPreview.DefaultProf.TabColor);

            foreach (var field in typeof(Theme.Structures.Cursor).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (field.FieldType.Name.ToLower() == "color")
                {
                    CL.Add((Color)field.GetValue(Cursor_Arrow));
                    CL.Add((Color)field.GetValue(Cursor_Help));
                    CL.Add((Color)field.GetValue(Cursor_AppLoading));
                    CL.Add((Color)field.GetValue(Cursor_Busy));
                    CL.Add((Color)field.GetValue(Cursor_Pen));
                    CL.Add((Color)field.GetValue(Cursor_None));
                    CL.Add((Color)field.GetValue(Cursor_Move));
                    CL.Add((Color)field.GetValue(Cursor_Up));
                    CL.Add((Color)field.GetValue(Cursor_NS));
                    CL.Add((Color)field.GetValue(Cursor_EW));
                    CL.Add((Color)field.GetValue(Cursor_NESW));
                    CL.Add((Color)field.GetValue(Cursor_NWSE));
                    CL.Add((Color)field.GetValue(Cursor_Link));
                    CL.Add((Color)field.GetValue(Cursor_Pin));
                    CL.Add((Color)field.GetValue(Cursor_Person));
                    CL.Add((Color)field.GetValue(Cursor_IBeam));
                    CL.Add((Color)field.GetValue(Cursor_Cross));
                }
            }

            if (!DontMergeRepeatedColors)
                CL = CL.Distinct().ToList();

            CL.Sort(new RGBColorComparer());

            if (CL.Contains(Color.FromArgb(0, 0, 0, 0)))
            {
                while (CL.Contains(Color.FromArgb(0, 0, 0, 0)))
                    CL.Remove(Color.FromArgb(0, 0, 0, 0));
            }

            return CL;
        }
        public static bool IsFontInstalled(string fontName)
        {
            bool installed = IsFontInstalled(fontName, FontStyle.Regular);

            if (!installed)
            {
                installed = IsFontInstalled(fontName, FontStyle.Bold);
            }

            if (!installed)
            {
                installed = IsFontInstalled(fontName, FontStyle.Italic);
            }

            return installed;
        }
        public static bool IsFontInstalled(string fontName, FontStyle style)
        {
            bool installed = false;
            const float emSize = 8.0f;

            try
            {

                using (var testFont = new Font(fontName, emSize, style))
                {
                    installed = 0 == string.Compare(fontName, testFont.Name, StringComparison.InvariantCultureIgnoreCase);
                }
            }
            catch
            {
            }

            return installed;
        }
        public static void AddNode(TreeView TreeView, string Text, string ImageKey)
        {
            if (TreeView is not null)
            {
                if (TreeView.InvokeRequired)
                {

                    try
                    {
                        TreeView.Invoke(new MethodInvoker(() =>
                            {
                                {
                                    var temp = TreeView.Nodes.Add(Text);
                                    temp.ImageKey = ImageKey;
                                    temp.SelectedImageKey = ImageKey;
                                }
                                TreeView.SelectedNode = TreeView.Nodes[TreeView.Nodes.Count - 1];
                                //TreeView.Update();
                            }));
                    }
                    catch
                    {
                    }
                }

                else
                {

                    try
                    {
                        TreeView.Invoke(new MethodInvoker(() =>
                            {
                                {
                                    var temp = TreeView.Nodes.Add(Text);
                                    temp.ImageKey = ImageKey;
                                    temp.SelectedImageKey = ImageKey;
                                }
                                TreeView.SelectedNode = TreeView.Nodes[TreeView.Nodes.Count - 1];
                                //TreeView.Update();
                            }));
                    }
                    catch
                    {
                    }
                }

            }
        }
        private void AddException(string Label, Exception Exception)
        {
            Program.Saving_Exceptions.Add(new Tuple<string, Exception>(Label, Exception));
        }
        public void Execute(MethodInvoker Sub, TreeView TreeView = null, string StartStr = "", string ErrorStr = "", string TimeStr = "", Stopwatch overallStopwatch = null, bool Skip = false, string SkipStr = "", bool ExecuteEvenIfSkip = false)
        {

            bool ReportProgress = TreeView is not null;
            var sw = new Stopwatch();
            sw.Reset();
            sw.Stop();
            sw.Start();

            if (!Skip | ExecuteEvenIfSkip)
            {
                if (!ExecuteEvenIfSkip)
                {
                    if (!string.IsNullOrWhiteSpace(StartStr))
                        AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), StartStr), "apply");
                }
                else if (!string.IsNullOrWhiteSpace(ErrorStr))
                    AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), SkipStr), "skip");

                try
                {
                    Sub();
                    if (ReportProgress & !string.IsNullOrWhiteSpace(TimeStr))
                        AddNode(TreeView, string.Format(TimeStr, sw.ElapsedMilliseconds / 1000d), "time");
                }
                catch (Exception ex)
                {
                    sw.Stop();
                    overallStopwatch.Stop();
                    _ErrorHappened = true;
                    if (ReportProgress)
                    {
                        if (!string.IsNullOrWhiteSpace(ErrorStr))
                            AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), ErrorStr), "error");
                        AddException(ErrorStr, ex);
                    }
                    else
                    {
                        Forms.BugReport.ThrowError(ex);
                    }
                    sw.Start();
                    overallStopwatch.Start();
                }
            }
            else if (!string.IsNullOrWhiteSpace(ErrorStr))
                AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), SkipStr), "skip");

            sw.Stop();
        }
        #endregion

        #region Theme Handling (Loading/Applying)
        public Manager(Source Source, string File = "", bool IgnoreExtractionThemePack = false)
        {

            switch (Source)
            {
                case Source.Registry:
                    {

                        using (var _Def = Theme.Default.From(Program.PreviewStyle))
                        {
                            Program.Loading_Exceptions.Clear();

                            #region Registry
                            Info.Load();
                            Windows11.Load(new Theme.Default().Windows11().Windows11, new Theme.Default().Default_Windows11Accents_Bytes);
                            Windows10.Load(new Theme.Default().Windows10().Windows10, new Theme.Default().Default_Windows10Accents_Bytes);
                            Windows81.Load(_Def.Windows81);
                            Windows7.Load(_Def.Windows7);
                            WindowsVista.Load(_Def.WindowsVista);
                            WindowsXP.Load(_Def.WindowsXP);
                            WindowsEffects.Load(_Def.WindowsEffects);
                            LogonUI10x.Load(_Def.LogonUI10x);
                            LogonUI7.Load(_Def.LogonUI7);
                            LogonUIXP.Load(_Def.LogonUIXP);
                            Win32.Load();
                            MetricsFonts.Load(_Def.MetricsFonts);
                            AltTab.Load(_Def.AltTab);
                            ScreenSaver.Load(_Def.ScreenSaver);
                            Sounds.Load(_Def.Sounds);
                            AppTheme.Load(_Def.AppTheme);

                            WallpaperTone_W11.Load("Win11");
                            WallpaperTone_W10.Load("Win10");
                            WallpaperTone_W81.Load("Win8.1");
                            WallpaperTone_W7.Load("Win7");
                            WallpaperTone_WVista.Load("WinVista");
                            WallpaperTone_WXP.Load("WinXP");
                            Wallpaper.Load(_Def.Wallpaper);

                            CommandPrompt.Load("", "Terminal_CMD_Enabled", _Def.CommandPrompt);
                            if (Directory.Exists(Program.PATH_PS86_app))
                            {
                                try
                                {
                                    Registry.CurrentUser.CreateSubKey(@"Console\" + Program.PATH_PS86_reg, true).Close();
                                }
                                catch
                                {
                                }
                                PowerShellx86.Load(Program.PATH_PS86_reg, "Terminal_PS_32_Enabled", _Def.PowerShellx86);
                            }
                            else
                            {
                                PowerShellx86 = _Def.PowerShellx86;
                            }
                            if (Directory.Exists(Program.PATH_PS64_app))
                            {
                                try
                                {
                                    Registry.CurrentUser.CreateSubKey(@"Console\" + Program.PATH_PS64_reg, true).Close();
                                }
                                catch
                                {
                                }
                                PowerShellx64.Load(Program.PATH_PS64_reg, "Terminal_PS_64_Enabled", _Def.PowerShellx64);
                            }
                            else
                            {
                                PowerShellx64 = _Def.PowerShellx64;
                            }


                            #region Windows Terminal
                            Terminal.Enabled = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_Stable_Enabled", 0)).ToBoolean();
                            TerminalPreview.Enabled = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_Preview_Enabled", 0)).ToBoolean();

                            if (Program.W10 | Program.W11)
                            {
                                string TerDir;
                                string TerPreDir;

                                if (!Program.Settings.WindowsTerminals.Path_Deflection)
                                {
                                    TerDir = Program.PATH_TerminalJSON;
                                    TerPreDir = Program.PATH_TerminalPreviewJSON;
                                }
                                else
                                {
                                    if (System.IO.File.Exists(Program.Settings.WindowsTerminals.Terminal_Stable_Path))
                                    {
                                        TerDir = Program.Settings.WindowsTerminals.Terminal_Stable_Path;
                                    }
                                    else
                                    {
                                        TerDir = Program.PATH_TerminalJSON;
                                    }

                                    if (System.IO.File.Exists(Program.Settings.WindowsTerminals.Terminal_Preview_Path))
                                    {
                                        TerPreDir = Program.Settings.WindowsTerminals.Terminal_Preview_Path;
                                    }
                                    else
                                    {
                                        TerPreDir = Program.PATH_TerminalPreviewJSON;
                                    }
                                }


                                if (System.IO.File.Exists(TerDir))
                                {
                                    Terminal = new WinTerminal(TerDir, WinTerminal.Mode.JSONFile);
                                }
                                else
                                {
                                    Terminal = new WinTerminal("", WinTerminal.Mode.Empty);
                                }

                                if (System.IO.File.Exists(TerPreDir))
                                {
                                    TerminalPreview = new WinTerminal(TerPreDir, WinTerminal.Mode.JSONFile, WinTerminal.Version.Preview);
                                }
                                else
                                {
                                    TerminalPreview = new WinTerminal("", WinTerminal.Mode.Empty, WinTerminal.Version.Preview);
                                }
                            }

                            else
                            {
                                Terminal = new WinTerminal("", WinTerminal.Mode.Empty);
                                TerminalPreview = new WinTerminal("", WinTerminal.Mode.Empty, WinTerminal.Version.Preview);
                            }
                            #endregion

                            #region Cursors
                            Cursor_Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors", "", false));

                            if (Fixer.SystemParametersInfo((int)SPI.Cursors.GETCURSORSHADOW, 0, ref Cursor_Shadow, (int)SPIF.None) == 0)
                                Cursor_Shadow = _Def.Cursor_Shadow;
                            if (Fixer.SystemParametersInfo((int)SPI.Cursors.GETMOUSETRAILS, 0, ref Cursor_Trails, (int)SPIF.None) == 0)
                                Cursor_Trails = _Def.Cursor_Trails;
                            if (Fixer.SystemParametersInfo((int)SPI.Cursors.GETMOUSESONAR, 0, ref Cursor_Sonar, (int)SPIF.None) == 0)
                                Cursor_Sonar = _Def.Cursor_Sonar;

                            Cursor_Arrow.Load("Arrow");
                            Cursor_Help.Load("Help");
                            Cursor_AppLoading.Load("AppLoading");
                            Cursor_Busy.Load("Busy");
                            Cursor_Move.Load("Move");
                            Cursor_NS.Load("NS");
                            Cursor_EW.Load("EW");
                            Cursor_NESW.Load("NESW");
                            Cursor_NWSE.Load("NWSE");
                            Cursor_Up.Load("Up");
                            Cursor_Pen.Load("Pen");
                            Cursor_None.Load("None");
                            Cursor_Link.Load("Link");
                            Cursor_Pin.Load("Pin");
                            Cursor_Person.Load("Person");
                            Cursor_IBeam.Load("IBeam");
                            Cursor_Cross.Load("Cross");
                            #endregion

                            if (Program.Loading_Exceptions.Count > 0)
                            {
                                Forms.Saving_ex_list.ex_List = Program.Loading_Exceptions;
                                Forms.Saving_ex_list.ShowDialog();
                            }
                            #endregion
                        }

                        break;
                    }

                case Source.File:
                    {

                        #region File
                        using (var TMx = Theme.Default.Get())
                        {
                            foreach (FieldInfo field in GetType().GetFields(bindingFlags))
                            {
                                var type = field.FieldType;
                                try
                                {
                                    field.SetValue(this, field.GetValue(TMx));
                                }
                                catch
                                {
                                }
                            }

                        Start:
                            ;


                            if (!System.IO.File.Exists(File))
                                return;

                            // Rough method to get theme name to create its proper resources pack folder
                            foreach (var line in Decompress(File))
                            {
                                if (line.Trim().StartsWith("\"ThemeName\":", Program._ignore))
                                {
                                    Info.ThemeName = line.Split(':')[1].ToString().Replace("\"", "").Replace(",", "").Trim();
                                    break;
                                }
                            }

                            var txt = new List<string>();
                            txt.Clear();
                            string Pack = new FileInfo(File).DirectoryName + @"\" + Path.GetFileNameWithoutExtension(File) + ".wptp";
                            bool Pack_IsValid = System.IO.File.Exists(Pack) && new FileInfo(Pack).Length > 0L && _Converter.FetchFile(File) == Converter_CP.WP_Format.JSON;
                            string cache = Program.PATH_ThemeResPackCache + @"\" + string.Concat(Info.ThemeName.Replace(" ", "").Split(Path.GetInvalidFileNameChars()));

                            // Extract theme resources pack
                            try
                            {
                                if (Pack_IsValid & !IgnoreExtractionThemePack)
                                {
                                    if (!Directory.Exists(cache))
                                        Directory.CreateDirectory(cache);

                                    using (var s = new FileStream(Pack, FileMode.Open, FileAccess.Read))
                                    {
                                        using (var archive = new ZipArchive(s, ZipArchiveMode.Read))
                                        {
                                            foreach (ZipArchiveEntry entry in archive.Entries)
                                            {
                                                if (entry.FullName.Contains(@"\"))
                                                {
                                                    string dest = Path.Combine(cache, entry.FullName);
                                                    string dest_dir = dest.Replace(@"\" + dest.Split('\\').Last(), "");
                                                    if (!Directory.Exists(dest_dir))
                                                        Directory.CreateDirectory(dest_dir);
                                                }
                                                entry.ExtractToFile(Path.Combine(cache, entry.FullName), true);
                                            }
                                        }

                                        s.Close();
                                        s.Dispose();
                                    }

                                }
                            }

                            catch (Exception ex)
                            {
                                Pack_IsValid = false;
                                Forms.BugReport.ThrowError(ex);
                            }

                            txt = (List<string>)Decompress(File);

                            if (IsValidJson(string.Join("\r\n", txt)))
                            {

                                // Replace %WinPaletterAppData% variable with a valid AppData folder path
                                for (int x = 0, loopTo = txt.Count - 1; x <= loopTo; x++)
                                {
                                    if (txt[x].Contains(":"))
                                    {
                                        string[] arr = txt[x].Split(':');
                                        if (arr.Count() == 2 && arr[1].Contains("%WinPaletterAppData%"))
                                        {
                                            txt[x] = arr[0] + ":" + arr[1].Replace("%WinPaletterAppData%", Program.PATH_appData.Replace(@"\", @"\\"));
                                        }
                                    }
                                }

                                JObject J = JObject.Parse(string.Join("\r\n", txt));

                                // This will get the new added features to fix bug (null values on opening a theme file)
                                try
                                {
                                    JObject J_Original = JObject.Parse(TMx.ToString(true));
                                    foreach (var item in J_Original)
                                    {
                                        if (J[item.Key] is null && J_Original[item.Key] is not null)
                                            J[item.Key] = J_Original[item.Key];
                                        if (!(item.Value is JValue))
                                        {
                                            foreach (KeyValuePair<string, JToken> prop in (JObject)item.Value)
                                            {
                                                try
                                                {
                                                    if (J[item.Key][prop.Key] is null && J_Original[item.Key] is not null && J_Original[item.Key][prop.Key] is not null)
                                                    {
                                                        J[item.Key][prop.Key] = J_Original[item.Key][prop.Key];
                                                    }
                                                }
                                                catch
                                                {
                                                }
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                }

                                foreach (FieldInfo field in GetType().GetFields(bindingFlags))
                                {
                                    var type = field.FieldType;
                                    var JSet = new JsonSerializerSettings();

                                    if (J[field.Name] is not null)
                                    {
                                        field.SetValue(this, J[field.Name].ToObject(type));
                                    }
                                }
                            }

                            else if (_Converter.FetchFile(File) == Converter_CP.WP_Format.WPTH)
                            {
                                if (MsgBox(Program.Lang.Convert_Detect_Old_OnLoading0, MessageBoxButtons.YesNo, MessageBoxIcon.Question, Program.Lang.Convert_Detect_Old_OnLoading1, "", "", "", "", Program.Lang.Convert_Detect_Old_OnLoading2, Ookii.Dialogs.WinForms.TaskDialogIcon.Information) == DialogResult.Yes)
                                {
                                    _Converter.Convert(File, File, Program.Settings.FileTypeManagement.CompressThemeFile, false);
                                    goto Start;
                                }
                            }
                            else
                            {
                                WPStyle.MsgBox(Program.Lang.Convert_Error_Phrasing, MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }

                        break;
                    }

                    #endregion

            }
        }

        public void Save(Source Destination, string File = "", TreeView TreeView = null, bool ResetToDefault = false)
        {

            switch (Destination)
            {
                case Source.Registry:
                    {

                        #region Registry
                        bool ReportProgress = Program.Settings.ThemeLog.VerboseLevel != WPSettings.Structures.ThemeLog.VerboseLevels.None && TreeView is not null;
                        bool ReportProgress_Detailed = ReportProgress && Program.Settings.ThemeLog.VerboseLevel == WPSettings.Structures.ThemeLog.VerboseLevels.Detailed;

                        _ErrorHappened = false;

                        var sw_all = new Stopwatch();
                        sw_all.Reset();
                        sw_all.Start();


                        if (ReportProgress)
                        {
                            Program.Saving_Exceptions.Clear();
                            TreeView.Visible = false;
                            TreeView.Nodes.Clear();
                            TreeView.Visible = true;
                            string OS;
                            if (Program.W11)
                            {
                                OS = Program.Lang.OS_Win11;
                            }
                            else if (Program.W10)
                            {
                                OS = Program.Lang.OS_Win10;
                            }
                            else if (Program.W8)
                            {
                                OS = Program.Lang.OS_Win8;
                            }
                            else if (Program.W81)
                            {
                                OS = Program.Lang.OS_Win81;
                            }
                            else if (Program.W7)
                            {
                                OS = Program.Lang.OS_Win7;
                            }
                            else if (Program.WVista)
                            {
                                OS = Program.Lang.OS_WinVista;
                            }
                            else if (Program.WXP)
                            {
                                OS = Program.Lang.OS_WinXP;
                            }
                            else
                            {
                                OS = Program.Lang.OS_WinUndefined;
                            }

                            AddNode(TreeView, string.Format("{0}", string.Format(Program.Lang.TM_ApplyFrom, OS)), "info");

                            AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Applying_Started), "info");

                            if (!Program.isElevated)
                            {
                                AddNode(TreeView, string.Format("{0}", Program.Lang.TM_Admin_Msg0), "admin");
                                AddNode(TreeView, string.Format("{0}", Program.Lang.TM_Admin_Msg1), "admin");
                            }

                        }

                        // Reset to default Windows theme
                        if (ResetToDefault)
                        {






                            Execute(() => { using (var def = Theme.Default.Get()) { def.LogonUI10x.NoLockScreen = false; def.LogonUI7.Enabled = false; def.Windows81.NoLockScreen = false; def.LogonUIXP.Enabled = true; if (!Program.WXP) ResetCursorsToAero(); else ResetCursorsToNone_XP(); def.CommandPrompt.Enabled = true; def.PowerShellx86.Enabled = true; def.PowerShellx64.Enabled = true; def.MetricsFonts.Enabled = true; def.WindowsEffects.Enabled = true; def.AltTab.Enabled = true; def.ScreenSaver.Enabled = true; def.Sounds.Enabled = true; def.AppTheme.Enabled = true; def.Wallpaper.Enabled = false; def.Save(Source.Registry); } }, TreeView, Program.Lang.TM_ThemeReset, Program.Lang.TM_ThemeReset_Error, Program.Lang.TM_Time, sw_all);
                        }

                        // Theme info
                        Execute(() => Info.Apply(ReportProgress_Detailed ? TreeView : null), TreeView, Program.Lang.TM_SavingInfo, Program.Lang.TM_SavingInfo_Error, Program.Lang.TM_Time, sw_all);

                        // WinPaletter application theme
                        Execute(() => AppTheme.Apply(ReportProgress_Detailed ? TreeView : null), TreeView, Program.Lang.TM_Applying_AppTheme, Program.Lang.TM_Error_AppTheme, Program.Lang.TM_Time, sw_all, !AppTheme.Enabled, Program.Lang.TM_Skip_AppTheme, true);

                        // Wallpaper
                        // Make Wallpaper before the following LogonUI items, to make a logonUI that depends on current wallpaper gets the correct file
                        this.Execute(new MethodInvoker(() => Wallpaper.Apply(false, ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_Wallpaper, Program.Lang.TM_Error_Wallpaper, Program.Lang.TM_Time, sw_all, !Wallpaper.Enabled, Program.Lang.TM_Skip_Wallpaper);

                        if (Program.W11)
                        {
                            this.Execute(new MethodInvoker(() => Windows11.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_Win11, Program.Lang.TM_W11_Error, Program.Lang.TM_Time, sw_all);

                            this.Execute(new MethodInvoker(() => LogonUI10x.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_LogonUI11, Program.Lang.TM_LogonUI11_Error, Program.Lang.TM_Time, sw_all);
                        }

                        if (Program.W10)
                        {
                            this.Execute(new MethodInvoker(() => Windows10.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_Win10, Program.Lang.TM_W10_Error, Program.Lang.TM_Time, sw_all);

                            this.Execute(new MethodInvoker(() => LogonUI10x.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_LogonUI10, Program.Lang.TM_LogonUI10_Error, Program.Lang.TM_Time, sw_all);
                        }

                        if (Program.W8 | Program.W81)
                        {
                            this.Execute(new MethodInvoker(() =>
                                    {
                                        Windows81.Apply(ReportProgress_Detailed ? TreeView : null);
                                        RefreshDWM(this);
                                    }), TreeView, Program.Lang.TM_Applying_Win81, Program.Lang.TM_W81_Error, Program.Lang.TM_Time, sw_all);


                            this.Execute(new MethodInvoker(() => Apply_LogonUI_8(TreeView)), TreeView, Program.Lang.TM_Applying_LogonUI8, Program.Lang.TM_LogonUI8_Error, Program.Lang.TM_Time, sw_all);
                        }

                        if (Program.W7)
                        {
                            this.Execute(new MethodInvoker(() =>
                                    {
                                        Windows7.Apply(ReportProgress_Detailed ? TreeView : null);
                                        RefreshDWM(this);
                                    }), TreeView, Program.Lang.TM_Applying_Win7, Program.Lang.TM_W7_Error, Program.Lang.TM_Time, sw_all);

                            this.Execute(new MethodInvoker(() => Apply_LogonUI7(LogonUI7, "LogonUI", TreeView)), TreeView, Program.Lang.TM_Applying_LogonUI7, Program.Lang.TM_LogonUI7_Error, Program.Lang.TM_Time, sw_all);
                        }

                        if (Program.WVista)
                        {
                            this.Execute(new MethodInvoker(() =>
                                    {
                                        WindowsVista.Apply(ReportProgress_Detailed ? TreeView : null);
                                        RefreshDWM(this);
                                    }), TreeView, Program.Lang.TM_Applying_WinVista, Program.Lang.TM_WVista_Error, Program.Lang.TM_Time, sw_all);
                        }

                        if (Program.WXP)
                        {
                            this.Execute(new MethodInvoker(() => WindowsXP.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_WinXP, Program.Lang.TM_WXP_Error, Program.Lang.TM_Time, sw_all);

                            this.Execute(new MethodInvoker(() => LogonUIXP.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_LogonUIXP, Program.Lang.TM_LogonUIXP_Error, Program.Lang.TM_Time, sw_all);
                        }

                        // Win32UI
                        this.Execute(new MethodInvoker(() => Win32.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_Win32UI, Program.Lang.TM_WIN32UI_Error, Program.Lang.TM_Time, sw_all);

                        // WindowsEffects
                        this.Execute(new MethodInvoker(() => WindowsEffects.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_WinEffects, Program.Lang.TM_WinEffects_Error, Program.Lang.TM_Time, sw_all);

                        // Metrics\Fonts
                        this.Execute(new MethodInvoker(() => MetricsFonts.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_Metrics, Program.Lang.TM_Error_Metrics, Program.Lang.TM_Time_They, sw_all, !MetricsFonts.Enabled, Program.Lang.TM_Skip_Metrics);

                        // AltTab
                        this.Execute(new MethodInvoker(() => AltTab.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_AltTab, Program.Lang.TM_Error_AltTab, Program.Lang.TM_Time, sw_all, !AltTab.Enabled, Program.Lang.TM_Skip_AltTab, true);

                        // WallpaperTone
                        this.Execute(new MethodInvoker(() =>
                                {
                                    WallpaperTone.Save_To_Registry(WallpaperTone_W11, "Win11", ReportProgress_Detailed ? TreeView : null);
                                    WallpaperTone.Save_To_Registry(WallpaperTone_W10, "Win10", ReportProgress_Detailed ? TreeView : null);
                                    WallpaperTone.Save_To_Registry(WallpaperTone_W81, "Win8.1", ReportProgress_Detailed ? TreeView : null);
                                    WallpaperTone.Save_To_Registry(WallpaperTone_W7, "Win7", ReportProgress_Detailed ? TreeView : null);
                                    WallpaperTone.Save_To_Registry(WallpaperTone_WVista, "WinVista", ReportProgress_Detailed ? TreeView : null);
                                    WallpaperTone.Save_To_Registry(WallpaperTone_WXP, "WinXP", ReportProgress_Detailed ? TreeView : null);

                                    if (Wallpaper.Enabled)
                                    {
                                        if (Program.W11 & WallpaperTone_W11.Enabled)
                                            WallpaperTone_W11.Apply(ReportProgress_Detailed ? TreeView : null);
                                        if (Program.W10 & WallpaperTone_W10.Enabled)
                                            WallpaperTone_W10.Apply(ReportProgress_Detailed ? TreeView : null);
                                        if (Program.W81 & WallpaperTone_W81.Enabled)
                                            WallpaperTone_W81.Apply(ReportProgress_Detailed ? TreeView : null);
                                        if (Program.W7 & WallpaperTone_W7.Enabled)
                                            WallpaperTone_W7.Apply(ReportProgress_Detailed ? TreeView : null);
                                        if (Program.WVista & WallpaperTone_WVista.Enabled)
                                            WallpaperTone_WVista.Apply(ReportProgress_Detailed ? TreeView : null);
                                        if (Program.WXP & WallpaperTone_WXP.Enabled)
                                            WallpaperTone_WXP.Apply(ReportProgress_Detailed ? TreeView : null);
                                    }

                                }), TreeView, Program.Lang.TM_Applying_WallpaperTone, Program.Lang.TM_WallpaperTone_Error, Program.Lang.TM_Time, sw_all);

                        #region Consoles
                        EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_CMD_Enabled", CommandPrompt.Enabled);
                        EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_PS_32_Enabled", PowerShellx86.Enabled);
                        EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_PS_64_Enabled", PowerShellx64.Enabled);
                        EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_Stable_Enabled", Terminal.Enabled);
                        EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Terminals", "Terminal_Preview_Enabled", TerminalPreview.Enabled);

                        this.Execute(new MethodInvoker(() => Apply_CommandPrompt(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_CMD, Program.Lang.TM_CMD_Error, Program.Lang.TM_Time, sw_all, !CommandPrompt.Enabled, Program.Lang.TM_Skip_CMD);

                        this.Execute(new MethodInvoker(() => Apply_PowerShell86(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_PS32, Program.Lang.TM_PS32_Error, Program.Lang.TM_Time, sw_all, !PowerShellx86.Enabled, Program.Lang.TM_Skip_PS32);

                        this.Execute(new MethodInvoker(() => Apply_PowerShell64(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_PS64, Program.Lang.TM_PS64_Error, Program.Lang.TM_Time, sw_all, !PowerShellx64.Enabled, Program.Lang.TM_Skip_PS64);
                        #endregion

                        #region Windows Terminal
                        var sw = new Stopwatch();
                        sw.Reset();
                        sw.Start();
                        if (Program.W10 | Program.W11)
                        {

                            if (ReportProgress)
                            {
                                if (Terminal.Enabled & TerminalPreview.Enabled)
                                {
                                    AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Check_Terminals), "info");
                                }

                                else if (Terminal.Enabled)
                                {
                                    AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Skip_TerminalPreview), "skip");
                                    AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Check_TerminalStable), "info");
                                }

                                else if (TerminalPreview.Enabled)
                                {
                                    AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Skip_TerminalStable), "skip");
                                    AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Check_TerminalPreview), "info");
                                }

                                else
                                {
                                    AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Skip_Terminals), "skip");

                                }

                            }

                            string TerDir;
                            string TerPreDir;

                            if (!Program.Settings.WindowsTerminals.Path_Deflection)
                            {
                                TerDir = Program.PATH_TerminalJSON;
                                TerPreDir = Program.PATH_TerminalPreviewJSON;
                            }
                            else
                            {
                                if (System.IO.File.Exists(Program.Settings.WindowsTerminals.Terminal_Stable_Path))
                                {
                                    TerDir = Program.Settings.WindowsTerminals.Terminal_Stable_Path;
                                }
                                else
                                {
                                    TerDir = Program.PATH_TerminalJSON;
                                }

                                if (System.IO.File.Exists(Program.Settings.WindowsTerminals.Terminal_Preview_Path))
                                {
                                    TerPreDir = Program.Settings.WindowsTerminals.Terminal_Preview_Path;
                                }
                                else
                                {
                                    TerPreDir = Program.PATH_TerminalPreviewJSON;
                                }
                            }

                            if (Terminal.Enabled)
                            {
                                if (System.IO.File.Exists(TerDir))
                                {

                                    try
                                    {
                                        AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Applying_TerminalStable), "info");
                                        Terminal.Save(TerDir, WinTerminal.Mode.JSONFile);
                                        if (ReportProgress)
                                            AddNode(TreeView, string.Format(Program.Lang.TM_Time, sw.ElapsedMilliseconds / 1000d), "time");
                                    }
                                    catch (Exception ex)
                                    {
                                        sw.Stop();
                                        sw_all.Stop();
                                        _ErrorHappened = true;
                                        if (ReportProgress)
                                        {
                                            AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Error_TerminalStable), "error");
                                            AddException(Program.Lang.TM_Error_TerminalStable, ex);
                                        }
                                        else
                                        {
                                            Forms.BugReport.ThrowError(ex);
                                        }

                                        sw.Start();
                                        sw_all.Start();
                                    }
                                }


                                else if (!Program.Settings.WindowsTerminals.Path_Deflection)
                                {
                                    AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Skip_TerminalStable_NotInstalled), "skip");
                                }
                                else
                                {
                                    AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Skip_TerminalStable_DeflectionNotFound), "skip");

                                }
                            }

                            if (TerminalPreview.Enabled)
                            {
                                if (System.IO.File.Exists(TerPreDir))
                                {

                                    try
                                    {
                                        AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Applying_TerminalPreview), "info");
                                        TerminalPreview.Save(TerPreDir, WinTerminal.Mode.JSONFile, WinTerminal.Version.Preview);
                                        if (ReportProgress)
                                            AddNode(TreeView, string.Format(Program.Lang.TM_Time, sw.ElapsedMilliseconds / 1000d), "time");
                                    }
                                    catch (Exception ex)
                                    {
                                        sw.Stop();
                                        sw_all.Stop();
                                        _ErrorHappened = true;
                                        if (ReportProgress)
                                        {
                                            AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Error_TerminalPreview), "error");
                                            AddException(Program.Lang.TM_Error_TerminalPreview, ex);
                                        }
                                        else
                                        {
                                            Forms.BugReport.ThrowError(ex);
                                        }

                                        sw.Start();
                                        sw_all.Start();
                                    }
                                }

                                else if (!Program.Settings.WindowsTerminals.Path_Deflection)
                                {
                                    AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Skip_TerminalPreview_NotInstalled), "skip");
                                }
                                else
                                {
                                    AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Skip_TerminalPreview_DeflectionNotFound), "skip");
                                }
                            }
                        }

                        else
                        {
                            AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Skip_Terminals_NotSupported), "skip");
                        }
                        sw.Stop();
                        #endregion

                        // ScreenSaver
                        this.Execute(new MethodInvoker(() => ScreenSaver.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_ScreenSaver, Program.Lang.TM_Error_ScreenSaver, Program.Lang.TM_Time, sw_all);

                        // Sounds
                        this.Execute(new MethodInvoker(() => Sounds.Apply(ReportProgress_Detailed ? TreeView : null)), TreeView, Program.Lang.TM_Applying_Sounds, Program.Lang.TM_Error_Sounds, Program.Lang.TM_Time, sw_all, !Sounds.Enabled, Program.Lang.TM_Skip_Sounds);

                        // Cursors
                        this.Execute(new MethodInvoker(() => Apply_Cursors(TreeView)), TreeView, "", Program.Lang.TM_Error_Cursors, Program.Lang.TM_Time_Cursors, sw_all);

                        // Update LogonUI wallpaper in HKEY_USERS\.DEFAULT
                        if (Program.Settings.ThemeApplyingBehavior.Desktop_HKU_DEFAULT == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                        {

                            this.Execute(new MethodInvoker(() =>
                                    {
                                        EditReg(ReportProgress_Detailed ? TreeView : null, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "Wallpaper", GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", ""), RegistryValueKind.String);
                                        EditReg(ReportProgress_Detailed ? TreeView : null, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "WallpaperStyle", GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "WallpaperStyle", "2"), RegistryValueKind.String);
                                        EditReg(ReportProgress_Detailed ? TreeView : null, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "TileWallpaper", GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "TileWallpaper", "0"), RegistryValueKind.String);
                                        EditReg(ReportProgress_Detailed ? TreeView : null, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "Pattern", GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Pattern", ""), RegistryValueKind.String);
                                    }), TreeView, Program.Lang.TM_Applying_DesktopAllUsers, Program.Lang.TM_Error_SetDesktop, Program.Lang.TM_Time);
                        }

                        else if (Program.Settings.ThemeApplyingBehavior.Desktop_HKU_DEFAULT == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.RestoreDefaults)
                        {

                            this.Execute(new MethodInvoker(() =>
                                    {
                                        EditReg(ReportProgress_Detailed ? TreeView : null, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "Wallpaper", "", RegistryValueKind.String);
                                        EditReg(ReportProgress_Detailed ? TreeView : null, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "WallpaperStyle", "2", RegistryValueKind.String);
                                        EditReg(ReportProgress_Detailed ? TreeView : null, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "TileWallpaper", "0", RegistryValueKind.String);
                                        EditReg(ReportProgress_Detailed ? TreeView : null, @"HKEY_USERS\.DEFAULT\Control Panel\Desktop", "Pattern", "", RegistryValueKind.String);
                                    }), TreeView, Program.Lang.TM_Applying_DesktopAllUsers, Program.Lang.TM_Error_SetDesktop, Program.Lang.TM_Time);

                        }

                        // Update User Preference Mask for HKEY_USERS\.DEFAULT
                        // Always make it the last operation
                        try
                        {
                            Win32.Update_UPM_DEFAULT(ReportProgress_Detailed ? TreeView : null);
                        }
                        catch
                        {
                        }

                        if (ReportProgress_Detailed)
                            AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SMT, "User32", "SendMessageTimeout", "HWND_BROADCAST", "WM_SETTINGCHANGE", "UIntPtr.Zero", "Marshal.StringToHGlobalAnsi(\"Environment\")", "SMTO_ABORTIFHUNG", MSG_TIMEOUT, "RESULT"), "dll");
                        User32.SendMessageTimeout(HWND_BROADCAST, WM_SETTINGCHANGE, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("Environment"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, (uint)MSG_TIMEOUT, out RESULT);

                        if (ReportProgress)
                        {
                            if (!_ErrorHappened)
                            {
                                AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), string.Format(Program.Lang.TM_Applied, sw_all.ElapsedMilliseconds / 1000d)), "success");
                            }
                            else
                            {
                                AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), string.Format(Program.Lang.TM_AppliedWithErrors, sw_all.ElapsedMilliseconds / 1000d)), "warning");
                            }
                        }

                        sw_all.Reset();
                        sw_all.Stop();
                        break;
                    }
                #endregion

                case Source.File:
                    {
                        if (System.IO.File.Exists(File))
                        {
                            try
                            {
                                FileSystem.Kill(File);
                            }
                            catch
                            {
                            }
                        }

                        if (Info.ExportResThemePack)
                        {
                            PackThemeResources((Manager)Clone(), File, new FileInfo(File).DirectoryName + @"\" + Path.GetFileNameWithoutExtension(File) + ".wptp");
                        }
                        else
                        {
                            System.IO.File.WriteAllText(File, ToString());
                        }

                        break;
                    }

            }
        }

        public string ToString(bool IgnoreCompression = false)
        {
            var JSON_Overall = new JObject();
            JSON_Overall.RemoveAll();

            Info.AppVersion = Program.AppVersion;

            foreach (FieldInfo field in GetType().GetFields(bindingFlags))
            {
                var type = field.FieldType;

                if (IsStructure(type))
                {
                    JSON_Overall.Add(field.Name, DeserializeProps(type, field.GetValue(this)));
                }
                else
                {
                    JSON_Overall.Add(field.Name, JToken.FromObject(field.GetValue(this)));
                }

            }

            if (Program.Settings.FileTypeManagement.CompressThemeFile && !IgnoreCompression)
            {
                return JSON_Overall.ToString().Compress();
            }
            else
            {
                return JSON_Overall.ToString();
            }
        }

        public bool IsStructure(Type type)
        {
            return type.IsValueType && !type.IsPrimitive && type.Namespace is not null && !type.Namespace.StartsWith("System.");
        }

        public void PackThemeResources(Manager TM, string File, string Package)
        {
            string cache = @"%WinPaletterAppData%\ThemeResPack_Cache\" + string.Concat(TM.Info.ThemeName.Replace(" ", "").Split(Path.GetInvalidFileNameChars())) + @"\";
            var filesList = new Dictionary<string, string>();
            filesList.Clear();
            string x;
            string ZipEntry;

            if (System.IO.File.Exists(Package))
                System.IO.File.Delete(Package);
            using (var archive = ZipFile.Open(Package, ZipArchiveMode.Create))
            {
                if (TM.LogonUI7.Enabled && TM.LogonUI7.Mode == Theme.Structures.LogonUI7.Modes.CustomImage || !TM.Windows81.NoLockScreen && TM.Windows81.LockScreenType == Theme.Structures.LogonUI7.Modes.CustomImage)
                {
                    x = TM.LogonUI7.ImagePath;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\Web", Program._ignore))
                    {
                        ZipEntry = cache + "LogonUI" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.LogonUI7.ImagePath = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }

                if (TM.Terminal.Enabled)
                {
                    x = TM.Terminal.DefaultProf.BackgroundImage;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\Web", Program._ignore))
                    {
                        ZipEntry = cache + "winterminal_defprofile_backimg" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Terminal.DefaultProf.BackgroundImage = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Terminal.DefaultProf.Icon;
                    if (!string.IsNullOrWhiteSpace(x) && !(x.Length <= 1) && !x.StartsWith(Program.PATH_Windows + @"\Web", Program._ignore))
                    {
                        ZipEntry = cache + "winterminal_defprofile_icon" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Terminal.DefaultProf.Icon = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    foreach (var i in TM.Terminal.Profiles)
                    {
                        x = i.BackgroundImage;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\Web", Program._ignore))
                        {
                            ZipEntry = cache + "winterminal_profile(" + string.Concat(i.Name.Replace(" ", "").Split(Path.GetInvalidFileNameChars())) + ")_backimg" + Path.GetExtension(x);
                            if (System.IO.File.Exists(x))
                                i.BackgroundImage = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }

                        x = i.Icon;
                        if (!string.IsNullOrWhiteSpace(x) && !(x.Length <= 1) && !x.StartsWith(Program.PATH_Windows + @"\Web", Program._ignore))
                        {
                            ZipEntry = cache + "winterminal_profile(" + string.Concat(i.Name.Replace(" ", "").Split(Path.GetInvalidFileNameChars())) + ")_icon" + Path.GetExtension(x);
                            if (System.IO.File.Exists(x))
                                i.Icon = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }
                    }
                }

                if (TM.TerminalPreview.Enabled)
                {
                    x = TM.TerminalPreview.DefaultProf.BackgroundImage;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\Web", Program._ignore))
                    {
                        ZipEntry = cache + "winterminal_preview_defprofile_backimg" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.TerminalPreview.DefaultProf.BackgroundImage = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.TerminalPreview.DefaultProf.Icon;
                    if (!string.IsNullOrWhiteSpace(x) && !(x.Length <= 1) && !x.StartsWith(Program.PATH_Windows + @"\Web", Program._ignore))
                    {
                        ZipEntry = cache + "winterminal_preview_defprofile_icon" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.TerminalPreview.DefaultProf.Icon = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    foreach (var i in TM.TerminalPreview.Profiles)
                    {
                        x = i.BackgroundImage;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\Web", Program._ignore))
                        {
                            ZipEntry = cache + "winterminal_preview_profile(" + string.Concat(i.Name.Replace(" ", "").Split(Path.GetInvalidFileNameChars())) + ")_backimg" + Path.GetExtension(x);
                            if (System.IO.File.Exists(x))
                                i.BackgroundImage = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }

                        x = i.Icon;
                        if (!string.IsNullOrWhiteSpace(x) && !(x.Length <= 1) && !x.StartsWith(Program.PATH_Windows + @"\Web", Program._ignore))
                        {
                            ZipEntry = cache + "winterminal_preview_profile(" + string.Concat(i.Name.Replace(" ", "").Split(Path.GetInvalidFileNameChars())) + ")_icon" + Path.GetExtension(x);
                            if (System.IO.File.Exists(x))
                                i.Icon = ZipEntry;
                            filesList.Add(ZipEntry, x);
                        }
                    }
                }

                if (TM.WallpaperTone_W11.Enabled)
                {
                    x = TM.WallpaperTone_W11.Image;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\Web", Program._ignore))
                    {
                        ZipEntry = cache + "wt_w11" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.WallpaperTone_W11.Image = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }

                if (TM.WallpaperTone_W10.Enabled)
                {
                    x = TM.WallpaperTone_W10.Image;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\Web", Program._ignore))
                    {
                        ZipEntry = cache + "wt_w10" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.WallpaperTone_W10.Image = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }

                if (TM.WallpaperTone_W81.Enabled)
                {
                    x = TM.WallpaperTone_W81.Image;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\Web", Program._ignore))
                    {
                        ZipEntry = cache + "wt_w81" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.WallpaperTone_W81.Image = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }

                if (TM.WallpaperTone_W7.Enabled)
                {
                    x = TM.WallpaperTone_W7.Image;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\Web", Program._ignore))
                    {
                        ZipEntry = cache + "wt_w7" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.WallpaperTone_W7.Image = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }

                if (TM.WallpaperTone_WVista.Enabled)
                {
                    x = TM.WallpaperTone_WVista.Image;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\Web", Program._ignore))
                    {
                        ZipEntry = cache + "wt_wvista" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.WallpaperTone_WVista.Image = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }

                if (TM.WallpaperTone_WXP.Enabled)
                {
                    x = TM.WallpaperTone_WXP.Image;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\Web", Program._ignore))
                    {
                        ZipEntry = cache + "wt_wxp" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.WallpaperTone_WXP.Image = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }

                if (TM.ScreenSaver.Enabled)
                {
                    x = TM.ScreenSaver.File;
                    if (!string.IsNullOrWhiteSpace(x))
                    {
                        ZipEntry = cache + "scrsvr" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.ScreenSaver.File = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }

                #region Sounds
                if (TM.Sounds.Enabled)
                {
                    x = TM.Sounds.Snd_Win_Default;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Default" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Default = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_AppGPFault;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_AppGPFault" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_AppGPFault = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_CCSelect;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_CCSelect" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_CCSelect = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_ChangeTheme;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_ChangeTheme" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_ChangeTheme = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Close;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Close" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Close = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_CriticalBatteryAlarm;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_CriticalBatteryAlarm" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_CriticalBatteryAlarm = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_DeviceConnect;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_DeviceConnect" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_DeviceConnect = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_DeviceDisconnect;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_DeviceDisconnect" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_DeviceDisconnect = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_DeviceFail;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_DeviceFail" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_DeviceFail = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_FaxBeep;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_FaxBeep" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_FaxBeep = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_LowBatteryAlarm;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_LowBatteryAlarm" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_LowBatteryAlarm = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_MailBeep;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_MailBeep" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_MailBeep = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Maximize;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Maximize" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Maximize = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_MenuCommand;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_MenuCommand" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_MenuCommand = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_MenuPopup;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_MenuPopup" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_MenuPopup = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_MessageNudge;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_MessageNudge" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_MessageNudge = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Minimize;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Minimize" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Minimize = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Default;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Default" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Default = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_IM;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_IM" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_IM = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm10;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm10" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm10 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm2;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm2" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm2 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm3;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm3" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm3 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm4;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm4" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm4 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm5;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm5" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm5 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm6;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm6" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm6 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm7;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm7" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm7 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm8;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm8" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm8 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Alarm9;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Alarm9" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Alarm9 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Call = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call10;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call10" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Call10 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call2;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call2" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Call2 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call3;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call3" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Call3 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call4;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call4" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Call4 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call5;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call5" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Call5 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call6;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call6" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Call6 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call7;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call7" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Call7 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call8;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call8" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Call8 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Looping_Call9;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Looping_Call9" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Looping_Call9 = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Mail;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Mail" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Mail = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Proximity;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Proximity" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Proximity = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_Reminder;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_Reminder" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_Reminder = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Notification_SMS;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Notification_SMS" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Notification_SMS = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_Open;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_Open" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_Open = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_PrintComplete;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_PrintComplete" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_PrintComplete = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_ProximityConnection;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_ProximityConnection" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_ProximityConnection = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_RestoreDown;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_RestoreDown" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_RestoreDown = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_RestoreUp;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_RestoreUp" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_RestoreUp = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_ShowBand;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_ShowBand" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_ShowBand = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_SystemAsterisk;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_SystemAsterisk" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_SystemAsterisk = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_SystemExclamation;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_SystemExclamation" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_SystemExclamation = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_SystemExit;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_SystemExit" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_SystemExit = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_SystemStart;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_SystemStart" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_SystemStart = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Imageres_SystemStart;
                    if (!string.IsNullOrWhiteSpace(x))  // Don't include the condition: Not x.StartsWith(My.PATH_Windows & "\media", My._ignore)
                    {
                        ZipEntry = cache + "Snd_Imageres_SystemStart" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Imageres_SystemStart = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_SystemHand;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_SystemHand" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_SystemHand = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_SystemNotification;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_SystemNotification" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_SystemNotification = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_SystemQuestion;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_SystemQuestion" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_SystemQuestion = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_WindowsLogoff;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_WindowsLogoff" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_WindowsLogoff = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_WindowsLogon;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_WindowsLogon" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_WindowsLogon = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_WindowsUAC;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_WindowsUAC" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_WindowsUAC = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Win_WindowsUnlock;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Win_WindowsUnlock" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Win_WindowsUnlock = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Explorer_ActivatingDocument;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Explorer_ActivatingDocument" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Explorer_ActivatingDocument = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Explorer_BlockedPopup;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Explorer_BlockedPopup" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Explorer_BlockedPopup = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Explorer_EmptyRecycleBin;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Explorer_EmptyRecycleBin" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Explorer_EmptyRecycleBin = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Explorer_FeedDiscovered;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Explorer_FeedDiscovered" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Explorer_FeedDiscovered = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Explorer_MoveMenuItem;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Explorer_MoveMenuItem" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Explorer_MoveMenuItem = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Explorer_Navigating;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Explorer_Navigating" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Explorer_Navigating = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Explorer_SecurityBand;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Explorer_SecurityBand" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Explorer_SecurityBand = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Explorer_SearchProviderDiscovered;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Explorer_SearchProviderDiscovered" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Explorer_SearchProviderDiscovered = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Explorer_FaxError;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Explorer_FaxError" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Explorer_FaxError = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Explorer_FaxLineRings;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Explorer_FaxLineRings" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Explorer_FaxLineRings = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Explorer_FaxNew;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Explorer_FaxNew" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Explorer_FaxNew = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_Explorer_FaxSent;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_Explorer_FaxSent" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_Explorer_FaxSent = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_NetMeeting_PersonJoins;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_NetMeeting_PersonJoins" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_NetMeeting_PersonJoins = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_NetMeeting_PersonLeaves;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_NetMeeting_PersonLeaves" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_NetMeeting_PersonLeaves = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_NetMeeting_ReceiveCall;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_NetMeeting_ReceiveCall" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_NetMeeting_ReceiveCall = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_NetMeeting_ReceiveRequestToJoin;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_NetMeeting_ReceiveRequestToJoin" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_NetMeeting_ReceiveRequestToJoin = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_SpeechRec_DisNumbersSound;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_SpeechRec_DisNumbersSound" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_SpeechRec_DisNumbersSound = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_SpeechRec_HubOffSound;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_SpeechRec_HubOffSound" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_SpeechRec_HubOffSound = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_SpeechRec_HubOnSound;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_SpeechRec_HubOnSound" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_SpeechRec_HubOnSound = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_SpeechRec_HubSleepSound;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_SpeechRec_HubSleepSound" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_SpeechRec_HubSleepSound = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_SpeechRec_MisrecoSound;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_SpeechRec_MisrecoSound" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_SpeechRec_MisrecoSound = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }

                    x = TM.Sounds.Snd_SpeechRec_PanelSound;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\media", Program._ignore))
                    {
                        ZipEntry = cache + "Snd_SpeechRec_PanelSound" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Sounds.Snd_SpeechRec_PanelSound = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }
                #endregion

                if (TM.Wallpaper.Enabled && TM.Wallpaper.WallpaperType == Wallpaper.WallpaperTypes.Picture)
                {
                    x = TM.Wallpaper.ImageFile;
                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\Web", Program._ignore))
                    {
                        ZipEntry = cache + "wallpaper_file" + Path.GetExtension(x);
                        if (System.IO.File.Exists(x))
                            TM.Wallpaper.ImageFile = ZipEntry;
                        filesList.Add(ZipEntry, x);
                    }
                }

                foreach (var _file in filesList)
                {
                    if (System.IO.File.Exists(_file.Value))
                        archive.CreateEntryFromFile(_file.Value, _file.Key.Split('\\').Last(), CompressionLevel.Optimal);
                }

                if (TM.WindowsXP.Theme == WindowsXP.Themes.Custom)
                {
                    x = TM.WindowsXP.ThemeFile;
                    if (!string.IsNullOrWhiteSpace(x) && System.IO.File.Exists(x) && !x.StartsWith(Program.PATH_Windows + @"\Resources\Themes\Luna", Program._ignore))
                    {
                        ZipEntry = cache + @"WXP_VS\" + Path.GetFileName(x);
                        if (System.IO.File.Exists(x))
                            TM.WindowsXP.ThemeFile = ZipEntry;
                        string DirName = new FileInfo(x).Directory.FullName;
                        foreach (string file in Directory.EnumerateFiles(DirName, "*.*", SearchOption.AllDirectories))
                        {
                            if (System.IO.File.Exists(file))
                                archive.CreateEntryFromFile(file, "WXP_VS" + file.Replace(DirName, ""), CompressionLevel.Optimal);
                        }
                    }
                }

                if (TM.Wallpaper.Enabled && TM.Wallpaper.WallpaperType == Wallpaper.WallpaperTypes.SlideShow)
                {
                    if (TM.Wallpaper.SlideShow_Folder_or_ImagesList)
                    {
                        x = TM.Wallpaper.Wallpaper_Slideshow_ImagesRootPath;
                        if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\Web", Program._ignore))
                        {
                            TM.Wallpaper.Wallpaper_Slideshow_ImagesRootPath = cache + "wallpapers_slideshow";

                            foreach (var image in Directory.EnumerateFiles(x, "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".bmp") || s.EndsWith(".jpg") || s.EndsWith(".png") || s.EndsWith(".gif")))


                            {


                                if (System.IO.File.Exists(image))
                                    archive.CreateEntryFromFile(image, @"wallpapers_slideshow\" + new FileInfo(image).Name, CompressionLevel.Optimal);

                            }

                        }
                    }

                    else
                    {
                        string[] arr = TM.Wallpaper.Wallpaper_Slideshow_Images.ToArray();
                        if (arr.Count() > 0)
                        {
                            if (!arr[0].StartsWith(Program.PATH_Windows + @"\Web", Program._ignore))
                            {
                                TM.Wallpaper.Wallpaper_Slideshow_ImagesRootPath = cache + "WallpapersList";
                                TM.Wallpaper.Wallpaper_Slideshow_Images = new string[] { };
                                for (int x0 = 0, loopTo = arr.Count() - 1; x0 <= loopTo; x0++)
                                {
                                    x = arr[x0];
                                    if (!string.IsNullOrWhiteSpace(x) && !x.StartsWith(Program.PATH_Windows + @"\Web", Program._ignore))
                                    {
                                        ZipEntry = cache + @"WallpapersList\wallpaperlist_" + x0 + "_file" + Path.GetExtension(x);
                                        if (System.IO.File.Exists(x))
                                        {
                                            TM.Wallpaper.Wallpaper_Slideshow_Images = TM.Wallpaper.Wallpaper_Slideshow_Images.Append(ZipEntry).ToArray();
                                            archive.CreateEntryFromFile(x, @"WallpapersList\wallpaperlist_" + x0 + "_file" + Path.GetExtension(x), CompressionLevel.Optimal);
                                        }
                                    }
                                }
                            }
                        }
                    }

                }

                System.IO.File.WriteAllText(File, TM.ToString());
            }

        }

        public static IEnumerable<string> Decompress(string File)
        {
            IEnumerable<string> DecompressedData;

            try
            {
                DecompressedData = System.IO.File.ReadAllText(File).Decompress().CList();
            }
            catch
            {
                DecompressedData = System.IO.File.ReadAllText(File).CList();
            }

            return DecompressedData;
        }

        private JObject DeserializeProps(Type StructureType, object Structure)
        {
            var j = new JObject();

            j.RemoveAll();

            foreach (var field in StructureType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                JToken result;

                try
                {
                    result = JToken.FromObject(field.GetValue(Structure));
                }
                catch
                {
                    result = default;
                }

                j.Add(field.Name, result);
            }

            return j;
        }

        private static bool IsValidJson(string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput))
            {
                return false;
            }
            strInput = strInput.Trim();
            if (strInput.StartsWith("{") && strInput.EndsWith("}") || strInput.StartsWith("[") && strInput.EndsWith("]")) // For object
            {
                // For array
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    // Exception in parsing json
                    return false;
                }
                catch (Exception ex) // some other exception
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Applying Voids
        public static void Apply_LogonUI7(Theme.Structures.LogonUI7 LogonElement, string RegEntryHint = "LogonUI", TreeView TreeView = null)
        {

            bool ReportProgress = Program.Settings.ThemeLog.VerboseLevel != WPSettings.Structures.ThemeLog.VerboseLevels.None && TreeView is not null;
            bool ReportProgress_Detailed = ReportProgress && Program.Settings.ThemeLog.VerboseLevel == WPSettings.Structures.ThemeLog.VerboseLevels.Detailed;

            EditReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background", "OEMBackground", LogonElement.Enabled.ToInteger());
            EditReg(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\System", "UseOEMBackground", LogonElement.Enabled.ToInteger());

            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\" + RegEntryHint, "Mode", (int)LogonElement.Mode);
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\" + RegEntryHint, "ImagePath", LogonElement.ImagePath, RegistryValueKind.String);
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\" + RegEntryHint, "Color", LogonElement.Color.ToArgb());
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\" + RegEntryHint, "Blur", LogonElement.Blur.ToInteger());
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\" + RegEntryHint, "Blur_Intensity", LogonElement.Blur_Intensity);
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\" + RegEntryHint, "Grayscale", LogonElement.Grayscale.ToInteger());
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\" + RegEntryHint, "Noise", LogonElement.Noise.ToInteger());
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\" + RegEntryHint, "Noise_Mode", (int)LogonElement.Noise_Mode);
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\" + RegEntryHint, "Noise_Intensity", LogonElement.Noise_Intensity);

            if (LogonElement.Enabled)
            {
                IntPtr wow64Value = IntPtr.Zero;
                Kernel32.Wow64DisableWow64FsRedirection(ref wow64Value);

                string DirX = Program.PATH_System32 + @"\oobe\info\backgrounds";

                Directory.CreateDirectory(DirX);

                foreach (string fileX in Program.Computer.FileSystem.GetFiles(DirX))
                {
                    try
                    {
                        FileSystem.Kill(fileX);
                    }
                    catch
                    {
                    }
                }

                var bmpList = new List<Bitmap>();
                bmpList.Clear();

                if (ReportProgress_Detailed)
                    AddNode(TreeView, Program.Lang.Verbose_GetInstanceLogonUIImg, "info");

                switch (LogonElement.Mode)
                {
                    case Theme.Structures.LogonUI7.Modes.Default:
                        {
                            for (int i = 5031; i <= 5043; i += +1)
                                bmpList.Add(PE_Functions.GetPNGFromDLL(Program.PATH_imageres, i, "IMAGE", Program.Computer.Screen.Bounds.Size.Width, Program.Computer.Screen.Bounds.Size.Height));
                            break;
                        }

                    case Theme.Structures.LogonUI7.Modes.CustomImage:
                        {
                            if (System.IO.File.Exists(LogonElement.ImagePath))
                            {
                                bmpList.Add((Bitmap)Bitmap_Mgr.Load(LogonElement.ImagePath).Resize(Program.Computer.Screen.Bounds.Size));
                            }
                            else
                            {
                                bmpList.Add((Bitmap)Color.Black.ToBitmap(Program.Computer.Screen.Bounds.Size));
                            }

                            break;
                        }

                    case Theme.Structures.LogonUI7.Modes.SolidColor:
                        {
                            bmpList.Add((Bitmap)LogonElement.Color.ToBitmap(Program.Computer.Screen.Bounds.Size));
                            break;
                        }

                    case Theme.Structures.LogonUI7.Modes.Wallpaper:
                        {
                            using (Bitmap b = new Bitmap(Program.GetWallpaper()))
                            {
                                bmpList.Add((Bitmap)b.Resize(Program.Computer.Screen.Bounds.Size).Clone());
                            }

                            break;
                        }

                }

                if (ReportProgress)
                    AddNode(TreeView, string.Format(Program.Lang.TM_RenderingCustomLogonUI_MayNotRespond), "info");

                for (int x = 0, loopTo = bmpList.Count - 1; x <= loopTo; x++)
                {
                    if (ReportProgress)
                        AddNode(TreeView, string.Format("{3}: " + Program.Lang.TM_RenderingCustomLogonUI_Progress + " {2} ({0}/{1})", x + 1, bmpList.Count, bmpList[x].Width + "x" + bmpList[x].Height, DateTime.Now.ToLongTimeString()), "info");

                    if (LogonElement.Grayscale)
                    {
                        if (ReportProgress_Detailed)
                            AddNode(TreeView, Program.Lang.Verbose_GrayscaleLogonUIImg, "apply");
                        bmpList[x] = bmpList[x].Grayscale();
                    }


                    if (LogonElement.Blur)
                    {
                        if (ReportProgress_Detailed)
                            AddNode(TreeView, Program.Lang.Verbose_BlurringLogonUIImg, "apply");

                        var imgF = new ImageProcessor.ImageFactory();
                        using (var b = new Bitmap(bmpList[x]))
                        {
                            imgF.Load(b);
                            imgF.GaussianBlur(LogonElement.Blur_Intensity);
                            bmpList[x] = (Bitmap)imgF.Image;
                        }

                    }

                    if (LogonElement.Noise)
                    {
                        if (ReportProgress_Detailed)
                            AddNode(TreeView, Program.Lang.Verbose_NoiseLogonUIImg, "apply");

                        bmpList[x] = bmpList[x].Noise(LogonElement.Noise_Mode, (float)(LogonElement.Noise_Intensity / 100d));
                    }
                }

                if (bmpList.Count == 1)
                {
                    if (Program.isElevated)
                    {
                        bmpList[0].Save(DirX + @"\backgroundDefault.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    else
                    {
                        bmpList[0].Save(Program.PATH_appData + @"\backgroundDefault.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        Reg_IO.Move_File(Program.PATH_appData + @"\backgroundDefault.jpg", DirX + @"\backgroundDefault.jpg");
                    }

                    if (ReportProgress_Detailed)
                        AddNode(TreeView, string.Format(Program.Lang.Verbose_LogonUIImgSaved, DirX + @"\backgroundDefault.jpg"), "info");
                }
                else
                {
                    for (int x = 0, loopTo1 = bmpList.Count - 1; x <= loopTo1; x++)
                    {
                        if (Program.isElevated)
                        {
                            bmpList[x].Save(DirX + string.Format(@"\background{0}x{1}.jpg", bmpList[x].Width, bmpList[x].Height), System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                        else
                        {
                            bmpList[x].Save(Program.PATH_appData + string.Format(@"\background{0}x{1}.jpg", bmpList[x].Width, bmpList[x].Height), System.Drawing.Imaging.ImageFormat.Jpeg);
                            Reg_IO.Move_File(Program.PATH_appData + string.Format(@"\background{0}x{1}.jpg", bmpList[x].Width, bmpList[x].Height), DirX + string.Format(@"\background{0}x{1}.jpg", bmpList[x].Width, bmpList[x].Height));
                        }

                        if (ReportProgress_Detailed)
                            AddNode(TreeView, string.Format(Program.Lang.Verbose_LogonUIImgNUMSaved, DirX + string.Format(@"\background{0}x{1}.jpg", bmpList[x].Width, bmpList[x].Height), x + 1), "info");

                    }
                }

                Kernel32.Wow64RevertWow64FsRedirection(IntPtr.Zero);
            }
        }

        public void Apply_LogonUI_8(TreeView TreeView = null)
        {

            bool ReportProgress = Program.Settings.ThemeLog.VerboseLevel != WPSettings.Structures.ThemeLog.VerboseLevels.None && TreeView is not null;
            bool ReportProgress_Detailed = ReportProgress && Program.Settings.ThemeLog.VerboseLevel == WPSettings.Structures.ThemeLog.VerboseLevels.Detailed;

            string lockimg = Program.PATH_appData + @"\LockScreen.png";

            EditReg(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "NoLockScreen", Windows81.NoLockScreen.ToInteger());
            EditReg(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\Personalization", "LockScreenImage", lockimg, RegistryValueKind.String);

            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Mode", (int)Windows81.LockScreenType);
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Metro_LockScreenSystemID", Windows81.LockScreenSystemID);
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "ImagePath", LogonUI7.ImagePath, RegistryValueKind.String);
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Color", LogonUI7.Color.ToArgb());
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Blur", LogonUI7.Blur.ToInteger());
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Blur_Intensity", LogonUI7.Blur_Intensity);
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Grayscale", LogonUI7.Grayscale.ToInteger());
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Noise", LogonUI7.Noise.ToInteger());
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Noise_Mode", (int)LogonUI7.Noise_Mode);
            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\LogonUI", "Noise_Intensity", LogonUI7.Noise_Intensity);

            if (!Windows81.NoLockScreen)
            {
                Bitmap bmp;

                if (ReportProgress_Detailed)
                    AddNode(TreeView, Program.Lang.Verbose_GetInstanceLockScreenImg, "info");

                switch (Windows81.LockScreenType)
                {
                    case Theme.Structures.LogonUI7.Modes.Default:
                        {
                            string syslock = "";

                            if (System.IO.File.Exists(string.Format(Program.PATH_Windows + @"\Web\Screen\img10{0}.png", Program.TM.Windows81.LockScreenSystemID)))
                            {
                                syslock = string.Format(Program.PATH_Windows + @"\Web\Screen\img10{0}.png", Program.TM.Windows81.LockScreenSystemID);
                            }

                            else if (System.IO.File.Exists(string.Format(Program.PATH_Windows + @"\Web\Screen\img10{0}.jpg", Program.TM.Windows81.LockScreenSystemID)))
                            {
                                syslock = string.Format(Program.PATH_Windows + @"\Web\Screen\img10{0}.jpg", Program.TM.Windows81.LockScreenSystemID);

                            }

                            if (System.IO.File.Exists(syslock))
                            {
                                bmp = Bitmap_Mgr.Load(syslock);
                            }
                            else
                            {
                                bmp = (Bitmap)Color.Black.ToBitmap(Program.Computer.Screen.Bounds.Size);
                            }

                            break;
                        }

                    case Theme.Structures.LogonUI7.Modes.CustomImage:
                        {
                            if (System.IO.File.Exists(LogonUI7.ImagePath))
                            {
                                bmp = Bitmap_Mgr.Load(LogonUI7.ImagePath);
                            }
                            else
                            {
                                bmp = (Bitmap)Color.Black.ToBitmap(Program.Computer.Screen.Bounds.Size);
                            }

                            break;
                        }

                    case Theme.Structures.LogonUI7.Modes.SolidColor:
                        {
                            bmp = (Bitmap)LogonUI7.Color.ToBitmap(Program.Computer.Screen.Bounds.Size);
                            break;
                        }

                    case Theme.Structures.LogonUI7.Modes.Wallpaper:
                        {
                            using (var b = new Bitmap(Program.GetWallpaper()))
                            {
                                bmp = (Bitmap)b.Clone();
                            }

                            break;
                        }

                    default:
                        {
                            bmp = (Bitmap)Color.Black.ToBitmap(Program.Computer.Screen.Bounds.Size);
                            break;
                        }

                }

                if (ReportProgress)
                    AddNode(TreeView, string.Format(Program.Lang.TM_RenderingCustomLogonUI_MayNotRespond), "info");

                if (ReportProgress)
                    AddNode(TreeView, string.Format("{0}:  " + Program.Lang.TM_RenderingCustomLogonUI, DateTime.Now.ToLongTimeString()), "info");

                if (LogonUI7.Grayscale)
                {
                    if (ReportProgress_Detailed)
                        AddNode(TreeView, Program.Lang.Verbose_GrayscaleLockScreenImg, "apply");
                    bmp = bmp.Grayscale();
                }

                if (LogonUI7.Blur)
                {
                    if (ReportProgress_Detailed)
                        AddNode(TreeView, Program.Lang.Verbose_BlurringLockScreenImg, "apply");
                    var imgF = new ImageProcessor.ImageFactory();
                    using (var b = new Bitmap(bmp))
                    {
                        imgF.Load(b);
                        imgF.GaussianBlur(LogonUI7.Blur_Intensity);
                        bmp = (Bitmap)imgF.Image;
                    }

                }

                if (LogonUI7.Noise)
                {
                    if (ReportProgress_Detailed)
                        AddNode(TreeView, Program.Lang.Verbose_NoiseLockScreenImg, "apply");
                    bmp = bmp.Noise(LogonUI7.Noise_Mode, (float)(LogonUI7.Noise_Intensity / 100d));
                }

                if (System.IO.File.Exists(lockimg))
                    FileSystem.Kill(lockimg);

                if (ReportProgress_Detailed)
                    AddNode(TreeView, string.Format(Program.Lang.Verbose_LockScreenImgSaved, lockimg), "info");

                bmp.Save(lockimg);

            }

        }

        public void Apply_CommandPrompt(TreeView TreeView = null)
        {
            if (CommandPrompt.Enabled)
            {
                Theme.Structures.Console.Save_Console_To_Registry("HKEY_CURRENT_USER", "", CommandPrompt, TreeView);
                if (Program.Settings.ThemeApplyingBehavior.CMD_OverrideUserPreferences)
                    Theme.Structures.Console.Save_Console_To_Registry("HKEY_CURRENT_USER", "%SystemRoot%_System32_cmd.exe", CommandPrompt, TreeView);

                if (Program.Settings.ThemeApplyingBehavior.CMD_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                {
                    Theme.Structures.Console.Save_Console_To_Registry(@"HKEY_USERS\.DEFAULT", "", CommandPrompt, TreeView);
                    Theme.Structures.Console.Save_Console_To_Registry(@"HKEY_USERS\.DEFAULT", "%SystemRoot%_System32_cmd.exe", CommandPrompt, TreeView);
                }
            }
        }

        public void Apply_PowerShell86(TreeView TreeView = null)
        {
            if (PowerShellx86.Enabled & Directory.Exists(Environment.GetEnvironmentVariable("WINDIR") + @"\System32\WindowsPowerShell\v1.0"))
            {
                Theme.Structures.Console.Save_Console_To_Registry("HKEY_CURRENT_USER", "%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", PowerShellx86, TreeView);
                if (Program.Settings.ThemeApplyingBehavior.PS86_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                {
                    Theme.Structures.Console.Save_Console_To_Registry(@"HKEY_USERS\.DEFAULT", "%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe", PowerShellx86, TreeView);
                }
            }
        }

        public void Apply_PowerShell64(TreeView TreeView = null)
        {
            if (PowerShellx64.Enabled & Directory.Exists(Environment.GetEnvironmentVariable("WINDIR") + @"\SysWOW64\WindowsPowerShell\v1.0"))
            {
                Theme.Structures.Console.Save_Console_To_Registry("HKEY_CURRENT_USER", "%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", PowerShellx64, TreeView);
                if (Program.Settings.ThemeApplyingBehavior.PS64_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                {
                    Theme.Structures.Console.Save_Console_To_Registry(@"HKEY_USERS\.DEFAULT", "%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe", PowerShellx64, TreeView);
                }
            }
        }

        public void Apply_Cursors(TreeView TreeView = null)
        {
            bool ReportProgress = Program.Settings.ThemeLog.VerboseLevel != WPSettings.Structures.ThemeLog.VerboseLevels.None && TreeView is not null;
            bool ReportProgress_Detailed = ReportProgress && Program.Settings.ThemeLog.VerboseLevel == WPSettings.Structures.ThemeLog.VerboseLevels.Detailed;

            EditReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Cursors", "", Cursor_Enabled);

            var sw = new Stopwatch();
            if (ReportProgress)
                AddNode(TreeView, string.Format("{0}: " + Program.Lang.TM_SavingCursorsColors, DateTime.Now.ToLongTimeString()), "info");

            sw.Reset();
            sw.Start();

            Theme.Structures.Cursor.Save_Cursors_To_Registry("Arrow", Cursor_Arrow, ReportProgress_Detailed ? TreeView : null);
            Theme.Structures.Cursor.Save_Cursors_To_Registry("Help", Cursor_Help, ReportProgress_Detailed ? TreeView : null);
            Theme.Structures.Cursor.Save_Cursors_To_Registry("AppLoading", Cursor_AppLoading, ReportProgress_Detailed ? TreeView : null);
            Theme.Structures.Cursor.Save_Cursors_To_Registry("Busy", Cursor_Busy, ReportProgress_Detailed ? TreeView : null);
            Theme.Structures.Cursor.Save_Cursors_To_Registry("Move", Cursor_Move, ReportProgress_Detailed ? TreeView : null);
            Theme.Structures.Cursor.Save_Cursors_To_Registry("NS", Cursor_NS, ReportProgress_Detailed ? TreeView : null);
            Theme.Structures.Cursor.Save_Cursors_To_Registry("EW", Cursor_EW, ReportProgress_Detailed ? TreeView : null);
            Theme.Structures.Cursor.Save_Cursors_To_Registry("NESW", Cursor_NESW, ReportProgress_Detailed ? TreeView : null);
            Theme.Structures.Cursor.Save_Cursors_To_Registry("NWSE", Cursor_NWSE, ReportProgress_Detailed ? TreeView : null);
            Theme.Structures.Cursor.Save_Cursors_To_Registry("Up", Cursor_Up, ReportProgress_Detailed ? TreeView : null);
            Theme.Structures.Cursor.Save_Cursors_To_Registry("Pen", Cursor_Pen, ReportProgress_Detailed ? TreeView : null);
            Theme.Structures.Cursor.Save_Cursors_To_Registry("None", Cursor_None, ReportProgress_Detailed ? TreeView : null);
            Theme.Structures.Cursor.Save_Cursors_To_Registry("Link", Cursor_Link, ReportProgress_Detailed ? TreeView : null);
            Theme.Structures.Cursor.Save_Cursors_To_Registry("Pin", Cursor_Pin, ReportProgress_Detailed ? TreeView : null);
            Theme.Structures.Cursor.Save_Cursors_To_Registry("Person", Cursor_Person, ReportProgress_Detailed ? TreeView : null);
            Theme.Structures.Cursor.Save_Cursors_To_Registry("IBeam", Cursor_IBeam, ReportProgress_Detailed ? TreeView : null);
            Theme.Structures.Cursor.Save_Cursors_To_Registry("Cross", Cursor_Cross, ReportProgress_Detailed ? TreeView : null);

            if (ReportProgress)
                AddNode(TreeView, string.Format(Program.Lang.TM_Time, sw.ElapsedMilliseconds / 1000d), "time");
            sw.Stop();

            if (Cursor_Enabled)
            {
                this.Execute(new MethodInvoker(() => ExportCursors(this, TreeView)), TreeView, Program.Lang.TM_RenderingCursors, Program.Lang.TM_RenderingCursors_Error, Program.Lang.TM_Time);

                if (Program.Settings.ThemeApplyingBehavior.AutoApplyCursors)
                {
                    this.Execute(new MethodInvoker(() =>
                        {
                            if (TreeView is not null)
                                AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Cursors.SETCURSORSHADOW.ToString(), 0, Cursor_Shadow, SPIF.UpdateINIFile.ToString()), "dll");
                            SystemParametersInfo((int)SPI.Cursors.SETCURSORSHADOW, 0, Cursor_Shadow, (int)SPIF.UpdateINIFile);

                            if (TreeView is not null)
                                AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Cursors.SETMOUSESONAR.ToString(), 0, Cursor_Sonar, SPIF.UpdateINIFile.ToString()), "dll");
                            SystemParametersInfo((int)SPI.Cursors.SETMOUSESONAR, 0, Cursor_Sonar, (int)SPIF.UpdateINIFile);

                            if (TreeView is not null)
                                AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Cursors.SETMOUSETRAILS.ToString(), 0, Cursor_Trails, SPIF.UpdateINIFile.ToString()), "dll");
                            SystemParametersInfo((int)SPI.Cursors.SETMOUSETRAILS, Cursor_Trails, 0, (int)SPIF.UpdateINIFile);

                            ApplyCursorsToReg("HKEY_CURRENT_USER", ReportProgress_Detailed ? TreeView : null);

                            if (Program.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                            {
                                EditReg(@"HKEY_USERS\.DEFAULT\Control Panel\Mouse", "MouseTrails", Cursor_Trails);
                                ApplyCursorsToReg(@"HKEY_USERS\.DEFAULT", ReportProgress_Detailed ? TreeView : null);
                            }

                        }), TreeView, Program.Lang.TM_ApplyingCursors, Program.Lang.TM_CursorsApplying_Error, Program.Lang.TM_Time);
                }
                else if (ReportProgress)
                    AddNode(TreeView, string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), Program.Lang.TM_Restricted_Cursors), "error");
            }

            else if (Program.Settings.ThemeApplyingBehavior.ResetCursorsToAero)
            {
                if (!Program.WXP)
                {
                    ResetCursorsToAero("HKEY_CURRENT_USER", ReportProgress_Detailed ? TreeView : null);
                    if (Program.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                        ResetCursorsToAero(@"HKEY_USERS\.DEFAULT");
                }

                else
                {
                    ResetCursorsToNone_XP("HKEY_CURRENT_USER", ReportProgress_Detailed ? TreeView : null);
                    if (Program.Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                        ResetCursorsToNone_XP(@"HKEY_USERS\.DEFAULT");

                }

            }

        }
        #endregion

        #region Cursors Render
        public void ExportCursors(Manager TM, TreeView TreeView = null)
        {
            bool ReportProgress = Program.Settings.ThemeLog.VerboseLevel != WPSettings.Structures.ThemeLog.VerboseLevels.None && TreeView is not null;
            bool ReportProgress_Detailed = ReportProgress && Program.Settings.ThemeLog.VerboseLevel == WPSettings.Structures.ThemeLog.VerboseLevels.Detailed;

            try { RenderCursor(Paths.CursorType.Arrow, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.Help, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.AppLoading, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.Busy, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.Pen, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.None, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.Move, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.Up, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.NS, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.EW, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.NESW, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.NWSE, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.Link, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.Pin, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.Person, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.IBeam, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
            try { RenderCursor(Paths.CursorType.Cross, TM, ReportProgress_Detailed ? TreeView : null); }
            catch { }
        }

        public void RenderCursor(Paths.CursorType Type, Manager TM, TreeView TreeView = null)
        {

            string CurName = "";

            switch (Type)
            {
                case Paths.CursorType.Arrow:
                    {
                        CurName = "Arrow";
                        break;
                    }

                case Paths.CursorType.Help:
                    {
                        CurName = "Help";
                        break;
                    }

                case Paths.CursorType.Busy:
                    {
                        CurName = "Busy";
                        break;
                    }

                case Paths.CursorType.AppLoading:
                    {
                        CurName = "AppLoading";
                        break;
                    }

                case Paths.CursorType.None:
                    {
                        CurName = "None";
                        break;
                    }

                case Paths.CursorType.Move:
                    {
                        CurName = "Move";
                        break;
                    }

                case Paths.CursorType.Up:
                    {
                        CurName = "Up";
                        break;
                    }

                case Paths.CursorType.NS:
                    {
                        CurName = "NS";
                        break;
                    }

                case Paths.CursorType.EW:
                    {
                        CurName = "EW";
                        break;
                    }

                case Paths.CursorType.NESW:
                    {
                        CurName = "NESW";
                        break;
                    }

                case Paths.CursorType.NWSE:
                    {
                        CurName = "NWSE";
                        break;
                    }

                case Paths.CursorType.Pen:
                    {
                        CurName = "Pen";
                        break;
                    }

                case Paths.CursorType.Link:
                    {
                        CurName = "Link";
                        break;
                    }

                case Paths.CursorType.Pin:
                    {
                        CurName = "Pin";
                        break;
                    }

                case Paths.CursorType.Person:
                    {
                        CurName = "Person";
                        break;
                    }

                case Paths.CursorType.IBeam:
                    {
                        CurName = "IBeam";
                        break;
                    }

                case Paths.CursorType.Cross:
                    {
                        CurName = "Cross";
                        break;
                    }

            }

            if (TreeView is not null)
                AddNode(TreeView, string.Format(Program.Lang.Verbose_RenderingCursor, CurName), "pe_patch");

            if (!(Type == Paths.CursorType.Busy) & !(Type == Paths.CursorType.AppLoading))
            {

                if (!Directory.Exists(Program.PATH_CursorsWP))
                    Directory.CreateDirectory(Program.PATH_CursorsWP);
                string Path = string.Format(Program.PATH_CursorsWP + @"\{0}.cur", CurName);

                var fs = new FileStream(Path, FileMode.Create);
                var EO = new EOIcoCurWriter(fs, 7, EOIcoCurWriter.IcoCurType.Cursor);

                for (float i = 1f; i <= 4f; i += 0.5f)
                {
                    var bmp = new Bitmap((int)Math.Round(32f * i), (int)Math.Round(32f * i), System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                    var HotPoint = new Point(1, 1);

                    switch (Type)
                    {
                        case Paths.CursorType.Arrow:
                            {
                                var CurOptions = new CursorOptions(Cursor_Arrow) { Cursor = Paths.CursorType.Arrow, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1, 1);
                                break;
                            }

                        case Paths.CursorType.Help:
                            {
                                var CurOptions = new CursorOptions(Cursor_Help) { Cursor = Paths.CursorType.Help, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1, 1);
                                break;
                            }

                        case Paths.CursorType.None:
                            {
                                var CurOptions = new CursorOptions(Cursor_None) { Cursor = Paths.CursorType.None, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(8f * i), 1 + (int)Math.Round(8f * i));
                                break;
                            }

                        case Paths.CursorType.Move:
                            {
                                var CurOptions = new CursorOptions(Cursor_Move) { Cursor = Paths.CursorType.Move, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(11f * i), 1 + (int)Math.Round(11f * i));
                                break;
                            }

                        case Paths.CursorType.Up:
                            {
                                var CurOptions = new CursorOptions(Cursor_Up) { Cursor = Paths.CursorType.Up, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(4f * i), 1);
                                break;
                            }

                        case Paths.CursorType.NS:
                            {
                                var CurOptions = new CursorOptions(Cursor_NS) { Cursor = Paths.CursorType.NS, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(4f * i), 1 + (int)Math.Round(11f * i));
                                break;
                            }

                        case Paths.CursorType.EW:
                            {
                                var CurOptions = new CursorOptions(Cursor_EW) { Cursor = Paths.CursorType.EW, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point((int)Math.Round(1f + 11f * i), (int)Math.Round(1f + 4f * i));
                                break;
                            }

                        case Paths.CursorType.NESW:
                            {
                                var CurOptions = new CursorOptions(Cursor_NESW) { Cursor = Paths.CursorType.NESW, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(8f * i), 1 + (int)Math.Round(8f * i));
                                break;
                            }

                        case Paths.CursorType.NWSE:
                            {
                                var CurOptions = new CursorOptions(Cursor_NWSE) { Cursor = Paths.CursorType.NWSE, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(8f * i), 1 + (int)Math.Round(8f * i));
                                break;
                            }

                        case Paths.CursorType.Pen:
                            {
                                var CurOptions = new CursorOptions(Cursor_Pen) { Cursor = Paths.CursorType.Pen, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1, 1);
                                break;
                            }

                        case Paths.CursorType.Link:
                            {
                                var CurOptions = new CursorOptions(Cursor_Link) { Cursor = Paths.CursorType.Link, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(6f * i), CurOptions.ArrowStyle != Paths.ArrowStyle.Classic ? 1 : 2);
                                break;
                            }

                        case Paths.CursorType.Pin:
                            {
                                var CurOptions = new CursorOptions(Cursor_Pin) { Cursor = Paths.CursorType.Pin, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(6f * i), CurOptions.ArrowStyle != Paths.ArrowStyle.Classic ? 1 : 2);
                                break;
                            }

                        case Paths.CursorType.Person:
                            {
                                var CurOptions = new CursorOptions(Cursor_Person) { Cursor = Paths.CursorType.Person, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(6f * i), CurOptions.ArrowStyle != Paths.ArrowStyle.Classic ? 1 : 2);
                                break;
                            }

                        case Paths.CursorType.IBeam:
                            {
                                var CurOptions = new CursorOptions(Cursor_IBeam) { Cursor = Paths.CursorType.IBeam, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(4f * i), 1 + (int)Math.Round(9f * i));
                                break;
                            }

                        case Paths.CursorType.Cross:
                            {
                                var CurOptions = new CursorOptions(Cursor_Cross) { Cursor = Paths.CursorType.Cross, LineThickness = 1f, Scale = i, _Angle = 0f };
                                bmp = Paths.Draw(CurOptions);
                                HotPoint = new Point(1 + (int)Math.Round(9f * i), 1 + (int)Math.Round(9f * i));
                                break;
                            }

                    }

                    EO.WriteBitmap(bmp, null, HotPoint);

                }

                fs.Close();

                if (TreeView is not null)
                    AddNode(TreeView, string.Format(Program.Lang.Verbose_CursorRenderedInto, Path), "info");
            }

            else
            {
                var HotPoint = new Point(1, 1);

                for (float i = 1f; i <= 4f; i += 1f)
                {
                    var BMPList = new List<Bitmap>();
                    BMPList.Clear();

                    #region Add angles bitmaps from 180 deg to 180 deg (Cycle)

                    for (int ang = 180; ang <= 360; ang += +10)
                    {
                        Bitmap bm;

                        if (Type == Paths.CursorType.AppLoading)
                        {
                            var CurOptions = new CursorOptions(Cursor_AppLoading) { Cursor = Paths.CursorType.AppLoading, LineThickness = 1f, Scale = i, _Angle = ang };
                            bm = new Bitmap(Paths.Draw(CurOptions));
                            HotPoint = new Point(1, 1 + (int)Math.Round(8f * i));
                        }

                        else
                        {
                            var CurOptions = new CursorOptions(Cursor_Busy) { Cursor = Paths.CursorType.Busy, LineThickness = 1f, Scale = i, _Angle = ang };
                            bm = new Bitmap(Paths.Draw(CurOptions));

                            HotPoint = new Point((CurOptions.CircleStyle != Paths.CircleStyle.Classic ? 1 : 2) + (int)Math.Round(11f * i), 1 + (int)Math.Round(11f * i));

                        }

                        BMPList.Add(bm);
                    }

                    for (int ang = 0; ang <= 180; ang += +10)
                    {
                        Bitmap bm;

                        if (Type == Paths.CursorType.AppLoading)
                        {
                            var CurOptions = new CursorOptions(Cursor_AppLoading) { Cursor = Paths.CursorType.AppLoading, LineThickness = 1f, Scale = i, _Angle = ang };
                            bm = new Bitmap(Paths.Draw(CurOptions));
                            HotPoint = new Point(1, 1 + (int)Math.Round(8f * i));
                        }

                        else
                        {
                            var CurOptions = new CursorOptions(Cursor_Busy) { Cursor = Paths.CursorType.Busy, LineThickness = 1f, Scale = i, _Angle = ang };
                            bm = new Bitmap(Paths.Draw(CurOptions));
                            HotPoint = new Point((CurOptions.CircleStyle != Paths.CircleStyle.Classic ? 1 : 2) + (int)Math.Round(11f * i), 1 + (int)Math.Round(11f * i));

                        }

                        BMPList.Add(bm);
                    }

                    #endregion

                    int Count = BMPList.Count;
                    uint[] frameRates = new uint[Count];
                    uint[] seqNums = new uint[Count];
                    int Speed = 2;

                    for (int ixx = 0, loopTo = Count - 1; ixx <= loopTo; ixx++)
                    {
                        frameRates[ixx] = Convert.ToUInt32(Speed);
                        seqNums[ixx] = (uint)ixx;
                    }

                    if (!Directory.Exists(Program.PATH_CursorsWP))
                        Directory.CreateDirectory(Program.PATH_CursorsWP);
                    var fs = new FileStream(string.Format(Program.PATH_CursorsWP + @"\{0}_{1}x.ani", CurName, i), FileMode.Create);

                    var AN = new EOANIWriter(fs, (uint)Count, (uint)Speed, frameRates, seqNums, null, null, HotPoint);

                    for (int ix = 0, loopTo1 = Count - 1; ix <= loopTo1; ix++)
                        AN.WriteFrame32(BMPList[ix]);

                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(Program.Lang.Verbose_CursorRenderedInto, string.Format(Program.PATH_CursorsWP + @"\{0}_{1}x.ani", CurName, i)), "info");

                    fs.Close();
                }

            }

        }

        public void ApplyCursorsToReg(string scopeReg = "HKEY_CURRENT_USER", TreeView TreeView = null)
        {
            string Path = Program.PATH_CursorsWP;

            string RegValue;
            RegValue = string.Format(@"{0}\{1}", Path, "Arrow.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "Help.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "AppLoading_1x.ani");
            RegValue += string.Format(@",{0}\{1}", Path, "Busy_1x.ani");
            RegValue += string.Format(@",{0}\{1}", Path, "Cross.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "IBeam.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "Pen.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "None.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "NS.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "EW.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "NWSE.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "NESW.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "Move.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "Up.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "Link.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "Pin.cur");
            RegValue += string.Format(@",{0}\{1}", Path, "Person.cur");

            EditReg(scopeReg + @"\Control Panel\Cursors\Schemes", "WinPaletter", RegValue, RegistryValueKind.String);
            EditReg(scopeReg + @"\Control Panel\Cursors", "", "WinPaletter", RegistryValueKind.String);
            EditReg(scopeReg + @"\Control Panel\Cursors", "CursorBaseSize", 32, RegistryValueKind.DWord);
            EditReg(scopeReg + @"\Control Panel\Cursors", "Scheme Source", 1, RegistryValueKind.DWord);

            string x = string.Format(@"{0}\{1}", Path, "AppLoading_1x.ani");
            EditReg(scopeReg + @"\Control Panel\Cursors", "AppStarting", x, RegistryValueKind.String);

            if (TreeView is not null)
                AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_APPSTARTING.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_APPSTARTING);

            x = string.Format(@"{0}\{1}", Path, "Arrow.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "Arrow", x, RegistryValueKind.String);
            if (TreeView is not null)
                AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_NORMAL.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_NORMAL);

            x = string.Format(@"{0}\{1}", Path, "Cross.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "Crosshair", x, RegistryValueKind.String);
            if (TreeView is not null)
                AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_CROSS.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_CROSS);

            x = string.Format(@"{0}\{1}", Path, "Link.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "Hand", x, RegistryValueKind.String);
            if (TreeView is not null)
                AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_HAND.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_HAND);

            x = string.Format(@"{0}\{1}", Path, "Help.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "Help", x, RegistryValueKind.String);
            if (TreeView is not null)
                AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_HELP.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_HELP);

            x = string.Format(@"{0}\{1}", Path, "IBeam.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "IBeam", x, RegistryValueKind.String);
            if (TreeView is not null)
                AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_IBEAM.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_IBEAM);

            x = string.Format(@"{0}\{1}", Path, "None.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "No", x, RegistryValueKind.String);
            if (TreeView is not null)
                AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_NO.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_NO);

            x = string.Format(@"{0}\{1}", Path, "Pen.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "NWPen", x, RegistryValueKind.String);
            // User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_)

            x = string.Format(@"{0}\{1}", Path, "Person.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "Person", x, RegistryValueKind.String);
            // User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

            x = string.Format(@"{0}\{1}", Path, "Pin.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "Pin", x, RegistryValueKind.String);
            // User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

            x = string.Format(@"{0}\{1}", Path, "Move.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "SizeAll", x, RegistryValueKind.String);
            if (TreeView is not null)
                AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZEALL.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZEALL);

            x = string.Format(@"{0}\{1}", Path, "NESW.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "SizeNESW", x, RegistryValueKind.String);
            if (TreeView is not null)
                AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZENESW.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZENESW);

            x = string.Format(@"{0}\{1}", Path, "NS.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "SizeNS", x, RegistryValueKind.String);
            if (TreeView is not null)
                AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZENS.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZENS);

            x = string.Format(@"{0}\{1}", Path, "NWSE.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "SizeNWSE", x, RegistryValueKind.String);
            if (TreeView is not null)
                AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZENWSE.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZENWSE);

            x = string.Format(@"{0}\{1}", Path, "EW.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "SizeWE", x, RegistryValueKind.String);
            if (TreeView is not null)
                AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZEWE.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZEWE);

            x = string.Format(@"{0}\{1}", Path, "Up.cur");
            EditReg(scopeReg + @"\Control Panel\Cursors", "UpArrow", x, RegistryValueKind.String);
            if (TreeView is not null)
                AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_UP.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_UP);

            x = string.Format(@"{0}\{1}", Path, "Busy_1x.ani");
            EditReg(scopeReg + @"\Control Panel\Cursors", "Wait", x, RegistryValueKind.String);
            if (TreeView is not null)
                AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_WAIT.ToString()), "dll");
            SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_WAIT);

            if (TreeView is not null)
                AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Cursors.SETCURSORS.ToString(), 0, 0, SPIF.UpdateINIFile.ToString()), "dll");
            SystemParametersInfo((int)SPI.Cursors.SETCURSORS, 0, 0, (int)(SPIF.UpdateINIFile | SPIF.UpdateINIFile));
        }

        public static void ResetCursorsToAero(string scopeReg = "HKEY_CURRENT_USER", TreeView TreeView = null)
        {
            try
            {
                string path = @"%SystemRoot%\Cursors";

                if (scopeReg.ToUpper() == "HKEY_CURRENT_USER")
                {
                    if (Registry.CurrentUser.OpenSubKey(@"Control Panel\Cursors\Schemes", false) is not null)
                    {
                        if (TreeView is not null)
                            AddNode(TreeView, string.Format(Program.Lang.Verbose_DelCursorWPFromReg, @"HKEY_CURRENT_USER\Control Panel\Cursors\Schemes"), "reg_delete");
                        var rx = Registry.CurrentUser.OpenSubKey(@"Control Panel\Cursors\Schemes", true);
                        rx.DeleteValue("WinPaletter", false);
                        rx.Close();
                    }
                }

                EditReg(scopeReg + @"\Control Panel\Cursors", "", "Windows Default", RegistryValueKind.String);
                EditReg(scopeReg + @"\Control Panel\Cursors", "CursorBaseSize", 32, RegistryValueKind.DWord);
                EditReg(scopeReg + @"\Control Panel\Cursors", "Scheme Source", 2, RegistryValueKind.DWord);

                string x = string.Format(@"{0}\{1}", path, "aero_working.ani");
                EditReg(scopeReg + @"\Control Panel\Cursors", "AppStarting", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_APPSTARTING.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_APPSTARTING);
                }

                x = string.Format(@"{0}\{1}", path, "aero_arrow.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "Arrow", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_NORMAL.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_NORMAL);
                }

                x = string.Format("");
                EditReg(scopeReg + @"\Control Panel\Cursors", "Crosshair", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_CROSS.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_CROSS);
                }

                x = string.Format(@"{0}\{1}", path, "aero_link.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "Hand", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_HAND.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_HAND);
                }

                x = string.Format(@"{0}\{1}", path, "aero_helpsel.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "Help", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_HELP.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_HELP);
                }

                x = string.Format("");
                EditReg(scopeReg + @"\Control Panel\Cursors", "IBeam", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_IBEAM.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_IBEAM);
                }

                x = string.Format(@"{0}\{1}", path, "aero_unavail.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "No", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_NO.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_NO);
                }

                x = string.Format(@"{0}\{1}", path, "aero_pen.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "NWPen", x, RegistryValueKind.String);
                // If IO.System.IO.File.Exists(X) then User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_)

                x = string.Format(@"{0}\{1}", path, "aero_person.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "Person", x, RegistryValueKind.String);
                // If IO.System.IO.File.Exists(X) then User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

                x = string.Format(@"{0}\{1}", path, "aero_pin.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "Pin", x, RegistryValueKind.String);
                // If IO.System.IO.File.Exists(X) then User32.SetSystemCursor(User32.LoadCursorFromFile(x), User32.OCR_SYSTEM_CURSORS.OCR_APPSTARTING)

                x = string.Format(@"{0}\{1}", path, "aero_move.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeAll", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZEALL.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZEALL);
                }

                x = string.Format(@"{0}\{1}", path, "aero_nesw.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeNESW", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZENESW.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZENESW);
                }

                x = string.Format(@"{0}\{1}", path, "aero_ns.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeNS", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZENS.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZENS);
                }

                x = string.Format(@"{0}\{1}", path, "aero_nwse.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeNWSE", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZENWSE.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZENWSE);
                }

                x = string.Format(@"{0}\{1}", path, "aero_ew.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeWE", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZEWE.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZEWE);
                }

                x = string.Format(@"{0}\{1}", path, "aero_up.cur");
                EditReg(scopeReg + @"\Control Panel\Cursors", "UpArrow", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_UP.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_UP);
                }

                x = string.Format(@"{0}\{1}", path, "aero_busy.ani");
                EditReg(scopeReg + @"\Control Panel\Cursors", "Wait", x, RegistryValueKind.String);
                if (System.IO.File.Exists(x))
                {
                    if (TreeView is not null)
                        AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_WAIT.ToString()), "dll");
                    SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_WAIT);
                }

                if (TreeView is not null)
                    AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Cursors.SETCURSORS.ToString(), 0, 0, SPIF.UpdateINIFile.ToString()), "dll");
                SystemParametersInfo((int)SPI.Cursors.SETCURSORS, 0, 0, (int)(SPIF.UpdateINIFile | SPIF.UpdateINIFile));
            }

            catch (Exception ex)
            {

                if (MsgBox(Program.Lang.TM_RestoreCursorsError, MessageBoxButtons.OKCancel, MessageBoxIcon.Error, Program.Lang.TM_RestoreCursorsErrorPressOK, "", "", "", "", Program.Lang.TM_RestoreCursorsTip, Ookii.Dialogs.WinForms.TaskDialogIcon.Information) == DialogResult.OK)
                    Forms.BugReport.ThrowError(ex);

            }

        }

        public static void ResetCursorsToNone_XP(string scopeReg = "HKEY_CURRENT_USER", TreeView TreeView = null)
        {
            try
            {
                string path = @"%SystemRoot%\Cursors";

                if (scopeReg.ToUpper() == "HKEY_CURRENT_USER")
                {
                    try
                    {
                        if (Registry.CurrentUser.OpenSubKey(@"Control Panel\Cursors\Schemes", false) is not null)
                        {
                            if (TreeView is not null)
                                AddNode(TreeView, string.Format(Program.Lang.Verbose_DelCursorWPFromReg, @"HKEY_CURRENT_USER\Control Panel\Cursors\Schemes"), "reg_delete");
                            var rx = Registry.CurrentUser.OpenSubKey(@"Control Panel\Cursors\Schemes", true);
                            rx.DeleteValue("WinPaletter", false);
                            rx.Close();
                        }
                    }
                    finally
                    {
                        Registry.CurrentUser.Close();
                    }
                }

                EditReg(scopeReg + @"\Control Panel\Cursors", "", "Windows Default", RegistryValueKind.String);
                EditReg(scopeReg + @"\Control Panel\Cursors", "CursorBaseSize", 32, RegistryValueKind.DWord);
                EditReg(scopeReg + @"\Control Panel\Cursors", "Scheme Source", 2, RegistryValueKind.DWord);

                string x = "";
                EditReg(scopeReg + @"\Control Panel\Cursors", "AppStarting", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_APPSTARTING.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_APPSTARTING);

                EditReg(scopeReg + @"\Control Panel\Cursors", "Arrow", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_NORMAL.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_NORMAL);

                EditReg(scopeReg + @"\Control Panel\Cursors", "Crosshair", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_CROSS.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_CROSS);

                EditReg(scopeReg + @"\Control Panel\Cursors", "Hand", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_HAND.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_HAND);

                EditReg(scopeReg + @"\Control Panel\Cursors", "Help", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_HELP.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_HELP);

                EditReg(scopeReg + @"\Control Panel\Cursors", "IBeam", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_IBEAM.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_IBEAM);

                EditReg(scopeReg + @"\Control Panel\Cursors", "No", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_NO.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_NO);

                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeAll", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZEALL.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZEALL);

                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeNESW", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZENESW.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZENESW);

                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeNS", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZENS.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZENS);

                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeNWSE", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZENWSE.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZENWSE);

                EditReg(scopeReg + @"\Control Panel\Cursors", "SizeWE", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_SIZEWE.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_SIZEWE);

                EditReg(scopeReg + @"\Control Panel\Cursors", "UpArrow", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_UP.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_UP);

                EditReg(scopeReg + @"\Control Panel\Cursors", "Wait", x, RegistryValueKind.String);
                if (TreeView is not null)
                    AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SSC, "User32", "SetSystemCursor", x, OCR_SYSTEM_CURSORS.OCR_WAIT.ToString()), "dll");
                SetSystemCursor(User32.LoadCursorFromFile(x), (int)OCR_SYSTEM_CURSORS.OCR_WAIT);

                if (TreeView is not null)
                    AddNode(TreeView, string.Format(Program.Lang.Verbose_User32_SPI, "User32", "SystemParameterInfo", SPI.Cursors.SETCURSORS.ToString(), 0, 0, SPIF.UpdateINIFile.ToString()), "dll");
                SystemParametersInfo((int)SPI.Cursors.SETCURSORS, 0, 0, (int)(SPIF.UpdateINIFile | SPIF.UpdateINIFile));
            }

            catch (Exception ex)
            {

                if (MsgBox(Program.Lang.TM_RestoreCursorsError, MessageBoxButtons.OKCancel, MessageBoxIcon.Error, Program.Lang.TM_RestoreCursorsErrorPressOK, "", "", "", "", Program.Lang.TM_RestoreCursorsTip, Ookii.Dialogs.WinForms.TaskDialogIcon.Information) == DialogResult.OK)
                    Forms.BugReport.ThrowError(ex);

            }

        }
        #endregion

        #region Comparisons

        /// <summary>
        /// Checks if two objects are equal
        /// </summary>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            bool _Equals = true;

            if (Info != ((Manager)obj).Info)
                _Equals = false;
            if (Windows11 != ((Manager)obj).Windows11)
                _Equals = false;
            if (LogonUI10x != ((Manager)obj).LogonUI10x)
                _Equals = false;
            if (Windows81 != ((Manager)obj).Windows81)
                _Equals = false;
            if (Windows7 != ((Manager)obj).Windows7)
                _Equals = false;
            if (WindowsVista != ((Manager)obj).WindowsVista)
                _Equals = false;
            if (WindowsXP != ((Manager)obj).WindowsXP)
                _Equals = false;
            if (LogonUI7 != ((Manager)obj).LogonUI7)
                _Equals = false;
            if (LogonUIXP != ((Manager)obj).LogonUIXP)
                _Equals = false;
            if (Win32 != ((Manager)obj).Win32)
                _Equals = false;
            if (WindowsEffects != ((Manager)obj).WindowsEffects)
                _Equals = false;
            if (MetricsFonts != ((Manager)obj).MetricsFonts)
                _Equals = false;
            if (AltTab != ((Manager)obj).AltTab)
                _Equals = false;
            if (WallpaperTone_W11 != ((Manager)obj).WallpaperTone_W11)
                _Equals = false;
            if (WallpaperTone_W10 != ((Manager)obj).WallpaperTone_W10)
                _Equals = false;
            if (WallpaperTone_W81 != ((Manager)obj).WallpaperTone_W81)
                _Equals = false;
            if (WallpaperTone_W7 != ((Manager)obj).WallpaperTone_W7)
                _Equals = false;
            if (WallpaperTone_WVista != ((Manager)obj).WallpaperTone_WVista)
                _Equals = false;
            if (WallpaperTone_WXP != ((Manager)obj).WallpaperTone_WXP)
                _Equals = false;
            if (ScreenSaver != ((Manager)obj).ScreenSaver)
                _Equals = false;
            if (Sounds != ((Manager)obj).Sounds)
                _Equals = false;
            if (Wallpaper != ((Manager)obj).Wallpaper)
                _Equals = false;
            if (AppTheme != ((Manager)obj).AppTheme)
                _Equals = false;

            if (Cursor_Enabled != ((Manager)obj).Cursor_Enabled)
                _Equals = false;
            if (Cursor_Arrow != ((Manager)obj).Cursor_Arrow)
                _Equals = false;
            if (Cursor_Help != ((Manager)obj).Cursor_Help)
                _Equals = false;
            if (Cursor_AppLoading != ((Manager)obj).Cursor_AppLoading)
                _Equals = false;
            if (Cursor_Busy != ((Manager)obj).Cursor_Busy)
                _Equals = false;
            if (Cursor_Move != ((Manager)obj).Cursor_Move)
                _Equals = false;
            if (Cursor_NS != ((Manager)obj).Cursor_NS)
                _Equals = false;
            if (Cursor_EW != ((Manager)obj).Cursor_EW)
                _Equals = false;
            if (Cursor_NESW != ((Manager)obj).Cursor_NESW)
                _Equals = false;
            if (Cursor_NWSE != ((Manager)obj).Cursor_NWSE)
                _Equals = false;
            if (Cursor_Up != ((Manager)obj).Cursor_Up)
                _Equals = false;
            if (Cursor_Pen != ((Manager)obj).Cursor_Pen)
                _Equals = false;
            if (Cursor_None != ((Manager)obj).Cursor_None)
                _Equals = false;
            if (Cursor_Link != ((Manager)obj).Cursor_Link)
                _Equals = false;
            if (Cursor_Pin != ((Manager)obj).Cursor_Pin)
                _Equals = false;
            if (Cursor_Person != ((Manager)obj).Cursor_Person)
                _Equals = false;
            if (Cursor_IBeam != ((Manager)obj).Cursor_IBeam)
                _Equals = false;
            if (Cursor_Cross != ((Manager)obj).Cursor_Cross)
                _Equals = false;

            if (CommandPrompt != ((Manager)obj).CommandPrompt)
                _Equals = false;
            if (PowerShellx86 != ((Manager)obj).PowerShellx86)
                _Equals = false;
            if (PowerShellx64 != ((Manager)obj).PowerShellx64)
                _Equals = false;
            // If Terminal <> DirectCast(obj, Manager).Terminal Then _Equals = False
            // If TerminalPreview <> DirectCast(obj, Manager).TerminalPreview Then _Equals = False

            return _Equals;
        }

        /// <summary>
        /// Checks if two theme managers are equal
        /// </summary>
        /// <param name="TM1">Theme manager #1</param>
        /// <param name="TM2">Theme manager #2</param>
        /// <returns></returns>
        public static bool operator ==(Manager TM1, Manager TM2) => (bool)TM1.Equals(TM2);

        /// <summary>
        /// Checks if two theme managers are not equal
        /// </summary>
        /// <param name="TM1">Theme manager #1</param>
        /// <param name="TM2">Theme manager #2</param>
        /// <returns></returns>
        public static bool operator !=(Manager TM1, Manager TM2) => !(TM1 == TM2);

        #endregion
    }
}