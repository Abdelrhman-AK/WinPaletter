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

            if (My.Env.W11 | My.Env.W12)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.Windows11();
                }
            }

            else if (My.Env.W10)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.Windows10();
                }
            }

            else if (My.Env.W81)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.Windows81();
                }
            }

            else if (My.Env.W7)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.Windows7();
                }
            }

            else if (My.Env.WVista)
            {
                using (var X = new Theme.Default())
                {
                    _Def = X.WindowsVista();
                }
            }

            else if (My.Env.WXP)
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
                ref var temp = ref TM.Info;
                temp.ThemeName = "Windows 11 (Initial)";
                temp.Description = "Initial; Like first time after Windows Setup";
                temp.ThemeVersion = "1.0.0.0";
                temp.Author = "Microsoft";
                temp.AuthorSocialMediaLink = "www.microsoft.com";
                temp.AppVersion = My.Env.AppVersion;
            }

            {
                ref var temp1 = ref TM.CommandPrompt;
                temp1.ColorTable05 = Color.FromArgb(136, 23, 152);
                temp1.ColorTable06 = Color.FromArgb(193, 156, 0);
                temp1.PopupBackground = 15;
                temp1.PopupForeground = 5;
                temp1.ScreenColorsForeground = 7;
                temp1.ScreenColorsBackground = 0;
                temp1.FaceName = "Consolas";
                temp1.FontSize = 18 * 65536;
                temp1.FontRaster = false;
                temp1.W10_1909_ForceV2 = true;
            }

            {
                ref var temp2 = ref TM.PowerShellx86;
                temp2.ColorTable05 = Color.FromArgb(1, 36, 86);
                temp2.ColorTable06 = Color.FromArgb(238, 237, 240);
                temp2.PopupBackground = 15;
                temp2.PopupForeground = 3;
                temp2.ScreenColorsForeground = 6;
                temp2.ScreenColorsBackground = 5;
                temp2.FaceName = "Consolas";
                temp2.FontSize = 17 * 65536;
                temp2.FontRaster = false;
                temp2.W10_1909_ForceV2 = true;
            }

            {
                ref var temp3 = ref TM.PowerShellx64;
                temp3.ColorTable05 = Color.FromArgb(1, 36, 86);
                temp3.ColorTable06 = Color.FromArgb(238, 237, 240);
                temp3.PopupBackground = 15;
                temp3.PopupForeground = 3;
                temp3.ScreenColorsForeground = 6;
                temp3.ScreenColorsBackground = 5;
                temp3.FaceName = "Consolas";
                temp3.FontSize = 17 * 65536;
                temp3.FontRaster = false;
                temp3.W10_1909_ForceV2 = true;
            }

            {
                ref var temp4 = ref TM.MetricsFonts;
                temp4.BorderWidth = 1;
                temp4.CaptionHeight = 22;
                temp4.CaptionWidth = 22;
                temp4.IconSpacing = 75;
                temp4.IconVerticalSpacing = 75;
                temp4.MenuHeight = 19;
                temp4.MenuWidth = 19;
                temp4.PaddedBorderWidth = 4;
                temp4.ScrollHeight = 17;
                temp4.ScrollWidth = 17;
                temp4.SmCaptionHeight = 22;
                temp4.SmCaptionWidth = 22;
                temp4.DesktopIconSize = 48;
                temp4.ShellIconSize = 32;
            }

            {
                ref var temp5 = ref TM.WindowsEffects;
                temp5.ShakeToMinimize = false;
                temp5.BalloonNotifications = false;
                temp5.PaintDesktopVersion = false;
                temp5.ShowSecondsInSystemClock = false;
                temp5.Win11ClassicContextMenu = false;
                temp5.SysListView32 = false;
            }

            TM.Terminal = new WinTerminal("", WinTerminal.Mode.Empty);
            TM.TerminalPreview = new WinTerminal("", WinTerminal.Mode.Empty);
            TM.Cursor_Shadow = false;

            {
                ref var temp6 = ref TM.Sounds;
                temp6.Snd_Imageres_SystemStart = "Default";
                temp6.Snd_Win_Default = My.Env.PATH_Windows + @"\media\Windows Background.wav";
                temp6.Snd_Win_AppGPFault = "";
                temp6.Snd_Win_CCSelect = "";
                temp6.Snd_Win_ChangeTheme = "";
                temp6.Snd_Win_Close = "";
                temp6.Snd_Win_CriticalBatteryAlarm = My.Env.PATH_Windows + @"\media\Windows Foreground.wav";
                temp6.Snd_Win_DeviceConnect = My.Env.PATH_Windows + @"\media\Windows Hardware Insert.wav";
                temp6.Snd_Win_DeviceDisconnect = My.Env.PATH_Windows + @"\media\Windows Hardware Remove.wav";
                temp6.Snd_Win_DeviceFail = My.Env.PATH_Windows + @"\media\Windows Hardware Fail.wav";
                temp6.Snd_Win_FaxBeep = My.Env.PATH_Windows + @"\media\Windows Notify Email.wav";
                temp6.Snd_Win_LowBatteryAlarm = My.Env.PATH_Windows + @"\media\Windows Background.wav";
                temp6.Snd_Win_MailBeep = My.Env.PATH_Windows + @"\media\Windows Notify Email.wav";
                temp6.Snd_Win_Maximize = "";
                temp6.Snd_Win_MenuCommand = "";
                temp6.Snd_Win_MenuPopup = "";
                temp6.Snd_Win_MessageNudge = My.Env.PATH_Windows + @"\media\Windows Message Nudge.wav";
                temp6.Snd_Win_Minimize = "";
                temp6.Snd_Win_Notification_Default = My.Env.PATH_Windows + @"\media\Windows Notify System Generic.wav";
                temp6.Snd_Win_Notification_IM = My.Env.PATH_Windows + @"\media\Windows Notify Messaging.wav";
                temp6.Snd_Win_Notification_Looping_Alarm = My.Env.PATH_Windows + @"\media\Alarm01.wav";
                temp6.Snd_Win_Notification_Looping_Alarm2 = My.Env.PATH_Windows + @"\media\Alarm02.wav";
                temp6.Snd_Win_Notification_Looping_Alarm3 = My.Env.PATH_Windows + @"\media\Alarm03.wav";
                temp6.Snd_Win_Notification_Looping_Alarm4 = My.Env.PATH_Windows + @"\media\Alarm04.wav";
                temp6.Snd_Win_Notification_Looping_Alarm5 = My.Env.PATH_Windows + @"\media\Alarm05.wav";
                temp6.Snd_Win_Notification_Looping_Alarm6 = My.Env.PATH_Windows + @"\media\Alarm06.wav";
                temp6.Snd_Win_Notification_Looping_Alarm7 = My.Env.PATH_Windows + @"\media\Alarm07.wav";
                temp6.Snd_Win_Notification_Looping_Alarm8 = My.Env.PATH_Windows + @"\media\Alarm08.wav";
                temp6.Snd_Win_Notification_Looping_Alarm9 = My.Env.PATH_Windows + @"\media\Alarm09.wav";
                temp6.Snd_Win_Notification_Looping_Alarm10 = My.Env.PATH_Windows + @"\media\Alarm10.wav";
                temp6.Snd_Win_Notification_Looping_Call = My.Env.PATH_Windows + @"\media\Ring01.wav";
                temp6.Snd_Win_Notification_Looping_Call2 = My.Env.PATH_Windows + @"\media\Ring02.wav";
                temp6.Snd_Win_Notification_Looping_Call3 = My.Env.PATH_Windows + @"\media\Ring03.wav";
                temp6.Snd_Win_Notification_Looping_Call4 = My.Env.PATH_Windows + @"\media\Ring04.wav";
                temp6.Snd_Win_Notification_Looping_Call5 = My.Env.PATH_Windows + @"\media\Ring05.wav";
                temp6.Snd_Win_Notification_Looping_Call6 = My.Env.PATH_Windows + @"\media\Ring06.wav";
                temp6.Snd_Win_Notification_Looping_Call7 = My.Env.PATH_Windows + @"\media\Ring07.wav";
                temp6.Snd_Win_Notification_Looping_Call8 = My.Env.PATH_Windows + @"\media\Ring08.wav";
                temp6.Snd_Win_Notification_Looping_Call9 = My.Env.PATH_Windows + @"\media\Ring09.wav";
                temp6.Snd_Win_Notification_Looping_Call10 = My.Env.PATH_Windows + @"\media\Ring10.wav";
                temp6.Snd_Win_Notification_Mail = My.Env.PATH_Windows + @"\media\Windows Notify Email.wav";
                temp6.Snd_Win_Notification_Proximity = My.Env.PATH_Windows + @"\media\Windows Proximity Notification.wav";
                temp6.Snd_Win_Notification_Reminder = My.Env.PATH_Windows + @"\media\Windows Notify Calendar.wav";
                temp6.Snd_Win_Notification_SMS = My.Env.PATH_Windows + @"\media\Windows Notify Messaging.wav";
                temp6.Snd_Win_Open = "";
                temp6.Snd_Win_PrintComplete = "";
                temp6.Snd_Win_ProximityConnection = My.Env.PATH_Windows + @"\media\Windows Proximity Connection.wav";
                temp6.Snd_Win_RestoreDown = "";
                temp6.Snd_Win_RestoreUp = "";
                temp6.Snd_Win_ShowBand = "";
                temp6.Snd_Win_SystemAsterisk = My.Env.PATH_Windows + @"\media\Windows Background.wav";
                temp6.Snd_Win_SystemExclamation = My.Env.PATH_Windows + @"\media\Windows Background.wav";
                temp6.Snd_Win_SystemExit = "";
                temp6.Snd_Win_SystemStart = "";
                temp6.Snd_Win_SystemHand = My.Env.PATH_Windows + @"\media\Windows Foreground.wav";
                temp6.Snd_Win_SystemNotification = My.Env.PATH_Windows + @"\media\Windows Background.wav";
                temp6.Snd_Win_SystemQuestion = "";
                temp6.Snd_Win_WindowsLogoff = "";
                temp6.Snd_Win_WindowsLogon = "";
                temp6.Snd_Win_WindowsUAC = My.Env.PATH_Windows + @"\media\Windows User Account Control.wav";
                temp6.Snd_Win_WindowsUnlock = My.Env.PATH_Windows + @"\media\Windows Unlock.wav";
                temp6.Snd_Explorer_ActivatingDocument = "";
                temp6.Snd_Explorer_BlockedPopup = "";
                temp6.Snd_Explorer_EmptyRecycleBin = "";
                temp6.Snd_Explorer_FeedDiscovered = "";
                temp6.Snd_Explorer_MoveMenuItem = "";
                temp6.Snd_Explorer_Navigating = "";
                temp6.Snd_Explorer_SecurityBand = "";
                temp6.Snd_Explorer_SearchProviderDiscovered = "";
                temp6.Snd_Explorer_FaxError = "";
                temp6.Snd_Explorer_FaxLineRings = "";
                temp6.Snd_Explorer_FaxNew = "";
                temp6.Snd_Explorer_FaxSent = "";
                temp6.Snd_NetMeeting_PersonJoins = "";
                temp6.Snd_NetMeeting_PersonLeaves = "";
                temp6.Snd_NetMeeting_ReceiveCall = "";
                temp6.Snd_NetMeeting_ReceiveRequestToJoin = "";
                temp6.Snd_SpeechRec_DisNumbersSound = My.Env.PATH_Windows + @"\media\Speech Disambiguation.wav";
                temp6.Snd_SpeechRec_HubOffSound = My.Env.PATH_Windows + @"\media\Speech Off.wav";
                temp6.Snd_SpeechRec_HubOnSound = My.Env.PATH_Windows + @"\media\Speech On.wav";
                temp6.Snd_SpeechRec_HubSleepSound = My.Env.PATH_Windows + @"\media\Speech Sleep.wav";
                temp6.Snd_SpeechRec_MisrecoSound = My.Env.PATH_Windows + @"\media\Speech Misrecognition.wav";
                temp6.Snd_SpeechRec_PanelSound = My.Env.PATH_Windows + @"\media\Speech Disambiguation.wav";

                temp6.Snd_Win_SystemExit_TaskMgmt = true;
                temp6.Snd_Win_WindowsLogoff_TaskMgmt = true;
                temp6.Snd_Win_WindowsLogon_TaskMgmt = true;
                temp6.Snd_Win_WindowsUnlock_TaskMgmt = true;
            }

            return TM;
        }

        public Theme.Manager Windows10()
        {
            var TM = new Theme.Manager(WinPaletter.Theme.Manager.Source.Empty);

            {
                ref var temp = ref TM.Info;
                temp.ThemeName = "Windows 10 (Initial)";
                temp.Description = "Initial; Like first time after Windows Setup";
                temp.ThemeVersion = "1.0.0.0";
                temp.Author = "Microsoft";
                temp.AuthorSocialMediaLink = "www.microsoft.com";
                temp.AppVersion = My.Env.AppVersion;
            }

            {
                ref var temp1 = ref TM.CommandPrompt;
                temp1.ColorTable05 = Color.FromArgb(136, 23, 152);
                temp1.ColorTable06 = Color.FromArgb(193, 156, 0);
                temp1.PopupBackground = 15;
                temp1.PopupForeground = 5;
                temp1.ScreenColorsForeground = 7;
                temp1.ScreenColorsBackground = 0;
                temp1.FaceName = "Consolas";
                temp1.FontSize = 18 * 65536;
                temp1.FontRaster = false;
                temp1.W10_1909_ForceV2 = My.Env.W10_1909;
            }

            {
                ref var temp2 = ref TM.PowerShellx86;
                temp2.ColorTable05 = Color.FromArgb(1, 36, 86);
                temp2.ColorTable06 = Color.FromArgb(238, 237, 240);
                temp2.PopupBackground = 15;
                temp2.PopupForeground = 3;
                temp2.ScreenColorsForeground = 6;
                temp2.ScreenColorsBackground = 5;
                temp2.FaceName = "Consolas";
                temp2.FontSize = 17 * 65536;
                temp2.FontRaster = false;
                temp2.W10_1909_ForceV2 = My.Env.W10_1909;
            }

            {
                ref var temp3 = ref TM.PowerShellx64;
                temp3.ColorTable05 = Color.FromArgb(1, 36, 86);
                temp3.ColorTable06 = Color.FromArgb(238, 237, 240);
                temp3.PopupBackground = 15;
                temp3.PopupForeground = 3;
                temp3.ScreenColorsForeground = 6;
                temp3.ScreenColorsBackground = 5;
                temp3.FaceName = "Consolas";
                temp3.FontSize = 17 * 65536;
                temp3.FontRaster = false;
                temp3.W10_1909_ForceV2 = My.Env.W10_1909;
            }

            {
                ref var temp4 = ref TM.MetricsFonts;
                temp4.BorderWidth = 1;
                temp4.CaptionHeight = 22;
                temp4.CaptionWidth = 22;
                temp4.IconSpacing = 75;
                temp4.IconVerticalSpacing = 75;
                temp4.MenuHeight = 19;
                temp4.MenuWidth = 19;
                temp4.PaddedBorderWidth = 4;
                temp4.ScrollHeight = 17;
                temp4.ScrollWidth = 17;
                temp4.SmCaptionHeight = 22;
                temp4.SmCaptionWidth = 22;
                temp4.DesktopIconSize = 48;
                temp4.ShellIconSize = 32;
            }

            {
                ref var temp5 = ref TM.WindowsEffects;
                temp5.ShakeToMinimize = true;
                temp5.BalloonNotifications = false;
                temp5.PaintDesktopVersion = false;
                temp5.ShowSecondsInSystemClock = false;
                temp5.Win11ClassicContextMenu = false;
                temp5.SysListView32 = false;
            }

            TM.Terminal = new WinTerminal("", WinTerminal.Mode.Empty);
            TM.TerminalPreview = new WinTerminal("", WinTerminal.Mode.Empty);

            TM.Cursor_Shadow = false;

            {
                ref var temp6 = ref TM.Sounds;
                temp6.Snd_Imageres_SystemStart = "";
                temp6.Snd_Win_Default = My.Env.PATH_Windows + @"\media\Windows Background.wav";
                temp6.Snd_Win_AppGPFault = "";
                temp6.Snd_Win_CCSelect = "";
                temp6.Snd_Win_ChangeTheme = "";
                temp6.Snd_Win_Close = "";
                temp6.Snd_Win_CriticalBatteryAlarm = My.Env.PATH_Windows + @"\media\Windows Foreground.wav";
                temp6.Snd_Win_DeviceConnect = My.Env.PATH_Windows + @"\media\Windows Hardware Insert.wav";
                temp6.Snd_Win_DeviceDisconnect = My.Env.PATH_Windows + @"\media\Windows Hardware Remove.wav";
                temp6.Snd_Win_DeviceFail = My.Env.PATH_Windows + @"\media\Windows Hardware Fail.wav";
                temp6.Snd_Win_FaxBeep = My.Env.PATH_Windows + @"\media\Windows Notify Email.wav";
                temp6.Snd_Win_LowBatteryAlarm = My.Env.PATH_Windows + @"\media\Windows Background.wav";
                temp6.Snd_Win_MailBeep = My.Env.PATH_Windows + @"\media\Windows Notify Email.wav";
                temp6.Snd_Win_Maximize = "";
                temp6.Snd_Win_MenuCommand = "";
                temp6.Snd_Win_MenuPopup = "";
                temp6.Snd_Win_MessageNudge = My.Env.PATH_Windows + @"\media\Windows Message Nudge.wav";
                temp6.Snd_Win_Minimize = "";
                temp6.Snd_Win_Notification_Default = My.Env.PATH_Windows + @"\media\Windows Notify System Generic.wav";
                temp6.Snd_Win_Notification_IM = My.Env.PATH_Windows + @"\media\Windows Notify Messaging.wav";
                temp6.Snd_Win_Notification_Looping_Alarm = My.Env.PATH_Windows + @"\media\Alarm01.wav";
                temp6.Snd_Win_Notification_Looping_Alarm2 = My.Env.PATH_Windows + @"\media\Alarm02.wav";
                temp6.Snd_Win_Notification_Looping_Alarm3 = My.Env.PATH_Windows + @"\media\Alarm03.wav";
                temp6.Snd_Win_Notification_Looping_Alarm4 = My.Env.PATH_Windows + @"\media\Alarm04.wav";
                temp6.Snd_Win_Notification_Looping_Alarm5 = My.Env.PATH_Windows + @"\media\Alarm05.wav";
                temp6.Snd_Win_Notification_Looping_Alarm6 = My.Env.PATH_Windows + @"\media\Alarm06.wav";
                temp6.Snd_Win_Notification_Looping_Alarm7 = My.Env.PATH_Windows + @"\media\Alarm07.wav";
                temp6.Snd_Win_Notification_Looping_Alarm8 = My.Env.PATH_Windows + @"\media\Alarm08.wav";
                temp6.Snd_Win_Notification_Looping_Alarm9 = My.Env.PATH_Windows + @"\media\Alarm09.wav";
                temp6.Snd_Win_Notification_Looping_Alarm10 = My.Env.PATH_Windows + @"\media\Alarm10.wav";
                temp6.Snd_Win_Notification_Looping_Call = My.Env.PATH_Windows + @"\media\Ring01.wav";
                temp6.Snd_Win_Notification_Looping_Call2 = My.Env.PATH_Windows + @"\media\Ring02.wav";
                temp6.Snd_Win_Notification_Looping_Call3 = My.Env.PATH_Windows + @"\media\Ring03.wav";
                temp6.Snd_Win_Notification_Looping_Call4 = My.Env.PATH_Windows + @"\media\Ring04.wav";
                temp6.Snd_Win_Notification_Looping_Call5 = My.Env.PATH_Windows + @"\media\Ring05.wav";
                temp6.Snd_Win_Notification_Looping_Call6 = My.Env.PATH_Windows + @"\media\Ring06.wav";
                temp6.Snd_Win_Notification_Looping_Call7 = My.Env.PATH_Windows + @"\media\Ring07.wav";
                temp6.Snd_Win_Notification_Looping_Call8 = My.Env.PATH_Windows + @"\media\Ring08.wav";
                temp6.Snd_Win_Notification_Looping_Call9 = My.Env.PATH_Windows + @"\media\Ring09.wav";
                temp6.Snd_Win_Notification_Looping_Call10 = My.Env.PATH_Windows + @"\media\Ring10.wav";
                temp6.Snd_Win_Notification_Mail = My.Env.PATH_Windows + @"\media\Windows Notify Email.wav";
                temp6.Snd_Win_Notification_Proximity = My.Env.PATH_Windows + @"\media\Windows Proximity Notification.wav";
                temp6.Snd_Win_Notification_Reminder = My.Env.PATH_Windows + @"\media\Windows Notify Calendar.wav";
                temp6.Snd_Win_Notification_SMS = My.Env.PATH_Windows + @"\media\Windows Notify Messaging.wav";
                temp6.Snd_Win_Open = "";
                temp6.Snd_Win_PrintComplete = "";
                temp6.Snd_Win_ProximityConnection = My.Env.PATH_Windows + @"\media\Windows Proximity Connection.wav";
                temp6.Snd_Win_RestoreDown = "";
                temp6.Snd_Win_RestoreUp = "";
                temp6.Snd_Win_ShowBand = "";
                temp6.Snd_Win_SystemAsterisk = My.Env.PATH_Windows + @"\media\Windows Background.wav";
                temp6.Snd_Win_SystemExclamation = My.Env.PATH_Windows + @"\media\Windows Background.wav";
                temp6.Snd_Win_SystemExit = "";
                temp6.Snd_Win_SystemStart = "";
                temp6.Snd_Win_SystemHand = My.Env.PATH_Windows + @"\media\Windows Foreground.wav";
                temp6.Snd_Win_SystemNotification = My.Env.PATH_Windows + @"\media\Windows Background.wav";
                temp6.Snd_Win_SystemQuestion = "";
                temp6.Snd_Win_WindowsLogoff = "";
                temp6.Snd_Win_WindowsLogon = "";
                temp6.Snd_Win_WindowsUAC = My.Env.PATH_Windows + @"\media\Windows User Account Control.wav";
                temp6.Snd_Win_WindowsUnlock = My.Env.PATH_Windows + @"\media\Windows Unlock.wav";
                temp6.Snd_Explorer_ActivatingDocument = "";
                temp6.Snd_Explorer_BlockedPopup = "";
                temp6.Snd_Explorer_EmptyRecycleBin = "";
                temp6.Snd_Explorer_FeedDiscovered = "";
                temp6.Snd_Explorer_MoveMenuItem = "";
                temp6.Snd_Explorer_Navigating = "";
                temp6.Snd_Explorer_SecurityBand = "";
                temp6.Snd_Explorer_SearchProviderDiscovered = "";
                temp6.Snd_Explorer_FaxError = "";
                temp6.Snd_Explorer_FaxLineRings = "";
                temp6.Snd_Explorer_FaxNew = "";
                temp6.Snd_Explorer_FaxSent = "";
                temp6.Snd_NetMeeting_PersonJoins = "";
                temp6.Snd_NetMeeting_PersonLeaves = "";
                temp6.Snd_NetMeeting_ReceiveCall = "";
                temp6.Snd_NetMeeting_ReceiveRequestToJoin = "";
                temp6.Snd_SpeechRec_DisNumbersSound = My.Env.PATH_Windows + @"\media\Speech Disambiguation.wav";
                temp6.Snd_SpeechRec_HubOffSound = My.Env.PATH_Windows + @"\media\Speech Off.wav";
                temp6.Snd_SpeechRec_HubOnSound = My.Env.PATH_Windows + @"\media\Speech On.wav";
                temp6.Snd_SpeechRec_HubSleepSound = My.Env.PATH_Windows + @"\media\Speech Sleep.wav";
                temp6.Snd_SpeechRec_MisrecoSound = My.Env.PATH_Windows + @"\media\Speech Misrecognition.wav";
                temp6.Snd_SpeechRec_PanelSound = My.Env.PATH_Windows + @"\media\Speech Disambiguation.wav";

                temp6.Snd_Win_SystemExit_TaskMgmt = true;
                temp6.Snd_Win_WindowsLogoff_TaskMgmt = true;
                temp6.Snd_Win_WindowsLogon_TaskMgmt = true;
                temp6.Snd_Win_WindowsUnlock_TaskMgmt = true;
            }

            return TM;
        }

        public Theme.Manager Windows81()
        {
            var TM = new Theme.Manager(WinPaletter.Theme.Manager.Source.Empty);

            {
                ref var temp = ref TM.Info;
                temp.ThemeName = "Windows 8.1 (Initial)";
                temp.Description = "Initial; Like first time after Windows Setup";
                temp.ThemeVersion = "1.0.0.0";
                temp.Author = "Microsoft";
                temp.AuthorSocialMediaLink = "www.microsoft.com";
                temp.AppVersion = My.Env.AppVersion;
            }

            {
                ref var temp1 = ref TM.Windows81;
                temp1.ColorizationColor = Color.FromArgb(246, 195, 74);
                temp1.ColorizationColorBalance = 78;
                temp1.PersonalColors_Background = Color.FromArgb(30, 0, 84);
                temp1.PersonalColors_Accent = Color.FromArgb(72, 29, 178);
                temp1.StartColor = Color.FromArgb(30, 0, 84);
                temp1.AccentColor = Color.FromArgb(72, 29, 178);
                temp1.Start = 0;
                temp1.Theme = Theme.Structures.Windows7.Themes.Aero;
                temp1.LogonUI = 0;
                temp1.NoLockScreen = false;
                temp1.LockScreenType = Theme.Structures.LogonUI7.Modes.Default;
                temp1.LockScreenSystemID = 0;
            }

            {
                ref var temp2 = ref TM.Windows7;
                temp2.ColorizationColor = Color.FromArgb(246, 195, 74);
                temp2.ColorizationAfterglow = Color.FromArgb(0, 0, 0);
                temp2.ColorizationColorBalance = 78;
                temp2.ColorizationAfterglowBalance = 31;
                temp2.ColorizationBlurBalance = 31;
                temp2.ColorizationGlassReflectionIntensity = 0;
                temp2.EnableAeroPeek = true;
                temp2.AlwaysHibernateThumbnails = false;
            }

            {
                ref var temp3 = ref TM.WindowsVista;
                temp3.ColorizationColor = Color.FromArgb(64, 158, 254);
            }

            {
                ref var temp4 = ref TM.CommandPrompt;
                temp4.ColorTable05 = Color.FromArgb(136, 23, 152);
                temp4.ColorTable06 = Color.FromArgb(193, 156, 0);
                temp4.PopupBackground = 15;
                temp4.PopupForeground = 5;
                temp4.ScreenColorsForeground = 7;
                temp4.ScreenColorsBackground = 0;
                temp4.FaceName = "Consolas";
                temp4.FontSize = 18 * 65536;
                temp4.FontRaster = true;
                temp4.W10_1909_ForceV2 = false;
            }

            {
                ref var temp5 = ref TM.PowerShellx86;
                temp5.ColorTable05 = Color.FromArgb(1, 36, 86);
                temp5.ColorTable06 = Color.FromArgb(238, 237, 240);
                temp5.PopupBackground = 15;
                temp5.PopupForeground = 3;
                temp5.ScreenColorsForeground = 6;
                temp5.ScreenColorsBackground = 5;
                temp5.FaceName = "Consolas";
                temp5.FontSize = 14 * 65536;
                temp5.FontRaster = true;
                temp5.W10_1909_ForceV2 = false;
            }

            {
                ref var temp6 = ref TM.PowerShellx64;
                temp6.ColorTable05 = Color.FromArgb(1, 36, 86);
                temp6.ColorTable06 = Color.FromArgb(238, 237, 240);
                temp6.PopupBackground = 15;
                temp6.PopupForeground = 3;
                temp6.ScreenColorsForeground = 6;
                temp6.ScreenColorsBackground = 5;
                temp6.FaceName = "Consolas";
                temp6.FontSize = 14 * 65536;
                temp6.FontRaster = true;
                temp6.W10_1909_ForceV2 = false;
            }

            {
                ref var temp7 = ref TM.MetricsFonts;
                temp7.BorderWidth = 1;
                temp7.CaptionHeight = 22;
                temp7.CaptionWidth = 22;
                temp7.IconSpacing = 75;
                temp7.IconVerticalSpacing = 75;
                temp7.MenuHeight = 19;
                temp7.MenuWidth = 19;
                temp7.PaddedBorderWidth = 4;
                temp7.ScrollHeight = 17;
                temp7.ScrollWidth = 17;
                temp7.SmCaptionHeight = 22;
                temp7.SmCaptionWidth = 22;
                temp7.DesktopIconSize = 48;
                temp7.ShellIconSize = 32;
                temp7.CaptionFont = new Font("Segoe UI", 11.25f, FontStyle.Regular);
                temp7.SmCaptionFont = new Font("Segoe UI", 11.25f, FontStyle.Regular);
            }

            {
                ref var temp8 = ref TM.WindowsEffects;
                temp8.ShakeToMinimize = true;
                temp8.BalloonNotifications = true;
                temp8.PaintDesktopVersion = false;
                temp8.ShowSecondsInSystemClock = false;
                temp8.Win11ClassicContextMenu = false;
                temp8.SysListView32 = false;
            }

            TM.Terminal = new WinTerminal("", WinTerminal.Mode.Empty);
            TM.TerminalPreview = new WinTerminal("", WinTerminal.Mode.Empty);
            TM.Cursor_Shadow = false;

            {
                ref var temp9 = ref TM.Sounds;
                temp9.Snd_Imageres_SystemStart = "";
                temp9.Snd_Win_Default = My.Env.PATH_Windows + @"\media\Windows Background.wav";
                temp9.Snd_Win_AppGPFault = "";
                temp9.Snd_Win_CCSelect = "";
                temp9.Snd_Win_ChangeTheme = "";
                temp9.Snd_Win_Close = "";
                temp9.Snd_Win_CriticalBatteryAlarm = My.Env.PATH_Windows + @"\media\Windows Foreground.wav";
                temp9.Snd_Win_DeviceConnect = My.Env.PATH_Windows + @"\media\Windows Hardware Insert.wav";
                temp9.Snd_Win_DeviceDisconnect = My.Env.PATH_Windows + @"\media\Windows Hardware Remove.wav";
                temp9.Snd_Win_DeviceFail = My.Env.PATH_Windows + @"\media\Windows Hardware Fail.wav";
                temp9.Snd_Win_FaxBeep = My.Env.PATH_Windows + @"\media\Windows Notify Email.wav";
                temp9.Snd_Win_LowBatteryAlarm = My.Env.PATH_Windows + @"\media\Windows Background.wav";
                temp9.Snd_Win_MailBeep = My.Env.PATH_Windows + @"\media\Windows Notify Email.wav";
                temp9.Snd_Win_Maximize = "";
                temp9.Snd_Win_MenuCommand = "";
                temp9.Snd_Win_MenuPopup = "";
                temp9.Snd_Win_MessageNudge = My.Env.PATH_Windows + @"\media\Windows Message Nudge.wav";
                temp9.Snd_Win_Minimize = "";
                temp9.Snd_Win_Notification_Default = My.Env.PATH_Windows + @"\media\Windows Notify System Generic.wav";
                temp9.Snd_Win_Notification_IM = My.Env.PATH_Windows + @"\media\Windows Notify Messaging.wav";
                temp9.Snd_Win_Notification_Looping_Alarm = My.Env.PATH_Windows + @"\media\Alarm01.wav";
                temp9.Snd_Win_Notification_Looping_Alarm2 = My.Env.PATH_Windows + @"\media\Alarm02.wav";
                temp9.Snd_Win_Notification_Looping_Alarm3 = My.Env.PATH_Windows + @"\media\Alarm03.wav";
                temp9.Snd_Win_Notification_Looping_Alarm4 = My.Env.PATH_Windows + @"\media\Alarm04.wav";
                temp9.Snd_Win_Notification_Looping_Alarm5 = My.Env.PATH_Windows + @"\media\Alarm05.wav";
                temp9.Snd_Win_Notification_Looping_Alarm6 = My.Env.PATH_Windows + @"\media\Alarm06.wav";
                temp9.Snd_Win_Notification_Looping_Alarm7 = My.Env.PATH_Windows + @"\media\Alarm07.wav";
                temp9.Snd_Win_Notification_Looping_Alarm8 = My.Env.PATH_Windows + @"\media\Alarm08.wav";
                temp9.Snd_Win_Notification_Looping_Alarm9 = My.Env.PATH_Windows + @"\media\Alarm09.wav";
                temp9.Snd_Win_Notification_Looping_Alarm10 = My.Env.PATH_Windows + @"\media\Alarm10.wav";
                temp9.Snd_Win_Notification_Looping_Call = My.Env.PATH_Windows + @"\media\Ring01.wav";
                temp9.Snd_Win_Notification_Looping_Call2 = My.Env.PATH_Windows + @"\media\Ring02.wav";
                temp9.Snd_Win_Notification_Looping_Call3 = My.Env.PATH_Windows + @"\media\Ring03.wav";
                temp9.Snd_Win_Notification_Looping_Call4 = My.Env.PATH_Windows + @"\media\Ring04.wav";
                temp9.Snd_Win_Notification_Looping_Call5 = My.Env.PATH_Windows + @"\media\Ring05.wav";
                temp9.Snd_Win_Notification_Looping_Call6 = My.Env.PATH_Windows + @"\media\Ring06.wav";
                temp9.Snd_Win_Notification_Looping_Call7 = My.Env.PATH_Windows + @"\media\Ring07.wav";
                temp9.Snd_Win_Notification_Looping_Call8 = My.Env.PATH_Windows + @"\media\Ring08.wav";
                temp9.Snd_Win_Notification_Looping_Call9 = My.Env.PATH_Windows + @"\media\Ring09.wav";
                temp9.Snd_Win_Notification_Looping_Call10 = My.Env.PATH_Windows + @"\media\Ring10.wav";
                temp9.Snd_Win_Notification_Mail = My.Env.PATH_Windows + @"\media\Windows Notify Email.wav";
                temp9.Snd_Win_Notification_Proximity = My.Env.PATH_Windows + @"\media\Windows Proximity Notification.wav";
                temp9.Snd_Win_Notification_Reminder = My.Env.PATH_Windows + @"\media\Windows Notify Calendar.wav";
                temp9.Snd_Win_Notification_SMS = My.Env.PATH_Windows + @"\media\Windows Notify Messaging.wav";
                temp9.Snd_Win_Open = "";
                temp9.Snd_Win_PrintComplete = "";
                temp9.Snd_Win_ProximityConnection = My.Env.PATH_Windows + @"\media\Windows Proximity Connection.wav";
                temp9.Snd_Win_RestoreDown = "";
                temp9.Snd_Win_RestoreUp = "";
                temp9.Snd_Win_ShowBand = "";
                temp9.Snd_Win_SystemAsterisk = My.Env.PATH_Windows + @"\media\Windows Background.wav";
                temp9.Snd_Win_SystemExclamation = My.Env.PATH_Windows + @"\media\Windows Background.wav";
                temp9.Snd_Win_SystemExit = "";
                temp9.Snd_Win_SystemStart = "";
                temp9.Snd_Win_SystemHand = My.Env.PATH_Windows + @"\media\Windows Foreground.wav";
                temp9.Snd_Win_SystemNotification = My.Env.PATH_Windows + @"\media\Windows Background.wav";
                temp9.Snd_Win_SystemQuestion = "";
                temp9.Snd_Win_WindowsLogoff = "";
                temp9.Snd_Win_WindowsLogon = "";
                temp9.Snd_Win_WindowsUAC = My.Env.PATH_Windows + @"\media\Windows User Account Control.wav";
                temp9.Snd_Win_WindowsUnlock = My.Env.PATH_Windows + @"\media\Windows Unlock.wav";
                temp9.Snd_Explorer_ActivatingDocument = "";
                temp9.Snd_Explorer_BlockedPopup = My.Env.PATH_Windows + @"\media\Windows Pop-up Blocked.wav";
                temp9.Snd_Explorer_EmptyRecycleBin = "";
                temp9.Snd_Explorer_FeedDiscovered = My.Env.PATH_Windows + @"\media\Windows Feed Discovered.wav";
                temp9.Snd_Explorer_MoveMenuItem = "";
                temp9.Snd_Explorer_Navigating = "";
                temp9.Snd_Explorer_SecurityBand = My.Env.PATH_Windows + @"\media\Windows Information Bar.wav";
                temp9.Snd_Explorer_SearchProviderDiscovered = "";
                temp9.Snd_Explorer_FaxError = "";
                temp9.Snd_Explorer_FaxLineRings = "";
                temp9.Snd_Explorer_FaxNew = "";
                temp9.Snd_Explorer_FaxSent = "";
                temp9.Snd_NetMeeting_PersonJoins = "";
                temp9.Snd_NetMeeting_PersonLeaves = "";
                temp9.Snd_NetMeeting_ReceiveCall = "";
                temp9.Snd_NetMeeting_ReceiveRequestToJoin = "";
                temp9.Snd_SpeechRec_DisNumbersSound = My.Env.PATH_Windows + @"\media\Speech Disambiguation.wav";
                temp9.Snd_SpeechRec_HubOffSound = My.Env.PATH_Windows + @"\media\Speech Off.wav";
                temp9.Snd_SpeechRec_HubOnSound = My.Env.PATH_Windows + @"\media\Speech On.wav";
                temp9.Snd_SpeechRec_HubSleepSound = My.Env.PATH_Windows + @"\media\Speech Sleep.wav";
                temp9.Snd_SpeechRec_MisrecoSound = My.Env.PATH_Windows + @"\media\Speech Misrecognition.wav";
                temp9.Snd_SpeechRec_PanelSound = My.Env.PATH_Windows + @"\media\Speech Disambiguation.wav";

                temp9.Snd_Win_SystemExit_TaskMgmt = true;
                temp9.Snd_Win_WindowsLogoff_TaskMgmt = true;
                temp9.Snd_Win_WindowsLogon_TaskMgmt = true;
                temp9.Snd_Win_WindowsUnlock_TaskMgmt = true;
            }

            return TM;
        }

        public Theme.Manager Windows7()
        {
            var TM = new Theme.Manager(WinPaletter.Theme.Manager.Source.Empty);

            {
                ref var temp = ref TM.Info;
                temp.ThemeName = "Windows 7 (Initial)";
                temp.Description = "Initial; Like first time after Windows Setup";
                temp.ThemeVersion = "1.0.0.0";
                temp.Author = "Microsoft";
                temp.AuthorSocialMediaLink = "www.microsoft.com";
                temp.AppVersion = My.Env.AppVersion;
            }

            {
                ref var temp1 = ref TM.Windows81;
                temp1.ColorizationColor = Color.FromArgb(246, 195, 74);
                temp1.ColorizationColorBalance = 78;
                temp1.PersonalColors_Background = Color.FromArgb(30, 0, 84);
                temp1.PersonalColors_Accent = Color.FromArgb(72, 29, 178);
                temp1.StartColor = Color.FromArgb(30, 0, 84);
                temp1.AccentColor = Color.FromArgb(72, 29, 178);
                temp1.Start = 0;
                temp1.Theme = Theme.Structures.Windows7.Themes.Aero;
                temp1.LogonUI = 0;
                temp1.NoLockScreen = false;
                temp1.LockScreenType = Theme.Structures.LogonUI7.Modes.Default;
                temp1.LockScreenSystemID = 0;
            }

            {
                ref var temp2 = ref TM.Windows7;
                temp2.ColorizationColor = Color.FromArgb(116, 184, 252);
                temp2.ColorizationAfterglow = Color.FromArgb(116, 184, 252);
                temp2.ColorizationColorBalance = 8;
                temp2.ColorizationAfterglowBalance = 43;
                temp2.ColorizationBlurBalance = 49;
                temp2.ColorizationGlassReflectionIntensity = 0;
                temp2.EnableAeroPeek = true;
                temp2.AlwaysHibernateThumbnails = false;
                temp2.Theme = Theme.Structures.Windows7.Themes.Aero;
            }

            {
                ref var temp3 = ref TM.WindowsVista;
                temp3.ColorizationColor = Color.FromArgb(64, 158, 254);
            }

            {
                ref var temp4 = ref TM.CommandPrompt;
                temp4.ColorTable05 = Color.FromArgb(136, 23, 152);
                temp4.ColorTable06 = Color.FromArgb(193, 156, 0);
                temp4.PopupBackground = 15;
                temp4.PopupForeground = 5;
                temp4.ScreenColorsForeground = 7;
                temp4.ScreenColorsBackground = 0;
                temp4.FaceName = "Consolas";
                temp4.FontSize = 18 * 65536;
                temp4.FontRaster = true;
                temp4.W10_1909_ForceV2 = false;
            }

            {
                ref var temp5 = ref TM.PowerShellx86;
                temp5.ColorTable05 = Color.FromArgb(1, 36, 86);
                temp5.ColorTable06 = Color.FromArgb(238, 237, 240);
                temp5.PopupBackground = 15;
                temp5.PopupForeground = 3;
                temp5.ScreenColorsForeground = 6;
                temp5.ScreenColorsBackground = 5;
                temp5.FaceName = "Consolas";
                temp5.FontSize = 14 * 65536;
                temp5.FontRaster = true;
                temp5.W10_1909_ForceV2 = false;
            }

            {
                ref var temp6 = ref TM.PowerShellx64;
                temp6.ColorTable05 = Color.FromArgb(1, 36, 86);
                temp6.ColorTable06 = Color.FromArgb(238, 237, 240);
                temp6.PopupBackground = 15;
                temp6.PopupForeground = 3;
                temp6.ScreenColorsForeground = 6;
                temp6.ScreenColorsBackground = 5;
                temp6.FaceName = "Consolas";
                temp6.FontSize = 14 * 65536;
                temp6.FontRaster = true;
                temp6.W10_1909_ForceV2 = false;
            }

            {
                ref var temp7 = ref TM.MetricsFonts;
                temp7.BorderWidth = 1;
                temp7.CaptionHeight = 21;
                temp7.CaptionWidth = 21;
                temp7.IconSpacing = 75;
                temp7.IconVerticalSpacing = 75;
                temp7.MenuHeight = 19;
                temp7.MenuWidth = 19;
                temp7.PaddedBorderWidth = 4;
                temp7.ScrollHeight = 17;
                temp7.ScrollWidth = 17;
                temp7.SmCaptionHeight = 17;
                temp7.SmCaptionWidth = 17;
                temp7.DesktopIconSize = 48;
                temp7.ShellIconSize = 32;
            }

            {
                ref var temp8 = ref TM.WindowsEffects;
                temp8.ShakeToMinimize = true;
                temp8.BalloonNotifications = true;
                temp8.PaintDesktopVersion = false;
                temp8.ShowSecondsInSystemClock = false;
                temp8.Win11ClassicContextMenu = false;
                temp8.SysListView32 = false;
            }

            TM.Terminal = new WinTerminal("", WinTerminal.Mode.Empty);
            TM.TerminalPreview = new WinTerminal("", WinTerminal.Mode.Empty);
            TM.Cursor_Shadow = true;

            {
                ref var temp9 = ref TM.Sounds;
                temp9.Snd_Imageres_SystemStart = "Default";
                temp9.Snd_Win_Default = My.Env.PATH_Windows + @"\media\Windows Ding.wav";
                temp9.Snd_Win_AppGPFault = "";
                temp9.Snd_Win_CCSelect = "";
                temp9.Snd_Win_ChangeTheme = My.Env.PATH_Windows + @"\media\Windows Logon Sound.wav";
                temp9.Snd_Win_Close = "";
                temp9.Snd_Win_CriticalBatteryAlarm = My.Env.PATH_Windows + @"\media\Windows Battery Critical.wav";
                temp9.Snd_Win_DeviceConnect = My.Env.PATH_Windows + @"\media\Windows Hardware Insert.wav";
                temp9.Snd_Win_DeviceDisconnect = My.Env.PATH_Windows + @"\media\Windows Hardware Remove.wav";
                temp9.Snd_Win_DeviceFail = My.Env.PATH_Windows + @"\media\Windows Hardware Fail.wav";
                temp9.Snd_Win_FaxBeep = My.Env.PATH_Windows + @"\media\Windows Notify.wav";
                temp9.Snd_Win_LowBatteryAlarm = My.Env.PATH_Windows + @"\media\Windows Battery Low.wav";
                temp9.Snd_Win_MailBeep = My.Env.PATH_Windows + @"\media\Windows Notify.wav";
                temp9.Snd_Win_Maximize = "";
                temp9.Snd_Win_MenuCommand = "";
                temp9.Snd_Win_MenuPopup = "";
                temp9.Snd_Win_MessageNudge = "";
                temp9.Snd_Win_Minimize = "";
                temp9.Snd_Win_Notification_Default = "";
                temp9.Snd_Win_Notification_IM = "";
                temp9.Snd_Win_Notification_Looping_Alarm = "";
                temp9.Snd_Win_Notification_Looping_Alarm2 = "";
                temp9.Snd_Win_Notification_Looping_Alarm3 = "";
                temp9.Snd_Win_Notification_Looping_Alarm4 = "";
                temp9.Snd_Win_Notification_Looping_Alarm5 = "";
                temp9.Snd_Win_Notification_Looping_Alarm6 = "";
                temp9.Snd_Win_Notification_Looping_Alarm7 = "";
                temp9.Snd_Win_Notification_Looping_Alarm8 = "";
                temp9.Snd_Win_Notification_Looping_Alarm9 = "";
                temp9.Snd_Win_Notification_Looping_Alarm10 = "";
                temp9.Snd_Win_Notification_Looping_Call = "";
                temp9.Snd_Win_Notification_Looping_Call2 = "";
                temp9.Snd_Win_Notification_Looping_Call3 = "";
                temp9.Snd_Win_Notification_Looping_Call4 = "";
                temp9.Snd_Win_Notification_Looping_Call5 = "";
                temp9.Snd_Win_Notification_Looping_Call6 = "";
                temp9.Snd_Win_Notification_Looping_Call7 = "";
                temp9.Snd_Win_Notification_Looping_Call8 = "";
                temp9.Snd_Win_Notification_Looping_Call9 = "";
                temp9.Snd_Win_Notification_Looping_Call10 = "";
                temp9.Snd_Win_Notification_Mail = "";
                temp9.Snd_Win_Notification_Proximity = "";
                temp9.Snd_Win_Notification_Reminder = "";
                temp9.Snd_Win_Notification_SMS = "";
                temp9.Snd_Win_Open = "";
                temp9.Snd_Win_PrintComplete = "";
                temp9.Snd_Win_ProximityConnection = "";
                temp9.Snd_Win_RestoreDown = "";
                temp9.Snd_Win_RestoreUp = "";
                temp9.Snd_Win_ShowBand = "";
                temp9.Snd_Win_SystemAsterisk = My.Env.PATH_Windows + @"\media\Windows Error.wav";
                temp9.Snd_Win_SystemExclamation = My.Env.PATH_Windows + @"\media\Windows Exclamation.wav";
                temp9.Snd_Win_SystemExit = My.Env.PATH_Windows + @"\media\Windows Shutdown.wav";
                temp9.Snd_Win_SystemStart = "";
                temp9.Snd_Win_SystemHand = My.Env.PATH_Windows + @"\media\Windows Critical Stop.wav";
                temp9.Snd_Win_SystemNotification = My.Env.PATH_Windows + @"\media\Windows Balloon.wav";
                temp9.Snd_Win_SystemQuestion = "";
                temp9.Snd_Win_WindowsLogoff = My.Env.PATH_Windows + @"\media\Windows Logoff Sound.wav";
                temp9.Snd_Win_WindowsLogon = My.Env.PATH_Windows + @"\media\Windows Logon Sound.wav";
                temp9.Snd_Win_WindowsUAC = My.Env.PATH_Windows + @"\media\Windows User Account Control.wav";
                temp9.Snd_Win_WindowsUnlock = "";
                temp9.Snd_Explorer_ActivatingDocument = "";
                temp9.Snd_Explorer_BlockedPopup = My.Env.PATH_Windows + @"\media\Windows Pop-up Blocked.wav";
                temp9.Snd_Explorer_EmptyRecycleBin = My.Env.PATH_Windows + @"\media\Windows Recycle.wav";
                temp9.Snd_Explorer_FeedDiscovered = My.Env.PATH_Windows + @"\media\Windows Feed Discovered.wav";
                temp9.Snd_Explorer_MoveMenuItem = "";
                temp9.Snd_Explorer_Navigating = My.Env.PATH_Windows + @"\media\Windows Navigation Start.wav";
                temp9.Snd_Explorer_SecurityBand = My.Env.PATH_Windows + @"\media\Windows Information Bar.wav";
                temp9.Snd_Explorer_SearchProviderDiscovered = "";
                temp9.Snd_Explorer_FaxError = My.Env.PATH_Windows + @"\media\ding.wav";
                temp9.Snd_Explorer_FaxLineRings = My.Env.PATH_Windows + @"\media\Windows Ringin.wav";
                temp9.Snd_Explorer_FaxNew = "";
                temp9.Snd_Explorer_FaxSent = My.Env.PATH_Windows + @"\media\tada.wav";
                temp9.Snd_NetMeeting_PersonJoins = "";
                temp9.Snd_NetMeeting_PersonLeaves = "";
                temp9.Snd_NetMeeting_ReceiveCall = "";
                temp9.Snd_NetMeeting_ReceiveRequestToJoin = "";
                temp9.Snd_SpeechRec_DisNumbersSound = My.Env.PATH_Windows + @"\media\Speech Disambiguation.wav";
                temp9.Snd_SpeechRec_HubOffSound = My.Env.PATH_Windows + @"\media\Speech Off.wav";
                temp9.Snd_SpeechRec_HubOnSound = My.Env.PATH_Windows + @"\media\Speech On.wav";
                temp9.Snd_SpeechRec_HubSleepSound = My.Env.PATH_Windows + @"\media\Speech Sleep.wav";
                temp9.Snd_SpeechRec_MisrecoSound = My.Env.PATH_Windows + @"\media\Speech Misrecognition.wav";
                temp9.Snd_SpeechRec_PanelSound = My.Env.PATH_Windows + @"\media\Speech Disambiguation.wav";

                temp9.Snd_Win_SystemExit_TaskMgmt = false;
                temp9.Snd_Win_WindowsLogoff_TaskMgmt = false;
                temp9.Snd_Win_WindowsLogon_TaskMgmt = false;
                temp9.Snd_Win_WindowsUnlock_TaskMgmt = false;
            }

            return TM;
        }

        public Theme.Manager WindowsVista()
        {
            var TM = new Theme.Manager(WinPaletter.Theme.Manager.Source.Empty);

            {
                ref var temp = ref TM.Info;
                temp.ThemeName = "Windows Vista (Initial)";
                temp.Description = "Initial; Like first time after Windows Setup";
                temp.ThemeVersion = "1.0.0.0";
                temp.Author = "Microsoft";
                temp.AuthorSocialMediaLink = "www.microsoft.com";
                temp.AppVersion = My.Env.AppVersion;
            }

            {
                ref var temp1 = ref TM.Windows81;
                temp1.ColorizationColor = Color.FromArgb(246, 195, 74);
                temp1.ColorizationColorBalance = 78;
                temp1.PersonalColors_Background = Color.FromArgb(30, 0, 84);
                temp1.PersonalColors_Accent = Color.FromArgb(72, 29, 178);
                temp1.StartColor = Color.FromArgb(30, 0, 84);
                temp1.AccentColor = Color.FromArgb(72, 29, 178);
                temp1.Start = 0;
                temp1.Theme = Theme.Structures.Windows7.Themes.Aero;
                temp1.LogonUI = 0;
                temp1.NoLockScreen = false;
                temp1.LockScreenType = Theme.Structures.LogonUI7.Modes.Default;
                temp1.LockScreenSystemID = 0;
            }

            {
                ref var temp2 = ref TM.Windows7;
                temp2.ColorizationColor = Color.FromArgb(116, 184, 252);
                temp2.ColorizationAfterglow = Color.FromArgb(116, 184, 252);
                temp2.ColorizationColorBalance = 8;
                temp2.ColorizationAfterglowBalance = 43;
                temp2.ColorizationBlurBalance = 49;
                temp2.ColorizationGlassReflectionIntensity = 0;
                temp2.EnableAeroPeek = true;
                temp2.AlwaysHibernateThumbnails = false;
                temp2.Theme = Theme.Structures.Windows7.Themes.Aero;
            }

            {
                ref var temp3 = ref TM.WindowsVista;
                temp3.ColorizationColor = Color.FromArgb(64, 158, 254);
            }

            {
                ref var temp4 = ref TM.CommandPrompt;
                temp4.ColorTable00 = Color.FromArgb(0, 0, 0);
                temp4.ColorTable01 = Color.FromArgb(0, 0, 128);
                temp4.ColorTable02 = Color.FromArgb(0, 128, 0);
                temp4.ColorTable03 = Color.FromArgb(0, 128, 128);
                temp4.ColorTable04 = Color.FromArgb(128, 0, 0);
                temp4.ColorTable05 = Color.FromArgb(128, 0, 128);
                temp4.ColorTable06 = Color.FromArgb(128, 128, 0);
                temp4.ColorTable07 = Color.FromArgb(192, 192, 192);
                temp4.ColorTable08 = Color.FromArgb(128, 128, 128);
                temp4.ColorTable09 = Color.FromArgb(0, 0, 255);
                temp4.ColorTable10 = Color.FromArgb(0, 255, 0);
                temp4.ColorTable11 = Color.FromArgb(0, 255, 255);
                temp4.ColorTable12 = Color.FromArgb(255, 0, 0);
                temp4.ColorTable13 = Color.FromArgb(255, 0, 255);
                temp4.ColorTable14 = Color.FromArgb(255, 255, 0);
                temp4.ColorTable15 = Color.FromArgb(255, 255, 255);
                temp4.PopupForeground = 5;
                temp4.PopupBackground = 15;
                temp4.ScreenColorsForeground = 7;
                temp4.ScreenColorsBackground = 0;
                temp4.FaceName = "Consolas";
                temp4.FontSize = 18 * 65536;
                temp4.FontRaster = true;
                temp4.W10_1909_ForceV2 = false;
            }

            {
                ref var temp5 = ref TM.PowerShellx86;
                temp5.ColorTable00 = Color.FromArgb(12, 12, 12);
                temp5.ColorTable01 = Color.FromArgb(0, 55, 218);
                temp5.ColorTable02 = Color.FromArgb(19, 161, 14);
                temp5.ColorTable03 = Color.FromArgb(58, 150, 221);
                temp5.ColorTable04 = Color.FromArgb(197, 15, 31);
                temp5.ColorTable05 = Color.FromArgb(1, 36, 86);
                temp5.ColorTable06 = Color.FromArgb(238, 237, 240);
                temp5.ColorTable07 = Color.FromArgb(204, 204, 204);
                temp5.ColorTable08 = Color.FromArgb(118, 118, 118);
                temp5.ColorTable09 = Color.FromArgb(59, 120, 255);
                temp5.ColorTable10 = Color.FromArgb(22, 198, 12);
                temp5.ColorTable11 = Color.FromArgb(97, 214, 214);
                temp5.ColorTable12 = Color.FromArgb(231, 72, 86);
                temp5.ColorTable13 = Color.FromArgb(180, 0, 158);
                temp5.ColorTable14 = Color.FromArgb(249, 241, 165);
                temp5.ColorTable15 = Color.FromArgb(242, 242, 242);
                temp5.PopupForeground = 15;
                temp5.PopupBackground = 3;
                temp5.ScreenColorsForeground = 6;
                temp5.ScreenColorsBackground = 5;
                temp5.FaceName = "Consolas";
                temp5.FontSize = 14 * 65536;
                temp5.FontRaster = true;
                temp5.W10_1909_ForceV2 = false;
            }

            {
                ref var temp6 = ref TM.PowerShellx64;
                temp6.ColorTable00 = Color.FromArgb(12, 12, 12);
                temp6.ColorTable01 = Color.FromArgb(0, 55, 218);
                temp6.ColorTable02 = Color.FromArgb(19, 161, 14);
                temp6.ColorTable03 = Color.FromArgb(58, 150, 221);
                temp6.ColorTable04 = Color.FromArgb(197, 15, 31);
                temp6.ColorTable05 = Color.FromArgb(1, 36, 86);
                temp6.ColorTable06 = Color.FromArgb(238, 237, 240);
                temp6.ColorTable07 = Color.FromArgb(204, 204, 204);
                temp6.ColorTable08 = Color.FromArgb(118, 118, 118);
                temp6.ColorTable09 = Color.FromArgb(59, 120, 255);
                temp6.ColorTable10 = Color.FromArgb(22, 198, 12);
                temp6.ColorTable11 = Color.FromArgb(97, 214, 214);
                temp6.ColorTable12 = Color.FromArgb(231, 72, 86);
                temp6.ColorTable13 = Color.FromArgb(180, 0, 158);
                temp6.ColorTable14 = Color.FromArgb(249, 241, 165);
                temp6.ColorTable15 = Color.FromArgb(242, 242, 242);
                temp6.PopupForeground = 15;
                temp6.PopupBackground = 3;
                temp6.ScreenColorsForeground = 6;
                temp6.ScreenColorsBackground = 5;
                temp6.FaceName = "Consolas";
                temp6.FontSize = 14 * 65536;
                temp6.FontRaster = true;
                temp6.W10_1909_ForceV2 = false;
            }

            {
                ref var temp7 = ref TM.MetricsFonts;
                temp7.BorderWidth = 1;
                temp7.CaptionHeight = 19;
                temp7.CaptionWidth = 19;
                temp7.IconSpacing = 75;
                temp7.IconVerticalSpacing = 75;
                temp7.MenuHeight = 19;
                temp7.MenuWidth = 19;
                temp7.PaddedBorderWidth = 4;
                temp7.ScrollHeight = 17;
                temp7.ScrollWidth = 17;
                temp7.SmCaptionHeight = 17;
                temp7.SmCaptionWidth = 17;
                temp7.DesktopIconSize = 48;
                temp7.ShellIconSize = 32;
            }

            {
                ref var temp8 = ref TM.WindowsEffects;
                temp8.ShakeToMinimize = false;
                temp8.BalloonNotifications = true;
                temp8.PaintDesktopVersion = false;
                temp8.ShowSecondsInSystemClock = false;
                temp8.Win11ClassicContextMenu = false;
                temp8.SysListView32 = true;
            }

            TM.Terminal = new WinTerminal("", WinTerminal.Mode.Empty);
            TM.TerminalPreview = new WinTerminal("", WinTerminal.Mode.Empty);
            TM.Cursor_Shadow = true;

            {
                ref var temp9 = ref TM.Sounds;
                temp9.Snd_Imageres_SystemStart = "Default";
                temp9.Snd_Win_Default = My.Env.PATH_Windows + @"\media\Windows Ding.wav";
                temp9.Snd_Win_AppGPFault = "";
                temp9.Snd_Win_CCSelect = "";
                temp9.Snd_Win_ChangeTheme = "";
                temp9.Snd_Win_Close = "";
                temp9.Snd_Win_CriticalBatteryAlarm = My.Env.PATH_Windows + @"\media\Windows Battery Critical.wav";
                temp9.Snd_Win_DeviceConnect = My.Env.PATH_Windows + @"\media\Windows Hardware Insert.wav";
                temp9.Snd_Win_DeviceDisconnect = My.Env.PATH_Windows + @"\media\Windows Hardware Remove.wav";
                temp9.Snd_Win_DeviceFail = My.Env.PATH_Windows + @"\media\Windows Hardware Fail.wav";
                temp9.Snd_Win_FaxBeep = My.Env.PATH_Windows + @"\media\Windows Notify.wav";
                temp9.Snd_Win_LowBatteryAlarm = My.Env.PATH_Windows + @"\media\Windows Battery Low.wav";
                temp9.Snd_Win_MailBeep = My.Env.PATH_Windows + @"\media\Windows Notify.wav";
                temp9.Snd_Win_Maximize = "";
                temp9.Snd_Win_MenuCommand = "";
                temp9.Snd_Win_MenuPopup = "";
                temp9.Snd_Win_MessageNudge = "";
                temp9.Snd_Win_Minimize = "";
                temp9.Snd_Win_Notification_Default = "";
                temp9.Snd_Win_Notification_IM = "";
                temp9.Snd_Win_Notification_Looping_Alarm = "";
                temp9.Snd_Win_Notification_Looping_Alarm2 = "";
                temp9.Snd_Win_Notification_Looping_Alarm3 = "";
                temp9.Snd_Win_Notification_Looping_Alarm4 = "";
                temp9.Snd_Win_Notification_Looping_Alarm5 = "";
                temp9.Snd_Win_Notification_Looping_Alarm6 = "";
                temp9.Snd_Win_Notification_Looping_Alarm7 = "";
                temp9.Snd_Win_Notification_Looping_Alarm8 = "";
                temp9.Snd_Win_Notification_Looping_Alarm9 = "";
                temp9.Snd_Win_Notification_Looping_Alarm10 = "";
                temp9.Snd_Win_Notification_Looping_Call = "";
                temp9.Snd_Win_Notification_Looping_Call2 = "";
                temp9.Snd_Win_Notification_Looping_Call3 = "";
                temp9.Snd_Win_Notification_Looping_Call4 = "";
                temp9.Snd_Win_Notification_Looping_Call5 = "";
                temp9.Snd_Win_Notification_Looping_Call6 = "";
                temp9.Snd_Win_Notification_Looping_Call7 = "";
                temp9.Snd_Win_Notification_Looping_Call8 = "";
                temp9.Snd_Win_Notification_Looping_Call9 = "";
                temp9.Snd_Win_Notification_Looping_Call10 = "";
                temp9.Snd_Win_Notification_Mail = "";
                temp9.Snd_Win_Notification_Proximity = "";
                temp9.Snd_Win_Notification_Reminder = "";
                temp9.Snd_Win_Notification_SMS = "";
                temp9.Snd_Win_Open = "";
                temp9.Snd_Win_PrintComplete = "";
                temp9.Snd_Win_ProximityConnection = "";
                temp9.Snd_Win_RestoreDown = "";
                temp9.Snd_Win_RestoreUp = "";
                temp9.Snd_Win_ShowBand = "";
                temp9.Snd_Win_SystemAsterisk = My.Env.PATH_Windows + @"\media\Windows Error.wav";
                temp9.Snd_Win_SystemExclamation = My.Env.PATH_Windows + @"\media\Windows Exclamation.wav";
                temp9.Snd_Win_SystemExit = My.Env.PATH_Windows + @"\media\Windows Shutdown.wav";
                temp9.Snd_Win_SystemStart = "";
                temp9.Snd_Win_SystemHand = My.Env.PATH_Windows + @"\media\Windows Critical Stop.wav";
                temp9.Snd_Win_SystemNotification = My.Env.PATH_Windows + @"\media\Windows Balloon.wav";
                temp9.Snd_Win_SystemQuestion = "";
                temp9.Snd_Win_WindowsLogoff = My.Env.PATH_Windows + @"\media\Windows Logoff Sound.wav";
                temp9.Snd_Win_WindowsLogon = My.Env.PATH_Windows + @"\media\Windows Logon Sound.wav";
                temp9.Snd_Win_WindowsUAC = My.Env.PATH_Windows + @"\media\Windows User Account Control.wav";
                temp9.Snd_Win_WindowsUnlock = "";
                temp9.Snd_Explorer_ActivatingDocument = "";
                temp9.Snd_Explorer_BlockedPopup = My.Env.PATH_Windows + @"\media\Windows Pop-up Blocked.wav";
                temp9.Snd_Explorer_EmptyRecycleBin = My.Env.PATH_Windows + @"\media\Windows Recycle.wav";
                temp9.Snd_Explorer_FeedDiscovered = My.Env.PATH_Windows + @"\media\Windows Feed Discovered.wav";
                temp9.Snd_Explorer_MoveMenuItem = "";
                temp9.Snd_Explorer_Navigating = My.Env.PATH_Windows + @"\media\Windows Navigation Start.wav";
                temp9.Snd_Explorer_SecurityBand = My.Env.PATH_Windows + @"\media\Windows Information Bar.wav";
                temp9.Snd_Explorer_SearchProviderDiscovered = "";
                temp9.Snd_Explorer_FaxError = My.Env.PATH_Windows + @"\media\ding.wav";
                temp9.Snd_Explorer_FaxLineRings = My.Env.PATH_Windows + @"\media\Windows Ringin.wav";
                temp9.Snd_Explorer_FaxNew = "";
                temp9.Snd_Explorer_FaxSent = My.Env.PATH_Windows + @"\media\tada.wav";
                temp9.Snd_NetMeeting_PersonJoins = "";
                temp9.Snd_NetMeeting_PersonLeaves = "";
                temp9.Snd_NetMeeting_ReceiveCall = "";
                temp9.Snd_NetMeeting_ReceiveRequestToJoin = "";
                temp9.Snd_SpeechRec_DisNumbersSound = My.Env.PATH_Windows + @"\media\Speech Disambiguation.wav";
                temp9.Snd_SpeechRec_HubOffSound = My.Env.PATH_Windows + @"\media\Speech Off.wav";
                temp9.Snd_SpeechRec_HubOnSound = My.Env.PATH_Windows + @"\media\Speech On.wav";
                temp9.Snd_SpeechRec_HubSleepSound = My.Env.PATH_Windows + @"\media\Speech Sleep.wav";
                temp9.Snd_SpeechRec_MisrecoSound = My.Env.PATH_Windows + @"\media\Speech Misrecognition.wav";
                temp9.Snd_SpeechRec_PanelSound = My.Env.PATH_Windows + @"\media\Speech Disambiguation.wav";

                temp9.Snd_Win_SystemExit_TaskMgmt = false;
                temp9.Snd_Win_WindowsLogoff_TaskMgmt = false;
                temp9.Snd_Win_WindowsLogon_TaskMgmt = false;
                temp9.Snd_Win_WindowsUnlock_TaskMgmt = false;
            }

            return TM;
        }

        public Theme.Manager WindowsXP()
        {
            var TM = new Theme.Manager(WinPaletter.Theme.Manager.Source.Empty);

            {
                ref var temp = ref TM.Info;
                temp.ThemeName = "Windows XP (Initial)";
                temp.Description = "Initial; Like first time after Windows Setup";
                temp.ThemeVersion = "1.0.0.0";
                temp.Author = "Microsoft";
                temp.AuthorSocialMediaLink = "www.microsoft.com";
                temp.AppVersion = My.Env.AppVersion;
            }

            {
                ref var temp1 = ref TM.Win32;
                temp1.ActiveBorder = Color.FromArgb(212, 208, 200);
                temp1.ActiveTitle = Color.FromArgb(0, 84, 227);
                temp1.AppWorkspace = Color.FromArgb(128, 128, 128);
                temp1.Background = Color.FromArgb(0, 78, 152);
                temp1.ButtonAlternateFace = Color.FromArgb(181, 181, 181);
                temp1.ButtonDkShadow = Color.FromArgb(113, 111, 100);
                temp1.ButtonFace = Color.FromArgb(236, 233, 216);
                temp1.ButtonHilight = Color.FromArgb(255, 255, 255);
                temp1.ButtonLight = Color.FromArgb(241, 239, 226);
                temp1.ButtonShadow = Color.FromArgb(172, 168, 153);
                temp1.ButtonText = Color.FromArgb(0, 0, 0);
                temp1.GradientActiveTitle = Color.FromArgb(61, 149, 255);
                temp1.GradientInactiveTitle = Color.FromArgb(157, 185, 235);
                temp1.GrayText = Color.FromArgb(172, 168, 153);
                temp1.HilightText = Color.FromArgb(255, 255, 255);
                temp1.HotTrackingColor = Color.FromArgb(0, 0, 128);
                temp1.InactiveBorder = Color.FromArgb(212, 208, 200);
                temp1.InactiveTitle = Color.FromArgb(212, 208, 200);
                temp1.InactiveTitleText = Color.FromArgb(216, 228, 248);
                temp1.InfoText = Color.FromArgb(0, 0, 0);
                temp1.InfoWindow = Color.FromArgb(255, 255, 225);
                temp1.Menu = Color.FromArgb(255, 255, 255);
                temp1.MenuBar = Color.FromArgb(236, 233, 216);
                temp1.MenuText = Color.FromArgb(0, 0, 0);
                temp1.Scrollbar = Color.FromArgb(212, 208, 200);
                temp1.TitleText = Color.FromArgb(255, 255, 255);
                temp1.Window = Color.FromArgb(255, 255, 255);
                temp1.WindowFrame = Color.FromArgb(0, 0, 0);
                temp1.WindowText = Color.FromArgb(0, 0, 0);
                temp1.Hilight = Color.FromArgb(49, 106, 197);
                temp1.MenuHilight = Color.FromArgb(49, 106, 197);
                temp1.Desktop = Color.FromArgb(0, 0, 0);
            }

            {
                ref var temp2 = ref TM.CommandPrompt;
                temp2.ColorTable00 = Color.FromArgb(12, 12, 12);
                temp2.ColorTable01 = Color.FromArgb(0, 55, 218);
                temp2.ColorTable02 = Color.FromArgb(19, 161, 14);
                temp2.ColorTable03 = Color.FromArgb(58, 150, 221);
                temp2.ColorTable04 = Color.FromArgb(197, 15, 31);
                temp2.ColorTable05 = Color.FromArgb(136, 23, 152);
                temp2.ColorTable06 = Color.FromArgb(193, 156, 0);
                temp2.ColorTable07 = Color.FromArgb(204, 204, 204);
                temp2.ColorTable08 = Color.FromArgb(118, 118, 118);
                temp2.ColorTable09 = Color.FromArgb(59, 120, 255);
                temp2.ColorTable10 = Color.FromArgb(22, 198, 12);
                temp2.ColorTable11 = Color.FromArgb(97, 214, 214);
                temp2.ColorTable12 = Color.FromArgb(231, 72, 86);
                temp2.ColorTable13 = Color.FromArgb(180, 0, 158);
                temp2.ColorTable14 = Color.FromArgb(249, 241, 165);
                temp2.ColorTable15 = Color.FromArgb(242, 242, 242);
                temp2.PopupBackground = 15;
                temp2.PopupForeground = 5;
                temp2.ScreenColorsForeground = 7;
                temp2.ScreenColorsBackground = 0;
                temp2.FaceName = "Consolas";
                temp2.FontSize = 18 * 65536;
                temp2.FontRaster = true;
                temp2.W10_1909_ForceV2 = false;
            }

            {
                ref var temp3 = ref TM.PowerShellx86;
                temp3.ColorTable00 = Color.FromArgb(12, 12, 12);
                temp3.ColorTable01 = Color.FromArgb(0, 55, 218);
                temp3.ColorTable02 = Color.FromArgb(19, 161, 14);
                temp3.ColorTable03 = Color.FromArgb(58, 150, 221);
                temp3.ColorTable04 = Color.FromArgb(197, 15, 31);
                temp3.ColorTable05 = Color.FromArgb(1, 36, 86);
                temp3.ColorTable06 = Color.FromArgb(238, 237, 240);
                temp3.ColorTable07 = Color.FromArgb(204, 204, 204);
                temp3.ColorTable08 = Color.FromArgb(118, 118, 118);
                temp3.ColorTable09 = Color.FromArgb(59, 120, 255);
                temp3.ColorTable10 = Color.FromArgb(22, 198, 12);
                temp3.ColorTable11 = Color.FromArgb(97, 214, 214);
                temp3.ColorTable12 = Color.FromArgb(231, 72, 86);
                temp3.ColorTable13 = Color.FromArgb(180, 0, 158);
                temp3.ColorTable14 = Color.FromArgb(249, 241, 165);
                temp3.ColorTable15 = Color.FromArgb(242, 242, 242);
                temp3.PopupBackground = 15;
                temp3.PopupForeground = 3;
                temp3.ScreenColorsForeground = 6;
                temp3.ScreenColorsBackground = 5;
                temp3.FaceName = "Consolas";
                temp3.FontSize = 14 * 65536;
                temp3.FontRaster = true;
                temp3.W10_1909_ForceV2 = false;
            }

            {
                ref var temp4 = ref TM.PowerShellx64;
                temp4.ColorTable00 = Color.FromArgb(12, 12, 12);
                temp4.ColorTable01 = Color.FromArgb(0, 55, 218);
                temp4.ColorTable02 = Color.FromArgb(19, 161, 14);
                temp4.ColorTable03 = Color.FromArgb(58, 150, 221);
                temp4.ColorTable04 = Color.FromArgb(197, 15, 31);
                temp4.ColorTable05 = Color.FromArgb(1, 36, 86);
                temp4.ColorTable06 = Color.FromArgb(238, 237, 240);
                temp4.ColorTable07 = Color.FromArgb(204, 204, 204);
                temp4.ColorTable08 = Color.FromArgb(118, 118, 118);
                temp4.ColorTable09 = Color.FromArgb(59, 120, 255);
                temp4.ColorTable10 = Color.FromArgb(22, 198, 12);
                temp4.ColorTable11 = Color.FromArgb(97, 214, 214);
                temp4.ColorTable12 = Color.FromArgb(231, 72, 86);
                temp4.ColorTable13 = Color.FromArgb(180, 0, 158);
                temp4.ColorTable14 = Color.FromArgb(249, 241, 165);
                temp4.ColorTable15 = Color.FromArgb(242, 242, 242);
                temp4.PopupBackground = 15;
                temp4.PopupForeground = 3;
                temp4.ScreenColorsForeground = 6;
                temp4.ScreenColorsBackground = 5;
                temp4.FaceName = "Consolas";
                temp4.FontSize = 14 * 65536;
                temp4.FontRaster = true;
                temp4.W10_1909_ForceV2 = false;
            }

            {
                ref var temp5 = ref TM.MetricsFonts;
                temp5.BorderWidth = 0;
                temp5.CaptionHeight = 25;
                temp5.CaptionWidth = 18;
                temp5.IconSpacing = 75;
                temp5.IconVerticalSpacing = 75;
                temp5.MenuHeight = 19;
                temp5.MenuWidth = 18;
                temp5.PaddedBorderWidth = 4;
                temp5.ScrollHeight = 17;
                temp5.ScrollWidth = 17;
                temp5.SmCaptionHeight = 17;
                temp5.SmCaptionWidth = 17;
                temp5.DesktopIconSize = 48;
                temp5.ShellIconSize = 32;
                temp5.Fonts_SingleBitPP = true;
                temp5.CaptionFont = new Font("Trebuchet MS", 9.75f, FontStyle.Bold);
                temp5.SmCaptionFont = new Font("Tahoma", 8.25f, FontStyle.Regular);
                temp5.IconFont = new Font("Tahoma", 8.25f, FontStyle.Regular);
                temp5.MenuFont = new Font("Tahoma", 8.25f, FontStyle.Regular);
                temp5.MessageFont = new Font("Tahoma", 8.25f, FontStyle.Regular);
                temp5.StatusFont = new Font("Tahoma", 8.25f, FontStyle.Regular);
            }

            TM.Cursor_Shadow = true;
            {
                ref var temp6 = ref TM.Cursor_Arrow;
                temp6.ArrowStyle = Paths.ArrowStyle.Classic;
                temp6.CircleStyle = Paths.CircleStyle.Classic;
                temp6.PrimaryColor1 = Color.White;
                temp6.PrimaryColor2 = Color.White;
                temp6.SecondaryColor1 = Color.Black;
                temp6.SecondaryColor2 = Color.Black;
                temp6.PrimaryColorGradient = false;
                temp6.SecondaryColorGradient = false;
                temp6.LoadingCircleBack1 = Color.White;
                temp6.LoadingCircleBack2 = Color.White;
                temp6.LoadingCircleHot1 = Color.Black;
                temp6.LoadingCircleHot2 = Color.Black;
                temp6.LoadingCircleBackGradient = false;
                temp6.LoadingCircleHotGradient = false;
                temp6.Shadow_Enabled = false;
                temp6.Shadow_Color = Color.Black;
                temp6.Shadow_Blur = 5;
                temp6.Shadow_Opacity = 0.3f;
                temp6.Shadow_OffsetX = 2;
                temp6.Shadow_OffsetY = 2;
            }

            {
                ref var temp7 = ref TM.Cursor_Help;
                temp7.ArrowStyle = Paths.ArrowStyle.Classic;
                temp7.CircleStyle = Paths.CircleStyle.Classic;
                temp7.PrimaryColor1 = Color.White;
                temp7.PrimaryColor2 = Color.White;
                temp7.SecondaryColor1 = Color.Black;
                temp7.SecondaryColor2 = Color.Black;
                temp7.PrimaryColorGradient = false;
                temp7.SecondaryColorGradient = false;
                temp7.LoadingCircleBack1 = Color.White;
                temp7.LoadingCircleBack2 = Color.White;
                temp7.LoadingCircleHot1 = Color.Black;
                temp7.LoadingCircleHot2 = Color.Black;
                temp7.LoadingCircleBackGradient = false;
                temp7.LoadingCircleHotGradient = false;
                temp7.Shadow_Enabled = false;
                temp7.Shadow_Color = Color.Black;
                temp7.Shadow_Blur = 5;
                temp7.Shadow_Opacity = 0.3f;
                temp7.Shadow_OffsetX = 2;
                temp7.Shadow_OffsetY = 2;
            }

            {
                ref var temp8 = ref TM.Cursor_AppLoading;
                temp8.ArrowStyle = Paths.ArrowStyle.Classic;
                temp8.CircleStyle = Paths.CircleStyle.Classic;
                temp8.PrimaryColor1 = Color.White;
                temp8.PrimaryColor2 = Color.White;
                temp8.SecondaryColor1 = Color.Black;
                temp8.SecondaryColor2 = Color.Black;
                temp8.LoadingCircleBack1 = Color.White;
                temp8.LoadingCircleBack2 = Color.White;
                temp8.LoadingCircleHot1 = Color.Black;
                temp8.LoadingCircleHot2 = Color.Black;
                temp8.LoadingCircleBackGradient = false;
                temp8.LoadingCircleHotGradient = false;
                temp8.PrimaryColorGradient = false;
                temp8.SecondaryColorGradient = false;
                temp8.Shadow_Enabled = false;
                temp8.Shadow_Color = Color.Black;
                temp8.Shadow_Blur = 5;
                temp8.Shadow_Opacity = 0.3f;
                temp8.Shadow_OffsetX = 2;
                temp8.Shadow_OffsetY = 2;
            }

            {
                ref var temp9 = ref TM.Cursor_Busy;
                temp9.ArrowStyle = Paths.ArrowStyle.Classic;
                temp9.CircleStyle = Paths.CircleStyle.Classic;
                temp9.PrimaryColor1 = Color.White;
                temp9.PrimaryColor2 = Color.White;
                temp9.SecondaryColor1 = Color.Black;
                temp9.SecondaryColor2 = Color.Black;
                temp9.LoadingCircleBack1 = Color.White;
                temp9.LoadingCircleBack2 = Color.White;
                temp9.LoadingCircleHot1 = Color.Black;
                temp9.LoadingCircleHot2 = Color.Black;
                temp9.LoadingCircleBackGradient = false;
                temp9.LoadingCircleHotGradient = false;
                temp9.PrimaryColorGradient = false;
                temp9.SecondaryColorGradient = false;
                temp9.Shadow_Enabled = false;
                temp9.Shadow_Color = Color.Black;
                temp9.Shadow_Blur = 5;
                temp9.Shadow_Opacity = 0.3f;
                temp9.Shadow_OffsetX = 2;
                temp9.Shadow_OffsetY = 2;
            }

            {
                ref var temp10 = ref TM.Cursor_Up;
                temp10.ArrowStyle = Paths.ArrowStyle.Classic;
                temp10.CircleStyle = Paths.CircleStyle.Classic;
                temp10.PrimaryColor1 = Color.White;
                temp10.PrimaryColor2 = Color.White;
                temp10.SecondaryColor1 = Color.Black;
                temp10.SecondaryColor2 = Color.Black;
                temp10.PrimaryColorGradient = false;
                temp10.SecondaryColorGradient = false;
                temp10.LoadingCircleBack1 = Color.White;
                temp10.LoadingCircleBack2 = Color.White;
                temp10.LoadingCircleHot1 = Color.Black;
                temp10.LoadingCircleHot2 = Color.Black;
                temp10.LoadingCircleBackGradient = false;
                temp10.LoadingCircleHotGradient = false;
                temp10.Shadow_Enabled = false;
                temp10.Shadow_Color = Color.Black;
                temp10.Shadow_Blur = 5;
                temp10.Shadow_Opacity = 0.3f;
                temp10.Shadow_OffsetX = 2;
                temp10.Shadow_OffsetY = 2;
            }

            {
                ref var temp11 = ref TM.Cursor_NS;
                temp11.ArrowStyle = Paths.ArrowStyle.Classic;
                temp11.CircleStyle = Paths.CircleStyle.Classic;
                temp11.PrimaryColor1 = Color.White;
                temp11.PrimaryColor2 = Color.White;
                temp11.SecondaryColor1 = Color.Black;
                temp11.SecondaryColor2 = Color.Black;
                temp11.PrimaryColorGradient = false;
                temp11.SecondaryColorGradient = false;
                temp11.LoadingCircleBack1 = Color.White;
                temp11.LoadingCircleBack2 = Color.White;
                temp11.LoadingCircleHot1 = Color.Black;
                temp11.LoadingCircleHot2 = Color.Black;
                temp11.LoadingCircleBackGradient = false;
                temp11.LoadingCircleHotGradient = false;
                temp11.Shadow_Enabled = false;
                temp11.Shadow_Color = Color.Black;
                temp11.Shadow_Blur = 5;
                temp11.Shadow_Opacity = 0.3f;
                temp11.Shadow_OffsetX = 2;
                temp11.Shadow_OffsetY = 2;
            }

            {
                ref var temp12 = ref TM.Cursor_EW;
                temp12.ArrowStyle = Paths.ArrowStyle.Classic;
                temp12.CircleStyle = Paths.CircleStyle.Classic;
                temp12.PrimaryColor1 = Color.White;
                temp12.PrimaryColor2 = Color.White;
                temp12.SecondaryColor1 = Color.Black;
                temp12.SecondaryColor2 = Color.Black;
                temp12.PrimaryColorGradient = false;
                temp12.SecondaryColorGradient = false;
                temp12.LoadingCircleBack1 = Color.White;
                temp12.LoadingCircleBack2 = Color.White;
                temp12.LoadingCircleHot1 = Color.Black;
                temp12.LoadingCircleHot2 = Color.Black;
                temp12.LoadingCircleBackGradient = false;
                temp12.LoadingCircleHotGradient = false;
                temp12.Shadow_Enabled = false;
                temp12.Shadow_Color = Color.Black;
                temp12.Shadow_Blur = 5;
                temp12.Shadow_Opacity = 0.3f;
                temp12.Shadow_OffsetX = 2;
                temp12.Shadow_OffsetY = 2;
            }

            {
                ref var temp13 = ref TM.Cursor_NESW;
                temp13.ArrowStyle = Paths.ArrowStyle.Classic;
                temp13.CircleStyle = Paths.CircleStyle.Classic;
                temp13.PrimaryColor1 = Color.White;
                temp13.PrimaryColor2 = Color.White;
                temp13.SecondaryColor1 = Color.Black;
                temp13.SecondaryColor2 = Color.Black;
                temp13.PrimaryColorGradient = false;
                temp13.SecondaryColorGradient = false;
                temp13.LoadingCircleBack1 = Color.White;
                temp13.LoadingCircleBack2 = Color.White;
                temp13.LoadingCircleHot1 = Color.Black;
                temp13.LoadingCircleHot2 = Color.Black;
                temp13.LoadingCircleBackGradient = false;
                temp13.LoadingCircleHotGradient = false;
                temp13.Shadow_Enabled = false;
                temp13.Shadow_Color = Color.Black;
                temp13.Shadow_Blur = 5;
                temp13.Shadow_Opacity = 0.3f;
                temp13.Shadow_OffsetX = 2;
                temp13.Shadow_OffsetY = 2;
            }

            {
                ref var temp14 = ref TM.Cursor_NWSE;
                temp14.ArrowStyle = Paths.ArrowStyle.Classic;
                temp14.CircleStyle = Paths.CircleStyle.Classic;
                temp14.PrimaryColor1 = Color.White;
                temp14.PrimaryColor2 = Color.White;
                temp14.SecondaryColor1 = Color.Black;
                temp14.SecondaryColor2 = Color.Black;
                temp14.PrimaryColorGradient = false;
                temp14.SecondaryColorGradient = false;
                temp14.LoadingCircleBack1 = Color.White;
                temp14.LoadingCircleBack2 = Color.White;
                temp14.LoadingCircleHot1 = Color.Black;
                temp14.LoadingCircleHot2 = Color.Black;
                temp14.LoadingCircleBackGradient = false;
                temp14.LoadingCircleHotGradient = false;
                temp14.Shadow_Enabled = false;
                temp14.Shadow_Color = Color.Black;
                temp14.Shadow_Blur = 5;
                temp14.Shadow_Opacity = 0.3f;
                temp14.Shadow_OffsetX = 2;
                temp14.Shadow_OffsetY = 2;
            }

            {
                ref var temp15 = ref TM.Cursor_Move;
                temp15.ArrowStyle = Paths.ArrowStyle.Classic;
                temp15.CircleStyle = Paths.CircleStyle.Classic;
                temp15.PrimaryColor1 = Color.White;
                temp15.PrimaryColor2 = Color.White;
                temp15.SecondaryColor1 = Color.Black;
                temp15.SecondaryColor2 = Color.Black;
                temp15.PrimaryColorGradient = false;
                temp15.SecondaryColorGradient = false;
                temp15.LoadingCircleBack1 = Color.White;
                temp15.LoadingCircleBack2 = Color.White;
                temp15.LoadingCircleHot1 = Color.Black;
                temp15.LoadingCircleHot2 = Color.Black;
                temp15.LoadingCircleBackGradient = false;
                temp15.LoadingCircleHotGradient = false;
                temp15.Shadow_Enabled = false;
                temp15.Shadow_Color = Color.Black;
                temp15.Shadow_Blur = 5;
                temp15.Shadow_Opacity = 0.3f;
                temp15.Shadow_OffsetX = 2;
                temp15.Shadow_OffsetY = 2;
            }

            {
                ref var temp16 = ref TM.Cursor_None;
                temp16.ArrowStyle = Paths.ArrowStyle.Classic;
                temp16.CircleStyle = Paths.CircleStyle.Classic;
                temp16.PrimaryColor1 = Color.Transparent;
                temp16.PrimaryColor2 = Color.Transparent;
                temp16.SecondaryColor1 = Color.Black;
                temp16.SecondaryColor2 = Color.Black;
                temp16.PrimaryColorGradient = false;
                temp16.SecondaryColorGradient = false;
                temp16.LoadingCircleBack1 = Color.White;
                temp16.LoadingCircleBack2 = Color.White;
                temp16.LoadingCircleHot1 = Color.Black;
                temp16.LoadingCircleHot2 = Color.Black;
                temp16.LoadingCircleBackGradient = false;
                temp16.LoadingCircleHotGradient = false;
                temp16.Shadow_Enabled = false;
                temp16.Shadow_Color = Color.Black;
                temp16.Shadow_Blur = 5;
                temp16.Shadow_Opacity = 0.3f;
                temp16.Shadow_OffsetX = 2;
                temp16.Shadow_OffsetY = 2;
            }

            {
                ref var temp17 = ref TM.Cursor_Arrow;
                temp17.ArrowStyle = Paths.ArrowStyle.Classic;
                temp17.CircleStyle = Paths.CircleStyle.Classic;
                temp17.PrimaryColor1 = Color.White;
                temp17.PrimaryColor2 = Color.White;
                temp17.SecondaryColor1 = Color.Black;
                temp17.SecondaryColor2 = Color.Black;
                temp17.PrimaryColorGradient = false;
                temp17.SecondaryColorGradient = false;
                temp17.LoadingCircleBack1 = Color.White;
                temp17.LoadingCircleBack2 = Color.White;
                temp17.LoadingCircleHot1 = Color.Black;
                temp17.LoadingCircleHot2 = Color.Black;
                temp17.LoadingCircleBackGradient = false;
                temp17.LoadingCircleHotGradient = false;
                temp17.Shadow_Enabled = false;
                temp17.Shadow_Color = Color.Black;
                temp17.Shadow_Blur = 5;
                temp17.Shadow_Opacity = 0.3f;
                temp17.Shadow_OffsetX = 2;
                temp17.Shadow_OffsetY = 2;
            }

            {
                ref var temp18 = ref TM.Cursor_Pen;
                temp18.ArrowStyle = Paths.ArrowStyle.Classic;
                temp18.CircleStyle = Paths.CircleStyle.Classic;
                temp18.PrimaryColor1 = Color.White;
                temp18.PrimaryColor2 = Color.White;
                temp18.SecondaryColor1 = Color.Black;
                temp18.SecondaryColor2 = Color.Black;
                temp18.PrimaryColorGradient = false;
                temp18.SecondaryColorGradient = false;
                temp18.LoadingCircleBack1 = Color.White;
                temp18.LoadingCircleBack2 = Color.White;
                temp18.LoadingCircleHot1 = Color.Black;
                temp18.LoadingCircleHot2 = Color.Black;
                temp18.LoadingCircleBackGradient = false;
                temp18.LoadingCircleHotGradient = false;
                temp18.Shadow_Enabled = false;
                temp18.Shadow_Color = Color.Black;
                temp18.Shadow_Blur = 5;
                temp18.Shadow_Opacity = 0.3f;
                temp18.Shadow_OffsetX = 2;
                temp18.Shadow_OffsetY = 2;
            }

            {
                ref var temp19 = ref TM.Cursor_IBeam;
                temp19.ArrowStyle = Paths.ArrowStyle.Classic;
                temp19.CircleStyle = Paths.CircleStyle.Classic;
                temp19.PrimaryColor1 = Color.White;
                temp19.PrimaryColor2 = Color.White;
                temp19.SecondaryColor1 = Color.Black;
                temp19.SecondaryColor2 = Color.Black;
                temp19.PrimaryColorGradient = false;
                temp19.SecondaryColorGradient = false;
                temp19.LoadingCircleBack1 = Color.White;
                temp19.LoadingCircleBack2 = Color.White;
                temp19.LoadingCircleHot1 = Color.Black;
                temp19.LoadingCircleHot2 = Color.Black;
                temp19.LoadingCircleBackGradient = false;
                temp19.LoadingCircleHotGradient = false;
                temp19.Shadow_Enabled = false;
                temp19.Shadow_Color = Color.Black;
                temp19.Shadow_Blur = 5;
                temp19.Shadow_Opacity = 0.3f;
                temp19.Shadow_OffsetX = 2;
                temp19.Shadow_OffsetY = 2;
            }

            {
                ref var temp20 = ref TM.Cursor_Cross;
                temp20.ArrowStyle = Paths.ArrowStyle.Classic;
                temp20.CircleStyle = Paths.CircleStyle.Classic;
                temp20.PrimaryColor1 = Color.White;
                temp20.PrimaryColor2 = Color.White;
                temp20.SecondaryColor1 = Color.Black;
                temp20.SecondaryColor2 = Color.Black;
                temp20.PrimaryColorGradient = false;
                temp20.SecondaryColorGradient = false;
                temp20.LoadingCircleBack1 = Color.White;
                temp20.LoadingCircleBack2 = Color.White;
                temp20.LoadingCircleHot1 = Color.Black;
                temp20.LoadingCircleHot2 = Color.Black;
                temp20.LoadingCircleBackGradient = false;
                temp20.LoadingCircleHotGradient = false;
                temp20.Shadow_Enabled = false;
                temp20.Shadow_Color = Color.Black;
                temp20.Shadow_Blur = 5;
                temp20.Shadow_Opacity = 0.3f;
                temp20.Shadow_OffsetX = 2;
                temp20.Shadow_OffsetY = 2;
            }

            {
                ref var temp21 = ref TM.Cursor_Link;
                temp21.ArrowStyle = Paths.ArrowStyle.Classic;
                temp21.CircleStyle = Paths.CircleStyle.Classic;
                temp21.PrimaryColor1 = Color.White;
                temp21.PrimaryColor2 = Color.White;
                temp21.SecondaryColor1 = Color.Black;
                temp21.SecondaryColor2 = Color.Black;
                temp21.PrimaryColorGradient = false;
                temp21.SecondaryColorGradient = false;
                temp21.LoadingCircleBack1 = Color.White;
                temp21.LoadingCircleBack2 = Color.White;
                temp21.LoadingCircleHot1 = Color.Black;
                temp21.LoadingCircleHot2 = Color.Black;
                temp21.LoadingCircleBackGradient = false;
                temp21.LoadingCircleHotGradient = false;
                temp21.Shadow_Enabled = false;
                temp21.Shadow_Color = Color.Black;
                temp21.Shadow_Blur = 5;
                temp21.Shadow_Opacity = 0.3f;
                temp21.Shadow_OffsetX = 2;
                temp21.Shadow_OffsetY = 2;
            }

            {
                ref var temp22 = ref TM.Cursor_Pin;
                temp22.ArrowStyle = Paths.ArrowStyle.Classic;
                temp22.CircleStyle = Paths.CircleStyle.Classic;
                temp22.PrimaryColor1 = Color.White;
                temp22.PrimaryColor2 = Color.White;
                temp22.SecondaryColor1 = Color.Black;
                temp22.SecondaryColor2 = Color.Black;
                temp22.PrimaryColorGradient = false;
                temp22.SecondaryColorGradient = false;
                temp22.LoadingCircleBack1 = Color.White;
                temp22.LoadingCircleBack2 = Color.White;
                temp22.LoadingCircleHot1 = Color.Black;
                temp22.LoadingCircleHot2 = Color.Black;
                temp22.LoadingCircleBackGradient = false;
                temp22.LoadingCircleHotGradient = false;
                temp22.Shadow_Enabled = false;
                temp22.Shadow_Color = Color.Black;
                temp22.Shadow_Blur = 5;
                temp22.Shadow_Opacity = 0.3f;
                temp22.Shadow_OffsetX = 2;
                temp22.Shadow_OffsetY = 2;
            }

            {
                ref var temp23 = ref TM.Cursor_Person;
                temp23.ArrowStyle = Paths.ArrowStyle.Classic;
                temp23.CircleStyle = Paths.CircleStyle.Classic;
                temp23.PrimaryColor1 = Color.White;
                temp23.PrimaryColor2 = Color.White;
                temp23.SecondaryColor1 = Color.Black;
                temp23.SecondaryColor2 = Color.Black;
                temp23.PrimaryColorGradient = false;
                temp23.SecondaryColorGradient = false;
                temp23.LoadingCircleBack1 = Color.White;
                temp23.LoadingCircleBack2 = Color.White;
                temp23.LoadingCircleHot1 = Color.Black;
                temp23.LoadingCircleHot2 = Color.Black;
                temp23.LoadingCircleBackGradient = false;
                temp23.LoadingCircleHotGradient = false;
                temp23.Shadow_Enabled = false;
                temp23.Shadow_Color = Color.Black;
                temp23.Shadow_Blur = 5;
                temp23.Shadow_Opacity = 0.3f;
                temp23.Shadow_OffsetX = 2;
                temp23.Shadow_OffsetY = 2;
            }

            {
                ref var temp24 = ref TM.WindowsEffects;
                temp24.ShakeToMinimize = false;
                temp24.BalloonNotifications = true;
                temp24.PaintDesktopVersion = false;
                temp24.ShowSecondsInSystemClock = false;
                temp24.Win11ClassicContextMenu = false;
                temp24.SysListView32 = true;
            }

            TM.Terminal = new WinTerminal("", WinTerminal.Mode.Empty);
            TM.TerminalPreview = new WinTerminal("", WinTerminal.Mode.Empty);
            TM.Cursor_Shadow = true;

            {
                ref var temp25 = ref TM.ScreenSaver;
                temp25.Enabled = true;
                temp25.IsSecure = false;
                temp25.TimeOut = 60;
                temp25.File = My.Env.PATH_System32 + @"\logon.scr";
            }

            {
                ref var temp26 = ref TM.Sounds;
                temp26.Snd_Imageres_SystemStart = "";
                temp26.Snd_Win_Default = My.Env.PATH_Windows + @"\media\Windows XP Ding.wav";
                temp26.Snd_Win_AppGPFault = "";
                temp26.Snd_Win_CCSelect = "";
                temp26.Snd_Win_ChangeTheme = "";
                temp26.Snd_Win_Close = "";
                temp26.Snd_Win_CriticalBatteryAlarm = My.Env.PATH_Windows + @"\media\Windows XP Battery Critical.wav";
                temp26.Snd_Win_DeviceConnect = My.Env.PATH_Windows + @"\media\Windows XP Hardware Insert.wav";
                temp26.Snd_Win_DeviceDisconnect = My.Env.PATH_Windows + @"\media\Windows XP Hardware Remove.wav";
                temp26.Snd_Win_DeviceFail = My.Env.PATH_Windows + @"\media\Windows XP Hardware Fail.wav";
                temp26.Snd_Win_FaxBeep = "";
                temp26.Snd_Win_LowBatteryAlarm = My.Env.PATH_Windows + @"\media\Windows XP Battery Low.wav";
                temp26.Snd_Win_MailBeep = My.Env.PATH_Windows + @"\media\Windows XP Notify.wav";
                temp26.Snd_Win_Maximize = "";
                temp26.Snd_Win_MenuCommand = "";
                temp26.Snd_Win_MenuPopup = "";
                temp26.Snd_Win_MessageNudge = "";
                temp26.Snd_Win_Minimize = "";
                temp26.Snd_Win_Notification_Default = "";
                temp26.Snd_Win_Notification_IM = "";
                temp26.Snd_Win_Notification_Looping_Alarm = "";
                temp26.Snd_Win_Notification_Looping_Alarm2 = "";
                temp26.Snd_Win_Notification_Looping_Alarm3 = "";
                temp26.Snd_Win_Notification_Looping_Alarm4 = "";
                temp26.Snd_Win_Notification_Looping_Alarm5 = "";
                temp26.Snd_Win_Notification_Looping_Alarm6 = "";
                temp26.Snd_Win_Notification_Looping_Alarm7 = "";
                temp26.Snd_Win_Notification_Looping_Alarm8 = "";
                temp26.Snd_Win_Notification_Looping_Alarm9 = "";
                temp26.Snd_Win_Notification_Looping_Alarm10 = "";
                temp26.Snd_Win_Notification_Looping_Call = "";
                temp26.Snd_Win_Notification_Looping_Call2 = "";
                temp26.Snd_Win_Notification_Looping_Call3 = "";
                temp26.Snd_Win_Notification_Looping_Call4 = "";
                temp26.Snd_Win_Notification_Looping_Call5 = "";
                temp26.Snd_Win_Notification_Looping_Call6 = "";
                temp26.Snd_Win_Notification_Looping_Call7 = "";
                temp26.Snd_Win_Notification_Looping_Call8 = "";
                temp26.Snd_Win_Notification_Looping_Call9 = "";
                temp26.Snd_Win_Notification_Looping_Call10 = "";
                temp26.Snd_Win_Notification_Mail = "";
                temp26.Snd_Win_Notification_Proximity = "";
                temp26.Snd_Win_Notification_Reminder = "";
                temp26.Snd_Win_Notification_SMS = "";
                temp26.Snd_Win_Open = "";
                temp26.Snd_Win_PrintComplete = "";
                temp26.Snd_Win_ProximityConnection = "";
                temp26.Snd_Win_RestoreDown = "";
                temp26.Snd_Win_RestoreUp = "";
                temp26.Snd_Win_ShowBand = "";
                temp26.Snd_Win_SystemAsterisk = My.Env.PATH_Windows + @"\media\Windows XP Error.wav";
                temp26.Snd_Win_SystemExclamation = My.Env.PATH_Windows + @"\media\Windows XP Exclamation.wav";
                temp26.Snd_Win_SystemExit = My.Env.PATH_Windows + @"\media\Windows XP Shutdown.wav";
                temp26.Snd_Win_SystemStart = My.Env.PATH_Windows + @"\media\Windows XP Startup.wav";
                temp26.Snd_Win_SystemHand = My.Env.PATH_Windows + @"\media\Windows XP Critical Stop.wav";
                temp26.Snd_Win_SystemNotification = My.Env.PATH_Windows + @"\media\Windows XP Balloon.wav";
                temp26.Snd_Win_SystemQuestion = "";
                temp26.Snd_Win_WindowsLogoff = My.Env.PATH_Windows + @"\media\Windows XP Logoff Sound.wav";
                temp26.Snd_Win_WindowsLogon = My.Env.PATH_Windows + @"\media\Windows XP Logon Sound.wav";
                temp26.Snd_Win_WindowsUAC = "";
                temp26.Snd_Win_WindowsUnlock = "";
                temp26.Snd_Explorer_ActivatingDocument = "";
                temp26.Snd_Explorer_BlockedPopup = My.Env.PATH_Windows + @"\media\Windows Pop-up Blocked.wav";
                temp26.Snd_Explorer_EmptyRecycleBin = My.Env.PATH_Windows + @"\media\Windows XP Recycle.wav";
                temp26.Snd_Explorer_FeedDiscovered = My.Env.PATH_Windows + @"\media\Windows Feed Discovered.wav";
                temp26.Snd_Explorer_MoveMenuItem = "";
                temp26.Snd_Explorer_Navigating = My.Env.PATH_Windows + @"\media\Windows Navigation Start.wav";
                temp26.Snd_Explorer_SecurityBand = My.Env.PATH_Windows + @"\media\Windows Information Bar.wav";
                temp26.Snd_Explorer_SearchProviderDiscovered = "";
                temp26.Snd_Explorer_FaxError = My.Env.PATH_Windows + @"\media\ding.wav";
                temp26.Snd_Explorer_FaxLineRings = My.Env.PATH_Windows + @"\media\ringin.wav";
                temp26.Snd_Explorer_FaxNew = My.Env.PATH_Windows + @"\media\notify.wav";
                temp26.Snd_Explorer_FaxSent = My.Env.PATH_Windows + @"\media\tada.wav";
                temp26.Snd_NetMeeting_PersonJoins = My.Env.PATH_ProgramFiles + @"\NetMeeting\Blip.wav";
                temp26.Snd_NetMeeting_PersonLeaves = My.Env.PATH_ProgramFiles + @"\NetMeeting\Blip.wav";
                temp26.Snd_NetMeeting_ReceiveCall = My.Env.PATH_Windows + @"\media\Windows XP RingIn.wav";
                temp26.Snd_NetMeeting_ReceiveRequestToJoin = My.Env.PATH_Windows + @"\media\Windows XP RingIn.wav";
                temp26.Snd_SpeechRec_DisNumbersSound = "";
                temp26.Snd_SpeechRec_HubOffSound = "";
                temp26.Snd_SpeechRec_HubOnSound = "";
                temp26.Snd_SpeechRec_HubSleepSound = "";
                temp26.Snd_SpeechRec_MisrecoSound = "";
                temp26.Snd_SpeechRec_PanelSound = "";

                temp26.Snd_Win_SystemExit_TaskMgmt = false;
                temp26.Snd_Win_WindowsLogoff_TaskMgmt = false;
                temp26.Snd_Win_WindowsLogon_TaskMgmt = false;
                temp26.Snd_Win_WindowsUnlock_TaskMgmt = false;
            }

            {
                ref var temp27 = ref TM.Wallpaper;
                temp27.ImageFile = My.Env.PATH_Windows + @"\Web\Wallpaper\Bliss.bmp";
                temp27.WallpaperStyle = Theme.Structures.Wallpaper.WallpaperStyles.Stretched;
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