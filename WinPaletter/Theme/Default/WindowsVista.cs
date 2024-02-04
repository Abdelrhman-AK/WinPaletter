using System.Drawing;

namespace WinPaletter.Theme
{
    public static partial class Default
    {
        public static Manager WindowsVista()
        {
            Manager TM = new(Manager.Source.Empty);

            {
                ref Structures.Info Info = ref TM.Info;
                Info.ThemeName = "Windows Vista (Initial)";
                Info.Description = "Initial; Like first time after Windows Setup";
                Info.ThemeVersion = "1.0.0.0";
                Info.Author = "Microsoft";
                Info.AuthorSocialMediaLink = "www.microsoft.com";
                Info.AppVersion = Program.Version;
            }

            {
                ref Structures.Windows8x Win81 = ref TM.Windows81;
                Win81.ColorizationColor = Color.FromArgb(246, 195, 74);
                Win81.ColorizationColorBalance = 78;
                Win81.PersonalColors_Background = Color.FromArgb(30, 0, 84);
                Win81.PersonalColors_Accent = Color.FromArgb(72, 29, 178);
                Win81.StartColor = Color.FromArgb(30, 0, 84);
                Win81.AccentColor = Color.FromArgb(72, 29, 178);
                Win81.Start = 0;
                Win81.Theme = Structures.Windows7.Themes.Aero;
                Win81.LogonUI = 0;
                Win81.NoLockScreen = false;
                Win81.LockScreenType = Structures.LogonUI7.Sources.Default;
                Win81.LockScreenSystemID = 0;
            }

            {
                ref Structures.Windows7 Win7 = ref TM.Windows7;
                Win7.ColorizationColor = Color.FromArgb(116, 184, 252);
                Win7.ColorizationAfterglow = Color.FromArgb(116, 184, 252);
                Win7.ColorizationColorBalance = 8;
                Win7.ColorizationAfterglowBalance = 43;
                Win7.ColorizationBlurBalance = 49;
                Win7.ColorizationGlassReflectionIntensity = 0;
                Win7.EnableAeroPeek = true;
                Win7.AlwaysHibernateThumbnails = false;
                Win7.Theme = Structures.Windows7.Themes.Aero;
            }

            {
                ref Structures.WindowsVista WinVista = ref TM.WindowsVista;
                WinVista.ColorizationColor = Color.FromArgb(64, 158, 254);
            }

            {
                ref Structures.Console CMD = ref TM.CommandPrompt;
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
                CMD.FontSize = 18 * 65536;
                CMD.FontRaster = true;
                CMD.W10_1909_ForceV2 = false;
            }

            {
                ref Structures.Console PS86 = ref TM.PowerShellx86;
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
                PS86.FontSize = 14 * 65536;
                PS86.FontRaster = true;
                PS86.W10_1909_ForceV2 = false;
            }

            {
                ref Structures.Console PS64 = ref TM.PowerShellx64;
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
                PS64.FontSize = 14 * 65536;
                PS64.FontRaster = true;
                PS64.W10_1909_ForceV2 = false;
            }

            {
                ref Structures.MetricsFonts MetricsFonts = ref TM.MetricsFonts;
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
            }

            {
                ref Structures.WinEffects WinEffects = ref TM.WindowsEffects;
                WinEffects.ShakeToMinimize = false;
                WinEffects.BalloonNotifications = true;
                WinEffects.PaintDesktopVersion = false;
                WinEffects.ShowSecondsInSystemClock = false;
                WinEffects.Win11ClassicContextMenu = false;
                WinEffects.SysListView32 = true;
            }

            TM.Terminal = new(string.Empty, WinTerminal.Mode.Empty);
            TM.TerminalPreview = new(string.Empty, WinTerminal.Mode.Empty);

            TM.Cursor_Enabled = true;
            TM.Cursor_Shadow = true;
            TM.Cursor_Arrow.UseFromFile = false;
            TM.Cursor_AppLoading.UseFromFile = false;
            TM.Cursor_Busy.UseFromFile = false;
            TM.Cursor_Help.UseFromFile = false;
            TM.Cursor_Pen.UseFromFile = false;
            TM.Cursor_None.UseFromFile = false;
            TM.Cursor_Move.UseFromFile = false;
            TM.Cursor_Up.UseFromFile = false;
            TM.Cursor_NS.UseFromFile = false;
            TM.Cursor_EW.UseFromFile = false;
            TM.Cursor_NESW.UseFromFile = false;
            TM.Cursor_NWSE.UseFromFile = false;
            TM.Cursor_Link.UseFromFile = false;
            TM.Cursor_Pin.UseFromFile = false;
            TM.Cursor_Person.UseFromFile = false;
            TM.Cursor_IBeam.UseFromFile = false;
            TM.Cursor_Cross.UseFromFile = false;

            TM.Cursor_AppLoading.LoadingCircleBack1 = Color.FromArgb(5, 109, 109);
            TM.Cursor_AppLoading.LoadingCircleBack2 = Color.FromArgb(66, 109, 122);
            TM.Cursor_AppLoading.LoadingCircleHot1 = Color.FromArgb(41, 174, 178);
            TM.Cursor_AppLoading.LoadingCircleHot2 = Color.FromArgb(121, 245, 239);
            TM.Cursor_AppLoading.LoadingCircleBackGradient = true;
            TM.Cursor_AppLoading.LoadingCircleHotGradient = true;
            TM.Cursor_AppLoading.LoadingCircleBackGradientMode = Paths.GradientMode.Circle;
            TM.Cursor_AppLoading.LoadingCircleHotGradientMode = Paths.GradientMode.Circle;
            TM.Cursor_AppLoading.LoadingCircleHotNoise = true;
            TM.Cursor_AppLoading.LoadingCircleBackNoiseOpacity = 0.5F;

            TM.Cursor_Busy.LoadingCircleBack1 = Color.FromArgb(5, 109, 109);
            TM.Cursor_Busy.LoadingCircleBack2 = Color.FromArgb(66, 109, 122);
            TM.Cursor_Busy.LoadingCircleHot1 = Color.FromArgb(41, 174, 178);
            TM.Cursor_Busy.LoadingCircleHot2 = Color.FromArgb(121, 245, 239);
            TM.Cursor_Busy.LoadingCircleBackGradient = true;
            TM.Cursor_Busy.LoadingCircleHotGradient = true;
            TM.Cursor_Busy.LoadingCircleBackGradientMode = Paths.GradientMode.Circle;
            TM.Cursor_Busy.LoadingCircleHotGradientMode = Paths.GradientMode.Circle;
            TM.Cursor_Busy.LoadingCircleHotNoise = true;
            TM.Cursor_Busy.LoadingCircleBackNoiseOpacity = 0.5F;

            {
                ref Structures.Sounds Sounds = ref TM.Sounds;
                Sounds.Snd_Imageres_SystemStart = "Default";
                Sounds.Snd_Win_Default = $@"{PathsExt.Windows}\media\Windows Ding.wav";
                Sounds.Snd_Win_AppGPFault = string.Empty;
                Sounds.Snd_Win_CCSelect = string.Empty;
                Sounds.Snd_Win_ChangeTheme = string.Empty;
                Sounds.Snd_Win_Close = string.Empty;
                Sounds.Snd_Win_CriticalBatteryAlarm = $@"{PathsExt.Windows}\media\Windows Battery Critical.wav";
                Sounds.Snd_Win_DeviceConnect = $@"{PathsExt.Windows}\media\Windows Hardware Insert.wav";
                Sounds.Snd_Win_DeviceDisconnect = $@"{PathsExt.Windows}\media\Windows Hardware Remove.wav";
                Sounds.Snd_Win_DeviceFail = $@"{PathsExt.Windows}\media\Windows Hardware Fail.wav";
                Sounds.Snd_Win_FaxBeep = $@"{PathsExt.Windows}\media\Windows Notify.wav";
                Sounds.Snd_Win_LowBatteryAlarm = $@"{PathsExt.Windows}\media\Windows Battery Low.wav";
                Sounds.Snd_Win_MailBeep = $@"{PathsExt.Windows}\media\Windows Notify.wav";
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
                Sounds.Snd_Win_SystemAsterisk = $@"{PathsExt.Windows}\media\Windows Error.wav";
                Sounds.Snd_Win_SystemExclamation = $@"{PathsExt.Windows}\media\Windows Exclamation.wav";
                Sounds.Snd_Win_SystemExit = $@"{PathsExt.Windows}\media\Windows Shutdown.wav";
                Sounds.Snd_Win_SystemStart = string.Empty;
                Sounds.Snd_Win_SystemHand = $@"{PathsExt.Windows}\media\Windows Critical Stop.wav";
                Sounds.Snd_Win_SystemNotification = $@"{PathsExt.Windows}\media\Windows Balloon.wav";
                Sounds.Snd_Win_SystemQuestion = string.Empty;
                Sounds.Snd_Win_WindowsLogoff = $@"{PathsExt.Windows}\media\Windows Logoff Sound.wav";
                Sounds.Snd_Win_WindowsLogon = $@"{PathsExt.Windows}\media\Windows Logon Sound.wav";
                Sounds.Snd_Win_WindowsUAC = $@"{PathsExt.Windows}\media\Windows User Account Control.wav";
                Sounds.Snd_Win_WindowsUnlock = string.Empty;
                Sounds.Snd_Explorer_ActivatingDocument = string.Empty;
                Sounds.Snd_Explorer_BlockedPopup = $@"{PathsExt.Windows}\media\Windows Pop-up Blocked.wav";
                Sounds.Snd_Explorer_EmptyRecycleBin = $@"{PathsExt.Windows}\media\Windows Recycle.wav";
                Sounds.Snd_Explorer_FeedDiscovered = $@"{PathsExt.Windows}\media\Windows Feed Discovered.wav";
                Sounds.Snd_Explorer_MoveMenuItem = string.Empty;
                Sounds.Snd_Explorer_Navigating = $@"{PathsExt.Windows}\media\Windows Navigation Start.wav";
                Sounds.Snd_Explorer_SecurityBand = $@"{PathsExt.Windows}\media\Windows Information Bar.wav";
                Sounds.Snd_Explorer_SearchProviderDiscovered = string.Empty;
                Sounds.Snd_Explorer_FaxError = $@"{PathsExt.Windows}\media\ding.wav";
                Sounds.Snd_Explorer_FaxLineRings = $@"{PathsExt.Windows}\media\Windows Ringin.wav";
                Sounds.Snd_Explorer_FaxNew = string.Empty;
                Sounds.Snd_Explorer_FaxSent = $@"{PathsExt.Windows}\media\tada.wav";
                Sounds.Snd_NetMeeting_PersonJoins = string.Empty;
                Sounds.Snd_NetMeeting_PersonLeaves = string.Empty;
                Sounds.Snd_NetMeeting_ReceiveCall = string.Empty;
                Sounds.Snd_NetMeeting_ReceiveRequestToJoin = string.Empty;
                Sounds.Snd_SpeechRec_DisNumbersSound = $@"{PathsExt.Windows}\media\Speech Disambiguation.wav";
                Sounds.Snd_SpeechRec_HubOffSound = $@"{PathsExt.Windows}\media\Speech Off.wav";
                Sounds.Snd_SpeechRec_HubOnSound = $@"{PathsExt.Windows}\media\Speech On.wav";
                Sounds.Snd_SpeechRec_HubSleepSound = $@"{PathsExt.Windows}\media\Speech Sleep.wav";
                Sounds.Snd_SpeechRec_MisrecoSound = $@"{PathsExt.Windows}\media\Speech Misrecognition.wav";
                Sounds.Snd_SpeechRec_PanelSound = $@"{PathsExt.Windows}\media\Speech Disambiguation.wav";
            }

            return TM;
        }
    }
}