using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.NativeMethods.User32;

namespace WinPaletter.Tabs
{
    /// <summary>
    /// A container control that extends the titlebar of a form using either a DWM effect or a continuation of the tab on <see cref="TabsContainer"/>
    /// </summary>
    public partial class TitlebarExtender : ContainerControl
    {
        Config.Scheme scheme => Enabled ? Program.Style.Schemes.Main : Program.Style.Schemes.Disabled;
        private FormNCPainter _ncPainter;
        public Color activeTtl, inactiveTtl, activeTtlG, inactiveTtlG;
        private Point newPoint = new();
        private Point oldPoint = new();
        private TitlebarTypes? _lastBackdropType = null;
        private Padding _lastBackdropPadding = Padding.Empty;
        private bool _firstBackdropUpdate = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="TitlebarExtender"/> class.
        /// </summary>
        public TitlebarExtender()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            UpdateStyles();

            DoubleBuffered = true;

            BackColor = Color.Black;

            Program.WindowsTransparencyChanged += Program_WindowsTransparencyChanged;
        }

        private void Program_WindowsTransparencyChanged(bool obj)
        {
            UpdateBackDrop();
        }

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

        public bool _formFocused = true;
        public bool FormFocused
        {
            get => _formFocused;
            set
            {
                if (_formFocused != value)
                {
                    _formFocused = value;

                    // Ensure the backdrop logic also processes the new state
                    UpdateBackDrop();

                    // Force the control to redraw its client area
                    this.Invalidate();

                    // Redraw seam line if needed
                    if (Flag == Flags.System && TitlebarType == TitlebarTypes.Classic)
                    {
                        _ncPainter.DrawSeamLine(value);
                    }
                }
            }
        }

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

        protected override void OnHandleCreated(EventArgs e)
        {
            isCompositionEnabled = DWMAPI.IsCompositionEnabled();

            base.OnHandleCreated(e);

            if (!DesignMode)
            {
                System.Windows.Forms.Form form = FindForm();
                if (form != null)
                {
                    // Only keep the Load event for initialization
                    form.Load += Form_Load;
                    SystemEvents.UserPreferenceChanged += OnSystemSettingsUpdated;
                    Config.DarkModeChanged += Config_DarkModeChanged;

                    // Create FormNCPainter to handle all WM messages including focus tracking
                    _ncPainter = new FormNCPainter(this, form);
                }

                UpdateBackDrop();

                // Force initial seam line draw after control is fully created
                if (_ncPainter != null && Flag == Flags.System && TitlebarType == TitlebarTypes.Classic)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        _ncPainter.DrawSeamLine(_formFocused);
                    }));
                }
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (FindForm() != null)
            {
                SystemEvents.UserPreferenceChanged -= OnSystemSettingsUpdated;
                Config.DarkModeChanged -= Config_DarkModeChanged;
            }

            _ncPainter?.ReleaseHandle();
            _ncPainter = null;

            base.OnHandleDestroyed(e);
        }

        private void Config_DarkModeChanged()
        {
            UpdateStyles();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            _firstBackdropUpdate = true;   // force redraw
            UpdateBackDrop();
            ((System.Windows.Forms.Form)sender).Load -= Form_Load; // unsubscribe, only need once

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();

            // Force seam line draw after form loads
            if (_ncPainter != null && Flag == Flags.System && TitlebarType == TitlebarTypes.Classic)
            {
                _ncPainter.DrawSeamLine(_formFocused);
            }
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

                    // Redraw seam line after system settings update
                    if (_ncPainter != null && Flag == Flags.System && TitlebarType == TitlebarTypes.Classic)
                    {
                        _ncPainter.DrawSeamLine(_formFocused);
                    }
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

        /// <summary>
        /// Raises the <see cref="Control.MouseDown"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (this is not TabsContainer) oldPoint = MousePosition - (Size)FindForm()?.Location;

            base.OnMouseDown(e);
        }

        /// <summary>
        /// Raises the <see cref="Control.MouseMove"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this is not TabsContainer && Flag == Flags.System && FindForm() != null && e.Button == MouseButtons.Left)
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

            System.Windows.Forms.Form form = FindForm();
            if (form is not null && form.Parent is not null) form = form.Parent.FindForm();

            if (Flag != Flags.System || form is null) return;

            TitlebarTypes type = TitlebarType;

            UpdateColors();

            // Process only if changed
            bool needsRedraw = _firstBackdropUpdate || _lastBackdropType == null || type != _lastBackdropType || p != _lastBackdropPadding;
            _firstBackdropUpdate = false;

            switch (type)
            {
                case TitlebarTypes.DWM:
                    BackColor = Color.Black;
                    if (needsRedraw && p != Padding.Empty) form.DropEffect(p, FormStyle: Program.Style.DarkMode ? DWM.DWMStyles.Mica : DWM.DWMStyles.Tabbed);
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
                    BackColor = _formFocused ? activeTtl : inactiveTtl;
                    break;
            }

            _lastBackdropType = type;
            _lastBackdropPadding = p;

            // Force seam line redraw if we're in classic mode and the state changed
            if (type == TitlebarTypes.Classic && (needsRedraw))
            {
                _ncPainter?.DrawSeamLine(_formFocused);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // If we are in Classic mode, we handle the background manually in OnPaint to allow for gradients. Do not call base.OnPaintBackground.
            bool isClassicStyle = Flag == Flags.System && (TitlebarType == TitlebarTypes.Classic || TitlebarType == TitlebarTypes.Basic);

            if (!isClassicStyle)
            {
                base.OnPaintBackground(e);
            }
        }

        public static void GradientFillCaptionEased(Graphics G, Rectangle rect, Color start, Color end)
        {
            float[] positions = { 0.00f, 0.05f, 0.25f, 0.50f, 0.75f, 0.95f, 1.00f };
            float[] tEased = { 0.00f, 0.0327f, 0.2456f, 0.5133f, 0.7763f, 0.9934f, 1.00f };

            Color Lerp(Color a, Color b, float t) => Color.FromArgb(255,
                (int)(a.R + (b.R - a.R) * t),
                (int)(a.G + (b.G - a.G) * t),
                (int)(a.B + (b.B - a.B) * t));

            ColorBlend blend = new(positions.Length)
            {
                Positions = positions,
                Colors = new Color[positions.Length]
            };
            for (int i = 0; i < positions.Length; i++) blend.Colors[i] = Lerp(start, end, tEased[i]);

            using (LinearGradientBrush brush = new(rect, start, end, LinearGradientMode.Horizontal))
            {
                brush.InterpolationColors = blend;

                GraphicsState s = G.Save();

                G.PixelOffsetMode = PixelOffsetMode.Half;
                G.SmoothingMode = SmoothingMode.None;

                G.FillRectangle(brush, rect);

                G.Restore(s);
            }
        }

        /// <summary>
        /// Raises the <see cref="Control.Paint"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            bool isClassicStyle = Flag == Flags.System && (TitlebarType == TitlebarTypes.Classic || TitlebarType == TitlebarTypes.Basic);

            if (isClassicStyle && Width > 0 && Height > 0)
            {
                Rectangle rect = new(0, 0, Width, Height);

                Color c1 = _formFocused ? activeTtl : inactiveTtl;
                Color c2 = _formFocused ? activeTtlG : inactiveTtlG;

                GradientFillCaptionEased(e.Graphics, rect, c1, c2);
            }
            else
            {
                base.OnPaint(e);
            }
        }

        /// <summary>
        /// Handles all WM messages for the form's non-client area drawing with optimized performance
        /// </summary>
        private class FormNCPainter : NativeWindow
        {
            private readonly TitlebarExtender _owner;
            private readonly System.Windows.Forms.Form _form;

            public FormNCPainter(TitlebarExtender owner, System.Windows.Forms.Form form)
            {
                _owner = owner;
                _form = form;
                AssignHandle(form.Handle);
            }

            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case (int)User32.WindowsMessage.NCActivate:
                    case (int)User32.WindowsMessage.Activate:
                        // 1. Update the owner state immediately
                        bool isActive = m.WParam != IntPtr.Zero;
                        _owner.FormFocused = isActive;
                        base.WndProc(ref m);
                        return;
                }

                base.WndProc(ref m);
            }

            public void DrawSeamLine(bool isActive)
            {
                IntPtr hdc = User32.GetWindowDC(_form.Handle);
                if (hdc == IntPtr.Zero) return;

                try
                {
                    User32.GetWindowRect(_form.Handle, out var windowRect);
                    int windowWidth = windowRect.right - windowRect.left;
                    int windowHeight = windowRect.bottom - windowRect.top;

                    // Use the form's actual border style logic
                    Size frame = _form.FormBorderStyle switch
                    {
                        FormBorderStyle.Sizable => SystemInformation.FrameBorderSize,
                        FormBorderStyle.FixedSingle => SystemInformation.FixedFrameBorderSize,
                        _ => Size.Empty
                    };

                    int captionHeight = SystemInformation.CaptionHeight;
                    int top = frame.Height + captionHeight - 1;
                    int width = windowWidth - (frame.Width * 2);

                    using (Graphics G = Graphics.FromHdc(hdc))
                    {
                        // Force anti-alias off for classic look
                        GraphicsState s = G.Save();
                        G.SmoothingMode = SmoothingMode.None;

                        Rectangle lineRect = new(frame.Width, top, width, 1);

                        // Get colors directly from the owner to ensure they're current
                        Color c1 = isActive ? _owner.activeTtl : _owner.inactiveTtl;
                        Color c2 = isActive ? _owner.activeTtlG : _owner.inactiveTtlG;

                        GradientFillCaptionEased(G, lineRect, c1, c2);

                        G.Restore(s);
                    }
                }
                finally
                {
                    User32.ReleaseDC(_form.Handle, hdc);
                }
            }
        }
    }
}