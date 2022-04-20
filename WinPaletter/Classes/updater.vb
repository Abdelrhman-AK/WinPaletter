Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.IO
Imports System.Net
Imports System.Reflection
Imports System.Text
Imports System.Text.RegularExpressions

Friend Class Updater
    Private ReadOnly _lazyVersionUrls As Lazy(Of IDictionary(Of Version, Uri)) = New Lazy(Of IDictionary(Of Version, Uri))(Function() _GetVersionUrls())

    Private ReadOnly Property _VersionUrls As IDictionary(Of Version, Uri)
        Get
            Return _lazyVersionUrls.Value
        End Get
    End Property

    Public Property GitHubRepo As String

    Public ReadOnly Property GitHubRepoName As String
        Get
            Dim si = GitHubRepo.LastIndexOf("/"c)
            Return GitHubRepo.Substring(si + 1)
        End Get
    End Property

    Private Function _GetVersionUrls() As IDictionary(Of Version, Uri)
        Dim pattern = String.Concat(Regex.Escape(GitHubRepo), "\/releases\/download\/Refresh.v[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+.*\.zip")
        Dim urlMatcher As Regex = New Regex(pattern, RegexOptions.CultureInvariant Or RegexOptions.Compiled)
        Dim result = New Dictionary(Of Version, Uri)()
        Dim wrq = WebRequest.Create(String.Concat("https://github.com", GitHubRepo, "/releases/latest"))
        Dim wrs As WebResponse = Nothing

        Try
            wrs = wrq.GetResponse()
        Catch ex As Exception
            Debug.WriteLine("Error fetching repo: " & ex.Message)
            Return result
        End Try

        Using sr = New StreamReader(wrs.GetResponseStream())
            Dim line As String

            While Not Equals(Nothing, (CSharpImpl.__Assign(line, sr.ReadLine())))
                Dim match = urlMatcher.Match(line)

                If match.Success Then
                    Dim uri = New Uri(String.Concat("https://github.com", match.Value))
                    Dim vs = match.Value.LastIndexOf("/Refresh.v")
                    Dim sa = match.Value.Substring(vs + 10).Split("."c, "/"c)
                    Dim v = New Version(Integer.Parse(sa(0)), Integer.Parse(sa(1)), Integer.Parse(sa(2)), Integer.Parse(sa(3)))
                    result.Add(v, uri)
                End If
            End While
        End Using

        Return result
    End Function

    Public ReadOnly Property HasUpdate As Boolean
        Get
            Dim v = Assembly.GetEntryAssembly().GetName().Version

            For Each e In _VersionUrls
                If e.Key > v Then Return True
            Next

            Return False
        End Get
    End Property

    Public ReadOnly Property LatestVersion As Version
        Get
            Dim v = Assembly.GetEntryAssembly().GetName().Version
            Dim va = New List(Of Version)(_VersionUrls.Keys)
            va.Add(v)
            va.Sort()
            Return va(va.Count - 1)
        End Get
    End Property

    Public Sub Update(ByVal Optional args As String() = Nothing)
        Update(LatestVersion, args)
    End Sub

    Public Sub Update(ByVal version As Version, ByVal Optional args As String() = Nothing)
        Dim ns = GetType(Updater).Namespace
        Dim names = Assembly.GetExecutingAssembly().GetManifestResourceNames()
        Dim exename As String = Nothing

        For i = 0 To names.Length - 1
            Dim name = names(i)

            If name.Contains(".ZZupdater0.") Then
                Dim respath = name
                If String.IsNullOrEmpty(exename) AndAlso name.EndsWith(".exe") Then exename = name.Substring(name.IndexOf("."c) + 1)
                name = name.Substring(name.IndexOf("."c) + 1)

                Using stm = Assembly.GetExecutingAssembly().GetManifestResourceStream(respath)

                    Using stm2 = File.OpenWrite(name)
                        stm2.SetLength(0L)
                        stm.CopyTo(stm2)
                    End Using
                End Using
            End If
        Next

        If Not Equals(Nothing, exename) Then
            Dim psi = New ProcessStartInfo()
            Dim sb = New StringBuilder()
            sb.Append(_Esc(Assembly.GetEntryAssembly().GetModules()(0).Name))
            sb.Append(" "c)
            sb.Append(_Esc(_VersionUrls(version).ToString()))

            If Nothing IsNot args Then
                Dim i = 0

                While i < args.Length
                    sb.Append(" "c)
                    sb.Append(_Esc(args(i)))
                    Threading.Interlocked.Increment(i)
                End While
            End If

            psi.Arguments = sb.ToString()
            psi.FileName = exename
            Dim proc = Process.Start(psi)
        End If
    End Sub

    Private Function _Esc(ByVal arg As String) As String
        Return String.Concat("""", arg.Replace("""", """"""), """")
    End Function

    Private Class CSharpImpl
        <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
        Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
            target = value
            Return value
        End Function
    End Class
End Class
