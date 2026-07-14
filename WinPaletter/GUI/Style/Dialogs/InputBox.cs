using Microsoft.VisualBasic;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WinPaletter.NativeMethods;
using static WinPaletter.NativeMethods.User32;

namespace WinPaletter.UI.Style
{
    /// <summary>
    /// A class that provides modern dialogs with content-adaptive layouts.
    /// </summary>
    public partial class Dialogs
    {
        /// <summary>
        /// Holds the transient state for a single native input task dialog invocation.
        /// </summary>
        private sealed class InputDialogState
        {
            public IntPtr hWnd;
            public IntPtr hEdit;
            public IntPtr ConfigPointer = IntPtr.Zero;
            public IntPtr DarkEditBrush = IntPtr.Zero;
            public string InputText;
            public string DefaultValue;
            public bool Canceled = true;
            public int EditHeight = 24;
            public IntPtr OldEditWndProc = IntPtr.Zero;
            public IntPtr OldDialogWndProc = IntPtr.Zero;

            // Kept alive as fields so the GC does not collect the delegates while
            // native code holds a pointer to them.
            public Comctl32.TaskDialogCallback Callback;
            public WNDPROC EditWndProc;
            public WNDPROC DialogWndProc;
            public IntPtr ManagedFontHandle = IntPtr.Zero;
        }

        // Delegate for window procedures
        private delegate IntPtr WNDPROC(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        public static string InputBox(string Instruction, string Value = "", string Notice = "", string Title = "")
        {
            try
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Information, "InputBox query");

                if (!OS.WXP)
                {
                    string response = InputBox_Native(Instruction, Value, Notice, Title, out bool canceled);

                    if (canceled)
                    {
                        Program.Log?.Write(Serilog.Events.LogEventLevel.Information, "InputBox > Canceled by user.");
                        return Value;
                    }

                    if (string.IsNullOrWhiteSpace(response)) response = Value;
                    return response;
                }
                else
                {
                    return InputBox_Classic(Instruction, Value, Notice, Title);
                }
            }
            catch (Exception ex)
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Warning, $"Modern InputBox failed: {ex.Message}, falling back to classic.");
                return InputBox_Classic(Instruction, Value, Notice, Title);
            }
        }

        private static string InputBox_Native(string Instruction, string Value, string Notice, string Title, out bool canceled)
        {
            InputDialogState state = new()
            {
                DefaultValue = Value ?? string.Empty,
                EditHeight = 24
            };

            state.Callback = (hwnd, uNotification, wParam, lParam, lpRefData) => InputDialogCallbackProc(state, hwnd, uNotification, wParam, lParam);

            // DIALOG HEIGHT ADJUSTMENT MECHANISM
            // We adjust the dialog height by modulating text block spacing.
            // If the Instruction is long, or Notice is missing, we balance the vertical canvas size.
            string baseContent = Dialogs_ConvertToLinkSafe(Notice);
            string dynamicPadding = string.Empty;

            bool hasInstruction = !string.IsNullOrWhiteSpace(Instruction);
            bool hasNotice = !string.IsNullOrWhiteSpace(baseContent);

            if (hasInstruction && hasNotice)
            {
                // Both fields present: Dialog naturally expands. We only need space for the textbox.
                dynamicPadding = "\r\n\r\n\r\n";
            }
            else if (hasInstruction && !hasNotice)
            {
                // Instruction only: Dialog shrinks. We inject extra spacing lines to expand the window framework height.
                dynamicPadding = "\r\n";
            }
            else if (!hasInstruction && hasNotice)
            {
                // Notice only: Standard spacing layout expansion.
                dynamicPadding = "\r\n";
            }
            else
            {
                // Both missing: Force a minimalist box height buffer zone so elements don't collapse.
                dynamicPadding = "\r\n";
            }

            string content = baseContent + dynamicPadding;

            Comctl32.TASKDIALOGCONFIG config = new()
            {
                cbSize = (uint)Marshal.SizeOf<Comctl32.TASKDIALOGCONFIG>(),
                hwndParent = IntPtr.Zero,
                hInstance = IntPtr.Zero,
                dwFlags = Comctl32.TaskDialogFlags.EnableHyperlinks | Comctl32.TaskDialogFlags.AllowDialogCancellation,
                dwCommonButtons = Comctl32.TaskDialogCommonButtonFlags.OKCancel,
                pszWindowTitle = !string.IsNullOrWhiteSpace(Title) ? Title : Application.ProductName,
                pszMainInstruction = Instruction ?? string.Empty,
                pszContent = content,
                nDefaultButton = Comctl32.IDOK,
                pfCallback = state.Callback
            };

            if (Program.Localization.Information.RightToLeft)
            {
                config.dwFlags |= Comctl32.TaskDialogFlags.RTLLayout;
            }

            int size = Marshal.SizeOf(config);
            state.ConfigPointer = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(config, state.ConfigPointer, false);

            try
            {
                int hr = Comctl32.TaskDialogIndirect(ref config, out int pnButton, out _, out _);
                canceled = state.Canceled || pnButton == Comctl32.IDCANCEL || pnButton == 0;

                if (state.hEdit != IntPtr.Zero && string.IsNullOrEmpty(state.InputText))
                {
                    StringBuilder sb = new(1024);
                    User32.GetWindowText(state.hEdit, sb, sb.Capacity);
                    state.InputText = sb.ToString();
                }

                return canceled ? state.DefaultValue : (state.InputText ?? state.DefaultValue);
            }
            finally
            {
                CleanupInputDialog(state);
            }
        }

        private static string Dialogs_ConvertToLinkSafe(string text) => string.IsNullOrEmpty(text) ? string.Empty : text;

        private static IntPtr InputDialogCallbackProc(InputDialogState state, IntPtr hwnd, uint uNotification, IntPtr wParam, IntPtr lParam)
        {
            if (uNotification == 0) // TDN_CREATED
            {
                state.hWnd = hwnd;
                ApplyDarkModeToInputDialog(state, hwnd);
                CreateInputEdit(state, hwnd);
            }
            else if (uNotification == 2) // TDN_BUTTON_CLICKED
            {
                int buttonId = (int)wParam;
                if (state.hEdit != IntPtr.Zero)
                {
                    StringBuilder sb = new(1024);
                    User32.GetWindowText(state.hEdit, sb, sb.Capacity);
                    state.InputText = sb.ToString();
                }
                state.Canceled = (buttonId == Comctl32.IDCANCEL);
            }
            else if (uNotification == 5) // TDN_DESTROYED
            {
                if (state.hEdit != IntPtr.Zero && string.IsNullOrEmpty(state.InputText))
                {
                    StringBuilder sb = new(1024);
                    User32.GetWindowText(state.hEdit, sb, sb.Capacity);
                    state.InputText = sb.ToString();
                }
            }
            return IntPtr.Zero;
        }

        private static void ApplyDarkModeToInputDialog(InputDialogState state, IntPtr hwnd)
        {
            if (!Program.Style.DarkMode) return;
            if (OS.WXP || OS.WVista || OS.W7 || OS.W8x) return;
            if (hwnd == IntPtr.Zero) return;

            NativeMethods.Helpers.SetHWNDDarkMode(hwnd, Program.Style.DarkMode);
            if (state.ConfigPointer != IntPtr.Zero) Dark.DarkDirectUI.DarkenTaskDialog(hwnd, state.ConfigPointer);
            state.DarkEditBrush = GDI32.CreateSolidBrush((int)Dark.DarkColors.kSecondary.Value);
        }

        private static IntPtr EditWndProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, InputDialogState state)
        {
            switch (uMsg)
            {
                case (uint)WindowsMessage.EraseBkgnd:
                    if (!Program.Style.DarkMode) return User32.CallWindowProc(state.OldEditWndProc, hWnd, uMsg, wParam, lParam);
                    // In dark mode, we skip default background erasing to prevent white flashing
                    return new IntPtr(1);
            }
            return User32.CallWindowProc(state.OldEditWndProc, hWnd, uMsg, wParam, lParam);
        }

        private static IntPtr DialogWndProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam, InputDialogState state)
        {
            switch (uMsg)
            {
                case (uint)WindowsMessage.CtlColorEdit:
                    // CRITICAL FIX: Only intercept colors if we are explicitly in Dark Mode
                    if (Program.Style.DarkMode && lParam == state.hEdit && state.hEdit != IntPtr.Zero && state.DarkEditBrush != IntPtr.Zero)
                    {
                        GDI32.SetTextColor(wParam, (int)Dark.DarkColors.kTextContent.Value);
                        GDI32.SetBkColor(wParam, (int)Dark.DarkColors.kFootnote.Value);
                        GDI32.SetBkMode(wParam, GDI32.OPAQUE); // Use OPAQUE to fill the text background cleanly
                        return state.DarkEditBrush;
                    }
                    break; // Let default system processing handle it for Light Mode

                case (uint)WindowsMessage.Paint:
                case (uint)WindowsMessage.Size:
                case (uint)WindowsMessage.WindowPosChanged:
                    IntPtr res = User32.CallWindowProc(state.OldDialogWndProc, hWnd, uMsg, wParam, lParam);
                    if (state.hEdit != IntPtr.Zero && User32.IsWindow(state.hEdit))
                    {
                        User32.InvalidateRect(state.hEdit, IntPtr.Zero, true);
                    }
                    return res;
            }
            return User32.CallWindowProc(state.OldDialogWndProc, hWnd, uMsg, wParam, lParam);
        }

        private static void CreateInputEdit(InputDialogState state, IntPtr hwnd)
        {
            try
            {
                if (!User32.IsWindow(hwnd)) return;

                state.DialogWndProc = (h, msg, wParam, lParam) => DialogWndProc(h, msg, wParam, lParam, state);
                state.OldDialogWndProc = User32.SetWindowLongPtr(hwnd, (int)NativeMethods.User32.WindowsLongs.WndProc, Marshal.GetFunctionPointerForDelegate(state.DialogWndProc));

                int parentStyle = (int)User32.GetWindowLong(hwnd, -16);
                User32.SetWindowLong(hwnd, -16, parentStyle | (int)Win32Control.ControlStyles.ClipChildren);

                IntPtr hContent = User32.GetDlgItem(hwnd, Comctl32.TDLG_CONTENTPANE);
                bool workingWithContentPane = hContent != IntPtr.Zero && User32.IsWindow(hContent);

                if (!workingWithContentPane)
                {
                    hContent = User32.GetDlgItem(hwnd, NativeMethods.Comctl32.TDLG_MAININSTRUCTION);
                }

                int x = 0;
                int y = 0;
                int width = 0;
                int height = state.EditHeight;
                int padding = 5;

                User32.GetClientRect(hwnd, out UxTheme.RECT clientRect);

                if (hContent != IntPtr.Zero && User32.IsWindow(hContent))
                {
                    User32.GetWindowRect(hContent, out UxTheme.RECT contentRect);

                    User32.POINT topLeft = new() { X = contentRect.left, Y = contentRect.top };
                    User32.POINT bottomRight = new() { X = contentRect.right, Y = contentRect.bottom };

                    User32.MapWindowPoints(IntPtr.Zero, hwnd, ref topLeft, 1);
                    User32.MapWindowPoints(IntPtr.Zero, hwnd, ref bottomRight, 1);

                    x = topLeft.X + padding;
                    width = (bottomRight.X - topLeft.X) - (padding * 2);

                    // Dynamic Height Calculation logic:
                    // If TDLG_CONTENTPANE exists, target its bottom boundary.
                    // If missing and relying on TDLG_MAININSTRUCTION, calculate further offset downward to dynamically account for layout structures safely.
                    if (workingWithContentPane)
                    {
                        y = bottomRight.Y - height - 6;
                    }
                    else
                    {
                        y = bottomRight.Y + 10;
                    }

                    if (y + height > clientRect.bottom - 45 || y < 0 || width < 100)
                    {
                        hContent = IntPtr.Zero; // Reset layout fallback marker
                    }
                }

                // Fallback Layout: Calculates absolute context measurements using structural window client height
                if (hContent == IntPtr.Zero)
                {
                    x = clientRect.left + padding + 10;
                    y = clientRect.bottom - height - 57;
                    width = (clientRect.right - clientRect.left) - (x * 2);
                }

                if (width < 200) width = 200;

                int style = (int)Win32Control.ControlStyles.Child | (int)Win32Control.ControlStyles.Visible | (int)Win32Control.ControlStyles.TabStop | (int)Win32Control.ControlStyles.AutoScroll | (int)Win32Control.ControlStyles.EditStyle_Left;

                state.hEdit = User32.CreateWindowEx((int)Win32Control.ControlExtendedStyles.ClientEdge, "EDIT", state.DefaultValue ?? string.Empty, style, x, y, width, height, hwnd, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
                if (state.hEdit == IntPtr.Zero) return;

                User32.SetWindowPos(state.hEdit, IntPtr.Zero, 0, 0, 0, 0, User32.SetWindowsPosition.NoSize | User32.SetWindowsPosition.NoMove | User32.SetWindowsPosition.ShowWindow);

                IntPtr hFont = IntPtr.Zero;
                try
                {
                    if (System.Drawing.SystemFonts.MessageBoxFont != null)
                    {
                        hFont = System.Drawing.SystemFonts.MessageBoxFont.ToHfont();
                    }
                }
                catch
                {
                    hFont = User32.SendMessage(hwnd, (uint)WindowsMessage.GetFont, IntPtr.Zero, IntPtr.Zero);
                    if (hFont == IntPtr.Zero) hFont = NativeMethods.GDI32.GetStockObject(GDI32.StockObjects.DEFAULT_GUI_FONT);
                }

                if (hFont != IntPtr.Zero)
                {
                    User32.SendMessage(state.hEdit, (uint)WindowsMessage.SetFont, hFont, new IntPtr(1));
                    state.ManagedFontHandle = hFont;
                }

                if (Program.Style.DarkMode)
                {
                    UxTheme.SetWindowTheme(state.hEdit, "DarkMode_CFD", null);
                }
                else
                {
                    // Revert to default native msstyles (restores the modern blue-focus text box)
                    UxTheme.SetWindowTheme(state.hEdit, null, null);
                }

                User32.SetWindowPos(state.hEdit, IntPtr.Zero, 0, 0, 0, 0, User32.SetWindowsPosition.NoSize | User32.SetWindowsPosition.NoMove | User32.SetWindowsPosition.NoZOrder | User32.SetWindowsPosition.FrameChanged);

                state.EditWndProc = (h, msg, wParam, lParam) => EditWndProc(h, msg, wParam, lParam, state);
                state.OldEditWndProc = User32.SetWindowLongPtr(state.hEdit, (int)NativeMethods.User32.WindowsLongs.WndProc, Marshal.GetFunctionPointerForDelegate(state.EditWndProc));

                User32.ShowWindow(state.hEdit, User32.SW_SHOW);
                User32.SetFocus(state.hEdit);
                User32.SendMessage(state.hEdit, User32.EM_SETSEL, IntPtr.Zero, new IntPtr(-1));
                User32.InvalidateRect(state.hEdit, IntPtr.Zero, true);
                User32.UpdateWindow(state.hEdit);
            }
            catch (Exception ex)
            {
                Program.Log?.Write(Serilog.Events.LogEventLevel.Error, $"Error creating edit control: {ex.Message}");
            }
        }

        private static void CleanupInputDialog(InputDialogState state)
        {
            if (state == null) return;
            try
            {
                if (state.hEdit != IntPtr.Zero && state.OldEditWndProc != IntPtr.Zero) User32.SetWindowLongPtr(state.hEdit, (int)NativeMethods.User32.WindowsLongs.WndProc, state.OldEditWndProc);
                if (state.hWnd != IntPtr.Zero && state.OldDialogWndProc != IntPtr.Zero) User32.SetWindowLongPtr(state.hWnd, (int)NativeMethods.User32.WindowsLongs.WndProc, state.OldDialogWndProc);
            }
            catch { }

            if (state.DarkEditBrush != IntPtr.Zero) { NativeMethods.GDI32.DeleteObject(state.DarkEditBrush); state.DarkEditBrush = IntPtr.Zero; }
            if (state.ConfigPointer != IntPtr.Zero) { Marshal.FreeHGlobal(state.ConfigPointer); state.ConfigPointer = IntPtr.Zero; }
            if (state.ManagedFontHandle != IntPtr.Zero) { NativeMethods.GDI32.DeleteObject(state.ManagedFontHandle); state.ManagedFontHandle = IntPtr.Zero; }
            state.hEdit = IntPtr.Zero;
            state.hWnd = IntPtr.Zero;
        }

        private static string InputBox_Classic(string Instruction, string Value = "", string Notice = "", string Title = "")
        {
            string N = !string.IsNullOrWhiteSpace(Notice) ? $"\r\n\r\n{Notice}" : string.Empty;
            string T = !string.IsNullOrWhiteSpace(Title) ? Title : Application.ProductName;
            return Interaction.InputBox($"{Instruction}{N}", T, Value);
        }
    }
}