using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using WinPaletter.UI.WP;
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
        private const string _user32 = "user32.dll";

        /// <summary>
        /// Hook procedure delegate used for low-level keyboard and mouse hooks. This delegate defines the signature of the callback function that will be called by the system when a hook event occurs.
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Retrieves a handle to the foreground window (the window with which the user is currently working).
        /// </summary>
        /// <returns>A handle to the foreground window. The foreground window can be NULL in certain circumstances, such as when a window is losing activation.</returns>
        [DllImport(_user32, SetLastError = true)]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// Retrieves the dimensions of the bounding rectangle of the specified window.
        /// </summary>
        /// <remarks>The coordinates returned are relative to the screen. If the window is minimized, the
        /// coordinates may not represent the visible area. To get the client area, use the GetClientRect
        /// function.</remarks>
        /// <param name="hWnd">A handle to the window whose coordinates are to be retrieved.</param>
        /// <param name="lpRect">When this method returns, contains a RECT structure that receives the screen coordinates of the upper-left
        /// and lower-right corners of the window.</param>
        /// <returns>true if the function succeeds; otherwise, false.</returns>
        [DllImport(_user32)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

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
        [DllImport(_user32, SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        /// <summary>
        /// Set position of a window in Z order
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hWndInsertAfter"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="Flags"></param>
        /// <returns></returns>
        [DllImport(_user32, SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowsPosition Flags);

        /// <summary>
        /// Get a handle to the ancestor of the specified window. The ancestor window is determined based on the specified flags, which can indicate a parent, root, or root owner window. This method is useful for navigating the window hierarchy and retrieving related windows based on their relationships.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="gaFlags"></param>
        /// <returns></returns>
        [DllImport(_user32)]
        public static extern IntPtr GetAncestor(IntPtr hWnd, uint gaFlags);

        /// <summary>
        /// Retrieves a value indicating whether the specified window is visible.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport(_user32, SetLastError = true)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport(_user32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsChild(IntPtr hWndParent, IntPtr hWnd);

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
        [DllImport(_user32)]
        public static extern int GetSystemMetrics(int nIndex);

        /// <summary>
        /// Retrieves the parameters of a scroll bar, including its range, page size, position, and tracking position.
        /// </summary>
        /// <remarks>The SCROLLINFO structure's cbSize member must be set to the size of the structure
        /// before calling this function. This method is typically used in interop scenarios to call the native Win32
        /// API.</remarks>
        /// <param name="hwnd">A handle to the window with the scroll bar.</param>
        /// <param name="fnBar">The type of scroll bar to retrieve information for. Specify SB_HORZ for the window's standard horizontal
        /// scroll bar, SB_VERT for the standard vertical scroll bar, or SB_CTL for a scroll bar control.</param>
        /// <param name="lpsi">A reference to a SCROLLINFO structure that, on input, specifies the scroll bar parameters to retrieve, and
        /// on output, receives the scroll bar information.</param>
        /// <returns>true if the function retrieves any values; otherwise, false.</returns>
        [DllImport(_user32)]
        public static extern bool GetScrollInfo(IntPtr hwnd, int fnBar, ref SCROLLINFO lpsi);

        /// <summary>
        /// Sets the specified window's show state, such as hiding or showing the window without activating it.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nCmdShow"></param>
        /// <returns></returns>
        [DllImport(_user32)]
        public static extern bool ShowWindow(IntPtr hWnd, ShowWindowFlags nCmdShow);

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
        [DllImport(_user32)]
        public static extern int LoadCursor(int hInstance, int lpCursorName);

        /// <summary>
        /// Sets the cursor to the specified handle.
        /// </summary>
        /// <remarks>This method is a wrapper for the native SetCursor function in the Windows API.  The caller is
        /// responsible for ensuring that the provided cursor handle is valid.</remarks>
        /// <param name="hCursor">The handle to the cursor to set. This value must be a valid cursor handle.</param>
        /// <returns>An integer indicating the previous cursor handle. If the function fails, the return value is zero.</returns>
        [DllImport(_user32)]
        public static extern int SetCursor(int hCursor);

        /// <summary>
        /// Brings the thread that created the specified window into the foreground and activates the window.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport(_user32)]
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
        [DllImport(_user32)]
        public static extern bool AnimateWindow(IntPtr hWnd, int time, AnimateWindowFlags flags);

        /// <summary>
        /// Updates the specified window or windows by invalidating the client area, causing the operating system to
        /// send a paint message and optionally updating the window immediately.
        /// </summary>
        /// <remarks>This method is a platform invoke (P/Invoke) signature for the native RedrawWindow
        /// function in user32.dll. Use this method to force a window or region to be redrawn, which can be useful when
        /// custom painting or when the window's appearance must be refreshed. For more information about valid flag
        /// values and usage scenarios, see the RedrawWindow function documentation on learn.microsoft.com.</remarks>
        /// <param name="hWnd">A handle to the window to be updated. If this parameter is <see langword="IntPtr.Zero"/>, the update applies
        /// to all windows in the system.</param>
        /// <param name="lprcUpdate">A pointer to a RECT structure that contains the coordinates of the update region in logical coordinates. If
        /// this parameter is <see langword="IntPtr.Zero"/>, the entire client area is considered for updating.</param>
        /// <param name="hrgnUpdate">A handle to a region that defines the area to be updated. If this parameter is <see
        /// langword="IntPtr.Zero"/>, no region is used.</param>
        /// <param name="flags">A set of flags that control the update operation. These flags specify options such as whether to erase the
        /// background, whether to send a paint message, and whether to update child windows.</param>
        /// <returns>true if the function succeeds; otherwise, false.</returns>
        [DllImport(_user32)]
        public static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, uint flags);

        /// <summary>
        /// Updates the specified window or windows by invalidating the client area, causing the operating system to
        /// send a paint message and optionally updating the window immediately.
        /// </summary>
        /// <remarks>This method is a platform invoke (P/Invoke) signature for the native RedrawWindow
        /// function in user32.dll. Use this method to force a window or region to be redrawn, which can be useful when
        /// custom painting or when the window's appearance must be refreshed. For more information about valid flag
        /// values and usage scenarios, see the RedrawWindow function documentation on learn.microsoft.com.</remarks>
        /// <param name="hWnd">A handle to the window to be updated. If this parameter is <see langword="IntPtr.Zero"/>, the update applies
        /// to all windows in the system.</param>
        /// <param name="lprcUpdate">A pointer to a RECT structure that contains the coordinates of the update region in logical coordinates. If
        /// this parameter is <see langword="IntPtr.Zero"/>, the entire client area is considered for updating.</param>
        /// <param name="hrgnUpdate">A handle to a region that defines the area to be updated. If this parameter is <see
        /// langword="IntPtr.Zero"/>, no region is used.</param>
        /// <param name="flags">A set of flags that control the update operation. These flags specify options such as whether to erase the
        /// background, whether to send a paint message, and whether to update child windows.</param>
        /// <returns>true if the function succeeds; otherwise, false.</returns>
        [DllImport(_user32)]
        public static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, RedrawWindowFlags flags);

        /// <summary>
        /// Gets the client coordinates of a specified point on the screen, converting from screen coordinates to client-area coordinates relative to the specified window.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpPoint"></param>
        /// <returns></returns>
        [DllImport(_user32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ScreenToClient(IntPtr hWnd, ref POINT lpPoint);

        /// <summary>
        /// Retrieves information about icons or icon-sized images from a specified File.
        /// </summary>
        /// <param name="lpszFile">The name of the File that contains the icon or icon-sized image.</param>
        /// <param name="nIconIndex">The zero-based index of the icon or image.</param>
        /// <param name="cxIcon">The desired width of the icon in pixels. The function uses this value to create a monochrome icon of the desired width.</param>
        /// <param name="cyIcon">The desired height of the icon in pixels. The function uses this value to create a monochrome icon of the desired height.</param>
        /// <param name="phicon">An array of icon or image handles to be filled by this function. The array should be pre-allocated to hold at least <paramref name="nIcons"/> elements.</param>
        /// <param name="piconid">An array of icon IDs. This parameter can be <see langword="null"/>.</param>
        /// <param name="nIcons">The number of icons to extract. This value is limited to the number of image bits in the icon resource.</param>
        /// <param name="flags">A combination of flags that specify the dimensions and behavior of the function.</param>
        /// <returns>The number of icons successfully extracted, or zero if the function fails.</returns>
        [DllImport(_user32, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint PrivateExtractIcons(string lpszFile, int nIconIndex, int cxIcon, int cyIcon, IntPtr[] phicon, uint[] piconid, uint nIcons, uint flags);

        /// <summary>
        /// Destroys an icon and frees any associated system resources.
        /// </summary>
        /// <remarks>After calling this method, the icon handle is no longer valid and should not be used.
        /// Failing to destroy unused icons can result in resource leaks.</remarks>
        /// <param name="hIcon">A handle to the icon to be destroyed. This handle must have been obtained from a previous call to a function
        /// that creates or loads icons.</param>
        /// <returns>true if the function succeeds; otherwise, false.</returns>
        [DllImport(_user32)]
        public static extern bool DestroyIcon(IntPtr hIcon);

        /// <summary>
        /// Displays a shortcut menu at the specified location and tracks the selection of menu items by the user.
        /// </summary>
        /// <remarks>This function is typically used to display context menus in response to user actions
        /// such as right-clicks. The caller is responsible for processing the command identifier returned by the
        /// function. If the function fails, use Marshal.GetLastWin32Error to obtain error information.</remarks>
        /// <param name="hMenu">A handle to the shortcut menu to be displayed. This handle must be created by a prior call to a menu
        /// creation function.</param>
        /// <param name="uFlags">A set of flags that specify function options and how the menu is displayed. These flags determine menu
        /// alignment, animation, and user interaction behavior.</param>
        /// <param name="x">The horizontal screen coordinate, in pixels, at which to display the menu.</param>
        /// <param name="y">The vertical screen coordinate, in pixels, at which to display the menu.</param>
        /// <param name="hWnd">A handle to the window that owns the shortcut menu. This window receives notification messages from the
        /// menu.</param>
        /// <param name="lptpm">A pointer to a TRACKPOPUPMENUEX structure that provides additional options for menu display, or IntPtr.Zero
        /// if not used.</param>
        /// <returns>The identifier of the selected menu item if a command item is chosen; otherwise, zero if no item is selected
        /// or if an error occurs.</returns>
        [DllImport(_user32, SetLastError = true, ExactSpelling = true)]
        public static extern uint TrackPopupMenuEx(IntPtr hMenu, uint uFlags, int x, int y, IntPtr hWnd, IntPtr lptpm);

        /// <summary>
        /// Changes the display colors for one or more display elements in the Windows user interface.
        /// </summary>
        /// <remarks>This method is a P/Invoke signature for the native SetSysColors function in
        /// user32.dll. Changes made by this function affect the system's color settings and may impact all
        /// applications. Callers should ensure that the arrays provided are not null and have at least cElements
        /// elements. This function may require appropriate privileges to execute successfully.</remarks>
        /// <param name="cElements">The number of display elements to change. This value must match the lengths of the lpaElements and
        /// lpaRgbValues arrays.</param>
        /// <param name="lpaElements">An array of integers specifying the display elements to change. Each value corresponds to a system color
        /// index (such as COLOR_WINDOW or COLOR_MENU).</param>
        /// <param name="lpaRgbValues">An array of unsigned integers specifying the new RGB color values for the corresponding elements in
        /// lpaElements. Each value is a COLORREF value representing a color.</param>
        /// <returns>true if the function succeeds; otherwise, false.</returns>
        [DllImport(_user32)]
        public static extern bool SetSysColors(int cElements, int[] lpaElements, uint[] lpaRgbValues);

        /// <summary>
        /// Retrieves a handle to a window whose class name and window name match the specified strings, searching among
        /// child windows of a specified parent window.
        /// </summary>
        /// <remarks>This method is a wrapper for the native FindWindowEx function in user32.dll. The
        /// search is case-sensitive. If both className and windowTitle are null, the function returns the first child
        /// window. This method does not search descendant windows; it searches only direct child windows.</remarks>
        /// <param name="parent">A handle to the parent window whose child windows are to be searched. Use IntPtr.Zero to search all
        /// top-level windows.</param>
        /// <param name="childAfter">A handle to a child window. The search begins with the next child window in Z order. Use IntPtr.Zero to
        /// start the search with the first child window of the parent.</param>
        /// <param name="className">The class name of the window to search for. This parameter can be null to match any class name.</param>
        /// <param name="windowTitle">The window name (title) to search for. This parameter can be null to match any window name.</param>
        /// <returns>A handle to the window that matches the specified criteria if found; otherwise, IntPtr.Zero.</returns>
        [DllImport(_user32)]
        public static extern IntPtr FindWindowEx(IntPtr parent, IntPtr childAfter, string className, string windowTitle);

        /// <summary>
        /// Enumerates all non-child windows associated with a specified thread by passing the handle of each window to a callback function.
        /// </summary>
        /// <param name="dwThreadId"></param>
        /// <param name="lpfn"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport(_user32)]
        public static extern bool EnumThreadWindows(int dwThreadId, EnumThreadWndProc lpfn, IntPtr lParam);
        public delegate bool EnumThreadWndProc(IntPtr hWnd, IntPtr lParam);

        /// <summary>
        /// Finds the handle of the edit control associated with a specified list view control.
        /// </summary>
        /// <remarks>This method is typically used to obtain the handle of the in-place edit control that
        /// appears when a list view item is being edited. The returned handle can be used to interact with the edit
        /// control directly, such as setting or retrieving text.</remarks>
        /// <param name="listViewHandle">The handle of the list view control in which to search for the edit control. This handle must refer to a
        /// valid list view window.</param>
        /// <returns>An <see cref="IntPtr"/> representing the handle of the edit control if found; otherwise, <see
        /// cref="IntPtr.Zero"/> if no edit control is present.</returns>
        public static IntPtr FindEditControl(IntPtr listViewHandle)
        {
            return FindWindowEx(listViewHandle, IntPtr.Zero, "Edit", null);
        }

        /// <summary>
        /// Sets a new value for a specified class attribute of a window.
        /// </summary>
        /// <remarks>This method is a wrapper for the Win32 API function SetClassLong, which modifies
        /// attributes of a window class. Ensure that the window handle is valid and that the specified index
        /// corresponds to a valid class attribute.</remarks>
        /// <param name="hWnd">A handle to the window whose class attribute is to be modified.</param>
        /// <param name="nIndex">The zero-based offset to the value to be set. This specifies which class attribute to modify.</param>
        /// <param name="dwNewLong">The new value to assign to the specified class attribute.</param>
        /// <returns>The previous value of the specified class attribute. If the function fails, the return value is zero.</returns>
        [DllImport(_user32, EntryPoint = "SetClassLong")]
        private static extern int SetClassLong32(IntPtr hWnd, int nIndex, int dwNewLong);

        /// <summary>
        /// Sets a new value for the specified class attribute of a window.
        /// </summary>
        /// <remarks>This method is intended for use with 64-bit Windows operating systems. Modifying
        /// class attributes can affect window behavior and appearance. Ensure that the window handle and attribute
        /// index are valid before calling this method.</remarks>
        /// <param name="hWnd">A handle to the window whose class attribute is to be modified.</param>
        /// <param name="nIndex">The zero-based offset that specifies which class attribute to set.</param>
        /// <param name="dwNewLong">The new value to assign to the specified class attribute.</param>
        /// <returns>The previous value of the specified class attribute if successful; otherwise, <see cref="IntPtr.Zero"/> if
        /// the operation fails.</returns>
        [DllImport(_user32, EntryPoint = "SetClassLongPtr")]
        private static extern IntPtr SetClassLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        /// <summary>
        /// Sets a new value for the specified class attribute of a window.
        /// </summary>
        /// <remarks>This method automatically selects the appropriate implementation for 32-bit or 64-bit
        /// platforms based on the current process architecture.</remarks>
        /// <param name="hWnd">A handle to the window whose class attribute is to be modified.</param>
        /// <param name="nIndex">The zero-based offset that specifies which class attribute to set.</param>
        /// <param name="dwNewLong">The new value to assign to the specified class attribute. The meaning and type of this value depend on the
        /// attribute identified by nIndex.</param>
        /// <returns>A handle to the previous value of the specified class attribute if successful; otherwise, <see
        /// cref="IntPtr.Zero"/>.</returns>
        public static IntPtr SetClassLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 8) return SetClassLongPtr64(hWnd, nIndex, dwNewLong);

            return new IntPtr(SetClassLong32(hWnd, nIndex, dwNewLong.ToInt32()));
        }

        /// <summary>
        /// Gets a value indicating whether a specified point lies within the boundaries of a given rectangle.
        /// </summary>
        /// <param name="lprc"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        [DllImport(_user32)]
        public static extern bool PtInRect(ref RECT lprc, POINT pt);

        /// <summary>
        /// Gets the DPI (dots per inch) value for a specified window, which can be used to determine the scaling factor for rendering content appropriately on high-DPI displays.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport(_user32)]
        public static extern uint GetDpiForWindow(IntPtr hWnd);

        /// <summary>
        /// Draws an icon or cursor into the specified device context, allowing for custom rendering of icons at specific locations and sizes.
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="xLeft"></param>
        /// <param name="yTop"></param>
        /// <param name="hIcon"></param>
        /// <param name="cxWidth"></param>
        /// <param name="cyWidth"></param>
        /// <param name="istepIfAniCur"></param>
        /// <param name="hbrFlickerFreeDraw"></param>
        /// <param name="diFlags"></param>
        /// <returns></returns>
        [DllImport(_user32)]
        public static extern bool DrawIconEx(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon, int cxWidth, int cyWidth, uint istepIfAniCur, IntPtr hbrFlickerFreeDraw, uint diFlags);

        [DllImport(_user32, SetLastError = true)]
        public static extern bool DrawEdge(IntPtr hdc, ref UxTheme.RECT qrc, uint edge, uint grfFlags);

        [DllImport("user32.dll")]
        public static extern bool ValidateRect(IntPtr hWnd, IntPtr lpRect);

        /// <summary>
        /// Gets the value of a specified class attribute for a window, allowing retrieval of information such as styles, extended styles, or other class-specific data.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        [DllImport(_user32)]
        public static extern IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex);

        /// <summary>
        /// Gets the text of the specified window's title bar (if it has one) or the text of a control within the window, allowing retrieval of user-visible strings for display or processing.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpString"></param>
        /// <param name="nMaxCount"></param>
        /// <returns></returns>
        [DllImport(_user32, CharSet = CharSet.Unicode)]
        public static extern int GetWindowTextW(IntPtr hWnd, System.Text.StringBuilder lpString, int nMaxCount);

        /// <summary>
        /// Begins the painting process for a specified window by preparing the device context and returning a structure containing information about the painting area.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpPaint"></param>
        /// <returns></returns>
        [DllImport(_user32)]
        public static extern IntPtr BeginPaint(IntPtr hWnd, out PAINTSTRUCT lpPaint);

        /// <summary>
        /// Ends the painting process for a specified window, releasing the device context and finalizing any drawing operations that were performed during the painting session.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpPaint"></param>
        /// <returns></returns>
        [DllImport(_user32)]
        public static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT lpPaint);

        /// <summary>
        /// Gets a property associated with a specified window, allowing retrieval of custom data or state information that has been set for the window.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpString"></param>
        /// <returns></returns>
        [DllImport(_user32, CharSet = CharSet.Unicode)]
        public static extern IntPtr GetProp(IntPtr hWnd, string lpString);

        /// <summary>
        /// Gets a handle to the parent window of a specified child window, allowing navigation of the window hierarchy and retrieval of related windows.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport(_user32)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        /// <summary>
        /// Tracks mouse events for a specified window, allowing the application to receive notifications when the mouse enters, leaves, or hovers over the window.
        /// </summary>
        /// <param name="lpEventTrack"></param>
        /// <returns></returns>
        [DllImport(_user32)]
        public static extern IntPtr TrackMouseEvent2(ref TRACKMOUSEEVENT lpEventTrack);

        [DllImport(_user32, EntryPoint = "TrackMouseEvent")]
        public static extern bool TrackMouseEvent(ref TRACKMOUSEEVENT lpEventTrack);

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
        [DllImport(_user32)]
        public static extern uint GetSysColor(int nIndex);

        /// <summary>
        /// Registers a window message with the system and returns a unique message identifier that can be used for
        /// inter-process communication.
        /// </summary>
        /// <remarks>Use this method to define custom window messages for communication between different
        /// applications or components. The message name should be unique across the system to prevent unintended
        /// message handling. If the specified message is already registered, the existing identifier is
        /// returned.</remarks>
        /// <param name="lpString">The name of the message to register. This string should be unique to avoid conflicts with other registered
        /// messages.</param>
        /// <returns>A message identifier that can be used to send or receive the registered message. Returns zero if the
        /// function fails.</returns>
        [DllImport(_user32, SetLastError = true)]
        public static extern uint RegisterWindowMessage(string lpString);

        /// <summary>
        /// Retrieves a handle to the top-level window whose class name and window name match the specified strings.
        /// </summary>
        /// <remarks>This method is a managed definition of the native FindWindow function in user32.dll.
        /// If the function fails, call <see cref="System.Runtime.InteropServices.Marshal.GetLastWin32Error"/> to obtain
        /// the error code. The search is case-sensitive and only matches top-level windows.</remarks>
        /// <param name="lpClassName">The class name of the window to search for. Specify <see langword="null"/> to match any class name.</param>
        /// <param name="lpWindowName">The window name (title) to search for. Specify <see langword="null"/> to match any window name.</param>
        /// <returns>An <see cref="IntPtr"/> that represents the handle to the window if found; otherwise, <see
        /// cref="IntPtr.Zero"/> if no matching window is found.</returns>
        [DllImport(_user32, SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// Sends the specified message to a window or windows by calling the window procedure for the given window
        /// handle.
        /// </summary>
        /// <remarks>This method is a managed definition of the native SendMessage function from
        /// user32.dll. It sends the message and waits for the window procedure to process it before returning. For
        /// certain messages, this call may block until the message is processed. Use caution when sending messages that
        /// may cause the receiving window to perform lengthy operations.</remarks>
        /// <param name="hWnd">A handle to the window whose window procedure will receive the message. If this parameter is IntPtr.Zero,
        /// the message is sent to all top-level windows.</param>
        /// <param name="Msg">The message to be sent. This value determines the action to be performed and must be a valid window message
        /// identifier.</param>
        /// <param name="wParam">Additional message-specific information. The interpretation of this parameter depends on the value of the
        /// Msg parameter.</param>
        /// <param name="lParam">Additional message-specific information. The interpretation of this parameter depends on the value of the
        /// Msg parameter.</param>
        /// <returns>The result of the message processing, which depends on the message sent. If the function fails, the return
        /// value is IntPtr.Zero.</returns>
        [DllImport(_user32, SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Sends the specified message to a window or windows by calling the window procedure for the given window
        /// handle.
        /// </summary>
        /// <remarks>This method is a managed definition of the native SendMessage function from
        /// user32.dll. It sends the message and waits for the window procedure to process it before returning. For
        /// certain messages, this call may block until the message is processed. Use caution when sending messages that
        /// may cause the receiving window to perform lengthy operations.</remarks>
        /// <param name="hWnd">A handle to the window whose window procedure will receive the message. If this parameter is IntPtr.Zero,
        /// the message is sent to all top-level windows.</param>
        /// <param name="Msg">The message to be sent. This value determines the action to be performed and must be a valid window message
        /// identifier.</param>
        /// <param name="wParam">Additional message-specific information. The interpretation of this parameter depends on the value of the
        /// Msg parameter.</param>
        /// <param name="lParam">Additional message-specific information. The interpretation of this parameter depends on the value of the
        /// Msg parameter.</param>
        /// <returns>The result of the message processing, which depends on the message sent. If the function fails, the return
        /// value is IntPtr.Zero.</returns>
        [DllImport(_user32, SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, WindowsMessage Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Sends the specified message to a window or windows by calling the window procedure for the given window
        /// handle.
        /// </summary>
        /// <remarks>This method is a managed definition of the native SendMessage function from
        /// user32.dll. It sends the message and waits for the window procedure to process it before returning. For
        /// certain messages, this call may block until the message is processed. Use caution when sending messages that
        /// may cause the receiving window to perform lengthy operations.</remarks>
        /// <param name="hWnd">A handle to the window whose window procedure will receive the message. If this parameter is IntPtr.Zero,
        /// the message is sent to all top-level windows.</param>
        /// <param name="Msg">The message to be sent. This value determines the action to be performed and must be a valid window message
        /// identifier.</param>
        /// <param name="wParam">Additional message-specific information. The interpretation of this parameter depends on the value of the
        /// Msg parameter.</param>
        /// <param name="lParam">Additional message-specific information. The interpretation of this parameter depends on the value of the
        /// Msg parameter.</param>
        /// <returns>The result of the message processing, which depends on the message sent. If the function fails, the return
        /// value is IntPtr.Zero.</returns>
        [DllImport(_user32, SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, StringBuilder lParam);

        /// <summary>
        /// Posts a message to the message queue of the specified window and returns immediately without waiting for the
        /// window to process the message.
        /// </summary>
        /// <remarks>Unlike SendMessage, PostMessage returns immediately after posting the message to the
        /// message queue, without waiting for the message to be processed. The message is processed asynchronously by
        /// the target window's message loop. If hWnd is set to IntPtr.Zero, the message is posted to all top-level
        /// windows in the system.</remarks>
        /// <param name="hWnd">A handle to the window whose message queue will receive the message. This can be a handle to a top-level
        /// window or a child window.</param>
        /// <param name="Msg">The message identifier to be posted. This value must be one of the message constants defined by the Windows
        /// API, such as WM_CLOSE or WM_SETTEXT.</param>
        /// <param name="wParam">Additional message-specific information. The interpretation of this parameter depends on the value of the
        /// Msg parameter.</param>
        /// <param name="lParam">Additional message-specific information. The interpretation of this parameter also depends on the value of
        /// the Msg parameter.</param>
        /// <returns>true if the function succeeds; otherwise, false. To obtain extended error information, call
        /// Marshal.GetLastWin32Error.</returns>
        [DllImport(_user32, SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Posts a message to the message queue of the specified window and returns immediately without waiting for the
        /// window to process the message.
        /// </summary>
        /// <remarks>Unlike SendMessage, PostMessage returns immediately after posting the message to the
        /// message queue, without waiting for the message to be processed. The message is processed asynchronously by
        /// the target window's message loop. If hWnd is set to IntPtr.Zero, the message is posted to all top-level
        /// windows in the system.</remarks>
        /// <param name="hWnd">A handle to the window whose message queue will receive the message. This can be a handle to a top-level
        /// window or a child window.</param>
        /// <param name="Msg">The message identifier to be posted. This value must be one of the message constants defined by the Windows
        /// API, such as WM_CLOSE or WM_SETTEXT.</param>
        /// <param name="wParam">Additional message-specific information. The interpretation of this parameter depends on the value of the
        /// Msg parameter.</param>
        /// <param name="lParam">Additional message-specific information. The interpretation of this parameter also depends on the value of
        /// the Msg parameter.</param>
        /// <returns>true if the function succeeds; otherwise, false. To obtain extended error information, call
        /// Marshal.GetLastWin32Error.</returns>
        [DllImport(_user32, SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, WindowsMessage Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Sets the composition attribute for a specified window, enabling visual effects such as transparency or blur.
        /// </summary>
        /// <remarks>This method is typically used to modify the visual appearance of a window in desktop
        /// applications. The window must be created with the appropriate styles to support composition effects. This
        /// function is intended for advanced scenarios and may not be supported on all versions of Windows.</remarks>
        /// <param name="hwnd">A handle to the window whose composition attribute is to be set. This must be a valid window handle.</param>
        /// <param name="data">A reference to a structure that specifies the composition attribute to set and its associated data.</param>
        /// <returns>An integer value that indicates whether the operation succeeded. Returns zero if the operation fails;
        /// otherwise, returns a nonzero value.</returns>
        [DllImport(_user32)]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        /// <summary>
        /// Retrieves information about the specified window (32-bit version).
        /// </summary>
        /// <param name="hWnd">A handle to the window whose information is to be retrieved.</param>
        /// <param name="nIndex">The zero-based offset to the value to be retrieved, such as GWL_STYLE or GWL_EXSTYLE.</param>
        /// <returns>The requested window attribute as an integer. Returns zero on failure.</returns>
        [DllImport(_user32, EntryPoint = "GetWindowLong")]
        private static extern int GetWindowLong32(IntPtr hWnd, int nIndex);

        /// <summary>
        /// Retrieves information about the specified window (64-bit version).
        /// </summary>
        /// <param name="hWnd">A handle to the window whose information is to be retrieved.</param>
        /// <param name="nIndex">The zero-based offset to the value to be retrieved, such as GWL_STYLE or GWL_EXSTYLE.</param>
        /// <returns>The requested window attribute as an <see cref="IntPtr"/>. Returns zero on failure.</returns>
        [DllImport(_user32, EntryPoint = "GetWindowLongPtr")]
        private static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

        /// <summary>
        /// Retrieves information about the specified window attribute, automatically selecting the correct
        /// API based on whether the current process is 32-bit or 64-bit.
        /// </summary>
        /// <remarks>
        /// Calls <see cref="GetWindowLongPtr64"/> on 64-bit processes and <see cref="GetWindowLong32"/>
        /// on 32-bit processes. Common values for <paramref name="nIndex"/> include GWL_STYLE and GWL_EXSTYLE.
        /// </remarks>
        /// <param name="hWnd">A handle to the window whose attribute is to be retrieved.</param>
        /// <param name="nIndex">The zero-based offset of the value to retrieve.</param>
        /// <returns>A <see langword="long"/> representing the requested window attribute.</returns>
        public static long GetWindowLong(IntPtr hWnd, int nIndex)
        {
            if (Environment.Is64BitProcess) return GetWindowLongPtr64(hWnd, nIndex).ToInt64();
            else return GetWindowLong32(hWnd, nIndex);
        }

        /// <summary>
        /// Sets a new value for the specified window attribute, automatically selecting the correct
        /// API based on whether the current process is 32-bit or 64-bit.
        /// </summary>
        /// <remarks>
        /// Calls <see cref="SetWindowLongPtr64"/> on 64-bit processes and <see cref="SetWindowLong32"/>
        /// on 32-bit processes. This is the correct way to subclass a window procedure (WNDPROC) on
        /// both architectures by passing <see cref="GWL_WNDPROC"/> as <paramref name="nIndex"/>.
        /// </remarks>
        /// <param name="hWnd">A handle to the window whose attribute is to be modified.</param>
        /// <param name="nIndex">The zero-based offset of the attribute to set, such as GWL_WNDPROC or GWL_EXSTYLE.</param>
        /// <param name="dwNewLong">The new value to assign to the specified window attribute.</param>
        /// <returns>The previous value of the specified attribute as an <see cref="IntPtr"/>. Returns zero on failure.</returns>
        public static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (Environment.Is64BitProcess) return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            else return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
        }

        /// <summary>
        /// Sets a new value for the specified window attribute (64-bit version).
        /// </summary>
        /// <param name="hWnd">A handle to the window whose attribute is to be modified.</param>
        /// <param name="nIndex">The zero-based offset of the attribute to set, such as GWL_STYLE or GWL_EXSTYLE.</param>
        /// <param name="dwNewLong">The new value to assign to the specified window attribute.</param>
        /// <returns>The previous value of the specified attribute as an <see cref="IntPtr"/>. Returns zero on failure.</returns>
        [DllImport(_user32, EntryPoint = "SetWindowLongPtr", SetLastError = true)]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        /// <summary>
        /// Sets a new value for the specified window attribute (32-bit version).
        /// </summary>
        /// <param name="hWnd">A handle to the window whose attribute is to be modified.</param>
        /// <param name="nIndex">The zero-based offset of the attribute to set, such as GWL_STYLE or GWL_EXSTYLE.</param>
        /// <param name="dwNewLong">The new value to assign to the specified window attribute.</param>
        /// <returns>The previous value of the specified attribute. Returns zero on failure.</returns>
        [DllImport(_user32, EntryPoint = "SetWindowLong", SetLastError = true)]
        private static extern int SetWindowLong32(IntPtr hWnd, int nIndex, int dwNewLong);

        /// <summary>
        /// Sets a specified attribute or property for a window identified by its handle, accepting a
        /// <see langword="long"/> value for convenience.
        /// </summary>
        /// <remarks>
        /// Calls <see cref="SetWindowLongPtr64"/> on 64-bit processes and <see cref="SetWindowLong32"/>
        /// on 32-bit processes. Use this overload when setting style flags such as WS_EX_COMPOSITED
        /// via GWL_EXSTYLE, where the value is most naturally expressed as a <see langword="long"/>.
        /// </remarks>
        /// <param name="hWnd">A handle to the window whose attribute is to be set.</param>
        /// <param name="nIndex">The zero-based offset of the attribute to set, such as GWL_STYLE or GWL_EXSTYLE.</param>
        /// <param name="value">The new value to assign to the specified window attribute.</param>
        public static void SetWindowLong(IntPtr hWnd, int nIndex, long value)
        {
            if (Environment.Is64BitProcess) SetWindowLongPtr64(hWnd, nIndex, new IntPtr(value));
            else SetWindowLong32(hWnd, nIndex, (int)value);
        }

        /// <summary>
        /// Retrieves a handle to the system menu for the specified window.
        /// </summary>
        /// <remarks>The system menu is the menu that appears when the user clicks the window's title bar
        /// icon or presses Alt+Space. This function can be used to modify or restore the system menu. Modifying the
        /// menu affects only the specified window.</remarks>
        /// <param name="hWnd">A handle to the window whose system menu is to be retrieved. This handle must refer to a window that has a
        /// system menu.</param>
        /// <param name="bRevert">true to reset the window's system menu to its default state; false to retrieve the current system menu.</param>
        /// <returns>A handle to the system menu of the specified window. Returns IntPtr.Zero if the window does not have a
        /// system menu.</returns>
        [DllImport(_user32)]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        /// <summary>
        /// Enables, disables, or grays a menu item in the specified menu.
        /// </summary>
        /// <remarks>This method is a platform invoke (P/Invoke) signature for the native EnableMenuItem
        /// function in user32.dll. Ensure that the menu handle and item identifier are valid. Modifying menu items
        /// dynamically can be useful for reflecting application state in the user interface.</remarks>
        /// <param name="hMenu">A handle to the menu that contains the menu item to be modified.</param>
        /// <param name="uIDEnableItem">The identifier or position of the menu item to be changed, depending on the value of uEnable.</param>
        /// <param name="uEnable">Specifies the action to take on the menu item. Use MF_ENABLED to enable, MF_DISABLED to disable, or
        /// MF_GRAYED to gray the item. This parameter can also include MF_BYCOMMAND or MF_BYPOSITION to indicate how
        /// uIDEnableItem is interpreted.</param>
        /// <returns>true if the function succeeds; otherwise, false.</returns>
        [DllImport(_user32)]
        public static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

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
        [DllImport(_user32, SetLastError = true)]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        /// <summary>
        /// Loads the specified cursor resource from a File.
        /// </summary>
        [DllImport(_user32, EntryPoint = "LoadCursorFromFileA")]
        public static extern IntPtr LoadCursorFromFile(string lpFileName);

        /// <summary>
        /// Sends the specified message to a window or windows.
        /// </summary>
        [DllImport(_user32)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Sends the specified message to a window or windows.
        /// </summary>
        [DllImport(_user32, SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport(_user32, CharSet = CharSet.Auto)]
        public static extern IntPtr DefWindowProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

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
        [DllImport(_user32)]
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
        [DllImport(_user32)]
        public static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);

        /// <summary>
        /// Updates the client area of the specified window by sending a <c>WM_PAINT</c> message to the window procedure.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport(_user32)]
        public static extern bool UpdateWindow(IntPtr hWnd);

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
        [DllImport(_user32)]
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
        [DllImport(_user32, SetLastError = true)]
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
        [DllImport(_user32)]
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
        /// Gets the identifier of the control associated with the specified window handle.
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        [DllImport(_user32, SetLastError = true)]
        public static extern int GetDlgCtrlID(IntPtr hwnd);

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
        [DllImport(_user32)]
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
        [DllImport(_user32)]
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
        [DllImport(_user32)]
        public static extern uint SetBkColor(IntPtr hdc, int crColor);

        /// <summary>
        /// Gets a handle to a brush that corresponds to the specified system color index.
        /// </summary>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        [DllImport(_user32)]
        public static extern IntPtr GetSysColorBrush(int nIndex);

        /// <summary>
        /// Fills a rectangle in the specified device context with the specified brush.
        /// </summary>
        /// <param name="hDC"></param>
        /// <param name="lprc"></param>
        /// <param name="hbr"></param>
        /// <returns></returns>
        [DllImport(_user32, SetLastError = true)]
        public static extern int FillRect(IntPtr hDC, [In] ref UxTheme.RECT lprc, IntPtr hbr);

        /// <summary>
        /// Draws formatted text in the specified rectangle of a device context.
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="lpchText"></param>
        /// <param name="nCount"></param>
        /// <param name="lpRect"></param>
        /// <param name="uFormat"></param>
        /// <returns></returns>
        [DllImport(_user32, CharSet = CharSet.Unicode)]
        public static extern int DrawText(IntPtr hdc, string lpchText, int nCount, ref UxTheme.RECT lpRect, uint uFormat);

        /// <summary>
        /// Gets the text of the specified window's title bar (if it has one) or the text of a control.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpString"></param>
        /// <param name="nMaxCount"></param>
        /// <returns></returns>
        [DllImport(_user32, CharSet = CharSet.Unicode)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        /// <summary>
        /// Gets the length, in characters, of the specified window's title bar text (if the window has a title bar). If the specified window is a control, the return value is the length of the text within the control.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport(_user32)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

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
        [DllImport(_user32, SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        /// <summary>
        /// Gets if a hwnd is a window or not.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport(_user32, SetLastError = false)]
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
        [DllImport(_user32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        /// <summary>
        /// Sets a hook procedure that monitors low-level mouse input events. This delegate is used with the SetWindowsHookEx function to install a hook that can intercept mouse events before they reach the target window or application.
        /// </summary>
        /// <param name="idHook"></param>
        /// <param name="lpfn"></param>
        /// <param name="hMod"></param>
        /// <param name="dwThreadId"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        /// <summary>
        /// UnhookWindowsHookEx function: Removes a hook procedure installed in a hook chain by the SetWindowsHookEx function.
        /// </summary>
        /// <param name="hhk">A handle to the hook to be removed.</param>
        /// <returns>If the function succeeds, the return value is true.
        /// If the function fails, the return value is false.</returns>
        [DllImport(_user32, CharSet = CharSet.Auto, SetLastError = true)]
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
        [DllImport(_user32, CharSet = CharSet.Auto, SetLastError = true)]
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
        [DllImport(_user32, SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageTimeout(IntPtr hWnd, uint Msg, UIntPtr wParam, string lParam, uint fuFlags, uint uTimeout, out UIntPtr lpdwResult);

        [DllImport(_user32, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageTimeout(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam, uint fuFlags, uint uTimeout, out IntPtr lpdwResult);

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
        [DllImport(_user32, SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageTimeout(IntPtr hWnd, WindowsMessage Msg, UIntPtr wParam, string lParam, uint fuFlags, uint uTimeout, out UIntPtr lpdwResult);

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
            SendMessageTimeout(new IntPtr(HWND_BROADCAST), WindowsMessage.SettingChange, UIntPtr.Zero, section, SMTO_ABORTIFHUNG, 100, out _);
        }

        /// <summary>
        /// Releases the mouse capture from a window in the current thread.
        /// </summary>
        /// <remarks>This method is a wrapper for the native <c>ReleaseCapture</c> function in the Windows
        /// API.  It is typically used to release mouse capture when a window has captured the mouse input.</remarks>
        /// <returns><see langword="true"/> if the mouse capture was successfully released; otherwise, <see langword="false"/>.</returns>
        [DllImport(_user32)]
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
        [DllImport(_user32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DrawIconEx(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon, int cxWidth, int cyHeight, int istepIfAniCur, IntPtr hbrFlickerFreeDraw, int diFlags);

        /// <summary>
        /// Sets the display affinity setting for a window, which determines how the window's content is displayed on the screen.
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="affinity"></param>
        /// <returns></returns>
        [DllImport(_user32)]
        public static extern bool SetWindowDisplayAffinity(IntPtr hwnd, uint affinity);

        /// <summary>
        /// Creates an overlapped, pop-up, or child window with an extended window style.
        /// </summary>
        /// <param name="dwExStyle"></param>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <param name="dwStyle"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="nWidth"></param>
        /// <param name="nHeight"></param>
        /// <param name="hWndParent"></param>
        /// <param name="hMenu"></param>
        /// <param name="hInstance"></param>
        /// <param name="lpParam"></param>
        /// <returns></returns>
        [DllImport(_user32, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr CreateWindowEx(int dwExStyle, string lpClassName, string lpWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

        /// <summary>
        /// Destroys the specified window. The function sends a WM_DESTROY message to the window to be destroyed and then removes the window from the screen.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport(_user32)]
        public static extern bool DestroyWindow(IntPtr hWnd);

        /// <summary>
        /// Gets a handle to a control in the specified dialog box. The control is identified by its control identifier.
        /// </summary>
        /// <param name="hDlg"></param>
        /// <param name="nIDDlgItem"></param>
        /// <returns></returns>
        [DllImport(_user32)]
        public static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);

        /// <summary>
        /// Maps a set of points from a coordinate space relative to one window to a coordinate space relative to another window.
        /// </summary>
        /// <param name="hWndFrom"></param>
        /// <param name="hWndTo"></param>
        /// <param name="lpPoints"></param>
        /// <param name="cPoints"></param>
        /// <returns></returns>
        [DllImport(_user32)]
        public static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, ref RECT lpPoints, uint cPoints);

        /// <summary>
        /// Maps a set of points from a coordinate space relative to one window to a coordinate space relative to another window.
        /// </summary>
        /// <param name="hWndFrom"></param>
        /// <param name="hWndTo"></param>
        /// <param name="lpPoints"></param>
        /// <param name="cPoints"></param>
        /// <returns></returns>
        [DllImport(_user32)]
        public static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, ref POINT lpPoints, uint cPoints);

        /// <summary>
        /// Moves the specified window to a new position and size. The window is redrawn if the bRepaint parameter is true.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="nWidth"></param>
        /// <param name="nHeight"></param>
        /// <param name="bRepaint"></param>
        /// <returns></returns>
        [DllImport(_user32)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        /// <summary>
        /// Sets the keyboard focus to the specified window. The window must be attached to the calling thread's message queue.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport(_user32)]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        /// <summary>
        /// Sets the parent window of the specified child window. The child window is removed from its current parent and added to the new parent.
        /// </summary>
        /// <param name="hWndChild"></param>
        /// <param name="hWndNewParent"></param>
        /// <returns></returns>
        [DllImport(_user32)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        /// <summary>
        /// Sets a new value for the specified window's long pointer (window data). This function can be used to change window styles, extended styles, or other window-specific data.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nIndex"></param>
        /// <param name="dwNewLong"></param>
        /// <returns></returns>
        [DllImport(_user32)]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        /// <summary>
        /// Gets a handle to the device context (DC) for the entire window, including the non-client area (title bar, borders, scroll bars, etc.). This function can be used to draw on the entire window surface.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport(_user32)]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        /// <summary>
        /// Gets a handle to the window that currently has the keyboard focus. The window must be attached to the calling thread's message queue.
        /// </summary>
        /// <returns></returns>
        [DllImport(_user32)]
        public static extern IntPtr GetFocus();

        /// <summary>
        /// Draws a focus rectangle in the specified device context. The focus rectangle is a dotted rectangle that indicates the current focus of user input.
        /// </summary>
        /// <param name="hDC"></param>
        /// <param name="lprc"></param>
        /// <returns></returns>
        [DllImport(_user32)]
        public static extern bool DrawFocusRect(IntPtr hDC, ref RECT lprc);

        /// <summary>
        /// Gets a handle to the active window associated with the calling thread's message queue. The active window is the window that currently receives user input.
        /// </summary>
        /// <returns></returns>
        [DllImport(_user32, CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetActiveWindow();

        /// <summary>
        /// Creates an IShellItem object from a parsing name (path). This function is used to obtain a shell item interface for a specified file or folder path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [DllImport(_user32, CharSet = CharSet.Unicode, PreserveSig = false)]
        public static extern IShellItem CreateItemFromParsingName(string path);

        /// <summary>
        /// Enables or disables mouse and keyboard input to the specified window. When a window is disabled, it cannot receive input from the user.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="bEnable"></param>
        /// <returns></returns>
        [DllImport(_user32, CharSet = CharSet.Auto, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        /// <summary>
        /// Gets the identifier of the thread that created the specified window and, optionally, the identifier of the process that created the window.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpdwProcessId"></param>
        /// <returns></returns>
        [DllImport(_user32)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        /// <summary>
        /// Determines whether the specified window is enabled for mouse and keyboard input.
        /// </summary>
        /// <param name="hWnd">Handle to the window to test.</param>
        /// <returns>true if the window is enabled; otherwise, false.</returns>
        [DllImport(_user32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowEnabled(IntPtr hWnd);

        /// <summary>
        /// Retrieves information about a window class, including a handle to the small icon 
        /// associated with the window class. The GetClassInfoEx function does not retrieve 
        /// the class extra bytes or the window extra bytes.
        /// </summary>
        /// <param name="hInstance">A handle to the instance of the application that created the class.</param>
        /// <param name="lpClassName">The class name.</param>/// <param name="lpwcx">A pointer to a WNDCLASSEX structure that receives the class information.</param>
        /// <returns>true if successful; otherwise, false.</returns>
        [DllImport(_user32, SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetClassInfoEx(IntPtr hInstance, string lpClassName, ref WNDCLASSEX lpwcx);

        /// <summary>
        /// RedrawWindow flags: Invalidates the window, causing it to be redrawn. This flag is used with the RedrawWindow function to specify that the entire window should be invalidated and repainted.
        /// </summary>
        public const uint RDW_INVALIDATE = 0x0001;

        /// <summary>
        /// RedrawWindow flags: Causes the non-client area (frame) of the window to be redrawn. This flag is used with the RedrawWindow function to specify that the window's frame should be repainted.
        /// </summary>
        public const uint RDW_FRAME = 0x0400;

        /// <summary>
        /// RedrawWindow flags: Causes the window to be updated immediately before the function returns, rather than queuing the update for later. This flag is used with the RedrawWindow function to force an immediate repaint of the window.
        /// </summary>
        public const uint RDW_UPDATENOW = 0x0100;

        /// <summary>
        /// Constant for LoadImage function specifying the type of image to load.
        /// </summary>
        public const uint IMAGE_CURSOR = 2;

        /// <summary>
        /// Specifies that the message should be sent only if the receiving application is responsive.
        /// </summary>
        /// <remarks>This constant is used with the <c>SendMessageTimeout</c> function to indicate that
        /// the operation  should be aborted if the receiving application is not responding.</remarks>
        public const uint SMTO_ABORTIFHUNG = 0x0002;

        /// <summary>
        /// RedrawWindow flag: Causes all child windows of the specified window to be invalidated or updated.
        /// </summary>
        public const int RDW_ALLCHILDREN = 0x0080;

        /// <summary>
        /// SetWindowDisplayAffinity flag: Removes any display affinity setting, allowing the window to be captured
        /// or displayed normally.
        /// </summary>
        public const uint WDA_NONE = 0x00000000;

        /// <summary>
        /// SetWindowDisplayAffinity flag: Prevents the window from being displayed in screen capture or mirroring.
        /// The window content appears black in captures.
        /// </summary>
        public const uint WDA_MONITOR = 0x00000001;

        /// <summary>
        /// SetWindowDisplayAffinity flag: Excludes the window from screen capture while allowing the window
        /// to be displayed normally. Includes WDA_MONITOR behavior combined with capture exclusion.
        /// </summary>
        public const uint WDA_EXCLUDEFROMCAPTURE = 0x00000011;

        /// <summary>
        /// EM_SETSEL message: Sets the selection range in an edit control. Used to select text programmatically.
        /// </summary>
        public const int EM_SETSEL = 0x00B1;

        /// <summary>
        /// GetAncestor flag: Retrieves the root window of the specified window.
        /// </summary>
        public const uint GA_ROOT = 2;

        /// <summary>
        /// SetLayeredWindowAttributes flag: Uses the alpha value specified to set the opacity of the layered window.
        /// The bAlpha parameter specifies the transparency level (0 = fully transparent, 255 = fully opaque).
        /// </summary>
        public const uint LWA_ALPHA = 0x00000002;

        /// <summary>
        /// SetLayeredWindowAttributes flag: Uses the color key instead of alpha.
        /// </summary>
        public const uint LWA_COLORKEY = 0x1;

        /// <summary>
        /// Track mouse event flags: Request mouse leave notification
        /// </summary>
        public const int TME_LEAVE = 0x00000002;

        /// <summary>
        /// GetClassLongPtr index: Background brush handle
        /// </summary>
        public const int GCLP_HBRBACKGROUND = -10;

        /// <summary>
        /// System color index: Window background color
        /// </summary>
        public const int COLOR_WINDOW = 5;

        /// <summary>
        /// System color index: Button face color
        /// </summary>
        public const int COLOR_BTNFACE = 15;

        /// <summary>
        /// Paint/Print flags: Client area
        /// </summary>
        public const uint PRF_CLIENT = 0x00000004;

        /// <summary>
        /// Default GUI font identifier
        /// </summary>
        public const int DEFAULT_GUI_FONT = 17;

        /// <summary>
        /// EM_GETSEL edit message - Gets the starting and ending character positions of the current selection
        /// </summary>
        public const int EM_GETSEL = 0x00B0;

        /// <summary>
        /// WH_CALLWNDPROC hook type - Installs a hook procedure that monitors messages before they are sent to the target window procedure.
        /// </summary>
        public const int WH_CALLWNDPROC = 4;

        [Flags]
        public enum ShowWindowFlags
        {
            /// <summary>
            /// ShowWindow command: Hides the window and deactivates it.
            /// </summary>
            Hide = 0,

            /// <summary>
            /// Show show window command - Shows the window in its current position and size and activates it.
            /// </summary>
            ShowNoActivate = 4,

            /// <summary>
            /// Show show window command - Shows the window in its current position and size.
            /// </summary>
            Show = 5,

            /// <summary>
            /// ShowWindow command: Shows the window in its current state without activating it.
            /// Preserves the window's position and size.
            /// </summary>
            ShowActivate = 8
        }

        [Flags]
        public enum SetWindowsPosition : int
        {
            /// <summary>
            /// Retains the current window size, ignoring the cx and cy parameters.
            /// </summary>
            NoSize = 0x0001,

            /// <summary>
            /// Retains the current window position, ignoring the x and y parameters.
            /// </summary>
            NoMove = 0x0002,

            /// <summary>
            /// Retains the current Z-order position, ignoring the hWndInsertAfter parameter.
            /// </summary>
            NoZOrder = 0x0004,

            /// <summary>
            /// Does not activate the window. If this flag is not set, the window is activated
            /// and moved to the top of the Z-order.
            /// </summary>
            NoActivate = 0x0010,

            /// <summary>
            /// Sends a WM_NCCALCSIZE message to the window, even if the window's size is not changing.
            /// Used to force recalculation of the non-client area.
            /// </summary>
            FrameChanged = 0x0020,

            /// <summary>
            /// Displays the window (shows it). Used in conjunction with NoActivate and other flags.
            /// </summary>
            ShowWindow = 0x0040,

            /// <summary>
            /// Hides the window. Used in conjunction with NoActivate and other flags.
            /// </summary>
            HideWindow = 0x0080
        }

        [Flags]
        public enum WindowsLongs : int
        {
            /// <summary>
            /// Retrieves the address of the window procedure
            /// </summary>
            WndProc = -4,

            /// <summary>
            /// Window ID
            /// </summary>
            ID = -12,

            /// <summary>
            /// Retrieves or sets the window's style (WS_* flags).
            /// </summary>
            Style = -16,

            /// <summary>
            /// Retrieves or sets the window's extended style (WS_EX_* flags).
            /// </summary>
            ExtendedStyle = -20
        }

        /// <summary>
        /// LVITEM structure: Contains information about a list view item, including its state, text, image, and other attributes. This structure is used with list view controls to retrieve or set item information.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct LVITEM
        {
            public uint mask;
            public int iItem;
            public int iSubItem;
            public uint state;
            public uint stateMask;
            public IntPtr pszText;
            public int cchTextMax;
            public int iImage;
            public IntPtr lParam;
            public int iIndent;
            public int iGroupId;
            public uint cColumns;
            public IntPtr puColumns;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPOS
        {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public int flags;
        }

        /// <summary>
        /// Contains the window class information for the GetClassInfoEx function.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct WNDCLASSEX
        {
            /// <summary>
            /// The size, in bytes, of this structure. Set this member to sizeof(WNDCLASSEX).
            /// </summary>
            public int cbSize;

            /// <summary>
            /// The class styles. Can be a combination of CS_* values.
            /// </summary>
            public uint style;

            /// <summary>
            /// A pointer to the window procedure.
            /// </summary>
            public IntPtr lpfnWndProc;

            /// <summary>
            /// The number of extra bytes to allocate following the window-class structure.
            /// </summary>
            public int cbClsExtra;

            /// <summary>
            /// The number of extra bytes to allocate following the window instance.
            /// </summary>
            public int cbWndExtra;

            /// <summary>
            /// A handle to the class instance.
            /// </summary>
            public IntPtr hInstance;

            /// <summary>
            /// A handle to the class icon.
            /// </summary>
            public IntPtr hIcon;

            /// <summary>
            /// A handle to the class cursor.
            /// </summary>
            public IntPtr hCursor;

            /// <summary>
            /// A handle to the class background brush.
            /// </summary>
            public IntPtr hbrBackground;

            /// <summary>
            /// The menu name.
            /// </summary>
            public string lpszMenuName;

            /// <summary>
            /// The class name.
            /// </summary>
            public string lpszClassName;

            /// <summary>
            /// A handle to the small class icon.
            /// </summary>
            public IntPtr hIconSm;
        }

        /// <summary>
        /// IShellItem interface: Represents a Shell item object, which can be a file, folder, or other item in the Windows Shell namespace. This interface provides methods to retrieve information about the item, bind to handlers, and compare items.
        /// </summary>
        [ComImport]
        [Guid("43826d1e-e718-42ee-bc55-a1e261c37bfe")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IShellItem
        {
            void BindToHandler([In] IntPtr pbc, [In] ref Guid rbhid, [In] ref Guid riid, out IntPtr ppv);
            void GetParent(out IShellItem ppsi);
            void GetDisplayName([In] SIGDN sigdnName, [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);
            void GetAttributes([In] uint sfgaoMask, out uint psfgaoAttribs);
            void Compare([In] IShellItem psi, [In] uint hint, out int piOrder);
        }

        /// <summary>
        /// SIGDN enumeration: Specifies the type of display name to retrieve for a Shell item. This enumeration is used with the IShellItem.GetDisplayName method to indicate how the display name should be formatted or represented.
        /// </summary>
        public enum SIGDN : uint
        {
            SIGDN_FILESYSPATH = 0x80058000,
            SIGDN_URL = 0x80058000
        }

        /// <summary>
        /// DRAWITEMSTRUCT structure: Contains information about an owner-drawn control, including the control type, item ID, item state, device context, and rectangle for drawing. This structure is used in the WM_DRAWITEM message to provide details for custom drawing of controls.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct DRAWITEMSTRUCT
        {
            public uint CtlType;
            public uint CtlID;
            public uint itemID;
            public uint itemAction;
            public uint itemState;
            public IntPtr hwndItem;
            public IntPtr hDC;
            public UxTheme.RECT rcItem;
            public IntPtr itemData;
        }

        /// <summary>
        /// Struct that contains information about the painting process for a window, including the device context, the area to be painted, and flags indicating whether the background should be erased or restored.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct PAINTSTRUCT
        {
            public IntPtr hdc;
            public bool fErase;
            public RECT rcPaint;
            public bool fRestore;
            public bool fIncUpdate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] rgbReserved;
        }

        /// <summary>
        /// CWPSTRUCT structure: Contains information about a window message being processed by the CallWndProc hook procedure. This structure is used to pass message parameters to the hook procedure, allowing it to examine or modify the message before it is dispatched to the target window.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct CWPSTRUCT
        {
            public IntPtr lParam;
            public IntPtr wParam;
            public uint message;
            public IntPtr hwnd;
        }

        /// <summary>
        /// CWPRETSTRUCT structure: Contains information about a window message being processed by the CallWndRetProc hook procedure. This structure is used to pass message parameters and the result of the message processing to the hook procedure, allowing it to examine or modify the message after it has been processed by the target window.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct CWPRETSTRUCT
        {
            public IntPtr lResult;
            public IntPtr lParam;
            public IntPtr wParam;
            public uint message;
            public IntPtr hwnd;
        }

        /// <summary>
        /// Struct that contains information about a mouse event being tracked, including the size of the structure, the flags indicating which events to track, the window handle to track, and the hover time for mouse hover events.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct TRACKMOUSEEVENT
        {
            public int cbSize;
            public int dwFlags;
            public IntPtr hwndTrack;
            public int dwHoverTime;
        }

        /// <summary>
        /// Contains information that an application uses to calculate the size, position, and valid client area of a
        /// window during processing of the WM_NCCALCSIZE message.
        /// </summary>
        /// <remarks>This structure is typically used in custom window frame scenarios to determine how
        /// the client area should be adjusted when the window is resized or its non-client area changes. It is
        /// primarily relevant when handling the WM_NCCALCSIZE message in window procedure implementations.</remarks>
        [StructLayout(LayoutKind.Sequential)]
        public struct NCCALCSIZE_PARAMS
        {
            public RECT rgrc0;  // proposed → new client rect (in/out)
            public RECT rgrc1;  // previous window rect
            public RECT rgrc2;  // previous client rect
            public IntPtr lppos;
        }

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
        /// Contains scroll bar parameters used by Windows API functions to retrieve or set scroll bar information.
        /// </summary>
        /// <remarks>The SCROLLINFO structure is typically used with native Windows API calls to specify
        /// or receive information about a scroll bar's range, page size, position, and tracking position. All fields
        /// must be initialized appropriately before passing the structure to API functions. The cbSize field must be
        /// set to the size of the structure in bytes.</remarks>
        [StructLayout(LayoutKind.Sequential)]
        public struct SCROLLINFO
        {
            public int cbSize;
            public int fMask;
            public int nMin;
            public int nMax;
            public int nPage;
            public int nPos;
            public int nTrackPos;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT(int x, int y)
        {
            public int X = x;
            public int Y = y;
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
            /// A LOGFONT structure that defines the font of the caption or title bar.
            /// </summary>
            public LOGFONT lfCaptionFont;

            /// <summary>
            /// The width of the small caption, in pixels.
            /// </summary>
            public int iSMCaptionWidth;

            /// <summary>
            /// The height of the small caption, in pixels.
            /// </summary>
            public int iSMCaptionHeight;

            /// <summary>
            /// A LOGFONT structure that defines the font of the small caption.
            /// </summary>
            public LOGFONT lfSMCaptionFont;

            /// <summary>
            /// The width of the menu bar, in pixels.
            /// </summary>
            public int iMenuWidth;

            /// <summary>
            /// The height of the menu bar, in pixels.
            /// </summary>
            public int iMenuHeight;

            /// <summary>
            /// A LOGFONT structure that defines the font of the menu bar.
            /// </summary>
            public LOGFONT lfMenuFont;

            /// <summary>
            /// A LOGFONT structure that defines the font of the status bar.
            /// </summary>
            public LOGFONT lfStatusFont;

            /// <summary>
            /// A LOGFONT structure that defines the font of the message box window.
            /// </summary>
            public LOGFONT lfMessageFont;

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
                int logFontSize = 60; //Marshal.SizeOf(typeof(LOGFONT));
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
            /// A LOGFONT structure that defines the font of the icon label.
            /// </summary>
            public LOGFONT lfFont;

            /// <summary>
            /// Convert a byte array to an ICONMETRICS structure.
            /// </summary>
            /// <param name="bytes"></param>
            public ICONMETRICS(byte[] bytes)
            {
                int intSize = sizeof(int);
                int uintSize = sizeof(uint);
                int logFontSize = 60; //Marshal.SizeOf(typeof(LOGFONT));
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

        [Flags]
        public enum RedrawWindowFlags : uint
        {
            /// <summary>
            /// Invalidates the specified rectangle or region. If the hRgn parameter is NULL, 
            /// the entire window is invalidated.
            /// </summary>
            Invalidate = 0x0001,

            /// <summary>
            /// Causes a WM_PAINT message to be posted to the window regardless of whether any 
            /// portion of the window is invalid.
            /// </summary>
            InternalPaint = 0x0002,

            /// <summary>
            /// Causes the window to receive a WM_ERASEBKGND message when the window is repainted.
            /// The Erase flag must be used with the Invalidate or InternalPaint flag.
            /// </summary>
            Erase = 0x0004,

            /// <summary>
            /// Validates the specified rectangle or region.
            /// </summary>
            Validate = 0x0008,

            /// <summary>
            /// Suppresses any pending WM_ERASEBKGND messages.
            /// </summary>
            NoErase = 0x0010,

            /// <summary>
            /// Excludes child windows, if any, from the repainting operation.
            /// </summary>
            NoChildren = 0x0040,

            /// <summary>
            /// Includes child windows, if any, in the repainting operation.
            /// </summary>
            AllChildren = 0x0080,

            /// <summary>
            /// Causes the affected windows (as specified by the AllChildren and NoChildren flags) 
            /// to receive WM_NCPAINT, WM_ERASEBKGND, and WM_PAINT messages, if necessary, 
            /// before the function returns.
            /// </summary>
            UpdateNow = 0x0100,

            /// <summary>
            /// Causes the affected windows to receive WM_NCPAINT, WM_ERASEBKGND, and WM_PAINT 
            /// messages after the application or system processes other queued messages.
            /// </summary>
            NoInternalPaint = 0x0200,

            /// <summary>
            /// The window receives a WM_NCPAINT message, allowing you to redraw the 
            /// non-client area (frame/titlebar).
            /// </summary>
            Frame = 0x0400,

            /// <summary>
            /// The window does not receive any internal paint messages.
            /// </summary>
            NoFrame = 0x0800
        }

        /// <summary>
        /// Defines standard Windows message identifiers (WM_*) as a C# enumeration.
        /// <para>
        /// These constants are used in window procedures (WndProc) to handle system-defined messages.
        /// The values are defined in <c>winuser.h</c> and are used for communication between windows and the operating system.
        /// </para>
        /// <para>
        /// <b>Usage:</b> Cast the <c>msg</c> parameter in <c>WndProc</c> to this enum type to compare with known message values.
        /// For example: <c>switch ((WindowsMessage)m.Msg) { case WindowsMessage.Paint: ... }</c>
        /// </para>
        /// <para>
        /// For custom application-defined messages, use values starting from <see cref="WindowsMessage.User"/> (0x0400) or <see cref="WindowsMessage.App"/> (0x8000).
        /// </para>
        /// </summary>
        /// <remarks>
        /// This enum is based on the official Windows SDK definitions from <c>winuser.h</c>.
        /// The numeric values are represented in hexadecimal. For more details on each message,
        /// refer to the Microsoft documentation or <see href="https://pinvoke.net/default.aspx/Constants.WM">pinvoke.net</see>.
        /// </remarks>
        public enum WindowsMessage : uint
        {
            /// <summary>
            /// Performs no operation. Used to post a message that the recipient window will ignore.
            /// </summary>
            Null = 0x0000,

            /// <summary>
            /// Sent when an application requests that a window be created.
            /// <para>
            /// The message is sent before <c>CreateWindowEx</c> or <c>CreateWindow</c> returns,
            /// after the window is created but before it becomes visible.
            /// </para>
            /// </summary>
            Create = 0x0001,

            /// <summary>
            /// Sent when a window is being destroyed. Sent after the window is removed from the screen.
            /// <para>
            /// This message is sent first to the window being destroyed and then to its child windows.
            /// All child windows still exist when this message is processed.
            /// </para>
            /// </summary>
            Destroy = 0x0002,

            /// <summary>
            /// Sent after a window has been moved.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the window's new position. The low-order word is the x-coordinate, and the high-order word is the y-coordinate.</item>
            /// </list>
            /// </para>
            /// </summary>
            Move = 0x0003,

            /// <summary>
            /// Sent to a window after its size has changed.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The type of resizing (e.g., <c>SIZE_RESTORED</c>, <c>SIZE_MINIMIZED</c>, <c>SIZE_MAXIMIZED</c>).</item>
            /// <item><c>lParam</c> - The new width and height of the client area. The low-order word is the width, and the high-order word is the height.</item>
            /// </list>
            /// </para>
            /// </summary>
            Size = 0x0005,

            /// <summary>
            /// Sent when a window is being activated or deactivated.
            /// <para>
            /// Sent first to the window procedure of the top-level window being deactivated,
            /// then to the window procedure of the top-level window being activated.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Specifies whether the window is being activated or deactivated (<c>WA_ACTIVE</c>, <c>WA_CLICKACTIVE</c>, <c>WA_INACTIVE</c>).</item>
            /// <item><c>lParam</c> - Handle to the window being activated or deactivated.</item>
            /// </list>
            /// </para>
            /// </summary>
            Activate = 0x0006,

            /// <summary>
            /// Sent to a window immediately before it receives the keyboard focus.
            /// <para>
            /// Allows the window to prepare for focus changes, such as updating visual cues or state.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the window that has lost the keyboard focus.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            SetFocus = 0x0007,

            /// <summary>
            /// Sent to a window immediately before it loses the keyboard focus.
            /// <para>
            /// Allows the window to perform any necessary cleanup or state updates before losing focus.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the window that will receive the keyboard focus.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            KillFocus = 0x0008,

            /// <summary>
            /// Sent when an application changes the enabled state of a window.
            /// <para>
            /// Sent before the <c>EnableWindow</c> function returns, after the enabled state (<c>WS_DISABLED</c> style bit) has changed.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Indicates whether the window has been enabled (<c>TRUE</c>) or disabled (<c>FALSE</c>).</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            Enable = 0x000A,

            /// <summary>
            /// Sent to a window to allow changes in that window to be redrawn or to prevent changes from being redrawn.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Redraw state (<c>TRUE</c> to enable redrawing, <c>FALSE</c> to disable).</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            SetRedraw = 0x000B,

            /// <summary>
            /// Sent to set the text of a window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Pointer to a null-terminated string containing the new text.</item>
            /// </list>
            /// </para>
            /// </summary>
            SetText = 0x000C,

            /// <summary>
            /// Sent to copy the text that corresponds to a window into a buffer provided by the caller.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The maximum number of characters to copy.</item>
            /// <item><c>lParam</c> - Pointer to the buffer that receives the text.</item>
            /// </list>
            /// </para>
            /// </summary>
            GetText = 0x000D,

            /// <summary>
            /// Sent to determine the length, in characters, of the text associated with a window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            GetTextLength = 0x000E,

            /// <summary>
            /// Sent when a window needs to be repainted. The window should process this message to update its client area.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            Paint = 0x000F,

            /// <summary>
            /// Sent as a signal that a window or an application should terminate.
            /// <para>
            /// An application can handle this message to prompt the user for confirmation or to perform cleanup before closing.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            Close = 0x0010,

            /// <summary>
            /// Sent when the user chooses to end the session or when an application calls a system shutdown function.
            /// <para>
            /// If any application returns zero, the session is not ended.
            /// After processing this message, the system sends the <see cref="EndSession"/> message.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            QueryEndSession = 0x0011,

            /// <summary>
            /// Signals the application to end its message loop.
            /// <para>
            /// Can be generated by Alt+F4, clicking the X button, or selecting File → Exit.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            Quit = 0x0012,

            /// <summary>
            /// Sent to an icon when the user requests that the window be restored to its previous size and position.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            QueryOpen = 0x0013,

            /// <summary>
            /// Sent when the window background must be erased (for example, when a window is resized).
            /// <para>
            /// Used to prepare an invalidated portion of a window for painting.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the device context (DC) for the window.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            EraseBkgnd = 0x0014,

            /// <summary>
            /// Sent to all top-level windows when a system color setting has changed.
            /// <para>
            /// Applications should handle this message to update their color schemes.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            SysColorChange = 0x0015,

            /// <summary>
            /// Sent to an application after the system processes the results of the <see cref="QueryEndSession"/> message.
            /// <para>
            /// Informs the application whether the session is ending.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - <c>TRUE</c> if the session is ending; otherwise <c>FALSE</c>.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            EndSession = 0x0016,

            /// <summary>
            /// Sent to a window when the window is about to be hidden or shown.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - <c>TRUE</c> if the window is being shown; <c>FALSE</c> if being hidden.</item>
            /// <item><c>lParam</c> - The status of the window being shown or hidden.</item>
            /// </list>
            /// </para>
            /// </summary>
            ShowWindow = 0x0018,

            /// <summary>
            /// Sent to all top-level windows after a change to the system settings (e.g., <c>WIN.INI</c>).
            /// <para>
            /// The <c>SystemParametersInfo</c> function sends this message after an application changes a system setting.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Pointer to a string indicating the system parameter that changed, or <c>NULL</c>.</item>
            /// </list>
            /// </para>
            /// </summary>
            SettingChange = 0x001A,

            /// <summary>
            /// Sent to all top-level windows after a change to the <c>WIN.INI</c> file.
            /// <para>
            /// Provided for compatibility with earlier versions. New applications should use <see cref="SettingChange"/>.
            /// </para>
            /// </summary>
            WinIniChange = 0x001A,

            /// <summary>
            /// Sent to all top-level windows whenever the user changes device-mode settings.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            DevModeChange = 0x001B,

            /// <summary>
            /// Sent when a window belonging to a different application than the active window is about to be activated.
            /// <para>
            /// Sent to the application whose window is being activated and to the application whose window is being deactivated.
            /// </para>
            /// </summary>
            ActivateApp = 0x001C,

            /// <summary>
            /// Sent to all top-level windows after the pool of font resources has changed.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            FontChange = 0x001D,

            /// <summary>
            /// Sent whenever there is a change in the system time.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            TimeChange = 0x001E,

            /// <summary>
            /// Sent to cancel certain modes, such as mouse capture.
            /// <para>
            /// For example, sent to the active window when a dialog box or message box is displayed.
            /// </para>
            /// </summary>
            CancelMode = 0x001F,

            /// <summary>
            /// Sent to a window if the mouse causes the cursor to move within a window and mouse input is not captured.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the window that contains the cursor.</item>
            /// <item><c>lParam</c> - The hit-test code.</item>
            /// </list>
            /// </para>
            /// </summary>
            SetCursor = 0x0020,

            /// <summary>
            /// Sent when the cursor is in an inactive window and the user presses a mouse button.
            /// <para>
            /// The parent window receives this message only if the child window passes it to <c>DefWindowProc</c>.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the top-level parent window of the window being activated.</item>
            /// <item><c>lParam</c> - The hit-test code and mouse message identifier.</item>
            /// </list>
            /// </para>
            /// </summary>
            MouseActivate = 0x0021,

            /// <summary>
            /// Sent to a child window when the user clicks the window's title bar or when the window is activated, moved, or sized.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            ChildActivate = 0x0022,

            /// <summary>
            /// Sent by a computer-based training (CBT) application to separate user-input messages from other messages sent through the <c>WH_JOURNALPLAYBACK</c> hook procedure.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            QueueSync = 0x0023,

            /// <summary>
            /// Sent to a window when the size or position of the window is about to change.
            /// <para>
            /// Allows the application to override the window's default maximized size and position, or its default minimum or maximum tracking size.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Pointer to a <c>MINMAXINFO</c> structure.</item>
            /// </list>
            /// </para>
            /// </summary>
            GetMinMaxInfo = 0x0024,

            /// <summary>
            /// Sent to a minimized window when the icon needs to be painted. (Windows NT 3.51 and earlier; not sent by newer versions.)
            /// </summary>
            PaintIcon = 0x0026,

            /// <summary>
            /// Sent to a minimized window when the background of the icon must be filled before painting the icon. (Windows NT 3.51 and earlier; not sent by newer versions.)
            /// </summary>
            IconEraseBkgnd = 0x0027,

            /// <summary>
            /// Sent to a dialog box procedure to set the keyboard focus to a different control in the dialog box.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the control that is to receive the focus, or <c>NULL</c> to use the next control.</item>
            /// <item><c>lParam</c> - Flags indicating how to set the focus.</item>
            /// </list>
            /// </para>
            /// </summary>
            NextDlgCtl = 0x0028,

            /// <summary>
            /// Sent from Print Manager whenever a job is added to or removed from the Print Manager queue.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Job ID.</item>
            /// <item><c>lParam</c> - Status flags.</item>
            /// </list>
            /// </para>
            /// </summary>
            SpoolerStatus = 0x002A,

            /// <summary>
            /// Sent to the parent window of an owner-drawn button, combo box, list box, or menu when a visual aspect of the control or menu has changed.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Control ID.</item>
            /// <item><c>lParam</c> - Pointer to a <c>DRAWITEMSTRUCT</c> structure.</item>
            /// </list>
            /// </para>
            /// </summary>
            DrawItem = 0x002B,

            /// <summary>
            /// Sent to the owner window of a combo box, list box, list view control, or menu item when the control or menu is created.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Control ID.</item>
            /// <item><c>lParam</c> - Pointer to a <c>MEASUREITEMSTRUCT</c> structure.</item>
            /// </list>
            /// </para>
            /// </summary>
            MeasureItem = 0x002C,

            /// <summary>
            /// Sent to the owner of a list box or combo box when the list box or combo box is destroyed or when items are removed.
            /// <para>
            /// Sent for each deleted item with nonzero item data.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Control ID.</item>
            /// <item><c>lParam</c> - Pointer to a <c>DELETEITEMSTRUCT</c> structure.</item>
            /// </list>
            /// </para>
            /// </summary>
            DeleteItem = 0x002D,

            /// <summary>
            /// Sent by a list box with the <c>LBS_WANTKEYBOARDINPUT</c> style to its owner in response to a <see cref="KeyDown"/> message.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Virtual key code.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            VKeyToItem = 0x002E,

            /// <summary>
            /// Sent by a list box with the <c>LBS_WANTKEYBOARDINPUT</c> style to its owner in response to a <see cref="Char"/> message.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Character code.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            CharToItem = 0x002F,

            /// <summary>
            /// Sent to a control to set the font with which it will draw its text.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the font (<c>HFONT</c>).</item>
            /// <item><c>lParam</c> - <c>TRUE</c> if the control should redraw immediately; otherwise <c>FALSE</c>.</item>
            /// </list>
            /// </para>
            /// </summary>
            SetFont = 0x0030,

            /// <summary>
            /// Sent to a control to retrieve the font with which it is drawing its text.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            GetFont = 0x0031,

            /// <summary>
            /// Sent to a window to associate a hot key with the window. When the user presses the hot key, the system activates the window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Virtual key code and modifier flags.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            SetHotKey = 0x0032,

            /// <summary>
            /// Sent to determine the hot key associated with a window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            GetHotKey = 0x0033,

            /// <summary>
            /// Sent to a minimized (iconic) window that is about to be dragged but does not have an icon defined for its class.
            /// <para>
            /// The application can return a handle to an icon or cursor.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            QueryDragIcon = 0x0037,

            /// <summary>
            /// Sent to determine the relative position of a new item in the sorted list of an owner-drawn combo box or list box.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Control ID.</item>
            /// <item><c>lParam</c> - Pointer to a <c>COMPAREITEMSTRUCT</c> structure.</item>
            /// </list>
            /// </para>
            /// </summary>
            CompareItem = 0x0039,

            /// <summary>
            /// Sent by Active Accessibility to obtain information about an accessible object contained in a server application.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Pointer to an <c>OBJID</c> value.</item>
            /// </list>
            /// </para>
            /// </summary>
            GetObject = 0x003D,

            /// <summary>
            /// Sent to all top-level windows when the system detects more than 12.5% of system time over a 30- to 60-second interval is being spent compacting memory.
            /// <para>
            /// Indicates that system memory is low.
            /// </para>
            /// </summary>
            Compacting = 0x0041,

            /// <summary>
            /// Sent when a window is created (internal use).
            /// </summary>
            OtherWindowCreated = 0x0042,

            /// <summary>
            /// Sent when a window is destroyed (internal use).
            /// </summary>
            OtherWindowDestroyed = 0x0043,

            /// <summary>
            /// Sent when communication with a device changes (internal use).
            /// </summary>
            CommNotify = 0x0044,

            /// <summary>
            /// Sent to a window whose size, position, or Z-order is about to change as a result of a call to <c>SetWindowPos</c> or another window-management function.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Pointer to a <c>WINDOWPOS</c> structure.</item>
            /// </list>
            /// </para>
            /// </summary>
            WindowPosChanging = 0x0046,

            /// <summary>
            /// Sent to a window whose size, position, or Z-order has changed as a result of a call to <c>SetWindowPos</c> or another window-management function.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Pointer to a <c>WINDOWPOS</c> structure.</item>
            /// </list>
            /// </para>
            /// </summary>
            WindowPosChanged = 0x0047,

            /// <summary>
            /// Notifies applications that the system is about to enter a suspended mode.
            /// <para>
            /// Obsolete: Use <see cref="PowerBroadcast"/> instead.
            /// </para>
            /// </summary>
            Power = 0x0048,

            /// <summary>
            /// Sent to pass data to another application.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the window passing the data.</item>
            /// <item><c>lParam</c> - Pointer to a <c>COPYDATASTRUCT</c> structure.</item>
            /// </list>
            /// </para>
            /// </summary>
            CopyData = 0x004A,

            /// <summary>
            /// Posted to an application when a user cancels the application's journaling activities.
            /// <para>
            /// Posted with a <c>NULL</c> window handle.
            /// </para>
            /// </summary>
            CancelJournal = 0x004B,

            /// <summary>
            /// Sent by a common control to its parent window when an event has occurred or the control requires some information.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Control ID.</item>
            /// <item><c>lParam</c> - Pointer to an <c>NMHDR</c> structure.</item>
            /// </list>
            /// </para>
            /// </summary>
            Notify = 0x004E,

            /// <summary>
            /// Posted to the window with the focus when the user chooses a new input language.
            /// <para>
            /// An application can accept the change by passing the message to <c>DefWindowProc</c> or reject it by returning immediately.
            /// </para>
            /// </summary>
            InputLangChangeRequest = 0x0050,

            /// <summary>
            /// Sent to the topmost affected window after an application's input language has been changed.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - New input locale identifier.</item>
            /// <item><c>lParam</c> - Previous input locale identifier.</item>
            /// </list>
            /// </para>
            /// </summary>
            InputLangChange = 0x0051,

            /// <summary>
            /// Sent to an application that has initiated a training card with Microsoft Windows Help.
            /// <para>
            /// Informs the application when the user clicks an authorable button.
            /// </para>
            /// </summary>
            TCard = 0x0052,

            /// <summary>
            /// Indicates that the user pressed the F1 key.
            /// <para>
            /// If a menu is active, sent to the window associated with the menu; otherwise, sent to the window with the keyboard focus.
            /// </para>
            /// </summary>
            Help = 0x0053,

            /// <summary>
            /// Sent to all windows after the user has logged on or off.
            /// <para>
            /// The system updates user-specific settings and sends this message immediately after.
            /// </para>
            /// </summary>
            UserChanged = 0x0054,

            /// <summary>
            /// Determines if a window accepts ANSI or Unicode structures in the <see cref="Notify"/> notification message.
            /// <para>
            /// Sent from a common control to its parent window and from the parent window to the common control.
            /// </para>
            /// </summary>
            NotifyFormat = 0x0055,

            /// <summary>
            /// Notifies a window that the user right-clicked in the window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the window.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            ContextMenu = 0x007B,

            /// <summary>
            /// Sent to a window when <c>SetWindowLong</c> is about to change one or more of the window's styles.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The style type (GWL_STYLE or GWL_EXSTYLE).</item>
            /// <item><c>lParam</c> - Pointer to a <c>STYLESTRUCT</c> structure.</item>
            /// </list>
            /// </para>
            /// </summary>
            StyleChanging = 0x007C,

            /// <summary>
            /// Sent to a window after <c>SetWindowLong</c> has changed one or more of the window's styles.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The style type (GWL_STYLE or GWL_EXSTYLE).</item>
            /// <item><c>lParam</c> - Pointer to a <c>STYLESTRUCT</c> structure.</item>
            /// </list>
            /// </para>
            /// </summary>
            StyleChanged = 0x007D,

            /// <summary>
            /// Sent to all windows when the display resolution has changed.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - New bits per pixel.</item>
            /// <item><c>lParam</c> - New width and height. Low-order word is width, high-order word is height.</item>
            /// </list>
            /// </para>
            /// </summary>
            DisplayChange = 0x007E,

            /// <summary>
            /// Sent to a window to retrieve a handle to the large or small icon associated with a window.
            /// <para>
            /// The system displays the large icon in the ALT+TAB dialog and the small icon in the window caption.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Specifies the type of icon (<c>ICON_BIG</c> or <c>ICON_SMALL</c>).</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            GetIcon = 0x007F,

            /// <summary>
            /// Sent to associate a new large or small icon with a window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Specifies the type of icon (<c>ICON_BIG</c> or <c>ICON_SMALL</c>).</item>
            /// <item><c>lParam</c> - Handle to the icon (<c>HICON</c>).</item>
            /// </list>
            /// </para>
            /// </summary>
            SetIcon = 0x0080,

            /// <summary>
            /// Sent prior to the <see cref="Create"/> message when a window is first created.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Pointer to a <c>CREATESTRUCT</c> structure.</item>
            /// </list>
            /// </para>
            /// </summary>
            NCCreate = 0x0081,

            /// <summary>
            /// Informs a window that its non-client area is being destroyed.
            /// <para>
            /// Sent by <c>DestroyWindow</c> following the <see cref="Destroy"/> message.
            /// </para>
            /// </summary>
            NCDestroy = 0x0082,

            /// <summary>
            /// Sent when the size and position of a window's client area must be calculated.
            /// <para>
            /// Allows the application to customize non-client area sizing behavior.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - <c>TRUE</c> if the application should specify the client area; otherwise <c>FALSE</c>.</item>
            /// <item><c>lParam</c> - Pointer to an <c>NCCALCSIZE_PARAMS</c> structure or a <c>RECT</c> structure.</item>
            /// </list>
            /// </para>
            /// </summary>
            NCCalcSize = 0x0083,

            /// <summary>
            /// Sent to a window when the cursor moves, or when a mouse button is pressed or released.
            /// <para>
            /// If the mouse is not captured, the message is sent to the window beneath the cursor.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            NCHitTest = 0x0084,

            /// <summary>
            /// Sent to a window when its non-client area (frame, title bar, scroll bars, etc.) needs to be repainted.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the update region.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            NCPaint = 0x0085,

            /// <summary>
            /// Sent when the non-client area of a window is being activated or deactivated.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - <c>TRUE</c> if the caption is being activated; otherwise <c>FALSE</c>.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            NCActivate = 0x0086,

            /// <summary>
            /// Sent to the window procedure associated with a control to determine how the control handles keyboard input.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            GetDlgCode = 0x0087,

            /// <summary>
            /// Used to synchronize painting while avoiding linking independent GUI threads.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            SyncPaint = 0x0088,

            /// <summary>
            /// Sent to a window when the cursor moves within the non-client area of the window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The hit-test code.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            NCMouseMove = 0x00A0,

            /// <summary>
            /// Posted when the user presses the left mouse button while the cursor is within the non-client area of a window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The hit-test code.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            NCLButtonDown = 0x00A1,

            /// <summary>
            /// Posted when the user releases the left mouse button while the cursor is within the non-client area of a window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The hit-test code.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            NCLButtonUp = 0x00A2,

            /// <summary>
            /// Posted when the user double-clicks the left mouse button while the cursor is within the non-client area of a window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The hit-test code.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            NCLButtonDblClk = 0x00A3,

            /// <summary>
            /// Posted when the user presses the right mouse button while the cursor is within the non-client area of a window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The hit-test code.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            NCRButtonDown = 0x00A4,

            /// <summary>
            /// Posted when the user releases the right mouse button while the cursor is within the non-client area of a window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The hit-test code.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            NCRButtonUp = 0x00A5,

            /// <summary>
            /// Posted when the user double-clicks the right mouse button while the cursor is within the non-client area of a window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The hit-test code.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            NCRButtonDblClk = 0x00A6,

            /// <summary>
            /// Posted when the user presses the middle mouse button while the cursor is within the non-client area of a window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The hit-test code.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            NCMButtonDown = 0x00A7,

            /// <summary>
            /// Posted when the user releases the middle mouse button while the cursor is within the non-client area of a window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The hit-test code.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            NCMButtonUp = 0x00A8,

            /// <summary>
            /// Posted when the user double-clicks the middle mouse button while the cursor is within the non-client area of a window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The hit-test code.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            NCMButtonDblClk = 0x00A9,

            /// <summary>
            /// Posted when the user presses the first or second X button while the cursor is within the non-client area of a window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The hit-test code.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            NCXButtonDown = 0x00AB,

            /// <summary>
            /// Posted when the user releases the first or second X button while the cursor is within the non-client area of a window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The hit-test code.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            NCXButtonUp = 0x00AC,

            /// <summary>
            /// Posted when the user double-clicks the first or second X button while the cursor is within the non-client area of a window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The hit-test code.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            NCXButtonDblClk = 0x00AD,

            /// <summary>
            /// Undocumented message related to themes. Should be handled when handling <see cref="NCPaint"/>.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            NCUAHDRawCaption = 0x00AE,

            /// <summary>
            /// Undocumented message related to themes. Should be handled when handling <see cref="NCPaint"/>.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            NCUAHDRawFrame = 0x00AF,

            /// <summary>
            /// Filters for keyboard messages. The first keyboard message.
            /// </summary>
            KeyFirst = 0x0100,

            /// <summary>
            /// Posted to the window with the keyboard focus when a nonsystem key is pressed.
            /// <para>
            /// A nonsystem key is a key pressed when the ALT key is not pressed.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Virtual key code.</item>
            /// <item><c>lParam</c> - Repeat count, scan code, and other flags.</item>
            /// </list>
            /// </para>
            /// </summary>
            KeyDown = 0x0100,

            /// <summary>
            /// Posted to the window with the keyboard focus when a nonsystem key is released.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Virtual key code.</item>
            /// <item><c>lParam</c> - Repeat count, scan code, and other flags.</item>
            /// </list>
            /// </para>
            /// </summary>
            KeyUp = 0x0101,

            /// <summary>
            /// Posted to the window with the keyboard focus when a <see cref="KeyDown"/> message is translated by <c>TranslateMessage</c>.
            /// <para>
            /// Contains the character code of the key that was pressed.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Character code.</item>
            /// <item><c>lParam</c> - Repeat count, scan code, and other flags.</item>
            /// </list>
            /// </para>
            /// </summary>
            Char = 0x0102,

            /// <summary>
            /// Posted to the window with the keyboard focus when a <see cref="KeyUp"/> message is translated by <c>TranslateMessage</c>.
            /// <para>
            /// Specifies a character code generated by a dead key (e.g., a key that generates a character like the umlaut).
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Character code of the dead key.</item>
            /// <item><c>lParam</c> - Repeat count, scan code, and other flags.</item>
            /// </list>
            /// </para>
            /// </summary>
            DeadChar = 0x0103,

            /// <summary>
            /// Posted to the window with the keyboard focus when the user presses the F10 key or holds down the ALT key and presses another key.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Virtual key code.</item>
            /// <item><c>lParam</c> - Repeat count, scan code, and other flags.</item>
            /// </list>
            /// </para>
            /// </summary>
            SysKeyDown = 0x0104,

            /// <summary>
            /// Posted to the window with the keyboard focus when the user releases a key that was pressed while the ALT key was held down.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Virtual key code.</item>
            /// <item><c>lParam</c> - Repeat count, scan code, and other flags.</item>
            /// </list>
            /// </para>
            /// </summary>
            SysKeyUp = 0x0105,

            /// <summary>
            /// Posted to the window with the keyboard focus when a <see cref="SysKeyDown"/> message is translated by <c>TranslateMessage</c>.
            /// <para>
            /// Specifies the character code of a system character key (pressed while the ALT key is down).
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Character code.</item>
            /// <item><c>lParam</c> - Repeat count, scan code, and other flags.</item>
            /// </list>
            /// </para>
            /// </summary>
            SysChar = 0x0106,

            /// <summary>
            /// Posted to the window with the keyboard focus when a <see cref="SysKeyDown"/> message is translated by <c>TranslateMessage</c>.
            /// <para>
            /// Specifies the character code of a system dead key (pressed while holding down the ALT key).
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Character code of the system dead key.</item>
            /// <item><c>lParam</c> - Repeat count, scan code, and other flags.</item>
            /// </list>
            /// </para>
            /// </summary>
            SysDeadChar = 0x0107,

            /// <summary>
            /// Filters for keyboard messages. The last keyboard message.
            /// </summary>
            KeyLast = 0x0108,

            /// <summary>
            /// Sent immediately before the IME generates the composition string as a result of a keystroke.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            IMEStartComposition = 0x010D,

            /// <summary>
            /// Sent to an application when the IME ends composition.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            IMEEndComposition = 0x010E,

            /// <summary>
            /// Sent to an application when the IME changes composition status as a result of a keystroke.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            IMEComposition = 0x010F,

            /// <summary>
            /// Last IME key message.
            /// </summary>
            IMEKeyLast = 0x010F,

            /// <summary>
            /// Sent to the dialog box procedure immediately before a dialog box is displayed.
            /// <para>
            /// Used to initialize controls and carry out other initialization tasks.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the control that has the default focus.</item>
            /// <item><c>lParam</c> - Additional initialization data.</item>
            /// </list>
            /// </para>
            /// </summary>
            InitDialog = 0x0110,

            /// <summary>
            /// Sent when the user selects a command item from a menu, when a control sends a notification message to its parent window, or when an accelerator keystroke is translated.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The command identifier.</item>
            /// <item><c>lParam</c> - For controls, the low-order word is the control ID, and the high-order word is the notification code.</item>
            /// </list>
            /// </para>
            /// </summary>
            Command = 0x0111,

            /// <summary>
            /// Received when the user chooses a command from the Window menu (system menu) or the maximize, minimize, restore, or close button.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The system command type (e.g., <c>SC_CLOSE</c>, <c>SC_MAXIMIZE</c>).</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            SysCommand = 0x0112,

            /// <summary>
            /// Posted to the installing thread's message queue when a timer expires.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The timer identifier.</item>
            /// <item><c>lParam</c> - Pointer to the timer callback function (if specified).</item>
            /// </list>
            /// </para>
            /// </summary>
            Timer = 0x0113,

            /// <summary>
            /// Sent to a window when a scroll event occurs in the window's standard horizontal scroll bar or the owner of a horizontal scroll bar control.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Scroll code and position.</item>
            /// <item><c>lParam</c> - Handle to the scroll bar control (if applicable).</item>
            /// </list>
            /// </para>
            /// </summary>
            HScroll = 0x0114,

            /// <summary>
            /// Sent to a window when a scroll event occurs in the window's standard vertical scroll bar or the owner of a vertical scroll bar control.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Scroll code and position.</item>
            /// <item><c>lParam</c> - Handle to the scroll bar control (if applicable).</item>
            /// </list>
            /// </para>
            /// </summary>
            VScroll = 0x0115,

            /// <summary>
            /// Sent when a menu is about to become active. Occurs when the user clicks an item on the menu bar or presses a menu key.
            /// <para>
            /// Allows the application to modify the menu before it is displayed.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the menu.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            InitMenu = 0x0116,

            /// <summary>
            /// Sent when a drop-down menu or submenu is about to become active.
            /// <para>
            /// Allows an application to modify the menu before it is displayed, without changing the entire menu.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the drop-down menu or submenu.</item>
            /// <item><c>lParam</c> - The low-order word is the menu index, and the high-order word is the system menu flag.</item>
            /// </list>
            /// </para>
            /// </summary>
            InitMenuPopup = 0x0117,

            /// <summary>
            /// Sent to a menu's owner window when the user selects a menu item.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Menu item information.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            MenuSelect = 0x011F,

            /// <summary>
            /// Sent when a menu is active and the user presses a key that does not correspond to any mnemonic or accelerator key.
            /// <para>
            /// Sent to the window that owns the menu.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Character code.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            MenuChar = 0x0120,

            /// <summary>
            /// Sent to the owner window of a modal dialog box or menu that is entering an idle state.
            /// <para>
            /// A modal dialog box or menu enters an idle state when no messages are waiting in its queue.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            EnterIdle = 0x0121,

            /// <summary>
            /// Sent when the user releases the right mouse button while the cursor is on a menu item.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            MenuRButtonUp = 0x0122,

            /// <summary>
            /// Sent to the owner of a drag-and-drop menu when the user drags a menu item.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            MenuDrag = 0x0123,

            /// <summary>
            /// Sent to the owner of a drag-and-drop menu when the mouse cursor enters a menu item or moves from the center of the item to the top or bottom of the item.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            MenuGetObject = 0x0124,

            /// <summary>
            /// Sent when a drop-down menu or submenu has been destroyed.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            UnInitMenuPopup = 0x0125,

            /// <summary>
            /// Sent when the user makes a selection from a menu.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            MenuCommand = 0x0126,

            /// <summary>
            /// Sent to indicate that the user interface (UI) state should be changed.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Flags indicating the action and the UI element.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            ChangeUIState = 0x0127,

            /// <summary>
            /// Posted to a window when the cursor hovers over the non-client area of the window for the period of time specified in a prior call to <c>TrackMouseEvent</c>.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            NCMouseHover = 0x02A0,

            /// <summary>
            /// Posted to a window when the cursor leaves the non-client area of the window specified in a prior call to <c>TrackMouseEvent</c>.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            NCMouseLeave = 0x02A2,

            /// <summary>
            /// Filters for mouse messages. The first mouse message.
            /// </summary>
            MouseFirst = 0x0200,

            /// <summary>
            /// Posted to a window when the cursor moves.
            /// <para>
            /// If the mouse is not captured, the message is posted to the window that contains the cursor.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            MouseMove = 0x0200,


            MouseLL = 14,

            /// <summary>
            /// Posted when the user presses the left mouse button while the cursor is in the client area of a window.
            /// <para>
            /// If the mouse is not captured, the message is posted to the window beneath the cursor.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Flags indicating the state of the mouse buttons and modifier keys.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            LButtonDown = 0x0201,

            /// <summary>
            /// Posted when the user releases the left mouse button while the cursor is in the client area of a window.
            /// <para>
            /// If the mouse is not captured, the message is posted to the window beneath the cursor.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Flags indicating the state of the mouse buttons and modifier keys.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            LButtonUp = 0x0202,

            /// <summary>
            /// Posted when the user double-clicks the left mouse button while the cursor is in the client area of a window.
            /// <para>
            /// If the mouse is not captured, the message is posted to the window beneath the cursor.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Flags indicating the state of the mouse buttons and modifier keys.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            LButtonDblClk = 0x0203,

            /// <summary>
            /// Posted when the user presses the right mouse button while the cursor is in the client area of a window.
            /// <para>
            /// If the mouse is not captured, the message is posted to the window beneath the cursor.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Flags indicating the state of the mouse buttons and modifier keys.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            RButtonDown = 0x0204,

            /// <summary>
            /// Posted when the user releases the right mouse button while the cursor is in the client area of a window.
            /// <para>
            /// If the mouse is not captured, the message is posted to the window beneath the cursor.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Flags indicating the state of the mouse buttons and modifier keys.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            RButtonUp = 0x0205,

            /// <summary>
            /// Posted when the user double-clicks the right mouse button while the cursor is in the client area of a window.
            /// <para>
            /// If the mouse is not captured, the message is posted to the window beneath the cursor.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Flags indicating the state of the mouse buttons and modifier keys.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            RButtonDblClk = 0x0206,

            /// <summary>
            /// Posted when the user presses the middle mouse button while the cursor is in the client area of a window.
            /// <para>
            /// If the mouse is not captured, the message is posted to the window beneath the cursor.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Flags indicating the state of the mouse buttons and modifier keys.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            MButtonDown = 0x0207,

            /// <summary>
            /// Posted when the user releases the middle mouse button while the cursor is in the client area of a window.
            /// <para>
            /// If the mouse is not captured, the message is posted to the window beneath the cursor.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Flags indicating the state of the mouse buttons and modifier keys.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            MButtonUp = 0x0208,

            /// <summary>
            /// Posted when the user double-clicks the middle mouse button while the cursor is in the client area of a window.
            /// <para>
            /// If the mouse is not captured, the message is posted to the window beneath the cursor.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Flags indicating the state of the mouse buttons and modifier keys.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            MButtonDblClk = 0x0209,

            /// <summary>
            /// Sent to the focus window when the mouse wheel is rotated.
            /// <para>
            /// The <c>DefWindowProc</c> function propagates the message up the parent chain until it finds a window that processes it.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The high-order word indicates the wheel rotation delta; the low-order word is the modifier keys.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            MouseWheel = 0x020A,

            /// <summary>
            /// Posted when the user presses the first or second X button while the cursor is in the client area of a window.
            /// <para>
            /// If the mouse is not captured, the message is posted to the window beneath the cursor.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Flags indicating which X button was pressed and the modifier keys.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            XButtonDown = 0x020B,

            /// <summary>
            /// Posted when the user releases the first or second X button while the cursor is in the client area of a window.
            /// <para>
            /// If the mouse is not captured, the message is posted to the window beneath the cursor.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Flags indicating which X button was released and the modifier keys.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            XButtonUp = 0x020C,

            /// <summary>
            /// Posted when the user double-clicks the first or second X button while the cursor is in the client area of a window.
            /// <para>
            /// If the mouse is not captured, the message is posted to the window beneath the cursor.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Flags indicating which X button was double-clicked and the modifier keys.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            XButtonDblClk = 0x020D,

            /// <summary>
            /// Last mouse message.
            /// </summary>
            MouseLast = 0x020D,

            /// <summary>
            /// Sent to the focus window when the mouse's horizontal scroll wheel is tilted or rotated.
            /// <para>
            /// The <c>DefWindowProc</c> function propagates the message up the parent chain until it finds a window that processes it.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The high-order word indicates the wheel rotation delta; the low-order word is the modifier keys.</item>
            /// <item><c>lParam</c> - The x and y coordinates of the cursor.</item>
            /// </list>
            /// </para>
            /// </summary>
            MouseHWheel = 0x020E,

            /// <summary>
            /// Posted to a window when the cursor hovers over the client area of the window for the period of time specified in a prior call to <c>TrackMouseEvent</c>.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            MouseHover = 0x02A1,

            /// <summary>
            /// Posted to a window when the cursor leaves the client area of the window specified in a prior call to <c>TrackMouseEvent</c>.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            MouseLeave = 0x02A3,

            /// <summary>
            /// Sent to the parent of a child window when the child window is created or destroyed, or when the user clicks a mouse button while the cursor is over the child window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The event code (e.g., <c>WM_PARENTNOTIFY</c>).</item>
            /// <item><c>lParam</c> - Additional event-specific information.</item>
            /// </list>
            /// </para>
            /// </summary>
            ParentNotify = 0x0210,

            /// <summary>
            /// Informs an application's main window procedure that a menu modal loop has been entered.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            EnterMenuLoop = 0x0211,

            /// <summary>
            /// Informs an application's main window procedure that a menu modal loop has been exited.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            ExitMenuLoop = 0x0212,

            /// <summary>
            /// Sent to an application when the right or left arrow key is used to switch between the menu bar and the system menu.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            NextMenu = 0x0213,

            /// <summary>
            /// Sent to a window that the user is resizing. Allows the application to monitor and adjust the size and position of the drag rectangle.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The edge of the window being sized.</item>
            /// <item><c>lParam</c> - Pointer to a <c>RECT</c> structure.</item>
            /// </list>
            /// </para>
            /// </summary>
            Sizing = 0x0214,

            /// <summary>
            /// Sent to the window that is losing the mouse capture.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the window that is gaining the mouse capture.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            CaptureChanged = 0x0215,

            /// <summary>
            /// Sent to a window that the user is moving. Allows the application to monitor the position of the drag rectangle and, if needed, change its position.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Pointer to a <c>RECT</c> structure.</item>
            /// </list>
            /// </para>
            /// </summary>
            Moving = 0x0216,

            /// <summary>
            /// Notifies applications that a power-management event has occurred.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The power event (e.g., <c>PBT_APMQUERYSUSPEND</c>).</item>
            /// <item><c>lParam</c> - Event-specific data.</item>
            /// </list>
            /// </para>
            /// </summary>
            PowerBroadcast = 0x0218,

            /// <summary>
            /// Notifies an application of a change to the hardware configuration of a device or the computer.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The device change event (e.g., <c>DBT_DEVICEARRIVAL</c>).</item>
            /// <item><c>lParam</c> - Pointer to a device event structure.</item>
            /// </list>
            /// </para>
            /// </summary>
            DeviceChange = 0x0219,

            /// <summary>
            /// Sent to a multiple-document interface (MDI) client window to create an MDI child window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Pointer to an <c>MDICREATESTRUCT</c> structure.</item>
            /// </list>
            /// </para>
            /// </summary>
            MDICreate = 0x0220,

            /// <summary>
            /// Sent to a multiple-document interface (MDI) client window to close an MDI child window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the MDI child window to close.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            MDIDestroy = 0x0221,

            /// <summary>
            /// Sent to a multiple-document interface (MDI) client window to activate a different MDI child window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the MDI child window to activate.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            MDIActivate = 0x0222,

            /// <summary>
            /// Sent to a multiple-document interface (MDI) client window to restore an MDI child window from maximized or minimized size.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the MDI child window to restore.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            MDIRestore = 0x0223,

            /// <summary>
            /// Sent to a multiple-document interface (MDI) client window to activate the next or previous child window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the current MDI child window, or <c>NULL</c>.</item>
            /// <item><c>lParam</c> - Flags indicating the direction and type.</item>
            /// </list>
            /// </para>
            /// </summary>
            MDINext = 0x0224,

            /// <summary>
            /// Sent to a multiple-document interface (MDI) client window to maximize an MDI child window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the MDI child window to maximize.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            MDIMaximize = 0x0225,

            /// <summary>
            /// Sent to a multiple-document interface (MDI) client window to arrange all of its MDI child windows in a tile format.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Tiling flags.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            MDITile = 0x0226,

            /// <summary>
            /// Sent to a multiple-document interface (MDI) client window to arrange all its child windows in a cascade format.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            MDICascade = 0x0227,

            /// <summary>
            /// Sent to a multiple-document interface (MDI) client window to arrange all minimized MDI child windows.
            /// <para>
            /// It does not affect child windows that are not minimized.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            MDIIconArrange = 0x0228,

            /// <summary>
            /// Sent to a multiple-document interface (MDI) client window to retrieve the handle to the active MDI child window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            MDIGetActive = 0x0229,

            /// <summary>
            /// Sent to a multiple-document interface (MDI) client window to replace the entire menu of an MDI frame window, to replace the window menu of the frame window, or both.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the new menu.</item>
            /// <item><c>lParam</c> - Handle to the new window menu.</item>
            /// </list>
            /// </para>
            /// </summary>
            MDISetMenu = 0x0230,

            /// <summary>
            /// Sent one time to a window after it enters the moving or sizing modal loop.
            /// <para>
            /// The window enters the moving or sizing modal loop when the user clicks the window's title bar or sizing border, or when the window passes the <see cref="SysCommand"/> message to <c>DefWindowProc</c> with <c>SC_MOVE</c> or <c>SC_SIZE</c>.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            EnterSizeMove = 0x0231,

            /// <summary>
            /// Sent one time to a window after it has exited the moving or sizing modal loop.
            /// <para>
            /// The window enters the moving or sizing modal loop when the user clicks the window's title bar or sizing border, or when the window passes the <see cref="SysCommand"/> message to <c>DefWindowProc</c> with <c>SC_MOVE</c> or <c>SC_SIZE</c>.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            ExitSizeMove = 0x0232,

            /// <summary>
            /// Sent when the user drops a file on the window of an application that has registered itself as a recipient of dropped files.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the drop file structure.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            DropFiles = 0x0233,

            /// <summary>
            /// Sent to a multiple-document interface (MDI) client window to refresh the window menu of the MDI frame window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            MDIRefreshMenu = 0x0234,

            /// <summary>
            /// The first AFX message (used internally by the MFC library).
            /// </summary>
            AfxFirst = 0x0360,

            /// <summary>
            /// The last AFX message (used internally by the MFC library).
            /// </summary>
            AfxLast = 0x037F,

            /// <summary>
            /// The first pen window message.
            /// </summary>
            PenWinFirst = 0x0380,

            /// <summary>
            /// The last pen window message.
            /// </summary>
            PenWinLast = 0x038F,

            /// <summary>
            /// The base value for application-defined private messages.
            /// <para>
            /// Applications can define their own custom messages starting from this value.
            /// Typically used as <c>WM_USER + X</c>.
            /// </para>
            /// </summary>
            User = 0x0400,

            /// <summary>
            /// The base value for application-defined private messages that are guaranteed to be unique across applications.
            /// <para>
            /// Typically used as <c>WM_APP + X</c>.
            /// </para>
            /// </summary>
            App = 0x8000,

            /// <summary>
            /// Sent to the clipboard owner by a clipboard viewer window to request the name of a <c>CF_OWNERDISPLAY</c> clipboard format.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The maximum number of characters to copy.</item>
            /// <item><c>lParam</c> - Pointer to a buffer that receives the format name.</item>
            /// </list>
            /// </para>
            /// </summary>
            AskCBFormatName = 0x030C,

            /// <summary>
            /// Sent to the first window in the clipboard viewer chain when a window is being removed from the chain.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the window being removed.</item>
            /// <item><c>lParam</c> - Handle to the next window in the chain.</item>
            /// </list>
            /// </para>
            /// </summary>
            ChangeCBChain = 0x030D,

            /// <summary>
            /// Sent to an edit control or combo box to delete (clear) the current selection from the edit control.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            Clear = 0x0303,

            /// <summary>
            /// Sent to an edit control or combo box to copy the current selection to the clipboard in <c>CF_TEXT</c> format.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            Copy = 0x0301,

            /// <summary>
            /// Sent to an edit control or combo box to delete (cut) the current selection and copy the deleted text to the clipboard in <c>CF_TEXT</c> format.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            Cut = 0x0300,

            /// <summary>
            /// Sent to the clipboard owner when <c>EmptyClipboard</c> empties the clipboard.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            DestroyClipboard = 0x0307,

            /// <summary>
            /// Sent to the first window in the clipboard viewer chain when the content of the clipboard changes.
            /// <para>
            /// Enables a clipboard viewer window to display the new content of the clipboard.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            DrawClipboard = 0x0308,

            /// <summary>
            /// Sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the <c>CF_OWNERDISPLAY</c> format and the clipboard viewer's client area needs repainting.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the clipboard viewer window.</item>
            /// <item><c>lParam</c> - Pointer to a <c>PAINTSTRUCT</c> structure.</item>
            /// </list>
            /// </para>
            /// </summary>
            PaintClipboard = 0x0309,

            /// <summary>
            /// Sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the <c>CF_OWNERDISPLAY</c> format and an event occurs in the clipboard viewer's horizontal scroll bar.
            /// <para>
            /// The owner should scroll the clipboard image and update the scroll bar values.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The scroll code and position.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            HScrollClipboard = 0x030E,

            /// <summary>
            /// Sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the <c>CF_OWNERDISPLAY</c> format and an event occurs in the clipboard viewer's vertical scroll bar.
            /// <para>
            /// The owner should scroll the clipboard image and update the scroll bar values.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The scroll code and position.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            VScrollClipboard = 0x030A,

            /// <summary>
            /// Sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the <c>CF_OWNERDISPLAY</c> format and the clipboard viewer's client area has changed size.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Pointer to a <c>RECT</c> structure.</item>
            /// </list>
            /// </para>
            /// </summary>
            SizeClipboard = 0x030B,

            /// <summary>
            /// Sent to an edit control or combo box to copy the current content of the clipboard to the edit control at the current caret position.
            /// <para>
            /// Data is inserted only if the clipboard contains data in <c>CF_TEXT</c> format.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            Paste = 0x0302,

            /// <summary>
            /// Sent to the clipboard owner before it is destroyed, if the clipboard owner has delayed rendering one or more clipboard formats.
            /// <para>
            /// The owner must render data in all the formats it is capable of generating and place the data on the clipboard.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            RenderAllFormats = 0x0306,

            /// <summary>
            /// Sent to the clipboard owner if it has delayed rendering a specific clipboard format and an application has requested data in that format.
            /// <para>
            /// The owner must render data in the specified format and place it on the clipboard.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The clipboard format to render.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            RenderFormat = 0x0305,

            /// <summary>
            /// Sent to an edit control to undo the last operation.
            /// <para>
            /// The previously deleted text is restored or the previously added text is deleted.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            Undo = 0x0304,

            /// <summary>
            /// Sent to a window to request that it draw itself in the specified device context, most commonly in a printer device context.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the device context (DC).</item>
            /// <item><c>lParam</c> - Flags specifying the drawing options.</item>
            /// </list>
            /// </para>
            /// </summary>
            Print = 0x0317,

            /// <summary>
            /// Sent to a window to request that it draw its client area in the specified device context, most commonly in a printer device context.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the device context (DC).</item>
            /// <item><c>lParam</c> - Flags specifying the drawing options.</item>
            /// </list>
            /// </para>
            /// </summary>
            PrintClient = 0x0318,

            /// <summary>
            /// Sent by the OS to all top-level and overlapped windows after the window with the keyboard focus realizes its logical palette.
            /// <para>
            /// Enables windows that do not have the keyboard focus to realize their logical palettes and update their client areas.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the window that realized its palette.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            PaletteChanged = 0x0311,

            /// <summary>
            /// Informs applications that an application is going to realize its logical palette.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            PaletteIsChanging = 0x0310,

            /// <summary>
            /// Informs a window that it is about to receive the keyboard focus, giving the window the opportunity to realize its logical palette.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            QueryNewPalette = 0x030F,

            /// <summary>
            /// Sent to the parent window of a message box before Windows draws the message box.
            /// <para>
            /// Allows the owner to set the text and background colors using the given display device context handle.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the device context (DC).</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            CtlColorMsgBox = 0x0132,

            /// <summary>
            /// Sent by an edit control that is not read-only or disabled to its parent window when the control is about to be drawn.
            /// <para>
            /// Allows the parent to set the text and background colors of the edit control.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the device context (DC).</item>
            /// <item><c>lParam</c> - Handle to the edit control.</item>
            /// </list>
            /// </para>
            /// </summary>
            CtlColorEdit = 0x0133,

            /// <summary>
            /// Sent to the parent window of a list box before the system draws the list box.
            /// <para>
            /// Allows the parent to set the text and background colors of the list box.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the device context (DC).</item>
            /// <item><c>lParam</c> - Handle to the list box control.</item>
            /// </list>
            /// </para>
            /// </summary>
            CtlColorListBox = 0x0134,

            /// <summary>
            /// Sent to the parent window of a button before drawing the button.
            /// <para>
            /// Allows the parent to change the button's text and background colors. Only owner-drawn buttons respond to this message.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the device context (DC).</item>
            /// <item><c>lParam</c> - Handle to the button control.</item>
            /// </list>
            /// </para>
            /// </summary>
            CtlColorBtn = 0x0135,

            /// <summary>
            /// Sent to a dialog box before the system draws the dialog box.
            /// <para>
            /// Allows the dialog box to set its text and background colors using the specified display device context handle.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the device context (DC).</item>
            /// <item><c>lParam</c> - Handle to the dialog box.</item>
            /// </list>
            /// </para>
            /// </summary>
            CtlColorDlg = 0x0136,

            /// <summary>
            /// Sent to the parent window of a scroll bar control when the control is about to be drawn.
            /// <para>
            /// Allows the parent to set the background color of the scroll bar control.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the device context (DC).</item>
            /// <item><c>lParam</c> - Handle to the scroll bar control.</item>
            /// </list>
            /// </para>
            /// </summary>
            CtlColorScrollBar = 0x0137,

            /// <summary>
            /// Sent by a static control, or a read-only or disabled edit control, to its parent window when the control is about to be drawn.
            /// <para>
            /// Allows the parent to set the text and background colors of the static control.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Handle to the device context (DC).</item>
            /// <item><c>lParam</c> - Handle to the static control.</item>
            /// </list>
            /// </para>
            /// </summary>
            CtlColorStatic = 0x0138,

            /// <summary>
            /// Sent to the IME window by an application to direct the IME window to carry out the requested command.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The command to execute.</item>
            /// <item><c>lParam</c> - Command-specific data.</item>
            /// </list>
            /// </para>
            /// </summary>
            ImeControl = 0x0283,

            /// <summary>
            /// Sent to an application when the IME window finds no space to extend the area for the composition window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            ImeCompositionFull = 0x0284,

            /// <summary>
            /// Sent to an application when the operating system is about to change the current IME.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - New input locale identifier.</item>
            /// <item><c>lParam</c> - Previous input locale identifier.</item>
            /// </list>
            /// </para>
            /// </summary>
            ImeSelect = 0x0285,

            /// <summary>
            /// Sent to an application when the IME gets a character of the conversion result.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Character code.</item>
            /// <item><c>lParam</c> - Repeat count, scan code, and other flags.</item>
            /// </list>
            /// </para>
            /// </summary>
            ImeChar = 0x0286,

            /// <summary>
            /// Sent to an application to notify it of changes to the IME window.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Notification code.</item>
            /// <item><c>lParam</c> - Notification-specific data.</item>
            /// </list>
            /// </para>
            /// </summary>
            ImeNotify = 0x0282,

            /// <summary>
            /// Sent to all top-level windows when a change in the current visual theme has occurred.
            /// <para>
            /// This message is broadcast by the system when the user switches themes, modifies theme settings,
            /// or when an application changes theme-related system parameters through the <c>SetThemeProperties</c>
            /// function. It provides an opportunity for applications to update their UI elements to match the
            /// new visual style.
            /// </para>
            /// </summary>
            /// <remarks>
            /// <para>
            /// <b>When to handle this message:</b>
            /// Applications that perform custom drawing or use custom controls should handle this message
            /// to refresh their visual appearance. Common responses include:
            /// <list type="bullet">
            /// <item>Invalidating the window to trigger a repaint (<c>InvalidateRect</c>)</item>
            /// <item>Requerying theme metrics (<c>GetThemeMetrics</c>)</item>
            /// <item>Recreating theme-dependent brushes or fonts</item>
            /// <item>Updating custom-drawn controls to reflect new theme colors and sizes</item>
            /// </list>
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used; should be ignored.</item>
            /// <item><c>lParam</c> - Not used; should be ignored.</item>
            /// </list>
            /// </para>
            /// <para>
            /// <b>Default behavior:</b>
            /// If not handled, <c>DefWindowProc</c> does nothing with this message, as it is primarily
            /// a notification for applications to act upon.
            /// </para>
            /// <para>
            /// <b>Related messages:</b>
            /// This message is often processed alongside <see cref="SysColorChange"/> (0x15) and
            /// <see cref="SettingChange"/> (0x001A) when system-wide visual settings are modified.
            /// </para>
            /// <para>
            /// <b>Note:</b> This message is defined in <c>uxtheme.h</c> and is part of the Desktop Window Manager (DWM)
            /// theme API. It was introduced with Windows XP theme support.
            /// </para>
            /// </remarks>
            /// <seealso cref="SysColorChange"/>
            /// <seealso cref="SettingChange"/>
            ThemeChanged = 0x31A,

            /// <summary>
            /// Sent to an application to provide commands and request information.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Request command.</item>
            /// <item><c>lParam</c> - Request-specific data.</item>
            /// </list>
            /// </para>
            /// </summary>
            ImeRequest = 0x0288,

            /// <summary>
            /// Sent to an application when a window is activated.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used.</item>
            /// <item><c>lParam</c> - Not used.</item>
            /// </list>
            /// </para>
            /// </summary>
            ImeSetContext = 0x0281,

            /// <summary>
            /// Sent to an application by the IME to notify the application of a key press and to keep message order.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Virtual key code.</item>
            /// <item><c>lParam</c> - Repeat count, scan code, and other flags.</item>
            /// </list>
            /// </para>
            /// </summary>
            ImeKeyDown = 0x0290,

            /// <summary>
            /// Sent to an application by the IME to notify the application of a key release and to keep message order.
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Virtual key code.</item>
            /// <item><c>lParam</c> - Repeat count, scan code, and other flags.</item>
            /// </list>
            /// </para>
            /// </summary>
            ImeKeyUp = 0x0291,

            /// <summary>
            /// Posted when the user presses a hot key registered by <c>RegisterHotKey</c>.
            /// <para>
            /// The message is placed at the top of the message queue associated with the thread that registered the hot key.
            /// </para>
            /// <para>
            /// <b>Parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - The hot key identifier.</item>
            /// <item><c>lParam</c> - The virtual key code and modifiers.</item>
            /// </list>
            /// </para>
            /// </summary>
            HotKey = 0x0312,

            /// <summary>
            /// Task Dialog notification sent when the expand/collapse button (expando) is clicked by the user.
            /// <para>
            /// This notification is sent to the task dialog callback procedure when the user toggles the
            /// expand/collapse button that controls the visibility of the expanded information area
            /// in a task dialog.
            /// </para>
            /// </summary>
            /// <remarks>
            /// <para>
            /// <b>Usage scenarios:</b>
            /// <list type="bullet">
            /// <item>When a task dialog includes expanded information that can be toggled</item>
            /// <item>To track user interaction with the expando button for analytics</item>
            /// <item>To update the task dialog's expanded state programmatically</item>
            /// <item>To respond to state changes in custom task dialog implementations</item>
            /// </list>
            /// </para>
            /// <para>
            /// <b>Message parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Not used; should be ignored.</item>
            /// <item><c>lParam</c> - Not used; should be ignored.</item>
            /// </list>
            /// </para>
            /// <para>
            /// <b>Callback handling:</b>
            /// When this notification is received in the <c>TaskDialogCallbackProc</c>:
            /// <list type="number">
            /// <item>The task dialog's expanded state has already been toggled</item>
            /// <item>The callback can update any custom UI elements in response</item>
            /// <item>Return <c>S_OK</c> to continue normal processing</item>
            /// <item>Returning <c>E_FAIL</c> prevents further processing</item>
            /// </list>
            /// </para>
            /// <para>
            /// <b>Usage example:</b>
            /// <code>
            /// private int TaskDialogCallback(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam, IntPtr lpRefData)
            /// {
            ///     switch ((TaskDialogNotification)msg)
            ///     {
            ///         case TaskDialogNotification.ExpandoButtonClicked:
            ///             // User toggled the expando button
            ///             bool isExpanded = TaskDialog_GetExpandedState(hwnd);
            ///             UpdateCustomUI(isExpanded);
            ///             break;
            ///     }
            ///     return 0;
            /// }
            /// </code>
            /// </para>
            /// <para>
            /// <b>Related notifications:</b>
            /// <list type="bullet">
            /// <item><see cref="TaskDialogNotification.Navigated"/> - Sent when the task dialog is navigated</item>
            /// <item><see cref="TaskDialogNotification.ButtonClicked"/> - Sent when a button is clicked</item>
            /// <item><see cref="TaskDialogNotification.HyperlinkClicked"/> - Sent when a hyperlink is clicked</item>
            /// </list>
            /// </para>
            /// <para>
            /// <b>Note:</b> This notification is defined in <c>commctrl.h</c> as part of the Task Dialog API
            /// (introduced in Windows Vista). The notification code is <c>TDN_EXPANDO_BUTTON_CLICKED</c>
            /// with a value of 0x1102.
            /// </para>
            /// </remarks>
            /// <seealso cref="TaskDialogNotification.Navigated"/>
            /// <seealso cref="TaskDialogNotification.ButtonClicked"/>
            /// <seealso cref="TaskDialogNotification.HyperlinkClicked"/>
            ExpandoButtonClicked = 0x1102,


            /// <summary>
            /// A custom application-defined message received when DWM composition has changed.
            /// </summary>
            DWMCompositionChanged = 0x31e,

            /// <summary>
            /// A custom application-defined message used to request a refresh of the DirectUI (DUI) state.
            /// <para>
            /// This message can be triggered to force DirectUI-based UI elements to update, redraw,
            /// or synchronize their state with the underlying data model. It is particularly useful
            /// when multiple components need to refresh their visual representation without
            /// causing a full window repaint.
            /// </para>
            /// </summary>
            /// <remarks>
            /// <para>
            /// <b>Usage scenarios:</b>
            /// <list type="bullet">
            /// <item>After data-bound properties change in the view model</item>
            /// <item>When theme or style changes affect DirectUI visual elements</item>
            /// <item>To synchronize multiple DirectUI controls after batch updates</item>
            /// <item>When resuming from a suspended state that may have affected UI state</item>
            /// </list>
            /// </para>
            /// <para>
            /// <b>Message parameters:</b>
            /// <list type="bullet">
            /// <item><c>wParam</c> - Optional parameter; can be used to specify refresh flags or specific element identifiers.</item>
            /// <item><c>lParam</c> - Optional parameter; can be used to pass additional context or a pointer to refresh data.</item>
            /// </list>
            /// </para>
            /// <para>
            /// <b>Recommended handling:</b>
            /// When this message is received, the window procedure should:
            /// <list type="number">
            /// <item>Call <c>Invalidate()</c> on affected DirectUI elements</item>
            /// <item>Rebind any data sources or refresh collections</item>
            /// <item>Update the visual state to reflect current data</item>
            /// <item>Optionally force a layout pass if needed</item>
            /// </list>
            /// </para>
            /// <para>
            /// <b>Scope:</b> This is an application-defined message and can be used
            /// internally across different modules of the application. It is not a system message
            /// and will not be sent by Windows.
            /// </para>
            /// <para>
            /// <b>Value:</b> This message is defined as <c>WM_USER + 4242</c> to avoid conflicts with
            /// other custom messages. The offset 4242 is chosen arbitrarily and can be adjusted
            /// if conflicts arise.
            /// </para>
            /// <para>
            /// <b>Thread safety:</b> This message should be posted or sent from the UI thread
            /// to avoid cross-thread issues with DirectUI rendering.
            /// </para>
            /// </remarks>
            /// <seealso cref="WindowsMessage.User"/>
            /// <seealso cref="WindowsMessage.Paint"/>
            RefreshDuiState = (uint)User + 4242,
        }

        public enum HitTestValues
        {
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTBOTTOM = 15,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17
        }

        /// <summary>
        /// Special window handle value to broadcast the message to all top-level windows.
        /// </summary>
        public const int HWND_BROADCAST = 0xFFFF;

        public delegate bool EnumChildProc(IntPtr hWnd, IntPtr lParam);
    }
}
