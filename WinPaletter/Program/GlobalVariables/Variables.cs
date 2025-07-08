using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using WinPaletter.NativeMethods;

namespace WinPaletter
{
    internal partial class Program
    {
        /// <summary>
        /// A class that represents WinPaletter's Settings
        /// </summary>
        public static Settings Settings = new(Settings.Source.Registry);

        /// <summary>
        /// An instance of the Serilog logger class
        /// </summary>
        public static Serilog.Core.Logger Log;

        /// <summary>
        /// Class represents colors for WinPaletter Controls (Styles)
        /// </summary>
        public static Config Style = new(DefaultColors.PrimaryColor_Dark, DefaultColors.SecondaryColor_Dark, DefaultColors.TertiaryColor_Dark, DefaultColors.DisabledColor_Dark, DefaultColors.BackColor_Dark, DefaultColors.DisabledBackColor_Dark, true, true, true);

        /// <summary>
        /// WinPaletter version, instead of using long statement 'System.Windows.Forms.Application.ProductVersion'
        /// </summary>
        public readonly static string Version = System.Windows.Forms.Application.ProductVersion;

        /// <summary>
        /// WinPaletter elements animation interval in ms.
        /// </summary>
        public readonly static int AnimationDuration = 1000;

        /// <summary>
        /// WinPaletter elements quick animation interval in ms.
        /// </summary>
        public readonly static int AnimationDuration_Quick = 200;

        /// <summary>
        /// WinPaletter executable File path
        /// </summary>
        public readonly static string AppFile = Assembly.GetExecutingAssembly().Location;

        /// <summary>
        /// WinPaletter executable File size in bytes
        /// </summary>
        public readonly static long Length = new System.IO.FileInfo(AppFile).Length;

        /// <summary>
        /// Get if Application is started as administrator or not
        /// </summary>
        public static bool Elevated => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

        /// <summary>
        /// Gets if WinPaletter's current version is designed as beta or not
        /// <br>Don't forget to make it <b>true</b> when you design a beta one</br>
        /// </summary>
        public readonly static bool IsBeta = true;

        /// <summary>
        /// A boolean that represents if WinPaletter has started with a classic theme enabled (Loaded at application startup)
        /// </summary>
        public static bool ClassicThemeRunning
        {
            get
            {
                Tuple<string, string, string> ThemeTuple = UxTheme.GetCurrentVS();
                return string.IsNullOrEmpty(ThemeTuple.Item1.ToString()) || !System.IO.File.Exists(ThemeTuple.Item1.ToString());
            }
        }

        /// <summary>
        /// First visual styles File to be used in WinPaletter (Loaded at application startup)
        /// </summary>
        public static string FirstVisualStyles = $"{SysPaths.Windows}\\Resources\\Themes\\aero\\aero.msstyles";

        /// <summary>
        /// A class that represents WinPaletter's Language Strings_Cls (Loaded at application startup)
        /// </summary>
        public static Localizer Lang = new();

        /// <summary>
        /// Current applied wallpaper
        /// </summary>
        public static Bitmap Wallpaper;

        /// <summary>
        /// Variable responsible for the preview type on forms
        /// </summary>
        public static PreviewHelpers.WindowStyle WindowStyle = PreviewHelpers.WindowStyle.W11;

        /// <summary>
        /// Global variables to manage WinPaletter theme
        /// </summary>
        public static Theme.Manager TM, TM_Original, TM_FirstTime;

        /// <summary>
        /// Process that kills (stops by force) Windows Explorer
        /// </summary>
        public static readonly Process ExplorerKiller = new()
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
        public static ExplorerPatcher EP = new();

        /// <summary>
        /// Gets if WinPaletter is opened by a File or not
        /// </summary>
        public static bool ExternalLink = false;

        /// <summary>
        /// Gets File that opened WinPaletter
        /// </summary>
        public static string ExternalLink_File = string.Empty;

        /// <summary>
        /// Class that has bitmaps to used visual styles files and renders them in WinPaletter preview
        /// </summary>
        public static VisualStylesRes resVS;

        /// <summary>
        /// Settings to exit after exception error
        /// </summary>
        public static bool ExitAfterException = false;

        /// <summary>
        /// Hide What's new form after loading WinPaletter
        /// </summary>
        public static bool ShowWhatsNew = false;

        /// <summary>
        /// AnimatorNS control to be exposed globally to all forms and classes
        /// </summary>
        public static WinPaletter.Animator Animator = new();

        /// <summary>
        /// A global ToolTip to be used in all forms
        /// </summary>
        public static UI.WP.ToolTip ToolTip = new();

        /// <summary>
        /// Delete WinPaletter reg on exit (used by uninstaller)
        /// </summary>
        public static bool DeleteWinPaletteReg = false;

        /// <summary>
        /// A boolean that represents if WinPaletter uninstaller has finished or not
        /// </summary>
        public static bool UninstallDone = false;

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
    }
}
