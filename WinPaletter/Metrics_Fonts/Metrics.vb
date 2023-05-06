Imports System.Runtime.InteropServices

Public Class Metrics
    Public Structure NONCLIENTMETRICS           'NEVER CHANGE VARIABLES ORDERS
        Public cbSize As Integer
        Public iBorderWidth As Integer
        Public iScrollWidth As Integer
        Public iScrollHeight As Integer
        Public iCaptionWidth As Integer
        Public iCaptionHeight As Integer
        Public lfCaptionFont As LogFont
        Public iSMCaptionWidth As Integer
        Public iSMCaptionHeight As Integer
        Public lfSMCaptionFont As LogFont
        Public iMenuWidth As Integer
        Public iMenuHeight As Integer
        Public lfMenuFont As LogFont
        Public lfStatusFont As LogFont
        Public lfMessageFont As LogFont
        Public iPaddedBorderWidth As Integer
    End Structure

    Public Structure ICONMETRICS
        Dim cbSize As UInteger
        Dim iHorzSpacing As Integer
        Dim iVertSpacing As Integer
        Dim iTitleWrap As Integer
        Dim lfFont As LogFont
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Public Structure ANIMATIONINFO
        Public cbSize As UInteger
        Public IMinAnimate As Integer
    End Structure
End Class