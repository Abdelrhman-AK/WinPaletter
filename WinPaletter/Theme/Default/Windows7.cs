﻿using System.Drawing;

namespace WinPaletter.Theme
{
    public static partial class Default
    {
        public static Manager Windows7()
        {
            Manager TM = new(Manager.Source.Empty);

            {
                ref Structures.Info Info = ref TM.Info;
                Info.ThemeName = "Windows 7 (Initial)";
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
            }

            {
                ref Structures.Windows7 Win7 = ref TM.Windows7;
                Win7.ColorizationColor = Color.FromArgb(116, 184, 252);
                Win7.ColorizationAfterglow = Color.FromArgb(116, 184, 252);
                Win7.ColorizationColorBalance = 8;
                Win7.ColorizationAfterglowBalance = 43;
                Win7.ColorizationBlurBalance = 49;
                Win7.ColorizationGlassReflectionIntensity = 0;
                Win7.Theme = Structures.Windows7.Themes.Aero;
            }

            {
                ref Structures.WindowsVista WinVista = ref TM.WindowsVista;
                WinVista.ColorizationColor = Color.FromArgb(64, 158, 254);
            }

            {
                ref Structures.Console CMD = ref TM.CommandPrompt;
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
                ref Structures.Console PS86 = ref TM.PowerShellx86;
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
                ref Structures.Console PS64 = ref TM.PowerShellx64;
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
                ref Structures.MetricsFonts MetricsFonts = ref TM.MetricsFonts;
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
                ref Structures.WinEffects WinEffects = ref TM.WindowsEffects;
                WinEffects.ShakeToMinimize = true;
                WinEffects.BalloonNotifications = true;
                WinEffects.PaintDesktopVersion = false;
                WinEffects.ShowSecondsInSystemClock = false;
                WinEffects.Win11ClassicContextMenu = false;
                WinEffects.SysListView32 = false;
                WinEffects.EnableAeroPeek = true;
            }

            TM.Terminal = new(string.Empty, WinTerminal.Mode.Empty);
            TM.TerminalPreview = new(string.Empty, WinTerminal.Mode.Empty);

            TM.Cursors.Enabled = true;
            TM.Cursors.Shadow = true;
            TM.Cursors.Cursor_Arrow.UseFromFile = false;
            TM.Cursors.Cursor_AppLoading.UseFromFile = false;
            TM.Cursors.Cursor_Busy.UseFromFile = false;
            TM.Cursors.Cursor_Help.UseFromFile = false;
            TM.Cursors.Cursor_Pen.UseFromFile = false;
            TM.Cursors.Cursor_None.UseFromFile = false;
            TM.Cursors.Cursor_Move.UseFromFile = false;
            TM.Cursors.Cursor_Up.UseFromFile = false;
            TM.Cursors.Cursor_NS.UseFromFile = false;
            TM.Cursors.Cursor_EW.UseFromFile = false;
            TM.Cursors.Cursor_NESW.UseFromFile = false;
            TM.Cursors.Cursor_NWSE.UseFromFile = false;
            TM.Cursors.Cursor_Link.UseFromFile = false;
            TM.Cursors.Cursor_Pin.UseFromFile = false;
            TM.Cursors.Cursor_Person.UseFromFile = false;
            TM.Cursors.Cursor_IBeam.UseFromFile = false;
            TM.Cursors.Cursor_Cross.UseFromFile = false;

            TM.Cursors.Cursor_AppLoading.LoadingCircleBack1 = Color.FromArgb(5, 109, 109);
            TM.Cursors.Cursor_AppLoading.LoadingCircleBack2 = Color.FromArgb(66, 109, 122);
            TM.Cursors.Cursor_AppLoading.LoadingCircleHot1 = Color.FromArgb(41, 174, 178);
            TM.Cursors.Cursor_AppLoading.LoadingCircleHot2 = Color.FromArgb(121, 245, 239);
            TM.Cursors.Cursor_AppLoading.LoadingCircleBackGradient = true;
            TM.Cursors.Cursor_AppLoading.LoadingCircleHotGradient = true;
            TM.Cursors.Cursor_AppLoading.LoadingCircleBackGradientMode = Paths.GradientMode.Circle;
            TM.Cursors.Cursor_AppLoading.LoadingCircleHotGradientMode = Paths.GradientMode.Circle;
            TM.Cursors.Cursor_AppLoading.LoadingCircleHotNoise = true;
            TM.Cursors.Cursor_AppLoading.LoadingCircleBackNoiseOpacity = 0.5F;

            TM.Cursors.Cursor_Busy.LoadingCircleBack1 = Color.FromArgb(5, 109, 109);
            TM.Cursors.Cursor_Busy.LoadingCircleBack2 = Color.FromArgb(66, 109, 122);
            TM.Cursors.Cursor_Busy.LoadingCircleHot1 = Color.FromArgb(41, 174, 178);
            TM.Cursors.Cursor_Busy.LoadingCircleHot2 = Color.FromArgb(121, 245, 239);
            TM.Cursors.Cursor_Busy.LoadingCircleBackGradient = true;
            TM.Cursors.Cursor_Busy.LoadingCircleHotGradient = true;
            TM.Cursors.Cursor_Busy.LoadingCircleBackGradientMode = Paths.GradientMode.Circle;
            TM.Cursors.Cursor_Busy.LoadingCircleHotGradientMode = Paths.GradientMode.Circle;
            TM.Cursors.Cursor_Busy.LoadingCircleHotNoise = true;
            TM.Cursors.Cursor_Busy.LoadingCircleBackNoiseOpacity = 0.5F;

            {
                ref Structures.Sounds Sounds = ref TM.Sounds;
                Sounds.Snd_Imageres_SystemStart = "Default";
                Sounds.Snd_Win_Default = $@"{SysPaths.Windows}\media\Windows Ding.wav";
                Sounds.Snd_Win_AppGPFault = string.Empty;
                Sounds.Snd_Win_CCSelect = string.Empty;
                Sounds.Snd_Win_ChangeTheme = $@"{SysPaths.Windows}\media\Windows Logon Sound.wav";
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
            }

            return TM;
        }
    }
}