using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using static WinPaletter.Metrics;

namespace WinPaletter.NativeMethods
{
    public class Dwmapi
    {

        [DllImport("dwmapi.dll", EntryPoint = "#131", PreserveSig = false)]
        public static extern void DwmSetColorizationParameters(ref DWM_COLORIZATION_PARAMS parameters, bool unknown);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref bool enabled);

        [DllImport("dwmapi")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi")]
        internal static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        internal static extern int DwmSetWindowAttribute(IntPtr hwnd, DWMATTRIB dwAttribute, ref int pvAttribute, int cbAttribute);

        public enum DWMATTRIB : int
        {
            SYSTEMBACKDROP_TYPE = 38,
            MICA_EFFECT = 1029,
            USE_IMMERSIVE_DARK_MODE = 20,
            WINDOW_CORNER_PREFERENCE = 33,
            TEXT_COLOR = 36,
            CAPTION_COLOR = 35,
            BORDER_COLOR = 34
        }
        public enum FormCornersType
        {
            Default,
            Rectangular,
            Round,
            SmallRound
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
        public struct DWM_COLORIZATION_PARAMS
        {
            public int clrColor;
            public int clrAfterGlow;
            public int nIntensity;
            public int clrAfterGlowBalance;
            public int clrBlurBalance;
            public int clrGlassReflectionIntensity;
            public bool fOpaque;
        }

        public const int CS_DROPSHADOW = 0x20000;
        public const int WM_NCPAINT = 0x85;

    }

    public class User32
    {
        [DllImport("user32.dll")]
        public static extern int LoadCursor(int hInstance, int lpCursorName);
        [DllImport("user32.dll")]
        public static extern int SetCursor(int hCursor);
        [DllImport("user32.dll")]
        public static extern bool AnimateWindow(IntPtr hWnd, int time, AnimateWindowFlags flags);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageTimeout(IntPtr hWnd, uint Msg, UIntPtr wParam, IntPtr lParam, SendMessageTimeoutFlags fuFlags, uint uTimeout, out UIntPtr lpdwResult);
        [DllImport("user32.dll")]
        public static extern bool SetSystemCursor(IntPtr hcur, int id);
        [DllImport("user32.dll", EntryPoint = "DestroyIcon")]
        public static extern bool DestroyIcon(IntPtr hIcon);
        [DllImport("user32.dll")]
        public static extern bool SetSysColors(int cElements, int[] lpaElements, uint[] lpaRgbValues);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(int uAction, int uParam, ref NONCLIENTMETRICS lpvParam, SPIF fuWinIni);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(int uAction, int uParam, ref ICONMETRICS lpvParam, SPIF fuWinIni);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(int uAction, int uParam, ref ANIMATIONINFO lpvParam, SPIF fuWinIni);

        [DllImport("user32", EntryPoint = "SystemParametersInfoA")]
        public static extern int SystemParametersInfo(int uAction, int uParam, int lpvParam, int fuWinIni);
        [DllImport("user32", EntryPoint = "SystemParametersInfoA")]
        public static extern int SystemParametersInfo(int uAction, int uParam, uint lpvParam, int fuWinIni);
        [DllImport("user32", EntryPoint = "SystemParametersInfoA")]
        public static extern int SystemParametersInfo(int uAction, uint uParam, int lpvParam, int fuWinIni);
        [DllImport("user32", EntryPoint = "SystemParametersInfoA")]
        public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
        [DllImport("user32", EntryPoint = "SystemParametersInfoA")]
        public static extern int SystemParametersInfo(int uAction, int uParam, bool lpvParam, int fuWinIni);
        [DllImport("user32", EntryPoint = "SystemParametersInfoA")]
        public static extern int SystemParametersInfo(int uAction, bool uParam, int lpvParam, int fuWinIni);

        public class Fixer
        {
            /// <summary>
            /// It is used outside global user32 to fix issue of not remembring settings
            /// </summary>
            [DllImport("user32", EntryPoint = "SystemParametersInfoA")]
            public static extern int SystemParametersInfo(int uAction, int uParam, ref bool lpvParam, int fuWinIni);
            [DllImport("user32", EntryPoint = "SystemParametersInfoA")]
            public static extern int SystemParametersInfo(int uAction, int uParam, ref int lpvParam, int fuWinIni);
            [DllImport("user32", EntryPoint = "SystemParametersInfoA")]
            public static extern int SystemParametersInfo(int uAction, int uParam, ref uint lpvParam, int fuWinIni);

        }

        /// <summary>
        /// SPI: System-wide parameter - Used in SystemParametersInfo function
        /// </summary>
        public class SPI
        {

            public enum Icons : int
            {
                /// <summary>
                /// <b>Sets or retrieves the width, in pixels, of an icon cell. The system uses this rectangle to arrange icons in large icon view.</b>
                /// <br></br>
                /// <br></br> • To set this value, set uiParam to the new value and set pvParam to null. You cannot set this value to less than SM_CXICON.
                /// <br></br> • To retrieve this value, pvParam must point to an integer that receives the current value.
                /// </summary>
                ICONHORIZONTALSPACING = 0xD,

                /// <summary>
                /// <b>Sets or retrieves the height, in pixels, of an icon cell.</b>
                /// <br></br>
                /// <br></br> • To set this value, set uiParam to the new value and set pvParam to null. You cannot set this value to less than SM_CYICON.
                /// <br></br> • To retrieve this value, pvParam must point to an integer that receives the current value.
                /// </summary>
                ICONVERTICALSPACING = 0x18,

                /// <summary>
                /// <b>Retrieves the logical font information for the current icon-title font.</b>
                /// <br></br>
                /// <br></br> • The uiParam parameter specifies the size of a LOGFONT structure.
                /// <br></br> • The pvParam parameter must point to the LOGFONT structure to fill in.
                /// </summary>
                GETICONTITLELOGFONT = 0x1F,

                /// <summary>
                /// <b>Sets the font that is used for icon titles.</b>
                /// <br></br>
                /// <br></br> • The uiParam parameter specifies the size of a LOGFONT structure.
                /// <br></br> • The pvParam parameter must point to a LOGFONT structure.
                /// </summary>
                SETICONTITLELOGFONT = 0x22,

                /// <summary>
                /// <b>Retrieves the metrics associated with icons.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to an ICONMETRICS structure that receives the information.
                /// <br></br> • Set the cbSize member of this structure and the uiParam parameter to sizeof(ICONMETRICS).
                /// </summary>
                GETICONMETRICS = 0x2D,

                /// <summary>
                /// <b>Sets the metrics associated with icons.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to an ICONMETRICS structure that contains the new parameters. 
                /// <br></br> • Set the cbSize member of this structure and the uiParam parameter to sizeof(ICONMETRICS).
                /// </summary>
                SETICONMETRICS = 0x2E,

                /// <summary>
                /// <b>Reloads the system icons.</b>
                /// <br></br>
                /// <br></br> • Set the uiParam parameter to zero.
                /// <br></br> • Set the pvParam parameter to null.
                /// </summary>
                SETICONS = 0x58

            }

            public enum Desktop : int
            {
                /// <summary>
                /// <b>Sets the desktop wallpaper.</b>
                /// <br></br>
                /// <br></br> • The value of the pvParam parameter determines the new wallpaper. 
                /// <br></br> • To specify a wallpaper bitmap, set pvParam to point to a null-terminated string containing the name of a bitmap file. Setting pvParam to "" removes the wallpaper.
                /// <br></br> • Setting pvParam to SETWALLPAPER_DEFAULT or null reverts to the default wallpaper.
                /// </summary>
                SETDESKWALLPAPER = 0x14,

                /// <summary>
                /// <b>Sets the current desktop pattern by causing Windows to read the Pattern= setting from the WIN.INI file.</b>
                /// </summary>
                SETDESKPATTERN = 0x15
            }

            public enum Metrics : int
            {
                /// <summary>
                /// <b>Retrieves the metrics associated with the nonclient area of nonminimized windows.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a NONCLIENTMETRICS structure that receives the information. Set the cbSize member of this structure.
                /// <br></br> • The uiParam parameter to sizeof(NONCLIENTMETRICS).
                /// </summary>
                GETNONCLIENTMETRICS = 0x29,

                /// <summary>
                /// <b>Sets the metrics associated with the nonclient area of nonminimized windows.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a NONCLIENTMETRICS structure that contains the new parameters. 
                /// <br></br> • Set the cbSize member of this structure and the uiParam parameter to sizeof(NONCLIENTMETRICS). Also, the lfHeight member of the LOGFONT structure must be a negative value.
                /// </summary>
                SETNONCLIENTMETRICS = 0x2A
            }

            public enum Cursors : int
            {
                /// <summary>
                /// <b>Reloads the system cursors.</b>
                /// <br></br>
                /// <br></br> • Set the uiParam parameter to zero.
                /// <br></br> • Set the pvParam parameter to null.
                /// </summary>
                SETCURSORS = 0x57,

                /// <summary>
                /// <b>Determines whether the cursor has a shadow around it.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives True if the shadow is enabled, False if it is disabled.
                /// <br></br> • This effect appears only if the system has a color depth of more than 256 colors.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value Is not supported.</i>
                /// </summary>
                GETCURSORSHADOW = 0x101A,

                /// <summary>
                /// <b>Enables or disables a shadow around the cursor.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter is a BOOL variable. Set pvParam to 1 to enable the shadow or 0 to disable the shadow.
                /// <br></br> • This effect appears only if the system has a color depth of more than 256 colors.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                SETCURSORSHADOW = 0x101B,

                /// <summary>
                /// <b>Determines whether the Mouse Trails feature is enabled. This feature improves the visibility of mouse cursor movements by briefly showing a trail of cursors and quickly erasing them.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to an integer variable that receives a value.
                /// <br></br> • If the value is zero or 1, the feature is disabled. If the value Is greater than 1, the feature Is enabled And the value indicates the number of cursors drawn in the trail.
                /// <br></br> • The uiParam parameter is not used.
                /// <br></br>
                /// <br></br> <i>(!) Windows 2000/NT:  This value is not supported.</i>
                /// </summary>
                GETMOUSETRAILS = 0x5E,

                /// <summary>
                /// <b>Enables or disables the Mouse Trails feature, which improves the visibility of mouse cursor movements by briefly showing a trail of cursors and quickly erasing them.</b>
                /// <br></br>
                /// <br></br> • To disable the feature, set the uiParam parameter to zero or 1.
                /// <br></br> • To enable the feature, set uiParam to a value greater than 1 to indicate the number of cursors drawn in the trail.
                /// <br></br>
                /// <br></br> <i>(!) Windows 2000/NT:  This value is not supported.</i>
                /// </summary>
                SETMOUSETRAILS = 0x5D,

                /// <summary>
                /// <b>Retrieves the state of the Mouse Sonar feature.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if enabled or FALSE otherwise.
                /// <br></br> • For more information, see About Mouse Input on MSDN.
                /// <br></br>
                /// <br></br> <i>(!) Windows 2000/NT, Windows 98/95:  This value is not supported.</i>
                /// </summary>
                GETMOUSESONAR = 0x101C,

                /// <summary>
                /// <b>Turns the Sonar accessibility feature on or off. This feature briefly shows several concentric circles around the mouse pointer when the user presses And releases the CTRL key.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter specifies TRUE for on and FALSE for off. The default is off.
                /// <br></br> • For more information, see About Mouse Input on MSDN.
                /// <br></br>
                /// <br></br> <i>(!) Windows 2000/NT, Windows 98/95:  This value is not supported.</i>
                /// </summary>
                SETMOUSESONAR = 0x101D,

                /// <summary>
                /// <b>Determines whether the snap-to-default-button feature is enabled. If enabled, the mouse cursor automatically moves to the default button, such as OK Or Apply, of a dialog box.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if the feature Is on, Or FALSE if it Is off.
                /// <br></br>
                /// <br></br> <i>(!) Windows 95:  Not supported.</i>
                /// </summary>
                GETSNAPTODEFBUTTON = 0x5F,

                /// <summary>
                /// <b>Enables or disables the snap-to-default-button feature. If enabled, the mouse cursor automatically moves to the default button, such as OK Or Apply, of a dialog box.</b>
                /// <br></br>
                /// <br></br> • Set the uiParam parameter to TRUE to enable the feature, or FALSE to disable it.
                /// <br></br> • Applications should use the ShowWindow function when displaying a dialog box so the dialog manager can position the mouse cursor.
                /// <br></br>
                /// <br></br> <i>(!) Windows 95:  Not supported.</i>
                /// </summary>
                SETSNAPTODEFBUTTON = 0x60
            }

            public enum Titlebars : int
            {
                /// <summary>
                /// <b>Determines whether the gradient effect for window title bars is enabled.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE for enabled, or FALSE for disabled.
                /// <br></br> • For more information about the gradient effect, see the GetSysColor function.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                GETGRADIENTCAPTIONS = 0x1008,

                /// <summary>
                /// <b>Enables or disables the gradient effect for window title bars.</b>
                /// <br></br>
                /// <br></br> • Set the pvParam parameter to 1 to enable it, or 0 to disable it.
                /// <br></br> • The gradient effect is possible only if the system has a color depth of more than 256 colors.
                /// <br></br> • For more information about the gradient effect, see the GetSysColor function.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                SETGRADIENTCAPTIONS = 0x1009
            }

            public enum Effects : int
            {
                /// <summary>
                /// <b>Retrieves the animation effects associated with user actions.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to an ANIMATIONINFO structure that receives the information. 
                /// <br></br> • Set the cbSize member of this structure.
                /// <br></br> • Set the uiParam parameter to sizeof(ANIMATIONINFO).
                /// </summary>
                GETANIMATION = 0x48,

                /// <summary>
                /// <b>Sets the animation effects associated with user actions.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to an ANIMATIONINFO structure that contains the new parameters.
                /// <br></br> • Set the cbSize member of this structure and the uiParam parameter to sizeof(ANIMATIONINFO).
                /// </summary>
                SETANIMATION = 0x49,

                /// <summary>
                /// <b>Retrieves the animation of controls and elements inside window.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if animation is enabled and FALSE if it is disabled.
                /// <br></br>
                /// </summary>
                GETCLIENTAREAANIMATION = 0x1042,

                /// <summary>
                /// <b>Sets the animation of controls and elements inside window.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if animation is enabled and FALSE if it is disabled.
                /// <br></br>
                /// </summary>
                SETCLIENTAREAANIMATION = 0x1043,

                /// <summary>
                /// <b>Determines whether the menu animation feature is enabled. This master switch must be on to enable menu animation effects.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if animation is enabled and FALSE if it is disabled.
                /// <br></br> • If animation is enabled, GETMENUFADE indicates whether menus use fade or slide animation.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                GETMENUANIMATION = 0x1002,

                /// <summary>
                /// <b>Enables or disables menu animation. This master switch must be on for any menu animation to occur.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter is a BOOL variable; set pvParam to 1 to enable animation and 0 to disable animation.
                /// <br></br> • If animation is enabled, GETMENUFADE indicates whether menus use fade or slide animation.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                SETMENUANIMATION = 0x1003,

                /// <summary>
                /// <b>Determines whether the slide-open effect for combo boxes is enabled.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE for enabled, or FALSE for disabled.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                GETCOMBOBOXANIMATION = 0x1004,

                /// <summary>
                /// <b>Enables or disables the slide-open effect for combo boxes.</b>
                /// <br></br>
                /// <br></br> • Set the pvParam parameter to 1 to enable the gradient effect, or 0 to disable it.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                SETCOMBOBOXANIMATION = 0x1005,

                /// <summary>
                /// <b>Determines whether the smooth-scrolling effect for list boxes is enabled.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE for enabled, or FALSE for disabled.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                GETLISTBOXSMOOTHSCROLLING = 0x1006,

                /// <summary>
                /// <b>Enables or disables the smooth-scrolling effect for list boxes.</b>
                /// <br></br>
                /// <br></br> • Set the pvParam parameter to 1 to enable the smooth-scrolling effect, or 0 to disable it.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                SETLISTBOXSMOOTHSCROLLING = 0x1007,

                /// <summary>
                /// <b>Determines whether menu fade animation is enabled.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE when fade animation is enabled and FALSE when it is disabled.
                /// <br></br> • If fade animation is disabled, menus use slide animation. This flag is ignored unless menu animation is enabled, which you can do using the SETMENUANIMATION flag.
                /// <br></br> • For more information, see AnimateWindow.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                GETMENUFADE = 0x1012,

                /// <summary>
                /// <b>Enables or disables menu fade animation.</b>
                /// <br></br>
                /// <br></br> • Set pvParam to 1 to enable the menu fade effect or 0 to disable it (i.e. use slide animation).
                /// <br></br> • If fade animation is disabled, menus use slide animation. he The menu fade effect is possible only if the system has a color depth of more than 256 colors.
                /// <br></br> • This flag is ignored unless SPI_MENUANIMATION is also set.
                /// <br></br> • For more information, see AnimateWindow.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                SETMENUFADE = 0x1013,

                /// <summary>
                /// <b>Retrieves the time, in milliseconds, that the system waits before displaying a shortcut menu when the mouse cursor is over a submenu item.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a DWORD variable that receives the time of the delay.
                /// <br></br>
                /// <br></br> <i>(!) Windows 95:  Not supported.</i>
                /// </summary>
                GETMENUSHOWDELAY = 0x6A,

                /// <summary>
                /// <b>Sets the time, in milliseconds, that the system waits before displaying a shortcut menu when the mouse cursor is over a submenu item.</b>
                /// <br></br>
                /// <br></br> • The uiParam parameter must point to a DWORD variable that sets the time of the delay.
                /// <br></br>
                /// <br></br> <i>(!) Windows 95:  Not supported.</i>
                /// </summary>
                SETMENUSHOWDELAY = 0x6B,

                /// <summary>
                /// <b>Determines whether the selection fade effect is enabled.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if enabled or FALSE if disabled.
                /// <br></br> • The selection fade effect causes the menu item selected by the user to remain on the screen briefly while fading out after the menu is dismissed.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                GETSELECTIONFADE = 0x1014,

                /// <summary>
                /// <b>The selection fade effect causes the menu item selected by the user to remain on the screen briefly while fading out after the menu is dismissed. The selection fade effect is possible only if the system has a color depth of more than 256 colors.</b>
                /// <br></br>
                /// <br></br> • Set pvParam to 1 to enable the selection fade effect or 0 to disable it.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                SETSELECTIONFADE = 0x1015,

                /// <summary>
                /// <b>Determines whether ToolTip animation is enabled.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if enabled or FALSE if disabled.
                /// <br></br> • If ToolTip animation is enabled, GETTOOLTIPFADE indicates whether ToolTips use fade or slide animation.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                GETTOOLTIPANIMATION = 0x1016,

                /// <summary>
                /// <b>Sets ToolTip animation if enabled or not.</b>
                /// <br></br>
                /// <br></br> • Set pvParam to 1 to enable ToolTip animation or 0 to disable it. If enabled, you can use SETTOOLTIPFADE to specify fade or slide animation.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                SETTOOLTIPANIMATION = 0x1017,

                /// <summary>
                /// <b>If SETTOOLTIPANIMATION is enabled, GETTOOLTIPFADE indicates whether ToolTip animation uses a fade effect or a slide effect.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE for fade animation or FALSE for slide animation.
                /// <br></br> • For more information on slide and fade effects, see AnimateWindow.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                GETTOOLTIPFADE = 0x1018,

                /// <summary>
                /// <b>If the SETTOOLTIPANIMATION flag is enabled, use SETTOOLTIPFADE to indicate whether ToolTip animation uses a fade effect or a slide effect.</b>
                /// <br></br>
                /// <br></br> • Set pvParam to TRUE for fade animation or FALSE for slide animation. The tooltip fade effect is possible only if the system has a color depth of more than 256 colors.
                /// <br></br> • For more information on the slide and fade effects, see the AnimateWindow function.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                SETTOOLTIPFADE = 0x1019,


                /// <summary>
                /// <b>Determines whether native User menus have flat menu appearance.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that returns TRUE if the flat menu appearance is set, or FALSE otherwise.
                /// <br></br>
                /// <br></br> <i>(!) Windows 2000/NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                GETFLATMENU = 0x1022,

                /// <summary>
                /// <b>Enables or disables flat menu appearance for native User menus.</b>
                /// <br></br>
                /// <br></br> • Set pvParam to TRUE to enable flat menu appearance or FALSE to disable it.
                /// <br></br> • When enabled, the menu bar uses COLOR_MENUBAR for the menubar background, COLOR_MENU for the menu-popup background, COLOR_MENUHILIGHT for the fill of the current menu selection, and COLOR_HILIGHT for the outline of the current menu selection.
                /// <br></br> • If disabled, menus are drawn using the same metrics and colors as in Windows 2000 and earlier.
                /// <br></br>
                /// <br></br> <i>(!) Windows 2000/NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                SETFLATMENU = 0x1023,

                /// <summary>
                /// <b>Determines whether the drop shadow effect is enabled.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that returns TRUE if enabled or FALSE if disabled.
                /// <br></br>
                /// <br></br> <i>(!) Windows 2000/NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                GETDROPSHADOW = 0x1024,

                /// <summary>
                /// <b>Enables or disables the drop shadow effect.</b>
                /// <br></br>
                /// <br></br> • Set pvParam to 1 to enable the drop shadow effect or 0 to disable it.
                /// <br></br> • You must also have CS_DROPSHADOW in the window class style.
                /// <br></br>
                /// <br></br> <i>(!) Windows 2000/NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                SETDROPSHADOW = 0x1025,

                /// <summary>
                /// <b>Determines whether UI effects are enabled or disabled.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if all UI effects are enabled, or FALSE if they are disabled.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                GETUIEFFECTS = 0x103E,

                /// <summary>
                /// <b>Enables or disables UI effects.</b>
                /// <br></br>
                /// <br></br> • Set the pvParam parameter to 1 to enable all UI effects or 0 to disable all UI effects.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                SETUIEFFECTS = 0x103F,

                /// <summary>
                /// <b>Sets dragging of full windows either on or off.</b>
                /// <br></br>
                /// <br></br> • The uiParam parameter specifies TRUE for on, or FALSE for off.
                /// <br></br>
                /// <br></br> <i>(!) Windows 95:  This flag is supported only if Windows Plus! is installed. See SPI_GETWINDOWSEXTENSION.</i>
                /// </summary>
                SETDRAGFULLWINDOWS = 0x25,

                /// <summary>
                /// <b>Determines whether dragging of full windows is enabled.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if enabled, Or FALSE otherwise.
                /// <br></br>
                /// <br></br> <i>(!) Windows 95:  This flag is supported only if Windows Plus! is installed. See SPI_GETWINDOWSEXTENSION.</i>
                /// </summary>
                GETDRAGFULLWINDOWS = 0x26,

                /// <summary>
                /// <b>Determines whether menu access keys are always underlined.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if menu access keys are always underlined, And FALSE if they are underlined only when the menu Is activated by the keyboard.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                GETMENUUNDERLINES = 0x100A,

                /// <summary>
                /// <b>Determines whether menu access keys are always underlined.</b>
                /// <br></br>
                /// <br></br> • Set pvParam to TRUE to always underline menu access keys, Or FALSE to underline menu access keys only when the menu Is activated from the keyboard.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                SETMENUUNDERLINES = 0x100B,

                /// <summary>
                /// <b>Determines whether active window tracking (activating the window the mouse is on) is on or off.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE for on, Or FALSE for off.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                GETACTIVEWINDOWTRACKING = 0x1000,

                /// <summary>
                /// <b>Sets active window tracking (activating the window the mouse is on) either on or off.</b>
                /// <br></br>
                /// <br></br> • Set pvParam to TRUE for on or FALSE for off.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                SETACTIVEWINDOWTRACKING = 0x1001,

                /// <summary>
                /// <b>Retrieves the active window tracking delay, in milliseconds.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a DWORD variable that receives the time.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                GETACTIVEWNDTRKTIMEOUT = 0x2002,

                /// <summary>
                /// <b>Sets the active window tracking delay.</b>
                /// <br></br>
                /// <br></br> • Set pvParam to the number of milliseconds to delay before activating the window under the mouse pointer.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95: This value is not supported.</i>
                /// </summary>
                SETACTIVEWNDTRKTIMEOUT = 0x2003,

                /// <summary>
                /// <b>Determines whether or not windows activated through active window tracking should be brought to the top.</b>
                /// <br></br>
                /// <br></br> • Set pvParam to TRUE for on Or FALSE for off.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                GETACTIVEWNDTRKZORDER = 0x100C,

                /// <summary>
                /// <b>Sets if windows activated through active window tracking should be brought to the top.</b>
                /// <br></br>
                /// <br></br> • Set pvParam to TRUE for on Or FALSE for off.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                SETACTIVEWNDTRKZORDER = 0x100D,

                /// <summary>
                /// <b>Retrieves the caret width in edit controls, in pixels.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a DWORD that receives this value.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                GETCARETWIDTH = 0x2006,

                /// <summary>
                /// <b>Sets the caret width in edit controls.</b>
                /// <br></br>
                /// <br></br> • Set pvParam to the desired width, in pixels.
                /// <br></br> • The default and minimum value is 1.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                SETCARETWIDTH = 0x2007
            }

            public enum FocusRect : int
            {
                /// <summary>
                /// <b>Retrieves the width, in pixels, of the left and right edges of the focus rectangle drawn with DrawFocusRect.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a UINT.
                /// <br></br>
                /// <br></br> <i>(!) Windows 2000/NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                GETFOCUSBORDERWIDTH = 0x200E,

                /// <summary>
                /// <b>Sets the height of the left and right edges of the focus rectangle drawn with DrawFocusRect</b>
                /// <br></br>
                /// <br></br> • Set the pvParam parameter.
                /// <br></br>
                /// <br></br> <i>(!) Windows 2000/NT, Windows Me/98/95:  This value is not supported.</i>
                /// </summary>
                SETFOCUSBORDERWIDTH = 0x200F,

                /// <summary>
                /// <b>Retrieves the height, in pixels, of the top and bottom edges of the focus rectangle drawn with DrawFocusRect.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a UINT.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                GETFOCUSBORDERHEIGHT = 0x2010,

                /// <summary>
                /// <b>Sets the height of the top and bottom edges of the focus rectangle drawn with DrawFocusRect to the value of the pvParam parameter.</b>
                /// <br></br>
                /// <br></br> • Set the pvParam parameter.
                /// <br></br>
                /// <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                /// </summary>
                SETFOCUSBORDERHEIGHT = 0x2011
            }

            public enum Fonts : int
            {
                /// <summary>
                /// <b>Determines whether the font smoothing feature is enabled. This feature uses font antialiasing to make font curves appear smoother by painting pixels at different gray levels.</b>
                /// <br></br>
                /// <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if the feature is enabled, or FALSE if it is not.
                /// <br></br>
                /// <br></br> <i>(!) Windows 95:  This flag is supported only if Windows Plus! is installed. See SPI_GETWINDOWSEXTENSION.</i>
                /// </summary>
                GETFONTSMOOTHING = 0x4A,

                /// <summary>
                /// <b>Enables or disables the font smoothing feature, which uses font antialiasing to make font curves appear smoother by painting pixels at different gray levels.</b>
                /// <br></br>
                /// <br></br> • To enable the feature, set the uiParam parameter to TRUE. To disable the feature, set uiParam to FALSE.
                /// <br></br>
                /// <br></br> <i>(!) Windows 95: This flag is supported only if Windows Plus! is installed. See SPI_GETWINDOWSEXTENSION.</i>
                /// </summary>
                SETFONTSMOOTHING = 0x4B
            }
        }

        [Flags]
        public enum SPIF : int
        {
            None = 0x0,

            /// <summary>
            /// Writes the new system-wide parameter setting to the user profile.
            /// </summary>
            UpdateINIFile = 0x1,

            /// <summary>
            /// Broadcasts the WM_SETTINGCHANGE message after updating the user profile, but it is temporary until you logoff.
            /// </summary>
            SendChange = 0x2,

            /// <summary>
            /// Same as SENDCHANGE
            /// <br></br> Broadcasts the WM_SETTINGCHANGE message after updating the user profile, but it is temporary until you logoff.
            /// </summary>
            SendWinINIChange = SendChange
        }

        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "LoadCursorFromFileA")]
        public static extern IntPtr LoadCursorFromFile(string lpFileName);

        [StructLayout(LayoutKind.Sequential)]
        internal struct AccentPolicy
        {
            public AccentState AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }
        internal struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }
        public enum WindowCompositionAttribute
        {
            WCA_ACCENT_POLICY = 19,
            WCA_USEDARKMODECOLORS = 26
        }
        internal enum AccentState
        {
            ACCENT_DISABLED = 0,
            ACCENT_ENABLE_GRADIENT = 1,
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_ENABLE_TRANSPARANT = 6,
            ACCENT_ENABLE_ACRYLICBLURBEHIND = 4
        }

        [Flags]
        public enum AnimateWindowFlags
        {
            AW_HOR_POSITIVE = 0x0,
            AW_HOR_NEGATIVE = 0x2,
            AW_VER_POSITIVE = 0x4,
            AW_VER_NEGATIVE = 0x8,
            AW_CENTER = 0x10,
            AW_HIDE = 0x10000,
            AW_ACTIVATE = 0x20000,
            AW_SLIDE = 0x40000,
            AW_BLEND = 0x80000
        }
        public enum OCR_SYSTEM_CURSORS : int
        {

            // Standard arrow And small hourglass
            OCR_APPSTARTING = 32650,

            // Standard arrow
            OCR_NORMAL = 32512,

            // Crosshair
            OCR_CROSS = 32515,

            // Windows 2000/XP: Hand
            OCR_HAND = 32649,

            // Arrow And question mark
            OCR_HELP = 32651,

            // I-beam
            OCR_IBEAM = 32513,

            // Slashed circle
            OCR_NO = 32648,

            // Four-pointed arrow pointing north south east And west
            OCR_SIZEALL = 32646,

            // Double-pointed arrow pointing northeast And southwest
            OCR_SIZENESW = 32643,

            // Double-pointed arrow pointing north And south
            OCR_SIZENS = 32645,

            // Double-pointed arrow pointing northwest And southeast
            OCR_SIZENWSE = 32642,

            // Double-pointed arrow pointing west And east
            OCR_SIZEWE = 32644,

            // Vertical arrow
            OCR_UP = 32516,

            // Hourglass
            OCR_WAIT = 32514
        }
        public enum SendMessageTimeoutFlags : uint
        {
            SMTO_NORMAL = 0x0U,
            SMTO_BLOCK = 0x1U,
            SMTO_ABORTIFHUNG = 0x2U,
            SMTO_NOTIMEOUTIFNOTHUNG = 0x8U
        }

        public static int WM_DWMCOLORIZATIONCOLORCHANGED = 0x320;
        public static int WM_DWMCOMPOSITIONCHANGED = 0x31E;
        public static int WM_THEMECHANGED = 0x31A;
        public static int WM_SYSCOLORCHANGE = 0x15;
        public static int WM_PALETTECHANGED = 0x311;
        public static uint WM_WININICHANGE = 0x1AU;
        public static uint WM_SETTINGCHANGE = WM_WININICHANGE;
        public static int HWND_MESSAGE = -0x3;
        public static IntPtr HWND_BROADCAST = new IntPtr(0xFFFF);
        public static int MSG_TIMEOUT = 5000;
        public static UIntPtr RESULT;
    }

    public class Kernel32
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool MoveFileEx(string lpExistingFileName, string lpNewFileName, MoveFileFlags dwFlags);

        [Flags]
        public enum MoveFileFlags
        {
            None = 0,
            ReplaceExisting = 1,
            CopyAllowed = 2,
            DelayUntilReboot = 4,
            WriteThrough = 8,
            CreateHardlink = 16,
            FailIfNotTrackable = 32
        }

    }

    public class UxTheme
    {
        [DllImport("UxTheme.DLL", BestFitMapping = false, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode, EntryPoint = "#65")]
        public static extern int SetSystemVisualStyle(string pszFilename, string pszColor, string pszSize, int dwReserved);

        [DllImport("uxtheme", ExactSpelling = true)]
        public static extern int EnableTheming(int fEnable);

        [DllImport("uxtheme", CharSet = CharSet.Unicode)]
        public static extern int GetCurrentThemeName(StringBuilder stringThemeName, int lengthThemeName, StringBuilder stringColorName, int lengthColorName, StringBuilder stringSizeName, int lengthSizeName);

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int DrawThemeTextEx(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, string text, int iCharCount, int dwFlags, ref Rect pRect, ref DttOpts pOptions);

        /// <summary>
        /// Set The WindowR's Theme Attributes
        /// </summary>
        /// <returns>If The Call Was Successful or Not</returns>
        [DllImport("UxTheme.dll")]
        public static extern int SetWindowThemeAttribute(IntPtr hWnd, WindowThemeAttributeType wtype, ref WTA_OPTIONS attributes, uint size);

        /// <summary>
        /// Do Not Draw The Caption (Text)
        /// </summary>
        public static uint WTNCA_NODRAWCAPTION = 0x1U;
        /// <summary>
        /// Do Not Draw the Icon
        /// </summary>
        public static uint WTNCA_NODRAWICON = 0x2U;
        /// <summary>
        /// Do Not Show the System Menu
        /// </summary>
        public static uint WTNCA_NOSYSMENU = 0x4U;
        /// <summary>
        /// Do Not Mirror the Question mark Symbol
        /// </summary>
        public static uint WTNCA_NOMIRRORHELP = 0x8U;

        /// <summary>
        /// The Options of What Attributes to Add/Remove
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct WTA_OPTIONS
        {
            public uint Flags;
            public uint Mask;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DttOpts
        {
            public int dwSize;
            public DttOptsFlags dwFlags;
            public int crText;
            public int crBorder;
            public int crShadow;
            public int iTextShadowType;
            public Point ptShadowOffset;
            public int iBorderSize;
            public int iFontPropId;
            public int iColorPropId;
            public int iStateId;
            public bool fApplyOverlay;
            public int iGlowSize;
            public int pfnDrawTextCallback;
            public IntPtr lParam;
        }

        [Flags]
        public enum DttOptsFlags : int
        {
            DTT_TEXTCOLOR = 1,
            DTT_BORDERCOLOR = 2,
            DTT_SHADOWCOLOR = 4,
            DTT_SHADOWTYPE = 8,
            DTT_SHADOWOFFSET = 16,
            DTT_BORDERSIZE = 32,
            // DTT_FONTPROP = 64,		commented values are currently unused
            // DTT_COLORPROP = 128,
            // DTT_STATEID = 256,
            DTT_CALCRECT = 512,
            DTT_APPLYOVERLAY = 1024,
            DTT_GLOWSIZE = 2048,
            // DTT_CALLBACK = 4096,
            DTT_COMPOSITED = 8192
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public Rect(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public Rect(Rectangle rect)
            {
                Left = rect.X;
                Top = rect.Y;
                Right = rect.Right;
                Bottom = rect.Bottom;
            }

            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public int Width
            {
                get
                {
                    return Right - Left;
                }
                set
                {
                    Right = Left + value;
                }
            }

            public int Height
            {
                get
                {
                    return Bottom - Top;
                }
                set
                {
                    Bottom = Top + value;
                }
            }

            public Rectangle ToRectangle()
            {
                return new Rectangle(Left, Top, Right - Left, Bottom - Top);
            }

        }

        /// <summary>
        /// What Type of Attributes? (Only One is Currently Defined)
        /// </summary>
        public enum WindowThemeAttributeType
        {
            WTA_NONCLIENT = 1
        }
    }

    public class Shell32
    {
        [DllImport("shell32.dll")]
        public static extern void SHChangeNotify(int wEventId, int uFlags, int dwItem1, int dwItem2);
        [DllImport("Shell32.dll", EntryPoint = "SHDefExtractIconW")]
        public static extern int SHDefExtractIconW([MarshalAs(UnmanagedType.LPTStr)] string pszIconFile, int iIndex, uint uFlags, ref IntPtr phiconLarge, ref IntPtr phiconSmall, uint nIconSize);
        [DllImport("Shell32.dll", SetLastError = false)]
        public static extern int SHGetStockIconInfo(SHSTOCKICONID siid, SHGSI uFlags, ref SHSTOCKICONINFO psii);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct SHSTOCKICONINFO
        {
            public uint cbSize;
            public IntPtr hIcon;
            public int iSysIconIndex;
            public int iIcon;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string szPath;
        }
        public enum SHSTOCKICONID
        {
            /// <summary>
            /// Blank document icon (Document Of a type With no associated application).
            /// </summary>
            DOCNOASSOC = 0,

            /// <summary>
            /// Application-associated document icon (Document Of a type With an associated application).
            /// </summary>
            DOCASSOC = 1,

            /// <summary>
            /// Generic application With no custom icon.
            /// </summary>
            APPLICATION = 2,

            /// <summary>
            /// Folder (generic unspecified state).
            /// </summary>
            FOLDER = 3,

            /// <summary>
            /// Folder (open).
            /// </summary>
            FOLDEROPEN = 4,

            /// <summary>
            /// 5.25-inch disk drive.
            /// </summary>
            DRIVE525 = 5,

            /// <summary>
            /// 3.5-inch disk drive.
            /// </summary>
            DRIVE35 = 6,

            /// <summary>
            /// Removable drive.
            /// </summary>
            DRIVEREMOVE = 7,

            /// <summary>
            /// Fixed drive (hard disk).
            /// </summary>
            DRIVEFIXED = 8,

            /// <summary>
            /// Network drive (connected).
            /// </summary>
            DRIVENET = 9,

            /// <summary>
            /// Network drive (disconnected).
            /// </summary>
            DRIVENETDISABLED = 10,

            /// <summary>
            /// CD drive.
            /// </summary>
            DRIVECD = 11,

            /// <summary>
            /// RAM disk drive.
            /// </summary>
            DRIVERAM = 12,

            /// <summary>
            /// The entire network.
            /// </summary>
            WORLD = 13,

            /// <summary>
            /// A computer On the network.
            /// </summary>
            SERVER = 15,

            /// <summary>
            /// A local printer Or print destination.
            /// </summary>
            PRINTER = 16,

            /// <summary>
            /// The Network virtual folder (FOLDERID_NetworkFolder/CSIDL_NETWORK).
            /// </summary>
            MYNETWORK = 17,

            /// <summary>
            /// The Search feature.
            /// </summary>
            FIND = 22,

            /// <summary>
            /// The Help And Support feature.
            /// </summary>
            HELP = 23,

            /// <summary>
            /// Overlay For a Shared item.
            /// </summary>
            SHARE = 28,

            /// <summary>
            /// Overlay For a shortcut.
            /// </summary>
            LINK = 29,

            /// <summary>
            /// Overlay For items that are expected To be slow To access.
            /// </summary>
            SLOWFILE = 30,

            /// <summary>
            /// The Recycle Bin (empty).
            /// </summary>
            RECYCLER = 31,

            /// <summary>
            /// The Recycle Bin (Not empty).
            /// </summary>
            RECYCLERFULL = 32,

            /// <summary>
            /// Audio CD media.
            /// </summary>
            MEDIACDAUDIO = 40,

            /// <summary>
            /// Security lock.
            /// </summary>
            LOCK = 47,

            /// <summary>
            /// A virtual folder that contains the results Of a search.
            /// </summary>
            AUTOLIST = 49,

            /// <summary>
            /// A network printer.
            /// </summary>
            PRINTERNET = 50,

            /// <summary>
            /// A server Shared On a network.
            /// </summary>
            SERVERSHARE = 51,

            /// <summary>
            /// A local fax printer.
            /// </summary>
            PRINTERFAX = 52,

            /// <summary>
            /// A network fax printer.
            /// </summary>
            PRINTERFAXNET = 53,

            /// <summary>
            /// A file that receives the output Of a Print To file operation.
            /// </summary>
            PRINTERFILE = 54,

            /// <summary>
            /// A category that results from a Stack by command To organize the contents Of a folder.
            /// </summary>
            STACK = 55,

            /// <summary>
            /// Super Video CD (SVCD) media.
            /// </summary>
            MEDIASVCD = 56,

            /// <summary>
            /// A folder that contains only subfolders As child items.
            /// </summary>
            STUFFEDFOLDER = 57,

            /// <summary>
            /// Unknown drive type.
            /// </summary>
            DRIVEUNKNOWN = 58,

            /// <summary>
            /// DVD drive.
            /// </summary>
            DRIVEDVD = 59,

            /// <summary>
            /// DVD media.
            /// </summary>
            MEDIADVD = 60,

            /// <summary>
            /// DVD-RAM media.
            /// </summary>
            MEDIADVDRAM = 61,

            /// <summary>
            /// DVD-RW media.
            /// </summary>
            MEDIADVDRW = 62,

            /// <summary>
            /// DVD-R media.
            /// </summary>
            MEDIADVDR = 63,

            /// <summary>
            /// DVD-ROM media.
            /// </summary>
            MEDIADVDROM = 64,

            /// <summary>
            /// CD+ (enhanced audio CD) media.
            /// </summary>
            MEDIACDAUDIOPLUS = 65,

            /// <summary>
            /// CD-RW media.
            /// </summary>
            MEDIACDRW = 66,

            /// <summary>
            /// CD-R media.
            /// </summary>
            MEDIACDR = 67,

            /// <summary>
            /// A writeable CD In the process Of being burned.
            /// </summary>
            MEDIACDBURN = 68,

            /// <summary>
            /// Blank writable CD media.
            /// </summary>
            MEDIABLANKCD = 69,

            /// <summary>
            /// CD-ROM media.
            /// </summary>
            MEDIACDROM = 70,

            /// <summary>
            /// An audio file.
            /// </summary>
            AUDIOFILES = 71,

            /// <summary>
            /// An image file.
            /// </summary>
            IMAGEFILES = 72,

            /// <summary>
            /// A video file.
            /// </summary>
            VIDEOFILES = 73,

            /// <summary>
            /// A mixed file.
            /// </summary>
            MIXEDFILES = 74,

            /// <summary>
            /// Folder back.
            /// </summary>
            FOLDERBACK = 75,

            /// <summary>
            /// Folder front.
            /// </summary>
            FOLDERFRONT = 76,

            /// <summary>
            /// Security shield. Use For UAC prompts only.
            /// </summary>
            SHIELD = 77,

            /// <summary>
            /// Warning.
            /// </summary>
            WARNING = 78,

            /// <summary>
            /// Informational.
            /// </summary>
            INFO = 79,

            /// <summary>
            /// Error.
            /// </summary>
            Error = 80,

            /// <summary>
            /// Key.
            /// </summary>
            KEY = 81,

            /// <summary>
            /// Software.
            /// </summary>
            SOFTWARE = 82,

            /// <summary>
            /// A UI item such As a button that issues a rename command.
            /// </summary>
            RENAME = 83,

            /// <summary>
            /// A UI item such As a button that issues a delete command.
            /// </summary>
            DELETE = 84,

            /// <summary>
            /// Audio DVD media.
            /// </summary>
            MEDIAAUDIODVD = 85,

            /// <summary>
            /// Movie DVD media.
            /// </summary>
            MEDIAMOVIEDVD = 86,

            /// <summary>
            /// Enhanced CD media.
            /// </summary>
            MEDIAENHANCEDCD = 87,

            /// <summary>
            /// Enhanced DVD media.
            /// </summary>
            MEDIAENHANCEDDVD = 88,

            /// <summary>
            /// High definition DVD media In the HD DVD format.
            /// </summary>
            MEDIAHDDVD = 89,

            /// <summary>
            /// High definition DVD media In the Blu-ray Disc™ format.
            /// </summary>
            MEDIABLURAY = 90,

            /// <summary>
            /// Video CD (VCD) media.
            /// </summary>
            MEDIAVCD = 91,

            /// <summary>
            /// DVD+R media.
            /// </summary>
            MEDIADVDPLUSR = 92,

            /// <summary>
            /// DVD+RW media.
            /// </summary>
            MEDIADVDPLUSRW = 93,

            /// <summary>
            /// A desktop computer.
            /// </summary>
            DESKTOPPC = 94,

            /// <summary>
            /// A mobile computer (laptop).
            /// </summary>
            MOBILEPC = 95,

            /// <summary>
            /// The User Accounts Control PanelR item.
            /// </summary>
            USERS = 96,

            /// <summary>
            /// Smart media.
            /// </summary>
            MEDIASMARTMEDIA = 97,

            /// <summary>
            /// CompactFlash media.
            /// </summary>
            MEDIACOMPACTFLASH = 98,

            /// <summary>
            /// A cell phone.
            /// </summary>
            DEVICECELLPHONE = 99,

            /// <summary>
            /// A digital camera.
            /// </summary>
            DEVICECAMERA = 100,

            /// <summary>
            /// A digital video camera.
            /// </summary>
            DEVICEVIDEOCAMERA = 101,

            /// <summary>
            /// An audio player.
            /// </summary>
            DEVICEAUDIOPLAYER = 102,

            /// <summary>
            /// Connect To network.
            /// </summary>
            NETWORKCONNECT = 103,

            /// <summary>
            /// The Network And Internet Control PanelR item.
            /// </summary>
            INTERNET = 104,

            /// <summary>
            /// A compressed file With a .zip file name extension.
            /// </summary>
            ZIPFILE = 105,

            /// <summary>
            /// The Additional Options Control PanelR item.
            /// </summary>
            SETTINGS = 106,

            /// <summary>
            /// High definition DVD drive (any type - HD DVD-ROM HD DVD-R HD-DVD-RAM) that uses the HD DVD format.
            /// Windows Vista With Service Pack 1 (SP1) And later. 
            /// </summary>
            DRIVEHDDVD = 132,

            /// <summary>
            /// High definition DVD drive (any type - BD-ROM BD-R BD-RE) that uses the Blu-ray Disc format.
            /// Windows Vista With Service Pack 1 (SP1) And later. 
            /// </summary>
            DRIVEBD = 133,

            /// <summary>
            /// High definition DVD-ROM media In the HD DVD-ROM format.
            /// Windows Vista With Service Pack 1 (SP1) And later. 
            /// </summary>
            MEDIAHDDVDROM = 134,

            /// <summary>
            /// High definition DVD-R media In the HD DVD-R format.
            /// Windows Vista With Service Pack 1 (SP1) And later. 
            /// </summary>
            MEDIAHDDVDR = 135,

            /// <summary>
            /// High definition DVD-RAM media In the HD DVD-RAM format.
            /// Windows Vista With Service Pack 1 (SP1) And later. 
            /// </summary>
            MEDIAHDDVDRAM = 136,

            /// <summary>
            /// High definition DVD-ROM media In the Blu-ray Disc BD-ROM format.
            /// Windows Vista With Service Pack 1 (SP1) And later. 
            /// </summary>
            MEDIABDROM = 137,

            /// <summary>
            /// High definition write-once media In the Blu-ray Disc BD-R format.
            /// Windows Vista With Service Pack 1 (SP1) And later. 
            /// </summary>
            MEDIABDR = 138,

            /// <summary>
            /// High definition read/write media In the Blu-ray Disc BD-RE format.
            /// Windows Vista With Service Pack 1 (SP1) And later. 
            /// </summary>
            MEDIABDRE = 139,

            /// <summary>
            /// A cluster disk array.
            /// Windows Vista With Service Pack 1 (SP1) And later. 
            /// </summary>
            CLUSTEREDDRIVE = 140,

            /// <summary>
            /// The highest valid value In the enumeration. Values over 160 are Windows 7-only icons.
            /// </summary>
            MAX_ICONS = 174
        }
        [Flags]
        public enum SHGSI
        {
            ICONLOCATION = 0,
            ICON = 0x100,
            SYSICONINDEX = 0x4000,
            LINKOVERLAY = 0x8000,
            SELECTED = 0x10000,
            LARGEICON = 0x0,
            SMALLICON = 0x1,
            SHELLICONSIZE = 0x4
        }

        public const int MAX_PATH = 260;
        public const int SHCNE_ASSOCCHANGED = 0x8000000;
        public const int SHCNF_IDLIST = 0;
    }

    public class GDI32
    {
        [DllImport("gdi32.dll")]
        public static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, BitBltOp dwRop);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDIBSection(IntPtr hdc, ref BitmapInfo pbmi, uint iUsage, int ppvBits, IntPtr hSection, uint dwOffset);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern bool DeleteObject(IntPtr hObject);

        public enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BitmapInfo
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;
            public byte bmiColors_rgbBlue;
            public byte bmiColors_rgbGreen;
            public byte bmiColors_rgbRed;
            public byte bmiColors_rgbReserved;
        }

        public enum BitBltOp : uint
        {
            SRCCOPY = 0xCC0020U,   // dest = source                   
            SRCPAINT = 0xEE0086U,   // dest = source OR dest           
            SRCAND = 0x8800C6U,   // dest = source AND dest          
            SRCINVERT = 0x660046U,   // dest = source XOR dest          
            SRCERASE = 0x440328U,   // dest = source AND (NOT dest )   
            NOTSRCCOPY = 0x330008U,   // dest = (NOT source)             
            NOTSRCERASE = 0x1100A6U,   // dest = (NOT src) AND (NOT dest) 
            MERGECOPY = 0xC000CAU,   // dest = (source AND pattern)     
            MERGEPAINT = 0xBB0226U,   // dest = (NOT source) OR dest     
            PATCOPY = 0xF00021U,   // dest = pattern                  
            PATPAINT = 0xFB0A09U,   // dest = DPSnoo                   
            PATINVERT = 0x5A0049U,   // dest = pattern XOR dest         
            DSTINVERT = 0x550009U,   // dest = (NOT dest)               
            BLACKNESS = 0x42U,   // dest = BLACK                    
            WHITENESS = 0xFF0062U,   // dest = WHITE                    

            NOMIRRORBITMAP = uint.MinValue + 0x00000000, // Do not Mirror the bitmap in this call 
            CAPTUREBLT = 0x40000000U      // Include layered windows 
        }
    }

    public class Wininet
    {
        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int Description, int ReservedValue);

        public static bool CheckNet()
        {
            int desc;
            return Wininet.InternetGetConnectedState(out desc, 0);
        }
    }

    public class Winmm
    {
        [DllImport("winmm.dll")]
        public static extern int mciSendString(string command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);
    }

    public class Dnsapi
    {
        [DllImport("dnsapi.dll", EntryPoint = "DnsFlushResolverCache")]
        public static extern uint DnsFlushResolverCache();
    }

    /// <summary>
    /// Functions not found internally in system DLLs, but uses the functions in DLLs to do something DLLs Functions cannot do alone.
    /// </summary>
    public class DLLFunc
    {
        #region User32\Shell32
        private static int MAKEICONSIZE(int low, int high)
        {
            return high << 16 | low & 0xFFFF;
        }
        public static object ExtractSmallIcon(string Path, int IconIndex = 0)
        {
            Icon ico = null;
            // Make the nIconSize value (See the Msdn documents). The LOWORD is the Large Icon Size. The HIWORD is the Small Icon Size.
            // The largest size for an icon is 256.
            uint LargeAndSmallSize = (uint)MAKEICONSIZE(256, 16);

            var hLrgIcon = IntPtr.Zero;
            var hSmlIcon = IntPtr.Zero;

            int result = Shell32.SHDefExtractIconW(Path, IconIndex, 0U, ref hLrgIcon, ref hSmlIcon, LargeAndSmallSize);

            if (result == 0)
            {
                if (ico is not null)
                    ico.Dispose();

                // if the large and/or small icons where created in the unmanaged memory successfully then create
                // a clone of them in the managed icons and delete the icons in the unmanaged memory.

                if (hSmlIcon != IntPtr.Zero)
                {
                    ico = (Icon)Icon.FromHandle(hSmlIcon).Clone();
                    User32.DestroyIcon(hSmlIcon);
                }

            }

            return ico;
        }
        public static Icon GetSystemIcon(Shell32.SHSTOCKICONID _Icon, Shell32.SHGSI _Type)
        {
            try
            {
                var sii = new Shell32.SHSTOCKICONINFO() { cbSize = (uint)Marshal.SizeOf(typeof(Shell32.SHSTOCKICONINFO)) };
                Shell32.SHGetStockIconInfo(_Icon, _Type, ref sii);
                if (sii.hIcon != null && sii.hIcon != IntPtr.Zero)
                {
                    return Icon.FromHandle(sii.hIcon);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Dwmapi
        public static void DarkTitlebar(IntPtr hWnd, bool DarkMode)
        {
            int argattrValue = DarkMode ? 1 : 0;
            Dwmapi.DwmSetWindowAttribute(hWnd, 20, ref argattrValue, Marshal.SizeOf<int>());
            int argattrValue1 = DarkMode ? 1 : 0;
            Dwmapi.DwmSetWindowAttribute(hWnd, 19, ref argattrValue1, Marshal.SizeOf<int>());
        }
        #endregion

        #region UxTheme
        public static void RemoveFormTitlebarTextAndIcon(IntPtr Handle)
        {
            var ops = new UxTheme.WTA_OPTIONS()
            {
                Flags = UxTheme.WTNCA_NODRAWCAPTION | UxTheme.WTNCA_NODRAWICON,
                Mask = UxTheme.WTNCA_NODRAWCAPTION | UxTheme.WTNCA_NODRAWICON
            };

            UxTheme.SetWindowThemeAttribute(Handle, UxTheme.WindowThemeAttributeType.WTA_NONCLIENT, ref ops, (uint)Marshal.SizeOf(ops));
        }

        #endregion

        #region Winmm
        public static void PlayAudio(string File)
        {
            if (System.IO.File.Exists(File))
            {
                Winmm.mciSendString("close myWAV", null, 0, (IntPtr)0);
                Winmm.mciSendString("open \"" + File + "\" type mpegvideo alias myWAV", null, 0, (IntPtr)0);
                Winmm.mciSendString("play myWAV", null, 0, (IntPtr)0);
                int Volume = 1000; // Sets it to use entire range of volume control
                Winmm.mciSendString("setaudio myWAV volume to " + Volume.ToString(), null, 0, (IntPtr)0);
            }
        }

        public static void StopAudio()
        {
            Winmm.mciSendString("seek myWAV to start", null, 0, IntPtr.Zero);
            Winmm.mciSendString("stop myWAV", null, 0, IntPtr.Zero);
        }
        #endregion

    }

}