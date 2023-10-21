using System.Drawing;

namespace WinPaletter.Theme
{
    public partial class Default
    {
        public Theme.Manager Windows11()
        {
            var TM = new Theme.Manager(Theme.Manager.Source.Empty);

            TM.Windows11.Titlebar_Inactive = Color.FromArgb(32, 32, 32);

            {
                ref Theme.Structures.Info Info = ref TM.Info;
                Info.ThemeName = "Windows 11 (Initial)";
                Info.Description = "Initial; Like first time after Windows Setup";
                Info.ThemeVersion = "1.0.0.0";
                Info.Author = "Microsoft";
                Info.AuthorSocialMediaLink = "www.microsoft.com";
                Info.AppVersion = Program.Version;
            }

            {
                ref Theme.Structures.Console CMD = ref TM.CommandPrompt;
                CMD.ColorTable05 = Color.FromArgb(136, 23, 152);
                CMD.ColorTable06 = Color.FromArgb(193, 156, 0);
                CMD.PopupBackground = 15;
                CMD.PopupForeground = 5;
                CMD.ScreenColorsForeground = 7;
                CMD.ScreenColorsBackground = 0;
                CMD.FaceName = "Consolas";
                CMD.FontSize = 18 * 65536;
                CMD.FontRaster = false;
                CMD.W10_1909_ForceV2 = true;
            }

            {
                ref Theme.Structures.Console PS86 = ref TM.PowerShellx86;
                PS86.ColorTable05 = Color.FromArgb(1, 36, 86);
                PS86.ColorTable06 = Color.FromArgb(238, 237, 240);
                PS86.PopupBackground = 15;
                PS86.PopupForeground = 3;
                PS86.ScreenColorsForeground = 6;
                PS86.ScreenColorsBackground = 5;
                PS86.FaceName = "Consolas";
                PS86.FontSize = 17 * 65536;
                PS86.FontRaster = false;
                PS86.W10_1909_ForceV2 = true;
            }

            {
                ref Theme.Structures.Console PS64 = ref TM.PowerShellx64;
                PS64.ColorTable05 = Color.FromArgb(1, 36, 86);
                PS64.ColorTable06 = Color.FromArgb(238, 237, 240);
                PS64.PopupBackground = 15;
                PS64.PopupForeground = 3;
                PS64.ScreenColorsForeground = 6;
                PS64.ScreenColorsBackground = 5;
                PS64.FaceName = "Consolas";
                PS64.FontSize = 17 * 65536;
                PS64.FontRaster = false;
                PS64.W10_1909_ForceV2 = true;
            }

            {
                ref Theme.Structures.MetricsFonts MetricsFonts = ref TM.MetricsFonts;
                MetricsFonts.BorderWidth = 1;
                MetricsFonts.CaptionHeight = 22;
                MetricsFonts.CaptionWidth = 22;
                MetricsFonts.IconSpacing = 75;
                MetricsFonts.IconVerticalSpacing = 75;
                MetricsFonts.MenuHeight = 19;
                MetricsFonts.MenuWidth = 19;
                MetricsFonts.PaddedBorderWidth = 4;
                MetricsFonts.ScrollHeight = 17;
                MetricsFonts.ScrollWidth = 17;
                MetricsFonts.SmCaptionHeight = 22;
                MetricsFonts.SmCaptionWidth = 22;
                MetricsFonts.DesktopIconSize = 48;
                MetricsFonts.ShellIconSize = 32;
            }

            {
                ref Theme.Structures.WinEffects WinEffects = ref TM.WindowsEffects;
                WinEffects.ShakeToMinimize = false;
                WinEffects.BalloonNotifications = false;
                WinEffects.PaintDesktopVersion = false;
                WinEffects.ShowSecondsInSystemClock = false;
                WinEffects.Win11ClassicContextMenu = false;
                WinEffects.SysListView32 = false;
            }

            TM.Terminal = new WinTerminal("", WinTerminal.Mode.Empty);
            TM.TerminalPreview = new WinTerminal("", WinTerminal.Mode.Empty);
            TM.Cursor_Shadow = false;

            {
                ref Theme.Structures.Sounds Sounds = ref TM.Sounds;
                Sounds.Snd_Imageres_SystemStart = "Default";
                Sounds.Snd_Win_Default = PathsExt.Windows + @"\media\Windows Background.wav";
                Sounds.Snd_Win_AppGPFault = "";
                Sounds.Snd_Win_CCSelect = "";
                Sounds.Snd_Win_ChangeTheme = "";
                Sounds.Snd_Win_Close = "";
                Sounds.Snd_Win_CriticalBatteryAlarm = PathsExt.Windows + @"\media\Windows Foreground.wav";
                Sounds.Snd_Win_DeviceConnect = PathsExt.Windows + @"\media\Windows Hardware Insert.wav";
                Sounds.Snd_Win_DeviceDisconnect = PathsExt.Windows + @"\media\Windows Hardware Remove.wav";
                Sounds.Snd_Win_DeviceFail = PathsExt.Windows + @"\media\Windows Hardware Fail.wav";
                Sounds.Snd_Win_FaxBeep = PathsExt.Windows + @"\media\Windows Notify Email.wav";
                Sounds.Snd_Win_LowBatteryAlarm = PathsExt.Windows + @"\media\Windows Background.wav";
                Sounds.Snd_Win_MailBeep = PathsExt.Windows + @"\media\Windows Notify Email.wav";
                Sounds.Snd_Win_Maximize = "";
                Sounds.Snd_Win_MenuCommand = "";
                Sounds.Snd_Win_MenuPopup = "";
                Sounds.Snd_Win_MessageNudge = PathsExt.Windows + @"\media\Windows Message Nudge.wav";
                Sounds.Snd_Win_Minimize = "";
                Sounds.Snd_Win_Notification_Default = PathsExt.Windows + @"\media\Windows Notify System Generic.wav";
                Sounds.Snd_Win_Notification_IM = PathsExt.Windows + @"\media\Windows Notify Messaging.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm = PathsExt.Windows + @"\media\Alarm01.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm2 = PathsExt.Windows + @"\media\Alarm02.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm3 = PathsExt.Windows + @"\media\Alarm03.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm4 = PathsExt.Windows + @"\media\Alarm04.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm5 = PathsExt.Windows + @"\media\Alarm05.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm6 = PathsExt.Windows + @"\media\Alarm06.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm7 = PathsExt.Windows + @"\media\Alarm07.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm8 = PathsExt.Windows + @"\media\Alarm08.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm9 = PathsExt.Windows + @"\media\Alarm09.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm10 = PathsExt.Windows + @"\media\Alarm10.wav";
                Sounds.Snd_Win_Notification_Looping_Call = PathsExt.Windows + @"\media\Ring01.wav";
                Sounds.Snd_Win_Notification_Looping_Call2 = PathsExt.Windows + @"\media\Ring02.wav";
                Sounds.Snd_Win_Notification_Looping_Call3 = PathsExt.Windows + @"\media\Ring03.wav";
                Sounds.Snd_Win_Notification_Looping_Call4 = PathsExt.Windows + @"\media\Ring04.wav";
                Sounds.Snd_Win_Notification_Looping_Call5 = PathsExt.Windows + @"\media\Ring05.wav";
                Sounds.Snd_Win_Notification_Looping_Call6 = PathsExt.Windows + @"\media\Ring06.wav";
                Sounds.Snd_Win_Notification_Looping_Call7 = PathsExt.Windows + @"\media\Ring07.wav";
                Sounds.Snd_Win_Notification_Looping_Call8 = PathsExt.Windows + @"\media\Ring08.wav";
                Sounds.Snd_Win_Notification_Looping_Call9 = PathsExt.Windows + @"\media\Ring09.wav";
                Sounds.Snd_Win_Notification_Looping_Call10 = PathsExt.Windows + @"\media\Ring10.wav";
                Sounds.Snd_Win_Notification_Mail = PathsExt.Windows + @"\media\Windows Notify Email.wav";
                Sounds.Snd_Win_Notification_Proximity = PathsExt.Windows + @"\media\Windows Proximity Notification.wav";
                Sounds.Snd_Win_Notification_Reminder = PathsExt.Windows + @"\media\Windows Notify Calendar.wav";
                Sounds.Snd_Win_Notification_SMS = PathsExt.Windows + @"\media\Windows Notify Messaging.wav";
                Sounds.Snd_Win_Open = "";
                Sounds.Snd_Win_PrintComplete = "";
                Sounds.Snd_Win_ProximityConnection = PathsExt.Windows + @"\media\Windows Proximity Connection.wav";
                Sounds.Snd_Win_RestoreDown = "";
                Sounds.Snd_Win_RestoreUp = "";
                Sounds.Snd_Win_ShowBand = "";
                Sounds.Snd_Win_SystemAsterisk = PathsExt.Windows + @"\media\Windows Background.wav";
                Sounds.Snd_Win_SystemExclamation = PathsExt.Windows + @"\media\Windows Background.wav";
                Sounds.Snd_Win_SystemExit = "";
                Sounds.Snd_Win_SystemStart = "";
                Sounds.Snd_Win_SystemHand = PathsExt.Windows + @"\media\Windows Foreground.wav";
                Sounds.Snd_Win_SystemNotification = PathsExt.Windows + @"\media\Windows Background.wav";
                Sounds.Snd_Win_SystemQuestion = "";
                Sounds.Snd_Win_WindowsLogoff = "";
                Sounds.Snd_Win_WindowsLogon = "";
                Sounds.Snd_Win_WindowsUAC = PathsExt.Windows + @"\media\Windows User Account Control.wav";
                Sounds.Snd_Win_WindowsUnlock = PathsExt.Windows + @"\media\Windows Unlock.wav";
                Sounds.Snd_Explorer_ActivatingDocument = "";
                Sounds.Snd_Explorer_BlockedPopup = "";
                Sounds.Snd_Explorer_EmptyRecycleBin = "";
                Sounds.Snd_Explorer_FeedDiscovered = "";
                Sounds.Snd_Explorer_MoveMenuItem = "";
                Sounds.Snd_Explorer_Navigating = "";
                Sounds.Snd_Explorer_SecurityBand = "";
                Sounds.Snd_Explorer_SearchProviderDiscovered = "";
                Sounds.Snd_Explorer_FaxError = "";
                Sounds.Snd_Explorer_FaxLineRings = "";
                Sounds.Snd_Explorer_FaxNew = "";
                Sounds.Snd_Explorer_FaxSent = "";
                Sounds.Snd_NetMeeting_PersonJoins = "";
                Sounds.Snd_NetMeeting_PersonLeaves = "";
                Sounds.Snd_NetMeeting_ReceiveCall = "";
                Sounds.Snd_NetMeeting_ReceiveRequestToJoin = "";
                Sounds.Snd_SpeechRec_DisNumbersSound = PathsExt.Windows + @"\media\Speech Disambiguation.wav";
                Sounds.Snd_SpeechRec_HubOffSound = PathsExt.Windows + @"\media\Speech Off.wav";
                Sounds.Snd_SpeechRec_HubOnSound = PathsExt.Windows + @"\media\Speech On.wav";
                Sounds.Snd_SpeechRec_HubSleepSound = PathsExt.Windows + @"\media\Speech Sleep.wav";
                Sounds.Snd_SpeechRec_MisrecoSound = PathsExt.Windows + @"\media\Speech Misrecognition.wav";
                Sounds.Snd_SpeechRec_PanelSound = PathsExt.Windows + @"\media\Speech Disambiguation.wav";

                Sounds.Snd_Win_SystemExit_TaskMgmt = true;
                Sounds.Snd_Win_WindowsLogoff_TaskMgmt = true;
                Sounds.Snd_Win_WindowsLogon_TaskMgmt = true;
                Sounds.Snd_Win_WindowsUnlock_TaskMgmt = true;
            }

            return TM;
        }

    }
}