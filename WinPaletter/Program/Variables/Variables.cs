using Serilog.Core;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.Theme;

namespace WinPaletter
{
    internal partial class Program
    {
        /// <summary>
        /// A class that represents WinPaletter's Settings
        /// </summary>
        public static Settings Settings = new(Settings.Source.Registry);

        /// <summary>
        /// An instance of WinPaletter logger class (wrapper for Serilog)
        /// </summary>
        public static Logger Log { get; set; }

        /// <summary>
        /// GitHub Login Manager
        /// </summary>
        public static GitHub.LoginManager GitHub { get; set; }

        /// <summary>
        /// Gets the file path for the log file, including the current timestamp in the file name.
        /// </summary>
        /// <remarks>The log file is stored in the directory specified by <see cref="SysPaths.LogsDir"/> and is named using
        /// the format "WinPaletter_Log_yyyyMMdd_HHmmss.json", where the timestamp reflects the moment the application
        /// initialized this field.</remarks>
        public static string LogFile => $"{SysPaths.LogsDir}\\WinPaletter_Log_{DateTime.Now:yyyyMMdd_HHmmss}.json";

        /// <summary>
        /// Class represents colors for WinPaletter Controls (Styles)
        /// </summary>
        public static Config Style { get; set; } = new(DefaultColors.PrimaryColor_Dark, DefaultColors.SecondaryColor_Dark, DefaultColors.TertiaryColor_Dark, DefaultColors.DisabledColor_Dark, DefaultColors.BackColor_Dark, DefaultColors.DisabledBackColor_Dark, true, true, true);

        /// <summary>
        /// WinPaletter version, instead of using long statement 'System.Windows.Forms.Application.ProductVersion'
        /// </summary>
        public static string Version => Application.ProductVersion;

        /// <summary>
        /// WinPaletter elements animation interval in ms.
        /// </summary>
        public static int AnimationDuration => 1000;

        /// <summary>
        /// WinPaletter elements quick animation interval in ms.
        /// </summary>
        public static int AnimationDuration_Quick => 200;

        /// <summary>
        /// WinPaletter executable File path
        /// </summary>
        public static string AppFile => Assembly.GetExecutingAssembly().Location;

        /// <summary>
        /// WinPaletter executable File size in bytes
        /// </summary>
        public static long Length => new FileInfo(AppFile).Length;

        /// <summary>
        /// Get if Application is started as administrator or not
        /// </summary>
        public static bool Elevated => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

        /// <summary>
        /// Gets if WinPaletter's current version is designed as beta or not
        /// <br>Don't forget to make it <b>true</b> when you design a beta one</br>
        /// </summary>
        public static bool IsBeta => true;

        /// <summary>
        /// A boolean that represents if WinPaletter has started with a classic theme enabled (Loaded at application startup)
        /// </summary>
        public static bool ClassicThemeRunning
        {
            get
            {
                Tuple<string, string, string> ThemeTuple = UxTheme.GetCurrentVS();
                return string.IsNullOrEmpty(ThemeTuple.Item1.ToString()) || !File.Exists(ThemeTuple.Item1.ToString());
            }
        }

        /// <summary>
        /// First visual styles File to be used in WinPaletter (Loaded at application startup)
        /// </summary>
        public static string FirstVisualStyles { get; set; } = $"{SysPaths.Windows}\\Resources\\Themes\\aero\\aero.msstyles";

        /// <summary>
        /// Gets or sets the global localizer used for retrieving localized strings throughout the application.
        /// </summary>
        /// <remarks>Assigning a custom localizer allows you control how text is localized across the
        /// application. Changes to this property affect all components that rely on localization.</remarks>
        public static Localizer Localization { get; set; } = new();

        /// <summary>
        /// Current applied wallpaper
        /// </summary>
        public static Bitmap Wallpaper { get; set; }

        /// <summary>
        /// Variable responsible for the preview type on forms
        /// </summary>
        public static PreviewHelpers.WindowStyle WindowStyle { get; set; } = PreviewHelpers.WindowStyle.W11;

        /// <summary>
        /// Global variables to manage WinPaletter theme
        /// </summary>
        public static Manager TM
        {
            get => tm;
            set
            {
                if (tm != value)
                {
                    tm = value;
                    Wallpaper = FetchSuitableWallpaper(value, Program.WindowStyle);
                }
            }
        }
        private static Manager tm = new(Manager.Source.Empty);

        /// <summary>
        /// Represents the original instance of the <see cref="Manager"/> class.
        /// </summary>
        /// <remarks>This static field holds a reference to the original <see cref="Manager"/> instance. 
        /// It can be used to access the default or initial configuration of the manager.</remarks>
        public static Manager TM_Original { get; set; }

        /// <summary>
        /// Represents the first-time instance of the <see cref="Manager"/> class.
        /// </summary>
        /// <remarks>This static field is used to manage and track the application's first-time theme.</remarks>
        public static Manager TM_FirstTime { get; set; }

        /// <summary>
        /// Process that kills (stops by force) Windows Explorer
        /// </summary>
        public static Process ExplorerKiller { get; } = new()
        {
            StartInfo = new()
            {
                FileName = $@"{SysPaths.System32}\taskkill.exe",
                Verb = !OS.WXP ? "runas" : string.Empty,
                Arguments = "/F /IM explorer.exe",
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = true
            }
        };

        /// <summary>
        /// A class that contains info about ExplorerPatcher settings
        /// </summary>
        public static ExplorerPatcher EP { get; set; } = new();

        /// <summary>
        /// Gets if WinPaletter is opened by a File or not
        /// </summary>
        public static bool ExternalLink { get; set; } = false;

        /// <summary>
        /// Gets File that opened WinPaletter
        /// </summary>
        public static string ExternalLink_File { get; set; } = string.Empty;

        /// <summary>
        /// Class that has bitmaps to used visual styles files and renders them in WinPaletter preview
        /// </summary>
        public static VisualStylesRes resVS { get; }

        /// <summary>
        /// Settings to exit after exception error
        /// </summary>
        public static bool ExitAfterException { get; set; } = false;

        /// <summary>
        /// Hide What's new form after loading WinPaletter
        /// </summary>
        public static bool ShowWhatsNew { get; set; } = false;

        /// <summary>
        /// AnimatorNS control to be exposed globally to all forms and classes
        /// </summary>
        public static Animator Animator { get; set; } = new();

        /// <summary>
        /// A global ToolTip to be used in all forms
        /// </summary>
        public static UI.WP.ToolTip ToolTip { get; set; } = new();

        /// <summary>
        /// Delete WinPaletter reg on exit (used by uninstaller)
        /// </summary>
        public static bool DeleteWinPaletteReg { get; set; } = false;

        /// <summary>
        /// A boolean that represents if WinPaletter uninstaller has finished or not
        /// </summary>
        public static bool UninstallDone { get; set; } = false;

        /// <summary>
        /// The system partition letter
        /// </summary>
        public static char SystemPartition => Environment.SystemDirectory[0];

        /// <summary>
        /// Enumeration that represents the boot status of the system
        /// </summary>
        public enum BootStatuses
        {
            Normal,
            SafeMode,
        }

        /// <summary>
        /// Gets the current boot status of the system
        /// </summary>
        public static BootStatuses BootStatus
        {
            get
            {
                int bootMode = User32.GetSystemMetrics(67 /*SM_CLEANBOOT*/);

                switch (bootMode)
                {
                    case 0:
                        return BootStatuses.Normal;
                    case 1:
                        return BootStatuses.SafeMode;
                    case 2: // with networking
                        return BootStatuses.SafeMode;
                    default:
                        return BootStatuses.Normal;
                }
            }
        }

        /// <summary>
        /// Timeout for web requests in milliseconds
        /// </summary>
        public static int Timeout => 15 * 1000;

        /// <summary>
        /// Represents the size of the preview area.
        /// </summary>
        public static Size PreviewSize => new(528, 297);

        public static bool WindowsTransparency => windowsTransparency;
        private static bool windowsTransparency = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", 1) == 1;

        public static event Action<bool> WindowsTransparencyChanged;
    }
}
