using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinPaletter.SysEventsSounds
{
    internal class Program
    {
        #region Variables
        static internal Program p = new Program();
        static bool exitSystem = false;
        private delegate bool EventHandler(CtrlType sig);
        static EventHandler ConsoleHandler;

        enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }
        #endregion

        #region Native methods
        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);
        #endregion

        #region Console exit handler
        private static bool Handler(CtrlType sig)
        {
            Exit(null, null);

            //allow main to run off
            exitSystem = true;

            //shutdown right away so there are no lingering threads
            Environment.Exit(-1);

            return true;
        }
        #endregion

        #region System events handlers
        static void PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            PowerStatus powerStatus = SystemInformation.PowerStatus;

            if (powerStatus.PowerLineStatus == PowerLineStatus.Online)
            {
                Console.WriteLine("- Event received: ChargerConnected");
                Play(GetReg("HKEY_CURRENT_USER\\Software\\WinPaletter\\Sounds", "Snd_ChargerConnected", "").ToString());
            }
            else
            {
                Console.WriteLine("- Event received: ChargerDisconnected");
                Play(GetReg("HKEY_CURRENT_USER\\Software\\WinPaletter\\Sounds", "Snd_ChargerDisconnected", "").ToString());
            }

        }
        #endregion

        #region Sounds player
        static SoundPlayer player = new SoundPlayer();
        static bool isPlaying = false;

        async public static void Play(string File)
        {
            if (System.IO.File.Exists(File))
            {
                if (isPlaying)
                    player.Stop();

                await Task.Run(() => { isPlaying = true; player.SoundLocation = File; player.Load(); player.PlaySync(); isPlaying = false; });
            }

        }
        #endregion

        public static object GetReg(string KeyName, string ValueName, object DefaultValue)
        {
            object Result = null;
            RegistryKey R = null;

            if (KeyName.StartsWith(@"Computer\", (StringComparison)5))
                KeyName = KeyName.Remove(0, @"Computer\".Count());

            if (KeyName.StartsWith("HKEY_CURRENT_USER", (StringComparison)5))
            {
                KeyName = KeyName.Remove(0, @"HKEY_CURRENT_USER\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
            }

            else if (KeyName.StartsWith("HKEY_USERS", (StringComparison)5))
            {
                KeyName = KeyName.Remove(0, @"HKEY_USERS\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Registry32);
            }

            else if (KeyName.StartsWith("HKEY_LOCAL_MACHINE", (StringComparison)5))
            {
                KeyName = KeyName.Remove(0, @"HKEY_LOCAL_MACHINE\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Default);
            }

            else if (KeyName.StartsWith("HKEY_CLASSES_ROOT", (StringComparison)5))
            {
                KeyName = KeyName.Remove(0, @"HKEY_CLASSES_ROOT\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry32);
            }

            else if (KeyName.StartsWith("HKEY_CURRENT_CONFIG", (StringComparison)5))
            {
                KeyName = KeyName.Remove(0, @"HKEY_CURRENT_CONFIG\".Count());
                R = RegistryKey.OpenBaseKey(RegistryHive.CurrentConfig, RegistryView.Registry32);
            }

            try
            {
                if (R.OpenSubKey(KeyName, (RegistryKeyPermissionCheck)Convert.ToInt32(false), RegistryRights.ReadKey) != null)
                    Result = R.OpenSubKey(KeyName, (RegistryKeyPermissionCheck)Convert.ToInt32(false), RegistryRights.ReadKey).GetValue(ValueName, DefaultValue);
                try
                {
                    if (R != null)
                    {
                        R.Flush();
                        R.Close();
                        R.Dispose();
                    }
                }
                catch
                {
                }
                return Result is null ? DefaultValue : Result;
            }
            catch { return DefaultValue; }
        }

        static void Main(string[] args)
        {
            ConsoleHandler += new EventHandler(Handler);
            SetConsoleCtrlHandler(ConsoleHandler, true);

            //start multi threaded console
            p.Start(args);

            //hold the console so it doesn't run off the end
            while (!exitSystem)
            {
                Thread.Sleep(500);
            }
        }

        public void Start(string[] args)
        {
            SystemEvents.PowerModeChanged += PowerModeChanged;
            Application.ApplicationExit += Exit;
            Console.ReadLine();
        }

        static void Exit(object sender, EventArgs e)
        {
            SystemEvents.PowerModeChanged -= PowerModeChanged;
        }
    }
}
