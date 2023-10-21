using System.Drawing;

namespace WinPaletter.Theme
{
    public partial class Default
    {
        public Theme.Manager Windows7()
        {
            var TM = new Theme.Manager(WinPaletter.Theme.Manager.Source.Empty);

            {
                ref Theme.Structures.Info Info = ref TM.Info;
                Info.ThemeName = "Windows 7 (Initial)";
                Info.Description = "Initial; Like first time after Windows Setup";
                Info.ThemeVersion = "1.0.0.0";
                Info.Author = "Microsoft";
                Info.AuthorSocialMediaLink = "www.microsoft.com";
                Info.AppVersion = Program.Version;
            }

            {
                ref Theme.Structures.Windows8x Win81 = ref TM.Windows81;
                Win81.ColorizationColor = Color.FromArgb(246, 195, 74);
                Win81.ColorizationColorBalance = 78;
                Win81.PersonalColors_Background = Color.FromArgb(30, 0, 84);
                Win81.PersonalColors_Accent = Color.FromArgb(72, 29, 178);
                Win81.StartColor = Color.FromArgb(30, 0, 84);
                Win81.AccentColor = Color.FromArgb(72, 29, 178);
                Win81.Start = 0;
                Win81.Theme = Theme.Structures.Windows7.Themes.Aero;
                Win81.LogonUI = 0;
                Win81.NoLockScreen = false;
                Win81.LockScreenType = Theme.Structures.LogonUI7.Modes.Default;
                Win81.LockScreenSystemID = 0;
            }

            {
                ref Theme.Structures.Windows7 Win7 = ref TM.Windows7;
                Win7.ColorizationColor = Color.FromArgb(116, 184, 252);
                Win7.ColorizationAfterglow = Color.FromArgb(116, 184, 252);
                Win7.ColorizationColorBalance = 8;
                Win7.ColorizationAfterglowBalance = 43;
                Win7.ColorizationBlurBalance = 49;
                Win7.ColorizationGlassReflectionIntensity = 0;
                Win7.EnableAeroPeek = true;
                Win7.AlwaysHibernateThumbnails = false;
                Win7.Theme = Theme.Structures.Windows7.Themes.Aero;
            }

            {
                ref Theme.Structures.WindowsVista WinVista = ref TM.WindowsVista;
                WinVista.ColorizationColor = Color.FromArgb(64, 158, 254);
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
                CMD.FontRaster = true;
                CMD.W10_1909_ForceV2 = false;
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
                PS86.FontSize = 14 * 65536;
                PS86.FontRaster = true;
                PS86.W10_1909_ForceV2 = false;
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
                PS64.FontSize = 14 * 65536;
                PS64.FontRaster = true;
                PS64.W10_1909_ForceV2 = false;
            }

            {
                ref Theme.Structures.MetricsFonts MetricsFonts = ref TM.MetricsFonts;
                MetricsFonts.BorderWidth = 1;
                MetricsFonts.CaptionHeight = 21;
                MetricsFonts.CaptionWidth = 21;
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
                ref Theme.Structures.WinEffects WinEffects = ref TM.WindowsEffects;
                WinEffects.ShakeToMinimize = true;
                WinEffects.BalloonNotifications = true;
                WinEffects.PaintDesktopVersion = false;
                WinEffects.ShowSecondsInSystemClock = false;
                WinEffects.Win11ClassicContextMenu = false;
                WinEffects.SysListView32 = false;
            }

            TM.Terminal = new WinTerminal("", WinTerminal.Mode.Empty);
            TM.TerminalPreview = new WinTerminal("", WinTerminal.Mode.Empty);
            TM.Cursor_Shadow = true;

            {
                ref Theme.Structures.Sounds Sounds = ref TM.Sounds;
                Sounds.Snd_Imageres_SystemStart = "Default";
                Sounds.Snd_Win_Default = PathsExt.Windows + @"\media\Windows Ding.wav";
                Sounds.Snd_Win_AppGPFault = "";
                Sounds.Snd_Win_CCSelect = "";
                Sounds.Snd_Win_ChangeTheme = PathsExt.Windows + @"\media\Windows Logon Sound.wav";
                Sounds.Snd_Win_Close = "";
                Sounds.Snd_Win_CriticalBatteryAlarm = PathsExt.Windows + @"\media\Windows Battery Critical.wav";
                Sounds.Snd_Win_DeviceConnect = PathsExt.Windows + @"\media\Windows Hardware Insert.wav";
                Sounds.Snd_Win_DeviceDisconnect = PathsExt.Windows + @"\media\Windows Hardware Remove.wav";
                Sounds.Snd_Win_DeviceFail = PathsExt.Windows + @"\media\Windows Hardware Fail.wav";
                Sounds.Snd_Win_FaxBeep = PathsExt.Windows + @"\media\Windows Notify.wav";
                Sounds.Snd_Win_LowBatteryAlarm = PathsExt.Windows + @"\media\Windows Battery Low.wav";
                Sounds.Snd_Win_MailBeep = PathsExt.Windows + @"\media\Windows Notify.wav";
                Sounds.Snd_Win_Maximize = "";
                Sounds.Snd_Win_MenuCommand = "";
                Sounds.Snd_Win_MenuPopup = "";
                Sounds.Snd_Win_MessageNudge = "";
                Sounds.Snd_Win_Minimize = "";
                Sounds.Snd_Win_Notification_Default = "";
                Sounds.Snd_Win_Notification_IM = "";
                Sounds.Snd_Win_Notification_Looping_Alarm = "";
                Sounds.Snd_Win_Notification_Looping_Alarm2 = "";
                Sounds.Snd_Win_Notification_Looping_Alarm3 = "";
                Sounds.Snd_Win_Notification_Looping_Alarm4 = "";
                Sounds.Snd_Win_Notification_Looping_Alarm5 = "";
                Sounds.Snd_Win_Notification_Looping_Alarm6 = "";
                Sounds.Snd_Win_Notification_Looping_Alarm7 = "";
                Sounds.Snd_Win_Notification_Looping_Alarm8 = "";
                Sounds.Snd_Win_Notification_Looping_Alarm9 = "";
                Sounds.Snd_Win_Notification_Looping_Alarm10 = "";
                Sounds.Snd_Win_Notification_Looping_Call = "";
                Sounds.Snd_Win_Notification_Looping_Call2 = "";
                Sounds.Snd_Win_Notification_Looping_Call3 = "";
                Sounds.Snd_Win_Notification_Looping_Call4 = "";
                Sounds.Snd_Win_Notification_Looping_Call5 = "";
                Sounds.Snd_Win_Notification_Looping_Call6 = "";
                Sounds.Snd_Win_Notification_Looping_Call7 = "";
                Sounds.Snd_Win_Notification_Looping_Call8 = "";
                Sounds.Snd_Win_Notification_Looping_Call9 = "";
                Sounds.Snd_Win_Notification_Looping_Call10 = "";
                Sounds.Snd_Win_Notification_Mail = "";
                Sounds.Snd_Win_Notification_Proximity = "";
                Sounds.Snd_Win_Notification_Reminder = "";
                Sounds.Snd_Win_Notification_SMS = "";
                Sounds.Snd_Win_Open = "";
                Sounds.Snd_Win_PrintComplete = "";
                Sounds.Snd_Win_ProximityConnection = "";
                Sounds.Snd_Win_RestoreDown = "";
                Sounds.Snd_Win_RestoreUp = "";
                Sounds.Snd_Win_ShowBand = "";
                Sounds.Snd_Win_SystemAsterisk = PathsExt.Windows + @"\media\Windows Error.wav";
                Sounds.Snd_Win_SystemExclamation = PathsExt.Windows + @"\media\Windows Exclamation.wav";
                Sounds.Snd_Win_SystemExit = PathsExt.Windows + @"\media\Windows Shutdown.wav";
                Sounds.Snd_Win_SystemStart = "";
                Sounds.Snd_Win_SystemHand = PathsExt.Windows + @"\media\Windows Critical Stop.wav";
                Sounds.Snd_Win_SystemNotification = PathsExt.Windows + @"\media\Windows Balloon.wav";
                Sounds.Snd_Win_SystemQuestion = "";
                Sounds.Snd_Win_WindowsLogoff = PathsExt.Windows + @"\media\Windows Logoff Sound.wav";
                Sounds.Snd_Win_WindowsLogon = PathsExt.Windows + @"\media\Windows Logon Sound.wav";
                Sounds.Snd_Win_WindowsUAC = PathsExt.Windows + @"\media\Windows User Account Control.wav";
                Sounds.Snd_Win_WindowsUnlock = "";
                Sounds.Snd_Explorer_ActivatingDocument = "";
                Sounds.Snd_Explorer_BlockedPopup = PathsExt.Windows + @"\media\Windows Pop-up Blocked.wav";
                Sounds.Snd_Explorer_EmptyRecycleBin = PathsExt.Windows + @"\media\Windows Recycle.wav";
                Sounds.Snd_Explorer_FeedDiscovered = PathsExt.Windows + @"\media\Windows Feed Discovered.wav";
                Sounds.Snd_Explorer_MoveMenuItem = "";
                Sounds.Snd_Explorer_Navigating = PathsExt.Windows + @"\media\Windows Navigation Start.wav";
                Sounds.Snd_Explorer_SecurityBand = PathsExt.Windows + @"\media\Windows Information Bar.wav";
                Sounds.Snd_Explorer_SearchProviderDiscovered = "";
                Sounds.Snd_Explorer_FaxError = PathsExt.Windows + @"\media\ding.wav";
                Sounds.Snd_Explorer_FaxLineRings = PathsExt.Windows + @"\media\Windows Ringin.wav";
                Sounds.Snd_Explorer_FaxNew = "";
                Sounds.Snd_Explorer_FaxSent = PathsExt.Windows + @"\media\tada.wav";
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

                Sounds.Snd_Win_SystemExit_TaskMgmt = false;
                Sounds.Snd_Win_WindowsLogoff_TaskMgmt = false;
                Sounds.Snd_Win_WindowsLogon_TaskMgmt = false;
                Sounds.Snd_Win_WindowsUnlock_TaskMgmt = false;
            }

            return TM;
        }
    }
}