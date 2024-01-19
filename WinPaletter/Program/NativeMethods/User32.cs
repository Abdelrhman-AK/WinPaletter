using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using static WinPaletter.NativeMethods.GDI32;

namespace WinPaletter.NativeMethods
{
    /// <summary>
    /// Provides partial class implementation for interacting with the User32 (User Interface) APIs.
    /// This partial class may contain additional members related to User32 functionality.
    /// </summary>
    public partial class User32
    {
        /// <summary>
        /// Loads the specified cursor resource from the executable (.EXE) file associated with an application instance.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern int LoadCursor(int hInstance, int lpCursorName);

        /// <summary>
        /// Sets the cursor shape.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern int SetCursor(int hCursor);

        /// <summary>
        /// Animates the window.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool AnimateWindow(IntPtr hWnd, int time, AnimateWindowFlags flags);

        /// <summary>
        /// Sends the specified message to a window or windows.
        /// </summary>
        /// <param name="hWnd">A handle to the window whose window procedure will receive the message.</param>
        /// <param name="Msg">The message to be sent.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, UIntPtr wParam, IntPtr lParam);

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
        /// Sets the window composition attribute.
        /// </summary>
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        /// <summary>
        /// Loads the specified cursor resource from a file.
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "LoadCursorFromFileA")]
        public static extern IntPtr LoadCursorFromFile(string lpFileName);

        /// <summary>
        /// Sends the specified message to a window or windows.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Retrieves a handle to the top-level window whose class name and window name match the specified strings.
        /// </summary>
        /// <param name="lpClassName">The class name or a class atom created by a previous call to the RegisterClass or RegisterClassEx function.</param>
        /// <param name="lpWindowName">The window name (the window's title).</param>
        /// <returns>If the function succeeds, the return value is a handle to the window that has the specified class name and window name. If the function fails, the return value is IntPtr.Zero.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// Sends the specified message to a window or windows. The SendMessage function calls the window procedure for the specified window and does not return until the window procedure has processed the message.
        /// </summary>
        /// <param name="hWnd">A handle to the window whose window procedure will receive the message.</param>
        /// <param name="Msg">The message to be sent.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        /// <returns>The result of the message processing; it depends on the message sent.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Retrieves the device context (DC) for the entire window, including title bar, menus, and scroll bars.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        /// <summary>
        /// Releases the device context (DC) for the specified window.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        /// <summary>
        /// Sets the background color of the specified device context (DC) to the specified color.
        /// </summary>
        [DllImport("gdi32.dll")]
        public static extern int SetBkColor(IntPtr hDC, int crColor);

        /// <summary>
        /// Sets the text color of the specified device context (DC) to the specified color.
        /// </summary>
        [DllImport("gdi32.dll")]
        public static extern int SetTextColor(IntPtr hDC, int crColor);

        [DllImport("user32.dll")]
        private static extern bool EnumChildWindows(IntPtr hWndParent, EnumChildProc lpEnumFunc, IntPtr lParam);

        /// <summary>
        /// Creates an icon or cursor from an ICONINFO structure.
        /// </summary>
        /// <param name="icon">
        /// A reference to an <see cref="ICONINFO"/> structure that contains information about the icon or cursor.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is a handle to the icon or cursor.
        /// If the function fails, the return value is <see cref="IntPtr.Zero"/>.
        /// </returns>
        /// <remarks>
        /// The ICONINFO structure should be properly initialized before calling this function.
        /// The created icon or cursor can be destroyed using <see cref="DestroyIcon"/> when it is no longer needed.
        /// </remarks>
        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconIndirect(ref ICONINFO icon);

        /// <summary>
        /// Loads an animated cursor from a file.
        /// </summary>
        /// <param name="hinst">A handle to the instance of the module that contains the image.</param>
        /// <param name="lpszName">The name or identifier of the image resource.</param>
        /// <param name="uType">The type of the image resource (e.g., IMAGE_CURSOR).</param>
        /// <param name="cxDesired">The desired width of the image, in pixels.</param>
        /// <param name="cyDesired">The desired height of the image, in pixels.</param>
        /// <param name="fuLoad">Flags that specify how to load the image (e.g., LR_LOADFROMFILE).</param>
        /// <returns>
        /// If the function succeeds, the handle to the loaded image. If the function fails, it returns IntPtr.Zero.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr LoadImage(IntPtr hinst, string lpszName, uint uType, int cxDesired, int cyDesired, uint fuLoad);

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
        /// MSLLHOOKSTRUCT structure: Contains information about a low-level mouse input event.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        /// <summary>
        /// POINT structure: Defines the x- and y-coordinates of a point in a two-dimensional plane.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        /// <summary>
        /// LowLevelMouseProc delegate: Represents the method that will handle the low-level mouse input events.
        /// </summary>
        /// <param name="nCode">A code the hook procedure uses to determine how to process the message.</param>
        /// <param name="wParam">The wParam value passed to the hook procedure.</param>
        /// <param name="lParam">The lParam value passed to the hook procedure.</param>
        /// <returns>If the function succeeds, the return value is the result value returned by the hook procedure.
        /// If the function fails, the return value is IntPtr.Zero.</returns>
        public delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);


        // Constants for LoadImage
        public const uint IMAGE_CURSOR = 2;

        /// <summary>
        /// Flags for the LoadImage function specifying how to load the image.
        /// </summary>
        [Flags]
        public enum LoadImageFlags : uint
        {
            /// <summary>
            /// Use the default size (cxDesired and cyDesired are ignored).
            /// </summary>
            LR_DEFAULTSIZE = 0x00000040,

            /// <summary>
            /// Load the image from a file (lpszName is the file path).
            /// </summary>
            LR_LOADFROMFILE = 0x00000010,

            /// <summary>
            /// Convert the image to monochrome.
            /// </summary>
            LR_MONOCHROME = 0x00000001,

            /// <summary>
            /// Share the image handle (do not create a new copy).
            /// </summary>
            LR_SHARED = 0x00008000,
        }

        /// <summary>
        /// Enum representing different image types for the LoadImage function.
        /// </summary>
        public enum ImageType : uint
        {
            /// <summary>
            /// Bitmap image type.
            /// </summary>
            IMAGE_BITMAP = 0,

            /// <summary>
            /// Icon image type.
            /// </summary>
            IMAGE_ICON = 1,

            /// <summary>
            /// Cursor image type.
            /// </summary>
            IMAGE_CURSOR = 2,

            /// <summary>
            /// Enhanced metafile image type.
            /// </summary>
            IMAGE_ENHMETAFILE = 3,

            // Add more image types as needed
        }


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
        /// <description>Draws the icon or cursor using the image's size without any stretching. This flag is similar to DI_NORMAL, but preserves the 8-bit alpha channel of the icon's XOR mask. The default is to treat this image as an opaque image.</description>
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
        /// Retrieves information about the specified icon or cursor.
        /// </summary>
        /// <param name="hIcon">A handle to the icon or cursor.</param>
        /// <param name="pIconInfo">A pointer to an ICONINFO structure. The function fills in the structure's members with information about the icon or cursor.</param>
        /// <returns>If the function succeeds, the return value is true. If the function fails, the return value is false.</returns>
        [DllImport("user32")]
        public static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO pIconInfo);

        /// <summary>
        /// Contains information about an icon or a cursor.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct ICONINFO
        {
            /// <summary>
            /// Specifies whether this structure defines an icon or a cursor. A value of true indicates an icon; false indicates a cursor.
            /// </summary>
            public bool fIcon;

            /// <summary>
            /// The x-coordinate of a cursor's hot spot. If this structure defines an icon, the hot spot is always in the center of the icon, and this member is ignored.
            /// </summary>
            public int xHotspot;

            /// <summary>
            /// The y-coordinate of the cursor's hot spot. If this structure defines an icon, the hot spot is always in the center of the icon, and this member is ignored.
            /// </summary>
            public int yHotspot;

            /// <summary>
            /// A handle to the mask bitmap. If this structure defines an icon, the mask bitmaps are XORed with the color bitmaps to produce the icon bitmaps.
            /// If this structure defines a cursor, the mask bitmaps are applied to the destination in the screen DC.
            /// </summary>
            public IntPtr hbmMask;

            /// <summary>
            /// A handle to the color bitmap. This member can be optional if this structure defines a black and white icon. The color bitmap is applied to the destination
            /// in the screen DC in a similar way to how the mask bitmaps are applied to the destination when a cursor is drawn.
            /// </summary>
            public IntPtr hbmColor;
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
            /// Windows 2000/XP: Hand.
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

        private delegate bool EnumChildProc(IntPtr hWnd, IntPtr lParam);

        /// <summary>
        /// Retrieves the handles of all child windows of the specified window.
        /// </summary>
        /// <param name="win32Window">The parent window.</param>
        /// <returns>A list of handles to the child windows.</returns>
        public static List<IntPtr> GetChildWindowHandles(IWin32Window win32Window)
        {
            // List to store child window handles
            List<IntPtr> childHandles = new();

            // Get the handle of the parent window
            IntPtr hWndParent = win32Window.Handle;

            // Callback function to process each child window
            EnumChildProc childProc = (hWnd, lParam) =>
            {
                // Add the handle to the list
                childHandles.Add(hWnd);
                return true; // Continue enumeration
            };

            // Enumerate through all child windows of the parent window
            EnumChildWindows(hWndParent, childProc, IntPtr.Zero);

            return childHandles;
        }
    }
}
