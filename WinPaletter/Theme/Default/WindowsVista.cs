using System.Drawing;
using WinPaletter.Theme.Structures;

namespace WinPaletter.Theme
{
    public static partial class Default
    {
        /// <summary>
        /// Default Windows Vista theme as a <see cref="Manager"/> object
        /// </summary>
        /// <returns><see cref="Manager"/> </returns>
        public static Manager WindowsVista()
        {
            Manager TM = new(Manager.Source.Empty);

            ref Info Info = ref TM.Info;
            Info.ThemeName = "Default Windows Vista";
            Info.Description = "Initial, like the default look after a fresh Windows Vista setup—clean, untouched, and ready for customization.";
            Info.ThemeVersion = "1.0.0.0";
            Info.Author = "Microsoft";
            Info.AuthorSocialMediaLink = "https://www.microsoft.com";
            Info.AppVersion = Program.Version;

            ref WindowsVista WinVista = ref TM.WindowsVista;
            WinVista.Enabled = true;
            WinVista.ColorizationColor = Color.FromArgb(64, 158, 254);

            ref Console CMD = ref TM.CommandPrompt;
            CMD.Enabled = true;
            CMD.ColorTable00 = Color.FromArgb(0, 0, 0);
            CMD.ColorTable01 = Color.FromArgb(0, 0, 128);
            CMD.ColorTable02 = Color.FromArgb(0, 128, 0);
            CMD.ColorTable03 = Color.FromArgb(0, 128, 128);
            CMD.ColorTable04 = Color.FromArgb(128, 0, 0);
            CMD.ColorTable05 = Color.FromArgb(128, 0, 128);
            CMD.ColorTable06 = Color.FromArgb(128, 128, 0);
            CMD.ColorTable07 = Color.FromArgb(192, 192, 192);
            CMD.ColorTable08 = Color.FromArgb(128, 128, 128);
            CMD.ColorTable09 = Color.FromArgb(0, 0, 255);
            CMD.ColorTable10 = Color.FromArgb(0, 255, 0);
            CMD.ColorTable11 = Color.FromArgb(0, 255, 255);
            CMD.ColorTable12 = Color.FromArgb(255, 0, 0);
            CMD.ColorTable13 = Color.FromArgb(255, 0, 255);
            CMD.ColorTable14 = Color.FromArgb(255, 255, 0);
            CMD.ColorTable15 = Color.FromArgb(255, 255, 255);
            CMD.PopupForeground = 5;
            CMD.PopupBackground = 15;
            CMD.ScreenColorsForeground = 7;
            CMD.ScreenColorsBackground = 0;
            CMD.FaceName = "Consolas";
            CMD.PixelWidth = 18;
            CMD.FontRaster = true;
            CMD.W10_1909_ForceV2 = false;

            ref Console PS86 = ref TM.PowerShellx86;
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
            PS86.PopupForeground = 15;
            PS86.PopupBackground = 3;
            PS86.ScreenColorsForeground = 6;
            PS86.ScreenColorsBackground = 5;
            PS86.FaceName = "Consolas";
            PS86.PixelWidth = 14;
            PS86.FontRaster = true;
            PS86.W10_1909_ForceV2 = false;

            ref Console PS64 = ref TM.PowerShellx64;
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
            PS64.PopupForeground = 15;
            PS64.PopupBackground = 3;
            PS64.ScreenColorsForeground = 6;
            PS64.ScreenColorsBackground = 5;
            PS64.FaceName = "Consolas";
            PS64.PixelWidth = 14;
            PS64.FontRaster = true;
            PS64.W10_1909_ForceV2 = false;

            ref MetricsFonts MetricsFonts = ref TM.MetricsFonts;
            MetricsFonts.Enabled = true;
            MetricsFonts.BorderWidth = 1;
            MetricsFonts.CaptionHeight = 19;
            MetricsFonts.CaptionWidth = 19;
            MetricsFonts.IconSpacing = 75;
            MetricsFonts.IconVerticalSpacing = 75;
            MetricsFonts.MenuHeight = 19;
            MetricsFonts.MenuWidth = 19;
            MetricsFonts.PaddedBorderWidth = 4;
            MetricsFonts.ScrollHeight = 17;
            MetricsFonts.ScrollWidth = 17;
            MetricsFonts.SmCaptionHeight = 17;
            MetricsFonts.SmCaptionWidth = 17;
            MetricsFonts.DesktopIconSize = 48;
            MetricsFonts.ShellIconSize = 32;

            ref WinEffects WinEffects = ref TM.WindowsEffects;
            WinEffects.Enabled = true;
            WinEffects.ShakeToMinimize = false;
            WinEffects.BalloonNotifications = true;
            WinEffects.PaintDesktopVersion = false;
            WinEffects.ShowSecondsInSystemClock = false;
            WinEffects.Win11ClassicContextMenu = false;
            WinEffects.SysListView32 = true;

            ref Icons Icons = ref TM.Icons;
            Icons.Enabled = true;

            TM.Terminal = new(string.Empty, WinTerminal.Mode.Empty);
            TM.TerminalPreview = new(string.Empty, WinTerminal.Mode.Empty);

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
            TM.Cursors.Cursor_AppLoading.CircleStyle = Paths.CircleStyle.Aero7;
            TM.Cursors.Cursor_AppLoading.LoadingCircleBack1 = Color.FromArgb(112, 255, 250);
            TM.Cursors.Cursor_AppLoading.LoadingCircleBack2 = Color.FromArgb(112, 255, 250);
            TM.Cursors.Cursor_AppLoading.LoadingCircleHot_AnimationSpeed = 20;
            TM.Cursors.Cursor_AppLoading.PrimaryColor1 = Color.FromArgb(255, 255, 255);
            TM.Cursors.Cursor_AppLoading.PrimaryColor2 = Color.FromArgb(137, 255, 249);
            TM.Cursors.Cursor_AppLoading.PrimaryColorGradient = true;
            TM.Cursors.Cursor_AppLoading.PrimaryColorGradientMode = Paths.GradientMode.Circle;
            TM.Cursors.Cursor_AppLoading.SecondaryColor1 = Color.FromArgb(66, 67, 77);
            TM.Cursors.Cursor_AppLoading.SecondaryColor2 = Color.FromArgb(0, 81, 77);
            TM.Cursors.Cursor_AppLoading.SecondaryColorGradient = true;
            TM.Cursors.Cursor_AppLoading.SecondaryColorGradientMode = Paths.GradientMode.Circle;
            TM.Cursors.Cursor_Busy.CircleStyle = Paths.CircleStyle.Aero7;
            TM.Cursors.Cursor_Busy.LoadingCircleBack1 = Color.FromArgb(112, 255, 250);
            TM.Cursors.Cursor_Busy.LoadingCircleBack2 = Color.FromArgb(112, 255, 250);
            TM.Cursors.Cursor_Busy.LoadingCircleHot_AnimationSpeed = 20;

            ref Sounds Sounds = ref TM.Sounds;
            Sounds.Enabled = true;
            Sounds.Snd_Imageres_SystemStart = "Default";
            Sounds.Snd_Win_Default = $@"{SysPaths.Windows}\media\Windows Ding.wav";
            Sounds.Snd_Win_AppGPFault = string.Empty;
            Sounds.Snd_Win_CCSelect = string.Empty;
            Sounds.Snd_Win_ChangeTheme = string.Empty;
            Sounds.Snd_Win_Close = string.Empty;
            Sounds.Snd_Win_CriticalBatteryAlarm = $@"{SysPaths.Windows}\media\Windows Battery Critical.wav";
            Sounds.Snd_Win_DeviceConnect = $@"{SysPaths.Windows}\media\Windows Hardware Insert.wav";
            Sounds.Snd_Win_DeviceDisconnect = $@"{SysPaths.Windows}\media\Windows Hardware Remove.wav";
            Sounds.Snd_Win_DeviceFail = $@"{SysPaths.Windows}\media\Windows Hardware Fail.wav";
            Sounds.Snd_Win_FaxBeep = $@"{SysPaths.Windows}\media\Windows Notify.wav";
            Sounds.Snd_Win_LowBatteryAlarm = $@"{SysPaths.Windows}\media\Windows Battery Low.wav";
            Sounds.Snd_Win_MailBeep = $@"{SysPaths.Windows}\media\Windows Notify.wav";
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
            Sounds.Snd_Win_SystemAsterisk = $@"{SysPaths.Windows}\media\Windows Error.wav";
            Sounds.Snd_Win_SystemExclamation = $@"{SysPaths.Windows}\media\Windows Exclamation.wav";
            Sounds.Snd_Win_SystemExit = $@"{SysPaths.Windows}\media\Windows Shutdown.wav";
            Sounds.Snd_Win_SystemStart = string.Empty;
            Sounds.Snd_Win_SystemHand = $@"{SysPaths.Windows}\media\Windows Critical Stop.wav";
            Sounds.Snd_Win_SystemNotification = $@"{SysPaths.Windows}\media\Windows Balloon.wav";
            Sounds.Snd_Win_SystemQuestion = string.Empty;
            Sounds.Snd_Win_WindowsLogoff = $@"{SysPaths.Windows}\media\Windows Logoff Sound.wav";
            Sounds.Snd_Win_WindowsLogon = $@"{SysPaths.Windows}\media\Windows Logon Sound.wav";
            Sounds.Snd_Win_WindowsUAC = $@"{SysPaths.Windows}\media\Windows User Account Control.wav";
            Sounds.Snd_Win_WindowsUnlock = string.Empty;
            Sounds.Snd_Explorer_ActivatingDocument = string.Empty;
            Sounds.Snd_Explorer_BlockedPopup = $@"{SysPaths.Windows}\media\Windows Pop-up Blocked.wav";
            Sounds.Snd_Explorer_EmptyRecycleBin = $@"{SysPaths.Windows}\media\Windows Recycle.wav";
            Sounds.Snd_Explorer_FeedDiscovered = $@"{SysPaths.Windows}\media\Windows Feed Discovered.wav";
            Sounds.Snd_Explorer_MoveMenuItem = string.Empty;
            Sounds.Snd_Explorer_Navigating = $@"{SysPaths.Windows}\media\Windows Navigation Start.wav";
            Sounds.Snd_Explorer_SecurityBand = $@"{SysPaths.Windows}\media\Windows Information Bar.wav";
            Sounds.Snd_Explorer_SearchProviderDiscovered = string.Empty;
            Sounds.Snd_Explorer_FaxError = $@"{SysPaths.Windows}\media\ding.wav";
            Sounds.Snd_Explorer_FaxLineRings = $@"{SysPaths.Windows}\media\Windows Ringin.wav";
            Sounds.Snd_Explorer_FaxNew = string.Empty;
            Sounds.Snd_Explorer_FaxSent = $@"{SysPaths.Windows}\media\tada.wav";
            Sounds.Snd_NetMeeting_PersonJoins = string.Empty;
            Sounds.Snd_NetMeeting_PersonLeaves = string.Empty;
            Sounds.Snd_NetMeeting_ReceiveCall = string.Empty;
            Sounds.Snd_NetMeeting_ReceiveRequestToJoin = string.Empty;
            Sounds.Snd_SpeechRec_DisNumbersSound = $@"{SysPaths.Windows}\media\Speech Disambiguation.wav";
            Sounds.Snd_SpeechRec_HubOffSound = $@"{SysPaths.Windows}\media\Speech Off.wav";
            Sounds.Snd_SpeechRec_HubOnSound = $@"{SysPaths.Windows}\media\Speech On.wav";
            Sounds.Snd_SpeechRec_HubSleepSound = $@"{SysPaths.Windows}\media\Speech Sleep.wav";
            Sounds.Snd_SpeechRec_MisrecoSound = $@"{SysPaths.Windows}\media\Speech Misrecognition.wav";
            Sounds.Snd_SpeechRec_PanelSound = $@"{SysPaths.Windows}\media\Speech Disambiguation.wav";

            ref Wallpaper Wallpaper = ref TM.Wallpaper;
            Wallpaper.Enabled = true;

            TM.AppTheme.Enabled = true;

            ref Accessibility Accessibility = ref TM.Accessibility;
            Accessibility.Enabled = true;

            return TM;
        }
    }
}