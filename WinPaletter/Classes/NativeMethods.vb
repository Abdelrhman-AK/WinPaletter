Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports WinPaletter.Metrics

Namespace NativeMethods
    Public Class Dwmapi

        <DllImport("dwmapi.dll", EntryPoint:="#131", PreserveSig:=False)>
        Public Shared Sub DwmSetColorizationParameters(ByRef parameters As DWM_COLORIZATION_PARAMS, ByVal unknown As Boolean)
        End Sub

        <DllImport("dwmapi.dll")>
        Public Shared Function DwmIsCompositionEnabled(ByRef enabled As Boolean) As Integer
        End Function

        <DllImport("dwmapi")> Public Shared Function DwmExtendFrameIntoClientArea(ByVal hWnd As IntPtr, ByRef pMarInset As MARGINS) As Integer
        End Function

        <DllImport("dwmapi")> Friend Shared Function DwmSetWindowAttribute(ByVal hwnd As IntPtr, ByVal attr As Integer, ByRef attrValue As Integer, ByVal attrSize As Integer) As Integer
        End Function

        <DllImport("dwmapi.dll")> Friend Shared Function DwmSetWindowAttribute(ByVal hwnd As IntPtr, ByVal dwAttribute As DWMATTRIB, ByRef pvAttribute As Integer, ByVal cbAttribute As Integer) As Integer
        End Function

        Public Enum DWMATTRIB As Integer
            SYSTEMBACKDROP_TYPE = 38
            MICA_EFFECT = 1029
            USE_IMMERSIVE_DARK_MODE = 20
            WINDOW_CORNER_PREFERENCE = 33
            TEXT_COLOR = 34
            CAPTION_COLOR = 35
            BORDER_COLOR = 36
        End Enum
        Public Enum FormCornersType
            [Default]
            Rectangular
            Round
            SmallRound
        End Enum
        <StructLayout(LayoutKind.Sequential)> Public Structure MARGINS
            Public leftWidth As Integer
            Public rightWidth As Integer
            Public topHeight As Integer
            Public bottomHeight As Integer
        End Structure
        Public Structure DWM_COLORIZATION_PARAMS
            Public clrColor As Integer
            Public clrAfterGlow As Integer
            Public nIntensity As Integer
            Public clrAfterGlowBalance As Integer
            Public clrBlurBalance As Integer
            Public clrGlassReflectionIntensity As Integer
            Public fOpaque As Boolean
        End Structure

        Public Const CS_DROPSHADOW As Integer = &H20000
        Public Const WM_NCPAINT As Integer = &H85

    End Class

    Public Class User32
        <DllImport("user32.dll")>
        Public Shared Function LoadCursor(ByVal hInstance As Integer, ByVal lpCursorName As Integer) As Integer
        End Function
        <DllImport("user32.dll")>
        Public Shared Function SetCursor(ByVal hCursor As Integer) As Integer
        End Function
        <DllImport("user32.dll")>
        Public Shared Function AnimateWindow(ByVal hWnd As IntPtr, ByVal time As Integer, ByVal flags As AnimateWindowFlags) As Boolean
        End Function
        <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
        Public Shared Function SendMessageTimeout(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As UIntPtr, ByVal lParam As IntPtr, ByVal fuFlags As SendMessageTimeoutFlags, ByVal uTimeout As UInteger, <Out> ByRef lpdwResult As UIntPtr) As IntPtr
        End Function
        <DllImport("user32.dll")>
        Public Shared Function SetSystemCursor(ByVal hcur As IntPtr, ByVal id As Integer) As Boolean
        End Function
        <DllImport("user32.dll", EntryPoint:="DestroyIcon")>
        Public Shared Function DestroyIcon(ByVal hIcon As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function
        <DllImport("user32.dll")>
        Public Shared Function SetSysColors(ByVal cElements As Integer, ByVal lpaElements As Integer(), ByVal lpaRgbValues As UInteger()) As Boolean
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)>
        Public Shared Function SystemParametersInfo(ByVal uAction As Integer, ByVal uParam As Integer, ByRef lpvParam As NONCLIENTMETRICS, ByVal fuWinIni As SPIF) As Integer
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)>
        Public Shared Function SystemParametersInfo(ByVal uAction As Integer, ByVal uParam As Integer, ByRef lpvParam As LogFontStr, ByVal fuWinIni As SPIF) As Integer
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)>
        Public Shared Function SystemParametersInfo(ByVal uAction As Integer, ByVal uParam As Integer, ByRef lpvParam As ICONMETRICS, ByVal fuWinIni As SPIF) As Integer
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)>
        Public Shared Function SystemParametersInfo(ByVal uAction As Integer, ByVal uParam As Integer, ByRef lpvParam As Integer, ByVal fuWinIni As SPIF) As Integer
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)>
        Public Shared Function SystemParametersInfo(ByVal uAction As Integer, ByVal uParam As Integer, ByRef lpvParam As Boolean, ByVal fuWinIni As SPIF) As Integer
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)>
        Public Shared Function SystemParametersInfo(ByVal uAction As Integer, ByVal uParam As Boolean, ByRef lpvParam As Integer, ByVal fuWinIni As SPIF) As Integer
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)>
        Public Shared Function SystemParametersInfo(ByVal uAction As Integer, ByVal uParam As Integer, ByRef lpvParam As ANIMATIONINFO, ByVal fuWinIni As SPIF) As Integer
        End Function

        <DllImport("user32.dll", SetLastError:=True)>
        Public Shared Function SystemParametersInfo(ByVal uiAction As Integer, ByVal uiParam As UInteger, ByVal pvParam As IntPtr, ByVal fWinIni As SPIF) As Boolean
        End Function

        <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
        Public Shared Function SystemParametersInfo(ByVal uiAction As UInteger, ByVal uiParam As UInteger, ByVal pvParam As String, ByVal fWinIni As SPIF) As Boolean
        End Function

        <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
        Public Shared Function SystemParametersInfo(ByVal uiAction As UInteger, ByVal uiParam As UInteger, ByVal pvParam As StringBuilder, ByVal fWinIni As SPIF) As Boolean
        End Function

        ''' <summary>
        ''' SPI: System-wide parameter - Used in SystemParametersInfo function
        ''' </summary>
        Public Class SPI
            Enum Icons
                ''' <summary>
                ''' <b>Sets or retrieves the width, in pixels, of an icon cell. The system uses this rectangle to arrange icons in large icon view.</b>
                ''' <br></br>
                ''' <br></br> • To set this value, set uiParam to the new value and set pvParam to null. You cannot set this value to less than SM_CXICON.
                ''' <br></br> • To retrieve this value, pvParam must point to an integer that receives the current value.
                ''' </summary>
                ICONHORIZONTALSPACING = &HD

                ''' <summary>
                ''' <b>Sets or retrieves the height, in pixels, of an icon cell.</b>
                ''' <br></br>
                ''' <br></br> • To set this value, set uiParam to the new value and set pvParam to null. You cannot set this value to less than SM_CYICON.
                ''' <br></br> • To retrieve this value, pvParam must point to an integer that receives the current value.
                ''' </summary>
                ICONVERTICALSPACING = &H18

                ''' <summary>
                ''' <b>Retrieves the logical font information for the current icon-title font.</b>
                ''' <br></br>
                ''' <br></br> • The uiParam parameter specifies the size of a LOGFONT structure.
                ''' <br></br> • The pvParam parameter must point to the LOGFONT structure to fill in.
                ''' </summary>
                GETICONTITLELOGFONT = &H1F

                ''' <summary>
                ''' <b>Sets the font that is used for icon titles.</b>
                ''' <br></br>
                ''' <br></br> • The uiParam parameter specifies the size of a LOGFONT structure.
                ''' <br></br> • The pvParam parameter must point to a LOGFONT structure.
                ''' </summary>
                SETICONTITLELOGFONT = &H22

                ''' <summary>
                ''' <b>Retrieves the metrics associated with icons.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to an ICONMETRICS structure that receives the information.
                ''' <br></br> • Set the cbSize member of this structure and the uiParam parameter to sizeof(ICONMETRICS).
                ''' </summary>
                GETICONMETRICS = &H2D

                ''' <summary>
                ''' <b>Sets the metrics associated with icons.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to an ICONMETRICS structure that contains the new parameters. 
                ''' <br></br> • Set the cbSize member of this structure and the uiParam parameter to sizeof(ICONMETRICS).
                ''' </summary>
                SETICONMETRICS = &H2E

                ''' <summary>
                ''' <b>Reloads the system icons.</b>
                ''' <br></br>
                ''' <br></br> • Set the uiParam parameter to zero.
                ''' <br></br> • Set the pvParam parameter to null.
                ''' </summary>
                SETICONS = &H58

            End Enum

            Enum Desktop
                ''' <summary>
                ''' <b>Sets the desktop wallpaper.</b>
                ''' <br></br>
                ''' <br></br> • The value of the pvParam parameter determines the new wallpaper. 
                ''' <br></br> • To specify a wallpaper bitmap, set pvParam to point to a null-terminated string containing the name of a bitmap file. Setting pvParam to "" removes the wallpaper.
                ''' <br></br> • Setting pvParam to SETWALLPAPER_DEFAULT or null reverts to the default wallpaper.
                ''' </summary>
                SETDESKWALLPAPER = &H14

                ''' <summary>
                ''' <b>Sets the current desktop pattern by causing Windows to read the Pattern= setting from the WIN.INI file.</b>
                ''' </summary>
                SETDESKPATTERN = &H15
            End Enum

            Enum Metrics
                ''' <summary>
                ''' <b>Retrieves the metrics associated with the nonclient area of nonminimized windows.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to a NONCLIENTMETRICS structure that receives the information. Set the cbSize member of this structure.
                ''' <br></br> • The uiParam parameter to sizeof(NONCLIENTMETRICS).
                ''' </summary>
                GETNONCLIENTMETRICS = &H29

                ''' <summary>
                ''' <b>Sets the metrics associated with the nonclient area of nonminimized windows.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to a NONCLIENTMETRICS structure that contains the new parameters. 
                ''' <br></br> • Set the cbSize member of this structure and the uiParam parameter to sizeof(NONCLIENTMETRICS). Also, the lfHeight member of the LOGFONT structure must be a negative value.
                ''' </summary>
                SETNONCLIENTMETRICS = &H2A
            End Enum

            Enum Cursors
                ''' <summary>
                ''' <b>Reloads the system cursors.</b>
                ''' <br></br>
                ''' <br></br> • Set the uiParam parameter to zero.
                ''' <br></br> • Set the pvParam parameter to null.
                ''' </summary>
                SETCURSORS = &H57

                ''' <summary>
                ''' <b>Determines whether the cursor has a shadow around it.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to a BOOL variable that receives True if the shadow is enabled, False if it is disabled.
                ''' <br></br> • This effect appears only if the system has a color depth of more than 256 colors.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value Is not supported.</i>
                ''' </summary>
                GETCURSORSHADOW = &H101A

                ''' <summary>
                ''' <b>Enables or disables a shadow around the cursor.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter is a BOOL variable. Set pvParam to 1 to enable the shadow or 0 to disable the shadow.
                ''' <br></br> • This effect appears only if the system has a color depth of more than 256 colors.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                ''' </summary>
                SETCURSORSHADOW = &H101B
            End Enum

            Enum FontSmoothing
                ''' <summary>
                ''' <b>Sets the contrast value used in ClearType smoothing.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter points to a int that holds the contrast value.
                ''' <br></br> • Valid contrast values are from 1000 to 2200. The default value is 1400.
                ''' <br></br> • When using this option, the fWinIni parameter must be set to SPIF_SENDWININICHANGE | SPIF_UPDATEINIFILE; otherwise, SystemParametersInfo fails.
                ''' <br></br> • SPI_SETFONTSMOOTHINGTYPE must also be set to FE_FONTSMOOTHINGCLEARTYPE.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows 2000/NT, Windows Me/98/95:  This value is not supported.</i>
                ''' </summary>
                SPI_SETFONTSMOOTHINGCONTRAST = &H200D

                ''' <summary>
                ''' <b>Sets the font smoothing type.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter points to a int that contains either FE_FONTSMOOTHINGSTANDARD,
                ''' <br></br> • if standard anti-aliasing is used, or FE_FONTSMOOTHINGCLEARTYPE, if ClearType is used. The default is FE_FONTSMOOTHINGSTANDARD.
                ''' <br></br> • When using this option, the fWinIni parameter must be set to SPIF_SENDWININICHANGE | SPIF_UPDATEINIFILE; otherwise, SystemParametersInfo fails.
                ''' </summary>
                SPI_SETFONTSMOOTHINGTYPE = &H200B

                ''' <summary>
                ''' <b>Retrieves a contrast value that is used in ClearType™ smoothing.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to a int that receives the information.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows 2000/NT, Windows Me/98/95:  This value is not supported.</i>
                ''' </summary>
                SPI_GETFONTSMOOTHINGCONTRAST = &H200C

                ''' <summary>
                ''' <i>(!) Not implemented.</i>
                ''' </summary>
                SPI_GETFONTSMOOTHINGORIENTATION = &H2012

                ''' <summary>
                ''' <i>(!) Not implemented.</i>
                ''' </summary>
                SPI_SETFONTSMOOTHINGORIENTATION = &H2013
            End Enum

            Enum Titlebars
                ''' <summary>
                ''' <b>Determines whether the gradient effect for window title bars is enabled.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE for enabled, or FALSE for disabled.
                ''' <br></br> • For more information about the gradient effect, see the GetSysColor function.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                ''' </summary>
                GETGRADIENTCAPTIONS = &H1008

                ''' <summary>
                ''' <b>Enables or disables the gradient effect for window title bars.</b>
                ''' <br></br>
                ''' <br></br> • Set the pvParam parameter to 1 to enable it, or 0 to disable it.
                ''' <br></br> • The gradient effect is possible only if the system has a color depth of more than 256 colors.
                ''' <br></br> • For more information about the gradient effect, see the GetSysColor function.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                ''' </summary>
                SETGRADIENTCAPTIONS = &H1009
            End Enum

            Enum Effects
                ''' <summary>
                ''' <b>Retrieves the animation effects associated with user actions.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to an ANIMATIONINFO structure that receives the information. 
                ''' <br></br> • Set the cbSize member of this structure.
                ''' <br></br> • Set the uiParam parameter to sizeof(ANIMATIONINFO).
                ''' </summary>
                GETANIMATION = &H48

                ''' <summary>
                ''' <b>Sets the animation effects associated with user actions.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to an ANIMATIONINFO structure that contains the new parameters.
                ''' <br></br> • Set the cbSize member of this structure and the uiParam parameter to sizeof(ANIMATIONINFO).
                ''' </summary>
                SETANIMATION = &H49

                ''' <summary>
                ''' <b>Determines whether the menu animation feature is enabled. This master switch must be on to enable menu animation effects.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if animation is enabled and FALSE if it is disabled.
                ''' <br></br> • If animation is enabled, GETMENUFADE indicates whether menus use fade or slide animation.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                ''' </summary>
                GETMENUANIMATION = &H1002

                ''' <summary>
                ''' <b>Enables or disables menu animation. This master switch must be on for any menu animation to occur.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter is a BOOL variable; set pvParam to 1 to enable animation and 0 to disable animation.
                ''' <br></br> • If animation is enabled, GETMENUFADE indicates whether menus use fade or slide animation.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                ''' </summary>
                SETMENUANIMATION = &H1003

                ''' <summary>
                ''' <b>Determines whether the slide-open effect for combo boxes is enabled.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE for enabled, or FALSE for disabled.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                ''' </summary>
                GETCOMBOBOXANIMATION = &H1004

                ''' <summary>
                ''' <b>Enables or disables the slide-open effect for combo boxes.</b>
                ''' <br></br>
                ''' <br></br> • Set the pvParam parameter to 1 to enable the gradient effect, or 0 to disable it.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                ''' </summary>
                SETCOMBOBOXANIMATION = &H1005

                ''' <summary>
                ''' <b>Determines whether the smooth-scrolling effect for list boxes is enabled.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE for enabled, or FALSE for disabled.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                ''' </summary>
                GETLISTBOXSMOOTHSCROLLING = &H1006

                ''' <summary>
                ''' <b>Enables or disables the smooth-scrolling effect for list boxes.</b>
                ''' <br></br>
                ''' <br></br> • Set the pvParam parameter to 1 to enable the smooth-scrolling effect, or 0 to disable it.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                ''' </summary>
                SETLISTBOXSMOOTHSCROLLING = &H1007

                ''' <summary>
                ''' <b>Determines whether menu fade animation is enabled.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE when fade animation is enabled and FALSE when it is disabled.
                ''' <br></br> • If fade animation is disabled, menus use slide animation. This flag is ignored unless menu animation is enabled, which you can do using the SETMENUANIMATION flag.
                ''' <br></br> • For more information, see AnimateWindow.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                ''' </summary>
                GETMENUFADE = &H1012

                ''' <summary>
                ''' <b>Enables or disables menu fade animation.</b>
                ''' <br></br>
                ''' <br></br> • Set pvParam to 1 to enable the menu fade effect or 0 to disable it (i.e. use slide animation).
                ''' <br></br> • If fade animation is disabled, menus use slide animation. he The menu fade effect is possible only if the system has a color depth of more than 256 colors.
                ''' <br></br> • This flag is ignored unless SPI_MENUANIMATION is also set.
                ''' <br></br> • For more information, see AnimateWindow.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                ''' </summary>
                SETMENUFADE = &H1013

                ''' <summary>
                ''' <b>Determines whether the selection fade effect is enabled.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if enabled or FALSE if disabled.
                ''' <br></br> • The selection fade effect causes the menu item selected by the user to remain on the screen briefly while fading out after the menu is dismissed.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                ''' </summary>
                GETSELECTIONFADE = &H1014

                ''' <summary>
                ''' <b>The selection fade effect causes the menu item selected by the user to remain on the screen briefly while fading out after the menu is dismissed. The selection fade effect is possible only if the system has a color depth of more than 256 colors.</b>
                ''' <br></br>
                ''' <br></br> • Set pvParam to 1 to enable the selection fade effect or 0 to disable it.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                ''' </summary>
                SETSELECTIONFADE = &H1015

                ''' <summary>
                ''' <b>Determines whether ToolTip animation is enabled.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if enabled or FALSE if disabled.
                ''' <br></br> • If ToolTip animation is enabled, GETTOOLTIPFADE indicates whether ToolTips use fade or slide animation.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                ''' </summary>
                GETTOOLTIPANIMATION = &H1016

                ''' <summary>
                ''' <b>Sets ToolTip animation if enabled or not.</b>
                ''' <br></br>
                ''' <br></br> • Set pvParam to 1 to enable ToolTip animation or 0 to disable it. If enabled, you can use SETTOOLTIPFADE to specify fade or slide animation.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                ''' </summary>
                SETTOOLTIPANIMATION = &H1017

                ''' <summary>
                ''' <b>If SETTOOLTIPANIMATION is enabled, GETTOOLTIPFADE indicates whether ToolTip animation uses a fade effect or a slide effect.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE for fade animation or FALSE for slide animation.
                ''' <br></br> • For more information on slide and fade effects, see AnimateWindow.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                ''' </summary>
                GETTOOLTIPFADE = &H1018

                ''' <summary>
                ''' <b>If the SETTOOLTIPANIMATION flag is enabled, use SETTOOLTIPFADE to indicate whether ToolTip animation uses a fade effect or a slide effect.</b>
                ''' <br></br>
                ''' <br></br> • Set pvParam to TRUE for fade animation or FALSE for slide animation. The tooltip fade effect is possible only if the system has a color depth of more than 256 colors.
                ''' <br></br> • For more information on the slide and fade effects, see the AnimateWindow function.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                ''' </summary>
                SETTOOLTIPFADE = &H1019


                ''' <summary>
                ''' <b>Determines whether native User menus have flat menu appearance.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to a BOOL variable that returns TRUE if the flat menu appearance is set, or FALSE otherwise.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows 2000/NT, Windows Me/98/95:  This value is not supported.</i>
                ''' </summary>
                GETFLATMENU = &H1022

                ''' <summary>
                ''' <b>Enables or disables flat menu appearance for native User menus.</b>
                ''' <br></br>
                ''' <br></br> • Set pvParam to 1 to enable flat menu appearance or 0 to disable it.
                ''' <br></br> • When enabled, the menu bar uses COLOR_MENUBAR for the menubar background, COLOR_MENU for the menu-popup background, COLOR_MENUHILIGHT for the fill of the current menu selection, and COLOR_HILIGHT for the outline of the current menu selection.
                ''' <br></br> • If disabled, menus are drawn using the same metrics and colors as in Windows 2000 and earlier.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows 2000/NT, Windows Me/98/95:  This value is not supported.</i>
                ''' </summary>
                SETFLATMENU = &H1023

                ''' <summary>
                ''' <b>Determines whether the drop shadow effect is enabled.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to a BOOL variable that returns TRUE if enabled or FALSE if disabled.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows 2000/NT, Windows Me/98/95:  This value is not supported.</i>
                ''' </summary>
                GETDROPSHADOW = &H1024

                ''' <summary>
                ''' <b>Enables or disables the drop shadow effect.</b>
                ''' <br></br>
                ''' <br></br> • Set pvParam to 1 to enable the drop shadow effect or 0 to disable it.
                ''' <br></br> • You must also have CS_DROPSHADOW in the window class style.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows 2000/NT, Windows Me/98/95:  This value is not supported.</i>
                ''' </summary>
                SETDROPSHADOW = &H1025

                ''' <summary>
                ''' <b>Determines whether UI effects are enabled or disabled.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if all UI effects are enabled, or FALSE if they are disabled.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                ''' </summary>
                GETUIEFFECTS = &H103E

                ''' <summary>
                ''' <b>Enables or disables UI effects.</b>
                ''' <br></br>
                ''' <br></br> • Set the pvParam parameter to 1 to enable all UI effects or 0 to disable all UI effects.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                ''' </summary>
                SETUIEFFECTS = &H103F
            End Enum

        End Class

        <Flags>
        Enum SPIF
            None = &H0
            UpdateINIFile = &H1                 ' Writes the new system-wide parameter setting to the user profile.
            SendChange = &H2                    ' Broadcasts the WM_SETTINGCHANGE message after updating the user profile.
            SendWinINIChange = SendChange       ' Same as SENDCHANGE.
        End Enum


        Public Declare Function SystemParametersInfo Lib "user32" Alias "SystemParametersInfoA" (uAction As Integer, uParam As Integer, lpvParam As Integer, fuWinIni As Integer) As Integer
        Public Declare Function SystemParametersInfo Lib "user32" Alias "SystemParametersInfoA" (uAction As Integer, uParam As Integer, lpvParam As String, fuWinIni As Integer) As Integer
        Public Declare Function SystemParametersInfo Lib "user32" Alias "SystemParametersInfoA" (uAction As Integer, uParam As Integer, ByRef lpvParam As Boolean, fuWinIni As Integer) As Integer

        Friend Declare Function SetWindowCompositionAttribute Lib "user32.dll" (ByVal hwnd As IntPtr, ByRef data As WindowCompositionAttributeData) As Integer
        Public Declare Auto Function FindWindow Lib "user32.dll" (ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
        Declare Function LoadCursorFromFile Lib "user32.dll" Alias "LoadCursorFromFileA" (ByVal lpFileName As String) As IntPtr


        <StructLayout(LayoutKind.Sequential)>
        Friend Structure AccentPolicy
            Public AccentState As AccentState
            Public AccentFlags As Integer
            Public GradientColor As Integer
            Public AnimationId As Integer
        End Structure
        Friend Structure WindowCompositionAttributeData
            Public Attribute As WindowCompositionAttribute
            Public Data As IntPtr
            Public SizeOfData As Integer
        End Structure
        Public Enum WindowCompositionAttribute
            WCA_ACCENT_POLICY = 19
            WCA_USEDARKMODECOLORS = 26
        End Enum
        Friend Enum AccentState
            ACCENT_DISABLED = 0
            ACCENT_ENABLE_GRADIENT = 1
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2
            ACCENT_ENABLE_BLURBEHIND = 3
            ACCENT_ENABLE_TRANSPARANT = 6
            ACCENT_ENABLE_ACRYLICBLURBEHIND = 4
        End Enum

        <Flags>
        Enum AnimateWindowFlags
            AW_HOR_POSITIVE = &H0
            AW_HOR_NEGATIVE = &H2
            AW_VER_POSITIVE = &H4
            AW_VER_NEGATIVE = &H8
            AW_CENTER = &H10
            AW_HIDE = &H10000
            AW_ACTIVATE = &H20000
            AW_SLIDE = &H40000
            AW_BLEND = &H80000
        End Enum
        Enum OCR_SYSTEM_CURSORS As Integer

            ' Standard arrow And small hourglass
            OCR_APPSTARTING = 32650

            'Standard arrow
            OCR_NORMAL = 32512

            'Crosshair
            OCR_CROSS = 32515

            'Windows 2000/XP: Hand
            OCR_HAND = 32649

            'Arrow And question mark
            OCR_HELP = 32651

            'I-beam
            OCR_IBEAM = 32513

            'Slashed circle
            OCR_NO = 32648

            'Four-pointed arrow pointing north south east And west
            OCR_SIZEALL = 32646

            'Double-pointed arrow pointing northeast And southwest
            OCR_SIZENESW = 32643

            'Double-pointed arrow pointing north And south
            OCR_SIZENS = 32645

            'Double-pointed arrow pointing northwest And southeast
            OCR_SIZENWSE = 32642

            'Double-pointed arrow pointing west And east
            OCR_SIZEWE = 32644

            'Vertical arrow
            OCR_UP = 32516

            'Hourglass
            OCR_WAIT = 32514
        End Enum
        Enum SendMessageTimeoutFlags As UInteger
            SMTO_NORMAL = &H0
            SMTO_BLOCK = &H1
            SMTO_ABORTIFHUNG = &H2
            SMTO_NOTIMEOUTIFNOTHUNG = &H8
        End Enum

        Public Shared WM_DWMCOLORIZATIONCOLORCHANGED As Integer = &H320
        Public Shared WM_DWMCOMPOSITIONCHANGED As Integer = &H31E
        Public Shared WM_THEMECHANGED As Integer = &H31A
        Public Shared WM_SYSCOLORCHANGE As Integer = &H15
        Public Shared WM_PALETTECHANGED As Integer = &H311
        Public Shared WM_WININICHANGE As UInteger = &H1A
        Public Shared WM_SETTINGCHANGE As UInteger = WM_WININICHANGE
        Public Shared HWND_MESSAGE As Int32 = -&H3
        Public Shared HWND_BROADCAST As New IntPtr(&HFFFF)
        Public Shared MSG_TIMEOUT As Integer = 5000
        Public Shared RESULT As UIntPtr
    End Class

    Public Class Kernel32
        <DllImport("kernel32.dll", SetLastError:=True)>
        Public Shared Function LoadLibraryEx(ByVal lpFileName As String, ByVal hFile As IntPtr, ByVal dwFlags As UInteger) As IntPtr
        End Function
        <DllImport("kernel32.dll")>
        Public Shared Function FindResource(ByVal hModule As IntPtr, ByVal lpID As Integer, ByVal lpType As String) As IntPtr
        End Function
        <DllImport("kernel32.dll", SetLastError:=True)>
        Public Shared Function LoadResource(ByVal hModule As IntPtr, ByVal hResInfo As IntPtr) As IntPtr
        End Function
        <DllImport("kernel32.dll", SetLastError:=True)>
        Public Shared Function SizeofResource(ByVal hModule As IntPtr, ByVal hResInfo As IntPtr) As UInteger
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)>
        Public Shared Function Wow64DisableWow64FsRedirection(ByRef ptr As IntPtr) As Boolean
        End Function
        <DllImport("kernel32.dll", SetLastError:=True)>
        Public Shared Function Wow64RevertWow64FsRedirection(ByVal ptr As IntPtr) As Boolean
        End Function
    End Class

    Public Class Uxtheme
        <DllImport("UxTheme.DLL", BestFitMapping:=False, CallingConvention:=CallingConvention.Winapi, CharSet:=CharSet.Unicode, EntryPoint:="#65")>
        Public Shared Function SetSystemVisualStyle(ByVal pszFilename As String, ByVal pszColor As String, ByVal pszSize As String, ByVal dwReserved As Integer) As Integer
        End Function

        <DllImport("uxtheme", ExactSpelling:=True)>
        Public Shared Function EnableTheming(ByVal fEnable As Integer) As Integer
        End Function

        Public Declare Unicode Function GetCurrentThemeName Lib "uxtheme" (ByVal stringThemeName As StringBuilder, ByVal lengthThemeName As Integer, ByVal stringColorName As StringBuilder, ByVal lengthColorName As Integer, ByVal stringSizeName As StringBuilder, ByVal lengthSizeName As Integer) As Int32

        <DllImport("uxtheme.dll", ExactSpelling:=True, CharSet:=CharSet.Unicode)>
        Public Shared Function SetWindowTheme(ByVal hwnd As IntPtr, ByVal pszSubAppName As String, ByVal pszSubIdList As String) As Integer
        End Function
    End Class

    Public Class Shell32
        <DllImport("shell32.dll")> Shared Sub SHChangeNotify(ByVal wEventId As Integer, ByVal uFlags As Integer, ByVal dwItem1 As Integer, ByVal dwItem2 As Integer)
        End Sub
        <DllImport("Shell32.dll", EntryPoint:="SHDefExtractIconW")>
        Public Shared Function SHDefExtractIconW(<MarshalAs(UnmanagedType.LPTStr)> ByVal pszIconFile As String, ByVal iIndex As Integer, ByVal uFlags As UInteger, ByRef phiconLarge As IntPtr, ByRef phiconSmall As IntPtr, ByVal nIconSize As UInteger) As Integer
        End Function
        <DllImport("Shell32.dll", SetLastError:=False)>
        Public Shared Function SHGetStockIconInfo(ByVal siid As SHSTOCKICONID, ByVal uFlags As SHGSI, ByRef psii As SHSTOCKICONINFO) As Int32
        End Function

        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
        Public Structure SHSTOCKICONINFO
            Public cbSize As UInt32
            Public hIcon As IntPtr
            Public iSysIconIndex As Int32
            Public iIcon As Int32
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=MAX_PATH)>
            Public szPath As String
        End Structure
        Public Enum SHSTOCKICONID
            ''' <summary>
            ''' Blank document icon (Document Of a type With no associated application).
            ''' </summary>
            DOCNOASSOC = 0

            ''' <summary>
            ''' Application-associated document icon (Document Of a type With an associated application).
            ''' </summary>
            DOCASSOC = 1

            ''' <summary>
            ''' Generic application With no custom icon.
            ''' </summary>
            APPLICATION = 2

            ''' <summary>
            ''' Folder (generic unspecified state).
            ''' </summary>
            FOLDER = 3

            ''' <summary>
            ''' Folder (open).
            ''' </summary>
            FOLDEROPEN = 4

            ''' <summary>
            ''' 5.25-inch disk drive.
            ''' </summary>
            DRIVE525 = 5

            ''' <summary>
            ''' 3.5-inch disk drive.
            ''' </summary>
            DRIVE35 = 6

            ''' <summary>
            ''' Removable drive.
            ''' </summary>
            DRIVEREMOVE = 7

            ''' <summary>
            ''' Fixed drive (hard disk).
            ''' </summary>
            DRIVEFIXED = 8

            ''' <summary>
            ''' Network drive (connected).
            ''' </summary>
            DRIVENET = 9

            ''' <summary>
            ''' Network drive (disconnected).
            ''' </summary>
            DRIVENETDISABLED = 10

            ''' <summary>
            ''' CD drive.
            ''' </summary>
            DRIVECD = 11

            ''' <summary>
            ''' RAM disk drive.
            ''' </summary>
            DRIVERAM = 12

            ''' <summary>
            ''' The entire network.
            ''' </summary>
            WORLD = 13

            ''' <summary>
            ''' A computer On the network.
            ''' </summary>
            SERVER = 15

            ''' <summary>
            ''' A local printer Or print destination.
            ''' </summary>
            PRINTER = 16

            ''' <summary>
            ''' The Network virtual folder (FOLDERID_NetworkFolder/CSIDL_NETWORK).
            ''' </summary>
            MYNETWORK = 17

            ''' <summary>
            ''' The Search feature.
            ''' </summary>
            FIND = 22

            ''' <summary>
            ''' The Help And Support feature.
            ''' </summary>
            HELP = 23

            ''' <summary>
            ''' Overlay For a Shared item.
            ''' </summary>
            SHARE = 28

            ''' <summary>
            ''' Overlay For a shortcut.
            ''' </summary>
            LINK = 29

            ''' <summary>
            ''' Overlay For items that are expected To be slow To access.
            ''' </summary>
            SLOWFILE = 30

            ''' <summary>
            ''' The Recycle Bin (empty).
            ''' </summary>
            RECYCLER = 31

            ''' <summary>
            ''' The Recycle Bin (Not empty).
            ''' </summary>
            RECYCLERFULL = 32

            ''' <summary>
            ''' Audio CD media.
            ''' </summary>
            MEDIACDAUDIO = 40

            ''' <summary>
            ''' Security lock.
            ''' </summary>
            LOCK = 47

            ''' <summary>
            ''' A virtual folder that contains the results Of a search.
            ''' </summary>
            AUTOLIST = 49

            ''' <summary>
            ''' A network printer.
            ''' </summary>
            PRINTERNET = 50

            ''' <summary>
            ''' A server Shared On a network.
            ''' </summary>
            SERVERSHARE = 51

            ''' <summary>
            ''' A local fax printer.
            ''' </summary>
            PRINTERFAX = 52

            ''' <summary>
            ''' A network fax printer.
            ''' </summary>
            PRINTERFAXNET = 53

            ''' <summary>
            ''' A file that receives the output Of a Print To file operation.
            ''' </summary>
            PRINTERFILE = 54

            ''' <summary>
            ''' A category that results from a Stack by command To organize the contents Of a folder.
            ''' </summary>
            STACK = 55

            ''' <summary>
            ''' Super Video CD (SVCD) media.
            ''' </summary>
            MEDIASVCD = 56

            ''' <summary>
            ''' A folder that contains only subfolders As child items.
            ''' </summary>
            STUFFEDFOLDER = 57

            ''' <summary>
            ''' Unknown drive type.
            ''' </summary>
            DRIVEUNKNOWN = 58

            ''' <summary>
            ''' DVD drive.
            ''' </summary>
            DRIVEDVD = 59

            ''' <summary>
            ''' DVD media.
            ''' </summary>
            MEDIADVD = 60

            ''' <summary>
            ''' DVD-RAM media.
            ''' </summary>
            MEDIADVDRAM = 61

            ''' <summary>
            ''' DVD-RW media.
            ''' </summary>
            MEDIADVDRW = 62

            ''' <summary>
            ''' DVD-R media.
            ''' </summary>
            MEDIADVDR = 63

            ''' <summary>
            ''' DVD-ROM media.
            ''' </summary>
            MEDIADVDROM = 64

            ''' <summary>
            ''' CD+ (enhanced audio CD) media.
            ''' </summary>
            MEDIACDAUDIOPLUS = 65

            ''' <summary>
            ''' CD-RW media.
            ''' </summary>
            MEDIACDRW = 66

            ''' <summary>
            ''' CD-R media.
            ''' </summary>
            MEDIACDR = 67

            ''' <summary>
            ''' A writeable CD In the process Of being burned.
            ''' </summary>
            MEDIACDBURN = 68

            ''' <summary>
            ''' Blank writable CD media.
            ''' </summary>
            MEDIABLANKCD = 69

            ''' <summary>
            ''' CD-ROM media.
            ''' </summary>
            MEDIACDROM = 70

            ''' <summary>
            ''' An audio file.
            ''' </summary>
            AUDIOFILES = 71

            ''' <summary>
            ''' An image file.
            ''' </summary>
            IMAGEFILES = 72

            ''' <summary>
            ''' A video file.
            ''' </summary>
            VIDEOFILES = 73

            ''' <summary>
            ''' A mixed file.
            ''' </summary>
            MIXEDFILES = 74

            ''' <summary>
            ''' Folder back.
            ''' </summary>
            FOLDERBACK = 75

            ''' <summary>
            ''' Folder front.
            ''' </summary>
            FOLDERFRONT = 76

            ''' <summary>
            ''' Security shield. Use For UAC prompts only.
            ''' </summary>
            SHIELD = 77

            ''' <summary>
            ''' Warning.
            ''' </summary>
            WARNING = 78

            ''' <summary>
            ''' Informational.
            ''' </summary>
            INFO = 79

            ''' <summary>
            ''' Error.
            ''' </summary>
            [Error] = 80

            ''' <summary>
            ''' Key.
            ''' </summary>
            KEY = 81

            ''' <summary>
            ''' Software.
            ''' </summary>
            SOFTWARE = 82

            ''' <summary>
            ''' A UI item such As a button that issues a rename command.
            ''' </summary>
            RENAME = 83

            ''' <summary>
            ''' A UI item such As a button that issues a delete command.
            ''' </summary>
            DELETE = 84

            ''' <summary>
            ''' Audio DVD media.
            ''' </summary>
            MEDIAAUDIODVD = 85

            ''' <summary>
            ''' Movie DVD media.
            ''' </summary>
            MEDIAMOVIEDVD = 86

            ''' <summary>
            ''' Enhanced CD media.
            ''' </summary>
            MEDIAENHANCEDCD = 87

            ''' <summary>
            ''' Enhanced DVD media.
            ''' </summary>
            MEDIAENHANCEDDVD = 88

            ''' <summary>
            ''' High definition DVD media In the HD DVD format.
            ''' </summary>
            MEDIAHDDVD = 89

            ''' <summary>
            ''' High definition DVD media In the Blu-ray Disc™ format.
            ''' </summary>
            MEDIABLURAY = 90

            ''' <summary>
            ''' Video CD (VCD) media.
            ''' </summary>
            MEDIAVCD = 91

            ''' <summary>
            ''' DVD+R media.
            ''' </summary>
            MEDIADVDPLUSR = 92

            ''' <summary>
            ''' DVD+RW media.
            ''' </summary>
            MEDIADVDPLUSRW = 93

            ''' <summary>
            ''' A desktop computer.
            ''' </summary>
            DESKTOPPC = 94

            ''' <summary>
            ''' A mobile computer (laptop).
            ''' </summary>
            MOBILEPC = 95

            ''' <summary>
            ''' The User Accounts Control Panel item.
            ''' </summary>
            USERS = 96

            ''' <summary>
            ''' Smart media.
            ''' </summary>
            MEDIASMARTMEDIA = 97

            ''' <summary>
            ''' CompactFlash media.
            ''' </summary>
            MEDIACOMPACTFLASH = 98

            ''' <summary>
            ''' A cell phone.
            ''' </summary>
            DEVICECELLPHONE = 99

            ''' <summary>
            ''' A digital camera.
            ''' </summary>
            DEVICECAMERA = 100

            ''' <summary>
            ''' A digital video camera.
            ''' </summary>
            DEVICEVIDEOCAMERA = 101

            ''' <summary>
            ''' An audio player.
            ''' </summary>
            DEVICEAUDIOPLAYER = 102

            ''' <summary>
            ''' Connect To network.
            ''' </summary>
            NETWORKCONNECT = 103

            ''' <summary>
            ''' The Network And Internet Control Panel item.
            ''' </summary>
            INTERNET = 104

            ''' <summary>
            ''' A compressed file With a .zip file name extension.
            ''' </summary>
            ZIPFILE = 105

            ''' <summary>
            ''' The Additional Options Control Panel item.
            ''' </summary>
            SETTINGS = 106

            ''' <summary>
            ''' High definition DVD drive (any type - HD DVD-ROM HD DVD-R HD-DVD-RAM) that uses the HD DVD format.
            ''' Windows Vista With Service Pack 1 (SP1) And later. 
            ''' </summary>
            DRIVEHDDVD = 132

            ''' <summary>
            ''' High definition DVD drive (any type - BD-ROM BD-R BD-RE) that uses the Blu-ray Disc format.
            ''' Windows Vista With Service Pack 1 (SP1) And later. 
            ''' </summary>
            DRIVEBD = 133

            ''' <summary>
            ''' High definition DVD-ROM media In the HD DVD-ROM format.
            ''' Windows Vista With Service Pack 1 (SP1) And later. 
            ''' </summary>
            MEDIAHDDVDROM = 134

            ''' <summary>
            ''' High definition DVD-R media In the HD DVD-R format.
            ''' Windows Vista With Service Pack 1 (SP1) And later. 
            ''' </summary>
            MEDIAHDDVDR = 135

            ''' <summary>
            ''' High definition DVD-RAM media In the HD DVD-RAM format.
            ''' Windows Vista With Service Pack 1 (SP1) And later. 
            ''' </summary>
            MEDIAHDDVDRAM = 136

            ''' <summary>
            ''' High definition DVD-ROM media In the Blu-ray Disc BD-ROM format.
            ''' Windows Vista With Service Pack 1 (SP1) And later. 
            ''' </summary>
            MEDIABDROM = 137

            ''' <summary>
            ''' High definition write-once media In the Blu-ray Disc BD-R format.
            ''' Windows Vista With Service Pack 1 (SP1) And later. 
            ''' </summary>
            MEDIABDR = 138

            ''' <summary>
            ''' High definition read/write media In the Blu-ray Disc BD-RE format.
            ''' Windows Vista With Service Pack 1 (SP1) And later. 
            ''' </summary>
            MEDIABDRE = 139

            ''' <summary>
            ''' A cluster disk array.
            ''' Windows Vista With Service Pack 1 (SP1) And later. 
            ''' </summary>
            CLUSTEREDDRIVE = 140

            ''' <summary>
            ''' The highest valid value In the enumeration. Values over 160 are Windows 7-only icons.
            ''' </summary>
            MAX_ICONS = 174
        End Enum
        <Flags>
        Public Enum SHGSI
            ICONLOCATION = 0
            ICON = &H100
            SYSICONINDEX = &H4000
            LINKOVERLAY = &H8000
            SELECTED = &H10000
            LARGEICON = &H0
            SMALLICON = &H1
            SHELLICONSIZE = &H4
        End Enum

        Public Const MAX_PATH As Integer = 260
        Public Const SHCNE_ASSOCCHANGED = &H8000000
        Public Const SHCNF_IDLIST = 0
    End Class

    Public Class GDI32
        <DllImport("gdi32.dll", CharSet:=CharSet.Auto)>
        Public Shared Function GetTextMetrics(ByVal hdc As IntPtr, <Out> ByRef lptm As TEXTMETRICW) As Boolean
        End Function
        <DllImport("gdi32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
        Public Shared Function DeleteObject(ByVal hObject As IntPtr) As Boolean
        End Function
        <DllImport("gdi32.dll", CharSet:=CharSet.Auto)>
        Public Shared Function SelectObject(ByVal hdc As IntPtr, ByVal hgdiObj As IntPtr) As IntPtr
        End Function

        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
        Public Structure TEXTMETRICW
            Public tmHeight As Integer
            Public tmAscent As Integer
            Public tmDescent As Integer
            Public tmInternalLeading As Integer
            Public tmExternalLeading As Integer
            Public tmAveCharWidth As Integer
            Public tmMaxCharWidth As Integer
            Public tmWeight As Integer
            Public tmOverhang As Integer
            Public tmDigitizedAspectX As Integer
            Public tmDigitizedAspectY As Integer
            Public tmFirstChar As UShort
            Public tmLastChar As UShort
            Public tmDefaultChar As UShort
            Public tmBreakChar As UShort
            Public tmItalic As Byte
            Public tmUnderlined As Byte
            Public tmStruckOut As Byte
            Public tmPitchAndFamily As Byte
            Public tmCharSet As Byte
        End Structure

        Public Shared Iterator Function GetFixedWidthFonts(ByVal dc As IDeviceContext) As IEnumerable(Of FontFamily)
            Dim hDC As IntPtr = dc.GetHdc()

            For Each oFontFamily As System.Drawing.FontFamily In System.Drawing.FontFamily.Families

                Try
                    If oFontFamily.IsStyleAvailable(FontStyle.Regular) Then
                        Using oFont As System.Drawing.Font = New System.Drawing.Font(oFontFamily, 10)
                            Dim hFont As IntPtr = IntPtr.Zero
                            Dim hFontDefault As IntPtr = IntPtr.Zero

                            Try
                                Dim oTextMetric As TEXTMETRICW
                                hFont = oFont.ToHfont()
                                hFontDefault = SelectObject(hDC, hFont)

                                If GetTextMetrics(hDC, oTextMetric) Then

                                    If (oTextMetric.tmPitchAndFamily And 1) = 0 Then
                                        Yield oFontFamily
                                    End If
                                End If

                            Finally

                                If hFontDefault <> IntPtr.Zero Then
                                    SelectObject(hDC, hFontDefault)
                                End If

                                If hFont <> IntPtr.Zero Then
                                    DeleteObject(hFont)
                                End If

                            End Try
                        End Using
                    End If
                Catch

                End Try
            Next

            dc.ReleaseHdc()
        End Function
    End Class

    ''' <summary>
    ''' Functions not found internally in system DLLs, but uses the functions in DLLs to do something DLLs Functions cannot do alone.
    ''' </summary>
    Public Class DLLFunc
#Region "User32\Shell32"
        Private Shared Function MAKEICONSIZE(ByVal low As Integer, ByVal high As Integer) As Integer
            Return (high << 16) Or (low And &HFFFF)
        End Function
        Public Shared Function ExtractSmallIcon(Path As String, Optional IconIndex As Integer = 0)
            Dim ico As Icon = Nothing
            'Make the nIconSize value (See the Msdn documents). The LOWORD is the Large Icon Size. The HIWORD is the Small Icon Size.
            'The largest size for an icon is 256.
            Dim LargeAndSmallSize As UInteger = CUInt(MAKEICONSIZE(256, 16))

            Dim hLrgIcon As IntPtr = IntPtr.Zero
            Dim hSmlIcon As IntPtr = IntPtr.Zero

            Dim result As Integer = Shell32.SHDefExtractIconW(Path, IconIndex, 0, hLrgIcon, hSmlIcon, LargeAndSmallSize)

            If result = 0 Then
                If ico IsNot Nothing Then ico.Dispose()

                'if the large and/or small icons where created in the unmanaged memory successfuly then create
                'a clone of them in the managed icons and delete the icons in the unmanaged memory.

                If hSmlIcon <> IntPtr.Zero Then
                    ico = CType(Icon.FromHandle(hSmlIcon).Clone, Icon)
                    User32.DestroyIcon(hSmlIcon)
                End If

            End If

            Return ico
        End Function
        Public Shared Function GetSystemIcon(_Icon As Shell32.SHSTOCKICONID, _Type As Shell32.SHGSI) As Icon
            Dim sii As New Shell32.SHSTOCKICONINFO With {.cbSize = Marshal.SizeOf(GetType(Shell32.SHSTOCKICONINFO))}
            Shell32.SHGetStockIconInfo(_Icon, _Type, sii)
            Return Icon.FromHandle(sii.hIcon)
        End Function

#End Region

#Region "Kernel32"
        Public Shared Function GetDllRes(File As String, ResourceID As Integer, Optional ResourceType As String = "IMAGE", Optional UnfoundW As Integer = 50, Optional UnfoundH As Integer = 50) As Bitmap
            Try

                If IO.File.Exists(File) Then
                    Dim hMod As IntPtr = Kernel32.LoadLibraryEx(File, IntPtr.Zero, &H2)
                    Dim hRes As IntPtr = Kernel32.FindResource(hMod, ResourceID, ResourceType)
                    Dim size As UInteger = Kernel32.SizeofResource(hMod, hRes)
                    Dim pt As IntPtr = Kernel32.LoadResource(hMod, hRes)
                    Dim bPtr As Byte() = New Byte(size - 1) {}
                    Marshal.Copy(pt, bPtr, 0, CInt(size))
                    Dim ms As New MemoryStream(bPtr)
                    Dim img As Image = Image.FromStream(ms)
                    ms.Close()
                    ms.Dispose()
                    Return img
                Else
                    Return Color.Black.ToBitmap(New Size(UnfoundW, UnfoundH))
                End If
            Catch
                Return Color.Black.ToBitmap(New Size(UnfoundW, UnfoundH))
            End Try

        End Function

#End Region

#Region "Dwmapi"
        Public Shared Sub DarkTitlebar(ByVal hWnd As IntPtr, DarkMode As Boolean)

            If Dwmapi.DwmSetWindowAttribute(hWnd, 19, If(DarkMode, 1, 0), 4) <> 0 Then Dwmapi.DwmSetWindowAttribute(hWnd, 20, If(DarkMode, 1, 0), 4)

            'Exit Sub

            'If IsWindows10OrGreater(18362) Then
            'SetProp(hWnd, "UseImmersiveDarkModeColors", New IntPtr(If(DarkMode, 1, 0)))
            'Else
            'Dim size As Integer = Marshal.SizeOf(DarkMode)
            'Dim ptr As IntPtr = Marshal.AllocHGlobal(size)
            'Marshal.StructureToPtr(DarkMode, ptr, False)
            'Dim data As WindowCompositionAttributeData = New WindowCompositionAttributeData With {
            '.Attribute = WindowCompositionAttribute.WCA_USEDARKMODECOLORS,
            '.Data = ptr,
            '.SizeOfData = size
            '}
            'SetWindowCompositionAttribute(hWnd, data)
            'End If

        End Sub
#End Region

    End Class

End Namespace
