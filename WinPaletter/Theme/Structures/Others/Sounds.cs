using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing Windows sounds
    /// </summary>
    public struct Sounds : ICloneable, IDisposable
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled;

        /// <summary>Windows default beep WAV sound file path</summary>
        public string Snd_Win_Default;

        /// <summary>Windows application GP fault WAV sound file path<br>???</br></summary>
        public string Snd_Win_AppGPFault;

        /// <summary>Windows CC select WAV sound file path<br>???</br></summary>
        public string Snd_Win_CCSelect;

        /// <summary>Windows theme change WAV sound file path</summary>
        public string Snd_Win_ChangeTheme;

        /// <summary>Window close WAV sound file path</summary>
        public string Snd_Win_Close;

        /// <summary>Critical battery alarm WAV sound file path</summary>
        public string Snd_Win_CriticalBatteryAlarm;

        /// <summary>Device (USB) connection WAV sound file path</summary>
        public string Snd_Win_DeviceConnect;

        /// <summary>Device (USB) disconnection WAV sound file path</summary>
        public string Snd_Win_DeviceDisconnect;

        /// <summary>Device (USB) failure WAV sound file path</summary>
        public string Snd_Win_DeviceFail;

        /// <summary>Fax beep WAV sound file path</summary>
        public string Snd_Win_FaxBeep;

        /// <summary>Low battery alarm WAV sound file path</summary>
        public string Snd_Win_LowBatteryAlarm;

        /// <summary>Mail received beep WAV sound file path</summary>
        public string Snd_Win_MailBeep;

        /// <summary>Window maximize WAV sound file path</summary>
        public string Snd_Win_Maximize;

        /// <summary>Menu item click WAV sound file path</summary>
        public string Snd_Win_MenuCommand;

        /// <summary>Menu popup WAV sound file path</summary>
        public string Snd_Win_MenuPopup;

        /// <summary>Message nudge WAV sound file path</summary>
        public string Snd_Win_MessageNudge;

        /// <summary>Window minimize WAV sound file path</summary>
        public string Snd_Win_Minimize;

        /// <summary>Windows notification WAV sound file path</summary>
        public string Snd_Win_Notification_Default;

        /// <summary>Instant message notification WAV sound file path</summary>
        public string Snd_Win_Notification_IM;

        /// <summary>Windows 8 (and later) alarm 0 WAV sound file path</summary>
        public string Snd_Win_Notification_Looping_Alarm;

        /// <summary>Windows 8 (and later) alarm 10 WAV sound file path</summary>
        public string Snd_Win_Notification_Looping_Alarm10;

        /// <summary>Windows 8 (and later) alarm 2 WAV sound file path</summary>
        public string Snd_Win_Notification_Looping_Alarm2;

        /// <summary>Windows 8 (and later) alarm 3 WAV sound file path</summary>
        public string Snd_Win_Notification_Looping_Alarm3;

        /// <summary>Windows 8 (and later) alarm 4 WAV sound file path</summary>
        public string Snd_Win_Notification_Looping_Alarm4;

        /// <summary>Windows 8 (and later) alarm 5 WAV sound file path</summary>
        public string Snd_Win_Notification_Looping_Alarm5;

        /// <summary>Windows 8 (and later) alarm 6 WAV sound file path</summary>
        public string Snd_Win_Notification_Looping_Alarm6;

        /// <summary>Windows 8 (and later) alarm 7 WAV sound file path</summary>
        public string Snd_Win_Notification_Looping_Alarm7;

        /// <summary>Windows 8 (and later) alarm 8 WAV sound file path</summary>
        public string Snd_Win_Notification_Looping_Alarm8;

        /// <summary>Windows 8 (and later) alarm 9 WAV sound file path</summary>
        public string Snd_Win_Notification_Looping_Alarm9;

        /// <summary>Windows 8 (and later) ring tone 0 WAV sound file path</summary>
        public string Snd_Win_Notification_Looping_Call;

        /// <summary>Windows 8 (and later) ring tone 10 WAV sound file path</summary>
        public string Snd_Win_Notification_Looping_Call10;

        /// <summary>Windows 8 (and later) ring tone 2 WAV sound file path</summary>
        public string Snd_Win_Notification_Looping_Call2;

        /// <summary>Windows 8 (and later) ring tone 3 WAV sound file path</summary>
        public string Snd_Win_Notification_Looping_Call3;

        /// <summary>Windows 8 (and later) ring tone 4 WAV sound file path</summary>
        public string Snd_Win_Notification_Looping_Call4;

        /// <summary>Windows 8 (and later) ring tone 5 WAV sound file path</summary>
        public string Snd_Win_Notification_Looping_Call5;

        /// <summary>Windows 8 (and later) ring tone 6 WAV sound file path</summary>
        public string Snd_Win_Notification_Looping_Call6;

        /// <summary>Windows 8 (and later) ring tone 7 WAV sound file path</summary>
        public string Snd_Win_Notification_Looping_Call7;

        /// <summary>Windows 8 (and later) ring tone 8 WAV sound file path</summary>
        public string Snd_Win_Notification_Looping_Call8;

        /// <summary>Windows 8 (and later) ring tone 9 WAV sound file path</summary>
        public string Snd_Win_Notification_Looping_Call9;

        /// <summary>Mail notification WAV sound file path</summary>
        public string Snd_Win_Notification_Mail;

        /// <summary>Proximity notification WAV sound file path</summary>
        public string Snd_Win_Notification_Proximity;

        /// <summary>Notification reminder WAV sound file path</summary>
        public string Snd_Win_Notification_Reminder;

        /// <summary>SMS notification WAV sound file path</summary>
        public string Snd_Win_Notification_SMS;

        /// <summary>Application open WAV sound file path</summary>
        public string Snd_Win_Open;

        /// <summary>Print job completed WAV sound file path</summary>
        public string Snd_Win_PrintComplete;

        /// <summary>Proximity connection WAV sound file path</summary>
        public string Snd_Win_ProximityConnection;

        /// <summary>Window restore down WAV sound file path</summary>
        public string Snd_Win_RestoreDown;

        /// <summary>Window restore up WAV sound file path</summary>
        public string Snd_Win_RestoreUp;

        /// <summary>Windows Explorer/Internet Explorer band showed WAV sound file path</summary>
        public string Snd_Win_ShowBand;

        /// <summary>System asterisk WAV sound file path</summary>
        public string Snd_Win_SystemAsterisk;

        /// <summary>Exclamation WAV sound file path</summary>
        public string Snd_Win_SystemExclamation;

        /// <summary>Windows shutdown sound (not working for Windows 8 and later)</summary>
        public string Snd_Win_SystemExit;

        /// <summary>Windows start up sound (not working for Windows Vista and later)</summary>
        public string Snd_Win_SystemStart;

        /// <summary>
        /// Syntax or path of WAV file that will be patched in imageres.dll to be used as Windows start up sound
        /// <br></br>- Targeting Windows Vista and later
        /// <code>
        /// Syntaxes:
        ///  file_path:          string path of WAV file that will be patched in imageres.dll
        ///  DEFAULT:            it will restore default sound from WinPaletter backup
        ///  CURRENT:            will do nothing
        ///  Empty string (""):  will disable startup sound
        /// </code>
        /// </summary>
        public string Snd_Imageres_SystemStart;

        /// <summary>Hyperlink clicked WAV sound file path</summary>
        public string Snd_Win_SystemHand;

        /// <summary>Information message WAV sound file path</summary>
        public string Snd_Win_SystemNotification;

        /// <summary>Question message WAV sound file path</summary>
        public string Snd_Win_SystemQuestion;

        /// <summary>Windows logoff WAV sound file path (not working for Windows 8 and later)</summary>
        public string Snd_Win_WindowsLogoff;

        /// <summary>Windows logon WAV sound file path (not working for Windows 8 and later)</summary>
        public string Snd_Win_WindowsLogon;

        /// <summary>User accound control (UAC) dialog WAV sound file path (for Windows Vista and later)</summary>
        public string Snd_Win_WindowsUAC;

        /// <summary>Windows unlock WAV sound file path (targeting Windows 8 and later, but not working)</summary>
        public string Snd_Win_WindowsUnlock;

        /// <summary>Activating document WAV sound file path</summary>
        public string Snd_Explorer_ActivatingDocument;

        /// <summary>Popup blocked WAV sound file path</summary>
        public string Snd_Explorer_BlockedPopup;

        /// <summary>Recycle bin eptied WAV sound file path</summary>
        public string Snd_Explorer_EmptyRecycleBin;

        /// <summary>Feed discovered WAV sound file path</summary>
        public string Snd_Explorer_FeedDiscovered;

        /// <summary>Menu item moved WAV sound file path</summary>
        public string Snd_Explorer_MoveMenuItem;

        /// <summary>Folders navigation WAV sound file path</summary>
        public string Snd_Explorer_Navigating;

        /// <summary>Security band appeared WAV sound file path</summary>
        public string Snd_Explorer_SecurityBand;

        /// <summary>Search provider discovered WAV sound file path</summary>
        public string Snd_Explorer_SearchProviderDiscovered;

        /// <summary>Fax error WAV sound file path</summary>
        public string Snd_Explorer_FaxError;

        /// <summary>Fax line ringing WAV sound file path</summary>
        public string Snd_Explorer_FaxLineRings;

        /// <summary>New fax received WAV sound file path</summary>
        public string Snd_Explorer_FaxNew;

        /// <summary>Fax sent WAV sound file path</summary>
        public string Snd_Explorer_FaxSent;

        /// <summary>NetMeeting application (Windows XP): person joins WAV sound file path</summary>
        public string Snd_NetMeeting_PersonJoins;

        /// <summary>NetMeeting application (Windows XP): person leaved WAV sound file path</summary>
        public string Snd_NetMeeting_PersonLeaves;

        /// <summary>NetMeeting application (Windows XP): receive call WAV sound file path</summary>
        public string Snd_NetMeeting_ReceiveCall;

        /// <summary>NetMeeting application (Windows XP): receive request to join WAV sound file path</summary>
        public string Snd_NetMeeting_ReceiveRequestToJoin;

        /// <summary>Speech recognition (Windows Vista and later): disambiguation numbers WAV sound file path</summary>
        public string Snd_SpeechRec_DisNumbersSound;

        /// <summary>Speech recognition (Windows Vista and later): Hub off WAV sound file path</summary>
        public string Snd_SpeechRec_HubOffSound;

        /// <summary>Speech recognition (Windows Vista and later): Hub on WAV sound file path</summary>
        public string Snd_SpeechRec_HubOnSound;

        /// <summary>Speech recognition (Windows Vista and later): Hub sleep WAV sound file path</summary>
        public string Snd_SpeechRec_HubSleepSound;

        /// <summary>Speech recognition (Windows Vista and later): Misrecognition WAV sound file path</summary>
        public string Snd_SpeechRec_MisrecoSound;

        /// <summary>Speech recognition (Windows Vista and later): disambiguation panel WAV sound file path</summary>
        public string Snd_SpeechRec_PanelSound;

        /// <summary>Windows unlock WAV sound file path 
        /// <br></br>- Targeting Windows 8 and later
        /// <br></br>- Deflected by service that listens to Windows events (WinPaletter.SysEventsSounds)
        /// </summary>
        public string Snd_Win_WindowsLock;

        /// <summary>Charger connected WAV sound file path 
        /// <br><b><i>(!) It is not an official sound in Windows</i></b></br>
        /// <br></br>- Deflected by service that listens to Windows events (WinPaletter.SysEventsSounds)
        /// </summary>
        public string Snd_ChargerConnected;

        /// <summary>Charger disconnected WAV sound file path 
        /// <br><b><i>(!) It is not an official sound in Windows</i></b></br>
        /// <br></br>- Deflected by service that listens to Windows events (WinPaletter.SysEventsSounds)
        /// </summary>
        public string Snd_ChargerDisconnected;

        /// <summary>
        /// Loads Sounds data from registry
        /// </summary>
        /// <param name="default">Default Sounds data structure</param>
        public void Load(Sounds @default)
        {
            Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "", @default.Enabled));
            Snd_Imageres_SystemStart = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Imageres.dll_Startup", @default.Snd_Imageres_SystemStart).ToString();
            Snd_ChargerConnected = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_ChargerConnected", @default.Snd_ChargerConnected).ToString();
            Snd_ChargerDisconnected = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_ChargerDisconnected", @default.Snd_ChargerDisconnected).ToString();
            Snd_Win_WindowsLock = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_WindowsLock", @default.Snd_Win_WindowsLock).ToString();

            string Scope_Win = @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\.Default\{0}\.Current";
            Snd_Win_Default = GetReg(string.Format(Scope_Win, ".Default"), "", @default.Snd_Win_Default).ToString();
            Snd_Win_AppGPFault = GetReg(string.Format(Scope_Win, "AppGPFault"), "", @default.Snd_Win_AppGPFault).ToString();
            Snd_Win_CCSelect = GetReg(string.Format(Scope_Win, "CCSelect"), "", @default.Snd_Win_CCSelect).ToString();
            Snd_Win_ChangeTheme = GetReg(string.Format(Scope_Win, "ChangeTheme"), "", @default.Snd_Win_ChangeTheme).ToString();
            Snd_Win_Close = GetReg(string.Format(Scope_Win, "Close"), "", @default.Snd_Win_Close).ToString();
            Snd_Win_CriticalBatteryAlarm = GetReg(string.Format(Scope_Win, "CriticalBatteryAlarm"), "", @default.Snd_Win_CriticalBatteryAlarm).ToString();
            Snd_Win_DeviceConnect = GetReg(string.Format(Scope_Win, "DeviceConnect"), "", @default.Snd_Win_DeviceConnect).ToString();
            Snd_Win_DeviceDisconnect = GetReg(string.Format(Scope_Win, "DeviceDisconnect"), "", @default.Snd_Win_DeviceDisconnect).ToString();
            Snd_Win_DeviceFail = GetReg(string.Format(Scope_Win, "DeviceFail"), "", @default.Snd_Win_DeviceFail).ToString();
            Snd_Win_FaxBeep = GetReg(string.Format(Scope_Win, "FaxBeep"), "", @default.Snd_Win_FaxBeep).ToString();
            Snd_Win_LowBatteryAlarm = GetReg(string.Format(Scope_Win, "LowBatteryAlarm"), "", @default.Snd_Win_LowBatteryAlarm).ToString();
            Snd_Win_MailBeep = GetReg(string.Format(Scope_Win, "MailBeep"), "", @default.Snd_Win_MailBeep).ToString();
            Snd_Win_Maximize = GetReg(string.Format(Scope_Win, "Maximize"), "", @default.Snd_Win_Maximize).ToString();
            Snd_Win_MenuCommand = GetReg(string.Format(Scope_Win, "MenuCommand"), "", @default.Snd_Win_MenuCommand).ToString();
            Snd_Win_MenuPopup = GetReg(string.Format(Scope_Win, "MenuPopup"), "", @default.Snd_Win_MenuPopup).ToString();
            Snd_Win_MessageNudge = GetReg(string.Format(Scope_Win, "MessageNudge"), "", @default.Snd_Win_MessageNudge).ToString();
            Snd_Win_Minimize = GetReg(string.Format(Scope_Win, "Minimize"), "", @default.Snd_Win_Minimize).ToString();
            Snd_Win_Notification_Default = GetReg(string.Format(Scope_Win, "Notification.Default"), "", @default.Snd_Win_Notification_Default).ToString();
            Snd_Win_Notification_IM = GetReg(string.Format(Scope_Win, "Notification.IM"), "", @default.Snd_Win_Notification_IM).ToString();
            Snd_Win_Notification_Looping_Alarm = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm"), "", @default.Snd_Win_Notification_Looping_Alarm).ToString();
            Snd_Win_Notification_Looping_Alarm2 = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm2"), "", @default.Snd_Win_Notification_Looping_Alarm2).ToString();
            Snd_Win_Notification_Looping_Alarm3 = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm3"), "", @default.Snd_Win_Notification_Looping_Alarm3).ToString();
            Snd_Win_Notification_Looping_Alarm4 = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm4"), "", @default.Snd_Win_Notification_Looping_Alarm4).ToString();
            Snd_Win_Notification_Looping_Alarm5 = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm5"), "", @default.Snd_Win_Notification_Looping_Alarm5).ToString();
            Snd_Win_Notification_Looping_Alarm6 = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm6"), "", @default.Snd_Win_Notification_Looping_Alarm6).ToString();
            Snd_Win_Notification_Looping_Alarm7 = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm7"), "", @default.Snd_Win_Notification_Looping_Alarm7).ToString();
            Snd_Win_Notification_Looping_Alarm8 = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm8"), "", @default.Snd_Win_Notification_Looping_Alarm8).ToString();
            Snd_Win_Notification_Looping_Alarm9 = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm9"), "", @default.Snd_Win_Notification_Looping_Alarm9).ToString();
            Snd_Win_Notification_Looping_Alarm10 = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm10"), "", @default.Snd_Win_Notification_Looping_Alarm10).ToString();
            Snd_Win_Notification_Looping_Call = GetReg(string.Format(Scope_Win, "Notification.Looping.Call"), "", @default.Snd_Win_Notification_Looping_Call).ToString();
            Snd_Win_Notification_Looping_Call2 = GetReg(string.Format(Scope_Win, "Notification.Looping.Call2"), "", @default.Snd_Win_Notification_Looping_Call2).ToString();
            Snd_Win_Notification_Looping_Call3 = GetReg(string.Format(Scope_Win, "Notification.Looping.Call3"), "", @default.Snd_Win_Notification_Looping_Call3).ToString();
            Snd_Win_Notification_Looping_Call4 = GetReg(string.Format(Scope_Win, "Notification.Looping.Call4"), "", @default.Snd_Win_Notification_Looping_Call4).ToString();
            Snd_Win_Notification_Looping_Call5 = GetReg(string.Format(Scope_Win, "Notification.Looping.Call5"), "", @default.Snd_Win_Notification_Looping_Call5).ToString();
            Snd_Win_Notification_Looping_Call6 = GetReg(string.Format(Scope_Win, "Notification.Looping.Call6"), "", @default.Snd_Win_Notification_Looping_Call6).ToString();
            Snd_Win_Notification_Looping_Call7 = GetReg(string.Format(Scope_Win, "Notification.Looping.Call7"), "", @default.Snd_Win_Notification_Looping_Call7).ToString();
            Snd_Win_Notification_Looping_Call8 = GetReg(string.Format(Scope_Win, "Notification.Looping.Call8"), "", @default.Snd_Win_Notification_Looping_Call8).ToString();
            Snd_Win_Notification_Looping_Call9 = GetReg(string.Format(Scope_Win, "Notification.Looping.Call9"), "", @default.Snd_Win_Notification_Looping_Call9).ToString();
            Snd_Win_Notification_Looping_Call10 = GetReg(string.Format(Scope_Win, "Notification.Looping.Call10"), "", @default.Snd_Win_Notification_Looping_Call10).ToString();
            Snd_Win_Notification_Mail = GetReg(string.Format(Scope_Win, "Notification.Mail"), "", @default.Snd_Win_Notification_Mail).ToString();
            Snd_Win_Notification_Proximity = GetReg(string.Format(Scope_Win, "Notification.Proximity"), "", @default.Snd_Win_Notification_Proximity).ToString();
            Snd_Win_Notification_Reminder = GetReg(string.Format(Scope_Win, "Notification.Reminder"), "", @default.Snd_Win_Notification_Reminder).ToString();
            Snd_Win_Notification_SMS = GetReg(string.Format(Scope_Win, "Notification.SMS"), "", @default.Snd_Win_Notification_SMS).ToString();
            Snd_Win_Open = GetReg(string.Format(Scope_Win, "Open"), "", @default.Snd_Win_Open).ToString();
            Snd_Win_PrintComplete = GetReg(string.Format(Scope_Win, "PrintComplete"), "", @default.Snd_Win_PrintComplete).ToString();
            Snd_Win_ProximityConnection = GetReg(string.Format(Scope_Win, "ProximityConnection"), "", @default.Snd_Win_ProximityConnection).ToString();
            Snd_Win_RestoreDown = GetReg(string.Format(Scope_Win, "RestoreDown"), "", @default.Snd_Win_RestoreDown).ToString();
            Snd_Win_RestoreUp = GetReg(string.Format(Scope_Win, "RestoreUp"), "", @default.Snd_Win_RestoreUp).ToString();
            Snd_Win_ShowBand = GetReg(string.Format(Scope_Win, "ShowBand"), "", @default.Snd_Win_ShowBand).ToString();
            Snd_Win_SystemAsterisk = GetReg(string.Format(Scope_Win, "SystemAsterisk"), "", @default.Snd_Win_SystemAsterisk).ToString();
            Snd_Win_SystemExclamation = GetReg(string.Format(Scope_Win, "SystemExclamation"), "", @default.Snd_Win_SystemExclamation).ToString();
            Snd_Win_SystemExit = GetReg(string.Format(Scope_Win, "SystemExit"), "", @default.Snd_Win_SystemExit).ToString();
            Snd_Win_SystemStart = GetReg(string.Format(Scope_Win, "SystemStart"), "", @default.Snd_Win_SystemStart).ToString();
            Snd_Win_SystemHand = GetReg(string.Format(Scope_Win, "SystemHand"), "", @default.Snd_Win_SystemHand).ToString();
            Snd_Win_SystemNotification = GetReg(string.Format(Scope_Win, "SystemNotification"), "", @default.Snd_Win_SystemNotification).ToString();
            Snd_Win_SystemQuestion = GetReg(string.Format(Scope_Win, "SystemQuestion"), "", @default.Snd_Win_SystemQuestion).ToString();
            Snd_Win_WindowsLogoff = GetReg(string.Format(Scope_Win, "WindowsLogoff"), "", @default.Snd_Win_WindowsLogoff).ToString();
            Snd_Win_WindowsLogon = GetReg(string.Format(Scope_Win, "WindowsLogon"), "", @default.Snd_Win_WindowsLogon).ToString();
            Snd_Win_WindowsUAC = GetReg(string.Format(Scope_Win, "WindowsUAC"), "", @default.Snd_Win_WindowsUAC).ToString();
            Snd_Win_WindowsUnlock = GetReg(string.Format(Scope_Win, "WindowsUnlock"), "", @default.Snd_Win_WindowsUnlock).ToString();

            string Scope_Explorer = @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\Explorer\{0}\.Current";
            Snd_Explorer_ActivatingDocument = GetReg(string.Format(Scope_Explorer, "ActivatingDocument"), "", @default.Snd_Explorer_ActivatingDocument).ToString();
            Snd_Explorer_BlockedPopup = GetReg(string.Format(Scope_Explorer, "BlockedPopup"), "", @default.Snd_Explorer_BlockedPopup).ToString();
            Snd_Explorer_EmptyRecycleBin = GetReg(string.Format(Scope_Explorer, "EmptyRecycleBin"), "", @default.Snd_Explorer_EmptyRecycleBin).ToString();
            Snd_Explorer_FeedDiscovered = GetReg(string.Format(Scope_Explorer, "FeedDiscovered"), "", @default.Snd_Explorer_FeedDiscovered).ToString();
            Snd_Explorer_MoveMenuItem = GetReg(string.Format(Scope_Explorer, "MoveMenuItem"), "", @default.Snd_Explorer_MoveMenuItem).ToString();
            Snd_Explorer_Navigating = GetReg(string.Format(Scope_Explorer, "Navigating"), "", @default.Snd_Explorer_Navigating).ToString();
            Snd_Explorer_SecurityBand = GetReg(string.Format(Scope_Explorer, "SecurityBand"), "", @default.Snd_Explorer_SecurityBand).ToString();
            Snd_Explorer_SearchProviderDiscovered = GetReg(string.Format(Scope_Explorer, "SearchProviderDiscovered"), "", @default.Snd_Explorer_SearchProviderDiscovered).ToString();
            Snd_Explorer_FaxError = GetReg(string.Format(Scope_Explorer, "FaxError"), "", @default.Snd_Explorer_FaxError).ToString();
            Snd_Explorer_FaxLineRings = GetReg(string.Format(Scope_Explorer, "FaxLineRings"), "", @default.Snd_Explorer_FaxLineRings).ToString();
            Snd_Explorer_FaxNew = GetReg(string.Format(Scope_Explorer, "FaxNew"), "", @default.Snd_Explorer_FaxNew).ToString();
            Snd_Explorer_FaxSent = GetReg(string.Format(Scope_Explorer, "FaxSent"), "", @default.Snd_Explorer_FaxSent).ToString();

            string Scope_NetMeeting = @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\Conf\{0}\.Current";
            Snd_NetMeeting_PersonJoins = GetReg(string.Format(Scope_NetMeeting, "Person Joins"), "", @default.Snd_NetMeeting_PersonJoins).ToString();
            Snd_NetMeeting_PersonLeaves = GetReg(string.Format(Scope_NetMeeting, "Person Leaves"), "", @default.Snd_NetMeeting_PersonLeaves).ToString();
            Snd_NetMeeting_ReceiveCall = GetReg(string.Format(Scope_NetMeeting, "Receive Call"), "", @default.Snd_NetMeeting_ReceiveCall).ToString();
            Snd_NetMeeting_ReceiveRequestToJoin = GetReg(string.Format(Scope_NetMeeting, "Receive Request to Join"), "", @default.Snd_NetMeeting_ReceiveRequestToJoin).ToString();

            string Scope_SpeechRec = @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\sapisvr\{0}\.current";
            Snd_SpeechRec_DisNumbersSound = GetReg(string.Format(Scope_SpeechRec, "DisNumbersSound"), "", @default.Snd_SpeechRec_DisNumbersSound).ToString();
            Snd_SpeechRec_HubOffSound = GetReg(string.Format(Scope_SpeechRec, "HubOffSound"), "", @default.Snd_SpeechRec_HubOffSound).ToString();
            Snd_SpeechRec_HubOnSound = GetReg(string.Format(Scope_SpeechRec, "HubOnSound"), "", @default.Snd_SpeechRec_HubOnSound).ToString();
            Snd_SpeechRec_HubSleepSound = GetReg(string.Format(Scope_SpeechRec, "HubSleepSound"), "", @default.Snd_SpeechRec_HubSleepSound).ToString();
            Snd_SpeechRec_MisrecoSound = GetReg(string.Format(Scope_SpeechRec, "MisrecoSound"), "", @default.Snd_SpeechRec_MisrecoSound).ToString();
            Snd_SpeechRec_PanelSound = GetReg(string.Format(Scope_SpeechRec, "PanelSound"), "", @default.Snd_SpeechRec_PanelSound).ToString();

        }

        /// <summary>
        /// Saves Sounds data into registry
        /// </summary>
        /// <param name="TreeView">TreeView used as theme log</param>
        public void Apply(TreeView TreeView = null)
        {
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "", Enabled);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Imageres.dll_Startup", Snd_Imageres_SystemStart, RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_ChargerConnected", Snd_ChargerConnected, RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_ChargerDisconnected", Snd_ChargerDisconnected, RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_WindowsLock", Snd_Win_WindowsLock, RegistryValueKind.String);

            if (System.IO.File.Exists(PathsExt.SysEventsSounds_Local_INI)) { System.IO.File.Delete(PathsExt.SysEventsSounds_Local_INI); }
            if (System.IO.File.Exists(PathsExt.SysEventsSounds_Global_INI)) { System.IO.File.Delete(PathsExt.SysEventsSounds_Global_INI); }

            if (Enabled)
            {
                string[] destination_StartupSnd = new[] { @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\System", @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\BootAnimation" };

                if (string.IsNullOrWhiteSpace(Snd_Imageres_SystemStart))
                {
                    EditReg_CMD(TreeView, destination_StartupSnd[0], "DisableStartupSound", 1);
                    EditReg_CMD(TreeView, destination_StartupSnd[1], "DisableStartupSound", 1);
                }

                else if (File.Exists(Snd_Imageres_SystemStart))
                {
                    EditReg_CMD(TreeView, destination_StartupSnd[0], "DisableStartupSound", 0);
                    EditReg_CMD(TreeView, destination_StartupSnd[1], "DisableStartupSound", 0);
                }

                else if (Snd_Imageres_SystemStart.Trim().ToUpper() == "DEFAULT")
                {
                    EditReg_CMD(TreeView, destination_StartupSnd[0], "DisableStartupSound", 0);
                    EditReg_CMD(TreeView, destination_StartupSnd[1], "DisableStartupSound", 0);
                }

                else if (!(Snd_Imageres_SystemStart.Trim().ToUpper() == "CURRENT"))
                {
                    EditReg_CMD(TreeView, destination_StartupSnd[0], "DisableStartupSound", (!OS.W11) ? 1 : 0);
                    EditReg_CMD(TreeView, destination_StartupSnd[1], "DisableStartupSound", (!OS.W11) ? 1 : 0);
                }

                else
                {
                    EditReg_CMD(TreeView, destination_StartupSnd[0], "DisableStartupSound", 1);
                    EditReg_CMD(TreeView, destination_StartupSnd[1], "DisableStartupSound", 1);
                }

                if (!OS.WXP)
                {
                    if (File.Exists(Snd_Imageres_SystemStart) && Path.GetExtension(Snd_Imageres_SystemStart).ToUpper() == ".WAV")
                    {

                        byte[] CurrentSoundBytes = PE.GetResource(PathsExt.imageres, "WAVE", OS.WVista ? 5051 : 5080);
                        byte[] TargetSoundBytes = File.ReadAllBytes(Snd_Imageres_SystemStart);

                        if (!CurrentSoundBytes.Equals(TargetSoundBytes))
                        {
                            PE.ReplaceResource(TreeView, PathsExt.imageres, "WAVE", OS.WVista ? 5051 : 5080, TargetSoundBytes);
                        }
                    }

                    else if (Snd_Imageres_SystemStart.Trim().ToUpper() == "DEFAULT")
                    {
                        byte[] CurrentSoundBytes = PE.GetResource(PathsExt.imageres, "WAVE", OS.WVista ? 5051 : 5080);
                        byte[] OriginalSoundBytes = File.ReadAllBytes(PathsExt.appData + @"\WindowsStartup_Backup.wav");

                        if (!CurrentSoundBytes.Equals(OriginalSoundBytes))
                        {
                            PE.ReplaceResource(TreeView, PathsExt.imageres, "WAVE", OS.WVista ? 5051 : 5080, OriginalSoundBytes);
                        }

                        if (Program.Settings.ThemeApplyingBehavior.SFC_on_restoring_StartupSound)
                            SFC(PathsExt.imageres);
                    }

                }

                if (OS.W8 | OS.W81 | OS.W10 | OS.W11 | OS.W12)
                {
                    Tasks.Delete(Tasks.TaskType.Shutdown, TreeView);
                    Tasks.Delete(Tasks.TaskType.Logoff, TreeView);
                    Tasks.Delete(Tasks.TaskType.Logon, TreeView);
                    Tasks.Delete(Tasks.TaskType.Unlock, TreeView);
                    Tasks.Delete(Tasks.TaskType.ChargerConnected, TreeView);
                }

                string[] Scope_Win = new[] { @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\.Default\{0}\.Current", @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\.Default\{0}\.Modified" };
                string[] Scope_Explorer = new[] { @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\Explorer\{0}\.Current", @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\Explorer\{0}\.Modified" };
                string[] Scope_SpeechRec = new[] { @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\sapisvr\{0}\.Current", @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\sapisvr\{0}\.Modified" };
                string[] Scope_NetMeeting = new[] { @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\Conf\{0}\.Current", @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\Conf\{0}\.Modified" };

                foreach (string Scope in Scope_Win)
                {
                    EditReg(TreeView, string.Format(Scope, ".Default"), "", Snd_Win_Default, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "AppGPFault"), "", Snd_Win_AppGPFault, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "CCSelect"), "", Snd_Win_CCSelect, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "ChangeTheme"), "", Snd_Win_ChangeTheme, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Close"), "", Snd_Win_Close, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "CriticalBatteryAlarm"), "", Snd_Win_CriticalBatteryAlarm, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "DeviceConnect"), "", Snd_Win_DeviceConnect, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "DeviceDisconnect"), "", Snd_Win_DeviceDisconnect, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "DeviceFail"), "", Snd_Win_DeviceFail, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "FaxBeep"), "", Snd_Win_FaxBeep, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "LowBatteryAlarm"), "", Snd_Win_LowBatteryAlarm, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "MailBeep"), "", Snd_Win_MailBeep, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Maximize"), "", Snd_Win_Maximize, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "MenuCommand"), "", Snd_Win_MenuCommand, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "MenuPopup"), "", Snd_Win_MenuPopup, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "MessageNudge"), "", Snd_Win_MessageNudge, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Minimize"), "", Snd_Win_Minimize, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Notification.Default"), "", Snd_Win_Notification_Default, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Notification.IM"), "", Snd_Win_Notification_IM, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Notification.Looping.Alarm"), "", Snd_Win_Notification_Looping_Alarm, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Notification.Looping.Alarm2"), "", Snd_Win_Notification_Looping_Alarm2, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Notification.Looping.Alarm3"), "", Snd_Win_Notification_Looping_Alarm3, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Notification.Looping.Alarm4"), "", Snd_Win_Notification_Looping_Alarm4, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Notification.Looping.Alarm5"), "", Snd_Win_Notification_Looping_Alarm5, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Notification.Looping.Alarm6"), "", Snd_Win_Notification_Looping_Alarm6, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Notification.Looping.Alarm7"), "", Snd_Win_Notification_Looping_Alarm7, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Notification.Looping.Alarm8"), "", Snd_Win_Notification_Looping_Alarm8, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Notification.Looping.Alarm9"), "", Snd_Win_Notification_Looping_Alarm9, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Notification.Looping.Alarm10"), "", Snd_Win_Notification_Looping_Alarm10, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Notification.Looping.Call"), "", Snd_Win_Notification_Looping_Call, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Notification.Looping.Call2"), "", Snd_Win_Notification_Looping_Call2, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Notification.Looping.Call3"), "", Snd_Win_Notification_Looping_Call3, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Notification.Looping.Call4"), "", Snd_Win_Notification_Looping_Call4, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Notification.Looping.Call5"), "", Snd_Win_Notification_Looping_Call5, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Notification.Looping.Call6"), "", Snd_Win_Notification_Looping_Call6, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Notification.Looping.Call7"), "", Snd_Win_Notification_Looping_Call7, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Notification.Looping.Call8"), "", Snd_Win_Notification_Looping_Call8, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Notification.Looping.Call9"), "", Snd_Win_Notification_Looping_Call9, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Notification.Looping.Call10"), "", Snd_Win_Notification_Looping_Call10, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Notification.Mail"), "", Snd_Win_Notification_Mail, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Notification.Proximity"), "", Snd_Win_Notification_Proximity, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Notification.Reminder"), "", Snd_Win_Notification_Reminder, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Notification.SMS"), "", Snd_Win_Notification_SMS, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Open"), "", Snd_Win_Open, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "PrintComplete"), "", Snd_Win_PrintComplete, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "ProximityConnection"), "", Snd_Win_ProximityConnection, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "RestoreDown"), "", Snd_Win_RestoreDown, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "RestoreUp"), "", Snd_Win_RestoreUp, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "ShowBand"), "", Snd_Win_ShowBand, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "SystemAsterisk"), "", Snd_Win_SystemAsterisk, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "SystemExclamation"), "", Snd_Win_SystemExclamation, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "SystemExit"), "", Snd_Win_SystemExit, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "SystemStart"), "", Snd_Win_SystemStart, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "SystemHand"), "", Snd_Win_SystemHand, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "SystemNotification"), "", Snd_Win_SystemNotification, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "SystemQuestion"), "", Snd_Win_SystemQuestion, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "WindowsLogoff"), "", Snd_Win_WindowsLogoff, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "WindowsLogon"), "", Snd_Win_WindowsLogon, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "WindowsUAC"), "", Snd_Win_WindowsUAC, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "WindowsUnlock"), "", Snd_Win_WindowsUnlock, RegistryValueKind.String);
                }

                foreach (string Scope in Scope_Explorer)
                {
                    EditReg(TreeView, string.Format(Scope, "ActivatingDocument"), "", Snd_Explorer_ActivatingDocument, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "BlockedPopup"), "", Snd_Explorer_BlockedPopup, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "EmptyRecycleBin"), "", Snd_Explorer_EmptyRecycleBin, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "FeedDiscovered"), "", Snd_Explorer_FeedDiscovered, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "MoveMenuItem"), "", Snd_Explorer_MoveMenuItem, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Navigating"), "", Snd_Explorer_Navigating, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "SecurityBand"), "", Snd_Explorer_SecurityBand, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "SearchProviderDiscovered"), "", Snd_Explorer_SearchProviderDiscovered, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "FaxError"), "", Snd_Explorer_FaxError, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "FaxLineRings"), "", Snd_Explorer_FaxLineRings, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "FaxNew"), "", Snd_Explorer_FaxNew, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "FaxSent"), "", Snd_Explorer_FaxSent, RegistryValueKind.String);
                }

                foreach (string Scope in Scope_NetMeeting)
                {
                    EditReg(TreeView, string.Format(Scope, "Person Joins"), "", Snd_NetMeeting_PersonJoins, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Person Leaves"), "", Snd_NetMeeting_PersonLeaves, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Receive Call"), "", Snd_NetMeeting_ReceiveCall, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "Receive Request to Join"), "", Snd_NetMeeting_ReceiveRequestToJoin, RegistryValueKind.String);
                }

                foreach (string Scope in Scope_SpeechRec)
                {
                    EditReg(TreeView, string.Format(Scope, "DisNumbersSound"), "", Snd_SpeechRec_DisNumbersSound, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "HubOffSound"), "", Snd_SpeechRec_HubOffSound, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "HubOnSound"), "", Snd_SpeechRec_HubOnSound, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "HubSleepSound"), "", Snd_SpeechRec_HubSleepSound, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "MisrecoSound"), "", Snd_SpeechRec_MisrecoSound, RegistryValueKind.String);
                    EditReg(TreeView, string.Format(Scope, "PanelSound"), "", Snd_SpeechRec_PanelSound, RegistryValueKind.String);
                }

                if (!System.IO.Directory.Exists(PathsExt.SysEventsSoundsDir))
                    System.IO.Directory.CreateDirectory(PathsExt.SysEventsSoundsDir);

                INI[] INIs = new INI[] { new(PathsExt.SysEventsSounds_Local_INI), new(PathsExt.SysEventsSounds_Global_INI) };

                foreach (INI ini in INIs)
                {
                    ini.Write("Power", "Snd_ChargerConnected", Snd_ChargerConnected);
                    ini.Write("Power", "Snd_ChargerDisconnected", Snd_ChargerDisconnected);
                    ini.Write("Windows", "Logoff", Snd_Win_WindowsLogoff);
                    ini.Write("Windows", "Logon", Snd_Win_WindowsLogon);
                    ini.Write("Windows", "Lock", Snd_Win_WindowsLock);
                    ini.Write("Windows", "Unlock", Snd_Win_WindowsUnlock);
                    ini.Write("Windows", "Exit", Snd_Win_SystemExit);
                }
            }
        }

        /// <summary>Operator to check if two Sounds structures are equal</summary>
        public static bool operator ==(Sounds First, Sounds Second)
        {
            return First.Equals(Second);
        }

        /// <summary>Operator to check if two Sounds structures are not equal</summary>
        public static bool operator !=(Sounds First, Sounds Second)
        {
            return !First.Equals(Second);
        }

        /// <summary>Clones Sounds structure</summary>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Dispose Sounds structure to free up memory
        /// </summary>
        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }

        /// <summary>Checks if two Sounds structures are equal or not</summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>Get hash code of Sounds structure</summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
