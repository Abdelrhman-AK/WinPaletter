Imports System.IO
Imports Devcorp.Controls.VisualStyles
Imports WinPaletter.XenonCore
Public Class VS2Win32UI
    Private Sub VS2Win32UI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        Icon = Win32UI.Icon
    End Sub

    Private Sub XenonButton16_Click(sender As Object, e As EventArgs) Handles XenonButton16.Click
        If OpenFileDialog2.ShowDialog = DialogResult.OK Then
            XenonTextBox1.Text = OpenFileDialog2.FileName
        End If
    End Sub

    Private Sub XenonButton8_Click(sender As Object, e As EventArgs) Handles XenonButton8.Click
        Try
            Dim theme As String = ""

            If Path.GetExtension(XenonTextBox1.Text) = ".theme" Then
                theme = XenonTextBox1.Text

            ElseIf Path.GetExtension(XenonTextBox1.Text) = ".msstyles" Then
                theme = My.Application.appData & "\VisualStyles\Luna\win32uischeme.theme"
                File.WriteAllText(My.Application.appData & "\VisualStyles\Luna\win32uischeme.theme", String.Format("[VisualStyles]{1}Path={0}{1}ColorStyle=NormalColor{1}Size=NormalSize", XenonTextBox1.Text, vbCrLf))
            End If

            If File.Exists(XenonTextBox1.Text) AndAlso File.Exists(theme) And Not String.IsNullOrEmpty(theme) Then
                Dim vs As New VisualStyleFile(theme)
                LoadColors(vs.Metrics)
                Win32UI.ApplyRetroPreview()
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub LoadColors(vs As VisualStyleMetrics)
        Win32UI.XenonToggle1.Checked = vs.FlatMenus
        'Win32UI.ActiveBorder_pick.BackColor = vs.colors.ActiveBorder
        Win32UI.activetitle_pick.BackColor = vs.Colors.ActiveCaption
        Win32UI.AppWorkspace_pick.BackColor = vs.Colors.AppWorkspace
        Win32UI.background_pick.BackColor = vs.Colors.Background
        'Win32UI.btnaltface_pick.BackColor = vs.colors.ButtonAlternateFace
        Win32UI.btndkshadow_pick.BackColor = vs.Colors.DkShadow3d
        Win32UI.btnface_pick.BackColor = vs.Colors.Btnface
        Win32UI.btnhilight_pick.BackColor = vs.Colors.BtnHighlight
        Win32UI.btnlight_pick.BackColor = vs.Colors.Light3d
        Win32UI.btnshadow_pick.BackColor = vs.Colors.BtnShadow
        'Win32UI.btntext_pick.BackColor = vs.colors.WindowText
        Win32UI.GActivetitle_pick.BackColor = vs.Colors.GradientActiveCaption
        Win32UI.GInactivetitle_pick.BackColor = vs.Colors.GradientInactiveCaption
        Win32UI.GrayText_pick.BackColor = vs.Colors.GrayText
        Win32UI.hilighttext_pick.BackColor = vs.Colors.HighlightText
        Win32UI.hottracking_pick.BackColor = vs.Colors.HotTracking
        'Win32UI.InactiveBorder_pick.BackColor = vs.colors.InactiveBorder
        Win32UI.InactiveTitle_pick.BackColor = vs.Colors.InactiveCaption
        Win32UI.InactivetitleText_pick.BackColor = vs.Colors.InactiveCaptionText
        'Win32UI.InfoText_pick.BackColor = vs.colors.InfoText
        'Win32UI.InfoWindow_pick.BackColor = vs.colors.InfoWindow
        Win32UI.menu_pick.BackColor = vs.Colors.Menu
        Win32UI.menubar_pick.BackColor = vs.Colors.MenuBar
        Win32UI.menutext_pick.BackColor = vs.Colors.MenuText
        'Win32UI.Scrollbar_pick.BackColor = vs.colors.Scrollbar
        Win32UI.TitleText_pick.BackColor = vs.Colors.CaptionText
        Win32UI.Window_pick.BackColor = vs.Colors.Window
        'Win32UI.Frame_pick.BackColor = vs.colors.WindowFrame
        Win32UI.WindowText_pick.BackColor = vs.Colors.WindowText
        Win32UI.hilight_pick.BackColor = vs.Colors.Highlight
        Win32UI.menuhilight_pick.BackColor = vs.Colors.MenuHilight
        Win32UI.desktop_pick.BackColor = vs.Colors.Background
    End Sub

    Private Sub XenonButton7_Click(sender As Object, e As EventArgs) Handles XenonButton7.Click
        Close()
    End Sub
End Class