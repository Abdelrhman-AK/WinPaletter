using FluentTransitions;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.UI.Style.Config.Colors_Collection;

namespace WinPaletter.UI.WP
{
    public partial class Form : System.Windows.Forms.Form
    {
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TRANSPARENT = 0x00000020;
        private const int WS_EX_LAYERED = 0x00080000;
        private const int DWMWA_NCRENDERING_POLICY = 2;
        private const int DWMNCRP_ENABLED = 2;
        private const int DWMNCRP_DISABLED = 1;
        private const uint MF_BYCOMMAND = 0x00000000;
        private const uint MF_ENABLED = 0x00000000;
        private const uint MF_GRAYED = 0x00000001;
        private const uint SC_CLOSE = 0xF060;
        private const uint WM_NCACTIVATE = 0x86U;
        private const int WM_MOUSEACTIVATE = 0x0021;
        private const int MA_NOACTIVATE = 0x0003;
        private const int WM_NCHITTEST = 0x0084;
        private const int HTCLIENT = 1;
        private const int HTCAPTION = 2;
        private static readonly IntPtr HWND_BOTTOM = new(1);
        private static readonly IntPtr HWND_NOTOPMOST = new(-2);
        private Panel _backdropPaddingPanel;
        private static bool layeredSet = false;

        //public new bool DesignMode => base.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime;

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
            this.Localize();
            this.DoubleBuffer();

            if (!_showIconAndCaptionText) NativeMethods.Helpers.SetFormTitlebarTextAndIcon(Handle, !_showIconAndCaptionText);

            CheckForIllegalCrossThreadCalls = false;

            if (FormBorderStyle == FormBorderStyle.None && _borders && DWMAPI.IsCompositionEnabled()) Opacity = 0;

            if (_backdrop)
            {
                DWM.DropEffect(Handle, Margins: _backdropMargin, Style: _backdropStyle);
                UpdateBackdropPaddingPanel();
            }

            ApplyDWMBorder();

            base.OnLoad(e);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (DesignMode) return;

            if (FormBorderStyle == FormBorderStyle.None && _borders && DWMAPI.IsCompositionEnabled())
            {
                Transition.With(this, nameof(Opacity), 1.0d).CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));
            }

            _shown = true;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (CloseOnClick) Close();
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
                    .CriticalDamp(TimeSpan.FromMilliseconds(Program.AnimationDuration_Quick));

                _shown = false;
            }
        }

        public new void Close()
        {
            if (!DesignMode && FormBorderStyle == FormBorderStyle.None && _borders && DWMAPI.IsCompositionEnabled())
            {
                Deactivate(true);
            }
            else
            {
                base.Close();
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _shown = false;
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
                // DWM/Aero borders and shadow
                case DWMAPI.WM_NCPAINT:
                    ApplyDWMBorder();
                    break;

                // Prevent focus steal
                case WM_MOUSEACTIVATE:
                    if (_preventFocusSteal)
                    {
                        m.Result = (IntPtr)MA_NOACTIVATE;
                        return;
                    }
                    break;

                case WM_NCHITTEST:
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
                case (int)WM_NCACTIVATE:
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
                    cp.ClassStyle |= DWMAPI.CS_DROPSHADOW;
                    cp.ExStyle |= 33554432; // WS_EX_LAYERED
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
                        User32.SetWindowPos(Handle, IntPtr.Zero, 0, 0, 0, 0, User32.SWP_NOMOVE | User32.SWP_NOSIZE | User32.SWP_NOZORDER | User32.SWP_FRAMECHANGED);
                    }
                }
            }
        }

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

                    // Keep Borders in sync: if the style is None, Borders can still be true
                    _borders = value == FormBorderStyle.None ? _borders : true;

                    if (!DesignMode)
                        this.Refresh();
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

        private byte _blurOpacity = 128; // default: 0-255
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
        [DefaultValue(128)]
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
            long style = User32.GetWindowLong(handle, GWL_EXSTYLE);
            if (enabled) style |= WS_EX_TRANSPARENT | WS_EX_LAYERED;
            else style &= ~WS_EX_TRANSPARENT;
            User32.SetWindowLong(handle, GWL_EXSTYLE, style);
        }

        private static void SetBottomMost(IntPtr handle, bool enabled)
        {
            User32.SetWindowPos(handle, enabled ? HWND_BOTTOM : HWND_NOTOPMOST, 0, 0, 0, 0, 0x0002 | 0x0001 | 0x0010);
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
            if (Environment.OSVersion.Version.Build < 10240) return; // Windows 10+

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

                DWMAPI.MARGINS margins = new()
                {
                    topHeight = 1,
                    bottomHeight = 1,
                    leftWidth = 1,
                    rightWidth = 1,
                };
                DWMAPI.DwmExtendFrameIntoClientArea(Handle, ref margins);
            }
        }

        private static void SetOpacityLayered(IntPtr handle, double alpha)
        {
            if (!layeredSet)
            {
                long style = User32.GetWindowLong(handle, GWL_EXSTYLE);
                style |= WS_EX_LAYERED;
                User32.SetWindowLong(handle, GWL_EXSTYLE, style);
                layeredSet = true;
            }

            byte bAlpha = (byte)(Math.Max(0, Math.Min(1, alpha)) * 255d);
            User32.SetLayeredWindowAttributes(handle, 0, bAlpha, 0x2); // LWA_ALPHA
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
                _backdropPaddingPanel = new Panel
                {
                    Parent = this,
                    Enabled = false,
                    TabStop = false,
                    BackColor = _backdropColor
                };
                _backdropPaddingPanel.SendToBack();
            }

            Padding m = _backdropMargin == default ? new Padding(0) : _backdropMargin;
            _backdropPaddingPanel.Bounds = new Rectangle(
                m.Left,
                m.Top,
                Math.Max(0, Width - m.Left - m.Right),
                Math.Max(0, Height - m.Top - m.Bottom)
            );

            _backdropPaddingPanel.BackColor = _backdropColor;
            _backdropPaddingPanel.Visible = _backdrop;
        }
    }
}
