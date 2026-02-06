using Microsoft.Win32;
using Serilog.Events;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinPaletter
{
    internal partial class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args"></param>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            // Assigning some event handlers to the application including a modified version of the default exception handler and the user switch event handler
            AppDomain.CurrentDomain.AssemblyResolve += DomainCheck;
            AppDomain.CurrentDomain.UnhandledException += Domain_UnhandledException;
            Application.ThreadException += ThreadExceptionHandler;
            //TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            //AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;

            Application.ApplicationExit += OnExit;

            // Configure the security protocol to use TLS 1.2 or higher. This is important for some features that require internet access, such as checking for updates and downloading themes.
            ConfigureSecurityProtocol();

            // Delete the update residuals
            DeleteUpdateResiduals();

            // Get the memory fonts (Jetbrain Mono)
            GetMemoryFonts();

            // Initialize the image lists to be used for logs and other purposes
            InitializeImageLists();

            // Initialize the application
            InitializeApplication();

            if (Program.BootStatus == BootStatuses.SafeMode) Forms.SOS.Show();

            SingleInstanceApplication.Run(Forms.MainForm, StartupNextInstanceEventHandler);
        }

        /// <summary>
        /// This method is called when the application is about to exit. It is used to clean up some resources and remove some event handlers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnExit(object sender, EventArgs e)
        {
            Log?.Write(LogEventLevel.Information, "WinPaletter is exiting. Cleaning up resources and removing event handlers.");

            DeleteUpdateResiduals();

            AppDomain.CurrentDomain.AssemblyResolve -= DomainCheck;
            AppDomain.CurrentDomain.UnhandledException -= Domain_UnhandledException;
            Application.ThreadException -= ThreadExceptionHandler;
            //TaskScheduler.UnobservedTaskException -= TaskScheduler_UnobservedTaskException;
            //AppDomain.CurrentDomain.FirstChanceException -= CurrentDomain_FirstChanceException;
            User.UserSwitch -= User.OnUserSwitch;
            //SystemEvents.UserPreferenceChanged -= OldWinPreferenceChanged;

            Log?.Write(LogEventLevel.Information, "WinPaletter has exited successfully.");
        }

        /// <summary>
        /// This method initializes the application and the user login. It is called when the application is started.
        /// </summary>
        public static void InitializeApplication()
        {
            // Impersonate the selected user to access the user's data
            using (WindowsImpersonationContext wic = User.Identity.Impersonate())
            {
                // Placing user switching event here fixed CLR20R3 error on Windows 7
                User.UserSwitch -= User.OnUserSwitch;
                User.UserSwitch += User.OnUserSwitch;

                Log = new();
                Log?.Write(LogEventLevel.Information, $"WinPaletter started: {DateTime.Now}");
                Log?.Write(LogEventLevel.Information, $"WinPaletter version: {Version}");
                Log?.Write(LogEventLevel.Information, $"WinPaletter file size: {Length} bytes.");
                Log?.Write(LogEventLevel.Information, $"WinPaletter file path: {AppFile}.");
                Log?.Write(LogEventLevel.Information, $"WinPaletter MD5: {Program.CalculateMD5(AppFile)}");
                Log?.Write(LogEventLevel.Information, $"WinPaletter log file path: {LogFile}");
                Log?.Write(LogEventLevel.Information, $"WinPaletter has started with user: {User.Identity.Name}.");

                // Create the data directory if it does not exist
                if (!Directory.Exists(SysPaths.ProgramFilesData))
                {
                    Directory.CreateDirectory(SysPaths.ProgramFilesData);
                    Log?.Write(LogEventLevel.Information, $"A new directory has been created: {SysPaths.ProgramFilesData}");
                }

                // Important to load proper style and language before showing login dialog
                Log?.Write(LogEventLevel.Information, $"Loading application style.");

                Style.RoundedCorners = GetRoundedCorners(true); // When not included, rounded corners may be applied in non-rounded-corners-OS
                GetDarkMode();
                ApplyStyle();
                LoadLanguage();

                // Data of following methods depends on current selected user, so they were not executed alone in Main() void
                ExtractLuna();

                // Load theme manager before monitors
                LoadThemeManager();

                CheckIfSetupIsComplete();
                AssociateFiles();
                StartMonitors();

                BackupWindowsStartupSound();
                CreateUninstaller();
                CheckWhatsNew();

                ExecuteArgs();
                UpdateSysEventsSounds();
            }

            GitHub = new();

            Task.Run(async () =>
            {
                bool isLoggedIn = await GitHub.IsLoggedInAsync();
                User.UpdateGitHubLoginStatus(isLoggedIn);
            });
        }

        /// <summary>
        /// Check if an instance of WinPaletter is already running. If it is, execute the arguments passed to it instead of opening a new instance.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void StartupNextInstanceEventHandler(object sender, Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs e)
        {
            using (WindowsImpersonationContext wic = User.Identity.Impersonate())
            {
                ExecuteArgs([.. e.CommandLine], false);
                wic.Undo();
            }
        }
    }
}