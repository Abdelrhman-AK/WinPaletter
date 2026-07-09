using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using WinPaletter.NativeMethods;

namespace WinPaletter.UI.Dark
{
    public class DarkWin32 : IDisposable
    {
        private const int WM_INITDIALOG = 0x0110;
        private const uint LVM_FIRST = 0x1000;
        private const uint LVM_SETBKCOLOR = (LVM_FIRST + 1);
        private const uint LVM_SETTEXTCOLOR = (LVM_FIRST + 3);
        private const uint LVM_SETTEXTBKCOLOR = (LVM_FIRST + 38);
        private const int EN_CHANGE = 0x0300;

        private const int WH_CALLWNDPROCRET = 12;
        private const int WH_CBT = 5;
        private const int HCBT_CREATEWND = 3;
        private const int HCBT_DESTROYWND = 4;
        private const int HCBT_ACTIVATE = 5;

        private const uint ODS_SELECTED = 0x0001;
        private const uint ODS_FOCUS = 0x0010;

        private const int SUBCLASS_ID_DIALOG = 1;
        private const int SUBCLASS_ID_LISTVIEW = 2;
        private const int SUBCLASS_ID_DROPDOWN = 3;

        // The path Edit control and OK button inside the common file dialog.
        // (The Icon ListView no longer needs an ID constant - it's identified via Win32Control.Type below.)
        private const int CTRL_ID_PATH_EDIT = 12290;
        private const int CTRL_ID_OK_BUTTON = 1;

        private readonly int DARK_COLOR_INT = (int)DarkColors.kPrimary.Value;
        private readonly int DARK_COLOR_SELECTION_INT = (int)DarkColors.kSeparator.Value;

        // These used to be static, which meant two DarkWin32 instances (e.g. two dialogs
        // open at once) would silently overwrite each other's tracked path/dialog handle.
        private string _acceptedPath = string.Empty;
        private IntPtr _targetDialogHwnd = IntPtr.Zero;

        private bool _disposed;
        private IntPtr _hookId = IntPtr.Zero;
        private IntPtr _cbtHookId = IntPtr.Zero;

        // Cached GDI brushes - created once on first use, deleted once in Dispose,
        // instead of allocating a brand new brush on every WM_CTLCOLOR*/WM_ERASEBKGND/WM_PAINT message
        private IntPtr _darkBrush = IntPtr.Zero;
        private IntPtr _selectionBrush = IntPtr.Zero;

        // Tracks every window we've subclassed (hwnd -> subclass id) so Dispose can remove
        // the subclass cleanly instead of leaving dangling delegate references behind
        private readonly Dictionary<IntPtr, UIntPtr> _activeSubclasses = [];

        // Delegates moved inside to prevent GC collection while instance is alive
        private readonly Comctl32.SUBCLASSPROC _dialogSubclassDelegate;
        private readonly Comctl32.SUBCLASSPROC _listViewAggressiveSubclass;
        private readonly Comctl32.SUBCLASSPROC _dropdownSubclassDelegate;
        private readonly User32.HookProc _hookDelegate;
        private readonly User32.HookProc _cbtDelegate;

        private IntPtr DarkBrush => _darkBrush != IntPtr.Zero ? _darkBrush : (_darkBrush = GDI32.CreateSolidBrush(DARK_COLOR_INT));
        private IntPtr SelectionBrush => _selectionBrush != IntPtr.Zero ? _selectionBrush : (_selectionBrush = GDI32.CreateSolidBrush(DARK_COLOR_SELECTION_INT));

        public DarkWin32()
        {
            if (!Program.Style.DarkMode) return;
            if (OS.WXP || OS.WVista || OS.W7 || OS.W8x) return;

            // Initialize delegates
            _dialogSubclassDelegate = DialogSubclassProc;
            _listViewAggressiveSubclass = ListViewSubclassProc;
            _dropdownSubclassDelegate = DropdownSubclassProc;
            _hookDelegate = HookProc;
            _cbtDelegate = CbtProc;

            uint threadId = Kernel32.GetCurrentThreadId();
            _hookId = User32.SetWindowsHookEx(WH_CALLWNDPROCRET, _hookDelegate, IntPtr.Zero, threadId);
            _cbtHookId = User32.SetWindowsHookEx(WH_CBT, _cbtDelegate, IntPtr.Zero, threadId);
        }

        #region Delegate Logic

        private IntPtr DialogSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData)
        {
            if (Program.Style.DarkMode)
            {
                if (uMsg == User32.WM_COMMAND)
                {
                    long wParamLong = (long)wParam;
                    int id = (ushort)(wParamLong & 0xFFFF);
                    int code = (int)((wParamLong >> 16) & 0xFFFF);

                    Win32Control sourceCtrl = Win32Control.FromParent(hWnd, id);
                    bool isOkButton = id == CTRL_ID_OK_BUTTON;
                    bool isPathEditChanged = sourceCtrl?.Type == Win32Control.ControlType.Edit && code == EN_CHANGE;

                    if (isOkButton || isPathEditChanged)
                    {
                        IntPtr hEdit = User32.GetDlgItem(hWnd, CTRL_ID_PATH_EDIT);
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

                    Win32Control itemCtrl = Win32Control.FromParent(hWnd, (int)dis.CtlID);

                    if (itemCtrl?.Type == Win32Control.ControlType.ListView)
                    {
                        if ((int)dis.itemID < 0) return (IntPtr)1;
                        bool selected = (dis.itemState & ODS_SELECTED) != 0;

                        // Fill rect
                        User32.FillRect(dis.hDC, ref dis.rcItem, selected ? SelectionBrush : DarkBrush);

                        // Step 2: Draw the icon
                        IntPtr hIcon = Shell32.ExtractIcon(IntPtr.Zero, _acceptedPath, (int)dis.itemID);
                        if (hIcon != IntPtr.Zero)
                        {
                            int iconSize = 32;
                            int x = dis.rcItem.left + ((dis.rcItem.right - dis.rcItem.left) - iconSize) / 2;
                            int y = dis.rcItem.top + ((dis.rcItem.bottom - dis.rcItem.top) - iconSize) / 2;
                            User32.DrawIconEx(dis.hDC, x, y, hIcon, iconSize, iconSize, 0, IntPtr.Zero, 0x0003);
                            User32.DestroyIcon(hIcon);
                        }

                        // Step 3: If selected, draw a border rectangle on top
                        if ((dis.itemState & 0x0001) != 0) // ODS_SELECTED
                        {
                            // Create a border pen with DarkColors.kTextInstruct color
                            IntPtr borderPen = GDI32.CreatePen(0, 1, DarkColors.kTextInstruct);
                            IntPtr oldPen = GDI32.SelectObject(dis.hDC, borderPen);

                            // Create a hollow brush for the border (just outline)
                            IntPtr nullBrush = GDI32.GetStockObject(GDI32.StockObjects.NULL_BRUSH);
                            IntPtr oldBrush = GDI32.SelectObject(dis.hDC, nullBrush);

                            try
                            {
                                // Draw the border rectangle
                                var borderRect = dis.rcItem;
                                borderRect.left += 1;
                                borderRect.top += 1;
                                borderRect.right -= 1;
                                borderRect.bottom -= 1;

                                GDI32.Rectangle(dis.hDC, borderRect.left, borderRect.top, borderRect.right, borderRect.bottom);
                            }
                            finally
                            {
                                // Restore original objects and delete the pen
                                GDI32.SelectObject(dis.hDC, oldPen);
                                GDI32.SelectObject(dis.hDC, oldBrush);
                                GDI32.DeleteObject(borderPen);
                            }
                        }

                        // Draw focus rect
                        if (selected)
                        {
                            User32.DrawFocusRect(dis.hDC, ref dis.rcItem);
                        }

                        return (IntPtr)1;
                    }
                }

                if (TryHandleColorMessage(uMsg, wParam, out IntPtr colorBrush))
                {
                    GDI32.SetBkMode(wParam, 1);
                    return colorBrush;
                }
            }
            return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
        }

        // Shared WM_CTLCOLORMSGBOX..WM_CTLCOLORSTATIC handling used by every subclass proc
        private bool TryHandleColorMessage(uint uMsg, IntPtr wParam, out IntPtr brush)
        {
            if (uMsg >= 0x0132 && uMsg <= 0x0138)
            {
                GDI32.SetTextColor(wParam, 0xFFFFFF);
                GDI32.SetBkColor(wParam, DARK_COLOR_INT);
                brush = DarkBrush;
                return true;
            }

            brush = IntPtr.Zero;
            return false;
        }

        private IntPtr ListViewSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData)
        {
            if (Program.Style.DarkMode)
            {
                if (TryHandleColorMessage(uMsg, wParam, out IntPtr brush)) return brush;

                if (uMsg == User32.WM_ERASEBKGND)
                {
                    User32.GetClientRect(hWnd, out var rect);
                    User32.FillRect(wParam, ref rect, DarkBrush);
                    return (IntPtr)1;
                }

                if (uMsg == User32.WM_PAINT)
                {
                    User32.SendMessage(hWnd, LVM_SETBKCOLOR, IntPtr.Zero, (IntPtr)DARK_COLOR_INT);
                    User32.SendMessage(hWnd, LVM_SETTEXTBKCOLOR, IntPtr.Zero, (IntPtr)DARK_COLOR_INT);
                    User32.SendMessage(hWnd, LVM_SETTEXTCOLOR, IntPtr.Zero, (IntPtr)0x00FFFFFF);
                }
            }
            return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
        }

        private IntPtr DropdownSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData)
        {
            if (Program.Style.DarkMode)
            {
                // The suggestion list is owner-drawn (same as the Icon ListView in the dialog),
                // and WM_DRAWITEM for an owner-drawn control is sent to its immediate PARENT -
                // here, that's this dropdown window, not the listbox itself. Without taking this
                // over, item rows keep painting themselves with their original light-mode colors,
                // which is exactly why only the empty margins were turning dark before.
                if (uMsg == User32.WM_DRAWITEM && lParam != IntPtr.Zero)
                {
                    return DrawDropdownItem(lParam);
                }

                if (TryHandleColorMessage(uMsg, wParam, out IntPtr brush)) return brush;

                if (uMsg == User32.WM_ERASEBKGND)
                {
                    User32.GetClientRect(hWnd, out var rect);
                    User32.FillRect(wParam, ref rect, DarkBrush);
                    return (IntPtr)1;
                }

                // Auto-Suggest Dropdown popups are shown with SWP_NOACTIVATE so they never take focus/activation, which means HCBT_ACTIVATE never fires for them.
                // Windows reuses the same dropdown window and just shows/repositions/resizes it on every keystroke, so WM_SHOWWINDOW / WM_WINDOWPOSCHANGED are what
                // reliably fire each time - use those to re-theme whatever children now exist.
                if (uMsg == User32.WM_SHOWWINDOW || uMsg == User32.WM_WINDOWPOSCHANGED)
                {
                    User32.GetChildWindowHandles(hWnd).ForEach(childHwnd => ApplyDarkModeToControl(new Win32Control(childHwnd)));
                    User32.RedrawWindow(hWnd, IntPtr.Zero, IntPtr.Zero, 0x0001 | 0x0004 | 0x0100);
                }
            }
            return Comctl32.DefSubclassProc(hWnd, uMsg, wParam, lParam);
        }

        private IntPtr DrawDropdownItem(IntPtr lParam)
        {
            User32.DRAWITEMSTRUCT dis = Marshal.PtrToStructure<User32.DRAWITEMSTRUCT>(lParam);

            if (dis.hDC == IntPtr.Zero || dis.hwndItem == IntPtr.Zero || dis.itemID == unchecked((uint)-1)) return (IntPtr)1;

            // Fill background
            bool selected = (dis.itemState & ODS_SELECTED) != 0;

            // Create a copy of the rectangle for background filling
            var fillRect = dis.rcItem;
            // Reduce width by 1px on the right side
            fillRect.right -= 1;
            User32.FillRect(dis.hDC, ref fillRect, selected ? SelectionBrush : DarkBrush);

            Win32Control listItem = new(dis.hwndItem);

            if (listItem.Type == Win32Control.ControlType.ListView)
            {
                const uint LVM_GETITEMTEXTW = (0x1000 + 115);
                const int MAX_TEXT_LENGTH = 256;

                // Allocate buffer for text
                IntPtr textBuffer = Marshal.AllocHGlobal(MAX_TEXT_LENGTH * 2);
                try
                {
                    // Zero the buffer
                    for (int i = 0; i < MAX_TEXT_LENGTH * 2; i++) Marshal.WriteByte(textBuffer, i, 0);

                    // Prepare LVITEM structure
                    User32.LVITEM lvItem = new()
                    {
                        mask = 0x0001, // LVIF_TEXT
                        iItem = (int)dis.itemID,
                        iSubItem = 0,
                        cchTextMax = MAX_TEXT_LENGTH,
                        pszText = textBuffer
                    };

                    IntPtr ptrLvItem = Marshal.AllocHGlobal(Marshal.SizeOf(lvItem));
                    try
                    {
                        Marshal.StructureToPtr(lvItem, ptrLvItem, false);
                        User32.SendMessage(dis.hwndItem, LVM_GETITEMTEXTW, (IntPtr)dis.itemID, ptrLvItem);

                        // Get the text
                        string itemText = Marshal.PtrToStringUni(textBuffer);

                        if (!string.IsNullOrEmpty(itemText))
                        {
                            // Set text color to white
                            GDI32.SetTextColor(dis.hDC, 0xFFFFFF);
                            GDI32.SetBkMode(dis.hDC, 1);

                            // Get and select the font
                            IntPtr hFont = User32.SendMessage(dis.hwndItem, 0x0031, IntPtr.Zero, IntPtr.Zero);
                            IntPtr hOldFont = IntPtr.Zero;
                            if (hFont != IntPtr.Zero) hOldFont = GDI32.SelectObject(dis.hDC, hFont);

                            // Draw the text with padding
                            var rect = dis.rcItem;
                            rect.left += 6;
                            rect.right -= 7; // Reduced by an extra 1px to account for the selection rectangle shrink

                            User32.DrawText(dis.hDC, itemText, -1, ref rect, GDI32.DT_LEFT | GDI32.DT_VCENTER | GDI32.DT_SINGLELINE | GDI32.DT_NOPREFIX);

                            // Restore original font
                            if (hOldFont != IntPtr.Zero) GDI32.SelectObject(dis.hDC, hOldFont);
                        }
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(ptrLvItem);
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(textBuffer);
                }
            }

            // Draw focus rect if needed
            if ((dis.itemState & ODS_FOCUS) != 0)
            {
                // Also reduce the focus rect by 1px on the right
                var focusRect = dis.rcItem;
                focusRect.right -= 1;
                User32.DrawFocusRect(dis.hDC, ref focusRect);
            }

            return (IntPtr)1;
        }

        private IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && lParam != IntPtr.Zero)
            {
                User32.CWPRETSTRUCT msg = Marshal.PtrToStructure<User32.CWPRETSTRUCT>(lParam);

                if (msg.message == WM_INITDIALOG)
                {
                    if (_targetDialogHwnd == IntPtr.Zero) _targetDialogHwnd = msg.hwnd;

                    if (msg.hwnd == _targetDialogHwnd)
                    {
                        IntPtr dialogHwnd = msg.hwnd;

                        // Initialize _acceptedPath
                        IntPtr hEdit = User32.GetDlgItem(dialogHwnd, CTRL_ID_PATH_EDIT);
                        int len = User32.GetWindowTextLength(hEdit);
                        if (len > 0)
                        {
                            StringBuilder tmp = new(len + 1);
                            User32.GetWindowText(hEdit, tmp, tmp.Capacity);
                            _acceptedPath = Environment.ExpandEnvironmentVariables(tmp.ToString());
                        }

                        NativeMethods.Helpers.SetHWNDDarkMode(dialogHwnd, Program.Style.DarkMode);
                        SubclassWindow(dialogHwnd, _dialogSubclassDelegate, (UIntPtr)SUBCLASS_ID_DIALOG);

                        User32.GetChildWindowHandles(dialogHwnd).ForEach(childHwnd => ApplyDarkModeToControl(new Win32Control(childHwnd)));
                    }
                }
            }
            return User32.CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        private IntPtr CbtProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode == HCBT_CREATEWND || nCode == HCBT_ACTIVATE)
            {
                Win32Control ctrl = new(wParam);

                if (ctrl.Type == Win32Control.ControlType.AutoSuggestDropdown)
                {
                    // HCBT_CREATEWND fires once, before the dropdown's inner suggestion list exists as a child, so theming it right then often has nothing to theme yet.
                    // Windows also reuses the same dropdown window across keystrokes instead of recreating it, so HCBT_CREATEWND never fires again either. Re-applying on
                    // every HCBT_ACTIVATE (each time the dropdown shows) covers both cases.
                    ApplyDarkModeToAutoSuggestDropdown(wParam);
                }
            }
            else if (nCode == HCBT_DESTROYWND)
            {
                // Stop tracking windows once they're gone so the subclass map doesn't grow forever
                _activeSubclasses.Remove(wParam);
            }

            return User32.CallNextHookEx(_cbtHookId, nCode, wParam, lParam);
        }

        #endregion

        #region Theming helpers

        private void ApplyDarkModeToControl(Win32Control ctrl)
        {
            if (!Program.Style.DarkMode) return;
            if (OS.WXP || OS.WVista || OS.W7 || OS.W8x) return;
            if (ctrl is null || ctrl.Hwnd == IntPtr.Zero) return;

            NativeMethods.Helpers.SetHWNDDarkMode(ctrl.Hwnd, Program.Style.DarkMode);

            switch (ctrl.Type)
            {
                case Win32Control.ControlType.ListView:
                case Win32Control.ControlType.ListBox:
                case Win32Control.ControlType.ComboBox:
                    SubclassWindow(ctrl.Hwnd, _listViewAggressiveSubclass, (UIntPtr)SUBCLASS_ID_LISTVIEW);
                    UxTheme.SetWindowTheme(ctrl.Hwnd, "Explorer", null);
                    UxTheme.SetWindowTheme(ctrl.Hwnd, "DarkMode_Explorer", null);
                    ctrl.RemoveExtendedStyle(Win32Control.WindowExtendedStyles.ClientEdge);
                    break;

                default:
                    UxTheme.SetWindowTheme(ctrl.Hwnd, "DarkMode_Explorer", null);
                    break;
            }

            User32.RedrawWindow(ctrl.Hwnd, IntPtr.Zero, IntPtr.Zero, 0x0001 | 0x0004 | 0x0100);
        }

        private void ApplyDarkModeToAutoSuggestDropdown(IntPtr hWnd)
        {
            if (!Program.Style.DarkMode) return;
            if (OS.WXP || OS.WVista || OS.W7 || OS.W8x) return;
            if (hWnd == IntPtr.Zero) return;

            NativeMethods.Helpers.SetHWNDDarkMode(hWnd, true);
            UxTheme.SetWindowTheme(hWnd, "DarkMode_Explorer", null);
            SubclassWindow(hWnd, _dropdownSubclassDelegate, (UIntPtr)SUBCLASS_ID_DROPDOWN);

            // The inner suggestion list is a child of the dropdown and may not have existed yet
            // at HCBT_CREATEWND time - theme whatever children are actually present right now.
            User32.GetChildWindowHandles(hWnd).ForEach(childHwnd => ApplyDarkModeToControl(new Win32Control(childHwnd)));

            User32.RedrawWindow(hWnd, IntPtr.Zero, IntPtr.Zero, 0x0001 | 0x0004 | 0x0100);
        }

        // Applies a subclass only once per hwnd and remembers it so Dispose can remove it cleanly.
        // (SetWindowSubclass with the same id again is basically a no-op update, but tracking here
        // is what lets us call RemoveWindowSubclass on the exact same delegate+id pair later.)
        private void SubclassWindow(IntPtr hWnd, Comctl32.SUBCLASSPROC proc, UIntPtr id)
        {
            if (_activeSubclasses.ContainsKey(hWnd)) return;

            Comctl32.SetWindowSubclass(hWnd, proc, id, IntPtr.Zero);
            _activeSubclasses[hWnd] = id;
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
                if (_hookId != IntPtr.Zero)
                {
                    User32.UnhookWindowsHookEx(_hookId);
                    _hookId = IntPtr.Zero;
                }

                if (_cbtHookId != IntPtr.Zero)
                {
                    User32.UnhookWindowsHookEx(_cbtHookId);
                    _cbtHookId = IntPtr.Zero;
                }

                // Remove every subclass we installed so no dangling delegate references remain
                // on windows that could outlive this instance (e.g. shared/common dialogs)
                foreach (KeyValuePair<IntPtr, UIntPtr> entry in _activeSubclasses)
                {
                    Comctl32.SUBCLASSPROC proc = (int)entry.Value switch
                    {
                        SUBCLASS_ID_DIALOG => _dialogSubclassDelegate,
                        SUBCLASS_ID_DROPDOWN => _dropdownSubclassDelegate,
                        _ => _listViewAggressiveSubclass,
                    };
                    Comctl32.RemoveWindowSubclass(entry.Key, proc, entry.Value);
                }
                _activeSubclasses.Clear();

                if (_darkBrush != IntPtr.Zero)
                {
                    GDI32.DeleteObject(_darkBrush);
                    _darkBrush = IntPtr.Zero;
                }

                if (_selectionBrush != IntPtr.Zero)
                {
                    GDI32.DeleteObject(_selectionBrush);
                    _selectionBrush = IntPtr.Zero;
                }

                _disposed = true;
            }
        }
    }
}