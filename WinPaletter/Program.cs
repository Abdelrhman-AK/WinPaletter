using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO.Compression;
using System.Linq;
using System.Management;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace WinPaletter
{
    internal partial class Program : ISynchronizeInvoke
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

            if (W7 | WVista | WXP)
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            ExternalLink = false;
            ExternalLink_File = "";

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

            #region WhatsNew
            if (!Settings.General.WhatsNewRecord.Contains(AppVersion))
            {
                // ### Pop up WhatsNew
                ShowWhatsNew = true;

                var ver = new List<string>();
                ver.Clear();
                ver.Add(AppVersion);

                foreach (string X in Settings.General.WhatsNewRecord.ToArray())
                    ver.Add(X);

                ver = ver.DeDuplicate();
                Settings.General.WhatsNewRecord = ver.ToArray();
                Settings.General.Save();
            }
            else
            {
                ShowWhatsNew = false;
            }
            #endregion

            InitializeImageLists();

            InitializeCMDWrapper();

            LoadThemeManager();
        }

        private static void OnExit(object sender, EventArgs e)
        {
            if (!WXP)
            {
                try
                {
                    WallMon_Watcher1.Stop();
                    WallMon_Watcher2.Stop();

                    if (!W7 & !W81 & !WVista)
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

        #region Variables
        internal static Microsoft.VisualBasic.Devices.Computer Computer = new();
        private static Mutex mutex = null;

        public readonly static string PATH_appData = System.IO.Directory.GetParent(Application.LocalUserAppDataPath).FullName;
        public readonly static string PATH_Windows = Environment.GetFolderPath(Environment.SpecialFolder.Windows).Replace("WINDOWS", "Windows");
        public readonly static string PATH_explorer = PATH_Windows + @"\explorer.exe";
        public readonly static string PATH_ProgramFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        public readonly static string PATH_System32 = PATH_Windows + @"\System32";
        public readonly static string PATH_CMD = PATH_System32 + @"\cmd.exe";
        public readonly static string PATH_SchTasks = PATH_System32 + @"\schtasks";
        public readonly static string PATH_WPElevator = PATH_appData + @"\WinPaletter.Elevator.exe";
        public readonly static string PATH_imageres = PATH_System32 + @"\imageres.dll";
        public readonly static string PATH_UserProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public readonly static string PATH_TerminalJSON = PATH_UserProfile + @"\AppData\Local\Packages\Microsoft.WindowsTerminal_8wekyb3d8bbwe\LocalState\settings.json";
        public readonly static string PATH_TerminalPreviewJSON = PATH_UserProfile + @"\AppData\Local\Packages\Microsoft.WindowsTerminalPreview_8wekyb3d8bbwe\LocalState\settings.json";
        public readonly static string PATH_PS86_reg = "%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe";
        public readonly static string PATH_PS86_app = PATH_Windows + @"\System32\WindowsPowerShell\v1.0";
        public readonly static string PATH_PS64_reg = "%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe";
        public readonly static string PATH_PS64_app = PATH_Windows + @"\SysWOW64\WindowsPowerShell\v1.0";
        public readonly static string PATH_StoreCache = PATH_appData + @"\Store";
        public readonly static string PATH_ThemeResPackCache = PATH_appData + @"\ThemeResPack_Cache";
        public readonly static string PATH_CursorsWP = PATH_appData + @"\Cursors";
        public readonly static string AppVersion = Application.ProductVersion;

        public readonly static StringComparison _ignore = StringComparison.OrdinalIgnoreCase;
        public static string VS = PATH_appData + @"\VisualStyles\Luna\luna.theme";
        public static VisualStylesRes resVS;

        public readonly static Color DefaultAccent = Color.FromArgb(0, 81, 210);
        public readonly static Color DefaultBackColorDark = Color.FromArgb(25, 25, 25);
        public readonly static Color DefaultBackColorLight = Color.FromArgb(230, 230, 230);

        /// <summary>
        /// Class represents colors for WinPaletter Controls (Styles)
        /// </summary>
        public static WPStyle Style = new WPStyle(DefaultAccent, DefaultBackColorDark, true);

        /// <summary>
        /// A boolean that represents if WinPaletter has started with a classic theme enabled (Loaded at application startup)
        /// </summary>
        public static bool StartedWithClassicTheme = false;

        /// <summary>
        /// A boolean that represents if OS is Windows XP
        /// </summary>
        public readonly static bool WXP = Environment.OSVersion.Version.Major == 5;

        /// <summary>
        /// A boolean that represents if OS is Windows Vista
        /// </summary>
        public readonly static bool WVista = Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 0;

        /// <summary>
        /// A boolean that represents if OS is Windows 7 or not
        /// </summary>
        public readonly static bool W7 = Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 1;

        /// <summary>
        /// A boolean that represents if OS is Windows 8 or not
        /// </summary>
        public readonly static bool W8 = Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 2;

        /// <summary>
        /// A boolean that represents if OS is Windows 8.1 or not
        /// </summary>
        public readonly static bool W81 = Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 3;

        /// <summary>
        /// A boolean that represents if OS is Windows 10 or not
        /// </summary>
        public readonly static bool W10 = Environment.OSVersion.Version.Major == 10 && Environment.OSVersion.Version.Minor == 0 && Environment.OSVersion.Version.Build < 22000;

        /// <summary>
        /// A boolean that represents if OS is Windows 11 or not
        /// </summary>
        public readonly static bool W11 = Environment.OSVersion.Version.Major == 10 && Environment.OSVersion.Version.Minor == 0 && Environment.OSVersion.Version.Build >= 22000;

        /// <summary>
        /// A boolean that represents if OS is Windows 12 or not (For near future! :))
        /// </summary>
        public readonly static bool W12 = System.Runtime.InteropServices.RuntimeInformation.OSDescription.Contains("12");

        /// <summary>
        /// A boolean that represents if OS is Windows 10 (19H2=1909) or higher or not at all
        /// </summary>
        public readonly static bool W10_1909 = W12 || W11 || W10 && Conversions.ToDouble(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", 0).ToString()) >= 1909d;

        /// <summary>
        /// A boolean that represents if OS is Windows 11 Build 22523 or higher or not at all
        /// </summary>
        public readonly static bool W11_22523 = W12 || W11 && Conversions.ToDouble(GetReg(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentBuild", 0).ToString()) >= 22523d;

        /// <summary>
        /// A class that represents AnimatorNS
        /// </summary>
        private static AnimatorNS.Animator _Animator;

        public static AnimatorNS.Animator Animator
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _Animator;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                _Animator = value;
            }
        }

        /// <summary>
        /// A class that represents WinPaletter's Settings
        /// </summary>
        public static WPSettings Settings = new WPSettings(WPSettings.Mode.Registry);

        /// <summary>
        /// A class that represents WinPaletter's Language Strings (Loaded at application startup)
        /// </summary>
        public static Localizer Lang = new Localizer();

        /// <summary>
        /// Current applied wallpaper
        /// </summary>
        public static Bitmap Wallpaper, Wallpaper_Unscaled;

        /// <summary>
        /// List of exceptions thrown during theme applying
        /// </summary>
        public static List<Tuple<string, Exception>> Saving_Exceptions = new List<Tuple<string, Exception>>();

        /// <summary>
        /// List of exceptions thrown during theme loading
        /// </summary>
        public static List<Tuple<string, Exception>> Loading_Exceptions = new List<Tuple<string, Exception>>();

        /// <summary>
        /// Get if Application is started as administrator or not
        /// </summary>
        public readonly static bool isElevated = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

        /// <summary>
        /// ImageList for Notifications mini-icons (Loaded at application startup)
        /// </summary>
        public static ImageList Notifications_IL = new ImageList() { ImageSize = new Size(20, 20), ColorDepth = ColorDepth.Depth32Bit };

        /// <summary>
        /// ImageList for Languages Nodes (Loaded at application startup)
        /// </summary>
        public static ImageList Lang_IL = new ImageList() { ImageSize = new Size(16, 16), ColorDepth = ColorDepth.Depth32Bit };

        /// <summary>
        /// A class that contains info about ExplorerPatcher settings
        /// </summary>
        public static ExplorerPatcher EP = new ExplorerPatcher();

        /// <summary>
        /// Variable responsible for the preview type on forms
        /// </summary>
        public static PreviewHelpers.WindowStyle PreviewStyle = PreviewHelpers.WindowStyle.W11;

        /// <summary>
        /// Gets if WinPaletter's current version is designed as Beta or not
        /// <br>Don't forget to make it <b>True</b> when you design a beta one</br>
        /// </summary>
        public readonly static bool IsBeta = true;

        /// <summary>
        /// Global variables to manage WinPaletter theme
        /// </summary>
        public static Theme.Manager TM, TM_Original, TM_FirstTime;

        /// <summary>
        /// Used to make custom controls follow Manager's font smoothing
        /// </summary>
        public static TextRenderingHint RenderingHint = TextRenderingHint.SystemDefault;

        private static ManagementEventWatcher WallMon_Watcher1, WallMon_Watcher2, WallMon_Watcher3, WallMon_Watcher4;
        private static readonly MethodInvoker UpdateDarkModeInvoker = new(() =>
        {
            WPStyle.FetchDarkMode();
            if (Settings.Appearance.AutoDarkMode)
                WPStyle.ApplyStyle();
        });

        private static MethodInvoker UpdateWallpaperInvoker()
        {
            Bitmap wall = FetchSuitableWallpaper(TM, PreviewStyle);
            Forms.MainFrm.pnl_preview.BackgroundImage = wall;
            Forms.MainFrm.pnl_preview_classic.BackgroundImage = wall;
            Forms.Metrics_Fonts.pnl_preview1.BackgroundImage = wall;
            Forms.Metrics_Fonts.pnl_preview2.BackgroundImage = wall;
            Forms.Metrics_Fonts.pnl_preview3.BackgroundImage = wall;
            Forms.Metrics_Fonts.pnl_preview4.BackgroundImage = wall;
            Forms.Metrics_Fonts.Classic_Preview1.BackgroundImage = wall;
            Forms.Metrics_Fonts.Classic_Preview3.BackgroundImage = wall;
            Forms.Metrics_Fonts.Classic_Preview4.BackgroundImage = wall;
            Forms.AltTabEditor.pnl_preview1.BackgroundImage = wall;
            Forms.AltTabEditor.Classic_Preview1.BackgroundImage = wall;
            return null;
        }

        public static Elevator CMD_Wrapper = new();

        public static bool ExternalLink = false;
        public static string ExternalLink_File = "";

        public static Color CopiedColor = default;
        public static MenuEvent ColorEvent = MenuEvent.None;

        public static Font ConsoleFont = new Font("Lucida Console", 7.5f);
        public static Font ConsoleFontMedium = new Font("Lucida Console", 9f);
        public static Font ConsoleFontLarge = new Font("Lucida Console", 10f);

        public static bool ExitAfterException = false;
        public static bool ShowWhatsNew = false;

        public static readonly Process processKiller = new()
        {
            StartInfo = new ProcessStartInfo()
            {
                FileName = PATH_System32 + @"\taskkill.exe",
                Verb = !WXP ? "runas" : "",
                Arguments = "/F /IM explorer.exe",
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = true
            }
        };
        public static readonly Process processExplorer = new()
        {
            StartInfo = new ProcessStartInfo()
            {
                FileName = PATH_explorer,
                Arguments = "",
                Verb = !W81 & !W8 & !WXP ? "runas" : "",
                WindowStyle = ProcessWindowStyle.Normal,
                UseShellExecute = true
            }
        };

        public static List<string> ArgsList = new List<string>();
        public enum MenuEvent
        {
            None,
            Copy,
            Cut,
            Paste,
            Override,
            Delete
        }
        #endregion

        #region Invoking Region
        private static readonly SynchronizationContext _currentContext = SynchronizationContext.Current;
        private static readonly object _invokeLocker = new object();

        private static bool ISynchronizeInvoke_InvokeRequired
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool ISynchronizeInvoke.InvokeRequired { get => ISynchronizeInvoke_InvokeRequired; }

        [Obsolete("This method Is Not supported!", true)]
        private IAsyncResult ISynchronizeInvoke_BeginInvoke(Delegate method, object[] args)
        {
            throw new NotSupportedException("The method Or operation Is Not implemented.");
        }

        [Obsolete("This method Is Not supported!", true)]
        IAsyncResult ISynchronizeInvoke.BeginInvoke(Delegate method, object[] args) => ISynchronizeInvoke_BeginInvoke(method, args);

        [Obsolete("This method Is Not supported!", true)]
        private object ISynchronizeInvoke_EndInvoke(IAsyncResult result)
        {
            throw new NotSupportedException("The method Or operation Is Not implemented.");
        }

        [Obsolete("This method Is Not supported!", true)]
        object ISynchronizeInvoke.EndInvoke(IAsyncResult result) => ISynchronizeInvoke_EndInvoke(result);

        static object Invoke(Delegate method, object[] args)
        {
            if (method is null)
            {
                throw new ArgumentNullException("method");
            }

            lock (_invokeLocker)
            {
                object objectToGet = null;
                var invoker = new SendOrPostCallback((data) => Operators.ConditionalCompareObjectEqual(objectToGet, method.DynamicInvoke(args), false));
                _currentContext.Send(invoker, method.Target);
                return objectToGet;
            }
        }

        object ISynchronizeInvoke.Invoke(Delegate method, object[] args) => Invoke(method, args);

        public static object Invoke(Delegate method)
        {
            return Invoke(method, null);
        }
        #endregion

        #region File Association And Uninstall

        /// <summary>
        /// Associate WinPaletter Files Types in Registry
        /// </summary>
        /// <param name="extension">Extension is the file type to be registered (eg ".wpth")</param>
        /// <param name="className">ClassName is the name of the associated class (eg "WinPaletter.ThemeFile")</param>
        /// <param name="description">Textual description (eg "WinPaletter ThemeFile")</param>
        /// <param name="exeProgram">ExeProgram is the app that manages that extension (eg. Assembly.GetExecutingAssembly().Location)</param>
        public static void CreateFileAssociation(string extension, string className, string description, string iconPath, string exeProgram)
        {

            if (extension.Substring(0, 1) != ".")
                extension = "." + extension;

            if (exeProgram.Contains("\""))
                exeProgram = exeProgram.Replace("\"", "");

            exeProgram = string.Format("\"{0}\"", exeProgram);

            EditReg($"HKEY_CURRENT_USER\\Software\\Classes\\{extension}", "", className, RegistryValueKind.String);
            EditReg($"HKEY_CURRENT_USER\\Software\\Classes\\{className}", "", description, RegistryValueKind.String);
            EditReg($"HKEY_CURRENT_USER\\Software\\Classes\\Shell\\Open", "Icon", exeProgram.Replace("\"", "") + ", 0", RegistryValueKind.String);
            EditReg($"HKEY_CURRENT_USER\\Software\\Classes\\Shell\\Open\\Command", "", exeProgram + " \"%1\"", RegistryValueKind.String);

            if ((className.ToLower() ?? "") == ("WinPaletter.ThemeFile".ToLower() ?? ""))
            {
                EditReg($"HKEY_CURRENT_USER\\Software\\Classes\\{className}\\Shell\\Edit In WinPaletter\\Command", "", exeProgram + "  /edit:\"%1\"", RegistryValueKind.String);
                EditReg($"HKEY_CURRENT_USER\\Software\\Classes\\{className}\\Shell\\Apply by WinPaletter\\Command", "", exeProgram + "  /apply:\"%1\"", RegistryValueKind.String);
            }

            EditReg($"HKEY_CURRENT_USER\\Software\\Classes\\{className}\\DefaultIcon", "", iconPath, RegistryValueKind.String);

            EditReg($"HKEY_CURRENT_USER\\Software\\WinPaletter", "DisplayName", Application.ProductName, RegistryValueKind.String);
            EditReg($"HKEY_CURRENT_USER\\Software\\WinPaletter", "Publisher", Application.CompanyName, RegistryValueKind.String);
            EditReg($"HKEY_CURRENT_USER\\Software\\WinPaletter", "Version", AppVersion, RegistryValueKind.String);

            // Notify Windows that file associations have changed
            NativeMethods.Shell32.SHChangeNotify(NativeMethods.Shell32.SHCNE_ASSOCCHANGED, NativeMethods.Shell32.SHCNF_IDLIST, 0, 0);
        }

        /// <summary>
        /// Removes WinPaletter Files Types Associate From Registry
        /// </summary>
        /// <param name="extension">Extension is the file type to be removed (eg ".wpth")</param>
        /// <param name="className">ClassName is the name of the associated class to be removed (eg "WinPaletter.ThemeFile")</param>
        public static void DeleteFileAssociation(string extension, string className)
        {

            if (extension.Substring(0, 1) != ".")
                extension = "." + extension;

            RegistryKey mainKey, descriptionKey;
            mainKey = Registry.CurrentUser.OpenSubKey(@"Software\Classes", true);
            descriptionKey = Registry.CurrentUser.OpenSubKey(@"Software\WinPaletter", true);

            try
            {
                mainKey.DeleteSubKeyTree(extension, false);
                mainKey.DeleteSubKeyTree(className, false);

                descriptionKey.DeleteValue("DisplayName", false);
                descriptionKey.DeleteValue("Publisher", false);
                descriptionKey.DeleteValue("Version", false);
            }

            catch (Exception e)
            {
            }
            finally
            {
                if (mainKey is not null)
                    mainKey.Close();
                if (descriptionKey is not null)
                    descriptionKey.Close();
            }

            // Notify Windows that file associations have changed
            NativeMethods.Shell32.SHChangeNotify(NativeMethods.Shell32.SHCNE_ASSOCCHANGED, NativeMethods.Shell32.SHCNF_IDLIST, 0, 0);
        }
        public static void CreateUninstaller()
        {
            string guidText = Application.ProductName;
            string RegPath = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Uninstall\" + guidText;
            string exe = Assembly.GetExecutingAssembly().Location;

            if (!System.IO.Directory.Exists(PATH_appData))
                System.IO.Directory.CreateDirectory(PATH_appData);
            System.IO.File.WriteAllBytes(PATH_appData + @"\uninstall.ico", Properties.Resources.Icon_Uninstall.ToByteArray());

            {
                EditReg(RegPath, "DisplayName", "WinPaletter", RegistryValueKind.String);
                EditReg(RegPath, "ApplicationVersion", AppVersion, RegistryValueKind.String);
                EditReg(RegPath, "DisplayVersion", AppVersion, RegistryValueKind.String);
                EditReg(RegPath, "Publisher", Application.CompanyName, RegistryValueKind.String);
                EditReg(RegPath, "DisplayIcon", PATH_appData + @"\uninstall.ico", RegistryValueKind.String);
                EditReg(RegPath, "URLInfoAbout", Properties.Resources.Link_Repository, RegistryValueKind.String);
                EditReg(RegPath, "Contact", Properties.Resources.Link_Repository, RegistryValueKind.String);
                EditReg(RegPath, "InstallDate", DateTime.Now.ToString("yyyyMMdd"), RegistryValueKind.String);
                EditReg(RegPath, "Comments", Lang.Uninstall_Comment, RegistryValueKind.String);
                EditReg(RegPath, "UninstallString", exe + " /uninstall", RegistryValueKind.String);
                EditReg(RegPath, "QuietUninstallString", exe + " /uninstall", RegistryValueKind.String);
                EditReg(RegPath, "InstallLocation", new System.IO.FileInfo(Application.ExecutablePath).DirectoryName, RegistryValueKind.String);
                EditReg(RegPath, "NoModify", 1, RegistryValueKind.DWord);
                EditReg(RegPath, "NoRepair", 1, RegistryValueKind.DWord);
                EditReg(RegPath, "EstimatedSize", new System.IO.FileInfo(exe).Length / 1024d, RegistryValueKind.DWord);
            }
        }
        public static void Uninstall_Quiet()
        {
            DeleteFileAssociation(".wpth", "WinPaletter.ThemeFile");
            DeleteFileAssociation(".wpsf", "WinPaletter.SettingsFile");
            DeleteFileAssociation(".wptp", "WinPaletter.ThemeResourcesPack");

            Registry.CurrentUser.DeleteSubKeyTree(@"Software\WinPaletter", false);

            try
            {
                if (!WXP && System.IO.File.Exists(PATH_appData + @"\WindowsStartup_Backup.wav"))
                {
                    PE.ReplaceResource(PATH_imageres, "WAV", WVista ? 5051 : 5080, System.IO.File.ReadAllBytes(PATH_appData + @"\WindowsStartup_Backup.wav"));
                }
            }
            catch
            {
            }

            if (System.IO.Directory.Exists(PATH_appData))
            {
                System.IO.Directory.Delete(PATH_appData, true);
                if (!WXP)
                {
                    Theme.Manager.ResetCursorsToAero();
                    if (Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                        Theme.Manager.ResetCursorsToAero(@"HKEY_USERS\.DEFAULT");
                }

                else
                {
                    Theme.Manager.ResetCursorsToNone_XP();
                    if (Settings.ThemeApplyingBehavior.Cursors_HKU_DEFAULT_Prefs == WPSettings.Structures.ThemeApplyingBehavior.OverwriteOptions.Overwrite)
                        Theme.Manager.ResetCursorsToNone_XP(@"HKEY_USERS\.DEFAULT");

                }
            }

            string guidText = Application.ProductName;
            string RegPath = @"Software\Microsoft\Windows\CurrentVersion\Uninstall";
            Registry.CurrentUser.OpenSubKey(RegPath, true).DeleteSubKeyTree(guidText, false);

            using (var Prc = Process.GetCurrentProcess())
            {
                Prc.Kill();
            }
        }
        #endregion

        #region Wallpaper and User Preferences System
        private static void FetchStockWallpaper()
        {
            using (var wall_New = new Bitmap((Bitmap)GetWallpaper().Clone()))
            {

                Wallpaper_Unscaled = (Bitmap)wall_New.Clone();
                Wallpaper = (Bitmap)wall_New.GetThumbnailImage(Computer.Screen.Bounds.Width, Computer.Screen.Bounds.Height, null, IntPtr.Zero);
            }
        }
        public static Bitmap FetchSuitableWallpaper(Theme.Manager TM, PreviewHelpers.WindowStyle PreviewConfig)
        {
            using (PictureBox picbox = new() { Size = Forms.MainFrm.pnl_preview.Size, BackColor = TM.Win32.Background })
            {
                Bitmap Wall;

                if (!TM.Wallpaper.Enabled)
                {
                    FetchStockWallpaper();
                    Wall = Wallpaper;
                }
                else
                {
                    bool condition0 = PreviewConfig == PreviewHelpers.WindowStyle.W11 & TM.WallpaperTone_W11.Enabled;
                    bool condition1 = PreviewConfig == PreviewHelpers.WindowStyle.W10 & TM.WallpaperTone_W10.Enabled;
                    bool condition2 = PreviewConfig == PreviewHelpers.WindowStyle.W81 & TM.WallpaperTone_W81.Enabled;
                    bool condition3 = PreviewConfig == PreviewHelpers.WindowStyle.W7 & TM.WallpaperTone_W7.Enabled;
                    bool condition4 = PreviewConfig == PreviewHelpers.WindowStyle.WVista & TM.WallpaperTone_WVista.Enabled;
                    bool condition5 = PreviewConfig == PreviewHelpers.WindowStyle.WXP & TM.WallpaperTone_WXP.Enabled;
                    bool condition = condition0 || condition1 || condition2 || condition3 || condition4 || condition5;

                    if (condition)
                    {
                        switch (PreviewConfig)
                        {
                            case PreviewHelpers.WindowStyle.W11:
                                {
                                    Wall = PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_W11);
                                    break;
                                }

                            case PreviewHelpers.WindowStyle.W10:
                                {
                                    Wall = PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_W10);
                                    break;
                                }

                            case PreviewHelpers.WindowStyle.W81:
                                {
                                    Wall = PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_W81);
                                    break;
                                }

                            case PreviewHelpers.WindowStyle.W7:
                                {
                                    Wall = PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_W7);
                                    break;
                                }

                            case PreviewHelpers.WindowStyle.WVista:
                                {
                                    Wall = PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_WVista);
                                    break;
                                }

                            case PreviewHelpers.WindowStyle.WXP:
                                {
                                    Wall = PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_WXP);
                                    break;
                                }

                            default:
                                {
                                    Wall = PreviewHelpers.GetTintedWallpaper(TM.WallpaperTone_W11);
                                    break;
                                }

                        }
                    }

                    else if (TM.Wallpaper.WallpaperType == Theme.Structures.Wallpaper.WallpaperTypes.Picture)
                    {
                        if (System.IO.File.Exists(TM.Wallpaper.ImageFile))
                        {
                            Wall = Bitmap_Mgr.Load(TM.Wallpaper.ImageFile);
                        }
                        else
                        {
                            FetchStockWallpaper();
                            Wall = Wallpaper;
                        }
                    }

                    else if (TM.Wallpaper.WallpaperType == Theme.Structures.Wallpaper.WallpaperTypes.SolidColor)
                    {
                        Wall = null;
                    }

                    else if (TM.Wallpaper.WallpaperType == Theme.Structures.Wallpaper.WallpaperTypes.SlideShow)
                    {

                        if (TM.Wallpaper.SlideShow_Folder_or_ImagesList)
                        {
                            string[] ls = System.IO.Directory.EnumerateFiles(TM.Wallpaper.Wallpaper_Slideshow_ImagesRootPath, "*.*", System.IO.SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".bmp") || s.EndsWith(".jpg") || s.EndsWith(".png") || s.EndsWith(".gif")).ToArray();


                            if (ls.Count() > 0 && System.IO.File.Exists(ls[0]))
                            {
                                Wall = Bitmap_Mgr.Load(ls[0]);
                            }

                            else
                            {
                                FetchStockWallpaper();
                                Wall = Wallpaper;
                            }
                        }

                        else if (TM.Wallpaper.Wallpaper_Slideshow_Images.Count() > 0 && System.IO.File.Exists(TM.Wallpaper.Wallpaper_Slideshow_Images[0]))
                        {
                            Wall = Bitmap_Mgr.Load(TM.Wallpaper.Wallpaper_Slideshow_Images[0]);
                        }
                        else
                        {
                            FetchStockWallpaper();
                            Wall = Wallpaper;
                        }
                    }
                    else
                    {
                        FetchStockWallpaper();
                        Wall = Wallpaper;
                    }
                }

                if (Wall is not null)
                {

                    double ScaleW = 1;
                    double ScaleH = 1;

                    if (Wall.Width > Screen.PrimaryScreen.Bounds.Size.Width | Wall.Height > Screen.PrimaryScreen.Bounds.Size.Height)
                    {
                        ScaleW = (1920 / (double)picbox.Size.Width);
                        ScaleH = (1080 / (double)picbox.Size.Height);
                    }

                    Wall = Wall.Resize((int)Math.Round((double)Wall.Width / ScaleW), (int)Math.Round((double)Wall.Height / ScaleH));

                    if (TM.Wallpaper.WallpaperStyle == Theme.Structures.Wallpaper.WallpaperStyles.Fill)
                    {
                        picbox.SizeMode = PictureBoxSizeMode.CenterImage;
                        Wall = (Bitmap)((Bitmap)Wall.Clone()).FillScale(picbox.Size);
                    }

                    else if (TM.Wallpaper.WallpaperStyle == Theme.Structures.Wallpaper.WallpaperStyles.Fit)
                    {
                        picbox.SizeMode = PictureBoxSizeMode.Zoom;
                    }

                    else if (TM.Wallpaper.WallpaperStyle == Theme.Structures.Wallpaper.WallpaperStyles.Stretched)
                    {
                        picbox.SizeMode = PictureBoxSizeMode.StretchImage;
                    }

                    else if (TM.Wallpaper.WallpaperStyle == Theme.Structures.Wallpaper.WallpaperStyles.Centered)
                    {
                        picbox.SizeMode = PictureBoxSizeMode.CenterImage;
                    }

                    else if (TM.Wallpaper.WallpaperStyle == Theme.Structures.Wallpaper.WallpaperStyles.Tile)
                    {
                        picbox.SizeMode = PictureBoxSizeMode.Normal;
                        Wall = ((Bitmap)Wall.Clone()).Tile(picbox.Size);

                    }

                }

                picbox.Image = Wall;

                return picbox.ToBitmap();
            }
        }
        public static Bitmap GetWallpaper()
        {
            string WallpaperPath = GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", "").ToString();
            int WallpaperType = Conversions.ToInteger(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "BackgroundType", 0));

            if (System.IO.File.Exists(WallpaperPath) && WallpaperType != 1)
            {
                return new Bitmap(Bitmap_Mgr.Load(WallpaperPath).GetThumbnailImage(Computer.Screen.Bounds.Width, Computer.Screen.Bounds.Height, null, IntPtr.Zero));
            }
            else
            {
                return (Bitmap)(GetReg(@"HKEY_CURRENT_USER\Control Panel\Colors", "Background", "0 0 0").ToString().FromWin32RegToColor().ToBitmap(Computer.Screen.Bounds.Size));
            }
        }
        public static void WallpaperType_Changed(object sender, EventArrivedEventArgs e)
        {
            int WallpaperType = Conversions.ToInteger(GetReg(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers", "BackgroundType", 0));
            var S = new Stopwatch();
            if (WallpaperType != 1)
            {
                S.Reset();
                S.Start();
                while (!System.IO.File.Exists(GetReg(@"HKEY_CURRENT_USER\Control Panel\Desktop", "Wallpaper", "").ToString()))
                {
                    if (S.ElapsedMilliseconds > 5000L)
                        break;
                }
                S.Stop();
                Wallpaper_Changed();
            }
        }
        public static void Monitor()
        {
            var currentUser = WindowsIdentity.GetCurrent();
            string KeyPath;
            string valueName;
            string Base;

            KeyPath = @"Control Panel\Desktop";
            valueName = "Wallpaper";
            Base = string.Format(@"SELECT * FROM RegistryValueChangeEvent WHERE Hive='HKEY_USERS' AND KeyPath='{0}\\{1}' AND ValueName='{2}'", currentUser.User.Value, KeyPath.Replace(@"\", @"\\"), valueName);
            var query1 = new WqlEventQuery(Base);
            WallMon_Watcher1 = new ManagementEventWatcher(query1);

            KeyPath = @"Control Panel\Colors";
            valueName = "Background";
            Base = string.Format(@"SELECT * FROM RegistryValueChangeEvent WHERE Hive='HKEY_USERS' AND KeyPath='{0}\\{1}' AND ValueName='{2}'", currentUser.User.Value, KeyPath.Replace(@"\", @"\\"), valueName);
            var query2 = new WqlEventQuery(Base);
            WallMon_Watcher2 = new ManagementEventWatcher(query2);

            WallMon_Watcher1.EventArrived += Wallpaper_Changed_EventHandler;
            WallMon_Watcher1.Start();

            WallMon_Watcher2.EventArrived += Wallpaper_Changed_EventHandler;
            WallMon_Watcher2.Start();

            if (W10 || W11)
            {
                KeyPath = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Wallpapers";
                valueName = "BackgroundType";
                Base = string.Format(@"SELECT * FROM RegistryValueChangeEvent WHERE Hive='HKEY_USERS' AND KeyPath='{0}\\{1}' AND ValueName='{2}'", currentUser.User.Value, KeyPath.Replace(@"\", @"\\"), valueName);
                var query3 = new WqlEventQuery(Base);
                WallMon_Watcher3 = new ManagementEventWatcher(query3);

                KeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
                valueName = "AppsUseLightTheme";
                Base = string.Format(@"SELECT * FROM RegistryValueChangeEvent WHERE Hive='HKEY_USERS' AND KeyPath='{0}\\{1}' AND ValueName='{2}'", currentUser.User.Value, KeyPath.Replace(@"\", @"\\"), valueName);
                var query4 = new WqlEventQuery(Base);
                WallMon_Watcher4 = new ManagementEventWatcher(query4);

                WallMon_Watcher3.EventArrived += WallpaperType_Changed;
                WallMon_Watcher3.Start();

                WallMon_Watcher4.EventArrived += DarkMode_Changed_EventHandler;
                WallMon_Watcher4.Start();
            }

            else
            {
                SystemEvents.UserPreferenceChanged += OldWinPreferenceChanged;
            }

        }
        public static void OldWinPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            if (WXP && e.Category == UserPreferenceCategory.General)
            {
                Wallpaper_Changed();
            }
            else if (e.Category == UserPreferenceCategory.Desktop | e.Category == UserPreferenceCategory.Color)
                Wallpaper_Changed();
        }
        public static void DarkMode_Changed_EventHandler(object sender, EventArgs e)
        {
            DarkMode_Changed();
        }
        public static void DarkMode_Changed()
        {
            Invoke(UpdateDarkModeInvoker);
        }
        public static void Wallpaper_Changed_EventHandler(object sender, EventArgs e)
        {
            Wallpaper_Changed();
        }
        public static void Wallpaper_Changed()
        {
            Invoke(UpdateWallpaperInvoker);

        }
        #endregion

        #region Helpers
        private static bool IsSecondInstance()
        {
            mutex = new Mutex(true, Application.ProductName, out bool createdNew);
            return !createdNew;
        }

        static private void LoadThemeManager()
        {
            if (W11)
                PreviewStyle = PreviewHelpers.WindowStyle.W11;
            else if (W10)
                PreviewStyle = PreviewHelpers.WindowStyle.W10;
            else if (W81)
                PreviewStyle = PreviewHelpers.WindowStyle.W81;
            else if (W8)
                PreviewStyle = PreviewHelpers.WindowStyle.W81;
            else if (W7)
                PreviewStyle = PreviewHelpers.WindowStyle.W7;
            else if (WVista)
                PreviewStyle = PreviewHelpers.WindowStyle.WVista;
            else if (WXP)
                PreviewStyle = PreviewHelpers.WindowStyle.WXP;
            else
                PreviewStyle = PreviewHelpers.WindowStyle.W11;

            // Load Manager
            if (!ExternalLink)
            {
                TM = new Theme.Manager(Theme.Manager.Source.Registry);
            }
            else
            {
                TM = new Theme.Manager(Theme.Manager.Source.File, ExternalLink_File);
                Forms.MainFrm.OpenFileDialog1.FileName = ExternalLink_File;
                Forms.MainFrm.SaveFileDialog1.FileName = ExternalLink_File;
                ExternalLink = false;
                ExternalLink_File = "";
            }

            TM_Original = (Theme.Manager)TM.Clone();
            TM_FirstTime = (Theme.Manager)TM.Clone();
        }

        static private void DeleteUpdateResiduals()
        {
            try
            {
                if (System.IO.File.Exists("oldWinpaletter.trash"))
                    FileSystem.Kill("oldWinpaletter.trash");
                if (System.IO.File.Exists("oldWinpaletter_2.trash"))
                    FileSystem.Kill("oldWinpaletter_2.trash");
            }
            catch
            {
            }
        }

        static private void GetMemoryFonts()
        {
            try
            {
                MemoryFonts.AddMemoryFont(Properties.Resources.JetBrainsMono_Medium);
                ConsoleFont = MemoryFonts.GetFont(0, 7.75f);
                ConsoleFontMedium = MemoryFonts.GetFont(0, 9f);
                ConsoleFontLarge = MemoryFonts.GetFont(0, 10f);
            }
            catch
            {
                ConsoleFont = new Font("Lucida Console", 7.5f);
                ConsoleFontMedium = new Font("Lucida Console", 9f);
                ConsoleFontLarge = new Font("Lucida Console", 10f);
            }
        }

        static private void ExecuteArgs()
        {
            if (Environment.GetCommandLineArgs().Count() > 1)
            {
                ArgsList.Clear();
                for (int x = 1, loopTo = Environment.GetCommandLineArgs().Count() - 1; x <= loopTo; x++)
                    ArgsList.Add(Environment.GetCommandLineArgs()[x]);
            }

            foreach (string arg in ArgsList)
            {
                if (arg.ToLower() == "/exportlanguage")
                {
                    Lang.ExportJSON(string.Format("language-en {0}.{1}.{2} {3}-{4}-{5}.json", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));
                    WPStyle.MsgBox(Lang.LngExported, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    using Process Prc = Process.GetCurrentProcess();
                    Prc.Kill();
                    break;
                }

                else if (arg.ToLower() == "/uninstall")
                {
                    Forms.Uninstall.ShowDialog();
                    using Process Prc = Process.GetCurrentProcess();
                    Prc.Kill();
                    break;
                }

                else if (arg.ToLower() == "/uninstall-quiet")
                {
                    Uninstall_Quiet();
                    break;
                }

                else if (arg.StartsWith("/convert:", _ignore))
                {
                    CMD_Convert(arg, true);
                }

                else if (arg.StartsWith("/convert-list:", _ignore))
                {
                    CMD_Convert_List(arg, true);

                }

                else if (!arg.StartsWith("/apply:", _ignore) & !arg.StartsWith("/edit:", _ignore) & !arg.StartsWith("/convert:", _ignore) & !arg.StartsWith("/convert-list:", _ignore))
                {
                    if (System.IO.Path.GetExtension(arg).ToLower() == ".wpth")
                    {
                        if (Settings.FileTypeManagement.OpeningPreviewInApp_or_AppliesIt)
                        {
                            ExternalLink = true;
                            ExternalLink_File = arg;
                        }
                        else
                        {
                            var TMx = new Theme.Manager(Theme.Manager.Source.File, arg);
                            TMx.Save(Theme.Manager.Source.Registry, arg);
                            if (Settings.ThemeApplyingBehavior.AutoRestartExplorer)
                                RestartExplorer();
                            using Process Prc = Process.GetCurrentProcess();
                            Prc.Kill();
                        }
                    }

                    if (System.IO.Path.GetExtension(arg).ToLower() == ".wpsf")
                    {
                        Forms.SettingsX._External = true;
                        Forms.SettingsX._File = arg;
                        Forms.SettingsX.ShowDialog();
                        using Process Prc = Process.GetCurrentProcess();
                        Prc.Kill();
                    }
                }

                else if (arg.StartsWith("/apply:", _ignore))
                {
                    string File = arg.Remove(0, "/apply:".Count());
                    File = File.Replace("\"", "");
                    if (System.IO.File.Exists(File))
                    {
                        var TMx = new Theme.Manager(Theme.Manager.Source.File, File);
                        TMx.Save(Theme.Manager.Source.Registry);
                        if (Settings.ThemeApplyingBehavior.AutoRestartExplorer)
                            RestartExplorer();
                        using Process Prc = Process.GetCurrentProcess();
                        Prc.Kill();
                    }
                }

                else if (arg.StartsWith("/edit:", _ignore))
                {
                    string File = arg.Remove(0, "/edit:".Count());
                    File = File.Replace("\"", "");
                    ExternalLink = true;
                    ExternalLink_File = File;
                }
            }
        }

        static private void ExecuteArgs_ProgramStarted(string[] args)
        {
            if (args.Count() > 0)
            {
                foreach (string arg in args) 
                {
                    try
                    {
                        if ((arg.ToLower() ?? "") == ("/exportlanguage".ToLower() ?? ""))
                        {
                            WPStyle.MsgBox(Lang.LngShouldClose, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        else if (arg.ToLower() == "/uninstall")
                        {
                            Forms.Uninstall.ShowDialog();
                        }

                        else if (arg.ToLower() == "/uninstall-quiet")
                        {
                            Uninstall_Quiet();
                        }

                        else if (arg.StartsWith("/convert:", _ignore))
                        {
                            CMD_Convert(arg, false);
                        }

                        else if (arg.StartsWith("/convert-list:", _ignore))
                        {
                            CMD_Convert_List(arg, false);
                        }

                        else if (!arg.StartsWith("/apply:", _ignore) & !arg.StartsWith("/edit:", _ignore))
                        {
                            if (System.IO.Path.GetExtension(arg).ToLower() == ".wpth")
                            {
                                Forms.ComplexSave.GetResponse(Forms.MainFrm.SaveFileDialog1, () => Forms.ThemeLog.Apply_Theme(), () => Forms.ThemeLog.Apply_Theme(TM_FirstTime), () => Forms.ThemeLog.Apply_Theme(Theme.Default.Get()));

                                TM = new Theme.Manager(Theme.Manager.Source.File, arg);
                                TM_Original = (Theme.Manager)TM.Clone();
                                Forms.MainFrm.OpenFileDialog1.FileName = arg;
                                Forms.MainFrm.SaveFileDialog1.FileName = arg;
                                Forms.MainFrm.LoadFromTM(TM);
                                Forms.MainFrm.ApplyColorsToElements(TM);

                                if (!Settings.FileTypeManagement.OpeningPreviewInApp_or_AppliesIt)
                                {
                                    Forms.ThemeLog.Apply_Theme();
                                }
                            }

                            if (System.IO.Path.GetExtension(arg).ToLower() == ".wpsf")
                            {
                                Forms.SettingsX._External = true;
                                Forms.SettingsX._File = arg;
                                Forms.SettingsX.ShowDialog();
                            }
                        }

                        else
                        {
                            if (arg.StartsWith("/apply:", _ignore))
                            {
                                string File = arg.Remove(0, "/apply:".Count());
                                File = File.Replace("\"", "");
                                if (System.IO.File.Exists(File))
                                {
                                    var TMx = new Theme.Manager(Theme.Manager.Source.File, File);
                                    TMx.Save(Theme.Manager.Source.Registry);
                                    if (Settings.ThemeApplyingBehavior.AutoRestartExplorer)
                                        RestartExplorer();
                                }
                            }

                            if (arg.StartsWith("/edit:", _ignore))
                            {
                                string File = arg.Remove(0, "/edit:".Count());
                                File = File.Replace("\"", "");

                                Forms.ComplexSave.GetResponse(Forms.MainFrm.SaveFileDialog1, () => Forms.ThemeLog.Apply_Theme(), () => Forms.ThemeLog.Apply_Theme(TM_FirstTime), () => Forms.ThemeLog.Apply_Theme(Theme.Default.Get()));

                                TM = new Theme.Manager(Theme.Manager.Source.File, File);
                                TM_Original = (Theme.Manager)TM.Clone();
                                Forms.MainFrm.OpenFileDialog1.FileName = File;
                                Forms.MainFrm.SaveFileDialog1.FileName = File;
                                Forms.MainFrm.LoadFromTM(TM);
                                Forms.MainFrm.ApplyColorsToElements(TM);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Forms.BugReport.ThrowError(ex);
                        BringApplicationToFront();
                    }
                }
            }
            else
            {
                BringApplicationToFront();
            }
        }

        public static void BringApplicationToFront()
        {
            try
            {
                Application.OpenForms[Forms.MainFrm.Name].BringToFront();
            }
            catch
            { }
        }

        static public void CMD_Convert(string arg, bool KillProcessAfterConvert)
        {
            try
            {
                string[] arr = arg.Remove(0, "/convert:".Count()).Split('|');
                string Source = arr[0];
                string Destination = arr[1];
                string Compress = Settings.FileTypeManagement.CompressThemeFile ? "1" : "0";
                string OldWPTH = "0";
                if (arr.Count() == 3)
                    Compress = arr[2];
                if (arr.Count() == 4)
                    OldWPTH = arr[3];

                var _Convert = new Converter();

                if (System.IO.File.Exists(Source) && !(_Convert.FetchFile(Source) == Converter_CP.WP_Format.Error))
                {
                    _Convert.Convert(Source, Destination, Compress == "1", OldWPTH == "1");
                }
                else
                {
                    WPStyle.MsgBox(Lang.Convert_Error_Phrasing, MessageBoxButtons.OK, MessageBoxIcon.Error, Source);
                }
            }

            catch (Exception ex)
            {
                Forms.BugReport.ThrowError(ex);
            }

            if (KillProcessAfterConvert)
            {
                using Process Prc = Process.GetCurrentProcess();
                Prc.Kill();
            }
        }

        static public void CMD_Convert_List(string arg, bool KillProcessAfterConvert)
        {
            try
            {
                string source = arg.Remove(0, "/convert-list:".Count());
                var _Convert = new Converter();

                if (System.IO.File.Exists(source))
                {
                    foreach (string File in System.IO.File.ReadAllLines(source))
                    {
                        string f;
                        string compress = Settings.FileTypeManagement.CompressThemeFile ? "1" : "0";
                        string OldWPTH = "0";

                        if (!string.IsNullOrWhiteSpace(File))
                        {
                            if (!File.Contains("|"))
                            {
                                f = File.Replace("\"", "");
                            }
                            else
                            {
                                string[] arr = File.Split('|');
                                f = arr[0].Replace("\"", "");
                                if (arr.Count() == 2)
                                    compress = arr[1];
                                if (arr.Count() == 3)
                                    compress = arr[2];
                            }

                            var FI = new System.IO.FileInfo(f);
                            string Name = System.IO.Path.GetFileNameWithoutExtension(FI.Name);
                            string Dir = FI.FullName.Replace(FI.FullName.Split('\\').Last(), "WinPaletterConversion");
                            string SaveAs = Dir + @"\" + Name + ".wpth";

                            if (!(_Convert.FetchFile(f) == Converter_CP.WP_Format.Error))
                            {
                                if (!System.IO.Directory.Exists(Dir))
                                    System.IO.Directory.CreateDirectory(Dir);
                                _Convert.Convert(f, SaveAs, compress == "1", OldWPTH == "1");
                            }
                            else
                            {
                                WPStyle.MsgBox(Lang.Convert_Error_Phrasing, MessageBoxButtons.OK, MessageBoxIcon.Error, f);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Forms.BugReport.ThrowError(ex);
            }

            if (KillProcessAfterConvert)
            {
                using Process Prc = Process.GetCurrentProcess();
                Prc.Kill();
            }
        }

        static private void LoadLanguage()
        {
            if (Settings.Language.Enabled)
            {
                try
                {
                    Lang.LoadLanguageFromJSON(Settings.Language.File);
                }
                catch (Exception ex)
                {
                    Forms.BugReport.ThrowError(ex);
                }
            }
        }

        static private void CheckIfLicenseChecked()
        {
            if (!Settings.General.LicenseAccepted)
            {
                if (Forms.LicenseForm.ShowDialog() != DialogResult.OK)
                {
                    using Process Prc = Process.GetCurrentProcess();
                    Prc.Kill();
                }
            }
        }

        static private void StartWallpaperMonitor()
        {
            if (!WXP)
            {
                try
                {
                    Monitor();
                }
                catch (Exception ex)
                {
                    if (WPStyle.MsgBox(Lang.MonitorIssue, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, Lang.MonitorIssue2 + "\r\n" + Lang.TM_RestoreCursorsErrorPressOK) == DialogResult.OK)
                    {
                        Forms.BugReport.ThrowError(ex);
                    }
                }
            }
            else
            {
                SystemEvents.UserPreferenceChanged += OldWinPreferenceChanged;
            }
        }

        static private void InitializeImageLists()
        {
            Notifications_IL.Images.Add("info", Properties.Resources.notify_info);
            Notifications_IL.Images.Add("apply", Properties.Resources.notify_applying);
            Notifications_IL.Images.Add("error", Properties.Resources.notify_error);
            Notifications_IL.Images.Add("warning", Properties.Resources.notify_warning);
            Notifications_IL.Images.Add("time", Properties.Resources.notify_time);
            Notifications_IL.Images.Add("success", Properties.Resources.notify_success);
            Notifications_IL.Images.Add("skip", Properties.Resources.notify_skip);
            Notifications_IL.Images.Add("admin", Properties.Resources.notify_administrator);
            Notifications_IL.Images.Add("reg_add", Properties.Resources.notify_reg_add);
            Notifications_IL.Images.Add("reg_delete", Properties.Resources.notify_reg_delete);
            Notifications_IL.Images.Add("reg_skip", Properties.Resources.notify_reg_skip);
            Notifications_IL.Images.Add("task_add", Properties.Resources.notify_task_add);
            Notifications_IL.Images.Add("task_remove", Properties.Resources.notify_task_remove);
            Notifications_IL.Images.Add("file_rename", Properties.Resources.notify_file_rename);
            Notifications_IL.Images.Add("dll", Properties.Resources.notify_dll);
            Notifications_IL.Images.Add("pe_patch", Properties.Resources.notify_pe_patch);
            Notifications_IL.Images.Add("pe_backup", Properties.Resources.notify_pe_backup);
            Notifications_IL.Images.Add("pe_restore", Properties.Resources.notify_pe_restore);

            Lang_IL.Images.Add("main", Properties.Resources.LangNode_Main);
            Lang_IL.Images.Add("value", Properties.Resources.LangNode_Value);
            Lang_IL.Images.Add("json", Properties.Resources.LangNode_JSON);

            Saving_Exceptions.Clear();
            Loading_Exceptions.Clear();
        }

        static private void AssociateFiles()
        {
            try
            {
                if (Settings.FileTypeManagement.AutoAddExt)
                {
                    if (!System.IO.Directory.Exists(PATH_appData))
                        System.IO.Directory.CreateDirectory(PATH_appData);

                    System.IO.File.WriteAllBytes(PATH_appData + @"\fileextension.ico", Properties.Resources.fileextension.ToByteArray());
                    System.IO.File.WriteAllBytes(PATH_appData + @"\settingsfile.ico", Properties.Resources.settingsfile.ToByteArray());
                    System.IO.File.WriteAllBytes(PATH_appData + @"\themerespack.ico", Properties.Resources.ThemesResIcon.ToByteArray());

                    CreateFileAssociation(".wpth", "WinPaletter.ThemeFile", Lang.WP_Theme_FileType, PATH_appData + @"\fileextension.ico", Assembly.GetExecutingAssembly().Location);
                    CreateFileAssociation(".wpsf", "WinPaletter.SettingsFile", Lang.WP_Settings_FileType, PATH_appData + @"\settingsfile.ico", Assembly.GetExecutingAssembly().Location);
                    CreateFileAssociation(".wptp", "WinPaletter.ThemeResourcesPack", Lang.WP_ResourcesPack_FileType, PATH_appData + @"\themerespack.ico", Assembly.GetExecutingAssembly().Location);
                }
            }
            catch
            {
            }
        }

        static private void DetectIfWPStartedWithClassicTheme()
        {
            System.Text.StringBuilder vsFile = new(260);
            System.Text.StringBuilder colorName = new(260);
            System.Text.StringBuilder sizeName = new(260);
            NativeMethods.UxTheme.GetCurrentThemeName(vsFile, vsFile.Capacity, colorName, colorName.Capacity, sizeName, sizeName.Capacity);
            StartedWithClassicTheme = string.IsNullOrEmpty(vsFile.ToString());
        }

        static private void ExtractLuna()
        {
            try
            {
                if (!System.IO.Directory.Exists(PATH_appData + @"\VisualStyles\Luna"))
                    System.IO.Directory.CreateDirectory(PATH_appData + @"\VisualStyles\Luna");
                System.IO.File.WriteAllBytes(PATH_appData + @"\VisualStyles\Luna\Luna.zip", Properties.Resources.luna);
                using (System.IO.FileStream s = new(PATH_appData + @"\VisualStyles\Luna\Luna.zip", System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    using (var z = new System.IO.Compression.ZipArchive(s, System.IO.Compression.ZipArchiveMode.Read))
                    {
                        foreach (System.IO.Compression.ZipArchiveEntry entry in z.Entries)
                        {
                            if (entry.FullName.Contains(@"\"))
                            {
                                string dest = System.IO.Path.Combine(PATH_appData + @"\VisualStyles\Luna", entry.FullName);
                                string dest_dir = dest.Replace(@"\" + dest.Split('\\').Last(), "");
                                if (!System.IO.Directory.Exists(dest_dir))
                                    System.IO.Directory.CreateDirectory(dest_dir);
                            }
                            entry.ExtractToFile(System.IO.Path.Combine(PATH_appData + @"\VisualStyles\Luna", entry.FullName), true);
                        }
                    }
                    s.Close();
                }
                System.IO.File.WriteAllText(PATH_appData + @"\VisualStyles\Luna\luna.theme", string.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=NormalColor{1}Size=NormalSize", PATH_appData + @"\VisualStyles\Luna\luna.msstyles", "\r\n"));
            }
            catch (Exception ex)
            {
                Forms.BugReport.ThrowError(ex);
            }
        }

        static private void BackupWindowsStartupSound()
        {
            try
            {
                if (!WXP && !System.IO.File.Exists(PATH_appData + @"\WindowsStartup_Backup.wav"))
                {
                    byte[] SoundBytes = PE.GetResource(PATH_imageres, "WAVE", WVista ? 5051 : 5080);
                    System.IO.File.WriteAllBytes(PATH_appData + @"\WindowsStartup_Backup.wav", SoundBytes);
                }
            }
            catch (Exception ex)
            {
                Forms.BugReport.ThrowError(ex);
            }
        }

        static public void InitializeCMDWrapper()
        {
            if (!isElevated && !Settings.Miscellaneous.DontUseWPElevatorConsole)
                CMD_Wrapper.Start();
        }
        #endregion

        #region Domain (External Resources) and Exceptions Handling
        static private Assembly DomainCheck(object sender, ResolveEventArgs e)
        {
            return GetAssemblyFromZIP(e.Name);
        }

        static public Assembly GetAssemblyFromZIP(string Name)
        {
            Name = new AssemblyName(Name).Name;

            if (Name.StartsWith("WinPaletter.resources", _ignore))
                return null;

            byte[] b = null;

            using (var ms = new System.IO.MemoryStream(Properties.Resources.Assemblies))
            {
                using (var zip = new ZipArchive(ms))
                {
                    if (zip.Entries.Any(entry => entry.Name.EndsWith(Name + ".dll", _ignore)))
                    {
                        using (var _as = new System.IO.MemoryStream())
                        {
                            zip.GetEntry(Name + ".dll").Open().CopyTo(_as);
                            _as.Seek(0L, System.IO.SeekOrigin.Begin);
                            b = _as.ToArray();
                        }
                    }
                }
            }

            if (b is not null)
            {
                return Assembly.Load(b);
            }
            else
            {
                return null;
            }
        }

        static public void ThreadExceptionHandler(object sender, ThreadExceptionEventArgs e)
        {
            try
            {
                Forms.BugReport.ThrowError(e.Exception);
            }
            catch
            {
                throw e.Exception;
            }
        }

        static private void SecondChanceExceptionHandler(object sender, Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs e)
        {
            try
            {
                e.ExitApplication = false;
                Forms.BugReport.ThrowError(e.Exception);
            }
            catch
            {
                throw e.Exception;
            }
        }

        static private void Domain_UnhandledException(object sender, System.UnhandledExceptionEventArgs e)
        {
            try
            {
#if DEBUG
                if (!Debugger.IsAttached)
                    Forms.BugReport.ThrowError((Exception)e.ExceptionObject, true);

#else
            if (!Debugger.IsAttached)
                throw (Exception)e.ExceptionObject;
#endif
            }
            catch
            {
                throw (Exception)e.ExceptionObject;
            }
        }
        #endregion
    }
}