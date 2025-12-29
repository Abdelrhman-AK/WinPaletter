using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
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
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;

            BackColor = Color.Black;

            Program.WindowsTransparencyChanged += Program_WindowsTransparencyChanged;
        }

        private void Program_WindowsTransparencyChanged(bool obj)
        {
            UpdateBackDrop();
        }

        Color activeTtl, inactiveTtl, activeTtlG, inactiveTtlG;

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
        private static bool accentOnTitlebars = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "ColorPrevalence", 1) == 1;

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

                // Windows 10/11/12
                if (AccentOnTitlebars && isCompositionEnabled) return Program.WindowsTransparency ? TitlebarTypes.DWM_Aero : TitlebarTypes.ColorPrevalence;

                if (AccentOnTitlebars && !isCompositionEnabled) return TitlebarTypes.ColorPrevalence;

                if (Program.WindowsTransparency) return OS.W10 ? TitlebarTypes.DWM_Aero : TitlebarTypes.DWM;

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
            isCompositionEnabled = DWMAPI.IsCompositionEnabled();

            base.OnHandleCreated(e);

            if (!DesignMode)
            {
                Form form = FindForm();
                if (form != null)
                {
                    form.Activated += Form_Activated;
                    form.Deactivate += Form_Deactivate;
                    form.Load += Form_Load;
                    SystemEvents.UserPreferenceChanged += OnSystemSettingsUpdated;
                }

                UpdateBackDrop();
            }
        }

        /// <summary>
        /// Raises the <see cref="Control.HandleDestroyed"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (FindForm() != null)
            {
                FindForm()?.Activated -= Form_Activated;
                FindForm()?.Deactivate -= Form_Deactivate;
                SystemEvents.UserPreferenceChanged -= OnSystemSettingsUpdated;
            }

            base.OnHandleDestroyed(e);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            _firstBackdropUpdate = true;   // force redraw
            UpdateBackDrop();
            ((Form)sender).Load -= Form_Load; // unsubscribe, only need once
        }

        private void OnSystemSettingsUpdated(object sender, UserPreferenceChangedEventArgs e)
        {
            isCompositionEnabled = DWMAPI.IsCompositionEnabled();

            if (e.Category == UserPreferenceCategory.General)
            {
                UpdateColors();
                if (!DesignMode)
                {
                    _lastBackdropType = (TitlebarTypes)(-1);
                    _lastBackdropPadding = Padding.Empty;
                    FindForm()?.ResetEffect();
                    UpdateBackDrop();
                }
            }
        }

        private void UpdateColors()
        {
            if (TitlebarType == TitlebarTypes.ColorPrevalence)
            {
                activeTtl = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColor", Color.Black.Reverse()).Reverse();
                inactiveTtl = ReadReg(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\DWM", "AccentColorInactive", Color.Black.Reverse()).Reverse();
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

        private TitlebarTypes? _lastBackdropType = null;
        private Padding _lastBackdropPadding = Padding.Empty;
        private bool? _lastFocused = null;
        private bool _firstBackdropUpdate = true;

        /// <summary>
        /// Updates the backdrop of the control.
        /// </summary>
        public void UpdateBackDrop()
        {
            if (!this.IsHandleCreated || this.Handle == IntPtr.Zero || !User32.IsWindow(this.Handle)) return;

            if (Flag == Flags.Tabs_Extended)
            {
                BackColor = scheme.Colors.Back_Hover(0);
                return;
            }

            Padding p = Dock switch
            {
                DockStyle.Top => new(0, Height, 0, 0),
                DockStyle.Bottom => new(0, 0, 0, Height),
                DockStyle.Left => new(Width, 0, 0, 0),
                DockStyle.Right => new(0, 0, Width, 0),
                DockStyle.Fill => new(0),
                _ => Padding.Empty
            };

            Form form = FindForm();
            if (form is not null && form.Parent is not null) form = form.Parent.FindForm();

            if (Flag != Flags.System || form is null) return;

            TitlebarTypes type = TitlebarType;

            // Apply only if changed
            bool needsRedraw = _firstBackdropUpdate || _lastBackdropType == null || type != _lastBackdropType || p != _lastBackdropPadding || _lastFocused != _formFocused;
            _firstBackdropUpdate = false;

            switch (type)
            {
                case TitlebarTypes.DWM:
                    BackColor = Color.Black;
                    if (needsRedraw && p != Padding.Empty) form.DropEffect(p);
                    break;
                case TitlebarTypes.DWM_Aero:
                    BackColor = Color.Black;
                    if (needsRedraw && p != Padding.Empty) form.DropEffect(p, false, DWM.DWMStyles.Aero);
                    break;
                case TitlebarTypes.ColorPrevalence:
                    BackColor = _formFocused ? activeTtl : inactiveTtl;
                    break;
                case TitlebarTypes.AppMode:
                    BackColor = Program.Style.DarkMode ? Color.FromArgb(32, 32, 32) : OS.W10 ? Color.White : Color.FromArgb(243, 243, 243);
                    break;
                case TitlebarTypes.Basic:
                case TitlebarTypes.Classic:
                    BackColor = Color.Black;
                    break;
            }

            _lastBackdropType = type;
            _lastBackdropPadding = p;
            _lastFocused = _formFocused;
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