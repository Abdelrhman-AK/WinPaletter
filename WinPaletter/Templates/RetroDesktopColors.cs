using Newtonsoft.Json.Linq;
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
        private List<WindowR> _windows = [];
        private List<ContextMenuR> _contextMenus = [];
        private List<ButtonR> _buttons = [];
        private List<MenuBarR> _menuBars = [];
        private List<LabelR> _labels = [];
        private List<ScrollBarR> _scrollBars = [];
        private List<AppWorkspaceR> _appWorkspaces = [];
        private List<WindowControlR> _windowControls = [];
        private List<ToolTipR> _toolTips = [];
        private List<PanelRaisedR> _panelRaised = [];
        private List<TextBoxR> _textBoxes = [];
        private List<PanelR> _panels = [];
        private List<ButtonR> _nonWindowButtons = [];

        private bool _collectionsInitialized = false;

        /// <summary>
        /// Create a new instance of <see cref="RetroDesktopColors"/>
        /// </summary>
        public RetroDesktopColors()
        {
            DoubleBuffered = true;

            InitializeComponent();
            InitializeControlCollections();

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

                SetEditingColors(value);
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

                foreach (ContextMenuR menu in GetContextMenus()) menu.Flat = value;
                foreach (MenuBarR menuBar in GetMenuBars()) menuBar.Flat = value;
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

                foreach (WindowR window in GetWindows()) window.ColorGradient = value;
            }
        }

        #region Border Colors

        public Color ActiveBorder
        {
            get => _activeBorder;
            set
            {
                if (_activeBorder == value) return;
                _activeBorder = value;

                foreach (WindowR window in GetWindows())
                {
                    if (window.Active) window.ColorBorder = value;
                }

                foreach (ContextMenuR menu in GetContextMenus()) menu.ButtonShadow = value;
            }
        }

        public Color InactiveBorder
        {
            get => _inactiveBorder;
            set
            {
                if (_inactiveBorder == value) return;
                _inactiveBorder = value;

                foreach (WindowR window in GetWindows())
                {
                    if (!window.Active) window.ColorBorder = value;
                }
            }
        }

        public Color WindowFrame
        {
            get => _windowFrame;
            set
            {
                if (_windowFrame == value) return;
                _windowFrame = value;

                foreach (ButtonR button in GetNonWindowButtons()) button.WindowFrame = value;
                foreach (ToolTipR tooltip in GetToolTips()) tooltip.WindowFrame = value;
            }
        }

        #endregion

        #region Titlebar Colors

        public Color ActiveTitle
        {
            get => _activeTitle;
            set
            {
                if (_activeTitle == value) return;
                _activeTitle = value;

                foreach (WindowR window in GetWindows())
                {
                    if (window.Active) window.Color1 = value;
                }
            }
        }

        public Color InactiveTitle
        {
            get => _inactiveTitle;
            set
            {
                if (_inactiveTitle == value) return;
                _inactiveTitle = value;

                foreach (WindowR window in GetWindows())
                {
                    if (!window.Active) window.Color1 = value;
                }
            }
        }

        public Color GradientActiveTitle
        {
            get => _gradientActiveTitle;
            set
            {
                if (_gradientActiveTitle == value) return;
                _gradientActiveTitle = value;

                foreach (WindowR window in GetWindows())
                {
                    if (window.Active) window.Color2 = value;
                }
            }
        }

        public Color GradientInactiveTitle
        {
            get => _gradientInactiveTitle;
            set
            {
                if (_gradientInactiveTitle == value) return;
                _gradientInactiveTitle = value;

                foreach (WindowR window in GetWindows())
                {
                    if (!window.Active) window.Color2 = value;
                }
            }
        }

        public Color TitleText
        {
            get => _titleText;
            set
            {
                if (_titleText == value) return;
                _titleText = value;

                foreach (WindowR window in GetWindows())
                {
                    if (window.Active) window.ForeColor = value;
                }
            }
        }

        public Color InactiveTitleText
        {
            get => _inactiveTitleText;
            set
            {
                if (_inactiveTitleText == value) return;
                _inactiveTitleText = value;

                foreach (WindowR window in GetWindows())
                {
                    if (!window.Active) window.ForeColor = value;
                }
            }
        }

        #endregion

        #region Button Colors

        public Color ButtonFace
        {
            get => _buttonFace;
            set
            {
                if (_buttonFace == value) return;
                _buttonFace = value;

                foreach (ButtonR button in GetNonWindowButtons()) button.BackColor = value;
                foreach (WindowR window in GetWindows()) window.BackColor = value;
                foreach (ScrollBarR scrollBar in GetScrollBars()) scrollBar.BackColor = value;
                foreach (ContextMenuR menu in GetContextMenus()) menu.BackColor = value;
                foreach (MenuBarR menuBar in GetMenuBars()) menuBar.BackColor = value;

                RetroShadow1.Invalidate();
            }
        }

        public Color ButtonText
        {
            get => _buttonText;
            set
            {
                if (_buttonText == value) return;
                _buttonText = value;

                foreach (ButtonR button in GetNonWindowButtons()) button.ForeColor = value;
                foreach (WindowR window in GetWindows()) window.ButtonText = value;
            }
        }

        public Color ButtonShadow
        {
            get => _buttonShadow;
            set
            {
                if (_buttonShadow == value) return;
                _buttonShadow = value;

                foreach (ButtonR button in GetNonWindowButtons()) button.ButtonShadow = value;
                foreach (WindowR window in GetWindows()) window.ButtonShadow = value;
                foreach (PanelRaisedR panel in GetPanelsRaised()) panel.ButtonShadow = value;
                foreach (TextBoxR textBox in GetTextBoxes()) textBox.ButtonShadow = value;
                foreach (PanelR panel in GetPanels()) panel.ButtonShadow = value;
                foreach (ContextMenuR menu in GetContextMenus()) menu.ButtonShadow = value;
                foreach (MenuBarR menuBar in GetMenuBars()) menuBar.ButtonShadow = value;
                foreach (WindowControlR control in GetWindowControls()) control.ButtonShadow = value;
            }
        }

        public Color ButtonDkShadow
        {
            get => _buttonDkShadow;
            set
            {
                if (_buttonDkShadow == value) return;
                _buttonDkShadow = value;

                foreach (ButtonR button in GetNonWindowButtons()) button.ButtonDkShadow = value;
                foreach (WindowR window in GetWindows()) window.ButtonDkShadow = value;
                foreach (TextBoxR textBox in GetTextBoxes()) textBox.ButtonDkShadow = value;
                foreach (ContextMenuR menu in GetContextMenus()) menu.ButtonDkShadow = value;
                foreach (WindowControlR control in GetWindowControls()) control.ButtonDkShadow = value;
            }
        }

        public Color ButtonHilight
        {
            get => _buttonHilight;
            set
            {
                if (_buttonHilight == value) return;
                _buttonHilight = value;

                foreach (ButtonR button in GetNonWindowButtons()) button.ButtonHilight = value;
                foreach (WindowR window in GetWindows()) window.ButtonHilight = value;
                foreach (PanelRaisedR panel in GetPanelsRaised()) panel.ButtonHilight = value;
                foreach (TextBoxR textBox in GetTextBoxes()) textBox.ButtonHilight = value;
                foreach (ScrollBarR scrollBar in GetScrollBars()) scrollBar.ButtonHilight = value;
                foreach (PanelR panel in GetPanels()) panel.ButtonHilight = value;
                foreach (ContextMenuR menu in GetContextMenus()) menu.ButtonHilight = value;
                foreach (MenuBarR menuBar in GetMenuBars()) menuBar.ButtonHilight = value;
                foreach (WindowControlR control in GetWindowControls()) control.ButtonHilight = value;
            }
        }

        public Color ButtonLight
        {
            get => _buttonLight;
            set
            {
                if (_buttonLight == value) return;
                _buttonLight = value;

                foreach (ButtonR button in GetNonWindowButtons()) button.ButtonLight = value;
                foreach (WindowR window in GetWindows()) window.ButtonLight = value;
                foreach (TextBoxR textBox in GetTextBoxes()) textBox.ButtonLight = value;
                foreach (ContextMenuR menu in GetContextMenus()) menu.ButtonLight = value;
                foreach (WindowControlR control in GetWindowControls()) control.ButtonLight = value;
            }
        }

        #endregion

        #region Menu Colors

        public Color Menu
        {
            get => _menu;
            set
            {
                if (_menu == value) return;
                _menu = value;

                foreach (ContextMenuR menu in GetContextMenus()) menu.BackColor = value;

                if (!_enableTheming)
                {
                    foreach (MenuBarR menuBar in GetMenuBars()) menuBar.BackColor = value;
                    foreach (PanelR panel in GetPanels()) panel.BackColor = value;
                }
            }
        }

        public Color MenuBar
        {
            get => _menuBar;
            set
            {
                if (_menuBar == value) return;
                _menuBar = value;

                foreach (MenuBarR menuBar in GetMenuBars()) menuBar.MenuBar = value;
            }
        }

        public Color MenuText
        {
            get => _menuText;
            set
            {
                if (_menuText == value) return;
                _menuText = value;

                foreach (ContextMenuR menu in GetContextMenus()) menu.ForeColor = value;
                foreach (MenuBarR menuBar in GetMenuBars()) menuBar.ForeColor = value;
            }
        }

        public Color MenuHilight
        {
            get => _menuHilight;
            set
            {
                if (_menuHilight == value) return;
                _menuHilight = value;

                foreach (ContextMenuR menu in GetContextMenus()) menu.MenuHilight = value;
                foreach (MenuBarR menuBar in GetMenuBars()) menuBar.MenuHilight = value;

                if (_enableTheming)
                {
                    foreach (PanelRaisedR panel in GetPanelsRaised()) panel.BackColor = value;
                    foreach (PanelR panel in GetPanels()) panel.BackColor = value;
                }
            }
        }

        #endregion

        #region Window and Text Colors

        public Color AppWorkspace
        {
            get => _appWorkspace;
            set
            {
                if (_appWorkspace == value) return;
                _appWorkspace = value;

                foreach (AppWorkspaceR workspace in GetAppWorkspaces()) workspace.BackColor = value;
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

                foreach (TextBoxR textBox in GetTextBoxes()) textBox.BackColor = value;
                foreach (WindowControlR control in GetWindowControls()) control.BackColor = value;
            }
        }

        public Color WindowText
        {
            get => _windowText;
            set
            {
                if (_windowText == value) return;
                _windowText = value;

                foreach (TextBoxR textBox in GetTextBoxes()) textBox.ForeColor = value;
                foreach (LabelR label in GetLabels()) label.ForeColor = value;
                foreach (WindowControlR control in GetWindowControls()) control.ForeColor = value;
            }
        }

        public Color GrayText
        {
            get => _grayText;
            set
            {
                if (_grayText == value) return;
                _grayText = value;

                foreach (ContextMenuR menu in GetContextMenus()) menu.GrayText = value;
                foreach (MenuBarR menuBar in GetMenuBars()) menuBar.GrayText = value;
            }
        }

        public Color HilightText
        {
            get => _hilightText;
            set
            {
                if (_hilightText == value) return;
                _hilightText = value;

                foreach (ContextMenuR menu in GetContextMenus()) menu.HilightText = value;
                foreach (MenuBarR menuBar in GetMenuBars()) menuBar.HilightText = value;
            }
        }

        public Color Hilight
        {
            get => _hilight;
            set
            {
                if (_hilight == value) return;
                _hilight = value;

                foreach (ContextMenuR menu in GetContextMenus()) menu.Hilight = value;
                foreach (MenuBarR menuBar in GetMenuBars()) menuBar.Hilight = value;

                if (_enableTheming)
                {
                    foreach (PanelRaisedR panel in GetPanelsRaised()) panel.ButtonShadow = value;
                }
            }
        }

        public Color InfoText
        {
            get => _infoText;
            set
            {
                if (_infoText == value) return;
                _infoText = value;

                foreach (ToolTipR tip in GetToolTips()) tip.ForeColor = value;
            }
        }

        public Color InfoWindow
        {
            get => _infoWindow;
            set
            {
                if (_infoWindow == value) return;
                _infoWindow = value;

                foreach (ToolTipR tip in GetToolTips()) tip.BackColor = value;
            }
        }

        #endregion

        #endregion

        #region Control Collection Management

        private void InitializeControlCollections()
        {
            if (_collectionsInitialized || DesignMode) return;

            _windows = [];
            _contextMenus = [];
            _buttons = [];
            _menuBars = [];
            _labels = [];
            _scrollBars = [];
            _appWorkspaces = [];
            _windowControls = [];
            _toolTips = [];
            _panelRaised = [];
            _textBoxes = [];
            _panels = [];
            _nonWindowButtons = [];

            foreach (Control control in this.GetAllControls())
            {
                AddToCollection(control);
                control.DoubleBuffer();
            }

            _collectionsInitialized = true;
        }

        private void AddToCollection(Control control)
        {
            if (control is WindowR window) _windows.Add(window);
            else if (control is ContextMenuR menu) _contextMenus.Add(menu);
            else if (control is ButtonR button)
            {
                _buttons.Add(button);
                if (button.Parent != null && !IsWindowControlButton(button))
                    _nonWindowButtons.Add(button);
            }
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
            else if (control is ButtonR button)
            {
                _buttons?.Remove(button);
                _nonWindowButtons?.Remove(button);
            }
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

        private bool IsWindowControlButton(ButtonR button)
        {
            string name = button.Name.ToLower();
            return name == "closebtn" || name == "maxbtn" || name == "minbtn";
        }

        // Typed accessors — return the cached list directly (zero allocation).
        // In design mode they fall back to a live scan.
        private List<WindowR> GetWindows() => DesignMode ? [.. this.GetAllControls().OfType<WindowR>()] : _windows;
        private List<ContextMenuR> GetContextMenus() => DesignMode ? [.. this.GetAllControls().OfType<ContextMenuR>()] : _contextMenus;
        private List<ButtonR> GetButtons() => DesignMode ? [.. this.GetAllControls().OfType<ButtonR>()] : _buttons;
        private List<ButtonR> GetNonWindowButtons() => DesignMode ? [.. this.GetAllControls().OfType<ButtonR>().Where(b => b.Parent != null && !IsWindowControlButton(b))] : _nonWindowButtons;
        private List<MenuBarR> GetMenuBars() => DesignMode ? [.. this.GetAllControls().OfType<MenuBarR>()] : _menuBars;
        private List<LabelR> GetLabels() => DesignMode ? [.. this.GetAllControls().OfType<LabelR>()] : _labels;
        private List<ScrollBarR> GetScrollBars() => DesignMode ? [.. this.GetAllControls().OfType<ScrollBarR>()] : _scrollBars;
        private List<AppWorkspaceR> GetAppWorkspaces() => DesignMode ? [.. this.GetAllControls().OfType<AppWorkspaceR>()] : _appWorkspaces;
        private List<WindowControlR> GetWindowControls() => DesignMode ? [.. this.GetAllControls().OfType<WindowControlR>()] : _windowControls;
        private List<ToolTipR> GetToolTips() => DesignMode ? [.. this.GetAllControls().OfType<ToolTipR>()] : _toolTips;
        private List<PanelRaisedR> GetPanelsRaised() => DesignMode ? [.. this.GetAllControls().OfType<PanelRaisedR>()] : _panelRaised;
        private List<TextBoxR> GetTextBoxes() => DesignMode ? [.. this.GetAllControls().OfType<TextBoxR>()] : _textBoxes;
        private List<PanelR> GetPanels() => DesignMode ? [.. this.GetAllControls().OfType<PanelR>()] : _panels;

        #endregion

        #region Events

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
                        foreach (Control child in e.Control.GetAllControls())
                        {
                            AddToCollection(child);
                        }
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
                    foreach (Control child in e.Control.GetAllControls())
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
                foreach (Control child in window.GetAllControls())
                {
                    AttachEditorEvents(child);
                }
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

        private void DetachEditorEvents(Control control)
        {
            if (control is WindowR window)
            {
                window.EditorInvoker -= EditorInvoked;
                foreach (Control child in window.GetAllControls())
                {
                    DetachEditorEvents(child);
                }
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

        #endregion

        #region Methods

        private void SetEditingColors(bool value)
        {
            foreach (WindowR window in GetWindows()) window.EnableEditingColors = value;
            foreach (ContextMenuR menu in GetContextMenus()) menu.EnableEditingColors = value;
            foreach (ButtonR button in GetButtons()) button.EnableEditingColors = value;
            foreach (MenuBarR menuBar in GetMenuBars()) menuBar.EnableEditingColors = value;
            foreach (LabelR label in GetLabels()) label.EnableEditingColors = value;
            foreach (ScrollBarR scrollBar in GetScrollBars()) scrollBar.EnableEditingColors = value;
            foreach (AppWorkspaceR workspace in GetAppWorkspaces()) workspace.EnableEditingColors = value;
            foreach (WindowControlR windowControl in GetWindowControls()) windowControl.EnableEditingColors = value;
            foreach (ToolTipR toolTip in GetToolTips()) toolTip.EnableEditingColors = value;
        }

        private void RetroDesktop_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                this.DoubleBuffer();
                InitializeControlCollections();
                SetEditingColors(EnableEditingColors);
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

            foreach (ContextMenuR menu in GetContextMenus()) menu.Font = TM.MetricsFonts.MenuFont;
            foreach (LabelR label in GetLabels()) label.Font = TM.MetricsFonts.MessageFont;
            foreach (ScrollBarR scrollBar in GetScrollBars()) scrollBar.Width = TM.MetricsFonts.ScrollWidth;
            foreach (ToolTipR tip in GetToolTips()) tip.Font = TM.MetricsFonts.CaptionFont;

            foreach (MenuBarR menuBar in GetMenuBars())
            {
                menuBar.MenuHeight = TM.MetricsFonts.MenuHeight;
                menuBar.Font = TM.MetricsFonts.MessageFont;
            }

            foreach (ButtonR button in GetButtons())
            {
                button.FocusRectWidth = (int)TM.WindowsEffects.FocusRectWidth;
                button.FocusRectHeight = (int)TM.WindowsEffects.FocusRectHeight;
            }

            foreach (WindowR window in GetWindows())
            {
                SetClassicWindowMetrics(TM, window);
                window.Padding = _Padding;
            }

            WindowR3.Height = (int)(90 + TM.MetricsFonts.PaddedBorderWidth + TM.MetricsFonts.BorderWidth + GetTitlebarTextHeight(WindowR3.Font));
            WindowR2.Height = (int)(120 + TM.MetricsFonts.PaddedBorderWidth + TM.MetricsFonts.BorderWidth + GetTitlebarTextHeight(WindowR2.Font) + TM.MetricsFonts.MenuHeight);

            WindowR3.Top = WindowR2.Top + windowControlR1.Top + windowControlR1.Font.Height + 10;
            WindowR3.Left = WindowR2.Left + windowControlR1.Left + 15;

            CenterControls();
        }

        private void CenterControls()
        {
            List<int>[] positions = [[], [], [], []];

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

            Rectangle rectangle = new(positions[1].Min(), positions[0].Min(), positions[3].Max() - positions[1].Min(), positions[2].Max() - positions[0].Min());

            Rectangle centerRect = new(0 + (Width - rectangle.Width) / 2, 0 + (Height - rectangle.Height) / 2, rectangle.Width, rectangle.Height);

            foreach (Control control in Controls)
            {
                if (control is not TransparentPictureBox && control is not ContextMenuR)
                {
                    control.Top = centerRect.Top + (control.Top - rectangle.Top);
                    control.Left = centerRect.Left + (control.Left - rectangle.Left);
                }
            }
        }

        public void LoadFromWinThemeString(string themeDatabase, string themeName)
        {
            // *********************************************************************
            // LINQ is not used as it caused multiple GCs, and worsened performance.
            // *********************************************************************

            if (string.IsNullOrEmpty(themeDatabase) || string.IsNullOrEmpty(themeName)) return;

            string matchedThemeLine = null;

            int totalLength = themeDatabase.Length;
            int lineStartIndex = 0;

            // 1. Find matching theme line
            for (int i = 0; i <= totalLength; i++)
            {
                bool isEndOfLine = (i == totalLength || themeDatabase[i] == '\n');

                if (!isEndOfLine) continue;

                int lineLength = i - lineStartIndex;

                if (lineLength > 0)
                {
                    // Extract one full line
                    string line = themeDatabase.Substring(lineStartIndex, lineLength);

                    int separatorIndex = line.IndexOf('|');

                    if (separatorIndex > 0)
                    {
                        // Match theme name (case-insensitive)
                        bool isTargetTheme = line.StartsWith(themeName, StringComparison.OrdinalIgnoreCase) && line.Length > themeName.Length && line[themeName.Length] == '|';

                        if (isTargetTheme)
                        {
                            // Convert format: "key|value|value" -> "key\r\nvalue\r\nvalue"
                            matchedThemeLine = line.Replace("|", "\r\n");
                            break;
                        }
                    }
                }

                lineStartIndex = i + 1;
            }

            if (matchedThemeLine == null) return;

            Visible = false;

            bool hasGradientActive = false;
            bool hasGradientInactive = false;

            totalLength = matchedThemeLine.Length;
            lineStartIndex = 0;

            // 2. Parse theme key/value lines
            for (int i = 0; i <= totalLength; i++)
            {
                bool isEndOfLine = (i == totalLength || matchedThemeLine[i] == '\n');

                if (!isEndOfLine) continue;

                int lineLength = i - lineStartIndex;

                if (lineLength > 0)
                {
                    ParseThemeItemSpan(matchedThemeLine, lineStartIndex, lineLength, ref hasGradientActive, ref hasGradientInactive);
                }

                lineStartIndex = i + 1;
            }

            Refresh();
            Visible = true;
            RetroShadow1.Refresh();
        }

        private void ParseThemeItemSpan(string themeData, int lineStart, int lineLength, ref bool hasGradientActive, ref bool hasGradientInactive)
        {
            // *********************************************************************
            // LINQ is not used as it caused multiple GCs, and worsened performance.
            // *********************************************************************

            int lineEnd = lineStart + lineLength;
            int equalsIndex = -1;

            // 1. Find '=' separator (key = value)
            for (int i = lineStart; i < lineEnd; i++)
            {
                if (themeData[i] == '=')
                {
                    equalsIndex = i;
                    break;
                }
            }

            if (equalsIndex <= lineStart || equalsIndex >= lineEnd - 1) return;

            // 2. Extract key and value (no allocations)
            ReadOnlySpan<char> keySpan = themeData.AsSpan(lineStart, equalsIndex - lineStart);
            ReadOnlySpan<char> valueSpan = themeData.AsSpan(equalsIndex + 1, lineEnd - equalsIndex - 1);

            string value = valueSpan.ToString(); // conversion only where needed

            // 3. Map keys to theme properties
            if (keySpan.Equals("activetitle", StringComparison.OrdinalIgnoreCase))
            {
                ActiveTitle = value.ToColorFromWin32();

                if (!hasGradientActive) GradientActiveTitle = ActiveTitle;

                return;
            }

            if (keySpan.Equals("gradientactivetitle", StringComparison.OrdinalIgnoreCase))
            {
                GradientActiveTitle = value.ToColorFromWin32();
                hasGradientActive = true;
                return;
            }

            if (keySpan.Equals("inactivetitle", StringComparison.OrdinalIgnoreCase))
            {
                InactiveTitle = value.ToColorFromWin32();

                if (!hasGradientInactive) GradientInactiveTitle = InactiveTitle;

                return;
            }

            if (keySpan.Equals("gradientinactivetitle", StringComparison.OrdinalIgnoreCase))
            {
                GradientInactiveTitle = value.ToColorFromWin32();
                hasGradientInactive = true;
                return;
            }

            // Standard Windows theme keys
            if (keySpan.Equals("background", StringComparison.OrdinalIgnoreCase)) { Background = value.ToColorFromWin32(); return; }
            if (keySpan.Equals("hilight", StringComparison.OrdinalIgnoreCase)) { Hilight = value.ToColorFromWin32(); return; }
            if (keySpan.Equals("hilighttext", StringComparison.OrdinalIgnoreCase)) { HilightText = value.ToColorFromWin32(); return; }
            if (keySpan.Equals("titletext", StringComparison.OrdinalIgnoreCase)) { TitleText = value.ToColorFromWin32(); return; }
            if (keySpan.Equals("window", StringComparison.OrdinalIgnoreCase)) { Window = value.ToColorFromWin32(); return; }
            if (keySpan.Equals("windowtext", StringComparison.OrdinalIgnoreCase)) { WindowText = value.ToColorFromWin32(); return; }
            if (keySpan.Equals("scrollbar", StringComparison.OrdinalIgnoreCase)) { Scrollbar = value.ToColorFromWin32(); return; }
            if (keySpan.Equals("menu", StringComparison.OrdinalIgnoreCase)) { Menu = value.ToColorFromWin32(); return; }
            if (keySpan.Equals("windowframe", StringComparison.OrdinalIgnoreCase)) { WindowFrame = value.ToColorFromWin32(); return; }
            if (keySpan.Equals("menutext", StringComparison.OrdinalIgnoreCase)) { MenuText = value.ToColorFromWin32(); return; }
            if (keySpan.Equals("activeborder", StringComparison.OrdinalIgnoreCase)) { ActiveBorder = value.ToColorFromWin32(); return; }
            if (keySpan.Equals("inactiveborder", StringComparison.OrdinalIgnoreCase)) { InactiveBorder = value.ToColorFromWin32(); return; }
            if (keySpan.Equals("appworkspace", StringComparison.OrdinalIgnoreCase)) { AppWorkspace = value.ToColorFromWin32(); return; }
            if (keySpan.Equals("buttonface", StringComparison.OrdinalIgnoreCase)) { ButtonFace = value.ToColorFromWin32(); return; }
            if (keySpan.Equals("buttonshadow", StringComparison.OrdinalIgnoreCase)) { ButtonShadow = value.ToColorFromWin32(); return; }
            if (keySpan.Equals("graytext", StringComparison.OrdinalIgnoreCase)) { GrayText = value.ToColorFromWin32(); return; }
            if (keySpan.Equals("buttontext", StringComparison.OrdinalIgnoreCase)) { ButtonText = value.ToColorFromWin32(); return; }
            if (keySpan.Equals("inactivetitletext", StringComparison.OrdinalIgnoreCase)) { InactiveTitleText = value.ToColorFromWin32(); return; }
            if (keySpan.Equals("buttonhilight", StringComparison.OrdinalIgnoreCase)) { ButtonHilight = value.ToColorFromWin32(); return; }
            if (keySpan.Equals("buttondkshadow", StringComparison.OrdinalIgnoreCase)) { ButtonDkShadow = value.ToColorFromWin32(); return; }
            if (keySpan.Equals("buttonlight", StringComparison.OrdinalIgnoreCase)) { ButtonLight = value.ToColorFromWin32(); return; }
            if (keySpan.Equals("infotext", StringComparison.OrdinalIgnoreCase)) { InfoText = value.ToColorFromWin32(); return; }
            if (keySpan.Equals("infowindow", StringComparison.OrdinalIgnoreCase)) { InfoWindow = value.ToColorFromWin32(); return; }
            if (keySpan.Equals("hottrackingcolor", StringComparison.OrdinalIgnoreCase)) { HotTrackingColor = value.ToColorFromWin32(); return; }
            if (keySpan.Equals("buttonalternateface", StringComparison.OrdinalIgnoreCase)) { ButtonAlternateFace = value.ToColorFromWin32(); return; }
            if (keySpan.Equals("menubar", StringComparison.OrdinalIgnoreCase)) { MenuBar = value.ToColorFromWin32(); return; }
            if (keySpan.Equals("menuhilight", StringComparison.OrdinalIgnoreCase)) { MenuHilight = value.ToColorFromWin32(); return; }
            if (keySpan.Equals("desktop", StringComparison.OrdinalIgnoreCase)) { Desktop = value.ToColorFromWin32(); return; }
        }

        public void LoadColors(Manager TM)
        {
            if (TM == null) return;

            Visible = false;

            Theme.Structures.Win32UI win32ui = TM.Win32;

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

        public void LoadColors(Theme.Structures.Win32UI win32ui)
        {
            if (win32ui == null) return;

            Visible = false;

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

        private void WindowR4_SizeChanged(object sender, EventArgs e) => UpdateToolTipPosition();
        private void WindowR4_LocationChanged(object sender, EventArgs e) => UpdateToolTipPosition();

        private void UpdateToolTipPosition()
        {
            toolTipR1.Top = WindowR4.Top + WindowR4.Metrics_CaptionHeight + 2;
            toolTipR1.Left = WindowR4.Right - WindowR4.Metrics_CaptionWidth - 2;
        }

        private void WindowR2_SizeChanged(object sender, EventArgs e) => UpdateContextMenuPosition();
        private void WindowR2_LocationChanged(object sender, EventArgs e) => UpdateContextMenuPosition();

        private void UpdateContextMenuPosition()
        {
            ContextMenu.Top = WindowR2.Top + menuBarR1.Top + menuBarR1.Height;
            ContextMenu.Left = Math.Min(WindowR2.Left + menuBarR1.Left + menuBarR1.SelectedItemLocation.X, WindowR2.Right - WindowR2.Metrics_PaddedBorderWidth - WindowR2.Metrics_BorderWidth);
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
            if (!DesignMode) RetroShadow1.Location = ContextMenu.Location + (Size)new Point(5, 5);
        }

        #endregion

        #region Overrides

        private bool _cursorMovingInBackground;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (EnableEditingColors)
            {
                _cursorMovingInBackground = true;
                Invalidate();
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (EnableEditingColors)
            {
                _cursorMovingInBackground = false;
                Invalidate();
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