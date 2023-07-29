Imports System.ComponentModel
Imports System.Net
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports Ookii.Dialogs.WinForms
Imports WinPaletter.CP
Imports WinPaletter.NativeMethods

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

                If My.W8 Or My.W81 Then
                    temp.clrColor = CP.Windows81.ColorizationColor.ToArgb
                    temp.nIntensity = CP.Windows81.ColorizationColorBalance

                ElseIf My.W7 Then
                    temp.clrColor = CP.Windows7.ColorizationColor.ToArgb
                    temp.nIntensity = CP.Windows7.ColorizationColorBalance

                    temp.clrAfterGlow = CP.Windows7.ColorizationAfterglow.ToArgb
                    temp.clrAfterGlowBalance = CP.Windows7.ColorizationAfterglowBalance
                    temp.clrBlurBalance = CP.Windows7.ColorizationBlurBalance
                    temp.clrGlassReflectionIntensity = CP.Windows7.ColorizationGlassReflectionIntensity
                    temp.fOpaque = (CP.Windows7.Theme = Structures.Windows7.Themes.AeroOpaque)

                ElseIf My.WVista Then
                    temp.clrColor = Color.FromArgb(CP.WindowsVista.Alpha, CP.WindowsVista.ColorizationColor).ToArgb
                    temp.clrAfterGlow = Color.FromArgb(CP.WindowsVista.Alpha, CP.WindowsVista.ColorizationColor).ToArgb

                    'temp.nIntensity = CP.WindowsVista.ColorizationColorBalance
                    'temp.clrAfterGlowBalance = CP.WindowsVista.ColorizationAfterglowBalance
                    'temp.clrBlurBalance = CP.WindowsVista.ColorizationBlurBalance
                    'temp.clrGlassReflectionIntensity = CP.WindowsVista.ColorizationGlassReflectionIntensity
                    temp.fOpaque = (CP.WindowsVista.Theme = Structures.Windows7.Themes.AeroOpaque)
                End If

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
    '''</summary>
    '''<returns>
    '''    <c>true</c> if a network connection is available; otherwise, <c>false</c>.
    '''</returns>
    Public Shared Function IsNetworkAvailable() As Boolean
        Return Wininet.CheckNet
    End Function
    Public Shared Function Ping(ByVal url As String) As Boolean
        Try
            Dim request As HttpWebRequest = CType(HttpWebRequest.Create(url), HttpWebRequest)
            request.Timeout = 60000
            request.AllowAutoRedirect = False
            request.Method = "HEAD"

            Using response = request.GetResponse()
                Return True
            End Using

        Catch
            Return False
        End Try
    End Function
    Public Shared Function GetWindowsScreenScalingFactor(ByVal Optional percentage As Boolean = True) As Double
        Dim GraphicsObject As Graphics = Graphics.FromHwnd(IntPtr.Zero)
        Dim DeviceContextHandle As IntPtr = GraphicsObject.GetHdc()
        Dim LogicalScreenHeight As Integer = GDI32.GetDeviceCaps(DeviceContextHandle, CInt(GDI32.DeviceCap.VERTRES))
        Dim PhysicalScreenHeight As Integer = GDI32.GetDeviceCaps(DeviceContextHandle, CInt(GDI32.DeviceCap.DESKTOPVERTRES))
        Dim ScreenScalingFactor As Double = Math.Round(CDbl(PhysicalScreenHeight) / CDbl(LogicalScreenHeight), 2)

        If percentage Then ScreenScalingFactor *= 100.0

        GraphicsObject.ReleaseHdc(DeviceContextHandle)
        GraphicsObject.Dispose()
        Return ScreenScalingFactor
    End Function

    Enum TaskType
        Shutdown
        Logoff
        Logon
    End Enum
    Enum Actions
        Add
        Delete
    End Enum
    Public Shared Sub TaskMgmt(TaskType As TaskType, Action As Actions, Optional File As String = "")

        DeleteTask(TaskType)

        If Action = Actions.Add Then
            Dim process As New Process With {.StartInfo = New ProcessStartInfo With {
                   .FileName = My.PATH_System32 & "\schtasks",
                   .Verb = If(My.WXP AndAlso My.isElevated, "", "runas"),
                   .WindowStyle = ProcessWindowStyle.Hidden,
                   .CreateNoWindow = True,
                   .UseShellExecute = True
                }}

            Dim tmp As String = IO.Path.ChangeExtension(IO.Path.GetTempFileName(), ".xml")
            If IO.File.Exists(tmp) Then Kill(tmp)

            Select Case TaskType
                Case TaskType.Shutdown
                    Dim XML_Scheme As String = String.Format(My.Resources.XML_Shutdown, File)
                    IO.File.WriteAllText(tmp, XML_Scheme)
                    process.StartInfo.Arguments = "/Create /TN WinPaletter\Shutdown /XML """ & tmp & """"

                Case TaskType.Logoff
                    Dim XML_Scheme As String = String.Format(My.Resources.XML_Logoff, File)
                    IO.File.WriteAllText(tmp, XML_Scheme)
                    process.StartInfo.Arguments = "/Create /TN WinPaletter\Logoff /XML """ & tmp & """"

                Case TaskType.Logon
                    Dim XML_Scheme As String = String.Format(My.Resources.XML_Logon, File)
                    IO.File.WriteAllText(tmp, XML_Scheme)
                    process.StartInfo.Arguments = "/Create /TN WinPaletter\Logon /XML """ & tmp & """"
            End Select

            process.Start()
            process.WaitForExit()

            If IO.File.Exists(tmp) Then Kill(tmp)
        End If
    End Sub
    Private Shared Sub DeleteTask(TaskType As TaskType)
        Dim process As New Process With {.StartInfo = New ProcessStartInfo With {
               .FileName = My.PATH_System32 & "\schtasks",
               .Verb = If(My.WXP AndAlso My.isElevated, "", "runas"),
               .WindowStyle = ProcessWindowStyle.Hidden,
               .CreateNoWindow = True,
               .UseShellExecute = True
            }}

        Select Case TaskType
            Case TaskType.Shutdown
                process.StartInfo.Arguments = "/Delete /TN WinPaletter\Shutdown /F"

            Case TaskType.Logoff
                process.StartInfo.Arguments = "/Delete /TN WinPaletter\Logoff /F"

            Case TaskType.Logon
                process.StartInfo.Arguments = "/Delete /TN WinPaletter\Logon /F"

        End Select

        process.Start()
        process.WaitForExit()
    End Sub


#End Region

#Region "Dark\Light Mode"
    Public Shared Function GetDarkMode() As Boolean

        If My.Settings.Appearance.ManagedByTheme AndAlso My.Settings.Appearance.CustomColors Then
            Return My.Settings.Appearance.CustomTheme

        Else
            Dim i As Long

            If System.ComponentModel.LicenseManager.UsageMode = System.ComponentModel.LicenseUsageMode.Designtime Then
                Return True
            Else
                Try
                    If My.Settings.Appearance.AutoDarkMode Then
                        If My.W11 Or My.W10 Then
                            Try
                                i = CLng(My.Computer.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Themes\Personalize").GetValue("AppsUseLightTheme", 0))
                                Return Not (i = 1)
                            Catch ex As Exception
                                Try
                                    Return My.Settings.Appearance.DarkMode
                                Catch
                                    Return My.W11 Or My.W10
                                End Try
                            Finally
                                My.Computer.Registry.CurrentUser.Close()
                            End Try
                        Else
                            Return False
                        End If
                    Else
                        Return My.Settings.Appearance.DarkMode
                    End If
                Catch
                    Try
                        Return My.Settings.Appearance.DarkMode
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

    Public Shared Sub ApplyDarkMode(Optional [Form] As Form = Nothing, Optional IgnoreTitleBar As Boolean = False)
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
            DarkMode = GetDarkMode() 'Must be before BackColor
            BackColor = If(DarkMode, DefaultBackColorDark, DefaultBackColorLight)
            AccentColor = DefaultAccent
            CustomR = False
        End If

        Style = New XenonStyle(AccentColor, BackColor)

        If Form Is Nothing Then

            '####################### For all open forms
            Try
                For Each OFORM As Form In Application.OpenForms
                    Dim FormWasVisible As Boolean = OFORM.Visible
                    If FormWasVisible Then OFORM.Visible = False
                    OFORM.SuspendLayout()
                    OFORM.BackColor = BackColor
                    If Not IgnoreTitleBar Then DLLFunc.DarkTitlebar(OFORM.Handle, DarkMode)
                    EnumControls(OFORM, DarkMode)

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
            EnumControls([Form], DarkMode)

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
    Public Shared Sub EnumControls(ByVal ctrl As Control, ByVal DarkMode As Boolean)
        'This will make all control have a consistent dark\light mode.
        Dim ctrl_theme As CtrlTheme = If(DarkMode, CtrlTheme.DarkExplorer, CtrlTheme.Default)
        SetTheme(ctrl.Handle, ctrl_theme)

        Dim b As Boolean = False
        If TypeOf ctrl Is RetroButton Then b = True
        If TypeOf ctrl Is RetroGroupBox Then b = True
        If TypeOf ctrl Is XenonLabel Then b = True
        If TypeOf ctrl Is RetroPanel Then b = True
        If TypeOf ctrl Is RetroPanelRaised Then b = True
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

        ElseIf TypeOf ctrl Is XenonRadioImage Then
            DirectCast(ctrl, XenonRadioImage).BackColor = GetParentColor(ctrl).CB(If(GetParentColor(ctrl).IsDark, 0.05, -0.05))

        ElseIf TypeOf ctrl Is XenonButton Then
            With DirectCast(ctrl, XenonButton)
                .BackColor = GetParentColor(ctrl).CB(If(GetParentColor(ctrl).IsDark, 0.04, -0.03))
                If .DrawOnGlass Then
                    .ForeColor = New VisualStyles.VisualStyleRenderer(VisualStyles.VisualStyleElement.Window.Caption.Active).GetColor(VisualStyles.ColorProperty.TextColor).Invert
                End If
            End With

        ElseIf TypeOf ctrl Is RichTextBox Then
            ctrl.BackColor = ctrl.Parent.BackColor

        ElseIf TypeOf ctrl Is LinkLabel Then
            DirectCast(ctrl, LinkLabel).LinkColor = If(DarkMode, Color.White, Color.Black)

        ElseIf TypeOf ctrl Is XenonLinkLabel Then
            DirectCast(ctrl, XenonLinkLabel).LinkColor = If(DarkMode, Color.White, Color.Black)

        ElseIf TypeOf ctrl Is TreeView Then
            With DirectCast(ctrl, TreeView)
                .BackColor = ctrl.Parent.BackColor
                .ForeColor = If(DarkMode, Color.White, Color.Black)
            End With

        ElseIf TypeOf ctrl Is XenonTreeView Then
            With DirectCast(ctrl, XenonTreeView)
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

        End If

        If ctrl.HasChildren Then
            For Each c As Control In ctrl.Controls
                If TypeOf c Is TabPage Then c.BackColor = ctrl.Parent.BackColor
                EnumControls(c, DarkMode)
            Next
        End If

        If ctrl.FindForm.Visible Then ctrl.Refresh()
    End Sub
    Public Shared Sub SetTheme(ByVal handle As IntPtr, ByVal theme As CtrlTheme)
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

''' <summary>
''' Functions that help you draw\drop special DWM effects (Tabbed\Mica\Acrylic\Aero) on a form
''' </summary>
Public Module FormDWMEffects

    ''' <summary>
    ''' Draw effect on form depending on both user choice (Tabbed\Mica\Acrylic\Aero) and current OS
    ''' </summary>
    <Extension()>
    Public Sub DrawDWMEffect([Form] As Form, Margins As Padding, Optional Border As Boolean = True, Optional FormStyle As FormStyle = FormStyle.Mica)

        If Margins = Nothing Then Margins = New Padding(-1, -1, -1, -1)

        Dim CompositionEnabled As Boolean
        Try
            Dwmapi.DwmIsCompositionEnabled(CompositionEnabled)
        Catch
            CompositionEnabled = False
        End Try

        Dim Transparency_W11_10 As Boolean
        Transparency_W11_10 = (My.W10 Or My.W11) AndAlso Reg_IO.GetReg("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", True)

        Try
            If My.W11 AndAlso Transparency_W11_10 Then
                If FormStyle = FormStyle.Mica Then
                    [Form].DrawMica(Margins, MicaStyle.Mica)

                ElseIf FormStyle = FormStyle.Tabbed Then
                    [Form].DrawMica(Margins, MicaStyle.Tabbed)

                ElseIf FormStyle = FormStyle.Acrylic Then
                    [Form].DrawAcrylic(Border)

                Else
                    [Form].DrawMica(Margins, MicaStyle.Mica)

                End If

            ElseIf My.W10 AndAlso Transparency_W11_10 Then
                [Form].DrawAcrylic(Border)

            ElseIf (My.W7 Or My.WVista) AndAlso CompositionEnabled Then
                [Form].DrawAero(Margins)

            Else
                [Form].DrawTransparentGray

            End If

        Catch
            [Form].DrawTransparentGray

        End Try

    End Sub

    <Extension()>
    Sub DrawAcrylic(Form As Form, Optional Border As Boolean = True)
        Dim accent = New User32.AccentPolicy With {.AccentState = NativeMethods.User32.AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND}
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
    Sub DrawMica(Form As Form, Margins As Padding, Optional Style As MicaStyle = MicaStyle.Mica)
        Dim CompositionEnabled As Boolean
        Try
            Dwmapi.DwmIsCompositionEnabled(CompositionEnabled)
        Catch
            CompositionEnabled = False
        End Try

        Dim FS As New FormStyle
        If Style = MicaStyle.Mica Then FS = FormStyle.Mica
        If Style = MicaStyle.Tabbed And My.W11_22523 Then FS = FormStyle.Tabbed Else FS = FormStyle.Mica

        If Margins = Nothing Then Margins = New Padding(-1, -1, -1, -1)
        Dim DWM_Margins As New Dwmapi.MARGINS With {.leftWidth = Margins.Left, .rightWidth = Margins.Right, .topHeight = Margins.Top, .bottomHeight = Margins.Bottom}

        DLLFunc.DarkTitlebar(Form.Handle, XenonCore.GetDarkMode)
        Dwmapi.DwmSetWindowAttribute(Form.Handle, Dwmapi.DWMATTRIB.SYSTEMBACKDROP_TYPE, CInt(FS), Marshal.SizeOf(GetType(Integer)))
        Dwmapi.DwmExtendFrameIntoClientArea(Form.Handle, DWM_Margins)
    End Sub

    <Extension()>
    Sub DrawAero(Form As Form, Margins As Padding)
        If Margins = Nothing Then Margins = New Padding(-1, -1, -1, -1)
        Dim DWM_Margins As New Dwmapi.MARGINS With {.leftWidth = Margins.Left, .rightWidth = Margins.Right, .topHeight = Margins.Top, .bottomHeight = Margins.Bottom}
        Dwmapi.DwmExtendFrameIntoClientArea(Form.Handle, DWM_Margins)
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
    Sub DrawCustomTitlebar([Form] As Form, Optional BackColor As Color = Nothing, Optional BorderColor As Color = Nothing, Optional ForeColor As Color = Nothing)

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
                info.CallBack?.Invoke(info.Container, info.ColorProperty)
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

Public Class RGBColorComparer
    Implements IComparer(Of Color)
    Public Function Compare(a As Color, b As Color) As Integer Implements IComparer(Of Color).Compare
        'Compare hue values
        Dim hueComparison As Integer = a.GetHue().CompareTo(b.GetHue())
        If hueComparison <> 0 Then Return hueComparison

        'Compare brightness values
        Dim brightnessComparison As Integer = a.GetBrightness().CompareTo(b.GetBrightness())
        If brightnessComparison <> 0 Then Return brightnessComparison

        'Compare saturation values
        Dim saturationComparison As Integer = a.GetSaturation().CompareTo(b.GetSaturation())
        Return saturationComparison
    End Function
End Class