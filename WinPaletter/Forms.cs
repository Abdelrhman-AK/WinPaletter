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
        public static IReadOnlyCollection<Type> IExclude { get; } =
            new HashSet<Type>
            {
                typeof(SchemeEditor),
                typeof(GlassWindow),
                typeof(AspectsTemplate),
                typeof(TabsForm),
                typeof(BorderlessForm),
                typeof(Store_Hover)
            };

        /// <summary>
        /// List of forms in current project and not in the exclude list.
        /// </summary>
        public static IEnumerable<Type> ITypes => Assembly.GetCallingAssembly().GetTypes().Where(t => !IExclude.Contains(t) && typeof(Form).IsAssignableFrom(t) && !t.IsAbstract && t.GetConstructor(Type.EmptyTypes) != null);

        /// <summary>
        /// Gets the instance of the form <see cref="Win12Colors"/> to manage Wiadows 12 colors and theme.
        /// </summary>
        public static Win12Colors Win12Colors => Get(ref _Win12Colors);
        private static Win12Colors _Win12Colors;

        /// <summary>
        /// Gets the instance of the form <see cref="Win11Colors"/> to manage Windows 11 colors and theme.
        /// </summary>
        public static Win11Colors Win11Colors => Get(ref _Win11Colors);
        private static Win11Colors _Win11Colors;

        /// <summary>
        /// Gets the instance of the form <see cref="Win10Colors"/> to manage Windows 10 colors and theme.
        /// </summary>
        public static Win10Colors Win10Colors => Get(ref _Win10Colors);
        private static Win10Colors _Win10Colors;

        /// <summary>
        /// Gets the instance of the form <see cref="Win81Colors"/> to manage Windows 8.1 colors and theme.
        /// </summary>
        public static Win81Colors Win81Colors => Get(ref _Win81Colors);
        private static Win81Colors _Win81Colors;

        /// <summary>
        /// Gets the instance of the form <see cref="Win81Colors"/> to manage Windows 8 colors and theme.
        /// </summary>
        public static Win8Colors Win8Colors => Get(ref _Win8Colors);
        private static Win8Colors _Win8Colors;

        /// <summary>
        /// Gets the instance of the form <see cref="Win7Colors"/> to manage Windows 7 colors and theme.
        /// </summary>
        public static Win7Colors Win7Colors => Get(ref _Win7Colors);
        private static Win7Colors _Win7Colors;

        /// <summary>
        /// Gets the instance of the form <see cref="WinVistaColors"/> to manage Windows Vista colors and theme.
        /// </summary>
        public static WinVistaColors WinVistaColors => Get(ref _WinVistaColors);
        private static WinVistaColors _WinVistaColors;

        /// <summary>
        /// Gets the instance of the form <see cref="WinXPColors"/> to manage Windows XP theme.
        /// </summary>
        public static WinXPColors WinXPColors => Get(ref _WinXPColors);
        private static WinXPColors _WinXPColors;

        /// <summary>
        /// Gets the instance of the form <see cref="ColorPickerDlg"/> to pick a color.
        /// </summary>
        public static ColorPickerDlg ColorPickerDlg => Get(ref _ColorPickerDlg);
        private static ColorPickerDlg _ColorPickerDlg;

        /// <summary>
        /// Gets the instance of the form <see cref="WallStudio"/> to manage wallpapers and backgrounds.
        /// </summary>
        public static WallStudio WallStudio => Get(ref _WallStudio);
        private static WallStudio _WallStudio;

        /// <summary>
        /// Gets the instance of the form <see cref="CursorsStudio"/> to manage Windows cursors.
        /// </summary>
        public static CursorsStudio CursorsStudio => Get(ref _CursorsStudio);
        private static CursorsStudio _CursorsStudio;

        /// <summary>
        /// Gets the instance of the form <see cref="About"/> to show the about dialog.
        /// </summary>
        public static About About => Get(ref _About);
        private static About _About;

        /// <summary>
        /// Gets the instance of the form <see cref="ServiceInstaller"/> to install system events sounds.
        /// </summary>
        public static ServiceInstaller ServiceInstaller => Get(ref _ServiceInstaller);
        private static ServiceInstaller _ServiceInstaller;

        /// <summary>
        /// Gets the instance of the form <see cref="ThemeLog"/> to show the theme log.
        /// </summary>
        public static ThemeLog ThemeLog => Get(ref _ThemeLog);
        private static ThemeLog _ThemeLog;

        /// <summary>
        /// Gets the instance of the form <see cref="SOS"/> to utilize Windows rescue tools.
        /// </summary>
        public static SOS SOS => Get(ref _SOS);
        private static SOS _SOS;

        /// <summary>
        /// Gets the instance of the form <see cref="BugReport"/> to show a bug report.
        /// </summary>
        public static BugReport BugReport => Get(ref _BugReport);
        private static BugReport _BugReport;

        /// <summary>
        /// Gets the instance of the form <see cref="UserSwitch"/> to switch user.
        /// </summary>
        public static UserSwitch UserSwitch => Get(ref _UserSwitch);
        private static UserSwitch _UserSwitch;

        /// <summary>
        /// Gets the instance of the form <see cref="GitHubLogin"/> to sign in to GitHub.
        /// </summary>
        public static GitHubLogin GitHubLogin => Get(ref _GitHubLogin);
        private static GitHubLogin _GitHubLogin;

        /// <summary>
        /// Gets the instance of the form <see cref="GitHub_FileAction"/> to perform actions on a GitHub file.
        /// </summary>
        public static GitHub_FileAction GitHub_FileAction => Get(ref _GitHub_FileAction);
        private static GitHub_FileAction _GitHub_FileAction;

        /// <summary>
        /// Gets the instance of the form <see cref="GitHub_FileConflict"/> to resolve GitHub file conflicts.
        /// </summary>
        public static GitHub_FileConflict GitHub_FileConflict => Get(ref _GitHub_FileConflict);
        private static GitHub_FileConflict _GitHub_FileConflict;

        /// <summary>
        /// Gets the instance of the form <see cref="GitHub_FilesCompare"/> to compare GitHub files.
        /// </summary>
        public static GitHub_FilesCompare GitHub_FilesCompare => Get(ref _GitHub_FilesCompare);
        private static GitHub_FilesCompare _GitHub_FilesCompare;

        /// <summary>
        /// Gets the instance of the form <see cref="GitHubMgrForm"/> to manage GitHub repositories and files.
        /// </summary>
        public static GitHubMgrForm GitHubMgrForm => Get(ref _GitHubMgrForm);
        private static GitHubMgrForm _GitHubMgrForm;

        /// <summary>
        /// Gets the instance of the form <see cref="ArgsHelp"/> to show the arguments help.
        /// </summary>
        public static ArgsHelp ArgsHelp => Get(ref _ArgsHelp);
        private static ArgsHelp _ArgsHelp;

        /// <summary>
        /// Gets the instance of the form <see cref="Setup"/> to show the setup dialog.
        /// </summary>
        public static Setup Setup => Get(ref _Setup);
        private static Setup _Setup;

        /// <summary>
        /// Gets the instance of the form <see cref="PE_Warning"/> to show a warning about portable executable files patching.
        /// </summary>
        public static PE_Warning PE_Warning => Get(ref _PE_Warning);
        private static PE_Warning _PE_Warning;

        /// <summary>
        /// Gets the instance of the form <see cref="WinEffectsAlert"/> to show a warning about Windows effects.
        /// </summary>
        public static WinEffectsAlert WinEffectsAlert => Get(ref _WinEffectsAlert);
        private static WinEffectsAlert _WinEffectsAlert;

        /// <summary>
        /// Gets the instance of the form <see cref="Saving_ex_list"/> to show the theme saving/applying exceptions list.
        /// </summary>
        public static Saving_ex_list Saving_ex_list => Get(ref _Saving_ex_list);
        private static Saving_ex_list _Saving_ex_list;

        /// <summary>
        /// Gets the instance of the form <see cref="AltTabEditor"/> to edit Alt+Tab window.
        /// </summary>
        public static AltTabEditor AltTabEditor => Get(ref _AltTabEditor);
        private static AltTabEditor _AltTabEditor;

        /// <summary>
        /// Gets the instance of the form <see cref="ApplicationThemer"/> to manage WinPaletter theme/appearance.
        /// </summary>
        public static ApplicationThemer ApplicationThemer => Get(ref _ApplicationThemer);
        private static ApplicationThemer _ApplicationThemer;

        /// <summary>
        /// Gets the instance of the form <see cref="ScreenSaver_Editor"/> to edit screen saver.
        /// </summary>
        public static ScreenSaver_Editor ScreenSaver_Editor => Get(ref _ScreenSaver_Editor);
        private static ScreenSaver_Editor _ScreenSaver_Editor;

        /// <summary>
        /// Gets the instance of the form <see cref="ScreenSavers_List"/> to show screen savers installed in the system.
        /// </summary>
        public static ScreenSavers_List ScreenSavers_List => Get(ref _ScreenSavers_List);
        private static ScreenSavers_List _ScreenSavers_List;

        /// <summary>
        /// Gets the instance of the form <see cref="Sounds_Editor"/> to edit system sounds.
        /// </summary>
        public static Sounds_Editor Sounds_Editor => Get(ref _Sounds_Editor);
        private static Sounds_Editor _Sounds_Editor;

        /// <summary>
        /// Gets the instance of the form <see cref="Wallpaper_Editor"/> to edit wallpaper.
        /// </summary>
        public static Wallpaper_Editor Wallpaper_Editor => Get(ref _Wallpaper_Editor);
        private static Wallpaper_Editor _Wallpaper_Editor;

        /// <summary>
        /// Gets the instance of the form <see cref="WinEffecter"/> to manage Windows effects.
        /// </summary>
        public static WinEffecter WinEffecter => Get(ref _WinEffecter);
        private static WinEffecter _WinEffecter;

        /// <summary>
        /// Gets the instance of the form <see cref="IconsStudio"/> to manage Windows icons.
        /// </summary>
        public static IconsStudio IconsStudio => Get(ref _IconsStudio);
        private static IconsStudio _IconsStudio;

        /// <summary>
        /// Gets the instance of the form <see cref="Lang_Add_Snippet"/> to add a new language snippet.
        /// </summary>
        public static Lang_Add_Snippet Lang_Add_Snippet => Get(ref _Lang_Add_Snippet);
        private static Lang_Add_Snippet _Lang_Add_Snippet;

        /// <summary>
        /// Gets the instance of the form <see cref="Lang_Editor"/> to edit language JSON file.
        /// </summary>
        public static Lang_Editor Lang_Editor => Get(ref _Lang_Editor);
        private static Lang_Editor _Lang_Editor;

        /// <summary>
        /// Gets the instance of the form <see cref="Lang_ReplaceText"/> to replace text in language JSON file.
        /// </summary>
        public static Lang_ReplaceText Lang_ReplaceText => Get(ref _Lang_ReplaceText);
        private static Lang_ReplaceText _Lang_ReplaceText;

        /// <summary>
        /// Gets the instance of the form <see cref="LogonUI"/> to manage Windows logon screen.
        /// </summary>
        public static LogonUI LogonUI => Get(ref _LogonUI);
        private static LogonUI _LogonUI;

        /// <summary>
        /// Gets the instance of the form <see cref="LogonUI7"/> to manage Windows 7 logon screen.
        /// </summary>
        public static LogonUI7 LogonUI7 => Get(ref _LogonUI7);
        private static LogonUI7 _LogonUI7;

        /// <summary>
        /// Gets the instance of the form <see cref="LogonUI81"/> to manage Windows 8.1 logon screen.
        /// </summary>
        public static LogonUI81 LogonUI81 => Get(ref _LogonUI81);
        private static LogonUI81 _LogonUI81;

        /// <summary>
        /// Gets the instance of the form <see cref="LogonUIXP"/> to manage Windows XP logon screen.
        /// </summary>
        public static LogonUIXP LogonUIXP => Get(ref _LogonUIXP);
        private static LogonUIXP _LogonUIXP;

        /// <summary>
        /// Gets the instance of the form <see cref="Metrics_Fonts"/> to manage Windows metrics and fonts.
        /// </summary>
        public static Metrics_Fonts Metrics_Fonts => Get(ref _Metrics_Fonts);
        private static Metrics_Fonts _Metrics_Fonts;

        /// <summary>
        /// Gets the instance of the form <see cref="PaletteGenerateFromColor"/> to generate a palette from a color.
        /// </summary>
        public static PaletteGenerator PaletteGenerator => Get(ref _PaletteGenerator);
        private static PaletteGenerator _PaletteGenerator;

        /// <summary>
        /// Gets the instance of the form <see cref="SettingsX"/> to manage WinPaletter settings.
        /// </summary>
        public static SettingsX SettingsX => Get(ref _SettingsX);
        private static SettingsX _SettingsX;

        /// <summary>
        /// Gets the instance of the form <see cref="Uninstall"/> to uninstall WinPaletter.
        /// </summary>
        public static Uninstall Uninstall => Get(ref _Uninstall);
        private static Uninstall _Uninstall;

        /// <summary>
        /// Gets the instance of the form <see cref="Store"/> to manage WinPaletter store.
        /// </summary>
        public static Store Store => Get(ref _Store);
        private static Store _Store;

        /// <summary>
        /// Gets the instance of the form <see cref="DownloadManager_Dlg"/> to show the download manager.
        /// </summary>
        public static DownloadManager_Dlg DownloadManager_Dlg => Get(ref _DownloadManager_Dlg);
        private static DownloadManager_Dlg _DownloadManager_Dlg;

        /// <summary>
        /// Gets the instance of the form <see cref="Store_Hover"/> to show the store theme hover.
        /// </summary>
        public static Store_Hover Store_Hover => Get(ref _Store_Hover);
        private static Store_Hover _Store_Hover;

        /// <summary>
        /// Gets the instance of the form <see cref="Store_Intro_New"/> to show the store introduction.
        /// </summary>
        public static Store_Intro_New Store_Intro_New => Get(ref _Store_Intro);
        private static Store_Intro_New _Store_Intro;

        /// <summary>
        /// Gets the instance of the form <see cref="Store_SearchFilter"/> to filter store themes search.
        /// </summary>
        public static Store_SearchFilter Store_SearchFilter => Get(ref _Store_SearchFilter);
        private static Store_SearchFilter _Store_SearchFilter;

        /// <summary>
        /// Gets the instance of the form <see cref="Store_ThemeLicense"/> to show the store theme license.
        /// </summary>
        public static Store_ThemeLicense Store_ThemeLicense => Get(ref _Store_ThemeLicense);
        private static Store_ThemeLicense _Store_ThemeLicense;

        /// <summary>
        /// Gets the instance of the form <see cref="CMD"/> to edit Windows Command Prompt and PowerShell appearance.
        /// </summary>
        public static CMD CMD => Get(ref _CMD);
        private static CMD _CMD;

        /// <summary>
        /// Gets the instance of the form <see cref="ExternalTerminal"/> to manage external terminal appearance.
        /// </summary>
        public static ExternalTerminal ExternalTerminal => Get(ref _ExternalTerminal);
        private static ExternalTerminal _ExternalTerminal;

        /// <summary>
        /// Gets the instance of the form <see cref="NewExtTerminal"/> to manage new external terminal appearance.
        /// </summary>
        public static NewExtTerminal NewExtTerminal => Get(ref _NewExtTerminal);
        private static NewExtTerminal _NewExtTerminal;

        /// <summary>
        /// Gets the instance of the form <see cref="TerminalInfo"/> to show terminal information.
        /// </summary>
        public static TerminalInfo TerminalInfo => Get(ref _TerminalInfo);
        private static TerminalInfo _TerminalInfo;

        /// <summary>
        /// Gets the instance of the form <see cref="TerminalsDashboard"/> to manage terminals.
        /// </summary>
        public static TerminalsDashboard TerminalsDashboard => Get(ref _TerminalsDashboard);
        private static TerminalsDashboard _TerminalsDashboard;

        /// <summary>
        /// Gets the instance of the form <see cref="WindowsTerminal"/> to manage Windows Terminal appearance.
        /// </summary>
        public static WindowsTerminal WindowsTerminal => Get(ref _WindowsTerminal);
        private static WindowsTerminal _WindowsTerminal;

        /// <summary>
        /// Gets the instance of the form <see cref="WindowsTerminalCopycat"/> to copycat a Windows Terminal appearance.
        /// </summary>
        public static WindowsTerminalCopycat WindowsTerminalCopycat => Get(ref _WindowsTerminalCopycat);
        private static WindowsTerminalCopycat _WindowsTerminalCopycat;

        /// <summary>
        /// Gets the instance of the form <see cref="WindowsTerminalDecide"/> to decide Windows Terminal branch from which WinPaletter should get data.
        /// </summary>
        public static WindowsTerminalDecide WindowsTerminalDecide => Get(ref _WindowsTerminalDecide);
        private static WindowsTerminalDecide _WindowsTerminalDecide;

        /// <summary>
        /// Gets the instance of the form <see cref="Updates"/> to manage WinPaletter updates.
        /// </summary>
        public static Updates Updates => Get(ref _Updates);
        private static Updates _Updates;

        /// <summary>
        /// Gets the instance of the form <see cref="Welcome"/> to show the welcome dialog.
        /// </summary>
        public static Welcome Welcome => Get(ref _Welcome);
        private static Welcome _Welcome;

        /// <summary>
        /// Gets the instance of the form <see cref="Win32UI_Fullscreen"/> to manage Windows 32-bit UI in fullscreen.
        /// </summary>
        public static Win32UI_Fullscreen Win32UI_Fullscreen => Get(ref _Win32UI_Fullscreen);
        private static Win32UI_Fullscreen _Win32UI_Fullscreen;

        /// <summary>
        /// Gets the instance of the form <see cref="Win32UI_Gallery"/> to show Windows 32-bit UI gallery (Schemes pictures).
        /// </summary>
        public static Win32UI_Gallery Win32UI_Gallery => Get(ref _Win32UI_Gallery);
        private static Win32UI_Gallery _Win32UI_Gallery;

        /// <summary>
        /// Gets the instance of the form <see cref="Win32UI"/> to manage Windows 32-bit UI.
        /// </summary>
        public static Win32UI Win32UI => Get(ref _Win32UI);
        private static Win32UI _Win32UI;

        /// <summary>
        /// Gets the instance of the form <see cref="AccessibilityEditor"/> to edit Windows accessibility settings.
        /// </summary>
        public static AccessibilityEditor AccessibilityEditor => Get(ref _AccessibilityEditor);
        private static AccessibilityEditor _AccessibilityEditor;

        /// <summary>
        /// Gets the instance of the form <see cref="GlassWindow"/> to show a DWM window with effect (Acrylic/Aero) to appear as a glass background.
        /// </summary>
        public static GlassWindow GlassWindow => Get(ref _glassWindow);
        private static GlassWindow _glassWindow;

        /// <summary>
        /// Gets the instance of the form <see cref="EditInfo"/> to edit theme information.
        /// </summary>
        public static EditInfo EditInfo => Get(ref _EditInfo);
        private static EditInfo _EditInfo;

        /// <summary>
        /// Gets the instance of the form <see cref="MainForm"/> to manage the main form that hosts tabs and pages.
        /// </summary>
        public static MainForm MainForm => Get(ref _MainForm);
        private static MainForm _MainForm;

        /// <summary>
        /// Gets the instance of the form <see cref="Home"/> to manage the home form that has cards for WinPaletter features and toolbar buttons.
        /// </summary>
        public static Home Home => Get(ref _Home);
        private static Home _Home;

        /// <summary>
        /// Gets the instance of the form <see cref="BackupThemes_List"/> to manage the list of backup themes.
        /// </summary>
        public static BackupThemes_List BackupThemes_List => Get(ref _BackupThemes_List);
        private static BackupThemes_List _BackupThemes_List;

        /// <summary>
        /// Gets the instance of the form <see cref="OS_Dashboard"/> to manage the OS dashboard.
        /// </summary>
        public static OS_Dashboard OS_Dashboard => Get(ref _OS_Dashboard);
        private static OS_Dashboard _OS_Dashboard;

        #region Engine

        private static readonly Dictionary<Type, Form> formBeingCreated = new();
        private static readonly object formBeingCreatedLock = new();

        private static T Get<T>(ref T field) where T : Form, new()
        {
            return field != null && !field.IsDisposed ? field : CreateInstance(ref field);
        }

        /// <summary>
        /// Creates a new instance of the specified form type or returns the existing instance if it is already created and not disposed.
        /// </summary>
        private static T CreateInstance<T>(ref T instance) where T : Form, new()
        {
            if (instance != null && !instance.IsDisposed)
                return instance;

            Type formType = typeof(T);

            lock (formBeingCreatedLock)
            {
                if (instance != null && !instance.IsDisposed)
                    return instance;

                if (formBeingCreated.TryGetValue(formType, out Form existing) &&
                    existing is T existingTyped &&
                    !existingTyped.IsDisposed)
                {
                    instance = existingTyped;
                    return existingTyped;
                }

                try
                {
                    T newInstance = new();
                    formBeingCreated[formType] = newInstance;
                    instance = newInstance;
                    return newInstance;
                }
                catch (TargetInvocationException ex) when (ex.InnerException != null)
                {
                    formBeingCreated.Remove(formType);

                    throw new InvalidOperationException(
                        $"Error creating form {formType.Name}: {ex.InnerException.Message}",
                        ex.InnerException);
                }
                catch
                {
                    formBeingCreated.Remove(formType);
                    throw;
                }
            }
        }

        #endregion
    }
}