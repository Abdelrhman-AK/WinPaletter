﻿using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Security.Principal;

namespace WinPaletter
{
    internal partial class Program
    {
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
        public readonly static bool Elevated = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

        /// <summary>
        /// Gets if WinPaletter's current version is designed as Beta or not
        /// <br>Don't forget to make it <b>True</b> when you design a beta one</br>
        /// </summary>
        public readonly static bool IsBeta = false;

        /// <summary>
        /// A boolean that represents if WinPaletter has started with a classic theme enabled (Loaded at application startup)
        /// </summary>
        public static bool StartedWithClassicTheme = false;

        /// <summary>
        /// Class represents colors for WinPaletter Controls (Styles)
        /// </summary>
        public static Config Style = new(DefaultColors.PrimaryColor, DefaultColors.SecondaryColor, DefaultColors.TertiaryColor, DefaultColors.DisabledColor_Dark, DefaultColors.BackColorDark, DefaultColors.DisabledBackColor_Dark, true, true, true);

        /// <summary>
        /// A class that represents WinPaletter's Settings
        /// </summary>
        public static Settings Settings = new(Settings.Mode.Registry);

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
        public static PreviewHelpers.WindowStyle PreviewStyle = PreviewHelpers.WindowStyle.W11;

        /// <summary>
        /// Global variables to manage WinPaletter theme
        /// </summary>
        public static Theme.Manager TM, TM_Original, TM_FirstTime;

        /// <summary>
        /// Process that kills (stops by force) Windows explorer
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
        /// Process that starts Windows explorer
        /// </summary>
        public static readonly Process Explorer_exe = new()
        {
            StartInfo = new()
            {
                FileName = PathsExt.explorer,
                WindowStyle = ProcessWindowStyle.Normal,
            }
        };

        /// <summary>
        /// Relative to My.Computer in VB.NET
        /// </summary>
        internal static Microsoft.VisualBasic.Devices.Computer Computer = new();

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
    }
}
