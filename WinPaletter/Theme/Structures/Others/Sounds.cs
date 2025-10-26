using Microsoft.Win32;
using Serilog.Events;
using System;
using System.IO;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    /// <summary>
    /// Structure responsible for managing Windows sounds
    /// </summary>
    public class Sounds : ManagerBase<Sounds>
    {
        /// <summary>Controls if this feature is enabled or not</summary>
        public bool Enabled { get; set; } = false;

        /// <summary>Windows default beep WAV sound File path</summary>
        public string Snd_Win_Default { get; set; } = string.Empty;

        /// <summary>Windows application GP fault WAV sound File path<br>???</br></summary>
        public string Snd_Win_AppGPFault { get; set; } = string.Empty;

        /// <summary>Windows CC select WAV sound File path<br>???</br></summary>
        public string Snd_Win_CCSelect { get; set; } = string.Empty;

        /// <summary>Windows theme change WAV sound File path</summary>
        public string Snd_Win_ChangeTheme { get; set; } = string.Empty;

        /// <summary>Window close WAV sound File path</summary>
        public string Snd_Win_Close { get; set; } = string.Empty;

        /// <summary>Critical battery alarm WAV sound File path</summary>
        public string Snd_Win_CriticalBatteryAlarm { get; set; } = string.Empty;

        /// <summary>Device (USB) connection WAV sound File path</summary>
        public string Snd_Win_DeviceConnect { get; set; } = string.Empty;

        /// <summary>Device (USB) disconnection WAV sound File path</summary>
        public string Snd_Win_DeviceDisconnect { get; set; } = string.Empty;

        /// <summary>Device (USB) failure WAV sound File path</summary>
        public string Snd_Win_DeviceFail { get; set; } = string.Empty;

        /// <summary>Fax beep WAV sound File path</summary>
        public string Snd_Win_FaxBeep { get; set; } = string.Empty;

        /// <summary>Low battery alarm WAV sound File path</summary>
        public string Snd_Win_LowBatteryAlarm { get; set; } = string.Empty;

        /// <summary>Mail received beep WAV sound File path</summary>
        public string Snd_Win_MailBeep { get; set; } = string.Empty;

        /// <summary>Window maximize WAV sound File path</summary>
        public string Snd_Win_Maximize { get; set; } = string.Empty;

        /// <summary>contextMenu item click WAV sound File path</summary>
        public string Snd_Win_MenuCommand { get; set; } = string.Empty;

        /// <summary>contextMenu popup WAV sound File path</summary>
        public string Snd_Win_MenuPopup { get; set; } = string.Empty;

        /// <summary>Message nudge WAV sound File path</summary>
        public string Snd_Win_MessageNudge { get; set; } = string.Empty;

        /// <summary>Window minimize WAV sound File path</summary>
        public string Snd_Win_Minimize { get; set; } = string.Empty;

        /// <summary>Windows notification WAV sound File path</summary>
        public string Snd_Win_Notification_Default { get; set; } = string.Empty;

        /// <summary>Instant message notification WAV sound File path</summary>
        public string Snd_Win_Notification_IM { get; set; } = string.Empty;

        /// <summary>Windows 8 (and later) alarm 0 WAV sound File path</summary>
        public string Snd_Win_Notification_Looping_Alarm { get; set; } = string.Empty;

        /// <summary>Windows 8 (and later) alarm 10 WAV sound File path</summary>
        public string Snd_Win_Notification_Looping_Alarm10 { get; set; } = string.Empty;

        /// <summary>Windows 8 (and later) alarm 2 WAV sound File path</summary>
        public string Snd_Win_Notification_Looping_Alarm2 { get; set; } = string.Empty;

        /// <summary>Windows 8 (and later) alarm 3 WAV sound File path</summary>
        public string Snd_Win_Notification_Looping_Alarm3 { get; set; } = string.Empty;

        /// <summary>Windows 8 (and later) alarm 4 WAV sound File path</summary>
        public string Snd_Win_Notification_Looping_Alarm4 { get; set; } = string.Empty;

        /// <summary>Windows 8 (and later) alarm 5 WAV sound File path</summary>
        public string Snd_Win_Notification_Looping_Alarm5 { get; set; } = string.Empty;

        /// <summary>Windows 8 (and later) alarm 6 WAV sound File path</summary>
        public string Snd_Win_Notification_Looping_Alarm6 { get; set; } = string.Empty;

        /// <summary>Windows 8 (and later) alarm 7 WAV sound File path</summary>
        public string Snd_Win_Notification_Looping_Alarm7 { get; set; } = string.Empty;

        /// <summary>Windows 8 (and later) alarm 8 WAV sound File path</summary>
        public string Snd_Win_Notification_Looping_Alarm8 { get; set; } = string.Empty;

        /// <summary>Windows 8 (and later) alarm 9 WAV sound File path</summary>
        public string Snd_Win_Notification_Looping_Alarm9 { get; set; } = string.Empty;

        /// <summary>Windows 8 (and later) ring tone 0 WAV sound File path</summary>
        public string Snd_Win_Notification_Looping_Call { get; set; } = string.Empty;

        /// <summary>Windows 8 (and later) ring tone 10 WAV sound File path</summary>
        public string Snd_Win_Notification_Looping_Call10 { get; set; } = string.Empty;

        /// <summary>Windows 8 (and later) ring tone 2 WAV sound File path</summary>
        public string Snd_Win_Notification_Looping_Call2 { get; set; } = string.Empty;

        /// <summary>Windows 8 (and later) ring tone 3 WAV sound File path</summary>
        public string Snd_Win_Notification_Looping_Call3 { get; set; } = string.Empty;

        /// <summary>Windows 8 (and later) ring tone 4 WAV sound File path</summary>
        public string Snd_Win_Notification_Looping_Call4 { get; set; } = string.Empty;

        /// <summary>Windows 8 (and later) ring tone 5 WAV sound File path</summary>
        public string Snd_Win_Notification_Looping_Call5 { get; set; } = string.Empty;

        /// <summary>Windows 8 (and later) ring tone 6 WAV sound File path</summary>
        public string Snd_Win_Notification_Looping_Call6 { get; set; } = string.Empty;

        /// <summary>Windows 8 (and later) ring tone 7 WAV sound File path</summary>
        public string Snd_Win_Notification_Looping_Call7 { get; set; } = string.Empty;

        /// <summary>Windows 8 (and later) ring tone 8 WAV sound File path</summary>
        public string Snd_Win_Notification_Looping_Call8 { get; set; } = string.Empty;

        /// <summary>Windows 8 (and later) ring tone 9 WAV sound File path</summary>
        public string Snd_Win_Notification_Looping_Call9 { get; set; } = string.Empty;

        /// <summary>Mail notification WAV sound File path</summary>
        public string Snd_Win_Notification_Mail { get; set; } = string.Empty;

        /// <summary>Proximity notification WAV sound File path</summary>
        public string Snd_Win_Notification_Proximity { get; set; } = string.Empty;

        /// <summary>Notification reminder WAV sound File path</summary>
        public string Snd_Win_Notification_Reminder { get; set; } = string.Empty;

        /// <summary>SMS notification WAV sound File path</summary>
        public string Snd_Win_Notification_SMS { get; set; } = string.Empty;

        /// <summary>Application open WAV sound File path</summary>
        public string Snd_Win_Open { get; set; } = string.Empty;

        /// <summary>Print job completed WAV sound File path</summary>
        public string Snd_Win_PrintComplete { get; set; } = string.Empty;

        /// <summary>Proximity connection WAV sound File path</summary>
        public string Snd_Win_ProximityConnection { get; set; } = string.Empty;

        /// <summary>Window restore down WAV sound File path</summary>
        public string Snd_Win_RestoreDown { get; set; } = string.Empty;

        /// <summary>Window restore up WAV sound File path</summary>
        public string Snd_Win_RestoreUp { get; set; } = string.Empty;

        /// <summary>Windows Explorer/Internet Explorer band showed WAV sound File path</summary>
        public string Snd_Win_ShowBand { get; set; } = string.Empty;

        /// <summary>System asterisk WAV sound File path</summary>
        public string Snd_Win_SystemAsterisk { get; set; } = string.Empty;

        /// <summary>Exclamation WAV sound File path</summary>
        public string Snd_Win_SystemExclamation { get; set; } = string.Empty;

        /// <summary>Windows shutdown sound (not working for Windows 8 and later)</summary>
        public string Snd_Win_SystemExit { get; set; } = string.Empty;

        /// <summary>Windows start up sound (not working for Windows Vista and later)</summary>
        public string Snd_Win_SystemStart { get; set; } = string.Empty;

        /// <summary>
        /// Syntax or path of WAV File that will be patched in imageres.dll to be used as Windows startup sound
        /// <br></br>- Targeting Windows Vista and later
        /// <code>
        /// Syntaxes:
        ///  file_path:          String path of WAV File that will be patched in imageres.dll
        ///  DEFAULT:            It will restore default sound from WinPaletter backup
        ///  CURRENT:            It will do nothing
        ///  Empty string (""):  It will disable startup sound
        /// </code>
        /// </summary>
        public string Snd_Imageres_SystemStart { get; set; } = (OS.W12 || OS.W11) ? "Default" : string.Empty;

        /// <summary>Hyperlink clicked WAV sound File path</summary>
        public string Snd_Win_SystemHand { get; set; } = string.Empty;

        /// <summary>Information message WAV sound File path</summary>
        public string Snd_Win_SystemNotification { get; set; } = string.Empty;

        /// <summary>Question message WAV sound File path</summary>
        public string Snd_Win_SystemQuestion { get; set; } = string.Empty;

        /// <summary>Windows logoff WAV sound File path (not working for Windows 8 and later)</summary>
        public string Snd_Win_WindowsLogoff { get; set; } = string.Empty;

        /// <summary>Windows logon WAV sound File path (not working for Windows 8 and later)</summary>
        public string Snd_Win_WindowsLogon { get; set; } = string.Empty;

        /// <summary>User accound control (UAC) dialog WAV sound File path (for Windows Vista and later)</summary>
        public string Snd_Win_WindowsUAC { get; set; } = string.Empty;

        /// <summary>Windows unlock WAV sound File path (targeting Windows 8 and later, but not working)</summary>
        public string Snd_Win_WindowsUnlock { get; set; } = string.Empty;

        /// <summary>Activating document WAV sound File path</summary>
        public string Snd_Explorer_ActivatingDocument { get; set; } = string.Empty;

        /// <summary>Popup blocked WAV sound File path</summary>
        public string Snd_Explorer_BlockedPopup { get; set; } = string.Empty;

        /// <summary>Recycle bin eptied WAV sound File path</summary>
        public string Snd_Explorer_EmptyRecycleBin { get; set; } = string.Empty;

        /// <summary>Feed discovered WAV sound File path</summary>
        public string Snd_Explorer_FeedDiscovered { get; set; } = string.Empty;

        /// <summary>contextMenu item moved WAV sound File path</summary>
        public string Snd_Explorer_MoveMenuItem { get; set; } = string.Empty;

        /// <summary>Folders navigation WAV sound File path</summary>
        public string Snd_Explorer_Navigating { get; set; } = string.Empty;

        /// <summary>Security band appeared WAV sound File path</summary>
        public string Snd_Explorer_SecurityBand { get; set; } = string.Empty;

        /// <summary>Search provider discovered WAV sound File path</summary>
        public string Snd_Explorer_SearchProviderDiscovered { get; set; } = string.Empty;

        /// <summary>Fax error WAV sound File path</summary>
        public string Snd_Explorer_FaxError { get; set; } = string.Empty;

        /// <summary>Fax line ringing WAV sound File path</summary>
        public string Snd_Explorer_FaxLineRings { get; set; } = string.Empty;

        /// <summary>New fax received WAV sound File path</summary>
        public string Snd_Explorer_FaxNew { get; set; } = string.Empty;

        /// <summary>Fax sent WAV sound File path</summary>
        public string Snd_Explorer_FaxSent { get; set; } = string.Empty;

        /// <summary>NetMeeting application (Windows XP): person joins WAV sound File path</summary>
        public string Snd_NetMeeting_PersonJoins { get; set; } = string.Empty;

        /// <summary>NetMeeting application (Windows XP): person leaved WAV sound File path</summary>
        public string Snd_NetMeeting_PersonLeaves { get; set; } = string.Empty;

        /// <summary>NetMeeting application (Windows XP): receive call WAV sound File path</summary>
        public string Snd_NetMeeting_ReceiveCall { get; set; } = string.Empty;

        /// <summary>NetMeeting application (Windows XP): receive request to join WAV sound File path</summary>
        public string Snd_NetMeeting_ReceiveRequestToJoin { get; set; } = string.Empty;

        /// <summary>Speech recognition (Windows Vista and later): disambiguation numbers WAV sound File path</summary>
        public string Snd_SpeechRec_DisNumbersSound { get; set; } = string.Empty;

        /// <summary>Speech recognition (Windows Vista and later): Hub off WAV sound File path</summary>
        public string Snd_SpeechRec_HubOffSound { get; set; } = string.Empty;

        /// <summary>Speech recognition (Windows Vista and later): Hub on WAV sound File path</summary>
        public string Snd_SpeechRec_HubOnSound { get; set; } = string.Empty;

        /// <summary>Speech recognition (Windows Vista and later): Hub sleep WAV sound File path</summary>
        public string Snd_SpeechRec_HubSleepSound { get; set; } = string.Empty;

        /// <summary>Speech recognition (Windows Vista and later): Misrecognition WAV sound File path</summary>
        public string Snd_SpeechRec_MisrecoSound { get; set; } = string.Empty;

        /// <summary>Speech recognition (Windows Vista and later): disambiguation panel WAV sound File path</summary>
        public string Snd_SpeechRec_PanelSound { get; set; } = string.Empty;

        /// <summary>Windows unlock WAV sound File path 
        /// <br></br>- Targeting Windows 8 and later
        /// <br></br>- Deflected by service that listens to Windows events (WinPaletter.SysEventsSounds)
        /// </summary>
        public string Snd_Win_WindowsLock { get; set; } = string.Empty;

        /// <summary>Charger connected WAV sound File path 
        /// <br><b><i>(!) It is not an official sound in Windows</i></b></br>
        /// <br></br>- Deflected by service that listens to Windows events (WinPaletter.SysEventsSounds)
        /// </summary>
        public string Snd_ChargerConnected { get; set; } = string.Empty;

        /// <summary>Charger disconnected WAV sound File path 
        /// <br><b><i>(!) It is not an official sound in Windows</i></b></br>
        /// <br></br>- Deflected by service that listens to Windows events (WinPaletter.SysEventsSounds)
        /// </summary>
        public string Snd_ChargerDisconnected { get; set; } = string.Empty;

        /// <summary>
        /// Wi-Fi connected WAV sound File path
        /// <br><b><i>(!) It is not an official sound in Windows</i></b></br>
        /// <br></br>- Deflected by service that listens to Windows events (WinPaletter.SysEventsSounds)
        /// </summary>
        public string Snd_WiFiConnected { get; set; } = string.Empty;

        /// <summary>
        /// Wi-Fi disconnected WAV sound File path
        /// <br><b><i>(!) It is not an official sound in Windows</i></b></br>
        /// <br></br>- Deflected by service that listens to Windows events (WinPaletter.SysEventsSounds)
        /// </summary>
        public string Snd_WiFiDisconnected { get; set; } = string.Empty;

        /// <summary>
        /// Wi-Fi connection failure WAV sound File path
        /// <br><b><i>(!) It is not an official sound in Windows</i></b></br>
        /// <br></br>- Deflected by service that listens to Windows events (WinPaletter.SysEventsSounds)
        /// </summary>
        public string Snd_WiFiConnectionFailed { get; set; } = string.Empty;

        /// <summary>
        /// Creates new Sounds structure with default values
        /// </summary>
        public Sounds() { }

        /// <summary>
        /// Loads Sounds data from registry
        /// </summary>
        /// <param name="default">Default Sounds data structure</param>
        public void Load(Sounds @default)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Loading Windows sounds settings from registry.");

            Enabled = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", string.Empty, @default.Enabled);
            Snd_Imageres_SystemStart = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Imageres.dll_Startup", @default.Snd_Imageres_SystemStart);
            Snd_ChargerConnected = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_ChargerConnected", @default.Snd_ChargerConnected);
            Snd_ChargerDisconnected = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_ChargerDisconnected", @default.Snd_ChargerDisconnected);
            Snd_Win_WindowsLock = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_WindowsLock", @default.Snd_Win_WindowsLock);
            Snd_WiFiConnected = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_WiFiConnected", @default.Snd_WiFiConnected);
            Snd_WiFiDisconnected = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_WiFiDisconnected", @default.Snd_WiFiDisconnected);
            Snd_WiFiConnectionFailed = ReadReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_WiFiConnectionFailed", @default.Snd_WiFiConnectionFailed);

            string Scope_Win = @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\.Default\{0}\.Current";
            Snd_Win_Default = ReadReg(string.Format(Scope_Win, ".Default"), string.Empty, @default.Snd_Win_Default);
            Snd_Win_AppGPFault = ReadReg(string.Format(Scope_Win, "AppGPFault"), string.Empty, @default.Snd_Win_AppGPFault);
            Snd_Win_CCSelect = ReadReg(string.Format(Scope_Win, "CCSelect"), string.Empty, @default.Snd_Win_CCSelect);
            Snd_Win_ChangeTheme = ReadReg(string.Format(Scope_Win, "ChangeTheme"), string.Empty, @default.Snd_Win_ChangeTheme);
            Snd_Win_Close = ReadReg(string.Format(Scope_Win, "Close"), string.Empty, @default.Snd_Win_Close);
            Snd_Win_CriticalBatteryAlarm = ReadReg(string.Format(Scope_Win, "CriticalBatteryAlarm"), string.Empty, @default.Snd_Win_CriticalBatteryAlarm);
            Snd_Win_DeviceConnect = ReadReg(string.Format(Scope_Win, "DeviceConnect"), string.Empty, @default.Snd_Win_DeviceConnect);
            Snd_Win_DeviceDisconnect = ReadReg(string.Format(Scope_Win, "DeviceDisconnect"), string.Empty, @default.Snd_Win_DeviceDisconnect);
            Snd_Win_DeviceFail = ReadReg(string.Format(Scope_Win, "DeviceFail"), string.Empty, @default.Snd_Win_DeviceFail);
            Snd_Win_FaxBeep = ReadReg(string.Format(Scope_Win, "FaxBeep"), string.Empty, @default.Snd_Win_FaxBeep);
            Snd_Win_LowBatteryAlarm = ReadReg(string.Format(Scope_Win, "LowBatteryAlarm"), string.Empty, @default.Snd_Win_LowBatteryAlarm);
            Snd_Win_MailBeep = ReadReg(string.Format(Scope_Win, "MailBeep"), string.Empty, @default.Snd_Win_MailBeep);
            Snd_Win_Maximize = ReadReg(string.Format(Scope_Win, "Maximize"), string.Empty, @default.Snd_Win_Maximize);
            Snd_Win_MenuCommand = ReadReg(string.Format(Scope_Win, "MenuCommand"), string.Empty, @default.Snd_Win_MenuCommand);
            Snd_Win_MenuPopup = ReadReg(string.Format(Scope_Win, "MenuPopup"), string.Empty, @default.Snd_Win_MenuPopup);
            Snd_Win_MessageNudge = ReadReg(string.Format(Scope_Win, "MessageNudge"), string.Empty, @default.Snd_Win_MessageNudge);
            Snd_Win_Minimize = ReadReg(string.Format(Scope_Win, "Minimize"), string.Empty, @default.Snd_Win_Minimize);
            Snd_Win_Notification_Default = ReadReg(string.Format(Scope_Win, "Notification.Default"), string.Empty, @default.Snd_Win_Notification_Default);
            Snd_Win_Notification_IM = ReadReg(string.Format(Scope_Win, "Notification.IM"), string.Empty, @default.Snd_Win_Notification_IM);
            Snd_Win_Notification_Looping_Alarm = ReadReg(string.Format(Scope_Win, "Notification.Looping.Alarm"), string.Empty, @default.Snd_Win_Notification_Looping_Alarm);
            Snd_Win_Notification_Looping_Alarm2 = ReadReg(string.Format(Scope_Win, "Notification.Looping.Alarm2"), string.Empty, @default.Snd_Win_Notification_Looping_Alarm2);
            Snd_Win_Notification_Looping_Alarm3 = ReadReg(string.Format(Scope_Win, "Notification.Looping.Alarm3"), string.Empty, @default.Snd_Win_Notification_Looping_Alarm3);
            Snd_Win_Notification_Looping_Alarm4 = ReadReg(string.Format(Scope_Win, "Notification.Looping.Alarm4"), string.Empty, @default.Snd_Win_Notification_Looping_Alarm4);
            Snd_Win_Notification_Looping_Alarm5 = ReadReg(string.Format(Scope_Win, "Notification.Looping.Alarm5"), string.Empty, @default.Snd_Win_Notification_Looping_Alarm5);
            Snd_Win_Notification_Looping_Alarm6 = ReadReg(string.Format(Scope_Win, "Notification.Looping.Alarm6"), string.Empty, @default.Snd_Win_Notification_Looping_Alarm6);
            Snd_Win_Notification_Looping_Alarm7 = ReadReg(string.Format(Scope_Win, "Notification.Looping.Alarm7"), string.Empty, @default.Snd_Win_Notification_Looping_Alarm7);
            Snd_Win_Notification_Looping_Alarm8 = ReadReg(string.Format(Scope_Win, "Notification.Looping.Alarm8"), string.Empty, @default.Snd_Win_Notification_Looping_Alarm8);
            Snd_Win_Notification_Looping_Alarm9 = ReadReg(string.Format(Scope_Win, "Notification.Looping.Alarm9"), string.Empty, @default.Snd_Win_Notification_Looping_Alarm9);
            Snd_Win_Notification_Looping_Alarm10 = ReadReg(string.Format(Scope_Win, "Notification.Looping.Alarm10"), string.Empty, @default.Snd_Win_Notification_Looping_Alarm10);
            Snd_Win_Notification_Looping_Call = ReadReg(string.Format(Scope_Win, "Notification.Looping.Call"), string.Empty, @default.Snd_Win_Notification_Looping_Call);
            Snd_Win_Notification_Looping_Call2 = ReadReg(string.Format(Scope_Win, "Notification.Looping.Call2"), string.Empty, @default.Snd_Win_Notification_Looping_Call2);
            Snd_Win_Notification_Looping_Call3 = ReadReg(string.Format(Scope_Win, "Notification.Looping.Call3"), string.Empty, @default.Snd_Win_Notification_Looping_Call3);
            Snd_Win_Notification_Looping_Call4 = ReadReg(string.Format(Scope_Win, "Notification.Looping.Call4"), string.Empty, @default.Snd_Win_Notification_Looping_Call4);
            Snd_Win_Notification_Looping_Call5 = ReadReg(string.Format(Scope_Win, "Notification.Looping.Call5"), string.Empty, @default.Snd_Win_Notification_Looping_Call5);
            Snd_Win_Notification_Looping_Call6 = ReadReg(string.Format(Scope_Win, "Notification.Looping.Call6"), string.Empty, @default.Snd_Win_Notification_Looping_Call6);
            Snd_Win_Notification_Looping_Call7 = ReadReg(string.Format(Scope_Win, "Notification.Looping.Call7"), string.Empty, @default.Snd_Win_Notification_Looping_Call7);
            Snd_Win_Notification_Looping_Call8 = ReadReg(string.Format(Scope_Win, "Notification.Looping.Call8"), string.Empty, @default.Snd_Win_Notification_Looping_Call8);
            Snd_Win_Notification_Looping_Call9 = ReadReg(string.Format(Scope_Win, "Notification.Looping.Call9"), string.Empty, @default.Snd_Win_Notification_Looping_Call9);
            Snd_Win_Notification_Looping_Call10 = ReadReg(string.Format(Scope_Win, "Notification.Looping.Call10"), string.Empty, @default.Snd_Win_Notification_Looping_Call10);
            Snd_Win_Notification_Mail = ReadReg(string.Format(Scope_Win, "Notification.Mail"), string.Empty, @default.Snd_Win_Notification_Mail);
            Snd_Win_Notification_Proximity = ReadReg(string.Format(Scope_Win, "Notification.Proximity"), string.Empty, @default.Snd_Win_Notification_Proximity);
            Snd_Win_Notification_Reminder = ReadReg(string.Format(Scope_Win, "Notification.Reminder"), string.Empty, @default.Snd_Win_Notification_Reminder);
            Snd_Win_Notification_SMS = ReadReg(string.Format(Scope_Win, "Notification.SMS"), string.Empty, @default.Snd_Win_Notification_SMS);
            Snd_Win_Open = ReadReg(string.Format(Scope_Win, "Open"), string.Empty, @default.Snd_Win_Open);
            Snd_Win_PrintComplete = ReadReg(string.Format(Scope_Win, "PrintComplete"), string.Empty, @default.Snd_Win_PrintComplete);
            Snd_Win_ProximityConnection = ReadReg(string.Format(Scope_Win, "ProximityConnection"), string.Empty, @default.Snd_Win_ProximityConnection);
            Snd_Win_RestoreDown = ReadReg(string.Format(Scope_Win, "RestoreDown"), string.Empty, @default.Snd_Win_RestoreDown);
            Snd_Win_RestoreUp = ReadReg(string.Format(Scope_Win, "RestoreUp"), string.Empty, @default.Snd_Win_RestoreUp);
            Snd_Win_ShowBand = ReadReg(string.Format(Scope_Win, "ShowBand"), string.Empty, @default.Snd_Win_ShowBand);
            Snd_Win_SystemAsterisk = ReadReg(string.Format(Scope_Win, "SystemAsterisk"), string.Empty, @default.Snd_Win_SystemAsterisk);
            Snd_Win_SystemExclamation = ReadReg(string.Format(Scope_Win, "SystemExclamation"), string.Empty, @default.Snd_Win_SystemExclamation);
            Snd_Win_SystemExit = ReadReg(string.Format(Scope_Win, "SystemExit"), string.Empty, @default.Snd_Win_SystemExit);
            Snd_Win_SystemStart = ReadReg(string.Format(Scope_Win, "SystemStart"), string.Empty, @default.Snd_Win_SystemStart);
            Snd_Win_SystemHand = ReadReg(string.Format(Scope_Win, "SystemHand"), string.Empty, @default.Snd_Win_SystemHand);
            Snd_Win_SystemNotification = ReadReg(string.Format(Scope_Win, "SystemNotification"), string.Empty, @default.Snd_Win_SystemNotification);
            Snd_Win_SystemQuestion = ReadReg(string.Format(Scope_Win, "SystemQuestion"), string.Empty, @default.Snd_Win_SystemQuestion);
            Snd_Win_WindowsLogoff = ReadReg(string.Format(Scope_Win, "WindowsLogoff"), string.Empty, @default.Snd_Win_WindowsLogoff);
            Snd_Win_WindowsLogon = ReadReg(string.Format(Scope_Win, "WindowsLogon"), string.Empty, @default.Snd_Win_WindowsLogon);
            Snd_Win_WindowsUAC = ReadReg(string.Format(Scope_Win, "WindowsUAC"), string.Empty, @default.Snd_Win_WindowsUAC);
            Snd_Win_WindowsUnlock = ReadReg(string.Format(Scope_Win, "WindowsUnlock"), string.Empty, @default.Snd_Win_WindowsUnlock);

            string Scope_Explorer = @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\Explorer\{0}\.Current";
            Snd_Explorer_ActivatingDocument = ReadReg(string.Format(Scope_Explorer, "ActivatingDocument"), string.Empty, @default.Snd_Explorer_ActivatingDocument);
            Snd_Explorer_BlockedPopup = ReadReg(string.Format(Scope_Explorer, "BlockedPopup"), string.Empty, @default.Snd_Explorer_BlockedPopup);
            Snd_Explorer_EmptyRecycleBin = ReadReg(string.Format(Scope_Explorer, "EmptyRecycleBin"), string.Empty, @default.Snd_Explorer_EmptyRecycleBin);
            Snd_Explorer_FeedDiscovered = ReadReg(string.Format(Scope_Explorer, "FeedDiscovered"), string.Empty, @default.Snd_Explorer_FeedDiscovered);
            Snd_Explorer_MoveMenuItem = ReadReg(string.Format(Scope_Explorer, "MoveMenuItem"), string.Empty, @default.Snd_Explorer_MoveMenuItem);
            Snd_Explorer_Navigating = ReadReg(string.Format(Scope_Explorer, "Navigating"), string.Empty, @default.Snd_Explorer_Navigating);
            Snd_Explorer_SecurityBand = ReadReg(string.Format(Scope_Explorer, "SecurityBand"), string.Empty, @default.Snd_Explorer_SecurityBand);
            Snd_Explorer_SearchProviderDiscovered = ReadReg(string.Format(Scope_Explorer, "SearchProviderDiscovered"), string.Empty, @default.Snd_Explorer_SearchProviderDiscovered);
            Snd_Explorer_FaxError = ReadReg(string.Format(Scope_Explorer, "FaxError"), string.Empty, @default.Snd_Explorer_FaxError);
            Snd_Explorer_FaxLineRings = ReadReg(string.Format(Scope_Explorer, "FaxLineRings"), string.Empty, @default.Snd_Explorer_FaxLineRings);
            Snd_Explorer_FaxNew = ReadReg(string.Format(Scope_Explorer, "FaxNew"), string.Empty, @default.Snd_Explorer_FaxNew);
            Snd_Explorer_FaxSent = ReadReg(string.Format(Scope_Explorer, "FaxSent"), string.Empty, @default.Snd_Explorer_FaxSent);

            string Scope_NetMeeting = @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\Conf\{0}\.Current";
            Snd_NetMeeting_PersonJoins = ReadReg(string.Format(Scope_NetMeeting, "Person Joins"), string.Empty, @default.Snd_NetMeeting_PersonJoins);
            Snd_NetMeeting_PersonLeaves = ReadReg(string.Format(Scope_NetMeeting, "Person Leaves"), string.Empty, @default.Snd_NetMeeting_PersonLeaves);
            Snd_NetMeeting_ReceiveCall = ReadReg(string.Format(Scope_NetMeeting, "Receive Call"), string.Empty, @default.Snd_NetMeeting_ReceiveCall);
            Snd_NetMeeting_ReceiveRequestToJoin = ReadReg(string.Format(Scope_NetMeeting, "Receive Request to Join"), string.Empty, @default.Snd_NetMeeting_ReceiveRequestToJoin);

            string Scope_SpeechRec = @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\sapisvr\{0}\.current";
            Snd_SpeechRec_DisNumbersSound = ReadReg(string.Format(Scope_SpeechRec, "DisNumbersSound"), string.Empty, @default.Snd_SpeechRec_DisNumbersSound);
            Snd_SpeechRec_HubOffSound = ReadReg(string.Format(Scope_SpeechRec, "HubOffSound"), string.Empty, @default.Snd_SpeechRec_HubOffSound);
            Snd_SpeechRec_HubOnSound = ReadReg(string.Format(Scope_SpeechRec, "HubOnSound"), string.Empty, @default.Snd_SpeechRec_HubOnSound);
            Snd_SpeechRec_HubSleepSound = ReadReg(string.Format(Scope_SpeechRec, "HubSleepSound"), string.Empty, @default.Snd_SpeechRec_HubSleepSound);
            Snd_SpeechRec_MisrecoSound = ReadReg(string.Format(Scope_SpeechRec, "MisrecoSound"), string.Empty, @default.Snd_SpeechRec_MisrecoSound);
            Snd_SpeechRec_PanelSound = ReadReg(string.Format(Scope_SpeechRec, "PanelSound"), string.Empty, @default.Snd_SpeechRec_PanelSound);
        }

        /// <summary>
        /// Saves Sounds data into registry
        /// </summary>
        /// <param name="treeView">treeView used as theme log</param>
        public void Apply(TreeView treeView = null)
        {
            Program.Log?.Write(LogEventLevel.Information, $"Saving Windows sounds settings into registry.");

            SaveToggleState(treeView);

            // Save Windows sounds and unofficial sounds entries in WinPaletter's registry scope
            Program.Log?.Write(LogEventLevel.Information, $"Saving unofficial Windows sounds entries in WinPaletter's registry scope.");
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Imageres.dll_Startup", Snd_Imageres_SystemStart, RegistryValueKind.String);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_ChargerConnected", Snd_ChargerConnected, RegistryValueKind.String);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_ChargerDisconnected", Snd_ChargerDisconnected, RegistryValueKind.String);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_WindowsLock", Snd_Win_WindowsLock, RegistryValueKind.String);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_WiFiConnected", Snd_WiFiConnected, RegistryValueKind.String);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_WiFiDisconnected", Snd_WiFiDisconnected, RegistryValueKind.String);
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_WiFiConnectionFailed", Snd_WiFiConnectionFailed, RegistryValueKind.String);

            // Save Windows sounds and unofficial sounds entries in Windows registry scope
            if (File.Exists(SysPaths.SysEventsSounds_Local_INI)) { File.Delete(SysPaths.SysEventsSounds_Local_INI); }

            if (File.Exists(SysPaths.SysEventsSounds_Global_INI))
            {
                try { File.Delete(SysPaths.SysEventsSounds_Global_INI); }
                catch { Program.SendCommand($"{SysPaths.CMD} /C del \"{SysPaths.SysEventsSounds_Global_INI}\" && exit"); }
            }

            if (Enabled)
            {
                // Registry keys that has option to disable/enable Windows startup sound
                string[] destination_StartupSnd = [@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\System", @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\BootAnimation"];

                if (string.IsNullOrWhiteSpace(Snd_Imageres_SystemStart))
                {
                    Program.Log?.Write(LogEventLevel.Information, $"Disabling Windows startup sound in registry.");
                    WriteReg_CMD(treeView, destination_StartupSnd[0], "DisableStartupSound", 1);
                    WriteReg_CMD(treeView, destination_StartupSnd[1], "DisableStartupSound", 1);
                }
                else if (File.Exists(Snd_Imageres_SystemStart))
                {
                    Program.Log?.Write(LogEventLevel.Information, $"Enabling Windows startup sound in registry.");
                    WriteReg_CMD(treeView, destination_StartupSnd[0], "DisableStartupSound", 0);
                    WriteReg_CMD(treeView, destination_StartupSnd[1], "DisableStartupSound", 0);
                }
                else if (Snd_Imageres_SystemStart.Trim().ToUpper() == "DEFAULT")
                {
                    Program.Log?.Write(LogEventLevel.Information, $"{((!OS.W12 && !OS.W11) ? "Enabling" : "Disabling")} Windows startup sound in registry.");
                    WriteReg_CMD(treeView, destination_StartupSnd[0], "DisableStartupSound", (!OS.W12 && !OS.W11) ? 1 : 0);
                    WriteReg_CMD(treeView, destination_StartupSnd[1], "DisableStartupSound", (!OS.W12 && !OS.W11) ? 1 : 0);
                }
                else if (!(Snd_Imageres_SystemStart.Trim().ToUpper() == "CURRENT"))
                {
                    // Change nothing
                }
                else
                {
                    WriteReg_CMD(treeView, destination_StartupSnd[0], "DisableStartupSound", 1);
                    WriteReg_CMD(treeView, destination_StartupSnd[1], "DisableStartupSound", 1);
                }

                // Patching Windows startup sound in imageres.dll if Windows is not Windows XP
                if (!OS.WXP)
                {
                    if (File.Exists(Snd_Imageres_SystemStart) && Path.GetExtension(Snd_Imageres_SystemStart).ToUpper() == ".WAV")
                    {
                        // Windows startup sound is saved in imageres.dll as resource ID 5051 for Windows Vista and 5080 for Windows 7
                        byte[] CurrentSoundBytes = PE.GetResource(SysPaths.imageres, "WAVE", OS.WVista ? 5051 : 5080);
                        byte[] TargetSoundBytes = File.ReadAllBytes(Snd_Imageres_SystemStart);

                        if (!CurrentSoundBytes.Equals_Method2(TargetSoundBytes))
                        {
                            Program.Log?.Write(LogEventLevel.Information, $"Patching Windows startup sound in `imageres.dll`.");
                            PE.ReplaceResource(treeView, SysPaths.imageres, "WAVE", OS.WVista ? 5051 : 5080, TargetSoundBytes);
                        }
                    }
                    // Restoring default Windows startup sound from WinPaletter backup
                    else if (Snd_Imageres_SystemStart.Trim().ToUpper() == "DEFAULT")
                    {
                        byte[] CurrentSoundBytes = PE.GetResource(SysPaths.imageres, "WAVE", OS.WVista ? 5051 : 5080);
                        byte[] OriginalSoundBytes = File.ReadAllBytes($@"{SysPaths.appData}\WindowsStartup_Backup.wav");

                        if (!CurrentSoundBytes.Equals_Method2(OriginalSoundBytes))
                        {
                            Program.Log?.Write(LogEventLevel.Information, $"Restoring default Windows startup sound by repatching `imageres.dll` with the default sound.");
                            PE.ReplaceResource(treeView, SysPaths.imageres, "WAVE", OS.WVista ? 5051 : 5080, OriginalSoundBytes);
                        }

                        // Restore Windows startup sound by an alternative method using SFC
                        if (Program.Settings.ThemeApplyingBehavior.SFC_on_restoring_StartupSound)
                            SFC(SysPaths.imageres);
                    }
                }

                // Delete tasks created by old WinPaletter versions (Sounds redirection method depends now on service, not task scheduler)
                if (OS.W8x | OS.W10 | OS.W11 | OS.W12)
                {
                    Tasks.Delete(Tasks.TaskType.Shutdown, treeView);
                    Tasks.Delete(Tasks.TaskType.Logoff, treeView);
                    Tasks.Delete(Tasks.TaskType.Logon, treeView);
                    Tasks.Delete(Tasks.TaskType.Unlock, treeView);
                    Tasks.Delete(Tasks.TaskType.ChargerConnected, treeView);
                }

                // Scopes for Windows sounds
                string[] Scope_Win = [@"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\.Default\{0}\.Current", @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\.Default\{0}\.Modified"];
                string[] Scope_Explorer = [@"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\Explorer\{0}\.Current", @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\Explorer\{0}\.Modified"];
                string[] Scope_SpeechRec = [@"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\sapisvr\{0}\.Current", @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\sapisvr\{0}\.Modified"];
                string[] Scope_NetMeeting = [@"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\Conf\{0}\.Current", @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\Conf\{0}\.Modified"];

                foreach (string Scope in Scope_Win)
                {
                    WriteReg(treeView, string.Format(Scope, ".Default"), string.Empty, Snd_Win_Default, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "AppGPFault"), string.Empty, Snd_Win_AppGPFault, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "CCSelect"), string.Empty, Snd_Win_CCSelect, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "ChangeTheme"), string.Empty, Snd_Win_ChangeTheme, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "Close"), string.Empty, Snd_Win_Close, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "CriticalBatteryAlarm"), string.Empty, Snd_Win_CriticalBatteryAlarm, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "DeviceConnect"), string.Empty, Snd_Win_DeviceConnect, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "DeviceDisconnect"), string.Empty, Snd_Win_DeviceDisconnect, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "DeviceFail"), string.Empty, Snd_Win_DeviceFail, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "FaxBeep"), string.Empty, Snd_Win_FaxBeep, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "LowBatteryAlarm"), string.Empty, Snd_Win_LowBatteryAlarm, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "MailBeep"), string.Empty, Snd_Win_MailBeep, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "Maximize"), string.Empty, Snd_Win_Maximize, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "MenuCommand"), string.Empty, Snd_Win_MenuCommand, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "MenuPopup"), string.Empty, Snd_Win_MenuPopup, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "MessageNudge"), string.Empty, Snd_Win_MessageNudge, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "Minimize"), string.Empty, Snd_Win_Minimize, RegistryValueKind.String);

                    if (!OS.WXP && !OS.WVista && !OS.W7)
                    {
                        WriteReg(treeView, string.Format(Scope, "Notification.Default"), string.Empty, Snd_Win_Notification_Default, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "Notification.IM"), string.Empty, Snd_Win_Notification_IM, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "Notification.Looping.Alarm"), string.Empty, Snd_Win_Notification_Looping_Alarm, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "Notification.Looping.Alarm2"), string.Empty, Snd_Win_Notification_Looping_Alarm2, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "Notification.Looping.Alarm3"), string.Empty, Snd_Win_Notification_Looping_Alarm3, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "Notification.Looping.Alarm4"), string.Empty, Snd_Win_Notification_Looping_Alarm4, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "Notification.Looping.Alarm5"), string.Empty, Snd_Win_Notification_Looping_Alarm5, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "Notification.Looping.Alarm6"), string.Empty, Snd_Win_Notification_Looping_Alarm6, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "Notification.Looping.Alarm7"), string.Empty, Snd_Win_Notification_Looping_Alarm7, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "Notification.Looping.Alarm8"), string.Empty, Snd_Win_Notification_Looping_Alarm8, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "Notification.Looping.Alarm9"), string.Empty, Snd_Win_Notification_Looping_Alarm9, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "Notification.Looping.Alarm10"), string.Empty, Snd_Win_Notification_Looping_Alarm10, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "Notification.Looping.Call"), string.Empty, Snd_Win_Notification_Looping_Call, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "Notification.Looping.Call2"), string.Empty, Snd_Win_Notification_Looping_Call2, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "Notification.Looping.Call3"), string.Empty, Snd_Win_Notification_Looping_Call3, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "Notification.Looping.Call4"), string.Empty, Snd_Win_Notification_Looping_Call4, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "Notification.Looping.Call5"), string.Empty, Snd_Win_Notification_Looping_Call5, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "Notification.Looping.Call6"), string.Empty, Snd_Win_Notification_Looping_Call6, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "Notification.Looping.Call7"), string.Empty, Snd_Win_Notification_Looping_Call7, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "Notification.Looping.Call8"), string.Empty, Snd_Win_Notification_Looping_Call8, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "Notification.Looping.Call9"), string.Empty, Snd_Win_Notification_Looping_Call9, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "Notification.Looping.Call10"), string.Empty, Snd_Win_Notification_Looping_Call10, RegistryValueKind.String);
                    }

                    WriteReg(treeView, string.Format(Scope, "Notification.Mail"), string.Empty, Snd_Win_Notification_Mail, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "Notification.Proximity"), string.Empty, Snd_Win_Notification_Proximity, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "Notification.Reminder"), string.Empty, Snd_Win_Notification_Reminder, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "Notification.SMS"), string.Empty, Snd_Win_Notification_SMS, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "Open"), string.Empty, Snd_Win_Open, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "PrintComplete"), string.Empty, Snd_Win_PrintComplete, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "ProximityConnection"), string.Empty, Snd_Win_ProximityConnection, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "RestoreDown"), string.Empty, Snd_Win_RestoreDown, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "RestoreUp"), string.Empty, Snd_Win_RestoreUp, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "ShowBand"), string.Empty, Snd_Win_ShowBand, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "SystemAsterisk"), string.Empty, Snd_Win_SystemAsterisk, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "SystemExclamation"), string.Empty, Snd_Win_SystemExclamation, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "SystemExit"), string.Empty, Snd_Win_SystemExit, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "SystemStart"), string.Empty, Snd_Win_SystemStart, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "SystemHand"), string.Empty, Snd_Win_SystemHand, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "SystemNotification"), string.Empty, Snd_Win_SystemNotification, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "SystemQuestion"), string.Empty, Snd_Win_SystemQuestion, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "WindowsLogoff"), string.Empty, Snd_Win_WindowsLogoff, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "WindowsLogon"), string.Empty, Snd_Win_WindowsLogon, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "WindowsUAC"), string.Empty, Snd_Win_WindowsUAC, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "WindowsUnlock"), string.Empty, Snd_Win_WindowsUnlock, RegistryValueKind.String);
                }

                foreach (string Scope in Scope_Explorer)
                {
                    WriteReg(treeView, string.Format(Scope, "ActivatingDocument"), string.Empty, Snd_Explorer_ActivatingDocument, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "BlockedPopup"), string.Empty, Snd_Explorer_BlockedPopup, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "EmptyRecycleBin"), string.Empty, Snd_Explorer_EmptyRecycleBin, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "FeedDiscovered"), string.Empty, Snd_Explorer_FeedDiscovered, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "MoveMenuItem"), string.Empty, Snd_Explorer_MoveMenuItem, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "Navigating"), string.Empty, Snd_Explorer_Navigating, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "SecurityBand"), string.Empty, Snd_Explorer_SecurityBand, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "SearchProviderDiscovered"), string.Empty, Snd_Explorer_SearchProviderDiscovered, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "FaxError"), string.Empty, Snd_Explorer_FaxError, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "FaxLineRings"), string.Empty, Snd_Explorer_FaxLineRings, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "FaxNew"), string.Empty, Snd_Explorer_FaxNew, RegistryValueKind.String);
                    WriteReg(treeView, string.Format(Scope, "FaxSent"), string.Empty, Snd_Explorer_FaxSent, RegistryValueKind.String);
                }

                if (OS.WXP)
                {
                    foreach (string Scope in Scope_NetMeeting)
                    {
                        WriteReg(treeView, string.Format(Scope, "Person Joins"), string.Empty, Snd_NetMeeting_PersonJoins, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "Person Leaves"), string.Empty, Snd_NetMeeting_PersonLeaves, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "Receive Call"), string.Empty, Snd_NetMeeting_ReceiveCall, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "Receive Request to Join"), string.Empty, Snd_NetMeeting_ReceiveRequestToJoin, RegistryValueKind.String);
                    }
                }

                if (!OS.WXP)
                {
                    foreach (string Scope in Scope_SpeechRec)
                    {
                        WriteReg(treeView, string.Format(Scope, "DisNumbersSound"), string.Empty, Snd_SpeechRec_DisNumbersSound, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "HubOffSound"), string.Empty, Snd_SpeechRec_HubOffSound, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "HubOnSound"), string.Empty, Snd_SpeechRec_HubOnSound, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "HubSleepSound"), string.Empty, Snd_SpeechRec_HubSleepSound, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "MisrecoSound"), string.Empty, Snd_SpeechRec_MisrecoSound, RegistryValueKind.String);
                        WriteReg(treeView, string.Format(Scope, "PanelSound"), string.Empty, Snd_SpeechRec_PanelSound, RegistryValueKind.String);
                    }
                }

                // Create INI files for Windows sounds used by service
                if (!Directory.Exists(SysPaths.SysEventsSoundsDir))
                {
                    Directory.CreateDirectory(SysPaths.SysEventsSoundsDir);
                    Program.Log?.Write(LogEventLevel.Information, $"Creating directory for Windows sounds INI files: {SysPaths.SysEventsSoundsDir}.");
                }

                INI[] INIs = [new(SysPaths.SysEventsSounds_Local_INI), new(SysPaths.SysEventsSounds_Global_INI)];

                foreach (INI ini in INIs)
                {
                    ini.Write("Power", "Snd_ChargerConnected", Snd_ChargerConnected);
                    ini.Write("Power", "Snd_ChargerDisconnected", Snd_ChargerDisconnected);
                    ini.Write("Windows", "Logoff", Snd_Win_WindowsLogoff);
                    ini.Write("Windows", "Logon", Snd_Win_WindowsLogon);
                    ini.Write("Windows", "Lock", Snd_Win_WindowsLock);
                    ini.Write("Windows", "Unlock", Snd_Win_WindowsUnlock);
                    ini.Write("Windows", "Exit", Snd_Win_SystemExit);
                    ini.Write("Network", "Wi-Fi_Connected", Snd_WiFiConnected);
                    ini.Write("Network", "Wi-Fi_Disconnected", Snd_WiFiDisconnected);
                    ini.Write("Network", "Wi-Fi_ConnectionFailed", Snd_WiFiConnectionFailed);
                }
            }
        }

        /// <summary>
        /// Saves Sounds toggle state into registry
        /// </summary>
        /// <param name="treeView"></param>
        public void SaveToggleState(TreeView treeView = null)
        {
            WriteReg(treeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", string.Empty, Enabled);
        }
    }
}
