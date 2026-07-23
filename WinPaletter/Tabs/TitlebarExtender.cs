using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
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
        public static event EventHandler FormFocusChanged;
        private Color? _basicActive = Color.FromArgb(185, 209, 234);
        private Color? _basicInactive = Color.FromArgb(215, 228, 242);
        private static readonly Color _basicActive_Fallback = Color.FromArgb(185, 209, 234);
        private static readonly Color _basicInactive_Fallback = Color.FromArgb(215, 228, 242);

        /// <summary>
        /// Initializes a new instance of the <see cref="TitlebarExtender"/> class.
        /// </summary>
        public TitlebarExtender()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            UpdateBasicThemeColors();
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
            if (this is null || !IsHandleCreated) return;

            UpdateStyles();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            _firstBackdropUpdate = true;   // force redraw
            UpdateBackDrop();
            ((System.Windows.Forms.Form)sender).Load -= Form_Load; // unsubscribe, only need once

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }

        private void OnSystemSettingsUpdated(object sender, UserPreferenceChangedEventArgs e)
        {
            isCompositionEnabled = DWMAPI.IsCompositionEnabled();

            if (e.Category == UserPreferenceCategory.General)
            {
                UpdateBasicThemeColors();
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

        private void UpdateBasicThemeColors()
        {
            _basicActive = GetBasicThemeColor(true);
            _basicInactive = GetBasicThemeColor(false);
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
            else if (TitlebarType == TitlebarTypes.Basic && !Program.ClassicThemeRunning)
            {
                activeTtl = _basicActive ?? _basicActive_Fallback;
                inactiveTtl = _basicInactive ?? _basicInactive_Fallback;
                activeTtlG = _basicActive ?? _basicActive_Fallback;
                inactiveTtlG = _basicInactive ?? _basicInactive_Fallback;
            }
            else
            {
                activeTtl = SystemColors.ActiveCaption;
                inactiveTtl = SystemColors.InactiveCaption;
                activeTtlG = SystemColors.GradientActiveCaption;
                inactiveTtlG = SystemColors.GradientInactiveCaption;
            }
        }

        private Color? GetBasicThemeColor(bool active)
        {
            if (Program.ClassicThemeRunning) return null;

            VisualStyleRenderer renderer = new(active ? VisualStyleElement.Window.Caption.Active : VisualStyleElement.Window.Caption.Inactive);
            using (Bitmap bmp = new(100, 10))
            {
                using (Graphics G = Graphics.FromImage(bmp))
                {
                    renderer.DrawBackground(G, new(0, 0, 100, 10));
                }

                return bmp.GetPixel(bmp.Width / 2, bmp.Height - 1);
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

            // ALWAYS process DWM effects if the form is visible.
            // Don't skip based on _firstBackdropUpdate alone.
            bool needsRedraw = _firstBackdropUpdate || _lastBackdropType == null || type != _lastBackdropType || p != _lastBackdropPadding;

            // For DWM types, always apply the effect when the form is visible
            bool isDwmType = type == TitlebarTypes.DWM || type == TitlebarTypes.DWM_Aero;

            // On Windows 7 with DWM, we need to reapply the effect every time the form is shown
            bool forceApply = isDwmType && form.Visible && !_firstBackdropUpdate;

            _firstBackdropUpdate = false;

            switch (type)
            {
                case TitlebarTypes.DWM:
                    BackColor = Color.Black;
                    if ((needsRedraw || forceApply) && p != Padding.Empty)
                        form.DropEffect(p, FormStyle: Program.Style.DarkMode ? DWM.DWMStyles.Mica : DWM.DWMStyles.Tabbed);
                    break;
                case TitlebarTypes.DWM_Aero:
                    BackColor = Color.Black;
                    if ((needsRedraw || forceApply) && p != Padding.Empty)
                        form.DropEffect(p, false, DWM.DWMStyles.Aero);
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
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // If we are in Classic mode, we handle the background manually in OnPaintBackground to allow for gradients.
            bool isClassicStyle = Flag == Flags.System && (TitlebarType == TitlebarTypes.Classic);

            if (!isClassicStyle)
            {
                base.OnPaintBackground(e);
            }
            else if (isClassicStyle && Width > 0 && Height > 0)
            {
                Rectangle rect = new(0, 0, Width, Height);

                Color c1 = _formFocused ? activeTtl : inactiveTtl;
                Color c2 = _formFocused ? activeTtlG : inactiveTtlG;

                GradientFillCaptionEased(e.Graphics, rect, c1, c2);
            }
            else
            {
                base.OnPaintBackground(e);
            }
        }

        private static void GradientFillCaptionEased(Graphics G, Rectangle rect, Rectangle captionRectangle, Color start, Color end)
        {
            float[] positions = [0.00f, 0.05f, 0.25f, 0.50f, 0.75f, 0.95f, 1.00f];
            float[] tEased = [0.00f, 0.0327f, 0.2456f, 0.5133f, 0.7763f, 0.9934f, 1.00f];

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

            using (LinearGradientBrush brush = new(captionRectangle, start, end, LinearGradientMode.Horizontal))
            {
                brush.InterpolationColors = blend;

                GraphicsState s = G.Save();

                G.PixelOffsetMode = PixelOffsetMode.Half;
                G.SmoothingMode = SmoothingMode.None;

                G.FillRectangle(brush, rect);

                G.Restore(s);
            }
        }

        private static void GradientFillCaptionEased(Graphics G, Rectangle rect, Color start, Color end) => GradientFillCaptionEased(G, rect, rect, start, end);

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
                if (_owner.Flag == Flags.System && (_owner.TitlebarType == TitlebarTypes.Classic))
                {
                    switch (m.Msg)
                    {
                        case (int)WindowsMessage.NCActivate:
                        case (int)WindowsMessage.Activate:
                        case (int)WindowsMessage.ActivateApp:
                            _owner.FormFocused = m.WParam != IntPtr.Zero;
                            FormFocusChanged?.Invoke(_owner, new());
                            base.WndProc(ref m);
                            DrawSeamLine(_owner.FormFocused);
                            return;
                        case (int)WindowsMessage.NCPaint:
                        case (int)WindowsMessage.Paint:
                        case (int)WindowsMessage.NCMouseMove:
                        case (int)WindowsMessage.EraseBkgnd:
                        case (int)WindowsMessage.Print:
                        case (int)WindowsMessage.PrintClient:
                        case (int)WindowsMessage.DrawItem:
                        case (int)WindowsMessage.ChangeUIState:
                        case (int)WindowsMessage.SetText:
                        case (int)WindowsMessage.Size:
                        case (int)WindowsMessage.EnterSizeMove:
                        case (int)WindowsMessage.ExitSizeMove:
                        case (int)WindowsMessage.GetMinMaxInfo:
                            base.WndProc(ref m);
                            DrawSeamLine(_owner.FormFocused);
                            return;
                    }
                }
                else
                {
                    switch (m.Msg)
                    {
                        case (int)WindowsMessage.NCActivate:
                        case (int)WindowsMessage.Activate:
                        case (int)WindowsMessage.ActivateApp:
                            _owner.FormFocused = m.WParam != IntPtr.Zero;
                            FormFocusChanged?.Invoke(_owner, new());
                            base.WndProc(ref m);
                            return;
                    }
                }

                base.WndProc(ref m);
            }
            private void DrawSeamLine(bool isActive)
            {
                if (_owner.TitlebarType != TitlebarTypes.Classic) return;

                IntPtr hdc = User32.GetWindowDC(_form.Handle);
                if (hdc == IntPtr.Zero) return;

                try
                {
                    // Calculate common metrics
                    Size frame = _form.FormBorderStyle switch
                    {
                        FormBorderStyle.Sizable => SystemInformation.FrameBorderSize,
                        FormBorderStyle.FixedSingle => SystemInformation.FixedFrameBorderSize,
                        _ => Size.Empty
                    };

                    User32.GetWindowRect(_form.Handle, out UxTheme.RECT windowRect);
                    int windowWidth = windowRect.right - windowRect.left;
                    int captionHeight = SystemInformation.CaptionHeight;

                    // Get colors
                    Color c1 = isActive ? _owner.activeTtl : _owner.inactiveTtl;
                    Color c2 = isActive ? _owner.activeTtlG : _owner.inactiveTtlG;

                    using (Graphics g = Graphics.FromHdc(hdc))
                    {
                        g.SmoothingMode = SmoothingMode.None;

                        // 1. Handle optional covering of text and icon
                        if (_form is UI.WP.Form wpForm && !wpForm.ShowIconAndCaptionText)
                        {
                            SizeF stringSize = _form.Text.Measure(SystemFonts.CaptionFont);
                            int widthText = (int)(16 + 2 + stringSize.Width);
                            Rectangle maskRect = new(frame.Width, frame.Height, widthText, captionHeight);
                            Rectangle fullCaptionRect = new(frame.Width, frame.Height + captionHeight - 1, windowWidth - (frame.Width * 2), captionHeight);

                            GradientFillCaptionEased(g, maskRect, fullCaptionRect, c1, c2);
                        }

                        // 2. Draw the Seam Line
                        int top = frame.Height + captionHeight - 1;
                        int width = windowWidth - (frame.Width * 2);
                        Rectangle lineRect = new(frame.Width, top, width, 1);

                        GradientFillCaptionEased(g, lineRect, c1, c2);
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