using System.ComponentModel;

namespace WinPaletter.NativeMethods
{
    /// <summary>
    /// Provides partial class implementation for interacting with the User32 (User Interface) APIs.
    /// This partial class may contain additional members related to User32 functionality.
    /// </summary>
    public partial class User32
    {
        /// <summary>
        /// SPI_ System-wide parameter - Used in SystemParametersInfo function
        /// </summary>
        [Description("SPI_(System-wide parameter - Used in SystemParametersInfo function )")]
        public enum SPI : int
        {
            /// <summary>
            /// <b>Determines whether the warning beeper is on.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives <b><c>true</c></b> if the beeper is on, or <b><c>false</c></b> if it is off.
            /// </summary>
            SPI_GETBEEP = 0x0001,

            /// <summary>
            /// <b>Turns the warning beeper on or off.</b>
            /// <br></br>
            /// <br></br> • The <b><c>uiParam</c></b> parameter specifies <b><c>true</c></b> for on, or <b><c>false</c></b> for off.
            /// </summary>
            SPI_SETBEEP = 0x0002,

            /// <summary>
            /// <b>Retrieves the two mouse threshold values and the mouse speed.</b>
            /// </summary>
            SPI_GETMOUSE = 0x0003,

            /// <summary>
            /// <b>Sets the two mouse threshold values and the mouse speed.</b>
            /// </summary>
            SPI_SETMOUSE = 0x0004,

            /// <summary>
            /// <b>Retrieves the border multiplier factor that determines the width of a window's sizing border.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to an <b><c>integer</c></b> variable that receives this value.
            /// </summary>
            SPI_GETBORDER = 0x0005,

            /// <summary>
            /// <b>Sets the border multiplier factor that determines the width of a window's sizing border.</b>
            /// <br></br>
            /// <br></br> • The <b><c>uiParam</c></b> parameter specifies the new value.
            /// </summary>
            SPI_SETBORDER = 0x0006,

            /// <summary>
            /// <b>Retrieves the keyboard repeat-speed setting, which is a value in the range from <c>0</c> (approximately 2.5 repetitions per second)
            /// through 31 (approximately 30 repetitions per second).</b>
            /// <br></br>
            /// The actual repeat rates are hardware-dependent and may vary from a linear scale by as much as 20%.
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <c>DWORD</c> variable that receives the setting.
            /// </summary>
            SPI_GETKEYBOARDSPEED = 0x000A,

            /// <summary>
            /// <b>Sets the keyboard repeat-speed setting.</b>
            /// <br></br>
            /// <br></br> • The <b><c>uiParam</c></b> parameter must specify a value in the range from <c>0</c> (approximately 2.5 repetitions per second) through 31 (approximately 30 repetitions per second).
            /// <br></br> • The actual repeat rates are hardware-dependent and may vary from a linear scale by as much as 20%.
            /// <br></br> • If <b><c>uiParam</c></b> is greater than 31, the parameter is set to 31.
            /// </summary>
            SPI_SETKEYBOARDSPEED = 0x000B,

            /// <summary>
            /// <b>Not implemented.</b>
            /// </summary>
            SPI_LANGDRIVER = 0x000C,

            /// <summary>
            /// <b>Sets or retrieves the width, in pixels, of an icon cell. The system uses this rectangle to arrange icons in large icon view.</b>
            /// <br></br>
            /// <br></br> • To set this value, set <b><c>uiParam</c></b> to the new value and set <b><c>pvParam</c></b> to null. You cannot set this value to less than <c>SM_CXICON</c>.
            /// <br></br> • To retrieve this value, <b><c>pvParam</c></b> must point to an <b><c>integer</c></b> that receives the current value.
            /// </summary>
            SPI_ICONHORIZONTALSPACING = 0x000D,

            /// <summary>
            /// <b>Retrieves the screen saver time-out value, in seconds.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to an <b><c>integer</c></b> variable that receives the value.
            /// </summary>
            SPI_GETSCREENSAVETIMEOUT = 0x000E,

            /// <summary>
            /// <b>Sets the screen saver time-out value to the value of the <b><c>uiParam</c></b> parameter. This value is the amount of time, in seconds,
            /// that the system must be idle before the screen saver activates.</b>
            /// </summary>
            SPI_SETSCREENSAVETIMEOUT = 0x000F,

            /// <summary>
            /// <b>Determines whether screen saving is enabled.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <c>bool</c> variable that receives <b><c>true</c></b>
            /// if screen saving is enabled, or <b><c>false</c></b> otherwise.
            /// </summary>
            SPI_GETSCREENSAVEACTIVE = 0x0010,

            /// <summary>
            /// <b>Sets the state of the screen saver.</b>
            /// <br></br>
            /// <br></br> • The <b><c>uiParam</c></b> parameter specifies <b><c>true</c></b> to activate screen saving, or <b><c>false</c></b> to deactivate it.
            /// </summary>
            SPI_SETSCREENSAVEACTIVE = 0x0011,

            /// <summary>
            /// <b>Retrieves the current granularity value of the desktop sizing grid.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to an <b><c>integer</c></b> variable that receives the granularity.
            /// </summary>
            SPI_GETGRIDGRANULARITY = 0x0012,

            /// <summary>
            /// <b>Sets the granularity of the desktop sizing grid to the value of the <b><c>uiParam</c></b> parameter.</b>
            /// </summary>
            SPI_SETGRIDGRANULARITY = 0x0013,

            /// <summary>
            /// <b>Sets the desktop wallpaper.</b>
            /// <br></br>
            /// <br></br> • The value of the <b><c>pvParam</c></b> parameter determines the new wallpaper.
            /// <br></br> • To specify a wallpaper bitmap, set <b><c>pvParam</c></b> to point to a null-terminated string containing the name of a bitmap file.
            /// <br></br> • Setting <b><c>pvParam</c></b> to <b><c>""</c></b> removes the wallpaper.
            /// <br></br> • Setting <b><c>pvParam</c></b> to <c>SETWALLPAPER_DEFAULT</c> or null reverts to the default wallpaper.
            /// </summary>
            SPI_SETDESKWALLPAPER = 0x0014,

            /// <summary>
            /// <b>Sets the current desktop pattern by causing Windows to read the Pattern= setting from the WIN.INI file.</b>
            /// </summary>
            SPI_SETDESKPATTERN = 0x0015,

            /// <summary>
            /// <b>Retrieves the keyboard repeat-delay setting.</b>
            /// <br></br>
            /// <br></br> • This is a value in the range from <c>0</c> (approximately 250 ms delay) through 3 (approximately <c>1</c> second delay).
            /// <br></br> • The actual delay associated with each value may vary depending on the hardware.
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to an <b><c>integer</c></b> variable that receives the setting.
            /// </summary>
            SPI_GETKEYBOARDDELAY = 0x0016,

            /// <summary>
            /// <b>Sets the keyboard repeat-delay setting.</b>
            /// <br></br>
            /// <br></br> • The <b><c>uiParam</c></b> parameter must specify 0, <c>1</c>, 2, or 3, where <c>zero</c> sets the shortest delay (approximately 250 ms) and 3 sets the longest delay (approximately <c>1</c> second).
            /// <br></br> • The actual delay associated with each value may vary depending on the hardware.
            /// </summary>
            SPI_SETKEYBOARDDELAY = 0x0017,

            /// <summary>
            /// <b>Sets or retrieves the height, in pixels, of an icon cell.</b>
            /// <br></br>
            /// <br></br> • To set this value, set <b><c>uiParam</c></b> to the new value and set <b><c>pvParam</c></b> to null. You cannot set this value to less than <c>SM_CYICON</c>.
            /// <br></br> • To retrieve this value, <b><c>pvParam</c></b> must point to an <b><c>integer</c></b> that receives the current value.
            /// </summary>
            SPI_ICONVERTICALSPACING = 0x0018,

            /// <summary>
            /// <b>Determines whether icon-title wrapping is enabled.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <c>bool</c> variable that receives <b><c>true</c></b> if enabled, or <b><c>false</c></b> otherwise.
            /// </summary>
            SPI_GETICONTITLEWRAP = 0x0019,

            /// <summary>
            /// <b>Turns icon-title wrapping on or off.</b>
            /// <br></br>
            /// <br></br> • The <b><c>uiParam</c></b> parameter specifies <b><c>true</c></b> for on, or <b><c>false</c></b> for off.
            /// </summary>
            SPI_SETICONTITLEWRAP = 0x001A,

            /// <summary>
            /// <b>Determines whether pop-up menus are left-aligned or right-aligned, relative to the corresponding menu-bar item.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives <b><c>true</c></b> if left-aligned, or <b><c>false</c></b> otherwise.
            /// </summary>
            SPI_GETMENUDROPALIGNMENT = 0x001B,

            /// <summary>
            /// <b>Sets the alignment value of pop-up menus.</b>
            /// <br></br>
            /// <br></br> • The <b><c>uiParam</c></b> parameter specifies <b><c>true</c></b> for right alignment, or <b><c>false</c></b> for left alignment.
            /// </summary>
            SPI_SETMENUDROPALIGNMENT = 0x001C,

            /// <summary>
            /// <b>Sets the width of the double-click rectangle to the value of the <b><c>uiParam</c></b> parameter.</b>
            /// <br></br>
            /// <br></br> • The double-click rectangle is the rectangle within which the second click of a double-click must fall for it to be registered
            /// as a double-click.
            /// <br></br> • To retrieve the width of the double-click rectangle, call GetSystemMetrics with the <c>SM_CXDOUBLECLK</c> flag.
            /// </summary>
            SPI_SETDOUBLECLKWIDTH = 0x001D,

            /// <summary>
            /// <b>Sets the height of the double-click rectangle to the value of the <b><c>uiParam</c></b> parameter.</b>
            /// <br></br>
            /// <br></br> • The double-click rectangle is the rectangle within which the second click of a double-click must fall for it to be registered
            /// as a double-click.
            /// <br></br> • To retrieve the height of the double-click rectangle, call GetSystemMetrics with the <c>SM_CYDOUBLECLK</c> flag.
            /// </summary>
            SPI_SETDOUBLECLKHEIGHT = 0x001E,

            /// <summary>
            /// <b>Retrieves the logical font information for the current icon-title font.</b>
            /// <br></br>
            /// <br></br> • The <b><c>uiParam</c></b> parameter specifies the size of a LogFont structure,
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to the LogFont structure to fill in.
            /// </summary>
            SPI_GETICONTITLELOGFONT = 0x001F,

            /// <summary>
            /// <b>Sets the double-click time for the mouse to the value of the <b><c>uiParam</c></b> parameter.</b>
            /// <br></br>
            /// <br></br> • The double-click time is the maximum number of milliseconds that can occur between the first and second clicks of a double-click.
            /// <br></br> • You can also call the SetDoubleClickTime function to set the double-click time.
            /// <br></br> • To get the current double-click time, call the GetDoubleClickTime function.
            /// </summary>
            SPI_SETDOUBLECLICKTIME = 0x0020,

            /// <summary>
            /// <b>Swaps or restores the meaning of the left and right mouse buttons.</b>
            /// <br></br>
            /// <br></br> • The <b><c>uiParam</c></b> parameter specifies <b><c>true</c></b> to swap the meanings of the buttons, or <b><c>false</c></b> to restore their original meanings.
            /// </summary>
            SPI_SETMOUSEBUTTONSWAP = 0x0021,

            /// <summary>
            /// <b>Sets the font that is used for icon titles.</b>
            /// <br></br>
            /// <br></br> • The <b><c>uiParam</c></b> parameter specifies the size of a LogFont structure,
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a LogFont structure.
            /// </summary>
            SPI_SETICONTITLELOGFONT = 0x0022,

            /// <summary>
            /// <b>This flag is obsolete.</b>
            /// <br></br>
            /// <br></br> • Previous versions of the system use this flag to determine whether ALT+TAB fast task switching is enabled.
            /// <br></br> • For Windows 95, Windows 98, and Windows NT version 4.0 and later, fast task switching is always enabled.
            /// </summary>
            SPI_GETFASTTASKSWITCH = 0x0023,

            /// <summary>
            /// <b>This flag is obsolete.</b>
            /// <br></br>
            /// <br></br> • Previous versions of the system use this flag to enable or disable ALT+TAB fast task switching.
            /// <br></br> • For Windows 95, Windows 98, and Windows NT version 4.0 and later, fast task switching is always enabled.
            /// </summary>
            SPI_SETFASTTASKSWITCH = 0x0024,

            /// <summary>
            /// <b>Sets dragging of full windows either on or off.</b>
            /// <br></br>
            /// <br></br> • The <b><c>uiParam</c></b> parameter specifies <b><c>true</c></b> for on, or <b><c>false</c></b> for off.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 95: This flag is supported only if Windows Plus! is installed. See <c>SPI_GETWINDOWSEXTENSION</c>.</i>
            /// </summary>
            SPI_SETDRAGFULLWINDOWS = 0x0025,

            /// <summary>
            /// <b>Determines whether dragging of full windows is enabled.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives <b><c>true</c></b> if enabled, or <b><c>false</c></b> otherwise.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 95: This flag is supported only if Windows Plus! is installed. See <c>SPI_GETWINDOWSEXTENSION</c>.</i>
            /// </summary>
            SPI_GETDRAGFULLWINDOWS = 0x0026,

            /// <summary>
            /// <b>Retrieves the metrics associated with the nonclient area of nonminimized windows.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>NONCLIENTMETRICS</c></b> structure that receives the information.
            /// <br></br> • Set the <b><c>cbSize</c></b> member of this structure and the <b><c>uiParam</c></b> parameter to <b><c>sizeof(NONCLIENTMETRICS)</c></b>.
            /// </summary>
            SPI_GETNONCLIENTMETRICS = 0x0029,

            /// <summary>
            /// <b>Sets the metrics associated with the nonclient area of nonminimized windows.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>NONCLIENTMETRICS</c></b> structure that contains the new parameters.
            /// <br></br> • Set the <b><c>cbSize</c></b> member of this structure and the <b><c>uiParam</c></b> parameter to <b><c>sizeof(NONCLIENTMETRICS)</c></b>.
            /// <br></br> • Also, the lfHeight member of the LogFont structure must be a negative value.
            /// </summary>
            SPI_SETNONCLIENTMETRICS = 0x002A,

            /// <summary>
            /// <b>Retrieves the metrics associated with minimized windows.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>MINIMIZEDMETRICS</c></b> structure that receives the information.
            /// <br></br> • Set the <b><c>cbSize</c></b> member of this structure and the <b><c>uiParam</c></b> parameter to <b><c>sizeof(MINIMIZEDMETRICS)</c></b>.
            /// </summary>
            SPI_GETMINIMIZEDMETRICS = 0x002B,

            /// <summary>
            /// <b>Sets the metrics associated with minimized windows.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>MINIMIZEDMETRICS</c></b> structure that contains the new parameters.
            /// <br></br> • Set the <b><c>cbSize</c></b> member of this structure and the <b><c>uiParam</c></b> parameter to <b><c>sizeof(MINIMIZEDMETRICS)</c></b>.
            /// </summary>
            SPI_SETMINIMIZEDMETRICS = 0x002C,

            /// <summary>
            /// <b>Retrieves the metrics associated with icons.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to an <b><c>ICONMETRICS</c></b> structure that receives the information.
            /// <br></br> • Set the <b><c>cbSize</c></b> member of this structure and the <b><c>uiParam</c></b> parameter to <b><c>sizeof(ICONMETRICS)</c></b>.
            /// </summary>
            SPI_GETICONMETRICS = 0x002D,

            /// <summary>
            /// <b>Sets the metrics associated with icons.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to an <b><c>ICONMETRICS</c></b> structure that contains the new parameters.
            /// <br></br> • Set the <b><c>cbSize</c></b> member of this structure and the <b><c>uiParam</c></b> parameter to <b><c>sizeof(ICONMETRICS)</c></b>.
            /// </summary>
            SPI_SETICONMETRICS = 0x002E,

            /// <summary>
            /// <b>Sets the size of the work area.</b>
            /// <br></br>
            /// <br></br> • The work area is the portion of the screen not obscured by the system taskbar
            /// <br></br> • or by application desktop toolbars.
            /// <br></br> • The <b><c>pvParam</c></b> parameter is a pointer to a <b><c>RECT</c></b> structure that specifies the new work area rectangle,
            /// <br></br> • expressed in virtual screen coordinates. In a system with multiple display monitors, the function sets the work area
            /// <br></br> • of the monitor that contains the specified rectangle.
            /// </summary>
            SPI_SETWORKAREA = 0x002F,

            /// <summary>
            /// <b>Retrieves the size of the work area on the primary display monitor.</b>
            /// <br></br>
            /// <br></br> • The work area is the portion of the screen not obscured
            /// <br></br> • by the system taskbar or by application desktop toolbars.
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>RECT</c></b> structure that receives
            /// <br></br> • the coordinates of the work area, expressed in virtual screen coordinates.
            /// <br></br> • To get the work area of a monitor other than the primary display monitor, call the GetMonitorInfo function.
            /// </summary>
            SPI_GETWORKAREA = 0x0030,

            /// <summary>
            /// <b>Windows Me/98/95: Pen windows is being loaded or unloaded.</b>
            /// <br></br>
            /// <br></br> • The <b><c>uiParam</c></b> parameter is <b><c>true</c></b> when loading and FALSE
            /// <br></br> • when unloading pen windows. The <b><c>pvParam</c></b> parameter is null.
            /// </summary>
            SPI_SETPENWINDOWS = 0x0031,

            /// <summary>
            /// <b>Retrieves information about the HighContrast accessibility feature.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <c>HIGHCONTRAST</c> structure
            /// <br></br> • that receives the information. Set the <b><c>cbSize</c></b> member of this structure
            /// <br></br> • and the <b><c>uiParam</c></b> parameter to <b><c>sizeof(HIGHCONTRAST)</c></b>.
            /// <br></br> • For a general discussion, see remarks.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT: This value is not supported.</i>
            /// </summary>
            /// <remarks>
            /// There is a difference between the High Contrast color scheme and the High Contrast Mode.
            /// The High Contrast color scheme changes the system colors to colors that have obvious contrast;
            /// you switch to this color scheme by using the Display Options in the control panel.
            /// The High Contrast Mode, which uses <c>SPI_GETHIGHCONTRAST</c> and <c>SPI_SETHIGHCONTRAST</c>, advises applications
            /// to modify their appearance for visually-impaired users. It involves such things as audible warning to users
            /// and customized color scheme (using the Accessibility Options in the control panel).
            /// For more information, see <c>HIGHCONTRAST</c> on MSDN.
            /// For more information on general accessibility features, see Accessibility on MSDN.
            /// </remarks>
            SPI_GETHIGHCONTRAST = 0x0042,

            /// <summary>
            /// <b>Sets the parameters of the HighContrast accessibility feature.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <c>HIGHCONTRAST</c> structure
            /// <br></br> • that contains the new parameters. Set the <b><c>cbSize</c></b> member of this structure
            /// <br></br> • and the <b><c>uiParam</c></b> parameter to <b><c>sizeof(HIGHCONTRAST)</c></b>.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT: This value is not supported.</i>
            /// </summary>
            SPI_SETHIGHCONTRAST = 0x0043,

            /// <summary>
            /// <b>Determines whether the user relies on the keyboard instead of the mouse,</b>
            /// <br></br>
            /// <br></br> • and wants applications to display keyboard interfaces
            /// <br></br> • that would otherwise be hidden. The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable
            /// <br></br> • that receives <b><c>true</c></b> if the user relies on the keyboard; or <b><c>false</c></b> otherwise.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT: This value is not supported.</i>
            /// </summary>
            SPI_GETKEYBOARDPREF = 0x0044,

            /// <summary>
            /// <b>Sets the keyboard preference.</b>
            /// <br></br>
            /// <br></br> • The <b><c>uiParam</c></b> parameter specifies <b><c>true</c></b> if the user relies on the keyboard
            /// <br></br> • instead of the mouse, and wants applications to display keyboard interfaces
            /// <br></br> • that would otherwise be hidden; <b><c>uiParam</c></b> is <b><c>false</c></b> otherwise.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT: This value is not supported.</i>
            /// </summary>
            SPI_SETKEYBOARDPREF = 0x0045,

            /// <summary>
            /// <b>Determines whether a screen reviewer utility is running.</b>
            /// <br></br>
            /// <br></br> • A screen reviewer utility directs textual information to an output device,
            /// <br></br> • such as a speech synthesizer or Braille display. When this flag is set, an application
            /// <br></br> • should provide textual information in situations where it would otherwise present the information graphically.
            /// <br></br> • The <b><c>pvParam</c></b> parameter is a pointer to a <b><c>bool</c></b> variable that receives <b><c>true</c></b> if a screen reviewer utility is running,
            /// <br></br> • or <b><c>false</c></b> otherwise. Windows NT: This value is not supported.
            /// </summary>
            SPI_GETSCREENREADER = 0x0046,

            /// <summary>
            /// <b>Determines whether a screen review utility is running.</b>
            /// <br></br>
            /// <br></br> • The <b><c>uiParam</c></b> parameter specifies <b><c>true</c></b> for on, or <b><c>false</c></b> for off.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT: This value is not supported.</i>
            /// </summary>
            SPI_SETSCREENREADER = 0x0047,

            /// <summary>
            /// <b>Retrieves the animation effects associated with user actions.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to an <b><c>ANIMATIONINFO</c></b> structure
            /// <br></br> • that receives the information. Set the <b><c>cbSize</c></b> member of this structure
            /// <br></br> • and the <b><c>uiParam</c></b> parameter to <b><c>sizeof(ANIMATIONINFO)</c></b>.
            /// </summary>
            SPI_GETANIMATION = 0x0048,

            /// <summary>
            /// <b>Sets the animation effects associated with user actions.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to an <b><c>ANIMATIONINFO</c></b> structure
            /// <br></br> • that contains the new parameters. Set the <b><c>cbSize</c></b> member of this structure
            /// <br></br> • and the <b><c>uiParam</c></b> parameter to <b><c>sizeof(ANIMATIONINFO)</c></b>.
            /// </summary>
            SPI_SETANIMATION = 0x0049,

            /// <summary>
            /// <b>Determines whether the font smoothing feature is enabled.</b>
            /// <br></br>This feature uses font antialiasing to make font curves appear smoother by painting pixels at different gray levels.
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives TRUE if the feature is enabled, or <b><c>false</c></b> if it is not.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 95: This flag is supported only if Windows Plus! is installed. See <c>SPI_GETWINDOWSEXTENSION</c>.</i>
            /// </summary>
            SPI_GETFONTSMOOTHING = 0x004A,

            /// <summary>
            /// <b>Enables or disables the font smoothing feature, which uses font antialiasing to make font curves appear smoother by painting pixels at different gray levels.</b>
            /// <br></br>
            /// <br></br> • To enable the feature, set the <b><c>uiParam</c></b> parameter to TRUE.
            /// <br></br> • To disable the feature, set <b><c>uiParam</c></b> to FALSE.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 95: This flag is supported only if Windows Plus! is installed. See <c>SPI_GETWINDOWSEXTENSION</c>.</i>
            /// </summary>
            SPI_SETFONTSMOOTHING = 0x004B,

            /// <summary>
            /// <b>Sets the width, in pixels, of the rectangle used to detect the start of a drag operation.</b>
            /// <br></br>
            /// <br></br> • Set <b><c>uiParam</c></b> to the new value.
            /// <br></br> • To retrieve the drag width, call GetSystemMetrics with the <c>SM_CXDRAG</c> flag.
            /// </summary>
            SPI_SETDRAGWIDTH = 0x004C,

            /// <summary>
            /// <b>Sets the height, in pixels, of the rectangle used to detect the start of a drag operation.</b>
            /// <br></br>
            /// <br></br> • Set <b><c>uiParam</c></b> to the new value.
            /// <br></br> • To retrieve the drag height, call GetSystemMetrics with the <c>SM_CYDRAG</c> flag.
            /// </summary>
            SPI_SETDRAGHEIGHT = 0x004D,

            /// <summary>
            /// <b>Used internally; applications should not use this value.</b>
            /// </summary>
            SPI_SETHANDHELD = 0x004E,

            /// <summary>
            /// <b>Retrieves the time-out value for the low-power phase of screen saving.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to an integer variable that receives the value.
            /// <br></br> • This flag is supported for <c>32-bit</c> applications only.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows Me/98: This flag is supported for <c>16-bit</c> and <c>32-bit</c> applications.</i>
            /// <br></br> • <i> (!) Windows 95: This flag is supported for <c>16-bit</c> applications only.</i>
            /// </summary>
            SPI_GETLOWPOWERTIMEOUT = 0x004F,

            /// <summary>
            /// <b>Retrieves the time-out value for the power-off phase of screen saving.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to an integer variable that receives the value.
            /// <br></br> • This flag is supported for <c>32-bit</c> applications only.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows Me/98: This flag is supported for <c>16-bit</c> and <c>32-bit</c> applications.</i>
            /// <br></br> • <i> (!) Windows 95: This flag is supported for <c>16-bit</c> applications only.</i>
            /// </summary>
            SPI_GETPOWEROFFTIMEOUT = 0x0050,

            /// <summary>
            /// <b>Sets the time-out value, in seconds, for the low-power phase of screen saving.</b>
            /// <br></br>
            /// <br></br> • The <b><c>uiParam</c></b> parameter specifies the new value.
            /// <br></br> • The <b><c>pvParam</c></b> parameter must be null.
            /// <br></br> • This flag is supported for <c>32-bit</c> applications only.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows Me/98: This flag is supported for <c>16-bit</c> and <c>32-bit</c> applications.</i>
            /// <br></br> • <i> (!) Windows 95: This flag is supported for <c>16-bit</c> applications only.</i>
            /// </summary>
            SPI_SETLOWPOWERTIMEOUT = 0x0051,

            /// <summary>
            /// <b>Sets the time-out value, in seconds, for the power-off phase of screen saving.</b>
            /// <br></br>
            /// <br></br> • The <b><c>uiParam</c></b> parameter specifies the new value.
            /// <br></br> • The <b><c>pvParam</c></b> parameter must be null.
            /// <br></br> • This flag is supported for <c>32-bit</c> applications only.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows Me/98: This flag is supported for <c>16-bit</c> and <c>32-bit</c> applications.</i>
            /// <br></br> • <i> (!) Windows 95: This flag is supported for <c>16-bit</c> applications only.</i>
            /// </summary>
            SPI_SETPOWEROFFTIMEOUT = 0x0052,

            /// <summary>
            /// <b>Determines whether the low-power phase of screen saving is enabled.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives <b><c>true</c></b> if enabled, or <b><c>false</c></b> if disabled.
            /// <br></br> • This flag is supported for <c>32-bit</c> applications only.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows Me/98: This flag is supported for <c>16-bit</c> and <c>32-bit</c> applications.</i>
            /// <br></br> • <i> (!) Windows 95: This flag is supported for <c>16-bit</c> applications only.</i>
            /// </summary>
            SPI_GETLOWPOWERACTIVE = 0x0053,

            /// <summary>
            /// <b>Determines whether the power-off phase of screen saving is enabled.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives <b><c>true</c></b> if enabled, or <b><c>false</c></b> if disabled.
            /// <br></br> • This flag is supported for <c>32-bit</c> applications only.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows Me/98: This flag is supported for <c>16-bit</c> and <c>32-bit</c> applications.</i>
            /// <br></br> • <i> (!) Windows 95: This flag is supported for <c>16-bit</c> applications only.</i>
            /// </summary>
            SPI_GETPOWEROFFACTIVE = 0x0054,

            /// <summary>
            /// <b>Activates or deactivates the low-power phase of screen saving.</b>
            /// <br></br>
            /// <br></br> • Set <b><c>uiParam</c></b> to <c>1</c> to activate, or <c>zero</c> to deactivate.
            /// <br></br> • The <b><c>pvParam</c></b> parameter must be null.
            /// <br></br> • This flag is supported for <c>32-bit</c> applications only.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows Me/98: This flag is supported for <c>16-bit</c> and <c>32-bit</c> applications.</i>
            /// <br></br> • <i> (!) Windows 95: This flag is supported for <c>16-bit</c> applications only.</i>
            /// </summary>
            SPI_SETLOWPOWERACTIVE = 0x0055,

            /// <summary>
            /// <b>Activates or deactivates the power-off phase of screen saving.</b>
            /// <br></br>
            /// <br></br> • Set <b><c>uiParam</c></b> to <c>1</c> to activate, or <c>zero</c> to deactivate.
            /// <br></br> • The <b><c>pvParam</c></b> parameter must be null.
            /// <br></br> • This flag is supported for <c>32-bit</c> applications only.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows Me/98: This flag is supported for <c>16-bit</c> and <c>32-bit</c> applications.</i>
            /// <br></br> • <i> (!) Windows 95: This flag is supported for <c>16-bit</c> applications only.</i>
            /// </summary>
            SPI_SETPOWEROFFACTIVE = 0x0056,

            /// <summary>
            /// <b>Reloads the system cursors.</b>
            /// <br></br>
            /// <br></br> • Set the <b><c>uiParam</c></b> parameter to <c>zero</c> and the <b><c>pvParam</c></b> parameter to null.
            /// </summary>
            SPI_SETCURSORS = 0x0057,

            /// <summary>
            /// <b>Reloads the system icons.</b>
            /// <br></br>
            /// <br></br> • Set the <b><c>uiParam</c></b> parameter to <c>zero</c> and the <b><c>pvParam</c></b> parameter to null.
            /// </summary>
            SPI_SETICONS = 0x0058,

            /// <summary>
            /// <b>Retrieves the input locale identifier for the system default input language.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to an HKL variable that receives this value.
            /// <br></br> • For more information, see Languages, Locales, and Keyboard Layouts on MSDN.
            /// </summary>
            SPI_GETDEFAULTINPUTLANG = 0x0059,

            /// <summary>
            /// <b>Sets the default input language for the system shell and applications.</b>
            /// <br></br>
            /// <br></br> • The specified language must be displayable using the current system character set.
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to an HKL variable that contains the input locale identifier for the default language.
            /// <br></br> • For more information, see Languages, Locales, and Keyboard Layouts on MSDN.
            /// </summary>
            SPI_SETDEFAULTINPUTLANG = 0x005A,

            /// <summary>
            /// <b>Sets the hot key set for switching between input languages.</b>
            /// <br></br>
            /// <br></br> • The <b><c>uiParam</c></b> and <b><c>pvParam</c></b> parameters are not used.
            /// <br></br> • The value sets the shortcut keys in the keyboard property sheets by reading the registry again.
            /// <br></br> • The registry must be set before this flag is used. The path in the registry is \HKEY_CURRENT_USER\keyboard layout\toggle.
            /// <br></br> • Valid values are "1" = ALT+SHIFT, "2" = CTRL+SHIFT, and "3" = none.
            /// </summary>
            SPI_SETLANGTOGGLE = 0x005B,

            /// <summary>
            /// <b>Windows 95: Determines whether the Windows extension, Windows Plus!, is installed.</b>
            /// <br></br>
            /// <br></br> • Set the <b><c>uiParam</c></b> parameter to <c>1</c>.
            /// <br></br> • The <b><c>pvParam</c></b> parameter is not used.
            /// <br></br> • The function returns <b><c>true</c></b> if the extension is installed, or <b><c>false</c></b> if it is not.
            /// </summary>
            SPI_GETWINDOWSEXTENSION = 0x005C,

            /// <summary>
            /// <b>Enables or disables the Mouse Trails feature, which improves the visibility of mouse cursor movements by briefly showing
            /// a trail of cursors and quickly erasing them.</b>
            /// <br></br>
            /// <br></br> • To disable the feature, set the <b><c>uiParam</c></b> parameter to <c>zero</c> or <c>1</c>.
            /// <br></br> • To enable the feature, set <b><c>uiParam</c></b> to a value greater than <c>1</c> to indicate the number of cursors drawn in the trail.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 2000/NT: This value is not supported.</i>
            /// </summary>
            SPI_SETMOUSETRAILS = 0x005D,

            /// <summary>
            /// <b>Determines whether the Mouse Trails feature is enabled.</b>
            /// <br></br>
            /// <br></br> • This feature improves the visibility of mouse cursor movements by briefly showing a trail of cursors and quickly erasing them.
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to an integer variable that receives a value.
            /// <br></br> • If the value is <c>zero</c> or <c>1</c>, the feature is disabled.
            /// <br></br> • If the value is greater than <c>1</c>, the feature is enabled and the value indicates the number of cursors drawn in the trail.
            /// <br></br> • The <b><c>uiParam</c></b> parameter is not used.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 2000/NT: This value is not supported.</i>
            /// </summary>
            SPI_GETMOUSETRAILS = 0x005E,

            /// <summary>
            /// <b>Windows Me/98: Used internally; applications should not use this flag.</b>
            /// </summary>
            SPI_SETSCREENSAVERRUNNING = 0x0061,

            /// <summary>
            /// <b>Same as SPI_SETSCREENSAVERRUNNING.</b>
            /// </summary>
            SPI_SCREENSAVERRUNNING = SPI_SETSCREENSAVERRUNNING,

            /// <summary>
            /// <b>Retrieves information about the FilterKeys accessibility feature.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a FILTERKEYS structure that receives the information.
            /// <br></br> • Set the <b><c>cbSize</c></b> member of this structure and the <b><c>uiParam</c></b> parameter to <b><c>sizeof(FILTERKEYS)</c></b>.
            /// </summary>
            SPI_GETFILTERKEYS = 0x0032,

            /// <summary>
            /// <b>Sets the parameters of the FilterKeys accessibility feature.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a FILTERKEYS structure that contains the new parameters.
            /// <br></br> • Set the <b><c>cbSize</c></b> member of this structure and the <b><c>uiParam</c></b> parameter to <b><c>sizeof(FILTERKEYS)</c></b>.
            /// </summary>
            SPI_SETFILTERKEYS = 0x0033,

            /// <summary>
            /// <b>Retrieves information about the ToggleKeys accessibility feature.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a TOGGLEKEYS structure that receives the information.
            /// <br></br> • Set the <b><c>cbSize</c></b> member of this structure and the <b><c>uiParam</c></b> parameter to <b><c>sizeof(TOGGLEKEYS)</c></b>.
            /// </summary>
            SPI_GETTOGGLEKEYS = 0x0034,

            /// <summary>
            /// <b>Sets the parameters of the ToggleKeys accessibility feature.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a TOGGLEKEYS structure that contains the new parameters.
            /// <br></br> • Set the <b><c>cbSize</c></b> member of this structure and the <b><c>uiParam</c></b> parameter to <b><c>sizeof(TOGGLEKEYS)</c></b>.
            /// </summary>
            SPI_SETTOGGLEKEYS = 0x0035,

            /// <summary>
            /// <b>Retrieves information about the MouseKeys accessibility feature.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a MOUSEKEYS structure that receives the information.
            /// <br></br> • Set the <b><c>cbSize</c></b> member of this structure and the <b><c>uiParam</c></b> parameter to <b><c>sizeof(MOUSEKEYS)</c></b>.
            /// </summary>
            SPI_GETMOUSEKEYS = 0x0036,

            /// <summary>
            /// <b>Sets the parameters of the MouseKeys accessibility feature.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a MOUSEKEYS structure that contains the new parameters.
            /// <br></br> • Set the <b><c>cbSize</c></b> member of this structure and the <b><c>uiParam</c></b> parameter to <b><c>sizeof(MOUSEKEYS)</c></b>.
            /// </summary>
            SPI_SETMOUSEKEYS = 0x0037,

            /// <summary>
            /// <b>Determines whether the Show Sounds accessibility flag is on or off.</b>
            /// <br></br>
            /// <br></br> • If it is on, the user requires an application to present information visually in situations where it would otherwise present the information only in audible form.
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives <b><c>true</c></b> if the feature is on, or <b><c>false</c></b> if it is off.
            /// <br></br> • Using this value is equivalent to calling GetSystemMetrics (<c>SM_SHOWSOUNDS</c>). That is the recommended call.
            /// </summary>
            SPI_GETSHOWSOUNDS = 0x0038,

            /// <summary>
            /// <b>Sets the parameters of the SoundSentry accessibility feature.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a SOUNDSENTRY structure that contains the new parameters.
            /// <br></br> • Set the <b><c>cbSize</c></b> member of this structure and the <b><c>uiParam</c></b> parameter to <b><c>sizeof(SOUNDSENTRY)</c></b>.
            /// </summary>
            SPI_SETSHOWSOUNDS = 0x0039,

            /// <summary>
            /// <b>Retrieves information about the StickyKeys accessibility feature.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a STICKYKEYS structure that receives the information.
            /// <br></br> • Set the <b><c>cbSize</c></b> member of this structure and the <b><c>uiParam</c></b> parameter to <b><c>sizeof(STICKYKEYS)</c></b>.
            /// </summary>
            SPI_GETSTICKYKEYS = 0x003A,

            /// <summary>
            /// <b>Sets the parameters of the StickyKeys accessibility feature.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a STICKYKEYS structure that contains the new parameters.
            /// <br></br> • Set the <b><c>cbSize</c></b> member of this structure and the <b><c>uiParam</c></b> parameter to <b><c>sizeof(STICKYKEYS)</c></b>.
            /// </summary>
            SPI_SETSTICKYKEYS = 0x003B,

            /// <summary>
            /// <b>Retrieves information about the time-out period associated with the accessibility features.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to an ACCESSTIMEOUT structure that receives the information.
            /// <br></br> • Set the <b><c>cbSize</c></b> member of this structure and the <b><c>uiParam</c></b> parameter to <b><c>sizeof(ACCESSTIMEOUT)</c></b>.
            /// </summary>
            SPI_GETACCESSTIMEOUT = 0x003C,

            /// <summary>
            /// <b>Sets the time-out period associated with the accessibility features.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to an ACCESSTIMEOUT structure that contains the new parameters.
            /// <br></br> • Set the <b><c>cbSize</c></b> member of this structure and the <b><c>uiParam</c></b> parameter to <b><c>sizeof(ACCESSTIMEOUT)</c></b>.
            /// </summary>
            SPI_SETACCESSTIMEOUT = 0x003D,

            //#if(WINVER >= 0x0400)
            /// <summary>
            /// <b>Windows Me/98/95: Retrieves information about the SerialKeys accessibility feature.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a SERIALKEYS structure that receives the information.
            /// <br></br> • Set the <b><c>cbSize</c></b> member of this structure and the <b><c>uiParam</c></b> parameter to <b><c>sizeof(SERIALKEYS)</c></b>.
            /// <br></br>
            /// <br></br> • <i> (!) Windows Server 2003, Windows XP/2000/NT: Not supported. The user controls this feature through the control panel.</i>
            /// </summary>
            SPI_GETSERIALKEYS = 0x003E,

            /// <summary>
            /// <b>Windows Me/98/95: Sets the parameters of the SerialKeys accessibility feature.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a SERIALKEYS structure that contains the new parameters.
            /// <br></br> • Set the <b><c>cbSize</c></b> member of this structure and the <b><c>uiParam</c></b> parameter to <b><c>sizeof(SERIALKEYS)</c></b>.
            /// <br></br>
            /// <br></br> • <i> (!) Windows Server 2003, Windows XP/2000/NT: Not supported. The user controls this feature through the control panel.</i>
            /// </summary>
            SPI_SETSERIALKEYS = 0x003F,

            /// <summary>
            /// <b>Retrieves information about the SoundSentry accessibility feature.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a SOUNDSENTRY structure that receives the information.
            /// <br></br> • Set the <b><c>cbSize</c></b> member of this structure and the <b><c>uiParam</c></b> parameter to <b><c>sizeof(SOUNDSENTRY)</c></b>.
            /// </summary>
            SPI_GETSOUNDSENTRY = 0x0040,

            /// <summary>
            /// <b>Sets the parameters of the SoundSentry accessibility feature.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a SOUNDSENTRY structure that contains the new parameters.
            /// <br></br> • Set the <b><c>cbSize</c></b> member of this structure and the <b><c>uiParam</c></b> parameter to <b><c>sizeof(SOUNDSENTRY)</c></b>.
            /// </summary>
            SPI_SETSOUNDSENTRY = 0x0041,

            //#if(_WIN32_WINNT >= 0x0400)
            /// <summary>
            /// <b>Determines whether the snap-to-default-button feature is enabled.</b>
            /// <br></br>
            /// <br></br> • If enabled, the mouse cursor automatically moves to the default button, such as OK or Apply, of a dialog box.
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives <b><c>true</c></b> if the feature is on, or <b><c>false</c></b> if it is off.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 95: Not supported.</i>
            /// </summary>
            SPI_GETSNAPTODEFBUTTON = 0x005F,

            /// <summary>
            /// <b>Enables or disables the snap-to-default-button feature.</b>
            /// <br></br>
            /// <br></br> • If enabled, the mouse cursor automatically moves to the default button, such as OK or Apply, of a dialog box.
            /// <br></br> • Set the <b><c>uiParam</c></b> parameter to <b><c>true</c></b> to enable the feature, or <b><c>false</c></b> to disable it.
            /// <br></br> • Applications should use the ShowWindow function when displaying a dialog box so the dialog manager can position the mouse cursor.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 95: Not supported.</i>
            /// </summary>
            SPI_SETSNAPTODEFBUTTON = 0x0060,

            /// <summary>
            /// <b>Retrieves the width, in pixels, of the rectangle within which the mouse pointer has to stay for <c>TrackMouseEvent</c></b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <c>UINT</c> variable that receives the width.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 95: Not supported.</i>
            /// </summary>
            SPI_GETMOUSEHOVERWIDTH = 0x0062,

            /// <summary>
            /// <b>Sets the width, in pixels, of the rectangle within which the mouse pointer has to stay for TrackMouseEvent</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <c>UINT</c> variable that receives the width.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 95: Not supported.</i>
            /// </summary>
            SPI_SETMOUSEHOVERWIDTH = 0x0063,

            /// <summary>
            /// <b>Retrieves the height, in pixels, of the rectangle within which the mouse pointer has to stay for TrackMouseEvent</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <c>UINT</c> variable that receives the height.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 95: Not supported.</i>
            /// </summary>
            SPI_GETMOUSEHOVERHEIGHT = 0x0064,

            /// <summary>
            /// <b>Sets the height, in pixels, of the rectangle within which the mouse pointer has to stay for TrackMouseEvent</b>
            /// <br></br>
            /// <br></br> • Set the <b><c>uiParam</c></b> parameter to the new height.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 95: Not supported.</i>
            /// </summary>
            SPI_SETMOUSEHOVERHEIGHT = 0x0065,

            /// <summary>
            /// <b>Retrieves the time, in milliseconds, that the mouse pointer has to stay in the hover rectangle for TrackMouseEvent</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <c>UINT</c> variable that receives the time.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 95: Not supported.</i>
            /// </summary>
            SPI_GETMOUSEHOVERTIME = 0x0066,

            /// <summary>
            /// <b>Sets the time, in milliseconds, that the mouse pointer has to stay in the hover rectangle for TrackMouseEvent</b>
            /// <br></br>
            /// <br></br> • This is used only if you pass HOVER_DEFAULT in the dwHoverTime parameter in the call to TrackMouseEvent.
            /// <br></br> • Set the <b><c>uiParam</c></b> parameter to the new time.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 95: Not supported.</i>
            /// </summary>
            SPI_SETMOUSEHOVERTIME = 0x0067,

            /// <summary>
            /// <b>Retrieves the number of lines to scroll when the mouse wheel is rotated.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <c>UINT</c> variable that receives the number of lines.
            /// <br></br> • The default value is 3.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 95: Not supported.</i>
            /// </summary>
            SPI_GETWHEELSCROLLLINES = 0x0068,

            /// <summary>
            /// <b>Sets the number of lines to scroll when the mouse wheel is rotated.</b>
            /// <br></br>
            /// <br></br> • The number of lines is set from the <b><c>uiParam</c></b> parameter.
            /// <br></br> • The number of lines is the suggested number of lines to scroll when the mouse wheel is rolled without using modifier keys.
            /// <br></br> • If the number is 0, then no scrolling should occur.
            /// <br></br> • If the number of lines to scroll is greater than the number of lines viewable,
            /// and in particular if it is WHEEL_PAGESCROLL (#defined as <c>UINT_MAX</c>),
            /// the scroll operation should be interpreted as clicking once in the page down or page up regions of the scroll bar.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 95: Not supported.</i>
            /// </summary>
            SPI_SETWHEELSCROLLLINES = 0x0069,

            /// <summary>
            /// <b>Retrieves the time, in milliseconds, that the system waits before displaying a shortcut menu when the mouse cursor is</b>
            /// <br></br>
            /// <br></br> • Over a submenu item.
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <c>DWORD</c> variable that receives the time of the delay.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 95: Not supported.</i>
            /// </summary>
            SPI_GETMENUSHOWDELAY = 0x006A,

            /// <summary>
            /// <b>Sets the time, in milliseconds, that the system waits before displaying a shortcut menu when the mouse cursor is</b>
            /// <br></br>
            /// <br></br> • Over a submenu item.
            /// <br></br> • Set <b><c>uiParam</c></b> to the time.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 95: Not supported.</i>
            /// </summary>
            SPI_SETMENUSHOWDELAY = 0x006B,

            /// <summary>
            /// <b>Determines whether the IME status window is visible (on a per-user basis).</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives <b><c>true</c></b> if the status window is visible, or <b><c>false</c></b> if it is not.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows 95: This value is not supported.</i>
            /// </summary>
            SPI_GETSHOWIMEUI = 0x006E,

            /// <summary>
            /// <b>Sets whether the IME status window is visible or not on a per-user basis.</b>
            /// <br></br>
            /// <br></br> • The <b><c>uiParam</c></b> parameter specifies <b><c>true</c></b> for on or <b><c>false</c></b> for off.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows 95: This value is not supported.</i>
            /// </summary>
            SPI_SETSHOWIMEUI = 0x006F,

            /// <summary>
            /// <b>Retrieves the current mouse speed.</b>
            /// <br></br>
            /// <br></br> • The mouse speed determines how far the pointer will move based on the distance the mouse moves.
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to an integer that receives a value which ranges between <c>1</c> (slowest) and 20 (fastest).
            /// <br></br> • A value of 10 is the default.
            /// <br></br> • The value can be set by an end user using the mouse control panel application or by an application using <c>SPI_SETMOUSESPEED</c>.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows 95: This value is not supported.</i>
            /// </summary>
            SPI_GETMOUSESPEED = 0x0070,

            /// <summary>
            /// <b>Sets the current mouse speed.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter is an integer between <c>1</c> (slowest) and 20 (fastest).
            /// <br></br> • A value of 10 is the default.
            /// <br></br> • This value is typically set using the mouse control panel application.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows 95: This value is not supported.</i>
            /// </summary>
            SPI_SETMOUSESPEED = 0x0071,

            /// <summary>
            /// <b>Determines whether a screen saver is currently running on the window station of the calling process.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives <b><c>true</c></b> if a screen saver is currently running, or <b><c>false</c></b> otherwise.
            /// <br></br> • Note that only the interactive window station, "WinSta0", can have a screen saver running.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows 95: This value is not supported.</i>
            /// </summary>
            SPI_GETSCREENSAVERRUNNING = 0x0072,

            /// <summary>
            /// <b>Retrieves the full path of the bitmap file for the desktop wallpaper.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a buffer that receives a null-terminated path string.
            /// <br></br> • Set the <b><c>uiParam</c></b> parameter to the size, in characters, of the <b><c>pvParam</c></b> buffer.
            /// <br></br> • The returned string will not exceed MAX_PATH characters. If there is no desktop wallpaper, the returned string is empty.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows Me/98/95: This value is not supported.</i>
            /// </summary>
            SPI_GETDESKWALLPAPER = 0x0073,

            /// <summary>
            /// <b>Determines whether active window tracking (activating the window the mouse is on) is on or off.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives <b><c>true</c></b> for on, or <b><c>false</c></b> for off.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows 95: This value is not supported.</i>
            /// </summary>
            SPI_GETACTIVEWINDOWTRACKING = 0x1000,

            /// <summary>
            /// <b>Sets active window tracking (activating the window the mouse is on) either on or off.</b>
            /// <br></br>
            /// <br></br> • Set <b><c>pvParam</c></b> to <b><c>true</c></b> for on or <b><c>false</c></b> for off.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows 95: This value is not supported.</i>
            /// </summary>
            SPI_SETACTIVEWINDOWTRACKING = 0x1001,

            /// <summary>
            /// <b>Determines whether the menu animation feature is enabled.</b>
            /// <br></br>
            /// <br></br> • This master switch must be on to enable menu animation effects.
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives <b><c>true</c></b> if animation is enabled and <b><c>false</c></b> if it is disabled.
            /// <br></br> • If animation is enabled, <c>SPI_GETMENUFADE</c> indicates whether menus use fade or slide animation.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows 95: This value is not supported</i>.
            /// </summary>
            SPI_GETMENUANIMATION = 0x1002,

            /// <summary>
            /// <b>Enables or disables menu animation.</b>
            /// <br></br>
            /// <br></br> • This master switch must be on for any menu animation to occur.
            /// <br></br> • The <b><c>pvParam</c></b> parameter is a <b><c>bool</c></b> variable; set <b><c>pvParam</c></b> to <b><c>true</c></b> to enable animation and <b><c>false</c></b> to disable animation.
            /// <br></br> • If animation is enabled, <c>SPI_GETMENUFADE</c> indicates whether menus use fade or slide animation.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows 95: This value is not supported.</i>
            /// </summary>
            SPI_SETMENUANIMATION = 0x1003,

            /// <summary>
            /// <b>Determines whether the slide-open effect for combo boxes is enabled.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives <b><c>true</c></b> for enabled, or <b><c>false</c></b> for disabled.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows 95: This value is not supported.</i>
            /// </summary>
            SPI_GETCOMBOBOXANIMATION = 0x1004,

            /// <summary>
            /// <b>Enables or disables the slide-open effect for combo boxes.</b>
            /// <br></br>
            /// <br></br> • Set the <b><c>pvParam</c></b> parameter to <b><c>true</c></b> to enable the gradient effect, or <b><c>false</c></b> to disable it.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows 95: This value is not supported.</i>
            /// </summary>
            SPI_SETCOMBOBOXANIMATION = 0x1005,

            /// <summary>
            /// <b>Determines whether the smooth-scrolling effect for list boxes is enabled.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives <b><c>true</c></b> for enabled, or <b><c>false</c></b> for disabled.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows 95: This value is not supported.</i>
            /// </summary>
            SPI_GETLISTBOXSMOOTHSCROLLING = 0x1006,

            /// <summary>
            /// <b>Enables or disables the smooth-scrolling effect for list boxes.</b>
            /// <br></br>
            /// <br></br> • Set the <b><c>pvParam</c></b> parameter to <b><c>true</c></b> to enable the smooth-scrolling effect, or <b><c>false</c></b> to disable it.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows 95: This value is not supported.</i>
            /// </summary>
            SPI_SETLISTBOXSMOOTHSCROLLING = 0x1007,

            /// <summary>
            /// <b>Determines whether the gradient effect for window title bars is enabled.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives <b><c>true</c></b> for enabled, or <b><c>false</c></b> for disabled.
            /// <br></br> • For more information about the gradient effect, see the GetSysColor function.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows 95: This value is not supported.</i>
            /// </summary>
            SPI_GETGRADIENTCAPTIONS = 0x1008,

            /// <summary>
            /// <b>Enables or disables the gradient effect for window title bars.</b>
            /// <br></br>
            /// <br></br> • Set the <b><c>pvParam</c></b> parameter to <b><c>true</c></b> to enable it, or <b><c>false</c></b> to disable it.
            /// <br></br> • The gradient effect is possible only if the system has a color depth of more than <c>256</c> colors.
            /// <br></br> • For more information about the gradient effect, see the GetSysColor function.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows 95: This value is not supported.</i>
            /// </summary>
            SPI_SETGRADIENTCAPTIONS = 0x1009,

            /// <summary>
            /// <b>Determines whether menu access keys are always underlined.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives <b><c>true</c></b> if menu access keys are always underlined,
            /// <br></br>   and <b><c>false</c></b> if they are underlined only when the menu is activated by the keyboard.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows 95: This value is not supported.</i>
            /// </summary>
            SPI_GETKEYBOARDCUES = 0x100A,

            /// <summary>
            /// <b>Sets the underlining of menu access key letters.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter is a <b><c>bool</c></b> variable. Set <b><c>pvParam</c></b> to <b><c>true</c></b> to always underline menu access keys,
            /// <br></br>   or <b><c>false</c></b> to underline menu access keys only when the menu is activated from the keyboard.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows 95: This value is not supported.</i>
            /// </summary>
            SPI_SETKEYBOARDCUES = 0x100B,

            /// <summary>
            /// <b>Same as SPI_GETKEYBOARDCUES.</b>
            /// </summary>
            SPI_GETMENUUNDERLINES = SPI_GETKEYBOARDCUES,

            /// <summary>
            /// <b>Same as SPI_SETKEYBOARDCUES.</b>
            /// </summary>
            SPI_SETMENUUNDERLINES = SPI_SETKEYBOARDCUES,

            /// <summary>
            /// <b>Determines whether windows activated through active window tracking (activating the window the mouse is on) will be brought to the top.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives <b><c>true</c></b> for on, or <b><c>false</c></b> for off.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows 95: This value is not supported.</i>
            /// </summary>
            SPI_GETACTIVEWNDTRKZORDER = 0x100C,

            /// <summary>
            /// <b>Determines whether or not windows activated through active window tracking should be brought to the top.</b>
            /// <br></br>
            /// <br></br> • Set <b><c>pvParam</c></b> to <b><c>true</c></b> for on or <b><c>false</c></b> for off.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows 95: This value is not supported.</i>
            /// </summary>
            SPI_SETACTIVEWNDTRKZORDER = 0x100D,

            /// <summary>
            /// <b>Determines whether hot tracking of user-interface elements, such as menu names on menu bars, is enabled.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives <b><c>true</c></b> for enabled, or <b><c>false</c></b> for disabled.
            /// <br></br> • Hot tracking means that when the cursor moves over an item, it is highlighted but not selected.
            /// <br></br> • You can query this value to decide whether to use hot tracking in the user interface of your application.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows 95: This value is not supported.</i>
            /// </summary>
            SPI_GETHOTTRACKING = 0x100E,

            /// <summary>
            /// <b>Enables or disables hot tracking of user-interface elements such as menu names on menu bars.</b>
            /// <br></br>
            /// <br></br> • Set the <b><c>pvParam</c></b> parameter to <b><c>true</c></b> to enable it, or <b><c>false</c></b> to disable it.
            /// <br></br> • Hot-tracking means that when the cursor moves over an item, it is highlighted but not selected.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows 95: This value is not supported.</i>
            /// </summary>
            SPI_SETHOTTRACKING = 0x100F,

            /// <summary>
            /// <b>Determines whether menu fade animation is enabled.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives TRUE
            /// <br></br>   when fade animation is enabled and <b><c>false</c></b> when it is disabled.
            /// <br></br> • If fade animation is disabled, menus use slide animation.
            /// <br></br> • This flag is ignored unless menu animation is enabled, which you can do using the <c>SPI_SETMENUANIMATION</c> flag.
            /// <br></br> • For more information, see AnimateWindow.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows Me/98/95: This value is not supported.</i>
            /// </summary>
            SPI_GETMENUFADE = 0x1012,

            /// <summary>
            /// <b>Enables or disables menu fade animation.</b>
            /// <br></br>
            /// <br></br> • Set <b><c>pvParam</c></b> to <b><c>true</c></b> to enable the menu fade effect or <b><c>false</c></b> to disable it.
            /// <br></br> • If fade animation is disabled, menus use slide animation.
            /// <br></br> • The menu fade effect is possible only if the system
            /// <br></br>   has a color depth of more than <c>256</c> colors.
            /// <br></br> • This flag is ignored unless <c>SPI_MENUANIMATION</c> is also set.
            /// <br></br>   For more information, see AnimateWindow.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows Me/98/95: This value is not supported.</i>
            /// </summary>
            SPI_SETMENUFADE = 0x1013,

            /// <summary>
            /// <b>Determines whether the selection fade effect is enabled.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives TRUE
            /// <br></br>   if enabled or <b><c>false</c></b> if disabled.
            /// <br></br> • The selection fade effect causes the menu item selected by the user to remain on the screen briefly while fading out
            /// <br></br>   after the menu is dismissed.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows Me/98/95: This value is not supported.</i>
            /// </summary>
            SPI_GETSELECTIONFADE = 0x1014,

            /// <summary>
            /// <b>Set <b><c>pvParam</c></b> to <b><c>true</c></b> to enable the selection fade effect or <b><c>false</c></b> to disable it.</b>
            /// <br></br>
            /// <br></br> • The selection fade effect causes the menu item selected by the user to remain on the screen briefly while fading out
            /// <br></br>   after the menu is dismissed.
            /// <br></br> • The selection fade effect is possible only if the system has a color depth of more than <c>256</c> colors.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows Me/98/95: This value is not supported.</i>
            /// </summary>
            SPI_SETSELECTIONFADE = 0x1015,

            /// <summary>
            /// <b>Determines whether ToolTip animation is enabled.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives TRUE
            /// <br></br>   if enabled or <b><c>false</c></b> if disabled.
            /// <br></br> • If ToolTip animation is enabled, <c>SPI_GETTOOLTIPFADE</c> indicates whether ToolTips use fade or slide animation.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows Me/98/95: This value is not supported.</i>
            /// </summary>
            SPI_GETTOOLTIPANIMATION = 0x1016,

            /// <summary>
            /// <b>Set <b><c>pvParam</c></b> to <b><c>true</c></b> to enable ToolTip animation or <b><c>false</c></b> to disable it.</b>
            /// <br></br>
            /// <br></br> • If enabled, you can use <c>SPI_SETTOOLTIPFADE</c>
            /// <br></br>   to specify fade or slide animation.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows Me/98/95: This value is not supported.</i>
            /// </summary>
            SPI_SETTOOLTIPANIMATION = 0x1017,

            /// <summary>
            /// <b>If <c>SPI_SETTOOLTIPANIMATION</c> is enabled, <c>SPI_GETTOOLTIPFADE</c> indicates whether ToolTip animation uses a fade effect or a slide effect.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives <b><c>true</c></b> for fade animation or <b><c>false</c></b> for slide animation.
            /// <br></br> • For more information on slide and fade effects, see AnimateWindow.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows Me/98/95: This value is not supported.</i>
            /// </summary>
            SPI_GETTOOLTIPFADE = 0x1018,

            /// <summary>
            /// <b>If the <c>SPI_SETTOOLTIPANIMATION</c> flag is enabled, use <c>SPI_SETTOOLTIPFADE</c> to indicate whether ToolTip animation uses a fade effect
            /// or a slide effect.</b>
            /// <br></br>
            /// <br></br> • Set <b><c>pvParam</c></b> to <b><c>true</c></b> for fade animation or <b><c>false</c></b> for slide animation.
            /// <br></br> • The tooltip fade effect is possible only
            /// <br></br>   if the system has a color depth of more than <c>256</c> colors.
            /// <br></br> • For more information on the slide and fade effects,
            /// <br></br>   see the AnimateWindow function.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows Me/98/95: This value is not supported.</i>
            /// </summary>
            SPI_SETTOOLTIPFADE = 0x1019,

            /// <summary>
            /// <b>Determines whether the cursor has a shadow around it.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives TRUE
            /// <br></br>   if the shadow is enabled, <b><c>false</c></b> if it is disabled.
            /// <br></br> • This effect appears only if the system has a color depth of more than <c>256</c> colors.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows Me/98/95: This value is not supported.</i>
            /// </summary>
            SPI_GETCURSORSHADOW = 0x101A,

            /// <summary>
            /// <b>Enables or disables a shadow around the cursor.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter is a <b><c>bool</c></b> variable.
            /// <br></br>   Set <b><c>pvParam</c></b> to <b><c>true</c></b> to enable the shadow
            /// <br></br>   or <b><c>false</c></b> to disable the shadow.
            /// <br></br> • This effect appears only if the system has a color depth of more than <c>256</c> colors.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows Me/98/95: This value is not supported.</i>
            /// </summary>
            SPI_SETCURSORSHADOW = 0x101B,

            //#if(_WIN32_WINNT >= 0x0501)
            /// <summary>
            /// <b>Retrieves the state of the Mouse Sonar feature.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives TRUE
            /// <br></br>   if enabled or <b><c>false</c></b> otherwise.
            /// <br></br> • For more information, see About Mouse Input on MSDN.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 2000/NT, Windows 98/95: This value is not supported.</i>
            /// </summary>
            SPI_GETMOUSESONAR = 0x101C,

            /// <summary>
            /// <b>Turns the Sonar accessibility feature on or off.</b>
            /// <br></br>
            /// <br></br> • This feature briefly shows several concentric circles around the mouse pointer
            /// <br></br>   when the user presses and releases the CTRL key.
            /// <br></br> • The <b><c>pvParam</c></b> parameter specifies <b><c>true</c></b> for on and <b><c>false</c></b> for off.
            /// <br></br> • The default is off.
            /// <br></br> • For more information, see About Mouse Input.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 2000/NT, Windows 98/95: This value is not supported.</i>
            /// </summary>
            SPI_SETMOUSESONAR = 0x101D,

            /// <summary>
            /// <b>Retrieves the state of the Mouse ClickLock feature.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives TRUE
            /// <br></br>   if enabled, or <b><c>false</c></b> otherwise.
            /// <br></br> • For more information, see About Mouse Input.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 2000/NT, Windows 98/95: This value is not supported.</i>
            /// </summary>
            SPI_GETMOUSECLICKLOCK = 0x101E,

            /// <summary>
            /// <b>Turns the Mouse ClickLock accessibility feature on or off.</b>
            /// <br></br>
            /// <br></br> • This feature temporarily locks down the primary mouse button
            /// <br></br>   when that button is clicked and held down for the time specified by <c>SPI_SETMOUSECLICKLOCKTIME</c>.
            /// <br></br> • The <b><c>uiParam</c></b> parameter specifies <b><c>true</c></b> for on,
            /// <br></br>   or <b><c>false</c></b> for off.
            /// <br></br> • The default is off.
            /// <br></br> • For more information, see Remarks and About Mouse Input on MSDN.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 2000/NT, Windows 98/95: This value is not supported.</i>
            /// </summary>
            SPI_SETMOUSECLICKLOCK = 0x101F,

            /// <summary>
            /// <b>Retrieves the state of the Mouse Vanish feature.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives TRUE
            /// <br></br>   if enabled or <b><c>false</c></b> otherwise.
            /// <br></br> • For more information, see About Mouse Input on MSDN.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 2000/NT, Windows 98/95: This value is not supported.</i>
            /// </summary>
            SPI_GETMOUSEVANISH = 0x1020,

            /// <summary>
            /// <b>Turns the Vanish feature on or off.</b>
            /// <br></br>
            /// <br></br> • This feature hides the mouse pointer when the user types; the pointer reappears
            /// <br></br>   when the user moves the mouse.
            /// <br></br> • The <b><c>pvParam</c></b> parameter specifies <b><c>true</c></b> for on and <b><c>false</c></b> for off.
            /// <br></br> • The default is off.
            /// <br></br> • For more information, see About Mouse Input on MSDN.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 2000/NT, Windows 98/95: This value is not supported.</i>
            /// </summary>
            SPI_SETMOUSEVANISH = 0x1021,

            /// <summary>
            /// <b>Determines whether native User menus have _flat menu appearance.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable
            /// <br></br>   that returns <b><c>true</c></b> if the _flat menu appearance is set, or <b><c>false</c></b> otherwise.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 2000/NT, Windows Me/98/95: This value is not supported.</i>
            /// </summary>
            SPI_GETFLATMENU = 0x1022,

            /// <summary>
            /// <b>Enables or disables _flat menu appearance for native User menus.</b>
            /// <br></br>
            /// <br></br> • Set <b><c>pvParam</c></b> to <b><c>true</c></b> to enable _flat menu appearance
            /// <br></br>   or <b><c>false</c></b> to disable it.
            /// <br></br> • When enabled, the menu bar uses COLOR_MENUBAR for the menubar background, COLOR_MENU for the menu-popup background,
            /// <br></br>   COLOR_MENUHILIGHT for the fill of the current menu selection, and COLOR_HILIGHT for the outline of the current menu selection.
            /// <br></br> • If disabled, menus are drawn using the same metrics and colors as in Windows 2000 and earlier.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 2000/NT, Windows Me/98/95: This value is not supported.</i>
            /// </summary>
            SPI_SETFLATMENU = 0x1023,

            /// <summary>
            /// <b>Determines whether the drop shadow effect is enabled.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that returns TRUE
            /// <br></br>   if enabled or <b><c>false</c></b> if disabled.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 2000/NT, Windows Me/98/95: This value is not supported.</i>
            /// </summary>
            SPI_GETDROPSHADOW = 0x1024,

            /// <summary>
            /// <b>Enables or disables the drop shadow effect.</b>
            /// <br></br>
            /// <br></br> • Set <b><c>pvParam</c></b> to <b><c>true</c></b> to enable the drop shadow effect or <b><c>false</c></b> to disable it.
            /// <br></br> • You must also have CS_DROPSHADOW in the window class style.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 2000/NT, Windows Me/98/95: This value is not supported.</i>
            /// </summary>
            SPI_SETDROPSHADOW = 0x1025,

            /// <summary>
            /// <b>Retrieves a <b><c>bool</c></b> indicating whether an application can reset the screensaver's timer by calling the SendInput function
            /// to simulate keyboard or mouse input.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives TRUE
            /// <br></br>   if the simulated input will be blocked, or <b><c>false</c></b> otherwise.
            /// </summary>
            SPI_GETBLOCKSENDINPUTRESETS = 0x1026,

            /// <summary>
            /// <b>Determines whether an application can reset the screensaver's timer by calling the SendInput function to simulate keyboard
            /// or mouse input.</b>
            /// <br></br>
            /// <br></br> • The <b><c>uiParam</c></b> parameter specifies <b><c>true</c></b> if the screensaver will not be deactivated by simulated input,
            /// <br></br>   or <b><c>false</c></b> if the screensaver will be deactivated by simulated input.
            /// </summary>
            SPI_SETBLOCKSENDINPUTRESETS = 0x1027,

            /// <summary>
            /// <b>Determines whether UI effects are enabled or disabled.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <b><c>bool</c></b> variable that receives TRUE
            /// <br></br>   if all UI effects are enabled, or <b><c>false</c></b> if they are disabled.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows Me/98/95: This value is not supported.</i>
            /// </summary>
            SPI_GETUIEFFECTS = 0x103E,

            /// <summary>
            /// <b>Enables or disables UI effects.</b>
            /// <br></br>
            /// <br></br> • Set the <b><c>pvParam</c></b> parameter to <b><c>true</c></b> to enable all UI effects or <b><c>false</c></b> to disable all UI effects.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows Me/98/95: This value is not supported.</i>
            /// </summary>
            SPI_SETUIEFFECTS = 0x103F,

            /// <summary>
            /// Retrieves the current state of client area animations.
            /// <br></br>
            /// <br></br> • Set the <b><c>pvParam</c></b> parameter to receive the current state.
            /// </summary>
            SPI_GETCLIENTAREAANIMATION = 0x1042,

            /// <summary>
            /// Enables or disables client area animations.
            /// <br></br>
            /// <br></br> • Set the <b><c>pvParam</c></b> parameter to <b><c>true</c></b> to enable client area animations or <b><c>false</c></b> to disable them.
            /// </summary>
            SPI_SETCLIENTAREAANIMATION = 0x1043,

            /// <summary>
            /// <b>Retrieves the amount of time following user input, in milliseconds, during which the system will not allow applications
            /// to force themselves into the foreground.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <c>DWORD</c> variable that receives the time.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows 95: This value is not supported.</i>
            /// </summary>
            SPI_GETFOREGROUNDLOCKTIMEOUT = 0x2000,

            /// <summary>
            /// <b>Sets the amount of time following user input, in milliseconds, during which the system does not allow applications
            /// to force themselves into the foreground.</b>
            /// <br></br>
            /// <br></br> • Set <b><c>pvParam</c></b> to the new timeout value.
            /// <br></br> • The calling thread must be able to change the foreground window, otherwise the call fails.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows 95: This value is not supported.</i>
            /// </summary>
            SPI_SETFOREGROUNDLOCKTIMEOUT = 0x2001,

            /// <summary>
            /// <b>Retrieves the active window tracking delay, in milliseconds.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <c>DWORD</c> variable that receives the time.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows 95: This value is not supported.</i>
            /// </summary>
            SPI_GETACTIVEWNDTRKTIMEOUT = 0x2002,

            /// <summary>
            /// <b>Sets the active window tracking delay.</b>
            /// <br></br>
            /// <br></br> • Set <b><c>pvParam</c></b> to the number of milliseconds to delay before activating the window under the mouse pointer.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows 95: This value is not supported.</i>
            /// </summary>
            SPI_SETACTIVEWNDTRKTIMEOUT = 0x2003,

            /// <summary>
            /// <b>Retrieves the number of times SetForegroundWindow will flash the taskbar button when rejecting a foreground switch request.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <c>DWORD</c> variable that receives the value.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows 95: This value is not supported.</i>
            /// </summary>
            SPI_GETFOREGROUNDFLASHCOUNT = 0x2004,

            /// <summary>
            /// <b>Sets the number of times SetForegroundWindow will flash the taskbar button when rejecting a foreground switch request.</b>
            /// <br></br>
            /// <br></br> • Set <b><c>pvParam</c></b> to the number of times to flash.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows 95: This value is not supported.</i>
            /// </summary>
            SPI_SETFOREGROUNDFLASHCOUNT = 0x2005,

            /// <summary>
            /// <b>Retrieves the caret width in edit controls, in pixels.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <c>DWORD</c> that receives this value.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows Me/98/95: This value is not supported.</i>
            /// </summary>
            SPI_GETCARETWIDTH = 0x2006,

            /// <summary>
            /// <b>Sets the caret width in edit controls.</b>
            /// <br></br>
            /// <br></br> • Set <b><c>pvParam</c></b> to the desired width, in pixels.
            /// <br></br> • The default and minimum value is <c>1</c>.
            /// <br></br>
            /// <br></br> • <i> (!) Windows NT, Windows Me/98/95: This value is not supported.</i>
            /// </summary>
            SPI_SETCARETWIDTH = 0x2007,

            /// <summary>
            /// <b>Retrieves the time delay before the primary mouse button is locked.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to <c>DWORD</c> that receives the time delay.
            /// <br></br> • This is only enabled if <c>SPI_SETMOUSECLICKLOCK</c> is set to TRUE.
            /// <br></br> • For more information, see About Mouse Input on MSDN.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 2000/NT, Windows 98/95: This value is not supported.</i>
            /// </summary>
            SPI_GETMOUSECLICKLOCKTIME = 0x2008,

            /// <summary>
            /// <b>Turns the Mouse ClickLock accessibility feature on or off.</b>
            /// <br></br>
            /// <br></br> • This feature temporarily locks down the primary mouse button
            /// <br></br>   when that button is clicked and held down for the time specified by <c>SPI_SETMOUSECLICKLOCKTIME</c>.
            /// <br></br> • The <b><c>uiParam</c></b> parameter specifies <b><c>true</c></b> for on, or <b><c>false</c></b> for off.
            /// <br></br> • The default is off.
            /// <br></br> • For more information, see Remarks and About Mouse Input on MSDN.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 2000/NT, Windows 98/95: This value is not supported.</i>
            /// </summary>
            SPI_SETMOUSECLICKLOCKTIME = 0x2009,

            /// <summary>
            /// <b>Retrieves the type of font smoothing.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <c>UINT</c> that receives the information.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 2000/NT, Windows Me/98/95: This value is not supported.</i>
            /// </summary>
            SPI_GETFONTSMOOTHINGTYPE = 0x200A,

            /// <summary>
            /// <b>Sets the font smoothing type.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter points to a <c>UINT</c> that contains either FE_FONTSMOOTHINGSTANDARD,
            /// <br></br>   if standard anti-aliasing is used, or FE_FONTSMOOTHINGCLEARTYPE, if ClearType is used.
            /// <br></br> • The default is FE_FONTSMOOTHINGSTANDARD.
            /// <br></br> • When using this option, the fWinIni parameter must be set to <c>SPIF_SENDWININICHANGE | SPIF_UPDATEINIFILE</c>;
            /// <br></br>   otherwise, SystemParametersInfo fails.
            /// </summary>
            SPI_SETFONTSMOOTHINGTYPE = 0x200B,

            /// <summary>
            /// <b>Retrieves a contrast value that is used in ClearType™ smoothing.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <c>UINT</c> that receives the information.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 2000/NT, Windows Me/98/95: This value is not supported.</i>
            /// </summary>
            SPI_GETFONTSMOOTHINGCONTRAST = 0x200C,

            /// <summary>
            /// <b>Sets the contrast value used in ClearType smoothing.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter points to a <c>UINT</c> that holds the contrast value.
            /// <br></br> • Valid contrast values are from 1000 to 2200. The default value is 1400.
            /// <br></br> • When using this option, the fWinIni parameter must be set to <c>SPIF_SENDWININICHANGE | SPIF_UPDATEINIFILE</c>;
            /// <br></br>   otherwise, SystemParametersInfo fails.
            /// <br></br> • <c>SPI_SETFONTSMOOTHINGTYPE</c> must also be set to <c>FE_FONTSMOOTHINGCLEARTYPE</c>.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 2000/NT, Windows Me/98/95: This value is not supported.</i>
            /// </summary>
            SPI_SETFONTSMOOTHINGCONTRAST = 0x200D,

            /// <summary>
            /// <b>Retrieves the width, in pixels, of the left and right edges of the focus rectangle drawn with DrawFocusRect.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <c>UINT</c>.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 2000/NT, Windows Me/98/95: This value is not supported.</i>
            /// </summary>
            SPI_GETFOCUSBORDERWIDTH = 0x200E,

            /// <summary>
            /// <b>Sets the height of the left and right edges of the focus rectangle drawn with DrawFocusRect to the value of the <b><c>pvParam</c></b> parameter.</b>
            /// <br></br>
            /// <br></br> • <i> (!) Windows 2000/NT, Windows Me/98/95: This value is not supported.</i>
            /// </summary>
            SPI_SETFOCUSBORDERWIDTH = 0x200F,

            /// <summary>
            /// <b>Retrieves the height, in pixels, of the top and bottom edges of the focus rectangle drawn with DrawFocusRect.</b>
            /// <br></br>
            /// <br></br> • The <b><c>pvParam</c></b> parameter must point to a <c>UINT</c>.
            /// <br></br>
            /// <br></br> • <i> (!) Windows 2000/NT, Windows Me/98/95: This value is not supported.</i>
            /// </summary>
            SPI_GETFOCUSBORDERHEIGHT = 0x2010,

            /// <summary>
            /// <b>Sets the height of the top and bottom edges of the focus rectangle drawn with DrawFocusRect to the value of the <b><c>pvParam</c></b> parameter.</b>
            /// <br></br>
            /// <br></br> • <i> (!) Windows 2000/NT, Windows Me/98/95: This value is not supported.</i>
            /// </summary>
            SPI_SETFOCUSBORDERHEIGHT = 0x2011,

            /// <summary>
            /// <b>Not implemented.</b>
            /// </summary>
            SPI_GETFONTSMOOTHINGORIENTATION = 0x2012,

            /// <summary>
            /// <b>Not implemented.</b>
            /// </summary>
            SPI_SETFONTSMOOTHINGORIENTATION = 0x2013,
        }
    }
}
