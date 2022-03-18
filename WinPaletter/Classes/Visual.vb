'' FILENAME:        Visual.vb
'' NAMESPACE:       PI.Common
'' APPLICATION:     N/A
'' CREATED BY:      Luke Berg
'' CREATED:         10-02-06
'' REVISED BY:      _revisedby_
'' REVISED:         _revised_
'' DESCRIPTION:     A common module of visual functions.

Imports System.ComponentModel

Public Class Visual

    Private Shared colorsFading As New Dictionary(Of String, BackgroundWorker) 'Keeps track of any backgroundworkers already fading colors
    Private Shared backgroundWorkers As New Dictionary(Of BackgroundWorker, ColorFaderInformation) 'Associate each background worker with information it needs

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
    Public Shared Sub FadeColor(ByVal container As Object, ByVal colorProperty As String, ByVal startColor As Color, ByVal endColor As Color, ByVal steps As Integer, ByVal delay As Integer, Optional ByVal callback As DoneFading = Nothing)
        Dim colorSteps(0) As ColorStep
        colorSteps(0) = New ColorStep(endColor, steps)
        FadeColor(container, colorProperty, startColor, colorSteps, delay, callback)
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
        FadeColor(container, colorProperty, startColor, colorSteps, delay, callback)
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

        Dim colorFader As BackgroundWorker

        ' Stores all the parameter information into a class that the background worker will access
        Dim colorFaderInfo As New ColorFaderInformation(container, colorProperty, startColor, colorSteps, delay, callBack)

        ' Checks if the color is already in the process of fading.
#Disable Warning BC42030
        If colorsFading.TryGetValue(GenerateHashCode(container, colorProperty), colorFader) Then
#Enable Warning BC42030

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

                CType(sender, BackgroundWorker).ReportProgress(0, Color.FromArgb(CInt(curA), CInt(curR), CInt(curG), CInt(curB)))

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
        Dim info As ColorFaderInformation
#Disable Warning BC42030
        If backgroundWorkers.TryGetValue(CType(sender, BackgroundWorker), info) Then
#Enable Warning BC42030
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