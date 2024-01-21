using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Win32;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Principal;
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
            CultureInfo.DefaultThreadCurrentUICulture = Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            AppDomain.CurrentDomain.AssemblyResolve += DomainCheck;
            AppDomain.CurrentDomain.UnhandledException += Domain_UnhandledException;
            Application.ThreadException += ThreadExceptionHandler;
            Application.ApplicationExit += OnExit;
            User.UserSwitch += User.OnUserSwitch;

            if (OS.W7 | OS.WVista | OS.WXP)
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            DeleteUpdateResiduals();
            GetMemoryFonts();
            InitializeImageLists();

            InitializeApplication(true);

            SingleInstanceApplication.Run(Forms.MainFrm, StartupNextInstanceEventHandler);
        }

        public static void InitializeApplication(bool ShowLoginDialog)
        {
            using (WindowsImpersonationContext wic = User.Identity.Impersonate())
            {
                Animator = new() { Interval = 1, TimeStep = 0.07f, DefaultAnimation = AnimatorNS.Animation.Transparent, AnimationType = AnimatorNS.AnimationType.Transparent };

                if (!System.IO.Directory.Exists(PathsExt.ProgramFilesData)) { System.IO.Directory.CreateDirectory(PathsExt.ProgramFilesData); }

                // Important to load proper style and language before showing login dialog
                GetRoundedCorners();
                GetDarkMode();
                ApplyStyle();
                LoadLanguage();

                if (ShowLoginDialog)
                {
                    User.Login(false, ArgsCanSkipUserLogin);
                    return;
                }

                // Data of following methods depends on current selected user
                ExtractLuna();

                CheckIfLicenseIsAccepted();
                AssociateFiles();
                StartWallpaperMonitor();

                BackupWindowsStartupSound();
                CreateUninstaller();
                CheckWhatsNew();

                ExecuteArgs();
                LoadThemeManager();
                UpdateSysEventsSounds();

                Wallpaper = FetchSuitableWallpaper(TM, WindowStyle);

                wic.Undo();
            }
        }

        private static void OnExit(object sender, EventArgs e)
        {
            DeleteUpdateResiduals();

            AppDomain.CurrentDomain.AssemblyResolve -= DomainCheck;
            AppDomain.CurrentDomain.UnhandledException -= Domain_UnhandledException;
            Application.ThreadException -= ThreadExceptionHandler;
            User.UserSwitch -= User.OnUserSwitch;
            SystemEvents.UserPreferenceChanged -= OldWinPreferenceChanged;
        }

        public static void StartupNextInstanceEventHandler(object sender, StartupNextInstanceEventArgs e)
        {
            using (WindowsImpersonationContext wic = User.Identity.Impersonate())
            {
                ExecuteArgs_ProgramStarted(e.CommandLine.Skip(1).ToArray());
                wic.Undo();
            }
        }

        public static void BringApplicationToFront()
        {
            try { Application.OpenForms[Forms.MainFrm.Name].BringToFront(); }
            catch { }
        }
    }
}