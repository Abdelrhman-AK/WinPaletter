Imports System.Reflection
Imports System.Runtime.InteropServices
Imports Ookii.Dialogs.WinForms
Imports WinPaletter.NativeMethods

Public Class WPStyle

    Structure Colors_Structure
        Public BaseColor As Color
        Public Border As Color
        Public Border_Checked As Color
        Public Border_Checked_Hover As Color

        Public Back As Color
        Public Back_Checked As Color
        Public Core As Color
        Public Back_Hover As Color              ''''''''''''''''''''''''''''''''''''''
        Public Border_Hover As Color            ''''''''''''''''''''''''''''''''''''''

        Public NotTranslatedColor As Color
        Public SearchColor As Color
    End Structure

    Public Colors As New Colors_Structure With {
                    .BaseColor = My.DefaultAccent,
                    .Core = Colors.BaseColor.LightLight,
                    .Back = Color.FromArgb(40, 40, 40),
                    .Back_Hover = Color.FromArgb(55, 55, 55),
                    .Back_Checked = Colors.BaseColor.Dark(0.2),
                    .Border = Color.FromArgb(55, 55, 55),
                    .Border_Hover = Color.FromArgb(65, 65, 65),
                    .Border_Checked = Colors.BaseColor.CB(0.08),
                    .Border_Checked_Hover = Colors.BaseColor.CB(-0.2)
                }

    Public Disabled_Colors As New Colors_Structure

    Public DarkMode As Boolean = True

    Sub New(BaseColor As Color, BackColor As Color, Dark As Boolean)
        DarkMode = Dark
        Colors.BaseColor = BaseColor
        Colors.NotTranslatedColor = If(DarkMode, Color.FromArgb(125, 20, 30), Color.FromArgb(255, 136, 127))
        Colors.SearchColor = If(DarkMode, Color.FromArgb(4, 94, 53), Color.FromArgb(255, 255, 163))

        If DarkMode Then
            Colors.Core = Colors.BaseColor.LightLight

            Colors.Back = BackColor.CB(0.08)
            Colors.Back_Hover = BackColor.CB(0.2)

            Colors.Back_Checked = Colors.BaseColor.Dark(0.3)

            Colors.Border = BackColor.CB(0.05)
            Colors.Border_Hover = BackColor.CB(0.1)

            Colors.Border_Checked = Colors.BaseColor.CB(-0.2)
            Colors.Border_Checked_Hover = Colors.BaseColor.CB(0.08)

        Else
            Colors.Core = Colors.BaseColor.Light(0.5)

            Colors.Back = BackColor.CB(-0.15)
            Colors.Back_Hover = BackColor.CB(-0.2)

            Colors.Back_Checked = Colors.BaseColor.CB(0.6)

            Colors.Border = BackColor.CB(-0.05)
            Colors.Border_Hover = BackColor.CB(-0.1)

            Colors.Border_Checked = Colors.BaseColor.CB(0.5)
            Colors.Border_Checked_Hover = Colors.BaseColor.CB(0.3)
        End If

        If DarkMode Then
            Disabled_Colors.Back_Checked = Color.FromArgb(80, 80, 80)
            Disabled_Colors.Core = Color.FromArgb(90, 90, 90)
            Disabled_Colors.Border_Checked_Hover = Color.FromArgb(80, 80, 80)
            Disabled_Colors.Border = Color.FromArgb(90, 90, 90)
            Disabled_Colors.Back = Color.FromArgb(80, 80, 80)
        Else
            Disabled_Colors.Back_Checked = Color.FromArgb(180, 180, 180)
            Disabled_Colors.Core = Color.FromArgb(190, 190, 190)
            Disabled_Colors.Border_Checked_Hover = Color.FromArgb(180, 180, 180)
            Disabled_Colors.Border = Color.FromArgb(190, 190, 190)
            Disabled_Colors.Back = Color.FromArgb(180, 180, 180)
        End If

    End Sub


#Region "Helpers"
    Public Shared Function GetRoundedCorners() As Boolean
        Try
            If My.Settings.Appearance.ManagedByTheme AndAlso My.Settings.Appearance.CustomColors Then
                Return My.Settings.Appearance.RoundedCorners
            Else
                If System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Designtime Then
                    Return False
                Else
                    If My.W11 Then
                        Return True
                    ElseIf My.W10 Or My.W8 Or My.W81 Then
                        Return False
                    ElseIf My.W7 OrElse My.WXP OrElse My.WVista Then
                        Return Not My.StartedWithClassicTheme
                    Else
                        Return False
                    End If
                End If
            End If
        Catch
            Return False
        End Try
    End Function

    Public Shared Sub FetchDarkMode()
        Try
            If My.Settings.Appearance.ManagedByTheme AndAlso My.Settings.Appearance.CustomColors Then
                My.Style.DarkMode = My.Settings.Appearance.CustomTheme

            Else
                Dim i As Long

                If System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Designtime Then
                    My.Style.DarkMode = True
                Else
                    Try
                        If My.Settings.Appearance.AutoDarkMode Then
                            If My.W11 Or My.W10 Then
                                Try
                                    i = CLng(My.Computer.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Themes\Personalize").GetValue("AppsUseLightTheme", 0))
                                    My.Style.DarkMode = Not (i = 1)
                                Catch ex As Exception
                                    Try
                                        My.Style.DarkMode = My.Settings.Appearance.DarkMode
                                    Catch
                                        My.Style.DarkMode = My.W11 Or My.W10
                                    End Try
                                Finally
                                    My.Computer.Registry.CurrentUser.Close()
                                End Try
                            Else
                                My.Style.DarkMode = False
                            End If
                        Else
                            My.Style.DarkMode = My.Settings.Appearance.DarkMode
                        End If
                    Catch
                        Try
                            My.Style.DarkMode = My.Settings.Appearance.DarkMode
                        Catch
                            My.Style.DarkMode = My.W11 Or My.W10
                        End Try
                    End Try
                End If
            End If
        Catch ex As Exception
            My.Style.DarkMode = True
        End Try
    End Sub

    Public Shared Sub ApplyStyle(Optional [Form] As Form = Nothing, Optional IgnoreTitleBar As Boolean = False)
        Dim DarkMode As Boolean
        Dim BackColor As Color
        Dim AccentColor As Color
        Dim CustomR As Boolean = False

        If My.Settings.Appearance.ManagedByTheme AndAlso My.Settings.Appearance.CustomColors Then
            BackColor = My.Settings.Appearance.BackColor
            AccentColor = My.Settings.Appearance.AccentColor
            DarkMode = My.Settings.Appearance.CustomTheme
            CustomR = My.W11
        Else
            DarkMode = My.Style.DarkMode  'Must be before BackColor
            BackColor = If(DarkMode, My.DefaultBackColorDark, My.DefaultBackColorLight)
            AccentColor = My.DefaultAccent
            CustomR = False
        End If

        My.Style = New WPStyle(AccentColor, BackColor, DarkMode)

        If Form Is Nothing Then

            '####################### For all open forms
            Try
                For Each OFORM As Form In Application.OpenForms
                    Dim FormWasVisible As Boolean = OFORM.Visible
                    If FormWasVisible Then OFORM.Visible = False
                    OFORM.SuspendLayout()
                    OFORM.BackColor = BackColor
                    If Not IgnoreTitleBar Then DLLFunc.DarkTitlebar(OFORM.Handle, DarkMode)
                    ApplyStyleToSubControls(OFORM, DarkMode)

                    If My.W11 Then Dwmapi.DwmSetWindowAttribute(OFORM.Handle, Dwmapi.DWMATTRIB.WINDOW_CORNER_PREFERENCE, CInt(Dwmapi.FormCornersType.Default), Marshal.SizeOf(GetType(Integer)))
                    If CustomR And Not My.Settings.Appearance.RoundedCorners Then Dwmapi.DwmSetWindowAttribute(OFORM.Handle, Dwmapi.DWMATTRIB.WINDOW_CORNER_PREFERENCE, CInt(Dwmapi.FormCornersType.Rectangular), Marshal.SizeOf(GetType(Integer)))

                    If FormWasVisible Then OFORM.Visible = True
                    OFORM.ResumeLayout()
                    OFORM.Refresh()
                Next
            Catch

            End Try

        Else
            '####################### For Selected [Form]
            [Form].BackColor = BackColor

            If Not IgnoreTitleBar Then DLLFunc.DarkTitlebar([Form].Handle, DarkMode)
            ApplyStyleToSubControls([Form], DarkMode)

            If My.W11 Then Dwmapi.DwmSetWindowAttribute([Form].Handle, Dwmapi.DWMATTRIB.WINDOW_CORNER_PREFERENCE, CInt(Dwmapi.FormCornersType.Default), Marshal.SizeOf(GetType(Integer)))
            If CustomR And Not My.Settings.Appearance.RoundedCorners Then Dwmapi.DwmSetWindowAttribute([Form].Handle, Dwmapi.DWMATTRIB.WINDOW_CORNER_PREFERENCE, CInt(Dwmapi.FormCornersType.Rectangular), Marshal.SizeOf(GetType(Integer)))

            If [Form].Name = ExternalTerminal.Name Then
                ExternalTerminal.Label102.ForeColor = If(DarkMode, Color.Gold, Color.Gold.Dark(0.1))
            End If

            If [Form].Name = MainFrm.Name Then
                MainFrm.status_lbl.ForeColor = If(DarkMode, Color.White, Color.Black)
            End If

            [Form].Invalidate()
        End If

    End Sub

    Private Shared Sub ApplyStyleToSubControls(ctrl As Control, DarkMode As Boolean)
        'This will make all control have a consistent dark\light mode.
        Dim ctrl_theme As CtrlTheme = If(DarkMode, CtrlTheme.DarkExplorer, CtrlTheme.Default)
        SetControlTheme(ctrl.Handle, ctrl_theme)

        Dim b As Boolean = False
        If TypeOf ctrl Is UI.Retro.ButtonR Then b = True
        If TypeOf ctrl Is UI.Retro.PanelR Then b = True
        If TypeOf ctrl Is UI.Retro.PanelRaisedR Then b = True
        If TypeOf ctrl Is UI.Retro.TextBoxR Then b = True
        If TypeOf ctrl Is UI.Retro.WindowR Then b = True
        If TypeOf ctrl Is UI.WP.LabelAlt Then b = True

        If Not b Then
            Select Case DarkMode
                Case True
                    If ctrl.ForeColor = Color.Black Then ctrl.ForeColor = Color.White
                Case False
                    If ctrl.ForeColor = Color.White Then ctrl.ForeColor = Color.Black
            End Select
        End If

        If TypeOf ctrl Is UI.WP.GroupBox Then
            DirectCast(ctrl, UI.WP.GroupBox).BackColor = GetParentColor(ctrl).CB(If(GetParentColor(ctrl).IsDark, 0.04, -0.05))

        ElseIf TypeOf ctrl Is UI.WP.RadioImage Then
            DirectCast(ctrl, UI.WP.RadioImage).BackColor = GetParentColor(ctrl).CB(If(GetParentColor(ctrl).IsDark, 0.05, -0.05))

        ElseIf TypeOf ctrl Is UI.WP.Button Then
            With DirectCast(ctrl, UI.WP.Button)
                .BackColor = GetParentColor(ctrl).CB(If(GetParentColor(ctrl).IsDark, 0.04, -0.03))
                If .DrawOnGlass Then
                    .ForeColor = New VisualStyles.VisualStyleRenderer(VisualStyles.VisualStyleElement.Window.Caption.Active).GetColor(VisualStyles.ColorProperty.TextColor).Invert
                End If
            End With

        ElseIf TypeOf ctrl Is RichTextBox Then
            ctrl.BackColor = ctrl.Parent.BackColor

        ElseIf TypeOf ctrl Is LinkLabel Then
            DirectCast(ctrl, LinkLabel).LinkColor = If(DarkMode, Color.White, Color.Black)

        ElseIf TypeOf ctrl Is UI.WP.LinkLabel Then
            DirectCast(ctrl, UI.WP.LinkLabel).LinkColor = If(DarkMode, Color.White, Color.Black)

        ElseIf TypeOf ctrl Is Windows.Forms.TreeView Then
            With DirectCast(ctrl, Windows.Forms.TreeView)
                .BackColor = ctrl.Parent.BackColor
                .ForeColor = If(DarkMode, Color.White, Color.Black)
            End With

        ElseIf TypeOf ctrl Is TreeView Then
            With DirectCast(ctrl, TreeView)
                .BackColor = ctrl.Parent.BackColor
                .ForeColor = If(DarkMode, Color.White, Color.Black)
            End With

        ElseIf TypeOf ctrl Is ListView Then
            With DirectCast(ctrl, ListView)
                .BackColor = ctrl.Parent.BackColor
            End With

        ElseIf TypeOf ctrl Is ListBox Then
            ctrl.BackColor = ctrl.Parent.BackColor

        ElseIf TypeOf ctrl Is CheckedListBox Then
            With DirectCast(ctrl, CheckedListBox)
                .BackColor = ctrl.Parent.BackColor
                .ForeColor = If(DarkMode, Color.White, Color.Black)
            End With

        ElseIf TypeOf ctrl Is NumericUpDown Then
            With DirectCast(ctrl, NumericUpDown)
                .BackColor = ctrl.FindForm.BackColor.CB(0.04 * If(DarkMode, +1, -1))
                .ForeColor = If(DarkMode, Color.White, Color.Black)
            End With

        ElseIf TypeOf ctrl Is ComboBox Then
            With DirectCast(ctrl, ComboBox)
                .FlatStyle = FlatStyle.Flat
                .BackColor = ctrl.FindForm.BackColor.CB(0.04 * If(DarkMode, +1, -1))
                .ForeColor = If(DarkMode, Color.White, Color.Black)
            End With

        ElseIf TypeOf ctrl Is DataGridView Then
            Dim ColumnBack As Color
            Dim CellBack As Color

            Select Case DarkMode
                Case True
                    ColumnBack = My.Style.Colors.Back.Light(0.05)
                    CellBack = My.Style.Colors.Back

                Case False
                    ColumnBack = My.Style.Colors.Back.Dark(0.05)
                    CellBack = My.Style.Colors.Back
            End Select

            TryCast(ctrl, DataGridView).ColumnHeadersDefaultCellStyle.BackColor = ColumnBack
            TryCast(ctrl, DataGridView).BackColor = ctrl.Parent.BackColor
            TryCast(ctrl, DataGridView).BackgroundColor = ctrl.Parent.BackColor
            TryCast(ctrl, DataGridView).DefaultCellStyle.BackColor = CellBack

        End If

        If ctrl.HasChildren Then
            For Each c As Control In ctrl.Controls
                If TypeOf c Is TabPage Then c.BackColor = ctrl.Parent.BackColor
                ApplyStyleToSubControls(c, DarkMode)
            Next
        End If

        If ctrl.FindForm.Visible Then ctrl.Refresh()
    End Sub

    Public Shared Sub SetControlTheme(handle As IntPtr, theme As CtrlTheme)
        'If Not My.W7 Then
        If handle = IntPtr.Zero Then Throw New ArgumentNullException(NameOf(handle))

        Select Case theme
            Case CtrlTheme.None
                NativeMethods.UxTheme.SetWindowTheme(handle, "", "")
            Case CtrlTheme.Explorer
                NativeMethods.UxTheme.SetWindowTheme(handle, "Explorer", Nothing)
            Case CtrlTheme.DarkExplorer
                NativeMethods.UxTheme.SetWindowTheme(handle, "DarkMode_Explorer", Nothing)
            Case CtrlTheme.[Default]
                NativeMethods.UxTheme.SetWindowTheme(handle, Nothing, Nothing)
            Case Else
                Throw New ArgumentOutOfRangeException(NameOf(theme), theme, Nothing)
        End Select
        'End If
    End Sub

    Public Enum CtrlTheme
        None
        Explorer
        DarkExplorer
        [Default]
    End Enum
#End Region

#Region "Modern Dialogs"

#Region "MsgBox"
    Private Shared WithEvents TD As TaskDialog

    Shared Function ConvertToLink([String] As String) As String
        Dim c As New List(Of String)
        For Each x As String In [String].Split(" ")
            If (Uri.IsWellFormedUriString(x, UriKind.Absolute)) Then
                c.Add(String.Format("<a href=""{0}"">{0}</a>", x))
            Else
                c.Add(x)
            End If
        Next

        Return c.CString(" ")
    End Function

    ''' <summary>
    ''' Windows Vista/7 Styled MsgBox
    ''' </summary>
    ''' <param name="Message">The first text in the dialog, with blue color and big font size</param>
    ''' <param name="Style">The Buttons and Icon of the dialog</param>
    ''' <param name="SubMessage">The text which is located under message, with black color and small size (It can accept URLs)</param>
    ''' <param name="CollapsedText">Text beside collapsed button</param>
    ''' <param name="ExpandedText">Text beside expanded button</param>
    ''' <param name="ExpandedDetails">Text appear when the dialog is extended (It can accept URLs)</param>
    ''' <param name="DialogTitle">Text of Dialog's Titlebar</param>
    ''' <param name="Footer">Footer is the lowermost text (under the buttons) (It can accept URLs)</param>
    ''' <param name="FooterIcon">Icon Type of the fotter</param>
    ''' <param name="FooterCustomIcon">Icon of the fotter when its type is set to TaskDialogIcon.Custom</param>
    ''' <param name="RequireElevation">Put shield icon beside (OK, Yes, Retry) that means Administrator\Elevation is required</param>
    ''' <returns></returns>
    Public Overloads Shared Function MsgBox(Message As String, Optional Style As MsgBoxStyle = Nothing, Optional SubMessage As String = "",
                                            Optional CollapsedText As String = "", Optional ExpandedText As String = "", Optional ExpandedDetails As String = "",
                                            Optional DialogTitle As String = "", Optional Footer As String = "", Optional FooterIcon As TaskDialogIcon = TaskDialogIcon.Custom,
                                            Optional FooterCustomIcon As Icon = Nothing, Optional RequireElevation As Boolean = False) As MsgBoxResult

        Try
            If Not My.WXP Then
                TD = New TaskDialog With {
                   .EnableHyperlinks = True,
                   .RightToLeft = My.Lang.RightToLeft,
                   .ButtonStyle = TaskDialogButtonStyle.Standard,
                   .Content = ConvertToLink(SubMessage),
                   .FooterIcon = FooterIcon,
                   .CenterParent = True}

                If Not String.IsNullOrWhiteSpace(DialogTitle) Then TD.WindowTitle = DialogTitle Else TD.WindowTitle = My.Application.Info.Title
                If Not String.IsNullOrWhiteSpace(Message) Then TD.MainInstruction = Message
                If Not String.IsNullOrWhiteSpace(ExpandedText) Then TD.CollapsedControlText = ExpandedText
                If Not String.IsNullOrWhiteSpace(CollapsedText) Then TD.ExpandedControlText = CollapsedText
                If Not String.IsNullOrWhiteSpace(ExpandedDetails) Then TD.ExpandedInformation = ConvertToLink(ExpandedDetails)
                If Not String.IsNullOrWhiteSpace(Footer) Then TD.Footer = ConvertToLink(Footer)
                If FooterCustomIcon Is Nothing Then FooterCustomIcon = Drawing.Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location)

                TD.CustomFooterIcon = FooterCustomIcon

                Dim okButton As New TaskDialogButton(ButtonType.Custom) With {.Text = My.Lang.OK, .ElevationRequired = RequireElevation}
                Dim yesButton As New TaskDialogButton(ButtonType.Custom) With {.Text = My.Lang.Yes, .ElevationRequired = RequireElevation}
                Dim noButton As New TaskDialogButton(ButtonType.Custom) With {.Text = My.Lang.No}
                Dim cancelButton As New TaskDialogButton(ButtonType.Custom) With {.Text = My.Lang.Cancel}
                Dim retryButton As New TaskDialogButton(ButtonType.Custom) With {.Text = My.Lang.Retry, .ElevationRequired = RequireElevation}
                Dim closeButton As New TaskDialogButton(ButtonType.Custom) With {.Text = My.Lang.Close}
                Dim customButton As New TaskDialogButton(ButtonType.Custom)
                Dim icon As TaskDialogIcon

                If Style.HasFlag(MsgBoxStyle.YesNoCancel) Then
                    TD.Buttons.Add(yesButton)
                    TD.Buttons.Add(noButton)
                    TD.Buttons.Add(cancelButton)
                ElseIf Style.HasFlag(MsgBoxStyle.YesNo) Then
                    TD.Buttons.Add(yesButton)
                    TD.Buttons.Add(noButton)
                ElseIf Style.HasFlag(MsgBoxStyle.RetryCancel) Then
                    TD.Buttons.Add(retryButton)
                    TD.Buttons.Add(cancelButton)
                ElseIf Style.HasFlag(MsgBoxStyle.OkCancel) Then
                    TD.Buttons.Add(okButton)
                    TD.Buttons.Add(cancelButton)
                ElseIf Style.HasFlag(MsgBoxStyle.OkOnly) Then
                    TD.Buttons.Add(okButton)
                Else
                    TD.Buttons.Add(okButton)
                End If

                If Style.HasFlag(MsgBoxStyle.Information) Then icon = TaskDialogIcon.Information

                If Style.HasFlag(MsgBoxStyle.Question) Then
                    Try
                        My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
                    Catch
                    End Try
                    icon = TaskDialogIcon.Custom

                    TD.CustomMainIcon = DLLFunc.GetSystemIcon(Shell32.SHSTOCKICONID.HELP, Shell32.SHGSI.ICON)
                End If

                If Style.HasFlag(MsgBoxStyle.Critical) Then icon = TaskDialogIcon.Error

                If Style.HasFlag(MsgBoxStyle.Exclamation) Then icon = TaskDialogIcon.Warning

                TD.MainIcon = icon

                Dim result As MsgBoxResult = MsgBoxResult.Ok
                Dim resultButton As TaskDialogButton = TD.ShowDialog()

                If resultButton Is yesButton Then
                    result = MsgBoxResult.Yes
                ElseIf resultButton Is okButton Then
                    result = MsgBoxResult.Ok
                ElseIf resultButton Is noButton Then
                    result = MsgBoxResult.No
                ElseIf resultButton Is cancelButton Then
                    result = MsgBoxResult.Cancel
                ElseIf retryButton Is retryButton Then
                    result = MsgBoxResult.Cancel
                ElseIf retryButton Is closeButton Then
                    result = MsgBoxResult.Ok
                ElseIf retryButton Is customButton Then
                    result = MsgBoxResult.Ok
                End If

                TD.Dispose()
                resultButton.Dispose()
                okButton.Dispose()
                yesButton.Dispose()
                noButton.Dispose()
                cancelButton.Dispose()
                retryButton.Dispose()
                closeButton.Dispose()
                customButton.Dispose()

                Return result

            Else
                Return Msgbox_Classic(Message, SubMessage, ExpandedDetails, Footer, DialogTitle, Style)
            End If

        Catch
            Return Msgbox_Classic(Message, SubMessage, ExpandedDetails, Footer, DialogTitle, Style)
        End Try
    End Function

    Private Shared Function Msgbox_Classic(Message As String, SubMessage As String, ExpandedDetails As String, Footer As String, DialogTitle As String, Optional Style As MsgBoxStyle = Nothing)
        Dim SM As String = If(Not String.IsNullOrWhiteSpace(SubMessage), vbCrLf & vbCrLf & SubMessage, "")
        Dim ED As String = If(Not String.IsNullOrWhiteSpace(ExpandedDetails), vbCrLf & vbCrLf & ExpandedDetails, "")
        Dim Fo As String = If(Not String.IsNullOrWhiteSpace(Footer), vbCrLf & vbCrLf & Footer, "")
        Dim T As String = If(Not String.IsNullOrWhiteSpace(DialogTitle), DialogTitle, My.Application.Info.Title)

        Return Interaction.MsgBox(Message & SM & ED & Fo, Style, T)
    End Function


#Region "MsgBox Functions Branches"
    Public Overloads Shared Function MsgBox(Message As String) As MsgBoxResult
        Return MsgBox(Message, Nothing, "", "", "", "", "", "", Nothing, Nothing, False)
    End Function

    Public Overloads Shared Function MsgBox(Message As String, Style As MsgBoxStyle) As MsgBoxResult
        Return MsgBox(Message, Style, "", "", "", "", "", "", Nothing, Nothing, False)
    End Function

    Public Overloads Shared Function MsgBox(Message As String, Style As MsgBoxStyle, SubMessage As String) As MsgBoxResult
        Return MsgBox(Message, Style, SubMessage, "", "", "", "", "", Nothing, Nothing, False)
    End Function

    Public Overloads Shared Function MsgBox(Message As String, Style As MsgBoxStyle, SubMessage As String, CollapsedText As String) As MsgBoxResult
        Return MsgBox(Message, Style, SubMessage, CollapsedText, "", "", "", "", Nothing, Nothing, False)
    End Function

    Public Overloads Shared Function MsgBox(Message As String, Style As MsgBoxStyle, SubMessage As String, CollapsedText As String, ExpandedText As String) As MsgBoxResult
        Return MsgBox(Message, Style, SubMessage, CollapsedText, ExpandedText, "", "", "", Nothing, Nothing, False)
    End Function

    Public Overloads Shared Function MsgBox(Message As String, Style As MsgBoxStyle, SubMessage As String, CollapsedText As String, ExpandedText As String,
                                            ExpandedDetails As String) As MsgBoxResult
        Return MsgBox(Message, Style, SubMessage, CollapsedText, ExpandedText, ExpandedDetails, "", "", Nothing, Nothing, False)
    End Function

    Public Overloads Shared Function MsgBox(Message As String, Style As MsgBoxStyle, SubMessage As String, CollapsedText As String, ExpandedText As String,
                                        ExpandedDetails As String, DialogTitle As String) As MsgBoxResult
        Return MsgBox(Message, Style, SubMessage, CollapsedText, ExpandedText, ExpandedDetails, DialogTitle, "", Nothing, Nothing, False)
    End Function

    Public Overloads Shared Function MsgBox(Message As String, Style As MsgBoxStyle, SubMessage As String, CollapsedText As String, ExpandedText As String,
                                    ExpandedDetails As String, DialogTitle As String, Footer As String) As MsgBoxResult
        Return MsgBox(Message, Style, SubMessage, CollapsedText, ExpandedText, ExpandedDetails, DialogTitle, Footer, Nothing, Nothing, False)
    End Function

    Public Overloads Shared Function MsgBox(Message As String, Style As MsgBoxStyle, SubMessage As String, CollapsedText As String, ExpandedText As String,
                                ExpandedDetails As String, DialogTitle As String, Footer As String, FooterIcon As TaskDialogIcon) As MsgBoxResult
        Return MsgBox(Message, Style, SubMessage, CollapsedText, ExpandedText, ExpandedDetails, DialogTitle, Footer, FooterIcon, Nothing, False)
    End Function

    Public Overloads Shared Function MsgBox(Message As String, Style As MsgBoxStyle, SubMessage As String, CollapsedText As String, ExpandedText As String,
                            ExpandedDetails As String, DialogTitle As String, Footer As String, FooterIcon As TaskDialogIcon, FooterCustomIcon As Icon) As MsgBoxResult
        Return MsgBox(Message, Style, SubMessage, CollapsedText, ExpandedText, ExpandedDetails, DialogTitle, Footer, FooterIcon, FooterCustomIcon, False)
    End Function
#End Region

#Region "Events"
    Private Shared Sub TD_HyperlinkClicked(sender As Object, e As HyperlinkClickedEventArgs) Handles TD.HyperlinkClicked
        'Process.Start(e.Href)
    End Sub

    Private Shared Sub TD_RadioButtonClicked(sender As Object, e As TaskDialogItemClickedEventArgs) Handles TD.RadioButtonClicked

    End Sub

    Private Shared Sub TD_Timer(sender As Object, e As TimerEventArgs) Handles TD.Timer

    End Sub

    Private Shared Sub TD_VerificationClicked(sender As Object, e As EventArgs) Handles TD.VerificationClicked

    End Sub

    Private Shared Sub TD_HelpRequested(sender As Object, e As EventArgs) Handles TD.HelpRequested

    End Sub

    Private Shared Sub TD_ExpandButtonClicked(sender As Object, e As ExpandButtonClickedEventArgs) Handles TD.ExpandButtonClicked

    End Sub

    Private Shared Sub TD_ButtonClicked(sender As Object, e As TaskDialogItemClickedEventArgs) Handles TD.ButtonClicked

    End Sub
#End Region

#End Region

#Region "InputBox"
    Public Shared Function InputBox(Instruction As String, Optional Value As String = "", Optional Notice As String = "", Optional Title As String = "") As String
        Try
            If Not My.WXP Then
                Dim ib As New InputDialog With {
                        .MainInstruction = Instruction,
                        .Input = Value,
                        .Content = Notice,
                        .WindowTitle = If(Not String.IsNullOrWhiteSpace(Title), Title, My.Application.Info.Title)
                       }

                If ib.ShowDialog() = DialogResult.OK Then
                    Dim response As String = ib.Input : If String.IsNullOrWhiteSpace(response) Then response = Value
                    ib.Dispose()
                    Return response
                Else
                    ib.Dispose()
                    Return Value
                End If
            Else
                Return InputBox_Classic(Instruction, Value, Notice, Title)
            End If
        Catch
            Return InputBox_Classic(Instruction, Value, Notice, Title)
        End Try
    End Function


    Private Shared Function InputBox_Classic(Instruction As String, Optional Value As String = "", Optional Notice As String = "", Optional Title As String = "") As String
        Dim N As String = If(Not String.IsNullOrWhiteSpace(Notice), vbCrLf & vbCrLf & Notice, "")
        Dim T As String = If(Not String.IsNullOrWhiteSpace(Title), Title, My.Application.Info.Title)

        Return Interaction.InputBox(Instruction & N, T, Value)
    End Function

#End Region

#End Region

End Class