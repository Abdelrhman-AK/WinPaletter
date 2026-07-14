using System;
using System.Collections.Generic;
using System.Text;
using WinPaletter.NativeMethods;

namespace WinPaletter.UI
{
    /// <summary>
    /// Provides a high-level wrapper for interacting with Win32 window handles (HWND).
    /// </summary>
    /// <remarks>
    /// This class simplifies the interaction with the Windows API by abstracting complex
    /// calls like <c>GetWindowLong</c> and <c>SetWindowPos</c>. Developers can use this
    /// to inspect or modify window styles and classifications without manually managing
    /// bitwise operations or raw handle pointers.
    /// </remarks>
    public class Win32Control
    {
        /// <summary>
        /// The native handle to the window or control.
        /// </summary>
        public IntPtr Handle { get; }

        /// <summary>
        /// The unique numeric identifier (ID) assigned to the control within its parent dialog.
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// The registered Windows class name (e.g., "Button", "SysListView32", "Edit").
        /// </summary>
        public string ClassName { get; }

        /// <summary>
        /// The identified <see cref="ControlType"/>, determined based on the class name and specific styles.
        /// </summary>
        public ControlType Type { get; }

        /// <summary>
        /// Gets or sets the bitmask representing the current standard window styles (GWL_STYLE).
        /// </summary>
        /// <remarks>
        /// Setting this property automatically calls <c>SetWindowPos</c> to force the
        /// OS to redraw the window frame and reflect the style changes.
        /// </remarks>
        public ControlStyles Style
        {
            get => (ControlStyles)(uint)User32.GetWindowLong(Handle, (int)NativeMethods.User32.WindowsLongs.Style);
            set => ApplyStyle((int)NativeMethods.User32.WindowsLongs.Style, (uint)value);
        }

        /// <summary>
        /// Gets or sets the bitmask representing the current extended window styles (GWL_EXSTYLE).
        /// </summary>
        /// <remarks>
        /// Setting this property automatically calls <c>SetWindowPos</c> to force the
        /// OS to redraw the window frame and reflect the style changes.
        /// </remarks>
        public ControlExtendedStyles ExtendedStyle
        {
            get => (ControlExtendedStyles)(uint)User32.GetWindowLong(Handle, (int)NativeMethods.User32.WindowsLongs.ExtendedStyle);
            set => ApplyStyle((int)NativeMethods.User32.WindowsLongs.ExtendedStyle, (uint)value);
        }

        /// <summary>
        /// Retrieves a list of all child controls (if any) contained within this window.
        /// </summary>
        public List<Win32Control> Controls
        {
            get
            {
                List<Win32Control> controls = [];
                User32.EnumChildWindows(Handle, (childHwnd, lParam) =>
                {
                    controls.Add(new(childHwnd));
                    return true; // Continue enumeration
                }, IntPtr.Zero);
                return controls;
            }
        }

        /// <summary>
        /// Recursively retrieves a list of all child controls and their descendants contained within this window.
        /// </summary>
        public List<Win32Control> ControlsRecursive
        {
            get
            {
                List<Win32Control> controls = [];
                void EnumerateChildren(IntPtr parentHwnd)
                {
                    User32.EnumChildWindows(parentHwnd, (childHwnd, lParam) =>
                    {
                        Win32Control control = new(childHwnd);
                        controls.Add(control);
                        EnumerateChildren(childHwnd); // Recursively enumerate children
                        return true; // Continue enumeration
                    }, IntPtr.Zero);
                }
                EnumerateChildren(Handle);
                return controls;
            }
        }

        /// <summary>
        /// Updates the internal window style or extended style and triggers a frame recalculation.
        /// </summary>
        /// <param name="nIndex">The index of the window data to set (GWL_STYLE or GWL_EXSTYLE).</param>
        /// <param name="value">The new bitwise combination of flags.</param>
        private void ApplyStyle(int nIndex, uint value)
        {
            User32.SetWindowLong(Handle, nIndex, value);

            // Force the non-client area (borders/frame) to recalculate
            // SWP_FRAMECHANGED (0x0020) is required for style changes to take visual effect
            User32.SetWindowPos(Handle, IntPtr.Zero, 0, 0, 0, 0, User32.SetWindowsPosition.NoMove | User32.SetWindowsPosition.NoSize | User32.SetWindowsPosition.NoZOrder | User32.SetWindowsPosition.FrameChanged);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Win32Control"/> class.
        /// </summary>
        /// <param name="hWnd">The target native window handle.</param>
        public Win32Control(IntPtr hWnd)
        {
            Handle = hWnd;
            ID = User32.GetDlgCtrlID(hWnd);

            StringBuilder sb = new(265);
            User32.GetClassName(hWnd, sb, sb.Capacity);
            ClassName = sb.ToString();

            Type = DetectType();
        }

        /// <summary>
        /// Creates a <see cref="Win32Control"/> instance by searching for a control ID within a parent dialog.
        /// </summary>
        /// <param name="dialogHwnd">The handle of the container window.</param>
        /// <param name="controlId">The resource ID of the child control.</param>
        /// <returns>A new <see cref="Win32Control"/> instance, or null if the child control could not be found.</returns>
        public static Win32Control FromParent(IntPtr dialogHwnd, int controlId)
        {
            IntPtr hCtrl = User32.GetDlgItem(dialogHwnd, controlId);
            return hCtrl != IntPtr.Zero ? new(hCtrl) : null;
        }

        /// <summary>
        /// Automatically determines the <see cref="ControlType"/> by inspecting the class name and current window styles.
        /// </summary>
        /// <returns>The detected <see cref="ControlType"/>.</returns>
        private ControlType DetectType()
        {
            // 1. Identify by ClassName with Contains for better flexibility
            if (ClassName.Contains("SysListView32", StringComparison.OrdinalIgnoreCase) || ID == 12297)
                return ControlType.ListView;

            if (ClassName.Contains("Edit", StringComparison.OrdinalIgnoreCase) || ID == 12290)
                return ControlType.Edit;

            if (ClassName.Contains("ListBox", StringComparison.OrdinalIgnoreCase))
                return ControlType.ListBox;

            if (ClassName.Contains("ComboBox", StringComparison.OrdinalIgnoreCase))
                return ControlType.ComboBox;

            if (ClassName.Contains("ScrollBar", StringComparison.OrdinalIgnoreCase))
                return ControlType.ScrollBar;

            if (ClassName.Contains("Auto-Suggest Dropdown", StringComparison.OrdinalIgnoreCase))
                return ControlType.AutoSuggestDropdown;

            // 2. Handle specialized Static/Button classifications
            switch (ClassName)
            {
                case string s when s.Equals("Static", StringComparison.OrdinalIgnoreCase):
                    return ((uint)Style & 0x0000000F) == 0x00000007 ? ControlType.GroupBox : ControlType.Static;

                case string s when s.Equals("Button", StringComparison.OrdinalIgnoreCase):
                    return ClassifyButton((uint)Style & 0x0000000F);

                case string s when s.StartsWith("RichEdit", StringComparison.OrdinalIgnoreCase):
                    return ControlType.RichEdit;

                case string s when s.Equals("ComboBoxEx32", StringComparison.OrdinalIgnoreCase):
                    return ControlType.ComboBoxEx;

                case string s when s.Equals("SysTreeView32", StringComparison.OrdinalIgnoreCase):
                    return ControlType.TreeView;

                case string s when s.Equals("SysTabControl32", StringComparison.OrdinalIgnoreCase):
                    return ControlType.TabControl;

                case string s when s.Equals("ToolbarWindow32", StringComparison.OrdinalIgnoreCase):
                    return ControlType.ToolBar;

                case string s when s.Equals("msctls_statusbar32", StringComparison.OrdinalIgnoreCase):
                    return ControlType.StatusBar;

                case string s when s.Equals("msctls_trackbar32", StringComparison.OrdinalIgnoreCase):
                    return ControlType.TrackBar;

                case string s when s.Equals("msctls_updown32", StringComparison.OrdinalIgnoreCase):
                    return ControlType.UpDown;

                case string s when s.Equals("msctls_progress32", StringComparison.OrdinalIgnoreCase):
                    return ControlType.ProgressBar;

                case string s when s.Equals("msctls_hotkey32", StringComparison.OrdinalIgnoreCase):
                    return ControlType.HotKey;

                case string s when s.Equals("SysHeader32", StringComparison.OrdinalIgnoreCase):
                    return ControlType.Header;

                case string s when s.Equals("SysAnimate32", StringComparison.OrdinalIgnoreCase):
                    return ControlType.Animate;

                case string s when s.Equals("SysDateTimePick32", StringComparison.OrdinalIgnoreCase):
                    return ControlType.DateTimePicker;

                case string s when s.Equals("SysMonthCal32", StringComparison.OrdinalIgnoreCase):
                    return ControlType.MonthCalendar;

                case string s when s.Equals("SysIPAddress32", StringComparison.OrdinalIgnoreCase):
                    return ControlType.IPAddress;

                case string s when s.Equals("SysPager", StringComparison.OrdinalIgnoreCase):
                    return ControlType.Pager;

                case string s when s.Equals("SysLink", StringComparison.OrdinalIgnoreCase):
                    return ControlType.Link;

                case string s when s.Equals("NativeFontCtl", StringComparison.OrdinalIgnoreCase):
                    return ControlType.NativeFont;

                case string s when s.Equals("#32770", StringComparison.OrdinalIgnoreCase):
                    return ControlType.Dialog;

                case string s when s.Equals("MDIClient", StringComparison.OrdinalIgnoreCase):
                    return ControlType.MDIClient;

                default:
                    return ControlType.Unknown;
            }
        }

        /// <summary>
        /// Refines the control type for Windows "Button" classes by examining the BS_TYPEMASK style bits.
        /// </summary>
        /// <param name="styleMask">The lower 4 bits of the window style flags (BS_TYPEMASK).</param>
        /// <returns>The refined <see cref="ControlType"/> (e.g., RadioButton, CheckBox).</returns>
        private static ControlType ClassifyButton(uint styleMask)
        {
            return styleMask switch
            {
                0x0 => ControlType.PushButton,
                0x1 => ControlType.DefaultPushButton,
                0x2 or 0x5 => ControlType.CheckBox,
                0x3 or 0x6 => ControlType.CheckBox,
                0x4 or 0x9 => ControlType.RadioButton,
                0x7 => ControlType.GroupBox,
                0xA => ControlType.PushButton,
                0xB => ControlType.OwnerDrawButton,
                0xC or 0xD => ControlType.SplitButton,
                0xE or 0xF => ControlType.CommandLink,
                _ => ControlType.Button
            };
        }

        /// <summary>
        /// Defines standard Win32 window styles (GWL_STYLE / WS_*).
        /// </summary>
        [Flags]
        public enum ControlStyles : uint
        {
            /// <summary>
            /// No style / an overlapped window.
            /// </summary>
            Overlapped = 0x00000000,

            /// <summary>
            /// Same numeric value as <see cref="Overlapped"/>.
            /// </summary>
            Tiled = 0x00000000,

            /// <summary>
            /// The window is a pop-up window.
            /// </summary>
            Popup = 0x80000000,

            /// <summary>
            /// The window is a child window.
            /// </summary>
            Child = 0x40000000,

            /// <summary>
            /// Same numeric value as <see cref="Child"/>.
            /// </summary>
            ChildWindow = 0x40000000,

            /// <summary>
            /// The window is initially minimized.
            /// </summary>
            Minimize = 0x20000000,

            /// <summary>
            /// Same numeric value as <see cref="Minimize"/>.
            /// </summary>
            Iconic = 0x20000000,

            /// <summary>
            /// The window is initially visible.
            /// </summary>
            Visible = 0x10000000,

            /// <summary>
            /// The window is initially disabled.
            /// </summary>
            Disabled = 0x08000000,

            /// <summary>
            /// Clips child windows relative to each other while drawing within the parent window.
            /// </summary>
            ClipSiblings = 0x04000000,

            /// <summary>
            /// Excludes the area occupied by child windows when drawing occurs within the parent window.
            /// </summary>
            ClipChildren = 0x02000000,

            /// <summary>
            /// The window is initially maximized.
            /// </summary>
            Maximize = 0x01000000,

            /// <summary>
            /// The window has a thin-line border.
            /// </summary>
            Border = 0x00800000,

            /// <summary>
            /// The window has a border of a style typically used with dialog boxes.
            /// </summary>
            DlgFrame = 0x00400000,

            /// <summary>
            /// The window has a title bar (includes <see cref="Border"/> and <see cref="DlgFrame"/> styles).
            /// </summary>
            Caption = Border | DlgFrame,

            /// <summary>
            /// The window has a vertical scroll bar.
            /// </summary>
            VScroll = 0x00200000,

            /// <summary>
            /// The window has a horizontal scroll bar.
            /// </summary>
            HScroll = 0x00100000,

            /// <summary>
            /// The window has a window menu on its title bar.
            /// </summary>
            SysMenu = 0x00080000,

            /// <summary>
            /// The window has a sizing border.
            /// </summary>
            ThickFrame = 0x00040000,

            /// <summary>
            /// Same numeric value as <see cref="ThickFrame"/>.
            /// </summary>
            SizeBox = 0x00040000,

            /// <summary>
            /// Specifies the first control of a group of controls.
            /// </summary>
            Group = 0x00020000,

            /// <summary>
            /// The window has a minimize button (top-level windows only, requires <see cref="SysMenu"/>).
            /// </summary>
            MinimizeBox = 0x00020000,

            /// <summary>
            /// Specifies a control that can receive keyboard focus when the user presses TAB.
            /// </summary>
            TabStop = 0x00010000,

            /// <summary>
            /// The window has a maximize button (top-level windows only, requires <see cref="SysMenu"/>).
            /// </summary>
            MaximizeBox = 0x00010000,

            /// <summary>
            /// An overlapped window with title bar, sizing border, system menu, and min/max boxes.
            /// </summary>
            OverlappedWindow = Overlapped | Caption | SysMenu | ThickFrame | MinimizeBox | MaximizeBox,

            /// <summary>
            /// Same as <see cref="OverlappedWindow"/>.
            /// </summary>
            TiledWindow = OverlappedWindow,

            /// <summary>
            /// A pop-up window with a border, title bar, and window menu.
            /// </summary>
            PopupWindow = Popup | Border | SysMenu,

            /// <summary>
            /// Displays a drop shadow (when supported).
            /// </summary>
            DropShadow = 0x00020000,

            /// <summary>
            /// Automatically scrolls text horizontally when entering text beyond control width
            /// </summary>
            AutoScroll = 0x0080,

            /// <summary>
            /// ES_LEFT edit style - Aligns text to the left
            /// </summary>
            EditStyle_Left = 0x0000,

            /// <summary>
            /// No style bits set.
            /// </summary>
            None = 0x00000000
        }

        /// <summary>
        /// Defines Win32 extended window styles (GWL_EXSTYLE / WS_EX_*).
        /// </summary>
        [Flags]
        public enum ControlExtendedStyles : uint
        {
            /// <summary>No extended style.</summary>
            None = 0x00000000,

            /// <summary>The window has a double border.</summary>
            DlgModalFrame = 0x00000001,

            /// <summary>The window should not send WM_PARENTNOTIFY to its parent on create/destroy.</summary>
            NoParentNotify = 0x00000004,

            /// <summary>The window should be placed above all non-topmost windows.</summary>
            TopMost = 0x00000008,

            /// <summary>The window accepts drag-and-drop files.</summary>
            AcceptFiles = 0x00000010,

            /// <summary>The window should not be painted until siblings beneath it have been painted.</summary>
            Transparent = 0x00000020,

            /// <summary>The window is a child window of an MDI frame window.</summary>
            MDIChild = 0x00000040,

            /// <summary>The window is intended to be used as a floating toolbar.</summary>
            ToolWindow = 0x00000080,

            /// <summary>The window has a border with a raised edge.</summary>
            WindowEdge = 0x00000100,

            /// <summary>The window has a border with a sunken edge.</summary>
            ClientEdge = 0x00000200,

            /// <summary>The window includes a question mark in the title bar.</summary>
            ContextHelp = 0x00000400,

            /// <summary>The window has generic right-aligned properties (for RTL-mirrored windows).</summary>
            Right = 0x00001000,

            /// <summary>The window has generic left-aligned properties. Same numeric value as default.</summary>
            Left = 0x00000000,

            /// <summary>The window text is displayed using right-to-left reading order.</summary>
            RtlReading = 0x00002000,

            /// <summary>The window text is displayed using left-to-right reading order (default).</summary>
            LtrReading = 0x00000000,

            /// <summary>The vertical scroll bar (if present) is to the left of the client area.</summary>
            LeftScrollbar = 0x00004000,

            /// <summary>The vertical scroll bar (if present) is to the right of the client area (default).</summary>
            RightScrollbar = 0x00000000,

            /// <summary>The child window created with this style enables the user to navigate among children using TAB.</summary>
            ControlParent = 0x00010000,

            /// <summary>The window has a three-dimensional (static) edge border.</summary>
            StaticEdge = 0x00020000,

            /// <summary>The window is a top-level window intended to appear as a taskbar icon.</summary>
            AppWindow = 0x00040000,

            /// <summary>A window with a raised and a sunken edge, equivalent to <see cref="WindowEdge"/> | <see cref="ClientEdge"/>.</summary>
            OverlappedWindow = WindowEdge | ClientEdge,

            /// <summary>Style for a palette window, equivalent to <see cref="WindowEdge"/> | <see cref="ToolWindow"/> | <see cref="TopMost"/>.</summary>
            PaletteWindow = WindowEdge | ToolWindow | TopMost,

            /// <summary>The window is a layered window.</summary>
            Layered = 0x00080000,

            /// <summary>The window does not pass its window layout to its child windows.</summary>
            NoInheritLayout = 0x00100000,

            /// <summary>The window does not render to a redirection surface (used with flip model swap chains).</summary>
            NoRedirectionBitmap = 0x00200000,

            /// <summary>If the shell language supports reading-order alignment, the horizontal origin is on the right edge.</summary>
            LayoutRtl = 0x00400000,

            /// <summary>Paints all descendants of a window in bottom-to-top painting order using double-buffering.</summary>
            Composited = 0x02000000,

            /// <summary>A top-level window that does not become the foreground window when clicked.</summary>
            NoActivate = 0x08000000
        }

        /// <summary>
        /// Defines the types of standard Win32 and common controls supported for easy classification.
        /// </summary>
        public enum ControlType
        {
            /// <summary>Unknown or unsupported control type.</summary>
            Unknown,

            /// <summary>Static text or label control.</summary>
            Static,

            /// <summary>GroupBox control (a "Static" or "Button" with BS_GROUPBOX styling).</summary>
            GroupBox,

            /// <summary>Edit/TextBox control.</summary>
            Edit,

            /// <summary>RichEdit control (any RichEditNN class version).</summary>
            RichEdit,

            /// <summary>Standard push-button control.</summary>
            PushButton,

            /// <summary>Push-button control marked as the default button.</summary>
            DefaultPushButton,

            /// <summary>Generic Button control that did not match a more specific subtype.</summary>
            Button,

            /// <summary>Owner-drawn button control.</summary>
            OwnerDrawButton,

            /// <summary>Split-button control.</summary>
            SplitButton,

            /// <summary>Command-link button control.</summary>
            CommandLink,

            /// <summary>CheckBox control.</summary>
            CheckBox,

            /// <summary>RadioButton control.</summary>
            RadioButton,

            /// <summary>ListBox control.</summary>
            ListBox,

            /// <summary>ComboBox control.</summary>
            ComboBox,

            /// <summary>ComboBoxEx32 control.</summary>
            ComboBoxEx,

            /// <summary>ScrollBar control.</summary>
            ScrollBar,

            /// <summary>ListView control.</summary>
            ListView,

            /// <summary>TreeView control.</summary>
            TreeView,

            /// <summary>Tab control.</summary>
            TabControl,

            /// <summary>ToolBar control.</summary>
            ToolBar,

            /// <summary>StatusBar control.</summary>
            StatusBar,

            /// <summary>TrackBar (slider) control.</summary>
            TrackBar,

            /// <summary>UpDown (spinner) control.</summary>
            UpDown,

            /// <summary>ProgressBar control.</summary>
            ProgressBar,

            /// <summary>HotKey control.</summary>
            HotKey,

            /// <summary>Header control (typically used with ListView in report mode).</summary>
            Header,

            /// <summary>Animate control (AVI playback).</summary>
            Animate,

            /// <summary>DateTimePicker control.</summary>
            DateTimePicker,

            /// <summary>MonthCalendar control.</summary>
            MonthCalendar,

            /// <summary>IPAddress control.</summary>
            IPAddress,

            /// <summary>Pager control.</summary>
            Pager,

            /// <summary>SysLink (hyperlink) control.</summary>
            Link,

            /// <summary>Native Font control.</summary>
            NativeFont,

            /// <summary>Auto-Suggest Dropdown control.</summary>
            AutoSuggestDropdown,

            /// <summary>A dialog box window (#32770).</summary>
            Dialog,

            /// <summary>An MDI client window.</summary>
            MDIClient
        }

        /// <summary>
        /// Adds a specific style bit to the current window styles, skipping the native call if the style is already applied.
        /// </summary>
        /// <param name="style">The flag to enable.</param>
        public void AppendStyle(ControlStyles style)
        {
            ControlStyles current = Style;
            if ((current & style) == style) return;
            Style = current | style;
        }

        /// <summary>
        /// Removes a specific style bit from the current window styles, skipping the native call if the style is already absent.
        /// </summary>
        /// <param name="style">The flag to disable.</param>
        public void RemoveStyle(ControlStyles style)
        {
            ControlStyles current = Style;
            if ((current & style) == ControlStyles.None) return;
            Style = current & ~style;
        }

        /// <summary>
        /// Adds a specific extended style bit to the current window extended styles, skipping the native call if the style is already applied.
        /// </summary>
        /// <param name="style">The flag to enable.</param>
        public void AppendExtendedStyle(ControlExtendedStyles style)
        {
            ControlExtendedStyles current = ExtendedStyle;
            if ((current & style) == style) return;
            ExtendedStyle = current | style;
        }

        /// <summary>
        /// Removes a specific extended style bit from the current window extended styles, skipping the native call if the style is already absent.
        /// </summary>
        /// <param name="style">The flag to disable.</param>
        public void RemoveExtendedStyle(ControlExtendedStyles style)
        {
            ControlExtendedStyles current = ExtendedStyle;
            if ((current & style) == ControlExtendedStyles.None) return;
            ExtendedStyle = current & ~style;
        }

        /// <summary>
        /// Adds a specific style bit to the window styles of an arbitrary HWND, skipping the native call if the style is already applied.
        /// </summary>
        public static void AppendStyle(IntPtr hwnd, ControlStyles style)
        {
            long current = User32.GetWindowLong(hwnd, (int)NativeMethods.User32.WindowsLongs.Style);
            long updated = current | (long)style;
            if (updated == current) return;
            User32.SetWindowLong(hwnd, (int)NativeMethods.User32.WindowsLongs.Style, updated);
        }

        /// <summary>
        /// Removes a specific style bit from the window styles of an arbitrary HWND, skipping the native call if the style is already absent.
        /// </summary>
        public static void RemoveStyle(IntPtr hwnd, ControlStyles style)
        {
            long current = User32.GetWindowLong(hwnd, (int)NativeMethods.User32.WindowsLongs.Style);
            long updated = current & ~(long)style;
            if (updated == current) return;
            User32.SetWindowLong(hwnd, (int)NativeMethods.User32.WindowsLongs.Style, updated);
        }

        /// <summary>
        /// Adds a specific extended style bit to the window extended styles of an arbitrary HWND, skipping the native call if the style is already applied.
        /// </summary>
        public static void AppendExtendedStyle(IntPtr hwnd, ControlExtendedStyles extendedStyle)
        {
            long current = User32.GetWindowLong(hwnd, (int)NativeMethods.User32.WindowsLongs.ExtendedStyle);
            long updated = current | (long)extendedStyle;
            if (updated == current) return;
            User32.SetWindowLong(hwnd, (int)NativeMethods.User32.WindowsLongs.ExtendedStyle, updated);
        }

        /// <summary>
        /// Removes a specific extended style bit from the window extended styles of an arbitrary HWND, skipping the native call if the style is already absent.
        /// </summary>
        public static void RemoveExtendedStyle(IntPtr hwnd, ControlExtendedStyles extendedStyle)
        {
            long current = User32.GetWindowLong(hwnd, (int)NativeMethods.User32.WindowsLongs.ExtendedStyle);
            long updated = current & ~(long)extendedStyle;
            if (updated == current) return;
            User32.SetWindowLong(hwnd, (int)NativeMethods.User32.WindowsLongs.ExtendedStyle, updated);
        }

        /// <summary>
        /// Retrieves an enumerable list of the names of all currently applied window styles.
        /// </summary>
        /// <remarks>
        /// Zero-valued and duplicate-valued alias members (e.g. <see cref="ControlStyles.Tiled"/> vs
        /// <see cref="ControlStyles.Overlapped"/>) are only reported once.
        /// </remarks>
        public IEnumerable<string> Styles
        {
            get
            {
                ControlStyles current = Style;
                HashSet<uint> seenValues = [];

                foreach (ControlStyles style in Enum.GetValues(typeof(ControlStyles)))
                {
                    uint value = (uint)style;
                    if (value == 0 || !seenValues.Add(value)) continue;
                    if (current.HasFlag(style)) yield return style.ToString();
                }
            }
        }

        /// <summary>
        /// Retrieves an enumerable list of the names of all currently applied window extended styles.
        /// </summary>
        /// <remarks>
        /// Zero-valued and duplicate-valued alias members (e.g. <see cref="ControlExtendedStyles.Left"/> vs
        /// <see cref="ControlExtendedStyles.LtrReading"/>) are only reported once.
        /// </remarks>
        public IEnumerable<string> ExtendedStyles
        {
            get
            {
                ControlExtendedStyles current = ExtendedStyle;
                HashSet<uint> seenValues = [];

                foreach (ControlExtendedStyles style in Enum.GetValues(typeof(ControlExtendedStyles)))
                {
                    uint value = (uint)style;
                    if (value == 0 || !seenValues.Add(value)) continue;
                    if (current.HasFlag(style)) yield return style.ToString();
                }
            }
        }
    }
}