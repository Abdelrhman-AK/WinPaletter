
Imports System.Reflection
Imports WinPaletter.XenonCore

Public Class Localizer
    Sub New()

    End Sub

#Region "Language Info"
    Property Name
    Property TrVer
    Property Lang
    Property LangCode
    Property AppVer
    Property RightToLeft As Boolean = False
#End Region

#Region "Deep-In-Code Strings"
    Property OK As String
    Property Yes As String
    Property No As String
    Property By As String
    Property Next_ As String
    Property NewUpdate As String
    Property OpenForActions As String
    Property X1 As String
    Property X2 As String
    Property X3 As String
    Property X4 As String
    Property X5 As String
    Property X6 As String
    Property X7 As String
    Property X8 As String
    Property X9 As String
    Property X10 As String
    Property X11 As String
    Property X12 As String
    Property X13 As String
    Property X14 As String
    Property X15 As String
    Property X16 As String
    Property X17 As String
    Property X18 As String
    Property X19 As String
    Property X20 As String
    Property X21 As String
    Property X22 As String
    Property X23 As String
    Property NoDefResExplorer As String
    Property CurrentMode As String
    Property SaveMsg As String
    Property SettingsSaved As String
    Property RemoveExtMsg As String
    Property RemoveExtMsgNote As String
    Property UninstallMsgLine1 As String
    Property UninstallMsgLine2 As String
    Property RestartRecommendation As String
    Property EmptyName As String
    Property EmptyAuthorName As String
    Property EmptyVer As String
    Property WrongVerFormat As String
    Property Extracting As String
    Property Sorting As String
    Property ErrorPhrasingChangelog As String
    Property VersionNotReleased As String
    Property ReleasedOn As String
    Property Version As String
    Property Label5_Checking As String
    Property Error_Online As String
    Property NoNetwork As String
    Property CheckConnection As String
    Property XenonButton1_UpdateAvaliable As String
    Property XenonAlertBox2_UpdateAvaliable As String
    Property XenonButton1_NoUpdateAvaliable As String
    Property XenonAlertBox2_NoUpdateAvaliable As String
    Property XenonButton1_Error As String
    Property XenonAlertBox2_Error As String
    Property XenonButton1_ServerError As String
    Property XenonAlertBox2_ServerError As String
    Property Msgbox_Downloaded As String
    Property MBSizeUnit As String
    Property Stable As String
    Property Beta As String
    Property Channel As String
    Property LngExported As String
    Property InvalidTheme As String
    Property ThemeNotExist As String
    Property RescueBoxAutoClose As String
    Property RescueBox As String
    Property NewTag As String
    Property OpenTag As String
    Property SaveThemeTag As String
    Property SaveThemeAsTag As String
    Property UndoTag As String
    Property UltraUndoTag As String
    Property EditTag As String
    Property SettingsTag As String
    Property UpdatesTag As String
    Property WhatsnewTag As String
    Property AboutTag As String
    Property MenuNativeWin As String
    Property MenuInit As String
    Property MenuAppliedReg As String
    Property ScalingTip As String

#End Region

    Public Sub ExportNativeLang(File As String)
        Dim LS As New List(Of String)
        LS.Clear()

        For Each f In Assembly.GetExecutingAssembly().GetTypes().Where(Function(t) GetType(Form).IsAssignableFrom(t))

            Using ins = DirectCast(Activator.CreateInstance(f), Form)
                LS.Add(ins.Name & ".Text = " & ins.Text)
                For Each ctrl In GetAllControls(ins)
                    If Not String.IsNullOrWhiteSpace(ctrl.Text) And Not IsNumeric(ctrl.Text) And Not ctrl.Text.Count = 1 And Not ctrl.Text = ctrl.Name Then
                        LS.Add(ins.Name & "." & ctrl.Name & ".Text = " & ctrl.Text.Replace(vbCrLf, "<br>"))
                    End If

                    If Not String.IsNullOrWhiteSpace(ctrl.Tag) Then
                        LS.Add(ins.Name & "." & ctrl.Name & ".Tag = " & ctrl.Tag.Replace(vbCrLf, "<br>"))
                    End If
                Next
            End Using
        Next

        Dim lx As New List(Of String)
        lx.Add("!Name = Abdelrhman-AK")
        lx.Add("!TrVer = 1.0")
        lx.Add("!Lang = English")
        lx.Add("!LangCode = EN-US")
        lx.Add("!AppVer = " & My.Application.Info.Version.ToString)
        lx.Add("!RightToLeft = False")

        IO.File.WriteAllText(File, CStr_FromList(lx) & vbCrLf & My.Resources.CodeStr & vbCrLf & CStr_FromList(LS))
    End Sub

    Public Sub LoadLanguageFromFile(File As String, Optional [_Form] As Form = Nothing)
        If IO.File.Exists(File) Then
            Dim Dic As New List(Of ControlsBase)
            Dic.Clear()

            For Each X As String In IO.File.ReadAllLines(File)
                If X.StartsWith("!") Then
                    If X.StartsWith("!Name = ") Then Name = X.Remove(0, "!Name = ".Count)
                    If X.StartsWith("!TrVer = ") Then TrVer = X.Remove(0, "!TrVer = ".Count)
                    If X.StartsWith("!Lang = ") Then Lang = X.Remove(0, "!Lang = ".Count)
                    If X.StartsWith("!LangCode = ") Then LangCode = X.Remove(0, "!LangCode = ".Count)
                    If X.StartsWith("!AppVer = ") Then AppVer = X.Remove(0, "!AppVer = ".Count)
                    If X.StartsWith("!RightToLeft = ") Then RightToLeft = X.Remove(0, "!RightToLeft = ".Count)

                ElseIf X.StartsWith("@") Then
                    Dim x0, x1 As String
                    x0 = X.Split("=")(0).Trim
                    x1 = X.Split("=")(1).Trim
                    x1 = x1

                    Dim type1 As Type = [GetType]() : Dim properties1 As PropertyInfo() = type1.GetProperties()

                    For Each [property] As PropertyInfo In properties1
                        If [property].Name = x0.Remove(0, 1) Then
                            [property].SetValue(Me, Convert.ChangeType(x1.Replace("<br>", vbCrLf), [property].PropertyType), Nothing)
                        End If
                    Next

                Else
                    Dim FormName, ControlName, Prop, Value As String


                    Select Case X.Split("=")(0).Trim.Split(".").Count
                        Case 3
                            FormName = X.Split("=")(0).Trim.Split(".")(0)
                            ControlName = X.Split("=")(0).Trim.Split(".")(1)
                            Prop = X.Split("=")(0).Trim.Split(".")(2)
                        Case 2
                            FormName = X.Split("=")(0).Trim.Split(".")(0)
                            ControlName = Nothing
                            Prop = X.Split("=")(0).Trim.Split(".")(1)
                    End Select

                    Value = X.Split("=")(1).Trim

                    Dic.Add(New ControlsBase(FormName, ControlName, Prop, Value))
                End If
            Next

            If [_Form] Is Nothing Then
                For x As Integer = 0 To My.Application.allForms.Count - 1
                    Populate(Dic, My.Application.allForms(x))
                Next
            Else
                Populate(Dic, [_Form])
            End If

        End If
    End Sub

    Sub LoadInternal()
        Dim ls As New List(Of String)
        CList_FromStr(ls, My.Resources.CodeStr)

        For Each X As String In ls
            Dim x0, x1 As String
            x0 = X.Split("=")(0).Trim
            x1 = X.Split("=")(1).Trim

            Dim type1 As Type = [GetType]() : Dim properties1 As PropertyInfo() = type1.GetProperties()

            For Each [property] As PropertyInfo In properties1
                If [property].Name = x0.Remove(0, 1) Then
                    [property].SetValue(Me, Convert.ChangeType(x1, [property].PropertyType), Nothing)
                End If
            Next
        Next

        MainFrm.ToolStripMenuItem2.Text = MenuInit
        MainFrm.FromCurrentPaletteToolStripMenuItem.Text = MenuAppliedReg

        My.Application.AdjustFonts()
    End Sub

    Sub Populate(ByVal Dic As List(Of ControlsBase), [Form] As Form)
        [Form].SuspendLayout()

        For Each dicX As ControlsBase In Dic

            If [Form].Name = dicX.Form Then
                If dicX.Control = Nothing Then
                    '# Form
                    If dicX.Prop.ToLower = "text" Then [Form].Text = dicX.Value
                    If dicX.Prop.ToLower = "tag" Then [Form].Tag = dicX.Value
                    [Form].RightToLeft = If(RightToLeft, 1, 0)
                    [Form].RightToLeftLayout = RightToLeft
                    RTL([Form])
                    [Form].Refresh()

                Else
                    '# Control
                    For Each ctrl As Control In [Form].Controls.Find(dicX.Control, True)
                        ctrl.SuspendLayout()
                        If dicX.Prop.ToLower = "text" Then ctrl.Text = dicX.Value.ToString.Replace("<br>", vbCrLf)
                        If dicX.Prop.ToLower = "tag" Then ctrl.Tag = dicX.Value.ToString.Replace("<br>", vbCrLf)
                        ctrl.RightToLeft = If(RightToLeft, 1, 0)
                        ctrl.Refresh()
                        [Form].Refresh()
                        ctrl.ResumeLayout()
                    Next

                End If
            End If

        Next

        MainFrm.ToolStripMenuItem2.Text = MenuInit
        MainFrm.FromCurrentPaletteToolStripMenuItem.Text = MenuAppliedReg

        My.Application.AdjustFonts()

        [Form].ResumeLayout()
    End Sub
    Sub RTL(Parent As Control)
        If RightToLeft Then

            For Each XeTP As XenonTabControl In Parent.Controls.OfType(Of XenonTabControl)
                XeTP.RightToLeft = If(RightToLeft, 1, 0)
                XeTP.RightToLeftLayout = RightToLeft

                For i = 0 To XeTP.TabPages.Count - 1
                    XeTP.TabPages.Item(i).RightToLeft = If(RightToLeft, 1, 0)
                    If XeTP.TabPages.Item(i).HasChildren Then RTL(XeTP.TabPages.Item(i))

                    For Each Cx As Control In XeTP.TabPages.Item(i).Controls
                        Cx.Left = XeTP.TabPages.Item(i).Width - Cx.Left - Cx.Width
                        If Cx.HasChildren Then RTL(Cx)
                    Next
                Next
            Next

            For Each XeTP As Control In Parent.Controls
                If TypeOf XeTP Is XenonGroupBox Or TypeOf XeTP Is Panel Then
                    XeTP.RightToLeft = If(RightToLeft, 1, 0)
                    For Each Cx As Control In XeTP.Controls
                        Cx.Left = XeTP.Width - Cx.Left - Cx.Width
                        If Cx.HasChildren Then RTL(Cx)
                    Next
                End If
            Next

            My.Application.AdjustFonts()

        End If
    End Sub

    Private Function GetAllControls(parent As Control) As IEnumerable(Of Control)
        Dim cs = parent.Controls.OfType(Of Control)
        Return cs.SelectMany(Function(c) GetAllControls(c)).Concat(cs)
    End Function

End Class

Public Class ControlsBase
    Public Sub New(Form As String, Control As String, Prop As String, Value As Object)
        Me.Form = Form
        Me.Control = Control
        Me.Prop = Prop
        Me.Value = Value
    End Sub
    Public Property Form As String
    Public Property Control As String
    Public Property Prop As String
    Public Property Value As Object

End Class
