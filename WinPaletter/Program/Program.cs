using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Win32;
using Serilog;
using System;
using System.Globalization;
using System.IO;
using System.Media;
using System.Net;
using System.Security.Principal;
using System.ServiceProcess;
using System.Threading;
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
            CultureInfo.DefaultThreadCurrentUICulture = Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            // Assigning some event handlers to the application including a modified version of the default exception handler and the user switch event handler
            AppDomain.CurrentDomain.AssemblyResolve += DomainCheck;
            AppDomain.CurrentDomain.UnhandledException += Domain_UnhandledException;
            Application.ThreadException += ThreadExceptionHandler;
            Application.ApplicationExit += OnExit;
            User.UserSwitch += User.OnUserSwitch;

            // Change security protocol to TLS 1.2 if the OS is Windows 7, Vista or WXP
            if (OS.W7 | OS.WVista | OS.WXP) ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            // Delete the update residuals
            DeleteUpdateResiduals();

            // Get the memory fonts (Jetbrain Mono), initialize the image lists and the application
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
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, "WinPaletter is exiting. Cleaning up resources and removing event handlers.");

            DeleteUpdateResiduals();

            AppDomain.CurrentDomain.AssemblyResolve -= DomainCheck;
            AppDomain.CurrentDomain.UnhandledException -= Domain_UnhandledException;
            Application.ThreadException -= ThreadExceptionHandler;
            User.UserSwitch -= User.OnUserSwitch;
            SystemEvents.UserPreferenceChanged -= OldWinPreferenceChanged;

            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, "WinPaletter has exited successfully.");
        }

        /// <summary>
        /// This method initializes the application and the user login. It is called when the application is started.
        /// </summary>
        public static void InitializeApplication()
        {
            // Impersonate the selected user to access the user's data
            using (WindowsImpersonationContext wic = User.Identity.Impersonate())
            {
                // Initialize log
                if (!System.IO.Directory.Exists(SysPaths.Logs)) { System.IO.Directory.CreateDirectory(SysPaths.Logs); }
                string logFileName = $"{SysPaths.Logs}\\WinPaletter_Log_{DateTime.Now:yyyyMMdd_HHmmss}.json";
                if (File.Exists(logFileName)) using (FileStream fs = new(logFileName, FileMode.Truncate)) { }
                LoggerConfiguration log = new();
                log.WriteTo.File(new Serilog.Formatting.Json.JsonFormatter(), logFileName);
                Log = log.CreateLogger();
                Log?.Write(Serilog.Events.LogEventLevel.Information, $"WinPaletter started: {DateTime.Now}");
                Log?.Write(Serilog.Events.LogEventLevel.Information, $"WinPaletter version: {Version}");
                Log?.Write(Serilog.Events.LogEventLevel.Information, $"WinPaletter file size: {Length} bytes.");
                Log?.Write(Serilog.Events.LogEventLevel.Information, $"WinPaletter file path: {AppFile}.");
                Log?.Write(Serilog.Events.LogEventLevel.Information, $"WinPaletter file MD5: {Program.CalculateMD5(AppFile)}");
                Log?.Write(Serilog.Events.LogEventLevel.Information, $"WinPaletter log file path: {logFileName}");
                Log?.Write(Serilog.Events.LogEventLevel.Information, $"WinPaletter has started with user: {User.Identity.Name}.");

                // Initialize the animator. It must be here to avoid some issues and bugs with its assembly
                Animator = new() { Interval = 10, TimeStep = 0.05f, DefaultAnimation = AnimatorNS.Animation.Transparent, AnimationType = AnimatorNS.AnimationType.Transparent };

                // Create the data directory if it does not exist
                if (!System.IO.Directory.Exists(SysPaths.ProgramFilesData))
                {
                    System.IO.Directory.CreateDirectory(SysPaths.ProgramFilesData);
                    Log?.Write(Serilog.Events.LogEventLevel.Information, $"A new directory has been created: {SysPaths.ProgramFilesData}");
                }

                // Important to load proper style and language before showing login dialog
                Log?.Write(Serilog.Events.LogEventLevel.Information, $"Loading application style.");
                SetRoundedCorners();
                GetDarkMode();
                ApplyStyle();
                LoadLanguage();

                // Data of following methods depends on current selected user, so they were not executed alone in Main() void
                ExtractLuna();

                CheckIfSetupIsComplete();
                AssociateFiles();
                StartMonitors();

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

        /// <summary>
        /// Check if an instance of WinPaletter is already running. If it is, execute the arguments passed to it instead of opening a new instance.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void StartupNextInstanceEventHandler(object sender, StartupNextInstanceEventArgs e)
        {
            using (WindowsImpersonationContext wic = User.Identity.Impersonate())
            {
                ExecuteArgs([.. e.CommandLine], false);
                wic.Undo();
            }
        }
    }
}