using Microsoft.Win32;
using System;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using static WinPaletter.Users;

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
            Users.UserChange += OnUserChange;

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

            string PreviousUser = Users.SID;

            DeleteUpdateResiduals();
            GetMemoryFonts();
            InitializeImageLists();

            FetchDarkMode();
            ApplyStyle();
            LoadLanguage();
            CheckIfLicenseChecked();

            DetectIfWPStartedWithClassicTheme();
            ExtractLuna();

            Users.Login();
            if (PreviousUser == Users.SID)
            {
                // User didn't change, so do rest voids that depends on data on current user
                // Never forger to add voids into void OnUserChange(), so it gets new data for current user
                AssociateFiles();
                StartWallpaperMonitor();

                BackupWindowsStartupSound();
                CreateUninstaller();
                CheckWhatsNew();
                InitializeSysEventsSounds();
            }

            ExecuteArgs();
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

            AppDomain.CurrentDomain.AssemblyResolve -= DomainCheck;
            AppDomain.CurrentDomain.UnhandledException -= Domain_UnhandledException;
            Application.ThreadException -= ThreadExceptionHandler;
            Users.UserChange -= OnUserChange;

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

        public static void OnUserChange(UserChangeEventArgs e)
        {
            switch (e.Timing)
            {
                case UserChangeEventArgs.Timings.BeforeChange:
                    {
                        Settings.Save(WPSettings.Mode.Registry);
                        break;
                    }

                case UserChangeEventArgs.Timings.AfterChange:
                    {
                        Users.UpdatePathsFromSID(Users.SID);

                        Program.Settings = new(WPSettings.Mode.Registry);

                        FetchDarkMode();
                        ApplyStyle();
                        LoadLanguage();
                        CheckIfLicenseChecked();

                        AssociateFiles();
                        StartWallpaperMonitor();

                        BackupWindowsStartupSound();
                        CreateUninstaller();
                        CheckWhatsNew();
                        InitializeSysEventsSounds();

                        break;
                    }
            }
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