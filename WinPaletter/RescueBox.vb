Imports WinPaletter.XenonCore

Public Class RescueBox
    Private _AutoClosing As Boolean = False

    Private Sub RescueBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
        _AutoClosing = True
        int = 0
        Timer1.Enabled = True
        Timer1.Start()
        Label11.Visible = False
        Dim CV As String = "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion"
        Label10.Text = My.Computer.Info.OSFullName.Replace("Microsoft ", "") & " " & Microsoft.Win32.Registry.GetValue(CV, "DisplayVersion", Nothing) & " " & String.Format("{0}.{1}", Microsoft.Win32.Registry.GetValue(CV, "CurrentBuildNumber", 0), Microsoft.Win32.Registry.GetValue(CV, "UBR", 0))

        Dim scr = Screen.FromPoint(Me.Location)
        Me.Location = New Point(scr.WorkingArea.Right - Me.Width, scr.WorkingArea.Top + 5)

        For Each Ctrl As XenonButton In XenonGroupBox2.Controls.OfType(Of XenonButton)
            AddHandler Ctrl.Click, AddressOf StopAutoClose
        Next

        For Each Ctrl As XenonButton In XenonGroupBox3.Controls.OfType(Of XenonButton)
            AddHandler Ctrl.Click, AddressOf StopAutoClose
        Next
    End Sub

    Protected Overrides Function ProcessKeyPreview(ByRef m As System.Windows.Forms.Message) As Boolean
        If m.Msg = &H100 Then  'WM_KEYDOWN
            Dim key As Keys = m.WParam
            'If key = Keys.S And My.Computer.Keyboard.CtrlKeyDown Then

            _AutoClosing = False
            Text = "Rescue Box"

            Return True
            'End If
        End If

        Return MyBase.ProcessKeyPreview(m)
    End Function


    Private Sub RescueBox_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        int = 0
        Timer1.Enabled = False
        Timer1.Stop()

        For Each Ctrl As XenonButton In XenonGroupBox2.Controls.OfType(Of XenonButton)
            RemoveHandler Ctrl.Click, AddressOf StopAutoClose
        Next

        For Each Ctrl As XenonButton In XenonGroupBox3.Controls.OfType(Of XenonButton)
            RemoveHandler Ctrl.Click, AddressOf StopAutoClose
        Next
    End Sub

    Sub StopAutoClose()
        My.Application.AnimatorX.Hide(Label11)
        int = 0
        Timer1.Enabled = False
        Timer1.Stop()
    End Sub

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        Try : My.Application.processKiller.Kill() : Catch : End Try
    End Sub

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Try : My.Application.processExplorer.Start() : Catch : End Try
    End Sub

    Private Sub XenonButton3_Click(sender As Object, e As EventArgs) Handles XenonButton3.Click

        Dim sfcInfo As New ProcessStartInfo With {
                .FileName = "sfc.exe",
                .Verb = "runas",
                .Arguments = "/scannow",
                .WindowStyle = ProcessWindowStyle.Normal,
                .UseShellExecute = True
            }

        Dim Prc As New Process With {.StartInfo = sfcInfo}
        Prc.Start()

    End Sub

    Private Sub XenonButton4_Click(sender As Object, e As EventArgs) Handles XenonButton4.Click
        Dim dismInfo As New ProcessStartInfo With {
        .FileName = "dism.exe",
        .Verb = "runas",
        .Arguments = "/Online /Cleanup-Image /RestoreHealth",
        .WindowStyle = ProcessWindowStyle.Normal,
        .UseShellExecute = True
    }

        Dim Prc As New Process With {.StartInfo = dismInfo}
        Prc.Start()
    End Sub

    Private int As Integer = 0

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If int >= 4 Then
            int = 0

            With My.Application
                Dim StartMenuExperienceHost As Boolean
                Try
                    StartMenuExperienceHost = Not Process.GetProcessesByName("StartMenuExperienceHost")(0).HasExited

                    If Not StartMenuExperienceHost Then
                        .AnimatorX.Show(Label11)
                    Else
                        If _AutoClosing Then Me.Close()
                    End If

                Catch
                    .AnimatorX.Show(Label11)
                End Try
            End With

            Timer1.Enabled = False
            Timer1.Stop()

        Else
            int += 1

            With My.Application
                Dim StartMenuExperienceHost As Boolean
                Try
                    StartMenuExperienceHost = Not Process.GetProcessesByName("StartMenuExperienceHost")(0).HasExited
                    If Not StartMenuExperienceHost Then
                    Else
                        If _AutoClosing Then Me.Close()
                    End If
                Catch
                End Try
            End With
        End If
    End Sub

    Private Sub XenonButton5_Click(sender As Object, e As EventArgs) Handles XenonButton5.Click
        Try : My.Application.processKiller.Start() : Catch : End Try
    End Sub

    Private Sub XenonButton6_Click(sender As Object, e As EventArgs) Handles XenonButton6.Click
        Shell("shutdown.exe -r -t 20", AppWinStyle.Hide)
    End Sub
End Class