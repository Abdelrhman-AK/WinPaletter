Imports WinPaletter.XenonCore

Public Class Win32UI
    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub

    Private Sub Win32UI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        MainFrm.Visible = False
        Location = New Point(10, (My.Computer.Screen.Bounds.Height - Height) / 2 - 20)
        loadCP(MainFrm.CP)
    End Sub


    Sub LoadCP(ByVal CP As CP)
        ApplyCPValues(CP)
    End Sub

    Sub ApplyCPValues(ByVal CP As CP)
        ActiveBorder_pick.BackColor = CP.Win32UI_ActiveBorder
        activetitle_pick.BackColor = CP.Win32UI_ActiveTitle
        AppWorkspace_pick.BackColor = CP.Win32UI_AppWorkspace
        background_pick.BackColor = CP.Win32UI_Background
        btnaltface_pick.BackColor = CP.Win32UI_ButtonAlternateFace
        btndkshadow_pick.BackColor = CP.Win32UI_ButtonDkShadow
        btnface_pick.BackColor = CP.Win32UI_ButtonFace
        btnhilight_pick.BackColor = CP.Win32UI_ButtonHilight
        btnlight_pick.BackColor = CP.Win32UI_ButtonLight
        btnshadow_pick.BackColor = CP.Win32UI_ButtonShadow
        btntext_pick.BackColor = CP.Win32UI_ButtonText
        GActivetitle_pick.BackColor = CP.Win32UI_GradientActiveTitle
        GInactivetitle_pick.BackColor = CP.Win32UI_GradientInactiveTitle
        GrayText_pick.BackColor = CP.Win32UI_GrayText
        hilighttext_pick.BackColor = CP.Win32UI_HilightText
        hottracking_pick.BackColor = CP.Win32UI_HotTrackingColor
        InactiveBorder_pick.BackColor = CP.Win32UI_InactiveBorder
        InactiveTitle_pick.BackColor = CP.Win32UI_InactiveTitle
        InactivetitleText_pick.BackColor = CP.Win32UI_InactiveTitleText
        InfoText_pick.BackColor = CP.Win32UI_InfoText
        InfoWindow_pick.BackColor = CP.Win32UI_InfoWindow
        menu_pick.BackColor = CP.Win32UI_Menu
        menubar_pick.BackColor = CP.Win32UI_MenuBar
        menutext_pick.BackColor = CP.Win32UI_MenuText
        Scrollbar_pick.BackColor = CP.Win32UI_Scrollbar
        TitleText_pick.BackColor = CP.Win32UI_TitleText
        Window_pick.BackColor = CP.Win32UI_Window
        Frame_pick.BackColor = CP.Win32UI_WindowFrame
        WindowText_pick.BackColor = CP.Win32UI_WindowText
        hilight_pick.BackColor = CP.Win32UI_Hilight
        menuhilight_pick.BackColor = CP.Win32UI_MenuHilight
        desktop_pick.BackColor = CP.Win32UI_Desktop
    End Sub

    Sub ApplyToCP(ByVal CP As CP)
        CP.Win32UI_ActiveBorder = ActiveBorder_pick.BackColor
        CP.Win32UI_ActiveTitle = activetitle_pick.BackColor
        CP.Win32UI_AppWorkspace = AppWorkspace_pick.BackColor
        CP.Win32UI_Background = background_pick.BackColor
        CP.Win32UI_ButtonAlternateFace = btnaltface_pick.BackColor
        CP.Win32UI_ButtonDkShadow = btndkshadow_pick.BackColor
        CP.Win32UI_ButtonFace = btnface_pick.BackColor
        CP.Win32UI_ButtonHilight = btnhilight_pick.BackColor
        CP.Win32UI_ButtonLight = btnlight_pick.BackColor
        CP.Win32UI_ButtonShadow = btnshadow_pick.BackColor
        CP.Win32UI_ButtonText = btntext_pick.BackColor
        CP.Win32UI_GradientActiveTitle = GActivetitle_pick.BackColor
        CP.Win32UI_GradientInactiveTitle = GInactivetitle_pick.BackColor
        CP.Win32UI_GrayText = GrayText_pick.BackColor
        CP.Win32UI_HilightText = hilighttext_pick.BackColor
        CP.Win32UI_HotTrackingColor = hottracking_pick.BackColor
        CP.Win32UI_InactiveBorder = InactiveBorder_pick.BackColor
        CP.Win32UI_InactiveTitle = InactiveTitle_pick.BackColor
        CP.Win32UI_InactiveTitleText = InactivetitleText_pick.BackColor
        CP.Win32UI_InfoText = InfoText_pick.BackColor
        CP.Win32UI_InfoWindow = InfoWindow_pick.BackColor
        CP.Win32UI_Menu = menu_pick.BackColor
        CP.Win32UI_MenuBar = menubar_pick.BackColor
        CP.Win32UI_MenuText = menutext_pick.BackColor
        CP.Win32UI_Scrollbar = Scrollbar_pick.BackColor
        CP.Win32UI_TitleText = TitleText_pick.BackColor
        CP.Win32UI_Window = Window_pick.BackColor
        CP.Win32UI_WindowFrame = Frame_pick.BackColor
        CP.Win32UI_WindowText = WindowText_pick.BackColor
        CP.Win32UI_Hilight = hilight_pick.BackColor
        CP.Win32UI_MenuHilight = menuhilight_pick.BackColor
        CP.Win32UI_Desktop = desktop_pick.BackColor
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        ApplyToCP(MainFrm.CP)
        Me.Close()
    End Sub


    Private Sub X_pick_Click(sender As Object, e As EventArgs) Handles ActiveBorder_pick.Click, activetitle_pick.Click, AppWorkspace_pick.Click, background_pick.Click,
            btnaltface_pick.Click, btndkshadow_pick.Click, btnface_pick.Click, btnhilight_pick.Click, btnlight_pick.Click, btnshadow_pick.Click, btntext_pick.Click, GActivetitle_pick.Click,
            GInactivetitle_pick.Click, GrayText_pick.Click, hilighttext_pick.Click, hottracking_pick.Click, InactiveBorder_pick.Click, InactiveTitle_pick.Click, InactivetitleText_pick.Click,
            InfoText_pick.Click, InfoWindow_pick.Click, menu_pick.Click, menubar_pick.Click, menutext_pick.Click, Scrollbar_pick.Click, TitleText_pick.Click, Window_pick.Click, Frame_pick.Click,
            WindowText_pick.Click, hilight_pick.Click, menuhilight_pick.Click, desktop_pick.Click

        Dim CList As New List(Of Control) From {sender}

        Dim C As Color = ColorPicker.Pick(CList)
        CType(sender, XenonGroupBox).BackColor = C
        CList.Clear()

    End Sub

    Private Sub AppWorkspace_pick_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub Win32UI_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        MainFrm.Visible = True
    End Sub
End Class