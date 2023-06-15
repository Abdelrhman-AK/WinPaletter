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

        <DllImport("dwmapi")> Public Shared Function DwmExtendFrameIntoClientArea(hWnd As IntPtr, ByRef pMarInset As MARGINS) As Integer
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
            TEXT_COLOR = 36
            CAPTION_COLOR = 35
            BORDER_COLOR = 34
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
        Public Shared Function SystemParametersInfo(uAction As Integer, uParam As Integer, ByRef lpvParam As NONCLIENTMETRICS, fuWinIni As SPIF) As Integer
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)>
        Public Shared Function SystemParametersInfo(uAction As Integer, uParam As Integer, ByRef lpvParam As ICONMETRICS, fuWinIni As SPIF) As Integer
        End Function

        <DllImport("user32", CharSet:=CharSet.Auto)>
        Public Shared Function SystemParametersInfo(uAction As Integer, uParam As Integer, ByRef lpvParam As ANIMATIONINFO, fuWinIni As SPIF) As Integer
        End Function

        <DllImport("user32.dll")>
        Public Shared Function GetWindowDC(ByVal hWnd As IntPtr) As IntPtr
        End Function

        <DllImport("user32.dll")>
        Public Shared Function ReleaseDC(ByVal hWnd As IntPtr, ByVal hDC As IntPtr) As Integer
        End Function

        Public Declare Function SystemParametersInfo Lib "user32" Alias "SystemParametersInfoA" (uAction As Integer, uParam As Integer, lpvParam As Integer, fuWinIni As Integer) As Integer
        Public Declare Function SystemParametersInfo Lib "user32" Alias "SystemParametersInfoA" (uAction As Integer, uParam As Integer, lpvParam As UInteger, fuWinIni As Integer) As Integer
        Public Declare Function SystemParametersInfo Lib "user32" Alias "SystemParametersInfoA" (uAction As Integer, uParam As UInteger, lpvParam As Integer, fuWinIni As Integer) As Integer
        Public Declare Function SystemParametersInfo Lib "user32" Alias "SystemParametersInfoA" (uAction As Integer, uParam As Integer, lpvParam As String, fuWinIni As Integer) As Integer
        Public Declare Function SystemParametersInfo Lib "user32" Alias "SystemParametersInfoA" (uAction As Integer, uParam As Integer, lpvParam As Boolean, fuWinIni As Integer) As Integer
        Public Declare Function SystemParametersInfo Lib "user32" Alias "SystemParametersInfoA" (uAction As Integer, uParam As Boolean, lpvParam As Integer, fuWinIni As Integer) As Integer

        Class Fixer
            ''' <summary>
            ''' It is used outside global user32 to fix issue of not remembring settings
            ''' </summary>
            Public Declare Function SystemParametersInfo Lib "user32" Alias "SystemParametersInfoA" (uAction As Integer, uParam As Integer, ByRef lpvParam As Boolean, fuWinIni As Integer) As Integer
            Public Declare Function SystemParametersInfo Lib "user32" Alias "SystemParametersInfoA" (uAction As Integer, uParam As Integer, ByRef lpvParam As Integer, fuWinIni As Integer) As Integer
            Public Declare Function SystemParametersInfo Lib "user32" Alias "SystemParametersInfoA" (uAction As Integer, uParam As Integer, ByRef lpvParam As UInteger, fuWinIni As Integer) As Integer

        End Class

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

                ''' <summary>
                ''' <b>Determines whether the Mouse Trails feature is enabled. This feature improves the visibility of mouse cursor movements by briefly showing a trail of cursors and quickly erasing them.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to an integer variable that receives a value.
                ''' <br></br> • If the value is zero or 1, the feature is disabled. If the value Is greater than 1, the feature Is enabled And the value indicates the number of cursors drawn in the trail.
                ''' <br></br> • The uiParam parameter is not used.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows 2000/NT:  This value is not supported.</i>
                ''' </summary>
                GETMOUSETRAILS = &H5E

                ''' <summary>
                ''' <b>Enables or disables the Mouse Trails feature, which improves the visibility of mouse cursor movements by briefly showing a trail of cursors and quickly erasing them.</b>
                ''' <br></br>
                ''' <br></br> • To disable the feature, set the uiParam parameter to zero or 1.
                ''' <br></br> • To enable the feature, set uiParam to a value greater than 1 to indicate the number of cursors drawn in the trail.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows 2000/NT:  This value is not supported.</i>
                ''' </summary>
                SETMOUSETRAILS = &H5D

                ''' <summary>
                ''' <b>Retrieves the state of the Mouse Sonar feature.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if enabled or FALSE otherwise.
                ''' <br></br> • For more information, see About Mouse Input on MSDN.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows 2000/NT, Windows 98/95:  This value is not supported.</i>
                ''' </summary>
                GETMOUSESONAR = &H101C

                ''' <summary>
                ''' <b>Turns the Sonar accessibility feature on or off. This feature briefly shows several concentric circles around the mouse pointer when the user presses And releases the CTRL key.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter specifies TRUE for on and FALSE for off. The default is off.
                ''' <br></br> • For more information, see About Mouse Input on MSDN.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows 2000/NT, Windows 98/95:  This value is not supported.</i>
                ''' </summary>
                SETMOUSESONAR = &H101D

                ''' <summary>
                ''' <b>Determines whether the snap-to-default-button feature is enabled. If enabled, the mouse cursor automatically moves to the default button, such as OK Or Apply, of a dialog box.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if the feature Is on, Or FALSE if it Is off.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows 95:  Not supported.</i>
                ''' </summary>
                GETSNAPTODEFBUTTON = &H5F

                ''' <summary>
                ''' <b>Enables or disables the snap-to-default-button feature. If enabled, the mouse cursor automatically moves to the default button, such as OK Or Apply, of a dialog box.</b>
                ''' <br></br>
                ''' <br></br> • Set the uiParam parameter to TRUE to enable the feature, or FALSE to disable it.
                ''' <br></br> • Applications should use the ShowWindow function when displaying a dialog box so the dialog manager can position the mouse cursor.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows 95:  Not supported.</i>
                ''' </summary>
                SETSNAPTODEFBUTTON = &H60
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
                ''' <b>Retrieves the time, in milliseconds, that the system waits before displaying a shortcut menu when the mouse cursor is over a submenu item.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to a DWORD variable that receives the time of the delay.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows 95:  Not supported.</i>
                ''' </summary>
                GETMENUSHOWDELAY = &H6A

                ''' <summary>
                ''' <b>Sets the time, in milliseconds, that the system waits before displaying a shortcut menu when the mouse cursor is over a submenu item.</b>
                ''' <br></br>
                ''' <br></br> • The uiParam parameter must point to a DWORD variable that sets the time of the delay.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows 95:  Not supported.</i>
                ''' </summary>
                SETMENUSHOWDELAY = &H6B

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
                ''' <br></br> • Set pvParam to TRUE to enable flat menu appearance or FALSE to disable it.
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

                ''' <summary>
                ''' <b>Sets dragging of full windows either on or off.</b>
                ''' <br></br>
                ''' <br></br> • The uiParam parameter specifies TRUE for on, or FALSE for off.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows 95:  This flag is supported only if Windows Plus! is installed. See SPI_GETWINDOWSEXTENSION.</i>
                ''' </summary>
                SETDRAGFULLWINDOWS = &H25

                ''' <summary>
                ''' <b>Determines whether dragging of full windows is enabled.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if enabled, Or FALSE otherwise.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows 95:  This flag is supported only if Windows Plus! is installed. See SPI_GETWINDOWSEXTENSION.</i>
                ''' </summary>
                GETDRAGFULLWINDOWS = &H26

                ''' <summary>
                ''' <b>Determines whether menu access keys are always underlined.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if menu access keys are always underlined, And FALSE if they are underlined only when the menu Is activated by the keyboard.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                ''' </summary>
                GETMENUUNDERLINES = &H100A

                ''' <summary>
                ''' <b>Determines whether menu access keys are always underlined.</b>
                ''' <br></br>
                ''' <br></br> • Set pvParam to TRUE to always underline menu access keys, Or FALSE to underline menu access keys only when the menu Is activated from the keyboard.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                ''' </summary>
                SETMENUUNDERLINES = &H100B

                ''' <summary>
                ''' <b>Determines whether active window tracking (activating the window the mouse is on) is on or off.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE for on, Or FALSE for off.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                ''' </summary>
                GETACTIVEWINDOWTRACKING = &H1000

                ''' <summary>
                ''' <b>Sets active window tracking (activating the window the mouse is on) either on or off.</b>
                ''' <br></br>
                ''' <br></br> • Set pvParam to TRUE for on or FALSE for off.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                ''' </summary>
                SETACTIVEWINDOWTRACKING = &H1001

                ''' <summary>
                ''' <b>Retrieves the active window tracking delay, in milliseconds.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to a DWORD variable that receives the time.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                ''' </summary>
                GETACTIVEWNDTRKTIMEOUT = &H2002

                ''' <summary>
                ''' <b>Sets the active window tracking delay.</b>
                ''' <br></br>
                ''' <br></br> • Set pvParam to the number of milliseconds to delay before activating the window under the mouse pointer.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows 95: This value is not supported.</i>
                ''' </summary>
                SETACTIVEWNDTRKTIMEOUT = &H2003

                ''' <summary>
                ''' <b>Determines whether or not windows activated through active window tracking should be brought to the top.</b>
                ''' <br></br>
                ''' <br></br> • Set pvParam to TRUE for on Or FALSE for off.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                ''' </summary>
                GETACTIVEWNDTRKZORDER = &H100C

                ''' <summary>
                ''' <b>Sets if windows activated through active window tracking should be brought to the top.</b>
                ''' <br></br>
                ''' <br></br> • Set pvParam to TRUE for on Or FALSE for off.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                ''' </summary>
                SETACTIVEWNDTRKZORDER = &H100D

                ''' <summary>
                ''' <b>Retrieves the caret width in edit controls, in pixels.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to a DWORD that receives this value.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                ''' </summary>
                GETCARETWIDTH = &H2006

                ''' <summary>
                ''' <b>Sets the caret width in edit controls.</b>
                ''' <br></br>
                ''' <br></br> • Set pvParam to the desired width, in pixels.
                ''' <br></br> • The default and minimum value is 1.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows Me/98/95:  This value is not supported.</i>
                ''' </summary>
                SETCARETWIDTH = &H2007
            End Enum

            Enum FocusRect
                ''' <summary>
                ''' <b>Retrieves the width, in pixels, of the left and right edges of the focus rectangle drawn with DrawFocusRect.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to a UINT.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows 2000/NT, Windows Me/98/95:  This value is not supported.</i>
                ''' </summary>
                GETFOCUSBORDERWIDTH = &H200E

                ''' <summary>
                ''' <b>Sets the height of the left and right edges of the focus rectangle drawn with DrawFocusRect</b>
                ''' <br></br>
                ''' <br></br> • Set the pvParam parameter.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows 2000/NT, Windows Me/98/95:  This value is not supported.</i>
                ''' </summary>
                SETFOCUSBORDERWIDTH = &H200F

                ''' <summary>
                ''' <b>Retrieves the height, in pixels, of the top and bottom edges of the focus rectangle drawn with DrawFocusRect.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to a UINT.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                ''' </summary>
                GETFOCUSBORDERHEIGHT = &H2010

                ''' <summary>
                ''' <b>Sets the height of the top and bottom edges of the focus rectangle drawn with DrawFocusRect to the value of the pvParam parameter.</b>
                ''' <br></br>
                ''' <br></br> • Set the pvParam parameter.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows NT, Windows 95:  This value is not supported.</i>
                ''' </summary>
                SETFOCUSBORDERHEIGHT = &H2011
            End Enum

            Enum Fonts
                ''' <summary>
                ''' <b>Determines whether the font smoothing feature is enabled. This feature uses font antialiasing to make font curves appear smoother by painting pixels at different gray levels.</b>
                ''' <br></br>
                ''' <br></br> • The pvParam parameter must point to a BOOL variable that receives TRUE if the feature is enabled, or FALSE if it is not.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows 95:  This flag is supported only if Windows Plus! is installed. See SPI_GETWINDOWSEXTENSION.</i>
                ''' </summary>
                GETFONTSMOOTHING = &H4A

                ''' <summary>
                ''' <b>Enables or disables the font smoothing feature, which uses font antialiasing to make font curves appear smoother by painting pixels at different gray levels.</b>
                ''' <br></br>
                ''' <br></br> • To enable the feature, set the uiParam parameter to TRUE. To disable the feature, set uiParam to FALSE.
                ''' <br></br>
                ''' <br></br> <i>(!) Windows 95: This flag is supported only if Windows Plus! is installed. See SPI_GETWINDOWSEXTENSION.</i>
                ''' </summary>
                SETFONTSMOOTHING = &H4B
            End Enum
        End Class

        <Flags>
        Enum SPIF
            None = &H0

            ''' <summary>
            ''' Writes the new system-wide parameter setting to the user profile.
            ''' </summary>
            UpdateINIFile = &H1

            ''' <summary>
            ''' Broadcasts the WM_SETTINGCHANGE message after updating the user profile, but it is temporary until you logoff.
            ''' </summary>
            SendChange = &H2

            ''' <summary>
            ''' Same as SENDCHANGE
            ''' <br></br> Broadcasts the WM_SETTINGCHANGE message after updating the user profile, but it is temporary until you logoff.
            ''' </summary>
            SendWinINIChange = SendChange
        End Enum

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

        <DllImport("kernel32.dll", SetLastError:=True)>
        Public Shared Function FreeLibrary(ByVal hModule As IntPtr) As Boolean
        End Function

        <DllImport("User32.dll")>
        Public Shared Function LoadImage(ByVal hInstance As IntPtr, ByVal uID As Integer, ByVal type As UInteger, ByVal width As Integer, ByVal height As Integer, ByVal load As Integer) As IntPtr
        End Function

        <DllImport("User32.dll")>
        Public Shared Function LoadBitmap(ByVal hInstance As IntPtr, ByVal uID As Integer) As IntPtr
        End Function

        <DllImport("kernel32.dll")>
        Public Shared Function FindResource(ByVal hModule As IntPtr, ByVal lpName As String, ByVal lpType As String) As IntPtr
        End Function

        <DllImport("kernel32.dll")>
        Public Shared Function FindResource(ByVal hModule As IntPtr, ByVal iResID As Integer, ByVal lpType As String) As IntPtr
        End Function

        <DllImport("kernel32.dll")>
        Public Shared Function FindResource(ByVal hModule As IntPtr, ByVal lpName As String, ByVal iType As Integer) As IntPtr
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)>
        Public Shared Function LoadResource(ByVal hModule As IntPtr, ByVal hResInfo As IntPtr) As IntPtr
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)>
        Public Shared Function SizeofResource(ByVal hModule As IntPtr, ByVal hResInfo As IntPtr) As UInteger
        End Function

        <DllImport("KERNEL32.DLL", CallingConvention:=CallingConvention.StdCall, CharSet:=CharSet.Unicode, EntryPoint:="BeginUpdateResourceW", ExactSpelling:=True, SetLastError:=True)>
        Public Shared Function BeginUpdateResource(ByVal pFileName As String, ByVal bDeleteExistingResources As Boolean) As IntPtr
        End Function

        <DllImport("KERNEL32.DLL", CallingConvention:=CallingConvention.StdCall, CharSet:=CharSet.Unicode, EntryPoint:="UpdateResourceW", ExactSpelling:=True, SetLastError:=True)>
        Public Shared Function UpdateResource(ByVal hUpdate As IntPtr, ByVal pType As UInteger, ByVal pName As String, ByVal wLanguage As UShort, ByVal pData As Byte(), ByVal cbData As UInteger) As Boolean
        End Function

        <DllImport("KERNEL32.DLL", CallingConvention:=CallingConvention.StdCall, CharSet:=CharSet.Unicode, EntryPoint:="UpdateResourceW", ExactSpelling:=True, SetLastError:=True)>
        Public Shared Function UpdateResource(ByVal hUpdate As IntPtr, ByVal pType As UInteger, ByVal iResID As Integer, ByVal wLanguage As UShort, ByVal pData As Byte(), ByVal cbData As UInteger) As Boolean
        End Function

        <DllImport("KERNEL32.DLL", CallingConvention:=CallingConvention.StdCall, CharSet:=CharSet.Unicode, EntryPoint:="UpdateResourceW", ExactSpelling:=True, SetLastError:=True)>
        Public Shared Function UpdateResource(ByVal hUpdate As IntPtr, ByVal lpType As String, ByVal iResID As Integer, ByVal wLanguage As UShort, ByVal pData As Byte(), ByVal cbData As UInteger) As Boolean
        End Function

        <DllImport("KERNEL32.DLL", CallingConvention:=CallingConvention.StdCall, CharSet:=CharSet.Unicode, EntryPoint:="EndUpdateResourceW", ExactSpelling:=True, SetLastError:=True)>
        Public Shared Function EndUpdateResource(ByVal hUpdate As IntPtr, ByVal bDiscard As Boolean) As Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)>
        Public Shared Function Wow64DisableWow64FsRedirection(ByRef ptr As IntPtr) As Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)>
        Public Shared Function Wow64RevertWow64FsRedirection(ByVal ptr As IntPtr) As Boolean
        End Function

        <DllImport("kernel32")>
        Public Shared Function WritePrivateProfileString(ByVal section As String, ByVal key As String, ByVal val As String, ByVal filePath As String) As Long
        End Function
        <DllImport("kernel32")>
        Public Shared Function GetPrivateProfileString(ByVal section As String, ByVal key As String, ByVal def As String, ByVal retVal As StringBuilder, ByVal size As Integer, ByVal filePath As String) As Integer
        End Function

    End Class

    Public Class UxTheme
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

        <DllImport("uxtheme.dll", CharSet:=CharSet.Unicode, SetLastError:=True)>
        Public Shared Function DrawThemeTextEx(ByVal hTheme As IntPtr, ByVal hdc As IntPtr, ByVal iPartId As Integer, ByVal iStateId As Integer, ByVal text As String, ByVal iCharCount As Integer, ByVal dwFlags As Integer, ByRef pRect As Rect, ByRef pOptions As DttOpts) As Integer
        End Function

        ''' <summary>
        ''' Set The Window's Theme Attributes
        ''' </summary>
        ''' <returns>If The Call Was Successful or Not</returns>
        <DllImport("UxTheme.dll")>
        Public Shared Function SetWindowThemeAttribute(ByVal hWnd As IntPtr, ByVal wtype As WindowThemeAttributeType, ByRef attributes As WTA_OPTIONS, ByVal size As UInteger) As Integer
        End Function

        ''' <summary>
        ''' Do Not Draw The Caption (Text)
        ''' </summary>
        Public Shared WTNCA_NODRAWCAPTION As UInteger = &H1
        ''' <summary>
        ''' Do Not Draw the Icon
        ''' </summary>
        Public Shared WTNCA_NODRAWICON As UInteger = &H2
        ''' <summary>
        ''' Do Not Show the System Menu
        ''' </summary>
        Public Shared WTNCA_NOSYSMENU As UInteger = &H4
        ''' <summary>
        ''' Do Not Mirror the Question mark Symbol
        ''' </summary>
        Public Shared WTNCA_NOMIRRORHELP As UInteger = &H8

        ''' <summary>
        ''' The Options of What Attributes to Add/Remove
        ''' </summary>
        <StructLayout(LayoutKind.Sequential)>
        Public Structure WTA_OPTIONS
            Public Flags As UInteger
            Public Mask As UInteger
        End Structure

        <StructLayout(LayoutKind.Sequential)>
        Structure DttOpts
            Public dwSize As Integer
            Public dwFlags As DttOptsFlags
            Public crText As Integer
            Public crBorder As Integer
            Public crShadow As Integer
            Public iTextShadowType As Integer
            Public ptShadowOffset As Point
            Public iBorderSize As Integer
            Public iFontPropId As Integer
            Public iColorPropId As Integer
            Public iStateId As Integer
            Public fApplyOverlay As Boolean
            Public iGlowSize As Integer
            Public pfnDrawTextCallback As Integer
            Public lParam As IntPtr
        End Structure

        <Flags>
        Enum DttOptsFlags As Integer
            DTT_TEXTCOLOR = 1
            DTT_BORDERCOLOR = 2
            DTT_SHADOWCOLOR = 4
            DTT_SHADOWTYPE = 8
            DTT_SHADOWOFFSET = 16
            DTT_BORDERSIZE = 32
            'DTT_FONTPROP = 64,		commented values are currently unused
            'DTT_COLORPROP = 128,
            'DTT_STATEID = 256,
            DTT_CALCRECT = 512
            DTT_APPLYOVERLAY = 1024
            DTT_GLOWSIZE = 2048
            'DTT_CALLBACK = 4096,
            DTT_COMPOSITED = 8192
        End Enum

        <StructLayout(LayoutKind.Sequential)>
        Structure Rect
            Public Sub New(ByVal left As Integer, ByVal top As Integer, ByVal right As Integer, ByVal bottom As Integer)
                Me.Left = left
                Me.Top = top
                Me.Right = right
                Me.Bottom = bottom
            End Sub

            Public Sub New(ByVal rect As Drawing.Rectangle)
                Left = rect.X
                Top = rect.Y
                Right = rect.Right
                Bottom = rect.Bottom
            End Sub

            Public Left As Integer
            Public Top As Integer
            Public Right As Integer
            Public Bottom As Integer

            Public Property Width As Integer
                Get
                    Return Right - Left
                End Get
                Set(ByVal value As Integer)
                    Right = Left + value
                End Set
            End Property

            Public Property Height As Integer
                Get
                    Return Bottom - Top
                End Get
                Set(ByVal value As Integer)
                    Bottom = Top + value
                End Set
            End Property

            Public Function ToRectangle() As Drawing.Rectangle
                Return New Drawing.Rectangle(Left, Top, Right - Left, Bottom - Top)
            End Function

        End Structure

        ''' <summary>
        ''' What Type of Attributes? (Only One is Currently Defined)
        ''' </summary>
        Public Enum WindowThemeAttributeType
            WTA_NONCLIENT = 1
        End Enum
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
        <DllImport("gdi32.dll")>
        Public Shared Function AddFontMemResourceEx(ByVal pbFont As IntPtr, ByVal cbFont As UInteger, ByVal pdv As IntPtr, <[In]> ByRef pcFonts As UInteger) As IntPtr
        End Function

        <DllImport("gdi32.dll", CharSet:=CharSet.Auto, SetLastError:=True, ExactSpelling:=True)>
        Public Shared Function GetDeviceCaps(ByVal hDC As IntPtr, ByVal nIndex As Integer) As Integer
        End Function

        <DllImport("gdi32.dll")>
        Public Shared Function CreateCompatibleDC(ByVal hDC As IntPtr) As IntPtr
        End Function

        <DllImport("gdi32.dll", SetLastError:=True)>
        Public Shared Function DeleteDC(ByVal hdc As IntPtr) As Boolean
        End Function

        <DllImport("gdi32.dll")>
        Public Shared Function BitBlt(ByVal hdc As IntPtr, ByVal nXDest As Integer, ByVal nYDest As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hdcSrc As IntPtr, ByVal nXSrc As Integer, ByVal nYSrc As Integer, ByVal dwRop As BitBltOp) As Boolean
        End Function

        <DllImport("gdi32.dll")>
        Public Shared Function CreateDIBSection(ByVal hdc As IntPtr, ByRef pbmi As BitmapInfo, ByVal iUsage As UInteger, ByVal ppvBits As Integer, ByVal hSection As IntPtr, ByVal dwOffset As UInteger) As IntPtr
        End Function

        <DllImport("gdi32.dll", ExactSpelling:=True)>
        Public Shared Function SelectObject(ByVal hDC As IntPtr, ByVal hObject As IntPtr) As IntPtr
        End Function

        <DllImport("gdi32.dll", ExactSpelling:=True)>
        Public Shared Function DeleteObject(ByVal hObject As IntPtr) As Boolean
        End Function

        Public Enum DeviceCap
            VERTRES = 10
            DESKTOPVERTRES = 117
        End Enum

        <StructLayout(LayoutKind.Sequential)>
        Structure BitmapInfo
            Public biSize As Integer
            Public biWidth As Integer
            Public biHeight As Integer
            Public biPlanes As Short
            Public biBitCount As Short
            Public biCompression As Integer
            Public biSizeImage As Integer
            Public biXPelsPerMeter As Integer
            Public biYPelsPerMeter As Integer
            Public biClrUsed As Integer
            Public biClrImportant As Integer
            Public bmiColors_rgbBlue As Byte
            Public bmiColors_rgbGreen As Byte
            Public bmiColors_rgbRed As Byte
            Public bmiColors_rgbReserved As Byte
        End Structure

        Enum BitBltOp As UInteger
            SRCCOPY = &HCC0020   ' dest = source                   
            SRCPAINT = &HEE0086   ' dest = source OR dest           
            SRCAND = &H8800C6   ' dest = source AND dest          
            SRCINVERT = &H660046   ' dest = source XOR dest          
            SRCERASE = &H440328   ' dest = source AND (NOT dest )   
            NOTSRCCOPY = &H330008   ' dest = (NOT source)             
            NOTSRCERASE = &H1100A6   ' dest = (NOT src) AND (NOT dest) 
            MERGECOPY = &HC000CA   ' dest = (source AND pattern)     
            MERGEPAINT = &HBB0226   ' dest = (NOT source) OR dest     
            PATCOPY = &HF00021   ' dest = pattern                  
            PATPAINT = &HFB0A09   ' dest = DPSnoo                   
            PATINVERT = &H5A0049   ' dest = pattern XOR dest         
            DSTINVERT = &H550009   ' dest = (NOT dest)               
            BLACKNESS = &H42   ' dest = BLACK                    
            WHITENESS = &HFF0062   ' dest = WHITE                    

            NOMIRRORBITMAP = &H80000000UI ' Do not Mirror the bitmap in this call 
            CAPTUREBLT = &H40000000      ' Include layered windows 
        End Enum
    End Class

    Public Class Wininet
        <DllImport("wininet.dll")>
        Private Shared Function InternetGetConnectedState(<Out> ByRef Description As Integer, ByVal ReservedValue As Integer) As Boolean
        End Function

        Public Shared Function CheckNet() As Boolean
            Dim desc As Integer
            Return InternetGetConnectedState(desc, 0)
        End Function
    End Class

    Public Class Winmm
        <DllImport("winmm.dll")>
        Public Shared Function mciSendString(ByVal command As String, ByVal buffer As StringBuilder, ByVal bufferSize As Int32, ByVal hwndCallback As IntPtr) As Int32
        End Function
    End Class

    Public Class Dnsapi
        <DllImport("dnsapi.dll", EntryPoint:="DnsFlushResolverCache")>
        Public Shared Function DnsFlushResolverCache() As UInt32
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

                'if the large and/or small icons where created in the unmanaged memory successfully then create
                'a clone of them in the managed icons and delete the icons in the unmanaged memory.

                If hSmlIcon <> IntPtr.Zero Then
                    ico = CType(Icon.FromHandle(hSmlIcon).Clone, Icon)
                    User32.DestroyIcon(hSmlIcon)
                End If

            End If

            Return ico
        End Function
        Public Shared Function GetSystemIcon(_Icon As Shell32.SHSTOCKICONID, _Type As Shell32.SHGSI) As Icon
            Try
                Dim sii As New Shell32.SHSTOCKICONINFO With {.cbSize = Marshal.SizeOf(GetType(Shell32.SHSTOCKICONINFO))}
                Shell32.SHGetStockIconInfo(_Icon, _Type, sii)
                If sii.hIcon <> Nothing AndAlso sii.hIcon <> IntPtr.Zero Then
                    Return Icon.FromHandle(sii.hIcon)
                Else
                    Return Nothing
                End If
            Catch
                Return Nothing
            End Try
        End Function

#End Region

#Region "Dwmapi"
        Public Shared Sub DarkTitlebar(ByVal hWnd As IntPtr, DarkMode As Boolean)
            Dwmapi.DwmSetWindowAttribute(hWnd, 20, If(DarkMode, 1, 0), Marshal.SizeOf(Of Integer))
            Dwmapi.DwmSetWindowAttribute(hWnd, 19, If(DarkMode, 1, 0), Marshal.SizeOf(Of Integer))
        End Sub
#End Region

#Region "UxTheme"
        Public Shared Sub RemoveFormTitlebarTextAndIcon(Handle As IntPtr)
            Dim ops As New UxTheme.WTA_OPTIONS With {
                .Flags = UxTheme.WTNCA_NODRAWCAPTION Or UxTheme.WTNCA_NODRAWICON,
                .Mask = UxTheme.WTNCA_NODRAWCAPTION Or UxTheme.WTNCA_NODRAWICON
            }

            UxTheme.SetWindowThemeAttribute(Handle, UxTheme.WindowThemeAttributeType.WTA_NONCLIENT, ops, Marshal.SizeOf(ops))
        End Sub

#End Region

#Region "Winmm"
        Shared Sub PlayAudio(File As String)
            If IO.File.Exists(File) Then
                Winmm.mciSendString("close myWAV", Nothing, 0, 0)
                Winmm.mciSendString("open """ & File & """ type mpegvideo alias myWAV", Nothing, 0, 0)
                Winmm.mciSendString("play myWAV", Nothing, 0, 0)
                Dim Volume As Integer = 1000 ' Sets it to use entire range of volume control
                Winmm.mciSendString("setaudio myWAV volume to " & Volume.ToString, Nothing, 0, 0)
            End If
        End Sub

        Shared Sub StopAudio()
            Winmm.mciSendString("seek myWAV to start", Nothing, 0, IntPtr.Zero)
            Winmm.mciSendString("stop myWAV", Nothing, 0, IntPtr.Zero)
        End Sub
#End Region

    End Class

End Namespace
