Imports System.ComponentModel
Imports System.IO
Imports System.Net.NetworkInformation
Imports System.Reflection
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
    Public Shared Function LoadFromDLL(File As String, ResourceID As Integer, Optional ResourceType As String = "IMAGE", Optional UnfoundW As Integer = 50, Optional UnfoundH As Integer = 50) As Bitmap
        Try

            If IO.File.Exists(File) Then
                Dim hMod As IntPtr = NativeMethods.Kernel32.LoadLibraryEx(File, IntPtr.Zero, &H2)
                Dim hRes As IntPtr = NativeMethods.Kernel32.FindResource(hMod, ResourceID, ResourceType)
                Dim size As UInteger = NativeMethods.Kernel32.SizeofResource(hMod, hRes)
                Dim pt As IntPtr = NativeMethods.Kernel32.LoadResource(hMod, hRes)
                Dim bPtr As Byte() = New Byte(size - 1) {}
                Marshal.Copy(pt, bPtr, 0, CInt(size))
                Return Image.FromStream(New MemoryStream(bPtr))
            Else
                Return Color.Black.ToBitmap(New Size(UnfoundW, UnfoundH))
            End If
        Catch
            Return Color.Black.ToBitmap(New Size(UnfoundW, UnfoundH))
        End Try

    End Function

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
            If (ni.Description.IndexOf("virtual", StringComparison.OrdinalIgnoreCase) >= 0) OrElse (ni.Name.IndexOf("virtual", StringComparison.OrdinalIgnoreCase) >= 0) Then Continue For
            If ni.Description.Equals("Microsoft Loopback Adapter", StringComparison.OrdinalIgnoreCase) Then Continue For
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
                        i = CLng(My.Computer.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Themes\Personalize").GetValue("AppsUseLightTheme", 0))
                        If i = 1 Then
                            Return False
                        Else
                            Return True
                        End If
                    Else
                        Return My.[Settings].Appearance_Dark
                    End If
                Catch
                    Try
                        Return My.[Settings].Appearance_Dark
                    Catch
                        Return True
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

        If My.Settings.Appearance_Custom Then
            BackColor = My.Settings.Appearance_Back
            AccentColor = My.Settings.Appearance_Accent
        Else
            BackColor = If(DarkMode, DefaultBackColorDark, DefaultBackColorLight)
            AccentColor = DefaultAccent
        End If

        If Form Is Nothing Then

            '####################### For all open forms
            Try
                For Each OFORM As Form In Application.OpenForms
                    OFORM.Visible = False
                    If DirectCast(OFORM, Form).BackColor <> BackColor Then
                        DirectCast(OFORM, Form).BackColor = BackColor
                    End If
                    User32.DarkTitlebar(DirectCast(OFORM, Form).Handle, DarkMode)
                    EnumControls(DirectCast(OFORM, Form), DarkMode)
                    DirectCast(OFORM, Form).Refresh()
                    OFORM.Visible = True
                Next
            Catch

            End Try

        Else
            '####################### For Selected [Form]

            If DirectCast([Form], Form).BackColor <> BackColor Then
                DirectCast([Form], Form).BackColor = BackColor
            End If
            User32.DarkTitlebar([Form].Handle, DarkMode)
            EnumControls(TryCast([Form], Form), DarkMode)
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

        If TypeOf ctrl Is DataGridView Then
            Dim ColumnBack As Color
            Dim Fore As Color
            Dim CellBack As Color

            Select Case DarkMode
                Case True
                    ColumnBack = Color.FromArgb(60, 60, 60)
                    Fore = Color.White
                    CellBack = ctrl.Parent.BackColor
                Case False
                    ColumnBack = Color.FromArgb(150, 150, 150)
                    Fore = Color.Black
                    CellBack = ctrl.Parent.BackColor
            End Select

            For Each Row As DataGridViewRow In TryCast(ctrl, DataGridView).Rows
                Row.DefaultCellStyle.ForeColor = Fore
            Next

            TryCast(ctrl, DataGridView).ColumnHeadersDefaultCellStyle.BackColor = ColumnBack
            TryCast(ctrl, DataGridView).ColumnHeadersDefaultCellStyle.ForeColor = Fore
            TryCast(ctrl, DataGridView).BackColor = ctrl.Parent.BackColor
            TryCast(ctrl, DataGridView).BackgroundColor = ctrl.Parent.BackColor
            TryCast(ctrl, DataGridView).DefaultCellStyle.BackColor = CellBack
            TryCast(ctrl, DataGridView).ForeColor = Fore
            TryCast(ctrl, DataGridView).DefaultCellStyle.ForeColor = Fore
            TryCast(ctrl, DataGridView).RowTemplate.DefaultCellStyle.ForeColor = Fore

            ctrl.Invalidate()
            ctrl.Refresh()
        End If

        If TypeOf ctrl Is TreeView Then
            With TryCast(ctrl, TreeView)
                .BackColor = ctrl.Parent.BackColor
                .ForeColor = If(DarkMode, Color.White, Color.Black)
            End With
        End If

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
            TD = New TaskDialog With {
                    .EnableHyperlinks = True,
                    .RightToLeft = My.Lang.RightToLeft,
                    .ButtonStyle = TaskDialogButtonStyle.CommandLinksNoIcon,
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

            Dim okButton As New TaskDialogButton(ButtonType.Ok) With {.Text = My.Lang.OK, .ElevationRequired = RequireElevation}
            Dim yesButton As New TaskDialogButton(ButtonType.Yes) With {.Text = My.Lang.Yes, .ElevationRequired = RequireElevation}
            Dim noButton As New TaskDialogButton(ButtonType.No) With {.Text = My.Lang.No}
            Dim cancelButton As New TaskDialogButton(ButtonType.Cancel) With {.Text = My.Lang.Cancel}
            Dim retryButton As New TaskDialogButton(ButtonType.Retry) With {.Text = My.Lang.Retry, .ElevationRequired = RequireElevation}
            Dim closeButton As New TaskDialogButton(ButtonType.Close) With {.Text = My.Lang.Close}
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
                icon = TaskDialogIcon.Custom
                TD.CustomMainIcon = Shell32.GetSystemIcon(Shell32.SHSTOCKICONID.HELP, Shell32.SHGSI.ICON)
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
            Return Interaction.MsgBox(Message, Style, SubMessage)
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

#End Region
End Class

Public Class Acrylism

    Public Shared Sub EnableBlur(ByVal [Form] As Form, Optional ByVal Border As Boolean = True)
        If My.W11 Or My.W10 Then
            [Form].BackColor = Color.Black
            [Form].Opacity = 1
            [Form].FormBorderStyle = FormBorderStyle.None

            Try
                If My.Computer.Registry.GetValue("HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "EnableTransparency", True) Then
                    Dim accent = New NativeMethods.User32.AccentPolicy With {.AccentState = NativeMethods.User32.AccentState.ACCENT_ENABLE_BLURBEHIND}
                    If Border Then accent.AccentFlags = &H20 Or &H40 Or &H80 Or &H100
                    Dim accentStructSize = Marshal.SizeOf(accent)
                    Dim accentPtr = Marshal.AllocHGlobal(accentStructSize)
                    Marshal.StructureToPtr(accent, accentPtr, False)

                    Dim Data = New User32.WindowCompositionAttributeData With {
                            .Attribute = User32.WindowCompositionAttribute.WCA_ACCENT_POLICY,
                            .SizeOfData = accentStructSize,
                            .Data = accentPtr
                        }

                    User32.SetWindowCompositionAttribute([Form].Handle, Data)
                    Marshal.FreeHGlobal(accentPtr)

                    'User32.DarkTitlebar([Form].Handle, XenonCore.GetDarkMode())
                    'Dwmapi.DwmSetWindowAttribute([Form].Handle, Dwmapi.DWMATTRIB.DWMWA_SYSTEMBACKDROP_TYPE, &H3, Marshal.SizeOf(GetType(Integer)))

                Else
                    GrayscaleMe([Form])
                End If

            Catch
                Try
                    Dim accent = New NativeMethods.User32.AccentPolicy With {.AccentState = NativeMethods.User32.AccentState.ACCENT_ENABLE_BLURBEHIND}
                    If Border Then accent.AccentFlags = &H20 Or &H40 Or &H80 Or &H100
                    Dim accentStructSize = Marshal.SizeOf(accent)
                    Dim accentPtr = Marshal.AllocHGlobal(accentStructSize)
                    Marshal.StructureToPtr(accent, accentPtr, False)

                    Dim Data = New User32.WindowCompositionAttributeData With {
                            .Attribute = User32.WindowCompositionAttribute.WCA_ACCENT_POLICY,
                            .SizeOfData = accentStructSize,
                            .Data = accentPtr
                        }

                    User32.SetWindowCompositionAttribute([Form].Handle, Data)
                    Marshal.FreeHGlobal(accentPtr)

                Catch ex As Exception
                    GrayscaleMe([Form])
                End Try
            End Try

        ElseIf My.W7 Then

            Dim Com As Boolean
            NativeMethods.Dwmapi.DwmIsCompositionEnabled(Com)

            [Form].FormBorderStyle = FormBorderStyle.Sizable

            If Com Then
                Dwmapi.DwmExtendFrameIntoClientArea([Form].Handle, New Dwmapi.MARGINS With {.leftWidth = -1, .rightWidth = -1, .topHeight = -1, .bottomHeight = -1})
            Else
                GrayscaleMe([Form])
            End If

        Else
            GrayscaleMe([Form])

        End If

    End Sub

    Shared Sub GrayscaleMe([Form] As Form)
        [Form].BackColor = Color.FromArgb(20, 20, 20)
        [Form].Opacity = 0.8
    End Sub

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