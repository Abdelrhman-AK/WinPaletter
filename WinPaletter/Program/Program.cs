using Microsoft.Win32;
using System;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace WinPaletter
{
    internal partial class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppDomain.CurrentDomain.AssemblyResolve += DomainCheck;
            AppDomain.CurrentDomain.UnhandledException += Domain_UnhandledException;
            Application.ThreadException += ThreadExceptionHandler;
            Application.ApplicationExit += OnExit;

            if (!IsSecondInstance())
            {
                InitializeApplication();
                Application.Run(Forms.MainFrm);
            }
            else
            {
                ExecuteArgs_ProgramStarted(args);
            }
        }

        static void InitializeApplication()
        {
            Animator = new AnimatorNS.Animator() { Interval = 1, TimeStep = 0.07f, DefaultAnimation = AnimatorNS.Animation.Transparent, AnimationType = AnimatorNS.AnimationType.Transparent };

            if (OS.W7 | OS.WVista | OS.WXP)
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            DeleteUpdateResiduals();
            GetMemoryFonts();
            FetchDarkMode();
            ApplyStyle();
            LoadLanguage();
            CheckIfLicenseChecked();
            ExecuteArgs();
            StartWallpaperMonitor();
            AssociateFiles();
            DetectIfWPStartedWithClassicTheme();
            ExtractLuna();
            BackupWindowsStartupSound();
            CreateUninstaller();
            CheckWhatsNew();
            InitializeImageLists();
            InitializeCMDWrapper();
            InitializeWPSysEventsSounds();
            LoadThemeManager();
        }

        private static void OnExit(object sender, EventArgs e)
        {
            if (!OS.WXP)
            {
                try
                {
                    WallMon_Watcher1.Stop();
                    WallMon_Watcher2.Stop();

                    if (!OS.W7 & !OS.W81 & !OS.WVista)
                    {
                        WallMon_Watcher3.Stop();
                        WallMon_Watcher4.Stop();
                    }
                }
                catch
                {
                }
            }

            DeleteUpdateResiduals();

            CMD_Wrapper.Exit();

            AppDomain.CurrentDomain.AssemblyResolve -= DomainCheck;
            AppDomain.CurrentDomain.UnhandledException -= Domain_UnhandledException;
            Application.ThreadException -= ThreadExceptionHandler;

            try
            {
                WallMon_Watcher1.EventArrived -= Wallpaper_Changed_EventHandler;
                WallMon_Watcher2.EventArrived -= Wallpaper_Changed_EventHandler;
                WallMon_Watcher3.EventArrived -= WallpaperType_Changed;
                WallMon_Watcher4.EventArrived -= DarkMode_Changed_EventHandler;
            }
            catch
            {
            }

            SystemEvents.UserPreferenceChanged -= OldWinPreferenceChanged;
        }

        private static bool IsSecondInstance()
        {
            mutex = new Mutex(true, Application.ProductName, out bool createdNew);
            return !createdNew;
        }

        public static void BringApplicationToFront()
        {
            try { Application.OpenForms[Forms.MainFrm.Name].BringToFront(); }
            catch { }
        }
    }
}