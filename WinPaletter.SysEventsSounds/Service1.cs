using Microsoft.Win32;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace WinPaletter.SysEventsSounds
{
    public partial class Service1 : ServiceBase
    {
        #region Variables
        static string GlobalSoundsINI = $"{new System.IO.FileInfo(Assembly.GetEntryAssembly().Location).Directory}\\sounds.ini";
        static string LocalSoundsINI = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\{Application.CompanyName}\\WinPaletter\\sounds.ini";
        private INI ini;
        bool ShuttingDown = false;
        bool unlockPlayed = false;
        bool logoffPlayed = false;
        bool chargerConnectedPlayed = false;
        #endregion

        #region Main
        public Service1()
        {
            InitializeComponent();
            this.ServiceName = "WinPaletter.SystemEventsSounds";
            this.CanHandlePowerEvent = true;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanHandleSessionChangeEvent = true;
            this.CanStop = true;
        }

        protected override void OnStart(string[] args)
        {
            logoffPlayed = false;
            unlockPlayed = false;
            chargerConnectedPlayed = false;

            UpdatePaths();

            StartReceiveEventsWatcher();
            SystemEvents.SessionEnding += SystemEvents_SessionEnding;

            base.OnStart(args);
        }

        protected override void OnStop()
        {
            logoffPlayed = false;
            unlockPlayed = false;
            ShuttingDown = true;
            chargerConnectedPlayed = false;

            UpdatePaths();
            ini = new INI(LocalSoundsINI);
            Play(ini.Read("Windows", "Exit", ""));

            //SystemEvents.PowerModeChanged -= PowerModeChanged;
            SystemEvents.SessionEnding -= SystemEvents_SessionEnding;

            base.OnStop();
        }

        protected override void OnShutdown()
        {
            logoffPlayed = false;
            unlockPlayed = false;
            ShuttingDown = true;
            chargerConnectedPlayed = false;

            base.OnShutdown();
        }
        #endregion

        #region Sounds player
        static SoundPlayer player = new SoundPlayer();
        static bool isPlaying = false;

        public static void Play(string File)
        {
            if (System.IO.File.Exists(File))
            {
                if (isPlaying)
                    player.Stop();

                isPlaying = true;
                player.SoundLocation = System.Environment.ExpandEnvironmentVariables(File);
                try
                {
                    player.Load();
                    player.PlaySync();
                    isPlaying = false;
                }
                catch
                {
                    Stop_Method2();
                    Play_Method2(player.SoundLocation);
                }
            }
        }

        public static void Play_Method2(string File)
        {
            if (System.IO.File.Exists(File))
            {
                isPlaying = false;
                mciSendString("close myWAV", null, 0, (IntPtr)0);
                mciSendString("open \"" + File + "\" type mpegvideo alias myWAV", null, 0, (IntPtr)0);
                mciSendString("play myWAV", null, 0, (IntPtr)0);
                int Volume = 1000; // Sets it to use entire range of volume control
                mciSendString("setaudio myWAV volume to " + Volume.ToString(), null, 0, (IntPtr)0);
                isPlaying = true;
            }
        }

        public static void Stop_Method2()
        {
            mciSendString("seek myWAV to start", null, 0, IntPtr.Zero);
            mciSendString("stop myWAV", null, 0, IntPtr.Zero);
            isPlaying = false;
        }

        [DllImport("winmm.dll")]
        public static extern int mciSendString(string command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);
        #endregion

        #region System events handlers
        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            if (powerStatus == PowerBroadcastStatus.PowerStatusChange) { ChargerSounds(); }
            return base.OnPowerEvent(powerStatus);
        }

        void ChargerSounds()
        {
            if (SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Online)
            {
                if (!chargerConnectedPlayed)
                {
                    //Charger connected, and make a lock by chargerConnectedPlayed to avoid double sound playing bug
                    chargerConnectedPlayed = true;
                    UpdatePaths();
                    ini = new INI(LocalSoundsINI);
                    Play(ini.Read("Power", "Snd_ChargerConnected", ""));
                }

            }
            else if (SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Offline)
            {
                //Charger disconnected
                UpdatePaths();
                ini = new INI(LocalSoundsINI);
                Play(ini.Read("Power", "Snd_ChargerDisconnected", ""));
                chargerConnectedPlayed = false;
            }
        }

        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            string UserName = GetUsername(changeDescription.SessionId);

            //Logoff (and restart) always invoked with SYSTEM account, while others not.
            bool isSystem = UserName.ToUpper() == "SYSTEM";

            bool oldSystems = OS.WXP || OS.WVista || OS.W7;

            UpdatePaths();

            ini = isSystem ? new INI(GlobalSoundsINI) : new INI(LocalSoundsINI);

            switch (changeDescription.Reason)
            {
                case SessionChangeReason.SessionLogoff:
                    {
                        if (!oldSystems)
                        {
                            if (!ShuttingDown)
                            {
                                Play(ini.Read("Windows", "Logoff", ""));
                                logoffPlayed = true;
                            }
                        }
                        break;
                    }

                case SessionChangeReason.SessionLogon:
                    {
                        if (!isSystem && !oldSystems && logoffPlayed) { Play(ini.Read("Windows", "Logon", "")); }
                        break;
                    }

                case SessionChangeReason.SessionLock:
                    {
                        if (!isSystem && !oldSystems && unlockPlayed) { Play(ini.Read("Windows", "Lock", "")); }
                        break;
                    }

                case SessionChangeReason.SessionUnlock:
                    {
                        if (!isSystem && !oldSystems) { Play(ini.Read("Windows", "Unlock", "")); unlockPlayed = true; }
                        break;
                    }

                default: { break; }
            }

            base.OnSessionChange(changeDescription);
        }

        private void ReceiveEvent_ShutdownOrReboot(Object obj, EventRecordWrittenEventArgs arg)
        {
            logoffPlayed = false;
            unlockPlayed = false;
            ShuttingDown = true;
            chargerConnectedPlayed = false;

            UpdatePaths();
            ini = new INI(LocalSoundsINI);
            Play(ini.Read("Windows", "Exit", ""));
        }

        private void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            if (e.Reason == SessionEndReasons.SystemShutdown)
            {
                logoffPlayed = false;
                unlockPlayed = false;
                ShuttingDown = true;
                chargerConnectedPlayed = false;

                UpdatePaths();
                ini = new INI(LocalSoundsINI);
                Play(ini.Read("Windows", "Exit", ""));
            }
        }
        #endregion

        #region Helpers
        [DllImport("Wtsapi32.dll")]
        private static extern bool WTSQuerySessionInformation(System.IntPtr hServer, int sessionId, WtsInfoClass wtsInfoClass, out System.IntPtr ppBuffer, out int pBytesReturned);

        [DllImport("Wtsapi32.dll")]
        private static extern void WTSFreeMemory(System.IntPtr pointer);

        [DllImport("kernel32.dll")]
        static extern uint WTSGetActiveConsoleSessionId();

        private enum WtsInfoClass
        {
            WTSUserName = 5,
            WTSDomainName = 7,
        }

        private static string GetUsername(int sessionId, bool prependDomain = false)
        {
            IntPtr buffer;
            int strLen;
            string username = "SYSTEM";
            if (WTSQuerySessionInformation(IntPtr.Zero, sessionId, WtsInfoClass.WTSUserName, out buffer, out strLen) && strLen > 1)
            {
                username = Marshal.PtrToStringAnsi(buffer);
                WTSFreeMemory(buffer);
                if (prependDomain)
                {
                    if (WTSQuerySessionInformation(IntPtr.Zero, sessionId, WtsInfoClass.WTSDomainName, out buffer, out strLen) && strLen > 1)
                    {
                        username = Marshal.PtrToStringAnsi(buffer) + "\\" + username;
                        WTSFreeMemory(buffer);
                    }
                }
            }
            return username;
        }

        [DllImport("wtsapi32.dll")]
        public static extern bool WTSQueryUserToken(uint sessionId, out IntPtr phToken);

        static void UpdatePaths()
        {
            uint sessionId = WTSGetActiveConsoleSessionId(); // Get the session ID from your source
            IntPtr userToken;

            if (WTSQueryUserToken(sessionId, out userToken))
            {
                WindowsIdentity windowsIdentity = new WindowsIdentity(userToken);
                using (WindowsImpersonationContext wic = windowsIdentity.Impersonate())
                {
                    LocalSoundsINI = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\{Application.CompanyName}\\WinPaletter\\sounds.ini";
                }
            }
            else
            {
                LocalSoundsINI = GlobalSoundsINI;
            }
        }

        private void StartReceiveEventsWatcher()
        {
            EventLogQuery logQuery0 = new EventLogQuery("System", PathType.LogName, "*[System[(EventID = 1074)]]");
            EventLogWatcher logWatcher0 = new EventLogWatcher(logQuery0);
            logWatcher0.EventRecordWritten += new EventHandler<EventRecordWrittenEventArgs>(ReceiveEvent_ShutdownOrReboot);
            logWatcher0.Enabled = true;

            EventLogQuery logQuery1 = new EventLogQuery("System", PathType.LogName, "*[System[(EventID = 109)]]");
            EventLogWatcher logWatcher1 = new EventLogWatcher(logQuery1);
            logWatcher1.EventRecordWritten += new EventHandler<EventRecordWrittenEventArgs>(ReceiveEvent_ShutdownOrReboot);
            logWatcher1.Enabled = true;

            EventLogQuery logQuery2 = new EventLogQuery("System", PathType.LogName, "*[System[(EventID = 41)]]");
            EventLogWatcher logWatcher2 = new EventLogWatcher(logQuery2);
            logWatcher2.EventRecordWritten += new EventHandler<EventRecordWrittenEventArgs>(ReceiveEvent_ShutdownOrReboot);
            logWatcher2.Enabled = true;

            EventLogQuery logQuery3 = new EventLogQuery("System", PathType.LogName, "*[System[(EventID = 6006)]]");
            EventLogWatcher logWatcher3 = new EventLogWatcher(logQuery2);
            logWatcher3.EventRecordWritten += new EventHandler<EventRecordWrittenEventArgs>(ReceiveEvent_ShutdownOrReboot);
            logWatcher3.Enabled = true;

            EventLogQuery logQuery4 = new EventLogQuery("System", PathType.LogName, "*[System[(EventID = 6008)]]");
            EventLogWatcher logWatcher4 = new EventLogWatcher(logQuery2);
            logWatcher4.EventRecordWritten += new EventHandler<EventRecordWrittenEventArgs>(ReceiveEvent_ShutdownOrReboot);
            logWatcher4.Enabled = true;
        }
        #endregion
    }
}
