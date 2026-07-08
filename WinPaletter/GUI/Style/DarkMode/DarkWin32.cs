using System;
using System.Runtime.InteropServices;
using System.Text;
using WinPaletter.NativeMethods;

namespace WinPaletter.UI.Dark
{
    public class DarkWin32 : IDisposable
    {
        private static string _acceptedPath = string.Empty;

        private const int WM_INITDIALOG = 0x0110;
        private const uint LVM_FIRST = 0x1000;
        private const uint LVM_SETBKCOLOR = (LVM_FIRST + 1);
        private const uint LVM_SETTEXTCOLOR = (LVM_FIRST + 3);
        private const uint LVM_SETTEXTBKCOLOR = (LVM_FIRST + 38);
        private int DARK_COLOR_INT = (int)DarkColors.kPrimary.Value;
        private int DARK_COLOR_SELECTION_INT = (int)DarkColors.kSeparator.Value;
        private const int EN_CHANGE = 0x0300;
        private static IntPtr targetDialogHwnd = IntPtr.Zero;
        private static IntPtr hookId = IntPtr.Zero;
        private static IntPtr cbtHookId = IntPtr.Zero;
        private const int WH_CBT = 5;
        private const int HCBT_CREATEWND = 3;

        private bool _disposed;
        private IntPtr _hookId = IntPtr.Zero;
        private IntPtr _cbtHookId = IntPtr.Zero;

        // Internal state trackers
        internal IntPtr TargetDialogHwnd = IntPtr.Zero;

        // Delegates moved inside to prevent GC collection while instance is alive
        private readonly Comctl32.SUBCLASSPROC _dialogSubclassDelegate;
        private readonly Comctl32.SUBCLASSPROC _listViewAggressiveSubclass;
        private readonly User32.HookProc _hookDelegate;
        private readonly User32.HookProc _cbtDelegate;

        public DarkWin32()
        {
            // Initialize delegates
            _dialogSubclassDelegate = DialogSubclassProc;
            _listViewAggressiveSubclass = ListViewSubclassProc;
            _hookDelegate = HookProc;
            _cbtDelegate = CbtProc;

            if (Program.Style.DarkMode)
            {
                uint threadId = Kernel32.GetCurrentThreadId();
                _hookId = User32.SetWindowsHookEx(12, _hookDelegate, IntPtr.Zero, threadId);
                _cbtHookId = User32.SetWindowsHookEx(5, _cbtDelegate, IntPtr.Zero, threadId);
            }
        }

        #region Delegate Logic

        private IntPtr DialogSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData)
        {
            if (Program.Style.DarkMode)
            {
                // Update _acceptedPath when text changes or OK is clicked
                if (uMsg == User32.WM_COMMAND)
                {
                    // Cast to long to perform bitwise shifts
                    long wParamLong = (long)wParam;
                    int id = (ushort)(wParamLong & 0xFFFF);
                    int code = (int)((wParamLong >> 16) & 0xFFFF);

                    if (id == 1 || (id == 12290 && code == EN_CHANGE))
                    {
                        IntPtr hEdit = User32.GetDlgItem(hWnd, 12290);
                        int len = User32.GetWindowTextLength(hEdit);
                        if (len > 0)
                        {
                            StringBuilder tmp = new(len + 1);
                            User32.GetWindowText(hEdit, tmp, tmp.Capacity);
                            _acceptedPath = Environment.ExpandEnvironmentVariables(tmp.ToString());
                        }
                    }
                }

                if (uMsg == User32.WM_DRAWITEM && lParam != IntPtr.Zero)
                {
                    User32.DRAWITEMSTRUCT dis = Marshal.PtrToStructure<User32.DRAWITEMSTRUCT>(lParam);

                    if (dis.CtlID == 12297) // The Icon ListView
                    {
                        if ((int)dis.itemID < 0) return (IntPtr)1;

                        // Use the fully resolved _acceptedPath
                        IntPtr hIcon = Shell32.ExtractIcon(IntPtr.Zero, _acceptedPath, (int)dis.itemID);

                        IntPtr br = GDI32.CreateSolidBrush(DARK_COLOR_INT);
                        User32.FillRect(dis.hDC, ref dis.rcItem, br);
                        GDI32.DeleteObject(br);

                        if ((dis.itemState & 0x0001) != 0)
                        {
                            IntPtr selectBrush = GDI32.CreateSolidBrush(DARK_COLOR_SELECTION_INT);
                            User32.FillRect(dis.hDC, ref dis.rcItem, selectBrush);
                            GDI32.DeleteObject(selectBrush);
                        }

                        if (hIcon != IntPtr.Zero)
                        {
                            int iconSize = 32;
                            int x = dis.rcItem.left + ((dis.rcItem.right - dis.rcItem.left) - iconSize) / 2;
                            int y = dis.rcItem.top + ((dis.rcItem.bottom - dis.rcItem.top) - iconSize) / 2;
                            User32.DrawIconEx(dis.hDC, x, y, hIcon, iconSize, iconSize, 0, IntPtr.Zero, 0x0003);
                            User32.DestroyIcon(hIcon);
                        }

                        if ((dis.itemState & 0x0008) != 0) User32.DrawFocusRect(dis.hDC, ref dis.rcItem);

                        return (IntPtr)1;
                    }
                }

                if (uMsg >= 0x0133 && uMsg <= 0x0138)
                {
                    GDI32.SetTextColor(wParam, 0xFFFFFF);
                    GDI32.SetBkMode(wParam, 1);
                    GDI32.SetBkColor(wParam, DARK_COLOR_INT);
                    return GDI32.CreateSolidBrush(DARK_COLOR_INT);
                }
            }
            return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
        }

        private IntPtr ListViewSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData)
        {
            if (Program.Style.DarkMode)
            {
                // 1. Force background and text color for all list/dropdown components
                // WM_CTLCOLORMSGBOX, WM_CTLCOLOREDIT, WM_CTLCOLORLISTBOX, etc.
                if (uMsg >= 0x0132 && uMsg <= 0x0138)
                {
                    GDI32.SetTextColor(wParam, 0xFFFFFF);                       // White Text
                    GDI32.SetBkColor(wParam, DARK_COLOR_INT);                   // Dark Background
                    return GDI32.CreateSolidBrush(DARK_COLOR_INT);              // Return the dark brush
                }

                switch (uMsg)
                {
                    case 0x0014: // WM_ERASEBKGND
                        User32.GetClientRect(hWnd, out var rect);
                        User32.FillRect(wParam, ref rect, GDI32.CreateSolidBrush(DARK_COLOR_INT));
                        return (IntPtr)1;

                    case 0x000F: // WM_PAINT
                        User32.SendMessage(hWnd, LVM_SETBKCOLOR, IntPtr.Zero, (IntPtr)DARK_COLOR_INT);
                        User32.SendMessage(hWnd, LVM_SETTEXTBKCOLOR, IntPtr.Zero, (IntPtr)DARK_COLOR_INT);
                        User32.SendMessage(hWnd, LVM_SETTEXTCOLOR, IntPtr.Zero, (IntPtr)0x00FFFFFF);
                        break;
                }
            }
            return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
        }

        private IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && lParam != IntPtr.Zero)
            {
                User32.CWPRETSTRUCT msg = Marshal.PtrToStructure<User32.CWPRETSTRUCT>(lParam);

                if (msg.message == WM_INITDIALOG)
                {
                    if (targetDialogHwnd == IntPtr.Zero) targetDialogHwnd = msg.hwnd;

                    if (msg.hwnd == targetDialogHwnd)
                    {
                        IntPtr dialogHwnd = msg.hwnd;

                        // Initialize _acceptedPath
                        IntPtr hEdit = User32.GetDlgItem(dialogHwnd, 12290);
                        int len = User32.GetWindowTextLength(hEdit);
                        if (len > 0)
                        {
                            StringBuilder tmp = new(len + 1);
                            User32.GetWindowText(hEdit, tmp, tmp.Capacity);
                            _acceptedPath = Environment.ExpandEnvironmentVariables(tmp.ToString());
                        }

                        NativeMethods.Helpers.SetHWNDDarkMode(dialogHwnd, Program.Style.DarkMode);
                        Comctl32.SetWindowSubclass(dialogHwnd, _dialogSubclassDelegate, (UIntPtr)1, IntPtr.Zero);

                        User32.GetChildWindowHandles(dialogHwnd).ForEach(childHwnd =>
                        {
                            Win32Control ctrl = new(childHwnd);
                            NativeMethods.Helpers.SetHWNDDarkMode(ctrl.Hwnd, Program.Style.DarkMode);

                            if (Program.Style.DarkMode)
                            {
                                switch (ctrl.Type)
                                {
                                    case Win32Control.ControlType.ListView:
                                    case Win32Control.ControlType.ListBox:
                                    case Win32Control.ControlType.ComboBox:
                                        Comctl32.SetWindowSubclass(ctrl.Hwnd, _listViewAggressiveSubclass, (UIntPtr)2, IntPtr.Zero);
                                        UxTheme.SetWindowTheme(ctrl.Hwnd, "Explorer", null);
                                        UxTheme.SetWindowTheme(ctrl.Hwnd, "DarkMode_Explorer", null);
                                        ctrl.ExtendedStyle &= ~Win32Control.WindowExtendedStyles.ClientEdge; // Remove 3D border for better dark mode appearance
                                        break;

                                    default:
                                        UxTheme.SetWindowTheme(ctrl.Hwnd, "DarkMode_Explorer", null);
                                        break;
                                }

                                User32.RedrawWindow(ctrl.Hwnd, IntPtr.Zero, IntPtr.Zero, 0x0001 | 0x0004 | 0x0100);
                            }
                        });
                    }
                }
            }
            return User32.CallNextHookEx(hookId, nCode, wParam, lParam);
        }

        private IntPtr CbtProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode == HCBT_CREATEWND)
            {
                IntPtr hWnd = wParam;
                Win32Control ctrl = new(hWnd);

                if (ctrl.Type == Win32Control.ControlType.AutoSuggestDropdown)
                {
                    NativeMethods.Helpers.SetHWNDDarkMode(hWnd, true);
                    UxTheme.SetWindowTheme(hWnd, "DarkMode_Explorer", null);
                }
            }
            return User32.CallNextHookEx(cbtHookId, nCode, wParam, lParam);
        }

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (_hookId != IntPtr.Zero) User32.UnhookWindowsHookEx(_hookId);
                if (_cbtHookId != IntPtr.Zero) User32.UnhookWindowsHookEx(_cbtHookId);

                _disposed = true;
            }
        }
    }
}