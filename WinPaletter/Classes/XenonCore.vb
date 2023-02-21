Imports System.ComponentModel
Imports System.Net.NetworkInformation
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports Ookii.Dialogs.WinForms
Imports WinPaletter.CP
Imports WinPaletter.NativeMethods
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Runtime.CompilerServices

Public Class XenonCore

#Region "Backgroundworker Fixers"

    Public Shared Sub SetCtrlTag(ByVal Tag As String, ByVal Ctrl As Control)
        Try
            If Ctrl.InvokeRequired Then
                Ctrl.Invoke(New setCtrlTagInvoker(AddressOf SetCtrlTag), Tag, Ctrl)
            Else
                Ctrl.Tag = Tag
            End If
        Catch

        End Try
    End Sub
    Private Delegate Sub setCtrlTagInvoker(ByVal Tag As String, ByVal Ctrl As Control)

    Public Shared Sub AddTreeNode(Ctrl As TreeView, text As String, imagekey As String)
        Try
            If Ctrl.InvokeRequired Then
                Ctrl.Invoke(New AddTreeNodeInvoker(AddressOf AddTreeNode), Ctrl, text, imagekey)
            Else
                With Ctrl.Nodes.Add(text)
                    .ImageKey = imagekey : .SelectedImageKey = imagekey
                End With
            End If
        Catch

        End Try
    End Sub
    Private Delegate Sub AddTreeNodeInvoker(Ctrl As TreeView, text As String, imagekey As String)
#End Region

#Region "Misc"
    Public Shared Sub RefreshDWM(CP As CP)


        Try
            Dim Com As Boolean
            Dwmapi.DwmIsCompositionEnabled(Com)

            If Com Then
                Dim temp As New Dwmapi.DWM_COLORIZATION_PARAMS

                If MainFrm.PreviewConfig = MainFrm.WinVer.W8 Then
                    temp.clrColor = CP.Windows8.ColorizationColor.ToArgb
                    temp.nIntensity = CP.Windows8.ColorizationColorBalance

                Else
                    temp.clrColor = CP.Windows7.ColorizationColor.ToArgb
                    temp.nIntensity = CP.Windows7.ColorizationColorBalance
                End If

                temp.clrAfterGlow = CP.Windows7.ColorizationAfterglow.ToArgb
                temp.clrAfterGlowBalance = CP.Windows7.ColorizationAfterglowBalance
                temp.clrBlurBalance = CP.Windows7.ColorizationBlurBalance
                temp.clrGlassReflectionIntensity = CP.Windows7.ColorizationGlassReflectionIntensity
                temp.fOpaque = (CP.Windows7.Theme = AeroTheme.AeroOpaque)
                Dwmapi.DwmSetColorizationParameters(temp, False)
            End If

        Catch
        End Try
    End Sub
    Public Shared Sub RestartExplorer(Optional [TreeView] As TreeView = Nothing)
        With My.Application

            Try
                If [TreeView] IsNot Nothing Then CP.AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.KillingExplorer), "info")
                Dim sw As New Stopwatch
                sw.Reset()
                sw.Start()
                .processKiller.Start()
                .processKiller.WaitForExit()
                .processExplorer.Start()
                sw.Stop()
                If [TreeView] IsNot Nothing Then CP.AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, String.Format(My.Lang.ExplorerRestarted, sw.ElapsedMilliseconds / 1000)), "time")
                sw.Reset()
            Catch ex As Exception
                If [TreeView] IsNot Nothing Then
                    CP.AddNode([TreeView], String.Format("{0}: {1}", Now.ToLongTimeString, My.Lang.ErrorExplorerRestart), "error")
                    My.Saving_Exceptions.Add(New Tuple(Of String, Exception)(My.Lang.ErrorExplorerRestart, ex))
                End If
            End Try

        End With
    End Sub

    '''<summary>
    '''Indicates whether any network connection is available
    '''Filter connections below a specified speed, as well as virtual network cards.
    '''</summary>
    '''<returns>
    '''    <c>true</c> if a network connection is available; otherwise, <c>false</c>.
    '''</returns>
    Public Shared Function IsNetworkAvailable() As Boolean
        Return IsNetworkAvailable(0)
    End Function

    '''<summary>
    '''Indicates whether any network connection is available
    '''Filter connections below a specified speed, as well as virtual network cards.
    '''</summary>
    '''<returns>
    '''    <c>true</c> if a network connection is available; otherwise, <c>false</c>.
    '''</returns>
    Public Shared Function IsNetworkAvailable(ByVal minimumSpeed As Long) As Boolean
        If Not NetworkInterface.GetIsNetworkAvailable() Then Return False

        For Each ni As NetworkInterface In NetworkInterface.GetAllNetworkInterfaces()
            If (ni.OperationalStatus <> OperationalStatus.Up) OrElse (ni.NetworkInterfaceType = NetworkInterfaceType.Loopback) OrElse (ni.NetworkInterfaceType = NetworkInterfaceType.Tunnel) Then Continue For
            If ni.Speed < minimumSpeed Then Continue For
            If (ni.Description.IndexOf("virtual", My._strIgnore) >= 0) OrElse (ni.Name.IndexOf("virtual", My._strIgnore) >= 0) Then Continue For
            If ni.Description.Equals("Microsoft Loopback Adapter", My._strIgnore) Then Continue For
            Return True
        Next

        Return False
    End Function

    'Public Shared Sub RefreshRegisrty()
    'Try : SendMessageTimeout(HWND_BROADCAST, WM_SETTINGCHANGE, 0, Marshal.PtrToStringAnsi("Environment"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
    'Try : SendMessageTimeout(HWND_BROADCAST, WM_SYSCOLORCHANGE, 0, Marshal.PtrToStringAnsi("Environment"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try

    'Try : SendMessageTimeout(HWND_BROADCAST, WM_DWMCOLORIZATIONCOLORCHANGED, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("WindowMetrics"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
    'Try : SendMessageTimeout(HWND_BROADCAST, WM_DWMCOMPOSITIONCHANGED, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("WindowMetrics"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
    'Try : SendMessageTimeout(HWND_BROADCAST, WM_THEMECHANGED, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("WindowMetrics"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
    'Try : SendMessageTimeout(HWND_BROADCAST, WM_SYSCOLORCHANGE, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("WindowMetrics"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
    'Try : SendMessageTimeout(HWND_BROADCAST, WM_PALETTECHANGED, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("WindowMetrics"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
    'Try : SendMessageTimeout(HWND_BROADCAST, WM_SETTINGCHANGE, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("WindowMetrics"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try

    'Try : SendMessageTimeout(HWND_BROADCAST, WM_DWMCOLORIZATIONCOLORCHANGED, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("Environment"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
    'Try : SendMessageTimeout(HWND_BROADCAST, WM_DWMCOMPOSITIONCHANGED, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("Environment"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
    'Try : SendMessageTimeout(HWND_BROADCAST, WM_THEMECHANGED, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("Environment"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
    'Try : SendMessageTimeout(HWND_BROADCAST, WM_SYSCOLORCHANGE, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("Environment"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
    'Try : SendMessageTimeout(HWND_BROADCAST, WM_PALETTECHANGED, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("Environment"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
    'Try : SendMessageTimeout(HWND_BROADCAST, WM_SETTINGCHANGE, UIntPtr.Zero, Marshal.StringToHGlobalAnsi("Environment"), SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, MSG_TIMEOUT, RESULT) : Catch : End Try
    'End Sub
#End Region

#Region "Dark\Light Mode"
    Public Shared Function GetDarkMode() As Boolean
        If My.Settings.Appearance_Custom Then
            Return My.Settings.Appearance_Custom_Dark
        Else
            Dim i As Long

            If System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Designtime Then
                Return True
            Else
                Try
                    If My.[Settings].Appearance_Auto Then
                        If My.W11 Or My.W10 Then
                            i = CLng(My.Computer.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Themes\Personalize").GetValue("AppsUseLightTheme", 0))
                            Return Not (i = 1)
                        Else
                            Return False
                        End If
                    Else
                        Return My.[Settings].Appearance_Dark
                    End If
                Catch
                    Try
                        Return My.[Settings].Appearance_Dark
                    Catch
                        Return My.W11 Or My.W10
                    End Try
                End Try
            End If
        End If

    End Function

    Public Shared ReadOnly DefaultAccent As Color = Color.FromArgb(0, 81, 210)
    Public Shared ReadOnly DefaultBackColorDark As Color = Color.FromArgb(25, 25, 25)
    Public Shared ReadOnly DefaultBackColorLight As Color = Color.FromArgb(230, 230, 230)

    Public Shared Sub ApplyDarkMode(Optional ByVal [Form] As Form = Nothing)
        Dim DarkMode As Boolean = GetDarkMode()

        Dim BackColor As Color
        Dim AccentColor As Color
        Dim CustomR As Boolean = False

        If My.Settings.Appearance_Custom Then
            BackColor = My.Settings.Appearance_Back
            AccentColor = My.Settings.Appearance_Accent
            CustomR = My.W11
        Else
            BackColor = If(DarkMode, DefaultBackColorDark, DefaultBackColorLight)
            AccentColor = DefaultAccent
            CustomR = False
        End If

        If Form Is Nothing Then

            '####################### For all open forms
            Try
                For Each OFORM As Form In Application.OpenForms
                    OFORM.Visible = False
                    OFORM.BackColor = BackColor
                    DLLFunc.DarkTitlebar(OFORM.Handle, DarkMode)
                    EnumControls(OFORM, DarkMode)

                    If My.W11 Then Dwmapi.DwmSetWindowAttribute(OFORM.Handle, Dwmapi.DWMATTRIB.WINDOW_CORNER_PREFERENCE, CInt(Dwmapi.FormCornersType.Default), Marshal.SizeOf(GetType(Integer)))
                    If CustomR And Not My.Settings.Appearance_Rounded Then Dwmapi.DwmSetWindowAttribute(OFORM.Handle, Dwmapi.DWMATTRIB.WINDOW_CORNER_PREFERENCE, CInt(Dwmapi.FormCornersType.Rectangular), Marshal.SizeOf(GetType(Integer)))

                    OFORM.Refresh()
                    OFORM.Visible = True
                Next
            Catch

            End Try

        Else
            '####################### For Selected [Form]

            If [Form].BackColor <> BackColor Then
                [Form].BackColor = BackColor
            End If
            DLLFunc.DarkTitlebar([Form].Handle, DarkMode)
            EnumControls([Form], DarkMode)

            If My.W11 Then Dwmapi.DwmSetWindowAttribute([Form].Handle, Dwmapi.DWMATTRIB.WINDOW_CORNER_PREFERENCE, CInt(Dwmapi.FormCornersType.Default), Marshal.SizeOf(GetType(Integer)))
            If CustomR And Not My.Settings.Appearance_Rounded Then Dwmapi.DwmSetWindowAttribute([Form].Handle, Dwmapi.DWMATTRIB.WINDOW_CORNER_PREFERENCE, CInt(Dwmapi.FormCornersType.Rectangular), Marshal.SizeOf(GetType(Integer)))

            If [Form].Name = ExternalTerminal.Name Then
                ExternalTerminal.Label102.ForeColor = If(DarkMode, Color.Gold, Color.Gold.Dark(0.1))
            End If
            If [Form].Name = MainFrm.Name Then
                MainFrm.status_lbl.ForeColor = If(DarkMode, Color.White, Color.Black)
            End If
            [Form].Refresh()

        End If

        Style = New XenonStyle(AccentColor, BackColor)
    End Sub
    Public Shared Sub EnumControls(ByVal ctrl As Control, ByVal DarkMode As Boolean)
        Dim ctrl_theme As CtrlTheme = If(DarkMode, CtrlTheme.DarkExplorer, CtrlTheme.Default)

        Dim b As Boolean = False
        If TypeOf ctrl Is RetroButton Then b = True
        If TypeOf ctrl Is RetroCheckBox Then b = True
        If TypeOf ctrl Is RetroGroupBox Then b = True
        If TypeOf ctrl Is RetroLabel Then b = True
        If TypeOf ctrl Is RetroPanel Then b = True
        If TypeOf ctrl Is RetroPanelRaised Then b = True
        If TypeOf ctrl Is RetroRadioButton Then b = True
        If TypeOf ctrl Is RetroSeparatorH Then b = True
        If TypeOf ctrl Is RetroSeparatorV Then b = True
        If TypeOf ctrl Is RetroTextBox Then b = True
        If TypeOf ctrl Is RetroWindow Then b = True

        If Not b Then
            Select Case DarkMode
                Case True
                    If ctrl.ForeColor = Color.Black Then ctrl.ForeColor = Color.White
                Case False
                    If ctrl.ForeColor = Color.White Then ctrl.ForeColor = Color.Black
            End Select
        End If

        If TypeOf ctrl Is XenonGroupBox Then
            DirectCast(ctrl, XenonGroupBox).BackColor = GetParentColor(ctrl).CB(If(GetParentColor(ctrl).IsDark, 0.04, -0.05))
        End If

        If TypeOf ctrl Is XenonRadioImage Then
            DirectCast(ctrl, XenonRadioImage).BackColor = GetParentColor(ctrl).CB(If(GetParentColor(ctrl).IsDark, 0.05, -0.05))
        End If

        If TypeOf ctrl Is XenonButton Then
            DirectCast(ctrl, XenonButton).BackColor = GetParentColor(ctrl).CB(If(GetParentColor(ctrl).IsDark, 0.04, -0.03))
            DirectCast(ctrl, XenonButton).ForeColor = If(DarkMode, Color.White, Color.Black)
        End If

        If TypeOf ctrl Is RichTextBox Then
            ctrl.BackColor = ctrl.Parent.BackColor
            ctrl.ForeColor = If(DarkMode, Color.White, Color.Black)
        End If

        If TypeOf ctrl Is LinkLabel Then
            DirectCast(ctrl, LinkLabel).LinkColor = If(DarkMode, Color.White, Color.Black)
        End If

        If TypeOf ctrl Is XenonLinkLabel Then
            DirectCast(ctrl, XenonLinkLabel).LinkColor = If(DarkMode, Color.White, Color.Black)
        End If

        If TypeOf ctrl Is TreeView Or TypeOf ctrl Is XenonTreeView Then
            With TryCast(ctrl, TreeView)
                .BackColor = ctrl.Parent.BackColor
                .ForeColor = If(DarkMode, Color.White, Color.Black)
            End With
        End If

        If TypeOf ctrl Is ListView Then
            With TryCast(ctrl, ListView)
                .BackColor = ctrl.Parent.BackColor
                .ForeColor = If(DarkMode, Color.White, Color.Black)
            End With
        End If

        If TypeOf ctrl Is NumericUpDown Then
            With TryCast(ctrl, NumericUpDown)
                .BackColor = ctrl.FindForm.BackColor.CB(0.04 * If(DarkMode, +1, -1))
                .ForeColor = If(DarkMode, Color.White, Color.Black)
            End With
        End If

        If TypeOf ctrl Is ComboBox Then
            With TryCast(ctrl, ComboBox)
                .FlatStyle = FlatStyle.Flat
                .BackColor = ctrl.FindForm.BackColor.CB(0.04 * If(DarkMode, +1, -1))
                .ForeColor = If(DarkMode, Color.White, Color.Black)
            End With
        End If

        'This will make all control have a consistent dark\light mode.
        SetTheme(ctrl.Handle, ctrl_theme)

        If ctrl.HasChildren Then
            For Each c As Control In ctrl.Controls

                If TypeOf ctrl Is TablessControl Then
                    With TryCast(ctrl, TablessControl)
                        For x = 0 To .TabPages.Count - 1
                            .TabPages.Item(x).BackColor = ctrl.Parent.BackColor
                            .TabPages.Item(x).Refresh()
                            .Refresh()
                        Next
                    End With
                End If

                If TypeOf ctrl Is XenonTabControl Then
                    With TryCast(ctrl, XenonTabControl)
                        For x = 0 To .TabPages.Count - 1
                            .TabPages.Item(x).Refresh()
                            .Invalidate()
                        Next
                    End With
                End If

                c.Invalidate()
                c.Refresh()
                EnumControls(c, DarkMode)
            Next
        End If

        ctrl.Refresh()
    End Sub

    Public Shared Sub SetTheme(ByVal handle As IntPtr, ByVal theme As CtrlTheme)
        'If Not My.W7 Then
        If handle = IntPtr.Zero Then Throw New ArgumentNullException(NameOf(handle))

        Select Case theme
            Case CtrlTheme.None
                NativeMethods.Uxtheme.SetWindowTheme(handle, "", "")
            Case CtrlTheme.Explorer
                NativeMethods.Uxtheme.SetWindowTheme(handle, "Explorer", Nothing)
            Case CtrlTheme.DarkExplorer
                NativeMethods.Uxtheme.SetWindowTheme(handle, "DarkMode_Explorer", Nothing)
            Case CtrlTheme.[Default]
                NativeMethods.Uxtheme.SetWindowTheme(handle, Nothing, Nothing)
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
            If My.WXP Then Throw New Exception("Modern Dialogs are not implemented for Windows XP")

            TD = New TaskDialog With {
                    .EnableHyperlinks = True,
                    .RightToLeft = My.Lang.RightToLeft,
                    .ButtonStyle = TaskDialogButtonStyle.Standard,
                    .Content = ConvertToLink(SubMessage),
                    .FooterIcon = FooterIcon}

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

        Catch
            Dim SM As String = If(Not String.IsNullOrWhiteSpace(SubMessage), vbCrLf & vbCrLf & SubMessage, "")
            Dim ED As String = If(Not String.IsNullOrWhiteSpace(ExpandedDetails), vbCrLf & vbCrLf & ExpandedDetails, "")
            Dim Fo As String = If(Not String.IsNullOrWhiteSpace(Footer), vbCrLf & vbCrLf & Footer, "")
            Dim T As String = If(Not String.IsNullOrWhiteSpace(DialogTitle), DialogTitle, My.Application.Info.Title)

            Return Interaction.MsgBox(Message & SM & ED & Fo, Style, T)
        End Try
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
            If My.WXP Then Throw New Exception("Modern Dialogs are not implemented for Windows XP")

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

        Catch
            Dim N As String = If(Not String.IsNullOrWhiteSpace(Notice), vbCrLf & vbCrLf & Notice, "")
            Dim T As String = If(Not String.IsNullOrWhiteSpace(Title), Title, My.Application.Info.Title)

            Return Interaction.InputBox(Instruction & N, T, Value)
        End Try
    End Function
#End Region

#End Region
End Class

''' <summary>
''' Functions that help you draw\drop special DWM effects (Tabbed\Mica\Acrylic\Aero) on a form
''' </summary>
Public Module FormDWMEffects

    ''' <summary>
    ''' Draw effect on form depending on both user choice (Tabbed\Mica\Acrylic\Aero) and current OS
    ''' </summary>
    <Extension()>
    Public Sub DrawDWMEffect(ByVal [Form] As Form, Optional ByVal Border As Boolean = True, Optional FormStyle As FormStyle = FormStyle.Mica)

        Dim Transparency_W7 As Boolean
        Try
            Dwmapi.DwmIsCompositionEnabled(Transparency_W7)
        Catch
            Transparency_W7 = False
        End Try

        Dim Temp As Boolean
        Dim Transparency_W11_10 As Boolean
        Try
            Temp = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", False)
            Transparency_W11_10 = (My.W10 Or My.W11) AndAlso Temp
        Catch
            Transparency_W11_10 = False
        End Try

        Try
            If My.W11 AndAlso Transparency_W11_10 Then
                If FormStyle = FormStyle.Mica Then
                    [Form].DrawMica(MicaStyle.Mica)

                ElseIf FormStyle = FormStyle.Tabbed Then
                    [Form].DrawMica(MicaStyle.Tabbed)

                ElseIf FormStyle = FormStyle.Acrylic Then
                    [Form].DrawAcrylic(Border)

                End If

            ElseIf My.W10 AndAlso Transparency_W11_10 Then
                [Form].DrawAcrylic(Border)

            ElseIf My.W7 AndAlso Transparency_W7 Then
                [Form].DrawAero

            Else
                [Form].DrawTransparentGray

            End If

        Catch
            [Form].DrawTransparentGray

        End Try

    End Sub

    <Extension()>
    Sub DrawAcrylic(Form As Form, Optional ByVal Border As Boolean = True)
        Dim accent = New User32.AccentPolicy With {.AccentState = NativeMethods.User32.AccentState.ACCENT_ENABLE_BLURBEHIND}
        If Border Then accent.AccentFlags = &H20 Or &H40 Or &H80 Or &H100
        Dim accentStructSize = Marshal.SizeOf(accent)
        Dim accentPtr = Marshal.AllocHGlobal(accentStructSize)
        Marshal.StructureToPtr(accent, accentPtr, False)

        Dim Data = New User32.WindowCompositionAttributeData With {
                .Attribute = User32.WindowCompositionAttribute.WCA_ACCENT_POLICY,
                .SizeOfData = accentStructSize,
                .Data = accentPtr
            }

        User32.SetWindowCompositionAttribute(Form.Handle, Data)
        Marshal.FreeHGlobal(accentPtr)
    End Sub

    ''' <summary>
    ''' Draws Mica Style (Windows 11 and Higher - Tabbed Style is for Windows 11 Build 22523 and Higher, if not, Mica will be used instead)
    ''' </summary>
    <Extension()>
    Sub DrawMica(Form As Form, Optional Style As MicaStyle = MicaStyle.Mica)
        Dim FS As New FormStyle
        If Style = MicaStyle.Mica Then FS = FormStyle.Mica
        If Style = MicaStyle.Tabbed And My.W11_22523 Then FS = FormStyle.Tabbed Else FS = FormStyle.Mica

        DLLFunc.DarkTitlebar(Form.Handle, True)
        Dwmapi.DwmSetWindowAttribute(Form.Handle, Dwmapi.DWMATTRIB.SYSTEMBACKDROP_TYPE, CInt(FS), Marshal.SizeOf(GetType(Integer)))
        Dwmapi.DwmExtendFrameIntoClientArea(Form.Handle, New Dwmapi.MARGINS With {.leftWidth = -1, .rightWidth = -1, .topHeight = -1, .bottomHeight = -1})
    End Sub

    <Extension()>
    Sub DrawAero(Form As Form)
        Dim Margins As New Dwmapi.MARGINS With {.leftWidth = -1, .rightWidth = -1, .topHeight = -1, .bottomHeight = -1}
        Dwmapi.DwmExtendFrameIntoClientArea(Form.Handle, Margins)
    End Sub

    <Extension()>
    Sub DrawTransparentGray([Form] As Form, Optional NoWindowBorders As Boolean = True)
        [Form].BackColor = Color.FromArgb(5, 5, 5)
        [Form].Opacity = 0.5
        If NoWindowBorders Then Form.FormBorderStyle = FormBorderStyle.None
    End Sub

    ''' <summary>
    ''' Sets Titlebar Backcolor, Forecolor and border color (Only for Windows 11 and Higher)
    ''' </summary>
    <Extension()>
    Sub DrawCustomTitlebar([Form] As Form, Optional BackColor As Color = Nothing, Optional ForeColor As Color = Nothing, Optional BorderColor As Color = Nothing)

        If My.W11 Then
            Try
                If BackColor <> Nothing Then Dwmapi.DwmSetWindowAttribute([Form].Handle, Dwmapi.DWMATTRIB.CAPTION_COLOR, ColorTranslator.ToWin32(BackColor), Marshal.SizeOf(GetType(Integer)))
            Catch
            End Try

            Try
                If ForeColor <> Nothing Then Dwmapi.DwmSetWindowAttribute([Form].Handle, Dwmapi.DWMATTRIB.TEXT_COLOR, ColorTranslator.ToWin32(ForeColor), Marshal.SizeOf(GetType(Integer)))
            Catch
            End Try

            Try
                If BorderColor <> Nothing Then Dwmapi.DwmSetWindowAttribute([Form].Handle, Dwmapi.DWMATTRIB.BORDER_COLOR, ColorTranslator.ToWin32(BorderColor), Marshal.SizeOf(GetType(Integer)))
            Catch
            End Try

        End If

    End Sub

    Public Enum FormStyle
        Auto
        [Default]
        Mica
        Acrylic
        Tabbed
    End Enum

    Enum MicaStyle
        Mica
        Tabbed
    End Enum

End Module

' Class implementing image filter interface and base filter class.
'
' Copyright (c) 2007 Miran Uhan
'
' This program is free software; you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation; either version 2 of the License, or
' (at your option) any later version.
'
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with this program; if not, write to the
'    Free Software Foundation, Inc.
'    51 Franklin Street, Fifth Floor
'    Boston, MA 02110-1301 USA
'
' Sources:
' - http://www.codeproject.com/cs/library/yael_image_filters.asp
'
' For questions you can contact author at
'    miran.uhan@gmail.com
' Sugestions and bug reports are also welcome.
'
Public Interface IFilter

    Function ExecuteFilter(
             ByVal inputImage As System.Drawing.Image) As System.Drawing.Image

End Interface
Public MustInherit Class BasicFilter
    Implements IFilter

    ''' <summary>
    ''' Background color. Default is a transparent background.
    ''' </summary>
    Private _bgColor As Color = Color.FromArgb(0, 0, 0, 0)

    ''' <summary>
    ''' Interpolation mode. Default is highest quality.
    ''' </summary>
    Private _interpolation As InterpolationMode =
             InterpolationMode.HighQualityBicubic

    ''' <summary>
    ''' Get or set background color.
    ''' </summary>
    Public Property BackgroundColor() As Color
        Get
            Return _bgColor
        End Get
        Set(ByVal value As Color)
            _bgColor = value
        End Set
    End Property

    ''' <summary>
    ''' Get or set resize interpolation mode.
    ''' </summary>
    Public Property Interpolation() As InterpolationMode
        Get
            Return _interpolation
        End Get
        Set(ByVal value As InterpolationMode)
            _interpolation = value
        End Set
    End Property

    ''' <summary>
    ''' Execute filter function and return new filtered image.
    ''' </summary>
    ''' <param name="img">Image to be filtered.</param>
    ''' <returns>New filtered image.</returns>
    Public MustOverride Function ExecuteFilter(
             ByVal img As System.Drawing.Image) _
             As System.Drawing.Image Implements IFilter.ExecuteFilter

End Class

' Class implementing hue, saturation and lightness filtering of image.
'
' Copyright (c) 2007 Miran Uhan
'
' This program is free software; you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation; either version 2 of the License, or
' (at your option) any later version.
'
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with this program; if not, write to the
'    Free Software Foundation, Inc.
'    51 Franklin Street, Fifth Floor
'    Boston, MA 02110-1301 USA
'
' Sources:
' - http://en.wikipedia.org/wiki/HSL_color_space
' - http://www.easyrgb.com/math.php?MATH=M19#text19
'
' For questions you can contact author at
'    miran.uhan@gmail.com
' Sugestions and bug reports are also welcome.
'
Public Class HSLFilter
    Inherits BasicFilter

    Private _hue As Double = 0.0
    Private _saturation As Double = 0.0
    Private _lightness As Double = 0.0

    ''' <summary>
    ''' Get or set hue correction value.
    ''' </summary>
    ''' <value>Any double, will be scaled to range [0..360).</value>
    ''' <returns>Double in range [0..360).</returns>
    Public Property Hue() As Double
        Get
            Return _hue
        End Get
        Set(ByVal value As Double)
            _hue = value
            Do While _hue < 0.0
                _hue += 360
            Loop
            Do While _hue >= 360.0
                _hue -= 360
            Loop
        End Set
    End Property

    ''' <summary>
    ''' Get or set saturation correction value.
    ''' </summary>
    ''' <value>Double in range [-100..+100]%.</value>
    ''' <returns>Double in range [-100..+100]%.</returns>
    ''' <remarks></remarks>
    Public Property Saturation() As Double
        Get
            Return _saturation
        End Get
        Set(ByVal value As Double)
            If ((value >= -100.0) And (value <= 100.0)) Then
                _saturation = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Get or set lightness correction value.
    ''' </summary>
    ''' <value>Double in range [-100..+100]%.</value>
    ''' <returns>Double in range [-100..+100]%.</returns>
    ''' <remarks></remarks>
    Public Property Lightness() As Double
        Get
            Return _lightness
        End Get
        Set(ByVal value As Double)
            If ((value >= -100.0) And (value <= 100.0)) Then
                _lightness = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Execute filter and return filtered image.
    ''' </summary>
    ''' <param name="img"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function ExecuteFilter(ByVal img As System.Drawing.Image) As System.Drawing.Image
        Select Case img.PixelFormat
            Case PixelFormat.Format16bppGrayScale
                Return img
            Case PixelFormat.Format24bppRgb,
                     PixelFormat.Format32bppArgb, PixelFormat.Format32bppRgb
                Return ExecuteRgb8(img)
            Case PixelFormat.Format48bppRgb
                Return img
            Case Else
                Return img
        End Select
    End Function

    ''' <summary>
    ''' Execute filter on (A)RGB image with 8 bits per color.
    ''' </summary>
    ''' <param name="img"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExecuteRgb8(ByVal img As System.Drawing.Image) As System.Drawing.Image
        Const c1o60 As Double = 1 / 60
        Const c1o255 As Double = 1 / 255
        Dim result As Bitmap = New Bitmap(img)
        result.SetResolution(img.HorizontalResolution, img.VerticalResolution)
        Dim bmpData As BitmapData = result.LockBits(
                       New Rectangle(0, 0, result.Width, result.Height),
                       ImageLockMode.ReadWrite, img.PixelFormat)
        Dim pixelBytes As Integer =
                 System.Drawing.Image.GetPixelFormatSize(img.PixelFormat) \ 8
        'Get the address of the first line.
        Dim ptr As IntPtr = bmpData.Scan0
        Dim size As Integer = bmpData.Stride * result.Height
        Dim pixels(size - 1) As Byte
        Dim index As Integer
        Dim R, G, B As Double
        Dim H, S, L, H1 As Double
        Dim min, max, dif, sum As Double
        Dim f1, f2 As Double
        Dim v1, v2, v3 As Double
        Dim sat As Double = 127 * _saturation / 100
        Dim lum As Double = 127 * _lightness / 100
        'Copy the RGB values into the array.
        System.Runtime.InteropServices.Marshal.Copy(ptr, pixels, 0, size)
        'Main loop.
        For row As Integer = 0 To result.Height - 1
            For col As Integer = 0 To result.Width - 1
                index = (row * bmpData.Stride) + (col * pixelBytes)
                R = pixels(index + 2)
                G = pixels(index + 1)
                B = pixels(index + 0)
                'Conversion to HSL space.
                min = R
                If (G < min) Then min = G
                If (B < min) Then min = B
                max = R : f1 = 0.0 : f2 = G - B
                If (G > max) Then
                    max = G : f1 = 120.0 : f2 = B - R
                End If
                If (B > max) Then
                    max = B : f1 = 240.0 : f2 = R - G
                End If
                dif = max - min
                sum = max + min
                L = 0.5 * sum
                If (dif = 0) Then
                    H = 0.0 : S = 0.0
                Else
                    If (L < 127.5) Then
                        S = 255.0 * dif / sum
                    Else
                        S = 255.0 * dif / (510.0 - sum)
                    End If
                    H = (f1 + 60.0 * f2 / dif)
                    If H < 0.0 Then H += 360.0
                    If H >= 360.0 Then H -= 360.0
                End If
                'Apply transformation.
                H += _hue
                If H >= 360.0 Then H -= 360.0
                S += sat
                If S < 0.0 Then S = 0.0
                If S > 255.0 Then S = 255.0
                L += lum
                If L < 0.0 Then L = 0.0
                If L > 255.0 Then L = 255.0
                'Conversion back to RGB space.
                If (S = 0) Then
                    R = L : G = L : B = L
                Else
                    If (L < 127.5) Then
                        v2 = c1o255 * L * (255 + S)
                    Else
                        v2 = L + S - c1o255 * S * L
                    End If
                    v1 = 2 * L - v2
                    v3 = v2 - v1
                    H1 = H + 120.0
                    If (H1 >= 360.0) Then H1 -= 360.0
                    If (H1 < 60.0) Then
                        R = v1 + v3 * H1 * c1o60
                    ElseIf (H1 < 180.0) Then
                        R = v2
                    ElseIf (H1 < 240.0) Then
                        R = v1 + v3 * (4 - H1 * c1o60)
                    Else
                        R = v1
                    End If
                    H1 = H
                    If (H1 < 60.0) Then
                        G = v1 + v3 * H1 * c1o60
                    ElseIf (H1 < 180.0) Then
                        G = v2
                    ElseIf (H1 < 240.0) Then
                        G = v1 + v3 * (4 - H1 * c1o60)
                    Else
                        G = v1
                    End If
                    H1 = H - 120.0
                    If (H1 < 0.0) Then H1 += 360.0
                    If (H1 < 60.0) Then
                        B = v1 + v3 * H1 * c1o60
                    ElseIf (H1 < 180.0) Then
                        B = v2
                    ElseIf (H1 < 240.0) Then
                        B = v1 + v3 * (4 - H1 * c1o60)
                    Else
                        B = v1
                    End If
                End If
                'Save new values.
                pixels(index + 2) = CByte(R)
                pixels(index + 1) = CByte(G)
                pixels(index + 0) = CByte(B)
            Next
        Next
        'Copy the RGB values back to the bitmap
        System.Runtime.InteropServices.Marshal.Copy(pixels, 0, ptr, size)
        'Unlock the bits.
        result.UnlockBits(bmpData)
        Return result
    End Function

End Class

Public Class Visual
    '' FILENAME:        Visual.vb
    '' NAMESPACE:       PI.Common
    '' CREATED BY:      Luke Berg
    '' CREATED:         10-02-06
    '' DESCRIPTION:     A common module of visual functions.

    Private Shared ReadOnly colorsFading As New Dictionary(Of String, BackgroundWorker) 'Keeps track of any backgroundworkers already fading colors
    Private Shared ReadOnly backgroundWorkers As New Dictionary(Of BackgroundWorker, ColorFaderInformation) 'Associate each background worker with information it needs

    ' The delegate of a method that will be called when the color finishes fading
    Public Delegate Sub DoneFading(ByVal container As Object, ByVal colorProperty As String)

    ''' <summary>
    '''  Fades a color property from one color to another
    ''' </summary>
    ''' <param name="container">The object that contains the color property</param>
    ''' <param name="colorProperty">The name of the color property to change</param>
    ''' <param name="startColor">The color to start the fade with</param>
    ''' <param name="endColor">The color to end the fade with</param>
    ''' <param name="steps">The number of steps to take to fade from the start color to the end color</param>
    ''' <param name="delay">The delay in milliseconds between each step in the fade.</param>
    ''' <param name="callback">A function to be called when the fade completes</param>
    ''' <remarks></remarks>
    ''' 
    Public Shared Sub FadeColor(ByVal container As Object, ByVal colorProperty As String, ByVal startColor As Color, ByVal endColor As Color, ByVal steps As Integer, ByVal delay As Integer, Optional ByVal callback As DoneFading = Nothing)
        Dim colorSteps(0) As ColorStep
        colorSteps(0) = New ColorStep(endColor, steps)
        Try
            FadeColor(container, colorProperty, startColor, colorSteps, delay, callback)
        Catch
        End Try
    End Sub

    ''' <summary>
    '''  Fades a color property from one color to another, and then to yet another
    ''' </summary>
    ''' <param name="container">The object that contains the color property</param>
    ''' <param name="colorProperty">The name of the color property to change</param>
    ''' <param name="startColor">The color to start the fade with</param>
    ''' <param name="middleColor">The color to fade to first</param>
    ''' <param name="middleSteps">The number of steps to take in fading to the first color</param>
    ''' <param name="endcolor">The last color to fade to</param>
    ''' <param name="endSteps">The number of steps to take in fading to the last color</param>
    ''' <param name="delay">The delay between each step in the fade</param>
    ''' <param name="callback">A function that will be called after the fading has completed</param>
    ''' <remarks></remarks>
    Public Shared Sub FadeColor(ByVal container As Object, ByVal colorProperty As String, ByVal startColor As Color, ByVal middleColor As Color, ByVal middleSteps As Integer, ByVal endcolor As Color, ByVal endSteps As Integer, ByVal delay As Integer, Optional ByVal callback As DoneFading = Nothing)
        Dim colorSteps(1) As ColorStep
        colorSteps(0) = New ColorStep(middleColor, middleSteps)
        colorSteps(1) = New ColorStep(endcolor, endSteps)
        Try
            FadeColor(container, colorProperty, startColor, colorSteps, delay, callback)
        Catch
        End Try
    End Sub

    ''' <summary>
    '''  Fades a color property to various colors
    ''' </summary>
    ''' <param name="container">The object that contains the color property</param>
    ''' <param name="colorProperty">The name of the color property to change</param>
    ''' <param name="startColor">The color to start the fade with</param>
    ''' <param name="colorSteps">A list of steps in fading the color - an enumerable list of colors and the steps to get to that color</param>
    ''' <param name="delay">The delay between each step in fading the color</param>
    ''' <param name="callBack">A method to call when the fading has completed</param>
    ''' <remarks></remarks>

    Public Shared Sub FadeColor(ByVal container As Object, ByVal colorProperty As String, ByVal startColor As Color, ByVal colorSteps As IEnumerable(Of ColorStep), ByVal delay As Integer, Optional ByVal callBack As DoneFading = Nothing)

        Dim colorFader As New BackgroundWorker

        ' Stores all the parameter information into a class that the background worker will access
        Dim colorFaderInfo As New ColorFaderInformation(container, colorProperty, startColor, colorSteps, delay, callBack)

        ' Checks if the color is already in the process of fading.
        If colorsFading.TryGetValue(GenerateHashCode(container, colorProperty), colorFader) Then
            ' Cancels the backgroundWorkers process and sets a flag indicating that it should restart itself with
            ' the new information.
            colorFader.CancelAsync()
            colorFaderInfo.Rerun = True
            backgroundWorkers(colorFader) = colorFaderInfo
        Else

            ' Creates a new backgroundWorker and adds handlers to all its events
            colorFader = New BackgroundWorker()
            AddHandler colorFader.DoWork, AddressOf BackgroundWorker_DoWork
            AddHandler colorFader.ProgressChanged, AddressOf BackgroundWorker_ProgressChanged
            AddHandler colorFader.RunWorkerCompleted, AddressOf BackgroundWorker_RunWorkerCompleted
            colorFader.WorkerReportsProgress = True
            colorFader.WorkerSupportsCancellation = True

            backgroundWorkers.Add(colorFader, colorFaderInfo)
            colorsFading.Add(GenerateHashCode(container, colorProperty), colorFader)

        End If

        ' Starts the backgroundWorker beginning the fade
        If Not colorFader.IsBusy() Then
            colorFader.RunWorkerAsync(colorFaderInfo)
        End If
    End Sub

    ''' <summary>
    '''  The work that the background worker does in fading the color
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Shared Sub BackgroundWorker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        Dim info As ColorFaderInformation = CType(e.Argument, ColorFaderInformation)
        ' These are calculated with each iteration (step) and used to set the color
        ' when the background worker reports its progress.

        Dim curA As Double
        Dim curR As Double
        Dim curG As Double
        Dim curB As Double

        Dim startStepColor As Color = info.StartColor
        Dim endStepColor As Color

        For Each colorStep As ColorStep In info.Colors

            endStepColor = colorStep.Color

            ' Gets the amount to change each color part per step

            Dim aStep As Double = (CType(endStepColor.A, Double) - startStepColor.A) / colorStep.Steps
            Dim rStep As Double = (CType(endStepColor.R, Double) - startStepColor.R) / colorStep.Steps
            Dim gStep As Double = (CType(endStepColor.G, Double) - startStepColor.G) / colorStep.Steps
            Dim bStep As Double = (CType(endStepColor.B, Double) - startStepColor.B) / colorStep.Steps

            ' the red, green and blue parts of the current color
            curA = startStepColor.A
            curR = startStepColor.R
            curG = startStepColor.G
            curB = startStepColor.B

            ' loop through, and fade
            For i As Integer = 1 To colorStep.Steps
                curA += aStep
                curR += rStep
                curB += bStep
                curG += gStep

                Try : CType(sender, BackgroundWorker).ReportProgress(0, Color.FromArgb(CInt(curA), CInt(curR), CInt(curG), CInt(curB))) : Catch : End Try

                System.Threading.Thread.Sleep(info.Delay)

                If CType(sender, BackgroundWorker).CancellationPending Then
                    e.Cancel = True
                    Exit For
                End If
            Next

            startStepColor = endStepColor

        Next

    End Sub

    ''' <summary>
    '''  Calls to this method are marshalled back to the original thread, so here is where we actually change the color.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Shared Sub BackgroundWorker_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
        Dim info As ColorFaderInformation = Nothing
        If backgroundWorkers.TryGetValue(CType(sender, BackgroundWorker), info) Then
            Dim currentColor As Color = CType(e.UserState, Color)
            Try
                CallByName(info.Container, info.ColorProperty, CallType.Let, currentColor)
            Catch
            End Try
        End If
    End Sub

    ''' <summary>
    '''  This is raised when the background method completes.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Shared Sub BackgroundWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)

        Dim info As ColorFaderInformation = Nothing

        If backgroundWorkers.TryGetValue(CType(sender, BackgroundWorker), info) Then

            If Not e.Cancelled Then

                If info.CallBack IsNot Nothing Then
                    info.CallBack.Invoke(info.Container, info.ColorProperty)
                End If

                backgroundWorkers.Remove(CType(sender, BackgroundWorker))
                colorsFading.Remove(GenerateHashCode(info.Container, info.ColorProperty))
            Else

                If info.Rerun Then

                    info.Rerun = False
                    CType(sender, BackgroundWorker).RunWorkerAsync(info)

                End If

            End If

        End If

    End Sub

    ''' <summary>
    '''  Generates a hashcode for an object and its color that are in the process of fading
    ''' </summary>
    ''' <param name="container">The object whose color property needs to be faded</param>
    ''' <param name="colorProperty">The string name of the property to fade</param>
    ''' <returns>A unique string representing the object and it's color property</returns>
    ''' <remarks></remarks>
    Private Shared Function GenerateHashCode(ByVal container As Object, ByVal colorProperty As String) As String
        Return container.GetHashCode() & colorProperty
    End Function

    ''' <summary>
    '''  A simple class for storing information a backgroundWorker needs to perform the fading.
    ''' </summary>
    ''' <remarks></remarks>
    Private Class ColorFaderInformation

        Public CallBack As DoneFading
        Public Container As Object
        Public ColorProperty As String
        Public StartColor As Color
        Public Colors As IEnumerable(Of ColorStep)
        Public Delay As Integer
        Public Rerun As Boolean

        Public Sub New(ByVal container As Object, ByVal colorProperty As String, ByVal startColor As Color, ByVal colorSteps As IEnumerable(Of ColorStep), ByVal delay As Integer, Optional ByVal callBack As DoneFading = Nothing)
            Me.Container = container
            Me.ColorProperty = colorProperty
            Me.StartColor = startColor
            Me.Colors = colorSteps
            Me.Delay = delay
            Me.CallBack = callBack
            Me.Rerun = False
        End Sub

    End Class

    ''' <summary>
    '''  A simple class needed to represent a single step in the fading process
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure ColorStep

        Public Color As Color
        Public Steps As Integer

        Public Sub New(ByVal color As Color, ByVal steps As Integer)
            Me.Color = color
            Me.Steps = steps
        End Sub

    End Structure

End Class