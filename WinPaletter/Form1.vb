
Imports System.Reflection
Imports WinPaletter.XenonCore

Public Class Form1
    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        'Dim L As New Localizer
        'L.ExportLanguage("D:\X.txt")

        MyCodes_Users_Salahiyat("D:\X.txt")
    End Sub


    Public Sub MyCodes_Users_Salahiyat(File As String)
        Dim LS As New List(Of String)
        LS.Clear()

        For Each f In Assembly.GetExecutingAssembly().GetTypes().
                Where(Function(t) GetType(Form).IsAssignableFrom(t))

            Using ins = DirectCast(Activator.CreateInstance(f), Form)
                ' You have a Form here, do what you need to do...
                LS.Add(ins.Name & "= " & ins.Text)
                For Each ctrl In GetAllControls(ins)
                    LS.Add(ins.Name & "\" & ctrl.Name & "= " & ctrl.Text)
                Next
            End Using
        Next

        IO.File.WriteAllText(File, CStr_FromList(LS))
    End Sub

    ' A recursive method to get all the controls...
    Private Function GetAllControls(parent As Control) As IEnumerable(Of Control)
        Dim cs = parent.Controls.OfType(Of Control)
        Return cs.SelectMany(Function(c) GetAllControls(c)).Concat(cs)
    End Function


    Public Function getAllTypesOfControl(assembly As Assembly) As IEnumerable(Of Type)
        Return assembly.GetTypes().
        Where(Function(t) t.IsSubclassOf(GetType(ContainerControl))).
        SelectMany(Function(container) container.GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)).
        Where(Function(f) f.FieldType.IsSubclassOf(GetType(Control))).
        Select(Function(f) f.FieldType)
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        XenonCore.ApplyDarkMode(Me)
    End Sub
End Class