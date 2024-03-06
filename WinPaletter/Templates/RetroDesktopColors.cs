using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using WinPaletter.UI.Retro;
using WinPaletter.UI.WP;
using static WinPaletter.PreviewHelpers;

namespace WinPaletter.Templates
{
    public partial class RetroDesktopColors : UserControl
    {
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
                        foreach (WindowR window in this.GetAllControls().OfType<WindowR>()) { window.EnableEditingColors = value; }
                        foreach (ContextMenuR contextMenu in this.GetAllControls().OfType<ContextMenuR>()) { contextMenu.EnableEditingColors = value; }
                        foreach (ButtonR button in this.GetAllControls().OfType<ButtonR>()) { button.EnableEditingColors = value; }
                        foreach (MenuBarR menuBar in this.GetAllControls().OfType<MenuBarR>()) { menuBar.EnableEditingColors = value; }
                        foreach (LabelR label in this.GetAllControls().OfType<LabelR>()) { label.EnableEditingColors = value; }
                        foreach (ScrollBarR scrollBar in this.GetAllControls().OfType<ScrollBarR>()) { scrollBar.EnableEditingColors = value; }
                        foreach (AppWorkspaceR appWorkspace in this.GetAllControls().OfType<AppWorkspaceR>()) { appWorkspace.EnableEditingColors = value; }
                        foreach (WindowControlR windowControl in this.GetAllControls().OfType<WindowControlR>()) { windowControl.EnableEditingColors = value; }
                        foreach (ToolTipR toolTip in this.GetAllControls().OfType<ToolTipR>()) { toolTip.EnableEditingColors = value; }
                    }
                }
            }
        }
        private bool enableColorsEditing = false;


        /// <summary>
        /// If disabled, classic 3D effects will be made to menus and menu items selection
        /// </summary>
        public bool EnableTheming
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
        /// Application workspace (like MDI container) BackColor
        /// </summary>
        public Color AppWorkspace
        {
            get { return appWorkspace; }
            set
            {
                if (appWorkspace != value)
                {
                    appWorkspace = value;
                    foreach (AppWorkspaceR appWorkspace in this.GetAllControls().OfType<AppWorkspaceR>()) { appWorkspace.BackColor = value; }
                }
            }
        }
        private Color appWorkspace;


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

                    RetroShadow1.Refresh();
                }
            }
        }
        private Color buttonFace;


        /// <summary>
        /// Obsolete
        /// </summary>
        public Color ButtonAlternateFace { get; set; }


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
        /// Highlighted text ForeColor
        /// </summary>
        public Color HilightText
        {
            get { return hilightText; }
            set
            {
                if (hilightText != value)
                {
                    hilightText = value;
                    foreach (ContextMenuR contextMenu in this.GetAllControls().OfType<ContextMenuR>()) { contextMenu.HilightText = value; }
                    foreach (MenuBarR menuBar in this.GetAllControls().OfType<MenuBarR>()) { menuBar.HilightText = value; }
                }
            }
        }
        private Color hilightText;


        /// <summary>
        /// Color of selection rectangles and hyperlinks
        /// </summary>
        public Color HotTrackingColor { get; set; }


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
                }
            }
        }
        private Color inactiveTitleText;


        /// <summary>
        /// ToolTip text ForeColor
        /// </summary>
        public Color InfoText
        {
            get { return infoText; }
            set
            {
                if (infoText != value)
                {
                    infoText = value;
                    foreach (ToolTipR toolTip in this.GetAllControls().OfType<ToolTipR>()) { toolTip.ForeColor = value; }
                }
            }
        }
        private Color infoText;


        /// <summary>
        /// ToolTip BackColor
        /// </summary>
        public Color InfoWindow
        {
            get { return infoWindow; }
            set
            {
                if (infoWindow != value)
                {
                    infoWindow = value;
                    foreach (ToolTipR toolTip in this.GetAllControls().OfType<ToolTipR>()) { toolTip.BackColor = value; }
                }
            }
        }
        private Color infoWindow;


        /// <summary>
        /// Color of cascaded menu
        /// </summary>
        public Color Menu
        {
            get { return menu; }
            set
            {
                if (menu != value)
                {
                    menu = value;

                    foreach (ContextMenuR contextMenu in this.GetAllControls().OfType<ContextMenuR>()) { contextMenu.BackColor = value; }

                    if (!enableTheming)
                    {
                        foreach (MenuBarR menuBar in this.GetAllControls().OfType<MenuBarR>()) { menuBar.BackColor = value; }
                        foreach (PanelR panel in this.GetAllControls().OfType<PanelR>()) { panel.BackColor = value; }
                    }
                }
            }
        }
        private Color menu;


        /// <summary>
        /// Color of menu bar
        /// </summary>
        public Color MenuBar
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
        public Color MenuText
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
        /// Obsolete: Was used in Windows 9x
        /// </summary>
        public Color Scrollbar { get; set; }


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


        /// <summary>
        /// Color of highlight
        /// </summary>
        public Color Hilight
        {
            get { return hilight; }
            set
            {
                if (hilight != value)
                {
                    hilight = value;

                    foreach (ContextMenuR contextMenu in this.GetAllControls().OfType<ContextMenuR>()) { contextMenu.Hilight = value; }
                    foreach (MenuBarR menuBar in this.GetAllControls().OfType<MenuBarR>()) { menuBar.Hilight = value; }

                    if (enableTheming)
                    {
                        foreach (PanelRaisedR panelRaised in this.GetAllControls().OfType<PanelRaisedR>()) { panelRaised.ButtonShadow = value; }
                    }
                }
            }
        }
        private Color hilight;


        /// <summary>
        /// Color of menu highlight
        /// </summary>
        public Color MenuHilight
        {
            get { return menuHilight; }
            set
            {
                if (menuHilight != value)
                {
                    menuHilight = value;

                    foreach (ContextMenuR contextMenu in this.GetAllControls().OfType<ContextMenuR>()) { contextMenu.MenuHilight = value; }
                    foreach (MenuBarR menuBar in this.GetAllControls().OfType<MenuBarR>()) { menuBar.MenuHilight = value; }

                    if (enableTheming)
                    {
                        foreach (PanelRaisedR panelRaised in this.GetAllControls().OfType<PanelRaisedR>()) { panelRaised.BackColor = value; }
                        foreach (PanelR panel in this.GetAllControls().OfType<PanelR>()) { panel.BackColor = value; }
                    }
                }
            }
        }
        private Color menuHilight;


        /// <summary>
        /// Obsolete: Was used in Windows 9x
        /// </summary>
        public Color Desktop { get; set; }

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
                if (e.Control is WindowR window)
                {
                    window.EditorInvoker += EditorInvoked;
                    foreach (ButtonR button in window.GetAllControls().OfType<ButtonR>()) button.EditorInvoker += EditorInvoked;
                    foreach (MenuBarR menuBar in window.GetAllControls().OfType<MenuBarR>()) menuBar.EditorInvoker += EditorInvoked;
                    foreach (ContextMenuR menu in window.GetAllControls().OfType<ContextMenuR>()) menu.EditorInvoker += EditorInvoked;
                    foreach (LabelR label in window.GetAllControls().OfType<LabelR>()) label.EditorInvoker += EditorInvoked;
                    foreach (ScrollBarR scrollBar in window.GetAllControls().OfType<ScrollBarR>()) scrollBar.EditorInvoker += EditorInvoked;
                    foreach (AppWorkspaceR appWorkspace in window.GetAllControls().OfType<AppWorkspaceR>()) appWorkspace.EditorInvoker += EditorInvoked;
                    foreach (WindowControlR windowControl in window.GetAllControls().OfType<WindowControlR>()) windowControl.EditorInvoker += EditorInvoked;
                    foreach (ToolTipR toolTip in window.GetAllControls().OfType<ToolTipR>()) toolTip.EditorInvoker += EditorInvoked;
                }

                else if (e.Control is ContextMenuR menu) menu.EditorInvoker += EditorInvoked;
                else if (e.Control is ButtonR button) button.EditorInvoker += EditorInvoked;
                else if (e.Control is MenuBarR menuBar) menuBar.EditorInvoker += EditorInvoked;
                else if (e.Control is LabelR label) label.EditorInvoker += EditorInvoked;
                else if (e.Control is ScrollBarR scrollBar) scrollBar.EditorInvoker += EditorInvoked;
                else if (e.Control is AppWorkspaceR appWorkspace) appWorkspace.EditorInvoker += EditorInvoked;
                else if (e.Control is WindowControlR windowControl) windowControl.EditorInvoker += EditorInvoked;
                else if (e.Control is ToolTipR toolTip) toolTip.EditorInvoker += EditorInvoked;
            }

            base.OnControlAdded(e);
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            if (!DesignMode)
            {
                if (e.Control is WindowR window)
                {
                    window.EditorInvoker -= EditorInvoked;
                    foreach (ButtonR button in window.GetAllControls().OfType<ButtonR>()) button.EditorInvoker -= EditorInvoked;
                    foreach (MenuBarR menuBar in window.GetAllControls().OfType<MenuBarR>()) menuBar.EditorInvoker -= EditorInvoked;
                    foreach (ContextMenuR menu in window.GetAllControls().OfType<ContextMenuR>()) menu.EditorInvoker -= EditorInvoked;
                    foreach (LabelR label in window.GetAllControls().OfType<LabelR>()) label.EditorInvoker -= EditorInvoked;
                    foreach (ScrollBarR scrollBar in window.GetAllControls().OfType<ScrollBarR>()) scrollBar.EditorInvoker -= EditorInvoked;
                    foreach (AppWorkspaceR appWorkspace in window.GetAllControls().OfType<AppWorkspaceR>()) appWorkspace.EditorInvoker -= EditorInvoked;
                    foreach (WindowControlR windowControl in window.GetAllControls().OfType<WindowControlR>()) windowControl.EditorInvoker -= EditorInvoked;
                    foreach (ToolTipR toolTip in window.GetAllControls().OfType<ToolTipR>()) toolTip.EditorInvoker -= EditorInvoked;
                }

                else if (e.Control is ContextMenuR menu) menu.EditorInvoker -= EditorInvoked;
                else if (e.Control is ButtonR button) button.EditorInvoker -= EditorInvoked;
                else if (e.Control is MenuBarR menuBar) menuBar.EditorInvoker -= EditorInvoked;
                else if (e.Control is LabelR label) label.EditorInvoker -= EditorInvoked;
                else if (e.Control is ScrollBarR scrollBar) scrollBar.EditorInvoker -= EditorInvoked;
                else if (e.Control is AppWorkspaceR appWorkspace) appWorkspace.EditorInvoker -= EditorInvoked;
                else if (e.Control is WindowControlR windowControl) windowControl.EditorInvoker -= EditorInvoked;
                else if (e.Control is ToolTipR toolTip) toolTip.EditorInvoker -= EditorInvoked;
            }

            base.OnControlRemoved(e);
        }

        #endregion

        #region Methods

        private void RetroDesktop_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                this.DoubleBuffer();

                foreach (Control c in this.GetAllControls()) { c.DoubleBuffer(); }
            }
        }

        private Bitmap GetShadow()
        {
            using (Bitmap b = new(RetroShadow1.Width, RetroShadow1.Height))
            using (Graphics G = Graphics.FromImage(b))
            {
                G.Clear(Color.Transparent);
                G.DrawGlow(new(5, 5, b.Width - 10 - 1, b.Height - 10 - 1), Color.FromArgb(100, 0, 0, 0));
                G.Save();
                G.Dispose();
                return new Bitmap(b);
            }
        }

        public void LoadMetrics(Theme.Manager TM)
        {
            if (TM is null) return;

            RetroShadow1.Visible = TM.WindowsEffects.WindowShadow;

            int iP = 3 + TM.MetricsFonts.PaddedBorderWidth + TM.MetricsFonts.BorderWidth;
            int iT = 4 + TM.MetricsFonts.PaddedBorderWidth + TM.MetricsFonts.BorderWidth + TM.MetricsFonts.CaptionHeight + GetTitlebarTextHeight(TM.MetricsFonts.CaptionFont);
            Padding _Padding = new(iP, iT, iP, iP);

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

            WindowR3.Height = 90 + TM.MetricsFonts.PaddedBorderWidth + TM.MetricsFonts.BorderWidth + GetTitlebarTextHeight(WindowR3.Font);
            WindowR2.Height = 120 + TM.MetricsFonts.PaddedBorderWidth + TM.MetricsFonts.BorderWidth + GetTitlebarTextHeight(WindowR2.Font) + TM.MetricsFonts.MenuHeight;

            WindowR3.Top = WindowR2.Top + windowControlR1.Top + windowControlR1.Font.Height + 10;
            WindowR3.Left = WindowR2.Left + windowControlR1.Left + 15;

            List<int> Tops = new();
            List<int> Lefts = new();
            List<int> Bottoms = new();
            List<int> Rights = new();

            foreach (Control control in Controls)
            {
                if (control is not TransparentPictureBox && control is not ContextMenuR)
                {
                    Tops.Add(control.Top);
                    Lefts.Add(control.Left);
                    Bottoms.Add(control.Bottom);
                    Rights.Add(control.Right);
                }
            }

            Rectangle rectangle = new(Lefts.Min(), Tops.Min(), Rights.Max() - Lefts.Min(), Bottoms.Max() - Tops.Min());
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

        public void LoadFromWinThemeString(string DB, string ThemeName)
        {
            if (string.IsNullOrWhiteSpace(DB) || !DB.Contains("|") || string.IsNullOrWhiteSpace(ThemeName)) return;

            string SelectedTheme = string.Empty;

            bool Found = false;

            foreach (string theme in DB.Split('\n'))
            {
                if ((theme.Split('|')[0].ToLower() ?? string.Empty) == (ThemeName.ToLower() ?? string.Empty))
                {
                    SelectedTheme = theme.Replace("|", "\r\n");
                    Found = true;
                    break;
                }
            }

            if (!Found) return;

            bool FoundGradientActive = false;
            bool FoundGradientInactive = false;

            Visible = false;

            foreach (string item in SelectedTheme.Split('\n'))
            {
                string x = item.ToLower();

                if (x.StartsWith("activetitle=", StringComparison.OrdinalIgnoreCase))
                {
                    ActiveTitle = x.Split('=')[1].FromWin32RegToColor();
                    if (!FoundGradientActive)
                        GradientActiveTitle = ActiveTitle;
                }

                if (x.StartsWith("gradientactivetitle=", StringComparison.OrdinalIgnoreCase))
                {
                    GradientActiveTitle = x.Split('=')[1].FromWin32RegToColor();
                    FoundGradientActive = true;
                }

                if (x.StartsWith("inactivetitle=", StringComparison.OrdinalIgnoreCase))
                {
                    InactiveTitle = x.Split('=')[1].FromWin32RegToColor();
                    if (!FoundGradientInactive)
                        GradientInactiveTitle = InactiveTitle;

                }

                if (x.StartsWith("gradientinactivetitle=", StringComparison.OrdinalIgnoreCase))
                {
                    GradientInactiveTitle = x.Split('=')[1].FromWin32RegToColor();
                    FoundGradientInactive = true;
                }

                if (x.StartsWith("background=", StringComparison.OrdinalIgnoreCase))
                    Background = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("hilight=", StringComparison.OrdinalIgnoreCase))
                    Hilight = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("hilighttext=", StringComparison.OrdinalIgnoreCase))
                    HilightText = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("titletext=", StringComparison.OrdinalIgnoreCase))
                    TitleText = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("window=", StringComparison.OrdinalIgnoreCase))
                    Window = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("windowtext=", StringComparison.OrdinalIgnoreCase))
                    WindowText = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("scrollbar=", StringComparison.OrdinalIgnoreCase))
                    Scrollbar = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("menu=", StringComparison.OrdinalIgnoreCase))
                    Menu = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("windowframe=", StringComparison.OrdinalIgnoreCase))
                    WindowFrame = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("menutext=", StringComparison.OrdinalIgnoreCase))
                    MenuText = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("activeborder=", StringComparison.OrdinalIgnoreCase))
                    ActiveBorder = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("inactiveborder=", StringComparison.OrdinalIgnoreCase))
                    InactiveBorder = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("appworkspace=", StringComparison.OrdinalIgnoreCase))
                    AppWorkspace = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("buttonface=", StringComparison.OrdinalIgnoreCase))
                    ButtonFace = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("buttonshadow=", StringComparison.OrdinalIgnoreCase))
                    ButtonShadow = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("graytext=", StringComparison.OrdinalIgnoreCase))
                    GrayText = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("buttontext=", StringComparison.OrdinalIgnoreCase))
                    ButtonText = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("inactivetitletext=", StringComparison.OrdinalIgnoreCase))
                    InactiveTitleText = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("buttonhilight=", StringComparison.OrdinalIgnoreCase))
                    ButtonHilight = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("buttondkshadow=", StringComparison.OrdinalIgnoreCase))
                    ButtonDkShadow = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("buttonlight=", StringComparison.OrdinalIgnoreCase))
                    ButtonLight = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("infotext=", StringComparison.OrdinalIgnoreCase))
                    InfoText = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("infowindow=", StringComparison.OrdinalIgnoreCase))
                    InfoWindow = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("hottrackingcolor=", StringComparison.OrdinalIgnoreCase))
                    HotTrackingColor = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("buttonalternateface=", StringComparison.OrdinalIgnoreCase))
                    ButtonAlternateFace = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("menubar=", StringComparison.OrdinalIgnoreCase))
                    MenuBar = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("menuhilight=", StringComparison.OrdinalIgnoreCase))
                    MenuHilight = x.Split('=')[1].FromWin32RegToColor();

                if (x.StartsWith("desktop=", StringComparison.OrdinalIgnoreCase))
                    Desktop = x.Split('=')[1].FromWin32RegToColor();
            }

            Refresh();
            Visible = true;

            RetroShadow1.Refresh();
        }

        public void LoadColors(Theme.Manager TM)
        {
            Visible = false;
            EnableTheming = TM.Win32.EnableTheming;
            EnableGradient = TM.Win32.EnableGradient;
            ActiveBorder = TM.Win32.ActiveBorder;
            ActiveTitle = TM.Win32.ActiveTitle;
            AppWorkspace = TM.Win32.AppWorkspace;
            Background = TM.Win32.Background;
            ButtonAlternateFace = TM.Win32.ButtonAlternateFace;
            ButtonDkShadow = TM.Win32.ButtonDkShadow;
            ButtonFace = TM.Win32.ButtonFace;
            ButtonHilight = TM.Win32.ButtonHilight;
            ButtonLight = TM.Win32.ButtonLight;
            ButtonShadow = TM.Win32.ButtonShadow;
            ButtonText = TM.Win32.ButtonText;
            GradientActiveTitle = TM.Win32.GradientActiveTitle;
            GradientInactiveTitle = TM.Win32.GradientInactiveTitle;
            GrayText = TM.Win32.GrayText;
            HilightText = TM.Win32.HilightText;
            HotTrackingColor = TM.Win32.HotTrackingColor;
            InactiveBorder = TM.Win32.InactiveBorder;
            InactiveTitle = TM.Win32.InactiveTitle;
            InactiveTitleText = TM.Win32.InactiveTitleText;
            InfoText = TM.Win32.InfoText;
            InfoWindow = TM.Win32.InfoWindow;
            Menu = TM.Win32.Menu;
            MenuBar = TM.Win32.MenuBar;
            MenuText = TM.Win32.MenuText;
            MenuHilight = TM.Win32.MenuHilight;
            Scrollbar = TM.Win32.Scrollbar;
            TitleText = TM.Win32.TitleText;
            Window = TM.Win32.Window;
            WindowFrame = TM.Win32.WindowFrame;
            WindowText = TM.Win32.WindowText;
            Hilight = TM.Win32.Hilight;
            Desktop = TM.Win32.Background;
            Visible = true;
        }

        public void LoadColors(Theme.Structures.Win32UI win32ui)
        {
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

        private void WindowR4_SizeChanged(object sender, EventArgs e)
        {
            toolTipR1.Top = WindowR4.Top + WindowR4.Metrics_CaptionHeight + 2;
            toolTipR1.Left = WindowR4.Right - WindowR4.Metrics_CaptionWidth - 2;
        }

        private void WindowR4_LocationChanged(object sender, EventArgs e)
        {
            toolTipR1.Top = WindowR4.Top + WindowR4.Metrics_CaptionHeight + 2;
            toolTipR1.Left = WindowR4.Right - WindowR4.Metrics_CaptionWidth - 2;
        }

        private void WindowR2_SizeChanged(object sender, EventArgs e)
        {
            ContextMenu.Top = WindowR2.Top + menuBarR1.Top + menuBarR1.Height;
            ContextMenu.Left = Math.Min(WindowR2.Left + menuBarR1.Left + menuBarR1.SelectedItemLocation.X, WindowR2.Right - WindowR2.Metrics_PaddedBorderWidth - WindowR2.Metrics_BorderWidth);
        }

        private void WindowR2_LocationChanged(object sender, EventArgs e)
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

        private bool _CursorMovingInBackground;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (EnableEditingColors)
            {
                _CursorMovingInBackground = true;
                Refresh();
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (EnableEditingColors)
            {
                _CursorMovingInBackground = false;
                Refresh();
            }

            base.OnMouseLeave(e);
        }

        protected override void OnClick(EventArgs e)
        {
            if (!DesignMode && EnableEditingColors)
            {
                if (_CursorMovingInBackground) EditorInvoker?.Invoke(this, new EditorEventArgs(nameof(Templates.RetroDesktopColors.Background)));
            }

            base.OnClick(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            #region Editor

            if (EnableEditingColors && _CursorMovingInBackground)
            {
                Color color = Color.FromArgb(80, BackColor.IsDark() ? Color.White : Color.Black);
                using (HatchBrush hb = new(HatchStyle.Percent10, color, Color.Transparent)) { e.Graphics.FillRectangle(hb, e.ClipRectangle); }
            }

            #endregion

            base.OnPaint(e);
        }
        #endregion
    }
}