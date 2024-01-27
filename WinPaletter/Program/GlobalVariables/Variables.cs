using System;
using System.Diagnostics;
using System.Drawing;
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
        public static Settings Settings = new(Settings.Mode.Registry);

        /// <summary>
        /// Class represents colors for WinPaletter Controls (Styles)
        /// </summary>
        public static Config Style = new(DefaultColors.PrimaryColor, DefaultColors.SecondaryColor, DefaultColors.TertiaryColor, DefaultColors.DisabledColor_Dark, DefaultColors.BackColorDark, DefaultColors.DisabledBackColor_Dark, true, true, true);

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
        /// WinPaletter executable file path
        /// </summary>
        public readonly static string AppFile = Assembly.GetExecutingAssembly().Location;

        /// <summary>
        /// WinPaletter executable file size in bytes
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
        /// A class that represents WinPaletter's Language Strings (Loaded at application startup)
        /// </summary>
        public static Localizer Lang = new();

        /// <summary>
        /// Current applied wallpaper
        /// </summary>
        public static Bitmap Wallpaper, Wallpaper_Unscaled;

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
                FileName = $@"{PathsExt.System32}\taskkill.exe",
                Verb = !OS.WXP ? "runas" : string.Empty,
                Arguments = "/F /IM explorer.exe",
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = true
            }
        };

        /// <summary>
        /// Process that starts Windows Explorer
        /// </summary>
        public static readonly Process Explorer_exe = new()
        {
            StartInfo = new()
            {
                FileName = PathsExt.Explorer,
                WindowStyle = ProcessWindowStyle.Normal
            }
        };

        /// <summary>
        /// A class that contains info about ExplorerPatcher settings
        /// </summary>
        public static ExplorerPatcher EP = new();

        /// <summary>
        /// Gets if WinPaletter is opened by a file or not
        /// </summary>
        public static bool ExternalLink = false;

        /// <summary>
        /// Gets file that opened WinPaletter
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
        /// Show What's new form after loading WinPaletter
        /// </summary>
        public static bool ShowWhatsNew = false;

        /// <summary>
        /// AnimatorNS control to be exposed globally to all forms and classes
        /// </summary>
        public static AnimatorNS.Animator Animator;

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
        /// Timeout for web requests in milliseconds
        /// </summary>
        public static int Timeout => 30 * 1000;
    }
}
