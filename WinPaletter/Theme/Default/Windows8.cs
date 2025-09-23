﻿using System.Drawing;
using WinPaletter.Theme.Structures;

namespace WinPaletter.Theme
{
    public static partial class Default
    {
        /// <summary>
        /// Default Windows 8 theme as a <see cref="Manager"/> object
        /// </summary>
        /// <returns><see cref="Manager"/> </returns>
        public static Manager Windows8()
        {
            Manager TM = new(Manager.Source.Empty);

            ref Info Info = ref TM.Info;
            Info.ThemeName = "Default Windows 8";
            Info.Description = "Initial, like the default look after a fresh Windows 8 setup—clean, untouched, and ready for customization.";
            Info.ThemeVersion = "1.0.0.0";
            Info.Author = "Microsoft";
            Info.AuthorSocialMediaLink = "https://www.microsoft.com";
            Info.AppVersion = Program.Version;

            ref Windows81 Win81 = ref TM.Windows81;
            Win81.Enabled = true;
            Win81.ColorizationColor = Color.FromArgb(246, 195, 74);
            Win81.ColorizationColorBalance = 78;
            Win81.PersonalColors_Background = Color.FromArgb(30, 0, 84);
            Win81.PersonalColors_Accent = Color.FromArgb(72, 29, 178);
            Win81.StartColor = Color.FromArgb(30, 0, 84);
            Win81.AccentColor = Color.FromArgb(72, 29, 178);
            Win81.Start = 0;

            ref Console CMD = ref TM.CommandPrompt;
            CMD.Enabled = true;
            CMD.ColorTable05 = Color.FromArgb(136, 23, 152);
            CMD.ColorTable06 = Color.FromArgb(193, 156, 0);
            CMD.PopupBackground = 15;
            CMD.PopupForeground = 5;
            CMD.ScreenColorsForeground = 7;
            CMD.ScreenColorsBackground = 0;
            CMD.FaceName = "Consolas";
            CMD.PixelWidth = 18;
            CMD.FontRaster = true;
            CMD.W10_1909_ForceV2 = false;

            ref Console PS86 = ref TM.PowerShellx86;
            PS86.Enabled = true;
            PS86.ColorTable05 = Color.FromArgb(1, 36, 86);
            PS86.ColorTable06 = Color.FromArgb(238, 237, 240);
            PS86.PopupBackground = 15;
            PS86.PopupForeground = 3;
            PS86.ScreenColorsForeground = 6;
            PS86.ScreenColorsBackground = 5;
            PS86.FaceName = "Consolas";
            PS86.PixelWidth = 14;
            PS86.FontRaster = true;
            PS86.W10_1909_ForceV2 = false;

            ref Console PS64 = ref TM.PowerShellx64;
            PS64.Enabled = true;
            PS64.ColorTable05 = Color.FromArgb(1, 36, 86);
            PS64.ColorTable06 = Color.FromArgb(238, 237, 240);
            PS64.PopupBackground = 15;
            PS64.PopupForeground = 3;
            PS64.ScreenColorsForeground = 6;
            PS64.ScreenColorsBackground = 5;
            PS64.FaceName = "Consolas";
            PS86.PixelWidth = 14;
            PS64.FontRaster = true;
            PS64.W10_1909_ForceV2 = false;

            ref MetricsFonts MetricsFonts = ref TM.MetricsFonts;
            MetricsFonts.Enabled = true;
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
            MetricsFonts.CaptionFont = new("Segoe UI", 11.25f, FontStyle.Regular);
            MetricsFonts.SmCaptionFont = new("Segoe UI", 11.25f, FontStyle.Regular);

            ref WinEffects WinEffects = ref TM.WindowsEffects;
            WinEffects.Enabled = true;
            WinEffects.ShakeToMinimize = true;
            WinEffects.BalloonNotifications = true;
            WinEffects.PaintDesktopVersion = false;
            WinEffects.ShowSecondsInSystemClock = false;
            WinEffects.Win11ClassicContextMenu = false;
            WinEffects.SysListView32 = false;
            WinEffects.EnableAeroPeek = true;

            ref Icons Icons = ref TM.Icons;
            Icons.Enabled = true;

            TM.Terminal = new(string.Empty, WinTerminal.Mode.Empty);
            TM.TerminalPreview = new(string.Empty, WinTerminal.Mode.Empty);

            TM.Cursors.Enabled = true;
            TM.Cursors.Shadow = false;
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

            ref Sounds Sounds = ref TM.Sounds;
            Sounds.Enabled = true;
            Sounds.Snd_Imageres_SystemStart = string.Empty;
            Sounds.Snd_Win_Default = $@"{SysPaths.Windows}\media\Windows Background.wav";
            Sounds.Snd_Win_AppGPFault = string.Empty;
            Sounds.Snd_Win_CCSelect = string.Empty;
            Sounds.Snd_Win_ChangeTheme = string.Empty;
            Sounds.Snd_Win_Close = string.Empty;
            Sounds.Snd_Win_CriticalBatteryAlarm = $@"{SysPaths.Windows}\media\Windows Foreground.wav";
            Sounds.Snd_Win_DeviceConnect = $@"{SysPaths.Windows}\media\Windows Hardware Insert.wav";
            Sounds.Snd_Win_DeviceDisconnect = $@"{SysPaths.Windows}\media\Windows Hardware Remove.wav";
            Sounds.Snd_Win_DeviceFail = $@"{SysPaths.Windows}\media\Windows Hardware Fail.wav";
            Sounds.Snd_Win_FaxBeep = $@"{SysPaths.Windows}\media\Windows Notify Email.wav";
            Sounds.Snd_Win_LowBatteryAlarm = $@"{SysPaths.Windows}\media\Windows Background.wav";
            Sounds.Snd_Win_MailBeep = $@"{SysPaths.Windows}\media\Windows Notify Email.wav";
            Sounds.Snd_Win_Maximize = string.Empty;
            Sounds.Snd_Win_MenuCommand = string.Empty;
            Sounds.Snd_Win_MenuPopup = string.Empty;
            Sounds.Snd_Win_MessageNudge = $@"{SysPaths.Windows}\media\Windows Message Nudge.wav";
            Sounds.Snd_Win_Minimize = string.Empty;
            Sounds.Snd_Win_Notification_Default = $@"{SysPaths.Windows}\media\Windows Notify System Generic.wav";
            Sounds.Snd_Win_Notification_IM = $@"{SysPaths.Windows}\media\Windows Notify Messaging.wav";
            Sounds.Snd_Win_Notification_Looping_Alarm = $@"{SysPaths.Windows}\media\Alarm01.wav";
            Sounds.Snd_Win_Notification_Looping_Alarm2 = $@"{SysPaths.Windows}\media\Alarm02.wav";
            Sounds.Snd_Win_Notification_Looping_Alarm3 = $@"{SysPaths.Windows}\media\Alarm03.wav";
            Sounds.Snd_Win_Notification_Looping_Alarm4 = $@"{SysPaths.Windows}\media\Alarm04.wav";
            Sounds.Snd_Win_Notification_Looping_Alarm5 = $@"{SysPaths.Windows}\media\Alarm05.wav";
            Sounds.Snd_Win_Notification_Looping_Alarm6 = $@"{SysPaths.Windows}\media\Alarm06.wav";
            Sounds.Snd_Win_Notification_Looping_Alarm7 = $@"{SysPaths.Windows}\media\Alarm07.wav";
            Sounds.Snd_Win_Notification_Looping_Alarm8 = $@"{SysPaths.Windows}\media\Alarm08.wav";
            Sounds.Snd_Win_Notification_Looping_Alarm9 = $@"{SysPaths.Windows}\media\Alarm09.wav";
            Sounds.Snd_Win_Notification_Looping_Alarm10 = $@"{SysPaths.Windows}\media\Alarm10.wav";
            Sounds.Snd_Win_Notification_Looping_Call = $@"{SysPaths.Windows}\media\Ring01.wav";
            Sounds.Snd_Win_Notification_Looping_Call2 = $@"{SysPaths.Windows}\media\Ring02.wav";
            Sounds.Snd_Win_Notification_Looping_Call3 = $@"{SysPaths.Windows}\media\Ring03.wav";
            Sounds.Snd_Win_Notification_Looping_Call4 = $@"{SysPaths.Windows}\media\Ring04.wav";
            Sounds.Snd_Win_Notification_Looping_Call5 = $@"{SysPaths.Windows}\media\Ring05.wav";
            Sounds.Snd_Win_Notification_Looping_Call6 = $@"{SysPaths.Windows}\media\Ring06.wav";
            Sounds.Snd_Win_Notification_Looping_Call7 = $@"{SysPaths.Windows}\media\Ring07.wav";
            Sounds.Snd_Win_Notification_Looping_Call8 = $@"{SysPaths.Windows}\media\Ring08.wav";
            Sounds.Snd_Win_Notification_Looping_Call9 = $@"{SysPaths.Windows}\media\Ring09.wav";
            Sounds.Snd_Win_Notification_Looping_Call10 = $@"{SysPaths.Windows}\media\Ring10.wav";
            Sounds.Snd_Win_Notification_Mail = $@"{SysPaths.Windows}\media\Windows Notify Email.wav";
            Sounds.Snd_Win_Notification_Proximity = $@"{SysPaths.Windows}\media\Windows Proximity Notification.wav";
            Sounds.Snd_Win_Notification_Reminder = $@"{SysPaths.Windows}\media\Windows Notify Calendar.wav";
            Sounds.Snd_Win_Notification_SMS = $@"{SysPaths.Windows}\media\Windows Notify Messaging.wav";
            Sounds.Snd_Win_Open = string.Empty;
            Sounds.Snd_Win_PrintComplete = string.Empty;
            Sounds.Snd_Win_ProximityConnection = $@"{SysPaths.Windows}\media\Windows Proximity Connection.wav";
            Sounds.Snd_Win_RestoreDown = string.Empty;
            Sounds.Snd_Win_RestoreUp = string.Empty;
            Sounds.Snd_Win_ShowBand = string.Empty;
            Sounds.Snd_Win_SystemAsterisk = $@"{SysPaths.Windows}\media\Windows Background.wav";
            Sounds.Snd_Win_SystemExclamation = $@"{SysPaths.Windows}\media\Windows Background.wav";
            Sounds.Snd_Win_SystemExit = string.Empty;
            Sounds.Snd_Win_SystemStart = string.Empty;
            Sounds.Snd_Win_SystemHand = $@"{SysPaths.Windows}\media\Windows Foreground.wav";
            Sounds.Snd_Win_SystemNotification = $@"{SysPaths.Windows}\media\Windows Background.wav";
            Sounds.Snd_Win_SystemQuestion = string.Empty;
            Sounds.Snd_Win_WindowsLogoff = string.Empty;
            Sounds.Snd_Win_WindowsLogon = string.Empty;
            Sounds.Snd_Win_WindowsUAC = $@"{SysPaths.Windows}\media\Windows User Account Control.wav";
            Sounds.Snd_Win_WindowsUnlock = $@"{SysPaths.Windows}\media\Windows Unlock.wav";
            Sounds.Snd_Explorer_ActivatingDocument = string.Empty;
            Sounds.Snd_Explorer_BlockedPopup = $@"{SysPaths.Windows}\media\Windows Pop-up Blocked.wav";
            Sounds.Snd_Explorer_EmptyRecycleBin = string.Empty;
            Sounds.Snd_Explorer_FeedDiscovered = $@"{SysPaths.Windows}\media\Windows Feed Discovered.wav";
            Sounds.Snd_Explorer_MoveMenuItem = string.Empty;
            Sounds.Snd_Explorer_Navigating = string.Empty;
            Sounds.Snd_Explorer_SecurityBand = $@"{SysPaths.Windows}\media\Windows Information Bar.wav";
            Sounds.Snd_Explorer_SearchProviderDiscovered = string.Empty;
            Sounds.Snd_Explorer_FaxError = string.Empty;
            Sounds.Snd_Explorer_FaxLineRings = string.Empty;
            Sounds.Snd_Explorer_FaxNew = string.Empty;
            Sounds.Snd_Explorer_FaxSent = string.Empty;
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