Imports WinPaletter.XenonCore
Imports WinPaletter.PreviewHelpers
Imports System.Management

Public Class Win32UI
    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Me.Close()
    End Sub

    Private Sub Win32UI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        XenonComboBox1.PopulateThemes
        XenonComboBox1.SelectedIndex = 0
        MainFrm.Visible = False
        If Not My.Settings.Classic_Color_Picker Then Location = New Point(10, (My.Computer.Screen.Bounds.Height - Height) / 2 - 20)
        ApplyDefaultCPValues()
        LoadCP(MainFrm.CP)
        SetMetics(MainFrm.CP)
        DoubleBuffer
    End Sub

    Sub LoadCP(ByVal [CP] As CP)
        ApplyCPValues([CP])
        ApplyRetroPreview()
    End Sub

    Sub ApplyCPValues(ByVal [CP] As CP)
        XenonToggle1.Checked = [CP].Win32.EnableTheming
        XenonToggle2.Checked = [CP].Win32.EnableGradient
        ActiveBorder_pick.BackColor = [CP].Win32.ActiveBorder
        activetitle_pick.BackColor = [CP].Win32.ActiveTitle
        AppWorkspace_pick.BackColor = [CP].Win32.AppWorkspace
        background_pick.BackColor = [CP].Win32.Background
        btnaltface_pick.BackColor = [CP].Win32.ButtonAlternateFace
        btndkshadow_pick.BackColor = [CP].Win32.ButtonDkShadow
        btnface_pick.BackColor = [CP].Win32.ButtonFace
        btnhilight_pick.BackColor = [CP].Win32.ButtonHilight
        btnlight_pick.BackColor = [CP].Win32.ButtonLight
        btnshadow_pick.BackColor = [CP].Win32.ButtonShadow
        btntext_pick.BackColor = [CP].Win32.ButtonText
        GActivetitle_pick.BackColor = [CP].Win32.GradientActiveTitle
        GInactivetitle_pick.BackColor = [CP].Win32.GradientInactiveTitle
        GrayText_pick.BackColor = [CP].Win32.GrayText
        hilighttext_pick.BackColor = [CP].Win32.HilightText
        hottracking_pick.BackColor = [CP].Win32.HotTrackingColor
        InactiveBorder_pick.BackColor = [CP].Win32.InactiveBorder
        InactiveTitle_pick.BackColor = [CP].Win32.InactiveTitle
        InactivetitleText_pick.BackColor = [CP].Win32.InactiveTitleText
        InfoText_pick.BackColor = [CP].Win32.InfoText
        InfoWindow_pick.BackColor = [CP].Win32.InfoWindow
        menu_pick.BackColor = [CP].Win32.Menu
        menubar_pick.BackColor = [CP].Win32.MenuBar
        menutext_pick.BackColor = [CP].Win32.MenuText
        Scrollbar_pick.BackColor = [CP].Win32.Scrollbar
        TitleText_pick.BackColor = [CP].Win32.TitleText
        Window_pick.BackColor = [CP].Win32.Window
        Frame_pick.BackColor = [CP].Win32.WindowFrame
        WindowText_pick.BackColor = [CP].Win32.WindowText
        hilight_pick.BackColor = [CP].Win32.Hilight
        menuhilight_pick.BackColor = [CP].Win32.MenuHilight
        desktop_pick.BackColor = [CP].Win32.Desktop
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
        ElseIf My.WVista Then
            DefCP = New CP_Defaults().Default_WindowsVista
        ElseIf My.WXP Then
            DefCP = New CP_Defaults().Default_WindowsXP
        Else
            DefCP = New CP_Defaults().Default_Windows11
        End If

        ActiveBorder_pick.DefaultColor = DefCP.Win32.ActiveBorder
        activetitle_pick.DefaultColor = DefCP.Win32.ActiveTitle
        AppWorkspace_pick.DefaultColor = DefCP.Win32.AppWorkspace
        background_pick.DefaultColor = DefCP.Win32.Background
        btnaltface_pick.DefaultColor = DefCP.Win32.ButtonAlternateFace
        btndkshadow_pick.DefaultColor = DefCP.Win32.ButtonDkShadow
        btnface_pick.DefaultColor = DefCP.Win32.ButtonFace
        btnhilight_pick.DefaultColor = DefCP.Win32.ButtonHilight
        btnlight_pick.DefaultColor = DefCP.Win32.ButtonLight
        btnshadow_pick.DefaultColor = DefCP.Win32.ButtonShadow
        btntext_pick.DefaultColor = DefCP.Win32.ButtonText
        GActivetitle_pick.DefaultColor = DefCP.Win32.GradientActiveTitle
        GInactivetitle_pick.DefaultColor = DefCP.Win32.GradientInactiveTitle
        GrayText_pick.DefaultColor = DefCP.Win32.GrayText
        hilighttext_pick.DefaultColor = DefCP.Win32.HilightText
        hottracking_pick.DefaultColor = DefCP.Win32.HotTrackingColor
        InactiveBorder_pick.DefaultColor = DefCP.Win32.InactiveBorder
        InactiveTitle_pick.DefaultColor = DefCP.Win32.InactiveTitle
        InactivetitleText_pick.DefaultColor = DefCP.Win32.InactiveTitleText
        InfoText_pick.DefaultColor = DefCP.Win32.InfoText
        InfoWindow_pick.DefaultColor = DefCP.Win32.InfoWindow
        menu_pick.DefaultColor = DefCP.Win32.Menu
        menubar_pick.DefaultColor = DefCP.Win32.MenuBar
        menutext_pick.DefaultColor = DefCP.Win32.MenuText
        Scrollbar_pick.DefaultColor = DefCP.Win32.Scrollbar
        TitleText_pick.DefaultColor = DefCP.Win32.TitleText
        Window_pick.DefaultColor = DefCP.Win32.Window
        Frame_pick.DefaultColor = DefCP.Win32.WindowFrame
        WindowText_pick.DefaultColor = DefCP.Win32.WindowText
        hilight_pick.DefaultColor = DefCP.Win32.Hilight
        menuhilight_pick.DefaultColor = DefCP.Win32.MenuHilight
        desktop_pick.DefaultColor = DefCP.Win32.Desktop
    End Sub

    Sub ApplyToCP(ByVal [CP] As CP)
        [CP].Win32.EnableTheming = XenonToggle1.Checked
        [CP].Win32.EnableGradient = XenonToggle2.Checked
        [CP].Win32.ActiveBorder = ActiveBorder_pick.BackColor
        [CP].Win32.ActiveTitle = activetitle_pick.BackColor
        [CP].Win32.AppWorkspace = AppWorkspace_pick.BackColor
        [CP].Win32.Background = background_pick.BackColor
        [CP].Win32.ButtonAlternateFace = btnaltface_pick.BackColor
        [CP].Win32.ButtonDkShadow = btndkshadow_pick.BackColor
        [CP].Win32.ButtonFace = btnface_pick.BackColor
        [CP].Win32.ButtonHilight = btnhilight_pick.BackColor
        [CP].Win32.ButtonLight = btnlight_pick.BackColor
        [CP].Win32.ButtonShadow = btnshadow_pick.BackColor
        [CP].Win32.ButtonText = btntext_pick.BackColor
        [CP].Win32.GradientActiveTitle = GActivetitle_pick.BackColor
        [CP].Win32.GradientInactiveTitle = GInactivetitle_pick.BackColor
        [CP].Win32.GrayText = GrayText_pick.BackColor
        [CP].Win32.HilightText = hilighttext_pick.BackColor
        [CP].Win32.HotTrackingColor = hottracking_pick.BackColor
        [CP].Win32.InactiveBorder = InactiveBorder_pick.BackColor
        [CP].Win32.InactiveTitle = InactiveTitle_pick.BackColor
        [CP].Win32.InactiveTitleText = InactivetitleText_pick.BackColor
        [CP].Win32.InfoText = InfoText_pick.BackColor
        [CP].Win32.InfoWindow = InfoWindow_pick.BackColor
        [CP].Win32.Menu = menu_pick.BackColor
        [CP].Win32.MenuBar = menubar_pick.BackColor
        [CP].Win32.MenuText = menutext_pick.BackColor
        [CP].Win32.Scrollbar = Scrollbar_pick.BackColor
        [CP].Win32.TitleText = TitleText_pick.BackColor
        [CP].Win32.Window = Window_pick.BackColor
        [CP].Win32.WindowFrame = Frame_pick.BackColor
        [CP].Win32.WindowText = WindowText_pick.BackColor
        [CP].Win32.Hilight = hilight_pick.BackColor
        [CP].Win32.MenuHilight = menuhilight_pick.BackColor
        [CP].Win32.Desktop = desktop_pick.BackColor
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        ApplyToCP(MainFrm.CP)
        SetClassicWindowColors(MainFrm.CP, MainFrm.ClassicWindow1)
        SetClassicWindowColors(MainFrm.CP, MainFrm.ClassicWindow2, False)
        SetClassicButtonColors(MainFrm.CP, MainFrm.RetroButton2)
        SetClassicButtonColors(MainFrm.CP, MainFrm.RetroButton3)
        SetClassicButtonColors(MainFrm.CP, MainFrm.RetroButton4)
        SetClassicRaisedPanelColors(MainFrm.CP, MainFrm.ClassicTaskbar)
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

        Dim C As Color = sender.BackColor

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
                CList.Add(Retro3DPreview1)

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
                Retro3DPreview1.WindowFrame = C

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
                CList.Add(Retro3DPreview1)
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
                Retro3DPreview1.BackColor = C
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
                CList.Add(Retro3DPreview1)
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
                Retro3DPreview1.ButtonDkShadow = C
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
                CList.Add(Retro3DPreview1)
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
                Retro3DPreview1.ButtonHilight = C

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
                CList.Add(Retro3DPreview1)
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
                Retro3DPreview1.ButtonLight = C

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
                CList.Add(Retro3DPreview1)
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
                Retro3DPreview1.ButtonShadow = C
                RetroTextBox1.ButtonShadow = C
                RetroPanel1.ButtonShadow = C

            Case "btntext_pick"
                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    CList.Add(RW)
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        CList.Add(RB)
                    Next
                Next
                For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
                    CList.Add(RB)
                Next
                CList.Add(Retro3DPreview1)
                Dim _Conditions As New Conditions With {.RetroButtonText = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)

                For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
                    RW.ButtonText = C
                    For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                        RB.ForeColor = C
                    Next
                Next
                For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
                    RB.ForeColor = C
                Next
                Retro3DPreview1.ForeColor = C

            Case "AppWorkspace_pick"
                CList.Add(programcontainer)
                Dim _Conditions As New Conditions With {.RetroAppWorkspace = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
                programcontainer.BackColor = C

            Case "background_pick"
                CList.Add(pnl_preview)
                Dim _Conditions As New Conditions With {.RetroBackground = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
                pnl_preview.BackColor = C

            Case "menu_pick"
                CList.Add(Menu_Window)

                If Not XenonToggle1.Checked Then CList.Add(RetroPanel1)
                If Not XenonToggle1.Checked Then CList.Add(menucontainer0)

                Dim _Conditions As New Conditions With {.RetroBackground = True}
                C = ColorPickerDlg.Pick(CList, _Conditions)
                Menu_Window.BackColor = C
                Menu_Window.Invalidate()

                If Not XenonToggle1.Checked Then RetroPanel1.BackColor = C
                If Not XenonToggle1.Checked Then menucontainer0.BackColor = C

            Case "menubar_pick"
                If XenonToggle1.Checked Then
                    CList.Add(menucontainer0)
                    Dim _Conditions As New Conditions With {.RetroBackground = True}
                    C = ColorPickerDlg.Pick(CList, _Conditions)
                    menucontainer0.BackColor = C
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

        CType(sender, XenonCP).BackColor = C

        For Each Ctrl In CList
            Ctrl.Refresh()
        Next

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
            Using _Def As CP = CP_Defaults.From(My.PreviewStyle)
                LoadFromWin9xTheme(OpenThemeDialog.FileName, _Def.Win32)
            End Using

        End If
    End Sub

    Sub LoadFromWin9xTheme(File As String, _DefWin32 As CP.Structures.Win32UI)
        If IO.File.Exists(File) Then

            Using _ini As New INI(File)
                Dim Section As String = "Control Panel\Colors"

                TitleText_pick.BackColor = _ini.IniReadValue(Section, "TitleText", _DefWin32.TitleText.ToWin32Reg).FromWin32RegToColor
                InactivetitleText_pick.BackColor = _ini.IniReadValue(Section, "InactiveTitleText", _DefWin32.InactiveTitleText.ToWin32Reg).FromWin32RegToColor
                ActiveBorder_pick.BackColor = _ini.IniReadValue(Section, "ActiveBorder", _DefWin32.ActiveBorder.ToWin32Reg).FromWin32RegToColor
                InactiveBorder_pick.BackColor = _ini.IniReadValue(Section, "InactiveBorder", _DefWin32.InactiveBorder.ToWin32Reg).FromWin32RegToColor
                activetitle_pick.BackColor = _ini.IniReadValue(Section, "ActiveTitle", _DefWin32.ActiveTitle.ToWin32Reg).FromWin32RegToColor
                InactiveTitle_pick.BackColor = _ini.IniReadValue(Section, "InactiveTitle", _DefWin32.InactiveTitle.ToWin32Reg).FromWin32RegToColor

                Dim GA As Color = _ini.IniReadValue(Section, "GradientActiveTitle").FromWin32RegToColor
                Dim GI As Color = _ini.IniReadValue(Section, "GradientInactiveTitle").FromWin32RegToColor

                If GA <> Color.Empty Then
                    GActivetitle_pick.BackColor = GA
                Else
                    GActivetitle_pick.BackColor = activetitle_pick.BackColor
                End If

                If GI <> Color.Empty Then
                    GInactivetitle_pick.BackColor = GA
                Else
                    GInactivetitle_pick.BackColor = InactiveTitle_pick.BackColor
                End If

                btnface_pick.BackColor = _ini.IniReadValue(Section, "ButtonFace", _DefWin32.ButtonFace.ToWin32Reg).FromWin32RegToColor
                btnshadow_pick.BackColor = _ini.IniReadValue(Section, "ButtonShadow", _DefWin32.ButtonShadow.ToWin32Reg).FromWin32RegToColor
                btntext_pick.BackColor = _ini.IniReadValue(Section, "ButtonText", _DefWin32.ButtonText.ToWin32Reg).FromWin32RegToColor
                btnhilight_pick.BackColor = _ini.IniReadValue(Section, "ButtonHilight", _DefWin32.ButtonHilight.ToWin32Reg).FromWin32RegToColor
                btndkshadow_pick.BackColor = _ini.IniReadValue(Section, "ButtonDkShadow", _DefWin32.ButtonDkShadow.ToWin32Reg).FromWin32RegToColor
                btnlight_pick.BackColor = _ini.IniReadValue(Section, "ButtonLight", _DefWin32.ButtonLight.ToWin32Reg).FromWin32RegToColor
                btnaltface_pick.BackColor = _ini.IniReadValue(Section, "ButtonAlternateFace", _DefWin32.ButtonAlternateFace.ToWin32Reg).FromWin32RegToColor
                background_pick.BackColor = _ini.IniReadValue(Section, "Background", _DefWin32.Background.ToWin32Reg).FromWin32RegToColor
                hilight_pick.BackColor = _ini.IniReadValue(Section, "Hilight", _DefWin32.Hilight.ToWin32Reg).FromWin32RegToColor
                hilighttext_pick.BackColor = _ini.IniReadValue(Section, "HilightText", _DefWin32.HilightText.ToWin32Reg).FromWin32RegToColor
                Window_pick.BackColor = _ini.IniReadValue(Section, "Window", _DefWin32.Window.ToWin32Reg).FromWin32RegToColor
                WindowText_pick.BackColor = _ini.IniReadValue(Section, "WindowText", _DefWin32.WindowText.ToWin32Reg).FromWin32RegToColor
                Scrollbar_pick.BackColor = _ini.IniReadValue(Section, "ScrollBar", _DefWin32.Scrollbar.ToWin32Reg).FromWin32RegToColor
                menu_pick.BackColor = _ini.IniReadValue(Section, "Menu", _DefWin32.Menu.ToWin32Reg).FromWin32RegToColor
                Frame_pick.BackColor = _ini.IniReadValue(Section, "WindowFrame", _DefWin32.WindowFrame.ToWin32Reg).FromWin32RegToColor
                menutext_pick.BackColor = _ini.IniReadValue(Section, "MenuText", _DefWin32.MenuText.ToWin32Reg).FromWin32RegToColor
                AppWorkspace_pick.BackColor = _ini.IniReadValue(Section, "AppWorkspace", _DefWin32.AppWorkspace.ToWin32Reg).FromWin32RegToColor
                GrayText_pick.BackColor = _ini.IniReadValue(Section, "GrayText", _DefWin32.GrayText.ToWin32Reg).FromWin32RegToColor
                InfoText_pick.BackColor = _ini.IniReadValue(Section, "InfoText", _DefWin32.InfoText.ToWin32Reg).FromWin32RegToColor
                InfoWindow_pick.BackColor = _ini.IniReadValue(Section, "InfoWindow", _DefWin32.InfoWindow.ToWin32Reg).FromWin32RegToColor
                hottracking_pick.BackColor = _ini.IniReadValue(Section, "HotTrackingColor", _DefWin32.HotTrackingColor.ToWin32Reg).FromWin32RegToColor
                menubar_pick.BackColor = _ini.IniReadValue(Section, "MenuBar", _DefWin32.MenuBar.ToWin32Reg).FromWin32RegToColor
                menuhilight_pick.BackColor = _ini.IniReadValue(Section, "MenuHilight", _DefWin32.MenuHilight.ToWin32Reg).FromWin32RegToColor
                desktop_pick.BackColor = _ini.IniReadValue(Section, "Desktop", _DefWin32.Desktop.ToWin32Reg).FromWin32RegToColor
            End Using

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

        Dim AllThemes As List(Of String) = [String].CList
        Dim SelectedTheme As String = ""
        Dim SelectedThemeList As New List(Of String)

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

        SelectedThemeList = SelectedTheme.CList

        Dim FoundGradientActive As Boolean = False
        Dim FoundGradientInactive As Boolean = False

        For Each x As String In SelectedThemeList

            If x.StartsWith("activetitle=", My._ignore) Then
                activetitle_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
                If Not FoundGradientActive Then GActivetitle_pick.BackColor = activetitle_pick.BackColor
            End If

            If x.StartsWith("gradientactivetitle=", My._ignore) Then
                GActivetitle_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
                FoundGradientActive = True
            End If

            If x.StartsWith("inactivetitle=", My._ignore) Then
                InactiveTitle_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
                If Not FoundGradientInactive Then GInactivetitle_pick.BackColor = InactiveTitle_pick.BackColor

            End If

            If x.StartsWith("gradientinactivetitle=", My._ignore) Then
                GInactivetitle_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
                FoundGradientInactive = True
            End If

            If x.StartsWith("background=", My._ignore) Then background_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
            If x.StartsWith("hilight=", My._ignore) Then hilight_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
            If x.StartsWith("hilighttext=", My._ignore) Then hilighttext_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
            If x.StartsWith("titletext=", My._ignore) Then TitleText_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
            If x.StartsWith("window=", My._ignore) Then Window_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
            If x.StartsWith("windowtext=", My._ignore) Then WindowText_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
            If x.StartsWith("scrollbar=", My._ignore) Then Scrollbar_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
            If x.StartsWith("menu=", My._ignore) Then menu_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
            If x.StartsWith("windowframe=", My._ignore) Then Frame_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
            If x.StartsWith("menutext=", My._ignore) Then menutext_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
            If x.StartsWith("activeborder=", My._ignore) Then ActiveBorder_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
            If x.StartsWith("inactiveborder=", My._ignore) Then InactiveBorder_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
            If x.StartsWith("appworkspace=", My._ignore) Then AppWorkspace_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
            If x.StartsWith("buttonface=", My._ignore) Then btnface_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
            If x.StartsWith("buttonshadow=", My._ignore) Then btnshadow_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
            If x.StartsWith("graytext=", My._ignore) Then GrayText_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
            If x.StartsWith("buttontext=", My._ignore) Then btntext_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
            If x.StartsWith("inactivetitletext=", My._ignore) Then InactivetitleText_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
            If x.StartsWith("buttonhilight=", My._ignore) Then btnhilight_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
            If x.StartsWith("buttondkshadow=", My._ignore) Then btndkshadow_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
            If x.StartsWith("buttonlight=", My._ignore) Then btnlight_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
            If x.StartsWith("infotext=", My._ignore) Then InfoText_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
            If x.StartsWith("infowindow=", My._ignore) Then InfoWindow_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
            If x.StartsWith("hottrackingcolor=", My._ignore) Then hottracking_pick.BackColor = x.Split("=")(1).FromWin32RegToColor

            If x.StartsWith("buttonalternateface=", My._ignore) Then btnaltface_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
            If x.StartsWith("menubar=", My._ignore) Then menubar_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
            If x.StartsWith("menuhilight=", My._ignore) Then menuhilight_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
            If x.StartsWith("desktop=", My._ignore) Then desktop_pick.BackColor = x.Split("=")(1).FromWin32RegToColor
        Next

        ApplyRetroPreview()
    End Sub

    Sub SetMetics(CP As CP)
        RetroPanel2.Width = CP.MetricsFonts.ScrollWidth
        menucontainer0.Height = CP.MetricsFonts.MenuHeight

        menucontainer0.Height = Math.Max(CP.MetricsFonts.MenuHeight, Metrics_Fonts.GetTitleTextHeight(CP.MetricsFonts.MenuFont))

        RetroLabel1.Font = CP.MetricsFonts.MenuFont
        RetroLabel2.Font = CP.MetricsFonts.MenuFont
        RetroLabel3.Font = CP.MetricsFonts.MenuFont

        RetroLabel9.Font = CP.MetricsFonts.MenuFont
        RetroLabel5.Font = CP.MetricsFonts.MenuFont
        RetroLabel6.Font = CP.MetricsFonts.MenuFont

        menucontainer1.Height = Metrics_Fonts.GetTitleTextHeight(CP.MetricsFonts.MenuFont) + 3
        highlight.Height = menucontainer1.Height + 1
        menucontainer3.Height = menucontainer1.Height + 1
        Menu_Window.Height = menucontainer1.Height + highlight.Height + menucontainer3.Height + Menu_Window.Padding.Top + Menu_Window.Padding.Bottom

        RetroLabel4.Font = CP.MetricsFonts.MessageFont

        RetroLabel1.Width = RetroLabel1.Text.Measure(CP.MetricsFonts.MenuFont).Width + 5
        RetroLabel2.Width = RetroLabel2.Text.Measure(CP.MetricsFonts.MenuFont).Width + 5
        RetroPanel1.Width = RetroLabel3.Text.Measure(CP.MetricsFonts.MenuFont).Width + 5 + RetroPanel1.Padding.Left + RetroPanel1.Padding.Right

        Dim TitleTextH, TitleTextH_9, TitleTextH_Sum As Integer
        TitleTextH = "ABCabc0123xYz.#".Measure(CP.MetricsFonts.CaptionFont).Height
        TitleTextH_9 = "ABCabc0123xYz.#".Measure(New Font(CP.MetricsFonts.CaptionFont.Name, 9, Font.Style)).Height
        TitleTextH_Sum = Math.Max(0, TitleTextH - TitleTextH_9 - 5)

        Dim iP As Integer = 3 + CP.MetricsFonts.PaddedBorderWidth + CP.MetricsFonts.BorderWidth
        Dim iT As Integer = 4 + CP.MetricsFonts.PaddedBorderWidth + CP.MetricsFonts.BorderWidth + CP.MetricsFonts.CaptionHeight + TitleTextH_Sum
        Dim _Padding As New Padding(iP, iT, iP, iP)

        For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
            If Not RW.UseItAsMenu Then
                SetClassicWindowMetrics(CP, RW)
                RW.Padding = _Padding
            End If
        Next

        RetroWindow3.Height = 85 + CP.MetricsFonts.PaddedBorderWidth + CP.MetricsFonts.BorderWidth + RetroWindow3.GetTitleTextHeight
        RetroWindow2.Height = 120 + CP.MetricsFonts.PaddedBorderWidth + CP.MetricsFonts.BorderWidth + RetroWindow2.GetTitleTextHeight + CP.MetricsFonts.MenuHeight


        Menu_Window.Top = RetroWindow2.Top + menucontainer0.Top + menucontainer0.Height
        Menu_Window.Left = Math.Min(RetroWindow2.Left + menucontainer0.Left + RetroPanel1.Left + +3, RetroWindow2.Right - CP.MetricsFonts.PaddedBorderWidth - CP.MetricsFonts.BorderWidth)

        RetroWindow3.Top = RetroWindow2.Top + RetroTextBox1.Top + RetroTextBox1.Font.Height + 10
        RetroWindow3.Left = RetroWindow2.Left + RetroTextBox1.Left + 15

        RetroLabel13.Top = RetroWindow4.Top + RetroWindow4.Metrics_CaptionHeight + 2
        RetroLabel13.Left = RetroWindow4.Right - RetroWindow4.Metrics_CaptionWidth - 2

        RetroShadow1.Visible = CP.WindowsEffects.WindowShadow

        RetroButton1.FocusRectWidth = CP.WindowsEffects.FocusRectWidth
        RetroButton1.FocusRectHeight = CP.WindowsEffects.FocusRectHeight
        RetroButton1.Refresh()
    End Sub

    Sub ApplyRetroPreview()
        RetroWindow1.ColorGradient = XenonToggle2.Checked
        RetroWindow2.ColorGradient = XenonToggle2.Checked
        RetroWindow3.ColorGradient = XenonToggle2.Checked
        RetroWindow4.ColorGradient = XenonToggle2.Checked

        RetroWindow2.Color1 = activetitle_pick.BackColor
        RetroWindow3.Color1 = activetitle_pick.BackColor
        RetroWindow4.Color1 = activetitle_pick.BackColor

        RetroWindow2.Color2 = GActivetitle_pick.BackColor
        RetroWindow3.Color2 = GActivetitle_pick.BackColor
        RetroWindow4.Color2 = GActivetitle_pick.BackColor

        RetroWindow2.ForeColor = TitleText_pick.BackColor
        RetroWindow3.ForeColor = TitleText_pick.BackColor
        RetroWindow4.ForeColor = TitleText_pick.BackColor

        RetroWindow1.Color1 = InactiveTitle_pick.BackColor
        RetroWindow1.Color2 = GInactivetitle_pick.BackColor
        RetroWindow1.ForeColor = InactivetitleText_pick.BackColor

        RetroWindow2.ColorBorder = ActiveBorder_pick.BackColor
        RetroWindow3.ColorBorder = ActiveBorder_pick.BackColor
        RetroWindow4.ColorBorder = ActiveBorder_pick.BackColor

        RetroWindow1.ColorBorder = InactiveBorder_pick.BackColor

        Retro3DPreview1.WindowFrame = Frame_pick.BackColor

        For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
            If RW IsNot Menu Then RW.BackColor = btnface_pick.BackColor
            RW.ButtonDkShadow = btndkshadow_pick.BackColor
            RW.ButtonHilight = btnhilight_pick.BackColor
            RW.ButtonLight = btnlight_pick.BackColor
            RW.ButtonShadow = btnshadow_pick.BackColor
            RW.ButtonText = btntext_pick.BackColor

            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.BackColor = btnface_pick.BackColor
                RB.WindowFrame = Frame_pick.BackColor
                RB.ButtonDkShadow = btndkshadow_pick.BackColor
                RB.ButtonHilight = btnhilight_pick.BackColor
                RB.ButtonLight = btnlight_pick.BackColor
                RB.ButtonShadow = btnshadow_pick.BackColor
                RB.ForeColor = btntext_pick.BackColor
            Next
        Next

        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.BackColor = btnface_pick.BackColor
            RB.WindowFrame = Frame_pick.BackColor
            RB.ButtonDkShadow = btndkshadow_pick.BackColor
            RB.ButtonHilight = btnhilight_pick.BackColor
            RB.ButtonLight = btnlight_pick.BackColor
            RB.ButtonShadow = btnshadow_pick.BackColor
            RB.ForeColor = btntext_pick.BackColor
        Next

        For Each RB As RetroPanelRaised In pnl_preview.Controls.OfType(Of RetroPanelRaised)
            RB.ButtonHilight = btnhilight_pick.BackColor
            RB.ButtonShadow = btnshadow_pick.BackColor
        Next

        RetroPanel2.BackColor = btnface_pick.BackColor
        Menu_Window.ButtonFace = btnface_pick.BackColor
        Retro3DPreview1.BackColor = btnface_pick.BackColor

        RetroTextBox1.ButtonDkShadow = btndkshadow_pick.BackColor
        Menu_Window.ButtonDkShadow = btndkshadow_pick.BackColor
        Retro3DPreview1.ButtonDkShadow = btndkshadow_pick.BackColor

        RetroTextBox1.ButtonHilight = btnhilight_pick.BackColor
        RetroPanel1.ButtonHilight = btnhilight_pick.BackColor
        RetroPanel2.ButtonHilight = btnhilight_pick.BackColor
        Menu_Window.ButtonHilight = btnhilight_pick.BackColor
        Retro3DPreview1.ButtonHilight = btnhilight_pick.BackColor

        RetroTextBox1.ButtonLight = btnlight_pick.BackColor
        Menu_Window.ButtonLight = btnlight_pick.BackColor
        Retro3DPreview1.ButtonLight = btnlight_pick.BackColor

        RetroTextBox1.ButtonShadow = btnshadow_pick.BackColor
        RetroPanel1.ButtonShadow = btnshadow_pick.BackColor
        RetroTextBox1.Refresh()
        Menu_Window.ButtonShadow = btnshadow_pick.BackColor
        Retro3DPreview1.ButtonShadow = btnshadow_pick.BackColor

        Retro3DPreview1.ForeColor = btntext_pick.BackColor

        programcontainer.BackColor = AppWorkspace_pick.BackColor

        pnl_preview.BackColor = background_pick.BackColor

        Menu_Window.BackColor = menu_pick.BackColor
        RetroPanel1.BackColor = menu_pick.BackColor
        Menu_Window.Refresh()

        menucontainer0.BackColor = menubar_pick.BackColor

        highlight.BackColor = hilight_pick.BackColor

        menuhilight.BackColor = menuhilight_pick.BackColor

        RetroLabel6.ForeColor = menutext_pick.BackColor
        RetroLabel1.ForeColor = menutext_pick.BackColor

        RetroLabel5.ForeColor = hilighttext_pick.BackColor

        RetroLabel2.ForeColor = GrayText_pick.BackColor
        RetroLabel9.ForeColor = GrayText_pick.BackColor

        RetroTextBox1.BackColor = Window_pick.BackColor

        RetroTextBox1.ForeColor = WindowText_pick.BackColor
        RetroLabel4.ForeColor = WindowText_pick.BackColor

        RetroLabel13.BackColor = InfoWindow_pick.BackColor

        RetroLabel13.ForeColor = InfoText_pick.BackColor

        For Each RW As RetroWindow In pnl_preview.Controls.OfType(Of RetroWindow)
            RW.Refresh()
            For Each RB As RetroButton In RW.Controls.OfType(Of RetroButton)
                RB.Refresh()
            Next
        Next

        For Each RB As RetroButton In RetroPanel2.Controls.OfType(Of RetroButton)
            RB.Refresh()
        Next

        Refresh17BitPreference()

        RetroShadow1.Refresh()
        Retro3DPreview1.Refresh()
    End Sub

    Private Sub XenonToggle1_CheckedChanged(sender As Object, e As EventArgs) Handles XenonToggle1.CheckedChanged
        Refresh17BitPreference()
    End Sub

    Sub Refresh17BitPreference()

        If XenonToggle1.Checked Then
            'Theming Enabled (Menus Has colors and borders)
            Menu_Window.Flat = True
            RetroPanel1.Flat = True
            menuhilight.BackColor = menuhilight_pick.BackColor  'Filling of selected item
            highlight.BackColor = hilight_pick.BackColor        'Outer Border of selected item

            RetroPanel1.BackColor = menuhilight_pick.BackColor
            RetroPanel1.ButtonShadow = hilight_pick.BackColor

            menucontainer0.BackColor = menubar_pick.BackColor
            RetroLabel3.ForeColor = hilighttext_pick.BackColor
        Else
            'Theming Disabled (Menus are retro 3D)
            Menu_Window.Flat = False
            RetroPanel1.Flat = False
            menuhilight.BackColor = hilight_pick.BackColor      'Both will have same color
            highlight.BackColor = hilight_pick.BackColor        'Both will have same color
            RetroPanel1.BackColor = menu_pick.BackColor
            RetroPanel1.ButtonShadow = btnshadow_pick.BackColor
            menucontainer0.BackColor = menu_pick.BackColor
            RetroLabel3.ForeColor = menutext_pick.BackColor

        End If

        Menu_Window.Refresh()
        RetroPanel1.Refresh()
        menuhilight.Refresh()
        highlight.Refresh()

    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim cpx As New CP(CP.CP_Type.File, OpenFileDialog1.FileName)
            LoadCP(cpx)
            cpx.Dispose()
        End If
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        Dim cpx As New CP(CP.CP_Type.Registry)
        LoadCP(cpx)
        cpx.Dispose()
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
        Cursor = Cursors.WaitCursor
        Dim CPx As New CP(CP.CP_Type.Registry)
        ApplyToCP(CPx)
        ApplyToCP(MainFrm.CP)

        SetClassicWindowColors(MainFrm.CP, MainFrm.ClassicWindow1)
        SetClassicWindowColors(MainFrm.CP, MainFrm.ClassicWindow2, False)
        SetClassicButtonColors(MainFrm.CP, MainFrm.RetroButton2)
        SetClassicButtonColors(MainFrm.CP, MainFrm.RetroButton3)
        SetClassicButtonColors(MainFrm.CP, MainFrm.RetroButton4)
        SetClassicRaisedPanelColors(MainFrm.CP, MainFrm.ClassicTaskbar)

        Try
            CPx.Win32.Apply()
            CPx.Win32.Update_UPM_DEFAULT()
        Catch
        End Try
        CPx.Dispose()
        Cursor = Cursors.Default
    End Sub

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click
        If SaveFileDialog2.ShowDialog = DialogResult.OK Then
            Dim s As New List(Of String)
            s.Clear()
            s.Add("; " & String.Format(My.Lang.OldMSTheme_Copyrights, Now.Year))
            s.Add("; " & String.Format(My.Lang.OldMSTheme_ProgrammedBy, My.Application.Info.CompanyName))
            s.Add("; " & String.Format(My.Lang.OldMSTheme_CreatedFromAppVer, MainFrm.CP.Info.AppVersion))
            s.Add("; " & String.Format(My.Lang.OldMSTheme_CreatedBy, MainFrm.CP.Info.Author))
            s.Add("; " & String.Format(My.Lang.OldMSTheme_ThemeName, MainFrm.CP.Info.ThemeName))
            s.Add("; " & String.Format(My.Lang.OldMSTheme_ThemeVersion, MainFrm.CP.Info.ThemeVersion))
            s.Add("")

            s.Add(String.Format("[Control Panel\Colors]"))

            With activetitle_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "ActiveTitle", .R, .G, .B)) : End With
            With background_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "Background", .R, .G, .B)) : End With
            With hilight_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "Hilight", .R, .G, .B)) : End With
            With hilighttext_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "HilightText", .R, .G, .B)) : End With
            With TitleText_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "TitleText", .R, .G, .B)) : End With
            With Window_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "Window", .R, .G, .B)) : End With
            With WindowText_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "WindowText", .R, .G, .B)) : End With
            With Scrollbar_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "Scrollbar", .R, .G, .B)) : End With
            With InactiveTitle_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "InactiveTitle", .R, .G, .B)) : End With
            With menu_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "Menu", .R, .G, .B)) : End With
            With Frame_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "WindowFrame", .R, .G, .B)) : End With
            With menutext_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "MenuText", .R, .G, .B)) : End With
            With ActiveBorder_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "ActiveBorder", .R, .G, .B)) : End With
            With InactiveBorder_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "InactiveBorder", .R, .G, .B)) : End With
            With AppWorkspace_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "AppWorkspace", .R, .G, .B)) : End With
            With btnface_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "ButtonFace", .R, .G, .B)) : End With
            With btnshadow_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "ButtonShadow", .R, .G, .B)) : End With
            With GrayText_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "GrayText", .R, .G, .B)) : End With
            With btntext_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "ButtonText", .R, .G, .B)) : End With
            With InactivetitleText_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "InactiveTitleText", .R, .G, .B)) : End With
            With btnhilight_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "ButtonHilight", .R, .G, .B)) : End With
            With btndkshadow_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "ButtonDkShadow", .R, .G, .B)) : End With
            With btnlight_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "ButtonLight", .R, .G, .B)) : End With
            With InfoText_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "InfoText", .R, .G, .B)) : End With
            With InfoWindow_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "InfoWindow", .R, .G, .B)) : End With
            With GActivetitle_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "GradientActiveTitle", .R, .G, .B)) : End With
            With GInactivetitle_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "GradientInactiveTitle", .R, .G, .B)) : End With
            With btnaltface_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "ButtonAlternateFace", .R, .G, .B)) : End With
            With hottracking_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "HotTrackingColor", .R, .G, .B)) : End With
            With menuhilight_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "MenuHilight", .R, .G, .B)) : End With
            With menubar_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "MenuBar", .R, .G, .B)) : End With
            With desktop_pick.BackColor : s.Add(String.Format("{0}={1} {2} {3}", "Desktop", .R, .G, .B)) : End With
            s.Add("")

            s.Add(String.Format("[MasterThemeSelector]"))
            s.Add(String.Format("MTSM=DABJDKT"))

            s.Add("")
            s.Add("[Control Panel\Desktop]")
            s.Add("Wallpaper=")
            s.Add("TileWallpaper=0")
            s.Add("WallpaperStyle=10")
            s.Add("Pattern=")
            s.Add("")

            s.Add("[VisualStyles]")
            s.Add("Path=")
            s.Add("ColorStyle=@themeui.dll,-854")
            s.Add("Size=@themeui.dll,-2019")
            s.Add("Transparency=0")
            s.Add("")

            Try
                IO.File.WriteAllText(SaveFileDialog2.FileName, s.CString)
            Catch ex As Exception
                BugReport.ThrowError(ex)
            End Try

        End If
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        VS2Win32UI.ShowDialog()
    End Sub

    Private Sub XenonComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles XenonComboBox1.SelectedIndexChanged
        If String.IsNullOrWhiteSpace(XenonComboBox1.SelectedItem) Then Exit Sub

        Dim condition0 As Boolean = (XenonComboBox1.SelectedIndex <= 3)
        Dim condition1 As Boolean = XenonComboBox1.SelectedItem.ToString.StartsWith("Windows Classic (3.1)")
        Dim condition2 As Boolean = XenonComboBox1.SelectedItem.ToString.StartsWith("Windows 3.1 - ")

        XenonToggle1.Checked = condition0 Or condition1 Or condition2

        LoadFromWinThemeString(My.Resources.RetroThemesDB, XenonComboBox1.SelectedItem)
    End Sub

    Private Sub Menu_Window_SizeChanged(sender As Object, e As EventArgs) Handles Menu_Window.SizeChanged, Menu_Window.LocationChanged
        RetroShadow1.Size = Menu_Window.Size
        RetroShadow1.Location = Menu_Window.Location + New Point(6, 5)

        Dim b As New Bitmap(RetroShadow1.Width, RetroShadow1.Height)
        Dim g As Graphics = Graphics.FromImage(b)
        g.DrawGlow(New Rectangle(5, 5, b.Width - 10 - 1, b.Height - 10 - 1), Color.FromArgb(128, 0, 0, 0))
        g.Save()
        RetroShadow1.Image = b
        g.Dispose()

        RetroShadow1.BringToFront()
        Menu_Window.BringToFront()
    End Sub

    Private Sub XenonButton9_Click(sender As Object, e As EventArgs) Handles XenonButton9.Click
        btndkshadow_pick.BackColor = btnface_pick.BackColor
        btnshadow_pick.BackColor = btnface_pick.BackColor
        btnhilight_pick.BackColor = btnface_pick.BackColor
        btnlight_pick.BackColor = btnface_pick.BackColor
        ApplyRetroPreview()
    End Sub

    Private Sub XenonButton11_Click(sender As Object, e As EventArgs) Handles XenonButton11.Click
        btndkshadow_pick.BackColor = btnshadow_pick.BackColor
        btnhilight_pick.BackColor = btnshadow_pick.BackColor
        btnlight_pick.BackColor = btnface_pick.BackColor
        btnshadow_pick.BackColor = btnface_pick.BackColor
        ApplyRetroPreview()
    End Sub
End Class