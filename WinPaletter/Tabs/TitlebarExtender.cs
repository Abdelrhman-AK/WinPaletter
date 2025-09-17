using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

namespace WinPaletter.Tabs
{
    /// <summary>
    /// A container control that extends the titlebar of a form using either a DWM effect or a continuation of the tab on <see cref="TabsContainer"/>
    /// </summary>
    public partial class TitlebarExtender : ContainerControl
    {
        private bool _formFocused = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="TitlebarExtender"/> class.
        /// </summary>
        public TitlebarExtender()
        {
            BackColor = Color.Black;
            DoubleBuffered = true;
            UpdateColors();
            UpdateBackDrop();
        }

        Color activeTtl, inactiveTtl, activeTtlG, inactiveTtlG;

        public static bool Transparency
        {
            get => _transparency;
            set
            {
                if (_transparency != value)
                {
                    _transparency = value;
                }
            }
        }
        private static bool _transparency = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", 1)) == 1;

        public static bool AccentOnTitlebars
        {
            get => accentOnTitlebars;
            set
            {
                if (accentOnTitlebars != value)
                {
                    accentOnTitlebars = value;
                }
            }
        }
        private static bool accentOnTitlebars = Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", 1)) == 1;

        Config.Scheme scheme => Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;

        /// <summary>
        /// Gets or sets the flag that determines the style of the titlebar extender.
        /// </summary>
        public Flags Flag
        {
            get => flag;
            set
            {
                if (flag != value)
                {
                    flag = value;
                    if (!DesignMode) UpdateBackDrop();
                    Invalidate();
                }
            }
        }
        private Flags flag = Flags.System;

        /// <summary>
        /// Flags enumeration that determines the style of the titlebar extender.
        /// </summary>
        public enum Flags
        {
            /// <summary>
            /// The titlebar extender will extend the tabs on the <see cref="TabsContainer"/>.
            /// </summary>
            Tabs_Extended = 0,

            /// <summary>
            /// The titlebar extender will extend the titlebar of the form using the default style according to the OS settings.
            /// </summary>
            System = 1,
        }

        private bool isCompositionEnabled = DWMAPI.IsCompositionEnabled();

        /// <summary>
        /// Gets the type of the titlebar according to the OS settings.
        /// </summary>
        public TitlebarTypes TitlebarType
        {
            get
            {
                if (Program.ClassicThemeRunning) return TitlebarTypes.Classic;

                if (!isCompositionEnabled) return TitlebarTypes.Basic;

                if (OS.WVista || OS.W7 || OS.W8x) return TitlebarTypes.DWM;

                // Windows 10/11
                if (AccentOnTitlebars && isCompositionEnabled) return TitlebarTypes.DWM_Aero;

                if (AccentOnTitlebars && !isCompositionEnabled) return TitlebarTypes.ColorPrevalence;

                if (Transparency) return OS.W10 ? TitlebarTypes.DWM_Aero : TitlebarTypes.DWM;

                return TitlebarTypes.AppMode;
            }
        }

        /// <summary>
        /// Enumeration that determines the style of the titlebar if the <see cref="Flag"/> is set to <see cref="Flags.System"/>.
        /// </summary>
        public enum TitlebarTypes
        {
            /// <summary>
            /// The titlebar extender will extend the titlebar of the form using the DWM effect.
            /// </summary>
            DWM = 0,

            /// <summary>
            /// The titlebar extender will extend the titlebar of the form using the DWM effect but forces Aero style.
            /// </summary>
            DWM_Aero = 1,

            /// <summary>
            /// The titlebar extender will extend the titlebar of the form using a color.
            /// </summary>
            ColorPrevalence = 2,

            /// <summary>
            /// The titlebar extender will extend the titlebar of the form using the dark mode.
            /// </summary>
            AppMode = 3,

            /// <summary>
            /// The titlebar extender will extend the titlebar of the form using the basic style.
            /// </summary>
            Basic = 4,

            /// <summary>
            /// The titlebar extender will extend the titlebar of the form using the classic style.
            /// </summary>
            Classic = 5,
        }

        private Rectangle _tabLocation = Rectangle.Empty;
        /// <summary>
        /// Gets or sets the location of the tab on the <see cref="TabsContainer"/> that this <see cref="TitlebarExtender"/> is extending.
        /// </summary>
        public Rectangle TabLocation
        {
            get => _tabLocation;
            set
            {
                if (_tabLocation != value)
                {
                    _tabLocation = value;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="Control.HandleCreated"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleCreated(EventArgs e)
        {
            if (!DesignMode)
            {
                if (FindForm() != null)
                {
                    FindForm().Activated += Form_Activated;
                    FindForm().Deactivate += Form_Deactivate;
                    SystemEvents.UserPreferenceChanged += (s, e) => { OnSystemColorsUpdated(s, e); };
                }

                UpdateBackDrop();
            }

            base.OnHandleCreated(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.HandleDestroyed"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (FindForm() != null)
            {
                FindForm().Activated -= Form_Activated;
                FindForm().Deactivate -= Form_Deactivate;
                SystemEvents.UserPreferenceChanged -= (s, e) => { OnSystemColorsUpdated(s, e); };
            }

            base.OnHandleDestroyed(e);
        }

        private void OnSystemColorsUpdated(object sender, UserPreferenceChangedEventArgs e)
        {
            isCompositionEnabled = DWMAPI.IsCompositionEnabled();

            if (e.Category == UserPreferenceCategory.Color || e.Category == UserPreferenceCategory.VisualStyle)
            {
                UpdateColors();
                if (!DesignMode) UpdateBackDrop();
            }
        }

        private void UpdateColors()
        {
            if (TitlebarType == TitlebarTypes.ColorPrevalence)
            {
                activeTtl = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", Color.Black.Reverse().ToArgb()))).Reverse();
                inactiveTtl = Color.FromArgb(Convert.ToInt32(GetReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", Color.Black.Reverse().ToArgb()))).Reverse();
                activeTtlG = activeTtl;
                inactiveTtlG = inactiveTtl;
            }
            else
            {
                activeTtl = TitlebarType == TitlebarTypes.Basic ? Color.FromArgb(185, 209, 234) : SystemColors.ActiveCaption;
                inactiveTtl = TitlebarType == TitlebarTypes.Basic ? Color.FromArgb(215, 228, 242) : SystemColors.InactiveCaption;
                activeTtlG = TitlebarType == TitlebarTypes.Basic ? activeTtl : SystemColors.GradientActiveCaption;
                inactiveTtlG = TitlebarType == TitlebarTypes.Basic ? inactiveTtl : SystemColors.GradientInactiveCaption;
            }
        }

        /// <summary>
        /// Raises the <see cref="Control.DockChanged"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDockChanged(EventArgs e)
        {
            if (!DesignMode) UpdateBackDrop();

            base.OnDockChanged(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.ControlAdded"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (e.Control is Panel || e.Control is FlowLayoutPanel)
            {
                e.Control.MouseDown += (s, e) => { OnMouseDown(e); };
                e.Control.MouseMove += (s, e) => { OnMouseMove(e); };
            }
        }

        /// <summary>
        /// Raises the <see cref="Control.ControlRemoved"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);

            if (e.Control is Panel || e.Control is FlowLayoutPanel)
            {
                e.Control.MouseDown -= (s, e) => { OnMouseDown(e); };
                e.Control.MouseMove -= (s, e) => { OnMouseMove(e); };
            }
        }

        private void Form_Activated(object sender, EventArgs e)
        {
            _formFocused = true;
            UpdateBackDrop();
        }

        private void Form_Deactivate(object sender, EventArgs e)
        {
            _formFocused = false;
            UpdateBackDrop();
        }

        private Point newPoint = new();
        private Point oldPoint = new();

        /// <summary>
        /// Raises the <see cref="Control.MouseDown"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            oldPoint = MousePosition - (Size)FindForm()?.Location;

            base.OnMouseDown(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.MouseMove"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (Flag == Flags.System && FindForm() != null && e.Button == MouseButtons.Left)
            {
                newPoint = MousePosition - (Size)oldPoint;
                FindForm().Location = newPoint;
            }

            base.OnMouseMove(e);
        }

        /// <summary>
        /// Updates the backdrop of the control.
        /// </summary>
        public void UpdateBackDrop()
        {
            if (Flag == Flags.Tabs_Extended)
            {
                BackColor = scheme.Colors.Back_Hover(0);
            }
            else
            {
                Padding p = Padding.Empty;

                if (Dock == DockStyle.Top)
                {
                    p = new(0, Height, 0, 0);
                }
                else if (Dock == DockStyle.Bottom)
                {
                    p = new(0, 0, 0, Height);
                }
                else if (Dock == DockStyle.Left)
                {
                    p = new(Width, 0, 0, 0);
                }
                else if (Dock == DockStyle.Right)
                {
                    p = new(0, 0, Width, 0);
                }
                else if (Dock == DockStyle.Fill)
                {
                    p = new(0);
                }

                Form form = FindForm();
                if (form is not null && form.Parent is not null) form = form.Parent.FindForm();

                form?.ResetEffect();

                if (Flag == Flags.System)
                {
                    TitlebarTypes type = TitlebarType;

                    if (type == TitlebarTypes.DWM)
                    {
                        BackColor = Color.Black;
                        if (p != Padding.Empty) form?.DropEffect(p);
                    }

                    else if (type == TitlebarTypes.DWM_Aero)
                    {
                        BackColor = Color.Black;
                        if (p != Padding.Empty) form?.DropEffect(p, false, DWM.FormStyle.Aero);
                    }

                    else if (type == TitlebarTypes.ColorPrevalence)
                    {
                        BackColor = _formFocused ? activeTtl : inactiveTtl;
                    }

                    else if (type == TitlebarTypes.AppMode)
                    {
                        BackColor = Program.Style.DarkMode ? Color.FromArgb(32, 32, 32) : OS.W10 ? Color.White : Color.FromArgb(243, 243, 243);
                    }

                    else if (type == TitlebarTypes.Basic)
                    {
                        BackColor = Color.Black;
                    }

                    else if (type == TitlebarTypes.Classic)
                    {
                        BackColor = Color.Black;
                    }
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="Control.Paint"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics G = e.Graphics;

            // Inferior border
            using (Pen P = new(scheme.Colors.Line_Hover(0))) { G.DrawLine(P, new Point(0, Height - 1), new Point(Width - 1, Height - 1)); }

            if (Flag == Flags.System && (TitlebarType == TitlebarTypes.Classic || TitlebarType == TitlebarTypes.Basic))
            {
                Rectangle rect = new(0, 0, Width, Height);
                using (LinearGradientBrush brush = new(rect, _formFocused ? activeTtl : inactiveTtl, _formFocused ? activeTtlG : inactiveTtlG, LinearGradientMode.Horizontal))
                {
                    G.FillRectangle(brush, rect);
                }
            }
        }
    }
}