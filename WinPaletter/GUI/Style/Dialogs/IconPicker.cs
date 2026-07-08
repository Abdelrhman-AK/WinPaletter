using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using WinPaletter.NativeMethods;

namespace WinPaletter.UI.Style
{
    public partial class Dialogs
    {
        private static string _acceptedPath = string.Empty;
        private const int MAX_PATH = 0x00000104;
        private const int WH_CALLWNDPROCRET = 12;
        private const int WM_INITDIALOG = 0x0110;
        private const uint LVM_FIRST = 0x1000;
        private const uint LVM_SETBKCOLOR = (LVM_FIRST + 1);
        private const uint LVM_SETTEXTCOLOR = (LVM_FIRST + 3);
        private const uint LVM_SETTEXTBKCOLOR = (LVM_FIRST + 38);
        private static IntPtr _darkBackgroundBrush = GDI32.CreateSolidBrush(0x1F1F1F);
        private const int DARK_COLOR_INT = 0x001F1F1F;
        private const int EN_CHANGE = 0x0300;
        private static IntPtr targetDialogHwnd = IntPtr.Zero;
        private static IntPtr hookId = IntPtr.Zero;
        private static IntPtr cbtHookId = IntPtr.Zero;
        private const int WH_CBT = 5;
        private const int HCBT_CREATEWND = 3;

        private static Comctl32.SUBCLASSPROC dialogSubclassDelegate = (IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData) =>
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

                        User32.FillRect(dis.hDC, ref dis.rcItem, _darkBackgroundBrush);

                        if ((dis.itemState & 0x0001) != 0)
                        {
                            IntPtr selectBrush = GDI32.CreateSolidBrush(0x003F3F3F);
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
                    return _darkBackgroundBrush;
                }
            }
            return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
        };

        private static Comctl32.SUBCLASSPROC listViewAggressiveSubclass = (IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData) =>
        {
            if (Program.Style.DarkMode)
            {
                // 1. Force background and text color for all list/dropdown components
                // WM_CTLCOLORMSGBOX, WM_CTLCOLOREDIT, WM_CTLCOLORLISTBOX, etc.
                if (uMsg >= 0x0132 && uMsg <= 0x0138)
                {
                    GDI32.SetTextColor(wParam, 0xFFFFFF);      // White Text
                    GDI32.SetBkColor(wParam, DARK_COLOR_INT); // Dark Background
                    return _darkBackgroundBrush;              // Return the dark brush
                }

                switch (uMsg)
                {
                    case 0x0014: // WM_ERASEBKGND
                        User32.GetClientRect(hWnd, out var rect);
                        User32.FillRect(wParam, ref rect, _darkBackgroundBrush);
                        return (IntPtr)1;

                    case 0x000F: // WM_PAINT
                        User32.SendMessage(hWnd, LVM_SETBKCOLOR, IntPtr.Zero, (IntPtr)DARK_COLOR_INT);
                        User32.SendMessage(hWnd, LVM_SETTEXTBKCOLOR, IntPtr.Zero, (IntPtr)DARK_COLOR_INT);
                        User32.SendMessage(hWnd, LVM_SETTEXTCOLOR, IntPtr.Zero, (IntPtr)0x00FFFFFF);
                        break;
                }
            }
            return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
        };

        private static User32.HookProc cbtDelegate = (int nCode, IntPtr wParam, IntPtr lParam) =>
        {
            if (nCode == HCBT_CREATEWND)
            {
                IntPtr hWnd = wParam;
                StringBuilder sb = new(265);
                User32.GetClassName(hWnd, sb, sb.Capacity);
                string className = sb.ToString();

                if (className.Equals("Auto-Suggest Dropdown", StringComparison.OrdinalIgnoreCase))
                {
                    // Ensure we are targeting the actual window handle, not a sub-part
                    IntPtr rootHwnd = User32.GetAncestor(hWnd, 2); // GA_ROOT = 2

                    NativeMethods.Helpers.SetHWNDDarkMode(rootHwnd, true);
                    UxTheme.SetWindowTheme(rootHwnd, "DarkMode_Explorer", null);

                    Comctl32.SetWindowSubclass(rootHwnd, listViewAggressiveSubclass, (UIntPtr)4, IntPtr.Zero);
                }
            }
            return User32.CallNextHookEx(cbtHookId, nCode, wParam, lParam);
        };

        private static User32.HookProc hookDelegate = (int nCode, IntPtr wParam, IntPtr lParam) =>
        {
            if (nCode >= 0 && lParam != IntPtr.Zero)
            {
                User32.CWPRETSTRUCT msg = Marshal.PtrToStructure<User32.CWPRETSTRUCT>(lParam);

                if (msg.message == WM_INITDIALOG)
                {
                    if (targetDialogHwnd == IntPtr.Zero)
                    {
                        targetDialogHwnd = msg.hwnd;
                    }

                    if (msg.hwnd == targetDialogHwnd)
                    {
                        IntPtr dialogHwnd = msg.hwnd;

                        NativeMethods.Helpers.SetHWNDDarkMode(dialogHwnd, Program.Style.DarkMode);
                        Comctl32.SetWindowSubclass(dialogHwnd, dialogSubclassDelegate, (UIntPtr)1, IntPtr.Zero);

                        User32.GetChildWindowHandles(dialogHwnd).ForEach(childHwnd =>
                        {
                            int ctrlId = User32.GetDlgCtrlID(childHwnd);
                            StringBuilder sb = new(265);
                            User32.GetClassName(childHwnd, sb, sb.Capacity);
                            string className = sb.ToString();

                            NativeMethods.Helpers.SetHWNDDarkMode(childHwnd, Program.Style.DarkMode);

                            if (Program.Style.DarkMode)
                            {
                                if (ctrlId == 12297 || className.Equals("ListBox", StringComparison.OrdinalIgnoreCase))
                                {
                                    Comctl32.SetWindowSubclass(childHwnd, listViewAggressiveSubclass, (UIntPtr)2, IntPtr.Zero);

                                    UxTheme.SetWindowTheme(childHwnd, "Explorer", null);
                                    UxTheme.SetWindowTheme(childHwnd, "DarkMode_Explorer", null);
                                }
                                else if (ctrlId == 12290 || className.Equals("Edit", StringComparison.OrdinalIgnoreCase))
                                {
                                    UxTheme.SetWindowTheme(childHwnd, "DarkMode_Explorer", null);
                                }

                                User32.RedrawWindow(childHwnd, IntPtr.Zero, IntPtr.Zero, 0x0001 | 0x0004 | 0x0100);
                            }
                        });
                    }
                }
            }
            return User32.CallNextHookEx(hookId, nCode, wParam, lParam);
        };
        public static string PickIcon(IntPtr windowHandle, string PEfileName, int index = 0)
        {
            _acceptedPath = Environment.ExpandEnvironmentVariables(PEfileName);
            StringBuilder sb = new(_acceptedPath, MAX_PATH);

            try
            {
                int retval;

                if (Program.Style.DarkMode)
                {
                    hookId = User32.SetWindowsHookEx(WH_CALLWNDPROCRET, hookDelegate, IntPtr.Zero, Kernel32.GetCurrentThreadId());
                    cbtHookId = User32.SetWindowsHookEx(WH_CBT, cbtDelegate, IntPtr.Zero, Kernel32.GetCurrentThreadId());
                    retval = Shell32.PickIconDlg(windowHandle, sb, sb.MaxCapacity, ref index);

                    GC.KeepAlive(hookDelegate);
                    GC.KeepAlive(cbtDelegate);
                    GC.KeepAlive(dialogSubclassDelegate);
                    GC.KeepAlive(listViewAggressiveSubclass);
                }
                else // No dark mode manipulation needed, just call the dialog directly
                {
                    retval = Shell32.PickIconDlg(windowHandle, sb, sb.MaxCapacity, ref index);
                }

                if (retval != 0)
                {
                    return Path.GetExtension(sb.ToString()).ToLower() != ".ico" ? $"{sb},{index}" : sb.ToString();
                }
            }
            finally
            {
                if (hookId != IntPtr.Zero) User32.UnhookWindowsHookEx(hookId);
            }

            return null;
        }
    }
}