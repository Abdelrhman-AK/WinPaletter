using System;
using System.Drawing;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter.Theme
{
    public class Default : IDisposable
    {

        public byte[] Default_Windows11Accents_Bytes;

        public byte[] Default_Windows10Accents_Bytes;

        public Default()
        {
            Default_Windows11Accents_Bytes = new[] { Windows11().Windows11.Color_Index0.R, Windows11().Windows11.Color_Index0.G, Windows11().Windows11.Color_Index0.B, (byte)255, Windows11().Windows11.Color_Index1.R, Windows11().Windows11.Color_Index1.G, Windows11().Windows11.Color_Index1.B, (byte)255, Windows11().Windows11.Color_Index2.R, Windows11().Windows11.Color_Index2.G, Windows11().Windows11.Color_Index2.B, (byte)255, Windows11().Windows11.Color_Index3.R, Windows11().Windows11.Color_Index3.G, Windows11().Windows11.Color_Index3.B, (byte)255, Windows11().Windows11.Color_Index4.R, Windows11().Windows11.Color_Index4.G, Windows11().Windows11.Color_Index4.B, (byte)255, Windows11().Windows11.Color_Index5.R, Windows11().Windows11.Color_Index5.G, Windows11().Windows11.Color_Index5.B, (byte)255, Windows11().Windows11.Color_Index6.R, Windows11().Windows11.Color_Index6.G, Windows11().Windows11.Color_Index6.B, (byte)255, Windows11().Windows11.Color_Index7.R, Windows11().Windows11.Color_Index7.G, Windows11().Windows11.Color_Index7.B, (byte)255 };
            Default_Windows10Accents_Bytes = new[] { Windows10().Windows11.Color_Index0.R, Windows10().Windows11.Color_Index0.G, Windows10().Windows11.Color_Index0.B, (byte)255, Windows10().Windows11.Color_Index1.R, Windows10().Windows11.Color_Index1.G, Windows10().Windows11.Color_Index1.B, (byte)255, Windows10().Windows11.Color_Index2.R, Windows10().Windows11.Color_Index2.G, Windows10().Windows11.Color_Index2.B, (byte)255, Windows10().Windows11.Color_Index3.R, Windows10().Windows11.Color_Index3.G, Windows10().Windows11.Color_Index3.B, (byte)255, Windows10().Windows11.Color_Index4.R, Windows10().Windows11.Color_Index4.G, Windows10().Windows11.Color_Index4.B, (byte)255, Windows10().Windows11.Color_Index5.R, Windows10().Windows11.Color_Index5.G, Windows10().Windows11.Color_Index5.B, (byte)255, Windows10().Windows11.Color_Index6.R, Windows10().Windows11.Color_Index6.G, Windows10().Windows11.Color_Index6.B, (byte)255, Windows10().Windows11.Color_Index7.R, Windows10().Windows11.Color_Index7.G, Windows10().Windows11.Color_Index7.B, (byte)255 };
        }

        public static Theme.Manager From(WindowStyle PreviewStyle)
        {
            Theme.Manager _Def;

            if (PreviewStyle == WindowStyle.W11)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.Windows11();
                }
            }

            else if (PreviewStyle == WindowStyle.W10)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.Windows10();
                }
            }

            else if (PreviewStyle == WindowStyle.W81)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.Windows81();
                }
            }

            else if (PreviewStyle == WindowStyle.W7)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.Windows7();
                }
            }

            else if (PreviewStyle == WindowStyle.WVista)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.WindowsVista();
                }
            }

            else if (PreviewStyle == WindowStyle.WXP)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.WindowsXP();
                }
            }

            else
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.Windows11();
                }

            }

            return _Def;
        }

        public static Theme.Manager Get()
        {
            Theme.Manager _Def;

            if (Program.W11 | Program.W12)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.Windows11();
                }
            }

            else if (Program.W10)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.Windows10();
                }
            }

            else if (Program.W81)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.Windows81();
                }
            }

            else if (Program.W7)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.Windows7();
                }
            }

            else if (Program.WVista)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.WindowsVista();
                }
            }

            else if (Program.WXP)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.WindowsXP();
                }
            }

            else
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.Windows11();
                }

            }

            return _Def;
        }

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
                Info.AppVersion = Program.AppVersion;
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
                Sounds.Snd_Win_Default = Program.PATH_Windows + @"\media\Windows Background.wav";
                Sounds.Snd_Win_AppGPFault = "";
                Sounds.Snd_Win_CCSelect = "";
                Sounds.Snd_Win_ChangeTheme = "";
                Sounds.Snd_Win_Close = "";
                Sounds.Snd_Win_CriticalBatteryAlarm = Program.PATH_Windows + @"\media\Windows Foreground.wav";
                Sounds.Snd_Win_DeviceConnect = Program.PATH_Windows + @"\media\Windows Hardware Insert.wav";
                Sounds.Snd_Win_DeviceDisconnect = Program.PATH_Windows + @"\media\Windows Hardware Remove.wav";
                Sounds.Snd_Win_DeviceFail = Program.PATH_Windows + @"\media\Windows Hardware Fail.wav";
                Sounds.Snd_Win_FaxBeep = Program.PATH_Windows + @"\media\Windows Notify Email.wav";
                Sounds.Snd_Win_LowBatteryAlarm = Program.PATH_Windows + @"\media\Windows Background.wav";
                Sounds.Snd_Win_MailBeep = Program.PATH_Windows + @"\media\Windows Notify Email.wav";
                Sounds.Snd_Win_Maximize = "";
                Sounds.Snd_Win_MenuCommand = "";
                Sounds.Snd_Win_MenuPopup = "";
                Sounds.Snd_Win_MessageNudge = Program.PATH_Windows + @"\media\Windows Message Nudge.wav";
                Sounds.Snd_Win_Minimize = "";
                Sounds.Snd_Win_Notification_Default = Program.PATH_Windows + @"\media\Windows Notify System Generic.wav";
                Sounds.Snd_Win_Notification_IM = Program.PATH_Windows + @"\media\Windows Notify Messaging.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm = Program.PATH_Windows + @"\media\Alarm01.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm2 = Program.PATH_Windows + @"\media\Alarm02.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm3 = Program.PATH_Windows + @"\media\Alarm03.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm4 = Program.PATH_Windows + @"\media\Alarm04.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm5 = Program.PATH_Windows + @"\media\Alarm05.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm6 = Program.PATH_Windows + @"\media\Alarm06.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm7 = Program.PATH_Windows + @"\media\Alarm07.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm8 = Program.PATH_Windows + @"\media\Alarm08.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm9 = Program.PATH_Windows + @"\media\Alarm09.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm10 = Program.PATH_Windows + @"\media\Alarm10.wav";
                Sounds.Snd_Win_Notification_Looping_Call = Program.PATH_Windows + @"\media\Ring01.wav";
                Sounds.Snd_Win_Notification_Looping_Call2 = Program.PATH_Windows + @"\media\Ring02.wav";
                Sounds.Snd_Win_Notification_Looping_Call3 = Program.PATH_Windows + @"\media\Ring03.wav";
                Sounds.Snd_Win_Notification_Looping_Call4 = Program.PATH_Windows + @"\media\Ring04.wav";
                Sounds.Snd_Win_Notification_Looping_Call5 = Program.PATH_Windows + @"\media\Ring05.wav";
                Sounds.Snd_Win_Notification_Looping_Call6 = Program.PATH_Windows + @"\media\Ring06.wav";
                Sounds.Snd_Win_Notification_Looping_Call7 = Program.PATH_Windows + @"\media\Ring07.wav";
                Sounds.Snd_Win_Notification_Looping_Call8 = Program.PATH_Windows + @"\media\Ring08.wav";
                Sounds.Snd_Win_Notification_Looping_Call9 = Program.PATH_Windows + @"\media\Ring09.wav";
                Sounds.Snd_Win_Notification_Looping_Call10 = Program.PATH_Windows + @"\media\Ring10.wav";
                Sounds.Snd_Win_Notification_Mail = Program.PATH_Windows + @"\media\Windows Notify Email.wav";
                Sounds.Snd_Win_Notification_Proximity = Program.PATH_Windows + @"\media\Windows Proximity Notification.wav";
                Sounds.Snd_Win_Notification_Reminder = Program.PATH_Windows + @"\media\Windows Notify Calendar.wav";
                Sounds.Snd_Win_Notification_SMS = Program.PATH_Windows + @"\media\Windows Notify Messaging.wav";
                Sounds.Snd_Win_Open = "";
                Sounds.Snd_Win_PrintComplete = "";
                Sounds.Snd_Win_ProximityConnection = Program.PATH_Windows + @"\media\Windows Proximity Connection.wav";
                Sounds.Snd_Win_RestoreDown = "";
                Sounds.Snd_Win_RestoreUp = "";
                Sounds.Snd_Win_ShowBand = "";
                Sounds.Snd_Win_SystemAsterisk = Program.PATH_Windows + @"\media\Windows Background.wav";
                Sounds.Snd_Win_SystemExclamation = Program.PATH_Windows + @"\media\Windows Background.wav";
                Sounds.Snd_Win_SystemExit = "";
                Sounds.Snd_Win_SystemStart = "";
                Sounds.Snd_Win_SystemHand = Program.PATH_Windows + @"\media\Windows Foreground.wav";
                Sounds.Snd_Win_SystemNotification = Program.PATH_Windows + @"\media\Windows Background.wav";
                Sounds.Snd_Win_SystemQuestion = "";
                Sounds.Snd_Win_WindowsLogoff = "";
                Sounds.Snd_Win_WindowsLogon = "";
                Sounds.Snd_Win_WindowsUAC = Program.PATH_Windows + @"\media\Windows User Account Control.wav";
                Sounds.Snd_Win_WindowsUnlock = Program.PATH_Windows + @"\media\Windows Unlock.wav";
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
                Sounds.Snd_SpeechRec_DisNumbersSound = Program.PATH_Windows + @"\media\Speech Disambiguation.wav";
                Sounds.Snd_SpeechRec_HubOffSound = Program.PATH_Windows + @"\media\Speech Off.wav";
                Sounds.Snd_SpeechRec_HubOnSound = Program.PATH_Windows + @"\media\Speech On.wav";
                Sounds.Snd_SpeechRec_HubSleepSound = Program.PATH_Windows + @"\media\Speech Sleep.wav";
                Sounds.Snd_SpeechRec_MisrecoSound = Program.PATH_Windows + @"\media\Speech Misrecognition.wav";
                Sounds.Snd_SpeechRec_PanelSound = Program.PATH_Windows + @"\media\Speech Disambiguation.wav";

                Sounds.Snd_Win_SystemExit_TaskMgmt = true;
                Sounds.Snd_Win_WindowsLogoff_TaskMgmt = true;
                Sounds.Snd_Win_WindowsLogon_TaskMgmt = true;
                Sounds.Snd_Win_WindowsUnlock_TaskMgmt = true;
            }

            return TM;
        }

        public Theme.Manager Windows10()
        {
            var TM = new Theme.Manager(WinPaletter.Theme.Manager.Source.Empty);

            {
                ref Theme.Structures.Info Info = ref TM.Info;
                Info.ThemeName = "Windows 10 (Initial)";
                Info.Description = "Initial; Like first time after Windows Setup";
                Info.ThemeVersion = "1.0.0.0";
                Info.Author = "Microsoft";
                Info.AuthorSocialMediaLink = "www.microsoft.com";
                Info.AppVersion = Program.AppVersion;
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
                CMD.W10_1909_ForceV2 = Program.W10_1909;
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
                PS86.W10_1909_ForceV2 = Program.W10_1909;
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
                PS64.W10_1909_ForceV2 = Program.W10_1909;
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
                WinEffects.ShakeToMinimize = true;
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
                Sounds.Snd_Imageres_SystemStart = "";
                Sounds.Snd_Win_Default = Program.PATH_Windows + @"\media\Windows Background.wav";
                Sounds.Snd_Win_AppGPFault = "";
                Sounds.Snd_Win_CCSelect = "";
                Sounds.Snd_Win_ChangeTheme = "";
                Sounds.Snd_Win_Close = "";
                Sounds.Snd_Win_CriticalBatteryAlarm = Program.PATH_Windows + @"\media\Windows Foreground.wav";
                Sounds.Snd_Win_DeviceConnect = Program.PATH_Windows + @"\media\Windows Hardware Insert.wav";
                Sounds.Snd_Win_DeviceDisconnect = Program.PATH_Windows + @"\media\Windows Hardware Remove.wav";
                Sounds.Snd_Win_DeviceFail = Program.PATH_Windows + @"\media\Windows Hardware Fail.wav";
                Sounds.Snd_Win_FaxBeep = Program.PATH_Windows + @"\media\Windows Notify Email.wav";
                Sounds.Snd_Win_LowBatteryAlarm = Program.PATH_Windows + @"\media\Windows Background.wav";
                Sounds.Snd_Win_MailBeep = Program.PATH_Windows + @"\media\Windows Notify Email.wav";
                Sounds.Snd_Win_Maximize = "";
                Sounds.Snd_Win_MenuCommand = "";
                Sounds.Snd_Win_MenuPopup = "";
                Sounds.Snd_Win_MessageNudge = Program.PATH_Windows + @"\media\Windows Message Nudge.wav";
                Sounds.Snd_Win_Minimize = "";
                Sounds.Snd_Win_Notification_Default = Program.PATH_Windows + @"\media\Windows Notify System Generic.wav";
                Sounds.Snd_Win_Notification_IM = Program.PATH_Windows + @"\media\Windows Notify Messaging.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm = Program.PATH_Windows + @"\media\Alarm01.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm2 = Program.PATH_Windows + @"\media\Alarm02.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm3 = Program.PATH_Windows + @"\media\Alarm03.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm4 = Program.PATH_Windows + @"\media\Alarm04.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm5 = Program.PATH_Windows + @"\media\Alarm05.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm6 = Program.PATH_Windows + @"\media\Alarm06.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm7 = Program.PATH_Windows + @"\media\Alarm07.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm8 = Program.PATH_Windows + @"\media\Alarm08.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm9 = Program.PATH_Windows + @"\media\Alarm09.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm10 = Program.PATH_Windows + @"\media\Alarm10.wav";
                Sounds.Snd_Win_Notification_Looping_Call = Program.PATH_Windows + @"\media\Ring01.wav";
                Sounds.Snd_Win_Notification_Looping_Call2 = Program.PATH_Windows + @"\media\Ring02.wav";
                Sounds.Snd_Win_Notification_Looping_Call3 = Program.PATH_Windows + @"\media\Ring03.wav";
                Sounds.Snd_Win_Notification_Looping_Call4 = Program.PATH_Windows + @"\media\Ring04.wav";
                Sounds.Snd_Win_Notification_Looping_Call5 = Program.PATH_Windows + @"\media\Ring05.wav";
                Sounds.Snd_Win_Notification_Looping_Call6 = Program.PATH_Windows + @"\media\Ring06.wav";
                Sounds.Snd_Win_Notification_Looping_Call7 = Program.PATH_Windows + @"\media\Ring07.wav";
                Sounds.Snd_Win_Notification_Looping_Call8 = Program.PATH_Windows + @"\media\Ring08.wav";
                Sounds.Snd_Win_Notification_Looping_Call9 = Program.PATH_Windows + @"\media\Ring09.wav";
                Sounds.Snd_Win_Notification_Looping_Call10 = Program.PATH_Windows + @"\media\Ring10.wav";
                Sounds.Snd_Win_Notification_Mail = Program.PATH_Windows + @"\media\Windows Notify Email.wav";
                Sounds.Snd_Win_Notification_Proximity = Program.PATH_Windows + @"\media\Windows Proximity Notification.wav";
                Sounds.Snd_Win_Notification_Reminder = Program.PATH_Windows + @"\media\Windows Notify Calendar.wav";
                Sounds.Snd_Win_Notification_SMS = Program.PATH_Windows + @"\media\Windows Notify Messaging.wav";
                Sounds.Snd_Win_Open = "";
                Sounds.Snd_Win_PrintComplete = "";
                Sounds.Snd_Win_ProximityConnection = Program.PATH_Windows + @"\media\Windows Proximity Connection.wav";
                Sounds.Snd_Win_RestoreDown = "";
                Sounds.Snd_Win_RestoreUp = "";
                Sounds.Snd_Win_ShowBand = "";
                Sounds.Snd_Win_SystemAsterisk = Program.PATH_Windows + @"\media\Windows Background.wav";
                Sounds.Snd_Win_SystemExclamation = Program.PATH_Windows + @"\media\Windows Background.wav";
                Sounds.Snd_Win_SystemExit = "";
                Sounds.Snd_Win_SystemStart = "";
                Sounds.Snd_Win_SystemHand = Program.PATH_Windows + @"\media\Windows Foreground.wav";
                Sounds.Snd_Win_SystemNotification = Program.PATH_Windows + @"\media\Windows Background.wav";
                Sounds.Snd_Win_SystemQuestion = "";
                Sounds.Snd_Win_WindowsLogoff = "";
                Sounds.Snd_Win_WindowsLogon = "";
                Sounds.Snd_Win_WindowsUAC = Program.PATH_Windows + @"\media\Windows User Account Control.wav";
                Sounds.Snd_Win_WindowsUnlock = Program.PATH_Windows + @"\media\Windows Unlock.wav";
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
                Sounds.Snd_SpeechRec_DisNumbersSound = Program.PATH_Windows + @"\media\Speech Disambiguation.wav";
                Sounds.Snd_SpeechRec_HubOffSound = Program.PATH_Windows + @"\media\Speech Off.wav";
                Sounds.Snd_SpeechRec_HubOnSound = Program.PATH_Windows + @"\media\Speech On.wav";
                Sounds.Snd_SpeechRec_HubSleepSound = Program.PATH_Windows + @"\media\Speech Sleep.wav";
                Sounds.Snd_SpeechRec_MisrecoSound = Program.PATH_Windows + @"\media\Speech Misrecognition.wav";
                Sounds.Snd_SpeechRec_PanelSound = Program.PATH_Windows + @"\media\Speech Disambiguation.wav";

                Sounds.Snd_Win_SystemExit_TaskMgmt = true;
                Sounds.Snd_Win_WindowsLogoff_TaskMgmt = true;
                Sounds.Snd_Win_WindowsLogon_TaskMgmt = true;
                Sounds.Snd_Win_WindowsUnlock_TaskMgmt = true;
            }

            return TM;
        }

        public Theme.Manager Windows81()
        {
            var TM = new Theme.Manager(WinPaletter.Theme.Manager.Source.Empty);

            {
                ref Theme.Structures.Info Info = ref TM.Info;
                Info.ThemeName = "Windows 8.1 (Initial)";
                Info.Description = "Initial; Like first time after Windows Setup";
                Info.ThemeVersion = "1.0.0.0";
                Info.Author = "Microsoft";
                Info.AuthorSocialMediaLink = "www.microsoft.com";
                Info.AppVersion = Program.AppVersion;
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
                Win7.ColorizationColor = Color.FromArgb(246, 195, 74);
                Win7.ColorizationAfterglow = Color.FromArgb(0, 0, 0);
                Win7.ColorizationColorBalance = 78;
                Win7.ColorizationAfterglowBalance = 31;
                Win7.ColorizationBlurBalance = 31;
                Win7.ColorizationGlassReflectionIntensity = 0;
                Win7.EnableAeroPeek = true;
                Win7.AlwaysHibernateThumbnails = false;
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
                MetricsFonts.CaptionFont = new Font("Segoe UI", 11.25f, FontStyle.Regular);
                MetricsFonts.SmCaptionFont = new Font("Segoe UI", 11.25f, FontStyle.Regular);
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
            TM.Cursor_Shadow = false;

            {
                ref Theme.Structures.Sounds Sounds = ref TM.Sounds;
                Sounds.Snd_Imageres_SystemStart = "";
                Sounds.Snd_Win_Default = Program.PATH_Windows + @"\media\Windows Background.wav";
                Sounds.Snd_Win_AppGPFault = "";
                Sounds.Snd_Win_CCSelect = "";
                Sounds.Snd_Win_ChangeTheme = "";
                Sounds.Snd_Win_Close = "";
                Sounds.Snd_Win_CriticalBatteryAlarm = Program.PATH_Windows + @"\media\Windows Foreground.wav";
                Sounds.Snd_Win_DeviceConnect = Program.PATH_Windows + @"\media\Windows Hardware Insert.wav";
                Sounds.Snd_Win_DeviceDisconnect = Program.PATH_Windows + @"\media\Windows Hardware Remove.wav";
                Sounds.Snd_Win_DeviceFail = Program.PATH_Windows + @"\media\Windows Hardware Fail.wav";
                Sounds.Snd_Win_FaxBeep = Program.PATH_Windows + @"\media\Windows Notify Email.wav";
                Sounds.Snd_Win_LowBatteryAlarm = Program.PATH_Windows + @"\media\Windows Background.wav";
                Sounds.Snd_Win_MailBeep = Program.PATH_Windows + @"\media\Windows Notify Email.wav";
                Sounds.Snd_Win_Maximize = "";
                Sounds.Snd_Win_MenuCommand = "";
                Sounds.Snd_Win_MenuPopup = "";
                Sounds.Snd_Win_MessageNudge = Program.PATH_Windows + @"\media\Windows Message Nudge.wav";
                Sounds.Snd_Win_Minimize = "";
                Sounds.Snd_Win_Notification_Default = Program.PATH_Windows + @"\media\Windows Notify System Generic.wav";
                Sounds.Snd_Win_Notification_IM = Program.PATH_Windows + @"\media\Windows Notify Messaging.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm = Program.PATH_Windows + @"\media\Alarm01.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm2 = Program.PATH_Windows + @"\media\Alarm02.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm3 = Program.PATH_Windows + @"\media\Alarm03.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm4 = Program.PATH_Windows + @"\media\Alarm04.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm5 = Program.PATH_Windows + @"\media\Alarm05.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm6 = Program.PATH_Windows + @"\media\Alarm06.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm7 = Program.PATH_Windows + @"\media\Alarm07.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm8 = Program.PATH_Windows + @"\media\Alarm08.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm9 = Program.PATH_Windows + @"\media\Alarm09.wav";
                Sounds.Snd_Win_Notification_Looping_Alarm10 = Program.PATH_Windows + @"\media\Alarm10.wav";
                Sounds.Snd_Win_Notification_Looping_Call = Program.PATH_Windows + @"\media\Ring01.wav";
                Sounds.Snd_Win_Notification_Looping_Call2 = Program.PATH_Windows + @"\media\Ring02.wav";
                Sounds.Snd_Win_Notification_Looping_Call3 = Program.PATH_Windows + @"\media\Ring03.wav";
                Sounds.Snd_Win_Notification_Looping_Call4 = Program.PATH_Windows + @"\media\Ring04.wav";
                Sounds.Snd_Win_Notification_Looping_Call5 = Program.PATH_Windows + @"\media\Ring05.wav";
                Sounds.Snd_Win_Notification_Looping_Call6 = Program.PATH_Windows + @"\media\Ring06.wav";
                Sounds.Snd_Win_Notification_Looping_Call7 = Program.PATH_Windows + @"\media\Ring07.wav";
                Sounds.Snd_Win_Notification_Looping_Call8 = Program.PATH_Windows + @"\media\Ring08.wav";
                Sounds.Snd_Win_Notification_Looping_Call9 = Program.PATH_Windows + @"\media\Ring09.wav";
                Sounds.Snd_Win_Notification_Looping_Call10 = Program.PATH_Windows + @"\media\Ring10.wav";
                Sounds.Snd_Win_Notification_Mail = Program.PATH_Windows + @"\media\Windows Notify Email.wav";
                Sounds.Snd_Win_Notification_Proximity = Program.PATH_Windows + @"\media\Windows Proximity Notification.wav";
                Sounds.Snd_Win_Notification_Reminder = Program.PATH_Windows + @"\media\Windows Notify Calendar.wav";
                Sounds.Snd_Win_Notification_SMS = Program.PATH_Windows + @"\media\Windows Notify Messaging.wav";
                Sounds.Snd_Win_Open = "";
                Sounds.Snd_Win_PrintComplete = "";
                Sounds.Snd_Win_ProximityConnection = Program.PATH_Windows + @"\media\Windows Proximity Connection.wav";
                Sounds.Snd_Win_RestoreDown = "";
                Sounds.Snd_Win_RestoreUp = "";
                Sounds.Snd_Win_ShowBand = "";
                Sounds.Snd_Win_SystemAsterisk = Program.PATH_Windows + @"\media\Windows Background.wav";
                Sounds.Snd_Win_SystemExclamation = Program.PATH_Windows + @"\media\Windows Background.wav";
                Sounds.Snd_Win_SystemExit = "";
                Sounds.Snd_Win_SystemStart = "";
                Sounds.Snd_Win_SystemHand = Program.PATH_Windows + @"\media\Windows Foreground.wav";
                Sounds.Snd_Win_SystemNotification = Program.PATH_Windows + @"\media\Windows Background.wav";
                Sounds.Snd_Win_SystemQuestion = "";
                Sounds.Snd_Win_WindowsLogoff = "";
                Sounds.Snd_Win_WindowsLogon = "";
                Sounds.Snd_Win_WindowsUAC = Program.PATH_Windows + @"\media\Windows User Account Control.wav";
                Sounds.Snd_Win_WindowsUnlock = Program.PATH_Windows + @"\media\Windows Unlock.wav";
                Sounds.Snd_Explorer_ActivatingDocument = "";
                Sounds.Snd_Explorer_BlockedPopup = Program.PATH_Windows + @"\media\Windows Pop-up Blocked.wav";
                Sounds.Snd_Explorer_EmptyRecycleBin = "";
                Sounds.Snd_Explorer_FeedDiscovered = Program.PATH_Windows + @"\media\Windows Feed Discovered.wav";
                Sounds.Snd_Explorer_MoveMenuItem = "";
                Sounds.Snd_Explorer_Navigating = "";
                Sounds.Snd_Explorer_SecurityBand = Program.PATH_Windows + @"\media\Windows Information Bar.wav";
                Sounds.Snd_Explorer_SearchProviderDiscovered = "";
                Sounds.Snd_Explorer_FaxError = "";
                Sounds.Snd_Explorer_FaxLineRings = "";
                Sounds.Snd_Explorer_FaxNew = "";
                Sounds.Snd_Explorer_FaxSent = "";
                Sounds.Snd_NetMeeting_PersonJoins = "";
                Sounds.Snd_NetMeeting_PersonLeaves = "";
                Sounds.Snd_NetMeeting_ReceiveCall = "";
                Sounds.Snd_NetMeeting_ReceiveRequestToJoin = "";
                Sounds.Snd_SpeechRec_DisNumbersSound = Program.PATH_Windows + @"\media\Speech Disambiguation.wav";
                Sounds.Snd_SpeechRec_HubOffSound = Program.PATH_Windows + @"\media\Speech Off.wav";
                Sounds.Snd_SpeechRec_HubOnSound = Program.PATH_Windows + @"\media\Speech On.wav";
                Sounds.Snd_SpeechRec_HubSleepSound = Program.PATH_Windows + @"\media\Speech Sleep.wav";
                Sounds.Snd_SpeechRec_MisrecoSound = Program.PATH_Windows + @"\media\Speech Misrecognition.wav";
                Sounds.Snd_SpeechRec_PanelSound = Program.PATH_Windows + @"\media\Speech Disambiguation.wav";

                Sounds.Snd_Win_SystemExit_TaskMgmt = true;
                Sounds.Snd_Win_WindowsLogoff_TaskMgmt = true;
                Sounds.Snd_Win_WindowsLogon_TaskMgmt = true;
                Sounds.Snd_Win_WindowsUnlock_TaskMgmt = true;
            }

            return TM;
        }

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
                Info.AppVersion = Program.AppVersion;
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
                Sounds.Snd_Win_Default = Program.PATH_Windows + @"\media\Windows Ding.wav";
                Sounds.Snd_Win_AppGPFault = "";
                Sounds.Snd_Win_CCSelect = "";
                Sounds.Snd_Win_ChangeTheme = Program.PATH_Windows + @"\media\Windows Logon Sound.wav";
                Sounds.Snd_Win_Close = "";
                Sounds.Snd_Win_CriticalBatteryAlarm = Program.PATH_Windows + @"\media\Windows Battery Critical.wav";
                Sounds.Snd_Win_DeviceConnect = Program.PATH_Windows + @"\media\Windows Hardware Insert.wav";
                Sounds.Snd_Win_DeviceDisconnect = Program.PATH_Windows + @"\media\Windows Hardware Remove.wav";
                Sounds.Snd_Win_DeviceFail = Program.PATH_Windows + @"\media\Windows Hardware Fail.wav";
                Sounds.Snd_Win_FaxBeep = Program.PATH_Windows + @"\media\Windows Notify.wav";
                Sounds.Snd_Win_LowBatteryAlarm = Program.PATH_Windows + @"\media\Windows Battery Low.wav";
                Sounds.Snd_Win_MailBeep = Program.PATH_Windows + @"\media\Windows Notify.wav";
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
                Sounds.Snd_Win_SystemAsterisk = Program.PATH_Windows + @"\media\Windows Error.wav";
                Sounds.Snd_Win_SystemExclamation = Program.PATH_Windows + @"\media\Windows Exclamation.wav";
                Sounds.Snd_Win_SystemExit = Program.PATH_Windows + @"\media\Windows Shutdown.wav";
                Sounds.Snd_Win_SystemStart = "";
                Sounds.Snd_Win_SystemHand = Program.PATH_Windows + @"\media\Windows Critical Stop.wav";
                Sounds.Snd_Win_SystemNotification = Program.PATH_Windows + @"\media\Windows Balloon.wav";
                Sounds.Snd_Win_SystemQuestion = "";
                Sounds.Snd_Win_WindowsLogoff = Program.PATH_Windows + @"\media\Windows Logoff Sound.wav";
                Sounds.Snd_Win_WindowsLogon = Program.PATH_Windows + @"\media\Windows Logon Sound.wav";
                Sounds.Snd_Win_WindowsUAC = Program.PATH_Windows + @"\media\Windows User Account Control.wav";
                Sounds.Snd_Win_WindowsUnlock = "";
                Sounds.Snd_Explorer_ActivatingDocument = "";
                Sounds.Snd_Explorer_BlockedPopup = Program.PATH_Windows + @"\media\Windows Pop-up Blocked.wav";
                Sounds.Snd_Explorer_EmptyRecycleBin = Program.PATH_Windows + @"\media\Windows Recycle.wav";
                Sounds.Snd_Explorer_FeedDiscovered = Program.PATH_Windows + @"\media\Windows Feed Discovered.wav";
                Sounds.Snd_Explorer_MoveMenuItem = "";
                Sounds.Snd_Explorer_Navigating = Program.PATH_Windows + @"\media\Windows Navigation Start.wav";
                Sounds.Snd_Explorer_SecurityBand = Program.PATH_Windows + @"\media\Windows Information Bar.wav";
                Sounds.Snd_Explorer_SearchProviderDiscovered = "";
                Sounds.Snd_Explorer_FaxError = Program.PATH_Windows + @"\media\ding.wav";
                Sounds.Snd_Explorer_FaxLineRings = Program.PATH_Windows + @"\media\Windows Ringin.wav";
                Sounds.Snd_Explorer_FaxNew = "";
                Sounds.Snd_Explorer_FaxSent = Program.PATH_Windows + @"\media\tada.wav";
                Sounds.Snd_NetMeeting_PersonJoins = "";
                Sounds.Snd_NetMeeting_PersonLeaves = "";
                Sounds.Snd_NetMeeting_ReceiveCall = "";
                Sounds.Snd_NetMeeting_ReceiveRequestToJoin = "";
                Sounds.Snd_SpeechRec_DisNumbersSound = Program.PATH_Windows + @"\media\Speech Disambiguation.wav";
                Sounds.Snd_SpeechRec_HubOffSound = Program.PATH_Windows + @"\media\Speech Off.wav";
                Sounds.Snd_SpeechRec_HubOnSound = Program.PATH_Windows + @"\media\Speech On.wav";
                Sounds.Snd_SpeechRec_HubSleepSound = Program.PATH_Windows + @"\media\Speech Sleep.wav";
                Sounds.Snd_SpeechRec_MisrecoSound = Program.PATH_Windows + @"\media\Speech Misrecognition.wav";
                Sounds.Snd_SpeechRec_PanelSound = Program.PATH_Windows + @"\media\Speech Disambiguation.wav";

                Sounds.Snd_Win_SystemExit_TaskMgmt = false;
                Sounds.Snd_Win_WindowsLogoff_TaskMgmt = false;
                Sounds.Snd_Win_WindowsLogon_TaskMgmt = false;
                Sounds.Snd_Win_WindowsUnlock_TaskMgmt = false;
            }

            return TM;
        }

        public Theme.Manager WindowsVista()
        {
            var TM = new Theme.Manager(WinPaletter.Theme.Manager.Source.Empty);

            {
                ref Theme.Structures.Info Info = ref TM.Info;
                Info.ThemeName = "Windows Vista (Initial)";
                Info.Description = "Initial; Like first time after Windows Setup";
                Info.ThemeVersion = "1.0.0.0";
                Info.Author = "Microsoft";
                Info.AuthorSocialMediaLink = "www.microsoft.com";
                Info.AppVersion = Program.AppVersion;
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
                ref Theme.Structures.Console PS86 = ref TM.PowerShellx86;
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
                ref Theme.Structures.Console PS64 = ref TM.PowerShellx64;
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
                ref Theme.Structures.MetricsFonts MetricsFonts = ref TM.MetricsFonts;
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
                ref Theme.Structures.WinEffects WinEffects = ref TM.WindowsEffects;
                WinEffects.ShakeToMinimize = false;
                WinEffects.BalloonNotifications = true;
                WinEffects.PaintDesktopVersion = false;
                WinEffects.ShowSecondsInSystemClock = false;
                WinEffects.Win11ClassicContextMenu = false;
                WinEffects.SysListView32 = true;
            }

            TM.Terminal = new WinTerminal("", WinTerminal.Mode.Empty);
            TM.TerminalPreview = new WinTerminal("", WinTerminal.Mode.Empty);
            TM.Cursor_Shadow = true;

            {
                ref Theme.Structures.Sounds Sounds = ref TM.Sounds;
                Sounds.Snd_Imageres_SystemStart = "Default";
                Sounds.Snd_Win_Default = Program.PATH_Windows + @"\media\Windows Ding.wav";
                Sounds.Snd_Win_AppGPFault = "";
                Sounds.Snd_Win_CCSelect = "";
                Sounds.Snd_Win_ChangeTheme = "";
                Sounds.Snd_Win_Close = "";
                Sounds.Snd_Win_CriticalBatteryAlarm = Program.PATH_Windows + @"\media\Windows Battery Critical.wav";
                Sounds.Snd_Win_DeviceConnect = Program.PATH_Windows + @"\media\Windows Hardware Insert.wav";
                Sounds.Snd_Win_DeviceDisconnect = Program.PATH_Windows + @"\media\Windows Hardware Remove.wav";
                Sounds.Snd_Win_DeviceFail = Program.PATH_Windows + @"\media\Windows Hardware Fail.wav";
                Sounds.Snd_Win_FaxBeep = Program.PATH_Windows + @"\media\Windows Notify.wav";
                Sounds.Snd_Win_LowBatteryAlarm = Program.PATH_Windows + @"\media\Windows Battery Low.wav";
                Sounds.Snd_Win_MailBeep = Program.PATH_Windows + @"\media\Windows Notify.wav";
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
                Sounds.Snd_Win_SystemAsterisk = Program.PATH_Windows + @"\media\Windows Error.wav";
                Sounds.Snd_Win_SystemExclamation = Program.PATH_Windows + @"\media\Windows Exclamation.wav";
                Sounds.Snd_Win_SystemExit = Program.PATH_Windows + @"\media\Windows Shutdown.wav";
                Sounds.Snd_Win_SystemStart = "";
                Sounds.Snd_Win_SystemHand = Program.PATH_Windows + @"\media\Windows Critical Stop.wav";
                Sounds.Snd_Win_SystemNotification = Program.PATH_Windows + @"\media\Windows Balloon.wav";
                Sounds.Snd_Win_SystemQuestion = "";
                Sounds.Snd_Win_WindowsLogoff = Program.PATH_Windows + @"\media\Windows Logoff Sound.wav";
                Sounds.Snd_Win_WindowsLogon = Program.PATH_Windows + @"\media\Windows Logon Sound.wav";
                Sounds.Snd_Win_WindowsUAC = Program.PATH_Windows + @"\media\Windows User Account Control.wav";
                Sounds.Snd_Win_WindowsUnlock = "";
                Sounds.Snd_Explorer_ActivatingDocument = "";
                Sounds.Snd_Explorer_BlockedPopup = Program.PATH_Windows + @"\media\Windows Pop-up Blocked.wav";
                Sounds.Snd_Explorer_EmptyRecycleBin = Program.PATH_Windows + @"\media\Windows Recycle.wav";
                Sounds.Snd_Explorer_FeedDiscovered = Program.PATH_Windows + @"\media\Windows Feed Discovered.wav";
                Sounds.Snd_Explorer_MoveMenuItem = "";
                Sounds.Snd_Explorer_Navigating = Program.PATH_Windows + @"\media\Windows Navigation Start.wav";
                Sounds.Snd_Explorer_SecurityBand = Program.PATH_Windows + @"\media\Windows Information Bar.wav";
                Sounds.Snd_Explorer_SearchProviderDiscovered = "";
                Sounds.Snd_Explorer_FaxError = Program.PATH_Windows + @"\media\ding.wav";
                Sounds.Snd_Explorer_FaxLineRings = Program.PATH_Windows + @"\media\Windows Ringin.wav";
                Sounds.Snd_Explorer_FaxNew = "";
                Sounds.Snd_Explorer_FaxSent = Program.PATH_Windows + @"\media\tada.wav";
                Sounds.Snd_NetMeeting_PersonJoins = "";
                Sounds.Snd_NetMeeting_PersonLeaves = "";
                Sounds.Snd_NetMeeting_ReceiveCall = "";
                Sounds.Snd_NetMeeting_ReceiveRequestToJoin = "";
                Sounds.Snd_SpeechRec_DisNumbersSound = Program.PATH_Windows + @"\media\Speech Disambiguation.wav";
                Sounds.Snd_SpeechRec_HubOffSound = Program.PATH_Windows + @"\media\Speech Off.wav";
                Sounds.Snd_SpeechRec_HubOnSound = Program.PATH_Windows + @"\media\Speech On.wav";
                Sounds.Snd_SpeechRec_HubSleepSound = Program.PATH_Windows + @"\media\Speech Sleep.wav";
                Sounds.Snd_SpeechRec_MisrecoSound = Program.PATH_Windows + @"\media\Speech Misrecognition.wav";
                Sounds.Snd_SpeechRec_PanelSound = Program.PATH_Windows + @"\media\Speech Disambiguation.wav";

                Sounds.Snd_Win_SystemExit_TaskMgmt = false;
                Sounds.Snd_Win_WindowsLogoff_TaskMgmt = false;
                Sounds.Snd_Win_WindowsLogon_TaskMgmt = false;
                Sounds.Snd_Win_WindowsUnlock_TaskMgmt = false;
            }

            return TM;
        }

        public Theme.Manager WindowsXP()
        {
            var TM = new Theme.Manager(WinPaletter.Theme.Manager.Source.Empty);

            {
                ref Theme.Structures.Info Info = ref TM.Info;
                Info.ThemeName = "Windows XP (Initial)";
                Info.Description = "Initial; Like first time after Windows Setup";
                Info.ThemeVersion = "1.0.0.0";
                Info.Author = "Microsoft";
                Info.AuthorSocialMediaLink = "www.microsoft.com";
                Info.AppVersion = Program.AppVersion;
            }

            {
                ref Theme.Structures.Win32UI Win32 = ref TM.Win32;
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
            }

            {
                ref Theme.Structures.Console CMD = ref TM.CommandPrompt;
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
                CMD.FontSize = 18 * 65536;
                CMD.FontRaster = true;
                CMD.W10_1909_ForceV2 = false;
            }

            {
                ref Theme.Structures.Console PS86 = ref TM.PowerShellx86;
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
                PS86.FontSize = 14 * 65536;
                PS86.FontRaster = true;
                PS86.W10_1909_ForceV2 = false;
            }

            {
                ref Theme.Structures.Console PS64 = ref TM.PowerShellx64;
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
                PS64.FontSize = 14 * 65536;
                PS64.FontRaster = true;
                PS64.W10_1909_ForceV2 = false;
            }

            {
                ref Theme.Structures.MetricsFonts MetricsFonts = ref TM.MetricsFonts;
                MetricsFonts.BorderWidth = 0;
                MetricsFonts.CaptionHeight = 25;
                MetricsFonts.CaptionWidth = 18;
                MetricsFonts.IconSpacing = 75;
                MetricsFonts.IconVerticalSpacing = 75;
                MetricsFonts.MenuHeight = 19;
                MetricsFonts.MenuWidth = 18;
                MetricsFonts.PaddedBorderWidth = 4;
                MetricsFonts.ScrollHeight = 17;
                MetricsFonts.ScrollWidth = 17;
                MetricsFonts.SmCaptionHeight = 17;
                MetricsFonts.SmCaptionWidth = 17;
                MetricsFonts.DesktopIconSize = 48;
                MetricsFonts.ShellIconSize = 32;
                MetricsFonts.Fonts_SingleBitPP = true;
                MetricsFonts.CaptionFont = new Font("Trebuchet MS", 9.75f, FontStyle.Bold);
                MetricsFonts.SmCaptionFont = new Font("Tahoma", 8.25f, FontStyle.Regular);
                MetricsFonts.IconFont = new Font("Tahoma", 8.25f, FontStyle.Regular);
                MetricsFonts.MenuFont = new Font("Tahoma", 8.25f, FontStyle.Regular);
                MetricsFonts.MessageFont = new Font("Tahoma", 8.25f, FontStyle.Regular);
                MetricsFonts.StatusFont = new Font("Tahoma", 8.25f, FontStyle.Regular);
            }

            TM.Cursor_Shadow = true;
            {
                ref Theme.Structures.Cursor Arrow = ref TM.Cursor_Arrow;
                Arrow.ArrowStyle = Paths.ArrowStyle.Classic;
                Arrow.CircleStyle = Paths.CircleStyle.Classic;
                Arrow.PrimaryColor1 = Color.White;
                Arrow.PrimaryColor2 = Color.White;
                Arrow.SecondaryColor1 = Color.Black;
                Arrow.SecondaryColor2 = Color.Black;
                Arrow.PrimaryColorGradient = false;
                Arrow.SecondaryColorGradient = false;
                Arrow.LoadingCircleBack1 = Color.White;
                Arrow.LoadingCircleBack2 = Color.White;
                Arrow.LoadingCircleHot1 = Color.Black;
                Arrow.LoadingCircleHot2 = Color.Black;
                Arrow.LoadingCircleBackGradient = false;
                Arrow.LoadingCircleHotGradient = false;
                Arrow.Shadow_Enabled = false;
                Arrow.Shadow_Color = Color.Black;
                Arrow.Shadow_Blur = 5;
                Arrow.Shadow_Opacity = 0.3f;
                Arrow.Shadow_OffsetX = 2;
                Arrow.Shadow_OffsetY = 2;
            }

            {
                ref Theme.Structures.Cursor Help = ref TM.Cursor_Help;
                Help.ArrowStyle = Paths.ArrowStyle.Classic;
                Help.CircleStyle = Paths.CircleStyle.Classic;
                Help.PrimaryColor1 = Color.White;
                Help.PrimaryColor2 = Color.White;
                Help.SecondaryColor1 = Color.Black;
                Help.SecondaryColor2 = Color.Black;
                Help.PrimaryColorGradient = false;
                Help.SecondaryColorGradient = false;
                Help.LoadingCircleBack1 = Color.White;
                Help.LoadingCircleBack2 = Color.White;
                Help.LoadingCircleHot1 = Color.Black;
                Help.LoadingCircleHot2 = Color.Black;
                Help.LoadingCircleBackGradient = false;
                Help.LoadingCircleHotGradient = false;
                Help.Shadow_Enabled = false;
                Help.Shadow_Color = Color.Black;
                Help.Shadow_Blur = 5;
                Help.Shadow_Opacity = 0.3f;
                Help.Shadow_OffsetX = 2;
                Help.Shadow_OffsetY = 2;
            }

            {
                ref Theme.Structures.Cursor AppLoading = ref TM.Cursor_AppLoading;
                AppLoading.ArrowStyle = Paths.ArrowStyle.Classic;
                AppLoading.CircleStyle = Paths.CircleStyle.Classic;
                AppLoading.PrimaryColor1 = Color.White;
                AppLoading.PrimaryColor2 = Color.White;
                AppLoading.SecondaryColor1 = Color.Black;
                AppLoading.SecondaryColor2 = Color.Black;
                AppLoading.LoadingCircleBack1 = Color.White;
                AppLoading.LoadingCircleBack2 = Color.White;
                AppLoading.LoadingCircleHot1 = Color.Black;
                AppLoading.LoadingCircleHot2 = Color.Black;
                AppLoading.LoadingCircleBackGradient = false;
                AppLoading.LoadingCircleHotGradient = false;
                AppLoading.PrimaryColorGradient = false;
                AppLoading.SecondaryColorGradient = false;
                AppLoading.Shadow_Enabled = false;
                AppLoading.Shadow_Color = Color.Black;
                AppLoading.Shadow_Blur = 5;
                AppLoading.Shadow_Opacity = 0.3f;
                AppLoading.Shadow_OffsetX = 2;
                AppLoading.Shadow_OffsetY = 2;
            }

            {
                ref Theme.Structures.Cursor Busy = ref TM.Cursor_Busy;
                Busy.ArrowStyle = Paths.ArrowStyle.Classic;
                Busy.CircleStyle = Paths.CircleStyle.Classic;
                Busy.PrimaryColor1 = Color.White;
                Busy.PrimaryColor2 = Color.White;
                Busy.SecondaryColor1 = Color.Black;
                Busy.SecondaryColor2 = Color.Black;
                Busy.LoadingCircleBack1 = Color.White;
                Busy.LoadingCircleBack2 = Color.White;
                Busy.LoadingCircleHot1 = Color.Black;
                Busy.LoadingCircleHot2 = Color.Black;
                Busy.LoadingCircleBackGradient = false;
                Busy.LoadingCircleHotGradient = false;
                Busy.PrimaryColorGradient = false;
                Busy.SecondaryColorGradient = false;
                Busy.Shadow_Enabled = false;
                Busy.Shadow_Color = Color.Black;
                Busy.Shadow_Blur = 5;
                Busy.Shadow_Opacity = 0.3f;
                Busy.Shadow_OffsetX = 2;
                Busy.Shadow_OffsetY = 2;
            }

            {
                ref Theme.Structures.Cursor Up = ref TM.Cursor_Up;
                Up.ArrowStyle = Paths.ArrowStyle.Classic;
                Up.CircleStyle = Paths.CircleStyle.Classic;
                Up.PrimaryColor1 = Color.White;
                Up.PrimaryColor2 = Color.White;
                Up.SecondaryColor1 = Color.Black;
                Up.SecondaryColor2 = Color.Black;
                Up.PrimaryColorGradient = false;
                Up.SecondaryColorGradient = false;
                Up.LoadingCircleBack1 = Color.White;
                Up.LoadingCircleBack2 = Color.White;
                Up.LoadingCircleHot1 = Color.Black;
                Up.LoadingCircleHot2 = Color.Black;
                Up.LoadingCircleBackGradient = false;
                Up.LoadingCircleHotGradient = false;
                Up.Shadow_Enabled = false;
                Up.Shadow_Color = Color.Black;
                Up.Shadow_Blur = 5;
                Up.Shadow_Opacity = 0.3f;
                Up.Shadow_OffsetX = 2;
                Up.Shadow_OffsetY = 2;
            }

            {
                ref Theme.Structures.Cursor NS = ref TM.Cursor_NS;
                NS.ArrowStyle = Paths.ArrowStyle.Classic;
                NS.CircleStyle = Paths.CircleStyle.Classic;
                NS.PrimaryColor1 = Color.White;
                NS.PrimaryColor2 = Color.White;
                NS.SecondaryColor1 = Color.Black;
                NS.SecondaryColor2 = Color.Black;
                NS.PrimaryColorGradient = false;
                NS.SecondaryColorGradient = false;
                NS.LoadingCircleBack1 = Color.White;
                NS.LoadingCircleBack2 = Color.White;
                NS.LoadingCircleHot1 = Color.Black;
                NS.LoadingCircleHot2 = Color.Black;
                NS.LoadingCircleBackGradient = false;
                NS.LoadingCircleHotGradient = false;
                NS.Shadow_Enabled = false;
                NS.Shadow_Color = Color.Black;
                NS.Shadow_Blur = 5;
                NS.Shadow_Opacity = 0.3f;
                NS.Shadow_OffsetX = 2;
                NS.Shadow_OffsetY = 2;
            }

            {
                ref Theme.Structures.Cursor EW = ref TM.Cursor_EW;
                EW.ArrowStyle = Paths.ArrowStyle.Classic;
                EW.CircleStyle = Paths.CircleStyle.Classic;
                EW.PrimaryColor1 = Color.White;
                EW.PrimaryColor2 = Color.White;
                EW.SecondaryColor1 = Color.Black;
                EW.SecondaryColor2 = Color.Black;
                EW.PrimaryColorGradient = false;
                EW.SecondaryColorGradient = false;
                EW.LoadingCircleBack1 = Color.White;
                EW.LoadingCircleBack2 = Color.White;
                EW.LoadingCircleHot1 = Color.Black;
                EW.LoadingCircleHot2 = Color.Black;
                EW.LoadingCircleBackGradient = false;
                EW.LoadingCircleHotGradient = false;
                EW.Shadow_Enabled = false;
                EW.Shadow_Color = Color.Black;
                EW.Shadow_Blur = 5;
                EW.Shadow_Opacity = 0.3f;
                EW.Shadow_OffsetX = 2;
                EW.Shadow_OffsetY = 2;
            }

            {
                ref Theme.Structures.Cursor NESW = ref TM.Cursor_NESW;
                NESW.ArrowStyle = Paths.ArrowStyle.Classic;
                NESW.CircleStyle = Paths.CircleStyle.Classic;
                NESW.PrimaryColor1 = Color.White;
                NESW.PrimaryColor2 = Color.White;
                NESW.SecondaryColor1 = Color.Black;
                NESW.SecondaryColor2 = Color.Black;
                NESW.PrimaryColorGradient = false;
                NESW.SecondaryColorGradient = false;
                NESW.LoadingCircleBack1 = Color.White;
                NESW.LoadingCircleBack2 = Color.White;
                NESW.LoadingCircleHot1 = Color.Black;
                NESW.LoadingCircleHot2 = Color.Black;
                NESW.LoadingCircleBackGradient = false;
                NESW.LoadingCircleHotGradient = false;
                NESW.Shadow_Enabled = false;
                NESW.Shadow_Color = Color.Black;
                NESW.Shadow_Blur = 5;
                NESW.Shadow_Opacity = 0.3f;
                NESW.Shadow_OffsetX = 2;
                NESW.Shadow_OffsetY = 2;
            }

            {
                ref Theme.Structures.Cursor NWSE = ref TM.Cursor_NWSE;
                NWSE.ArrowStyle = Paths.ArrowStyle.Classic;
                NWSE.CircleStyle = Paths.CircleStyle.Classic;
                NWSE.PrimaryColor1 = Color.White;
                NWSE.PrimaryColor2 = Color.White;
                NWSE.SecondaryColor1 = Color.Black;
                NWSE.SecondaryColor2 = Color.Black;
                NWSE.PrimaryColorGradient = false;
                NWSE.SecondaryColorGradient = false;
                NWSE.LoadingCircleBack1 = Color.White;
                NWSE.LoadingCircleBack2 = Color.White;
                NWSE.LoadingCircleHot1 = Color.Black;
                NWSE.LoadingCircleHot2 = Color.Black;
                NWSE.LoadingCircleBackGradient = false;
                NWSE.LoadingCircleHotGradient = false;
                NWSE.Shadow_Enabled = false;
                NWSE.Shadow_Color = Color.Black;
                NWSE.Shadow_Blur = 5;
                NWSE.Shadow_Opacity = 0.3f;
                NWSE.Shadow_OffsetX = 2;
                NWSE.Shadow_OffsetY = 2;
            }

            {
                ref Theme.Structures.Cursor Move = ref TM.Cursor_Move;
                Move.ArrowStyle = Paths.ArrowStyle.Classic;
                Move.CircleStyle = Paths.CircleStyle.Classic;
                Move.PrimaryColor1 = Color.White;
                Move.PrimaryColor2 = Color.White;
                Move.SecondaryColor1 = Color.Black;
                Move.SecondaryColor2 = Color.Black;
                Move.PrimaryColorGradient = false;
                Move.SecondaryColorGradient = false;
                Move.LoadingCircleBack1 = Color.White;
                Move.LoadingCircleBack2 = Color.White;
                Move.LoadingCircleHot1 = Color.Black;
                Move.LoadingCircleHot2 = Color.Black;
                Move.LoadingCircleBackGradient = false;
                Move.LoadingCircleHotGradient = false;
                Move.Shadow_Enabled = false;
                Move.Shadow_Color = Color.Black;
                Move.Shadow_Blur = 5;
                Move.Shadow_Opacity = 0.3f;
                Move.Shadow_OffsetX = 2;
                Move.Shadow_OffsetY = 2;
            }

            {
                ref Theme.Structures.Cursor None = ref TM.Cursor_None;
                None.ArrowStyle = Paths.ArrowStyle.Classic;
                None.CircleStyle = Paths.CircleStyle.Classic;
                None.PrimaryColor1 = Color.Transparent;
                None.PrimaryColor2 = Color.Transparent;
                None.SecondaryColor1 = Color.Black;
                None.SecondaryColor2 = Color.Black;
                None.PrimaryColorGradient = false;
                None.SecondaryColorGradient = false;
                None.LoadingCircleBack1 = Color.White;
                None.LoadingCircleBack2 = Color.White;
                None.LoadingCircleHot1 = Color.Black;
                None.LoadingCircleHot2 = Color.Black;
                None.LoadingCircleBackGradient = false;
                None.LoadingCircleHotGradient = false;
                None.Shadow_Enabled = false;
                None.Shadow_Color = Color.Black;
                None.Shadow_Blur = 5;
                None.Shadow_Opacity = 0.3f;
                None.Shadow_OffsetX = 2;
                None.Shadow_OffsetY = 2;
            }

            {
                ref Theme.Structures.Cursor Pen = ref TM.Cursor_Pen;
                Pen.ArrowStyle = Paths.ArrowStyle.Classic;
                Pen.CircleStyle = Paths.CircleStyle.Classic;
                Pen.PrimaryColor1 = Color.White;
                Pen.PrimaryColor2 = Color.White;
                Pen.SecondaryColor1 = Color.Black;
                Pen.SecondaryColor2 = Color.Black;
                Pen.PrimaryColorGradient = false;
                Pen.SecondaryColorGradient = false;
                Pen.LoadingCircleBack1 = Color.White;
                Pen.LoadingCircleBack2 = Color.White;
                Pen.LoadingCircleHot1 = Color.Black;
                Pen.LoadingCircleHot2 = Color.Black;
                Pen.LoadingCircleBackGradient = false;
                Pen.LoadingCircleHotGradient = false;
                Pen.Shadow_Enabled = false;
                Pen.Shadow_Color = Color.Black;
                Pen.Shadow_Blur = 5;
                Pen.Shadow_Opacity = 0.3f;
                Pen.Shadow_OffsetX = 2;
                Pen.Shadow_OffsetY = 2;
            }

            {
                ref Theme.Structures.Cursor IBeam = ref TM.Cursor_IBeam;
                IBeam.ArrowStyle = Paths.ArrowStyle.Classic;
                IBeam.CircleStyle = Paths.CircleStyle.Classic;
                IBeam.PrimaryColor1 = Color.White;
                IBeam.PrimaryColor2 = Color.White;
                IBeam.SecondaryColor1 = Color.Black;
                IBeam.SecondaryColor2 = Color.Black;
                IBeam.PrimaryColorGradient = false;
                IBeam.SecondaryColorGradient = false;
                IBeam.LoadingCircleBack1 = Color.White;
                IBeam.LoadingCircleBack2 = Color.White;
                IBeam.LoadingCircleHot1 = Color.Black;
                IBeam.LoadingCircleHot2 = Color.Black;
                IBeam.LoadingCircleBackGradient = false;
                IBeam.LoadingCircleHotGradient = false;
                IBeam.Shadow_Enabled = false;
                IBeam.Shadow_Color = Color.Black;
                IBeam.Shadow_Blur = 5;
                IBeam.Shadow_Opacity = 0.3f;
                IBeam.Shadow_OffsetX = 2;
                IBeam.Shadow_OffsetY = 2;
            }

            {
                ref Theme.Structures.Cursor Cross = ref TM.Cursor_Cross;
                Cross.ArrowStyle = Paths.ArrowStyle.Classic;
                Cross.CircleStyle = Paths.CircleStyle.Classic;
                Cross.PrimaryColor1 = Color.White;
                Cross.PrimaryColor2 = Color.White;
                Cross.SecondaryColor1 = Color.Black;
                Cross.SecondaryColor2 = Color.Black;
                Cross.PrimaryColorGradient = false;
                Cross.SecondaryColorGradient = false;
                Cross.LoadingCircleBack1 = Color.White;
                Cross.LoadingCircleBack2 = Color.White;
                Cross.LoadingCircleHot1 = Color.Black;
                Cross.LoadingCircleHot2 = Color.Black;
                Cross.LoadingCircleBackGradient = false;
                Cross.LoadingCircleHotGradient = false;
                Cross.Shadow_Enabled = false;
                Cross.Shadow_Color = Color.Black;
                Cross.Shadow_Blur = 5;
                Cross.Shadow_Opacity = 0.3f;
                Cross.Shadow_OffsetX = 2;
                Cross.Shadow_OffsetY = 2;
            }

            {
                ref Theme.Structures.Cursor Link = ref TM.Cursor_Link;
                Link.ArrowStyle = Paths.ArrowStyle.Classic;
                Link.CircleStyle = Paths.CircleStyle.Classic;
                Link.PrimaryColor1 = Color.White;
                Link.PrimaryColor2 = Color.White;
                Link.SecondaryColor1 = Color.Black;
                Link.SecondaryColor2 = Color.Black;
                Link.PrimaryColorGradient = false;
                Link.SecondaryColorGradient = false;
                Link.LoadingCircleBack1 = Color.White;
                Link.LoadingCircleBack2 = Color.White;
                Link.LoadingCircleHot1 = Color.Black;
                Link.LoadingCircleHot2 = Color.Black;
                Link.LoadingCircleBackGradient = false;
                Link.LoadingCircleHotGradient = false;
                Link.Shadow_Enabled = false;
                Link.Shadow_Color = Color.Black;
                Link.Shadow_Blur = 5;
                Link.Shadow_Opacity = 0.3f;
                Link.Shadow_OffsetX = 2;
                Link.Shadow_OffsetY = 2;
            }

            {
                ref Theme.Structures.Cursor Pin = ref TM.Cursor_Pin;
                Pin.ArrowStyle = Paths.ArrowStyle.Classic;
                Pin.CircleStyle = Paths.CircleStyle.Classic;
                Pin.PrimaryColor1 = Color.White;
                Pin.PrimaryColor2 = Color.White;
                Pin.SecondaryColor1 = Color.Black;
                Pin.SecondaryColor2 = Color.Black;
                Pin.PrimaryColorGradient = false;
                Pin.SecondaryColorGradient = false;
                Pin.LoadingCircleBack1 = Color.White;
                Pin.LoadingCircleBack2 = Color.White;
                Pin.LoadingCircleHot1 = Color.Black;
                Pin.LoadingCircleHot2 = Color.Black;
                Pin.LoadingCircleBackGradient = false;
                Pin.LoadingCircleHotGradient = false;
                Pin.Shadow_Enabled = false;
                Pin.Shadow_Color = Color.Black;
                Pin.Shadow_Blur = 5;
                Pin.Shadow_Opacity = 0.3f;
                Pin.Shadow_OffsetX = 2;
                Pin.Shadow_OffsetY = 2;
            }

            {
                ref Theme.Structures.Cursor Person = ref TM.Cursor_Person;
                Person.ArrowStyle = Paths.ArrowStyle.Classic;
                Person.CircleStyle = Paths.CircleStyle.Classic;
                Person.PrimaryColor1 = Color.White;
                Person.PrimaryColor2 = Color.White;
                Person.SecondaryColor1 = Color.Black;
                Person.SecondaryColor2 = Color.Black;
                Person.PrimaryColorGradient = false;
                Person.SecondaryColorGradient = false;
                Person.LoadingCircleBack1 = Color.White;
                Person.LoadingCircleBack2 = Color.White;
                Person.LoadingCircleHot1 = Color.Black;
                Person.LoadingCircleHot2 = Color.Black;
                Person.LoadingCircleBackGradient = false;
                Person.LoadingCircleHotGradient = false;
                Person.Shadow_Enabled = false;
                Person.Shadow_Color = Color.Black;
                Person.Shadow_Blur = 5;
                Person.Shadow_Opacity = 0.3f;
                Person.Shadow_OffsetX = 2;
                Person.Shadow_OffsetY = 2;
            }

            {
                ref Theme.Structures.WinEffects WinEffects = ref TM.WindowsEffects;
                WinEffects.ShakeToMinimize = false;
                WinEffects.BalloonNotifications = true;
                WinEffects.PaintDesktopVersion = false;
                WinEffects.ShowSecondsInSystemClock = false;
                WinEffects.Win11ClassicContextMenu = false;
                WinEffects.SysListView32 = true;
            }

            TM.Terminal = new WinTerminal("", WinTerminal.Mode.Empty);
            TM.TerminalPreview = new WinTerminal("", WinTerminal.Mode.Empty);
            TM.Cursor_Shadow = true;

            {
                ref Theme.Structures.ScreenSaver ScreenSaver = ref TM.ScreenSaver;
                ScreenSaver.Enabled = true;
                ScreenSaver.IsSecure = false;
                ScreenSaver.TimeOut = 60;
                ScreenSaver.File = Program.PATH_System32 + @"\logon.scr";
            }

            {
                ref Theme.Structures.Sounds Sounds = ref TM.Sounds;
                Sounds.Snd_Imageres_SystemStart = "";
                Sounds.Snd_Win_Default = Program.PATH_Windows + @"\media\Windows XP Ding.wav";
                Sounds.Snd_Win_AppGPFault = "";
                Sounds.Snd_Win_CCSelect = "";
                Sounds.Snd_Win_ChangeTheme = "";
                Sounds.Snd_Win_Close = "";
                Sounds.Snd_Win_CriticalBatteryAlarm = Program.PATH_Windows + @"\media\Windows XP Battery Critical.wav";
                Sounds.Snd_Win_DeviceConnect = Program.PATH_Windows + @"\media\Windows XP Hardware Insert.wav";
                Sounds.Snd_Win_DeviceDisconnect = Program.PATH_Windows + @"\media\Windows XP Hardware Remove.wav";
                Sounds.Snd_Win_DeviceFail = Program.PATH_Windows + @"\media\Windows XP Hardware Fail.wav";
                Sounds.Snd_Win_FaxBeep = "";
                Sounds.Snd_Win_LowBatteryAlarm = Program.PATH_Windows + @"\media\Windows XP Battery Low.wav";
                Sounds.Snd_Win_MailBeep = Program.PATH_Windows + @"\media\Windows XP Notify.wav";
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
                Sounds.Snd_Win_SystemAsterisk = Program.PATH_Windows + @"\media\Windows XP Error.wav";
                Sounds.Snd_Win_SystemExclamation = Program.PATH_Windows + @"\media\Windows XP Exclamation.wav";
                Sounds.Snd_Win_SystemExit = Program.PATH_Windows + @"\media\Windows XP Shutdown.wav";
                Sounds.Snd_Win_SystemStart = Program.PATH_Windows + @"\media\Windows XP Startup.wav";
                Sounds.Snd_Win_SystemHand = Program.PATH_Windows + @"\media\Windows XP Critical Stop.wav";
                Sounds.Snd_Win_SystemNotification = Program.PATH_Windows + @"\media\Windows XP Balloon.wav";
                Sounds.Snd_Win_SystemQuestion = "";
                Sounds.Snd_Win_WindowsLogoff = Program.PATH_Windows + @"\media\Windows XP Logoff Sound.wav";
                Sounds.Snd_Win_WindowsLogon = Program.PATH_Windows + @"\media\Windows XP Logon Sound.wav";
                Sounds.Snd_Win_WindowsUAC = "";
                Sounds.Snd_Win_WindowsUnlock = "";
                Sounds.Snd_Explorer_ActivatingDocument = "";
                Sounds.Snd_Explorer_BlockedPopup = Program.PATH_Windows + @"\media\Windows Pop-up Blocked.wav";
                Sounds.Snd_Explorer_EmptyRecycleBin = Program.PATH_Windows + @"\media\Windows XP Recycle.wav";
                Sounds.Snd_Explorer_FeedDiscovered = Program.PATH_Windows + @"\media\Windows Feed Discovered.wav";
                Sounds.Snd_Explorer_MoveMenuItem = "";
                Sounds.Snd_Explorer_Navigating = Program.PATH_Windows + @"\media\Windows Navigation Start.wav";
                Sounds.Snd_Explorer_SecurityBand = Program.PATH_Windows + @"\media\Windows Information Bar.wav";
                Sounds.Snd_Explorer_SearchProviderDiscovered = "";
                Sounds.Snd_Explorer_FaxError = Program.PATH_Windows + @"\media\ding.wav";
                Sounds.Snd_Explorer_FaxLineRings = Program.PATH_Windows + @"\media\ringin.wav";
                Sounds.Snd_Explorer_FaxNew = Program.PATH_Windows + @"\media\notify.wav";
                Sounds.Snd_Explorer_FaxSent = Program.PATH_Windows + @"\media\tada.wav";
                Sounds.Snd_NetMeeting_PersonJoins = Program.PATH_ProgramFiles + @"\NetMeeting\Blip.wav";
                Sounds.Snd_NetMeeting_PersonLeaves = Program.PATH_ProgramFiles + @"\NetMeeting\Blip.wav";
                Sounds.Snd_NetMeeting_ReceiveCall = Program.PATH_Windows + @"\media\Windows XP RingIn.wav";
                Sounds.Snd_NetMeeting_ReceiveRequestToJoin = Program.PATH_Windows + @"\media\Windows XP RingIn.wav";
                Sounds.Snd_SpeechRec_DisNumbersSound = "";
                Sounds.Snd_SpeechRec_HubOffSound = "";
                Sounds.Snd_SpeechRec_HubOnSound = "";
                Sounds.Snd_SpeechRec_HubSleepSound = "";
                Sounds.Snd_SpeechRec_MisrecoSound = "";
                Sounds.Snd_SpeechRec_PanelSound = "";

                Sounds.Snd_Win_SystemExit_TaskMgmt = false;
                Sounds.Snd_Win_WindowsLogoff_TaskMgmt = false;
                Sounds.Snd_Win_WindowsLogon_TaskMgmt = false;
                Sounds.Snd_Win_WindowsUnlock_TaskMgmt = false;
            }

            {
                ref Theme.Structures.Wallpaper Wallpaper = ref TM.Wallpaper;
                Wallpaper.ImageFile = Program.PATH_Windows + @"\Web\Wallpaper\Bliss.bmp";
                Wallpaper.WallpaperStyle = Theme.Structures.Wallpaper.WallpaperStyles.Stretched;
            }

            return TM;
        }

        #region IDisposable Support
        private bool disposedValue; // To detect redundant calls

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                // TODO: set large fields to null.
            }
            disposedValue = true;
        }

        // TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        // Protected Overrides Sub Finalize()
        // ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        // Dispose(False)
        // MyBase.Finalize()
        // End Sub

        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}