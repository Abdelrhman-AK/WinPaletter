using FluentTransitions;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.NativeMethods.UxTheme;

namespace WinPaletter.UI.WP
{
    public partial class Form : System.Windows.Forms.Form
    {
        private const int DWMWA_NCRENDERING_POLICY = 2;
        private const int DWMNCRP_ENABLED = 2;
        private const int DWMNCRP_DISABLED = 1;
        private const uint MF_BYCOMMAND = 0x00000000;
        private const uint MF_ENABLED = 0x00000000;
        private const uint MF_GRAYED = 0x00000001;
        private const uint SC_CLOSE = 0xF060;
        private const int MA_NOACTIVATE = 0x0003;
        private const int HTCLIENT = 1;
        private const int HTCAPTION = 2;
        private static readonly IntPtr HWND_BOTTOM = new(1);
        private static readonly IntPtr HWND_NOTOPMOST = new(-2);
        private Panel _backdropPaddingPanel;
        private static bool layeredSet = false;
        private Bitmap _bordersMsstyles_Active;
        private Bitmap _bordersMsstyles_Inactive;
        private bool isCompositionEnabled = DWMAPI.IsCompositionEnabled();
        private bool _isClassicThemeEnabled = false;

        //public new bool DesignMode => base.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime;

        /// <summary>
        /// Event that is raised after localization is applied. Can be used to perform additional adjustments or translations on the form after the main localization process is complete.
        /// </summary>
        public event Action Localized;

        /// <summary>
        /// Event that is raised when the form's tab is pinned. This can be used to perform actions or updates when the form is pinned in a tabbed interface.
        /// </summary>
        public event EventHandler FormTabPinned;

        /// <summary>
        /// Event that is raised when the form's tab is unpinned. This can be used to perform actions or updates when the form is unpinned in a tabbed interface.
        /// </summary>
        public event EventHandler FormTabUnpinned;

        public Form()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();

            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            if (DesignMode) { base.OnLoad(e); return; }

            _shown = false;
            ApplyStyle(this);
            Localize();
            if (this is not null && IsHandleCreated) Localized?.Invoke();
            this.DoubleBuffer();

            isCompositionEnabled = DWMAPI.IsCompositionEnabled();
            _isClassicThemeEnabled = Program.ClassicThemeRunning;

            if (!_showIconAndCaptionText) NativeMethods.Helpers.SetFormTitlebarTextAndIcon(Handle, !_showIconAndCaptionText);

            CheckForIllegalCrossThreadCalls = false;

            if (FormBorderStyle == FormBorderStyle.None && _borders && isCompositionEnabled && !(OS.WVista || OS.W7 || OS.W8 || OS.W81))
            {
                Opacity = 0;
            }

            if (_backdrop)
            {
                DWM.DropEffect(Handle, Margins: _backdropMargin, Style: _backdropStyle);
                UpdateBackdropPaddingPanel();
            }

            ApplyDWMBorder();

            SystemEvents.UserPreferenceChanged += OnSystemSettingsUpdated;
            UpdateBordersFromVisualStyles();

            base.OnLoad(e);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (DesignMode) return;

            if (FormBorderStyle == FormBorderStyle.None && _borders && DWMAPI.IsCompositionEnabled() && !(OS.WVista || OS.W7 || OS.W8 || OS.W81))
            {
                Transition.With(this, nameof(Opacity), 1.0d).CriticalDamp(Program.AnimationSpan_Quick);
            }

            _shown = true;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (CloseOnClick) Close();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            UpdateBordersFromVisualStyles();
        }

        /// <summary>
        /// Deactivates the form using an animation.
        /// </summary>
        /// <param name="close"></param>
        public new void Deactivate(bool close = true)
        {
            if (_shown)
            {
                Transition
                    .With(this, nameof(Opacity), 0d)
                    .HookOnCompletionInUiThread(this, () => { if (close) base.Close(); })
                    .CriticalDamp(Program.AnimationSpan_Quick);

                _shown = false;
            }
        }

        public new void Close()
        {
            SystemEvents.UserPreferenceChanged -= OnSystemSettingsUpdated;

            if (!DesignMode && FormBorderStyle == FormBorderStyle.None && _borders && isCompositionEnabled && !(OS.WVista || OS.W7 || OS.W8 || OS.W81))
            {
                Deactivate(true);
            }
            else
            {
                base.Close();
            }
        }

        /// <summary>
        /// Centers the form on the primary screen
        /// </summary>
        public new void CenterToScreen()
        {
            Rectangle screenBounds = Screen.PrimaryScreen.WorkingArea;
            Location = new Point((screenBounds.Width - Width) / 2, (screenBounds.Height - Height) / 2);
        }

        /// <summary>
        /// Applies localization to the form using the program's default localizer
        /// or a specified one.
        /// </summary>
        /// <param name="form">The form to localize</param>
        /// <param name="localizer">Optional specific localizer. Uses Program.Localization if null.</param>
        public void Localize(Localizer localizer = null)
        {
            localizer ??= Program.Localization;

            if (localizer != null && Program.Settings.Language.Enabled)
            {
                localizer.ApplyLocalization(this);
            }
        }

        internal void OnTabPinnedStatusChanged(bool pinned)
        {
            (pinned ? FormTabPinned : FormTabUnpinned)?.Invoke(this, EventArgs.Empty);
        }

        internal void OnLocalized()
        {
            Localized?.Invoke();
        }

        public JObject JObject => LocalizerExtensions.JObject(this);

        /// <summary>
        /// The parent form of container holding this form to appear in a tabbed interface. Returns null if the form is not contained in a tabbed interface.
        /// </summary>
        public new UI.WP.Form ParentForm => this.Parent.FindForm() as UI.WP.Form;

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _shown = false;

            Bitmap staleActive = _bordersMsstyles_Active;
            Bitmap staleInactive = _bordersMsstyles_Inactive;

            _bordersMsstyles_Active = null;
            _bordersMsstyles_Inactive = null;

            staleActive?.Dispose();
            staleInactive?.Dispose();
        }

        protected override void WndProc(ref Message m)
        {
            if (DesignMode || !IsHandleCreated)
            {
                base.WndProc(ref m);
                return;
            }

            switch (m.Msg)
            {
                case (int)User32.WindowsMessage.EraseBkgnd: // Never let DefWindowProc paint with DEFAULT_BRUSH
                    {
                        using (Graphics G = Graphics.FromHdc(m.WParam))
                        using (SolidBrush b = new(BackColor))
                        {
                            G.FillRectangle(b, ClientRectangle);
                        }
                        m.Result = (IntPtr)1;
                        break;
                    }

                case (int)User32.WindowsMessage.NCPaint:
                case (int)User32.WindowsMessage.Paint:
                case (int)User32.WindowsMessage.SyncPaint:
                    {
                        ApplyDWMBorder();
                        break;
                    }

                // Prevent focus steal
                case (int)User32.WindowsMessage.MouseActivate:
                    if (_preventFocusSteal)
                    {
                        m.Result = (IntPtr)MA_NOACTIVATE;
                        return;
                    }
                    break;

                case (int)User32.WindowsMessage.NCHitTest:
                    // MoveWhenBorderless takes priority
                    if (_moveWhenBorderless && FormBorderStyle == FormBorderStyle.None)
                    {
                        base.WndProc(ref m);
                        if ((int)m.Result == HTCLIENT)
                        {
                            m.Result = (IntPtr)HTCAPTION;
                            return;
                        }
                    }
                    // If not moving, prevent focus steal if enabled
                    else if (_preventFocusSteal)
                    {
                        m.Result = (IntPtr)HTCLIENT;
                        return;
                    }
                    break;

                // Close on lost focus
                case (int)User32.WindowsMessage.NCActivate:
                    if (CloseOnLostFocus && m.WParam == IntPtr.Zero)
                    {
                        if (Visible && !RectangleToScreen(DisplayRectangle).Contains(Cursor.Position))
                            Deactivate();
                    }
                    break;
            }

            base.WndProc(ref m);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (DesignMode) return cp;
                
                if (!_borders && !DWMAPI.IsCompositionEnabled())
                {
                    cp.ClassStyle |= (int)Win32Control.ControlStyles.DropShadow;
                    cp.ExStyle |= (int)Win32Control.ControlExtendedStyles.Layered;
                }

                // On Win7 (Vista/W8/W81 too), WS_EX_LAYERED on a borderless DWM-bordered window
                // causes DWM to composite through the layered alpha channel, producing a black frame.
                // Strip it from CreateParams; SetOpacityLayered adds it dynamically later if needed.
                if ((OS.WVista || OS.W7 || OS.W8 || OS.W81) && _borders && DWMAPI.IsCompositionEnabled())
                {
                    cp.ExStyle &= ~(int)Win32Control.ControlExtendedStyles.Layered;
                }

                return cp;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // Only update backdrop panel at runtime
            if (!DesignMode && _backdrop && _backdropPaddingPanel != null) UpdateBackdropPaddingPanel();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (!DesignMode && _screenCaptureBlock != ScreenCaptureBlockType.None)
            {
                ApplyScreenCaptureBlock();
            }
        }

        private Padding _contentPadding = default;

        /// <summary>
        /// Gets or sets the padding within the form's client area. When FormBorderStyle is None, Borders is enabled, and the border is actually painted onto the client surface (legacy
        /// non-composited OS or classic theme), the drawn border's width is automatically added on top of this value so docked/anchored child controls don't sit under the border art.
        /// <br></br>Note: this getter returns the declared content padding, not the combined effective padding actually applied to the control's layout.
        /// </summary>
        [Browsable(true)]
        [Category("Layout")]
        [Description("Padding within the form's client area, excluding any auto-added border inset.")]
        public new Padding Padding
        {
            get => _contentPadding;
            set
            {
                if (_contentPadding == value) return;
                _contentPadding = value;
                UpdateBorderPadding();
            }
        }

        private void UpdateBorderPadding()
        {
            if (DesignMode || !IsHandleCreated)
            {
                base.Padding = _contentPadding;
                return;
            }

            Padding effective = _contentPadding;

            if (FormBorderStyle == FormBorderStyle.None && _borders && ShouldUpdateBorders(true))
            {
                int borderWidth = GetDWMBorderWidth() - 1;
                effective.Left += borderWidth;
                effective.Top += borderWidth;
                effective.Right += borderWidth;
                effective.Bottom += borderWidth;
            }

            base.Padding = effective;
            PerformLayout();
        }

        // Backdrop property
        private bool _backdrop = false;
        [Browsable(true)]
        [Category("Advanced Native")]
        [DefaultValue(false)]
        [Description("Enables or disables the DWM backdrop effect on the form.")]
        public bool Backdrop
        {
            get => _backdrop;
            set
            {
                if (_backdrop == value) return;
                _backdrop = value;

                // Only apply at runtime when handle is created
                if (!DesignMode && IsHandleCreated)
                {
                    if (_backdrop)
                    {
                        DWM.DropEffect(Handle, Margins: _backdropMargin, Style: _backdropStyle);
                        UpdateBackdropPaddingPanel();
                    }
                    else
                    {
                        DWM.ResetEffect(Handle);
                        if (_backdropPaddingPanel != null) _backdropPaddingPanel.Visible = false;
                    }
                }
                // In designer mode, just invalidate to show property change
                else if (DesignMode)
                {
                    this.Invalidate(); // Force repaint to show property has changed
                }
            }
        }

        // BackdropStyle property
        private DWM.DWMStyles _backdropStyle = DWM.DWMStyles.None;
        [Browsable(true)]
        [Category("Advanced Native")]
        [DefaultValue(DWM.DWMStyles.None)]
        [Description("Specifies the style of the DWM backdrop effect.")]
        public DWM.DWMStyles BackdropStyle
        {
            get => _backdropStyle;
            set
            {
                if (_backdropStyle == value) return;
                _backdropStyle = value;

                if (!DesignMode && _backdrop && IsHandleCreated)
                    DWM.DropEffect(Handle, Margins: _backdropMargin, Style: _backdropStyle);
                // In designer, just invalidate
                else if (DesignMode)
                    this.Invalidate();
            }
        }

        // BackdropMargin property
        private Padding _backdropMargin = default;
        [Browsable(true)]
        [Category("Advanced Native")]
        [Description("Specifies the margin of the DWM backdrop effect on the form.")]
        public Padding BackdropMargin
        {
            get => _backdropMargin;
            set
            {
                if (_backdropMargin == value) return;
                _backdropMargin = value;

                if (!DesignMode && _backdrop && IsHandleCreated)
                    UpdateBackdropPaddingPanel();
                // In designer, just invalidate
                else if (DesignMode)
                    this.Invalidate();
            }
        }

        // BackdropColor property
        private Color _backdropColor = Color.Black;
        [Browsable(true)]
        [Category("Advanced Native")]
        [Description("Specifies the color drawn behind the DWM backdrop effect. Default is Black.")]
        [DefaultValue(typeof(Color), "Black")]
        public Color BackdropColor
        {
            get => _backdropColor;
            set
            {
                if (_backdropColor == value) return;
                _backdropColor = value;

                if (!DesignMode && _backdropPaddingPanel != null)
                    _backdropPaddingPanel.BackColor = _backdropColor;
                // In designer, just invalidate
                else if (DesignMode)
                    this.Invalidate();
            }
        }

        private bool _moveWhenBorderless = false;
        [Browsable(true)]
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Allows the form to be moved by dragging the mouse when FormBorderStyle is None.")]
        public bool MoveWhenBorderless
        {
            get => _moveWhenBorderless;
            set => _moveWhenBorderless = value;
        }

        private bool _borders = true;

        /// <summary>
        /// Indicates whether the form should have DWM borders (shadow/glass) even with FormBorderStyle.None.
        /// Setting this updates the form immediately.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Indicates whether the form should have DWM borders (shadow/glass) when using FormBorderStyle.None.")]
        public bool Borders
        {
            get => _borders;
            set
            {
                if (_borders != value)
                {
                    _borders = value;
                    if (!DesignMode && IsHandleCreated)
                    {
                        User32.SetWindowPos(Handle, IntPtr.Zero, 0, 0, 0, 0, User32.SetWindowsPosition.NoMove | User32.SetWindowsPosition.NoSize | User32.SetWindowsPosition.NoZOrder | User32.SetWindowsPosition.FrameChanged);
                        UpdateBordersFromVisualStyles();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets DWM (shadow/glass) borders when using <c>FormBorderStyle.None</c> and <c>Borders</c> is set to true. Works for Windows lower than 10.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets DWM (shadow/glass) borders when using FormBorderStyle.None and Borders is set to true. Works for Windows lower than 10.")]
        public int? BorderThickness
        {
            get => _bordersThickness;
            set
            {
                if (_bordersThickness != value)
                {
                    _bordersThickness = value;
                    ApplyDWMBorder();
                    UpdateBorderPadding();
                }
            }
        }
        private int? _bordersThickness = null;

        /// <summary>
        /// Override FormBorderStyle to keep the Borders property in sync.
        /// </summary>
        public new FormBorderStyle FormBorderStyle
        {
            get => base.FormBorderStyle;
            set
            {
                if (base.FormBorderStyle != value)
                {
                    base.FormBorderStyle = value;
                    _borders = value != FormBorderStyle.None || _borders;

                    if (!DesignMode)
                    {
                        UpdateBordersFromVisualStyles();
                        this.Refresh();
                    }
                }
            }
        }

        /// <summary>
        /// Whether the form should close when it loses focus.
        /// </summary>
        public bool CloseOnLostFocus { get; set; } = false;

        private bool _shown = false;
        /// <summary>
        /// Gets if form is shown or not
        /// </summary>
        public bool IsShown
        {
            get => _shown;
        }

        private bool _preventFocusSteal = false;
        [Browsable(true)]
        [Category("Advanced Native")]
        [DefaultValue(false)]
        public bool PreventFocusSteal
        {
            get => _preventFocusSteal;
            set
            {
                if (_preventFocusSteal != value)
                {
                    _preventFocusSteal = value;
                }
            }
        }

        private bool _clickThrough;
        [Browsable(true)]
        [Category("Advanced Native")]
        [DefaultValue(false)]
        public bool ClickThrough
        {
            get => _clickThrough;
            set
            {
                if (_clickThrough == value) return;
                _clickThrough = value;
                if (IsHandleCreated) SetClickThrough(Handle, value);
            }
        }

        private bool _alwaysOnBottom;
        [Browsable(true)]
        [Category("Advanced Native")]
        [DefaultValue(false)]
        public bool AlwaysOnBottom
        {
            get => _alwaysOnBottom;
            set
            {
                if (_alwaysOnBottom == value) return;
                _alwaysOnBottom = value;
                if (IsHandleCreated) SetBottomMost(Handle, value);
            }
        }

        private bool _showIconAndCaptionText = true;
        [Browsable(true)]
        [Category("Advanced Native")]
        [DefaultValue(false)]
        public bool ShowIconAndCaptionText
        {
            get => _showIconAndCaptionText;
            set
            {
                if (_showIconAndCaptionText != value)
                {
                    _showIconAndCaptionText = value;

                    // if set before form is shown, it will make form behind most, unfocused. So we need to update it only if form is already shown.
                    if (!DesignMode && IsHandleCreated && _shown)
                    {
                        NativeMethods.Helpers.SetFormTitlebarTextAndIcon(Handle, !_showIconAndCaptionText);
                        SetBasicTheme(Handle, !_useBasicTheme);
                        SetBasicTheme(Handle, _useBasicTheme);
                    }
                }
            }
        }

        /// <summary>
        /// Whether the form should close when clicked.
        /// </summary>
        public bool CloseOnClick { get; set; } = false;

        private bool _useBasicTheme;
        [Browsable(true)]
        [Category("Advanced Native")]
        [DefaultValue(false)]
        public bool UseBasicTheme
        {
            get => _useBasicTheme;
            set
            {
                if (_useBasicTheme == value) return;
                _useBasicTheme = value;
                if (IsHandleCreated) SetBasicTheme(Handle, value);
            }
        }

        private bool _closeButtonEnabled = true;
        [Browsable(true)]
        [Category("Advanced Native")]
        [DefaultValue(true)]
        public bool CloseButtonEnabled
        {
            get => _closeButtonEnabled;
            set
            {
                if (_closeButtonEnabled == value) return;
                _closeButtonEnabled = value;
                if (IsHandleCreated) SetCloseButtonState(Handle, value);
            }
        }

        private byte _blurOpacity = 255; // default: 0-255
        private bool _blurBehind;

        [Browsable(true)]
        [Category("Advanced Native")]
        [Description("Enables blur behind the window. Use BlurOpacity to adjust intensity.")]
        [DefaultValue(false)]
        public bool BlurBehind
        {
            get => _blurBehind;
            set
            {
                if (_blurBehind == value) return;
                _blurBehind = value;
                if (IsHandleCreated) ApplyBlur(Handle, _blurBehind, _blurOpacity);
            }
        }

        [Browsable(true)]
        [Category("Advanced Native")]
        [Description("Adjusts blur opacity when BlurBehind is enabled (0=transparent, 255=opaque).")]
        [DefaultValue(255)]
        public byte BlurOpacity
        {
            get => _blurOpacity;
            set
            {
                if (_blurOpacity == value) return;
                _blurOpacity = value;
                if (IsHandleCreated && _blurBehind) ApplyBlur(Handle, true, _blurOpacity);
            }
        }

        private double _opacityLayered = 1d;
        [Browsable(true)]
        [Category("Advanced Native")]
        [DefaultValue(1d)]
        public double OpacityLayered
        {
            get => _opacityLayered;
            set
            {
                _opacityLayered = value;
                if (IsHandleCreated) SetOpacityLayered(Handle, value);
            }
        }

        public enum ScreenCaptureBlockType
        {
            /// <summary>
            /// No screen capture blocking is applied.
            /// </summary>
            None = 0,

            /// <summary>
            /// Blocks screen capture by making the window content appear black or blank in screenshots.
            /// Works on all Windows versions that support display affinity (Windows 7 and later).
            /// </summary>
            BlackBlock = 1,  // Maps to WDA_MONITOR

            /// <summary>
            /// Excludes the window from screen capture completely.
            /// Only works on Windows 10 2004 (build 19041) and later.
            /// Falls back to Basic on older systems.
            /// </summary>
            HideWindow = 2,  // Maps to WDA_EXCLUDEFROMCAPTURE
        }

        private ScreenCaptureBlockType _screenCaptureBlock = ScreenCaptureBlockType.None;

        [Browsable(true)]
        [Category("Advanced Native")]
        [DefaultValue(ScreenCaptureBlockType.None)]
        [Description("Controls how the window content is protected from screen capture. Advanced mode excludes the window from capture (Windows 10 2004+), Basic mode makes content appear black in screenshots.")]
        public ScreenCaptureBlockType ScreenCaptureBlock
        {
            get => _screenCaptureBlock;
            set
            {
                if (_screenCaptureBlock == value) return;
                _screenCaptureBlock = value;

                if (IsHandleCreated)
                {
                    ApplyScreenCaptureBlock();
                }
            }
        }

        private void ApplyScreenCaptureBlock()
        {
            uint affinity = User32.WDA_NONE;
            bool useAdvanced = false;

            switch (_screenCaptureBlock)
            {
                case ScreenCaptureBlockType.BlackBlock:
                    affinity = User32.WDA_MONITOR;
                    break;

                case ScreenCaptureBlockType.HideWindow:
                    // Check if OS supports advanced mode
                    if (OS.W10_2004 || OS.W11 || OS.W12)
                    {
                        affinity = User32.WDA_EXCLUDEFROMCAPTURE;
                        useAdvanced = true;
                    }
                    else
                    {
                        // Fall back to basic on older OS
                        affinity = User32.WDA_MONITOR;
                    }
                    break;

                case ScreenCaptureBlockType.None:
                default:
                    affinity = User32.WDA_NONE;
                    break;
            }

            // Try to set the display affinity
            if (!User32.SetWindowDisplayAffinity(Handle, affinity))
            {
                // If advanced fails and we haven't already fallen back to basic
                if (useAdvanced)
                {
                    // Try fallback to basic
                    affinity = User32.WDA_MONITOR;
                    User32.SetWindowDisplayAffinity(Handle, affinity);
                }
                // If basic fails, there's not much we can do
            }
        }

        private static void SetClickThrough(IntPtr handle, bool enabled)
        {
            if (enabled) 
                Win32Control.AppendExtendedStyle(handle, Win32Control.ControlExtendedStyles.Transparent | Win32Control.ControlExtendedStyles.Layered);
            else
                Win32Control.RemoveExtendedStyle(handle, Win32Control.ControlExtendedStyles.Transparent);
        }

        private static void SetBottomMost(IntPtr handle, bool enabled)
        {
            User32.SetWindowPos(handle, enabled ? HWND_BOTTOM : HWND_NOTOPMOST, 0, 0, 0, 0, User32.SetWindowsPosition.NoSize | User32.SetWindowsPosition.NoMove | User32.SetWindowsPosition.NoActivate);
        }

        private static void SetBasicTheme(IntPtr handle, bool enabled)
        {
            int policy = enabled ? DWMNCRP_DISABLED : DWMNCRP_ENABLED;
            DWMAPI.DwmSetWindowAttribute(handle, DWMWA_NCRENDERING_POLICY, ref policy, sizeof(int));
        }

        private static void SetCloseButtonState(IntPtr handle, bool enabled)
        {
            IntPtr menu = User32.GetSystemMenu(handle, false);
            User32.EnableMenuItem(menu, SC_CLOSE, MF_BYCOMMAND | (enabled ? MF_ENABLED : MF_GRAYED));
        }

        private static void ApplyBlur(IntPtr hwnd, bool enable, byte opacity)
        {
            if (OS.WVista || OS.W7 || OS.W8 || OS.W81)
            {
                // DwmEnableBlurBehindWindow is the only blur API on Vista/7/8/8.1 opacity is not supported by this API; the blur is always full
                DWMAPI.DWM_BLURBEHIND bb = new()
                {
                    dwFlags = DWMAPI.DWM_BB_ENABLE,
                    fEnable = enable,
                    hRgnBlur = IntPtr.Zero,
                    fTransitionOnMaximized = false
                };
                DWMAPI.DwmEnableBlurBehindWindow(hwnd, bb);
                return;
            }

            // Windows 10+ acrylic blur via SetWindowCompositionAttribute
            if (Environment.OSVersion.Version.Build < 10240) return;

            var accent = new User32.AccentPolicy
            {
                AccentState = enable ? User32.AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND : User32.AccentState.ACCENT_DISABLED,
                GradientColor = (opacity << 24) // Alpha channel for opacity
            };

            int size = Marshal.SizeOf(accent);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(accent, ptr, false);

            var data = new User32.WindowCompositionAttributeData
            {
                Attribute = User32.WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = size,
                Data = ptr
            };

            User32.SetWindowCompositionAttribute(hwnd, ref data);
            Marshal.FreeHGlobal(ptr);
        }

        private void ApplyDWMBorder()
        {
            if (_borders && FormBorderStyle == FormBorderStyle.None && DWMAPI.IsCompositionEnabled())
            {
                int val = 2;
                DWMAPI.DwmSetWindowAttribute(Handle, Program.Style.RoundedCorners ? 2 : 1, ref val, 4);
                int borderWidth = GetDWMBorderWidth();

                DWMAPI.MARGINS margins = new()
                {
                    topHeight = borderWidth,
                    bottomHeight = borderWidth,
                    leftWidth = borderWidth,
                    rightWidth = borderWidth,
                };
                DWMAPI.DwmExtendFrameIntoClientArea(Handle, ref margins);
            }
        }

        private int GetDWMBorderWidth()
        {
            if (DesignMode) return 1;

            if (_bordersThickness != null && (OS.WXP || OS.WVista || OS.W7 || OS.W8x))
            {
                if (_bordersThickness == -1 && ShouldUpdateBorders()) return GetDWMBorderWidth_Internal(); else return _bordersThickness ?? GetDWMBorderWidth_Internal();
            }

            return GetDWMBorderWidth_Internal();
        }

        private int GetDWMBorderWidth_Internal()
        {
            if (OS.WXP) return Math.Max(1, User32.GetSystemMetrics(5)); // SM_CXBORDER = 5

            if (OS.WVista || OS.W7 || OS.W8 || OS.W81)
            {
                // SM_CXSIZEFRAME (32) = resize border, SM_CXPADDEDBORDER (92) = DWM padding
                // Together they form the full invisible DWM border on Aero glass windows
                int sizeFrame = User32.GetSystemMetrics(32); // SM_CXSIZEFRAME
                int paddedBorder = User32.GetSystemMetrics(92); // SM_CXPADDEDBORDER
                return Math.Max(1, sizeFrame + paddedBorder);
            }

            // DWMWA_EXTENDED_FRAME_BOUNDS = 9
            // Returns the visible frame rect excluding the invisible resize border
            int result = DWMAPI.DwmGetWindowAttribute(Handle, 9, out RECT extendedFrame, Marshal.SizeOf<RECT>());

            if (result != 0) // S_OK
                return 1;

            User32.GetWindowRect(Handle, out RECT windowRect);

            // The difference on any edge is the invisible DWM border thickness
            int borderWidth = extendedFrame.left - windowRect.left;

            // Clamp: should always be >= 1 on a bordered window, but never negative
            return Math.Max(1, borderWidth);
        }

        private static void SetOpacityLayered(IntPtr handle, double alpha)
        {
            if (!layeredSet)
            {
                Win32Control.AppendExtendedStyle(handle, Win32Control.ControlExtendedStyles.Layered);
                layeredSet = true;
            }

            byte bAlpha = (byte)(Math.Max(0, Math.Min(1, alpha)) * 255d);
            User32.SetLayeredWindowAttributes(handle, 0, bAlpha, NativeMethods.User32.LWA_ALPHA);
        }

        private void UpdateBackdropPaddingPanel()
        {
            if (DesignMode)
            {
                // In designer mode, we shouldn't create or modify actual controls
                // Just return without doing anything
                return;
            }

            if (_backdropPaddingPanel == null)
            {
                _backdropPaddingPanel = new()
                {
                    Parent = this,
                    Enabled = false,
                    TabStop = false,
                    BackColor = _backdropColor
                };
                _backdropPaddingPanel.SendToBack();
            }

            Padding m = _backdropMargin == default ? new Padding(0) : _backdropMargin;
            _backdropPaddingPanel.Bounds = new Rectangle(m.Left, m.Top, Math.Max(0, Width - m.Left - m.Right), Math.Max(0, Height - m.Top - m.Bottom));

            _backdropPaddingPanel.BackColor = _backdropColor;
            _backdropPaddingPanel.Visible = _backdrop;
        }

        private void OnSystemSettingsUpdated(object sender, UserPreferenceChangedEventArgs e)
        {
            isCompositionEnabled = DWMAPI.IsCompositionEnabled();

            bool wasClassicThemeEnabled = _isClassicThemeEnabled;
            _isClassicThemeEnabled = Program.ClassicThemeRunning;

            if (IsHandleCreated && ShouldUpdateBorders())
            {
                UpdateBordersFromVisualStyles();
            }
        }

        private bool ShouldUpdateBorders(bool skipNoDWMCompositopn = false)
        {
            // 1. FormBorderStyle is none and _borders property is true
            bool isBorderlessWithBorders = WindowState == FormWindowState.Normal && FormBorderStyle == FormBorderStyle.None && _borders;

            // 2. Program.ClassicThemeRunning or DWM.IsCompositionEnabled is false
            bool isLegacyOrClassic = skipNoDWMCompositopn || (Program.ClassicThemeRunning || !DWMAPI.IsCompositionEnabled());

            // 3. Form is not hosted in a control (Parent is null)
            bool isTopLevel = Parent == null;

            return !DesignMode && isBorderlessWithBorders && isLegacyOrClassic && isTopLevel;
        }

        /// <summary>
        /// Draws flat 3D raised edges around the client area, matching how classic (non-visual-styles) theme
        /// draws window frames. Used instead of the msstyles frame bitmaps when the classic theme is active,
        /// since visual style renderers have no frame art to draw from in that case.
        /// </summary>
        private void DrawClassicEdges(Graphics g)
        {
            Rectangle rect = new(0, 0, Width, Height);
            ControlPaint.DrawBorder3D(g, rect, Border3DStyle.Raised, Border3DSide.Left | Border3DSide.Right | Border3DSide.Bottom | Border3DSide.Top);
        }

        private Bitmap GetBordersFromVisualStyles(bool active)
        {
            if (Program.ClassicThemeRunning) return null; // Accessing Visual Styles with Classic theme enabled leads to exceptions errors
            if (!ShouldUpdateBorders()) return null;

            int bordersWidth = GetDWMBorderWidth();

            System.Windows.Forms.VisualStyles.VisualStyleElement el_left;
            System.Windows.Forms.VisualStyles.VisualStyleElement el_right;
            System.Windows.Forms.VisualStyles.VisualStyleElement el_bottom;

            el_left = active ? System.Windows.Forms.VisualStyles.VisualStyleElement.Window.FrameLeft.Active :
                               System.Windows.Forms.VisualStyles.VisualStyleElement.Window.FrameLeft.Inactive;

            el_right = active ? System.Windows.Forms.VisualStyles.VisualStyleElement.Window.FrameRight.Active :
                                System.Windows.Forms.VisualStyles.VisualStyleElement.Window.FrameRight.Inactive;

            el_bottom = active ? System.Windows.Forms.VisualStyles.VisualStyleElement.Window.FrameBottom.Active :
                                 System.Windows.Forms.VisualStyles.VisualStyleElement.Window.FrameBottom.Inactive;

            System.Windows.Forms.VisualStyles.VisualStyleRenderer r_left = new(el_left);
            System.Windows.Forms.VisualStyles.VisualStyleRenderer r_right = new(el_right);
            System.Windows.Forms.VisualStyles.VisualStyleRenderer r_bottom = new(el_bottom);

            Region reg_left = null;
            Region reg_right = null;
            Region reg_bottom = null;

            Rectangle rect = new(0, 0, Width, Height);
            Rectangle rect_left;
            Rectangle rect_top;
            Rectangle rect_right;
            Rectangle rect_bottom;

            Bitmap b = new(Width, Height);

            try
            {
                using (Graphics G = Graphics.FromImage(b))
                {
                    reg_left = r_left.GetBackgroundRegion(G, rect);
                    reg_right = r_right.GetBackgroundRegion(G, rect);
                    reg_bottom = r_bottom.GetBackgroundRegion(G, rect);

                    rect_left = Rectangle.Round(reg_left.GetBounds(G));
                    rect_right = Rectangle.Round(reg_right.GetBounds(G));
                    rect_bottom = Rectangle.Round(reg_bottom.GetBounds(G));

                    Size frame = new(Math.Max(bordersWidth, SystemInformation.FrameBorderSize.Width), Math.Max(bordersWidth, SystemInformation.FrameBorderSize.Height));

                    rect_left = new(rect_left.X, rect_left.Y, frame.Width, rect_left.Height);
                    rect_right = new(Width - frame.Width, rect_right.Y, frame.Width, rect_right.Height);
                    rect_bottom = new(rect_bottom.X, Height - frame.Height, rect_bottom.Width, frame.Height);
                    rect_top = new(rect_bottom.X, 0, rect_bottom.Width, rect_bottom.Height);

                    r_left.DrawBackground(G, rect_left);
                    r_right.DrawBackground(G, rect_right);
                    r_bottom.DrawBackground(G, rect_bottom);

                    using (Bitmap b_cropped = b.Clone(rect_bottom, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
                    {
                        b_cropped.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                        G.DrawImage(b_cropped, rect_top);
                    }
                }

                return b;
            }
            catch
            {
                b.Dispose();
                throw;
            }
            finally
            {
                reg_left?.Dispose();
                reg_right?.Dispose();
                reg_bottom?.Dispose();
            }
        }

        private void UpdateBordersFromVisualStyles()
        {
            if (DesignMode || !ShouldUpdateBorders())
            {
                DisposeBorders();
                UpdateBorderPadding();
                return;
            }

            // 1. Handle Classic Theme cleanup
            if (_isClassicThemeEnabled)
            {
                DisposeBorders();
                UpdateBorderPadding();
                Invalidate();
                return;
            }

            // 2. Generate new resources
            Bitmap newActive = GetBordersFromVisualStyles(true);
            Bitmap newInactive = GetBordersFromVisualStyles(false);

            // 3. Swap and dispose old resources safely
            Bitmap oldActive = _bordersMsstyles_Active;
            Bitmap oldInactive = _bordersMsstyles_Inactive;

            _bordersMsstyles_Active = newActive;
            _bordersMsstyles_Inactive = newInactive;

            oldActive?.Dispose();
            oldInactive?.Dispose();

            UpdateBorderPadding();
            Invalidate();
        }

        /// <summary>
        /// Helper to cleanly dispose of existing border bitmaps.
        /// </summary>
        private void DisposeBorders()
        {
            Bitmap tempActive = _bordersMsstyles_Active;
            Bitmap tempInactive = _bordersMsstyles_Inactive;

            _bordersMsstyles_Active = null;
            _bordersMsstyles_Inactive = null;

            tempActive?.Dispose();
            tempInactive?.Dispose();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (ShouldUpdateBorders())
            {
                if (_isClassicThemeEnabled)
                {
                    DrawClassicEdges(e.Graphics);
                }
                else
                {
                    Bitmap b = Focused ? _bordersMsstyles_Active : _bordersMsstyles_Inactive;
                    if (b is not null && b.IsValid()) e.Graphics.DrawImage(b, new Point(0, 0));
                }
            }
        }
    }
}