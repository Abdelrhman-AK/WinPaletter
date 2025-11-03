using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using WinPaletter.Dialogs;
using WinPaletter.Tabs;

namespace WinPaletter
{
    /// <summary>
    /// This class is used to create instances of all forms in the application.
    /// </summary>
    internal partial class Forms
    {
        /// <summary>
        /// List of forms to exclude from the list of forms to create (Useful only for languages creation.)
        /// </summary>
        public static IEnumerable<Type> IExclude = [typeof(SchemeEditor), typeof(GlassWindow), typeof(AspectsTemplate), typeof(TabsForm), typeof(BorderlessForm), typeof(Store_Hover)];

        /// <summary>
        /// List of forms in current project and not in the exclude list.
        /// </summary>
        public static IEnumerable<Type> ITypes => Assembly.GetCallingAssembly().GetTypes().Where(t => !IExclude.Contains(t) && typeof(Form).IsAssignableFrom(t) && !t.IsAbstract && t.GetConstructor(Type.EmptyTypes) != null);

        private static Win12Colors _Win12Colors;
        /// <summary>
        /// Gets the instance of the form <see cref="Win12Colors"/> to manage Windows 12 colors and theme.
        /// </summary>
        public static Win12Colors Win12Colors => (_Win12Colors == null || _Win12Colors.IsDisposed) ? (_Win12Colors = CreateInstance(_Win12Colors)) : _Win12Colors;

        private static Win11Colors _Win11Colors;
        /// <summary>
        /// Gets the instance of the form <see cref="Win11Colors"/> to manage Windows 11 colors and theme.
        /// </summary>
        public static Win11Colors Win11Colors => (_Win11Colors == null || _Win11Colors.IsDisposed) ? (_Win11Colors = CreateInstance(_Win11Colors)) : _Win11Colors;

        private static Win10Colors _Win10Colors;
        /// <summary>
        /// Gets the instance of the form <see cref="Win10Colors"/> to manage Windows 10 colors and theme.
        /// </summary>
        public static Win10Colors Win10Colors => (_Win10Colors == null || _Win10Colors.IsDisposed) ? (_Win10Colors = CreateInstance(_Win10Colors)) : _Win10Colors;

        private static Win81Colors _Win81Colors;
        /// <summary>
        /// Gets the instance of the form <see cref="Win81Colors"/> to manage Windows 8.1 colors and theme.
        /// </summary>
        public static Win81Colors Win81Colors => (_Win81Colors == null || _Win81Colors.IsDisposed) ? (_Win81Colors = CreateInstance(_Win81Colors)) : _Win81Colors;

        private static Win8Colors _Win8Colors;
        /// <summary>
        /// Gets the instance of the form <see cref="Win81Colors"/> to manage Windows 8 colors and theme.
        /// </summary>
        public static Win8Colors Win8Colors => (_Win8Colors == null || _Win8Colors.IsDisposed) ? (_Win8Colors = CreateInstance(_Win8Colors)) : _Win8Colors;

        private static Win7Colors _Win7Colors;
        /// <summary>
        /// Gets the instance of the form <see cref="Win7Colors"/> to manage Windows 7 colors and theme.
        /// </summary>
        public static Win7Colors Win7Colors => (_Win7Colors == null || _Win7Colors.IsDisposed) ? (_Win7Colors = CreateInstance(_Win7Colors)) : _Win7Colors;

        private static WinVistaColors _WinVistaColors;
        /// <summary>
        /// Gets the instance of the form <see cref="WinVistaColors"/> to manage Windows Vista colors and theme.
        /// </summary>
        public static WinVistaColors WinVistaColors => (_WinVistaColors == null || _WinVistaColors.IsDisposed) ? (_WinVistaColors = CreateInstance(_WinVistaColors)) : _WinVistaColors;

        private static WinXPColors _WinXPColors;
        /// <summary>
        /// Gets the instance of the form <see cref="WinXPColors"/> to manage Windows XP theme.
        /// </summary>
        public static WinXPColors WinXPColors => (_WinXPColors == null || _WinXPColors.IsDisposed) ? (_WinXPColors = CreateInstance(_WinXPColors)) : _WinXPColors;

        private static ColorPickerDlg _ColorPickerDlg;
        /// <summary>
        /// Gets the instance of the form <see cref="ColorPickerDlg"/> to pick a color.
        /// </summary>
        public static ColorPickerDlg ColorPickerDlg => (_ColorPickerDlg == null || _ColorPickerDlg.IsDisposed) ? (_ColorPickerDlg = CreateInstance(_ColorPickerDlg)) : _ColorPickerDlg;

        private static Chromify _Chromify;
        /// <summary>
        /// Gets the instance of the form <see cref="ColorPickerDlg"/> to pick a color.
        /// </summary>
        public static Chromify Chromify => (_Chromify == null || _Chromify.IsDisposed) ? (_Chromify = CreateInstance(_Chromify)) : _Chromify;

        private static CursorsStudio _CursorsStudio;
        /// <summary>
        /// Gets the instance of the form <see cref="CursorsStudio"/> to manage Windows cursors.
        /// </summary>
        public static CursorsStudio CursorsStudio => (_CursorsStudio == null || _CursorsStudio.IsDisposed) ? (_CursorsStudio = CreateInstance(_CursorsStudio)) : _CursorsStudio;

        private static About _About;
        /// <summary>
        /// Gets the instance of the form <see cref="About"/> to show the about dialog.
        /// </summary>
        public static About About => (_About == null || _About.IsDisposed) ? (_About = CreateInstance(_About)) : _About;

        private static ServiceInstaller _ServiceInstaller;
        /// <summary>
        /// Gets the instance of the form <see cref="ServiceInstaller"/> to install system events sounds.
        /// </summary>
        public static ServiceInstaller ServiceInstaller => (_ServiceInstaller == null || _ServiceInstaller.IsDisposed) ? (_ServiceInstaller = CreateInstance(_ServiceInstaller)) : _ServiceInstaller;

        private static ThemeLog _ThemeLog;
        /// <summary>
        /// Gets the instance of the form <see cref="ThemeLog"/> to show the theme log.
        /// </summary>
        public static ThemeLog ThemeLog => (_ThemeLog == null || _ThemeLog.IsDisposed) ? (_ThemeLog = CreateInstance(_ThemeLog)) : _ThemeLog;


        private static SOS _SOS;
        /// <summary>
        /// Gets the instance of the form <see cref="SOS"/> to utilize Windows rescue tools.
        /// </summary>
        public static SOS SOS => (_SOS == null || _SOS.IsDisposed) ? (_SOS = CreateInstance(_SOS)) : _SOS;

        private static BugReport _BugReport;
        /// <summary>
        /// Gets the instance of the form <see cref="BugReport"/> to show a bug report.
        /// </summary>
        public static BugReport BugReport => (_BugReport == null || _BugReport.IsDisposed) ? (_BugReport = CreateInstance(_BugReport)) : _BugReport;

        private static UserSwitch _UserSwitch;
        /// <summary>
        /// Gets the instance of the form <see cref="UserSwitch"/> to switch user.
        /// </summary>
        public static UserSwitch UserSwitch => (_UserSwitch == null || _UserSwitch.IsDisposed) ? (_UserSwitch = CreateInstance(_UserSwitch)) : _UserSwitch;


        private static ArgsHelp _ArgsHelp;
        /// <summary>
        /// Gets the instance of the form <see cref="ArgsHelp"/> to show the arguments help.
        /// </summary>
        public static ArgsHelp ArgsHelp => (_ArgsHelp == null || _ArgsHelp.IsDisposed) ? (_ArgsHelp = CreateInstance(_ArgsHelp)) : _ArgsHelp;

        private static Setup _Setup;
        /// <summary>
        /// Gets the instance of the form <see cref="Setup"/> to show the setup dialog.
        /// </summary>
        public static Setup Setup => (_Setup == null || _Setup.IsDisposed) ? (_Setup = CreateInstance(_Setup)) : _Setup;

        private static PE_Warning _PE_Warning;
        /// <summary>
        /// Gets the instance of the form <see cref="PE_Warning"/> to show a warning about portable executable files patching.
        /// </summary>
        public static PE_Warning PE_Warning => (_PE_Warning == null || _PE_Warning.IsDisposed) ? (_PE_Warning = CreateInstance(_PE_Warning)) : _PE_Warning;

        private static WinEffectsAlert _WinEffectsAlert;
        /// <summary>
        /// Gets the instance of the form <see cref="WinEffectsAlert"/> to show a warning about Windows effects.
        /// </summary>
        public static WinEffectsAlert WinEffectsAlert => (_WinEffectsAlert == null || _WinEffectsAlert.IsDisposed) ? (_WinEffectsAlert = CreateInstance(_WinEffectsAlert)) : _WinEffectsAlert;

        private static Saving_ex_list _Saving_ex_list;
        /// <summary>
        /// Gets the instance of the form <see cref="Saving_ex_list"/> to show the theme saving/applying exceptions list.
        /// </summary>
        public static Saving_ex_list Saving_ex_list => (_Saving_ex_list == null || _Saving_ex_list.IsDisposed) ? (_Saving_ex_list = CreateInstance(_Saving_ex_list)) : _Saving_ex_list;

        private static AltTabEditor _AltTabEditor;
        /// <summary>
        /// Gets the instance of the form <see cref="AltTabEditor"/> to edit Alt+Tab window.
        /// </summary>
        public static AltTabEditor AltTabEditor => (_AltTabEditor == null || _AltTabEditor.IsDisposed) ? (_AltTabEditor = CreateInstance(_AltTabEditor)) : _AltTabEditor;

        private static ApplicationThemer _ApplicationThemer;
        /// <summary>
        /// Gets the instance of the form <see cref="ApplicationThemer"/> to manage WinPaletter theme/appearance.
        /// </summary>
        public static ApplicationThemer ApplicationThemer => (_ApplicationThemer == null || _ApplicationThemer.IsDisposed) ? (_ApplicationThemer = CreateInstance(_ApplicationThemer)) : _ApplicationThemer;

        private static ScreenSaver_Editor _ScreenSaver_Editor;
        /// <summary>
        /// Gets the instance of the form <see cref="ScreenSaver_Editor"/> to edit screen saver.
        /// </summary>
        public static ScreenSaver_Editor ScreenSaver_Editor => (_ScreenSaver_Editor == null || _ScreenSaver_Editor.IsDisposed) ? (_ScreenSaver_Editor = CreateInstance(_ScreenSaver_Editor)) : _ScreenSaver_Editor;

        private static ScreenSavers_List _ScreenSavers_List;
        /// <summary>
        /// Gets the instance of the form <see cref="ScreenSavers_List"/> to show screen savers installed in the system.
        /// </summary>
        public static ScreenSavers_List ScreenSavers_List => (_ScreenSavers_List == null || _ScreenSavers_List.IsDisposed) ? (_ScreenSavers_List = CreateInstance(_ScreenSavers_List)) : _ScreenSavers_List;

        private static Sounds_Editor _Sounds_Editor;
        /// <summary>
        /// Gets the instance of the form <see cref="Sounds_Editor"/> to edit system sounds.
        /// </summary>
        public static Sounds_Editor Sounds_Editor => (_Sounds_Editor == null || _Sounds_Editor.IsDisposed) ? (_Sounds_Editor = CreateInstance(_Sounds_Editor)) : _Sounds_Editor;

        private static Wallpaper_Editor _Wallpaper_Editor;
        /// <summary>
        /// Gets the instance of the form <see cref="Wallpaper_Editor"/> to edit wallpaper.
        /// </summary>
        public static Wallpaper_Editor Wallpaper_Editor => (_Wallpaper_Editor == null || _Wallpaper_Editor.IsDisposed) ? (_Wallpaper_Editor = CreateInstance(_Wallpaper_Editor)) : _Wallpaper_Editor;

        private static WinEffecter _WinEffecter;
        /// <summary>
        /// Gets the instance of the form <see cref="WinEffecter"/> to manage Windows effects.
        /// </summary>
        public static WinEffecter WinEffecter => (_WinEffecter == null || _WinEffecter.IsDisposed) ? (_WinEffecter = CreateInstance(_WinEffecter)) : _WinEffecter;

        private static IconsStudio _IconsStudio;
        /// <summary>
        /// Gets the instance of the form <see cref="IconsStudio"/> to manage Windows icons.
        /// </summary>
        public static IconsStudio IconsStudio => (_IconsStudio == null || _IconsStudio.IsDisposed) ? (_IconsStudio = CreateInstance(_IconsStudio)) : _IconsStudio;

        private static Lang_Add_Snippet _Lang_Add_Snippet;
        /// <summary>
        /// Gets the instance of the form <see cref="Lang_Add_Snippet"/> to add a new language snippet.
        /// </summary>
        public static Lang_Add_Snippet Lang_Add_Snippet => (_Lang_Add_Snippet == null || _Lang_Add_Snippet.IsDisposed) ? (_Lang_Add_Snippet = CreateInstance(_Lang_Add_Snippet)) : _Lang_Add_Snippet;

        private static Lang_Editor _Lang_Editor;
        /// <summary>
        /// Gets the instance of the form <see cref="Lang_Editor"/> to edit language JSON file.
        /// </summary>
        public static Lang_Editor Lang_Editor => (_Lang_Editor == null || _Lang_Editor.IsDisposed) ? (_Lang_Editor = CreateInstance(_Lang_Editor)) : _Lang_Editor;

        private static Lang_ReplaceText _Lang_ReplaceText;
        /// <summary>
        /// Gets the instance of the form <see cref="Lang_ReplaceText"/> to replace text in language JSON file.
        /// </summary>
        public static Lang_ReplaceText Lang_ReplaceText => (_Lang_ReplaceText == null || _Lang_ReplaceText.IsDisposed) ? (_Lang_ReplaceText = CreateInstance(_Lang_ReplaceText)) : _Lang_ReplaceText;

        private static LogonUI _LogonUI;
        /// <summary>
        /// Gets the instance of the form <see cref="LogonUI"/> to manage Windows logon screen.
        /// </summary>
        public static LogonUI LogonUI => (_LogonUI == null || _LogonUI.IsDisposed) ? (_LogonUI = CreateInstance(_LogonUI)) : _LogonUI;

        private static LogonUI7 _LogonUI7;
        /// <summary>
        /// Gets the instance of the form <see cref="LogonUI7"/> to manage Windows 7 logon screen.
        /// </summary>
        public static LogonUI7 LogonUI7 => (_LogonUI7 == null || _LogonUI7.IsDisposed) ? (_LogonUI7 = CreateInstance(_LogonUI7)) : _LogonUI7;

        private static LogonUI81 _LogonUI81;
        /// <summary>
        /// Gets the instance of the form <see cref="LogonUI81"/> to manage Windows 8.1 logon screen.
        /// </summary>
        public static LogonUI81 LogonUI81 => (_LogonUI81 == null || _LogonUI81.IsDisposed) ? (_LogonUI81 = CreateInstance(_LogonUI81)) : _LogonUI81;

        private static LogonUIXP _LogonUIXP;
        /// <summary>
        /// Gets the instance of the form <see cref="LogonUIXP"/> to manage Windows XP logon screen.
        /// </summary>
        public static LogonUIXP LogonUIXP => (_LogonUIXP == null || _LogonUIXP.IsDisposed) ? (_LogonUIXP = CreateInstance(_LogonUIXP)) : _LogonUIXP;

        private static Metrics_Fonts _Metrics_Fonts;
        /// <summary>
        /// Gets the instance of the form <see cref="Metrics_Fonts"/> to manage Windows metrics and fonts.
        /// </summary>
        public static Metrics_Fonts Metrics_Fonts => (_Metrics_Fonts == null || _Metrics_Fonts.IsDisposed) ? (_Metrics_Fonts = CreateInstance(_Metrics_Fonts)) : _Metrics_Fonts;

        private static PaletteGenerator _PaletteGenerator;
        /// <summary>
        /// Gets the instance of the form <see cref="PaletteGenerateFromColor"/> to generate a palette from a color.
        /// </summary>
        public static PaletteGenerator PaletteGenerator => (_PaletteGenerator == null || _PaletteGenerator.IsDisposed) ? (_PaletteGenerator = CreateInstance(_PaletteGenerator)) : _PaletteGenerator;

        private static SettingsX _SettingsX;
        /// <summary>
        /// Gets the instance of the form <see cref="SettingsX"/> to manage WinPaletter settings.
        /// </summary>
        public static SettingsX SettingsX => (_SettingsX == null || _SettingsX.IsDisposed) ? (_SettingsX = CreateInstance(_SettingsX)) : _SettingsX;

        private static Uninstall _Uninstall;
        /// <summary>
        /// Gets the instance of the form <see cref="Uninstall"/> to uninstall WinPaletter.
        /// </summary>
        public static Uninstall Uninstall => (_Uninstall == null || _Uninstall.IsDisposed) ? (_Uninstall = CreateInstance(_Uninstall)) : _Uninstall;

        private static Store _Store;
        /// <summary>
        /// Gets the instance of the form <see cref="Store"/> to manage WinPaletter store.
        /// </summary>
        public static Store Store => (_Store == null || _Store.IsDisposed) ? (_Store = CreateInstance(_Store)) : _Store;

        private static Store_CPToggles _Store_CPToggles;
        /// <summary>
        /// Gets the instance of the form <see cref="Store_CPToggles"/> to manage WinPaletter store edited features toggles (secure locks).
        /// </summary>
        public static Store_CPToggles Store_CPToggles => (_Store_CPToggles == null || _Store_CPToggles.IsDisposed) ? (_Store_CPToggles = CreateInstance(_Store_CPToggles)) : _Store_CPToggles;

        private static Store_DownloadProgress _Store_DownloadProgress;
        /// <summary>
        /// Gets the instance of the form <see cref="Store_DownloadProgress"/> to show the store theme download progress.
        /// </summary>
        public static Store_DownloadProgress Store_DownloadProgress => (_Store_DownloadProgress == null || _Store_DownloadProgress.IsDisposed) ? (_Store_DownloadProgress = CreateInstance(_Store_DownloadProgress)) : _Store_DownloadProgress;

        private static Store_Hover _Store_Hover;
        /// <summary>
        /// Gets the instance of the form <see cref="Store_Hover"/> to show the store theme hover.
        /// </summary>
        public static Store_Hover Store_Hover => (_Store_Hover == null || _Store_Hover.IsDisposed) ? (_Store_Hover = CreateInstance(_Store_Hover)) : _Store_Hover;

        private static Store_Intro _Store_Intro;
        /// <summary>
        /// Gets the instance of the form <see cref="Store_Intro"/> to show the store introduction.
        /// </summary>
        public static Store_Intro Store_Intro => (_Store_Intro == null || _Store_Intro.IsDisposed) ? (_Store_Intro = CreateInstance(_Store_Intro)) : _Store_Intro;

        private static Store_SearchFilter _Store_SearchFilter;
        /// <summary>
        /// Gets the instance of the form <see cref="Store_SearchFilter"/> to filter store themes search.
        /// </summary>
        public static Store_SearchFilter Store_SearchFilter => (_Store_SearchFilter == null || _Store_SearchFilter.IsDisposed) ? (_Store_SearchFilter = CreateInstance(_Store_SearchFilter)) : _Store_SearchFilter;

        private static Store_ThemeLicense _Store_ThemeLicense;
        /// <summary>
        /// Gets the instance of the form <see cref="Store_ThemeLicense"/> to show the store theme license.
        /// </summary>
        public static Store_ThemeLicense Store_ThemeLicense => (_Store_ThemeLicense == null || _Store_ThemeLicense.IsDisposed) ? (_Store_ThemeLicense = CreateInstance(_Store_ThemeLicense)) : _Store_ThemeLicense;

        private static CMD _CMD;
        /// <summary>
        /// Gets the instance of the form <see cref="CMD"/> to edit Windows Command Prompt and PowerShell appearance.
        /// </summary>
        public static CMD CMD => (_CMD == null || _CMD.IsDisposed) ? (_CMD = CreateInstance(_CMD)) : _CMD;

        private static ExternalTerminal _ExternalTerminal;
        /// <summary>
        /// Gets the instance of the form <see cref="ExternalTerminal"/> to manage external terminal appearance.
        /// </summary>
        public static ExternalTerminal ExternalTerminal => (_ExternalTerminal == null || _ExternalTerminal.IsDisposed) ? (_ExternalTerminal = CreateInstance(_ExternalTerminal)) : _ExternalTerminal;

        private static NewExtTerminal _NewExtTerminal;
        /// <summary>
        /// Gets the instance of the form <see cref="NewExtTerminal"/> to manage new external terminal appearance.
        /// </summary>
        public static NewExtTerminal NewExtTerminal => (_NewExtTerminal == null || _NewExtTerminal.IsDisposed) ? (_NewExtTerminal = CreateInstance(_NewExtTerminal)) : _NewExtTerminal;

        private static TerminalInfo _TerminalInfo;
        /// <summary>
        /// Gets the instance of the form <see cref="TerminalInfo"/> to show terminal information.
        /// </summary>
        public static TerminalInfo TerminalInfo => (_TerminalInfo == null || _TerminalInfo.IsDisposed) ? (_TerminalInfo = CreateInstance(_TerminalInfo)) : _TerminalInfo;

        private static TerminalsDashboard _TerminalsDashboard;
        /// <summary>
        /// Gets the instance of the form <see cref="TerminalsDashboard"/> to manage terminals.
        /// </summary>
        public static TerminalsDashboard TerminalsDashboard => (_TerminalsDashboard == null || _TerminalsDashboard.IsDisposed) ? (_TerminalsDashboard = CreateInstance(_TerminalsDashboard)) : _TerminalsDashboard;

        private static WindowsTerminal _WindowsTerminal;
        /// <summary>
        /// Gets the instance of the form <see cref="WindowsTerminal"/> to manage Windows Terminal appearance.
        /// </summary>
        public static WindowsTerminal WindowsTerminal => (_WindowsTerminal == null || _WindowsTerminal.IsDisposed) ? (_WindowsTerminal = CreateInstance(_WindowsTerminal)) : _WindowsTerminal;

        private static WindowsTerminalCopycat _WindowsTerminalCopycat;
        /// <summary>
        /// Gets the instance of the form <see cref="WindowsTerminalCopycat"/> to copycat a Windows Terminal appearance.
        /// </summary>
        public static WindowsTerminalCopycat WindowsTerminalCopycat => (_WindowsTerminalCopycat == null || _WindowsTerminalCopycat.IsDisposed) ? (_WindowsTerminalCopycat = CreateInstance(_WindowsTerminalCopycat)) : _WindowsTerminalCopycat;

        private static WindowsTerminalDecide _WindowsTerminalDecide;
        /// <summary>
        /// Gets the instance of the form <see cref="WindowsTerminalDecide"/> to decide Windows Terminal branch from which WinPaletter should get data.
        /// </summary>
        public static WindowsTerminalDecide WindowsTerminalDecide => (_WindowsTerminalDecide == null || _WindowsTerminalDecide.IsDisposed) ? (_WindowsTerminalDecide = CreateInstance(_WindowsTerminalDecide)) : _WindowsTerminalDecide;

        private static Updates _Updates;
        /// <summary>
        /// Gets the instance of the form <see cref="Updates"/> to manage WinPaletter updates.
        /// </summary>
        public static Updates Updates => (_Updates == null || _Updates.IsDisposed) ? (_Updates = CreateInstance(_Updates)) : _Updates;

        private static Welcome _Welcome;
        /// <summary>
        /// Gets the instance of the form <see cref="Welcome"/> to show the welcome dialog.
        /// </summary>
        public static Welcome Welcome => (_Welcome == null || _Welcome.IsDisposed) ? (_Welcome = CreateInstance(_Welcome)) : _Welcome;

        private static Win32UI_Fullscreen _Win32UI_Fullscreen;
        /// <summary>
        /// Gets the instance of the form <see cref="Win32UI_Fullscreen"/> to manage Windows 32-bit UI in fullscreen.
        /// </summary>
        public static Win32UI_Fullscreen Win32UI_Fullscreen => (_Win32UI_Fullscreen == null || _Win32UI_Fullscreen.IsDisposed) ? (_Win32UI_Fullscreen = CreateInstance(_Win32UI_Fullscreen)) : _Win32UI_Fullscreen;

        private static Win32UI_Gallery _Win32UI_Gallery;
        /// <summary>
        /// Gets the instance of the form <see cref="Win32UI_Gallery"/> to show Windows 32-bit UI gallery (Schemes pictures).
        /// </summary>
        public static Win32UI_Gallery Win32UI_Gallery => (_Win32UI_Gallery == null || _Win32UI_Gallery.IsDisposed) ? (_Win32UI_Gallery = CreateInstance(_Win32UI_Gallery)) : _Win32UI_Gallery;

        private static Win32UI _Win32UI;
        /// <summary>
        /// Gets the instance of the form <see cref="Win32UI"/> to manage Windows 32-bit UI.
        /// </summary>
        public static Win32UI Win32UI => (_Win32UI == null || _Win32UI.IsDisposed) ? (_Win32UI = CreateInstance(_Win32UI)) : _Win32UI;

        private static AccessibilityEditor _AccessibilityEditor;
        /// <summary>
        /// Gets the instance of the form <see cref="AccessibilityEditor"/> to edit Windows accessibility settings.
        /// </summary>
        public static AccessibilityEditor AccessibilityEditor => (_AccessibilityEditor == null || _AccessibilityEditor.IsDisposed) ? (_AccessibilityEditor = CreateInstance(_AccessibilityEditor)) : _AccessibilityEditor;

        private static GlassWindow _glassWindow;
        /// <summary>
        /// Gets the instance of the form <see cref="GlassWindow"/> to show a DWM window with effect (Acrylic/Aero) to appear as a glass background.
        /// </summary>
        public static GlassWindow GlassWindow => (_glassWindow == null || _glassWindow.IsDisposed) ? (_glassWindow = CreateInstance(_glassWindow)) : _glassWindow;

        private static EditInfo _EditInfo;
        /// <summary>
        /// Gets the instance of the form <see cref="EditInfo"/> to edit theme information.
        /// </summary>
        public static EditInfo EditInfo => (_EditInfo == null || _EditInfo.IsDisposed) ? (_EditInfo = CreateInstance(_EditInfo)) : _EditInfo;

        private static MainForm _MainForm;
        /// <summary>
        /// Gets the instance of the form <see cref="MainForm"/> to manage the main form that hosts tabs and pages.
        /// </summary>
        public static MainForm MainForm => (_MainForm == null || _MainForm.IsDisposed) ? (_MainForm = CreateInstance(_MainForm)) : _MainForm;

        private static Home _Home;
        /// <summary>
        /// Gets the instance of the form <see cref="Home"/> to manage the home form that has cards for WinPaletter features and toolbar buttons.
        /// </summary>
        public static Home Home => (_Home == null || _Home.IsDisposed) ? (_Home = CreateInstance(_Home)) : _Home;

        private static BackupThemes_List _BackupThemes_List;
        /// <summary>
        /// Gets the instance of the form <see cref="BackupThemes_List"/> to manage the list of backup themes.
        /// </summary>
        public static BackupThemes_List BackupThemes_List => (_BackupThemes_List == null || _BackupThemes_List.IsDisposed) ? (_BackupThemes_List = CreateInstance(_BackupThemes_List)) : _BackupThemes_List;

        private static OS_Dashboard _OS_Dashboard;
        /// <summary>
        /// Gets the instance of the form <see cref="OS_Dashboard"/> to manage the OS dashboard.
        /// </summary>
        public static OS_Dashboard OS_Dashboard => (_OS_Dashboard == null || _OS_Dashboard.IsDisposed) ? (_OS_Dashboard = CreateInstance(_OS_Dashboard)) : _OS_Dashboard;

        #region Engine

        private static Dictionary<Type, Form> formBeingCreated = new();

        /// <summary>
        /// Creates a new instance of the specified form type or returns the existing instance if it is already created and not disposed.
        /// </summary>
        private static T CreateInstance<T>(T instance) where T : Form, new()
        {
            if (instance != null && !instance.IsDisposed) return instance;

            lock (formBeingCreated)
            {
                Type formType = typeof(T);

                // If the form is already being created, return the ongoing instance or null
                if (formBeingCreated.TryGetValue(formType, out Form existing) && existing != null && !existing.IsDisposed) return existing as T;

                try
                {
                    T newInstance = new();
                    formBeingCreated[formType] = newInstance;
                    return newInstance;
                }
                catch (TargetInvocationException ex) when (ex.InnerException != null)
                {
                    string betterMessage = $"Error creating form {typeof(T).Name}: {ex.InnerException.Message}";
                    throw new InvalidOperationException(betterMessage, ex.InnerException);
                }
                finally
                {
                    // Clean up dictionary after creation
                    formBeingCreated.Remove(formType);
                }
            }
        }

        #endregion
    }
}