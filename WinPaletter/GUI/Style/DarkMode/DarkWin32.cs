using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using WinPaletter.NativeMethods;
using static WinPaletter.NativeMethods.User32;

namespace WinPaletter.UI.Dark
{
    public class DarkWin32 : IDisposable
    {
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
        private const int CTRL_ID_PATH_EDIT = 12290;
        private const int CTRL_ID_OK_BUTTON = 1;

        private readonly int DARK_COLOR_INT = (int)DarkColors.kPrimary.Value;
        private readonly int DARK_COLOR_SELECTION_INT = (int)DarkColors.kSeparator.Value;

        private string _acceptedPath = string.Empty;
        private IntPtr _targetDialogHwnd = IntPtr.Zero;

        private bool _disposed;
        private IntPtr _hookId = IntPtr.Zero;
        private IntPtr _cbtHookId = IntPtr.Zero;

        // Cached GDI brushes - created once on first use, deleted once in Dispose.
        private IntPtr _darkBrush = IntPtr.Zero;
        private IntPtr _selectionBrush = IntPtr.Zero;

        // Tracks every window subclassed (hwnd -> subclass id) so Dispose can remove the subclass cleanly.
        private readonly Dictionary<IntPtr, UIntPtr> _activeSubclasses = [];

        // Dedicated handler for the Windows Font Dialog (ChooseFont).
        private readonly FontDialogDarkHandler _fontDialogHandler;

        // Delegates moved inside to prevent GC collection while instance is alive.
        private readonly Comctl32.SUBCLASSPROC _dialogSubclassDelegate;
        private readonly Comctl32.SUBCLASSPROC _listViewAggressiveSubclass;
        private readonly Comctl32.SUBCLASSPROC _dropdownSubclassDelegate;
        private readonly User32.HookProc _hookDelegate;
        private readonly User32.HookProc _cbtDelegate;

        internal IntPtr DarkBrush => _darkBrush != IntPtr.Zero ? _darkBrush : (_darkBrush = GDI32.CreateSolidBrush(DARK_COLOR_INT));
        internal IntPtr SelectionBrush => _selectionBrush != IntPtr.Zero ? _selectionBrush : (_selectionBrush = GDI32.CreateSolidBrush(DARK_COLOR_SELECTION_INT));

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

            // Font dialog handler shares this instance's subclass registry and brushes.
            _fontDialogHandler = new FontDialogDarkHandler(this);

            uint threadId = Kernel32.GetCurrentThreadId();
            _hookId = User32.SetWindowsHookEx(WH_CALLWNDPROCRET, _hookDelegate, IntPtr.Zero, threadId);
            _cbtHookId = User32.SetWindowsHookEx(WH_CBT, _cbtDelegate, IntPtr.Zero, threadId);
        }

        #region Delegate Logic

        private IntPtr DialogSubclassProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData)
        {
            if (Program.Style.DarkMode)
            {
                if (uMsg == (uint)WindowsMessage.Command)
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

                if (uMsg == (uint)WindowsMessage.DrawItem && lParam != IntPtr.Zero)
                {
                    User32.DRAWITEMSTRUCT dis = Marshal.PtrToStructure<User32.DRAWITEMSTRUCT>(lParam);

                    Win32Control itemCtrl = Win32Control.FromParent(hWnd, (int)dis.CtlID);

                    if (itemCtrl?.Type == Win32Control.ControlType.ListView)
                    {
                        if ((int)dis.itemID < 0) return (IntPtr)1;
                        bool selected = (dis.itemState & ODS_SELECTED) != 0;

                        User32.FillRect(dis.hDC, ref dis.rcItem, selected ? SelectionBrush : DarkBrush);

                        IntPtr hIcon = Shell32.ExtractIcon(IntPtr.Zero, _acceptedPath, (int)dis.itemID);
                        if (hIcon != IntPtr.Zero)
                        {
                            int iconSize = 32;
                            int x = dis.rcItem.left + ((dis.rcItem.right - dis.rcItem.left) - iconSize) / 2;
                            int y = dis.rcItem.top + ((dis.rcItem.bottom - dis.rcItem.top) - iconSize) / 2;
                            User32.DrawIconEx(dis.hDC, x, y, hIcon, iconSize, iconSize, 0, IntPtr.Zero, 0x0003);
                            User32.DestroyIcon(hIcon);
                        }

                        if (selected)
                        {
                            IntPtr borderPen = GDI32.CreatePen(0, 1, DarkColors.kTextInstruct);
                            IntPtr oldPen = GDI32.SelectObject(dis.hDC, borderPen);
                            IntPtr nullBrush = GDI32.GetStockObject(GDI32.StockObjects.NULL_BRUSH);
                            IntPtr oldBrush = GDI32.SelectObject(dis.hDC, nullBrush);

                            try
                            {
                                var borderRect = dis.rcItem;
                                borderRect.left += 1;
                                borderRect.top += 1;
                                borderRect.right -= 1;
                                borderRect.bottom -= 1;
                                GDI32.Rectangle(dis.hDC, borderRect.left, borderRect.top, borderRect.right, borderRect.bottom);
                            }
                            finally
                            {
                                GDI32.SelectObject(dis.hDC, oldPen);
                                GDI32.SelectObject(dis.hDC, oldBrush);
                                GDI32.DeleteObject(borderPen);
                            }

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

        internal bool TryHandleColorMessage(uint uMsg, IntPtr wParam, out IntPtr brush)
        {
            if (uMsg >= (int)User32.WindowsMessage.CtlColorMsgBox && uMsg <= (int)User32.WindowsMessage.CtlColorStatic)
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

                if (uMsg == (uint)WindowsMessage.EraseBkgnd)
                {
                    User32.GetClientRect(hWnd, out var rect);
                    User32.FillRect(wParam, ref rect, DarkBrush);
                    return (IntPtr)1;
                }

                if (uMsg == (uint)WindowsMessage.Paint)
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
                if (uMsg == (uint)WindowsMessage.DrawItem && lParam != IntPtr.Zero)
                {
                    return DrawDropdownItem(lParam);
                }

                if (TryHandleColorMessage(uMsg, wParam, out IntPtr brush)) return brush;

                if (uMsg == (uint)WindowsMessage.EraseBkgnd)
                {
                    User32.GetClientRect(hWnd, out var rect);
                    User32.FillRect(wParam, ref rect, DarkBrush);
                    return (IntPtr)1;
                }

                if (uMsg == (uint)WindowsMessage.ShowWindow || uMsg == (uint)WindowsMessage.WindowPosChanged)
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

            bool selected = (dis.itemState & ODS_SELECTED) != 0;

            var fillRect = dis.rcItem;
            fillRect.right -= 1;
            User32.FillRect(dis.hDC, ref fillRect, selected ? SelectionBrush : DarkBrush);

            Win32Control listItem = new(dis.hwndItem);

            if (listItem.Type == Win32Control.ControlType.ListView)
            {
                const uint LVM_GETITEMTEXTW = (0x1000 + 115);
                const int MAX_TEXT_LENGTH = 256;

                IntPtr textBuffer = Marshal.AllocHGlobal(MAX_TEXT_LENGTH * 2);
                try
                {
                    for (int i = 0; i < MAX_TEXT_LENGTH * 2; i++) Marshal.WriteByte(textBuffer, i, 0);

                    User32.LVITEM lvItem = new()
                    {
                        mask = 0x0001,
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

                        string itemText = Marshal.PtrToStringUni(textBuffer);

                        if (!string.IsNullOrEmpty(itemText))
                        {
                            GDI32.SetTextColor(dis.hDC, 0xFFFFFF);
                            GDI32.SetBkMode(dis.hDC, 1);

                            IntPtr hFont = User32.SendMessage(dis.hwndItem, 0x0031, IntPtr.Zero, IntPtr.Zero);
                            IntPtr hOldFont = IntPtr.Zero;
                            if (hFont != IntPtr.Zero) hOldFont = GDI32.SelectObject(dis.hDC, hFont);

                            var rect = dis.rcItem;
                            rect.left += 6;
                            rect.right -= 7;

                            User32.DrawText(dis.hDC, itemText, -1, ref rect, GDI32.DT_LEFT | GDI32.DT_VCENTER | GDI32.DT_SINGLELINE | GDI32.DT_NOPREFIX);

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

            if ((dis.itemState & ODS_FOCUS) != 0)
            {
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

                if (msg.message == (int)User32.WindowsMessage.InitDialog)
                {
                    if (_targetDialogHwnd == IntPtr.Zero) _targetDialogHwnd = msg.hwnd;

                    if (msg.hwnd == _targetDialogHwnd)
                    {
                        IntPtr dialogHwnd = msg.hwnd;

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
                    ApplyDarkModeToAutoSuggestDropdown(wParam);
                }
                else if (_fontDialogHandler is not null && _fontDialogHandler.IsFontDialog(wParam))
                {
                    _fontDialogHandler.ApplyDarkMode(wParam);
                }
            }
            else if (nCode == HCBT_DESTROYWND)
            {
                _activeSubclasses.Remove(wParam);
            }

            return User32.CallNextHookEx(_cbtHookId, nCode, wParam, lParam);
        }

        #endregion

        #region Theming helpers

        internal void ApplyDarkModeToControl(Win32Control ctrl)
        {
            if (!Program.Style.DarkMode) return;
            if (OS.WXP || OS.WVista || OS.W7 || OS.W8x) return;
            if (ctrl is null || ctrl.Handle == IntPtr.Zero) return;

            NativeMethods.Helpers.SetHWNDDarkMode(ctrl.Handle, Program.Style.DarkMode);

            switch (ctrl.Type)
            {
                case Win32Control.ControlType.ListView:
                case Win32Control.ControlType.ListBox:
                case Win32Control.ControlType.ComboBox:
                    SubclassWindow(ctrl.Handle, _listViewAggressiveSubclass, (UIntPtr)SUBCLASS_ID_LISTVIEW);
                    UxTheme.SetWindowTheme(ctrl.Handle, "Explorer", null);
                    UxTheme.SetWindowTheme(ctrl.Handle, "DarkMode_Explorer", null);
                    ctrl.RemoveExtendedStyle(Win32Control.ControlExtendedStyles.ClientEdge);
                    break;

                default:
                    UxTheme.SetWindowTheme(ctrl.Handle, "DarkMode_Explorer", null);
                    break;
            }

            User32.RedrawWindow(ctrl.Handle, IntPtr.Zero, IntPtr.Zero, 0x0001 | 0x0004 | 0x0100);
        }

        private void ApplyDarkModeToAutoSuggestDropdown(IntPtr hWnd)
        {
            if (!Program.Style.DarkMode) return;
            if (OS.WXP || OS.WVista || OS.W7 || OS.W8x) return;
            if (hWnd == IntPtr.Zero) return;

            NativeMethods.Helpers.SetHWNDDarkMode(hWnd, true);
            UxTheme.SetWindowTheme(hWnd, "DarkMode_Explorer", null);
            SubclassWindow(hWnd, _dropdownSubclassDelegate, (UIntPtr)SUBCLASS_ID_DROPDOWN);

            User32.GetChildWindowHandles(hWnd).ForEach(childHwnd => ApplyDarkModeToControl(new Win32Control(childHwnd)));

            User32.RedrawWindow(hWnd, IntPtr.Zero, IntPtr.Zero, 0x0001 | 0x0004 | 0x0100);
        }

        /// <summary>
        /// Applies a subclass only once per hwnd and remembers it so Dispose can remove it cleanly.
        /// Used by both DarkWin32 itself and the FontDialogDarkHandler.
        /// </summary>
        internal void SubclassWindow(IntPtr hWnd, Comctl32.SUBCLASSPROC proc, UIntPtr id)
        {
            if (_activeSubclasses.ContainsKey(hWnd)) return;

            Comctl32.SetWindowSubclass(hWnd, proc, id, IntPtr.Zero);
            _activeSubclasses[hWnd] = id;
        }

        /// <summary>
        /// Removes a previously registered subclass. Called by Dispose and by handlers
        /// that want to unregister their own subclasses explicitly.
        /// </summary>
        internal void UnsubclassWindow(IntPtr hWnd, Comctl32.SUBCLASSPROC proc, UIntPtr id)
        {
            if (!_activeSubclasses.TryGetValue(hWnd, out UIntPtr existing)) return;
            if (existing != id) return;

            Comctl32.RemoveWindowSubclass(hWnd, proc, id);
            _activeSubclasses.Remove(hWnd);
        }

        internal bool IsSubclassed(IntPtr hWnd) => _activeSubclasses.ContainsKey(hWnd);

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

                // Let the font dialog handler release its own tracked subclasses first.
                _fontDialogHandler?.Dispose();

                // Remove every subclass we installed so no dangling delegate references remain.
                foreach (KeyValuePair<IntPtr, UIntPtr> entry in _activeSubclasses)
                {
                    Comctl32.SUBCLASSPROC proc = (int)entry.Value switch
                    {
                        SUBCLASS_ID_DIALOG => _dialogSubclassDelegate,
                        SUBCLASS_ID_DROPDOWN => _dropdownSubclassDelegate,
                        FontDialogDarkHandler.SUBCLASS_ID_FONTDLG => _fontDialogHandler.DialogProcDelegate,
                        FontDialogDarkHandler.SUBCLASS_ID_FONTLIST => _fontDialogHandler.ListProcDelegate,
                        FontDialogDarkHandler.SUBCLASS_ID_FONTSAMPLE => _fontDialogHandler.SampleProcDelegate,
                        FontDialogDarkHandler.SUBCLASS_ID_FONTCOMBO => _fontDialogHandler.ComboProcDelegate,
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