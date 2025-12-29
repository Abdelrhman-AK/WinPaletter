using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using static WinPaletter.NativeMethods.GDI32;
using static WinPaletter.NativeMethods.UxTheme;

namespace WinPaletter.NativeMethods
{
    /// <summary>
    /// Provides a managed interface to various Windows User32.dll functions and constants.
    /// </summary>
    /// <remarks>The <see cref="User32"/> class contains static methods, constants, and structures that allow managed
    /// code to interact with the Windows User32 API. These include functions for window management, cursor handling, system
    /// metrics retrieval, and more. Many of these methods are wrappers around native Windows API functions and require
    /// careful usage to ensure proper resource management and error handling. <para> Note that some methods in this class
    /// are marked as <c>internal</c> and are not intended for public use. </para></remarks>
    public partial class User32
    {
        /// <summary>
        /// Set position of a window in Z order
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hWndInsertAfter"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="uFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        /// <summary>
        /// Retrieves the specified system metric or system configuration setting.
        /// </summary>
        /// <remarks>This method is a wrapper for the Windows API function <c>GetSystemMetrics</c> in the User32.dll
        /// library.  It allows managed code to access system metrics and configuration settings provided by the operating
        /// system.</remarks>
        /// <param name="nIndex">The system metric or configuration setting to retrieve. This parameter must be a valid system metric index. For
        /// example, use 0 to retrieve the screen width in pixels, or 1 to retrieve the screen height in pixels. Refer to the
        /// Windows API documentation for a complete list of valid indices.</param>
        /// <returns>The value of the specified system metric or configuration setting. The meaning of the return value depends on the
        /// <paramref name="nIndex"/> provided.</returns>
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);

        /// <summary>
        /// Loads the specified cursor resource from the application's executable file or a system-defined cursor.
        /// </summary>
        /// <remarks>This method is a wrapper for the Windows API function <c>LoadCursor</c>. When using system-defined
        /// cursors, set <paramref name="hInstance"/> to 0 and provide a predefined cursor identifier for <paramref
        /// name="lpCursorName"/>.</remarks>
        /// <param name="hInstance">A handle to the instance of the module containing the cursor resource. Pass 0 to load a system-defined cursor.</param>
        /// <param name="lpCursorName">The identifier or name of the cursor resource to load. Use predefined values for system cursors (e.g., <see
        /// langword="32512"/> for the standard arrow cursor).</param>
        /// <returns>A handle to the loaded cursor if successful; otherwise, 0 if the operation fails.</returns>
        [DllImport("user32.dll")]
        public static extern int LoadCursor(int hInstance, int lpCursorName);

        /// <summary>
        /// Sets the cursor to the specified handle.
        /// </summary>
        /// <remarks>This method is a wrapper for the native SetCursor function in the Windows API.  The caller is
        /// responsible for ensuring that the provided cursor handle is valid.</remarks>
        /// <param name="hCursor">The handle to the cursor to set. This value must be a valid cursor handle.</param>
        /// <returns>An integer indicating the previous cursor handle. If the function fails, the return value is zero.</returns>
        [DllImport("user32.dll")]
        public static extern int SetCursor(int hCursor);

        /// <summary>
        /// Brings the thread that created the specified window into the foreground and activates the window.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// Animates a window by applying a visual effect over a specified duration.
        /// </summary>
        /// <remarks>The animation effect is determined by the combination of flags provided in the <paramref
        /// name="flags"/> parameter. Ensure that the window handle (<paramref name="hWnd"/>) is valid and that the window is
        /// visible or hidden as required by the specified animation flags.</remarks>
        /// <param name="hWnd">A handle to the window to be animated. This parameter cannot be <see langword="IntPtr.Zero"/>.</param>
        /// <param name="time">The duration of the animation, in milliseconds.</param>
        /// <param name="flags">A combination of <see cref="AnimateWindowFlags"/> values that specify the type of animation to apply.</param>
        /// <returns><see langword="true"/> if the animation is successfully applied; otherwise, <see langword="false"/>.</returns>
        [DllImport("user32.dll")]
        public static extern bool AnimateWindow(IntPtr hWnd, int time, AnimateWindowFlags flags);

        /// <summary>
        /// Destroys an icon and frees any memory the icon occupied.
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "DestroyIcon")]
        public static extern bool DestroyIcon(IntPtr hIcon);

        /// <summary>
        /// Sets the colors of the specified display elements.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool SetSysColors(int cElements, int[] lpaElements, uint[] lpaRgbValues);

        /// <summary>
        /// Retrieves the current color of a specified display element.
        /// </summary>
        /// <remarks>This method is a wrapper for the Windows API function in user32.dll.  The color
        /// returned is based on the current system theme and user settings.</remarks>
        /// <param name="nIndex">A system color index that specifies the display element whose color is to be retrieved.  For example, use 1
        /// for the active window border or 2 for the background color.  Refer to the Windows API documentation for a
        /// complete list of valid system color indices.</param>
        /// <returns>The red, green, and blue (RGB) color value of the specified display element.  The value is a 32-bit integer
        /// where the least significant byte represents the blue component,  the next byte represents the green
        /// component, and the third byte represents the red component.</returns>
        [DllImport("user32.dll")]
        public static extern int GetSysColor(int nIndex);

        /// <summary>
        /// Sets the window composition attribute.
        /// </summary>
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        /// <summary>
        /// Retrieves information about the specified window. The function retrieves a value at the specified offset in the
        /// window's extra memory or other window-related information.
        /// </summary>
        /// <remarks>Common values for <paramref name="nIndex"/> include constants like <c>GWL_STYLE</c>,
        /// <c>GWL_EXSTYLE</c>, and <c>GWL_WNDPROC</c>. Refer to the Windows API documentation for a full list of valid
        /// constants.</remarks>
        /// <param name="hWnd">A handle to the window whose information is to be retrieved.</param>
        /// <param name="nIndex">The zero-based offset to the value to be retrieved. This can be a predefined constant that specifies the type of
        /// information to retrieve, such as window styles or extended styles.</param>
        /// <returns>The requested value if the function succeeds, or 0 if it fails. To get extended error information, call <see
        /// cref="Marshal.GetLastWin32Error"/>.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        /// <summary>
        /// Changes an attribute of the specified window.
        /// </summary>
        /// <remarks>This method is a wrapper for the native <c>SetWindowLong</c> function in the Windows
        /// API.  Use this method with caution, as modifying certain attributes can affect the behavior and appearance
        /// of the window.</remarks>
        /// <param name="hWnd">A handle to the window whose attribute is to be changed.</param>
        /// <param name="nIndex">The zero-based offset to the value to be set. This parameter specifies the attribute to modify, such as 
        /// window styles or extended window styles. Common values include <see langword="GWL_STYLE"/> and <see
        /// langword="GWL_EXSTYLE"/>.</param>
        /// <param name="dwNewLong">The new value to set for the specified attribute.</param>
        /// <returns>The previous value of the specified attribute if the function succeeds; otherwise, returns zero.  To get
        /// extended error information, call <see cref="Marshal.GetLastWin32Error"/>.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        /// <summary>
        /// Sets the opacity and transparency color key of a layered window.
        /// </summary>
        /// <remarks>This function is used to modify the appearance of a layered window. The window must
        /// have the <see langword="WS_EX_LAYERED"/> extended style set  using <see cref="SetWindowLong"/> before
        /// calling this function. The <paramref name="dwFlags"/> parameter determines whether the function modifies 
        /// the transparency color key, the alpha value, or both.</remarks>
        /// <param name="hwnd">A handle to the layered window. This window must have the <see langword="WS_EX_LAYERED"/> style set.</param>
        /// <param name="crKey">The color key to be used for transparency. Pixels in the window with this color will be transparent. Set to
        /// 0 to disable the color key.</param>
        /// <param name="bAlpha">The alpha value (0 to 255) that specifies the opacity of the layered window. A value of 0 is fully
        /// transparent, and 255 is fully opaque.</param>
        /// <param name="dwFlags">Flags that specify the options for the layered window. This can be a combination of <see
        /// langword="LWA_COLORKEY"/> and <see langword="LWA_ALPHA"/>.</param>
        /// <returns><see langword="true"/> if the function succeeds; otherwise, <see langword="false"/>.  Call <see
        /// cref="Marshal.GetLastWin32Error"/> to retrieve extended error information if the function fails.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        /// <summary>
        /// Loads the specified cursor resource from a File.
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "LoadCursorFromFileA")]
        public static extern IntPtr LoadCursorFromFile(string lpFileName);

        /// <summary>
        /// Sends the specified message to a window or windows.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Sends the specified message to a window or windows.
        /// </summary>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        /// <summary>
        /// Set a new IntPtr to a hwnd
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nIndex"></param>
        /// <param name="dwNewLong"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        /// <summary>
        /// Passes a window message to the specified window procedure for processing.
        /// </summary>
        /// <param name="lpPrevWndFunc">
        /// A pointer to the previous window procedure. This value is typically obtained by calling
        /// <c>SetWindowLongPtr</c> or <c>GetWindowLongPtr</c> with <c>GWLP_WNDPROC</c>.
        /// </param>
        /// <param name="hWnd">
        /// A handle to the window procedure that will receive the message.
        /// </param>
        /// <param name="msg">
        /// The message identifier. This value specifies the message being sent.
        /// </param>
        /// <param name="wParam">
        /// Additional message-specific information. The meaning of this parameter depends on the value of <paramref name="msg"/>.
        /// </param>
        /// <param name="lParam">
        /// Additional message-specific information. The meaning of this parameter depends on the value of <paramref name="msg"/>.
        /// </param>
        /// <returns>
        /// The return value depends on the message being processed. This value is returned directly from
        /// the window procedure specified by <paramref name="lpPrevWndFunc"/>.
        /// </returns>
        /// <remarks>
        /// This function is commonly used when subclassing a window to forward messages that are not
        /// explicitly handled by the custom window procedure to the original (previous) window procedure.
        /// Failing to call the previous window procedure for unhandled messages may result in undefined
        /// behavior or broken window functionality.
        /// </remarks>
        /// <seealso href="https://learn.microsoft.com/windows/win32/api/winuser/nf-winuser-callwindowprocw"/>
        [DllImport("user32.dll")]
        public static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Represents a window procedure callback used to process messages sent to a window.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window receiving the message.
        /// </param>
        /// <param name="msg">
        /// The message identifier that specifies the message being processed.
        /// </param>
        /// <param name="wParam">
        /// Additional message-specific information. The meaning of this parameter depends on the value of <paramref name="msg"/>.
        /// </param>
        /// <param name="lParam">
        /// Additional message-specific information. The meaning of this parameter depends on the value of <paramref name="msg"/>.
        /// </param>
        /// <returns>
        /// The result of the message processing, which depends on the message being handled.
        /// </returns>
        /// <remarks>
        /// This delegate is typically used when subclassing a window in managed code, allowing a custom
        /// window procedure to intercept and handle Windows messages before optionally forwarding them
        /// to the original window procedure via <c>CallWindowProc</c>.
        /// </remarks>
        public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Marks the specified window's client area (or a portion of it) for repainting.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window whose client area is to be invalidated. If this parameter is <see cref="IntPtr.Zero"/>,
        /// the entire screen is invalidated.
        /// </param>
        /// <param name="lpRect">
        /// A pointer to a <c>RECT</c> structure that specifies the client area to be invalidated.
        /// If this parameter is <see cref="IntPtr.Zero"/>, the entire client area is invalidated.
        /// </param>
        /// <param name="bErase">
        /// Specifies whether the background should be erased when the window is repainted.
        /// <c>true</c> erases the background; <c>false</c> does not.
        /// </param>
        /// <returns>
        /// <c>true</c> if the function succeeds; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// Invalidating a rectangle does not cause an immediate repaint. Instead, it signals the system
        /// that the window needs repainting, which results in a <c>WM_PAINT</c> message being posted to the window.
        /// </remarks>
        /// <seealso href="https://learn.microsoft.com/windows/win32/api/winuser/nf-winuser-invalidaterect"/>
        [DllImport("user32.dll")]
        public static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);

        /// <summary>
        /// Retrieves the coordinates of a window's client area.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window whose client area coordinates are to be retrieved.
        /// </param>
        /// <param name="lpRect">
        /// When this method returns, contains a <c>RECT</c> structure that receives the client coordinates.
        /// The coordinates are relative to the upper-left corner of the client area.
        /// </param>
        /// <returns>
        /// <c>true</c> if the function succeeds; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// The client area excludes non-client elements such as the title bar, borders, and scroll bars.
        /// The returned coordinates are expressed in client coordinates, not screen coordinates.
        /// </remarks>
        /// <seealso href="https://learn.microsoft.com/windows/win32/api/winuser/nf-winuser-getclientrect"/>
        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        /// <summary>
        /// Updates the system parameters for the current user.
        /// </summary>
        /// <remarks>This method is a wrapper for the native <c>UpdatePerUserSystemParameters</c> function
        /// in the Windows API. It is typically used to apply changes to system settings that are specific to the
        /// current user.</remarks>
        /// <param name="flags">1 to force update.</param>
        /// <param name="force">A value indicating whether to force the update of system parameters.  <see langword="true"/> forces the
        /// update; <see langword="false"/> performs the update only if necessary.</param>
        /// <returns><see langword="true"/> if the system parameters were successfully updated; otherwise, <see
        /// langword="false"/>.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UpdatePerUserSystemParameters(uint flags, bool force);

        /// <summary>
        /// Represents a callback method used to process each window enumerated by the <see cref="EnumWindows"/>
        /// function.
        /// </summary>
        /// <param name="hWnd">A handle to the window being enumerated.</param>
        /// <param name="lParam">An application-defined value passed to the <see cref="EnumWindows"/> function.</param>
        /// <returns><see langword="true"/> to continue enumerating windows; <see langword="false"/> to stop enumeration.</returns>
        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        /// <summary>
        /// Enumerates the child windows of a specified parent window by passing them to a callback function.
        /// </summary>
        /// <remarks>This method is a P/Invoke wrapper for the native `EnumChildWindows` function in the
        /// Windows API. Ensure that the callback function specified by <paramref name="lpEnumFunc"/> is implemented
        /// correctly to handle the enumeration process.</remarks>
        /// <param name="hwndParent">A handle to the parent window whose child windows are to be enumerated. Pass <see langword="IntPtr.Zero"/>
        /// to enumerate all top-level windows.</param>
        /// <param name="lpEnumFunc">A callback function that is called for each child window. The function must match the <see
        /// cref="EnumWindowsProc"/> delegate signature. Returning <see langword="true"/> from the callback continues
        /// enumeration; returning <see langword="false"/> stops it.</param>
        /// <param name="lParam">An application-defined value to be passed to the callback function. This can be used to pass custom data to
        /// the callback.</param>
        /// <returns><see langword="true"/> if the enumeration succeeds; otherwise, <see langword="false"/>. If the function
        /// fails, call <see cref="Marshal.GetLastWin32Error"/> to retrieve the error code.</returns>
        [DllImport("user32.dll")]
        public static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

        /// <summary>
        /// Retrieves a list of handles for all child windows of the specified parent window, including nested child
        /// windows.
        /// </summary>
        /// <remarks>This method performs a recursive enumeration of all child windows, including nested
        /// child windows, starting from the specified parent window.</remarks>
        /// <param name="parent">The handle of the parent window whose child window handles are to be retrieved.</param>
        /// <returns>A list of <see cref="IntPtr"/> objects representing the handles of all child windows of the specified parent
        /// window. The list will be empty if no child windows are found.</returns>
        public static List<IntPtr> GetChildWindowHandles(IntPtr parent)
        {
            List<IntPtr> handles = [];

            void EnumRecursive(IntPtr hWnd)
            {
                EnumChildWindows(hWnd, (child, lParam) =>
                {
                    handles.Add(child);
                    EnumRecursive(child); // recurse into this child's children
                    return true;
                }, IntPtr.Zero);
            }

            EnumRecursive(parent);
            return handles;
        }

        /// <summary>
        /// Retrieves a handle to a device context (DC) for the client area of a specified window or for the entire
        /// screen.
        /// </summary>
        /// <remarks>The device context handle retrieved by this method must be released after use by
        /// calling the <c>ReleaseDC</c> function. Failure to release the device context can result in resource
        /// leaks.</remarks>
        /// <param name="hWnd">A handle to the window whose device context is to be retrieved. If this parameter is <see
        /// langword="IntPtr.Zero"/>,  the device context for the entire screen is retrieved.</param>
        /// <returns>A handle to the device context for the specified window or screen. If the function fails, the return value
        /// is <see langword="IntPtr.Zero"/>.</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        /// <summary>
        /// Releases a device context (DC) for a specified window, freeing it for use by other applications.
        /// </summary>
        /// <remarks>This method is used to release a device context obtained by calling <c>GetDC</c> or
        /// <c>GetWindowDC</c>.  Failing to release a DC can result in resource leaks.</remarks>
        /// <param name="hWnd">A handle to the window whose DC is to be released. This parameter can be <see cref="IntPtr.Zero"/> if the DC was
        /// obtained for the entire screen.</param>
        /// <param name="hDC">A handle to the device context to be released.</param>
        /// <returns>A value indicating whether the device context was successfully released.  Returns 1 if the DC was released
        /// successfully; otherwise, returns 0.</returns>
        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        /// <summary>
        /// Sets the background color for a specified device context.
        /// </summary>
        /// <remarks>The background color is used to fill gaps in styled lines, the gaps between hatched lines in brushes,
        /// and the background in text operations.</remarks>
        /// <param name="hdc">A handle to the device context for which the background color is set.</param>
        /// <param name="crColor">The new background color, specified as a COLORREF value. The COLORREF value is a 32-bit integer that specifies the
        /// RGB color.</param>
        /// <returns>The previous background color as a COLORREF value if successful; otherwise, returns CLR_INVALID (0xFFFFFFFF).</returns>
        [DllImport("user32.dll")]
        public static extern uint SetBkColor(IntPtr hdc, int crColor);

        /// <summary>
        /// Sets the text color for the specified device context.
        /// </summary>
        /// <remarks>The <see cref="SetTextColor"/> function changes the text color used by the specified
        /// device context. The new color is used for text-drawing operations until the text color is changed
        /// again.</remarks>
        /// <param name="hdc">A handle to the device context.</param>
        /// <param name="crColor">The color value to set, specified as a COLORREF value. The COLORREF value is a 32-bit integer where the
        /// low-order byte specifies the intensity of red, the second byte specifies the intensity of green, and the
        /// third byte specifies the intensity of blue.</param>
        /// <returns>The previous text color as a COLORREF value, or 0xFFFFFFFF if an error occurs.</returns>
        [DllImport("user32.dll")]
        public static extern uint SetTextColor(IntPtr hdc, int crColor);

        /// <summary>
        /// Retrieves the class name of the specified window.
        /// </summary>
        /// <remarks>To retrieve extended error information when the function fails, call <see
        /// cref="Marshal.GetLastWin32Error"/>.</remarks>
        /// <param name="hWnd">A handle to the window whose class name is to be retrieved.</param>
        /// <param name="lpClassName">A <see cref="StringBuilder"/> that receives the class name string.</param>
        /// <param name="nMaxCount">The maximum number of characters to copy to <paramref name="lpClassName"/>, including the null-terminating
        /// character.</param>
        /// <returns>The length of the class name string, in characters, not including the null-terminating character, if
        /// successful; otherwise, 0 if the function fails.</returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        /// <summary>
        /// Gets if a hwnd is a window or not.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = false)]
        public static extern bool IsWindow(IntPtr hWnd);

        /// <summary>
        /// SetWindowsHookEx function: Installs an application-defined hook procedure into a hook chain.
        /// This hook can monitor low-level mouse input events before the system processes them.
        /// </summary>
        /// <param name="idHook">The type of hook procedure to be installed.</param>
        /// <param name="lpfn">A pointer to the hook procedure.</param>
        /// <param name="hMod">A handle to the DLL containing the hook procedure pointed to by the lpfn parameter.</param>
        /// <param name="dwThreadId">The identifier of the thread with which the hook procedure is to be associated.</param>
        /// <returns>If the function succeeds, the return value is the handle to the hook procedure.
        /// If the function fails, the return value is IntPtr.Zero.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        /// <summary>
        /// UnhookWindowsHookEx function: Removes a hook procedure installed in a hook chain by the SetWindowsHookEx function.
        /// </summary>
        /// <param name="hhk">A handle to the hook to be removed.</param>
        /// <returns>If the function succeeds, the return value is true.
        /// If the function fails, the return value is false.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        /// <summary>
        /// CallNextHookEx function: Passes the hook information to the next hook procedure in the current hook chain.
        /// </summary>
        /// <param name="hhk">A handle to the current hook.</param>
        /// <param name="nCode">The hook code passed to the current hook procedure.</param>
        /// <param name="wParam">The wParam value passed to the current hook procedure.</param>
        /// <param name="lParam">The lParam value passed to the current hook procedure.</param>
        /// <returns>If the function succeeds, the return value is the result value returned by the next hook procedure in the chain.
        /// If the function fails, the return value is IntPtr.Zero.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Sends a message to the specified window and waits for the operation to complete or for a timeout period to
        /// elapse.
        /// </summary>
        /// <remarks>This function is typically used for inter-process communication or to send messages
        /// to windows in other threads. Use caution when sending messages to avoid potential deadlocks or performance
        /// issues, especially when specifying a long timeout.</remarks>
        /// <param name="hWnd">A handle to the window whose window procedure will receive the message. If this parameter is <see
        /// langword="IntPtr.Zero"/>, the message is sent to all top-level windows in the system.</param>
        /// <param name="Msg">The message to be sent.</param>
        /// <param name="wParam">Additional message-specific information. The meaning of this parameter depends on the value of <paramref
        /// name="Msg"/>.</param>
        /// <param name="lParam">A string containing additional message-specific information. The meaning of this parameter depends on the
        /// value of <paramref name="Msg"/>.</param>
        /// <param name="fuFlags">The behavior of the function. This parameter can be a combination of one or more
        /// <c>SendMessageTimeoutFlags</c> values.</param>
        /// <param name="uTimeout">The duration, in milliseconds, to wait for the message to be processed. If the timeout elapses, the function
        /// returns without completing the operation.</param>
        /// <param name="lpdwResult">When the function returns, contains the result of the message processing. This parameter is set to <see
        /// langword="UIntPtr.Zero"/> if the function times out.</param>
        /// <returns>If the function succeeds, the return value is a handle to the window that processed the message. If the
        /// function times out or encounters an error, the return value is <see langword="IntPtr.Zero"/>. Call
        /// <c>Marshal.GetLastWin32Error</c> to retrieve extended error information.</returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageTimeout(IntPtr hWnd, uint Msg, UIntPtr wParam, string lParam, uint fuFlags, uint uTimeout, out UIntPtr lpdwResult);

        /// <summary>
        /// Represents the Windows message identifier for a system-wide setting change notification.
        /// </summary>
        /// <remarks>This constant is used to identify the <c>WM_SETTINGCHANGE</c> message, which is sent
        /// to all top-level windows when a system-wide setting or policy has changed. Applications can handle this
        /// message to respond to changes in system settings, such as environment variables or user
        /// preferences.</remarks>
        public const uint WM_SETTINGCHANGE = 0x001A;

        /// <summary>
        /// Specifies that the message should be sent only if the receiving application is responsive.
        /// </summary>
        /// <remarks>This constant is used with the <c>SendMessageTimeout</c> function to indicate that
        /// the operation  should be aborted if the receiving application is not responding.</remarks>
        public const uint SMTO_ABORTIFHUNG = 0x0002;

        /// <summary>
        /// Notifies the system that a setting has changed, allowing applications to update accordingly.
        /// </summary>
        /// <remarks>This method broadcasts a system-wide message to inform applications of the specified
        /// setting change. Use this method to notify other applications or components when a configuration or system
        /// setting has been updated. The default section, "ImmersiveColorSet", is commonly used for theme or
        /// color-related changes.</remarks>
        /// <param name="section">The name of the setting section that has changed. Defaults to "ImmersiveColorSet".</param>
        public static void NotifySettingChanged(string section = "ImmersiveColorSet")
        {
            SendMessageTimeout(new IntPtr(HWND_BROADCAST), WM_SETTINGCHANGE, UIntPtr.Zero, section, SMTO_ABORTIFHUNG, 100, out _);
        }

        /// <summary>
        /// Releases the mouse capture from a window in the current thread.
        /// </summary>
        /// <remarks>This method is a wrapper for the native <c>ReleaseCapture</c> function in the Windows
        /// API.  It is typically used to release mouse capture when a window has captured the mouse input.</remarks>
        /// <returns><see langword="true"/> if the mouse capture was successfully released; otherwise, <see langword="false"/>.</returns>
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        /// <summary>
        /// LowLevelMouseProc delegate: Represents the method that will handle the low-level mouse input events.
        /// </summary>
        /// <param name="nCode">A code the hook procedure uses to determine how to process the message.</param>
        /// <param name="wParam">The wParam value passed to the hook procedure.</param>
        /// <param name="lParam">The lParam value passed to the hook procedure.</param>
        /// <returns>If the function succeeds, the return value is the result value returned by the hook procedure.
        /// If the function fails, the return value is IntPtr.Zero.</returns>
        public delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Constant for LoadImage function specifying the type of image to load.
        /// </summary>
        public const uint IMAGE_CURSOR = 2;

        /// <summary>
        /// The DrawIconEx function draws an icon or cursor into the specified device context.
        /// </summary>
        /// <param name="hdc">A handle to the device context into which the icon or cursor will be drawn.</param>
        /// <param name="xLeft">The logical x-coordinate of the upper-left corner of the icon or cursor.</param>
        /// <param name="yTop">The logical y-coordinate of the upper-left corner of the icon or cursor.</param>
        /// <param name="hIcon">A handle to the icon or cursor to be drawn. The icon or cursor must have been created by a previous call to the LoadIcon function or LoadCursor function.</param>
        /// <param name="cxWidth">The logical width of the icon or cursor, in logical units. If this parameter is zero, the function uses the actual resource width.</param>
        /// <param name="cyHeight">The logical height of the icon or cursor, in logical units. If this parameter is zero, the function uses the actual resource height.</param>
        /// <param name="istepIfAniCur">The index of the image in the cursor's image list. This parameter is ignored for icons.</param>
        /// <param name="hbrFlickerFreeDraw">A handle to the brush that the function uses to draw the icon or cursor. If this parameter is NULL, the function uses the default brush.</param>
        /// <param name="diFlags">This parameter can be one or more of the following values.
        /// <list type="table">
        /// <item>
        /// <term>DI_NORMAL</term>
        /// <description>Draws the icon or cursor using the image's size without any stretching.</description>
        /// </item>
        /// <item>
        /// <term>DI_IMAGE</term>
        /// <description>Draws the icon or cursor using the image's size without any stretching. This flag is similar to DI_NORMAL, but preserves the 8-bit alpha_hover channel of the icon's XOR mask. The default is to treat this image as an opaque image.</description>
        /// </item>
        /// </list>
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is true.
        /// If the function fails, the return value is false.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DrawIconEx(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon, int cxWidth, int cyHeight, int istepIfAniCur, IntPtr hbrFlickerFreeDraw, int diFlags);

        /// <summary>
        /// Structure that contains information about the high contrast accessibility feature.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct HIGHCONTRAST
        {
            /// <summary>
            /// Size of the structure
            /// </summary>
            public int cbSize;
            /// <summary>
            /// Flags indicating high contrast settings
            /// </summary>
            public uint dwFlags;
            /// <summary>
            /// Pointer to a null-terminated string
            /// </summary>
            public IntPtr lpszDefaultScheme;
        }

        /// <summary>
        /// Contains parameters that describe the non-client area metrics of a window, such as the caption height, border width, and the system font.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NONCLIENTMETRICS
        {
            /// <summary>
            /// The size, in bytes, of this structure.
            /// </summary>
            public uint cbSize;

            /// <summary>
            /// The width of the window border, in pixels.
            /// </summary>
            public int iBorderWidth;

            /// <summary>
            /// The width of the scroll box, in pixels.
            /// </summary>
            public int iScrollWidth;

            /// <summary>
            /// The height of the scroll box, in pixels.
            /// </summary>
            public int iScrollHeight;

            /// <summary>
            /// The width of the caption or title bar, in pixels.
            /// </summary>
            public int iCaptionWidth;

            /// <summary>
            /// The height of the caption or title bar, in pixels.
            /// </summary>
            public int iCaptionHeight;

            /// <summary>
            /// A LogFont structure that defines the font of the caption or title bar.
            /// </summary>
            public LogFont lfCaptionFont;

            /// <summary>
            /// The width of the small caption, in pixels.
            /// </summary>
            public int iSMCaptionWidth;

            /// <summary>
            /// The height of the small caption, in pixels.
            /// </summary>
            public int iSMCaptionHeight;

            /// <summary>
            /// A LogFont structure that defines the font of the small caption.
            /// </summary>
            public LogFont lfSMCaptionFont;

            /// <summary>
            /// The width of the menu bar, in pixels.
            /// </summary>
            public int iMenuWidth;

            /// <summary>
            /// The height of the menu bar, in pixels.
            /// </summary>
            public int iMenuHeight;

            /// <summary>
            /// A LogFont structure that defines the font of the menu bar.
            /// </summary>
            public LogFont lfMenuFont;

            /// <summary>
            /// A LogFont structure that defines the font of the status bar.
            /// </summary>
            public LogFont lfStatusFont;

            /// <summary>
            /// A LogFont structure that defines the font of the message box window.
            /// </summary>
            public LogFont lfMessageFont;

            /// <summary>
            /// The border padding for caption buttons, in pixels.
            /// </summary>
            public int iPaddedBorderWidth;

            /// <summary>
            /// Convert a byte array to a NONCLIENTMETRICS structure.
            /// </summary>
            /// <param name="bytes"></param>
            public NONCLIENTMETRICS(byte[] bytes)
            {
                int intSize = sizeof(int);
                int uintSize = sizeof(uint);
                int logFontSize = 60; //Marshal.SizeOf(typeof(LogFont));
                int cursor = 0;

                cbSize = BitConverter.ToUInt32(bytes, cursor);
                cursor += uintSize;

                iBorderWidth = BitConverter.ToInt32(bytes, cursor);
                cursor += intSize;

                iScrollWidth = BitConverter.ToInt32(bytes, cursor);
                cursor += intSize;

                iScrollHeight = BitConverter.ToInt32(bytes, cursor);
                cursor += intSize;

                iCaptionWidth = BitConverter.ToInt32(bytes, cursor);
                cursor += intSize;

                iCaptionHeight = BitConverter.ToInt32(bytes, cursor);
                cursor += intSize;

                lfCaptionFont = bytes.SubArray(cursor, logFontSize).ToLogFont();
                cursor += logFontSize;

                iSMCaptionWidth = BitConverter.ToInt32(bytes, cursor);
                cursor += intSize;

                iSMCaptionHeight = BitConverter.ToInt32(bytes, cursor);
                cursor += intSize;

                lfSMCaptionFont = bytes.SubArray(cursor, logFontSize).ToLogFont();
                cursor += logFontSize;

                iMenuWidth = BitConverter.ToInt32(bytes, cursor);
                cursor += intSize;

                iMenuHeight = BitConverter.ToInt32(bytes, cursor);
                cursor += intSize;

                lfMenuFont = bytes.SubArray(cursor, logFontSize).ToLogFont();
                cursor += logFontSize;

                lfStatusFont = bytes.SubArray(cursor, logFontSize).ToLogFont();
                cursor += logFontSize;

                lfMessageFont = bytes.SubArray(cursor, logFontSize).ToLogFont();
                cursor += logFontSize;

                // Check if there are enough bytes to read the iPaddedBorderWidth value as it is implemented in Windows Vista and later.
                if (bytes.Length >= cursor + intSize)
                {
                    iPaddedBorderWidth = BitConverter.ToInt32(bytes, cursor);
                }
                else { iPaddedBorderWidth = 0; }
            }

            /// <summary>
            /// Convert a NONCLIENTMETRICS structure to a byte array.
            /// </summary>
            /// <returns></returns>
            public readonly byte[] ToByteArray()
            {
                List<byte> byteArray =
                [
                    .. BitConverter.GetBytes(cbSize),
                    .. BitConverter.GetBytes(iBorderWidth),
                    .. BitConverter.GetBytes(iScrollWidth),
                    .. BitConverter.GetBytes(iScrollHeight),
                    .. BitConverter.GetBytes(iCaptionWidth),
                    .. BitConverter.GetBytes(iCaptionHeight),
                    .. lfCaptionFont.ToBytes(60),
                    .. BitConverter.GetBytes(iSMCaptionWidth),
                    .. BitConverter.GetBytes(iSMCaptionHeight),
                    .. lfSMCaptionFont.ToBytes(60),
                    .. BitConverter.GetBytes(iMenuWidth),
                    .. BitConverter.GetBytes(iMenuHeight),
                    .. lfMenuFont.ToBytes(60),
                    .. lfStatusFont.ToBytes(60),
                    .. lfMessageFont.ToBytes(60),
                    .. BitConverter.GetBytes(iPaddedBorderWidth),
                ];
                return [.. byteArray];
            }
        }

        /// <summary>
        /// Contains parameters that describe the metrics of icons displayed by the Shell.
        /// </summary>
        public struct ICONMETRICS
        {
            /// <summary>
            /// The size, in bytes, of this structure.
            /// </summary>
            public uint cbSize;

            /// <summary>
            /// The horizontal space, in pixels, between icons.
            /// </summary>
            public int iHorzSpacing;

            /// <summary>
            /// The vertical space, in pixels, between icons.
            /// </summary>
            public int iVertSpacing;

            /// <summary>
            /// The maximum number of characters displayed in a label.
            /// </summary>
            public int iTitleWrap;

            /// <summary>
            /// A LogFont structure that defines the font of the icon label.
            /// </summary>
            public LogFont lfFont;

            /// <summary>
            /// Convert a byte array to an ICONMETRICS structure.
            /// </summary>
            /// <param name="bytes"></param>
            public ICONMETRICS(byte[] bytes)
            {
                int intSize = sizeof(int);
                int uintSize = sizeof(uint);
                int logFontSize = 60; //Marshal.SizeOf(typeof(LogFont));
                int cursor = 0;

                cbSize = BitConverter.ToUInt32(bytes, cursor);
                cursor += uintSize;

                iHorzSpacing = BitConverter.ToInt32(bytes, cursor);
                cursor += intSize;

                iVertSpacing = BitConverter.ToInt32(bytes, cursor);
                cursor += intSize;

                iTitleWrap = BitConverter.ToInt32(bytes, cursor);
                cursor += intSize;

                lfFont = bytes.SubArray(cursor, logFontSize).ToLogFont();
            }

            /// <summary>
            /// Convert an ICONMETRICS structure to a byte array.
            /// </summary>
            /// <returns></returns>
            public readonly byte[] ToByteArray()
            {
                List<byte> byteArray =
                [
                    .. BitConverter.GetBytes(cbSize),
                    .. BitConverter.GetBytes(iHorzSpacing),
                    .. BitConverter.GetBytes(iVertSpacing),
                    .. BitConverter.GetBytes(iTitleWrap),
                    .. lfFont.ToBytes(60),
                ];

                return [.. byteArray];
            }
        }

        /// <summary>
        /// Contains parameters that control the animation effects associated with user actions.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct ANIMATIONINFO
        {
            /// <summary>
            /// The size, in bytes, of this structure.
            /// </summary>
            public uint cbSize;

            /// <summary>
            /// If this member is TRUE, minimize and restore motion is enabled; otherwise, it is disabled.
            /// </summary>
            public int IMinAnimate;
        }

        /// <summary>
        /// Represents the accent policy for the Desktop Window Manager (DWM).
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct AccentPolicy
        {
            /// <summary>
            /// The state of the accent.
            /// </summary>
            public AccentState AccentState;

            /// <summary>
            /// Flags that determine which accent features are enabled.
            /// </summary>
            public int AccentFlags;

            /// <summary>
            /// The color of the gradient used for the accent.
            /// </summary>
            public int GradientColor;

            /// <summary>
            /// Animation identifier.
            /// </summary>
            public int AnimationId;
        }

        /// <summary>
        /// Represents data for setting window composition attributes.
        /// </summary>
        internal struct WindowCompositionAttributeData
        {
            /// <summary>
            /// The attribute to set.
            /// </summary>
            public WindowCompositionAttribute Attribute;

            /// <summary>
            /// Pointer to the data for the attribute.
            /// </summary>
            public IntPtr Data;

            /// <summary>
            /// Size of the data.
            /// </summary>
            public int SizeOfData;
        }

        /// <summary>
        /// Enumeration representing window composition attributes.
        /// </summary>
        public enum WindowCompositionAttribute
        {
            /// <summary>
            /// Accent policy attribute.
            /// </summary>
            WCA_ACCENT_POLICY = 19,

            /// <summary>
            /// Usedark mode colors attribute.
            /// </summary>
            WCA_USEDARKMODECOLORS = 26
        }

        /// <summary>
        /// Enumeration representing different accent states for window composition.
        /// </summary>
        internal enum AccentState
        {
            /// <summary>
            /// The accent is disabled.
            /// </summary>
            ACCENT_DISABLED = 0,

            /// <summary>
            /// Enable gradient accent.
            /// </summary>
            ACCENT_ENABLE_GRADIENT = 1,

            /// <summary>
            /// Enable transparent gradient accent.
            /// </summary>
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,

            /// <summary>
            /// Enable blur behind the window.
            /// </summary>
            ACCENT_ENABLE_BLURBEHIND = 3,

            /// <summary>
            /// Enable transparent accent.
            /// </summary>
            ACCENT_ENABLE_TRANSPARENT = 6,

            /// <summary>
            /// Enable acrylic blur behind the window.
            /// </summary>
            ACCENT_ENABLE_ACRYLICBLURBEHIND = 4
        }

        /// <summary>
        /// Flags for animating a window using the AnimateWindow function.
        /// </summary>
        [Flags]
        public enum AnimateWindowFlags
        {
            /// <summary>
            /// Animates the window from left to right.
            /// </summary>
            AW_HOR_POSITIVE = 0x0,

            /// <summary>
            /// Animates the window from right to left.
            /// </summary>
            AW_HOR_NEGATIVE = 0x2,

            /// <summary>
            /// Animates the window from top to bottom.
            /// </summary>
            AW_VER_POSITIVE = 0x4,

            /// <summary>
            /// Animates the window from bottom to top.
            /// </summary>
            AW_VER_NEGATIVE = 0x8,

            /// <summary>
            /// Centers the window during the animation.
            /// </summary>
            AW_CENTER = 0x10,

            /// <summary>
            /// Hides the window.
            /// </summary>
            AW_HIDE = 0x10000,

            /// <summary>
            /// Activates the window.
            /// </summary>
            AW_ACTIVATE = 0x20000,

            /// <summary>
            /// Uses slide animation.
            /// </summary>
            AW_SLIDE = 0x40000,

            /// <summary>
            /// Uses blend animation.
            /// </summary>
            AW_BLEND = 0x80000
        }

        /// <summary>
        /// Flags for the DrawIconEx function.
        /// </summary>
        [Flags]
        public enum DrawIconExFlags : uint
        {
            /// <summary>
            /// Draws the icon using the current size.
            /// </summary>
            DI_NORMAL = 0x0003,

            /// <summary>
            /// Uses a compatible brush (specified by hbrFlickerFreeDraw) for the background when the icon is drawn.
            /// </summary>
            DI_COMPAT = 0x0004,

            /// <summary>
            /// Draws the icon as an image without using the mask.
            /// </summary>
            DI_IMAGE = 0x0008,

            /// <summary>
            /// Draws the icon using the mask.
            /// </summary>
            DI_MASK = 0x0010,

            /// <summary>
            /// Disables mirroring of the image.
            /// </summary>
            DI_NOMIRROR = 0x0010,

            /// <summary>
            /// Uses the default icon size.
            /// </summary>
            DI_DEFAULTSIZE = 0x0040,

            /// <summary>
            /// Suppresses any confirmation dialog box.
            /// </summary>
            DI_UNSAFE = 0x2000,

            /// <summary>
            /// Allows drawing with Write access.
            /// </summary>
            DI_WRITABLE = 0x1000,

            /// <summary>
            /// Allows drawing with Read access.
            /// </summary>
            DI_READONLY = 0x0800
        }

        /// <summary>
        /// Enumeration representing system cursors for OCR.
        /// </summary>
        public enum OCR_SYSTEM_CURSORS : int
        {
            /// <summary>
            /// Standard arrow and small hourglass.
            /// </summary>
            OCR_APPSTARTING = 32650,

            /// <summary>
            /// Standard arrow.
            /// </summary>
            OCR_NORMAL = 32512,

            /// <summary>
            /// Crosshair.
            /// </summary>
            OCR_CROSS = 32515,

            /// <summary>
            /// Windows 2000/WXP: Hand.
            /// </summary>
            OCR_HAND = 32649,

            /// <summary>
            /// Arrow and question mark.
            /// </summary>
            OCR_HELP = 32651,

            /// <summary>
            /// I-beam.
            /// </summary>
            OCR_IBEAM = 32513,

            /// <summary>
            /// Slashed circle.
            /// </summary>
            OCR_NO = 32648,

            /// <summary>
            /// Four-pointed arrow pointing north, south, east, and west.
            /// </summary>
            OCR_SIZEALL = 32646,

            /// <summary>
            /// Double-pointed arrow pointing northeast and southwest.
            /// </summary>
            OCR_SIZENESW = 32643,

            /// <summary>
            /// Double-pointed arrow pointing north and south.
            /// </summary>
            OCR_SIZENS = 32645,

            /// <summary>
            /// Double-pointed arrow pointing northwest and southeast.
            /// </summary>
            OCR_SIZENWSE = 32642,

            /// <summary>
            /// Double-pointed arrow pointing west and east.
            /// </summary>
            OCR_SIZEWE = 32644,

            /// <summary>
            /// Vertical arrow.
            /// </summary>
            OCR_UP = 32516,

            /// <summary>
            /// Hourglass.
            /// </summary>
            OCR_WAIT = 32514
        }

        /// <summary>
        /// Constants for various Windows messages.
        /// </summary>
        public static class WindowsMessages
        {
            /// <summary>
            /// Indicates a change in colorization color.
            /// </summary>
            public const int WM_DWMCOLORIZATIONCOLORCHANGED = 0x320;

            /// <summary>
            /// Indicates a change in DWM composition status.
            /// </summary>
            public const int WM_DWMCOMPOSITIONCHANGED = 0x31E;

            /// <summary>
            /// Indicates a change in theme.
            /// </summary>
            public const int WM_THEMECHANGED = 0x31A;

            /// <summary>
            /// Indicates a change in system color.
            /// </summary>
            public const int WM_SYSCOLORCHANGE = 0x15;

            /// <summary>
            /// Windows message constant for notifying all top-level windows about system-wide setting changes.
            /// </summary>
            public const int WM_SETTINGCHANGE = 0x001A;

            /// <summary>
            /// Indicates a change in palette.
            /// </summary>
            public const int WM_PALETTECHANGED = 0x311;

            /// <summary>
            /// Indicates a change in Windows initialization.
            /// </summary>
            public const uint WM_WININICHANGE = 0x1AU;
        }

        /// <summary>
        /// Special window handle value to broadcast the message to all top-level windows.
        /// </summary>
        public const int HWND_BROADCAST = 0xFFFF;

        public delegate bool EnumChildProc(IntPtr hWnd, IntPtr lParam);
    }
}
