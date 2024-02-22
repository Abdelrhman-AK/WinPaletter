using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using WinPaletter.UI.Controllers;
using WinPaletter.UI.Retro;
using WinPaletter.UI.Simulation;
using static WinPaletter.PreviewHelpers;
using static WinPaletter.UI.Simulation.Window;

namespace WinPaletter.Templates
{
    public partial class WindowMetrics : UserControl
    {
        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);
        public event EditorInvokerEventHandler EditorInvoker;

        public WindowMetrics()
        {
            DoubleBuffered = true;

            this.DoubleBuffer();

            InitializeComponent();

            if (!DesignMode)
            {
                if (OS.WXP)
                {
                    pic.Image = SystemIcons.Information.ToBitmap();
                }
                else
                {
                    Icon ico = DLLFunc.GetSystemIcon(Shell32.SHSTOCKICONID.INFO, Shell32.SHGSI.ICON);
                    if (ico is not null)
                    {
                        pic.Image = ico.ToBitmap();
                    }
                    else
                    {
                        pic.Image = SystemIcons.Information.ToBitmap();
                    }
                }

                pictureBox1.Image = pic.Image;


                ResizableControlHook hook1 = new(MenuStrip1);
            }
        }

        #region Variables

        bool ForceRefresh = false;
        private bool WXP_VS_ReplaceColors = false;
        private bool WXP_VS_ReplaceMetrics = false;
        private bool WXP_VS_ReplaceFonts = false;
        private string msstyles;

        bool Win7;
        bool Win81;
        bool WinXP;

        #endregion

        #region Properties

        /// <summary>
        /// Window border width
        /// </summary>
        public int BorderWidth
        {
            get => _borderWidth;
            set
            {
                if (_borderWidth != value)
                {
                    _borderWidth = value;
                    windowR1.Metrics_BorderWidth = value;
                    windowR2.Metrics_BorderWidth = value;
                    windowR3.Metrics_BorderWidth = value;
                    Window1.Metrics_BorderWidth = value;
                    Window2.Metrics_BorderWidth = value;
                    Window3.Metrics_BorderWidth = value;
                }
            }
        }
        private int _borderWidth = 1;

        /// <summary>
        /// Titlebar (caption) height
        /// </summary>
        public int CaptionHeight
        {
            get => _captionHeight;
            set
            {
                if (_captionHeight != value)
                {
                    _captionHeight = value;
                    windowR1.Metrics_CaptionHeight = value;
                    windowR3.Metrics_CaptionHeight = value;
                    Window1.Metrics_CaptionHeight = value;
                    Window3.Metrics_CaptionHeight = value;
                }
            }
        }
        private int _captionHeight = 24;


        /// <summary>
        /// Titlebar (caption) height of a tool box window
        /// </summary>
        public int SmCaptionHeight
        {
            get => _smCaptionHeight;
            set
            {
                if (_smCaptionHeight != value)
                {
                    _smCaptionHeight = value;
                    windowR2.Metrics_CaptionHeight = value;
                    Window2.Metrics_CaptionHeight = value;
                }
            }
        }
        private int _smCaptionHeight;


        /// <summary>
        /// Buttons in classic titlebar (caption) width
        /// </summary>
        public int CaptionWidth
        {
            get => _captionWidth;
            set
            {
                if (_captionWidth != value)
                {
                    _captionWidth = value;
                    windowR1.Metrics_CaptionWidth = value;
                    windowR3.Metrics_CaptionWidth = value;
                }
            }
        }
        private int _captionWidth = 24;


        /// <summary>
        /// Width of Buttons in classic titlebar (caption) of a tool box window
        /// </summary>
        public int SmCaptionWidth
        {
            get => _smCaptionWidth;
            set
            {
                if (_smCaptionWidth != value)
                {
                    _smCaptionWidth = value;
                    windowR2.Metrics_CaptionWidth = value;
                }
            }
        }
        private int _smCaptionWidth = 24;


        /// <summary>
        /// Padding width of a Window border
        /// </summary>
        public int PaddedBorderWidth
        {
            get => _paddedBorderWidth;
            set
            {
                if (_paddedBorderWidth != value)
                {
                    _paddedBorderWidth = value;
                    windowR1.Metrics_PaddedBorderWidth = value;
                    windowR2.Metrics_PaddedBorderWidth = value;
                    windowR3.Metrics_PaddedBorderWidth = value;
                    Window1.Metrics_PaddedBorderWidth = value;
                    Window2.Metrics_PaddedBorderWidth = value;
                    Window3.Metrics_PaddedBorderWidth = value;
                }
            }
        }
        private int _paddedBorderWidth = 4;


        /// <summary>
        /// Titlebar (caption) font
        /// </summary>
        public Font CaptionFont
        {
            get => _captionFont;
            set
            {
                if (_captionFont != value)
                {
                    _captionFont = value;
                    windowR1.Font = value;
                    windowR3.Font = value;
                    Window1.Font = value;
                }
            }
        }
        private Font _captionFont = new("Segoe UI", 9f, FontStyle.Regular);


        /// <summary>
        /// Titlebar (caption) font of a tool box window
        /// <summary>
        public Font SmCaptionFont
        {
            get => _smCaptionFont;
            set
            {
                if (_smCaptionFont != value)
                {
                    _smCaptionFont = value;
                    windowR2.Font = value;
                    Window2.Font = value;
                }
            }
        }
        private Font _smCaptionFont = new("Segoe UI", 9f, FontStyle.Regular);


        /// <summary>
        /// contextMenu height (if it is a horizontal menu)
        /// </summary>
        public int MenuHeight
        {
            get { return _menuHeight; }
            set
            {
                _menuHeight = value;
                menuBarR1.MenuHeight = value;
                MenuStrip1.Height = value;
            }
        }
        private int _menuHeight = 19;

        /// <summary>
        /// contextMenu width (if it is a vertical menu)
        /// </summary>
        public int MenuWidth { get; set; } = 19;


        /// <summary>Scroll bar height (if it is a horizontal scroll bar)</summary>
        /// 
        public int ScrollHeight
        {
            get { return _scrollHeight; }
            set
            {
                _scrollHeight = value;
                scrollBarR1.Height = value;
                HScrollBar1.Height = value;
            }
        }
        private int _scrollHeight = 19;

        /// <summary>
        /// Scroll bar width (if it is a vertical scroll bar)
        /// </summary>
        public int ScrollWidth
        {
            get { return _scrollWidth; }
            set
            {
                _scrollWidth = value;
                scrollBarR2.Width = value;
                VScrollBar1.Width = value;
            }
        }
        private int _scrollWidth = 19;


        /// <summary>Context menu font</summary>
        public Font MenuFont
        {
            get { return _menuFont; }
            set
            {
                _menuFont = value;
                menuBarR1.Font = value;
                MenuStrip1.Font = value;
            }
        }
        private Font _menuFont = new("Segoe UI", 9f);

        /// <summary>Message box font</summary>
        public Font MessageFont
        {
            get { return _messageFont; }
            set
            {
                _messageFont = value;
                lbl.Font = value;
                msgLbl.Font = value;
            }
        }
        private Font _messageFont = new("Segoe UI", 9f);

        /// <summary>Status bar (in the lower part of a window) font</summary>
        public Font StatusFont
        {
            get { return _statusFont; }
            set
            {
                _statusFont = value;
                status.Font = value;
                StatusStrip1.Font = value;
                if (!DesignMode && PanelR1 is not null) PanelR1.Height = Math.Max(value.Height, 20);
            }
        }
        private Font _statusFont = new("Segoe UI", 9f);

        #endregion

        #region Important preview properties

        public bool ShowAsMenu
        {
            get => _showAsMenuOnly;
            set
            {
                tabs_preview.Visible = false;

                _showAsMenuOnly = value;

                menuBarR1.Visible = value;
                scrollBarR1.Visible = !value;
                scrollBarR2.Visible = !value;
                PanelR1.Visible = !value;
                lbl.Visible = !value;
                pictureBox1.Visible = !value;

                MenuStrip1.Visible = value;
                HScrollBar1.Visible = !value;
                VScrollBar1.Visible = !value;
                StatusStrip1.Visible = !value;
                msgLbl.Visible = !value;
                pic.Visible = !value;

                tabs_preview.Visible = true;
            }
        }
        private bool _showAsMenuOnly = false;


        private bool _classic = false;
        public bool Classic
        {
            get { return _classic; }
            set
            {
                if (value != _classic)
                {
                    _classic = value;

                    tabs_preview.Visible = false;

                    if (value) tabs_preview.SelectedIndex = _showMenuSection ? 3 : 1;
                    else tabs_preview.SelectedIndex = _showMenuSection ? 2 : 0;

                    tabs_preview.Visible = true;

                    WXP_Alert.Visible = _windowStyle == WindowStyle.WXP && Program.ClassicThemeRunning;
                }
            }
        }


        private bool _showMenuSection = false;
        public bool ShowMenuSection
        {
            get => _showMenuSection;
            set
            {
                if (value != _showMenuSection)
                {
                    _showMenuSection = value;

                    tabs_preview.Visible = false;

                    if (value) tabs_preview.SelectedIndex = _classic ? 3 : 2;
                    else tabs_preview.SelectedIndex = _classic ? 1 : 0;

                    tabs_preview.Visible = true;
                }
            }
        }


        private bool _shadow = true;
        private bool Shadow
        {
            get { return _shadow; }
            set
            {
                if (_shadow != value || ForceRefresh)
                {
                    _shadow = value;
                    Window1.Shadow = value;
                    Window2.Shadow = false;
                    Window3.Shadow = value;
                }
            }
        }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Theme.Manager HookedTM
        {
            get => !DesignMode ? _hookedTM ?? HookedTM : null;
            set
            {
                _hookedTM = value;
                LoadFromTM(_hookedTM);
            }
        }
        private Theme.Manager _hookedTM = Program.TM;


        private VisualStylesRes resVS
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

        private Theme.Structures.Windows7.Themes WindowsTheme
        {
            get => _windowsTheme;
            set
            {
                if (value != _windowsTheme || ForceRefresh)
                {
                    _windowsTheme = value;
                    SetTheme();
                }
            }
        }
        private Theme.Structures.Windows7.Themes _windowsTheme = Theme.Structures.Windows7.Themes.Aero;

        private Theme.Structures.WindowsXP.Themes WindowsXPTheme
        {
            get => _windowsXPTheme;
            set
            {
                if (value != _windowsXPTheme || ForceRefresh)
                {
                    _windowsXPTheme = value;
                    SetTheme();
                }
            }
        }
        private Theme.Structures.WindowsXP.Themes _windowsXPTheme = Theme.Structures.WindowsXP.Themes.LunaBlue;

        private string WindowsXPThemePath
        {
            get => _windowsXPThemePath;
            set
            {
                if (value != _windowsXPThemePath || ForceRefresh)
                {
                    _windowsXPThemePath = value;
                    SetTheme();
                }
            }
        }
        private string _windowsXPThemePath;

        private string WindowsXPThemeColorScheme
        {
            get => _windowsXPThemeColorScheme;
            set
            {
                if (value != _windowsXPThemeColorScheme || ForceRefresh)
                {
                    _windowsXPThemeColorScheme = value;
                    SetTheme();
                }
            }
        }
        private string _windowsXPThemeColorScheme;


        private WindowStyle _windowStyle = WindowStyle.W11;
        private WindowStyle WindowStyle
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
        private Preview_Enum Preview
        {
            get { return _preview; }
            set
            {
                if (_preview != value)
                {
                    _preview = value;
                    foreach (Window window in this.GetAllControls().OfType<Window>()) window.Preview = value;
                }
            }
        }


        private bool _darkMode_App = true;
        private bool DarkMode_App
        {
            get => _darkMode_App;
            set
            {
                if (value != _darkMode_App)
                {
                    _darkMode_App = value;
                    foreach (Window window in this.GetAllControls().OfType<Window>()) window.DarkMode = value;
                }
            }
        }


        private bool _darkMode_Win = true;
        private bool DarkMode_Win
        {
            get => _darkMode_Win;
            set
            {
                if (value != _darkMode_Win)
                {
                    _darkMode_Win = value;
                    foreach (WinElement element in this.GetAllControls().OfType<WinElement>()) element.DarkMode = value;
                }
            }
        }


        private bool _winVista = false;
        private bool WinVista
        {
            get => _winVista;
            set
            {
                if (value != _winVista)
                {
                    _winVista = value;
                    foreach (Window window in this.GetAllControls().OfType<Window>()) window.WinVista = value;
                }
            }
        }

        private int _win7Alpha = 100;
        private int Win7Alpha
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
        private int Win7ColorBal
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


        private int Win7GlowBal
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
        private float Win7Noise
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

        #region Colors properties

        private bool _AccentColor_Enabled = false;
        private bool TitlebarColor_Enabled
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
        private Color TitlebarColor_Active
        {
            get { return _titlebarColor_Active; }
            set
            {
                if (_titlebarColor_Active != value || ForceRefresh)
                {
                    _titlebarColor_Active = value;

                    foreach (Window window in this.GetAllControls().OfType<Window>()) window.AccentColor_Active = value;

                    if (WindowStyle == WindowStyle.W81 || WindowStyle == WindowStyle.W7 || WindowStyle == WindowStyle.WVista)
                    {
                        foreach (WinElement element in this.GetAllControls().OfType<WinElement>()) element.Background = value;
                    }
                }
            }
        }


        private Color _titlebarColor_Inactive = Color.FromArgb(32, 32, 32);
        private Color TitlebarColor_Inactive
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
        private Color AfterGlowColor_Active
        {
            get { return _afterglowColor_Active; }
            set
            {
                if (_afterglowColor_Active != value || ForceRefresh)
                {
                    _afterglowColor_Active = value;
                    foreach (Window window in this.GetAllControls().OfType<Window>()) window.AccentColor2_Active = value;

                    if (WindowStyle == WindowStyle.W81 || WindowStyle == WindowStyle.W7 || WindowStyle == WindowStyle.WVista)
                    {
                        foreach (WinElement element in this.GetAllControls().OfType<WinElement>()) element.Background2 = value;
                    }
                }
            }
        }


        private Color _afterglowColor2_Inactive = Color.FromArgb(32, 32, 32);
        private Color AfterGlowColor_Inactive
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


        /// <summary>
        /// If disabled, classic 3D effects will be made to menus and menu items selection
        /// </summary>
        private bool EnableTheming
        {
            get { return enableTheming; }
            set
            {
                if (enableTheming != value)
                {
                    enableTheming = value;

                    foreach (ContextMenuR contextMenu in this.GetAllControls().OfType<ContextMenuR>()) { contextMenu.Flat = value; }
                    foreach (MenuBarR menuBar in this.GetAllControls().OfType<MenuBarR>()) { menuBar.Flat = value; }
                }
            }
        }
        private bool enableTheming = false;


        /// <summary>
        /// Enable titlebar gradience
        /// </summary>
        private bool EnableGradient
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
        private Color ActiveBorder
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
        private Color ActiveTitle
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
        private Color Background
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
        private Color ButtonFace
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
                    foreach (PanelR panel in this.GetAllControls().OfType<PanelR>()) { panel.BackColor = value; }
                }
            }
        }
        private Color buttonFace;


        /// <summary>
        /// Button shadow
        /// </summary>
        private Color ButtonShadow
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
                }
            }
        }
        private Color buttonShadow;


        /// <summary>
        /// Button dark shadow
        /// </summary>
        private Color ButtonDkShadow
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
                }
            }
        }
        private Color buttonDkShadow;


        /// <summary>
        /// Button hilight
        /// </summary>
        private Color ButtonHilight
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

                }
            }
        }
        private Color buttonHilight;


        /// <summary>
        /// Button light
        /// </summary>
        private Color ButtonLight
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
                }
            }
        }
        private Color buttonLight;


        /// <summary>
        /// Forecolor of button and windows
        /// </summary>
        private Color ButtonText
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
                }
            }
        }
        private Color buttonText;


        /// <summary>
        /// Second color for gradience in active titlebar
        /// </summary>
        private Color GradientActiveTitle
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
        private Color GradientInactiveTitle
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
        private Color GrayText
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
        /// Color of cascaded menu
        /// </summary>
        private Color Menu
        {
            get { return menu; }
            set
            {
                if (menu != value)
                {
                    menu = value;

                    if (!enableTheming)
                    {
                        foreach (MenuBarR menuBar in this.GetAllControls().OfType<MenuBarR>()) { menuBar.BackColor = value; }
                    }
                }
            }
        }
        private Color menu;


        /// <summary>
        /// Color of menu bar
        /// </summary>
        private Color MenuBar
        {
            get { return menuBar; }
            set
            {
                if (menuBar != value)
                {
                    menuBar = value;
                    foreach (MenuBarR menuBar in this.GetAllControls().OfType<MenuBarR>()) { menuBar.MenuBar = value; }
                }
            }
        }
        private Color menuBar;


        /// <summary>
        /// ForeColor of menu item text
        /// </summary>
        private Color MenuText
        {
            get { return menuText; }
            set
            {
                if (menuText != value)
                {
                    menuText = value;

                    foreach (ContextMenuR contextMenu in this.GetAllControls().OfType<ContextMenuR>()) { contextMenu.ForeColor = value; }
                    foreach (MenuBarR menuBar in this.GetAllControls().OfType<MenuBarR>()) { menuBar.ForeColor = value; }
                }
            }
        }
        private Color menuText;


        /// <summary>
        /// Active titlebar text
        /// </summary>
        private Color TitleText
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
                }
            }
        }
        private Color titleText;


        /// <summary>
        /// Textbox and other controls BackColor
        /// </summary>
        private Color Window
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
        private Color WindowFrame
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
        private Color WindowText
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
            CtrlTheme theme = CtrlTheme.Default;
            Color statusBackColor = Color.White;
            Color statusForeColor = Color.Black;

            switch (_windowStyle)
            {
                case WindowStyle.W12:
                    Preview = Preview_Enum.W11;

                    if (!DarkMode_App)
                    {
                        theme = CtrlTheme.Default;
                        statusForeColor = Color.Black;
                        statusBackColor = Color.White;
                    }
                    else
                    {
                        theme = CtrlTheme.DarkExplorer;
                        statusForeColor = Color.White;
                        statusBackColor = Color.FromArgb(28, 28, 28);
                    }

                    break;

                case WindowStyle.W11:
                    Preview = Preview_Enum.W11;

                    if (!DarkMode_App)
                    {
                        theme = CtrlTheme.Default;
                        statusForeColor = Color.Black;
                        statusBackColor = Color.White;
                    }
                    else
                    {
                        theme = CtrlTheme.DarkExplorer;
                        statusForeColor = Color.White;
                        statusBackColor = Color.FromArgb(28, 28, 28);
                    }

                    break;

                case WindowStyle.W10:
                    Preview = Preview_Enum.W10;

                    if (!DarkMode_App)
                    {
                        theme = CtrlTheme.Default;
                        statusForeColor = Color.Black;
                        statusBackColor = Color.White;
                    }
                    else
                    {
                        theme = CtrlTheme.DarkExplorer;
                        statusForeColor = Color.White;
                        statusBackColor = Color.FromArgb(28, 28, 28);
                    }

                    break;
            }

            statusLbl.ForeColor = statusForeColor;
            StatusStrip1.BackColor = statusBackColor;
            toolStripMenuItem1.ForeColor = statusForeColor;
            toolStripMenuItem2.ForeColor = statusForeColor;

            Win81 = WindowStyle == WindowStyle.W81;
            Win7 = WindowStyle == WindowStyle.W7;
            WinVista = WindowStyle == WindowStyle.WVista;
            WinXP = WindowStyle == WindowStyle.WXP;

            if (!WinXP && !WinVista && !Win7 && !Win81)
            {
                msgLbl.ForeColor = Window1.DarkMode ? Color.White : Color.Black;
                MenuStrip1.BackColor = Window1.DarkMode ? Color.FromArgb(35, 35, 35) : Color.FromArgb(255, 255, 255);
                MenuStrip1.ForeColor = Window1.DarkMode ? Color.White : Color.Black;
            }
            else
            {
                msgLbl.ForeColor = Color.Black;
                MenuStrip1.BackColor = Color.FromArgb(255, 255, 255);
                MenuStrip1.ForeColor = Color.Black;
            }

            SetControlTheme(MenuStrip1.Handle, theme);
            SetControlTheme(VScrollBar1.Handle, theme);
            SetControlTheme(HScrollBar1.Handle, theme);
            SetControlTheme(StatusStrip1.Handle, theme);

            WXP_Alert.Visible = false;
        }

        private void SetTheme()
        {
            if (_windowStyle == WindowStyle.WXP)
            {
                Preview = Preview_Enum.WXP;

                if (_windowsXPTheme != Theme.Structures.WindowsXP.Themes.Classic && Program.ClassicThemeRunning)
                {
                    WXP_Alert.Visible = true;
                    WXP_Alert.Size = new(510, 70);
                    WXP_Alert.Location = new(7, 113);
                }
                else
                {
                    WXP_Alert.Visible = false;
                }

                switch (_windowsXPTheme)
                {
                    case Theme.Structures.WindowsXP.Themes.LunaOliveGreen:
                        {
                            msstyles = PathsExt.MSTheme_Luna_theme;
                            System.IO.File.WriteAllText(PathsExt.MSTheme_Luna_theme, $"[VisualStyles]{"\r\n"}Path={$@"{PathsExt.appData}\VisualStyles\Luna\luna.msstyles"}{"\r\n"}ColorStyle=HomeStead{"\r\n"}Size=NormalSize");
                            resVS = new(msstyles);
                            Classic = false;
                            break;
                        }

                    case Theme.Structures.WindowsXP.Themes.LunaSilver:
                        {
                            msstyles = PathsExt.MSTheme_Luna_theme;
                            System.IO.File.WriteAllText(PathsExt.MSTheme_Luna_theme, $"[VisualStyles]{"\r\n"}Path={$@"{PathsExt.appData}\VisualStyles\Luna\luna.msstyles"}{"\r\n"}ColorStyle=Metallic{"\r\n"}Size=NormalSize");
                            resVS = new(msstyles);
                            Classic = false;
                            break;
                        }

                    case Theme.Structures.WindowsXP.Themes.Custom:
                        {
                            if (System.IO.File.Exists(_windowsXPThemePath))
                            {
                                if (System.IO.Path.GetExtension(_windowsXPThemePath) == ".theme")
                                {
                                    msstyles = _windowsXPThemePath;
                                }
                                else if (System.IO.Path.GetExtension(_windowsXPThemePath) == ".msstyles")
                                {
                                    msstyles = PathsExt.MSTheme_Temp;
                                    System.IO.File.WriteAllText(PathsExt.MSTheme_Temp, $"[VisualStyles]{"\r\n"}Path={_windowsXPThemePath}{"\r\n"}ColorStyle={_windowsXPThemeColorScheme}{"\r\n"}Size=NormalSize");
                                }
                            }
                            Classic = false;
                            resVS = new(msstyles);
                            break;
                        }

                    case Theme.Structures.WindowsXP.Themes.Classic:
                        {
                            Classic = true;
                            break;
                        }

                    default:
                        {
                            msstyles = PathsExt.MSTheme_Luna_theme;
                            System.IO.File.WriteAllText(PathsExt.MSTheme_Luna_theme, $"[VisualStyles]{"\r\n"}Path={$@"{PathsExt.appData}\VisualStyles\Luna\luna.msstyles"}{"\r\n"}ColorStyle=NormalColor{"\r\n"}Size=NormalSize");
                            resVS = new(msstyles);
                            Classic = false;
                            break;
                        }
                }

                if (_windowsXPTheme != Theme.Structures.WindowsXP.Themes.Classic)
                {
                    if (System.IO.File.Exists(msstyles))
                    {
                        using (Devcorp.Controls.VisualStyles.VisualStyleFile vs = new(msstyles))
                        {
                            if (WXP_VS_ReplaceColors) HookedTM.Win32.Load(Theme.Structures.Win32UI.Sources.VisualStyles, vs.Metrics);

                            if (WXP_VS_ReplaceMetrics) HookedTM.MetricsFonts.Overwrite_Metrics(vs.Metrics);

                            if (WXP_VS_ReplaceFonts) HookedTM.MetricsFonts.Overwrite_Fonts(vs.Metrics);
                        }
                    }
                }

                Refresh();
            }

            else if (_windowStyle == WindowStyle.WVista || _windowStyle == WindowStyle.W7 || _windowStyle == WindowStyle.W81)
            {
                WinVista = _windowStyle == WindowStyle.WVista;

                switch (_windowsTheme)
                {
                    case Theme.Structures.Windows7.Themes.AeroOpaque:
                        {
                            Preview = WindowStyle != WindowStyle.W81 ? Preview_Enum.W7Opaque : Preview_Enum.W8Lite;
                            Classic = false;
                            break;
                        }

                    case Theme.Structures.Windows7.Themes.AeroLite:
                        {
                            Preview = WindowStyle == WindowStyle.W81 ? Preview_Enum.W8Lite : Preview_Enum.W7Opaque;
                            Classic = false;
                            break;
                        }

                    case Theme.Structures.Windows7.Themes.Basic:
                        {
                            Preview = Preview_Enum.W7Basic;
                            Classic = false;
                            break;
                        }

                    case Theme.Structures.Windows7.Themes.Classic:
                        {
                            Classic = true;
                            break;
                        }

                    default:
                        {
                            Preview = WindowStyle != WindowStyle.W81 ? Preview_Enum.W7Aero : Preview_Enum.W8;
                            break;
                        }
                }

                Refresh();
            }
        }

        public void LoadFromTM(Theme.Manager TM)
        {
            ForceRefresh = true;

            _hookedTM = TM;

            if (WindowStyle == WindowStyle.W11)
            {
                DarkMode_App = !HookedTM.Windows11.AppMode_Light;
                DarkMode_Win = !HookedTM.Windows11.WinMode_Light;
                TitlebarColor_Active = HookedTM.Windows11.Titlebar_Active;
                TitlebarColor_Inactive = HookedTM.Windows11.Titlebar_Inactive;
                TitlebarColor_Enabled = HookedTM.Windows11.ApplyAccentOnTitlebars;
            }

            else if (WindowStyle == WindowStyle.W10)
            {
                DarkMode_App = !HookedTM.Windows10.AppMode_Light;
                DarkMode_Win = !HookedTM.Windows10.WinMode_Light;
                TitlebarColor_Active = HookedTM.Windows10.Titlebar_Active;
                TitlebarColor_Inactive = HookedTM.Windows10.Titlebar_Inactive;
                TitlebarColor_Enabled = HookedTM.Windows10.ApplyAccentOnTitlebars;
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
                WindowsTheme = HookedTM.Windows81.Theme;
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
                WindowsTheme = HookedTM.Windows7.Theme;
            }

            else if (WindowStyle == WindowStyle.WVista)
            {
                TitlebarColor_Active = HookedTM.WindowsVista.ColorizationColor;
                TitlebarColor_Inactive = HookedTM.WindowsVista.ColorizationColor;
                Win7ColorBal = (HookedTM.WindowsVista.Alpha / 255) * 100;
                TitlebarColor_Enabled = true;
                AfterGlowColor_Active = Color.Transparent;
                AfterGlowColor_Inactive = Color.Transparent;
                Win7GlowBal = 0;
                Win7Noise = 100f;
                Win7Alpha = 100 - (HookedTM.WindowsVista.Alpha / 255) * 100;
                WindowsTheme = HookedTM.WindowsVista.Theme;
            }

            else if (WindowStyle == WindowStyle.WXP)
            {
                WindowsXPThemePath = HookedTM.WindowsXP.ThemeFile;
                WindowsXPThemeColorScheme = HookedTM.WindowsXP.ColorScheme;
                WindowsXPTheme = HookedTM.WindowsXP.Theme;
            }

            LoadMetrics(TM);
            LoadColors(TM);
            SetStyles();

            Shadow = TM.WindowsEffects.WindowShadow;

            ForceRefresh = false;
        }

        public void LoadMetrics(Theme.Manager TM)
        {
            CaptionFont = TM.MetricsFonts.CaptionFont;
            CaptionHeight = TM.MetricsFonts.CaptionHeight;
            CaptionWidth = TM.MetricsFonts.CaptionWidth;
            SmCaptionFont = TM.MetricsFonts.SmCaptionFont;
            SmCaptionHeight = TM.MetricsFonts.SmCaptionHeight;
            SmCaptionWidth = TM.MetricsFonts.SmCaptionWidth;
            BorderWidth = TM.MetricsFonts.BorderWidth;
            PaddedBorderWidth = TM.MetricsFonts.PaddedBorderWidth;

            MenuHeight = TM.MetricsFonts.MenuHeight;
            MenuFont = TM.MetricsFonts.MenuFont;

            ScrollHeight = TM.MetricsFonts.ScrollHeight;
            ScrollWidth = TM.MetricsFonts.ScrollWidth;

            MessageFont = TM.MetricsFonts.MessageFont;
            StatusFont = TM.MetricsFonts.StatusFont;
        }

        private void LoadColors(Theme.Manager TM)
        {
            EnableTheming = TM.Win32.EnableTheming;
            EnableGradient = TM.Win32.EnableGradient;
            ActiveBorder = TM.Win32.ActiveBorder;
            ActiveTitle = TM.Win32.ActiveTitle;
            Background = TM.Win32.Background;
            ButtonDkShadow = TM.Win32.ButtonDkShadow;
            ButtonFace = TM.Win32.ButtonFace;
            ButtonHilight = TM.Win32.ButtonHilight;
            ButtonLight = TM.Win32.ButtonLight;
            ButtonShadow = TM.Win32.ButtonShadow;
            ButtonText = TM.Win32.ButtonText;
            GradientActiveTitle = TM.Win32.GradientActiveTitle;
            GradientInactiveTitle = TM.Win32.GradientInactiveTitle;
            GrayText = TM.Win32.GrayText;
            Menu = TM.Win32.Menu;
            MenuBar = TM.Win32.MenuBar;
            MenuText = TM.Win32.MenuText;
            TitleText = TM.Win32.TitleText;
            Window = TM.Win32.Window;
            WindowFrame = TM.Win32.WindowFrame;
            WindowText = TM.Win32.WindowText;

            SetClassicWindowColors(HookedTM, windowR1);
            SetClassicWindowColors(HookedTM, windowR2, false);
            SetClassicWindowColors(HookedTM, windowR3);
        }

        #endregion

        private void WindowMetrics_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                WindowStyle = Program.WindowStyle;

                LoadFromTM(Program.TM);

                MenuStrip1.Renderer = new UI.WP.StripRenderer();  //Removes inferior white line from menu strip

                windowR1.EnableEditingMetrics = true;
                windowR2.EnableEditingMetrics = true;
                Window1.EnableEditingMetrics = true;
                Window2.EnableEditingMetrics = true;
                menuBarR1.EnableEditingMetrics = true;
                scrollBarR1.EnableEditingMetrics = true;
                scrollBarR2.EnableEditingMetrics = true;

                windowR1.Refresh();
                windowR2.Refresh();
                windowR3.Refresh();
            }
        }

        private void WindowMetrics_BackgroundImageChanged(object sender, EventArgs e)
        {
            tabs_preview.TabPages[0].BackgroundImage = BackgroundImage;
            tabs_preview.TabPages[1].BackgroundImage = BackgroundImage;
            tabs_preview.TabPages[2].BackgroundImage = BackgroundImage;
            tabs_preview.TabPages[3].BackgroundImage = BackgroundImage;
        }

        private void windowR1_EditorInvoker(object sender, UI.Retro.EditorEventArgs e)
        {
            if (sender == Window1)
            {
                if (e.PropertyName == nameof(Window1.Font))
                {
                    CaptionFont = Window1.Font;
                    EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(CaptionFont)));
                }
                else if (e.PropertyName == nameof(Window1.Metrics_CaptionHeight))
                {
                    CaptionHeight = Window1.Metrics_CaptionHeight;
                    EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(CaptionHeight)));
                }
                else if (e.PropertyName == nameof(Window1.Metrics_BorderWidth))
                {
                    BorderWidth = Window1.Metrics_BorderWidth;
                    EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(BorderWidth)));
                }
                else if (e.PropertyName == nameof(Window1.Metrics_PaddedBorderWidth))
                {
                    PaddedBorderWidth = Window1.Metrics_PaddedBorderWidth;
                    EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(PaddedBorderWidth)));
                }
            }

            if (sender == windowR1)
            {
                if (e.PropertyName == nameof(windowR1.Font))
                {
                    CaptionFont = windowR1.Font;
                    EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(CaptionFont)));
                }
                else if (e.PropertyName == nameof(windowR1.Metrics_CaptionHeight))
                {
                    CaptionHeight = windowR1.Metrics_CaptionHeight;
                    EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(CaptionHeight)));
                }
                else if (e.PropertyName == nameof(windowR1.Metrics_CaptionWidth))
                {
                    CaptionWidth = windowR1.Metrics_CaptionWidth;
                    EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(CaptionWidth)));
                }
                else if (e.PropertyName == nameof(windowR1.Metrics_BorderWidth))
                {
                    BorderWidth = windowR1.Metrics_BorderWidth;
                    EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(BorderWidth)));
                }
                else if (e.PropertyName == nameof(windowR1.Metrics_PaddedBorderWidth))
                {
                    PaddedBorderWidth = windowR1.Metrics_PaddedBorderWidth;
                    EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(PaddedBorderWidth)));
                }
            }
        }

        private void windowR2_EditorInvoker(object sender, UI.Retro.EditorEventArgs e)
        {
            if (sender == Window2)
            {
                if (e.PropertyName == nameof(Window2.Font))
                {
                    SmCaptionFont = Window2.Font;
                    EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(SmCaptionFont)));
                }
                else if (e.PropertyName == nameof(Window2.Metrics_CaptionHeight))
                {
                    SmCaptionHeight = Window2.Metrics_CaptionHeight;
                    EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(SmCaptionHeight)));
                }
                else if (e.PropertyName == nameof(Window2.Metrics_BorderWidth))
                {
                    BorderWidth = Window2.Metrics_BorderWidth;
                    EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(BorderWidth)));
                }
                else if (e.PropertyName == nameof(Window2.Metrics_PaddedBorderWidth))
                {
                    PaddedBorderWidth = Window2.Metrics_PaddedBorderWidth;
                    EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(PaddedBorderWidth)));
                }
            }

            if (sender == windowR2)
            {
                if (e.PropertyName == nameof(windowR2.Font))
                {
                    SmCaptionFont = windowR2.Font;
                    EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(SmCaptionFont)));
                }
                else if (e.PropertyName == nameof(windowR2.Metrics_CaptionHeight))
                {
                    SmCaptionHeight = windowR2.Metrics_CaptionHeight;
                    EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(SmCaptionHeight)));
                }
                else if (e.PropertyName == nameof(windowR2.Metrics_CaptionWidth))
                {
                    SmCaptionWidth = windowR2.Metrics_CaptionWidth;
                    EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(SmCaptionWidth)));
                }
                else if (e.PropertyName == nameof(windowR2.Metrics_BorderWidth))
                {
                    BorderWidth = windowR2.Metrics_BorderWidth;
                    EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(BorderWidth)));
                }
                else if (e.PropertyName == nameof(windowR2.Metrics_PaddedBorderWidth))
                {
                    PaddedBorderWidth = windowR2.Metrics_PaddedBorderWidth;
                    EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(PaddedBorderWidth)));
                }
            }
        }

        private void menuBarR1_EditorInvoker(object sender, EditorEventArgs e)
        {
            if (e.PropertyName == nameof(menuBarR1.MenuHeight))
            {
                MenuHeight = menuBarR1.MenuHeight;
                EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(MenuHeight)));
            }
            else if (e.PropertyName == nameof(menuBarR1.Font))
            {
                MenuFont = menuBarR1.Font;
                EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(MenuFont)));
            }
            else
            {
                EditorInvoker?.Invoke(sender, e);
            }
        }

        private void msgLbl_Click(object sender, EventArgs e)
        {
            using (FontDialog fd = new() { Font = lbl.Font })
            {
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    MessageFont = fd.Font;
                    EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(MessageFont)));
                }
            }
        }

        private void statusLbl_Click(object sender, EventArgs e)
        {
            using (FontDialog fd = new() { Font = status.Font })
            {
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    StatusFont = fd.Font;
                    EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(StatusFont)));
                }
            }
        }

        private void scrollBarR2_EditorInvoker(object sender, EditorEventArgs e)
        {
            if (e.PropertyName == nameof(ScrollHeight))
            {
                ScrollHeight = scrollBarR1.Height;
                EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(ScrollHeight)));
            }
            else if (e.PropertyName == nameof(ScrollWidth))
            {
                ScrollWidth = scrollBarR2.Width;
                EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(ScrollWidth)));
            }
            else
            {
                EditorInvoker?.Invoke(sender, e);
            }
        }

        private void VScrollBar1_SizeChanged(object sender, EventArgs e)
        {
            ScrollHeight = HScrollBar1.Height;
            EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(ScrollHeight)));

            ScrollWidth = VScrollBar1.Width;
            EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(ScrollWidth)));
        }

        private void MenuStrip1_SizeChanged(object sender, EventArgs e)
        {
            MenuHeight = MenuStrip1.Height;
            EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(MenuHeight)));
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (FontDialog fd = new() { Font = (sender as UI.WP.ToolStripMenuItem).Font })
            {
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    MenuFont = fd.Font;
                    EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(MenuFont)));
                }
            }
        }
    }
}