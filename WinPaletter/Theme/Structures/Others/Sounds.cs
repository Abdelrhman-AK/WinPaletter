using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;

namespace WinPaletter.Theme.Structures
{
    public struct Sounds : ICloneable, IDisposable
    {
        public bool Enabled;

        public string Snd_Win_Default;
        public string Snd_Win_AppGPFault;
        public string Snd_Win_CCSelect;
        public string Snd_Win_ChangeTheme;
        public string Snd_Win_Close;
        public string Snd_Win_CriticalBatteryAlarm;
        public string Snd_Win_DeviceConnect;
        public string Snd_Win_DeviceDisconnect;
        public string Snd_Win_DeviceFail;
        public string Snd_Win_FaxBeep;
        public string Snd_Win_LowBatteryAlarm;
        public string Snd_Win_MailBeep;
        public string Snd_Win_Maximize;
        public string Snd_Win_MenuCommand;
        public string Snd_Win_MenuPopup;
        public string Snd_Win_MessageNudge;
        public string Snd_Win_Minimize;
        public string Snd_Win_Notification_Default;
        public string Snd_Win_Notification_IM;
        public string Snd_Win_Notification_Looping_Alarm;
        public string Snd_Win_Notification_Looping_Alarm10;
        public string Snd_Win_Notification_Looping_Alarm2;
        public string Snd_Win_Notification_Looping_Alarm3;
        public string Snd_Win_Notification_Looping_Alarm4;
        public string Snd_Win_Notification_Looping_Alarm5;
        public string Snd_Win_Notification_Looping_Alarm6;
        public string Snd_Win_Notification_Looping_Alarm7;
        public string Snd_Win_Notification_Looping_Alarm8;
        public string Snd_Win_Notification_Looping_Alarm9;
        public string Snd_Win_Notification_Looping_Call;
        public string Snd_Win_Notification_Looping_Call10;
        public string Snd_Win_Notification_Looping_Call2;
        public string Snd_Win_Notification_Looping_Call3;
        public string Snd_Win_Notification_Looping_Call4;
        public string Snd_Win_Notification_Looping_Call5;
        public string Snd_Win_Notification_Looping_Call6;
        public string Snd_Win_Notification_Looping_Call7;
        public string Snd_Win_Notification_Looping_Call8;
        public string Snd_Win_Notification_Looping_Call9;
        public string Snd_Win_Notification_Mail;
        public string Snd_Win_Notification_Proximity;
        public string Snd_Win_Notification_Reminder;
        public string Snd_Win_Notification_SMS;
        public string Snd_Win_Open;
        public string Snd_Win_PrintComplete;
        public string Snd_Win_ProximityConnection;
        public string Snd_Win_RestoreDown;
        public string Snd_Win_RestoreUp;
        public string Snd_Win_ShowBand;
        public string Snd_Win_SystemAsterisk;
        public string Snd_Win_SystemExclamation;
        public string Snd_Win_SystemExit;
        public string Snd_Win_SystemStart;
        public string Snd_Imageres_SystemStart;
        public string Snd_Win_SystemHand;
        public string Snd_Win_SystemNotification;
        public string Snd_Win_SystemQuestion;
        public string Snd_Win_WindowsLogoff;
        public string Snd_Win_WindowsLogon;
        public string Snd_Win_WindowsUAC;
        public string Snd_Win_WindowsUnlock;
        public string Snd_Explorer_ActivatingDocument;
        public string Snd_Explorer_BlockedPopup;
        public string Snd_Explorer_EmptyRecycleBin;
        public string Snd_Explorer_FeedDiscovered;
        public string Snd_Explorer_MoveMenuItem;
        public string Snd_Explorer_Navigating;
        public string Snd_Explorer_SecurityBand;
        public string Snd_Explorer_SearchProviderDiscovered;
        public string Snd_Explorer_FaxError;
        public string Snd_Explorer_FaxLineRings;
        public string Snd_Explorer_FaxNew;
        public string Snd_Explorer_FaxSent;
        public string Snd_NetMeeting_PersonJoins;
        public string Snd_NetMeeting_PersonLeaves;
        public string Snd_NetMeeting_ReceiveCall;
        public string Snd_NetMeeting_ReceiveRequestToJoin;
        public string Snd_SpeechRec_DisNumbersSound;
        public string Snd_SpeechRec_HubOffSound;
        public string Snd_SpeechRec_HubOnSound;
        public string Snd_SpeechRec_HubSleepSound;
        public string Snd_SpeechRec_MisrecoSound;
        public string Snd_SpeechRec_PanelSound;

        public bool Snd_Win_SystemExit_TaskMgmt;
        public bool Snd_Win_WindowsLogoff_TaskMgmt;
        public bool Snd_Win_WindowsLogon_TaskMgmt;
        public bool Snd_Win_WindowsUnlock_TaskMgmt;
        public string Snd_ChargerConnected;

        public void Load(Sounds _DefSounds)
        {
            Enabled = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "", _DefSounds.Enabled));
            Snd_Imageres_SystemStart = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Imageres.dll_Startup", _DefSounds.Snd_Imageres_SystemStart).ToString();
            Snd_ChargerConnected = GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_ChargerConnected", _DefSounds.Snd_ChargerConnected).ToString();

            Snd_Win_SystemExit_TaskMgmt = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_SystemExit_TaskMgmt", _DefSounds.Snd_Win_SystemExit_TaskMgmt));
            Snd_Win_WindowsLogoff_TaskMgmt = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_WindowsLogoff_TaskMgmt", _DefSounds.Snd_Win_WindowsLogoff_TaskMgmt));
            Snd_Win_WindowsLogon_TaskMgmt = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_WindowsLogon_TaskMgmt", _DefSounds.Snd_Win_WindowsLogon_TaskMgmt));
            Snd_Win_WindowsUnlock_TaskMgmt = Convert.ToBoolean(GetReg(@"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_WindowsUnlock_TaskMgmt", _DefSounds.Snd_Win_WindowsUnlock_TaskMgmt));

            string Scope_Win = @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\.Default\{0}\.Current";
            Snd_Win_Default = GetReg(string.Format(Scope_Win, ".Default"), "", _DefSounds.Snd_Win_Default).ToString();
            Snd_Win_AppGPFault = GetReg(string.Format(Scope_Win, "AppGPFault"), "", _DefSounds.Snd_Win_AppGPFault).ToString();
            Snd_Win_CCSelect = GetReg(string.Format(Scope_Win, "CCSelect"), "", _DefSounds.Snd_Win_CCSelect).ToString();
            Snd_Win_ChangeTheme = GetReg(string.Format(Scope_Win, "ChangeTheme"), "", _DefSounds.Snd_Win_ChangeTheme).ToString();
            Snd_Win_Close = GetReg(string.Format(Scope_Win, "Close"), "", _DefSounds.Snd_Win_Close).ToString();
            Snd_Win_CriticalBatteryAlarm = GetReg(string.Format(Scope_Win, "CriticalBatteryAlarm"), "", _DefSounds.Snd_Win_CriticalBatteryAlarm).ToString();
            Snd_Win_DeviceConnect = GetReg(string.Format(Scope_Win, "DeviceConnect"), "", _DefSounds.Snd_Win_DeviceConnect).ToString();
            Snd_Win_DeviceDisconnect = GetReg(string.Format(Scope_Win, "DeviceDisconnect"), "", _DefSounds.Snd_Win_DeviceDisconnect).ToString();
            Snd_Win_DeviceFail = GetReg(string.Format(Scope_Win, "DeviceFail"), "", _DefSounds.Snd_Win_DeviceFail).ToString();
            Snd_Win_FaxBeep = GetReg(string.Format(Scope_Win, "FaxBeep"), "", _DefSounds.Snd_Win_FaxBeep).ToString();
            Snd_Win_LowBatteryAlarm = GetReg(string.Format(Scope_Win, "LowBatteryAlarm"), "", _DefSounds.Snd_Win_LowBatteryAlarm).ToString();
            Snd_Win_MailBeep = GetReg(string.Format(Scope_Win, "MailBeep"), "", _DefSounds.Snd_Win_MailBeep).ToString();
            Snd_Win_Maximize = GetReg(string.Format(Scope_Win, "Maximize"), "", _DefSounds.Snd_Win_Maximize).ToString();
            Snd_Win_MenuCommand = GetReg(string.Format(Scope_Win, "MenuCommand"), "", _DefSounds.Snd_Win_MenuCommand).ToString();
            Snd_Win_MenuPopup = GetReg(string.Format(Scope_Win, "MenuPopup"), "", _DefSounds.Snd_Win_MenuPopup).ToString();
            Snd_Win_MessageNudge = GetReg(string.Format(Scope_Win, "MessageNudge"), "", _DefSounds.Snd_Win_MessageNudge).ToString();
            Snd_Win_Minimize = GetReg(string.Format(Scope_Win, "Minimize"), "", _DefSounds.Snd_Win_Minimize).ToString();
            Snd_Win_Notification_Default = GetReg(string.Format(Scope_Win, "Notification.Default"), "", _DefSounds.Snd_Win_Notification_Default).ToString();
            Snd_Win_Notification_IM = GetReg(string.Format(Scope_Win, "Notification.IM"), "", _DefSounds.Snd_Win_Notification_IM).ToString();
            Snd_Win_Notification_Looping_Alarm = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm).ToString();
            Snd_Win_Notification_Looping_Alarm2 = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm2"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm2).ToString();
            Snd_Win_Notification_Looping_Alarm3 = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm3"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm3).ToString();
            Snd_Win_Notification_Looping_Alarm4 = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm4"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm4).ToString();
            Snd_Win_Notification_Looping_Alarm5 = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm5"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm5).ToString();
            Snd_Win_Notification_Looping_Alarm6 = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm6"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm6).ToString();
            Snd_Win_Notification_Looping_Alarm7 = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm7"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm7).ToString();
            Snd_Win_Notification_Looping_Alarm8 = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm8"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm8).ToString();
            Snd_Win_Notification_Looping_Alarm9 = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm9"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm9).ToString();
            Snd_Win_Notification_Looping_Alarm10 = GetReg(string.Format(Scope_Win, "Notification.Looping.Alarm10"), "", _DefSounds.Snd_Win_Notification_Looping_Alarm10).ToString();
            Snd_Win_Notification_Looping_Call = GetReg(string.Format(Scope_Win, "Notification.Looping.Call"), "", _DefSounds.Snd_Win_Notification_Looping_Call).ToString();
            Snd_Win_Notification_Looping_Call2 = GetReg(string.Format(Scope_Win, "Notification.Looping.Call2"), "", _DefSounds.Snd_Win_Notification_Looping_Call2).ToString();
            Snd_Win_Notification_Looping_Call3 = GetReg(string.Format(Scope_Win, "Notification.Looping.Call3"), "", _DefSounds.Snd_Win_Notification_Looping_Call3).ToString();
            Snd_Win_Notification_Looping_Call4 = GetReg(string.Format(Scope_Win, "Notification.Looping.Call4"), "", _DefSounds.Snd_Win_Notification_Looping_Call4).ToString();
            Snd_Win_Notification_Looping_Call5 = GetReg(string.Format(Scope_Win, "Notification.Looping.Call5"), "", _DefSounds.Snd_Win_Notification_Looping_Call5).ToString();
            Snd_Win_Notification_Looping_Call6 = GetReg(string.Format(Scope_Win, "Notification.Looping.Call6"), "", _DefSounds.Snd_Win_Notification_Looping_Call6).ToString();
            Snd_Win_Notification_Looping_Call7 = GetReg(string.Format(Scope_Win, "Notification.Looping.Call7"), "", _DefSounds.Snd_Win_Notification_Looping_Call7).ToString();
            Snd_Win_Notification_Looping_Call8 = GetReg(string.Format(Scope_Win, "Notification.Looping.Call8"), "", _DefSounds.Snd_Win_Notification_Looping_Call8).ToString();
            Snd_Win_Notification_Looping_Call9 = GetReg(string.Format(Scope_Win, "Notification.Looping.Call9"), "", _DefSounds.Snd_Win_Notification_Looping_Call9).ToString();
            Snd_Win_Notification_Looping_Call10 = GetReg(string.Format(Scope_Win, "Notification.Looping.Call10"), "", _DefSounds.Snd_Win_Notification_Looping_Call10).ToString();
            Snd_Win_Notification_Mail = GetReg(string.Format(Scope_Win, "Notification.Mail"), "", _DefSounds.Snd_Win_Notification_Mail).ToString();
            Snd_Win_Notification_Proximity = GetReg(string.Format(Scope_Win, "Notification.Proximity"), "", _DefSounds.Snd_Win_Notification_Proximity).ToString();
            Snd_Win_Notification_Reminder = GetReg(string.Format(Scope_Win, "Notification.Reminder"), "", _DefSounds.Snd_Win_Notification_Reminder).ToString();
            Snd_Win_Notification_SMS = GetReg(string.Format(Scope_Win, "Notification.SMS"), "", _DefSounds.Snd_Win_Notification_SMS).ToString();
            Snd_Win_Open = GetReg(string.Format(Scope_Win, "Open"), "", _DefSounds.Snd_Win_Open).ToString();
            Snd_Win_PrintComplete = GetReg(string.Format(Scope_Win, "PrintComplete"), "", _DefSounds.Snd_Win_PrintComplete).ToString();
            Snd_Win_ProximityConnection = GetReg(string.Format(Scope_Win, "ProximityConnection"), "", _DefSounds.Snd_Win_ProximityConnection).ToString();
            Snd_Win_RestoreDown = GetReg(string.Format(Scope_Win, "RestoreDown"), "", _DefSounds.Snd_Win_RestoreDown).ToString();
            Snd_Win_RestoreUp = GetReg(string.Format(Scope_Win, "RestoreUp"), "", _DefSounds.Snd_Win_RestoreUp).ToString();
            Snd_Win_ShowBand = GetReg(string.Format(Scope_Win, "ShowBand"), "", _DefSounds.Snd_Win_ShowBand).ToString();
            Snd_Win_SystemAsterisk = GetReg(string.Format(Scope_Win, "SystemAsterisk"), "", _DefSounds.Snd_Win_SystemAsterisk).ToString();
            Snd_Win_SystemExclamation = GetReg(string.Format(Scope_Win, "SystemExclamation"), "", _DefSounds.Snd_Win_SystemExclamation).ToString();
            Snd_Win_SystemExit = GetReg(string.Format(Scope_Win, "SystemExit"), "", _DefSounds.Snd_Win_SystemExit).ToString();
            Snd_Win_SystemStart = GetReg(string.Format(Scope_Win, "SystemStart"), "", _DefSounds.Snd_Win_SystemStart).ToString();
            Snd_Win_SystemHand = GetReg(string.Format(Scope_Win, "SystemHand"), "", _DefSounds.Snd_Win_SystemHand).ToString();
            Snd_Win_SystemNotification = GetReg(string.Format(Scope_Win, "SystemNotification"), "", _DefSounds.Snd_Win_SystemNotification).ToString();
            Snd_Win_SystemQuestion = GetReg(string.Format(Scope_Win, "SystemQuestion"), "", _DefSounds.Snd_Win_SystemQuestion).ToString();
            Snd_Win_WindowsLogoff = GetReg(string.Format(Scope_Win, "WindowsLogoff"), "", _DefSounds.Snd_Win_WindowsLogoff).ToString();
            Snd_Win_WindowsLogon = GetReg(string.Format(Scope_Win, "WindowsLogon"), "", _DefSounds.Snd_Win_WindowsLogon).ToString();
            Snd_Win_WindowsUAC = GetReg(string.Format(Scope_Win, "WindowsUAC"), "", _DefSounds.Snd_Win_WindowsUAC).ToString();
            Snd_Win_WindowsUnlock = GetReg(string.Format(Scope_Win, "WindowsUnlock"), "", _DefSounds.Snd_Win_WindowsUnlock).ToString();

            string Scope_Explorer = @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\Explorer\{0}\.Current";
            Snd_Explorer_ActivatingDocument = GetReg(string.Format(Scope_Explorer, "ActivatingDocument"), "", _DefSounds.Snd_Explorer_ActivatingDocument).ToString();
            Snd_Explorer_BlockedPopup = GetReg(string.Format(Scope_Explorer, "BlockedPopup"), "", _DefSounds.Snd_Explorer_BlockedPopup).ToString();
            Snd_Explorer_EmptyRecycleBin = GetReg(string.Format(Scope_Explorer, "EmptyRecycleBin"), "", _DefSounds.Snd_Explorer_EmptyRecycleBin).ToString();
            Snd_Explorer_FeedDiscovered = GetReg(string.Format(Scope_Explorer, "FeedDiscovered"), "", _DefSounds.Snd_Explorer_FeedDiscovered).ToString();
            Snd_Explorer_MoveMenuItem = GetReg(string.Format(Scope_Explorer, "MoveMenuItem"), "", _DefSounds.Snd_Explorer_MoveMenuItem).ToString();
            Snd_Explorer_Navigating = GetReg(string.Format(Scope_Explorer, "Navigating"), "", _DefSounds.Snd_Explorer_Navigating).ToString();
            Snd_Explorer_SecurityBand = GetReg(string.Format(Scope_Explorer, "SecurityBand"), "", _DefSounds.Snd_Explorer_SecurityBand).ToString();
            Snd_Explorer_SearchProviderDiscovered = GetReg(string.Format(Scope_Explorer, "SearchProviderDiscovered"), "", _DefSounds.Snd_Explorer_SearchProviderDiscovered).ToString();
            Snd_Explorer_FaxError = GetReg(string.Format(Scope_Explorer, "FaxError"), "", _DefSounds.Snd_Explorer_FaxError).ToString();
            Snd_Explorer_FaxLineRings = GetReg(string.Format(Scope_Explorer, "FaxLineRings"), "", _DefSounds.Snd_Explorer_FaxLineRings).ToString();
            Snd_Explorer_FaxNew = GetReg(string.Format(Scope_Explorer, "FaxNew"), "", _DefSounds.Snd_Explorer_FaxNew).ToString();
            Snd_Explorer_FaxSent = GetReg(string.Format(Scope_Explorer, "FaxSent"), "", _DefSounds.Snd_Explorer_FaxSent).ToString();

            string Scope_NetMeeting = @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\Conf\{0}\.Current";
            Snd_NetMeeting_PersonJoins = GetReg(string.Format(Scope_NetMeeting, "Person Joins"), "", _DefSounds.Snd_NetMeeting_PersonJoins).ToString();
            Snd_NetMeeting_PersonLeaves = GetReg(string.Format(Scope_NetMeeting, "Person Leaves"), "", _DefSounds.Snd_NetMeeting_PersonLeaves).ToString();
            Snd_NetMeeting_ReceiveCall = GetReg(string.Format(Scope_NetMeeting, "Receive Call"), "", _DefSounds.Snd_NetMeeting_ReceiveCall).ToString();
            Snd_NetMeeting_ReceiveRequestToJoin = GetReg(string.Format(Scope_NetMeeting, "Receive Request to Join"), "", _DefSounds.Snd_NetMeeting_ReceiveRequestToJoin).ToString();

            string Scope_SpeechRec = @"HKEY_CURRENT_USER\AppEvents\Schemes\Apps\sapisvr\{0}\.current";
            Snd_SpeechRec_DisNumbersSound = GetReg(string.Format(Scope_SpeechRec, "DisNumbersSound"), "", _DefSounds.Snd_SpeechRec_DisNumbersSound).ToString();
            Snd_SpeechRec_HubOffSound = GetReg(string.Format(Scope_SpeechRec, "HubOffSound"), "", _DefSounds.Snd_SpeechRec_HubOffSound).ToString();
            Snd_SpeechRec_HubOnSound = GetReg(string.Format(Scope_SpeechRec, "HubOnSound"), "", _DefSounds.Snd_SpeechRec_HubOnSound).ToString();
            Snd_SpeechRec_HubSleepSound = GetReg(string.Format(Scope_SpeechRec, "HubSleepSound"), "", _DefSounds.Snd_SpeechRec_HubSleepSound).ToString();
            Snd_SpeechRec_MisrecoSound = GetReg(string.Format(Scope_SpeechRec, "MisrecoSound"), "", _DefSounds.Snd_SpeechRec_MisrecoSound).ToString();
            Snd_SpeechRec_PanelSound = GetReg(string.Format(Scope_SpeechRec, "PanelSound"), "", _DefSounds.Snd_SpeechRec_PanelSound).ToString();

        }

        public void Apply(TreeView TreeView = null)
        {
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "", Enabled);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Imageres.dll_Startup", Snd_Imageres_SystemStart, RegistryValueKind.String);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_ChargerConnected", Snd_ChargerConnected, RegistryValueKind.String);

            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_SystemExit_TaskMgmt", Snd_Win_SystemExit_TaskMgmt);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_WindowsLogoff_TaskMgmt", Snd_Win_WindowsLogoff_TaskMgmt);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_WindowsLogon_TaskMgmt", Snd_Win_WindowsLogon_TaskMgmt);
            EditReg(TreeView, @"HKEY_CURRENT_USER\Software\WinPaletter\Sounds", "Snd_Win_WindowsUnlock_TaskMgmt", Snd_Win_WindowsUnlock_TaskMgmt);

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
                    EditReg_CMD(TreeView, destination_StartupSnd[0], "DisableStartupSound", (!Program.W11).ToInteger());
                    EditReg_CMD(TreeView, destination_StartupSnd[1], "DisableStartupSound", (!Program.W11).ToInteger());
                }

                else
                {
                    EditReg_CMD(TreeView, destination_StartupSnd[0], "DisableStartupSound", 1);
                    EditReg_CMD(TreeView, destination_StartupSnd[1], "DisableStartupSound", 1);
                }

                if (!Program.WXP)
                {
                    if (File.Exists(Snd_Imageres_SystemStart) && Path.GetExtension(Snd_Imageres_SystemStart).ToUpper() == ".WAV")
                    {

                        byte[] CurrentSoundBytes = PE.GetResource(Program.PATH_imageres, "WAVE", Program.WVista ? 5051 : 5080);
                        byte[] TargetSoundBytes = File.ReadAllBytes(Snd_Imageres_SystemStart);

                        if (!CurrentSoundBytes.Equals(TargetSoundBytes))
                        {
                            PE.ReplaceResource(TreeView, Program.PATH_imageres, "WAVE", Program.WVista ? 5051 : 5080, TargetSoundBytes);
                        }
                    }

                    else if (Snd_Imageres_SystemStart.Trim().ToUpper() == "DEFAULT")
                    {
                        byte[] CurrentSoundBytes = PE.GetResource(Program.PATH_imageres, "WAVE", Program.WVista ? 5051 : 5080);
                        byte[] OriginalSoundBytes = File.ReadAllBytes(Program.PATH_appData + @"\WindowsStartup_Backup.wav");

                        if (!CurrentSoundBytes.Equals(OriginalSoundBytes))
                        {
                            PE.ReplaceResource(TreeView, Program.PATH_imageres, "WAVE", Program.WVista ? 5051 : 5080, OriginalSoundBytes);
                        }

                        if (Program.Settings.ThemeApplyingBehavior.SFC_on_restoring_StartupSound)
                            SFC(Program.PATH_imageres);
                    }

                }

                if (Program.W8 | Program.W81 | Program.W10 | Program.W11 | Program.W12)
                {
                    if (Snd_Win_SystemExit_TaskMgmt && File.Exists(Snd_Win_SystemExit) && Path.GetExtension(Snd_Win_SystemExit).ToUpper() == ".WAV")
                    {
                        TaskMgmt(TaskType.Shutdown, Actions.Add, Snd_Win_SystemExit, TreeView);
                    }
                    else
                    {
                        TaskMgmt(TaskType.Shutdown, Actions.Delete, "", TreeView);
                    }

                    if (Snd_Win_WindowsLogoff_TaskMgmt && File.Exists(Snd_Win_WindowsLogoff) && Path.GetExtension(Snd_Win_WindowsLogoff).ToUpper() == ".WAV")
                    {
                        TaskMgmt(TaskType.Logoff, Actions.Add, Snd_Win_WindowsLogoff, TreeView);
                    }
                    else
                    {
                        TaskMgmt(TaskType.Logoff, Actions.Delete, "", TreeView);
                    }

                    if (Snd_Win_WindowsLogon_TaskMgmt && File.Exists(Snd_Win_WindowsLogon) && Path.GetExtension(Snd_Win_WindowsLogon).ToUpper() == ".WAV")
                    {
                        TaskMgmt(TaskType.Logon, Actions.Add, Snd_Win_WindowsLogon, TreeView);
                    }
                    else
                    {
                        TaskMgmt(TaskType.Logon, Actions.Delete, "", TreeView);
                    }

                    if (Snd_Win_WindowsUnlock_TaskMgmt && File.Exists(Snd_Win_WindowsUnlock) && Path.GetExtension(Snd_Win_WindowsUnlock).ToUpper() == ".WAV")
                    {
                        TaskMgmt(TaskType.Unlock, Actions.Add, Snd_Win_WindowsUnlock, TreeView);
                    }
                    else
                    {
                        TaskMgmt(TaskType.Unlock, Actions.Delete, "", TreeView);
                    }
                }

                if (File.Exists(Snd_ChargerConnected) && Path.GetExtension(Snd_ChargerConnected).ToUpper() == ".WAV")
                {
                    TaskMgmt(TaskType.ChargerConnected, Actions.Add, Snd_ChargerConnected, TreeView);
                }
                else
                {
                    TaskMgmt(TaskType.ChargerConnected, Actions.Delete, "", TreeView);
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

            }

        }

        public static bool operator ==(Sounds First, Sounds Second)
        {
            return First.Equals(Second);
        }

        public static bool operator !=(Sounds First, Sounds Second)
        {
            return !First.Equals(Second);
        }
        public object Clone()
        {
            return MemberwiseClone();
        }

        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }
    }
}
