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
        public IntPtr Hwnd { get; }

        /// <summary>
        /// The unique numeric identifier (ID) assigned to the control within its parent dialog.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The registered Windows class name (e.g., "Button", "SysListView32", "Edit").
        /// </summary>
        public string ClassName { get; }

        /// <summary>
        /// The identified <see cref="ControlType"/>, determined based on the class name and specific styles.
        /// </summary>
        public ControlType Type { get; }

        /// <summary>
        /// Gets or sets the bitmask representing the current standard window styles.
        /// </summary>
        /// <remarks>
        /// Setting this property automatically calls <c>SetWindowPos</c> to force the 
        /// OS to redraw the window frame and reflect the style changes.
        /// </remarks>
        public WindowStyles Style
        {
            get => (WindowStyles)User32.GetWindowLong(Hwnd, -16);
            set => ApplyStyle(-16, (uint)value);
        }

        /// <summary>
        /// Gets or sets the bitmask representing the current extended window styles.
        /// </summary>
        /// <remarks>
        /// Setting this property automatically calls <c>SetWindowPos</c> to force the 
        /// OS to redraw the window frame and reflect the style changes.
        /// </remarks>
        public WindowExtendedStyles ExtendedStyle
        {
            get => (WindowExtendedStyles)User32.GetWindowLong(Hwnd, -20);
            set => ApplyStyle(-20, (uint)value);
        }

        /// <summary>
        /// Retrieves a list of all child controls (if any) contained within this window.
        /// </summary>
        public List<Win32Control> Controls
        {
            get
            {
                List<Win32Control> controls = [];
                User32.EnumChildWindows(Hwnd, (childHwnd, lParam) =>
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
                        var control = new Win32Control(childHwnd);
                        controls.Add(control);
                        EnumerateChildren(childHwnd); // Recursively enumerate children
                        return true; // Continue enumeration
                    }, IntPtr.Zero);
                }
                EnumerateChildren(Hwnd);
                return controls;
            }
        }

        /// <summary>
        /// Updates the internal window style or extended style and triggers a frame recalculation.
        /// </summary>
        /// <param name="nIndex">The index of the window data to set (-16 for GWL_STYLE, -20 for GWL_EXSTYLE).</param>
        /// <param name="value">The new bitwise combination of flags.</param>
        private void ApplyStyle(int nIndex, uint value)
        {
            User32.SetWindowLong(Hwnd, nIndex, (int)value);

            // Force the non-client area (borders/frame) to recalculate
            // SWP_FRAMECHANGED (0x0020) is required for style changes to take visual effect
            User32.SetWindowPos(Hwnd, IntPtr.Zero, 0, 0, 0, 0, 0x0001 | 0x0002 | 0x0004 | 0x0020);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Win32Control"/> class.
        /// </summary>
        /// <param name="hWnd">The target native window handle.</param>
        public Win32Control(IntPtr hWnd)
        {
            Hwnd = hWnd;
            Id = User32.GetDlgCtrlID(hWnd);

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
            // 1. Identify by ClassName or ID
            if (ClassName.Contains("SysListView32", StringComparison.OrdinalIgnoreCase) || Id == 12297) return ControlType.ListView;
            if (ClassName.Equals("Edit", StringComparison.OrdinalIgnoreCase) || Id == 12290) return ControlType.Edit;
            if (ClassName.Equals("ListBox", StringComparison.OrdinalIgnoreCase)) return ControlType.ListBox;
            if (ClassName.Contains("ComboBox", StringComparison.OrdinalIgnoreCase)) return ControlType.ComboBox;
            if (ClassName.Equals("ScrollBar", StringComparison.OrdinalIgnoreCase)) return ControlType.ScrollBar;
            if (ClassName.Equals("Auto-Suggest Dropdown", StringComparison.OrdinalIgnoreCase)) return ControlType.AutoSuggestDropdown;

            // 2. Handle specialized Static/Button classifications
            return ClassName switch
            {
                "Static" => ((uint)Style & 0x000F) == 0x0007 ? ControlType.Button : ControlType.Static,
                "Button" => ClassifyButton((uint)Style & 0x000F),
                _ => ControlType.Unknown
            };
        }

        /// <summary>
        /// Refines the control type for Windows "Button" classes by examining the specific style bits.
        /// </summary>
        /// <param name="styleMask">The lower 4 bits of the window style flags.</param>
        /// <returns>The refined <see cref="ControlType"/> (e.g., RadioButton, CheckBox).</returns>
        private ControlType ClassifyButton(uint styleMask)
        {
            return styleMask switch
            {
                0x0002 or 0x0009 => ControlType.RadioButton,
                0x0003 or 0x0005 or 0x0006 => ControlType.CheckBox,
                _ => ControlType.Button // Includes BS_GROUPBOX (0x0007) and PushButtons
            };
        }

        /// <summary>
        /// Defines standard Win32 window styles.
        /// </summary>
        [Flags]
        public enum WindowStyles : uint
        {
            /// <summary>
            /// No style.
            /// </summary>
            None = 0,
            /// <summary>
            /// The window is an overlapped window.
            /// </summary>
            Overlapped = 0x00000000,
            /// <summary>
            /// The window is a pop-up window.
            /// </summary>
            Popup = 0x80000000,
            /// <summary>
            /// The window is a child window.
            /// </summary>
            Child = 0x40000000,
            /// <summary>
            /// The window is initially minimized.
            /// </summary>
            Minimize = 0x20000000,
            /// <summary>
            /// The window is initially visible.
            /// </summary>
            Visible = 0x10000000,
            /// <summary>
            /// The window is initially disabled.
            /// </summary>
            Disabled = 0x08000000,
            /// <summary>
            /// Excludes the area occupied by child windows when drawing occurs within the parent window.
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
            /// Specifies the first control of a group of controls.
            /// </summary>
            Group = 0x00020000,
            /// <summary>
            /// Specifies a control that can receive the keyboard focus when the user presses the TAB key.
            /// </summary>
            TabStop = 0x00010000,
            /// <summary>
            /// The window has a title bar (includes Border and DlgFrame styles).
            /// </summary>
            Caption = Border | DlgFrame
        }

        /// <summary>
        /// Defines Win32 extended window styles.
        /// </summary>
        [Flags]
        public enum WindowExtendedStyles : uint
        {
            /// <summary>
            /// No extended style.
            /// </summary>
            None = 0,
            /// <summary>
            /// The window has a double border.
            /// </summary>
            DlgModalFrame = 0x00000001,
            /// <summary>
            /// The window should not send WM_PARENTNOTIFY messages to its parent window when it is created or destroyed.
            /// </summary>
            NoParentNotify = 0x00000004,
            /// <summary>
            /// The window should be placed above all non-topmost windows.
            /// </summary>
            TopMost = 0x00000008,
            /// <summary>
            /// The window accepts drag-and-drop files.
            /// </summary>
            AcceptFiles = 0x00000010,
            /// <summary>
            /// The window should not be painted until siblings beneath it have been painted.
            /// </summary>
            Transparent = 0x00000020,
            /// <summary>
            /// The window is a child window of an MDI frame window.
            /// </summary>
            MDIChild = 0x00000040,
            /// <summary>
            /// The window is intended to be used as a floating toolbar.
            /// </summary>
            ToolWindow = 0x00000080,
            /// <summary>
            /// The window has a border with a raised edge.
            /// </summary>
            WindowEdge = 0x00000100,
            /// <summary>
            /// The window has a border with a sunken edge.
            /// </summary>
            ClientEdge = 0x00000200,
            /// <summary>
            /// The window includes a question mark in the title bar.
            /// </summary>
            ContextHelp = 0x00000400,
            /// <summary>
            /// The window has generic "right-aligned" properties.
            /// </summary>
            Right = 0x00001000,
            /// <summary>
            /// The window text is displayed using right-to-left reading order properties.
            /// </summary>
            RTLReading = 0x00002000,
            /// <summary>
            /// If the shell language is Hebrew, Arabic, or another language that supports reading-order alignment, the vertical scroll bar (if present) is to the left of the client area.
            /// </summary>
            LeftScrollbar = 0x00004000,
            /// <summary>
            /// The window is a child window that takes input focus (e.g., handles dialog navigation).
            /// </summary>
            ControlParent = 0x00010000,
            /// <summary>
            /// The window has a three-dimensional border style.
            /// </summary>
            StaticEdge = 0x00020000,
            /// <summary>
            /// The window is a top-level window intended to be a taskbar icon.
            /// </summary>
            AppWindow = 0x00040000,
            /// <summary>
            /// The window is a layered window.
            /// </summary>
            Layered = 0x00080000
        }

        /// <summary>
        /// Defines the types of standard Win32 controls supported for easy classification.
        /// </summary>
        public enum ControlType
        {
            /// <summary>
            /// Unknown or unsupported control type.
            /// </summary>
            Unknown,
            /// <summary>
            /// ListView control.
            /// </summary>
            ListView,
            /// <summary>
            /// Edit/TextBox control.
            /// </summary>
            Edit,
            /// <summary>
            /// ListBox control.
            /// </summary>
            ListBox,
            /// <summary>
            /// ComboBox control.
            /// </summary>
            ComboBox,
            /// <summary>
            /// Standard Button control.
            /// </summary>
            Button,
            /// <summary>
            /// CheckBox control.
            /// </summary>
            CheckBox,
            /// <summary>
            /// RadioButton control.
            /// </summary>
            RadioButton,
            /// <summary>
            /// Static text or label control.
            /// </summary>
            Static,
            /// <summary>
            /// ScrollBar control.
            /// </summary>
            ScrollBar,
            /// <summary>
            /// Auto-Suggest Dropdown control.
            /// </summary>
            AutoSuggestDropdown
        }

        /// <summary>
        /// Adds a specific style bit to the current window styles.
        /// </summary>
        /// <param name="style">The flag to enable.</param>
        public void AppendStyle(WindowStyles style)
        {
            Style |= style;
        }

        /// <summary>
        /// Removes a specific style bit from the current window styles.
        /// </summary>
        /// <param name="style">The flag to disable.</param>
        public void RemoveStyle(WindowStyles style)
        {
            Style &= ~style;
        }

        /// <summary>
        /// Adds a specific extended style bit to the current window extended styles.
        /// </summary>
        /// <param name="style">The flag to enable.</param>
        public void AppendExtendedStyle(WindowExtendedStyles style)
        {
            ExtendedStyle |= style;
        }

        /// <summary>
        /// Removes a specific extended style bit from the current window extended styles.
        /// </summary>
        /// <param name="style">The flag to disable.</param>
        public void RemoveExtendedStyle(WindowExtendedStyles style)
        {
            ExtendedStyle &= ~style;
        }

        /// <summary>
        /// Retrieves an enumerable list of the names of all currently applied window styles.
        /// </summary>
        public IEnumerable<string> Styles
        {
            get
            {
                foreach (WindowStyles style in Enum.GetValues(typeof(WindowStyles)))
                {
                    if (style != WindowStyles.None && Style.HasFlag(style))
                    {
                        yield return style.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves an enumerable list of the names of all currently applied window extended styles.
        /// </summary>
        public IEnumerable<string> ExtendedStyles
        {
            get
            {
                foreach (WindowExtendedStyles style in Enum.GetValues(typeof(WindowExtendedStyles)))
                {
                    if (style != WindowExtendedStyles.None && ExtendedStyle.HasFlag(style))
                    {
                        yield return style.ToString();
                    }
                }
            }
        }
    }
}