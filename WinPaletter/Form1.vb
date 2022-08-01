Imports WinPaletter.XenonCore
Imports System.IO
Imports System.Drawing.Imaging
Imports AnimCur
Imports Microsoft.Win32
Imports System.Text

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyDarkMode(Me)
    End Sub

    Dim Noise As New TextureBrush(FadeBitmap(My.Resources.GaussianBlurOpaque, 0.2))

    Private Sub XenonButton2_Click(sender As Object, e As EventArgs) Handles XenonButton2.Click
        Dim binaryStr As String = XenonTextBox1.Text

        Dim ar As Byte()
        ar = StringToBytesArray(binaryStr)

        If ar.Count < 8 Then
            For i = 0 To 7 - ar.Count
                ar = AddByteToArray(ar, 0)
            Next
        End If

        ar = ar.Reverse.ToArray

        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Control Panel\Desktop", "UserPreferencesMask", ar, RegistryValueKind.Binary)
    End Sub

    Public Function AddByteToArray(ByVal bArray As Byte(), ByVal newByte As Byte) As Byte()
        Dim newArray As Byte() = New Byte(bArray.Length + 1 - 1) {}
        bArray.CopyTo(newArray, 1)
        newArray(0) = newByte
        Return newArray
    End Function

    Private Function StringToBytesArray(ByVal str As String) As Byte()
        Dim bitsToPad = 8 - str.Length Mod 8

        If bitsToPad <> 8 Then
            Dim neededLength = bitsToPad + str.Length
            str = str.PadLeft(neededLength, "0"c)
        End If

        Dim size As Integer = str.Length / 8
        Dim arr As Byte() = New Byte(size - 1) {}

        For a As Integer = 0 To size - 1
            arr(a) = Convert.ToByte(str.Substring(a * 8, 8), 2)
        Next

        Return arr
    End Function

    Private Sub XenonButton1_Click(sender As Object, e As EventArgs) Handles XenonButton1.Click
        'Dim fs As FileStream = New FileStream("D:\cur.cur", FileMode.Create)
        'Dim EO As New EOIcoCurWriter(fs, 7, EOIcoCurWriter.IcoCurType.Cursor)

        'For i As Single = 1 To 4 Step 0.5
        'EO.WriteBitmap(Draw(i), Nothing, New Point(5 * i - 0.5 * i, 10 * i - 0.5 * i)) 'New Point(1, 1)
        'Next

        'fs.Close()


        Dim hexstring As Byte() = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop", "UserPreferencesMask", Nothing)

        Dim binarystring As String = String.Join("", hexstring.Reverse().[Select](Function(x) Convert.ToString(x, 2).PadLeft(8, "0"c)))

        Dim En As Boolean = If(binarystring(binarystring.Count - 1 - 17) = CChar("1"), True, False)

        MsgBox(En)
    End Sub

    Public Shared Function ByteArrayToString(ByVal ba As Byte()) As String
        Dim hex As StringBuilder = New StringBuilder(ba.Length * 2)

        For Each b As Byte In ba
            hex.AppendFormat("{0:x1}", b)
        Next

        Return hex.ToString()
    End Function

    Public Shared Function Reverse(ByVal s As String) As String
        Dim charArray As Char() = s.ToCharArray()
        Array.Reverse(charArray)
        Return New String(charArray)
    End Function


    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    End Sub


End Class