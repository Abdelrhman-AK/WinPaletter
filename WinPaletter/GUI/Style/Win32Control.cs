using System;
using System.Text;
using WinPaletter.NativeMethods;

namespace WinPaletter.UI
{
    public class Win32Control
    {
        public IntPtr Hwnd { get; }
        public int Id { get; }
        public string ClassName { get; }
        public ControlType Type { get; }

        public WindowStyles Style
        {
            get => (WindowStyles)User32.GetWindowLong(Hwnd, -16);
            set => ApplyStyle(-16, (uint)value);
        }

        public WindowExtendedStyles ExtendedStyle
        {
            get => (WindowExtendedStyles)User32.GetWindowLong(Hwnd, -20);
            set => ApplyStyle(-20, (uint)value);
        }

        private void ApplyStyle(int nIndex, uint value)
        {
            User32.SetWindowLong(Hwnd, nIndex, (int)value);

            // Force the non-client area (borders/frame) to recalculate
            User32.SetWindowPos(Hwnd, IntPtr.Zero, 0, 0, 0, 0, 0x0001 | 0x0002 | 0x0004 | 0x0020);
        }

        public Win32Control(IntPtr hWnd)
        {
            Hwnd = hWnd;
            Id = User32.GetDlgCtrlID(hWnd);

            StringBuilder sb = new(265);
            User32.GetClassName(hWnd, sb, sb.Capacity);
            ClassName = sb.ToString();

            Type = DetectType();
        }

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

        private ControlType ClassifyButton(uint styleMask)
        {
            return styleMask switch
            {
                0x0002 or 0x0009 => ControlType.RadioButton,
                0x0003 or 0x0005 or 0x0006 => ControlType.CheckBox,
                _ => ControlType.Button // Includes BS_GROUPBOX (0x0007) and PushButtons
            };
        }

        [Flags]
        public enum WindowStyles : uint
        {
            None = 0,
            Overlapped = 0x00000000,
            Popup = 0x80000000,
            Child = 0x40000000,
            Minimize = 0x20000000,
            Visible = 0x10000000,
            Disabled = 0x08000000,
            ClipSiblings = 0x04000000,
            ClipChildren = 0x02000000,
            Maximize = 0x01000000,
            Border = 0x00800000,
            DlgFrame = 0x00400000,
            VScroll = 0x00200000,
            HScroll = 0x00100000,
            SysMenu = 0x00080000,
            ThickFrame = 0x00040000,
            Group = 0x00020000,
            TabStop = 0x00010000,
            Caption = Border | DlgFrame
        }

        [Flags]
        public enum WindowExtendedStyles : uint
        {
            None = 0,
            DlgModalFrame = 0x00000001,
            NoParentNotify = 0x00000004,
            TopMost = 0x00000008,
            AcceptFiles = 0x00000010,
            Transparent = 0x00000020,
            MDIChild = 0x00000040,
            ToolWindow = 0x00000080,
            WindowEdge = 0x00000100,
            ClientEdge = 0x00000200,
            ContextHelp = 0x00000400,
            Right = 0x00001000,
            RTLReading = 0x00002000,
            LeftScrollbar = 0x00004000,
            ControlParent = 0x00010000,
            StaticEdge = 0x00020000,
            AppWindow = 0x00040000,
            Layered = 0x00080000
        }

        public enum ControlType
        {
            Unknown,
            ListView,
            Edit,
            ListBox,
            ComboBox,
            Button,
            CheckBox,
            RadioButton,
            Static,
            ScrollBar,
            AutoSuggestDropdown
        }
    }
}