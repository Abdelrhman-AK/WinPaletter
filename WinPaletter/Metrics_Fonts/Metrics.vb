Imports System.Runtime.InteropServices
Imports System.Text

Public Class Metrics

    <DllImport("user32", CharSet:=CharSet.Auto)>
    Public Shared Function SystemParametersInfo(ByVal uAction As Integer, ByVal uParam As Integer, ByRef lpvParam As NONCLIENTMETRICS, ByVal fuWinIni As SPIF) As Integer
    End Function

    <DllImport("user32", CharSet:=CharSet.Auto)>
    Public Shared Function SystemParametersInfo(ByVal uAction As Integer, ByVal uParam As Integer, ByRef lpvParam As LogFontStr, ByVal fuWinIni As SPIF) As Integer
    End Function

    <DllImport("user32", CharSet:=CharSet.Auto)>
    Public Shared Function SystemParametersInfo(ByVal uAction As Integer, ByVal uParam As Integer, ByRef lpvParam As ANIMATIONINFO, ByVal fuWinIni As SPIF) As Integer
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Public Shared Function SystemParametersInfo(ByVal uiAction As SPI, ByVal uiParam As UInteger, ByVal pvParam As IntPtr, ByVal fWinIni As SPIF) As Boolean
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Public Shared Function SystemParametersInfo(ByVal uiAction As UInteger, ByVal uiParam As UInteger, ByVal pvParam As String, ByVal fWinIni As SPIF) As Boolean
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Public Shared Function SystemParametersInfo(ByVal uiAction As UInteger, ByVal uiParam As UInteger, ByVal pvParam As StringBuilder, ByVal fWinIni As SPIF) As Boolean
    End Function

    <Flags>
    Enum SPIF
        None = &H0
        SPIF_UPDATEINIFILE = &H1                 ' Writes the new system-wide parameter setting to the user profile.
        SPIF_SENDCHANGE = &H2                    ' Broadcasts the WM_SETTINGCHANGE message after updating the user profile.
        SPIF_SENDWININICHANGE = SPIF_SENDCHANGE  ' Same as SPIF_SENDCHANGE.
    End Enum

    Public Structure NONCLIENTMETRICS
        Dim cbSize As UInteger
        Dim iBorderWidth As Integer
        Dim iScrollWidth As Integer
        Dim iScrollHeight As Integer
        Dim iCaptionWidth As Integer
        Dim iCaptionHeight As Integer
        Dim lfCaptionFont As LogFont
        Dim iSmCaptionWidth As Integer
        Dim iSmCaptionHeight As Integer
        Dim lfSmCaptionFont As LogFont
        Dim iMenuWidth As Integer
        Dim iMenuHeight As Integer
        Dim lfMenuFont As LogFont
        Dim lfStatusFont As LogFont
        Dim lfMessageFont As LogFont
        Dim iPaddedBorderWidth As Integer
    End Structure


    <StructLayout(LayoutKind.Sequential)>
    Public Structure ANIMATIONINFO
        Public Sub New(ByVal iMinAnimate As Integer)
            Me.cbSize = CUInt(Marshal.SizeOf(GetType(ANIMATIONINFO)))
            Me.iMinAnimate = iMinAnimate
        End Sub

        Public cbSize As UInteger
        Public iMinAnimate As Integer
    End Structure


    ' ## SPI_ System-wide parameter - Used in SystemParametersInfo function
    Public Enum SPI

        ' ## Retrieves the border multiplier factor that determines the width of a window's sizing border.
        ' ## The pvParam parameter must point to an integer variable that receives this value.
        SPI_GETBORDER = &H5

        ' ## Sets the border multiplier factor that determines the width of a window's sizing border.
        ' ## The uiParam parameter specifies the new value.
        SPI_SETBORDER = &H6

        ' ## Sets or retrieves the width, in pixels, of an icon cell. The system uses this rectangle to arrange icons in large icon view.
        ' ## To set this value, set uiParam to the new value and set pvParam to null. You cannot set this value to less than SM_CXICON.
        ' ## To retrieve this value, pvParam must point to an integer that receives the current value.
        SPI_ICONHORIZONTALSPACING = &HD

        ' ## Retrieves the current granularity value of the desktop sizing grid. The pvParam parameter must point to an integer variable
        ' ## that receives the granularity.
        SPI_GETGRIDGRANULARITY = &H12

        ' ## Sets the granularity of the desktop sizing grid to the value of the uiParam parameter.
        SPI_SETGRIDGRANULARITY = &H13

        ' ## Sets the desktop wallpaper. The value of the pvParam parameter determines the new wallpaper. To specify a wallpaper bitmap,
        ' ## set pvParam to point to a null-terminated string containing the name of a bitmap file. Setting pvParam to "" removes the wallpaper.
        ' ## Setting pvParam to SETWALLPAPER_DEFAULT or null reverts to the default wallpaper.
        SPI_SETDESKWALLPAPER = &H14

        ' ## Sets the current desktop pattern by causing Windows to read the Pattern= setting from the WIN.INI file.
        SPI_SETDESKPATTERN = &H15

        ' ## Sets or retrieves the height, in pixels, of an icon cell.
        ' ## To set this value, set uiParam to the new value and set pvParam to null. You cannot set this value to less than SM_CYICON.
        ' ## To retrieve this value, pvParam must point to an integer that receives the current value.
        SPI_ICONVERTICALSPACING = &H18

        ' ## Determines whether icon-title wrapping is enabled. The pvParam parameter must point to a bool variable that receives TRUE
        ' ## if enabled, or FALSE otherwise.
        SPI_GETICONTITLEWRAP = &H19

        ' ## Turns icon-title wrapping on or off. The uiParam parameter specifies TRUE for on, or FALSE for off.
        SPI_SETICONTITLEWRAP = &H1A

        ' ## Determines whether pop-up menus are left-aligned or right-aligned, relative to the corresponding menu-bar item.
        ' ## The pvParam parameter must point to a bool variable that receives TRUE if left-aligned, or FALSE otherwise.
        SPI_GETMENUDROPALIGNMENT = &H1B

        ' ## Sets the alignment value of pop-up menus. The uiParam parameter specifies TRUE for right alignment, or FALSE for left alignment.
        SPI_SETMENUDROPALIGNMENT = &H1C

        ' ## Retrieves the logical font information for the current icon-title font. The uiParam parameter specifies the size of a LOGFONT structure,
        ' ## and the pvParam parameter must point to the LOGFONT structure to fill in.
        SPI_GETICONTITLELOGFONT = &H1F

        ' ## Sets the double-click time for the mouse to the value of the uiParam parameter. The double-click time is the maximum number
        ' ## of milliseconds that can occur between the first and second clicks of a double-click. You can also call the SetDoubleClickTime
        ' ## function to set the double-click time. To get the current double-click time, call the GetDoubleClickTime function.
        SPI_SETDOUBLECLICKTIME = &H20

        ' ## Sets the font that is used for icon titles. The uiParam parameter specifies the size of a LOGFONT structure,
        ' ## and the pvParam parameter must point to a LOGFONT structure.
        SPI_SETICONTITLELOGFONT = &H22

        ' ## Retrieves the metrics associated with the nonclient area of nonminimized windows. The pvParam parameter must point
        ' ## to a NONCLIENTMETRICS structure that receives the information. Set the cbSize member of this structure and the uiParam parameter
        ' ## to sizeof(NONCLIENTMETRICS).
        SPI_GETNONCLIENTMETRICS = &H29

        ' ## Sets the metrics associated with the nonclient area of nonminimized windows. The pvParam parameter must point
        ' ## to a NONCLIENTMETRICS structure that contains the new parameters. Set the cbSize member of this structure
        ' ## and the uiParam parameter to sizeof(NONCLIENTMETRICS). Also, the lfHeight member of the LOGFONT structure must be a negative value.
        SPI_SETNONCLIENTMETRICS = &H2A

        ' ## Retrieves the metrics associated with minimized windows. The pvParam parameter must point to a MINIMIZEDMETRICS structure
        ' ## that receives the information. Set the cbSize member of this structure and the uiParam parameter to sizeof(MINIMIZEDMETRICS).
        SPI_GETMINIMIZEDMETRICS = &H2B

        ' ## Sets the metrics associated with minimized windows. The pvParam parameter must point to a MINIMIZEDMETRICS structure
        ' ## that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to sizeof(MINIMIZEDMETRICS).
        SPI_SETMINIMIZEDMETRICS = &H2C

        ' ## Retrieves the metrics associated with icons. The pvParam parameter must point to an ICONMETRICS structure that receives
        ' ## the information. Set the cbSize member of this structure and the uiParam parameter to sizeof(ICONMETRICS).
        SPI_GETICONMETRICS = &H2D

        ' ## Sets the metrics associated with icons. The pvParam parameter must point to an ICONMETRICS structure that contains
        ' ## the new parameters. Set the cbSize member of this structure and the uiParam parameter to sizeof(ICONMETRICS).
        SPI_SETICONMETRICS = &H2E

        ' ## Sets the size of the work area. The work area is the portion of the screen not obscured by the system taskbar
        ' ## or by application desktop toolbars. The pvParam parameter is a pointer to a RECT structure that specifies the new work area rectangle,
        ' ## expressed in virtual screen coordinates. In a system with multiple display monitors, the function sets the work area
        ' ## of the monitor that contains the specified rectangle.
        SPI_SETWORKAREA = &H2F

        ' ## Retrieves the size of the work area on the primary display monitor. The work area is the portion of the screen not obscured
        ' ## by the system taskbar or by application desktop toolbars. The pvParam parameter must point to a RECT structure that receives
        ' ## the coordinates of the work area, expressed in virtual screen coordinates.
        ' ## To get the work area of a monitor other than the primary display monitor, call the GetMonitorInfo function.
        SPI_GETWORKAREA = &H30

        ' ## Retrieves the animation effects associated with user actions. The pvParam parameter must point to an ANIMATIONINFO structure
        ' ## that receives the information. Set the cbSize member of this structure and the uiParam parameter to sizeof(ANIMATIONINFO).
        SPI_GETANIMATION = &H48

        ' ## Sets the animation effects associated with user actions. The pvParam parameter must point to an ANIMATIONINFO structure
        ' ## that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to sizeof(ANIMATIONINFO).
        SPI_SETANIMATION = &H49

        ' ## Determines whether the font smoothing feature is enabled. This feature uses font antialiasing to make font curves appear smoother
        ' ## by painting pixels at different gray levels.
        ' ## The pvParam parameter must point to a BOOL variable that receives TRUE if the feature is enabled, or FALSE if it is not.
        ' ## Windows 95:  This flag is supported only if Windows Plus! is installed. See SPI_GETWINDOWSEXTENSION.
        SPI_GETFONTSMOOTHING = &H4A

        ' ## Enables or disables the font smoothing feature, which uses font antialiasing to make font curves appear smoother
        ' ## by painting pixels at different gray levels.
        ' ## To enable the feature, set the uiParam parameter to TRUE. To disable the feature, set uiParam to FALSE.
        ' ## Windows 95:  This flag is supported only if Windows Plus! is installed. See SPI_GETWINDOWSEXTENSION.
        SPI_SETFONTSMOOTHING = &H4B

        ' ## Reloads the system cursors. Set the uiParam parameter to zero and the pvParam parameter to null.
        SPI_SETCURSORS = &H57

        ' ## Reloads the system icons. Set the uiParam parameter to zero and the pvParam parameter to null.
        SPI_SETICONS = &H58

        ' ## Retrieves the time, in milliseconds, that the system waits before displaying a shortcut menu when the mouse cursor is
        ' ## over a submenu item. The pvParam parameter must point to a DWORD variable that receives the time of the delay.
        ' ## Windows 95:  Not supported.
        SPI_GETMENUSHOWDELAY = &H6A

        ' ## Sets uiParam to the time, in milliseconds, that the system waits before displaying a shortcut menu when the mouse cursor is
        ' ## over a submenu item.
        ' ## Windows 95:  Not supported.
        SPI_SETMENUSHOWDELAY = &H6B

        ' ## Determines whether the menu animation feature is enabled. This master switch must be on to enable menu animation effects.
        ' ## The pvParam parameter must point to a BOOL variable that receives TRUE if animation is enabled and FALSE if it is disabled.
        ' ## If animation is enabled, SPI_GETMENUFADE indicates whether menus use fade or slide animation.
        ' ## Windows NT, Windows 95:  This value is not supported.
        SPI_GETMENUANIMATION = &H1002

        ' ## Enables or disables menu animation. This master switch must be on for any menu animation to occur.
        ' ## The pvParam parameter is a BOOL variable; set pvParam to TRUE to enable animation and FALSE to disable animation.
        ' ## If animation is enabled, SPI_GETMENUFADE indicates whether menus use fade or slide animation.
        ' ## Windows NT, Windows 95:  This value is not supported.
        SPI_SETMENUANIMATION = &H1003

        ' ## Determines whether the slide-open effect for combo boxes is enabled. The pvParam parameter must point to a BOOL variable
        ' ## that receives TRUE for enabled, or FALSE for disabled.
        ' ## Windows NT, Windows 95:  This value is not supported.
        SPI_GETCOMBOBOXANIMATION = &H1004

        ' ## Enables or disables the slide-open effect for combo boxes. Set the pvParam parameter to TRUE to enable the gradient effect,
        ' ## or FALSE to disable it.
        ' ## Windows NT, Windows 95:  This value is not supported.
        SPI_SETCOMBOBOXANIMATION = &H1005

        ' ## Determines whether the smooth-scrolling effect for list boxes is enabled. The pvParam parameter must point to a BOOL variable
        ' ## that receives TRUE for enabled, or FALSE for disabled.
        ' ## Windows NT, Windows 95:  This value is not supported.
        SPI_GETLISTBOXSMOOTHSCROLLING = &H1006

        ' ## Enables or disables the smooth-scrolling effect for list boxes. Set the pvParam parameter to TRUE to enable the smooth-scrolling effect,
        ' ## or FALSE to disable it.
        ' ## Windows NT, Windows 95:  This value is not supported.
        SPI_SETLISTBOXSMOOTHSCROLLING = &H1007

        ' ## Determines whether the gradient effect for window title bars is enabled. The pvParam parameter must point to a BOOL variable
        ' ## that receives TRUE for enabled, or FALSE for disabled. For more information about the gradient effect, see the GetSysColor function.
        ' ## Windows NT, Windows 95:  This value is not supported.
        SPI_GETGRADIENTCAPTIONS = &H1008

        ' ## Enables or disables the gradient effect for window title bars. Set the pvParam parameter to TRUE to enable it, or FALSE to disable it.
        ' ## The gradient effect is possible only if the system has a color depth of more than 256 colors. For more information about
        ' ## the gradient effect, see the GetSysColor function.
        ' ## Windows NT, Windows 95:  This value is not supported.
        SPI_SETGRADIENTCAPTIONS = &H1009

        ' ## Determines whether hot tracking of user-interface elements, such as menu names on menu bars, is enabled. The pvParam parameter
        ' ## must point to a BOOL variable that receives TRUE for enabled, or FALSE for disabled.
        ' ## Hot tracking means that when the cursor moves over an item, it is highlighted but not selected. You can query this value to decide
        ' ## whether to use hot tracking in the user interface of your application.
        ' ## Windows NT, Windows 95:  This value is not supported.
        SPI_GETHOTTRACKING = &H100E

        ' ## Enables or disables hot tracking of user-interface elements such as menu names on menu bars. Set the pvParam parameter to TRUE
        ' ## to enable it, or FALSE to disable it.
        ' ## Hot-tracking means that when the cursor moves over an item, it is highlighted but not selected.
        ' ## Windows NT, Windows 95:  This value is not supported.
        SPI_SETHOTTRACKING = &H100F

        ' ## Determines whether menu fade animation is enabled. The pvParam parameter must point to a BOOL variable that receives TRUE
        ' ## when fade animation is enabled and FALSE when it is disabled. If fade animation is disabled, menus use slide animation.
        ' ## This flag is ignored unless menu animation is enabled, which you can do using the SPI_SETMENUANIMATION flag.
        ' ## For more information, see AnimateWindow.
        ' ## Windows NT, Windows Me/98/95:  This value is not supported.
        SPI_GETMENUFADE = &H1012

        ' ## Enables or disables menu fade animation. Set pvParam to TRUE to enable the menu fade effect or FALSE to disable it.
        ' ## If fade animation is disabled, menus use slide animation. he The menu fade effect is possible only if the system
        ' ## has a color depth of more than 256 colors. This flag is ignored unless SPI_MENUANIMATION is also set. For more information,
        ' ## see AnimateWindow.
        ' ## Windows NT, Windows Me/98/95:  This value is not supported.
        SPI_SETMENUFADE = &H1013

        ' ## Determines whether the selection fade effect is enabled. The pvParam parameter must point to a BOOL variable that receives TRUE
        ' ## if enabled or FALSE if disabled.
        ' ## The selection fade effect causes the menu item selected by the user to remain on the screen briefly while fading out
        ' ## after the menu is dismissed.
        ' ## Windows NT, Windows Me/98/95:  This value is not supported.
        SPI_GETSELECTIONFADE = &H1014

        ' ## Set pvParam to TRUE to enable the selection fade effect or FALSE to disable it.
        ' ## The selection fade effect causes the menu item selected by the user to remain on the screen briefly while fading out
        ' ## after the menu is dismissed. The selection fade effect is possible only if the system has a color depth of more than 256 colors.
        ' ## Windows NT, Windows Me/98/95:  This value is not supported.
        SPI_SETSELECTIONFADE = &H1015

        ' ## Determines whether ToolTip animation is enabled. The pvParam parameter must point to a BOOL variable that receives TRUE
        ' ## if enabled or FALSE if disabled. If ToolTip animation is enabled, SPI_GETTOOLTIPFADE indicates whether ToolTips use fade or slide animation.
        ' ## Windows NT, Windows Me/98/95:  This value is not supported.
        SPI_GETTOOLTIPANIMATION = &H1016

        ' ## Set pvParam to TRUE to enable ToolTip animation or FALSE to disable it. If enabled, you can use SPI_SETTOOLTIPFADE
        ' ## to specify fade or slide animation.
        ' ## Windows NT, Windows Me/98/95:  This value is not supported.
        SPI_SETTOOLTIPANIMATION = &H1017

        ' ## If SPI_SETTOOLTIPANIMATION is enabled, SPI_GETTOOLTIPFADE indicates whether ToolTip animation uses a fade effect or a slide effect.
        ' ##  The pvParam parameter must point to a BOOL variable that receives TRUE for fade animation or FALSE for slide animation.
        ' ##  For more information on slide and fade effects, see AnimateWindow.
        ' ## Windows NT, Windows Me/98/95:  This value is not supported.
        SPI_GETTOOLTIPFADE = &H1018

        ' ## If the SPI_SETTOOLTIPANIMATION flag is enabled, use SPI_SETTOOLTIPFADE to indicate whether ToolTip animation uses a fade effect
        ' ## or a slide effect. Set pvParam to TRUE for fade animation or FALSE for slide animation. The tooltip fade effect is possible only
        ' ## if the system has a color depth of more than 256 colors. For more information on the slide and fade effects,
        ' ## see the AnimateWindow function.
        ' ## Windows NT, Windows Me/98/95:  This value is not supported.
        SPI_SETTOOLTIPFADE = &H1019

        ' ## Determines whether the cursor has a shadow around it. The pvParam parameter must point to a BOOL variable that receives TRUE
        ' ## if the shadow is enabled, FALSE if it is disabled. This effect appears only if the system has a color depth of more than 256 colors.
        ' ## Windows NT, Windows Me/98/95:  This value is not supported.
        SPI_GETCURSORSHADOW = &H101A

        ' ## Enables or disables a shadow around the cursor. The pvParam parameter is a BOOL variable. Set pvParam to TRUE to enable the shadow
        ' ## or FALSE to disable the shadow. This effect appears only if the system has a color depth of more than 256 colors.
        ' ## Windows NT, Windows Me/98/95:  This value is not supported.
        SPI_SETCURSORSHADOW = &H101B

        ' ## Determines whether native User menus have flat menu appearance. The pvParam parameter must point to a BOOL variable
        ' ## that returns TRUE if the flat menu appearance is set, or FALSE otherwise.
        ' ## Windows 2000/NT, Windows Me/98/95:  This value is not supported.
        SPI_GETFLATMENU = &H1022

        ' ## Enables or disables flat menu appearance for native User menus. Set pvParam to TRUE to enable flat menu appearance
        ' ## or FALSE to disable it.
        ' ## When enabled, the menu bar uses COLOR_MENUBAR for the menubar background, COLOR_MENU for the menu-popup background, COLOR_MENUHILIGHT
        ' ## for the fill of the current menu selection, and COLOR_HILIGHT for the outline of the current menu selection.
        ' ## If disabled, menus are drawn using the same metrics and colors as in Windows 2000 and earlier.
        ' ## Windows 2000/NT, Windows Me/98/95:  This value is not supported.
        SPI_SETFLATMENU = &H1023

        ' ## Determines whether the drop shadow effect is enabled. The pvParam parameter must point to a BOOL variable that returns TRUE
        ' ## if enabled or FALSE if disabled.
        ' ## Windows 2000/NT, Windows Me/98/95:  This value is not supported.
        SPI_GETDROPSHADOW = &H1024


        ' ## Enables or disables the drop shadow effect. Set pvParam to TRUE to enable the drop shadow effect or FALSE to disable it.
        ' ## You must also have CS_DROPSHADOW in the window class style.
        ' ## Windows 2000/NT, Windows Me/98/95:  This value is not supported.
        SPI_SETDROPSHADOW = &H1025


        ' ## Determines whether UI effects are enabled or disabled. The pvParam parameter must point to a BOOL variable that receives TRUE
        ' ## if all UI effects are enabled, or FALSE if they are disabled.
        ' ## Windows NT, Windows Me/98/95:  This value is not supported.
        SPI_GETUIEFFECTS = &H103E

        ' ## Enables or disables UI effects. Set the pvParam parameter to TRUE to enable all UI effects or FALSE to disable all UI effects.
        ' ## Windows NT, Windows Me/98/95:  This value is not supported.
        SPI_SETUIEFFECTS = &H103F

        ' ## Retrieves the number of times SetForegroundWindow will flash the taskbar button when rejecting a foreground switch request.
        ' ## The pvParam parameter must point to a DWORD variable that receives the value.
        ' ## Windows NT, Windows 95:  This value is not supported.
        SPI_GETFOREGROUNDFLASHCOUNT = &H2004

        ' ## Sets the number of times SetForegroundWindow will flash the taskbar button when rejecting a foreground switch request.
        ' ## Set pvParam to the number of times to flash.
        ' ## Windows NT, Windows 95:  This value is not supported.
        SPI_SETFOREGROUNDFLASHCOUNT = &H2005

        ' ## Retrieves the caret width in edit controls, in pixels. The pvParam parameter must point to a DWORD that receives this value.
        ' ## Windows NT, Windows Me/98/95:  This value is not supported.
        SPI_GETCARETWIDTH = &H2006

        ' ## Sets the caret width in edit controls. Set pvParam to the desired width, in pixels. The default and minimum value is 1.
        ' ## Windows NT, Windows Me/98/95:  This value is not supported.
        SPI_SETCARETWIDTH = &H2007


        ' ## Sets the font smoothing type. The pvParam parameter points to a int that contains either FE_FONTSMOOTHINGSTANDARD,
        ' ## if standard anti-aliasing is used, or FE_FONTSMOOTHINGCLEARTYPE, if ClearType is used. The default is FE_FONTSMOOTHINGSTANDARD.
        ' ## When using this option, the fWinIni parameter must be set to SPIF_SENDWININICHANGE | SPIF_UPDATEINIFILE; otherwise,
        ' ## SystemParametersInfo fails.
        SPI_SETFONTSMOOTHINGTYPE = &H200B

        ' ## Retrieves a contrast value that is used in ClearType™ smoothing. The pvParam parameter must point to a int
        ' ## that receives the information.
        ' ## Windows 2000/NT, Windows Me/98/95:  This value is not supported.
        SPI_GETFONTSMOOTHINGCONTRAST = &H200C

        ' ## Sets the contrast value used in ClearType smoothing. The pvParam parameter points to a int that holds the contrast value.
        ' ## Valid contrast values are from 1000 to 2200. The default value is 1400.
        ' ## When using this option, the fWinIni parameter must be set to SPIF_SENDWININICHANGE | SPIF_UPDATEINIFILE; otherwise,
        ' ## SystemParametersInfo fails.
        ' ## SPI_SETFONTSMOOTHINGTYPE must also be set to FE_FONTSMOOTHINGCLEARTYPE.
        ' ## Windows 2000/NT, Windows Me/98/95:  This value is not supported.
        SPI_SETFONTSMOOTHINGCONTRAST = &H200D

        ' ## Retrieves the width, in pixels, of the left and right edges of the focus rectangle drawn with DrawFocusRect.
        ' ## The pvParam parameter must point to a int.
        ' ## Windows 2000/NT, Windows Me/98/95:  This value is not supported.
        SPI_GETFOCUSBORDERWIDTH = &H200E


        ' ## Sets the height of the left and right edges of the focus rectangle drawn with DrawFocusRect to the value of the pvParam parameter.
        ' ## Windows 2000/NT, Windows Me/98/95:  This value is not supported.
        SPI_SETFOCUSBORDERWIDTH = &H200F


        ' ## Retrieves the height, in pixels, of the top and bottom edges of the focus rectangle drawn with DrawFocusRect.
        ' ## The pvParam parameter must point to a int.
        ' ## Windows 2000/NT, Windows Me/98/95:  This value is not supported.
        SPI_GETFOCUSBORDERHEIGHT = &H2010


        ' ## Sets the height of the top and bottom edges of the focus rectangle drawn with DrawFocusRect to the value of the pvParam parameter.
        ' ## Windows 2000/NT, Windows Me/98/95:  This value is not supported.
        SPI_SETFOCUSBORDERHEIGHT = &H2011


        ' ## Not implemented.
        SPI_GETFONTSMOOTHINGORIENTATION = &H2012


        ' ## Not implemented.
        SPI_SETFONTSMOOTHINGORIENTATION = &H2013

    End Enum

End Class
