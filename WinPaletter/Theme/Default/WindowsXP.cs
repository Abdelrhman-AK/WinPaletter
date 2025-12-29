using System.Drawing;
using WinPaletter.Theme.Structures;

namespace WinPaletter.Theme
{
    public static partial class Default
    {
        /// <summary>
        /// Default Windows XP theme as a <see cref="Manager"/> object
        /// </summary>
        /// <returns><see cref="Manager"/> </returns>
        /// 
        public static Manager WindowsXP
        {
            get
            {
                if (_windowsXP == null) _windowsXP = w10();
                return _windowsXP;
            }
        }
        private static Manager _windowsXP = wxp();
        private static Manager wxp()
        {
            Manager TM = new();

            Info Info = TM.Info;
            Info.ThemeName = "Default Windows XP";
            Info.Description = "Initial, like the default look after a fresh Windows XP setup—clean, untouched, and ready for customization.";
            Info.ThemeVersion = "1.0.0.0";
            Info.Author = "Microsoft";
            Info.AuthorSocialMediaLink = "https://www.microsoft.com";
            Info.AppVersion = Program.Version;

            WindowsXP WinXP = TM.WindowsXP;
            WinXP.Enabled = true;
            WinXP.VisualStyles.VisualStylesType = VisualStyles.DefaultVisualStyles.LunaBlue;

            Structures.LogonUIXP logonUI = TM.LogonUIXP;
            logonUI.Enabled = true;

            Structures.Win32UI Win32 = TM.Win32;
            Win32.Enabled = true;
            Win32.ActiveBorder = Color.FromArgb(212, 208, 200);
            Win32.ActiveTitle = Color.FromArgb(0, 84, 227);
            Win32.AppWorkspace = Color.FromArgb(128, 128, 128);
            Win32.Background = Color.FromArgb(0, 78, 152);
            Win32.ButtonAlternateFace = Color.FromArgb(181, 181, 181);
            Win32.ButtonDkShadow = Color.FromArgb(113, 111, 100);
            Win32.ButtonFace = Color.FromArgb(236, 233, 216);
            Win32.ButtonHilight = Color.FromArgb(255, 255, 255);
            Win32.ButtonLight = Color.FromArgb(241, 239, 226);
            Win32.ButtonShadow = Color.FromArgb(172, 168, 153);
            Win32.ButtonText = Color.FromArgb(0, 0, 0);
            Win32.GradientActiveTitle = Color.FromArgb(61, 149, 255);
            Win32.GradientInactiveTitle = Color.FromArgb(157, 185, 235);
            Win32.GrayText = Color.FromArgb(172, 168, 153);
            Win32.HilightText = Color.FromArgb(255, 255, 255);
            Win32.HotTrackingColor = Color.FromArgb(0, 0, 128);
            Win32.InactiveBorder = Color.FromArgb(212, 208, 200);
            Win32.InactiveTitle = Color.FromArgb(212, 208, 200);
            Win32.InactiveTitleText = Color.FromArgb(216, 228, 248);
            Win32.InfoText = Color.FromArgb(0, 0, 0);
            Win32.InfoWindow = Color.FromArgb(255, 255, 225);
            Win32.Menu = Color.FromArgb(255, 255, 255);
            Win32.MenuBar = Color.FromArgb(236, 233, 216);
            Win32.MenuText = Color.FromArgb(0, 0, 0);
            Win32.Scrollbar = Color.FromArgb(212, 208, 200);
            Win32.TitleText = Color.FromArgb(255, 255, 255);
            Win32.Window = Color.FromArgb(255, 255, 255);
            Win32.WindowFrame = Color.FromArgb(0, 0, 0);
            Win32.WindowText = Color.FromArgb(0, 0, 0);
            Win32.Hilight = Color.FromArgb(49, 106, 197);
            Win32.MenuHilight = Color.FromArgb(49, 106, 197);
            Win32.Desktop = Color.FromArgb(0, 0, 0);

            Console CMD = TM.CommandPrompt;
            CMD.Enabled = true;
            CMD.ColorTable00 = Color.FromArgb(12, 12, 12);
            CMD.ColorTable01 = Color.FromArgb(0, 55, 218);
            CMD.ColorTable02 = Color.FromArgb(19, 161, 14);
            CMD.ColorTable03 = Color.FromArgb(58, 150, 221);
            CMD.ColorTable04 = Color.FromArgb(197, 15, 31);
            CMD.ColorTable05 = Color.FromArgb(136, 23, 152);
            CMD.ColorTable06 = Color.FromArgb(193, 156, 0);
            CMD.ColorTable07 = Color.FromArgb(204, 204, 204);
            CMD.ColorTable08 = Color.FromArgb(118, 118, 118);
            CMD.ColorTable09 = Color.FromArgb(59, 120, 255);
            CMD.ColorTable10 = Color.FromArgb(22, 198, 12);
            CMD.ColorTable11 = Color.FromArgb(97, 214, 214);
            CMD.ColorTable12 = Color.FromArgb(231, 72, 86);
            CMD.ColorTable13 = Color.FromArgb(180, 0, 158);
            CMD.ColorTable14 = Color.FromArgb(249, 241, 165);
            CMD.ColorTable15 = Color.FromArgb(242, 242, 242);
            CMD.PopupBackground = 15;
            CMD.PopupForeground = 5;
            CMD.ScreenColorsForeground = 7;
            CMD.ScreenColorsBackground = 0;
            CMD.FaceName = "Consolas";
            CMD.PixelWidth = 18;
            CMD.FontRaster = true;
            CMD.W10_1909_ForceV2 = false;

            Console PS86 = TM.PowerShellx86;
            PS86.Enabled = true;
            PS86.ColorTable00 = Color.FromArgb(12, 12, 12);
            PS86.ColorTable01 = Color.FromArgb(0, 55, 218);
            PS86.ColorTable02 = Color.FromArgb(19, 161, 14);
            PS86.ColorTable03 = Color.FromArgb(58, 150, 221);
            PS86.ColorTable04 = Color.FromArgb(197, 15, 31);
            PS86.ColorTable05 = Color.FromArgb(1, 36, 86);
            PS86.ColorTable06 = Color.FromArgb(238, 237, 240);
            PS86.ColorTable07 = Color.FromArgb(204, 204, 204);
            PS86.ColorTable08 = Color.FromArgb(118, 118, 118);
            PS86.ColorTable09 = Color.FromArgb(59, 120, 255);
            PS86.ColorTable10 = Color.FromArgb(22, 198, 12);
            PS86.ColorTable11 = Color.FromArgb(97, 214, 214);
            PS86.ColorTable12 = Color.FromArgb(231, 72, 86);
            PS86.ColorTable13 = Color.FromArgb(180, 0, 158);
            PS86.ColorTable14 = Color.FromArgb(249, 241, 165);
            PS86.ColorTable15 = Color.FromArgb(242, 242, 242);
            PS86.PopupBackground = 15;
            PS86.PopupForeground = 3;
            PS86.ScreenColorsForeground = 6;
            PS86.ScreenColorsBackground = 5;
            PS86.FaceName = "Consolas";
            PS86.PixelWidth = 14;
            PS86.FontRaster = true;
            PS86.W10_1909_ForceV2 = false;

            Console PS64 = TM.PowerShellx64;
            PS64.Enabled = true;
            PS64.ColorTable00 = Color.FromArgb(12, 12, 12);
            PS64.ColorTable01 = Color.FromArgb(0, 55, 218);
            PS64.ColorTable02 = Color.FromArgb(19, 161, 14);
            PS64.ColorTable03 = Color.FromArgb(58, 150, 221);
            PS64.ColorTable04 = Color.FromArgb(197, 15, 31);
            PS64.ColorTable05 = Color.FromArgb(1, 36, 86);
            PS64.ColorTable06 = Color.FromArgb(238, 237, 240);
            PS64.ColorTable07 = Color.FromArgb(204, 204, 204);
            PS64.ColorTable08 = Color.FromArgb(118, 118, 118);
            PS64.ColorTable09 = Color.FromArgb(59, 120, 255);
            PS64.ColorTable10 = Color.FromArgb(22, 198, 12);
            PS64.ColorTable11 = Color.FromArgb(97, 214, 214);
            PS64.ColorTable12 = Color.FromArgb(231, 72, 86);
            PS64.ColorTable13 = Color.FromArgb(180, 0, 158);
            PS64.ColorTable14 = Color.FromArgb(249, 241, 165);
            PS64.ColorTable15 = Color.FromArgb(242, 242, 242);
            PS64.PopupBackground = 15;
            PS64.PopupForeground = 3;
            PS64.ScreenColorsForeground = 6;
            PS64.ScreenColorsBackground = 5;
            PS64.FaceName = "Consolas";
            PS64.PixelWidth = 14;
            PS64.FontRaster = true;
            PS64.W10_1909_ForceV2 = false;

            MetricsFonts MetricsFonts = TM.MetricsFonts;
            MetricsFonts.Enabled = true;
            MetricsFonts.BorderWidth = 0;
            MetricsFonts.CaptionHeight = 25;
            MetricsFonts.CaptionWidth = 18;
            MetricsFonts.IconSpacing = 75;
            MetricsFonts.IconVerticalSpacing = 75;
            MetricsFonts.MenuHeight = 19;
            MetricsFonts.MenuWidth = 18;
            MetricsFonts.PaddedBorderWidth = 0;
            MetricsFonts.ScrollHeight = 17;
            MetricsFonts.ScrollWidth = 17;
            MetricsFonts.SmCaptionHeight = 17;
            MetricsFonts.SmCaptionWidth = 17;
            MetricsFonts.DesktopIconSize = 48;
            MetricsFonts.ShellIconSize = 32;
            MetricsFonts.Fonts_SingleBitPP = true;
            MetricsFonts.CaptionFont = new("Trebuchet MS", 9.75f, FontStyle.Bold);
            MetricsFonts.SmCaptionFont = new("Tahoma", 8.25f, FontStyle.Regular);
            MetricsFonts.IconFont = new("Tahoma", 8.25f, FontStyle.Regular);
            MetricsFonts.MenuFont = new("Tahoma", 8.25f, FontStyle.Regular);
            MetricsFonts.MessageFont = new("Tahoma", 8.25f, FontStyle.Regular);
            MetricsFonts.StatusFont = new("Tahoma", 8.25f, FontStyle.Regular);

            TM.Cursors.Enabled = true;
            TM.Cursors.Shadow = true;
            TM.Cursors.Cursor_Arrow.UseFromFile = true;
            TM.Cursors.Cursor_AppLoading.UseFromFile = true;
            TM.Cursors.Cursor_Busy.UseFromFile = true;
            TM.Cursors.Cursor_Help.UseFromFile = true;
            TM.Cursors.Cursor_Pen.UseFromFile = true;
            TM.Cursors.Cursor_None.UseFromFile = true;
            TM.Cursors.Cursor_Move.UseFromFile = true;
            TM.Cursors.Cursor_Up.UseFromFile = true;
            TM.Cursors.Cursor_NS.UseFromFile = true;
            TM.Cursors.Cursor_EW.UseFromFile = true;
            TM.Cursors.Cursor_NESW.UseFromFile = true;
            TM.Cursors.Cursor_NWSE.UseFromFile = true;
            TM.Cursors.Cursor_Link.UseFromFile = true;
            TM.Cursors.Cursor_Pin.UseFromFile = true;
            TM.Cursors.Cursor_Person.UseFromFile = true;
            TM.Cursors.Cursor_IBeam.UseFromFile = true;
            TM.Cursors.Cursor_Cross.UseFromFile = true;
            TM.Cursors.Cursor_Arrow.File = string.Empty;
            TM.Cursors.Cursor_AppLoading.File = string.Empty;
            TM.Cursors.Cursor_Busy.File = string.Empty;
            TM.Cursors.Cursor_Help.File = string.Empty;
            TM.Cursors.Cursor_Pen.File = string.Empty;
            TM.Cursors.Cursor_None.File = string.Empty;
            TM.Cursors.Cursor_Move.File = string.Empty;
            TM.Cursors.Cursor_Up.File = string.Empty;
            TM.Cursors.Cursor_NS.File = string.Empty;
            TM.Cursors.Cursor_EW.File = string.Empty;
            TM.Cursors.Cursor_NESW.File = string.Empty;
            TM.Cursors.Cursor_NWSE.File = string.Empty;
            TM.Cursors.Cursor_Link.File = string.Empty;
            TM.Cursors.Cursor_Pin.File = string.Empty;
            TM.Cursors.Cursor_Person.File = string.Empty;
            TM.Cursors.Cursor_IBeam.File = string.Empty;
            TM.Cursors.Cursor_Cross.File = string.Empty;

            WinEffects WinEffects = TM.WindowsEffects;
            WinEffects.Enabled = true;
            WinEffects.ShakeToMinimize = false;
            WinEffects.BalloonNotifications = true;
            WinEffects.PaintDesktopVersion = false;
            WinEffects.ShowSecondsInSystemClock = false;
            WinEffects.Win11ClassicContextMenu = false;
            WinEffects.SysListView32 = true;

            Icons Icons = TM.Icons;
            Icons.Enabled = true;
            Icons.Computer = $"{SysPaths.Explorer},0";
            Icons.User = $"{SysPaths.System32}\\mydocs.dll,0";
            Icons.Network = $"{SysPaths.System32}\\shell32.dll,17";
            Icons.RecycleBinEmpty = $"{SysPaths.System32}\\shell32.dll,31";
            Icons.RecycleBinFull = $"{SysPaths.System32}\\shell32.dll,32";
            Icons.ControlPanel = $"{SysPaths.System32}\\shell32.dll,21";

            ScreenSaver ScreenSaver = TM.ScreenSaver;
            ScreenSaver.Enabled = true;
            ScreenSaver.IsSecure = false;
            ScreenSaver.TimeOut = 60;
            ScreenSaver.File = $@"{SysPaths.System32}\logon.scr";

            Sounds Sounds = TM.Sounds;
            Sounds.Enabled = true;
            Sounds.Snd_Imageres_SystemStart = string.Empty;
            Sounds.Snd_Win_Default = $@"{SysPaths.Windows}\media\Windows XP Ding.wav";
            Sounds.Snd_Win_AppGPFault = string.Empty;
            Sounds.Snd_Win_CCSelect = string.Empty;
            Sounds.Snd_Win_ChangeTheme = string.Empty;
            Sounds.Snd_Win_Close = string.Empty;
            Sounds.Snd_Win_CriticalBatteryAlarm = $@"{SysPaths.Windows}\media\Windows XP Battery Critical.wav";
            Sounds.Snd_Win_DeviceConnect = $@"{SysPaths.Windows}\media\Windows XP Hardware Insert.wav";
            Sounds.Snd_Win_DeviceDisconnect = $@"{SysPaths.Windows}\media\Windows XP Hardware Remove.wav";
            Sounds.Snd_Win_DeviceFail = $@"{SysPaths.Windows}\media\Windows XP Hardware Fail.wav";
            Sounds.Snd_Win_FaxBeep = string.Empty;
            Sounds.Snd_Win_LowBatteryAlarm = $@"{SysPaths.Windows}\media\Windows XP Battery Low.wav";
            Sounds.Snd_Win_MailBeep = $@"{SysPaths.Windows}\media\Windows XP Notify.wav";
            Sounds.Snd_Win_Maximize = string.Empty;
            Sounds.Snd_Win_MenuCommand = string.Empty;
            Sounds.Snd_Win_MenuPopup = string.Empty;
            Sounds.Snd_Win_MessageNudge = string.Empty;
            Sounds.Snd_Win_Minimize = string.Empty;
            Sounds.Snd_Win_Notification_Default = string.Empty;
            Sounds.Snd_Win_Notification_IM = string.Empty;
            Sounds.Snd_Win_Notification_Looping_Alarm = string.Empty;
            Sounds.Snd_Win_Notification_Looping_Alarm2 = string.Empty;
            Sounds.Snd_Win_Notification_Looping_Alarm3 = string.Empty;
            Sounds.Snd_Win_Notification_Looping_Alarm4 = string.Empty;
            Sounds.Snd_Win_Notification_Looping_Alarm5 = string.Empty;
            Sounds.Snd_Win_Notification_Looping_Alarm6 = string.Empty;
            Sounds.Snd_Win_Notification_Looping_Alarm7 = string.Empty;
            Sounds.Snd_Win_Notification_Looping_Alarm8 = string.Empty;
            Sounds.Snd_Win_Notification_Looping_Alarm9 = string.Empty;
            Sounds.Snd_Win_Notification_Looping_Alarm10 = string.Empty;
            Sounds.Snd_Win_Notification_Looping_Call = string.Empty;
            Sounds.Snd_Win_Notification_Looping_Call2 = string.Empty;
            Sounds.Snd_Win_Notification_Looping_Call3 = string.Empty;
            Sounds.Snd_Win_Notification_Looping_Call4 = string.Empty;
            Sounds.Snd_Win_Notification_Looping_Call5 = string.Empty;
            Sounds.Snd_Win_Notification_Looping_Call6 = string.Empty;
            Sounds.Snd_Win_Notification_Looping_Call7 = string.Empty;
            Sounds.Snd_Win_Notification_Looping_Call8 = string.Empty;
            Sounds.Snd_Win_Notification_Looping_Call9 = string.Empty;
            Sounds.Snd_Win_Notification_Looping_Call10 = string.Empty;
            Sounds.Snd_Win_Notification_Mail = string.Empty;
            Sounds.Snd_Win_Notification_Proximity = string.Empty;
            Sounds.Snd_Win_Notification_Reminder = string.Empty;
            Sounds.Snd_Win_Notification_SMS = string.Empty;
            Sounds.Snd_Win_Open = string.Empty;
            Sounds.Snd_Win_PrintComplete = string.Empty;
            Sounds.Snd_Win_ProximityConnection = string.Empty;
            Sounds.Snd_Win_RestoreDown = string.Empty;
            Sounds.Snd_Win_RestoreUp = string.Empty;
            Sounds.Snd_Win_ShowBand = string.Empty;
            Sounds.Snd_Win_SystemAsterisk = $@"{SysPaths.Windows}\media\Windows XP Error.wav";
            Sounds.Snd_Win_SystemExclamation = $@"{SysPaths.Windows}\media\Windows XP Exclamation.wav";
            Sounds.Snd_Win_SystemExit = $@"{SysPaths.Windows}\media\Windows XP Shutdown.wav";
            Sounds.Snd_Win_SystemStart = $@"{SysPaths.Windows}\media\Windows XP Startup.wav";
            Sounds.Snd_Win_SystemHand = $@"{SysPaths.Windows}\media\Windows XP Critical Stop.wav";
            Sounds.Snd_Win_SystemNotification = $@"{SysPaths.Windows}\media\Windows XP Balloon.wav";
            Sounds.Snd_Win_SystemQuestion = string.Empty;
            Sounds.Snd_Win_WindowsLogoff = $@"{SysPaths.Windows}\media\Windows XP Logoff Sound.wav";
            Sounds.Snd_Win_WindowsLogon = $@"{SysPaths.Windows}\media\Windows XP Logon Sound.wav";
            Sounds.Snd_Win_WindowsUAC = string.Empty;
            Sounds.Snd_Win_WindowsUnlock = string.Empty;
            Sounds.Snd_Explorer_ActivatingDocument = string.Empty;
            Sounds.Snd_Explorer_BlockedPopup = $@"{SysPaths.Windows}\media\Windows Pop-up Blocked.wav";
            Sounds.Snd_Explorer_EmptyRecycleBin = $@"{SysPaths.Windows}\media\Windows XP Recycle.wav";
            Sounds.Snd_Explorer_FeedDiscovered = $@"{SysPaths.Windows}\media\Windows Feed Discovered.wav";
            Sounds.Snd_Explorer_MoveMenuItem = string.Empty;
            Sounds.Snd_Explorer_Navigating = $@"{SysPaths.Windows}\media\Windows Navigation Start.wav";
            Sounds.Snd_Explorer_SecurityBand = $@"{SysPaths.Windows}\media\Windows Information Bar.wav";
            Sounds.Snd_Explorer_SearchProviderDiscovered = string.Empty;
            Sounds.Snd_Explorer_FaxError = $@"{SysPaths.Windows}\media\ding.wav";
            Sounds.Snd_Explorer_FaxLineRings = $@"{SysPaths.Windows}\media\ringin.wav";
            Sounds.Snd_Explorer_FaxNew = $@"{SysPaths.Windows}\media\notify.wav";
            Sounds.Snd_Explorer_FaxSent = $@"{SysPaths.Windows}\media\tada.wav";
            Sounds.Snd_NetMeeting_PersonJoins = $@"{SysPaths.ProgramFiles}\NetMeeting\Blip.wav";
            Sounds.Snd_NetMeeting_PersonLeaves = $@"{SysPaths.ProgramFiles}\NetMeeting\Blip.wav";
            Sounds.Snd_NetMeeting_ReceiveCall = $@"{SysPaths.Windows}\media\Windows XP RingIn.wav";
            Sounds.Snd_NetMeeting_ReceiveRequestToJoin = $@"{SysPaths.Windows}\media\Windows XP RingIn.wav";
            Sounds.Snd_SpeechRec_DisNumbersSound = string.Empty;
            Sounds.Snd_SpeechRec_HubOffSound = string.Empty;
            Sounds.Snd_SpeechRec_HubOnSound = string.Empty;
            Sounds.Snd_SpeechRec_HubSleepSound = string.Empty;
            Sounds.Snd_SpeechRec_MisrecoSound = string.Empty;
            Sounds.Snd_SpeechRec_PanelSound = string.Empty;

            Wallpaper Wallpaper = TM.Wallpaper;
            Wallpaper.Enabled = true;
            Wallpaper.ImageFile = $"{SysPaths.Windows}\\Web\\Wallpaper\\Bliss.bmp";
            Wallpaper.WallpaperStyle = Structures.Wallpaper.WallpaperStyles.Stretched;

            TM.AppTheme.Enabled = true;

            Accessibility Accessibility = TM.Accessibility;
            Accessibility.Enabled = true;

            return TM;
        }
    }
}