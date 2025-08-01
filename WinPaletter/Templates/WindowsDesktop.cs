﻿using Devcorp.Controls.VisualStyles;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.UI.Retro;
using WinPaletter.UI.Simulation;
using static WinPaletter.PreviewHelpers;
using static WinPaletter.Theme.Structures.Windows10x;
using static WinPaletter.UI.Simulation.Window;

namespace WinPaletter.Templates
{
    /// <summary>
    /// Windows Desktop template
    /// </summary>
    public partial class WindowsDesktop : UserControl
    {
        /// <summary>
        /// Create a new instance of <see cref="WindowsDesktop"/>
        /// </summary>
        public WindowsDesktop()
        {
            DoubleBuffered = true;

            InitializeComponent();
        }

        /// <summary>
        /// Property string used as reference for property editing event "SettingsIconsColor"
        /// </summary>
        public static string prop_settingsIconsColor = "SettingsIconsColor";

        /// <summary>
        /// Property string used as reference for property editing event "LinksColor"
        /// </summary>
        public static string prop_linksColor = "LinksColor";

        #region Variables

        private string msstyles;

        private Color Win10_taskbarColor => _darkMode_Win ? Color.FromArgb(16, 16, 16) : Color.FromArgb(238, 238, 238);
        private Color Win10_taskbarAppBackgroundColor => _darkMode_Win ? Color.FromArgb(150, 150, 150, 150) : Color.FromArgb(150, 238, 238, 238);
        private Color Win10_startColor => _darkMode_Win ? Color.FromArgb(31, 31, 31) : Color.FromArgb(228, 228, 228);
        private Color Win10_actionCenterColor => _darkMode_Win ? Color.FromArgb(31, 31, 31) : Color.FromArgb(228, 228, 228);
        private Color Win11_color => _darkMode_Win ? Color.FromArgb(28, 28, 28) : Color.FromArgb(255, 255, 255);


        private readonly bool WXP_VS_ReplaceColors = false;
        private readonly bool WXP_VS_ReplaceMetrics = false;
        private readonly bool WXP_VS_ReplaceFonts = false;

        #endregion

        #region Global Properties

        /// <summary>
        /// Enable editing colors on clicking on a classic color element
        /// </summary>
        public bool EnableEditingColors
        {
            get { return enableColorsEditing; }
            set
            {
                if (enableColorsEditing != value)
                {
                    enableColorsEditing = value;

                    if (!DesignMode)
                    {
                        foreach (Window window in this.GetAllControls().OfType<Window>()) window.EnableEditingColors = value;
                        foreach (WinElement element in this.GetAllControls().OfType<WinElement>()) element.EnableEditingColors = value;
                    }
                }
            }
        }
        private bool enableColorsEditing = false;

        /// <summary>
        /// <see cref="Theme.Manager"/> used for current preview
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Theme.Manager HookedTM
        {
            get => !DesignMode ? _hookedTM ?? Program.TM : null;
            set
            {
                _hookedTM = value;
                LoadFromTM(_hookedTM);
            }
        }
        private Theme.Manager _hookedTM = Program.TM;

        /// <summary>
        /// Visual style resources used for Windows WXP preview
        /// </summary>
        public VisualStylesRes resVS
        {
            get => _resVS;
            set
            {
                if (value != _resVS)
                {
                    _resVS = value;
                    foreach (Window window in this.GetAllControls().OfType<Window>())
                    {
                        window.resVS = value;
                        window.Refresh();
                    }

                    foreach (WinElement element in this.GetAllControls().OfType<WinElement>())
                    {
                        element.resVS = value;
                        element.Refresh();
                    }
                }
            }
        }
        private VisualStylesRes _resVS = null;

        /// <summary>
        /// Windows visual styles used in the preview
        /// </summary>
        public Theme.Structures.VisualStyles.DefaultVisualStyles VisualStyles
        {
            get => _visualStyles;
            set
            {
                if (value != _visualStyles || ForceRefresh)
                {
                    _visualStyles = value;
                    SetTheme();
                }
            }
        }
        private Theme.Structures.VisualStyles.DefaultVisualStyles _visualStyles = Theme.Structures.VisualStyles.DefaultVisualStyles.Aero;

        /// <summary>
        /// Windows theme path used in the preview
        /// </summary>
        public string VisualStylesPath
        {
            get => _visualStylesPath;
            set
            {
                if (value != _visualStylesPath || ForceRefresh)
                {
                    _visualStylesPath = value;
                    SetTheme();
                }
            }
        }
        private string _visualStylesPath;

        /// <summary>
        /// Windows theme color scheme used in the preview
        /// </summary>
        public string VisualStylesColorScheme
        {
            get => _visualStylesColorScheme;
            set
            {
                if (value != _visualStylesColorScheme || ForceRefresh)
                {
                    _visualStylesColorScheme = value;
                    SetTheme();
                }
            }
        }
        private string _visualStylesColorScheme;


        private WindowStyle _windowStyle = WindowStyle.W11;
        /// <summary>
        /// Style of the windows in the preview
        /// </summary>
        public WindowStyle WindowStyle
        {
            get { return _windowStyle; }
            set
            {
                if (_windowStyle != value)
                {
                    _windowStyle = value;

                    //Visible = false;
                    //if (!DesignMode) Program.Animator.HideSync(this);

                    SetStyles();

                    //Visible = true;
                    //if (!DesignMode) Program.Animator.ShowSync(this);
                }
            }
        }


        private Preview_Enum _preview = Preview_Enum.W11;
        /// <summary>
        /// Preview style
        /// </summary>
        public Preview_Enum Preview
        {
            get { return _preview; }
            set
            {
                if (_preview != value)
                {
                    _preview = value;

                    if (value == Preview_Enum.W11 || value == Preview_Enum.W11Lite)
                    {
                        ActionCenter.Style = WinElement.Styles.ActionCenter11;

                        if (ExplorerPatcher.CanBeUsed)
                        {
                            {
                                ref ExplorerPatcher EP = ref Program.EP;
                                if ((bool)!EP?.UseStart10)
                                {
                                    start.Style = UI.Simulation.WinElement.Styles.Start11;
                                }
                                else
                                {
                                    start.Style = UI.Simulation.WinElement.Styles.Start10;
                                }

                                if ((bool)!EP?.UseTaskbar10)
                                {
                                    taskbar.Style = UI.Simulation.WinElement.Styles.Taskbar11;
                                }
                                else
                                {
                                    taskbar.Style = UI.Simulation.WinElement.Styles.Taskbar10;
                                }
                            }
                        }
                        else
                        {
                            start.Style = UI.Simulation.WinElement.Styles.Start11;
                            taskbar.Style = UI.Simulation.WinElement.Styles.Taskbar11;
                        }

                        ActionCenter.Visible = true;
                    }
                    else if (value == Preview_Enum.W10 || value == Preview_Enum.W10Lite)
                    {
                        start.Style = WinElement.Styles.Start10;
                        ActionCenter.Style = WinElement.Styles.ActionCenter10;
                        taskbar.Style = WinElement.Styles.Taskbar10;

                        ActionCenter.Visible = true;
                    }
                    else if (value == Preview_Enum.W8)
                    {
                        start.Style = WinElement.Styles.Start8;
                        taskbar.Style = WindowStyle == WindowStyle.W81 ? WinElement.Styles.Taskbar81Aero : WinElement.Styles.Taskbar8Aero;

                        ActionCenter.Visible = false;

                        taskbar.BackColorAlpha = 100;
                        taskbar.Transparency = true;
                    }
                    else if (value == Preview_Enum.W8Lite)
                    {
                        start.Style = WinElement.Styles.Start8;
                        taskbar.Style = WindowStyle == WindowStyle.W81 ? WinElement.Styles.Taskbar81Lite : WinElement.Styles.Taskbar8Lite;

                        ActionCenter.Visible = false;

                        taskbar.BackColorAlpha = 255;
                        taskbar.Transparency = false;
                    }
                    else if (value == Preview_Enum.W7Aero)
                    {
                        if (!WinVista)
                        {
                            start.Style = WinElement.Styles.Start7Aero;
                            taskbar.Style = WinElement.Styles.Taskbar7Aero;
                        }
                        else
                        {
                            start.Style = WinElement.Styles.StartVistaAero;
                            taskbar.Style = WinElement.Styles.TaskbarVistaAero;
                        }

                        ActionCenter.Visible = false;
                    }
                    else if (value == Preview_Enum.W7Opaque)
                    {
                        if (!WinVista)
                        {
                            start.Style = WinElement.Styles.Start7Opaque;
                            taskbar.Style = WinElement.Styles.Taskbar7Opaque;
                        }
                        else
                        {
                            start.Style = WinElement.Styles.StartVistaOpaque;
                            taskbar.Style = WinElement.Styles.TaskbarVistaOpaque;
                        }

                        ActionCenter.Visible = false;
                    }
                    else if (value == Preview_Enum.W7Basic)
                    {
                        if (!WinVista)
                        {
                            start.Style = WinElement.Styles.Start7Basic;
                            taskbar.Style = WinElement.Styles.Taskbar7Basic;
                        }
                        else
                        {
                            start.Style = WinElement.Styles.StartVistaBasic;
                            taskbar.Style = WinElement.Styles.TaskbarVistaBasic;
                        }

                        ActionCenter.Visible = false;
                    }
                    else if (value == Preview_Enum.WXP)
                    {
                        start.Style = WinElement.Styles.StartXP;
                        taskbar.Style = WinElement.Styles.TaskbarXP;

                        ActionCenter.Visible = false;
                    }

                    foreach (Window window in this.GetAllControls().OfType<Window>()) window.Preview = value;
                }
            }
        }


        private bool _darkMode_App = true;
        /// <summary>
        /// Dark mode for applications
        /// </summary>
        public bool DarkMode_App
        {
            get => _darkMode_App;
            set
            {
                if (value != _darkMode_App)
                {
                    _darkMode_App = value;
                    foreach (Window window in this.GetAllControls().OfType<Window>()) window.DarkMode = value;
                    Label8.ForeColor = value ? Color.White : Color.Black;
                    RefreshColors();
                }
            }
        }


        private bool _darkMode_Win = true;
        /// <summary>
        /// Dark mode for windows
        /// </summary>
        public bool DarkMode_Win
        {
            get => _darkMode_Win;
            set
            {
                if (value != _darkMode_Win)
                {
                    _darkMode_Win = value;
                    foreach (WinElement element in this.GetAllControls().OfType<WinElement>()) element.DarkMode = value;
                    RefreshColors();
                }
            }
        }


        private bool _winVista = false;
        /// <summary>
        /// Determine if Windows 7 windows preview will be manipulated a bit to simulate Windows Vista
        /// </summary>
        public bool WinVista
        {
            get => _winVista;
            set
            {
                if (value != _winVista)
                {
                    _winVista = value;
                    foreach (Window window in this.GetAllControls().OfType<Window>()) window.WinVista = value;

                    switch (_preview)
                    {
                        case Preview_Enum.W7Aero:
                            {
                                if (!WinVista)
                                {
                                    start.Style = WinElement.Styles.Start7Aero;
                                    taskbar.Style = WinElement.Styles.Taskbar7Aero;
                                }
                                else
                                {
                                    start.Style = WinElement.Styles.StartVistaAero;
                                    taskbar.Style = WinElement.Styles.TaskbarVistaAero;
                                }


                                ActionCenter.Visible = false;

                                break;
                            }
                        case Preview_Enum.W7Opaque:
                            {
                                if (!WinVista)
                                {
                                    start.Style = WinElement.Styles.Start7Opaque;
                                    taskbar.Style = WinElement.Styles.Taskbar7Opaque;
                                }
                                else
                                {
                                    start.Style = WinElement.Styles.StartVistaOpaque;
                                    taskbar.Style = WinElement.Styles.TaskbarVistaOpaque;
                                }

                                ActionCenter.Visible = false;

                                break;
                            }
                        case Preview_Enum.W7Basic:
                            {
                                if (!WinVista)
                                {
                                    start.Style = WinElement.Styles.Start7Basic;
                                    taskbar.Style = WinElement.Styles.Taskbar7Basic;
                                }
                                else
                                {
                                    start.Style = WinElement.Styles.StartVistaBasic;
                                    taskbar.Style = WinElement.Styles.TaskbarVistaBasic;
                                }

                                ActionCenter.Visible = false;

                                break;
                            }
                    }
                }
            }
        }


        private bool _classic = false;
        /// <summary>
        /// Determine if the classic theme will be used
        /// </summary>
        public bool Classic
        {
            get { return _classic; }
            set
            {
                if (_classic != value)
                {
                    _classic = value;
                    tabs_preview.SelectedIndex = !value ? 0 : 1;
                    WXP_Alert.Visible = _windowStyle == WindowStyle.WXP && Program.ClassicThemeRunning;
                }
            }
        }


        private int _win7Alpha = 100;
        /// <summary>
        /// Windows 7 transparency level
        /// </summary>
        public int Win7Alpha
        {
            get { return _win7Alpha; }
            set
            {
                if (_win7Alpha != value)
                {
                    _win7Alpha = value;
                    foreach (Window window in this.GetAllControls().OfType<Window>()) window.Win7Alpha = value;
                    foreach (WinElement element in this.GetAllControls().OfType<WinElement>()) element.BackColorAlpha = value;
                }
            }
        }


        private int _win7ColorBal = 100;
        /// <summary>
        /// Windows 7 color balance
        /// </summary>
        public int Win7ColorBal
        {
            get { return _win7ColorBal; }
            set
            {
                if (_win7ColorBal != value)
                {
                    _win7ColorBal = value;
                    foreach (Window window in this.GetAllControls().OfType<Window>()) window.Win7ColorBal = value;
                    foreach (WinElement element in this.GetAllControls().OfType<WinElement>()) element.Win7ColorBal = value;
                }
            }
        }

        /// <summary>
        /// Windows 7 glow balance
        /// </summary>
        public int Win7GlowBal
        {
            get { return _win7GlowBal; }
            set
            {
                if (_win7GlowBal != value)
                {
                    _win7GlowBal = value;
                    foreach (Window window in this.GetAllControls().OfType<Window>()) window.Win7GlowBal = value;
                    foreach (WinElement element in this.GetAllControls().OfType<WinElement>()) element.Win7GlowBal = value;
                }
            }
        }
        private int _win7GlowBal = 100;


        private float _Win7Noise = 0f;
        /// <summary>
        /// Windows 7 noise power
        /// </summary>
        public float Win7Noise
        {
            get => _Win7Noise;
            set
            {
                if (value != _Win7Noise)
                {
                    _Win7Noise = value;
                    foreach (Window window in this.GetAllControls().OfType<Window>()) window.Win7Noise = value;
                    foreach (WinElement element in this.GetAllControls().OfType<WinElement>()) element.NoisePower = value;
                }
            }
        }

        #endregion

        #region Window Properties

        private bool _shadow = true;
        /// <summary>
        /// Enable shadow for windows
        /// </summary>
        public bool Shadow
        {
            get { return _shadow; }
            set
            {
                if (_shadow != value || ForceRefresh)
                {
                    _shadow = value;
                    foreach (Window window in this.GetAllControls().OfType<Window>()) window.Shadow = value;
                }
            }
        }


        private bool _AccentColor_Enabled = false;
        /// <summary>
        /// Enable accent color for titlebar
        /// </summary>
        public bool TitlebarColor_Enabled
        {
            get => _AccentColor_Enabled;
            set
            {
                if (value != _AccentColor_Enabled || ForceRefresh)
                {
                    _AccentColor_Enabled = value;
                    foreach (Window window in this.GetAllControls().OfType<Window>()) window.AccentColor_Enabled = value;
                }
            }
        }


        private Color _titlebarColor_Active = Color.FromArgb(0, 120, 212);
        /// <summary>
        /// Active titlebar color
        /// </summary>
        public Color TitlebarColor_Active
        {
            get { return _titlebarColor_Active; }
            set
            {
                if (_titlebarColor_Active != value || ForceRefresh)
                {
                    _titlebarColor_Active = value;

                    foreach (Window window in this.GetAllControls().OfType<Window>()) window.AccentColor_Active = value;

                    if (WindowStyle == WindowStyle.W81 || WindowStyle == WindowStyle.W8 || WindowStyle == WindowStyle.W7 || WindowStyle == WindowStyle.WVista)
                    {
                        foreach (WinElement element in this.GetAllControls().OfType<WinElement>()) element.Background = value;
                    }
                }
            }
        }


        private Color _titlebarColor_Inactive = Color.FromArgb(32, 32, 32);
        /// <summary>
        /// Inactive titlebar color
        /// </summary>
        public Color TitlebarColor_Inactive
        {
            get { return _titlebarColor_Inactive; }
            set
            {
                if (_titlebarColor_Inactive != value || ForceRefresh)
                {
                    _titlebarColor_Inactive = value;
                    foreach (Window window in this.GetAllControls().OfType<Window>()) window.AccentColor_Inactive = value;
                }
            }
        }

        private Color _afterglowColor_Active = Color.FromArgb(0, 120, 212);
        /// <summary>
        /// Active afterglow color
        /// </summary>
        public Color AfterGlowColor_Active
        {
            get { return _afterglowColor_Active; }
            set
            {
                if (_afterglowColor_Active != value || ForceRefresh)
                {
                    _afterglowColor_Active = value;
                    foreach (Window window in this.GetAllControls().OfType<Window>()) window.AccentColor2_Active = value;

                    if (WindowStyle == WindowStyle.W81 || WindowStyle == WindowStyle.W8 || WindowStyle == WindowStyle.W7 || WindowStyle == WindowStyle.WVista)
                    {
                        foreach (WinElement element in this.GetAllControls().OfType<WinElement>()) element.Background2 = value;
                    }
                }
            }
        }


        private Color _afterglowColor2_Inactive = Color.FromArgb(32, 32, 32);
        /// <summary>
        /// Inactive afterglow color
        /// </summary>
        public Color AfterGlowColor_Inactive
        {
            get { return _afterglowColor2_Inactive; }
            set
            {
                if (_afterglowColor2_Inactive != value || ForceRefresh)
                {
                    _afterglowColor2_Inactive = value;
                    foreach (Window window in this.GetAllControls().OfType<Window>()) window.AccentColor2_Inactive = value;
                }
            }
        }


        private int _Metrics_CaptionHeight = 22;
        /// <summary>
        /// Titlebar height in the preview
        /// </summary>
        public int Metrics_CaptionHeight
        {
            get => _Metrics_CaptionHeight;
            set
            {
                if (value != _Metrics_CaptionHeight)
                {
                    _Metrics_CaptionHeight = value;
                    foreach (Window window in this.GetAllControls().OfType<Window>()) window.Metrics_CaptionHeight = value;
                    foreach (WindowR windowR in this.GetAllControls().OfType<WindowR>()) windowR.Metrics_CaptionHeight = value;
                }
            }
        }


        private int _Metrics_CaptionWidth = 22;
        /// <summary>
        /// Titlebar buttons width in the preview if classic theme is enabled
        /// </summary>
        public int Metrics_CaptionWidth
        {
            get => _Metrics_CaptionWidth;
            set
            {
                if (value != _Metrics_CaptionWidth)
                {
                    _Metrics_CaptionWidth = value;
                    foreach (WindowR windowR in this.GetAllControls().OfType<WindowR>()) windowR.Metrics_CaptionWidth = value;
                }
            }
        }


        private int _Metrics_BorderWidth = 1;
        /// <summary>
        /// Border width in the preview
        /// </summary>
        public int Metrics_BorderWidth
        {
            get => _Metrics_BorderWidth;
            set
            {
                if (value != _Metrics_BorderWidth)
                {
                    _Metrics_BorderWidth = value;
                    foreach (Window window in this.GetAllControls().OfType<Window>()) window.Metrics_BorderWidth = value;
                    foreach (WindowR windowR in this.GetAllControls().OfType<WindowR>()) windowR.Metrics_BorderWidth = value;
                }
            }
        }


        private int _Metrics_PaddedBorderWidth = 4;
        /// <summary>
        /// Padded border width in the preview
        /// </summary>
        public int Metrics_PaddedBorderWidth
        {
            get => _Metrics_PaddedBorderWidth;
            set
            {
                if (value != _Metrics_PaddedBorderWidth)
                {
                    _Metrics_PaddedBorderWidth = value;
                    foreach (Window window in this.GetAllControls().OfType<Window>()) window.Metrics_PaddedBorderWidth = value;
                    foreach (WindowR windowR in this.GetAllControls().OfType<WindowR>()) windowR.Metrics_PaddedBorderWidth = value;
                }
            }
        }


        private Font _Metrics_CaptionFont = new("Segoe UI", 9f);
        /// <summary>
        /// Titlebar font in the preview
        /// </summary>
        public Font Metrics_CaptionFont
        {
            get => _Metrics_CaptionFont;
            set
            {
                if (value != _Metrics_CaptionFont)
                {
                    _Metrics_CaptionFont = value;
                    foreach (Window window in this.GetAllControls().OfType<Window>()) window.Font = value;
                    foreach (WindowR windowR in this.GetAllControls().OfType<WindowR>()) windowR.Font = value;

                    ButtonR3.Font = new(value.Name, 8f, ButtonR3.Font.Style);
                    ButtonR4.Font = new(value.Name, 8f, ButtonR4.Font.Style);
                    ButtonR2.Font = new(value.Name, 8.5f, ButtonR2.Font.Style);
                }
            }
        }

        #endregion

        #region Elements Properties

        bool ForceRefresh = false;


        private bool _Transparency = true;
        /// <summary>
        /// Enable transparency for elements
        /// </summary>
        public bool Transparency
        {
            get => _Transparency;
            set
            {
                if (value != _Transparency || ForceRefresh)
                {
                    _Transparency = value;
                    foreach (WinElement element in this.GetAllControls().OfType<WinElement>()) element.Transparency = value;
                    RefreshColors();
                }
            }
        }


        private bool _increaseTBTransparency = false;
        /// <summary>
        /// Increase transparency for taskbar in Windows 10 only
        /// </summary>
        public bool IncreaseTBTransparency
        {
            get => _increaseTBTransparency;
            set
            {
                if (value != _increaseTBTransparency)
                {
                    _increaseTBTransparency = value;

                    if (Preview == Preview_Enum.W10 || Preview == Preview_Enum.W10Lite)
                        taskbar.BlurPower = !value ? 12 : 6;
                }
            }
        }


        private bool _tb_Blur = false;
        /// <summary>
        /// Enable blur for taskbar in Windows 10 only
        /// </summary>
        public bool TB_Blur
        {
            get => _tb_Blur;
            set
            {
                if (value != _tb_Blur)
                {
                    _tb_Blur = value;

                    if (Preview == Preview_Enum.W10 || Preview == Preview_Enum.W10Lite)
                    {
                        byte TB_Blur_value;

                        if (!TB_Blur)
                        {
                            TB_Blur_value = 0;
                        }
                        else
                        {
                            TB_Blur_value = (byte)(!IncreaseTBTransparency ? 12 : 8);
                        }

                        taskbar.BlurPower = TB_Blur_value;
                    }
                }
            }
        }


        private Color _color1;
        /// <summary>
        /// Color 1 for Windows 10/11/12 taskbar, start menu and action center
        /// </summary>
        public Color Color1
        {
            get => _color1;
            set
            {
                if (value != _color1 || ForceRefresh)
                {
                    _color1 = value;

                    if (WindowStyle == WindowStyle.W11 || WindowStyle == WindowStyle.W12)
                    {
                        if (ExplorerPatcher.CanBeUsed)
                        {
                            if (DarkMode_Win)
                            {
                                if (_accentLevel == AccentTaskbarLevels.None)
                                {
                                    taskbar.Background = Win11_color;
                                    ActionCenter.Background = Win11_color;
                                    start.Background = Win11_color;
                                }
                                else if (_accentLevel == AccentTaskbarLevels.Taskbar)
                                {
                                    taskbar.Background = value;
                                    ActionCenter.Background = Win11_color;
                                    start.Background = Win11_color;
                                }
                                else if (_accentLevel == AccentTaskbarLevels.Taskbar_Start_AC)
                                {
                                    ActionCenter.Background = value;
                                    taskbar.Background = value;
                                    if ((bool)!Program.EP?.UseStart10) start.Background = value; else start.Background = Win11_color;
                                }
                            }
                            else
                            {
                                lnk_preview.ForeColor = value;
                                ActionCenter.ActionCenterButton_Hover = value;
                            }
                        }

                        else if (DarkMode_Win)
                        {
                            if (_accentLevel == AccentTaskbarLevels.None)
                            {
                                taskbar.Background = Win11_color;
                                ActionCenter.Background = Win11_color;
                                start.Background = Win11_color;
                            }
                            else if (_accentLevel == AccentTaskbarLevels.Taskbar)
                            {
                                taskbar.Background = value;
                                ActionCenter.Background = Win11_color;
                                start.Background = Win11_color;
                            }
                            else if (_accentLevel == AccentTaskbarLevels.Taskbar_Start_AC)
                            {
                                ActionCenter.Background = value;
                                taskbar.Background = value;
                                start.Background = value;
                            }
                        }
                        else
                        {
                            taskbar.Background = _accentLevel != AccentTaskbarLevels.None ? value : Win11_color;
                            lnk_preview.ForeColor = value;
                            ActionCenter.ActionCenterButton_Hover = value;
                        }
                    }

                    else if (WindowStyle == WindowStyle.W10)
                    {
                        if (!Transparency)
                            taskbar.Background = _accentLevel == AccentTaskbarLevels.None ? Win10_taskbarColor : value;

                        else { } // ColorControls_List.Add(start) ''Hamburger
                    }
                }
            }
        }


        private Color _color2;
        /// <summary>
        /// Color 2 for Windows 10/11/12 taskbar, start menu and action center
        /// </summary>
        public Color Color2
        {
            get => _color2;
            set
            {
                if (value != _color2 || ForceRefresh)
                {
                    _color2 = value;

                    if (WindowStyle == WindowStyle.W11 || WindowStyle == WindowStyle.W12)
                    {
                        if (!DarkMode_Win)
                        {
                            if (_accentLevel == AccentTaskbarLevels.None)
                            {
                                ActionCenter.Background = Win11_color;
                                start.Background = Win11_color;
                            }
                            else if (_accentLevel == AccentTaskbarLevels.Taskbar)
                            {
                                ActionCenter.Background = Win11_color;
                                start.Background = Win11_color;
                            }
                            else if (_accentLevel == AccentTaskbarLevels.Taskbar_Start_AC)
                            {
                                ActionCenter.Background = value;
                                start.Background = value;
                            }
                        }
                        else
                        {
                            lnk_preview.ForeColor = value;
                            ActionCenter.ActionCenterButton_Hover = value;
                        }
                    }

                    else if (WindowStyle == WindowStyle.W10)
                    {
                        if (DarkMode_Win || _accentLevel == AccentTaskbarLevels.Taskbar_Start_AC)
                            ActionCenter.LinkColor = value;
                    }
                }
            }
        }


        private Color _color3;
        /// <summary>
        /// Color 3 for Windows 10/11/12 taskbar, start menu and action center
        /// </summary>
        public Color Color3
        {
            get => _color3;
            set
            {
                if (value != _color3 || ForceRefresh)
                {
                    _color3 = value;

                    if (WindowStyle == WindowStyle.W11 || WindowStyle == WindowStyle.W12)
                    {
                        if (ExplorerPatcher.CanBeUsed)
                        {
                            taskbar.Background = _accentLevel == AccentTaskbarLevels.None ? Win11_color : value;

                            if (DarkMode_Win)
                            {
                                taskbar.AppUnderline = value;
                                ActionCenter.ActionCenterButton_Normal = value;
                            }
                            else
                            {
                                taskbar.AppUnderline = value;
                            }
                        }
                        else if (DarkMode_Win)
                        {
                            taskbar.AppUnderline = value;
                            ActionCenter.ActionCenterButton_Normal = value;
                        }
                    }

                    else if (WindowStyle == WindowStyle.W10)
                    {
                        if (DarkMode_Win || _accentLevel != AccentTaskbarLevels.None)
                            taskbar.AppUnderline = value;
                    }
                }
            }
        }


        private Color _color4;
        /// <summary>
        /// Color 4 for Windows 10/11/12 taskbar, start menu and action center
        /// </summary>
        public Color Color4
        {
            get => _color4;
            set
            {
                if (value != _color4 || ForceRefresh)
                {
                    _color4 = value;

                    if (WindowStyle == WindowStyle.W11 || WindowStyle == WindowStyle.W12)
                    {
                        if (ExplorerPatcher.CanBeUsed)
                        {
                            if ((bool)Program.EP?.UseStart10)
                            {
                                if (_accentLevel == AccentTaskbarLevels.None)
                                {
                                    start.Background = Win11_color;
                                }
                                else if (_accentLevel == AccentTaskbarLevels.Taskbar)
                                {
                                    start.Background = Win11_color;
                                }
                                else if (_accentLevel == AccentTaskbarLevels.Taskbar_Start_AC)
                                {
                                    start.Background = value;
                                }
                            }

                            else if (!DarkMode_Win)
                            {
                                ActionCenter.ActionCenterButton_Normal = value;
                            }
                            else
                            {
                                start.StartColor = value;
                            }
                        }

                        else if (!DarkMode_Win)
                        {
                            ActionCenter.ActionCenterButton_Normal = value;
                            taskbar.AppUnderline = value;
                        }
                        else
                        {
                            start.StartColor = value;
                        }
                    }

                    else if (WindowStyle == WindowStyle.W10)
                    {
                        if (DarkMode_Win)
                        {
                            if (_accentLevel == AccentTaskbarLevels.Taskbar_Start_AC)
                            {
                                ActionCenter.Background = value;
                                start.Background = value;
                            }
                            else
                            {
                                ActionCenter.Background = Win10_actionCenterColor;
                                start.Background = Win10_startColor;
                            }

                            if (_accentLevel != AccentTaskbarLevels.None)
                            {
                                if (Transparency)
                                {
                                    taskbar.AppBackground = _color5;
                                }
                                else
                                {
                                    taskbar.AppBackground = value;
                                }
                            }
                            else
                            {
                                taskbar.AppBackground = Win10_taskbarAppBackgroundColor;
                            }
                        }

                        else
                        {
                            start.Background = _accentLevel == AccentTaskbarLevels.Taskbar_Start_AC ? value : Win10_startColor;
                            ActionCenter.Background = _accentLevel == AccentTaskbarLevels.Taskbar_Start_AC ? value : Win10_actionCenterColor;

                            if (!Transparency) taskbar.AppBackground = value;
                        }
                    }
                }
            }
        }


        private Color _color5;
        /// <summary>
        /// Color 5 for Windows 10/11/12 taskbar, start menu and action center
        /// </summary>
        public Color Color5
        {
            get => _color5;
            set
            {
                if (value != _color5 || ForceRefresh)
                {
                    _color5 = value;

                    if (WindowStyle == WindowStyle.W11 || WindowStyle == WindowStyle.W12)
                    {
                        if (!DarkMode_Win) taskbar.AppUnderline = value;

                        setting_icon_preview.ForeColor = value;
                    }

                    else if (WindowStyle == WindowStyle.W10)
                    {
                        if (DarkMode_Win)
                        {
                            setting_icon_preview.ForeColor = value;
                            lnk_preview.ForeColor = value;
                            ActionCenter.ActionCenterButton_Normal = value;

                            if (_accentLevel != AccentTaskbarLevels.None)
                            {
                                if (Transparency)
                                {
                                    taskbar.AppBackground = value;
                                }
                                else
                                {
                                    taskbar.AppBackground = _color4;
                                }
                            }
                            else
                            {
                                taskbar.AppBackground = Win10_taskbarAppBackgroundColor;
                            }
                        }
                        else
                        {
                            setting_icon_preview.ForeColor = value;
                            lnk_preview.ForeColor = value;
                            ActionCenter.ActionCenterButton_Normal = value;

                            if (Transparency)
                            {
                                if (_accentLevel == AccentTaskbarLevels.None)
                                {
                                    taskbar.AppBackground = Win10_taskbarAppBackgroundColor;
                                }
                                else
                                {
                                    taskbar.AppBackground = value;
                                }
                            }
                            else if (_accentLevel == AccentTaskbarLevels.None)
                            {
                                taskbar.AppBackground = Win10_taskbarAppBackgroundColor;
                            }

                            if (_accentLevel == AccentTaskbarLevels.None)
                            {
                                taskbar.AppUnderline = value;
                            }
                            else
                            {
                                taskbar.AppUnderline = _color3;
                            }
                        }
                    }
                }
            }
        }


        private Color _color6;
        /// <summary>
        /// Color 6 for Windows 10/11/12 taskbar, start menu and action center
        /// </summary>
        public Color Color6
        {
            get => _color6;
            set
            {
                if (value != _color6 || ForceRefresh)
                {
                    _color6 = value;

                    if (WindowStyle == WindowStyle.W11 || WindowStyle == WindowStyle.W12)
                    {
                        ActionCenter.ActionCenterButton_Pressed = value;
                    }

                    else if (WindowStyle == WindowStyle.W10)
                    {
                        taskbar.StartColor = value;
                    }
                }
            }
        }


        private Color _color7;
        /// <summary>
        /// Color 7 for Windows 10/11/12 taskbar, start menu and action center
        /// </summary>
        public Color Color7
        {
            get => _color7;
            set
            {
                if (value != _color7 || ForceRefresh)
                {
                    _color7 = value;

                    //if (WindowStyle == WindowStyle.W11 || WindowStyle == WindowStyle.W12)
                    //{

                    //}

                    //else if (WindowStyle == WindowStyle.W10)
                    //{

                    //}
                }
            }
        }


        private Color _color8;
        /// <summary>
        /// Color 8 for Windows 10/11/12 taskbar, start menu and action center
        /// </summary>
        public Color Color8
        {
            get => _color8;
            set
            {
                if (value != _color8 || ForceRefresh)
                {
                    _color8 = value;

                    if (WindowStyle == WindowStyle.W11 || WindowStyle == WindowStyle.W12)
                    {
                        taskbar.AppBackground = value;
                    }

                    else if (WindowStyle == WindowStyle.W10)
                    {
                        if (Transparency)
                            taskbar.Background = _accentLevel != AccentTaskbarLevels.None ? value : Win10_taskbarColor;

                        if (!DarkMode_Win && Transparency && _accentLevel != AccentTaskbarLevels.Taskbar_Start_AC)
                            ActionCenter.LinkColor = value;
                    }
                }
            }
        }


        private Color _color9;
        /// <summary>
        /// Color 9 for Windows 10/11/12 taskbar, start menu and action center
        /// </summary>
        public Color Color9
        {
            get => _color9;
            set
            {
                if (value != _color9 || ForceRefresh)
                {
                    _color9 = value;

                    //if (WindowStyle == WindowStyle.W11 || WindowStyle == WindowStyle.W12)
                    //{

                    //}

                    //else if (WindowStyle == WindowStyle.W10)
                    //{

                    //}
                }
            }
        }


        private AccentTaskbarLevels _accentLevel = AccentTaskbarLevels.None;
        /// <summary>
        /// Accent level for Windows 10/11/12 taskbar, start menu and action center
        /// </summary>
        public AccentTaskbarLevels AccentLevel
        {
            get => _accentLevel;
            set
            {
                if (value != _accentLevel || ForceRefresh)
                {
                    _accentLevel = value;
                    RefreshColors();
                }
            }
        }


        private bool _useWin11ORB_WithWin10 = false;
        /// <summary>
        /// Use Windows 11 start button with Windows 10 (ExplorerPatcher preview simulation option)
        /// </summary>
        public bool UseWin11ORB_WithWin10
        {
            get => _useWin11ORB_WithWin10;
            set
            {
                if (value != _useWin11ORB_WithWin10 || ForceRefresh)
                {
                    _useWin11ORB_WithWin10 = value;
                    taskbar.UseWin11ORB_WithWin10 = value;
                }
            }
        }


        private bool _useWin11RoundedCorners_WithWin10_Level1 = false;
        /// <summary>
        /// Use Windows 10 rounded corners start menu with Windows 11 (ExplorerPatcher preview simulation option)
        /// </summary>
        public bool UseWin11RoundedCorners_WithWin10_Level1
        {
            get => _useWin11RoundedCorners_WithWin10_Level1;
            set
            {
                if (value != _useWin11RoundedCorners_WithWin10_Level1 || ForceRefresh)
                {
                    _useWin11RoundedCorners_WithWin10_Level1 = value;
                    start.UseWin11RoundedCorners_WithWin10_Level1 = value;
                }
            }
        }


        private bool _useWin11RoundedCorners_WithWin10_Level2 = false;
        /// <summary>
        /// Use Windows 11 rounded corners start menu with Windows 10 (ExplorerPatcher preview simulation option)
        /// </summary>
        public bool UseWin11RoundedCorners_WithWin10_Level2
        {
            get => _useWin11RoundedCorners_WithWin10_Level2;
            set
            {
                if (value != _useWin11RoundedCorners_WithWin10_Level2 || ForceRefresh)
                {
                    _useWin11RoundedCorners_WithWin10_Level2 = value;
                    start.UseWin11RoundedCorners_WithWin10_Level2 = value;
                }
            }
        }

        #endregion

        #region Classic colors

        /// <summary>
        /// Enable titlebar gradience
        /// </summary>
        public bool EnableGradient
        {
            get { return enableGradient; }
            set
            {
                if (enableGradient != value)
                {
                    enableGradient = value;
                    foreach (WindowR window in this.GetAllControls().OfType<WindowR>()) { window.ColorGradient = value; }
                }
            }
        }
        private bool enableGradient = true;


        /// <summary>
        /// Color of active window border
        /// </summary>
        public Color ActiveBorder
        {
            get { return activeBorder; }
            set
            {
                if (activeBorder != value)
                {
                    activeBorder = value;

                    foreach (WindowR window in this.GetAllControls().OfType<WindowR>())
                    {
                        if (window.Active) window.ColorBorder = value;
                    }

                    foreach (ContextMenuR contextMenu in this.GetAllControls().OfType<ContextMenuR>()) { contextMenu.ColorBorder = value; }
                }
            }
        }
        private Color activeBorder;


        /// <summary>
        /// Active titlebar main color
        /// </summary>
        public Color ActiveTitle
        {
            get { return activeTitle; }
            set
            {
                if (activeTitle != value)
                {
                    activeTitle = value;

                    foreach (WindowR window in this.GetAllControls().OfType<WindowR>())
                    {
                        if (window.Active) window.Color1 = value;
                    }
                }
            }
        }
        private Color activeTitle;


        /// <summary>
        /// Modern Windows desktop solid color wallpaper
        /// </summary>
        public Color Background
        {
            get { return background; }
            set
            {
                if (background != value)
                {
                    background = value;
                    BackColor = value;
                }
            }
        }
        private Color background;


        /// <summary>
        /// Button face
        /// </summary>
        public Color ButtonFace
        {
            get { return buttonFace; }
            set
            {
                if (buttonFace != value)
                {
                    buttonFace = value;

                    foreach (ButtonR button in this.GetAllControls().OfType<ButtonR>())
                    {
                        if (button.Parent != null && button.Name.ToLower() != "closebtn" && button.Name.ToLower() != "maxbtn" && button.Name.ToLower() != "minbtn")
                            button.BackColor = value;
                    }

                    foreach (WindowR window in this.GetAllControls().OfType<WindowR>()) { window.BackColor = value; }
                    foreach (ScrollBarR scrollBar in this.GetAllControls().OfType<ScrollBarR>()) { scrollBar.BackColor = value; }
                    foreach (ContextMenuR contextMenu in this.GetAllControls().OfType<ContextMenuR>()) { contextMenu.BackColor = value; }
                    foreach (MenuBarR menu in this.GetAllControls().OfType<MenuBarR>()) { menu.BackColor = value; }
                    foreach (PanelRaisedR panelRaisedR in this.GetAllControls().OfType<PanelRaisedR>()) { panelRaisedR.BackColor = value; }
                }
            }
        }
        private Color buttonFace;


        /// <summary>
        /// Button shadow
        /// </summary>
        public Color ButtonShadow
        {
            get { return buttonShadow; }
            set
            {
                if (buttonShadow != value)
                {
                    buttonShadow = value;

                    foreach (ButtonR button in this.GetAllControls().OfType<ButtonR>())
                    {
                        if (button.Parent != null && button.Name.ToLower() != "closebtn" && button.Name.ToLower() != "maxbtn" && button.Name.ToLower() != "minbtn")
                            button.ButtonShadow = value;
                    }

                    foreach (WindowR window in this.GetAllControls().OfType<WindowR>()) { window.ButtonShadow = value; }
                    foreach (PanelRaisedR panelRaised in this.GetAllControls().OfType<PanelRaisedR>()) { panelRaised.ButtonShadow = value; }
                    foreach (TextBoxR textBox in this.GetAllControls().OfType<TextBoxR>()) { textBox.ButtonShadow = value; }
                    foreach (PanelR panel in this.GetAllControls().OfType<PanelR>()) { panel.ButtonShadow = value; }
                    foreach (ContextMenuR contextMenu in this.GetAllControls().OfType<ContextMenuR>()) { contextMenu.ButtonShadow = value; }
                    foreach (MenuBarR menuBar in this.GetAllControls().OfType<MenuBarR>()) { menuBar.ButtonShadow = value; }
                    foreach (WindowControlR windowControl in this.GetAllControls().OfType<WindowControlR>()) { windowControl.ButtonShadow = value; }
                    foreach (PanelRaisedR panelRaisedR in this.GetAllControls().OfType<PanelRaisedR>()) { panelRaisedR.ButtonShadow = value; }
                }
            }
        }
        private Color buttonShadow;


        /// <summary>
        /// Button dark shadow
        /// </summary>
        public Color ButtonDkShadow
        {
            get { return buttonDkShadow; }
            set
            {
                if (buttonDkShadow != value)
                {
                    buttonDkShadow = value;

                    foreach (ButtonR button in this.GetAllControls().OfType<ButtonR>())
                    {
                        if (button.Parent != null && button.Name.ToLower() != "closebtn" && button.Name.ToLower() != "maxbtn" && button.Name.ToLower() != "minbtn")
                            button.ButtonDkShadow = value;
                    }

                    foreach (WindowR window in this.GetAllControls().OfType<WindowR>()) { window.ButtonDkShadow = value; }
                    foreach (TextBoxR textBox in this.GetAllControls().OfType<TextBoxR>()) { textBox.ButtonDkShadow = value; }
                    foreach (ContextMenuR contextMenu in this.GetAllControls().OfType<ContextMenuR>()) { contextMenu.ButtonDkShadow = value; }
                    foreach (WindowControlR windowControl in this.GetAllControls().OfType<WindowControlR>()) { windowControl.ButtonDkShadow = value; }
                    foreach (PanelRaisedR panelRaisedR in this.GetAllControls().OfType<PanelRaisedR>()) { panelRaisedR.ButtonDkShadow = value; }
                }
            }
        }
        private Color buttonDkShadow;


        /// <summary>
        /// Button hilight
        /// </summary>
        public Color ButtonHilight
        {
            get { return buttonHilight; }
            set
            {
                if (buttonHilight != value)
                {
                    buttonHilight = value;

                    foreach (ButtonR button in this.GetAllControls().OfType<ButtonR>())
                    {
                        if (button.Parent != null && button.Name.ToLower() != "closebtn" && button.Name.ToLower() != "maxbtn" && button.Name.ToLower() != "minbtn")
                            button.ButtonHilight = value;
                    }

                    foreach (WindowR window in this.GetAllControls().OfType<WindowR>()) { window.ButtonHilight = value; }
                    foreach (PanelRaisedR panelRaised in this.GetAllControls().OfType<PanelRaisedR>()) { panelRaised.ButtonHilight = value; }
                    foreach (TextBoxR textBox in this.GetAllControls().OfType<TextBoxR>()) { textBox.ButtonHilight = value; }
                    foreach (ScrollBarR scrollBar in this.GetAllControls().OfType<ScrollBarR>()) { scrollBar.ButtonHilight = value; }
                    foreach (PanelR panel in this.GetAllControls().OfType<PanelR>()) { panel.ButtonHilight = value; }
                    foreach (ContextMenuR contextMenu in this.GetAllControls().OfType<ContextMenuR>()) { contextMenu.ButtonHilight = value; }
                    foreach (MenuBarR menuBar in this.GetAllControls().OfType<MenuBarR>()) { menuBar.ButtonHilight = value; }
                    foreach (WindowControlR windowControl in this.GetAllControls().OfType<WindowControlR>()) { windowControl.ButtonHilight = value; }
                    foreach (PanelRaisedR panelRaisedR in this.GetAllControls().OfType<PanelRaisedR>()) { panelRaisedR.ButtonHilight = value; }
                }
            }
        }
        private Color buttonHilight;


        /// <summary>
        /// Button light
        /// </summary>
        public Color ButtonLight
        {
            get { return buttonLight; }
            set
            {
                if (buttonLight != value)
                {
                    buttonLight = value;

                    foreach (ButtonR button in this.GetAllControls().OfType<ButtonR>())
                    {
                        if (button.Parent != null && button.Name.ToLower() != "closebtn" && button.Name.ToLower() != "maxbtn" && button.Name.ToLower() != "minbtn")
                            button.ButtonLight = value;
                    }

                    foreach (WindowR window in this.GetAllControls().OfType<WindowR>()) { window.ButtonLight = value; }
                    foreach (TextBoxR textBox in this.GetAllControls().OfType<TextBoxR>()) { textBox.ButtonLight = value; }
                    foreach (ContextMenuR contextMenu in this.GetAllControls().OfType<ContextMenuR>()) { contextMenu.ButtonLight = value; }
                    foreach (WindowControlR windowControl in this.GetAllControls().OfType<WindowControlR>()) { windowControl.ButtonLight = value; }
                    foreach (PanelRaisedR panelRaisedR in this.GetAllControls().OfType<PanelRaisedR>()) { panelRaisedR.ButtonLight = value; }
                }
            }
        }
        private Color buttonLight;


        /// <summary>
        /// Forecolor of button and windows
        /// </summary>
        public Color ButtonText
        {
            get { return buttonText; }
            set
            {
                if (buttonText != value)
                {
                    buttonText = value;

                    foreach (ButtonR button in this.GetAllControls().OfType<ButtonR>())
                    {
                        if (button.Parent != null && button.Name.ToLower() != "closebtn" && button.Name.ToLower() != "maxbtn" && button.Name.ToLower() != "minbtn")
                            button.ForeColor = value;
                    }

                    foreach (WindowR window in this.GetAllControls().OfType<WindowR>()) { window.ButtonText = value; }
                    foreach (PanelRaisedR panelRaisedR in this.GetAllControls().OfType<PanelRaisedR>()) { panelRaisedR.ForeColor = value; }
                }
            }
        }
        private Color buttonText;


        /// <summary>
        /// Second color for gradience in active titlebar
        /// </summary>
        public Color GradientActiveTitle
        {
            get { return gradientActiveTitle; }
            set
            {
                if (gradientActiveTitle != value)
                {
                    gradientActiveTitle = value;

                    foreach (WindowR window in this.GetAllControls().OfType<WindowR>())
                    {
                        if (window.Active) window.Color2 = value;
                    }
                }
            }
        }
        private Color gradientActiveTitle;


        /// <summary>
        /// Second color for gradience in inactive titlebar
        /// </summary>
        public Color GradientInactiveTitle
        {
            get { return gradientInactiveTitle; }
            set
            {
                if (gradientInactiveTitle != value)
                {
                    gradientInactiveTitle = value;

                    foreach (WindowR window in this.GetAllControls().OfType<WindowR>())
                    {
                        if (!window.Active) window.Color2 = value;
                    }
                }
            }
        }
        private Color gradientInactiveTitle;


        /// <summary>
        /// Used in disabled items
        /// </summary>
        public Color GrayText
        {
            get { return grayText; }
            set
            {
                if (grayText != value)
                {
                    grayText = value;

                    foreach (ContextMenuR contextMenu in this.GetAllControls().OfType<ContextMenuR>()) { contextMenu.GrayText = value; }
                    foreach (MenuBarR menuBar in this.GetAllControls().OfType<MenuBarR>()) { menuBar.GrayText = value; }
                }
            }
        }
        private Color grayText;


        /// <summary>
        /// Color of inactive window border
        /// </summary>
        public Color InactiveBorder
        {
            get { return inactiveBorder; }
            set
            {
                if (inactiveBorder != value)
                {
                    inactiveBorder = value;

                    foreach (WindowR window in this.GetAllControls().OfType<WindowR>())
                    {
                        if (!window.Active) window.ColorBorder = value;
                    }
                }
            }
        }
        private Color inactiveBorder;


        /// <summary>
        /// Inactive titlebar main color
        /// </summary>
        public Color InactiveTitle
        {
            get { return inactiveTitle; }
            set
            {
                if (inactiveTitle != value)
                {
                    inactiveTitle = value;

                    foreach (WindowR window in this.GetAllControls().OfType<WindowR>())
                    {
                        if (!window.Active) window.Color1 = value;
                    }
                }
            }
        }
        private Color inactiveTitle;


        /// <summary>
        /// Inactive titlebar text
        /// </summary>
        public Color InactiveTitleText
        {
            get { return inactiveTitleText; }
            set
            {
                if (inactiveTitleText != value)
                {
                    inactiveTitleText = value;

                    foreach (WindowR window in this.GetAllControls().OfType<WindowR>())
                    {
                        if (!window.Active) window.ForeColor = value;
                    }

                    foreach (Window window in this.GetAllControls().OfType<Window>())
                    {
                        if (!window.Active) window.ForeColor = value;
                    }
                }
            }
        }
        private Color inactiveTitleText;


        /// <summary>
        /// Active titlebar text
        /// </summary>
        public Color TitleText
        {
            get { return titleText; }
            set
            {
                if (titleText != value)
                {
                    titleText = value;

                    foreach (WindowR window in this.GetAllControls().OfType<WindowR>())
                    {
                        if (window.Active) window.ForeColor = value;
                    }

                    foreach (Window window in this.GetAllControls().OfType<Window>())
                    {
                        if (window.Active) window.ForeColor = value;
                    }
                }
            }
        }
        private Color titleText;


        /// <summary>
        /// Textbox and other controls BackColor
        /// </summary>
        public Color Window
        {
            get { return window; }
            set
            {
                if (window != value)
                {
                    window = value;
                    foreach (TextBoxR textBox in this.GetAllControls().OfType<TextBoxR>()) { textBox.BackColor = value; }
                    foreach (WindowControlR windowControl in this.GetAllControls().OfType<WindowControlR>()) { windowControl.BackColor = value; }
                }
            }
        }
        private Color window;


        /// <summary>
        /// Color of rectangle surrounding a pressed button
        /// </summary>
        public Color WindowFrame
        {
            get { return windowFrame; }
            set
            {
                if (windowFrame != value)
                {
                    windowFrame = value;

                    foreach (ButtonR button in this.GetAllControls().OfType<ButtonR>())
                    {
                        if (button.Parent != null && button.Name.ToLower() != "closebtn" && button.Name.ToLower() != "maxbtn" && button.Name.ToLower() != "minbtn")
                            button.WindowFrame = value;
                    }
                }
            }
        }
        private Color windowFrame;


        /// <summary>
        /// Textbox and other controls ForeColor
        /// </summary>
        public Color WindowText
        {
            get { return windowText; }
            set
            {
                if (windowText != value)
                {
                    windowText = value;
                    foreach (TextBoxR textBox in this.GetAllControls().OfType<TextBoxR>()) { textBox.ForeColor = value; }
                    foreach (LabelR label in this.GetAllControls().OfType<LabelR>()) { label.ForeColor = value; }
                    foreach (WindowControlR windowControl in this.GetAllControls().OfType<WindowControlR>()) { windowControl.ForeColor = value; }
                }
            }
        }
        private Color windowText;

        #endregion

        #region Methods

        private void SetStyles()
        {
            switch (_windowStyle)
            {
                case WindowStyle.W12:
                    {
                        Preview = VisualStyles == Theme.Structures.VisualStyles.DefaultVisualStyles.Aero || VisualStyles == Theme.Structures.VisualStyles.DefaultVisualStyles.AeroOpaque ? Preview_Enum.W11 : Preview_Enum.W11Lite;

                        if (OS.W12 || OS.W11) Program.EP = new();

                        if (ExplorerPatcher.CanBeUsed)
                        {
                            ref ExplorerPatcher EP = ref Program.EP;

                            if ((bool)!EP?.UseTaskbar10)
                            {
                                taskbar.Height = 42;
                                taskbar.NoisePower = 0.3f;
                            }
                            else
                            {
                                taskbar.Height = 35;
                                taskbar.NoisePower = 0f;
                                UseWin11ORB_WithWin10 = (bool)!EP?.TaskbarButton10;
                            }

                            if ((bool)!EP?.UseStart10)
                            {
                                start.BlurPower = 6;
                                start.NoisePower = 0.3f;
                                start.Size = new(135, 200);
                                start.Location = new(10, taskbar.Bottom - taskbar.Height - start.Height - 10);
                            }
                            else
                            {
                                start.BlurPower = 7;
                                start.NoisePower = 0.3f;

                                switch (EP?.StartStyle)
                                {
                                    case ExplorerPatcher.StartStyles.NotRounded:
                                        {
                                            start.Size = new(182, 201);
                                            start.Left = 0;
                                            start.Top = taskbar.Bottom - taskbar.Height - start.Height;
                                            UseWin11RoundedCorners_WithWin10_Level1 = false;
                                            UseWin11RoundedCorners_WithWin10_Level2 = false;
                                            break;
                                        }

                                    case ExplorerPatcher.StartStyles.RoundedCornersDockedMenu:
                                        {
                                            start.Size = new(182, 201);
                                            start.Left = 0;
                                            start.Top = taskbar.Bottom - taskbar.Height - start.Height;
                                            UseWin11RoundedCorners_WithWin10_Level1 = true;
                                            UseWin11RoundedCorners_WithWin10_Level2 = false;
                                            break;
                                        }

                                    case ExplorerPatcher.StartStyles.RoundedCornersFloatingMenu:
                                        {
                                            start.Size = new(182, 201);
                                            start.Location = new(10, taskbar.Bottom - taskbar.Height - start.Height - 10);
                                            UseWin11RoundedCorners_WithWin10_Level1 = false;
                                            UseWin11RoundedCorners_WithWin10_Level2 = true;
                                            break;
                                        }
                                }
                            }
                        }

                        else
                        {
                            taskbar.BlurPower = 8;
                            taskbar.Height = 42;
                            taskbar.NoisePower = 0.3f;

                            start.BlurPower = 6;
                            start.NoisePower = 0.3f;
                            start.Size = new(135, 200);
                            start.Location = new(10, taskbar.Bottom - taskbar.Height - start.Height - 10);
                        }

                        ActionCenter.Dock = default;
                        ActionCenter.BlurPower = 6;
                        ActionCenter.NoisePower = 0.3f;
                        ActionCenter.Size = new(120, 85);
                        ActionCenter.Location = new(ActionCenter.Parent.Width - ActionCenter.Width - 10, taskbar.Top - ActionCenter.Height - 10);

                        byte TB_Alpha = default, S_Alpha = default, AC_Alpha = default, TB_Blur = default;

                        if (DarkMode_Win)
                        {
                            AC_Alpha = 90;

                            if (ExplorerPatcher.CanBeUsed)
                            {
                                S_Alpha = (bool)Program.EP?.UseStart10 ? (byte)185 : (byte)90;
                                (TB_Alpha, TB_Blur) = (bool)Program.EP?.UseTaskbar10 ? ((byte)185, (byte)8) : ((byte)105, (byte)8);
                            }
                            else
                            {
                                (TB_Alpha, TB_Blur, S_Alpha) = (105, 8, 90);
                            }
                        }
                        else
                        {

                            AC_Alpha = 180;

                            if (ExplorerPatcher.CanBeUsed)
                            {
                                S_Alpha = (bool)Program.EP?.UseStart10 ? (byte)210 : (byte)180;
                                (TB_Alpha, TB_Blur) = (bool)Program.EP?.UseTaskbar10 ? ((byte)210, (byte)8) : ((byte)180, (byte)8);
                            }
                            else
                            {
                                (TB_Alpha, TB_Blur, S_Alpha) = (180, 8, 180);
                            }
                        }

                        ActionCenter.BackColorAlpha = AC_Alpha;
                        start.BackColorAlpha = S_Alpha;
                        taskbar.BackColorAlpha = TB_Alpha;
                        taskbar.BlurPower = TB_Blur;

                        break;
                    }

                case WindowStyle.W11:
                    {
                        Preview = VisualStyles == Theme.Structures.VisualStyles.DefaultVisualStyles.Aero || VisualStyles == Theme.Structures.VisualStyles.DefaultVisualStyles.AeroOpaque ? Preview_Enum.W11 : Preview_Enum.W11Lite;

                        if (OS.W12 || OS.W11) Program.EP = new();

                        if (ExplorerPatcher.CanBeUsed)
                        {
                            ref ExplorerPatcher EP = ref Program.EP;

                            if ((bool)!EP?.UseTaskbar10)
                            {
                                taskbar.Height = 42;
                                taskbar.NoisePower = 0.3f;
                            }
                            else
                            {
                                taskbar.Height = 35;
                                taskbar.NoisePower = 0f;
                                UseWin11ORB_WithWin10 = (bool)!EP?.TaskbarButton10;
                            }

                            if ((bool)!EP?.UseStart10)
                            {
                                start.BlurPower = 6;
                                start.NoisePower = 0.3f;
                                start.Size = new(135, 200);
                                start.Location = new(10, taskbar.Bottom - taskbar.Height - start.Height - 10);
                            }
                            else
                            {
                                start.BlurPower = 7;
                                start.NoisePower = 0.3f;

                                switch (EP?.StartStyle)
                                {
                                    case ExplorerPatcher.StartStyles.NotRounded:
                                        {
                                            start.Size = new(182, 201);
                                            start.Left = 0;
                                            start.Top = taskbar.Bottom - taskbar.Height - start.Height;
                                            UseWin11RoundedCorners_WithWin10_Level1 = false;
                                            UseWin11RoundedCorners_WithWin10_Level2 = false;
                                            break;
                                        }

                                    case ExplorerPatcher.StartStyles.RoundedCornersDockedMenu:
                                        {
                                            start.Size = new(182, 201);
                                            start.Left = 0;
                                            start.Top = taskbar.Bottom - taskbar.Height - start.Height;
                                            UseWin11RoundedCorners_WithWin10_Level1 = true;
                                            UseWin11RoundedCorners_WithWin10_Level2 = false;
                                            break;
                                        }

                                    case ExplorerPatcher.StartStyles.RoundedCornersFloatingMenu:
                                        {
                                            start.Size = new(182, 201);
                                            start.Location = new(10, taskbar.Bottom - taskbar.Height - start.Height - 10);
                                            UseWin11RoundedCorners_WithWin10_Level1 = false;
                                            UseWin11RoundedCorners_WithWin10_Level2 = true;
                                            break;
                                        }
                                }
                            }
                        }

                        else
                        {
                            taskbar.BlurPower = 8;
                            taskbar.Height = 42;
                            taskbar.NoisePower = 0.3f;

                            start.BlurPower = 6;
                            start.NoisePower = 0.3f;
                            start.Size = new(135, 200);
                            start.Location = new(10, taskbar.Bottom - taskbar.Height - start.Height - 10);
                        }

                        ActionCenter.Dock = default;
                        ActionCenter.BlurPower = 6;
                        ActionCenter.NoisePower = 0.3f;
                        ActionCenter.Size = new(120, 85);
                        ActionCenter.Location = new(ActionCenter.Parent.Width - ActionCenter.Width - 10, taskbar.Top - ActionCenter.Height - 10);

                        byte TB_Alpha = default, S_Alpha = default, AC_Alpha = default, TB_Blur = default;

                        if (DarkMode_Win)
                        {
                            AC_Alpha = 90;

                            if (ExplorerPatcher.CanBeUsed)
                            {
                                S_Alpha = (bool)Program.EP?.UseStart10 ? (byte)185 : (byte)90;
                                (TB_Alpha, TB_Blur) = (bool)Program.EP?.UseTaskbar10 ? ((byte)185, (byte)8) : ((byte)105, (byte)8);
                            }
                            else
                            {
                                (TB_Alpha, TB_Blur, S_Alpha) = (105, 8, 90);
                            }
                        }
                        else
                        {

                            AC_Alpha = 180;

                            if (ExplorerPatcher.CanBeUsed)
                            {
                                S_Alpha = (bool)Program.EP?.UseStart10 ? (byte)210 : (byte)180;
                                (TB_Alpha, TB_Blur) = (bool)Program.EP?.UseTaskbar10 ? ((byte)210, (byte)8) : ((byte)180, (byte)8);
                            }
                            else
                            {
                                (TB_Alpha, TB_Blur, S_Alpha) = (180, 8, 180);
                            }
                        }

                        ActionCenter.BackColorAlpha = AC_Alpha;
                        start.BackColorAlpha = S_Alpha;
                        taskbar.BackColorAlpha = TB_Alpha;
                        taskbar.BlurPower = TB_Blur;

                        break;
                    }

                case WindowStyle.W10:
                    {
                        Preview = VisualStyles == Theme.Structures.VisualStyles.DefaultVisualStyles.Aero || VisualStyles == Theme.Structures.VisualStyles.DefaultVisualStyles.AeroOpaque ? Preview_Enum.W10 : Preview_Enum.W10Lite;

                        ActionCenter.Dock = DockStyle.Right;
                        ActionCenter.BlurPower = 7;
                        ActionCenter.NoisePower = 0.3f;
                        // ########################
                        taskbar.BlurPower = !IncreaseTBTransparency ? 12 : 6;
                        // ########################
                        start.BlurPower = 7;
                        start.NoisePower = 0.3f;
                        // ########################

                        taskbar.Height = 35;
                        UseWin11ORB_WithWin10 = false;
                        start.Size = new(182, 201);
                        start.Left = 0;
                        start.Top = taskbar.Bottom - taskbar.Height - start.Height;
                        UseWin11RoundedCorners_WithWin10_Level1 = false;
                        UseWin11RoundedCorners_WithWin10_Level2 = false;

                        byte TB_Alpha, S_Alpha, AC_Alpha;
                        byte TB_Blur_value;

                        if (!TB_Blur)
                        {
                            TB_Blur_value = 0;
                        }
                        else
                        {
                            TB_Blur_value = (byte)(!IncreaseTBTransparency ? 12 : 8);
                        }

                        if (Transparency)
                        {
                            if (DarkMode_Win)
                            {
                                TB_Alpha = (byte)(!IncreaseTBTransparency ? 150 : 75);
                                S_Alpha = 150;
                                AC_Alpha = 150;
                            }
                            else
                            {
                                TB_Alpha = (byte)(!IncreaseTBTransparency ? 200 : 125);
                                S_Alpha = 200;
                                AC_Alpha = 200;
                            }
                        }
                        else
                        {
                            TB_Alpha = 255;
                            S_Alpha = 255;
                            AC_Alpha = 255;
                        }


                        ActionCenter.BackColorAlpha = AC_Alpha;
                        start.BackColorAlpha = S_Alpha;
                        taskbar.BackColorAlpha = TB_Alpha;
                        taskbar.BlurPower = TB_Blur_value;

                        break;
                    }

                case WindowStyle.W81:
                    {
                        taskbar.BlurPower = 0;
                        taskbar.Height = 34;

                        start.BlurPower = 0;
                        start.Top = taskbar.Top - start.Height;
                        start.Left = 0;
                        break;
                    }

                case WindowStyle.W8:
                    {
                        taskbar.BlurPower = 0;
                        taskbar.Height = 34;

                        start.BlurPower = 0;
                        start.Top = taskbar.Top - start.Height;
                        start.Left = 0;
                        break;
                    }

                case WindowStyle.W7:
                    {
                        taskbar.BlurPower = 1;
                        start.BlurPower = 1;

                        taskbar.Height = 34;
                        start.NoisePower = 0.5f;
                        start.Width = 136;
                        start.Height = 191;
                        start.Left = 0;
                        start.Top = taskbar.Top - start.Height;
                        break;
                    }

                case WindowStyle.WVista:
                    {
                        taskbar.Height = 30;

                        start.Width = 136;
                        start.Height = 191;
                        start.Left = 0;
                        start.Top = taskbar.Top - start.Height;

                        ButtonR2.Image = Assets.WinLogos.Win7.Resize(18, 16);
                        break;
                    }

                case WindowStyle.WXP:
                    {
                        taskbar.Height = 30;
                        start.Width = 150;
                        start.Height = 190;
                        start.Left = 0;
                        start.Top = taskbar.Top - start.Height;

                        ButtonR2.Image = Assets.WinLogos.WinXP.Resize(18, 16);
                        break;
                    }

                default:
                    break;
            }

            if (_windowStyle != WindowStyle.WVista & _windowStyle != WindowStyle.WXP)
            {
                ClassicTaskbar.Height = 44;
                ButtonR3.Image = Properties.Resources.SampleApp_Active;
                ButtonR4.Image = Properties.Resources.SampleApp_Inactive;
                ButtonR2.Image = Assets.WinLogos.Win7.Resize(18, 16);
                ButtonR3.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
                ButtonR4.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
                ButtonR3.Width = 48;
                ButtonR4.Width = 48;
                ButtonR3.Text = string.Empty;
                ButtonR4.Text = string.Empty;
                ButtonR4.Left = ButtonR3.Right + 3;
                ButtonR3.HatchBrush = false;
                ClassicTaskbar.UseItAsWin7Taskbar = false;
            }
            else
            {
                ClassicTaskbar.Height = taskbar.Height;
                ButtonR3.Image = Properties.Resources.SampleApp_Active.Resize(23, 23);
                ButtonR4.Image = Properties.Resources.SampleApp_Inactive.Resize(23, 23);
                ButtonR3.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
                ButtonR4.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
                ButtonR3.Width = 140;
                ButtonR4.Width = 140;
                ButtonR3.Text = ClassicWindow1.Text;
                ButtonR4.Text = ClassicWindow2.Text;
                ButtonR4.Left = ButtonR3.Right + 3;
                ButtonR3.HatchBrush = true;
                ClassicTaskbar.UseItAsWin7Taskbar = true;
            }

            // !DesignMode is used to avoid ex error of accessing WinPaletter theme manager from designer mode
            if (!DesignMode && (_windowStyle == WindowStyle.W12 || _windowStyle == WindowStyle.W11 || (_windowStyle == WindowStyle.W10 && _hookedTM is not null && (bool)!_hookedTM?.WindowsEffects.FullScreenStartMenu)))
            {
                Window1.Left = (int)Math.Round(start.Right + (Window1.Parent.Width - (start.Width + start.Left) - (ActionCenter.Width + (ActionCenter.Parent.Width - ActionCenter.Right)) - Window1.Width) / 2d);
            }
            else
            {
                Window1.Left = (int)Math.Round((Window1.Parent.Width - Window1.Width) / 2d);
            }

            WXP_Alert.Visible = false;

            // DesignMode is used to avoid ex error of accessing WinPaletter theme manager from designer mode
            start.Visible = DesignMode || ((_windowStyle != WindowStyle.W81 && _windowStyle != WindowStyle.W8) & !(_windowStyle == WindowStyle.W10 && _hookedTM is not null && (bool)_hookedTM?.WindowsEffects.FullScreenStartMenu));
            Panel3.Visible = _windowStyle == WindowStyle.W12 || _windowStyle == WindowStyle.W11 || _windowStyle == WindowStyle.W10;
            lnk_preview.Visible = _windowStyle == WindowStyle.W12 || _windowStyle == WindowStyle.W11 || _windowStyle == WindowStyle.W10;
            ActionCenter.Visible = _windowStyle == WindowStyle.W12 || _windowStyle == WindowStyle.W11 || _windowStyle == WindowStyle.W10;

            Window1.Top = (int)Math.Round((Window1.Parent.Height - taskbar.Height - (Window1.Height + Window2.Height)) / 2d);
            Window2.Top = Window1.Bottom;
            Window2.Left = Window1.Left;

            if (!DesignMode && HookedTM is not null) LoadFromTM(HookedTM);
        }

        /// <summary>
        /// Refreshes the colors of the form
        /// </summary>
        public void RefreshColors()
        {
            ForceRefresh = true;
            Color1 = _color1;
            Color2 = _color2;
            Color3 = _color3;
            Color4 = _color4;
            Color5 = _color5;
            Color6 = _color6;
            Color7 = _color7;
            Color8 = _color8;
            Color9 = _color9;
            ForceRefresh = false;
        }

        /// <summary>
        /// Set the theme of the preview
        /// </summary>
        private void SetTheme()
        {
            Classic = false;

            if (_windowStyle == WindowStyle.WXP)
            {
                Preview = Preview_Enum.WXP;

                if (_visualStyles != Theme.Structures.VisualStyles.DefaultVisualStyles.Classic && Program.ClassicThemeRunning)
                {
                    WXP_Alert.Visible = true;
                    WXP_Alert.Size = new(510, 70);
                    WXP_Alert.Location = new(7, 113);
                }
                else
                {
                    WXP_Alert.Visible = false;
                }

                switch (_visualStyles)
                {
                    case Theme.Structures.VisualStyles.DefaultVisualStyles.LunaOlive:
                        {
                            msstyles = SysPaths.Theme_Luna_WP;
                            System.IO.File.WriteAllText(SysPaths.Theme_Luna_WP, $"[VisualStyles]{"\r\n"}Path={$@"{SysPaths.MSSTYLES_Luna_WP}"}{"\r\n"}ColorStyle=HomeStead{"\r\n"}Size=NormalSize");
                            resVS = new(msstyles);
                            Classic = false;
                            break;
                        }

                    case Theme.Structures.VisualStyles.DefaultVisualStyles.LunaSilver:
                        {
                            msstyles = SysPaths.Theme_Luna_WP;
                            System.IO.File.WriteAllText(SysPaths.Theme_Luna_WP, $"[VisualStyles]{"\r\n"}Path={$@"{SysPaths.MSSTYLES_Luna_WP}"}{"\r\n"}ColorStyle=Metallic{"\r\n"}Size=NormalSize");
                            resVS = new(msstyles);
                            Classic = false;
                            break;
                        }

                    case Theme.Structures.VisualStyles.DefaultVisualStyles.Custom:
                        {
                            if (System.IO.File.Exists(_visualStylesPath))
                            {
                                if (System.IO.Path.GetExtension(_visualStylesPath) == ".theme")
                                {
                                    msstyles = _visualStylesPath;
                                }
                                else if (System.IO.Path.GetExtension(_visualStylesPath) == ".msstyles")
                                {
                                    msstyles = SysPaths.Theme_Temp;
                                    System.IO.File.WriteAllText(SysPaths.Theme_Temp, $"[VisualStyles]{"\r\n"}Path={_visualStylesPath}{"\r\n"}ColorStyle={_visualStylesColorScheme}{"\r\n"}Size=NormalSize");
                                }
                            }
                            Classic = false;
                            resVS = new(msstyles);
                            break;
                        }

                    case Theme.Structures.VisualStyles.DefaultVisualStyles.Classic:
                        {
                            Classic = true;
                            break;
                        }

                    default:
                        {
                            msstyles = SysPaths.Theme_Luna_WP;
                            System.IO.File.WriteAllText(SysPaths.Theme_Luna_WP, $"[VisualStyles]{"\r\n"}Path={$@"{SysPaths.MSSTYLES_Luna_WP}"}{"\r\n"}ColorStyle=NormalColor{"\r\n"}Size=NormalSize");
                            resVS = new(msstyles);
                            Classic = false;
                            break;
                        }
                }

                if (_visualStyles != Theme.Structures.VisualStyles.DefaultVisualStyles.Classic)
                {
                    if (System.IO.File.Exists(msstyles))
                    {
                        using (VisualStyleFile vs = new(msstyles))
                        {
                            if (WXP_VS_ReplaceColors) HookedTM.Win32.Load(Theme.Structures.Win32UI.Sources.VisualStyles, vs.Metrics);

                            if (WXP_VS_ReplaceMetrics) HookedTM.MetricsFonts.Overwrite_Metrics(vs.Metrics);

                            if (WXP_VS_ReplaceFonts) HookedTM.MetricsFonts.Overwrite_Fonts(vs.Metrics);
                        }
                    }
                }

                Refresh();
            }

            else if (_windowStyle == WindowStyle.WVista || _windowStyle == WindowStyle.W7 || WindowStyle == WindowStyle.W8 || _windowStyle == WindowStyle.W81)
            {
                WinVista = _windowStyle == WindowStyle.WVista;

                switch (_visualStyles)
                {
                    case Theme.Structures.VisualStyles.DefaultVisualStyles.AeroOpaque:
                        {
                            Preview = (WindowStyle != WindowStyle.W81 && WindowStyle != WindowStyle.W8) ? Preview_Enum.W7Opaque : Preview_Enum.W8Lite;
                            Classic = false;
                            break;
                        }

                    case Theme.Structures.VisualStyles.DefaultVisualStyles.AeroLite:
                        {
                            Preview = (WindowStyle == WindowStyle.W81 || WindowStyle == WindowStyle.W8) ? Preview_Enum.W8Lite : Preview_Enum.W7Opaque;
                            Classic = false;
                            break;
                        }

                    case Theme.Structures.VisualStyles.DefaultVisualStyles.Basic:
                        {
                            Preview = Preview_Enum.W7Basic;
                            Classic = false;
                            break;
                        }

                    case Theme.Structures.VisualStyles.DefaultVisualStyles.Classic:
                        {
                            Classic = true;
                            break;
                        }

                    default:
                        {
                            Classic = false;
                            Preview = (WindowStyle != WindowStyle.W81 && WindowStyle != WindowStyle.W8) ? Preview_Enum.W7Aero : Preview_Enum.W8;
                            break;
                        }
                }

                Refresh();
            }

            else if (_windowStyle == WindowStyle.W10 || _windowStyle == WindowStyle.W11 || _windowStyle == WindowStyle.W12)
            {
                switch (_visualStyles)
                {
                    case Theme.Structures.VisualStyles.DefaultVisualStyles.AeroLite:
                        {
                            Preview = _windowStyle == WindowStyle.W10 ? Preview_Enum.W10Lite : Preview_Enum.W11Lite;
                            break;
                        }

                    default:
                        {
                            Preview = _windowStyle == WindowStyle.W10 ? Preview_Enum.W10 : Preview_Enum.W11;
                            break;
                        }
                }
            }
        }

        #endregion

        /// <summary>
        /// Event handler for editing elements on the preview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);

        /// <summary>
        /// Event for editing elements on the preview
        /// </summary>
        public event EditorInvokerEventHandler EditorInvoker;

        /// <summary>
        /// Load preferernces from a <see cref="Theme.Manager"/>
        /// </summary>
        /// <param name="TM"></param>
        public void LoadFromTM(Theme.Manager TM)
        {
            ForceRefresh = true;

            _hookedTM = TM;

            if (WindowStyle == WindowStyle.W12)
            {
                Transparency = HookedTM.Windows12.Transparency;
                DarkMode_App = !HookedTM.Windows12.AppMode_Light;
                DarkMode_Win = !HookedTM.Windows12.WinMode_Light;
                TitlebarColor_Active = HookedTM.Windows12.Titlebar_Active;
                TitlebarColor_Inactive = HookedTM.Windows12.Titlebar_Inactive;
                TitlebarColor_Enabled = HookedTM.Windows12.ApplyAccentOnTitlebars;
                AccentLevel = HookedTM.Windows12.ApplyAccentOnTaskbar;
                TB_Blur = true;
                Color1 = HookedTM.Windows12.Color_Index5;
                Color2 = HookedTM.Windows12.Color_Index0;
                Color3 = HookedTM.Windows12.Color_Index1;
                Color4 = HookedTM.Windows12.Color_Index4;
                Color5 = HookedTM.Windows12.Color_Index3;
                Color6 = HookedTM.Windows12.Color_Index2;
                Color7 = HookedTM.Windows12.StartMenu_Accent;
                Color8 = HookedTM.Windows12.Color_Index6;
                Color9 = HookedTM.Windows12.Color_Index7;
                VisualStyles = HookedTM.Windows12.VisualStyles.VisualStylesType;
            }

            else if (WindowStyle == WindowStyle.W11)
            {
                Transparency = HookedTM.Windows11.Transparency;
                DarkMode_App = !HookedTM.Windows11.AppMode_Light;
                DarkMode_Win = !HookedTM.Windows11.WinMode_Light;
                TitlebarColor_Active = HookedTM.Windows11.Titlebar_Active;
                TitlebarColor_Inactive = HookedTM.Windows11.Titlebar_Inactive;
                TitlebarColor_Enabled = HookedTM.Windows11.ApplyAccentOnTitlebars;
                AccentLevel = HookedTM.Windows11.ApplyAccentOnTaskbar;
                TB_Blur = true;
                Color1 = HookedTM.Windows11.Color_Index5;
                Color2 = HookedTM.Windows11.Color_Index0;
                Color3 = HookedTM.Windows11.Color_Index1;
                Color4 = HookedTM.Windows11.Color_Index4;
                Color5 = HookedTM.Windows11.Color_Index3;
                Color6 = HookedTM.Windows11.Color_Index2;
                Color7 = HookedTM.Windows11.StartMenu_Accent;
                Color8 = HookedTM.Windows11.Color_Index6;
                Color9 = HookedTM.Windows11.Color_Index7;
                VisualStyles = HookedTM.Windows11.VisualStyles.VisualStylesType;
            }

            else if (WindowStyle == WindowStyle.W10)
            {
                Transparency = HookedTM.Windows10.Transparency;
                DarkMode_App = !HookedTM.Windows10.AppMode_Light;
                DarkMode_Win = !HookedTM.Windows10.WinMode_Light;
                TitlebarColor_Active = HookedTM.Windows10.Titlebar_Active;
                TitlebarColor_Inactive = HookedTM.Windows10.Titlebar_Inactive;
                TitlebarColor_Enabled = HookedTM.Windows10.ApplyAccentOnTitlebars;
                AccentLevel = HookedTM.Windows10.ApplyAccentOnTaskbar;
                TB_Blur = HookedTM.Windows10.TB_Blur;
                Color1 = HookedTM.Windows10.Color_Index5;
                Color2 = HookedTM.Windows10.Color_Index0;
                Color3 = HookedTM.Windows10.Color_Index1;
                Color4 = HookedTM.Windows10.Color_Index4;
                Color5 = HookedTM.Windows10.Color_Index3;
                Color6 = HookedTM.Windows10.Color_Index2;
                Color7 = HookedTM.Windows10.StartMenu_Accent;
                Color8 = HookedTM.Windows10.Color_Index6;
                Color9 = HookedTM.Windows10.Color_Index7;
                VisualStyles = HookedTM.Windows10.VisualStyles.VisualStylesType;
            }

            else if (WindowStyle == WindowStyle.W81)
            {
                Win7GlowBal = 100;
                Win7Noise = 100f;
                Win7Alpha = 100;

                TitlebarColor_Active = HookedTM.Windows81.ColorizationColor;
                TitlebarColor_Inactive = HookedTM.Windows81.ColorizationColor;
                Win7ColorBal = HookedTM.Windows81.ColorizationColorBalance;
                TitlebarColor_Enabled = true;
                VisualStyles = HookedTM.Windows81.VisualStyles.VisualStylesType;
            }

            else if (WindowStyle == WindowStyle.W8)
            {
                Win7GlowBal = 100;
                Win7Noise = 100f;
                Win7Alpha = 100;

                TitlebarColor_Active = HookedTM.Windows8.ColorizationColor;
                TitlebarColor_Inactive = HookedTM.Windows8.ColorizationColor;
                Win7ColorBal = HookedTM.Windows8.ColorizationColorBalance;
                TitlebarColor_Enabled = true;
                VisualStyles = HookedTM.Windows8.VisualStyles.VisualStylesType;
            }

            else if (WindowStyle == WindowStyle.W7)
            {
                TitlebarColor_Active = HookedTM.Windows7.ColorizationColor;
                TitlebarColor_Inactive = HookedTM.Windows7.ColorizationColor;
                Win7ColorBal = HookedTM.Windows7.ColorizationColorBalance;
                TitlebarColor_Enabled = true;
                AfterGlowColor_Active = HookedTM.Windows7.ColorizationAfterglow;
                AfterGlowColor_Inactive = HookedTM.Windows7.ColorizationAfterglow;
                Win7GlowBal = HookedTM.Windows7.ColorizationAfterglowBalance;
                Win7Noise = HookedTM.Windows7.ColorizationGlassReflectionIntensity;
                Win7Alpha = HookedTM.Windows7.ColorizationBlurBalance;
                VisualStyles = HookedTM.Windows7.VisualStyles.VisualStylesType;

            }

            else if (WindowStyle == WindowStyle.WVista)
            {
                TitlebarColor_Active = HookedTM.WindowsVista.ColorizationColor;
                TitlebarColor_Inactive = HookedTM.WindowsVista.ColorizationColor;
                Win7ColorBal = HookedTM.WindowsVista.Alpha / 255 * 100;
                TitlebarColor_Enabled = true;
                AfterGlowColor_Active = Color.Transparent;
                AfterGlowColor_Inactive = Color.Transparent;
                Win7GlowBal = 0;
                Win7Noise = 100f;
                Win7Alpha = 100 - HookedTM.WindowsVista.Alpha / 255 * 100;
                VisualStyles = HookedTM.WindowsVista.VisualStyles.VisualStylesType;

            }

            else if (WindowStyle == WindowStyle.WXP)
            {
                VisualStylesPath = HookedTM.WindowsXP.VisualStyles.ThemeFile;
                VisualStylesColorScheme = HookedTM.WindowsXP.VisualStyles.ColorScheme;
                VisualStyles = HookedTM.WindowsXP.VisualStyles.VisualStylesType;
            }

            Metrics_CaptionHeight = TM.MetricsFonts.CaptionHeight;
            Metrics_CaptionWidth = TM.MetricsFonts.CaptionWidth;
            Metrics_BorderWidth = TM.MetricsFonts.BorderWidth;
            Metrics_PaddedBorderWidth = TM.MetricsFonts.PaddedBorderWidth;
            Metrics_CaptionFont = TM.MetricsFonts.CaptionFont;

            Shadow = TM.WindowsEffects.WindowShadow;

            ForceRefresh = false;
        }

        /// <summary>
        /// Load metrics and fonts from a <see cref="Theme.Manager"/>
        /// </summary>
        /// <param name="TM"></param>
        public void LoadMetricsFonts(Theme.Manager TM)
        {
            Metrics_BorderWidth = TM.MetricsFonts.BorderWidth;
            Metrics_CaptionHeight = TM.MetricsFonts.CaptionHeight;
            Metrics_CaptionWidth = TM.MetricsFonts.CaptionWidth;
            Metrics_PaddedBorderWidth = TM.MetricsFonts.PaddedBorderWidth;
            Metrics_CaptionFont = TM.MetricsFonts.CaptionFont;

            int iP = 3 + TM.MetricsFonts.PaddedBorderWidth + TM.MetricsFonts.BorderWidth;
            int iT = 4 + TM.MetricsFonts.PaddedBorderWidth + TM.MetricsFonts.BorderWidth + TM.MetricsFonts.CaptionHeight + GetTitlebarTextHeight(TM.MetricsFonts.CaptionFont);
            System.Windows.Forms.Padding _Padding = new(iP, iT, iP, iP);

            foreach (ContextMenuR contextMenu in this.GetAllControls().OfType<ContextMenuR>()) { contextMenu.Font = TM.MetricsFonts.MenuFont; }
            foreach (LabelR label in this.GetAllControls().OfType<LabelR>()) { label.Font = TM.MetricsFonts.MessageFont; }
            foreach (ScrollBarR scrollBar in this.GetAllControls().OfType<ScrollBarR>()) { scrollBar.Width = TM.MetricsFonts.ScrollWidth; }
            foreach (ToolTipR toolTip in this.GetAllControls().OfType<ToolTipR>()) { toolTip.Font = TM.MetricsFonts.CaptionFont; }
            foreach (MenuBarR menubar in this.GetAllControls().OfType<MenuBarR>())
            {
                menubar.MenuHeight = TM.MetricsFonts.MenuHeight;
                menubar.Font = TM.MetricsFonts.MessageFont;
            }
            foreach (ButtonR button in this.GetAllControls().OfType<ButtonR>())
            {
                button.FocusRectWidth = (int)TM.WindowsEffects.FocusRectWidth;
                button.FocusRectHeight = (int)TM.WindowsEffects.FocusRectHeight;
            }
            foreach (WindowR WindowR in this.GetAllControls().OfType<WindowR>())
            {
                SetClassicWindowMetrics(TM, WindowR);
                WindowR.Padding = _Padding;
            }
        }

        /// <summary>
        /// Load metrics and fonts from a <see cref="VisualStyleMetrics"/>
        /// </summary>
        /// <param name="vs"></param>
        public void LoadMetricsFonts(VisualStyleMetrics vs)
        {
            using (Theme.Manager TMx = new(Theme.Manager.Source.Empty))
            {
                TMx.MetricsFonts.Overwrite_Metrics(vs);
                TMx.MetricsFonts.Overwrite_Fonts(vs);
                LoadMetricsFonts(TMx);
            }
        }

        /// <summary>
        /// Load classic colors from a <see cref="Theme.Structures.Win32UI"/>
        /// </summary>
        /// <param name="w32ui"></param>
        public void LoadClassicColors(Theme.Structures.Win32UI w32ui)
        {
            EnableGradient = w32ui.EnableGradient;
            ActiveBorder = w32ui.ActiveBorder;
            ActiveTitle = w32ui.ActiveTitle;
            Background = w32ui.Background;
            ButtonDkShadow = w32ui.ButtonDkShadow;
            ButtonFace = w32ui.ButtonFace;
            ButtonHilight = w32ui.ButtonHilight;
            ButtonLight = w32ui.ButtonLight;
            ButtonShadow = w32ui.ButtonShadow;
            ButtonText = w32ui.ButtonText;
            GradientActiveTitle = w32ui.GradientActiveTitle;
            GradientInactiveTitle = w32ui.GradientInactiveTitle;
            GrayText = w32ui.GrayText;
            InactiveBorder = w32ui.InactiveBorder;
            InactiveTitle = w32ui.InactiveTitle;
            InactiveTitleText = w32ui.InactiveTitleText;
            TitleText = w32ui.TitleText;
            Window = w32ui.Window;
            WindowFrame = w32ui.WindowFrame;
            WindowText = w32ui.WindowText;
        }

        /// <summary>
        /// Load classic colors from a <see cref="VisualStyleMetrics"/>
        /// </summary>
        /// <param name="vs"></param>
        public void LoadClassicColors(VisualStyleMetrics vs)
        {
            EnableGradient = true;
            ActiveTitle = vs.Colors.ActiveCaption;
            Background = vs.Colors.Background;
            ButtonDkShadow = vs.Colors.DkShadow3d;
            ButtonFace = vs.Colors.Btnface;
            ButtonHilight = vs.Colors.BtnHighlight;
            ButtonLight = vs.Colors.Light3d;
            ButtonShadow = vs.Colors.BtnShadow;
            ButtonText = vs.Colors.WindowText;
            GradientActiveTitle = vs.Colors.GradientActiveCaption;
            GradientInactiveTitle = vs.Colors.GradientInactiveCaption;
            GrayText = vs.Colors.GrayText;
            InactiveTitle = vs.Colors.InactiveCaption;
            InactiveTitleText = vs.Colors.InactiveCaptionText;
            TitleText = vs.Colors.CaptionText;
            Window = vs.Colors.Window;
            WindowText = vs.Colors.WindowText;
        }

        private void WindowsDesktop_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                LoadFromTM(HookedTM);

                LoadMetricsFonts(HookedTM);
                LoadClassicColors(HookedTM.Win32);

                SetStyles();
            }
        }

        /// <summary>
        /// Background image of the desktop.
        /// </summary>
        public override Image BackgroundImage
        {
            get => base.BackgroundImage;
            set
            {
                base.BackgroundImage = value;
                pnl_preview.BackgroundImage = value;
                pnl_preview_classic.BackgroundImage = value;
            }
        }

        private void WindowX_EditorInvoker(object sender, EditorEventArgs e)
        {
            EditorInvoker?.Invoke(sender, e);
        }

        private void setting_icon_preview_MouseClick(object sender, MouseEventArgs e)
        {
            EditorInvoker?.Invoke(sender, new EditorEventArgs(prop_settingsIconsColor));
        }

        private void lnk_preview_MouseClick(object sender, MouseEventArgs e)
        {
            EditorInvoker?.Invoke(sender, new EditorEventArgs(prop_linksColor));
        }
    }
}