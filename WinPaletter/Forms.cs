using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WinPaletter.Dialogs;

namespace WinPaletter
{
    internal partial class Forms
    {
        private static Win12Colors _Win12Colors;
        public static Win12Colors Win12Colors => (_Win12Colors == null || _Win12Colors.IsDisposed) ? (_Win12Colors = CreateInstance(_Win12Colors)) : _Win12Colors;

        private static Win11Colors _Win11Colors;
        public static Win11Colors Win11Colors => (_Win11Colors == null || _Win11Colors.IsDisposed) ? (_Win11Colors = CreateInstance(_Win11Colors)) : _Win11Colors;

        private static Win10Colors _Win10Colors;
        public static Win10Colors Win10Colors => (_Win10Colors == null || _Win10Colors.IsDisposed) ? (_Win10Colors = CreateInstance(_Win10Colors)) : _Win10Colors;

        private static Win81Colors _Win81Colors;
        public static Win81Colors Win81Colors => (_Win81Colors == null || _Win81Colors.IsDisposed) ? (_Win81Colors = CreateInstance(_Win81Colors)) : _Win81Colors;

        private static Win7Colors _Win7Colors;
        public static Win7Colors Win7Colors => (_Win7Colors == null || _Win7Colors.IsDisposed) ? (_Win7Colors = CreateInstance(_Win7Colors)) : _Win7Colors;

        private static WinVistaColors _WinVistaColors;
        public static WinVistaColors WinVistaColors => (_WinVistaColors == null || _WinVistaColors.IsDisposed) ? (_WinVistaColors = CreateInstance(_WinVistaColors)) : _WinVistaColors;

        private static WinXPColors _WinXPColors;
        public static WinXPColors WinXPColors => (_WinXPColors == null || _WinXPColors.IsDisposed) ? (_WinXPColors = CreateInstance(_WinXPColors)) : _WinXPColors;

        private static VisualStyles _VisualStyles;
        public static VisualStyles VisualStyles => (_VisualStyles == null || _VisualStyles.IsDisposed) ? (_VisualStyles = CreateInstance(_VisualStyles)) : _VisualStyles;

        private static ColorPickerDlg _ColorPickerDlg;
        public static ColorPickerDlg ColorPickerDlg => (_ColorPickerDlg == null || _ColorPickerDlg.IsDisposed) ? (_ColorPickerDlg = CreateInstance(_ColorPickerDlg)) : _ColorPickerDlg;

        private static SubMenu _SubMenu;
        public static SubMenu SubMenu => (_SubMenu == null || _SubMenu.IsDisposed) ? (_SubMenu = CreateInstance(_SubMenu)) : _SubMenu;

        private static CursorsStudio _CursorsStudio;
        public static CursorsStudio CursorsStudio => (_CursorsStudio == null || _CursorsStudio.IsDisposed) ? (_CursorsStudio = CreateInstance(_CursorsStudio)) : _CursorsStudio;

        private static About _About;
        public static About About => (_About == null || _About.IsDisposed) ? (_About = CreateInstance(_About)) : _About;

        private static SysEventsSndsInstaller _SysEventsSndsInstaller;
        public static SysEventsSndsInstaller SysEventsSndsInstaller => (_SysEventsSndsInstaller == null || _SysEventsSndsInstaller.IsDisposed) ? (_SysEventsSndsInstaller = CreateInstance(_SysEventsSndsInstaller)) : _SysEventsSndsInstaller;

        private static ThemeLog _ThemeLog;
        public static ThemeLog ThemeLog => (_ThemeLog == null || _ThemeLog.IsDisposed) ? (_ThemeLog = CreateInstance(_ThemeLog)) : _ThemeLog;

        private static RescueTools _RescueTools;
        public static RescueTools RescueTools => (_RescueTools == null || _RescueTools.IsDisposed) ? (_RescueTools = CreateInstance(_RescueTools)) : _RescueTools;

        private static BugReport _BugReport;
        public static BugReport BugReport => (_BugReport == null || _BugReport.IsDisposed) ? (_BugReport = CreateInstance(_BugReport)) : _BugReport;

        private static UserSwitch _UserSwitch;
        public static UserSwitch UserSwitch => (_UserSwitch == null || _UserSwitch.IsDisposed) ? (_UserSwitch = CreateInstance(_UserSwitch)) : _UserSwitch;

        private static UserPassword _UserPassword;
        public static UserPassword UserPassword => (_UserPassword == null || _UserPassword.IsDisposed) ? (_UserPassword = CreateInstance(_UserPassword)) : _UserPassword;

        private static ArgsHelp _ArgsHelp;
        public static ArgsHelp ArgsHelp => (_ArgsHelp == null || _ArgsHelp.IsDisposed) ? (_ArgsHelp = CreateInstance(_ArgsHelp)) : _ArgsHelp;

        private static LicenseForm _LicenseForm;
        public static LicenseForm LicenseForm => (_LicenseForm == null || _LicenseForm.IsDisposed) ? (_LicenseForm = CreateInstance(_LicenseForm)) : _LicenseForm;

        private static PE_Warning _PE_Warning;
        public static PE_Warning PE_Warning => (_PE_Warning == null || _PE_Warning.IsDisposed) ? (_PE_Warning = CreateInstance(_PE_Warning)) : _PE_Warning;

        private static WinEffectsAlert _WinEffectsAlert;
        public static WinEffectsAlert WinEffectsAlert => (_WinEffectsAlert == null || _WinEffectsAlert.IsDisposed) ? (_WinEffectsAlert = CreateInstance(_WinEffectsAlert)) : _WinEffectsAlert;

        private static Saving_ex_list _Saving_ex_list;
        public static Saving_ex_list Saving_ex_list => (_Saving_ex_list == null || _Saving_ex_list.IsDisposed) ? (_Saving_ex_list = CreateInstance(_Saving_ex_list)) : _Saving_ex_list;

        private static SecureUxTheme_Setup _SecureUxTheme_Setup;
        public static SecureUxTheme_Setup SecureUxTheme_Setup => (_SecureUxTheme_Setup == null || _SecureUxTheme_Setup.IsDisposed) ? (_SecureUxTheme_Setup = CreateInstance(_SecureUxTheme_Setup)) : _SecureUxTheme_Setup;

        private static AltTabEditor _AltTabEditor;
        public static AltTabEditor AltTabEditor => (_AltTabEditor == null || _AltTabEditor.IsDisposed) ? (_AltTabEditor = CreateInstance(_AltTabEditor)) : _AltTabEditor;

        private static ApplicationThemer _ApplicationThemer;
        public static ApplicationThemer ApplicationThemer => (_ApplicationThemer == null || _ApplicationThemer.IsDisposed) ? (_ApplicationThemer = CreateInstance(_ApplicationThemer)) : _ApplicationThemer;

        private static ScreenSaver_Editor _ScreenSaver_Editor;
        public static ScreenSaver_Editor ScreenSaver_Editor => (_ScreenSaver_Editor == null || _ScreenSaver_Editor.IsDisposed) ? (_ScreenSaver_Editor = CreateInstance(_ScreenSaver_Editor)) : _ScreenSaver_Editor;

        private static ScreenSavers_List _ScreenSavers_List;
        public static ScreenSavers_List ScreenSavers_List => (_ScreenSavers_List == null || _ScreenSavers_List.IsDisposed) ? (_ScreenSavers_List = CreateInstance(_ScreenSavers_List)) : _ScreenSavers_List;

        private static Sounds_Editor _Sounds_Editor;
        public static Sounds_Editor Sounds_Editor => (_Sounds_Editor == null || _Sounds_Editor.IsDisposed) ? (_Sounds_Editor = CreateInstance(_Sounds_Editor)) : _Sounds_Editor;

        private static Start8Selector _Start8Selector;
        public static Start8Selector Start8Selector => (_Start8Selector == null || _Start8Selector.IsDisposed) ? (_Start8Selector = CreateInstance(_Start8Selector)) : _Start8Selector;

        private static Wallpaper_Editor _Wallpaper_Editor;
        public static Wallpaper_Editor Wallpaper_Editor => (_Wallpaper_Editor == null || _Wallpaper_Editor.IsDisposed) ? (_Wallpaper_Editor = CreateInstance(_Wallpaper_Editor)) : _Wallpaper_Editor;

        private static WinEffecter _WinEffecter;
        public static WinEffecter WinEffecter => (_WinEffecter == null || _WinEffecter.IsDisposed) ? (_WinEffecter = CreateInstance(_WinEffecter)) : _WinEffecter;

        private static IconsStudio _IconsStudio;
        public static IconsStudio IconsStudio => (_IconsStudio == null || _IconsStudio.IsDisposed) ? (_IconsStudio = CreateInstance(_IconsStudio)) : _IconsStudio;

        private static Lang_Add_Snippet _Lang_Add_Snippet;
        public static Lang_Add_Snippet Lang_Add_Snippet => (_Lang_Add_Snippet == null || _Lang_Add_Snippet.IsDisposed) ? (_Lang_Add_Snippet = CreateInstance(_Lang_Add_Snippet)) : _Lang_Add_Snippet;

        private static Lang_Dashboard _Lang_Dashboard;
        public static Lang_Dashboard Lang_Dashboard => (_Lang_Dashboard == null || _Lang_Dashboard.IsDisposed) ? (_Lang_Dashboard = CreateInstance(_Lang_Dashboard)) : _Lang_Dashboard;

        private static Lang_JSON_GUI _Lang_JSON_GUI;
        public static Lang_JSON_GUI Lang_JSON_GUI => (_Lang_JSON_GUI == null || _Lang_JSON_GUI.IsDisposed) ? (_Lang_JSON_GUI = CreateInstance(_Lang_JSON_GUI)) : _Lang_JSON_GUI;

        private static Lang_JSON_Manage _Lang_JSON_Manage;
        public static Lang_JSON_Manage Lang_JSON_Manage => (_Lang_JSON_Manage == null || _Lang_JSON_Manage.IsDisposed) ? (_Lang_JSON_Manage = CreateInstance(_Lang_JSON_Manage)) : _Lang_JSON_Manage;

        private static Lang_JSON_Update _Lang_JSON_Update;
        public static Lang_JSON_Update Lang_JSON_Update => (_Lang_JSON_Update == null || _Lang_JSON_Update.IsDisposed) ? (_Lang_JSON_Update = CreateInstance(_Lang_JSON_Update)) : _Lang_JSON_Update;

        private static Lang_ReplaceText _Lang_ReplaceText;
        public static Lang_ReplaceText Lang_ReplaceText => (_Lang_ReplaceText == null || _Lang_ReplaceText.IsDisposed) ? (_Lang_ReplaceText = CreateInstance(_Lang_ReplaceText)) : _Lang_ReplaceText;

        private static LogonUI _LogonUI;
        public static LogonUI LogonUI => (_LogonUI == null || _LogonUI.IsDisposed) ? (_LogonUI = CreateInstance(_LogonUI)) : _LogonUI;

        private static LogonUI7 _LogonUI7;
        public static LogonUI7 LogonUI7 => (_LogonUI7 == null || _LogonUI7.IsDisposed) ? (_LogonUI7 = CreateInstance(_LogonUI7)) : _LogonUI7;

        private static LogonUI8_Pics _LogonUI8_Pics;
        public static LogonUI8_Pics LogonUI8_Pics => (_LogonUI8_Pics == null || _LogonUI8_Pics.IsDisposed) ? (_LogonUI8_Pics = CreateInstance(_LogonUI8_Pics)) : _LogonUI8_Pics;

        private static LogonUI8Colors _LogonUI8Colors;
        public static LogonUI8Colors LogonUI8Colors => (_LogonUI8Colors == null || _LogonUI8Colors.IsDisposed) ? (_LogonUI8Colors = CreateInstance(_LogonUI8Colors)) : _LogonUI8Colors;

        private static LogonUIXP _LogonUIXP;
        public static LogonUIXP LogonUIXP => (_LogonUIXP == null || _LogonUIXP.IsDisposed) ? (_LogonUIXP = CreateInstance(_LogonUIXP)) : _LogonUIXP;

        private static Metrics_Fonts _Metrics_Fonts;
        public static Metrics_Fonts Metrics_Fonts => (_Metrics_Fonts == null || _Metrics_Fonts.IsDisposed) ? (_Metrics_Fonts = CreateInstance(_Metrics_Fonts)) : _Metrics_Fonts;

        private static PaletteGenerateFromColor _PaletteGenerateFromColor;
        public static PaletteGenerateFromColor PaletteGenerateFromColor => (_PaletteGenerateFromColor == null || _PaletteGenerateFromColor.IsDisposed) ? (_PaletteGenerateFromColor = CreateInstance(_PaletteGenerateFromColor)) : _PaletteGenerateFromColor;

        private static PaletteGenerateFromImage _PaletteGenerateFromImage;
        public static PaletteGenerateFromImage PaletteGenerateFromImage => (_PaletteGenerateFromImage == null || _PaletteGenerateFromImage.IsDisposed) ? (_PaletteGenerateFromImage = CreateInstance(_PaletteGenerateFromImage)) : _PaletteGenerateFromImage;

        private static SettingsX _SettingsX;
        public static SettingsX SettingsX => (_SettingsX == null || _SettingsX.IsDisposed) ? (_SettingsX = CreateInstance(_SettingsX)) : _SettingsX;

        private static Uninstall _Uninstall;
        public static Uninstall Uninstall => (_Uninstall == null || _Uninstall.IsDisposed) ? (_Uninstall = CreateInstance(_Uninstall)) : _Uninstall;

        private static Store _Store;
        public static Store Store => (_Store == null || _Store.IsDisposed) ? (_Store = CreateInstance(_Store)) : _Store;

        private static Store_CPToggles _Store_CPToggles;
        public static Store_CPToggles Store_CPToggles => (_Store_CPToggles == null || _Store_CPToggles.IsDisposed) ? (_Store_CPToggles = CreateInstance(_Store_CPToggles)) : _Store_CPToggles;

        private static Store_DownloadProgress _Store_DownloadProgress;
        public static Store_DownloadProgress Store_DownloadProgress => (_Store_DownloadProgress == null || _Store_DownloadProgress.IsDisposed) ? (_Store_DownloadProgress = CreateInstance(_Store_DownloadProgress)) : _Store_DownloadProgress;

        private static Store_Hover _Store_Hover;
        public static Store_Hover Store_Hover => (_Store_Hover == null || _Store_Hover.IsDisposed) ? (_Store_Hover = CreateInstance(_Store_Hover)) : _Store_Hover;

        private static Store_Intro _Store_Intro;
        public static Store_Intro Store_Intro => (_Store_Intro == null || _Store_Intro.IsDisposed) ? (_Store_Intro = CreateInstance(_Store_Intro)) : _Store_Intro;

        private static Store_SearchFilter _Store_SearchFilter;
        public static Store_SearchFilter Store_SearchFilter => (_Store_SearchFilter == null || _Store_SearchFilter.IsDisposed) ? (_Store_SearchFilter = CreateInstance(_Store_SearchFilter)) : _Store_SearchFilter;

        private static Store_ThemeLicense _Store_ThemeLicense;
        public static Store_ThemeLicense Store_ThemeLicense => (_Store_ThemeLicense == null || _Store_ThemeLicense.IsDisposed) ? (_Store_ThemeLicense = CreateInstance(_Store_ThemeLicense)) : _Store_ThemeLicense;

        private static CMD _CMD;
        public static CMD CMD => (_CMD == null || _CMD.IsDisposed) ? (_CMD = CreateInstance(_CMD)) : _CMD;

        private static ExternalTerminal _ExternalTerminal;
        public static ExternalTerminal ExternalTerminal => (_ExternalTerminal == null || _ExternalTerminal.IsDisposed) ? (_ExternalTerminal = CreateInstance(_ExternalTerminal)) : _ExternalTerminal;

        private static NewExtTerminal _NewExtTerminal;
        public static NewExtTerminal NewExtTerminal => (_NewExtTerminal == null || _NewExtTerminal.IsDisposed) ? (_NewExtTerminal = CreateInstance(_NewExtTerminal)) : _NewExtTerminal;

        private static TerminalInfo _TerminalInfo;
        public static TerminalInfo TerminalInfo => (_TerminalInfo == null || _TerminalInfo.IsDisposed) ? (_TerminalInfo = CreateInstance(_TerminalInfo)) : _TerminalInfo;

        private static TerminalsDashboard _TerminalsDashboard;
        public static TerminalsDashboard TerminalsDashboard => (_TerminalsDashboard == null || _TerminalsDashboard.IsDisposed) ? (_TerminalsDashboard = CreateInstance(_TerminalsDashboard)) : _TerminalsDashboard;

        private static WindowsTerminal _WindowsTerminal;
        public static WindowsTerminal WindowsTerminal => (_WindowsTerminal == null || _WindowsTerminal.IsDisposed) ? (_WindowsTerminal = CreateInstance(_WindowsTerminal)) : _WindowsTerminal;

        private static WindowsTerminalCopycat _WindowsTerminalCopycat;
        public static WindowsTerminalCopycat WindowsTerminalCopycat => (_WindowsTerminalCopycat == null || _WindowsTerminalCopycat.IsDisposed) ? (_WindowsTerminalCopycat = CreateInstance(_WindowsTerminalCopycat)) : _WindowsTerminalCopycat;

        private static WindowsTerminalDecide _WindowsTerminalDecide;
        public static WindowsTerminalDecide WindowsTerminalDecide => (_WindowsTerminalDecide == null || _WindowsTerminalDecide.IsDisposed) ? (_WindowsTerminalDecide = CreateInstance(_WindowsTerminalDecide)) : _WindowsTerminalDecide;

        private static Updates _Updates;
        public static Updates Updates => (_Updates == null || _Updates.IsDisposed) ? (_Updates = CreateInstance(_Updates)) : _Updates;

        private static Whatsnew _Whatsnew;
        public static Whatsnew Whatsnew => (_Whatsnew == null || _Whatsnew.IsDisposed) ? (_Whatsnew = CreateInstance(_Whatsnew)) : _Whatsnew;

        private static Welcome _Welcome;
        public static Welcome Welcome => (_Welcome == null || _Welcome.IsDisposed) ? (_Welcome = CreateInstance(_Welcome)) : _Welcome;

        private static Win32UI_Fullscreen _Win32UI_Fullscreen;
        public static Win32UI_Fullscreen Win32UI_Fullscreen => (_Win32UI_Fullscreen == null || _Win32UI_Fullscreen.IsDisposed) ? (_Win32UI_Fullscreen = CreateInstance(_Win32UI_Fullscreen)) : _Win32UI_Fullscreen;

        private static Win32UI_Gallery _Win32UI_Gallery;
        public static Win32UI_Gallery Win32UI_Gallery => (_Win32UI_Gallery == null || _Win32UI_Gallery.IsDisposed) ? (_Win32UI_Gallery = CreateInstance(_Win32UI_Gallery)) : _Win32UI_Gallery;

        private static Win32UI _Win32UI;
        public static Win32UI Win32UI => (_Win32UI == null || _Win32UI.IsDisposed) ? (_Win32UI = CreateInstance(_Win32UI)) : _Win32UI;

        private static GlassWindow _glassWindow;
        public static GlassWindow GlassWindow => (_glassWindow == null || _glassWindow.IsDisposed) ? (_glassWindow = CreateInstance(_glassWindow)) : _glassWindow;

        private static EditInfo _EditInfo;
        public static EditInfo EditInfo => (_EditInfo == null || _EditInfo.IsDisposed) ? (_EditInfo = CreateInstance(_EditInfo)) : _EditInfo;

        private static MainForm _MainForm;
        public static MainForm MainForm => (_MainForm == null || _MainForm.IsDisposed) ? (_MainForm = CreateInstance(_MainForm)) : _MainForm;

        private static Home _Home;
        public static Home Home => (_Home == null || _Home.IsDisposed) ? (_Home = CreateInstance(_Home)) : _Home;

        private static BackupThemes_List _BackupThemes_List;
        public static BackupThemes_List BackupThemes_List => (_BackupThemes_List == null || _BackupThemes_List.IsDisposed) ? (_BackupThemes_List = CreateInstance(_BackupThemes_List)) : _BackupThemes_List;

        private static OS_Dashboard _OS_Dashboard;
        public static OS_Dashboard OS_Dashboard => (_OS_Dashboard == null || _OS_Dashboard.IsDisposed) ? (_OS_Dashboard = CreateInstance(_OS_Dashboard)) : _OS_Dashboard;

        #region Engine

        private static Dictionary<Type, Form> formBeingCreated;

        private static T CreateInstance<T>(T instance) where T : Form, new()
        {
            if (instance is null || instance.IsDisposed)
            {
                formBeingCreated ??= new Dictionary<Type, Form>();

                // Check if the form is already being created
                if (formBeingCreated.ContainsKey(typeof(T)))
                {
                    // Return the existing instance to handle recursive creation
                    return instance;
                }

                formBeingCreated[typeof(T)] = null;

                try
                {
                    instance = new T();
                    return instance;
                }
                catch (System.Reflection.TargetInvocationException ex) when (ex.InnerException is not null)
                {
                    string betterMessage = $"Error creating form: {ex.InnerException.Message}";
                    throw new InvalidOperationException(betterMessage, ex.InnerException);
                }
                finally
                {
                    formBeingCreated.Remove(typeof(T));
                }
            }
            else
            {
                return instance;
            }
        }

        //private static void DisposeInstance<T>(ref T instance) where T : Form
        //{
        //    instance?.Dispose();
        //    instance = null;
        //}

        #endregion
    }
}