using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.Theme;
using WinPaletter.UI.Retro;
using WinPaletter.UI.WP;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter.Templates
{
    /// <summary>
    /// Windows classic theme with 3D effects preview template
    /// </summary>
    public partial class RetroDesktopColors : UserControl
    {
        // Cached control collections for faster access
        private List<WindowR> _windows;
        private List<ContextMenuR> _contextMenus;
        private List<ButtonR> _buttons;
        private List<MenuBarR> _menuBars;
        private List<LabelR> _labels;
        private List<ScrollBarR> _scrollBars;
        private List<AppWorkspaceR> _appWorkspaces;
        private List<WindowControlR> _windowControls;
        private List<ToolTipR> _toolTips;
        private List<PanelRaisedR> _panelRaised;
        private List<TextBoxR> _textBoxes;
        private List<PanelR> _panels;

        private bool _collectionsInitialized = false;

        /// <summary>
        /// Create a new instance of <see cref="RetroDesktopColors"/>
        /// </summary>
        public RetroDesktopColors()
        {
            DoubleBuffered = true;

            InitializeComponent();

            if (!DesignMode)
            {
                RetroShadow1.Size = ContextMenu.Size;
                RetroShadow1.Location = ContextMenu.Location + (Size)new Point(5, 5);
                RetroShadow1.Image = GetShadow();
            }
        }

        #region Properties

        private bool _enableColorsEditing = false;
        private bool _enableTheming = false;
        private bool _enableGradient = true;

        // Color fields grouped by logical categories for batch updates
        private Color _activeBorder;
        private Color _activeTitle;
        private Color _appWorkspace;
        private Color _background;
        private Color _buttonFace;
        private Color _buttonShadow;
        private Color _buttonDkShadow;
        private Color _buttonHilight;
        private Color _buttonLight;
        private Color _buttonText;
        private Color _gradientActiveTitle;
        private Color _gradientInactiveTitle;
        private Color _grayText;
        private Color _hilightText;
        private Color _inactiveBorder;
        private Color _inactiveTitle;
        private Color _inactiveTitleText;
        private Color _infoText;
        private Color _infoWindow;
        private Color _menu;
        private Color _menuBar;
        private Color _menuText;
        private Color _menuHilight;
        private Color _titleText;
        private Color _window;
        private Color _windowFrame;
        private Color _windowText;
        private Color _hilight;

        // Obsolete properties (kept for compatibility)
        public Color ButtonAlternateFace { get; set; }
        public Color HotTrackingColor { get; set; }
        public Color Scrollbar { get; set; }
        public Color Desktop { get; set; }

        /// <summary>
        /// Enable editing colors on clicking on a classic color element
        /// </summary>
        public bool EnableEditingColors
        {
            get => _enableColorsEditing;
            set
            {
                if (_enableColorsEditing == value) return;
                _enableColorsEditing = value;

                if (DesignMode) return;

                var controls = GetAllControlsOfType<Control>();
                foreach (var control in controls)
                {
                    SetControlEditingColors(control, value);
                }
            }
        }

        /// <summary>
        /// If disabled, classic 3D effects will be made to menus and menu items selection
        /// </summary>
        public bool EnableTheming
        {
            get => _enableTheming;
            set
            {
                if (_enableTheming == value) return;
                _enableTheming = value;

                var contextMenus = GetAllControlsOfType<ContextMenuR>();
                foreach (var menu in contextMenus)
                {
                    menu.Flat = value;
                }

                var menuBars = GetAllControlsOfType<MenuBarR>();
                foreach (var menuBar in menuBars)
                {
                    menuBar.Flat = value;
                }
            }
        }

        /// <summary>
        /// Enable titlebar gradience
        /// </summary>
        public bool EnableGradient
        {
            get => _enableGradient;
            set
            {
                if (_enableGradient == value) return;
                _enableGradient = value;

                var windows = GetAllControlsOfType<WindowR>();
                foreach (var window in windows)
                {
                    window.ColorGradient = value;
                }
            }
        }

        // Border colors group
        #region Border Colors
        public Color ActiveBorder
        {
            get => _activeBorder;
            set
            {
                if (_activeBorder == value) return;
                _activeBorder = value;
                UpdateActiveWindowBorders(value);
                UpdateContextMenuBorders(value);
            }
        }

        public Color InactiveBorder
        {
            get => _inactiveBorder;
            set
            {
                if (_inactiveBorder == value) return;
                _inactiveBorder = value;
                UpdateInactiveWindowBorders(value);
            }
        }

        public Color WindowFrame
        {
            get => _windowFrame;
            set
            {
                if (_windowFrame == value) return;
                _windowFrame = value;
                UpdateButtonFrames(value);
            }
        }
        #endregion

        // Titlebar colors group
        #region Titlebar Colors
        public Color ActiveTitle
        {
            get => _activeTitle;
            set
            {
                if (_activeTitle == value) return;
                _activeTitle = value;
                UpdateActiveWindowMainColor(value);
            }
        }

        public Color InactiveTitle
        {
            get => _inactiveTitle;
            set
            {
                if (_inactiveTitle == value) return;
                _inactiveTitle = value;
                UpdateInactiveWindowMainColor(value);
            }
        }

        public Color GradientActiveTitle
        {
            get => _gradientActiveTitle;
            set
            {
                if (_gradientActiveTitle == value) return;
                _gradientActiveTitle = value;
                UpdateActiveWindowGradientColor(value);
            }
        }

        public Color GradientInactiveTitle
        {
            get => _gradientInactiveTitle;
            set
            {
                if (_gradientInactiveTitle == value) return;
                _gradientInactiveTitle = value;
                UpdateInactiveWindowGradientColor(value);
            }
        }

        public Color TitleText
        {
            get => _titleText;
            set
            {
                if (_titleText == value) return;
                _titleText = value;
                UpdateActiveWindowText(value);
            }
        }

        public Color InactiveTitleText
        {
            get => _inactiveTitleText;
            set
            {
                if (_inactiveTitleText == value) return;
                _inactiveTitleText = value;
                UpdateInactiveWindowText(value);
            }
        }
        #endregion

        // Button colors group
        #region Button Colors
        public Color ButtonFace
        {
            get => _buttonFace;
            set
            {
                if (_buttonFace == value) return;
                _buttonFace = value;
                UpdateButtonFace(value);
            }
        }

        public Color ButtonText
        {
            get => _buttonText;
            set
            {
                if (_buttonText == value) return;
                _buttonText = value;
                UpdateButtonText(value);
            }
        }

        public Color ButtonShadow
        {
            get => _buttonShadow;
            set
            {
                if (_buttonShadow == value) return;
                _buttonShadow = value;
                UpdateButtonShadow(value);
            }
        }

        public Color ButtonDkShadow
        {
            get => _buttonDkShadow;
            set
            {
                if (_buttonDkShadow == value) return;
                _buttonDkShadow = value;
                UpdateButtonDarkShadow(value);
            }
        }

        public Color ButtonHilight
        {
            get => _buttonHilight;
            set
            {
                if (_buttonHilight == value) return;
                _buttonHilight = value;
                UpdateButtonHilight(value);
            }
        }

        public Color ButtonLight
        {
            get => _buttonLight;
            set
            {
                if (_buttonLight == value) return;
                _buttonLight = value;
                UpdateButtonLight(value);
            }
        }
        #endregion

        // Menu colors group
        #region Menu Colors
        public Color Menu
        {
            get => _menu;
            set
            {
                if (_menu == value) return;
                _menu = value;
                UpdateMenuBackground(value);
            }
        }

        public Color MenuBar
        {
            get => _menuBar;
            set
            {
                if (_menuBar == value) return;
                _menuBar = value;

                var menuBars = GetAllControlsOfType<MenuBarR>();
                foreach (var menuBar in menuBars)
                {
                    menuBar.MenuBar = value;
                }
            }
        }

        public Color MenuText
        {
            get => _menuText;
            set
            {
                if (_menuText == value) return;
                _menuText = value;

                var contextMenus = GetAllControlsOfType<ContextMenuR>();
                foreach (var menu in contextMenus)
                {
                    menu.ForeColor = value;
                }

                var menuBars = GetAllControlsOfType<MenuBarR>();
                foreach (var menuBar in menuBars)
                {
                    menuBar.ForeColor = value;
                }
            }
        }

        public Color MenuHilight
        {
            get => _menuHilight;
            set
            {
                if (_menuHilight == value) return;
                _menuHilight = value;
                UpdateMenuHilight(value);
            }
        }
        #endregion

        // Window/Text colors group
        #region Window and Text Colors
        public Color AppWorkspace
        {
            get => _appWorkspace;
            set
            {
                if (_appWorkspace == value) return;
                _appWorkspace = value;

                var workspaces = GetAllControlsOfType<AppWorkspaceR>();
                foreach (var workspace in workspaces)
                {
                    workspace.BackColor = value;
                }
            }
        }

        public Color Background
        {
            get => _background;
            set
            {
                if (_background == value) return;
                _background = value;
                BackColor = value;
            }
        }

        public Color Window
        {
            get => _window;
            set
            {
                if (_window == value) return;
                _window = value;

                var textBoxes = GetAllControlsOfType<TextBoxR>();
                foreach (var textBox in textBoxes)
                {
                    textBox.BackColor = value;
                }

                var windowControls = GetAllControlsOfType<WindowControlR>();
                foreach (var control in windowControls)
                {
                    control.BackColor = value;
                }
            }
        }

        public Color WindowText
        {
            get => _windowText;
            set
            {
                if (_windowText == value) return;
                _windowText = value;

                var textBoxes = GetAllControlsOfType<TextBoxR>();
                foreach (var textBox in textBoxes)
                {
                    textBox.ForeColor = value;
                }

                var labels = GetAllControlsOfType<LabelR>();
                foreach (var label in labels)
                {
                    label.ForeColor = value;
                }

                var windowControls = GetAllControlsOfType<WindowControlR>();
                foreach (var control in windowControls)
                {
                    control.ForeColor = value;
                }
            }
        }

        public Color GrayText
        {
            get => _grayText;
            set
            {
                if (_grayText == value) return;
                _grayText = value;

                var contextMenus = GetAllControlsOfType<ContextMenuR>();
                foreach (var menu in contextMenus)
                {
                    menu.GrayText = value;
                }

                var menuBars = GetAllControlsOfType<MenuBarR>();
                foreach (var menuBar in menuBars)
                {
                    menuBar.GrayText = value;
                }
            }
        }

        public Color HilightText
        {
            get => _hilightText;
            set
            {
                if (_hilightText == value) return;
                _hilightText = value;

                var contextMenus = GetAllControlsOfType<ContextMenuR>();
                foreach (var menu in contextMenus)
                {
                    menu.HilightText = value;
                }

                var menuBars = GetAllControlsOfType<MenuBarR>();
                foreach (var menuBar in menuBars)
                {
                    menuBar.HilightText = value;
                }
            }
        }

        public Color Hilight
        {
            get => _hilight;
            set
            {
                if (_hilight == value) return;
                _hilight = value;
                UpdateHilight(value);
            }
        }

        public Color InfoText
        {
            get => _infoText;
            set
            {
                if (_infoText == value) return;
                _infoText = value;

                var toolTips = GetAllControlsOfType<ToolTipR>();
                foreach (var tip in toolTips)
                {
                    tip.ForeColor = value;
                }
            }
        }

        public Color InfoWindow
        {
            get => _infoWindow;
            set
            {
                if (_infoWindow == value) return;
                _infoWindow = value;

                var toolTips = GetAllControlsOfType<ToolTipR>();
                foreach (var tip in toolTips)
                {
                    tip.BackColor = value;
                }
            }
        }
        #endregion

        #endregion

        #region Batch Update Methods

        private void SetControlEditingColors(Control control, bool enable)
        {
            if (control is WindowR window)
            {
                window.EnableEditingColors = enable;
                SetChildControlsEditingColors(window, enable);
            }
            else if (control is ContextMenuR menu) menu.EnableEditingColors = enable;
            else if (control is ButtonR button) button.EnableEditingColors = enable;
            else if (control is MenuBarR menuBar) menuBar.EnableEditingColors = enable;
            else if (control is LabelR label) label.EnableEditingColors = enable;
            else if (control is ScrollBarR scrollBar) scrollBar.EnableEditingColors = enable;
            else if (control is AppWorkspaceR workspace) workspace.EnableEditingColors = enable;
            else if (control is WindowControlR windowControl) windowControl.EnableEditingColors = enable;
            else if (control is ToolTipR toolTip) toolTip.EnableEditingColors = enable;
        }

        private void SetChildControlsEditingColors(Control parent, bool enable)
        {
            foreach (Control control in parent.Controls)
            {
                SetControlEditingColors(control, enable);
            }
        }

        private void UpdateButtonFace(Color value)
        {
            var buttons = GetAllControlsOfType<ButtonR>()
                .Where(b => b.Parent != null && !IsWindowControlButton(b));

            foreach (var button in buttons)
            {
                button.BackColor = value;
            }

            var windows = GetAllControlsOfType<WindowR>();
            foreach (var window in windows)
            {
                window.BackColor = value;
            }

            var scrollBars = GetAllControlsOfType<ScrollBarR>();
            foreach (var scrollBar in scrollBars)
            {
                scrollBar.BackColor = value;
            }

            var contextMenus = GetAllControlsOfType<ContextMenuR>();
            foreach (var menu in contextMenus)
            {
                menu.BackColor = value;
            }

            var menuBars = GetAllControlsOfType<MenuBarR>();
            foreach (var menuBar in menuBars)
            {
                menuBar.BackColor = value;
            }

            RetroShadow1.Refresh();
        }

        private bool IsWindowControlButton(ButtonR button)
        {
            string name = button.Name.ToLower();
            return name == "closebtn" || name == "maxbtn" || name == "minbtn";
        }

        private void UpdateButtonShadow(Color value)
        {
            var buttons = GetAllControlsOfType<ButtonR>()
                .Where(b => b.Parent != null && !IsWindowControlButton(b));

            foreach (var button in buttons)
            {
                button.ButtonShadow = value;
            }

            var windows = GetAllControlsOfType<WindowR>();
            foreach (var window in windows)
            {
                window.ButtonShadow = value;
            }

            var panelsRaised = GetAllControlsOfType<PanelRaisedR>();
            foreach (var panel in panelsRaised)
            {
                panel.ButtonShadow = value;
            }

            var textBoxes = GetAllControlsOfType<TextBoxR>();
            foreach (var textBox in textBoxes)
            {
                textBox.ButtonShadow = value;
            }

            var panels = GetAllControlsOfType<PanelR>();
            foreach (var panel in panels)
            {
                panel.ButtonShadow = value;
            }

            var contextMenus = GetAllControlsOfType<ContextMenuR>();
            foreach (var menu in contextMenus)
            {
                menu.ButtonShadow = value;
            }

            var menuBars = GetAllControlsOfType<MenuBarR>();
            foreach (var menuBar in menuBars)
            {
                menuBar.ButtonShadow = value;
            }

            var windowControls = GetAllControlsOfType<WindowControlR>();
            foreach (var control in windowControls)
            {
                control.ButtonShadow = value;
            }
        }

        private void UpdateButtonDarkShadow(Color value)
        {
            var buttons = GetAllControlsOfType<ButtonR>()
                .Where(b => b.Parent != null && !IsWindowControlButton(b));

            foreach (var button in buttons)
            {
                button.ButtonDkShadow = value;
            }

            var windows = GetAllControlsOfType<WindowR>();
            foreach (var window in windows)
            {
                window.ButtonDkShadow = value;
            }

            var textBoxes = GetAllControlsOfType<TextBoxR>();
            foreach (var textBox in textBoxes)
            {
                textBox.ButtonDkShadow = value;
            }

            var contextMenus = GetAllControlsOfType<ContextMenuR>();
            foreach (var menu in contextMenus)
            {
                menu.ButtonDkShadow = value;
            }

            var windowControls = GetAllControlsOfType<WindowControlR>();
            foreach (var control in windowControls)
            {
                control.ButtonDkShadow = value;
            }
        }

        private void UpdateButtonHilight(Color value)
        {
            var buttons = GetAllControlsOfType<ButtonR>()
                .Where(b => b.Parent != null && !IsWindowControlButton(b));

            foreach (var button in buttons)
            {
                button.ButtonHilight = value;
            }

            var windows = GetAllControlsOfType<WindowR>();
            foreach (var window in windows)
            {
                window.ButtonHilight = value;
            }

            var panelsRaised = GetAllControlsOfType<PanelRaisedR>();
            foreach (var panel in panelsRaised)
            {
                panel.ButtonHilight = value;
            }

            var textBoxes = GetAllControlsOfType<TextBoxR>();
            foreach (var textBox in textBoxes)
            {
                textBox.ButtonHilight = value;
            }

            var scrollBars = GetAllControlsOfType<ScrollBarR>();
            foreach (var scrollBar in scrollBars)
            {
                scrollBar.ButtonHilight = value;
            }

            var panels = GetAllControlsOfType<PanelR>();
            foreach (var panel in panels)
            {
                panel.ButtonHilight = value;
            }

            var contextMenus = GetAllControlsOfType<ContextMenuR>();
            foreach (var menu in contextMenus)
            {
                menu.ButtonHilight = value;
            }

            var menuBars = GetAllControlsOfType<MenuBarR>();
            foreach (var menuBar in menuBars)
            {
                menuBar.ButtonHilight = value;
            }

            var windowControls = GetAllControlsOfType<WindowControlR>();
            foreach (var control in windowControls)
            {
                control.ButtonHilight = value;
            }
        }

        private void UpdateButtonLight(Color value)
        {
            var buttons = GetAllControlsOfType<ButtonR>()
                .Where(b => b.Parent != null && !IsWindowControlButton(b));

            foreach (var button in buttons)
            {
                button.ButtonLight = value;
            }

            var windows = GetAllControlsOfType<WindowR>();
            foreach (var window in windows)
            {
                window.ButtonLight = value;
            }

            var textBoxes = GetAllControlsOfType<TextBoxR>();
            foreach (var textBox in textBoxes)
            {
                textBox.ButtonLight = value;
            }

            var contextMenus = GetAllControlsOfType<ContextMenuR>();
            foreach (var menu in contextMenus)
            {
                menu.ButtonLight = value;
            }

            var windowControls = GetAllControlsOfType<WindowControlR>();
            foreach (var control in windowControls)
            {
                control.ButtonLight = value;
            }
        }

        private void UpdateButtonText(Color value)
        {
            var buttons = GetAllControlsOfType<ButtonR>()
                .Where(b => b.Parent != null && !IsWindowControlButton(b));

            foreach (var button in buttons)
            {
                button.ForeColor = value;
            }

            var windows = GetAllControlsOfType<WindowR>();
            foreach (var window in windows)
            {
                window.ButtonText = value;
            }
        }

        private void UpdateButtonFrames(Color value)
        {
            var buttons = GetAllControlsOfType<ButtonR>()
                .Where(b => b.Parent != null && !IsWindowControlButton(b));

            foreach (var button in buttons)
            {
                button.WindowFrame = value;
            }
        }

        private void UpdateActiveWindowBorders(Color value)
        {
            var windows = GetAllControlsOfType<WindowR>();
            foreach (var window in windows.Where(w => w.Active))
            {
                window.ColorBorder = value;
            }
        }

        private void UpdateInactiveWindowBorders(Color value)
        {
            var windows = GetAllControlsOfType<WindowR>();
            foreach (var window in windows.Where(w => !w.Active))
            {
                window.ColorBorder = value;
            }
        }

        private void UpdateContextMenuBorders(Color value)
        {
            var contextMenus = GetAllControlsOfType<ContextMenuR>();
            foreach (var menu in contextMenus)
            {
                menu.ColorBorder = value;
            }
        }

        private void UpdateActiveWindowMainColor(Color value)
        {
            var windows = GetAllControlsOfType<WindowR>();
            foreach (var window in windows.Where(w => w.Active))
            {
                window.Color1 = value;
            }
        }

        private void UpdateInactiveWindowMainColor(Color value)
        {
            var windows = GetAllControlsOfType<WindowR>();
            foreach (var window in windows.Where(w => !w.Active))
            {
                window.Color1 = value;
            }
        }

        private void UpdateActiveWindowGradientColor(Color value)
        {
            var windows = GetAllControlsOfType<WindowR>();
            foreach (var window in windows.Where(w => w.Active))
            {
                window.Color2 = value;
            }
        }

        private void UpdateInactiveWindowGradientColor(Color value)
        {
            var windows = GetAllControlsOfType<WindowR>();
            foreach (var window in windows.Where(w => !w.Active))
            {
                window.Color2 = value;
            }
        }

        private void UpdateActiveWindowText(Color value)
        {
            var windows = GetAllControlsOfType<WindowR>();
            foreach (var window in windows.Where(w => w.Active))
            {
                window.ForeColor = value;
            }
        }

        private void UpdateInactiveWindowText(Color value)
        {
            var windows = GetAllControlsOfType<WindowR>();
            foreach (var window in windows.Where(w => !w.Active))
            {
                window.ForeColor = value;
            }
        }

        private void UpdateMenuBackground(Color value)
        {
            var contextMenus = GetAllControlsOfType<ContextMenuR>();
            foreach (var menu in contextMenus)
            {
                menu.BackColor = value;
            }

            if (!_enableTheming)
            {
                var menuBars = GetAllControlsOfType<MenuBarR>();
                foreach (var menuBar in menuBars)
                {
                    menuBar.BackColor = value;
                }

                var panels = GetAllControlsOfType<PanelR>();
                foreach (var panel in panels)
                {
                    panel.BackColor = value;
                }
            }
        }

        private void UpdateMenuHilight(Color value)
        {
            var contextMenus = GetAllControlsOfType<ContextMenuR>();
            foreach (var menu in contextMenus)
            {
                menu.MenuHilight = value;
            }

            var menuBars = GetAllControlsOfType<MenuBarR>();
            foreach (var menuBar in menuBars)
            {
                menuBar.MenuHilight = value;
            }

            if (_enableTheming)
            {
                var panelsRaised = GetAllControlsOfType<PanelRaisedR>();
                foreach (var panel in panelsRaised)
                {
                    panel.BackColor = value;
                }

                var panels = GetAllControlsOfType<PanelR>();
                foreach (var panel in panels)
                {
                    panel.BackColor = value;
                }
            }
        }

        private void UpdateHilight(Color value)
        {
            var contextMenus = GetAllControlsOfType<ContextMenuR>();
            foreach (var menu in contextMenus)
            {
                menu.Hilight = value;
            }

            var menuBars = GetAllControlsOfType<MenuBarR>();
            foreach (var menuBar in menuBars)
            {
                menuBar.Hilight = value;
            }

            if (_enableTheming)
            {
                var panelsRaised = GetAllControlsOfType<PanelRaisedR>();
                foreach (var panel in panelsRaised)
                {
                    panel.ButtonShadow = value;
                }
            }
        }

        #endregion

        #region Control Collection Management

        private void InitializeControlCollections()
        {
            if (_collectionsInitialized || DesignMode) return;

            _windows = new List<WindowR>();
            _contextMenus = new List<ContextMenuR>();
            _buttons = new List<ButtonR>();
            _menuBars = new List<MenuBarR>();
            _labels = new List<LabelR>();
            _scrollBars = new List<ScrollBarR>();
            _appWorkspaces = new List<AppWorkspaceR>();
            _windowControls = new List<WindowControlR>();
            _toolTips = new List<ToolTipR>();
            _panelRaised = new List<PanelRaisedR>();
            _textBoxes = new List<TextBoxR>();
            _panels = new List<PanelR>();

            BuildControlCollections(this);
            _collectionsInitialized = true;
        }

        private void BuildControlCollections(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                AddToCollection(control);

                if (control.HasChildren)
                {
                    BuildControlCollections(control);
                }
            }
        }

        private void AddToCollection(Control control)
        {
            if (control is WindowR window) _windows.Add(window);
            else if (control is ContextMenuR menu) _contextMenus.Add(menu);
            else if (control is ButtonR button) _buttons.Add(button);
            else if (control is MenuBarR menuBar) _menuBars.Add(menuBar);
            else if (control is LabelR label) _labels.Add(label);
            else if (control is ScrollBarR scrollBar) _scrollBars.Add(scrollBar);
            else if (control is AppWorkspaceR workspace) _appWorkspaces.Add(workspace);
            else if (control is WindowControlR windowControl) _windowControls.Add(windowControl);
            else if (control is ToolTipR toolTip) _toolTips.Add(toolTip);
            else if (control is PanelRaisedR panelRaised) _panelRaised.Add(panelRaised);
            else if (control is TextBoxR textBox) _textBoxes.Add(textBox);
            else if (control is PanelR panel) _panels.Add(panel);
        }

        private void RemoveFromCollection(Control control)
        {
            if (control is WindowR window) _windows?.Remove(window);
            else if (control is ContextMenuR menu) _contextMenus?.Remove(menu);
            else if (control is ButtonR button) _buttons?.Remove(button);
            else if (control is MenuBarR menuBar) _menuBars?.Remove(menuBar);
            else if (control is LabelR label) _labels?.Remove(label);
            else if (control is ScrollBarR scrollBar) _scrollBars?.Remove(scrollBar);
            else if (control is AppWorkspaceR workspace) _appWorkspaces?.Remove(workspace);
            else if (control is WindowControlR windowControl) _windowControls?.Remove(windowControl);
            else if (control is ToolTipR toolTip) _toolTips?.Remove(toolTip);
            else if (control is PanelRaisedR panelRaised) _panelRaised?.Remove(panelRaised);
            else if (control is TextBoxR textBox) _textBoxes?.Remove(textBox);
            else if (control is PanelR panel) _panels?.Remove(panel);
        }

        private List<T> GetAllControlsOfType<T>() where T : Control
        {
            if (DesignMode)
            {
                return this.GetAllControls().OfType<T>().ToList();
            }

            InitializeControlCollections();

            return typeof(T) switch
            {
                var t when t == typeof(WindowR) => _windows?.Cast<T>().ToList() ?? new List<T>(),
                var t when t == typeof(ContextMenuR) => _contextMenus?.Cast<T>().ToList() ?? new List<T>(),
                var t when t == typeof(ButtonR) => _buttons?.Cast<T>().ToList() ?? new List<T>(),
                var t when t == typeof(MenuBarR) => _menuBars?.Cast<T>().ToList() ?? new List<T>(),
                var t when t == typeof(LabelR) => _labels?.Cast<T>().ToList() ?? new List<T>(),
                var t when t == typeof(ScrollBarR) => _scrollBars?.Cast<T>().ToList() ?? new List<T>(),
                var t when t == typeof(AppWorkspaceR) => _appWorkspaces?.Cast<T>().ToList() ?? new List<T>(),
                var t when t == typeof(WindowControlR) => _windowControls?.Cast<T>().ToList() ?? new List<T>(),
                var t when t == typeof(ToolTipR) => _toolTips?.Cast<T>().ToList() ?? new List<T>(),
                var t when t == typeof(PanelRaisedR) => _panelRaised?.Cast<T>().ToList() ?? new List<T>(),
                var t when t == typeof(TextBoxR) => _textBoxes?.Cast<T>().ToList() ?? new List<T>(),
                var t when t == typeof(PanelR) => _panels?.Cast<T>().ToList() ?? new List<T>(),
                _ => this.GetAllControls().OfType<T>().ToList()
            };
        }

        #endregion

        #region Events/Overrides

        public delegate void EditorInvokerEventHandler(object sender, EditorEventArgs e);
        public event EditorInvokerEventHandler EditorInvoker;

        private void EditorInvoked(object sender, EditorEventArgs e)
        {
            EditorInvoker?.Invoke(sender, e);
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            if (!DesignMode)
            {
                if (_collectionsInitialized)
                {
                    AddToCollection(e.Control);
                    if (e.Control.HasChildren)
                    {
                        BuildControlCollections(e.Control);
                    }
                }

                AttachEditorEvents(e.Control);
            }

            base.OnControlAdded(e);
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            if (!DesignMode && _collectionsInitialized)
            {
                RemoveFromCollection(e.Control);
                if (e.Control.HasChildren)
                {
                    foreach (Control child in e.Control.Controls)
                    {
                        RemoveFromCollection(child);
                    }
                }

                DetachEditorEvents(e.Control);
            }

            base.OnControlRemoved(e);
        }

        private void AttachEditorEvents(Control control)
        {
            if (control is WindowR window)
            {
                window.EditorInvoker += EditorInvoked;
                AttachChildEditorEvents(window);
            }
            else if (control is ContextMenuR menu) menu.EditorInvoker += EditorInvoked;
            else if (control is ButtonR button) button.EditorInvoker += EditorInvoked;
            else if (control is MenuBarR menuBar) menuBar.EditorInvoker += EditorInvoked;
            else if (control is LabelR label) label.EditorInvoker += EditorInvoked;
            else if (control is ScrollBarR scrollBar) scrollBar.EditorInvoker += EditorInvoked;
            else if (control is AppWorkspaceR workspace) workspace.EditorInvoker += EditorInvoked;
            else if (control is WindowControlR windowControl) windowControl.EditorInvoker += EditorInvoked;
            else if (control is ToolTipR toolTip) toolTip.EditorInvoker += EditorInvoked;
        }

        private void AttachChildEditorEvents(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                AttachEditorEvents(control);
            }
        }

        private void DetachEditorEvents(Control control)
        {
            if (control is WindowR window)
            {
                window.EditorInvoker -= EditorInvoked;
                DetachChildEditorEvents(window);
            }
            else if (control is ContextMenuR menu) menu.EditorInvoker -= EditorInvoked;
            else if (control is ButtonR button) button.EditorInvoker -= EditorInvoked;
            else if (control is MenuBarR menuBar) menuBar.EditorInvoker -= EditorInvoked;
            else if (control is LabelR label) label.EditorInvoker -= EditorInvoked;
            else if (control is ScrollBarR scrollBar) scrollBar.EditorInvoker -= EditorInvoked;
            else if (control is AppWorkspaceR workspace) workspace.EditorInvoker -= EditorInvoked;
            else if (control is WindowControlR windowControl) windowControl.EditorInvoker -= EditorInvoked;
            else if (control is ToolTipR toolTip) toolTip.EditorInvoker -= EditorInvoked;
        }

        private void DetachChildEditorEvents(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                DetachEditorEvents(control);
            }
        }

        #endregion

        #region Methods

        private void RetroDesktop_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                this.DoubleBuffer();
                InitializeControlCollections();

                foreach (Control c in this.GetAllControls())
                {
                    c.DoubleBuffer();
                }
            }
        }

        private Bitmap GetShadow()
        {
            using (Bitmap b = new(RetroShadow1.Width, RetroShadow1.Height))
            using (Graphics G = Graphics.FromImage(b))
            {
                G.Clear(Color.Transparent);
                G.DrawGlow(new(5, 5, b.Width - 10 - 1, b.Height - 10 - 1), Color.FromArgb(100, 0, 0, 0));
                return new Bitmap(b);
            }
        }

        public void LoadMetrics(Manager TM)
        {
            if (TM is null) return;

            RetroShadow1.Visible = TM.WindowsEffects.WindowShadow;

            float iP = 3 + TM.MetricsFonts.PaddedBorderWidth + TM.MetricsFonts.BorderWidth;
            float iT = 4 + TM.MetricsFonts.PaddedBorderWidth + TM.MetricsFonts.BorderWidth + TM.MetricsFonts.CaptionHeight + GetTitlebarTextHeight(TM.MetricsFonts.CaptionFont);
            Padding _Padding = new((int)iP, (int)iT, (int)iP, (int)iP);

            // Batch update fonts and metrics
            var contextMenus = GetAllControlsOfType<ContextMenuR>();
            foreach (var menu in contextMenus)
            {
                menu.Font = TM.MetricsFonts.MenuFont;
            }

            var labels = GetAllControlsOfType<LabelR>();
            foreach (var label in labels)
            {
                label.Font = TM.MetricsFonts.MessageFont;
            }

            var scrollBars = GetAllControlsOfType<ScrollBarR>();
            foreach (var scrollBar in scrollBars)
            {
                scrollBar.Width = TM.MetricsFonts.ScrollWidth;
            }

            var toolTips = GetAllControlsOfType<ToolTipR>();
            foreach (var tip in toolTips)
            {
                tip.Font = TM.MetricsFonts.CaptionFont;
            }

            var menuBars = GetAllControlsOfType<MenuBarR>();
            foreach (var menuBar in menuBars)
            {
                menuBar.MenuHeight = TM.MetricsFonts.MenuHeight;
                menuBar.Font = TM.MetricsFonts.MessageFont;
            }

            var buttons = GetAllControlsOfType<ButtonR>();
            foreach (var button in buttons)
            {
                button.FocusRectWidth = (int)TM.WindowsEffects.FocusRectWidth;
                button.FocusRectHeight = (int)TM.WindowsEffects.FocusRectHeight;
            }

            var windows = GetAllControlsOfType<WindowR>();
            foreach (var window in windows)
            {
                SetClassicWindowMetrics(TM, window);
                window.Padding = _Padding;
            }

            // Set correct sizes of windows according to the metrics
            WindowR3.Height = (int)(90 + TM.MetricsFonts.PaddedBorderWidth + TM.MetricsFonts.BorderWidth + GetTitlebarTextHeight(WindowR3.Font));
            WindowR2.Height = (int)(120 + TM.MetricsFonts.PaddedBorderWidth + TM.MetricsFonts.BorderWidth + GetTitlebarTextHeight(WindowR2.Font) + TM.MetricsFonts.MenuHeight);

            WindowR3.Top = WindowR2.Top + windowControlR1.Top + windowControlR1.Font.Height + 10;
            WindowR3.Left = WindowR2.Left + windowControlR1.Left + 15;

            CenterControls();
        }

        private void CenterControls()
        {
            var positions = new List<int>[4] { new(), new(), new(), new() };

            foreach (Control control in Controls)
            {
                if (control is not TransparentPictureBox && control is not ContextMenuR)
                {
                    positions[0].Add(control.Top);
                    positions[1].Add(control.Left);
                    positions[2].Add(control.Bottom);
                    positions[3].Add(control.Right);
                }
            }

            if (positions[0].Count == 0) return;

            Rectangle rectangle = new(
                positions[1].Min(),
                positions[0].Min(),
                positions[3].Max() - positions[1].Min(),
                positions[2].Max() - positions[0].Min()
            );

            Rectangle centerRect = new(
                0 + (Width - rectangle.Width) / 2,
                0 + (Height - rectangle.Height) / 2,
                rectangle.Width,
                rectangle.Height
            );

            foreach (Control control in Controls)
            {
                if (control is not TransparentPictureBox && control is not ContextMenuR)
                {
                    control.Top = centerRect.Top + (control.Top - rectangle.Top);
                    control.Left = centerRect.Left + (control.Left - rectangle.Left);
                }
            }
        }

        public void LoadFromWinThemeString(string DB, string ThemeName)
        {
            if (string.IsNullOrWhiteSpace(DB) || !DB.Contains("|") || string.IsNullOrWhiteSpace(ThemeName)) return;

            string selectedTheme = FindThemeInDatabase(DB, ThemeName);
            if (string.IsNullOrEmpty(selectedTheme)) return;

            Visible = false;

            bool foundGradientActive = false;
            bool foundGradientInactive = false;

            foreach (string item in selectedTheme.Split('\n'))
            {
                ParseThemeItem(item, ref foundGradientActive, ref foundGradientInactive);
            }

            Refresh();
            Visible = true;
            RetroShadow1.Refresh();
        }

        private string FindThemeInDatabase(string db, string themeName)
        {
            foreach (string theme in db.Split('\n'))
            {
                if ((theme.Split('|')[0].ToLower() ?? "") == (themeName.ToLower() ?? ""))
                {
                    return theme.Replace("|", "\r\n");
                }
            }
            return null;
        }

        private void ParseThemeItem(string item, ref bool foundGradientActive, ref bool foundGradientInactive)
        {
            string x = item.ToLower();
            string[] parts = x.Split('=');
            if (parts.Length < 2) return;

            string value = parts[1];

            switch (parts[0])
            {
                case "activetitle":
                    ActiveTitle = value.ToColorFromWin32();
                    if (!foundGradientActive)
                        GradientActiveTitle = ActiveTitle;
                    break;
                case "gradientactivetitle":
                    GradientActiveTitle = value.ToColorFromWin32();
                    foundGradientActive = true;
                    break;
                case "inactivetitle":
                    InactiveTitle = value.ToColorFromWin32();
                    if (!foundGradientInactive)
                        GradientInactiveTitle = InactiveTitle;
                    break;
                case "gradientinactivetitle":
                    GradientInactiveTitle = value.ToColorFromWin32();
                    foundGradientInactive = true;
                    break;
                case "background":
                    Background = value.ToColorFromWin32();
                    break;
                case "hilight":
                    Hilight = value.ToColorFromWin32();
                    break;
                case "hilighttext":
                    HilightText = value.ToColorFromWin32();
                    break;
                case "titletext":
                    TitleText = value.ToColorFromWin32();
                    break;
                case "window":
                    Window = value.ToColorFromWin32();
                    break;
                case "windowtext":
                    WindowText = value.ToColorFromWin32();
                    break;
                case "scrollbar":
                    Scrollbar = value.ToColorFromWin32();
                    break;
                case "menu":
                    Menu = value.ToColorFromWin32();
                    break;
                case "windowframe":
                    WindowFrame = value.ToColorFromWin32();
                    break;
                case "menutext":
                    MenuText = value.ToColorFromWin32();
                    break;
                case "activeborder":
                    ActiveBorder = value.ToColorFromWin32();
                    break;
                case "inactiveborder":
                    InactiveBorder = value.ToColorFromWin32();
                    break;
                case "appworkspace":
                    AppWorkspace = value.ToColorFromWin32();
                    break;
                case "buttonface":
                    ButtonFace = value.ToColorFromWin32();
                    break;
                case "buttonshadow":
                    ButtonShadow = value.ToColorFromWin32();
                    break;
                case "graytext":
                    GrayText = value.ToColorFromWin32();
                    break;
                case "buttontext":
                    ButtonText = value.ToColorFromWin32();
                    break;
                case "inactivetitletext":
                    InactiveTitleText = value.ToColorFromWin32();
                    break;
                case "buttonhilight":
                    ButtonHilight = value.ToColorFromWin32();
                    break;
                case "buttondkshadow":
                    ButtonDkShadow = value.ToColorFromWin32();
                    break;
                case "buttonlight":
                    ButtonLight = value.ToColorFromWin32();
                    break;
                case "infotext":
                    InfoText = value.ToColorFromWin32();
                    break;
                case "infowindow":
                    InfoWindow = value.ToColorFromWin32();
                    break;
                case "hottrackingcolor":
                    HotTrackingColor = value.ToColorFromWin32();
                    break;
                case "buttonalternateface":
                    ButtonAlternateFace = value.ToColorFromWin32();
                    break;
                case "menubar":
                    MenuBar = value.ToColorFromWin32();
                    break;
                case "menuhilight":
                    MenuHilight = value.ToColorFromWin32();
                    break;
                case "desktop":
                    Desktop = value.ToColorFromWin32();
                    break;
            }
        }

        public void LoadColors(Manager TM)
        {
            if (TM == null) return;
            LoadColorsFromStructure(TM.Win32);
        }

        public void LoadColors(Theme.Structures.Win32UI win32ui)
        {
            LoadColorsFromStructure(win32ui);
        }

        private void LoadColorsFromStructure(Theme.Structures.Win32UI win32ui)
        {
            Visible = false;

            // Batch assign all colors
            EnableTheming = win32ui.EnableTheming;
            EnableGradient = win32ui.EnableGradient;
            ActiveBorder = win32ui.ActiveBorder;
            ActiveTitle = win32ui.ActiveTitle;
            AppWorkspace = win32ui.AppWorkspace;
            Background = win32ui.Background;
            ButtonAlternateFace = win32ui.ButtonAlternateFace;
            ButtonDkShadow = win32ui.ButtonDkShadow;
            ButtonFace = win32ui.ButtonFace;
            ButtonHilight = win32ui.ButtonHilight;
            ButtonLight = win32ui.ButtonLight;
            ButtonShadow = win32ui.ButtonShadow;
            ButtonText = win32ui.ButtonText;
            GradientActiveTitle = win32ui.GradientActiveTitle;
            GradientInactiveTitle = win32ui.GradientInactiveTitle;
            GrayText = win32ui.GrayText;
            HilightText = win32ui.HilightText;
            HotTrackingColor = win32ui.HotTrackingColor;
            InactiveBorder = win32ui.InactiveBorder;
            InactiveTitle = win32ui.InactiveTitle;
            InactiveTitleText = win32ui.InactiveTitleText;
            InfoText = win32ui.InfoText;
            InfoWindow = win32ui.InfoWindow;
            Menu = win32ui.Menu;
            MenuBar = win32ui.MenuBar;
            MenuText = win32ui.MenuText;
            MenuHilight = win32ui.MenuHilight;
            Scrollbar = win32ui.Scrollbar;
            TitleText = win32ui.TitleText;
            Window = win32ui.Window;
            WindowFrame = win32ui.WindowFrame;
            WindowText = win32ui.WindowText;
            Hilight = win32ui.Hilight;
            Desktop = win32ui.Background;

            Visible = true;
        }

        public void LoadColors(RetroDesktopColors retroDesktopColors)
        {
            if (retroDesktopColors == null) return;

            Visible = false;

            // Batch copy all properties
            EnableTheming = retroDesktopColors.EnableTheming;
            EnableGradient = retroDesktopColors.EnableGradient;
            ActiveBorder = retroDesktopColors.ActiveBorder;
            ActiveTitle = retroDesktopColors.ActiveTitle;
            AppWorkspace = retroDesktopColors.AppWorkspace;
            Background = retroDesktopColors.Background;
            ButtonAlternateFace = retroDesktopColors.ButtonAlternateFace;
            ButtonDkShadow = retroDesktopColors.ButtonDkShadow;
            ButtonFace = retroDesktopColors.ButtonFace;
            ButtonHilight = retroDesktopColors.ButtonHilight;
            ButtonLight = retroDesktopColors.ButtonLight;
            ButtonShadow = retroDesktopColors.ButtonShadow;
            ButtonText = retroDesktopColors.ButtonText;
            GradientActiveTitle = retroDesktopColors.GradientActiveTitle;
            GradientInactiveTitle = retroDesktopColors.GradientInactiveTitle;
            GrayText = retroDesktopColors.GrayText;
            HilightText = retroDesktopColors.HilightText;
            HotTrackingColor = retroDesktopColors.HotTrackingColor;
            InactiveBorder = retroDesktopColors.InactiveBorder;
            InactiveTitle = retroDesktopColors.InactiveTitle;
            InactiveTitleText = retroDesktopColors.InactiveTitleText;
            InfoText = retroDesktopColors.InfoText;
            InfoWindow = retroDesktopColors.InfoWindow;
            Menu = retroDesktopColors.Menu;
            MenuBar = retroDesktopColors.MenuBar;
            MenuText = retroDesktopColors.MenuText;
            MenuHilight = retroDesktopColors.MenuHilight;
            Scrollbar = retroDesktopColors.Scrollbar;
            TitleText = retroDesktopColors.TitleText;
            Window = retroDesktopColors.Window;
            WindowFrame = retroDesktopColors.WindowFrame;
            WindowText = retroDesktopColors.WindowText;
            Hilight = retroDesktopColors.Hilight;
            Desktop = retroDesktopColors.Background;

            Visible = true;
        }

        private void WindowR4_SizeChanged(object sender, EventArgs e)
        {
            UpdateToolTipPosition();
        }

        private void WindowR4_LocationChanged(object sender, EventArgs e)
        {
            UpdateToolTipPosition();
        }

        private void UpdateToolTipPosition()
        {
            toolTipR1.Top = WindowR4.Top + WindowR4.Metrics_CaptionHeight + 2;
            toolTipR1.Left = WindowR4.Right - WindowR4.Metrics_CaptionWidth - 2;
        }

        private void WindowR2_SizeChanged(object sender, EventArgs e)
        {
            UpdateContextMenuPosition();
        }

        private void WindowR2_LocationChanged(object sender, EventArgs e)
        {
            UpdateContextMenuPosition();
        }

        private void UpdateContextMenuPosition()
        {
            ContextMenu.Top = WindowR2.Top + menuBarR1.Top + menuBarR1.Height;
            ContextMenu.Left = Math.Min(
                WindowR2.Left + menuBarR1.Left + menuBarR1.SelectedItemLocation.X,
                WindowR2.Right - WindowR2.Metrics_PaddedBorderWidth - WindowR2.Metrics_BorderWidth
            );
        }

        private void ContextMenu_SizeChanged(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                RetroShadow1.Size = ContextMenu.Size;
                RetroShadow1.Image = GetShadow();
            }
        }

        private void ContextMenu_LocationChanged(object sender, EventArgs e)
        {
            if (!DesignMode)
                RetroShadow1.Location = ContextMenu.Location + (Size)new Point(5, 5);
        }

        #endregion

        #region Overrides

        private bool _cursorMovingInBackground;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (EnableEditingColors)
            {
                _cursorMovingInBackground = true;
                Refresh();
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (EnableEditingColors)
            {
                _cursorMovingInBackground = false;
                Refresh();
            }
            base.OnMouseLeave(e);
        }

        protected override void OnClick(EventArgs e)
        {
            if (!DesignMode && EnableEditingColors && _cursorMovingInBackground)
            {
                EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Background)));
            }
            base.OnClick(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (EnableEditingColors && _cursorMovingInBackground)
            {
                Color color = Color.FromArgb(80, BackColor.IsDark() ? Color.White : Color.Black);
                using (HatchBrush hb = new(HatchStyle.Percent10, color, Color.Transparent))
                {
                    e.Graphics.FillRectangle(hb, e.ClipRectangle);
                }
            }
            base.OnPaint(e);
        }

        #endregion
    }
}