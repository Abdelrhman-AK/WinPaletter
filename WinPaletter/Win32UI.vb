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


    Sub LoadCP(ByVal [CP] As CP)
        ApplyCPValues([CP])
    End Sub

    Sub ApplyCPValues(ByVal [CP] As CP)
        ActiveBorder_pick.BackColor = [CP].Win32UI_ActiveBorder
        activetitle_pick.BackColor = [CP].Win32UI_ActiveTitle
        AppWorkspace_pick.BackColor = [CP].Win32UI_AppWorkspace
        background_pick.BackColor = [CP].Win32UI_Background
        btnaltface_pick.BackColor = [CP].Win32UI_ButtonAlternateFace
        btndkshadow_pick.BackColor = [CP].Win32UI_ButtonDkShadow
        btnface_pick.BackColor = [CP].Win32UI_ButtonFace
        btnhilight_pick.BackColor = [CP].Win32UI_ButtonHilight
        btnlight_pick.BackColor = [CP].Win32UI_ButtonLight
        btnshadow_pick.BackColor = [CP].Win32UI_ButtonShadow
        btntext_pick.BackColor = [CP].Win32UI_ButtonText
        GActivetitle_pick.BackColor = [CP].Win32UI_GradientActiveTitle
        GInactivetitle_pick.BackColor = [CP].Win32UI_GradientInactiveTitle
        GrayText_pick.BackColor = [CP].Win32UI_GrayText
        hilighttext_pick.BackColor = [CP].Win32UI_HilightText
        hottracking_pick.BackColor = [CP].Win32UI_HotTrackingColor
        InactiveBorder_pick.BackColor = [CP].Win32UI_InactiveBorder
        InactiveTitle_pick.BackColor = [CP].Win32UI_InactiveTitle
        InactivetitleText_pick.BackColor = [CP].Win32UI_InactiveTitleText
        InfoText_pick.BackColor = [CP].Win32UI_InfoText
        InfoWindow_pick.BackColor = [CP].Win32UI_InfoWindow
        menu_pick.BackColor = [CP].Win32UI_Menu
        menubar_pick.BackColor = [CP].Win32UI_MenuBar
        menutext_pick.BackColor = [CP].Win32UI_MenuText
        Scrollbar_pick.BackColor = [CP].Win32UI_Scrollbar
        TitleText_pick.BackColor = [CP].Win32UI_TitleText
        Window_pick.BackColor = [CP].Win32UI_Window
        Frame_pick.BackColor = [CP].Win32UI_WindowFrame
        WindowText_pick.BackColor = [CP].Win32UI_WindowText
        hilight_pick.BackColor = [CP].Win32UI_Hilight
        menuhilight_pick.BackColor = [CP].Win32UI_MenuHilight
        desktop_pick.BackColor = [CP].Win32UI_Desktop
    End Sub

    Sub ApplyToCP(ByVal [CP] As CP)
        [CP].Win32UI_ActiveBorder = ActiveBorder_pick.BackColor
        [CP].Win32UI_ActiveTitle = activetitle_pick.BackColor
        [CP].Win32UI_AppWorkspace = AppWorkspace_pick.BackColor
        [CP].Win32UI_Background = background_pick.BackColor
        [CP].Win32UI_ButtonAlternateFace = btnaltface_pick.BackColor
        [CP].Win32UI_ButtonDkShadow = btndkshadow_pick.BackColor
        [CP].Win32UI_ButtonFace = btnface_pick.BackColor
        [CP].Win32UI_ButtonHilight = btnhilight_pick.BackColor
        [CP].Win32UI_ButtonLight = btnlight_pick.BackColor
        [CP].Win32UI_ButtonShadow = btnshadow_pick.BackColor
        [CP].Win32UI_ButtonText = btntext_pick.BackColor
        [CP].Win32UI_GradientActiveTitle = GActivetitle_pick.BackColor
        [CP].Win32UI_GradientInactiveTitle = GInactivetitle_pick.BackColor
        [CP].Win32UI_GrayText = GrayText_pick.BackColor
        [CP].Win32UI_HilightText = hilighttext_pick.BackColor
        [CP].Win32UI_HotTrackingColor = hottracking_pick.BackColor
        [CP].Win32UI_InactiveBorder = InactiveBorder_pick.BackColor
        [CP].Win32UI_InactiveTitle = InactiveTitle_pick.BackColor
        [CP].Win32UI_InactiveTitleText = InactivetitleText_pick.BackColor
        [CP].Win32UI_InfoText = InfoText_pick.BackColor
        [CP].Win32UI_InfoWindow = InfoWindow_pick.BackColor
        [CP].Win32UI_Menu = menu_pick.BackColor
        [CP].Win32UI_MenuBar = menubar_pick.BackColor
        [CP].Win32UI_MenuText = menutext_pick.BackColor
        [CP].Win32UI_Scrollbar = Scrollbar_pick.BackColor
        [CP].Win32UI_TitleText = TitleText_pick.BackColor
        [CP].Win32UI_Window = Window_pick.BackColor
        [CP].Win32UI_WindowFrame = Frame_pick.BackColor
        [CP].Win32UI_WindowText = WindowText_pick.BackColor
        [CP].Win32UI_Hilight = hilight_pick.BackColor
        [CP].Win32UI_MenuHilight = menuhilight_pick.BackColor
        [CP].Win32UI_Desktop = desktop_pick.BackColor
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

        Dim C As Color

        Select Case sender.Name
            Case "activetitle_pick"
                CList.Add(RetroWindow2)
                CList.Add(RetroWindow3)
                CList.Add(RetroWindow4)

                Dim _Conditions As New Conditions With {.RetroWindowColor1 = True}
                C = ColorPicker.Pick(CList, _Conditions)
                RetroWindow2.Color1 = C
                RetroWindow3.Color1 = C
                RetroWindow4.Color1 = C

            Case "GActivetitle_pick"
                CList.Add(RetroWindow2)
                CList.Add(RetroWindow3)
                CList.Add(RetroWindow4)

                Dim _Conditions As New Conditions With {.RetroWindowColor2 = True}
                C = ColorPicker.Pick(CList, _Conditions)
                RetroWindow2.Color2 = C
                RetroWindow3.Color2 = C
                RetroWindow4.Color2 = C

            Case "TitleText_pick"
                CList.Add(RetroWindow2)
                CList.Add(RetroWindow3)
                CList.Add(RetroWindow4)

                Dim _Conditions As New Conditions With {.RetroWindowForeColor = True}
                C = ColorPicker.Pick(CList, _Conditions)
                RetroWindow2.ForeColor = C
                RetroWindow3.ForeColor = C
                RetroWindow4.ForeColor = C

            Case "InactiveTitle_pick"
                CList.Add(RetroWindow1)
                Dim _Conditions As New Conditions With {.RetroWindowColor1 = True}
                C = ColorPicker.Pick(CList, _Conditions)
                RetroWindow1.Color1 = C
            Case "GInactivetitle_pick"
                CList.Add(RetroWindow1)
                Dim _Conditions As New Conditions With {.RetroWindowColor2 = True}
                C = ColorPicker.Pick(CList, _Conditions)
                RetroWindow1.Color2 = C
            Case "InactivetitleText_pick"
                CList.Add(RetroWindow1)
                Dim _Conditions As New Conditions With {.RetroWindowForeColor = True}
                C = ColorPicker.Pick(CList, _Conditions)
                RetroWindow1.ForeColor = C
            Case "ActiveBorder_pick"
                CList.Add(RetroWindow2)
                CList.Add(RetroWindow3)
                CList.Add(RetroWindow4)

                Dim _Conditions As New Conditions With {.RetroWindowBorder = True}
                C = ColorPicker.Pick(CList, _Conditions)
                RetroWindow2.ColorBorder = C
                RetroWindow3.ColorBorder = C
                RetroWindow4.ColorBorder = C

            Case "InactiveBorder_pick"
                CList.Add(RetroWindow1)
                Dim _Conditions As New Conditions With {.RetroWindowBorder = True}
                C = ColorPicker.Pick(CList, _Conditions)
                RetroWindow1.ColorBorder = C

            Case "Frame_pick"
                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        CList.Add(RB)
                    Next
                Next

                Dim _Conditions As New Conditions With {.RetroWindowFrame = True}
                C = ColorPicker.Pick(CList, _Conditions)

                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        RB.WindowFrame = C
                    Next
                Next

            Case "btndkshadow_pick"
                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    CList.Add(RW)
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        CList.Add(RB)
                    Next
                Next

                Dim _Conditions As New Conditions With {.RetroButtonDkShadow = True}
                C = ColorPicker.Pick(CList, _Conditions)

                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    RW.ButtonDkShadow = C
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        RB.ButtonDkShadow = C
                    Next
                Next

            Case "btnhilight_pick"
                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    CList.Add(RW)
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        CList.Add(RB)
                    Next
                Next

                Dim _Conditions As New Conditions With {.RetroButtonHilight = True}
                C = ColorPicker.Pick(CList, _Conditions)

                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    RW.ButtonHilight = C
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        RB.ButtonHilight = C
                    Next
                Next

            Case "btnlight_pick"
                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    CList.Add(RW)
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        CList.Add(RB)
                    Next
                Next

                Dim _Conditions As New Conditions With {.RetroButtonLight = True}
                C = ColorPicker.Pick(CList, _Conditions)

                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    RW.ButtonLight = C
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        RB.ButtonLight = C
                    Next
                Next

            Case "btnshadow_pick"
                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    CList.Add(RW)
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        CList.Add(RB)
                    Next
                Next

                Dim _Conditions As New Conditions With {.RetroButtonShadow = True}
                C = ColorPicker.Pick(CList, _Conditions)

                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    RW.ButtonShadow = C
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        RB.ButtonShadow = C
                    Next
                Next

            Case "btntext_pick"
                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        CList.Add(RB)
                    Next
                Next

                Dim _Conditions As New Conditions With {.RetroButtonText = True}
                C = ColorPicker.Pick(CList, _Conditions)

                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        RB.ForeColor = C
                    Next
                Next

            Case "AppWorkspace_pick"
                CList.Add(Panel2)
                Dim _Conditions As New Conditions With {.RetroAppWorkspace = True}
                C = ColorPicker.Pick(CList, _Conditions)
                Panel2.BackColor = C

            Case "background_pick"
                CList.Add(pnl_preview)
                Dim _Conditions As New Conditions With {.RetroBackground = True}
                C = ColorPicker.Pick(CList, _Conditions)
                pnl_preview.BackColor = C

            Case "menu_pick"
                CList.Add(Menu)
                CList.Add(Panel3)
                CList.Add(RetroPanel1)
                CList.Add(RetroSeparatorH1)
                CList.Add(RetroSeparatorH2)
                CList.Add(RetroSeparatorH3)
                CList.Add(RetroSeparatorH4)
                CList.Add(RetroSeparatorH5)

                Dim _Conditions As New Conditions With {.RetroBackground = True}
                C = ColorPicker.Pick(CList, _Conditions)
                Menu.BackColor = C
                Panel3.BackColor = C
                RetroPanel1.BackColor = C
                RetroSeparatorH1.BackColor = C
                RetroSeparatorH2.BackColor = C
                RetroSeparatorH3.BackColor = C
                RetroSeparatorH4.BackColor = C
                RetroSeparatorH5.BackColor = C

            Case "menuhilight_pick"
                CList.Add(menuhighlight)
                Dim _Conditions As New Conditions With {.RetroBackground = True}
                C = ColorPicker.Pick(CList, _Conditions)
                menuhighlight.BackColor = C

            Case "menutext_pick"
                CList.Add(RetroLabel6)
                CList.Add(RetroLabel5)
                CList.Add(RetroLabel7)
                CList.Add(RetroLabel11)
                CList.Add(RetroLabel12)
                C = ColorPicker.Pick(CList)
                RetroLabel6.ForeColor = C
                RetroLabel5.ForeColor = C
                RetroLabel7.ForeColor = C
                RetroLabel11.ForeColor = C
                RetroLabel12.ForeColor = C

            Case "hilighttext_pick"
                CList.Add(RetroLabel8)
                C = ColorPicker.Pick(CList)
                RetroLabel8.ForeColor = C

            Case "GrayText_pick"
                CList.Add(RetroLabel2)
                CList.Add(RetroLabel9)
                CList.Add(RetroLabel10)

                C = ColorPicker.Pick(CList)
                RetroLabel2.ForeColor = C
                RetroLabel9.ForeColor = C
                RetroLabel10.ForeColor = C

            Case "Window_pick"
                CList.Add(RetroTextBox1)
                C = ColorPicker.Pick(CList)
                RetroTextBox1.BackColor = C

            Case "WindowText_pick"
                CList.Add(RetroTextBox1)
                Dim _Conditions As New Conditions With {.RetroWindowText = True}
                C = ColorPicker.Pick(CList, _Conditions)
                RetroTextBox1.ForeColor = C

            Case Else
                C = ColorPicker.Pick(CList)
        End Select

        CType(sender, XenonGroupBox).BackColor = C

        CList.Clear()

        RetroWindow1.Invalidate()
        RetroWindow2.Invalidate()
        RetroWindow3.Invalidate()


    End Sub


    Private Sub Win32UI_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        MainFrm.Visible = True
    End Sub

End Class