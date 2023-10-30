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
            Users.UserChange += OnUserSwitch;

            if (!IsSecondInstance())
            {
                if (OS.W7 | OS.WVista | OS.WXP)
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                DeleteUpdateResiduals();
                GetMemoryFonts();
                InitializeImageLists();

                InitializeApplication(true);

                Application.Run(Forms.MainFrm);
            }
            else
            {
                ExecuteArgs_ProgramStarted(args);
            }
        }


        static void InitializeApplication(bool ShowLoginDialog)
        {
            Animator = new AnimatorNS.Animator() { Interval = 1, TimeStep = 0.07f, DefaultAnimation = AnimatorNS.Animation.Transparent, AnimationType = AnimatorNS.AnimationType.Transparent };

            // Important to load proper style and language before showing login dialog
            FetchDarkMode();
            ApplyStyle();
            LoadLanguage();

            if (ShowLoginDialog) 
            { 
                Users.Login();
                return;
            }

            // Data of following methods depends on current selected user
            DetectIfWPStartedWithClassicTheme();
            ExtractLuna();

            CheckIfLicenseChecked();
            AssociateFiles();
            StartWallpaperMonitor();

            BackupWindowsStartupSound();
            CreateUninstaller();
            CheckWhatsNew();
            InitializeSysEventsSounds();

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
            Users.UserChange -= OnUserSwitch;

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

        public static void OnUserSwitch(UserChangeEventArgs e)
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
                        bool MainFormIsOpened = Application.OpenForms[Forms.MainFrm.Name] is not null;

                        Users.UpdatePathsFromSID(Users.SID);
                        Program.Settings = new(WPSettings.Mode.Registry);

                        if (MainFormIsOpened)
                        {
                            if (Settings.ThemeApplyingBehavior.ShowSaveConfirmation && (TM != TM_Original))
                            {
                                Forms.ComplexSave.GetResponse(Forms.MainFrm.SaveFileDialog1, () => Forms.ThemeLog.Apply_Theme(), () => Forms.ThemeLog.Apply_Theme(Program.TM_FirstTime), () => Forms.ThemeLog.Apply_Theme(Theme.Default.Get()));
                            }
                        }

                        InitializeApplication(false);

                        if (MainFormIsOpened) { Forms.MainFrm.LoadData(); }

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