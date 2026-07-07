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
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

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
        [DllImport(_user32, EntryPoint = "DestroyIcon")]
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
        /// Refreshes the non-client area of a window, causing its frame and decorations to be recalculated and
        /// repainted.
        /// </summary>
        /// <remarks>Call this method to update the appearance of a window's borders, title bar, and other
        /// non-client elements after changes such as resizing, theme updates, or custom drawing. This method sends
        /// messages to the specified window to recalculate and redraw its non-client area immediately.</remarks>
        /// <param name="handle">The handle to the window whose non-client area is to be refreshed.</param>
        public static void RefreshNonClient(IntPtr handle)
        {
            // Recalculate frame
            SendMessage(handle, WM_NCCALCSIZE, IntPtr.Zero, IntPtr.Zero);

            // Force NC repaint
            SendMessage(handle, WM_NCPAINT, IntPtr.Zero, IntPtr.Zero);

            // Strong redraw
            RedrawWindow(handle, IntPtr.Zero, IntPtr.Zero, RDW_INVALIDATE | RDW_FRAME | RDW_UPDATENOW | RDW_ALLCHILDREN);
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
        public static extern int GetSysColor(int nIndex);

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
        /// Sets a new value for the specified window attribute (32-bit version).
        /// </summary>
        /// <param name="hWnd">A handle to the window whose attribute is to be modified.</param>
        /// <param name="nIndex">The zero-based offset of the attribute to set, such as GWL_STYLE or GWL_EXSTYLE.</param>
        /// <param name="dwNewLong">The new value to assign to the specified window attribute.</param>
        /// <returns>The previous value of the specified attribute. Returns zero on failure.</returns>
        [DllImport(_user32, EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(IntPtr hWnd, int nIndex, int dwNewLong);

        /// <summary>
        /// Retrieves information about the specified window (64-bit version).
        /// </summary>
        /// <param name="hWnd">A handle to the window whose information is to be retrieved.</param>
        /// <param name="nIndex">The zero-based offset to the value to be retrieved, such as GWL_STYLE or GWL_EXSTYLE.</param>
        /// <returns>The requested window attribute as an <see cref="IntPtr"/>. Returns zero on failure.</returns>
        [DllImport(_user32, EntryPoint = "GetWindowLongPtr")]
        private static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

        /// <summary>
        /// Sets a new value for the specified window attribute (64-bit version).
        /// </summary>
        /// <param name="hWnd">A handle to the window whose attribute is to be modified.</param>
        /// <param name="nIndex">The zero-based offset of the attribute to set, such as GWL_STYLE or GWL_EXSTYLE.</param>
        /// <param name="dwNewLong">The new value to assign to the specified window attribute.</param>
        /// <returns>The previous value of the specified attribute as an <see cref="IntPtr"/>. Returns zero on failure.</returns>
        [DllImport(_user32, EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

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
        /// WM_NCCALCSIZE message: Sent when the size and position of a window's client area must be calculated.
        /// Used to customize non-client area sizing behavior.
        /// </summary>
        public const int WM_NCCALCSIZE = 0x0083;

        /// <summary>
        /// WM_NCPAINT message: Sent to a window when its non-client area (frame, title bar, scroll bars, etc.)
        /// needs to be repainted.
        /// </summary>
        public const int WM_NCPAINT = 0x0085;

        /// <summary>
        /// WM_SETFOCUS message: Sent to a window when it is about to receive keyboard focus. This message allows the window to prepare for focus changes, such as updating visual cues or state.
        /// </summary>
        public const uint WM_SETFOCUS = 0x0007;

        /// <summary>
        /// WM_KILLFOCUS message: Sent to a window when it is about to lose keyboard focus. This message allows the window to perform any necessary cleanup or state updates before losing focus.
        /// </summary>
        public const uint WM_KILLFOCUS = 0x0008;

        /// <summary>
        /// WM_USER message: The first message identifier available for application-defined messages. Applications can define their own custom messages starting from this value.
        /// </summary>
        public const uint WM_USER = 0x0400;

        /// <summary>
        /// WM_REFRESH_DUI_STATE message: A custom message defined by the application to request a refresh of the DirectUI state. This message can be used to trigger updates or redraws of UI elements that rely on DirectUI.
        /// </summary>
        public const uint WM_REFRESH_DUI_STATE = WM_USER + 4242;

        /// <summary>
        /// RedrawWindow flag: Causes all child windows of the specified window to be invalidated or updated.
        /// </summary>
        public const int RDW_ALLCHILDREN = 0x0080;

        /// <summary>
        /// SetWindowPos flag: Retains the current window size, ignoring the cx and cy parameters.
        /// </summary>
        public const uint SWP_NOSIZE = 0x0001;

        /// <summary>
        /// SetWindowPos flag: Retains the current window position, ignoring the x and y parameters.
        /// </summary>
        public const uint SWP_NOMOVE = 0x0002;

        /// <summary>
        /// SetWindowPos flag: Retains the current Z-order position, ignoring the hWndInsertAfter parameter.
        /// </summary>
        public const uint SWP_NOZORDER = 0x0004;

        /// <summary>
        /// SetWindowPos flag: Does not activate the window. If this flag is not set, the window is activated
        /// and moved to the top of the Z-order.
        /// </summary>
        public const uint SWP_NOACTIVATE = 0x0010;

        /// <summary>
        /// SetWindowPos flag: Sends a WM_NCCALCSIZE message to the window, even if the window's size is not changing.
        /// Used to force recalculation of the non-client area.
        /// </summary>
        public const uint SWP_FRAMECHANGED = 0x0020;

        /// <summary>
        /// GetWindowLong/SetWindowLong index: Retrieves or sets the window's style (WS_* flags).
        /// </summary>
        public const int GWL_STYLE = -16;

        /// <summary>
        /// GetWindowLong/SetWindowLong index: Retrieves or sets the window's extended style (WS_EX_* flags).
        /// </summary>
        public const int GWL_EXSTYLE = -20;

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
        /// ShowWindow command: Hides the window and deactivates it.
        /// </summary>
        public const int SW_HIDE = 0;

        /// <summary>
        /// ShowWindow command: Shows the window in its current state without activating it.
        /// Preserves the window's position and size.
        /// </summary>
        public const int SW_SHOWNA = 8;

        /// <summary>
        /// SetWindowPos flag: Displays the window (shows it). Used in conjunction with SWP_NOACTIVATE and other flags.
        /// </summary>
        public const uint SWP_SHOWWINDOW = 0x0040;

        /// <summary>
        /// SetWindowPos flag: Hides the window. Used in conjunction with SWP_NOACTIVATE and other flags.
        /// </summary>
        public const uint SWP_HIDEWINDOW = 0x0080;

        /// <summary>
        /// GetAncestor flag: Retrieves the root window of the specified window.
        /// </summary>
        public const uint GA_ROOT = 2;

        /// <summary>
        /// Extended window style flag: Makes the window layered, enabling transparency, opacity,
        /// and per-pixel alpha blending.
        /// </summary>
        public const int WS_EX_LAYERED = 0x00080000;

        /// <summary>
        /// SetLayeredWindowAttributes flag: Uses the alpha value specified to set the opacity of the layered window.
        /// The bAlpha parameter specifies the transparency level (0 = fully transparent, 255 = fully opaque).
        /// </summary>
        public const uint LWA_ALPHA = 0x00000002;

        /// <summary>
        /// Window message: Erase background
        /// </summary>
        public const uint WM_ERASEBKGND = 0x0014;

        /// <summary>
        /// Window message: Paint window
        /// </summary>
        public const uint WM_PAINT = 0x000F;

        /// <summary>
        /// Window message: Mouse movement
        /// </summary>
        public const uint WM_MOUSEMOVE = 0x0200;

        /// <summary>
        /// Window message: Mouse leave notification
        /// </summary>
        public const uint WM_MOUSELEAVE = 0x02A3;

        /// <summary>
        /// Window message: Left mouse button down
        /// </summary>
        public const uint WM_LBUTTONDOWN = 0x0201;

        /// <summary>
        /// Window message: Left mouse button up
        /// </summary>
        public const uint WM_LBUTTONUP = 0x0202;

        /// <summary>
        /// Window message: Window destroyed
        /// </summary>
        public const uint WM_DESTROY = 0x0002;

        /// <summary>
        /// Window message: Non-client area destroyed (sent when the non-client area of a window is being destroyed)
        /// </summary>
        public const uint WM_NCDESTROY = 0x0082;

        /// <summary>
        /// Window message: Notify - Sent to a window when a control sends a notification message (e.g., button click, selection change).
        /// </summary>
        public const uint WM_NOTIFY = 0x004E;

        /// <summary>
        /// Window message: Control color - message box
        /// </summary>
        public const uint WM_CTLCOLORMSGBOX = 0x0132;

        /// <summary>
        /// Window message: Control color - edit control
        /// </summary>
        public const uint WM_CTLCOLOREDIT = 0x0133;

        /// <summary>
        /// Window message: Control color - list box
        /// </summary>
        public const uint WM_CTLCOLORLISTBOX = 0x0134;

        /// <summary>
        /// Window message: Control color - button
        /// </summary>
        public const uint WM_CTLCOLORBTN = 0x0135;

        /// <summary>
        /// Window message: Control color - dialog box
        /// </summary>
        public const uint WM_CTLCOLORDLG = 0x0136;

        /// <summary>
        /// Window message: Control color - scrollbar
        /// </summary>
        public const uint WM_CTLCOLORSCROLLBAR = 0x0137;

        /// <summary>
        /// Window message: Control color - static control
        /// </summary>
        public const uint WM_CTLCOLORSTATIC = 0x0138;

        /// <summary>
        /// Window message: System color change
        /// </summary>
        public const uint WM_SYSCOLORCHANGE = 0x0015;

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
        /// Window message: Print client area
        /// </summary>
        public const uint WM_PRINTCLIENT = 0x0318;

        /// <summary>
        /// Paint/Print flags: Client area
        /// </summary>
        public const uint PRF_CLIENT = 0x00000004;

        /// <summary>
        /// Get window long index: Window ID
        /// </summary>
        public const int GWL_ID = -12;

        /// <summary>
        /// Default GUI font identifier
        /// </summary>
        public const int DEFAULT_GUI_FONT = 17;

        /// <summary>
        /// WM_GETFONT message - Sent to a control to retrieve the font with which it is drawing its text
        /// </summary>
        public const uint WM_GETFONT = 0x0031;

        /// <summary>
        /// WM_SETFONT message - Sent to a control to set the font with which it will draw its text
        /// </summary>
        public const uint WM_SETFONT = 0x0030;

        /// <summary>
        /// WM_SIZE message - Sent to a window after its size has changed
        /// </summary>
        public const uint WM_SIZE = 0x0005;

        /// <summary>
        /// WM_WINDOWPOSCHANGED message - Sent to a window whose size, position, or Z-order has changed
        /// </summary>
        public const uint WM_WINDOWPOSCHANGED = 0x0047;

        /// <summary>
        /// WS_CHILD window style - Creates a child window
        /// </summary>
        public const int WS_CHILD = 0x40000000;

        /// <summary>
        /// WS_VISIBLE window style - Makes the window initially visible
        /// </summary>
        public const int WS_VISIBLE = 0x10000000;

        /// <summary>
        /// WS_TABSTOP window style - Control can receive focus through tab navigation
        /// </summary>
        public const int WS_TABSTOP = 0x00010000;

        /// <summary>
        /// WS_BORDER window style - Creates a window with a thin-line border
        /// </summary>
        public const int WS_BORDER = 0x00800000;

        /// <summary>
        /// WS_CLIPCHILDREN window style - Excludes child window areas from drawing
        /// </summary>
        public const int WS_CLIPCHILDREN = 0x02000000;

        /// <summary>
        /// WS_EX_CLIENTEDGE extended window style - Adds a sunken edge to the client area
        /// </summary>
        public const int WS_EX_CLIENTEDGE = 0x00000200;

        /// <summary>
        /// ES_AUTOHSCROLL edit style - Automatically scrolls text horizontally when entering text beyond control width
        /// </summary>
        public const int ES_AUTOHSCROLL = 0x0080;

        /// <summary>
        /// ES_LEFT edit style - Aligns text to the left
        /// </summary>
        public const int ES_LEFT = 0x0000;

        /// <summary>
        /// SW_SHOW show window command - Shows the window in its current position and size
        /// </summary>
        public const int SW_SHOW = 5;

        /// <summary>
        /// EM_GETSEL edit message - Gets the starting and ending character positions of the current selection
        /// </summary>
        public const int EM_GETSEL = 0x00B0;

        /// <summary>
        /// GWL_WNDPROC window long index - Retrieves the address of the window procedure
        /// </summary>
        public const int GWL_WNDPROC = -4;

        /// <summary>
        /// GWLP_WNDPROC window long pointer index - Retrieves the address of the window procedure (64-bit compatible)
        /// </summary>
        public const int GWLP_WNDPROC = -4;

        /// <summary>
        /// WH_CALLWNDPROC hook type - Installs a hook procedure that monitors messages before they are sent to the target window procedure.
        /// </summary>
        public const int WH_CALLWNDPROC = 4;

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
