Imports WinPaletter.XenonCore

Public Class Win32UI
    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub

    Private Sub Win32UI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)

        CP.PopulateThemeToListbox(XenonComboBox1)
        RevalidateEverything(pnl_preview)

        MainFrm.Visible = False
        Location = New Point(10, (My.Computer.Screen.Bounds.Height - Height) / 2 - 20)
        ApplyDefaultCPValues()
        LoadCP(MainFrm.CP)
    End Sub


    Sub LoadCP(ByVal [CP] As CP)
        ApplyCPValues([CP])
        ApplyRetroPreview()
    End Sub

    Sub ApplyCPValues(ByVal [CP] As CP)
        XenonToggle1.Checked = [CP].Win32UI_EnableTheming
        XenonToggle2.Checked = [CP].Win32UI_EnableGradient
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

    Sub ApplyDefaultCPValues()
        Dim DefCP As CP

        If My.W11 Then
            DefCP = New CP_Defaults().Default_Windows11
        ElseIf My.W10 Then
            DefCP = New CP_Defaults().Default_Windows10
        ElseIf My.W8 Then
            DefCP = New CP_Defaults().Default_Windows8
        ElseIf My.W7 Then
            DefCP = New CP_Defaults().Default_Windows7
        Else
            DefCP = New CP_Defaults().Default_Windows11
        End If

        ActiveBorder_pick.DefaultColor = DefCP.Win32UI_ActiveBorder
        activetitle_pick.DefaultColor = DefCP.Win32UI_ActiveTitle
        AppWorkspace_pick.DefaultColor = DefCP.Win32UI_AppWorkspace
        background_pick.DefaultColor = DefCP.Win32UI_Background
        btnaltface_pick.DefaultColor = DefCP.Win32UI_ButtonAlternateFace
        btndkshadow_pick.DefaultColor = DefCP.Win32UI_ButtonDkShadow
        btnface_pick.DefaultColor = DefCP.Win32UI_ButtonFace
        btnhilight_pick.DefaultColor = DefCP.Win32UI_ButtonHilight
        btnlight_pick.DefaultColor = DefCP.Win32UI_ButtonLight
        btnshadow_pick.DefaultColor = DefCP.Win32UI_ButtonShadow
        btntext_pick.DefaultColor = DefCP.Win32UI_ButtonText
        GActivetitle_pick.DefaultColor = DefCP.Win32UI_GradientActiveTitle
        GInactivetitle_pick.DefaultColor = DefCP.Win32UI_GradientInactiveTitle
        GrayText_pick.DefaultColor = DefCP.Win32UI_GrayText
        hilighttext_pick.DefaultColor = DefCP.Win32UI_HilightText
        hottracking_pick.DefaultColor = DefCP.Win32UI_HotTrackingColor
        InactiveBorder_pick.DefaultColor = DefCP.Win32UI_InactiveBorder
        InactiveTitle_pick.DefaultColor = DefCP.Win32UI_InactiveTitle
        InactivetitleText_pick.DefaultColor = DefCP.Win32UI_InactiveTitleText
        InfoText_pick.DefaultColor = DefCP.Win32UI_InfoText
        InfoWindow_pick.DefaultColor = DefCP.Win32UI_InfoWindow
        menu_pick.DefaultColor = DefCP.Win32UI_Menu
        menubar_pick.DefaultColor = DefCP.Win32UI_MenuBar
        menutext_pick.DefaultColor = DefCP.Win32UI_MenuText
        Scrollbar_pick.DefaultColor = DefCP.Win32UI_Scrollbar
        TitleText_pick.DefaultColor = DefCP.Win32UI_TitleText
        Window_pick.DefaultColor = DefCP.Win32UI_Window
        Frame_pick.DefaultColor = DefCP.Win32UI_WindowFrame
        WindowText_pick.DefaultColor = DefCP.Win32UI_WindowText
        hilight_pick.DefaultColor = DefCP.Win32UI_Hilight
        menuhilight_pick.DefaultColor = DefCP.Win32UI_MenuHilight
        desktop_pick.DefaultColor = DefCP.Win32UI_Desktop
    End Sub

    Sub ApplyToCP(ByVal [CP] As CP)
        [CP].Win32UI_EnableTheming = XenonToggle1.Checked
        [CP].Win32UI_EnableGradient = XenonToggle2.Checked
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

        If DirectCast(e, MouseEventArgs).Button = MouseButtons.Right Then
            SubMenu.ShowMenu(sender)
            If My.Application.ColorEvent = My.MyApplication.MenuEvent.Cut Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Paste Or My.Application.ColorEvent = My.MyApplication.MenuEvent.Override Then
                ApplyRetroPreview()
            End If
            Exit Sub
        End If

        Dim CList As New List(Of Control) From {sender}

        Dim C As Color

        Select Case sender.Name
            Case "activetitle_pick"
                CList.Add(RetroWindow2)
                CList.Add(RetroWindow3)
                CList.Add(RetroWindow4)

                Dim _Conditions As New Conditions With {.RetroWindowColor1 = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
                RetroWindow2.Color1 = C
                RetroWindow3.Color1 = C
                RetroWindow4.Color1 = C

            Case "GActivetitle_pick"
                CList.Add(RetroWindow2)
                CList.Add(RetroWindow3)
                CList.Add(RetroWindow4)

                Dim _Conditions As New Conditions With {.RetroWindowColor2 = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
                RetroWindow2.Color2 = C
                RetroWindow3.Color2 = C
                RetroWindow4.Color2 = C

            Case "TitleText_pick"
                CList.Add(RetroWindow2)
                CList.Add(RetroWindow3)
                CList.Add(RetroWindow4)

                Dim _Conditions As New Conditions With {.RetroWindowForeColor = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
                RetroWindow2.ForeColor = C
                RetroWindow3.ForeColor = C
                RetroWindow4.ForeColor = C

            Case "InactiveTitle_pick"
                CList.Add(RetroWindow1)
                Dim _Conditions As New Conditions With {.RetroWindowColor1 = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
                RetroWindow1.Color1 = C

            Case "GInactivetitle_pick"
                CList.Add(RetroWindow1)
                Dim _Conditions As New Conditions With {.RetroWindowColor2 = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
                RetroWindow1.Color2 = C

            Case "InactivetitleText_pick"
                CList.Add(RetroWindow1)
                Dim _Conditions As New Conditions With {.RetroWindowForeColor = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
                RetroWindow1.ForeColor = C

            Case "ActiveBorder_pick"
                CList.Add(RetroWindow2)
                CList.Add(RetroWindow3)
                CList.Add(RetroWindow4)

                Dim _Conditions As New Conditions With {.RetroWindowBorder = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
                RetroWindow2.ColorBorder = C
                RetroWindow3.ColorBorder = C
                RetroWindow4.ColorBorder = C

            Case "InactiveBorder_pick"
                CList.Add(RetroWindow1)
                Dim _Conditions As New Conditions With {.RetroWindowBorder = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
                RetroWindow1.ColorBorder = C

            Case "Frame_pick"
                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        CList.Add(RB)
                    Next
                Next

                For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
                    CList.Add(RB)
                Next

                Dim _Conditions As New Conditions With {.RetroWindowFrame = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)

                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        RB.WindowFrame = C
                    Next
                Next
                For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
                    RB.WindowFrame = C
                Next

            Case "btnface_pick"
                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    CList.Add(RW)
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        CList.Add(RB)
                    Next
                Next
                For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
                    CList.Add(RB)
                Next
                CList.Add(RetroPanel2)
                Dim _Conditions As New Conditions With {.RetroButtonFace = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)

                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    If RW IsNot Menu Then RW.BackColor = C Else RW.ButtonFace = C
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        RB.BackColor = C
                    Next
                Next
                For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
                    RB.BackColor = C
                Next

                RetroPanel2.BackColor = C

            Case "btndkshadow_pick"
                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    CList.Add(RW)
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        CList.Add(RB)
                    Next
                Next
                For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
                    CList.Add(RB)
                Next

                CList.Add(RetroTextBox1)

                Dim _Conditions As New Conditions With {.RetroButtonDkShadow = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)

                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    RW.ButtonDkShadow = C
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        RB.ButtonDkShadow = C
                    Next
                Next
                For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
                    RB.ButtonDkShadow = C
                Next

                RetroTextBox1.ButtonDkShadow = C

            Case "btnhilight_pick"
                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    CList.Add(RW)
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        CList.Add(RB)
                    Next
                Next
                For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
                    CList.Add(RB)
                Next
                For Each RB As RetroPanelRaised In pnl_preview.Controls.OfType(Of RetroPanelRaised)
                    CList.Add(RB)
                Next

                CList.Add(RetroTextBox1)
                CList.Add(RetroPanel1)
                CList.Add(RetroPanel2)

                Dim _Conditions As New Conditions With {.RetroButtonHilight = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)

                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    RW.ButtonHilight = C
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        RB.ButtonHilight = C
                    Next
                Next
                For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
                    RB.ButtonHilight = C
                Next
                For Each RB As RetroPanelRaised In pnl_preview.Controls.OfType(Of RetroPanelRaised)
                    RB.ButtonHilight = C
                Next
                RetroTextBox1.ButtonHilight = C
                RetroPanel1.ButtonHilight = C
                RetroPanel2.ButtonHilight = C

            Case "btnlight_pick"
                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    CList.Add(RW)
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        CList.Add(RB)
                    Next
                Next
                For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
                    CList.Add(RB)
                Next

                CList.Add(RetroTextBox1)

                Dim _Conditions As New Conditions With {.RetroButtonLight = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)

                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    RW.ButtonLight = C
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        RB.ButtonLight = C
                    Next
                Next
                For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
                    RB.ButtonLight = C
                Next
                RetroTextBox1.ButtonLight = C

            Case "btnshadow_pick"
                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    CList.Add(RW)
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        CList.Add(RB)
                    Next
                Next
                For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
                    CList.Add(RB)
                Next
                For Each RB As RetroPanelRaised In pnl_preview.Controls.OfType(Of RetroPanelRaised)
                    CList.Add(RB)
                Next

                CList.Add(RetroTextBox1)
                CList.Add(RetroPanel1)

                Dim _Conditions As New Conditions With {.RetroButtonShadow = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)

                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    RW.ButtonShadow = C
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        RB.ButtonShadow = C
                    Next
                Next
                For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
                    RB.ButtonShadow = C
                Next
                For Each RB As RetroPanelRaised In pnl_preview.Controls.OfType(Of RetroPanelRaised)
                    RB.ButtonShadow = C
                Next

                RetroTextBox1.ButtonShadow = C
                RetroPanel1.ButtonShadow = C

            Case "btntext_pick"
                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        CList.Add(RB)
                    Next
                Next
                For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
                    CList.Add(RB)
                Next

                Dim _Conditions As New Conditions With {.RetroButtonText = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)

                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        RB.ForeColor = C
                    Next
                Next
                For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
                    RB.ForeColor = C
                Next

            Case "AppWorkspace_pick"
                CList.Add(Panel2)
                Dim _Conditions As New Conditions With {.RetroAppWorkspace = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
                Panel2.BackColor = C

            Case "background_pick"
                CList.Add(pnl_preview)
                Dim _Conditions As New Conditions With {.RetroBackground = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
                pnl_preview.BackColor = C

            Case "menu_pick"
                CList.Add(Menu)

                If Not XenonToggle1.Checked Then CList.Add(RetroPanel1)
                If Not XenonToggle1.Checked Then CList.Add(Panel3)

                Dim _Conditions As New Conditions With {.RetroBackground = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
                Menu.BackColor = C
                Menu.Invalidate()

                If Not XenonToggle1.Checked Then RetroPanel1.BackColor = C
                If Not XenonToggle1.Checked Then Panel3.BackColor = C

            Case "menubar_pick"
                If XenonToggle1.Checked Then
                    CList.Add(Panel3)
                    Dim _Conditions As New Conditions With {.RetroBackground = True}
                    C = ColorPickerDlg.Pick(CList, _Conditions)
                    Panel3.BackColor = C
                Else
                    C = ColorPickerDlg.Pick(CList)
                    sender.BackColor = C
                End If

            Case "hilight_pick"

                If XenonToggle1.Checked Then
                    CList.Add(highlight)
                    CList.Add(RetroPanel1)
                    Dim _Conditions As New Conditions With {.RetroButtonShadow = True, .RetroBackground = True, .RetroHighlight17BitFixer = True}
                    C = ColorPickerDlg.Pick(CList, _Conditions)
                    highlight.BackColor = C
                    RetroPanel1.ButtonShadow = C
                Else
                    CList.Add(highlight)
                    CList.Add(menuhilight)
                    Dim _Conditions As New Conditions With {.RetroBackground = True}
                    C = ColorPickerDlg.Pick(CList, _Conditions)
                    highlight.BackColor = C
                    menuhilight.BackColor = C
                End If


            Case "menuhilight_pick"

                If XenonToggle1.Checked Then
                    CList.Add(menuhilight)
                    CList.Add(RetroPanel1)
                    Dim _Conditions As New Conditions With {.RetroBackground = True}
                    C = ColorPickerDlg.Pick(CList, _Conditions)
                    menuhilight.BackColor = C
                    RetroPanel1.BackColor = C

                Else
                    C = ColorPickerDlg.Pick(CList)
                    sender.BackColor = C
                End If

            Case "menutext_pick"
                CList.Add(RetroLabel1)
                CList.Add(RetroLabel2)
                If Not XenonToggle1.Checked Then CList.Add(RetroLabel3)
                CList.Add(RetroLabel6)

                C = ColorPickerDlg.Pick(CList)

                RetroLabel1.ForeColor = C
                RetroLabel2.ForeColor = C
                If Not XenonToggle1.Checked Then RetroLabel3.ForeColor = C
                RetroLabel6.ForeColor = C

            Case "hilighttext_pick"
                CList.Add(RetroLabel5)
                If XenonToggle1.Checked Then CList.Add(RetroLabel3)

                C = ColorPickerDlg.Pick(CList)
                RetroLabel5.ForeColor = C
                If XenonToggle1.Checked Then RetroLabel3.ForeColor = C

            Case "GrayText_pick"
                CList.Add(RetroLabel2)
                CList.Add(RetroLabel9)

                C = ColorPickerDlg.Pick(CList)
                RetroLabel2.ForeColor = C
                RetroLabel9.ForeColor = C

            Case "Window_pick"
                CList.Add(RetroTextBox1)
                Dim _Conditions As New Conditions With {.RetroBackground = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
                RetroTextBox1.BackColor = C

            Case "WindowText_pick"
                CList.Add(RetroTextBox1)
                CList.Add(RetroLabel4)

                Dim _Conditions As New Conditions With {.RetroWindowText = True, .RetroWindowForeColor = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
                RetroTextBox1.ForeColor = C
                RetroLabel4.ForeColor = C

            Case "InfoWindow_pick"
                CList.Add(RetroLabel13)
                Dim _Conditions As New Conditions With {.RetroBackground = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
                RetroLabel13.BackColor = C

            Case "InfoText_pick"
                CList.Add(RetroLabel13)
                C = ColorPickerDlg.Pick(CList)
                RetroLabel13.ForeColor = C

            Case "Scrollbar_pick"
                'CList.Add(RetroPanel2)
                'Dim _Conditions As New Conditions With {.RetroBackground = True}
                C = ColorPickerDlg.Pick(CList) ', _Conditions)
                'RetroPanel2.BackColor = C

            Case Else
                C = ColorPickerDlg.Pick(CList)
        End Select

        CType(sender, XenonGroupBox).BackColor = C

        CList.Clear()

        RetroWindow1.Invalidate()
        RetroWindow2.Invalidate()
        RetroWindow3.Invalidate()

        Refresh17BitPreference()
    End Sub

    Private Sub Win32UI_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        MainFrm.Visible = True
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click
        If OpenThemeDialog.ShowDialog = DialogResult.OK Then
            XenonToggle1.Checked = False
            LoadFromWin9xTheme(OpenThemeDialog.FileName)
        End If
    End Sub

    Sub LoadFromWin9xTheme(File As String)
        If IO.File.Exists(File) Then
            Dim s As New List(Of String)
            Dim FoundGradientActive As Boolean = False
            Dim FoundGradientInactive As Boolean = False

            CList_FromStr(s, IO.File.ReadAllText(File))
            For Each x As String In s

                If x.ToLower.StartsWith("activetitle=".ToLower) Then
                    activetitle_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                    If Not FoundGradientActive Then GActivetitle_pick.BackColor = activetitle_pick.BackColor
                End If

                If x.ToLower.StartsWith("gradientactivetitle=".ToLower) Then
                    GActivetitle_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                    FoundGradientActive = True
                End If

                If x.ToLower.StartsWith("inactivetitle=".ToLower) Then
                    InactiveTitle_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                    If Not FoundGradientInactive Then GInactivetitle_pick.BackColor = InactiveTitle_pick.BackColor

                End If

                If x.ToLower.StartsWith("gradientinactivetitle=".ToLower) Then
                    GInactivetitle_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                    FoundGradientInactive = True
                End If

                If x.ToLower.StartsWith("background=".ToLower) Then background_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If x.ToLower.StartsWith("hilight=".ToLower) Then hilight_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If x.ToLower.StartsWith("hilighttext=".ToLower) Then hilighttext_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If x.ToLower.StartsWith("titletext=".ToLower) Then TitleText_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If x.ToLower.StartsWith("window=".ToLower) Then Window_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If x.ToLower.StartsWith("windowtext=".ToLower) Then WindowText_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If x.ToLower.StartsWith("scrollbar=".ToLower) Then Scrollbar_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If x.ToLower.StartsWith("menu=".ToLower) Then menu_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If x.ToLower.StartsWith("windowframe=".ToLower) Then Frame_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If x.ToLower.StartsWith("menutext=".ToLower) Then menutext_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If x.ToLower.StartsWith("activeborder=".ToLower) Then ActiveBorder_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If x.ToLower.StartsWith("inactiveborder=".ToLower) Then InactiveBorder_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If x.ToLower.StartsWith("appworkspace=".ToLower) Then AppWorkspace_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If x.ToLower.StartsWith("buttonface=".ToLower) Then btnface_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If x.ToLower.StartsWith("buttonshadow=".ToLower) Then btnshadow_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If x.ToLower.StartsWith("graytext=".ToLower) Then GrayText_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If x.ToLower.StartsWith("buttontext=".ToLower) Then btntext_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If x.ToLower.StartsWith("inactivetitletext=".ToLower) Then InactivetitleText_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If x.ToLower.StartsWith("buttonhilight=".ToLower) Then btnhilight_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If x.ToLower.StartsWith("buttondkshadow=".ToLower) Then btndkshadow_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If x.ToLower.StartsWith("buttonlight=".ToLower) Then btnlight_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If x.ToLower.StartsWith("infotext=".ToLower) Then InfoText_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If x.ToLower.StartsWith("infowindow=".ToLower) Then InfoWindow_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If x.ToLower.StartsWith("hottrackingcolor=".ToLower) Then hottracking_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))

                If x.ToLower.StartsWith("buttonalternateface=".ToLower) Then btnaltface_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If x.ToLower.StartsWith("menubar=".ToLower) Then menubar_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If x.ToLower.StartsWith("menuhilight=".ToLower) Then menuhilight_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If x.ToLower.StartsWith("desktop=".ToLower) Then desktop_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
            Next
        End If

        ApplyRetroPreview()
    End Sub

    Sub LoadFromWinThemeString([String] As String, ThemeName As String)

        If String.IsNullOrWhiteSpace([String]) Then
            Exit Sub
        End If

        If Not [String].Contains("|") Then
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(ThemeName) Then
            Exit Sub
        End If

        Dim ls As New List(Of Color)
        ls.Clear()

        Dim AllThemes As New List(Of String)
        Dim SelectedTheme As String = ""
        Dim SelectedThemeList As New List(Of String)

        CList_FromStr(AllThemes, [String])
        Dim Found As Boolean = False

        For Each th As String In AllThemes
            If th.Split("|")(0).ToLower = ThemeName.ToLower Then
                SelectedTheme = th.Replace("|", vbCrLf)
                Found = True
                Exit For
            End If
        Next

        If Not Found Then
            Exit Sub
        End If

        CList_FromStr(SelectedThemeList, SelectedTheme)

        Dim FoundGradientActive As Boolean = False
        Dim FoundGradientInactive As Boolean = False

        For Each x As String In SelectedThemeList

            If x.ToLower.StartsWith("activetitle=".ToLower) Then
                activetitle_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If Not FoundGradientActive Then GActivetitle_pick.BackColor = activetitle_pick.BackColor
            End If

            If x.ToLower.StartsWith("gradientactivetitle=".ToLower) Then
                GActivetitle_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                FoundGradientActive = True
            End If

            If x.ToLower.StartsWith("inactivetitle=".ToLower) Then
                InactiveTitle_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                If Not FoundGradientInactive Then GInactivetitle_pick.BackColor = InactiveTitle_pick.BackColor

            End If

            If x.ToLower.StartsWith("gradientinactivetitle=".ToLower) Then
                GInactivetitle_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
                FoundGradientInactive = True
            End If

            If x.ToLower.StartsWith("background=".ToLower) Then background_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
            If x.ToLower.StartsWith("hilight=".ToLower) Then hilight_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
            If x.ToLower.StartsWith("hilighttext=".ToLower) Then hilighttext_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
            If x.ToLower.StartsWith("titletext=".ToLower) Then TitleText_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
            If x.ToLower.StartsWith("window=".ToLower) Then Window_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
            If x.ToLower.StartsWith("windowtext=".ToLower) Then WindowText_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
            If x.ToLower.StartsWith("scrollbar=".ToLower) Then Scrollbar_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
            If x.ToLower.StartsWith("menu=".ToLower) Then menu_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
            If x.ToLower.StartsWith("windowframe=".ToLower) Then Frame_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
            If x.ToLower.StartsWith("menutext=".ToLower) Then menutext_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
            If x.ToLower.StartsWith("activeborder=".ToLower) Then ActiveBorder_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
            If x.ToLower.StartsWith("inactiveborder=".ToLower) Then InactiveBorder_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
            If x.ToLower.StartsWith("appworkspace=".ToLower) Then AppWorkspace_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
            If x.ToLower.StartsWith("buttonface=".ToLower) Then btnface_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
            If x.ToLower.StartsWith("buttonshadow=".ToLower) Then btnshadow_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
            If x.ToLower.StartsWith("graytext=".ToLower) Then GrayText_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
            If x.ToLower.StartsWith("buttontext=".ToLower) Then btntext_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
            If x.ToLower.StartsWith("inactivetitletext=".ToLower) Then InactivetitleText_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
            If x.ToLower.StartsWith("buttonhilight=".ToLower) Then btnhilight_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
            If x.ToLower.StartsWith("buttondkshadow=".ToLower) Then btndkshadow_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
            If x.ToLower.StartsWith("buttonlight=".ToLower) Then btnlight_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
            If x.ToLower.StartsWith("infotext=".ToLower) Then InfoText_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
            If x.ToLower.StartsWith("infowindow=".ToLower) Then InfoWindow_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
            If x.ToLower.StartsWith("hottrackingcolor=".ToLower) Then hottracking_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))

            If x.ToLower.StartsWith("buttonalternateface=".ToLower) Then btnaltface_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
            If x.ToLower.StartsWith("menubar=".ToLower) Then menubar_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
            If x.ToLower.StartsWith("menuhilight=".ToLower) Then menuhilight_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
            If x.ToLower.StartsWith("desktop=".ToLower) Then desktop_pick.BackColor = Color.FromArgb(x.Split("=")(1).Split(" ")(0), x.Split("=")(1).Split(" ")(1), x.Split("=")(1).Split(" ")(2))
        Next

        ApplyRetroPreview()
    End Sub

    Sub ApplyRetroPreview()
        RetroWindow1.ColorGradient = XenonToggle2.Checked
        RetroWindow2.ColorGradient = XenonToggle2.Checked
        RetroWindow3.ColorGradient = XenonToggle2.Checked
        RetroWindow4.ColorGradient = XenonToggle2.Checked

        Dim c As Color
        c = activetitle_pick.BackColor
        RetroWindow2.Color1 = c
        RetroWindow3.Color1 = c
        RetroWindow4.Color1 = c

        c = GActivetitle_pick.BackColor
        RetroWindow2.Color2 = c
        RetroWindow3.Color2 = c
        RetroWindow4.Color2 = c

        c = TitleText_pick.BackColor
        RetroWindow2.ForeColor = c
        RetroWindow3.ForeColor = c
        RetroWindow4.ForeColor = c

        c = InactiveTitle_pick.BackColor
        RetroWindow1.Color1 = c

        c = GInactivetitle_pick.BackColor
        RetroWindow1.Color2 = c

        c = InactivetitleText_pick.BackColor
        RetroWindow1.ForeColor = c

        c = ActiveBorder_pick.BackColor
        RetroWindow2.ColorBorder = c
        RetroWindow3.ColorBorder = c
        RetroWindow4.ColorBorder = c

        c = InactiveBorder_pick.BackColor
        RetroWindow1.ColorBorder = c

        c = Frame_pick.BackColor
        For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.WindowFrame = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.WindowFrame = c
        Next

        c = btnface_pick.BackColor
        For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
            If RW IsNot Menu Then RW.BackColor = c
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.BackColor = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.BackColor = c
        Next
        RetroPanel2.BackColor = c
        Menu.ButtonFace = c

        c = btndkshadow_pick.BackColor
        For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
            RW.ButtonDkShadow = c
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.ButtonDkShadow = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.ButtonDkShadow = c
        Next
        RetroTextBox1.ButtonDkShadow = c
        Menu.ButtonDkShadow = c

        c = btnhilight_pick.BackColor
        For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
            RW.ButtonHilight = c
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.ButtonHilight = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.ButtonHilight = c
        Next
        For Each RB As RetroPanelRaised In pnl_preview.Controls.OfType(Of RetroPanelRaised)
            RB.ButtonHilight = c
        Next
        RetroTextBox1.ButtonHilight = c
        RetroPanel1.ButtonHilight = c
        RetroPanel2.ButtonHilight = c
        Menu.ButtonHilight = c

        c = btnlight_pick.BackColor
        For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
            RW.ButtonLight = c
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.ButtonLight = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.ButtonLight = c
        Next
        RetroTextBox1.ButtonLight = c
        Menu.ButtonLight = c

        c = btnshadow_pick.BackColor
        For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
            RW.ButtonShadow = c
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.ButtonShadow = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.ButtonShadow = c
        Next
        For Each RB As RetroPanelRaised In pnl_preview.Controls.OfType(Of RetroPanelRaised)
            RB.ButtonShadow = c
        Next
        RetroTextBox1.ButtonShadow = c
        RetroPanel1.ButtonShadow = c
        RetroTextBox1.Invalidate()
        Menu.ButtonShadow = c

        c = btntext_pick.BackColor
        For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.ForeColor = c
            Next
        Next
        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.ForeColor = c
        Next

        c = AppWorkspace_pick.BackColor
        Panel2.BackColor = c

        c = background_pick.BackColor
        pnl_preview.BackColor = c

        c = menu_pick.BackColor
        Menu.BackColor = c
        RetroPanel1.BackColor = c
        Menu.Invalidate()

        c = menubar_pick.BackColor
        Panel3.BackColor = c

        c = hilight_pick.BackColor
        highlight.BackColor = c

        c = menuhilight_pick.BackColor
        menuhilight.BackColor = c

        c = menutext_pick.BackColor
        RetroLabel6.ForeColor = c
        RetroLabel1.ForeColor = c

        c = hilighttext_pick.BackColor
        RetroLabel5.ForeColor = c

        c = GrayText_pick.BackColor
        RetroLabel2.ForeColor = c
        RetroLabel9.ForeColor = c

        c = Window_pick.BackColor
        RetroTextBox1.BackColor = c

        c = WindowText_pick.BackColor
        RetroTextBox1.ForeColor = c
        RetroLabel4.ForeColor = c

        c = InfoWindow_pick.BackColor
        RetroLabel13.BackColor = c

        c = InfoText_pick.BackColor
        RetroLabel13.ForeColor = c

        For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
            RW.Invalidate()
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.Invalidate()
            Next
        Next

        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.Invalidate()
        Next

        Refresh17BitPreference()

    End Sub

    Private Sub XenonToggle1_CheckedChanged(sender As Object, e As EventArgs) Handles XenonToggle1.CheckedChanged
        Refresh17BitPreference()
    End Sub

    Sub Refresh17BitPreference()

        If XenonToggle1.Checked Then
            'Theming Enabled (Menus Has colors and borders)
            Menu.Flat = True
            RetroPanel1.Flat = True
            menuhilight.BackColor = menuhilight_pick.BackColor 'Filling of selected item
            highlight.BackColor = hilight_pick.BackColor 'Outer Border of selected item

            RetroPanel1.BackColor = menuhilight_pick.BackColor
            RetroPanel1.ButtonShadow = hilight_pick.BackColor

            Panel3.BackColor = menubar_pick.BackColor
            RetroLabel3.ForeColor = hilighttext_pick.BackColor
        Else
            'Theming Disabled (Menus are retro 3d)
            Menu.Flat = False
            RetroPanel1.Flat = False
            menuhilight.BackColor = hilight_pick.BackColor 'Both will have same color
            highlight.BackColor = hilight_pick.BackColor 'Both will have same color
            RetroPanel1.BackColor = menu_pick.BackColor
            RetroPanel1.ButtonShadow = btnshadow_pick.BackColor
            Panel3.BackColor = menu_pick.BackColor
            RetroLabel3.ForeColor = menutext_pick.BackColor

        End If

        Menu.Invalidate()
        RetroPanel1.Invalidate()
        menuhilight.Invalidate()
        highlight.Invalidate()

    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        btndkshadow_pick.BackColor = Color.Black
        btnshadow_pick.BackColor = Color.FromArgb(128, 128, 128)
        btnhilight_pick.BackColor = Color.White
        btnlight_pick.BackColor = Color.FromArgb(192, 192, 192)
        btnface_pick.BackColor = Color.FromArgb(192, 192, 192)
        Frame_pick.BackColor = Color.Black
        XenonToggle1.Checked = False
        ApplyRetroPreview()
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            LoadCP(New CP(CP.Mode.File, OpenFileDialog1.FileName))
        End If
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        LoadCP(New CP(CP.Mode.Registry))
    End Sub


    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        Process.Start("https://www.neowin.net/forum/topic/624901-windows-colors-explained/")
    End Sub

    Private Sub XenonButton9_Click(sender As Object, e As EventArgs) Handles XenonButton9.Click
        If String.IsNullOrWhiteSpace(XenonComboBox1.SelectedItem) Then Exit Sub
        XenonToggle1.Checked = (XenonComboBox1.SelectedIndex = 0)
        LoadFromWinThemeString(My.Resources.RetroThemesDB, XenonComboBox1.SelectedItem)
    End Sub

    Private Sub XenonToggle2_CheckedChanged(sender As Object, e As EventArgs) Handles XenonToggle2.CheckedChanged
        RetroWindow1.ColorGradient = XenonToggle2.Checked
        RetroWindow2.ColorGradient = XenonToggle2.Checked
        RetroWindow3.ColorGradient = XenonToggle2.Checked
        RetroWindow4.ColorGradient = XenonToggle2.Checked

        RetroWindow1.Invalidate()
        RetroWindow2.Invalidate()
        RetroWindow3.Invalidate()
        RetroWindow4.Invalidate()
    End Sub


    Sub RevalidateEverything([Control] As Control)
        For Each c As Control In Control.Controls
            If c.HasChildren Then RevalidateEverything(c)
            c.Invalidate()
        Next
        [Control].Invalidate()
    End Sub

    Private Sub XenonButton10_Click(sender As Object, e As EventArgs) Handles XenonButton10.Click
        Dim CPx As New CP(CP.Mode.Registry)
        ApplyToCP(CPx)
        ApplyToCP(MainFrm.CP)
        CPx.Save(CP.SavingMode.Registry)
    End Sub

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click
        If SaveFileDialog2.ShowDialog = DialogResult.OK Then
            Dim s As New List(Of String)
            s.Clear()
            s.Add(String.Format("; Copyright © Microsoft Corp. 1995-{0}", Now.Year))
            s.Add(String.Format("; This theme was designed by WinPaletter, programmed by Abdelrhman-AK"))
            s.Add(String.Format("; Created from application version {0}", MainFrm.CP.AppVersion))
            s.Add(String.Format("; Created by {0}", MainFrm.CP.Author))
            s.Add(String.Format("; Palette Name: {0}", MainFrm.CP.PaletteName))
            s.Add(String.Format("; Palette Version: {0}", MainFrm.CP.PaletteVersion))
            s.Add("")
            s.Add(String.Format("[Control Panel\Colors]"))

            s.Add("")
            s.Add(String.Format("[MasterThemeSelector]"))
            s.Add(String.Format("MTSM=DABJDKT"))

        End If
    End Sub
End Class