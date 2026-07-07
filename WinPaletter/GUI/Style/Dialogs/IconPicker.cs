using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using WinPaletter.NativeMethods;
namespace WinPaletter.UI.Style
{
    public partial class Dialogs
    {
        private const int MAX_PATH = 0x00000104;
        private const int WH_CALLWNDPROCRET = 12;
        private const int WM_INITDIALOG = 0x0110;

        // Standard Win32 ListView Native Color Messages
        private const uint LVM_FIRST = 0x1000;
        private const uint LVM_SETBKCOLOR = (LVM_FIRST + 1);
        private const uint LVM_SETTEXTCOLOR = (LVM_FIRST + 3);
        private const uint LVM_SETTEXTBKCOLOR = (LVM_FIRST + 38);
        private const uint LVM_GETIMAGELIST = (LVM_FIRST + 2);
        private const int LVSIL_NORMAL = 0;
        private const uint CLR_NONE = 0xFFFFFFFF; // Forces pure background alpha transparency

        // Keep a static brush reference so we don't leak GDI handles
        private static IntPtr _darkBackgroundBrush = GDI32.CreateSolidBrush(0x1F1F1F); // Color: #1F1F1F (COLORREF format)
        private const int DARK_COLOR_INT = 0x001F1F1F; // Color: #1F1F1F (BBGGRR / COLORREF value)

        [StructLayout(LayoutKind.Sequential)]
        private struct CWPRETSTRUCT
        {
            public IntPtr lResult;
            public IntPtr lParam;
            public IntPtr wParam;
            public uint message;
            public IntPtr hwnd;
        }

        [DllImport("comctl32.dll", SetLastError = true)]
        public static extern uint ImageList_SetBkColor(IntPtr himl, uint clrBk);

        public static string PickIcon(IntPtr windowHandle, string PEfileName, int index = 0)
        {
            Program.Log?.Write(Serilog.Events.LogEventLevel.Information, "UI.Style.PickIcon query");

            StringBuilder sb = new(PEfileName, MAX_PATH);
            IntPtr hookId = IntPtr.Zero;

            // Subclass 1: Handles the general dialog shell container coloration
            Comctl32.SUBCLASSPROC dialogSubclassDelegate = (IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData) =>
            {
                if (Program.Style.DarkMode)
                {
                    if (uMsg == User32.WM_CTLCOLORDLG || uMsg == User32.WM_CTLCOLORSTATIC || uMsg == User32.WM_CTLCOLORLISTBOX || uMsg == User32.WM_CTLCOLOREDIT || uMsg == User32.WM_CTLCOLORBTN)
                    {
                        GDI32.SetTextColor(wParam, 0xFFFFFF);
                        GDI32.SetBkMode(wParam, 1); // Transparent
                        GDI32.SetBkColor(wParam, 0x1F1F1F);
                        return _darkBackgroundBrush;
                    }
                }
                return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
            };

            // Subclass 2: The absolute brute-force dark mode layer for the Icon Grid View (12297)
            Comctl32.SUBCLASSPROC listViewAggressiveSubclass = (IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData) =>
            {
                if (Program.Style.DarkMode)
                {
                    switch (uMsg)
                    {
                        case User32.WM_ERASEBKGND:
                            User32.GetClientRect(hWnd, out var rect);
                            User32.FillRect(wParam, ref rect, _darkBackgroundBrush);
                            return (IntPtr)1; // Prevent original code from clearing to white

                        case User32.WM_PAINT:
                            // 1. Force control backgrounds dark
                            User32.SendMessage(hWnd, LVM_SETBKCOLOR, IntPtr.Zero, (IntPtr)DARK_COLOR_INT);

                            // 2. CRITICAL: Force item text backgrounds to be completely transparent (CLR_NONE)
                            User32.SendMessage(hWnd, LVM_SETTEXTBKCOLOR, IntPtr.Zero, (IntPtr)CLR_NONE);
                            User32.SendMessage(hWnd, LVM_SETTEXTCOLOR, IntPtr.Zero, (IntPtr)0x00FFFFFF); // White text

                            // 3. CRITICAL: Grab the control's internal icon image list and strip its white background
                            IntPtr hImageList = User32.SendMessage(hWnd, LVM_GETIMAGELIST, (IntPtr)LVSIL_NORMAL, IntPtr.Zero);
                            if (hImageList != IntPtr.Zero)
                            {
                                ImageList_SetBkColor(hImageList, DARK_COLOR_INT);
                            }
                            break;
                    }
                }
                return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
            };

            User32.HookProc hookDelegate = (int nCode, IntPtr wParam, IntPtr lParam) =>
            {
                if (nCode >= 0 && lParam != IntPtr.Zero)
                {
                    CWPRETSTRUCT msg = Marshal.PtrToStructure<CWPRETSTRUCT>(lParam);

                    if (msg.message == WM_INITDIALOG)
                    {
                        IntPtr dialogHwnd = msg.hwnd;

                        // 1. Darken main titlebar and frame structures
                        NativeMethods.Helpers.SetHWNDDarkMode(dialogHwnd, Program.Style.DarkMode);

                        // 2. Bind the master text/label background subclass
                        Comctl32.SetWindowSubclass(dialogHwnd, dialogSubclassDelegate, (UIntPtr)1, IntPtr.Zero);

                        // 3. Enumerate layout elements
                        User32.GetChildWindowHandles(dialogHwnd).ForEach(childHwnd =>
                        {
                            int ctrlId = User32.GetDlgCtrlID(childHwnd);
                            NativeMethods.Helpers.SetHWNDDarkMode(childHwnd, Program.Style.DarkMode);

                            if (Program.Style.DarkMode)
                            {
                                if (ctrlId == 12297)
                                {
                                    // Inject aggressive rendering subclass directly to the item handler
                                    Comctl32.SetWindowSubclass(childHwnd, listViewAggressiveSubclass, (UIntPtr)2, IntPtr.Zero);

                                    // Force direct memory coloration changes via explicit Win32 window message commands
                                    User32.SendMessage(childHwnd, LVM_SETBKCOLOR, IntPtr.Zero, (IntPtr)DARK_COLOR_INT);
                                    User32.SendMessage(childHwnd, LVM_SETTEXTCOLOR, IntPtr.Zero, (IntPtr)0x00FFFFFF);
                                    User32.SendMessage(childHwnd, LVM_SETTEXTBKCOLOR, IntPtr.Zero, (IntPtr)DARK_COLOR_INT);

                                    // Also apply fallback themes to keep scrollbars darkened safely
                                    UxTheme.SetWindowTheme(childHwnd, "Explorer", null);
                                    UxTheme.SetWindowTheme(childHwnd, "DarkMode_Explorer", null);
                                }
                                else if (ctrlId == 12290)
                                {
                                    UxTheme.SetWindowTheme(childHwnd, "CFD", null);
                                }

                                // Trigger an absolute immediate paint update invalidation loop
                                User32.RedrawWindow(childHwnd, IntPtr.Zero, IntPtr.Zero, 0x0001 | 0x0004 | 0x0100);
                            }
                        });
                    }
                }
                return User32.CallNextHookEx(hookId, nCode, wParam, lParam);
            };

            try
            {
                if (Program.Style.DarkMode)
                {
                    NativeMethods.UxTheme.SetPreferredAppMode(2); // Request strict thread compliance
                }

                hookId = User32.SetWindowsHookEx(WH_CALLWNDPROCRET, hookDelegate, IntPtr.Zero, Kernel32.GetCurrentThreadId());

                int retval = Shell32.PickIconDlg(windowHandle, sb, sb.MaxCapacity, ref index);

                GC.KeepAlive(hookDelegate);
                GC.KeepAlive(dialogSubclassDelegate);
                GC.KeepAlive(listViewAggressiveSubclass);

                if (retval != 0)
                {
                    string result = Path.GetExtension(sb.ToString()).ToLower() != ".ico" ? $"{sb},{index}" : sb.ToString();
                    return result;
                }
            }
            finally
            {
                if (hookId != IntPtr.Zero)
                {
                    User32.UnhookWindowsHookEx(hookId);
                }
                if (Program.Style.DarkMode)
                {
                    NativeMethods.UxTheme.SetPreferredAppMode(0);
                }
            }

            return null;
        }
    }
}