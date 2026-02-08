using AnimatorNS;
using FluentTransitions;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinPaletter.NativeMethods;

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
        private const int WM_MOUSEACTIVATE = 0x0021;
        private const int MA_NOACTIVATE = 0x0003;
        private const int WM_NCHITTEST = 0x0084;
        private const int HTCLIENT = 1;
        private static readonly IntPtr HWND_BOTTOM = new(1);
        private static readonly IntPtr HWND_NOTOPMOST = new(-2);

        public Form()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            _shown = false;

            ApplyStyle(this);
            this.Localize();
            this.DoubleBuffer();

            if (!_showIconAndCaptionText) NativeMethods.Helpers.SetFormTitlebarTextAndIcon(Handle, !_showIconAndCaptionText);

            CheckForIllegalCrossThreadCalls = false;

            base.OnLoad(e);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            _shown = true;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_MOUSEACTIVATE:
                    if (_preventFocusSteal)
                    {
                        // Prevent the window from being activated when clicked
                        m.Result = (IntPtr)MA_NOACTIVATE;
                        return;
                    }
                    break;

                case WM_NCHITTEST:
                    if (_preventFocusSteal)
                    {
                        // Make the entire window respond to mouse events but without activating it
                        m.Result = (IntPtr)HTCLIENT;
                        return;
                    }
                    break;
            }

            base.WndProc(ref m);
        }

        private bool _shown = false;
        /// <summary>
        /// Controls if form is shown
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
                    if (_shown)
                    {
                        NativeMethods.Helpers.SetFormTitlebarTextAndIcon(Handle, !_showIconAndCaptionText);
                        SetBasicTheme(Handle, !_useBasicTheme);
                        SetBasicTheme(Handle, _useBasicTheme);
                    }
                }
            }
        }

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

        private static void SetOpacityLayered(IntPtr handle, double alpha)
        {
            long style = User32.GetWindowLong(handle, GWL_EXSTYLE);
            style |= WS_EX_LAYERED;
            User32.SetWindowLong(handle, GWL_EXSTYLE, style);
            User32.SetLayeredWindowAttributes(handle, 0, (byte)(alpha * 255d), 0x2); // LWA_ALPHA
        }
    }
}
