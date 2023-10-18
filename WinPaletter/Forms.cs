using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System;
using WinPaletter.Dialogs;

namespace WinPaletter
{
    internal partial class Forms
    {
        private static string ex_msg = "Property can only be set to nothing";


        private static ColorInfoDragDrop _ColorInfoDragDrop;
        public static ColorInfoDragDrop ColorInfoDragDrop
        {
            get
            {
                _ColorInfoDragDrop = CreateInstance(_ColorInfoDragDrop);
                return _ColorInfoDragDrop;
            }
            set
            {
                if (value == _ColorInfoDragDrop)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _ColorInfoDragDrop);
            }
        }


        private static ColorPickerDlg _ColorPickerDlg;
        public static ColorPickerDlg ColorPickerDlg
        {
            get
            {
                _ColorPickerDlg = CreateInstance(_ColorPickerDlg);
                return _ColorPickerDlg;
            }
            set
            {
                if (value == _ColorPickerDlg)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _ColorPickerDlg);
            }
        }


        private static SubMenu _SubMenu;
        public static SubMenu SubMenu
        {
            get
            {
                _SubMenu = CreateInstance(_SubMenu);
                return _SubMenu;
            }
            set
            {
                if (value == _SubMenu)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _SubMenu);
            }
        }


        private static Converter_Form _Converter_Form;
        public static Converter_Form Converter_Form
        {
            get
            {
                _Converter_Form = CreateInstance(_Converter_Form);
                return _Converter_Form;
            }
            set
            {
                if (value == _Converter_Form)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _Converter_Form);
            }
        }


        private static CursorsStudio _CursorsStudio;
        public static CursorsStudio CursorsStudio
        {
            get
            {
                _CursorsStudio = CreateInstance(_CursorsStudio);
                return _CursorsStudio;
            }
            set
            {
                if (value == _CursorsStudio)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _CursorsStudio);
            }
        }


        private static About _About;
        public static About About
        {
            get
            {
                _About = CreateInstance(_About);
                return _About;
            }
            set
            {
                if (value == _About)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _About);
            }
        }


        private static ThemeLog _ThemeLog;
        public static ThemeLog ThemeLog
        {
            get
            {
                _ThemeLog = CreateInstance(_ThemeLog);
                return _ThemeLog;
            }
            set
            {
                if (value == _ThemeLog)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _ThemeLog);
            }
        }


        private static RescueTools _RescueTools;
        public static RescueTools RescueTools
        {
            get
            {
                _RescueTools = CreateInstance(_RescueTools);
                return _RescueTools;
            }
            set
            {
                if (value == _RescueTools)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _RescueTools);
            }
        }

        private static BugReport _BugReport;
        public static BugReport BugReport
        {
            get
            {
                _BugReport = CreateInstance(_BugReport);
                return _BugReport;
            }
            set
            {
                if (value == _BugReport)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _BugReport);
            }
        }


        private static ComplexSave _ComplexSave;
        public static ComplexSave ComplexSave
        {
            get
            {
                _ComplexSave = CreateInstance(_ComplexSave);
                return _ComplexSave;
            }
            set
            {
                if (value == _ComplexSave)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _ComplexSave);
            }
        }


        private static LicenseForm _LicenseForm;
        public static LicenseForm LicenseForm
        {
            get
            {
                _LicenseForm = CreateInstance(_LicenseForm);
                return _LicenseForm;
            }
            set
            {
                if (value == _LicenseForm)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _LicenseForm);
            }
        }


        private static PE_Warning _PE_Warning;
        public static PE_Warning PE_Warning
        {
            get
            {
                _PE_Warning = CreateInstance(_PE_Warning);
                return _PE_Warning;
            }
            set
            {
                if (value == _PE_Warning)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _PE_Warning);
            }
        }


        private static Saving_ex_list _Saving_ex_list;
        public static Saving_ex_list Saving_ex_list
        {
            get
            {
                _Saving_ex_list = CreateInstance(_Saving_ex_list);
                return _Saving_ex_list;
            }
            set
            {
                if (value == _Saving_ex_list)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _Saving_ex_list);
            }
        }


        private static AltTabEditor _AltTabEditor;
        public static AltTabEditor AltTabEditor
        {
            get
            {
                _AltTabEditor = CreateInstance(_AltTabEditor);
                return _AltTabEditor;
            }
            set
            {
                if (value == _AltTabEditor)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _AltTabEditor);
            }
        }

        private static ApplicationThemer _ApplicationThemer;
        public static ApplicationThemer ApplicationThemer
        {
            get
            {
                _ApplicationThemer = CreateInstance(_ApplicationThemer);
                return _ApplicationThemer;
            }
            set
            {
                if (value == _ApplicationThemer)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _ApplicationThemer);
            }
        }


        private static ScreenSaver_Editor _ScreenSaver_Editor;
        public static ScreenSaver_Editor ScreenSaver_Editor
        {
            get
            {
                _ScreenSaver_Editor = CreateInstance(_ScreenSaver_Editor);
                return _ScreenSaver_Editor;
            }
            set
            {
                if (value == _ScreenSaver_Editor)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _ScreenSaver_Editor);
            }
        }


        private static Sounds_Editor _Sounds_Editor;
        public static Sounds_Editor Sounds_Editor
        {
            get
            {
                _Sounds_Editor = CreateInstance(_Sounds_Editor);
                return _Sounds_Editor;
            }
            set
            {
                if (value == _Sounds_Editor)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _Sounds_Editor);
            }
        }


        private static Start8Selector _Start8Selector;
        public static Start8Selector Start8Selector
        {
            get
            {
                _Start8Selector = CreateInstance(_Start8Selector);
                return _Start8Selector;
            }
            set
            {
                if (value == _Start8Selector)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _Start8Selector);
            }
        }


        private static Wallpaper_Editor _Wallpaper_Editor;
        public static Wallpaper_Editor Wallpaper_Editor
        {
            get
            {
                _Wallpaper_Editor = CreateInstance(_Wallpaper_Editor);
                return _Wallpaper_Editor;
            }
            set
            {
                if (value == _Wallpaper_Editor)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _Wallpaper_Editor);
            }
        }


        private static WinEffecter _WinEffecter;
        public static WinEffecter WinEffecter
        {
            get
            {
                _WinEffecter = CreateInstance(_WinEffecter);
                return _WinEffecter;
            }
            set
            {
                if (value == _WinEffecter)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _WinEffecter);
            }
        }


        private static Lang_Add_Snippet _Lang_Add_Snippet;
        public static Lang_Add_Snippet Lang_Add_Snippet
        {
            get
            {
                _Lang_Add_Snippet = CreateInstance(_Lang_Add_Snippet);
                return _Lang_Add_Snippet;
            }
            set
            {
                if (value == _Lang_Add_Snippet)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _Lang_Add_Snippet);
            }
        }


        private static Lang_Dashboard _Lang_Dashboard;
        public static Lang_Dashboard Lang_Dashboard
        {
            get
            {
                _Lang_Dashboard = CreateInstance(_Lang_Dashboard);
                return _Lang_Dashboard;
            }
            set
            {
                if (value == _Lang_Dashboard)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _Lang_Dashboard);
            }
        }


        private static Lang_JSON_GUI _Lang_JSON_GUI;
        public static Lang_JSON_GUI Lang_JSON_GUI
        {
            get
            {
                _Lang_JSON_GUI = CreateInstance(_Lang_JSON_GUI);
                return _Lang_JSON_GUI;
            }
            set
            {
                if (value == _Lang_JSON_GUI)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _Lang_JSON_GUI);
            }
        }


        private static Lang_JSON_Manage _Lang_JSON_Manage;
        public static Lang_JSON_Manage Lang_JSON_Manage
        {
            get
            {
                _Lang_JSON_Manage = CreateInstance(_Lang_JSON_Manage);
                return _Lang_JSON_Manage;
            }
            set
            {
                if (value == _Lang_JSON_Manage)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _Lang_JSON_Manage);
            }
        }


        private static Lang_JSON_Update _Lang_JSON_Update;
        public static Lang_JSON_Update Lang_JSON_Update
        {
            get
            {
                _Lang_JSON_Update = CreateInstance(_Lang_JSON_Update);
                return _Lang_JSON_Update;
            }
            set
            {
                if (value == _Lang_JSON_Update)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _Lang_JSON_Update);
            }
        }


        private static Lang_ReplaceText _Lang_ReplaceText;
        public static Lang_ReplaceText Lang_ReplaceText
        {
            get
            {
                _Lang_ReplaceText = CreateInstance(_Lang_ReplaceText);
                return _Lang_ReplaceText;
            }
            set
            {
                if (value == _Lang_ReplaceText)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _Lang_ReplaceText);
            }
        }


        private static LogonUI _LogonUI;
        public static LogonUI LogonUI
        {
            get
            {
                _LogonUI = CreateInstance(_LogonUI);
                return _LogonUI;
            }
            set
            {
                if (value == _LogonUI)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _LogonUI);
            }
        }


        private static LogonUI7 _LogonUI7;
        public static LogonUI7 LogonUI7
        {
            get
            {
                _LogonUI7 = CreateInstance(_LogonUI7);
                return _LogonUI7;
            }
            set
            {
                if (value == _LogonUI7)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _LogonUI7);
            }
        }


        private static LogonUI8_Pics _LogonUI8_Pics;
        public static LogonUI8_Pics LogonUI8_Pics
        {
            get
            {
                _LogonUI8_Pics = CreateInstance(_LogonUI8_Pics);
                return _LogonUI8_Pics;
            }
            set
            {
                if (value == _LogonUI8_Pics)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _LogonUI8_Pics);
            }
        }


        private static LogonUI8Colors _LogonUI8Colors;
        public static LogonUI8Colors LogonUI8Colors
        {
            get
            {
                _LogonUI8Colors = CreateInstance(_LogonUI8Colors);
                return _LogonUI8Colors;
            }
            set
            {
                if (value == _LogonUI8Colors)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _LogonUI8Colors);
            }
        }


        private static LogonUIXP _LogonUIXP;
        public static LogonUIXP LogonUIXP
        {
            get
            {
                _LogonUIXP = CreateInstance(_LogonUIXP);
                return _LogonUIXP;
            }
            set
            {
                if (value == _LogonUIXP)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _LogonUIXP);
            }
        }


        private static Metrics_Fonts _Metrics_Fonts;
        public static Metrics_Fonts Metrics_Fonts
        {
            get
            {
                _Metrics_Fonts = CreateInstance(_Metrics_Fonts);
                return _Metrics_Fonts;
            }
            set
            {
                if (value == _Metrics_Fonts)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _Metrics_Fonts);
            }
        }


        private static VS2Metrics _VS2Metrics;
        public static VS2Metrics VS2Metrics
        {
            get
            {
                _VS2Metrics = CreateInstance(_VS2Metrics);
                return _VS2Metrics;
            }
            set
            {
                if (value == _VS2Metrics)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _VS2Metrics);
            }
        }


        private static PaletteGenerateDashboard _PaletteGenerateDashboard;
        public static PaletteGenerateDashboard PaletteGenerateDashboard
        {
            get
            {
                _PaletteGenerateDashboard = CreateInstance(_PaletteGenerateDashboard);
                return _PaletteGenerateDashboard;
            }
            set
            {
                if (value == _PaletteGenerateDashboard)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _PaletteGenerateDashboard);
            }
        }


        private static PaletteGenerateFromColor _PaletteGenerateFromColor;
        public static PaletteGenerateFromColor PaletteGenerateFromColor
        {
            get
            {
                _PaletteGenerateFromColor = CreateInstance(_PaletteGenerateFromColor);
                return _PaletteGenerateFromColor;
            }
            set
            {
                if (value == _PaletteGenerateFromColor)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _PaletteGenerateFromColor);
            }
        }


        private static PaletteGenerateFromImage _PaletteGenerateFromImage;
        public static PaletteGenerateFromImage PaletteGenerateFromImage
        {
            get
            {
                _PaletteGenerateFromImage = CreateInstance(_PaletteGenerateFromImage);
                return _PaletteGenerateFromImage;
            }
            set
            {
                if (value == _PaletteGenerateFromImage)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _PaletteGenerateFromImage);
            }
        }


        private static SettingsX _SettingsX;
        public static SettingsX SettingsX
        {
            get
            {
                _SettingsX = CreateInstance(_SettingsX);
                return _SettingsX;
            }
            set
            {
                if (value == _SettingsX)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _SettingsX);
            }
        }


        private static Uninstall _Uninstall;
        public static Uninstall Uninstall
        {
            get
            {
                _Uninstall = CreateInstance(_Uninstall);
                return _Uninstall;
            }
            set
            {
                if (value == _Uninstall)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _Uninstall);
            }
        }


        private static Store _Store;
        public static Store Store
        {
            get
            {
                _Store = CreateInstance(_Store);
                return _Store;
            }
            set
            {
                if (value == _Store)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _Store);
            }
        }


        private static Store_CPToggles _Store_CPToggles;
        public static Store_CPToggles Store_CPToggles
        {
            get
            {
                _Store_CPToggles = CreateInstance(_Store_CPToggles);
                return _Store_CPToggles;
            }
            set
            {
                if (value == _Store_CPToggles)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _Store_CPToggles);
            }
        }


        private static Store_DownloadProgress _Store_DownloadProgress;
        public static Store_DownloadProgress Store_DownloadProgress
        {
            get
            {
                _Store_DownloadProgress = CreateInstance(_Store_DownloadProgress);
                return _Store_DownloadProgress;
            }
            set
            {
                if (value == _Store_DownloadProgress)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _Store_DownloadProgress);
            }
        }


        private static Store_Hover _Store_Hover;
        public static Store_Hover Store_Hover
        {
            get
            {
                _Store_Hover = CreateInstance(_Store_Hover);
                return _Store_Hover;
            }
            set
            {
                if (value == _Store_Hover)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _Store_Hover);
            }
        }


        private static Store_Intro _Store_Intro;
        public static Store_Intro Store_Intro
        {
            get
            {
                _Store_Intro = CreateInstance(_Store_Intro);
                return _Store_Intro;
            }
            set
            {
                if (value == _Store_Intro)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _Store_Intro);
            }
        }


        private static Store_SearchFilter _Store_SearchFilter;
        public static Store_SearchFilter Store_SearchFilter
        {
            get
            {
                _Store_SearchFilter = CreateInstance(_Store_SearchFilter);
                return _Store_SearchFilter;
            }
            set
            {
                if (value == _Store_SearchFilter)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _Store_SearchFilter);
            }
        }


        private static Store_ThemeLicense _Store_ThemeLicense;
        public static Store_ThemeLicense Store_ThemeLicense
        {
            get
            {
                _Store_ThemeLicense = CreateInstance(_Store_ThemeLicense);
                return _Store_ThemeLicense;
            }
            set
            {
                if (value == _Store_ThemeLicense)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _Store_ThemeLicense);
            }
        }


        private static CMD _CMD;
        public static CMD CMD
        {
            get
            {
                _CMD = CreateInstance(_CMD);
                return _CMD;
            }
            set
            {
                if (value == _CMD)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _CMD);
            }
        }


        private static ExternalTerminal _ExternalTerminal;
        public static ExternalTerminal ExternalTerminal
        {
            get
            {
                _ExternalTerminal = CreateInstance(_ExternalTerminal);
                return _ExternalTerminal;
            }
            set
            {
                if (value == _ExternalTerminal)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _ExternalTerminal);
            }
        }


        private static NewExtTerminal _NewExtTerminal;
        public static NewExtTerminal NewExtTerminal
        {
            get
            {
                _NewExtTerminal = CreateInstance(_NewExtTerminal);
                return _NewExtTerminal;
            }
            set
            {
                if (value == _NewExtTerminal)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _NewExtTerminal);
            }
        }


        private static TerminalInfo _TerminalInfo;
        public static TerminalInfo TerminalInfo
        {
            get
            {
                _TerminalInfo = CreateInstance(_TerminalInfo);
                return _TerminalInfo;
            }
            set
            {
                if (value == _TerminalInfo)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _TerminalInfo);
            }
        }


        private static TerminalsDashboard _TerminalsDashboard;
        public static TerminalsDashboard TerminalsDashboard
        {
            get
            {
                _TerminalsDashboard = CreateInstance(_TerminalsDashboard);
                return _TerminalsDashboard;
            }
            set
            {
                if (value == _TerminalsDashboard)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _TerminalsDashboard);
            }
        }


        private static WindowsTerminal _WindowsTerminal;
        public static WindowsTerminal WindowsTerminal
        {
            get
            {
                _WindowsTerminal = CreateInstance(_WindowsTerminal);
                return _WindowsTerminal;
            }
            set
            {
                if (value == _WindowsTerminal)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _WindowsTerminal);
            }
        }


        private static WindowsTerminalCopycat _WindowsTerminalCopycat;
        public static WindowsTerminalCopycat WindowsTerminalCopycat
        {
            get
            {
                _WindowsTerminalCopycat = CreateInstance(_WindowsTerminalCopycat);
                return _WindowsTerminalCopycat;
            }
            set
            {
                if (value == _WindowsTerminalCopycat)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _WindowsTerminalCopycat);
            }
        }


        private static WindowsTerminalDecide _WindowsTerminalDecide;
        public static WindowsTerminalDecide WindowsTerminalDecide
        {
            get
            {
                _WindowsTerminalDecide = CreateInstance(_WindowsTerminalDecide);
                return _WindowsTerminalDecide;
            }
            set
            {
                if (value == _WindowsTerminalDecide)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _WindowsTerminalDecide);
            }
        }


        private static Updates _Updates;
        public static Updates Updates
        {
            get
            {
                _Updates = CreateInstance(_Updates);
                return _Updates;
            }
            set
            {
                if (value == _Updates)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _Updates);
            }
        }


        private static Whatsnew _Whatsnew;
        public static Whatsnew Whatsnew
        {
            get
            {
                _Whatsnew = CreateInstance(_Whatsnew);
                return _Whatsnew;
            }
            set
            {
                if (value == _Whatsnew)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _Whatsnew);
            }
        }


        private static VS2Win32UI _VS2Win32UI;
        public static VS2Win32UI VS2Win32UI
        {
            get
            {
                _VS2Win32UI = CreateInstance(_VS2Win32UI);
                return _VS2Win32UI;
            }
            set
            {
                if (value == _VS2Win32UI)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _VS2Win32UI);
            }
        }


        private static Win32UI _Win32UI;
        public static Win32UI Win32UI
        {
            get
            {
                _Win32UI = CreateInstance(_Win32UI);
                return _Win32UI;
            }
            set
            {
                if (value == _Win32UI)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _Win32UI);
            }
        }


        private static BK _BK;
        public static BK BK
        {
            get
            {
                _BK = CreateInstance(_BK);
                return _BK;
            }
            set
            {
                if (value == _BK)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _BK);
            }
        }


        private static EditInfo _EditInfo;
        public static EditInfo EditInfo
        {
            get
            {
                _EditInfo = CreateInstance(_EditInfo);
                return _EditInfo;
            }
            set
            {
                if (value == _EditInfo)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _EditInfo);
            }
        }


        private static MainFrm _MainFrm;
        public static MainFrm MainFrm
        {
            get
            {
                _MainFrm = CreateInstance(_MainFrm);
                return _MainFrm;
            }
            set
            {
                if (value == _MainFrm)
                    return;
                if (value is not null)
                    throw new ArgumentException(ex_msg);
                DisposeInstance(ref _MainFrm);
            }
        }


        [ThreadStatic()]
        private static Hashtable m_FormBeingCreated;

        [DebuggerHidden()]
        private static T CreateInstance<T>(T Instance) where T : Form, new()
        {
            if (Instance is null || Instance.IsDisposed)
            {
                if (m_FormBeingCreated is not null)
                {
                    if (m_FormBeingCreated.ContainsKey(typeof(T)) == true)
                    {
                        throw new InvalidOperationException(Microsoft.VisualBasic.CompilerServices.Utils.GetResourceString("WinForms_RecursiveFormCreate"));
                    }
                }
                else
                {
                    m_FormBeingCreated = new Hashtable();
                }
                m_FormBeingCreated.Add(typeof(T), null);
                try
                {
                    return new T();
                }
                catch (System.Reflection.TargetInvocationException ex) when (ex.InnerException is not null)
                {
                    string BetterMessage = Microsoft.VisualBasic.CompilerServices.Utils.GetResourceString("WinForms_SeeInnerException", ex.InnerException.Message);
                    throw new InvalidOperationException(BetterMessage, ex.InnerException);
                }
                finally
                {
                    m_FormBeingCreated.Remove(typeof(T));
                }
            }
            else
            {
                return Instance;
            }
        }

        [DebuggerHidden()]
        private static void DisposeInstance<T>(ref T instance) where T : Form
        {
            instance.Dispose();
            instance = null;
        }
    }
}